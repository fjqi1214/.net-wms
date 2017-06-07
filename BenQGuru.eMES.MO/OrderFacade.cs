using System;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Material;

namespace BenQGuru.eMES.MOModel
{
	/// <summary>
	/// DashboardFacade 的摘要说明。
	/// 文件名:		DashboardFacade.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
	/// 创建日期:	2006-4-24 14:26:00
	/// 修改人:
	/// 修改日期:
	/// 描 述:	
	/// 版 本:	
	/// </summary>
	public class OrderFacade:MarshalByRefObject
	{
		private IDomainDataProvider _domainDataProvider = null;
		private FacadeHelper _helper = null;

		public override object InitializeLifetimeService()
		{
			return null;
		}

		public OrderFacade()
		{
			this._helper = new FacadeHelper( DataProvider );
		}

		public OrderFacade(IDomainDataProvider domainDataProvider)
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

		#region Order
		/// <summary>
		/// 
		/// </summary>
		public Order CreateNewOrder()
		{
			return new Order();
		}

		public void AddOrder( Order order)
		{
			this._helper.AddDomainObject( order );
		}

		public void UpdateOrder(Order order)
		{
			this._helper.UpdateDomainObject( order );
		}

		public void DeleteOrder(Order order)
		{
			this._helper.DeleteDomainObject( order, 
				new ICheck[]{ new DeleteAssociateCheck( order,
								this.DataProvider, 
								new Type[]{
											  typeof(OrderDetail), typeof(InvRCard)})} );
		}

		public void DeleteOrder(Order[] order)
		{
			this._helper.DeleteDomainObject( order, 
				new ICheck[]{ new DeleteAssociateCheck( order,
								this.DataProvider, 
								new Type[]{
											  typeof(OrderDetail), typeof(InvRCard)	})} );
		}

		public object GetOrder( string orderNumber )
		{
			return this.DataProvider.CustomSearch(typeof(Order), new object[]{ orderNumber });
		}

		/// <summary>
		/// ** 功能描述:	查询Order的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-4-25 9:04:01
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="orderNumber">OrderNumber，模糊查询</param>
		/// <returns> Order的总记录数</returns>
		public int QueryOrderCount( string orderNumber)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLORDER where 1=1 and OrderNumber like '{0}%' " , orderNumber)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询Order
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-4-25 9:04:01
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="orderNumber">OrderNumber，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> Order数组</returns>
		public object[] QueryOrder( string orderNumber, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(Order), new PagerCondition(string.Format("select {0} from TBLORDER where 1=1 and OrderNumber like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Order)) , orderNumber), "OrderNumber", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的Order
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-4-25 9:04:01
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>Order的总记录数</returns>
		public object[] GetAllOrder()
		{
			return this.DataProvider.CustomQuery(typeof(Order), new SQLCondition(string.Format("select {0} from TBLORDER order by OrderNumber", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Order)))));
		}

		public int QueryORDERCount( string orderNumber, string partnerCode, string itemCodes, string orderstatus)
		{
			string sql = string.Format(
				@"select {0} from tblorder where 1 = 1",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(Order)));
			if( orderNumber!=null && orderNumber.Length >0 )
			{
				sql = string.Format( " {0} and ordernumber like '{1}%' ", sql, orderNumber );
			}

			if( orderstatus!=null && orderstatus.Length >0 )
			{
				sql = string.Format( " {0} and ordernumber = '{1}' ", sql, orderstatus );
			}

			if( partnerCode!=null && partnerCode.Length >0 || itemCodes!=null && itemCodes.Length >0 )
			{
				sql = string.Format( " {0} and ordernumber in (select ordernumber from tblorderdetail where 1=1 ",sql );
				if( partnerCode!=null && partnerCode.Length >0 )
				{
					sql = string.Format( " {0} and partnercode like '{1}%' ", sql, partnerCode );
				}

				if( itemCodes!=null && itemCodes.Length >0 )
				{
					sql = string.Format( " {0} and itemcode in ({1})", sql, FormatHelper.ProcessQueryValues(itemCodes) );
				}
				
				sql = sql +")";
			}
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from ({0})" , sql)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询ORDER
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-4-24 14:26:00
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="orderNumber">OrderNumber，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> ORDER数组</returns>
		public object[] QueryORDER( string orderNumber, string partnerCode, string itemCodes, string orderstatus, int inclusive, int exclusive )
		{
			string sql = string.Format(
				@"select {0} from tblorder where 1 = 1",
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(Order)));
			if( orderNumber!=null && orderNumber.Length >0 )
			{
				sql = string.Format( " {0} and ordernumber like '{1}%' ", sql, orderNumber );
			}

			if( orderstatus!=null && orderstatus.Length >0 )
			{
				sql = string.Format( " {0} and ordernumber = '{1}' ", sql, orderstatus );
			}

			if( partnerCode!=null && partnerCode.Length >0 || itemCodes!=null && itemCodes.Length >0 )
			{
				sql = string.Format( " {0} and ordernumber in (select ordernumber from tblorderdetail where 1=1 ",sql );
				if( partnerCode!=null && partnerCode.Length >0 )
				{
					sql = string.Format( " {0} and partnercode like '{1}%' ", sql, partnerCode );
				}

				if( itemCodes!=null && itemCodes.Length >0 )
				{
					sql = string.Format( " {0} and itemcode in ({1})", sql, FormatHelper.ProcessQueryValues(itemCodes) );
				}
				
				sql = sql +")";
			}
			
			return this.DataProvider.CustomQuery(typeof(Order), new PagerCondition( sql, "OrderNumber", inclusive, exclusive));
		}

