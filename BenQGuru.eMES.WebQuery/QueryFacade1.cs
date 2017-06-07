using System;
using System.Collections;
using System.Collections.Specialized;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;

/* 报表查询常用表tblrptreallineqty 统计字段含义
  tblrptreallineqty
				inputqty	 中间产量投入数
				eattribute1	 工单投入数
				EATTRIBUTE2	 通过数 
				outputqty	 产量 （qtyflay 为 Y 表示工单产量，为N表示中间产量）
				ALLGOODQTY   直通数量	（产线 工段）
				moallgoodqty 直通数量 （工单 机种 产品）

 */

namespace BenQGuru.eMES.WebQuery
{
    /// <summary>
    /// QueryFacade 的摘要说明。
    /// </summary>
    public class QueryFacade1
    {
        private IDomainDataProvider _domainDataProvider = null;

        public static int Denominator_Replacer = -1;

        public QueryFacade1(IDomainDataProvider domainDataProvider)
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

        #region OQC FirstHandingYield
        public object[] QueryFirstHandingYield(
            string modelCode, string itemCode,
            int startDate, int endDate,
            int inclusive, int exclusive)
        {
            string itemCondition = string.Empty;
            if (itemCode != null && itemCode != "")
            {
                itemCondition = string.Format(" and itemcode in ( {0} ) ", FormatHelper.ProcessQueryValues(itemCode));
            }
            string shiftDayCondition = FormatHelper.GetDateRangeSql("tbllot.mdate", startDate, endDate);

            string sqlStr = string.Format(@"SELECT   modelcode, itemcode, SUM (lotsize) AS amount,
					SUM (pass_amount) AS yield_amount,
					DECODE (SUM (lotsize),
							0, 0,
							SUM (pass_amount) / SUM (lotsize)
							) AS yield_percent
				FROM (SELECT   DECODE (lotstatus,
									'oqclotstatus_pass', lotsize - SUM (ngresult),
									0
									) AS pass_amount,
							SUM (ngresult) AS ngcount, itemcode,modelcode,
							lotno, lotsize, lotstatus
						FROM (SELECT   MIN (  tbllot2cardcheck.mdate * 1000000
											+ tbllot2cardcheck.mtime
											) AS mintime,
										CASE tbllot2cardcheck.status
											WHEN 'NG'
											THEN 1
											ELSE 0
										END AS ngresult,
										tbllot2cardcheck.itemcode,
										tbllot2cardcheck.modelcode,
										tbllot2cardcheck.lotno, tbllot2cardcheck.rcard,
										tbllot2cardcheck.status, tbllot.lotsize,
										lotstatus
									FROM tbllot2cardcheck JOIN tbllot ON (    tbllot2cardcheck.lotno =
																				tbllot.lotno
																		{0}{1}
																		AND oqclottype ='oqclottype_normal'
																		AND tbllot.lotstatus IN('oqclotstatus_pass','oqclotstatus_reject')
																		)
								GROUP BY tbllot2cardcheck.rcard,
										tbllot2cardcheck.status,
										tbllot2cardcheck.itemcode,
										tbllot2cardcheck.modelcode,
										tbllot2cardcheck.lotno,
										lotsize,
										lotstatus)
					GROUP BY itemcode, modelcode, lotno, lotsize, lotstatus)
			GROUP BY itemcode, modelcode", shiftDayCondition, itemCondition);
#if DEBUG

            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(OQCFirstHandingYield),
                new PagerCondition(sqlStr, "modelcode,itemcode", inclusive, exclusive));
        }

