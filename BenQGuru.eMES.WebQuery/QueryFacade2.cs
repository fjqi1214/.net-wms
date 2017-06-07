using System;
using System.Collections;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.SMT;

namespace BenQGuru.eMES.WebQuery
{
    /// <summary>
    /// vizo 的摘要说明。
    /// </summary>
    public class QueryFacade2
    {
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

        public QueryFacade2(IDomainDataProvider dataProvider)
        {
            this._domainDataProvider = dataProvider;
        }


        #region ItemTracing

        public object[] QueryItemTracing(string SNFrom, string SNTo, string moCode, int inclusive, int exclusive)
        {
            return QueryItemTracing(SNFrom, SNTo, moCode, string.Empty, string.Empty, string.Empty, inclusive, exclusive);
        }

        public object[] QueryItemTracing(string SNFrom, string SNTo, string moCode, string itemcode, string mmodelcode, string bigsscode, int inclusive, int exclusive)
        {
            string queryColnum = " TBLSIMULATIONREPORT.RESCODE || ' - ' || tblres.resdesc AS RESCODE,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.SSCODE || ' - ' || TBLSS.SSDESC AS SSCODE, TBLSIMULATIONREPORT.SEGCODE || ' - ' || tblseg.segdesc AS SEGCODE,";
            queryColnum += " TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, ";
            queryColnum += "  TBLSIMULATIONREPORT.ITEMCODE || ' - ' || tblitem.itemdesc AS ITEMCODE,  TBLSIMULATIONREPORT.muser || ' - ' || tbluser.username AS muser , TBLSIMULATIONREPORT.rcard,TBLSIMULATIONREPORT.tcard, ";
            queryColnum += " TBLSIMULATIONREPORT.mtime, TBLSIMULATIONREPORT.OPCODE || ' - ' || tblop.opdesc AS OPCODE, TBLSIMULATIONREPORT.mdate,TBLSIMULATIONREPORT.LACTION,'' as optype,tblmaterial.mmodelcode,tblss.bigsscode ";

            string joinSql = "  LEFT OUTER JOIN tblmaterial ON TBLSIMULATIONREPORT.Itemcode=tblmaterial.mcode ";
            joinSql += " LEFT OUTER JOIN tblss ON TBLSIMULATIONREPORT.Sscode=tblss.sscode";
            joinSql += " LEFT OUTER JOIN tblitem ON TBLSIMULATIONREPORT.itemcode = tblitem.itemcode";
            joinSql += " LEFT OUTER JOIN tblop ON TBLSIMULATIONREPORT.OPCODE = tblop.OPCODE";
            joinSql += " LEFT OUTER JOIN tblseg ON TBLSIMULATIONREPORT.Segcode = tblseg.segcode";
            joinSql += " LEFT OUTER JOIN tblres ON TBLSIMULATIONREPORT.rescode = tblres.rescode";
            joinSql += " LEFT OUTER JOIN tbluser ON TBLSIMULATIONREPORT.muser = tbluser.usercode";

            string sql = string.Format("select {0} from tblsimulationreport {1} where 1=1 ",
               queryColnum, joinSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemTracingQuery)));
            /*
            string sql = string.Format(@"select TBLSIMULATIONREPORT.rescode,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.sscode, TBLSIMULATIONREPORT.segcode, TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, TBLSIMULATIONREPORT.itemcode,  TBLSIMULATIONREPORT.muser, TBLSIMULATIONREPORT.rcard, TBLSIMULATIONREPORT.mtime,"
                //modified by jessie lee for CS0041,2005/10/11,P4.10
                //完成spec要求的功能
                +@" 
                case (select count(*)
                        from tblts
                       where tblsimulationreport.rcard = tblts.rcard
                         and tblsimulationreport.rcardseq = tblts.rcardseq)
                    when 0 then TBLSIMULATIONREPORT.opcode
                    else 'TS' 
                end as opcode,"
                +@" 
                TBLSIMULATIONREPORT.mdate,
                TBLSIMULATIONREPORT.LACTION,
                '' as optype 
                from tblsimulationreport where 1=1 ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemTracing)) ) ;
                */

            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("tblsimulationreport.rcard", SNFrom.ToUpper(), SNTo.ToUpper());
            sql += SnCondition;

