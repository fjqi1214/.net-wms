using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.IQC;

namespace BenQGuru.eMES.SRMInterface
{
    public class InterfaceModelFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;
        private const string TS_Operation = "TS";

        public InterfaceModelFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        //Laws Lu,max life time to unlimited
        public override object InitializeLifetimeService()
        {
            return null;
        }

        public InterfaceModelFacade()
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

        public int QueryIQCHeadCount(string IQCNo, string ASNNo, bool statusWaitCheck, bool statusClosed, bool ROHS, string vendorCode, string appDateFrom, string appDateTo)
        {
            string qSql = string.Format("select count(*) from tblasn asn, tblasniqc iqc where asn.stno=iqc.stno ");
            if (IQCNo != "") { qSql += string.Format(" and IQCNO like '{0}%' ", IQCNo); }
            if (ASNNo != "") { qSql += string.Format(" and STNO like '{0}%' ", ASNNo); }
            if (statusWaitCheck) { qSql += string.Format(" and UPPER(STATUS) = '{0}' ", "WAITCHECK"); }
            else { qSql += string.Format(" and UPPER(STATUS) = '{0}' ", "CLOSE"); }
            if (ROHS) { qSql += string.Format(" and UPPER(ROHS) = '{0}' ", "TRUE"); }
            else { qSql += string.Format(" and UPPER(ROHS) = '{0}' ", "FALSE"); }
            if (vendorCode != "") { qSql += string.Format(" and VENDORCODE like '{0}%' ", vendorCode); }
            if (appDateFrom != "0") { qSql += string.Format(" and appdate >= {0} ", appDateFrom); }
            if (appDateTo != "0") { qSql += string.Format(" and appdate <= {0} ", appDateTo); }


            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            qSql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.GetCount(new SQLCondition(qSql));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Resource
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="resourceCode">ResourceCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Resource数组</returns>
        public object[] QueryIQCHead(string IQCNo, string ASNNo, bool statusWaitCheck, bool statusClosed, bool ROHS, string vendorCode, string appDateFrom, string appDateTo, int inclusive, int exclusive)
        {
            string qSql = string.Format("select applicant, standard, lotno, appdate, inspector, proddate, status, rohs, invuser, pic, vendorname, result, method, iqcno, inspdate, receivedate, iqc.stno stno, apptime, vendorcode from tblasn asn, tblasniqc iqc where asn.stno=iqc.stno ");
            if (IQCNo != "") { qSql += string.Format(" and UPPER(IQCNO) like '{0}%' ", IQCNo); }
            if (ASNNo != "") { qSql += string.Format(" and UPPER(iqc.STNO) like '{0}%' ", ASNNo); }
            if (statusWaitCheck) { qSql += string.Format(" and UPPER(STATUS) = '{0}' ", "WAITCHECK"); }
            else { qSql += string.Format(" and UPPER(STATUS) = '{0}' ", "CLOSE"); }
            if (ROHS) { qSql += string.Format(" and UPPER(ROHS) = '{0}' ", "TRUE"); }
            else { qSql += string.Format(" and UPPER(ROHS) = '{0}' ", "FALSE"); }
            if (vendorCode != "") { qSql += string.Format(" and UPPER(VENDORCODE) like '{0}%' ", vendorCode); }
            if (appDateFrom != "0") { qSql += string.Format(" and appdate >= {0} ", appDateFrom); }
            if (appDateTo != "0") { qSql += string.Format(" and appdate <= {0} ", appDateTo); }

            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            qSql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(IQCHead), new PagerCondition(qSql, "IQCNO", inclusive, exclusive));
        }

