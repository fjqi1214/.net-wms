using System;

namespace BenQGuru.eMES.Client.Service
{
	/// <summary>
	/// Laws Lu,2005/12/16,根据Rcard号码反向匹配工单
	/// </summary>
	public abstract class clsRCard2Mo
	{
		private clsRCard2Mo()
		{
			
		}
		//Laws Lu，2005/12/16，新增	匹配工单
		/// <summary>
		/// 产品序列号第二位应该和工单第一位相同
		/// 产品序列号第三~七位应该和工单最后五位相同
		/// </summary>
		/// <param name="rcardCode">产品序列号</param>
		/// <param name="moCode">工单</param>
		/// <returns>True：匹配/False：匹配</returns>

		public static bool MatchMo(string rcardCode,string moCode) 
		{
			bool iReturn = false;
			if(rcardCode.Length >= 7 && moCode != String.Empty && moCode.Length >= 6)
			{
				string RcardSecondString = rcardCode.Substring(1,1);
				string RcardThird2SevenString = rcardCode.Substring(2,5);
				string MoFirstString = moCode.Substring(0,1);
				string MoLastFiveString = moCode = moCode.Substring(moCode.Length - 5,5);
				
				if(RcardSecondString == MoFirstString
					&& MoLastFiveString == RcardThird2SevenString)
				{
					iReturn = true;
				}
			}

			return iReturn;
		}
		
		/// <summary>
		/// 根据输入的产品序列号和工单集合，选出匹配的工单
		/// </summary>
		/// <param name="rcard">产品序列号</param>
		/// <param name="moCodes">工单</param>
		/// <returns>工单</returns>
		public static string GetMatchMo(string rcard,string[] moCodes)
		{
			string iReturnMoCode = String.Empty;
			foreach(string moCode in moCodes)
			{
				if(MatchMo(rcard.Trim().ToUpper(),moCode.Trim().ToUpper()))
				{
					iReturnMoCode = moCode;
					break;
				}
			}

			return iReturnMoCode;
		}
	}
}
