using System;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QDOTryItem 的摘要说明。
	/// </summary>
	public class QDOTryNo : OnWIPTRY
	{
		public string SNState;
		//采集工位  
		public string CollectionOPCODE;
	}
}
