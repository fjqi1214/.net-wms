using System;
using System.Text.RegularExpressions;

using ControlLibrary.Web.Language;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// MessageCenter 的摘要说明。
	/// </summary>
	public class MessageCenter
	{
		public MessageCenter()
		{
		}

		private static ILanguageComponent _languageComponent = null;
		/// <summary>
		/// ** 功能描述:	实现信息的多语言,将以'$'开头，由 字母、数字及'_' 组成的字符串替换成languageComponent中找到的字符串
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2004/05/16
		/// ** 修 改:
		/// ** 日 期:
		/// ** 版本:
		/// </summary>
		/// <param name="originMsg">原始信息</param>
		/// <param name="language"></param>
		/// <returns></returns>
		public static string ParserMessage(string originMsg, ILanguageComponent languageComponent )
		{
			if ( originMsg == string.Empty )
			{
				return string.Empty;
			}

			_languageComponent = languageComponent;

			return new Regex(@"\$([A-Za-z0-9_]+)").Replace( originMsg, new MatchEvaluator( replaceErrorCode ) );
		}

		private static string replaceErrorCode( Match match )
		{
			return _languageComponent.GetString( match.Value );
		}
	}
}
