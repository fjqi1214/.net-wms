using System;
using System.Data;
using System.Collections;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WatchPanel
{
    /// <summary>
    /// WatchPanelFacade 的摘要说明。
    /// </summary>
    public class WatchPanelFacade
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public WatchPanelFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
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

        #region Old

        public object GetShift(string ShiftTypeCode)
        {
            BaseSetting.ShiftModelFacade shiftModel = new BaseSetting.ShiftModelFacade(this.DataProvider);

            Domain.BaseSetting.TimePeriod period = (Domain.BaseSetting.TimePeriod)shiftModel.GetTimePeriod(ShiftTypeCode,
                Web.Helper.FormatHelper.TOTimeInt(DateTime.Now));

            if (period == null)
                return null;

            return shiftModel.GetShift(period.ShiftCode);
        }

        public object[] GetTimePeriods(string ShiftCode)
        {
            BaseSetting.ShiftModelFacade shiftModel = new BaseSetting.ShiftModelFacade(this.DataProvider);

            object[] objs = shiftModel.GetTimePeriodByShiftCode(ShiftCode);

            if (objs == null)
                objs = new object[] { };

            return objs;
        }

        public object[] GetResources(string SSCode)
        {
            BaseSetting.BaseModelFacade baseModeFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);

            object[] objs = baseModeFacade.GetResourceByStepSequenceCode(SSCode);

            if (objs == null)
                objs = new object[] { };

            return objs;
        }

        private int GetShiftDay(string ShiftCode)
        {
            BaseSetting.ShiftModelFacade shiftModel = new BaseSetting.ShiftModelFacade(this.DataProvider);
            Shift shift = (Shift)shiftModel.GetShift(ShiftCode);

            TimePeriod period = (TimePeriod)shiftModel.GetTimePeriod(shift.ShiftTypeCode,
                Web.Helper.FormatHelper.TOTimeInt(DateTime.Now));

            if (period.IsOverDate == Web.Helper.FormatHelper.TRUE_STRING)
            {
                if (period.TimePeriodBeginTime < period.TimePeriodEndTime)
                {
                    return FormatHelper.TODateInt(DateTime.Now.AddDays(-1));
                }
                else if (Web.Helper.FormatHelper.TOTimeInt(DateTime.Now) < period.TimePeriodBeginTime)
                {
                    return FormatHelper.TODateInt(DateTime.Now.AddDays(-1));
                }
                else
                {
                    return FormatHelper.TODateInt(DateTime.Now);
                }
            }

            else
            {
                return FormatHelper.TODateInt(DateTime.Now);
            }
        }

        public object[] GetItemCodes(string SSCode, string ShiftCode)
        {
            int ShiftDay = GetShiftDay(ShiftCode);

            string sql = string.Format("select distinct itemcode from tblsimulationreport where sscode ='{0}' and iscom=0 and shiftday={1}",
                SSCode, ShiftDay);

            return this.DataProvider.CustomQuery(typeof(Item), new SQLCondition(sql));
        }

        public DataSet GetWatchingData(string SSCode, string ItemCode, string ShiftCode, string ResCode)
        {
            //string ResCodes = "'"+string.Join("','",resCodes) + "'";

            //EAttribute2   通过数  NGTimes  不良次数  OuputQty 产出

            //int ShiftDay = Web.Helper.FormatHelper.TODateInt(DateTime.Today);


            int ShiftDay = GetShiftDay(ShiftCode);

            ItemCode = FormatHelper.ProcessQueryValues(ItemCode, true);
            ResCode = FormatHelper.ProcessQueryValues(ResCode, true);

            //tp.
#if DEBUG
            //ShiftDay = 20060713;
#endif

            string sql = string.Format("SELECT SUM(a.EAttribute2) input ,SUM(a.OUTPUTQTY) OUTPUTQTY,SUM(a.NGTimes) defects, a.RESCODE,a.TPCODE /*,a.ItemCode*/ FROM " +
                        " TBLRPTHISOPQTY a inner join tbltp b on a.TPCODE = b.TPCODE WHERE a.SSCODE='{0}' AND a.ITEMCODE IN ({1}) " +
                        " and b.ShiftCode = '{2}' AND ShiftDay={3} AND a.RESCODE IN({4}) GROUP BY a.RESCODE /*,a.ITEMCODE*/,a.TPCODE ",
                SSCode, ItemCode, ShiftCode, ShiftDay, ResCode);

            return ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.Query(sql);

            //return this.DataProvider.CustomQuery(typeof(ReportHistoryOPQty),new SQLCondition(sql));
        }

        #endregion

        #region 新看板使用

        public object[] QueryExceptionList(string bigSSCode, int shiftDate)
        {
            string sql = " SELECT E.*, C.EXCEPTIONDESC,S.SSDESC FROM TBLEXCEPTION E, TBLEXCEPTIONCODE C, TBLSS S ";
            sql += " WHERE E.SSCODE = S.SSCODE AND C.EXCEPTIONCODE = E.EXCEPTIONCODE ";
            if (bigSSCode.Trim().Length > 0)
            {
                sql += " AND S.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "' ";
            }
            if (shiftDate > 0)
            {
                sql += " AND E.SHIFTDATE = " + shiftDate + " ";
            }
            sql += " ORDER BY E.BEGINTIME DESC ";

            return this.DataProvider.CustomQuery(typeof(ExceptionEventWithDescription), new SQLCondition(sql));
        }

        public object[] QueryAlertNoticeList(int shiftDate)
        {
            string sql = "SELECT {0} FROM tblalertnotice WHERE noticedate = {1} ORDER BY noticetime DESC ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertNotice)), shiftDate.ToString());

            return this.DataProvider.CustomQuery(typeof(AlertNotice), new SQLCondition(sql));
        }

        public int GetWorkPlanQty(string bigSSCode, int planDate)
        {
            string sql = "SELECT SUM(PLANQTY) AS PLANQTY,bigsscode  FROM TBLWORKPLAN";
            sql += "  WHERE BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'   AND PLANDATE = " + planDate + "  GROUP BY BIGSSCODE";

            object[] WorkPlanList = this.DataProvider.CustomQuery(typeof(WorkPlan), new SQLCondition(sql));

            if (WorkPlanList != null)
            {
                return Convert.ToInt32(((WorkPlan)WorkPlanList[0]).PlanQty);
            }

            return 0;
        }

        public int GetBigSSCodeOutPutQty(string bigSSCode, int shiftDate)
        {
            string sql = " SELECT TBLRPTSOQTY.SHIFTDAY AS SHIFTDAY,SUM(TBLRPTSOQTY.MOINPUTCOUNT) AS INPUT,SUM(TBLRPTSOQTY.MOLINEOUTPUTCOUNT) AS OUTPUT";
            sql += "                FROM TBLRPTSOQTY ";
            sql += "                LEFT OUTER JOIN TBLMESENTITYLIST TBLMESENTITYLIST ON TBLMESENTITYLIST.SERIAL =TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL";
            sql += "            WHERE 1 = 1 ";
            sql += "                 AND TBLMESENTITYLIST.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'";
            sql += "                 AND TBLRPTSOQTY.SHIFTDAY = " + shiftDate + " ";
            sql += "  GROUP BY TBLRPTSOQTY.SHIFTDAY ORDER BY TBLRPTSOQTY.SHIFTDAY";

            object[] reportList = this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));

            if (reportList != null)
            {
                return Convert.ToInt32(((NewReportDomainObject)reportList[0]).Output);
            }

            return 0;
        }

        public object[] QueryCrewList(string bigSSCode, int shiftDate)
        {
            string sql = "SELECT DISTINCT CREWCODE  FROM TBLLINE2CREW E, TBLSS S WHERE E.SSCODE = S.SSCODE ";
            sql += " AND S.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'   AND E.SHIFTDATE = " + shiftDate + "";

            return this.DataProvider.CustomQuery(typeof(Line2Crew), new SQLCondition(sql));
        }

        public int GetOnPostManCount(string bigSSCode, int shiftDate)
        {
            string sql = "SELECT COUNT(1) AS MANCOUNT  FROM TBLLINE2MANDETAIL E, TBLSS S WHERE E.SSCODE = S.SSCODE";
            sql += "        AND S.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'";
            sql += "        AND E.Shiftdate =" + shiftDate + "";
            sql += "        AND E.STATUS IN ('on', 'autoon')";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryShiftCode(string bigSSCode, string ssCode)
        {
            string sql = " SELECT DISTINCT TBLSHIFT.SHIFTCODE  FROM TBLSS, TBLSHIFT WHERE TBLSS.SHIFTTYPECODE = TBLSHIFT.SHIFTTYPECODE";

            if (bigSSCode.Trim() != string.Empty)
            {
                sql += "  AND TBLSS.Bigsscode='" + bigSSCode.Trim().ToUpper() + "'";
            }

            if (ssCode.Trim() != string.Empty)
            {
                sql += "  AND TBLSS.sscode='" + ssCode.Trim().ToUpper() + "'";
            }

            sql += "   ORDER BY TBLSHIFT.SHIFTCODE ASC";

            return this.DataProvider.CustomQuery(typeof(Shift), new SQLCondition(sql));
        }

        public object[] QueryProductData(string bigSSCode, int shiftDate, ArrayList shiftCodeList, string timePeriodCode)
        {
            string beginCondition = string.Empty;
            string mainCondition = string.Empty;
            string leftJoinCondition = string.Empty;

            string bodyBSsql = string.Empty;
            string shiftLineOutColunmsSql = string.Empty;
            string shiftLineOutMainSql = string.Empty;

            //Modified By Nettie Chen 2009/09/23 ADD MM.MNAME
            //beginCondition = "SELECT tt.ITEMCODE AS ITEMCODE,MM.Mmodelcode AS MATERIALMODELCODE,";
            beginCondition = "SELECT MM.MNAME AS ItemName,tt.ITEMCODE AS ITEMCODE,MM.Mmodelcode AS MATERIALMODELCODE,";
            //End Modified
            beginCondition += " TT.PLANQTY AS DAYPLANQTY,NVL(SS.LOUT, 0) AS PERTIMEOUTPUTQTY,AA.PASSRCARDRATE AS PASSRATE,";
            beginCondition += " CY.MANHOURPERPRODUCT AS ONENEEDTIME,UP.UPPH AS UPPH";
            if (shiftCodeList.Count > 0)
            {
                for (int i = 1; i <= shiftCodeList.Count; i++)
                {
                    beginCondition += ",tt.SHIFTLOUT" + i.ToString();
                }
            }
            beginCondition += " FROM (SELECT NVL(A.PLANDATE, B.BSHIFTDAY) SHIFTDAY,NVL(A.ITEMCODE, B.ITEMCODE) ITEMCODE,NVL(A.PLANQTY, 0) PLANQTY";

            if (shiftCodeList.Count > 0)
            {
                for (int i = 1; i <= shiftCodeList.Count; i++)
                {
                    beginCondition += ",NVL(B.LOUT" + i.ToString() + ", 0) SHIFTLOUT" + i.ToString();
                }
            }

            beginCondition += "  FROM (SELECT SUM(PLANQTY) AS PLANQTY, BIGSSCODE, ITEMCODE, PLANDATE  FROM TBLWORKPLAN";
            beginCondition += "    WHERE BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'   AND PLANDATE = " + shiftDate + "";
            beginCondition += " GROUP BY BIGSSCODE, ITEMCODE, PLANDATE) A   FULL JOIN (";

            shiftLineOutColunmsSql = "SELECT BSHIFTDAY,ITEMCODE ";
            if (shiftCodeList.Count > 0)
            {
                for (int i = 1; i <= shiftCodeList.Count; i++)
                {
                    shiftLineOutColunmsSql += ",SUM(LOUT" + i.ToString() + ") AS LOUT" + i.ToString();
                }
                shiftLineOutColunmsSql += " FROM (";
            }

            shiftLineOutColunmsSql += " SELECT ";

            if (shiftCodeList.Count > 0)
            {
                string shiftDayString = "SHIFT1.SHIFTDAY";
                string itemCodeString = "SHIFT1.ITEMCODE";
                string shiftCodeString = "SHIFT1.SHIFTCODE";

                for (int i = 1; i <= shiftCodeList.Count; i++)
                {
                    shiftDayString = " NVL(" + shiftDayString + ",SHIFT" + i.ToString() + ".SHIFTDAY)";
                    itemCodeString = " NVL(" + itemCodeString + ",SHIFT" + i.ToString() + ".ITEMCODE) ";
                    shiftCodeString = " NVL(" + shiftCodeString + ",SHIFT" + i.ToString() + ".SHIFTCODE)";
                }

                shiftLineOutColunmsSql += shiftDayString + " AS BSHIFTDAY," + itemCodeString + "AS ITEMCODE," + shiftCodeString + "AS BSHIFTCODE";

                for (int i = 0; i < shiftCodeList.Count; i++)
                {
                    shiftLineOutColunmsSql += ",NVL(SHIFT" + Convert.ToString(i + 1) + ".LOUT, 0) LOUT" + Convert.ToString(i + 1) + "";

                    if (i > 0)
                    {
                        shiftLineOutMainSql += "  FULL JOIN (";
                    }

                    shiftLineOutMainSql += "SELECT Y.SHIFTDAY,Y.ITEMCODE,E.SHIFTCODE,SUM(MOLineOutputCount) LOUT";
                    shiftLineOutMainSql += "    FROM TBLRPTSOQTY Y, TBLMESENTITYLIST E, TBLSS S";
                    shiftLineOutMainSql += "   WHERE Y.TBLMESENTITYLIST_SERIAL = E.SERIAL";
                    shiftLineOutMainSql += "    AND E.SSCODE = S.SSCODE     AND Y.SHIFTDAY = " + shiftDate + "";
                    shiftLineOutMainSql += "    AND S.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'  AND E.SHIFTCODE = '" + ((Shift)shiftCodeList[i]).ShiftCode + "'";
                    shiftLineOutMainSql += "   GROUP BY Y.ITEMCODE, Y.SHIFTDAY, E.SHIFTCODE) SHIFT" + Convert.ToString(i + 1);

                    if (i > 0)
                    {
                        shiftLineOutMainSql += "  ON SHIFT1.SHIFTDAY =SHIFT" + Convert.ToString(i + 1) + ".SHIFTDAY ";
                        shiftLineOutMainSql += "  AND SHIFT1.ITEMCODE =SHIFT" + Convert.ToString(i + 1) + ".ITEMCODE";
                        shiftLineOutMainSql += "  AND SHIFT1.SHIFTCODE =SHIFT" + Convert.ToString(i + 1) + ".SHIFTCODE";
                    }
                }

                shiftLineOutColunmsSql += " FROM (";
                shiftLineOutMainSql += ") GROUP BY BSHIFTDAY, ITEMCODE) B ON A.ITEMCODE = B.ITEMCODE   AND A.PLANDATE = B.BSHIFTDAY) TT";
            }

            bodyBSsql = shiftLineOutColunmsSql + shiftLineOutMainSql;
            mainCondition = beginCondition + bodyBSsql;

            leftJoinCondition = "  LEFT JOIN (SELECT Y.SHIFTDAY,Y.ITEMCODE,E.SHIFTCODE,E.TPCODE,SUM(MOLineOutputCount) LOUT";
            leftJoinCondition += "  FROM TBLRPTSOQTY Y, TBLMESENTITYLIST E, TBLSS S WHERE Y.TBLMESENTITYLIST_SERIAL = E.SERIAL";
            leftJoinCondition += "    AND E.SSCODE = S.SSCODE   AND Y.SHIFTDAY = " + shiftDate + "";
            leftJoinCondition += "    AND S.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "' AND E.TPCODE = '" + timePeriodCode.Trim().ToUpper() + "'";
            leftJoinCondition += "  GROUP BY Y.ITEMCODE, Y.SHIFTDAY, E.SHIFTCODE, E.TPCODE) SS ";
            leftJoinCondition += "  ON TT.SHIFTDAY =SS.SHIFTDAY    AND TT.ITEMCODE =SS.ITEMCODE";

            leftJoinCondition += "   LEFT JOIN TBLMATERIAL MM ON TT.ITEMCODE = MM.MCODE";

            leftJoinCondition += "  LEFT JOIN (SELECT SUM(RPT.MOLINEOUTPUTCOUNT) AS MOLINEOUTPUTCOUNT,SUM(RPT.MOOUTPUTWHITECARDCOUNT) AS MOOUTPUTWHITECARDCOUNT,";
            leftJoinCondition += "   DECODE(NVL(SUM(RPT.MOLINEOUTPUTCOUNT), 0),0,0,SUM(RPT.MOOUTPUTWHITECARDCOUNT) /SUM(RPT.MOLINEOUTPUTCOUNT)) AS PASSRCARDRATE,";
            leftJoinCondition += "    T.BIGSSCODE,RPT.ITEMCODE  FROM TBLRPTSOQTY RPT, TBLMESENTITYLIST T";
            leftJoinCondition += " WHERE RPT.TBLMESENTITYLIST_SERIAL = T.SERIAL AND RPT.SHIFTDAY = " + shiftDate + "";
            leftJoinCondition += "   AND T.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'  GROUP BY T.BIGSSCODE, RPT.ITEMCODE) AA ON TT.ITEMCODE =AA.ITEMCODE";

            ReportSQLHelper sqlHelper = new ReportSQLHelper(this.DataProvider);
            string sqlUPPH = sqlHelper.GetPerformanceReportSQL(
                false,
                true,
                false,
                false,
                false,

                "1 = 1 AND **.bigsscode = '" + bigSSCode.Trim().ToUpper() + "'   AND **.shiftday = " + shiftDate + "",
                "**.shiftday AS shiftday",
                "**.itemcode || ' - ' || tblmaterial.mdesc AS itemcode,**.bigsscode AS bigsscode",
                "DECODE(SUM(manhour), 0, 0, SUM(realqty) / (SUM(manhour))) AS upph, SUM(realqty) AS realqty, SUM(manhour) AS manhoursum"

            );


            sqlUPPH = sqlUPPH.Replace("ct.itemcode || ' - ' || tblmaterial.mdesc", "ct.itemcode");

            leftJoinCondition += "   LEFT JOIN (" + sqlUPPH + ") UP ON TT.ITEMCODE =UP.ITEMCODE";

            string manHourPerProductSql = sqlHelper.GetPerformanceReportSQL(
                false,
                true,
                false,
                false,
                false,
                "1 = 1 AND **.bigsscode = '" + bigSSCode.Trim().ToUpper() + "'   AND **.shiftday = " + shiftDate + " ",
                "**.shiftday AS shiftday",
                "**.itemcode || ' - ' || tblmaterial.mdesc AS itemcode,**.bigsscode AS bigsscode",
                "DECODE(SUM(standardqty), 0, 0, (SUM(manhour)) / SUM(standardqty)) AS manhourperproduct, SUM(standardqty) AS standardqty, SUM(manhour) AS manhoursum"
            );

            manHourPerProductSql = manHourPerProductSql.Replace("ct.itemcode || ' - ' || tblmaterial.mdesc", "ct.itemcode");


            leftJoinCondition += "  LEFT JOIN (" + manHourPerProductSql + ") CY ON TT.ITEMCODE =CY.ITEMCODE";

            mainCondition += leftJoinCondition;

            return this.DataProvider.CustomQuery(typeof(watchPanelProductDate), new SQLCondition(mainCondition));
        }

        public object[] QueryPassRateData(string bigSSCode, int shiftDate)
        {
            string sql = " SELECT TPDESC AS TPCODE,SHIFTTYPECODE,SHIFTCODE,TPSEQ,SUM(CT.MOLINEOUTPUTCOUNT) AS OUTPUT,SUM(CT.MOOUTPUTWHITECARDCOUNT) AS PASSRCARDQTY,";
            sql += " DECODE(NVL(SUM(CT.MOLINEOUTPUTCOUNT), 0),0,0,SUM(CT.MOOUTPUTWHITECARDCOUNT) / SUM(CT.MOLINEOUTPUTCOUNT)) AS PASSRCARDRATE";
            sql += "  FROM (SELECT TBLRPTSOQTY.SHIFTDAY,TBLRPTSOQTY.MOOUTPUTCOUNT, TBLRPTSOQTY.MOINPUTCOUNT,TBLRPTSOQTY.MOOUTPUTWHITECARDCOUNT,";
            sql += "   TBLRPTSOQTY.OPCOUNT,TBLRPTSOQTY.EATTRIBUTE1,TBLRPTSOQTY.LINEOUTPUTCOUNT,TBLRPTSOQTY.MOWHITECARDCOUNT,";
            sql += "   TBLRPTSOQTY.MOLINEOUTPUTCOUNT,TBLRPTSOQTY.OPWHITECARDCOUNT,TBLRPTSOQTY.LINEINPUTCOUNT,TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL,";
            sql += "     TBLRPTSOQTY.ITEMCODE,TBLRPTSOQTY.MOCODE,TBLMESENTITYLIST.SHIFTCODE,TBLMESENTITYLIST.SSCODE,TBLMESENTITYLIST.FACCODE,";
            sql += "    TBLMESENTITYLIST.MODELCODE,TBLMESENTITYLIST.SEGCODE,TBLMESENTITYLIST.OPCODE,TBLMESENTITYLIST.SHIFTTYPECODE,TBLMESENTITYLIST.BIGSSCODE,";
            sql += "  TBLMESENTITYLIST.TPCODE,TBLMESENTITYLIST.SERIAL,TBLMESENTITYLIST.RESCODE,TBLMESENTITYLIST.ORGID,TBLTP.TPDESC,TBLTP.TPSEQ  FROM TBLRPTSOQTY";
            sql += "  INNER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL =TBLMESENTITYLIST.SERIAL";
            sql += "   INNER JOIN TBLTP ON TBLTP.TPCODE = TBLMESENTITYLIST.TPCODE) CT";
            sql += "  WHERE 1 = 1   AND CT.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "' AND CT.SHIFTDAY = " + shiftDate + "";
            sql += " GROUP BY TPDESC,SHIFTTYPECODE,SHIFTCODE,TPSEQ ";
            sql += "  ORDER BY SHIFTTYPECODE,SHIFTCODE,TPSEQ";

            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        public object[] QueryOutPutQtyData(string bigSSCode, int shiftDate)
        {
            string sql = "SELECT TBLTP.Tpdesc AS TPCODE,TBLTP.Shifttypecode AS Shifttypecode,TBLTP.Shiftcode AS Shiftcode,";
            sql += "  tbltp.tpseq AS tpseq,SUM(TBLRPTSOQTY.MOINPUTCOUNT) AS INPUT,SUM(TBLRPTSOQTY.MOLINEOUTPUTCOUNT) AS OUTPUT";
            sql += " FROM TBLRPTSOQTY  LEFT OUTER JOIN TBLMESENTITYLIST TBLMESENTITYLIST ON TBLMESENTITYLIST.SERIAL =TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL";
            sql += " INNER JOIN TBLTP ON TBLTP.TPCODE = TBLMESENTITYLIST.TPCODE";
            sql += " WHERE 1 = 1   AND TBLMESENTITYLIST.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'   AND TBLRPTSOQTY.SHIFTDAY = " + shiftDate + "";
            sql += "   GROUP BY TBLTP.Tpdesc,TBLTP.Shifttypecode,TBLTP.Shiftcode,tbltp.tpseq";
            sql += " ORDER BY TBLTP.Shifttypecode,TBLTP.Shiftcode,tbltp.tpseq";

            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql)); ;
        }

        public object GettimePeriod(string bigSSCode)
        {
            string sql = "SELECT DISTINCT TPCODE,TBLTP.shifttypecode     FROM TBLTP     LEFT JOIN TBLSS ON TBLTP.SHIFTTYPECODE = TBLSS.SHIFTTYPECODE";
            sql += " WHERE ((TPBTIME < TPETIME AND  TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) BETWEEN TPBTIME AND TPETIME) ";
            sql += "      OR (TPBTIME > TPETIME AND  TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) < TPBTIME ";
            sql += "         AND TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) + 240000 BETWEEN   TPBTIME AND TPETIME + 240000)";
            sql += "      OR (TPBTIME > TPETIME  AND  TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) > TPBTIME ";
            sql += "      AND TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')) BETWEEN TPBTIME AND TPETIME + 240000))";
            sql += "      AND TBLSS.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'";
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

        public object[] QueryOutPutQtyByGourpConditin(string bigLineList, string itemType, string gourpByCondition, bool isGroupByBigLine, int shiftDay)
        {
            int beginDate = Convert.ToInt32(shiftDay.ToString().Substring(0, 6) + "01");
            int endDate = Convert.ToInt32(shiftDay.ToString().Substring(0, 6) + "31");

            string sql = string.Empty;
            bigLineList = bigLineList.Replace(",", "','");
            if (gourpByCondition == "PerTime")
            {
                sql = " SELECT tbltp.tpdesc AS TPCODE,tbltp.shifttypecode,tbltp.shiftcode,tbltp.tpseq,";
            }

            if (gourpByCondition == "PerDay")
            {
                sql = "  SELECT TBLRPTSOQTY.SHIFTDAY AS tpcode,";
            }

            if (isGroupByBigLine)
            {
                sql += " TBLMESENTITYLIST.Bigsscode,";
            }

            sql += " SUM(TBLRPTSOQTY.MOINPUTCOUNT) AS INPUT,SUM(TBLRPTSOQTY.MOLINEOUTPUTCOUNT) AS OUTPUT  FROM TBLRPTSOQTY";
            sql += " LEFT OUTER JOIN TBLMESENTITYLIST TBLMESENTITYLIST ON TBLMESENTITYLIST.SERIAL =TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL";
            sql += "  LEFT OUTER JOIN TBLMATERIAL ON TBLRPTSOQTY.ITEMCODE = TBLMATERIAL.MCODE";
            sql += " INNER  JOIN tbltp ON tbltp.tpcode=TBLMESENTITYLIST.Tpcode";
            sql += "  WHERE 1 = 1   AND TBLMESENTITYLIST.BIGSSCODE IN ('" + bigLineList.Trim().ToUpper() + "')";

            if (itemType.Trim() != string.Empty)
            {
                sql += " AND TBLMATERIAL.Mtype='" + itemType + "'";
            }

            if (gourpByCondition == "PerDay")
            {
                sql += "    AND TBLRPTSOQTY.SHIFTDAY >=" + beginDate + " AND TBLRPTSOQTY.SHIFTDAY <= " + endDate + "";
                if (isGroupByBigLine)
                {
                    sql += "   GROUP BY TBLRPTSOQTY.SHIFTDAY,TBLMESENTITYLIST.Bigsscode ORDER BY TBLRPTSOQTY.SHIFTDAY,TBLMESENTITYLIST.Bigsscode";
                }
                else
                {
                    sql += "   GROUP BY TBLRPTSOQTY.SHIFTDAY ORDER BY TBLRPTSOQTY.SHIFTDAY";
                }
            }

            if (gourpByCondition == "PerTime")
            {
                sql += "    AND TBLRPTSOQTY.SHIFTDAY =" + shiftDay + "";

                if (isGroupByBigLine)
                {
                    sql += "  GROUP BY tbltp.tpdesc,tbltp.shifttypecode,tbltp.shiftcode,tbltp.tpseq,TBLMESENTITYLIST.Bigsscode ORDER BY tbltp.shifttypecode,tbltp.shiftcode,tbltp.tpseq,TBLMESENTITYLIST.Bigsscode";
                }
                else
                {
                    sql += "  GROUP BY tbltp.tpdesc,tbltp.shifttypecode,tbltp.shiftcode,tbltp.tpseq ORDER BY tbltp.shifttypecode,tbltp.shiftcode,tbltp.tpseq";
                }
            }

            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        public object[] QueryRateByGourpConditin(string bigLineList, string itemType, string gourpByCondition, int shiftDay)
        {
            int beginDate = Convert.ToInt32(shiftDay.ToString().Substring(0, 6) + "01");
            int endDate = Convert.ToInt32(shiftDay.ToString().Substring(0, 6) + "31");
            string sql = string.Empty;
            bigLineList = bigLineList.Replace(",", "','");

            if (gourpByCondition == "PerTime")
            {
                sql = " SELECT CT.TPDESC AS TPCODE,CT.SHIFTTYPECODE,CT.SHIFTCODE,CT.TPSEQ,";
            }

            if (gourpByCondition == "PerDay")
            {
                sql = "  SELECT CT.SHIFTDAY AS TPCODE,";
            }

            sql += "  SUM(CT.MOLINEOUTPUTCOUNT) AS OUTPUT,SUM(CT.MOOUTPUTWHITECARDCOUNT) AS PASSRCARDQTY,";
            sql += " DECODE(NVL(SUM(CT.MOLINEOUTPUTCOUNT), 0),0,0,SUM(CT.MOOUTPUTWHITECARDCOUNT) / SUM(CT.MOLINEOUTPUTCOUNT)) AS PASSRCARDRATE";
            sql += " FROM (SELECT TBLRPTSOQTY.SHIFTDAY,TBLRPTSOQTY.MOOUTPUTCOUNT,TBLRPTSOQTY.MOINPUTCOUNT,TBLRPTSOQTY.MOOUTPUTWHITECARDCOUNT,";
            sql += "   TBLRPTSOQTY.OPCOUNT,TBLRPTSOQTY.EATTRIBUTE1,TBLRPTSOQTY.LINEOUTPUTCOUNT,TBLRPTSOQTY.MOWHITECARDCOUNT,TBLRPTSOQTY.MOLINEOUTPUTCOUNT,";
            sql += "   TBLRPTSOQTY.OPWHITECARDCOUNT,TBLRPTSOQTY.LINEINPUTCOUNT,TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL,TBLRPTSOQTY.ITEMCODE,";
            sql += "    TBLRPTSOQTY.MOCODE,TBLMESENTITYLIST.SHIFTCODE,TBLMESENTITYLIST.SSCODE,TBLMESENTITYLIST.FACCODE,TBLMESENTITYLIST.MODELCODE,";
            sql += " TBLMESENTITYLIST.SEGCODE,TBLMESENTITYLIST.OPCODE,TBLMESENTITYLIST.SHIFTTYPECODE,TBLMESENTITYLIST.BIGSSCODE,TBLMESENTITYLIST.TPCODE,";
            sql += "    TBLMESENTITYLIST.SERIAL,TBLMESENTITYLIST.RESCODE,TBLMESENTITYLIST.ORGID,TBLMATERIAL.Mtype,TBLTP.TPDESC,TBLTP.TPSEQ    FROM TBLRPTSOQTY";
            sql += "       INNER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL =TBLMESENTITYLIST.SERIAL";
            sql += "     LEFT OUTER JOIN TBLMATERIAL ON TBLRPTSOQTY.ITEMCODE =TBLMATERIAL.MCODE";
            sql += "  INNER JOIN TBLTP ON TBLTP.TPCODE = TBLMESENTITYLIST.TPCODE) CT WHERE 1 = 1";
            sql += "  AND CT.BIGSSCODE IN ('" + bigLineList.Trim().ToUpper() + "')";

            if (itemType.Trim() != string.Empty)
            {
                sql += "  AND Ct.Mtype='" + itemType + "'";
            }

            if (gourpByCondition == "PerDay")
            {
                sql += "    AND CT.SHIFTDAY >=" + beginDate + " AND CT.SHIFTDAY <= " + endDate + "";
                sql += "   GROUP BY CT.SHIFTDAY ORDER BY CT.SHIFTDAY";
            }

            if (gourpByCondition == "PerTime")
            {
                sql += "    AND CT.SHIFTDAY = " + shiftDay + "";
                sql += "  GROUP BY CT.TPDESC, CT.SHIFTTYPECODE, CT.SHIFTCODE, CT.TPSEQ ORDER BY CT.SHIFTTYPECODE, CT.SHIFTCODE, CT.TPSEQ";
            }

            return this.DataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        public object[] QueryProudctDataByDateAndSSCodeList(int shiftDate, string bigSSCodeList)
        {
            bigSSCodeList = bigSSCodeList.Trim().ToUpper().Replace(",", "','");
            string Sql = string.Empty;
            Sql = " SELECT  nvl(daydate.PASSRATE,0) as PASSRATE,nvl(daydate.MTYPE,D.MTYPE) as MTYPE,nvl(daydate.MANCOUNT,0) as MANCOUNT,nvl(daydate.UPPH,0) as UPPH, D.MONTHPRODUCTQTY FROM (";
            Sql += " SELECT A.PASSRATE, A.MTYPE, B.MANCOUNT, C.UPPH ";
            Sql += "     FROM (SELECT SUM(RPT.MOLINEOUTPUTCOUNT) AS MOLINEOUTPUTCOUNT,";
            Sql += "                    SUM(RPT.MOOUTPUTWHITECARDCOUNT) AS MOOUTPUTWHITECARDCOUNT,";
            Sql += "                    DECODE(NVL(SUM(RPT.MOLINEOUTPUTCOUNT), 0),0,0,SUM(RPT.MOOUTPUTWHITECARDCOUNT) /SUM(RPT.MOLINEOUTPUTCOUNT)) AS PASSRATE,";
            Sql += "                    RPT.SHIFTDAY AS SHIFTDATE,MA.MTYPE  FROM TBLRPTSOQTY RPT, TBLMESENTITYLIST T, TBLMATERIAL MA";
            Sql += "            WHERE RPT.TBLMESENTITYLIST_SERIAL = T.SERIAL  AND RPT.ITEMCODE = MA.MCODE";
            Sql += "                    AND RPT.SHIFTDAY = " + shiftDate + "";
            Sql += "                    AND T.BIGSSCODE IN ('" + bigSSCodeList + "')";
            Sql += "                    GROUP BY RPT.SHIFTDAY, MA.MTYPE   ORDER BY RPT.SHIFTDAY, MA.MTYPE) A  FULL JOIN";

            Sql += "            (SELECT COUNT(1) AS MANCOUNT, SHIFTDATE, TBLMATERIAL.MTYPE";
            Sql += "                    FROM TBLLINE2MANDETAIL, TBLMATERIAL, TBLMO,TBLSS";
            Sql += "             WHERE TBLLINE2MANDETAIL.MOCODE = TBLMO.MOCODE  AND TBLMO.ITEMCODE = TBLMATERIAL.MCODE";
            Sql += "                   AND TBLLINE2MANDETAIL.Sscode=TBLSS.Sscode";
            Sql += "                   AND SHIFTDATE = " + shiftDate + "";
            Sql += "                    AND TBLSS.Bigsscode IN  ('" + bigSSCodeList + "')";
            Sql += "                    AND STATUS IN ('" + Line2ManDetailStatus.Line2ManDetailStatus_On + "', '" + Line2ManDetailStatus.Line2ManDetailStatus_AutoOn + "')";
            Sql += "                    GROUP BY SHIFTDATE, TBLMATERIAL.MTYPE) B ON A.MTYPE =B.MTYPE FULL JOIN  ";

            ReportSQLHelper sqlHelper = new ReportSQLHelper(this.DataProvider);
            string sqlUPPH = sqlHelper.GetPerformanceReportSQL(
                false,
                true,
                false,
                false,
                false,

                "1 = 1 AND **.bigsscode in ('" + bigSSCodeList + "')   AND **.shiftday =" + shiftDate + " ",
                "**.shiftday AS shiftday",
                "tblmaterial.mtype",
                "DECODE(SUM(manhour), 0, 0, SUM(realqty) / (SUM(manhour))) AS upph, SUM(realqty) AS realqty, SUM(manhour) AS manhoursum"

            );

            Sql += "   (" + sqlUPPH + ") C ON A.MTYPE = C.MTYPE) daydate FULL JOIN ";
            Sql += "   (" + GetOutPutQtyGroupByItemTypeSql(bigSSCodeList, shiftDate, true) + ") D";

            Sql += "  ON daydate.MTYPE=D.MTYPE";

            return this.DataProvider.CustomQuery(typeof(watchPanelProductDate), new SQLCondition(Sql));
        }

        public object[] QueryOutPutQtyGroupByItemType(string bigSSCodeList, int shiftDate, bool isMonthOutPut)
        {
            bigSSCodeList = bigSSCodeList.Trim().ToUpper().Replace(",", "','");
            string sql = GetOutPutQtyGroupByItemTypeSql(bigSSCodeList, shiftDate, isMonthOutPut);
            return this.DataProvider.CustomQuery(typeof(watchPanelProductDate), new SQLCondition(sql));
        }

        private string GetOutPutQtyGroupByItemTypeSql(string bigSSCodeList, int shiftDate, bool isMonthOutPut)
        {
            string monthDays = shiftDate.ToString().Substring(0, 6);
            int BenMonthDay = shiftDate;
            int EndMonthDay = shiftDate;

            if (isMonthOutPut)
            {
                BenMonthDay = int.Parse(monthDays + "01");
                EndMonthDay = int.Parse(monthDays + "31");
            }


            string Sql = "SELECT TBLMATERIAL.MTYPE,SUM(TBLRPTSOQTY.MOINPUTCOUNT) AS INPUT,SUM(TBLRPTSOQTY.MOLINEOUTPUTCOUNT) AS MONTHPRODUCTQTY";
            Sql += "         FROM TBLRPTSOQTY";
            Sql += "        LEFT OUTER JOIN TBLMESENTITYLIST ON TBLMESENTITYLIST.SERIAL =TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL";
            Sql += "        LEFT JOIN TBLMATERIAL ON TBLMATERIAL.MCODE = TBLRPTSOQTY.ITEMCODE";
            Sql += "        WHERE 1 = 1";
            Sql += "         AND TBLMESENTITYLIST.BIGSSCODE IN ('" + bigSSCodeList + "')";
            Sql += "        AND TBLRPTSOQTY.SHIFTDAY>=" + BenMonthDay + " AND TBLRPTSOQTY.SHIFTDAY<=" + EndMonthDay + "";
            Sql += "        GROUP BY TBLMATERIAL.MTYPE";

            return Sql;
        }

        public bool CheckLineIsProduct(string bigSSCode, int date, int time)
        {
            long dateTime = date * 1000000L + time;
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += "FROM tblline2mandetail ";
            sql += "WHERE status IN ('" + Line2ManDetailStatus.Line2ManDetailStatus_On + "', '" + Line2ManDetailStatus.Line2ManDetailStatus_AutoOn + "') ";
            sql += "AND EXISTS(SELECT * FROM tblss WHERE sscode = tblline2mandetail.sscode AND bigsscode = '" + bigSSCode + "') ";
            sql += "AND " + dateTime.ToString() + " BETWEEN ondate * 1000000 + ontime AND offdate * 1000000 + offtime ";

            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            return count > 0;
        }

        public object[] QueryOQCLotPassRate(int shiftDate, string bigLineCode)
        {
            string[] bigLineCodelist = bigLineCode.Split(',');
            bigLineCode = string.Empty;
            for (int i = 0; i < bigLineCodelist.Length; i++)
            {
                bigLineCode += "'" + bigLineCodelist[i] + "',";
            }
            bigLineCode = bigLineCode.Substring(0, bigLineCode.Length - 1);

            string sql = " SELECT SSCODE, OQCLOTPASSRATE";
            sql += " FROM (SELECT CT.SSCODE,CT.DDATE AS DDATE,SUM(CT.PASSCOUNT) AS OQCPASSCOUNT,";
            sql += "             SUM(CT.LOTCOUNT) AS OQCLOTCOUNT,SUM(CT.PASSCOUNT) / SUM(CT.LOTCOUNT) AS OQCLOTPASSRATE";
            sql += "          FROM (SELECT TBLLOT.LOTNO,TBLLOT.ITEMCODE,TBLLOT.DDATE,TBLLOT.SSCODE,";
            sql += "                       TBLLOT.RESCODE,TBLSS.BIGSSCODE,TBLLINE2CREW.CREWCODE,";
            sql += "                       COUNT(LOTNO) AS LOTCOUNT,";
            sql += "                       SUM(DECODE(LOTSTATUS, '" + OQCLotStatus.OQCLotStatus_Pass + "', 1, 0)) AS PASSCOUNT";
            sql += "                  FROM TBLLOT";
            sql += "                  LEFT OUTER JOIN TBLSS ON TBLLOT.SSCODE = TBLSS.SSCODE";
            sql += "                  LEFT OUTER JOIN TBLLINE2CREW ON TBLLOT.SHIFTDAY =TBLLINE2CREW.SHIFTDATE";
            sql += "                                              AND TBLLOT.SHIFTCODE =TBLLINE2CREW.SHIFTCODE";
            sql += "                                             AND TBLLOT.SSCODE =TBLLINE2CREW.SSCODE";
            sql += "                 GROUP BY TBLLOT.LOTNO,TBLLOT.ITEMCODE,TBLLOT.DDATE, TBLLOT.SSCODE,";
            sql += "                          TBLLOT.RESCODE,TBLSS.BIGSSCODE,TBLLINE2CREW.CREWCODE) CT";
            sql += "          LEFT OUTER JOIN TBLLOT TBLLOT ON TBLLOT.LOTNO = CT.LOTNO";
            sql += "        WHERE 1 = 1";
            sql += "           AND TBLLOT.OQCLOTTYPE IN ('" + OQCLotType.OQCLotType_Normal + "', '" + OQCLotType.OQCLotType_Split + "')";
            sql += "  AND TBLLOT.LOTSTATUS IN   ('" + OQCLotStatus.OQCLotStatus_Pass + "', '" + OQCLotStatus.OQCLotStatus_Reject + "')";
            sql += "           AND CT.DDATE = " + shiftDate + "";
            sql += "            AND CT.BIGSSCODE IN (" + bigLineCode.Trim().ToUpper() + ")";
            sql += "          GROUP BY CT.SSCODE, CT.DDATE   ORDER BY CT.SSCODE, CT.DDATE)";

            return this.DataProvider.CustomQuery(typeof(watchPanelProductDate), new SQLCondition(sql));
        }

        public object[] QueryErrorCasueTopFive(int shiftDate, string bigLineCode)
        {
            string[] bigLineCodelist = bigLineCode.Split(',');
            bigLineCode = string.Empty;
            for (int i = 0; i < bigLineCodelist.Length; i++)
            {
                bigLineCode += "'" + bigLineCodelist[i] + "',";
            }
            bigLineCode = bigLineCode.Substring(0, bigLineCode.Length - 1);

            string sql = " SELECT ERRORCAUSE, ECSDESC, ALLQTY, QTY, PERCENT";
            sql += "     FROM (SELECT ECSCODE ERRORCAUSE,SUM(QTY) OVER() ALLQTY,";
            sql += "            QTY, RATIO_TO_REPORT(QTY) OVER() PERCENT";
            sql += "             FROM (SELECT ECSCODE, COUNT(DISTINCT TSID) AS QTY";
            sql += "                FROM TBLTSERRORCAUSE";
            sql += "             WHERE (TSID, ECGCODE, ECODE, ECSCODE) IN";
            sql += "                   (SELECT TS.TSID,ECS.ECGCODE,ECS.ECODE,ECS.ECSCODE";
            sql += "                   FROM TBLTS TS,";
            sql += "                        TBLTSERRORCAUSE     ECS,";
            sql += "                        TBLTSERRORCAUSE2LOC EL,";
            sql += "                        TBLMATERIAL         MAL,";
            sql += "                        TBLITEMCLASS        ITS,";
            sql += "                        TBLTSERRORCAUSE2COM SC,";
            sql += "                        TBLSS               SS";
            sql += "                           WHERE TS.TSID = ECS.TSID(+)";
            sql += "                             AND ECS.TSID = EL.TSID(+)";
            sql += "                             AND ECS.ECGCODE = EL.ECGCODE(+)";
            sql += "                             AND ECS.ECODE = EL.ECODE(+)";
            sql += "                             AND ECS.ECSCODE = EL.ECSCODE(+)";
            sql += "                             AND ECS.ECSGCODE = EL.ECSGCODE(+)";
            sql += "                              AND TS.ITEMCODE = MAL.MCODE(+)";
            sql += "                             AND ITS.ITEMGROUP(+) = MAL.MGROUP";
            sql += "                             AND ECS.TSID = SC.TSID(+)";
            sql += "                             AND ECS.ECODE = SC.ECODE(+)";
            sql += "                             AND ECS.ECGCODE = SC.ECGCODE(+)";
            sql += "                             AND ECS.ECSCODE = SC.ECSCODE(+)";
            sql += "                             AND ECS.ECSGCODE = SC.ECSGCODE(+)";
            sql += "                            AND TS.FRMSSCODE=SS.SSCODE(+)";
            sql += "                            AND ss.bigsscode IN (" + bigLineCode.Trim().ToUpper() + ")";
            sql += "                            AND TS.SHIFTDAY = " + shiftDate + ")";
            sql += "             GROUP BY ECSCODE)) A,";
            sql += "                TBLECS B";
            sql += "        WHERE A.ERRORCAUSE = B.ECSCODE";

            return this.DataProvider.CustomQuery(typeof(QDOTSInfo), new PagerCondition(sql, "QTY DESC", 1, 5));
        }

        public bool CheckBigLineCodeIsHaveSSCode(string bigLineCode)
        {
            string sql = "SELECT COUNT(DISTINCT sscode) FROM tblss WHERE bigsscode='" + bigLineCode.Trim().ToUpper() + "'";
            int count = this.DataProvider.GetCount(new SQLCondition(sql));

            if (count > 0)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
