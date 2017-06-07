using System;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.ReportView;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.ReportView
{
	/// <summary>
	/// ReportViewFacade 的摘要说明。
	/// 文件名:		ReportViewFacade.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
	/// 创建日期:	2007-7-17 9:17:33
	/// 修改人:
	/// 修改日期:
	/// 描 述:	
	/// 版 本:	
	/// </summary>
	public partial class ReportViewFacade
	{
		private IDomainDataProvider _domainDataProvider = null;
		private FacadeHelper _helper = null;

 		public ReportViewFacade()
		{
			this._helper = new FacadeHelper( DataProvider );
		}

		public ReportViewFacade(IDomainDataProvider domainDataProvider)
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

		#region RptViewChartCategory
		/// <summary>
		/// 数据分组栏位
		/// </summary>
		public RptViewChartCategory CreateNewRptViewChartCategory()
		{
			return new RptViewChartCategory();
		}

		public void AddRptViewChartCategory( RptViewChartCategory rptViewChartCategory)
		{
			this._helper.AddDomainObject( rptViewChartCategory );
		}

		public void UpdateRptViewChartCategory(RptViewChartCategory rptViewChartCategory)
		{
			this._helper.UpdateDomainObject( rptViewChartCategory );
		}

		public void DeleteRptViewChartCategory(RptViewChartCategory rptViewChartCategory)
		{
			this._helper.DeleteDomainObject( rptViewChartCategory );
		}

		public void DeleteRptViewChartCategory(RptViewChartCategory[] rptViewChartCategory)
		{
			this._helper.DeleteDomainObject( rptViewChartCategory );
		}

		public object GetRptViewChartCategory( string reportID, decimal chartSequence, decimal categorySequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewChartCategory), new object[]{ reportID, chartSequence, categorySequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewChartCategory的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="chartSequence">ChartSequence，模糊查询</param>
		/// <param name="categorySequence">CategorySequence，模糊查询</param>
		/// <returns> RptViewChartCategory的总记录数</returns>
		public int QueryRptViewChartCategoryCount( string reportID, decimal chartSequence, decimal categorySequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVCHARTCATE where 1=1 and RPTID like '{0}%'  and CHARTSEQ like '{1}%'  and CATESEQ like '{2}%' " , reportID, chartSequence, categorySequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewChartCategory
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="chartSequence">ChartSequence，模糊查询</param>
		/// <param name="categorySequence">CategorySequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewChartCategory数组</returns>
		public object[] QueryRptViewChartCategory( string reportID, decimal chartSequence, decimal categorySequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewChartCategory), new PagerCondition(string.Format("select {0} from TBLRPTVCHARTCATE where 1=1 and RPTID like '{1}%'  and CHARTSEQ like '{2}%'  and CATESEQ like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewChartCategory)) , reportID, chartSequence, categorySequence), "RPTID,CHARTSEQ,CATESEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewChartCategory
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewChartCategory的总记录数</returns>
		public object[] GetAllRptViewChartCategory()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewChartCategory), new SQLCondition(string.Format("select {0} from TBLRPTVCHARTCATE order by RPTID,CHARTSEQ,CATESEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewChartCategory)))));
		}


		#endregion

		#region RptViewChartData
		/// <summary>
		/// 数据统计栏位
		/// </summary>
		public RptViewChartData CreateNewRptViewChartData()
		{
			return new RptViewChartData();
		}

		public void AddRptViewChartData( RptViewChartData rptViewChartData)
		{
			this._helper.AddDomainObject( rptViewChartData );
		}

		public void UpdateRptViewChartData(RptViewChartData rptViewChartData)
		{
			this._helper.UpdateDomainObject( rptViewChartData );
		}

		public void DeleteRptViewChartData(RptViewChartData rptViewChartData)
		{
			this._helper.DeleteDomainObject( rptViewChartData );
		}

		public void DeleteRptViewChartData(RptViewChartData[] rptViewChartData)
		{
			this._helper.DeleteDomainObject( rptViewChartData );
		}

		public object GetRptViewChartData( string reportID, decimal dataSequence, decimal chartSequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewChartData), new object[]{ reportID, dataSequence, chartSequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewChartData的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="dataSequence">DataSequence，模糊查询</param>
		/// <param name="chartSequence">ChartSequence，模糊查询</param>
		/// <returns> RptViewChartData的总记录数</returns>
		public int QueryRptViewChartDataCount( string reportID, decimal dataSequence, decimal chartSequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVCHARTDATA where 1=1 and RPTID like '{0}%'  and DATASEQ like '{1}%'  and CHARTSEQ like '{2}%' " , reportID, dataSequence, chartSequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewChartData
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="dataSequence">DataSequence，模糊查询</param>
		/// <param name="chartSequence">ChartSequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewChartData数组</returns>
		public object[] QueryRptViewChartData( string reportID, decimal dataSequence, decimal chartSequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewChartData), new PagerCondition(string.Format("select {0} from TBLRPTVCHARTDATA where 1=1 and RPTID like '{1}%'  and DATASEQ like '{2}%'  and CHARTSEQ like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewChartData)) , reportID, dataSequence, chartSequence), "RPTID,DATASEQ,CHARTSEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewChartData
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewChartData的总记录数</returns>
		public object[] GetAllRptViewChartData()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewChartData), new SQLCondition(string.Format("select {0} from TBLRPTVCHARTDATA order by RPTID,DATASEQ,CHARTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewChartData)))));
		}


		#endregion

		#region RptViewChartMain
		/// <summary>
		/// 报表显示栏位
		/// </summary>
		public RptViewChartMain CreateNewRptViewChartMain()
		{
			return new RptViewChartMain();
		}

		public void AddRptViewChartMain( RptViewChartMain rptViewChartMain)
		{
			this._helper.AddDomainObject( rptViewChartMain );
		}

		public void UpdateRptViewChartMain(RptViewChartMain rptViewChartMain)
		{
			this._helper.UpdateDomainObject( rptViewChartMain );
		}

		public void DeleteRptViewChartMain(RptViewChartMain rptViewChartMain)
		{
			this._helper.DeleteDomainObject( rptViewChartMain );
		}

		public void DeleteRptViewChartMain(RptViewChartMain[] rptViewChartMain)
		{
			this._helper.DeleteDomainObject( rptViewChartMain );
		}

		public object GetRptViewChartMain( string reportID, decimal chartSequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewChartMain), new object[]{ reportID, chartSequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewChartMain的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="chartSequence">ChartSequence，模糊查询</param>
		/// <returns> RptViewChartMain的总记录数</returns>
		public int QueryRptViewChartMainCount( string reportID, decimal chartSequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVCHARTMAIN where 1=1 and RPTID like '{0}%'  and CHARTSEQ like '{1}%' " , reportID, chartSequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewChartMain
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="chartSequence">ChartSequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewChartMain数组</returns>
		public object[] QueryRptViewChartMain( string reportID, decimal chartSequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewChartMain), new PagerCondition(string.Format("select {0} from TBLRPTVCHARTMAIN where 1=1 and RPTID like '{1}%'  and CHARTSEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewChartMain)) , reportID, chartSequence), "RPTID,CHARTSEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewChartMain
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewChartMain的总记录数</returns>
		public object[] GetAllRptViewChartMain()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewChartMain), new SQLCondition(string.Format("select {0} from TBLRPTVCHARTMAIN order by RPTID,CHARTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewChartMain)))));
		}


		#endregion

		#region RptViewChartSeries
		/// <summary>
		/// 系列分组栏位
		/// </summary>
		public RptViewChartSeries CreateNewRptViewChartSeries()
		{
			return new RptViewChartSeries();
		}

		public void AddRptViewChartSeries( RptViewChartSeries rptViewChartSeries)
		{
			this._helper.AddDomainObject( rptViewChartSeries );
		}

		public void UpdateRptViewChartSeries(RptViewChartSeries rptViewChartSeries)
		{
			this._helper.UpdateDomainObject( rptViewChartSeries );
		}

		public void DeleteRptViewChartSeries(RptViewChartSeries rptViewChartSeries)
		{
			this._helper.DeleteDomainObject( rptViewChartSeries );
		}

		public void DeleteRptViewChartSeries(RptViewChartSeries[] rptViewChartSeries)
		{
			this._helper.DeleteDomainObject( rptViewChartSeries );
		}

		public object GetRptViewChartSeries( string reportID, decimal chartSequence, decimal seriesSequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewChartSeries), new object[]{ reportID, chartSequence, seriesSequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewChartSeries的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="chartSequence">ChartSequence，模糊查询</param>
		/// <param name="seriesSequence">SeriesSequence，模糊查询</param>
		/// <returns> RptViewChartSeries的总记录数</returns>
		public int QueryRptViewChartSeriesCount( string reportID, decimal chartSequence, decimal seriesSequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVCHARTSER where 1=1 and RPTID like '{0}%'  and CHARTSEQ like '{1}%'  and SERSEQ like '{2}%' " , reportID, chartSequence, seriesSequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewChartSeries
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="chartSequence">ChartSequence，模糊查询</param>
		/// <param name="seriesSequence">SeriesSequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewChartSeries数组</returns>
		public object[] QueryRptViewChartSeries( string reportID, decimal chartSequence, decimal seriesSequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewChartSeries), new PagerCondition(string.Format("select {0} from TBLRPTVCHARTSER where 1=1 and RPTID like '{1}%'  and CHARTSEQ like '{2}%'  and SERSEQ like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewChartSeries)) , reportID, chartSequence, seriesSequence), "RPTID,CHARTSEQ,SERSEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewChartSeries
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewChartSeries的总记录数</returns>
		public object[] GetAllRptViewChartSeries()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewChartSeries), new SQLCondition(string.Format("select {0} from TBLRPTVCHARTSER order by RPTID,CHARTSEQ,SERSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewChartSeries)))));
		}


		#endregion

		#region RptViewDataConnect
		/// <summary>
		/// 数据库连接
		/// </summary>
		public RptViewDataConnect CreateNewRptViewDataConnect()
		{
			return new RptViewDataConnect();
		}

		public void AddRptViewDataConnect( RptViewDataConnect rptViewDataConnect)
		{
			this._helper.AddDomainObject( rptViewDataConnect );
		}

		public void UpdateRptViewDataConnect(RptViewDataConnect rptViewDataConnect)
		{
			this._helper.UpdateDomainObject( rptViewDataConnect );
		}

		public void DeleteRptViewDataConnect(RptViewDataConnect rptViewDataConnect)
		{
			this._helper.DeleteDomainObject( rptViewDataConnect );
		}

		public void DeleteRptViewDataConnect(RptViewDataConnect[] rptViewDataConnect)
		{
			this._helper.DeleteDomainObject( rptViewDataConnect );
		}

		public object GetRptViewDataConnect( decimal dataConnectID )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewDataConnect), new object[]{ dataConnectID });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewDataConnect的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="dataConnectID">DataConnectID，模糊查询</param>
		/// <returns> RptViewDataConnect的总记录数</returns>
		public int QueryRptViewDataConnectCount( decimal dataConnectID)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVCONNECT where 1=1 and DATACONNECTID like '{0}%' " , dataConnectID)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewDataConnect
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="dataConnectID">DataConnectID，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewDataConnect数组</returns>
		public object[] QueryRptViewDataConnect( decimal dataConnectID, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataConnect), new PagerCondition(string.Format("select {0} from TBLRPTVCONNECT where 1=1 and DATACONNECTID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataConnect)) , dataConnectID), "DATACONNECTID", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewDataConnect
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewDataConnect的总记录数</returns>
		public object[] GetAllRptViewDataConnect()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataConnect), new SQLCondition(string.Format("select {0} from TBLRPTVCONNECT order by DATACONNECTID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataConnect)))));
		}


		#endregion

		#region RptViewDataFormat
		/// <summary>
		/// 文本显示格式
		/// </summary>
		public RptViewDataFormat CreateNewRptViewDataFormat()
		{
			return new RptViewDataFormat();
		}

		public void AddRptViewDataFormat( RptViewDataFormat rptViewDataFormat)
		{
			this._helper.AddDomainObject( rptViewDataFormat );
		}

		public void UpdateRptViewDataFormat(RptViewDataFormat rptViewDataFormat)
		{
			this._helper.UpdateDomainObject( rptViewDataFormat );
		}

		public void DeleteRptViewDataFormat(RptViewDataFormat rptViewDataFormat)
		{
			this._helper.DeleteDomainObject( rptViewDataFormat );
		}

		public void DeleteRptViewDataFormat(RptViewDataFormat[] rptViewDataFormat)
		{
			this._helper.DeleteDomainObject( rptViewDataFormat );
		}

		public object GetRptViewDataFormat( string formatID )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewDataFormat), new object[]{ formatID });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewDataFormat的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="formatID">FormatID，模糊查询</param>
		/// <returns> RptViewDataFormat的总记录数</returns>
		public int QueryRptViewDataFormatCount( string formatID)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVDATAFMT where 1=1 and FORMATID like '{0}%' " , formatID)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewDataFormat
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="formatID">FormatID，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewDataFormat数组</returns>
		public object[] QueryRptViewDataFormat( string formatID, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataFormat), new PagerCondition(string.Format("select {0} from TBLRPTVDATAFMT where 1=1 and FORMATID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataFormat)) , formatID), "FORMATID", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewDataFormat
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewDataFormat的总记录数</returns>
		public object[] GetAllRptViewDataFormat()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataFormat), new SQLCondition(string.Format("select {0} from TBLRPTVDATAFMT order by FORMATID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataFormat)))));
		}


		#endregion

		#region RptViewDataSource
		/// <summary>
		/// 数据查询
		/// </summary>
		public RptViewDataSource CreateNewRptViewDataSource()
		{
			return new RptViewDataSource();
		}

		public void AddRptViewDataSource( RptViewDataSource rptViewDataSource)
		{
			this._helper.AddDomainObject( rptViewDataSource );
		}

		public void UpdateRptViewDataSource(RptViewDataSource rptViewDataSource)
		{
			this._helper.UpdateDomainObject( rptViewDataSource );
		}

		public void DeleteRptViewDataSource(RptViewDataSource rptViewDataSource)
		{
			this._helper.DeleteDomainObject( rptViewDataSource );
		}

		public void DeleteRptViewDataSource(RptViewDataSource[] rptViewDataSource)
		{
			this._helper.DeleteDomainObject( rptViewDataSource );
		}

		public object GetRptViewDataSource( decimal dataSourceID )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewDataSource), new object[]{ dataSourceID });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewDataSource的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="dataSourceID">DataSourceID，模糊查询</param>
		/// <returns> RptViewDataSource的总记录数</returns>
		public int QueryRptViewDataSourceCount( decimal dataSourceID)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVDATASRC where 1=1 and DATASOURCEID like '{0}%' " , dataSourceID)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewDataSource
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="dataSourceID">DataSourceID，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewDataSource数组</returns>
		public object[] QueryRptViewDataSource( decimal dataSourceID, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataSource), new PagerCondition(string.Format("select {0} from TBLRPTVDATASRC where 1=1 and DATASOURCEID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSource)) , dataSourceID), "DATASOURCEID", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewDataSource
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewDataSource的总记录数</returns>
		public object[] GetAllRptViewDataSource()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataSource), new SQLCondition(string.Format("select {0} from TBLRPTVDATASRC order by DATASOURCEID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSource)))));
		}


		#endregion

		#region RptViewDataSourceColumn
		/// <summary>
		/// 数据查询栏位描述
		/// </summary>
		public RptViewDataSourceColumn CreateNewRptViewDataSourceColumn()
		{
			return new RptViewDataSourceColumn();
		}

		public void AddRptViewDataSourceColumn( RptViewDataSourceColumn rptViewDataSourceColumn)
		{
			this._helper.AddDomainObject( rptViewDataSourceColumn );
		}

		public void UpdateRptViewDataSourceColumn(RptViewDataSourceColumn rptViewDataSourceColumn)
		{
			this._helper.UpdateDomainObject( rptViewDataSourceColumn );
		}

		public void DeleteRptViewDataSourceColumn(RptViewDataSourceColumn rptViewDataSourceColumn)
		{
			this._helper.DeleteDomainObject( rptViewDataSourceColumn );
		}

		public void DeleteRptViewDataSourceColumn(RptViewDataSourceColumn[] rptViewDataSourceColumn)
		{
			this._helper.DeleteDomainObject( rptViewDataSourceColumn );
		}

		public object GetRptViewDataSourceColumn( decimal dataSourceID, decimal columnSequence, string columnName )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewDataSourceColumn), new object[]{ dataSourceID, columnSequence, columnName });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewDataSourceColumn的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="dataSourceID">DataSourceID，模糊查询</param>
		/// <param name="columnSequence">ColumnSequence，模糊查询</param>
		/// <param name="columnName">ColumnName，模糊查询</param>
		/// <returns> RptViewDataSourceColumn的总记录数</returns>
		public int QueryRptViewDataSourceColumnCount( decimal dataSourceID, decimal columnSequence, string columnName)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVDATASRCCOLUMN where 1=1 and DATASOURCEID like '{0}%'  and COLUMNSEQ like '{1}%'  and COLUMNNAME like '{2}%' " , dataSourceID, columnSequence, columnName)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewDataSourceColumn
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="dataSourceID">DataSourceID，模糊查询</param>
		/// <param name="columnSequence">ColumnSequence，模糊查询</param>
		/// <param name="columnName">ColumnName，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewDataSourceColumn数组</returns>
		public object[] QueryRptViewDataSourceColumn( decimal dataSourceID, decimal columnSequence, string columnName, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataSourceColumn), new PagerCondition(string.Format("select {0} from TBLRPTVDATASRCCOLUMN where 1=1 and DATASOURCEID like '{1}%'  and COLUMNSEQ like '{2}%'  and COLUMNNAME like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSourceColumn)) , dataSourceID, columnSequence, columnName), "DATASOURCEID,COLUMNSEQ,COLUMNNAME", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewDataSourceColumn
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewDataSourceColumn的总记录数</returns>
		public object[] GetAllRptViewDataSourceColumn()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataSourceColumn), new SQLCondition(string.Format("select {0} from TBLRPTVDATASRCCOLUMN order by DATASOURCEID,COLUMNSEQ,COLUMNNAME", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSourceColumn)))));
		}


		#endregion

		#region RptViewDataSourceParam
		/// <summary>
		/// 数据查询参数(仅对DLL有效)
		/// </summary>
		public RptViewDataSourceParam CreateNewRptViewDataSourceParam()
		{
			return new RptViewDataSourceParam();
		}

		public void AddRptViewDataSourceParam( RptViewDataSourceParam rptViewDataSourceParam)
		{
			this._helper.AddDomainObject( rptViewDataSourceParam );
		}

		public void UpdateRptViewDataSourceParam(RptViewDataSourceParam rptViewDataSourceParam)
		{
			this._helper.UpdateDomainObject( rptViewDataSourceParam );
		}

		public void DeleteRptViewDataSourceParam(RptViewDataSourceParam rptViewDataSourceParam)
		{
			this._helper.DeleteDomainObject( rptViewDataSourceParam );
		}

		public void DeleteRptViewDataSourceParam(RptViewDataSourceParam[] rptViewDataSourceParam)
		{
			this._helper.DeleteDomainObject( rptViewDataSourceParam );
		}

		public object GetRptViewDataSourceParam( decimal dataSourceID, decimal parameterSequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewDataSourceParam), new object[]{ dataSourceID, parameterSequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewDataSourceParam的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="dataSourceID">DataSourceID，模糊查询</param>
		/// <param name="parameterSequence">ParameterSequence，模糊查询</param>
		/// <returns> RptViewDataSourceParam的总记录数</returns>
		public int QueryRptViewDataSourceParamCount( decimal dataSourceID, decimal parameterSequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVDATASRCPARAM where 1=1 and DATASOURCEID like '{0}%'  and PARAMSEQ like '{1}%' " , dataSourceID, parameterSequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewDataSourceParam
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="dataSourceID">DataSourceID，模糊查询</param>
		/// <param name="parameterSequence">ParameterSequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewDataSourceParam数组</returns>
		public object[] QueryRptViewDataSourceParam( decimal dataSourceID, decimal parameterSequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataSourceParam), new PagerCondition(string.Format("select {0} from TBLRPTVDATASRCPARAM where 1=1 and DATASOURCEID like '{1}%'  and PARAMSEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSourceParam)) , dataSourceID, parameterSequence), "DATASOURCEID,PARAMSEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewDataSourceParam
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewDataSourceParam的总记录数</returns>
		public object[] GetAllRptViewDataSourceParam()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDataSourceParam), new SQLCondition(string.Format("select {0} from TBLRPTVDATASRCPARAM order by DATASOURCEID,PARAMSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSourceParam)))));
		}


		#endregion

		#region RptViewDesignMain
		/// <summary>
		/// 自定义报表主表
		/// </summary>
		public RptViewDesignMain CreateNewRptViewDesignMain()
		{
			return new RptViewDesignMain();
		}

		public void AddRptViewDesignMain( RptViewDesignMain rptViewDesignMain)
		{
			this._helper.AddDomainObject( rptViewDesignMain );
		}

		public void UpdateRptViewDesignMain(RptViewDesignMain rptViewDesignMain)
		{
			this._helper.UpdateDomainObject( rptViewDesignMain );
		}

		public void DeleteRptViewDesignMain(RptViewDesignMain rptViewDesignMain)
		{
			this._helper.DeleteDomainObject( rptViewDesignMain );
		}

		public void DeleteRptViewDesignMain(RptViewDesignMain[] rptViewDesignMain)
		{
			this._helper.DeleteDomainObject( rptViewDesignMain );
		}

		public object GetRptViewDesignMain( string reportID )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewDesignMain), new object[]{ reportID });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewDesignMain的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <returns> RptViewDesignMain的总记录数</returns>
		public int QueryRptViewDesignMainCount( string reportID)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVDESIGNMAIN where 1=1 and RPTID like '{0}%' " , reportID)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewDesignMain
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewDesignMain数组</returns>
		public object[] QueryRptViewDesignMain( string reportID, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDesignMain), new PagerCondition(string.Format("select {0} from TBLRPTVDESIGNMAIN where 1=1 and RPTID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDesignMain)) , reportID), "RPTID", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewDesignMain
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewDesignMain的总记录数</returns>
		public object[] GetAllRptViewDesignMain()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewDesignMain), new SQLCondition(string.Format("select {0} from TBLRPTVDESIGNMAIN order by RPTID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDesignMain)))));
		}


		#endregion

		#region RptViewEntry
		/// <summary>
		/// 报表结构维护
		/// </summary>
		public RptViewEntry CreateNewRptViewEntry()
		{
			return new RptViewEntry();
		}

		public void AddRptViewEntry( RptViewEntry rptViewEntry)
		{
			this._helper.AddDomainObject( rptViewEntry );
		}

		public void UpdateRptViewEntry(RptViewEntry rptViewEntry)
		{
			this._helper.UpdateDomainObject( rptViewEntry );
		}

		public void DeleteRptViewEntry(RptViewEntry rptViewEntry)
		{
			this._helper.DeleteDomainObject( rptViewEntry );
		}

		public void DeleteRptViewEntry(RptViewEntry[] rptViewEntry)
		{
			this._helper.DeleteDomainObject( rptViewEntry );
		}

		public object GetRptViewEntry( string entryCode )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewEntry), new object[]{ entryCode });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewEntry的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="entryCode">EntryCode，模糊查询</param>
		/// <returns> RptViewEntry的总记录数</returns>
		public int QueryRptViewEntryCount( string entryCode)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVENTRY where 1=1 and ENTRYCODE like '{0}%' " , entryCode)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewEntry
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="entryCode">EntryCode，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewEntry数组</returns>
		public object[] QueryRptViewEntry( string entryCode, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewEntry), new PagerCondition(string.Format("select {0} from TBLRPTVENTRY where 1=1 and ENTRYCODE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewEntry)) , entryCode), "ENTRYCODE", inclusive, exclusive));
		}

        public object[] QueryRptViewEntryForMenu(string entryCode)
        {
            return this.DataProvider.CustomQuery(typeof(RptViewEntry), new SQLCondition(string.Format("SELECT {0} FROM tblrptventry WHERE visible = '{1}' AND entrycode LIKE '{2}%' ORDER BY PENTRYCODE, SEQ ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewEntry)), FormatHelper.TRUE_STRING, entryCode)));
        }

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewEntry
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewEntry的总记录数</returns>
		public object[] GetAllRptViewEntry()
		{
            return this.DataProvider.CustomQuery(typeof(RptViewEntry), new SQLCondition(string.Format("select {0} from TBLRPTVENTRY order by Seq,ENTRYCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewEntry)))));
		}


		#endregion

		#region RptViewExtendText
		/// <summary>
		/// 报表显示栏位
		/// </summary>
		public RptViewExtendText CreateNewRptViewExtendText()
		{
			return new RptViewExtendText();
		}

		public void AddRptViewExtendText( RptViewExtendText rptViewExtendText)
		{
			this._helper.AddDomainObject( rptViewExtendText );
		}

		public void UpdateRptViewExtendText(RptViewExtendText rptViewExtendText)
		{
			this._helper.UpdateDomainObject( rptViewExtendText );
		}

		public void DeleteRptViewExtendText(RptViewExtendText rptViewExtendText)
		{
			this._helper.DeleteDomainObject( rptViewExtendText );
		}

		public void DeleteRptViewExtendText(RptViewExtendText[] rptViewExtendText)
		{
			this._helper.DeleteDomainObject( rptViewExtendText );
		}

		public object GetRptViewExtendText( string reportID, decimal sequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewExtendText), new object[]{ reportID, sequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewExtendText的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <returns> RptViewExtendText的总记录数</returns>
		public int QueryRptViewExtendTextCount( string reportID, decimal sequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVEXTTEXT where 1=1 and RPTID like '{0}%'  and SEQ like '{1}%' " , reportID, sequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewExtendText
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewExtendText数组</returns>
		public object[] QueryRptViewExtendText( string reportID, decimal sequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewExtendText), new PagerCondition(string.Format("select {0} from TBLRPTVEXTTEXT where 1=1 and RPTID like '{1}%'  and SEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewExtendText)) , reportID, sequence), "RPTID,SEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewExtendText
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewExtendText的总记录数</returns>
		public object[] GetAllRptViewExtendText()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewExtendText), new SQLCondition(string.Format("select {0} from TBLRPTVEXTTEXT order by RPTID,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewExtendText)))));
		}


		#endregion

		#region RptViewFileParameter
		/// <summary>
		/// 报表文件参数列表
		/// </summary>
		public RptViewFileParameter CreateNewRptViewFileParameter()
		{
			return new RptViewFileParameter();
		}

		public void AddRptViewFileParameter( RptViewFileParameter rptViewFileParameter)
		{
			this._helper.AddDomainObject( rptViewFileParameter );
		}

		public void UpdateRptViewFileParameter(RptViewFileParameter rptViewFileParameter)
		{
			this._helper.UpdateDomainObject( rptViewFileParameter );
		}

		public void DeleteRptViewFileParameter(RptViewFileParameter rptViewFileParameter)
		{
			this._helper.DeleteDomainObject( rptViewFileParameter );
		}

		public void DeleteRptViewFileParameter(RptViewFileParameter[] rptViewFileParameter)
		{
			this._helper.DeleteDomainObject( rptViewFileParameter );
		}

		public object GetRptViewFileParameter( string reportID, decimal sequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewFileParameter), new object[]{ reportID, sequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewFileParameter的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <returns> RptViewFileParameter的总记录数</returns>
		public int QueryRptViewFileParameterCount( string reportID, decimal sequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVFILEPARAM where 1=1 and RPTID like '{0}%'  and SEQ like '{1}%' " , reportID, sequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewFileParameter
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewFileParameter数组</returns>
		public object[] QueryRptViewFileParameter( string reportID, decimal sequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewFileParameter), new PagerCondition(string.Format("select {0} from TBLRPTVFILEPARAM where 1=1 and RPTID like '{1}%'  and SEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewFileParameter)) , reportID, sequence), "RPTID,SEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewFileParameter
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewFileParameter的总记录数</returns>
		public object[] GetAllRptViewFileParameter()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewFileParameter), new SQLCondition(string.Format("select {0} from TBLRPTVFILEPARAM order by RPTID,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewFileParameter)))));
		}


		#endregion

		#region RptViewFilterUI
		/// <summary>
		/// 过滤栏位界面
		/// </summary>
		public RptViewFilterUI CreateNewRptViewFilterUI()
		{
			return new RptViewFilterUI();
		}

		public void AddRptViewFilterUI( RptViewFilterUI rptViewFilterUI)
		{
			this._helper.AddDomainObject( rptViewFilterUI );
		}

		public void UpdateRptViewFilterUI(RptViewFilterUI rptViewFilterUI)
		{
			this._helper.UpdateDomainObject( rptViewFilterUI );
		}

		public void DeleteRptViewFilterUI(RptViewFilterUI rptViewFilterUI)
		{
			this._helper.DeleteDomainObject( rptViewFilterUI );
		}

		public void DeleteRptViewFilterUI(RptViewFilterUI[] rptViewFilterUI)
		{
			this._helper.DeleteDomainObject( rptViewFilterUI );
		}

		public object GetRptViewFilterUI( string reportID, decimal sequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewFilterUI), new object[]{ reportID, sequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewFilterUI的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <returns> RptViewFilterUI的总记录数</returns>
		public int QueryRptViewFilterUICount( string reportID, decimal sequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVFILTERUI where 1=1 and RPTID like '{0}%'  and SEQ like '{1}%' " , reportID, sequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewFilterUI
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewFilterUI数组</returns>
		public object[] QueryRptViewFilterUI( string reportID, decimal sequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewFilterUI), new PagerCondition(string.Format("select {0} from TBLRPTVFILTERUI where 1=1 and RPTID like '{1}%'  and SEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewFilterUI)) , reportID, sequence), "RPTID,SEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewFilterUI
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewFilterUI的总记录数</returns>
		public object[] GetAllRptViewFilterUI()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewFilterUI), new SQLCondition(string.Format("select {0} from TBLRPTVFILTERUI order by RPTID,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewFilterUI)))));
		}


		#endregion

		#region RptViewGridColumn
		/// <summary>
		/// 报表显示栏位
		/// </summary>
		public RptViewGridColumn CreateNewRptViewGridColumn()
		{
			return new RptViewGridColumn();
		}

		public void AddRptViewGridColumn( RptViewGridColumn rptViewGridColumn)
		{
			this._helper.AddDomainObject( rptViewGridColumn );
		}

		public void UpdateRptViewGridColumn(RptViewGridColumn rptViewGridColumn)
		{
			this._helper.UpdateDomainObject( rptViewGridColumn );
		}

		public void DeleteRptViewGridColumn(RptViewGridColumn rptViewGridColumn)
		{
			this._helper.DeleteDomainObject( rptViewGridColumn );
		}

		public void DeleteRptViewGridColumn(RptViewGridColumn[] rptViewGridColumn)
		{
			this._helper.DeleteDomainObject( rptViewGridColumn );
		}

		public object GetRptViewGridColumn( string reportID, decimal displaySequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewGridColumn), new object[]{ reportID, displaySequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewGridColumn的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="displaySequence">DisplaySequence，模糊查询</param>
		/// <returns> RptViewGridColumn的总记录数</returns>
		public int QueryRptViewGridColumnCount( string reportID, decimal displaySequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVGRIDCOLUMN where 1=1 and RPTID like '{0}%'  and DISPLAYSEQ like '{1}%' " , reportID, displaySequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewGridColumn
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="displaySequence">DisplaySequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewGridColumn数组</returns>
		public object[] QueryRptViewGridColumn( string reportID, decimal displaySequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridColumn), new PagerCondition(string.Format("select {0} from TBLRPTVGRIDCOLUMN where 1=1 and RPTID like '{1}%'  and DISPLAYSEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridColumn)) , reportID, displaySequence), "RPTID,DISPLAYSEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewGridColumn
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewGridColumn的总记录数</returns>
		public object[] GetAllRptViewGridColumn()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridColumn), new SQLCondition(string.Format("select {0} from TBLRPTVGRIDCOLUMN order by RPTID,DISPLAYSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridColumn)))));
		}


		#endregion

		#region RptViewGridDataFormat
		/// <summary>
		/// 报表显示栏位
		/// </summary>
		public RptViewGridDataFormat CreateNewRptViewGridDataFormat()
		{
			return new RptViewGridDataFormat();
		}

		public void AddRptViewGridDataFormat( RptViewGridDataFormat rptViewGridDataFormat)
		{
			this._helper.AddDomainObject( rptViewGridDataFormat );
		}

		public void UpdateRptViewGridDataFormat(RptViewGridDataFormat rptViewGridDataFormat)
		{
			this._helper.UpdateDomainObject( rptViewGridDataFormat );
		}

		public void DeleteRptViewGridDataFormat(RptViewGridDataFormat rptViewGridDataFormat)
		{
			this._helper.DeleteDomainObject( rptViewGridDataFormat );
		}

		public void DeleteRptViewGridDataFormat(RptViewGridDataFormat[] rptViewGridDataFormat)
		{
			this._helper.DeleteDomainObject( rptViewGridDataFormat );
		}

		public object GetRptViewGridDataFormat( string reportID, string columnName, string styleType, decimal groupSequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewGridDataFormat), new object[]{ reportID, columnName, styleType, groupSequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewGridDataFormat的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="columnName">ColumnName，模糊查询</param>
		/// <param name="styleType">StyleType，模糊查询</param>
		/// <param name="groupSequence">GroupSequence，模糊查询</param>
		/// <returns> RptViewGridDataFormat的总记录数</returns>
		public int QueryRptViewGridDataFormatCount( string reportID, string columnName, string styleType, decimal groupSequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVGRIDDATAFMT where 1=1 and RPTID like '{0}%'  and COLUMNNAME like '{1}%'  and STYLETYPE like '{2}%'  and GRPSEQ like '{3}%' " , reportID, columnName, styleType, groupSequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewGridDataFormat
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="columnName">ColumnName，模糊查询</param>
		/// <param name="styleType">StyleType，模糊查询</param>
		/// <param name="groupSequence">GroupSequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewGridDataFormat数组</returns>
		public object[] QueryRptViewGridDataFormat( string reportID, string columnName, string styleType, decimal groupSequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridDataFormat), new PagerCondition(string.Format("select {0} from TBLRPTVGRIDDATAFMT where 1=1 and RPTID like '{1}%'  and COLUMNNAME like '{2}%'  and STYLETYPE like '{3}%'  and GRPSEQ like '{4}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridDataFormat)) , reportID, columnName, styleType, groupSequence), "RPTID,COLUMNNAME,STYLETYPE,GRPSEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewGridDataFormat
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewGridDataFormat的总记录数</returns>
		public object[] GetAllRptViewGridDataFormat()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridDataFormat), new SQLCondition(string.Format("select {0} from TBLRPTVGRIDDATAFMT order by RPTID,COLUMNNAME,STYLETYPE,GRPSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridDataFormat)))));
		}


		#endregion

		#region RptViewGridDataStyle
		/// <summary>
		/// 报表显示栏位
		/// </summary>
		public RptViewGridDataStyle CreateNewRptViewGridDataStyle()
		{
			return new RptViewGridDataStyle();
		}

		public void AddRptViewGridDataStyle( RptViewGridDataStyle rptViewGridDataStyle)
		{
			this._helper.AddDomainObject( rptViewGridDataStyle );
		}

		public void UpdateRptViewGridDataStyle(RptViewGridDataStyle rptViewGridDataStyle)
		{
			this._helper.UpdateDomainObject( rptViewGridDataStyle );
		}

		public void DeleteRptViewGridDataStyle(RptViewGridDataStyle rptViewGridDataStyle)
		{
			this._helper.DeleteDomainObject( rptViewGridDataStyle );
		}

		public void DeleteRptViewGridDataStyle(RptViewGridDataStyle[] rptViewGridDataStyle)
		{
			this._helper.DeleteDomainObject( rptViewGridDataStyle );
		}

		public object GetRptViewGridDataStyle( string reportID )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewGridDataStyle), new object[]{ reportID });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewGridDataStyle的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <returns> RptViewGridDataStyle的总记录数</returns>
		public int QueryRptViewGridDataStyleCount( string reportID)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVGRIDDATASTYLE where 1=1 and RPTID like '{0}%' " , reportID)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewGridDataStyle
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewGridDataStyle数组</returns>
		public object[] QueryRptViewGridDataStyle( string reportID, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridDataStyle), new PagerCondition(string.Format("select {0} from TBLRPTVGRIDDATASTYLE where 1=1 and RPTID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridDataStyle)) , reportID), "RPTID", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewGridDataStyle
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewGridDataStyle的总记录数</returns>
		public object[] GetAllRptViewGridDataStyle()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridDataStyle), new SQLCondition(string.Format("select {0} from TBLRPTVGRIDDATASTYLE order by RPTID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridDataStyle)))));
		}


		#endregion

		#region RptViewGridFilter
		/// <summary>
		/// 报表分组设定
		/// </summary>
		public RptViewGridFilter CreateNewRptViewGridFilter()
		{
			return new RptViewGridFilter();
		}

		public void AddRptViewGridFilter( RptViewGridFilter rptViewGridFilter)
		{
			this._helper.AddDomainObject( rptViewGridFilter );
		}

		public void UpdateRptViewGridFilter(RptViewGridFilter rptViewGridFilter)
		{
			this._helper.UpdateDomainObject( rptViewGridFilter );
		}

		public void DeleteRptViewGridFilter(RptViewGridFilter rptViewGridFilter)
		{
			this._helper.DeleteDomainObject( rptViewGridFilter );
		}

		public void DeleteRptViewGridFilter(RptViewGridFilter[] rptViewGridFilter)
		{
			this._helper.DeleteDomainObject( rptViewGridFilter );
		}

		public object GetRptViewGridFilter( string reportID, decimal filterSequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewGridFilter), new object[]{ reportID, filterSequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewGridFilter的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="filterSequence">FilterSequence，模糊查询</param>
		/// <returns> RptViewGridFilter的总记录数</returns>
		public int QueryRptViewGridFilterCount( string reportID, decimal filterSequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVGRIDFLT where 1=1 and RPTID like '{0}%'  and FLTSEQ like '{1}%' " , reportID, filterSequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewGridFilter
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="filterSequence">FilterSequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewGridFilter数组</returns>
		public object[] QueryRptViewGridFilter( string reportID, decimal filterSequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridFilter), new PagerCondition(string.Format("select {0} from TBLRPTVGRIDFLT where 1=1 and RPTID like '{1}%'  and FLTSEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridFilter)) , reportID, filterSequence), "RPTID,FLTSEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewGridFilter
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewGridFilter的总记录数</returns>
		public object[] GetAllRptViewGridFilter()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridFilter), new SQLCondition(string.Format("select {0} from TBLRPTVGRIDFLT order by RPTID,FLTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridFilter)))));
		}


		#endregion

		#region RptViewGridGroup
		/// <summary>
		/// 报表分组设定
		/// </summary>
		public RptViewGridGroup CreateNewRptViewGridGroup()
		{
			return new RptViewGridGroup();
		}

		public void AddRptViewGridGroup( RptViewGridGroup rptViewGridGroup)
		{
			this._helper.AddDomainObject( rptViewGridGroup );
		}

		public void UpdateRptViewGridGroup(RptViewGridGroup rptViewGridGroup)
		{
			this._helper.UpdateDomainObject( rptViewGridGroup );
		}

		public void DeleteRptViewGridGroup(RptViewGridGroup rptViewGridGroup)
		{
			this._helper.DeleteDomainObject( rptViewGridGroup );
		}

		public void DeleteRptViewGridGroup(RptViewGridGroup[] rptViewGridGroup)
		{
			this._helper.DeleteDomainObject( rptViewGridGroup );
		}

		public object GetRptViewGridGroup( string reportID, decimal groupSequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewGridGroup), new object[]{ reportID, groupSequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewGridGroup的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="groupSequence">GroupSequence，模糊查询</param>
		/// <returns> RptViewGridGroup的总记录数</returns>
		public int QueryRptViewGridGroupCount( string reportID, decimal groupSequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVGRIDGRP where 1=1 and RPTID like '{0}%'  and GRPSEQ like '{1}%' " , reportID, groupSequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewGridGroup
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="groupSequence">GroupSequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewGridGroup数组</returns>
		public object[] QueryRptViewGridGroup( string reportID, decimal groupSequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridGroup), new PagerCondition(string.Format("select {0} from TBLRPTVGRIDGRP where 1=1 and RPTID like '{1}%'  and GRPSEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridGroup)) , reportID, groupSequence), "RPTID,GRPSEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewGridGroup
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewGridGroup的总记录数</returns>
		public object[] GetAllRptViewGridGroup()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridGroup), new SQLCondition(string.Format("select {0} from TBLRPTVGRIDGRP order by RPTID,GRPSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridGroup)))));
		}


		#endregion

		#region RptViewGridGroupTotal
		/// <summary>
		/// 报表分组设定
		/// </summary>
		public RptViewGridGroupTotal CreateNewRptViewGridGroupTotal()
		{
			return new RptViewGridGroupTotal();
		}

		public void AddRptViewGridGroupTotal( RptViewGridGroupTotal rptViewGridGroupTotal)
		{
			this._helper.AddDomainObject( rptViewGridGroupTotal );
		}

		public void UpdateRptViewGridGroupTotal(RptViewGridGroupTotal rptViewGridGroupTotal)
		{
			this._helper.UpdateDomainObject( rptViewGridGroupTotal );
		}

		public void DeleteRptViewGridGroupTotal(RptViewGridGroupTotal rptViewGridGroupTotal)
		{
			this._helper.DeleteDomainObject( rptViewGridGroupTotal );
		}

		public void DeleteRptViewGridGroupTotal(RptViewGridGroupTotal[] rptViewGridGroupTotal)
		{
			this._helper.DeleteDomainObject( rptViewGridGroupTotal );
		}

		public object GetRptViewGridGroupTotal( string reportID, decimal groupSequence, string columnName )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewGridGroupTotal), new object[]{ reportID, groupSequence, columnName });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewGridGroupTotal的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="groupSequence">GroupSequence，模糊查询</param>
		/// <param name="columnName">ColumnName，模糊查询</param>
		/// <returns> RptViewGridGroupTotal的总记录数</returns>
		public int QueryRptViewGridGroupTotalCount( string reportID, decimal groupSequence, string columnName)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVGRIDGRPTOTAL where 1=1 and RPTID like '{0}%'  and GRPSEQ like '{1}%'  and COLUMNNAME like '{2}%' " , reportID, groupSequence, columnName)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewGridGroupTotal
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="groupSequence">GroupSequence，模糊查询</param>
		/// <param name="columnName">ColumnName，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewGridGroupTotal数组</returns>
		public object[] QueryRptViewGridGroupTotal( string reportID, decimal groupSequence, string columnName, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridGroupTotal), new PagerCondition(string.Format("select {0} from TBLRPTVGRIDGRPTOTAL where 1=1 and RPTID like '{1}%'  and GRPSEQ like '{2}%'  and COLUMNNAME like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridGroupTotal)) , reportID, groupSequence, columnName), "RPTID,GRPSEQ,COLUMNNAME", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewGridGroupTotal
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewGridGroupTotal的总记录数</returns>
		public object[] GetAllRptViewGridGroupTotal()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewGridGroupTotal), new SQLCondition(string.Format("select {0} from TBLRPTVGRIDGRPTOTAL order by RPTID,GRPSEQ,COLUMNNAME", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewGridGroupTotal)))));
		}


		#endregion

		#region RptViewReportSecurity
		/// <summary>
		/// 报表权限
		/// </summary>
		public RptViewReportSecurity CreateNewRptViewReportSecurity()
		{
			return new RptViewReportSecurity();
		}

		public void AddRptViewReportSecurity( RptViewReportSecurity rptViewReportSecurity)
		{
			this._helper.AddDomainObject( rptViewReportSecurity );
		}

		public void UpdateRptViewReportSecurity(RptViewReportSecurity rptViewReportSecurity)
		{
			this._helper.UpdateDomainObject( rptViewReportSecurity );
		}

		public void DeleteRptViewReportSecurity(RptViewReportSecurity rptViewReportSecurity)
		{
			this._helper.DeleteDomainObject( rptViewReportSecurity );
		}

		public void DeleteRptViewReportSecurity(RptViewReportSecurity[] rptViewReportSecurity)
		{
			this._helper.DeleteDomainObject( rptViewReportSecurity );
		}

		public object GetRptViewReportSecurity( string reportID, decimal sequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewReportSecurity), new object[]{ reportID, sequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewReportSecurity的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <returns> RptViewReportSecurity的总记录数</returns>
		public int QueryRptViewReportSecurityCount( string reportID, decimal sequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVRPTSECURITY where 1=1 and RPTID like '{0}%'  and SEQ like '{1}%' " , reportID, sequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewReportSecurity
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewReportSecurity数组</returns>
		public object[] QueryRptViewReportSecurity( string reportID, decimal sequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewReportSecurity), new PagerCondition(string.Format("select {0} from TBLRPTVRPTSECURITY where 1=1 and RPTID like '{1}%'  and SEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewReportSecurity)) , reportID, sequence), "RPTID,SEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewReportSecurity
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewReportSecurity的总记录数</returns>
		public object[] GetAllRptViewReportSecurity()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewReportSecurity), new SQLCondition(string.Format("select {0} from TBLRPTVRPTSECURITY order by RPTID,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewReportSecurity)))));
		}


		#endregion

		#region RptViewReportStyle
		/// <summary>
		/// 报表样式
///
		/// </summary>
		public RptViewReportStyle CreateNewRptViewReportStyle()
		{
			return new RptViewReportStyle();
		}

		public void AddRptViewReportStyle( RptViewReportStyle rptViewReportStyle)
		{
			this._helper.AddDomainObject( rptViewReportStyle );
		}

		public void UpdateRptViewReportStyle(RptViewReportStyle rptViewReportStyle)
		{
			this._helper.UpdateDomainObject( rptViewReportStyle );
		}

		public void DeleteRptViewReportStyle(RptViewReportStyle rptViewReportStyle)
		{
			this._helper.DeleteDomainObject( rptViewReportStyle );
		}

		public void DeleteRptViewReportStyle(RptViewReportStyle[] rptViewReportStyle)
		{
			this._helper.DeleteDomainObject( rptViewReportStyle );
		}

		public object GetRptViewReportStyle( decimal styleID )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewReportStyle), new object[]{ styleID });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewReportStyle的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="styleID">StyleID，模糊查询</param>
		/// <returns> RptViewReportStyle的总记录数</returns>
		public int QueryRptViewReportStyleCount( decimal styleID)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVSTYLE where 1=1 and STYLEID like '{0}%' " , styleID)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewReportStyle
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="styleID">StyleID，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewReportStyle数组</returns>
		public object[] QueryRptViewReportStyle( decimal styleID, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewReportStyle), new PagerCondition(string.Format("select {0} from TBLRPTVSTYLE where 1=1 and STYLEID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewReportStyle)) , styleID), "STYLEID", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewReportStyle
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewReportStyle的总记录数</returns>
		public object[] GetAllRptViewReportStyle()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewReportStyle), new SQLCondition(string.Format("select {0} from TBLRPTVSTYLE order by STYLEID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewReportStyle)))));
		}


		#endregion

		#region RptViewReportStyleDetail
		/// <summary>
		/// 报表样式
///
		/// </summary>
		public RptViewReportStyleDetail CreateNewRptViewReportStyleDetail()
		{
			return new RptViewReportStyleDetail();
		}

		public void AddRptViewReportStyleDetail( RptViewReportStyleDetail rptViewReportStyleDetail)
		{
			this._helper.AddDomainObject( rptViewReportStyleDetail );
		}

		public void UpdateRptViewReportStyleDetail(RptViewReportStyleDetail rptViewReportStyleDetail)
		{
			this._helper.UpdateDomainObject( rptViewReportStyleDetail );
		}

		public void DeleteRptViewReportStyleDetail(RptViewReportStyleDetail rptViewReportStyleDetail)
		{
			this._helper.DeleteDomainObject( rptViewReportStyleDetail );
		}

		public void DeleteRptViewReportStyleDetail(RptViewReportStyleDetail[] rptViewReportStyleDetail)
		{
			this._helper.DeleteDomainObject( rptViewReportStyleDetail );
		}

		public object GetRptViewReportStyleDetail( decimal styleID, string styleType )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewReportStyleDetail), new object[]{ styleID, styleType });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewReportStyleDetail的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="styleID">StyleID，模糊查询</param>
		/// <param name="styleType">StyleType，模糊查询</param>
		/// <returns> RptViewReportStyleDetail的总记录数</returns>
		public int QueryRptViewReportStyleDetailCount( decimal styleID, string styleType)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVSTYLEDTL where 1=1 and STYLEID like '{0}%'  and STYLETYPE like '{1}%' " , styleID, styleType)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewReportStyleDetail
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="styleID">StyleID，模糊查询</param>
		/// <param name="styleType">StyleType，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewReportStyleDetail数组</returns>
		public object[] QueryRptViewReportStyleDetail( decimal styleID, string styleType, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewReportStyleDetail), new PagerCondition(string.Format("select {0} from TBLRPTVSTYLEDTL where 1=1 and STYLEID like '{1}%'  and STYLETYPE like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewReportStyleDetail)) , styleID, styleType), "STYLEID,STYLETYPE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewReportStyleDetail
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewReportStyleDetail的总记录数</returns>
		public object[] GetAllRptViewReportStyleDetail()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewReportStyleDetail), new SQLCondition(string.Format("select {0} from TBLRPTVSTYLEDTL order by STYLEID,STYLETYPE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewReportStyleDetail)))));
		}


		#endregion

		#region RptViewUserDefault
		/// <summary>
		/// 用户订阅
		/// </summary>
		public RptViewUserDefault CreateNewRptViewUserDefault()
		{
			return new RptViewUserDefault();
		}

		public void AddRptViewUserDefault( RptViewUserDefault rptViewUserDefault)
		{
			this._helper.AddDomainObject( rptViewUserDefault );
		}

		public void UpdateRptViewUserDefault(RptViewUserDefault rptViewUserDefault)
		{
			this._helper.UpdateDomainObject( rptViewUserDefault );
		}

		public void DeleteRptViewUserDefault(RptViewUserDefault rptViewUserDefault)
		{
			this._helper.DeleteDomainObject( rptViewUserDefault );
		}

		public void DeleteRptViewUserDefault(RptViewUserDefault[] rptViewUserDefault)
		{
			this._helper.DeleteDomainObject( rptViewUserDefault );
		}

		public object GetRptViewUserDefault( string userCode )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewUserDefault), new object[]{ userCode });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewUserDefault的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userCode">UserCode，模糊查询</param>
		/// <returns> RptViewUserDefault的总记录数</returns>
		public int QueryRptViewUserDefaultCount( string userCode)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVUSERDFT where 1=1 and USERCODE like '{0}%' " , userCode)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewUserDefault
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userCode">UserCode，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewUserDefault数组</returns>
		public object[] QueryRptViewUserDefault( string userCode, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewUserDefault), new PagerCondition(string.Format("select {0} from TBLRPTVUSERDFT where 1=1 and USERCODE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewUserDefault)) , userCode), "USERCODE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewUserDefault
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewUserDefault的总记录数</returns>
		public object[] GetAllRptViewUserDefault()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewUserDefault), new SQLCondition(string.Format("select {0} from TBLRPTVUSERDFT order by USERCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewUserDefault)))));
		}


		#endregion

		#region RptViewUserSubscription
		/// <summary>
		/// 用户订阅
		/// </summary>
		public RptViewUserSubscription CreateNewRptViewUserSubscription()
		{
			return new RptViewUserSubscription();
		}

		public void AddRptViewUserSubscription( RptViewUserSubscription rptViewUserSubscription)
		{
			this._helper.AddDomainObject( rptViewUserSubscription );
		}

		public void UpdateRptViewUserSubscription(RptViewUserSubscription rptViewUserSubscription)
		{
			this._helper.UpdateDomainObject( rptViewUserSubscription );
		}

		public void DeleteRptViewUserSubscription(RptViewUserSubscription rptViewUserSubscription)
		{
			this._helper.DeleteDomainObject( rptViewUserSubscription );
		}

		public void DeleteRptViewUserSubscription(RptViewUserSubscription[] rptViewUserSubscription)
		{
			this._helper.DeleteDomainObject( rptViewUserSubscription );
		}

		public object GetRptViewUserSubscription( string userCode, string reportID, decimal sequence )
		{
			return this.DataProvider.CustomSearch(typeof(RptViewUserSubscription), new object[]{ userCode, reportID, sequence });
		}

		/// <summary>
		/// ** 功能描述:	查询RptViewUserSubscription的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userCode">UserCode，模糊查询</param>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <returns> RptViewUserSubscription的总记录数</returns>
		public int QueryRptViewUserSubscriptionCount( string userCode, string reportID, decimal sequence)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVUSERSUBSCR where 1=1 and USERCODE like '{0}%'  and RPTID like '{1}%'  and SEQ like '{2}%' " , userCode, reportID, sequence)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询RptViewUserSubscription
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userCode">UserCode，模糊查询</param>
		/// <param name="reportID">ReportID，模糊查询</param>
		/// <param name="sequence">Sequence，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> RptViewUserSubscription数组</returns>
		public object[] QueryRptViewUserSubscription( string userCode, string reportID, decimal sequence, int inclusive, int exclusive )
		{
			return this.DataProvider.CustomQuery(typeof(RptViewUserSubscription), new PagerCondition(string.Format("select {0} from TBLRPTVUSERSUBSCR where 1=1 and USERCODE like '{1}%'  and RPTID like '{2}%'  and SEQ like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewUserSubscription)) , userCode, reportID, sequence), "USERCODE,RPTID,SEQ", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的RptViewUserSubscription
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2007-7-17 9:17:33
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>RptViewUserSubscription的总记录数</returns>
		public object[] GetAllRptViewUserSubscription()
		{
			return this.DataProvider.CustomQuery(typeof(RptViewUserSubscription), new SQLCondition(string.Format("select {0} from TBLRPTVUSERSUBSCR order by USERCODE,RPTID,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewUserSubscription)))));
		}


		#endregion

	}
}

