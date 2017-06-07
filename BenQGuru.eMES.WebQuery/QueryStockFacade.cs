using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QueryStockFacade 的摘要说明。
	/// </summary>
	public class QueryStockFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public QueryStockFacade( IDomainDataProvider domainDataProvider )
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

		#region Stock In
		/// <summary>
		/// ** 功能描述:	入库明细查询,分页
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-07-27
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="startTicketNo">起始入库单号,为空不做查询条件</param>
		/// <param name="endTicketNo">结束入库单号,为空不做查询条件</param>
		/// <param name="startInDate">起始入库时间,为0不做查询条件</param>
		/// <param name="endInDate">结束入库时间,为0不做查询条件</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns></returns>
		public object[] QueryMaterialStockIn( 
			string startTicketNo, string endTicketNo, 
			int startInDate, int endInDate, 
			int inclusive, int exclusive )
		{
			string ticketCondition = string.Empty;

			if ( startTicketNo != null && startTicketNo != string.Empty )
			{
				ticketCondition += string.Format(" and TKTNO >= '{0}'", startTicketNo);
			}
			if ( endTicketNo != null && endTicketNo != string.Empty )
			{
				ticketCondition += string.Format(" and TKTNO <= '{0}'", endTicketNo);
			}

			string dateCondition = string.Empty;

			if ( startInDate != 0 )
			{
				dateCondition += string.Format(" and MDATE >= {0}", startInDate);
			}
			if ( endInDate != 0 )
			{
				dateCondition += string.Format(" and MDATE <= {0}", endInDate);
			}
			
			return this.DataProvider.CustomQuery(
				typeof(QStockInDetail),	
				new PagerCondition( 
				string.Format( @"select {0}, b.QTY from( select * from TBLMSTOCKIN where 1=1 {1} {2}) TBLMSTOCKIN, 
									( select TKTNO, count(RCARD) as QTY from TBLMSTOCKIN where 1=1 {1} group by TKTNO ) b
									where TBLMSTOCKIN.TKTNO = b.TKTNO", 
				DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MaterialStockIn)),
				ticketCondition, 
				dateCondition ), 
				"TBLMSTOCKIN.TKTNO", 
				inclusive, 
				exclusive ) );
		}


		/// <summary>
		/// Added by Jessie Lee,2005/9/5
		/// </summary>
		/// <param name="startTicketNo">起始入库单号,为空不做查询条件</param>
		/// <param name="endTicketNo">结束入库单号,为空不做查询条件</param>
		/// <param name="startInDate">起始入库时间,为0不做查询条件</param>
		/// <param name="endInDate">结束入库时间,为0不做查询条件</param>
		/// <param name="includeDelStatus">是否包含删除状态</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns></returns>
		public object[] QueryMaterialStockIn( 
			string startTicketNo, 
			string endTicketNo, 
			int startInDate, 
			int endInDate,
			bool includeDelStatus,
			int inclusive, 
			int exclusive )
		{
			string ticketCondition = string.Empty;

			if ( startTicketNo != null && startTicketNo != string.Empty )
			{
				ticketCondition += string.Format(" and TKTNO >= '{0}'", startTicketNo);
			}
			if ( endTicketNo != null && endTicketNo != string.Empty )
			{
				ticketCondition += string.Format(" and TKTNO <= '{0}'", endTicketNo);
			}

			string dateCondition = string.Empty;

			if ( startInDate != 0 )
			{
				dateCondition += string.Format(" and MDATE >= {0}", startInDate);
			}
			if ( endInDate != 0 )
			{
				dateCondition += string.Format(" and MDATE <= {0}", endInDate);
			}

			//包括删除状态，即显示所有状态
			if(includeDelStatus)
			{
				return this.DataProvider.CustomQuery(
					typeof(QStockInDetail),	
					new PagerCondition( 
					string.Format( @"select {0}, b.QTY from( select * from TBLMSTOCKIN where 1=1 {1} {2}) TBLMSTOCKIN, 
									( select TKTNO, count(RCARD) as QTY from TBLMSTOCKIN where 1=1 {1} group by TKTNO ) b
									where TBLMSTOCKIN.TKTNO = b.TKTNO", 
					DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MaterialStockIn)),
					ticketCondition, 
					dateCondition ), 
					"TBLMSTOCKIN.TKTNO", 
					inclusive, 
					exclusive ) );
			}

			//不包括删除状态
			return this.DataProvider.CustomQuery(
				typeof(QStockInDetail),	
				new PagerCondition( 
				string.Format( @"select {0}, b.QTY from( select * from TBLMSTOCKIN where status<>'{3}' {1} {2}) TBLMSTOCKIN, 
									( select TKTNO, count(RCARD) as QTY from TBLMSTOCKIN where 1=1 {1} group by TKTNO ) b
									where TBLMSTOCKIN.TKTNO = b.TKTNO", 
				DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MaterialStockIn)),
				ticketCondition, 
				dateCondition,
				StockStatus.Deleted), 
				"TBLMSTOCKIN.TKTNO", 
				inclusive, 
				exclusive ) );
		}
		/// <summary>
		/// ** 功能描述:	入库明细查询,求总行数
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-08-01
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="startTicketNo">起始入库单号,为空不做查询条件</param>
		/// <param name="endTicketNo">结束入库单号,为空不做查询条件</param>
		/// <param name="startInDate">起始入库时间,为0不做查询条件</param>
		/// <param name="endInDate">结束入库时间,为0不做查询条件</param>
		/// <returns></returns>
		public int QueryMaterialStockInCount( 
			string startTicketNo, string endTicketNo, 
			int startInDate, int endInDate)
		{			
			string condition = string.Empty;

			if ( startTicketNo != null && startTicketNo != string.Empty )
			{
				condition += string.Format(" and TKTNO >= '{0}'", startTicketNo);
			}
			if ( endTicketNo != null && endTicketNo != string.Empty )
			{
				condition += string.Format(" and TKTNO <= '{0}'", endTicketNo);
			}

			if ( startInDate != 0 )
			{
				condition += string.Format(" and MDATE >= {0}", startInDate);
			}
			if ( endInDate != 0 )
			{
				condition += string.Format(" and MDATE <= {0}", endInDate);
			}
			
			return this.DataProvider.GetCount( new SQLCondition( 
				string.Format( @"select count(*) from TBLMSTOCKIN where 1=1 {0}",condition )));
		}

		/// <summary>
		/// Added by Jessie Lee,2005/9/5
		/// </summary>
		/// <param name="startTicketNo">起始入库单号,为空不做查询条件</param>
		/// <param name="endTicketNo">结束入库单号,为空不做查询条件</param>
		/// <param name="startInDate">起始入库时间,为0不做查询条件</param>
		/// <param name="endInDate">结束入库时间,为0不做查询条件</param>
		/// <param name="includeDelStatus">是否包含删除状态</param>
		/// <returns></returns>
		public int QueryMaterialStockInCount( 
			string startTicketNo, string endTicketNo, 
			int startInDate, int endInDate,bool includeDelStatus)
		{			
			string condition = string.Empty;

			if ( startTicketNo != null && startTicketNo != string.Empty )
			{
				condition += string.Format(" and TKTNO >= '{0}'", startTicketNo);
			}
			if ( endTicketNo != null && endTicketNo != string.Empty )
			{
				condition += string.Format(" and TKTNO <= '{0}'", endTicketNo);
			}

			if ( startInDate != 0 )
			{
				condition += string.Format(" and MDATE >= {0}", startInDate);
			}
			if ( endInDate != 0 )
			{
				condition += string.Format(" and MDATE <= {0}", endInDate);
			}
			
			if(includeDelStatus)
			{
				return this.DataProvider.GetCount( new SQLCondition( 
					string.Format( @"select count(*) from TBLMSTOCKIN where 1=1 {0}",condition )));
			}

			return this.DataProvider.GetCount( new SQLCondition( 
				string.Format( @"select count(*) from TBLMSTOCKIN where status<>'{1}' {0}",condition,StockStatus.Deleted )));	
		}
		#endregion

		#region Stock Out
		/// <summary>
		/// ** 功能描述:	出货单明细查询,分页
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-08-01
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="startTicketNo">起始入库单号,为空不做查询条件</param>
		/// <param name="endTicketNo">结束入库单号,为空不做查询条件</param>
		/// <param name="startRunningCard">开始产品序列号,为空不做查询条件</param>
		/// <param name="endRunningCard">结束产品序列号,为空不做查询条件</param>
		/// <param name="modelCode">机种代码，以','分割的字符串，精确查询</param>
		/// <param name="dealer">经销商，模糊查询</param>
		/// <param name="startOutDate">起始出库时间,为0不做查询条件</param>
		/// <param name="endOutDate">结束出库时间,为0不做查询条件</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns></returns>
		public object[] QueryMaterialStockOut( 
			string startTicketNo, string endTicketNo, 
			string startRunningCard, string endRunningCard,
			string modelCode, string dealer,
			int startOutDate, int endOutDate, 
			int inclusive, int exclusive )
		{
			string condition = string.Empty;

			if ( startTicketNo != null && startTicketNo != string.Empty )
			{
				condition += string.Format(" and tblmstockout.TKTNO >= '{0}'", startTicketNo);
			}
			if ( endTicketNo != null && endTicketNo != string.Empty )
			{
				condition += string.Format(" and tblmstockout.TKTNO <= '{0}'", endTicketNo);
			}

			if ( startRunningCard != null && startRunningCard != string.Empty )
			{
				condition += string.Format(" and tblmstockoutdetail.rcard >= '{0}'", startRunningCard);
			}
			if ( endRunningCard != null && endRunningCard != string.Empty )
			{
				condition += string.Format(" and tblmstockoutdetail.rcard <= '{0}'", endRunningCard);
			}

			if ( modelCode != null && modelCode != string.Empty )
			{
				condition += string.Format(" and tblmstockout.modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
			}
			if ( dealer != null && dealer != string.Empty )
			{
				condition += string.Format(" and upper(tblmstockout.dealer) like '{0}%'", dealer.ToUpper());
			}

			if ( startOutDate != 0 )
			{
				condition += string.Format(" and tblmstockout.outdate >= {0}", startOutDate);
			}
			if ( endOutDate != 0 )
			{
				condition += string.Format(" and tblmstockout.outdate <= {0}", endOutDate);
			}
			
			return this.DataProvider.CustomQuery(
				typeof(QStockOutDetail),	
				new PagerCondition( 
				string.Format( @"select {0},tblmstockoutdetail.rcard from tblmstockout,tblmstockoutdetail 
												where tblmstockout.tktno=tblmstockoutdetail.tktno and tblmstockout.seq=tblmstockoutdetail.seq {1}", 
				DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MaterialStockOut)),
				condition), 
				"tblmstockout.TKTNO,tblmstockout.modelcode,tblmstockout.dealer,tblmstockout.outdate", 
				inclusive, 
				exclusive ) );
		}

		
		/// <summary>
		/// Added by jessie lee,2005/9/5
		/// </summary>
		/// <param name="startTicketNo">起始入库单号,为空不做查询条件</param>
		/// <param name="endTicketNo">结束入库单号,为空不做查询条件</param>
		/// <param name="startRunningCard">开始产品序列号,为空不做查询条件</param>
		/// <param name="endRunningCard">结束产品序列号,为空不做查询条件</param>
		/// <param name="modelCode">机种代码，以','分割的字符串，精确查询</param>
		/// <param name="dealer">经销商，模糊查询</param>
		/// <param name="startOutDate">起始出库时间,为0不做查询条件</param>
		/// <param name="endOutDate">结束出库时间,为0不做查询条件</param>
		/// <param name="includeDelStatus">是否包含删除状态</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns></returns>
		public object[] QueryMaterialStockOut( 
			string startTicketNo, string endTicketNo, 
			string startRunningCard, string endRunningCard,
			string modelCode, string dealer,
			int startOutDate, int endOutDate,
			bool includeDelStatus,
			int inclusive, int exclusive )
		{
			string condition = string.Empty;

			if ( startTicketNo != null && startTicketNo != string.Empty )
			{
				condition += string.Format(" and tblmstockout.TKTNO >= '{0}'", startTicketNo);
			}
			if ( endTicketNo != null && endTicketNo != string.Empty )
			{
				condition += string.Format(" and tblmstockout.TKTNO <= '{0}'", endTicketNo);
			}

			if ( startRunningCard != null && startRunningCard != string.Empty )
			{
				condition += string.Format(" and tblmstockoutdetail.rcard >= '{0}'", startRunningCard);
			}
			if ( endRunningCard != null && endRunningCard != string.Empty )
			{
				condition += string.Format(" and tblmstockoutdetail.rcard <= '{0}'", endRunningCard);
			}

			if ( modelCode != null && modelCode != string.Empty )
			{
				condition += string.Format(" and tblmstockout.modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
			}
			if ( dealer != null && dealer != string.Empty )
			{
				condition += string.Format(" and upper(tblmstockout.dealer) like '{0}%'", dealer.ToUpper());
			}

			if ( startOutDate != 0 )
			{
				condition += string.Format(" and tblmstockout.outdate >= {0}", startOutDate);
			}
			if ( endOutDate != 0 )
			{
				condition += string.Format(" and tblmstockout.outdate <= {0}", endOutDate);
			}
			//包含删除状态，返回所有状态
			if(includeDelStatus)
			{
				return this.DataProvider.CustomQuery(
					typeof(QStockOutDetail),	
					new PagerCondition( 
					string.Format( @"select {0},tblmstockoutdetail.rcard from tblmstockout,tblmstockoutdetail 
												where tblmstockout.tktno=tblmstockoutdetail.tktno and tblmstockout.seq=tblmstockoutdetail.seq {1}", 
					DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MaterialStockOut)),
					condition), 
					"tblmstockout.TKTNO,tblmstockout.modelcode,tblmstockout.dealer,tblmstockout.outdate", 
					inclusive, 
					exclusive ) );
			}

			return this.DataProvider.CustomQuery(
				typeof(QStockOutDetail),	
				new PagerCondition( 
				string.Format( @"select {0},tblmstockoutdetail.rcard from tblmstockout,tblmstockoutdetail 
												where tblmstockout.status<>'{2}' and tblmstockout.tktno=tblmstockoutdetail.tktno and tblmstockout.seq=tblmstockoutdetail.seq {1}", 
				DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MaterialStockOut)),
				condition,
				StockStatus.Deleted), 
				"tblmstockout.TKTNO,tblmstockout.modelcode,tblmstockout.dealer,tblmstockout.outdate", 
				inclusive, 
				exclusive ) );

		}
		/// <summary>
		/// ** 功能描述:	出货单明细查询,求总行数
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-08-01
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="startTicketNo">起始入库单号,为空不做查询条件</param>
		/// <param name="endTicketNo">结束入库单号,为空不做查询条件</param>
		/// <param name="startRunningCard">开始产品序列号,为空不做查询条件</param>
		/// <param name="endRunningCard">结束产品序列号,为空不做查询条件</param>
		/// <param name="modelCode">机种代码，以','分割的字符串，精确查询</param>
		/// <param name="dealer">经销商，模糊查询</param>
		/// <param name="startOutDate">起始出库时间,为0不做查询条件</param>
		/// <param name="endOutDate">结束出库时间,为0不做查询条件</param>
		/// <returns></returns>
		public int QueryMaterialStockOutCount( 
			string startTicketNo, string endTicketNo, 
			string startRunningCard, string endRunningCard,
			string modelCode, string dealer,
			int startOutDate, int endOutDate)
		{			
			string condition = string.Empty;

			if ( startTicketNo != null && startTicketNo != string.Empty )
			{
				condition += string.Format(" and tblmstockout.TKTNO >= '{0}'", startTicketNo);
			}
			if ( endTicketNo != null && endTicketNo != string.Empty )
			{
				condition += string.Format(" and tblmstockout.TKTNO <= '{0}'", endTicketNo);
			}

			if ( startRunningCard != null && startRunningCard != string.Empty )
			{
				condition += string.Format(" and tblmstockoutdetail.rcard >= '{0}'", startRunningCard);
			}
			if ( endRunningCard != null && endRunningCard != string.Empty )
			{
				condition += string.Format(" and tblmstockoutdetail.rcard <= '{0}'", endRunningCard);
			}

			if ( modelCode != null && modelCode != string.Empty )
			{
				condition += string.Format(" and tblmstockout.modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
			}
			if ( dealer != null && dealer != string.Empty )
			{
				condition += string.Format(" and upper(tblmstockout.dealer) like '{0}%'", dealer.ToUpper());
			}

			if ( startOutDate != 0 )
			{
				condition += string.Format(" and tblmstockout.outdate >= {0}", startOutDate);
			}
			if ( endOutDate != 0 )
			{
				condition += string.Format(" and tblmstockout.outdate <= {0}", endOutDate);
			}
			
			return this.DataProvider.GetCount(
				new SQLCondition( 
				string.Format( @"select count(*) from tblmstockout,tblmstockoutdetail 
												where tblmstockout.tktno=tblmstockoutdetail.tktno and tblmstockout.seq=tblmstockoutdetail.seq {0}", 
				condition)) );
		}
	
		/// <summary>
		/// Added by jessie lee,2005/9/5
		/// </summary>
		/// <param name="startTicketNo">起始入库单号,为空不做查询条件</param>
		/// <param name="endTicketNo">结束入库单号,为空不做查询条件</param>
		/// <param name="startRunningCard">开始产品序列号,为空不做查询条件</param>
		/// <param name="endRunningCard">结束产品序列号,为空不做查询条件</param>
		/// <param name="modelCode">机种代码，以','分割的字符串，精确查询</param>
		/// <param name="dealer">经销商，模糊查询</param>
		/// <param name="startOutDate">起始出库时间,为0不做查询条件</param>
		/// <param name="endOutDate">结束出库时间,为0不做查询条件</param>
		/// <param name="includeDelStatus">是否包含删除状态</param>
		/// <returns></returns>
		public int QueryMaterialStockOutCount( 
			string startTicketNo, string endTicketNo, 
			string startRunningCard, string endRunningCard,
			string modelCode, string dealer,
			int startOutDate, int endOutDate,
			bool includeDelStatus )
		{			
			string condition = string.Empty;

			if ( startTicketNo != null && startTicketNo != string.Empty )
			{
				condition += string.Format(" and tblmstockout.TKTNO >= '{0}'", startTicketNo);
			}
			if ( endTicketNo != null && endTicketNo != string.Empty )
			{
				condition += string.Format(" and tblmstockout.TKTNO <= '{0}'", endTicketNo);
			}

			if ( startRunningCard != null && startRunningCard != string.Empty )
			{
				condition += string.Format(" and tblmstockoutdetail.rcard >= '{0}'", startRunningCard);
			}
			if ( endRunningCard != null && endRunningCard != string.Empty )
			{
				condition += string.Format(" and tblmstockoutdetail.rcard <= '{0}'", endRunningCard);
			}

			if ( modelCode != null && modelCode != string.Empty )
			{
				condition += string.Format(" and tblmstockout.modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
			}
			if ( dealer != null && dealer != string.Empty )
			{
				condition += string.Format(" and upper(tblmstockout.dealer) like '{0}%'", dealer.ToUpper());
			}

			if ( startOutDate != 0 )
			{
				condition += string.Format(" and tblmstockout.outdate >= {0}", startOutDate);
			}
			if ( endOutDate != 0 )
			{
				condition += string.Format(" and tblmstockout.outdate <= {0}", endOutDate);
			}
			
			if (includeDelStatus)
			{
				return this.DataProvider.GetCount(
					new SQLCondition( 
					string.Format( @"select count(*) from tblmstockout,tblmstockoutdetail 
												where tblmstockout.tktno=tblmstockoutdetail.tktno and tblmstockout.seq=tblmstockoutdetail.seq {0}", 
					condition)) );
			}

			return this.DataProvider.GetCount(
				new SQLCondition( 
				string.Format( @"select count(*) from tblmstockout,tblmstockoutdetail 
												where tblmstockout.status<>'{0}' and tblmstockout.tktno=tblmstockoutdetail.tktno and tblmstockout.seq=tblmstockoutdetail.seq {1}",
				StockStatus.Deleted, 
				condition)) );
		}
	
		#endregion

		#region Stock In/Out Contrast
		/// <summary>
		/// ** 功能描述:	入库出货明细比对,分页
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-08-02
		/// *************************************
		/// ** 修 改:       jessie lee         
		/// ** 日 期:		2005/9/5
		/// ** 修改描述:	排除状态为删除和新增的
		/// </summary>
		/// <param name="status">状态：已入已出0，已入未出1，未入已出2</param>
		/// <param name="startDate">开始时间,为0不做查询条件</param>
		/// <param name="endDate">结束时间,为0不做查询条件</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns></returns>
		public object[] QueryStockContrast( 
			int status, int startDate, int endDate, 
			int inclusive, int exclusive )
		{
			string condition = string.Empty;
			string sql = string.Empty; 

			if ( startDate != 0 )
			{
				condition += string.Format(" and a.mdate >= {0}", startDate);
			}
			if ( endDate != 0 )
			{
				condition += string.Format(" and a.mdate <= {0}", endDate);
			}
			
			// 已入已出：在查询条件设定的时间区间内以入库时间最早的那一笔为依据，
			// 然后去寻找在这个时间之后有出货记录的资料，此时出货时间与查询条件的时间区间没有关系。
			if ( status == 0 )
			{
				sql = string.Format( 
@"select * from 
    (select a.rcard, a.tktno as INTKTNO, b.tktno as OUTTKTNO, 
    a.mdate as INDATE, b.mdate as OUTDATE, a.muser as inuser, b.muser as outuser,
    row_number() over(partition by a.rcard order by a.mdate,a.mtime) rnum 
    from tblmstockin a, tblmstockoutdetail b 
    where  a.status<>'{0}' and a.rcard=b.rcard 
    {1}
    and (a.mdate<b.mdate or (a.mdate=b.mdate and a.mtime<=b.mtime)) )
where rnum = 1",StockStatus.Deleted ,condition);
			}

			// 已入未出：在查询条件设定的时间区间内以入库时间最早的那一笔为依据，
			// 然后去寻找在这个时间之后没有出货记录的资料，此时出货时间与查询条件的时间区间没有关系。
			if ( status == 1 )
			{ 
				sql = string.Format(
@"select * from 
    (select rcard, tktno as INTKTNO, mdate as INDATE, muser as inuser,
    row_number() over(partition by rcard order by mdate,mtime) rnum 
    from tblmstockin a
    where  a.status<>'{0}' and rcard not in ( select distinct b.rcard from tblmstockin a, tblmstockoutdetail b where 
    a.rcard=b.rcard and (a.mdate<b.mdate or (a.mdate=b.mdate and a.mtime<=b.mtime)) {1})
	{1})
where rnum = 1" ,StockStatus.Deleted ,condition);
			}
			
			// 未入已出：在查询条件设定的时间区间内以出货时间最早的那一笔为依据，
			// 然后去寻找在这个时间之前没有入库记录的资料，此时入库时间与查询条件的时间区间没有关系。
			if ( status == 2 )
			{
				sql = string.Format(
@"select * from 
    (select rcard, tktno as OUTTKTNO, mdate as OUTDATE, muser as OUTUSER,
    row_number() over(partition by rcard order by mdate,mtime) rnum 
    from tblmstockoutdetail a
    where rcard not in ( select distinct a.rcard from tblmstockin a, tblmstockoutdetail b where a.status<>'{0}' and 
    a.rcard=b.rcard and (a.mdate<b.mdate or (a.mdate=b.mdate and a.mtime<=b.mtime)) {1} ) 
	{1})
where rnum = 1" ,StockStatus.Deleted ,condition);
			}

			return this.DataProvider.CustomQuery(
				typeof(QStockContrast),	
				new PagerCondition(sql, "rcard", inclusive, exclusive, true ) );
		}

		/// <summary>
		/// ** 功能描述:	入库出货明细比对,求总行数
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-08-02
		/// ** 修 改:		jessie lee	
		/// ** 日 期:		2005/9/5
		/// </summary>
		/// <param name="status">状态：已入已出0，已入未出1，未入已出2</param>
		/// <param name="startDate">开始时间,为0不做查询条件</param>
		/// <param name="endDate">结束时间,为0不做查询条件</param>
		/// <returns></returns>
		public int QueryStockContrastCount( int status, int startDate, int endDate)
		{			
			string condition = string.Empty;
			string sql = string.Empty; 

			if ( startDate != 0 )
			{
				condition += string.Format(" and a.mdate >= {0}", startDate);
			}
			if ( endDate != 0 )
			{
				condition += string.Format(" and a.mdate <= {0}", endDate);
			}

			if ( status == 0 )
			{
				sql = string.Format( 
@"select count(*) from 
    (select row_number() over(partition by a.rcard order by a.mdate,a.mtime) rnum 
    from tblmstockin a, tblmstockoutdetail b 
    where  a.status<>'{0}' and a.rcard=b.rcard 
    {1}
    and (a.mdate<b.mdate or (a.mdate=b.mdate and a.mtime<=b.mtime)) )
where rnum = 1" ,StockStatus.Deleted ,condition);
			}
			
			if ( status == 1 )
			{ 
				sql = string.Format(
@"select count(*) from 
    (select row_number() over(partition by rcard order by mdate,mtime) rnum 
    from tblmstockin a
    where   a.status<>'{0}' and  rcard not in ( select distinct b.rcard from tblmstockin a, tblmstockoutdetail b where 
    a.rcard=b.rcard and (a.mdate<b.mdate or (a.mdate=b.mdate and a.mtime<=b.mtime)) {1} )
    {1})
where rnum = 1" ,StockStatus.Deleted , condition);
			}
			
			if ( status == 2 )
			{
				sql = string.Format(
@"select count(*) from 
    (select row_number() over(partition by rcard order by mdate,mtime) rnum 
    from tblmstockoutdetail a
    where rcard not in ( select distinct a.rcard from tblmstockin a, tblmstockoutdetail b where  a.status<>'{0}' and  
    a.rcard=b.rcard and (a.mdate<b.mdate or (a.mdate=b.mdate and a.mtime<=b.mtime)) {1} )
    {1})
where rnum = 1" ,StockStatus.Deleted ,condition );
			}
			
			return this.DataProvider.GetCount( new SQLCondition(sql) );
		}
		#endregion
	}
}
