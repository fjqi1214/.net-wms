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
    /// QueryTSInfoFacade 的摘要说明。
    /// </summary>
    public class QueryTSInfoFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public QueryTSInfoFacade(IDomainDataProvider domainDataProvider)
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

        public object[] QueryTSInfo(
            string modelCodes, string itemCodes, string moCodes,
            int startDate, int endDate,
            string summaryTarget, int top,
            int inclusive, int exclusive)
        {
            string sql = string.Empty;
            string condition = string.Empty;

            if (modelCodes != null && modelCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and modelcode in({0})", FormatHelper.ProcessQueryValues(modelCodes));
            }
            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and itemcode in({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }
            if (moCodes != null && moCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and mocode in({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            condition += FormatHelper.GetDateRangeSql("SHIFTDAY", startDate, endDate);

            if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                //modified by jessie lee for AM0228,2005/10/17,P4.11
                //按ErrorCodeGroup统计,在tbltserrorcode里查询
                sql = string.Format(@"select ECGCODE as errorcodegroup, qty,allqty, qty / allqty as percent
										 from (select ECGCODE, count(distinct tsid) as qty
												 from tbltserrorcode
												where tsid in
													   (select tsid
														       from tblts
														      where tblts.tsid = tbltserrorcode.tsid {0})
												group by ECGCODE),
											  (select count(*) as allqty 
												 from (select distinct tsid,ECGCODE
												         from tbltserrorcode
												        where tsid in (select tsid 
																         from tblts 
																        where tblts.tsid = tbltserrorcode.tsid {0})))",
                    condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcodegroup, ecgdesc, qty,allqty, percent from ({0}) a,tblecg b where a.errorcodegroup=b.ecgcode ", sql);
            }
            //modified by jessie lee for AM0228,2005/10/17,P4.11
            //按ErrorCode统计,在tbltserrorcode里查询
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                sql = string.Format(@"select ECGCODE as errorcodegroup, ECODE as errorcode, qty,allqty, qty/allQty as percent from
(select ECGCODE, ECODE, count(distinct tsid) as qty from tbltserrorcode where tsid in (select tsid from tblts where tblts.tsid = tbltserrorcode.tsid {0}) group by ECGCODE, ECODE),
(select count(*) as allQty from ( select distinct ECGCODE, ECODE,tsid from tbltserrorcode where tsid in (select tsid from tblts where tblts.tsid = tbltserrorcode.tsid {0}) ))", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcodegroup, ecgdesc,errorcode,ecdesc, qty,allqty, percent from ({0}) a,tblecg b,tblec c where a.errorcodegroup=b.ecgcode and a.errorcode = c.ecode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                sql = string.Format(@"select ECSCODE as errorcause, qty,allqty, qty/allQty as percent from
(select ECSCODE, count(distinct tsid) as qty from TBLTSERRORCAUSE where tsid in (select tsid from tblts where 1=1 {0}) group by ECSCODE),
(select count(*) as allQty from ( select distinct ECSCODE,tsid from TBLTSERRORCAUSE where tsid in (select tsid from tblts where tblts.tsid = TBLTSERRORCAUSE.tsid {0}) ))", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcause, ecsdesc, qty,allqty, percent from ({0}) a,tblecs b where a.errorcause=b.ecscode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                sql = string.Format(@"select ELOC as errorlocation, qty,allqty, qty/allQty as percent from
(select ELOC, count(distinct tsid) as qty from TBLTSERRORCAUSE2LOC where tsid in (select tsid from tblts where 1=1 {0}) group by ELOC),
(select count(*) as allQty from ( select distinct ELOC,tsid from TBLTSERRORCAUSE2LOC where tsid in (select tsid from tblts where tblts.tsid = TBLTSERRORCAUSE2LOC.tsid {0}) ))", condition);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                sql = string.Format(@"select DUTYCODE as duty, qty,allqty, qty/allQty as percent from
(select DUTYCODE, count(distinct tsid) as qty from TBLTSERRORCAUSE where tsid in (select tsid from tblts where 1=1 {0}) group by DUTYCODE),
(select count(*) as allQty from ( select distinct DUTYCODE,tsid from TBLTSERRORCAUSE where tsid in (select tsid from tblts where tblts.tsid = TBLTSERRORCAUSE.tsid {0}) ))", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select duty, dutydesc, qty,allqty, percent from ({0}) a,tblduty b where a.duty=b.dutycode ", sql);
            }

#if DEBUG
            Log.Info(
                new PagerCondition(
                sql, "qty desc",
                inclusive, Math.Min(exclusive, top), true).SQLText);

#endif

            return this.DataProvider.CustomQuery(typeof(QDOTSInfo), new PagerCondition(sql, "qty desc", inclusive, Math.Min(exclusive, top), true));
        }

        public int QueryTSInfoCount(
            string modelCodes, string itemCodes, string moCodes,
            int startDate, int endDate,
            string summaryTarget, int top)
        {
            string sql = string.Empty;
            string condition = string.Empty;

            if (modelCodes != null && modelCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and modelcode in({0})", FormatHelper.ProcessQueryValues(modelCodes));
            }
            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and itemcode in({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }
            if (moCodes != null && moCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and mocode in({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            condition += FormatHelper.GetDateRangeSql("SHIFTDAY", startDate, endDate);

            if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                sql = string.Format(@"select count(*) from (select ECGCODE from tbltserrorcode where tsid in 
(select tsid from tblts where tblts.tsid = tbltserrorcode.tsid {0}) group by ECGCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                sql = string.Format(@"select count(*) from (select ECGCODE, ECODE from tbltserrorcode where tsid in 
(select tsid from tblts where tblts.tsid = tbltserrorcode.tsid {0}) group by ECGCODE, ECODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                sql = string.Format(@"select count(*) from (select ECSCODE from TBLTSERRORCAUSE where tsid in 
(select tsid from tblts where tblts.tsid = TBLTSERRORCAUSE.tsid {0}) group by ECSCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                sql = string.Format(@"select count(*) from (select DUTYCODE from TBLTSERRORCAUSE where tsid in 
(select tsid from tblts where tblts.tsid = TBLTSERRORCAUSE.tsid {0}) group by DUTYCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                sql = string.Format(@"select count(*) from (select ELOC from TBLTSERRORCAUSE2LOC where tsid in 
(select tsid from tblts where tblts.tsid = TBLTSERRORCAUSE2LOC.tsid {0}) group by ELOC)", condition);
            }

#if DEBUG
            Log.Info(
                new SQLCondition(sql).SQLText);
#endif
            return Math.Min(top, this.DataProvider.GetCount(new SQLCondition(sql)));
        }


        public object[] QueryTSInfo(
            string ecg, string ec, string ecs, string el, string ed,
            string modelCodes, string itemCodes, string moCodes, string frmResources,

            int startDate, int endDate,
            string summaryTarget, string lotNo, int top,
            int inclusive, int exclusive)
        {
            string sql = string.Empty;
            string condition = @" from tblts ts,tbltserrorcause ecs,tbltserrorcause2loc el
                                 where ts.tsid=ecs.tsid
                                 and ecs.tsid = el.tsid(+)
                                 and ecs.ecgcode = el.ecgcode(+)
                                 and ecs.ecode = el.ecode(+)
                                 and ecs.ecscode = el.ecscode(+) ";

















            if (modelCodes != null && modelCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.modelcode in({0})", FormatHelper.ProcessQueryValues(modelCodes));
            }
            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.itemcode in({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }
            if (moCodes != null && moCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.mocode in({0})", FormatHelper.ProcessQueryValues(moCodes));
            }
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.FRMRESCODE in({0})", FormatHelper.ProcessQueryValues(frmResources));
            }
            if (lotNo != null && lotNo.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.rcard in (select rcard from tbllot2card where lotno in ({0}) ) ", FormatHelper.ProcessQueryValues(lotNo));
            }
            condition += FormatHelper.GetDateRangeSql("ts.SHIFTDAY", startDate, endDate);

            if (ecg != null && ecg.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecgcode in({0})", FormatHelper.ProcessQueryValues(ecg));
            }

            if (ec != null && ec.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecode in({0})", FormatHelper.ProcessQueryValues(ec));
            }

            if (ecs != null && ecs.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecscode in({0})", FormatHelper.ProcessQueryValues(ecs));
            }

            if (el != null && el.Trim() != string.Empty)
            {
                condition += string.Format(" and el.eloc in({0})", FormatHelper.ProcessQueryValues(el));
            }

            if (ed != null && ed.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.dutycode in({0})", FormatHelper.ProcessQueryValues(ed));
            }

            if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                //modified by jessie lee for AM0228,2005/10/17,P4.11
                //按ErrorCodeGroup统计,在tbltserrorcode里查询 
                //使用分析函数sum()over() 和 ratio_to_report()over()增加速度 joe song 20060714
                condition = "select ts.tsid,ecs.ecgcode " + condition;
                sql = string.Format(@"select ECGCODE as errorcodegroup, sum(qty)over() allqty,qty, ratio_to_report (qty)over() percent
										 from (select ECGCODE, count(distinct tsid) as qty
												 from tbltserrorcode
												where (tsid,ecgcode) in
													   ({0})
												group by ECGCODE)",
                    condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcodegroup, ecgdesc, qty,allqty, percent from ({0}) a,tblecg b where a.errorcodegroup=b.ecgcode ", sql);
            }
            //modified by jessie lee for AM0228,2005/10/17,P4.11
            //按ErrorCode统计,在tbltserrorcode里查询
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode " + condition;
                sql = string.Format(@"select ECGCODE as errorcodegroup, ECODE as errorcode, sum(qty)over() allqty,qty, ratio_to_report (qty)over() percent from
                                    (select ECGCODE, ECODE, count(distinct tsid) as qty from tbltserrorcode where (tsid,ecgcode,ecode) in ({0}) group by ECGCODE, ECODE)
                                     ", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcodegroup, ecgdesc,errorcode,ecdesc, qty,allqty, percent from ({0}) a,tblecg b,tblec c where a.errorcodegroup=b.ecgcode and a.errorcode = c.ecode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                //change by joe song
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"SELECT ECSCODE errorcause,sum(qty)over() allqty,qty,ratio_to_report (qty)over() percent 
									from (select ECSCODE, count(distinct tsid) as qty from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode) in ({0}) group by ECSCODE)
									",
                                condition);

                sql = string.Format(" select errorcause, ecsdesc,allqty, qty,percent from ({0}) a,tblecs b where a.errorcause=b.ecscode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"select ELOC as errorlocation, sum(qty)over() allqty,qty,ratio_to_report (qty)over() percent  from
                                      (select ELOC, count(distinct tsid) as qty from TBLTSERRORCAUSE2LOC where (tsid,ecgcode,ecode,ecscode) in ({0}) group by ELOC)
                                      ", condition);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"select DUTYCODE as duty, sum(qty)over() allqty,qty,ratio_to_report (qty)over() percent  from
                                    (select DUTYCODE, count(distinct tsid) as qty from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode) in ({0}) group by DUTYCODE)
                                    ", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select duty, dutydesc, qty,allqty, percent from ({0}) a,tblduty b where a.duty=b.dutycode ", sql);
            }

#if DEBUG
            Log.Info(
                new PagerCondition(
                sql, "qty desc",
                inclusive, Math.Min(exclusive, top), true).SQLText);

#endif

            object[] rets = this.DataProvider.CustomQuery(typeof(QDOTSInfo), new PagerCondition(sql, "qty desc", inclusive, Math.Min(exclusive, top), true));
            return rets;
        }

        public int QueryTSInfoCount(
            string ecg, string ec, string ecs, string el, string ed,
            string modelCodes, string itemCodes, string moCodes, string frmResources,

            int startDate, int endDate,
            string summaryTarget, string lotNo, int top)
        {
            string sql = string.Empty;
            string condition = @"  from tblts ts,tbltserrorcause ecs,tbltserrorcause2loc el
                                 where ts.tsid=ecs.tsid
                                 and ecs.tsid = el.tsid(+)
                                 and ecs.ecgcode = el.ecgcode(+)
                                 and ecs.ecode = el.ecode(+)
                                 and ecs.ecscode = el.ecscode(+) ";

            if (modelCodes != null && modelCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.modelcode in({0})", FormatHelper.ProcessQueryValues(modelCodes));
            }
            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.itemcode in({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }
            if (moCodes != null && moCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.mocode in({0})", FormatHelper.ProcessQueryValues(moCodes));
            }
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.FRMRESCODE in({0})", FormatHelper.ProcessQueryValues(frmResources));
            }
            if (lotNo != null && lotNo.Trim() != string.Empty)
            {
                condition += string.Format(" and ts.rcard in (select rcard from tbllot2card where lotno in ({0}) ) ", FormatHelper.ProcessQueryValues(lotNo));
            }
            condition += FormatHelper.GetDateRangeSql("ts.SHIFTDAY", startDate, endDate);

            if (ecg != null && ecg.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecgcode in({0})", FormatHelper.ProcessQueryValues(ecg));
            }

            if (ec != null && ec.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecode in({0})", FormatHelper.ProcessQueryValues(ec));
            }

            if (ecs != null && ecs.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecscode in({0})", FormatHelper.ProcessQueryValues(ecs));
            }

            if (el != null && el.Trim() != string.Empty)
            {
                condition += string.Format(" and el.eloc in({0})", FormatHelper.ProcessQueryValues(el));
            }

            if (ed != null && ed.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.dutycode in({0})", FormatHelper.ProcessQueryValues(ed));
            }

            if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                condition = "select ts.tsid,ecs.ecgcode " + condition;
                sql = string.Format(@"select count(*) from (select ECGCODE from tbltserrorcode where (tsid,ecgcode) in ({0}) group by ECGCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode " + condition;
                sql = string.Format(@"select count(*) from (select ECGCODE, ECODE from tbltserrorcode where (tsid,ecgcode,ecode) in ({0}) group by ECGCODE, ECODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"select count(*) from (select ECSCODE from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode) in ({0}) group by ECSCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"select count(*) from (select DUTYCODE from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode) in ({0}) group by DUTYCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode" + condition;
                sql = string.Format(@"select count(*) from (select ELOC from TBLTSERRORCAUSE2LOC where (tsid,ecgcode,ecode,ecscode) in ({0}) group by ELOC)", condition);
            }

#if DEBUG
            Log.Info(
                new SQLCondition(sql).SQLText);
#endif
            return Math.Min(top, this.DataProvider.GetCount(new SQLCondition(sql)));
        }

        //add by roger.xue on 200/11/03: query with mtype and itemclass
        #region Query TS
        public object[] QueryTSInfo(
           string ecg, string ec, string ecs, string ecsg, string el, string ed,
           string itemType, string itemCodes, string moCodes, string frmResources,
           string firstClass, string secondClass, string thirdClass,
           int startDate, int endDate,
           string summaryTarget, string lotNo, int top, string errorComponent,
           int inclusive, int exclusive)
        {
            string sql = string.Empty;
            string condition = @" from tblts ts,tbltserrorcause ecs,tbltserrorcause2loc el,tblmaterial mal, tblitemclass its,tbltserrorcause2com sc
                                 where ts.tsid=ecs.tsid(+)
                                 and ecs.tsid = el.tsid(+)
                                 and ecs.ecgcode = el.ecgcode(+)
                                 and ecs.ecode = el.ecode(+)
                                 and ecs.ecscode = el.ecscode(+) 
                                 and ecs.ecsgcode = el.ecsgcode(+) 
                                 and ts.itemcode = mal.mcode(+) 
                                 and its.itemgroup(+) = mal.mgroup
                                 and ecs.tsid=sc.tsid(+)
                                 and ecs.ecode =sc.ecode(+)
                                 and ecs.ecgcode =sc.ecgcode(+)
                                 and ecs.ecscode =sc.ecscode(+)
                                 and ecs.ecsgcode =sc.ecsgcode(+)";
            if (firstClass != null && firstClass.Trim() != string.Empty)
            {
                condition += string.Format(" and its.firstclass = '{0}' ", firstClass);
            }

            if (secondClass != null && secondClass.Trim() != string.Empty)
            {
                condition += string.Format(" and its.secondclass = '{0}' ", secondClass);
            }

            if (thirdClass != null && thirdClass.Trim() != string.Empty)
            {
                condition += string.Format(" and its.thirdclass = '{0}' ", thirdClass);
            }

            if (itemType != null && itemType.Trim() != string.Empty)
            {
                condition += string.Format(" and mal.mtype = '{0}' ", itemType);
            }
            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    condition += string.Format(" and ts.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    condition += string.Format(" and ts.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }
            if (moCodes != null && moCodes.Trim() != string.Empty)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    condition += string.Format(" and ts.mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    condition += string.Format(" and ts.mocode like '{0}%'", moCodes.ToUpper());
                }
            }
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                if (frmResources.IndexOf(",") >= 0)
                {
                    string[] lists = frmResources.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    frmResources = string.Join(",", lists);
                    condition += string.Format(" and ts.FRMRESCODE in ({0})", frmResources.ToUpper());
                }
                else
                {
                    condition += string.Format(" and ts.FRMRESCODE like '{0}%'", frmResources.ToUpper());
                }
            }
            if (lotNo != null && lotNo.Trim() != string.Empty)
            {
                if (lotNo.IndexOf(",") >= 0)
                {
                    string[] lists = lotNo.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    lotNo = string.Join(",", lists);
                    condition += string.Format(" and ts.rcard in (select rcard from tbllot2card where lotno in ({0}))", lotNo.ToUpper());
                }
                else
                {
                    condition += string.Format(" and ts.rcard in (select rcard from tbllot2card where lotno like '{0}%')", lotNo.ToUpper());
                }
            }
            condition += FormatHelper.GetDateRangeSql("ts.SHIFTDAY", startDate, endDate);

            if (ecg != null && ecg.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecgcode like  '{0}%' ", ecg);
            }

            if (ec != null && ec.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecode like '{0}%'", ec);
            }

            if (ecs != null && ecs.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecscode like '{0}%'", ecs);
            }

            if (ecsg != null && ecsg.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecsgcode like '{0}%'", ecsg);
            }


            if (errorComponent != null && errorComponent.Trim() != string.Empty)
            {
                condition += string.Format(" and sc.errorcomponent like '{0}%'", errorComponent);
            }

            if (el != null && el.Trim() != string.Empty)
            {
                condition += string.Format(" and el.eloc like '{0}%'", el);
            }

            if (ed != null && ed.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.dutycode like '{0}%'", ed);
            }

            if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                //modified by jessie lee for AM0228,2005/10/17,P4.11
                //按ErrorCodeGroup统计,在tbltserrorcode里查询 
                //使用分析函数sum()over() 和 ratio_to_report()over()增加速度 joe song 20060714
                condition = "select ts.tsid,ecs.ecgcode " + condition;
                sql = string.Format(@"select ECGCODE as errorcodegroup, sum(qty)over() allqty,qty,round( ratio_to_report (qty)over(),4) percent
										 from (select ECGCODE, count(distinct tsid) as qty
												 from tbltserrorcode
												where (tsid,ecgcode) in
													   ({0})
												group by ECGCODE)",
                    condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcodegroup, ecgdesc, qty,allqty, percent from ({0}) a,tblecg b where a.errorcodegroup=b.ecgcode ", sql);
            }
            //modified by jessie lee for AM0228,2005/10/17,P4.11
            //按ErrorCode统计,在tbltserrorcode里查询
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode " + condition;
                sql = string.Format(@"select ECGCODE as errorcodegroup, ECODE as errorcode, sum(qty)over() allqty,qty, round( ratio_to_report (qty)over(),4)  percent from
                                    (select ECGCODE, ECODE, count(distinct tsid) as qty from tbltserrorcode where (tsid,ecgcode,ecode) in ({0}) group by ECGCODE, ECODE)
                                     ", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcodegroup, ecgdesc,errorcode,ecdesc, qty,allqty, percent from ({0}) a,tblecg b,tblec c where a.errorcodegroup=b.ecgcode and a.errorcode = c.ecode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCauseGroup)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode,ecs.ecsgcode " + condition;
                sql = string.Format(@"SELECT ECSGCODE errorcausegroup,sum(qty)over() allqty,qty,round( ratio_to_report (qty)over(),4) percent 
									from (select ECSGCODE, count(distinct tsid) as qty from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode,ecsgcode) in ({0}) group by ECSGCODE)
									",
                                condition);

                sql = string.Format(" select errorcausegroup, ecsgdesc,allqty, qty,percent from ({0}) a,tblecsg b where a.errorcausegroup=b.ecsgcode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                //change by joe song
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"SELECT ECSCODE errorcause,sum(qty)over() allqty,qty,round( ratio_to_report (qty)over(),4) percent 
									from (select ECSCODE, count(distinct tsid) as qty from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode) in ({0}) group by ECSCODE)
									",
                                condition);

                sql = string.Format(" select errorcause, ecsdesc,allqty, qty,percent from ({0}) a,tblecs b where a.errorcause=b.ecscode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"select ELOC as errorlocation, sum(qty)over() allqty,qty,round( ratio_to_report (qty)over(),4) percent  from
                                      (select ELOC, count(distinct tsid) as qty from TBLTSERRORCAUSE2LOC where (tsid,ecgcode,ecode,ecscode) in ({0}) group by ELOC)
                                      ", condition);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"select DUTYCODE as duty, sum(qty)over() allqty,qty,round( ratio_to_report (qty)over(),4) percent  from
                                    (select DUTYCODE, count(distinct tsid) as qty from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode) in ({0}) group by DUTYCODE)
                                    ", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select duty, dutydesc, qty,allqty, percent from ({0}) a,tblduty b where a.duty=b.dutycode ", sql);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Errorcomponent)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"select Errorcomponent as Errorcomponent, sum(qty)over() allqty,qty,round( ratio_to_report (qty)over(),4) percent  from
                                      (select Errorcomponent, count(distinct tsid) as qty from TBLTSERRORCAUSE2COM where (tsid,ecgcode,ecode,ecscode) in ({0}) group by Errorcomponent)
                                      ", condition);
            }



#if DEBUG
            Log.Info(
                new PagerCondition(
                sql, "qty desc",
                inclusive, Math.Min(exclusive, top), true).SQLText);

#endif

            object[] rets = this.DataProvider.CustomQuery(typeof(QDOTSInfo), new PagerCondition(sql, "qty desc", inclusive, Math.Min(exclusive, top), true));
            return rets;
        }

        public int QueryTSInfoCount(
            string ecg, string ec, string ecs, string ecsg, string el, string ed,
            string itemType, string itemCodes, string moCodes, string frmResources,
            string firstClass, string secondClass, string thirdClass,
            int startDate, int endDate,
            string summaryTarget, string lotNo, int top, string errorComponent)
        {
            string sql = string.Empty;
            string condition = @" from tblts ts,tbltserrorcause ecs,tbltserrorcause2loc el,tblmaterial mal, tblitemclass its ,tbltserrorcause2com sc
                                 where ts.tsid=ecs.tsid(+)
                                 and ecs.tsid = el.tsid(+)
                                 and ecs.ecgcode = el.ecgcode(+)
                                 and ecs.ecode = el.ecode(+)
                                 and ecs.ecscode = el.ecscode(+) 
                                 and ecs.ecsgcode = el.ecsgcode(+)
                                 and ts.itemcode = mal.mcode(+) 
                                 and its.itemgroup(+) = mal.mgroup
                                 and ecs.tsid=sc.tsid(+)
                                 and ecs.ecode =sc.ecode(+)
                                 and ecs.ecgcode =sc.ecgcode(+)
                                 and ecs.ecscode =sc.ecscode(+)
                                 and ecs.ecsgcode =sc.ecsgcode(+)";
            if (firstClass != null && firstClass.Trim() != string.Empty)
            {
                condition += string.Format(" and its.firstclass = '{0}' ", firstClass);
            }

            if (secondClass != null && secondClass.Trim() != string.Empty)
            {
                condition += string.Format(" and its.secondclass = '{0}' ", secondClass);
            }

            if (thirdClass != null && thirdClass.Trim() != string.Empty)
            {
                condition += string.Format(" and its.thirdclass = '{0}' ", thirdClass);
            }

            if (itemType != null && itemType.Trim() != string.Empty)
            {
                condition += string.Format(" and mal.mtype = '{0}' ", itemType);
            }


            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    condition += string.Format(" and ts.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    condition += string.Format(" and ts.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }
            if (moCodes != null && moCodes.Trim() != string.Empty)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    condition += string.Format(" and ts.mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    condition += string.Format(" and ts.mocode like '{0}%'", moCodes.ToUpper());
                }
            }
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                if (frmResources.IndexOf(",") >= 0)
                {
                    string[] lists = frmResources.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    frmResources = string.Join(",", lists);
                    condition += string.Format(" and ts.FRMRESCODE in ({0})", frmResources.ToUpper());
                }
                else
                {
                    condition += string.Format(" and ts.FRMRESCODE like '{0}%'", frmResources.ToUpper());
                }
            }
            if (lotNo != null && lotNo.Trim() != string.Empty)
            {
                if (lotNo.IndexOf(",") >= 0)
                {
                    string[] lists = lotNo.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    lotNo = string.Join(",", lists);
                    condition += string.Format(" and ts.rcard in (select rcard from tbllot2card where lotno in ({0}))", lotNo.ToUpper());
                }
                else
                {
                    condition += string.Format(" and ts.rcard in (select rcard from tbllot2card where lotno like '{0}%')", lotNo.ToUpper());
                }
            }



            condition += FormatHelper.GetDateRangeSql("ts.SHIFTDAY", startDate, endDate);

            if (ecg != null && ecg.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecgcode like '{0}%'", ecg);
            }

            if (ec != null && ec.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecode like '{0}%'", ec);
            }

            if (errorComponent != null && errorComponent.Trim() != string.Empty)
            {
                condition += string.Format(" and sc.errorcomponent like '{0}%'", errorComponent);
            }

            if (ecs != null && ecs.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecscode like '{0}%'", ecs);
            }

            if (ecsg != null && ecsg.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.ecsgcode like '{0}%'", ecsg);
            }

            if (el != null && el.Trim() != string.Empty)
            {
                condition += string.Format(" and el.eloc like '{0}%'", el);
            }

            if (ed != null && ed.Trim() != string.Empty)
            {
                condition += string.Format(" and ecs.dutycode like '{0}%'", ed);
            }



            if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                condition = "select ts.tsid,ecs.ecgcode " + condition;
                sql = string.Format(@"select count(*) from (select ECGCODE from tbltserrorcode where (tsid,ecgcode) in ({0}) group by ECGCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode " + condition;
                sql = string.Format(@"select count(*) from (select ECGCODE, ECODE from tbltserrorcode where (tsid,ecgcode,ecode) in ({0}) group by ECGCODE, ECODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCauseGroup)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode,ecs.ecsgcode " + condition;
                sql = string.Format(@"select count(*) from (select ECSGCODE from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode,ecsgcode) in ({0}) group by ECSGCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"select count(*) from (select ECSCODE from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode) in ({0}) group by ECSCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode " + condition;
                sql = string.Format(@"select count(*) from (select DUTYCODE from TBLTSERRORCAUSE where (tsid,ecgcode,ecode,ecscode) in ({0}) group by DUTYCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode" + condition;
                sql = string.Format(@"select count(*) from (select ELOC from TBLTSERRORCAUSE2LOC where (tsid,ecgcode,ecode,ecscode) in ({0}) group by ELOC)", condition);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Errorcomponent)
            {
                condition = "select ts.tsid,ecs.ecgcode,ecs.ecode,ecs.ecscode" + condition;
                sql = string.Format(@"select count(*) from (select Errorcomponent from TBLTSERRORCAUSE2COM where (tsid,ecgcode,ecode,ecscode) in ({0}) group by Errorcomponent)", condition);
            }

#if DEBUG
            Log.Info(
                new SQLCondition(sql).SQLText);
#endif
            return Math.Min(top, this.DataProvider.GetCount(new SQLCondition(sql)));
        }
        #endregion

        #region Abandon

        public object[] QueryTSInfoList(
            string moCode,
            int startDate, int endDate,
            string summaryTarget, string summaryObject, string summaryObject1,
            string modelCodes, string itemCodes,
            int inclusive, int exclusive)
        {
            string modelCondition = string.Empty;
            if (modelCodes != null && modelCodes.Trim() != string.Empty)
            {
                modelCondition += string.Format(" and a.modelcode in({0})", FormatHelper.ProcessQueryValues(modelCodes));
            }

            string itemCondition = string.Empty;
            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                itemCondition += string.Format(" and a.itemcode in({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
            }

            string dateCondition = FormatHelper.GetDateRangeSql("a.SHIFTDAY", startDate, endDate);

            //ErrorCodeGroup,ErrorCode用sqlStr1
            string sqlStr1 = "select distinct {0} from tblts a,tbltserrorcode b,{1} where a.tsid = b.tsid and a.tsid = c.tsid(+) {2}{3}{4}{5}{6} group by {7} ";
            //ErrorCause,ErrorLocation,Duty用sqlStr2
            string sqlStr2 = "select distinct {0} from tblts a,{1} where a.tsid = b.tsid(+) {2}{3}{4}{5}{6} group by {7} ";

            string table = "";
            string summaryTargetCondition = "";
            if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                table = "TBLTSERRORCAUSE c";
                summaryTargetCondition = string.Format(@" and b.ECGCODE = '{0}' and b.ECODE = '{1}'", summaryObject, summaryObject1);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                table = "TBLTSERRORCAUSE b";
                summaryTargetCondition = string.Format(@" and b.ECSCODE = '{0}'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                table = "TBLTSERRORCAUSE c";
                summaryTargetCondition = string.Format(@" and b.ECGCODE = '{0}'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                table = "TBLTSERRORCAUSE2LOC b";
                summaryTargetCondition = string.Format(@" and b.ELOC = '{0}'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Errorcomponent)
            {
                table = "TBLTSERRORCAUSE2COM d";
                summaryTargetCondition = string.Format(@" and d.Errorcomponent = '{0}'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                table = "TBLTSERRORCAUSE b";
                summaryTargetCondition = string.Format(@" and b.DUTYCODE = '{0}'", summaryObject);
            }
            if (table == "")
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null");
            }

#if DEBUG
            if (summaryTarget == TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                Log.Info(
                    new PagerCondition(
                    string.Format(sqlStr1,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(c.MDATE) AS RDATE,max(c.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE"),
                    "rcard", inclusive, exclusive, true).SQLText);
            }
            else
            {
                Log.Info(
                    new PagerCondition(
                    string.Format(sqlStr2,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE"),
                    "rcard", inclusive, exclusive, true).SQLText);
            }
#endif

            if (summaryTarget == TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                return this.DataProvider.CustomQuery(
                    typeof(QDOTSRecord),
                    new PagerCondition(
                    string.Format(sqlStr1,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(c.MDATE) AS RDATE,max(c.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE"),
                    "rcard", inclusive, exclusive, true));
            }

            return this.DataProvider.CustomQuery(
                typeof(QDOTSRecord),
                new PagerCondition(
                string.Format(sqlStr2,
                "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
                table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE"),
                "rcard", inclusive, exclusive, true));
        }



        public int QueryTSInfoListCount(
            string moCode,
            int startDate, int endDate,
            string summaryTarget, string summaryObject, string summaryObject1,
            string modelCodes, string itemCodes)
        {
            string modelCondition = string.Empty;
            if (modelCodes != null && modelCodes.Trim() != string.Empty)
            {
                modelCondition += string.Format(" and a.modelcode in({0})", FormatHelper.ProcessQueryValues(modelCodes));
            }

            string itemCondition = string.Empty;
            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                itemCondition += string.Format(" and a.itemcode in({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
            }

            string dateCondition = FormatHelper.GetDateRangeSql("a.SHIFTDAY", startDate, endDate);

            //ErrorCodeGroup,ErrorCode用sqlStr1
            string sqlStr1 = "select count(*) from (select distinct {0} from tblts a,tbltserrorcode b,{1} where a.tsid = b.tsid and a.tsid = c.tsid(+) {2}{3}{4}{5}{6} group by {7} )";
            //ErrorCause,ErrorLocation,Duty用sqlStr2
            string sqlStr2 = "select count(*) from (select distinct {0} from tblts a,{1} where a.tsid = b.tsid(+) {2}{3}{4}{5}{6} group by {7} )";
            string table = "";
            string summaryTargetCondition = "";
            if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                table = "TBLTSERRORCAUSE c";
                summaryTargetCondition = string.Format(@" and b.ECGCODE = '{0}' and b.ECODE = '{1}'", summaryObject, summaryObject1);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                table = "TBLTSERRORCAUSE b";
                summaryTargetCondition = string.Format(@" and b.ECSCODE = '{0}'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                table = "TBLTSERRORCAUSE c";
                summaryTargetCondition = string.Format(@" and b.ECGCODE = '{0}'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                table = "TBLTSERRORCAUSE2LOC b";
                summaryTargetCondition = string.Format(@" and b.ELOC = '{0}'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                table = "TBLTSERRORCAUSE b";
                summaryTargetCondition = string.Format(@" and b.DUTYCODE = '{0}'", summaryObject);
            }
            if (table == "")
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null");
            }

#if DEBUG
            if (summaryTarget == TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                Log.Info(
                    new SQLCondition(
                    string.Format(sqlStr1,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(c.MDATE) AS RDATE,max(c.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE")).SQLText);
            }
            else
            {
                Log.Info(
                    new SQLCondition(
                    string.Format(sqlStr2,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE")).SQLText);
            }
#endif

            if (summaryTarget == TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                return this.DataProvider.GetCount(
                    new SQLCondition(
                    string.Format(sqlStr1,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(c.MDATE) AS RDATE,max(c.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE")));
            }

            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(sqlStr2,
                "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
                table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE")));
        }

        #endregion

        public object[] QueryTSInfoList(
            string ecg, string ec, string ecs, string ecsg, string el, string ed,
            string moCode, string errorcomponent, string firstClassGroup, string secondClassGroup, string thirdClassGroup,
            int startDate, int endDate,
            string summaryTarget, string summaryObject, string summaryObject1,
            string modelCodes, string itemCodes, string frmResources, string lotNo,
            int inclusive, int exclusive)
        {
            string modelCondition = string.Empty;
            if (modelCodes != null && modelCodes.Trim() != string.Empty)
            {
                modelCondition += string.Format(" and g.mtype = '{0}'", modelCodes);
            }

            string itemCondition = string.Empty;
            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                if (itemCodes.Trim().IndexOf(",") >= 0)
                {
                    itemCondition += string.Format(" and a.itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCodes));
                }
                else
                {
                    itemCondition += string.Format(" and a.itemcode like '{0}%'", itemCodes);
                }
            }

            string moCondition = string.Empty;
            if (moCode != "" && moCode != null)
            {
                if (moCode.Trim().IndexOf(",") >= 0)
                {
                    moCondition += string.Format(" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
                }
                else
                {
                    moCondition += string.Format(" and a.mocode like '{0}%' ", moCode);
                }
            }

            string frmResCondition = string.Empty;
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                if (frmResources.Trim().IndexOf(",") >= 0)
                {
                    frmResCondition += string.Format(" and a.FRMRESCODE in ({0})", FormatHelper.ProcessQueryValues(frmResources));
                }
                else
                {
                    frmResCondition += string.Format(" and a.FRMRESCODE like '{0}%'", frmResources);
                }
            }

            if (lotNo != null && lotNo.Trim() != string.Empty)
            {
                if (lotNo.Trim().IndexOf(",") >= 0)
                {
                    frmResCondition += string.Format(" and a.rcard in (select rcard from tbllot2card where lotno in ({0}) ) ", FormatHelper.ProcessQueryValues(lotNo));
                }
                else
                {
                    frmResCondition += string.Format(" and a.rcard in (select rcard from tbllot2card where lotno like  '{0}%' ) ", lotNo);
                }
            }

            string dateCondition = FormatHelper.GetDateRangeSql("a.SHIFTDAY", startDate, endDate);

            string sqlStr = "select {0} from tblts a,tbltserrorcause b,tbltserrorcause2loc c,tbltserrorcause2com d,tblitem e,tblres f,tblmaterial g,tblitemclass h ";
            sqlStr += " where a.tsid=b.tsid(+) and b.tsid=c.tsid(+) and b.ecgcode=c.ecgcode(+) and b.ecode=c.ecode(+) and b.ecscode=c.ecscode(+)  and b.ecsgcode = c.ecsgcode(+) ";
            sqlStr += " and b.tsid=d.tsid(+) and b.ecgcode = d.ecgcode(+)  and b.ecode = d.ecode(+)  and b.ecscode = d.ecscode(+) and b.ecsgcode = d.ecsgcode(+)";
            sqlStr += " and a.itemcode=e.itemcode(+)  AND a.REFOPCODE=f.rescode(+)  AND a.itemcode=g.mcode(+)  AND g.mgroup=h.itemgroup(+) {8} {2}{3}{4}{5}{6} group by {7}";
            string table = "";
            string summaryTargetCondition = "";
            if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                summaryTargetCondition = string.Format(" and b.ECGCODE like '{0}%' and b.ECODE like '{1}%'", summaryObject, summaryObject1);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCauseGroup)
            {
                summaryTargetCondition = string.Format(" and b.ECSGCODE like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                summaryTargetCondition = string.Format(" and b.ECSCODE like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                summaryTargetCondition = string.Format(" and b.ECGCODE like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                summaryTargetCondition = string.Format(" and c.ELOC like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                summaryTargetCondition = string.Format(" and b.DUTYCODE like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Errorcomponent)
            {
                summaryTargetCondition = string.Format(" and d.Errorcomponent like '{0}%'", summaryObject);
            }

            if (ecg != null && ecg.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.ecgcode like '{0}%'", ecg);
            }

            if (ec != null && ec.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.ecode like '{0}%'", ec);
            }

            if (ecsg != null && ecsg.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.ecsgcode like '{0}%'", ecsg);
            }

            if (ecs != null && ecs.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.ecscode like '{0}%'", ecs);
            }

            if (el != null && el.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and c.eloc like '{0}%'", el);
            }

            if (ed != null && ed.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.dutycode like '{0}%'", ed);
            }

            if (errorcomponent != null && errorcomponent.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and d.Errorcomponent like '{0}%'", errorcomponent);
            }

            if (firstClassGroup != null && firstClassGroup.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and h.firstclass = '{0}'", firstClassGroup);
            }

            if (secondClassGroup != null && secondClassGroup.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and h.secondclass = '{0}'", secondClassGroup);
            }

            if (thirdClassGroup != null && thirdClassGroup.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and h.thirdclass = '{0}'", thirdClassGroup);
            }


            string sql = string.Format(sqlStr,
                @"a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE, A.ITEMCODE || ' - ' || e.itemdesc AS ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.rcardseq,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tsdate)
						when 'tsstatus_split'
						then max(a.tsdate)
						when 'tsstatus_reflow' 
						then max(a.tsdate) 
						else 
						MAX (c.mdate) 
						end as rdate,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tstime)
						when 'tsstatus_split'
						then max(a.tstime)
						when 'tsstatus_reflow' 
						then max(a.tstime) 
						else 
						MAX (c.mtime) 
						end as rtime,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tsrescode) 
						when 'tsstatus_split'
						then max(a.tsrescode) 
						when 'tsstatus_reflow' 
						then max(a.tsrescode) 
						else 
						max(c.rrescode )
						end as rrescode,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tsuser) 
						when 'tsstatus_split'
						then max(a.tsuser) 
						when 'tsstatus_reflow' 
						then max(a.tsuser) 
						else 
						max(c.muser )
						end as tsuser,a.FRMDATE,a.FRMTIME,A.REFOPCODE || ' - ' || f.resdesc AS REFRESCODE,a.refopcode",
                table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                "a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,a.refopcode,a.RCARDSEQ,e.itemdesc,f.resdesc,a.refopcode",
                frmResCondition);

            string selectCondition = "SELECT e.TSID,e.RCARD, e.TSSTATUS,e.MOCODE,e.MODELCODE,e.ITEMCODE,e.TSDATE,e.TSTIME,e.FRMRESCODE || ' - ' || tblres.resdesc AS FRMRESCODE, e.RCARDSEQ,e.RDATE, e.RTIME,e.RRESCODE,e.TSUSER || ' - ' || tbluser.username AS TSUSER,e.FRMDATE,e.FRMTIME, e.REFRESCODE,e.refopcode FROM (";

            sql = selectCondition + sql + ") e LEFT OUTER JOIN tblres ON e.FRMRESCODE=tblres.rescode  LEFT OUTER JOIN tbluser ON e.TSUSER=tbluser.usercode ";

            string selectColunms = "SELECT e.TSID,e.RCARD, e.TSSTATUS,e.MOCODE,e.MODELCODE,e.ITEMCODE,e.TSDATE,e.TSTIME,e.FRMRESCODE, e.RCARDSEQ,e.RDATE, e.RTIME,e.RRESCODE || ' - ' || tblres.resdesc AS RRESCODE,e.TSUSER,e.FRMDATE,e.FRMTIME, e.REFRESCODE,e.refopcode || ' - ' || tblop.opdesc as refopcode FROM (";

            sql = selectColunms + sql + ") e LEFT OUTER JOIN tblres ON e.RRESCODE=tblres.rescode left outer join tblop on e.refopcode=tblop.opcode ";


            return this.DataProvider.CustomQuery(
                typeof(QDOTSRecord),
                new PagerCondition(
                sql,
                "rcard", inclusive, exclusive, true));
        }

        #region OLD Code joe 修改之前的版本 20060717 修改内容:查询条件中加不良代码组,不良代码,不良原因,不良位置,责任
        //		public object[] QueryTSInfoList(
        //			string ecg,string ec,string ecs,string loc,string duty,
        //			string moCode,
        //			int startDate,int endDate,
        //			string summaryTarget,string summaryObject,string summaryObject1,
        //			string modelCodes , string itemCodes,string frmResources,
        //			int inclusive,int exclusive)
        //		{
        //			string modelCondition = string.Empty ;
        //			if ( modelCodes != null && modelCodes.Trim() != string.Empty )
        //			{
        //				modelCondition += string.Format( " and a.modelcode in({0})", FormatHelper.ProcessQueryValues(modelCodes) );
        //			}
        //
        //			string itemCondition = string.Empty ;
        //			if ( itemCodes != null && itemCodes.Trim() != string.Empty )
        //			{
        //				itemCondition += string.Format( " and a.itemcode in({0})", FormatHelper.ProcessQueryValues(itemCodes) );
        //			}
        //
        //			string moCondition = "";
        //			if( moCode != "" && moCode != null )
        //			{
        //				moCondition += string.Format( @" and a.mocode in ({0}) ",FormatHelper.ProcessQueryValues( moCode ));
        //			}
        //			
        //			string frmResCondition = string.Empty;
        //			if( frmResources != null && frmResources.Trim() != string.Empty )
        //			{
        //				frmResCondition += string.Format( " and a.FRMRESCODE in({0})", FormatHelper.ProcessQueryValues(frmResources) );
        //			}
        //
        //			string dateCondition = FormatHelper.GetDateRangeSql("a.SHIFTDAY",startDate,endDate);
        //			
        //			//ErrorCodeGroup,ErrorCode用sqlStr1
        //			//string sqlStr1 = "select  {0} from tblts a,tbltserrorcode b,{1} where a.tsid = b.tsid and a.tsid = c.tsid(+) {8} {2}{3}{4}{5}{6} group by {7} ";
        //			//ErrorCause,ErrorLocation,Duty用sqlStr2
        //			//string sqlStr2 = "select  {0} from tblts a,{1} where a.tsid = b.tsid(+) {8} {2}{3}{4}{5}{6} group by {7} ";
        //			string sql = "select {0} from tblts a,tbltserrorcause b,tbltserrorcause2loc c where a.tsid=b.tsid(+) and a.tsid=c.tsid(+) {8} {2}{3}{4}{5}{6} group by {7}";  
        //			string table = "";
        //			string summaryTargetCondition = "";
        //			if( summaryTarget == TSInfoSummaryTarget.ErrorCode )
        //			{			
        //				table = "TBLTSERRORCAUSE c";
        //				summaryTargetCondition = string.Format( @" and b.ECGCODE = '{0}' and b.ECODE = '{1}'",summaryObject,summaryObject1);
        //			}
        //			else if( summaryTarget == TSInfoSummaryTarget.ErrorCause )
        //			{
        //				table = "TBLTSERRORCAUSE b";
        //				summaryTargetCondition = string.Format( @" and b.ECSCODE = '{0}'",summaryObject);
        //			}
        //			else if( summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup )
        //			{
        //				table = "TBLTSERRORCAUSE c";
        //				summaryTargetCondition = string.Format( @" and b.ECGCODE = '{0}'",summaryObject);
        //			}
        //			else if( summaryTarget == TSInfoSummaryTarget.ErrorLocation )
        //			{
        //				table = "TBLTSERRORCAUSE2LOC b";
        //				summaryTargetCondition = string.Format( @" and b.ELOC = '{0}'",summaryObject);
        //			}
        //			else if( summaryTarget == TSInfoSummaryTarget.Duty )
        //			{
        //				table = "TBLTSERRORCAUSE b";
        //				summaryTargetCondition = string.Format( @" and b.DUTYCODE = '{0}'",summaryObject);
        //			}
        //			if( table == "" )
        //			{
        //				ExceptionManager.Raise(this.GetType(),"$Error_Argument_Null");
        //			}
        //
        //#if DEBUG
        //			if( summaryTarget==TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup )
        //			{
        //				Log.Info(
        //					new PagerCondition(
        //					string.Format(sqlStr1,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(c.MDATE) AS RDATE,max(c.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
        //					table,modelCondition,itemCondition, moCondition,dateCondition,summaryTargetCondition,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
        //					frmResCondition),
        //					"rcard",inclusive,exclusive,true).SQLText);
        //			}
        //			else
        //			{
        //				Log.Info(
        //					new PagerCondition(
        //					string.Format(sqlStr2,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
        //					table,modelCondition,itemCondition,moCondition,dateCondition,summaryTargetCondition,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
        //					frmResCondition),
        //					"rcard",inclusive,exclusive,true).SQLText);
        //			}
        //#endif
        //
        //			string sql  = string.Empty;
        //			if( summaryTarget==TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup )
        //			{
        //				sql = string.Format(sqlStr1,
        //					@"a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.rcardseq,
        //					case TSSTATUS
        //						when 'tsstatus_scrap'
        //						then max(a.tsdate)
        //						when 'tsstatus_split'
        //						then max(a.tsdate)
        //						else 
        //						MAX (c.mdate) 
        //						end as rdate,
        //					case TSSTATUS
        //						when 'tsstatus_scrap'
        //						then max(a.tstime)
        //						when 'tsstatus_split'
        //						then max(a.tstime)
        //						else 
        //						MAX (c.mtime) 
        //						end as rtime,
        //					case TSSTATUS
        //						when 'tsstatus_scrap'
        //						then max(a.tsrescode) 
        //						when 'tsstatus_split'
        //						then max(a.tsrescode) 
        //						else 
        //						max(c.rrescode )
        //						end as rrescode,
        //					case TSSTATUS
        //						when 'tsstatus_scrap'
        //						then max(a.tsuser) 
        //						when 'tsstatus_split'
        //						then max(a.tsuser) 
        //						else 
        //						max(c.muser )
        //						end as tsuser,a.FRMDATE,a.FRMTIME,a.REFRESCODE",
        //					table,modelCondition,itemCondition,moCondition,dateCondition,summaryTargetCondition,
        //					"a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,a.REFRESCODE,a.RCARDSEQ",
        //					frmResCondition);
        //				return this.DataProvider.CustomQuery(
        //					typeof(QDOTSRecord),
        //					new PagerCondition(
        //					sql,
        //					"rcard",inclusive,exclusive,true));
        //			}
        //
        //			sql = string.Format(sqlStr2,
        //				@"a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.rcardseq,
        //						CASE tsstatus
        //						WHEN 'tsstatus_scrap'
        //							THEN MAX (a.tsdate)
        //						WHEN 'tsstatus_split'
        //							THEN MAX (a.tsdate)
        //						ELSE MAX (b.mdate)
        //						END AS rdate,
        //						CASE tsstatus
        //						WHEN 'tsstatus_scrap'
        //							THEN MAX (a.tstime)
        //						WHEN 'tsstatus_split'
        //							THEN MAX (a.tstime)
        //						ELSE MAX (b.mtime)
        //						END AS rtime,
        //						CASE tsstatus
        //						WHEN 'tsstatus_scrap'
        //							THEN MAX (a.tsrescode)
        //						WHEN 'tsstatus_split'
        //							THEN MAX (a.tsrescode)
        //						ELSE MAX (b.rrescode)
        //						END AS rrescode,
        //						CASE tsstatus
        //						WHEN 'tsstatus_scrap'
        //							THEN MAX (a.tsuser)
        //						WHEN 'tsstatus_split'
        //							THEN MAX (a.tsuser)
        //						ELSE MAX (b.muser)
        //						END AS tsuser,
        //				a.FRMDATE,a.FRMTIME,a.REFRESCODE",
        //				table,modelCondition,itemCondition,moCondition,dateCondition,summaryTargetCondition,
        //				"a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,a.REFRESCODE,a.RCARDSEQ",
        //				frmResCondition);
        //			return this.DataProvider.CustomQuery(
        //				typeof(QDOTSRecord),
        //				new PagerCondition(
        //				sql,
        //				"rcard",inclusive,exclusive,true));
        //		}


        //		public int QueryTSInfoListCount(
        //			string moCode,
        //			int startDate,int endDate,
        //			string summaryTarget,string summaryObject,string summaryObject1,
        //			string modelCodes , string itemCodes,string frmResources)
        //		{
        //			string modelCondition = string.Empty ;
        //			if ( modelCodes != null && modelCodes.Trim() != string.Empty )
        //			{
        //				modelCondition += string.Format( " and a.modelcode in({0})", FormatHelper.ProcessQueryValues(modelCodes) );
        //			}
        //
        //			string itemCondition = string.Empty ;
        //			if ( itemCodes != null && itemCodes.Trim() != string.Empty )
        //			{
        //				itemCondition += string.Format( " and a.itemcode in({0})", FormatHelper.ProcessQueryValues(itemCodes) );
        //			}
        //
        //			string moCondition = "";
        //			if( moCode != "" && moCode != null )
        //			{
        //				moCondition += string.Format( @" and a.mocode in ({0}) ",FormatHelper.ProcessQueryValues( moCode ));
        //			}
        //
        //			string frmResCondition = string.Empty;
        //			if( frmResources != null && frmResources.Trim() != string.Empty )
        //			{
        //				frmResCondition += string.Format( " and a.FRMRESCODE in({0})", FormatHelper.ProcessQueryValues(frmResources) );
        //			}
        //
        //			string dateCondition = FormatHelper.GetDateRangeSql("a.SHIFTDAY",startDate,endDate);
        //	
        //			//ErrorCodeGroup,ErrorCode用sqlStr1
        //			string sqlStr1 = "select count(*) from (select distinct {0} from tblts a,tbltserrorcode b,{1} where a.tsid = b.tsid and a.tsid = c.tsid(+) {8} {2}{3}{4}{5}{6} group by {7} )";
        //			//ErrorCause,ErrorLocation,Duty用sqlStr2
        //			string sqlStr2 = "select count(*) from (select distinct {0} from tblts a,{1} where a.tsid = b.tsid(+) {8} {2}{3}{4}{5}{6} group by {7} )";
        //			string table = "";
        //			string summaryTargetCondition = "";
        //			if( summaryTarget == TSInfoSummaryTarget.ErrorCode )
        //			{			
        //				table = "TBLTSERRORCAUSE c";
        //				summaryTargetCondition = string.Format( @" and b.ECGCODE = '{0}' and b.ECODE = '{1}'",summaryObject,summaryObject1);
        //			}
        //			else if( summaryTarget == TSInfoSummaryTarget.ErrorCause )
        //			{
        //				table = "TBLTSERRORCAUSE b";
        //				summaryTargetCondition = string.Format( @" and b.ECSCODE = '{0}'",summaryObject);
        //			}
        //			else if( summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup )
        //			{
        //				table = "TBLTSERRORCAUSE c";
        //				summaryTargetCondition = string.Format( @" and b.ECGCODE = '{0}'",summaryObject);
        //			}
        //			else if( summaryTarget == TSInfoSummaryTarget.ErrorLocation )
        //			{
        //				table = "TBLTSERRORCAUSE2LOC b";
        //				summaryTargetCondition = string.Format( @" and b.ELOC = '{0}'",summaryObject);
        //			}
        //			else if( summaryTarget == TSInfoSummaryTarget.Duty )
        //			{
        //				table = "TBLTSERRORCAUSE b";
        //				summaryTargetCondition = string.Format( @" and b.DUTYCODE = '{0}'",summaryObject);
        //			}
        //			if( table == "" )
        //			{
        //				ExceptionManager.Raise(this.GetType(),"$Error_Argument_Null");
        //			}
        //
        //#if DEBUG
        //			if( summaryTarget==TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup )
        //			{
        //				Log.Info(
        //					new SQLCondition(
        //					string.Format(sqlStr1,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(c.MDATE) AS RDATE,max(c.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
        //					table,modelCondition,itemCondition,moCondition,dateCondition,summaryTargetCondition,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",frmResCondition)).SQLText);
        //			}
        //			else
        //			{
        //				Log.Info(
        //					new SQLCondition(
        //					string.Format(sqlStr2,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
        //					table,modelCondition,itemCondition,moCondition,dateCondition,summaryTargetCondition,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",frmResCondition)).SQLText);
        //			}
        //#endif
        //
        //			if( summaryTarget==TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup )
        //			{
        //				return this.DataProvider.GetCount(
        //					new SQLCondition(
        //					string.Format(sqlStr1,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(c.MDATE) AS RDATE,max(c.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
        //					table,modelCondition,itemCondition,moCondition,dateCondition,summaryTargetCondition,
        //					"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",frmResCondition)));
        //			}
        //
        //			return this.DataProvider.GetCount(
        //				new SQLCondition(
        //				string.Format(sqlStr2,
        //				"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
        //				table,modelCondition,itemCondition,moCondition,dateCondition,summaryTargetCondition,
        //				"a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",frmResCondition)));
        //		}
        #endregion
        public int QueryTSInfoListCount(
            string ecg, string ec, string ecs, string ecsg, string el, string ed,
            string moCode, string errorcomponent, string firstClassGroup, string secondClassGroup, string thirdClassGroup,
            int startDate, int endDate,
            string summaryTarget, string summaryObject, string summaryObject1,
            string modelCodes, string itemCodes, string frmResources, string lotNo)
        {
            string modelCondition = string.Empty;
            if (modelCodes != null && modelCodes.Trim() != string.Empty)
            {
                modelCondition += string.Format(" and g.mtype = '{0}'", modelCodes);
            }

            string itemCondition = string.Empty;
            if (itemCodes != null && itemCodes.Trim() != string.Empty)
            {
                if (itemCodes.Trim().IndexOf(",") >= 0)
                {
                    itemCondition += string.Format(" and a.itemcode in({0})", FormatHelper.ProcessQueryValues(itemCodes));
                }
                else
                {
                    itemCondition += string.Format(" and a.itemcode like '{0}%'", itemCodes);
                }
            }

            string moCondition = string.Empty;
            if (moCode != "" && moCode != null)
            {
                if (moCode.Trim().IndexOf(",") >= 0)
                {
                    moCondition += string.Format(" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
                }
                else
                {
                    moCondition += string.Format(" and a.mocode like '{0}%' ", moCode);
                }
            }

            string frmResCondition = string.Empty;
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                if (frmResources.Trim().IndexOf(",") >= 0)
                {
                    frmResCondition += string.Format(" and a.FRMRESCODE in ({0})", FormatHelper.ProcessQueryValues(frmResources));
                }
                else
                {
                    frmResCondition += string.Format(" and a.FRMRESCODE like '{0}%'", frmResources);
                }
            }


            if (lotNo != null && lotNo.Trim() != string.Empty)
            {
                if (lotNo.Trim().IndexOf(",") >= 0)
                {
                    frmResCondition += string.Format(" and a.rcard in (select rcard from tbllot2card where lotno in ({0}) ) ", FormatHelper.ProcessQueryValues(lotNo));
                }
                else
                {
                    frmResCondition += string.Format(" and a.rcard in (select rcard from tbllot2card where lotno like '{0}%' ) ", lotNo);
                }
            }

            string dateCondition = FormatHelper.GetDateRangeSql("a.SHIFTDAY", startDate, endDate);

            string sqlStr = "select {0} from tblts a,tbltserrorcause b,tbltserrorcause2loc c,tbltserrorcause2com d,tblitem e,tblres f,tblmaterial g,tblitemclass h ";
            sqlStr += " where a.tsid=b.tsid(+) and b.tsid=c.tsid(+) and b.ecgcode=c.ecgcode(+) and b.ecode=c.ecode(+) and b.ecscode=c.ecscode(+)  and b.ecsgcode = c.ecsgcode(+) ";
            sqlStr += " and b.tsid=d.tsid(+) and b.ecgcode = d.ecgcode(+)  and b.ecode = d.ecode(+)  and b.ecscode = d.ecscode(+) and b.ecsgcode = d.ecsgcode(+)";
            sqlStr += " and a.itemcode=e.itemcode(+)  AND a.REFOPCODE=f.rescode(+) AND a.itemcode=g.mcode(+)  AND g.mgroup=h.itemgroup(+) {8} {2}{3}{4}{5}{6} group by {7}";
            string table = "";
            string summaryTargetCondition = "";
            if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                summaryTargetCondition = string.Format(@" and b.ECGCODE like '{0}%' and b.ECODE like '{1}%'", summaryObject, summaryObject1);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCauseGroup)
            {
                summaryTargetCondition = string.Format(@" and b.ECSGCODE like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                summaryTargetCondition = string.Format(@" and b.ECSCODE like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                summaryTargetCondition = string.Format(@" and b.ECGCODE like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                summaryTargetCondition = string.Format(@" and c.ELOC like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                summaryTargetCondition = string.Format(@" and b.DUTYCODE like '{0}%'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Errorcomponent)
            {
                summaryTargetCondition = string.Format(@" and d.Errorcomponent like '{0}%'", summaryObject);
            }

            if (ecg != null && ecg.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.ecgcode like '{0}%'", ecg);
            }

            if (ec != null && ec.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.ecode like '{0}%'", ec);
            }

            if (ecsg != null && ecsg.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.ecsgcode like '{0}%'", ecsg);
            }

            if (ecs != null && ecs.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.ecscode like '{0}%'", ecs);
            }

            if (el != null && el.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and c.eloc like '{0}%'", el);
            }

            if (ed != null && ed.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and b.dutycode like '{0}%'", ed);
            }

            if (errorcomponent != null && errorcomponent.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and d.Errorcomponent like  '{0}%'", errorcomponent);
            }

            if (firstClassGroup != null && firstClassGroup.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and h.firstclass = '{0}'", firstClassGroup);
            }

            if (secondClassGroup != null && secondClassGroup.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and h.secondclass = '{0}'", secondClassGroup);
            }

            if (thirdClassGroup != null && thirdClassGroup.Trim() != string.Empty)
            {
                summaryTargetCondition += string.Format(" and h.thirdclass = '{0}'", thirdClassGroup);
            }


            string sql = string.Format(sqlStr,
                @"a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE, A.ITEMCODE || ' - ' || e.itemdesc AS ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.rcardseq,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tsdate)
						when 'tsstatus_split'
						then max(a.tsdate)
						when 'tsstatus_reflow' 
						then max(a.tsdate) 
						else 
						MAX (c.mdate) 
						end as rdate,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tstime)
						when 'tsstatus_split'
						then max(a.tstime)
						when 'tsstatus_reflow' 
						then max(a.tstime) 
						else 
						MAX (c.mtime) 
						end as rtime,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tsrescode) 
						when 'tsstatus_split'
						then max(a.tsrescode) 
						when 'tsstatus_reflow' 
						then max(a.tsrescode) 
						else 
						max(c.rrescode )
						end as rrescode,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tsuser) 
						when 'tsstatus_split'
						then max(a.tsuser) 
						when 'tsstatus_reflow' 
						then max(a.tsuser) 
						else 
						max(c.muser )
						end as tsuser,a.FRMDATE,a.FRMTIME,A.REFOPCODE || ' - ' || f.resdesc AS REFRESCODE",
                table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                "a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,a.refopcode,a.RCARDSEQ,e.itemdesc,f.resdesc",
                frmResCondition);

            string selectCondition = "SELECT e.TSID,e.RCARD, e.TSSTATUS,e.MOCODE,e.MODELCODE,e.ITEMCODE,e.TSDATE,e.TSTIME,e.FRMRESCODE || ' - ' || tblres.resdesc AS FRMRESCODE, e.RCARDSEQ,e.RDATE, e.RTIME,e.RRESCODE,e.TSUSER || ' - ' || tbluser.username AS TSUSER,e.FRMDATE,e.FRMTIME, e.REFRESCODE FROM (";

            sql = selectCondition + sql + ") e LEFT OUTER JOIN tblres ON e.FRMRESCODE=tblres.rescode  LEFT OUTER JOIN tbluser ON e.TSUSER=tbluser.usercode ";

            string selectColunms = "SELECT e.TSID,e.RCARD, e.TSSTATUS,e.MOCODE,e.MODELCODE,e.ITEMCODE,e.TSDATE,e.TSTIME,e.FRMRESCODE, e.RCARDSEQ,e.RDATE, e.RTIME,e.RRESCODE || ' - ' || tblres.resdesc AS RRESCODE,e.TSUSER,e.FRMDATE,e.FRMTIME, e.REFRESCODE FROM (";

            sql = selectColunms + sql + ") e LEFT OUTER JOIN tblres ON e.RRESCODE=tblres.rescode  ";
            sql = "select count(*) from (" + sql + ")";


            return this.DataProvider.GetCount(new SQLCondition(sql));


        }
        //导出维修资料统计(包含维修明细)
        public object[] ExportQueryTSInfoList(
            string ecg, string ec, string ecs, string ecsg, string el, string ed,
            string moCode, string errorcomponent, string firstClassGroup, string secondClassGroup, string thirdClassGroup,
            int startDate, int endDate,
            string summaryTarget, string summaryObject, string summaryObject1,
            string modelCodes, string itemCodes, string frmResources, string lotNo,
            int inclusive, int exclusive)
        {
            object[] tsObjs = this.QueryTSInfoList(ecg, ec, ecs, ecsg, el, ed, moCode, errorcomponent, firstClassGroup, secondClassGroup, thirdClassGroup,
                                                   startDate, endDate, summaryTarget, summaryObject, summaryObject1, modelCodes, itemCodes, frmResources, lotNo, inclusive, exclusive);
            ArrayList returnObjs = new ArrayList();

            ArrayList tsidList = new ArrayList();
            foreach (QDOTSRecord qts in tsObjs)
            {
                tsidList.Add(qts.TSID);
            }
            string tsIDs = "('" + String.Join("','", (string[])tsidList.ToArray(typeof(string))) + "')";

            if (tsObjs != null && tsObjs.Length > 0)
            {
                QueryTSDetailsFacade tsDetailFacade = new QueryTSDetailsFacade(this.DataProvider);
                object[] tsDetailObjs = tsDetailFacade.QueryTSDetailsByID(tsIDs, 0, int.MaxValue);

                if (tsDetailObjs != null && tsDetailObjs.Length > 0)
                {
                    foreach (QDOTSRecord _ts in tsObjs)
                    {
                        foreach (QDOTSDetails1 tsdetail in tsDetailObjs)
                        {
                            if (tsdetail.TSID == _ts.TSID)
                            {
                                ExportQDOTSDetails1 exportTSDetails = new ExportQDOTSDetails1();
                                //映射明细
                                exportTSDetails.Memo = tsdetail.Memo;
                                exportTSDetails.TSOperator = tsdetail.TSOperator;
                                exportTSDetails.TSDate = tsdetail.TSDate;
                                exportTSDetails.TSTime = tsdetail.TSTime;
                                exportTSDetails.ErrorCodeGroup = tsdetail.ErrorCodeGroup;
                                exportTSDetails.ErrorCodeGroupDescription = tsdetail.ErrorCodeGroupDescription;
                                exportTSDetails.ErrorCode = tsdetail.ErrorCode;
                                exportTSDetails.ErrorCodeDescription = tsdetail.ErrorCodeDescription;
                                exportTSDetails.ErrorCauseCode = tsdetail.ErrorCauseCode;
                                exportTSDetails.ErrorCauseDescription = tsdetail.ErrorCauseDescription;
                                exportTSDetails.ErrorLocation = tsdetail.ErrorLocation;
                                exportTSDetails.ErrorParts = tsdetail.ErrorParts;
                                exportTSDetails.Solution = tsdetail.SolutionCode;
                                exportTSDetails.Duty = tsdetail.Duty;
                                exportTSDetails.DutyDescription = tsdetail.DutyDescription;
                                exportTSDetails.SolutionDescription = tsdetail.SolutionDescription;
                                exportTSDetails.ErrorCauseGroupCode = tsdetail.ErrorCauseGroupCode;
                                //映射主档
                                exportTSDetails.SN = _ts.SN;
                                exportTSDetails.TsState = _ts.TsState;
                                exportTSDetails.MoCode = _ts.MoCode;
                                exportTSDetails.ModelCode = _ts.ModelCode;
                                exportTSDetails.ItemCode = _ts.ItemCode;
                                exportTSDetails.NGCount = _ts.NGCount;
                                exportTSDetails.NGDate = _ts.NGDate;
                                exportTSDetails.NGTime = _ts.NGTime;
                                exportTSDetails.SourceResource = _ts.SourceResource;
                                exportTSDetails.SourceResourceDate = _ts.SourceResourceDate;
                                exportTSDetails.SourceResourceTime = _ts.SourceResourceTime;
                                exportTSDetails.RepaireResource = _ts.RepaireResource;
                                exportTSDetails.RepaireDate = _ts.RepaireDate;
                                exportTSDetails.RepaireTime = _ts.RepaireTime;
                                exportTSDetails.DestResource = _ts.DestResource;
                                exportTSDetails.DestOpCode = _ts.DestOpCode;
                                exportTSDetails.FrmUser = _ts.FrmUser;
                                exportTSDetails.TSUser = _ts.TSUser;
                                exportTSDetails.ConfirmResource = _ts.ConfirmResource;
                                exportTSDetails.ConfirmUser = _ts.ConfirmUser;
                                exportTSDetails.ConfirmDate = _ts.ConfirmDate;
                                exportTSDetails.ConfiemTime = _ts.ConfiemTime;
                                exportTSDetails.DestResourceDate = _ts.DestResourceDate;
                                exportTSDetails.DestResourceTime = _ts.DestResourceTime;

                                returnObjs.Add(exportTSDetails);
                            }
                        }
                    }
                }
            }
            return (ExportQDOTSDetails1[])returnObjs.ToArray(typeof(ExportQDOTSDetails1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object[] QueryTSLocECode(string itemCode, string moCode, int startDate, int endDate, int inclusive, int exclusive)
        {
            string strSql = "select loc.eloc as errorlocation,tblecs.ecsdesc,count(*) as qty from tbltserrorcause2loc loc ";
            strSql += " left join tblecs on tblecs.ecscode=loc.ecscode ";
            strSql += " where 1=1 ";
            if (itemCode != string.Empty)
                strSql += string.Format(" and itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCode));
            if (moCode != string.Empty)
                strSql += string.Format(" and mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
            if (startDate > 0)
                strSql += string.Format(" and loc.mdate>={0} ", startDate);
            if (endDate > 0)
                strSql += string.Format(" and loc.mdate<={0} ", endDate);
            strSql += " group by loc.eloc,tblecs.ecsdesc ";

            return this.DataProvider.CustomQuery(typeof(QDOTSInfo), new SQLCondition(strSql));
        }

        /// <summary>
        /// 维修不良率查询中的包装数
        /// </summary>
        /// <returns></returns>
        public object[] QueryTSECodeAnalyseOutput(string modelCode, string ssCode, string errorGroupCode, string errorCode, int startDate, int endDate, string dateGroup)
        {
            //机种
            string modelCodition = string.Empty;
            if (modelCode != "" && modelCode != null)
            {
                modelCodition = string.Format(
                    @" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            //产线
            string ssCodition = string.Empty;
            if (ssCode != "" && ssCode != null)
            {
                ssCodition = string.Format(
                    @" and sscode in ({0})", FormatHelper.ProcessQueryValues(ssCode));
            }

            //包装日期
            string dateCondition = string.Empty;
            if (startDate > 0)
                dateCondition = string.Format(" and mdate>={0} ", startDate);
            if (endDate > 0)
                dateCondition += string.Format(" and mdate<={0} ", endDate);

            //查询条件
            string sqlCondition = modelCodition + ssCodition + dateCondition;

            //查询字段
            string sqlSelect = string.Empty;
            if (dateGroup != "" && dateGroup != null)
            {
                sqlSelect = string.Format(@" ModelCode,COUNT(*) Qty, {0} as dategroup", dateGroup);
            }

            //GroupBy 条件
            string sqlGroupByCondition = string.Empty;
            if (dateGroup != "" && dateGroup != null)
            {
                sqlGroupByCondition = string.Format("GROUP BY modelcode, {0}", dateGroup);
            }

            // OrderBy
            string orderBy = "modelcode," + dateGroup;

            string sql = string.Format(
                @"SELECT  {0}
					FROM (SELECT ModelCode,MDate, 
								TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'),'yyyyww') AS WEEK,
								TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'), 'yyyyMM') AS MONTH 
							FROM tblLot2Card 
						WHERE 1=1 {1})
				{2} ORDER BY {3}", sqlSelect, sqlCondition, sqlGroupByCondition, orderBy);

            return this.DataProvider.CustomQuery(typeof(QDOTSErrorCodeAnalyse), new SQLCondition(sql));
        }

        /// <summary>
        /// 维修不良率查询
        /// </summary>
        /// <returns></returns>
        public object[] QueryTSECodeAnalyse(string modelCode, string ssCode, string errorGroupCode, string errorCode, int startDate, int endDate, string dateGroup, int inclusive, int exclusive)
        {
            //机种
            string modelCodition = string.Empty;
            if (modelCode != "" && modelCode != null)
            {
                modelCodition = string.Format(
                    @" and ts.modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            //产线
            string ssCodition = string.Empty;
            if (ssCode != "" && ssCode != null)
            {
                ssCodition = string.Format(
                    @" and ts.frmsscode in ({0})", FormatHelper.ProcessQueryValues(ssCode));
            }

            // 不良代码组
            string errGroupCondition = string.Empty;
            if (errorGroupCode != string.Empty)
                errGroupCondition = string.Format(@" and err.ecgcode like '{0}%' ", errorGroupCode);

            // 不良代码
            string errCodeCondition = string.Empty;
            if (errorCode != string.Empty)
                errCodeCondition = string.Format(@" and err.ecode like '{0}%' ", errorCode);

            //日期
            string dateCondition = string.Empty;
            if (startDate > 0)
                dateCondition = string.Format(" and ts.frmdate>={0} ", startDate);
            if (endDate > 0)
                dateCondition += string.Format(" and ts.frmdate<={0} ", endDate);

            //过滤不良品重复采集的数据
            string strUndoNGCondition = string.Format(" and ts.tsstatus<>'{0}' ", TSStatus.TSStatus_RepeatNG);

            //查询条件
            string sqlCondition = modelCodition + ssCodition + errGroupCondition + errCodeCondition + dateCondition + strUndoNGCondition;

            //查询字段
            string sqlSelect = string.Empty;
            if (dateGroup != "" && dateGroup != null)
            {
                sqlSelect = string.Format(@" ModelCode,ECGCode,ECode,ECGDesc,ECDesc,COUNT(*) Qty, {0} as dategroup", dateGroup);
            }

            //GroupBy 条件
            string sqlGroupByCondition = string.Empty;
            if (dateGroup != "" && dateGroup != null)
            {
                sqlGroupByCondition = string.Format("GROUP BY ECGCode,ECGDesc,ModelCode,ECode,ECDesc, {0}", dateGroup);
            }

            // OrderBy
            string orderBy = string.Format("ECGCode,ECode,{0},ModelCode", dateGroup);

            string sql = string.Format(
                @"SELECT  {0}
					FROM (SELECT ModelCode,err.ECGCode,err.ECode,ECGDesc,ECDesc,err.MDate,
								TO_CHAR (TO_DATE (err.mdate, 'yyyyMMdd'),'yyyyww') AS WEEK,
								TO_CHAR (TO_DATE (err.mdate, 'yyyyMMdd'), 'yyyyMM') AS MONTH 
							FROM tblEC,tblECG,
								(SELECT ts.ModelCode,err.ECGCode,err.ECode,ts.frmdate as mdate FROM tblTS ts,tblTSErrorCode err 
									WHERE ts.tsid=err.tsid {1} ) err 
						WHERE err.ECode=tblEC.ECode AND err.ECGCode=tblECG.ECGCode )
				{2} ORDER BY {3}", sqlSelect, sqlCondition, sqlGroupByCondition, orderBy);

            return this.DataProvider.CustomQuery(typeof(QDOTSErrorCodeAnalyse), new SQLCondition(sql));
        }

    }
}