		public string[] QueryOrderByPartner( string partnerCode )
		{
			string sql = String.Format("select ordernumber from tblorder where ordernumber in (select ordernumber from tblorderdetail where  partnercode='{0}')", partnerCode);
			object[] orderNOs = this.DataProvider.CustomQuery( typeof(Order), new SQLCondition( sql ));
			if(orderNOs!=null && orderNOs.Length>0)
			{
				string[] orders = new string[orderNOs.Length];
				for(int i=0; i<orderNOs.Length; i++)
				{
					orders[i] = (orderNOs[i] as Order).OrderNumber;
				}
				return orders;
			}
			return null;
		}

		public int CheckOrder( string orderNumber, string rcard )
		{
			string sql = string.Format( @"select count(*)
				from tblorder
				where ordernumber in
					(select ordernumber
						from tblorderdetail
						where ordernumber = '{0}'
						and itemcode in
							(select itemcode from tblsimulationreport where rcard = '{1}'))",
				orderNumber, rcard);
			return this.DataProvider.GetCount( new SQLCondition( sql ) );
		}

		public bool CheckOrderIsCompleted( string ordernumber)
		{
			string sql = string.Format("select count(*) from tblorderdetail where actqty<planqty and ordernumber='{0}'", ordernumber);
			int count = this.DataProvider.GetCount( new SQLCondition( sql ));
			return count>0 ? false:true;
		}

		#endregion

		#region OrderDetail
		/// <summary>
		/// 
		/// </summary>
		public OrderDetail CreateNewOrderDetail()
		{
			return new OrderDetail();
		}

		public void AddOrderDetail( OrderDetail orderDetail)
		{
			this._helper.AddDomainObject( orderDetail );
		}

		public void UpdateOrderDetail(OrderDetail orderDetail)
		{
			this._helper.UpdateDomainObject( orderDetail );
		}

		public void DeleteOrderDetail(OrderDetail orderDetail)
		{
			this._helper.DeleteDomainObject( orderDetail,
				new ICheck[]{ new DeleteAssociateCheck( orderDetail,
								this.DataProvider, 
								new Type[]{
											 typeof(InvRCard)	})});
		}

		public void DeleteOrderDetail(OrderDetail[] orderDetail)
		{
			this._helper.DeleteDomainObject( orderDetail,
				new ICheck[]{ new DeleteAssociateCheck( orderDetail,
								this.DataProvider, 
								new Type[]{
											  typeof(InvRCard)	})});
		}

		public object GetOrderDetail( string partnerCode, string itemCode, string orderNumber )
		{
			return this.DataProvider.CustomSearch(typeof(OrderDetail), new object[]{ partnerCode, itemCode, orderNumber });
		}

		/// <summary>
		/// ** 功能描述:	查询OrderDetail的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-4-25 9:04:01
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="partnerCode">PartnerCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="orderNumber">OrderNumber，模糊查询</param>
		/// <returns> OrderDetail的总记录数</returns>
		public int QueryOrderDetailCount( string partnerCode, string itemCode, string orderNumber)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLORDERDETAIL where 1=1 and PartnerCode like '{0}%'  and ItemCode like '{1}%'  and OrderNumber like '{2}%' " , partnerCode, itemCode, orderNumber)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询OrderDetail
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-4-25 9:04:01
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="partnerCode">PartnerCode，模糊查询</param>
		/// <param name="itemCode">ItemCode，模糊查询</param>
		/// <param name="orderNumber">OrderNumber，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> OrderDetail数组</returns>
		public object[] QueryOrderDetail( string partnerCode, string itemCode, string orderNumber, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(OrderDetail), new PagerCondition(string.Format("select {0} from TBLORDERDETAIL where 1=1 and PartnerCode like '{1}%'  and ItemCode like '{2}%'  and OrderNumber like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OrderDetail)) , partnerCode, itemCode, orderNumber), "PartnerCode,ItemCode,OrderNumber", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的OrderDetail
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2006-4-25 9:04:01
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>OrderDetail的总记录数</returns>
		public object[] GetAllOrderDetail()
		{
			return this.DataProvider.CustomQuery(typeof(OrderDetail), new SQLCondition(string.Format("select {0} from TBLORDERDETAIL order by PartnerCode,ItemCode,OrderNumber", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OrderDetail)))));
		}


		#endregion

	}
}

