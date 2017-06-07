using System;
using System.Collections;
using System.Collections.Specialized;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.OutSourcing;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// OutSourcingQueryFacade 的摘要说明。
	/// </summary>
	public class OutSourcingQueryFacade
	{
		private  IDomainDataProvider _domainDataProvider= null;

		public OutSourcingQueryFacade( IDomainDataProvider domainDataProvider )
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
		
		public object[] QueryOutSourcingWIP(string cardType, string itemCode, string moCode, string startSn, string endSn, int inclusive, int exclusive)
		{
			string strSql = string.Format("SELECT {0} FROM tblOutWIP WHERE Type='" + cardType + "' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OutWIP)));
			if (cardType == "LOT")
			{
				if (itemCode != string.Empty)
				{
					strSql += " AND StartSN IN (SELECT StartSN FROM tblOutMO WHERE ItemCode LIKE '" + itemCode + "%') ";
				}
			}
			else
			{
				if (itemCode != string.Empty || moCode != string.Empty)
				{
					strSql += " AND MOCode IN (SELECT MOCode FROM tblOutMO WHERE MOCode LIKE '" + moCode + "%' AND ItemCode LIKE '" + itemCode + "%') ";
				}
			}
			if (startSn != string.Empty || endSn != string.Empty)
			{
				if (startSn == string.Empty)
					startSn = endSn;
				if (endSn == string.Empty)
					endSn = startSn;
				string strWhere = " AND ((StartSN<='{0}' AND EndSN>='{0}') OR ";
				strWhere +=				"(StartSN<='{1}' AND EndSN>='{0}') OR ";
				strWhere +=				"(StartSN>='{0}' AND EndSN<='{1}')) ";
				strSql += string.Format(strWhere, startSn, endSn);
			}
			object[] objs = this.DataProvider.CustomQuery(typeof(OutWIP), new PagerCondition(strSql, "MOCode,StartSN,OPCode", inclusive, exclusive));
			return objs;
		}
		
		public int QueryOutSourcingWIPCount(string cardType, string itemCode, string moCode, string startSn, string endSn)
		{
			string strSql = "SELECT COUNT(StartSN) FROM tblOutWIP WHERE Type='" + cardType + "' ";
			if (cardType == "LOT")
			{
				if (itemCode != string.Empty)
				{
					strSql += " AND StartSN IN (SELECT StartSN FROM tblOutMO WHERE ItemCode LIKE '" + itemCode + "%') ";
				}
			}
			else
			{
				if (itemCode != string.Empty || moCode != string.Empty)
				{
					strSql += " AND MOCode IN (SELECT MOCode FROM tblOutMO WHERE MOCode LIKE '" + moCode + "%' AND ItemCode LIKE '" + itemCode + "%') ";
				}
			}
			if (startSn != string.Empty || endSn != string.Empty)
			{
				if (startSn == string.Empty)
					startSn = endSn;
				if (endSn == string.Empty)
					endSn = startSn;
				string strWhere = " AND ((StartSN<='{0}' AND EndSN>='{0}') OR ";
				strWhere +=				"(StartSN<='{1}' AND EndSN>='{0}') OR ";
				strWhere +=				"(StartSN>='{0}' AND EndSN<='{1}')) ";
				strSql += string.Format(strWhere, startSn, endSn);
			}
			return this.DataProvider.GetCount(new SQLCondition(strSql));
		}
				
		public object[] QueryOutSourcingWIPMaterial(string cardType, string itemCode, string moCode, string startSn, string endSn, int inclusive, int exclusive)
		{
			string strSql = string.Format("SELECT {0} FROM tblOutWIPMaterial WHERE Type='" + cardType + "' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OutWIPMaterial)));
			if (cardType == "PCS" && moCode != string.Empty)
			{
				strSql += " AND MOCode IN (SELECT MOCode FROM tblOutMO WHERE MOCode LIKE '" + moCode + "%') ";
			}
			if (startSn != string.Empty || endSn != string.Empty)
			{
				if (startSn == string.Empty)
					startSn = endSn;
				if (endSn == string.Empty)
					endSn = startSn;
				string strWhere = " AND ((StartSN<='{0}' AND EndSN>='{0}') OR ";
				strWhere +=				"(StartSN<='{1}' AND EndSN>='{0}') OR ";
				strWhere +=				"(StartSN>='{0}' AND EndSN<='{1}')) ";
				strSql += string.Format(strWhere, startSn, endSn);
			}
			if (itemCode != string.Empty)
			{
				strSql += " AND MaterialCode LIKE '" + itemCode + "%' ";
			}
			object[] objs = this.DataProvider.CustomQuery(typeof(OutWIPMaterial), new PagerCondition(strSql, "MOCode,StartSN,MaterialCode", inclusive, exclusive));
			return objs;
		}
		
		public int QueryOutSourcingWIPMaterialCount(string cardType, string itemCode, string moCode, string startSn, string endSn)
		{
			string strSql = "SELECT COUNT(StartSN) FROM tblOutWIPMaterial WHERE Type='" + cardType + "' ";
			if (cardType == "PCS" && moCode != string.Empty)
			{
				strSql += " AND MOCode IN (SELECT MOCode FROM tblOutMO WHERE MOCode LIKE '" + moCode + "%') ";
			}
			if (startSn != string.Empty || endSn != string.Empty)
			{
				if (startSn == string.Empty)
					startSn = endSn;
				if (endSn == string.Empty)
					endSn = startSn;
				string strWhere = " AND ((StartSN<='{0}' AND EndSN>='{0}') OR ";
				strWhere +=				"(StartSN<='{1}' AND EndSN>='{0}') OR ";
				strWhere +=				"(StartSN>='{0}' AND EndSN<='{1}')) ";
				strSql += string.Format(strWhere, startSn, endSn);
			}
			if (itemCode != string.Empty)
			{
				strSql += " AND MaterialCode LIKE '" + itemCode + "%' ";
			}
			return this.DataProvider.GetCount(new SQLCondition(strSql));
		}

		public object[] QueryOutMO(string moCode, string cardType, int inclusive, int exclusive)
		{
			string strSql = "SELECT {0} FROM tblOutMO WHERE Type='" + cardType + "' ";
			if (cardType == "PCS" && moCode != string.Empty)
				strSql += " AND MOCode LIKE '" + moCode + "%' ";
			else if (cardType == "LOT" && moCode != string.Empty)
				strSql += " AND StartSN LIKE '" + moCode + "%' ";
			object[] objs = this.DataProvider.CustomQuery(typeof(OutMO), new PagerCondition(string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OutMO))), "MOCode,StartSN", inclusive, exclusive));
			return objs;
		}

		public int QueryOutMOCount(string moCode, string cardType)
		{
			string strSql = "SELECT COUNT(MOCode) FROM tblOutMO WHERE Type='" + cardType + "' ";
			if (cardType == "PCS" && moCode != string.Empty)
				strSql += " AND MOCode LIKE '" + moCode + "%' ";
			else if (cardType == "LOT" && moCode != string.Empty)
				strSql += " AND StartSN LIKE '" + moCode + "%' ";
			return this.DataProvider.GetCount(new SQLCondition(strSql));
		}

	}
}
