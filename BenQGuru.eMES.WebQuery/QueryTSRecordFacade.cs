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
    /// QueryTSRecordFacade 的摘要说明。
    /// </summary>
    public class QueryTSRecordFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public QueryTSRecordFacade(IDomainDataProvider domainDataProvider)
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

        // Added By Hi1/Venus.Feng on 20081104 for Hisense Version :
        private string GenerateSQLForTSRecordQuery(
            string itemType, string itemCode, string moCode,
            string snStart, string snEnd, string reworkCode,
            string segmentCode, string ssCode, string opCode,
            string bigSSCode, string mModelCode, string mMachineType,
            int startDate, int endDate, ArrayList tsState)
        {
            string strSql = "";
            strSql += "SELECT DISTINCT a.rcard, a.rcardseq, a.tsstatus, a.mocode, a.modelcode, a.itemcode || ' - ' || b.mdesc as itemcode,";
            strSql += "       a.tsdate, a.tstime, a.tstimes, a.frmrescode, a.frmdate, a.frmtime,";
            strSql += "       a.tsrescode || ' - ' || e.resdesc AS rrescode, a.tsrepairmdate AS rdate,";
            strSql += "       a.tsrepairmtime AS rtime, a.refrescode, a.refopcode || ' - ' || f.opdesc as refopcode, a.frmuser || ' - ' || d.username as frmuser,";
            strSql += "       a.tsuser, a.crescode, a.confirmuser, a.confirmdate, a.confirmtime,";
            strSql += "       a.tsid, a.frmmemo, a.scrapcause, b.mmodelcode AS modeltype, b.mdesc, ";
            strSql += "       c.mobom AS bomversion";
            strSql += "  FROM tblts a, tblmaterial b, tblmo c,tbluser d,tblres e,tblop f,tblss  g";
            strSql += " WHERE a.mocode = c.mocode AND a.itemcode = b.mcode and a.frmuser=d.usercode(+) and a.tsrescode=e.rescode(+) and a.refopcode=f.opcode(+)";
            strSql += " AND E.SSCODE = G.SSCODE(+)  AND E.SEGCODE = G.SEGCODE(+) AND E.SHIFTTYPECODE = G.SHIFTTYPECODE(+)";
            if (!string.IsNullOrEmpty(itemType) && itemType.Trim().Length > 0)
            {
                strSql += " AND b.mtype='" + itemType + "' ";
            }

            if (!string.IsNullOrEmpty(itemCode) && itemCode.Trim().Length > 0)
            {
                if (itemCode.IndexOf(",") >= 0)
                {
                    string[] lists = itemCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCode = string.Join(",", lists);
                    strSql += string.Format(" and a.itemcode in ({0})", itemCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" and a.itemcode like '{0}%'", itemCode.ToUpper());
                }
            }

            if (!string.IsNullOrEmpty(moCode) && moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    strSql += string.Format(" and a.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" and a.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            strSql += FormatHelper.GetRCardRangeSql("a.rcard", snStart.ToUpper(), snEnd.ToUpper());

            if (!string.IsNullOrEmpty(reworkCode) && reworkCode.Trim().Length > 0)
            {
                string reworkCodeTrain = reworkCode;
                if (reworkCode.IndexOf(",") >= 0)
                {
                    string[] lists = reworkCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    reworkCode = string.Join(",", lists);
                    strSql += string.Format(" AND a.rcard IN (SELECT DISTINCT rcard FROM tblreworkrange WHERE reworkcode IN ({0}))", reworkCode.ToUpper());

                }
                else
                {
                    strSql += string.Format(" AND a.rcard IN (SELECT DISTINCT rcard FROM tblreworkrange WHERE reworkcode  like '{0}%')", reworkCode.ToUpper());
                }

                //strSql += " AND a.rcard IN (SELECT DISTINCT rcard FROM tblreworkrange WHERE reworkcode IN (" + FormatHelper.ProcessQueryValues(reworkCode) + ") ) ";
                strSql += this.GetReworkSheetDateRange(reworkCodeTrain);
            }

            if (!string.IsNullOrEmpty(segmentCode) && segmentCode.Trim().Length > 0)
            {
                if (segmentCode.IndexOf(",") >= 0)
                {
                    string[] lists = segmentCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    segmentCode = string.Join(",", lists);
                    strSql += string.Format(" AND a.frmsegcode in  ({0})", segmentCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND a.frmsegcode like '{0}%'", segmentCode.ToUpper());
                }
            }

            if (!string.IsNullOrEmpty(ssCode) && ssCode.Trim().Length > 0)
            {
                if (ssCode.IndexOf(",") >= 0)
                {
                    string[] lists = ssCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    ssCode = string.Join(",", lists);
                    strSql += string.Format(" AND a.frmsscode in  ({0})", ssCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND a.frmsscode like '{0}%'", ssCode.ToUpper());
                }
            }

            if (!string.IsNullOrEmpty(opCode) && opCode.Trim().Length > 0)
            {
                if (opCode.IndexOf(",") >= 0)
                {
                    string[] lists = opCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    opCode = string.Join(",", lists);
                    strSql += string.Format(" AND a.frmopcode in  ({0})", opCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND a.frmopcode like '{0}%'", opCode.ToUpper());
                }
            }

            if (!string.IsNullOrEmpty(bigSSCode) && bigSSCode.Trim().Length > 0)
            {
                if (bigSSCode.IndexOf(",") >= 0)
                {
                    string[] lists = bigSSCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigSSCode = string.Join(",", lists);
                    strSql += string.Format(" AND g.bigsscode in  ({0})", bigSSCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND g.bigsscode like '{0}%'", bigSSCode.ToUpper());
                }
            }


            if (!string.IsNullOrEmpty(mModelCode) && mModelCode.Trim().Length > 0)
            {
                if (mModelCode.IndexOf(",") >= 0)
                {
                    string[] lists = mModelCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mModelCode = string.Join(",", lists);
                    strSql += string.Format(" AND b.mmodelcode in  ({0})", mModelCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND b.mmodelcode like '{0}%'", mModelCode.ToUpper());
                }
            }


            if (!string.IsNullOrEmpty(mMachineType) && mMachineType.Trim().Length > 0)
            {
                if (mMachineType.IndexOf(",") >= 0)
                {
                    string[] lists = mMachineType.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mMachineType = string.Join(",", lists);
                    strSql += string.Format(" AND b.mmachinetype in  ({0})", mMachineType.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND b.mmachinetype like '{0}%'", mMachineType.ToUpper());
                }
            }

            if (startDate != DefaultDateTime.DefaultToInt)
            {
                strSql += FormatHelper.GetDateRangeSql("a.shiftday", startDate, endDate);
            }

            if (tsState != null && tsState.Count > 0)
            {
                string statusList = "";
                foreach (string status in tsState)
                {
                    statusList += status + ",";
                }
                if (statusList.IndexOf(TSStatus.TSStatus_Complete) >= 0)
                {
                    statusList += TSStatus.TSStatus_Reflow + ",";
                }

                if (statusList.Length > 0)
                {
                    statusList = statusList.Substring(0, statusList.Length - 1);
                    statusList = statusList.Replace(",", "','");

                    strSql += " AND a.tsstatus IN ('" + statusList + "') ";
                }
            }
            else
            {
                strSql += " AND a.tsstatus IN ('" + TSStatus.TSStatus_Complete + "','"
                                                  + TSStatus.TSStatus_Reflow + "','"
                                                  + TSStatus.TSStatus_Confirm + "','"
                                                  + TSStatus.TSStatus_New + "','"
                                                  + TSStatus.TSStatus_Scrap + "','"
                                                  + TSStatus.TSStatus_Split + "','"
                                                  + TSStatus.TSStatus_TS + "') ";
            }

            string selectColunms1 = "A.RCARD,A.RCARDSEQ,A.TSSTATUS,A.MOCODE,A.MODELCODE,A.ITEMCODE,A.TSDATE,A.TSTIME,A.TSTIMES,A.FRMRESCODE,A.FRMDATE,";
            selectColunms1 += "A.FRMTIME,A.RRESCODE,A.RDATE,A.RTIME,A.REFRESCODE,A.REFOPCODE,A.FRMUSER,A.CRESCODE,A.CONFIRMDATE,A.CONFIRMTIME,";
            selectColunms1 += "A.TSID,A.FRMMEMO,A.SCRAPCAUSE,A.MODELTYPE,A.MDESC,A.BOMVERSION";


            string linkSql1 = FormatHelper.GetLinkTableSQL("A", "CONFIRMUSER", "tbluser", "usercode");
            string linkSql2 = FormatHelper.GetLinkTableSQL("A", "TSUSER", "tbluser", "usercode");

            strSql = "select " + selectColunms1 + " ,A.confirmuser || ' - ' || tbluser.username AS confirmuser,A.tsuser  from (" + strSql + ") A  " + linkSql1 + "";
            strSql = "select " + selectColunms1 + " ,A.tsuser || ' - ' || tbluser.username AS tsuser,A.confirmuser from (" + strSql + ") A  " + linkSql2 + "";

            string selectColunms2 = "A.RCARD,A.RCARDSEQ,A.TSSTATUS,A.MOCODE,A.MODELCODE,A.ITEMCODE,A.TSDATE,A.TSTIME,A.TSTIMES,A.FRMDATE,";
            selectColunms2 += "A.FRMTIME,A.RRESCODE,A.RDATE,A.RTIME,A.REFOPCODE,A.FRMUSER,A.CONFIRMDATE,A.CONFIRMTIME,";
            selectColunms2 += "A.TSID,A.FRMMEMO,A.SCRAPCAUSE,A.MODELTYPE,A.MDESC,A.BOMVERSION,A.confirmuser,A.tsuser ";

            string linkSql3 = FormatHelper.GetLinkTableSQL("A", "REFRESCODE", "tblres", "rescode");
            string linkSql4 = FormatHelper.GetLinkTableSQL("A", "CRESCODE", "tblres", "rescode");
            string linkSql5 = FormatHelper.GetLinkTableSQL("A", "FRMRESCODE", "tblres", "rescode");

            strSql = "select " + selectColunms2 + " ,A.refrescode || ' - ' || tblres.resdesc AS refrescode,A.CRESCODE,A.FRMRESCODE  from (" + strSql + ") A  " + linkSql3 + "";
            strSql = "select " + selectColunms2 + " ,A.crescode || ' - ' || tblres.resdesc AS crescode,A.refrescode,A.FRMRESCODE from (" + strSql + ") A  " + linkSql4 + "";
            strSql = "select " + selectColunms2 + " ,A.frmrescode || ' - ' || tblres.resdesc AS frmrescode,A.REFRESCODE,A.CRESCODE from (" + strSql + ") A  " + linkSql5 + "";

            return strSql;
        }

        public string GetExceptSQL(string itemType, string itemCode, string moCode,
            string snStart, string snEnd, string reworkCode,
            string segmentCode, string ssCode, string opCode,
            string bigSSCode, string mModelCode, string mMachineType,
            int startDate, int endDate, ArrayList tsState)
        {
            string strSql = "";
            strSql += "SELECT DISTINCT a.rcard, a.rcardseq, a.tsstatus, a.mocode, a.modelcode, a.itemcode || ' - ' || b.mdesc as itemcode,B.MNAME AS ITEMNAME,";
            strSql += "       a.tsdate, a.tstime, a.tstimes, a.frmrescode, a.frmdate, a.frmtime,";
            strSql += "       a.tsrescode || ' - ' || e.resdesc AS rrescode, a.tsrepairmdate AS rdate,";
            strSql += "       a.tsrepairmtime AS rtime, a.refrescode, a.refopcode || ' - ' || f.opdesc as refopcode, a.frmuser || ' - ' || d.username as frmuser,";
            strSql += "       a.tsuser, a.crescode, a.confirmuser, a.confirmdate, a.confirmtime,";
            strSql += "       a.tsid, a.frmmemo, a.scrapcause, b.mmodelcode AS modeltype, b.mdesc, ";
            strSql += "       c.mobom AS bomversion";
            strSql += "  FROM tblts a, tblmaterial b, tblmo c,tbluser d,tblres e,tblop f,tblss  g";
            strSql += " WHERE a.mocode = c.mocode AND a.itemcode = b.mcode and a.frmuser=d.usercode(+) and a.tsrescode=e.rescode(+) and a.refopcode=f.opcode(+)";
            strSql += " AND E.SSCODE = G.SSCODE(+)  AND E.SEGCODE = G.SEGCODE(+) AND E.SHIFTTYPECODE = G.SHIFTTYPECODE(+)";
            if (!string.IsNullOrEmpty(itemType) && itemType.Trim().Length > 0)
            {
                strSql += " AND b.mtype='" + itemType + "' ";
            }

            if (!string.IsNullOrEmpty(itemCode) && itemCode.Trim().Length > 0)
            {
                if (itemCode.IndexOf(",") >= 0)
                {
                    string[] lists = itemCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCode = string.Join(",", lists);
                    strSql += string.Format(" and a.itemcode in ({0})", itemCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" and a.itemcode like '{0}%'", itemCode.ToUpper());
                }
            }

            if (!string.IsNullOrEmpty(moCode) && moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    strSql += string.Format(" and a.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" and a.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            strSql += FormatHelper.GetRCardRangeSql("a.rcard", snStart.ToUpper(), snEnd.ToUpper());

            if (!string.IsNullOrEmpty(reworkCode) && reworkCode.Trim().Length > 0)
            {
                string reworkCodeTrain = reworkCode;
                if (reworkCode.IndexOf(",") >= 0)
                {
                    string[] lists = reworkCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    reworkCode = string.Join(",", lists);
                    strSql += string.Format(" AND a.rcard IN (SELECT DISTINCT rcard FROM tblreworkrange WHERE reworkcode IN ({0}))", reworkCode.ToUpper());

                }
                else
                {
                    strSql += string.Format(" AND a.rcard IN (SELECT DISTINCT rcard FROM tblreworkrange WHERE reworkcode  like '{0}%')", reworkCode.ToUpper());
                }

                //strSql += " AND a.rcard IN (SELECT DISTINCT rcard FROM tblreworkrange WHERE reworkcode IN (" + FormatHelper.ProcessQueryValues(reworkCode) + ") ) ";
                strSql += this.GetReworkSheetDateRange(reworkCodeTrain);
            }

            if (!string.IsNullOrEmpty(segmentCode) && segmentCode.Trim().Length > 0)
            {
                if (segmentCode.IndexOf(",") >= 0)
                {
                    string[] lists = segmentCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    segmentCode = string.Join(",", lists);
                    strSql += string.Format(" AND a.frmsegcode in  ({0})", segmentCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND a.frmsegcode like '{0}%'", segmentCode.ToUpper());
                }
            }

            if (!string.IsNullOrEmpty(ssCode) && ssCode.Trim().Length > 0)
            {
                if (ssCode.IndexOf(",") >= 0)
                {
                    string[] lists = ssCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    ssCode = string.Join(",", lists);
                    strSql += string.Format(" AND a.frmsscode in  ({0})", ssCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND a.frmsscode like '{0}%'", ssCode.ToUpper());
                }
            }

            if (!string.IsNullOrEmpty(opCode) && opCode.Trim().Length > 0)
            {
                if (opCode.IndexOf(",") >= 0)
                {
                    string[] lists = opCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    opCode = string.Join(",", lists);
                    strSql += string.Format(" AND a.frmopcode in  ({0})", opCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND a.frmopcode like '{0}%'", opCode.ToUpper());
                }
            }

            if (!string.IsNullOrEmpty(bigSSCode) && bigSSCode.Trim().Length > 0)
            {
                if (bigSSCode.IndexOf(",") >= 0)
                {
                    string[] lists = bigSSCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigSSCode = string.Join(",", lists);
                    strSql += string.Format(" AND g.bigsscode in  ({0})", bigSSCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND g.bigsscode like '{0}%'", bigSSCode.ToUpper());
                }
            }


            if (!string.IsNullOrEmpty(mModelCode) && mModelCode.Trim().Length > 0)
            {
                if (mModelCode.IndexOf(",") >= 0)
                {
                    string[] lists = mModelCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mModelCode = string.Join(",", lists);
                    strSql += string.Format(" AND b.mmodelcode in  ({0})", mModelCode.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND b.mmodelcode like '{0}%'", mModelCode.ToUpper());
                }
            }


            if (!string.IsNullOrEmpty(mMachineType) && mMachineType.Trim().Length > 0)
            {
                if (mMachineType.IndexOf(",") >= 0)
                {
                    string[] lists = mMachineType.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mMachineType = string.Join(",", lists);
                    strSql += string.Format(" AND b.mmachinetype in  ({0})", mMachineType.ToUpper());
                }
                else
                {
                    strSql += string.Format(" AND b.mmachinetype like '{0}%'", mMachineType.ToUpper());
                }
            }

            if (startDate != DefaultDateTime.DefaultToInt)
            {
                strSql += FormatHelper.GetDateRangeSql("a.shiftday", startDate, endDate);
            }

            if (tsState != null && tsState.Count > 0)
            {
                string statusList = "";
                foreach (string status in tsState)
                {
                    statusList += status + ",";
                }
                if (statusList.IndexOf(TSStatus.TSStatus_Complete) >= 0)
                {
                    statusList += TSStatus.TSStatus_Reflow + ",";
                }

                if (statusList.Length > 0)
                {
                    statusList = statusList.Substring(0, statusList.Length - 1);
                    statusList = statusList.Replace(",", "','");

                    strSql += " AND a.tsstatus IN ('" + statusList + "') ";
                }
            }
            else
            {
                strSql += " AND a.tsstatus IN ('" + TSStatus.TSStatus_Complete + "','"
                                                  + TSStatus.TSStatus_Reflow + "','"
                                                  + TSStatus.TSStatus_Confirm + "','"
                                                  + TSStatus.TSStatus_New + "','"
                                                  + TSStatus.TSStatus_Scrap + "','"
                                                  + TSStatus.TSStatus_Split + "','"
                                                  + TSStatus.TSStatus_TS + "') ";
            }

            string selectColunms1 = "A.RCARD,A.RCARDSEQ,A.TSSTATUS,A.MOCODE,A.MODELCODE,A.ITEMCODE,A.ITEMNAME,A.TSDATE,A.TSTIME,A.TSTIMES,A.FRMRESCODE,A.FRMDATE,";
            selectColunms1 += "A.FRMTIME,A.RRESCODE,A.RDATE,A.RTIME,A.REFRESCODE,A.REFOPCODE,A.FRMUSER,A.CRESCODE,A.CONFIRMDATE,A.CONFIRMTIME,";
            selectColunms1 += "A.TSID,A.FRMMEMO,A.SCRAPCAUSE,A.MODELTYPE,A.MDESC,A.BOMVERSION";


            string linkSql1 = FormatHelper.GetLinkTableSQL("A", "CONFIRMUSER", "tbluser", "usercode");
            string linkSql2 = FormatHelper.GetLinkTableSQL("A", "TSUSER", "tbluser", "usercode");

            strSql = "select " + selectColunms1 + " ,A.confirmuser || ' - ' || tbluser.username AS confirmuser,A.tsuser  from (" + strSql + ") A  " + linkSql1 + "";
            strSql = "select " + selectColunms1 + " ,A.tsuser || ' - ' || tbluser.username AS tsuser,A.confirmuser from (" + strSql + ") A  " + linkSql2 + "";

            string selectColunms2 = "A.RCARD,A.RCARDSEQ,A.TSSTATUS,A.MOCODE,A.MODELCODE,A.ITEMCODE,A.ITEMNAME,A.TSDATE,A.TSTIME,A.TSTIMES,A.FRMDATE,";
            selectColunms2 += "A.FRMTIME,A.RRESCODE,A.RDATE,A.RTIME,A.REFOPCODE,A.FRMUSER,A.CONFIRMDATE,A.CONFIRMTIME,";
            selectColunms2 += "A.TSID,A.FRMMEMO,A.SCRAPCAUSE,A.MODELTYPE,A.MDESC,A.BOMVERSION,A.confirmuser,A.tsuser ";

            string linkSql3 = FormatHelper.GetLinkTableSQL("A", "REFRESCODE", "tblres", "rescode");
            string linkSql4 = FormatHelper.GetLinkTableSQL("A", "CRESCODE", "tblres", "rescode");
            string linkSql5 = FormatHelper.GetLinkTableSQL("A", "FRMRESCODE", "tblres", "rescode");

            strSql = "select " + selectColunms2 + " ,A.refrescode || ' - ' || tblres.resdesc AS refrescode,A.CRESCODE,A.FRMRESCODE  from (" + strSql + ") A  " + linkSql3 + "";
            strSql = "select " + selectColunms2 + " ,A.crescode || ' - ' || tblres.resdesc AS crescode,A.refrescode,A.FRMRESCODE from (" + strSql + ") A  " + linkSql4 + "";
            string retunStrSql = "select DISTINCT " + selectColunms2 + " ,A.frmrescode || ' - ' || tblres.resdesc AS frmrescode,A.REFRESCODE,A.CRESCODE, B.ECGCODE,";
            retunStrSql += " C.ECGDESC,B.ECODE,D.ECDESC,E.ECSCODE,F.ECSDESC,E.SOLCODE,G.SOLDESC,E.SOLMEMO,E.DUTYCODE,H.DUTYDESC,L.Epart,K.Eloc,A.TSDATE AS RealTSDATE, A.TSTIME AS RealTSTIME, A.TSUSER AS TSOperator from (" + strSql + ") A  " + linkSql5 + "";
            retunStrSql += "   LEFT JOIN TBLTSERRORCODE B ON A.TSID = B.TSID  INNER JOIN TBLECG C ON B.ECGCODE = C.ECGCODE ";
            retunStrSql += "  INNER JOIN TBLEC D ON B.ECODE = D.ECODE  LEFT JOIN TBLTSERRORCAUSE E ON A.TSID = E.TSID";
            retunStrSql += "         AND B.ECODE = E.ECODE      AND B.ECGCODE = E.ECGCODE  LEFT JOIN TBLECS F ON E.ECSCODE = F.ECSCODE";
            retunStrSql += "   LEFT JOIN TBLSOLUTION G ON E.SOLCODE = G.SOLCODE  LEFT JOIN TBLDUTY H ON E.DUTYCODE = H.DUTYCODE";
            retunStrSql += "   LEFT JOIN TBLTSERRORCAUSE2EPART L ON A.TSID = L.TSID     AND B.ECODE = L.ECODE";
            retunStrSql += "  LEFT JOIN TBLTSERRORCAUSE2LOC K ON A.TSID = K.TSID     AND B.ECODE = K.ECODE";
            retunStrSql += " order by a.RCARD ";
            return retunStrSql;

        }

        public object[] QueryTSRecord_New(
            string itemType, string itemCode, string moCode,
            string snStart, string snEnd, string reworkCode,
            string segmentCode, string ssCode, string opCode,
            string bigSSCode, string mModelCode, string mMachineType,
            int startDate, int endDate, ArrayList tsState,
            int inclusive, int exclusive)
        {
            string strSql = this.GenerateSQLForTSRecordQuery(itemType, itemCode, moCode,
                snStart, snEnd, reworkCode,
                segmentCode, ssCode, opCode,
                bigSSCode, mModelCode, mMachineType,
                startDate, endDate, tsState);

            return this.DataProvider.CustomQuery(typeof(QDOTSRecord), new PagerCondition(strSql, "rcard", inclusive, exclusive, true));
        }

        public int QueryTSRecordCount_New(
            string itemType, string itemCode, string moCode,
            string snStart, string snEnd, string reworkCode,
            string segmentCode, string ssCode, string opCode,
            string bigSSCode, string mModelCode, string mMachineType,
            int startDate, int endDate, ArrayList tsState)
        {
            string strSql = this.GenerateSQLForTSRecordQuery(itemType, itemCode, moCode,
                snStart, snEnd, reworkCode,
                segmentCode, ssCode, opCode,
                bigSSCode, mModelCode, mMachineType,
                startDate, endDate, tsState);

            strSql = "SELECT COUNT(*) FROM (" + strSql + ")";

            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }

        public object[] QueryExportTSRecord_New(
            string itemType, string itemCode, string moCode,
            string snStart, string snEnd, string reworkCode,
            string segmentCode, string ssCode, string opCode,
            string bigSSCode, string mModelCode, string mMachineType,
            int startDate, int endDate, ArrayList tsState,
            int inclusive, int exclusive)
        {
            //获取ts主档
            //object[] tsObjs = this.QueryTSRecord_New(itemType, itemCode, moCode,
            //    snStart, snEnd, reworkCode,
            //    segmentCode, ssCode, opCode,
            //    bigSSCode, mModelCode, mMachineType,
            //    startDate, endDate, tsState,
            //    inclusive, exclusive);

            string strSql = GetExceptSQL(itemType, itemCode, moCode,
                snStart, snEnd, reworkCode,
                segmentCode, ssCode, opCode,
                bigSSCode, mModelCode, mMachineType,
                startDate, endDate, tsState);

            object[] tsobjects = this.DataProvider.CustomQuery(typeof(ExportQDOTSDetails1), new SQLCondition(strSql));

            return tsobjects;
            //ArrayList returnObjs = new ArrayList();

            //ArrayList tsidList = new ArrayList();
            //foreach (QDOTSRecord qts in tsObjs)
            //{
            //    tsidList.Add(qts.TSID);
            //}

            //string tsIDs = "('" + String.Join("','", (string[])tsidList.ToArray(typeof(string))) + "')";

            //if (tsObjs != null && tsObjs.Length > 0)
            //{
            //    QueryTSDetailsFacade tsDetailFacade = new QueryTSDetailsFacade(this.DataProvider);
            //    //				
            //    object[] tsDetailObjs = tsDetailFacade.QueryTSDetailsByID(tsIDs, 0, int.MaxValue);
            //    if (tsDetailObjs != null && tsDetailObjs.Length > 0)
            //    {
            //        foreach (QDOTSRecord _ts in tsObjs)
            //        {
            //            foreach (QDOTSDetails1 tsdetail in tsDetailObjs)
            //            {
            //                if (tsdetail.TSID == _ts.TSID)
            //                {
            //                    ExportQDOTSDetails1 exportTSDetails = new ExportQDOTSDetails1();
            //                    //映射明细
            //                    exportTSDetails.Memo = tsdetail.Memo;
            //                    exportTSDetails.TSOperator = tsdetail.TSOperator;
            //                    exportTSDetails.TSDate = tsdetail.TSDate;
            //                    exportTSDetails.TSTime = tsdetail.TSTime;
            //                    exportTSDetails.ErrorCodeGroup = tsdetail.ErrorCodeGroup;
            //                    exportTSDetails.ErrorCodeGroupDescription = tsdetail.ErrorCodeGroupDescription;
            //                    exportTSDetails.ErrorCode = tsdetail.ErrorCode;
            //                    exportTSDetails.ErrorCodeDescription = tsdetail.ErrorCodeDescription;
            //                    exportTSDetails.ErrorCauseCode = tsdetail.ErrorCauseCode;
            //                    exportTSDetails.ErrorCauseDescription = tsdetail.ErrorCauseDescription;
            //                    exportTSDetails.ErrorLocation = tsdetail.ErrorLocation;
            //                    exportTSDetails.ErrorParts = tsdetail.ErrorParts;
            //                    exportTSDetails.Solution = tsdetail.SolutionCode;
            //                    exportTSDetails.Duty = tsdetail.Duty;
            //                    exportTSDetails.DutyDescription = tsdetail.DutyDescription;
            //                    exportTSDetails.SolutionDescription = tsdetail.SolutionDescription;
            //                    exportTSDetails.TSMemo = _ts.TSMemo;
            //                    //映射主档
            //                    exportTSDetails.SN = _ts.SN;
            //                    exportTSDetails.TsState = _ts.TsState;
            //                    exportTSDetails.MoCode = _ts.MoCode;
            //                    exportTSDetails.ModelCode = _ts.ModelCode;
            //                    exportTSDetails.ItemCode = _ts.ItemCode;
            //                    exportTSDetails.ModelType = _ts.ModelType;
            //                    exportTSDetails.BOMVersion = _ts.BOMVersion;
            //                    exportTSDetails.NGCount = _ts.NGCount;
            //                    exportTSDetails.NGDate = _ts.NGDate;
            //                    exportTSDetails.NGTime = _ts.NGTime;
            //                    exportTSDetails.SourceResource = _ts.SourceResource;
            //                    exportTSDetails.SourceResourceDate = _ts.SourceResourceDate;
            //                    exportTSDetails.SourceResourceTime = _ts.SourceResourceTime;
            //                    exportTSDetails.RepaireResource = _ts.RepaireResource;
            //                    exportTSDetails.RepaireDate = _ts.RepaireDate;
            //                    exportTSDetails.RepaireTime = _ts.RepaireTime;
            //                    exportTSDetails.DestResource = _ts.DestResource;
            //                    exportTSDetails.DestOpCode = _ts.DestOpCode;
            //                    exportTSDetails.FrmUser = _ts.FrmUser;
            //                    exportTSDetails.TSUser = _ts.TSUser;
            //                    exportTSDetails.ConfirmResource = _ts.ConfirmResource;
            //                    exportTSDetails.ConfirmUser = _ts.ConfirmUser;
            //                    exportTSDetails.ConfirmDate = _ts.ConfirmDate;
            //                    exportTSDetails.ConfiemTime = _ts.ConfiemTime;
            //                    exportTSDetails.DestResourceDate = _ts.DestResourceDate;
            //                    exportTSDetails.DestResourceTime = _ts.DestResourceTime;

            //                    returnObjs.Add(exportTSDetails);
            //                }
            //            }
            //        }

            //    }
            //}
            //return (ExportQDOTSDetails1[])returnObjs.ToArray(typeof(ExportQDOTSDetails1));
        }
        // End Added

        public object[] QueryTSRecord(
            string modelCode, string itemCode,
            string moCode, string reworkMOCode,
            string snStart, string snEnd,
            int startDate, int startTime,
            int endDate, int endTime,
            int receiveStartDate, int receiveStartTime,
            int receiveEndDate, int receiveEndTime,
            int TSstartDate, int TSstartTime,
            int TSendDate, int TSendTime,
            ArrayList tsState, string tsResources, string fromResources, string confirmResource,
            int inclusive, int exclusive)
        {
            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
            }

            string itemCondition = "";
            if (itemCode != "" && itemCode != null)
            {
                itemCondition += string.Format(@" and a.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            string modelCondition = "";
            if (modelCode != "" && modelCode != null)
            {
                modelCondition += string.Format(@" and a.modelcode in ({0}) ", FormatHelper.ProcessQueryValues(modelCode));
            }

            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("a.rcard", snStart.ToUpper(), snEnd.ToUpper());

            string dateCondition = string.Empty;
            string receivedateCondition = string.Empty;
            string TSdateCondition1 = string.Empty;
            string TSdateCondition2 = string.Empty;
            if (startDate != DefaultDateTime.DefaultToInt)
            {
                //不良日期	tblts.mdate 按送修时间查询
                //dateCondition = FormatHelper.GetDateRangeSql("a.FRMDATE","a.FRMTIME",startDate,startTime,endDate,endTime);
                dateCondition = FormatHelper.GetDateRangeSql("a.FRMDATE", startDate, endDate);
            }
            else if (receiveStartDate != DefaultDateTime.DefaultToInt)
            {
                //接收日期	tblts.CONFIRMDATE
                //receivedateCondition = FormatHelper.GetDateRangeSql("a.CONFIRMDATE","a.CONFIRMTIME",receiveStartDate,receiveStartTime,receiveEndDate,receiveEndTime);
                receivedateCondition = FormatHelper.GetDateRangeSql("a.CONFIRMDATE", receiveStartDate, receiveEndDate);
            }
            else if (TSstartDate != DefaultDateTime.DefaultToInt)
            {
                //维修日期 	tbltserrorcause.mdate 和  tblts.tsdate
                //TSdateCondition1 = FormatHelper.GetDateRangeSql("b.mdate", "b.mtime", TSstartDate, TSstartTime, TSendDate, TSendTime);
                TSdateCondition1 = FormatHelper.GetDateRangeSql("a.tsrepairmdate", TSstartDate, TSendDate);
                //TSdateCondition2 = FormatHelper.GetDateRangeSql("a.tsdate", "a.tstime", TSstartDate, TSstartTime, TSendDate, TSendTime);
                //TSdateCondition2 = FormatHelper.GetDateRangeSql("a.tsdate", TSstartDate, TSendDate);
            }

            //返工工单条件
            string reworkCondition = string.Empty;
            if (reworkMOCode != "" && reworkMOCode != null)
            {
                reworkCondition = string.Format(" and a.rcard in ( select rcard from tblreworkrange where reworkcode in ({0}) )", FormatHelper.ProcessQueryValues(reworkMOCode));
                reworkCondition += this.GetReworkSheetDateRange(reworkMOCode);	//返工工单签核日期条件 (大于返工工单签核日期的维修记录)
            }

            string tsResourceCondition1 = "";
            string tsResourceCondition2 = "";
            if (tsResources != "" && tsResources != null)
            {
                //tsResourceCondition1 += string.Format(@" and b.RRESCODE in ({0}) ", FormatHelper.ProcessQueryValues(tsResources));
                tsResourceCondition1 += string.Format(@" and a.TSRESCODE in ({0}) ", FormatHelper.ProcessQueryValues(tsResources));
                //tsResourceCondition2 += string.Format(@" and a.TSRESCODE in ({0}) ", FormatHelper.ProcessQueryValues(tsResources));
            }

            string fromResourceCondition = "";
            if (fromResources != "" && fromResources != null)
            {
                fromResourceCondition += string.Format(@" and a.FRMRESCODE in ({0})", FormatHelper.ProcessQueryValues(fromResources));
            }

            string confirmResourceCondition = "";
            if (confirmResource != "" && confirmResource != null)
            {
                confirmResourceCondition += string.Format(@" and a.CRESCODE in ({0})", FormatHelper.ProcessQueryValues(confirmResource));
            }


            string tsStateCondition1 = string.Empty;
            string tsStateCondition2 = string.Empty;
            if (tsState == null)
            {
                /*
                tsStateCondition1 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}')",
                    TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow, TSStatus.TSStatus_TS);

                tsStateCondition2 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}','{3}')",
                    TSStatus.TSStatus_New, TSStatus.TSStatus_Confirm, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split, TSStatus.TSStatus_OffMo);
            
                */
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
                            || string.Compare(state, TSStatus.TSStatus_Scrap, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_OffMo, true) == 0)
                        {
                            tsStateCondition1 += string.Format(@"'{0}',", state);
                        }

                    }

                    if (tsStateCondition1.Length > 0)
                    {
                        tsStateCondition1 = string.Format(@" and a.TSSTATUS in ({0})", tsStateCondition1.TrimEnd(','));
                    }

                    if (tsStateCondition2.Length > 0)
                    {
                        tsStateCondition2 = string.Format(@" and a.TSSTATUS in ({0})", tsStateCondition2.TrimEnd(','));
                    }
                }
                else
                {
                    /*
                    tsStateCondition1 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}')",
                        TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow, TSStatus.TSStatus_TS);

                    tsStateCondition2 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}','{3}')",
                        TSStatus.TSStatus_New, TSStatus.TSStatus_Confirm, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split, TSStatus.TSStatus_OffMo);
                
                    */
                }
            }
            //维修完成，回流，维修中--
            string sql1 = string.Empty;
            //if (tsStateCondition1 != string.Empty)
            //{
            sql1 =
                string.Format(@" select distinct {0} from tblts a where 1=1 {8}{10}{11}{12} {1}{2}{3}{4}{5}{6}{7}{9} ",
                "a.RCARD,a.tsid,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,a.TSREPAIRUser as tsuser,a.TSREPAIRMDATE AS RDATE,a.TSREPAIRMTIME AS RTIME,a.FRMDATE,a.FRMTIME,a.TSRESCODE as rrescode,a.REFRESCODE,a.REFOPCODE,a.frmmemo,a.tstimes,a.RCARDSEQ",
                modelCondition, itemCondition, moCondition,
                SnCondition, dateCondition, tsResourceCondition1, fromResourceCondition,
                tsStateCondition1,
                confirmResourceCondition,
                receivedateCondition,
                TSdateCondition1,
                reworkCondition
                );
            //}

            //送修，待修，拆解，报废 -- 不涉及TBLTSERRORCAUSE
            string sql2 = string.Empty;
            /*
            if (tsStateCondition2 != string.Empty)
            {
                sql2 =
                 string.Format(@" select {0} from tblts a where {1} {2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                 "a.RCARD,a.rcardseq,a.tsid,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,a.tsuser,a.tsdate AS RDATE,a.tstime AS RTIME,a.FRMDATE,a.FRMTIME,a.TSRESCODE as RRESCODE,a.REFRESCODE,a.REFOPCODE,a.TSTIMES,a.SCRAPCAUSE",
                    tsStateCondition2,
                    modelCondition,
                    itemCondition,
                    moCondition,
                    SnCondition,
                    dateCondition,
                    tsResourceCondition2,
                    fromResourceCondition,
                    confirmResourceCondition,
                    receivedateCondition,
                    TSdateCondition2,
                    reworkCondition
                 );
            }
            */

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
                typeof(QDOTSRecord),
                new PagerCondition(sql
                , "rcard", inclusive, exclusive, true));
        }

        public int QueryTSRecordCount(
            string modelCode, string itemCode,
            string moCode, string reworkMOCode,
            string snStart, string snEnd,
            int startDate, int startTime,
            int endDate, int endTime,
            int receiveStartDate, int receiveStartTime,
            int receiveEndDate, int receiveEndTime,
            int TSstartDate, int TSstartTime,
            int TSendDate, int TSendTime,
            ArrayList tsState, string tsResources, string fromResources, string confirmResource)
        {
            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
            }

            string itemCondition = "";
            if (itemCode != "" && itemCode != null)
            {
                itemCondition += string.Format(@" and a.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            string modelCondition = "";
            if (modelCode != "" && modelCode != null)
            {
                modelCondition += string.Format(@" and a.modelcode in ({0}) ", FormatHelper.ProcessQueryValues(modelCode));
            }

            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("a.rcard", snStart.ToUpper(), snEnd.ToUpper());

            string dateCondition = string.Empty;
            string receivedateCondition = string.Empty;
            string TSdateCondition1 = string.Empty;
            string TSdateCondition2 = string.Empty;
            if (startDate != DefaultDateTime.DefaultToInt)
            {
                //不良日期	tblts.mdate 按送修时间查询
                //dateCondition = FormatHelper.GetDateRangeSql("a.FRMDATE", "a.FRMTIME", startDate, startTime, endDate, endTime);
                dateCondition = FormatHelper.GetDateRangeSql("a.FRMDATE", startDate, endDate);
            }
            else if (receiveStartDate != DefaultDateTime.DefaultToInt)
            {
                //接收日期	tblts.CONFIRMDATE
                //receivedateCondition = FormatHelper.GetDateRangeSql("a.CONFIRMDATE", "a.CONFIRMTIME", receiveStartDate, receiveStartTime, receiveEndDate, receiveEndTime);
                receivedateCondition = FormatHelper.GetDateRangeSql("a.CONFIRMDATE", receiveStartDate, receiveEndDate);
            }
            else if (TSstartDate != DefaultDateTime.DefaultToInt)
            {
                //维修日期 	tbltserrorcause.mdate 和  tblts.tsdate
                //TSdateCondition1 = FormatHelper.GetDateRangeSql("b.mdate", "b.mtime", TSstartDate, TSstartTime, TSendDate, TSendTime);
                TSdateCondition1 = FormatHelper.GetDateRangeSql("a.tsrepairmdate", TSstartDate, TSendDate);
                //TSdateCondition2 = FormatHelper.GetDateRangeSql("a.tsdate", "a.tstime", TSstartDate, TSstartTime, TSendDate, TSendTime);                
                //TSdateCondition2 = FormatHelper.GetDateRangeSql("a.tsdate", TSstartDate, TSendDate);
            }

            //返工工单条件
            string reworkCondition = string.Empty;
            if (reworkMOCode != "" && reworkMOCode != null)
            {
                reworkCondition = string.Format(" and a.rcard in ( select rcard from tblreworkrange where reworkcode in ({0}) )", FormatHelper.ProcessQueryValues(reworkMOCode));
            }


            string tsResourceCondition1 = "";
            string tsResourceCondition2 = "";
            if (tsResources != "" && tsResources != null)
            {
                //tsResourceCondition1 += string.Format(@" and b.RRESCODE in ({0}) ", FormatHelper.ProcessQueryValues(tsResources));
                tsResourceCondition1 += string.Format(@" and a.TSRESCODE in ({0}) ", FormatHelper.ProcessQueryValues(tsResources));
                //tsResourceCondition2 += string.Format(@" and a.TSRESCODE in ({0}) ", FormatHelper.ProcessQueryValues(tsResources));
            }

            string fromResourceCondition = "";
            if (fromResources != "" && fromResources != null)
            {
                fromResourceCondition += string.Format(@" and a.FRMRESCODE in ({0})", FormatHelper.ProcessQueryValues(fromResources));
            }

            string confirmResourceCondition = "";
            if (confirmResource != "" && confirmResource != null)
            {
                confirmResourceCondition += string.Format(@" and a.CRESCODE in ({0})", FormatHelper.ProcessQueryValues(confirmResource));
            }


            string tsStateCondition1 = string.Empty;
            string tsStateCondition2 = string.Empty;
            if (tsState == null)
            {
                /*
                tsStateCondition1 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}')",
                    TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow, TSStatus.TSStatus_TS);

                tsStateCondition2 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}','{3}')",
                    TSStatus.TSStatus_New, TSStatus.TSStatus_Confirm, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split, TSStatus.TSStatus_OffMo);
                */
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
                            || string.Compare(state, TSStatus.TSStatus_Scrap, true) == 0
                            || string.Compare(state, TSStatus.TSStatus_OffMo, true) == 0)
                        {
                            tsStateCondition1 += string.Format(@"'{0}',", state);
                        }

                    }

                    if (tsStateCondition1.Length > 0)
                    {
                        tsStateCondition1 = string.Format(@" and a.TSSTATUS in ({0})", tsStateCondition1.TrimEnd(','));
                    }

                    if (tsStateCondition2.Length > 0)
                    {
                        tsStateCondition2 = string.Format(@" and a.TSSTATUS in ({0})", tsStateCondition2.TrimEnd(','));
                    }
                }
                else
                {
                    /*
                    tsStateCondition1 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}')",
                        TSStatus.TSStatus_Complete, TSStatus.TSStatus_Reflow, TSStatus.TSStatus_TS);

                    tsStateCondition2 = string.Format(@" a.TSSTATUS in ('{0}','{1}','{2}','{3}')",
                        TSStatus.TSStatus_New, TSStatus.TSStatus_Confirm, TSStatus.TSStatus_Scrap, TSStatus.TSStatus_Split, TSStatus.TSStatus_OffMo);
                    */
                }
            }
            //维修完成，回流，维修中--
            string sql1 = string.Empty;
            //if (tsStateCondition1 != string.Empty)
            //{
            sql1 =
                string.Format(@" select distinct {0} from tblts a where 1=1 {8}{10}{11}{12} {1}{2}{3}{4}{5}{6}{7}{9}",
                "a.RCARD,a.tsid,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,a.TSREPAIRUser as tsuser,a.TSREPAIRMDATE AS RDATE,a.TSREPAIRMTIME AS RTIME,a.FRMDATE,a.FRMTIME,a.TSRESCODE as rrescode,a.REFRESCODE,a.REFOPCODE,a.frmmemo",
                modelCondition, itemCondition, moCondition,
                SnCondition, dateCondition, tsResourceCondition1, fromResourceCondition,
                tsStateCondition1,
                confirmResourceCondition,
                receivedateCondition,
                TSdateCondition1,
                reworkCondition
                );
            //}

            //送修，待修，拆解，报废 -- 不涉及TBLTSERRORCAUSE
            string sql2 = string.Empty;
            /*
            if (tsStateCondition2 != string.Empty)
            {
                sql2 =
                    string.Format(@" select {0} from tblts a where {1} {2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                    "a.RCARD,a.tsid,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.crescode,a.confirmuser,a.confirmtime, a.confirmdate,a.tsuser,a.tsdate AS RDATE,a.tstime AS RTIME,a.FRMDATE,a.FRMTIME,a.TSRESCODE as RRESCODE,a.REFRESCODE,a.REFOPCODE",
                    tsStateCondition2,
                    modelCondition,
                    itemCondition,
                    moCondition,
                    SnCondition,
                    dateCondition,
                    tsResourceCondition2,
                    fromResourceCondition,
                    confirmResourceCondition,
                    receivedateCondition,
                    TSdateCondition2,
                    reworkCondition
                    );
            }
            */

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

        //取得返工工单的签核日期条件 
        private string GetReworkSheetDateRange(string reworkcodes)
        {
            if (reworkcodes == null || reworkcodes.Length == 0) return string.Empty;
            string[] codes = reworkcodes.Split(',');
            if (codes != null && codes.Length > 0)
            {
                //取第一个返工工单.
                string sql = string.Format(" select TBLREWORKPASS.* from TBLREWORKPASS where status=1 and ISPASS=1 and REWORKCODE='{0}' ", codes[0].ToUpper());
                object[] reworkpasses = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Rework.ReworkPass), new SQLCondition(sql));
                if (reworkpasses != null && reworkpasses.Length > 0)
                {
                    string returnDateRange = string.Format(" and a.mdate > {0} ", ((BenQGuru.eMES.Domain.Rework.ReworkPass)reworkpasses[0]).MaintainDate);
                    return returnDateRange;
                }
            }
            return string.Empty;
        }

        //维修导出方法,导出包含明细信息.
        public object[] QueryExportTSRecord(
            string modelCode, string itemCode,
            string moCode, string reworkMOCode,
            string snStart, string snEnd,
            int startDate, int startTime,
            int endDate, int endTime,
            int receiveStartDate, int receiveStartTime,
            int receiveEndDate, int receiveEndTime,
            int TSstartDate, int TSstartTime,
            int TSendDate, int TSendTime,
            ArrayList tsState, string tsResources, string fromResources, string confirmResource,
            int inclusive, int exclusive)
        {
            //获取ts主档
            object[] tsObjs = this.QueryTSRecord(modelCode, itemCode, moCode, reworkMOCode, snStart, snEnd, startDate, startTime,
             endDate, endTime,
             receiveStartDate, receiveStartTime,
             receiveEndDate, receiveEndTime,
             TSstartDate, TSstartTime,
             TSendDate, TSendTime, tsState, tsResources, fromResources, confirmResource, inclusive, exclusive);
            ArrayList returnObjs = new ArrayList();

            ArrayList tsidList = new ArrayList();
            foreach (QDOTSRecord qts in tsObjs)
            {
                tsidList.Add(qts.TSID);
            }

            string tsIDs = "('" + String.Join("','", (string[])tsidList.ToArray(typeof(string))) + "')";
            //string runningCards = String.Join("','",tsObjs
            if (tsObjs != null && tsObjs.Length > 0)
            {
                QueryTSDetailsFacade tsDetailFacade = new QueryTSDetailsFacade(this.DataProvider);
                //				
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
                                exportTSDetails.TSMemo = _ts.TSMemo;
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

        public object[] QueryTSPerformanceRecord(
            string modelCode, string itemCode, string moCode,
            string snStart, string snEnd,
            int startDate,
            int endDate,
            ArrayList tsState, string tsOperator, string tsResources,
            int inclusive, int exclusive)
        {
            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
            }

            string itemCondition = "";
            if (moCondition == "" &&
                itemCode != "" && itemCode != null)
            {
                itemCondition += string.Format(@" and a.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            string modelCondition = "";
            if (itemCondition == "" &&
                modelCode != "" && modelCode != null)
            {
                modelCondition += string.Format(@" and a.modelcode in ({0}) ", FormatHelper.ProcessQueryValues(modelCode));
            }

            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("a.rcard", snStart.ToUpper(), snEnd.ToUpper());

            //暂时还没有确认，按送修时间查询
            string dateCondition = FormatHelper.GetDateRangeSql("b.shiftday", startDate, endDate);

            string tsStateCondition = "";
            if (tsState != null)
            {
                if (tsState.Count > 0)
                {
                    string tsStateList = "";
                    foreach (string state in tsState)
                    {
                        tsStateList += string.Format(@"'{0}',", state);
                        //added by jessie lee,2005-9-19
                        if ((string.Compare(state, TSStatus.TSStatus_Complete, true)) == 0)
                        {
                            tsStateList += string.Format(@"'{0}',", TSStatus.TSStatus_Reflow);
                        }
                    }

                    if (tsStateList.Length > 0)
                    {
                        tsStateList = tsStateList.Substring(0, tsStateList.Length - 1);
                        tsStateCondition += string.Format(@" and a.TSSTATUS in ({0})", tsStateList);
                    }
                }
            }

            string tsOperatorCondition = "";
            if (tsOperator != "" && tsOperator != null)
            {
                tsOperatorCondition += string.Format(@" and decode(b.muser,null,a.muser,'',a.muser,b.muser) in ({0})", FormatHelper.ProcessQueryValues(tsOperator));
            }

            string tsResourceCondition = "";
            if (tsResources != "" && tsResources != null)
            {
                tsResourceCondition += string.Format(@" and b.RRESCODE in ({0})", FormatHelper.ProcessQueryValues(tsResources));
            }

            string sql = string.Format(@" select  {0} from tblts a,TBLTSERRORCAUSE b,tblitem c where a.tsid = b.tsid and a.itemcode=c.itemcode(+) {1}{2}{3}{4}{5}{6}{7}{8} group by {9} ",
                string.Format("a.RCARD,a.rcardseq,a.tsid,decode(a.TSSTATUS,'{0}','{1}',a.TSSTATUS) as TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE || ' - ' || c.itemdesc as ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,b.muser as tsuser,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,max(a.FRMDATE) as FRMDATE,max(a.FRMTIME) as FRMTIME,b.RRESCODE,a.REFRESCODE,a.REFOPCODE",
                TSStatus.TSStatus_Reflow, TSStatus.TSStatus_Complete),
                modelCondition, itemCondition, moCondition,
                SnCondition, dateCondition,
                tsStateCondition, tsOperatorCondition, tsResourceCondition,
                " a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,b.RRESCODE,a.REFRESCODE,a.REFOPCODE,a.FRMUSER,b.muser,a.tsid,a.rcardseq,c.itemdesc "
                );

            string selectCondition = "SELECT A.RCARD,A.RCARDSEQ, A.TSID,TSSTATUS ,A.MOCODE, A.MODELCODE, A.ITEMCODE ,A.TSDATE,";
            selectCondition += "A.TSTIME,A.FRMRESCODE || ' - ' || tblres.resdesc AS FRMRESCODE, A.FRMUSER,A.TSUSER,A.RDATE,A.RTIME,";
            selectCondition += "A.FRMDATE,A.FRMTIME, A.RRESCODE,A.REFRESCODE,A.REFOPCODE || ' - ' || tblop.opdesc AS REFOPCODE FROM ( ";
            sql = selectCondition + sql + ") A  LEFT OUTER JOIN tblres ON a.FRMRESCODE=tblres.rescode left outer join tblop on a.REFOPCODE=tblop.opcode";


            string selectCondition1 = "SELECT A.RCARD,A.RCARDSEQ, A.TSID,TSSTATUS ,A.MOCODE, A.MODELCODE, A.ITEMCODE ,A.TSDATE,";
            selectCondition1 += "A.TSTIME,A.FRMRESCODE, A.FRMUSER,A.TSUSER,A.RDATE,A.RTIME,";
            selectCondition1 += "A.FRMDATE,A.FRMTIME,A.REFOPCODE";

            string linkSQL1 = FormatHelper.GetLinkTableSQL("A", "RRESCODE", "tblres", "rescode");
            string linkSQL2 = FormatHelper.GetLinkTableSQL("A", "REFRESCODE", "tblres", "rescode");

            sql = selectCondition1 + ",A.RRESCODE || ' - ' || tblres.resdesc AS RRESCODE,A.REFRESCODE from (" + sql + ") A " + linkSQL1 + "";
            sql = selectCondition1 + ",A.RRESCODE,A.REFRESCODE || ' - ' || tblres.resdesc AS REFRESCODE from (" + sql + ") A " + linkSQL2 + "";





#if DEBUG
            Log.Info(
                new PagerCondition(sql, "rcard", inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(QDOTSRecord),
                new PagerCondition(sql, "rcard", inclusive, exclusive, true));
        }

        public int QueryTSPerformanceRecordCount(
            string modelCode, string itemCode, string moCode,
            string snStart, string snEnd,
            int startDate,
            int endDate,
            ArrayList tsState, string tsOperator, string tsResources)
        {
            string moCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCondition += string.Format(@" and a.mocode in ({0}) ", FormatHelper.ProcessQueryValues(moCode));
            }

            string itemCondition = "";
            if (moCondition == "" &&
                itemCode != "" && itemCode != null)
            {
                itemCondition += string.Format(@" and a.itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            string modelCondition = "";
            if (itemCondition == "" &&
                modelCode != "" && modelCode != null)
            {
                modelCondition += string.Format(@" and a.modelcode in ({0}) ", FormatHelper.ProcessQueryValues(modelCode));
            }

            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("a.rcard", snStart.ToUpper(), snEnd.ToUpper());

            //暂时还没有确认，按送修时间查询
            string dateCondition = FormatHelper.GetDateRangeSql("b.shiftday", startDate, endDate);

            string tsStateCondition = "";
            if (tsState != null)
            {
                string tsStateList = "";
                foreach (string state in tsState)
                {
                    tsStateList += string.Format(@"'{0}',", state);
                    //added by jessie lee,2005-9-19
                    if ((string.Compare(state, TSStatus.TSStatus_Complete, true)) == 0)
                    {
                        tsStateList += string.Format(@"'{0}',", TSStatus.TSStatus_Reflow);
                    }
                }
                if (tsStateList.Length > 0)
                {
                    tsStateList = tsStateList.Substring(0, tsStateList.Length - 1);
                    tsStateCondition += string.Format(@" and a.TSSTATUS in ({0})", tsStateList);
                }
            }

            string tsOperatorCondition = "";
            if (tsOperator != "" && tsOperator != null)
            {
                tsOperatorCondition += string.Format(@" and decode(b.muser,null,a.muser,'',a.muser,b.muser) in ({0})", FormatHelper.ProcessQueryValues(tsOperator));
            }

            string tsResourceCondition = "";
            if (tsResources != "" && tsResources != null)
            {
                tsResourceCondition += string.Format(@" and b.RRESCODE in ({0})", FormatHelper.ProcessQueryValues(tsResources));
            }

            string sql = string.Format(@" select count(*) from ( select distinct {0} from tblts a,TBLTSERRORCAUSE b where a.tsid = b.tsid {1}{2}{3}{4}{5}{6}{7}{8}  group by {9} )",
                string.Format("a.RCARD,a.tsid,decode(a.TSSTATUS,'{0}','{1}',a.TSSTATUS) as TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,a.FRMUSER,a.TSUSER,max(b.MDATE) AS RDATE,max(b.MTIME) AS RTIME,max(a.FRMDATE) as FRMDATE,max(a.FRMTIME) as FRMTIME,b.RRESCODE,a.REFRESCODE,a.REFOPCODE",
                TSStatus.TSStatus_Reflow, TSStatus.TSStatus_Complete),
                modelCondition, itemCondition, moCondition,
                SnCondition, dateCondition,
                tsStateCondition, tsOperatorCondition, tsResourceCondition,
                "  a.RCARD,a.TSSTATUS,a.MOCODE,a.MODELCODE,a.ITEMCODE,a.TSDATE,a.TSTIME,a.FRMRESCODE,b.RRESCODE,a.REFRESCODE,a.REFOPCODE,a.FRMUSER,a.TSUSER,a.tsid  ");
#if DEBUG
            Log.Info(
                new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(sql));
        }

        //维修绩效导出明细(包含维修明细)
        public object[] ExportQueryTSPerformanceRecord(
            string modelCode, string itemCode, string moCode,
            string snStart, string snEnd,
            int startDate,
            int endDate,
            ArrayList tsState, string tsOperator, string tsResources,
            int inclusive, int exclusive)
        {
            //获取ts主档
            object[] tsObjs = this.QueryTSPerformanceRecord(modelCode, itemCode, moCode, snStart, snEnd, startDate, endDate, tsState, tsOperator, tsResources, inclusive, exclusive);
            ArrayList returnObjs = new ArrayList();
            if (tsObjs != null && tsObjs.Length > 0)
            {
                QueryTSDetailsFacade tsDetailFacade = new QueryTSDetailsFacade(this.DataProvider);
                foreach (QDOTSRecord _ts in tsObjs)
                {
                    object[] tsDetailObjs = tsDetailFacade.QueryTSDetails("", "", _ts.MoCode, _ts.SN, "", "", _ts.RepaireResource, _ts.SourceResourceDate, _ts.SourceResourceTime, 1, int.MaxValue);
                    if (tsDetailObjs != null && tsDetailObjs.Length > 0)
                        foreach (QDOTSDetails1 tsdetail in tsDetailObjs)
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
            return (ExportQDOTSDetails1[])returnObjs.ToArray(typeof(ExportQDOTSDetails1));
        }
    }
}
