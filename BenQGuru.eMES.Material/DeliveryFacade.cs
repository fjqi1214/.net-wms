using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Delivery;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Material
{
    public class DeliveryFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public DeliveryFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public DeliveryFacade()
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

        #region DN

        public DeliveryNote CreateNewDN()
        {
            return new DeliveryNote();
        }

        public void AddDeliveryNote(DeliveryNote dn)
        {
            this.DataProvider.Insert(dn);
        }

        public void UpdateDeliveryNote(DeliveryNote dn)
        {
            this.DataProvider.Update(dn);
        }

        public void DeleteDeliveryNote(DeliveryNote dn)
        {
            this.DataProvider.Delete(dn);
        }

        public void DeleteDNWithOutLine(DeliveryNote dn)
        {
            string sql = "DELETE FROM tbldn WHERE dnno = '" + dn.DNCode + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void DeleteDN4Receive(string dnNoList)
        {
            string sql = "DELETE FROM tbldn WHERE dnstatus = '" + DNStatus.StatusInit + "' AND dnno IN ('" + dnNoList + "')";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public object GetDeliveryNote(string dnNo, string dnLine)
        {
            return this.DataProvider.CustomSearch(typeof(DeliveryNote), new object[] { dnNo, dnLine });
        }

        //public object[] GetDeliveryNoteHeadByMES(string dnNo)
        //{
        //    string sql = "";
        //    sql = string.Format("SELECT {0} FROM tbldn WHERE dnline='0' AND dnno = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DeliveryNote)), dnNo);
        //    return this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));

        //}


        //public object[] GetDeliveryNoteDetailByMES(string dnNo)
        //{
        //    string sql = "";
        //    sql = string.Format("SELECT {0} FROM tbldn WHERE dnline<>'0' AND dnno = '{1}' ORDER BY dnline ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DeliveryNote)), dnNo);
        //    return this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));
        //}

        //public object[] GetActiveDeliveryNoteBySAP(string dnNo)
        //{
        //    string sql = "";
        //    sql = string.Format("SELECT {0} FROM tbldn WHERE dnno = '{1}' AND dnstatus <> '" + DNStatus.StatusClose + "' ORDER BY dnno, dnline ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DeliveryNote)), dnNo);
        //    return this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));
        //}

        public object[] GetActiveDeliveryNoteDetailList(string dnno, string dnFrom)
        {
            string sql = "";
            sql = sql + "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(DeliveryNote)) + " FROM tbldn ";
            sql = sql + " WHERE 1= 1 ";
            sql = sql + "   AND dnline > '0'";
            sql = sql + "   AND DNFROM = '" + dnFrom + "'";
            sql = sql + "   AND dnstatus <> '" + DNStatus.StatusClose + "'";


            if (dnno.Trim().Length != 0)
            {
                sql = sql + " AND dnno = '" + dnno + "' ";
            }

            sql = sql + " ORDER BY dnno, dnline ";

            return this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));
        }

        //public object[] GetDeliveryNoteListBySAP(string customerOrderNo,string dnno)
        //{
        //    string sql = "";
        //    sql = sql + "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(DeliveryNote)) + " FROM tbldn ";
        //    sql = sql + " WHERE 1=1 ";

        //    if (customerOrderNo.Trim().Length != 0)
        //    {
        //         sql = sql + " AND cusorderno = '" + customerOrderNo + "' ";
        //    }

        //    if (dnno.Trim().Length != 0)
        //    {
        //         sql = sql + " AND dnno = '" + dnno + "' ";
        //    }
        //    sql = sql + "  AND dnstatus <> '" + DNStatus.StatusClose + "'";
        //    sql = sql + " ORDER BY dnno, dnline ";

        //    return this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));
        //}

        public object[] GetActiveDeliveryNoteHeadList(string customerOrderNo, string dnno,string dnFrom)
        {
            string sql = "";
            sql = sql + "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(DeliveryNote)) + " FROM tbldn ";
            sql = sql + " WHERE 1= 1 ";

            if (dnFrom.Equals(DNFrom.MES))
            {
        		 sql = sql + " AND dnline= '0'";
            }

            sql = sql + "   AND DNFROM = '" + dnFrom + "'";

            if (dnFrom.Equals(DNFrom.ERP))
            {
                sql = sql + "   AND dnstatus <> '" + DNStatus.StatusClose + "'";
            }

            if (customerOrderNo.Trim().Length != 0)
            {
                sql = sql + " AND cusorderno = '" + customerOrderNo + "' ";
            }

            if (dnno.Trim().Length != 0)
            {
                sql = sql + " AND dnno = '" + dnno + "' ";
            }

            sql = sql + " ORDER BY dnno, dnline ";

            return this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));
        }

        public string GenerateSql(string businessCode, string DNNO, string DNStatus, string dept, int orgId, bool type)
        {
            string sql = "";

            if (type)
            {
                string newSql = "SELECT tbldn.businesscode, tblinvbusiness.businessdesc, tbldn.dnno, tbldn.relateddocument, "
                + "tbldn.dept, tbldn.orderno, tbldn.shiptoparty, tbldn.dnstatus, tbldn.memo "
                //+ "FROM tbldn, tblinvbusiness "
                //+ "WHERE tbldn.businesscode = tblinvbusiness.businesscode";
                + "FROM tbldn left join tblinvbusiness on tbldn.businesscode = tblinvbusiness.businesscode "
                + "WHERE 1=1 ";

                //sql = string.Format("{0} AND tbldn.dnline='0' AND tbldn.dnfrom='{1}' AND tbldn.orgid='{2}' AND tbldn.dnno LIKE '%{3}%'", newSql, DNFrom.MES, orgId, DNNO);
                sql = string.Format("{0} AND tbldn.dnline='0' AND tbldn.orgid='{1}' AND tbldn.dnno LIKE '%{2}%'", newSql, orgId, DNNO);//add by Jarvis
            }
            else
            {
                //sql = string.Format("SELECT COUNT(*) FROM tbldn, tblinvbusiness WHERE tbldn.businesscode = tblinvbusiness.businesscode "
                //+ "AND tbldn.dnline='0' AND tbldn.dnfrom='{0}' AND tbldn.orgid='{1}' AND tbldn.dnno LIKE '%{2}%'", DNFrom.MES, orgId, DNNO);
                sql = string.Format("SELECT COUNT(*) FROM tbldn left join tblinvbusiness on tbldn.businesscode = tblinvbusiness.businesscode WHERE 1=1 "
                + "AND tbldn.dnline='0' AND tbldn.orgid='{0}' AND tbldn.dnno LIKE '%{1}%'", orgId, DNNO);//add by Jarvis
            }
            
            if (businessCode != null && businessCode.Length != 0)
            {
                sql = string.Format("{0} AND tbldn.businesscode IN('{1}')", sql, businessCode);
            }

            if (DNStatus != null && DNStatus.Length != 0)
            {
                sql = string.Format("{0} AND tbldn.dnstatus IN('{1}')", sql, DNStatus);
            }

            if (dept != null && dept.Length != 0)
            {
                sql = string.Format("{0} AND tbldn.dept IN('{1}')", sql, dept);
            }

            return sql;
        }
        
        public object[] QueryDN(string businessCode, string DNNO, string DNStatus, string dept, int orgId, int inclusive, int exclusive)
        {

            string sql = GenerateSql(businessCode, DNNO, DNStatus, dept, orgId, true);

            return this.DataProvider.CustomQuery(typeof(DNWithBusinessDesc), new PagerCondition(sql, "tbldn.dnno", inclusive, exclusive));
        }

        public int QueryDNCount(string businessCode, string DNNO, string DNStatus, string dept, int orgId)
        {
            string sql = GenerateSql(businessCode, DNNO, DNStatus, dept, orgId, false);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public void DNClose(DeliveryNote[] deliveryNotes)
        {
            this._domainDataProvider.BeginTransaction();
            try
            {
                foreach (DeliveryNote deliveryNote in deliveryNotes)
                {
                    this.UpdateDeliveryNote(deliveryNote);
                }
                this._domainDataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this._domainDataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_DNClose", ex);
            }
        }

        public object[] QueryDNLine(string DNNO, int orgId, int inclusive, int exclusive)
        {
            string sql = string.Format("SELECT t1.itemcode AS ITEMCODE , t2.mdesc AS ITEMDESC, t1.frmstorage AS FRMSTORAGE, "
            + "t1.mocode AS MOCODE, t1.reworkmocode AS REWORKMOCODE, t1.dnquantity AS DNQUANTITY, t1.dnline AS DNLINE,t1.orderno AS ORDERNO  "
            + "FROM tbldn t1, tblmaterial t2 WHERE t1.itemcode = t2.mcode AND t1.dnline<>'0' AND t1.orgid='{0}' AND t1.dnno = '{1}'", orgId, DNNO);

            return this.DataProvider.CustomQuery(typeof(DeliveryNote), new PagerCondition(sql, "dnline", inclusive, exclusive));
        }

        public int QueryDNLineCount(string DNNO, int orgId)
        {
            string sql = string.Format("SELECT COUNT(*) FROM tbldn t1, tblmaterial t2 WHERE t1.itemcode = t2.mcode AND t1.dnline<>'0' AND t1.orgid='{0}' AND t1.dnno = '{1}'", orgId, DNNO);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetMaxDNLine(string DNNO)
        {
            string sql = string.Format("SELECT MAX(dnline) AS DNLINE FROM tbldn WHERE dnno = '{0}'", DNNO);

            return this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));
        }

        public object[] GetDNForClose(string DNNO)
        {
            string sql = string.Format("SELECT {0} FROM tbldn WHERE dnno = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DeliveryNote)), DNNO);

            return this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));
        }

        public object[] QueryDNNotConfirmed()
        {
            string sql = "SELECT {0} FROM tbldn WHERE dnfrom = 'SAP' AND flag = 'MES' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(DeliveryNote)));
            return this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));
        }

        public void UpdateDNFlag(string dnno, string flag)
        {
            string sql = "UPDATE tbldn SET flag = '" + flag.Trim().ToUpper() + "' WHERE dnno = '" + dnno.Trim().ToUpper() + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        //Add by sandy on 20130220
        public void UpdateDNHeadStatus(string dnNo, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            if (CheckDNStatusClosed(dnNo))
            {
                string sql = string.Empty;
                sql += "UPDATE tbldn a SET         a.dnstatus = '" + DNStatus.StatusClose + "',";
                sql += "                           a.muser = '" + userCode + "', ";
                sql += "                           a.mdate = " + dbDateTime.DBDate + ",";
                sql += "                           a.mtime = " + dbDateTime.DBTime;
                sql += " WHERE a.dnno = '" + dnNo + "' ";
                sql += "   AND a.dnline = '0' ";
                this.DataProvider.CustomExecute(new SQLCondition(sql));
            }
        }

        private bool CheckDNStatusClosed(string dnNo)
        {
            string sql = string.Empty;

            sql += "SELECT * FROM tbldn WHERE DNLINE<>'0' ";
            if (dnNo.Trim() != string.Empty)
            {
                sql += "   AND dnno = '" + dnNo + "'";
            }

            sql += " ORDER BY dnno,dnline";

            object[] DeliveryNoteObjects = this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));

            if (DeliveryNoteObjects != null && DeliveryNoteObjects.Length > 0)
            {
                for (int i = 0; i < DeliveryNoteObjects.Length; i++)
                {
                    if (((DeliveryNote)DeliveryNoteObjects[i]).DNStatus == DNStatus.StatusUsing
                        || ((DeliveryNote)DeliveryNoteObjects[i]).DNStatus == DNStatus.StatusInit)  
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        //End Add
        #endregion


        #region SN2SAP

        public DN2SAP CreateNewDN2SAP()
        {
            return new DN2SAP();
        }

        public void AddDN2SAP(DN2SAP dn2sap)
        {
            this.DataProvider.Insert(dn2sap);
        }

        public void UpdateDN2SAP(DN2SAP dn2sap)
        {
            this.DataProvider.Update(dn2sap);
        }

        public void DeleteDN2SAP(DN2SAP dn2sap)
        {
            this.DataProvider.Delete(dn2sap);
        }

        public void UpdateDN2SAPStatusByDNNo(string dnno)
        {
            string sql = "UPDATE tbldn2sap SET active='N' WHERE dnno='" + dnno + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        } 
        #endregion
    }
}
