using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.Rework;

namespace BenQGuru.eMES.WebQuery
{
    /// <summary>
    /// QueryTSDetails 的摘要说明。
    /// </summary>
    public class QueryTSDetailsFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public QueryTSDetailsFacade(IDomainDataProvider domainDataProvider)
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

        #region 复杂,已抛弃

        public object[] QueryTSDetails(
            string modelCode, string itemCode, string moCode,
            string sn, string tsState, string tsOperator,
            string tsResourceCode, int sourceResourceDate, int sourceResourceTime,
            int inclusive, int exclusive)
        {
            string modelCondition = "";
            if (modelCode != "" && modelCode != null)
            {
                modelCondition += string.Format(@" and a.modelcode = '{0}'", modelCode);
            }

            string itemCondition = "";
            if (itemCode != "" && itemCode != null)
            {
                itemCondition += string.Format(@" and a.itemcode = '{0}'", itemCode);
            }

            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode = '{0}'", moCode);
            }

            string snCondition = "";
            if (sn != "" && sn != null)
            {
                snCondition += string.Format(@" and a.rcard = '{0}'", sn.ToUpper());
            }

            string tsStateCondition = "";
            if (tsState != "" && tsState != null)
            {
                tsStateCondition += string.Format(@" and tsstate = '{0}'", tsState);
            }
            string tsResCodeCondition = string.Empty;
            if (tsResourceCode != string.Empty && tsResourceCode != null)
            {
                tsResCodeCondition += string.Format(" and c.rrescode = '{0}' ", tsResourceCode);
            }

            string sourceResourceDateCondition = string.Format(@" and a.FRMDATE = {0}", sourceResourceDate);

            string sourceResourceTimeCondition = string.Format(@" and a.FRMTIME = {0}", sourceResourceTime);

            string sql = string.Format(
                @" select distinct {0} from tblts a,tbltserrorcode b,TBLTSERRORCAUSE c where a.tsid = b.tsid and a.tsid = c.tsid(+) {8} {1}{2}{3}{4}{5}{6}{7}",
                "a.tsid,a.TSMEMO,a.tsuser, a.tsdate ,a.tstime ",
                modelCondition, itemCondition, moCondition,
                snCondition, tsStateCondition,
                sourceResourceDateCondition, sourceResourceTimeCondition, tsResCodeCondition);

#if DEBUG
            Log.Info(
                new PagerCondition(sql,
                inclusive, exclusive, true).SQLText);
#endif
            object[] source = this.DataProvider.CustomQuery(
                typeof(QDOTSDetails),
                new PagerCondition(
                sql,
                inclusive, exclusive, true));

            object[] retSource = null;
            if (source != null)
            {
                #region Assign ErrorCodeGroup,ErrorCode
                foreach (QDOTSDetails details in source)
                {
                    //error code,error code group
                    object[] errorCodes = this.DataProvider.CustomQuery(typeof(QDOTSErrorCode),
                        new SQLParamCondition(string.Format(@"select {0} from tbltserrorcode a,tblecg b,tblec c 
							where a.tsid=$TSID and a.ecgcode = b.ecgcode and a.ecode = c.ecode",
                        @"a.tsid,
							  a.itemcode,
                              a.modelcode,
                              a.mocode,
                              a.ecode,
                              a.eattribute1,
                              a.ecgcode,
                              a.rcardseq,
                              a.mtime,
                              a.rcard,
                              a.mdate,
                              c.ecdesc,
                              a.muser,
                              b.ecgdesc"),
                        new SQLParameter[]{
											  new SQLParameter("TSID",typeof(String),details.TSID)
										  }));

                    //error codes
                    if (errorCodes != null)
                    {
                        foreach (QDOTSErrorCode code in errorCodes)
                        {
                            details.ErrorCodes.Add(code);

                            //ErrorCause,Duty,ErrorSolution,ErrorLocation
                            object[] errorCauses = this.DataProvider.CustomQuery(typeof(QDOTSErrorCause),
                                new SQLParamCondition(string.Format(@"select {0} from tbltserrorcause a,tblduty b,tblecs c,tblsolution d 
									where a.tsid=$TSID and a.ecgcode=$ERRORCODEGROUP and a.ecode=$ERRORCODE
									 and a.dutycode = b.dutycode and a.ecscode = c.ecscode and a.solcode = d.solcode",
                                @"a.tsid,
									a.itemcode,
									a.modelcode,
									a.rcardseq,
									a.rcard,
									a.ecgcode,
									a.ropcode,
									a.ecode,
									a.dutycode,
									a.mocode,
									a.ecscode,
									a.eattribute1,
									a.rrescode,
									a.solcode,
									a.mtime,
									a.solmemo,
									a.mdate,
									b.dutydesc,
									a.muser,
									c.ecsdesc,
									d.soldesc"),
                                new SQLParameter[]{
													  new SQLParameter("TSID",typeof(String),code.TSId),
													  new SQLParameter("ERRORCODEGROUP",typeof(String),	code.ErrorCodeGroup),
													  new SQLParameter("ERRORCODE",typeof(String),code.ErrorCode),	
							}));

                            if (errorCauses != null)
                            {
                                foreach (QDOTSErrorCause cause in errorCauses)
                                {
                                    code.ErrorCauses.Add(cause);

                                    //error part
                                    cause.ErrorPartsList = this.DataProvider.CustomSearch(
                                        typeof(TSErrorCause2ErrorPart),
                                        new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                        new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });

                                    //error location
                                    cause.LocationList = this.DataProvider.CustomSearch(
                                        typeof(TSErrorCause2Location),
                                        new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                        new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });

                                }
                            }
                        }
                    }
                }
                #endregion

                #region Change object to dimension
                ArrayList array = new ArrayList();

                foreach (QDOTSDetails detail in source)
                {
                    object[] data = QDOTSDetails1.ToArray(detail);
                    if (data != null)
                    {
                        foreach (QDOTSDetails1 details1 in data)
                        {
                            array.Add(details1);
                        }
                    }
                }

                retSource = (QDOTSDetails1[])array.GetRange(0, System.Math.Min(array.Count, exclusive - inclusive)).ToArray(typeof(QDOTSDetails1));
                #endregion
            }

