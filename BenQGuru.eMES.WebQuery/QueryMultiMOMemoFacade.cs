using System;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QueryMultiMOMemoFacade 的摘要说明。
	/// </summary>
	public class QueryMultiMOMemoFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;
		public QueryMultiMOMemoFacade( IDomainDataProvider domainDataProvider )
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
			
		public object[] QueryMultiMOMemo(
			string itemcodes,string mos,string startRCard, string endRCARD,
			string startMemo,string endMemo,
			int inclusive,int exclusive)
		{
			string sql = string.Format(@"select {0} from TBLONWIP where action = '{1}' and length(eattribute1)>0 ",
				DomainObjectUtility.GetDomainObjectFieldsString( typeof(QDOMultiMOMemo) ),
				ActionType.DataCollectAction_GoMO);

			if( itemcodes!=null && itemcodes.Length > 0 )
			{
				sql += string.Format(" and itemcode in ({0}) ",FormatHelper.ProcessQueryValues(itemcodes) );
			}

			if( mos!=null && mos.Length > 0 )
			{
				sql += string.Format(" and MoCode in ({0}) ",FormatHelper.ProcessQueryValues(mos) );
			}

			sql += FormatHelper.GetRCardRangeSql("RCARD",startRCard,endRCARD) ;

			sql += FormatHelper.GetCodeRangeSql( "EAttribute1" , startMemo, endMemo );

			PagerCondition pageCondition = new PagerCondition(sql,"RCARD",inclusive,exclusive) ;

#if DEBUG
			Log.Info(
				pageCondition.SQLText);
#endif
			return this.DataProvider.CustomQuery(
				typeof(QDOMultiMOMemo),
				pageCondition);

		}

		public int QueryMultiMOMemoCount(
			string itemcodes,string mos,string startRCard, string endRCARD,
			string startMemo,string endMemo)
		{
			string sql = string.Format(@"select count( * ) from TBLONWIP where action = '{0}' and length(eattribute1)>0 ",
				ActionType.DataCollectAction_GoMO);

			if( itemcodes!=null && itemcodes.Length > 0 )
			{
				sql += string.Format(" and itemcode in ({0}) ",FormatHelper.ProcessQueryValues(itemcodes) );
			}

			if( mos!=null && mos.Length > 0 )
			{
				sql += string.Format(" and MoCode in ({0}) ",FormatHelper.ProcessQueryValues(mos) );
			}

			sql += FormatHelper.GetRCardRangeSql("RCARD",startRCard,endRCARD) ;

			sql += FormatHelper.GetCodeRangeSql( "EAttribute1" , startMemo, endMemo );

			SQLCondition sqlCondition = new SQLCondition(sql);

#if DEBUG
			Log.Info( sqlCondition.SQLText );
#endif
			return this.DataProvider.GetCount( sqlCondition );
		}
	}
}