        public bool IsIQCHeadExist(string IQCNo)
        {
            string sql = "";
            sql = "SELECT COUNT(*) FROM tblasniqc WHERE iqcno='" + IQCNo + "'";
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

        public bool IsASNExist(string ASNNO)
        {
            string sql = "";
            sql = "SELECT COUNT(*) FROM tblasn WHERE stno='" + ASNNO + "'";
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

        public void AddIQCHead(IQCHead iQCHead, string user)
        {
            this.DataProvider.BeginTransaction();
            string sql = "";
            sql += "INSERT INTO tblasniqc";
            sql += "            (iqcno, stno, invuser,";
            sql += "             status, applicant, appdate, apptime, lotno, inspdate, proddate,";
            sql += "             STANDARD, method, RESULT, receivedate, pic, inspector, rohs,";
            sql += "             mdate, mtime, muser";
            sql += "            )";
            sql += "     VALUES ('" + iQCHead.IQCNo + "', '" + iQCHead.STNo + "', '" + iQCHead.InventoryUser + "',";
            sql += "             'WaitCheck', '" + iQCHead.Applicant + "', TO_NUMBER (TO_CHAR (SYSDATE, 'yyyyMMdd')), TO_NUMBER (TO_CHAR (SYSDATE, 'hh24mmss')), '" + iQCHead.LotNo + "', " + iQCHead.InspectDate + ", " + iQCHead.ProduceDate + ",";
            sql += "             '" + iQCHead.Standard + "', '" + iQCHead.Method + "', '" + iQCHead.Result + "', " + iQCHead.ReceiveDate + ", '" + iQCHead.PIC + "', '" + iQCHead.Inspector + "', '" + iQCHead.ROHS + "',";
            sql += "               TO_NUMBER (TO_CHAR (SYSDATE, 'yyyyMMdd')),";
            sql += "                   TO_NUMBER (TO_CHAR (SYSDATE, 'hh24mmss')), '" + user + "'";
            sql += "            )";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            sql = "";
            sql += "INSERT INTO tblasn";
            sql += "            (stno, orgid, ";
            sql += "             ststatus,";
            sql += "             mdate, mtime, muser, syncstatus";
            sql += "            )";
            sql += "     VALUES ('" + iQCHead.STNo + "', " + GlobalVariables.CurrentOrganizations.First().OrganizationID + ", ";
            sql += "             'WaitCheck',";
            sql += "             TO_NUMBER (TO_CHAR (SYSDATE, 'yyyyMMdd')), TO_NUMBER (TO_CHAR (SYSDATE, 'hh24mmss')), '" + user + "', 'Closed'";
            sql += "            )";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            this.DataProvider.CommitTransaction();
        }

        public void DeleteIQCHead(IQCHead[] iQCHead)
        {
            this.DataProvider.BeginTransaction();
            foreach (IQCHead head in iQCHead)
            {
                if (head.Status.ToUpper().Equals("CLOSE"))
                {
                    continue;
                }
                String IQCNo = head.IQCNo;

                string sql = "";
                sql = "";
                sql += "UPDATE tblasniqc";
                sql += "   SET status = 'Close'";
                sql += " WHERE iqcno = '" + IQCNo + "'";
                this.DataProvider.CustomExecute(new SQLCondition(sql));

                sql = "";
                sql += "UPDATE tbliqcdetail";
                sql += "   SET stdstatus = 'Cancel'";
                sql += " WHERE  iqcno = '" + IQCNo + "'";
                this.DataProvider.CustomExecute(new SQLCondition(sql));
            }
            this.DataProvider.CommitTransaction();
        }

        public void UpdateIQCHead(IQCHead iQCHead)
        {
            string sql = "";
            sql += string.Format("UPDATE tblasniqc SET applicant='{0}', ", iQCHead.Applicant);
            sql += string.Format(" inspector='{0}', ", iQCHead.Inspector);
            sql += string.Format(" lotno='{0}', ", iQCHead.LotNo);
            sql += string.Format(" inspdate='{0}', ", iQCHead.InspectDate);
            sql += string.Format(" proddate='{0}', ", iQCHead.ProduceDate);
            sql += string.Format(" standard='{0}', ", iQCHead.Standard);
            sql += string.Format(" method='{0}', ", iQCHead.Method);
            sql += string.Format(" result='{0}', ", iQCHead.Result);
            sql += string.Format(" receivedate='{0}', ", iQCHead.ReceiveDate);
            sql += string.Format(" pic='{0}' ", iQCHead.PIC);
            sql += string.Format(" WHERE iqcno='{0}'", iQCHead.IQCNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }

        public IQCHead CreateNewIQCHead()
        {
            return new IQCHead();
        }

        public object GetIQCHead(string IQCNo)
        {
            object[] list = this.DataProvider.CustomQuery(typeof(IQCHead), new SQLCondition(string.Format("select applicant, standard, lotno, appdate, inspector, proddate, status, rohs, invuser, pic, vendorname, result, method, iqcno, inspdate, receivedate, iqc.stno stno, apptime, vendorcode from  tblasn asn, tblasniqc iqc where asn.stno=iqc.stno and iqcno='{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), IQCNo)));
            if (list.Length == 0)
            {
                return null;
            }
            else
            {
                return list[0];
            }
        }

        public bool IsIQCAvaliableToClose(string IQCNo)
        {
            string sql = "";
            sql = "";
            sql += "SELECT COUNT(*)";
            sql += "  FROM tbliqcdetail";
            sql += " WHERE UPPER (checkstatus) = 'WAITCHECK'";
            sql += "   AND UPPER (stdstatus) <> 'CANCEL'";
            sql += "   AND iqcno = '" + IQCNo + "'";
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CloseIQC(string IQCNo)
        {
            this.DataProvider.BeginTransaction();
            string sql = "";
            sql = "";
            sql += "UPDATE tblasniqc";
            sql += "   SET status = 'Close'";
            sql += " WHERE iqcno = '" + IQCNo + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            sql = "";
            sql += "UPDATE tbliqcdetail";
            sql += "   SET stdstatus = 'Close'";
            sql += " WHERE UPPER (stdstatus) <> 'CANCEL' AND iqcno = '" + IQCNo + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            sql = "";
            sql += "SELECT COUNT(*)";
            sql += "  FROM tblasniqc";
            sql += " WHERE UPPER (status) <> 'CLOSE' AND stno = (SELECT stno";
            sql += "                                                 FROM tblasniqc";
            sql += "                                                WHERE iqcno = '" + IQCNo + "')";
            int count = this.DataProvider.GetCount(new SQLCondition(sql));

            if (count == 0)
            {
                sql = "";
                sql += "UPDATE tblasn";
                sql += "   SET ststatus = 'Close'";
                sql += " WHERE stno = (SELECT stno";
                sql += "                 FROM tblasniqc";
                sql += "                WHERE iqcno = '" + IQCNo + "')";
                this.DataProvider.CustomExecute(new SQLCondition(sql));
            }
            this.DataProvider.CommitTransaction();
        }

        public object[] QueryIQCDetail(string iqcNo, int inclusive, int exclusive)
        {
            string qSql = string.Format("select {0} from tbliqcdetail where iqcno= '{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(IQCDetail)), iqcNo);
            return this.DataProvider.CustomQuery(typeof(IQCDetail), new PagerCondition(qSql, "ORDERNO,ORDERLINE", inclusive, exclusive));
        }

        public int QueryIQCDetailCount(string iqcNo)
        {
            string sSql = string.Format("select count(*) from tbliqcdetail where iqcno= '{0}' ",  iqcNo);
            return this.DataProvider.GetCount(new SQLCondition(sSql));
        }

        public IQCDetail CreateNewIQCDetail()
        {
            IQCDetail detail = new IQCDetail();
            detail.CheckStatus = "WaitCheck";
            return detail;
        }

        public object GetIQCDetail(string iqcNo, string line)
        {
            return this.DataProvider.CustomSearch(typeof(IQCDetail), new object[] { iqcNo, line }); 
        }

        public bool IsIQCDetailExist(IQCDetail iQCDetail)
        {
            if (this.DataProvider.CustomSearch(typeof(IQCDetail), new object[] { iQCDetail.IQCNo, iQCDetail.STLine }) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddIQCDetail(IQCDetail iQCDetail, string user)
        {
            iQCDetail.PIC = user;
            iQCDetail.STDStatus = "WaitCheck";
            iQCDetail.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
            iQCDetail.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            this._helper.AddDomainObject(iQCDetail);
        }

        public void UpdateIQCDetail(IQCDetail iQCDetail)
        {

            iQCDetail.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
            iQCDetail.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            this._helper.UpdateDomainObject(iQCDetail);
        }

        public void DeleteIQCDetail(IQCDetail[] iQCDetail)
        {
            string sql;
            this.DataProvider.BeginTransaction();
            String IQCNo = iQCDetail[0].IQCNo;
            foreach (IQCDetail detail in iQCDetail)
            {
               
                String line = detail.STLine.ToString();
                
                //更新状态
                sql = "";
                sql += "UPDATE tbliqcdetail";
                sql += "   SET stdstatus = 'Cancel',checkstatus=''";
                sql += " WHERE  iqcno = '" + IQCNo + "' AND stline='"+line +"'";
                this.DataProvider.CustomExecute(new SQLCondition(sql));
            }

            //检查是不是都是Cancel的行
            sql = "";
            sql += "SELECT COUNT(*)";
            sql += "  FROM tbliqcdetail";
            sql += " WHERE UPPER (stdstatus) <> 'CANCEL' AND iqcno =  '" + IQCNo + "'";
            int count = this.DataProvider.GetCount(new SQLCondition(sql));

            if (count == 0)
            {
                //更新iqc单为Close状态
                sql = "";
                sql += "UPDATE tblasniqc";
                sql += "   SET status = 'Close'";
                sql += " WHERE iqcno = '" + IQCNo + "'";
                this.DataProvider.CustomExecute(new SQLCondition(sql));

                //检查是不是所有iqc单都是close
                sql = "";
                sql += "SELECT COUNT(*)";
                sql += "  FROM tblasniqc";
                sql += " WHERE UPPER (status) <> 'CLOSE' AND stno= '" + IQCNo + "'";
                count = this.DataProvider.GetCount(new SQLCondition(sql));

                if (count == 0)
                {
                    //更新asn为Close
                    sql = "";
                    sql += "UPDATE tblasn";
                    sql += "   SET ststatus = 'Close'";
                    sql += " WHERE stno = (SELECT stno";
                    sql += "                 FROM tblasniqc";
                    sql += "                WHERE iqcno = '" + IQCNo + "')";
                    this.DataProvider.CustomExecute(new SQLCondition(sql));
                }
            }
            this.DataProvider.CommitTransaction();
        }
    }
}
