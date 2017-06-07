using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.WebQuery
{
    public class KPIQueryFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public KPIQueryFacade(IDomainDataProvider domainDataProvider)
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

        #region 线体平衡率

        public object[] GetKPIRes(string mocode, string sscode)
        {
            string sql = "select rescode from tblres WHERE SSCODE='" + sscode + "' AND RESCODE IN (";
            sql += "SELECT RESCODE  FROM TBLOP2RES WHERE OPCODE  IN (";
            sql += "select OPCODE FROM TBLITEMROUTE2OP WHERE itemcode=(";
            sql += " SELECT ITEMCODE from tblmo WHERE mocode='" + mocode + "')))";
            return this.DataProvider.CustomQuery(typeof(Domain.BaseSetting.Resource), new SQLCondition(sql));
        }

        public object GetOutPutTimes(string rescode, string mocode, string shiftcode, int begindate, int enddate)
        {
            string sql = "select  nvl(SUM(CT.OUTPUTTIMES),0) AS OUTPUTTIMES  FROM (SELECT MOCODE, SHIFTDAY,ITEMCODE,TBLMESENTITYLIST_SERIAL,INPUTTIMES,OUTPUTTIMES,NGTIMES,EATTRIBUTE1 FROM TBLRPTOPQTY) CT LEFT OUTER JOIN TBLMESENTITYLIST TBLMESENTITYLIST";
            sql += " ON TBLMESENTITYLIST.SERIAL = CT.TBLMESENTITYLIST_SERIAL LEFT OUTER JOIN TBLSHIFT TBLSHIFT ON TBLSHIFT.SHIFTCODE = TBLMESENTITYLIST.SHIFTCODE LEFT OUTER JOIN TBLTP TBLTP ON TBLTP.TPCODE = TBLMESENTITYLIST.TPCODE";
            sql += "  LEFT OUTER JOIN TBLRES TBLRES ON TBLRES.RESCODE = TBLMESENTITYLIST.RESCODE WHERE 1 = 1";
            sql += " AND CT.SHIFTDAY >= " + begindate;
            sql += " AND CT.SHIFTDAY <= " + enddate;
            sql += " AND  TBLMESENTITYLIST.RESCODE='" + rescode + "'";
            sql += " AND MOCODE='" + mocode + "'";
            if (!string.IsNullOrEmpty(shiftcode))
            {
                sql += string.Format(" AND  TBLMESENTITYLIST.SHIFTCODE='{0}'", shiftcode);
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(Domain.Report.ReportOPQty), new SQLCondition(sql));
            if (objs.Length > 0 && objs != null)
            {
                return objs[0];
            }
            return null;
        }

        public object GetMaxMiniSerial(string rescode, string mocode, string shiftcode, int begindate, int enddate)
        {
            string sql = "SELECT  MAX(serial) as Inputtimes, MIN(serial) as outputtimes from TBLONWIP WHERE 1=1";
            sql += " AND SHIFTDAY >= " + begindate;
            sql += " AND SHIFTDAY <= " + enddate;
            sql += " AND MOCODE='" + mocode + "' AND RESCODE='" + rescode + "'";
            if (!string.IsNullOrEmpty(shiftcode))
            {
                sql += string.Format(" AND SHIFTCODE = '{0}'", shiftcode);
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(Domain.Report.ReportOPQty), new SQLCondition(sql));
            if (objs.Length > 0 && objs != null)
            {
                return objs[0];
            }
            return null;
        }

        public int GetMaxMinTime(int maxserial, int minserial)
        {
            string sql = "SELECT ROUND(TO_NUMBER(T2.TIMEEND - T1.TIMESTART) * 24 * 60 * 60) as Inputtimes FROM (SELECT TO_DATE(TRIM(TO_CHAR(MDATE, '00000000')) || TRIM(TO_CHAR(MTIME, '000000')), 'yyyy-mm-dd HH24:mi:ss') AS TIMESTART FROM TBLONWIP WHERE SERIAL = " + minserial + ") T1,";
            sql += " (SELECT TO_DATE(TRIM(TO_CHAR(MDATE, '00000000')) || TRIM(TO_CHAR(MTIME, '000000')), 'yyyy-mm-dd HH24:mi:ss') AS TIMEEND FROM TBLONWIP WHERE SERIAL = " + maxserial + ") T2";
            object[] objs = this.DataProvider.CustomQuery(typeof(Domain.Report.ReportOPQty), new SQLCondition(sql));
            if (objs.Length > 0 && objs != null)
            {
                return ((Domain.Report.ReportOPQty)objs[0]).InputTimes;
            }
            return 0;
        }

        #endregion

        //综合能力
        #region
        //根据查询条件得出生产途程的最后一道工序的资源
        public object[] GetKpiLastRes(string sscode, string mocode)
        {
            string sql = "SELECT *  FROM TBLRES WHERE SSCODE = '" + sscode + "'AND RESCODE IN (SELECT RESCODE  FROM TBLOP2RES ";
            sql += " WHERE OPCODE IN (SELECT OPCODE FROM (SELECT OPCODE, OPSEQ FROM TBLITEMROUTE2OP WHERE ITEMCODE =";
            sql += "(SELECT ITEMCODE FROM TBLMO WHERE MOCODE = '" + mocode + "') ORDER BY OPSEQ DESC) WHERE ROWNUM = 1))";
            return this.DataProvider.CustomQuery(typeof(Domain.BaseSetting.Resource), new SQLCondition(sql));
        }

        //根据查询资源计算出已完工的产品数量。
        public int GetOutputSum(int starttime, int endtime, string rescode, string shiftcode, string tpcode)
        {
            string sql = "select nvl(SUM(CT.OUTPUTTIMES), 0) AS OUTPUT FROM (SELECT MOCODE,SHIFTDAY,ITEMCODE,TBLMESENTITYLIST_SERIAL,";
            sql += "INPUTTIMES,OUTPUTTIMES,NGTIMES,EATTRIBUTE1 FROM TBLRPTOPQTY) CT LEFT OUTER JOIN TBLMESENTITYLIST TBLMESENTITYLIST";
            sql += " ON TBLMESENTITYLIST.SERIAL = CT.TBLMESENTITYLIST_SERIAL LEFT OUTER JOIN TBLSHIFT TBLSHIFT ON TBLSHIFT.SHIFTCODE = TBLMESENTITYLIST.SHIFTCODE";
            sql += " LEFT OUTER JOIN TBLTP TBLTP ON TBLTP.TPCODE = TBLMESENTITYLIST.TPCODE LEFT OUTER JOIN TBLRES TBLRES ON TBLRES.RESCODE = TBLMESENTITYLIST.RESCODE";
            sql += " WHERE 1 = 1 AND CT.SHIFTDAY >= " + starttime + " AND CT.SHIFTDAY <= " + endtime + " AND TBLMESENTITYLIST.RESCODE = '" + rescode + "'";

            if (!string.IsNullOrEmpty(shiftcode))
            {
                sql += " AND TBLMESENTITYLIST.SHIFTCODE = '" + shiftcode + "'";
            }
            if (!string.IsNullOrEmpty(tpcode))
            {
                sql += " AND TBLMESENTITYLIST.TPCODE = '" + tpcode + "'";
            }
            object obj = this.DataProvider.CustomQuery(typeof(Domain.KPI.Output), new SQLCondition(sql))[0];
            if (obj != null)
            {
                return ((Domain.KPI.Output)obj).outputsum;
            }
            return 0;
        }

        //若产品数量不为0，按查询条件查询出最后一个产品产出时间
        public DateTime GetLastProTime(string rescode, string thiftcode, string tpcode, int begintime, int endtime, string mocode)
        {
            string sql = "SELECT TO_DATE(TRIM(TO_CHAR(MDATE, '00000000')) || TRIM(TO_CHAR(MTIME, '000000')), 'yyyy-mm-dd HH24:mi:ss') AS ENDDATE";
            sql += " FROM TBLONWIP  WHERE SERIAL IN (SELECT MAX(SERIAL) FROM TBLONWIP WHERE RESCODE = '" + rescode + "'";
            if (!string.IsNullOrEmpty(thiftcode))
            {
                sql += " AND SHIFTCODE = '" + thiftcode + "'";
            }
            if (!string.IsNullOrEmpty(thiftcode))
            {
                sql += "  AND TPCODE = '" + tpcode + "'";
            }
            sql += "AND SHIFTDAY >=" + begintime + " AND SHIFTDAY <= " + endtime + " AND MOCODE = '" + mocode + "')";
            object[] objs = this.DataProvider.CustomQuery(typeof(Domain.KPI.EndDateEn), new SQLCondition(sql));
            return ((Domain.KPI.EndDateEn)objs[0]).enddate;
        }

        //按查询条件查询出第一个产品的投入时间
        public DateTime? GetInputTime(string rescode, string shiftcode, string tpcode, int begintime, int endtime, string mocode)
        {
            string sql = "select TO_DATE(TRIM(TO_CHAR(MDATE, '00000000')) || TRIM(TO_CHAR(MTIME, '000000')), 'yyyy-mm-dd HH24:mi:ss') AS BEGINDATE from (";
            sql += " SELECT  t.*,ROW_NUMBER() OVER(ORDER BY serial) AS ROW_INDEX   FROM TBLONWIP t WHERE RESCODE = '"+rescode+"' AND SHIFTDAY >="+begintime;
            sql += " AND SHIFTDAY <= "+endtime+" AND MOCODE = '"+mocode+"'";
            //string sql = "SELECT TO_DATE(TRIM(TO_CHAR(MDATE, '00000000')) || TRIM(TO_CHAR(MTIME, '000000')), 'yyyy-mm-dd HH24:mi:ss') AS BEGINDATE";
            //sql += " FROM TBLONWIP WHERE RCARD IN (SELECT RCARD FROM TBLONWIP WHERE SERIAL IN (SELECT MIN(SERIAL) FROM TBLONWIP WHERE RESCODE = '" + rescode + "'";
            if (!string.IsNullOrEmpty(shiftcode))
            {
                sql += " AND SHIFTCODE = '" + shiftcode + "'";
            }
            if (!string.IsNullOrEmpty(tpcode))
            {
                sql += "  AND TPCODE = '" + tpcode + "'";
            }
            sql += ")   where ROW_INDEX=1";
            //sql += " AND SHIFTDAY >=" + begintime + " AND SHIFTDAY <=" + endtime + " AND MOCODE = '" + mocode + "'))";
            //sql += " AND ROWNUM = 1 ORDER BY SERIAL ASC";
            object[] objs = this.DataProvider.CustomQuery(typeof(Domain.KPI.BeginDateEn), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                return ((Domain.KPI.BeginDateEn)objs[0]).beigindate;
            }
            return null;
        }

        #endregion
        //按照工单，产线和时间等条件查询投入和产出数量
        #region
        public string GetInOut(String mocode, string sscode, int begintime, int endtime, string shiftcode, string tpcode)
        {
            string sql = "SELECT SUM(CT.MOOUTPUTCOUNT) / SUM(CT.MOINPUTCOUNT) AS inandout FROM (SELECT MOCODE,SHIFTDAY,ITEMCODE,TBLMESENTITYLIST_SERIAL,";
            sql += " MOINPUTCOUNT,MOOUTPUTCOUNT,MOLINEOUTPUTCOUNT,MOWHITECARDCOUNT,MOOUTPUTWHITECARDCOUNT, LINEINPUTCOUNT,LINEOUTPUTCOUNT,";
            sql += " OPCOUNT,OPWHITECARDCOUNT,EATTRIBUTE1 FROM TBLRPTSOQTY) CT LEFT OUTER JOIN TBLMESENTITYLIST TBLMESENTITYLIST ON TBLMESENTITYLIST.SERIAL =";
            sql += "CT.TBLMESENTITYLIST_SERIAL LEFT OUTER JOIN TBLSHIFT TBLSHIFT ON TBLSHIFT.SHIFTCODE = TBLMESENTITYLIST.SHIFTCODE LEFT OUTER JOIN TBLTP TBLTP";
            sql += " ON TBLTP.TPCODE = TBLMESENTITYLIST.TPCODE WHERE 1 = 1 AND CT.SHIFTDAY >=" + begintime + " AND CT.SHIFTDAY <= " + endtime;
            if (!string.IsNullOrEmpty(shiftcode))
            {
                sql += " AND TBLMESENTITYLIST.SHIFTCODE = '" + shiftcode + "'";
            }
            if (!string.IsNullOrEmpty(tpcode))
            {
                sql += " AND TBLMESENTITYLIST.TPCODE = '" + tpcode + "'";
            }
            sql += " AND CT.MOCODE='" + mocode + "'AND TBLMESENTITYLIST.SSCODE='" + sscode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(Domain.KPI.InAndOut), new SQLCondition(sql));
            string inAndOut = ((Domain.KPI.InAndOut)objs[0]).inandout;
            if (inAndOut.Equals(""))
            {
                return "0";
            }
            return inAndOut;
        }

        #endregion

        #region 获取实际生产率
        public string GetRealRate(string mocode, string sscode, int begintime, int endtime, string shiftcode)
        {
            if (shiftcode.Equals(""))
            {
                shiftcode = null;
            }
            string sql = "SELECT DECODE(ACT - LOST, 0, 0, ROUND(ACQ / (ACT - LOST), 4)) * 100 || '%' AS RATE FROM (SELECT SUM(ACTMANHOUR) AS ACT,";

            sql += " SUM(LOSTMANHOUR) AS LOST,SUM(ACQUIREMANHOUR) AS ACQ FROM TBLLOSTMANHOUR WHERE 1 = 1 AND SHIFTDATE >=" + begintime + " AND SHIFTDATE <=" + endtime;
            if (shiftcode != null)
            {
                sql += " AND SHIFTCODE = '" + shiftcode + "'";
            }
            sql += " AND ITEMCODE = '" + mocode + "' AND SSCODE = '" + sscode + "')";
            object[] objs = this.DataProvider.CustomQuery(typeof(Domain.KPI.RealRate), new SQLCondition(sql));
            return ((Domain.KPI.RealRate)objs[0]).rate;
        }
        #endregion

        #region 人均生产数
        public object[] QueryPerCapita(string itemcode, string sscode, string shfitcode, string begindate, string enddate, string groupby)
        {
            string sql = string.Format("select {0} ct.ACQ, ct.ACT, ct.WOR, ct.DATETIME, ROUND((ct.ACQ / ct.ACT) * ct.DATETIME / ct.WOR, 2) as PER", groupby.Replace("**", "ct"));
            sql += string.Format(" FROM (SELECT {0} SUM(FLOOR(TO_NUMBER(DECODE(B.ISOVERDAY,1,TO_DATE(DECODE(LENGTH(B.SHIFTETIME),5, 0 || B.SHIFTETIME,B.SHIFTETIME),'hh24:mi:ss') -TO_DATE(DECODE(LENGTH(B.SHIFTBTIME),5,0 || B.SHIFTBTIME,B.SHIFTBTIME),'hh24:mi:ss') + 1,TO_DATE(DECODE(LENGTH(B.SHIFTETIME), 5,0 || B.SHIFTETIME,B.SHIFTETIME),'hh24:mi:ss') -TO_DATE(DECODE(LENGTH(B.SHIFTBTIME), 5,0 || B.SHIFTBTIME,B.SHIFTBTIME),'hh24:mi:ss')) * 24 * 60 * 60))) AS DATETIME,SUM(A.ACQUIREMANHOUR) AS ACQ,SUM(A.ACTMANHOUR) AS ACT,SUM(C.WORKINGTIME) AS WOR", groupby.Replace("**", "A"));
            sql += " FROM TBLLOSTMANHOUR A LEFT JOIN TBLSHIFT B ON A.SHIFTCODE = B.SHIFTCODE  LEFT JOIN TBLPLANWORKTIME C ON C.ITEMCODE = A.ITEMCODE AND C.SSCODE = A.SSCODE WHERE 1=1";
            sql += string.Format(" AND A.SHIFTDATE >= '{0}'", begindate);
            sql += string.Format(" AND A.SHIFTDATE <= '{0}'", enddate);
            if (!string.IsNullOrEmpty(itemcode))
            {
                sql += string.Format(" AND A.ITEMCODE = '{0}'", itemcode);
            }
            if (!string.IsNullOrEmpty(sscode))
            {
                sql += string.Format(" AND A.SSCODE = '{0}'", sscode);
            }
            if (!string.IsNullOrEmpty(shfitcode))
            {
                sql += string.Format(" AND A.SHIFTCODE = '{0}'", shfitcode);
            }
            if (!string.IsNullOrEmpty(groupby))
            {
                sql += string.Format(" GROUP BY {0}", groupby.Replace("**", "A").Substring(0, groupby.Replace("**", "A").Length - 1));
            }
            sql += " )ct";

            return this.DataProvider.CustomQuery(typeof(Domain.KPI.PerCapitaOutput), new SQLCondition(sql));
        }
        #endregion

    }


}
