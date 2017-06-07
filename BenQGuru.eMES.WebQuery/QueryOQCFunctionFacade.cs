using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.WebQuery
{
    /// <summary>
    /// QueryOQCFunctionFacade 的摘要说明。
    /// </summary>
    public class QueryOQCFunctionFacade
    {
        public QueryOQCFunctionFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        private IDomainDataProvider _domainDataProvider = null;

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

        public string GetRCardListInCarton(string cartonNo, string rcardStart, string rcardEnd)
        {
            if (cartonNo == string.Empty)
                return string.Empty;
            string strSql = "SELECT RCard FROM tblSimulationReport WHERE CartonCode='" + cartonNo + "' ";
            if (rcardStart != string.Empty)
                strSql += " AND RCard>='" + rcardStart + "' ";
            if (rcardEnd != string.Empty)
                strSql += " AND RCard<='" + rcardEnd + "' ";
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.SimulationReport), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return string.Empty;
            string rcardList = string.Empty;
            for (int i = 0; i < objs.Length; i++)
            {
                rcardList += ((BenQGuru.eMES.Domain.DataCollect.SimulationReport)objs[i]).RunningCard + ",";
            }
            if (rcardList.Length > 0)
                rcardList = rcardList.Remove(rcardList.Length - 1, 1);
            return rcardList;
        }

        public object GetOQCFuncTestValue(string RCard, int RCardSeq, string LotNo, string LotSeq)
        {
            // Added by Icyer 2006/08/15
            string sql = string.Format("SELECT {0} FROM TBLOQCFUNCTESTVALUE WHERE RCARD='{1}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValue)), RCard);
            // Added end
            /*	Removed by Icyer 2006/08/15
            string sql = string.Format("SELECT {0} FROM TBLOQCFUNCTESTVALUE WHERE RCARD='{1}' AND LOTNO = '{2}' AND LOTSEQ = {3}",//AND RCARDSEQ={2}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValue)),RCard,LotNo,LotSeq);//,RCardSeq);
            */

            object[] objs = this.DataProvider.CustomQuery(typeof(OQCFuncTestValue), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return objs[0];
            else
                return null;
        }
              
        public object[] QueryOQCFuncTestValue(string RCard, int RCardSeq,string LotNo,string LotSeq,int inclusive,int exclusive)
        {
            string sql = string.Format("SELECT {0} FROM TBLOQCFUNCTESTVALUE WHERE RCARD='{1}' AND LOTNO = '{2}' AND LOTSEQ = {3}",// AND RCARDSEQ={2}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValue)), RCard, LotNo, LotSeq);//,RCardSeq);

            return this.DataProvider.CustomQuery(typeof(OQCFuncTestValue), new PagerCondition(sql, "rcardseq", inclusive, exclusive));
        }

        public int QueryOQCFuncTestValueCount(string RCard, int RCardSeq,string LotNo,string LotSeq)
        {
            string sql = string.Format("SELECT count(rcard) FROM TBLOQCFUNCTESTVALUE WHERE RCARD='{0}' AND LOTNO = '{1}' AND LOTSEQ = {2}",//AND RCARDSEQ={1}",
                RCard, LotNo, LotSeq);//,RCardSeq);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        
        public int QueryMaxOQCFuncTestValueDetailElectricCount(string itemCode,
            string moCode,
            string lOTNO,
            string startsn, string endsn,
            string ssCode,
			int oqcBeginDate,int oqcBeginTime,
			int oqcEndDate ,int oqcEndTime)
        {
            string itemCodition = string.Empty;
            if (itemCode.Trim().Length > 0 && itemCode != null)
            {
                if (itemCode.IndexOf(",") >= 0)
                {
                    string[] lists = itemCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCode = string.Join(",", lists);
                    itemCodition += string.Format(@" and itemcode in ({0})", itemCode.ToUpper());
                }
                else
                {
                    itemCodition += string.Format(@" and itemcode like '{0}%'", itemCode.ToUpper());
                }

            }

            string moCodition = string.Empty;
            if (moCode.Trim().Length > 0 && moCode != null)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    itemCodition += string.Format(@" and mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    itemCodition += string.Format(@" and mocode like '{0}%'", moCode.ToUpper());
                }
            }

            string lotCodition = string.Empty;
            if (lOTNO != null && lOTNO != string.Empty)
            {
                if (lOTNO.IndexOf(",") < 0)
                    lotCodition = string.Format(" AND LOTNO LIKE '{0}%' ", lOTNO.ToUpper());
                else
                    lotCodition = string.Format(" AND LOTNO IN ('{0}') ", lOTNO.ToUpper().Replace(",", "','"));
            }
            string rcardCodition = string.Empty;
            if (startsn != null && startsn != string.Empty)
            {
                rcardCodition = FormatHelper.GetCodeRangeSql("rcard", startsn, endsn);
            }

            string sscodeCondition = string.Empty;
            if (ssCode != null && ssCode != "")
            {
                sscodeCondition = string.Format(" and sscode in ({0}) ", FormatHelper.ProcessQueryValues(ssCode));
            }

            //string dateCondition = FormatHelper.GetDateRangeSql("mdate",oqcBeginDate,oqcEndDate);

            string dateCondition = FormatHelper.GetDateRangeSql("mdate", "mtime", oqcBeginDate, oqcBeginTime, oqcEndDate, oqcEndTime);

            string sql = string.Format("SELECT {0} FROM TBLOQCFUNCTESTVALUE WHERE (RCARD,LOTNO,LOTSEQ) in (SELECT RCARD,LOTNO,LOTSEQ FROM TBLOQCFUNCTESTVALUE WHERE 1=1 {1}{2}{3}{4}{5} {6}) ",//AND RCARDSEQ={2}",
                "max(ElectricTestCount)", itemCodition, moCodition,
                lotCodition, rcardCodition, dateCondition, sscodeCondition);//,RCardSeq);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        
        public object[] QueryOQCDimention(string RCard, int RCardSeq,string LotNo,string LotSeq,int inclusive,int exclusive)
        {
            string sql = string.Format("SELECT {0} FROM TBLOQCDIM WHERE RCARD='{1}' AND LOTNO = '{2}' AND LOTSEQ = {3}",//AND RCARDSEQ={2}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDimention)), RCard, LotNo, LotSeq);//,RCardSeq);

            return this.DataProvider.CustomQuery(typeof(OQCDimention), new PagerCondition(sql, "rcardseq", inclusive, exclusive));
        }

        public int QueryOQCDimentionCount(string RCard, int RCardSeq,string LotNo,string LotSeq)
        {
            string sql = string.Format("SELECT count(rcard) FROM TBLOQCDIM WHERE RCARD='{0}' AND LOTNO = '{1}' AND LOTSEQ = {2}",// AND RCARDSEQ={1}",
                RCard, LotNo, LotSeq);//,RCardSeq);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

    }
}
