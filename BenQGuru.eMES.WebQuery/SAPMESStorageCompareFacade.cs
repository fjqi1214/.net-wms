using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
    public class SAPMESStorageCompareFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public SAPMESStorageCompareFacade(IDomainDataProvider domainDataProvider)
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

        private string GetSAPMESStorageCompareSql(string factoryList, string storageList, string itemCode)
        {
            string sql = string.Empty;

            string orgIdCondition = GetOrgIdCondition(factoryList);
            string storageIdCondition = GetStorageIdCondition(storageList);

            sql += "FROM ( ";
            sql += "    SELECT NVL(a.orgid, D.orgid) orgid, NVL(a.storageid, D.sapstorage) storageid, ";
            sql += "        NVL(a.itemcode, D.itemcode) itemcode, NVL(a.itemgrade, D.itemgrade) itemgrade, ";
            sql += "        NVL(a.clabsqty, 0) sapqty, NVL(a.cspemqty, 0) sapmqty, NVL(D.qty, 0) mesqty,nvl(D.sapokqty, 0) sapokqty";
            sql += "    FROM tblsapstorageinfo a ";
            sql += "    FULL JOIN ( ";
            sql += "    SELECT B.QTY,B.SAPOKQTY,B.ORGID,B.SAPSTORAGE,B.ITEMCODE,B.ITEMGRADE FROM (SELECT COUNT(*) QTY,";
            sql += "               SUM(CASE";
            sql += "                      WHEN GETRCARDSTATUSONTIME(SERIALNO) LIKE";
            sql += "                          '%SAPÍê¹¤%' THEN";
            sql += "                      '1'";
            sql += "                      ELSE";
            sql += "                       '0'";
            sql += "                    END) SAPOKQTY,";
            sql += "                G.ORGID,";
            sql += "                G.SAPSTORAGE,";
            sql += "                D.ITEMCODE,";
            sql += "                D.ITEMGRADE";
            sql += "           FROM TBLSTACK2RCARD D, TBLSTORAGE G ";
            sql += "          WHERE D.STORAGECODE = G.STORAGECODE ";

            if (orgIdCondition.Trim().Length > 0)
            {
                sql += "AND g.orgid " + orgIdCondition + " ";
            }

            if (storageIdCondition.Trim().Length > 0)
            {
                sql += "AND g.sapstorage " + storageIdCondition + " ";
            }


            sql += "          GROUP BY G.ORGID, G.SAPSTORAGE, D.ITEMCODE, D.ITEMGRADE) B) D ON A.ORGID =";
            sql += "                                                                           D.ORGID";
            sql += "                                                                       AND A.STORAGEID =";
            sql += "                                                                          D.SAPSTORAGE";
            sql += "                                                                      AND A.ITEMCODE =";
            sql += "                                                                           D.ITEMCODE";
            sql += "                                                                      AND A.ITEMGRADE =";
            sql += "                                                                          D.ITEMGRADE) c ";
            sql += "LEFT JOIN tblmaterial d ON c.itemcode = d.mcode ";
            sql += "WHERE 1 = 1 ";

            if (orgIdCondition.Trim().Length > 0)
            {
                sql += "AND c.orgid " + orgIdCondition + " ";
            }

            if (storageIdCondition.Trim().Length > 0)
            {
                sql += "AND c.storageid " + storageIdCondition + " ";
            }

            if (itemCode.Trim().Length > 0)
            {
                sql += "AND c.itemcode LIKE '%" + itemCode.Trim().ToUpper() + "%' ";
            }

            return sql;
        }

        private string GetOrgIdCondition(string factoryList)
        {
            string returnValue = string.Empty;

            if (factoryList.Trim().Length > 0)
            {
                string[] factoryStringList = factoryList.Trim().Split(',');
                List<int> orgIDList = new List<int>();
                for (int i = 0; i < factoryStringList.Length; i++)
                {
                    int orgID = -1;
                    if (int.TryParse(factoryStringList[i], out orgID))
                    {
                        orgIDList.Add(orgID);
                    }
                }

                if (orgIDList.Count == 1)
                {
                    returnValue = " = " + orgIDList[0].ToString() + " ";
                }
                else if (orgIDList.Count > 1)
                {
                    string temp = string.Empty;
                    foreach (int orgID in orgIDList)
                    {
                        if (temp.Trim().Length > 0)
                        {
                            temp += ",";
                        }
                        temp += orgID.ToString();
                    }
                    returnValue = " IN (" + temp + ") ";
                }
            }

            return returnValue;
        }

        private string GetStorageIdCondition(string storageList)
        {
            string returnValue = string.Empty;

            if (storageList.Trim().Length > 0)
            {
                if (storageList.IndexOf(",") >= 0)
                {
                    returnValue = " IN (" + FormatHelper.ProcessQueryValues(storageList.Trim().ToUpper()) + ") ";
                }
                else
                {
                    returnValue = " LIKE '%" + storageList.Trim().ToUpper() + "%' ";
                }
            }

            return returnValue;
        }

        public object[] QuerySAPMESStorageCompare(string factoryList, string storageList, string itemCode, int inclusive, int exclusive)
        {
            string sql = string.Empty;

            sql += "SELECT c.*, d.mname ";
            sql += GetSAPMESStorageCompareSql(factoryList, storageList, itemCode);
            sql += "ORDER BY c.orgid, c.storageid, c.itemcode, c.itemgrade ";

            return this.DataProvider.CustomQuery(typeof(SAPMESStorageCompare), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QuerySAPMESStorageCompareCount(string factoryList, string storageList, string itemCode)
        {
            string sql = string.Empty;

            sql += "SELECT COUNT(*) ";
            sql += GetSAPMESStorageCompareSql(factoryList, storageList, itemCode);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
    }
}
