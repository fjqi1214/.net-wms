using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.SMT;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QuerySMTComponentLoadingFacade 的摘要说明。
	/// </summary>
	public class QuerySMTComponentLoadingFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QuerySMTComponentLoadingFacade( IDomainDataProvider domainDataProvider )
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

		public object[] QuerySMTComponentLoading(
			string moCodes,string resourceCodes,
			int inclusive,int exclusive)
		{
			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(@" and mocode in ({0})",
					FormatHelper.ProcessQueryValues(moCodes));
			}

			string resourceCondition = "";
			if( resourceCodes != "" && resourceCodes != null )
			{
				resourceCondition = string.Format(@" and rescode in ({0})",
					FormatHelper.ProcessQueryValues( resourceCodes ) );
			}
#if DEBUG
			Log.Info(
				new PagerCondition(
				string.Format(@"select distinct {0} from TBLSMTRESBOM where 1=1 {1}{2}",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(SMTResourceBOM)),moCondition,resourceCondition),"mocode,rescode",
				inclusive,exclusive,true).SQLText);
#endif
			return this.DataProvider.CustomQuery(
				typeof(SMTResourceBOM),
				new PagerCondition(
				string.Format(@"select distinct {0} from TBLSMTRESBOM where 1=1 {1}{2}",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(SMTResourceBOM)),moCondition,resourceCondition),"mocode,rescode",
				inclusive,exclusive,true));

		}

		public int QuerySMTComponentLoadingCount(
			string moCodes,string resourceCodes)
		{
			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(@" and mocode in ({0})",
					FormatHelper.ProcessQueryValues(moCodes));
			}

			string resourceCondition = "";
			if( resourceCodes != "" && resourceCodes != null )
			{
				resourceCondition = string.Format(@" and rescode in ({0})",
					FormatHelper.ProcessQueryValues( resourceCodes ) );
			}
#if DEBUG
			Log.Info(
				new SQLCondition(
				string.Format(@"select {0} from TBLSMTRESBOM where 1=1 {1}{2}",
				"count(mocode)",moCondition,resourceCondition)).SQLText);
#endif
			return this.DataProvider.GetCount(
				new SQLCondition(
				string.Format(@"select {0} from TBLSMTRESBOM where 1=1 {1}{2}",
				"count(mocode)",moCondition,resourceCondition)));
		}
	}
}