            //if (moCode != string.Empty)
            //{
            //    sql += string.Format(" and tblsimulationreport.mocode='{0}' ", moCode);
            //}

            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    sql += string.Format(" and tblsimulationreport.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblsimulationreport.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            if (itemcode.Trim().Length > 0)
            {
                if (itemcode.IndexOf(",") >= 0)
                {
                    string[] lists = itemcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemcode = string.Join(",", lists);
                    sql += string.Format(" and tblsimulationreport.itemcode in ({0})", itemcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblsimulationreport.itemcode like '{0}%'", itemcode.ToUpper());
                }
            }

            if (mmodelcode.Trim().Length > 0)
            {
                if (mmodelcode.IndexOf(",") >= 0)
                {
                    string[] lists = mmodelcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mmodelcode = string.Join(",", lists);
                    sql += string.Format(" and tblmaterial.mmodelcode in ({0})", mmodelcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblmaterial.mmodelcode like '{0}%'", mmodelcode.ToUpper());
                }
            }

            if (bigsscode.Trim().Length > 0)
            {
                if (bigsscode.IndexOf(",") >= 0)
                {
                    string[] lists = bigsscode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigsscode = string.Join(",", lists);
                    sql += string.Format(" and tblss.bigsscode in ({0})", bigsscode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblss.bigsscode like '{0}%'", bigsscode.ToUpper());
                }
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(ItemTracingQuery), new PagerCondition(sql, "tblsimulationreport.rcard", inclusive, exclusive));

            if (objs == null)
            {
                return null;
            }

            BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);


            for (int i = 0; i < objs.Length; i++)
            {
                ItemTracing objIT = objs[i] as ItemTracing;
                //added by jessie lee for CS0041,2005/10/11,P4.10
                //根据前面已经得到的rcard

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
                //				string tsSQL = string.Format("select {0} from tblts where rcard = '{1}' and rcardseq = {2} and mocode = '{3}' and tsstatus <> '{4}'",
                //					DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.TS.TS)),
                //					objIT.RCard,
                //					objIT.RCardSeq,
                //					objIT.MOCode,
                //					TSStatus.TSStatus_New);

                string tsSQL = string.Format(@"select scrapcause, frmtime, frmdate, frmuser, a.shifttypecode, shiftcode,
								tpcode, scardseq, frmmemo, tcardseq, tffullpath, a.shiftday, refrescode,
								transstatus, refopcode, a.COPCODE || ' - ' || tblop.opdesc AS COPCODE, refroutecode, crescode || ' - ' || tblres.resdesc as crescode, refmocode,
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
								frmroutecode, rcard, mocode from tblts a LEFT OUTER JOIN tblres ON A.CRESCODE=tblres.rescode
                                LEFT OUTER JOIN tblop ON a.copcode=tblop.OPCODE
								where rcard = '{0}' and rcardseq = {1} and mocode = '{2}' and tsstatus <> '{3}'",
                    objIT.RCard,
                    objIT.RCardSeq,
                    objIT.MOCode,
                    TSStatus.TSStatus_New);

                Object[] tsObjs = this.DataProvider.CustomQuery(
                    typeof(BenQGuru.eMES.Domain.TS.TS),
                    new PagerCondition(tsSQL, inclusive, exclusive));

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

                if (string.Compare(objIT.ItemStatus, ProductStatus.NG, true) == 0)
                {
                    objIT.ItemStatus = TSStatus.TSStatus_New;
                }

                object op = bmFacade.GetOperation(objIT.OPCode);
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
                        object ir2o = this.GetItemRoute2Operation(
                            objIT.ItemCode,
                            objIT.RouteCode,
                            objIT.OPCode
                            );

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

        //jack  add  2012-04-11  start 
        public object[] QueryRcardsInSimReportOrSplitboard(string SNFrom, string SNTo)
        {
            string SnCondition = string.Empty;
            SnCondition = FormatHelper.GetRCardRangeSql("tblsimulationreport.rcard", SNFrom.ToUpper(), SNTo.ToUpper());
            if (!string.IsNullOrEmpty(SnCondition))
            {
                string Sql = "";
                Sql += "   select distinct h.rcard  from (        ";
                Sql += "     select distinct tblsimulationreport.rcard      ";
                Sql += "    from tblsimulationreport  where 1 = 1     ";
                Sql += SnCondition;

                Sql += "     union all     ";
                Sql += "     select distinct tblsplitboard.rcard      ";
                Sql += "       from tblsplitboard      where 1 = 1     ";
                Sql += "       and (tblsplitboard.rcard in (select distinct tblsimulationreport.rcard  ";
                Sql += "                                      from tblsimulationreport   where 1 = 1     ";
                Sql += SnCondition;
                Sql += "      )  or    ";
                Sql += "         tblsplitboard.scard in (select distinct tblsimulationreport.rcard   ";
                Sql += "         from tblsimulationreport    where 1 = 1    ";
                Sql += SnCondition;
                Sql += "     ))     ";

                Sql += "     union all     ";
                Sql += "     select distinct tblsplitboard.scard  as rcard     ";
                Sql += "       from tblsplitboard      where 1 = 1     ";
                Sql += "       and (tblsplitboard.rcard in (select distinct tblsimulationreport.rcard  ";
                Sql += "                                      from tblsimulationreport   where 1 = 1     ";
                Sql += SnCondition;
                Sql += "      )  or    ";
                Sql += "         tblsplitboard.scard in (select distinct tblsimulationreport.rcard   ";
                Sql += "         from tblsimulationreport    where 1 = 1    ";
                Sql += SnCondition;
                Sql += "     ))     ";

                Sql += "     )  h       ";

                object[] objs = this.DataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(Sql));
                return objs;
            }
            return null;
        }

        public object[] QueryItemTracingForSplitboard(string SNFrom, string SNTo, string moCode, string itemcode, string mmodelcode, string bigsscode, int inclusive, int exclusive)
        {
            string queryColnum = " TBLSIMULATIONREPORT.RESCODE || ' - ' || tblres.resdesc AS RESCODE,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.SSCODE || ' - ' || TBLSS.SSDESC AS SSCODE, TBLSIMULATIONREPORT.SEGCODE || ' - ' || tblseg.segdesc AS SEGCODE,";
            queryColnum += " TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, ";
            queryColnum += "  TBLSIMULATIONREPORT.ITEMCODE || ' - ' || tblitem.itemdesc AS ITEMCODE,  TBLSIMULATIONREPORT.muser || ' - ' || tbluser.username AS muser , TBLSIMULATIONREPORT.rcard,TBLSIMULATIONREPORT.tcard, ";
            queryColnum += " TBLSIMULATIONREPORT.mtime, TBLSIMULATIONREPORT.OPCODE || ' - ' || tblop.opdesc AS OPCODE, TBLSIMULATIONREPORT.mdate,TBLSIMULATIONREPORT.LACTION,'' as optype,tblmaterial.mmodelcode,tblss.bigsscode ";

            string joinSql = "  LEFT OUTER JOIN tblmaterial ON TBLSIMULATIONREPORT.Itemcode=tblmaterial.mcode ";
            joinSql += " LEFT OUTER JOIN tblss ON TBLSIMULATIONREPORT.Sscode=tblss.sscode";
            joinSql += " LEFT OUTER JOIN tblitem ON TBLSIMULATIONREPORT.itemcode = tblitem.itemcode";
            joinSql += " LEFT OUTER JOIN tblop ON TBLSIMULATIONREPORT.OPCODE = tblop.OPCODE";
            joinSql += " LEFT OUTER JOIN tblseg ON TBLSIMULATIONREPORT.Segcode = tblseg.segcode";
            joinSql += " LEFT OUTER JOIN tblres ON TBLSIMULATIONREPORT.rescode = tblres.rescode";
            joinSql += " LEFT OUTER JOIN tbluser ON TBLSIMULATIONREPORT.muser = tbluser.usercode";

            string sql = string.Format("select {0} from tblsimulationreport {1} where 1=1 ",
               queryColnum, joinSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemTracingQuery)));
            /*
            string sql = string.Format(@"select TBLSIMULATIONREPORT.rescode,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.sscode, TBLSIMULATIONREPORT.segcode, TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, TBLSIMULATIONREPORT.itemcode,  TBLSIMULATIONREPORT.muser, TBLSIMULATIONREPORT.rcard, TBLSIMULATIONREPORT.mtime,"
                //modified by jessie lee for CS0041,2005/10/11,P4.10
                //完成spec要求的功能
                +@" 
                case (select count(*)
                        from tblts
                       where tblsimulationreport.rcard = tblts.rcard
                         and tblsimulationreport.rcardseq = tblts.rcardseq)
                    when 0 then TBLSIMULATIONREPORT.opcode
                    else 'TS' 
                end as opcode,"
                +@" 
                TBLSIMULATIONREPORT.mdate,
                TBLSIMULATIONREPORT.LACTION,
                '' as optype 
                from tblsimulationreport where 1=1 ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemTracing)) ) ;
                */
            if (!string.IsNullOrEmpty(SNFrom) && !string.IsNullOrEmpty(SNTo))
            {
                // 
                string sqlRcard = "";
                object[] objRcards = QueryRcardsInSimReportOrSplitboard(SNFrom.ToUpper(), SNTo.ToUpper());
                if (objRcards == null || objRcards.Length == 0)
                {
                    return null;
                }
                if (objRcards != null && objRcards.Length > 0)
                {
                    for (int i = 0; i < objRcards.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(((SimulationReport)objRcards[i]).RunningCard))
                        {
                            sqlRcard += "'" + ((SimulationReport)objRcards[i]).RunningCard.Trim().ToUpper() + "',";
                        }
                    }
                }
                if (string.IsNullOrEmpty(sqlRcard))
                {
                    return null;
                }

                sql += string.Format("  and tblsimulationreport.rcard  in  ( {0} ) ", sqlRcard.Trim().Substring(0, sqlRcard.Trim().Length - 1));
            }

            #region  未作改动的
            //
            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    sql += string.Format(" and tblsimulationreport.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblsimulationreport.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            if (itemcode.Trim().Length > 0)
            {
                if (itemcode.IndexOf(",") >= 0)
                {
                    string[] lists = itemcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemcode = string.Join(",", lists);
                    sql += string.Format(" and tblsimulationreport.itemcode in ({0})", itemcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblsimulationreport.itemcode like '{0}%'", itemcode.ToUpper());
                }
            }

            if (mmodelcode.Trim().Length > 0)
            {
                if (mmodelcode.IndexOf(",") >= 0)
                {
                    string[] lists = mmodelcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mmodelcode = string.Join(",", lists);
                    sql += string.Format(" and tblmaterial.mmodelcode in ({0})", mmodelcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblmaterial.mmodelcode like '{0}%'", mmodelcode.ToUpper());
                }
            }

            if (bigsscode.Trim().Length > 0)
            {
                if (bigsscode.IndexOf(",") >= 0)
                {
                    string[] lists = bigsscode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigsscode = string.Join(",", lists);
                    sql += string.Format(" and tblss.bigsscode in ({0})", bigsscode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblss.bigsscode like '{0}%'", bigsscode.ToUpper());
                }
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(ItemTracingQuery), new PagerCondition(sql, "tblsimulationreport.rcard", inclusive, exclusive));

            if (objs == null)
            {
                return null;
            }

            BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);


            for (int i = 0; i < objs.Length; i++)
            {
                ItemTracing objIT = objs[i] as ItemTracing;
                //added by jessie lee for CS0041,2005/10/11,P4.10
                //根据前面已经得到的rcard

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
                //				string tsSQL = string.Format("select {0} from tblts where rcard = '{1}' and rcardseq = {2} and mocode = '{3}' and tsstatus <> '{4}'",
                //					DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.TS.TS)),
                //					objIT.RCard,
                //					objIT.RCardSeq,
                //					objIT.MOCode,
                //					TSStatus.TSStatus_New);

                string tsSQL = string.Format(@"select scrapcause, frmtime, frmdate, frmuser, a.shifttypecode, shiftcode,
								tpcode, scardseq, frmmemo, tcardseq, tffullpath, a.shiftday, refrescode,
								transstatus, refopcode, a.COPCODE || ' - ' || tblop.opdesc AS COPCODE, refroutecode, crescode || ' - ' || tblres.resdesc as crescode, refmocode,
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
								frmroutecode, rcard, mocode from tblts a LEFT OUTER JOIN tblres ON A.CRESCODE=tblres.rescode
                                LEFT OUTER JOIN tblop ON a.copcode=tblop.OPCODE
								where rcard = '{0}' and rcardseq = {1} and mocode = '{2}' and tsstatus <> '{3}'",
                    objIT.RCard,
                    objIT.RCardSeq,
                    objIT.MOCode,
                    TSStatus.TSStatus_New);

                Object[] tsObjs = this.DataProvider.CustomQuery(
                    typeof(BenQGuru.eMES.Domain.TS.TS),
                    new PagerCondition(tsSQL, inclusive, exclusive));

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

                if (string.Compare(objIT.ItemStatus, ProductStatus.NG, true) == 0)
                {
                    objIT.ItemStatus = TSStatus.TSStatus_New;
                }

                object op = bmFacade.GetOperation(objIT.OPCode);
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
                        object ir2o = this.GetItemRoute2Operation(
                            objIT.ItemCode,
                            objIT.RouteCode,
                            objIT.OPCode
                            );

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
            #endregion

            return objs;
        }
      
        public int QueryItemTracingCountForSplitboard(string SNFrom, string SNTo, string moCode, string itemcode, string mmodelcode, string bigsscode)
        {
            string queryColnum = " TBLSIMULATIONREPORT.rescode,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.sscode, TBLSIMULATIONREPORT.segcode, TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, TBLSIMULATIONREPORT.itemcode,  TBLSIMULATIONREPORT.muser, TBLSIMULATIONREPORT.rcard,TBLSIMULATIONREPORT.tcard, TBLSIMULATIONREPORT.mtime, TBLSIMULATIONREPORT.opcode, TBLSIMULATIONREPORT.mdate,TBLSIMULATIONREPORT.LACTION,'' as optype,tblmaterial.mmodelcode,tblss.bigsscode ";
            string joinSql = "  LEFT JOIN tblmaterial ON TBLSIMULATIONREPORT.Itemcode=tblmaterial.mcode ";
            joinSql += " LEFT JOIN tblss ON TBLSIMULATIONREPORT.Sscode=tblss.sscode";
            string sql = string.Format("select {0} from tblsimulationreport {1} where 1=1 ", queryColnum, joinSql);

            if (!string.IsNullOrEmpty(SNFrom) && !string.IsNullOrEmpty(SNTo))
            {
                //
                string sqlRcard = "";
                object[] objRcards = QueryRcardsInSimReportOrSplitboard(SNFrom.ToUpper(), SNTo.ToUpper());
                if (objRcards == null || objRcards.Length == 0)
                {
                    return 0;
                }
                if (objRcards != null && objRcards.Length > 0)
                {
                    for (int i = 0; i < objRcards.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(((SimulationReport)objRcards[i]).RunningCard))
                        {
                            sqlRcard += "'" + ((SimulationReport)objRcards[i]).RunningCard.Trim().ToUpper() + "',";
                        }
                    }
                }
                if (string.IsNullOrEmpty(sqlRcard))
                {
                    return 0;
                }
                string SnCondition = string.Empty;
                sql += string.Format("  and tblsimulationreport.rcard  in  ( {0} ) ", sqlRcard.Trim().Substring(0, sqlRcard.Trim().Length - 1));
            }


            #region  未作改动的
            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    sql += string.Format(" and tblsimulationreport.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblsimulationreport.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            if (itemcode.Trim().Length > 0)
            {
                if (itemcode.IndexOf(",") >= 0)
                {
                    string[] lists = itemcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemcode = string.Join(",", lists);
                    sql += string.Format(" and tblsimulationreport.itemcode in ({0})", itemcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblsimulationreport.itemcode like '{0}%'", itemcode.ToUpper());
                }
            }

            if (mmodelcode.Trim().Length > 0)
            {
                if (mmodelcode.IndexOf(",") >= 0)
                {
                    string[] lists = mmodelcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mmodelcode = string.Join(",", lists);
                    sql += string.Format(" and tblmaterial.mmodelcode in ({0})", mmodelcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblmaterial.mmodelcode like '{0}%'", mmodelcode.ToUpper());
                }
            }

            if (bigsscode.Trim().Length > 0)
            {
                if (bigsscode.IndexOf(",") >= 0)
                {
                    string[] lists = bigsscode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigsscode = string.Join(",", lists);
                    sql += string.Format(" and tblss.bigsscode in ({0})", bigsscode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblss.bigsscode like '{0}%'", bigsscode.ToUpper());
                }
            }
            
            #endregion
            sql = " select count(rcard) from (" + sql + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
      
        public object[] QueryItemTracingForwordForSplitboard(string SNFrom, string SNTo, string moCode, string itemcode, string mmodelcode, string bigsscode, int inclusive, int exclusive)
        {
            //转换前的查询
            string tcardCondition = string.Empty;
            tcardCondition = FormatHelper.GetRCardRangeSql("tblonwipcardtrans.tcard", SNFrom.ToUpper(), SNTo.ToUpper());
            string tcardsql = string.Format(" select tblonwipcardtrans.* from tblonwipcardtrans where 1=1 {0} ", tcardCondition);
            object[] tObjects = this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer), new SQLCondition(tcardsql));
            if (tObjects == null || tObjects.Length == 0)
            {
                return null;
            }

            //  修改开始 2012-04-12 jack
            // 目的是：  从 tblsplitboard 中获取SMT多连板分板产生的序列号 以及原来的序列号
            string rcards = "";
            string SnConditionSpliteboard = string.Empty;
            foreach (OnWIPCardTransfer cardtrans in tObjects)
            {
                if (!string.IsNullOrEmpty(cardtrans.RunningCard))
                {
                    rcards += "'" + cardtrans.RunningCard.Trim().ToUpper() + "',";
                }
            }
            if (string.IsNullOrEmpty(rcards))
            {
                return null;
            }

            string RCARDS = rcards.Trim().Substring(0, rcards.Trim().Length - 1).ToUpper();

            string SqlSpliteboard = "";
            SqlSpliteboard += "   select distinct h.rcard  from (        ";
            SqlSpliteboard += "     select distinct tblsimulationreport.rcard      ";
            SqlSpliteboard += "    from tblsimulationreport  where 1 = 1     ";
            SqlSpliteboard += "    and    tblsimulationreport.rcard  in (  " + RCARDS + ")";

            SqlSpliteboard += "     union all     ";
            SqlSpliteboard += "     select distinct tblsplitboard.rcard      ";
            SqlSpliteboard += "       from tblsplitboard      where 1 = 1     ";
            SqlSpliteboard += "       and (tblsplitboard.rcard in (  " + RCARDS + "   )";
            SqlSpliteboard += "            or    ";
            SqlSpliteboard += "            tblsplitboard.scard in (   " + RCARDS + "  )";
            SqlSpliteboard += "           )     ";

            SqlSpliteboard += "     union all     ";
            SqlSpliteboard += "     select distinct tblsplitboard.scard  as rcard     ";
            SqlSpliteboard += "       from tblsplitboard      where 1 = 1     ";
            SqlSpliteboard += "       and (tblsplitboard.rcard in (  " + RCARDS + "  )";
            SqlSpliteboard += "            or    ";
            SqlSpliteboard += "            tblsplitboard.scard in (   " + RCARDS + "  )";
            SqlSpliteboard += "           )     ";

            SqlSpliteboard += "     )  h       ";

            object[] objsRcards = this.DataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(SqlSpliteboard));
            string sqlRcard = "";
            if (objsRcards == null || objsRcards.Length <= 0)
            {
                return null;
            }
            for (int j = 0; j < objsRcards.Length; j++)
            {
                if (!string.IsNullOrEmpty(((SimulationReport)objsRcards[j]).RunningCard))
                {
                    sqlRcard += "'" + ((SimulationReport)objsRcards[j]).RunningCard.Trim().ToUpper() + "',";
                }
            }

            if (string.IsNullOrEmpty(sqlRcard))
            {
                return null;
            }

            //转换后的查询
            string SnCondition = string.Empty;
            SnCondition = "  and tblsimulationreport.rcard in ( " + sqlRcard.Trim().Substring(0, sqlRcard.Trim().Length - 1) + " )";
            //  修改完成 2012-04-12 jack

            #region  未作改动的
            //新增工单代码查询
            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblsimulationreport.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblsimulationreport.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            if (itemcode.Trim().Length > 0)
            {
                if (itemcode.IndexOf(",") >= 0)
                {
                    string[] lists = itemcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemcode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblsimulationreport.itemcode in ({0})", itemcode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblsimulationreport.itemcode like '{0}%'", itemcode.ToUpper());
                }
            }

            if (mmodelcode.Trim().Length > 0)
            {
                if (mmodelcode.IndexOf(",") >= 0)
                {
                    string[] lists = mmodelcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mmodelcode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblmaterial.mmodelcode in ({0})", mmodelcode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblmaterial.mmodelcode like '{0}%'", mmodelcode.ToUpper());
                }
            }

            if (bigsscode.Trim().Length > 0)
            {
                if (bigsscode.IndexOf(",") >= 0)
                {
                    string[] lists = bigsscode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigsscode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblss.bigsscode in ({0})", bigsscode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblss.bigsscode like '{0}%'", bigsscode.ToUpper());
                }
            }

            string queryColnum = " TBLSIMULATIONREPORT.RESCODE || ' - ' || tblres.resdesc AS RESCODE,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.SSCODE || ' - ' || TBLSS.SSDESC AS SSCODE, TBLSIMULATIONREPORT.SEGCODE || ' - ' || tblseg.segdesc AS SEGCODE,";
            queryColnum += " TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, ";
            queryColnum += "  TBLSIMULATIONREPORT.ITEMCODE || ' - ' || tblitem.itemdesc AS ITEMCODE,  TBLSIMULATIONREPORT.muser || ' - ' || tbluser.username AS muser, TBLSIMULATIONREPORT.rcard,TBLSIMULATIONREPORT.tcard, ";
            queryColnum += " TBLSIMULATIONREPORT.mtime, TBLSIMULATIONREPORT.OPCODE || ' - ' || tblop.opdesc AS OPCODE, TBLSIMULATIONREPORT.mdate,TBLSIMULATIONREPORT.LACTION,'' as optype,tblmaterial.mmodelcode,tblss.bigsscode ";

            string joinSql = "  LEFT OUTER JOIN tblmaterial ON TBLSIMULATIONREPORT.Itemcode=tblmaterial.mcode ";
            joinSql += " LEFT OUTER JOIN tblss ON TBLSIMULATIONREPORT.Sscode=tblss.sscode";
            joinSql += " LEFT OUTER JOIN tblitem ON TBLSIMULATIONREPORT.itemcode = tblitem.itemcode";
            joinSql += " LEFT OUTER JOIN tblop ON TBLSIMULATIONREPORT.OPCODE = tblop.OPCODE";
            joinSql += " LEFT OUTER JOIN tblseg ON TBLSIMULATIONREPORT.Segcode = tblseg.segcode";
            joinSql += " LEFT OUTER JOIN tblres ON TBLSIMULATIONREPORT.rescode = tblres.rescode";
            joinSql += " LEFT OUTER JOIN tbluser ON TBLSIMULATIONREPORT.muser = tbluser.usercode";

            string sql = string.Format("select {0} from tblsimulationreport {1} where 1=1 ", queryColnum, joinSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemTracingQuery)));
            sql += SnCondition;
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemTracingQuery), new PagerCondition(sql, "tblsimulationreport.rcard", inclusive, exclusive));

            if (objs == null)
            {
                return null;
            }

            BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);


            for (int i = 0; i < objs.Length; i++)
            {
                ItemTracing objIT = objs[i] as ItemTracing;
                //added by jessie lee for CS0041,2005/10/11,P4.10
                //根据前面已经得到的rcard

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


                string tsSQL = string.Format(@"select scrapcause, frmtime, frmdate, frmuser, a.shifttypecode, shiftcode,
								tpcode, scardseq, frmmemo, tcardseq, tffullpath, shiftday, refrescode,
								transstatus, refopcode, a.COPCODE || ' - ' || tblop.opdesc AS COPCODE, refroutecode, crescode || ' - ' || tblres.resdesc as crescode, refmocode,
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
								frmroutecode, rcard, mocode from tblts a
                                LEFT OUTER JOIN tblres ON A.CRESCODE=tblres.rescode
                                LEFT OUTER JOIN tblop ON a.copcode=tblop.OPCODE
								where rcard = '{0}' and rcardseq = {1} and mocode = '{2}' and tsstatus <> '{3}'",
                    objIT.RCard,
                    objIT.RCardSeq,
                    objIT.MOCode,
                    TSStatus.TSStatus_New);

                Object[] tsObjs = this.DataProvider.CustomQuery(
                    typeof(BenQGuru.eMES.Domain.TS.TS),
                    new PagerCondition(tsSQL, inclusive, exclusive));

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

                if (string.Compare(objIT.ItemStatus, ProductStatus.NG, true) == 0)
                {
                    objIT.ItemStatus = TSStatus.TSStatus_New;
                }

                object op = bmFacade.GetOperation(objIT.OPCode);
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
                        object ir2o = this.GetItemRoute2Operation(
                            objIT.ItemCode,
                            objIT.RouteCode,
                            objIT.OPCode
                            );

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
            #endregion

            return objs;
        }

        public int QueryItemTracingForwordCountForSplitboard(string SNFrom, string SNTo, string moCode, string itemcode, string mmodelcode, string bigsscode)
        {
            string tcardCondition = string.Empty;
            tcardCondition = FormatHelper.GetRCardRangeSql("tblonwipcardtrans.tcard", SNFrom.ToUpper(), SNTo.ToUpper());
            string tcardsql = string.Format(" select tblonwipcardtrans.* from tblonwipcardtrans where 1=1 {0} ", tcardCondition);
            object[] tObjects = this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer), new SQLCondition(tcardsql));
            if (tObjects == null || tObjects.Length == 0) return 0;


            //  修改开始 2012-04-12 jack
            // 目的是：  从 tblsplitboard 中获取SMT多连板分板产生的序列号 以及原来的序列号
            string rcards = "";
            string SnConditionSpliteboard = string.Empty;
            foreach (OnWIPCardTransfer cardtrans in tObjects)
            {
                if (!string.IsNullOrEmpty(cardtrans.RunningCard))
                {
                    rcards += "'" + cardtrans.RunningCard.Trim().ToUpper() + "',";
                }
            }
            if (string.IsNullOrEmpty(rcards))
            {
                return 0;
            }

            string RCARDS = rcards.Trim().Substring(0, rcards.Trim().Length - 1).ToUpper();

            string SqlSpliteboard = "";
            SqlSpliteboard += "   select distinct h.rcard  from (        ";
            SqlSpliteboard += "     select distinct tblsimulationreport.rcard      ";
            SqlSpliteboard += "    from tblsimulationreport  where 1 = 1     ";
            SqlSpliteboard += "    and    tblsimulationreport.rcard  in (  " + RCARDS + ")";

            SqlSpliteboard += "     union all     ";
            SqlSpliteboard += "     select distinct tblsplitboard.rcard      ";
            SqlSpliteboard += "       from tblsplitboard      where 1 = 1     ";
            SqlSpliteboard += "       and (tblsplitboard.rcard in (  " + RCARDS + "   )";
            SqlSpliteboard += "            or    ";
            SqlSpliteboard += "            tblsplitboard.scard in (   " + RCARDS + "  )";
            SqlSpliteboard += "           )     ";

            SqlSpliteboard += "     union all     ";
            SqlSpliteboard += "     select distinct tblsplitboard.scard  as rcard     ";
            SqlSpliteboard += "       from tblsplitboard      where 1 = 1     ";
            SqlSpliteboard += "       and (tblsplitboard.rcard in (  " + RCARDS + "  )";
            SqlSpliteboard += "            or    ";
            SqlSpliteboard += "            tblsplitboard.scard in (   " + RCARDS + "  )";
            SqlSpliteboard += "           )     ";

            SqlSpliteboard += "     )  h       ";

            object[] objsRcards = this.DataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(SqlSpliteboard));
            string sqlRcard = "";
            if (objsRcards == null || objsRcards.Length <= 0)
            {
                return 0;
            }
            for (int j = 0; j < objsRcards.Length; j++)
            {
                if (!string.IsNullOrEmpty(((SimulationReport)objsRcards[j]).RunningCard))
                {
                    sqlRcard += "'" + ((SimulationReport)objsRcards[j]).RunningCard.Trim().ToUpper() + "',";
                }
            }

            if (string.IsNullOrEmpty(sqlRcard))
            {
                return 0;
            }

            //转换后的查询
            string SnCondition = string.Empty;
            SnCondition = "  and tblsimulationreport.rcard in ( " + sqlRcard.Trim().Substring(0, sqlRcard.Trim().Length - 1) + " )";
            //  修改完成 2012-04-12 jack

            #region   未作改动
            //新增工单代码查询
            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblsimulationreport.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblsimulationreport.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            if (itemcode.Trim().Length > 0)
            {
                if (itemcode.IndexOf(",") >= 0)
                {
                    string[] lists = itemcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemcode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblsimulationreport.itemcode in ({0})", itemcode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblsimulationreport.itemcode like '{0}%'", itemcode.ToUpper());
                }
            }

            if (mmodelcode.Trim().Length > 0)
            {
                if (mmodelcode.IndexOf(",") >= 0)
                {
                    string[] lists = mmodelcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mmodelcode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblmaterial.mmodelcode in ({0})", mmodelcode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblmaterial.mmodelcode like '{0}%'", mmodelcode.ToUpper());
                }
            }

            if (bigsscode.Trim().Length > 0)
            {
                if (bigsscode.IndexOf(",") >= 0)
                {
                    string[] lists = bigsscode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigsscode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblss.bigsscode in ({0})", bigsscode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblss.bigsscode like '{0}%'", bigsscode.ToUpper());
                }
            }

            string queryColnums = " TBLSIMULATIONREPORT.rescode,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.sscode, TBLSIMULATIONREPORT.segcode, TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, TBLSIMULATIONREPORT.itemcode,  TBLSIMULATIONREPORT.muser, TBLSIMULATIONREPORT.rcard,TBLSIMULATIONREPORT.tcard, TBLSIMULATIONREPORT.mtime, TBLSIMULATIONREPORT.opcode, TBLSIMULATIONREPORT.mdate,TBLSIMULATIONREPORT.LACTION,'' as optype,tblmaterial.mmodelcode,tblss.bigsscode";
            string joinSql = "  LEFT JOIN tblmaterial ON TBLSIMULATIONREPORT.Itemcode=tblmaterial.mcode ";
            joinSql += " LEFT JOIN tblss ON TBLSIMULATIONREPORT.Sscode=tblss.sscode";

            string sql = string.Format("select {0} from tblsimulationreport {1} where 1=1 ", queryColnums, joinSql);
            sql += SnCondition;
            #endregion 
           
            sql = "select count(rcard) from (" + sql + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        // jack end





        private string getQueryLikeSN(string SNFrom, string SNTo)
        {
            if (SNFrom != string.Empty && SNTo != string.Empty) return string.Empty;
            if (SNFrom == string.Empty && SNTo == string.Empty) return string.Empty;
            string returnStr = string.Empty;
            returnStr = SNFrom;
            if (SNTo != string.Empty) { returnStr = SNTo; }

            #region 截取like条件 已经注销,暂时不用此方法

            //			char splitchar = ' - ';							//分隔符(默认为- , 根据分隔符解析like条件)
            //			if(SNFrom !=string.Empty){returnStr = SNFrom; }
            //			else if(SNTo!=string.Empty){returnStr = SNTo;}
            //			else {return string.Empty;}
            //
            //			string[] splitStrs = returnStr.Split(splitchar);
            //			if(splitStrs.Length>2)
            //			{
            //				returnStr = splitStrs[0] + splitchar + splitStrs[1];
            //			}
            //			else
            //			{
            //				if(splitStrs[0].Length>9)				//如果无法分隔,默认取前9位数
            //				{
            //					returnStr = splitStrs[0].Substring(0,8);
            //				}
            //				else 
            //				{
            //					returnStr = splitStrs[0];
            //				}
            //			}

            #endregion

            return returnStr;
        }

        public int QueryItemTracingCount(string SNFrom, string SNTo, string moCode)
        {
            return QueryItemTracingCount(SNFrom, SNTo, moCode, string.Empty, string.Empty, string.Empty);
        }

        public int QueryItemTracingCount(string SNFrom, string SNTo, string moCode, string itemcode, string mmodelcode, string bigsscode)
        {
            string queryColnum = " TBLSIMULATIONREPORT.rescode,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.sscode, TBLSIMULATIONREPORT.segcode, TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, TBLSIMULATIONREPORT.itemcode,  TBLSIMULATIONREPORT.muser, TBLSIMULATIONREPORT.rcard,TBLSIMULATIONREPORT.tcard, TBLSIMULATIONREPORT.mtime, TBLSIMULATIONREPORT.opcode, TBLSIMULATIONREPORT.mdate,TBLSIMULATIONREPORT.LACTION,'' as optype,tblmaterial.mmodelcode,tblss.bigsscode ";
            string joinSql = "  LEFT JOIN tblmaterial ON TBLSIMULATIONREPORT.Itemcode=tblmaterial.mcode ";
            joinSql += " LEFT JOIN tblss ON TBLSIMULATIONREPORT.Sscode=tblss.sscode";
            string sql = string.Format("select {0} from tblsimulationreport {1} where 1=1 ", queryColnum, joinSql);

            string SnCondition = string.Empty;

            SnCondition = FormatHelper.GetRCardRangeSql("tblsimulationreport.rcard", SNFrom.ToUpper(), SNTo.ToUpper());
            sql += SnCondition;

            //if (moCode != string.Empty)
            //{
            //    sql += string.Format(" and mocode='{0}' ", moCode);
            //}

            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    sql += string.Format(" and tblsimulationreport.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblsimulationreport.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            if (itemcode.Trim().Length > 0)
            {
                if (itemcode.IndexOf(",") >= 0)
                {
                    string[] lists = itemcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemcode = string.Join(",", lists);
                    sql += string.Format(" and tblsimulationreport.itemcode in ({0})", itemcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblsimulationreport.itemcode like '{0}%'", itemcode.ToUpper());
                }
            }

            if (mmodelcode.Trim().Length > 0)
            {
                if (mmodelcode.IndexOf(",") >= 0)
                {
                    string[] lists = mmodelcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mmodelcode = string.Join(",", lists);
                    sql += string.Format(" and tblmaterial.mmodelcode in ({0})", mmodelcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblmaterial.mmodelcode like '{0}%'", mmodelcode.ToUpper());
                }
            }

            if (bigsscode.Trim().Length > 0)
            {
                if (bigsscode.IndexOf(",") >= 0)
                {
                    string[] lists = bigsscode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigsscode = string.Join(",", lists);
                    sql += string.Format(" and tblss.bigsscode in ({0})", bigsscode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and tblss.bigsscode like '{0}%'", bigsscode.ToUpper());
                }
            }
            sql = " select count(rcard) from (" + sql + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// form ItemFacade
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="routeCode"></param>
        /// <param name="opCode"></param>
        /// <returns></returns>
        public object GetItemRoute2Operation(string itemCode, string routeCode, string opCode)
        {
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op where itemcode=$itemcode and routecode=$routecode and opcode =$opcode" + GlobalVariables.CurrentOrganizations.GetSQLCondition();
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLParamCondition(selectSql, new SQLParameter[] { new SQLParameter("itemcode",typeof(string),itemCode),new SQLParameter("routecode",typeof(string),routeCode),
                                                                                                                                      new SQLParameter("opcode",typeof(string), opCode)}));
            if (objs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemRoute_NotExisted");
            }
            return objs[0];
        }


        public object[] QueryItemTracing(string SNFrom, string SNTo, int inclusive, int exclusive)
        {
            return this.QueryItemTracing(SNFrom, SNTo, string.Empty, inclusive, exclusive);
        }

        public int QueryItemTracingCount(string SNFrom, string SNTo)
        {
            return this.QueryItemTracingCount(SNFrom, SNTo, string.Empty);
        }

        public object[] QueryItemTracingForword(string SNFrom, string SNTo, int inclusive, int exclusive)
        {
            return QueryItemTracingForword(SNFrom, SNTo, string.Empty, string.Empty, string.Empty, inclusive, exclusive);
        }

        public object[] QueryItemTracingForword(string SNFrom, string SNTo, string itemcode, string mmodelcode, string bigsscode, int inclusive, int exclusive)
        {
            return QueryItemTracingForword(SNFrom, SNTo, string.Empty, itemcode, mmodelcode, bigsscode, inclusive, exclusive);
        }

        public object[] QueryItemTracingForword(string SNFrom, string SNTo, string moCode, string itemcode, string mmodelcode, string bigsscode, int inclusive, int exclusive)
        {
            //转换前的查询
            string tcardCondition = string.Empty;
            tcardCondition = FormatHelper.GetRCardRangeSql("tblonwipcardtrans.tcard", SNFrom.ToUpper(), SNTo.ToUpper());
            string tcardsql = string.Format(" select tblonwipcardtrans.* from tblonwipcardtrans where 1=1 {0} ", tcardCondition);
            object[] tObjects = this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer), new SQLCondition(tcardsql));
            if (tObjects == null || tObjects.Length == 0) return null;

            ArrayList rcardList = new ArrayList();
            foreach (OnWIPCardTransfer cardtrans in tObjects)
            {
                rcardList.Add(cardtrans.RunningCard);
            }

            //转换后的查询
            string SnCondition = string.Empty;
            SnCondition = "and tblsimulationreport.rcard in ('" + String.Join("','", (string[])rcardList.ToArray(typeof(string))) + "')";

            //新增工单代码查询
            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblsimulationreport.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblsimulationreport.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            if (itemcode.Trim().Length > 0)
            {
                if (itemcode.IndexOf(",") >= 0)
                {
                    string[] lists = itemcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemcode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblsimulationreport.itemcode in ({0})", itemcode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblsimulationreport.itemcode like '{0}%'", itemcode.ToUpper());
                }
            }

            if (mmodelcode.Trim().Length > 0)
            {
                if (mmodelcode.IndexOf(",") >= 0)
                {
                    string[] lists = mmodelcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mmodelcode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblmaterial.mmodelcode in ({0})", mmodelcode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblmaterial.mmodelcode like '{0}%'", mmodelcode.ToUpper());
                }
            }

            if (bigsscode.Trim().Length > 0)
            {
                if (bigsscode.IndexOf(",") >= 0)
                {
                    string[] lists = bigsscode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigsscode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblss.bigsscode in ({0})", bigsscode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblss.bigsscode like '{0}%'", bigsscode.ToUpper());
                }
            }

            string queryColnum = " TBLSIMULATIONREPORT.RESCODE || ' - ' || tblres.resdesc AS RESCODE,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.SSCODE || ' - ' || TBLSS.SSDESC AS SSCODE, TBLSIMULATIONREPORT.SEGCODE || ' - ' || tblseg.segdesc AS SEGCODE,";
            queryColnum += " TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, ";
            queryColnum += "  TBLSIMULATIONREPORT.ITEMCODE || ' - ' || tblitem.itemdesc AS ITEMCODE,  TBLSIMULATIONREPORT.muser || ' - ' || tbluser.username AS muser, TBLSIMULATIONREPORT.rcard,TBLSIMULATIONREPORT.tcard, ";
            queryColnum += " TBLSIMULATIONREPORT.mtime, TBLSIMULATIONREPORT.OPCODE || ' - ' || tblop.opdesc AS OPCODE, TBLSIMULATIONREPORT.mdate,TBLSIMULATIONREPORT.LACTION,'' as optype,tblmaterial.mmodelcode,tblss.bigsscode ";

            string joinSql = "  LEFT OUTER JOIN tblmaterial ON TBLSIMULATIONREPORT.Itemcode=tblmaterial.mcode ";
            joinSql += " LEFT OUTER JOIN tblss ON TBLSIMULATIONREPORT.Sscode=tblss.sscode";
            joinSql += " LEFT OUTER JOIN tblitem ON TBLSIMULATIONREPORT.itemcode = tblitem.itemcode";
            joinSql += " LEFT OUTER JOIN tblop ON TBLSIMULATIONREPORT.OPCODE = tblop.OPCODE";
            joinSql += " LEFT OUTER JOIN tblseg ON TBLSIMULATIONREPORT.Segcode = tblseg.segcode";
            joinSql += " LEFT OUTER JOIN tblres ON TBLSIMULATIONREPORT.rescode = tblres.rescode";
            joinSql += " LEFT OUTER JOIN tbluser ON TBLSIMULATIONREPORT.muser = tbluser.usercode";

            string sql = string.Format("select {0} from tblsimulationreport {1} where 1=1 ", queryColnum, joinSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemTracingQuery)));
            sql += SnCondition;
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemTracingQuery), new PagerCondition(sql, "tblsimulationreport.rcard", inclusive, exclusive));

            if (objs == null)
            {
                return null;
            }

            BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);


            for (int i = 0; i < objs.Length; i++)
            {
                ItemTracing objIT = objs[i] as ItemTracing;
                //added by jessie lee for CS0041,2005/10/11,P4.10
                //根据前面已经得到的rcard

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


                string tsSQL = string.Format(@"select scrapcause, frmtime, frmdate, frmuser, a.shifttypecode, shiftcode,
								tpcode, scardseq, frmmemo, tcardseq, tffullpath, shiftday, refrescode,
								transstatus, refopcode, a.COPCODE || ' - ' || tblop.opdesc AS COPCODE, refroutecode, crescode || ' - ' || tblres.resdesc as crescode, refmocode,
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
								frmroutecode, rcard, mocode from tblts a
                                LEFT OUTER JOIN tblres ON A.CRESCODE=tblres.rescode
                                LEFT OUTER JOIN tblop ON a.copcode=tblop.OPCODE
								where rcard = '{0}' and rcardseq = {1} and mocode = '{2}' and tsstatus <> '{3}'",
                    objIT.RCard,
                    objIT.RCardSeq,
                    objIT.MOCode,
                    TSStatus.TSStatus_New);

                Object[] tsObjs = this.DataProvider.CustomQuery(
                    typeof(BenQGuru.eMES.Domain.TS.TS),
                    new PagerCondition(tsSQL, inclusive, exclusive));

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

                if (string.Compare(objIT.ItemStatus, ProductStatus.NG, true) == 0)
                {
                    objIT.ItemStatus = TSStatus.TSStatus_New;
                }

                object op = bmFacade.GetOperation(objIT.OPCode);
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
                        object ir2o = this.GetItemRoute2Operation(
                            objIT.ItemCode,
                            objIT.RouteCode,
                            objIT.OPCode
                            );

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

        public int QueryItemTracingForwordCount(string SNFrom, string SNTo)
        {
            return QueryItemTracingForwordCount(SNFrom, SNTo, string.Empty, string.Empty, string.Empty);
        }

        public int QueryItemTracingForwordCount(string SNFrom, string SNTo, string itemcode, string mmodelcode, string bigsscode)
        {
            return QueryItemTracingForwordCount(SNFrom, SNTo, string.Empty, itemcode, mmodelcode, bigsscode);
        }

        public int QueryItemTracingForwordCount(string SNFrom, string SNTo, string moCode, string itemcode, string mmodelcode, string bigsscode)
        {
            string tcardCondition = string.Empty;
            tcardCondition = FormatHelper.GetRCardRangeSql("tblonwipcardtrans.tcard", SNFrom.ToUpper(), SNTo.ToUpper());
            string tcardsql = string.Format(" select tblonwipcardtrans.* from tblonwipcardtrans where 1=1 {0} ", tcardCondition);
            object[] tObjects = this.DataProvider.CustomQuery(typeof(OnWIPCardTransfer), new SQLCondition(tcardsql));
            if (tObjects == null || tObjects.Length == 0) return 0;

            ArrayList rcardList = new ArrayList();
            foreach (OnWIPCardTransfer cardtrans in tObjects)
            {
                rcardList.Add(cardtrans.RunningCard);
            }

            //转换后的查询
            string SnCondition = string.Empty;
            SnCondition = "and tblsimulationreport.rcard in ('" + String.Join("','", (string[])rcardList.ToArray(typeof(string))) + "')";

            //新增工单代码查询
            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblsimulationreport.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblsimulationreport.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            if (itemcode.Trim().Length > 0)
            {
                if (itemcode.IndexOf(",") >= 0)
                {
                    string[] lists = itemcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemcode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblsimulationreport.itemcode in ({0})", itemcode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblsimulationreport.itemcode like '{0}%'", itemcode.ToUpper());
                }
            }

            if (mmodelcode.Trim().Length > 0)
            {
                if (mmodelcode.IndexOf(",") >= 0)
                {
                    string[] lists = mmodelcode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mmodelcode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblmaterial.mmodelcode in ({0})", mmodelcode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblmaterial.mmodelcode like '{0}%'", mmodelcode.ToUpper());
                }
            }

            if (bigsscode.Trim().Length > 0)
            {
                if (bigsscode.IndexOf(",") >= 0)
                {
                    string[] lists = bigsscode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigsscode = string.Join(",", lists);
                    SnCondition += string.Format(" and tblss.bigsscode in ({0})", bigsscode.ToUpper());
                }
                else
                {
                    SnCondition += string.Format(" and tblss.bigsscode like '{0}%'", bigsscode.ToUpper());
                }
            }

            string queryColnums = " TBLSIMULATIONREPORT.rescode,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.sscode, TBLSIMULATIONREPORT.segcode, TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, TBLSIMULATIONREPORT.itemcode,  TBLSIMULATIONREPORT.muser, TBLSIMULATIONREPORT.rcard,TBLSIMULATIONREPORT.tcard, TBLSIMULATIONREPORT.mtime, TBLSIMULATIONREPORT.opcode, TBLSIMULATIONREPORT.mdate,TBLSIMULATIONREPORT.LACTION,'' as optype,tblmaterial.mmodelcode,tblss.bigsscode";
            string joinSql = "  LEFT JOIN tblmaterial ON TBLSIMULATIONREPORT.Itemcode=tblmaterial.mcode ";
            joinSql += " LEFT JOIN tblss ON TBLSIMULATIONREPORT.Sscode=tblss.sscode";

            string sql = string.Format("select {0} from tblsimulationreport {1} where 1=1 ", queryColnums, joinSql);
            sql += SnCondition;
            sql = "select count(rcard) from (" + sql + ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object GetItemTracing(string moCode, string sn)
        {
            string sql = string.Format(
                "select TBLSIMULATIONREPORT.rescode,TBLSIMULATIONREPORT.rcardseq, TBLSIMULATIONREPORT.sscode, TBLSIMULATIONREPORT.segcode, TBLSIMULATIONREPORT.routecode, TBLSIMULATIONREPORT.modelcode, TBLSIMULATIONREPORT.mocode, TBLSIMULATIONREPORT.status, TBLSIMULATIONREPORT.itemcode,  TBLSIMULATIONREPORT.muser, TBLSIMULATIONREPORT.rcard, TBLSIMULATIONREPORT.mtime, TBLSIMULATIONREPORT.opcode, TBLSIMULATIONREPORT.mdate,tblop.opcontrol as optype from tblsimulationreport,tblop where  tblop.opcode=tblsimulationreport.opcode ",
                DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(ItemTracing))
            );

            if (moCode != string.Empty)
            {
                sql += " and tblsimulationreport.mocode = '" + moCode + "'";
            }

            if (sn != string.Empty)
            {
                sql += "and tblsimulationreport.rcard = '" + sn + "'";
            }
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemTracing), new SQLCondition(sql));
            if (objs == null)
            {
                return null;
            }

            return objs[0];

        }


        //从TBLSIMULATIONREPORT 中取得rcard对应的工单
        //返回的结果为在制的工单 和 返工前的工单(如果有返工)
        public object[] GetMOByRcard(string rcard)
        {
            if (rcard.Trim() == string.Empty) return null;
            string sql = string.Format("select tblmo.* from tblmo where mocode in (select mocode from tblonwip where rcard = '{0}')" + GlobalVariables.CurrentOrganizations.GetSQLCondition(), rcard);
            object[] moObjs = this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(sql));
            return moObjs;
        }

        #endregion

        #region ProductionProcess
        public object GetProductionProcess(string moCode, string sn, int seq)
        {
            string sql = "select tblonwip.mdate, tblonwip.rescode, tblonwip.sscode,"
                + " tblonwip.segcode, tblonwip.mocode, tblonwip.opcode,"
                + " tblonwip.routecode, tblonwip.rcardseq, tblonwip.action,"
                + " tblonwip.rcard, tblonwip.muser, tblonwip.actionresult,"
                + " tblonwip.mtime,tblonwip.modelcode,tblonwip.itemcode,"
                + " tblop.opcontrol as optype"
                + " from tblonwip,tblop where tblop.opcode=tblonwip.opcode";

            if (moCode != string.Empty)
            {
                sql += " and tblonwip.mocode = '" + moCode + "' ";
            }

            if (sn != string.Empty)
            {
                sql += " and tblonwip.rcard = '" + sn + "'";
            }

            if (seq > -1)
            {
                sql += " and tblonwip.rcardseq = '" + seq + "'";
            }


            object[] objs = this.DataProvider.CustomQuery(typeof(ProductionProcess), new SQLCondition(sql));
            if (objs != null)
            {
                if (objs[0] != null)
                {
                    return objs[0];
                }
            }

            return null;
        }

        /// <summary>
        /// modified by jessie lee,2005/9/13
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="sn"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryProductionProcess(string moCode, string rCard, int inclusive, int exclusive)
        {
            //tCard = string.Empty;
            string sql = string.Empty;

            sql = "select tblonwip.mdate, TBLONWIP.RESCODE || ' - ' || tblres.resdesc AS RESCODE, TBLONWIP.SSCODE || ' - ' || tblss.ssdesc AS SSCODE,"
                + " TBLONWIP.SEGCODE || ' - ' || tblseg.segdesc AS SEGCODE, tblonwip.mocode, TBLONWIP.OPCODE || ' - ' || TBLOP.Opdesc AS OPCODE,"
                + " tblonwip.routecode, tblonwip.rcardseq, tblonwip.action,"
                + " tblonwip.rcard, TBLONWIP.MUSER || ' - ' || tbluser.username AS MUSER, tblonwip.actionresult,"
                + " tblonwip.mtime,tblonwip.modelcode,tblonwip.itemcode,"
                + " tblop.opcontrol as optype"
                + " from tblonwip,tblop,tblres,tblss,tblseg,tbluser where tblop.opcode=tblonwip.opcode"
                + " AND TBLONWIP.Rescode=tblres.rescode(+) "
                + " AND TBLONWIP.Sscode=tblss.sscode(+)"
                + " AND TBLONWIP.segcode=tblseg.segcode(+)"
                + " AND TBLONWIP.Muser=tbluser.usercode(+) ";

            if (moCode != string.Empty)
            {
                sql += " and tblonwip.mocode = '" + moCode + "' ";
            }

            ArrayList array = new ArrayList();
            if (rCard != string.Empty)
            {
                array.Add(rCard);
                CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
                castDownHelper.GetAllRCard(ref array, rCard);

                string[] rCards = (string[])array.ToArray(typeof(System.String));

                if (rCards.Length == 1)
                {
                    sql += " and tblonwip.rcard = '" + rCard + "'";
                }
                else
                {
                    sql += string.Format(" and tblonwip.rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
                }
            }
            //added by jessie lee,2005-9-13
            //把维修信息添加进ProductionProcess
            //拼sql联合，无奈啊
            sql += " union ";

            sql += string.Format(@"select decode(confirmdate,null,tblts.mdate,0,tblts.mdate,confirmdate),
                        tblts.crescode || ' - ' || tblres.resdesc as  crescode,
                        '',
                        '',
                        mocode,
                        tblts.copcode || ' - ' || tblop.opdesc as  copcode,
                        '',
                        rcardseq,
                        'TS',
                        rcard,
                        tblts.muser || ' - ' || tbluser.username as  muser,
                        tsstatus,
                        decode(confirmtime,null,tblts.mtime+1,0,tblts.mtime+1,confirmtime),
                        '',
                        '',
                        'TS'
                    from tblts  LEFT OUTER JOIN tblres ON TBLTS.Crescode=tblres.rescode 
                                LEFT OUTER JOIN tblop ON TBLTS.COPCODE=tblop.OPCODE
                                LEFT OUTER JOIN tbluser  ON TBLTS.MUSER=tbluser.usercode 
                   where tsstatus<>'{0}' and tsstatus<>'{1}' ", TSStatus.TSStatus_New, TSStatus.TSStatus_RepeatNG);//modified by jessie Lee, 2005/12/6
            if (moCode != string.Empty)
            {
                sql += " and mocode = '" + moCode + "' ";
            }

            if (rCard != string.Empty)
            {
                string[] rCards = (string[])array.ToArray(typeof(System.String));
                if (rCards.Length == 1)
                {
                    sql += " and rcard = '" + rCard + "'";
                }
                else
                {
                    sql += string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
                }
            }

            sql = string.Format(" select {0} from ( {1} ) ",
                @"  mdate,
				rescode,
				sscode,
				segcode,
				mocode,
				opcode,
				routecode,
				rcardseq,
				action,
				rcard,
				muser,
				actionresult,
				mtime,
				modelcode,
				itemcode,
				optype ",
                sql);

            object[] objs = this.DataProvider.CustomQuery(typeof(ProductionProcess), new PagerCondition(sql, "mdate*1000000+mtime desc,rcardseq desc", inclusive, exclusive));
            BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);


            for (int i = 0; i < objs.Length; i++)
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
                ProductionProcess objproProcess = objs[i] as ProductionProcess;
                if (string.Compare(objproProcess.ItemStatus, ProductStatus.NG, true) == 0)
                {
                    objproProcess.ItemStatus = TSStatus.TSStatus_New;
                }
                //end by jessie lee

                object op = bmFacade.GetOperation(((ProductionProcess)objs[i]).OPCode);

                if (op == null)
                {
                    ((ProductionProcess)objs[i]).OPType = string.Empty;
                }
                else
                {
                    if (((Operation)op).OPControl[(int)BenQGuru.eMES.BaseSetting.OperationList.OutsideRoute] == '1')
                    {
                        ((ProductionProcess)objs[i]).OPType = ((Operation)op).OPControl;
                    }
                    else if (((ProductionProcess)objs[i]).OPType == "TS")//如果Opcode为TS，说明在维修，不做修改
                    {
                    }
                    else
                    {
                        object ir2o = this.GetItemRoute2Operation(
                            ((ProductionProcess)objs[i]).ItemCode,
                            ((ProductionProcess)objs[i]).RouteCode,
                            ((ProductionProcess)objs[i]).OPCode
                            );

                        if (ir2o == null)
                        {
                            ((ProductionProcess)objs[i]).OPType = string.Empty;
                        }
                        else
                        {
                            ((ProductionProcess)objs[i]).OPType = ((ItemRoute2OP)ir2o).OPControl;
                        }
                    }
                }
            }


            return objs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="sn"></param>
        /// <returns></returns>
        public int QueryProductionProcessCount(string moCode, string rCard)
        {
            string sql = string.Empty;
            string sql2 = string.Empty;

            ArrayList array = new ArrayList();
            array.Add(rCard);
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            castDownHelper.GetAllRCard(ref array, rCard);

            string[] rCards = (string[])array.ToArray(typeof(System.String));

            sql = "select count(rcard) from tblonwip where 1=1";

            if (moCode != string.Empty)
            {
                sql += " and mocode = '" + moCode + "'";
            }

            if (rCard != string.Empty)
            {
                sql += " and rcard in (" + FormatHelper.ProcessQueryValues(rCards) + ")";
            }

            sql2 = "select count(rcard) from tblts where 1=1";

            if (moCode != string.Empty)
            {
                sql2 += " and mocode = '" + moCode + "'";
            }

            if (rCard != string.Empty)
            {
                sql2 += " and rcard in (" + FormatHelper.ProcessQueryValues(rCards) + ")";
            }
            // Added by Icyer 2007/03/15
            sql2 += string.Format(" and tsstatus<>'{0}' and tsstatus<>'{1}' ", TSStatus.TSStatus_New, TSStatus.TSStatus_RepeatNG);

            return this.DataProvider.GetCount(new SQLCondition(sql)) + this.DataProvider.GetCount(new SQLCondition(sql2));

        }

        //  jack add 2012-04-12 start

        public string  QueryRcardsInOnwipOrSplitboard(string Rcard)
        {
            if (string.IsNullOrEmpty(Rcard))
            {
                return string.Empty;
            }
            string returnRcards = "";
            string Sql = "";
            Sql += "   select distinct h.rcard  from (         ";

            Sql += "     select distinct tblsplitboard.rcard      ";
            Sql += "       from tblsplitboard      where 1 = 1     ";
            Sql += "       and (tblsplitboard.rcard = '" + FormatHelper.CleanString(Rcard).ToUpper() + "'";
            Sql += "           or    ";
            Sql += "           tblsplitboard.scard = '" + FormatHelper.CleanString(Rcard).ToUpper() + "'";
            Sql += "           )       ";

            Sql += "     union all     ";
            Sql += "     select distinct tblsplitboard.scard  as rcard     ";
            Sql += "       from tblsplitboard      where 1 = 1     ";
            Sql += "       and (tblsplitboard.rcard = '" + FormatHelper.CleanString(Rcard).ToUpper() + "'";
            Sql += "           or    ";
            Sql += "           tblsplitboard.scard = '" + FormatHelper.CleanString(Rcard).ToUpper() + "'";
            Sql += "           )       ";

            Sql += "     )  h       ";

            object[] objs = this.DataProvider.CustomQuery(typeof(Smtrelationqty), new SQLCondition(Sql));
           
            returnRcards += "'" + FormatHelper.CleanString(Rcard).Trim().ToUpper() + "',";
            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    returnRcards += "'" + ((Smtrelationqty)objs[i]).Rcard.Trim().ToUpper() + "',";
                }
            }
            
            returnRcards = returnRcards.Trim().Substring(0, returnRcards.Trim().Length - 1).ToUpper();
            return returnRcards;
        }


        public object[] QueryProductionProcessForSplitboard(string moCode, string rCard, int inclusive, int exclusive)
        {
            if (string.IsNullOrEmpty(rCard))
            {
                return null;
            }


            //tCard = string.Empty;
            string sql = string.Empty;

            sql = "select tblonwip.mdate, TBLONWIP.RESCODE || ' - ' || tblres.resdesc AS RESCODE, TBLONWIP.SSCODE || ' - ' || tblss.ssdesc AS SSCODE,"
                + " TBLONWIP.SEGCODE || ' - ' || tblseg.segdesc AS SEGCODE, tblonwip.mocode, TBLONWIP.OPCODE || ' - ' || TBLOP.Opdesc AS OPCODE,"
                + " tblonwip.routecode, tblonwip.rcardseq, tblonwip.action,"
                + " tblonwip.rcard, TBLONWIP.MUSER || ' - ' || tbluser.username AS MUSER, tblonwip.actionresult,"
                + " tblonwip.mtime,tblonwip.modelcode,tblonwip.itemcode,"
                + " tblop.opcontrol as optype"
                + " from tblonwip,tblop,tblres,tblss,tblseg,tbluser where tblop.opcode=tblonwip.opcode"
                + " AND TBLONWIP.Rescode=tblres.rescode(+) "
                + " AND TBLONWIP.Sscode=tblss.sscode(+)"
                + " AND TBLONWIP.segcode=tblseg.segcode(+)"
                + " AND TBLONWIP.Muser=tbluser.usercode(+) ";

            if (moCode != string.Empty)
            {
                sql += " and tblonwip.mocode = '" + moCode + "' ";
            }

           // ArrayList array = new ArrayList();
            if (rCard != string.Empty)
            {
                sql += string.Format(" and tblonwip.rcard in ( {0} ) ", QueryRcardsInOnwipOrSplitboard(rCard));
              
                //array.Add(rCard);
                //CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
                //castDownHelper.GetAllRCard(ref array, rCard);

                //string[] rCards = (string[])array.ToArray(typeof(System.String));

                //if (rCards.Length == 1)
                //{
                //    sql += " and tblonwip.rcard = '" + rCard + "'";
                //}
                //else
                //{
                //    sql += string.Format(" and tblonwip.rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
                //}
            }
            //added by jessie lee,2005-9-13
            //把维修信息添加进ProductionProcess
            //拼sql联合，无奈啊
            sql += " union ";

            sql += string.Format(@"select decode(confirmdate,null,tblts.mdate,0,tblts.mdate,confirmdate),
                        tblts.crescode || ' - ' || tblres.resdesc as  crescode,
                        '',
                        '',
                        mocode,
                        tblts.copcode || ' - ' || tblop.opdesc as  copcode,
                        '',
                        rcardseq,
                        'TS',
                        rcard,
                        tblts.muser || ' - ' || tbluser.username as  muser,
                        tsstatus,
                        decode(confirmtime,null,tblts.mtime+1,0,tblts.mtime+1,confirmtime),
                        '',
                        '',
                        'TS'
                    from tblts  LEFT OUTER JOIN tblres ON TBLTS.Crescode=tblres.rescode 
                                LEFT OUTER JOIN tblop ON TBLTS.COPCODE=tblop.OPCODE
                                LEFT OUTER JOIN tbluser  ON TBLTS.MUSER=tbluser.usercode 
                   where tsstatus<>'{0}' and tsstatus<>'{1}' ", TSStatus.TSStatus_New, TSStatus.TSStatus_RepeatNG);//modified by jessie Lee, 2005/12/6
            if (moCode != string.Empty)
            {
                sql += " and mocode = '" + moCode + "' ";
            }

            if (rCard != string.Empty)
            {
                sql += string.Format(" and rcard in ( {0} ) ", QueryRcardsInOnwipOrSplitboard(rCard));
                //string[] rCards = (string[])array.ToArray(typeof(System.String));
                //if (rCards.Length == 1)
                //{
                //    sql += " and rcard = '" + rCard + "'";
                //}
                //else
                //{
                //    sql += string.Format(" and rcard in ({0}) ", FormatHelper.ProcessQueryValues(rCards));
                //}
            }



            #region  未作改动
            sql = string.Format(" select {0} from ( {1} ) ",
                @"  mdate,
				rescode,
				sscode,
				segcode,
				mocode,
				opcode,
				routecode,
				rcardseq,
				action,
				rcard,
				muser,
				actionresult,
				mtime,
				modelcode,
				itemcode,
				optype ",
                sql);

            object[] objs = this.DataProvider.CustomQuery(typeof(ProductionProcess), new PagerCondition(sql, "mdate*1000000+mtime desc,rcardseq desc", inclusive, exclusive));
            BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);

           
            for (int i = 0; i < objs.Length; i++)
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
                ProductionProcess objproProcess = objs[i] as ProductionProcess;
                if (string.Compare(objproProcess.ItemStatus, ProductStatus.NG, true) == 0)
                {
                    objproProcess.ItemStatus = TSStatus.TSStatus_New;
                }
                //end by jessie lee

                object op = bmFacade.GetOperation(((ProductionProcess)objs[i]).OPCode);

                if (op == null)
                {
                    ((ProductionProcess)objs[i]).OPType = string.Empty;
                }
                else
                {
                    if (((Operation)op).OPControl[(int)BenQGuru.eMES.BaseSetting.OperationList.OutsideRoute] == '1')
                    {
                        ((ProductionProcess)objs[i]).OPType = ((Operation)op).OPControl;
                    }
                    else if (((ProductionProcess)objs[i]).OPType == "TS")//如果Opcode为TS，说明在维修，不做修改
                    {
                    }
                    else
                    {
                        object ir2o = this.GetItemRoute2Operation(
                            ((ProductionProcess)objs[i]).ItemCode,
                            ((ProductionProcess)objs[i]).RouteCode,
                            ((ProductionProcess)objs[i]).OPCode
                            );

                        if (ir2o == null)
                        {
                            ((ProductionProcess)objs[i]).OPType = string.Empty;
                        }
                        else
                        {
                            ((ProductionProcess)objs[i]).OPType = ((ItemRoute2OP)ir2o).OPControl;
                        }
                    }
                }
            }
            #endregion

            return objs;
        }

        public int QueryProductionProcessCountForSplitboard(string moCode, string rCard)
        {
            string sql = string.Empty;
            string sql2 = string.Empty;

            ArrayList array = new ArrayList();
            array.Add(rCard);
            CastDownHelper castDownHelper = new CastDownHelper(this.DataProvider);
            castDownHelper.GetAllRCard(ref array, rCard);

            string[] rCards = (string[])array.ToArray(typeof(System.String));

            sql = "select count(rcard) from tblonwip where 1=1";

            if (moCode != string.Empty)
            {
                sql += " and mocode = '" + moCode + "'";
            }

            if (rCard != string.Empty)
            {
                sql += string.Format(" and tblonwip.rcard in ( {0} ) ", QueryRcardsInOnwipOrSplitboard(rCard));
               
               // sql += " and rcard in (" + FormatHelper.ProcessQueryValues(rCards) + ")";
            }

            sql2 = "select count(rcard) from tblts where 1=1";

            if (moCode != string.Empty)
            {
                sql2 += " and mocode = '" + moCode + "'";
            }

            if (rCard != string.Empty)
            {
                sql2 += string.Format(" and rcard in ( {0} ) ", QueryRcardsInOnwipOrSplitboard(rCard));
              //  sql2 += " and rcard in (" + FormatHelper.ProcessQueryValues(rCards) + ")";
            }
            // Added by Icyer 2007/03/15
            sql2 += string.Format(" and tsstatus<>'{0}' and tsstatus<>'{1}' ", TSStatus.TSStatus_New, TSStatus.TSStatus_RepeatNG);

            return this.DataProvider.GetCount(new SQLCondition(sql)) + this.DataProvider.GetCount(new SQLCondition(sql2));

        }
        //jack add  2012-04-12  end 
        #endregion

        #region OPResult

        public object[] QueryOPResult(string moCode, string rCard, int rCardSeq, string[] actions, int inclusive, int exclusive)
        {
            string sql = "select tblonwip.mdate, tblonwip.rescode || ' - ' || tblres.resdesc as rescode, tblonwip.sscode || ' - ' || tblss.ssdesc as sscode,"
                + " tblonwip.segcode || ' - ' || tblseg.segdesc as segcode, tblonwip.mocode, tblonwip.opcode || ' - ' || tblop.opdesc as opcode,"
                + " tblonwip.routecode, tblonwip.rcardseq, tblonwip.action,"
                + " tblonwip.rcard, tblonwip.muser || ' - ' || tbluser.username as muser, tblonwip.actionresult,"
                + " tblonwip.mtime,tblonwip.modelcode,tblonwip.shelfno, tblonwip.itemcode,'' as optype"
                + " from tblonwip left outer join tblop on tblonwip.opcode=tblop.opcode "
                + " left outer join tblseg on tblonwip.segcode=tblseg.segcode "
                + " left outer join tblss on tblonwip.sscode=tblss.sscode "
                + " left outer join tblres on tblonwip.rescode=tblres.rescode "
                + " left outer join tbluser on tblonwip.muser=tbluser.usercode "
                + "where 1=1 ";

            if (moCode != string.Empty)
            {
                sql += " and tblonwip.mocode = '" + moCode + "' ";
            }

            if (rCard != string.Empty)
            {
                sql += " and tblonwip.rcard = '" + rCard + "'";
            }

            if (rCardSeq >= 0)
            {
                sql += " and tblonwip.rcardseq = " + rCardSeq;
            }

            if (actions != null)
            {
                if (actions.Length > 0)
                {
                    sql += " and tblonwip.action in ('" + string.Join("','", actions) + "') ";
                }
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(OPResult), new PagerCondition(sql, "rcardseq", inclusive, exclusive));

            if (objs == null)
            {
                return null;
            }


            BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);


            for (int i = 0; i < objs.Length; i++)
            {

                object op = bmFacade.GetOperation(((OPResult)objs[i]).OPCode);

                if (op == null)
                {
                    ((OPResult)objs[i]).OPType = string.Empty;
                }
                else
                {

                    if (((Operation)op).OPControl[(int)BenQGuru.eMES.BaseSetting.OperationList.OutsideRoute] == '1')
                    {
                        ((OPResult)objs[i]).OPType = ((Operation)op).OPControl;
                    }
                    else
                    {
                        object ir2o = this.GetItemRoute2Operation(
                            ((OPResult)objs[i]).ItemCode,
                            ((OPResult)objs[i]).RouteCode,
                            ((OPResult)objs[i]).OPCode
                            );

                        if (ir2o == null)
                        {
                            ((OPResult)objs[i]).OPType = string.Empty;
                        }
                        else
                        {
                            ((OPResult)objs[i]).OPType = ((ItemRoute2OP)ir2o).OPControl;
                        }
                    }
                }
            }

            return objs;


        }


        public int QueryOPResultCount(string moCode, string sn, int rCardSeq, string[] actions)
        {
            string sql = "select count(rcard) from tblonwip where 1=1";

            if (moCode != string.Empty)
            {
                sql += " and mocode = '" + moCode + "'";
            }

            if (sn != string.Empty)
            {
                sql += " and rcard = '" + sn + "'";
            }

            if (rCardSeq >= 0)
            {
                sql += " and tblonwip.rcardseq = " + rCardSeq;
            }

            if (actions != null)
            {
                if (actions.Length > 0)
                {
                    sql += " and tblonwip.action in ('" + string.Join("','", actions) + "') ";
                }
            }


            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        #endregion

        #region Lot
        public object[] QueryLotItemInfo(string moCode, string rCard, int rCardSeq, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select mcard,mitemcode as itemcode,vendorcode,vendoritemcode,lotno,DATECODE,version,pcba,bios from tblonwipitem where rcard='{1}' and rcardseq={2} and mocode='{0}' and MCARDTYPE= 1 "
                , moCode, rCard, rCardSeq);

            return this.DataProvider.CustomQuery(typeof(LotItemInfo), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryLotItemInfoCount(string moCode, string rCard)
        {
            string sql = string.Format(
                "select count(tblonwipitem.mcard) from tblonwipitem where tblonwipitem.rcard='{1}' and tblonwipitem.mocode='{0}' and tblonwipitem.MCARDTYPE= 1 "
                , moCode, rCard);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region OQC LRR & SDR

        public object[] QueryOQCLRR(string modelCode, string itemCode, string dateGroup, int oqcBeginDate, int oqcBeginTime, int oqcEndDate, int oqcEndTime, string lotType)
        {
            #region 查询条件

            //机种
            string modelCodition = string.Empty;
            if (modelCode != "" && modelCode != null)
            {
                modelCodition = string.Format(
                    @" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            //产品
            string itemCodition = string.Empty;
            if (itemCode != "" && itemCode != null)
            {
                itemCodition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            //抽检日期
            string dateCondition = FormatHelper.GetDateRangeSql("mdate", "mtime", oqcBeginDate, oqcBeginTime, oqcEndDate, oqcEndTime);

            //送检批类型
            string lotTypeCondition = string.Empty;
            if (lotType == "NORMAL")	// 一次送检
                lotTypeCondition = string.Format(" and oqclottype='{0}' ", OQCLotType.OQCLotType_Normal);
            else if (lotType == "RELOT")	//重检
                lotTypeCondition = " and oldlotno is not null ";
            else if (lotType == "REWORKLOT")	//返工送检
                lotTypeCondition = string.Format(" and oqclottype='{0}' ", OQCLotType.OQCLotType_ReDO);
            else if (lotType == "TRYLOT")	//非量产送检
                lotTypeCondition = string.Format(" and productiontype='{0}' ", ProductionType.ProductionType_Try);
            #endregion

            //查询条件
            string sqlCondition = modelCodition + itemCodition + dateCondition;

            //查询字段
            string sqlSelect = string.Empty;
            if (dateGroup != "" && dateGroup != null)
            {
                sqlSelect = string.Format(@" modelcode,COUNT (distinct lotno) AS LOTTOTALCOUNT,
												SUM (CASE lotstatus
														WHEN 'oqclotstatus_pass'
															THEN 0
														WHEN 'oqclotstatus_reject'
															THEN 1
													END
													) AS lotngcount, SUM(LotSize) AS LotSize,{0} as dategroup", dateGroup);
            }

            //GroupBy 条件
            string sqlGroupByCondition = string.Empty;
            if (dateGroup != "" && dateGroup != null)
            {
                sqlGroupByCondition = string.Format("GROUP BY modelcode, {0}", dateGroup);
            }

            // OrderBy
            string orderBy = dateGroup + ",modelcode";

            string sql = string.Format(
                @"SELECT  {0}
					FROM (SELECT tbllot.*, 
								TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'),'yyyyww') AS week,
								TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'), 'yyyyMM') AS MONTH,
								d.mocode, d.modelcode, d.itemcode  
							FROM (SELECT tbllot.* FROM tbllot 
								  WHERE tbllot.lotstatus IN ('oqclotstatus_pass', 'oqclotstatus_reject') {4}) tbllot,
								(SELECT DISTINCT lotno, lotseq, mocode, modelcode, itemcode
											FROM tbllot2card 
											WHERE 1=1 {1}) d 
						WHERE tbllot.lotno = d.lotno
							AND tbllot.lotseq = d.lotseq
							{2})
				{3} ORDER BY {5}", sqlSelect, sqlCondition, dateCondition, sqlGroupByCondition, lotTypeCondition, orderBy);



            object[] objs = this.DataProvider.CustomQuery(typeof(OQCLRR), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                sqlSelect = string.Format(@" modelcode,COUNT(rcard) as lotsamplecount,
												SUM (CASE rcardstatus
														WHEN 'GOOD'
															THEN 0
														WHEN 'NG'
															THEN 1
													END
													) AS lotsamplengcount, {0} as dategroup", dateGroup);
                sql = string.Format(
                    @"SELECT  {0}
					FROM (SELECT tbllot.*, 
								TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'),'yyyyww') AS week,
								TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'), 'yyyyMM') AS MONTH,
								d.mocode, d.modelcode, d.itemcode,d.rcard,d.rcardstatus  
							FROM (SELECT tbllot.* FROM tbllot 
								  WHERE tbllot.lotstatus IN ('oqclotstatus_pass', 'oqclotstatus_reject') {4}) tbllot,
								(SELECT DISTINCT lotno, lotseq, mocode, modelcode, itemcode,rcard,status as rcardstatus 
											FROM tbllot2cardcheck 
											WHERE 1=1 {1}) d 
						WHERE tbllot.lotno = d.lotno
							AND tbllot.lotseq = d.lotseq
							{2})
				{3} ORDER BY {5}", sqlSelect, sqlCondition, dateCondition, sqlGroupByCondition, lotTypeCondition, orderBy);
                object[] objsTmp = this.DataProvider.CustomQuery(typeof(OQCLRR), new SQLCondition(sql));
                if (objsTmp != null && objsTmp.Length > 0)
                {
                    for (int i = 0; i < objsTmp.Length; i++)
                    {
                        OQCLRR lrrChk = (OQCLRR)objsTmp[i];
                        for (int n = 0; n < objs.Length; n++)
                        {
                            OQCLRR lrr = (OQCLRR)objs[n];
                            if (lrrChk.ModelCode == lrr.ModelCode && lrrChk.DateGroup == lrr.DateGroup)
                            {
                                lrr.LotSampleCount = lrrChk.LotSampleCount;
                                lrr.LotSampleNGCount = lrrChk.LotSampleNGCount;
                            }
                        }
                    }
                }
            }
            return objs;
        }

        public object[] QueryOQCSDR(string modelCode, string itemCode, string moCode, string dateGroup, string ssCode, int oqcBeginDate, int oqcBeginTime, int oqcEndDate, int oqcEndTime)
        {
            //			#region 查询条件
            //
            //			//机种
            //			string modelCodition = string.Empty;
            //			if( modelCode != "" && modelCode != null )
            //			{
            //				modelCodition = string.Format(
            //					@" and modelcode in ({0})",FormatHelper.ProcessQueryValues( modelCode ) );
            //			}
            //
            //			//产品
            //			string itemCodition = string.Empty;
            //			if( itemCode != "" && itemCode != null )
            //			{
            //				itemCodition = string.Format(
            //					@" and itemcode in ({0})",FormatHelper.ProcessQueryValues( itemCode ) );
            //			}
            //
            //			//工单
            //			string moCodition = string.Empty;
            //			if( moCode != "" && moCode != null )
            //			{
            //				moCodition = string.Format(
            //					@" and mocode in ({0})",FormatHelper.ProcessQueryValues( moCode ) );
            //			}
            //			
            //			//抽检日期
            //			string dateCondition = FormatHelper.GetDateRangeSql("mdate","mtime",oqcBeginDate,oqcBeginTime,oqcEndDate,oqcEndTime);
            //
            //			#endregion
            //
            //			//查询条件
            //			string sqlCondition = modelCodition + itemCodition + moCodition + dateCondition;
            //
            //			//查询字段
            //			string sqlSelect = string.Empty;
            //			if( dateGroup != "" && dateGroup != null )
            //			{
            //				sqlSelect = string.Format(" modelcode,itemcode,mocode,SUM (lotsize) AS samplecount,SUM (samplengcount) AS samplengcount,{0} as dategroup",dateGroup);
            //			}
            //
            //			//GroupBy 条件
            //			string sqlGroupByCondition = string.Empty;
            //			if( dateGroup != "" && dateGroup != null )
            //			{
            //				sqlGroupByCondition = string.Format("GROUP BY modelcode,itemcode,mocode,{0}",dateGroup);
            //			}
            //
            //			string sql = string.Format(
            //				@"SELECT   {0}
            //							FROM (SELECT tbllot.*, TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'),
            //															'ww') AS week,
            //										TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'), 'MM') AS MONTH,
            //										d.modelcode, d.itemcode,d.mocode,d.samplengcount
            //									FROM tbllot,
            //										(SELECT modelcode, itemcode,mocode, lotno, lotseq,  SUM (CASE STATUS
            //										WHEN 'GOOD'
            //											THEN 0
            //										WHEN 'NG'
            //											THEN 1
            //									END
            //									) AS samplengcount
            //													FROM tbllot2cardcheck
            //													WHERE 1=1 {1}
            //													group by modelcode, itemcode,mocode, lotno, lotseq) d
            //								WHERE tbllot.lotno = d.lotno
            //									AND tbllot.lotseq = d.lotseq
            //									AND tbllot.lotstatus IN ('oqclotstatus_pass', 'oqclotstatus_reject')
            //									{2})
            //										{3}
            //										",sqlSelect,sqlCondition,dateCondition,sqlGroupByCondition);


            #region 查询条件

            //机种
            string modelCodition = string.Empty;
            if (modelCode != "" && modelCode != null)
            {
                modelCodition = string.Format(
                    @" and modelcode in ({0})", FormatHelper.ProcessQueryValues(modelCode));
            }

            //产品
            string itemCodition = string.Empty;
            if (itemCode != "" && itemCode != null)
            {
                itemCodition = string.Format(
                    @" and itemcode in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }

            //工单
            string moCodition = string.Empty;
            if (moCode != "" && moCode != null)
            {
                moCodition = string.Format(
                    @" and mocode in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }

            string sscodeCondition = string.Empty;
            if (ssCode != null && ssCode != "")
            {
                sscodeCondition = string.Format("and sscode in ({0}) ", FormatHelper.ProcessQueryValues(ssCode));
            }

            //抽检日期
            string dateCondition = FormatHelper.GetDateRangeSql("mdate", "mtime", oqcBeginDate, oqcBeginTime, oqcEndDate, oqcEndTime);

            #endregion

            //查询条件
            string sqlCondition = modelCodition + itemCodition + moCodition + dateCondition + sscodeCondition;

            //查询字段
            string sqlSelect = string.Empty;
            if (dateGroup != "" && dateGroup != null)
            {
                sqlSelect = string.Format(" modelcode, itemcode, mocode, SUM (samplecount) AS samplecount,SUM (samplengcount) AS samplengcount, {0} AS dategroup", dateGroup);
            }

            //GroupBy 条件
            string sqlGroupByCondition = string.Empty;
            if (dateGroup != "" && dateGroup != null)
            {
                sqlGroupByCondition = string.Format("GROUP BY modelcode,itemcode,mocode,{0}", dateGroup);
            }
            //针对不同机种，产品，工单混单的Lot统计
            string sql = string.Format(@"
						SELECT   {0}
								FROM (SELECT a.lotno, a.lotsize, a.ssize, a.modelcode, a.itemcode,
											a.mocode, a.samplecount, b.samplengcount, a.mdate, a.week,
											a.MONTH
										FROM (SELECT tbllot.*,
													TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'), 'yyyyww') AS week,
													TO_CHAR (TO_DATE (mdate, 'yyyyMMdd'), 'yyyyMM') AS MONTH,
													c.modelcode, c.mocode, c.samplecount
												FROM tbllot,
													(SELECT   lotno, lotseq, modelcode, itemcode, mocode,
															COUNT (rcard) AS samplecount
														FROM tbllot2card
														WHERE 1 = 1  {1}
													GROUP BY lotno, lotseq, modelcode, itemcode, mocode) c
											WHERE tbllot.lotno = c.lotno
												AND tbllot.lotseq = c.lotseq
												AND tbllot.lotstatus IN
															('oqclotstatus_pass', 'oqclotstatus_reject')
												{2}
												) a LEFT JOIN (SELECT   modelcode,
																							itemcode,
																							mocode,
																							lotno,
																							lotseq,
																							SUM
																								(CASE status
																									WHEN 'GOOD'
																									THEN 0
																									WHEN 'NG'
																									THEN 1
																								END
																								)
																								AS samplengcount
																						FROM tbllot2cardcheck
																					WHERE 1=1 {1}
																					GROUP BY modelcode,
																							itemcode,
																							mocode,
																							lotno,
																							lotseq) b ON (    a.lotno =
																												b.lotno
																										AND a.lotseq =
																												b.lotseq
																										AND a.modelcode =
																												b.modelcode
																										AND a.itemcode =
																												b.itemcode
																										AND a.mocode =
																												b.mocode
																										)
											)
							{3}
							ORDER BY modelcode, itemcode, mocode
							", sqlSelect, sqlCondition, dateCondition, sqlGroupByCondition);

            return this.DataProvider.CustomQuery(typeof(OQCSDR), new SQLCondition(sql));
        }

        #endregion

        #region RunningItemInfo
        public object[] QueryRunningItemInfo(string moCode, string rCard, int rCardSeq, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select tblonwipitem.mcard,tblonwipitem.mitemcode as itemcode,tblonwipitem.vendorcode,tblonwipitem.vendoritemcode,tblonwipitem.lotno,tblonwipitem.datecode,tblonwipitem.version,tblonwipitem.pcba,tblonwipitem.bios,'' as TRACINGABLE from tblonwipitem where tblonwipitem.rcard='{1}' and tblonwipitem.rcardseq={2} and tblonwipitem.mocode='{0}'  and tblonwipitem.MCARDTYPE= 0  "
                , moCode, rCard, rCardSeq);

            object[] objs = this.DataProvider.CustomQuery(typeof(RunningItemInfo), new PagerCondition(sql, inclusive, exclusive));
            if (objs == null)
            {
                return null;
            }

            for (int i = 0; i < objs.Length; i++)
            {
                ((RunningItemInfo)objs[i]).CanTracing = CheckCanTracing(((RunningItemInfo)objs[i]).MCARD.Replace("'", "''"));
            }

            return objs;
        }

        public bool CheckCanTracing(string rcard)
        {
            string sql = string.Format(
                "select count(rcard) from tblsimulationreport where rcard='{0}' "
                , rcard);

            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int QueryRunningItemInfoCount(string moCode, string rCard)
        {
            string sql = string.Format(
                "select count(tblonwipitem.mcard) from tblonwipitem where tblonwipitem.rcard='{1}' and tblonwipitem.mocode='{0}'  and tblonwipitem.MCARDTYPE= 0  "
                , moCode, rCard);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region SNInfo

        public object[] QuerySNInfo(string moCode, string rCard, int rCardSeq, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "SELECT scard,tcard,rcard FROM tblonwipcardtrans where mocode='{0}' and ( ( tcard='{1}' and tcardseq={2} ) or (tcard IN (SELECT tcard FROM tblonwipcardtrans WHERE rcard = '{1}' AND rcardseq = {2} )))"
                , moCode, rCard, rCardSeq);

            return this.DataProvider.CustomQuery(typeof(SNInfo), new PagerCondition(sql, "tcard", inclusive, exclusive));

        }

        public int QuerySNInfoCount(string moCode, string rCard)
        {
            string sql = string.Format(
                "SELECT count(scard) FROM tblonwipcardtrans where rcard='{1}' and mocode='{0}' "
                , moCode, rCard);

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }
        //add by klaus
        public object[] QuerySNInfoNew(string moCode, string rCard, int rCardSeq, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "SELECT scard,tcard,rcard FROM tblonwipcardtrans where mocode='{0}' and scard = '{1}' AND rcardseq = {2} "
                , moCode, rCard, rCardSeq);

            return this.DataProvider.CustomQuery(typeof(SNInfo), new PagerCondition(sql, "tcard", inclusive, exclusive));

        }
        public int QuerySNInfoCountNew(string moCode, string rCard)
        {
            string sql = string.Format(
                "SELECT count(scard) FROM tblonwipcardtrans where scard='{1}' and mocode='{0}' "
                , moCode, rCard);

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }
        //end
        #endregion

        #region PackingInfo

        public object[] QueryPackingInfo(string moCode, string rCard, int rCardSeq, int inclusive, int exclusive)
        {
            // Changed by Icyer 2006/07/07
            /*
            string sql = string.Format(
                "SELECT cartoncode,palletcode FROM tblsimulation where mocode='{0}' and rcard='{1}' and rcardseq={2}"
                ,moCode,rCard,rCardSeq);

            object[] objs =  this.DataProvider.CustomQuery(typeof(Simulation),new PagerCondition(sql,"rcard",inclusive,exclusive)) ;

            if( objs == null)
                return new object[]{};

            return objs ;
            */
            string sql = string.Format(
                "SELECT {0} FROM tblcartoninfo where cartonno=(select cartoncode from tblsimulationreport where mocode='{1}' and rcard='{2}' ) "
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.Package.CARTONINFO)), moCode, rCard);

            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Package.CARTONINFO), new PagerCondition(sql, "cartonno", inclusive, exclusive));

            if (objs == null)
                return new object[] { };

            return objs;
        }

        public int QueryPackingInfoCount(string moCode, string rCard, int rCardSeq)
        {
            // Changed by Icyer 2006/07/07
            /*
            string sql = string.Format(
                "SELECT count(rcard) FROM tblsimulation where mocode='{0}' and rcard='{1}' and rcardseq={2} "
                ,moCode,rCard,rCardSeq);

            return this.DataProvider.GetCount(new SQLCondition(sql)) ;
            */
            string sql = string.Format(
                "SELECT count(*) FROM tblcartoninfo where cartonno=(select cartoncode from tblsimulationreport where mocode='{0}' and rcard='{1}' ) "
                , moCode, rCard);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region PackingInfoRCard

        public object[] QueryPackingInfoRCard(string cartonNo, int inclusive, int exclusive)
        {
            string selectColunms = " tblsimulationReport.RCard,tblsimulationReport.rcardseq,tblsimulationReport.ItemCode || ' - ' || tblitem.itemdesc AS ItemCode,tblsimulationReport.MOCode";
            string joinCondition = " LEFT OUTER JOIN tblitem ON tblsimulationReport.itemcode=tblitem.itemcode ";
            string sql = string.Format(
                "select {0} from tblsimulationReport {1} where cartoncode='{2}' "
                , selectColunms, joinCondition, cartonNo);

            PagerCondition condition = new PagerCondition(sql, "rcard,rcardseq", inclusive, exclusive);
            object[] objs = this.DataProvider.CustomQuery(typeof(SimulationReport), condition);

            if (objs == null)
                return new object[] { };

            return objs;
        }

        public int QueryPackingInfoRCardCount(string cartonNo)
        {
            string sql = string.Format(
                "select count(rcard) from tblsimulationReport where cartoncode='{0}' "
                , cartonNo);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region PackingInfoRCard修改SQL

        public object[] QueryPackingInfoRCard1(string cartonNo, int inclusive, int exclusive)
        {
            string selectColunms = " A.RCARD, B.ITEMCODE, A.MOCODE";
            string joinCondition = " LEFT JOIN TBLMO B ON A.MOCODE = B.MOCODE ";
            string sql = string.Format(
                "select {0} from TBLCARTON2RCARD A {1} where cartonno='{2}' "
                , selectColunms, joinCondition, cartonNo);

            PagerCondition condition = new PagerCondition(sql, "rcard", inclusive, exclusive);
            object[] objs = this.DataProvider.CustomQuery(typeof(SimulationReport), condition);

            if (objs == null)
                return new object[] { };

            return objs;
        }

        public int QueryPackingInfoRCardCount1(string cartonNo)
        {
            string sql = string.Format(
                "select count(rcard) from TBLCARTON2RCARD where cartonno='{0}' "
                , cartonNo);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region PackingInfoDetail

        public object[] QueryPackingInfoDetail(string cartonNo, int packingBeginDate, int packingEndDate, string rcardStart, string rcardEnd, string moCode, string itemCode, string memo, int inclusive, int exclusive)
        {
            string selectColunms = FormatHelper.GetAllFieldWithDesc(typeof(BenQGuru.eMES.Domain.Package.CARTONINFO), "TBLCARTONINFO", new string[] { "muser" }, new string[] { "tbluser.username" });

            string sql = string.Format(
                "select {0} from tblcartoninfo left outer join tbluser on tblcartoninfo.muser=tbluser.usercode  where cartonno like '{1}%' ", selectColunms, cartonNo.ToUpper());
            if (packingBeginDate > 0)
                sql += " AND tblcartoninfo.MDate>=" + packingBeginDate.ToString();
            if (packingEndDate > 0)
                sql += " AND tblcartoninfo.MDate<=" + packingEndDate.ToString();
            if (memo != string.Empty)
                sql += " AND tblcartoninfo.EAttribute1 LIKE '" + memo + "%' ";
            if (rcardStart != string.Empty || rcardEnd != string.Empty || moCode != string.Empty || itemCode != string.Empty)
            {
                string sqlRCard = "SELECT CartonCode FROM tblSimulationreport WHERE 1=1 ";
                if (rcardStart != string.Empty)
                    sqlRCard += " AND RCard>='" + rcardStart + "' ";
                if (rcardEnd != string.Empty)
                    sqlRCard += " AND RCard<='" + rcardEnd + "' ";

                if (moCode.Trim().Length > 0)
                {
                    if (moCode.IndexOf(",") >= 0)
                    {
                        string[] lists = moCode.Split(',');
                        for (int i = 0; i < lists.Length; i++)
                        {
                            lists[i] = "'" + lists[i] + "'";
                        }
                        moCode = string.Join(",", lists);
                        sqlRCard += string.Format(" and mocode in ({0})", moCode.ToUpper());
                    }
                    else
                    {
                        sqlRCard += string.Format(" and mocode like '{0}%'", moCode.ToUpper());
                    }
                }
                if (itemCode.Trim().Length > 0)
                {
                    if (itemCode.IndexOf(",") >= 0)
                    {
                        string[] lists = itemCode.Split(',');
                        for (int i = 0; i < lists.Length; i++)
                        {
                            lists[i] = "'" + lists[i] + "'";
                        }
                        itemCode = string.Join(",", lists);
                        sqlRCard += string.Format(" and itemcode in ({0})", itemCode.ToUpper());
                    }
                    else
                    {
                        sqlRCard += string.Format(" and itemcode like '{0}%'", itemCode.ToUpper());
                    }
                }

                sql += " AND CartonNo IN (" + sqlRCard + " ) ";
            }

            PagerCondition condition = new PagerCondition(sql, "cartonno", inclusive, exclusive);
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Package.CARTONINFO), condition);

            if (objs == null)
                return new object[] { };

            return objs;
        }

        public int QueryPackingInfoDetailCount(string cartonNo, int packingBeginDate, int packingEndDate, string rcardStart, string rcardEnd, string moCode, string itemCode, string memo)
        {
            string sql = string.Format(
                "select count(cartonno) from tblcartoninfo where cartonno like '{0}%' "
                , cartonNo.ToUpper());
            if (packingBeginDate > 0)
                sql += " AND MDate>=" + packingBeginDate.ToString();
            if (packingEndDate > 0)
                sql += " AND MDate<=" + packingEndDate.ToString();
            if (memo != string.Empty)
                sql += " AND EAttribute1 LIKE '" + memo + "%' ";
            if (rcardStart != string.Empty || rcardEnd != string.Empty || moCode != string.Empty || itemCode != string.Empty)
            {
                string sqlRCard = "SELECT CartonCode FROM tblSimulationReport WHERE 1=1 ";
                if (rcardStart != string.Empty)
                    sqlRCard += " AND RCard>='" + rcardStart + "' ";
                if (rcardEnd != string.Empty)
                    sqlRCard += " AND RCard<='" + rcardEnd + "' ";
                if (moCode.Trim().Length > 0)
                {
                    if (moCode.IndexOf(",") >= 0)
                    {
                        string[] lists = moCode.Split(',');
                        for (int i = 0; i < lists.Length; i++)
                        {
                            lists[i] = "'" + lists[i] + "'";
                        }
                        moCode = string.Join(",", lists);
                        sqlRCard += string.Format(" and mocode in ({0})", moCode.ToUpper());
                    }
                    else
                    {
                        sqlRCard += string.Format(" and mocode like '{0}%'", moCode.ToUpper());
                    }
                }
                if (itemCode.Trim().Length > 0)
                {
                    if (itemCode.IndexOf(",") >= 0)
                    {
                        string[] lists = itemCode.Split(',');
                        for (int i = 0; i < lists.Length; i++)
                        {
                            lists[i] = "'" + lists[i] + "'";
                        }
                        itemCode = string.Join(",", lists);
                        sqlRCard += string.Format(" and itemcode in ({0})", itemCode.ToUpper());
                    }
                    else
                    {
                        sqlRCard += string.Format(" and itemcode like '{0}%'", itemCode.ToUpper());
                    }
                }

                sql += " AND CartonNo IN (" + sqlRCard + " ) ";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region OpResult-TS
        //added by jessie lee for CS0041,2005/10/12,P4.10
        public object[] QueryTSOPResult(string rCard, int rCardSeq, int inclusive, int exclusive)
        {
            String fields = DomainObjectUtility.GetDomainObjectFieldsString(typeof(TSOPResult));

            fields = fields.Replace("optype", "'TS' as OPTYPE");
            fields = fields.Replace("segcode", "'' as SEGCODE");
            fields = fields.Replace("sscode", "'' as SSCODE");
            fields = fields.Replace("tsstatus", string.Format("decode(tsstatus,'{0}','{1}',tsstatus) as tsstatus", TSStatus.TSStatus_Reflow, TSStatus.TSStatus_Complete));
            string sql = string.Format("select {0} from tblts where rcard = '{1}' and rcardseq = {2}",
                fields, rCard, rCardSeq);

            string selectColunms = "A.TSSTATUS,A.SSCODE,A.MDATE,A.MUSER || ' - ' || tbluser.username as MUSER,A.FRMROUTECODE,A.SEGCODE,A.RCARDSEQ,A.RCARD,A.COPCODE || ' - ' || tblop.opdesc as COPCODE,A.CRESCODE || ' - ' || tblres.resdesc as CRESCODE,A.OPTYPE,A.MTIME";
            string linkSQL1 = FormatHelper.GetLinkTableSQL("A", "COPCODE", "tblop", "opcode");
            string linkSQL2 = FormatHelper.GetLinkTableSQL("A", "CRESCODE", "tblres", "rescode");
            string linkSQL3 = FormatHelper.GetLinkTableSQL("A", "MUSER", "tbluser", "usercode");

            sql = "select " + selectColunms + " from (" + sql + ") A " + linkSQL1 + " " + linkSQL2 + " " + linkSQL3 + " ";

            object[] objs = this.DataProvider.CustomQuery(typeof(TSOPResult), new PagerCondition(sql, inclusive, exclusive));
            return objs;
        }


        public int QueryTSOPResultCount(string rCard, int rCardSeq)
        {
            string sql = string.Format("select count(rcard) from tblts where rcard='{0}' and rcardseq = {1} ", rCard, rCardSeq);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryTSInfo(string rCard, int rCardSeq, int inclusive, int exclusive)
        {
            string sql = string.Format(
                @"select ecgcode,ecode,ecscode,eloc,epart,SOLCODE,DUTYCODE,SOLMEMO,muser,mdate,mtime from 
					( select distinct a.ecgcode,
					a.ecode,
					a.ecscode,
					c.eloc,
					b.epart,
					a.SOLCODE,
					a.DUTYCODE,
					a.SOLMEMO,
					a.muser,
					a.mdate,
					a.mtime
				from tbltserrorcause a 
				left join tbltserrorcause2epart b on a.tsid = b.tsid
				left join tbltserrorcause2loc c on a.tsid = c.tsid
				where a.rcard = '{0}'
				and a.rcardseq = {1} )", rCard, rCardSeq);
            object[] objs = this.DataProvider.CustomQuery(typeof(TSInfo), new PagerCondition(sql, inclusive, exclusive));
            return objs;
        }

        public int QueryTSInfoCount(string rCard, int rCardSeq)
        {
            string sql = string.Format(
                @"select count(*) from 
				(select distinct a.ecgcode,
					a.ecode,
					a.ecscode,
					c.eloc,
					b.epart,
					a.SOLCODE,
					a.DUTYCODE,
					a.SOLMEMO,
					a.muser,
					a.mdate,
					a.mtime
				from tbltserrorcause a 
				left join tbltserrorcause2epart b on a.tsid = b.tsid
				left join tbltserrorcause2loc c on a.tsid = c.tsid
				where a.rcard = '{0}'
				and a.rcardseq = {1} )", rCard, rCardSeq);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryHLInfo(string rCard, int rCardSeq, int inclusive, int exclusive)
        {
            string sql = string.Format(
                @"select {0} from TBLTSITEM
				where rcard = '{1}'
				and rcardseq = {2} ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(HLInfo)),
                rCard,
                rCardSeq);
            object[] objs = this.DataProvider.CustomQuery(typeof(HLInfo), new PagerCondition(sql, inclusive, exclusive));
            return objs;
        }

        public int QueryHLInfoCount(string rCard, int rCardSeq)
        {
            string sql = string.Format(
                @"select count(*) from TBLTSITEM
				where rcard = '{0}'
				and rcardseq = {1} ", rCard, rCardSeq);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region ReportCenter - OQC LRR
        /// <summary>
        /// 查询OQC不良现象
        /// </summary>
        public object[] QueryOQCErrorCode(string segmentCode, int dateFrom, int dateTo)
        {
            string strWhere = " 1=1 ";
            if (segmentCode != string.Empty)
                strWhere += " and segcode='" + segmentCode + "' ";
            if (dateFrom > 0)
                strWhere += " and mdate>=" + dateFrom.ToString();
            if (dateTo > 0)
                strWhere += " and mdate<=" + dateTo.ToString();
            string strSql = "select ecode,ecdesc,itemcode,itemcardqty,sum(itemcardqty) over (partition by ecode,ecdesc) errorcodecardqty from ( " +
                            "select ec.ecode,tblec.ecdesc,lot.itemcode,count(*) itemcardqty from tbloqclotcard2errorcode ec,tblec," +
                            "(select distinct rcard,itemcode from tbllot2cardcheck where " + strWhere + ") lot " +
                            "where ec.rcard=lot.rcard and ec.ecode=tblec.ecode " +
                            "group by ec.ecode,tblec.ecdesc,lot.itemcode " +
                            ") order by errorcodecardqty desc,ecode,itemcode ";
            object[] objsTmp = this.DataProvider.CustomQuery(typeof(OQCErrorCode), new SQLCondition(strSql));
            return objsTmp;
        }
        public int QueryOQCErrorCodeCount(string segmentCode, int dateFrom, int dateTo)
        {
            string strWhere = " 1=1 ";
            if (segmentCode != string.Empty)
                strWhere += " and segcode='" + segmentCode + "' ";
            if (dateFrom > 0)
                strWhere += " and mdate>=" + dateFrom.ToString();
            if (dateTo > 0)
                strWhere += " and mdate<=" + dateTo.ToString();
            string strSql = "select count(*) from (" +
                "select ec.ecode,lot.itemcode itemcardqty from tbloqclotcard2errorcode ec," +
                "(select distinct rcard,itemcode from tbllot2cardcheck where " + strWhere + ") lot " +
                "where ec.rcard=lot.rcard " +
                "group by ec.ecode,lot.itemcode " +
                ")";
            int iCount = this.DataProvider.GetCount(new SQLCondition(strSql));
            return iCount;
        }
        #endregion

    }
}
