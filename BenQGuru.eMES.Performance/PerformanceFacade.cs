using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Performance
{
    public class PerformanceFacade : MarshalByRefObject
    {
        private FacadeHelper _helper = null;
        private IDomainDataProvider _domainDataProvider = null;

        public override object InitializeLifetimeService()
        {
            return null;
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

        public PerformanceFacade()
        {
        }

        public PerformanceFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        #region PlanWorkTime

        public PlanWorkTime CreateNewPlanWorkTime()
        {
            return new PlanWorkTime();
        }

        public void AddPlanWorkTime(PlanWorkTime planWorkTime)
        {
            this._helper.AddDomainObject(planWorkTime);
        }

        public void DeletePlanWorkTime(PlanWorkTime planWorkTime)
        {
            this._helper.DeleteDomainObject(planWorkTime);
        }

        public void DeletePlanWorkTime(PlanWorkTime[] planWorkTime)
        {
            this._helper.DeleteDomainObject(planWorkTime);
        }

        public void UpdatePlanWorkTime(PlanWorkTime planWorkTime)
        {
            this._helper.UpdateDomainObject(planWorkTime);
        }

        public object GetPlanWorkTime(string itemCode, string sSCode)
        {
            return this.DataProvider.CustomSearch(typeof(PlanWorkTime), new object[] { itemCode, sSCode });
        }

        public object[] QueryPlanWorkTime(string itemCode, string sSCode, string bigSSCode, int inclusive, int exclusive)
        {
            string sql = this.GetPlanWorkTimeSql(itemCode, sSCode, bigSSCode);
            return this.DataProvider.CustomQuery(typeof(PlanWorkTimeWithMessage), new PagerCondition(sql, inclusive, exclusive));
        }

        public int GetPlanWorkTimeCount(string itemCode, string sSCode, string bigSSCode)
        {
            string sql = "  SELECT COUNT(*) FROM (" + this.GetPlanWorkTimeSql(itemCode, sSCode, bigSSCode) + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        private string GetPlanWorkTimeSql(string itemCode, string sSCode, string bigSSCode)
        {
            string sql = "  SELECT ITEMCODE, C.MDESC AS ITEMDESC, A.SSCODE, B.SSDESC AS SSDESC,";
            sql += "        CYCLETIME,WORKINGTIME,A.MUSER || '-' || D.USERNAME AS MUSER,A.MDATE,A.MTIME,B.BIGSSCODE";
            sql += "      FROM TBLPLANWORKTIME A   LEFT JOIN TBLSS B ON A.SSCODE = B.SSCODE";
            sql += "  LEFT JOIN TBLMATERIAL C ON A.ITEMCODE = C.MCODE";
            sql += "   LEFT JOIN TBLUSER D ON A.MUSER = D.USERCODE WHERE 1=1";

            if (itemCode.Trim() != string.Empty)
            {
                if (itemCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = itemCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    itemCode = string.Join(",", list);
                    sql += " AND  A.ITEMCODE in (" + itemCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.ITEMCODE = '" + itemCode.Trim().ToUpper() + "'";
                }
            }

            if (sSCode.Trim() != string.Empty)
            {
                sql += " AND A.SSCODE = '" + sSCode.Trim().ToUpper() + "'";
            }

            if (bigSSCode.Trim() != string.Empty)
            {
                sql += " AND B.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "'";
            }

            sql += "   ORDER BY A.ITEMCODE, A.SSCODE, B.BIGSSCODE ";

            return sql;
        }
        #endregion

        #region Line2Crew

        public Line2Crew CreateNewLine2Crew()
        {
            return new Line2Crew();
        }

        public void AddLine2Crew(Line2Crew line2Crew)
        {
            this._helper.AddDomainObject(line2Crew);
        }

        public void DeleteLine2Crew(Line2Crew line2Crew)
        {
            this._helper.DeleteDomainObject(line2Crew);
        }

        public void DeleteLine2Crew(Line2Crew[] line2Crew)
        {
            this._helper.DeleteDomainObject(line2Crew);
        }

        public void UpdateLine2Crew(Line2Crew line2Crew)
        {
            this._helper.UpdateDomainObject(line2Crew);
        }

        public object GetLine2Crew(int shiftDate, string sSCode, string shiftCode)
        {
            return this.DataProvider.CustomSearch(typeof(Line2Crew), new object[] { shiftDate, sSCode, shiftCode });
        }

        public object[] QueryLine2Crew(int shiftDate, string sSCode, string shiftCode, string crewCode, int inclusive, int exclusive)
        {
            string sql = GetLine2CrewCountSql(shiftDate, sSCode, shiftCode, crewCode);
            return this.DataProvider.CustomQuery(typeof(Line2CrewWithMessage), new PagerCondition(sql, inclusive, exclusive));
        }

        public int GetLine2CrewCount(int shiftDate, string sSCode, string shiftCode, string crewCode)
        {
            string sql = " SELECT COUNT(*) FROM (" + GetLine2CrewCountSql(shiftDate, sSCode, shiftCode, crewCode) + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetLine2CrewCountSql(int shiftDate, string sSCode, string shiftCode, string crewCode)
        {
            string sql = " SELECT A.SHIFTDATE,A.SSCODE,B.SSDESC,A.SHIFTCODE,C.SHIFTDESC,A.CREWCODE,D.CREWDESC,B.BIGSSCODE,";
            sql += "  A.MUSER || '-' || E.USERNAME AS MUSER,A.MDATE, A.MTIME   FROM TBLLINE2CREW A";
            sql += "   LEFT JOIN TBLSS B ON A.SSCODE = B.SSCODE  LEFT JOIN TBLSHIFT C ON A.SHIFTCODE = C.SHIFTCODE";
            sql += "   LEFT JOIN TBLCREW D ON A.CREWCODE = D.CREWCODE  LEFT JOIN TBLUSER E ON A.MUSER = E.USERCODE";
            sql += "  WHERE 1 = 1";
            if (shiftDate > 0)
            {
                sql += " AND A.SHIFTDATE = " + shiftDate + " ";
            }
            if (sSCode.Trim() != string.Empty)
            {
                sql += " AND A.SSCODE = '" + sSCode.Trim().ToUpper() + "' ";
            }
            if (shiftCode.Trim() != string.Empty)
            {
                if (shiftCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = shiftCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    shiftCode = string.Join(",", list);
                    sql += " AND  A.shiftCode in (" + shiftCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.shiftCode = '" + shiftCode.Trim().ToUpper() + "'";
                }
            }

            if (crewCode.Trim() != string.Empty)
            {
                if (crewCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = crewCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    crewCode = string.Join(",", list);
                    sql += " AND  A.crewCode in (" + crewCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.crewCode = '" + crewCode.Trim().ToUpper() + "'";
                }
            }
            sql += "  ORDER BY A.SHIFTDATE, A.SSCODE, A.SHIFTCODE,a.crewcode";

            return sql;
        }
        #endregion

        #region Line2ManDetail

        #region Basic Functions

        public Line2ManDetail CreateNewLine2ManDetail()
        {
            return new Line2ManDetail();
        }

        public void AddLine2ManDetail(Line2ManDetail line2ManDetail)
        {
            this._helper.AddDomainObject(line2ManDetail);
        }

        public void DeleteLine2ManDetail(Line2ManDetail line2ManDetail)
        {
            this._helper.DeleteDomainObject(line2ManDetail);
        }

        public void UpdateLine2ManDetail(Line2ManDetail line2ManDetail)
        {
            this._helper.UpdateDomainObject(line2ManDetail);
        }

        public object GetLine2ManDetail(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(Line2ManDetail), new object[] { serial });
        }

        //public object[] QueryLine2ManDetail(string userCode, string ssCode, string resCode, string opCode, string statusList, int inclusive, int exclusive)
        //{
        //    string sql = string.Empty;
        //    sql += string.Format("SELECT {0} ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Line2ManDetail)));
        //    sql += "FROM tblline2mandetail ";
        //    sql += QueryLine2ManDetailWhereCondition(userCode, ssCode, resCode, opCode, statusList);

        //    return this.DataProvider.CustomQuery(typeof(Line2ManDetail), new PagerCondition(sql, "serial", inclusive, exclusive));
        //}

        public int QueryLine2ManDetailCount(string userCode, string ssCode, string resCode, string opCode, int shiftDate, string shiftCode, string statusList)
        {
            string sql = string.Empty;
            sql += "SELECT /*+ ORDERED */ COUNT (DISTINCT usercode) ";
            sql += "FROM ( ";
            sql += QueryUserCurrentLine2ManDetailSQL(userCode, ssCode, resCode, opCode, shiftDate, shiftCode, string.Empty);
            sql += ") tblline2mandetail ";
            if (statusList.Trim().Length > 0)
            {
                sql += string.Format("WHERE status IN({0}) ", FormatHelper.ProcessQueryValues(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(statusList))).ToLower());
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object GetUserCurrentLine2ManDetail(string userCode)
        {
            object[] list = QueryUserCurrentLine2ManDetail(userCode, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty);

            if (list != null && list.Length > 0)
            {
                return list[list.Length - 1];
            }
            else
            {
                return null;
            }
        }

        public object[] QueryUserCurrentLine2ManDetail(string userCode, string ssCode, string resCode, string opCode, int shiftDate, string shiftCode, string statusList)
        {
            return QueryUserCurrentLine2ManDetail(userCode, ssCode, resCode, opCode, shiftDate, shiftCode, statusList, false);
        }

        public object[] QueryUserCurrentLine2ManDetail(string userCode, string ssCode, string resCode, string opCode, int shiftDate, string shiftCode, string statusList, bool isForUpdate)
        {
            string sql = string.Empty;
            sql += QueryUserCurrentLine2ManDetailSQL(userCode, ssCode, resCode, opCode, shiftDate, shiftCode, statusList);

            sql += "ORDER BY sscode, usercode, shiftdate, shiftcode ";
            if (isForUpdate)
            {
                sql += "FOR UPDATE ";
            }

            return this.DataProvider.CustomQuery(typeof(Line2ManDetail), new SQLCondition(sql));
        }

        public string QueryUserCurrentLine2ManDetailSQL(string userCode, string ssCode, string resCode, string opCode, int shiftDate, string shiftCode, string statusList)
        {
            string sql = string.Empty;

            sql += string.Format("SELECT {0} ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Line2ManDetail)));
            sql += "FROM ";
            sql += "( ";
            sql += "SELECT * ";
            sql += "FROM tblline2mandetail a ";
            sql += "WHERE 1 = 1 ";
            if (userCode.Trim().Length > 0)
            {
                sql += string.Format("AND a.usercode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(userCode)));
            }
            sql += "AND NOT EXISTS ";
            sql += "( ";
            sql += "    SELECT * ";
            sql += "    FROM tblline2mandetail b ";
            sql += "    WHERE b.usercode = a.usercode ";
            sql += "    AND b.serial > a.serial ";
            sql += ") ";
            sql += ") tblline2mandetail ";
            sql += "WHERE 1 = 1 ";

            if (ssCode.Trim().Length > 0)
            {
                sql += string.Format("AND sscode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ssCode)));
            }
            if (resCode.Trim().Length > 0)
            {
                sql += string.Format("AND rescode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(resCode)));
            }
            if (opCode.Trim().Length > 0)
            {
                sql += string.Format("AND opcode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(opCode)));
            }
            if (shiftDate > 0)
            {
                sql += string.Format("AND shiftdate = {0} ", shiftDate.ToString());
            }
            if (shiftCode.Trim().Length > 0)
            {
                sql += string.Format("AND shiftcode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(shiftCode)));
            }
            if (statusList.Trim().Length > 0)
            {
                sql += string.Format("AND status IN ({0}) ", FormatHelper.ProcessQueryValues(statusList).ToLower());
            }

            return sql;
        }

        public object[] QueryUserCurrentLine2ManDetailEx(string userCode, string ssCode, string resCode, string opCode, int shiftDate, string shiftCode, string statusList)
        {
            /*
            string sql = string.Empty;
            sql += string.Format("SELECT {0} ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Line2ManDetail)));
            sql += ", NVL(tbluser.username, ' ') AS username, NVL(tblop.opdesc, ' ') AS opdesc, NVL(tblres.resdesc, ' ') AS resdesc, NVL(tblshift.shiftdesc, ' ') AS shiftdesc ";
            sql += "FROM ";
            sql += "( ";
            sql += "SELECT * ";
            sql += "FROM tblline2mandetail a ";
            sql += "WHERE 1 = 1 ";
            if (userCode.Trim().Length > 0)
            {
                sql += string.Format("AND a.usercode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(userCode)));
            }
            sql += "AND NOT EXISTS ";
            sql += "( ";
            sql += "    SELECT * ";
            sql += "    FROM tblline2mandetail b ";
            sql += "    WHERE b.usercode = a.usercode ";
            sql += "    AND b.serial > a.serial ";
            sql += ") ";
            sql += ") tblline2mandetail ";
            sql += "LEFT OUTER JOIN tbluser ";
            sql += "ON tblline2mandetail.usercode = tbluser.usercode ";
            sql += "LEFT OUTER JOIN tblop ";
            sql += "ON tblline2mandetail.opcode = tblop.opcode ";
            sql += "LEFT OUTER JOIN tblres ";
            sql += "ON tblline2mandetail.rescode = tblres.rescode ";
            sql += "LEFT OUTER JOIN tblshift ";
            sql += "ON tblline2mandetail.shiftcode = tblshift.shiftcode ";
            sql += "WHERE 1 = 1 ";

            if (ssCode.Trim().Length > 0)
            {
                sql += string.Format("AND sscode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ssCode)));
            }
            if (resCode.Trim().Length > 0)
            {
                sql += string.Format("AND rescode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(resCode)));
            }
            if (opCode.Trim().Length > 0)
            {
                sql += string.Format("AND opcode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(opCode)));
            }
            if (shiftDate > 0)
            {
                sql += string.Format("AND shiftdate = {0} ", shiftDate.ToString());
            }
            if (shiftCode.Trim().Length > 0)
            {
                sql += string.Format("AND shiftcode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(shiftCode)));
            }
            if (statusList.Trim().Length > 0)
            {
                sql += string.Format("AND status IN ({0}) ", FormatHelper.ProcessQueryValues(statusList).ToLower());
            }

            sql += "ORDER BY sscode, usercode, shiftdate, shiftcode ";
            */
            string sql = string.Empty;
            sql += string.Format("SELECT {0} ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Line2ManDetail)));
            sql += ", tbluser.username,tblop.opdesc, tblres.resdesc, tblshift.shiftdesc ";
            sql += "    FROM (SELECT tblline2mandetail.*";
            sql += "           FROM tblline2mandetail";
            sql += "               RIGHT JOIN";
            sql += "                (SELECT   usercode, MAX (serial) serial";
            sql += "                     FROM tblline2mandetail";
            sql += "                    WHERE 1=1 ";
            if (ssCode.Trim().Length > 0)
            {
                sql += string.Format("AND sscode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ssCode)));
            }
            if (resCode.Trim().Length > 0)
            {
                sql += string.Format(" AND rescode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(resCode)));
            }
            if (opCode.Trim().Length > 0)
            {
                sql += string.Format(" AND opcode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(opCode)));
            }
            if (shiftDate > 0)
            {
                sql += string.Format("AND shiftdate = {0} ", shiftDate.ToString());
            }
            if (shiftCode.Trim().Length > 0)
            {
                sql += string.Format("AND shiftcode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(shiftCode)));
            }
            if (statusList.Trim().Length > 0)
            {
                sql += string.Format("AND status IN ({0}) ", FormatHelper.ProcessQueryValues(statusList).ToLower());
            }
            sql += "                 GROUP BY usercode) b";
            sql += "                ON tblline2mandetail.serial = b.serial";
            sql += "              AND tblline2mandetail.usercode = b.usercode";
            sql += "                ) tblline2mandetail,";
            sql += "        tbluser,";
            sql += "        tblop,";
            sql += "        tblres,";
            sql += "        tblshift";
            sql += "  WHERE 1 = 1  ";


            sql += "     AND tbluser.usercode = tblline2mandetail.usercode";
            sql += "     AND tblline2mandetail.opcode = tblop.opcode";
            sql += "     AND tblline2mandetail.rescode = tblres.rescode";
            sql += "     AND tblline2mandetail.shiftcode = tblshift.shiftcode";
            sql += " ORDER BY tblline2mandetail.sscode,tblline2mandetail.usercode,tblline2mandetail.shiftdate,tblline2mandetail.shiftcode";

            return this.DataProvider.CustomQuery(typeof(Line2ManDetailEx), new SQLCondition(sql));
        }

        public object[] QueryLineOnPostAndPauseList(string ssCode, int shiftDate, string shiftCode, string bigSSCode)
        {
            string sql = string.Empty;
            sql += "SELECT tblline2mandetail.sscode, tblss.ssdesc, TO_CHAR(COUNT(*)) AS eattribute1 ";
            sql += "FROM ";
            sql += "( ";
            sql += "SELECT * ";
            sql += "FROM tblline2mandetail a ";
            sql += "WHERE 1 = 1 ";
            sql += "AND NOT EXISTS ";
            sql += "( ";
            sql += "    SELECT * ";
            sql += "    FROM tblline2mandetail b ";
            sql += "    WHERE b.usercode = a.usercode ";
            sql += "    AND b.serial > a.serial ";
            sql += ") ";
            sql += ") tblline2mandetail ";
            sql += "LEFT OUTER JOIN tblss ";
            sql += "ON tblline2mandetail.sscode = tblss.sscode ";
            sql += "WHERE 1 = 1 ";

            if (ssCode.Trim().Length > 0)
            {
                sql += string.Format("AND tblline2mandetail.sscode = '{0}' ", ssCode);
            }

            if (shiftDate > 0)
            {
                sql += string.Format("AND tblline2mandetail.shiftdate = {0} ", shiftDate.ToString());
            }

            if (shiftCode.Trim().Length > 0)
            {
                sql += string.Format("AND tblline2mandetail.shiftcode = '{0}' ", shiftCode);
            }

            if (bigSSCode.Trim().Length > 0)
            {
                sql += string.Format("AND tblss.bigsscode = '{0}' ", bigSSCode);
            }

            sql += string.Format("AND UPPER(tblline2mandetail.status) IN ({0}) ", FormatHelper.ProcessQueryValues(GetLine2ManDetailStatusList(true) + "," + Line2ManDetailStatus.Line2ManDetailStatus_Pause));
            sql += "GROUP BY tblline2mandetail.sscode, tblss.ssdesc ";
            sql += "ORDER BY tblline2mandetail.sscode, tblss.ssdesc ";

            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(sql));
        }

        #endregion

        #region Business Functions

        private List<string> GetUserList(string ssCode, string statusList)
        {
            List<string> returnValue = new List<string>();

            statusList = "," + statusList + ",";

            object[] line2ManDetailList = QueryUserCurrentLine2ManDetail(string.Empty, ssCode, string.Empty, string.Empty, 0, string.Empty, string.Empty);

            if (line2ManDetailList != null)
            {
                foreach (Line2ManDetail line2ManDetail in line2ManDetailList)
                {
                    if (statusList.IndexOf("," + line2ManDetail.Status + ",") >= 0 && !returnValue.Contains(line2ManDetail.UserCode))
                    {
                        returnValue.Add(line2ManDetail.UserCode);
                    }
                }
            }

            return returnValue;
        }

        public bool IsOnPost(string userCode)
        {
            bool retrunValue = false;

            Line2ManDetail line2ManDetail = (Line2ManDetail)GetUserCurrentLine2ManDetail(userCode);

            if (line2ManDetail != null
                && (line2ManDetail.Status == Line2ManDetailStatus.Line2ManDetailStatus_On
                    || line2ManDetail.Status == Line2ManDetailStatus.Line2ManDetailStatus_AutoOn))
            {
                retrunValue = true;
            }

            return retrunValue;
        }

        public bool IsOffPost(string userCode)
        {
            bool retrunValue = false;

            Line2ManDetail line2ManDetail = (Line2ManDetail)GetUserCurrentLine2ManDetail(userCode);

            if (line2ManDetail == null
                || line2ManDetail.Status == Line2ManDetailStatus.Line2ManDetailStatus_Off
                || line2ManDetail.Status == Line2ManDetailStatus.Line2ManDetailStatus_AutoOff)
            {
                retrunValue = true;
            }

            return retrunValue;
        }

        private bool IsLinePaused(string ssCode)
        {
            bool returnValue = false;

            LinePause linePause = (LinePause)GetLatestLinePause(ssCode);
            if (linePause != null && linePause.EndDate == 0 && linePause.EndTime == 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        private bool IsLineOfResPaused(string resCode)
        {
            bool returnValue = false;

            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            Resource res = (Resource)baseModelFacade.GetResource(resCode);
            if (res != null)
            {
                StepSequence stepSequence = (StepSequence)baseModelFacade.GetStepSequence(res.StepSequenceCode);
                if (stepSequence != null)
                {
                    LinePause linePause = (LinePause)GetLatestLinePause(stepSequence.StepSequenceCode);
                    if (linePause != null && linePause.EndDate == 0 && linePause.EndTime == 0)
                    {
                        returnValue = true;
                    }
                }
            }

            return returnValue;
        }

        private bool IsLineOfUserPaused(string userCode, out string ssCode)
        {
            bool returnValue = false;
            ssCode = string.Empty;

            Line2ManDetail line2ManDetail = (Line2ManDetail)GetUserCurrentLine2ManDetail(userCode);

            if (line2ManDetail != null)
            {
                LinePause linePause = (LinePause)GetLatestLinePause(line2ManDetail.SSCode);
                if (linePause != null && linePause.EndDate == 0 && linePause.EndTime == 0)
                {
                    returnValue = true;
                    ssCode = linePause.SSCode;
                }
            }

            return returnValue;
        }

        public string GetLine2ManDetailStatusList(bool onPost)
        {
            if (onPost)
            {
                return Line2ManDetailStatus.Line2ManDetailStatus_On + "," + Line2ManDetailStatus.Line2ManDetailStatus_AutoOn;
            }
            else
            {
                return Line2ManDetailStatus.Line2ManDetailStatus_Off + "," + Line2ManDetailStatus.Line2ManDetailStatus_AutoOff;
            }
        }

        public Messages CheckBeforeGoOnPost(string resCode, List<string> userCodeList)
        {
            Messages returnValue = new Messages();

            //检查当前产线是否在列表中
            if (returnValue.IsSuccess())
            {
                if (!IsPerformanceCollectLine(resCode))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_NotOnPostLine"));
                }
            }

            //检查产线是否处于暂停状态
            if (returnValue.IsSuccess())
            {
                if (IsLineOfResPaused(resCode))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_LineIsPaused"));
                }
            }

            //检查用户是否存在
            UserFacade userFacade = new UserFacade(this.DataProvider);
            if (returnValue.IsSuccess())
            {
                foreach (string userCode in userCodeList)
                {
                    User user = (User)userFacade.GetUser(userCode);
                    if (user == null)
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_User_Not_Exist : " + userCode));
                    }
                }
            }

            //检查当前UserCode可否上岗
            if (returnValue.IsSuccess())
            {
                foreach (string userCode in userCodeList)
                {
                    if (IsOnPost(userCode))
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_AlreadyOnPost : " + userCode));
                    }
                }
            }

            //检查当前UserCode所在产线是否处于暂停状态
            if (returnValue.IsSuccess())
            {
                foreach (string userCode in userCodeList)
                {
                    string ssCode = string.Empty;
                    if (IsLineOfUserPaused(userCode, out ssCode))
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_LineOfUserIsPaused $User=" + userCode + " $StepSequence=" + ssCode));
                        continue;
                    }
                }
            }

            return returnValue;
        }

        public Messages CheckBeforeGoOffPost(List<string> userCodeList)
        {
            Messages returnValue = new Messages();

            //检查产线是否处于暂停状态，检查当前UserCode可否离岗
            if (returnValue.IsSuccess())
            {
                foreach (string userCode in userCodeList)
                {
                    if (IsOffPost(userCode))
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_NotOnPost : " + userCode));
                        continue;
                    }

                    string ssCode = string.Empty;
                    if (IsLineOfUserPaused(userCode, out ssCode))
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_LineOfUserIsPaused $User=" + userCode + " $StepSequence=" + ssCode));
                        continue;
                    }
                }
            }

            return returnValue;
        }

        public Messages CheckBeforePauseUserOnPost(string ssCode, List<string> userCodeList)
        {
            Messages returnValue = new Messages();

            if (returnValue.IsSuccess())
            {
                if (IsLinePaused(ssCode))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_LineIsPaused"));
                }
            }

            if (returnValue.IsSuccess())
            {
                if (userCodeList.Count <= 0)
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_NoUserToPause"));
                }
            }

            return returnValue;
        }

        public Messages CheckBeforeCancelPauseUserOnPost(string ssCode, List<string> userCodeList)
        {
            Messages returnValue = new Messages();

            if (returnValue.IsSuccess())
            {
                if (!IsLinePaused(ssCode))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_LineIsNotPaused"));
                }
            }

            if (returnValue.IsSuccess())
            {
                if (userCodeList.Count <= 0)
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_NoUserToCancelPause"));
                }
            }

            return returnValue;
        }

        public Messages GoOnPost(string resCode, List<string> userCodeList, string maintainUserCode)
        {
            Messages returnValue = new Messages();

            if (returnValue.IsSuccess())
            {
                returnValue.AddMessages(CheckBeforeGoOnPost(resCode, userCodeList));
            }

            DBDateTime nowDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DBDateTime lastSecondDateTime = new DBDateTime(nowDateTime.DateTime.AddSeconds(-1));
            OnOffPostEnvirenment nowEnv = new OnOffPostEnvirenment();
            OnOffPostEnvirenment lastSecondEnv = new OnOffPostEnvirenment();

            if (returnValue.IsSuccess())
            {
                if (!nowEnv.Init(this.DataProvider, resCode, nowDateTime))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_CannotGetEnvironmentInfo"));
                }
            }
            if (returnValue.IsSuccess())
            {
                if (!lastSecondEnv.Init(this.DataProvider, resCode, lastSecondDateTime))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_CannotGetEnvironmentInfo"));
                }
            }

            if (returnValue.IsSuccess())
            {
                try
                {
                    this.DataProvider.BeginTransaction();

                    foreach (string userCode in userCodeList)
                    {
                        Line2ManDetail newLine2ManDetail = CreateNewLine2ManDetail();
                        newLine2ManDetail.UserCode = userCode;
                        newLine2ManDetail.OPCode = nowEnv.Operation.OPCode;
                        newLine2ManDetail.ResourceCode = nowEnv.ResCode;
                        newLine2ManDetail.SSCode = nowEnv.StepSequence.StepSequenceCode;
                        newLine2ManDetail.ShiftDate = nowEnv.ShiftDate;
                        newLine2ManDetail.ShiftCode = nowEnv.Shift.ShiftCode;
                        newLine2ManDetail.OnDate = nowEnv.DBDateTime.DBDate;
                        newLine2ManDetail.OnTime = nowEnv.DBDateTime.DBTime;
                        newLine2ManDetail.OffDate = nowEnv.GetShiftEndDate();
                        newLine2ManDetail.OffTime = nowEnv.Shift.ShiftEndTime;
                        newLine2ManDetail.Duration = FormatHelper.GetSpanSeconds(newLine2ManDetail.OnDate, newLine2ManDetail.OnTime, newLine2ManDetail.OffDate, newLine2ManDetail.OffTime);
                        newLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_On;
                        newLine2ManDetail.MOCode = " ";
                        newLine2ManDetail.ManActQty = 0;
                        newLine2ManDetail.MaintainUser = maintainUserCode;
                        AddLine2ManDetail(newLine2ManDetail);
                    }

                    //tblproddetail的处理
                    DealProduceDetail(lastSecondEnv, nowEnv, maintainUserCode);

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    returnValue.Add(new UserControl.Message(ex));
                }
            }

            if (returnValue.IsSuccess())
            {
                returnValue.Add(new UserControl.Message(MessageType.Success, "$Message_GoOnPostSuccessfully"));
            }

            return returnValue;
        }

        public Messages GoOffPost(List<string> userCodeList, string maintainUserCode)
        {
            Messages returnValue = new Messages();

            if (returnValue.IsSuccess())
            {
                returnValue.AddMessages(CheckBeforeGoOffPost(userCodeList));
            }

            DBDateTime nowDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DBDateTime nextSecondDateTime = new DBDateTime(nowDateTime.DateTime.AddSeconds(1));
            OnOffPostEnvirenment nowEnv = new OnOffPostEnvirenment();
            OnOffPostEnvirenment nextSecondEnv = new OnOffPostEnvirenment();

            if (returnValue.IsSuccess())
            {
                try
                {
                    Dictionary<string, List<Line2ManDetail>> line2ManDetailDictionary = new Dictionary<string, List<Line2ManDetail>>();

                    this.DataProvider.BeginTransaction();

                    foreach (string userCode in userCodeList)
                    {
                        Line2ManDetail oldLine2ManDetail = (Line2ManDetail)GetUserCurrentLine2ManDetail(userCode);

                        if (line2ManDetailDictionary.ContainsKey(oldLine2ManDetail.SSCode))
                        {
                            line2ManDetailDictionary[oldLine2ManDetail.SSCode].Add(oldLine2ManDetail);
                        }
                        else
                        {
                            List<Line2ManDetail> line2ManDetailList = new List<Line2ManDetail>();
                            line2ManDetailList.Add(oldLine2ManDetail);
                            line2ManDetailDictionary.Add(oldLine2ManDetail.SSCode, line2ManDetailList);
                        }

                        if (!nowEnv.InitWithoutResAndOP(this.DataProvider, oldLine2ManDetail.SSCode, nowDateTime))
                        {
                            throw new Exception("$Error_CannotGetEnvironmentInfo");
                        }

                        if (oldLine2ManDetail.ShiftDate == nowEnv.ShiftDate && string.Compare(oldLine2ManDetail.ShiftCode, nowEnv.Shift.ShiftCode, true) == 0)
                        {
                            //一般的离岗的逻辑
                            oldLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_Off;
                            oldLine2ManDetail.OffDate = nowEnv.DBDateTime.DBDate;
                            oldLine2ManDetail.OffTime = nowEnv.DBDateTime.DBTime;
                            oldLine2ManDetail.Duration = FormatHelper.GetSpanSeconds(oldLine2ManDetail.OnDate, oldLine2ManDetail.OnTime, oldLine2ManDetail.OffDate, oldLine2ManDetail.OffTime);
                            oldLine2ManDetail.MaintainUser = maintainUserCode;
                            UpdateLine2ManDetail(oldLine2ManDetail);
                        }
                        else
                        {
                            //跨班次的离岗的逻辑
                            oldLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_AutoOff;
                            oldLine2ManDetail.MaintainUser = maintainUserCode;
                            UpdateLine2ManDetail(oldLine2ManDetail);

                            Line2ManDetail newLine2ManDetail = CreateNewLine2ManDetail();
                            newLine2ManDetail.UserCode = userCode;
                            newLine2ManDetail.OPCode = oldLine2ManDetail.OPCode;
                            newLine2ManDetail.ResourceCode = oldLine2ManDetail.ResourceCode;
                            newLine2ManDetail.SSCode = oldLine2ManDetail.SSCode;
                            newLine2ManDetail.ShiftDate = nowEnv.ShiftDate;
                            newLine2ManDetail.ShiftCode = nowEnv.Shift.ShiftCode;
                            newLine2ManDetail.OnDate = nowEnv.GetShiftBeginDate();
                            newLine2ManDetail.OnTime = nowEnv.Shift.ShiftBeginTime;
                            newLine2ManDetail.OffDate = nowEnv.DBDateTime.DBDate;
                            newLine2ManDetail.OffTime = nowEnv.DBDateTime.DBTime;
                            newLine2ManDetail.Duration = FormatHelper.GetSpanSeconds(newLine2ManDetail.OnDate, newLine2ManDetail.OnTime, newLine2ManDetail.OffDate, newLine2ManDetail.OffTime);
                            newLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_Off;
                            newLine2ManDetail.MOCode = oldLine2ManDetail.MOCode;
                            newLine2ManDetail.ManActQty = 0;
                            newLine2ManDetail.MaintainUser = maintainUserCode;
                            AddLine2ManDetail(newLine2ManDetail);
                        }
                    }

                    //tblproddetail的处理
                    foreach (string ssCode in line2ManDetailDictionary.Keys)
                    {
                        if (!nextSecondEnv.InitWithoutResAndOP(this.DataProvider, ssCode, nextSecondDateTime))
                        {
                            throw new Exception("$Error_CannotGetEnvironmentInfo");
                        }

                        DealProduceDetail(nowEnv, nextSecondEnv, maintainUserCode);
                    }

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    returnValue.Add(new UserControl.Message(ex));
                }
            }

            if (returnValue.IsSuccess())
            {
                returnValue.Add(new UserControl.Message(MessageType.Success, "$Message_GoOffPostSuccessfully"));
            }

            return returnValue;
        }

        public Messages GoOffPost(string ssCode, string maintainUserCode)
        {
            Messages returnValue = new Messages();

            if (returnValue.IsSuccess())
            {
                if (IsLinePaused(ssCode))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_LineIsPaused"));
                }
            }

            if (returnValue.IsSuccess())
            {
                returnValue.AddMessages(GoOffPost(GetUserList(ssCode, GetLine2ManDetailStatusList(true)), maintainUserCode));
            }

            return returnValue;
        }

        public Messages PauseUserOnPost(string ssCode, string maintainUserCode)
        {
            Messages returnValue = new Messages();

            List<string> userCodeList = GetUserList(ssCode, GetLine2ManDetailStatusList(true));

            if (returnValue.IsSuccess())
            {
                returnValue.AddMessages(CheckBeforePauseUserOnPost(ssCode, userCodeList));
            }

            DBDateTime nowDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DBDateTime nextSecondDateTime = new DBDateTime(nowDateTime.DateTime.AddSeconds(1));
            OnOffPostEnvirenment nowEnv = new OnOffPostEnvirenment();
            OnOffPostEnvirenment nextSecondEnv = new OnOffPostEnvirenment();

            if (returnValue.IsSuccess())
            {
                if (!nowEnv.InitWithoutResAndOP(this.DataProvider, ssCode, nowDateTime))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_CannotGetEnvironmentInfo"));
                }
            }
            if (returnValue.IsSuccess())
            {
                if (!nextSecondEnv.InitWithoutResAndOP(this.DataProvider, ssCode, nextSecondDateTime))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_CannotGetEnvironmentInfo"));
                }
            }

            if (returnValue.IsSuccess())
            {
                try
                {
                    int pausedManCount = 0;

                    this.DataProvider.BeginTransaction();

                    foreach (string userCode in userCodeList)
                    {
                        Line2ManDetail oldLine2ManDetail = (Line2ManDetail)GetUserCurrentLine2ManDetail(userCode);

                        if (oldLine2ManDetail == null
                            || (oldLine2ManDetail.Status != Line2ManDetailStatus.Line2ManDetailStatus_On
                                && oldLine2ManDetail.Status != Line2ManDetailStatus.Line2ManDetailStatus_AutoOn))
                        {
                            continue;
                        }

                        pausedManCount++;

                        if (oldLine2ManDetail.ShiftDate == nowEnv.ShiftDate && string.Compare(oldLine2ManDetail.ShiftCode, nowEnv.Shift.ShiftCode, true) == 0)
                        {
                            //一般的暂停的逻辑
                            oldLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_Pause;
                            oldLine2ManDetail.OffDate = nowEnv.DBDateTime.DBDate;
                            oldLine2ManDetail.OffTime = nowEnv.DBDateTime.DBTime;
                            oldLine2ManDetail.Duration = FormatHelper.GetSpanSeconds(oldLine2ManDetail.OnDate, oldLine2ManDetail.OnTime, oldLine2ManDetail.OffDate, oldLine2ManDetail.OffTime);
                            oldLine2ManDetail.MaintainUser = maintainUserCode;
                            UpdateLine2ManDetail(oldLine2ManDetail);
                        }
                        else
                        {
                            //跨班次的暂停的逻辑
                            oldLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_AutoOff;
                            oldLine2ManDetail.MaintainUser = maintainUserCode;
                            UpdateLine2ManDetail(oldLine2ManDetail);

                            Line2ManDetail newLine2ManDetail = CreateNewLine2ManDetail();
                            newLine2ManDetail.UserCode = userCode;
                            newLine2ManDetail.OPCode = oldLine2ManDetail.OPCode;
                            newLine2ManDetail.ResourceCode = oldLine2ManDetail.ResourceCode;
                            newLine2ManDetail.SSCode = oldLine2ManDetail.SSCode;
                            newLine2ManDetail.ShiftDate = nowEnv.ShiftDate;
                            newLine2ManDetail.ShiftCode = nowEnv.Shift.ShiftCode;
                            newLine2ManDetail.OnDate = nowEnv.GetShiftBeginDate();
                            newLine2ManDetail.OnTime = nowEnv.Shift.ShiftBeginTime;
                            newLine2ManDetail.OffDate = nowEnv.DBDateTime.DBDate;
                            newLine2ManDetail.OffTime = nowEnv.DBDateTime.DBTime;
                            newLine2ManDetail.Duration = FormatHelper.GetSpanSeconds(newLine2ManDetail.OnDate, newLine2ManDetail.OnTime, newLine2ManDetail.OffDate, newLine2ManDetail.OffTime);
                            newLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_Pause;
                            newLine2ManDetail.MOCode = oldLine2ManDetail.MOCode;
                            newLine2ManDetail.ManActQty = 0;
                            newLine2ManDetail.MaintainUser = maintainUserCode;
                            AddLine2ManDetail(newLine2ManDetail);
                        }
                    }

                    //tblproddetail的处理
                    DealProduceDetail(nowEnv, nextSecondEnv, maintainUserCode);

                    //tbllinepause的处理
                    LinePause linePause = CreateNewLinePause();
                    linePause.SSCode = ssCode;
                    linePause.ManCount = pausedManCount;
                    linePause.BeginDate = nextSecondEnv.DBDateTime.DBDate;
                    linePause.BeginTime = nextSecondEnv.DBDateTime.DBTime;
                    linePause.EndDate = 0;
                    linePause.EndTime = 0;
                    linePause.Duration = 0;
                    linePause.MaintainUser = maintainUserCode;
                    AddLinePause(linePause);

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    returnValue.Add(new UserControl.Message(ex));
                }
            }

            if (returnValue.IsSuccess())
            {
                returnValue.Add(new UserControl.Message(MessageType.Success, "$Message_PauseUserOnPostSuccessfully"));
            }

            return returnValue;
        }

        public Messages CancelPauseUserOnPost(string ssCode, string maintainUserCode)
        {
            Messages returnValue = new Messages();

            List<string> userCodeList = GetUserList(ssCode, Line2ManDetailStatus.Line2ManDetailStatus_Pause);

            if (returnValue.IsSuccess())
            {
                returnValue.AddMessages(CheckBeforeCancelPauseUserOnPost(ssCode, userCodeList));
            }

            DBDateTime nowDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DBDateTime lastSecondDateTime = new DBDateTime(nowDateTime.DateTime.AddSeconds(-1));
            OnOffPostEnvirenment nowEnv = new OnOffPostEnvirenment();
            OnOffPostEnvirenment lastSecondEnv = new OnOffPostEnvirenment();

            if (returnValue.IsSuccess())
            {
                if (!nowEnv.InitWithoutResAndOP(this.DataProvider, ssCode, nowDateTime))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_CannotGetEnvironmentInfo"));
                }
            }
            if (returnValue.IsSuccess())
            {
                if (!lastSecondEnv.InitWithoutResAndOP(this.DataProvider, ssCode, lastSecondDateTime))
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_CannotGetEnvironmentInfo"));
                }
            }

            if (returnValue.IsSuccess())
            {
                try
                {
                    this.DataProvider.BeginTransaction();

                    foreach (string userCode in userCodeList)
                    {
                        Line2ManDetail oldLine2ManDetail = (Line2ManDetail)GetUserCurrentLine2ManDetail(userCode);

                        if (oldLine2ManDetail == null || oldLine2ManDetail.Status != Line2ManDetailStatus.Line2ManDetailStatus_Pause)
                        {
                            continue;
                        }

                        Line2ManDetail newLine2ManDetail = CreateNewLine2ManDetail();
                        newLine2ManDetail.UserCode = userCode;
                        newLine2ManDetail.OPCode = oldLine2ManDetail.OPCode;
                        newLine2ManDetail.ResourceCode = oldLine2ManDetail.ResourceCode;
                        newLine2ManDetail.SSCode = oldLine2ManDetail.SSCode;
                        newLine2ManDetail.ShiftDate = nowEnv.ShiftDate;
                        newLine2ManDetail.ShiftCode = nowEnv.Shift.ShiftCode;
                        newLine2ManDetail.OnDate = nowEnv.DBDateTime.DBDate;
                        newLine2ManDetail.OnTime = nowEnv.DBDateTime.DBTime;
                        newLine2ManDetail.OffDate = nowEnv.GetShiftEndDate();
                        newLine2ManDetail.OffTime = nowEnv.Shift.ShiftEndTime;
                        newLine2ManDetail.Duration = FormatHelper.GetSpanSeconds(newLine2ManDetail.OnDate, newLine2ManDetail.OnTime, newLine2ManDetail.OffDate, newLine2ManDetail.OffTime);
                        newLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_On;
                        newLine2ManDetail.MOCode = " ";
                        newLine2ManDetail.ManActQty = 0;
                        newLine2ManDetail.MaintainUser = maintainUserCode;
                        AddLine2ManDetail(newLine2ManDetail);
                    }

                    //tblproddetail的处理
                    DealProduceDetail(lastSecondEnv, nowEnv, maintainUserCode);

                    //tbllinepause的处理
                    LinePause linePause = (LinePause)GetLatestLinePause(ssCode);
                    if (linePause != null && linePause.EndDate == 0 && linePause.EndTime == 0)
                    {
                        linePause.EndDate = lastSecondEnv.DBDateTime.DBDate;
                        linePause.EndTime = lastSecondEnv.DBDateTime.DBTime;
                        linePause.Duration = FormatHelper.GetSpanSeconds(linePause.BeginDate, linePause.BeginTime, linePause.EndDate, linePause.EndTime);
                        linePause.MaintainUser = maintainUserCode;
                        UpdateLinePause(linePause);
                    }

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    returnValue.Add(new UserControl.Message(ex));
                }
            }

            if (returnValue.IsSuccess())
            {
                returnValue.Add(new UserControl.Message(MessageType.Success, "$Message_CancelPauseUserOnPostSuccessfully"));
            }

            return returnValue;
        }

        #endregion

        #endregion

        #region IndirectManCount

        public IndirectManCount CreateNewIndirectManCount()
        {
            return new IndirectManCount();
        }

        public void AddIndirectManCount(IndirectManCount indirectManCount)
        {
            this._helper.AddDomainObject(indirectManCount);
        }

        public void DeleteIndirectManCount(IndirectManCount indirectManCount)
        {
            this._helper.DeleteDomainObject(indirectManCount);
        }

        public void DeleteIndirectManCount(IndirectManCount[] indirectManCount)
        {
            this._helper.DeleteDomainObject(indirectManCount);
        }

        public void UpdateIndirectManCount(IndirectManCount indirectManCount)
        {
            this._helper.UpdateDomainObject(indirectManCount);
        }

        public object GetIndirectManCount(int shiftDate, string shiftCode, string crewCode, string factoryCode, string firstClass)
        {
            return this.DataProvider.CustomSearch(typeof(IndirectManCount), new object[] { shiftDate, shiftCode, crewCode, factoryCode, firstClass });
        }

        public object[] QueryIndirectManCount(int shiftDate, string shiftCode, string crewCode, string factoryCode, string firstClass, int inclusive, int exclusive)
        {
            string sql = GetIndirectManCountSql(shiftDate, shiftCode, crewCode, factoryCode, firstClass);
            return this.DataProvider.CustomQuery(typeof(IndirectManCountWithMessage), new PagerCondition(sql, inclusive, exclusive));
        }

        public int GetIndirectManCountCount(int shiftDate, string shiftCode, string crewCode, string factoryCode, string firstClass)
        {
            string sql = " SELECT COUNT(*) FROM (" + GetIndirectManCountSql(shiftDate, shiftCode, crewCode, factoryCode, firstClass) + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetIndirectManCountSql(int shiftDate, string shiftCode, string crewCode, string factoryCode, string firstClass)
        {
            string sql = "SELECT A.SHIFTDATE,A.SHIFTCODE,B.SHIFTDESC,A.CREWCODE,C.CREWDESC,A.FACCODE,D.FACDESC,";
            sql += " A.FIRSTCLASS,A.MANCOUNT,A.DURATION,A.MUSER || '-' || E.USERNAME AS MUSER,A.MDATE,A.MTIME";
            sql += " FROM TBLINDIRECTMANCOUNT A  LEFT JOIN TBLSHIFT B ON A.SHIFTCODE = B.SHIFTCODE";
            sql += "  LEFT JOIN TBLCREW C ON A.CREWCODE = C.CREWCODE  LEFT JOIN TBLFACTORY D ON A.FACCODE = D.FACCODE";
            sql += " LEFT JOIN TBLUSER E ON A.MUSER = E.USERCODE WHERE 1 = 1";
            if (shiftDate > 0)
            {
                sql += " AND A.SHIFTDATE = " + shiftDate + "";
            }

            if (shiftCode.Trim() != string.Empty)
            {
                if (shiftCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = shiftCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    shiftCode = string.Join(",", list);
                    sql += " AND  A.SHIFTCODE in (" + shiftCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.SHIFTCODE = '" + shiftCode.Trim().ToUpper() + "'";
                }
            }

            if (crewCode.Trim() != string.Empty)
            {
                if (crewCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = crewCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    crewCode = string.Join(",", list);
                    sql += " AND  A.CREWCODE in (" + crewCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.CREWCODE = '" + crewCode.Trim().ToUpper() + "'";
                }
            }

            if (factoryCode.Trim() != string.Empty)
            {
                if (factoryCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = factoryCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    factoryCode = string.Join(",", list);
                    sql += " AND  A.FACCODE in (" + factoryCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.FACCODE = '" + factoryCode.Trim().ToUpper() + "'";
                }
            }

            if (firstClass.Trim() != string.Empty)
            {
                sql += " AND A.FIRSTCLASS = '" + firstClass.Trim().ToUpper() + "'";
            }

            sql += "ORDER BY A.SHIFTDATE, A.SHIFTCODE, A.CREWCODE, A.FACCODE, A.FIRSTCLASS";

            return sql;
        }

        #endregion

        #region ProduceDetail

        public ProduceDetail CreateNewProduceDetail()
        {
            return new ProduceDetail();
        }

        public void AddProduceDetail(ProduceDetail produceDetail)
        {
            this._helper.AddDomainObject(produceDetail);
        }

        public void DeleteProduceDetail(ProduceDetail produceDetail)
        {
            this._helper.DeleteDomainObject(produceDetail);
        }

        public void UpdateProduceDetail(ProduceDetail produceDetail)
        {
            this._helper.UpdateDomainObject(produceDetail);
        }

        public object GetProduceDetail(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(ProduceDetail), new object[] { serial });
        }

        public object[] QueryProduceDetail(string ssCode, int shiftDay, string shiftCode, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql += string.Format("SELECT {0} ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ProduceDetail)));
            sql += "FROM tblproddetail ";
            sql += QueryProduceDetailWhereCondition(ssCode, shiftDay, shiftCode);

            return this.DataProvider.CustomQuery(typeof(ProduceDetail), new PagerCondition(sql, "serial", inclusive, exclusive));
        }

        public int QueryProduceDetailCount(string ssCode, int shiftDay, string shiftCode)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += "FROM tblproddetail ";
            sql += QueryProduceDetailWhereCondition(ssCode, shiftDay, shiftCode);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public string QueryProduceDetailWhereCondition(string ssCode, int shiftDay, string shiftCode)
        {
            string sql = string.Empty;
            sql += "WHERE 1 = 1 ";
            if (ssCode.Trim().Length > 0)
            {
                sql += string.Format("AND sscode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ssCode)));
            }
            if (shiftDay > 0)
            {
                sql += string.Format("AND shiftdate = {0} ", shiftDay.ToString());
            }
            if (shiftCode.Trim().Length > 0)
            {
                sql += string.Format("AND shiftcode = '{0}' ", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(shiftCode)));
            }

            return sql;
        }

        public object GetLatestProduceDetail(string ssCode, int shiftDay, string shiftCode, bool lockProduceDetail)
        {
            ProduceDetail prodDetail = null;

            string sqlForMax = "SELECT MAX(serial) AS serial FROM tblproddetail ";
            sqlForMax += QueryProduceDetailWhereCondition(ssCode, shiftDay, shiftCode);

            string sql = "SELECT {0} FROM tblproddetail WHERE serial = (" + sqlForMax + ") ";
            if (lockProduceDetail)
            {
                sql += "FOR UPDATE ";
            }
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ProduceDetail)));

            object[] list = this.DataProvider.CustomQuery(typeof(ProduceDetail), new SQLCondition(sql));
            if (list != null && list.Length > 0)
            {
                prodDetail = (ProduceDetail)list[0];
            }

            return prodDetail;
        }

        private void DealProduceDetail(OnOffPostEnvirenment lastSecondEnv, OnOffPostEnvirenment nowEnv, string maintainUserCode)
        {
            ProduceDetail oldProduceDetail = (ProduceDetail)GetLatestProduceDetail(nowEnv.StepSequence.StepSequenceCode, -1, string.Empty, true);
            if (oldProduceDetail != null)
            {
                //处理旧的ProduceDetail的时间和状态
                if (oldProduceDetail.ShiftDate == lastSecondEnv.ShiftDate && oldProduceDetail.ShiftCode == lastSecondEnv.Shift.ShiftCode)
                {
                    if (oldProduceDetail.Status == ProduceDetailStatus.ProduceDetailStatus_Open)
                    {
                        oldProduceDetail.EndDate = lastSecondEnv.DBDateTime.DBDate;
                        oldProduceDetail.EndTime = lastSecondEnv.DBDateTime.DBTime;
                        oldProduceDetail.Duration = FormatHelper.GetSpanSeconds(oldProduceDetail.BeginDate, oldProduceDetail.BeginTime, oldProduceDetail.EndDate, oldProduceDetail.EndTime);
                        oldProduceDetail.Status = ProduceDetailStatus.ProduceDetailStatus_Close;
                        oldProduceDetail.MaintainUser = maintainUserCode;
                        UpdateProduceDetail(oldProduceDetail);
                    }

                    //添加新的ProduceDetail
                    int manCount = QueryLine2ManDetailCount(string.Empty, nowEnv.StepSequence.StepSequenceCode, string.Empty, string.Empty, 0, string.Empty, GetLine2ManDetailStatusList(true));
                    if (manCount > 0)
                    {
                        ProduceDetail newProduceDetail = CreateNewProduceDetail();
                        newProduceDetail.SSCode = nowEnv.StepSequence.StepSequenceCode;
                        newProduceDetail.ShiftDate = nowEnv.ShiftDate;
                        newProduceDetail.ShiftCode = nowEnv.Shift.ShiftCode;
                        newProduceDetail.BeginDate = nowEnv.DBDateTime.DBDate;
                        newProduceDetail.BeginTime = nowEnv.DBDateTime.DBTime;
                        newProduceDetail.EndDate = nowEnv.GetShiftEndDate();
                        newProduceDetail.EndTime = nowEnv.Shift.ShiftEndTime;
                        newProduceDetail.Duration = FormatHelper.GetSpanSeconds(newProduceDetail.BeginDate, newProduceDetail.BeginTime, newProduceDetail.EndDate, newProduceDetail.EndTime);
                        newProduceDetail.Status = ProduceDetailStatus.ProduceDetailStatus_Open;
                        newProduceDetail.ManCount = manCount;
                        newProduceDetail.MOCode = oldProduceDetail.MOCode;
                        newProduceDetail.MaintainUser = maintainUserCode;
                        AddProduceDetail(newProduceDetail);
                    }
                }
                else
                {
                    if (oldProduceDetail.Status == ProduceDetailStatus.ProduceDetailStatus_Open)
                    {
                        oldProduceDetail.Status = ProduceDetailStatus.ProduceDetailStatus_Close;
                        oldProduceDetail.MaintainUser = maintainUserCode;
                        UpdateProduceDetail(oldProduceDetail);
                    }
                }
            }
        }

        #endregion

        #region LinePause

        public LinePause CreateNewLinePause()
        {
            return new LinePause();
        }

        public void AddLinePause(LinePause linePause)
        {
            this._helper.AddDomainObject(linePause);
        }

        public void DeleteLinePause(LinePause linePause)
        {
            this._helper.DeleteDomainObject(linePause);
        }

        public void UpdateLinePause(LinePause linePause)
        {
            this._helper.UpdateDomainObject(linePause);
        }

        public object GetLinePause(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(LinePause), new object[] { serial });
        }

        public object GetLatestLinePause(string ssCode)
        {
            ssCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ssCode));

            string sql = string.Empty;
            sql += "SELECT {0} ";
            sql += "FROM ( ";
            sql += "    SELECT * ";
            sql += "    FROM tbllinepause ";
            sql += "    WHERE sscode = '{1}' ";
            sql += "    ORDER BY serial DESC ";
            sql += ") tbllinepause ";
            sql += "WHERE ROWNUM <= 1 ";

            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(LinePause)), ssCode);

            object[] list = this.DataProvider.CustomQuery(typeof(LinePause), new SQLCondition(sql));
            if (list == null || list.Length <= 0)
            {
                return null;
            }
            else
            {
                return list[0];
            }
        }

        #endregion

        #region ExceptionCode

        public ExceptionCode CreateNewExceptionCode()
        {
            return new ExceptionCode();
        }

        public void AddExceptionCode(ExceptionCode exceptionCode)
        {
            this._helper.AddDomainObject(exceptionCode);
        }

        public void DeleteExceptionCode(ExceptionCode exceptionCode)
        {
            this._helper.DeleteDomainObject(exceptionCode);
        }

        public void DeleteExceptionCode(ExceptionCode[] exceptionCode)
        {
            this._helper.DeleteDomainObject(exceptionCode);
        }

        public void UpdateExceptionCode(ExceptionCode exceptionCode)
        {
            this._helper.UpdateDomainObject(exceptionCode);
        }

        public object GetExceptionCode(string exceptionCode)
        {
            return this.DataProvider.CustomSearch(typeof(ExceptionCode), new object[] { exceptionCode });
        }

        public object[] QueryExceptionCode(string exceptionCode, string exceptionDesc, string exceptionType, string exceptionFlag, int inclusive, int exclusive)
        {
            string sql = GetExceptionCodeSql(exceptionCode, exceptionDesc, exceptionType, exceptionFlag);
            return this.DataProvider.CustomQuery(typeof(ExceptionCode), new PagerCondition(sql, inclusive, exclusive));
        }

        public int GetExceptionCodeCount(string exceptionCode, string exceptionDesc, string exceptionType, string exceptionFlag)
        {
            string sql = " SELECT COUNT(*) FROM (" + GetExceptionCodeSql(exceptionCode, exceptionDesc, exceptionType, exceptionFlag) + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetExceptionCodeSql(string exceptionCode, string exceptionDesc, string exceptionType, string exceptionFlag)
        {
            string sql = "SELECT EXCEPTIONCODE,EXCEPTIONNAME,EXCEPTIONDESC,EXCEPTIONTYPE,EXCEPTIONFLAG,a.MUSER || '-' || b.username AS MUSER,a.MDATE,a.MTIME";
            sql += "  FROM TBLEXCEPTIONCODE a  LEFT JOIN TBLUSER b ON a.MUSER = b.USERCODE WHERE 1 = 1";

            if (exceptionCode.Trim() != string.Empty)
            {
                sql += " AND EXCEPTIONCODE LIKE '%" + exceptionCode.Trim().ToUpper() + "%'";
            }

            if (exceptionDesc.Trim() != string.Empty)
            {
                sql += " AND EXCEPTIONDESC LIKE  '%" + exceptionDesc.Trim().ToUpper() + "%'";
            }

            if (exceptionType.Trim() != string.Empty)
            {
                sql += " AND EXCEPTIONTYPE = '" + exceptionType.Trim().ToUpper() + "'";
            }

            if (exceptionFlag.Trim() != string.Empty)
            {
                sql += " AND EXCEPTIONFLAG = '" + exceptionFlag.Trim().ToUpper() + "'";
            }

            sql += "   ORDER BY EXCEPTIONCODE, EXCEPTIONTYPE, EXCEPTIONFLAG";

            return sql;
        }

        public object[] QueryExceptionTypeFromSystem()
        {
            string sql = string.Format(
               "select {0} from TBLSYSPARAM  where 1=1 and PARAMGROUPCODE='EXCEPTIONTYPE'  order by EATTRIBUTE1,PARAMALIAS",
               DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.BaseSetting.Parameter)));
            return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));
        }
        #endregion

        #region ExceptionEvent

        public ExceptionEvent CreateNewExceptionEvent()
        {
            return new ExceptionEvent();
        }

        public void AddExceptionEvent(ExceptionEvent exceptionEvent)
        {
            this._helper.AddDomainObject(exceptionEvent);
        }

        public void DeleteExceptionEvent(ExceptionEvent exceptionEvent)
        {
            this._helper.DeleteDomainObject(exceptionEvent);
        }

        public void DeleteExceptionEvent(ExceptionEvent[] exceptionEvent)
        {
            this._helper.DeleteDomainObject(exceptionEvent);
        }

        public void UpdateExceptionEvent(ExceptionEvent exceptionEvent)
        {
            this._helper.UpdateDomainObject(exceptionEvent);
        }

        public object GetExceptionEvent(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(ExceptionEvent), new object[] { serial });
        }

        public object[] QueryExceptionEvent(string ssCode, int shiftDay, string shiftCode, string itemCode, string exceptionCode, int inclusive, int exclusive)
        {
            string sql = GetExceptionEventSql(ssCode, shiftDay, shiftCode, itemCode, exceptionCode);
            return this.DataProvider.CustomQuery(typeof(ExceptionEvent), new PagerCondition(sql, inclusive, exclusive));
        }

        public object[] QueryExceptionEvent(string ssCode, int shiftDay, string shiftCode, string itemCode, string exceptionCode)
        {
            string sql = GetExceptionEventSql(ssCode, shiftDay, shiftCode, itemCode, exceptionCode);
            return this.DataProvider.CustomQuery(typeof(ExceptionEvent), new SQLCondition(sql));
        }

        public int GetExceptionEventCount(string ssCode, int shiftDay, string shiftCode, string itemCode, string exceptionCode)
        {
            string sql = " SELECT COUNT(*) FROM (" + GetExceptionEventSql(ssCode, shiftDay, shiftCode, itemCode, exceptionCode) + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetExceptionEventSql(string ssCode, int shiftDate, string shiftCode, string itemCode, string exceptionCode)
        {
            string sql = "SELECT A.SERIAL,A.SSCODE || '-' || B.SSDESC AS SSCODE,A.SHIFTDATE,A.SHIFTCODE || '-' || C.SHIFTDESC AS SHIFTCODE,";
            sql += " A.ITEMCODE || '-' || D.ITEMDESC AS ITEMCODE,A.BEGINTIME,A.ENDTIME,A.EXCEPTIONCODE || '-' || E.EXCEPTIONDESC AS EXCEPTIONCODE,";
            sql += " A.MEMO,A.COMFIRMMEMO,A.MUSER || '-' || F.USERNAME AS MUSER,A.MDATE,A.MTIME  FROM TBLEXCEPTION A";
            sql += " LEFT JOIN TBLSS B ON A.SSCODE = B.SSCODE  LEFT JOIN TBLSHIFT C ON A.SHIFTCODE = C.SHIFTCODE";
            sql += " LEFT JOIN TBLITEM D ON A.ITEMCODE = D.ITEMCODE  LEFT JOIN TBLEXCEPTIONCODE E ON A.EXCEPTIONCODE = E.EXCEPTIONCODE";
            sql += " LEFT JOIN TBLUSER F ON A.MUSER = F.USERCODE WHERE 1 = 1";

            if (ssCode.Trim() != string.Empty)
            {
                if (ssCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = ssCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    ssCode = string.Join(",", list);
                    sql += " AND  A.SSCODE in (" + ssCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.SSCODE like '" + ssCode.Trim().ToUpper() + "%'";
                }
            }

            if (shiftDate > 0)
            {
                sql += " AND A.SHIFTDATE = " + shiftDate + " ";
            }

            if (shiftCode.Trim() != string.Empty)
            {
                if (shiftCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = shiftCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    shiftCode = string.Join(",", list);
                    sql += " AND  A.SHIFTCODE in (" + shiftCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.SHIFTCODE like '" + shiftCode.Trim().ToUpper() + "%'";
                }
            }

            if (itemCode.Trim() != string.Empty)
            {
                if (itemCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = itemCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    itemCode = string.Join(",", list);
                    sql += " AND  A.ITEMCODE in (" + itemCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.ITEMCODE like '" + itemCode.Trim().ToUpper() + "%'";
                }
            }

            if (exceptionCode.Trim() != string.Empty)
            {
                if (exceptionCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = exceptionCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    exceptionCode = string.Join(",", list);
                    sql += " AND  A.EXCEPTIONCODE in (" + exceptionCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.EXCEPTIONCODE like '" + exceptionCode.Trim().ToUpper() + "%'";
                }
            }

            sql += " ORDER BY A.SSCODE,A.SHIFTDATE,A.SHIFTCODE,A.ITEMCODE,A.EXCEPTIONCODE";

            return sql;
        }


        #endregion

        #region LostManHourHead

        public LostManHourHead CreateNewLostManHourHead()
        {
            return new LostManHourHead();
        }

        public void AddLostManHourHead(LostManHourHead lostManHourHead)
        {
            this._helper.AddDomainObject(lostManHourHead);
        }

        public void DeleteLostManHourHead(LostManHourHead lostManHourHead)
        {
            this._helper.DeleteDomainObject(lostManHourHead);
        }

        public void UpdateLostManHourHead(LostManHourHead lostManHourHead)
        {
            this._helper.UpdateDomainObject(lostManHourHead);
        }

        public object GetLostManHourHead(string sSCode, int shiftDate, string shiftCode, string itemCode)
        {
            return this.DataProvider.CustomSearch(typeof(LostManHourHead), new object[] { sSCode, shiftDate, shiftCode, itemCode });
        }

        public object[] QueryLostManHourHead(string sSCode, int shiftDate, string shiftCode, string itemCode, int inclusive, int exclusive)
        {
            string sql = GetLostManHourHeadSql(sSCode, shiftDate, shiftCode, itemCode);
            return this.DataProvider.CustomQuery(typeof(LostManHourHeadWithMessage), new PagerCondition(sql, inclusive, exclusive));
        }

        public int GetLostManHourHeadCount(string sSCode, int shiftDate, string shiftCode, string itemCode)
        {
            string sql = " SELECT COUNT(*) FROM (" + GetLostManHourHeadSql(sSCode, shiftDate, shiftCode, itemCode) + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetLostManHourHeadSql(string sSCode, int shiftDate, string shiftCode, string itemCode)
        {
            string sql = " SELECT A.SSCODE,B.SSDESC,A.SHIFTDATE,A.SHIFTCODE,C.SHIFTDESC,A.ITEMCODE,";
            sql += "              D.ITEMDESC,A.ACTMANHOUR,A.ACTOUTPUT,A.ACQUIREMANHOUR,LOSTMANHOUR";
            sql += "       FROM TBLLOSTMANHOUR A";
            sql += "       LEFT JOIN TBLSS B ON A.SSCODE = B.SSCODE";
            sql += "       LEFT JOIN TBLSHIFT C ON A.SHIFTCODE = C.SHIFTCODE";
            sql += "       LEFT JOIN TBLITEM D ON A.ITEMCODE = D.ITEMCODE";
            sql += "       WHERE 1=1";

            if (sSCode.Trim() != string.Empty)
            {
                sql += " AND A.SSCODE like '%" + sSCode.Trim().ToUpper() + "%'";
            }

            if (shiftDate > 0)
            {
                sql += " AND A.SHIFTDATE = " + shiftDate + " ";
            }

            if (shiftCode.Trim() != string.Empty)
            {
                if (shiftCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = shiftCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    shiftCode = string.Join(",", list);
                    sql += " AND  A.shiftCode in (" + shiftCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.shiftCode like '%" + shiftCode.Trim().ToUpper() + "%'";
                }
            }

            if (itemCode.Trim() != string.Empty)
            {
                if (itemCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = itemCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    itemCode = string.Join(",", list);
                    sql += " AND  A.ITEMCODE in (" + itemCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.ITEMCODE like '%" + itemCode.Trim().ToUpper() + "%'";
                }
            }

            sql += " ORDER BY A.SHIFTDATE, A.SSCODE, A.SHIFTCODE, A.ITEMCODE";
            return sql;
        }

        #endregion

        #region LostManHourDetail

        public LostManHourDetail CreateNewLostManHourDetail()
        {
            return new LostManHourDetail();
        }

        public void AddLostManHourDetail(LostManHourDetail lostManHourDetail)
        {
            this._helper.AddDomainObject(lostManHourDetail);
        }

        public void DeleteLostManHourDetail(LostManHourDetail lostManHourDetail)
        {
            this._helper.DeleteDomainObject(lostManHourDetail);
        }

        public void DeleteLostManHourDetail(LostManHourDetail[] lostManHourDetail)
        {
            this._helper.DeleteDomainObject(lostManHourDetail);
        }

        public void UpdateLostManHourDetail(LostManHourDetail lostManHourDetail)
        {
            this._helper.UpdateDomainObject(lostManHourDetail);
        }

        public object GetLostManHourDetail(string sSCode, int shiftDate, string shiftCode, string itemCode, int seq)
        {
            return this.DataProvider.CustomSearch(typeof(LostManHourDetail), new object[] { sSCode, shiftDate, shiftCode, itemCode, seq });
        }

        public object[] QueryLostManHourDetail(string sSCode, int shiftDate, string shiftCode, string itemCode, int lostManHour, int inclusive, int exclusive)
        {
            string sql = GetLostManHourDetailSql(sSCode, shiftDate, shiftCode, itemCode, lostManHour, false);
            return this.DataProvider.CustomQuery(typeof(LostManHourDetailWithMessage), new PagerCondition(sql, inclusive, exclusive));
        }

        public object[] QueryLostManHourDetail(string sSCode, int shiftDate, string shiftCode, string itemCode)
        {
            string sql = GetLostManHourDetailSql(sSCode, shiftDate, shiftCode, itemCode, 0, true);
            return this.DataProvider.CustomQuery(typeof(LostManHourDetailWithMessage), new SQLCondition(sql));
        }
        public int GetLostManHourDetailcount(string sSCode, int shiftDate, string shiftCode, string itemCode, int lostManHour)
        {
            string sql = " SELECT COUNT(*) FROM (" + GetLostManHourDetailSql(sSCode, shiftDate, shiftCode, itemCode, lostManHour, false) + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetLostManHourDetailSql(string sSCode, int shiftDate, string shiftCode, string itemCode, int lostManHour, bool addCondition)
        {
            string sql = " SELECT A.SSCODE,A.SHIFTDATE,A.SHIFTCODE,A.ITEMCODE,A.SEQ,A.LOSTMANHOUR,A.EXCEPTIONCODE,";
            sql += " A.EXCEPTIONSERIAL,A.DUTYCODE || '-' || D.DUTYDESC AS DUTYCODE,A.MEMO,B.BEGINTIME,B.ENDTIME,B.MEMO AS EXCEPTIONMEMO,C.EXCEPTIONDESC";
            sql += " FROM TBLLOSTMANHOURDETAIL A  LEFT JOIN TBLEXCEPTION B ON ";
            if (addCondition)
            {
                sql += "  A.SSCODE = B.SSCODE  AND A.SHIFTDATE = B.SHIFTDATE   AND A.SHIFTCODE = B.SHIFTCODE";
                sql += "   AND A.ITEMCODE = B.ITEMCODE     AND A.EXCEPTIONCODE = B.EXCEPTIONCODE AND ";
            }

            sql += "  A.ExceptionSerial=B.Serial";
            sql += "  LEFT JOIN TBLEXCEPTIONCODE C ON A.EXCEPTIONCODE = C.EXCEPTIONCODE";
            sql += "  LEFT JOIN TBLDUTY D ON A.DUTYCODE = D.DUTYCODE";
            sql += " WHERE 1 = 1";
            if (sSCode.Trim() != string.Empty)
            {
                sql += " AND A.SSCODE like '%" + sSCode.Trim().ToUpper() + "%'";
            }

            if (shiftDate > 0)
            {
                sql += " AND A.SHIFTDATE = " + shiftDate + " ";
            }

            if (shiftCode.Trim() != string.Empty)
            {
                if (shiftCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = shiftCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    shiftCode = string.Join(",", list);
                    sql += " AND  A.shiftCode in (" + shiftCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.shiftCode like '%" + shiftCode.Trim().ToUpper() + "%'";
                }
            }

            if (itemCode.Trim() != string.Empty)
            {
                if (itemCode.Trim().IndexOf(",") >= 0)
                {
                    string[] list = itemCode.Trim().Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }
                    itemCode = string.Join(",", list);
                    sql += " AND  A.ITEMCODE in (" + itemCode.Trim().ToUpper() + ")";

                }
                else
                {
                    sql += " AND  A.ITEMCODE like '%" + itemCode.Trim().ToUpper() + "%'";
                }
            }

            if (lostManHour > 0)
            {
                sql += " AND A.LOSTMANHOUR = " + lostManHour + " ";
            }

            sql += "    ORDER BY a.lostmanhour,b.begintime,b.endtime";
            return sql;
        }

        public int GetLostManHourDetailSeq(string sSCode, int shiftDate, string shiftCode, string itemCode)
        {
            string sql = " SELECT MAX(SEQ) from (" + GetLostManHourDetailSql(sSCode, shiftDate, shiftCode, itemCode, 0, true) + ")";
            try
            {
                int maxSEQ = this.DataProvider.GetCount(new SQLCondition(sql));
                return maxSEQ + 1;
            }
            catch
            {
                return 1;
            }

        }
        #endregion

        public bool IsPerformanceCollectLine(string resCode)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += "FROM tblsysparam, tblres ";
            sql += "WHERE tblsysparam.paramcode = tblres.sscode ";
            sql += "AND tblsysparam.paramgroupcode = 'PERFORMANCECOLLECTLINE' ";
            sql += "AND tblres.rescode = '{0}' ";
            sql = string.Format(sql, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(resCode)));

            return this.DataProvider.GetCount(new SQLCondition(sql)) > 0;
        }
    }
}
