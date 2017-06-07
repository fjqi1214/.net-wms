using System;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QDOECNDetail 的摘要说明。
	/// </summary>
	public class QDOECNList : OnWIPECN
	{
		public string SNState;
		//采集工位  
		public string CollectionOPCODE;
	}
}
