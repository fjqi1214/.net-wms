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
    /// QueryRMATSFacade 的摘要说明。
    /// </summary>
    public class QueryRMATSFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public QueryRMATSFacade(IDomainDataProvider domainDataProvider)
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

        #region RMA维修记录查询
        public object[] QueryRMATSRecord(
            string modelCodes,
            string itemCodes,
            string reworkMOCodes,
            string rmaBillCodes,
            int receiveStartDate, int receiveStartTime,
            int receiveEndDate, int receiveEndTime,
            ArrayList tsState,
            int inclusive, int exclusive)
        {
            string modelCondition = "";
            if (modelCodes != "" && modelCodes != null)
            {
                modelCondition += string.Format(@" and a.modelcode in ({0}) ", FormatHelper.ProcessQueryValues(modelCodes));
            }

            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition += string.Format(@" and a.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCodes));
            }

            if (rmaBillCodes != null && rmaBillCodes.Trim() != string.Empty)
            {
                itemCondition += string.Format(" and a.rmabillcode in({0})", FormatHelper.ProcessQueryValues(rmaBillCodes));
            }

            //返工工单条件
            string reworkCondition = string.Empty;
            if (reworkMOCodes != "" && reworkMOCodes != null)
            {
                reworkCondition = string.Format(" and a.mocode in ( {0})", FormatHelper.ProcessQueryValues(reworkMOCodes));
            }

            string receivedateCondition = FormatHelper.GetDateRangeSql("a.CONFIRMDATE", "a.CONFIRMTIME", receiveStartDate, receiveStartTime, receiveEndDate, receiveEndTime);

            string tsStateCondition1 = string.Empty;
            string tsStateCondition2 = string.Empty;
            if (tsState == null)
            {
                tsStateCondition1 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}')",
                    TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow, TSStatus.TSStatus_TS);

                tsStateCondition2 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}','{3}')",
                    TSStatus.TSStatus_New, TSStatus.TSStatus_Confirm, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split, TSStatus.TSStatus_OffMo);
            }
            if (tsState != null)
            {
                if (tsState.Count > 0)
                {
                    foreach (string state in tsState)
                    {
                        if (string.Compare(state, TSStatus.TSStatus_Complete, true) == 0)
                        {
                            tsStateCondition1 += string.Format("'{0}','{1}',", TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow);
                        }
                        else if (string.Compare(state, TSStatus.TSStatus_TS, true) == 0)
                        {
                            tsStateCondition1 += string.Format(@"'{0}',", state);
                        }
                        else if (string.Compare(state, TSStatus.TSStatus_New, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_Confirm, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_Split, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_Scrap, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_OffMo, true) == 0)
                        {
                            tsStateCondition2 += string.Format(@"'{0}',", state);
                        }

                    }

                    if (tsStateCondition1.Length > 0)
                    {
                        tsStateCondition1 = string.Format(@" a.TSSTATUS in ({0})", tsStateCondition1.TrimEnd(','));
                    }

                    if (tsStateCondition2.Length > 0)
                    {
                        tsStateCondition2 = string.Format(@" a.TSSTATUS in ({0})", tsStateCondition2.TrimEnd(','));
                    }
                }
                else
                {
                    tsStateCondition1 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}')",
                        TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow, TSStatus.TSStatus_TS);

                    tsStateCondition2 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}','{3}')",
                        TSStatus.TSStatus_New, TSStatus.TSStatus_Confirm, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split, TSStatus.TSStatus_OffMo);
                }
            }
            //维修完成，回流，维修中--
            string sql1 = string.Empty;
            if (tsStateCondition1 != string.Empty)
            {
                sql1 =
                    string.Format(@" select distinct {0} from tblts a,TBLTSERRORCAUSE b where {3}{4}{5} and a.tsid = b.tsid {1}{2}  and a.rmabillcode is not null group by {6} ",
                    "a.RCARD,a.rcardseq,a.tsid,a.TSSTATUS,a.MOCODE,a.rmabillcode,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,b.muser as tsuser,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE,a.REFOPCODE,max(a.TSTIMES) as TSTIMES",
                    modelCondition,
                    itemCondition,
                    tsStateCondition1,
                    receivedateCondition,
                    reworkCondition,
                    "a.RCARD,a.rcardseq,a.tsid,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.rmabillcode,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,b.muser,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE,a.REFOPCODE"
                    );
            }

            //送修，待修，拆解，报废 -- 不涉及TBLTSERRORCAUSE
            string sql2 = string.Empty;
            if (tsStateCondition2 != string.Empty)
            {
                sql2 =
                    string.Format(@" select {0} from tblts a where {1}{2}{3}{4}{5} and rmabillcode is not null",
                    "a.RCARD,a.rcardseq,a.tsid,a.TSSTATUS,a.MOCODE,a.rmabillcode,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,a.tsuser,a.tsdate AS RDATE,a.tstime AS RTIME,a.FRMDATE,a.FRMTIME,a.TSRESCODE as RRESCODE,a.REFRESCODE,a.REFOPCODE,a.TSTIMES",
                    tsStateCondition2,
                    modelCondition,
                    itemCondition,
                    receivedateCondition,
                    reworkCondition
                    );
            }

            string sql = string.Empty;
            if (sql1 != string.Empty && sql2 != string.Empty)
            {
                sql = sql1 + " union " + sql2;
            }
            else
            {
                sql = sql1 != string.Empty ? sql1 : sql2;
            }

#if DEBUG
            Log.Info(
                new PagerCondition(sql, "rcard", inclusive, exclusive, true).SQLText);

