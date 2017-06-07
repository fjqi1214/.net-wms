using System;
using BenQGuru.eMES.Common.MutiLanguage;
using System.Text.RegularExpressions;

namespace UserControl
{
	/// <summary>
	/// MutiLanguage 的摘要说明。
	/// </summary>
	public class MutiLanguages
	{
		public MutiLanguages()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static string Language
		{
			get
			{
				return defaultLanguage;
			}
			set
			{
				if(LanguageType.SimplifiedChinese == value || LanguageType.TraditionalChinese == value || LanguageType.English == value)
				{
					defaultLanguage = value;
				}
			}
		}
		private static string defaultLanguage = LanguageType.SimplifiedChinese;

		public static string ParserMessage(string originMsg )
		{
			string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,  "language.mdb");     
			LanguagePack languagePack = LanguagePack.Current(path);
			if ( originMsg == string.Empty )
			{
				return string.Empty;
			}
			string errMsg = originMsg;
			
			Regex regex = new Regex(@"\$([A-Za-z0-9_]+)");
			string word = string.Empty;

			foreach ( Match match in regex.Matches( errMsg ) )
			{
				word = languagePack.GetString( match.Value ,defaultLanguage);
				
				if ( word != null && word != string.Empty )
				{
					errMsg = errMsg.Replace( match.Value, word );
				}
			}

			return errMsg;
		}

		public static string ParserString(string origin)
		{
			string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,  "language.mdb");     
			LanguagePack languagePack = LanguagePack.Current(path);
			
			if ( origin == string.Empty )
			{
				return string.Empty;
			}
			
			return languagePack.GetString( origin ,defaultLanguage);
		}
	}
}
