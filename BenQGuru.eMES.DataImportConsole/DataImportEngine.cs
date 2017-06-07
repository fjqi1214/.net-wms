using System;
using System.Data;
using System.Xml;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.DataImportConsole
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	class DataImportEngine
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			new DataImportHelper().Import();						
		}
	}
}
