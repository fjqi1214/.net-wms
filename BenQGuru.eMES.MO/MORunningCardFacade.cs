#region System
using System;
using System.Runtime.Remoting;  
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;
#endregion
   
namespace BenQGuru.eMES.MOModel
{
	public class MORunningCardFacade:MarshalByRefObject
	{
		private static readonly log4net.ILog _log = BenQGuru.eMES.Common.Log.GetLogger(typeof(MORunningCardFacade));
		private  IDomainDataProvider _domainDataProvider= null;
		private FacadeHelper _helper = null;

		public MORunningCardFacade(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._helper = new FacadeHelper( DataProvider );
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}

		public MORunningCardFacade()
		{
			this._helper = new FacadeHelper( DataProvider );
		}

		protected IDomainDataProvider DataProvider
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

		#region MORunningCard
		/// <summary>
		/// 工单流程卡号
		/// </summary>
		public MORunningCard CreateNewMORunningCard()
		{
			return new MORunningCard();
		}

		public void AddMORunningCard( MORunningCard mORunningCard)
		{
			mORunningCard.Sequence = this.GetUniqueMORunningCardSequence();

			//this._helper.AddDomainObject( mORunningCard );
			this.DataProvider.Insert( mORunningCard );
		}

		public void UpdateMORunningCard(MORunningCard mORunningCard)
		{
			this._helper.UpdateDomainObject( mORunningCard );
		}

		public void DeleteMORunningCard(MORunningCard mORunningCard)
		{
			this._helper.DeleteDomainObject( mORunningCard );
		}

		public void DeleteMORunningCard(MORunningCard[] mORunningCard)
		{
			this._helper.DeleteDomainObject( mORunningCard );
		}

		public object GetMORunningCard( string mOCode, decimal sequence )
		{
			return this.DataProvider.CustomSearch(typeof(MORunningCard), new object[]{ mOCode, sequence });
		}

		/// <summary>
		/// ** 功能描述:	RunningCard是否已被工单使用
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-06-03
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="runningCard"></param>
		/// <returns></returns>
		public bool IsRunningCardUsed( string runningCard )
		{
//			if ( this.DataProvider.GetCount( 
//				new SQLParamCondition( "select count(*) from TBLMORCARD where MORCARDSTART <= $MORCARDSTART and MORCARDEND >= $MORCARDEND",
//				new SQLParameter[] {
//									   new SQLParameter("MORCARDSTART", typeof(string), runningCard),
//									   new SQLParameter("MORCARDEND", typeof(string), runningCard)} )) > 0)
//			{
//				return true;
//			}
			if ( this.DataProvider.GetCount( 
				new SQLParamCondition( "select count(*) from TBLMORCARD where MORCARDSTART = $MORCARDSTART ",
				new SQLParameter[] {
									   new SQLParameter("MORCARDSTART", typeof(string), runningCard)
								   } )) > 0)
			{
				return true;
			}

			return false;
		}

		public object GetMORunningCard( string runningCard, string moCode )
		{
			 object[] objs = this.DataProvider.CustomQuery( typeof(MORunningCard),
								new SQLParamCondition( string.Format("select {0} from TBLMORCARD where MOCODE=$MOCODE and MORCARDSTART <= $MORCARDSTART and MORCARDEND >= $MORCARDEND", 
																		DomainObjectUtility.GetDomainObjectFieldsString(typeof(MORunningCard)) ),
								new SQLParameter[] {
													new SQLParameter("MOCODE", typeof(string), moCode),
													new SQLParameter("MORCARDSTART", typeof(string), runningCard),
													new SQLParameter("MORCARDEND", typeof(string), runningCard)} ));

			if ( objs == null )
			{
				return null;
			}

			return objs[0];
		}

