using System;
using System.Collections;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.ATE;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.TSModel;

namespace BenQGuru.eMES.DataCollect
{
	/// <summary>
	/// ATEFacade 的摘要说明。
	/// 文件名:		ATEFacade.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
	/// 创建日期:	2006-5-22 10:29:35
	/// 修改人:
	/// 修改日期:
	/// 描 述:	
	/// 版 本:	
	/// </summary>
	public class ATEFacade
	{
		private IDomainDataProvider _domainDataProvider = null;
		private FacadeHelper _helper = null;

		public ATEFacade(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._helper = new FacadeHelper( DataProvider );
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

		#region ATETestInfo
		/// <summary>
		/// 
		/// </summary>
		public ATETestInfo CreateNewATETestInfo()
		{
			return new ATETestInfo();
		}

		public void AddATETestInfo( ATETestInfo aTETestInfo)
		{
			this._helper.AddDomainObject( aTETestInfo );
		}

		public void UpdateATETestInfo(ATETestInfo aTETestInfo)
		{
			this._helper.UpdateDomainObject( aTETestInfo );
		}

		public void DeleteATETestInfo(ATETestInfo aTETestInfo)
		{
			this._helper.DeleteDomainObject( aTETestInfo );
		}

		public void DeleteATETestInfo(ATETestInfo[] aTETestInfo)
		{
			this._helper.DeleteDomainObject( aTETestInfo );
		}

		public object GetATETestInfo( string pKID )
		{
			return this.DataProvider.CustomSearch(typeof(ATETestInfo), new object[]{ pKID });
		}

		/// <summary>
		/// ** 功能描述:	查询ATETestInfo的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-5-22 10:29:36
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="pKID">PKID，模糊查询</param>
		/// <returns> ATETestInfo的总记录数</returns>
		public int QueryATETestInfoCount( string pKID)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLATETESTINFO where 1=1 and PKID like '{0}%' " , pKID)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询ATETestInfo
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-5-22 10:29:36
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="pKID">PKID，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> ATETestInfo数组</returns>
		public object[] QueryATETestInfo( string pKID, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(ATETestInfo), new PagerCondition(string.Format("select {0} from TBLATETESTINFO where 1=1 and PKID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ATETestInfo)) , pKID), "PKID", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的ATETestInfo
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-5-22 10:29:36
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>ATETestInfo的总记录数</returns>
		public object[] GetAllATETestInfo()
		{
			return this.DataProvider.CustomQuery(typeof(ATETestInfo), new SQLCondition(string.Format("select {0} from TBLATETESTINFO order by PKID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ATETestInfo)))));
		}


		public object[] GetATETestInfoByRCard(string rcard, string rescode)
		{
			return this.DataProvider.CustomQuery(typeof(ATETestInfo), 
				new SQLParamCondition(string.Format("select {0} from TBLATETESTINFO where rcard=$RCARD and rescode=$RESCODE", 
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(ATETestInfo))),
				new SQLParameter[]{ 
									  new SQLParameter("RCARD", typeof(string), rcard),
									  new SQLParameter("RESCODE", typeof(string), rescode)
								  }));
		}

		public object[] GetErrorInfo( ATETestInfo ateTestInfo, string modelcode )
		{
			/* Fail Code,to be confirmed,
			 * 格式为FailCode和KeyName之间的分隔符是^；KeyName之间的分隔符是#；每组FailCode之间的分隔符是*；
			 * 例如：FailCode^KeyName#KeyName*FailCode^KeyName#KeyName */
			if( ateTestInfo.FailCode==null || ateTestInfo.FailCode.Trim().Length == 0 )
			{
				return null;
			}
  
			string[] failGroup = ateTestInfo.FailCode.Split('*');

			int count=0;
			for (int i=0 ; i<failGroup.Length; i++)
			{
				count+= failGroup[i].Split('^')[1].Split('#').Length;
			}

			object[] objs = new object[count];
			int k=0;
			for (int i=0 ; i<failGroup.Length; i++)
			{
				string errorCode = failGroup[i].Split('^')[0];
				string[] errorLoc = failGroup[i].Split('^')[1].Split('#');

				for( int j=0; j<errorLoc.Length; j++)
				{
					TSErrorCode2Location tsinfo = new TSErrorCode2Location();
					tsinfo.ErrorCode = errorCode;

					TSModelFacade tsmodelFacade = new TSModelFacade(this.DataProvider);
					object[] objecgs = tsmodelFacade.QueryECG2ECByECAndModelCode(new string[]{ errorCode  },modelcode);
				
					tsinfo.ErrorCodeGroup = (objecgs[0] as ErrorCodeGroup2ErrorCode).ErrorCodeGroup ; 

					tsinfo.ErrorLocation = errorLoc[j];
					tsinfo.AB = ItemLocationSide.ItemLocationSide_AB;
					objs[k] = tsinfo;
					k++;
				}
			}
			return objs;
		}
		#endregion

	}
}