#endif

            return this.DataProvider.CustomQuery(
                typeof(QDORMATSRecord),
                new PagerCondition(sql
                , "rcard", inclusive, exclusive, true));
        }

        public int QueryRMATSRecordCount(
            string modelCodes,
            string itemCodes,
            string reworkMOCodes,
            string rmaBillCodes,
            int receiveStartDate, int receiveStartTime,
            int receiveEndDate, int receiveEndTime,
            ArrayList tsState)
        {
            string modelCondition = "";
            if (modelCodes != "" && modelCodes != null)
            {
                modelCondition += string.Format(@" and a.modelcode in ({0}) ", FormatHelper.ProcessQueryValues(modelCodes));
            }

            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition += string.Format(@" and a.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCodes));
            }

            if (rmaBillCodes != null && rmaBillCodes.Trim() != string.Empty)
            {
                itemCondition += string.Format(" and a.rmabillcode in({0})", FormatHelper.ProcessQueryValues(rmaBillCodes));
            }

            //返工工单条件
            string reworkCondition = string.Empty;
            if (reworkMOCodes != "" && reworkMOCodes != null)
            {
                reworkCondition = string.Format(" and a.mocode in ( {0})", FormatHelper.ProcessQueryValues(reworkMOCodes));
            }


            string receivedateCondition = FormatHelper.GetDateRangeSql("a.CONFIRMDATE", "a.CONFIRMTIME", receiveStartDate, receiveStartTime, receiveEndDate, receiveEndTime);

            string tsStateCondition1 = string.Empty;
            string tsStateCondition2 = string.Empty;
            if (tsState == null)
            {
                tsStateCondition1 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}')",
                    TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow, TSStatus.TSStatus_TS);

                tsStateCondition2 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}','{3}')",
                    TSStatus.TSStatus_New, TSStatus.TSStatus_Confirm, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split, TSStatus.TSStatus_OffMo);
            }
            if (tsState != null)
            {
                if (tsState.Count > 0)
                {
                    foreach (string state in tsState)
                    {
                        if (string.Compare(state, TSStatus.TSStatus_Complete, true) == 0)
                        {
                            tsStateCondition1 += string.Format("'{0}','{1}',", TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow);
                        }
                        else if (string.Compare(state, TSStatus.TSStatus_TS, true) == 0)
                        {
                            tsStateCondition1 += string.Format(@"'{0}',", state);
                        }
                        else if (string.Compare(state, TSStatus.TSStatus_New, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_Confirm, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_Split, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_Scrap, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_OffMo, true) == 0)
                        {
                            tsStateCondition2 += string.Format(@"'{0}',", state);
                        }

                    }

                    if (tsStateCondition1.Length > 0)
                    {
                        tsStateCondition1 = string.Format(@" a.TSSTATUS in ({0})", tsStateCondition1.TrimEnd(','));
                    }

                    if (tsStateCondition2.Length > 0)
                    {
                        tsStateCondition2 = string.Format(@" a.TSSTATUS in ({0})", tsStateCondition2.TrimEnd(','));
                    }
                }
                else
                {
                    tsStateCondition1 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}')",
                        TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow, TSStatus.TSStatus_TS);

                    tsStateCondition2 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}','{3}')",
                        TSStatus.TSStatus_New, TSStatus.TSStatus_Confirm, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split, TSStatus.TSStatus_OffMo);
                }
            }
            //维修完成，回流，维修中--
            string sql1 = string.Empty;
            if (tsStateCondition1 != string.Empty)
            {
                sql1 =
                    string.Format(@" select distinct {0} from tblts a,TBLTSERRORCAUSE b where {3}{4}{5} and a.tsid = b.tsid {1}{2}  and a.rmabillcode is not null group by {6} ",
                    "a.RCARD,a.rcardseq,a.tsid,a.TSSTATUS,a.MOCODE,a.rmabillcode,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,b.muser as tsuser,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE,a.REFOPCODE,max(a.TSTIMES) as TSTIMES",
                    modelCondition,
                    itemCondition,
                    tsStateCondition1,
                    receivedateCondition,
                    reworkCondition,
                    "a.RCARD,a.rcardseq,a.tsid,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.rmabillcode,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,b.muser,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE,a.REFOPCODE"
                    );
            }

            //送修，待修，拆解，报废 -- 不涉及TBLTSERRORCAUSE
            string sql2 = string.Empty;
            if (tsStateCondition2 != string.Empty)
            {
                sql2 =
                    string.Format(@" select {0} from tblts a where {1}{2}{3}{4}{5} and rmabillcode is not null",
                    "a.RCARD,a.rcardseq,a.tsid,a.TSSTATUS,a.MOCODE,a.rmabillcode,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,a.tsuser,a.tsdate AS RDATE,a.tstime AS RTIME,a.FRMDATE,a.FRMTIME,a.TSRESCODE as RRESCODE,a.REFRESCODE,a.REFOPCODE,a.TSTIMES",
                    tsStateCondition2,
                    modelCondition,
                    itemCondition,
                    receivedateCondition,
                    reworkCondition
                    );
            }

            string sql = string.Empty;
            if (sql1 != string.Empty && sql2 != string.Empty)
            {
                sql = string.Format(" select count(*) from ( {0} union {1} )", sql1, sql2);
            }
            else
            {
                sql = string.Format(" select count(*) from ( {0} )", sql1 != string.Empty ? sql1 : sql2);
            }