		/// <summary>
		/// ** 功能描述:	查询MORunningCard的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-06-03 8:54:00
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <returns> MORunningCard的总记录数</returns>
		public int QueryMORunningCardCount( string mOCode, decimal sequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMORCARD where 1=1 and MOCODE like '{0}%'  and SEQ like '{1}%' " , mOCode, sequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询MORunningCard
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-06-03 8:54:00
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> MORunningCard数组</returns>
		public object[] QueryMORunningCard( string mOCode, decimal sequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(MORunningCard), new PagerCondition(string.Format("select {0} from TBLMORCARD where 1=1 and MOCODE like '{1}%'  and SEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MORunningCard)) , mOCode, sequence), "MOCODE,SEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的MORunningCard
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-06-03 8:54:00
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>MORunningCard的总记录数</returns>
		public object[] GetAllMORunningCard()
		{
			return this.DataProvider.CustomQuery(typeof(MORunningCard), new SQLCondition(string.Format("select {0} from TBLMORCARD order by MOCODE,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MORunningCard)))));
		}

		/// <summary>
		/// ** 功能描述:	生成MORunningCard的唯一Sequence
		///						取最大的Sequence加1
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-06-03 8:54:00
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns></returns>
		public decimal GetUniqueMORunningCardSequence()
		{
//			object[] objs = this.DataProvider.CustomQuery( 
//													typeof(MORunningCard), 
//													new PagerCondition(
//																		string.Format("select {0} from TBLMORCARD", 
//																		DomainObjectUtility.GetDomainObjectFieldsString(typeof(MORunningCard)) ), 
//																		"SEQ desc", 1, 1) );
//
//			if ( objs == null || objs.Length < 1 )
//			{
//				return 0;
//			}
//
//			return ((MORunningCard)objs[0]).Sequence + 1;
			//return FormatHelper.GetUniqueID("","","");
			//MMDDHHMMSS
			//((DD*24)+HH)*60+MM)*60+SS   共7位 最大为2678400   加3位随机数
			DateTime dt= DateTime.Now ;
			int n=(((dt.Day *24)+dt.Hour )*60+dt.Minute)*60+dt.Second ;
			Random r=new Random();
			n=n*r.Next(999);
			return n;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public string GetMOcodeByRcard(string rcard)
        {
           // string sql = "Select mocode from TBLMO2RCARDLINK  where rcard='" + rcard + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(MO2RCARDLINK), new SQLCondition(string.Format("Select {0} from TBLMO2RCARDLINK  where rcard = '{1}'  ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2RCARDLINK)), rcard.Trim().ToUpper())));
           
            if (objs == null)
            {
                return null;
            }
            MO2RCARDLINK rcardDetail = (MO2RCARDLINK)objs[0];
            return rcardDetail.MOCode;
        }

		#endregion

		#region MORunningCardRange
		/// <summary>
		/// 工单流程卡号
		/// </summary>
		public MORunningCardRange CreateNewMORunningCardRange()
		{
			return new MORunningCardRange();
		}

		public void AddMORunningCardRange( MORunningCardRange mORunningCardRange)
		{
			string strSql = "SELECT MAX(SEQ) SEQ FROM tblMORCardRange WHERE MOCode='" + mORunningCardRange.MOCode + "'";
			object[] objTmp = this.DataProvider.CustomQuery(typeof(MORunningCardRange), new SQLCondition(strSql));
			decimal deSeq = 1;
			if (objTmp != null && objTmp.Length > 0)
			{
				deSeq = ((MORunningCardRange)objTmp[0]).Sequence + 1;
			}
			mORunningCardRange.Sequence = deSeq;
			this._helper.AddDomainObject( mORunningCardRange );
		}

		public void UpdateMORunningCardRange(MORunningCardRange mORunningCardRange)
		{
			this._helper.UpdateDomainObject( mORunningCardRange );
		}

		public void DeleteMORunningCardRange(MORunningCardRange mORunningCardRange)
		{
			this._helper.DeleteDomainObject( mORunningCardRange );
		}

		public void DeleteMORunningCardRange(MORunningCardRange[] mORunningCardRange)
		{
			this._helper.DeleteDomainObject( mORunningCardRange );
		}

		public object GetMORunningCardRange( decimal sequence, string mOCode )
		{
			return this.DataProvider.CustomSearch(typeof(MORunningCardRange), new object[]{ sequence, mOCode });
		}

		/// <summary>
		/// ** 功能描述:	查询MORunningCardRange的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-12-30 10:51:25
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <returns> MORunningCardRange的总记录数</returns>
		public int QueryMORunningCardRangeCount( decimal sequence, string mOCode)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMORCARDRANGE where 1=1 and SEQ like '{0}%'  and MOCODE like '{1}%' " , sequence, mOCode)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询MORunningCardRange
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-12-30 10:51:25
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <param name="mOCode">MOCode，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> MORunningCardRange数组</returns>
		public object[] QueryMORunningCardRange( decimal sequence, string mOCode, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(MORunningCardRange), new PagerCondition(string.Format("select {0} from TBLMORCARDRANGE where 1=1 and SEQ like '{1}%'  and MOCODE like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MORunningCardRange)) , sequence, mOCode), "SEQ,MOCODE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的MORunningCardRange
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-12-30 10:51:25
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>MORunningCardRange的总记录数</returns>
		public object[] GetAllMORunningCardRange()
		{
			return this.DataProvider.CustomQuery(typeof(MORunningCardRange), new SQLCondition(string.Format("select {0} from TBLMORCARDRANGE order by SEQ,MOCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MORunningCardRange)))));
		}

		public bool CheckRunningCardInRange(string moCode, string rcardType, string runningCard)
		{
			// 如果没有设置范围，则直接返回True
			string strSql = "SELECT COUNT(*) FROM tblMORCardRange WHERE MOCode='" + moCode + "' AND RCardType='" + rcardType + "' ";
			int iCount = this.DataProvider.GetCount(new SQLCondition(strSql));
			if (iCount == 0)
				return true;
			strSql = "SELECT COUNT(*) FROM tblMORCardRange WHERE MOCode='" + moCode + "' AND RCardType='" + rcardType + "' AND MORCARDSTART<='" + runningCard + "' AND MORCARDEND>='" + runningCard + "' ";
			iCount = this.DataProvider.GetCount(new SQLCondition(strSql));
			return (iCount > 0);
		}
		public object[] QueryMORunningCardRange(string mOCode, string rcardType, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(MORunningCardRange), new PagerCondition(string.Format("select {0} from TBLMORCARDRANGE where 1=1  and MOCODE='{1}' and RCARDTYPE like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MORunningCardRange)) , mOCode, rcardType), "RCARDTYPE desc,MORCARDSTART", inclusive, exclusive));
		}
		public int QueryMORunningCardRangeCount(string mOCode, string rcardType)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMORCARDRANGE where 1=1 and MOCODE='{0}' and RCARDTYPE like '{1}%' " , mOCode, rcardType)));
		}

		#endregion
	}		
}