using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.TS;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QueryChangedPartsFacade 的摘要说明。
	/// </summary>
	public class QueryTSChangedPartsFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QueryTSChangedPartsFacade( IDomainDataProvider domainDataProvider )
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

		public object[] QueryTSChangedParts(
			string modelCode,string itemCode,string moCode,
			string sn,string tsState,string tsResource,string tsOperator,
			int inclusive,int exclusive)
		{
			string modelCondition = "";
			if( modelCode != "" && modelCode != null )
			{
				modelCondition += string.Format(@" and modelcode = '{0}'",modelCode);
			}

			string itemCondition = "";
			if( itemCode != "" && itemCode != null )
			{
				itemCondition += string.Format(@" and itemcode = '{0}'",itemCode);
			}

			string moCondition = "";
			if( moCode != "" && moCode != null )
			{
				moCondition += string.Format(@" and mocode = '{0}'",moCode);
			}

			string snCondition = "";
			if( sn != "" && sn != null )
			{
				snCondition += string.Format(@" and rcard = '{0}'",sn.ToUpper());
			}

			string tsOperatorCondition = "";
			if( tsOperator != "" && tsOperator != null )
			{
				tsOperatorCondition += string.Format(@" and MUSER = '{0}'",tsOperator.ToUpper());;
			}

			string tsResourceCondition = "";
			if( tsResource != "" && tsResource != null )
			{
				tsResourceCondition += string.Format(@" and RRESCODE = '{0}'",tsResource);
			}
			
#if DEBUG
			Log.Info(
				new PagerCondition(
				string.Format(@"select distinct {0} from TBLTSITEM where 1=1 {1}{2}{3}{4}{5}{6}",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(TSItem)),
				modelCondition,itemCondition,moCondition,
				snCondition,tsOperatorCondition,tsResourceCondition),"rcard",
				inclusive,exclusive,true).SQLText);
#endif
			return this.DataProvider.CustomQuery(
				typeof(TSItem),
				new PagerCondition(
				string.Format(@"select distinct {0} from TBLTSITEM where 1=1 {1}{2}{3}{4}{5}{6}",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(TSItem)),
				modelCondition,itemCondition,moCondition,
				snCondition,tsOperatorCondition,tsResourceCondition),"rcard",
				inclusive,exclusive,true));
		}

		public int QueryTSChangedPartsCount(
			string modelCode,string itemCode,string moCode,
			string sn,string tsState,string tsResource,string tsOperator)
		{
			string modelCondition = "";
			if( modelCode != "" && modelCode != null )
			{
				modelCondition += string.Format(@" and modelcode = '{0}'",modelCode);
			}

			string itemCondition = "";
			if( itemCode != "" && itemCode != null )
			{
				itemCondition += string.Format(@" and itemcode = '{0}'",itemCode);
			}

			string moCondition = "";
			if( moCode != "" && moCode != null )
			{
				moCondition += string.Format(@" and mocode = '{0}'",moCode);
			}

			string snCondition = "";
			if( sn != "" && sn != null )
			{
				snCondition += string.Format(@" and rcard = '{0}'",sn.ToUpper());
			}

			string tsOperatorCondition = "";
			if( tsOperator != "" && tsOperator != null )
			{
				tsOperatorCondition += string.Format(@" and MUSER = '{0}'",tsOperator.ToUpper());;
			}

			string tsResourceCondition = "";
			if( tsResource != "" && tsResource != null )
			{
				tsResourceCondition += string.Format(@" and RRESCODE = '{0}'",tsResource);
			}
			
#if DEBUG
			Log.Info(
				new SQLCondition(
				string.Format(@"select distinct {0} from TBLTSITEM where 1=1 {1}{2}{3}{4}{5}{6}",
				"count(*)",
				modelCondition,itemCondition,moCondition,
				snCondition,tsOperatorCondition,tsResourceCondition)).SQLText);
#endif
			return this.DataProvider.GetCount(
				new SQLCondition(
				string.Format(@"select distinct {0} from TBLTSITEM where 1=1 {1}{2}{3}{4}{5}{6}",
				"count(*)",
				modelCondition,itemCondition,moCondition,
				snCondition,tsOperatorCondition,tsResourceCondition)));
		}

		
		public object[] QueryTSChangedPartsSMT(
			string modelCode,string itemCode,string moCode,
			string sn,string tsState,string tsResource,string tsOperator,
			int inclusive,int exclusive)
		{
			string modelCondition = "";
			if( modelCode != "" && modelCode != null )
			{
				modelCondition += string.Format(@" and modelcode = '{0}'",modelCode);
			}

			string itemCondition = "";
			if( itemCode != "" && itemCode != null )
			{
				itemCondition += string.Format(@" and itemcode = '{0}'",itemCode);
			}

			string moCondition = "";
			if( moCode != "" && moCode != null )
			{
				moCondition += string.Format(@" and mocode = '{0}'",moCode);
			}

			string snCondition = "";
			if( sn != "" && sn != null )
			{
				snCondition += string.Format(@" and rcard = '{0}'",sn.ToUpper());
			}

			string tsOperatorCondition = "";
			if( tsOperator != "" && tsOperator != null )
			{
				tsOperatorCondition += string.Format(@" and MUSER = '{0}'",tsOperator.ToUpper());;
			}

			string tsResourceCondition = "";
			if( tsResource != "" && tsResource != null )
			{
				tsResourceCondition += string.Format(@" and RRESCODE = '{0}'",tsResource);
			}
			
#if DEBUG
			Log.Info(
				new PagerCondition(
				string.Format(@"select distinct {0} from TBLTSSMTITEM where 1=1 {1}{2}{3}{4}{5}{6}",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(TSSMTItem)),
				modelCondition,itemCondition,moCondition,
				snCondition,tsOperatorCondition,tsResourceCondition),"rcard",
				inclusive,exclusive,true).SQLText);
