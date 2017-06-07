using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QueryRecordPerformanceFacade 的摘要说明。
	/// </summary>
	public class QueryTSRecordPerformanceFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QueryTSRecordPerformanceFacade( IDomainDataProvider domainDataProvider )
		{
			this._domainDataProvider = domainDataProvider;
		}

		public IDomainDataProvider DataProvider
		{
			get
			{
				if (_domainDataProvider == null)
				{
					_domainDataProvider = DomainDataProviderManager.DomainDataProvider(); 
				}

				return _domainDataProvider;
			}	
		}

		public object[] QueryTSRecordPerformance(
			string modelCode,string itemCode,string moCode,
			string tsOperation,string tsOperator,
			int startDate,int endDate)
		{
			return null;
		}

		public object[] QueryTSRecordListByOperator(
			string modelCode,string itemCode,string moCode,
			string tsOperation,string tsOperator,
			int startDate,int endDate)
		{
			return null;
		}
	}
}