        public int QueryFirstHandingYieldCount(string modelCode, string itemCode, int startDate, int endDate)
        {
            string itemCondition = "";
            if (itemCode != null && itemCode != "")
            {
                itemCondition = string.Format(" and itemcode in ( {0} ) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            string shiftDayCondition = FormatHelper.GetDateRangeSql("tbllot.mdate", startDate, endDate);

            string sqlStr = string.Format(
                @"select count(itemcode) from (SELECT   modelcode, itemcode, SUM (lotsize) AS amount,
					SUM (pass_amount) AS yield_amount,
					DECODE (SUM (lotsize),
							0, 0,
							SUM (pass_amount) / SUM (lotsize)
							) AS yield_percent
				FROM (SELECT   DECODE (lotstatus,
									'oqclotstatus_pass', lotsize - SUM (ngresult),
									0
									) AS pass_amount,
							SUM (ngresult) AS ngcount, itemcode,modelcode,
							lotno, lotsize, lotstatus
						FROM (SELECT   MIN (  tbllot2cardcheck.mdate * 1000000
											+ tbllot2cardcheck.mtime
											) AS mintime,
										CASE tbllot2cardcheck.status
											WHEN 'NG'
											THEN 1
											ELSE 0
										END AS ngresult,
										tbllot2cardcheck.itemcode,
										tbllot2cardcheck.modelcode,
										tbllot2cardcheck.lotno, tbllot2cardcheck.rcard,
										tbllot2cardcheck.status, tbllot.lotsize,
										lotstatus
									FROM tbllot2cardcheck JOIN tbllot ON (    tbllot2cardcheck.lotno =
																				tbllot.lotno
																		{0}{1}
																		AND oqclottype ='oqclottype_normal'
																		AND tbllot.lotstatus IN('oqclotstatus_pass','oqclotstatus_reject')
																		)
								GROUP BY tbllot2cardcheck.rcard,
										tbllot2cardcheck.status,
										tbllot2cardcheck.itemcode,
										tbllot2cardcheck.modelcode,
										tbllot2cardcheck.lotno,
										lotsize,
										lotstatus)
					GROUP BY itemcode, modelcode, lotno, lotsize, lotstatus)
			GROUP BY itemcode, modelcode)", shiftDayCondition, itemCondition);

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.GetCount(
                new SQLCondition(sqlStr));
        }
        #endregion

        #region OQC FirstHandingYieldDetail
        public object[] QueryFirstHandingYieldDetail(
            string modelCode, string itemCode,
            int startDate, int endDate,
            int inclusive, int exclusive)
        {
            string itemCondition = string.Empty;
            if (itemCode != null && itemCode != "")
            {
                itemCondition = string.Format(" and itemcode in ( {0} )", FormatHelper.ProcessQueryValues(itemCode));
            }

            string shiftDayCondition = FormatHelper.GetDateRangeSql("tbllot.mdate", startDate, endDate);

            //			string sqlStr = string.Format(@"SELECT tbllot.lotno, tbllot.lotsize, tbllot.ssize, tbllot.mdate, tbllot.mtime,
            //						tbllot.lotstatus, tbllot.muser, tbloqclotcklist.agradetimes,
            //						tbloqclotcklist.bggradetimes, tbloqclotcklist.cgradetimes
            //					FROM tbllot JOIN tbloqclotcklist ON (tbloqclotcklist.lotno = tbllot.lotno)
            //					WHERE 1=1 {0}
            //						AND oqclottype = 'oqclottype_normal'
            //						AND tbllot.lotstatus IN('oqclotstatus_pass','oqclotstatus_reject')
            //						AND tbllot.lotno IN (
            //							SELECT DISTINCT lotno FROM tbllot2card
            //										WHERE  itemcode ='{1}')",shiftDayCondition,itemCode);

            string sqlStr = string.Format(@"SELECT   COUNT (distinct tbllot2cardcheck.rcard) AS actcheckcount, tbllot.lotno,
									tbllot.lotsize, tbllot.ssize, tbllot.mdate, tbllot.mtime,
									tbllot.lotstatus, tbllot.muser, tbloqclotcklist.agradetimes,
									tbloqclotcklist.bggradetimes, tbloqclotcklist.cgradetimes,tbloqclotcklist.zgradetimes
								FROM tbllot2cardcheck, tbllot, tbloqclotcklist
							WHERE tbllot2cardcheck.lotno = tbllot.lotno
								AND tbllot.lotno = tbloqclotcklist.lotno
								AND tbllot.lotstatus IN ('oqclotstatus_pass', 'oqclotstatus_reject')
								AND tbllot.oqclottype = 'oqclottype_normal'
								{0}
								AND tbllot.lotno IN (SELECT lotno
														FROM tbllot2cardcheck
													WHERE 1 = 1 AND itemcode = '{1}')
							GROUP BY tbllot.lotno,
									tbllot.lotsize,
									tbllot.ssize,
									tbllot.mdate,
									tbllot.mtime,
									tbllot.lotstatus,
									tbllot.muser,
									tbloqclotcklist.agradetimes,
									tbloqclotcklist.bggradetimes,
									tbloqclotcklist.cgradetimes,
                                    tbloqclotcklist.zgradetimes", shiftDayCondition, itemCode);
#if DEBUG

            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(OQCFirstHandingYieldDetail),
                new PagerCondition(sqlStr, inclusive, exclusive, true));
        }

        public int QueryFirstHandingYieldDetailCount(string modelCode, string itemCode, int startDate, int endDate)
        {
            string itemCondition = "";
            if (itemCode != null && itemCode != "")
            {
                itemCondition = string.Format(" and itemcode in ( {0} )", FormatHelper.ProcessQueryValues(itemCode));
            }
            string shiftDayCondition = FormatHelper.GetDateRangeSql("tbllot.mdate", startDate, endDate);
            //			string sqlStr = string.Format(@"select count(tbllot.LOTNO) 
            //					FROM tbllot JOIN tbloqclotcklist ON (tbloqclotcklist.lotno = tbllot.lotno)
            //					WHERE 1=1 {0}
            //						AND oqclottype = 'oqclottype_normal'
            //						AND tbllot.lotstatus IN('oqclotstatus_pass','oqclotstatus_reject')
            //						AND tbllot.lotno IN (
            //							SELECT DISTINCT lotno FROM tbllot2card
            //										WHERE  itemcode ='{1}')",shiftDayCondition,itemCode);

            string sqlStr = string.Format(@"select count(LOTNO) 
					FROM (SELECT   COUNT (tbllot2cardcheck.rcard) AS actcheckcount, tbllot.lotno,
									tbllot.lotsize, tbllot.ssize, tbllot.mdate, tbllot.mtime,
									tbllot.lotstatus, tbllot.muser, tbloqclotcklist.agradetimes,
									tbloqclotcklist.bggradetimes, tbloqclotcklist.cgradetimes, tbloqclotcklist.zgradetimes
								FROM tbllot2cardcheck, tbllot, tbloqclotcklist
							WHERE tbllot2cardcheck.lotno = tbllot.lotno
								AND tbllot.lotno = tbloqclotcklist.lotno
								AND tbllot.lotstatus IN ('oqclotstatus_pass', 'oqclotstatus_reject')
								AND tbllot.oqclottype = 'oqclottype_normal'
								{0}
								AND tbllot.lotno IN (SELECT lotno
														FROM tbllot2cardcheck
													WHERE 1 = 1 AND itemcode = '{1}')
							GROUP BY tbllot.lotno,
									tbllot.lotsize,
									tbllot.ssize,
									tbllot.mdate,
									tbllot.mtime,
									tbllot.lotstatus,
									tbllot.muser,
									tbloqclotcklist.agradetimes,
									tbloqclotcklist.bggradetimes,
									tbloqclotcklist.cgradetimes,
                                    tbloqclotcklist.zgradetimes)", shiftDayCondition, itemCode);

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.GetCount(
                new SQLCondition(sqlStr));
        }
        #endregion

        #region HistroyQuantitySummary
        /// <summary>
        /// modify desc : 历史产量不再判断qtyflag,CS端采集的时候统一以小板处理 (即乘以分板比例的数量) 查询条件添加工厂查询.
        /// author	   : Simone Xu
        /// date		   : 2005/08/29
        /// </summary>
        /// <param name="modelCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="moCode"></param>
        /// <param name="opCode"></param>
        /// <param name="segmentCode"></param>
        /// <param name="stepSequenceCode"></param>
        /// <param name="resourceCode"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="factorycode"></param>
        /// <param name="timingType"></param>
        /// <param name="summaryTarget"></param>
        /// <param name="visibleStyle"></param>
        /// <param name="chartStyle"></param>
        /// <returns></returns>
        public object[] QueryHistoryQuantitySummary(
            string modelCode, string itemCode, string moCode, string opCode,
            string segmentCode, string stepSequenceCode, string resourceCode, NameValueCollection iCondition,
            int startDate, int endDate,
            string timingType, string summaryTarget,
            string visibleStyle, string chartStyle)
        {

            //(产出数)产量查询 需要根据业务判断qtyflag ,即判断是否最后工序产量
            //(投入数)查询 不需要判断qtyflag

            string table = "";
            string selectFields = "";
            string selectFields2 = string.Empty; //投入数选择字段
            string condition = " 1=1 ";
            string groupBy = "";
            string orderBy = "";

            #region Summary Target

            //如果统计对象是工序,工段,产线,资源,拥有以下查询条件
            string iModelCodeCondition = string.Empty;					//机种 条件
            string iItemCodeCondition = string.Empty;					//产品 条件 
            string iMoCodeCondition = string.Empty;						//工单 条件
            if (iCondition != null && iCondition.Count > 0)
            {
                string iModelCode = iCondition["iModelCode"];			//机种Code
                string iItemCode = iCondition["iItemCode"];				//产品Code
                string iMoCode = iCondition["iMoCode"];					//工单Code

                if (iModelCode != null && iModelCode != string.Empty)
                {
                    iModelCodeCondition = string.Format(" AND modelcode in ({0}) ", FormatHelper.ProcessQueryValues(iModelCode));
                }
                if (iItemCode != null && iItemCode != string.Empty)
                {
                    iItemCodeCondition = string.Format(" AND itemcode in ({0}) ", FormatHelper.ProcessQueryValues(iItemCode));
                }
                if (iMoCode != null && iMoCode != string.Empty)
                {
                    iMoCodeCondition = string.Format(" AND mocode in ({0}) ", FormatHelper.ProcessQueryValues(iMoCode));
                }
            }

            if (summaryTarget == SummaryTarget.Model)//机种
            {
                selectFields += " modelcode,sum(decode(qtyflag,'Y',OUTPUTQTY,0)) as OUTPUTQTY,sum(eattribute1) as INPUTQTY, ";
                selectFields2 += " modelcode,sum(eattribute1) as INPUTQTY, ";
                groupBy += " modelcode, ";
                orderBy += " modelcode, ";
                table = " TBLRPTREALLINEQTY ";
                //condition += " and upper(qtyflag) = 'Y' ";
                if (modelCode != "" && modelCode != null)
                {
                    condition += " and modelcode in (" + FormatHelper.ProcessQueryValues(modelCode.ToUpper()) + ")";
                }
            }
            else if (summaryTarget == SummaryTarget.Item)//产品
            {
                selectFields += " itemcode,sum(decode(qtyflag,'Y',OUTPUTQTY,0)) as OUTPUTQTY,sum(eattribute1) as INPUTQTY, ";
                selectFields2 += " itemcode,sum(eattribute1) as INPUTQTY, ";
                groupBy += " itemcode, ";
                orderBy += " itemcode, ";
                table = " TBLRPTREALLINEQTY ";
                //condition += " and upper(qtyflag) = 'Y' ";
                if (itemCode != "" && itemCode != null)
                {
                    condition += " and itemcode in (" + FormatHelper.ProcessQueryValues(itemCode.ToUpper()) + ")";
                }
            }
            else if (summaryTarget == SummaryTarget.Mo)//工单
            {
                selectFields += " mocode,sum(decode(qtyflag,'Y',OUTPUTQTY,0)) as OUTPUTQTY, sum(eattribute1) as INPUTQTY, ";
                selectFields2 += " mocode,sum(eattribute1) as INPUTQTY, ";
                groupBy += " mocode, ";
                orderBy += " mocode, ";
                table = " TBLRPTREALLINEQTY ";
                //condition += " and upper(qtyflag) = 'Y' ";
                if (moCode != "" && moCode != null)
                {
                    condition += " and mocode in (" + FormatHelper.ProcessQueryValues(moCode.ToUpper()) + ")";
                }
            }
            else if (summaryTarget == SummaryTarget.Operation)//工序
            {
                selectFields += " opcode, sum(OUTPUTQTY) as OUTPUTQTY,  sum(EATTRIBUTE2) as INPUTQTY, "; //工序是用通过数来表示投入数
                selectFields2 += " opcode, sum(EATTRIBUTE2) as INPUTQTY, ";
                groupBy += " opcode, ";
                orderBy += " opcode,";
                table = " TBLRPTHISOPQTY ";
                if (opCode != "" && opCode != null)
                {
                    condition += " and opcode in (" + FormatHelper.ProcessQueryValues(opCode.ToUpper()) + ")";
                }
                condition += iModelCodeCondition;
                condition += iItemCodeCondition;
                condition += iMoCodeCondition;
            }
            else if (summaryTarget == SummaryTarget.Segment)//工段
            {
                selectFields += " segcode,sum(OUTPUTQTY) as OUTPUTQTY, sum(eattribute1 + inputqty) as INPUTQTY, ";
                selectFields2 += " segcode,sum(eattribute1 + inputqty) as INPUTQTY, ";
                groupBy += " segcode, ";
                orderBy += " segcode, ";
                table = " TBLRPTREALLINEQTY ";

                //added by jessie lee,2005/9/28,for CS0052
                //最后工序的产量，也包含中间产量工序的产量
                //condition += " and upper(qtyflag) = 'Y' ";

                if (segmentCode != "" && segmentCode != null)
                {
                    condition += "and segcode in (" + FormatHelper.ProcessQueryValues(segmentCode.ToUpper()) + ")";
                }
                condition += iModelCodeCondition;
                condition += iItemCodeCondition;
                condition += iMoCodeCondition;
            }
            else if (summaryTarget == SummaryTarget.StepSequence)//生产线
            {
                selectFields += " sscode,sum(OUTPUTQTY) as OUTPUTQTY, sum(eattribute1 + inputqty) as INPUTQTY, ";
                selectFields2 += " sscode,sum(eattribute1 + inputqty) as INPUTQTY, ";
                groupBy += " sscode, ";
                orderBy += " sscode, ";
                table = " TBLRPTREALLINEQTY ";

                //added by jessie lee,2005/9/28,for CS0052
                //condition += " and upper(qtyflag) = 'Y' ";

                if (stepSequenceCode != "" && stepSequenceCode != null)
                {
                    condition += " and SSCODE in (" + FormatHelper.ProcessQueryValues(stepSequenceCode.ToUpper()) + ")";
                }
                condition += iModelCodeCondition;
                condition += iItemCodeCondition;
                condition += iMoCodeCondition;
            }
            else if (summaryTarget == SummaryTarget.Resource)//资源
            {
                selectFields += " rescode,sum(OUTPUTQTY) as OUTPUTQTY,sum(EATTRIBUTE2) as INPUTQTY, "; //资源是用通过数来表示投入数
                selectFields2 += " rescode,sum(EATTRIBUTE2) as INPUTQTY, ";
                groupBy += " rescode, ";
                orderBy += " rescode, ";
                table = " TBLRPTHISOPQTY ";
                if (resourceCode != "" && resourceCode != null)
                {
                    condition += " and rescode in (" + FormatHelper.ProcessQueryValues(resourceCode.ToUpper()) + ")";
                }
                condition += iModelCodeCondition;
                condition += iItemCodeCondition;
                condition += iMoCodeCondition;
            }
            else
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", summaryTarget);
            }

            condition += FormatHelper.GetDateRangeSql("shiftday", startDate, endDate);
            #endregion

            #region TimingType
            if (timingType == TimingType.TimePeriod)
            {
                selectFields += " TPCODE, ";
                groupBy += " TPCODE, ";
                orderBy += " TPCODE, ";
            }
            else if (timingType == TimingType.Shift)
            {
                selectFields += " SHIFTCODE, ";
                groupBy += " SHIFTCODE, ";
                orderBy += " SHIFTCODE, ";
            }
            else if (timingType == TimingType.Day)
            {
                selectFields += " SHIFTDAY, ";
                groupBy += " SHIFTDAY, ";
                orderBy += " SHIFTDAY, ";
            }
            else if (timingType == TimingType.Week)
            {
                selectFields += " WEEK,SUBSTR(SHIFTDAY,0,4) AS SHIFTDAY, ";
                groupBy += " WEEK,SUBSTR(SHIFTDAY,0,4), ";
                orderBy += " WEEK,SUBSTR(SHIFTDAY,0,4), ";
            }
            else if (timingType == TimingType.Month)
            {
                selectFields += " MONTH,SUBSTR(SHIFTDAY,0,4) AS SHIFTDAY, ";
                groupBy += " MONTH,SUBSTR(SHIFTDAY,0,4), ";
                orderBy += " MONTH,SUBSTR(SHIFTDAY,0,4), ";
            }
            else
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", timingType);
            }
            #endregion

            #region Process String
            selectFields = selectFields.Substring(0, selectFields.TrimEnd().Length - 1);
            groupBy = groupBy.Substring(0, groupBy.TrimEnd().Length - 1);
            orderBy = orderBy.Substring(0, orderBy.TrimEnd().Length - 1);
            #endregion

            #region Sql Assemble
#if DEBUG
            Log.Info(string.Format(
                @"select {0} from {1} where {2} group by {3} order by {4}",
                selectFields, table, condition, groupBy, orderBy));
#endif

            string sqlQuantity = string.Format(
                @"select {0} from {1} where {2} group by {3} order by {4}",
                selectFields, table, condition, groupBy, orderBy);

            //			string sqlInputQty = string.Format(
            //				@"select {0} from {1} where {2} group by {3} order by {4}",
            //				selectFields2,table,condition,groupBy,orderBy);

            //产量查询
            object[] quantitySummaryObjs = this.DataProvider.CustomQuery(
                typeof(HistroyQuantitySummary),
                new SQLCondition(sqlQuantity));

            //			//投入数查询
            //			object[] inputQtySummaryObjs = this.DataProvider.CustomQuery(
            //				typeof(HistroyQuantitySummary),
            //				new SQLCondition(sqlQuantity));
            //
            //			#region Map 产量查询 和 投入数查询
            //
            //			Hashtable InputQtySummaryHT = new Hashtable();
            //			if(inputQtySummaryObjs != null)
            //			{
            //				foreach(HistroyQuantitySummary inputSummary in inputQtySummaryObjs)
            //				{
            //					InputQtySummaryHT.Add(this.getHtKey(inputSummary),inputSummary.InputQty);
            //				}
            //			}
            //
            //			if(quantitySummaryObjs != null)
            //			{
            //				foreach(HistroyQuantitySummary quantitySummary in quantitySummaryObjs)
            //				{
            //					string htKey = this.getHtKey(quantitySummary);
            //					if(InputQtySummaryHT.Contains(htKey))
            //					{
            //						quantitySummary.InputQty = Convert.ToInt32(InputQtySummaryHT[htKey]);
            //					}
            //				}
            //			}
            //
            //			#endregion

            return quantitySummaryObjs;
            #endregion
        }

        //根据历史产量对象构造HashKey
        private string getHtKey(HistroyQuantitySummary hisQty)
        {
            System.Text.StringBuilder htKeyBuild = new System.Text.StringBuilder();

            #region 构造htkey

            if (hisQty.ModelCode != null)
            {
                htKeyBuild.Append(hisQty.ModelCode);
            }
            if (hisQty.ItemCode != null)
            {
                htKeyBuild.Append(hisQty.ItemCode);
            }
            if (hisQty.MoCode != null)
            {
                htKeyBuild.Append(hisQty.MoCode);
            }
            if (hisQty.OperationCode != null)
            {
                htKeyBuild.Append(hisQty.OperationCode);
            }
            if (hisQty.SegmentCode != null)
            {
                htKeyBuild.Append(hisQty.SegmentCode);
            }
            if (hisQty.StepSequenceCode != null)
            {
                htKeyBuild.Append(hisQty.StepSequenceCode);
            }
            if (hisQty.ResourceCode != null)
            {
                htKeyBuild.Append(hisQty.ResourceCode);
            }
            if (hisQty.Month != null)
            {
                htKeyBuild.Append(hisQty.Month);
            }
            if (hisQty.Week != null)
            {
                htKeyBuild.Append(hisQty.Week);
            }
            htKeyBuild.Append(hisQty.NatureDate);
            htKeyBuild.Append(hisQty.ShiftDay);
            if (hisQty.ShiftCode != null)
            {
                htKeyBuild.Append(hisQty.ShiftCode);
            }
            if (hisQty.TimePeriodCode != null)
            {
                htKeyBuild.Append(hisQty.TimePeriodCode);
            }

            #endregion

            return htKeyBuild.ToString();

        }
        #endregion

        #region HistoryYieldPercent
        public object[] QueryHistoryYieldPercent(
            string modelCode, string itemCode, string moCode, string opCode,
            string segmentCode, string stepSequenceCode, string resourceCode,
            int startDate, int endDate, NameValueCollection ppmSSCondition,
            string timingType, string summaryTarget, string yieldCatalog,
            string visiableStyle, string chartStyle)
        {
            string table = "";
            string selectFields = "";
            string condition = " 1=1 ";
            string groupBy = "";
            string orderBy = "";

            #region Summary Target

            //产品ppm 特殊查询条件
            string ppmModelCodeCondition = string.Empty;					//机种 条件
            string ppmItemCodeCondition = string.Empty;						//产品 条件 
            string ppmItemCodeCondition2 = string.Empty;						//产品 条件 
            string ppmMoCodeCondition = string.Empty;						//工单 条件
            if (ppmSSCondition != null && ppmSSCondition.Count > 0)
            {
                string ppmModelCode = ppmSSCondition["ppmModelCode"];			//机种Code
                string ppmItemCode = ppmSSCondition["ppmItemCode"];				//产品Code
                string ppmMoCode = ppmSSCondition["ppmMoCode"];					//工单Code

                if (ppmModelCode != null && ppmModelCode != string.Empty)
                {
                    ppmModelCodeCondition = string.Format(" AND modelcode in ({0}) ", FormatHelper.ProcessQueryValues(ppmModelCode));
                }
                if (ppmItemCode != null && ppmItemCode != string.Empty)
                {
                    ppmItemCodeCondition = string.Format(" AND itemcode in ({0}) ", FormatHelper.ProcessQueryValues(ppmItemCode));
                    ppmItemCodeCondition2 = string.Format(" AND c.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(ppmItemCode));
                }
                if (ppmMoCode != null && ppmMoCode != string.Empty)
                {
                    ppmMoCodeCondition = string.Format(" AND mocode in ({0}) ", FormatHelper.ProcessQueryValues(ppmMoCode));
                }
            }
            if (summaryTarget == SummaryTarget.Model || summaryTarget == SummaryTarget.Item || summaryTarget == SummaryTarget.Mo)
            {
                //如果统计对象是机种,产品,工单,没有以下查询条件
                ppmModelCodeCondition = string.Empty;
                ppmItemCodeCondition = string.Empty;
                ppmItemCodeCondition2 = string.Empty;
                ppmMoCodeCondition = string.Empty;
            }


            if (summaryTarget == SummaryTarget.Model)
            {
                selectFields += " modelcode, ";//,itemcode, ";
                groupBy += " modelcode, ";//,itemcode, ";
                table = " TBLRPTREALLINEQTY ";
                if (modelCode != "" && modelCode != null)
                {
                    condition += " and modelcode in (" + FormatHelper.ProcessQueryValues(modelCode.ToUpper()) + ")";
                }
            }
            else if (summaryTarget == SummaryTarget.Item)
            {
                selectFields += " itemcode, ";
                groupBy += " itemcode, ";
                table = " TBLRPTREALLINEQTY ";
                if (itemCode != "" && itemCode != null)
                {
                    condition += " and itemcode in (" + FormatHelper.ProcessQueryValues(itemCode.ToUpper()) + ")";
                }
            }
            else if (summaryTarget == SummaryTarget.Mo)
            {
                selectFields += " mocode,  ";
                groupBy += " mocode, ";
                table = " TBLRPTREALLINEQTY ";
                if (moCode != "" && moCode != null)
                {
                    condition += " and mocode in (" + FormatHelper.ProcessQueryValues(moCode.ToUpper()) + ")";
                }
            }
            else if (summaryTarget == SummaryTarget.Operation)
            {
                selectFields += " opcode,   ";
                groupBy += " opcode, ";
                table = " TBLRPTHISOPQTY ";
                if (opCode != "" && opCode != null)
                {
                    condition += " and opcode in (" + FormatHelper.ProcessQueryValues(opCode.ToUpper()) + ")";
                }
                condition += ppmModelCodeCondition;
                condition += ppmItemCodeCondition;
                condition += ppmMoCodeCondition;
            }
            else if (summaryTarget == SummaryTarget.Segment)
            {
                selectFields += " segcode, ";
                groupBy += " segcode, ";
                table = " TBLRPTREALLINEQTY ";
                if (segmentCode != "" && segmentCode != null)
                {
                    condition += " and segcode in (" + FormatHelper.ProcessQueryValues(segmentCode.ToUpper()) + ")";
                }
                condition += ppmModelCodeCondition;
                condition += ppmItemCodeCondition;
                condition += ppmMoCodeCondition;
            }
            else if (summaryTarget == SummaryTarget.StepSequence)
            {
                selectFields += " sscode, ";
                groupBy += " sscode, ";
                table = " TBLRPTREALLINEQTY ";
                if (stepSequenceCode != "" && stepSequenceCode != null)
                {
                    condition += " and SSCODE in (" + FormatHelper.ProcessQueryValues(stepSequenceCode.ToUpper()) + ")";
                }
                condition += ppmModelCodeCondition;
                condition += ppmItemCodeCondition;
                condition += ppmMoCodeCondition;
            }
            else if (summaryTarget == SummaryTarget.Resource)
            {
                selectFields += " rescode, ";
                groupBy += " rescode, ";
                table = " TBLRPTHISOPQTY ";
                if (resourceCode != "" && resourceCode != null)
                {
                    condition += " and rescode in (" + FormatHelper.ProcessQueryValues(resourceCode.ToUpper()) + ")";
                }
                condition += ppmModelCodeCondition;
                condition += ppmItemCodeCondition;
                condition += ppmMoCodeCondition;
            }
            else
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", summaryTarget);
            }

            condition += FormatHelper.GetDateRangeSql("SHIFTDAY", startDate, endDate);
            //condition += " AND qtyflag = 'Y' ";		//如果不统计中间产量工序的数量,可以用此语句.
            #endregion

            #region Yield Catalog
            /*
			 * CS210		"OWC图形（柱状图、折线图）
			 * 对于分母为0的不良率、直通率、PPM不可显示为－1，调整为不显示"	查询分析	Enhancement	Eric	2005.12.27
			 * modified by jessie lee, 2005/12/28
			 * 
			 * */
            if (yieldCatalog == YieldCatalog.NotYield)	//不良率
            {
                //良率类型为不良率时，统计对象机种、产品、工单、工段、生产线共5种的计算公式的分母(通过数)由完成数，变更为投入数 2005/10/27 CS116要求
                //工序和资源	分母(通过数 为 EATTRIBUTE2 栏位  )
                //inputqty	中间产量投入数
                //eattribute1	工单投入数
                //outputqty	产量 （qtyflay 为 Y 表示工单产量，为N表示中间产量）
                //ALLGOODQTY   直通数量	（产线 工段）
                //moallgoodqty 直通数量 （工单 机种 产品）
                if (summaryTarget.ToUpper() == SummaryTarget.Model.ToUpper() ||
                    summaryTarget.ToUpper() == SummaryTarget.Item.ToUpper() ||
                    summaryTarget.ToUpper() == SummaryTarget.Mo.ToUpper())
                {
                    //机种、产品、工单的投入数统计eattribute1
                    selectFields += string.Format(" sum(NGTimes) as NGTimes,sum(decode(qtyflag,'Y',OUTPUTQTY,0)) as OUTPUTQTY,sum(eattribute1) as INPUTQTY,decode(sum(eattribute1),0,{0},null,{0},sum(NGTimes)/sum(eattribute1)) as NotYieldPercent, ", "null");
                    if (summaryTarget.ToUpper() != SummaryTarget.Mo.ToUpper())
                    {
                        //机种和产品的不良率的分子和分母要去除返工工单对应的数据
                        condition += this.GetMOCodeSqlCondition();
                    }

                }
                else if (summaryTarget.ToUpper() == SummaryTarget.Segment.ToUpper() ||
                    summaryTarget.ToUpper() == SummaryTarget.StepSequence.ToUpper())
                {
                    //工段、生产线投入数统计 (inputqty + eattribute1)
                    selectFields += string.Format(" sum(NGTimes) as NGTimes,sum(decode(qtyflag,'Y',OUTPUTQTY,0)) as OUTPUTQTY,sum(eattribute1 + inputqty) as INPUTQTY,decode(sum(eattribute1 + inputqty),0,{0},null,{0},sum(NGTimes)/sum(eattribute1 + inputqty)) as NotYieldPercent, ", "null");
                }
                else if (summaryTarget.ToUpper() == SummaryTarget.Resource.ToUpper() ||
                    summaryTarget.ToUpper() == SummaryTarget.Operation.ToUpper())
                {
                    //(通过数  sum(EATTRIBUTE2)
                    //selectFields += @" sum(NGTimes) as NGTimes,(sum(OUTPUTQTY) + sum(NGTimes)) as AllTimes, DECODE ((SUM (outputqty) + SUM (ngtimes)),0, -1,NULL, -1,SUM (ngtimes) / (SUM (outputqty) + SUM (ngtimes))) AS notyieldpercent,";
                    selectFields += @" sum(NGTimes) as NGTimes,sum(EATTRIBUTE2) as AllTimes, DECODE (sum(EATTRIBUTE2),0, null,NULL, null,SUM (ngtimes) /sum(EATTRIBUTE2)) AS notyieldpercent,";
                }
                else
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", summaryTarget);
                }
            }
            else if (yieldCatalog.ToUpper() == YieldCatalog.AllGood.ToUpper())	//直通率
            {
                //				直通率：
                //				针对不同的对象有不同的计算方法：
                //				1. 工段和产线：
                //					工段直通率：＝(从该工段的产线上投入到产出的过程中未曾出现过不良的产品台数)/从该工段的产线上完成的产品良品台数
                //					产线直通率：＝(从该产线上投入到产出的过程中未曾出现过不良的产品台数)/从该产线上完成的产品良品台数
                //				2. 机种和产品直通率：＝(机种或产品从投入到产出的过程中在正常工单中未曾出现过不良的产品台数)/该机种或产品在正常工单中完成的产品良品台数
                //				3. 工单直通率：＝(工单（正常工单或返工工单）从投入到产出的过程中未曾出现过不良的产品台数)/该工单（正常工单或返工工单）中完成的产品良品
                //				moallgoodqty 直通台数 （工单 机种 产品）
                if (summaryTarget.ToUpper() == SummaryTarget.Model.ToUpper() ||
                    summaryTarget.ToUpper() == SummaryTarget.Item.ToUpper() ||
                    summaryTarget.ToUpper() == SummaryTarget.Mo.ToUpper())
                {
                    selectFields += string.Format(" sum(moallgoodqty) as ALLGOODQTY,sum(OUTPUTQTY) as OUTPUTQTY,decode(sum(OUTPUTQTY),0,{0},sum(moallgoodqty)/sum(OUTPUTQTY)) as AllGoodYieldPercent,", "null");
                    if (summaryTarget.ToUpper() != SummaryTarget.Mo.ToUpper())
                    {
                        //机种和产品的直通率的分子和分母要去除返工工单对应的数据
                        condition += this.GetMOCodeSqlCondition();
                    }
                    //不良标致位逻辑
                    //只算工单产量 qtyflag = Y 表示工单产量(即最后工序产量)
                    condition += " AND qtyflag = 'Y' ";
                }
                else if (summaryTarget.ToUpper() == SummaryTarget.StepSequence.ToUpper() ||
                    summaryTarget.ToUpper() == SummaryTarget.Segment.ToUpper())
                {
                    selectFields += string.Format(" sum(ALLGOODQTY) as ALLGOODQTY,sum(OUTPUTQTY) as OUTPUTQTY,decode(sum(OUTPUTQTY),0,{0},sum(ALLGOODQTY)/sum(OUTPUTQTY)) as AllGoodYieldPercent,", "null");
                }
                else
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_SummaryTarget_AllGood");
                }
            }
            else if (yieldCatalog.ToUpper() == YieldCatalog.PPM.ToUpper())	//非常特殊，需要将前面的栏位都清空		//PPM
            {
                //产品ppm
                if (summaryTarget.ToUpper() == SummaryTarget.Item.ToUpper())
                {

                    #region 产品ppm


                    string itemCondition = string.Empty;
                    string itemCondition2 = string.Empty;
                    if (itemCode != "" && itemCode != null)
                    {
                        itemCondition = string.Format(@" and itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCode));
                        itemCondition2 = string.Format(@" and c.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCode));
                    }
                    string shiftDayCondition = string.Empty;						//日期 条件
                    shiftDayCondition = FormatHelper.GetDateRangeSql("shiftday", startDate, endDate);

                    //产线ppm
                    selectFields = string.Format(
                        @"  a.itemcode AS itemcode,
							c.notyieldlocation AS notyieldlocation,
							a.totallocation * outputqty AS totallocation,
							DECODE (a.totallocation * outputqty,
									0, NULL,
									ROUND (  1000000
										* c.notyieldlocation
										/ (a.totallocation * outputqty)
										)
								) AS ppm,", "null");

                    #region tableA tblitemlocation

                    string tableA = string.Format(@"(SELECT   a.itemcode, SUM (a.qty) AS totallocation
													FROM tblitemlocation a 
													WHERE 1=1 {1} {0}
												GROUP BY itemcode) a", itemCondition, GlobalVariables.CurrentOrganizations.GetSQLCondition());

                    #endregion

                    #region tableB tblrptreallineqty

                    string tableB = "tblrptreallineqty b";
                    tableB = string.Format(@"(SELECT itemcode,shiftday,{0},sum(decode(qtyflag,'Y',OUTPUTQTY,0)) as OUTPUTQTY
									FROM tblrptreallineqty
									WHERE 1 = 1 {1}{2}
										group by itemcode,shiftday,{0}) b", "{0}", shiftDayCondition, itemCondition);
                    if (timingType == TimingType.TimePeriod)
                    {
                        //" TPCODE";
                        tableB = string.Format(tableB, "TPCODE");
                    }
                    else if (timingType == TimingType.Shift)
                    {
                        //" SHIFTCODE ";
                        tableB = string.Format(tableB, "SHIFTCODE");
                    }
                    else if (timingType == TimingType.Day)
                    {
                        //" SHIFTCODE";
                        tableB = string.Format(tableB, "SHIFTCODE");
                    }
                    else if (timingType == TimingType.Week)
                    {
                        //" WEEK";
                        tableB = string.Format(tableB, "WEEK");
                    }
                    else if (timingType == TimingType.Month)
                    {
                        //" MONTH";
                        tableB = string.Format(tableB, "MONTH");
                    }

                    #endregion

                    #region talbeC tbltserrorcode2loc

                    // 根据分组条件 查询
                    shiftDayCondition = FormatHelper.GetDateRangeSql("c.shiftday", startDate, endDate);
                    string tableC = string.Empty;
                    tableC = string.Format(@"(SELECT   itemcode,  COUNT (eloc) AS notyieldlocation,shiftday1,{3}
											FROM (SELECT c.*, tblts.shiftday as shiftday1,{2}
													FROM tbltserrorcode2loc c LEFT JOIN tblts ON (tblts.tsid = c.tsid)
													WHERE  1=1 {0}{1})
										GROUP BY itemcode,shiftday1,{2}) c", shiftDayCondition, itemCondition2, "{0}", "{1}");

                    if (timingType == TimingType.TimePeriod)
                    {
                        //" TPCODE";
                        tableC = string.Format(tableC, "TPCODE", "TPCODE as TPCODE1");
                    }
                    else if (timingType == TimingType.Shift)
                    {
                        //" SHIFTCODE ";
                        tableC = string.Format(tableC, "SHIFTCODE", "SHIFTCODE as SHIFTCODE1");
                    }
                    else if (timingType == TimingType.Day)
                    {
                        //" SHIFTCODE";
                        tableC = string.Format(tableC, "SHIFTCODE", "SHIFTCODE as SHIFTCODE1");
                    }
                    else if (timingType == TimingType.Week)
                    {
                        //" WEEK";
                        tableC = string.Format(tableC, "FRMWEEK", "FRMWEEK");
                    }
                    else if (timingType == TimingType.Month)
                    {
                        //" MONTH";
                        tableC = string.Format(tableC, "FRMMONTH", "FRMMONTH");
                    }

                    #endregion

                    #region table

                    table = string.Format(@" {0},{1},{2}", tableA, tableB, tableC);

                    #endregion

                    #region condition

                    // 根据分组条件 拼condition
                    condition = string.Format(@" 1=1 
						AND a.itemcode = b.itemcode
						AND b.itemcode = c.itemcode
						AND b.shiftday = c.shiftday1
						{0}
						", "{0}");

                    if (timingType == TimingType.TimePeriod)
                    {
                        //" TPCODE";
                        condition = string.Format(condition, " and b.tpcode = c.tpcode1 ");
                    }
                    else if (timingType == TimingType.Shift)
                    {
                        //" SHIFTCODE ";
                        condition = string.Format(condition, " and b.SHIFTCODE = c.SHIFTCODE1 ");
                    }
                    else if (timingType == TimingType.Day)
                    {
                        //" SHIFTCODE";
                        condition = string.Format(condition, " and b.SHIFTCODE = c.SHIFTCODE1 ");
                    }
                    else if (timingType == TimingType.Week)
                    {
                        //" WEEK";
                        condition = string.Format(condition, " and b.WEEK = c.FRMWEEK ");
                    }
                    else if (timingType == TimingType.Month)
                    {
                        //" MONTH";
                        condition = string.Format(condition, " and b.MONTH = c.FRMMONTH ");
                    }

                    #endregion

                    #endregion

                }
                else if (summaryTarget.ToUpper() == SummaryTarget.StepSequence.ToUpper())
                {

                    #region 产线ppm 统计对象为产线,产品,机种,工单

                    //产线ppm要统计中间产量.
                    string ppmSSCodeCondition = string.Empty;						//产线 条件
                    string shiftDayCondition = string.Empty;						//日期 条件
                    if (stepSequenceCode != null && stepSequenceCode != string.Empty)
                    {
                        ppmSSCodeCondition = string.Format(" AND sscode in ({0}) ", FormatHelper.ProcessQueryValues(stepSequenceCode));
                    }
                    shiftDayCondition = FormatHelper.GetDateRangeSql("shiftday", startDate, endDate);

                    //产线ppm
                    #region selectFields

                    selectFields = string.Format(
                        @"  a.sscode,a.itemcode,a.mocode,a.modelcode,a.totallocation,a.shiftday,{0},c.notyieldlocation,
							DECODE (a.totallocation ,
											0, NULL,
											ROUND (  1000000
													* c.notyieldlocation
													/ a.totallocation 
												)
											) AS ppm,", "{0}");

                    if (timingType == TimingType.TimePeriod)
                    {
                        //" TPCODE ";
                        selectFields = string.Format(selectFields, "a.TPCODE");
                    }
                    else if (timingType == TimingType.Shift)
                    {
                        //" SHIFTCODE ";
                        selectFields = string.Format(selectFields, "a.SHIFTCODE");
                    }
                    else if (timingType == TimingType.Day)
                    {
                        //" SHIFTCODE ";
                        selectFields = string.Format(selectFields, "a.SHIFTCODE");
                    }
                    else if (timingType == TimingType.Week)
                    {
                        //" WEEK ";
                        selectFields = string.Format(selectFields, "a.WEEK");
                    }
                    else if (timingType == TimingType.Month)
                    {
                        //" MONTH";
                        selectFields = string.Format(selectFields, "a.MONTH");
                    }

                    #endregion

                    #region tblitemlocation tableA

                    string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
                    if (orgIDList.Length > 0) orgIDList = " AND a.orgid IN(" + orgIDList + ") AND tblss.orgid IN(" + orgIDList + ") ";

                    string tableA = string.Format(@"(SELECT   a.itemcode, sscode, SUM (a.qty) AS totallocation
								FROM tblitemlocation a LEFT JOIN tblss ON ( a.segcode = tblss.segcode)
								WHERE 1=1 {2} {0} {1}
							GROUP BY itemcode, sscode) a", ppmItemCodeCondition, ppmSSCodeCondition, orgIDList);





                    #endregion

                    #region tblrptreallineqty tableB

                    string tableB = string.Empty;
                    tableB = string.Format(@"(SELECT itemcode, sscode, mocode, modelcode,{0},shiftday,SUM (outputqty) as outputqty
									FROM tblrptreallineqty
									WHERE 1 = 1 {1}{2}{3}{4}{5}
										group by itemcode, sscode, mocode, modelcode,{0},shiftday) b", "{0}", shiftDayCondition, ppmMoCodeCondition, ppmModelCodeCondition, ppmItemCodeCondition, ppmSSCodeCondition);

                    if (timingType == TimingType.TimePeriod)
                    {
                        //" TPCODE ";
                        tableB = string.Format(tableB, "TPCODE");
                    }
                    else if (timingType == TimingType.Shift)
                    {
                        //" SHIFTCODE ";
                        tableB = string.Format(tableB, "SHIFTCODE");
                    }
                    else if (timingType == TimingType.Day)
                    {
                        //" SHIFTCODE ";
                        tableB = string.Format(tableB, "SHIFTCODE");
                    }
                    else if (timingType == TimingType.Week)
                    {
                        //" WEEK ";
                        tableB = string.Format(tableB, "WEEK");
                    }
                    else if (timingType == TimingType.Month)
                    {
                        //" MONTH";
                        tableB = string.Format(tableB, "MONTH");
                    }

                    #endregion

                    #region tbltserrorcode2loc tableC

                    shiftDayCondition = FormatHelper.GetDateRangeSql("c.shiftday", startDate, endDate);
                    string ppmModelCode = ppmSSCondition["ppmModelCode"];			//机种Code
                    string ppmItemCode = ppmSSCondition["ppmItemCode"];				//产品Code
                    string ppmMoCode = ppmSSCondition["ppmMoCode"];					//工单Code
                    if (ppmModelCode != null && ppmModelCode != string.Empty)
                    {
                        ppmModelCodeCondition = string.Format(" AND c.modelcode in ({0}) ", FormatHelper.ProcessQueryValues(ppmModelCode));
                    }
                    if (ppmItemCode != null && ppmItemCode != string.Empty)
                    {
                        ppmItemCodeCondition = string.Format(" AND c.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(ppmItemCode));
                        ppmItemCodeCondition2 = string.Format(" AND c.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(ppmItemCode));
                    }
                    if (ppmMoCode != null && ppmMoCode != string.Empty)
                    {
                        ppmMoCodeCondition = string.Format(" AND c.mocode in ({0}) ", FormatHelper.ProcessQueryValues(ppmMoCode));
                    }
                    if (stepSequenceCode != null && stepSequenceCode != string.Empty)
                    {
                        ppmSSCodeCondition = string.Format(" AND frmsscode in ({0}) ", FormatHelper.ProcessQueryValues(stepSequenceCode));
                    }

                    string tableC = string.Empty;

                    tableC = string.Format(@"(SELECT   itemcode, sscode, mocode, modelcode, COUNT (eloc) AS notyieldlocation,shiftday1,{6}
											FROM (SELECT c.*, frmsscode AS sscode,tblts.shiftday AS shiftday1,{5}
													FROM tbltserrorcode2loc c LEFT JOIN tblts ON (tblts.tsid = c.tsid)
													WHERE  1=1 {0}{1}{2}{3}{4} )
										GROUP BY itemcode, sscode, mocode, modelcode, shiftday1,{5}) c", shiftDayCondition, ppmMoCodeCondition, ppmModelCodeCondition, ppmItemCodeCondition, ppmSSCodeCondition, "{0}", "{1}");


                    if (timingType == TimingType.TimePeriod)
                    {
                        //" TPCODE";
                        tableC = string.Format(tableC, "TPCODE", "TPCODE as TPCODE1");
                    }
                    else if (timingType == TimingType.Shift)
                    {
                        //" SHIFTCODE ";
                        tableC = string.Format(tableC, "SHIFTCODE", "SHIFTCODE as SHIFTCODE1");
                    }
                    else if (timingType == TimingType.Day)
                    {
                        //" SHIFTCODE";
                        tableC = string.Format(tableC, "SHIFTCODE", "SHIFTCODE as SHIFTCODE1");
                    }
                    else if (timingType == TimingType.Week)
                    {
                        //" WEEK";
                        tableC = string.Format(tableC, "FRMWEEK", "FRMWEEK");
                    }
                    else if (timingType == TimingType.Month)
                    {
                        //" MONTH";
                        tableC = string.Format(tableC, "FRMMONTH", "FRMMONTH");
                    }

                    #endregion

                    #region table

                    table = string.Format(@"(
								SELECT   b.sscode, a.itemcode AS itemcode, b.mocode, b.modelcode,
										a.totallocation * outputqty AS totallocation,
										{3}, shiftday
										FROM {0},{1}
									WHERE 1 = 1
										AND a.itemcode = b.itemcode
										AND a.sscode = b.sscode
									ORDER BY {3}, shiftday
								) a
								left join {2}
										on ( a.sscode = c.sscode and a.itemcode = c.itemcode
												AND a.shiftday = c.shiftday1 {4}) ", tableA, tableB, tableC, "{0}", "{1}");

                    if (timingType == TimingType.TimePeriod)
                    {
                        //" TPCODE";
                        table = string.Format(table, "b.tpcode", " and a.tpcode = c.tpcode1 ");
                    }
                    else if (timingType == TimingType.Shift)
                    {
                        //" SHIFTCODE ";
                        table = string.Format(table, "b.SHIFTCODE", " and a.SHIFTCODE = c.SHIFTCODE1 ");
                    }
                    else if (timingType == TimingType.Day)
                    {
                        //" SHIFTCODE";
                        table = string.Format(table, "b.SHIFTCODE", " and a.SHIFTCODE = c.SHIFTCODE1 ");
                    }
                    else if (timingType == TimingType.Week)
                    {
                        //" WEEK";
                        table = string.Format(table, "b.WEEK", " and a.WEEK = c.FRMWEEK ");
                    }
                    else if (timingType == TimingType.Month)
                    {
                        //" MONTH";
                        table = string.Format(table, "b.MONTH", " and a.MONTH = c.FRMMONTH ");
                    }

                    #endregion

                    #region condition

                    condition = string.Format(" 1=1 ");

                    #endregion

                    #endregion

                }
                else
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_SummaryTarget_PPM");
                }
            }

            #endregion

            #region TimingType            
            if (timingType == TimingType.TimePeriod)
            {
                selectFields += " TPCODE, ";
                groupBy += " TPCODE, ";
                orderBy += " TPCODE, ";
            }
            else if (timingType == TimingType.Shift)
            {
                selectFields += " SHIFTCODE, ";
                groupBy += " SHIFTCODE, ";
                orderBy += " SHIFTCODE, ";
            }
            else if (timingType == TimingType.Day)
            {
                selectFields += " SHIFTDAY, ";
                groupBy += " SHIFTDAY, ";
                orderBy += " SHIFTDAY, ";
            }
            else if (timingType == TimingType.Week)
            {
                selectFields += " WEEK,SUBSTR(SHIFTDAY,0,4) AS SHIFTDAY, ";
                groupBy += " WEEK,SUBSTR(SHIFTDAY,0,4), ";
                orderBy += " WEEK,SUBSTR(SHIFTDAY,0,4), ";
            }
            else if (timingType == TimingType.Month)
            {
                selectFields += " MONTH,SUBSTR(SHIFTDAY,0,4) AS SHIFTDAY, ";
                groupBy += " MONTH,SUBSTR(SHIFTDAY,0,4), ";
                orderBy += " MONTH,SUBSTR(SHIFTDAY,0,4), ";
            }
            else
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", timingType);
            }
            #endregion

            #region Process String
            if (selectFields.TrimEnd().EndsWith(","))
            {
                selectFields = selectFields.Substring(0, selectFields.TrimEnd().Length - 1);
            }
            if (groupBy.TrimEnd().EndsWith(","))
            {
                groupBy = groupBy.Substring(0, groupBy.TrimEnd().Length - 1);
            }
            if (orderBy.TrimEnd().EndsWith(","))
            {
                orderBy = orderBy.Substring(0, orderBy.TrimEnd().Length - 1);
            }
            #endregion

            string sqlStr = string.Format(
                @"select {0} from {1} where {2} group by {3} order by {4}",
                selectFields, table, condition, groupBy, orderBy);

            #region Process Special Usage

            #region ppm sql

            if (yieldCatalog == YieldCatalog.PPM)
            {
                if (summaryTarget.ToUpper() == SummaryTarget.StepSequence.ToUpper())
                {
                    sqlStr = string.Format(
                        @"select {0} from {1} where {2} ",
                        selectFields, table, condition, groupBy, orderBy);
                }
                else
                {
                    sqlStr = string.Format(
                        @"select {0} from {1} where {2} order by {4}",
                        selectFields, table, condition, groupBy, orderBy);
                }
            }

            #endregion

            if (yieldCatalog == YieldCatalog.NotYield &&
                (summaryTarget.ToUpper() == SummaryTarget.Model.ToUpper() ||
                summaryTarget.ToUpper() == SummaryTarget.Item.ToUpper() ||
                summaryTarget.ToUpper() == SummaryTarget.Mo.ToUpper()))
            {
                sqlStr = string.Format(@"select * from ({0}) where outputqty is not null", sqlStr);
            }
            #endregion

            #region Sql Assemble
#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(HistoryYieldPercent),
                new SQLCondition(sqlStr));
            #endregion
        }

        //获取不是返工工单的sql条件(包括普通返工工单和每月大返工工单)
        private string GetMOCodeSqlCondition()
        {
            return @"and mocode in (SELECT mocode
											FROM tblmo
											WHERE 1 = 1
												 AND NOT EXISTS (
													SELECT mocode
														FROM tblmo
														WHERE tblrptreallineqty.mocode = tblmo.mocode
														AND EXISTS (
																SELECT paramcode
																FROM tblsysparam
																WHERE tblsysparam.PARAMCODE = tblmo.motype
																	AND paramgroupcode = 'MOTYPE'
																	AND paramvalue in ('motype_reworkmotype','motype_monthreworkmotype')
																	) " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")";
        }

        //获取产线对应的产品不良位号数
        private object[] GetSSItemLocationSegment(string ssCode, string itemCode)
        {
            string sql = string.Format("select tblitemlocation.* from tblitemlocation where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode = '{0}'and segcode in (select segcode from tblss where 1=1 {2} and sscode = '{1}'",
                itemCode, ssCode, GlobalVariables.CurrentOrganizations.GetSQLCondition());

            return this.DataProvider.CustomQuery(
                typeof(ItemLocation),
                new SQLCondition(sql));
        }

        #endregion

        #region On Wip
        public object[] QueryOpWipInfoOnOperation(
            string modelCode, string itemCode, string moCode,
            int inclusive, int exclusive)
        {
            return new object[] { new OnWipInfoOnOperation() };
        }

        public int QueryOpWipInfoOnOperationCount(
            string modelCode, string itemCode, string moCode)
        {
            return 10;
        }

        public object[] QueryOpWipInfoOnResource(
            string modelCode, string itemCode, string moCode, string operationCode,
            int inclusive, int exclusive)
        {
            return new object[] { new OnWipInfoOnResource() };
        }

        public int QueryOpWipInfoOnResourceCount(
            string modelCode, string itemCode, string moCode, string operationCode)
        {
            return 10;
        }

        public object[] QueryOnWipInfoDistributing(
            string modelCode, string itemCode, string moCode, string operationCode,
            string segmentCode, string stepSequenceCode, string resourceCode,
            int inclusive, int exclusive)
        {
            return new object[] { new OnWipInfoDistributing() };
        }

        public int QueryOnWipInfoDistributingCount(
            string modelCode, string itemCode, string moCode, string operationCode,
            string segmentCode, string stepSequenceCode, string resourceCode)
        {
            return 10;
        }
        #endregion

        #region ComponentLoading Tracking
        public object[] QueryComponentLoadingTracking(
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode,
            string inno, string keypartsStart, string keypartsEnd,
            int startDate, int endDate,
            int inclusive, int exclusive)
        {
            string insideItemCodeCondition = "";
            if (insideItemCode != "" && insideItemCode != null)
            {
                insideItemCodeCondition = string.Format(@" and MITEMCODE like '{0}%'", insideItemCode.ToUpper());
            }

            string vendorCodeCondition = "";
            if (vendorCode != "" && vendorCode != null)
            {
                vendorCodeCondition = string.Format(@" and VENDORCODE like '{0}%'", vendorCode.ToUpper());
            }

            string vendorItemCodeCondition = "";
            if (vendorItemCode != "" && vendorItemCode != null)
            {
                vendorItemCodeCondition = string.Format(@" and VENDORITEMCODE like '{0}%'", vendorItemCode.ToUpper());
            }

            string lotnoCondition = "";
            if (lotno != "" && lotno != null)
            {
                lotnoCondition = string.Format(@" and LOTNO like '{0}%'", lotno.ToUpper());
            }

            string dateCodeCondition = "";
            if (dateCode != "" && dateCode != null)
            {
                dateCodeCondition = string.Format(@" and DATECODE like '{0}%'", dateCode.ToUpper());
            }

            //集成料号
            string innoCondition = "";
            if (inno != "" && inno != null)
            {
                innoCondition = string.Format(@" and MCARD like '{0}%' and mcardtype = {1} ", inno.ToUpper(), MCardType.MCardType_INNO);
            }

            if ((keypartsStart != "" && keypartsStart != null) &&
                (keypartsEnd == "" || keypartsEnd == null))
            {
                keypartsEnd = keypartsStart;
            }

            if ((keypartsEnd != "" && keypartsEnd != null) &&
                (keypartsStart == "" || keypartsStart == null))
            {
                keypartsStart = keypartsEnd;
            }

            string keypartsStartCondition = FormatHelper.GetRCardRangeSql("mcard", keypartsStart.ToUpper(), keypartsEnd.ToUpper());

            string shiftDayCondition = FormatHelper.GetDateRangeSql("mdate", startDate, endDate);


            string sSQL = String.Empty;
            if ((keypartsStart == "" || keypartsStart == null) && (keypartsEnd == "" || keypartsEnd == null))
            {
                sSQL = String.Format(
                    @"select distinct 
						RCARD as sn,RCARDSEQ,modelcode,itemcode,mocode,RCARD as snstate,opcode,routecode,segcode,sscode,rescode,max(mdate) as mdate,max(mtime) as mtime,muser
				  from 
						tblonwipitem where 1=1 {0} {1} {2} {3} {4} {5} {6}
					group by {7}
				",
                    innoCondition,
                    insideItemCodeCondition,
                    vendorCodeCondition,
                    vendorItemCodeCondition,
                    lotnoCondition,
                    dateCodeCondition,
                    shiftDayCondition,
                    " RCARD,RCARDseq,modelcode,itemcode,mocode,opcode,routecode,segcode,sscode,rescode,muser "
                    );
            }
            else
            {
                sSQL = String.Format(
                    @"select distinct 
						rcard as sn,RCARDSEQ,modelcode,itemcode,mocode,rcard as snstate,opcode,routecode,segcode,sscode,rescode,max(mdate) as mdate,max(mtime) as mtime,muser
				  from 
						tblonwipitem where 1=1 {0} {1} {2} {3} {4} {5} {6} 
				   {7} {8} and mcardtype = {9} 
				  group by {10}	
				",
                    innoCondition,
                    insideItemCodeCondition,
                    vendorCodeCondition,
                    vendorItemCodeCondition,
                    lotnoCondition,
                    dateCodeCondition,
                    shiftDayCondition,
                    keypartsStartCondition,
                    "",
                    MCardType.MCardType_Keyparts,
                    " RCARD,RCARDseq,modelcode,itemcode,mocode,opcode,routecode,segcode,sscode,rescode,muser "
                    );
            }

#if DEBUG
            Log.Info(new PagerCondition(sSQL, "sn", inclusive, exclusive, true).SQLText);
#endif
            object[] objs = this.DataProvider.CustomQuery(
                typeof(ComponentLoadingTracking),
                new PagerCondition(sSQL, "sn,mdate desc,mtime desc", inclusive, exclusive, true));

            if (objs != null)
            {
                foreach (ComponentLoadingTracking com in objs)
                {
                    //status
                    object[] sn = this.DataProvider.CustomQuery(typeof(SimulationReport),
                        new SQLCondition(
                        string.Format(@"select status from (SELECT   actionresult AS status
																FROM tblonwip 
															WHERE rcard = '{0}' order by rcardseq desc)
															where rownum = 1", com.SNState, com.SNSeq)));
                    if (sn != null)
                    {
                        com.SNState = (sn[0] as SimulationReport).Status;
                    }
                    else
                    {
                        com.SNState = "";
                    }
                }
            }
            return objs;


        }


        public int QueryComponentLoadingTrackingCount(
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode,
            string inno, string keypartsStart, string keypartsEnd,
            int startDate, int endDate)
        {
            string insideItemCodeCondition = "";
            if (insideItemCode != "" && insideItemCode != null)
            {
                insideItemCodeCondition = string.Format(@" and MITEMCODE like '{0}%'", insideItemCode.ToUpper());
            }

            string vendorCodeCondition = "";
            if (vendorCode != "" && vendorCode != null)
            {
                vendorCodeCondition = string.Format(@" and VENDORCODE like '{0}%'", vendorCode.ToUpper());
            }

            string vendorItemCodeCondition = "";
            if (vendorItemCode != "" && vendorItemCode != null)
            {
                vendorItemCodeCondition = string.Format(@" and VENDORITEMCODE like '{0}%'", vendorItemCode.ToUpper());
            }

            string lotnoCondition = "";
            if (lotno != "" && lotno != null)
            {
                lotnoCondition = string.Format(@" and LOTNO like '{0}%'", lotno.ToUpper());
            }

            string dateCodeCondition = "";
            if (dateCode != "" && dateCode != null)
            {
                dateCodeCondition = string.Format(@" and DATECODE like '{0}%'", dateCode.ToUpper());
            }

            //集成料号
            string innoCondition = "";
            if (inno != "" && inno != null)
            {
                innoCondition = string.Format(@" and MCARD like '{0}%' and mcardtype = {1} ", inno.ToUpper(), MCardType.MCardType_INNO);
            }

            if ((keypartsStart != "" && keypartsStart != null) &&
                (keypartsEnd == "" || keypartsEnd == null))
            {
                keypartsEnd = keypartsStart;
            }

            if ((keypartsEnd != "" && keypartsEnd != null) &&
                (keypartsStart == "" || keypartsStart == null))
            {
                keypartsStart = keypartsEnd;
            }

            string keypartsStartCondition = FormatHelper.GetRCardRangeSql("mcard", keypartsStart.ToUpper(), keypartsEnd.ToUpper());

            string shiftDayCondition = FormatHelper.GetDateRangeSql("mdate", startDate, endDate);

            string sSQL = String.Empty;
            if ((keypartsStart == "" || keypartsStart == null) && (keypartsEnd == "" || keypartsEnd == null))
            {
                sSQL = String.Format(
                    @"select distinct 
						RCARD as sn,RCARDSEQ,modelcode,itemcode,mocode,RCARD as snstate,opcode,routecode,segcode,sscode,rescode,max(mdate) as mdate,max(mtime) as mtime,muser
				  from 
						tblonwipitem where 1=1 {0} {1} {2} {3} {4} {5} {6}
					group by {7}
				",
                    innoCondition,
                    insideItemCodeCondition,
                    vendorCodeCondition,
                    vendorItemCodeCondition,
                    lotnoCondition,
                    dateCodeCondition,
                    shiftDayCondition,
                    " RCARD,RCARDseq,modelcode,itemcode,mocode,opcode,routecode,segcode,sscode,rescode,muser "
                    );
            }
            else
            {
                sSQL = String.Format(
                    @"select distinct 
						rcard as sn,RCARDSEQ,modelcode,itemcode,mocode,rcard as snstate,opcode,routecode,segcode,sscode,rescode,max(mdate) as mdate,max(mtime) as mtime,muser
				  from 
						tblonwipitem where 1=1 {0} {1} {2} {3} {4} {5} {6} 
				  {7} {8} and mcardtype = {9} 	
				  group by {10}	
				",
                    innoCondition,
                    insideItemCodeCondition,
                    vendorCodeCondition,
                    vendorItemCodeCondition,
                    lotnoCondition,
                    dateCodeCondition,
                    shiftDayCondition,
                    keypartsStartCondition,
                    "",
                    MCardType.MCardType_Keyparts,
                    " RCARD,RCARDseq,modelcode,itemcode,mocode,opcode,routecode,segcode,sscode,rescode,muser "
                    );
            }


            sSQL = String.Format("select count(*) from ( {0} )", sSQL);
#if DEBUG
            Log.Info(sSQL);
#endif
            return this.DataProvider.GetCount(new SQLCondition(sSQL));
        }


        #region

        public object[] QueryComponentLoadingTracking2(
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode,
            string inno, string keypartsStart, string keypartsEnd,
            int startDate, int endDate,
            int inclusive, int exclusive)
        {
            string insideItemCodeCondition = "";
            if (insideItemCode != "" && insideItemCode != null)
            {
                insideItemCodeCondition = string.Format(@" and MITEMCODE like '{0}%'", insideItemCode.ToUpper());
            }

            string vendorCodeCondition = "";
            if (vendorCode != "" && vendorCode != null)
            {
                vendorCodeCondition = string.Format(@" and VENDORCODE like '{0}%'", vendorCode.ToUpper());
            }

            string vendorItemCodeCondition = "";
            if (vendorItemCode != "" && vendorItemCode != null)
            {
                vendorItemCodeCondition = string.Format(@" and VENDORITEMCODE like '{0}%'", vendorItemCode.ToUpper());
            }

            string lotnoCondition = "";
            if (lotno != "" && lotno != null)
            {
                lotnoCondition = string.Format(@" and LOTNO like '{0}%'", lotno.ToUpper());
            }

            string dateCodeCondition = "";
            if (dateCode != "" && dateCode != null)
            {
                dateCodeCondition = string.Format(@" and DATECODE like '{0}%'", dateCode.ToUpper());
            }

            //集成料号
            string innoCondition = "";
            if (inno != "" && inno != null)
            {
                innoCondition = string.Format(@" and MCARD like '{0}%' and mcardtype = {1} ", inno.ToUpper(), MCardType.MCardType_INNO);
            }

            if ((keypartsStart != "" && keypartsStart != null) &&
                (keypartsEnd == "" || keypartsEnd == null))
            {
                keypartsEnd = keypartsStart;
            }

            if ((keypartsEnd != "" && keypartsEnd != null) &&
                (keypartsStart == "" || keypartsStart == null))
            {
                keypartsStart = keypartsEnd;
            }

            string keypartsStartCondition = FormatHelper.GetRCardRangeSql("mcard", keypartsStart.ToUpper(), keypartsEnd.ToUpper());

            string shiftDayCondition = FormatHelper.GetDateRangeSql("a.mdate", startDate, endDate);

            string selectColunms = FormatHelper.GetAllFieldWithDesc(typeof(OnWIPItem), "a",
                new string[] { "itemcode", "opcode", "segcode", "sscode", "rescode", "muser" },
                new string[] { "tblitem.itemdesc", "tblop.opdesc", "tblseg.segdesc", "tblss.ssdesc", "tblres.resdesc", "tbluser.username" });

            string joinCondition = FormatHelper.GetLinkTableSQL("a", "itemcode", "tblitem", "itemcode");
            joinCondition += FormatHelper.GetLinkTableSQL("a", "opcode", "tblop", "opcode");
            joinCondition += FormatHelper.GetLinkTableSQL("a", "segcode", "tblseg", "segcode");
            joinCondition += FormatHelper.GetLinkTableSQL("a", "sscode", "tblss", "sscode");
            joinCondition += FormatHelper.GetLinkTableSQL("a", "rescode", "tblres", "rescode");
            joinCondition += FormatHelper.GetLinkTableSQL("a", "muser", "tbluser", "usercode");

            string sSQL = String.Empty;
            #region  sSQL

            //			if( (keypartsStart == "" || keypartsStart == null) && (keypartsEnd == "" || keypartsEnd == null) )
            //			{
            //				sSQL = String.Format(
            //					@"select distinct 
            //						RCARD , max(rcardseq) as RCARDSEQ,max(MSEQ) as MSEQ,mocode
            //				  from 
            //						tblonwipitem where 1=1 {0} {1} {2} {3} {4} {5} {6}
            //					group by {7}
            //				",
            //					innoCondition,
            //					insideItemCodeCondition,
            //					vendorCodeCondition,
            //					vendorItemCodeCondition,
            //					lotnoCondition,
            //					dateCodeCondition,
            //					shiftDayCondition,
            //					" RCARD,mocode "
            //					);	
            //			}
            //			else
            //			{
            //				sSQL = String.Format(
            //					@"select distinct 
            //						RCARD , max(rcardseq) as RCARDSEQ,max(MSEQ) as MSEQ,mocode
            //				  from 
            //						tblonwipitem where 1=1 {0} {1} {2} {3} {4} {5} {6} 
            //				   {7} {8} and mcardtype = {9} 
            //				  group by {10}	
            //				",
            //					innoCondition,
            //					insideItemCodeCondition,
            //					vendorCodeCondition,
            //					vendorItemCodeCondition,
            //					lotnoCondition,
            //					dateCodeCondition,
            //					shiftDayCondition,
            //					keypartsStartCondition,
            //					"",
            //					MCardType.MCardType_Keyparts,
            //					" RCARD,mocode "
            //					);
            //			}
            //
            //#if DEBUG
            //			Log.Info( new PagerCondition(sSQL,"sn",inclusive,exclusive,true).SQLText );
            //#endif
            //
            //			object[] objs = this.DataProvider.CustomQuery(
            //				typeof(OnWIPItem),
            //				new PagerCondition(sSQL,inclusive,exclusive,true));
            //			if(objs == null) return null;
            //
            //			string rcardCondtion = this.getSqlCondition(objs);
            //
            //			string loadingSql = string.Format(" select rcard as sn,tblonwipitem.* from  tblonwipitem where 1=1 {0}",rcardCondtion);
            //
            //			object[] loadingObjs = this.DataProvider.CustomQuery(
            //				typeof(ComponentLoadingTracking),
            //				new PagerCondition(loadingSql,"sn",inclusive,exclusive,true));

            #endregion

            #region

            if ((keypartsStart == "" || keypartsStart == null) && (keypartsEnd == "" || keypartsEnd == null))
            {

                sSQL = String.Format(
                    @"SELECT rcard as sn,{7} FROM tblonwipitem a {8}
						WHERE 1 = 1 and actiontype='0' {0} {1} {2} {3} {4} {5} {6}
						AND rcardseq =
								(SELECT MAX (rcardseq)
									FROM tblonwipitem b
									WHERE a.rcard = b.rcard
									AND a.mocode = b.mocode and actiontype='0' 
									{0} {1} {2} {3} {4} {5} {6}
									)
						AND mseq =
								(SELECT MAX (mseq)
									FROM tblonwipitem b
									WHERE a.rcard = b.rcard
									AND a.mocode = b.mocode
									AND a.rcardseq = b.rcardseq  and actiontype='0'
									{0} {1} {2} {3} {4} {5} {6}
									)",
                    innoCondition,
                    insideItemCodeCondition,
                    vendorCodeCondition,
                    vendorItemCodeCondition,
                    lotnoCondition,
                    dateCodeCondition,
                    shiftDayCondition,
                    selectColunms,
                    joinCondition
                    );
            }
            else
            {
                sSQL = String.Format(
                    @"SELECT rcard as sn,{9} FROM tblonwipitem a {10}
						WHERE 1 = 1 {0} {1} {2} {3} {4} {5} {6} {7} AND mcardtype = {8}
						AND rcardseq =
								(SELECT MAX (rcardseq)
									FROM tblonwipitem b
									WHERE a.rcard = b.rcard
									AND a.mocode = b.mocode  and actiontype='0'
									{0} {1} {2} {3} {4} {5} {6} {7} 
									AND mcardtype = {8}
									)
						AND mseq =
								(SELECT MAX (mseq)
									FROM tblonwipitem b
									WHERE a.rcard = b.rcard
									AND a.mocode = b.mocode
									AND a.rcardseq = b.rcardseq  and actiontype='0'
									{0} {1} {2} {3} {4} {5} {6} {7} 
									AND mcardtype = {8}
									)",
                    innoCondition,
                    insideItemCodeCondition,
                    vendorCodeCondition,
                    vendorItemCodeCondition,
                    lotnoCondition,
                    dateCodeCondition,
                    shiftDayCondition,
                    keypartsStartCondition,
                    MCardType.MCardType_Keyparts,
                    selectColunms,
                    joinCondition
                    );
            }

#if DEBUG
            Log.Info(new PagerCondition(sSQL, "sn", inclusive, exclusive, true).SQLText);
#endif

            object[] loadingObjs = this.DataProvider.CustomQuery(
                typeof(ComponentLoadingTracking),
                new PagerCondition(sSQL, "sn", inclusive, exclusive, true));

            if (loadingObjs == null) return null;
            #endregion

            ArrayList rcardList = new ArrayList();
            foreach (ComponentLoadingTracking loadObj in loadingObjs)
            {
                if (rcardList.Contains(loadObj.SN) == false)
                    rcardList.Add(loadObj.SN);
            }

            object[] ItemTracingObjs = this.QueryItemTracingForword(rcardList);	//tblsimulation 中的最新记录,如果是已转换,则没有记录在tblsimulation中
            object[] TransObjs = this.getTransObjs(rcardList);						//序列号转换记录

            return this.MapToComponentLoadingTracking(loadingObjs, ItemTracingObjs, TransObjs);
        }

        private object[] getTransObjs(ArrayList rcardList)
        {
            if (rcardList == null || rcardList.Count == 0) return null;
            string SnCondition = string.Empty;
            SnCondition = "and tblonwipcardtrans.tcard in ('" + String.Join("','", (string[])rcardList.ToArray(typeof(string))) + "')";
            string sql = string.Format(" select tblonwipcardtrans.* from tblonwipcardtrans where 1=1 {0}", SnCondition);
            return this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer), new SQLCondition(sql));
        }

        public int QueryComponentLoadingTrackingCount2(
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode,
            string inno, string keypartsStart, string keypartsEnd,
            int startDate, int endDate)
        {
            string insideItemCodeCondition = "";
            if (insideItemCode != "" && insideItemCode != null)
            {
                insideItemCodeCondition = string.Format(@" and MITEMCODE like '{0}%'", insideItemCode.ToUpper());
            }

            string vendorCodeCondition = "";
            if (vendorCode != "" && vendorCode != null)
            {
                vendorCodeCondition = string.Format(@" and VENDORCODE like '{0}%'", vendorCode.ToUpper());
            }

            string vendorItemCodeCondition = "";
            if (vendorItemCode != "" && vendorItemCode != null)
            {
                vendorItemCodeCondition = string.Format(@" and VENDORITEMCODE like '{0}%'", vendorItemCode.ToUpper());
            }

            string lotnoCondition = "";
            if (lotno != "" && lotno != null)
            {
                lotnoCondition = string.Format(@" and LOTNO like '{0}%'", lotno.ToUpper());
            }

            string dateCodeCondition = "";
            if (dateCode != "" && dateCode != null)
            {
                dateCodeCondition = string.Format(@" and DATECODE like '{0}%'", dateCode.ToUpper());
            }

            //集成料号
            string innoCondition = "";
            if (inno != "" && inno != null)
            {
                innoCondition = string.Format(@" and MCARD like '{0}%' and mcardtype = {1} ", inno.ToUpper(), MCardType.MCardType_INNO);
            }

            if ((keypartsStart != "" && keypartsStart != null) &&
                (keypartsEnd == "" || keypartsEnd == null))
            {
                keypartsEnd = keypartsStart;
            }

            if ((keypartsEnd != "" && keypartsEnd != null) &&
                (keypartsStart == "" || keypartsStart == null))
            {
                keypartsStart = keypartsEnd;
            }

            string keypartsStartCondition = FormatHelper.GetRCardRangeSql("mcard", keypartsStart.ToUpper(), keypartsEnd.ToUpper());

            string shiftDayCondition = FormatHelper.GetDateRangeSql("mdate", startDate, endDate);


            #region 不用的代码
            /*	Removed by Icyer 2006/12/25 @ YHI	查询记录总数为什么要全部查出来？
			string sSQL = String.Empty;
			if( (keypartsStart == "" || keypartsStart == null) && (keypartsEnd == "" || keypartsEnd == null) )
			{
				sSQL = String.Format(
					@"select distinct 
						RCARD , max(rcardseq) as RCARDSEQ,max(MSEQ) as MSEQ,mocode
				  from 
						tblonwipitem where 1=1 and actiontype='0' {0} {1} {2} {3} {4} {5} {6}
					group by {7}
				",
					innoCondition,
					insideItemCodeCondition,
					vendorCodeCondition,
					vendorItemCodeCondition,
					lotnoCondition,
					dateCodeCondition,
					shiftDayCondition,
					" RCARD,mocode "
					);	
			}
			else
			{
				sSQL = String.Format(
					@"select distinct 
						RCARD , max(rcardseq) as RCARDSEQ,max(MSEQ) as MSEQ,mocode
				  from 
						tblonwipitem where 1=1 and actiontype='0' {0} {1} {2} {3} {4} {5} {6} 
				   {7} {8} and mcardtype = {9} 
				  group by {10}	
				",
					innoCondition,
					insideItemCodeCondition,
					vendorCodeCondition,
					vendorItemCodeCondition,
					lotnoCondition,
					dateCodeCondition,
					shiftDayCondition,
					keypartsStartCondition,
					"",
					MCardType.MCardType_Keyparts,
					" RCARD,mocode "
					);
			}

#if DEBUG
			Log.Info( new PagerCondition(sSQL,"sn",0,int.MaxValue,true).SQLText );
#endif

			object[] objs = this.DataProvider.CustomQuery(
				typeof(OnWIPItem),
				new PagerCondition(sSQL,0,int.MaxValue,true));

			if(objs != null)
			{
				return objs.Length;
			}
			return 0;
			*/
            #endregion

            // Added by Icyer 2006/12/25 @ YHI	修改上面注释掉的
            string sSQL = String.Empty;
            if ((keypartsStart == "" || keypartsStart == null) && (keypartsEnd == "" || keypartsEnd == null))
            {
                sSQL = String.Format(
                    @"select distinct 
						RCARD as sn, max(rcardseq) as RCARDSEQ,max(MSEQ) as MSEQ,mocode
				  from 
						tblonwipitem where 1=1 and actiontype='0' {0} {1} {2} {3} {4} {5} {6}
					group by {7}",
                    innoCondition,
                    insideItemCodeCondition,
                    vendorCodeCondition,
                    vendorItemCodeCondition,
                    lotnoCondition,
                    dateCodeCondition,
                    shiftDayCondition,
                    " RCARD,mocode "
                    );
            }
            else
            {
                sSQL = String.Format(
                    @"select distinct 
						RCARD as sn, max(rcardseq) as RCARDSEQ,max(MSEQ) as MSEQ,mocode
				  from 
						tblonwipitem where 1=1 and actiontype='0' {0} {1} {2} {3} {4} {5} {6} 
				   {7} {8} and mcardtype = {9} 
				  group by {10}		",
                    innoCondition,
                    insideItemCodeCondition,
                    vendorCodeCondition,
                    vendorItemCodeCondition,
                    lotnoCondition,
                    dateCodeCondition,
                    shiftDayCondition,
                    keypartsStartCondition,
                    "",
                    MCardType.MCardType_Keyparts,
                    " RCARD,mocode "
                    );
            }

#if DEBUG
            Log.Info(new PagerCondition(sSQL, "sn", 0, int.MaxValue, true).SQLText);
#endif


            object[] loadingObjs = this.DataProvider.CustomQuery(
                typeof(ComponentLoadingTracking),
                new SQLCondition(sSQL));

            if (loadingObjs == null) return 0;


            ArrayList rcardList = new ArrayList();
            foreach (ComponentLoadingTracking loadObj in loadingObjs)
            {
                if (rcardList.Contains(loadObj.SN) == false)
                    rcardList.Add(loadObj.SN);
            }

            object[] ItemTracingObjs = this.QueryItemTracingForword(rcardList);	//tblsimulation 中的最新记录,如果是已转换,则没有记录在tblsimulation中
            object[] TransObjs = this.getTransObjs(rcardList);						//序列号转换记录


            object[] returnObjects = this.MapToComponentLoadingTracking(loadingObjs, ItemTracingObjs, TransObjs);

            if (returnObjects == null)
            {
                return 0;
            }

            return returnObjects.Length;

            //int iCount = this.DataProvider.CustomQuery(typeof(ComponentLoadingTracking),new SQLCondition(sSQL));
            //return iCount;
        }

        private string getSqlCondition(object[] onWipItemObjs)
        {
            if (onWipItemObjs == null || onWipItemObjs.Length == 0) { return string.Empty; }
            System.Text.StringBuilder conditionBuilder = new System.Text.StringBuilder();
            conditionBuilder.Append("and (rcard,rcardseq,MSEQ,mocode) in (('',0,0,'')");
            foreach (OnWIPItem onwip in onWipItemObjs)
            {
                conditionBuilder.Append(string.Format(" ,('{0}',{1},{2},'{3}')", onwip.RunningCard, onwip.RunningCardSequence, onwip.MSequence, onwip.MOCode));
            }
            conditionBuilder.Append(")");

            return conditionBuilder.ToString();
        }

        private object[] MapToComponentLoadingTracking(object[] loadingObjs, object[] ItemTracingObjs, object[] TransObjs)
        {
            ArrayList returnList = new ArrayList();
            Hashtable tracingHT = new Hashtable();	//追溯记录
            Hashtable tracnsHT = new Hashtable();	//转换记录
            if (ItemTracingObjs != null)
            {
                foreach (ItemTracing tracingObj in ItemTracingObjs)
                {
                    ComponentLoadingTracking newLoadObj = new ComponentLoadingTracking();
                    newLoadObj.SN = tracingObj.RCard;
                    newLoadObj.SNSeq = Convert.ToInt32(tracingObj.RCardSeq);
                    newLoadObj.MoCode = tracingObj.MOCode;
                    newLoadObj.ModelCode = tracingObj.ModelCode;
                    newLoadObj.ItemCode = tracingObj.ItemCode;
                    newLoadObj.SNState = tracingObj.ItemStatus;
                    newLoadObj.OperationCode = tracingObj.OPCode;
                    newLoadObj.RouteCode = tracingObj.RouteCode;
                    newLoadObj.SegmentCode = tracingObj.SegmentCode;
                    newLoadObj.StepSequenceCode = tracingObj.LineCode;
                    newLoadObj.ResourceCode = tracingObj.ResCode;
                    newLoadObj.MaintainDate = tracingObj.MaintainDate;
                    newLoadObj.MaintainTime = tracingObj.MaintainTime;
                    newLoadObj.MaintainUser = tracingObj.MaintainUser;
                    newLoadObj.MCard = tracingObj.TCard;

                    returnList.Add(newLoadObj);
                    if (tracingHT.ContainsKey(tracingObj.RCard) == false)
                        tracingHT.Add(tracingObj.RCard, tracingObj.RCard);
                }
            }
            if (TransObjs != null)
            {
                foreach (OnWIPCardTransfer cardTansfer in TransObjs)
                {
                    if (tracnsHT.ContainsKey(cardTansfer.TranslateCard) == false)
                        tracnsHT.Add(cardTansfer.TranslateCard, cardTansfer);
                }
            }
            foreach (ComponentLoadingTracking loadObj in loadingObjs)
            {
                string rcard = loadObj.SN;
                if (!tracingHT.Contains(rcard))
                {
                    if (tracnsHT.Contains(rcard))
                    {
                        OnWIPCardTransfer htTransfer = ((OnWIPCardTransfer)tracnsHT[rcard]);
                        loadObj.OperationCode = htTransfer.OPCode;
                        loadObj.RouteCode = htTransfer.RouteCode;
                        loadObj.ResourceCode = htTransfer.ResourceCode;
                        loadObj.SegmentCode = htTransfer.SegmnetCode;
                        loadObj.StepSequenceCode = htTransfer.StepSequenceCode;
                        loadObj.MaintainDate = htTransfer.MaintainDate;
                        loadObj.MaintainTime = htTransfer.MaintainTime;
                        loadObj.MaintainUser = htTransfer.MaintainUser;
                    }
                    loadObj.SNState = "TRANS"; //在tblsimulationreport 中查询不到的rcard,默认产品状态为已转换
                    if (returnList.Contains(loadObj) == false)
                        returnList.Add(loadObj);
                }
            }
            return (ComponentLoadingTracking[])returnList.ToArray(typeof(ComponentLoadingTracking));
        }

        #endregion


        public object[] QueryComponentLoadingSplitTracking(
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode,
            string inno, string keypartsStart, string keypartsEnd,
            int startDate, int endDate,
            int inclusive, int exclusive)
        {
            string insideItemCodeCondition = "";
            if (insideItemCode != "" && insideItemCode != null)
            {
                insideItemCodeCondition = string.Format(@" and MITEMCODE like '{0}%'", insideItemCode.ToUpper());
            }

            string vendorCodeCondition = "";
            if (vendorCode != "" && vendorCode != null)
            {
                vendorCodeCondition = string.Format(@" and VENDORCODE like '{0}%'", vendorCode.ToUpper());
            }

            string vendorItemCodeCondition = "";
            if (vendorItemCode != "" && vendorItemCode != null)
            {
                vendorItemCodeCondition = string.Format(@" and VENDORITEMCODE like '{0}%'", vendorItemCode.ToUpper());
            }

            string lotnoCondition = "";
            if (lotno != "" && lotno != null)
            {
                lotnoCondition = string.Format(@" and LOTNO like '{0}%'", lotno.ToUpper());
            }

            string dateCodeCondition = "";
            if (dateCode != "" && dateCode != null)
            {
                dateCodeCondition = string.Format(@" and DATECODE like '{0}%'", dateCode.ToUpper());
            }

            //集成料号
            string innoCondition = "";
            if (inno != "" && inno != null)
            {
                innoCondition = string.Format(@" and MCARD like '{0}%' and mcardtype = {1} ", inno.ToUpper(), MCardType.MCardType_INNO);
            }

            if ((keypartsStart != "" && keypartsStart != null) &&
                (keypartsEnd == "" || keypartsEnd == null))
            {
                keypartsEnd = keypartsStart;
            }

            if ((keypartsEnd != "" && keypartsEnd != null) &&
                (keypartsStart == "" || keypartsStart == null))
            {
                keypartsStart = keypartsEnd;
            }

            string keypartsStartCondition = FormatHelper.GetRCardRangeSql("mcard", keypartsStart.ToUpper(), keypartsEnd.ToUpper());

            string shiftDayCondition = FormatHelper.GetDateRangeSql("mdate", startDate, endDate);


            //追溯分板信息
            string tcardsql = string.Format(" select tblonwipcardtrans.* from tblonwipcardtrans where 1=1 and tcard in (select tblonwipitem.rcard  from tblonwipitem where 1=1 and actiontype='0' {0}{1}{2}{3}{4}{5}{6}{7}) "
                , insideItemCodeCondition
                , vendorCodeCondition
                , vendorItemCodeCondition
                , lotnoCondition
                , dateCodeCondition
                , innoCondition
                , keypartsStartCondition
                , shiftDayCondition
                );
            object[] tObjects = this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer), new SQLCondition(tcardsql));
            if (tObjects == null || tObjects.Length == 0) return null;

            ArrayList rcardList = new ArrayList();
            foreach (OnWIPCardTransfer cardtrans in tObjects)
            {
                if (rcardList.Contains(cardtrans.RunningCard) == false)
                    rcardList.Add(cardtrans.RunningCard);
            }

            return this.QueryItemTracingForword(rcardList);
        }

        public object[] QueryItemTracingForword(ArrayList rcardList)
        {
            if (rcardList == null || rcardList.Count == 0) return null;

            string SnCondition = string.Empty;
            SnCondition = "and tblsimulationreport.rcard in ('" + String.Join("','", (string[])rcardList.ToArray(typeof(string))) + "')";

            string selectColnums = "TBLSIMULATIONREPORT.RESCODE || ' - ' || tblres.resdesc AS RESCODE,TBLSIMULATIONREPORT.RCARDSEQ,TBLSIMULATIONREPORT.SSCODE || ' - ' || tblss.ssdesc AS SSCODE,";
            selectColnums += "TBLSIMULATIONREPORT.SEGCODE || ' - ' || tblseg.segdesc AS SEGCODE,TBLSIMULATIONREPORT.ROUTECODE,TBLSIMULATIONREPORT.MODELCODE,TBLSIMULATIONREPORT.MOCODE,";
            selectColnums += "TBLSIMULATIONREPORT.STATUS,TBLSIMULATIONREPORT.ITEMCODE || ' - ' || tblitem.itemdesc AS ITEMCODE,TBLSIMULATIONREPORT.MUSER || ' - ' || tbluser.username AS MUSER,";
            selectColnums += "TBLSIMULATIONREPORT.RCARD,TBLSIMULATIONREPORT.TCARD,TBLSIMULATIONREPORT.MTIME,TBLSIMULATIONREPORT.OPCODE || ' - ' || tblop.opdesc AS OPCODE,TBLSIMULATIONREPORT.MDATE,TBLSIMULATIONREPORT.LACTION, '' AS OPTYPE";

            string joinCondition = "   LEFT OUTER JOIN tblitem ON TBLSIMULATIONREPORT.Itemcode=tblitem.itemcode";
            joinCondition += " LEFT OUTER JOIN tblop ON TBLSIMULATIONREPORT.OPCODE=tblop.OPCODE";
            joinCondition += " LEFT OUTER JOIN tblseg ON TBLSIMULATIONREPORT.segcode=tblseg.segcode";
            joinCondition += " LEFT OUTER JOIN tblss ON TBLSIMULATIONREPORT.sscode=tblss.sscode";
            joinCondition += " LEFT OUTER JOIN tblres ON TBLSIMULATIONREPORT.rescode=tblres.rescode";
            joinCondition += " LEFT OUTER JOIN tbluser ON TBLSIMULATIONREPORT.muser=tbluser.usercode";
            string sql = string.Format("select {0} from tblsimulationreport {1} where 1=1 ", selectColnums, joinCondition);
            sql += SnCondition;
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemTracing), new PagerCondition(sql, "tblsimulationreport.rcard", 0, int.MaxValue));

