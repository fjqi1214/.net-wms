using System;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.DeviceInterface;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.DeviceInterface
{
	/// <summary>
	/// DeviceInterfaceFacade 的摘要说明。
	/// 文件名:		DeviceInterfaceFacade.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
	/// 创建日期:	2006-5-30 09:13:52 上午
	/// 修改人:
	/// 修改日期:
	/// 描 述:	
	/// 版 本:	
	/// </summary>
	public class DeviceInterfaceFacade
	{
		private IDomainDataProvider _domainDataProvider = null;
		private FacadeHelper _helper = null;

 		public DeviceInterfaceFacade()
		{
			this._helper = new FacadeHelper( DataProvider );
		}

		public DeviceInterfaceFacade(IDomainDataProvider domainDataProvider)
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

		#region PreTestValue
		/// <summary>
		/// 
		/// </summary>
		public PreTestValue CreateNewPreTestValue()
		{
			return new PreTestValue();
		}

		public void AddPreTestValue( PreTestValue preTestValue)
		{
			this._helper.AddDomainObject( preTestValue );
		}

		public void UpdatePreTestValue(PreTestValue preTestValue)
		{
			this._helper.UpdateDomainObject( preTestValue );
		}

		public void DeletePreTestValue(PreTestValue preTestValue)
		{
			this._helper.DeleteDomainObject( preTestValue );
		}

		public void DeletePreTestValue(PreTestValue[] preTestValue)
		{
			this._helper.DeleteDomainObject( preTestValue );
		}

		public object GetPreTestValue( string iD )
		{
			return this.DataProvider.CustomSearch(typeof(PreTestValue), new object[]{ iD });
		}

		/// <summary>
		/// ** 功能描述:	查询PreTestValue的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-5-30 09:13:52 上午
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="iD">ID，模糊查询</param>
		/// <returns> PreTestValue的总记录数</returns>
		public int QueryPreTestValueCount( string iD)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tblPreTestValue where 1=1 and ID like '{0}%' " , iD)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询PreTestValue
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-5-30 09:13:52 上午
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="iD">ID，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> PreTestValue数组</returns>
		public object[] QueryPreTestValue( string iD, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(PreTestValue), new PagerCondition(string.Format("select {0} from tblPreTestValue where 1=1 and ID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PreTestValue)) , iD), "ID", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的PreTestValue
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-5-30 09:13:52 上午
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>PreTestValue的总记录数</returns>
		public object[] GetAllPreTestValue()
		{
			return this.DataProvider.CustomQuery(typeof(PreTestValue), new SQLCondition(string.Format("select {0} from tblPreTestValue order by ID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PreTestValue)))));
		}


		#endregion

	}
}

