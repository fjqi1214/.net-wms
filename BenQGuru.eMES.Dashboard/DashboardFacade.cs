using System;

using UserControl;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.MutiLanguage; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper; 

namespace BenQGuru.eMES.Dashboard
{
	/// <summary>
	/// DashboardFactory 的摘要说明。
	/// </summary>
	public class DashboardFacade
	{
		public const string  SOCR = "SOCR";

		public const string ByMonth = "MONTH";
		public const string ByDay = "DAY";
		public const string ByWeek = "WEEK";
		//public static string  SOCR = "SOCR";

		private  IDomainDataProvider _domainDataProvider = null;
		public IDomainDataProvider DataProvider
		{
			get
			{
				//				if (_domainDataProvider == null)
				//				{
				//					_domainDataProvider = DomainDataProviderManager.DomainDataProvider(); 
				//				}

				return _domainDataProvider;
			}	
		}

		public DashboardFacade(IDomainDataProvider dataProvider)
		{
			_domainDataProvider = dataProvider;
		}


		#region 出货及时率考核
		public string getSOCR(
			string modelCode
			,string itemCode
			,string orderCode
			,string frmDate
			,string toDate
			,string frmMonth
			,string toMonth
			,string frmWeek
			,string toWeek
			,string statisticlatitude)
		{
			string strReturn = String.Empty;

			#region Contract Total SQL String

			string sqlCondition = String.Empty;

			string sqlMain = @"SELECT   b.*,
         CASE 
             WHEN a.planshipdate >= a.actshipdate AND a.actshipdate IS NOT NULL 
                THEN 'OnTimeShip' 
             WHEN a.planshipdate < a.actshipdate AND a.actshipdate IS NOT NULL  
                THEN 'NotOnTimeShip' 
             WHEN a.actshipdate IS NULL 
                THEN 'NotOnTimeShip' 
          END AS shiptype,
         a.planshipdate, a.actshipdate,
         CASE
            WHEN a.planshipdate = 99991231
               THEN '0'
            WHEN LENGTH (a.planshipdate) < 8
               THEN '0'
            ELSE TO_CHAR (TO_DATE (a.planshipdate, 'yyyyMMdd'),
                          'ww'
                         )
         END AS planshipweek,
         CASE
            WHEN a.planshipdate = 99991231
               THEN '0'
            WHEN LENGTH (a.planshipdate) < 8
               THEN '0'
            ELSE TO_CHAR (TO_DATE (a.planshipdate, 'yyyyMMdd'),
                          'MM'
                         )
         END AS planshipmonth
    FROM tblorder a, tblorderdetail b
   WHERE a.ordernumber = b.ordernumber {0}
ORDER BY a.planshipdate";

			//			string sqlMain = @"SELECT   shiptype, COUNT (shiptype) AS ordercount,
			//         COUNT (*) AS totalordercount, SUM (actqty) AS shipqty,
			//         SUM (planqty) AS totoalqty,
			//         ROUND (COUNT (shiptype) / COUNT (*), 2) * 100 AS PERCENT
			//    FROM (SELECT (CASE
			//                     WHEN a.planshipdate >= b.actdate
			//                          AND b.actdate IS NOT NULL
			//                        THEN " + MutiLanguages.ParserMessage("OnTimeShip") + @"      /*计划日期大于等于实际完成日期，及时出货*/
			//                     WHEN a.planshipdate < b.actdate AND b.actdate IS NOT NULL
			//                        THEN " + MutiLanguages.ParserMessage("NotOnTimeShip") + @"            /*计划日期小于完成日期，未及时出货*/
			//                     WHEN b.actdate IS NULL
			//                        THEN "+ MutiLanguages.ParserMessage("NotOnTimeShip") + @"            /*计划日期小于完成日期，未及时出货*/
			//                  END
			//                 ) AS shiptype,
			//                 a.planshipdate, b.*
			//            FROM tblorder a, tblorderdetail b
			//           WHERE a.ordernumber = b.ordernumber {0}) s
			//GROUP BY shiptype";

			//机种
			if(modelCode != null && modelCode != String.Empty)
			{
				sqlCondition += " and itemCode in (select itemcode from tblmodel2item where modelcode='" + modelCode + "'" + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")";
			}
			//产品
			if(itemCode != null && itemCode != String.Empty)
			{
				sqlCondition += " and itemCode = '" + itemCode + "'";
			}
			//订单号
			if(orderCode != null && orderCode != String.Empty)
			{
				sqlCondition += " and  OrderNumber = '" + orderCode + "'";
			}
			//开始日期（计划出货日期）
			if(frmDate != null && frmDate != String.Empty)
			{
				sqlCondition += " and A.PlanShipDate >= " + frmDate;
			}
			//结束日期（计划出货日期）
			if(toDate != null && toDate != String.Empty)
			{
				sqlCondition += " and A.PlanShipDate <= " + toDate;
			}
			//开始实际出货月份
			if(frmMonth != null && frmMonth != String.Empty)
			{
				sqlCondition += " and A.ActShipMonth >= " + frmMonth;
			}
			//截至实际出货月份
			if(toMonth != null && toMonth != String.Empty)
			{
				sqlCondition += " and A.ActShipMonth <= " + toMonth ;
			}
			//开始实际出货星期
			if(frmWeek != null && frmWeek != String.Empty)
			{
				sqlCondition += " and A.ActShipWeek <= " + frmWeek ;
			}
			//开始实际出货星期
			if(toWeek != null && toWeek != String.Empty)
			{
				sqlCondition += " and A.ActShipWeek <= " + toWeek;
			}

			string sql = String.Format(sqlMain,sqlCondition);
			#endregion

			object[] objs = DataProvider.CustomQuery(typeof(SOCRTOTALObject)
				,new SQLCondition(sql));

			System.Collections.Hashtable htTotal = new System.Collections.Hashtable();

			if(objs != null && objs.Length > 0)
			{
				foreach(SOCRTOTALObject obj in objs)
				{
					//处理Total Object
					if(htTotal.ContainsKey(obj.ItemType))
					{
						#region 已经存在Key=obj.ItemType的TotalObject对象
						TotalObject to = (TotalObject)htTotal[obj.ItemType];

						to.ItemType = obj.ItemType;
						
						to.OrderCount = (double)objs.Length;

						#region 处理DateShip

						if(to.DailyShips == null)
						{
							to.DailyShips = new System.Collections.Hashtable();

							DailyShip ds = new DailyShip();
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.ActShipDate = obj.PlanShipDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.ActShipDate = obj.PlanShipWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.ActShipDate = obj.PlanShipMonth;
									break;
								}
							}
							ds.OrderCount = 1;	

							//							if(obj.ItemType == TotalObject.OnTimeShip)
							//							{
								
							to.ShippedOrderCount = to.ShippedOrderCount  + 1;
							//							}
							//处理ShipDetails
							if(ds.ShipDetails == null)
							{
								ds.ShipDetails = new System.Collections.ArrayList();
							}

							ds.ShipDetails.Add(obj);

							to.DailyShips.Add(ds.ActShipDate + obj.ItemType,ds);
						}
						else
						{
							string byObject = String.Empty;
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									byObject = obj.PlanShipDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									byObject = obj.PlanShipWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									byObject = obj.PlanShipMonth;
									break;
								}
							}

							if(to.DailyShips.ContainsKey(byObject + obj.ItemType))
							{
								DailyShip ds = (DailyShip)to.DailyShips[byObject + obj.ItemType];
								//Laws Lu,2006/04/26 允许按不同纬度统计
								switch(statisticlatitude)
								{
									case DashboardFacade.ByDay:
									{
										ds.ActShipDate = obj.PlanShipDate;
										break;
									}
									case DashboardFacade.ByWeek:
									{
										ds.ActShipDate = obj.PlanShipWeek;
										break;
									}
									case DashboardFacade.ByMonth:
									{
										ds.ActShipDate = obj.PlanShipMonth;
										break;
									}
								}
								ds.OrderCount = ds.OrderCount  + 1;

								//								if(obj.ItemType == TotalObject.OnTimeShip)
								//								{
								to.ShippedOrderCount = to.ShippedOrderCount  + 1;
								//								}
								//处理ShipDetails
								if(ds.ShipDetails == null)
								{
									ds.ShipDetails = new System.Collections.ArrayList();
								}

								ds.ShipDetails.Add(obj);

								to.DailyShips[ds.ActShipDate + obj.ItemType] = ds;
							}
							else
							{
								DailyShip ds = new DailyShip();//(DailyShip)to.DailyShips[ds.ActShipDate + obj.ItemType];

								ds.OrderCount = 1;
								//Laws Lu,2006/04/26 允许按不同纬度统计
								switch(statisticlatitude)
								{
									case DashboardFacade.ByDay:
									{
										ds.ActShipDate = obj.PlanShipDate;
										break;
									}
									case DashboardFacade.ByWeek:
									{
										ds.ActShipDate = obj.PlanShipWeek;
										break;
									}
									case DashboardFacade.ByMonth:
									{
										ds.ActShipDate = obj.PlanShipMonth;
										break;
									}
								}
								//								if(to.ItemType == TotalObject.OnTimeShip)
								//								{
								to.ShippedOrderCount = to.ShippedOrderCount  + 1;
								//								}
								//处理ShipDetails
								if(ds.ShipDetails == null)
								{
									ds.ShipDetails = new System.Collections.ArrayList();
								}

								ds.ShipDetails.Add(obj);

								to.DailyShips.Add(ds.ActShipDate + obj.ItemType,ds);
							}
						}