#if DEBUG
            Log.Info(new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(sql));
        }
        #endregion

        #region RMA维修资料统计

        public object[] QueryRMATSInfo(
            string modelCodes, string itemCodes, string moCodes, string rmaBillCodes, string frmResources,
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
            if (rmaBillCodes != null && rmaBillCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and rmabillcode in({0})", FormatHelper.ProcessQueryValues(rmaBillCodes));
            }
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                condition += string.Format(" and FRMRESCODE in({0})", FormatHelper.ProcessQueryValues(frmResources));
            }
            condition += FormatHelper.GetDateRangeSql("SHIFTDAY", startDate, endDate);

            if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                //modified by jessie lee for AM0228,2005/10/17,P4.11
                //按ErrorCodeGroup统计,在tbltserrorcode里查询
                sql = string.Format(@"select ECGCODE as errorcodegroup, qty,allqty,round(qty / allqty,4) as percent
										 from (select ECGCODE, count(distinct tsid) as qty
												 from tbltserrorcode
												where tsid in
													   (select tsid
														       from tblts
														      where tblts.rmabillcode is not null and tblts.tsid = tbltserrorcode.tsid {0})
												group by ECGCODE),
											  (select count(*) as allqty 
												 from (select distinct tsid,ECGCODE
												         from tbltserrorcode
												        where tsid in (select tsid 
																         from tblts 
																        where tblts.rmabillcode is not null and tblts.tsid = tbltserrorcode.tsid {0})))",
                    condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcodegroup, ecgdesc, qty,allqty, percent from ({0}) a,tblecg b where a.errorcodegroup=b.ecgcode ", sql);
            }
            //modified by jessie lee for AM0228,2005/10/17,P4.11
            //按ErrorCode统计,在tbltserrorcode里查询
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                sql = string.Format(@"select ECGCODE as errorcodegroup, ECODE as errorcode, qty,allqty, round(qty/allQty,4) as percent from
(select ECGCODE, ECODE, count(distinct tsid) as qty from tbltserrorcode where tsid in (select tsid from tblts where tblts.rmabillcode is not null and tblts.rmabillcode is not null and tblts.tsid = tbltserrorcode.tsid {0}) group by ECGCODE, ECODE),
(select count(*) as allQty from ( select distinct ECGCODE, ECODE,tsid from tbltserrorcode where tsid in (select tsid from tblts where tblts.rmabillcode is not null and tblts.tsid = tbltserrorcode.tsid {0}) ))", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcodegroup, ecgdesc,errorcode,ecdesc, qty,allqty, percent from ({0}) a,tblecg b,tblec c where a.errorcodegroup=b.ecgcode and a.errorcode = c.ecode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                sql = string.Format(@"select ECSCODE as errorcause, qty,allqty, round(qty/allQty,4) as percent from
(select ECSCODE, count(distinct tsid) as qty from TBLTSERRORCAUSE where tsid in (select tsid from tblts where  tblts.rmabillcode is not null {0}) group by ECSCODE),
(select count(*) as allQty from ( select distinct ECSCODE,tsid from TBLTSERRORCAUSE where tsid in (select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = TBLTSERRORCAUSE.tsid {0}) ))", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcause, ecsdesc, qty,allqty, percent from ({0}) a,tblecs b where a.errorcause=b.ecscode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCauseGroup)
            {

                sql = string.Format(@"select ecsgcode as errorcausegroup, qty,allqty, round(qty / allqty,4) as percent
										 from (select ecsgcode, count(distinct tsid) as qty
												 from TBLTSERRORCAUSE
												where tsid in
													   (select tsid
														       from tblts
														      where tblts.rmabillcode is not null and tblts.tsid = TBLTSERRORCAUSE.tsid {0})
												group by ECSGCODE),
											  (select count(*) as allqty 
												 from (select distinct tsid,ECSGCODE
												         from TBLTSERRORCAUSE
												        where tsid in (select tsid 
																         from tblts 
																        where tblts.rmabillcode is not null and tblts.tsid = TBLTSERRORCAUSE.tsid {0})))",
                     condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select errorcausegroup, ecsgdesc, qty,allqty, percent from ({0}) a,TBLECSG b where a.errorcausegroup=b.ecsgcode ", sql);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                sql = string.Format(@"select ELOC as errorlocation, qty,allqty, round(qty/allQty,4) as percent from
(select ELOC, count(distinct tsid) as qty from TBLTSERRORCAUSE2LOC where tsid in (select tsid from tblts where  tblts.rmabillcode is not null  {0}) group by ELOC),
(select count(*) as allQty from ( select distinct ELOC,tsid from TBLTSERRORCAUSE2LOC where tsid in (select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = TBLTSERRORCAUSE2LOC.tsid {0}) ))", condition);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                sql = string.Format(@"select DUTYCODE as duty, qty,allqty,round( qty/allQty,4) as percent from
(select DUTYCODE, count(distinct tsid) as qty from TBLTSERRORCAUSE where tsid in (select tsid from tblts where  tblts.rmabillcode is not null  {0}) group by DUTYCODE),
(select count(*) as allQty from ( select distinct DUTYCODE,tsid from TBLTSERRORCAUSE where tsid in (select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = TBLTSERRORCAUSE.tsid {0}) ))", condition);

                //added by jessie lee for TSC028,2005/10/21,P4.12
                sql = string.Format(" select duty, dutydesc, qty,allqty, percent from ({0}) a,tblduty b where a.duty=b.dutycode ", sql);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Errorcomponent)
            {
                sql = string.Format(@"select ERRORCOMPONENT as ERRORCOMPONENT, qty,allqty, round(qty/allQty,4) as percent from
                (select ERRORCOMPONENT, count(distinct tsid) as qty from TBLTSERRORCAUSE2COM where tsid in (select tsid from tblts where  tblts.rmabillcode is not null  {0}) group by ERRORCOMPONENT),
                (select count(*) as allQty from ( select distinct ERRORCOMPONENT,tsid from TBLTSERRORCAUSE2COM where tsid in (select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = TBLTSERRORCAUSE2COM.tsid {0}) ))", condition);
                            
            }

#if DEBUG
            Log.Info(
                new PagerCondition(
                sql, "qty desc",
                inclusive, Math.Min(exclusive, top), true).SQLText);

#endif

            return this.DataProvider.CustomQuery(typeof(QDOTSInfo), new PagerCondition(sql, "qty desc", inclusive, Math.Min(exclusive, top), true));
        }

        public int QueryRMATSInfoCount(
            string modelCodes, string itemCodes, string moCodes, string rmaBillCodes, string frmResources,
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
            if (rmaBillCodes != null && rmaBillCodes.Trim() != string.Empty)
            {
                condition += string.Format(" and rmabillcode in({0})", FormatHelper.ProcessQueryValues(rmaBillCodes));
            }
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                condition += string.Format(" and FRMRESCODE in({0})", FormatHelper.ProcessQueryValues(frmResources));
            }

            condition += FormatHelper.GetDateRangeSql("SHIFTDAY", startDate, endDate);

            if (summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                sql = string.Format(@"select count(*) from (select ECGCODE from tbltserrorcode where tsid in 
(select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = tbltserrorcode.tsid {0}) group by ECGCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCode)
            {
                sql = string.Format(@"select count(*) from (select ECGCODE, ECODE from tbltserrorcode where tsid in 
(select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = tbltserrorcode.tsid {0}) group by ECGCODE, ECODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorCause)
            {
                sql = string.Format(@"select count(*) from (select ECSCODE from TBLTSERRORCAUSE where tsid in 
(select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = TBLTSERRORCAUSE.tsid {0}) group by ECSCODE)", condition);
            }

            if (summaryTarget == TSInfoSummaryTarget.ErrorCauseGroup)
            {
                sql = string.Format(@"select count(*) from (select ECSGCODE,ECSCODE from TBLTSERRORCAUSE where tsid in 
(select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = TBLTSERRORCAUSE.tsid {0}) group by ECSGCODE, ECSCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.Duty)
            {
                sql = string.Format(@"select count(*) from (select DUTYCODE from TBLTSERRORCAUSE where tsid in 
(select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = TBLTSERRORCAUSE.tsid {0}) group by DUTYCODE)", condition);
            }

            else if (summaryTarget == TSInfoSummaryTarget.ErrorLocation)
            {
                sql = string.Format(@"select count(*) from (select ELOC from TBLTSERRORCAUSE2LOC where tsid in 
(select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = TBLTSERRORCAUSE2LOC.tsid {0}) group by ELOC)", condition);
            }
            else if (summaryTarget == TSInfoSummaryTarget.Errorcomponent)
            {
                sql = string.Format(@"select count(*) from (select ERRORCOMPONENT from TBLTSERRORCAUSE2COM where tsid in 
(select tsid from tblts where tblts.rmabillcode is not null and  tblts.tsid = TBLTSERRORCAUSE2COM.tsid {0}) group by ERRORCOMPONENT)", condition);
            }

#if DEBUG
            Log.Info(
                new SQLCondition(sql).SQLText);
#endif
            return Math.Min(top, this.DataProvider.GetCount(new SQLCondition(sql)));
        }

        #endregion

        #region 维修品清单
        public object[] QueryRMATSInfoList(
            string moCode,
            int startDate, int endDate,
            string summaryTarget, string summaryObject, string summaryObject1,
            string modelCodes, string itemCodes, string frmResources, string rmabillcodes,
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

            if (rmabillcodes != null && rmabillcodes.Trim() != string.Empty)
            {
                itemCondition += string.Format(" and a.rmabillcode in({0})", FormatHelper.ProcessQueryValues(rmabillcodes));
            }

            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
            }

            string frmResCondition = string.Empty;
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                frmResCondition += string.Format(" and a.FRMRESCODE in({0})", FormatHelper.ProcessQueryValues(frmResources));
            }

            string dateCondition = FormatHelper.GetDateRangeSql("a.SHIFTDAY", startDate, endDate);

            //ErrorCodeGroup,ErrorCode用sqlStr1
            string sqlStr1 = "select  {0} from tblts a,tbltserrorcode b,{1} where a.rmabillcode is not null and a.tsid = b.tsid and a.tsid = c.tsid(+) {8} {2}{3}{4}{5}{6} group by {7} ";
            //ErrorCause,ErrorLocation,Duty用sqlStr2
            string sqlStr2 = "select  {0} from tblts a,{1} where a.rmabillcode is not null and a.tsid = b.tsid(+) {8} {2}{3}{4}{5}{6} group by {7} ";

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
            else if (summaryTarget == TSInfoSummaryTarget.Errorcomponent)
            {
                table = "TBLTSERRORCAUSE2COM b";
                summaryTargetCondition = string.Format(@" and b.Errorcomponent = '{0}'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCauseGroup)
            {
                table = "TBLTSERRORCAUSE b";
                summaryTargetCondition = string.Format(@" and b.ECSGCODE = '{0}'", summaryObject);
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
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
                    frmResCondition),
                    "rcard", inclusive, exclusive, true).SQLText);
            }
            else
            {
                Log.Info(
                    new PagerCondition(
                    string.Format(sqlStr2,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
                    frmResCondition),
                    "rcard", inclusive, exclusive, true).SQLText);
            }
#endif

            string sql = string.Empty;
            if (summaryTarget == TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                sql = string.Format(sqlStr1,
                    @"a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.rcardseq,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tsdate)
						when 'tsstatus_split'
						then max(a.tsdate)
						else 
						MAX (c.mdate) 
						end as rdate,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tstime)
						when 'tsstatus_split'
						then max(a.tstime)
						else 
						MAX (c.mtime) 
						end as rtime,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tsrescode) 
						when 'tsstatus_split'
						then max(a.tsrescode) 
						else 
						max(c.rrescode )
						end as rrescode,
					case TSSTATUS
						when 'tsstatus_scrap'
						then max(a.tsuser) 
						when 'tsstatus_split'
						then max(a.tsuser) 
						else 
						max(c.muser )
						end as tsuser,a.FRMDATE,a.FRMTIME,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,a.REFRESCODE,a.RCARDSEQ",
                    frmResCondition);
                return this.DataProvider.CustomQuery(
                    typeof(QDOTSRecord),
                    new PagerCondition(
                    sql,
                    "rcard", inclusive, exclusive, true));
            }

            sql = string.Format(sqlStr2,
                @"a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.rcardseq,
						CASE tsstatus
						WHEN 'tsstatus_scrap'
							THEN MAX (a.tsdate)
						WHEN 'tsstatus_split'
							THEN MAX (a.tsdate)
						ELSE MAX (b.mdate)
						END AS rdate,
						CASE tsstatus
						WHEN 'tsstatus_scrap'
							THEN MAX (a.tstime)
						WHEN 'tsstatus_split'
							THEN MAX (a.tstime)
						ELSE MAX (b.mtime)
						END AS rtime,
						CASE tsstatus
						WHEN 'tsstatus_scrap'
							THEN MAX (a.tsrescode)
						WHEN 'tsstatus_split'
							THEN MAX (a.tsrescode)
						ELSE MAX (b.rrescode)
						END AS rrescode,
						CASE tsstatus
						WHEN 'tsstatus_scrap'
							THEN MAX (a.tsuser)
						WHEN 'tsstatus_split'
							THEN MAX (a.tsuser)
						ELSE MAX (b.muser)
						END AS tsuser,
				a.FRMDATE,a.FRMTIME,a.REFRESCODE",
                table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                "a.tsid,a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,a.REFRESCODE,a.RCARDSEQ",
                frmResCondition);
            return this.DataProvider.CustomQuery(
                typeof(QDOTSRecord),
                new PagerCondition(
                sql,
                "rcard", inclusive, exclusive, true));
        }

        public int QueryRMATSInfoListCount(
            string moCode,
            int startDate, int endDate,
            string summaryTarget, string summaryObject, string summaryObject1,
            string modelCodes, string itemCodes, string frmResources, string rmabillcodes)
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

            if (rmabillcodes != null && rmabillcodes.Trim() != string.Empty)
            {
                itemCondition += string.Format(" and a.rmabillcode in({0})", FormatHelper.ProcessQueryValues(rmabillcodes));
            }

            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
            }

            string frmResCondition = string.Empty;
            if (frmResources != null && frmResources.Trim() != string.Empty)
            {
                frmResCondition += string.Format(" and a.FRMRESCODE in({0})", FormatHelper.ProcessQueryValues(frmResources));
            }

            string dateCondition = FormatHelper.GetDateRangeSql("a.SHIFTDAY", startDate, endDate);

            //ErrorCodeGroup,ErrorCode用sqlStr1
            string sqlStr1 = "select count(*) from (select distinct {0} from tblts a,tbltserrorcode b,{1} where a.rmabillcode is not null and  a.tsid = b.tsid and a.tsid = c.tsid(+) {8} {2}{3}{4}{5}{6} group by {7} )";
            //ErrorCause,ErrorLocation,Duty用sqlStr2
            string sqlStr2 = "select count(*) from (select distinct {0} from tblts a,{1} where a.rmabillcode is not null and a.tsid = b.tsid(+) {8} {2}{3}{4}{5}{6} group by {7} )";
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
            else if (summaryTarget == TSInfoSummaryTarget.Errorcomponent)
            {
                table = "TBLTSERRORCAUSE2COM b";
                summaryTargetCondition = string.Format(@" and b.Errorcomponent = '{0}'", summaryObject);
            }
            else if (summaryTarget == TSInfoSummaryTarget.ErrorCauseGroup)
            {
                table = "TBLTSERRORCAUSE b";
                summaryTargetCondition = string.Format(@" and b.ECSGCODE = '{0}'", summaryObject);
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
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE", frmResCondition)).SQLText);
            }
            else
            {
                Log.Info(
                    new SQLCondition(
                    string.Format(sqlStr2,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE", frmResCondition)).SQLText);
            }
#endif

            if (summaryTarget == TSInfoSummaryTarget.ErrorCode || summaryTarget == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                return this.DataProvider.GetCount(
                    new SQLCondition(
                    string.Format(sqlStr1,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(c.MDATE) AS RDATE,max(c.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE",
                    table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                    "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,c.RRESCODE,a.REFRESCODE", frmResCondition)));
            }

            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(sqlStr2,
                "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE",
                table, modelCondition, itemCondition, moCondition, dateCondition, summaryTargetCondition,
                "a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMDATE,a.FRMTIME,b.RRESCODE,a.REFRESCODE", frmResCondition)));
        }

        //导出维修资料统计(包含维修明细)
        public object[] ExportQueryRMATSInfoList(
            string moCode,
            int startDate, int endDate,
            string summaryTarget, string summaryObject, string summaryObject1,
            string modelCodes, string itemCodes, string frmResources, string rmabillcodes,
            int inclusive, int exclusive)
        {
            object[] tsObjs = this.QueryRMATSInfoList(moCode, startDate, endDate, summaryTarget, summaryObject, summaryObject1, modelCodes, itemCodes, frmResources, rmabillcodes, inclusive, exclusive);
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
                                exportTSDetails.Solution = tsdetail.Solution;
                                exportTSDetails.Duty = tsdetail.Duty;
                                exportTSDetails.DutyDescription = tsdetail.DutyDescription;
                                exportTSDetails.SolutionDescription = tsdetail.SolutionDescription;
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
        #endregion

        #region RMA不良品
        public object[] QueryRMADefect(string timetype, int startDate, int endDate, int inclusive, int exclusive)
        {
            string timefield = string.Empty;
            switch (timetype)
            {
                case RMATimeKide.Day:
                    timefield = "C.DDATE";
                    break;
                case RMATimeKide.Week:
                    timefield = "C.DWEEK";
                    break;
                case RMATimeKide.Month:
                    timefield = "C.DMONTH";
                    break;
                default:
                    timefield = "C.DDATE";
                    break;
            }
            string condition = FormatHelper.GetDateRangeSql("B.MDATE", startDate, endDate);
            string sqlStr = string.Format(@"      
                                  SELECT TIMETYPE,
                                 SUM(TOTALQTY) AS TOTALQTY,
                                 SUM(RMAQTY) AS RMAQTY,
                                 SUM(DECODE(STATUS, '{0}', RMAQTY, 0)) AS CLOSEDQTY
                            FROM (SELECT {1} AS TIMETYPE,
                                         COUNT(DISTINCT B.RMABILLCODE) AS RMAQTY,
                                         COUNT(B.RMABILLCODE) AS TOTALQTY,
                                         A.STATUS
                                    FROM TBLRMABILL A
                                   INNER JOIN TBLRMADETIAL B ON A.RMABILLCODE = B.RMABILLCODE
                                   LEFT JOIN TBLTIMEDIMENSION C ON C.DDATE = B.MDATE
                                   WHERE 1 = 1
                                    {2}
                                   GROUP BY {1}, STATUS)
                           GROUP BY TIMETYPE ", RMABillStatus.Closed, timefield, condition);
            PagerCondition pagerCondition = new PagerCondition(sqlStr, "TIMETYPE", inclusive, exclusive, true);
#if DEBUG
            Log.Info(pagerCondition.SQLText);
#endif

            return this.DataProvider.CustomQuery(typeof(QDORMADefect), pagerCondition);
        }

        public int QueryRMADefectCount(string timetype, int startDate, int endDate)
        {
            string timefield = string.Empty;
            switch (timetype)
            {
                case RMATimeKide.Day:
                    timefield = "C.DDATE";
                    break;
                case RMATimeKide.Week:
                    timefield = "C.DWEEK";
                    break;
                case RMATimeKide.Month:
                    timefield = "C.DMONTH";
                    break;
                default:
                    timefield = "C.DDATE";
                    break;
            }
            string condition = FormatHelper.GetDateRangeSql("B.MDATE", startDate, endDate);
            string sqlStr = string.Format(@"      
                                  SELECT TIMETYPE,
                                 SUM(TOTALQTY) AS TOTALQTY,
                                 SUM(RMAQTY) AS RMAQTY,
                                 SUM(DECODE(STATUS, '{0}', RMAQTY, 0)) AS CLOSEDQTY
                            FROM (SELECT {1} AS TIMETYPE,
                                         COUNT(DISTINCT B.RMABILLCODE) AS RMAQTY,
                                         COUNT(B.RMABILLCODE) AS TOTALQTY,
                                         A.STATUS
                                    FROM TBLRMABILL A
                                   INNER JOIN TBLRMADETIAL B ON A.RMABILLCODE = B.RMABILLCODE
                                   LEFT JOIN TBLTIMEDIMENSION C ON C.DDATE = B.MDATE
                                   WHERE 1 = 1
                                    {2}
                                   GROUP BY {1}, STATUS)
                           GROUP BY TIMETYPE ", RMABillStatus.Closed, timefield, condition);
            sqlStr = string.Format("select count(*) from ({0})", sqlStr);
            SQLCondition sqlCondition = new SQLCondition(sqlStr);
#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            return this.DataProvider.GetCount(sqlCondition);
        }
        #endregion

        #region 厂区RMA数量分布
        public object[] QueryRMAFactoryQTYDistribute(string timetype, int startDate, int endDate)
        {
            string timefield = string.Empty;
            switch (timetype)
            {
                case RMATimeKide.Day:
                    timefield = "opendate";
                    break;
                case RMATimeKide.Week:
                    timefield = "openweek";
                    break;
                case RMATimeKide.Month:
                    timefield = "openmonth";
                    break;
                default:
                    timefield = "opendate";
                    break;
            }
            string condition = FormatHelper.GetDateRangeSql("opendate", startDate, endDate);

            string sql = string.Format(
                @"select faccode,
					{0} as TIMETYPE,
					sum(decode(itemissue, '{1}', QTY, 0)) as QIQTY,
					sum(decode(itemissue, '{1}', 0, QTY)) as NQIQTY,
					sum(QTY) as QTY
				from tblrmabill
				where 1=1 {2}
				group by faccode, {0}", timefield, RMAItemIssue.QualityIssue, condition);
            SQLCondition sqlCondition = new SQLCondition(sql);
#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            return this.DataProvider.CustomQuery(typeof(QDORMAFactoryQTYDistribute), sqlCondition);
        }

        public object[] QueryRMAFactoryQTYTotal(int startDate, int endDate)
        {
            string condition = FormatHelper.GetDateRangeSql("opendate", startDate, endDate);

            string sql = string.Format(
                @"select faccode,
					sum(QTY) as QTY
				from tblrmabill
				where 1=1 {0}
				group by faccode", condition);
            SQLCondition sqlCondition = new SQLCondition(sql);
#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            return this.DataProvider.CustomQuery(typeof(QDORMAFactoryQTYTotal), sqlCondition);
        }


        #endregion

        #region 厂区RMA件数分布
        public object[] QueryRMAFactoryCountDistribute(string timetype, int startDate, int endDate)
        {
            string timefield = string.Empty;
            switch (timetype)
            {
                case RMATimeKide.Day:
                    timefield = "opendate";
                    break;
                case RMATimeKide.Week:
                    timefield = "openweek";
                    break;
                case RMATimeKide.Month:
                    timefield = "openmonth";
                    break;
                default:
                    timefield = "opendate";
                    break;
            }
            string condition = FormatHelper.GetDateRangeSql("opendate", startDate, endDate);

            string sql = string.Format(
                @"select faccode,
					TIMETYPE,
					sum(decode(itemissue, '{1}', count, 0)) as QICOUNT,
					sum(decode(itemissue, '{1}', 0, count)) as NQICOUNT,
					sum(count) as COUNT
				from (select faccode,
							{0} as TIMETYPE,
							count(distinct rmabillcode) as count,
							itemissue
						from tblrmabill
						where 1 = 1 {2}
						group by faccode, {0}, itemissue)
				group by faccode, TIMETYPE",
                timefield, RMAItemIssue.QualityIssue, condition);
            SQLCondition sqlCondition = new SQLCondition(sql);

#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            return this.DataProvider.CustomQuery(typeof(QDORMAFactoryCountDistribute), sqlCondition);

        }

        public object[] QueryRMAFactoryCountTotal(int startDate, int endDate)
        {
            string condition = FormatHelper.GetDateRangeSql("opendate", startDate, endDate);

            string sql = string.Format(
                @"select faccode,
					count(distinct rmabillcode) as count
				from tblrmabill
				where 1=1 {0}
				group by faccode", condition);
            SQLCondition sqlCondition = new SQLCondition(sql);
#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            return this.DataProvider.CustomQuery(typeof(QDORMAFactoryCountTotal), sqlCondition);
        }

        #endregion

        #region 产品问题比率
        public object[] QueryRMAItemIssueRate(int startDate, int endDate)
        {
            string condition = FormatHelper.GetDateRangeSql("opendate", startDate, endDate);

            string sql = string.Format(
                @"select itemissue, sum(qty) as qty
					from tblrmabill
					where 1 = 1 {0}
					group by itemissue", condition);
            SQLCondition sqlCondition = new SQLCondition(sql);
#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            object[] objs = this.DataProvider.CustomQuery(typeof(QDORMAItemIssueRate), sqlCondition);

            if (objs != null && objs.Length > 0)
            {
                decimal totalQty = Decimal.Zero;
                for (int i = 0; i < objs.Length; i++)
                {
                    totalQty += (objs[i] as QDORMAItemIssueRate).Qty;
                }

                for (int i = 0; i < objs.Length; i++)
                {
                    (objs[i] as QDORMAItemIssueRate).TotalQty = totalQty;
                }
            }

            return objs;

        }
        #endregion

        #region RMA结案率
        public object[] QueryRMAClosedRate(int startDate, int endDate)
        {
            string condition = FormatHelper.GetDateRangeSql("a.mdate", startDate, endDate);

            string sql = string.Format(
                @"select STATUS, count(STATUS) as QTY FROM 
					(SELECT DISTINCT a.RMABILLCODE ,a.STATUS FROM TBLRMABILL a INNER JOIN TBLRMADETIAL b ON a.RMABILLCODE = b.rmabillcode
					where 1 = 1  {0} )
					group by status", condition);
            SQLCondition sqlCondition = new SQLCondition(sql);
#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            object[] objs = this.DataProvider.CustomQuery(typeof(QDORMAClosedRate), sqlCondition);

            if (objs != null && objs.Length > 0)
            {
                decimal totalQty = Decimal.Zero;
                for (int i = 0; i < objs.Length; i++)
                {
                    totalQty += (objs[i] as QDORMAClosedRate).Qty;
                }

                for (int i = 0; i < objs.Length; i++)
                {
                    (objs[i] as QDORMAClosedRate).TotalQty = totalQty;
                }
            }

            return objs;

        }
        #endregion

        #region
        public object[] QueryRMARework(string startSourceCard, string endSourceCard, string remo, int inclusive, int exclusive)
        {
            string sqlStr = @"SELECT TBLSIMULATIONREPORT.RESCODE || ' - ' || TBLRES.RESDESC AS RESCODE,
                               TBLSIMULATIONREPORT.RCARDSEQ,
                               TBLSIMULATIONREPORT.SSCODE || ' - ' || TBLSS.SSDESC AS SSCODE,
                               TBLSIMULATIONREPORT.SEGCODE || ' - ' || TBLSEG.SEGDESC AS SEGCODE,
                               TBLSIMULATIONREPORT.ROUTECODE,
                               TBLSIMULATIONREPORT.MODELCODE,
                               TBLSIMULATIONREPORT.MOCODE,
                               TBLSIMULATIONREPORT.STATUS,
                               TBLSIMULATIONREPORT.ITEMCODE || ' - ' || TBLITEM.ITEMDESC AS ITEMCODE,
                               TBLSIMULATIONREPORT.MUSER || ' - ' || TBLUSER.USERNAME AS MUSER,
                               TBLSIMULATIONREPORT.RCARD,
                               TBLSIMULATIONREPORT.TCARD,
                               TBLSIMULATIONREPORT.MTIME,
                               TBLSIMULATIONREPORT.OPCODE || ' - ' || TBLOP.OPDESC AS OPCODE,
                               TBLSIMULATIONREPORT.MDATE,
                               TBLSIMULATIONREPORT.LACTION,
                               '' AS OPTYPE,
                               TBLMATERIAL.MMODELCODE,
                               TBLSS.BIGSSCODE
                          FROM TBLSIMULATIONREPORT
                          LEFT OUTER JOIN TBLMATERIAL ON TBLSIMULATIONREPORT.ITEMCODE =
                                                         TBLMATERIAL.MCODE
                         INNER JOIN TBLMO ON TBLSIMULATIONREPORT.MOCODE = TBLMO.MOCODE
                          LEFT OUTER JOIN TBLSS ON TBLSIMULATIONREPORT.SSCODE = TBLSS.SSCODE
                          LEFT OUTER JOIN TBLITEM ON TBLSIMULATIONREPORT.ITEMCODE =
                                                     TBLITEM.ITEMCODE
                          LEFT OUTER JOIN TBLOP ON TBLSIMULATIONREPORT.OPCODE = TBLOP.OPCODE
                          LEFT OUTER JOIN TBLSEG ON TBLSIMULATIONREPORT.SEGCODE = TBLSEG.SEGCODE
                          LEFT OUTER JOIN TBLRES ON TBLSIMULATIONREPORT.RESCODE = TBLRES.RESCODE
                          LEFT OUTER JOIN TBLUSER ON TBLSIMULATIONREPORT.MUSER = TBLUSER.USERCODE
                         WHERE 1=1 AND TBLSIMULATIONREPORT.Rmabillcode<>' ' ";
            if (startSourceCard != null && startSourceCard.Length > 0)
            {
                sqlStr += " AND TBLSIMULATIONREPORT.RCARD >= '" + startSourceCard + "'";
            }
            if (endSourceCard != null && endSourceCard.Length > 0)
            {
                sqlStr += " AND TBLSIMULATIONREPORT.RCARD <= '" + endSourceCard + "'";
            }
            if (remo != null && remo.Length > 0)
            {
                sqlStr += " AND TBLSIMULATIONREPORT.MOCODE = '" + remo + "'";
            }

            PagerCondition pagerCondition = new PagerCondition(sqlStr, "MOCODE,RCARDSEQ", inclusive, exclusive, true);
            return this.DataProvider.CustomQuery(typeof(ItemTracingQuery), pagerCondition);
        }

        public int QueryRMAReworkCount(string startSourceCard, string endSourceCard, string remo)
        {
            string sqlStr = @"SELECT Count(*)
                          FROM TBLSIMULATIONREPORT
                          LEFT OUTER JOIN TBLMATERIAL ON TBLSIMULATIONREPORT.ITEMCODE =
                                                         TBLMATERIAL.MCODE
                         INNER JOIN TBLMO ON TBLSIMULATIONREPORT.MOCODE = TBLMO.MOCODE
                          LEFT OUTER JOIN TBLSS ON TBLSIMULATIONREPORT.SSCODE = TBLSS.SSCODE
                          LEFT OUTER JOIN TBLITEM ON TBLSIMULATIONREPORT.ITEMCODE =
                                                     TBLITEM.ITEMCODE
                          LEFT OUTER JOIN TBLOP ON TBLSIMULATIONREPORT.OPCODE = TBLOP.OPCODE
                          LEFT OUTER JOIN TBLSEG ON TBLSIMULATIONREPORT.SEGCODE = TBLSEG.SEGCODE
                          LEFT OUTER JOIN TBLRES ON TBLSIMULATIONREPORT.RESCODE = TBLRES.RESCODE
                          LEFT OUTER JOIN TBLUSER ON TBLSIMULATIONREPORT.MUSER = TBLUSER.USERCODE
                         WHERE 1=1 AND TBLSIMULATIONREPORT.Rmabillcode<>' ' ";
            if (startSourceCard != null && startSourceCard.Length > 0)
            {
                sqlStr += " AND TBLSIMULATIONREPORT.RCARD >= '" + startSourceCard + "'";
            }
            if (endSourceCard != null && endSourceCard.Length > 0)
            {
                sqlStr += " AND TBLSIMULATIONREPORT.RCARD <= '" + endSourceCard + "'";
            }
            if (remo != null && remo.Length > 0)
            {
                sqlStr += " AND TBLSIMULATIONREPORT.MOCODE = '" + remo + "'";
            }

            SQLCondition sqlCondition = new SQLCondition(sqlStr);

            return this.DataProvider.GetCount(sqlCondition);
        }
        #endregion

        #region 客户RMA件数与数量分布
        public object[] QueryRMACustomerDis(int startDate, int endDate, int inclusive, int exclusive)
        {
            string condition = FormatHelper.GetDateRangeSql("B.MDATE", startDate, endDate);
            string sql = string.Format(
                @"SELECT B.Customcode AS CUSCODE,
                                 COUNT(DISTINCT b.RMABILLCODE) AS COUNT,
                                 COUNT( b.RMABILLCODE ) AS QTY
                            FROM TBLRMABILL a INNER JOIN TBLRMADETIAL B ON a.RMABILLCODE = b.rmabillcode
                           WHERE 1 = 1
                             {0}
                           GROUP BY B.Customcode", condition);
            PagerCondition pagerCondition = new PagerCondition(sql, "CUSCODE", inclusive, exclusive, true);
#if DEBUG
            Log.Info(pagerCondition.SQLText);
#endif

            object[] objs = this.DataProvider.CustomQuery(typeof(QDORMACustomerDis), pagerCondition);
            if (objs != null && objs.Length > 0)
            {
                decimal totalQty = Decimal.Zero;
                for (int i = 0; i < objs.Length; i++)
                {
                    totalQty += (objs[i] as QDORMACustomerDis).Quantity;
                }

                for (int i = 0; i < objs.Length; i++)
                {
                    (objs[i] as QDORMACustomerDis).TotalQty = totalQty;
                }
            }

            return objs;
        }

        public int QueryRMACustomerDisCount(int startDate, int endDate)
        {
            string condition = FormatHelper.GetDateRangeSql("B.MDATE", startDate, endDate);
            string sql = string.Format(
                @"SELECT B.Customcode AS CUSCODE,
                                 COUNT(DISTINCT b.RMABILLCODE) AS COUNT,
                                 COUNT( b.RMABILLCODE ) AS QTY
                            FROM TBLRMABILL a INNER JOIN TBLRMADETIAL B ON a.RMABILLCODE = b.rmabillcode
                           WHERE 1 = 1
                             {0}
                           GROUP BY B.Customcode", condition);
            sql = string.Format("select count(*) from ({0})", sql);
            SQLCondition sqlCondition = new SQLCondition(sql);
#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            return this.DataProvider.GetCount(sqlCondition);
        }
        #endregion
    }

    #region RMA Query Object
    public class QDORMATSRecord : QDOTSRecord
    {
        [FieldMapAttribute("RMABillCode", typeof(string), 40, true)]
        public string RMABillCode;
    }

    public class QDORMADefect : DomainObject
    {
        [FieldMapAttribute("TIMETYPE", typeof(int), 8, true)]
        public int TimeType;

        [FieldMapAttribute("TOTALQTY", typeof(int), 10, true)]
        public int TotalQty;

        [FieldMapAttribute("RMAQTY", typeof(int), 8, true)]
        public int RMAQuantity;

        [FieldMapAttribute("CLOSEDQTY", typeof(int), 8, true)]
        public int ClosedQty;
    }

    public class QDORMAFactoryQTYTotal : DomainObject
    {
        /// <summary>
        /// 工厂
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string Factory;

        [FieldMapAttribute("QTY", typeof(int), 8, true)]
        public int Qty;
    }

    public class QDORMAFactoryQTYDistribute : QDORMAFactoryQTYTotal
    {
        [FieldMapAttribute("TIMETYPE", typeof(int), 8, true)]
        public int TimeType;

        [FieldMapAttribute("QIQTY", typeof(int), 10, true)]
        public int QualityIssueQty;

        [FieldMapAttribute("NQIQTY", typeof(int), 8, true)]
        public int NonQualityIssueQty;
    }

    public class QDORMAFactoryCountTotal : DomainObject
    {
        /// <summary>
        /// 工厂
        /// </summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string Factory;

        [FieldMapAttribute("COUNT", typeof(int), 8, true)]
        public int Count;
    }

    public class QDORMAFactoryCountDistribute : QDORMAFactoryCountTotal
    {
        [FieldMapAttribute("TIMETYPE", typeof(int), 8, true)]
        public int TimeType;

        [FieldMapAttribute("QICOUNT", typeof(int), 10, true)]
        public int QualityIssueCount;

        [FieldMapAttribute("NQICOUNT", typeof(int), 8, true)]
        public int NonQualityIssueCount;
    }

    public class QDORMAItemIssueRate : DomainObject
    {
        /// <summary>
        /// 工厂
        /// </summary>
        [FieldMapAttribute("itemissue", typeof(string), 40, true)]
        public string ItemIssue;

        [FieldMapAttribute("QTY", typeof(int), 8, true)]
        public decimal Qty;

        [FieldMapAttribute("TOTALQTY", typeof(int), 8, true)]
        public decimal TotalQty;
    }

    public class QDORMAClosedRate : DomainObject
    {
        /// <summary>
        /// 工厂
        /// </summary>
        [FieldMapAttribute("Status", typeof(string), 40, true)]
        public string Status;

        [FieldMapAttribute("QTY", typeof(int), 8, true)]
        public decimal Qty;

        [FieldMapAttribute("TOTALQTY", typeof(int), 8, true)]
        public decimal TotalQty;
    }

    public class QDORMACustomerDis : DomainObject
    {
        [FieldMapAttribute("CUSCODE", typeof(string), 40, true)]
        public string CustomerCode;

        [FieldMapAttribute("COUNT", typeof(int), 10, true)]
        public decimal Count;

        [FieldMapAttribute("QTY", typeof(int), 8, true)]
        public decimal Quantity;

        [FieldMapAttribute("TOTALQTY", typeof(int), 8, true)]
        public decimal TotalQty;
    }

    #endregion
}
