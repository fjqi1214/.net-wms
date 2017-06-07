using System;

using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QueryItemConfigrationFacade 的摘要说明。
	/// </summary>
	public class QueryItemConfigrationFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;
		public QueryItemConfigrationFacade( IDomainDataProvider domainDataProvider )
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
			
		public object[] QueryItemConfigration(
			string itemcodes,string mos,string startRCard, string endRCARD,
			string itemConfig,string ConfigCategory,string ConfigCode,int startDate,int enddate,
			int inclusive,int exclusive)
		{
			string sql = string.Format(@"select {0} from TBLONWIPCFGCOLLECT where 1=1 ",DomainObjectUtility.GetDomainObjectFieldsString( typeof(QDOItemConfigration) ));

			if( itemcodes!=null && itemcodes.Length > 0 )
			{
				sql += string.Format(" and itemcode in ({0}) ",FormatHelper.ProcessQueryValues(itemcodes) );
			}

			if( mos!=null && mos.Length > 0 )
			{
				sql += string.Format(" and MoCode in ({0}) ",FormatHelper.ProcessQueryValues(mos) );
			}

			sql += FormatHelper.GetRCardRangeSql("RCARD",startRCard,endRCARD) ;

			if( itemConfig!=null && itemConfig.Length>0 )
			{
				sql += string.Format(" and ItemConfig like '{0}%' ",itemConfig);
			}

			if( ConfigCategory!=null && ConfigCategory.Length>0 )
			{
				sql += string.Format(" and CatergoryCode like '{0}%' ",ConfigCategory);
			}

			if( ConfigCode!=null && ConfigCode.Length>0 )
			{
				sql += string.Format(" and CheckItemCode like '{0}%' ",ConfigCode);
			}

			sql += FormatHelper.GetDateRangeSql( "MDATE" , startDate, enddate );

			PagerCondition pageCondition = new PagerCondition(sql,"RCARD",inclusive,exclusive) ;

#if DEBUG
			Log.Info(
				pageCondition.SQLText);
#endif
			return this.DataProvider.CustomQuery(
				typeof(QDOItemConfigration),
				pageCondition);

		}

		public int QueryItemConfigrationCount(
			string itemcodes,string mos,string startRCard, string endRCARD,
			string itemConfig,string ConfigCategory,string ConfigCode,int startDate,int enddate)
		{
			string sql = string.Format(@"select count( {0} ) from TBLONWIPCFGCOLLECT where 1=1 ","PKID");

			if( itemcodes!=null && itemcodes.Length > 0 )
			{
				sql += string.Format(" and itemcode in ({0}) ",FormatHelper.ProcessQueryValues(itemcodes) );
			}

			if( mos!=null && mos.Length > 0 )
			{
				sql += string.Format(" and MoCode in ({0}) ",FormatHelper.ProcessQueryValues(mos) );
			}

			sql += FormatHelper.GetRCardRangeSql("RCARD",startRCard,endRCARD) ;

			if( itemConfig!=null && itemConfig.Length>0 )
			{
				sql += string.Format(" and ItemConfig like '{0}%' ",itemConfig);
			}

			if( ConfigCategory!=null && ConfigCategory.Length>0 )
			{
				sql += string.Format(" and CatergoryCode like '{0}%' ",ConfigCategory);
			}

			if( ConfigCode!=null && ConfigCode.Length>0 )
			{
				sql += string.Format(" and CheckItemCode like '{0}%' ",ConfigCode);
			}

			sql += FormatHelper.GetDateRangeSql( "MDATE" , startDate, enddate );

			SQLCondition sqlCondition = new SQLCondition(sql);

#if DEBUG
			Log.Info( sqlCondition.SQLText );
#endif
			return this.DataProvider.GetCount( sqlCondition );
		}
	}
}