            if (objs == null)
            {
                return null;
            }

            BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
            QueryFacade2 facade2 = new QueryFacade2(this.DataProvider);


            // Added by Icyer 2006/12/26 @ YHI
            Hashtable htOP = new Hashtable();
            Hashtable htItem2OP = new Hashtable();
            // Added end
            for (int i = 0; i < objs.Length; i++)
            {
                ItemTracing objIT = objs[i] as ItemTracing;
                //added by jessie lee for CS0041,2005/10/11,P4.10
                //根据前面已经得到的rcard

                if (objIT.ItemStatus != ProductStatus.GOOD)
                {
                    //added by jessie lee, 2005/12/6, for CS179
                    /*产品追溯管理：
                            维修的资料已在产品追溯的信息中，因而将产品状态的不良品状态，细化为：送修、待修、维修中、报废、拆解、维修完成。不再有不良品的产品状态，而以上述7种状态来代替。

                            下面是产品状态的整体描述：
                            产品状态：
                            1. 良品：只要采集为良品，产品的状态就为良品。如良品/不良品采集为良品，批通过采集为良品以及其他默认为良品的采集
                            2. 送修：已采集为不良，如良品/不良品采集为不良品或抽检发现为不良品，或拆解后得到的不良,但尚未被送修确认的状态
                            3. 待修：已被送修确认，尚未进行维修的状态
                            4. 维修中：正在维修的状态
                            5. 报废：产品被指定为报废
                            6. 拆解：产品被拆解
                            7. 维修完成：产品已被维修完成，尚未回到产线生产
                            8. 判退品：已被整批批退的产品。该批中的所有产品的状态都是判退品状态
                        */


                    string tsSQL = string.Format(@"select scrapcause, frmtime, frmdate, frmuser, a.shifttypecode, a.shiftcode,
								tpcode, scardseq, frmmemo, tcardseq, tffullpath, shiftday, refrescode,
								transstatus, refopcode, a.COPCODE || ' - ' || tblop.opdesc AS COPCODE, refroutecode, CRESCODE || ' - ' || TBLRES.RESDESC AS CRESCODE, refmocode,
								cardtype, rrcard, frminputtype, tsmemo, tstime, confirmtime, tsuser,
								confirmdate, tsdate, confirmuser, tsstatus, scard, tsid, tcard,
								tstimes, a.eattribute1, rcardseq, tsrescode, modelcode, frmrescode,
								itemcode, frmopcode, a.mtime, frmsscode, a.mdate, frmsegcode, 
								CASE tsstatus
									WHEN 'tsstatus_ts'
										THEN (SELECT muser
												FROM tbltserrorcause
												WHERE tbltserrorcause.tsid = a.tsid AND ROWNUM = 1)
									WHEN 'tsstatus_reflow'
										THEN a.tsuser
									WHEN 'tsstatus_complete'
										THEN a.tsuser
									WHEN 'tsstatus_confirm'
										THEN a.confirmuser
									WHEN 'tsstatus_split'
										THEN a.tsuser
									WHEN 'tsstatus_scrap'
										THEN a.tsuser
									ELSE a.muser
								END AS muser,
								frmroutecode, rcard, mocode from tblts a LEFT OUTER JOIN TBLRES ON A.CRESCODE = TBLRES.RESCODE
                                LEFT OUTER JOIN tblop ON a.copcode=tblop.OPCODE
								where rcard = '{0}' and rcardseq = {1} and mocode = '{2}' and tsstatus <> '{3}'",
                        objIT.RCard,
                        objIT.RCardSeq,
                        objIT.MOCode,
                        TSStatus.TSStatus_New);

                    Object[] tsObjs = this.DataProvider.CustomQuery(
                        typeof(BenQGuru.eMES.Domain.TS.TS),
                        new PagerCondition(tsSQL, 0, int.MaxValue));

                    if (tsObjs != null)
                    {
                        BenQGuru.eMES.Domain.TS.TS tsObj = tsObjs[0] as BenQGuru.eMES.Domain.TS.TS;
                        objIT.ResCode = tsObj.ConfirmResourceCode;
                        objIT.LastAction = "TS";
                        objIT.MaintainDate = tsObj.MaintainDate;
                        objIT.MaintainTime = tsObj.MaintainTime;

                        //代录人替换
                        //待修是tblts 的confirmuser
                        //维修完成和维修是tblerrorcause 的muser
                        //拆解和报废是tblts的tblts 的tsuser
                        objIT.MaintainUser = tsObj.MaintainUser;
                        UserFacade userFacade = new UserFacade(this.DataProvider);
                        object userObject = userFacade.GetUser(objIT.MaintainUser);
                        objIT.MaintainUser += " - ";
                        if (userObject != null)
                        {
                            objIT.MaintainUser = objIT.MaintainUser + ((User)userObject).UserName;
                        }

                        objIT.OPCode = tsObj.ConfirmOPCode;
                        objIT.OPType = "TS";
                        objIT.RouteCode = string.Empty;
                        objIT.LineCode = string.Empty;
                        objIT.SegmentCode = string.Empty;
                        objIT.ItemStatus = tsObj.TSStatus;
                        continue;
                    }
                    //added end
                }

                if (string.Compare(objIT.ItemStatus, ProductStatus.NG, true) == 0)
                {
                    objIT.ItemStatus = TSStatus.TSStatus_New;
                }

                // Modified by Icyer 2006/12/26
                //object op = bmFacade.GetOperation( objIT.OPCode) ;
                object op = null;
                if (htOP.ContainsKey(objIT.OPCode) == false)
                    htOP.Add(objIT.OPCode, bmFacade.GetOperation(objIT.OPCode));
                op = htOP[objIT.OPCode];
                // Modified end
                if (op == null)
                {
                    objIT.OPType = string.Empty;
                }
                else
                {

                    if (((Operation)op).OPControl[(int)BenQGuru.eMES.BaseSetting.OperationList.OutsideRoute] == '1')
                    {
                        objIT.OPType = ((Operation)op).OPControl;
                    }
                    else
                    {
                        /*	Removed by Icyer 2006/12/26
                        object ir2o = facade2.GetItemRoute2Operation(
                            objIT.ItemCode,
                            objIT.RouteCode,
                            objIT.OPCode
                            ) ;
                        */
                        object ir2o = null;
                        string strKey = objIT.ItemCode + ":" + objIT.RouteCode + ":" + objIT.OPCode;
                        if (htItem2OP.ContainsKey(strKey) == false)
                            htItem2OP.Add(strKey, facade2.GetItemRoute2Operation(objIT.ItemCode, objIT.RouteCode, objIT.OPCode));
                        ir2o = htItem2OP[strKey];

                        if (ir2o == null)
                        {
                            objIT.OPType = string.Empty;
                        }
                        else
                        {
                            objIT.OPType = ((Operation)op).OPControl;
                        }
                    }
                }
            }

