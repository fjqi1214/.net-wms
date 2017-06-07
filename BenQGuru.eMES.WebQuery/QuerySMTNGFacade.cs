using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.TSModel;

namespace BenQGuru.eMES.WebQuery
{
    /// <summary>
    /// QuerySMTNGFacade 的摘要说明。
    /// </summary>
    public class QuerySMTNGFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public QuerySMTNGFacade(IDomainDataProvider domainDataProvider)
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

        public object[] QuerySMTNG(
            string itemCodes, string moCodes,
            string stepSequenceCodes, string startSn, string endSn,
            int inclusive, int exclusive)
        {
            string itemCondition = "";
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
                    itemCondition += string.Format(@" and TBLTS.itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and TBLTS.itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }

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
                    moCondition += string.Format(@" and TBLTS.mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and TBLTS.mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            string stepSeqCondition = "";
            if (stepSequenceCodes.Trim().Length > 0 && stepSequenceCodes != null)
            {
                if (stepSequenceCodes.IndexOf(",") >= 0)
                {
                    string[] lists = stepSequenceCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    stepSequenceCodes = string.Join(",", lists);
                    stepSeqCondition += string.Format(@" and TBLTS.FRMsscode in ({0})", stepSequenceCodes.ToUpper());
                }
                else
                {
                    stepSeqCondition += string.Format(@" and TBLTS.FRMsscode like '{0}%'", stepSequenceCodes.ToUpper());
                }
            }

            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("TBLTS.rcard", startSn.ToUpper(), endSn.ToUpper());

            //			string startSnCondition = "";
            //			if( startSn != "" && startSn != null )
            //			{
            //				startSnCondition = string.Format(
            //					@" and rcard >= '{0}'",startSn.ToUpper());
            //			}
            //
            //			string endSnCondition = "";
            //			if( endSn != "" && endSn != null )
            //			{
            //				endSnCondition = string.Format(
            //					@" and rcard <= '{0}'",endSn.ToUpper() );
            //			}

            string fields = " TBLTS.TSDATE,TBLTS.RCARD,TBLTS.COPCODE ,TBLTS.TFFULLPATH,TBLTS.FRMWEEK,TBLTS.TSMEMO,TBLTS.TSTYPE,TBLTS.SHIFTTYPECODE,";
            fields += "TBLTS.FRMOPCODE || ' - ' || tblop.opdesc AS FRMOPCODE,TBLTS.FRMRESCODE || ' - ' || tblres.resdesc AS FRMRESCODE,TBLTS.TCARDSEQ,TBLTS.SHIFTCODE,TBLTS.TSUSER,TBLTS.FRMROUTECODE,";
            fields += "TBLTS.FRMSSCODE || ' - ' || tblss.ssdesc AS FRMSSCODE,TBLTS.FRMTIME,TBLTS.TSREPAIRUSER,TBLTS.FRMDATE,TBLTS.TSTIMES,TBLTS.FRMOUTROUTECODE,";
            fields += "TBLTS.TSID,TBLTS.TSREPAIRMTIME,TBLTS.MUSER || ' - ' || tbluser.username AS MUSER ,TBLTS.TSTIME,TBLTS.EATTRIBUTE1,TBLTS.RMABILLCODE,TBLTS.MOCODE,";
            fields += "TBLTS.SCARDSEQ,TBLTS.RRCARD,TBLTS.CONFIRMDATE,TBLTS.REFRESCODE,TBLTS.FRMSEGCODE,DECODE(TBLTS.TSSTATUS, 'tsstatus_reflow', 'tsstatus_complete', TBLTS.TSSTATUS) AS TSSTATUS,";
            fields += "TBLTS.MODELCODE,TBLTS.CARDTYPE,TBLTS.FRMINPUTTYPE,TBLTS.SHIFTDAY,TBLTS.SCRAPCAUSE,TBLTS.REFMOCODE,TBLTS.FRMUSER,TBLTS.FRMMONTH,TBLTS.ITEMCODE || ' - ' || TBLITEM.Itemdesc AS ITEMCODE ,";
            fields += "TBLTS.TSREPAIRMDATE,TBLTS.REFOPCODE,TBLTS.SCARD, TBLTS.CONFIRMUSER,TBLTS.TPCODE,TBLTS.RCARDSEQ,TBLTS.MOSEQ,TBLTS.CRESCODE,TBLTS.TRANSSTATUS,";
            fields += "TBLTS.MTIME,TBLTS.REFROUTECODE,TBLTS.TCARD,TBLTS.MDATE,TBLTS.TSRESCODE,TBLTS.CONFIRMTIME,  TBLTS.FRMMEMO";

            string joinCondition = " LEFT OUTER JOIN TBLITEM ON TBLTS.ITEMCODE = TBLITEM.ITEMCODE ";
            joinCondition += " LEFT OUTER JOIN tblop ON TBLTS.Frmopcode = tblop.OPCODE ";
            joinCondition += " LEFT OUTER JOIN tblss ON TBLTS.Frmsscode = tblss.sscode ";
            joinCondition += " LEFT OUTER JOIN tblres ON TBLTS.Frmrescode=tblres.rescode ";
            joinCondition += " LEFT OUTER JOIN TBLUSER ON TBLTS.MUSER = TBLUSER.USERCODE ";
            string sql = string.Format(
                @"select {0} from TBLTS {1} where 1=1 {2}{3}{4}{5}",
                fields,joinCondition,
                itemCondition, moCondition, stepSeqCondition, SnCondition);

#if DEBUG
            Log.Info(
                new PagerCondition(
                sql,
                "mocode,itemcode,RCARD",
                inclusive, exclusive, true).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(Domain.TS.TS),
                new PagerCondition(
                sql,
                "mocode,itemcode,RCARD",
                inclusive, exclusive, true));
        }

        public int QuerySMTNGCount(
            string itemCodes, string moCodes,
            string stepSequenceCodes, string startSn, string endSn)
        {
            string itemCondition = "";
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
                    itemCondition += string.Format(@" and itemcode in ({0})", itemCodes.ToUpper());
                }
                else
                {
                    itemCondition += string.Format(@" and itemcode like '{0}%'", itemCodes.ToUpper());
                }
            }

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
                    moCondition += string.Format(@" and mocode in ({0})", moCodes.ToUpper());
                }
                else
                {
                    moCondition += string.Format(@" and mocode like '{0}%'", moCodes.ToUpper());
                }
            }

            string stepSeqCondition = "";
            if (stepSequenceCodes.Trim().Length > 0 && stepSequenceCodes != null)
            {
                if (stepSequenceCodes.IndexOf(",") >= 0)
                {
                    string[] lists = stepSequenceCodes.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    stepSequenceCodes = string.Join(",", lists);
                    stepSeqCondition += string.Format(@" and FRMsscode in ({0})", stepSequenceCodes.ToUpper());
                }
                else
                {
                    stepSeqCondition += string.Format(@" and FRMsscode like '{0}%'", stepSequenceCodes.ToUpper());
                }
            }

            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("rcard", startSn.ToUpper(), endSn.ToUpper());

            string sql = string.Format(
                @"select {0} from TBLTS where 1=1 {1}{2}{3}{4}",
                "count(*)",
                itemCondition, moCondition, stepSeqCondition, SnCondition);
