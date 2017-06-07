using System;
using System.Collections.Generic;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.MailUtility;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.AlertModel
{

    /// <summary>
    /// Alert 的摘要说明。
    /// </summary>
    public class AlertFacade
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public AlertFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
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

        #region 这些Facade在Hisense MES中已经过期作废，不再使用

        #region Alert
        /// <summary>
        /// 
        /// </summary>
        public Alert CreateNewAlert()
        {
            return new Alert();
        }

        public BenQGuru.eMES.Domain.Alert.Alert CreateNewAlert(BenQGuru.eMES.Domain.Alert.AlertBill bill)
        {
            BenQGuru.eMES.Domain.Alert.Alert alert = CreateNewAlert();
            alert.BillId = bill.BillId;
            alert.AlertDate = FormatHelper.TODateInt(DateTime.Today);
            alert.ValidDate = alert.AlertDate;
            alert.AlertItem = bill.AlertItem;
            alert.AlertStatus = AlertStatus_Old.Unhandled;
            alert.AlertTime = FormatHelper.TOTimeInt(DateTime.Now);
            alert.AlertType = bill.AlertType;
            alert.ItemCode = bill.ItemCode;
            alert.MailNotify = bill.MailNotify;
            alert.SendUser = "SYSTEM";
            alert.MaintainDate = alert.AlertDate;
            alert.MaintainTime = alert.AlertTime;
            alert.MaintainUser = alert.SendUser;

            return alert;
        }
        public void AddAlert(Alert alert)
        {
            if (alert != null)
                alert.AlertID = this.GetNextAlertID();

            alert.SendUser = alert.MaintainUser;

            //首件的预警日期，单独维护
            if (alert.AlertType != AlertType_Old.First)
            {
                alert.AlertDate = alert.MaintainDate;
            }

            alert.AlertTime = alert.MaintainTime;

            //如果预警项是产线,将预警项值写到产线字段
            if (alert.AlertItem == AlertItem_Old.SS)
                alert.SSCode = alert.ItemCode;

            //如果预警项是产品,将预警项值写到产品代码字段
            if (alert.AlertItem == AlertItem_Old.Item)
                alert.ProductCode = alert.ItemCode;

            this._helper.AddDomainObject(alert);
        }

        public void UpdateAlert(Alert alert)
        {
            this._helper.UpdateDomainObject(alert);
        }

        public void DeleteAlert(Alert alert)
        {
            this._helper.DeleteDomainObject(alert,
                new ICheck[]{ new DeleteAssociateCheck( alert,
                                this.DataProvider, 
                                new Type[]{
                                              typeof(AlertHandleLog),
                                              typeof(AlertManualNotifier),
                                              typeof(AlertMailLog)	})});
        }


        public void DeleteAlertAndRelation(System.Collections.ArrayList domainObjects)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < domainObjects.Count; i++)
                {
                    BenQGuru.eMES.Domain.Alert.Alert alert = domainObjects[i] as BenQGuru.eMES.Domain.Alert.Alert;

                    if (alert != null)
                    {
                        //delete handle log
                        object[] objs = QueryAlertHandleLog(alert.AlertID);
                        if (objs != null)
                        {
                            foreach (object obj in objs)
                            {
                                AlertHandleLog log = obj as AlertHandleLog;
                                if (log != null)
                                    DeleteAlertHandleLog(log);
                            }
                        }

                        //delete notifier
                        objs = QueryAlertManualNotifier(alert.AlertID);
                        if (objs != null)
                        {
                            foreach (object obj in objs)
                            {
                                BenQGuru.eMES.Domain.Alert.AlertManualNotifier an = obj as BenQGuru.eMES.Domain.Alert.AlertManualNotifier;
                                if (an != null)
                                    DeleteAlertManualNotifier(an);
                            }
                        }
                        //delete maillog
                        objs = QueryAlertMailLog(alert.AlertID);
                        if (objs != null)
                        {
                            foreach (object obj in objs)
                            {
                                BenQGuru.eMES.Domain.Alert.AlertMailLog log = obj as BenQGuru.eMES.Domain.Alert.AlertMailLog;
                                if (log != null)
                                    this.DeleteAlertMailLog(log);
                            }
                        }
                    }

                    DeleteAlert(alert);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }


        public void DeleteAlert(Alert[] alert)
        {
            this._helper.DeleteDomainObject(alert,
                new ICheck[]{ new DeleteAssociateCheck( alert,
                                this.DataProvider, 
                                new Type[]{
                                              typeof(AlertHandleLog),
                                              typeof(AlertManualNotifier),
                                              typeof(AlertMailLog)	})});
        }

        public object GetAlert(decimal alertID)
        {
            return this.DataProvider.CustomSearch(typeof(Alert), new object[] { alertID });
        }

        /// <summary>
        /// ** 功能描述:	查询Alert的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <returns> Alert的总记录数</returns>
        public int QueryAlertCount(decimal alertID)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLALERT where 1=1 and ALERTID like '{0}%' ", alertID)));
        }

        /// <summary>
        /// ** 功能描述:	查询Alert的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <returns> Alert的总记录数</returns>
        //		public int GetNextAlertID()
        //		{
        //			int max = 1;
        //			try
        //			{
        //				max = this.DataProvider.GetCount(new SQLCondition("select max(alertId) + 1 max from TBLALERT"));
        //			}
        //			catch
        //			{
        //				max = 1;
        //			}
        //			return max;
        //		}

        private int GetNextAlertID()
        {
            return this.DataProvider.GetCount(new SQLCondition("SELECT SEQALERT.NEXTVAL FROM DUAL"));
        }
        /// <summary>
        /// ** 功能描述:	分页查询Alert
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Alert数组</returns>
        public object[] QueryAlert(decimal alertID, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Alert), new PagerCondition(string.Format("select {0} from TBLALERT where 1=1 and ALERTID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert)), alertID), "ALERTID", inclusive, exclusive));
        }


        private string GetCondition(string alerttype, string alertitem, string itemcode, int datefrom, int dateto, string status, string product, string sscode, string msg)
        {
            string sql = " where 1=1";
            if (alerttype != "*")
                sql = sql + " and alerttype = '" + alerttype + "'";

            if (alertitem != "*")
                sql = sql + " and alertitem = '" + alertitem + "'";

            if (itemcode != string.Empty)
                sql = sql + " and itemcode in(" + itemcode + ")";

            if (datefrom != 0)
                sql = sql + " and alertdate >= " + datefrom.ToString();

            if (dateto != 0)
                sql = sql + " and alertdate <=" + dateto.ToString();

            if (status != string.Empty)
                sql = sql + " and TBLALERT.alertstatus in (" + status + ")";

            if (product != string.Empty)
                sql = sql + " and productcode in(" + product + ")";

            if (sscode != string.Empty)
                sql = sql + " and sscode in(" + sscode + ")";

            if (msg != string.Empty)
                sql = sql + " and alertmsg like '%" + msg + "%'";

            return sql;
        }

        public object[] QueryAlert(string alerttype, string alertitem, string itemcode, int datefrom, int dateto, string status, string product, string sscode, string msg, int inclusive, int exclusive)
        {
            string sql = "select {0} from TBLALERT " + GetCondition(alerttype, alertitem, itemcode, datefrom, dateto, status, product, sscode, msg);
            return this.DataProvider.CustomQuery(typeof(Alert), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert))), "ALERTDATE desc,ALERTTIME desc", inclusive, exclusive));
        }

        public object[] QueryAlert2Handle(string alerttype, string alertitem, string itemcode, int datefrom, int dateto, string status, string product, string sscode, string msg)
        {
            string sql = "select TBLALERT.*,b.HANDLEMSG,b.HANDLEUSER,b.USEREMAIL,b.HANDLEDATE,b.HANDLETIME,b.ALERTLEVEL HandlAlertLevel,b.ALERTSTATUS HandlAlertStatus"
                        + " from tblalert "
                        + " left join TBLALERTHANDLELOG b on TBLALERT.alertid = b.alertid "
                        + GetCondition(alerttype, alertitem, itemcode, datefrom, dateto, status, product, sscode, msg);
            return this.DataProvider.CustomQuery(typeof(Alert2Handle), new SQLCondition(sql + " order by TBLALERT.mdate desc,TBLALERT.mtime desc"));
        }

        public int QueryAlertCountShift(string alerttype, string alertitem, string itemcode, int datefrom, int dateto, string status, string product, string sscode, string msg, string shiftCode, int begintime)
        {
            string sql = "select count(*) from TBLALERT " + GetCondition(alerttype, alertitem, itemcode, datefrom, dateto, status, product, sscode, msg);
            if (shiftCode != string.Empty)
                sql = sql + " AND shiftcode='" + shiftCode + "'";

            sql = sql + " and shifttime=" + begintime;

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int QueryAlertCount(string alerttype, string alertitem, string itemcode, int datefrom, int dateto, string status, string product, string sscode, string msg, int inclusive, int exclusive)
        {
            string sql = "select count(*) from TBLALERT " + GetCondition(alerttype, alertitem, itemcode, datefrom, dateto, status, product, sscode, msg);
            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert)))));
        }


        public object[] QueryAlert(string alerttype, string alertitem, string itemcode, int datefrom, int dateto, string status, int inclusive, int exclusive)
        {
            string sql = "select {0} from TBLALERT " + GetCondition(alerttype, alertitem, itemcode, datefrom, dateto, status, string.Empty, string.Empty, string.Empty);
            return this.DataProvider.CustomQuery(typeof(Alert), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert))), "ALERTDATE desc,ALERTTIME desc", inclusive, exclusive));
        }


        public int QueryAlertCount(string alerttype, string alertitem, string itemcode, int datefrom, int dateto, string status, int inclusive, int exclusive)
        {
            string sql = "select count(*) from TBLALERT " + GetCondition(alerttype, alertitem, itemcode, datefrom, dateto, status, string.Empty, string.Empty, string.Empty);
            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert)))));
        }

        public int QueryManualAlertCount(string alertlevel)
        {
            string sql = "select count(*) from tblalert where alerttype = 'Manual'";
            if (alertlevel != "*")
                sql = sql + " and alertlevel= '" + alertlevel + "'";

            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert)))));
        }

        /// <summary>
        /// 根据BillID确认预警的数量
        /// </summary>
        /// <param name="billId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int QueryAlertCount(int billId, string status)
        {

            string sql = "select count(*)  from TBLALERT where 1=1 and billid=" + billId;
            if (status != string.Empty)
                sql = sql + " and alertstatus in (" + status + ")";

            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert)))));
        }

        public int QueryAlertCountDate(int billId, int date, string shiftCode, int begintime, string sscode)
        {
            string sql = "select count(*)  from TBLALERT where 1=1 and billid=" + billId + " and alertdate=" + date;

            if (shiftCode != string.Empty)
                sql = sql + " AND shiftcode='" + shiftCode + "'";

            sql = sql + " and shifttime=" + begintime;

            if (sscode != string.Empty)
                sql = sql + " and sscode ='" + sscode + "'";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryAlert(int billId, string status)
        {

            string sql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert))
                + "from TBLALERT where 1=1 and billid=" + billId;
            if (status != string.Empty)
                sql = sql + " and alertstatus in (" + status + ")";

            return this.DataProvider.CustomQuery(typeof(Alert), new SQLCondition(sql));
        }
        /// <summary>
        /// ** 功能描述:	查询Alert的总行数,以预警级别做查询条件
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <returns> Alert的总记录数</returns>
        public int QueryAlertCount(string alertlevel)
        {
            if (alertlevel == "*")
                alertlevel = "";

            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLALERT where 1=1 and AlertLevel like '{0}%' ", alertlevel)));
        }

        /// <summary>
        /// ** 功能描述:	查询Alert的总行数,以预警级别做查询条件
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Alert数组</returns>
        public object[] QueryAlert(string alertlevel, int inclusive, int exclusive)
        {
            if (alertlevel == "*")
                alertlevel = "";

            return this.DataProvider.CustomQuery(typeof(Alert), new PagerCondition(string.Format("select {0} from TBLALERT where 1=1 and AlertLevel like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert)), alertlevel), "AlertDate DESC,AlertTime desc", inclusive, exclusive));
        }

        //取手动预警
        public object[] QueryManualAlert(string alertlevel, int inclusive, int exclusive)
        {
            if (alertlevel == "*")
                alertlevel = "";

            return this.DataProvider.CustomQuery(typeof(Alert), new PagerCondition(string.Format("select {0} from TBLALERT where 1=1 and alerttype = 'Manual' and AlertLevel like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert)), alertlevel), "AlertDate DESC,AlertTime desc", inclusive, exclusive));
        }
        /// <summary>
        /// ** 功能描述:	获得所有的Alert
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Alert的总记录数</returns>
        public object[] GetAllAlert()
        {
            return this.DataProvider.CustomQuery(typeof(Alert), new SQLCondition(string.Format("select {0} from TBLALERT order by ALERTID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Alert)))));
        }


        #endregion

        #region AlertHandleLog
        /// <summary>
        /// 
        /// </summary>
        public AlertHandleLog CreateNewAlertHandleLog()
        {
            return new AlertHandleLog();
        }

        public void AddAlertHandleLog(AlertHandleLog alertHandleLog)
        {
            this._helper.AddDomainObject(alertHandleLog);
        }

        public void UpdateAlertHandleLog(AlertHandleLog alertHandleLog)
        {
            this._helper.UpdateDomainObject(alertHandleLog);
        }

        public void DeleteAlertHandleLog(AlertHandleLog alertHandleLog)
        {
            this._helper.DeleteDomainObject(alertHandleLog);
        }

        public void DeleteAlertHandleLog(AlertHandleLog[] alertHandleLog)
        {
            this._helper.DeleteDomainObject(alertHandleLog);
        }

        public object GetAlertHandleLog(decimal handleSeq, decimal alertID)
        {
            return this.DataProvider.CustomSearch(typeof(AlertHandleLog), new object[] { handleSeq, alertID });
        }

        /// <summary>
        /// ** 功能描述:	查询AlertHandleLog的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="handleSeq">HandleSeq，模糊查询</param>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <returns> AlertHandleLog的总记录数</returns>
        public int QueryAlertHandleLogCount(decimal handleSeq, decimal alertID)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLALERTHANDLELOG where 1=1 and HANDLESEQ like '{0}%'  and ALERTID like '{1}%' ", handleSeq, alertID)));
        }

        //取下一个SeqNo
        public int GetNextHandleSeq(decimal alertID)
        {

            int max = 1;
            try
            {
                max = this.DataProvider.GetCount(new SQLCondition(String.Format("select max(HANDLESEQ) + 1 max from TBLALERTHANDLELOG where ALERTID={0}", alertID)));
            }
            catch
            {
                max = 1;
            }
            return max;
        }

        /// <summary>
        /// ** 功能描述:	分页查询AlertHandleLog
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="handleSeq">HandleSeq，模糊查询</param>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> AlertHandleLog数组</returns>
        public object[] QueryAlertHandleLog(decimal handleSeq, decimal alertID, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(AlertHandleLog), new PagerCondition(string.Format("select {0} from TBLALERTHANDLELOG where 1=1 and HANDLESEQ like '{1}%'  and ALERTID like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertHandleLog)), handleSeq, alertID), "HANDLESEQ,ALERTID", inclusive, exclusive));
        }

        public object[] QueryAlertHandleLog(decimal alertID)
        {
            return this.DataProvider.CustomQuery(typeof(AlertHandleLog), new SQLCondition(string.Format("select {0} from TBLALERTHANDLELOG where 1=1 and ALERTID = {1} order by HandleDate Desc,HandleTime Desc ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertHandleLog)), alertID)));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的AlertHandleLog
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>AlertHandleLog的总记录数</returns>
        public object[] GetAllAlertHandleLog()
        {
            return this.DataProvider.CustomQuery(typeof(AlertHandleLog), new SQLCondition(string.Format("select {0} from TBLALERTHANDLELOG order by HANDLESEQ,ALERTID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertHandleLog)))));
        }


        #endregion

        #region AlertMailLog
        /// <summary>
        /// 
        /// </summary>
        public AlertMailLog CreateNewAlertMailLog()
        {
            return new AlertMailLog();
        }

        public void AddAlertMailLog(AlertMailLog alertMailLog)
        {
            this._helper.AddDomainObject(alertMailLog);
        }

        public void UpdateAlertMailLog(AlertMailLog alertMailLog)
        {
            this._helper.UpdateDomainObject(alertMailLog);
        }

        public void DeleteAlertMailLog(AlertMailLog alertMailLog)
        {
            this._helper.DeleteDomainObject(alertMailLog);
        }

        public void DeleteAlertMailLog(AlertMailLog[] alertMailLog)
        {
            this._helper.DeleteDomainObject(alertMailLog);
        }

        public object GetAlertMailLog(decimal alertID, decimal seq)
        {
            return this.DataProvider.CustomSearch(typeof(AlertMailLog), new object[] { alertID, seq });
        }

        /// <summary>
        /// ** 功能描述:	查询AlertMailLog的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <param name="seq">Seq，模糊查询</param>
        /// <returns> AlertMailLog的总记录数</returns>
        public int QueryAlertMailLogCount(decimal alertID, decimal seq)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLALERTMAILLOG where 1=1 and ALERTID like '{0}%'  and Seq like '{1}%' ", alertID, seq)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询AlertMailLog
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <param name="seq">Seq，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> AlertMailLog数组</returns>
        public object[] QueryAlertMailLog(decimal alertID, decimal seq, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(AlertMailLog), new PagerCondition(string.Format("select {0} from TBLALERTMAILLOG where 1=1 and ALERTID like '{1}%'  and Seq like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertMailLog)), alertID, seq), "ALERTID,Seq", inclusive, exclusive));
        }

        public object[] QueryAlertMailLog(decimal alertID)
        {
            return this.DataProvider.CustomQuery(typeof(AlertMailLog), new SQLCondition(string.Format("select {0} from TBLALERTMAILLOG where 1=1 and ALERTID ={1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertMailLog)), alertID)));
        }
        /// <summary>
        /// ** 功能描述:	获得所有的AlertMailLog
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>AlertMailLog的总记录数</returns>
        public object[] GetAllAlertMailLog()
        {
            return this.DataProvider.CustomQuery(typeof(AlertMailLog), new SQLCondition(string.Format("select {0} from TBLALERTMAILLOG order by ALERTID,Seq", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertMailLog)))));
        }


        #endregion

        #region AlertManualNotifier
        /// <summary>
        /// 
        /// </summary>
        public AlertManualNotifier CreateNewAlertManualNotifier()
        {
            return new AlertManualNotifier();
        }

        public void AddAlertManualNotifier(AlertManualNotifier alertManualNotifier)
        {
            this._helper.AddDomainObject(alertManualNotifier);
        }

        public void UpdateAlertManualNotifier(AlertManualNotifier alertManualNotifier)
        {
            this._helper.UpdateDomainObject(alertManualNotifier);
        }

        public void DeleteAlertManualNotifier(AlertManualNotifier alertManualNotifier)
        {
            this._helper.DeleteDomainObject(alertManualNotifier);
        }

        public void DeleteAlertManualNotifier(AlertManualNotifier[] alertManualNotifier)
        {
            this._helper.DeleteDomainObject(alertManualNotifier);
        }

        public object GetAlertManualNotifier(decimal alertID)
        {
            return this.DataProvider.CustomSearch(typeof(AlertManualNotifier), new object[] { alertID });
        }

        /// <summary>
        /// ** 功能描述:	查询AlertManualNotifier的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <returns> AlertManualNotifier的总记录数</returns>
        public int QueryAlertManualNotifierCount(decimal alertID)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLALERTMANUALNOTIFIER where 1=1 and ALERTID like '{0}%' ", alertID)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询AlertManualNotifier
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> AlertManualNotifier数组</returns>
        public object[] QueryAlertManualNotifier(decimal alertID, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(AlertManualNotifier), new PagerCondition(string.Format("select {0} from TBLALERTMANUALNOTIFIER where 1=1 and ALERTID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertManualNotifier)), alertID), "ALERTID", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	查询AlertManualNotifier
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="alertID">AlertID，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> AlertManualNotifier数组</returns>
        public object[] QueryAlertManualNotifier(decimal alertID)
        {
            return this.DataProvider.CustomQuery(typeof(AlertManualNotifier), new SQLCondition(string.Format("select {0} from TBLALERTMANUALNOTIFIER where 1=1 and ALERTID = '{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertManualNotifier)), alertID)));
        }
        /// <summary>
        /// ** 功能描述:	获得所有的AlertManualNotifier
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-5 14:02:07
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>AlertManualNotifier的总记录数</returns>
        public object[] GetAllAlertManualNotifier()
        {
            return this.DataProvider.CustomQuery(typeof(AlertManualNotifier), new SQLCondition(string.Format("select {0} from TBLALERTMANUALNOTIFIER order by ALERTID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertManualNotifier)))));
        }


        #endregion

        #region AlertSample
        /// <summary>
        /// 
        /// </summary>
        public AlertSample CreateNewAlertSample()
        {
            return new AlertSample();
        }

        public void AddAlertSample(AlertSample alertSample)
        {
            this._helper.AddDomainObject(alertSample);
        }

        public void UpdateAlertSample(AlertSample alertSample)
        {
            this._helper.UpdateDomainObject(alertSample);
        }

        public void DeleteAlertSample(AlertSample alertSample)
        {
            this._helper.DeleteDomainObject(alertSample);
        }

        public void DeleteAlertSample(AlertSample[] alertSample)
        {
            this._helper.DeleteDomainObject(alertSample);
        }

        public object GetAlertSample(string iD)
        {
            return this.DataProvider.CustomSearch(typeof(AlertSample), new object[] { iD });
        }

        /// <summary>
        /// ** 功能描述:	查询AlertSample的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-8 8:10:34
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="iD">ID，模糊查询</param>
        /// <returns> AlertSample的总记录数</returns>
        public int QueryAlertSampleCount()
        {
            return this.DataProvider.GetCount(new SQLCondition("select count(*) from TBLALERTSAMPLE where 1=1"));
        }

        /// <summary>
        /// ** 功能描述:	分页查询AlertSample
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-8 8:10:34
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="iD">ID，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> AlertSample数组</returns>
        public object[] QueryAlertSample(int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(AlertSample), new PagerCondition(string.Format("select {0} from TBLALERTSAMPLE where 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertSample))), "MDATE DESC,MTIME DESC", inclusive, exclusive));
        }

        public object[] QueryAlertSample()
        {
            return this.DataProvider.CustomQuery(typeof(AlertSample), new SQLCondition(string.Format("select {0} from TBLALERTSAMPLE where 1=1 order by sampledesc", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertSample)))));
        }
        /// <summary>
        /// ** 功能描述:	获得所有的AlertSample
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-9-8 8:10:34
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>AlertSample的总记录数</returns>
        public object[] GetAllAlertSample()
        {
            return this.DataProvider.CustomQuery(typeof(AlertSample), new SQLCondition(string.Format("select {0} from TBLALERTSAMPLE order by ID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertSample)))));
        }


        #endregion

        #endregion

        #region AlertItem

        public AlertItem CreateNewAlertItem()
        {
            return new AlertItem();
        }

        public void AddAlertItem(AlertItem alertItem)
        {
            this._helper.AddDomainObject(alertItem);
        }

        public void DeleteAlertItem(AlertItem alertItem)
        {
            this._helper.DeleteDomainObject(alertItem);
        }

        public void DeleteAlertItems(AlertItem[] alertItems)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (AlertItem item in alertItems)
                {
                    this._helper.DeleteDomainObject(item);

                    switch (item.AlertType)
                    {
                        case AlertType.AlertType_DirectPass: //直通率预警
                            DeleteAlertDirectPassByItemSeq(item.ItemSequence);
                            break;
                        case AlertType.AlertType_Error: //故障预警
                            DeleteAlertErrorByItemSeq(item.ItemSequence);
                            break;
                        case AlertType.AlertType_ErrorCode://不良原因预警
                            DeleteAlertErrorCodeByItemSeq(item.ItemSequence);
                            break;
                        case AlertType.AlertType_LinePause://停线时长预警
                            DeleteAlertLinePauseByItemSeq(item.ItemSequence);
                            break;
                        case AlertType.AlertType_OQCNG://OQC抽检故障重复预警
                            DeleteAlertOQCNGByItemSeq(item.ItemSequence);
                            break;
                    }

                    this.DeleteAlertMailSettingByItemSeq(item.ItemSequence);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Delete", ex);
            }

        }

        public void UpdateAlertItem(AlertItem alertItem)
        {
            this._helper.UpdateDomainObject(alertItem);
        }

        public void UpdateAlertItem1(AlertItem alertItem)
        {
            this.DataProvider.Update(alertItem);
        }

        public object GetAlertItem(string itemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertItem), new object[] { itemSequence });
        }

        /// <summary>
        /// 对字段alertType使用模糊查询
        /// </summary>
        /// <param name="model"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryAlertItems(string alertType, int inclusive, int exclusive)
        {

            string selectSql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertItem)) + "  FROM tblAlertItem ";
            if (!alertType.Equals(string.Empty))
            {
                selectSql += " WHERE alerttype='" + alertType + "'";
            }

            selectSql += " ORDER BY itemsequence";

            return this.DataProvider.CustomQuery(typeof(AlertItem), new PagerCondition(selectSql, inclusive, exclusive));
        }

        public int QueryAlertItemsCount(string alertType)
        {
            string selectSql = "SELECT COUNT(alerttype) FROM tblAlertItem ";
            if (!alertType.Equals(string.Empty))
            {
                selectSql += " WHERE alerttype='" + alertType + "'";
            }


            return this.DataProvider.GetCount(new SQLCondition(selectSql));
        }

        public string GetItemSequence(string alertType)
        {
            int seq = 0;

            string selectSql = "SELECT MAX(itemsequence) as itemsequence";
            selectSql += "  FROM tblalertitem";
            selectSql += " WHERE alerttype = '" + alertType + "'";
            selectSql += " GROUP BY alerttype";

            object[] obj = this.DataProvider.CustomQuery(typeof(AlertItem), new SQLCondition(selectSql));

            if (obj != null)
            {
                string itemSequence = ((AlertItem)obj[0]).ItemSequence;
                int.TryParse(itemSequence.Substring(itemSequence.LastIndexOf("-") + 1), out seq);
            }

            if (seq <= 0)
            {
                seq = 1;
            }
            else if (seq >= 999)
            {
                throw new Exception("$Error_SeqOverFlow");
            }
            else
            {
                seq++;
            }

            return seq.ToString("000");
        }

        public string GetSubItemSequence()
        {
            return "";
        }

        #endregion

        #region AlertError

        public AlertError CreateNewAlertError()
        {
            return new AlertError();
        }

        public void AddAlertError(AlertError alertError)
        {
            this._helper.AddDomainObject(alertError);
        }

        public void DeleteAlertError(AlertError alertError)
        {
            this._helper.DeleteDomainObject(alertError);
        }

        public void DeleteAlertErrorByItemSeq(string itemSeq)
        {
            string selectSql = "DELETE FROM tblAlertError WHERE itemsequence='" + itemSeq + "'";

            this.DataProvider.CustomExecute(new SQLCondition(selectSql));
        }

        public void UpdateAlertError(AlertError alertError)
        {
            this._helper.UpdateDomainObject(alertError);
        }

        public object GetAlertError(string subItemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertError), new object[] { subItemSequence });
        }

        public object[] GetAlertErrors(string itemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertError), new string[] { "ItemSequence" }, new object[] { itemSequence });
        }

        public void SaveAlertError(AlertError alertError)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                DeleteAlertErrorByItemSeq(alertError.ItemSequence);

                if (alertError.ErrorCode == string.Empty)
                {
                    this.DataProvider.CommitTransaction();
                    return;
                }
                else
                {
                    string[] errorCodes = alertError.ErrorCode.Split(',');

                    for (int i = 0; i < errorCodes.Length; i++)
                    {
                        alertError.SubItemSequence = alertError.ItemSequence + "-" + string.Format("{0:D3}", i + 1);
                        alertError.ErrorCode = errorCodes[i];
                        this._helper.AddDomainObject(alertError);
                    }

                }

                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", ex);
            }

        }

        #endregion

        #region AlertErrorCode

        public AlertErrorCode CreateNewAlertErrorCode()
        {
            return new AlertErrorCode();
        }

        public void AddAlertErrorCode(AlertErrorCode alertErrorCode)
        {
            this._helper.AddDomainObject(alertErrorCode);
        }

        public void DeleteAlertErrorCode(AlertErrorCode alertErrorCode)
        {
            this._helper.DeleteDomainObject(alertErrorCode);
        }

        public void DeleteAlertErrorCodeByItemSeq(string itemSeq)
        {
            string selectSql = "DELETE FROM tblAlertErrorCode WHERE itemsequence='" + itemSeq + "'";

            this.DataProvider.CustomExecute(new SQLCondition(selectSql));
        }

        public void UpdateAlertErrorCode(AlertErrorCode alertErrorCode)
        {
            this._helper.UpdateDomainObject(alertErrorCode);
        }

        public object GetAlertErrorCode(string subItemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertErrorCode), new object[] { subItemSequence });
        }

        public object[] GetAlertErrorCodes(string itemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertErrorCode), new string[] { "ItemSequence" }, new object[] { itemSequence });
        }

        public void SaveAlertErrorCode(AlertErrorCode alertError)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                DeleteAlertErrorCodeByItemSeq(alertError.ItemSequence);

                if (alertError.ErrorCauseCode == string.Empty)
                {
                    this.DataProvider.CommitTransaction();
                    return;
                }
                else
                {
                    string[] errorCodes = alertError.ErrorCauseCode.Split(',');

                    for (int i = 0; i < errorCodes.Length; i++)
                    {
                        alertError.SubItemSequence = alertError.ItemSequence + "-" + string.Format("{0:D3}", i + 1);
                        alertError.ErrorCauseCode = errorCodes[i];
                        this._helper.AddDomainObject(alertError);
                    }

                }

                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", ex);
            }

        }

        #endregion

        #region AlertDirectPass

        public AlertDirectPass CreateNewAlertDirectPass()
        {
            return new AlertDirectPass();
        }

        public void AddAlertDirectPass(AlertDirectPass alertDirectPass)
        {
            this._helper.AddDomainObject(alertDirectPass);
        }

        public void DeleteAlertDirectPass(AlertDirectPass alertDirectPass)
        {
            this._helper.DeleteDomainObject(alertDirectPass);
        }

        public void DeleteAlertDirectPassByItemSeq(string itemSeq)
        {
            string selectSql = "DELETE FROM tblAlertDirectPass WHERE itemsequence='" + itemSeq + "'";

            this.DataProvider.CustomExecute(new SQLCondition(selectSql));
        }

        public void UpdateAlertDirectPass(AlertDirectPass alertDirectPass)
        {
            this._helper.UpdateDomainObject(alertDirectPass);
        }

        public object GetAlertDirectPass(string subItemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertDirectPass), new object[] { subItemSequence });
        }

        public object[] GetAlertDirectPasses(string itemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertDirectPass), new string[] { "ItemSequence" }, new object[] { itemSequence });
        }

        public void SaveAlertDirectPass(AlertDirectPass alertDirectPass)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                DeleteAlertDirectPassByItemSeq(alertDirectPass.ItemSequence);

                alertDirectPass.SubItemSequence = alertDirectPass.ItemSequence + "-001";
                this._helper.AddDomainObject(alertDirectPass);

                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", ex);
            }

        }

        #endregion

        #region AlertLinePause

        public AlertLinePause CreateNewAlertLinePause()
        {
            return new AlertLinePause();
        }

        public void AddAlertLinePause(AlertLinePause alertLinePause)
        {
            this._helper.AddDomainObject(alertLinePause);
        }

        public void DeleteAlertLinePause(AlertLinePause alertLinePause)
        {
            this._helper.DeleteDomainObject(alertLinePause);
        }

        public void DeleteAlertLinePauseByItemSeq(string itemSeq)
        {
            string selectSql = "DELETE FROM tblAlertLinePause WHERE itemsequence='" + itemSeq + "'";

            this.DataProvider.CustomExecute(new SQLCondition(selectSql));
        }

        public void UpdateAlertLinePause(AlertLinePause alertLinePause)
        {
            this._helper.UpdateDomainObject(alertLinePause);
        }

        public object GetAlertLinePause(string subItemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertLinePause), new object[] { subItemSequence });
        }

        public object[] GetAlertLinePauses(string itemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertLinePause), new string[] { "ItemSequence" }, new object[] { itemSequence });
        }

        public void SaveAlertLinePause(AlertLinePause alertError)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                DeleteAlertLinePauseByItemSeq(alertError.ItemSequence);

                if (alertError.SSCode == string.Empty)
                {
                    this.DataProvider.CommitTransaction();
                    return;
                }
                else
                {
                    string[] SSCodes = alertError.SSCode.Split(',');

                    for (int i = 0; i < SSCodes.Length; i++)
                    {
                        alertError.SubItemSequence = alertError.ItemSequence + "-" + string.Format("{0:D3}", i + 1);
                        alertError.SSCode = SSCodes[i];
                        this._helper.AddDomainObject(alertError);
                    }

                }

                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", ex);
            }

        }
        #endregion

        #region AlertOQCNG

        public AlertOQCNG CreateNewAlertOQCNG()
        {
            return new AlertOQCNG();
        }

        public void AddAlertOQCNG(AlertOQCNG alertOQCNG)
        {
            this._helper.AddDomainObject(alertOQCNG);
        }

        public void DeleteAlertOQCNG(AlertOQCNG alertOQCNG)
        {
            this._helper.DeleteDomainObject(alertOQCNG);
        }

        public void DeleteAlertOQCNGByItemSeq(string itemSeq)
        {
            string selectSql = "DELETE FROM tblAlertOQCNG WHERE itemsequence='" + itemSeq + "'";

            this.DataProvider.CustomExecute(new SQLCondition(selectSql));
        }

        public void UpdateAlertOQCNG(AlertOQCNG alertOQCNG)
        {
            this._helper.UpdateDomainObject(alertOQCNG);
        }

        public object GetAlertOQCNG(string subItemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertOQCNG), new object[] { subItemSequence });
        }

        public object[] GetAlertOQCNGs(string itemSequence)
        {
            return this.DataProvider.CustomSearch(typeof(AlertOQCNG), new string[] { "ItemSequence" }, new object[] { itemSequence });
        }

        public void SaveAlertOQCNG(AlertOQCNG alertOQCNG)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                DeleteAlertOQCNGByItemSeq(alertOQCNG.ItemSequence);

                if (alertOQCNG.ErrorCode == string.Empty)
                {
                    this.DataProvider.CommitTransaction();
                    return;
                }
                else
                {
                    string[] errorCodes = alertOQCNG.ErrorCode.Split(',');

                    for (int i = 0; i < errorCodes.Length; i++)
                    {
                        alertOQCNG.SubItemSequence = alertOQCNG.ItemSequence + "-" + string.Format("{0:D3}", i + 1);
                        alertOQCNG.ErrorCode = errorCodes[i];
                        this._helper.AddDomainObject(alertOQCNG);
                    }

                }

                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", ex);
            }

        }

        #endregion

        #region AlertMailSetting

        public AlertMailSetting CreateNewAlertMailSetting()
        {
            return new AlertMailSetting();
        }

        public void AddAlertMailSetting(AlertMailSetting alertMailSetting)
        {
            try
            {
                this.DataProvider.BeginTransaction();


                this._helper.AddDomainObject(alertMailSetting);


                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Delete", ex);
            }


        }

        public void DeleteAlertMailSetting(AlertMailSetting alertMailSetting)
        {
            this._helper.DeleteDomainObject(alertMailSetting);
        }

        public void DeleteAlertMailSetting(AlertMailSetting[] alertMailSettings)
        {
            this._helper.DeleteDomainObject(alertMailSettings);
        }

        public void DeleteAlertMailSettingByItemSeq(string itemSeq)
        {
            string selectSql = "DELETE FROM tblAlertMailSetting WHERE itemsequence='" + itemSeq + "'";

            this.DataProvider.CustomExecute(new SQLCondition(selectSql));
        }

        public void UpdateAlertMailSetting(AlertMailSetting alertMailSetting)
        {

            try
            {
                this.DataProvider.BeginTransaction();

                DeleteAlertMailSettingByItemSeq(alertMailSetting.ItemSequence);


                this._helper.AddDomainObject(alertMailSetting);

                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Delete", ex);
            }
        }

        public object GetAlertMailSetting(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(AlertMailSetting), new object[] { serial });
        }


        public object[] QueryAlertMailSettings(string itemSequence, int inclusive, int exclusive)
        {

            string selectSql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertMailSetting)) + "  FROM tblalertmailsetting ";
            selectSql += " WHERE itemsequence='" + itemSequence + "'";

            selectSql += " ORDER BY serial";

            return this.DataProvider.CustomQuery(typeof(AlertMailSetting), new PagerCondition(selectSql, inclusive, exclusive));
        }

        public object[] QueryAlertMailSettings1(string itemSequence, int inclusive, int exclusive)
        {

            string selectSql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertMailSetting)) + "  FROM tblalertmailsetting ";
            selectSql += " WHERE itemsequence='" + itemSequence.ToLower() + "'";

            selectSql += " ORDER BY serial";

            return this.DataProvider.CustomQuery(typeof(AlertMailSetting), new PagerCondition(selectSql, inclusive, exclusive));
        }

        public int QueryAlertMailSettingsCount(string itemSequence)
        {
            string selectSql = "SELECT COUNT(serial) FROM tblalertmailsetting ";
            selectSql += " WHERE itemsequence='" + itemSequence.ToLower() + "'";



            return this.DataProvider.GetCount(new SQLCondition(selectSql));
        }


        public int QueryAlertMailSettingsCount1(string itemSequence)
        {
            string selectSql = "SELECT COUNT(serial) FROM tblalertmailsetting ";
            selectSql += " WHERE itemsequence='" + itemSequence + "'";



            return this.DataProvider.GetCount(new SQLCondition(selectSql));
        }

        #endregion

        #region AlertNotice

        public AlertNotice CreateNewAlertNotice()
        {
            return new AlertNotice();
        }

        public void AddAlertNotice(AlertNotice alertNotice)
        {
            this._helper.AddDomainObject(alertNotice);
        }

        public void DeleteAlertNotice(AlertNotice alertNotice)
        {
            this._helper.DeleteDomainObject(alertNotice);
        }

        public void UpdateAlertNotice(AlertNotice alertNotice)
        {
            this._helper.UpdateDomainObject(alertNotice);
        }

        public object GetAlertNotice(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(AlertNotice), new object[] { serial });
        }

        public object[] QueryAlertNotice(string alertType, int startDate, int endDate, string dealStatus,
                                     int inclusive, int exclusive)
        {
            string selectSql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(AlertNotice)) + "  FROM tblAlertNotice WHERE 1=1 ";

            if (!alertType.Equals(string.Empty))
            {
                selectSql += " AND alerttype='" + alertType + "'";
            }
            if (!dealStatus.Equals(string.Empty))
            {
                selectSql += " AND status='" + dealStatus + "'";
            }
            if (startDate > 0)
            {
                selectSql += " AND noticedate>=" + startDate.ToString();
            }
            if (endDate > 0)
            {
                selectSql += " AND noticedate<=" + endDate.ToString();
            }

            selectSql += " ORDER BY alerttype,itemsequence";

            return this.DataProvider.CustomQuery(typeof(AlertNotice), new PagerCondition(selectSql, inclusive, exclusive));
        }

        public int QueryAlertNoticeCount(string alertType, int startDate, int endDate, string dealStatus)
        {
            string selectSql = "SELECT count(alerttype)  FROM tblAlertNotice WHERE 1=1 ";

            if (!alertType.Equals(string.Empty))
            {
                selectSql += " AND alerttype='" + alertType + "'";
            }
            if (!dealStatus.Equals(string.Empty))
            {
                selectSql += " AND status='" + dealStatus + "'";
            }
            if (startDate > 0)
            {
                selectSql += " AND noticedate>=" + startDate.ToString();
            }
            if (endDate > 0)
            {
                selectSql += " AND noticedate<=" + endDate.ToString();
            }

            selectSql += " ORDER BY alerttype,itemsequence";

            return this.DataProvider.GetCount(new SQLCondition(selectSql));
        }

        public void QuickDealAlertNotice(AlertNotice[] alertNotices)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (AlertNotice notice in alertNotices)
                {
                    this._helper.UpdateDomainObject(notice);

                    if (notice.MOList.Trim() != string.Empty)
                    {
                        string[] moCodeArray = notice.MOList.Trim().Split(',');
                        if (moCodeArray != null && moCodeArray.Length > 0)
                        {
                            foreach (string moCode in moCodeArray)
                            {
                                if (QueryNotDealAlertNoticeCountByMO(notice.Serial, moCode) <= 0)
                                {
                                    UpdateMOStatus(moCode);
                                }
                            }
                        }
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Delete", ex);
            }
        }

        public int QueryNotDealAlertNoticeCountByMO(int alertNoticeSerial, string moCode)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += "FROM tblalertnotice ";
            sql += "WHERE status = '{0}' ";
            sql += "AND serial <> {1} ";
            sql += "AND ',' || molist || ',' LIKE '%,{2},%' ";
            sql = string.Format(sql, AlertNoticeStatus.AlertNoticeStatus_NotDeal, alertNoticeSerial.ToString(), moCode);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public void UpdateMOStatus(string moList)
        {
            string mocode = moList.Replace(",", "','");
            mocode = mocode.Insert(0, "'");
            mocode = mocode.Insert(mocode.Length, "'");

            string sql = "";
            sql += "UPDATE tblmo";
            sql += "   SET MOSTATUS = '" + MOManufactureStatus.MOSTATUS_OPEN + "', eattribute3 = '', mopendingcause = ''";
            sql += " WHERE mocode IN (" + mocode + ")";
            sql += "   AND mostatus = '" + MOManufactureStatus.MOSTATUS_PENDING + "'";
            sql += "   AND eattribute3 = '" + AlertJob.alertJob + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region NoticeError

        public NoticeError CreateNewNoticeError()
        {
            return new NoticeError();
        }

        public void AddNoticeError(NoticeError noticeError)
        {
            this._helper.AddDomainObject(noticeError);
        }

        public void DeleteNoticeError(NoticeError noticeError)
        {
            this._helper.DeleteDomainObject(noticeError);
        }

        public void UpdateNoticeError(NoticeError noticeError)
        {
            this._helper.UpdateDomainObject(noticeError);
        }

        public object GetNoticeError(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(NoticeError), new object[] { serial });
        }

        #endregion

        #region NoticeErrorCode

        public NoticeErrorCode CreateNewNoticeErrorCode()
        {
            return new NoticeErrorCode();
        }

        public void AddNoticeErrorCode(NoticeErrorCode noticeErrorCode)
        {
            this._helper.AddDomainObject(noticeErrorCode);
        }

        public void DeleteNoticeErrorCode(NoticeErrorCode noticeErrorCode)
        {
            this._helper.DeleteDomainObject(noticeErrorCode);
        }

        public void UpdateNoticeErrorCode(NoticeErrorCode noticeErrorCode)
        {
            this._helper.UpdateDomainObject(noticeErrorCode);
        }

        public object GetNoticeErrorCode(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(NoticeErrorCode), new object[] { serial });
        }

        #endregion

        #region NoticeDirectPass

        public NoticeDirectPass CreateNewNoticeDirectPass()
        {
            return new NoticeDirectPass();
        }

        public void AddNoticeDirectPass(NoticeDirectPass noticeDirectPass)
        {
            this._helper.AddDomainObject(noticeDirectPass);
        }

        public void DeleteNoticeDirectPass(NoticeDirectPass noticeDirectPass)
        {
            this._helper.DeleteDomainObject(noticeDirectPass);
        }

        public void UpdateNoticeDirectPass(NoticeDirectPass noticeDirectPass)
        {
            this._helper.UpdateDomainObject(noticeDirectPass);
        }

        public object GetNoticeDirectPass(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(NoticeDirectPass), new object[] { serial });
        }

        #endregion

        #region NoticeLinePause

        public NoticeLinePause CreateNewNoticeLinePause()
        {
            return new NoticeLinePause();
        }

        public void AddNoticeLinePause(NoticeLinePause noticeLinePause)
        {
            this._helper.AddDomainObject(noticeLinePause);
        }

        public void DeleteNoticeLinePause(NoticeLinePause noticeLinePause)
        {
            this._helper.DeleteDomainObject(noticeLinePause);
        }

        public void UpdateNoticeLinePause(NoticeLinePause noticeLinePause)
        {
            this._helper.UpdateDomainObject(noticeLinePause);
        }

        public object GetNoticeLinePause(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(NoticeLinePause), new object[] { serial });
        }

        #endregion

        #region MailUser

        public object[] QueryUserMailByCodes(string codes)
        {
            string usercodes = "";
            usercodes = codes.Replace(",", "','");

            usercodes = usercodes.Insert(0, "'");
            usercodes = usercodes.Insert(usercodes.Length, "'");
            string sql = "SELECT useremail FROM tbluser WHERE useremail is not null AND usercode IN(" + usercodes + ")";

            return this.DataProvider.CustomQuery(typeof(User), new SQLCondition(sql));
        }

        //added by leon.li @20121120
        public object[] QueryUserTelByCodes(string codes)
        {
            string usercodes = "";
            usercodes = codes.Replace(",", "','");

            usercodes = usercodes.Insert(0, "'");
            usercodes = usercodes.Insert(usercodes.Length, "'");
            string sql = "SELECT usertel FROM tbluser WHERE usertel is not null AND usercode IN(" + usercodes + ")";

            return this.DataProvider.CustomQuery(typeof(User), new SQLCondition(sql));
        }

        public string GetRecipients(string itemSequence, string bigSSCode, string firstClass, string secondClass)
        {
            string returnValue = string.Empty;

            itemSequence = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(itemSequence));
            if (itemSequence.Trim().Length <= 0)
            {
                itemSequence = " ";
            }
            bigSSCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(bigSSCode));
            if (bigSSCode.Trim().Length <= 0)
            {
                bigSSCode = " ";
            }
            firstClass = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(firstClass));
            if (firstClass.Trim().Length <= 0)
            {
                firstClass = " ";
            }
            secondClass = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(secondClass));
            if (secondClass.Trim().Length <= 0)
            {
                secondClass = " ";
            }

            string sql = string.Empty;
            sql += "SELECT recipients ";
            sql += "FROM tblalertmailsetting ";
            sql += "WHERE NVL(TRIM(itemsequence), ' ') = '" + itemSequence + "' ";
            sql += "AND NVL(TRIM(bigsscode), ' ') = '" + bigSSCode + "' ";
            sql += "AND NVL(TRIM(itemfirstclass), ' ') = '" + firstClass + "' ";
            sql += "AND NVL(TRIM(itemsecondclass), ' ') = '" + secondClass + "' ";
            sql += "UNION ";
            sql += "SELECT recipients ";
            sql += "FROM tblalertmailsetting ";
            sql += "WHERE NVL(TRIM(itemsequence), ' ') = '" + itemSequence + "' ";
            sql += "AND NVL(TRIM(bigsscode), ' ') = ' ' ";
            sql += "AND NVL(TRIM(itemfirstclass), ' ') = '" + firstClass + "' ";
            sql += "AND NVL(TRIM(itemsecondclass), ' ') ='" + secondClass + "' ";
            sql += "UNION ";
            sql += "SELECT recipients ";
            sql += "FROM tblalertmailsetting ";
            sql += "WHERE NVL(TRIM(itemsequence), ' ') = '" + itemSequence + "' ";
            sql += "AND NVL(TRIM(bigsscode), ' ') = ' ' ";
            sql += "AND NVL(TRIM(itemfirstclass), ' ') = ' ' ";
            sql += "AND NVL(TRIM(itemsecondclass), ' ') = ' ' ";
            sql += "UNION ";
            sql += "SELECT recipients ";
            sql += "FROM tblalertmailsetting ";
            sql += "WHERE NVL(TRIM(itemsequence), ' ') = '" + itemSequence + "' ";
            sql += "AND NVL(TRIM(bigsscode), ' ') = '" + bigSSCode + "' ";
            sql += "AND NVL(TRIM(itemfirstclass), ' ') = ' ' ";
            sql += "AND NVL(TRIM(itemsecondclass), ' ') = ' ' ";
            sql += "UNION ";
            sql += "SELECT recipients ";
            sql += "FROM tblalertmailsetting ";
            sql += "WHERE NVL(TRIM(itemsequence), ' ') = '" + itemSequence + "' ";
            sql += "AND NVL(TRIM(bigsscode), ' ') = '" + bigSSCode + "' ";
            sql += "AND NVL(TRIM(itemfirstclass), ' ') = '" + firstClass + "' ";
            sql += "AND NVL(TRIM(itemsecondclass), ' ') = ' ' ";

            object[] alertMailSettingArray = this.DataProvider.CustomQuery(typeof(AlertMailSetting), new SQLCondition(sql));
            if (alertMailSettingArray != null)
            {
                foreach (AlertMailSetting setting in alertMailSettingArray)
                {
                    returnValue += setting.Recipients + ';';
                }
            }

            //Distinct Mail Address列表            
            string[] mailAddressArray = returnValue.Split(';');
            returnValue = ";";

            if (mailAddressArray != null && mailAddressArray.Length > 0)
            {
                foreach (string mailAddress in mailAddressArray)
                {
                    if (mailAddress.Trim().Length > 0 && returnValue.IndexOf(";" + mailAddress.Trim().ToLower() + ";") < 0)
                    {
                        returnValue += mailAddress.Trim().ToLower() + ';';
                    }
                }
            }

            if (returnValue.Trim().Length > 1)
            {
                returnValue = returnValue.Substring(1, returnValue.Length - 2).Trim();
            }
            else
            {
                returnValue = " ";
            }

            return returnValue;
        }

        private string FormatMailContent(string mailContentTemplate, List<string> variableList, List<string> valueList)
        {
            string returnValue = mailContentTemplate;

            if (variableList != null && valueList != null)

                for (int i = 0; i < variableList.Count && i < valueList.Count; i++)
                {
                    returnValue = returnValue.Replace(variableList[i], valueList[i]);
                }

            return returnValue;
        }

        #endregion

        #region Alert For OQCReject & IQCNG

        public void AlertOQCReject(string itemCode, string ssCode, string lotNo, string reason)
        {
            //获取AlertItem
            object[] alertItemArray = QueryAlertItems(AlertType.AlertType_OQCReject, int.MinValue, int.MaxValue);

            if (alertItemArray == null || alertItemArray.Length <= 0)
            {
                return;
            }

            //获取Alert信息
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            object[] countForAlertArray = oqcFacade.QueryOQCLotCountForAlert(itemCode, ssCode, OQCLotStatus.OQCLotStatus_Reject);
            if (countForAlertArray == null || countForAlertArray.Length <= 0)
            {
                return;
            }
            OQCLotCountForAlert countForAlert = (OQCLotCountForAlert)countForAlertArray[0];

            DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DateTime formatDatetime = now.DateTime;

            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(itemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            string itemAndDesc = itemCode;
            if (material != null)
            {
                //itemAndDesc += " - " + material.MaterialDescription;
            }

            List<string> variableList = new List<string>();
            variableList.Add("$$datetime$$");
            variableList.Add("$$mcode$$");
            variableList.Add("$$bigsscode$$");
            variableList.Add("$$alertvalue$$");
            variableList.Add("$$lotno$$");
            variableList.Add("$$reason$$");

            List<string> valueList = new List<string>();
            valueList.Add(formatDatetime.ToLongDateString() + " " + formatDatetime.ToLongTimeString());
            valueList.Add(itemAndDesc);
            valueList.Add(countForAlert.BigSSCode);
            valueList.Add(countForAlert.OQCLotCount.ToString());
            valueList.Add(lotNo);
            valueList.Add(reason);

            //获取FirstClass和SecondClass
            object[] itemClassArray = itemFacade.QueryItemClass(itemCode);
            if (itemClassArray == null || itemClassArray.Length <= 0)
            {
                return;
            }
            ItemClass itemClass = (ItemClass)itemClassArray[0];

            MailFacade mailFacade = new MailFacade(this.DataProvider);
            foreach (AlertItem alertItem in alertItemArray)
            {
                string mailContent = FormatMailContent(alertItem.MailContent, variableList, valueList);

                //Add Mail
                Mail alertMail = mailFacade.CreateNewMail();
                alertMail.Recipients = GetRecipients(alertItem.ItemSequence, countForAlert.BigSSCode, itemClass.FirstClass, itemClass.SecondClass);
                alertMail.MailSubject = alertItem.MailSubject;
                alertMail.MailContent = mailContent;
                alertMail.IsSend = "N";
                alertMail.SendTimes = 0;
                alertMail.SendResult = " ";
                alertMail.ErrorMessage = " ";
                alertMail.MaintainUser = "ALERTJOB";
                alertMail.EAttribute1 = alertItem.ItemSequence;
                alertMail.EAttribute2 = alertItem.AlertType;
                alertMail.EAttribute3 = "0";

                if (alertMail.Recipients.Trim().Length > 0)
                {
                    mailFacade.AddMail(alertMail);
                }
            }
        }

        public void AlertIQCNG(string vendor, int orgID, string itemCode, string iqcNo, int stLine, string reason)
        {
            //获取AlertItem
            object[] alertItemArray = QueryAlertItems(AlertType.AlertType_IQCNG, int.MinValue, int.MaxValue);

            if (alertItemArray == null || alertItemArray.Length <= 0)
            {
                return;
            }

            //获取物料描述，FirstClass和SecondClass
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);

            string itemDesc = string.Empty;
            Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(itemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (material != null)
            {
                //itemDesc = material.MaterialDescription;
            }

            object[] itemClassArray = itemFacade.QueryItemClass(itemCode);
            if (itemClassArray == null || itemClassArray.Length <= 0)
            {
                return;
            }
            ItemClass itemClass = (ItemClass)itemClassArray[0];

            DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DateTime formatDatetime = now.DateTime;

            //获取Alert信息
            List<string> variableList = new List<string>();
            variableList.Add("$$datetime$$");
            variableList.Add("$$vendor$$");
            variableList.Add("$$itemcode$$");
            variableList.Add("$$iqcno$$");
            variableList.Add("$$reason$$");

            List<string> valueList = new List<string>();
            valueList.Add(formatDatetime.ToLongDateString() + " " + formatDatetime.ToLongTimeString());
            valueList.Add(vendor);
            valueList.Add(itemCode + " - " + itemDesc);
            valueList.Add(iqcNo + "(" + stLine.ToString() + ")");
            valueList.Add(reason);

            MailFacade mailFacade = new MailFacade(this.DataProvider);
            foreach (AlertItem alertItem in alertItemArray)
            {
                string mailContent = FormatMailContent(alertItem.MailContent, variableList, valueList);

                //Add Mail
                string recipients = GetRecipients(alertItem.ItemSequence, " ", itemClass.FirstClass, itemClass.SecondClass);
                //Mark by hiro 基础版暂不和SRM交互，根据项目需要是否Mark
                //string iqcRecipients = GetIQCMailList(orgID, itemCode);
                //if (iqcRecipients.Trim().Length > 0)
                //{
                //    recipients += iqcRecipients;
                //}

                Mail alertMail = mailFacade.CreateNewMail();
                alertMail.Recipients = recipients;
                alertMail.MailSubject = alertItem.MailSubject;
                alertMail.MailContent = mailContent;
                alertMail.IsSend = "N";
                alertMail.SendTimes = 0;
                alertMail.SendResult = " ";
                alertMail.ErrorMessage = " ";
                alertMail.MaintainUser = "ALERTJOB";
                alertMail.EAttribute1 = alertItem.ItemSequence;
                alertMail.EAttribute2 = alertItem.AlertType;
                alertMail.EAttribute3 = "0";

                if (alertMail.Recipients.Trim().Length > 0)
                {
                    mailFacade.AddMail(alertMail);
                }

                //Add AlertNotice
                AlertNotice alertNotice = CreateNewAlertNotice();
                alertNotice.AlertType = alertItem.AlertType;
                alertNotice.ItemSequence = alertItem.ItemSequence;
                alertNotice.SubItemSequence = "0";
                alertNotice.Description = alertItem.Description;
                alertNotice.NoticeContent = mailContent;
                alertNotice.NoticeSerial = 0;
                alertNotice.NoticeDate = now.DBDate;
                alertNotice.NoticeTime = now.DBTime;
                alertNotice.AnalysisReason = " ";
                alertNotice.DealMethods = " ";
                alertNotice.MOList = " ";
                alertNotice.DealDate = 0;
                alertNotice.DealTime = 0;
                alertNotice.DealUser = " ";
                alertNotice.Status = AlertNoticeStatus.AlertNoticeStatus_NotDeal;
                alertNotice.MaintainUser = "ALERTJOB";
                AddAlertNotice(alertNotice);
            }
        }

        #endregion

        #region SRM DB Link

        private string _SRMDBLink = string.Empty;
        private string GetSRMDBLink()
        {
            if (_SRMDBLink.Trim().Length <= 0 || _SRMDBLink.IndexOf("@") < 0)
            {
                Parameter srmDBLink = (Parameter)(new SystemSettingFacade(this.DataProvider)).GetParameter("SRMDBLINK", "SRMINTERFACE");

                if (srmDBLink == null)
                {
                    _SRMDBLink = "@SRM";
                }
                else
                {
                    _SRMDBLink = srmDBLink.ParameterAlias.Trim().ToUpper();
                    if (_SRMDBLink.IndexOf("@") != 0)
                    {
                        _SRMDBLink = "@" + _SRMDBLink;
                    }
                }
            }

            return _SRMDBLink;
        }

        private string GetIQCMailList(int ordID, string itemCode)
        {
            string returnValue = string.Empty;

            string srmDBLink = GetSRMDBLink();

            string sql = string.Empty;
            sql += "SELECT DISTINCT useremail AS recipients ";
            sql += "FROM plant2item{0} m, purchusergroup{0} p, sysuser{0} u ";
            sql += "WHERE m.purchugcode = p.purchugcode ";
            sql += "AND u.username = p.purchugdesc ";
            sql += "AND m.purchugcode <> ' ' ";
            sql += "AND m.plantcode = {1} ";
            sql += "AND m.itemcode = '{2}' ";
            sql = string.Format(sql, srmDBLink, ordID.ToString(), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(itemCode)));

            object[] mailArray = this.DataProvider.CustomQuery(typeof(AlertMailSetting), new SQLCondition(sql));

            if (mailArray != null && mailArray.Length > 0)
            {
                foreach (AlertMailSetting mailSetting in mailArray)
                {
                    returnValue += ";" + mailSetting.Recipients;
                }
            }

            return returnValue;
        }

        #endregion
    }
}