            return retSource;
        }

        public object[] QueryTSDetailsByID(string tsIDs, int inclusive, int exclusive)
        {
            string sql = string.Format(
                @" select distinct {0} from tblts a,tbltserrorcode b,TBLTSERRORCAUSE c where a.tsid = b.tsid and a.tsid = c.tsid(+) and a.tsid in {1}",
                "a.tsid,a.TSMEMO,a.tsuser, a.tsdate ,a.tstime ", tsIDs);
#if DEBUG
            Log.Info(
                new PagerCondition(sql,
                inclusive, exclusive, true).SQLText);
#endif
            object[] source = this.DataProvider.CustomQuery(
                typeof(QDOTSDetails),
                new PagerCondition(
                sql,
                inclusive, exclusive, true));

            object[] retSource = null;
            if (source != null)
            {
                #region Assign ErrorCodeGroup,ErrorCode
                QDOTSDetails details = (QDOTSDetails)source[0];

                #region 已废弃代码

                //foreach(QDOTSDetails details in source)
                //				{
                //					//error code,error code group
                //					object[] errorCodes = this.DataProvider.CustomQuery(typeof(QDOTSErrorCode),
                //						new SQLCondition(string.Format(@"select distinct {0} from tbltserrorcode a,tblecg b,tblec c 
                //							where a.tsid in {1} and a.ecgcode = b.ecgcode and a.ecode = c.ecode",
                //						@"a.tsid,
                //							  a.itemcode,
                //                              a.modelcode,
                //                              a.mocode,
                //                              a.ecode,
                //                              a.eattribute1,
                //                              a.ecgcode,
                //                              a.rcardseq,
                //                              a.mtime,
                //                              a.rcard,
                //                              a.mdate,
                //                              c.ecdesc,
                //                              a.muser,
                //                              b.ecgdesc",tsIDs)));
                //					
                //					//error codes
                //					if( errorCodes != null )
                //					{						
                //						foreach(QDOTSErrorCode code in errorCodes)
                //						{	
                //							details.ErrorCodes.Add( code );
                //
                //							//ErrorCause,Duty,ErrorSolution,ErrorLocation
                //							object[] errorCauses = this.DataProvider.CustomQuery(typeof(QDOTSErrorCause),
                //								new SQLParamCondition(string.Format(@"select {0} from tbltserrorcause a,tblduty b,tblecs c,tblsolution d 
                //									where a.tsid in {1} and a.ecgcode=$ERRORCODEGROUP and a.ecode=$ERRORCODE
                //									 and a.dutycode = b.dutycode and a.ecscode = c.ecscode and a.solcode = d.solcode",
                //								@"a.tsid,
                //									a.itemcode,
                //									a.modelcode,
                //									a.rcardseq,
                //									a.rcard,
                //									a.ecgcode,
                //									a.ropcode,
                //									a.ecode,
                //									a.dutycode,
                //									a.mocode,
                //									a.ecscode,
                //									a.eattribute1,
                //									a.rrescode,
                //									a.solcode,
                //									a.mtime,
                //									a.solmemo,
                //									a.mdate,
                //									b.dutydesc,
                //									a.muser,
                //									c.ecsdesc,
                //									d.soldesc",tsIDs),
                //								new SQLParameter[]{
                //													  new SQLParameter("ERRORCODEGROUP",typeof(String),	code.ErrorCodeGroup),
                //													  new SQLParameter("ERRORCODE",typeof(String),code.ErrorCode),	
                //							}));							
                //
                //							if( errorCauses != null )
                //							{
                //								foreach(QDOTSErrorCause cause in errorCauses)
                //								{
                //									if(cause.TSId == code.TSId)
                //									{
                //										code.ErrorCauses.Add( cause );
                //
                //										//error part
                //										cause.ErrorPartsList = this.DataProvider.CustomSearch(
                //											typeof(TSErrorCause2ErrorPart),
                //											new string[]{"TSId","ErrorCodeGroup","ErrorCode","ErrorCauseCode"},
                //											new object[]{cause.TSId,cause.ErrorCodeGroup,cause.ErrorCode,cause.ErrorCauseCode});
                //
                //										//error location
                //										cause.LocationList = this.DataProvider.CustomSearch(
                //											typeof(TSErrorCause2Location),
                //											new string[]{"TSId","ErrorCodeGroup","ErrorCode","ErrorCauseCode"},
                //											new object[]{cause.TSId,cause.ErrorCodeGroup,cause.ErrorCode,cause.ErrorCauseCode});
                //									}
                //
                //								}
                //							}
                //						}
                //					}

                #endregion

                #region 不良代码

                //error code,error code group
                object[] errorCodes = this.DataProvider.CustomQuery(typeof(QDOTSErrorCode),
                    new SQLCondition(string.Format(@"select distinct {0} from tbltserrorcode a,tblecg b,tblec c 
							where a.tsid in {1} and a.ecgcode = b.ecgcode and a.ecode = c.ecode",
                    @"a.tsid,
							  a.itemcode,
                              a.modelcode,
                              a.mocode,
                              a.ecode,
                              a.eattribute1,
                              a.ecgcode,
                              a.rcardseq,
                              a.mtime,
                              a.rcard,
                              a.mdate,
                              c.ecdesc,
                              a.muser,
                              b.ecgdesc", tsIDs)));

                #endregion

                #region 不良原因

                object[] errorCause = this.DataProvider.CustomQuery(typeof(QDOTSErrorCause),
                    new SQLCondition(string.Format(@"select {0} from tbltserrorcause a
												LEFT JOIN tblduty b ON (a.dutycode = b.dutycode)
												LEFT JOIN tblecs c ON (a.ecscode = c.ecscode)
												LEFT JOIN tblsolution d ON (a.solcode = d.solcode)
									where a.tsid in {1} ",
                    @"a.tsid,
									a.itemcode,
									a.modelcode,
									a.rcardseq,
									a.rcard,
									a.ecgcode,
									a.ropcode,
									a.ecode,
									a.dutycode,
									a.mocode,
									a.ecscode,
									a.eattribute1,
									a.rrescode,
									a.solcode,
									a.mtime,
									a.solmemo,
									a.mdate,
									b.dutydesc,
									a.muser,
									c.ecsdesc,
									d.soldesc", tsIDs)));

                #endregion

                #region 不良元件

                object[] errorPartsList = this.DataProvider.CustomQuery(typeof(TSErrorCause2ErrorPart),
                    new SQLCondition(string.Format(@"select TBLTSERRORCAUSE2EPART.* from TBLTSERRORCAUSE2EPART where tsid in {0} ", tsIDs)));

                #endregion

                #region 不良位置

                object[] locationList = this.DataProvider.CustomQuery(typeof(TSErrorCause2Location),
                    new SQLCondition(string.Format(@"select TBLTSERRORCAUSE2LOC.* from TBLTSERRORCAUSE2LOC where tsid in {0} ", tsIDs)));

                #endregion

                if (errorCodes != null)
                {
                    foreach (QDOTSErrorCode code in errorCodes)
                    {
                        details.ErrorCodes.Add(code);

                        if (errorCause != null)
                        {
                            foreach (QDOTSErrorCause cause in errorCause)
                            {
                                if (cause.TSId == code.TSId && cause.ErrorCode == code.ErrorCode)
                                {
                                    code.ErrorCauses.Add(cause);

                                    //error part
                                    ArrayList arrayErrorPart = new ArrayList();
                                    //if (errorPartsList != null)
                                    //    foreach (TSErrorCause2ErrorPart errorPart in errorPartsList)
                                    //    {
                                    //        if (errorPart.TSId == cause.TSId && errorPart.ErrorCode == code.ErrorCode)
                                    //        {
                                    //            arrayErrorPart.Add(errorPart);
                                    //        }
                                    //    }
                                   // TBLTSERRORCAUSE2EPART 已经不使用

                                    if (errorPartsList != null)
                                        foreach (TSErrorCause2ErrorPart errorPart in errorPartsList)
                                        {
                                            if (errorPart.TSId == cause.TSId && errorPart.ErrorCode == code.ErrorCode)
                                            {
                                                arrayErrorPart.Add(errorPart);
                                            }
                                        }
                                    cause.ErrorPartsList = (TSErrorCause2ErrorPart[])arrayErrorPart.ToArray(typeof(TSErrorCause2ErrorPart));

                                    //error location
                                    ArrayList arraylocation = new ArrayList();
                                    if (locationList != null)
                                        foreach (TSErrorCause2Location errorLocation in locationList)
                                        {
                                            if (errorLocation.TSId == cause.TSId && errorLocation.ErrorCode == code.ErrorCode)
                                            {
                                                arraylocation.Add(errorLocation);
                                            }
                                        }
                                    cause.LocationList = (TSErrorCause2Location[])arraylocation.ToArray(typeof(TSErrorCause2Location));
                                }

                            }
                        }
                    }
                }
                #endregion

                #region Change object to dimension
                ArrayList array = new ArrayList();

                object[] data = QDOTSDetails1.ToArray(details);
                if (data != null)
                {
                    foreach (QDOTSDetails1 details1 in data)
                    {
                        array.Add(details1);
                    }
                }

                //				foreach(QDOTSDetails detail in source)
                //				{
                //					object[] data = QDOTSDetails1.ToArray( detail );
                //					if( data != null )
                //					{
                //						foreach(QDOTSDetails1 details1 in data)
                //						{
                //							array.Add( details1 );
                //						}
                //					}
                //				}

                retSource = (QDOTSDetails1[])array.GetRange(0, System.Math.Min(array.Count, exclusive - inclusive)).ToArray(typeof(QDOTSDetails1));
                #endregion
            }

            return retSource;
        }


        public int QueryTSDetailsCount(
            string modelCode, string itemCode, string moCode,
            string sn, string tsState, string tsOperator, string sourceResourceCode, int sourceResourceDate, int sourceResourceTime)
        {
            string modelCondition = "";
            if (modelCode != "" && modelCode != null)
            {
                modelCondition += string.Format(@" and a.modelcode = '{0}'", modelCode);
            }

            string itemCondition = "";
            if (itemCode != "" && itemCode != null)
            {
                itemCondition += string.Format(@" and a.itemcode = '{0}'", itemCode);
            }

            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode = '{0}'", moCode);
            }

            string snCondition = "";
            if (sn != "" && sn != null)
            {
                snCondition += string.Format(@" and a.rcard = '{0}'", sn.ToUpper());
            }

            string tsStateCondition = "";
            if (tsState != "" && tsState != null)
            {
                tsStateCondition += string.Format(@" and tsstate = '{0}'", tsState);
            }

            string sourceResourceDateCondition = string.Format(@" and a.FRMDATE = {0}", sourceResourceDate);

            string sourceResourceTimeCondition = string.Format(@" and a.FRMTIME = {0}", sourceResourceTime);

#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(@"select count(a.rcard) 
					from 
					TBLTS a,tbltserrorcode b where a.tsid = b.tsid(+) 
					{0}{1}{2}{3}{4}{5}{6}",
                modelCondition, itemCondition, moCondition,
                snCondition, tsStateCondition, sourceResourceDateCondition, sourceResourceTimeCondition
                )).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(@"select count(a.rcard) 
					from 
					TBLTS a,tbltserrorcode b where a.tsid = b.tsid(+) 
					{0}{1}{2}{3}{4}{5}{6}",
                modelCondition, itemCondition, moCondition,
                snCondition, tsStateCondition, sourceResourceDateCondition, sourceResourceTimeCondition
                )));

        }

        #endregion

        #region mocode,rcard,rcardseq

        public object[] QueryTSDetails(
            string mocode, string rcard, int rcardseq,
            int inclusive, int exclusive)
        {
            string sql = string.Format(
                @" select distinct {0} from tblts a  left outer join tbluser on a.tsuser=tbluser.usercode where a.rcard = '{1}' and a.rcardseq = {2}  ",
                "a.tsid,a.TSMEMO,a.tsuser || ' - ' || tbluser.username as tsuser, a.tsdate ,a.tstime ", rcard, rcardseq);

            if (mocode == "")
            {

                sql += " and (a.mocode is null  or   a.mocode ='') ";
            }
            else
            {
                sql += " and a.mocode = '"+mocode+"' ";
            }
#if DEBUG
            Log.Info(
                new PagerCondition(
                sql,
                inclusive, exclusive, true).SQLText);
#endif
            object[] source = this.DataProvider.CustomQuery(
                typeof(QDOTSDetails),
                new PagerCondition(
                sql,
                inclusive, exclusive, true));

            object[] retSource = null;
            if (source != null)
            {
                #region Assign ErrorCodeGroup,ErrorCode
                foreach (QDOTSDetails details in source)
                {
                    //error code,error code group
                    object[] errorCodes = this.DataProvider.CustomQuery(typeof(QDOTSErrorCode),
                        new SQLParamCondition(string.Format(@"select {0} from tbltserrorcode a,tblecg b,tblec c 
							where a.tsid=$TSID and a.ecgcode = b.ecgcode and a.ecode = c.ecode",
                        @"a.tsid,
							  a.itemcode,
                              a.modelcode,
                              a.mocode,
                              a.ecode,
                              a.eattribute1,
                              a.ecgcode,
                              a.rcardseq,
                              a.mtime,
                              a.rcard,
                              a.mdate,
                              c.ecdesc,
                              a.muser,
                              b.ecgdesc"),
                        new SQLParameter[]{
											  new SQLParameter("TSID",typeof(String),details.TSID)
										  }));

                    //error codes
                    if (errorCodes != null)
                    {
                        foreach (QDOTSErrorCode code in errorCodes)
                        {
                            details.ErrorCodes.Add(code);

                            //ErrorCause,Duty,ErrorSolution,ErrorLocation
                            object[] errorCauses = this.DataProvider.CustomQuery(typeof(QDOTSErrorCause),
                                new SQLParamCondition(string.Format(@"select {0} from tbltserrorcause a
													LEFT JOIN tblduty b ON (a.dutycode = b.dutycode)
													LEFT JOIN tblecs c ON (a.ecscode = c.ecscode)
													LEFT JOIN tblsolution d ON (a.solcode = d.solcode)
                                                    LEFT JOIN tbltserrorcause2com e ON (a.tsid = e.tsid) and (a.ecscode = e.ecscode)
                                                    and (a.ecgcode = e.ecgcode) and  (a.ecode = e.ecode) and (a.ecsgcode=e.ecsgcode)
                                                    left join tbluser f on (a.muser=f.usercode)
									where a.tsid=$TSID and a.ecgcode=$ERRORCODEGROUP and a.ecode=$ERRORCODE",
                                @"a.tsid,
									a.itemcode,
									a.modelcode,
									a.rcardseq,
									a.rcard,
									a.ecgcode,
									a.ropcode,
									a.ecode,
									a.dutycode,
									a.mocode,
									a.ecscode,
									a.eattribute1,
									a.rrescode,
									a.solcode,
									a.mtime,
									a.solmemo,
									a.mdate,
                                    a.ecsgcode,
									b.dutydesc,
									a.muser || ' - ' || f.username as muser,
									c.ecsdesc,
									d.soldesc,
                                    e.Errorcomponent"),
                                new SQLParameter[]{
													  new SQLParameter("TSID",typeof(String),code.TSId),
													  new SQLParameter("ERRORCODEGROUP",typeof(String),	code.ErrorCodeGroup),
													  new SQLParameter("ERRORCODE",typeof(String),code.ErrorCode),	
							}));

                            if (errorCauses != null)
                            {
                                foreach (QDOTSErrorCause cause in errorCauses)
                                {
                                    code.ErrorCauses.Add(cause);

                                    //error part
                                    //cause.ErrorPartsList = this.DataProvider.CustomSearch(
                                    //    typeof(TSErrorCause2ErrorPart),
                                    //    new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                    //    new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });
                                    //marked by hiro 此表已不使用

                                    //error location
                                    cause.LocationList = this.DataProvider.CustomSearch(
                                        typeof(TSErrorCause2Location),
                                        new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                        new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });

                                }
                            }
                        }
                    }
                }
                #endregion

                #region Change object to dimension
                ArrayList array = new ArrayList();

                foreach (QDOTSDetails detail in source)
                {
                    object[] data = QDOTSDetails1.ToArray(detail);
                    if (data != null)
                    {
                        foreach (QDOTSDetails1 details1 in data)
                        {
                            array.Add(details1);
                        }
                    }
                }

                retSource = (QDOTSDetails1[])array.GetRange(0, System.Math.Min(array.Count, exclusive - inclusive)).ToArray(typeof(QDOTSDetails1));
                #endregion
            }

            return retSource;
        }


        public int QueryTSDetailsCount(
            string mocode, string rcard, int rcardseq)
        {

            string sql = string.Format(@"select count(a.rcard) 
					from 
					TBLTS a,tbltserrorcode b where a.tsid = b.tsid(+) and a.rcard = '{0}' and a.rcardseq = {1} ",
                rcard, rcardseq, mocode
                );

            if (mocode == "")
            {

                sql += " and ( a.mocode is null  or   a.mocode ='') ";
            }
            else
            {
                sql += " and a.mocode = '" + mocode + "' ";
            }
#if DEBUG
            Log.Info(
                new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(sql));

        }

        #endregion

        #region rcard,rcardseq

        public object[] QueryTSDetails(
            string rcard, int rcardseq,
            int inclusive, int exclusive)
        {
#if DEBUG
            Log.Info(
                new PagerCondition(
                string.Format(
                @" select distinct {0} from tblts left outer join tbluser on a.tsuser=tbluser.usercode where rcard = '{1}' and rcardseq = {2}",
                "a.tsid,a.TSMEMO,a.tsuser || ' - ' || tbluser.username as tsuser, a.tsdate ,a.tstime ", rcard, rcardseq),
                inclusive, exclusive, true).SQLText);
#endif
            object[] source = this.DataProvider.CustomQuery(
                typeof(QDOTSDetails),
                new PagerCondition(
                string.Format(
                @" select distinct {0} from tblts  left outer join tbluser on tblts.tsuser=tbluser.usercode where rcard = '{1}' and rcardseq = {2}",
                "tblts.tsid,tblts.tsmemo,tblts.tsuser || ' - ' || tbluser.username as tsuser", rcard, rcardseq),
                inclusive, exclusive, true));

            object[] retSource = null;
            if (source != null)
            {
                #region Assign ErrorCodeGroup,ErrorCode
                foreach (QDOTSDetails details in source)
                {
                    //error code,error code group
                    object[] errorCodes = this.DataProvider.CustomQuery(typeof(QDOTSErrorCode),
                        new SQLParamCondition(string.Format(@"select {0} from tbltserrorcode a,tblecg b,tblec c 
							where a.tsid=$TSID and a.ecgcode = b.ecgcode and a.ecode = c.ecode",
                        @"a.tsid,
							  a.itemcode,
                              a.modelcode,
                              a.mocode,
                              a.ecode,
                              a.eattribute1,
                              a.ecgcode,
                              a.rcardseq,
                              a.mtime,
                              a.rcard,
                              a.mdate,
                              c.ecdesc,
                              a.muser,
                              b.ecgdesc"),
                        new SQLParameter[]{
											  new SQLParameter("TSID",typeof(String),details.TSID)
										  }));

                    //error codes
                    if (errorCodes != null)
                    {
                        foreach (QDOTSErrorCode code in errorCodes)
                        {
                            details.ErrorCodes.Add(code);

                            //ErrorCause,Duty,ErrorSolution,ErrorLocation
                            object[] errorCauses = this.DataProvider.CustomQuery(typeof(QDOTSErrorCause),
                                new SQLParamCondition(string.Format(@"select {0} from tbltserrorcause a,tblduty b,tblecs c,tblsolution d  
									where a.tsid=$TSID and a.ecgcode=$ERRORCODEGROUP and a.ecode=$ERRORCODE
									 and a.dutycode = b.dutycode and a.ecscode = c.ecscode and a.solcode = d.solcode",
                                @"a.tsid,
									a.itemcode,
									a.modelcode,
									a.rcardseq,
									a.rcard,
									a.ecgcode,
									a.ropcode,
									a.ecode,
									a.dutycode,
									a.mocode,
									a.ecscode,
									a.eattribute1,
									a.rrescode,
									a.solcode,
									a.mtime,
									a.solmemo,
									a.mdate,
									b.dutydesc,
									a.muser,
									c.ecsdesc,
									d.soldesc"),
                                new SQLParameter[]{
													  new SQLParameter("TSID",typeof(String),code.TSId),
													  new SQLParameter("ERRORCODEGROUP",typeof(String),	code.ErrorCodeGroup),
													  new SQLParameter("ERRORCODE",typeof(String),code.ErrorCode),	
							}));

                            if (errorCauses != null)
                            {
                                foreach (QDOTSErrorCause cause in errorCauses)
                                {
                                    code.ErrorCauses.Add(cause);

                                    //error part
                                    cause.ErrorPartsList = this.DataProvider.CustomSearch(
                                        typeof(TSErrorCause2ErrorPart),
                                        new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                        new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });

                                    //error location
                                    cause.LocationList = this.DataProvider.CustomSearch(
                                        typeof(TSErrorCause2Location),
                                        new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                        new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });

                                }
                            }
                        }
                    }
                }
                #endregion

                #region Change object to dimension
                ArrayList array = new ArrayList();

                foreach (QDOTSDetails detail in source)
                {
                    object[] data = QDOTSDetails1.ToArray(detail);
                    if (data != null)
                    {
                        foreach (QDOTSDetails1 details1 in data)
                        {
                            array.Add(details1);
                        }
                    }
                }

                retSource = (QDOTSDetails1[])array.GetRange(0, System.Math.Min(array.Count, exclusive - inclusive)).ToArray(typeof(QDOTSDetails1));
                #endregion
            }

            return retSource;
        }


        public int QueryTSDetailsCount(
            string rcard, int rcardseq)
        {

#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(@"select count(a.rcard) 
					from 
					TBLTS a,tbltserrorcode b where a.tsid = b.tsid(+) and a.rcard = '{0}' and a.rcardseq = {1} ",
                rcard, rcardseq
                )).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(@"select count(a.rcard) 
					from 
					TBLTS a,tbltserrorcode b where a.tsid = b.tsid(+) and a.rcard = '{0}' and a.rcardseq = {1} ",
                rcard, rcardseq
                )));

        }

        #endregion

        #region 样本不良明细

        public object[] QueryOQCSampleNGDetails(
            string lotno,
            int inclusive, int exclusive)
        {

            string lotnoCondition = "";
            if (lotno != "" && lotno != null)
            {
                lotnoCondition += string.Format(@" and (b.rcard,b.rcardseq) in ( select rcard,rcardseq from tbllot2cardcheck where lotno ='{0}') ", lotno);
            }

            string sql = string.Format(
                @" select distinct {0} from tblts a,tbltserrorcode b,TBLTSERRORCAUSE c where a.tsid = b.tsid and a.tsid = c.tsid(+)  {1}",
                "a.tsid,A.RCARD,A.RCARDSEQ,a.TSMEMO,a.frmmemo",
                lotnoCondition);

#if DEBUG
            Log.Info(sql);
#endif
            object[] source = this.DataProvider.CustomQuery(
                typeof(QDOTSDetails),
                new PagerCondition(
                sql,
                inclusive, exclusive, true));

            object[] retSource = null;
            if (source != null)
            {
                #region Assign ErrorCodeGroup,ErrorCode
                foreach (QDOTSDetails details in source)
                {
                    //error code,error code group
                    object[] errorCodes = this.DataProvider.CustomQuery(typeof(QDOTSErrorCode),
                        new SQLParamCondition(string.Format(@"select {0} from tbltserrorcode a,tblecg b,tblec c 
							where a.rcardseq=" + details.RCARDSEQ + " and a.tsid=$TSID and a.ecgcode = b.ecgcode and a.ecode = c.ecode",
                        @"a.tsid,
							  a.itemcode,
                              a.modelcode,
                              a.mocode,
                              a.ecode,
                              a.eattribute1,
                              a.ecgcode,
                              a.rcardseq,
                              a.mtime,
                              a.rcard,
                              a.mdate,
                              c.ecdesc,
                              a.muser,
                              b.ecgdesc"),
                        new SQLParameter[]{
											  new SQLParameter("TSID",typeof(String),details.TSID)
										  }));

                    //error codes
                    if (errorCodes != null)
                    {
                        foreach (QDOTSErrorCode code in errorCodes)
                        {
                            details.ErrorCodes.Add(code);

                            //ErrorCause,Duty,ErrorSolution,ErrorLocation
                            object[] errorCauses = this.DataProvider.CustomQuery(typeof(QDOTSErrorCause),
                                new SQLParamCondition(string.Format(@"select {0} from tbltserrorcause a,tblduty b,tblecs c,tblsolution d,tbluser e "
                                    + "where a.rcardseq=" + details.RCARDSEQ + " and a.tsid=$TSID and a.ecgcode=$ERRORCODEGROUP and a.ecode=$ERRORCODE "
                                    + " and a.dutycode = b.dutycode and a.ecscode = c.ecscode and a.solcode = d.solcode and a.muser=e.usercode(+)",
                                @"a.tsid,
									a.itemcode,
									a.modelcode,
									a.rcardseq,
									a.rcard,
									a.ecgcode,
									a.ropcode,
									a.ecode,
									a.dutycode,
									a.mocode,
									a.ecscode,
									a.eattribute1,
									a.rrescode,
									a.solcode,
									a.mtime,
									a.solmemo,
									a.mdate,
									b.dutydesc,
									a.muser || ' - ' ||  e.username as muser,
									c.ecsdesc,
									d.soldesc"),
                                new SQLParameter[]{
													  new SQLParameter("TSID",typeof(String),code.TSId),
													  new SQLParameter("ERRORCODEGROUP",typeof(String),	code.ErrorCodeGroup),
													  new SQLParameter("ERRORCODE",typeof(String),code.ErrorCode),	
							}));

                            if (errorCauses != null)
                            {
                                foreach (QDOTSErrorCause cause in errorCauses)
                                {
                                    code.ErrorCauses.Add(cause);

                                    //error part
                                    cause.ErrorPartsList = this.DataProvider.CustomSearch(
                                        typeof(TSErrorCause2ErrorPart),
                                        new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                        new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });

                                    //error location
                                    cause.LocationList = this.DataProvider.CustomSearch(
                                        typeof(TSErrorCause2Location),
                                        new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                        new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });

                                }
                            }
                        }
                    }
                }
                #endregion

                #region Change object to dimension
                ArrayList array = new ArrayList();

                foreach (QDOTSDetails detail in source)
                {
                    object[] data = QDOTSDetails1.ToArray(detail);
                    if (data != null)
                    {
                        foreach (QDOTSDetails1 details1 in data)
                        {
                            array.Add(details1);
                        }
                    }
                }

                retSource = (QDOTSDetails1[])array.GetRange(0, System.Math.Min(array.Count, exclusive - inclusive)).ToArray(typeof(QDOTSDetails1));
                #endregion
            }

            return retSource;

        }


        public int QueryOQCSampleNGDetailsCount(string lotno)
        {

            string sql = string.Format(@"select count(rcard) from (select rcard from tblts b where  (b.rcard,b.rcardseq) in ( select rcard,rcardseq from tbllot2cardcheck where lotno ='{0}') group by rcard)", lotno);

#if DEBUG
            Log.Info(sql);
#endif
            int iCount = 0;
            try
            {
                iCount = this.DataProvider.GetCount(new SQLCondition(sql));
            }
            catch { }

            return iCount;

        }


        public object[] QueryOQCSampleNGDetails(
            string lotno, string rcard, string rcardseq, string mocode,
            int inclusive, int exclusive)
        {

            string lotnoCondition = string.Empty;
            if (lotno != "" && lotno != null)
            {
                lotnoCondition += string.Format(@" and (b.rcard,b.rcardseq) in ( select rcard,rcardseq from tbllot2cardcheck where lotno ='{0}') ", lotno);
            }

            if (rcard != "" && rcard != null)
            {
                lotnoCondition += string.Format(@" AND b.rcard = '{0}' ", rcard);
            }

            if (rcardseq != "" && rcardseq != null)
            {
                lotnoCondition += string.Format(@" AND b.RCARDSEQ = '{0}' ", rcardseq);
            }

            if (mocode != "" && mocode != null)
            {
                lotnoCondition += string.Format(@" AND b.MOCODE = '{0}' ", mocode);
            }

            string sql = string.Format(
                  @" select distinct {0} from tblts a,tbltserrorcode b,TBLTSERRORCAUSE c where a.tsid = b.tsid and a.tsid = c.tsid(+)  {1}",
                  "a.tsid,A.RCARD,A.RCARDSEQ,a.TSMEMO,a.frmmemo",
                  lotnoCondition);
#if DEBUG
            Log.Info(sql);
#endif
            object[] source = this.DataProvider.CustomQuery(
                typeof(QDOTSDetails),
                new PagerCondition(
                sql,
                inclusive, exclusive, true));

            object[] retSource = null;
            if (source != null)
            {
                #region Assign ErrorCodeGroup,ErrorCode
                foreach (QDOTSDetails details in source)
                {
                    //error code,error code group
                    // Changed by Icyer 2006/07/06
                    /*
                    object[] errorCodes = this.DataProvider.CustomQuery(typeof(QDOTSErrorCode),
                        new SQLParamCondition(string.Format(@"select {0} from tbltserrorcode a,tblecg b,tblec c 
                            where a.tsid=$TSID and a.ecgcode = b.ecgcode and a.ecode = c.ecode",
                        @"a.tsid,
                              a.itemcode,
                              a.modelcode,
                              a.mocode,
                              a.ecode,
                              a.eattribute1,
                              a.ecgcode,
                              a.rcardseq,
                              a.mtime,
                              a.rcard,
                              a.mdate,
                              c.ecdesc,
                              a.muser,
                              b.ecgdesc"),
                        new SQLParameter[]{
                                              new SQLParameter("TSID",typeof(String),details.TSID)
                                          }));
                    */
                    object[] errorCodes = this.DataProvider.CustomQuery(typeof(QDOTSErrorCode),
                        new SQLParamCondition(string.Format(@"select {0} from tbltserrorcode a,tblecg b,tblec c 
							where a.rcardseq=" + details.RCARDSEQ + " and a.tsid=$TSID  and a.ecgcode = b.ecgcode and a.ecode = c.ecode  ",
                        @"a.tsid,
							  a.itemcode,
                              a.modelcode,
                              a.mocode,
                              a.ecode,
                              a.eattribute1,
                              a.ecgcode,
                              a.rcardseq,
                              a.mtime,
                              a.rcard,							  
                              a.mdate,
                              c.ecdesc,
                              a.muser,
                              b.ecgdesc"),
                        new SQLParameter[]{
											  new SQLParameter("TSID",typeof(String),details.TSID)
										  }));
                    // Changed end

                    //error codes
                    if (errorCodes != null)
                    {
                        foreach (QDOTSErrorCode code in errorCodes)
                        {
                            details.ErrorCodes.Add(code);

                            //ErrorCause,Duty,ErrorSolution,ErrorLocation
                            // Changed by Icyer 2006/07/06
                            /*
                            object[] errorCauses = this.DataProvider.CustomQuery(typeof(QDOTSErrorCause),
                                new SQLParamCondition(string.Format(@"select {0} from tbltserrorcause a,tblduty b,tblecs c,tblsolution d 
                                    where a.tsid=$TSID and a.ecgcode=$ERRORCODEGROUP and a.ecode=$ERRORCODE
                                     and a.dutycode = b.dutycode and a.ecscode = c.ecscode and a.solcode = d.solcode",
                                @"a.tsid,
                                    a.itemcode,
                                    a.modelcode,
                                    a.rcardseq,
                                    a.rcard,
                                    a.ecgcode,
                                    a.ropcode,
                                    a.ecode,
                                    a.dutycode,
                                    a.mocode,
                                    a.ecscode,
                                    a.eattribute1,
                                    a.rrescode,
                                    a.solcode,
                                    a.mtime,
                                    a.solmemo,
                                    a.mdate,
                                    b.dutydesc,
                                    a.muser,
                                    c.ecsdesc,
                                    d.soldesc"),
                                new SQLParameter[]{
                                                      new SQLParameter("TSID",typeof(String),code.TSId),
                                                      new SQLParameter("ERRORCODEGROUP",typeof(String),	code.ErrorCodeGroup),
                                                      new SQLParameter("ERRORCODE",typeof(String),code.ErrorCode),	
                            }));							
                            */
                            object[] errorCauses = this.DataProvider.CustomQuery(typeof(QDOTSErrorCause),
                                new SQLParamCondition(string.Format(@"select {0} from tbltserrorcause a,tblduty b,tblecs c,tblsolution d,tbltserrorcause2com e,tbluser f "
                                    + " where a.rcardseq=" + details.RCARDSEQ + " and  a.tsid=$TSID and a.ecgcode=$ERRORCODEGROUP and a.ecode=$ERRORCODE "
                                    + " and a.dutycode = b.dutycode and a.ecscode = c.ecscode and a.solcode = d.solcode and a.tsid=e.tsid(+) "
                                    + " and a.ecscode=e.ecscode AND a.ecgcode=e.ecgcode(+) AND a.ecode=e.ecode(+) and a.ecsgcode=e.ecsgcode(+) "
                                    + " and a.muser=f.usercode(+) ",
                                @"a.tsid,
									a.itemcode,
									a.modelcode,
									a.rcardseq,
									a.rcard,
									a.ecgcode,
									a.ropcode,
									a.ecode,
									a.dutycode,
									a.mocode,
									a.ecscode,
									a.eattribute1,
									a.rrescode,
									a.solcode,
									a.mtime,
									a.solmemo,									
									a.mdate,
                                    a.ecsgcode,
									b.dutydesc,
									a.muser || ' - ' || f.username as muser,
									c.ecsdesc,
									d.soldesc,
                                    e.Errorcomponent"),
                                new SQLParameter[]{
													  new SQLParameter("TSID",typeof(String),code.TSId),
													  new SQLParameter("ERRORCODEGROUP",typeof(String),	code.ErrorCodeGroup),
													  new SQLParameter("ERRORCODE",typeof(String),code.ErrorCode),	
							}));
                            // Changed end

                            if (errorCauses != null)
                            {
                                foreach (QDOTSErrorCause cause in errorCauses)
                                {
                                    code.ErrorCauses.Add(cause);

                                    //error part
                                    //cause.ErrorPartsList = this.DataProvider.CustomSearch(
                                    //    typeof(TSErrorCause2ErrorPart),
                                    //    new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                    //    new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });
                                    //marked by hiro 此表已不使用

                                    //error location
                                    cause.LocationList = this.DataProvider.CustomSearch(
                                        typeof(TSErrorCause2Location),
                                        new string[] { "TSId", "ErrorCodeGroup", "ErrorCode", "ErrorCauseCode" },
                                        new object[] { cause.TSId, cause.ErrorCodeGroup, cause.ErrorCode, cause.ErrorCauseCode });

                                }
                            }
                        }
                    }
                }
                #endregion

                #region Change object to dimension
                ArrayList array = new ArrayList();

                foreach (QDOTSDetails detail in source)
                {
                    object[] data = QDOTSDetails1.ToArray(detail);
                    if (data != null)
                    {
                        foreach (QDOTSDetails1 details1 in data)
                        {
                            array.Add(details1);
                        }
                    }
                }

                retSource = (QDOTSDetails1[])array.GetRange(0, System.Math.Min(array.Count, exclusive - inclusive)).ToArray(typeof(QDOTSDetails1));
                #endregion
            }

            retSource = this.MatchReject2ErrorCode(lotno, rcard, retSource);

            return retSource;

        }

        //匹配抽检不良记录
        private object[] MatchReject2ErrorCode(string lotno, string rcard, object[] retSource)
        {
            //维修记录可能会比抽检不良记录多,因为维修可能发现和产生新的不良.
            //系统目前的处理方式为 : 没有在修品的批次 才可以判退. (即判退的批可能在维修记录中出现维修完成,报废,拆解的记录)

            BenQGuru.eMES.TSModel.TSModelFacade tsFacade = new BenQGuru.eMES.TSModel.TSModelFacade(this.DataProvider);

            #region Laws Lu,2006/08/09 write off
            //
            //			string lotnoCondition = string.Empty;
            //			if( lotno != "" && lotno != null )
            //			{
            //				lotnoCondition += string.Format(@" AND  lotno ='{0}' ",lotno);
            //			}
            //
            //			if( rcard != "" && rcard != null )
            //			{
            //				lotnoCondition += string.Format(@" AND rcard = '{0}' ",rcard);
            //			}
            //			string sql = string.Format(" SELECT tblreject2errorcode.* FROM tblreject2errorcode WHERE 1=1 {0} /*rcard in ( select rcard from tbllot2cardcheck where 1=1 {0}) */ ",lotnoCondition);
            //
            //			object[] reobjs = this.DataProvider.CustomQuery(typeof(Reject2ErrorCode),new SQLCondition(sql));

            #endregion

            ArrayList returnList = new ArrayList();
            //			if(reobjs !=null && reobjs.Length > 0 )
            //			{
            if (retSource != null)
            {
                #region Laws Lu,2006/08/09 write off

                //					Hashtable ht = new Hashtable();

                //					foreach(QDOTSDetails1 tsdetail in retSource)
                //					{
                ////						string htkey = tsdetail.RunningCard + tsdetail.ErrorCodeGroup + tsdetail.ErrorCode;
                ////						ht.Add(htkey,htkey);
                //					}



                //					foreach(Reject2ErrorCode r2eobj in reobjs)
                //					{
                //						string htkey = r2eobj.RunningCard + r2eobj.ErrorCodeGroup + r2eobj.ErrorCode;
                //						if(!ht.Contains(htkey))
                //						{
                //					BenQGuru.eMES.Domain.TSModel.ErrorCodeA ec = tsFacade.GetErrorCode(r2eobj.ErrorCode) as BenQGuru.eMES.Domain.TSModel.ErrorCodeA;
                //
                //					QDOTSDetails1 tempTSDetail = new QDOTSDetails1();
                //					tempTSDetail.RunningCard = r2eobj.RunningCard;
                //					tempTSDetail.ErrorCodeGroup = r2eobj.ErrorCodeGroup;
                //					tempTSDetail.ErrorCodeGroupDescription = r2eobj.ErrorCodeGroup;
                //					tempTSDetail.ErrorCode = r2eobj.ErrorCode;
                //					if(ec != null)
                //						tempTSDetail.ErrorCodeDescription = ec.ErrorDescription;
                //					else
                //						tempTSDetail.ErrorCodeDescription = r2eobj.ErrorCode;

                //					returnList.Add(tempTSDetail);
                //						}
                //						
                //					}

                #endregion

                foreach (QDOTSDetails1 tsdetail in retSource)
                {
                    BenQGuru.eMES.Domain.TSModel.ErrorCodeA ec = tsFacade.GetErrorCode(tsdetail.ErrorCode) as BenQGuru.eMES.Domain.TSModel.ErrorCodeA;

                    QDOTSDetails1 tempTSDetail = new QDOTSDetails1();
                    tempTSDetail.RunningCard = tsdetail.RunningCard;
                    tempTSDetail.ErrorCodeGroup = tsdetail.ErrorCodeGroup;
                    tempTSDetail.ErrorCodeGroupDescription = tsdetail.ErrorCodeGroup;
                    tempTSDetail.ErrorCode = tsdetail.ErrorCode;

                    if (ec != null)
                        tempTSDetail.ErrorCodeDescription = ec.ErrorDescription;
                    else
                        tempTSDetail.ErrorCodeDescription = tsdetail.ErrorCode;

                    returnList.Add(tsdetail);
                }
                return (QDOTSDetails1[])returnList.ToArray(typeof(QDOTSDetails1));

                #region Laws Lu,2006/08/09 write off

                //				}
                //				else
                //				{
                //					foreach(Reject2ErrorCode r2eobj in reobjs)
                //					{
                //						QDOTSDetails1 tempTSDetail = new QDOTSDetails1();
                //
                //						BenQGuru.eMES.Domain.TSModel.ErrorCodeA ec = tsFacade.GetErrorCode(r2eobj.ErrorCode) as BenQGuru.eMES.Domain.TSModel.ErrorCodeA;
                //
                //						tempTSDetail.RunningCard = r2eobj.RunningCard;
                //						tempTSDetail.ErrorCodeGroup = r2eobj.ErrorCodeGroup;
                //						tempTSDetail.ErrorCodeGroupDescription = r2eobj.ErrorCodeGroup;
                //						tempTSDetail.ErrorCode = r2eobj.ErrorCode;
                //
                //						if(ec != null)
                //							tempTSDetail.ErrorCodeDescription = ec.ErrorDescription;
                //						else
                //							tempTSDetail.ErrorCodeDescription = r2eobj.ErrorCode;
                //						returnList.Add(tempTSDetail);
                //						
                //					}
                //					return (QDOTSDetails1[])returnList.ToArray(typeof(QDOTSDetails1));
                //				}
                //			}
                #endregion

            }
            return retSource;
        }


        public int QueryOQCSampleNGDetailsCount(string lotno, string rcard, string rcardseq, string mocode)
        {
            /*
            string lotnoCondition = string.Empty;
            if( lotno != "" && lotno != null )
            {
                lotnoCondition += string.Format(@" and b.rcard in ( select rcard from tbllot2cardcheck where lotno ='{0}') ",lotno);
            }

            if( rcard != "" && rcard != null )
            {
                lotnoCondition += string.Format(@" AND b.rcard = '{0}' ",rcard);
            }

            if( rcardseq != "" && rcardseq != null )
            {
                lotnoCondition += string.Format(@" AND b.RCARDSEQ = '{0}' ",rcardseq);
            }

            if( mocode != "" && mocode != null )
            {
                lotnoCondition += string.Format(@" AND b.MOCODE = '{0}' ",mocode);
            }

            string sql = string.Format(
                @" select count(a.rcard) from tblts a,tbltserrorcode b,TBLTSERRORCAUSE c where a.tsid = b.tsid and a.tsid = c.tsid(+)  {0}",
                lotnoCondition);

#if DEBUG
            Log.Info(sql);
#endif
            return this.DataProvider.GetCount(new SQLCondition(sql));
            */

            //string sql = string.Empty;
            string sql = string.Format(@"select count(rcard) from (select rcard from tblts b where  (b.rcard,b.rcardseq) in ( select rcard,rcardseq from tbllot2cardcheck where lotno ='{0}') ", lotno);
            if (rcard != string.Empty)
                sql += " AND rcard='" + rcard + "' ";
            if (rcardseq != string.Empty)
                sql += " AND rcardseq=" + rcardseq;
            if (mocode != string.Empty)
                sql += " AND MOCode='" + mocode + "' ";

            sql += " group by rcard) ";
            int iCount = 0;
            try
            {
#if DEBUG
                Log.Info(sql);
#endif
                iCount = this.DataProvider.GetCount(new SQLCondition(sql));
            }
            catch { }

            return iCount;

        }

        #endregion
    }
}
