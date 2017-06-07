using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
    /// <summary>
    /// QueryRecordPerformanceFacade 的摘要说明。
    /// </summary>
    public class QueryTSPerformanceFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public QueryTSPerformanceFacade(IDomainDataProvider domainDataProvider)
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

        public object[] QueryTSPerformance(
            string modelCodes, string itemCodes, string moCodes,
            string tsResources, string tsOperators,
            string summaryTarget, int top,
            int startDate, int endDate,
            int inclusive, int exclusive)
        {
            string moCondition = "";
            if (moCodes.Trim().Length > 0 && moCodes != null)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    moCondition += string.Format(@" and a.mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and a.mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            string itemCondition = "";
            if (moCondition == "" &&
                itemCodes != "" && itemCodes != null)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    itemCondition += string.Format(@" and a.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and a.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }

            string modelCondition = "";
            if (itemCondition == "" &&
                modelCodes != "" && modelCodes != null)
            {
                if (modelCodes.IndexOf(",") >= 0)
                {
                    string[] lists = modelCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    modelCodes = string.Join(",", lists);
                    modelCondition += string.Format(@" and a.modelcode in ({0})", modelCodes.ToUpper());
                }
                else
                {
                    modelCondition += string.Format(@" and a.modelcode like '{0}%'", modelCodes.ToUpper());
                }
            }

            string tsResourceCondition = "";
            if (tsResources != "" && tsResources != null)
            {
                if (tsResources.IndexOf(",") >= 0)
                {
                    string[] lists = tsResources.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    tsResources = string.Join(",", lists);
                    tsResourceCondition += string.Format(@" and b.RRESCODE in ({0})", tsResources.ToUpper());
                }
                else
                {
                    tsResourceCondition += string.Format(@" and b.RRESCODE like '{0}%'", tsResources.ToUpper());
                }
            }

            string tsOperatorCondition = "";
            if (tsOperators != "" && tsOperators != null)
            {
                if (tsOperators.IndexOf(",") >= 0)
                {
                    string[] lists = tsOperators.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    tsOperators = string.Join(",", lists);
                    tsOperatorCondition += string.Format(@" and decode(b.MUSER,null,a.muser,'',a.muser,b.muser) in ({0})", tsOperators.ToUpper());
                }
                else
                {
                    tsOperatorCondition += string.Format(@" and decode(b.MUSER,null,a.muser,'',a.muser,b.muser) like '{0}%'", tsOperators.ToUpper());
                }
            }

            string dateCondition = FormatHelper.GetDateRangeSql("b.shiftday", startDate, endDate);

            string summaryTargetField = "a.tsid";//不再用rcard做统计，用tsid做统计，用rcard做统计，会漏掉多次送修的

            //added by jessie lee,2005/9/20
            //限制计入统计的维修状态，维修完成（回流），报废，拆解
            //modified by jessie lee for AM0256,2005/10/25,P4.13
            //改为只有维修完成得计入统计
            string tsStatus = TSStatus.TSStatus_Complete + ",";
            tsStatus += TSStatus.TSStatus_Reflow;

            string sqlStr = string.Format(
                //@" select {0} from tblts a,TBLTSERRORCAUSE b where a.tsstatus in ( {9} ) and a.tsid = b.tsid(+) {1}{2}{3}{4}{5}{6}{7} group by {8} ",
                //modified by jessie lee,2005/9/20
                @" select {0} from tblts a,TBLTSERRORCAUSE b where a.tsstatus in ( {8} ) and a.tsid = b.tsid {1}{2}{3}{4}{5}{6} group by {7} ",
                string.Format("decode(b.MUSER,null,a.muser,'',a.muser,b.muser) as tsuser,count(distinct {0}) as tsqty", summaryTargetField),
                modelCondition, itemCondition, moCondition,
                tsResourceCondition, tsOperatorCondition,
                dateCondition, "decode(b.MUSER,null,a.muser,'',a.muser,b.muser)", FormatHelper.ProcessQueryValues(tsStatus, false));

            sqlStr = " select c.TSUSER || ' - ' || tbluser.username AS TSUSER, c.TSQTY,c.TSUSER as tsuserhidden FROM   (" + sqlStr + ") c LEFT OUTER JOIN tbluser ON c.TSUSER=tbluser.usercode ";
#if DEBUG
            Log.Info(
                new PagerCondition(
                sqlStr,
                "tsqty desc",
                inclusive, Math.Min(exclusive, top), true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(QDOTSPerformance),
                new PagerCondition(
                sqlStr,
                "tsqty desc",
                inclusive, Math.Min(exclusive, top), true));

        }

        public int QueryTSPerformanceCount(
            string modelCodes, string itemCodes,string moCodes,
			string tsResources,string tsOperators,
			string summaryTarget,int startDate,int endDate,int top)
        {
            string moCondition = "";
            if (moCodes.Trim().Length > 0 && moCodes != null)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    moCondition += string.Format(@" and a.mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and a.mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            string itemCondition = "";
            if (moCondition == "" &&
                itemCodes != "" && itemCodes != null)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    itemCondition += string.Format(@" and a.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and a.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }

            string modelCondition = "";
            if (itemCondition == "" &&
                modelCodes != "" && modelCodes != null)
            {
                if (modelCodes.IndexOf(",") >= 0)
                {
                    string[] lists = modelCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    modelCodes = string.Join(",", lists);
                    modelCondition += string.Format(@" and a.modelcode in ({0})", modelCodes.ToUpper());
                }
                else
                {
                    modelCondition += string.Format(@" and a.modelcode like '{0}%'", modelCodes.ToUpper());
                }
            }

            string tsResourceCondition = "";
            if (tsResources != "" && tsResources != null)
            {
                if (tsResources.IndexOf(",") >= 0)
                {
                    string[] lists = tsResources.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    tsResources = string.Join(",", lists);
                    tsResourceCondition += string.Format(@" and b.RRESCODE in ({0})", tsResources.ToUpper());
                }
                else
                {
                    tsResourceCondition += string.Format(@" and b.RRESCODE like '{0}%'", tsResources.ToUpper());
                }
            }

            string tsOperatorCondition = "";
            if (tsOperators != "" && tsOperators != null)
            {
                if (tsOperators.IndexOf(",") >= 0)
                {
                    string[] lists = tsOperators.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    tsOperators = string.Join(",", lists);
                    tsOperatorCondition += string.Format(@" and decode(b.MUSER,null,a.muser,'',a.muser,b.muser) in ({0})", tsOperators.ToUpper());
                }
                else
                {
                    tsOperatorCondition += string.Format(@" and decode(b.MUSER,null,a.muser,'',a.muser,b.muser) like '{0}%'", tsOperators.ToUpper());
                }
            }

            string dateCondition = FormatHelper.GetDateRangeSql("b.shiftday", startDate, endDate);

            string summaryTargetField = "a.tsid";

            //added by jessie lee,2005/9/20
            //限制计入统计的维修状态，维修完成（回流），报废，拆解
            //modified by jessie lee for AM0256,2005/10/25,P4.13
            //改为只有维修完成得计入统计
            string tsStatus = TSStatus.TSStatus_Complete + ",";
            tsStatus += TSStatus.TSStatus_Reflow;

            string sqlStr = string.Format(
                //@" select count(*) from (select {0} from tblts a,TBLTSERRORCAUSE b where a.tsstatus in ( {9} ) and a.tsid = b.tsid(+) {1}{2}{3}{4}{5}{6}{7} group by {8})",
                //modified by jessie lee,2005/9/20
                @" select count(*) from (select {0} from tblts a,TBLTSERRORCAUSE b where a.tsstatus in ( {8} ) and a.tsid = b.tsid {1}{2}{3}{4}{5}{6} group by {7})",
                string.Format("decode(b.MUSER,null,a.muser,'',a.muser,b.muser) as tsuser,count(distinct {0}) as tsqty", summaryTargetField),
                modelCondition, itemCondition, moCondition,
                tsResourceCondition, tsOperatorCondition,
                dateCondition, "decode(b.MUSER,null,a.muser,'',a.muser,b.muser)", FormatHelper.ProcessQueryValues(tsStatus, false));

#if DEBUG
            Log.Info(
                new SQLCondition(
                sqlStr).SQLText);
#endif
            return Math.Min(top, this.DataProvider.GetCount(
                new SQLCondition(
                sqlStr)));
        }
    }
}