#endif
			return this.DataProvider.CustomQuery(
				typeof(TSSMTItem),
				new PagerCondition(
				string.Format(@"select distinct {0} from TBLTSSMTITEM where 1=1 {1}{2}{3}{4}{5}{6}",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(TSSMTItem)),
				modelCondition,itemCondition,moCondition,
				snCondition,tsOperatorCondition,tsResourceCondition),"rcard",
				inclusive,exclusive,true));
		}

		public int QueryTSChangedPartsSMTCount(
			string modelCode,string itemCode,string moCode,
			string sn,string tsState,string tsResource,string tsOperator)
		{
			string modelCondition = "";
			if( modelCode != "" && modelCode != null )
			{
				modelCondition += string.Format(@" and modelcode = '{0}'",modelCode);
			}

			string itemCondition = "";
			if( itemCode != "" && itemCode != null )
			{
				itemCondition += string.Format(@" and itemcode = '{0}'",itemCode);
			}

			string moCondition = "";
			if( moCode != "" && moCode != null )
			{
				moCondition += string.Format(@" and mocode = '{0}'",moCode);
			}

			string snCondition = "";
			if( sn != "" && sn != null )
			{
				snCondition += string.Format(@" and rcard = '{0}'",sn.ToUpper());
			}

			string tsOperatorCondition = "";
			if( tsOperator != "" && tsOperator != null )
			{
				tsOperatorCondition += string.Format(@" and MUSER = '{0}'",tsOperator.ToUpper());;
			}

			string tsResourceCondition = "";
			if( tsResource != "" && tsResource != null )
			{
				tsResourceCondition += string.Format(@" and RRESCODE = '{0}'",tsResource);
			}
			
#if DEBUG
			Log.Info(
				new SQLCondition(
				string.Format(@"select {0} from TBLTSSMTITEM where 1=1 {1}{2}{3}{4}{5}{6}",
				"count(*)",
				modelCondition,itemCondition,moCondition,
				snCondition,tsOperatorCondition,tsResourceCondition)).SQLText);
#endif
			return this.DataProvider.GetCount(
				new SQLCondition(
				string.Format(@"select {0} from TBLTSSMTITEM where 1=1 {1}{2}{3}{4}{5}{6}",
				"count(*)",
				modelCondition,itemCondition,moCondition,
				snCondition,tsOperatorCondition,tsResourceCondition)));
		}
	}
}