            return objs;
        }


        public int QueryComponentLoadingSplitTrackingCount(
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode,
            string inno, string keypartsStart, string keypartsEnd,
            int startDate, int endDate)
        {
            string insideItemCodeCondition = "";
            if (insideItemCode != "" && insideItemCode != null)
            {
                insideItemCodeCondition = string.Format(@" and MITEMCODE like '{0}%'", insideItemCode.ToUpper());
            }

            string vendorCodeCondition = "";
            if (vendorCode != "" && vendorCode != null)
            {
                vendorCodeCondition = string.Format(@" and VENDORCODE like '{0}%'", vendorCode.ToUpper());
            }

            string vendorItemCodeCondition = "";
            if (vendorItemCode != "" && vendorItemCode != null)
            {
                vendorItemCodeCondition = string.Format(@" and VENDORITEMCODE like '{0}%'", vendorItemCode.ToUpper());
            }

            string lotnoCondition = "";
            if (lotno != "" && lotno != null)
            {
                lotnoCondition = string.Format(@" and LOTNO like '{0}%'", lotno.ToUpper());
            }

            string dateCodeCondition = "";
            if (dateCode != "" && dateCode != null)
            {
                dateCodeCondition = string.Format(@" and DATECODE like '{0}%'", dateCode.ToUpper());
            }

            //集成料号
            string innoCondition = "";
            if (inno != "" && inno != null)
            {
                innoCondition = string.Format(@" and MCARD like '{0}%' and mcardtype = {1} ", inno.ToUpper(), MCardType.MCardType_INNO);
            }

            if ((keypartsStart != "" && keypartsStart != null) &&
                (keypartsEnd == "" || keypartsEnd == null))
            {
                keypartsEnd = keypartsStart;
            }

            if ((keypartsEnd != "" && keypartsEnd != null) &&
                (keypartsStart == "" || keypartsStart == null))
            {
                keypartsStart = keypartsEnd;
            }

            string keypartsStartCondition = FormatHelper.GetRCardRangeSql("mcard", keypartsStart.ToUpper(), keypartsEnd.ToUpper());

            string shiftDayCondition = FormatHelper.GetDateRangeSql("mdate", startDate, endDate);


            /*	Removed by Icyer 2006/12/25 @ YHI	用COUNT(*)查询总数
            //追溯分板信息
            string tcardsql = string.Format(" select tblonwipcardtrans.* from tblonwipcardtrans where 1=1  and tcard in (select tblonwipitem.rcard  from tblonwipitem where 1=1 and actiontype='0' {0}{1}{2}{3}{4}{5}{6}{7}) "
                ,insideItemCodeCondition
                ,vendorCodeCondition
                ,vendorItemCodeCondition
                ,lotnoCondition
                ,dateCodeCondition
                ,innoCondition
                ,keypartsStartCondition
                ,shiftDayCondition
                );
            object[] tObjects = this.DataProvider.CustomQuery( typeof(OnWIPCardTransfer) , new SQLCondition( tcardsql )) ;
            if(tObjects == null || tObjects.Length == 0) return 0;

			
            return tObjects.Length;
            */
            string tcardsql = string.Format("SELECT COUNT(*) FROM ( select tblonwipcardtrans.* from tblonwipcardtrans where 1=1  and tcard in (select tblonwipitem.rcard  from tblonwipitem where 1=1 and actiontype='0' {0}{1}{2}{3}{4}{5}{6}{7}) ) "
                , insideItemCodeCondition
                , vendorCodeCondition
                , vendorItemCodeCondition
                , lotnoCondition
                , dateCodeCondition
                , innoCondition
                , keypartsStartCondition
                , shiftDayCondition
                );
            return this.DataProvider.GetCount(new SQLCondition(tcardsql));
        }


