using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QueryIDMergeFacade 的摘要说明。
	/// </summary>
	public class QueryCardTransferFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QueryCardTransferFacade( IDomainDataProvider domainDataProvider )
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

		public object[] QueryCardTransfer(
			string itemCodes,string moCodes,
			string startSn,string endSn,string startDate,string endDate,
			int inclusive,int exclusive)
		{
			string itemCondition = "";
			if( itemCodes != "" && itemCodes != null )
			{
				itemCondition = string.Format(
					@" and itemcode in ({0})",FormatHelper.ProcessQueryValues( itemCodes ) );
			}

			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(
					@" and mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
			}
			string dateCondition = "";
			if(startDate != null && startDate != String.Empty
				&& endDate != null && endDate != String.Empty)
			{
				dateCondition = FormatHelper.GetDateRangeSql("MDATE",int.Parse(startDate),int.Parse(endDate));
			}

			string SnCondition = string.Empty;
			string SnConditionT = string.Empty;
			SnCondition  = FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());
			SnConditionT = FormatHelper.GetRCardRangeSql("tcard",startSn.ToUpper(),endSn.ToUpper());
			//modifyed by jessie lee,2005/9/16
			//传入的startSn 和 endSn即要作为转换前序列号的查询条件，又要作为转换后序列号的查询条件，所以用union联合
			string sql = SnCondition.Trim().Length != 0?
				string.Format(@"select {0} from 
										(select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}{5}
										 union
										 select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}{4}{5}
										)" 
				,
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPCardTransfer)),
				itemCondition,
				moCondition,
				SnCondition,
				SnConditionT,
				dateCondition
				)
				:
				string.Format(@"select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPCardTransfer)),
				itemCondition,
				moCondition,
				dateCondition
				);


#if DEBUG
			Log.Info(
				new PagerCondition(
				sql,
				"mocode,itemcode,TCARD",
				inclusive,exclusive,true).SQLText);

#endif
			return this.DataProvider.CustomQuery(
				typeof(OnWIPCardTransfer),
				new PagerCondition(
				sql,
				"mocode,itemcode,TCARD",
				inclusive,exclusive,true));
		}

		public int QueryCardTransferCount(
			string itemCodes,string moCodes,
			string startSn,string endSn,string startDate,string endDate)
		{
			string itemCondition = "";
			if( itemCodes != "" && itemCodes != null )
			{
				itemCondition = string.Format(
					@" and itemcode in ({0})",FormatHelper.ProcessQueryValues( itemCodes ) );
			}

			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(
					@" and mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
			}

			string dateCondition = "";
			if(startDate != null && startDate != String.Empty
				&& endDate != null && endDate != String.Empty)
			{
				dateCondition = FormatHelper.GetDateRangeSql("MDATE",int.Parse(startDate),int.Parse(endDate));
			}

			string SnCondition = string.Empty;
			string SnConditionT = string.Empty;

			SnCondition  = FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());
			SnConditionT = FormatHelper.GetRCardRangeSql("tcard",startSn.ToUpper(),endSn.ToUpper());

			string sql = SnCondition.Trim().Length != 0?
				string.Format(@"select {0} from 
										(select RCARD,MOCODE,RCARDSEQ from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}{5}
										 union
										 select RCARD,MOCODE,RCARDSEQ from TBLONWIPCARDTRANS where 1=1 {1}{2}{4}{5}
										)",
				"count(*)",
				itemCondition,
				moCondition,
				SnCondition,
				SnConditionT,
				dateCondition
				):
				string.Format(@"select {0} from 
										(select RCARD,MOCODE,RCARDSEQ from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}
										)",
				"count(*)",
				itemCondition,
				moCondition
				,dateCondition
				);

#if DEBUG
			Log.Info(
				new SQLCondition(sql).SQLText);

