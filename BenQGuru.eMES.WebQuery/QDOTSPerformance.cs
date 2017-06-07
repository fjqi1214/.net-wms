using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QDOTSRecordPerformance 的摘要说明。
	/// </summary>
	public class QDOTSPerformance : DomainObject
	{
		[FieldMapAttribute("tsuser", typeof(string), 40, true)]
		public string TsOperator;

        [FieldMapAttribute("tsuserhidden", typeof(string), 40, true)]
        public string TsOperatorHidden;

		[FieldMapAttribute("tsqty", typeof(int), 10, true)]
		public int TsQuantity;
	}
}
