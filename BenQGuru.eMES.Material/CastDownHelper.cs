using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.DomainDataProvider; 

namespace BenQGuru.eMES.Material
{
	/// <summary>
	/// CastDownHelper 的摘要说明。
	/// </summary>
	public class CastDownHelper:MarshalByRefObject
	{
		private  IDomainDataProvider _domainDataProvider= null;
		public CastDownHelper(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}

		public CastDownHelper()
		{
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

		#region Help Method Single
		public void GetAllRCard(ref ArrayList array,string tcard)
		{
			if(RCardCount(tcard)>0)
			{
				object[] product = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIP), new SQLCondition( string.Format(" select distinct tcard as rcard from tblonwipcardtrans where rcard = '{0}'  and rcard<>tcard ",tcard)));
				if(product!=null)
				{
					for( int i=0; i<product.Length ; i++ )
					{
						array.Add ( (product[i] as BenQGuru.eMES.Domain.DataCollect.OnWIP).RunningCard );
						GetAllRCard(ref array,(product[i] as BenQGuru.eMES.Domain.DataCollect.OnWIP).RunningCard);
					}
				}
				else
				{
					return;
				}
			}

			return;
		}

		public void GetAllRCardByMo(ref ArrayList array,string tcard,string moCode)
		{
			if(RCardCountByMo(tcard,moCode)>0)
			{
				object[] product = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIP), new SQLCondition( string.Format(" select distinct tcard as rcard from tblonwipcardtrans where rcard = '{0}'  and rcard<>tcard  and mocode='{1}'",tcard,moCode)));
				if(product!=null)
				{
					for( int i=0; i<product.Length ; i++ )
					{
						array.Add ( (product[i] as BenQGuru.eMES.Domain.DataCollect.OnWIP).RunningCard );
						GetAllRCardByMo(ref array,(product[i] as BenQGuru.eMES.Domain.DataCollect.OnWIP).RunningCard,moCode);
					}
				}
				else
				{
					return;
				}
			}

			return;
		}
		
		private int RCardCount( string rcard )
		{
			int count = 0;
			count = this.DataProvider.GetCount( new SQLCondition(string.Format(" select count (tcard) from tblonwipcardtrans where rcard = '{0}' and rcard<>tcard ",rcard)));
			return count ;            
		}

		private int RCardCountByMo( string rcard ,string moCode)
		{
			int count = 0;
			count = this.DataProvider.GetCount( new SQLCondition(string.Format(" select count (tcard) from tblonwipcardtrans where rcard = '{0}' and rcard<>tcard and mocode='{1}'",rcard,moCode)));
			return count ;            
		}
		#endregion

		#region Help Method Multi
		public void BuildProcessRcardCondition(ref ArrayList sqlParam,string startRcard, string endRCard)
		{
			string CountSQL =string.Format("select count (tcard) from tblonwipcardtrans where rcard between '{0}' and '{1}' and rcard<>tcard ",startRcard,endRCard);
			string buildSQL = string.Format(" select tcard from tblonwipcardtrans where rcard between '{0}' and '{1}' and rcard<>tcard ",startRcard,endRCard);
			this.BuildString(ref sqlParam, CountSQL, buildSQL); 	
		}

		private void BuildString(ref ArrayList sqlParam, string countsql, string buildsql )
		{
			if(GetRcardCount(countsql)>0)
			{
				sqlParam.Add(buildsql);
				countsql = string.Format(" select count(tcard) from tblonwipcardtrans where rcard in ({0}) and rcard<>tcard ",buildsql);
				buildsql = string.Format(" select tcard from tblonwipcardtrans where rcard in ( {0} ) and rcard<>tcard ",buildsql);
				BuildString(ref sqlParam, countsql, buildsql);
			}
			
			return;
		}

		private int GetRcardCount(string sql)
		{
			int count = 0;
			count = this.DataProvider.GetCount( new SQLCondition(sql));
			return count ; 	
		}
		#endregion
	}
}
