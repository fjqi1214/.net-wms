#region System
using System;
using System.Runtime.Remoting;
using System.Collections;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;
#endregion


/// TryFacade 的摘要说明。
/// 文件名:
/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
/// 创建人:Crystal Chu
/// 创建日期:2008/11/07
/// 修改人:hiro
/// 修改日期:
/// 描 述: 对TryFacade设置的操作控制
/// 版 本:	
/// </summary>  
namespace BenQGuru.eMES.MOModel
{
    public class TryFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public TryFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }


        public TryFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
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

        #region Try

        public Try CreateNewTry()
        {
            return new Try();
        }

        public void AddTRY(Try TRY)
        {
            //this._domainDataProvider.Insert(TRY);
            this._helper.AddDomainObject(TRY);
        }

        public void UpdateTry(Try TRY)
        {
            this._domainDataProvider.Update(TRY);
        }

        public void DeleteTry(Try TRY)
        {
            this._domainDataProvider.Delete(TRY);
        }

        public void DeleteTry(Try[] TRY)
        {
            this._helper.DeleteDomainObject(TRY);
        }

        public object GetTry(string tryCode)
        {
            return this.DataProvider.CustomSearch(typeof(Try), new object[] { tryCode });
        }

        public object[] QueryTry(string tryCode)
        {
            string sql = "";
            sql = " SELECT * FROM tbltry  WHERE 1=1";
            if (tryCode.Length > 0)
            {
                if (tryCode.IndexOf(",") >= 0)
                {
                    tryCode = tryCode.Replace(",", "','");
                }

                sql += " AND trycode in ('" + tryCode.Trim().ToUpper() + "')";
            }

            sql += " ORDER by trycode";

            return this.DataProvider.CustomQuery(typeof(Try), new SQLCondition(sql));
        }


        public object[] GetTryWithStatus()
        {
            string sql = " SELECT a.*,b.mname as itemdesc FROM TBLTRY a LEFT OUTER JOIN tblmaterial b ON a.itemcode=b.mcode ";
            sql += " WHERE  a.STATUS IN ('trystatus_produce', 'trystatus_release') ORDER BY TRYCODE,a.itemcode ";
            return this._domainDataProvider.CustomQuery(typeof(TryAndItemDesc), new SQLCondition(sql));
        }


        public object[] GetAllTry()
        {
            string sql = " select * from tbltry where 1=1 order by trycode ";
            return this._domainDataProvider.CustomQuery(typeof(Try), new SQLCondition(sql));
        }

        public string GenerateTrySql(string tryCode, string status, string materialCode, string dept, string vendorName, int cdate, string createUser,string tryType,bool type)
        {
            string sql = "";

            if (type)
            {
                sql = "SELECT t1.trycode AS TRYCODE, t1.status AS STATUS, t1.planqty AS PLANQTY, t1.actualqty AS ACTUALQTY, "
                + "t1.itemcode AS ITEMCODE, t2.mdesc AS ItemDesc, t1.dept AS DEPT, t1.vendorname AS VENDORNAME, "
                + "t1.result AS RESULT, t1.memo AS MEMO, t1.trydocument AS TRYDOCUMENT, t1.cuser AS CUSER, t1.cdate AS CDATE,"
                + "t1.ruser AS RUSER, t1.rdate AS RDATE, t1.fuser AS FUSER, t1.fdate AS FDATE,t1.TryType AS TRYTYPE,"
                + "t1.TryReason AS TRYREASON,t1.SoftVersion AS SOFTVERSION,t1.WaitTry AS WAITTRY,t1.Change AS CHANGE,t1.BurnINDuration AS BURNINDURATION "
                + "FROM tbltry t1, tblmaterial t2 WHERE t1.itemcode = t2.mcode(+)";
            }
            else
            {
                sql = "SELECT COUNT(*) FROM tbltry t1, tblmaterial t2 WHERE t1.itemcode = t2.mcode(+)";
            }

            if (tryCode != null && tryCode.Length != 0)
            {
                sql = string.Format("{0} AND t1.trycode LIKE '%{1}%'", sql, tryCode);
            }
            if (status != null && status.Length != 0)
            {
                sql = string.Format("{0} AND t1.status = '{1}'", sql, status);
            }
            if (materialCode != null && materialCode.Length != 0)
            {
                string[] materialCodeArray = materialCode.Split(new Char[] { ',' });
                string str = string.Empty;

                foreach (string matrialCodeSingle in materialCodeArray)
                {
                    str += "'" + matrialCodeSingle + "',";
                }

                string newStr = str.Substring(0, str.Length - 1);

                sql = string.Format("{0} AND t1.itemcode in ({1})", sql, newStr);
            }
            if (dept != null && dept.Length != 0)
            {
                string[] deptArray = dept.Split(new Char[] { ',' });
                string str = string.Empty;

                foreach (string deptSingle in deptArray)
                {
                    str += "'" + deptSingle + "',";
                }

                string newStr = str.Substring(0, str.Length - 1);

                sql = string.Format("{0} AND t1.dept in ({1})", sql, newStr);
            }
            if (vendorName != null && vendorName.Length != 0)
            {
                sql = string.Format("{0} AND t1.vendorname LIKE '%{1}%'", sql, vendorName);
            }
            if (cdate != 0)
            {
                sql = string.Format("{0} AND t1.cdate = '{1}'", sql, cdate);
            }
            if (createUser!=null && createUser.Length != 0)
            {
                string[] createUserArray = createUser.Split(new Char[] { ',' });
                string str = string.Empty;

                foreach (string deptSingle in createUserArray)
                {
                    str += "'" + deptSingle + "',";
                }

                string newStr = str.Substring(0, str.Length - 1);

                sql = string.Format("{0} AND t1.cuser in ({1})", sql, newStr);
            }
            if (tryType!=null && tryType.Length!=0)
            {
                sql = string.Format("{0} AND t1.TryType LIKE '%{1}%'", sql, tryType);
            }
            return sql;
        }

        public object[] QueryTry(string tryCode, string status, string materialCode, string dept, string vendorName, int cdate, string createUser,string tryType,int inclusive, int exclusive)
        {
            string sql = GenerateTrySql(tryCode, status, materialCode, dept, vendorName, cdate,createUser,tryType, true);

            return this.DataProvider.CustomQuery(typeof(TryWithDesc), new PagerCondition(sql, "t1.trycode", inclusive, exclusive));

        }

        public int QueryTryCount(string tryCode, string status, string materialCode, string dept, string vendorName, int cdate,string createUser,string tryType)
        {
            string sql = GenerateTrySql(tryCode, status, materialCode, dept, vendorName, cdate,createUser,tryType, false);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetTryListOfRCard(string rCard, string itemCode)
        {
            string strSql = "";
            strSql += "SELECT   {0}";
            strSql += "    FROM tbltry";
            strSql += "   WHERE trycode IN (SELECT DISTINCT trycode";
            strSql += "                                FROM tbltry2rcard";
            strSql += "                               WHERE rcard = '" + rCard + "' AND itemcode = '" + itemCode + "')";
            strSql += "         AND linklot = 'Y' ";
            strSql += "ORDER BY trycode";

            strSql = string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Try)));
            return this.DataProvider.CustomQuery(typeof(Try), new SQLCondition(strSql));
        }

        public object[] GetTryListOfLotNo(string lotNo)
        {
            string strSql = "";
            strSql += "SELECT   {0}";
            strSql += "    FROM tbltry";
            strSql += "   WHERE trycode IN (SELECT DISTINCT trycode";
            strSql += "                                FROM tbltry2lot";
            strSql += "                               WHERE lotno = '" + lotNo + "') AND linklot = 'Y'";
            strSql += "ORDER BY trycode";

            strSql = string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Try)));
            return this.DataProvider.CustomQuery(typeof(Try), new SQLCondition(strSql));
        }

        public object[] QueryTryByRcard(string rcard)
        {
            string sql = "";

            sql += " SELECT * FROM tbltry";
            sql += "    WHERE trycode IN (SELECT trycode ";
            sql += "                         FROM tbltry2rcard ";
            sql += "                         WHERE rcard = '" + rcard.Trim().ToUpper() + "')";
            sql += " order by trycode";
            return this.DataProvider.CustomQuery(typeof(Try), new SQLCondition(sql));

        }


        #endregion

        #region TryStautsChanged

        public void TryStatusChanged(Try[] tryObjs)
        {
            this._domainDataProvider.BeginTransaction();
            try
            {
                foreach (Try tryObj in tryObjs)
                {
                    this.UpdateTry(tryObj);
                }
                this._domainDataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this._domainDataProvider.RollbackTransaction();
                throw ex;
            }
        }
        #endregion

        #region Try2RCard
        public Try2RCard CreateNewTry2RCard()
        {
            return new Try2RCard();
        }

        public void AddTry2RCard(Try2RCard try2RCard)
        {
            this._domainDataProvider.Insert(try2RCard);
        }

        public void UpdateTry2RCard(Try2RCard try2RCard)
        {
            this._domainDataProvider.Update(try2RCard);
        }

        public void DeleteTry2RCard(Try2RCard try2RCard)
        {
            this._domainDataProvider.Delete(try2RCard);
        }

        public void DeleteTry2RCard(Try2RCard[] try2RCard)
        {
            this._domainDataProvider.Delete(try2RCard);
        }

        public object GetTry2RCard(string trycode, string rcard, string itemcode)
        {
            return this.DataProvider.CustomSearch(typeof(Try2RCard), new object[] { trycode, rcard, itemcode });
        }

        public object[] QueryTry2RcardByTryCode(string tryCode, int inclusive, int exclusive)
        {
            string sql = "SELECT t1.rcard AS RCARD, t3.cartoncode AS CARTONCODE, t4.palletcode AS PALLETCODE, t1.itemcode AS ITEMCODE, "
            + "t2.mdesc AS MDESC, t1.opcode AS OPCODE, t1.muser AS MUSER, t1.mdate AS MDATE, t1.mtime AS MTIME "
            + "FROM tbltry2rcard t1, tblmaterial t2, tblsimulationreport t3, tblpallet2rcard t4 "
            + "WHERE t1.trycode='" + tryCode + "' and t1.itemcode = t2.mcode and t1.rcard = t3.rcard and t1.rcard = t4.rcard(+)";

            return this.DataProvider.CustomQuery(typeof(Try2RcardNew), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryTry2RcardByTryCodeCount(string tryCode)
        {
            string sql = "SELECT COUNT(*) FROM tbltry2rcard t1, tblmaterial t2, tblsimulationreport t3, tblpallet2rcard t4 "
            + "WHERE t1.trycode='" + tryCode + "' and t1.itemcode = t2.mcode and t1.rcard = t3.rcard and t1.rcard = t4.rcard(+)";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryTry2RCard(string tryCode, string rCard, string itemCode)
        {
            string sql = string.Empty;

            sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Try2RCard)) + " ";
            sql += "FROM tbltry2rcard ";
            sql += "WHERE 1 = 1 ";
            if (tryCode.Trim().Length > 0)
            {
                sql += "AND trycode = '" + tryCode.Trim().ToUpper() + "' ";
            }
            if (rCard.Trim().Length > 0)
            {
                sql += "AND rcard = '" + rCard.Trim().ToUpper() + "' ";
            }
            if (itemCode.Trim().Length > 0)
            {
                sql += "AND itemcode = '" + itemCode.Trim().ToUpper() + "' ";
            }

            return this.DataProvider.CustomQuery(typeof(Try2RCard), new SQLCondition(sql));
        }

        public void DeleteTry2RCard(string tryCode, string rCard)
        {
            string sql = string.Empty;

            sql += "DELETE FROM tbltry2rcard WHERE  trycode='" + tryCode.Trim().ToUpper() + "' and  rcard='" + rCard.Trim().ToUpper() + "' ";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        public Try2Lot CreateNewTry2Lot()
        {
            return new Try2Lot();
        }

        public void AddTry2Lot(Try2Lot try2Lot)
        {
            this.DataProvider.Insert(try2Lot);
        }

        public void UpdateTry2Lot(Try2Lot try2Lot)
        {
            this.DataProvider.Update(try2Lot);
        }

        public void DeleteTry2Lot(Try2Lot try2Lot)
        {
            this.DataProvider.Delete(try2Lot);
        }

        public object[] GetTry2LotList(string lotNo)
        {
            string strSql = "SELECT {0} FROM tbltry2lot WHERE lotno='" + lotNo + "' ORDER BY trycode";
            strSql = string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Try2Lot)));

            return this.DataProvider.CustomQuery(typeof(Try2Lot), new SQLCondition(strSql));
        }
    }
}
