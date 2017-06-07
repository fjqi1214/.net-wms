using System;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.WatchPanelNew
{
    public class WatchPanelFacade
    {
        public const int TIME_DEFAULT = -1;
        private IDomainDataProvider _domainDataProvider = null;
        public WatchPanelFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }
        protected IDomainDataProvider DataProvider
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

        #region 静态函数
        public static string ProcessQueryValues(string text)
        {
            string[] array = text.ToUpper().Split(new char[] { ',', ';' });

            return ProcessQueryValues(array);
        }

        public static string ProcessQueryValues(string[] array)
        {
            if (array == null || array.Length == 0)
            {
                return "";
            }
            else
            {
                string retText = "";
                foreach (string str in array)
                {
                    retText += "'" + str.Trim() + "',";
                }
                return retText.Substring(0, retText.Length - 1);
            }
        }

        public static DBDateTime GetNowDBDateTime(IDomainDataProvider domainDataProvider)
        {
            object[] objs = domainDataProvider.CustomQuery(typeof(DBDateTime),
                new SQLParamCondition("select to_char(sysdate,'yyyymmdd') as dbdate,to_char(sysdate,'hh24miss')  as dbtime from dual where $RCARD = '1'"
                , new SQLParameter[] { new SQLParameter("RCARD", typeof(string), "1") }));
            if (objs.Length == 0)
                ExceptionManager.Raise(typeof(WatchPanelFacade), "$SystemError_GetDBTimeError");
            return (DBDateTime)objs[0];
        }

        public static DateTime ToDateTime(int date, int time)
        {
            string dateString = date.ToString().PadLeft(8, '0');
            string timeString = time.ToString().PadLeft(6, '0');
            return new DateTime(System.Int32.Parse(dateString.Substring(0, 4)),
                                System.Int32.Parse(dateString.Substring(4, 2)),
                                System.Int32.Parse(dateString.Substring(6, 2)),
                System.Int32.Parse(timeString.Substring(0, 2)),
                System.Int32.Parse(timeString.Substring(2, 2)),
                System.Int32.Parse(timeString.Substring(4, 2)));
        }

        public static string ToTimeString(int time, string timeSplitChar)
        {
            string timeString = time.ToString().PadLeft(6, '0');

            return string.Format("{0}{1}{2}{3}{4}"
                                , timeString.Substring(0, 2)
                                , timeSplitChar
                                , timeString.Substring(2, 2)
                                , timeSplitChar
                                , timeString.Substring(4, 2));
        }

        public static string ToTimeString(int time)
        {
            if (time == WatchPanelFacade.TIME_DEFAULT)
            {
                return string.Empty;
            }
            return ToTimeString(time, ":");
        }
        #endregion

        /// <summary>
        /// ** 功能描述:	获得所有的StepSequence
        /// </summary>
        /// <returns>所有的StepSequence</returns>
        public object[] GetAllStepSequence()
        {
            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(string.Format("select {0} from TBLSS where 1=1 order by SSCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)))));
        }

        public object[] GetAllWorkShop()
        {
            string sql = string.Format(@" SELECT  {0}  FROM  TBLSEG  ORDER  BY   SEGCODE ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Segment)));
            return this.DataProvider.CustomQuery(typeof(Segment), new SQLCondition(sql));
        }

        //查询车间下的所有产线信息
        public object[] GetStepSequenceBySeg(string segment)
        {
            string sql = string.Format(@" SELECT   {0}  FROM  TBLSS  WHERE  1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)));
            if (!string.IsNullOrEmpty(segment))
            {
                sql += string.Format(@"  AND   segCode='{0}' ", segment);
            }
            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(sql));
        }

        public string GetStepSequenceDescription(string ssCode)
        {
            object obj = this.DataProvider.CustomSearch(typeof(StepSequence), new object[] { ssCode });
            if (obj != null)
            {
                return (obj as StepSequence).StepSequenceDescription;
            }
            return string.Empty;
        }

        public object[] QueryStepSequence(string ssCode)
        {
            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(string.Format("select {0} from TBLSS where 1=1 and SSCODE in ({1}) order by SSCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)), ProcessQueryValues(ssCode))));
        }

        public object[] GetAllEquipment()
        {
            return this.DataProvider.CustomQuery(typeof(Equipment), new SQLCondition(string.Format("select {0} from TBLEquipment where 1=1 order by EqpId asc ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Equipment)))));
        }

        public string GetEquipmentData(string eqpId, string eventName)
        {
            string sql = "SELECT REALVALUE FROM TBLEQPDATA WHERE 1=1 ";
            if (eqpId.Trim() != string.Empty)
            {
                sql += " AND EQPID = '" + eqpId.Trim() + "' ";
            }
            if (eventName.Trim() != string.Empty)
            {
                sql += "AND EVENTNAME='" + eventName.Trim() + "' ";
            }
            sql += " ORDER BY MDATE DESC,MTIME DESC";
            object[] obj = this.DataProvider.CustomQuery(typeof(EQPData), new SQLCondition(sql));
            if (obj != null && obj.Length > 0)
            {
                return (obj[0] as EQPData).RealValue.ToString();
            }
            return string.Empty;
        }

        public object GetParameter(string parameterCode, string parameterGroupCode)
        {
            return this.DataProvider.CustomSearch(typeof(Parameter), new object[] { parameterCode, parameterGroupCode });
        }

        public int GetParameterForOp(string parameterCode)
        {
            string sql = string.Format("SELECT NVL(SUM(PARAMALIAS),0) FROM TBLSYSPARAM WHERE PARAMGROUPCODE = 'WATCHPANNEL' AND PARAMCODE IN ({0})", ProcessQueryValues(parameterCode));
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        //产线产量
        public object[] QueryLineQty(int shiftDate, string ssCode)
        {
            string addSSCode = ProcessQueryValues(ssCode);
            string sql = " SELECT tblss.ssdesc,c.* FROM (   ";
            sql += "     SELECT nvl(a.sscode,b.sscode) AS sscode,nvl(SHIFTDAY,plandate) AS shiftday,  NVL(SUM(B.PLANQTY), 0) AS PLANQTY, NVL(SUM(COMPELETEPUT), 0) AS COMPELETEPUT ,      ";
            sql += "       decode(nvl(SUM(PlanQty),0),0,0,SUM(COMPELETEPUT)/SUM(PlanQty)) as achievedrate  FROM ( SELECT SUM(LINEOUTPUTCOUNT) AS COMPELETEPUT,  ";
            sql += "       TBLMESENTITYLIST.SSCODE as SSCODE, TBLMESENTITYLIST.ORGID, SHIFTDAY  FROM TBLRPTSOQTY  ";
            sql += "             LEFT JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = TBLMESENTITYLIST.SERIAL      ";
            sql += "             LEFT JOIN TBLMO ON TBLMO.MOCODE = TBLRPTSOQTY.MOCODE  ";
            sql += "             LEFT JOIN TBLTIMEDIMENSION  ON TBLTIMEDIMENSION.DDATE = SHIFTDAY   ";
            sql += "             WHERE shiftday >= {0} AND shiftday <= {1} AND SSCODE  in ({2})         ";
            sql += "              GROUP BY TBLMESENTITYLIST.SSCODE, TBLMESENTITYLIST.ORGID, SHIFTDAY )  A       ";
            sql += "         FULL JOIN (  SELECT SUM(PLANQTY) PLANQTY, ORGID, PLANDATE, SSCODE, PLANTYPE FROM         ";
            sql += "                 (( SELECT PLANQTY, TBLPLAN.SSCODE as SSCODE, TBLPLAN.ORGID, TBLPLAN.PLANTYPE, TBLPLAN.PLANDATE FROM TBLPLAN,            ";
            sql += "                 (SELECT PLANTYPE, PLANDATE, ORGID, MAX(VERSION) AS VERSION FROM TBLPLANVERSION GROUP BY PLANTYPE, PLANDATE, ORGID) TBLPLANVERSION    ";
            sql += "         WHERE TBLPLANVERSION.PLANTYPE = TBLPLAN.PLANTYPE AND TBLPLANVERSION.PLANDATE = TBLPLAN.PLANDATE AND TBLPLANVERSION.ORGID = TBLPLAN.ORGID   ";
            sql += "            AND TBLPLANVERSION.VERSION = TBLPLAN.VERSION)) WHERE plandate >= {3} AND plandate <= {4} AND SSCODE  in ({5}) ";
            sql += "             GROUP BY ORGID, SSCODE, PLANDATE, PLANTYPE) B ON 1 = 1 AND B.ORGID = A.ORGID AND B.PLANTYPE = 'D'   ";
            sql += "               AND B.PLANDATE = SHIFTDAY AND A.SSCODE = B.SSCODE ";
            sql += "               GROUP BY  a.sscode,b.sscode,SHIFTDAY,plandate HAVING (SUM(COMPELETEPUT) > 0 AND SHIFTDAY IS NOT NULL) OR SUM(B.PLANQTY) > 0  ORDER BY  sscode, SHIFTDAY)c ";
            sql += "     LEFT JOIN TBLSS ON C.SSCODE = TBLSS.SSCODE ORDER BY C.SSCODE DESC ";

            sql = string.Format(sql, shiftDate, shiftDate, addSSCode, shiftDate, shiftDate, addSSCode);
            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        //产线的直通率
        public object[] QueryLineAllGoodQty(int shiftDate, string ssCode)
        {
            string sql = "SELECT TBLSS.SSDESC AS SSDESC,CT.SSCODE, ";
            sql += "       CT.SHIFTDAY AS SHIFTDAY, ";
            sql += "       SUM(CT.LINEOUTPUTCOUNT) AS OUTPUT, ";
            sql += "       SUM(CT.LINEWHITECARDCOUNT) AS PASSRCARDQTY, ";
            sql += "       DECODE(SUM(LINEOUTPUTCOUNT), 0, 0, ";
            sql += "              SUM(LINEWHITECARDCOUNT) / SUM(LINEOUTPUTCOUNT)) AS PASSRCARDRATE, TBLSS.EATTRIBUTE1 ";
            sql += "  FROM (SELECT LINEOUTPUTCOUNT, LINEWHITECARDCOUNT, TBLRPTSOQTY.MOCODE, TBLRPTSOQTY.SHIFTDAY, TBLRPTSOQTY.ITEMCODE, ";
            sql += "               TBLMESENTITYLIST.SHIFTCODE, TBLMESENTITYLIST.SSCODE, TBLMESENTITYLIST.FACCODE, TBLMESENTITYLIST.MODELCODE, ";
            sql += "               TBLMESENTITYLIST.SEGCODE, TBLMESENTITYLIST.OPCODE, TBLMESENTITYLIST.SHIFTTYPECODE, TBLMESENTITYLIST.BIGSSCODE, ";
            sql += "               TBLMESENTITYLIST.TPCODE, TBLMESENTITYLIST.SERIAL, TBLMESENTITYLIST.RESCODE, TBLMESENTITYLIST.ORGID ";
            sql += "          FROM TBLRPTSOQTY ";
            sql += "         INNER JOIN TBLRPTLINEQTY ON TBLRPTSOQTY.MOCODE = ";
            sql += "                                     TBLRPTLINEQTY.MOCODE ";
            sql += "                                 AND TBLRPTSOQTY.SHIFTDAY = ";
            sql += "                                     TBLRPTLINEQTY.SHIFTDAY ";
            sql += "                                 AND TBLRPTSOQTY.ITEMCODE = ";
            sql += "                                     TBLRPTLINEQTY.ITEMCODE ";
            sql += "                                 AND TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                     TBLRPTLINEQTY.TBLMESENTITYLIST_SERIAL ";
            sql += "          LEFT OUTER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                              TBLMESENTITYLIST.SERIAL) CT ";
            sql += "  LEFT OUTER JOIN TBLSS TBLSS ON TBLSS.SSCODE = CT.SSCODE ";
            sql += " WHERE 1 = 1 AND CT.SHIFTDAY >= '{0}' AND CT.SHIFTDAY <= '{1}' AND CT.SSCODE IN ({2}) ";
            sql += " GROUP BY TBLSS.SSDESC,CT.SSCODE, CT.SHIFTDAY,TBLSS.EATTRIBUTE1 ";
            sql += "HAVING SUM(CT.LINEOUTPUTCOUNT) > 0 OR SUM(CT.LINEWHITECARDCOUNT) > 0 OR DECODE(SUM(LINEOUTPUTCOUNT), 0, 0, SUM(LINEWHITECARDCOUNT) / SUM(LINEOUTPUTCOUNT)) > 0 ";
            sql += " ORDER BY CT.SSCODE";
            sql = string.Format(sql, shiftDate, shiftDate, ProcessQueryValues(ssCode));
            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        //车间产量
        public object[] QueryFactoryQty(int shiftDate, string ssCode)
        {
            DateTime dt = DateTime.ParseExact(shiftDate.ToString(), "yyyyMMdd", null).AddDays(-7);
            int beginDate = int.Parse(dt.ToString("yyyyMMdd"));

            string sql = "SELECT CT.SHIFTDAY AS SHIFTDAY,CT.SSCODE,TBLSS.SSDESC, SUM(CT.LINEOUTPUTCOUNT) AS OUTPUT ";
            sql += "  FROM (SELECT MOCODE,SHIFTDAY, ITEMCODE,TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL, MOINPUTCOUNT, ";
            sql += "               MOOUTPUTCOUNT, MOLINEOUTPUTCOUNT,MOWHITECARDCOUNT, MOOUTPUTWHITECARDCOUNT, ";
            sql += "               LINEINPUTCOUNT,LINEOUTPUTCOUNT,OPCOUNT,OPWHITECARDCOUNT,TBLRPTSOQTY.EATTRIBUTE1, ";
            sql += "               TBLMESENTITYLIST.ORGID,TBLMESENTITYLIST.SSCODE ";
            sql += "          FROM TBLRPTSOQTY";
            sql += "          LEFT JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = TBLMESENTITYLIST.SERIAL) CT ";
            sql += "  LEFT OUTER JOIN TBLSS TBLSS ON TBLSS.SSCODE = CT.SSCODE ";
            sql += " WHERE 1 = 1 AND CT.SHIFTDAY >= '{0}' AND CT.SHIFTDAY <= '{1}' ";
            sql += "   AND CT.SSCODE IN ({2}) ";
            sql += " GROUP BY CT.SHIFTDAY,CT.SSCODE,TBLSS.SSDESC  ";
            sql += "HAVING SUM(CT.LINEOUTPUTCOUNT) > 0 ";
            sql += " ORDER BY CT.SSCODE,TBLSS.SSDESC,CT.SHIFTDAY ";
            sql = string.Format(sql, beginDate, shiftDate, ProcessQueryValues(ssCode));
            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        //车间直通率
        public object[] QueryFactoryAllGoodQty(int shiftDate, string ssCode)
        {
            DateTime dt = DateTime.ParseExact(shiftDate.ToString(), "yyyyMMdd", null).AddDays(-7);
            int beginDate = int.Parse(dt.ToString("yyyyMMdd"));

            string sql = "SELECT   CT.SHIFTDAY AS SHIFTDAY, CT.SSCODE, TBLSS.SSDESC, ";
            sql += " SUM (CT.LINEOUTPUTCOUNT) AS OUTPUT, ";
            sql += "          SUM (CT.LINEWHITECARDCOUNT) AS PASSRCARDQTY, ";
            sql += "          DECODE (NVL (SUM (CT.LINEOUTPUTCOUNT), 0), ";
            sql += "                  0, 0, ";
            sql += "                  SUM (CT.LINEWHITECARDCOUNT) / SUM (CT.LINEOUTPUTCOUNT) ";
            sql += "                 ) AS PASSRCARDRATE ";
            sql += "     FROM (SELECT TBLRPTSOQTY.SHIFTDAY, TBLRPTSOQTY.MOOUTPUTCOUNT, ";
            sql += "                  TBLRPTSOQTY.MOINPUTCOUNT, TBLRPTSOQTY.MOOUTPUTWHITECARDCOUNT, ";
            sql += "                  TBLRPTSOQTY.OPCOUNT, TBLRPTSOQTY.EATTRIBUTE1, ";
            sql += "                  TBLRPTSOQTY.LINEOUTPUTCOUNT, TBLRPTSOQTY.MOWHITECARDCOUNT, ";
            sql += "                  TBLRPTSOQTY.MOLINEOUTPUTCOUNT, TBLRPTSOQTY.OPWHITECARDCOUNT, ";
            sql += "                  TBLRPTSOQTY.LINEINPUTCOUNT, ";
            sql += "                  TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL, TBLRPTSOQTY.ITEMCODE, ";
            sql += "                  TBLRPTSOQTY.MOCODE, TBLMESENTITYLIST.SHIFTCODE, ";
            sql += "                  TBLMESENTITYLIST.SSCODE, TBLMESENTITYLIST.FACCODE, ";
            sql += "                  TBLMESENTITYLIST.MODELCODE, TBLMESENTITYLIST.SEGCODE, ";
            sql += "                  TBLMESENTITYLIST.OPCODE, TBLMESENTITYLIST.SHIFTTYPECODE, ";
            sql += "                  TBLMESENTITYLIST.BIGSSCODE, TBLMESENTITYLIST.TPCODE, ";
            sql += "                  TBLMESENTITYLIST.SERIAL, TBLMESENTITYLIST.RESCODE, ";
            sql += "                  TBLMESENTITYLIST.ORGID, TBLRPTLINEQTY.LINEWHITECARDCOUNT ";
            sql += "             FROM TBLRPTSOQTY INNER JOIN TBLRPTLINEQTY ON TBLRPTSOQTY.MOCODE = ";
            sql += "                                                            TBLRPTLINEQTY.MOCODE ";
            sql += "                                                     AND TBLRPTSOQTY.SHIFTDAY = ";
            sql += "                                                            TBLRPTLINEQTY.SHIFTDAY ";
            sql += "                                                     AND TBLRPTSOQTY.ITEMCODE = ";
            sql += "                                                            TBLRPTLINEQTY.ITEMCODE ";
            sql += "                                                     AND TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                                            TBLRPTLINEQTY.TBLMESENTITYLIST_SERIAL ";
            sql += "                  INNER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                                        TBLMESENTITYLIST.SERIAL ";
            sql += "                  ) CT ";
            sql += "  LEFT OUTER JOIN TBLSS TBLSS ON TBLSS.SSCODE = CT.SSCODE ";
            sql += "    WHERE 1 = 1 ";
            sql += " AND CT.SHIFTDAY >= '{0}' AND CT.SHIFTDAY <= '{1}' AND CT.SSCODE IN ({2}) ";
            sql += " GROUP BY CT.SHIFTDAY,CT.SSCODE,TBLSS.SSDESC ";
            sql += "   HAVING SUM (CT.LINEOUTPUTCOUNT) > 0 ";
            sql += "       OR SUM (CT.LINEWHITECARDCOUNT) > 0 ";
            sql += "       OR DECODE (NVL (SUM (CT.LINEOUTPUTCOUNT), 0), ";
            sql += "                  0, 0, ";
            sql += "                  SUM (CT.LINEWHITECARDCOUNT) / SUM (CT.LINEOUTPUTCOUNT) ";
            sql += "                 ) > 0 ";
            sql += " ORDER BY CT.SSCODE,TBLSS.SSDESC,CT.SHIFTDAY ";

            sql = string.Format(sql, beginDate, shiftDate, ProcessQueryValues(ssCode));
            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        //产线日计划量
        public int GetWorkPlanQty(int shiftDate, string ssCode)
        {
            string sql = "SELECT NVL(SUM(PLANQTY),0) AS PLANQTY FROM TBLPLAN A WHERE PLANTYPE = 'D' AND PLANDATE = '{0}' ";
            sql += "   AND SSCODE='{1}' AND VERSION = (SELECT MAX(VERSION) FROM TBLPLANVERSION B ";
            sql += "    WHERE PLANTYPE = 'D' AND PLANDATE = '{2}' AND A.ORGID = B.ORGID) ";
            sql += " ORDER BY SSCODE ";
            sql = string.Format(sql, shiftDate, ssCode, shiftDate);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //产线日下线数量
        public object GetSSCodeOutPutQty(int shiftDate, string ssCode)
        {
            string sql = "SELECT CT.SHIFTDAY AS SHIFTDAY, NVL(SUM(CT.LINEOUTPUTCOUNT),0) AS OUTPUT,TBLMESENTITYLIST.SSCODE|| ' - ' || TBLSS.SSDESC AS SSCODE ";
            sql += "   FROM (SELECT MOCODE,SHIFTDAY,ITEMCODE,TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL, ";
            sql += "                MOINPUTCOUNT,MOOUTPUTCOUNT,MOLINEOUTPUTCOUNT,MOWHITECARDCOUNT, ";
            sql += "                MOOUTPUTWHITECARDCOUNT,LINEINPUTCOUNT,LINEOUTPUTCOUNT,OPCOUNT, ";
            sql += "                OPWHITECARDCOUNT,TBLRPTSOQTY.EATTRIBUTE1,TBLMESENTITYLIST.ORGID ";
            sql += "           FROM TBLRPTSOQTY ";
            sql += "           LEFT JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = TBLMESENTITYLIST.SERIAL) CT ";
            sql += "   LEFT OUTER JOIN TBLMESENTITYLIST TBLMESENTITYLIST ON TBLMESENTITYLIST.SERIAL =CT.TBLMESENTITYLIST_SERIAL ";
            sql += "   LEFT OUTER JOIN TBLSS TBLSS ON TBLSS.SSCODE = TBLMESENTITYLIST.SSCODE ";
            sql += "  WHERE 1 = 1 AND CT.SHIFTDAY >= '{0}'AND CT.SHIFTDAY <= '{1}' AND TBLMESENTITYLIST.SSCODE='{2}' ";
            sql += "  GROUP  BY CT.SHIFTDAY,TBLMESENTITYLIST.SSCODE|| ' - ' || TBLSS.SSDESC ";
            sql += " HAVING SUM(CT.LINEOUTPUTCOUNT) > 0 ORDER BY TBLMESENTITYLIST.SSCODE || ' - ' || TBLSS.SSDESC, CT.SHIFTDAY ";
            sql = string.Format(sql, shiftDate, shiftDate, ssCode);
            object[] obj = this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
            if (obj != null)
            {
                return obj[0];
            }
            return null;
        }

        public object[] QueryExceptionList(string ssCode, int shiftDate)
        {
            /*string sql = " SELECT E.*, C.EXCEPTIONDESC,S.SSDESC FROM TBLEXCEPTION E, TBLEXCEPTIONCODE C, TBLSS S ";
            sql += " WHERE E.SSCODE = S.SSCODE AND C.EXCEPTIONCODE = E.EXCEPTIONCODE ";
            if (ssCode.Trim().Length > 0)
            {
                sql += " AND S.SSCODE = '" + ssCode.Trim().ToUpper() + "' ";
            }
            if (shiftDate > 0)
            {
                sql += " AND E.SHIFTDATE = " + shiftDate + " ";
            }
            sql += " ORDER BY E.BEGINTIME DESC "; */

            string sql = "SELECT noticecontent as EXCEPTIONDESC";
            sql += "  FROM (SELECT DISTINCT a.noticecontent, a.noticedate, c.sscode";
            sql += "                   FROM tblalertnotice a, tblnoticeerror b, tblss c";
            sql += "                  WHERE a.itemsequence = b.itemsequence";
            sql += "                    AND a.subitemsequence = b.subitemsequence";
            sql += "                    AND a.noticedate = b.shiftday";
            sql += "                    AND b.bigsscode = c.bigsscode";
            sql += "        UNION";
            sql += "        SELECT DISTINCT a.noticecontent, a.noticedate, c.sscode";
            sql += "                   FROM tblalertnotice a, tblnoticeerrorcode b, tblss c";
            sql += "                  WHERE a.itemsequence = b.itemsequence";
            sql += "                    AND a.subitemsequence = b.subitemsequence";
            sql += "                    AND a.noticedate = b.shiftday";
            sql += "                    AND b.bigsscode = c.bigsscode";
            sql += "        UNION";
            sql += "        SELECT DISTINCT a.noticecontent, a.noticedate, c.sscode";
            sql += "                   FROM tblalertnotice a, tblnoticedirectpass b, tblss c";
            sql += "                  WHERE a.itemsequence = b.itemsequence";
            sql += "                    AND a.subitemsequence = b.subitemsequence";
            sql += "                    AND a.noticedate = b.shiftday";
            sql += "                    AND b.bigsscode = c.bigsscode";
            sql += "        UNION";
            sql += "        SELECT DISTINCT a.noticecontent, a.noticedate, c.sscode";
            sql += "                   FROM tblalertnotice a, tblnoticelinepause b, tblss c";
            sql += "                  WHERE a.itemsequence = b.itemsequence";
            sql += "                    AND a.subitemsequence = b.subitemsequence";
            sql += "                    AND a.noticedate = b.shiftday";
            sql += "                    AND b.sscode = c.sscode";
            sql += "        UNION";
            sql += "        SELECT DISTINCT a.noticecontent, a.noticedate, c.sscode";
            sql += "                   FROM tblalertnotice a, tblnoticelog b, tblss c";
            sql += "                  WHERE a.itemsequence = b.itemsequence";
            sql += "                    AND a.noticedate = b.shiftday";
            sql += "                    AND b.attribute1 = c.bigsscode)";
            sql += "  WHERE 1=1";

            if (ssCode.Trim().Length > 0)
            {
                sql += " AND SSCODE = '" + ssCode.Trim().ToUpper() + "' ";
            }
            if (shiftDate > 0)
            {
                sql += " AND noticedate = " + shiftDate + " ";
            }
            sql += " ORDER BY noticecontent ";

            return this.DataProvider.CustomQuery(typeof(ExceptionEventWithDescription), new SQLCondition(sql));
        }

        public object GettimePeriod(string ssCode)
        {
            string sql = "SELECT DISTINCT TPCODE,TBLTP.SHIFTTYPECODE FROM TBLTP LEFT JOIN TBLSS ON TBLTP.SHIFTTYPECODE = TBLSS.SHIFTTYPECODE";
            sql += " WHERE ((TPBTIME < TPETIME AND  TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) BETWEEN TPBTIME AND TPETIME) ";
            sql += "      OR (TPBTIME > TPETIME AND  TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) < TPBTIME ";
            sql += "         AND TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) + 240000 BETWEEN   TPBTIME AND TPETIME + 240000)";
            sql += "      OR (TPBTIME > TPETIME  AND  TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) > TPBTIME ";
            sql += "      AND TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) BETWEEN TPBTIME AND TPETIME + 240000))";
            sql += "      AND TBLSS.SSCODE = '" + ssCode.Trim().ToUpper() + "'";
            object[] tpList = this.DataProvider.CustomQuery(typeof(TimePeriod), new SQLCondition(sql));

            if (tpList != null)
            {
                return tpList[0];
            }

            return null;
        }

        public int GetShiftDay(string shiftTypeCode, int cuerrtDate, int cuerrtTime)
        {

            string sql = "SELECT COUNT(*)   FROM tbltp  WHERE shifttypecode = '" + shiftTypeCode.Trim().ToUpper() + "' AND isoverdate = 1   ";
            sql += "   AND (CASE WHEN tpbtime > tpetime THEN tpbtime - 240000 ELSE tpbtime END) <= " + cuerrtTime + "  AND tpetime >= " + cuerrtTime + "";

            int datatime = this.DataProvider.GetCount(new SQLCondition(sql));

            if (datatime > 0)
            {
                DateTime newDatetime = new DateTime(int.Parse(cuerrtDate.ToString().Substring(0, 4)), int.Parse(cuerrtDate.ToString().Substring(4, 2)), int.Parse(cuerrtDate.ToString().Substring(6, 2)));
                cuerrtDate = int.Parse(newDatetime.AddDays(-1).ToString("yyyyMMdd"));
                return cuerrtDate;
            }
            else
            {
                return cuerrtDate;
            }
        }

        public object[] QueryProductData(string ssCode, int shiftDate)
        {
            DBDateTime now = GetNowDBDateTime(this.DataProvider);
            long dateTime = now.DBDate * 1000000L + now.DBTime;

            string sql = "SELECT A.ITEMCODE, ";
            sql += "        A.MDESC AS ITEMNAME, ";
            sql += "        A.OUTPUT AS PERTIMEOUTPUTQTY, ";
            sql += "        B.PLANQTY AS DAYPLANQTY, ";
            sql += "        C.PASSRCARDRATE AS PASSRATE, ";
            sql += "        D.MANHOURPERPRODUCT AS ONENEEDTIME, ";
            sql += "        E.UPPH ";
            sql += "   FROM (  ";
            sql += "         SELECT CT.ITEMCODE AS ITEMCODE, ";
            sql += "                 TBLMATERIAL.MDESC, ";
            sql += "                 SUM(CT.LINEOUTPUTCOUNT) AS OUTPUT ";
            sql += "           FROM (SELECT MOCODE, ";
            sql += "                         SHIFTDAY, ";
            sql += "                         ITEMCODE, ";
            sql += "                         TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL, ";
            sql += "                         MOINPUTCOUNT, ";
            sql += "                         MOOUTPUTCOUNT, ";
            sql += "                         MOLINEOUTPUTCOUNT, ";
            sql += "                         MOWHITECARDCOUNT, ";
            sql += "                         MOOUTPUTWHITECARDCOUNT, ";
            sql += "                         LINEINPUTCOUNT, ";
            sql += "                         LINEOUTPUTCOUNT, ";
            sql += "                         OPCOUNT, ";
            sql += "                         OPWHITECARDCOUNT, ";
            sql += "                         TBLRPTSOQTY.EATTRIBUTE1, ";
            sql += "                         TBLMESENTITYLIST.ORGID ";
            sql += "                    FROM TBLRPTSOQTY ";
            sql += "                    LEFT JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                                  TBLMESENTITYLIST.SERIAL) CT ";
            sql += "           LEFT OUTER JOIN TBLMATERIAL TBLMATERIAL ON TBLMATERIAL.MCODE = ";
            sql += "                                                      CT.ITEMCODE ";
            sql += "                                                  AND TBLMATERIAL.ORGID = ";
            sql += "                                                      CT.ORGID ";
            sql += "           LEFT OUTER JOIN TBLMESENTITYLIST TBLMESENTITYLIST ON TBLMESENTITYLIST.SERIAL = ";
            sql += "                                                                CT.TBLMESENTITYLIST_SERIAL ";
            sql += "          WHERE 1 = 1 ";
            sql += "            AND CT.SHIFTDAY = " + shiftDate;
            sql += "            AND TBLMESENTITYLIST.SSCODE = '" + ssCode.Trim() + "' ";
            sql += "          GROUP BY CT.ITEMCODE, TBLMATERIAL.MDESC ";
            sql += "         HAVING SUM(CT.LINEOUTPUTCOUNT) > 0 ";
            sql += "          ORDER BY CT.ITEMCODE, TBLMATERIAL.MDESC) A ";
            sql += "   LEFT OUTER JOIN (SELECT ITEMCODE, NVL(SUM(PLANQTY), 0) AS PLANQTY ";
            sql += "                      FROM TBLPLAN A ";
            sql += "                     WHERE PLANTYPE = 'D' ";
            sql += "                       AND PLANDATE = '" + shiftDate + "' ";
            sql += "                       AND SSCODE = '" + ssCode.Trim() + "' ";
            sql += "                       AND VERSION = (SELECT MAX(VERSION) ";
            sql += "                                        FROM TBLPLANVERSION B ";
            sql += "                                       WHERE PLANTYPE = 'D' ";
            sql += "                                         AND PLANDATE = '" + shiftDate + "' ";
            sql += "                                         AND A.ORGID = B.ORGID) ";
            sql += "                     GROUP BY ITEMCODE) B ON A.ITEMCODE = B.ITEMCODE ";
            sql += "   LEFT OUTER JOIN (SELECT CT.ITEMCODE AS ITEMCODE, ";
            sql += "                            SUM(CT.LINEOUTPUTCOUNT) AS OUTPUT, ";
            sql += "                            SUM(CT.LINEWHITECARDCOUNT) AS PASSRCARDQTY, ";
            sql += "                            DECODE(SUM(LINEOUTPUTCOUNT), ";
            sql += "                                   0, ";
            sql += "                                   0, ";
            sql += "                                   SUM(LINEWHITECARDCOUNT) / ";
            sql += "                                   SUM(LINEOUTPUTCOUNT)) AS PASSRCARDRATE ";
            sql += "                      FROM (SELECT LINEOUTPUTCOUNT, ";
            sql += "                                    LINEWHITECARDCOUNT,MOOUTPUTCOUNT,MOINPUTCOUNT,MOOUTPUTWHITECARDCOUNT,MOLINEOUTPUTCOUNT, ";
            sql += "                                    TBLRPTSOQTY.MOCODE, ";
            sql += "                                    TBLRPTSOQTY.SHIFTDAY, ";
            sql += "                                    TBLRPTSOQTY.ITEMCODE, ";
            sql += "                                    TBLMESENTITYLIST.SHIFTCODE, ";
            sql += "                                    TBLMESENTITYLIST.SSCODE, ";
            sql += "                                    TBLMESENTITYLIST.FACCODE, ";
            sql += "                                    TBLMESENTITYLIST.MODELCODE, ";
            sql += "                                    TBLMESENTITYLIST.SEGCODE, ";
            sql += "                                    TBLMESENTITYLIST.OPCODE, ";
            sql += "                                    TBLMESENTITYLIST.SHIFTTYPECODE, ";
            sql += "                                    TBLMESENTITYLIST.BIGSSCODE, ";
            sql += "                                    TBLMESENTITYLIST.TPCODE, ";
            sql += "                                    TBLMESENTITYLIST.SERIAL, ";
            sql += "                                    TBLMESENTITYLIST.RESCODE, ";
            sql += "                                    TBLMESENTITYLIST.ORGID ";
            sql += "                               FROM TBLRPTSOQTY ";
            sql += "                              INNER JOIN TBLRPTLINEQTY ON TBLRPTSOQTY.MOCODE = ";
            sql += "                                                          TBLRPTLINEQTY.MOCODE ";
            sql += "                                                      AND TBLRPTSOQTY.SHIFTDAY = ";
            sql += "                                                          TBLRPTLINEQTY.SHIFTDAY ";
            sql += "                                                      AND TBLRPTSOQTY.ITEMCODE = ";
            sql += "                                                          TBLRPTLINEQTY.ITEMCODE ";
            sql += "                                                      AND TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                                          TBLRPTLINEQTY.TBLMESENTITYLIST_SERIAL ";
            sql += "                               LEFT OUTER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                                                   TBLMESENTITYLIST.SERIAL) CT ";
            sql += "                     WHERE 1 = 1 ";
            sql += "                       AND CT.SHIFTDAY = " + shiftDate;
            sql += "                       AND CT.SSCODE = '" + ssCode.Trim() + "' ";
            sql += "                     GROUP BY CT.ITEMCODE ";
            sql += "                    HAVING SUM(CT.LINEOUTPUTCOUNT) > 0 OR SUM(CT.LINEWHITECARDCOUNT) > 0 OR DECODE(SUM(LINEOUTPUTCOUNT), 0, 0, SUM(LINEWHITECARDCOUNT) / SUM(LINEOUTPUTCOUNT)) > 0 ";
            sql += "                     ORDER BY CT.ITEMCODE) C ON C.ITEMCODE = A.ITEMCODE ";
            sql += "   LEFT OUTER JOIN (SELECT CT.ITEMCODE AS ITEMCODE, ";
            sql += "                            DECODE(SUM(STANDARDQTY), ";
            sql += "                                   0, ";
            sql += "                                   0, ";
            sql += "                                   (SUM(MANHOUR)) / SUM(STANDARDQTY)) AS MANHOURPERPRODUCT, ";
            sql += "                            SUM(STANDARDQTY) AS STANDARDQTY, ";
            sql += "                            SUM(MANHOUR) AS MANHOURSUM ";
            sql += "                      FROM (SELECT A.SHIFTDATE AS SHIFTDAY, ";
            sql += "                                    A.SHIFTCODE, ";
            sql += "                                    A.SSCODE, ";
            sql += "                                    A.SEGCODE, ";
            sql += "                                    TBLSS.BIGSSCODE, ";
            sql += "                                    A.ITEMCODE, ";
            sql += "                                    A.WORKINGTIME, ";
            sql += "                                    A.CYCLETIME, ";
            sql += "                                    A.MANHOUR, ";
            sql += "                                    A.DURATION, ";
            sql += "                                    A.PLANQTY, ";
            sql += "                                    A.REALQTY, ";
            sql += "                                    A.ACQUIREDMANHOUR, ";
            sql += "                                    A.STANDARDQTY, ";
            sql += "                                    A.LOSTMANHOUR1, ";
            sql += "                                    A.LOSTMANHOUR2, ";
            sql += "                                    A.ORGID, ";
            sql += "                                    A.MOCODE ";
            sql += "                               FROM (SELECT SUM(NVL(TBLPRODDETAIL.MANHOUR, 0)) / ";
            sql += "                                            3600.0 AS MANHOUR, ";
            sql += "                                            SUM(NVL(TBLPRODDETAIL.DURATION, 0)) / ";
            sql += "                                            3600.0 AS DURATION, ";
            sql += "                                            SUM(DECODE(NVL(TBLPLANWORKTIME.CYCLETIME, ";
            sql += "                                                           0), ";
            sql += "                                                       0, ";
            sql += "                                                       0, ";
            sql += "                                                       NVL(TBLPRODDETAIL.DURATION, ";
            sql += "                                                           0) / NVL(TBLPLANWORKTIME.CYCLETIME, ";
            sql += "                                                                    0))) AS PLANQTY, ";
            sql += "                                            SUM(NVL(TBLRPTSOQTY.QTY, 0)) AS REALQTY, ";
            sql += "                                            SUM(NVL(TBLRPTSOQTY.QTY, 0) * ";
            sql += "                                                NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                    0)) / 3600.0 AS ACQUIREDMANHOUR, ";
            sql += "                                            SUM(DECODE(10, ";
            sql += "                                                       0, ";
            sql += "                                                       0, ";
            sql += "                                                       NVL(TBLRPTSOQTY.QTY, 0) * ";
            sql += "                                                       NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                           0) / 10)) AS STANDARDQTY, ";
            sql += "                                            SUM((DECODE(NVL(TBLPLANWORKTIME.CYCLETIME, ";
            sql += "                                                            0), ";
            sql += "                                                        0, ";
            sql += "                                                        0, ";
            sql += "                                                        NVL(TBLPRODDETAIL.DURATION, ";
            sql += "                                                            0) / NVL(TBLPLANWORKTIME.CYCLETIME, ";
            sql += "                                                                     0)) - ";
            sql += "                                                NVL(TBLRPTSOQTY.QTY, 0)) * ";
            sql += "                                                NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                    0)) / 3600.0 AS LOSTMANHOUR1, ";
            sql += "                                            SUM(NVL(TBLPRODDETAIL.MANHOUR, 0) - ";
            sql += "                                                NVL(TBLRPTSOQTY.QTY, 0) * ";
            sql += "                                                NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                    0)) / 3600.0 AS LOSTMANHOUR2, ";
            sql += "                                            MAX(NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                    0)) AS WORKINGTIME, ";
            sql += "                                            MAX(NVL(TBLPLANWORKTIME.CYCLETIME, 0)) AS CYCLETIME, ";
            sql += "                                            TBLRPTSOQTY.ORGID, ";
            sql += "                                            TBLPRODDETAIL.SHIFTDATE, ";
            sql += "                                            TBLPRODDETAIL.SHIFTCODE, ";
            sql += "                                            TBLPRODDETAIL.SSCODE, ";
            sql += "                                            TBLPRODDETAIL.ITEMCODE, ";
            sql += "                                            TBLPRODDETAIL.SEGCODE, ";
            sql += "                                            TBLPRODDETAIL.MOCODE ";
            sql += "                                       FROM (SELECT TBLPRODDETAIL.SHIFTDATE, ";
            sql += "                                                    TBLPRODDETAIL.SHIFTCODE, ";
            sql += "                                                    TBLPRODDETAIL.SSCODE, ";
            sql += "                                                    TBLPRODDETAIL.MOCODE, ";
            sql += "                                                    TBLMO.ITEMCODE, ";
            sql += "                                                    TBLSS.BIGSSCODE, ";
            sql += "                                                    TBLSS.SEGCODE, ";
            sql += "                                                    TBLSEG.FACCODE, ";
            sql += "                                                    SUM(NVL(MANCOUNT, 0)) AS MANCOUNT, ";
            sql += "                                                    SUM(NVL(DURATION, 0)) AS DURATION, ";
            sql += "                                                    SUM(NVL(MANCOUNT, 0) * ";
            sql += "                                                        (CASE ";
            sql += "                                                           WHEN " + dateTime.ToString() + " <= ";
            sql += "                                                                ENDDATE * 1000000 + ";
            sql += "                                                                ENDTIME AND ";
            sql += "                                                                " + dateTime.ToString() + " >= ";
            sql += "                                                                BEGINDATE * 1000000 + ";
            sql += "                                                                BEGINTIME THEN ";
            sql += "                                                            (TO_DATE('" + dateTime.ToString() + "', ";
            sql += "                                                                     'yyyymmddhh24miss') - ";
            sql += "                                                            TO_DATE(TO_CHAR(BEGINDATE * ";
            sql += "                                                                             1000000 + ";
            sql += "                                                                             BEGINTIME), ";
            sql += "                                                                     'yyyymmddhh24miss')) * 24 * 60 * 60 ";
            sql += "                                                           ELSE ";
            sql += "                                                            NVL(DURATION, 0) ";
            sql += "                                                         END)) AS MANHOUR ";
            sql += "                                               FROM TBLPRODDETAIL, ";
            sql += "                                                    TBLMO, ";
            sql += "                                                    TBLSS, ";
            sql += "                                                    TBLSEG ";
            sql += "                                              WHERE TBLPRODDETAIL.MOCODE = ";
            sql += "                                                    TBLMO.MOCODE ";
            sql += "                                                AND TBLPRODDETAIL.SSCODE = ";
            sql += "                                                    TBLSS.SSCODE ";
            sql += "                                                AND TBLSS.SEGCODE = TBLSEG.SEGCODE ";
            sql += "                                              GROUP BY TBLPRODDETAIL.SHIFTDATE, ";
            sql += "                                                       TBLPRODDETAIL.SHIFTCODE, ";
            sql += "                                                       TBLPRODDETAIL.SSCODE, ";
            sql += "                                                       TBLPRODDETAIL.MOCODE, ";
            sql += "                                                       TBLMO.ITEMCODE, ";
            sql += "                                                       TBLSS.BIGSSCODE, ";
            sql += "                                                       TBLSS.SEGCODE, ";
            sql += "                                                       TBLSEG.FACCODE) TBLPRODDETAIL ";
            sql += "                                       LEFT OUTER JOIN (SELECT SHIFTDAY, ";
            sql += "                                                              MOCODE, ";
            sql += "                                                              ITEMCODE, ";
            sql += "                                                              BIGSSCODE, ";
            sql += "                                                              SEGCODE, ";
            sql += "                                                              SSCODE, ";
            sql += "                                                              SHIFTCODE, ";
            sql += "                                                              FACCODE, ";
            sql += "                                                              SUM(TBLRPTSOQTY.LINEOUTPUTCOUNT) AS QTY, ";
            sql += "                                                              TBLMESENTITYLIST.ORGID ";
            sql += "                                                         FROM TBLRPTSOQTY ";
            sql += "                                                        INNER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                                                                       TBLMESENTITYLIST.SERIAL ";
            sql += "                                                        GROUP BY SHIFTDAY, ";
            sql += "                                                                 MOCODE, ";
            sql += "                                                                 ITEMCODE, ";
            sql += "                                                                 BIGSSCODE, ";
            sql += "                                                                 SEGCODE, ";
            sql += "                                                                 SSCODE, ";
            sql += "                                                                 SHIFTCODE, ";
            sql += "                                                                 FACCODE, ";
            sql += "                                                                 TBLMESENTITYLIST.ORGID) TBLRPTSOQTY ON TBLRPTSOQTY.SSCODE = ";
            sql += "                                                                                                        TBLPRODDETAIL.SSCODE ";
            sql += "                                                                                                    AND TBLRPTSOQTY.SHIFTDAY = ";
            sql += "                                                                                                        TBLPRODDETAIL.SHIFTDATE ";
            sql += "                                                                                                    AND TBLRPTSOQTY.SHIFTCODE = ";
            sql += "                                                                                                        TBLPRODDETAIL.SHIFTCODE ";
            sql += "                                                                                                    AND TBLRPTSOQTY.MOCODE = ";
            sql += "                                                                                                        TBLPRODDETAIL.MOCODE ";
            sql += "                                       LEFT OUTER JOIN TBLPLANWORKTIME ON TBLPLANWORKTIME.ITEMCODE = ";
            sql += "                                                                          TBLPRODDETAIL.ITEMCODE ";
            sql += "                                                                      AND TBLPLANWORKTIME.SSCODE = ";
            sql += "                                                                          TBLPRODDETAIL.SSCODE ";
            sql += "                                      GROUP BY TBLPRODDETAIL.SHIFTDATE, ";
            sql += "                                               TBLPRODDETAIL.SHIFTCODE, ";
            sql += "                                               TBLPRODDETAIL.SSCODE, ";
            sql += "                                               TBLPRODDETAIL.ITEMCODE, ";
            sql += "                                               TBLPRODDETAIL.SEGCODE, ";
            sql += "                                               TBLPRODDETAIL.MOCODE, ";
            sql += "                                               TBLRPTSOQTY.ORGID) A ";
            sql += "                               LEFT OUTER JOIN TBLSS ON TBLSS.SSCODE = A.SSCODE) CT ";
            sql += "                     WHERE 1 = 1 ";
            sql += "                       AND CT.SHIFTDAY = " + shiftDate;
            sql += "                       AND CT.SSCODE = '" + ssCode.Trim() + "' ";
            sql += "                     GROUP BY CT.ITEMCODE ";
            sql += "                     ORDER BY CT.ITEMCODE) D ON A.ITEMCODE = D.ITEMCODE ";
            sql += "  ";
            sql += "   LEFT OUTER JOIN (SELECT CT.ITEMCODE AS ITEMCODE, ";
            sql += "                             ";
            sql += "                            DECODE(SUM(MANHOUR), ";
            sql += "                                   0, ";
            sql += "                                   0, ";
            sql += "                                   SUM(REALQTY) / (SUM(MANHOUR))) AS UPPH, ";
            sql += "                            SUM(REALQTY) AS REALQTY, ";
            sql += "                            SUM(MANHOUR) AS MANHOURSUM ";
            sql += "                      FROM (SELECT A.SHIFTDATE AS SHIFTDAY, ";
            sql += "                                    A.SHIFTCODE, ";
            sql += "                                    A.SSCODE, ";
            sql += "                                    A.SEGCODE, ";
            sql += "                                    TBLSS.BIGSSCODE, ";
            sql += "                                    A.ITEMCODE, ";
            sql += "                                    A.WORKINGTIME, ";
            sql += "                                    A.CYCLETIME, ";
            sql += "                                    A.MANHOUR, ";
            sql += "                                    A.DURATION, ";
            sql += "                                    A.PLANQTY, ";
            sql += "                                    A.REALQTY, ";
            sql += "                                    A.ACQUIREDMANHOUR, ";
            sql += "                                    A.STANDARDQTY, ";
            sql += "                                    A.LOSTMANHOUR1, ";
            sql += "                                    A.LOSTMANHOUR2, ";
            sql += "                                    A.ORGID, ";
            sql += "                                    A.MOCODE ";
            sql += "                               FROM (SELECT SUM(NVL(TBLPRODDETAIL.MANHOUR, 0)) / ";
            sql += "                                            3600.0 AS MANHOUR, ";
            sql += "                                            SUM(NVL(TBLPRODDETAIL.DURATION, 0)) / ";
            sql += "                                            3600.0 AS DURATION, ";
            sql += "                                            SUM(DECODE(NVL(TBLPLANWORKTIME.CYCLETIME, ";
            sql += "                                                           0), ";
            sql += "                                                       0, ";
            sql += "                                                       0, ";
            sql += "                                                       NVL(TBLPRODDETAIL.DURATION, ";
            sql += "                                                           0) / NVL(TBLPLANWORKTIME.CYCLETIME, ";
            sql += "                                                                    0))) AS PLANQTY, ";
            sql += "                                            SUM(NVL(TBLRPTSOQTY.QTY, 0)) AS REALQTY, ";
            sql += "                                            SUM(NVL(TBLRPTSOQTY.QTY, 0) * ";
            sql += "                                                NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                    0)) / 3600.0 AS ACQUIREDMANHOUR, ";
            sql += "                                            SUM(DECODE(10, ";
            sql += "                                                       0, ";
            sql += "                                                       0, ";
            sql += "                                                       NVL(TBLRPTSOQTY.QTY, 0) * ";
            sql += "                                                       NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                           0) / 10)) AS STANDARDQTY, ";
            sql += "                                            SUM((DECODE(NVL(TBLPLANWORKTIME.CYCLETIME, ";
            sql += "                                                            0), ";
            sql += "                                                        0, ";
            sql += "                                                        0, ";
            sql += "                                                        NVL(TBLPRODDETAIL.DURATION, ";
            sql += "                                                            0) / NVL(TBLPLANWORKTIME.CYCLETIME, ";
            sql += "                                                                     0)) - ";
            sql += "                                                NVL(TBLRPTSOQTY.QTY, 0)) * ";
            sql += "                                                NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                    0)) / 3600.0 AS LOSTMANHOUR1, ";
            sql += "                                            SUM(NVL(TBLPRODDETAIL.MANHOUR, 0) - ";
            sql += "                                                NVL(TBLRPTSOQTY.QTY, 0) * ";
            sql += "                                                NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                    0)) / 3600.0 AS LOSTMANHOUR2, ";
            sql += "                                            MAX(NVL(TBLPLANWORKTIME.WORKINGTIME, ";
            sql += "                                                    0)) AS WORKINGTIME, ";
            sql += "                                            MAX(NVL(TBLPLANWORKTIME.CYCLETIME, 0)) AS CYCLETIME, ";
            sql += "                                            TBLRPTSOQTY.ORGID, ";
            sql += "                                            TBLPRODDETAIL.SHIFTDATE, ";
            sql += "                                            TBLPRODDETAIL.SHIFTCODE, ";
            sql += "                                            TBLPRODDETAIL.SSCODE, ";
            sql += "                                            TBLPRODDETAIL.ITEMCODE, ";
            sql += "                                            TBLPRODDETAIL.SEGCODE, ";
            sql += "                                            TBLPRODDETAIL.MOCODE ";
            sql += "                                       FROM (SELECT TBLPRODDETAIL.SHIFTDATE, ";
            sql += "                                                    TBLPRODDETAIL.SHIFTCODE, ";
            sql += "                                                    TBLPRODDETAIL.SSCODE, ";
            sql += "                                                    TBLPRODDETAIL.MOCODE, ";
            sql += "                                                    TBLMO.ITEMCODE, ";
            sql += "                                                    TBLSS.BIGSSCODE, ";
            sql += "                                                    TBLSS.SEGCODE, ";
            sql += "                                                    TBLSEG.FACCODE, ";
            sql += "                                                    SUM(NVL(MANCOUNT, 0)) AS MANCOUNT, ";
            sql += "                                                    SUM(NVL(DURATION, 0)) AS DURATION, ";
            sql += "                                                    SUM(NVL(MANCOUNT, 0) * ";
            sql += "                                                        (CASE ";
            sql += "                                                           WHEN " + dateTime.ToString() + " <= ";
            sql += "                                                                ENDDATE * 1000000 + ";
            sql += "                                                                ENDTIME AND ";
            sql += "                                                                " + dateTime.ToString() + " >= ";
            sql += "                                                                BEGINDATE * 1000000 + ";
            sql += "                                                                BEGINTIME THEN ";
            sql += "                                                            (TO_DATE('" + dateTime.ToString() + "', ";
            sql += "                                                                     'yyyymmddhh24miss') - ";
            sql += "                                                            TO_DATE(TO_CHAR(BEGINDATE * ";
            sql += "                                                                             1000000 + ";
            sql += "                                                                             BEGINTIME), ";
            sql += "                                                                     'yyyymmddhh24miss')) * 24 * 60 * 60 ";
            sql += "                                                           ELSE ";
            sql += "                                                            NVL(DURATION, 0) ";
            sql += "                                                         END)) AS MANHOUR ";
            sql += "                                               FROM TBLPRODDETAIL, ";
            sql += "                                                    TBLMO, ";
            sql += "                                                    TBLSS, ";
            sql += "                                                    TBLSEG ";
            sql += "                                              WHERE TBLPRODDETAIL.MOCODE = ";
            sql += "                                                    TBLMO.MOCODE ";
            sql += "                                                AND TBLPRODDETAIL.SSCODE = ";
            sql += "                                                    TBLSS.SSCODE ";
            sql += "                                                AND TBLSS.SEGCODE = TBLSEG.SEGCODE ";
            sql += "                                              GROUP BY TBLPRODDETAIL.SHIFTDATE, ";
            sql += "                                                       TBLPRODDETAIL.SHIFTCODE, ";
            sql += "                                                       TBLPRODDETAIL.SSCODE, ";
            sql += "                                                       TBLPRODDETAIL.MOCODE, ";
            sql += "                                                       TBLMO.ITEMCODE, ";
            sql += "                                                       TBLSS.BIGSSCODE, ";
            sql += "                                                       TBLSS.SEGCODE, ";
            sql += "                                                       TBLSEG.FACCODE) TBLPRODDETAIL ";
            sql += "                                       LEFT OUTER JOIN (SELECT SHIFTDAY, ";
            sql += "                                                              MOCODE, ";
            sql += "                                                              ITEMCODE, ";
            sql += "                                                              BIGSSCODE, ";
            sql += "                                                              SEGCODE, ";
            sql += "                                                              SSCODE, ";
            sql += "                                                              SHIFTCODE, ";
            sql += "                                                              FACCODE, ";
            sql += "                                                              SUM(TBLRPTSOQTY.LINEOUTPUTCOUNT) AS QTY, ";
            sql += "                                                              TBLMESENTITYLIST.ORGID ";
            sql += "                                                         FROM TBLRPTSOQTY ";
            sql += "                                                        INNER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                                                                       TBLMESENTITYLIST.SERIAL ";
            sql += "                                                        GROUP BY SHIFTDAY, ";
            sql += "                                                                 MOCODE, ";
            sql += "                                                                 ITEMCODE, ";
            sql += "                                                                 BIGSSCODE, ";
            sql += "                                                                 SEGCODE, ";
            sql += "                                                                 SSCODE, ";
            sql += "                                                                 SHIFTCODE, ";
            sql += "                                                                 FACCODE, ";
            sql += "                                                                 TBLMESENTITYLIST.ORGID) TBLRPTSOQTY ON TBLRPTSOQTY.SSCODE = ";
            sql += "                                                                                                        TBLPRODDETAIL.SSCODE ";
            sql += "                                                                                                    AND TBLRPTSOQTY.SHIFTDAY = ";
            sql += "                                                                                                        TBLPRODDETAIL.SHIFTDATE ";
            sql += "                                                                                                    AND TBLRPTSOQTY.SHIFTCODE = ";
            sql += "                                                                                                        TBLPRODDETAIL.SHIFTCODE ";
            sql += "                                                                                                    AND TBLRPTSOQTY.MOCODE = ";
            sql += "                                                                                                        TBLPRODDETAIL.MOCODE ";
            sql += "                                       LEFT OUTER JOIN TBLPLANWORKTIME ON TBLPLANWORKTIME.ITEMCODE = ";
            sql += "                                                                          TBLPRODDETAIL.ITEMCODE ";
            sql += "                                                                      AND TBLPLANWORKTIME.SSCODE = ";
            sql += "                                                                          TBLPRODDETAIL.SSCODE ";
            sql += "                                      GROUP BY TBLPRODDETAIL.SHIFTDATE, ";
            sql += "                                               TBLPRODDETAIL.SHIFTCODE, ";
            sql += "                                               TBLPRODDETAIL.SSCODE, ";
            sql += "                                               TBLPRODDETAIL.ITEMCODE, ";
            sql += "                                               TBLPRODDETAIL.SEGCODE, ";
            sql += "                                               TBLPRODDETAIL.MOCODE, ";
            sql += "                                               TBLRPTSOQTY.ORGID) A ";
            sql += "                               LEFT OUTER JOIN TBLSS ON TBLSS.SSCODE = A.SSCODE) CT ";
            sql += "                     WHERE 1 = 1 ";
            sql += "                       AND CT.SHIFTDAY = " + shiftDate;
            sql += "                       AND CT.SSCODE = '" + ssCode.Trim() + "' ";
            sql += "                     GROUP BY CT.ITEMCODE ";
            sql += "                     ORDER BY CT.ITEMCODE) E ON A.ITEMCODE = E.ITEMCODE   ";



            return this.DataProvider.CustomQuery(typeof(WatchPanelProductDate), new SQLCondition(sql));
        }

        //产线电子看板产量
        public object[] QuerySSCodeQty(int shiftDate, string ssCode)
        {
            DateTime dt = DateTime.ParseExact(shiftDate.ToString(), "yyyyMMdd", null).AddDays(-7);
            int beginDate = int.Parse(dt.ToString("yyyyMMdd"));

            string sql = "SELECT CT.SHIFTDAY AS SHIFTDAY, SUM(CT.LINEOUTPUTCOUNT) AS OUTPUT ";
            sql += "  FROM (SELECT MOCODE,SHIFTDAY, ITEMCODE,TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL, MOINPUTCOUNT, ";
            sql += "               MOOUTPUTCOUNT, MOLINEOUTPUTCOUNT,MOWHITECARDCOUNT, MOOUTPUTWHITECARDCOUNT, ";
            sql += "               LINEINPUTCOUNT,LINEOUTPUTCOUNT,OPCOUNT,OPWHITECARDCOUNT,TBLRPTSOQTY.EATTRIBUTE1, ";
            sql += "               TBLMESENTITYLIST.ORGID,TBLMESENTITYLIST.SSCODE ";
            sql += "          FROM TBLRPTSOQTY";
            sql += "          LEFT JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = TBLMESENTITYLIST.SERIAL) CT ";
            sql += " WHERE 1 = 1 AND CT.SHIFTDAY >= '{0}' AND CT.SHIFTDAY <= '{1}' ";
            sql += "   AND CT.SSCODE = '{2}' ";
            sql += " GROUP BY CT.SHIFTDAY ";
            sql += "HAVING SUM(CT.LINEOUTPUTCOUNT) > 0 ";
            sql += " ORDER BY CT.SHIFTDAY ";
            sql = string.Format(sql, beginDate, shiftDate, ssCode);
            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        //产线电子看板直通率
        public object[] QuerySSCodeAllGoodQty(int shiftDate, string ssCode)
        {
            DateTime dt = DateTime.ParseExact(shiftDate.ToString(), "yyyyMMdd", null).AddDays(-7);
            int beginDate = int.Parse(dt.ToString("yyyyMMdd"));

            string sql = "SELECT CT.SHIFTDAY AS SHIFTDAY, ";
            sql += "       SUM(CT.LINEOUTPUTCOUNT) AS OUTPUT, ";
            sql += "       SUM(CT.LINEWHITECARDCOUNT) AS PASSRCARDQTY, ";
            sql += "       DECODE(NVL(SUM(CT.LINEOUTPUTCOUNT), 0), 0, 0, SUM(CT.LINEWHITECARDCOUNT) / SUM(CT.LINEOUTPUTCOUNT)) AS PASSRCARDRATE ";
            sql += "  FROM (SELECT TBLRPTSOQTY.SHIFTDAY, TBLRPTSOQTY.MOOUTPUTCOUNT, TBLRPTSOQTY.MOINPUTCOUNT,TBLRPTSOQTY.MOOUTPUTWHITECARDCOUNT,TBLRPTSOQTY.OPCOUNT, ";
            sql += "               TBLRPTSOQTY.EATTRIBUTE1,TBLRPTSOQTY.LINEOUTPUTCOUNT,TBLRPTSOQTY.MOWHITECARDCOUNT, TBLRPTSOQTY.MOLINEOUTPUTCOUNT,TBLRPTSOQTY.OPWHITECARDCOUNT, ";
            sql += "               TBLRPTSOQTY.LINEINPUTCOUNT, TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL, TBLRPTSOQTY.ITEMCODE,TBLRPTSOQTY.MOCODE, TBLMESENTITYLIST.SHIFTCODE, ";
            sql += "               TBLMESENTITYLIST.SSCODE,TBLMESENTITYLIST.FACCODE, TBLMESENTITYLIST.MODELCODE, TBLMESENTITYLIST.SEGCODE,TBLMESENTITYLIST.OPCODE, ";
            sql += "               TBLMESENTITYLIST.SHIFTTYPECODE, TBLMESENTITYLIST.BIGSSCODE, TBLMESENTITYLIST.TPCODE,TBLMESENTITYLIST.SERIAL, TBLMESENTITYLIST.RESCODE, ";
            sql += "               TBLMESENTITYLIST.ORGID,TBLRPTLINEQTY.LINEWHITECARDCOUNT";
            sql += "          FROM TBLRPTSOQTY";
            sql += "          INNER JOIN TBLRPTLINEQTY ON TBLRPTSOQTY.MOCODE = ";
            sql += "                                                          TBLRPTLINEQTY.MOCODE ";
            sql += "                                                      AND TBLRPTSOQTY.SHIFTDAY = ";
            sql += "                                                          TBLRPTLINEQTY.SHIFTDAY ";
            sql += "                                                      AND TBLRPTSOQTY.ITEMCODE = ";
            sql += "                                                          TBLRPTLINEQTY.ITEMCODE ";
            sql += "                                                      AND TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = ";
            sql += "                                                          TBLRPTLINEQTY.TBLMESENTITYLIST_SERIAL ";
            sql += "         INNER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = TBLMESENTITYLIST.SERIAL) CT";
            sql += " WHERE 1 = 1 AND CT.SHIFTDAY >= '{0}' AND CT.SHIFTDAY <= '{1}' AND CT.SSCODE = '{2}' ";
            sql += " GROUP BY CT.SHIFTDAY ";
            sql += "HAVING SUM(CT.LINEOUTPUTCOUNT) > 0 OR SUM(CT.LINEWHITECARDCOUNT) > 0 OR DECODE(NVL(SUM(CT.LINEOUTPUTCOUNT), 0), 0, 0, SUM(CT.LINEWHITECARDCOUNT) / SUM(CT.LINEOUTPUTCOUNT)) > 0  ORDER BY CT.SHIFTDAY ";
            sql = string.Format(sql, beginDate, shiftDate, ssCode);
            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        public string QueryDPPM(int shiftDate, string ssCode, string opCode)
        {
            string ssCodeShift = ProcessQueryValues(ssCode.Trim());
            string opCodeShift = ProcessQueryValues(opCode.Trim());

            string sql = " SELECT CT.OPCODE || ' - ' || TBLOP.OPDESC AS OPCODE, CT.SHIFTDAY AS SHIFTDAY, SUM(ALLCOUNT) AS ALLCOUNT,      ";
            sql += "        SUM(SPOTCOUNT) AS SPOTCOUNT, ";
            sql += "        (CASE SUM(ALLCOUNT) WHEN 0 THEN 0  ";
            sql += "          ELSE SUM(SPOTCOUNT) / SUM(ALLCOUNT) * 1000000 END) AS DPPM     ";
            sql += "     FROM (SELECT A.SHIFTDAY,A.ITEMCODE, A.MODELCODE, ";
            sql += "            A.MMACHINETYPE,A.MOCODE,A.ORGID,A.SSCODE AS SSCODE, ";
            sql += "            A.OPCODE AS OPCODE,NVL(SUM(A.OUTPUTCOUNT), 0) * NVL(POINTCOUNT, 0) AS ALLCOUNT,NVL(SPOTCOUNT, 0) AS SPOTCOUNT ";
            sql += "           FROM (SELECT TBLRPTOPQTY.SHIFTDAY, TBLRPTOPQTY.MOCODE, TBLRPTOPQTY.ITEMCODE,C.MMACHINETYPE, TBLMESENTITYLIST.SSCODE AS SSCODE, ";
            sql += "           TBLMESENTITYLIST.OPCODE AS OPCODE,SUM(TBLRPTOPQTY.OUTPUTTIMES) AS OUTPUTCOUNT,TBLMESENTITYLIST.ORGID,TBLMESENTITYLIST.MODELCODE, ";
            sql += "           NVL((CASE (SELECT PARAMALIAS FROM TBLSYSPARAM WHERE PARAMGROUPCODE = 'PCBA' AND TBLSYSPARAM.PARAMCODE = TBLMESENTITYLIST.OPCODE)  ";
            sql += "             WHEN 'POINTCOUNT' THEN POINTCOUNT  WHEN 'POINTCOUNT2' THEN POINTCOUNT2 END),0) AS POINTCOUNT  FROM TBLRPTOPQTY  ";
            sql += "              INNER JOIN TBLMESENTITYLIST ON TBLRPTOPQTY.TBLMESENTITYLIST_SERIAL =   TBLMESENTITYLIST.SERIAL    ";
            sql += "              INNER JOIN TBLMATERIAL C ON TBLRPTOPQTY.ITEMCODE = C.MCODE AND C.ORGID = TBLMESENTITYLIST.ORGID  ";
            sql += "              INNER JOIN TBLITEM D ON TBLRPTOPQTY.ITEMCODE = D.ITEMCODE  AND TBLMESENTITYLIST.ORGID = D.ORGID   ";
            sql += "               WHERE TBLMESENTITYLIST.OPCODE IN (" + opCodeShift + ")" + " AND TBLMESENTITYLIST.SSCODE IN (" + ssCodeShift + ") ";
            sql += "                GROUP BY TBLRPTOPQTY.SHIFTDAY,TBLRPTOPQTY.MOCODE, TBLRPTOPQTY.ITEMCODE, C.MMACHINETYPE,TBLMESENTITYLIST.SSCODE,TBLMESENTITYLIST.OPCODE,   ";
            sql += "                POINTCOUNT, POINTCOUNT2,TBLMESENTITYLIST.ORGID,TBLMESENTITYLIST.modelcode  ORDER BY SHIFTDAY) A  ";
            sql += "              LEFT OUTER JOIN (SELECT DISTINCT A.ITEMCODE,A.MOCODE,A.SHIFTDAY AS SHIFTDAY,F.FRMOPCODE,NVL(SUM(A.SPOTCOUNT), 0) AS SPOTCOUNT  ";
            sql += "                 FROM TBLTSERRORCODE A   ";
            sql += "                  INNER JOIN TBLMO B ON A.MOCODE = B.MOCODE   ";
            sql += "                  INNER JOIN TBLTS F ON A.TSID = F.TSID     ";
            sql += "               WHERE F.FRMOPCODE IN (" + opCodeShift + ")" + " AND F.FRMSSCODE IN (" + ssCodeShift + ") ";
            sql += "                GROUP BY A.ITEMCODE,A.MOCODE,A.SHIFTDAY,F.FRMOPCODE ";
            sql += "               ORDER BY A.SHIFTDAY) B ON A.SHIFTDAY = B.SHIFTDAY AND A.MOCODE = B.MOCODE AND A.ITEMCODE = B.ITEMCODE  AND A.OPCODE = B.FRMOPCODE ";
            sql += "              GROUP BY A.SHIFTDAY,A.ITEMCODE,A.MODELCODE,A.MMACHINETYPE,  ";
            sql += "                 A.MOCODE,A.ORGID,A.SSCODE,A.OPCODE,NVL(SPOTCOUNT, 0),NVL(POINTCOUNT, 0)   ";
            sql += "                 ORDER BY A.SHIFTDAY, A.SSCODE, A.OPCODE) CT     ";
            sql += "    LEFT OUTER JOIN TBLOP TBLOP ON TBLOP.OPCODE = CT.OPCODE    ";
            sql += "  WHERE 1 = 1 ";
            sql += "    AND CT.SSCODE IN (" + ssCodeShift + ") ";
            sql += "    AND CT.SHIFTDAY >= " + shiftDate;
            sql += "    AND CT.SHIFTDAY <= " + shiftDate;
            sql += "                  GROUP BY CT.OPCODE || ' - ' || TBLOP.OPDESC, CT.SHIFTDAY   ";
            sql += "   ORDER BY CT.OPCODE || ' - ' || TBLOP.OPDESC,CT.SHIFTDAY  ";

            object[] obj = this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
            if (obj != null)
            {
                return Math.Round(((NewReportDomainObject)obj[0]).DPPM, 2).ToString();
            }
            return "0";
        }

        //
        public object GetDBTimeDimension(int shifDay)
        {
            string sql = string.Format(@" SELECT  {0} FROM  Tbltimedimension  WHERE   DDATE={1} ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DBTimeDimension)), shifDay);

            object[] objs = this.DataProvider.CustomQuery(typeof(DBTimeDimension), new SQLCondition(sql));
            if (objs != null && objs.Length != 0)
            {
                return objs[0];
            }
            return null;
        }

        #region 投入产出柱状图  时间维度 天，周，月  
        //按照选择的时间维度为周和月的投入产出信息
        public object[] QueryInputOutputByShiftDay(string sscode, string workShopCode, int shiftDay, string timeDimension, DBTimeDimension timeParam)
        {
            //WatchPanelType.WorkShop  这里应该是周或者 月
            string sql = string.Empty;
            if (timeDimension.Equals(TimeDimension.Month))
            {
                sql = string.Format(@" SELECT Distinct segcode,  sscode,dweek AS tpcode,  NVL(output, 0) AS output,  NVL(input, 0) AS input
                                                      FROM (SELECT DISTINCT dweek, dmonth  FROM tbltimedimension WHERE dmonth = {0}  And Year={1}   ORDER BY dweek ASC) T1
                                                      LEFT JOIN ( ", timeParam.Month,timeParam.Year);
            }
            else
            {
                sql = string.Format(@" SELECT Distinct segcode,  sscode,ddate AS tpcode,  NVL(output, 0) AS output,  NVL(input, 0) AS input
                                                      FROM (SELECT DISTINCT dweek, ddate  FROM tbltimedimension WHERE dweek = {0} And Year={1} ORDER BY dweek ASC) T1
                                                      LEFT JOIN ( ", timeParam.Week,timeParam.Year);
            }


            sql += string.Format(@" SELECT tblmesentitylist.segcode,    tblmesentitylist.sscode ,");
            if (timeDimension.Equals(TimeDimension.Week))
            {
                sql += string.Format(@"  tbltimedimension.Ddate as tpcode,");
            }
            if (timeDimension.Equals(TimeDimension.Month))
            {
                sql += string.Format(@"   tbltimedimension.dweek as tpcode ,");
            }
            sql += string.Format(@"  nvl(SUM(ct.lineinputcount),0)  AS input,  nvl(SUM(ct.lineoutputcount),0) AS output");
            sql += string.Format(@" FROM (SELECT mocode,  shiftday,
                                                             itemcode,  tblmesentitylist_serial, moinputcount, mooutputcount,  molineoutputcount,
                                                             mowhitecardcount,  mooutputwhitecardcount,  lineinputcount,  lineoutputcount,
                                                            opcount,  opwhitecardcount, eattribute1  FROM tblrptsoqty) ct");
            sql += string.Format(@"    LEFT OUTER JOIN tbltimedimension tbltimedimension ON tbltimedimension.ddate = ct.shiftday
                                                         LEFT OUTER JOIN tblmesentitylist tblmesentitylist ON tblmesentitylist.serial = ct.tblmesentitylist_serial
                                                         WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(sscode))
            {
                sql += string.Format(@"    AND tblmesentitylist.sscode = '{0}'      ", sscode);
            }
            if (!string.IsNullOrEmpty(workShopCode))
            {
                sql += string.Format(@"  AND tblmesentitylist.segcode = '{0}'  ", workShopCode);
            }
            if (timeDimension.Equals(TimeDimension.Month))
            {
                sql += string.Format(@"AND    tbltimedimension.Dmonth={0}  and   tbltimedimension.year={1}
                                             GROUP BY tblmesentitylist.segcode,  tblmesentitylist.sscode , tbltimedimension.dweek,  tbltimedimension.Dmonth
                                             ORDER BY  tbltimedimension.dweek, tblmesentitylist.segcode,tblmesentitylist.sscode", timeParam.Month,timeParam.Year);
            }
            if (timeDimension.Equals(TimeDimension.Week))
            {
                sql += string.Format(@"  AND     tbltimedimension.dweek= {0}  and   tbltimedimension.year={1}", timeParam.Week,timeParam.Year);
                sql += string.Format(@"  GROUP BY tblmesentitylist.segcode,  tblmesentitylist.sscode ,   tbltimedimension.dweek,   tbltimedimension.Ddate
                                                           ORDER BY  tbltimedimension.Ddate,tblmesentitylist.segcode,tblmesentitylist.sscode,  tbltimedimension.dweek");
            }

            if (timeDimension.Equals(TimeDimension.Week))
            {
                sql += string.Format(@"  )T2 ON  t1.dweek={0}     AND  T2.tpcode=T1.Ddate  ORDER  BY  tpcode", timeParam.Week);
            }
            else
            {
                sql += string.Format(@" 	 )T2 ON  t1.Dmonth={0}  AND  T2.tpcode=T1.dWEEK  ORDER  BY  tpcode", timeParam.Month);
            }

            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        public int GetSEQForLineChar(string ssCode)
        {
            string sql = string.Format(@" SELECT seq  FROM (SELECT ROWNUM AS seq, a.*
                                                             FROM (SELECT   ss.sscode, tp.tpcode, tp.tpbtime, tp.tpetime,ss.Segcode,
                                                             tp.isoverdate    FROM tblss ss, tblshift shift, tbltp tp  where  1=1");
            if (!string.IsNullOrEmpty(ssCode) )
            {
                sql += string.Format(@"  and  ss.sscode = '{0}' ", ssCode);
            }
            sql += string.Format(@"  AND ss.shifttypecode = shift.shifttypecode
                    AND shift.shifttypecode = tp.shifttypecode
                    AND shift.shiftcode = tp.shiftcode
                    ORDER BY shift.shiftseq, tp.tpseq) a) tp
                    WHERE (   (    tp.tpbtime <= TO_CHAR (SYSDATE, 'hh24miss')
                    AND tp.tpetime >= TO_CHAR (SYSDATE, 'hh24miss')  )
                    OR (    tp.tpbtime > tp.tpetime  AND tp.tpbtime <= TO_CHAR (SYSDATE, 'hh24miss')
                AND tp.tpetime <= TO_CHAR (SYSDATE, 'hh24miss') AND tp.isoverdate = 1  ) )");

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryLineCharDataSource(string ssCode, int shiftDay, int seq)
        {
            string sql = string.Empty;
            sql += string.Format(@"   SELECT tp.seq as TPSeq, tp.PERIOD as TPCODE,NVL(Ct.INPUT, 0) AS input,NVL(ct.output, 0) AS output");
            sql += string.Format(@"    FROM (SELECT rownum AS seq,  a.* From( select  ss.sscode,  tp.TPCODE,");
            sql += string.Format(@"   TO_CHAR(TO_DATE(TRIM('{0}' || ' ' ||  LPAD(tp.TPBTIME, 6, '0')), 'yyyymmdd hh24miss'), 'hh24:mi') || '~' ||", shiftDay);
            sql += string.Format(@"   TO_CHAR(TO_DATE (TRIM ( '{0}' || ' '|| LPAD (tp.TPETIME, 6, '0')), 'yyyymmdd hh24miss'),'hh24:mi') as Period", shiftDay);
            sql += string.Format(@"  FROM tblss ss, tblshift shift, tbltp tp WHERE ss.sscode = '{0}'", ssCode);
            sql += string.Format(@"  AND ss.shifttypecode = shift.shifttypecode   AND shift.shifttypecode = tp.shifttypecode AND shift.shiftcode = tp.shiftcode");
            sql += string.Format(@"  ORDER BY shift.shiftseq, tp.tpseq) a) tp ");
            sql += string.Format(@"   LEFT OUTER JOIN (SELECT TPCODE, SUM(LINEINPUTCOUNT) AS INPUT,   SUM(LINEOUTPUTCOUNT) AS OUTPUT ");
            sql += string.Format(@"   FROM (SELECT TBLRPTSOQTY.LINEINPUTCOUNT,");
            sql += string.Format(@"  TBLRPTSOQTY.LINEOUTPUTCOUNT,    TBLMESENTITYLIST.TPCODE, TBLMESENTITYLIST.sscode");
            sql += string.Format(@"   FROM TBLRPTSOQTY   INNER JOIN TBLMESENTITYLIST");
            sql += string.Format(@"   ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = TBLMESENTITYLIST.SERIAL");
            sql += string.Format(@"   WHERE TBLMESENTITYLIST.SSCODE = '{0}' AND TBLRPTSOQTY.shiftday = {1})", ssCode, shiftDay);
            sql += string.Format(@"  GROUP BY TPCODE) CT ON CT.TPCODE = tp.tpcode");
            sql += string.Format(@"   WHERE tp.seq > {0} - 10  AND tp.seq <= {0}  ORDER BY tp.seq", seq);

            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        #endregion  

        #region 不良原因Top5 时间维度 天，周，月
        //不良原因分布(产线的ShiftDay)
        public object[] QueryErrorCasueTopFiveBySSCode(int shiftDate, string ssCode, string workShopCode)
        {
            string sql = "SELECT errorcode,ecdesc, allqty, qty,  nvl(ROUND(PERCENT,4 ),0)     AS   PERCENT ";
            sql += "   FROM (SELECT ecode errorcode, ";
            sql += "                SUM(qty) OVER() allqty, ";
            sql += "                qty, ";
            sql += "                ratio_to_report(qty) OVER() PERCENT ";
            sql += "           FROM (SELECT ecode, COUNT(DISTINCT tsid) AS qty ";
            sql += "                   FROM TBLTSERRORCODE ";
            sql += "                  WHERE (tsid, ecgcode, ecode) IN ";
            sql += "                        (SELECT ts.tsid, ec.ecgcode, ec.ecode ";
            sql += "                           FROM tblts ts, TBLTSERRORCODE ec, tblss ss ";
            sql += "                          WHERE ts.tsid = ec.tsid(+) ";
            sql += "                            AND ts.frmsscode = ss.sscode(+) ";
            if (!string.IsNullOrEmpty(ssCode))
            {
                sql += "                            AND ss.sscode IN ";
                sql += "                                ({0}) ";
            }
            if (!string.IsNullOrEmpty(workShopCode))
            {
                sql += "                            AND  ss.Segcode ='{2}' ";
            }
            sql += "                              AND ts.shiftday = {1} ";
            sql += "                 )GROUP BY ecode)) a, ";
            sql += "        tblec b ";
            sql += "  WHERE a.errorcode = b.ecode ";
            sql = string.Format(sql, ProcessQueryValues(ssCode), shiftDate, workShopCode);

            return this.DataProvider.CustomQuery(typeof(QDOTSInfo), new PagerCondition(sql, "qty DESC", 1, 5));
        }

        //当前时间维度为周时，取出该shiftDate所在周的所有不良代码
        public object[] QueryErrorCasueTopFiveByWeek(string ssCode, string workShopCode, DBTimeDimension timeParam)
        {
            string sql = string.Format(@" SELECT errorcode, ecdesc, allqty, qty, nvl(ROUND(PERCENT, 4), 0) AS PERCENT
                                                             FROM (SELECT ecode errorcode, SUM(qty) OVER() allqty, qty, ratio_to_report(qty) OVER() PERCENT
                                                             FROM (SELECT ecode, COUNT(DISTINCT tsid) AS qty
                                                             FROM TBLTSERRORCODE
                                                             WHERE (tsid, ecgcode, ecode) IN
                                                                             (SELECT ts.tsid, ec.ecgcode, ec.ecode
                                                                               FROM tblts ts, TBLTSERRORCODE ec, tblss ss
                                                                               WHERE ts.tsid = ec.tsid(+)
                                                                                AND ts.frmsscode = ss.sscode(+) ");
            if(!string.IsNullOrEmpty(ssCode))
            {
                sql+=string.Format(@"  AND ss.sscode IN ('{0}') " ,ssCode);
            }
            if(!string.IsNullOrEmpty(workShopCode))
            {
                sql+=string.Format(@"  AND ss.Segcode = '{0}' ",workShopCode);
            }
                sql+=string.Format(@"  AND ts.shiftday IN
                                                               (SELECT ddate   FROM Tbltimedimension
                                                                 WHERE dweek = {0} AND YEAR = {1}))
                                                              GROUP BY ecode)) a, tblec b
                                                        WHERE a.errorcode = b.ecode
                                                        GROUP BY Errorcode, ecdesc, allqty, qty, nvl(ROUND(PERCENT, 4), 0)
                                                         ORDER BY PERCENT DESC ",timeParam.Week,timeParam.Year);

                return this.DataProvider.CustomQuery(typeof(QDOTSInfo), new PagerCondition(sql, "qty DESC", 1, 5));
        }
        //当前时间维度为月时，取出该shiftDate所在月的所有不良代码
        public object[] QueryErrorCasueTopFiveByMonth(string ssCode, string workShopCode, DBTimeDimension timeParam)
        {
            string sql = string.Format(@" SELECT errorcode, ecdesc, allqty, qty, nvl(ROUND(PERCENT, 4), 0) AS PERCENT
                                                             FROM (SELECT ecode errorcode, SUM(qty) OVER() allqty, qty, ratio_to_report(qty) OVER() PERCENT
                                                             FROM (SELECT ecode, COUNT(DISTINCT tsid) AS qty
                                                             FROM TBLTSERRORCODE
                                                             WHERE (tsid, ecgcode, ecode) IN
                                                                             (SELECT ts.tsid, ec.ecgcode, ec.ecode
                                                                               FROM tblts ts, TBLTSERRORCODE ec, tblss ss
                                                                               WHERE ts.tsid = ec.tsid(+)
                                                                                AND ts.frmsscode = ss.sscode(+) ");
            if (!string.IsNullOrEmpty(ssCode))
            {
                sql += string.Format(@"  AND ss.sscode IN ('{0}') ", ssCode);
            }
            if (!string.IsNullOrEmpty(workShopCode))
            {
                sql += string.Format(@"  AND ss.Segcode = '{0}' ", workShopCode);
            }
            sql += string.Format(@"  AND ts.shiftday IN
                                                               (SELECT ddate   FROM Tbltimedimension
                                                                 WHERE dmonth = {0} AND YEAR = {1}))
                                                              GROUP BY ecode)) a, tblec b
                                                        WHERE a.errorcode = b.ecode
                                                        GROUP BY Errorcode, ecdesc, allqty, qty, nvl(ROUND(PERCENT, 4), 0)
                                                         ORDER BY PERCENT DESC ", timeParam.Month, timeParam.Year);

            return this.DataProvider.CustomQuery(typeof(QDOTSInfo), new PagerCondition(sql, "qty DESC", 1, 5));
        }
        #endregion

        #region 产线或者车间的总投入和产出   时间维度 天，周，月
        public object GetInputOutputQtyByShiftDay(int shiftDate, string sscode, string workShopCode, DBTimeDimension timeParam, string timeDimension)
        {
            string sqlStr = string.Format(@" SELECT nvl(SUM(lineinputcount), 0) AS INPUT,
                                                              nvl(SUM(lineoutputcount), 0) AS OUTPUT
                                                             FROM (SELECT shiftday,
                                                                             tblmesentitylist_serial, moinputcount,  mooutputcount,
                                                                             molineoutputcount,  mowhitecardcount, mooutputwhitecardcount,
                                                                             lineinputcount,   lineoutputcount,  opcount,  opwhitecardcount,    
						                                                     t2.Segcode,       t2.Sscode
                                                                             FROM tblrptsoqty T1
                                                                             LEFT OUTER JOIN tblmesentitylist T2
                                                                             ON T2.serial = T1.tblmesentitylist_serial
                                                                             WHERE 1 = 1     ");
            if (shiftDate != -1)
            {
                sqlStr += string.Format(@"  AND T1.Shiftday = {0} ", shiftDate);
            }
            else if (timeDimension.Equals(TimeDimension.Month))
            {
                sqlStr += string.Format(@"  AND  T1.Shiftday  IN  (SELECT ddate   FROM Tbltimedimension  
                                                                  WHERE dmonth = {0} AND YEAR = {1})",timeParam.Month,timeParam.Year);
            }
            else if(timeDimension.Equals(TimeDimension.Week))
            {
                sqlStr += string.Format(@"  AND  T1.Shiftday  IN  (SELECT ddate   FROM Tbltimedimension  
                                                                    WHERE dweek = {0} AND YEAR = {1})",timeParam.Week,timeParam.Year);
            }
            if (!string.IsNullOrEmpty(workShopCode))
            {
                sqlStr += string.Format(@"  AND t2.Segcode = '{0}' ", workShopCode);
            }
            if (!string.IsNullOrEmpty(sscode))
            {
                sqlStr += string.Format(@"   AND t2.Sscode = '{0}' ", sscode);
            }
            sqlStr += string.Format(@" )");

            object[] objs = this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sqlStr));
            if (objs != null)
            {
                return objs[0];
            }
            return null;
        }
        #endregion  

        #region 直通率 维度天，周，月
        //产线直通率 按照Period显示
        public object[] QueryFPYBySSCodeForPeriod(string sscode, int shiftday, int seq)
        {
            string sql = string.Format(@" SELECT  tp.seq AS  TPSeq,  tp.PERIOD AS  TPCODE,
                                                                                NVL(SUM(ct.LineOutputCount),0) AS output,
                                                                                NVL(SUM(ct.LineWhiteCardCount),0)  AS PassRcardQty,
                                                                                ROUND(  NVL(DECODE(SUM(ct.LineOutputCount), 0, 0,  SUM(ct.LineWhiteCardCount) / SUM(ct.LineOutputCount)),0),4) AS PassRcardRate
                                                             FROM (  SELECT rownum AS seq, a.*
                                                                             FROM (SELECT ss.sscode,   tp.TPCODE,
                                                                                    TO_CHAR(TO_DATE(TRIM('{0}' || ' ' || LPAD(tp.TPBTIME, 6, '0')),   'yyyymmdd hh24miss'), 'hh24:mi') || '~' ||
                                                                                    TO_CHAR(TO_DATE(TRIM('{0}' || ' ' || LPAD(tp.TPETIME, 6, '0')),   'yyyymmdd hh24miss'), 'hh24:mi') AS Period
                                                                                              FROM tblss ss, tblshift shift, tbltp tp
                                                                                              WHERE ss.sscode = '{1}'
                                                                                                             AND ss.shifttypecode = shift.shifttypecode
                                                                                                             AND shift.shifttypecode = tp.shifttypecode
                                                                                                              AND shift.shiftcode = tp.shiftcode
                                                                                               ORDER BY shift.shiftseq, tp.tpseq) a) tp
                                                                                LEFT OUTER JOIN (SELECT TPCODE,
                                                                                                                     NVL( SUM(LINEOUTPUTCOUNT),0) AS  LINEOUTPUTCOUNT ,
                                                                                                                      NVL( SUM(Linewhitecardcount),0) AS   Linewhitecardcount
                                                                                                                         FROM (SELECT TBLRPTSOQTY.LINEINPUTCOUNT,
                                                                                                                                       TBLRPTSOQTY.LINEOUTPUTCOUNT,
                                                                                                                                       TBLMESENTITYLIST.TPCODE,
                                                                                                                                       TBLRPTLINEQTY.Linewhitecardcount
                                                                                                                                        FROM TBLRPTSOQTY
                                                                                INNER JOIN TBLMESENTITYLIST
                                                                                               ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL =  TBLMESENTITYLIST.SERIAL  
													                            INNER  JOIN  TBLRPTLINEQTY                                                              
													                                           ON tblrptsoqty.mocode = tblrptlineqty.mocode    AND tblrptsoqty.shiftday = tblrptlineqty.shiftday
                                                                                               AND tblrptsoqty.itemcode = tblrptlineqty.itemcode
                                                                                               AND tblrptsoqty.tblmesentitylist_serial = tblrptlineqty.tblmesentitylist_serial
															                                    AND  TBLRPTLINEQTY.Shiftday=  TBLRPTSOQTY.Shiftday
                                                                                                WHERE TBLMESENTITYLIST.SSCODE = '{1}'
                                                                                                 AND TBLRPTSOQTY.shiftday = {0}	)
                                                                                                 GROUP BY TPCODE ) ct
                                                                                 ON CT.TPCODE = tp.tpcode
                                                        WHERE tp.seq >{2}- 10   AND tp.seq <={2}
                                                                       GROUP  BY   tp.PERIOD , ct.LineOutputCount,ct.LineWhiteCardCount,tp.seq  
                                                                                               ORDER BY tp.seq    ", shiftday, sscode, seq);

            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        //产线直通率时间维度为月
        public object[] QueryFPYBySSCodeForMonth(string sscode, int shiftDay, string workShopCode, DBTimeDimension timeParam)
        {
            string sql = string.Format(@" SELECT  Distinct segcode, sscode, dweek AS   tpcode,NVL(output,0) AS   output,NVL(passrcardqty,0)AS passrcardqty ,
ROUND( NVL(passrcardrate,0),4) AS  passrcardrate FROM  
(SELECT  DISTINCT  dweek,dmonth FROM  tbltimedimension   WHERE   dmonth={0}  AND  YEAR={1} ORDER  BY  dweek ASC )    T1
 LEFT  JOIN   ( ", timeParam.Month,timeParam.Year);
            sql += string.Format(@"  SELECT ct.segcode AS segcode,  ct.sscode  AS sscode, 
                                                                                Tbltimedimension.Dweek as TPCODE, 
                                                                               SUM(ct.LineOutputCount) AS output,  SUM(ct.LineWhiteCardCount) AS PassRcardQty,
                                                                               DECODE(SUM(LineOutputCount), 0, 0, SUM(LineWhiteCardCount) / SUM(LineOutputCount)) AS PassRcardRate
                                                               FROM (SELECT lineoutputcount,  linewhitecardcount, tblrptsoqty.mocode, 
                                                                              tblrptsoqty.shiftday, tblrptsoqty.itemcode,tblmesentitylist.shiftcode,   tblmesentitylist.sscode,  
                                                                              tblmesentitylist.faccode,  tblmesentitylist.modelcode,
                                                                              tblmesentitylist.segcode, tblmesentitylist.opcode,   
                                                                              tblmesentitylist.shifttypecode,  tblmesentitylist.bigsscode,
                                                                              tblmesentitylist.tpcode, tblmesentitylist.serial,   tblmesentitylist.rescode,  tblmesentitylist.orgid
                                                                              FROM tblrptsoqty
                                                                              INNER JOIN tblrptlineqty
                                                                                            ON tblrptsoqty.mocode = tblrptlineqty.mocode
                                                                                            AND tblrptsoqty.shiftday = tblrptlineqty.shiftday
                                                                                            AND tblrptsoqty.itemcode = tblrptlineqty.itemcode
                                                                                            AND tblrptsoqty.tblmesentitylist_serial = tblrptlineqty.tblmesentitylist_serial
                                                                              LEFT OUTER JOIN tblmesentitylist
                                                                                          ON tblrptsoqty.tblmesentitylist_serial = tblmesentitylist.serial) ct
                                                                              LEFT OUTER JOIN tbltimedimension tbltimedimension
                                                                                          ON tbltimedimension.ddate = ct.shiftday
                                                            WHERE 1 = 1              
                                                                                 AND   tbltimedimension.Year={0}
                                                                                 AND  Tbltimedimension.Dmonth= {1} ", timeParam.Year, timeParam.Month);
            if (!string.IsNullOrEmpty(sscode))
            {
                sql += string.Format(@"  AND   ct.sscode='{0}' ", sscode);
            }
            if (!string.IsNullOrEmpty(workShopCode))
            {
                sql += string.Format(@"  AND  ct.Segcode='{0}' ", workShopCode);
            }
            sql += string.Format(@"  GROUP BY ct.segcode ,    ct.sscode ,    Tbltimedimension.dweek,  tbltimedimension.year,Tbltimedimension.Dmonth
                                                       ORDER BY  tbltimedimension.dweek  DESC ,segcode,sscode ");
            sql += string.Format(@"  )  T2 ON  t1.dmonth={0}  AND  T2.tpcode=T1.dWEEK  ORDER  BY  tpcode ", timeParam.Month);
            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        //产线直通率 时间维度为周
        public object[] QueryFPYBySSCodeForWeek(string sscode, int shiftDay, string workShopCode, DBTimeDimension timeParam)
        {
            string sql = string.Format(@" SELECT   Distinct segcode, sscode, ddate AS   tpcode,NVL(output,0) AS   output,NVL(passrcardqty,0)AS passrcardqty ,
ROUND( NVL(passrcardrate,0),4) AS  passrcardrate FROM  
(SELECT  DISTINCT  ddate,dweek FROM  tbltimedimension   WHERE   dweek={0}  AND  YEAR={1}   ORDER  BY  dweek ASC )    T1
 LEFT  JOIN   ( ", timeParam.Week,timeParam.Year);
            sql+= string.Format(@"  SELECT ct.segcode AS segcode,  ct.sscode  AS sscode,  
                                                                               Tbltimedimension.Ddate as  tpcode , 
                                                                               SUM(ct.LineOutputCount) AS output,  SUM(ct.LineWhiteCardCount) AS PassRcardQty,
                                                                               DECODE(SUM(LineOutputCount), 0, 0, SUM(LineWhiteCardCount) / SUM(LineOutputCount)) AS PassRcardRate
                                                               FROM (SELECT lineoutputcount,  linewhitecardcount, tblrptsoqty.mocode, 
                                                                              tblrptsoqty.shiftday, tblrptsoqty.itemcode,tblmesentitylist.shiftcode,   tblmesentitylist.sscode,  
                                                                              tblmesentitylist.faccode,  tblmesentitylist.modelcode,
                                                                              tblmesentitylist.segcode, tblmesentitylist.opcode,   
                                                                              tblmesentitylist.shifttypecode,  tblmesentitylist.bigsscode,
                                                                              tblmesentitylist.tpcode, tblmesentitylist.serial,   tblmesentitylist.rescode,  tblmesentitylist.orgid
                                                                              FROM tblrptsoqty
                                                                              INNER JOIN tblrptlineqty
                                                                                            ON tblrptsoqty.mocode = tblrptlineqty.mocode
                                                                                            AND tblrptsoqty.shiftday = tblrptlineqty.shiftday
                                                                                            AND tblrptsoqty.itemcode = tblrptlineqty.itemcode
                                                                                            AND tblrptsoqty.tblmesentitylist_serial = tblrptlineqty.tblmesentitylist_serial
                                                                              LEFT OUTER JOIN tblmesentitylist
                                                                                          ON tblrptsoqty.tblmesentitylist_serial = tblmesentitylist.serial) ct
                                                                              LEFT OUTER JOIN tbltimedimension tbltimedimension
                                                                                          ON tbltimedimension.ddate = ct.shiftday
                                                            WHERE 1 = 1              
                                                                               AND   tbltimedimension.Year={0}
                                                                               AND  tbltimedimension.Dweek= {1} ", timeParam.Year, timeParam.Week);
            if (!string.IsNullOrEmpty(sscode))
            {
                sql += string.Format(@"   AND   ct.sscode='{0}' ", sscode);
            }
            if (!string.IsNullOrEmpty(workShopCode))
            {
                sql += string.Format(@"  AND  ct.Segcode='{0}' ", workShopCode);
            }
            sql += string.Format(@"   GROUP BY ct.segcode ,ct.sscode , tbltimedimension.year,Tbltimedimension.Ddate,Tbltimedimension.Dweek
                                                        ORDER BY  tbltimedimension.Ddate DESC ,segcode,sscode      ");
            sql += string.Format(@"  )  T2 ON  t1.Dweek={0}  AND  T2.tpcode=T1.ddate   ORDER  BY  tpcode", timeParam.Week);
            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        #endregion 

        #region 看板明细数据，维度天，周，月
        public object[] QueryPanelDetailsData(string sscode, int shiftday, string workShopCode, DBTimeDimension timeParam, string timeDimension)
        {
            string sql = string.Format(@" SELECT   to_Char(to_date(Tbltimedimension.Ddate,'yyyyMMss'),'yyyy-MM-ss')  AS   shiftDay,
                                                                ct.Sscode,
                                                               CT.MOCODE AS MOCODE, CT.ITEMCODE AS ITEMCODE,
                                                               TBLITEM.ITEMDESC AS ITEMNAME,   Tblmo.Orderno AS orderNO,
                                                               TBLMO.MOPLANQTY AS MOPLAYQTY,
                                                               TBLMO.MOINPUTQTY AS MOINPUTQTY,
                                                               TBLMO.MOACTQTY AS MOOUTQTY,
                                                               TRUNC(TBLMO.MOACTQTY / TBLMO.MOPLANQTY, 4) AS ACHIEVEMENTRATE,
                                                                DECODE(NVL(SUM(CT.linewhitecardcount), 0), 0, 0, TRUNC((SUM(CT.linewhitecardcount) / SUM(CT.lineoutputcount)), 4)) AS PASSYIELD
                                                   FROM (SELECT  TBLRPTSOQTY.MOCODE, TBLRPTSOQTY.ITEMCODE,TBLMESENTITYLIST.SSCODE,
                                                                                  TBLRPTSOQTY.SHIFTDAY,   TBLRPTSOQTY.MOLINEOUTPUTCOUNT,
                                                                                  TBLRPTSOQTY.MOOUTPUTWHITECARDCOUNT,
                                                                                  TBLRPTSOQTY.OPWHITECARDCOUNT,
                                                                               	 Tblmesentitylist.Segcode,
                                                                                  TBLRPTSOQTY.lineinputcount, TBLRPTSOQTY.lineoutputcount, tblrptlineqty.linewhitecardcount
                                                                FROM TBLRPTSOQTY
                                                                INNER JOIN tblrptlineqty
                                                                                      ON tblrptsoqty.mocode = tblrptlineqty.mocode
                                                                                      AND tblrptsoqty.shiftday = tblrptlineqty.shiftday
                                                                                      AND tblrptsoqty.itemcode = tblrptlineqty.itemcode
                                                                                      AND tblrptsoqty.tblmesentitylist_serial = tblrptlineqty.tblmesentitylist_serial
                                                                INNER JOIN TBLMESENTITYLIST
                                                                                      ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL = TBLMESENTITYLIST.SERIAL) CT
                                                   LEFT OUTER JOIN TBLMO  ON TBLMO.MOCODE = CT.MOCODE
                                                   LEFT OUTER JOIN TBLITEM  ON TBLITEM.ITEMCODE = CT.ITEMCODE
	                                               LEFT  JOIN  Tbltimedimension  ON   ct.Shiftday=Tbltimedimension.Ddate
                                                   WHERE 1 = 1    ");
           
            if (timeDimension.Equals(TimeDimension.Day))
            {
                sql += string.Format(@"        AND CT.SHIFTDAY = {0}  ", shiftday);
            }
            if (timeDimension.Equals(TimeDimension.Month))
            {
                sql += string.Format(@"  AND CT.SHIFTDAY   IN  (SELECT ddate   FROM Tbltimedimension  
                                                                    WHERE DMONTH = {0} AND YEAR = {1}) ", timeParam.Month, timeParam.Year);
            }
            if (timeDimension.Equals(TimeDimension.Week))
            {
                sql += string.Format(@"  AND CT.SHIFTDAY   IN  (SELECT ddate   FROM Tbltimedimension  
                                                                    WHERE dweek = {0} AND YEAR = {1}) ", timeParam.Week, timeParam.Year);
            }
             
            if (!string.IsNullOrEmpty(sscode))
            {
                sql += string.Format(@" AND CT.SSCODE = '{0}'  ", sscode);
            }
            if (!string.IsNullOrEmpty(workShopCode))
            {
                sql += string.Format(@"   AND  CT.Segcode='{0}' ", workShopCode);
            }
            sql += string.Format(@"   GROUP BY CT.MOCODE, CT.ITEMCODE,  TBLITEM.ITEMDESC, Tblmo.Orderno,  ct.Sscode,Tbltimedimension.Ddate,
                                                                         TBLMO.MOPLANQTY,TBLMO.MOINPUTQTY,TBLMO.MOACTQTY
                                                         ORDER BY   Tbltimedimension.Ddate DESC,    CT.Sscode desc, mocode DESC ");
            return this.DataProvider.CustomQuery(typeof(PanelDetailsData), new SQLCondition(sql));
        }
        #endregion 

        #region  实际上岗人数 
        //实际在岗人数
        public int GetOnPostManCount(int shiftDate, string ssCode, string workShopCode, DBTimeDimension timeParam, string timeDimension)
        {
            string sql = "SELECT COUNT(1) AS MANCOUNT  FROM TBLLINE2MANDETAIL E, TBLSS S WHERE E.SSCODE = S.SSCODE";

            if (!string.IsNullOrEmpty(ssCode))
            {
                sql += string.Format(@"        AND S.SSCODE IN ({0}) ",ProcessQueryValues(ssCode));
            }
            if (!string.IsNullOrEmpty(workShopCode))
            {
                sql += string.Format(" AND   s.Segcode='{0}' ", workShopCode);
            }
            if (timeDimension.Equals(TimeDimension.Day))
            {
                sql += string.Format(@"        AND E.SHIFTDATE = {0} ",shiftDate);
            }
            if(timeDimension.Equals(TimeDimension.Month))
            {
                sql += string.Format(@"  and  E.SHIFTDATE  IN  (SELECT ddate   FROM Tbltimedimension  
                                                                    WHERE DMONTH = {0} AND YEAR = {1}) ",timeParam.Month,timeParam.Year);
            }
            if (timeDimension.Equals(TimeDimension.Week))
            {
                sql += string.Format(@"  and  E.SHIFTDATE  IN  (SELECT ddate   FROM Tbltimedimension  
                                                                    WHERE dweek = {0} AND YEAR = {1}) ", timeParam.Week, timeParam.Year);
            }
            sql += "        AND E.STATUS IN ('on', 'autoon')";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

    }

    #region DBDateTime
    /// <summary>
    /// DBDateTime 的摘要说明。
    /// 文件名:		
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
    /// 创建日期:	20050819 14:21:26
    /// 修改人:Mark Lee
    /// 修改日期:20050819
    /// 描 述:	定义数据库时间格式
    /// 版 本:	
    /// </summary>
    [Serializable, TableMap("dual", "systimestamp")]
    public class DBDateTime : DomainObject
    {
        public DBDateTime()
        {
        }

        public DBDateTime(DateTime dateTime)
        {
            this.DBDate = int.Parse(dateTime.ToString("yyyyMMdd"));
            this.DBTime = int.Parse(dateTime.ToString("HHmmss"));
        }

        [FieldMapAttribute("DBDate", typeof(int), 100, false)]
        public int DBDate;
        [FieldMapAttribute("DBTime", typeof(int), 100, false)]
        public int DBTime;

        public DateTime DateTime
        {
            get { return WatchPanelFacade.ToDateTime(DBDate, DBTime); }
        }

    }
    #endregion
}
