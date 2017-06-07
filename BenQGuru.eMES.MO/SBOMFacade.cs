#region System
using System;
using System.Runtime.Remoting;
using System.Collections;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;
#endregion

/// SBOMFacade 的摘要说明。
/// 文件名:
/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
/// 创建人:Crystal Chu
/// 创建日期:2005/03/22
/// 修改人:
/// 修改日期:
/// 描 述: 对SBOM设置的操作控制
/// 版 本:	
/// </summary>   
namespace BenQGuru.eMES.MOModel
{
    public class SBOMFacade : MarshalByRefObject
    {

        //private static readonly log4net.ILog _log = BenQGuru.eMES.Common.Log.GetLogger(typeof(MOFacade));
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public SBOMFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public SBOMFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        protected IDomainDataProvider DataProvider
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

        public SBOM CreateSBOM()
        {
            return new SBOM();
        }




        public void AddSBOM(SBOM sbom)
        {
            try
            {
                //Laws Lu,2006/11/13 uniform system collect date
                DBDateTime dbDateTime;

                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                sbom.MaintainDate = dbDateTime.DBDate;
                sbom.MaintainTime = dbDateTime.DBTime;
                this.DataProvider.Insert(sbom);
            }
            catch (Exception ex)
            {
                //_log.Error(ex.Message,ex);
                ExceptionManager.Raise(this.GetType(), "$Error_AddSBOM", ex);
                //					throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(SBOMFacade),"SBOMModel_ERROR_AddSBOM"),ex); 
            }

        }

