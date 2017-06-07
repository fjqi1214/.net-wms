using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Material
{
    public class PauseFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public PauseFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public PauseFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
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

        #region Pause
        public void AddPause(Pause pause)
        {
            this.DataProvider.Insert(pause);
        }

        public void DeletePause(Pause pause)
        {
            this.DataProvider.Delete(pause);
        }

        public void UpdatePause(Pause pause)
        {
            this.DataProvider.Update(pause);
        }

        public object GetPause(string pauseCode)
        {
            return this.DataProvider.CustomSearch(typeof(Pause), new object[] { pauseCode });
        }

        public void PauseRcard(Pause pause, IList<Pause2Rcard> pause2RcardList)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                if (this.GetPause(pause.PauseCode) == null)
                {
                    this.AddPause(pause);
                }

                foreach (Pause2Rcard obj in pause2RcardList)
                {
                    this.AddPause2Rcard(obj);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }


        }

        public object[] GetPauseInfoFromHead(string itemModel, string bom, string itemDesc, string inInvDateFrom, string inInvDateTo, string rcardFrom, string rcardTo, bool isFinished,string bigSSCode)
        {
            string sql = string.Empty;
            sql += "SELECT itemcode,mdesc,mobom,COUNT(rcard) qty";
            sql += "  FROM (" + this.GetPauseInfoSql(itemModel, bom, itemDesc, inInvDateFrom, inInvDateTo, rcardFrom, rcardTo, isFinished, bigSSCode) + ")";
            sql += " GROUP BY itemcode,mdesc,mobom";

            return this.DataProvider.CustomQuery(typeof(PauseSetting), new SQLCondition(sql));
        }

        public object[] GetPauseInfoFromDetail(string itemModel, string bom, string itemDesc, string inInvDateFrom, string inInvDateTo, string rcardFrom, string rcardTo, bool isFinished, string bigSSCode)
        {
            string sql = string.Empty;
            sql += "SELECT ITEMCODE, PALLETCODE, MOBOM, COUNT(nvl(PALLETCODE,' ')) QTY";
            sql += "  FROM (" + this.GetPauseInfoSql(itemModel, bom, itemDesc, inInvDateFrom, inInvDateTo, rcardFrom, rcardTo, isFinished, bigSSCode) + ")";
            sql += " GROUP BY itemcode,PALLETCODE,mobom";
            return this.DataProvider.CustomQuery(typeof(PauseSetting), new SQLCondition(sql));
        }

        public object[] GetPauseInfoFromPalletDetail(string itemModel, string bom, string itemDesc, string inInvDateFrom, string inInvDateTo, string rcardFrom, string rcardTo, bool isFinished, string bigSSCode)
        {
            string sql = string.Empty;
            sql = this.GetPauseInfoSql(itemModel, bom, itemDesc, inInvDateFrom, inInvDateTo, rcardFrom, rcardTo, isFinished, bigSSCode);
            return this.DataProvider.CustomQuery(typeof(PauseSetting), new SQLCondition(sql));
        }


        public string GetPauseInfoSql(string itemModel, string bom, string itemDesc, string inInvDateFrom, string inInvDateTo, string rcardFrom, string rcardTo, bool isFinished, string bigSSCode)
        {
            string sql = string.Empty;
            sql += "SELECT * ";
            sql += "  FROM ";
            sql += "  (";
            sql += "    SELECT tblstack2rcard.itemcode,tblstack2rcard.serialno rcard, tblmaterial.mdesc,tblmaterial.mmodelcode,tblpallet2rcard.palletcode,tblstack2rcard.indate,";
            sql += "           GETMOCODEBYRCARD(tblstack2rcard.serialno) mocode,";
            sql += "           GETFINISHEDDATEBYRCARD(tblstack2rcard.serialno) finisheddate,";
            sql += "           NVL(GETBOMVERSIONBYRCARD(tblstack2rcard.serialno),' ') mobom";
            sql += "      FROM tblstack2rcard,tblmaterial,tblpallet2rcard";
            sql += "     WHERE tblstack2rcard.itemcode=tblmaterial.mcode ";
            sql += "       AND tblstack2rcard.serialno=tblpallet2rcard.rcard(+) ";
            sql += "       AND tblstack2rcard.businessreason='" + BussinessReason.type_produce + "' ";
            sql += "     UNION";
            sql += "    SELECT tblstack2rcard.itemcode,tblstack2rcard.serialno rcard,tblmaterial.mdesc,tblmaterial.mmodelcode,tblpallet2rcard.palletcode ,tblstack2rcard.indate,";
            sql += "           ' ' mocode,null finisheddate,' ' mobom";
            sql += "      FROM tblstack2rcard ,tblmaterial,tblpallet2rcard ";
            sql += "     WHERE tblstack2rcard.itemcode=tblmaterial.mcode ";
            sql += "       AND tblstack2rcard.serialno=tblpallet2rcard.rcard(+) ";
            sql += "       AND tblstack2rcard.businessreason='" + BussinessReason.type_noneproduce + "' ";
            sql += "  ) ";
            sql += " WHERE 1=1";

            if (itemModel.Length > 0)
            {
                sql += "   AND mmodelcode LIKE '%" + itemModel + "%' ";
            }

            if (bom.Length > 0)
            {
                sql += "   AND mobom = '" + bom + "'";
            }

            if (itemDesc.Length > 0)
            {
                sql += "   AND mdesc LIKE '%" + itemDesc + "%' ";
            }

            if (inInvDateFrom.Length > 0)
            {
                sql += "   AND indate >= " + inInvDateFrom;
            }

            if (inInvDateTo.Length > 0)
            {
                sql += "   AND indate <= " + inInvDateTo;
            }

            if (rcardFrom.Length > 0)
            {
                sql += "   AND rcard >= '" + rcardFrom + "'";
            }

            if (rcardTo.Length > 0)
            {
                sql += "   AND rcard <= '" + rcardTo + "'";
            }

            if (isFinished)
            {
                sql += "   AND finisheddate IS NOT NULL";
            }

            sql += "   AND NOT EXISTS (SELECT serialno FROM Tblpause2rcard WHERE serialno = rcard AND status='" + PauseStatus.status_pause + "')";

            string newSql = string.Empty;
            
            newSql = " SELECT B.itemcode,B.RCARD,B.MDESC,B.MMODELCODE,B.PALLETCODE,B.INDATE,B.MOCODE,B.FINISHEDDATE,B.MOBOM, BIGSSCODE  FROM (SELECT A.*, GETLOTNOBYRCARD(RCARD) AS LOTNO FROM (" + sql + ") A) B,TBLLOT,TBLSS";
            newSql += " WHERE B.LOTNO = TBLLOT.LOTNO(+)   AND TBLLOT.SSCODE = TBLSS.SSCODE(+)  ";
            
            if (bigSSCode.Trim() != string.Empty)
            {
                newSql += "AND TBLSS.BIGSSCODE = '" + bigSSCode.Trim().ToUpper() + "' ";
            }
            newSql += " ORDER BY TBLSS.BIGSSCODE";

            return newSql;
        }

        #endregion

        #region Pause2Rcard
        public void AddPause2Rcard(Pause2Rcard pause2Rcard)
        {
            this.DataProvider.Insert(pause2Rcard);
        }

        public void DeletePause2Rcard(Pause2Rcard pause2Rcard)
        {
            this.DataProvider.Delete(pause2Rcard);
        }

        public void UpdatePause2Rcard(Pause2Rcard pause2Rcard)
        {
            this.DataProvider.Update(pause2Rcard);
        }

        public bool CheckExistPause2Rcard(string pauseCode, string rcard)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) FROM tblpause2rcard WHERE pausecode = '" + pauseCode + "' AND serialno = '" + rcard + "'";
            if (this.DataProvider.GetCount(new SQLCondition(sql)) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object[] GetNotCancelPause2RcardList(string pauseCode, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql += "SELECT storagecode,stackcode,itemcode,mdesc,pausecode,pauseqty,cancelqty FROM (";
            sql += "SELECT DISTINCT tblstack2rcard.storagecode, tblstack2rcard.stackcode,";
            sql += "       tblpause2rcard.itemcode, tblmaterial.mdesc,";
            sql += "       tblpause.pausecode,";
            sql += "        (SELECT COUNT(*) FROM tblpause2rcard a,tblstack2rcard b WHERE a.serialno = b.serialno(+) AND a.pausecode=tblpause.pausecode";
            sql += "         AND a.itemcode = tblpause2rcard.itemcode AND b.storagecode = tblstack2rcard.storagecode AND NVL(b.stackcode,' ') = NVL(tblstack2rcard.stackcode,' ') ";
            sql += "         ) pauseqty,";
            sql += "         (SELECT COUNT(*) FROM tblpause2rcard a,tblstack2rcard b WHERE a.serialno = b.serialno(+) AND a.pausecode=tblpause.pausecode";
            sql += "             AND a.itemcode = tblpause2rcard.itemcode AND NVL(b.storagecode,' ') = NVL(tblstack2rcard.storagecode,' ') AND NVL(b.stackcode,' ') = NVL(tblstack2rcard.stackcode,' ')  ";
            sql += "             AND a.status='" + PauseStatus.status_cancel + "'";
            sql += "                   ) cancelqty";
            sql += "  FROM tblstack2rcard, tblpause, tblpause2rcard, tblmaterial";
            sql += " WHERE tblpause.pausecode = tblpause2rcard.pausecode";
            sql += "   AND tblpause2rcard.itemcode = tblmaterial.mcode";
            sql += "   AND tblpause2rcard.serialno = tblstack2rcard.serialno(+)";
            sql += "   AND tblpause.pausecode = '" + pauseCode + "')";
            sql += " ORDER BY storagecode,stackcode,itemcode";

            return this.DataProvider.CustomQuery(typeof(CancelPauseQuery), new PagerCondition(sql, inclusive, exclusive));
        }

        public int GetNotCancelPause2RcardListCount(string pauseCode)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) FROM (";
            sql += "SELECT DISTINCT tblstack2rcard.storagecode, tblstack2rcard.stackcode,";
            sql += "       tblpause2rcard.itemcode, tblmaterial.mdesc,";
            sql += "       tblpause.pausecode";
            sql += "  FROM tblstack2rcard, tblpause, tblpause2rcard, tblmaterial";
            sql += " WHERE tblpause.pausecode = tblpause2rcard.pausecode";
            sql += "   AND tblpause2rcard.itemcode = tblmaterial.mcode";
            sql += "   AND tblpause2rcard.serialno = tblstack2rcard.serialno(+)";
            sql += "   AND tblpause.pausecode = '" + pauseCode + "')";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public void CancelPause(string userCode, string cancelSeq, string cancelReason, IList<CancelPauseQuery> cancelPauseList)
        {
            try
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                this.DataProvider.BeginTransaction();

                foreach (CancelPauseQuery obj in cancelPauseList)
                {
                    ////解除停发
                    //
                    this.UpdateCancelPause(cancelSeq, cancelReason, obj.StorageCode, obj.StackCode, obj.PauseCode, obj.ItemCode, dbDateTime, userCode, "");
                }

                ////如果rcard都被解除停发,则把header资料解除停发
                //
                this.UpdateCancelPauseHead(cancelPauseList[0].PauseCode, dbDateTime, userCode);

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();

                throw ex;
            }
        }

        //public void CancelPauseByRcard(string rcard, string userCode, string cancelSeq, string cancelReason, IList<CancelPauseQuery> cancelPauseList)
        //{
        //    try
        //    {
        //        DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

        //        this.DataProvider.BeginTransaction();

        //        foreach (CancelPauseQuery obj in cancelPauseList)
        //        {
        //            ////解除停发
        //            //
        //            this.UpdateCancelPause(cancelSeq, cancelReason, obj.StorageCode, obj.StackCode, obj.PauseCode, obj.ItemCode, dbDateTime, userCode,rcard);
        //        }

        //        ////如果rcard都被解除停发,则把header资料解除停发
        //        //
        //        this.UpdateCancelPauseHead(cancelPauseList[0].PauseCode, dbDateTime, userCode);

        //        this.DataProvider.CommitTransaction();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.DataProvider.RollbackTransaction();

        //        throw ex;
        //    }
        //} 

        public void UpdateCancelPause(string cancelSeq, string cancelReason, string storageCode, string stackCode, string pauseCode, string itemCode, DBDateTime dtNow, string userCode, string rcard)
        {

            ////解除停发
            //
            string sql = string.Empty;
            sql += "UPDATE tblpause2rcard SET status = '" + PauseStatus.status_cancel + "',";
            sql += "                          cuser = '" + userCode + "',";
            sql += "                          cdate = " + dtNow.DBDate + ",";
            sql += "                          ctime = " + dtNow.DBTime + ",";
            sql += "                          cancelseq= '" + cancelSeq + "',";
            sql += "                          cancelreason= '" + cancelReason + "'";
            sql += " WHERE itemcode= '" + itemCode + "'";
            sql += "   AND EXISTS (SELECT serialno FROM tblstack2rcard WHERE stackcode='" + stackCode + "' and storagecode= '" + storageCode + "' AND tblstack2rcard.serialno = tblpause2rcard.serialno) ";
            sql += "   AND status='" + PauseStatus.status_pause + "'";
            sql += "   AND pausecode = '" + pauseCode + "'";

            if (rcard.Length != 0)
            {
                sql += "   AND serialno = '" + rcard + "'";
            }

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateCancelPauseHead(string pauseCode, DBDateTime dtNow, string userCode)
        {
            ////如果rcard都被解除停发,则把header资料解除停发
            //
            string sql = string.Empty;
            sql += "UPDATE tblpause SET status = '" + PauseStatus.status_cancel + "',";
            sql += "                          cuser = '" + userCode + "',";
            sql += "                          cdate = " + dtNow.DBDate + ",";
            sql += "                          ctime = " + dtNow.DBTime;
            sql += " WHERE 1=1";
            sql += "   AND NOT EXISTS (SELECT pausecode FROM tblpause2rcard WHERE pausecode='" + pauseCode + "' and status= '" + PauseStatus.status_pause + "' AND tblpause.pausecode = tblpause2rcard.pausecode) ";
            sql += "   AND pausecode='" + pauseCode + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region PauseQuery

        public string GeneratePauseDetailsSql(string pauseCode, string cancelSeq, bool type)
        {
            string sql = string.Empty;
            string newSql = string.Empty;

            sql = @"
              SELECT DISTINCT tblstack2rcard.storagecode, tblstack2rcard.stackcode,
                   tblpallet2rcard.palletcode,
                   (SELECT rcardcount
                      FROM tblpallet
                     WHERE palletcode = tblpallet2rcard.palletcode) AS rcardcount,
                   (SELECT COUNT (*)
                      FROM tblpause2rcard a,
                           tblstack2rcard b,
                           tblpallet2rcard c
                     WHERE a.serialno = b.serialno(+)
                       AND a.serialno = c.rcard(+)
                       AND a.pausecode = tblpause.pausecode
                       AND a.itemcode = tblpause2rcard.itemcode
                       AND nvl(b.storagecode,' ') = nvl(tblstack2rcard.storagecode,' ')
                       AND nvl(b.stackcode,' ') = nvl(tblstack2rcard.stackcode,' ')
                       AND nvl(c.palletcode,' ') = nvl(tblpallet2rcard.palletcode,' ')) pauseqty,
                   (SELECT COUNT (*)
                      FROM tblpause2rcard a,
                           tblstack2rcard b,
                           tblpallet2rcard c
                     WHERE a.serialno = b.serialno(+)
                       AND a.serialno = c.rcard(+)
                       AND a.pausecode = tblpause.pausecode
                       AND a.itemcode = tblpause2rcard.itemcode
                       AND nvl(b.storagecode,' ') = nvl(tblstack2rcard.storagecode,' ')
                       AND nvl(b.stackcode,' ') = nvl(tblstack2rcard.stackcode,' ')
                       AND nvl(c.palletcode,' ') = nvl(tblpallet2rcard.palletcode,' ')
                       AND a.status = '" + PauseStatus.status_cancel + @"') cancelqty,
                   tblmaterial.mcode, tblmaterial.mdesc
              FROM tblstack2rcard, tblpause, tblpause2rcard, tblmaterial,
                   tblpallet2rcard
             WHERE tblpause.pausecode = tblpause2rcard.pausecode
               AND tblpause2rcard.itemcode = tblmaterial.mcode
               AND tblpause2rcard.serialno = tblstack2rcard.serialno(+)
               AND tblpause2rcard.serialno = tblpallet2rcard.rcard(+)";

            if (pauseCode != null && pauseCode.Length > 0)
            {
                sql = string.Format("{0} AND tblpause2rcard.pausecode = '{1}'", sql, pauseCode);
            }

            if (cancelSeq != null && cancelSeq.Length > 0)
            {
                sql = string.Format("{0} AND tblpause2rcard.cancelseq = '{1}'", sql, cancelSeq);
            }

            if (type)
            {
                newSql = "SELECT storagecode, stackcode, palletcode, RCARDCOUNT, PAUSEQTY, CANCELQTY, mcode, mdesc FROM";
            }
            else
            {
                newSql = "SELECT COUNT(*) FROM";
            }

            sql = string.Format("{0} ({1} ORDER BY tblstack2rcard.storagecode, tblstack2rcard.stackcode, tblpallet2rcard.palletcode, tblmaterial.mcode)", newSql, sql);

            return sql;

        }

        public object[] QueryPauseDetails(string pauseCode, string cancelSeq, int inclusive, int exclusive)
        {
            string sql = GeneratePauseDetailsSql(pauseCode, cancelSeq, true);

            return this.DataProvider.CustomQuery(typeof(PauseQuery), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryPauseDetailsCount(string pauseCode, string cancelSeq)
        {
            string sql = GeneratePauseDetailsSql(pauseCode, cancelSeq, false);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public string GeneratePauseSequenceSql(string storageCode, string stackCode, string palletCode, string itemCode,
            string pauseCode, string cancelSeq, bool type, bool newType)
        {
            string sql = string.Empty;

            if (type)
            {

                sql = "SELECT tblstack2rcard.storagecode, tblstack2rcard.stackcode, tblpallet2rcard.palletcode, "
                + "getcartoncodebyrcard (tblpause2rcard.serialno) AS CARTONCODE, "
                + "tblpause2rcard.serialno, tblpause2rcard.mocode, tblpause2rcard.bom, tblpause.pausereason, "
                + "tblmaterial.mcode, tblmaterial.mdesc, tblmaterial.mmodelcode, tblpause2rcard.cancelreason, tblpause2rcard.cdate, tblpause2rcard.cuser "
                + "FROM tblstack2rcard, tblpause, tblpause2rcard, tblmaterial, tblpallet2rcard "
                + "WHERE tblpause.pausecode = tblpause2rcard.pausecode "
                + "AND tblpause2rcard.itemcode = tblmaterial.mcode "
                + "AND tblpause2rcard.serialno = tblstack2rcard.serialno(+) "
                + "AND tblpause2rcard.serialno = tblpallet2rcard.rcard(+) ";
            }
            else
            {
                sql = "SELECT COUNT(*) "
                + "FROM tblstack2rcard, tblpause, tblpause2rcard, tblmaterial, tblpallet2rcard "
                + "WHERE tblpause.pausecode = tblpause2rcard.pausecode "
                + "AND tblpause2rcard.itemcode = tblmaterial.mcode "
                + "AND tblpause2rcard.serialno = tblstack2rcard.serialno(+) "
                + "AND tblpause2rcard.serialno = tblpallet2rcard.rcard(+) ";
            }

            if (newType)
            {
                if (storageCode != null && storageCode.Length != 0)
                {
                    sql = string.Format("{0} AND tblstack2rcard.storagecode = '{1}'", sql, storageCode);
                }
                else
                {
                    sql = string.Format("{0} AND tblstack2rcard.storagecode IS NULL", sql);
                }
            }

            if (stackCode != null && stackCode.Length > 0)
            {
                sql = string.Format("{0} AND tblstack2rcard.stackcode = '{1}'", sql, stackCode);
            }

            if (palletCode != null && palletCode.Length > 0)
            {
                sql = string.Format("{0} AND tblpallet2rcard.palletcode = '{1}'", sql, palletCode);
            }

            if (itemCode != null && itemCode.Length > 0)
            {
                sql = string.Format("{0} AND tblpause2rcard.itemcode = '{1}'", sql, itemCode);
            }

            if (pauseCode != null && pauseCode.Length > 0)
            {
                sql = string.Format("{0} AND tblpause2rcard.pausecode = '{1}'", sql, pauseCode);
            }

            if (cancelSeq != null && cancelSeq.Length > 0)
            {
                sql = string.Format("{0} AND tblpause2rcard.cancelseq = '{1}'", sql, cancelSeq);
            }

            return sql;
        }

        public object[] QueryPauseSequence(string storageCode, string stackCode, string palletCode, string itemCode,
            string pauseCode, string cancelSeq, bool newType, int inclusive, int exclusive)
        {
            string sql = GeneratePauseSequenceSql(storageCode, stackCode, palletCode, itemCode, pauseCode, cancelSeq, true, newType);

            return this.DataProvider.CustomQuery(typeof(PauseQuery), new PagerCondition(sql, "tblpause.pausecode", inclusive, exclusive));
        }

        public int QueryPauseSequenceCount(string storageCode, string stackCode, string palletCode, string itemCode,
            string pauseCode, string cancelSeq, bool newType)
        {
            string sql = GeneratePauseSequenceSql(storageCode, stackCode, palletCode, itemCode, pauseCode, cancelSeq, false, newType);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public string GenerateHistorySql(string pauseCode, string pauseStatus, string itemCode, string BOMVersion,
            int pauseStartDate, int pauseEndDate, bool type)
        {
            string sql = string.Empty;

            if (type)
            {
                sql = "SELECT DISTINCT tblpause.pausecode, tblpause.pdate, tblpause.puser, "
                + "(SELECT COUNT (*) FROM tblpause2rcard WHERE pausecode = tblpause.pausecode) AS PAUSEQTY, "
                + "(SELECT COUNT (*) FROM tblpause2rcard WHERE pausecode = tblpause.pausecode AND status = '" + PauseStatus.status_cancel + "') AS CANCELQTY, "
                + "tblpause.status, tblpause.cdate, tblpause.cuser FROM tblpause, tblpause2rcard "
                + "WHERE tblpause.pausecode = tblpause2rcard.pausecode ";
            }
            else
            {
                sql = "SELECT COUNT(DISTINCT tblpause.pausecode) "
                + "FROM tblpause, tblpause2rcard "
                + "WHERE tblpause.pausecode = tblpause2rcard.pausecode ";
            }

            if (pauseCode != null && pauseCode.Length > 0)
            {
                sql = string.Format("{0} AND tblpause.pausecode LIKE '%{1}%'", sql, pauseCode);
            }

            if (pauseStatus != null && pauseStatus.Length > 0)
            {
                sql = string.Format("{0} AND tblpause.status LIKE '%{1}%'", sql, pauseStatus);
            }

            if (itemCode != null && itemCode.Length > 0)
            {
                sql = string.Format("{0} AND tblpause2rcard.itemcode LIKE '%{1}%'", sql, itemCode);
            }

            if (BOMVersion != null && BOMVersion.Length > 0)
            {
                sql = string.Format("{0} AND tblpause2rcard.bom = '{1}'", sql, BOMVersion);
            }

            if (pauseStartDate > 0)
            {
                sql = string.Format("{0} AND tblpause.pdate >= '{1}'", sql, pauseStartDate);
            }

            if (pauseEndDate > 0)
            {
                sql = string.Format("{0} AND tblpause.pdate <= '{1}'", sql, pauseEndDate);
            }

            if (type)
            {
                string newSql = "SELECT pausecode, pdate, puser, PAUSEQTY, CANCELQTY, status, cdate, cuser FROM";

                sql = string.Format("{0} ({1} ORDER BY tblpause.pausecode)", newSql, sql);
            }

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pauseCode"></param>
        /// <param name="pauseStatus"></param>
        /// <param name="itemCode"></param>
        /// <param name="BOMVersion"></param>
        /// <param name="pauseStartDate"></param>
        /// <param name="pauseEndDate"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        /// bighai.wang 2009/02/10
        /// 

        public object[] QueryInvPeriodcode(string PeiodGroup)
        {
            string sql2 = string.Empty;
        
            if (PeiodGroup == "" || PeiodGroup == null)
            {
                //sql2 = "select distinct invperiodcode,datefrom,dateto from tblinvperiod order  by invperiodcode ";

                sql2 = "select distinct invperiodcode,datefrom,PEIODGROUP from tblinvperiod order  by PEIODGROUP,datefrom ";

            }
            else
            {

                //sql2 = "select distinct invperiodcode,datefrom,dateto from tblinvperiod where peiodgroup = '" + PeiodGroup + "' order  by invperiodcode";

                sql2 = "select distinct invperiodcode,datefrom,PEIODGROUP from tblinvperiod where peiodgroup = '" + PeiodGroup + "' order  by PEIODGROUP,datefrom ";
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Warehouse.InvPeriod), new SQLCondition(sql2));
            return objs;
 
        }
    



        public object[] QueryHistory(string pauseCode, string pauseStatus, string itemCode, string BOMVersion,
            int pauseStartDate, int pauseEndDate, int inclusive, int exclusive)
        {
            string sql = GenerateHistorySql(pauseCode, pauseStatus, itemCode, BOMVersion, pauseStartDate, pauseEndDate, true);

            return this.DataProvider.CustomQuery(typeof(PauseQuery), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryHistoryCount(string pauseCode, string pauseStatus, string itemCode, string BOMVersion,
            int pauseStartDate, int pauseEndDate)
        {
            string sql = GenerateHistorySql(pauseCode, pauseStatus, itemCode, BOMVersion, pauseStartDate, pauseEndDate, false);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

     

       


        public object[] QueryLast(string pauseCode)
        {
            string sql = string.Format("SELECT pausereason, puser, pdate FROM tblpause WHERE pausecode= '{0}'", pauseCode);

            return this.DataProvider.CustomQuery(typeof(Pause), new SQLCondition(sql));

        }


        #endregion

    }
}
