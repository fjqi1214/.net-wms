using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QueryTryItemFacade 的摘要说明。
	/// </summary>
	public class QueryTryItemFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QueryTryItemFacade( IDomainDataProvider domainDataProvider )
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

		public object[] QueryTryItem(
			string tryItem,string moCodes,
			int inclusive,int exclusive)
		{
			string tryItemCondition = "";
			if( tryItem != "" && tryItem != null )
			{
				tryItemCondition = string.Format(" and TBLMINNO.IsTry = 'Y' and upper(TBLMINNO.TryItemCode) like '{0}%'",tryItem.ToUpper());
			}

			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(
					@" and a.mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
			}

#if DEBUG
			Log.Info(
				new PagerCondition(
				string.Format(@"select distinct {0} from TBLMINNO,TBLONWIPITEM a where 
				a.MCARD = TBLMINNO.mitempackedno and a.MCARDTYPE = '1' {1}{2}",
				"TBLMINNO.TryItemCode,a.MCARD,a.ItemCode,a.MoCode,a.RCARD,a.OPCODE,a.SSCODE,a.RESCODE,a.RCARD as snstate,a.MUSER,a.MDATE,a.mtime",
				tryItemCondition,moCondition),
				"TryItemCode",
				inclusive,exclusive,true).SQLText);
#endif
			object[] objs = this.DataProvider.CustomQuery(
				typeof(QDOTryItem),
				new PagerCondition(
				string.Format(@"select distinct {0} from TBLMINNO,TBLONWIPITEM a where 
				a.MCARD = TBLMINNO.mitempackedno and a.MCARDTYPE = '1' {1}{2}",
				"TBLMINNO.TryItemCode,a.MCARD,a.RCARD,a.ItemCode,a.MoCode,a.OPCODE,a.SSCODE,a.RESCODE,a.RCARD as snstate,a.MUSER,a.MDATE,a.mtime",
				tryItemCondition,moCondition),
				"TryItemCode",
				inclusive,exclusive,true));

			if( objs != null )	//look for sn's state
			{
				foreach(QDOTryItem obj in objs)
				{
					object[] sn = this.DataProvider.CustomQuery(typeof(Simulation),
						new SQLCondition(
						string.Format(@"select ProductStatus from TBLSIMULATION where RCARD = '{0}'",obj.SNState)));
					if( sn != null )
					{
						obj.SNState = (sn[0] as Simulation).ProductStatus;
					}
					else
					{
						obj.SNState = ProductStatus.OffLine;
					}
				}
			}

			return objs;
		}

		public int QueryTryItemCount(
			string tryItem,string moCodes)
		{
			string tryItemCondition = "";
			if( tryItem != "" && tryItem != null )
			{
				tryItemCondition = string.Format(" and TBLMINNO.IsTry = 'Y' and upper(TBLMINNO.TryItemCode) like '{0}%'",tryItem.ToUpper());
			}

			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(
					@" and a.mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
			}

#if DEBUG
			Log.Info(
				new SQLCondition(
				string.Format(@"select {0} from TBLMINNO,TBLONWIPITEM a where 
				a.MCARD = TBLMINNO.mitempackedno and a.MCARDTYPE = '1' {1}{2}",
				"count(TBLMINNO.TryItemCode)",
				tryItemCondition,moCondition)).SQLText);
#endif
			return this.DataProvider.GetCount(
				new SQLCondition(
				string.Format(@"select {0} from TBLMINNO,TBLONWIPITEM a where 
				a.MCARD = TBLMINNO.mitempackedno and a.MCARDTYPE = '1' {1}{2}",
				"count(TBLMINNO.TryItemCode)",
				tryItemCondition,moCondition)));
		}
	}
}
