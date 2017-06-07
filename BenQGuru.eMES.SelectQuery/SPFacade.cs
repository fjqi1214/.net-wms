using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Domain.ReportView;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Domain.RMA;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.SelectQuery
{
    /// <summary>
    /// vizo 的摘要说明。
    /// </summary>
    public class SPFacade
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

        public SPFacade(IDomainDataProvider dataProvider)
        {
            _domainDataProvider = dataProvider;
        }


        #region InvNoSP--SAP单据号选择 add by jinger 2016-02-04

        //获取选中的数据
        /// <summary>
        /// 获取选中的数据
        /// </summary>
        /// <param name="codes">选中的代码</param>
        /// <returns></returns>
        public object[] QuerySelectedInvNo(string[] codes)
        {
            string sql = string.Format(
                "SELECT {0} FROM TBLINVOICES WHERE 1=1 AND INVNO IN ({1}) ORDER BY INVNO",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Invoices)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
        }

        //获取没有被选中的数据
        /// <summary>
        /// 获取没有被选中的数据
        /// </summary>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="receiveUser">供应商代码</param>
        /// <param name="storageCode">库位代码</param>
        /// <param name="codeExcept">codeExcept数组中包含的invNo将不会被显示</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryUNSelectInvNo(string invNo, string receiveUser, string storageCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = @"SELECT    A.INVNO,B.RECEIVERUSER,B.Storagecode
                             FROM TBLINVOICES A ,TBLINVOICESDETAIL B 
                             WHERE A.INVNO=B.INVNO
                               AND A.INVTYPE in( 'PRC','GZC') ";
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(" AND A.INVNO LIKE '%{0}%'", invNo);
            }
            if (!string.IsNullOrEmpty(receiveUser))
            {
                sql += string.Format(" AND B.RECEIVERUSER LIKE '%{0}%'", receiveUser);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND B.Storagecode LIKE '%{0}%'", storageCode);
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND A.INVNO NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += string.Format(" and a.invstatus not in ('Cancel')");
            sql += " group by A.INVNO,B.RECEIVERUSER,B.Storagecode ";
            return this.DataProvider.CustomQuery(typeof(InvoicesDetail), new PagerCondition(sql, "A.INVNO", inclusive, exclusive));
        }

        //获取没有被选中的数据数量
        /// <summary>
        /// 获取没有被选中的数据数量
        /// </summary>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="receiveUser">供应商代码</param>
        /// <param name="storageCode">库位代码</param>
        /// <param name="codeExcept">codeExcept数组中包含的invNo将不会被显示</param>
        /// <returns></returns>
        public int QueryUNSelectInvNoCount(string invNo, string receiveUser, string storageCode, string[] codeExcept)
        {
            string sql = @" SELECT  COUNT(1) from  (
                     SELECT   A.INVNO,B.RECEIVERUSER,B.Storagecode
                            FROM TBLINVOICES A ,TBLINVOICESDETAIL B 
                             WHERE A.INVNO=B.INVNO
                               AND A.INVTYPE in( 'PRC','GZC') ";
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(" AND A.INVNO LIKE '%{0}%'", invNo);
            }
            if (!string.IsNullOrEmpty(receiveUser))
            {
                sql += string.Format(" AND B.RECEIVERUSER LIKE '%{0}%'", receiveUser);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND B.Storagecode LIKE '%{0}%'", storageCode);
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND A.INVNO NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += string.Format(" and a.invstatus not in  ('Cancel')");
            sql += " group by A.INVNO,B.RECEIVERUSER,B.Storagecode )";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region 预留单物料选择介面 add by sam
        public object[] QueryUNSelectInvNoMaterial(string invNo, string dqmCode, string storageCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = @" SELECT A.INVNO,a.invline,B.Mchlongdesc,a.dqmcode , a.planqty,a.custmcode 
                             FROM TBLINVOICESDETAIL A ,tblmaterial B 
                             where A.Mcode=B.Mcode and a.INVLINESTATUS not in ('Cancel')  ";
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(" AND A.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND A.DQMCode = '{0}'", dqmCode);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND A.storagecode ='{0}' ", storageCode);
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND A.INVNO NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(InvoicesDetailQuery), new PagerCondition(sql, "A.INVNO", inclusive, exclusive));
        }

        public int QueryUNSelectInvNoMaterialCount(string invNo, string dqmCode, string storageCode, string[] codeExcept)
        {
            string sql = @"SELECT  COUNT(1)
                             FROM TBLINVOICESDETAIL A ,tblmaterial B 
                             where A.Mcode=B.Mcode  and a.INVLINESTATUS not in ('Cancel')  ";
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(" AND A.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND A.DQMCode = '{0}'", dqmCode);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND A.storagecode ='{0}' ", storageCode);
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND A.INVNO NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region 预留单WWpo介面 add by sam

        public object[] QuerySelectedWWpoInvNo(string[] codes)
        {
            string sql = string.Format(
                @"  SELECT a.*,b.dqmcode,b.mchlongdesc,c.dqmcode cpdqmcode ,c.menshortdesc cpmdesc  FROM tblwwpo a
                            left join tblmaterial b on a.mcode=b.mcode
                           left join tblinvoicesdetail c on  c.invno = a.pono and c.invline= a.poline
                           where 1=1  and a.serial in ({0}) ",
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(MesWWPOExc), new SQLCondition(sql));
        }

        public object[] QueryUnwWpoInvNo(string invNo, string dqmCode, string mdesc, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = @" 
                          SELECT a.*,b.dqmcode,b.mchlongdesc,c.dqmcode cpdqmcode ,c.menshortdesc cpmdesc  FROM tblwwpo a
                            left join tblmaterial b on a.mcode=b.mcode
                           left join tblinvoicesdetail c on  c.invno = a.pono and c.invline= a.poline
                           where 1=1 ";
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(" AND a.pono ='{0}' ", invNo);
            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND b.DQMCode LIKE '%{0}%' ", dqmCode);
            }
            if (!string.IsNullOrEmpty(mdesc))
            {
                sql += string.Format(" AND b.mchlongdesc LIKE '%{0}%' ", mdesc);
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND A.serial NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(MesWWPOExc), new PagerCondition(sql, "a.serial", inclusive, exclusive));
        }

        public int QueryUNWWpoInvNoCount(string invNo, string dqmCode, string mdesc, string[] codeExcept)
        {
            string sql = @"
                        SELECT count(1) FROM tblwwpo a
                        left join tblmaterial b on a.mcode=b.mcode
                        left join tblinvoicesdetail c on  c.invno = a.pono and c.invline= a.poline
                        where 1=1 ";
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(" AND a.pono ='{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND b.DQMCode LIKE '%{0}%' ", dqmCode);
            }
            if (!string.IsNullOrEmpty(mdesc))
            {
                sql += string.Format(" AND b.mchlongdesc LIKE '%{0}%' ", mdesc);
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND A.serial NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region 报废物料选择介面 add by sam
        public object[] QueryUNSelectScrapMaterial(string storageCode, string dqmCode, string mdesc, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = @"  select  a.Dqmcode,a.mdesc  FROM tblstoragedetail A  where 1=1 ";
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND A.storageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(mdesc))
            {
                sql += string.Format(" AND A.mdesc LIKE '%{0}%'", mdesc);
            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND A.DQMCode LIKE '%{0}%'", dqmCode);
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND A.DQMCode NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += string.Format("group by  a.Dqmcode,a.mdesc ");
            return this.DataProvider.CustomQuery(typeof(StorageDetail), new PagerCondition(sql, "A.DQMCode", inclusive, exclusive));
        }

        public int QueryUNSelectScrapMaterialCount(string storageCode, string dqmCode, string mdesc, string[] codeExcept)
        {
            string sql = @"SELECT  a.Dqmcode,a.mdesc 
                                            FROM tblstoragedetail A  where 1=1 ";
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND A.storageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(mdesc))
            {
                sql += string.Format(" AND A.mdesc LIKE '%{0}%'", mdesc);
            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND A.DQMCode LIKE '%{0}%'", dqmCode);
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND A.DQMCode NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += string.Format("group by  a.Dqmcode,a.mdesc ");
            string finalsql = string.Format(" SELECT COUNT(1) from ({0}) ", sql);
            return this.DataProvider.GetCount(new SQLCondition(finalsql));
        }

        #endregion

        #region Model 选择
        /// <summary>
        /// 选择没有被选中的Model
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="modelExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedModel(string modelCode, string[] modelExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and modelcode like '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model)),
                modelCode.ToUpper()
                );

            if (modelExcept.Length > 0)
            {
                sql += string.Format(
                    " and modelcode not in( {0} ) ",
                    FormatObjectCodesForSql(modelExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Model), new PagerCondition(sql, "modelcode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Model的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="modelExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedModelCount(string modelCode, string[] modelExcept)
        {
            string sql = string.Format(
                "select count(modelcode) from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and modelcode like '%{0}%' ",
                modelCode.ToUpper()
                );

            if (modelExcept.Length > 0)
            {
                sql += string.Format(
                    " and modelcode not in( {0} ) ",
                    FormatObjectCodesForSql(modelExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedModel(string[] modelExcept)
        {
            string sql = string.Format(
                "select {0} from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and modelcode in ({1}) order by modelcode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model)),
                FormatObjectCodesForSql(modelExcept).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Model), new SQLCondition(sql));
        }

        #endregion

        #region Item 选择

        /// <summary>
        /// 选择没有被选中的Item
        /// </summary>
        /// <param name="modelCode">模糊查询,产品别代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="itemName">模糊查询,产品名称</param>
        /// <param name="itemDesc">模糊查询,产品描述</param>
        /// <param name="strugc">用户组代码</param>
        /// <returns>object[]</returns>
        public object[] QueryUnSelectedItem(string itemCode, string modelCode, string itemName, string itemDesc, string strugc)
        {
            string sql = string.Format(
                    "select {0} from tblitem,tblmodel2item where 1=1 and tblitem.itemcode = tblmodel2item.itemcode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode like '{2}%' and itemname like '%{3}%' and itemdesc like '%{4}%' and itemcode in (select itemcode from usergroup2item where usergroupcode = '{1}') ",
                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemForQuery)),
                    strugc.ToUpper(),
                    itemCode.ToUpper(),
                    itemName,
                    itemDesc
                    );
            return this.DataProvider.CustomQuery(typeof(ItemForQuery), new SQLCondition(sql));
        }

        /// <summary>
        /// 选择没有被选中的Item
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="itemName">模糊查询,产品名称</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedItem(string modelCode, string itemCode, string itemName, string itemDesc, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select a.itemcode,a.itemname,a.itemdesc,b.modelcode from tblitem a left join  tblmodel2item b on a.itemcode = b.itemcode where 1=1 "
                + " and a.itemcode like '%{1}%' and a.itemname like '%{2}%' and a.itemdesc like '%{3}%' and b.modelcode like '%{0}%' and a.orgid={4} "
                + " ",
                modelCode.ToUpper(),
                itemCode.ToUpper(),
                itemName,
                itemDesc,
                GlobalVariables.CurrentOrganizations.First().OrganizationID
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and a.itemcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(ItemForQuery), new PagerCondition(sql, "a.itemcode", inclusive, exclusive));
        }

        /// <summary>
        /// 选择没有被选中的Item
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="itemDesc">模糊查询,产品描述</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedItem(string modelCode, string itemCode, string itemDesc, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition()
                + " and itemcode like '%{2}%' and itemdesc like '%{3}%' and itemcode in (select itemcode from tblmodel2item where modelcode like '%{1}%' "
                + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)),
                modelCode.ToUpper(),
                itemCode.ToUpper(),
                itemDesc
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and itemcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Item), new PagerCondition(sql, "a.itemcode", inclusive, exclusive));
        }

        public object[] CQueryUnSelectedItem(string modelCode, string itemCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode like '%{2}%' and itemcode in (select itemcode from usergroup2item where usergroupcode ='%{1}%') ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)),
                modelCode.ToUpper(),
                itemCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and itemcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Item), new PagerCondition(sql, "itemcode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="itemName">模糊查询,产品名称</param>
        /// <param name="itemDesc">模糊查询,产品描述</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedItemCount(string modelCode, string itemCode, string itemName, string itemDesc, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(itemcode) from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode like '%{1}%' and itemname like '%{2}%' and itemdesc like '%{3}%' and itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ",
                modelCode.ToUpper(),
                itemCode.ToUpper(),
                itemName,
                itemDesc
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and itemcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="itemDesc">模糊查询,产品描述</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedItemCount(string modelCode, string itemCode, string itemDesc, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(itemcode) from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode like '%{1}%' and itemdesc like '%{2}%' and itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ",
                modelCode.ToUpper(),
                itemCode.ToUpper(),
                itemDesc
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and itemcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedItem(string[] codes)
        {
            string sql = string.Format(
                //"select {0} from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode in ({1}) order by itemcode",
                "select a.itemcode,a.itemname,a.itemdesc,b.modelcode from tblitem a left join  tblmodel2item b on a.itemcode = b.itemcode where 1=1 and a.itemcode in ({0}) and a.orgid={1} order by a.itemcode",
                //DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)),
                FormatObjectCodesForSql(codes).ToUpper(),
                GlobalVariables.CurrentOrganizations.First().OrganizationID
                );
            return this.DataProvider.CustomQuery(typeof(ItemForQuery), new SQLCondition(sql));
        }

        #endregion

        #region Material

        public object[] QueryUnSelectedMaterial(string materialCode, string materialName, string materialDesc, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblmaterial where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and mcode like '%{1}%' and mdesc like '%{2}%' and mname like '%{3}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.MOModel.Material)),
                materialCode,
                materialDesc,
                materialName
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and mcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material), new PagerCondition(sql, "mcode", inclusive, exclusive));
        }

        public object[] QueryUnSelectedMaterial(string materialCode, string materialName, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblmaterial where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and mcode like '%{1}%' and mname like '%{2}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.MOModel.Material)),
                materialCode,
                materialName
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and mcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material), new PagerCondition(sql, "mcode", inclusive, exclusive));
        }

        public int QueryUnSelectedMaterialCount(string materialCode, string materialName, string materialDesc, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(mcode) from tblmaterial where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and mcode like '%{0}%' and mdesc like '%{1}%' and mname like '%{2}%'", materialCode, materialDesc, materialName);

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and mcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int QueryUnSelectedMaterialCount(string materialCode, string materialName, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(mcode) from tblmaterial where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and mcode like '%{0}%' and mname like '%{1}%'", materialCode, materialName);

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and mcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedMaterial(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblmaterial where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and mcode in ({1}) order by mcode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.MOModel.Material)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material), new SQLCondition(sql));
        }

        #endregion

        #region MO 选择
        /// <summary>
        /// 选择没有被选中的Item
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedMO(string modelCode, string itemCode, string moCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

            string sql = string.Format(
                "select tblmo.*,tblitem.itemname from tblmo join tblitem on (tblitem.itemcode = tblmo.itemcode) where 1=1 " + orgIDList + " and tblmo.itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and  tblmo.itemcode like '%{1}%' and tblmo.mocode like '%{2}%'",
                modelCode.ToUpper(),
                itemCode.ToUpper(),
                moCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblmo.mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            // 去除初始化和已下发的工单
            sql += string.Format(
                " and tblmo.mostatus not in ('{0}','{1}')",
                BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_INITIAL,
                BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_RELEASE
                );

            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sql += " and tblmo.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ") ";
            }

            return this.DataProvider.CustomQuery(typeof(MO2Item), new PagerCondition(sql, "tblmo.mocode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedMOCount(string modelCode, string itemCode, string moCode, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(mocode) from tblmo where itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and  itemcode like '%{1}%' and mocode like '%{2}%'",
                modelCode.ToUpper(),
                itemCode.ToUpper(),
                moCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            // 去除初始化和已下发的工单
            sql += string.Format(
                " and mostatus not in ('{0}','{1}')",
                BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_INITIAL,
                BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_RELEASE
                );

            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sql += " and tblmo.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ") ";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedMO(string[] codes)
        {
            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

            string sql = string.Format(
                "select tblmo.*,tblitem.itemname from tblmo join tblitem on (tblitem.itemcode = tblmo.itemcode) where 1=1 " + orgIDList + " and tblmo.mocode in ({0}) order by tblmo.mocode",
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(MO2Item), new SQLCondition(sql));
        }

        #region  By Factory查询条件		add by Simone

        /// <summary>
        /// 选择没有被选中的Item
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedMO(string modelCode, string itemCode, string moCode, string factorycode, string[] codeExcept, string mostatusParam, int inclusive, int exclusive)
        {
            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

            string sql = string.Format(
                "select tblmo.*,tblitem.itemname from tblmo join tblitem on (tblitem.itemcode = tblmo.itemcode)	where 1=1 " + orgIDList + " and tblmo.itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and  tblmo.itemcode like '%{1}%' and tblmo.mocode like '%{2}%'",
                modelCode.ToUpper(),
                itemCode.ToUpper(),
                moCode.ToUpper()
                );

            //工厂查询条件  marked by hiro 08/10/27
            //if(factorycode!=null && factorycode!=string.Empty)
            //{
            //    sql += string.Format(" and tblmo.mocode in (select mocode from tblmo where factory = '{0}') ",factorycode );
            //}

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblmo.mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            //工单状态控制
            if (mostatusParam == "false")
            {
                sql += string.Format(
                    " and tblmo.mostatus not in ('{0}')",
                    BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_CLOSE
                    );
            }
            else
            {
                // 去除初始化和已下发的工单
                sql += string.Format(
                    " and tblmo.mostatus not in ('{0}','{1}')",
                    BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_INITIAL,
                    BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_RELEASE

                    );
            }
            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sql += " and tblmo.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ") ";
            }
            return this.DataProvider.CustomQuery(typeof(MO2Item), new PagerCondition(sql, "tblmo.mocode", inclusive, exclusive));
        }

        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedMOCount(string modelCode, string itemCode, string moCode, string factorycode, string[] codeExcept, string mostatusParam)
        {
            string sql = string.Format(
                "select count(mocode) from tblmo where itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and  itemcode like '%{1}%' and mocode like '%{2}%'",
                modelCode.ToUpper(),
                itemCode.ToUpper(),
                moCode.ToUpper()
                );

            //工厂查询条件  marked by hiro 08/10/27
            //if(factorycode!=null && factorycode!=string.Empty)
            //{
            //    sql += string.Format(" and tblmo.mocode in (select mocode from tblmo where factory = '{0}') ",factorycode );
            //}

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            //工单状态控制
            if (mostatusParam == "false")
            {
                sql += string.Format(
                    " and tblmo.mostatus not in ('{0}')",
                    BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_CLOSE
                    );
            }
            else
            {
                // 去除初始化和已下发的工单
                sql += string.Format(
                    " and tblmo.mostatus not in ('{0}','{1}')",
                    BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_INITIAL,
                    BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_RELEASE
                    );
            }
            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sql += " and tblmo.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ") ";
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region For 在制品查询
        /// <summary>
        /// 选择没有被选中的MO		Jane Shu	2005-06-23
        /// </summary>
        /// <param name="itemCode">精确查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码,为空不做过滤条件</param>
        /// <param name="moStatus">工单状态，用','隔开,为空不做过滤条件</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedMOByItemCode(string itemCode, string moCode, string moStatus, string[] codeExcept, int inclusive, int exclusive)
        {
            string moCodeCondition = "";
            if (moCode != "" && moCode != null)
            {
                moCodeCondition = string.Format(" and mocode like '%{0}%'", moCode.ToUpper());
            }

            string sql = string.Format("select {0} from tblmo where itemcode = '{1}' {2} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)),
                                        itemCode.ToUpper(),
                                        moCode == string.Empty ? "" : string.Format(" and mocode like '%{0}%'", moCode.ToUpper()));

            if (codeExcept.Length > 0)
            {
                sql += string.Format(" and mocode not in({0}) ", FormatObjectCodesForSql(codeExcept));
            }

            if (moStatus.Trim() != string.Empty)
            {
                sql += string.Format(" and upper(mostatus) in ({0})", FormatHelper.ProcessQueryValues(moStatus));
            }

            return this.DataProvider.CustomQuery(typeof(MO), new PagerCondition(sql, "mocode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的MO的数量
        /// </summary>
        /// <param name="itemCode">精确查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码,为空不做过滤条件</param>
        /// <param name="moStatus">工单状态，用','隔开,为空不做过滤条件</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedMOByItemCodeCount(string itemCode, string moCode, string moStatus, string[] codeExcept)
        {
            string sql = string.Format("select count(*) from tblmo where itemcode = '{0}'{1}" + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                                        itemCode.ToUpper(),
                                        moCode == string.Empty ? "" : string.Format(" and mocode like '%{0}%'", moCode.ToUpper()));

            if (codeExcept.Length > 0)
            {
                sql += string.Format(" and mocode not in({0}) ", FormatObjectCodesForSql(codeExcept));
            }

            if (moStatus.Trim() != string.Empty)
            {
                sql += string.Format(" and upper(mostatus) in ({0})", FormatHelper.ProcessQueryValues(moStatus));
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #endregion

        #region ReworkMO 选择

        /// <summary>
        /// 返工工单选择
        /// </summary>
        /// <param name="reworkcode">返工需求单单号</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedReworkMO(string reworkcode, string modelCode, string itemCode, string moCode, string factorycode, string[] codeExcept, int inclusive, int exclusive)
        {
            //--选择的返工工单条件 
            //--在工单中是返工工单(tblmo motype = 'REWORK') 
            //--排除在返工需求单已有的工单 (mocode not in tblreworksheet) ,但是包含当前返工需求单对应的工单 

            //motype = 'REWORK' (select PARAMCODE from TBLSYSPARAM where PARAMGROUPCODE='MOTYPE' and PARAMVALUE = 'motype_reworkmotype')
            string sql = string.Format(
                @"SELECT tblmo.*
					FROM tblmo
					WHERE motype in ( select PARAMCODE from TBLSYSPARAM where PARAMGROUPCODE='MOTYPE' and PARAMVALUE = '{0}' )
					AND mocode NOT IN (SELECT DISTINCT mocode
                                     FROM tblreworksheet
                                    WHERE mocode IS NOT NULL) " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), BenQGuru.eMES.Web.Helper.MOType.MOTYPE_REWORKMOTYPE);


            if (modelCode != null && modelCode != string.Empty)
            {
                sql += string.Format(@" AND itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ", modelCode.ToUpper());
            }

            if (itemCode != null && itemCode != string.Empty)
            {
                sql += string.Format(@" and  itemcode like '%{0}%'  ", itemCode.ToUpper());
            }

            if (moCode != null && moCode != string.Empty)
            {
                sql += string.Format(@" and mocode like '%{0}%'  ", moCode.ToUpper());
            }
            //工厂查询条件
            if (factorycode != null && factorycode != string.Empty)
            {
                sql += string.Format(" and tblmo.mocode in (select mocode from tblmo where factory = '{0}') ", factorycode);
            }

            //包含当前需求单对应的工单
            if (reworkcode != null && reworkcode != string.Empty)
            {
                sql += string.Format(@" OR mocode IN (SELECT mocode FROM tblreworksheet WHERE reworkcode = '{0}') ", reworkcode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblmo.mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            return this.DataProvider.CustomQuery(typeof(MO), new PagerCondition(sql, "tblmo.mocode", inclusive, exclusive));
        }


        /// <summary>
        /// 返工工单数量
        /// </summary>
        /// <param name="reworkcode">返工需求单单号</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public int QueryUnSelectedReworkMOCount(string reworkcode, string modelCode, string itemCode, string moCode, string factorycode, string[] codeExcept)
        {
            string sql = string.Format(
                @"SELECT count(tblmo.mocode)
								FROM tblmo
					WHERE motype  in ( select PARAMCODE from TBLSYSPARAM where PARAMGROUPCODE='MOTYPE' and PARAMVALUE = '{0}' )
					AND mocode NOT IN (SELECT DISTINCT mocode
                                     FROM tblreworksheet
                                    WHERE mocode IS NOT NULL) " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), BenQGuru.eMES.Web.Helper.MOType.MOTYPE_REWORKMOTYPE);


            if (modelCode != null && modelCode != string.Empty)
            {
                sql += string.Format(@" AND itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ", modelCode.ToUpper());
            }

            if (itemCode != null && itemCode != string.Empty)
            {
                sql += string.Format(@" and  itemcode like '%{0}%'  ", itemCode.ToUpper());
            }

            if (moCode != null && moCode != string.Empty)
            {
                sql += string.Format(@" and mocode like '%{0}%'  ", moCode.ToUpper());
            }
            //工厂查询条件
            if (factorycode != null && factorycode != string.Empty)
            {
                sql += string.Format(" and tblmo.mocode in (select mocode from tblmo where factory = '{0}') ", factorycode);
            }

            //包含当前需求单对应的工单
            if (reworkcode != null && reworkcode != string.Empty)
            {
                sql += string.Format(@" OR mocode IN (SELECT mocode FROM tblreworksheet WHERE reworkcode = '{0}') ", reworkcode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblmo.mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region  RMAReworkMO 选择

        /// <summary>
        /// 返工工单选择
        /// </summary>
        /// <param name="reworkcode">返工需求单单号</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedReworkMO(string reworkcode, string modelCode, string itemCode, string moCode, string factorycode, string[] codeExcept, int inclusive, int exclusive, string moType)
        {
            string sql = string.Format(
                @"SELECT tblmo.*
					FROM tblmo
					WHERE motype in ( '{0}' )
					AND mocode NOT IN (SELECT DISTINCT mocode
                                     FROM tblreworksheet
                                    WHERE mocode IS NOT NULL) " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), moType);


            if (modelCode != null && modelCode != string.Empty)
            {
                sql += string.Format(@" AND itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ", modelCode.ToUpper());
            }

            if (itemCode != null && itemCode != string.Empty)
            {
                sql += string.Format(@" and  itemcode like '%{0}%'  ", itemCode.ToUpper());
            }

            if (moCode != null && moCode != string.Empty)
            {
                sql += string.Format(@" and mocode like '%{0}%'  ", moCode.ToUpper());
            }
            //工厂查询条件
            if (factorycode != null && factorycode != string.Empty)
            {
                sql += string.Format(" and tblmo.mocode in (select mocode from tblmo where factory = '{0}') ", factorycode);
            }

            //包含当前需求单对应的工单
            if (reworkcode != null && reworkcode != string.Empty)
            {
                sql += string.Format(@" OR mocode IN (SELECT mocode FROM tblreworksheet WHERE reworkcode = '{0}') ", reworkcode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblmo.mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            return this.DataProvider.CustomQuery(typeof(MO), new PagerCondition(sql, "tblmo.mocode", inclusive, exclusive));
        }


        /// <summary>
        /// 返工工单数量
        /// </summary>
        /// <param name="reworkcode">返工需求单单号</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public int QueryUnSelectedReworkMOCount(string reworkcode, string modelCode, string itemCode, string moCode, string factorycode, string[] codeExcept, string moType)
        {
            string sql = string.Format(
                @"SELECT count(tblmo.mocode)
								FROM tblmo
					WHERE motype  in ( '{0}' )
					AND mocode NOT IN (SELECT DISTINCT mocode
                                     FROM tblreworksheet
                                    WHERE mocode IS NOT NULL) " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), moType);


            if (modelCode != null && modelCode != string.Empty)
            {
                sql += string.Format(@" AND itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ", modelCode.ToUpper());
            }

            if (itemCode != null && itemCode != string.Empty)
            {
                sql += string.Format(@" and  itemcode like '%{0}%'  ", itemCode.ToUpper());
            }

            if (moCode != null && moCode != string.Empty)
            {
                sql += string.Format(@" and mocode like '%{0}%'  ", moCode.ToUpper());
            }
            //工厂查询条件
            if (factorycode != null && factorycode != string.Empty)
            {
                sql += string.Format(" and tblmo.mocode in (select mocode from tblmo where factory = '{0}') ", factorycode);
            }

            //包含当前需求单对应的工单
            if (reworkcode != null && reworkcode != string.Empty)
            {
                sql += string.Format(@" OR mocode IN (SELECT mocode FROM tblreworksheet WHERE reworkcode = '{0}') ", reworkcode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblmo.mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region ReworkSheet 返工工单选择

        /// <summary>
        /// 选择没有被选中的Item
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedReworkSheet(string modelCode, string itemCode, string moCode, string reworkSheetCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string Condition = string.Empty;
            if (moCode != null && moCode != string.Empty)
            {
                Condition += string.Format(" and tblreworksheet.mocode like '%{0}%' ", moCode);
            }
            else if (itemCode != null && itemCode != string.Empty)
            {
                Condition += string.Format(" and  tblreworksheet.itemcode like '%{0}%' ", itemCode);
            }
            else if (reworkSheetCode != null && reworkSheetCode != string.Empty)
            {
                Condition += string.Format(" and tblreworksheet.REWORKCODE like '%{0}%' ", reworkSheetCode);
            }

            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

            string sql = string.Format(
                "select tblreworksheet.*,tblitem.itemname from tblreworksheet join tblitem on (tblitem.itemcode = tblreworksheet.itemcode) where 1=1 " + orgIDList + " and reworkcode in (select reworkcode from TBLREWORKPASS where status=1 and ISPASS=1) and tblreworksheet.itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")  {1}",
                modelCode.ToUpper(),
                Condition
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblreworksheet.REWORKCODE not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += " order by tblreworksheet.REWORKCODE ";
            return this.DataProvider.CustomQuery(typeof(SelectReworkSheet), new PagerCondition(sql, "tblreworksheet.REWORKSCODE", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedReworkSheetCount(string modelCode, string itemCode, string moCode, string reworkSheetCode, string[] codeExcept)
        {
            string Condition = string.Empty;
            if (moCode != null && moCode != string.Empty)
            {
                Condition += string.Format(" and tblreworksheet.mocode like '%{0}%' ", moCode);
            }
            else if (itemCode != null && itemCode != string.Empty)
            {
                Condition += string.Format(" and  tblreworksheet.itemcode like '%{0}%' ", itemCode);
            }
            else if (reworkSheetCode != null && reworkSheetCode != string.Empty)
            {
                Condition += string.Format(" and tblreworksheet.REWORKCODE like '%{0}%' ", reworkSheetCode);
            }

            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

            string sql = string.Format(
                "select count(tblreworksheet.REWORKCODE) from tblreworksheet join tblitem on (tblitem.itemcode = tblreworksheet.itemcode) where 1=1 " + orgIDList + " and reworkcode in (select reworkcode from TBLREWORKPASS where status=1 and ISPASS=1) and tblreworksheet.itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")  {1}",
                modelCode.ToUpper(),
                Condition
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblreworksheet.REWORKCODE not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedReworkSheet(string[] codes)
        {

            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

            string sql = string.Format(
                "select tblreworksheet.*,tblitem.itemname from tblreworksheet join tblitem on (tblitem.itemcode = tblreworksheet.itemcode) where 1=1 " + orgIDList + " and tblreworksheet.REWORKCODE in ({0}) order by tblreworksheet.REWORKCODE",
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(SelectReworkSheet), new SQLCondition(sql));
        }

        #endregion

        #region ReworkCode

        public object[] QueryUnSelectedReworkCode(string reworkCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = "SELECT {0} FROM tblreworksheet WHERE reworkcode LIKE '%" + reworkCode + "%' AND status IN ('" + ReworkStatus.REWORKSTATUS_RELEASE + "','" + ReworkStatus.REWORKSTATUS_OPEN + "')";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReworkSheet)));

            if (codeExcept.Length > 0)
            {
                sql += " AND reworkcode NOT IN ( " + FormatObjectCodesForSql(codeExcept) + " )";
            }

            return this.DataProvider.CustomQuery(typeof(ReworkSheet), new PagerCondition(sql, "reworkcode", inclusive, exclusive));
        }

        public int QueryUnSelectedReworkCodeCount(string reworkCode, string[] codeExcept)
        {
            string sql = "SELECT COUNT(*) FROM tblreworksheet WHERE reworkcode LIKE '%" + reworkCode + "%' AND status IN ('" + ReworkStatus.REWORKSTATUS_RELEASE + "','" + ReworkStatus.REWORKSTATUS_OPEN + "')";

            if (codeExcept.Length > 0)
            {
                sql += " AND reworkcode NOT IN ( " + FormatObjectCodesForSql(codeExcept) + " )";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedReworkCode(string[] codes)
        {
            string sql = "SELECT {0} FROM tblreworksheet WHERE reworkcode IN (" + FormatObjectCodesForSql(codes) + ") AND status IN ('" + ReworkStatus.REWORKSTATUS_RELEASE + "','" + ReworkStatus.REWORKSTATUS_OPEN + "')";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReworkSheet)));
            return this.DataProvider.CustomQuery(typeof(ReworkSheet), new SQLCondition(sql));
        }

        #endregion

        #region Operation 选择
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeCode"></param>
        /// <param name="opCode"></param>
        /// <param name="codeExcept"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedOperation(string routeCode, string opCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblop where opcode in (select opcode from tblroute2op where routecode like '%{1}%') and opcode like '%{2}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation)),
                routeCode.ToUpper(),
                opCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and opcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.CustomQuery(typeof(Operation), new PagerCondition(sql, "opcode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedOperationCount(string routeCode, string opCode, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(opcode) from tblop where opcode in (select opcode from tblroute2op where routecode like '%{0}%') and opcode like '%{1}%' ",
                routeCode.ToUpper(),
                opCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and opcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedOperation(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblop where opcode in ({1}) order by opcode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition(sql));
        }

        #endregion

        #region Segment 选择
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segmentCode"></param>
        /// <param name="codeExcept"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedSegment(string segmentCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblseg where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and segcode like '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Segment)),
                segmentCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and segcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.CustomQuery(typeof(Segment), new PagerCondition(sql, "segcode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedSegmentCount(string segmentCode, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(segcode) from tblseg where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and segcode like '%{0}%' ",
                segmentCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and segcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedSegment(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblseg where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and segcode in ({1}) order by segcode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Segment)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Segment), new SQLCondition(sql));
        }

        #endregion

        #region StepSequence 选择
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segmentCode"></param>
        /// <param name="stepSequenceCode"></param>
        /// <param name="codeExcept"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedStepSequence(string segmentCode, string stepSequenceCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblss where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and segcode like '%{1}%' and sscode like '%{2}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)),
                segmentCode.ToUpper(),
                stepSequenceCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and sscode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.CustomQuery(typeof(StepSequence), new PagerCondition(sql, "sscode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedStepSequenceCount(string segmentCode, string stepSequenceCode, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(sscode) from tblss where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and segcode like '%{0}%' and sscode like '%{1}%' ",
                segmentCode.ToUpper(),
                stepSequenceCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and sscode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedStepSequence(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblss where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and sscode in ({1}) order by sscode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(sql));
        }

        public object[] QueryUnSelectedStepSequenceInSegment(string segmentCode, string stepSequenceCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string segCondition = segmentCode.ToUpper();
            if (segCondition.Trim().Length > 0)
            {
                segCondition = " AND segcode IN (" + FormatHelper.ProcessQueryValues(segmentCode.ToUpper()) + ") ";
            }

            string sql = string.Format(
                "select {0} from tblss where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {1} and sscode like '%{2}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)),
                segCondition,
                stepSequenceCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and sscode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }

            return this.DataProvider.CustomQuery(typeof(StepSequence), new PagerCondition(sql, "sscode", inclusive, exclusive));

        }

        public int QueryUnSelectedStepSequenceInSegmentCount(string segmentCode, string stepSequenceCode, string[] codeExcept)
        {
            string segCondition = segmentCode.ToUpper();
            if (segCondition.Trim().Length > 0)
            {
                segCondition = " AND segcode IN (" + FormatHelper.ProcessQueryValues(segmentCode.ToUpper()) + ") ";
            }

            string sql = string.Format(
                "select count(sscode) from tblss where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {0} and sscode like '%{1}%' ",
                segCondition,
                stepSequenceCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and sscode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region Resource 选择
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segmentCode"></param>
        /// <param name="stepSequenceCode"></param>
        /// <param name="codeExcept"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedResource(string segmentCode, string stepSequenceCode, string opCode, string resCode, string[] codeExcept, int inclusive, int exclusive)
        {

            string sql1, sql2;
            // segment,stepsequence做为条件
            if (segmentCode.Length > 0 || stepSequenceCode.Length > 0)
            {
                sql1 = string.Format(
                    " (segcode like '%{0}%' and sscode like '%{1}%')",
                    segmentCode.ToUpper(),
                    stepSequenceCode.ToUpper()
                    );
            }
            else
            {
                sql1 = " 1=1 ";
            }

            // op做为条件
            if (opCode.Length > 0)
            {
                sql2 = string.Format(
                    " rescode in (select rescode from tblop2res where opcode like '%{0}%')",
                    opCode.ToUpper()
                    );
            }
            else
            {
                sql2 = " 1=1 ";
            }

            string sql = string.Format(
                "select {0} from tblres where rescode like '%{1}%' and {2} and {3} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)),
                resCode.ToUpper(),
                sql1,
                sql2
                );


            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and rescode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }


            return this.DataProvider.CustomQuery(typeof(Resource), new PagerCondition(sql, "rescode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedResourceCount(string segmentCode, string stepSequenceCode, string opCode, string resCode, string[] codeExcept)
        {
            string sql1, sql2;
            // segment,stepsequence做为条件
            if (segmentCode.Length > 0 || stepSequenceCode.Length > 0)
            {
                sql1 = string.Format(
                    " (segcode like '%{0}%' and sscode like '%{1}%')",
                    segmentCode.ToUpper(),
                    stepSequenceCode.ToUpper()
                    );
            }
            else
            {
                sql1 = "1=1";
            }

            // op做为条件
            if (opCode.Length > 0)
            {
                sql2 = string.Format(
                    " rescode in (select rescode from tblop2res where opcode like '%{0}%')",
                    opCode.ToUpper()
                    );
            }
            else
            {
                sql2 = "1=1";
            }

            string sql = string.Format(
                "select count(rescode) from tblres where rescode like '%{0}%' and {1} and {2} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                resCode.ToUpper(),
                sql1,
                sql2
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and rescode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedResource(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblres where rescode in ({1}) " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by rescode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(sql));
        }

        #endregion

        #region Resource 选择-根据TBLRES表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="segmentCode"></param>
        /// <param name="stepSequenceCode"></param>
        /// <param name="codeExcept"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedResource(string ssCode, string resCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblres where rescode like '%{1}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)),
                resCode.ToUpper()
                );
            if (ssCode != null && ssCode.Length > 0)
            {
                sql += string.Format(
                    " and sscode in( {0} ) ",
                    FormatHelper.ProcessQueryValues(ssCode)
                    );
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and rescode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }


            return this.DataProvider.CustomQuery(typeof(Resource), new PagerCondition(sql, "rescode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的数量
        /// </summary>
        /// <param name="resCode">模糊查询</param>
        /// <param name="codeExcept">codeExcept数组中包含的Code将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedResourceCount(string ssCode, string resCode, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(rescode) from tblres where rescode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                resCode.ToUpper()
                );

            if (ssCode != null && ssCode.Length > 0)
            {
                sql += string.Format(
                    " and sscode in( {0} ) ",
                    FormatHelper.ProcessQueryValues(ssCode)
                    );
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and rescode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //public object[] QuerySelectedResource(string[] codes)
        //{
        //    string sql = string.Format(
        //        "select {0} from tblres where rescode in ({1}) " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by rescode",
        //        DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)),
        //        FormatObjectCodesForSql(codes).ToUpper()
        //        );
        //    return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(sql));
        //}

        #endregion

        #region SelectComplex选择

        public object[] QueryUnSelectedData(string dataSource, string dataType, string selectCode, string selectDesc, string queryCode, string queryDesc, string[] codeExcept, int inclusive, int exclusive)
        {
            if (dataType != null && dataType.ToUpper() == "STATIC")
            {
                object[] objreturn = null;
                ArrayList arrRutn = new ArrayList();
                string[] data = dataSource.Split(';');

                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] != "" && data[i].IndexOf(",") >= 0)
                    {
                        string[] strTmpVal = data[i].Split(',');
                        eMES.Domain.ReportView.SelectQuery obj = new BenQGuru.eMES.Domain.ReportView.SelectQuery();
                        obj.Code = strTmpVal[0];
                        obj.CodeDesc = strTmpVal[1];
                        bool bcode = false;
                        bool bdesc = false;
                        if (queryCode != null && queryCode.Length > 0)
                        {
                            if (obj.Code.ToUpper() == queryCode.ToUpper())
                            {
                                bcode = true;
                            }
                        }
                        else
                        {
                            bcode = true;
                        }

                        if (queryDesc != null && queryDesc.Length > 0)
                        {
                            if (obj.CodeDesc.ToUpper() == queryDesc.ToUpper())
                            {
                                bdesc = true;
                            }
                        }
                        else
                        {
                            bdesc = true;
                        }

                        if (bcode && bdesc)
                        {
                            arrRutn.Add(obj);
                        }
                    }
                }
                return (object[])arrRutn.ToArray(typeof(eMES.Domain.ReportView.SelectQuery));
            }
            else
            {
                string sql = "SELECT  " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(eMES.Domain.ReportView.SelectQuery)) + "  FROM (SELECT DISTINCT " + selectCode + " as code ," + selectDesc + " as codedesc  FROM (" + dataSource + ") WHERE "
                   + selectCode + " LIKE '%" + queryCode + "%' ";

                if (queryDesc.Trim().Length > 0)
                {
                    sql += " AND " + selectDesc + " LIKE '%" + queryDesc + "%' ";
                }

                sql += ")";
                if (codeExcept.Length > 0)
                {
                    sql += string.Format(
                        " WHERE Code not in( {0} ) ",
                        FormatObjectCodesForSql(codeExcept)
                        );
                }

                //sql += " ORDER BY code";
                return this.DataProvider.CustomQuery(typeof(eMES.Domain.ReportView.SelectQuery), new PagerCondition(sql, "code", inclusive, exclusive));
                //return this.DataProvider.CustomQuery(typeof(eMES.Domain.ReportView.SelectQuery), new SQLCondition(sql));
            }
        }

        public object[] QuerySelectedData(string dataSource, string dataType, string selectCode, string selectDesc, string[] codes)
        {
            if (dataType != null && dataType.ToUpper() == "STATIC")
            {
                ArrayList arrRutn = new ArrayList();
                string[] data = dataSource.Split(';');

                for (int i = 0; i < data.Length; i++)
                {
                    bool select = false;
                    if (data[i] != "" && data[i].IndexOf(",") >= 0)
                    {
                        string[] strTmpVal = data[i].Split(',');
                        foreach (string str in codes)
                        {
                            if (str.ToUpper() == strTmpVal[0].ToUpper())
                            {
                                select = true;
                                break;
                            }
                        }
                        if (select)
                        {
                            eMES.Domain.ReportView.SelectQuery obj = new BenQGuru.eMES.Domain.ReportView.SelectQuery();
                            obj.Code = strTmpVal[0];
                            obj.CodeDesc = strTmpVal[1];
                            arrRutn.Add(obj);
                        }
                    }
                }
                return (object[])arrRutn.ToArray(typeof(eMES.Domain.ReportView.SelectQuery));

            }
            else
            {
                string sql = "SELECT DISTINCT " + selectCode + " as code ," + selectDesc + " as codedesc  FROM (" + dataSource + ") WHERE  "
                    + selectCode + " in (" + FormatObjectCodesForSql(codes) + ")";
                sql += " ORDER BY " + selectCode + " ";
                return this.DataProvider.CustomQuery(typeof(eMES.Domain.ReportView.SelectQuery), new SQLCondition(sql));
            }
        }

        public int QueryUnSelectedDataCount(string dataSource, string dataType, string selectCode, string selectDesc, string queryCode, string queryDesc, string[] codeExcept)
        {
            if (dataType != null && dataType.ToUpper() == "STATIC")
            {
                ArrayList arrRutn = new ArrayList();
                string[] data = dataSource.Split(';');
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] != "" && data[i].IndexOf(",") >= 0)
                    {
                        string[] strTmpVal = data[i].Split(',');
                        eMES.Domain.ReportView.SelectQuery obj = new BenQGuru.eMES.Domain.ReportView.SelectQuery();
                        obj.Code = strTmpVal[0];
                        obj.CodeDesc = strTmpVal[1];
                        bool bcode = false;
                        bool bdesc = false;
                        if (queryCode != null && queryCode.Length > 0)
                        {
                            if (obj.Code.ToUpper() == queryCode.ToUpper())
                            {
                                bcode = true;
                            }
                        }
                        else
                        {
                            bcode = true;
                        }

                        if (queryDesc != null && queryDesc.Length > 0)
                        {
                            if (obj.CodeDesc.ToUpper() == queryDesc.ToUpper())
                            {
                                bdesc = true;
                            }
                        }
                        else
                        {
                            bdesc = true;
                        }

                        if (bcode && bdesc)
                        {
                            arrRutn.Add(obj);
                        }
                    }
                }
                return arrRutn.Count;
            }
            else
            {
                string sql = "SELECT  count(distinct " + selectCode + ")  FROM (" + dataSource + ") WHERE "
                     + selectCode + " LIKE '%" + queryCode + "%' ";

                if (queryDesc.Trim().Length > 0)
                {
                    sql += " AND " + selectDesc + " LIKE '%" + queryDesc + "%' ";
                }

                if (codeExcept.Length > 0)
                {
                    sql += string.Format(
                        " and {0} not in( {1} ) ", selectCode,
                        FormatObjectCodesForSql(codeExcept)
                        );
                }
                return this.DataProvider.GetCount(new SQLCondition(sql));
            }
        }

        #endregion

        #region ErrorCode 选择
        /// <summary>
        /// 选择没有被选中的ErrorCode
        /// </summary>
        /// <param name="itemCode">模糊查询,物料代码</param>
        /// <param name="itemName">模糊查询,物料名称</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedErrorCode(string _modelCode, string _ecgcode, string _ecode, string[] codeExcept, int inclusive, int exclusive)
        {
            string ecgroupcondition = string.Empty;
            if (_ecgcode != string.Empty)
            { ecgroupcondition = string.Format(" and b.ecgcode ='{0}' ", _ecgcode); }//不良代码组条件
            string eccondition = string.Empty;
            if (_ecode != string.Empty)
            { eccondition = string.Format(" and a.ecode  = '{0}' ", _ecode); }//不良代码条件
            string sql = string.Format(
                @" select b.ecgcode,b.ecgdesc,a.ecode,a.ecdesc
						from tblec a,tblecg b,tblecg2ec c
						where a.ecode = c.ecode
						and   b.ecgcode = c.ecgcode
						and c.ecgcode in (
							select ecgcode from TBLMODEL2ECG
							where modelcode='{0}'
						)
						{1}{2}"
                , _modelCode, ecgroupcondition, eccondition
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and (c.ECGCODE,c.ECODE) not in ( {0} ) ",
                    FormatObjectMutiCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.CustomQuery(typeof(ErrorGroup2CodeSelect), new PagerCondition(sql, inclusive, exclusive));
        }

        //处理split的参数组
        private string FormatObjectMutiCodesForSql(string[] codeExcept)
        {
            if (codeExcept != null && codeExcept.Length > 0)
            {
                for (int i = 0; i < codeExcept.Length; i++)
                {
                    string[] ss = codeExcept[i].Split(':');
                    codeExcept[i] = "('" + ss[0] + "','" + ss[1] + "')";
                }
                return string.Join(",", codeExcept);
            }
            return "('','')";
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="itemCode">模糊查询,物料代码</param>
        /// <param name="itemName">模糊查询,物料名称</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedErrorCodeCount(string _modelCode, string _ecgcode, string _ecode, string[] codeExcept)
        {
            string ecgroupcondition = string.Empty;
            if (_ecgcode != string.Empty)
            { ecgroupcondition = string.Format(" and b.ecgcode ='{0}' ", _ecgcode); }//不良代码组条件
            string eccondition = string.Empty;
            if (_ecode != string.Empty)
            { eccondition = string.Format(" and a.ecode  = '{0}' ", _ecode); }//不良代码条件
            string sql = string.Format(
                @" select Count(b.ecgcode)
						from tblec a,tblecg b,tblecg2ec c
						where a.ecode = c.ecode
						and   b.ecgcode = c.ecgcode
						and c.ecgcode in (
							select ecgcode from TBLMODEL2ECG
							where modelcode='{0}'
						)
						{1}{2}"
                , _modelCode, ecgroupcondition, eccondition
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and (c.ECGCODE,c.ECODE) not in ( {0} ) ",
                    FormatObjectMutiCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedErrorCode(string[] codeExcept)
        {
            string sql = string.Format(
                @" select b.ecgcode,b.ecgdesc,a.ecode,a.ecdesc
						from tblec a,tblecg b,tblecg2ec c
						where a.ecode = c.ecode
						and   b.ecgcode = c.ecgcode
						and c.ecgcode in (
							select ecgcode from TBLMODEL2ECG
							where 1=1 and (c.ECGCODE,c.ECODE) in ( {0} )) ",
                            FormatObjectMutiCodesForSql(codeExcept).ToUpper());

            return this.DataProvider.CustomQuery(typeof(ErrorGroup2CodeSelect), new SQLCondition(sql));
        }

        #endregion

        #region WarehouseItem 选择
        /// <summary>
        /// 选择没有被选中的Item
        /// </summary>
        /// <param name="itemCode">模糊查询,物料代码</param>
        /// <param name="itemName">模糊查询,物料名称</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedWarehouseItem(string mocode, string itemCode, string itemName, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            if (mocode != string.Empty)
            {
                sql = "select a.*  from TBLWHITEM a,tblopbomdetail b " +
                                    "		where 1 = 1 " +
                                    "		and a.ITEMCODE like '%{0}%' and  a.ITEMNAME like '%{1}%' " +
                                    "		and a.itemcode = b.obitemcode " +
                                    "		and b.actiontype = 0	 " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "		and b.itemcode = (select itemcode from tblmo where mocode = '{2}') " +
                                    "		and b.obcode in (select routecode from tblmo2route where mocode = '{2}') " +
                                    "		and b.opcode in " +
                                    "					(select opcode " +
                                    "						from tblitemroute2op " +
                                    "						where itemcode = " +
                                    "							(select itemcode from tblmo where mocode = '{2}') " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "						and (substr(opcontrol, {3}, 1) = '1'))";
                sql = string.Format(sql, itemCode, itemName, mocode
                                            , ((int)BenQGuru.eMES.BaseSetting.OperationList.ComponentLoading + 1));
            }
            else
            {
                sql = string.Format(
                    "select a.* from TBLWHITEM a where a.ITEMCODE like '%{0}%' and  a.ITEMNAME like '%{1}%' ",
                    itemCode,
                    itemName
                    );

            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and a.ITEMCODE not in ( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Warehouse.WarehouseItem), new PagerCondition(sql, "a.ITEMCODE", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="itemCode">模糊查询,物料代码</param>
        /// <param name="itemName">模糊查询,物料名称</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedWarehouseItemCount(string mocode, string itemCode, string itemName, string[] codeExcept)
        {
            string sql = string.Empty;
            if (mocode != string.Empty)
            {
                sql = "select count(*)  from TBLWHITEM a,tblopbomdetail b " +
                                    "		where 1 = 1 " +
                                    "		and a.ITEMCODE like '%{0}%' and  a.ITEMNAME like '%{1}%' " +
                                    "		and a.itemcode = b.obitemcode " +
                                    "		and b.actiontype = 0	 " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "		and b.itemcode = (select itemcode from tblmo where mocode = '{2}') " +
                                    "		and b.obcode in (select routecode from tblmo2route where mocode = '{2}') " +
                                    "		and b.opcode in " +
                                    "					(select opcode " +
                                    "						from tblitemroute2op " +
                                    "						where itemcode = " +
                                    "							(select itemcode from tblmo where mocode = '{2}') " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "						and (substr(opcontrol, {3}, 1) = '1'))";
                sql = string.Format(sql, itemCode, itemName, mocode
                    , ((int)BenQGuru.eMES.BaseSetting.OperationList.ComponentLoading + 1));
            }
            else
            {
                sql = string.Format(
                    "select count(*) from TBLWHITEM a where a.ITEMCODE like '%{0}%' and  a.ITEMNAME like '%{1}%' ",
                    itemCode,
                    itemName
                    );

            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and a.ITEMCODE not in ( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedWarehouseItem(string[] codes)
        {
            string sql = string.Format(
                "select {0} from TBLWHITEM where ITEMCODE in ({1}) order by ITEMCODE",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.Warehouse.WarehouseItem)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Warehouse.WarehouseItem), new SQLCondition(sql));
        }

        #endregion

        #region User
        public object[] QueryUnSelectedUser(string department, string userCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string departmentCondition = "";
            if (department != "" && department != null)
            {
                departmentCondition += string.Format(
                            @" and upper(USERDEPART) like '%{0}%'", department.ToUpper());
            }

            string userCodeCondition = "";
            if (userCode != "" && userCode != null)
            {
                userCodeCondition += string.Format(
                    @" and usercode like '%{0}%'", userCode.ToUpper());
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and usercode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }
#if DEBUG
            Log.Info(
                new PagerCondition(
                string.Format(
                @" select {0} from tbluser where 1=1 {1}{2}{3}", "usercode,username",
                departmentCondition, userCodeCondition, exceptCondition),
                "usercode", inclusive, exclusive, true).SQLText);
#endif

            return this.DataProvider.CustomQuery(
                typeof(User),
                new PagerCondition(
                string.Format(
                @" select {0} from tbluser where 1=1 {1}{2}{3}", "usercode,username",
                departmentCondition, userCodeCondition, exceptCondition),
                "usercode", inclusive, exclusive, true));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedUserCount(string department, string userCode, string[] codeExcept)
        {
            string departmentCondition = "";
            if (department != "" && department != null)
            {
                departmentCondition += string.Format(
                    @" and upper(USERDEPART) like '%{0}%'", department.ToUpper());
            }

            string userCodeCondition = "";
            if (userCode != "" && userCode != null)
            {
                userCodeCondition += string.Format(
                    @" and usercode like '%{0}%'", userCode.ToUpper());
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and usercode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }

#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(
                @" select {0} from tbluser where 1=1 {1}{2}{3}", "count(usercode)",
                departmentCondition, userCodeCondition, exceptCondition)).SQLText);
#endif

            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(
                @" select {0} from tbluser where 1=1 {1}{2}{3}", "count(usercode)",
                departmentCondition, userCodeCondition, exceptCondition)));
        }

        public object[] QuerySelectedUser(string[] codes)
        {
            string codesCondition = string.Format(@" and usercode in ({0})", this.FormatObjectCodesForSql(codes).ToUpper());

#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(@" select {0} from tbluser where 1=1 {1}", "usercode,username", codesCondition)
                ).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(User),
                new SQLCondition(
                string.Format(@" select {0} from tbluser where 1=1 {1}", "usercode,username", codesCondition)
                ));
        }
        #endregion

        #region User Mail
        public object[] QueryUnSelectedUserMail(string department, string userCode, string userName, string[] codeExcept, int inclusive, int exclusive)
        {
            string departmentCondition = "";
            if (department != "" && department != null)
            {
                departmentCondition += string.Format(
                    @" and upper(USERDEPART) like '%{0}%'", department.ToUpper());
            }

            string userCodeCondition = "";
            if (userCode != "" && userCode != null)
            {
                userCodeCondition += string.Format(
                    @" and usercode like '%{0}%'", userCode.ToUpper());
            }

            string userNameCondition = "";
            if (userName != "" && userName != null)
            {
                userNameCondition += string.Format(
                    @" and username like '%{0}%'", userName);
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and usercode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }
#if DEBUG
            Log.Info(
                new PagerCondition(
                string.Format(
                @" select {0} from tbluser where 1=1 {1}{2}{3}{4}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(User)),
                departmentCondition, userCodeCondition, userNameCondition, exceptCondition),
                "usercode", inclusive, exclusive, true).SQLText);
#endif

            return this.DataProvider.CustomQuery(
                typeof(User),
                new PagerCondition(
                string.Format(
                @" select {0} from tbluser where 1=1 {1}{2}{3}{4}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(User)),
                departmentCondition, userCodeCondition, userNameCondition, exceptCondition),
                "usercode", inclusive, exclusive, true));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedUserMailCount(string department, string userCode, string userName, string[] codeExcept)
        {
            string departmentCondition = "";
            if (department != "" && department != null)
            {
                departmentCondition += string.Format(
                    @" and upper(USERDEPART) like '%{0}%'", department.ToUpper());
            }

            string userCodeCondition = "";
            if (userCode != "" && userCode != null)
            {
                userCodeCondition += string.Format(
                    @" and usercode like '%{0}%'", userCode.ToUpper());
            }

            string userNameCondition = "";
            if (userName != "" && userName != null)
            {
                userNameCondition += string.Format(
                    @" and username like '%{0}%'", userName);
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and usercode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }

#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(
                @" select {0} from tbluser where 1=1 {1}{2}{3}{4}", "count(usercode)",
                departmentCondition, userCodeCondition, userNameCondition, exceptCondition)).SQLText);
#endif

            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(
                @" select {0} from tbluser where 1=1 {1}{2}{3}{4}", "count(usercode)",
                departmentCondition, userCodeCondition, userNameCondition, exceptCondition)));
        }

        public object[] QuerySelectedUserMail(string[] codes)
        {
            string codesCondition = string.Format(@" and usercode in ({0})", this.FormatObjectCodesForSql(codes).ToUpper());

#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(@" select {0} from tbluser where 1=1 {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(User)), codesCondition)
                ).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(User),
                new SQLCondition(
                string.Format(@" select {0} from tbluser where 1=1 {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(User)), codesCondition)
                ));
        }
        #endregion

        #region Symptom
        /* added by jessie lee, 2006/6/28 */

        public object[] QueryUnSelectedSymptom(string modelcode, string[] codeExcept, int inclusive, int exclusive)
        {
            string modelCodeCondition = "";
            if (modelcode != "" && modelcode != null)
            {
                modelCodeCondition += string.Format(
                    @" and symptomcode in (select symptomcode from tblmodel2errsym where modelcode like '%{0}%') ", modelcode.ToUpper());
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and symptomcode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }

            PagerCondition pagerCondition = new PagerCondition(string.Format(
                @" select {0} from tbles where 1=1 {1}{2}", "symptomcode,esdesc",
                modelCodeCondition, exceptCondition),
                "symptomcode,esdesc", inclusive, exclusive, true);
#if DEBUG
            Log.Info(pagerCondition.SQLText);
#endif

            return this.DataProvider.CustomQuery(typeof(ErrorSymptom), pagerCondition);
        }

        public int QueryUnSelectedSymptomCount(string modelcode, string[] codeExcept)
        {
            string modelCodeCondition = "";
            if (modelcode != "" && modelcode != null)
            {
                modelCodeCondition += string.Format(
                    @" and symptomcode in (select symptomcode from tblmodel2errsym where modelcode like '%{0}%') ", modelcode.ToUpper());
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and symptomcode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }

            SQLCondition sqlCondition = new SQLCondition(
                string.Format(
                @" select {0} from tbles where 1=1 {1}{2}", "count(*)",
                modelCodeCondition, exceptCondition));

#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif

            return this.DataProvider.GetCount(sqlCondition);
        }

        public object[] QuerySelectedSymptom(string[] codes)
        {
            string codesCondition = string.Format(@" and symptomcode in ({0})", this.FormatObjectCodesForSql(codes).ToUpper());

#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(@" select {0} from tbles where 1=1 {1}", "symptomcode,esdesc", codesCondition)
                ).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(ErrorSymptom),
                new SQLCondition(
                string.Format(@" select {0} from tbles where 1=1 {1}", "symptomcode,esdesc", codesCondition)
                ));
        }

        #endregion

        #region BU
        /* added by jessie lee, 2006/6/28 */

        public object[] QueryUnSelectedBU(string bucode, string[] codeExcept, int inclusive, int exclusive)
        {
            string buCondition = "";
            if (bucode != "" && bucode != null)
            {
                buCondition += string.Format(
                    @" and bucode like '%{0}%'", bucode.ToUpper());
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and bucode not in ({0})", this.FormatObjectCodesForSql(codeExcept));
            }

            PagerCondition pagerCondition = new PagerCondition(string.Format(
                @" select {0} from tblrmacoderule where 1=1 {1}{2}", "bucode,rmabillfl",
                buCondition, exceptCondition),
                "bucode,rmabillfl", inclusive, exclusive, true);
#if DEBUG
            Log.Info(pagerCondition.SQLText);
#endif

            return this.DataProvider.CustomQuery(typeof(RMACodeRule), pagerCondition);
        }

        public int QueryUnSelectedBUCount(string bucode, string[] codeExcept)
        {
            string buCondition = "";
            if (bucode != "" && bucode != null)
            {
                buCondition += string.Format(
                    @" and bucode like '%{0}%'", bucode.ToUpper());
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and bucode not in ({0})", this.FormatObjectCodesForSql(codeExcept));
            }

            SQLCondition sqlCondition = new SQLCondition(
                string.Format(
                @" select {0} from tblrmacoderule where 1=1 {1}{2}", "count(*)",
                buCondition, exceptCondition));

#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif

            return this.DataProvider.GetCount(sqlCondition);
        }

        public object[] QuerySelectedBU(string[] codes)
        {
            string codesCondition = string.Format(@" and bucode in ({0})", this.FormatObjectCodesForSql(codes));

            SQLCondition sqlCondition = new SQLCondition(
                string.Format(@" select {0} from tblrmacoderule where 1=1 {1}", "bucode,rmabillfl", codesCondition)
                );

#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(RMACodeRule), sqlCondition);
        }

        #endregion

        #region Department
        /* added by jessie lee, 2006/6/28 */

        public object[] QueryUnSelectedDepartment(string departmentcode, string[] codeExcept, int inclusive, int exclusive)
        {
            string departCondition = "";
            if (departmentcode != "" && departmentcode != null)
            {
                departCondition += string.Format(
                    @" and paramcode like '%{0}%'", departmentcode.ToUpper());
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and paramcode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }

            PagerCondition pagerCondition = new PagerCondition(string.Format(
                @" select {0} from tblsysparam where 1=1 {1}{2} and paramgroupcode = 'DEPARTMENT' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)),
                departCondition, exceptCondition),
                "paramcode", inclusive, exclusive, true);
#if DEBUG
            Log.Info(pagerCondition.SQLText);
#endif

            return this.DataProvider.CustomQuery(typeof(Parameter), pagerCondition);
        }

        public int QueryUnSelectedDepartmentCount(string departmentcode, string[] codeExcept)
        {
            string departCondition = "";
            if (departmentcode != "" && departmentcode != null)
            {
                departCondition += string.Format(
                    @" and paramcode like '%{0}%'", departmentcode.ToUpper());
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and paramcode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }

            SQLCondition sqlCondition = new SQLCondition(
                string.Format(
                @" select {0} from tblsysparam where 1=1 {1}{2}  and paramgroupcode = 'DEPARTMENT' ", "count(*)",
                departCondition, exceptCondition));

#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif

            return this.DataProvider.GetCount(sqlCondition);
        }

        public object[] QuerySelectedDepartment(string[] codes)
        {
            string codesCondition = string.Format(@" and paramcode in ({0})", this.FormatObjectCodesForSql(codes).ToUpper());

            SQLCondition sqlCondition = new SQLCondition(
                string.Format(@" select {0} from tblsysparam where 1=1 {1} and paramgroupcode = 'DEPARTMENT' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)), codesCondition)
                );

#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(Parameter), sqlCondition);
        }

        #endregion

        #region RMAReworkMO

        /// <summary>
        /// 返工工单选择
        /// </summary>
        /// <param name="reworkcode">返工需求单单号</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedReworkMO(string modelCode, string itemCode, string moCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                @"SELECT tblmo.*
					FROM tblmo
					WHERE motype in ( select PARAMCODE from TBLSYSPARAM where PARAMGROUPCODE='MOTYPE' and PARAMVALUE = '{0}' ) 
					AND mocode NOT IN (SELECT DISTINCT mocode
                                     FROM tblreworksheet
                                    WHERE mocode IS NOT NULL)" + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                BenQGuru.eMES.Web.Helper.MOType.MOTYPE_RMAREWORKMOTYPE);


            if (modelCode != null && modelCode != string.Empty)
            {
                sql += string.Format(@" AND itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ", modelCode.ToUpper());
            }

            if (itemCode != null && itemCode != string.Empty)
            {
                sql += string.Format(@" and  itemcode like '%{0}%'  ", itemCode.ToUpper());
            }

            if (moCode != null && moCode != string.Empty)
            {
                sql += string.Format(@" and mocode like '%{0}%'  ", moCode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblmo.mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(MO), new PagerCondition(sql, "tblmo.mocode", inclusive, exclusive));
        }


        /// <summary>
        /// 返工工单数量
        /// </summary>
        /// <param name="reworkcode">返工需求单单号</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public int QueryUnSelectedReworkMOCount(string modelCode, string itemCode, string moCode, string[] codeExcept)
        {
            string sql = string.Format(
                @"SELECT count(tblmo.mocode)
								FROM tblmo
					WHERE motype  in ( select PARAMCODE from TBLSYSPARAM where PARAMGROUPCODE='MOTYPE' and PARAMVALUE = '{0}' ) 
					AND mocode NOT IN (SELECT DISTINCT mocode
                                     FROM tblreworksheet
                                    WHERE mocode IS NOT NULL)" + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                BenQGuru.eMES.Web.Helper.MOType.MOTYPE_RMAREWORKMOTYPE);


            if (modelCode != null && modelCode != string.Empty)
            {
                sql += string.Format(@" AND itemcode in (select itemcode from tblmodel2item where modelcode like '%{0}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ", modelCode.ToUpper());
            }

            if (itemCode != null && itemCode != string.Empty)
            {
                sql += string.Format(@" and  itemcode like '%{0}%'  ", itemCode.ToUpper());
            }

            if (moCode != null && moCode != string.Empty)
            {
                sql += string.Format(@" and mocode like '%{0}%'  ", moCode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblmo.mocode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedReworkMO(string[] codes)
        {
            string sql = string.Format(
                @"SELECT tblmo.*
					FROM tblmo
					WHERE motype in ( select PARAMCODE from TBLSYSPARAM where PARAMGROUPCODE='MOTYPE' and PARAMVALUE = '{0}' ) 
					AND mocode NOT IN (SELECT DISTINCT mocode
                                     FROM tblreworksheet
                                    WHERE mocode IS NOT NULL)",
                BenQGuru.eMES.Web.Helper.MOType.MOTYPE_RMAREWORKMOTYPE);
            sql += string.Format(
                    " and tblmo.mocode in( {0} ) ",
                    FormatObjectCodesForSql(codes).ToUpper()
                    );
            return this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(sql));
        }

        #endregion

        #region RMABill

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="moCode"></param>
        /// <param name="codeExcept"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedRMABill(string modelCode, string itemCode, string moCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                @"SELECT distinct tblrmabill.rmabillcode, tblrmabill.cuscode FROM tblrmabill WHERE 1=1 ");


            if (modelCode != null && modelCode != string.Empty)
            {
                sql += string.Format(@" AND modelcode like '%{0}%' ", modelCode.ToUpper());
            }

            if (itemCode != null && itemCode != string.Empty)
            {
                sql += string.Format(@" and  itemcode like '%{0}%'  ", itemCode.ToUpper());
            }

            if (moCode != null && moCode != string.Empty)
            {
                sql += string.Format(@" and remocode like '%{0}%'  ", moCode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblrmabill.rmabillcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            sql = string.Format("select rmabillcode, cuscode from ({0})", sql);
            return this.DataProvider.CustomQuery(typeof(RMABill), new PagerCondition(sql, "rmabillcode", inclusive, exclusive));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="moCode"></param>
        /// <param name="codeExcept"></param>
        /// <returns></returns>
        public int QueryUnSelectedRMABillCount(string modelCode, string itemCode, string moCode, string[] codeExcept)
        {
            string sql = string.Format(
                @"SELECT distinct tblrmabill.rmabillcode, tblrmabill.cuscode FROM tblrmabill WHERE 1=1 ");


            if (modelCode != null && modelCode != string.Empty)
            {
                sql += string.Format(@" AND modelcode like '%{0}%' ", modelCode.ToUpper());
            }

            if (itemCode != null && itemCode != string.Empty)
            {
                sql += string.Format(@" and  itemcode like '%{0}%'  ", itemCode.ToUpper());
            }

            if (moCode != null && moCode != string.Empty)
            {
                sql += string.Format(@" and remocode like '%{0}%'  ", moCode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblrmabill.rmabillcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            sql = string.Format("select count(*) from ({0})", sql);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedRMABill(string[] codes)
        {
            string sql = string.Format(
                @"SELECT distinct tblrmabill.rmabillcode, tblrmabill.cuscode FROM tblrmabill WHERE 1=1 ");
            sql += string.Format(
                " and tblrmabill.rmabillcode in( {0} ) ",
                FormatObjectCodesForSql(codes).ToUpper());
            return this.DataProvider.CustomQuery(typeof(RMABill), new SQLCondition(sql));
        }

        #endregion

        #region Shelf 选择
        /// <summary>
        /// 选择没有被选中的Shelf
        /// </summary>
        /// <param name="ShelfNo">模糊查询,车号</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedShelf(string ShelfNo, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblshelf where ShelfNo like '%{1}%'  ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shelf)),
                ShelfNo.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and ShelfNo not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Shelf), new PagerCondition(sql, "ShelfNo", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Shelf的数量
        /// </summary>
        /// <param name="ShelfNo">模糊查询,车号</param>
        /// <param name="codeExcept">modelExcept数组中包含的Code将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedShelfCount(string ShelfNo, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(ShelfNo) from tblshelf where ShelfNo like '%{0}%'  ",
                ShelfNo.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and ShelfNo not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedShelf(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblshelf where ShelfNo in ({1}) order by ShelfNo",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shelf)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Shelf), new SQLCondition(sql));
        }

        #endregion

        #region Lot 选择
        /// <summary>
        /// 选择没有被选中的Item
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedLot(string lotno, string status, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql;
            if (status == string.Empty)
            {
                sql = "select {0} from tbllot where lotno like '%{1}%'";
            }
            else
            {
                sql = "select {0} from tbllot where lotno like '%{1}%' and lotstatus= '" + status + "'";
            }

            sql = string.Format(
                sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.OQC.OQCLot)),
                lotno.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and lotno not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.OQC.OQCLot), new PagerCondition(sql, "lotno", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedLotCount(string lotno, string status, string[] codeExcept)
        {
            string sql;
            if (status == string.Empty)
            {
                sql = "select count(lotno) from tbllot where lotno like '%{0}%'";
            }
            else
            {
                sql = "select count(lotno) from tbllot where lotno like '%{0}%' and lotstatus= '" + status + "'";
            }
            sql = string.Format(
                sql, lotno.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and lotno not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedLot(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tbllot where lotno in ({1}) order by lotno",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.OQC.OQCLot)),
                FormatObjectCodesForSql(codes)
                );
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.OQC.OQCLot), new SQLCondition(sql));
        }

        #endregion

        #region Factory
        /* added by jessie lee, 2006/8/03 */

        public object[] QueryUnSelectedFactory(string facCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string condition = "";
            if (facCode != "" && facCode != null)
            {
                condition += string.Format(
                    @" and faccode like '%{0}%'", facCode.ToUpper());
            }

            if (codeExcept != null && codeExcept.Length > 0)
            {
                condition += string.Format(
                    @" and faccode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }

            PagerCondition pagerCondition = new PagerCondition(string.Format(
                @" select {0} from tblfactory where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {1}", "faccode, facdesc", condition),
                "faccode", inclusive, exclusive, true);
#if DEBUG
            Log.Info(pagerCondition.SQLText);
#endif

            return this.DataProvider.CustomQuery(typeof(Factory), pagerCondition);
        }

        public int QueryUnSelectedFactoryCount(string facCode, string[] codeExcept)
        {
            string condition = "";
            if (facCode != "" && facCode != null)
            {
                condition += string.Format(
                    @" and faccode like '%{0}%'", facCode.ToUpper());
            }

            if (codeExcept != null && codeExcept.Length > 0)
            {
                condition += string.Format(
                    @" and faccode not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }

            SQLCondition sqlCondition = new SQLCondition(
                string.Format(
                @" select {0} from tblfactory where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {1}", "count(*)", condition));

#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif

            return this.DataProvider.GetCount(sqlCondition);
        }

        public object[] QuerySelectedFactory(string[] codes)
        {
            string codesCondition = string.Format(@" and faccode in ({0})", this.FormatObjectCodesForSql(codes).ToUpper());

            SQLCondition sqlCondition = new SQLCondition(
                string.Format(@" select {0} from tblfactory where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {1}", "faccode, facdesc", codesCondition)
                );

#if DEBUG
            Log.Info(sqlCondition.SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(Factory), sqlCondition);
        }

        #endregion

        #region 选择客户

        public object[] QuerySelectedCusItemCodeCheckList(string[] codes)
        {
            string codesCondition = string.Format(@" and CUSCODE in ({0})", this.FormatObjectCodesForSql(codes).ToUpper());

#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(@" select distinct {0} from TBLCITEMCODECL where 1=1 {1}", "CUSCODE", codesCondition)
                ).SQLText);
#endif
            return this.DataProvider.CustomQuery(
                typeof(CusItemCodeCheckList),
                new SQLCondition(
                string.Format(@" select distinct {0} from TBLCITEMCODECL where 1=1 {1}", "CUSCODE", codesCondition)
                ));
        }

        public object[] QueryUnCusItemCodeCheckList(string CUSCODE, string[] codeExcept, int inclusive, int exclusive)
        {
            string CUSCODECondition = "";
            if (CUSCODE != "" && CUSCODE != null)
            {
                CUSCODECondition += string.Format(
                    @" and upper(CUSCODE) like '%{0}%'", CUSCODE.ToUpper());
            }


            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and CUSCODE not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }
#if DEBUG
            Log.Info(
                new PagerCondition(
                string.Format(
                @" select distinct {0} from TBLCITEMCODECL where 1=1 {1}{2}", "CUSCODE",
                CUSCODECondition, exceptCondition),
                "CUSCODE", inclusive, exclusive, true).SQLText);
#endif

            return this.DataProvider.CustomQuery(
                typeof(CusItemCodeCheckList),
                new PagerCondition(
                string.Format(
                @" select distinct {0} from TBLCITEMCODECL where 1=1 {1}{2}", "CUSCODE",
                CUSCODECondition, exceptCondition),
                "CUSCODE", inclusive, exclusive, true));
        }

        public int QueryUnCusItemCodeCheckListCount(string CUSCODE, string[] codeExcept)
        {
            string CUSCODECondition = "";
            if (CUSCODE != "" && CUSCODE != null)
            {
                CUSCODECondition += string.Format(
                    @" and upper(CUSCODE) like '%{0}%'", CUSCODE.ToUpper());
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and CUSCODE not in ({0})", this.FormatObjectCodesForSql(codeExcept).ToUpper());
            }

#if DEBUG
            Log.Info(
                new SQLCondition(
                string.Format(
                @" select {0} from TBLCITEMCODECL where 1=1 {1}{2}", "count(distinct  CUSCODE)",
                CUSCODECondition, exceptCondition)).SQLText);
#endif

            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(
                @" select count(CUSCODE)  from (select {0}  from TBLCITEMCODECL where 1=1 {1}{2})", "distinct CUSCODE",
                CUSCODECondition, exceptCondition)));
        }
        #endregion

        #region Org

        public object[] QueryUnSelectedOrg(string[] orgIDExcept, int inclusive, int exclusive)
        {
            string orgIDCondition = "";
            if (orgIDExcept != null && orgIDExcept.Length > 0)
            {
                orgIDCondition = " AND orgid NOT IN (" + string.Join(",", orgIDExcept) + ") ";
            }
            return this.DataProvider.CustomQuery(typeof(Organization), new PagerCondition(string.Format("SELECT {0} FROM tblorg WHERE 1=1 {1} ORDER BY orgid ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Organization)), orgIDCondition), inclusive, exclusive, true));
        }


        public int QueryUnSelectedOrgCount(string[] orgIDExcept)
        {
            string orgIDCondition = "";
            if (orgIDExcept != null && orgIDExcept.Length > 0)
            {
                orgIDCondition = " AND orgid NOT IN (" + string.Join(",", orgIDExcept) + ") ";
            }

            return this.DataProvider.GetCount(new SQLCondition(string.Format("SELECT count(orgdesc) FROM tblorg WHERE 1=1 {0}", orgIDCondition)));
        }

        public object[] QuerySelectedOrg(string[] orgID)
        {
            string orgIDCondition = "";
            if (orgID != null && orgID.Length > 0)
            {
                orgIDCondition = " AND orgid IN (" + string.Join(",", orgID) + ") ";
            }
            else
            {
                orgIDCondition = " AND 1<>1 ";
            }
            return this.DataProvider.CustomQuery(typeof(Organization), new SQLCondition(string.Format("SELECT {0} FROM tblorg WHERE 1=1 {1}", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Organization)), orgIDCondition)));
        }

        #endregion

        #region 途程选择
        public object[] QueryUnSelectedRoute(string routeCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string strCondition = "";
            if (routeCode != "")
            {
                strCondition = string.Format(" and RouteCode like '%{0}%' ", routeCode);
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and RouteCode not in ({0})", this.FormatObjectCodesForSql(codeExcept));
            }
            return this.DataProvider.CustomQuery(
                typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                new PagerCondition(
                string.Format(
                @" select {0} from tblroute where 1=1 {1}{2}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.BaseSetting.Route)),
                strCondition, exceptCondition),
                "RouteCode", inclusive, exclusive, true));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedRouteCount(string routeCode, string[] codeExcept)
        {
            string strCondition = "";
            if (routeCode != "")
            {
                strCondition = string.Format(" and routeCode like '%{0}%' ", routeCode);
            }

            string exceptCondition = "";
            if (codeExcept != null && codeExcept.Length > 0)
            {
                exceptCondition += string.Format(
                    @" and routeCode not in ({0})", this.FormatObjectCodesForSql(codeExcept));
            }

            return this.DataProvider.GetCount(
                new SQLCondition(
                string.Format(
                @" select {0} from tblroute where 1=1 {1}{2}", "count(*)",
                strCondition, exceptCondition)));
        }

        public object[] QuerySelectedRoute(string[] codes)
        {
            string codesCondition = string.Format(@" and routeCode in ({0})", this.FormatObjectCodesForSql(codes));
            return this.DataProvider.CustomQuery(
                typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                new SQLCondition(
                string.Format(@" select {0} from tblroute where 1=1 {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.BaseSetting.Route)), codesCondition)
                ));
        }
        #endregion

        #region ErrorCode2OPRework 选择

        //处理split的参数组
        private string FormatMutiCodesForSql(string[] codeExcept)
        {
            if (codeExcept != null && codeExcept.Length > 0)
            {
                for (int i = 0; i < codeExcept.Length; i++)
                {
                    codeExcept[i] = "'" + codeExcept[i] + "'";
                }
                return string.Join(",", codeExcept);
            }
            return "('')";
        }


        /// <summary>
        /// 选择没有被选中的ErrorCode2OPRework
        /// </summary>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedErrorCode2OPRework(string errorCode, string errorCodeDesc, string errorGroupCode, string errorGroupCodeDesc, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = "SELECT t2.ecode AS ECODE, t2.ecdesc AS ECDESC, t3.ecgcode AS ECGCODE, t3.ecgdesc AS ECGDESC FROM tblecg2ec t1, tblec t2, tblecg t3 WHERE t1.ecode = t2.ecode and t1.ecgcode = t3.ecgcode";

            if (errorCode != null && errorCode.Length != 0)
            {
                sql = string.Format("{0} and t2.ecode LIKE '%{1}%'", sql, errorCode);
            }

            if (errorCodeDesc != null && errorCodeDesc.Length != 0)
            {
                sql = string.Format("{0} and t2.ecdesc LIKE '%{1}%'", sql, errorCodeDesc);
            }

            if (errorGroupCode != null && errorGroupCode.Length != 0)
            {
                sql = string.Format("{0} and t3.ecgcode LIKE '%{1}%'", sql, errorGroupCode);
            }

            if (errorGroupCodeDesc != null && errorGroupCodeDesc.Length != 0)
            {
                sql = string.Format("{0} and t3.ecgdesc LIKE '%{1}%'", sql, errorGroupCodeDesc);
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and t1.ecode not in ( {0} ) ",
                    FormatMutiCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.CustomQuery(typeof(ErrorGroup2CodeSelect), new PagerCondition(sql, "t1.ecode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的ErrorCode2OPRework的数量
        /// </summary>
        /// <returns></returns>
        public int QueryUnSelectedErrorCode2OPReworkCount(string errorCode, string errorCodeDesc, string errorGroupCode, string errorGroupCodeDesc, string[] codeExcept)
        {
            string sql = "SELECT COUNT(*) FROM tblecg2ec t1, tblec t2, tblecg t3 WHERE t1.ecode = t2.ecode and t1.ecgcode = t3.ecgcode";

            if (errorCode != null && errorCode.Length != 0)
            {
                sql = string.Format("{0} and t2.ecode LIKE '%{1}%'", sql, errorCode);
            }

            if (errorCodeDesc != null && errorCodeDesc.Length != 0)
            {
                sql = string.Format("{0} and t2.ecdesc LIKE '%{1}%'", sql, errorCodeDesc);
            }

            if (errorGroupCode != null && errorGroupCode.Length != 0)
            {
                sql = string.Format("{0} and t3.ecgcode LIKE '%{1}%'", sql, errorGroupCode);
            }

            if (errorGroupCodeDesc != null && errorGroupCodeDesc.Length != 0)
            {
                sql = string.Format("{0} and t3.ecgdesc LIKE '%{1}%'", sql, errorGroupCodeDesc);
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and t1.ecode not in ( {0} ) ",
                    FormatMutiCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        /// <summary>
        /// 得到被选中的ErrorCode2OPRework
        /// </summary>
        /// <returns></returns>
        public object[] QuerySelectedErrorCode2OPRework(string[] codeExcept)
        {
            string sql = "SELECT t2.ecode AS ECODE, t2.ecdesc AS ECDESC, t3.ecgcode AS ECGCODE, t3.ecgdesc AS ECGDESC FROM tblecg2ec t1, tblec t2, tblecg t3 WHERE t1.ecode = t2.ecode and t1.ecgcode = t3.ecgcode";

            sql += string.Format(
                " and t1.ecode in ( {0} ) ORDER BY t1.ecode",
                FormatMutiCodesForSql(codeExcept).ToUpper()
                );

            return this.DataProvider.CustomQuery(typeof(ErrorGroup2CodeSelect), new SQLCondition(sql));
        }

        #endregion

        #region mmodelcode

        public object[] QueryMmodelcode(string[] codes)
        {
            string sql = string.Format(
                @"select distinct mmodelcode  from tblmaterial where 1=1  and  mtype='itemtype_finishedproduct'  and  trim(mmodelcode)!=' '");
            sql += string.Format(" and mmodelcode in ({0}) order by mmodelcode ", FormatObjectCodesForSql(codes));
            return this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
        }

        public object[] QueryUNMmodelcode(string MmodelCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                @"select distinct tblmaterial.mmodelcode from tblmaterial where 1=1 "
                + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and mtype='itemtype_finishedproduct' and trim(mmodelcode)!=' ' ");
            if (MmodelCode.Length > 0)
            {
                sql += @" and mmodelcode like '%" + MmodelCode + "%' ";
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and mmodelcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            sql = string.Format("select mmodelcode from ({0}) ", sql);
            return this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new PagerCondition(sql, "mmodelcode", inclusive, exclusive));
        }

        public int QueryUNMmodelcodeCount(string MmodelCode, string[] codeExcept)
        {
            string sql = "select count(distinct mmodelcode) from tblmaterial where 1=1  " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + "and mtype='itemtype_finishedproduct' and trim(mmodelcode)!=' ' ";
            if (MmodelCode.Length > 0)
            {
                sql += " and mmodelcode like '%" + MmodelCode + "%'";
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and mmodelcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            sql += " order by mmodelcode";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region SYSPARAM
        public object[] QuerySYSPARAM(string[] codes)
        {
            string sql = string.Format(
                "select {0} from TBLSYSPARAM  where 1=1 and PARAMGROUPCODE='BIGLINEGROUP' and PARAMALIAS in ({1}) order by PARAMALIAS",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.BaseSetting.Parameter)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));
        }


        public object[] QueryUnSYSPARAM(string bingline, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from TBLSYSPARAM where 1=1 and PARAMGROUPCODE='BIGLINEGROUP' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)));
            if (bingline.Length > 0)
            {
                sql += " and upper(PARAMALIAS) like '%" + bingline.ToUpper() + "%'";
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and PARAMALIAS not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Parameter), new PagerCondition(sql, "PARAMALIAS", inclusive, exclusive));
        }

        public int QueryUnSYSPARAMCount(string bingline, string[] codeExcept)
        {
            string sql = string.Format(
               "select count(PARAMALIAS) from TBLSYSPARAM where 1=1 and PARAMGROUPCODE='BIGLINEGROUP' ",
               DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)));

            if (bingline.Length > 0)
            {
                sql += " and upper(PARAMALIAS) like '%" + bingline.ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and PARAMALIAS not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += " order by PARAMALIAS";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region machinetype
        public object[] QueryMmachinetype(string[] codes)
        {
            string sql = string.Format(
                @"select distinct MMACHINETYPE  from tblmaterial where 1=1 and  trim(MMACHINETYPE)!=' '");
            sql += string.Format(" and MMACHINETYPE in ({0}) order by MMACHINETYPE ", FormatObjectCodesForSql(codes).ToUpper());
            return this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
        }
        public object[] QueryUNMmachinetype(string mmachinetype, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                @"select distinct MMACHINETYPE from tblmaterial where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and trim(MMACHINETYPE)!=' ' ");
            if (mmachinetype.Length > 0)
            {
                sql += " and MMACHINETYPE like '%" + mmachinetype.ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and MMACHINETYPE not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql = string.Format("select MMACHINETYPE from ({0})", sql);
            return this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new PagerCondition(sql, "MMACHINETYPE", inclusive, exclusive));
        }
        public int QueryUNMmachinetypeCount(string mmachinetype, string[] codeExcept)
        {
            string sql = "select count(distinct MMACHINETYPE) from tblmaterial where 1=1  " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and trim(MMACHINETYPE)!=' ' ";
            if (mmachinetype.Length > 0)
            {
                sql += " and MMACHINETYPE like '%" + mmachinetype.ToUpper() + "%'";
            }
            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and MMACHINETYPE not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += " order by MMACHINETYPE";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region Vendor

        public object[] QuerySelectedVendorCode(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblvendor where 1=1 and vendorcode in ({1}) order by vendorcode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Vendor)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Vendor), new SQLCondition(sql));
        }

        public object[] QueryUNSelectVendorCode(string vendorcode, string vendorName, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format("select {0} from tblvendor where 1=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Vendor)));
            if (vendorcode.Length > 0)
            {
                sql += " and vendorcode like '%" + vendorcode.ToUpper() + "%'";
            }
            if (!string.IsNullOrEmpty(vendorName))
            {
                sql += " and vendorname like '%" + vendorName.ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and vendorcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Vendor), new PagerCondition(sql, "vendorcode,vendorname", inclusive, exclusive));
        }

        public int QueryUNSelectVendorCodeCount(string vendorcode, string vendorName, string[] codeExcept)
        {
            string sql = "select count(*) from tblvendor where 1=1";
            if (vendorcode.Length > 0)
            {
                sql += " and vendorcode like '%" + vendorcode.ToUpper() + "%'";
            }
            if (!string.IsNullOrEmpty(vendorName))
            {
                sql += " and vendorname like '%" + vendorName.ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and vendorcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region Duty

        public object[] QuerySelectedDutyCode(string[] dutycodes)
        {
            string sql = string.Format(
                "select {0} from tblduty where 1=1 and dutycode in ({1}) order by dutycode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Duty)),
                FormatObjectCodesForSql(dutycodes)
                );
            return this.DataProvider.CustomQuery(typeof(Duty), new SQLCondition(sql));
        }

        public object[] QueryUNSelectDutyCode(string dutycode, string dutydesc, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format("select {0} from tblduty where 1=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Duty)));
            if (dutycode.Length > 0)
            {
                sql += " and dutycode like '%" + dutycode.ToUpper() + "%'";
            }
            if (!string.IsNullOrEmpty(dutydesc))
            {
                sql += " and dutydesc like '%" + dutydesc.ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and dutycode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Duty), new PagerCondition(sql, "dutycode,dutydesc", inclusive, exclusive));
        }

        public int QueryUNSelectDutyCodeCount(string dutycode, string dutydesc, string[] codeExcept)
        {
            string sql = "select count(*) from tblduty where 1=1";
            if (dutycode.Length > 0)
            {
                sql += " and dutycode like '%" + dutycode.ToUpper() + "%'";
            }
            if (!string.IsNullOrEmpty(dutydesc))
            {
                sql += " and dutydesc like '%" + dutydesc.ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and dutycode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept)
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region productiontype

        public object[] QuerySelectedProductionType(string[] codeExcept)
        {
            if (codeExcept.Length == 0)
            {
                return null;
            }

            if (codeExcept.Length > 0)
            {
                string allString = "," + ProductionType.ProductionType_Claim
                    + "," + ProductionType.ProductionType_Mass
                    + "," + ProductionType.ProductionType_New
                    + "," + ProductionType.ProductionType_Try + ",";

                List<object> productiontypes = new List<object>();

                for (int i = 0; i < codeExcept.Length; i++)
                {
                    if (allString.IndexOf("," + codeExcept[i].ToLower() + ",") >= 0)
                    {
                        productiontypes.Add(codeExcept[i].ToLower());
                    }
                }

                return productiontypes.ToArray();
            }

            return null;
        }

        public object[] QueryUNSelectProductionType(string[] codeExcept, int inclusive, int exclusive)
        {
            ArrayList productiontypeList = new ArrayList();
            productiontypeList.Add(ProductionType.ProductionType_Claim);
            productiontypeList.Add(ProductionType.ProductionType_Mass);
            productiontypeList.Add(ProductionType.ProductionType_New);
            productiontypeList.Add(ProductionType.ProductionType_Try);

            if (codeExcept.Length > 0)
            {
                if (codeExcept.Length == 4)
                {
                    return null;
                }

                for (int i = 0; i < codeExcept.Length; i++)
                {
                    for (int j = 0; j < productiontypeList.Count; j++)
                    {
                        if (codeExcept[i].ToLower() == productiontypeList[j].ToString())
                        {
                            productiontypeList.RemoveAt(j);
                        }
                    }
                }
            }

            object[] productiontypes = new object[productiontypeList.Count];
            productiontypeList.CopyTo(productiontypes);
            return productiontypes;


        }

        public int QueryUNSelectProductionTypeCount(string[] codeExcept)
        {
            ArrayList productiontypeList = new ArrayList();
            productiontypeList.Add(ProductionType.ProductionType_Claim);
            productiontypeList.Add(ProductionType.ProductionType_Mass);
            productiontypeList.Add(ProductionType.ProductionType_New);
            productiontypeList.Add(ProductionType.ProductionType_Try);

            if (codeExcept.Length > 0)
            {
                if (codeExcept.Length == 4)
                {
                    return 0;
                }

                for (int i = 0; i < codeExcept.Length; i++)
                {
                    for (int j = 0; j < productiontypeList.Count; j++)
                    {
                        if (codeExcept[i].ToLower() == productiontypeList[j].ToString())
                        {
                            productiontypeList.RemoveAt(j);
                        }
                    }
                }
            }
            return productiontypeList.Count;
        }

        #endregion

        #region OQCLotType

        public object[] QuerySelectedOQCLotType(string[] codeExcept)
        {
            if (codeExcept.Length == 0)
            {
                return null;
            }

            if (codeExcept.Length > 0)
            {
                string allString = "," + OQCLotType.OQCLotType_Normal
                    + "," + OQCLotType.OQCLotType_ReDO
                    + "," + OQCLotType.OQCLotType_Split + ",";

                List<object> OQCLotTypes = new List<object>();

                for (int i = 0; i < codeExcept.Length; i++)
                {
                    if (allString.IndexOf("," + codeExcept[i].ToLower() + ",") >= 0)
                    {
                        OQCLotTypes.Add(codeExcept[i].ToLower());
                    }
                }

                return OQCLotTypes.ToArray();
            }

            return null;
        }

        public object[] QueryUNSelectOQCLotType(string[] codeExcept, int inclusive, int exclusive)
        {
            ArrayList OQCLotTypesList = new ArrayList();

            OQCLotTypesList.Add(OQCLotType.OQCLotType_Normal);
            OQCLotTypesList.Add(OQCLotType.OQCLotType_ReDO);
            OQCLotTypesList.Add(OQCLotType.OQCLotType_Split);

            if (codeExcept.Length > 0)
            {
                if (codeExcept.Length == 3)
                {
                    return null;
                }

                for (int i = 0; i < codeExcept.Length; i++)
                {
                    for (int j = 0; j < OQCLotTypesList.Count; j++)
                    {
                        if (codeExcept[i].ToLower() == OQCLotTypesList[j].ToString())
                        {
                            OQCLotTypesList.RemoveAt(j);
                        }
                    }
                }
            }

            object[] productiontypes = new object[OQCLotTypesList.Count];
            OQCLotTypesList.CopyTo(productiontypes);
            return productiontypes;


        }

        public int QueryUNSelectOQCLotTypeCount(string[] codeExcept)
        {
            ArrayList OQCLotTypesList = new ArrayList();

            OQCLotTypesList.Add(OQCLotType.OQCLotType_Normal);
            OQCLotTypesList.Add(OQCLotType.OQCLotType_ReDO);
            OQCLotTypesList.Add(OQCLotType.OQCLotType_Split);


            if (codeExcept.Length > 0)
            {
                if (codeExcept.Length == 4)
                {
                    return 0;
                }

                for (int i = 0; i < codeExcept.Length; i++)
                {
                    for (int j = 0; j < OQCLotTypesList.Count; j++)
                    {
                        if (codeExcept[i].ToLower() == OQCLotTypesList[j].ToString())
                        {
                            OQCLotTypesList.RemoveAt(j);
                        }
                    }
                }
            }
            return OQCLotTypesList.Count;
        }

        #endregion

        #region POMaterial

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

        public object[] QuerySelectedPOMaterial(string poNo, string[] codes)
        {
            string srmDBLink = GetSRMDBLink();
            string sql = string.Empty;
            sql += "SELECT DISTINCT od.itemcode AS mcode, tblmaterial.mdesc ";
            sql += "FROM orderdetail{0} od ";
            sql += "LEFT OUTER JOIN tblmaterial ON od.itemcode = tblmaterial.mcode ";
            sql += "AND od.palantcode = tblmaterial.orgid ";
            sql += "WHERE 1 = 1 ";
            sql += "AND od.orderno = '{1}' ";
            sql += "AND od.itemcode IN ({2}) ";
            sql = string.Format(sql, srmDBLink, poNo, FormatObjectCodesForSql(codes));

            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material), new SQLCondition(sql));
        }

        public object[] QueryUnSelectedPOMaterialByPO(string poNo, string materialCode, string materialDesc, string[] codeExcept, int inclusive, int exclusive)
        {
            poNo = poNo.Trim().ToUpper();
            materialCode = materialCode.Trim().ToUpper();
            materialDesc = materialDesc.Trim();

            if (poNo.Length <= 0)
            {
                return null;
            }

            string srmDBLink = GetSRMDBLink();
            string sql = string.Empty;
            sql += "SELECT DISTINCT od.itemcode AS mcode, tblmaterial.mdesc ";
            sql += "FROM orderdetail{0} od ";
            sql += "LEFT OUTER JOIN tblmaterial ON od.itemcode = tblmaterial.mcode ";
            sql += "AND od.palantcode = tblmaterial.orgid ";
            sql += "WHERE 1 = 1 ";
            sql += "AND od.orderno = '{1}' ";
            sql += "AND od.itemcode LIKE '{2}%' ";
            sql = string.Format(sql, srmDBLink, poNo, materialCode);

            if (materialDesc.Length > 0)
            {
                sql += string.Format("AND tblmaterial.mdesc LIKE '%{0}%' ", materialDesc);
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format("AND od.itemcode NOT IN ({0}) ", FormatObjectCodesForSql(codeExcept));
            }

            sql = string.Format("SELECT mcode, mdesc FROM ({0}) a ", sql);

            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material), new PagerCondition(sql, "mcode", inclusive, exclusive));

        }

        public int QueryUnSelectedPOMaterialByPOCount(string poNo, string materialCode, string materialDesc, string[] codeExcept)
        {
            poNo = poNo.Trim().ToUpper();
            materialCode = materialCode.Trim().ToUpper();
            materialDesc = materialDesc.Trim();

            if (poNo.Length <= 0)
            {
                return 0;
            }

            string srmDBLink = GetSRMDBLink();
            string sql = string.Empty;
            sql += "SELECT count(DISTINCT od.itemcode) ";
            sql += "FROM orderdetail{0} od ";
            sql += "LEFT OUTER JOIN tblmaterial ON od.itemcode = tblmaterial.mcode ";
            sql += "AND od.palantcode = tblmaterial.orgid ";
            sql += "WHERE 1 = 1 ";
            sql += "AND od.orderno = '{1}' ";
            sql += "AND od.itemcode LIKE '{2}%' ";
            sql = string.Format(sql, srmDBLink, poNo, materialCode);

            if (materialDesc.Length > 0)
            {
                sql += string.Format("AND tblmaterial.mdesc LIKE '%{0}%' ", materialDesc);
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format("AND od.itemcode NOT IN ({0}) ", FormatObjectCodesForSql(codeExcept));
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region MOMemo 选择

        public object[] QueryUnSelectedMOMemo(string moMemo, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "SELECT {0} FROM tblsysparam WHERE 1 = 1 AND paramgroupcode = 'MO_PRODUCT_TYPE' AND paramcode LIKE '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)),
                moMemo.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND paramcode NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.CustomQuery(typeof(Parameter), new PagerCondition(sql, "paramcode", inclusive, exclusive));
        }

        public int QueryUnSelectedMOMemoCount(string moMemo, string[] codeExcept)
        {
            string sql = string.Format(
                "SELECT COUNT(paramcode) FROM tblsysparam WHERE 1 = 1 AND paramgroupcode = 'MO_PRODUCT_TYPE' AND paramcode LIKE '%{0}%' ",
                moMemo.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " AND paramcode NOT IN( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedMOMemo(string[] codes)
        {
            string sql = string.Format(
                "SELECT {0} FROM tblsysparam WHERE 1 = 1 AND paramgroupcode = 'MO_PRODUCT_TYPE' AND paramcode in ({1}) ORDER BY paramcode ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));
        }

        #endregion

        #region ErrorCauseGroup 选择

        public object[] QueryUnSelectedErrorCauseGroup(string errorCauseGroupCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblecsg where 1=1 and ecsgcode like '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(ErrorCauseGroup)),
                errorCauseGroupCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and ecsgcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.CustomQuery(typeof(ErrorCauseGroup), new PagerCondition(sql, "ecsgcode", inclusive, exclusive));
        }

        public int QueryUnSelectedErrorCauseGroupCount(string errorCauseGroupCode, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(ecsgcode) from tblecsg where 1=1 and ecsgcode like '%{0}%' ",
                errorCauseGroupCode.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and ecsgcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedErrorCauseGroup(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblecsg where 1=1 and ecsgcode in ({1}) order by ecsgcode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(ErrorCauseGroup)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(ErrorCauseGroup), new SQLCondition(sql));
        }

        #endregion

        private string FormatObjectCodesForSql(string[] codes)
        {
            return "'" + string.Join("','", codes) + "'";
        }

        //bighai.wang 2009/02/11

        #region warehousestoragetype 选择
        /// <summary>
        /// 选择没有被选中的Item
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="codeExcept">CodeExcept数组中包含的Code将不会被显示</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedStorageType(string storagecode, string storagename, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblstorage where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and storagecode like '%{1}%' and storagename like '%{2}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)),
                storagecode.ToUpper(),
                storagename
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and storagecode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Storage), new PagerCondition(sql, "storagecode", inclusive, exclusive));
        }

        public object[] CQueryUnSelectedStorageType(string storagecode, string storagename, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblstorage where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and storagecode like '%{1}%'  ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)),
                storagecode.ToUpper()

                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and storagecode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Storage), new PagerCondition(sql, "storagecode", inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedStorageTypeCount(string storagecode, string storagename, string[] codeExcept)
        {


            string sql = string.Format(
              "select count(storagecode) from tblstorage where 1=1" + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and storagecode like '%{0}%' and storagename like '%{1}%' ", storagecode.ToUpper(), storagename);

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and storagecode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));


        }

        public object[] QuerySelectedStorageType(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblstorage where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and storagecode in ({1}) order by storagecode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
        }

        #endregion

        #region Storage 选择
        #region Storage 选择

        public object[] QueryUnSelectedStorage(string storage, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select distinct sapstorage from tblstorage where 1 = 1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and sapstorage like '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)),
                storage.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and sapstorage not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += " order by sapstorage ";

            return this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
        }

        public object[] QueryUnSelectedStorageCode(string storage, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select distinct storagecode,storagename from tblstorage where 1 = 1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and sapstorage like '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)),
                storage.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and storagecode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += " order by storagecode ";

            return this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
        }



        public int QueryUnSelectedStorageCount(string storage, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(distinct sapstorage) from tblstorage where 1 = 1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and sapstorage like '%{0}%' ",
                storage.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and sapstorage not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int QueryUnSelectedStorageCodeCount(string storage, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(*) from tblstorage where 1 = 1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and storagecode like '%{0}%' ",
                storage.ToUpper()
                );

            //if (codeExcept.Length > 0)
            //{
            //    sql += string.Format(
            //        " and storagecode not in( {0} ) ",
            //        FormatObjectCodesForSql(codeExcept).ToUpper()
            //        );
            //}

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedStorage(string[] codes)
        {
            string sql = string.Format(
                "select distinct sapstorage from tblstorage where 1 = 1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and sapstorage in ({1}) order by sapstorage",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
        }



        public object[] QueryUnSelectedStorageCode1(string storage, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select distinct storagecode,storagename from tblstorage where 1 = 1 and storagecode like '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)),
                storage.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and storagecode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += " order by storagecode ";

            return this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
        }


        public int QueryUnSelectedStorageCodeCount1(string storage, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(*) from tblstorage where 1 = 1 and storagecode like '%{0}%' ",
                storage.ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and storagecode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        #endregion

        #endregion

        #region CommonSingle和CommonMulti选择

        public object[] QueryUnselectedObject(SelectQueryInfo info, string code, string[] codeExcept, int inclusive, int exclusive)
        {
            if (info == null)
            {
                return null;
            }
            string sql = "SELECT {0} FROM {1} WHERE {2} LIKE '%{3}%' ";
            if (codeExcept.Length > 0)
            {
                sql += "AND {2} NOT IN ({4}) ";
            }

            sql = string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsString(info.DomainObjectType),
                info.GetTableName(), info.GetCodeTableFieldName(), code.ToUpper(), FormatObjectCodesForSql(codeExcept).ToUpper());

            return this.DataProvider.CustomQuery(info.DomainObjectType, new PagerCondition(sql, info.GetCodeTableFieldName(), inclusive, exclusive));
        }

        public int QueryUnselectedObjectCount(SelectQueryInfo info, string code, string[] codeExcept)
        {
            if (info == null)
            {
                return 0;
            }

            string sql = "SELECT COUNT(*) FROM {0} WHERE {1} LIKE '%{2}%' ";
            if (codeExcept.Length > 0)
            {
                sql += "AND {1} NOT IN ({3}) ";
            }

            sql = string.Format(sql, info.GetTableName(), info.GetCodeTableFieldName(), code.ToUpper(), FormatObjectCodesForSql(codeExcept).ToUpper());

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedObject(SelectQueryInfo info, string[] codeArray)
        {
            if (info == null)
            {
                return null;
            }

            string sql = "SELECT {0} FROM {1} WHERE {2} IN ({3}) ORDER BY {2} ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(info.DomainObjectType),
                info.GetTableName(), info.GetCodeTableFieldName(), FormatObjectCodesForSql(codeArray).ToUpper());

            return this.DataProvider.CustomQuery(info.DomainObjectType, new SQLCondition(sql));
        }

        #endregion

        #region Stack

        public object[] QueryUnSelectedStack(string stackCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblstack where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and stackCode like '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(SStack)),
                stackCode.Trim().ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and stackCode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(SStack), new PagerCondition(sql, "stackCode", inclusive, exclusive));
        }

        public int QueryUnSelectedStackCount(string stackCode, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(stackCode) from tblstack where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and stackCode like '%{0}%' ", stackCode.Trim().ToUpper());

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and stackCode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedStack(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblstack where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and stackCode in ({1}) order by stackCode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(SStack)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(SStack), new SQLCondition(sql));
        }

        #endregion

        #region Shift

        public object[] QueryUnSelectedShift(string shiftCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from tblshift where 1=1  and ShiftCode like '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)),
                shiftCode.Trim().ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and ShiftCode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(Shift), new PagerCondition(sql, "shiftCode", inclusive, exclusive));
        }

        public int QueryUnSelectedShiftCount(string shiftCode, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(ShiftCode) from tblshift where 1=1 and shiftCode like '%{0}%' ", shiftCode.Trim().ToUpper());

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and ShiftCode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedShift(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblshift where 1=1  and shiftCode in ({1}) order by shiftCode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Shift), new SQLCondition(sql));
        }

        #endregion

        #region ExceptionCode 选择
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeCode"></param>
        /// <param name="opCode"></param>
        /// <param name="codeExcept"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedExceptionCode(int date, string itemCode, string shiftCode, string ssCode, string exceptionCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = "SELECT SHIFTDATE,COMFIRMMEMO,SSCODE,EXCEPTIONDESC,SHIFTCODE,BEGINTIME,a.MTIME,a.MUSER,";
            sql += "  MEMO,ENDTIME,a.EXCEPTIONCODE,a.MDATE,SERIAL,ITEMCODE FROM tblexception a ";
            sql += "  LEFT JOIN tblexceptioncode b ON a.exceptioncode=b.exceptioncode WHERE 1=1 ";

            if (date > 0)
            {
                sql += " AND SHIFTDATE=" + date + "";
            }

            if (itemCode.Trim() != string.Empty)
            {
                sql += " AND itemcode like '%" + itemCode.Trim().ToUpper() + "%'";
            }

            if (shiftCode.Trim() != string.Empty)
            {
                sql += " AND shiftCode like '%" + shiftCode.Trim().ToUpper() + "%'";
            }

            if (ssCode.Trim() != string.Empty)
            {
                sql += " AND ssCode like '%" + ssCode.Trim().ToUpper() + "%'";
            }

            if (exceptionCode.Trim() != string.Empty)
            {
                sql += " AND a.exceptionCode like '%" + exceptionCode.Trim().ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and a.exceptionCode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            sql += " ORDER BY a.SERIAL,a.exceptionCode";

            return this.DataProvider.CustomQuery(typeof(ExceptionEventWithDescription), new PagerCondition(sql, inclusive, exclusive));
        }


        /// <summary>
        /// 得到没有被选中的 Item的数量
        /// </summary>
        /// <param name="modelCode">模糊查询,机种代码</param>
        /// <param name="itemCode">模糊查询,产品代码</param>
        /// <param name="moCode">模糊查询,工单代码</param>
        /// <param name="codeExcept">modelExcept数组中包含的ModelCode将不会被显示</param>
        /// <returns></returns>
        public int QueryUnSelectedExceptionCodeCount(int date, string itemCode, string shiftCode, string ssCode, string exceptionCode, string[] codeExcept)
        {
            string sql = "SELECT count(*) FROM tblexception WHERE 1=1 "; ;

            if (date > 0)
            {
                sql += " AND SHIFTDATE=" + date + "";
            }

            if (itemCode.Trim() != string.Empty)
            {
                sql += " AND itemcode like '%" + itemCode.Trim().ToUpper() + "%'";
            }

            if (shiftCode.Trim() != string.Empty)
            {
                sql += " AND shiftCode like '%" + shiftCode.Trim().ToUpper() + "%'";
            }

            if (ssCode.Trim() != string.Empty)
            {
                sql += " AND ssCode like '%" + ssCode.Trim().ToUpper() + "%'";
            }

            if (exceptionCode.Trim() != string.Empty)
            {
                sql += " AND exceptionCode like '%" + exceptionCode.Trim().ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and exceptionCode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedExceptionCode(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblexception where exceptionCode in ({1}) order by exceptionCode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(ExceptionEvent)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(ExceptionEvent), new SQLCondition(sql));
        }

        #endregion

        #region shiftCodeByStepsequence 选择
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeCode"></param>
        /// <param name="opCode"></param>
        /// <param name="codeExcept"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryUnSelectedShiftCodeByStepsequence(string shiftCode, string ssCode, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = " SELECT SHIFTCODE,SHIFTDESC from (SELECT DISTINCT b.shiftcode,b.shiftdesc FROM tblss a ,tblshift b WHERE a.shifttypecode=b.shifttypecode ";

            if (shiftCode.Trim() != string.Empty)
            {
                sql += " AND b.shiftCode like '%" + shiftCode.Trim().ToUpper() + "%'";
            }

            if (ssCode.Trim() != string.Empty)
            {
                sql += " AND a.ssCode like '%" + ssCode.Trim().ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and b.shiftCode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            sql += " ORDER BY b.shiftCode)";

            return this.DataProvider.CustomQuery(typeof(Shift), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryUnSelectedShiftCodeByStepsequenceCount(string shiftCode, string ssCode, string[] codeExcept)
        {
            string sql = "SELECT count(DISTINCT b.shiftcode) FROM tblss a ,tblshift b WHERE a.shifttypecode=b.shifttypecode "; ;

            if (shiftCode.Trim() != string.Empty)
            {
                sql += " AND b.shiftCode like '%" + shiftCode.Trim().ToUpper() + "%'";
            }

            if (ssCode.Trim() != string.Empty)
            {
                sql += " AND a.ssCode like '%" + ssCode.Trim().ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and b.shiftCode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedShiftCodeByStepsequence(string[] codes)
        {
            string sql = string.Format(
                "select DISTINCT b.shiftcode,b.shiftdesc from TBLLINE2CREW blss a ,tblshift b WHERE a.shifttypecode=b.shifttypecode and  b.shiftCode in ({1}) order by b.shiftCode",
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(Shift), new SQLCondition(sql));
        }

        #endregion

        #region TryCreateUser

        public object[] QueryUnSelectedTryCreateUser(string createUser, string userName, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "SELECT DISTINCT A.CUSER, B.USERNAME  FROM TBLTRY A  LEFT JOIN TBLUSER B ON A.CUSER = B.USERCODE where 1=1 and A.CUSER  like '%{0}%' AND B.USERNAME LIKE '%{1}%'  ",
                createUser.Trim().ToUpper(),
                userName.Trim().ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and A.CUSER not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            sql += " ORDER BY A.CUSER";

            sql = "select CUSER,USERNAME from (" + sql + ")";
            return this.DataProvider.CustomQuery(typeof(TryAndItemDesc), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryUnSelectedTryCreateUserCount(string createUser, string userName, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(CUSER) from (SELECT DISTINCT A.CUSER, B.USERNAME  FROM TBLTRY A  LEFT JOIN TBLUSER B ON A.CUSER = B.USERCODE where 1=1 and A.CUSER  like '%{0}%' AND B.USERNAME LIKE '%{1}%' ",
                createUser.Trim().ToUpper(),
                userName.Trim().ToUpper());

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and A.CUSER not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedTryCreateUser(string[] codes)
        {
            string sql = string.Format(
                "select DISTINCT A.CUSER, B.USERNAME  FROM TBLTRY A  LEFT JOIN TBLUSER B ON A.CUSER = B.USERCODE  where 1=1  and A.CUSER in ({0}) order by A.CUSER",
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(TryAndItemDesc), new SQLCondition(sql));
        }

        #endregion

        #region Inspector

        public object[] QueryUnSelectedInspector(string createUser, string userName, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "SELECT DISTINCT  Inspector,username FROM TBLASNIQC LEFT JOIN tbluser ON TBLASNIQC.Inspector=tbluser.usercode  where 1=1 and Inspector  like '%{0}%' AND username LIKE '%{1}%'  ",
                createUser.Trim().ToUpper(),
                userName.Trim().ToUpper()
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and Inspector not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            sql += " ORDER  BY Inspector";

            sql = "select Inspector,username from (" + sql + ")";
            return this.DataProvider.CustomQuery(typeof(IQCHeadWithInspectorName), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryUnSelectedInspectorCount(string createUser, string userName, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(Inspector) from (SELECT DISTINCT  Inspector,username FROM TBLASNIQC LEFT JOIN tbluser ON TBLASNIQC.Inspector=tbluser.usercode  where 1=1 and Inspector  like '%{0}%' AND USERNAME LIKE '%{1}%' ",
                createUser.Trim().ToUpper(),
                userName.Trim().ToUpper());

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and Inspector not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedInspector(string[] codes)
        {
            string sql = string.Format(
                "SELECT DISTINCT  Inspector,username FROM TBLASNIQC LEFT JOIN tbluser ON TBLASNIQC.Inspector=tbluser.usercode  where 1=1  and Inspector in ({0}) order by Inspector",
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(IQCHeadWithInspectorName), new SQLCondition(sql));
        }

        #endregion

        #region ErrorCodeWithGroup 不良代码与代码组

        public object[] QueryUnSelectedErrorCodeWithGroup(string errorCode, string errorCodeDesc, string errorCodeGroup, string errorCodeGrouDesc, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = "SELECT DISTINCT TBLEC.ECODE, TBLEC.ECDESC  FROM TBLEC, TBLECG, TBLECG2EC WHERE TBLEC.ECODE = TBLECG2EC.ECODE";
            sql += " AND TBLECG.ECGCODE = TBLECG2EC.ECGCODE   AND TBLEC.ECODE LIKE '%" + errorCode.Trim().ToUpper() + "%'   AND TBLEC.ECDESC LIKE '%" + errorCodeDesc.Trim().ToUpper() + "%'";
            sql += " AND TBLECG.ECGCODE LIKE '%" + errorCodeGroup.Trim().ToUpper() + "%'   AND TBLECG.ECGDESC LIKE '%" + errorCodeGrouDesc.Trim().ToUpper() + "%' ";

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblec.ecode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            sql += " ORDER  BY tblec.ecode";

            sql = "select ecode,ecdesc from (" + sql + ")";
            return this.DataProvider.CustomQuery(typeof(ErrorCodeA), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryUnSelectedErrorCodeWithGroupCount(string errorCode, string errorCodeDesc, string errorCodeGroup, string errorCodeGrouDesc, string[] codeExcept)
        {
            string sql = "select count(ecode) from (SELECT DISTINCT TBLEC.ECODE, TBLEC.ECDESC  FROM TBLEC, TBLECG, TBLECG2EC WHERE TBLEC.ECODE = TBLECG2EC.ECODE ";
            sql += " AND TBLECG.ECGCODE = TBLECG2EC.ECGCODE   AND TBLEC.ECODE LIKE '%" + errorCode.Trim().ToUpper() + "%'   AND TBLEC.ECDESC LIKE '%" + errorCodeDesc.Trim().ToUpper() + "%'";
            sql += " AND TBLECG.ECGCODE LIKE '%" + errorCodeGroup.Trim().ToUpper() + "%'   AND TBLECG.ECGDESC LIKE '%" + errorCodeGrouDesc.Trim().ToUpper() + "%' ";

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblec.ecode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedErrorCodeWithGroup(string[] codes)
        {
            string sql = string.Format(
                "SELECT DISTINCT tblec.ecode, tblec.ecdesc FROM tblec, tblecg, tblecg2ec WHERE tblec.ecode = tblecg2ec.ecode AND tblecg.ecgcode = tblecg2ec.ecgcode  and tblec.ecode in ({0}) order by tblec.ecode",
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(ErrorCodeA), new SQLCondition(sql));
        }

        #endregion

        #region ErrorCauseWithErrorCauseGroup 不良原因与不良原因组

        public object[] QueryUnSelectedErrorCauseWithErrorCauseGroup(string errorCause, string errorCauseDesc, string errorCauseGroup, string errorCauseGrouDesc, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = "SELECT DISTINCT tblecs.ecscode,tblecs.ecsdesc  FROM tblecs, tblecsg, tblecsg2ecs";
            sql += " WHERE tblecs.ecscode = tblecsg2ecs.ecscode  AND tblecsg.ecsgcode = tblecsg2ecs.ecsgcode";
            sql += "   AND tblecs.ecscode LIKE '%" + errorCause.Trim().ToUpper() + "%'   AND tblecs.ecsdesc LIKE '%" + errorCauseDesc.Trim().ToUpper() + "%'";
            sql += " AND tblecsg.ecsgcode LIKE '%" + errorCauseGroup.Trim().ToUpper() + "%'   AND tblecsg.ecsgdesc LIKE '%" + errorCauseGrouDesc.Trim().ToUpper() + "%' ";

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblecs.ecscode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }

            sql += " ORDER  BY tblecs.ecscode";

            sql = "select ecscode,ecsdesc from (" + sql + ")";
            return this.DataProvider.CustomQuery(typeof(ErrorCause), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryUnSelectedErrorCauseWithErrorCauseGroupCount(string errorCause, string errorCauseDesc, string errorCauseGroup, string errorCauseGrouDesc, string[] codeExcept)
        {
            string sql = "select count(ecscode) from (SELECT DISTINCT tblecs.ecscode,tblecs.ecsdesc  FROM tblecs, tblecsg, tblecsg2ecs ";
            sql += " WHERE tblecs.ecscode = tblecsg2ecs.ecscode  AND tblecsg.ecsgcode = tblecsg2ecs.ecsgcode";
            sql += "   AND tblecs.ecscode LIKE '%" + errorCause.Trim().ToUpper() + "%'   AND tblecs.ecsdesc LIKE '%" + errorCauseDesc.Trim().ToUpper() + "%'";
            sql += " AND tblecsg.ecsgcode LIKE '%" + errorCauseGroup.Trim().ToUpper() + "%'   AND tblecsg.ecsgdesc LIKE '%" + errorCauseGrouDesc.Trim().ToUpper() + "%' ";

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and tblecs.ecscode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += ")";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedErrorCauseWithErrorCauseGroup(string[] codes)
        {
            string sql = string.Format(
                "SELECT DISTINCT tblecs.ecscode,tblecs.ecsdesc FROM tblecs, tblecsg, tblecsg2ecs WHERE tblecs.ecscode = tblecsg2ecs.ecscode  AND tblecsg.ecsgcode = tblecsg2ecs.ecsgcode  and tblecs.ecscode in ({0}) order by tblecs.ecscode",
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(ErrorCause), new SQLCondition(sql));
        }

        #endregion

        #region OQCCheckItem
        public object[] QueryUnSelectedCheckItem(string checkGroupCode, string ckItemCode, string ckItemName, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = "select b.* from tbloqcckgroup2list a inner join tbloqccklist b on a.ckitemcode = b.ckitemcode where 1=1 ";
            if (checkGroupCode.Length > 0)
            {
                sql += " and a.ckgroup = '" + checkGroupCode.ToUpper() + "'";
            }
            if (ckItemCode.Length > 0)
            {
                sql += " and b.ckitemcode like '%" + ckItemCode.ToUpper() + "%'";
            }
            if (ckItemName.Length > 0)
            {
                sql += " and b.ckitemname like '%" + ckItemName.ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and b.ckitemcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.OQC.OQCCheckList), new PagerCondition(sql, "b.ckitemcode", inclusive, exclusive));
        }


        public int QueryUnSelectedCheckItemCount(string checkGroupCode, string ckItemCode, string ckItemName, string[] codeExcept)
        {
            string sql = "select count(*) from tbloqcckgroup2list a inner join tbloqccklist b on a.ckitemcode = b.ckitemcode where 1=1  ";
            if (checkGroupCode.Length > 0)
            {
                sql += " and a.ckgroup = '" + checkGroupCode.ToUpper() + "'";
            }
            if (ckItemCode.Length > 0)
            {
                sql += " and b.ckitemcode like '%" + ckItemCode.ToUpper() + "%'";
            }
            if (ckItemName.Length > 0)
            {
                sql += " and b.ckitemname like '%" + ckItemName.ToUpper() + "%'";
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and b.ckitemcode not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedCheckItem(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tbloqccklist where 1=1 and ckitemcode in ({1}) order by ckitemcode",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.OQC.OQCCheckList)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.OQC.OQCCheckList), new SQLCondition(sql));
        }
        #endregion

        #region Transfer
        public object[] QueryUnSelectedTransfer(string transferNo, string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format(
                "select {0} from TBLINVTRANSFER where 1=1 and TRANSFERNO like '%{1}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.Warehouse.InvTransfer)),
                transferNo
                );

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and TRANSFERNO not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Warehouse.InvTransfer), new PagerCondition(sql, "TRANSFERNO", inclusive, exclusive));
        }


        public int QueryUnSelectedTransferCount(string transferNo, string[] codeExcept)
        {
            string sql = string.Format(
                "select count(*) from TBLINVTRANSFER where 1=1 and TRANSFERNO like '%{0}%' ", transferNo);

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and TRANSFERNO not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySelectedTransfer(string[] codes)
        {
            string sql = string.Format(
                "select {0} from TBLINVTRANSFER where 1=1 and TRANSFERNO in ({1}) order by TRANSFERNO",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.Warehouse.InvTransfer)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Warehouse.InvTransfer), new SQLCondition(sql));
        }
        #endregion


        #region ASN 号
        public object[] QuerySelectedASN(string[] codes)
        {
            string sql = string.Format(
                "select {0} from tblasn where 1=1  and stno in ({1}) and  Direct_Flag = 'Y' and status = 'Close' order by stno",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(ASN)),
                FormatObjectCodesForSql(codes).ToUpper()
                );
            return this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql));
        }


        public object[] QueryUnSelectedASN(string asnCode, string vendorCode, string storageCode,
            string[] codeExcept, int inclusive, int exclusive)
        {
            string sql = string.Format("select a.*  from tblasn a where 1=1 ");
            //+ "and a.stno like '%{0}%'   and a.VENDORCODE like '%{1}%' and a.StorageCode  like '%{2}%'  ",
            //asnCode.ToUpper(),
            //vendorCode.ToUpper(),
            //storageCode
            //  and a.orgid={3}    GlobalVariables.CurrentOrganizations.First().OrganizationID

            if (!string.IsNullOrEmpty(asnCode))
            {
                sql += string.Format(" AND  a.stno LIKE '%{0}%'", asnCode.ToUpper());
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {
                sql += string.Format(" AND  a.VENDORCODE LIKE '%{0}%'", vendorCode.ToUpper());
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND  a.storageCode LIKE '%{0}%'", storageCode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and a.stno not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += string.Format(" and a.Direct_Flag = 'Y' and a.status = 'Close' ");
            return this.DataProvider.CustomQuery(typeof(ASN), new PagerCondition(sql, "a.STNO", inclusive, exclusive));
        }

        public int QueryUnSelectedASNCount(string asnCode, string vendorCode, string storageCode, string[] codeExcept)
        {
            string sql = string.Format(@"select count(a.stno) from tblasn a where 1=1  ");
            //      and a.stno like '%{0}%'   and a.VENDORCODE like '%{1}%' and a.StorageCode  like '%{2}%'   ",
            // asnCode.ToUpper(),
            //vendorCode.ToUpper(),
            //storageCode
            // GlobalVariables.CurrentOrganizations.First().OrganizationID

            if (!string.IsNullOrEmpty(asnCode))
            {
                sql += string.Format(" AND  a.stno LIKE '%{0}%'", asnCode.ToUpper());
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {
                sql += string.Format(" AND  a.VENDORCODE LIKE '%{0}%'", vendorCode.ToUpper());
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND  a.storageCode LIKE '%{0}%'", storageCode.ToUpper());
            }

            if (codeExcept.Length > 0)
            {
                sql += string.Format(
                    " and a.stno not in( {0} ) ",
                    FormatObjectCodesForSql(codeExcept).ToUpper()
                    );
            }
            sql += string.Format(" and a.Direct_Flag = 'Y' and a.status = 'Close' ");
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public object[] QueryUnSelectedStorageCode(string storageCode, int inclusive, int exclusive)
        {

            string sql = "SELECT STORAGECODE,STORAGENAME FROM TBLSTORAGE A WHERE 1=1";
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND  A.STORAGECODE LIKE '%{0}%'", storageCode.ToUpper());
            }


            return this.DataProvider.CustomQuery(typeof(Storage), new PagerCondition(sql, "A.storageCode", inclusive, exclusive));
        }

        public int QueryUnSelectedStorageCodeCount(string storageCode)
        {
            string sql = "SELECT count(*) FROM TBLSTORAGE A WHERE 1=1 ";
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND  A.STORAGECODE LIKE '%{0}%'", storageCode.ToUpper());
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion
    }
}