#endif
			return this.DataProvider.GetCount(
				new SQLCondition(sql));

			//			Log.Info(
			//				new SQLCondition(
			//				string.Format(@"select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}{4}",
			//				"count(mocode)",
			//				itemCondition,moCondition,startSnCondition,endSnCondition)).SQLText);
			//
			//			return this.DataProvider.GetCount(
			//				new SQLCondition(
			//				string.Format(@"select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}{4}",
			//				"count(mocode)",
			//				itemCondition,moCondition,startSnCondition,endSnCondition)));
		}


		public object[] QueryCardTransfer(
			string itemCodes,string moCodes,
			string startSn,string endSn,
			int inclusive,int exclusive)
		{
			string itemCondition = "";
			if( itemCodes != "" && itemCodes != null )
			{
				itemCondition = string.Format(
					@" and itemcode in ({0})",FormatHelper.ProcessQueryValues( itemCodes ) );
			}

			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(
					@" and mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
			}

			string SnCondition = string.Empty;
			string SnConditionT = string.Empty;
			SnCondition  = FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());
			SnConditionT = FormatHelper.GetRCardRangeSql("tcard",startSn.ToUpper(),endSn.ToUpper());
			//modifyed by jessie lee,2005/9/16
			//传入的startSn 和 endSn即要作为转换前序列号的查询条件，又要作为转换后序列号的查询条件，所以用union联合
			string sql = SnCondition.Trim().Length != 0?
				string.Format(@"select {0} from 
										(select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}
										 union
										 select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}{4}
										)" 
				,
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPCardTransfer)),
				itemCondition,
				moCondition,
				SnCondition,
				SnConditionT
				)
				:
				string.Format(@"select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPCardTransfer)),
				itemCondition,
				moCondition
				);


#if DEBUG
			Log.Info(
				new PagerCondition(
				sql,
				"mocode,itemcode,TCARD",
				inclusive,exclusive,true).SQLText);

#endif
			return this.DataProvider.CustomQuery(
				typeof(OnWIPCardTransfer),
				new PagerCondition(
				sql,
				"mocode,itemcode,TCARD",
				inclusive,exclusive,true));
		}

		public int QueryCardTransferCount(
			string itemCodes,string moCodes,
			string startSn,string endSn)
		{
			string itemCondition = "";
			if( itemCodes != "" && itemCodes != null )
			{
				itemCondition = string.Format(
					@" and itemcode in ({0})",FormatHelper.ProcessQueryValues( itemCodes ) );
			}

			string moCondition = "";
			if( moCodes != "" && moCodes != null )
			{
				moCondition = string.Format(
					@" and mocode in ({0})",FormatHelper.ProcessQueryValues( moCodes ) );
			}

			string SnCondition = string.Empty;
			string SnConditionT = string.Empty;

			SnCondition  = FormatHelper.GetRCardRangeSql("rcard",startSn.ToUpper(),endSn.ToUpper());
			SnConditionT = FormatHelper.GetRCardRangeSql("tcard",startSn.ToUpper(),endSn.ToUpper());

			string sql = SnCondition.Trim().Length != 0?
				string.Format(@"select {0} from 
										(select RCARD,MOCODE,RCARDSEQ from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}
										 union
										 select RCARD,MOCODE,RCARDSEQ from TBLONWIPCARDTRANS where 1=1 {1}{2}{4}
										)",
				"count(*)",
				itemCondition,
				moCondition,
				SnCondition,
				SnConditionT
				):
				string.Format(@"select {0} from 
										(select RCARD,MOCODE,RCARDSEQ from TBLONWIPCARDTRANS where 1=1 {1}{2}
										)",
				"count(*)",
				itemCondition,
				moCondition
				);

#if DEBUG
			Log.Info(
				new SQLCondition(sql).SQLText);

#endif
			return this.DataProvider.GetCount(
				new SQLCondition(sql));

//			Log.Info(
//				new SQLCondition(
//				string.Format(@"select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}{4}",
//				"count(mocode)",
//				itemCondition,moCondition,startSnCondition,endSnCondition)).SQLText);
//
//			return this.DataProvider.GetCount(
//				new SQLCondition(
//				string.Format(@"select {0} from TBLONWIPCARDTRANS where 1=1 {1}{2}{3}{4}",
//				"count(mocode)",
//				itemCondition,moCondition,startSnCondition,endSnCondition)));
		}
	}
}
