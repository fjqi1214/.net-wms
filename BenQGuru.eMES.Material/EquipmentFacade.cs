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
using BenQGuru.eMES.Domain.Equipment;

namespace BenQGuru.eMES.Material
{
    public class EquipmentFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public EquipmentFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public EquipmentFacade()
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

        #region EquipmentType
        public object GetEquipmentType(string eqptype)
        {
            return this.DataProvider.CustomSearch(typeof(Domain.Equipment.EquipmentType), new object[] { eqptype });
        }

        public Domain.Equipment.EquipmentType CreateEquipmentType()
        {
            return new Domain.Equipment.EquipmentType();
        }

        public void AddEquipmentType(Domain.Equipment.EquipmentType equipmentType)
        {
            this._helper.AddDomainObject(equipmentType);
        }

        public void UpdateEquipmentType(Domain.Equipment.EquipmentType equipmentType)
        {
            this._helper.UpdateDomainObject(equipmentType);
        }

        public void DeleteEquipmentType(Domain.Equipment.EquipmentType equipmentType)
        {
            this._helper.DeleteDomainObject(equipmentType);
        }

        public void DeleteEquipmentType(Domain.Equipment.EquipmentType[] equipmentType)
        {
            this._helper.DeleteDomainObject(equipmentType);
        }