#if DEBUG
            Log.Info(sql);
#endif
            return this.DataProvider.GetCount(
                new SQLCondition(sql));
        }

        public object[] QuerySMTNGDetails(
            string moCode, string sn, string opCode, string resourceCode,
            int inclusive, int exclusive)
        {
            //因为tbltserrorcode 和 tbltserrorcode2loc的数据是有重复的
            //有不良位置的才会到tbltserrorcode2loc,因此连接查询
            string sql = string.Format(@" SELECT DISTINCT tbltserrorcode.tsid, ab, subeloc, tbltserrorcode.itemcode, tbltserrorcode.modelcode, tbltserrorcode.ecgcode, eloc, memo,
                tbltserrorcode.ecode, tbltserrorcode.mocode, tbltserrorcode.mtime, tbltserrorcode.eattribute1, tbltserrorcode.mdate, tbltserrorcode.rcardseq, tbltserrorcode.muser,
                tbltserrorcode.rcard
           FROM tbltserrorcode  , tbltserrorcode2loc 
          WHERE  tbltserrorcode.rcard  =tbltserrorcode2loc.rcard(+) AND tbltserrorcode.ECODE = tbltserrorcode2loc.ECODE(+) and  tbltserrorcode.rcard = '{0}' 
			order by ECGCODE,ECODE,ELOC,AB ", sn.ToUpper());
            //			string sql = string.Format(
            //				@"select distinct {0} from TBLTSERRORCODE2LOC where rcard = '{1}' union
            //					SELECT DISTINCT tsid,'' as ab,'' as subeloc, itemcode, modelcode, ecgcode,'' as eloc,'' as memo,
            //					ecode, mocode, mtime, eattribute1, mdate, rcardseq, muser,rcard FROM tbltserrorcode
            //					where  rcard = '{1}' ",
            //				DomainObjectUtility.GetDomainObjectFieldsString(typeof(TSErrorCode2Location)),
            //				sn.ToUpper());
#if DEBUG
            Log.Info(
                new PagerCondition(
                sql,
                //"ECGCODE,ECODE,ELOC,AB",
                inclusive, exclusive, true).SQLText);
#endif
            object[] objs = this.DataProvider.CustomQuery(
                typeof(QSMTNGList),
                new PagerCondition(
                sql,
                //"ECGCODE,ECODE,ELOC,AB",
                inclusive, exclusive, true));

            if (objs != null)
            {
                foreach (QSMTNGList obj in objs)
                {
                    object[] ec = this.DataProvider.CustomQuery(typeof(ErrorCodeA),
                        new SQLCondition(
                        string.Format(@"select ecdesc from tblec where ecode = '{0}'", obj.ErrorCode)));
                    if (ec != null)
                    {
                        obj.ErrorCodeDesc = (ec[0] as ErrorCodeA).ErrorDescription;
                    }
                    else
                    {
                        obj.ErrorCodeDesc = string.Empty;
                    }

                    object[] eg = this.DataProvider.CustomQuery(typeof(ErrorCodeGroupA),
                        new SQLCondition(String.Format("select ecgdesc from tblecg where ecgcode ='{0}'", obj.ErrorCodeGroup)));

                    if (eg != null)
                    {
                        obj.ErrorCodeGroupDesc = ((ErrorCodeGroupA)eg[0]).ErrorCodeGroupDescription;
                    }
                }
            }
            return objs;
        }

        public int QuerySMTNGDetailsCount(
            string moCode, string sn, string opCode, string resourceCode)
        {
            //			string sql = string.Format(
            //				@"select {0} from (select  ecode from TBLTSERRORCODE2LOC where rcard = '{1}' union
            //					SELECT  ecode FROM tbltserrorcode
            //					where  rcard = '{1}' )",
            //				"count(ecode)",
            //				sn.ToUpper());

            string sql = string.Format(@" SELECT {0} FROM ( SELECT DISTINCT tbltserrorcode.tsid, ab, subeloc, tbltserrorcode.itemcode, tbltserrorcode.modelcode, tbltserrorcode.ecgcode, eloc, memo,
                tbltserrorcode.ecode, tbltserrorcode.mocode, tbltserrorcode.mtime, tbltserrorcode.eattribute1, tbltserrorcode.mdate, tbltserrorcode.rcardseq, tbltserrorcode.muser,tbltserrorcode.rcard
				FROM tbltserrorcode  , tbltserrorcode2loc 
				WHERE  tbltserrorcode.rcard  =tbltserrorcode2loc.rcard(+) AND tbltserrorcode.ECODE = tbltserrorcode2loc.ECODE(+) and  tbltserrorcode.rcard = '{1}'  ) ",
                "count(ecode)", sn.ToUpper());
#if DEBUG
            Log.Info(new SQLCondition(sql).SQLText);
#endif
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
    }
}
