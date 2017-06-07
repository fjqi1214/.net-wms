using System;
using System.Collections;
using BenQGuru.eMES.Domain.SolderPaste;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QuerySolderPasteFacade 的摘要说明。
	/// </summary>
	public class QuerySolderPasteFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QuerySolderPasteFacade( IDomainDataProvider domainDataProvider )
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

		#region 锡膏使用记录查询
		public object[] QuerySolderPastePro( string moCodes, string ssCodes, string spID, string spStatus, int inclusive,int exclusive)
		{
			string condition = string.Empty;
			if( moCodes!=null && moCodes.Length>0 )
			{
				condition += string.Format( " and mocode in ({0}) ", FormatHelper.ProcessQueryValues( moCodes ) ); 
			}

			if( ssCodes!=null && ssCodes.Length>0 )
			{
				condition += string.Format( " and linecode in ({0}) ", FormatHelper.ProcessQueryValues( ssCodes ) ); 
			}

			if( spID!=null && spID.Length>0 )
			{
				condition += string.Format( " and SOLDERPASTEID like '{0}%' ", spID ); 
			}

			if( spStatus!=null && spStatus.Length>0 )
			{
				condition += string.Format( " and STATUS = '{0}' ", spStatus ); 
			}

			string sql = string.Format( " select {0} from TBLSOLDERPASTEPRO where 1=1 {1} ", 
				DomainObjectUtility.GetDomainObjectFieldsString( typeof(SOLDERPASTEPRO)), condition );
#if DEBUG
			Log.Info(
				new PagerCondition(sql,"SOLDERPASTEID",inclusive,exclusive,true).SQLText);
		
#endif
			
			return this.DataProvider.CustomQuery(
				typeof(SOLDERPASTEPRO),
				new PagerCondition(sql
				,"SOLDERPASTEID",inclusive,exclusive,true));
		}

		public int QuerySolderPasteProCount( string moCodes, string ssCodes, string spID, string spStatus )
		{
			string condition = string.Empty;
			if( moCodes!=null && moCodes.Length>0 )
			{
				condition += string.Format( " and mocode in ({0}) ", FormatHelper.ProcessQueryValues( moCodes ) ); 
			}

			if( ssCodes!=null && ssCodes.Length>0 )
			{
				condition += string.Format( " and linecode in ({0}) ", FormatHelper.ProcessQueryValues( ssCodes ) ); 
			}

			if( spID!=null && spID.Length>0 )
			{
				condition += string.Format( " and SOLDERPASTEID like '{0}%' ", spID ); 
			}

			if( spStatus!=null && spStatus.Length>0 )
			{
				condition += string.Format( " and STATUS = '{0}' ", spStatus ); 
			}

			string sql = string.Format( " select {0} from TBLSOLDERPASTEPRO where 1=1 {1} ", " count(*) ", condition );
#if DEBUG
			Log.Info(new SQLCondition( sql ).SQLText);
#endif
			return this.DataProvider.GetCount(
				new SQLCondition(sql));
		}

		public object[] QuerySolderPastePro( string moCodes, string ssCodes, string spID, string spStatus,string beginDate,string endDate, int inclusive,int exclusive)
		{
			string condition = string.Empty;
			if( moCodes!=null && moCodes.Length>0 )
			{
				condition += string.Format( " and mocode in ({0}) ", FormatHelper.ProcessQueryValues( moCodes ) ); 
			}

			if( ssCodes!=null && ssCodes.Length>0 )
			{
				condition += string.Format( " and linecode in ({0}) ", FormatHelper.ProcessQueryValues( ssCodes ) ); 
			}

			if( spID!=null && spID.Length>0 )
			{
				condition += string.Format( " and SOLDERPASTEID like '{0}%' ", spID ); 
			}

			if( spStatus!=null && spStatus.Length>0 )
			{
				condition += string.Format( " and STATUS = '{0}' ", spStatus ); 
			}

			if(beginDate != null && beginDate != String.Empty
				&& endDate != null &&　endDate != String.Empty)
			{
				condition += FormatHelper.GetDateRangeSql("OPENDATE",int.Parse(beginDate),int.Parse(endDate));
			}

			string sql = string.Format( " select {0} from TBLSOLDERPASTEPRO where 1=1 {1} ", 
				DomainObjectUtility.GetDomainObjectFieldsString( typeof(SOLDERPASTEPRO)), condition );
#if DEBUG
			Log.Info(
				new PagerCondition(sql,"SOLDERPASTEID",inclusive,exclusive,true).SQLText);
		
#endif
			
			return this.DataProvider.CustomQuery(
				typeof(SOLDERPASTEPRO),
				new PagerCondition(sql
				,"SOLDERPASTEID",inclusive,exclusive,true));
		}

		public int QuerySolderPasteProCount( string moCodes, string ssCodes, string spID, string spStatus,string beginDate,string endDate )
		{
			string condition = string.Empty;
			if( moCodes!=null && moCodes.Length>0 )
			{
				condition += string.Format( " and mocode in ({0}) ", FormatHelper.ProcessQueryValues( moCodes ) ); 
			}

			if( ssCodes!=null && ssCodes.Length>0 )
			{
				condition += string.Format( " and linecode in ({0}) ", FormatHelper.ProcessQueryValues( ssCodes ) ); 
			}

			if( spID!=null && spID.Length>0 )
			{
				condition += string.Format( " and SOLDERPASTEID like '{0}%' ", spID ); 
			}

			if( spStatus!=null && spStatus.Length>0 )
			{
				condition += string.Format( " and STATUS = '{0}' ", spStatus ); 
			}

			if(beginDate != null && beginDate != String.Empty
				&& endDate != null &&　endDate != String.Empty)
			{
				condition += FormatHelper.GetDateRangeSql("OPENDATE",int.Parse(beginDate),int.Parse(endDate));
			}

			string sql = string.Format( " select {0} from TBLSOLDERPASTEPRO where 1=1 {1} ", " count(*) ", condition );
#if DEBUG
			Log.Info(new SQLCondition( sql ).SQLText);
#endif
			return this.DataProvider.GetCount(
				new SQLCondition(sql));
		}
		#endregion
	}
}
