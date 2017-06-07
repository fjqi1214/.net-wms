using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.OQC;

namespace BenQGuru.eMES.WebQuery
{
    /// <summary>
    /// QueryOQCLotDetailsFacade 的摘要说明。
    /// </summary>
    public class QueryOQCLotDetailsFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public QueryOQCLotDetailsFacade(IDomainDataProvider domainDataProvider)
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

        #region 送检批明细查询

        public object[] QueryOQCLotDetailsListInfo(
            string oqclot,
            int inclusive, int exclusive)
        {
#if DEBUG
            Log.Info(
                new PagerCondition(
                string.Format(@"select distinct {0} from TBLLOT2CARD where lotno = '{1}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)),
                oqclot.ToUpper()), "RCARD", inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(OQCLot2Card),
                new PagerCondition(
                string.Format(@"select distinct {0} from TBLLOT2CARD where lotno = '{1}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)),
                oqclot.ToUpper()), "RCARD", inclusive, exclusive, true));
        }

        public int QueryOQCLotDetailsListInfoCount(
            string oqclot)
        {
#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(@"select {0} from TBLLOT2CARD where lotno = '{1}'",
                "count(rcard)",
                oqclot.ToUpper())).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(@"select {0} from TBLLOT2CARD where lotno = '{1}'",
                "count(rcard)",
                oqclot.ToUpper())));
        }

        #endregion

        #region  送检批明细 最新方法

        public object[] QueryOQCLotDetails(string productiontype, string ss,
            string itemCodes, string moCodes,
            string oqclot, string state, string startSn, string endSn,
            string firstClass, string secondClass, string thirdClass, string crewCode,
            string bigLine, string materialModelCode, string materialMachineType, string materialExportImport,
            string decideMan, string oqcLotType, string reWorkMO,
            int ReworkStartDate, int ReworkEndDate,
            int packedstartDate, int packedstartTime,
            int packedendDate, int packedendTime,
            int teststartDate, int teststartTime,
            int testendDate, int testendTime,
            bool isQueryHistory,
            int inclusive, int exclusive)
        {
            string itemCondition = "";
            if (ss.Trim().Length > 0 && ss != null)
            {
                if (ss.IndexOf(",") >= 0)
                {
                    string[] lists = ss.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    ss = string.Join(",", lists);
                    itemCondition += string.Format(@" and TBLLOT.sscode in ({0})", ss.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and TBLLOT.sscode like '{0}%'", ss.ToUpper());
                }
            }
            if (itemCodes.Trim().Length > 0 && itemCodes != null)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    itemCondition += string.Format(@" and TBLLOT.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and TBLLOT.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }

            if (productiontype != "" && productiontype != null)
            {
                if (productiontype.IndexOf(",") >= 0)
                {
                    string[] lists = productiontype.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    productiontype = string.Join(",", lists);
                    itemCondition += string.Format(@" and TBLLOT.productiontype in ({0})", productiontype);
                }
                else
                {
                    itemCondition += string.Format(@" and TBLLOT.productiontype like '{0}%'", productiontype);
                }
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    moCondition += string.Format(@" and TBLLOT2CARD.mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and TBLLOT2CARD.mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            string oqclotCondition = "";
            if (oqclot != "" && oqclot != null)
            {
                oqclotCondition = string.Format(@" and TBLLOT.LOTNO like '{0}%'", oqclot.ToUpper());
            }

            string stateCondition = "";
            if (state != "" && state != null)
            {
                stateCondition = string.Format(@" and upper(TBLLOT.LOTSTATUS) = '{0}'", state.ToUpper());
            }


            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("TBLLOT2CARD.rcard", startSn.ToUpper(), endSn.ToUpper());

            string dateCondition = string.Empty;
            if (packedstartDate != DefaultDateTime.DefaultToInt)
            {
                dateCondition = FormatHelper.GetDateRangeSql("TBLLOT.MDATE", "TBLLOT.MTIME", packedstartDate, packedstartTime, packedendDate, packedendTime);
            }
            else if (teststartDate != DefaultDateTime.DefaultToInt)
            {
                dateCondition += FormatHelper.GetDateRangeSql("TBLLOT.shiftday", teststartDate, testendDate);
            }

            string itemClassCondition = string.Empty;
            if (firstClass.Trim().Length > 0)
            {
                itemClassCondition += string.Format(@" and tblitemclass.firstclass = '{0}'", firstClass.Trim().ToUpper());
            }
            if (secondClass.Trim().Length > 0)
            {
                itemClassCondition += string.Format(@" and tblitemclass.secondclass = '{0}'", secondClass.Trim().ToUpper());
            }
            if (thirdClass.Trim().Length > 0)
            {
                itemClassCondition += string.Format(@" and tblitemclass.thirdclass = '{0}'", thirdClass.Trim().ToUpper());
            }

            string crewCodeCondition = string.Empty;
            if (crewCode.Trim().Length > 0)
            {
                crewCodeCondition += string.Format(@" and tblline2crew.crewcode = '{0}' ", crewCode.Trim().ToUpper());
            }

            string ReworkCondition = string.Empty;
            if (ReworkStartDate > 0)
            {
                ReworkCondition += string.Format(@" and TBLLOT.ddate >={0} ", ReworkStartDate);
            }
            if (ReworkEndDate > 0)
            {
                ReworkCondition += string.Format(@" and TBLLOT.ddate <={0} ", ReworkEndDate);
            }

            string OQCLotTypeCondition = string.Empty;
            if (oqcLotType.Trim().Length > 0)
            {
                if (oqcLotType.IndexOf(",") >= 0)
                {
                    string[] lists = oqcLotType.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    oqcLotType = string.Join(",", lists);

                    OQCLotTypeCondition += string.Format(@" and TBLLOT.oqcLotType in ({0})", oqcLotType);
                }
                else
                {
                    OQCLotTypeCondition += string.Format(@" and TBLLOT.oqcLotType like '{0}%'", oqcLotType);
                }
            }

            string bigsscodeCondition = string.Empty;
            if (bigLine.Trim().Length > 0)
            {
                if (bigLine.IndexOf(",") >= 0)
                {
                    string[] lists = bigLine.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigLine = string.Join(",", lists);

                    bigsscodeCondition += string.Format(@" and tblss.bigsscode in ({0})", bigLine.ToUpper());
                }
                else
                {
                    bigsscodeCondition += string.Format(@" and tblss.bigsscode like '{0}%'", bigLine.ToUpper());
                }
            }

            string mmodelcodeCondition = string.Empty;
            if (materialModelCode.Trim().Length > 0)
            {
                if (materialModelCode.IndexOf(",") >= 0)
                {
                    string[] lists = materialModelCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    materialModelCode = string.Join(",", lists);

                    mmodelcodeCondition += string.Format(@" and tblmaterial.mmodelcode in ({0})", materialModelCode.ToUpper());
                }
                else
                {
                    mmodelcodeCondition += string.Format(@" and tblmaterial.mmodelcode like '{0}%'", materialModelCode.ToUpper());
                }
            }

            string mmachinetypeCondition = string.Empty;
            if (materialMachineType.Trim().Length > 0)
            {
                if (materialMachineType.IndexOf(",") >= 0)
                {
                    string[] lists = materialMachineType.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    materialMachineType = string.Join(",", lists);

                    mmachinetypeCondition += string.Format(@" and tblmaterial.mmachinetype in ({0})", materialMachineType.ToUpper());
                }
                else
                {
                    mmachinetypeCondition += string.Format(@" and tblmaterial.mmachinetype like '{0}%'", materialMachineType.ToUpper());
                }
            }

            string mexportimportCondition = string.Empty;
            if (materialExportImport.Trim().Length > 0)
            {
                mexportimportCondition += string.Format(@" and tblmaterial.mexportimport = '{0}'", materialExportImport);
            }

            string reWorkMOCondition = string.Empty;
            if (reWorkMO.Trim().Length > 0)
            {
                reWorkMOCondition += string.Format(@" and tblreworksheet.reworkcode like '{0}%'", reWorkMO.ToUpper());
            }

            string decideManCondition = string.Empty;
            if (decideMan.Trim().Length > 0)
            {
                if (decideMan.IndexOf(",") >= 0)
                {
                    string[] lists = decideMan.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    decideMan = string.Join(",", lists);

                    decideManCondition += string.Format(@" and tbllot.duser in ({0}) ", decideMan.ToUpper());
                }
                else
                {
                    decideManCondition += string.Format(@" and tbllot.duser like  '{0}%' ", decideMan.ToUpper());
                }
            }
            string selectColnums = string.Empty; 
            
            selectColnums += FormatHelper.GetAllFieldWithDesc(typeof(OQCLot), "tbllot", new string[] { "itemcode", "sscode", "muser", "duser" }, new string[] { "tblitem.itemdesc", "tblss.ssdesc", "tbluser1.username", "tbluser2.username" });
            selectColnums += ",tbloqclotcklist.agradetimes,tbloqclotcklist.bggradetimes,tbloqclotcklist.cgradetimes,tbloqclotcklist.zgradetimes,tblmaterial.mdesc,tblmaterial.mmodelcode,tblmaterial.mmachinetype,tblmaterial.mexportimport,tblss.bigsscode,tblreworksheet.reworkcode ";
            selectColnums += ",";
            selectColnums += FormatHelper.GetFieldWithDesc("tblline2crew", "crewcode", "tblcrew", "crewdesc");

            string groupColumns = selectColnums.ToLower();
            groupColumns = groupColumns.Replace("as itemcode", "");
            groupColumns = groupColumns.Replace("as sscode", "");
            groupColumns = groupColumns.Replace("as muser", "");
            groupColumns = groupColumns.Replace("as duser", "");
            groupColumns = groupColumns.Replace("as crewcode", "");

            string sumColnums = string.Empty;
            sumColnums += ",COUNT(rcard) AS samplecount ";
            sumColnums += ",SUM(DECODE(tbllot2cardcheck.status, 'NG', 1, 0)) AS samplengcount ";
            //sumColnums += ",DECODE(tbllot.lotstatus, '" + OQCLotStatus.OQCLotStatus_Examing + "', 0, '" + OQCLotStatus.OQCLotStatus_Reject + "', 0, tbllot.lotsize - SUM(DECODE(status, 'NG', 1, 0))) AS firstgoodcount ";
            sumColnums += ",1 AS firstgoodcount ";

            string _sql = @" select {0} from TBLLOT 
									left outer join tbloqclotcklist on tbllot.lotno = tbloqclotcklist.lotno
                                    left outer join tblmaterial on tbllot.itemcode = tblmaterial.mcode
                                    left outer join tblitemclass on tblmaterial.mgroup = tblitemclass.itemgroup
                                    left outer join tblss on tbllot.sscode=tblss.sscode
                                    left outer join tblreworksheet on tblreworksheet.lotlist=tbllot.lotno
                                    LEFT OUTER JOIN tblline2crew ON tblline2crew.sscode = tbllot.sscode AND tblline2crew.shiftdate = tbllot.shiftday AND tblline2crew.shiftcode = tbllot.shiftcode
                                    left outer join tblcrew on tblline2crew.crewcode=tblcrew.crewcode
                                    left outer join tbluser tbluser1 on tbllot.muser=tbluser1.usercode
                                    left outer join tbluser tbluser2 on tbllot.duser=tbluser2.usercode
                                    left outer join tblitem on tbllot.itemcode=tblitem.itemcode
                                    LEFT OUTER JOIN tbllot2cardcheck ON tbllot.lotno = tbllot2cardcheck.lotno
									where 1=1
                                    {8}  {1}
									and TBLLOT.LOTNO in ( select LOTNO from TBLLOT2CARD where TBLLOT.LOTNO = TBLLOT2CARD.LOTNO {2}{5} {9} {10} {11} ) 
									
                                    {6} {3} {4} {7} {12} {13} {14} {15} {16}";

            string sql = string.Format(_sql,
                selectColnums + sumColnums,
                itemCondition, moCondition, oqclotCondition, stateCondition, SnCondition, dateCondition, itemClassCondition, crewCodeCondition,
                ReworkCondition, OQCLotTypeCondition, decideManCondition, bigsscodeCondition, mmodelcodeCondition, mmachinetypeCondition, mexportimportCondition, reWorkMOCondition);

            sql += "GROUP BY " + groupColumns;

            object[] returnObjs = null;

#if DEBUG
            Log.Info(
                new PagerCondition(
                sql, "LOTNO", inclusive, exclusive, true).SQLText);
#endif
            returnObjs = this.DataProvider.CustomQuery(
                typeof(QueryOQCLot),
                new PagerCondition(
                sql, "LOTNO", inclusive, exclusive, true));

            return returnObjs;
        }

        private void GetQueryOQCLot(object[] objs, string oqclotCondition, ArrayList list)
        {
            string _sql = @" select TBLLOT.*,tbloqclotcklist.agradetimes,tbloqclotcklist.bggradetimes,tbloqclotcklist.cgradetimes from TBLLOT 
									left join tbloqclotcklist on (tbllot.lotno = tbloqclotcklist.lotno)
									where 1=1
									and TBLLOT.LOTNO in ( select LOTNO from TBLLOT2CARD where TBLLOT.LOTNO = TBLLOT2CARD.LOTNO {0} ) 
									and tbllot.lotseq = ( select max(lotseq) from TBLLOT b where TBLLOT.LOTNO = b.LOTNO  )  ";

            string sql = string.Format(_sql, oqclotCondition);

#if DEBUG
            Log.Info(
                new SQLCondition(sql).SQLText);
#endif
            object[] returnObjs = this.DataProvider.CustomQuery(
                typeof(QueryOQCLot),
                new SQLCondition(sql));


            if (returnObjs != null && returnObjs.Length > 0)
            {
                ArrayList oldLotNo = new ArrayList();
                foreach (QueryOQCLot lot in returnObjs)
                {
                    list.Add(lot);

                    if (lot.OldLotNo != null && lot.OldLotNo != "")
                    {
                        oldLotNo.Add(lot.OldLotNo);
                    }
                }

                if (oldLotNo.Count > 0)
                {
                    string _oqclotCondition = "and TBLLOT.LOTNO in ('" + string.Join("','", (string[])oldLotNo.ToArray(typeof(string))) + "') ";

                    GetQueryOQCLot(objs, _oqclotCondition, list);
                }
            }
        }

        private void GetQueryOQCLot(object[] objs, string _sql, string itemCondition, string moCondition,
            string oqclotCondition, string stateCondition, string SnCondition, string dateCondition, ArrayList list)
        {
            string sql = string.Format(_sql,
                "TBLLOT.*,tbloqclotcklist.agradetimes,tbloqclotcklist.bggradetimes,tbloqclotcklist.cgradetimes ",
                itemCondition, moCondition, oqclotCondition, stateCondition, SnCondition, dateCondition);
#if DEBUG
            Log.Info(
                new SQLCondition(sql).SQLText);
#endif
            object[] returnObjs = this.DataProvider.CustomQuery(
                typeof(QueryOQCLot),
                new SQLCondition(sql));


            if (returnObjs != null && returnObjs.Length > 0)
            {
                ArrayList oldLotNo = new ArrayList();
                foreach (QueryOQCLot lot in returnObjs)
                {
                    list.Add(lot);

                    if (lot.OldLotNo != null && lot.OldLotNo != "")
                    {
                        oldLotNo.Add(lot.OldLotNo);
                    }
                }

                if (oldLotNo.Count > 0)
                {
                    string _oqclotCondition = "and TBLLOT.LOTNO in ('" + string.Join("','", (string[])oldLotNo.ToArray(typeof(string))) + "') ";

                    GetQueryOQCLot(objs, sql, itemCondition, moCondition, _oqclotCondition, stateCondition, SnCondition,
                        dateCondition, list);
                }
            }
        }

        private decimal ToDateTimeInt(int date, int time)
        {
            decimal dd = Convert.ToDecimal(date * 1000000D + time);
            return dd;
        }

        public int QueryOQCLotDetailsCount(string productiontype, string ss,
            string itemCodes, string moCodes,
            string oqclot, string state, string startSn, string endSn,
            string firstClass, string secondClass, string thirdClass, string crewCode,
            string bigLine, string materialModelCode, string materialMachineType, string materialExportImport,
            string decideMan, string oqcLotType, string reWorkMO,
            int ReworkStartDate, int ReworkEndDate,
            int packedstartDate, int packedstartTime,
            int packedendDate, int packedendTime,
            int teststartDate, int teststartTime,
            int testendDate, int testendTime
            )
        {
            string itemCondition = "";
            if (ss.Trim().Length > 0 && ss != null)
            {
                if (ss.IndexOf(",") >= 0)
                {
                    string[] lists = ss.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    ss = string.Join(",", lists);
                    itemCondition += string.Format(@" and TBLLOT.sscode in ({0})", ss.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and TBLLOT.sscode like '{0}%'", ss.ToUpper());
                }
            }

            if (itemCodes.Trim().Length > 0 && itemCodes != null)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    itemCondition += string.Format(@" and TBLLOT.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and TBLLOT.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }


            if (productiontype != "" && productiontype != null)
            {
                if (productiontype.IndexOf(",") >= 0)
                {
                    string[] lists = productiontype.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    productiontype = string.Join(",", lists);
                    itemCondition += string.Format(@" and TBLLOT.productiontype in ({0})", productiontype);
                }
                else
                {
                    itemCondition += string.Format(@" and TBLLOT.productiontype like '{0}%'", productiontype);
                }
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    moCondition += string.Format(@" and TBLLOT2CARD.mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and TBLLOT2CARD.mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            string oqclotCondition = "";
            if (oqclot != "" && oqclot != null)
            {
                oqclotCondition = string.Format(@" and TBLLOT.LOTNO like '{0}%'", oqclot.ToUpper());
            }

            string stateCondition = "";
            if (state != "" && state != null)
            {
                stateCondition = string.Format(@" and upper(TBLLOT.LOTSTATUS) = '{0}'", state.ToUpper());
            }

            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("TBLLOT2CARD.rcard", startSn.ToUpper(), endSn.ToUpper());

            string dateCondition = string.Empty;
            if (packedstartDate != DefaultDateTime.DefaultToInt)
            {
                dateCondition = FormatHelper.GetDateRangeSql("TBLLOT.MDATE", "TBLLOT.MTIME", packedstartDate, packedstartTime, packedendDate, packedendTime);
            }
            else if (teststartDate != DefaultDateTime.DefaultToInt)
            {
                dateCondition += FormatHelper.GetDateRangeSql("TBLLOT.shiftday", teststartDate, testendDate);
            }

            string itemClassCondition = string.Empty;
            if (firstClass.Trim().Length > 0)
            {
                itemClassCondition += string.Format(@" and tblitemclass.firstclass = '{0}'", firstClass.Trim().ToUpper());
            }
            if (secondClass.Trim().Length > 0)
            {
                itemClassCondition += string.Format(@" and tblitemclass.secondclass = '{0}'", secondClass.Trim().ToUpper());
            }
            if (thirdClass.Trim().Length > 0)
            {
                itemClassCondition += string.Format(@" and tblitemclass.thirdclass = '{0}'", thirdClass.Trim().ToUpper());
            }

            string crewCodeCondition = string.Empty;
            if (crewCode.Trim().Length > 0)
            {
                crewCodeCondition += string.Format(@" and tblline2crew.crewcode = '{0}' ", crewCode.Trim().ToUpper());
            }

            string ReworkCondition = string.Empty;
            if (ReworkStartDate > 0)
            {
                ReworkCondition += string.Format(@" and TBLLOT.ddate >={0} ", ReworkStartDate);
            }
            if (ReworkEndDate > 0)
            {
                ReworkCondition += string.Format(@" and TBLLOT.ddate <={0} ", ReworkEndDate);
            }

            string OQCLotTypeCondition = string.Empty;
            if (oqcLotType.Trim().Length > 0)
            {
                if (oqcLotType.IndexOf(",") >= 0)
                {
                    string[] lists = oqcLotType.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    oqcLotType = string.Join(",", lists);

                    OQCLotTypeCondition += string.Format(@" and TBLLOT.oqcLotType in ({0})", oqcLotType);
                }
                else
                {
                    OQCLotTypeCondition += string.Format(@" and TBLLOT.oqcLotType like '{0}%'", oqcLotType);
                }
            }

            string bigsscodeCondition = string.Empty;
            if (bigLine.Trim().Length > 0)
            {
                if (bigLine.IndexOf(",") >= 0)
                {
                    string[] lists = bigLine.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigLine = string.Join(",", lists);

                    bigsscodeCondition += string.Format(@" and tblss.bigsscode in ({0})", bigLine.ToUpper());
                }
                else
                {
                    bigsscodeCondition += string.Format(@" and tblss.bigsscode like '{0}%'", bigLine.ToUpper());
                }
            }

            string mmodelcodeCondition = string.Empty;
            if (materialModelCode.Trim().Length > 0)
            {
                if (materialModelCode.IndexOf(",") >= 0)
                {
                    string[] lists = materialModelCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    materialModelCode = string.Join(",", lists);

                    mmodelcodeCondition += string.Format(@" and tblmaterial.mmodelcode in ({0})", materialModelCode.ToUpper());
                }
                else
                {
                    mmodelcodeCondition += string.Format(@" and tblmaterial.mmodelcode like '{0}%'", materialModelCode.ToUpper());
                }
            }

            string mmachinetypeCondition = string.Empty;
            if (materialMachineType.Trim().Length > 0)
            {
                if (materialMachineType.IndexOf(",") >= 0)
                {
                    string[] lists = materialMachineType.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    materialMachineType = string.Join(",", lists);

                    mmachinetypeCondition += string.Format(@" and tblmaterial.mmachinetype in ({0})", materialMachineType.ToUpper());
                }
                else
                {
                    mmachinetypeCondition += string.Format(@" and tblmaterial.mmachinetype like '{0}%'", materialMachineType.ToUpper());
                }
            }

            string mexportimportCondition = string.Empty;
            if (materialExportImport.Trim().Length > 0)
            {
                mexportimportCondition += string.Format(@" and tblmaterial.mexportimport = '{0}'", materialExportImport);
            }

            string reWorkMOCondition = string.Empty;
            if (reWorkMO.Trim().Length > 0)
            {
                reWorkMOCondition += string.Format(@" and tblreworksheet.reworkcode like '{0}%'", reWorkMO.ToUpper());
            }

            string decideManCondition = string.Empty;
            if (decideMan.Trim().Length > 0)
            {
                if (decideMan.IndexOf(",") >= 0)
                {
                    string[] lists = decideMan.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    decideMan = string.Join(",", lists);

                    decideManCondition += string.Format(@" and tbllot.duser in ({0}) ", decideMan.ToUpper());
                }
                else
                {
                    decideManCondition += string.Format(@" and tbllot.duser like  '{0}%' ", decideMan.ToUpper());
                }
            }

            string sql = string.Format(
                @"select count(*) from (
                    select distinct {0} 
                    from TBLLOT 
                    left join tblmaterial on tbllot.itemcode = tblmaterial.mcode
                    left join tblitemclass on tblmaterial.mgroup = tblitemclass.itemgroup
                    left join tblss on tbllot.sscode=tblss.sscode
                    left join tblreworksheet on tblreworksheet.lotlist=tbllot.lotno
                    LEFT OUTER JOIN tblline2crew ON tblline2crew.sscode = tbllot.sscode AND tblline2crew.shiftdate = tbllot.shiftday AND tblline2crew.shiftcode = tbllot.shiftcode
				    where 1=1 
                    {8} {1}
                    and TBLLOT.LOTNO in ( select LOTNO from TBLLOT2CARD where TBLLOT.LOTNO = TBLLOT2CARD.LOTNO {2}{5} {9} {10} {11} ) 
				    
                    {6} {3} {4} {7} {12} {13} {14} {15} {16})",
                "TBLLOT.LOTNO,TBLLOT.LotSize,TBLLOT.OQCLotType,TBLLOT.LOTSTATUS,TBLLOT.MUSER,TBLLOT.DDATE,TBLLOT.DTIME,TBLLOT.MDATE,TBLLOT.MTIME",
                itemCondition, moCondition, oqclotCondition, stateCondition, SnCondition, dateCondition, itemClassCondition, crewCodeCondition,
                ReworkCondition, OQCLotTypeCondition, decideManCondition, bigsscodeCondition, mmodelcodeCondition, mmachinetypeCondition, mexportimportCondition, reWorkMOCondition);
#if DEBUG
            Log.Info(
                new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(sql));
        }

        #endregion

        #region 送检批明细导出

        //产品明细导出
        public object[] ExportQueryOQCLotDetails(string productintype, string ss,
            string itemCodes, string moCodes,
            string oqclot, string state, string startSn, string endSn,
            string firstClass, string secondClass, string thirdClass, string crewCode,
            string bigLine, string materialModelCode, string materialMachineType, string materialExportImport,
            string decideMan, string oqcLotType, string reWorkMO,
            int ReworkStartDate, int ReworkEndDate,
            int packedstartDate, int packedstartTime,
            int packedendDate, int packedendTime,
            int teststartDate, int teststartTime,
            int testendDate, int testendTime,
            bool isQueryHistory,
            int inclusive, int exclusive)
        {
            object[] mainLotInfo = this.QueryOQCLotDetails(productintype, ss, itemCodes, moCodes,
                oqclot, state, startSn, endSn,
                firstClass, secondClass, thirdClass, crewCode,
               bigLine, materialModelCode, materialMachineType, materialExportImport,
             decideMan, oqcLotType, reWorkMO,
              ReworkStartDate, ReworkEndDate,
                packedstartDate, packedstartTime,
                packedendDate, packedendTime,
                teststartDate, teststartTime,
                testendDate, testendTime,
                isQueryHistory,
                1, int.MaxValue);

            ArrayList returnList = new ArrayList();
            if (mainLotInfo != null && mainLotInfo.Length > 0)
            {
                foreach (QueryOQCLot qlot in mainLotInfo)
                {
                    object[] detials = this.QueryOQCLotDetailsListInfo(qlot.LOTNO, 1, int.MaxValue);
                    if (detials != null && detials.Length > 0)
                    {
                        foreach (OQCLot2Card detail in detials)
                        {
                            ExportQueryOQCLotDetails exportdetail = new ExportQueryOQCLotDetails();
                            //主档信息映射
                            exportdetail.ItemCode = qlot.ItemCode;
                            exportdetail.LOTNO = qlot.LOTNO;
                            exportdetail.SSCode = qlot.SSCode;
                            exportdetail.LotSize = qlot.LotSize;
                            exportdetail.OQCLotType = qlot.OQCLotType;
                            exportdetail.LOTStatus = qlot.LOTStatus;
                            exportdetail.SampleCount = qlot.SampleCount;
                            exportdetail.SampleNGCount = qlot.SampleNGCount;
                            exportdetail.FirstGoodCount = qlot.FirstGoodCount;
                            exportdetail.AGradeTimes = qlot.AGradeTimes;
                            exportdetail.BGradeTimes = qlot.BGradeTimes;
                            exportdetail.CGradeTimes = qlot.CGradeTimes;
                            exportdetail.ZGradeTimes = qlot.ZGradeTimes;
                            exportdetail.MaintainUser = qlot.MaintainUser;
                            exportdetail.MaintainDate = qlot.MaintainDate;
                            exportdetail.MaintainTime = qlot.MaintainTime;
                            exportdetail.DealUser = qlot.DealUser;
                            exportdetail.DealDate = qlot.DealDate;
                            exportdetail.DealTime = qlot.DealTime;

                            //明细映射
                            exportdetail.RunningCard = detail.RunningCard;
                            exportdetail.ItemCode = detail.ItemCode;
                            exportdetail.MaintainDate = detail.MaintainDate;
                            exportdetail.MaintainTime = detail.MaintainTime;

                            returnList.Add(exportdetail);
                        }
                    }
                }
            }


            return (ExportQueryOQCLotDetails[])returnList.ToArray(typeof(ExportQueryOQCLotDetails));
        }


        //样本明细导出
        public object[] ExportQueryOQCLotSampleDetails(string productiontype, string ss,
            string itemCodes, string moCodes,
            string oqclot, string state, string startSn, string endSn,
            string firstClass, string secondClass, string thirdClass, string crewCode,
            string bigLine, string materialModelCode, string materialMachineType, string materialExportImport,
            string decideMan, string oqcLotType, string reWorkMO,
            int ReworkStartDate, int ReworkEndDate,
            int packedstartDate, int packedstartTime,
            int packedendDate, int packedendTime,
            int teststartDate, int teststartTime,
            int testendDate, int testendTime,
            bool isQueryHistory,
            int inclusive, int exclusive)
        {
            object[] mainLotInfo = this.QueryOQCLotDetails(productiontype, ss, itemCodes, moCodes,
                oqclot, state, startSn, endSn,
                firstClass, secondClass, thirdClass, crewCode,
               bigLine, materialModelCode, materialMachineType, materialExportImport,
             decideMan, oqcLotType, reWorkMO,
            ReworkStartDate, ReworkEndDate,
                packedstartDate, packedstartTime,
                packedendDate, packedendTime,
                teststartDate, teststartTime,
                testendDate, testendTime,
                isQueryHistory,
                1, int.MaxValue);

            QueryTSDetailsFacade tsdetailFacade = new QueryTSDetailsFacade(this.DataProvider);
            ArrayList returnList = new ArrayList();
            if (mainLotInfo != null && mainLotInfo.Length > 0)
            {
                foreach (QueryOQCLot qlot in mainLotInfo)
                {
                    object[] detials = tsdetailFacade.QueryOQCSampleNGDetails(qlot.LOTNO, 1, int.MaxValue);
                    if (detials != null && detials.Length > 0)
                    {
                        foreach (QDOTSDetails1 tsdetail in detials)
                        {
                            ExportQueryOQCLotSampleDetails exportdetail = new ExportQueryOQCLotSampleDetails();
                            //主档信息映射
                            exportdetail.ItemCode = qlot.ItemCode;
                            exportdetail.BigStepSequenceCode = qlot.BigStepSequenceCode;
                            exportdetail.LOTNO = qlot.LOTNO;
                            exportdetail.SSCode = qlot.SSCode;
                            exportdetail.CrewCode = qlot.CrewCode;//Added By Nettie Chen 2009/09/23
                            exportdetail.LotSize = qlot.LotSize;
                            exportdetail.OQCLotType = qlot.OQCLotType;
                            exportdetail.LOTStatus = qlot.LOTStatus;
                            exportdetail.SampleCount = qlot.SampleCount;
                            exportdetail.SampleNGCount = qlot.SampleNGCount;
                            exportdetail.FirstGoodCount = qlot.FirstGoodCount;
                            exportdetail.AGradeTimes = qlot.AGradeTimes;
                            exportdetail.BGradeTimes = qlot.BGradeTimes;
                            exportdetail.CGradeTimes = qlot.CGradeTimes;
                            exportdetail.ZGradeTimes = qlot.ZGradeTimes;
                            exportdetail.MaintainUser = qlot.MaintainUser;
                            exportdetail.MaintainDate = qlot.MaintainDate;
                            exportdetail.MaintainTime = qlot.MaintainTime;
                            exportdetail.DealUser = qlot.DealUser;
                            exportdetail.DealDate = qlot.DealDate;
                            exportdetail.DealTime = qlot.DealTime;
                            exportdetail.MaterialMachineType = qlot.MaterialMachineType;
                            exportdetail.MaterialModelCode = qlot.MaterialModelCode;
                            exportdetail.MaterialExportImport = qlot.MaterialExportImport;
                            exportdetail.ReworkCode = qlot.ReworkCode;
                            exportdetail.OldLotNo = qlot.OldLotNo;
                            exportdetail.LotFrozen = qlot.LotFrozen;


                            //样本不良明细映射
                            exportdetail.RunningCard = tsdetail.RunningCard;
                            exportdetail.ErrorCodeGroupDescription = tsdetail.ErrorCodeGroupDescription;
                            exportdetail.ErrorCodeDescription = tsdetail.ErrorCodeDescription;
                            exportdetail.ErrorCauseDescription = tsdetail.ErrorCauseDescription;
                            exportdetail.ErrorLocation = tsdetail.ErrorLocation;
                            exportdetail.ErrorParts = tsdetail.ErrorParts;
                            exportdetail.SolutionDescription = tsdetail.SolutionDescription;
                            exportdetail.DutyDescription = tsdetail.DutyDescription;
                            exportdetail.Memo = tsdetail.Memo;
                            exportdetail.TSOperator = tsdetail.TSOperator;
                            exportdetail.TSDate = tsdetail.TSDate;
                            exportdetail.TSTime = tsdetail.TSTime;

                            returnList.Add(exportdetail);
                        }
                    }
                }
            }


            return (ExportQueryOQCLotSampleDetails[])returnList.ToArray(typeof(ExportQueryOQCLotSampleDetails));
        }

        #endregion

    }
}