        public void AddSBOMs(SBOM[] boms)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                //added by jessie lee for P4.4 ,2005/8/31
                //该料品在物料清单中已经存在,则删除
                /*分产品导入，所以不用再判断
                ArrayList aList = new ArrayList();
                for(int i=0;i<boms.Length;i++)
                {
                    bool repeat = false ;
                    for(int j = aList.Count ; j>0 ; j--)
                    {
                        if( (string.Compare(boms[i].ItemCode,aList[j-1].ToString(),true)) == 0  )
                        {
                            repeat = true ;
                            break ;
                        }
                    }
                    if(!repeat)
                    {
                        aList.Add( boms[i].ItemCode );
                    }
                }
				
                string[] itemCodes = new string[aList.Count];
                for(int i = 0 ; i <aList.Count ; i++)
                {
                    itemCodes[i] = aList[i].ToString();
                }
                */
                object[] objs = this.DataProvider.CustomQuery(typeof(SBOM), new SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(SBOM)) + " from tblsbom where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode = '{0}' ", (boms[0] as SBOM).ItemCode)));
                if (objs != null)
                {
                    //该料品在物料清单中已经存在,则删除
                    for (int i = 0; i < objs.Length; i++)
                    {
                        this.DataProvider.Delete(objs[i]);
                    }
                }

                for (int i = 0; i < boms.Length; i++)
                {
                    if (boms[i] != null)
                    {
                        boms[i].Sequence = i;
                        AddSBOM(boms[i]);
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                //_log.Error(ex.Message,ex);
                DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_AddSBOMs", ex);
                //				throw ex;
            }
        }

        public void AddSBOMsWithoutTransaction(SBOM[] boms)
        {
            for (int i = 0; i < boms.Length; i++)
            {
                if (boms[i] != null)
                {
                    boms[i].Sequence = i;
                    AddSBOM(boms[i]);
                }
            }
        }

        public void UpdateSBOM(SBOM sbom)
        {
            this._helper.UpdateDomainObject(sbom);
        }

        public void DeleteSBOM(SBOM sbom)
        {
            try
            {
                this.DataProvider.Delete(sbom);
            }
            catch (Exception ex)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_DeleteSBOM", ex);
            }

        }

        public void DeleteSBOM(SBOM[] boms)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                for (int i = 0; i < boms.Length; i++)
                {
                    if (boms[i] != null)
                    {
                        this.DeleteSBOM(boms[i]);
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_DeleteSBOM", ex);
            }
        }

        public void DeleteSBOM(string itemCode)
        {
            object[] objs = GetSBOM(itemCode, int.MinValue, int.MaxValue);
            if (objs != null)
            {
                //this.DataProvider.BeginTransaction();
                try
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        this.DataProvider.Delete(objs[i]);
                    }
                    //this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    //this.DataProvider.RollbackTransaction();
                    ExceptionManager.Raise(this.GetType(), "$Error_DeleteSBOM", String.Format("[$ItemCode='{0}']", itemCode), ex);
                }
            }

        }

        public void DeleteSBOM(string[] itemCodes)
        {
            if (itemCodes != null)
            {
                this.DataProvider.BeginTransaction();
                try
                {
                    for (int i = 0; i < itemCodes.Length; i++)
                    {
                        DeleteSBOM(itemCodes[i]);
                    }
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    ExceptionManager.Raise(this.GetType(), "$Error_DeleteSBOM", ex);
                }
            }
        }

        public void DeleteSBOMWithoutTransaction(string[] itemCodes)
        {
            if (itemCodes != null)
            {
                for (int i = 0; i < itemCodes.Length; i++)
                {
                    DeleteSBOM(itemCodes[i]);
                }
            }
        }



        public object[] GetSBOM(string itemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(SBOM),
                new PagerCondition(string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(SBOM)) + " from TBLSBOM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode ='{0}' order by SBITEMCODE", itemCode), inclusive, exclusive));
        }

        public object[] GetSBOM(string itemCode, string sbomVersion, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(SBOM)).ToLower().Replace("tblsbom.eattribute1", "tblmaterial.mdesc AS eattribute1") + " ";
            sql += "FROM tblsbom ";
            sql += "LEFT OUTER JOIN tblmaterial ";
            sql += "ON tblsbom.sbitemcode = tblmaterial.mcode ";
            sql += "AND tblsbom.orgid = tblmaterial.orgid ";
            sql += "WHERE 1 = 1 ";
            sql += "AND tblsbom.itemcode = '{0}' ";
            sql += "AND tblsbom.orgid = " + GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString() + " ";
            sql += "AND tblsbom.sbomver = '{1}' ";
            sql += "ORDER BY tblsbom.sbitemcode ";
            sql = string.Format(sql, itemCode, sbomVersion);

            return this.DataProvider.CustomQuery(typeof(SBOM), new PagerCondition(sql, inclusive, exclusive));
        }

        public int GetSBOMCounts(string itemCode)
        {
            return this.DataProvider.GetCount(
                new SQLCondition(string.Format("select count(*) from TBLSBOM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode ='{0}'", itemCode)));
        }

        public decimal GetSbomItemQtyWithMo(string sbomItemCode, string moCode)
        {
            string sql = " SELECT SUM(B.SBITEMQTY) as SBITEMQTY  FROM TBLMO A, TBLSBOM B WHERE A.ITEMCODE = B.ITEMCODE AND a.mobom=b.sbomver  AND a.Orgid=b.Orgid";

            if (sbomItemCode.Trim() != string.Empty)
            {
                sql += " AND b.sbitemcode='" + sbomItemCode.Trim().ToUpper() + "'";
            }

            if (moCode.Trim() != string.Empty)
            {
                sql += " AND mocode='" + moCode.Trim().ToUpper() + "'";
            }

            sql += " GROUP BY B.SBITEMCODE";

            object[] sbomObjects = this._domainDataProvider.CustomQuery(typeof(SBOM), new SQLCondition(sql));

            if (sbomObjects == null)
            {
                return 0;
            }

            return ((SBOM)sbomObjects[0]).SBOMItemQty;
        }

        public int GetSBOMCounts(string itemCode, string sbomVersion)
        {
            return this.DataProvider.GetCount(
                new SQLCondition(string.Format("select count(*) from TBLSBOM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode ='{0}' and sbomver = '{1}'", itemCode, sbomVersion)));
        }

        public decimal GetSBOMMaxSequence(string itemCode, string sBOMVersion)
        {
            object[] sBOM = this.DataProvider.CustomQuery(typeof(SBOM),
                new SQLCondition("SELECT MAX(seq) AS Seq FROM tblsbom WHERE itemcode='" + itemCode + "' and sbomver='" + sBOMVersion + "'"));

            SBOM bom = sBOM[0] as SBOM;
            if (bom == null || bom.Sequence == 0)
            {
                return 0;
            }
            else
            {
                return bom.Sequence + 1;
            }
        }

        /// <summary>
        /// 修改主键后的Function
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="sbItemCode"></param>
        /// <param name="sbomVer"></param>
        /// <param name="sbomItemProject"></param>
        /// <param name="sbomItemSeq"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public object GetSBOM(string itemCode, string sbItemCode, string sbomVer, string sbomItemProject, string sbomItemSeq, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(SBOM), new object[] { itemCode, sbItemCode, sbomVer, sbomItemProject, sbomItemSeq, orgID });
        }

        public object GetSBOM(string itemcode, string sbitemcode, string sbsitemcode, string sbitemqty)
        {
            return GetSBOM(itemcode, sbitemcode, sbsitemcode, sbitemqty, string.Empty, -1, string.Empty);
            //return this.DataProvider.CustomSearch(typeof(SBOM),new object[]{itemcode,sbitemcode,sbsitemcode,sbitemqty});
        }

        public object GetSBOM(string itemcode, string sbitemcode, string sbsitemcode, string sbitemqty, string sbitemeffdate, int orgID, string sBOMVersion)
        {
            string sql = "select {0} from tblsbom where 1=1 ";
            if (itemcode != null && itemcode != String.Empty)
            {
                sql += " AND ITEMCODE = '" + itemcode + "'";
            }
            if (sbitemcode != null && sbitemcode != String.Empty)
            {
                sql += " AND SBITEMCODE = '" + sbitemcode + "'";
            }
            if (sbsitemcode != null && sbsitemcode != String.Empty)
            {
                sql += " AND SBSITEMCODE = '" + sbsitemcode + "'";
            }
            if (sbitemqty != null && sbitemqty != String.Empty)
            {
                sql += " AND SBITEMQTY = " + sbitemqty;
            }
            if (sbitemeffdate != null && sbitemeffdate != String.Empty)
            {
                sql += " AND SBITEMEFFDATE = " + sbitemeffdate;
            }
            if (orgID > 0)
            {
                sql += " AND ORGID = " + orgID.ToString();
            }
            if (sBOMVersion != null && sBOMVersion != String.Empty)
            {
                sql += " AND SBOMVER = '" + sBOMVersion + "'";
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(SBOM), new SQLCondition(String.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(SBOM)))));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        public object[] GetUnSelectSBOMItems(string itemCode, string opBOMCode, string opBOMVersion, string routeCode, string OPID, int inclusive, int exclusive)
        {
            if ((itemCode == string.Empty) || (opBOMCode == string.Empty) || (opBOMVersion == string.Empty) || (routeCode == string.Empty) || (OPID == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(OPBOMFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"itemCode")));
            }
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(SBOM)) + " from tblsbom where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode='{0}' " +
                             " and sbitemcode not in(select obitemcode from tblopbomdetail " +
                             " where itemcode='{0}' and obcode='{1}' and opbomver ='{2}'  " +
                             GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                             " and opid='{3}' )  ";
            object[] objs = new object[4];
            objs[0] = itemCode;
            objs[1] = opBOMCode;
            objs[2] = opBOMVersion;
            objs[3] = OPID;
            return this.DataProvider.CustomQuery(typeof(SBOM), new PagerCondition(String.Format(selectSql, objs), "SBITEMCODE", inclusive, exclusive));
        }

        public object[] GetUnSelectSBOMItems(string itemCode, string opBOMCode, string opBOMVersion, string routeCode, string OPID, string SBOMItemCode, string SBOMItemName, string SBOMSourceItemCode, int inclusive, int exclusive)
        {
            if ((itemCode == string.Empty) || (opBOMCode == string.Empty) || (opBOMVersion == string.Empty) || (routeCode == string.Empty) || (OPID == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(SBOM)) + " from tblsbom where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode='{0}' " +
                " and sbitemcode not in(select obitemcode from tblopbomdetail " +
                " where itemcode='{0}' and obcode='{1}' and opbomver ='{2}'  " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                " and opid='{3}' )";
            object[] objs = new object[4];
            objs[0] = itemCode;
            objs[1] = opBOMCode;
            objs[2] = opBOMVersion;
            objs[3] = OPID;

            string sql = String.Format(selectSql, objs);
            if (SBOMItemCode != string.Empty)
            {
                sql += string.Format(" and  SBITEMCODE like '{0}%' ", SBOMItemCode);
            }
            if (SBOMItemName != string.Empty)
            {
                sql += string.Format(" and  SBITEMNAME like '{0}%' ", SBOMItemName);
            }
            if (SBOMSourceItemCode != string.Empty)
            {
                sql += string.Format(" and  SBSITEMCODE like '{0}%' ", SBOMSourceItemCode);
            }
            return this.DataProvider.CustomQuery(typeof(SBOM), new PagerCondition(sql, "SBITEMCODE", inclusive, exclusive));

        }

        public object[] GetUnSelectSBOMItems(string itemCode, string opBOMCode, string opBOMVersion, string routeCode, string OPID, string SBOMItemCode, string SBOMItemName, string SBOMSourceItemCode, int actiontype, int inclusive, int exclusive)
        {
            if ((itemCode == string.Empty) || (opBOMCode == string.Empty) || (opBOMVersion == string.Empty) || (routeCode == string.Empty) || (OPID == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(SBOM)) + " from tblsbom where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode='{0}' " +
                " and sbitemcode not in(select obitemcode from tblopbomdetail " +
                " where itemcode='{0}' and obcode='{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and opbomver ='{2}'  and opid='{3}' and Actiontype={4})  ";
            object[] objs = new object[5];
            objs[0] = itemCode;
            objs[1] = opBOMCode;
            objs[2] = opBOMVersion;
            objs[3] = OPID;
            objs[4] = actiontype;
            string sql = String.Format(selectSql, objs);
            if (SBOMItemCode != string.Empty)
            {
                sql += string.Format(" and  SBITEMCODE like '{0}%' ", SBOMItemCode);
            }
            if (SBOMItemName != string.Empty)
            {
                sql += string.Format(" and  SBITEMNAME like '{0}%' ", SBOMItemName);
            }
            if (SBOMSourceItemCode != string.Empty)
            {
                sql += string.Format(" and  SBSITEMCODE like '{0}%' ", SBOMSourceItemCode);
            }
            return this.DataProvider.CustomQuery(typeof(SBOM), new PagerCondition(sql, "SBITEMCODE", inclusive, exclusive));

        }

        public object[] GetAllSBOMItemsByItem(string itemCode, string opBOMCode, string opBOMVersion, string routeCode, string OPID, string SBOMItemCode, string SBOMItemName, string SBOMSourceItemCode, int inclusive, int exclusive)
        {
            if ((itemCode == string.Empty) || (opBOMCode == string.Empty) || (opBOMVersion == string.Empty) || (routeCode == string.Empty) || (OPID == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }

            string fieldSql = DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(SBOM));
            fieldSql = fieldSql.ToLower().Replace("tblsbom.eattribute1", "tblmaterial.mdesc as eattribute1");
            fieldSql = fieldSql.ToLower().Replace("tblsbom.sbitemcontype", "tblmaterial.mcontroltype as sbitemcontype");

            string selectSql = "select " + fieldSql + " from tblsbom left outer join tblmaterial ";
            selectSql += "on tblsbom.orgid = tblmaterial.orgid ";
            selectSql += "and tblsbom.sbsitemcode = tblmaterial.mcode ";
            selectSql += "where 1=1 and tblsbom.orgid = " + GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString() + " and tblsbom.itemcode='{0}' and tblsbom.sbomver = '{1}' ";

            object[] objs = new object[2];
            objs[0] = itemCode;
            objs[1] = opBOMVersion;

            string sql = String.Format(selectSql, objs);
            if (SBOMItemCode != string.Empty)
            {
                sql += string.Format(" and tblsbom.SBITEMCODE like '{0}%' ", SBOMItemCode);
            }
            if (SBOMItemName != string.Empty)
            {
                sql += string.Format(" and tblsbom.SBITEMNAME like '{0}%' ", SBOMItemName);
            }
            if (SBOMSourceItemCode != string.Empty)
            {
                sql += string.Format(" and tblsbom.SBSITEMCODE like '{0}%' ", SBOMSourceItemCode);
            }
            sql += "and tblmaterial.mcontroltype in ('item_control_keyparts','item_control_lot') ";

            return this.DataProvider.CustomQuery(typeof(SBOM), new PagerCondition(sql, "tblsbom.SBITEMCODE", inclusive, exclusive));

        }

        public int GetUnSelectSBOMItemsCounts(string itemCode, string opBOMCode, string opBOMVersion, string routeCode, string OPID)
        {
            if ((itemCode == string.Empty) || (opBOMCode == string.Empty) || (opBOMVersion == string.Empty) || (routeCode == string.Empty) || (OPID == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(OPBOMFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"itemCode")));
            }
            string selectSql = "select count(*) from tblsbom where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode='{0}' " +
                " and sbitemcode not in(select obitemcode from tblopbomdetail " +
                " where itemcode='{0}' and obcode='{1}' and opbomver ='{2}'  " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                " and opid='{3}' )";
            object[] objs = new object[4];
            objs[0] = itemCode;
            objs[1] = opBOMCode;
            objs[2] = opBOMVersion;
            objs[3] = OPID;
            return this.DataProvider.GetCount(new SQLCondition(String.Format(selectSql, objs)));
        }

        public int GetUnSelectSBOMItemsCounts(string itemCode, string opBOMCode, string opBOMVersion, string routeCode, string OPID, string SBOMItemCode, string SBOMItemName, string SBOMSourceItemCode)
        {
            if ((itemCode == string.Empty) || (opBOMCode == string.Empty) || (opBOMVersion == string.Empty) || (routeCode == string.Empty) || (OPID == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            string selectSql = "select count(*) from tblsbom where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode='{0}' " +
                " and sbitemcode not in(select obitemcode from tblopbomdetail " +
                " where itemcode='{0}' and obcode='{1}' and opbomver ='{2}'  " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                " and opid='{3}' )";
            object[] objs = new object[4];
            objs[0] = itemCode;
            objs[1] = opBOMCode;
            objs[2] = opBOMVersion;
            objs[3] = OPID;

            string sql = String.Format(selectSql, objs);
            if (SBOMItemCode != string.Empty)
            {
                sql += string.Format(" and  SBITEMCODE like '{0}%' ", SBOMItemCode);
            }
            if (SBOMItemName != string.Empty)
            {
                sql += string.Format(" and  SBITEMNAME like '{0}%' ", SBOMItemName);
            }
            if (SBOMSourceItemCode != string.Empty)
            {
                sql += string.Format(" and  SBSITEMCODE like '{0}%' ", SBOMSourceItemCode);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public int GetAllSBOMItemsByItemCounts(string itemCode, string opBOMCode, string opBOMVersion, string routeCode, string OPID, string SBOMItemCode, string SBOMItemName, string SBOMSourceItemCode)
        {
            if ((itemCode == string.Empty) || (opBOMCode == string.Empty) || (opBOMVersion == string.Empty) || (routeCode == string.Empty) || (OPID == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            string selectSql = "select count(*) from tblsbom left outer join tblmaterial ";
            selectSql += "on tblsbom.orgid = tblmaterial.orgid ";
            selectSql += "and tblsbom.sbsitemcode = tblmaterial.mcode ";
            selectSql += "where 1=1 and tblsbom.orgid = " + GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString() + " and tblsbom.itemcode='{0}' and tblsbom.sbomver = '{1}' ";


            object[] objs = new object[2];
            objs[0] = itemCode;
            objs[1] = opBOMVersion;

            string sql = String.Format(selectSql, objs);
            if (SBOMItemCode != string.Empty)
            {
                sql += string.Format(" and  tblsbom.SBITEMCODE like '{0}%' ", SBOMItemCode);
            }
            if (SBOMItemName != string.Empty)
            {
                sql += string.Format(" and  tblsbom.SBITEMNAME like '{0}%' ", SBOMItemName);
            }
            if (SBOMSourceItemCode != string.Empty)
            {
                sql += string.Format(" and  tblsbom.SBSITEMCODE like '{0}%' ", SBOMSourceItemCode);
            }
            sql += " and tblmaterial.mcontroltype in ('item_control_keyparts','item_control_lot') ";

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public object[] Query(string itemCode)
        {
            return this.DataProvider.CustomQuery(typeof(SBOM),
                new SQLCondition(string.Format("select * from TBLSBOM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ITEMCODE like '{0}%'", itemCode)));
        }



        /// <summary>
        /// 根据Itemcode来获得相对应的最新的OPBOM的版本
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetOPBOMLastVersion(string itemCode)
        {
            return string.Empty;
        }


        public object[] GetAllSBOMVersion(string itemCode, int orgID)
        {
            string sql;
            sql = "";
            sql += "SELECT DISTINCT sbomver";
            sql += "           FROM tblsbom";
            sql += "          WHERE itemcode = '" + itemCode + "' AND orgid=" + orgID;

            return this.DataProvider.CustomQuery(typeof(SBOM), new SQLCondition(sql));
        }

        public object[] QuerySBOMByMoCode(string moCode)
        {
            string sql = "SELECT DISTINCT tblsbom.* FROM tblsbom ,tblmo WHERE tblsbom.itemcode=tblmo.itemcode AND  tblsbom.sbomver=tblmo.mobom ";

            if (moCode.Trim() != string.Empty)
            {
                sql += " AND mocode='" + FormatHelper.PKCapitalFormat(FormatHelper.CleanString(moCode)) + "'";
            }

            return this.DataProvider.CustomQuery(typeof(SBOM), new SQLCondition(sql));
        }
    }

}