        //public object[] QueryINNOInfo(string sn,
        //    string insideItemCode,string vendorCode,string vendorItemCode,
        //    string lotno,string dateCode,
        //    string inno,int inclusive,int exclusive)
        //{
        //			string insideItemCodeCondition = "";
        //			if( insideItemCode != "" && insideItemCode != null )
        //			{
        //				insideItemCodeCondition = string.Format( @" and MITEMCODE like '{0}%'",insideItemCode.ToUpper());
        //			}
        //
        //			string vendorCodeCondition = "";
        //			if( vendorCode != "" && vendorCode != null )
        //			{
        //				vendorCodeCondition = string.Format( @" and VENDORCODE like '{0}%'",vendorCode.ToUpper());
        //			}
        //
        //			string vendorItemCodeCondition = "";
        //			if( vendorItemCode != "" && vendorItemCode != null )
        //			{
        //				vendorItemCodeCondition = string.Format( @" and VENDORITEMCODE like '{0}%'",vendorItemCode.ToUpper());
        //			}
        //
        //			string lotnoCondition = "";
        //			if( lotno != "" && lotno != null )
        //			{
        //				lotnoCondition = string.Format( @" and LOTNO like '{0}%'",lotno.ToUpper());
        //			}
        //
        //			string dateCodeCondition = "";
        //			if( dateCode != "" && dateCode != null )
        //			{
        //				dateCodeCondition = string.Format( @" and DATECODE like '{0}%'",dateCode.ToUpper());
        //			}

        //#if DEBUG
        //            Log.Info(new PagerCondition(
        //                string.Format(
        //                @"select distinct {0} from TBLMINNO where mitempackedno in ( select mcard from TBLONWIPITEM where RCARD = '{1}' and mcardtype = '1')",
        //                "mitempackedno as mcard,mitemcode,vendorcode,vendoritemcode,lotno,datecode,version,PCBA,BIOS", sn), "mcard", inclusive, exclusive, true).SQLText);
        //#endif
        //            return this.DataProvider.CustomQuery(
        //                typeof(ComponentLoadingTracking),
        //                new PagerCondition(
        //                string.Format(
        //                @"select distinct {0} from TBLMINNO where mitempackedno in ( select mcard from TBLONWIPITEM where RCARD = '{1}' and mcardtype = '1') ",
        //                "mitempackedno as mcard,mitemcode,vendorcode,vendoritemcode,lotno,datecode,version,PCBA,BIOS", sn), "mcard", inclusive, exclusive, true));
        //}		

        //public int QueryINNOInfoCount(string sn,
        //    string insideItemCode,string vendorCode,string vendorItemCode,
        //    string lotno,string dateCode,string inno)
        //{
        //			string insideItemCodeCondition = "";
        //			if( insideItemCode != "" && insideItemCode != null )
        //			{
        //				insideItemCodeCondition = string.Format( @" and MITEMCODE like '{0}%'",insideItemCode.ToUpper());
        //			}
        //
        //			string vendorCodeCondition = "";
        //			if( vendorCode != "" && vendorCode != null )
        //			{
        //				vendorCodeCondition = string.Format( @" and VENDORCODE like '{0}%'",vendorCode.ToUpper());
        //			}
        //
        //			string vendorItemCodeCondition = "";
        //			if( vendorItemCode != "" && vendorItemCode != null )
        //			{
        //				vendorItemCodeCondition = string.Format( @" and VENDORITEMCODE like '{0}%'",vendorItemCode.ToUpper());
        //			}
        //
        //			string lotnoCondition = "";
        //			if( lotno != "" && lotno != null )
        //			{
        //				lotnoCondition = string.Format( @" and LOTNO like '{0}%'",lotno.ToUpper());
        //			}
        //
        //			string dateCodeCondition = "";
        //			if( dateCode != "" && dateCode != null )
        //			{
        //				dateCodeCondition = string.Format( @" and DATECODE like '{0}%'",dateCode.ToUpper());
        //			}
        //#if DEBUG
        //            Log.Info(string.Format(@"select {0} from TBLMINNO where mitempackedno in ( select mcard from TBLONWIPITEM where RCARD = '{1}' and mcardtype = '1') ", "count(mitempackedno)", sn));
        //#endif
        //            return this.DataProvider.GetCount(
        //                new SQLCondition(
        //                string.Format(@"select {0} from TBLMINNO where mitempackedno in ( select mcard from TBLONWIPITEM where RCARD = '{1}' and mcardtype = '1') ", "count(mitempackedno)", sn)));

        //}

        #region QueryINNOInfo2 (添加OPCODE查询条件)

