using System;
using System.Collections;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.DataCollect;

using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.WebQuery
{
    /// <summary>
    /// QueryCmponentLoadingFacade 的摘要说明。
    /// </summary>
    public class QueryComponentLoadingFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public QueryComponentLoadingFacade(IDomainDataProvider domainDataProvider)
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

        #region Integrated

        #region 集成上料查询

        public object[] QueryIntegratedLoading(
            string itemCodes, string moCodes, string mcard,
            string startSn, string endSn,
            int inclusive, int exclusive)
        {
            string itemCondition = "";
            if (itemCodes != null && itemCodes.Trim().Length > 0)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    itemCondition += string.Format(@" and TBLONWIPITEM.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and TBLONWIPITEM.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }

            string moCondition = "";
            if (moCodes != null && moCodes.Trim().Length > 0)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    moCondition += string.Format(@" and mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            string mcardCondition = string.Empty;
            if (mcard != null && mcard != string.Empty)
            {
                mcardCondition = string.Format(" and mcard like '{0}%' ", mcard);
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }

                SnCondition += " ) ";
            }
            //ACTIONTYPE = 0 表示集成上料
            string selectColnums = "TBLONWIPITEM.SSCODE || ' - ' || tblss.ssdesc AS SSCODE,TBLONWIPITEM.MTIME,TBLONWIPITEM.OPCODE || ' - ' || tblop.opdesc AS OPCODE, TBLONWIPITEM.RCARD,TBLONWIPITEM.MDATE,";
            selectColnums += " TBLONWIPITEM.ITEMCODE || ' - ' || tblitem.itemdesc AS ITEMCODE, TBLONWIPITEM.RESCODE || ' - ' || tblres.resdesc AS RESCODE,";
            selectColnums += " TBLONWIPITEM.MOCODE, TBLONWIPITEM.MCARD,TBLONWIPITEM.MUSER || ' - ' || TBLUSER.USERNAME AS MUSER ";

            string joinCondition = " LEFT OUTER JOIN TBLUSER ON TBLONWIPITEM.MUSER = TBLUSER.USERCODE ";
            joinCondition += " LEFT OUTER JOIN tblitem ON TBLONWIPITEM.Itemcode=tblitem.itemcode ";
            joinCondition += " LEFT OUTER JOIN tblres ON TBLONWIPITEM.Rescode=tblres.rescode ";
            joinCondition += " LEFT OUTER JOIN tblss ON TBLONWIPITEM.sscode = tblss.sscode ";
            joinCondition += " LEFT OUTER JOIN tblop ON TBLONWIPITEM.OPCODE = tblop.OPCODE ";

            string sql = string.Format(@"select distinct {0} from TBLONWIPITEM {1} where 1=1 /*and ACTIONTYPE = 0*/ and mcardtype='1' {2}{3}{4}{5}",
                selectColnums, joinCondition,
                itemCondition, moCondition, SnCondition, mcardCondition);

#if DEBUG

            Log.Info(new PagerCondition(sql, "mocode,itemcode,rcard", inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(QDOIntegrated), new PagerCondition(sql, "mocode,itemcode,rcard", inclusive, exclusive, true));
        }

        public int QueryIntegratedLoadingCount(
            string itemCodes, string moCodes, string mcard,
            string startSn, string endSn)
        {
            string itemCondition = "";
            if (itemCodes != null && itemCodes.Trim().Length > 0)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    itemCondition += string.Format(@" and TBLONWIPITEM.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and TBLONWIPITEM.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }

            string moCondition = "";
            if (moCodes != null && moCodes.Trim().Length > 0)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    moCondition += string.Format(@" and mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            string mcardCondition = string.Empty;
            if (mcard != null && mcard != string.Empty)
            {
                mcardCondition = string.Format(" and mcard like '{0}%' ", mcard);
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }

                SnCondition += " ) ";
            }
            //ACTIONTYPE = 0 表示集成上料
            /* modified by jessie lee, 2006/1/18,
             * 修改集成上料查询计数不准确的问题 */
            string sql = string.Format(@"select distinct {0} from TBLONWIPITEM where 1=1 /*and ACTIONTYPE = 0*/ and mcardtype='1' {1}{2}{3}{4}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(QDOIntegrated)),
                itemCondition, moCondition, SnCondition, mcardCondition);
            sql = string.Format(" select count(*) from ({0}) ", sql);

#if DEBUG
            Log.Info(new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region 集成下料查询

        public object[] QueryIntegratedDown(
            string itemCodes, string moCodes,
            string startSn, string endSn,
            int inclusive, int exclusive)
        {
            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                moCondition = string.Format(
                    @" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }

                SnCondition += " ) ";
            }
            //ACTIONTYPE = 1 表示集成下料
            string sql = string.Format(@"select distinct {0} from TBLONWIPITEM where 1=1 and ACTIONTYPE = 1 and mcardtype='1' {1}{2}{3}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(QDOIntegrated)),
                itemCondition, moCondition, SnCondition);

#if DEBUG

            Log.Info(new PagerCondition(sql, "mocode,itemcode,rcard", inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(QDOIntegrated), new PagerCondition(sql, "mocode,itemcode,rcard", inclusive, exclusive, true));
        }

        public int QueryIntegratedDownCount(
            string itemCodes, string moCodes,
            string startSn, string endSn)
        {
            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                moCondition = string.Format(
                    @" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }

                SnCondition += " ) ";
            }
            //ACTIONTYPE = 1 表示集成下料
            /* modified by jessie lee, 2006/1/18,
             * 修改集成下料查询计数不准确的问题 */
            string sql = string.Format(@"select {0} from TBLONWIPITEM where 1=1 and ACTIONTYPE = 1 and mcardtype='1' {1}{2}{3}",
                "rcard,rcardseq,mocode,mseq",
                itemCondition, moCondition, SnCondition);
            sql = string.Format(" select count(*) from ({0}) ", sql);

#if DEBUG
            Log.Info(new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        public object[] QueryIntegrated(
            string itemCodes, string moCodes,
            string startSn, string endSn,
            int inclusive, int exclusive)
        {
            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                moCondition = string.Format(
                    @" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }

                SnCondition += " ) ";
            }

            string sql = string.Format(@"select distinct {0} from TBLONWIPITEM where 1=1 and mcardtype='1' {1}{2}{3}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(QDOIntegrated)),
                itemCondition, moCondition, SnCondition);

#if DEBUG

            Log.Info(new PagerCondition(sql, "mocode,itemcode,rcard", inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(QDOIntegrated), new PagerCondition(sql, "mocode,itemcode,rcard", inclusive, exclusive, true));
        }

        public int QueryIntegratedCount(
            string itemCodes, string moCodes,
            string startSn, string endSn)
        {
            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                moCondition = string.Format(
                    @" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }

                SnCondition += " ) ";
            }

            string sql = string.Format(@"select {0} from TBLONWIPITEM where 1=1 and mcardtype='1' {1}{2}{3}",
                "count(mocode)",
                itemCondition, moCondition, SnCondition);

#if DEBUG
            Log.Info(new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryLotDetails(
            string packedNo,
            int inclusive, int exclusive)
        {
            string selectCondition = " TBLONWIPITEM.BIOS,TBLONWIPITEM.RCARD,TBLONWIPITEM.ACTIONTYPE,TBLONWIPITEM.QTY,TBLONWIPITEM.VERSION,";
            selectCondition += " TBLONWIPITEM.SEGCODE,TBLONWIPITEM.VENDORCODE,TBLONWIPITEM.SHIFTTYPECODE,TBLONWIPITEM.PCBA,TBLONWIPITEM.OPCODE || ' - ' || tblop.opdesc AS OPCODE ,";
            selectCondition += " TBLONWIPITEM.ROUTECODE,TBLONWIPITEM.SSCODE,TBLONWIPITEM.LOTNO,TBLONWIPITEM.SHIFTCODE,TBLONWIPITEM.MITEMCODE,TBLONWIPITEM.EATTRIBUTE1,";
            selectCondition += " TBLONWIPITEM.MOCODE,TBLONWIPITEM.DROPOP,TBLONWIPITEM.DROPTIME,TBLONWIPITEM.MODELCODE,TBLONWIPITEM.RESCODE || ' - ' || TBLRES.RESDESC AS RESCODE,";
            selectCondition += " TBLONWIPITEM.MCARD,TBLONWIPITEM.Itemcode || ' - ' || TBLITEM.ITEMDESC AS ITEMCODE,TBLONWIPITEM.MCARDTYPE,TBLONWIPITEM.DROPDATE,";
            selectCondition += " TBLONWIPITEM.MSEQ,TBLONWIPITEM.VENDORITEMCODE,TBLONWIPITEM.TPCODE,TBLONWIPITEM.RCARDSEQ,TBLONWIPITEM.MOSEQ,TRANSSTATUS,";
            selectCondition += " TBLONWIPITEM.MTIME,TBLONWIPITEM.DATECODE,TBLONWIPITEM.MDATE,TBLONWIPITEM.DROPUSER,TBLONWIPITEM.MUSER || ' - ' || tbluser.username AS MUSER";

            string joinCondition = "  LEFT OUTER JOIN TBLITEM ON TBLONWIPITEM.ITEMCODE = TBLITEM.ITEMCODE ";
            joinCondition += " LEFT OUTER JOIN TBLOP ON TBLONWIPITEM.OPCODE = TBLOP.OPCODE";
            joinCondition += " LEFT OUTER JOIN TBLRES ON TBLONWIPITEM.RESCODE = TBLRES.RESCODE";
            joinCondition += " LEFT OUTER JOIN tbluser ON TBLONWIPITEM.MUSER = tbluser.usercode";

#if DEBUG
            Log.Info(new PagerCondition(
                string.Format(
                @"select {0} from TBLONWIPITEM {1} where mcard = '{2}'",
                selectCondition, joinCondition, packedNo.ToUpper()),
                "mcard", inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(OnWIPItem), new PagerCondition(
                string.Format(
                @"select {0} from TBLONWIPITEM {1} where mcard = '{2}'",
                selectCondition, joinCondition, packedNo.ToUpper()),
                "mcard", inclusive, exclusive, true));
        }

        public int QueryLotDetailsCount(
            string packedNo)
        {
#if DEBUG
            Log.Info(new SQLCondition(
                string.Format(
                @"select {0} from TBLONWIPITEM where mcard = '{1}'",
                "count(mcard)", packedNo.ToUpper())).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(
                @"select {0} from TBLONWIPITEM where mcard = '{1}'",
                "count(mcard)", packedNo.ToUpper())));
        }
        #endregion

        #region KeyParts

        #region KeyParts集成上料查询

        public object[] QueryLoadingKeyparts(
            string itemCodes, string moCodes, string mitemcode,
            string startSn, string endSn,
            string startKeyparts, string endKeyparts,
            int inclusive, int exclusive)
        {
            string itemCondition = "";
            if (itemCodes != null && itemCodes.Trim().Length > 0)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    itemCondition += string.Format(@" and TBLONWIPITEM.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and TBLONWIPITEM.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }

            string moCondition = "";
            if (moCodes != null && moCodes.Trim().Length > 0)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    moCondition += string.Format(@" and TBLONWIPITEM.mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and TBLONWIPITEM.mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            //物料料号查询条件
            string mitemcodeCondition = string.Empty;
            if (mitemcode != null && mitemcode != string.Empty)
            {
                mitemcodeCondition = string.Format(" and TBLONWIPITEM.MITEMCODE like '{0}%' ", mitemcode.ToUpper());
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and TBLONWIPITEM.rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("TBLONWIPITEM.rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or TBLONWIPITEM.rcard in ({0}) ", rcardCondition);
                }
                SnCondition += " ) ";
            }

            string KeypartsCondition = string.Empty;
            KeypartsCondition = FormatHelper.GetRCardRangeSql("TBLONWIPITEM.mcard", startKeyparts, endKeyparts);

            string selectCondition = " TBLONWIPITEM.BIOS,TBLONWIPITEM.RCARD,TBLONWIPITEM.ACTIONTYPE,TBLONWIPITEM.QTY,TBLONWIPITEM.VERSION,";
            selectCondition += " TBLONWIPITEM.SEGCODE,TBLONWIPITEM.VENDORCODE,TBLONWIPITEM.SHIFTTYPECODE,TBLONWIPITEM.PCBA,TBLONWIPITEM.OPCODE || ' - ' || tblop.opdesc AS OPCODE ,";
            selectCondition += " TBLONWIPITEM.ROUTECODE,TBLONWIPITEM.SSCODE || ' - ' || tblss.ssdesc AS SSCODE,TBLONWIPITEM.LOTNO,TBLONWIPITEM.SHIFTCODE,TBLONWIPITEM.MITEMCODE,TBLONWIPITEM.EATTRIBUTE1,";
            selectCondition += " TBLONWIPITEM.MOCODE,TBLONWIPITEM.DROPOP,TBLONWIPITEM.DROPTIME,TBLONWIPITEM.MODELCODE,TBLONWIPITEM.RESCODE || ' - ' || TBLRES.RESDESC AS RESCODE,";
            selectCondition += " TBLONWIPITEM.MCARD,TBLONWIPITEM.Itemcode || ' - ' || TBLITEM.ITEMDESC AS ITEMCODE,TBLONWIPITEM.MCARDTYPE,TBLONWIPITEM.DROPDATE,";
            selectCondition += " TBLONWIPITEM.MSEQ,TBLONWIPITEM.VENDORITEMCODE,TBLONWIPITEM.TPCODE,TBLONWIPITEM.RCARDSEQ,TBLONWIPITEM.MOSEQ,TRANSSTATUS,";
            selectCondition += " TBLONWIPITEM.MTIME,TBLONWIPITEM.DATECODE,TBLONWIPITEM.MDATE,TBLONWIPITEM.DROPUSER,TBLONWIPITEM.MUSER || ' - ' || TBLUSER.USERNAME AS MUSER";

            string joinCondition = "  LEFT OUTER JOIN TBLITEM ON TBLONWIPITEM.ITEMCODE = TBLITEM.ITEMCODE ";
            joinCondition += " LEFT OUTER JOIN TBLOP ON TBLONWIPITEM.OPCODE = TBLOP.OPCODE";
            joinCondition += " LEFT OUTER JOIN TBLRES ON TBLONWIPITEM.RESCODE = TBLRES.RESCODE";
            joinCondition += " LEFT OUTER JOIN tblss ON TBLONWIPITEM.sscode = tblss.sscode";
            joinCondition += "  LEFT OUTER JOIN TBLUSER ON TBLONWIPITEM.MUSER = TBLUSER.USERCODE";
            //ACTIONTYPE = 0 表示集成上料
            string sql = string.Format(@"select distinct {0} from TBLONWIPITEM {1} where 1=1 /*and ACTIONTYPE = 0*/ and mcardtype='0'{2}{3}{4}{5} {6}",
                selectCondition, joinCondition,
                itemCondition, moCondition, SnCondition, KeypartsCondition, mitemcodeCondition);
#if DEBUG
            Log.Info(new PagerCondition(
                sql, "mocode,itemcode,rcard",
                inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(QDOKeyparts), new PagerCondition(
                sql, "mocode,itemcode,rcard",
                inclusive, exclusive, true));
        }

        public int QueryLoadingKeypartsCount(
            string itemCodes, string moCodes, string mitemcode,
            string startSn, string endSn,
            string startKeyparts, string endKeyparts)
        {
            string itemCondition = "";
            if (itemCodes != null && itemCodes.Trim().Length > 0)
            {
                if (itemCodes.IndexOf(",") >= 0)
                {
                    string[] lists = itemCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCodes = string.Join(",", lists);
                    itemCondition += string.Format(@" and itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }

            string moCondition = "";
            if (moCodes != null && moCodes.Trim().Length > 0)
            {
                if (moCodes.IndexOf(",") >= 0)
                {
                    string[] lists = moCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCodes = string.Join(",", lists);
                    moCondition += string.Format(@" and mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            //物料料号查询条件
            string mitemcodeCondition = string.Empty;
            if (mitemcode != null && mitemcode != string.Empty)
            {
                mitemcodeCondition = string.Format(" and MITEMCODE like '{0}%' ", mitemcode.ToUpper());
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }
                SnCondition += " ) ";
            }

            string KeypartsCondition = string.Empty;
            KeypartsCondition = FormatHelper.GetRCardRangeSql("mcard", startKeyparts, endKeyparts);
            //ACTIONTYPE = 0 表示集成上料
            string sql = string.Format(@"select {0} from TBLONWIPITEM where 1=1 /*and ACTIONTYPE = 0*/ and mcardtype='0'{1}{2}{3}{4}{5}",
                "count(mocode)",
                itemCondition, moCondition, SnCondition, KeypartsCondition, mitemcodeCondition);

#if DEBUG
            Log.Info(new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(sql));
        }

        #endregion

        #region KeyParts集成下料查询

        public object[] QueryDownKeyparts(
            string itemCodes, string moCodes,
            string startSn, string endSn,
            string startKeyparts, string endKeyparts,
            int inclusive, int exclusive)
        {
            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                moCondition = string.Format(
                    @" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }
                SnCondition += " ) ";
            }

            string KeypartsCondition = string.Empty;
            KeypartsCondition = FormatHelper.GetRCardRangeSql("mcard", startKeyparts, endKeyparts);
            //ACTIONTYPE = 1 表示集成下料
            string sql = string.Format(@"select distinct {0} from TBLONWIPITEM where 1=1 and ACTIONTYPE = 1 and mcardtype='0'{1}{2}{3}{4}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(QDOKeyparts)),
                itemCondition, moCondition, SnCondition, KeypartsCondition);
#if DEBUG
            Log.Info(new PagerCondition(
                sql, "mocode,itemcode,rcard",
                inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(QDOKeyparts), new PagerCondition(
                sql, "mocode,itemcode,rcard",
                inclusive, exclusive, true));
        }

        public int QueryDownKeypartsCount(
            string itemCodes, string moCodes,
            string startSn, string endSn,
            string startKeyparts, string endKeyparts)
        {
            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                moCondition = string.Format(
                    @" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }
                SnCondition += " ) ";
            }

            string KeypartsCondition = string.Empty;
            KeypartsCondition = FormatHelper.GetRCardRangeSql("mcard", startKeyparts, endKeyparts);
            //ACTIONTYPE = 1 表示集成下料
            string sql = string.Format(@"select {0} from TBLONWIPITEM where 1=1 and ACTIONTYPE = 1  and mcardtype='0'{1}{2}{3}{4}",
                "count(mocode)",
                itemCondition, moCondition, SnCondition, KeypartsCondition);

#if DEBUG
            Log.Info(new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(sql));
        }

        #endregion


        public object[] QueryKeyparts(
            string itemCodes, string moCodes,
            string startSn, string endSn,
            string startKeyparts, string endKeyparts,
            int inclusive, int exclusive)
        {
            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                moCondition = string.Format(
                    @" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }
                SnCondition += " ) ";
            }

            string KeypartsCondition = string.Empty;
            KeypartsCondition = FormatHelper.GetRCardRangeSql("mcard", startKeyparts, endKeyparts);

            string sql = string.Format(@"select distinct {0} from TBLONWIPITEM where 1=1 and mcardtype='0'{1}{2}{3}{4}",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(QDOKeyparts)),
                itemCondition, moCondition, SnCondition, KeypartsCondition);
#if DEBUG
            Log.Info(new PagerCondition(
                sql, "mocode,itemcode,rcard",
                inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(QDOKeyparts), new PagerCondition(
                sql, "mocode,itemcode,rcard",
                inclusive, exclusive, true));
        }

        public int QueryKeypartsCount(
            string itemCodes, string moCodes,
            string startSn, string endSn,
            string startKeyparts, string endKeyparts)
        {
            string itemCondition = "";
            if (itemCodes != "" && itemCodes != null)
            {
                itemCondition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCodes));
            }

            string moCondition = "";
            if (moCodes != "" && moCodes != null)
            {
                moCondition = string.Format(
                    @" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCodes));
            }

            string SnCondition = string.Empty;
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            if (string.Compare(startSn, endSn, true) == 0 && startSn != string.Empty)
            {
                ArrayList array = new ArrayList();
                array.Add(startSn);
                castDownHelper.GetAllRCard(ref array, startSn);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                SnCondition = string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
            }
            else if (string.Compare(startSn, endSn, true) != 0)
            {
                ArrayList array = new ArrayList();
                castDownHelper.BuildProcessRcardCondition(ref array, startSn.ToUpper(), endSn.ToUpper());
                string rcardCondition = string.Empty;
                if (array.Count > 0)
                {
                    for (int i = 0; i < array.Count; i++)
                    {
                        if (i < array.Count - 1)
                        {
                            rcardCondition += array[i].ToString() + " union ";
                        }
                        else
                        {
                            rcardCondition += array[i].ToString();
                        }
                    }
                }
                SnCondition = "and ( 1=1 " + FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

                if (rcardCondition != string.Empty)
                {
                    SnCondition += string.Format(" or rcard in ({0}) ", rcardCondition);
                }
                SnCondition += " ) ";
            }

            string KeypartsCondition = string.Empty;
            KeypartsCondition = FormatHelper.GetRCardRangeSql("mcard", startKeyparts, endKeyparts);

            string sql = string.Format(@"select {0} from TBLONWIPITEM where 1=1 and mcardtype='0'{1}{2}{3}{4}",
                                "count(mocode)",
                                itemCondition, moCondition, SnCondition, KeypartsCondition);

#if DEBUG
            Log.Info(new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(sql));
        }

        public object[] QueryKeypartsDetails(
            string keyparts,
            int inclusive, int exclusive)
        {
#if DEBUG
            Log.Info(
                new PagerCondition(
                string.Format(@"select {0} from TBLMKEYPART where mitemcode = '{1}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)), keyparts.ToUpper()
                ), "mitemcode", inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(MKeyPart), new PagerCondition(
                string.Format(@"select {0} from TBLMKEYPART where mitemcode = '{1}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(MKeyPart)), keyparts.ToUpper()
                ), "mitemcode", inclusive, exclusive, true));
        }

        public int QueryKeypartsDetailsCount(
            string keyparts)
        {
#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(@"select {0} from TBLMKEYPART where mitemcode = '{1}'",
                "count(mitemcode)", keyparts.ToUpper()
                )).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(@"select {0} from TBLMKEYPART where mitemcode = '{1}'",
                "count(mitemcode)", keyparts.ToUpper())));
        }

        /// <summary>
        /// addedy by jessie lee,2005/9/16
        /// </summary>
        /// <param name="keyparts">keyparts序列号</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryKeypartsListDetails(
            string keyparts,
            int inclusive, int exclusive)
        {
            string selectColunms = FormatHelper.GetAllFieldWithDesc(typeof(OnWIPItem), "tblonwipitem", new string[] { "muser" }, new string[] { "tbluser.username" });

            string sql = "select " + selectColunms + " from tblonwipitem left outer join tbluser on tblonwipitem.muser=tbluser.usercode where mcard='" + keyparts + "'";

#if DEBUG
            Log.Info(
                new PagerCondition(sql, "mitemcode", inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(OnWIPItem), new PagerCondition(sql, "mitemcode", inclusive, exclusive, true));
        }

        /// <summary>
        /// added by jessie lee,2005/9/16
        /// </summary>
        /// <param name="keyparts">keyparts序列号</param>
        /// <returns></returns>
        public int QueryKeypartsListDetailsCount(
            string keyparts)
        {
#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(@"select {0} from tblonwipitem where mcard = '{1}'",
                "count(mcard)", keyparts.ToUpper()
                )).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(@"select {0} from tblonwipitem where mcard = '{1}'",
                "count(mcard)", keyparts.ToUpper())));
        }
        #endregion
    }
}