        public object[] GetAllEquipmentType()
        {
            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.EquipmentType), new SQLCondition(string.Format("select {0} from TBLEquipmentType where 1=1 order by EqpType asc ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.EquipmentType)))));
        }

        public object[] QueryEquipmenType(string equipmenType, string equipmenTypedesc, int inclusive, int exclusive)
        {
            string sql = " select  TBLEquipmentType.EQPType,TBLEquipmentType.EQPTypeDesc,TBLEquipmentType.MUSER,TBLEquipmentType.MDATE,TBLEquipmentType.MTIME ";
            sql += "    from TBLEquipmentType where 1=1 ";
            if (equipmenType.Trim().Length > 0)
            {
                sql = sql + "AND EQPType like '%" + equipmenType.ToUpper() + "%'  ";
            }

            if (equipmenTypedesc.Trim().Length > 0)
            {
                sql = sql + "AND UPPER(EQPTypeDESC) like '%" + equipmenTypedesc.ToUpper() + "%'  ";
            }

            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.EquipmentType),
                    new PagerCondition(sql, "EQPType", inclusive, exclusive));
            //return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)), equipmenId), "EQPID", inclusive, exclusive));
        }


        public int QueryEquipmenTypeCount(string equipmenType, string equipmenTypedesc)
        {
            string sql = " select Count(*) from TBLEquipmentType where 1=1 ";
            if (equipmenType.Trim().Length > 0)
            {
                sql = sql + "AND EQPType like '%" + equipmenType.ToUpper() + "%'  ";
            }

            if (equipmenTypedesc.Trim().Length > 0)
            {
                sql = sql + "AND UPPER(EQPTypeDESC) like '%" + equipmenTypedesc.ToUpper() + "%'  ";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
            //return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)), equipmenId), "EQPID", inclusive, exclusive));
        }
        #endregion

        #region EquipmentTsType
        public object GetEquipmentTsType(string eqptype)
        {
            return this.DataProvider.CustomSearch(typeof(Domain.Equipment.EquipmentTsType), new object[] { eqptype });
        }

        public Domain.Equipment.EquipmentTsType CreateEquipmentTsType()
        {
            return new Domain.Equipment.EquipmentTsType();
        }

        public void AddEquipmentTsType(Domain.Equipment.EquipmentTsType equipmentType)
        {
            this._helper.AddDomainObject(equipmentType);
        }

        public void UpdateEquipmentTsType(Domain.Equipment.EquipmentTsType equipmentType)
        {
            this._helper.UpdateDomainObject(equipmentType);
        }

        public void DeleteEquipmentTsType(Domain.Equipment.EquipmentTsType equipmentType)
        {
            this._helper.DeleteDomainObject(equipmentType);
        }

        public void DeleteEquipmentTsType(Domain.Equipment.EquipmentTsType[] equipmentType)
        {
            this._helper.DeleteDomainObject(equipmentType);
        }

        public object[] GetAllEquipmentTsType()
        {
            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.EquipmentTsType), new SQLCondition(string.Format("select {0} from TBLEquipmentTsType where 1=1 order by EqpTsType asc ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.EquipmentTsType)))));
        }

        public object[] QueryEquipmenTsType(string equipmenTsType, string equipmenTsTypedesc, int inclusive, int exclusive)
        {
            string sql = " select  TBLEquipmentTsType.EQPTsType,TBLEquipmentTsType.EQPTsTypeDesc,TBLEquipmentTsType.MUSER,TBLEquipmentTsType.MDATE,TBLEquipmentTsType.MTIME ";
            sql += "    from TBLEquipmentTsType where 1=1 ";
            if (equipmenTsType.Trim().Length > 0)
            {
                sql = sql + "AND EQPTsType like '%" + equipmenTsType.ToUpper() + "%'  ";
            }

            if (equipmenTsTypedesc.Trim().Length > 0)
            {
                sql = sql + "AND UPPER(EQPTsTypeDESC) like '%" + equipmenTsTypedesc.Trim().ToUpper() + "%'  ";
            }

            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.EquipmentTsType),
                    new PagerCondition(sql, "EQPTsType", inclusive, exclusive));
            //return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)), equipmenId), "EQPID", inclusive, exclusive));
        }


        public int QueryEquipmenTsTypeCount(string equipmenTsType, string equipmenTsTypedesc)
        {
            string sql = " select Count(*) from TBLEquipmentTsType where 1=1 ";
            if (equipmenTsType.Trim().Length > 0)
            {
                sql = sql + "AND EQPTsType like '%" + equipmenTsType.ToUpper() + "%'  ";
            }

            if (equipmenTsTypedesc.Trim().Length > 0)
            {
                sql = sql + "AND UPPER(EQPTsTypeDESC) like '%" + equipmenTsTypedesc.Trim().ToUpper() + "%'  ";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
            //return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)), equipmenId), "EQPID", inclusive, exclusive));
        }
        #endregion

        #region Equipment
        public object GetEquipment(string eqpId)
        {
            return this.DataProvider.CustomSearch(typeof(Domain.Equipment.Equipment), new object[] { eqpId });
        }

        public Domain.Equipment.Equipment CreateEquipment()
        {
            return new Domain.Equipment.Equipment();
        }

        public void AddEquipment(Domain.Equipment.Equipment equipment)
        {
            this._helper.AddDomainObject(equipment);
        }

        public void UpdateEquipment(Domain.Equipment.Equipment equipment)
        {
            this._helper.UpdateDomainObject(equipment);
        }

        public void DeleteEquipment(Domain.Equipment.Equipment equipment)
        {
            this._helper.DeleteDomainObject(equipment);
        }

        public void DeleteEquipment(Domain.Equipment.Equipment[] equipment)
        {
            this._helper.DeleteDomainObject(equipment);
        }

        public int QueryEquipmentCountByType(string type)
        {
            string sql = " select count(*)  from TBLEquipment where type='" + type + "' ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        public object[] GetAllEquipment()
        {
            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new SQLCondition(string.Format("select {0} from TBLEquipment where 1=1 order by EqpId asc ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)))));
        }

        public object[] QueryEquipmen(string equipmenId, int inclusive, int exclusive)
        {
            string sql = " select TBLEquipment.EQPID,TBLEquipment.EQPName,TBLEquipment.Model,TBLEquipment.Type,TBLEquipment.EQPType,TBLEquipment.EQPDESC ,TBLEquipment.MUSER,TBLEquipment.MDATE,TBLEquipment.MTIME  from TBLEquipment where 1=1 ";
            if (equipmenId.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(equipmenId));
            }

            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment),
                    new PagerCondition(sql, "EQPID", inclusive, exclusive));
            //return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)), equipmenId), "EQPID", inclusive, exclusive));
        }

        public object[] QueryEquipmen(string equipmenId, string equipmentdesc, string equipmentstatus, int inclusive, int exclusive)
        {
            string sql = " select  TBLEquipment.EQPID,TBLEquipment.Model,TBLEquipment.EQPName,TBLEquipment.Type,TBLEquipment.EQPDESC ,TBLEquipment.MUSER,TBLEquipment.MDATE,TBLEquipment.MTIME  ,";
            sql += " TBLEquipment.EQPTYPE,TBLEquipment.EQPCompany,TBLEquipment.Contact,TBLEquipment.TELPHONE, ";
            sql += " TBLEquipment.EATTRIBUTE1,TBLEquipment.EATTRIBUTE2,TBLEquipment.EATTRIBUTE3,TBLEquipment.EQPSTATUS ";
            sql += "    from TBLEquipment where 1=1 ";
            if (equipmenId.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(equipmenId));
            }

            if (equipmentdesc.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPDESC like '%" + equipmentdesc.Trim() + "%'  ");
            }

            if (equipmentstatus.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPSTATUS in ({0}) ", FormatHelper.ProcessQueryValues(equipmentstatus));
            }

            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment),
                    new PagerCondition(sql, "EQPID", inclusive, exclusive));
            //return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)), equipmenId), "EQPID", inclusive, exclusive));
        }

        public object[] QueryEquipmen(string equipmenId, string equipmentdesc, string equipmenttype, string equipmentstatus, int inclusive, int exclusive)
        {
            string sql = " select  TBLEquipment.EQPID,TBLEquipment.Model,TBLEquipment.EQPName,TBLEquipment.Type,TBLEquipment.EQPDESC ,TBLEquipment.MUSER,TBLEquipment.MDATE,TBLEquipment.MTIME  ,";
            sql += " TBLEquipment.EQPTYPE,TBLEquipment.EQPCompany,TBLEquipment.Contact,TBLEquipment.TELPHONE, ";
            sql += " TBLEquipment.EATTRIBUTE1,TBLEquipment.EATTRIBUTE2,TBLEquipment.EATTRIBUTE3,TBLEquipment.EQPSTATUS ";
            sql += "    from TBLEquipment where 1=1 ";
            if (equipmenId.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(equipmenId));
            }

            if (equipmentdesc.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPDESC like '%" + equipmentdesc.Trim() + "%'  ");
            }

            if (equipmenttype.Trim().Length > 0)
            {
                sql = sql + string.Format("AND TYPE in ({0}) ", FormatHelper.ProcessQueryValues(equipmenttype));
            }

            if (equipmentstatus.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPSTATUS in ({0}) ", FormatHelper.ProcessQueryValues(equipmentstatus));
            }

            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment),
                    new PagerCondition(sql, "EQPID", inclusive, exclusive));
            //return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)), equipmenId), "EQPID", inclusive, exclusive));
        }


        public int QueryEquipmenCount(string equipmenId)
        {
            string sql = " select Count(*) from TBLEquipment where 1=1 ";
            if (equipmenId.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(equipmenId));
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
            //return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)), equipmenId), "EQPID", inclusive, exclusive));
        }

        public int QueryEquipmenCount(string equipmenId, string equipmentdesc, string equipmentstatus)
        {
            string sql = " select Count(*) from TBLEquipment where 1=1 ";
            if (equipmenId.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(equipmenId));
            }
            if (equipmentdesc.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPTYPE like '%" + equipmentdesc.Trim() + "%'  ");
            }

            if (equipmentstatus.Trim().Length > 0)
            {
                sql = sql + string.Format("AND EQPSTATUS in ({0}) ", FormatHelper.ProcessQueryValues(equipmentstatus));
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
            //return this.DataProvider.CustomQuery(typeof(Domain.Equipment.Equipment), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Equipment.Equipment)), equipmenId), "EQPID", inclusive, exclusive));
        }
        #endregion

        #region EQPLOG
        /// <summary>
        /// TBLEQPLOG
        /// </summary>
        public EQPLog CreateNewEQPLOG()
        {
            return new EQPLog();
        }

        public void AddEQPLOG(EQPLog eqplog)
        {
            this.DataProvider.Insert(eqplog);
        }

        public void DeleteEQPLog(EQPLog eqplog)
        {
            this.DataProvider.Delete(eqplog);
        }

        public void DeleteEQPLog(EQPLog[] eqplog)
        {
            this._helper.DeleteDomainObject(eqplog);
        }

        public void UpdateEQPLog(EQPLog eqplog)
        {
            this.DataProvider.Update(eqplog);
        }

        public object GetEQPLog(int SERIAL)
        {
            return this.DataProvider.CustomSearch(typeof(EQPLog), new object[] { SERIAL });
        }

        public object[] QueryEQPLog(string EQPID, int inclusive, int exclusive)
        {
            string sql = " select TBLEQPLog.EQPID,TBLEQPLog.EQPSTATUS,TBLEQPLog.MEMO,TBLEQPLog.MUSER,TBLEQPLog.MDATE,TBLEQPLog.MTIME  from TBLEQPLog where 1=1 ";
            sql = sql + string.Format("AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(EQPID));
            //sql += " order by mdate desc , mtime desc ";

            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.EQPLog),
                    new PagerCondition(sql, "mdate desc ,mtime desc ",
                        inclusive, exclusive));
        }

        public int QueryEQPLogCount(string EQPID)
        {
            string sql = " select Count(*) from TBLEQPLog where 1=1 ";
            sql = sql + string.Format("AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(EQPID));
            sql += " order by mdate desc , mtime desc ";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region EQPTSLOG
        /// <summary>
        /// TBLEQPLOG
        /// </summary>
        public EQPTSLog CreateNewEQPTSLOG()
        {
            return new EQPTSLog();
        }

        public void AddEQPTSLog(EQPTSLog eqptslog)
        {
            this.DataProvider.Insert(eqptslog);
        }

        public void DeleteEQPTSLog(EQPTSLog eqptslog)
        {
            this.DataProvider.Delete(eqptslog);
        }

        public void DeleteEQPTSLog(EQPTSLog[] eqptslog)
        {
            this._helper.DeleteDomainObject(eqptslog);
        }

        public void UpdateEQPTSLog(EQPTSLog eqptslog)
        {
            this.DataProvider.Update(eqptslog);
        }

        public object GetEQPTSLog(int SERIAL)
        {
            return this.DataProvider.CustomSearch(typeof(EQPTSLog), new object[] { SERIAL });
        }

        public object[] QueryEQPTSLog(string EQPID, int inclusive, int exclusive)
        {
            string sql = " select TBLEQPTSLog.Serial,TBLEQPTSLog.Duration, TBLEQPTSLog.EQPID,TBLEQPTSLog.TSType,TBLEQPTSLog.REASON,";
            sql += "TBLEQPTSLog.Solution,TBLEQPTSLog.Result,TBLEQPTSLog.MEMO,TBLEQPTSLog.MUSER,TBLEQPTSLog.MDATE,TBLEQPTSLog.MTIME,";
            sql += "TBLEQPTSLog.FINDUSER,TBLEQPTSLog.FINDMDATE,TBLEQPTSLog.FINDMTIME,TBLEQPTSLog.TSINFO,TBLEQPTSLog.STATUS from TBLEQPTSLog where 1=1 ";
            sql += string.Format("AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(EQPID));
            sql += " order by mdate desc , mtime desc ";

            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.EQPTSLog),
                    new PagerCondition(sql,
                        inclusive, exclusive));

        }

        public int QueryEQPTSLogCount(string EQPID)
        {
            string sql = " select Count(*) from TBLEQPTSLog where 1=1 ";
            sql = sql + string.Format("AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(EQPID));
            sql += " order by mdate desc , mtime desc ";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int CheckEQPTSLogExists(string EQPID, string STATUS)
        {
            string sql = "select Count(*) from TBLEQPTSLog where status = '" + STATUS + "' and EQPID = '" + EQPID + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetEQPTSLog(string EQPTSType, string STATUS)
        {
            string sql = "select Count(*) from TBLEQPTSLog where TSType = '" + EQPTSType + "' and STATUS = '" + STATUS + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region EQPOEE
        /// <summary>
        /// TBLEQPOEE
        /// </summary>
        public EQPOEE CreateNewEQPOEE()
        {
            return new EQPOEE();
        }

        public void AddEQPOEE(EQPOEE Eqpoee)
        {
            this.DataProvider.Insert(Eqpoee);
        }

        public void DeleteEQPOEE(EQPOEE Eqpoee)
        {
            this.DataProvider.Delete(Eqpoee);
        }

        public void DeleteEQPOEE(EQPOEE[] Eqpoee)
        {
            this._helper.DeleteDomainObject(Eqpoee);
        }

        public void UpdateEQPOEE(EQPOEE Eqpoee)
        {
            this.DataProvider.Update(Eqpoee);
        }

        public object GetEQPOEE(string eqpid)
        {
            return this.DataProvider.CustomSearch(typeof(EQPOEE), new object[] { eqpid });
        }

        public object[] QueryEQPOEE(string EQPID, int inclusive, int exclusive)
        {
            //string sql = " select a.EQPID , a.MUSER, a.MDATE,a.MTIME, a.WORKTIME,  a.sscode || '-' || b.ssdesc as sscode  , a.opcode || '-' || c.opdesc as opcode , d.eqpdesc from TBLEQPOEE a ";
            string sql = " select a.EQPID , a.MUSER, a.MDATE,a.MTIME, a.WORKTIME,  a.sscode || '-' || b.ssdesc as sscode  , a.rescode || '-' || c.resdesc as rescode , d.eqpdesc from TBLEQPOEE a ";//Jarvis
            sql += " left join tblss b on a.sscode = b.sscode ";
            //sql += " left join tblop c on a.opcode = c.opcode ";
            sql += " left join tblres c on a.rescode = c.rescode ";//Jarvis
            sql += " left join tblequipment d on a.eqpid = d.eqpid  ";
            sql += " where 1 = 1 ";

            if (EQPID.Trim().Length > 0)
            {
                sql = sql + string.Format("AND a.EQPID in ({0}) ", FormatHelper.ProcessQueryValues(EQPID));
            }

            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.EQPOEEForQuery),
                    new PagerCondition(sql,
                        inclusive, exclusive));
        }

        public int QueryEQPOEECount(string EQPID)
        {
            string sql = " select Count(*) from TBLEQPOEE a ";
            sql += " left join tblss b on a.sscode = b.sscode ";
            //sql += " left join tblop c on a.opcode = c.opcode ";
            sql += " left join tblres c on a.rescode = c.rescode ";//Jarvis
            sql += " left join tblequipment d on a.eqpid = d.eqpid ";
            sql += " where 1 = 1 ";
            if (EQPID.Trim().Length > 0)
            {
                sql = sql + string.Format("AND a.EQPID in ({0}) ", FormatHelper.ProcessQueryValues(EQPID));
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region EQPUseInfo
        /// <summary>
        /// TBLEQPOEE
        /// </summary>
        public EQPUseInfo CreateNewEQPUseInfo()
        {
            return new EQPUseInfo();
        }

        public void AddEQPUseInfo(EQPUseInfo eqpuseinfo)
        {
            this.DataProvider.Insert(eqpuseinfo);
        }

        public void DeleteEQPUseInfo(EQPUseInfo eqpuseinfo)
        {
            this.DataProvider.Delete(eqpuseinfo);
        }

        public void DeleteEQPUseInfo(EQPUseInfo[] eqpuseinfo)
        {
            this._helper.DeleteDomainObject(eqpuseinfo);
        }

        public void UpdateEQPUseInfo(EQPUseInfo eqpuseinfo)
        {
            this.DataProvider.Update(eqpuseinfo);
        }

        public object GetEQPUseInfo(string eqpid, int usedate)
        {
            return this.DataProvider.CustomSearch(typeof(EQPUseInfo), new object[] { eqpid, usedate });
        }

        public object[] QueryEQPUseInfo(string EQPID, int useDateBegin, int useDateEnd, int inclusive, int exclusive)
        {
            string sql = " select useinfo.*, a.eqpdesc, b.worktime  from  TBLEQPUSEINFO useinfo  ";
            sql += " left join tblequipment a on useinfo.eqpid = a.eqpid  ";
            sql += " left join TBLEQPOEE b on useinfo.eqpid = b.eqpid  where 1=1 ";
            if (EQPID.Trim().Length > 0)
            {
                sql = sql + string.Format("AND useinfo.eqpid in ({0}) ", FormatHelper.ProcessQueryValues(EQPID));
            }

            if (useDateBegin.ToString().Trim().Length > 0 && useDateBegin.ToString().Trim() != "0")
            {
                sql = sql + " AND useinfo.usedate >= " + useDateBegin;
            }
            if (useDateEnd.ToString().Trim().Length > 0 && useDateEnd.ToString().Trim() != "0")
            {
                sql = sql + " AND useinfo.usedate <= " + useDateEnd;
            }

            return this.DataProvider.CustomQuery(typeof(Domain.Equipment.EQPUseInfoForQuery),
                    new PagerCondition(sql, "useinfo.eqpid asc , useinfo.usedate desc",
                        inclusive, exclusive));
        }

        public int QueryEQPUseInfoCount(string EQPID, int useDateBegin, int useDateEnd)
        {
            string sql = " select Count(*) from TBLEQPUSEINFO useinfo ";
            sql += " left join tblequipment a on useinfo.eqpid = a.eqpid  ";
            sql += " left join TBLEQPOEE b on useinfo.eqpid = b.eqpid  where 1=1 ";
            if (EQPID.Trim().Length > 0)
            {
                sql = sql + string.Format("AND useinfo.eqpid in ({0}) ", FormatHelper.ProcessQueryValues(EQPID));
            }

            if (useDateBegin.ToString().Trim().Length > 0 && useDateBegin.ToString().Trim() != "0")
            {
                sql = sql + " AND useinfo.usedate >= " + useDateBegin;
            }
            if (useDateEnd.ToString().Trim().Length > 0 && useDateEnd.ToString().Trim() != "0")
            {
                sql = sql + " AND useinfo.usedate <= " + useDateEnd;
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region EQPMaintenance
        /// <summary>
        /// EQPMaintenance
        /// </summary>
        public EQPMaintenance CreateNewEQPMaintenance()
        {
            return new EQPMaintenance();
        }

        public void AddEQPMaintenance(EQPMaintenance eqpMaintenance)
        {
            this.DataProvider.Insert(eqpMaintenance);
        }

        public void DeleteEQPMaintenance(EQPMaintenance eqpMaintenance)
        {
            this.DataProvider.Delete(eqpMaintenance);
        }

        public void DeleteEQPMaintenance(EQPMaintenance[] eqpMaintenance)
        {
            this._helper.DeleteDomainObject(eqpMaintenance);
        }

        public void UpdateEQPMaintenance(EQPMaintenance eqpMaintenance)
        {
            this.DataProvider.Update(eqpMaintenance);
        }

        public object GetEQPMaintenance(string EQPID, string MaintainITEM, string MaintainType)
        {
            return this.DataProvider.CustomSearch(typeof(EQPMaintenance), new object[] { EQPID, MaintainITEM, MaintainType });
        }

        public object[] QueryEQPMaintenance(string eqpid, string cycle, string type, int inclusive, int exclusive)
        {
            string sql = " select a.eqpid, b.eqpdesc, a.cycletype, a.frequency, a.maintainitem, a.maintaintype,a.muser,a.mdate,a.mtime from TBLEQPMaintenance a ";
            sql += "left join TBLEquipment b on a.eqpid = b.eqpid where 1=1 ";

            if (eqpid.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.EQPID in ({0}) ", FormatHelper.ProcessQueryValues(eqpid));
            }
            if (cycle.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.cycletype = '{0}'", cycle.Trim());
            }
            if (type.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.MaintainType = '{0}'", type.Trim());
            }

            return this.DataProvider.CustomQuery(typeof(EQPMaintenanceQuery), new PagerCondition
                (sql, "a.Eqpid", inclusive, exclusive));
        }

        public int QueryEQPMaintenanceCount(string eqpid, string cycle, string type)
        {
            string sql = " select count(*) from TBLEQPMaintenance where 1=1";

            if (eqpid.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND EQPID in ({0}) ", FormatHelper.ProcessQueryValues(eqpid));
            }
            if (cycle.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND cycletype = '{0}'", cycle.Trim());
            }
            if (type.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND MaintainType = '{0}'", type.Trim());
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryEQPMaintenanceForAdd(string eqpid, string cycle, string type, int inclusive, int exclusive)
        {
            string sql = " select a.eqpid, b.eqpdesc, a.cycletype, a.frequency, a.maintainitem,a.maintaintype ,nvl(max(c.mdate),0) as lastMaintenanceDate from TBLEQPMaintenance a ";
            sql += "left join TBLEquipment b on a.eqpid = b.eqpid ";
            sql += "left join TBLEQPMaintainLog c on a.eqpid = c.eqpid and a.maintainitem = c.maintainitem where 1=1 ";

            if (eqpid.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.EQPID in ({0}) ", FormatHelper.ProcessQueryValues(eqpid));
            }
            if (cycle.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.cycletype = '{0}'", cycle.Trim());
            }
            if (type.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.maintaintype = '{0}'", type.Trim());
            }
            sql += " group by a.eqpid,b.eqpdesc,a.cycletype,a.frequency,a.maintainitem ,a.maintaintype ";

            return this.DataProvider.CustomQuery(typeof(EQPMaintenanceQuery), new PagerCondition
                (sql, "a.Eqpid", inclusive, exclusive));
        }

        public object[] QueryEQPMaintenanceAutoRemind(string eqpid, string maintainITEM, string maintainType)
        {
            string sqlStr = @"SELECT EQPID,
                               EQPNAME,
                               MAINTAINTYPE,
                               MAINTAINITEM,
                               CYCLETYPE,
                               FREQUENCY,
                               MDATE, --保养计划维护日期
                               PLANDURATION,
                               LASTMAINTENANCEDATE,
                               ACTDURATION,
                               CASE MAINTAINTYPE
                                 WHEN 'USINGTYPE' THEN
                                  PLANDURATION - ACTDURATION
                                 WHEN 'DAYTYPE' THEN
                                  PLANDURATION - TRUNC(SYSDATE - TO_DATE(TO_CHAR(DECODE(LASTMAINTENANCEDATE,
                                                                                        0,
                                                                                        MDATE,
                                                                                        LASTMAINTENANCEDATE)),
                                                                         'yyyy-mm-dd'),
                                                       1)
                               END LASTTIME

                          FROM (SELECT M.EQPID,
                                       M.EQPNAME,
                                       M.MAINTAINTYPE,
                                       M.MAINTAINITEM,
                                       M.CYCLETYPE,
                                       M.FREQUENCY,
                                       M.MDATE,
                                       M.PLANDURATION,
                                       M.LASTMAINTENANCEDATE,
                                       NVL(SUM(N.ACTDURATION), 0) AS ACTDURATION
                                  FROM (SELECT A.EQPID,
                                               B.EQPNAME,
                                               A.MAINTAINTYPE,
                                               A.CYCLETYPE,
                                               A.FREQUENCY,
                                               A.MDATE,
                                               CASE A.CYCLETYPE
                                                 WHEN 'Y' THEN
                                                  365
                                                 WHEN 'M' THEN
                                                  30
                                                 WHEN 'W' THEN
                                                  7
                                                 WHEN 'D' THEN
                                                  1
                                               END * A.FREQUENCY AS PLANDURATION,
                                               A.MAINTAINITEM,
                                               NVL(MAX(C.MDATE), 0) AS LASTMAINTENANCEDATE
                                          FROM TBLEQPMAINTENANCE A
                                          LEFT JOIN TBLEQUIPMENT B ON A.EQPID = B.EQPID
                                          LEFT JOIN TBLEQPMAINTAINLOG C ON A.EQPID = C.EQPID
                                                                       AND A.MAINTAINTYPE =
                                                                           C.MAINTAINTYPE 
                                                                       AND A.MAINTAINITEM =
                                                                           C.MAINTAINITEM WHERE 1 = 1 ";
            if (eqpid.Trim().Length > 0)
            {
                sqlStr += " AND A.EQPID = '" + eqpid.Trim() + "'";
            }
            if (maintainType.Trim().Length > 0)
            {
                sqlStr += " AND A.MAINTAINTYPE = '" + maintainType.Trim() + "'";
            }
            if (maintainITEM.Trim().Length > 0)
            {
                sqlStr += " AND A.MAINTAINITEM like '%" + maintainITEM.Trim() + "%'";
            }

            sqlStr += @"  GROUP BY A.EQPID,
                                                  B.EQPNAME,
                                                  A.CYCLETYPE,
                                                  A.FREQUENCY,
                                                  A.MDATE,
                                                  A.MAINTAINTYPE,
                                                  A.MAINTAINITEM) M
                                  LEFT JOIN (SELECT A.EQPID,
                                                   A.USEDATE,
                                                   TRUNC(DECODE((A.RUNURATION - A.STOPDURATION -
                                                                (NVL(C.TSDURATION, 0))),
                                                                0,
                                                                0,
                                                                (A.RUNURATION - A.STOPDURATION -
                                                                (NVL(C.TSDURATION, 0))) / (60 * 24)),
                                                         1) ACTDURATION
                                              FROM TBLEQPUSEINFO A
                                              LEFT JOIN (SELECT EQPID,
                                                               MDATE,
                                                               SUM(DURATION) TSDURATION
                                                          FROM TBLEQPTSLOG
                                                         GROUP BY EQPID, MDATE) C ON A.EQPID =
                                                                                     C.EQPID
                                                                                 AND A.USEDATE =
                                                                                     C.MDATE) N ON M.EQPID =
                                                                                                   N.EQPID
                                                                                               AND M.LASTMAINTENANCEDATE <=
                                                                                                   N.USEDATE
                                 GROUP BY M.EQPID,
                                          M.EQPNAME,
                                          M.MAINTAINTYPE,
                                          M.MAINTAINITEM,
                                          M.CYCLETYPE,
                                          M.FREQUENCY,
                                          M.MDATE,
                                          M.PLANDURATION,
                                          M.LASTMAINTENANCEDATE  ORDER BY M.EQPID,M.MAINTAINTYPE,M.MAINTAINITEM) ORDER BY LASTTIME ";


            return this.DataProvider.CustomQuery(typeof(EQPMaintenanceForQuery), new SQLCondition
               (sqlStr));


        }

        public object[] QueryEQPMaintenanceEffective(string eqpid, string sscode, string rescode)
        {
            //            string sqlStr = @"SELECT E.EQPID AS EQPID,
            //                                       TBLEQUIPMENT.Eqpname,
            //                                       (E.SSCODE || '-' || TBLSS.SSDESC) AS SSCODE,
            //                                       (E.OPCODE ||  '-' || TBLOP.OPDESC) AS OPCODE,
            //                                       E.USEDATE AS USEDATE,
            //                                       E.ACTUSEDRATE AS ACTUSEDRATE,
            //                                       E.BXRATE AS BXRATE,
            //                                       E.GOODRATE AS GOODRATE,
            //                                       E.OEE AS OEE
            //                                       from 
            //                                (
            //                                   SELECT KY.EQPID,
            //                                   KY.SSCODE,
            //                                   KY.OPCODE,
            //                                   KY.USEDATE,
            //                                   KY.ACTUSEDRATE,
            //                                   BX.BXRATE,
            //                                   ZL.GOODRATE,
            //                                   KY.ACTUSEDRATE * BX.BXRATE * ZL.GOODRATE AS OEE
            //                              FROM (SELECT A.EQPID,
            //                                           A.USEDATE,
            //                                           A.RUNURATION,
            //                                           B.WORKTIME,
            //                                           B.SSCODE,
            //                                           B.OPCODE,
            //                                           NVL(C.TSDURATION, 0) TSDURATION,
            //                                           TRUNC((A.RUNURATION - A.STOPDURATION - (NVL(C.TSDURATION, 0))) /
            //                                                 B.WORKTIME,
            //                                                 2) ACTUSEDRATE
            //                                      FROM TBLEQPOEE B, TBLEQPUSEINFO A
            //                                      LEFT JOIN (SELECT EQPID, MDATE, SUM(DURATION) TSDURATION
            //                                                  FROM TBLEQPTSLOG
            //                                                 GROUP BY EQPID, MDATE) C ON A.EQPID = C.EQPID
            //                                                                         AND A.USEDATE = C.MDATE
            //                                     WHERE A.EQPID = B.EQPID) KY,
            //                                   (SELECT SHIFTDAY,
            //                                           SSCODE,
            //                                           OPCODE,
            //                                           SUM(OPCOUNT) OPCOUNT,
            //                                           SUM(PLANCNT) PLANCNT,
            //                                           DECODE(SUM(PLANCNT),
            //                                                  0,
            //                                                  0,
            //                                                  TRUNC(SUM(OPCOUNT) / SUM(PLANCNT), 2)) BXRATE
            //                                      FROM (SELECT F.SHIFTDAY,
            //                                                   F.ITEMCODE,
            //                                                   F.SSCODE,
            //                                                   F.OPCODE,
            //                                                   F.OPCOUNT,
            //                                                   G.PLANCNT
            //                                              FROM (SELECT B.SHIFTDAY,
            //                                                           B.ITEMCODE,
            //                                                           C.SSCODE,
            //                                                           C.OPCODE,
            //                                                           SUM(B.OPCOUNT) OPCOUNT
            //                                                      FROM TBLRPTSOQTY B, TBLMESENTITYLIST C
            //                                                     WHERE B.TBLMESENTITYLIST_SERIAL = C.SERIAL
            //                                                     GROUP BY B.ITEMCODE, C.SSCODE, B.SHIFTDAY, OPCODE) F
            //                                              LEFT JOIN (SELECT C.*,
            //                                                               D.CYCLETIME,
            //                                                               TRUNC(C.ACTDURATION / D.CYCLETIME, 0) PLANCNT
            //                                                          FROM (SELECT ITEMCODE,
            //                                                                       SSCODE,
            //                                                                       SHIFTDATE,
            //                                                                       SUM(DURATION) ACTDURATION
            //                                                                  FROM TBLPRODDETAIL A, TBLMO B
            //                                                                 WHERE A.MOCODE = B.MOCODE
            //                                                                 GROUP BY SSCODE, SHIFTDATE, ITEMCODE) C
            //                                                          LEFT JOIN TBLPLANWORKTIME D ON C.ITEMCODE =
            //                                                                                         D.ITEMCODE
            //                                                                                     AND C.SSCODE =
            //                                                                                         D.SSCODE) G ON F.SHIFTDAY =
            //                                                                                                        G.SHIFTDATE
            //                                                                                                    AND F.ITEMCODE =
            //                                                                                                        G.ITEMCODE
            //                                                                                                    AND F.SSCODE =
            //                                                                                                        G.SSCODE)
            //                                     GROUP BY SHIFTDAY, SSCODE, OPCODE) BX,
            //                                   (SELECT A.SHIFTDAY,
            //                                           B.SSCODE,
            //                                           B.OPCODE,
            //                                           SUM(A.Inputtimes) OPCOUNT,
            //                                           SUM(A.OUTPUTTIMES) GOODCOUNT,
            //                                           TRUNC(SUM(A.OUTPUTTIMES) / SUM(A.Inputtimes), 2) GOODRATE
            //                                      FROM TBLRPTOPQTY A, TBLMESENTITYLIST B
            //                                     WHERE A.TBLMESENTITYLIST_SERIAL = B.SERIAL
            //                                     GROUP BY A.SHIFTDAY, B.SSCODE, B.OPCODE
            //                                    HAVING SUM(A.Inputtimes) <> 0
            //                                     ORDER BY SHIFTDAY DESC
            //                                    ) ZL
            //                             WHERE KY.SSCODE = BX.SSCODE
            //                               AND KY.SSCODE = ZL.SSCODE
            //                               AND KY.OPCODE = BX.OPCODE
            //                               AND KY.OPCODE = ZL.OPCODE
            //                               AND KY.USEDATE = ZL.SHIFTDAY
            //                               AND KY.USEDATE = BX.SHIFTDAY) E
            //                            LEFT JOIN TBLSS   ON TBLSS.SSCODE = E.SSCODE 
            //                            LEFT JOIN TBLOP   ON TBLOP.OPCODE = E.OPCODE
            //                            LEFT JOIN TBLEQUIPMENT ON TBLEQUIPMENT.EQPID = E.EQPID
            //                            WHERE 1=1 ";

            string sqlStr = @"SELECT E.EQPID AS EQPID,
       TBLEQUIPMENT.EQPNAME,
       (E.SSCODE || '-' || TBLSS.SSDESC) AS SSCODE,
       (E.RESCODE || '-' || TBLRES.RESDESC) AS RESCODE,
       E.USEDATE AS USEDATE,
       E.ACTUSEDRATE AS ACTUSEDRATE,
       E.BXRATE AS BXRATE,
       E.GOODRATE AS GOODRATE,
       E.OEE AS OEE
  FROM (SELECT KY.EQPID,
               KY.SSCODE,
               KY.RESCODE,
               KY.USEDATE,
               KY.ACTUSEDRATE,
               BX.BXRATE,
               ZL.GOODRATE,
               KY.ACTUSEDRATE * BX.BXRATE * ZL.GOODRATE AS OEE
          FROM (SELECT A.EQPID,
                       A.USEDATE,
                       A.RUNURATION,
                       B.WORKTIME,
                       B.SSCODE,
                       B.RESCODE,
                       NVL(C.TSDURATION, 0) TSDURATION,
                       TRUNC((A.RUNURATION - A.STOPDURATION -
                             (NVL(C.TSDURATION, 0))) / B.WORKTIME,
                             2) ACTUSEDRATE
                  FROM TBLEQPOEE B, TBLEQPUSEINFO A
                  LEFT JOIN (SELECT EQPID, MDATE, SUM(DURATION) TSDURATION
                              FROM TBLEQPTSLOG
                             GROUP BY EQPID, MDATE) C ON A.EQPID = C.EQPID
                                                     AND A.USEDATE = C.MDATE
                 WHERE A.EQPID = B.EQPID) KY,
               (SELECT SHIFTDAY,
                       SSCODE,
                       RESCODE,
                       SUM(OPCOUNT) OPCOUNT,
                       SUM(PLANCNT) PLANCNT,
                       DECODE(SUM(PLANCNT),
                              0,
                              0,
                              TRUNC(SUM(OPCOUNT) / SUM(PLANCNT), 2)) BXRATE
                  FROM (SELECT F.SHIFTDAY,
                               F.ITEMCODE,
                               F.SSCODE,
                               F.RESCODE,
                               F.OPCOUNT,
                               G.PLANCNT
                          FROM (SELECT B.SHIFTDAY,
                                       B.ITEMCODE,
                                       C.SSCODE,
                                       C.RESCODE,
                                       SUM(B.OPCOUNT) OPCOUNT
                                  FROM TBLRPTSOQTY B, TBLMESENTITYLIST C
                                 WHERE B.TBLMESENTITYLIST_SERIAL = C.SERIAL
                                 GROUP BY B.ITEMCODE,
                                          C.SSCODE,
                                          B.SHIFTDAY,
                                          C.RESCODE) F
                          LEFT JOIN (SELECT C.*,
                                           D.CYCLETIME,
                                           TRUNC(C.ACTDURATION / D.CYCLETIME,
                                                 0) PLANCNT
                                      FROM (SELECT ITEMCODE,
                                                   SSCODE,
                                                   SHIFTDATE,
                                                   SUM(DURATION) ACTDURATION
                                              FROM TBLPRODDETAIL A, TBLMO B
                                             WHERE A.MOCODE = B.MOCODE
                                             GROUP BY SSCODE,
                                                      SHIFTDATE,
                                                      ITEMCODE) C
                                      LEFT JOIN TBLPLANWORKTIME D ON C.ITEMCODE =
                                                                     D.ITEMCODE
                                                                 AND C.SSCODE =
                                                                     D.SSCODE) G ON F.SHIFTDAY =
                                                                                    G.SHIFTDATE
                                                                                AND F.ITEMCODE =
                                                                                    G.ITEMCODE
                                                                                AND F.SSCODE =
                                                                                    G.SSCODE)
                 GROUP BY SHIFTDAY, SSCODE, RESCODE) BX,
               (SELECT A.SHIFTDAY,
                       B.SSCODE,
                       B.RESCODE,
                       SUM(A.INPUTTIMES) OPCOUNT,
                       SUM(A.OUTPUTTIMES) GOODCOUNT,
                       TRUNC(SUM(A.OUTPUTTIMES) / SUM(A.INPUTTIMES), 2) GOODRATE
                  FROM TBLRPTOPQTY A, TBLMESENTITYLIST B
                 WHERE A.TBLMESENTITYLIST_SERIAL = B.SERIAL
                 GROUP BY A.SHIFTDAY, B.SSCODE, B.RESCODE
                HAVING SUM(A.INPUTTIMES) <> 0
                 ORDER BY SHIFTDAY DESC) ZL
         WHERE KY.SSCODE = BX.SSCODE
           AND KY.SSCODE = ZL.SSCODE
           AND KY.RESCODE = BX.RESCODE
           AND KY.RESCODE = ZL.RESCODE
           AND KY.USEDATE = ZL.SHIFTDAY
           AND KY.USEDATE = BX.SHIFTDAY) E
  LEFT JOIN TBLSS ON TBLSS.SSCODE = E.SSCODE
  LEFT JOIN TBLRES ON TBLRES.RESCODE = E.RESCODE
  LEFT JOIN TBLEQUIPMENT ON TBLEQUIPMENT.EQPID = E.EQPID
 WHERE 1 = 1 ";

            if (eqpid.Trim().Length > 0)
            {
                sqlStr += " AND E.EQPID = '" + eqpid.Trim() + "'";
            }
            //if (opcode.Trim().Length > 0)
            //{
            //    sqlStr += string.Format(" AND E.OPCODE in ({0}) ", FormatHelper.ProcessQueryValues(opcode.Trim()));
            //}
            if (rescode.Trim().Length > 0)
            {
                sqlStr += string.Format(" AND E.RESCODE in ({0}) ", FormatHelper.ProcessQueryValues(rescode.Trim()));
            }
            if (sscode.Trim().Length > 0)
            {
                sqlStr += " AND E.SSCODE = '" + sscode.Trim() + "'";
            }
            return this.DataProvider.CustomQuery(typeof(EQPMaintenanceForEffective), new SQLCondition(sqlStr));

        }


        #endregion

        #region EQPMaintainLog
        /// <summary>
        /// EQPMaintainLog
        /// </summary>
        public EQPMaintainLog CreateNewEQPMaintainLog()
        {
            return new EQPMaintainLog();
        }

        public void AddEQPMaintainLog(EQPMaintainLog eqpMaintainLog)
        {
            this.DataProvider.Insert(eqpMaintainLog);
        }

        public void DeleteEQPMaintainLog(EQPMaintainLog eqpMaintainLog)
        {
            this.DataProvider.Delete(eqpMaintainLog);
        }

        public void DeleteEQPMaintainLog(EQPMaintainLog[] eqpMaintainLog)
        {
            this._helper.DeleteDomainObject(eqpMaintainLog);
        }

        public void UpdateEQPMaintainLog(EQPMaintainLog eqpMaintainLog)
        {
            this.DataProvider.Update(eqpMaintainLog);
        }

        public object GetEQPMaintainLog(int SERIAL)
        {
            return this.DataProvider.CustomSearch(typeof(EQPMaintainLog), new object[] { SERIAL });
        }

        public object[] QueryEQPMaintainLog(string eqpid, int dateFrom, int dateTo, string result, string cycle, string type, int inclusive, int exclusive)
        {
            string sql = " select a.serial,a.eqpid,b.eqpdesc,a.maintainitem,a.maintaintype,a.result,a.mdate,a.muser,a.memo,c.cycletype from TBLEQPMaintainLog a ";
            sql += "left join TBLEquipment b on a.eqpid = b.eqpid ";
            sql += "left join TBLEQPMaintenance c on a.eqpid = c.eqpid and a.maintaintype = c.maintaintype and a.maintainitem = c.maintainitem where 1=1 ";//modified by Jarvis

            if (eqpid.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.EQPID in ({0}) ", FormatHelper.ProcessQueryValues(eqpid));
            }
            if (dateFrom > 0)
            {
                sql += " AND a.mdate >=" + dateFrom;
            }
            if (dateTo > 0)
            {
                sql += " AND a.mdate <=" + dateTo;
            }
            if (result.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.result = '{0}'", result);
            }
            if (cycle.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND c.cycletype = '{0}'", cycle.Trim());
            }
            if (type.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND c.maintaintype = '{0}'", type.Trim());
            }
            sql += " order by a.serial desc";

            return this.DataProvider.CustomQuery(typeof(EQPMaintainLogQuery), new PagerCondition
                (sql, inclusive, exclusive));
        }

        public int QueryEQPMaintainLogCount(string eqpid, int dateFrom, int dateTo, string result, string cycle, string type)
        {
            string sql = " select count(*) from TBLEQPMaintainLog a ";
            sql += "left join TBLEQPMaintenance c on a.eqpid = c.eqpid and a.maintainitem = c.maintainitem where 1=1 ";

            if (eqpid.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.EQPID in ({0}) ", FormatHelper.ProcessQueryValues(eqpid));
            }
            if (dateFrom > 0)
            {
                sql += " AND a.mdate >=" + dateFrom;
            }
            if (dateTo > 0)
            {
                sql += " AND a.mdate <=" + dateTo;
            }
            if (result.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND a.result = '{0}'", result);
            }
            if (cycle.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND c.cycletype = '{0}'", cycle.Trim());
            }
            if (type.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND c.maintaintype = '{0}'", type.Trim());
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

    }
}