        public object[] QueryINNOInfo2(string sn,
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode,
            string inno, string opcode, int inclusive, int exclusive)
        {


            //需要查询序列号转换前的rcard
            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            ArrayList array = new ArrayList();
            array.Add(sn);
            castDownHelper.GetAllRCard(ref array, sn);
            string[] rCards = (string[])array.ToArray(typeof(System.String));
            SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));

            /* modified by jessie lee, 2006-3-25 
             * 上料和下料的记录都在onwipitem里查询 */

            //			string sql = string.Format(
            //				@"select  {0} from TBLMINNO where islast='Y' and INNO in ( select mcard from TBLONWIPITEM where 1=1  and mcardtype = '1' {1})  /*AND OPCODE = '{2}'*/ ",
            //				" TBLMINNO.* ",SnCondition,opcode);
            //
            //			object[] minnoObjs = this.DataProvider.CustomQuery(
            //				typeof(TracedMinno),
            //				new PagerCondition(
            //				sql,"INNO desc",inclusive,exclusive,true));

            string onwipDropSql = string.Format(
                /* 上料 */
                @" SELECT TBLONWIPITEM.rcard, TBLONWIPITEM.mcardtype, TBLONWIPITEM.rcardseq, TBLONWIPITEM.mcard, TBLONWIPITEM.mocode, TBLONWIPITEM.itemcode, TBLONWIPITEM.mitemcode, TBLONWIPITEM.tpcode,
					TBLONWIPITEM.shiftcode, TBLONWIPITEM.shifttypecode, TBLONWIPITEM.rescode, TBLONWIPITEM.sscode, TBLONWIPITEM.segcode, TBLONWIPITEM.routecode,
					TBLONWIPITEM.transstatus, TBLONWIPITEM.modelcode, TBLONWIPITEM.lotno, TBLONWIPITEM.pcba, TBLONWIPITEM.bios, TBLONWIPITEM.VERSION, TBLONWIPITEM.vendoritemcode,
					TBLONWIPITEM.vendorcode, TBLONWIPITEM.datecode,  TBLONWIPITEM.mseq, TBLONWIPITEM.qty,
					0 as actiontype,  TBLONWIPITEM.OPCODE || ' - ' || tblop.opdesc AS OPCODE,
					TBLONWIPITEM.MUSER || ' - ' || tbluser.username AS MUSER, TBLONWIPITEM.mdate, TBLONWIPITEM.mtime FROM tblonwipitem
                                    LEFT OUTER JOIN tblop ON TBLONWIPITEM.Opcode=tblop.opcode 
                                    LEFT OUTER JOIN tbluser ON TBLONWIPITEM.Muser=tbluser.usercode
				WHERE 1 = 1
					AND mcardtype = '1'
					{0}"
                + " union " +
                /* 下料 */
                @" SELECT TBLONWIPITEM.rcard, TBLONWIPITEM.mcardtype, TBLONWIPITEM.rcardseq, TBLONWIPITEM.mcard, TBLONWIPITEM.mocode, TBLONWIPITEM.itemcode, TBLONWIPITEM.mitemcode, TBLONWIPITEM.tpcode,
					TBLONWIPITEM.shiftcode, TBLONWIPITEM.shifttypecode, TBLONWIPITEM.rescode, TBLONWIPITEM.sscode, TBLONWIPITEM.segcode, TBLONWIPITEM.routecode,
					TBLONWIPITEM.transstatus, TBLONWIPITEM.modelcode, TBLONWIPITEM.lotno, TBLONWIPITEM.pcba, TBLONWIPITEM.bios, TBLONWIPITEM.VERSION, TBLONWIPITEM.vendoritemcode,
					TBLONWIPITEM.vendorcode, TBLONWIPITEM.datecode,  TBLONWIPITEM.mseq, TBLONWIPITEM.qty,
					TBLONWIPITEM.actiontype,  TBLONWIPITEM.DROPOP || ' - ' || tblop.opdesc AS OPCODE,
					TBLONWIPITEM.DROPUSER || ' - ' || tbluser.username AS MUSER, TBLONWIPITEM.dropdate as mdate, TBLONWIPITEM.droptime as mtime FROM tblonwipitem
                                        LEFT OUTER JOIN tblop ON TBLONWIPITEM.DROPOP=tblop.opcode 
                                        LEFT OUTER JOIN tbluser ON TBLONWIPITEM.DROPUSER=tbluser.usercode
				WHERE 1 = 1
					AND mcardtype = '1'
					AND actiontype = 1 {0}", SnCondition);
            object[] OnwipDropObjs = this.DataProvider.CustomQuery(typeof(TracedKeyParts), new SQLCondition(onwipDropSql));

            this.GetMItemName(OnwipDropObjs);

            ArrayList dropMinnoList = new ArrayList();
            if (OnwipDropObjs != null)
            {
                foreach (TracedKeyParts dropOnwipitem in OnwipDropObjs)
                {
                    TracedMinno dropMinno = new TracedMinno();
                    dropMinno.DateCode = dropOnwipitem.DateCode;
                    dropMinno.VendorCode = dropOnwipitem.VendorCode;
                    dropMinno.VendorItemCode = dropOnwipitem.VendorItemCode;
                    dropMinno.Version = dropOnwipitem.Version;
                    dropMinno.BIOS = dropOnwipitem.BIOS;
                    dropMinno.PCBA = dropOnwipitem.PCBA;
                    dropMinno.MaintainUser = dropOnwipitem.MaintainUser;
                    dropMinno.MaintainTime = dropOnwipitem.MaintainTime;
                    dropMinno.MaintainDate = dropOnwipitem.MaintainDate;
                    dropMinno.INNO = dropOnwipitem.MCARD;
                    dropMinno.LotNO = dropOnwipitem.LotNO;
                    dropMinno.ItemCode = dropOnwipitem.ItemCode;
                    dropMinno.Qty = dropOnwipitem.Qty;
                    dropMinno.MOCode = dropOnwipitem.MOCode;
                    dropMinno.RouteCode = dropOnwipitem.RouteCode;
                    dropMinno.OPCode = dropOnwipitem.OPCode;
                    dropMinno.ResourceCode = dropOnwipitem.ResourceCode;
                    dropMinno.MItemCode = dropOnwipitem.MItemCode;
                    dropMinno.MItemName = dropOnwipitem.MItemName;
                    dropMinno.ActionType = dropOnwipitem.ActionType;						//1表示下料,0表示上料


                    dropMinnoList.Add(dropMinno);

                }
            }

            /*
            if(minnoObjs!=null && minnoObjs.Length>0)
            {
                foreach(TracedMinno minno in minnoObjs)
                {
                    minno.ActionType	= 0;						//1表示下料,0表示上料
                    dropMinnoList.Add(minno);
                }
            }
            */


        #endregion

#if DEBUG
            Log.Info(onwipDropSql);
#endif
            return (MINNO[])dropMinnoList.ToArray(typeof(MINNO));

        }

        public int QueryINNOInfoCount2(string sn,
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode, string inno, string opcode)
        {

            //需要查询序列号转换前的rcard
            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            ArrayList array = new ArrayList();
            array.Add(sn);
            castDownHelper.GetAllRCard(ref array, sn);
            string[] rCards = (string[])array.ToArray(typeof(System.String));
            SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));

            /* modified by jessie lee, 2006-3-25
             * 添加查询条件，  islast='Y' ，标识是最后一次维护的一笔 */

            //			string sql = string.Format(
            //				@"select  {0} from TBLMINNO where islast='Y' and INNO in ( select mcard from TBLONWIPITEM where 1=1  and mcardtype = '1' {1})  /*AND OPCODE = '{2}'*/ ",
            //				" count(inno) ",SnCondition,opcode);
            //
            //			int minnoCount = this.DataProvider.GetCount(new SQLCondition(sql));

            #region 批次料下料记录

            string onwipDropSql = string.Format(
                @" SELECT rcard, mcardtype, rcardseq, mcard, mocode, itemcode, mitemcode, tpcode,
					shiftcode, shifttypecode, rescode, sscode, segcode, routecode,
					transstatus, modelcode, lotno, pcba, bios, VERSION, vendoritemcode,
					vendorcode, datecode,  mseq, qty,
					0 as actiontype, opcode,
					muser, mdate, mtime FROM tblonwipitem
				WHERE 1 = 1
					AND mcardtype = '1'
					{0}"
                + " union " +
                /* 下料 */
                @" SELECT rcard, mcardtype, rcardseq, mcard, mocode, itemcode, mitemcode, tpcode,
					shiftcode, shifttypecode, rescode, sscode, segcode, routecode,
					transstatus, modelcode, lotno, pcba, bios, VERSION, vendoritemcode,
					vendorcode, datecode,  mseq, qty,
					actiontype, dropop AS opcode,
					dropuser as muser, dropdate as mdate, droptime as mtime FROM tblonwipitem
				WHERE 1 = 1
					AND mcardtype = '1'
					AND actiontype = 1 {0}", SnCondition);
            int onwipDropCount = this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from ({0})", onwipDropSql)));

            #endregion

#if DEBUG
            Log.Info(onwipDropSql);
#endif
            //return minnoCount + onwipDropCount;
            return onwipDropCount;

        }

        #endregion

        public object[] QueryKeyPartsInfo(string sn,
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode, string keyparts,
            int inclusive, int exclusive)
        {
            //需要查询序列号转换前的rcard
            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            ArrayList array = new ArrayList();
            array.Add(sn);
            castDownHelper.GetAllRCard(ref array, sn);
            string[] rCards = (string[])array.ToArray(typeof(System.String));
            SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));

            //			string sql = string.Format(
            //				@"select tblonwipitem.* from tblonwipitem where 1=1  and mcardtype = '0' {0} order by mdate desc,mtime desc",SnCondition);

            string sql = string.Format(@"select a.* from 
											(
											SELECT rcard, mcardtype, rcardseq, mcard, mocode, itemcode, mitemcode, tpcode,
												shiftcode, shifttypecode, rescode, sscode, segcode, routecode,
												transstatus, modelcode, lotno, pcba, bios, VERSION, vendoritemcode,
												vendorcode, datecode,  mseq, qty, 
												DECODE (actiontype, 1, 0, 0) as actiontype, TBLONWIPITEM.OPCODE || ' - ' || tblop.opdesc AS OPCODE, TBLONWIPITEM.MUSER || ' - ' || tbluser.username AS MUSER,
												tblonwipitem.mdate, tblonwipitem.mtime
											FROM tblonwipitem
                                                LEFT OUTER JOIN tblop ON TBLONWIPITEM.Opcode=tblop.opcode 
                                                LEFT OUTER JOIN tbluser ON TBLONWIPITEM.Muser=tbluser.usercode
											WHERE 1 = 1 AND mcardtype = '0' {0}
											UNION 
											SELECT rcard, mcardtype, rcardseq, mcard, mocode, itemcode, mitemcode, tpcode,
												shiftcode, shifttypecode, rescode, sscode, segcode, routecode,
												transstatus, modelcode, lotno, pcba, bios, VERSION, vendoritemcode,
												vendorcode, datecode,  mseq, qty,
												actiontype, TBLONWIPITEM.DROPOP || ' - ' || tblop.opdesc AS OPCODE,
												TBLONWIPITEM.DROPUSER || ' - ' || tbluser.username AS MUSER, dropdate as mdate, droptime as mtime
											FROM tblonwipitem
                                                 LEFT OUTER JOIN tblop ON TBLONWIPITEM.DROPOP=tblop.opcode 
                                                 LEFT OUTER JOIN tbluser ON TBLONWIPITEM.DROPUSER=tbluser.usercode
											WHERE 1 = 1 AND mcardtype = '0' AND actiontype = 1 {0}
											) a
											order by mdate*1000000+mtime desc", SnCondition);

#if DEBUG
            Log.Info(sql);
#endif
            object[] objs = this.DataProvider.CustomQuery(
                typeof(TracedKeyParts),
                new PagerCondition(sql, inclusive, exclusive, true));

            if (objs != null)
            {
                foreach (TracedKeyParts keypart in objs)
                {
                    if (0 == this.DataProvider.GetCount(
                        new SQLCondition(string.Format("select count(itemcode) from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode = '{0}'", keypart.MItemCode))))
                    {
                        keypart.CanTrace = false;
                    }
                    else
                    {
                        keypart.CanTrace = true;
                    }
                }
            }
            this.GetMItemName(objs);
            return objs;
        }

        private void GetMItemName(object[] objs)
        {
            if (objs == null) return;

            System.Text.StringBuilder mitemcodeConditionBuilder = new System.Text.StringBuilder();
            mitemcodeConditionBuilder.Append(" and OBITEMCODE in (''");
            foreach (TracedKeyParts tracedKeyPart in objs)
            {
                mitemcodeConditionBuilder.Append(string.Format(",'{0}'", tracedKeyPart.MItemCode));
            }
            mitemcodeConditionBuilder.Append(")");

            string sql = string.Format(" select tblopbomdetail.* from tblopbomdetail where 1=1 {0} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), mitemcodeConditionBuilder.ToString());

            object[] mitems = this.DataProvider.CustomQuery(typeof(OPBOMDetail), new SQLCondition(sql));
            if (mitems != null)
            {
                Hashtable mitemHT = new Hashtable();
                foreach (OPBOMDetail opbomdetail in mitems)
                {
                    if (!mitemHT.Contains(opbomdetail.OPBOMItemCode))
                    {
                        mitemHT.Add(opbomdetail.OPBOMItemCode, opbomdetail.OPBOMItemName);
                    }
                }
                foreach (TracedKeyParts tracedKeyPart in objs)
                {
                    if (mitemHT.Contains(tracedKeyPart.MItemCode))
                    {
                        tracedKeyPart.MItemName = (string)mitemHT[tracedKeyPart.MItemCode];
                    }
                }
            }

        }

        public int QueryKeyPartsInfoCount(string sn,
            string insideItemCode, string vendorCode, string vendorItemCode,
            string lotno, string dateCode, string keyparts)
        {
            //需要查询序列号转换前的rcard
            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            ArrayList array = new ArrayList();
            array.Add(sn);
            castDownHelper.GetAllRCard(ref array, sn);
            string[] rCards = (string[])array.ToArray(typeof(System.String));
            SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            /*
            string sql = string.Format(
                @"select count(mcard) from (select distinct {0} from tblonwipitem where 1=1 and mcardtype = '0' {1})",
                "mcard,mitemcode,vendorcode,vendoritemcode,lotno,datecode,version,PCBA,BIOS",SnCondition);
            */

            //modified by jessie lee, 2006-3-25
            string sql = string.Format
                (@"select count(*) from 
				(
				SELECT rcard, mcardtype, rcardseq, mcard, mocode, itemcode, mitemcode, tpcode,
					shiftcode, shifttypecode, rescode, sscode, segcode, routecode,
					transstatus, modelcode, lotno, pcba, bios, VERSION, vendoritemcode,
					vendorcode, datecode,  mseq, qty, 
					DECODE (actiontype, 1, 0, 0) as actiontype, opcode, muser,
					mdate, mtime
				FROM tblonwipitem
				WHERE 1 = 1 AND mcardtype = '0' {0}
				UNION 
				SELECT rcard, mcardtype, rcardseq, mcard, mocode, itemcode, mitemcode, tpcode,
					shiftcode, shifttypecode, rescode, sscode, segcode, routecode,
					transstatus, modelcode, lotno, pcba, bios, VERSION, vendoritemcode,
					vendorcode, datecode,  mseq, qty,
					actiontype, dropop AS opcode,
					dropuser as muser, dropdate as mdate, droptime as mtime
				FROM tblonwipitem
				WHERE 1 = 1 AND mcardtype = '0' AND actiontype = 1 {0}) ", SnCondition);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #region Real Time Quantity
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segmentCode">工段</param>
        /// <param name="stepSequenceCode">产线</param>
        /// <param name="modelCode">机种</param>
        /// <param name="itemCode">产品</param>
        /// <param name="moCode">工单</param>
        /// <param name="factorycode">工厂</param>
        /// <param name="shiftCode">班次</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public object[] QueryRealTimeQuantity(
            string segmentCode,
            string stepSequenceCode,
            string modelCode,
            string itemCode,
            string moCode,
            string factorycode,
            string shiftCode,
            int date)
        {
            bool needQtyFlag = false;

            string stepSeqCondition = "";
            if (stepSequenceCode != "" &&
                stepSequenceCode != null)
            {
                stepSeqCondition = string.Format(" and sscode in ({0})", FormatHelper.ProcessQueryValues(stepSequenceCode.ToUpper()));
            }

            string modelCondition = "";
            if (modelCode != "" &&
                modelCode != null)
            {
                //needQtyFlag = true ;
                modelCondition = string.Format(" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            string itemCondition = "";
            if (itemCode != "" &&
                itemCode != null)
            {
                //needQtyFlag = true ;
                itemCondition = string.Format(" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            string moCondition = "";
            if (moCode != "" &&
                moCode != null)
            {
                //needQtyFlag = true ;
                moCondition = string.Format(" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }
            //工厂查询条件
            if (factorycode != null && factorycode != string.Empty)
            {
                moCondition += string.Format(" and mocode in (select mocode from tblmo where factory = '{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ", factorycode);
            }

            //时间区段查询条件
            string timeRange = string.Empty;
            timeRange = FormatHelper.GetDateRangeSql("shiftday", date, date);

            string sqlStr = string.Empty;
            if (needQtyFlag)
            {
                //工单、机种、产品
                //对于工单、机种、产品，投入量会在tblrptreallineqty的eattribute1字段中CS程序累加计算
                sqlStr = string.Format(
                    @"select 
					sscode as SSCODE,
					ITEMCODE,
					tpcode as TPCODE,
					sum( decode(qtyflag,'Y',outputqty,0) ) as OUTPUTQTY,
					SUM( eattribute1 ) AS inputqty,
					SUM( scrapqty ) AS scrapqty
				  from 
					TBLRPTREALLINEQTY
				  where 
					segcode = '{0}'
					and shiftcode = '{1}'
					{2}{3}{4}{5}{6} group by SSCODE,ITEMCODE,TPCODE ",
                    segmentCode.ToUpper(),
                    shiftCode.ToUpper(),
                    timeRange,
                    stepSeqCondition,
                    modelCondition,
                    itemCondition,
                    moCondition);
            }
            else
            {
                //对于产线、工段，投入量在tblrptreallineqty的inputqty字段中由CS程序累加计算
                sqlStr = string.Format(
                    @"select 
					sscode as SSCODE,
					ITEMCODE,
					tpcode as TPCODE,
					sum(outputqty) as OUTPUTQTY," +
                    ///inputqty为中间投入量
                    ///eattribute1为工单投入量
                    @"SUM (inputqty + eattribute1) AS inputqty,
					SUM (scrapqty) AS scrapqty
				  from 
					TBLRPTREALLINEQTY
				  where 
					segcode = '{0}'
					and shiftcode = '{1}'
					{2}{3}{4}{5}{6} group by SSCODE,ITEMCODE,TPCODE ",
                    segmentCode.ToUpper(),
                    shiftCode.ToUpper(),
                    timeRange,
                    stepSeqCondition,
                    modelCondition,
                    itemCondition,
                    moCondition);
            }
#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RealTimeQuantity),
                new SQLCondition(sqlStr));
        }

        /// <summary>
        /// 实时投入产出产量
        /// </summary>
        /// <param name="segmentCode">工段</param>
        /// <param name="moCode">工单</param>
        /// <param name="shiftCode">班次</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public object[] QueryRealTimeInputOutputQuantity(
            string segmentCode,
            string moCode,
            string shiftCode,
            int date)
        {
            //string qtyFlagCondition = " 1=1 ";

            //工单查询条件
            string moCondition = "";
            if (moCode != "" &&
                moCode != null)
            {
                //qtyFlagCondition = " upper(qtyflag) = 'Y' " ;
                moCondition = string.Format(" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }

            //工段查询条件
            string segmentCondition = "";
            if (segmentCode != "" &&
                segmentCode != null)
            {
                //qtyFlagCondition = " upper(qtyflag) = 'Y' " ;
                segmentCondition = string.Format(" and segcode = '{0}'", segmentCode);
            }

            //班次查询条件
            string shiftCondition = "";
            if (shiftCode != "" &&
                shiftCode != null)
            {
                //qtyFlagCondition = " upper(qtyflag) = 'Y' " ;
                shiftCondition = string.Format(" and shiftcode = '{0}'", shiftCode);
            }

            //时间区段查询条件
            string timeRange = string.Empty;
            timeRange = FormatHelper.GetDateRangeSql("shiftday", date, date);

            string sqlStr = @"select rl.modelcode ,rl.itemcode ,rl.mocode ,rl.inputqty, rl.outputqty,tblmo.moplanqty,tblmo.moinputqty,tblmo.moactqty,tblmo.moscrapqty,tblmo.offmoqty
					from (
						SELECT   modelcode AS modelcode,itemcode AS itemcode,mocode AS mocode, SUM (eattribute1) AS inputqty, SUM (DECODE (qtyflag, 'Y', outputqty, 0)) AS outputqty
							FROM tblrptreallineqty
						WHERE 1 = 1
						{0}{1}{2}{3}
						GROUP BY modelcode, itemcode,mocode
					)  rl,tblmo
					where rl.mocode = tblmo.mocode(+)";
            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sqlStr += " and tblmo.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ")";
            }

            sqlStr = string.Format(sqlStr, moCondition, segmentCondition, shiftCondition, timeRange);
#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RealTimeInputOutputQuantity),
                new SQLCondition(sqlStr));
        }

        /// <summary>
        /// 实时投入产量明细
        /// </summary>
        /// <param name="segmentCode">工段</param>
        /// <param name="modelCode">机种</param>
        /// <param name="itemCode">产品</param>
        /// <param name="moCode">工单</param>
        /// <param name="shiftCode">班次</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public object[] QueryRealTimeInputQuantity(
            string segmentCode,
            string modelCode,
            string itemCode,
            string moCode,
            string shiftCode,
            int date)
        {
            //工段条件
            string segmentCondition = "";
            if (segmentCode != "" &&
                segmentCode != null)
            {
                segmentCondition = string.Format(" and segcode in ({0})", FormatHelper.ProcessQueryValues(segmentCode));
            }

            //班次条件
            string ShiftCondition = "";
            if (shiftCode != "" &&
                shiftCode != null)
            {
                ShiftCondition = string.Format(" and shiftcode in ({0})", FormatHelper.ProcessQueryValues(shiftCode));
            }

            //机种条件
            string modelCondition = "";
            if (modelCode != "" &&
                modelCode != null)
            {
                modelCondition = string.Format(" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            //产品条件
            string itemCondition = "";
            if (itemCode != "" &&
                itemCode != null)
            {
                itemCondition = string.Format(" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            //工单条件
            string moCondition = "";
            if (moCode != "" &&
                moCode != null)
            {
                moCondition = string.Format(" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }

            //时间区段查询条件
            string timeRange = string.Empty;
            timeRange = FormatHelper.GetDateRangeSql("shiftday", date, date);

            string sqlStr = string.Format(
                @"select 
					sscode as SSCODE,
					tpcode as TPCODE,
					sum(EATTRIBUTE1) as INPUTQTY
				  from 
					TBLRPTREALLINEQTY
				  where 1=1
				  {0}{1}{2}{3}{4}{5} group by SSCODE,TPCODE ",
                segmentCondition, ShiftCondition, modelCondition, itemCondition, moCondition, timeRange);

            return this.DataProvider.CustomQuery(
                typeof(RealTimeInputQuantity),
                new SQLCondition(sqlStr));
        }


        /// <summary>
        /// 实时产出产量明细
        /// </summary>
        /// <param name="segmentCode">工段</param>
        /// <param name="modelCode">机种</param>
        /// <param name="itemCode">产品</param>
        /// <param name="moCode">工单</param>
        /// <param name="shiftCode">班次</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public object[] QueryRealTimeOutputQuantity(
            string segmentCode,
            string modelCode,
            string itemCode,
            string moCode,
            string shiftCode,
            int date)
        {
            //工段条件
            string segmentCondition = "";
            if (segmentCode != "" &&
                segmentCode != null)
            {
                segmentCondition = string.Format(" and segcode in ({0})", FormatHelper.ProcessQueryValues(segmentCode));
            }

            //班次条件
            string ShiftCondition = "";
            if (shiftCode != "" &&
                shiftCode != null)
            {
                ShiftCondition = string.Format(" and shiftcode in ({0})", FormatHelper.ProcessQueryValues(shiftCode));
            }

            //机种条件
            string modelCondition = "";
            if (modelCode != "" &&
                modelCode != null)
            {
                modelCondition = string.Format(" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            //产品条件
            string itemCondition = "";
            if (itemCode != "" &&
                itemCode != null)
            {
                itemCondition = string.Format(" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            //工单条件
            string moCondition = "";
            if (moCode != "" &&
                moCode != null)
            {
                moCondition = string.Format(" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }

            //时间区段查询条件
            string timeRange = string.Empty;
            timeRange = FormatHelper.GetDateRangeSql("shiftday", date, date);

            string sqlStr = string.Format(
                @"select 
					sscode as SSCODE,
					tpcode as TPCODE,
					sum(OUTPUTQTY) as OUTPUTQTY
				  from 
					TBLRPTREALLINEQTY
				  where 1=1
				  {0}{1}{2}{3}{4}{5} group by SSCODE,TPCODE ",
                segmentCondition, ShiftCondition, modelCondition, itemCondition, moCondition, timeRange);

            return this.DataProvider.CustomQuery(
                typeof(RealTimeOutputQuantity),
                new SQLCondition(sqlStr));
        }

        //获取班次时间区段.
        //因为实时产量查询是按照天来查询的.如果查询的时段是跨天的,需要做出相应处理
        //Add by Simone Xu  2005/09/13
        private string GetDateRangeByShiftCode(string shiftCode, int ShiftDay)
        {
            ShiftModelFacade smFacade = new ShiftModelFacade(this.DataProvider);
            object[] returnObjs = smFacade.QueryShift(shiftCode, "", 0, System.Int32.MaxValue);
            Shift tp = (Shift)returnObjs[0];
            string timeRange = string.Format(" and shiftday = {0} ", ShiftDay);
            if (tp.IsOverDate == FormatHelper.TRUE_STRING)
            {
                //如果时段跨区,则结束时间为第二天的时间
                string BeginTimeStr = ShiftDay + (tp.ShiftBeginTime - 1).ToString().PadLeft(6, '0');
                string EndTimeStr = (ShiftDay + 1) + (tp.ShiftEndTime + 1).ToString().PadLeft(6, '0');
                timeRange = string.Format(" AND ShiftDay={2} AND (DAY*1000000 + TPBTIME) > {0} AND (DAY*1000000 + TPETIME) < {1} ", BeginTimeStr, EndTimeStr, ShiftDay);
            }
            return timeRange;
        }

        public object[] QueryRealTimeDetails(
            string segmentCode, int shiftDay, string shiftCode,
            string stepSequenceCode, string modelCode, string itemCode, string moCode, string timePeriodCode,
            bool includeMidOutput,
            int inclusive, int exclusive)
        {
            bool needQtyFlag = false;
            string modelCondition = "";
            if (modelCode != "" && modelCode != null)
            {
                needQtyFlag = true;
                modelCondition = string.Format(" and TBLRPTREALLINEQTY.modelcode in ({0} ) ", FormatHelper.ProcessQueryValues(modelCode));
            }

            string itemCondition = "";
            if (itemCode != "" && itemCode != null)
            {
                //needQtyFlag = true ;
                itemCondition = string.Format(" and TBLRPTREALLINEQTY.itemcode in ({0} ) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                needQtyFlag = true;
                moCondition = string.Format(" and TBLRPTREALLINEQTY.mocode in ({0} ) ", FormatHelper.ProcessQueryValues(moCode));
            }

            needQtyFlag = needQtyFlag == true ? needQtyFlag : includeMidOutput;

            string timeRange = string.Empty;
            timeRange = timeRange = FormatHelper.GetDateRangeSql("shiftday", shiftDay, shiftDay);

            string sqlStr = string.Empty;
            if (needQtyFlag)
            {
                ///工单、机种、产品
                string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
                if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

                sqlStr = @"select " +
                "	TBLRPTREALLINEQTY.mocode,TBLRPTREALLINEQTY.modelcode,TBLRPTREALLINEQTY.itemcode,SUM ( decode(qtyflag,'Y',outputqty,0) ) AS outputqty ,SUM (TBLRPTREALLINEQTY.eattribute1) AS inputqty,SUM (scrapqty) AS scrapqty,tblmo.momemo,tblitem.itemname " +
                "	from  " +
                "	TBLRPTREALLINEQTY  " +
                "	join tblmo on (tblmo.mocode = TBLRPTREALLINEQTY.Mocode ";

                if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
                {
                    sqlStr += " and tblmo.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ")";
                }

                sqlStr += " ) " +
                "	join tblitem on (tblitem.itemcode = TBLRPTREALLINEQTY.Itemcode and tblmo.orgid = tblitem.orgid) " +
                "	where 1=1 " + orgIDList +
                "	 and segcode = '{0}' " +
                "	 {1} " +
                "	and shiftcode = '{2}' " +
                "	and sscode = '{3}' " +
                "	and tpcode = '{4}' " +
                "	{5}{6}{7} group by TBLRPTREALLINEQTY.mocode,TBLRPTREALLINEQTY.modelcode,TBLRPTREALLINEQTY.itemcode,tblmo.momemo,tblitem.itemname";

                sqlStr = string.Format(sqlStr
                    , segmentCode, timeRange, shiftCode, stepSequenceCode, timePeriodCode,
                    modelCondition, itemCondition, moCondition);
            }
            else
            {
                ///产线、工段
                ///inputqty为中间投入量
                ///eattribute1为工单投入量
                string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
                if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

                sqlStr = @"select " +
                "	TBLRPTREALLINEQTY.mocode,TBLRPTREALLINEQTY.modelcode,TBLRPTREALLINEQTY.itemcode,SUM (outputqty) AS outputqty , " +
                "	SUM (inputqty+TBLRPTREALLINEQTY.eattribute1) AS inputqty,SUM (scrapqty) AS scrapqty,tblmo.momemo,tblitem.itemname " +
                "	from  " +
                "	TBLRPTREALLINEQTY  " +
                "	join tblmo on (tblmo.mocode = TBLRPTREALLINEQTY.Mocode ";
                if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
                {
                    sqlStr += " and tblmo.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ")";
                }

                sqlStr += " ) " +
                "	join tblitem on (tblitem.itemcode = TBLRPTREALLINEQTY.Itemcode and tblmo.orgid = tblitem.orgid) " +
                "	where 1=1 " + orgIDList +
                "	 and segcode = '{0}' " +
                "	 {1} " +
                "	and shiftcode = '{2}' " +
                "	and sscode = '{3}' " +
                "	and tpcode = '{4}' " +
                "	{5}{6}{7} group by TBLRPTREALLINEQTY.mocode,TBLRPTREALLINEQTY.modelcode,TBLRPTREALLINEQTY.itemcode,tblmo.momemo,tblitem.itemname";

                sqlStr = string.Format(sqlStr
                    , segmentCode, timeRange, shiftCode, stepSequenceCode, timePeriodCode,
                    modelCondition, itemCondition, moCondition);
            }
#if DEBUG
            Log.Info(sqlStr);
#endif
            return this.DataProvider.CustomQuery(
                typeof(RealTimeDetails),
                new PagerCondition(sqlStr, "TBLRPTREALLINEQTY.mocode,TBLRPTREALLINEQTY.modelcode,TBLRPTREALLINEQTY.itemcode", inclusive, exclusive));
        }

        public int QueryRealTimeDetailsCount(
            string segmentCode, int shiftDay, string shiftCode,
            string stepSequenceCode, string modelCode, string itemCode, string moCode, string timePeriodCode, bool includeMidOutput)
        {
            string modelCondition = "";
            if (modelCode != "" && modelCode != null)
            {
                modelCondition = string.Format(" and TBLRPTREALLINEQTY.modelcode in ({0} ) ", FormatHelper.ProcessQueryValues(modelCode));
            }

            string itemCondition = "";
            if (itemCode != "" && itemCode != null)
            {
                itemCondition = string.Format(" and TBLRPTREALLINEQTY.itemcode in ({0} ) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition = string.Format(" and TBLRPTREALLINEQTY.mocode in ({0} ) ", FormatHelper.ProcessQueryValues(moCode));
            }

            //时间区段查询条件
            string timeRange = string.Empty;
            timeRange = timeRange = FormatHelper.GetDateRangeSql("shiftday", shiftDay, shiftDay);
            string sqlStr = string.Format(
                @"select count(mocode) from (
					select 
					mocode,modelcode,itemcode,sum(OUTPUTQTY) as OUTPUTQTY
					from 
					TBLRPTREALLINEQTY 
					where
						segcode = '{0}'
					 {1}
					and shiftcode = '{2}'
					and sscode = '{3}'
					and tpcode = '{4}'
					{5}{6}{7} group by mocode,modelcode,itemcode)", segmentCode, timeRange, shiftCode, stepSequenceCode, timePeriodCode,
                modelCondition, itemCondition, moCondition);
#if DEBUG
            Log.Info(sqlStr);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(sqlStr));
        }
        #endregion

        #region Real Time Yield Percent
        /// <summary>
        /// by factory
        /// </summary>
        /// <param name="segmentCode"></param>
        /// <param name="stepSequenceCode"></param>
        /// <param name="modelCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="moCode"></param>
        /// <param name="factorycode"></param>
        /// <param name="shiftCode"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public object[] QueryRealTimeYieldPercent(
            string segmentCode,
            string stepSequenceCode,
            string modelCode,
            string itemCode,
            string moCode,
            string factorycode,
            string shiftCode,
            int date)
        {
            string qtyFlagCondition = string.Empty;

            string stepSeqCondition = "";
            if (stepSequenceCode != "" &&
                stepSequenceCode != null)
            {
                stepSeqCondition = string.Format(" and sscode in ({0})", FormatHelper.ProcessQueryValues(stepSequenceCode.ToUpper()));
            }

            string modelCondition = "";
            if (modelCode != "" &&
                modelCode != null)
            {
                qtyFlagCondition = " qtyflag = 'Y' and ";
                modelCondition = string.Format(" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode.ToUpper()));
            }
            //工厂查询条件
            if (factorycode != null && factorycode != string.Empty)
            {
                modelCondition += string.Format(" and mocode in (select mocode from tblmo where factory = '{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ", factorycode);
            }

            string itemCondition = "";
            if (itemCode != "" &&
                itemCode != null)
            {
                qtyFlagCondition = " qtyflag = 'Y' and ";
                itemCondition = string.Format(" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCode.ToUpper()));
            }

            string moCondition = "";
            if (moCode != "" &&
                moCode != null)
            {
                qtyFlagCondition = " qtyflag = 'Y' and ";
                moCondition = string.Format(" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCode.ToUpper()));
            }

            string sqlStr = string.Format(
                @"select 
					sscode as SSCODE,
					sum(MOALLGOODQTY) as ALLGOODQTY,
					sum(OUTPUTQTY) as OUTPUTQTY,
					decode(sum(OUTPUTQTY),0,{7},sum(MOALLGOODQTY)/sum(OUTPUTQTY)) as AllGoodYieldPercent 
				  from 
					TBLRPTREALLINEQTY
				  where {8}
					segcode = '{0}'
					and shiftcode = '{1}'
					and shiftday = {2}
					{3}{4}{5}{6} group by sscode",
                segmentCode.ToUpper(),
                shiftCode.ToUpper(),
                date,
                stepSeqCondition,
                modelCondition,
                itemCondition,
                moCondition, Denominator_Replacer,
                qtyFlagCondition);
#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RealTimeYieldPercent),
                new SQLCondition(sqlStr));
        }


        public object[] QuerySegmentRealTimeYieldPercent(
            string segmentCode,
            string modelCode,
            string itemCode,
            string moCode,
            string shiftCode,
            int date)
        {
            string qtyFlagCondition = string.Empty;

            string modelCondition = "";
            if (modelCode != "" &&
                modelCode != null)
            {
                qtyFlagCondition = " qtyflag = 'Y' and ";
                modelCondition = string.Format(" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            string itemCondition = "";
            if (itemCode != "" &&
                itemCode != null)
            {
                qtyFlagCondition = " qtyflag = 'Y' and ";
                modelCondition = string.Format(" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            string moCondition = "";
            if (moCode != "" &&
                moCode != null)
            {
                qtyFlagCondition = " qtyflag = 'Y' and ";
                moCondition = string.Format(" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }

            string sqlStr = string.Format(
                @"select 
					segcode as SegCode,
					sum(MOALLGOODQTY) as ALLGOODQTY,
					sum(OUTPUTQTY) as OUTPUTQTY,
					decode(sum(OUTPUTQTY),0,{6},sum(MOALLGOODQTY)/sum(OUTPUTQTY)) as AllGoodYieldPercent 
				  from 
					TBLRPTREALLINEQTY
				  where {7}
					segcode = '{0}'
					and shiftcode = '{1}'
					and shiftday = {2}
					{3}{4}{5} group by segcode",
                segmentCode.ToUpper(),
                shiftCode.ToUpper(),
                date,
                modelCondition,
                itemCondition,
                moCondition, Denominator_Replacer,
                qtyFlagCondition);
#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RealTimeYieldPercent),
                new SQLCondition(sqlStr));
        }

        #endregion

        #region Real Time Defect
        public object[] QueryRealTimeDefect(
            string segmentCode,
            string stepSequenceCode,
            string modelCode,
            string itemCode,
            string moCode,
            string shiftCode,
            int date,
            int top)
        {
            string stepSeqCondition = "";
            if (stepSequenceCode != "" &&
                stepSequenceCode != null)
            {
                stepSeqCondition = string.Format(" and sscode in ({0})", FormatHelper.ProcessQueryValues(stepSequenceCode.ToUpper()));
            }

            string modelCondition = "";
            if (modelCode != "" &&
                modelCode != null)
            {
                modelCondition = string.Format(" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            string itemCondition = "";
            if (itemCode != "" &&
                itemCode != null)
            {
                itemCondition = string.Format(" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            string moCondition = "";
            if (moCode != "" &&
                moCode != null)
            {
                moCondition = string.Format(" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }
            ArrayList defectList = new ArrayList();

            //filter step sequence
            object[] stepSeqs = this.DataProvider.CustomQuery(
                typeof(StepSequence),
                new SQLCondition(string.Format(
                @"select distinct sscode from TBLRPTREALLINEECQTY where segcode = '{0}' {1}", segmentCode, stepSeqCondition)));
            if (stepSeqs != null)
            {
                foreach (StepSequence ss in stepSeqs)
                {
                    string sqlStr = string.Format(
                        @"select SSCODE,ERRORCODE,ERRORCODEGROUP,DEFECTDQTY from (
				select 
					SSCODE as SSCODE,
					ECODE as ERRORCODE,
					ECGCODE as ERRORCODEGROUP,
					sum(ECTIMES ) as DEFECTDQTY
				from 
					TBLRPTREALLINEECQTY
				where
					segcode = '{0}'
				and sscode = '{6}'
				and SHIFTDAY = {1}
				and SHIFTCODE = '{2}'				
				{3}{4}{5}
				group by sscode,ECODE,ECGCODE order by DEFECTDQTY desc)  where rownum<={7} ",
                        segmentCode.ToUpper(),
                        date,
                        shiftCode.ToUpper(),
                        modelCondition,
                        itemCondition,
                        moCondition, ss.StepSequenceCode, top.ToString());
#if DEBUG
                    Log.Info(sqlStr);
#endif
                    object[] defects = this.DataProvider.CustomQuery
                        (typeof(RealTimeDefect),
                        new SQLCondition(sqlStr));
                    if (defects != null)
                    {
                        foreach (RealTimeDefect defect in defects)
                        {
                            defectList.Add(defect);
                        }
                    }
                }
            }
            return (RealTimeDefect[])defectList.ToArray(typeof(RealTimeDefect));
        }

        //by factory
        public object[] QueryRealTimeDefect(
            string segmentCode,
            string stepSequenceCode,
            string modelCode,
            string itemCode,
            string moCode,
            string factorycode,
            string shiftCode,
            int date,
            int top)
        {
            string inputSum = "sum(b.eattribute1 + b.inputqty)";
            string stepSeqCondition = "";
            if (stepSequenceCode != "" &&
                stepSequenceCode != null)
            {
                stepSeqCondition = string.Format(" and sscode in ({0})", FormatHelper.ProcessQueryValues(stepSequenceCode.ToUpper()));
            }

            string modelCondition = "";
            if (modelCode != "" &&
                modelCode != null)
            {
                inputSum = "sum( b.eattribute1 )";
                modelCondition = string.Format(" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            string itemCondition = "";
            if (itemCode != "" &&
                itemCode != null)
            {
                inputSum = "sum( b.eattribute1 )";
                itemCondition = string.Format(" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            string moCondition = "";
            if (moCode != "" &&
                moCode != null)
            {
                inputSum = "sum( b.eattribute1 )";
                moCondition = string.Format(" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }
            //工厂查询条件
            if (factorycode != null && factorycode != string.Empty)
            {
                moCondition += string.Format(" and mocode in (select mocode from tblmo where factory = '{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ", factorycode);
            }
            ArrayList defectList = new ArrayList();

            //filter step sequence
            object[] stepSeqs = this.DataProvider.CustomQuery(
                typeof(StepSequence),
                new SQLCondition(string.Format(
                @"select distinct sscode from TBLRPTREALLINEECQTY where segcode = '{0}' {1}", segmentCode, stepSeqCondition)));

            if (stepSeqs != null)
            {
                foreach (StepSequence ss in stepSeqs)
                {
                    //Simone sql
                    /* modified by jessie lee, 2005/12/19,
                     * 请在实时缺陷分析界面上增加缺陷不良率（计算公式：缺陷数量/投入总数＝不良率） */
                    string sqlStr = string.Format(
                        @"select itemcode, sscode, errorcode, errorcodegroup, defectqty,ECGDESC,ECDESC,INPUTQTY from 
					(
					SELECT a.itemcode, a.sscode, a.errorcode, a.errorcodegroup, a.defectqty, a.ECGDESC, a.ECDESC,{8} as INPUTQTY
						FROM (SELECT tblrptreallineecqty.itemcode AS itemcode  , tblrptreallineecqty.sscode AS sscode, tblrptreallineecqty.ecode AS errorcode,
                 tblrptreallineecqty.ecgcode AS errorcodegroup, SUM (ectimes) AS defectqty,
                 TBLECG.ECGDESC ,TBLEC.ECDESC
				 FROM tblrptreallineecqty,TBLECG,TBLEC
				 WHERE
                 tblrptreallineecqty.ecgcode = TBLECG.ecgcode(+)
                 and           
                 tblrptreallineecqty.ecode = TBLEC.ecode(+)
                 AND 
					segcode = '{0}'
				and sscode = '{6}'
				and SHIFTDAY = {1}
				and SHIFTCODE = '{2}'				
				{3}{4}{5}
				GROUP BY tblrptreallineecqty.sscode,tblrptreallineecqty.itemcode, tblrptreallineecqty.ecode, tblrptreallineecqty.ecgcode,TBLECG.ECGDESC ,TBLEC.ECDESC
				) a
				join TBLRPTREALLINEQTY b
				on a.sscode =  b.sscode
				AND b.segcode = '{0}'
				and b.SHIFTDAY = {1}
				and b.SHIFTCODE = '{2}'
				and b.itemcode = a.itemcode {9}{10}{11}
				group by  a.sscode, a.itemcode,a.defectqty, a.errorcode, a.errorcodegroup,a.ECGDESC, a.ECDESC
				order by a.defectqty desc )
					  where rownum<={7} ",
                        segmentCode.ToUpper(),
                        date,
                        shiftCode.ToUpper(),
                        modelCondition,
                        itemCondition,
                        moCondition,
                        ss.StepSequenceCode,
                        top.ToString(),
                        inputSum,
                        modelCondition.Replace("modelcode", "b.modelcode"),
                        itemCondition.Replace("itemcode", "b.itemcode"),
                        moCondition.Replace("mocode", "b.mocode"));

#if DEBUG
                    Log.Info(sqlStr);
#endif
                    object[] defects = this.DataProvider.CustomQuery
                        (typeof(RealTimeDefect),
                        new SQLCondition(sqlStr));
                    if (defects != null)
                    {
                        foreach (RealTimeDefect defect in defects)
                        {
                            defectList.Add(defect);
                        }
                    }
                }
            }
            return (RealTimeDefect[])defectList.ToArray(typeof(RealTimeDefect));
        }
        #endregion

        #region Real Time Rescode2EC
        public object[] QueryRescode2EC(
            string resCodes,
            int date,
            string shiftCode,
            string ssCodes,
            string modelCodes,
            string itemCodes,
            string moCodes)
        {
            string condition = string.Empty;

            if (resCodes != null && resCodes.Length > 0)
            {
                condition += string.Format(" and rescode in ({0})  ", FormatHelper.ProcessQueryValues(resCodes));
            }

            condition += string.Format(" and shiftday = {0} ", date);

            if (shiftCode != null && shiftCode.Length > 0)
            {
                condition += string.Format(" and shiftcode = '{0}'  ", shiftCode);
            }

            if (ssCodes != null && ssCodes.Length > 0)
            {
                condition += string.Format(" and sscode in ({0})  ", FormatHelper.ProcessQueryValues(ssCodes));
            }

            if (modelCodes != null && modelCodes.Length > 0)
            {
                condition += string.Format(" and modelcode in ({0})  ", FormatHelper.ProcessQueryValues(modelCodes));
            }

            if (itemCodes != null && itemCodes.Length > 0)
            {
                condition += string.Format(" and itemcode in ({0})  ", FormatHelper.ProcessQueryValues(itemCodes));
            }

            if (moCodes != null && moCodes.Length > 0)
            {
                condition += string.Format(" and mocode in ({0})  ", FormatHelper.ProcessQueryValues(moCodes));
            }

            string sql = string.Format(@" select ecgcode, eccode, TPCode, TPBTime, TPETime, sum(ngtimes) as ngtimes
				from tblrptresecg
				where 1=1 {0}
				group by ecgcode, eccode, TPCode, TPBTime, TPETime ", condition);

            object[] objs = this.DataProvider.CustomQuery(typeof(QDORes2EC), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                TSModel.TSModelFacade facade = new BenQGuru.eMES.TSModel.TSModelFacade(this.DataProvider);
                for (int i = 0; i < objs.Length; i++)
                {
                    object ec = facade.GetErrorCode((objs[i] as QDORes2EC).ErrorCode);
                    (objs[i] as QDORes2EC).ErrorCodeDesc = (ec != null) ? (ec as ErrorCodeA).ErrorDescription : (objs[i] as QDORes2EC).ErrorCode;
                }
            }

            return objs;
        }

        #endregion

        #region Time Quantity
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepSequenceCode">产线</param>
        /// <param name="startDate">日期</param>
        /// <param name="startTime">时间</param>
        /// <returns></returns>
        public object[] QueryTimeQuantity(
            string stepSequenceCode,
            long startDate,
            long startTime)
        {
            string sqlStr = string.Empty;

            sqlStr = " select SSCODE,ITEMCODE,SUM(OUTPUTQTY) as OUTPUTQTY from TBLRPTREALLINEQTY where 1=1 ";
            //日期、时间都有：查该时间段记录
            if (startDate > 0 && startTime > 0)
            {
                long dateTime = startDate * 1000000 + startTime;
                sqlStr += " and (day*1000000 + tpbtime) <= " + dateTime + " and (day*1000000 + tpetime) >= " + dateTime;
            }
            //只有日期：查当天所有记录
            else if (startDate > 0)
            {
                sqlStr += " and day = " + startDate;
            }
            //只有时间：查所有日期这个时间段的记录
            else if (startTime > 0)
            {
                sqlStr += " and tpbtime <= " + startTime + " and tpetime >= " + startTime;
            }

            if (stepSequenceCode != "" &&
                stepSequenceCode != null)
            {
                sqlStr += " and sscode in (" + FormatHelper.ProcessQueryValues(stepSequenceCode.ToUpper()) + ") ";
            }
            sqlStr += " group by SSCODE,ITEMCODE ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RealTimeQuantity),
                new SQLCondition(sqlStr));
        }

        #endregion

        #region Time Quantity Sum
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stepSequenceCode">产线</param>
        /// <param name="rescode">资源</param>
        /// <param name="startDate">日期</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public object[] QueryTimeQuantitySum(
            string stepSequenceCode,
            string resourceCode,
            int startDate,
            int endDate,
            int startTime,
            int endTime)
        {
            string sqlStr = string.Empty;
            int days = endDate - startDate;
            int sday = startDate + 1;
            int tday = startDate + 2;

            string sscodeCondition = " and sscode in (" + FormatHelper.ProcessQueryValues(stepSequenceCode.ToUpper()) + ") ";
            string rescodeCondition = " and rescode in (" + FormatHelper.ProcessQueryValues(resourceCode.ToUpper()) + ") ";
            if (days == 0)
            {
                sqlStr += " select Sscode,Rescode,sum(decode(actionresult,'GOOD',1,0))-sum(decode(actionresult,'NG',1,0)) as ActionResult " +
                    " from Tblonwip where Action != 'GOMO' ";
                if (stepSequenceCode != "" && stepSequenceCode != null)
                {
                    sqlStr += sscodeCondition;
                }
                if (resourceCode != "" && resourceCode != null)
                {
                    sqlStr += rescodeCondition;
                }
                sqlStr += " and MDate =" + startDate + " and MTime >" + startTime + " and MTime <" + endTime;
                sqlStr += " group by SSCODE,Rescode ";
            }
            else
            {
                sqlStr = "select Sscode, Rescode, sum(actionresult) as actionresult from " +
                    " (select Sscode,Rescode,sum(decode(actionresult,'GOOD',1,0))-sum(decode(actionresult,'NG',1,0)) as ActionResult " +
                    " from Tblonwip where Action != 'GOMO' ";
                if (stepSequenceCode != "" && stepSequenceCode != null)
                {
                    sqlStr += sscodeCondition;
                }
                if (resourceCode != "" && resourceCode != null)
                {
                    sqlStr += rescodeCondition;
                }
                sqlStr += " and MDate =" + startDate + " and MTime >" + startTime +
                    " group by SSCODE,Rescode " +
                    " union all " +
                    " select Sscode,Rescode,sum(decode(actionresult,'GOOD',1,0))-sum(decode(actionresult,'NG',1,0)) as ActionResult " +
                    " from Tblonwip where Action != 'GOMO' ";
                if (stepSequenceCode != "" && stepSequenceCode != null)
                {
                    sqlStr += sscodeCondition;
                }
                if (resourceCode != "" && resourceCode != null)
                {
                    sqlStr += rescodeCondition;
                }
                sqlStr += " and MDate =" + endDate + " and MTime <" + endTime +
                    " group by SSCODE,Rescode ";

                if (days >= 2)
                {
                    sqlStr += " union all " +
                        " select Sscode,Rescode,sum(decode(actionresult,'GOOD',1,0))-sum(decode(actionresult,'NG',1,0)) as ActionResult " +
                        " from Tblonwip where Action != 'GOMO' ";
                    if (stepSequenceCode != "" && stepSequenceCode != null)
                    {
                        sqlStr += sscodeCondition;
                    }
                    if (resourceCode != "" && resourceCode != null)
                    {
                        sqlStr += rescodeCondition;
                    }
                    sqlStr += " and MDate =" + sday +
                        " group by SSCODE,Rescode ";
                    if (days == 3)
                    {
                        sqlStr += " union all " +
                            " select Sscode,Rescode,sum(decode(actionresult,'GOOD',1,0))-sum(decode(actionresult,'NG',1,0)) as ActionResult " +
                            " from Tblonwip where Action != 'GOMO' ";
                        if (stepSequenceCode != "" && stepSequenceCode != null)
                        {
                            sqlStr += sscodeCondition;
                        }
                        if (resourceCode != "" && resourceCode != null)
                        {
                            sqlStr += rescodeCondition;
                        }
                        sqlStr += " and MDate =" + tday +
                            " group by SSCODE,Rescode ";
                    }
                }

                sqlStr += " )group by SSCODE,Rescode ";
            }

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(TimeQuantitySum),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterQuantity
        /// <summary>
        /// 报表中心 产量报表
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <returns></returns>
        public object[] QueryRPTCenterQuantity(int today)
        {
            string sqlStr = string.Empty;

            //			sqlStr = "select segcode,sum(decode(shiftday,'" + today + "',OUTPUTQTY,0)) as DayQuantity,"+
            //				"sum(decode(week,to_char(to_date('" + today + "','yyyyMMdd'),'ww'),OUTPUTQTY,0)) as WeekQuantity,"+
            //				"sum(OUTPUTQTY) as MonthQuantity "+
            //				"from TBLRPTREALLINEQTY where substr(shiftday,1,6) = substr('" + today + "',1,6) group by segcode ";

            //melo zheng 修改于 2007.1.4
            sqlStr = "select segcode,sum(decode(shiftday,'" + today + "',OUTPUTQTY,0)) as DayQuantity," +
                "sum(decode(week,to_number(to_char(to_date('" + today + "','yyyyMMdd'),'ww')),OUTPUTQTY,0)) as WeekQuantity," +
                "sum(OUTPUTQTY) as MonthQuantity " +
                "from TBLRPTREALLINEQTY where substr(shiftday,1,6) = substr('" + today + "',1,6) group by segcode ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterQuantity),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterYield
        /// <summary>
        /// 报表中心 工序良率
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <returns></returns>
        public object[] QueryRPTCenterYield(int today)
        {
            string sqlStr = string.Empty;

            //			sqlStr = "select opcode,1-Decode(sum(decode(shiftday,'" + today + "',EATTRIBUTE2,0)),0, -1,null,-1,sum(decode(shiftday,'" + today +
            //				"',ngtimes,0))/sum(decode(shiftday,'" + today + "',EATTRIBUTE2,0))) as DayPercent,"+
            //				"1-Decode(sum(decode(week,to_char(to_date('" + today + 
            //				"','yyyyMMdd'),'ww'),EATTRIBUTE2,0)),0,-1,null,-1,sum(decode(week,to_char(to_date('" + today +
            //				"','yyyyMMdd'),'ww'),ngtimes,0))/sum(decode(week,to_char(to_date('" + today + "','yyyyMMdd'),'ww'),EATTRIBUTE2,0))) as WeekPercent,"+
            //				"1-Decode(sum(EATTRIBUTE2),0,-1,null,-1,sum(ngtimes)/sum(EATTRIBUTE2)) as MonthPercent "+
            //				"from TBLRPTHISOPQTY where substr(shiftday,1,6) = substr('" + today + "',1,6) group by opcode ";

            //melo zheng 修改于 2007.1.4
            sqlStr = "select opcode,1-Decode(sum(decode(shiftday,'" + today + "',EATTRIBUTE2,0)),0, -1,null,-1,sum(decode(shiftday,'" + today +
                "',ngtimes,0))/sum(decode(shiftday,'" + today + "',EATTRIBUTE2,0))) as DayPercent," +
                "1-Decode(sum(decode(week,to_number(to_char(to_date('" + today +
                "','yyyyMMdd'),'ww')),EATTRIBUTE2,0)),0,-1,null,-1,sum(decode(week,to_number(to_char(to_date('" + today +
                "','yyyyMMdd'),'ww')),ngtimes,0))/sum(decode(week,to_number(to_char(to_date('" + today + "','yyyyMMdd'),'ww')),EATTRIBUTE2,0))) as WeekPercent," +
                "1-Decode(sum(EATTRIBUTE2),0,-1,null,-1,sum(ngtimes)/sum(EATTRIBUTE2)) as MonthPercent " +
                "from TBLRPTHISOPQTY where substr(shiftday,1,6) = substr('" + today + "',1,6) group by opcode ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterYield),
                new SQLCondition(sqlStr));
        }

        //melo zheng,2007.1.5,去掉Burn In/OQC包装站/独立的序列号转换站
        public object[] QueryRPTCenterYieldDay(int today)
        {
            string sqlStr = string.Empty;

            sqlStr = "select opcode, itemcode, mocode, shiftday, ngtimes, EATTRIBUTE2 " +
                "from TBLRPTHISOPQTY where shiftday = '" + today + "' order by opcode, itemcode, mocode, shiftday ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterYield),
                new SQLCondition(sqlStr));
        }
        public object[] QueryRPTCenterYieldWeek(int today)
        {
            string sqlStr = string.Empty;

            sqlStr = "select opcode, itemcode, mocode, shiftday, ngtimes, EATTRIBUTE2 " +
                "from TBLRPTHISOPQTY where to_number(to_char(to_date('" + today + "','yyyyMMdd'),'ww')) = week " +
                "and substr(shiftday,1,4) = substr('" + today + "',1,4) " +
                "order by opcode, itemcode, mocode, shiftday ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterYield),
                new SQLCondition(sqlStr));
        }
        public object[] QueryRPTCenterYieldMonth(int today)
        {
            string sqlStr = string.Empty;

            sqlStr = "select opcode, itemcode, mocode, shiftday, ngtimes, EATTRIBUTE2 " +
                "from TBLRPTHISOPQTY where substr(shiftday,1,6) = substr('" + today + "',1,6) " +
                "order by opcode, itemcode, mocode, shiftday ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterYield),
                new SQLCondition(sqlStr));
        }
        //取当月所有MORoute
        public object[] QueryMO2RouteByMOCode(int today)
        {
            string sqlStr = string.Empty;

            sqlStr = "select DISTINCT(a.mocode),b.routecode from TBLRPTHISOPQTY a,tblmo2route b " +
                "where a.mocode=b.mocode and substr(a.shiftday,1,6) = substr('" + today + "',1,6) ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(MO2Route),
                new SQLCondition(sqlStr));
        }
        //取出当月所有OP
        public object[] QueryOP(int today)
        {
            string sqlStr = string.Empty;

            sqlStr = "select distinct b.opControl,b.ItemCode,c.routeCode,b.OpCode " +
                "from TBLRPTHISOPQTY a,tblitemroute2op b,tblmo2route c " +
                "where a.ItemCode = b.ItemCode and c.routecode = b.routecode and a.OpCode = b.OpCode " +
                "and substr(a.shiftday,1,6) = substr('" + today + "',1,6) ";
            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sqlStr += " and b.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ") ";
            }

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(ItemRoute2OP),
                new SQLCondition(sqlStr));
        }
        public object GetItemRoute2Operation(string itemCode, string routeCode, string opCode)
        {
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op where itemcode=$itemcode and routecode=$routecode and opcode =$opcode" + GlobalVariables.CurrentOrganizations.GetSQLCondition();
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLParamCondition(selectSql, new SQLParameter[] { new SQLParameter("itemcode",typeof(string),itemCode),new SQLParameter("routecode",typeof(string),routeCode),
																																	  new SQLParameter("opcode",typeof(string), opCode)}));
            if (objs != null)
            {
                return objs[0];
            }
            return null;
        }
        #endregion

        #region RPTCenterLRR
        /// <summary>
        /// 报表中心 LRR报表
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <returns></returns>
        public object[] QueryRPTCenterLRR(int today)
        {
            string sqlStr = string.Empty;

            sqlStr = "Select d.segcode as Segcode," +
                "SUM(decode(mdate,'" + today + "',decode(lotstatus,'" + OQCLotStatus.OQCLotStatus_Reject + "',1,0),0))/" +
                "decode((select COUNT(lotno) from tbllot where mdate='" + today + "' and lotstatus IN " +
                "('" + OQCLotStatus.OQCLotStatus_Pass + "', '" + OQCLotStatus.OQCLotStatus_Reject + "')),0,null," +
                "(select COUNT(lotno) from tbllot where mdate='" + today + "' and lotstatus IN " +
                "('" + OQCLotStatus.OQCLotStatus_Pass + "', '" + OQCLotStatus.OQCLotStatus_Reject + "'))) AS DayLRR," +
                "SUM(decode(to_char(to_date(mdate,'yyyyMMdd'),'ww'),to_char(to_date('" + today + "','yyyyMMdd'),'ww')," +
                "decode(lotstatus,'" + OQCLotStatus.OQCLotStatus_Reject + "',1,0),0))/" +
                "decode((select COUNT(lotno) from tbllot " +
                "where to_char(to_date(mdate,'yyyyMMdd'),'ww')=to_char(to_date('" + today + "','yyyyMMdd'),'ww') " +
                "and substr(mdate,1,4) = substr('" + today + "',1,4) and lotstatus IN ('" + OQCLotStatus.OQCLotStatus_Pass +
                "', '" + OQCLotStatus.OQCLotStatus_Reject + "')),0,null," +
                "(select COUNT(lotno) from tbllot where to_char(to_date(mdate,'yyyyMMdd'),'ww')=to_char(to_date('" + today + "','yyyyMMdd'),'ww') " +
                "and substr(mdate,1,4) = substr('" + today + "',1,4) and lotstatus " +
                "IN ('" + OQCLotStatus.OQCLotStatus_Pass + "', '" + OQCLotStatus.OQCLotStatus_Reject + "'))) AS WeekLRR," +
                "SUM(decode(lotstatus,'" + OQCLotStatus.OQCLotStatus_Reject + "',1,0))/COUNT(tbllot.lotno) AS MonthLRR " +
                "FROM (SELECT tbllot.* FROM tbllot WHERE tbllot.lotstatus IN ('" + OQCLotStatus.OQCLotStatus_Pass +
                "', '" + OQCLotStatus.OQCLotStatus_Reject + "') " +
                "and oqclottype='" + OQCLotType.OQCLotType_Normal + "') tbllot,(SELECT DISTINCT lotno, lotseq, modelcode,segcode FROM tbllot2card " +
                "WHERE substr(mdate,1,6) = substr('" + today + "',1,6)) d WHERE tbllot.lotno = d.lotno AND tbllot.lotseq = d.lotseq " +
                "AND substr(mdate,1,6) = substr('" + today + "',1,6) " +
                "GROUP BY d.segcode";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterLRR),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterTPT
        /// <summary>
        /// 报表中心 工单TPT报表
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <returns></returns>
        public object[] QueryRPTCenterTPT(int today)
        {
            string sqlStr = string.Empty;

            sqlStr = "select mocode as Mo_MOCode,itemcode as Mo_ItemCode,moactstartdate as Mo_StartDate," +
                "moplanenddate as Mo_PlanEndDate,moactenddate as Mo_EndDate," +
                "Decode(MOSTATUS,'" + MOManufactureStatus.MOSTATUS_CLOSE + "'," + "to_date(Decode(Length(moactenddate),8,moactenddate,null),'yyyyMMdd')-" +
                "to_date(Decode(Length(moactstartdate),8,moactstartdate,null),'yyyyMMdd')," +
                "to_date('" + today + "','yyyyMMdd')-to_date(Decode(Length(moactstartdate),8,moactstartdate,null),'yyyyMMdd')) as Mo_DateNum," +
                "Decode(MOSTATUS,'" + MOManufactureStatus.MOSTATUS_CLOSE + "'," +
                "to_date(Decode(Length(moactenddate),8,moactenddate,null),'yyyyMMdd')-" +
                "to_date(Decode(Length(moplanenddate),8,moplanenddate,null),'yyyyMMdd')," +
                "to_date('" + today + "','yyyyMMdd')-to_date(Decode(Length(moplanenddate),8,moplanenddate,null),'yyyyMMdd')) as Mo_OverDateNum," +
                "Decode(MOSTATUS,'" + MOManufactureStatus.MOSTATUS_INITIAL + "','初始化','" + MOManufactureStatus.MOSTATUS_CLOSE +
                "','强制完工','" + MOManufactureStatus.MOSTATUS_OPEN + "','生产中'," + "'" + MOManufactureStatus.MOSTATUS_PENDING +
                "','暂停中','" + MOManufactureStatus.MOSTATUS_RELEASE + "','已下发') as Mo_Estate " +
                "from TBLMO where rownum <= 20 " +
                "and (to_date(Decode(Length(moactstartdate),8,moactstartdate,null),'yyyyMMdd'))>to_date('" + today + "','yyyyMMdd')-60 " +
                "and mostatus in ('mostatus_open','mostatus_pending') " + GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                "order by Mo_OverDateNum desc ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterTPT),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterLong
        /// <summary>
        /// 报表中心 维修长尾巴报表 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <returns></returns>
        public object[] QueryRPTCenterLong(int today)
        {
            string sqlStr = string.Empty;

            sqlStr = "select * from(select * from (select RCARD as Ts_SN,confirmdate as Ts_ConfirmDate," +
                "decode(Length(confirmdate),8,(to_date('" + today + "','yyyyMMdd')- to_date(confirmdate,'yyyyMMdd')),null) as Ts_Days " +
                "from tblts where TSSTATUS in ('" + TSStatus.TSStatus_Confirm + "','" + TSStatus.TSStatus_TS + "')) " +
                "where Ts_Days>=0 order by Ts_Days desc,Ts_SN) " +
                "where rownum <= 20";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterLong),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterLine
        /// <summary>
        /// 报表中心 产线
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="segcode">工段</param>
        /// <returns></returns>
        public object[] QueryRPTCenterLine(int today, string segcode)
        {
            string sqlStr = string.Empty;
            //			sqlStr = "select sscode as SSCODE,sum(decode(shiftday,'" + today + "',OUTPUTQTY,0)) as DayQuantity,"+
            //				"sum(decode(week,to_char(to_date('" + today + "','yyyyMMdd'),'ww'),OUTPUTQTY,0)) as WeekQuantity,"+
            //				"sum(OUTPUTQTY) as MonthQuantity "+
            //				"from TBLRPTREALLINEQTY where substr(shiftday,1,6) = substr('" + today + "',1,6) and segcode ='"+segcode+"' "+
            //				"group by sscode ";

            //melo zheng 修改于 2007.1.4
            sqlStr = "select sscode as SSCODE,sum(decode(shiftday,'" + today + "',OUTPUTQTY,0)) as DayQuantity," +
                "sum(decode(week,to_number(to_char(to_date('" + today + "','yyyyMMdd'),'ww')),OUTPUTQTY,0)) as WeekQuantity," +
                "sum(OUTPUTQTY) as MonthQuantity " +
                "from TBLRPTREALLINEQTY where substr(shiftday,1,6) = substr('" + today + "',1,6) and segcode ='" + segcode + "' " +
                "group by sscode ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterLine),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterWeekQuantity
        /// <summary>
        /// 报表中心 本周产量趋势
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="segcode">工段</param>
        /// <returns></returns>
        public object[] QueryRPTCenterWeekQuantity(int today, string segcode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select segcode,shiftday,sum(OUTPUTQTY) as DayQuantity " +
                "from TBLRPTREALLINEQTY " +
                "where to_char(to_date(shiftday,'yyyyMMdd'),'ww') = to_char(to_date('" + today + "','yyyyMMdd'),'ww') " +
                "and substr(shiftday,1,4) = substr('" + today + "',1,4) and segcode = '" + segcode + "' " +
                "group by shiftday,segcode ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterWeekQuantity),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterMonthQuantity
        /// <summary>
        /// 报表中心 本月产量趋势
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="segcode">工段</param>
        /// <returns></returns>
        public object[] QueryRPTCenterMonthQuantity(int today, string segcode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select segcode,shiftday,sum(OUTPUTQTY) as DayQuantity " +
                "from TBLRPTREALLINEQTY " +
                "where substr(shiftday,1,6) = substr('" + today + "',1,6) and segcode = '" + segcode + "' " +
                "group by shiftday,segcode ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterMonthQuantity),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterMocode
        /// <summary>
        /// 报表中心 产线工单 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="segcode">工段</param>
        /// <param name="stepSequenceCode">产线</param>
        /// <returns></returns>
        public object[] QueryRPTCenterMocode(int today, string segcode, string stepSequenceCode)
        {
            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND c.orgid IN(" + orgIDList + ") ";

            string sqlStr = string.Empty;

            sqlStr = "select a.sscode as SSCODE,a.mocode as MoCode,a.itemcode as ItemCode,c.ITEMNAME as ItemName," +
                "sum(a.OUTPUTQTY) as DayQuantity,sum(b.MOPLANQTY) as PlanQTY,sum(b.MOACTQTY) as ActQTY " +
                "from TBLRPTREALLINEQTY a,tblmo b,tblitem c where 1=1 and b.orgid = c.orgid " + orgIDList + " and a.shiftday = '" + today + "' and a.segcode ='" + segcode + "' " +
                "and a.ssCode ='" + stepSequenceCode + "' and a.mocode = b.mocode and a.ITEMCODE = c.ITEMCODE ";
            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sqlStr += " and b.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ") ";
            }
            sqlStr += " group by a.sscode,a.mocode,a.itemcode,c.ITEMNAME ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterMocode),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterWeekMocode
        /// <summary>
        /// 报表中心 产线工单 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="segcode">工段</param>
        /// <param name="stepSequenceCode">产线</param>
        /// <returns></returns>
        public object[] QueryRPTCenterWeekMocode(int today, string segcode, string stepSequenceCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select sscode as SSCODE,sum(OUTPUTQTY) as DayQuantity,shiftday as ShiftDay " +
                "from TBLRPTREALLINEQTY where substr(shiftday,1,4) = substr('" + today + "',1,4) " +
                "and week = to_char(to_date('" + today + "','yyyyMMdd'),'ww') " +
                "and segcode ='" + segcode + "' and ssCode ='" + stepSequenceCode + "' " +
                "group by sscode,shiftday ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterWeekMocode),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterMonthMocode
        /// <summary>
        /// 报表中心 产线工单 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="segcode">工段</param>
        /// <param name="stepSequenceCode">产线</param>
        /// <returns></returns>
        public object[] QueryRPTCenterMonthMocode(int today, string segcode, string stepSequenceCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select sscode as SSCODE,sum(OUTPUTQTY) as DayQuantity,shiftday as ShiftDay " +
                "from TBLRPTREALLINEQTY where substr(shiftday,1,6) = substr('" + today + "',1,6) " +
                "and segcode ='" + segcode + "' and ssCode ='" + stepSequenceCode + "' " +
                "group by sscode,shiftday ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterMonthMocode),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterResCode
        /// <summary>
        /// 报表中心 资源 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="segcode">工段</param>
        /// <param name="stepSequenceCode">产线</param>
        /// <param name="moCode">工单号</param>
        /// <param name="itemCode">产品</param>
        /// <returns></returns>
        public object[] QueryRPTCenterResCode(int today, string segcode, string stepSequenceCode, string moCode, string itemCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select * from(select RESCODE as ResCode," +
                "sum(decode(shiftday,'" + today + "',OUTPUTQTY,0)) as DayQuantity," +
                "sum(OUTPUTQTY) as ActQTY from TBLRPTHISOPQTY where segcode ='" + segcode + "' " +
                "and ssCode ='" + stepSequenceCode + "' and mocode = '" + moCode + "' and itemcode =  '" + itemCode + "' " +
                "group by RESCODE) where DayQuantity != 0";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterResCode),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterYield
        /// <summary>
        /// 报表中心 资源良率 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="opCode">工序</param>
        /// <returns></returns>
        public object[] QueryRPTCenterYield(int today, string opCode)
        {
            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND b.orgid IN(" + orgIDList + ") ";

            string sqlStr = string.Empty;

            sqlStr = "select a.sscode as SSCode,a.itemcode as ItemCode,b.itemname as ItemName,a.rescode as ResCode," +
                "1-Decode(sum(a.EATTRIBUTE2),0, 0,null,null,sum(a.ngtimes)/sum(a.EATTRIBUTE2)) as DayPercent " +
                "from TBLRPTHISOPQTY a,tblitem b where 1=1 " + orgIDList + " and a.itemcode=b.itemcode and a.shiftday='" + today + "' and a.opcode ='" + opCode + "' " +
                "group by a.sscode,a.itemcode,b.itemname,a.rescode ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterDayYield),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterWeekPercent
        /// <summary>
        /// 报表中心 资源良率 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="opCode">工序</param>
        /// <returns></returns>
        public object[] QueryRPTCenterWeekPercent(int today, string opCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select opcode,shiftday,(1-Decode(sum(EATTRIBUTE2),0, 0,null,null,sum(ngtimes)/sum(EATTRIBUTE2)))*100 as DayPercent " +
                "from TBLRPTHISOPQTY " +
                "where to_char(to_date(shiftday,'yyyyMMdd'),'ww') = to_char(to_date('" + today + "','yyyyMMdd'),'ww') " +
                "and substr(shiftday,1,4) = substr('" + today + "',1,4) and opCode = '" + opCode + "' " +
                "group by opcode,shiftday";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterWeekYield),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterMonthPercent
        /// <summary>
        /// 报表中心 资源良率 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="opCode">工序</param>
        /// <returns></returns>
        public object[] QueryRPTCenterMonthPercent(int today, string opCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select opcode,shiftday,(1-Decode(sum(EATTRIBUTE2),0, 0,null,null,sum(ngtimes)/sum(EATTRIBUTE2)))*100 as DayPercent " +
                "from TBLRPTHISOPQTY " +
                "where substr(shiftday,1,6) = substr('" + today + "',1,6) and opCode = '" + opCode + "' " +
                "group by opcode,shiftday";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterMonthYield),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterDayYield
        /// <summary>
        /// 报表中心 本日不良分布 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="opCode">工序</param>
        /// <returns></returns>
        public object[] QueryRPTCenterDayYield(int today, string opCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select a.ecgcode as EcgCode,a.ecode as ECode,c.ecdesc as Ecdesc,count(*) as Qty " +
                "from TBLTSERRORCODE a,Tblts b,tblec c " +
                "where a.tsid = b.tsid and a.ecode = c.ecode and b.frmopcode = '" + opCode + "' and b.shiftday = '" + today + "' " +
                "and b.frmweek > 0 and b.frmmonth > 0 " +
                "group by a.ecode,a.ecgcode,c.ecdesc order by a.ecgcode,Qty";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterDayPercent),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterWeekYield
        /// <summary>
        /// 报表中心 本周不良分布 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="opCode">工序</param>
        /// <returns></returns>
        public object[] QueryRPTCenterWeekYield(int today, string opCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select a.ecgcode as EcgCode,a.ecode as ECode,c.ecdesc as Ecdesc,count(*) as Qty " +
                "from TBLTSERRORCODE a,Tblts b,tblec c " +
                "where a.tsid = b.tsid and a.ecode = c.ecode and b.frmopcode = '" + opCode +
                //				"' and to_char(to_date(b.frmdate,'yyyyMMdd'),'ww') = to_char(to_date('" + today + "','yyyyMMdd'),'ww') "+
                "' and b.frmweek = to_char(to_date('" + today + "','yyyyMMdd'),'ww') " +
                "group by a.ecode,a.ecgcode,c.ecdesc order by a.ecgcode,Qty";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterWeekPercent),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterMonthYield
        /// <summary>
        /// 报表中心 本月不良分布 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="opCode">工序</param>
        /// <returns></returns>
        public object[] QueryRPTCenterMonthYield(int today, string opCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select a.ecgcode as EcgCode,a.ecode as ECode,c.ecdesc as Ecdesc,count(*) as Qty " +
                "from TBLTSERRORCODE a,Tblts b,tblec c " +
                "where a.tsid = b.tsid and a.ecode = c.ecode and b.frmopcode = '" + opCode +
                //				"' and substr(b.frmdate,1,6) = substr('" + today + "',1,6) "+
                "' and b.frmmonth = decode(substr('" + today + "',5,1),0,substr('" + today + "',6,1),substr('" + today + "',5,2)) " +
                "and substr(b.frmdate,1,4) = substr('" + today + "',1,4) " +
                "group by a.ecode,a.ecgcode,c.ecdesc order by a.ecgcode,Qty";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterMonthPercent),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterDayProduct
        /// <summary>
        /// 报表中心 本日产品分布 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="opCode">工序</param>
        /// <param name="eCode">不良代码</param>
        /// <returns></returns>
        public object[] QueryRPTCenterDayProduct(int today, string opCode, string eCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select b.RCARD as RCard,b.ITEMCODE as ItemCode,b.FRMSSCODE as FrmSSCode,b.FRMUSER as FrmUser," +
                "b.FRMOPCODE as FrmOPCode,b.FRMRESCODE as FrmResCode,b.SHIFTDAY as ShiftDay " +
                "from TBLTSERRORCODE a,Tblts b,tblec c " +
                "where a.tsid = b.tsid and a.ecode = c.ecode and b.frmopcode = '" + opCode +
                "' and a.eCode = '" + eCode + "' and b.shiftday = '" + today + "' and b.frmweek > 0 and b.frmmonth > 0 ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterDayProduct),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterWeekProduct
        /// <summary>
        /// 报表中心 本周产品分布 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="opCode">工序</param>
        /// <param name="eCode">不良代码</param>
        /// <returns></returns>
        public object[] QueryRPTCenterWeekProduct(int today, string opCode, string eCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select b.RCARD as RCard,b.ITEMCODE as ItemCode,b.FRMSSCODE as FrmSSCode,b.FRMUSER as FrmUser," +
                "b.FRMOPCODE as FrmOPCode,b.FRMRESCODE as FrmResCode,b.SHIFTDAY as ShiftDay " +
                "from TBLTSERRORCODE a,Tblts b,tblec c " +
                "where a.tsid = b.tsid and a.ecode = c.ecode and b.frmopcode = '" + opCode +
                "' and a.eCode = '" + eCode + "' and b.frmweek = to_char(to_date('" + today + "','yyyyMMdd'),'ww') ";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterWeekProduct),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTCenterMonthProduct
        /// <summary>
        /// 报表中心 本月产品分布 
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="opCode">工序</param>
        /// <param name="eCode">不良代码</param>
        /// <returns></returns>
        public object[] QueryRPTCenterMonthProduct(int today, string opCode, string eCode)
        {
            string sqlStr = string.Empty;

            sqlStr = "select b.RCARD as RCard,b.ITEMCODE as ItemCode,b.FRMSSCODE as FrmSSCode,b.FRMUSER as FrmUser," +
                "b.FRMOPCODE as FrmOPCode,b.FRMRESCODE as FrmResCode,b.SHIFTDAY as ShiftDay " +
                "from TBLTSERRORCODE a,Tblts b,tblec c " +
                "where a.tsid = b.tsid and a.ecode = c.ecode and b.frmopcode = '" + opCode +
                "' and b.frmmonth = decode(substr('" + today + "',5,1),0,substr('" + today + "',6,1),substr('" + today + "',5,2)) " +
                "and substr(b.frmdate,1,4) = substr('" + today + "',1,4) and a.ecode = '" + eCode + "'";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTCenterMonthProduct),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region RPTFactoryWeekCheck
        /// <summary>
        /// 外包厂检验周报
        /// </summary>
        /// <param name="lastWeek">本周</param>
        /// <param name="nowWeek">本周</param>
        /// <returns></returns>
        public object[] QueryRPTFactoryWeekCheck(string lastWeek, string nowWeek)
        {
            string sqlStr = string.Empty;

            sqlStr = "select FactoryID as FactoryID," +
                "SUM(Decode(WeekNo,'" + lastWeek + "',Total,0)) as lastTotal,SUM(Decode(WeekNo,'" + lastWeek + "',LRR,0)) as lastLRR," +
                "SUM(Decode(WeekNo,'" + nowWeek + "',Total,0)) as nowTotal,SUM(Decode(WeekNo,'" + nowWeek + "',LRR,0)) as nowLRR " +
                "from TBLFACTORYWEEKCHECK where WeekNo = '" + lastWeek + "' or WeekNo = '" + nowWeek + "' " +
                "group by FactoryID";

#if DEBUG
            Log.Info(sqlStr);
#endif

            return this.DataProvider.CustomQuery(
                typeof(RPTFactoryWeekCheck),
                new SQLCondition(sqlStr));
        }
        #endregion

        #region StorageQuery

        public string GenerateQueryStockAgeSql(string StorageType, string PeiodGroup, string itemCode, string OrderNo,
string Mmodelcode, string FirstClassGroup, bool type, bool checkSAP, int inclusive, int exclusive)
        {
            string sql = string.Empty;

            string sql2 = string.Empty;
            string strsql = string.Empty;

            string strTempStr = string.Empty;

            string FirstClassCondition = "";
            if (FirstClassGroup != "" &&
                FirstClassGroup != null)
            {
                FirstClassCondition = string.Format(" and tblitemclass.firstclass =({0})", FormatHelper.ProcessQueryValues(FirstClassGroup));
            }

            string CusorderNoCondition = "";
            if (OrderNo != "" &&
                OrderNo != null)
            {
                CusorderNoCondition = string.Format(@" and tblmo.cusorderno  like   '%{0}%'", OrderNo.ToUpper());


            }

            string MmodelCodeCondition = "";
            if (Mmodelcode != "" &&
                Mmodelcode != null)
            {
                MmodelCodeCondition = string.Format(" and tblmaterial.mmodelcode in ({0})", FormatHelper.ProcessQueryValues(Mmodelcode));
            }

            //产品代码
            string MCodeCondition = "";
            if (itemCode != "" &&
                itemCode != null)
            {
                MCodeCondition = string.Format(" and tblmaterial.mcode in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            string PeiodgroupCondition = "";
            if (PeiodGroup != "" &&
                PeiodGroup != null)
            {
                PeiodgroupCondition = string.Format(" and tblinvperiod.peiodgroup = {0}", FormatHelper.ProcessQueryValues(PeiodGroup));
            }

            string StorageCodeCondition = "";
            if (StorageType != "" &&
                StorageType != null)
            {
                StorageCodeCondition = string.Format(" and tblstack2rcard.storagecode in ({0})", FormatHelper.ProcessQueryValues(StorageType));
            }

            strTempStr = string.Empty;
            strTempStr += "SELECT tblstack2rcard.company, ";
            strTempStr += "tblstack2rcard.storagecode || ' - ' || tblstorage.storagename AS storagecode, ";
            strTempStr += "tblitemclass.firstclass, tblmaterial.mmodelcode, ";
            strTempStr += "tblmaterial.mcode || ' - ' || tblmaterial.mname AS mcode, ";
            strTempStr += "tblmo.eattribute1 AS mname, tblmo.cusorderno, tblmo.cusitemcode, ";
            strTempStr += "tblstack2rcard.itemgrade, tblinvperiod.invperiodcode, tblinvperiod.datefrom, ";
            strTempStr += "tblinvperiod.dateto, COUNT (tblstack2rcard.serialno) AS cnt ";
            strTempStr += "FROM tblstack2rcard, ";
            strTempStr += "tblmaterial, ";
            strTempStr += "tblitemclass, ";
            strTempStr += "tblinvperiod, ";
            strTempStr += "tblstorage, ";
            strTempStr += "tblinvintransaction ";
            strTempStr += "LEFT JOIN tblmo ";
            strTempStr += "ON tblinvintransaction.mocode = tblmo.mocode ";
            strTempStr += "WHERE 1=1 ";
            strTempStr += "AND tblstack2rcard.transinserial = tblinvintransaction.serial ";
            strTempStr += "AND tblstack2rcard.itemcode = tblmaterial.mcode ";
            strTempStr += "AND tblmaterial.mgroup = tblitemclass.itemgroup ";
            strTempStr += "AND tblstack2rcard.storagecode = tblstorage.storagecode ";
            strTempStr += "AND tblstack2rcard.indate BETWEEN TO_CHAR (SYSDATE - tblinvperiod.dateto, 'yyyymmdd') AND TO_CHAR (SYSDATE - tblinvperiod.datefrom,'yyyymmdd') ";

            if (checkSAP)
            {
                strTempStr += " AND GETRCARDSTATUSONTIME(tblstack2rcard.serialno) like '%SAP完工%'";
            }

            strTempStr += FirstClassCondition
                   + CusorderNoCondition
                   + MmodelCodeCondition
                   + MCodeCondition
                   + PeiodgroupCondition
                   + StorageCodeCondition
                   + " GROUP BY tblstack2rcard.company,tblstack2rcard.storagecode || ' - ' || tblstorage.storagename, tblitemclass.firstclass, tblmaterial.mmodelcode, tblmaterial.mcode || ' - ' ||tblmaterial.mname, tblmo.eattribute1, "
                   + " tblmo.cusorderno,tblmo.cusitemcode, tblstack2rcard.itemgrade, tblinvperiod.invperiodcode, tblinvperiod.datefrom, tblinvperiod.dateto "
                   + " ORDER BY tblstack2rcard.company,tblstack2rcard.storagecode || ' - ' || tblstorage.storagename, tblitemclass.firstclass, tblmaterial.mmodelcode, tblmaterial.mcode || ' - ' ||tblmaterial.mname, tblmo.eattribute1, "
                   + " tblmo.cusorderno,tblmo.cusitemcode, tblstack2rcard.itemgrade, tblinvperiod.invperiodcode, tblinvperiod.datefrom,tblinvperiod.dateto ";

            if (type)
            {
                sql = strTempStr;
            }
            else
            {
                sql = " select count(*) from (" + strTempStr + ")";
            }

            if (type)
            {

                string newSql = " SELECT company,storagecode,firstclass,mmodelcode, mcode, mname,"
                    + " Cusorderno,cusitemcode, itemgrade, invperiodcode, datefrom,dateto, cnt FROM ";

                sql = string.Format("{0} ({1} )", newSql, sql);
            }

            return sql;
        }

        public object[] QueryStockAge(string pauseCode, string pauseStatus, string itemCode, string BOMVersion,
            string pauseStartDate, string pauseEndDate,bool checkSAP, int inclusive, int exclusive)
        {
            string sql = GenerateQueryStockAgeSql(pauseCode, pauseStatus, itemCode, BOMVersion, pauseStartDate, pauseEndDate, true, checkSAP, inclusive, exclusive);

            //return this.DataProvider.CustomQuery(typeof(StockAge), new PagerCondition(sql, inclusive, exclusive));
            return null;
        }

        //public object[] QueryStockAge(string pauseCode, string pauseStatus, string itemCode, string BOMVersion,
        //   string pauseStartDate, string pauseEndDate)
        //{
        //    string sql = GenerateQueryStockAgeSql(pauseCode, pauseStatus, itemCode, BOMVersion, pauseStartDate, pauseEndDate, true);

        //    return this.DataProvider.CustomQuery(typeof(StockAge), new SQLCondition(sql));
        //}

        public int QueryStockAgeCount(string pauseCode, string pauseStatus, string itemCode, string BOMVersion,
             string pauseStartDate, string pauseEndDate,bool checkSAP)
        {
            string sql = GenerateQueryStockAgeSql(pauseCode, pauseStatus, itemCode, BOMVersion, pauseStartDate, pauseEndDate, false, checkSAP, 1, 1);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        #endregion

        #region 绩效指标

        public object[] QueryAchievingRate(int startDate, int endDate, string bigSSCode, string moCode, string itemCode, string materialModelCode, string goodSemiGoodQuery, int inclusive, int exclusive)
        {
            string sql = "SELECT t.shiftday, SUM(t.achievingqty) AS achievingqty, SUM(t.planqty) AS planqty, ROUND(DECODE(SUM(t.planqty), 0, 1, SUM(t.achievingqty) / SUM(t.planqty)), 4) AS achievingrate ";
            sql += GetAchievingRateSql(startDate, endDate, bigSSCode, moCode, itemCode, materialModelCode, goodSemiGoodQuery);
            sql += "GROUP BY t.shiftday";

            return this.DataProvider.CustomQuery(typeof(RptAchievingRate), new PagerCondition(sql, "shiftday", inclusive, exclusive));
        }

        public int QueryAchievingRateCount(int startDate, int endDate, string bigSSCode, string moCode, string itemCode, string materialModelCode, string goodSemiGoodQuery)
        {
            string sql = "SELECT COUNT(DISTINCT t.shiftday) ";
            sql += GetAchievingRateSql(startDate, endDate, bigSSCode, moCode, itemCode, materialModelCode, goodSemiGoodQuery);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetAchievingRateSql(int startDate, int endDate, string bigSSCode, string moCode, string itemCode, string materialModelCode, string goodSemiGoodQuery)
        {
            string sql = string.Empty;
            sql += "FROM ( ";
            sql += "SELECT ";
            sql += "NVL(tblworkplan.plandate, tblrptsoqty.shiftday) AS shiftday, ";
            sql += "NVL(tblworkplan.bigsscode, tblrptsoqty.bigsscode) AS bigsscode, ";
            sql += "NVL(tblworkplan.mocode, tblrptsoqty.mocode) AS mocode, ";
            sql += "NVL(tblworkplan.itemcode, tblrptsoqty.itemcode) AS itemcode, ";
            sql += "NVL(tblworkplan.planqty, 0) AS planqty, ";
            sql += "NVL(tblrptsoqty.realqty, 0) AS achievingqty ";
            sql += "FROM ( ";
            sql += "    SELECT plandate, bigsscode, mocode, itemcode, SUM(planqty) planqty ";
            sql += "    FROM tblworkplan ";
            sql += "    GROUP BY bigsscode, plandate, mocode, itemcode ";
            sql += ") tblworkplan ";
            sql += "FULL JOIN ( ";
            sql += "    SELECT tblrptsoqty.shiftday, tblmesentitylist.bigsscode, tblrptsoqty.mocode, tblrptsoqty.itemcode, SUM(tblrptsoqty.mooutputcount) realqty ";
            sql += "    FROM tblrptsoqty, tblmesentitylist ";
            sql += "    WHERE tblrptsoqty.tblmesentitylist_serial = tblmesentitylist.serial ";
            sql += "    GROUP BY tblrptsoqty.shiftday, tblmesentitylist.bigsscode, tblrptsoqty.mocode, tblrptsoqty.itemcode ";
            sql += ") tblrptsoqty ";
            sql += "ON tblworkplan.bigsscode = tblrptsoqty.bigsscode ";
            sql += "AND tblworkplan.plandate = tblrptsoqty.shiftday ";
            sql += "AND tblworkplan.mocode = tblrptsoqty.mocode ";
            sql += "AND tblworkplan.itemcode = tblrptsoqty.itemcode ";
            sql += ") t ";
            sql += "LEFT OUTER JOIN tblmaterial ";
            sql += "ON tblmaterial.mcode = t.itemcode ";
            sql += "WHERE 1 = 1 ";

            if (startDate > 0)
            {
                sql += "AND t.shiftday >= " + startDate.ToString() + " ";
            }

            if (endDate > 0)
            {
                sql += "AND t.shiftday <= " + endDate.ToString() + " ";
            }

            if (bigSSCode.Trim().Length > 0)
            {
                if (bigSSCode.IndexOf(",") >= 0)
                {
                    sql += "AND t.bigsscode IN (" + FormatHelper.ProcessQueryValues(bigSSCode) + ") ";
                }
                else
                {
                    sql += "AND t.bigsscode LIKE '%" + FormatHelper.PKCapitalFormat(bigSSCode) + "%' ";
                }
            }

            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    sql += "AND t.mocode IN (" + FormatHelper.ProcessQueryValues(moCode) + ") ";
                }
                else
                {
                    sql += "AND t.mocode LIKE '%" + FormatHelper.PKCapitalFormat(moCode) + "%' ";
                }
            }

            if (itemCode.Trim().Length > 0)
            {
                if (itemCode.IndexOf(",") >= 0)
                {
                    sql += "AND t.itemcode IN (" + FormatHelper.ProcessQueryValues(itemCode) + ") ";
                }
                else
                {
                    sql += "AND t.itemcode LIKE '%" + FormatHelper.PKCapitalFormat(itemCode) + "%' ";
                }
            }

            if (materialModelCode.Trim().Length > 0)
            {
                if (materialModelCode.IndexOf(",") >= 0)
                {
                    sql += "AND tblmaterial.mmodelcode IN (" + FormatHelper.ProcessQueryValues(materialModelCode) + ") ";
                }
                else
                {
                    sql += "AND tblmaterial.mmodelcode LIKE '%" + FormatHelper.PKCapitalFormat(materialModelCode) + "%' ";
                }
            }

            if (goodSemiGoodQuery.Trim().Length > 0)
            {
                if (goodSemiGoodQuery.IndexOf(",") >= 0)
                {
                    sql += "AND UPPER(tblmaterial.mtype) IN (" + FormatHelper.ProcessQueryValues(goodSemiGoodQuery) + ") ";
                }
                else
                {
                    sql += "AND UPPER(tblmaterial.mtype) LIKE '%" + FormatHelper.PKCapitalFormat(goodSemiGoodQuery) + "%' ";
                }
            }

            return sql;
        }

        #endregion
    }
}