						#endregion

						to.Scale = System.Math.Round(to.ShippedOrderCount/to.OrderCount,2) * 100;

						htTotal[obj.ItemType] = to;
						#endregion
					}
					else
					{
						#region 不存在时的处理
						TotalObject to = new TotalObject();
					
						to.ItemType = obj.ItemType;
						to.OrderCount = (double)objs.Length;
						
						#region 处理DateShip

						if(to.DailyShips == null)
						{
							to.DailyShips = new System.Collections.Hashtable();

							DailyShip ds = new DailyShip();
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.ActShipDate = obj.PlanShipDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.ActShipDate = obj.PlanShipWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.ActShipDate = obj.PlanShipMonth;
									break;
								}
							}
							ds.OrderCount = 1;	

							//							if(obj.ItemType == TotalObject.OnTimeShip)
							//							{
							to.ShippedOrderCount = to.ShippedOrderCount  + 1;
							//							}
							//处理ShipDetails
							if(ds.ShipDetails == null)
							{
								ds.ShipDetails = new System.Collections.ArrayList();
							}

							ds.ShipDetails.Add(obj);

							to.DailyShips.Add(ds.ActShipDate + obj.ItemType,ds);
						}
						else
						{
							string byObject = String.Empty;
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									byObject = obj.PlanShipDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									byObject = obj.PlanShipWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									byObject = obj.PlanShipMonth;
									break;
								}
							}


							DailyShip ds = (DailyShip)to.DailyShips[byObject];
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.ActShipDate = obj.PlanShipDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.ActShipDate = obj.PlanShipWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.ActShipDate = obj.PlanShipMonth;
									break;
								}
							}
							ds.OrderCount = ds.OrderCount  + 1;

							//							if(to.ItemType == TotalObject.OnTimeShip)
							//							{
							to.ShippedOrderCount = to.ShippedOrderCount  + 1;
							//							}
							//处理ShipDetails
							if(ds.ShipDetails == null)
							{
								ds.ShipDetails = new System.Collections.ArrayList();
							}

							ds.ShipDetails.Add(obj);

							to.DailyShips.Add(ds.ActShipDate + obj.ItemType,ds);
						}

						#endregion

						to.Scale = System.Math.Round(to.ShippedOrderCount/to.OrderCount,2) * 100;

						htTotal.Add(obj.ItemType,to);
						#endregion
					}
				}
			}

			SOCRXmlBuilder xml = new SOCRXmlBuilder();
			xml.BeginBuildRoot();
			//写入汇总
			foreach(TotalObject to in htTotal.Values)
			{
				xml.BeginBuildItem(to.ItemType,to.ShippedOrderCount.ToString(),to.OrderCount.ToString(),to.Scale.ToString());
				//写入天数据
				foreach(DailyShip ds in to.DailyShips.Values)
				{
					xml.BeginBuildDateShip(ds.ActShipDate,ds.OrderCount.ToString());
					//写入月数据
					foreach(SOCRTOTALObject obj in ds.ShipDetails)
					{
						xml.BuildDateShipDetail(obj.ItemCode,obj.OrderNO,obj.PartnerCode,obj.PlanShipDate,obj.ActDate,obj.ActQty);
					}
					//Flex的bug，单条数据无法显示
					if(ds.ShipDetails.Count == 1)
					{
						xml.BuildDateShipDetail(String.Empty,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty);
					}

					xml.EndBuildDateShip();
				}
				//Flex的bug，单条数据无法显示
				if(to.DailyShips.Values.Count == 1)
				{
					xml.BeginBuildDateShip(String.Empty,String.Empty);
					xml.EndBuildDateShip();
				}

				xml.EndBuildItem();
			}
			//Flex的bug，单条数据无法显示
			if(htTotal.Values.Count == 1)
			{	
				xml.BeginBuildItem(String.Empty,String.Empty,String.Empty,String.Empty);
				xml.EndBuildItem();
			}

			xml.EndBuildRoot();

			strReturn = xml.XmlContent.ToString();

			return strReturn;
		}
		#endregion

		#region 工单及时完成率
		public string getMPOCR(
			string modelCode
			,string itemCode
			,string moCode
			,string frmDate
			,string toDate
			,string frmMonth
			,string toMonth
			,string frmWeek
			,string toWeek
			,string statisticlatitude)
		{
			string strReturn = String.Empty;

			#region Contract Total SQL String

			string sqlCondition = String.Empty;

			string sqlMain = @"/* Formatted on 2006/04/27 16:01 (Formatter Plus v4.8.6) */
SELECT *
  FROM (SELECT b.*,
               CASE
                  WHEN b.moplanenddate = 99991231
                     THEN '0'
                  WHEN LENGTH (b.moplanenddate) < 8
                     THEN '0'
                  ELSE TO_CHAR (TO_DATE (b.moplanenddate, 'yyyyMMdd'),
                                'ww'
                               )
               END AS planendweek,
               CASE
                  WHEN b.moplanenddate = 99991231
                     THEN '0'
                  WHEN LENGTH (b.moplanenddate) < 8
                     THEN '0'
                  ELSE TO_CHAR (TO_DATE (b.moplanenddate, 'yyyyMMdd'),
                                'MM'
                               )
               END AS planendmonth,
               CASE
                  WHEN b.moplanenddate >= b.moactenddate
                     THEN 'OnTimeProduct'
                  ELSE 'NotOnTimeProduct'
               END AS moprodcutstatus
          FROM tblmo b) a
 WHERE a.planendweek <> 0 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {0} order by moplanenddate";

			//机种
			if(modelCode != null && modelCode != String.Empty)
			{
				sqlCondition += " and itemCode in (select itemcode from tblmodel2item where modelcode='" + modelCode + "'" + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")";
			}
			//产品
			if(itemCode != null && itemCode != String.Empty)
			{
				sqlCondition += " and itemCode = '" + itemCode + "'";
			}
			//订单号
			if(moCode != null && moCode != String.Empty)
			{
				sqlCondition += " and  mocode = '" + moCode + "'";
			}
			//开始日期（计划出货日期）
			if(frmDate != null && frmDate != String.Empty)
			{
				sqlCondition += " and A.MoPlanEndDate >= " + frmDate;
			}
			//结束日期（计划出货日期）
			if(toDate != null && toDate != String.Empty)
			{
				sqlCondition += " and A.MoPlanEndDate <= " + toDate;
			}
			//开始实际出货月份
			if(frmMonth != null && frmMonth != String.Empty)
			{
				sqlCondition += " and A.ActEndMonth >= " + frmMonth;
			}
			//截至实际出货月份
			if(toMonth != null && toMonth != String.Empty)
			{
				sqlCondition += " and A.ActEndMonth <= " + toMonth ;
			}
			//开始实际出货星期
			if(frmWeek != null && frmWeek != String.Empty)
			{
				sqlCondition += " and A.ActEndWeek <= " + frmWeek ;
			}
			//开始实际出货星期
			if(toWeek != null && toWeek != String.Empty)
			{
				sqlCondition += " and A.ActEndWeek <= " + toWeek;
			}

			string sql = String.Format(sqlMain,sqlCondition);
			#endregion

			object[] objs = DataProvider.CustomQuery(typeof(MPOCRObject)
				,new SQLCondition(sql));

			System.Collections.Hashtable htTotal = new System.Collections.Hashtable();

			if(objs != null && objs.Length > 0)
			{
				foreach(MPOCRObject obj in objs)
				{
					//处理Total Object
					if(htTotal.ContainsKey(obj.MoProductStatus))
					{
						#region 已经存在Key=obj.MoProductStatus的MoObject对象
						MoObject to = (MoObject)htTotal[obj.MoProductStatus];

						to.MoProductStatus = obj.MoProductStatus;
						
						to.TotalMoCount = (double)objs.Length;

						#region 处理DailyMo

						if(to.DailyMos == null)
						{
							to.DailyMos = new System.Collections.SortedList();

							DailyMo ds = new DailyMo();
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.CountScale = obj.PlanEndDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.CountScale = obj.PlanEndWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.CountScale = obj.PlanEndMonth;
									break;
								}
							}
							ds.MoCount = 1;	

							//							if(obj.MoProductStatus == MoObject.OnTimeShip)
							//							{
								
							to.MoCount = to.MoCount  + 1;
							//							}
							//处理ShipDetails
							if(ds.MoDetails == null)
							{
								ds.MoDetails = new System.Collections.ArrayList();
							}

							ds.MoDetails.Add(obj);

							to.DailyMos.Add(ds.CountScale + obj.MoProductStatus,ds);
						}
						else
						{
							string byObject = String.Empty;
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									byObject = obj.PlanEndDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									byObject = obj.PlanEndWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									byObject = obj.PlanEndMonth;
									break;
								}
							}

							if(to.DailyMos.ContainsKey(byObject + obj.MoProductStatus))
							{
								DailyMo ds = (DailyMo)to.DailyMos[byObject + obj.MoProductStatus];
								//Laws Lu,2006/04/26 允许按不同纬度统计
								switch(statisticlatitude)
								{
									case DashboardFacade.ByDay:
									{
										ds.CountScale = obj.PlanEndDate;
										break;
									}
									case DashboardFacade.ByWeek:
									{
										ds.CountScale = obj.PlanEndWeek;
										break;
									}
									case DashboardFacade.ByMonth:
									{
										ds.CountScale = obj.PlanEndMonth;
										break;
									}
								}
								ds.MoCount = ds.MoCount  + 1;

								//								if(obj.MoProductStatus == MoObject.OnTimeShip)
								//								{
								to.MoCount = to.MoCount  + 1;
								//								}
								//处理ShipDetails
								if(ds.MoDetails == null)
								{
									ds.MoDetails = new System.Collections.ArrayList();
								}

								ds.MoDetails.Add(obj);

								to.DailyMos[ds.CountScale + obj.MoProductStatus] = ds;
							}
							else
							{
								DailyMo ds = new DailyMo();//(DailyMo)to.DailyMos[ds.CountScale + obj.MoProductStatus];

								ds.MoCount = 1;
								//Laws Lu,2006/04/26 允许按不同纬度统计
								switch(statisticlatitude)
								{
									case DashboardFacade.ByDay:
									{
										ds.CountScale = obj.PlanEndDate;
										break;
									}
									case DashboardFacade.ByWeek:
									{
										ds.CountScale = obj.PlanEndWeek;
										break;
									}
									case DashboardFacade.ByMonth:
									{
										ds.CountScale = obj.PlanEndMonth;
										break;
									}
								}
								//								if(to.MoProductStatus == MoObject.OnTimeShip)
								//								{
								to.MoCount = to.MoCount  + 1;
								//								}
								//处理ShipDetails
								if(ds.MoDetails == null)
								{
									ds.MoDetails = new System.Collections.ArrayList();
								}

								ds.MoDetails.Add(obj);

								to.DailyMos.Add(ds.CountScale + obj.MoProductStatus,ds);
							}
						}

						#endregion

						to.Scale = System.Math.Round(to.MoCount/to.TotalMoCount,2) * 100;

						htTotal[obj.MoProductStatus] = to;
						#endregion
					}
					else
					{
						#region 不存在时的处理
						MoObject to = new MoObject();
					
						to.MoProductStatus = obj.MoProductStatus;
						to.TotalMoCount = (double)objs.Length;
						
						#region 处理DailyMo

						if(to.DailyMos == null)
						{
							to.DailyMos = new System.Collections.SortedList();

							DailyMo ds = new DailyMo();
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.CountScale = obj.PlanEndDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.CountScale = obj.PlanEndWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.CountScale = obj.PlanEndMonth;
									break;
								}
							}
							ds.MoCount = 1;	

							//							if(obj.MoProductStatus == MoObject.OnTimeShip)
							//							{
							to.MoCount = to.MoCount  + 1;
							//							}
							//处理ShipDetails
							if(ds.MoDetails == null)
							{
								ds.MoDetails = new System.Collections.ArrayList();
							}

							ds.MoDetails.Add(obj);

							to.DailyMos.Add(ds.CountScale + obj.MoProductStatus,ds);
						}
						else
						{
							string byObject = String.Empty;
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									byObject = obj.PlanEndDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									byObject = obj.PlanEndWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									byObject = obj.PlanEndMonth;
									break;
								}
							}


							DailyMo ds = (DailyMo)to.DailyMos[byObject];
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.CountScale = obj.PlanEndDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.CountScale = obj.PlanEndWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.CountScale = obj.PlanEndMonth;
									break;
								}
							}
							ds.MoCount = ds.MoCount  + 1;

							//							if(to.MoProductStatus == MoObject.OnTimeShip)
							//							{
							to.MoCount = to.MoCount  + 1;
							//							}
							//处理ShipDetails
							if(ds.MoDetails == null)
							{
								ds.MoDetails = new System.Collections.ArrayList();
							}

							ds.MoDetails.Add(obj);

							to.DailyMos.Add(ds.CountScale + obj.MoProductStatus,ds);
						}

						#endregion

						to.Scale = System.Math.Round(to.MoCount/to.TotalMoCount,2) * 100;

						htTotal.Add(obj.MoProductStatus,to);
						#endregion
					}
				}
			}

			MPOCRXmlBuilder xml = new MPOCRXmlBuilder();
			xml.BeginBuildRoot();
			//写入汇总
			foreach(MoObject to in htTotal.Values)
			{
				xml.BeginBuildItem(to.MoProductStatus,to.MoCount.ToString(),to.TotalMoCount.ToString(),to.Scale.ToString());
				//写入天数据
				foreach(DailyMo ds in to.DailyMos.Values)
				{
					xml.BeginBuildDateMo(ds.CountScale,ds.MoCount.ToString());
					//写入月数据
					foreach(MPOCRObject obj in ds.MoDetails)
					{
						xml.BuildDateMoDetail(obj.ItemCode,obj.MoCode,obj.PlanEndDate,obj.ActEndDate,obj.OutPutQty);
					}
					//Flex的bug，单条数据无法显示
					if(ds.MoDetails.Count == 1)
					{
						xml.BuildDateMoDetail(String.Empty,String.Empty,String.Empty,String.Empty,String.Empty);
					}
					xml.EndBuildDateMo();
				}
				//Flex的bug，单条数据无法显示
				if(to.DailyMos.Values.Count == 1)
				{
					xml.BeginBuildDateMo(String.Empty,String.Empty);
					xml.EndBuildDateMo();
				}
				xml.EndBuildItem();
			}
			if(htTotal.Values.Count == 1)
			{	
				xml.BeginBuildItem(String.Empty,String.Empty,String.Empty,String.Empty);
				xml.EndBuildItem();
			}
			xml.EndBuildRoot();

			strReturn = xml.XmlContent.ToString();

			return strReturn;
		}
		#endregion

		#region MTTR考核
		//TODO:尚未完成
		public string getMTTR(
			string modelCode
			,string itemCode
			,string resCode
			,string frmDate
			,string toDate
			,string frmMonth
			,string toMonth
			,string frmWeek
			,string toWeek
			,string statisticlatitude
			,string filterfield)
		{
			string strReturn = String.Empty;

			#region Contract Total SQL String

			string sqlCondition = String.Empty;


			string sqlMain = @"SELECT   a.*,(case when a.confirmdate>0 then to_char(to_date(a.confirmdate,'yyyyMMdd'),'ww') else '0' end) as confirmweek,(case when a.confirmdate>0 then to_char(to_date(a.confirmdate,'yyyyMMdd'),'MM') else '0' end) as confirmmonth
    FROM (SELECT
                   round((CASE
                       WHEN confirmdate <= tsdate AND tstime IS NOT NULL AND confirmtime IS NOT NULL AND tsdate - confirmdate < 1
                          THEN   TO_DATE (to_char(tstime,'000000'), 'HH24MISS') - TO_DATE (to_char(confirmtime,'000000'), 'HH24MISS')
                       WHEN confirmdate <= tsdate AND tsdate - confirmdate >= 1 AND tstime IS NOT NULL AND confirmtime IS NOT NULL
                          THEN   TO_DATE (TO_CHAR (tsdate) || TO_CHAR (tstime,'000000'),
                                          'yyyyMMddHH24MISS'
                                         )
                               - TO_DATE (   TO_CHAR (confirmdate)
                                          || TO_CHAR (confirmtime,'000000'),
                                          'yyyyMMddHH24MISS'
                                         )
                       ELSE 0
                    END
                   )
                 * 24
                 * 60,0) AS tsspantime,
                 modelcode,itemcode,tsrescode,rcard,confirmdate,tsdate
            FROM tblts b
           WHERE tsstatus IN ('tsstatus_reflow', 'tsstatus_complete')) a
ORDER BY a.confirmdate";

			//机种
			if(modelCode != null && modelCode != String.Empty)
			{
				sqlCondition += " and itemCode in (select itemcode from tblmodel2item where modelcode='" + modelCode + "'" + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")";
			}
			//产品
			if(itemCode != null && itemCode != String.Empty)
			{
				sqlCondition += " and itemCode = '" + itemCode + "'";
			}
			//订单号
			if(resCode != null && resCode != String.Empty)
			{
				sqlCondition += " and  tsrescode = '" + resCode + "'";
			}
			//开始日期（计划出货日期）
			if(frmDate != null && frmDate != String.Empty)
			{
				sqlCondition += " and A.confirmdate >= " + frmDate;
			}
			//结束日期（计划出货日期）
			if(toDate != null && toDate != String.Empty)
			{
				sqlCondition += " and A.confirmdate <= " + toDate;
			}
			//开始实际出货月份
			if(frmMonth != null && frmMonth != String.Empty)
			{
				sqlCondition += " and A.confirmmonth >= " + frmMonth;
			}
			//截至实际出货月份
			if(toMonth != null && toMonth != String.Empty)
			{
				sqlCondition += " and A.confirmmonth <= " + toMonth ;
			}
			//开始实际出货星期
			if(frmWeek != null && frmWeek != String.Empty)
			{
				sqlCondition += " and A.confirmweek <= " + frmWeek ;
			}
			//开始实际出货星期
			if(toWeek != null && toWeek != String.Empty)
			{
				sqlCondition += " and A.confirmweek <= " + toWeek;
			}

			string sql = String.Format(sqlMain,sqlCondition);
			#endregion

			object[] objs = DataProvider.CustomQuery(typeof(MTTRQueryObject)
				,new SQLCondition(sql));

			System.Collections.Hashtable htTotal = new System.Collections.Hashtable();

			if(objs != null && objs.Length > 0)
			{

				double mttr = 0;
				foreach(MTTRQueryObject obj in objs)
				{
					string objGroupType = String.Empty;

					switch(filterfield)
					{
						case "MODELCODE":
						{
							objGroupType = obj.ModelCode;
							break;
						}
						case "ITEMCODE":
						{
							objGroupType = obj.ItemCode;
							break;
						}
						case "RESCODE":
						{
							objGroupType = obj.ResourceCode;
							break;
						}
					}
					//处理Total Object
					if(htTotal.ContainsKey(objGroupType))
					{
						#region 已经存在Key=objGroupType的MTTRObject对象
						MTTRObject mto = (MTTRObject)htTotal[objGroupType];

						mto.FieldCode = objGroupType;
						
						mto.TSQty = mto.TSQty + 1;

						//mto.MTTR

						#region 处理SubMTTR Details

						if(mto.DailyMTTRs == null)
						{
							mto.DailyMTTRs = new System.Collections.SortedList();

							SubMTTR ds = new SubMTTR();
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.Date = obj.ConfrimDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.Date = obj.ConfrimWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.Date = obj.ConfrimMonth;
									break;
								}
							}
							ds.FieldCode = mto.FieldCode;
							ds.TSQty = 1;	

							ds.TTR = ds.TTR + Convert.ToDouble(obj.TTR);

							//							if(objGroupType == MTTRObject.OnTimeShip)
							//							{
								
							//mto.ShippedOrderCount = mto.ShippedOrderCount  + 1;
							//							}
							//处理TTRDetails
							if(ds.TTRDetails == null)
							{
								ds.TTRDetails = new System.Collections.ArrayList();
							}

							ds.TTRDetails.Add(obj);

							ds.MTTR = System.Math.Round(ds.TTR / ds.TSQty,0);

							mto.DailyMTTRs.Add(ds.Date + objGroupType,ds);
						}
						else
						{
							string byObject = String.Empty;
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									byObject = obj.ConfrimDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									byObject = obj.ConfrimWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									byObject = obj.ConfrimMonth;
									break;
								}
							}

							if(mto.DailyMTTRs.ContainsKey(byObject + objGroupType))
							{
								SubMTTR ds = (SubMTTR)mto.DailyMTTRs[byObject + objGroupType];
								//Laws Lu,2006/04/26 允许按不同纬度统计
								switch(statisticlatitude)
								{
									case DashboardFacade.ByDay:
									{
										ds.Date = obj.ConfrimDate;
										break;
									}
									case DashboardFacade.ByWeek:
									{
										ds.Date = obj.ConfrimWeek;
										break;
									}
									case DashboardFacade.ByMonth:
									{
										ds.Date = obj.ConfrimMonth;
										break;
									}
								}
								ds.FieldCode = mto.FieldCode;
								ds.TSQty = ds.TSQty  + 1;
								ds.TTR = ds.TTR + Convert.ToDouble(obj.TTR);

								//								if(objGroupType == MTTRObject.OnTimeShip)
								//								{
								//mto.ShippedOrderCount = mto.ShippedOrderCount  + 1;
								//								}
								//处理TTRDetails
								if(ds.TTRDetails == null)
								{
									ds.TTRDetails = new System.Collections.ArrayList();
								}

								ds.TTRDetails.Add(obj);

								ds.MTTR = System.Math.Round(ds.TTR / ds.TSQty,0);

								mto.DailyMTTRs[ds.Date + objGroupType] = ds;
							}
							else
							{
								SubMTTR ds = new SubMTTR();//(SubMTTR)mto.DailyMTTRs[ds.Date + objGroupType];
								ds.FieldCode = mto.FieldCode;
								ds.TSQty = 1;
								ds.TTR = ds.TTR + Convert.ToDouble(obj.TTR);
								//Laws Lu,2006/04/26 允许按不同纬度统计
								switch(statisticlatitude)
								{
									case DashboardFacade.ByDay:
									{
										ds.Date = obj.ConfrimDate;
										break;
									}
									case DashboardFacade.ByWeek:
									{
										ds.Date = obj.ConfrimWeek;
										break;
									}
									case DashboardFacade.ByMonth:
									{
										ds.Date = obj.ConfrimMonth;
										break;
									}
								}
								//								if(mto.FieldCode == MTTRObject.OnTimeShip)
								//								{
								ds.MTTR = System.Math.Round(ds.TTR / ds.TSQty,0);
								//								}
								//处理TTRDetails
								if(ds.TTRDetails == null)
								{
									ds.TTRDetails = new System.Collections.ArrayList();
								}

								ds.TTRDetails.Add(obj);

								mto.DailyMTTRs.Add(ds.Date + objGroupType,ds);
							}
						}

						#endregion
						
						mto.TTR = Convert.ToDouble(obj.TTR) + mto.TTR;

						mto.MTTR = System.Math.Round(Convert.ToDouble(mto.TTR)/mto.TSQty,0);

						mttr = mttr + mto.MTTR;

						mto.TotalMTTR = System.Math.Round(mttr/objs.Length,0);

						htTotal[objGroupType] = mto;
						#endregion
					}
					else
					{
						#region 不存在时的处理
						MTTRObject mto = new MTTRObject();
					
						mto.FieldCode = objGroupType;
						
						mto.TSQty = mto.TSQty + 1;
						
						#region 处理SubMTTR Details

						if(mto.DailyMTTRs == null)
						{
							mto.DailyMTTRs = new System.Collections.SortedList();

							SubMTTR ds = new SubMTTR();
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.Date = obj.ConfrimDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.Date = obj.ConfrimWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.Date = obj.ConfrimMonth;
									break;
								}
							}
							ds.FieldCode = mto.FieldCode;
							ds.TSQty = 1;	
							ds.TTR = ds.TTR +Convert.ToDouble(obj.TTR);

							//							if(objGroupType == MTTRObject.OnTimeShip)
							//							{
							ds.MTTR = System.Math.Round(ds.TTR / ds.TSQty,0);
							//							}
							//处理TTRDetails
							if(ds.TTRDetails == null)
							{
								ds.TTRDetails = new System.Collections.ArrayList();
							}

							ds.TTRDetails.Add(obj);

							mto.DailyMTTRs.Add(ds.Date + objGroupType,ds);
						}
						else
						{
							string byObject = String.Empty;
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									byObject = obj.ConfrimDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									byObject = obj.ConfrimWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									byObject = obj.ConfrimMonth;
									break;
								}
							}


							SubMTTR ds = (SubMTTR)mto.DailyMTTRs[byObject];
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.Date = obj.ConfrimDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.Date = obj.ConfrimWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.Date = obj.ConfrimMonth;
									break;
								}
							}
							ds.FieldCode = mto.FieldCode;
							ds.TSQty = ds.TSQty  + 1;
							ds.TTR = ds.TTR +Convert.ToDouble(obj.TTR);

							//							if(mto.FieldCode == MTTRObject.OnTimeShip)
							//							{
							ds.MTTR = System.Math.Round(ds.TTR / ds.TSQty,0);
							//							}
							//处理TTRDetails
							if(ds.TTRDetails == null)
							{
								ds.TTRDetails = new System.Collections.ArrayList();
							}

							ds.TTRDetails.Add(obj);

							mto.DailyMTTRs.Add(ds.Date + objGroupType,ds);
						}

						#endregion

						mto.TTR = Convert.ToDouble(obj.TTR) + mto.TTR;

						mto.MTTR = System.Math.Round(Convert.ToDouble(mto.TTR)/mto.TSQty,0);

						mttr = mttr + mto.MTTR;

						mto.TotalMTTR =  System.Math.Round(mttr/objs.Length,0);

						htTotal.Add(objGroupType,mto);
						#endregion
					}
				}
			}

			MTTRXmlBuilder xml = new MTTRXmlBuilder();
			xml.BeginBuildRoot();
			//写入汇总
			foreach(MTTRObject to in htTotal.Values)
			{
				xml.BeginBuildItem(to.FieldCode,to.MTTR.ToString(),to.TotalMTTR.ToString(),to.TSQty.ToString());
				//写入天数据
				foreach(SubMTTR ds in to.DailyMTTRs.Values)
				{
					xml.BeginBuildDateMTTR(ds.Date,ds.FieldCode,ds.TSQty.ToString(),ds.MTTR.ToString());
					//写入月数据
					foreach(MTTRQueryObject obj in ds.TTRDetails)
					{
						xml.BuildDateMTTRDetail(obj.ItemCode,obj.ModelCode,obj.ResourceCode,obj.SN,obj.ConfrimDate,obj.CompleteDate,obj.TTR);
					}
					//Flex的bug，单条数据无法显示
					if(ds.TTRDetails.Count == 1)
					{
						xml.BuildDateMTTRDetail(String.Empty,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty);
					}

					xml.EndBuildDateMTTR();
				}
				//Flex的bug，单条数据无法显示
				if(to.DailyMTTRs.Values.Count == 1)
				{
					xml.BeginBuildDateMTTR(String.Empty,String.Empty,String.Empty,String.Empty);
					xml.EndBuildDateMTTR();
				}

				xml.EndBuildItem();
			}
			//Flex的bug，单条数据无法显示
			if(htTotal.Values.Count == 1)
			{	
				xml.BeginBuildItem(String.Empty,String.Empty,String.Empty,String.Empty);
				xml.EndBuildItem();
			}

			xml.EndBuildRoot();

			strReturn = xml.XmlContent.ToString();

			return strReturn;
		}
		#endregion

		#region MTBF 平均无故障时间

		public string getMTBF(
			string modelCode
			,string itemCode
			,string ssCode
			,string frmDate
			,string toDate
			,string frmMonth
			,string toMonth
			,string frmWeek
			,string toWeek
			,string statisticlatitude
			,string filterfield)
		{
			string strReturn = String.Empty;


			#region Contract Total SQL String

			string sqlCondition = String.Empty;


			string sqlMain = @"SELECT d.*, (SELECT MAX (mtime)
										FROM tblonwip c
										WHERE c.shiftday = d.shiftday) AS endtime
							FROM (SELECT rcard, modelcode, itemcode, sscode, shiftday,
										TO_CHAR (TO_DATE (shiftday, 'yyyyMMdd'), 'ww') AS week,
										TO_CHAR (TO_DATE (shiftday, 'yyyyMMdd'), 'MM') AS MONTH,
										(SELECT MIN (mtime)
											FROM tblonwip b
											WHERE b.shiftday = a.shiftday) AS begintime
									FROM tblonwip a
									WHERE action = 'NG' {0} ) d";

			//机种
			if(modelCode != null && modelCode != String.Empty)
			{
				sqlCondition += " and itemCode in (select itemcode from tblmodel2item where modelcode='" + modelCode + "'" + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")";
			}
			//产品
			if(itemCode != null && itemCode != String.Empty)
			{
				sqlCondition += " and itemCode = '" + itemCode + "'";
			}
			//产线代码
			if(ssCode != null && ssCode != String.Empty)
			{
				sqlCondition += " and  sscode = '" + ssCode + "'";
			}
			//开始日期（计划出货日期）
			if(frmDate != null && frmDate != String.Empty)
			{
				sqlCondition += " and A.shiftday >= " + frmDate;
			}
			//结束日期（计划出货日期）
			if(toDate != null && toDate != String.Empty)
			{
				sqlCondition += " and A.shiftday <= " + toDate;
			}
			

			string sql = String.Format(sqlMain,sqlCondition);
			#endregion

			object[] objs = DataProvider.CustomQuery(typeof(MTBFQueryObject)
				,new SQLCondition(sql));

			System.Collections.Hashtable htTotal = new System.Collections.Hashtable();
			double totalMTBF=0;
			if(objs != null && objs.Length > 0)
			{

				double ngQty = 0;
				double totalManuTime = 0;
				foreach(MTBFQueryObject obj in objs)
				{
					ngQty += 1;
					string objGroupType = String.Empty;

					switch(filterfield)
					{
						case "MODELCODE":
						{
							objGroupType = obj.ModelCode;
							break;
						}
						case "ITEMCODE":
						{
							objGroupType = obj.ItemCode;
							break;
						}
						case "SSCODE":
						{
							objGroupType = obj.SSCode;
							break;
						}
					}
					//处理Total Object
					if(htTotal.ContainsKey(objGroupType))
					{
						#region 已经存在Key=objGroupType的MTBFObject对象
						MTBFObject mto = (MTBFObject)htTotal[objGroupType];
						
						mto.FieldCode = objGroupType;						
						mto.NGQty += 1;					//不良次数
						
						#region 处理SubMTBF Details
						
						if(mto.DailyMTBFs != null)
						{
							string byObject = String.Empty;
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									byObject = obj.NGDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									byObject = obj.NGWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									byObject = obj.NGMonth;
									break;
								}
							}
						
							string htKey = byObject + objGroupType + obj.NGDate;
							
							if(mto.DailyMTBFs.ContainsKey(byObject + objGroupType) )
							{
								if(!mto.HTDayData.ContainsKey(htKey))
								{
									mto.ManufactureTime += obj.ManufactureTime;	//生产时间	
									totalManuTime += obj.ManufactureTime;

									SubMTBF ds = (SubMTBF)mto.DailyMTBFs[byObject + objGroupType];
									//Laws Lu,2006/04/26 允许按不同纬度统计
									switch(statisticlatitude)
									{
										case DashboardFacade.ByDay:
										{
											ds.Date = obj.NGDate;
											break;
										}
										case DashboardFacade.ByWeek:
										{
											ds.Date = obj.NGWeek;
											break;
										}
										case DashboardFacade.ByMonth:
										{
											ds.Date = obj.NGMonth;
											break;
										}
									}
									ds.FieldCode = mto.FieldCode;
									ds.NGQty = ds.NGQty  + 1;
	
									ds.ManufactureTime += obj.ManufactureTime;
									//ds.MTBF;在对象中自动计算
														
									//处理MTBFDetails
									if(ds.MTBFDetails == null)
									{
										ds.MTBFDetails = new System.Collections.ArrayList();
										ds.HTDetailDayData = new System.Collections.Hashtable();
									}
						
									if(!ds.HTDetailDayData.Contains(htKey))
									{
										ds.MTBFDetails.Add(obj);
										ds.HTDetailDayData.Add(htKey,htKey);//记录已经统计的明细数据
									}
						
									mto.DailyMTBFs[ds.Date + objGroupType] = ds;
									mto.HTDayData.Add(htKey,htKey); //记录已经统计的明细数据
								}
								else
								{
									//明细的生产时间已经统计过了，加入对应的MTBFDetails中即可.
									SubMTBF ds = (SubMTBF)mto.DailyMTBFs[byObject + objGroupType];
									ds.NGQty += 1;
									ds.MTBFDetails.Add(obj);
									mto.DailyMTBFs[ds.Date + objGroupType] = ds;
								}
							}
							else
							{
								SubMTBF ds = new SubMTBF();
								//Laws Lu,2006/04/26 允许按不同纬度统计
								switch(statisticlatitude)
								{
									case DashboardFacade.ByDay:
									{
										ds.Date = obj.NGDate;
										break;
									}
									case DashboardFacade.ByWeek:
									{
										ds.Date = obj.NGWeek;
										break;
									}
									case DashboardFacade.ByMonth:
									{
										ds.Date = obj.NGMonth;
										break;
									}
								}
								ds.FieldCode = mto.FieldCode;
								ds.NGQty = 1;	
								ds.ManufactureTime += obj.ManufactureTime;
								//ds.MTBF;在对象中自动计算
							
								//处理MTBFDetails
								if(ds.MTBFDetails == null)
								{
									ds.MTBFDetails = new System.Collections.ArrayList();
									ds.HTDetailDayData = new System.Collections.Hashtable();
								}
							
								ds.MTBFDetails.Add(obj);
								ds.HTDetailDayData.Add(htKey,htKey);//记录已经统计的明细数据

								mto.DailyMTBFs.Add(ds.Date + objGroupType,ds);
								mto.HTDayData.Add(htKey,htKey); //记录已经统计的明细数据
							}
						}
						
						#endregion
						
						htTotal[objGroupType] = mto;
						#endregion
					}
					else
					{
						#region 不存在时的处理
						MTBFObject mto = new MTBFObject();
					
						mto.FieldCode = objGroupType;						
						mto.NGQty = mto.NGQty + 1;					//不良次数
						mto.ManufactureTime += obj.ManufactureTime;	//生产时间
						totalManuTime += obj.ManufactureTime;
						
						#region 处理SubMTBF Details

						if(mto.DailyMTBFs == null)
						{
							mto.DailyMTBFs = new System.Collections.SortedList();
							mto.HTDayData = new System.Collections.Hashtable();

							SubMTBF ds = new SubMTBF();
							//Laws Lu,2006/04/26 允许按不同纬度统计
							switch(statisticlatitude)
							{
								case DashboardFacade.ByDay:
								{
									ds.Date = obj.NGDate;
									break;
								}
								case DashboardFacade.ByWeek:
								{
									ds.Date = obj.NGWeek;
									break;
								}
								case DashboardFacade.ByMonth:
								{
									ds.Date = obj.NGMonth;
									break;
								}
							}
							ds.FieldCode = mto.FieldCode;
							ds.NGQty = 1;	
							ds.ManufactureTime += obj.ManufactureTime;
							//ds.MTBF;在对象中自动计算
							
							//处理MTBFDetails
							if(ds.MTBFDetails == null)
							{
								ds.MTBFDetails = new System.Collections.ArrayList();
								ds.HTDetailDayData = new System.Collections.Hashtable();
							}
							string htKey = ds.Date + objGroupType + obj.NGDate;
							
							ds.MTBFDetails.Add(obj);
							ds.HTDetailDayData.Add(htKey,htKey);//记录已经统计的明细数据

							mto.DailyMTBFs.Add(ds.Date + objGroupType,ds);
							mto.HTDayData.Add(htKey,htKey); //记录已经统计的明细数据
						}

						#endregion


						htTotal.Add(objGroupType,mto);
						#endregion
					}
				}
				totalMTBF = System.Math.Round(totalManuTime/ngQty,2);
			}

			MTBFXmlBuilder xml = new MTBFXmlBuilder();
			xml.BeginBuildRoot();
			//写入汇总
			foreach(MTBFObject to in htTotal.Values)
			{
				xml.BeginBuildItem(to.FieldCode,to.MTBF.ToString(),totalMTBF.ToString(),to.NGQty.ToString());
				//写入天数据
				foreach(SubMTBF ds in to.DailyMTBFs.Values)
				{
					xml.BeginBuildDateMTBF(ds.Date,ds.FieldCode,ds.NGQty.ToString(),ds.MTBF.ToString());
					//写入月数据
					foreach(MTBFQueryObject obj in ds.MTBFDetails)
					{
						xml.BuildDateMTBFDetail(obj.ItemCode,obj.ModelCode,obj.SSCode,obj.SN,obj.NGDate,obj.begintime,obj.endtime,obj.ManufactureTime.ToString());
					}
					//Flex的bug，单条数据无法显示
					if(ds.MTBFDetails.Count == 1)
					{
						xml.BuildDateMTBFDetail(String.Empty,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty);
					}

					xml.EndBuildDateMTBF();
				}
				//Flex的bug，单条数据无法显示
				if(to.DailyMTBFs.Values.Count == 1)
				{
					xml.BeginBuildDateMTBF(String.Empty,String.Empty,String.Empty,String.Empty);
					xml.EndBuildDateMTBF();
				}

				xml.EndBuildItem();
			}
			//Flex的bug，单条数据无法显示
			if(htTotal.Values.Count == 1)
			{	
				xml.BeginBuildItem(String.Empty,String.Empty,String.Empty,String.Empty);
				xml.EndBuildItem();
			}

			xml.EndBuildRoot();

			strReturn = xml.XmlContent.ToString();

			return strReturn;
		}

		private string getManufacrureTime(string begintime,string endtime)
		{
			return string.Empty;
		}

		#endregion
	}

}
