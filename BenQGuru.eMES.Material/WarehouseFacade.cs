using System;
using System.Data;
using System.Collections;
using System.Text;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Web.Helper;
using System.Collections.Generic;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.BaseSetting;


namespace BenQGuru.eMES.Material
{
    /// <summary>
    /// WarehouseFacade 的摘要说明。
    /// 文件名:		WarehouseFacade.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
    /// 创建日期:	2005-7-28 16:37:09
    /// 修改人:
    /// 修改日期:
    /// 描 述:	
    /// 版 本:	
    /// </summary>
    public class WarehouseFacade : MarshalByRefObject
    {
        /// <summary>
        /// 维修换料
        /// </summary>
        public const string ReplaceMaterial = "ReplaceMaterial";

        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public WarehouseFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }


        public WarehouseFacade(IDomainDataProvider domainDataProvider)
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

        #region Factory
        /// <summary>
        /// 工厂
        /// </summary>
        public Factory CreateNewFactory()
        {
            return new Factory();
        }

        public void AddFactory(Factory factory)
        {
            this._helper.AddDomainObject(factory);
        }

        public void UpdateFactory(Factory factory)
        {
            this._helper.UpdateDomainObject(factory);
        }

        public void DeleteFactory(Factory factory)
        {
            if (IsExist(typeof(Warehouse), new string[] { "FactoryCode" }, new object[] { factory.FactoryCode }))
            {
                throw new Exception(string.Format("$Delete_Factory_Error_UseInWarehosue [$FactoryCode={0}]", factory.FactoryCode));
            }
            this._helper.DeleteDomainObject(factory);
        }

        public void DeleteFactory(Factory[] factory)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < factory.Length; i++)
                {
                    this.DeleteFactory(factory[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message);
            }
        }

        public object GetFactory(string factoryCode)
        {
            return this.DataProvider.CustomSearch(typeof(Factory), new object[] { factoryCode });
        }

        /// <summary>
        /// ** 功能描述:	查询Factory的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="factoryCode">FactoryCode，模糊查询</param>
        /// <returns> Factory的总记录数</returns>
        public int QueryFactoryCount(string factoryCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLFACTORY where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and FACCODE like '{0}%' ", factoryCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Factory
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="factoryCode">FactoryCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// 		/// <param name="exclusive">结束行数</param>
        /// <returns> Factory数组</returns>
        public object[] QueryFactory(string factoryCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Factory), new PagerCondition(string.Format("select {0} from TBLFACTORY where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and FACCODE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Factory)), factoryCode), "FACCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Factory
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Factory的总记录数</returns>
        public object[] GetAllFactory()
        {
            return this.DataProvider.CustomQuery(typeof(Factory), new SQLCondition(string.Format("select {0} from TBLFACTORY where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by FACCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Factory)))));
        }

        //Bighai.wang 2009/2/10 

        public object[] GetAllinvperiodGroup()
        {
            //return this.DataProvider.CustomQuery(typeof(InvPeriod), new SQLCondition(string.Format("select distinct peiodgroup from tblinvperiod t where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + "  order by t.peiodgroup ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvPeriod)))));

            return this.DataProvider.CustomQuery(typeof(InvPeriod), new SQLCondition(string.Format("select distinct peiodgroup from tblinvperiod t order by t.peiodgroup", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvPeriod)))));
        }

        //public object[] GetAllFirstClass()
        //{
        //    //return this.DataProvider.CustomQuery(typeof(InvPeriod), new SQLCondition(string.Format("select distinct peiodgroup from tblinvperiod t where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + "  order by t.peiodgroup ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvPeriod)))));

        //    return this.DataProvider.CustomQuery(typeof(ItemClass), new SQLCondition(string.Format("select distinct firstclass from tblitemclass t order by t.firstclass", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemClass)))));
        //}
        ////


        #endregion

        #region MOStock
        /// <summary>
        /// 工厂用料统计
        /// </summary>
        public MOStock CreateNewMOStock()
        {
            return new MOStock();
        }

        public void AddMOStock(MOStock mOStock)
        {
            this._helper.AddDomainObject(mOStock);
        }

        public void UpdateMOStock(MOStock mOStock)
        {
            this._helper.UpdateDomainObject(mOStock);
        }

        public void DeleteMOStock(MOStock mOStock)
        {
            this._helper.DeleteDomainObject(mOStock);
        }

        public void DeleteMOStock(MOStock[] mOStock)
        {
            this._helper.DeleteDomainObject(mOStock);
        }

        public object GetMOStock(string itemCode, string mOCode)
        {
            return this.DataProvider.CustomSearch(typeof(MOStock), new object[] { itemCode, mOCode });
        }

        /// <summary>
        /// ** 功能描述:	查询MOStock的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <returns> MOStock的总记录数</returns>
        public int QueryMOStockCount(string itemCode, string mOCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMOSTOCK where 1=1 and ITEMCODE like '{0}%'  and MOCODE like '{1}%' ", itemCode, mOCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询MOStock
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> MOStock数组</returns>
        public object[] QueryMOStock(string itemCode, string mOCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(MOStock), new PagerCondition(string.Format("select {0} from TBLMOSTOCK where 1=1 and ITEMCODE like '{1}%'  and MOCODE like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MOStock)), itemCode, mOCode), "ITEMCODE,MOCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的MOStock
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>MOStock的总记录数</returns>
        public object[] GetAllMOStock()
        {
            return this.DataProvider.CustomQuery(typeof(MOStock), new SQLCondition(string.Format("select {0} from TBLMOSTOCK order by ITEMCODE,MOCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MOStock)))));
        }

        class CacheSequence
        {
            public const string MO = "1";
            public const string WHS = "2";
        }
        private Hashtable _cacheSQL = new Hashtable();

        //执行缓存的ＳＱＬ
        public void ExecCacheSQL()
        {
            ArrayList keys = new ArrayList(this._cacheSQL.Keys);
            keys.Sort();
            foreach (String key in keys)
            {
                string SQL = this._cacheSQL[key].ToString();
                this.DataProvider.CustomExecute(new SQLCondition(SQL));
            }

            this._cacheSQL.Clear();
        }

        /// <summary>
        /// 在单据做交易时，更新工单耗用量,//将ＳＱＬ语句缓存起来执行，防止死锁
        /// </summary>
        private void UpdateMOStockInTrans(string moCode, string itemCode, string attributeName, decimal qty, string operation)
        {
            //读取原始内容
            object obj = this.GetMOStock(itemCode, moCode);
            MOStock stock = obj as MOStock;
            bool bIsNew = false;
            if (stock == null)
            {
                stock = this.CreateNewMOStock();
                stock.MOCode = moCode;
                stock.ItemCode = itemCode;
                stock.MaintainUser = " ";
                bIsNew = true;
            }

            //保持
            if (bIsNew)
            {
                //计算新值
                object objvalue = BenQGuru.eMES.Common.Domain.DomainObjectUtility.GetValue(stock, attributeName, new object[] { });
                decimal d = 0;
                try
                {
                    d = decimal.Parse(objvalue.ToString());
                }
                catch
                { }
                if (operation.ToLower() == "add")
                    d = d + qty;
                else if (operation.ToLower() == "sub")
                {
                    d = d - qty;
                }
                else
                    return;
                BenQGuru.eMES.Common.Domain.DomainObjectUtility.SetValue(stock, attributeName, d);

                this.AddMOStock(stock);
            }
            else if (qty != 0) // joe song 20060222 直接使用ＳＱＬ语句更新数量，避免实体类的方式导致的并发数量错误　
            {
                string field = BenQGuru.eMES.Common.Domain.DomainObjectUtility.GetFieldName(typeof(MOStock), attributeName);
                string sql = "update tblmostock set " + field + "=" + field;
                if (operation.ToLower() == "add")
                {
                    sql += " + ";
                }
                else if (operation.ToLower() == "sub")
                {
                    sql += " - ";
                }
                else
                    return;

                sql += qty;
                sql = sql + " where mocode ='" + moCode + "'";
                sql = sql + " and itemcode='" + itemCode + "'";

                //this.DataProvider.CustomExecute(new SQLCondition(sql));
                //为防止死锁将ＳＱＬ语句缓存起来，最后调用 ExecCacheSQL 一起执行 joe song 20060227
                this._cacheSQL.Add(CacheSequence.MO + moCode + itemCode + Guid.NewGuid().ToString(),
                                    sql);
            }

        }

        #endregion

        #region TransactionType
        /// <summary>
        /// 交易类型
        /// </summary>
        public TransactionType CreateNewTransactionType()
        {
            return new TransactionType();
        }

        public void AddTransactionType(TransactionType transactionType)
        {
            //检查单据名称是否重复
            object[] obj = this.DataProvider.CustomSearch(typeof(TransactionType), new string[] { "TransactionTypeName" }, new object[] { transactionType.TransactionTypeName });
            if (obj != null && (obj.Length > 1 || ((TransactionType)obj[0]).TransactionTypeCode != transactionType.TransactionTypeCode))
            {
                ExceptionManager.Raise(transactionType.GetType(), "$Error_TransactionType_Check_CodeName");
                return;
            }
            transactionType.IsInit = "0";
            this._helper.AddDomainObject(transactionType);
        }

        public void UpdateTransactionType(TransactionType transactionType)
        {
            //检查单据名称是否重复
            object[] obj = this.DataProvider.CustomSearch(typeof(TransactionType), new string[] { "TransactionTypeName" }, new object[] { transactionType.TransactionTypeName });
            if (obj != null && (obj.Length > 1 || ((TransactionType)obj[0]).TransactionTypeCode != transactionType.TransactionTypeCode))
            {
                ExceptionManager.Raise(transactionType.GetType(), "$Error_TransactionType_Check_CodeName");
                return;
            }
            this._helper.UpdateDomainObject(transactionType);
        }
        public void UpdateTransactionTypeInit(string typeCode, string isInit)
        {
            string strSql = string.Format("update TBLTRANSTYPE set ISINIT='{0}' where TRANSTYPECODE='{1}' ", isInit, typeCode);
            this.DataProvider.CustomExecute(new SQLCondition(strSql));
        }

        public void DeleteTransactionType(TransactionType transactionType)
        {
            if (IsExist(typeof(WarehouseTicket), new string[] { "TransactionTypeCode" }, new object[] { transactionType.TransactionTypeCode }))
            {
                throw new Exception(string.Format("$Delete_TransactionType_Error_UseInTicket [$TransactionTypeCode={0}]", transactionType.TransactionTypeCode));
            }
            this._helper.DeleteDomainObject(transactionType);
        }

        /// <summary>
        /// 根据MOCode SSCode读取对应仓库
        /// </summary>
        /// <returns>Warehouse</returns>
        public Warehouse GetWarehouseByMoSS(string moCode, string sscode)
        {
            //读工厂代码
            BenQGuru.eMES.MOModel.MOFacade mof = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
            object obj = mof.GetMO(moCode);
            string strFactoryCode = ((MO)obj).Factory;
            //读仓库
            string strSSCode = sscode;
            object[] objwh = this.DataProvider.CustomSearch(typeof(Warehouse2StepSequence), new string[] { "StepSequenceCode", "FactoryCode" }, new object[] { strSSCode, strFactoryCode });
            if (objwh == null)
                return null;
            Warehouse2StepSequence w2ss = (Warehouse2StepSequence)objwh[0];
            Warehouse wh = new Warehouse();

            wh = (new WarehouseFacade(DataProvider)).GetWarehouse(w2ss.WarehouseCode,/*w2ss.SegmentCode,*/ w2ss.FactoryCode) as Warehouse;
            //wh.FactoryCode = w2ss.FactoryCode;
            //wh.SegmentCode = w2ss.SegmentCode;
            //wh.WarehouseCode = w2ss.WarehouseCode;
            w2ss = null;
            return wh;
        }

        public void DeleteTransactionType(TransactionType[] transactionType)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < transactionType.Length; i++)
                {
                    this.DeleteTransactionType(transactionType[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message);
            }
        }

        public object GetTransactionType(string transactionTypeCode)
        {
            return this.DataProvider.CustomSearch(typeof(TransactionType), new object[] { transactionTypeCode });
        }

        /// <summary>
        /// ** 功能描述:	查询TransactionType的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="transactionTypeCode">TransactionTypeCode，模糊查询</param>
        /// <returns> TransactionType的总记录数</returns>
        public int QueryTransactionTypeCount(string transactionTypeCode, string transName)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLTRANSTYPE where 1=1 and TRANSTYPECODE like '{0}%' and TRANSTYPENAME like '{1}%'  ", transactionTypeCode, transName)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询TransactionType
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="transactionTypeCode">TransactionTypeCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> TransactionType数组</returns>
        public object[] QueryTransactionType(string transactionTypeCode, string transName, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(TransactionType), new PagerCondition(string.Format("select {0} from TBLTRANSTYPE where 1=1 and TRANSTYPECODE like '{1}%' and TRANSTYPENAME like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(TransactionType)), transactionTypeCode, transName), "TRANSTYPECODE,TRANSTYPENAME", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的TransactionType
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>TransactionType的总记录数</returns>
        public object[] GetAllTransactionType()
        {
            return this.DataProvider.CustomQuery(typeof(TransactionType), new SQLCondition(string.Format("select {0} from TBLTRANSTYPE order by TRANSTYPECODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(TransactionType)))));
        }

        private string GetTransactionTypeByName(string name)
        {
            string strCode = "";
            if (name == "AdjustTrans")
                strCode = "202";
            else if (name == "TSSplitItem")
                strCode = "796";
            else if (name == "WarehouseInit")
                strCode = "100";
            else if (name == "ReturnItem")
                strCode = "262";
            return strCode;
        }

        #endregion

        #region Warehouse
        /// <summary>
        /// 仓库
        /// </summary>
        public Warehouse CreateNewWarehouse()
        {
            return new Warehouse();
        }

        public void AddWarehouse(Warehouse warehouse)
        {
            warehouse.UseCount = 0;
            if (warehouse.FactoryCode == string.Empty /* || warehouse.SegmentCode == string.Empty*/)
            {
                // 工厂或工段选择"全部"

                //获取所有需要增加的工厂、工段
                Hashtable htFactory = new Hashtable();
                if (warehouse.FactoryCode == string.Empty)
                {
                    object[] objs = this.GetAllFactory();
                    if (objs != null)
                    {
                        foreach (object obj in objs)
                            htFactory.Add(((Factory)obj).FactoryCode, "");
                    }
                    objs = null;
                }
                else
                    htFactory.Add(warehouse.FactoryCode, "");
                Hashtable htSeg = new Hashtable();
                //				if (warehouse.SegmentCode == string.Empty)
                //				{
                //					BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this._domainDataProvider);
                //					object[] objs = bmFacade.GetAllSegment();
                //					if (objs != null)
                //					{
                //						foreach (object obj in objs)
                //							htSeg.Add(((BenQGuru.eMES.Domain.BaseSetting.Segment)obj).SegmentCode, "");
                //					}
                //					objs = null;
                //				}
                //				else
                //					htSeg.Add(warehouse.SegmentCode, "");

                //查询所有已存在的仓库
                object[] whs = this.GetAllWarehouse();
                Hashtable htwh = new Hashtable();
                if (whs != null)
                {
                    foreach (object obj in whs)
                    {
                        Warehouse wh = (Warehouse)obj;
                        htwh.Add(string.Format("{0}-{1}", wh.FactoryCode, /*wh.SegmentCode, */wh.WarehouseCode), "");
                    }
                }
                whs = null;

                //添加所有的仓库
                foreach (object objf in htFactory.Keys)
                {
                    foreach (object objseg in htSeg.Keys)
                    {
                        //如果仓库已存在，则跳过
                        if (!htwh.ContainsKey(string.Format("{0}-{1}", objf, objseg, warehouse.WarehouseCode)))
                        {
                            warehouse.FactoryCode = objf.ToString();
                            //warehouse.SegmentCode = objseg.ToString();
                            this._helper.AddDomainObject(warehouse);
                        }
                    }
                }
                htFactory = null;
                htSeg = null;
                whs = null;
            }
            else
                this._helper.AddDomainObject(warehouse);
        }

        public void UpdateWarehouse(Warehouse warehouse)
        {
            if (this.GetWarehouseStatus(warehouse.WarehouseCode,/*warehouse.SegmentCode,*/warehouse.FactoryCode) == Warehouse.WarehouseStatus_Cycle)
            {
                //如果库房之前是盘点状态，对在制品解锁
                this.UnLockSimulation(warehouse.WarehouseCode);
            }
            this._helper.UpdateDomainObject(warehouse);
        }

        public void DeleteWarehouse(Warehouse warehouse)
        {
            if (warehouse.UseCount > 0 || warehouse.UseCount == -1)
            {
                ExceptionManager.Raise(warehouse.GetType(), "$Error_Delete_Warehouse_InUse");
                return;
            }
            if (IsExist(typeof(WarehouseStock), new string[] { "FactoryCode",/*"SegmentCode",*/"WarehouseCode" }, new object[] { warehouse.FactoryCode, /*warehouse.SegmentCode,*/ warehouse.WarehouseCode }))
            {
                throw new Exception(string.Format("$Delete_Warehouse_Error_UseInWarehouseStock [$FactoryCode={0},$WarehouseCode={1}]", warehouse.FactoryCode,/* warehouse.SegmentCode,*/ warehouse.WarehouseCode));
            }
            this._helper.DeleteDomainObject(warehouse);
            object[] objs = this.DataProvider.CustomSearch(typeof(Warehouse2StepSequence), new string[] { "FactoryCode", /*"SegmentCode",*/ "WarehouseCode" }, new string[] { warehouse.FactoryCode, /*warehouse.SegmentCode, */ warehouse.WarehouseCode });
            if (objs != null)
            {
                foreach (object obj in objs)
                {
                    Warehouse2StepSequence wss = (Warehouse2StepSequence)obj;
                    this.DeleteWarehouse2StepSequence(wss);
                    wss = null;
                }
            }
            objs = null;
        }

        public void DeleteWarehouse(Warehouse[] warehouse)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < warehouse.Length; i++)
                {
                    this.DeleteWarehouse(warehouse[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message);
            }
        }

        public object GetWarehouse(string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            return this.DataProvider.CustomSearch(typeof(Warehouse), new object[] { warehouseCode, /*segmentCode,*/ factoryCode });
        }

        public string GetWarehouseStatus(string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            object obj = this.GetWarehouse(warehouseCode,/* segmentCode,*/ factoryCode);
            if (obj == null)
                return "";
            Warehouse wh = (Warehouse)obj;
            string str = wh.WarehouseStatus;
            wh = null;
            return str;
        }

        /// <summary>
        /// ** 功能描述:	查询Warehouse的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="warehouseCode">WarehouseCode，模糊查询</param>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <param name="factoryCode">FactoryCode，模糊查询</param>
        /// <returns> Warehouse的总记录数</returns>
        public int QueryWarehouseCount(string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWAREHOURSE where 1=1 and WHCODE like '{0}%' and FACCODE like '{1}%' ", warehouseCode, /*segmentCode,*/ factoryCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Warehouse
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="warehouseCode">WarehouseCode，模糊查询</param>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <param name="factoryCode">FactoryCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Warehouse数组</returns>
        public object[] QueryWarehouse(string warehouseCode, /*string segmentCode,*/ string factoryCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Warehouse), new PagerCondition(string.Format("select {0} from TBLWAREHOURSE where 1=1 and WHCODE like '{1}%' and FACCODE like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse)), warehouseCode,/* segmentCode,*/ factoryCode), "FACCODE,WHCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Warehouse
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Warehouse的总记录数</returns>
        public object[] GetAllWarehouse()
        {
            return this.DataProvider.CustomQuery(typeof(Warehouse), new SQLCondition(string.Format("select {0} from TBLWAREHOURSE order by WHCODE,FACCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse)))));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Warehouse
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Warehouse数组</returns>
        public object[] GetAllDistinctWarehouse()
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Warehouse), new SQLCondition(string.Format("select distinct WHCODE from TBLWAREHOURSE order by WHCODE")));
            return objs;
        }

        /// <summary>
        /// ** 功能描述:	获得所有的WarehouseType
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Parameter数组</returns>
        public object[] GetWarehouseTypes()
        {
            BenQGuru.eMES.BaseSetting.SystemSettingFacade ssfacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(this._domainDataProvider);
            object[] objs = ssfacade.GetParametersByParameterGroup(BenQGuru.eMES.Domain.Warehouse.Warehouse.WarehouseTypeGroup);
            ssfacade = null;
            return objs;
        }

        public object[] GetWarehouseByFactorySeg(/* string segmentCode,*/ string factoryCode, bool onlyControl)
        {
            string strSql = string.Format("select {0} from TBLWAREHOURSE where FACCODE = '{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse)),  /*segmentCode,*/ factoryCode);
            if (onlyControl)
                strSql += string.Format(" and ISCTRL='1' ");
            strSql += " order by WHCODE";
            return this.DataProvider.CustomQuery(typeof(Warehouse), new SQLCondition(strSql));
        }
        public object[] GetWarehouseByFactorySeg( /*string segmentCode,*/ string factoryCode)
        {
            return GetWarehouseByFactorySeg(/*segmentCode,*/ factoryCode, false);
        }

        public void AddWarehouseUseCount(string warehouseCode,/* string segmentCode, */string factoryCode)
        {
            if (warehouseCode == string.Empty || /*segmentCode == string.Empty ||*/ factoryCode == string.Empty)
                return;
            object obj = this.GetWarehouse(warehouseCode, /*segmentCode,*/ factoryCode);
            if (obj != null)
            {
                Warehouse wh = (Warehouse)obj;
                if (wh.UseCount >= 0)
                {
                    this.DataProvider.CustomExecute(new SQLCondition(string.Format("update TBLWAREHOURSE set USECOUNT=USECOUNT+1 where WHCODE = '{0}' and FACCODE = '{1}' ", warehouseCode,/* segmentCode,*/ factoryCode)));
                }
                wh = null;
            }
            obj = null;
        }
        public void DeleteWarehouseUseCount(string warehouseCode, /*string segmentCode, */ string factoryCode)
        {
            if (warehouseCode == string.Empty /* || segmentCode == string.Empty*/ || factoryCode == string.Empty)
                return;
            object obj = this.GetWarehouse(warehouseCode, /*segmentCode,*/ factoryCode);
            if (obj != null)
            {
                Warehouse wh = (Warehouse)obj;
                if (wh.UseCount >= 0)
                {
                    this.DataProvider.CustomExecute(new SQLCondition(string.Format("update TBLWAREHOURSE set USECOUNT=USECOUNT-1 where WHCODE = '{0}' and FACCODE = '{1}' ", warehouseCode, /*segmentCode,*/ factoryCode)));
                }
                wh = null;
            }
            obj = null;
        }

        /// <summary>
        /// 盘点关帐
        /// </summary>
        public void WarehouseClose(string warehouseCode, /*string segmentCode,*/ string factoryCode, string userId)
        {
            //查询出所有符合条件的仓库
            System.Text.StringBuilder sb = new System.Text.StringBuilder("select {0} from TBLWAREHOURSE where 1=1 ");
            if (warehouseCode != string.Empty)
            {
                sb.Append(string.Format(" and WHCODE = '{0}' ", warehouseCode));
            }
            //			if (segmentCode != string.Empty)
            //			{
            //				sb.Append(string.Format(" and SEGCODE = '{0}' ", segmentCode));
            //			}
            if (factoryCode != string.Empty)
            {
                sb.Append(string.Format(" and FACCODE = '{0}' ", factoryCode));
            }
            object[] objs = this.DataProvider.CustomQuery(typeof(Warehouse), new SQLCondition(string.Format(sb.ToString(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse)))));
            if (objs != null)
            {
                //对每个仓库做盘点关帐处理
                for (int i = 0; i < objs.Length; i++)
                {
                    Warehouse wh = (Warehouse)objs[i];
                    if (wh.WarehouseStatus == Warehouse.WarehouseStatus_Normal)
                    {
                        this.DataProvider.BeginTransaction();
                        try
                        {
                            //更新盘点主表
                            WarehouseCycleCount cycle = this.CreateNewWarehouseCycleCount();
                            cycle.FactoryCode = wh.FactoryCode;
                            //cycle.SegmentCode = wh.SegmentCode;
                            cycle.WarehouseCode = wh.WarehouseCode;
                            cycle.MaintainUser = userId;
                            this.AddWarehouseCycleCount(cycle);

                            //更新盘点明细表
                            object[] objstock = this.GetWarehouseStockPro(string.Empty, wh.WarehouseCode, /* wh.SegmentCode,*/ wh.FactoryCode);
                            if (objstock != null)
                            {
                                for (int m = 0; m < objstock.Length; m++)
                                {
                                    WarehouseStock stock = (WarehouseStock)objstock[m];
                                    WarehouseCycleCountDetail dtl = this.CreateNewWarehouseCycleCountDetail();
                                    dtl.CycleCountCode = cycle.CycleCountCode;
                                    dtl.FactoryCode = stock.FactoryCode;
                                    //dtl.SegmentCode = stock.SegmentCode;
                                    dtl.WarehouseCode = stock.WarehouseCode;
                                    dtl.ItemCode = stock.ItemCode;
                                    /* modified by jessie lee, 2006-2-24, CS053 */
                                    dtl.Qty = stock.OpenQty;
                                    dtl.LineQty = 0;
                                    dtl.Warehouse2LineQty = dtl.Qty;
                                    dtl.PhysicalQty = 0;
                                    dtl.MaintainUser = userId;
                                    this.AddWarehouseCycleCountDetail(dtl);
                                    dtl = null;
                                }
                            }

                            //更新仓库状态
                            wh.WarehouseStatus = Warehouse.WarehouseStatus_Cycle;
                            wh.LastCycleCountCode = cycle.CycleCountCode;
                            //2006/11/17,Laws Lu add get DateTime from db Server
                            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                            wh.LastCycleCountDate = FormatHelper.TODateInt(dtNow);
                            wh.LastCycleCountTime = FormatHelper.TOTimeInt(dtNow);
                            this.UpdateWarehouse(wh);

                            //锁定在制品
                            this.LockSimulation(warehouseCode);

                            this.DataProvider.CommitTransaction();
                        }
                        catch
                        {
                            this.DataProvider.RollbackTransaction();
                        }
                    }
                    wh = null;
                }
            }
            objs = null;
        }


        /// <summary>
        /// 锁定在制品	(盘点关帐时) 
        /// </summary>
        /// <param name="whcode">库房代码</param>
        private void LockSimulation(string whcode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += " UPDATE tblsimulation SET ishold = -1 ";
            sql += " where rescode in (select rescode from tblres where sscode in (select sscode from TBLWH2SSCODE where whcode='{0}') ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ";
            // End Added

            //ishold 为 "-1" 表示 正在盘点,CS将不能采集
            sql = string.Format(sql, whcode);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        /// <summary>
        /// 对在制品解锁 （盘点结束时）
        /// </summary>
        /// <param name="whcode">库房代码</param>
        public void UnLockSimulation(string whcode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "  UPDATE tblsimulation SET ishold = 0 ";
            sql += "  where rescode in (select rescode from tblres where sscode in (select sscode from TBLWH2SSCODE where whcode='{0}') ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ";
            // End Added

            //ishold 为 "0" 表示 正常
            sql = string.Format(sql, whcode);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        /// <summary>
        /// 库房是否盘点状态
        /// </summary>
        /// <param name="whcode"></param>
        /// <returns></returns>
        public bool isWareHourseCycle(string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            string sql = string.Format(@"select count(whcode) from TBLWAREHOURSE 
								where whstatus = '{0}' 
								and whcode='{1}'
								and faccode='{2}'", Warehouse.WarehouseStatus_Cycle, warehouseCode,/*segmentCode,*/factoryCode);
            int i = this.DataProvider.GetCount(new SQLCondition(sql));
            return (i > 0) ? true : false;
        }

        #region 盘点维修换料

        //盘点维修换料记录
        private Hashtable CycleTSItem(string warehouseCode)
        {
            //获取要盘点的库房对应的产线资源的在制品维修换料信息 , 然后增加相应在制品的上料信息,(因下料已经增加相应库存 , 因此下料无须统计)
            //tbltsitem 

            string sql = string.Format(@" SELECT {0} " +
                                    "	FROM tbltsitem " +
                                    "	WHERE 1 = 1 " +
                                    "	AND mitemcode > '0' " +
                                    "	AND EXISTS ( " +
                                    "			SELECT mocode " +
                                    "				FROM tblmo " +
                                    "			WHERE tblmo.mocode = tbltsitem.mocode " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "				AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                                    "	AND EXISTS ( " +
                                    "			SELECT rescode " +
                                    "				FROM tblres " +
                                    "			WHERE tblres.rescode = tbltsitem.rrescode " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "				AND EXISTS ( " +
                                    "						SELECT sscode " +
                                    "							FROM tblwh2sscode " +
                                    "							WHERE tblwh2sscode.sscode = tblres.sscode " +
                                    "								AND whcode = '{1}')) ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.TS.TSItem))
                , warehouseCode);

            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TS.TSItem), new SQLCondition(sql));

            //--SITEMCODE 换下的料
            //--mitemcode 换上的料 

            //对上料信息作统计。
            //mitemcode 换上的料 
            Hashtable _loadinght = new Hashtable();	//上料料号作键值，上料数量作Value
            foreach (BenQGuru.eMES.Domain.TS.TSItem _tsitem in objs)
            {
                string htkey = _tsitem.MItemCode;
                if (_loadinght.Contains(htkey))
                {
                    _loadinght[htkey] = Convert.ToDecimal(_loadinght[htkey]) + _tsitem.Qty;
                }
                else
                {
                    _loadinght.Add(_tsitem.MItemCode, _tsitem.Qty);
                }
            }

            //对下料信息作统计。
            //SITEMCODE 换下的料
            Hashtable _downht = new Hashtable();	//下料料号作键值，上料数量作Value
            foreach (BenQGuru.eMES.Domain.TS.TSItem _tsitem in objs)
            {
                string htkey = _tsitem.SourceItemCode;
                if (_downht.Contains(htkey))
                {
                    _downht[htkey] = Convert.ToDecimal(_downht[htkey]) + _tsitem.Qty;
                }
                else
                {
                    _downht.Add(_tsitem.SourceItemCode, _tsitem.Qty);
                }
            }

            Hashtable _ht = new Hashtable();
            _ht.Add("_loadinght", _loadinght);
            _ht.Add("_downht", _downht);
            //返回统计结果
            return _ht;
        }

        #endregion

        #region 盘点在制品

        /// <summary>
        /// 盘点在制品 (上料记录)
        /// </summary>
        /// <param name="warehouseCode"></param>
        private Hashtable CycleSimulation(string warehouseCode)
        {
            //获取要盘点的库房对应的产线资源的在制品上料信息
            //tblonwipitem 表中 ActionType表示上料（0）或者下料（1）
            //TRANSSTATUS表示是否进行库房操作 NO表示没有库房操作，YES表式进行了库房操作 
            string sql = string.Format(@" SELECT {0} " +
                                    "	FROM tblonwipitem " +
                                    "	WHERE 1 = 1 " +
                                    "	AND ACTIONTYPE=0 " +
                                    "	AND TRANSSTATUS = 'YES' " +
                                    "	AND mitemcode > '0' " +
                                    "	AND EXISTS ( " +
                                    "			SELECT mocode " +
                                    "				FROM tblmo " +
                                    "			WHERE tblmo.mocode = tblonwipitem.mocode " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "				AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                                    "	AND EXISTS ( " +
                                    "			SELECT rescode " +
                                    "				FROM tblres " +
                                    "			WHERE tblres.rescode = tblonwipitem.rescode " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "				AND EXISTS ( " +
                                    "						SELECT sscode " +
                                    "							FROM tblwh2sscode " +
                                    "							WHERE tblwh2sscode.sscode = tblres.sscode " +
                                    "								AND whcode = '{1}')) ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem))
                                                                    , warehouseCode);

            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem), new SQLCondition(sql));

            //对上料信息作统计。
            Hashtable _ht = new Hashtable();	//上料料号作键值，上料数量作Value
            if (objs != null && objs.Length > 0)
                foreach (BenQGuru.eMES.Domain.DataCollect.OnWIPItem _onwipitem in objs)
                {
                    string htkey = _onwipitem.MItemCode;
                    if (_ht.Contains(htkey))
                    {
                        _ht[htkey] = Convert.ToDecimal(_ht[htkey]) + _onwipitem.Qty;
                    }
                    else
                    {
                        _ht.Add(_onwipitem.MItemCode, _onwipitem.Qty);
                    }
                }

            //返回统计结果
            return _ht;
        }

        //盘点下料记录
        private Hashtable CycleDownSimulation(string warehouseCode)
        {
            //获取要盘点的库房对应的产线资源的在制品下料信息 （统计有作库房操作的数据）
            //tblonwipitem 表中 ActionType表示上料（0）或者下料（1）
            //TRANSSTATUS表示是否进行库房操作 NO表示没有库房操作，YES表式进行了库房操作 
            string sql = string.Format(@" SELECT {0} " +
                                    "	FROM tblonwipitem " +
                                    "	WHERE 1 = 1 " +
                                    "	AND ACTIONTYPE=1 " +
                                    "	AND TRANSSTATUS = 'YES' " +
                                    "	AND mitemcode > '0' " +
                                    "	AND EXISTS ( " +
                                    "			SELECT mocode " +
                                    "				FROM tblmo " +
                                    "			WHERE tblmo.mocode = tblonwipitem.mocode " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "				AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                                    "	AND EXISTS ( " +
                                    "			SELECT rescode " +
                                    "				FROM tblres " +
                                    "			WHERE tblres.rescode = tblonwipitem.rescode " +
                                    GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                                    "				AND EXISTS ( " +
                                    "						SELECT sscode " +
                                    "							FROM tblwh2sscode " +
                                    "							WHERE tblwh2sscode.sscode = tblres.sscode " +
                                    "								AND whcode = '{1}')) ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem))
                , warehouseCode);

            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem), new SQLCondition(sql));

            //对下料信息作统计。
            Hashtable _ht = new Hashtable();	//下料料号作键值，上料数量作Value
            foreach (BenQGuru.eMES.Domain.DataCollect.OnWIPItem _onwipitem in objs)
            {
                string htkey = _onwipitem.MItemCode;
                if (_ht.Contains(htkey))
                {
                    _ht[htkey] = Convert.ToDecimal(_ht[htkey]) + _onwipitem.Qty;
                }
                else
                {
                    _ht.Add(_onwipitem.MItemCode, _onwipitem.Qty);
                }
            }

            //返回统计结果
            return _ht;
        }

        #endregion

        #endregion

        #region Warehouse2StepSequence
        /// <summary>
        /// 产线与仓库对应
        /// </summary>
        public Warehouse2StepSequence CreateNewWarehouse2StepSequence()
        {
            return new Warehouse2StepSequence();
        }

        public void AddWarehouse2StepSequence(Warehouse2StepSequence warehouse2StepSequence)
        {
            if (warehouse2StepSequence.FactoryCode == string.Empty)
            {
                //如果工厂代码为空，则添加所有的工厂代码
                object[] objs = this.DataProvider.CustomSearch(typeof(Warehouse), new string[] { "WarehouseCode" }, new string[] { warehouse2StepSequence.WarehouseCode /*, warehouse2StepSequence.SegmentCode*/});
                if (objs == null)
                    return;

                //查询出现有数据
                object[] objswss = this.QueryWarehouse2StepSequenceBySS(warehouse2StepSequence.StepSequenceCode, 1, int.MaxValue);
                Hashtable ht = new Hashtable();
                Hashtable htwh = new Hashtable();
                if (objswss != null)
                {
                    foreach (object obj in objswss)
                    {
                        Warehouse2StepSequence wss = (Warehouse2StepSequence)obj;
                        ht.Add(string.Format("{0}-{1}-{2}", wss.StepSequenceCode, wss.WarehouseCode, /*wss.SegmentCode,*/ wss.FactoryCode), string.Format("{0}", /*wss.StepSequenceCode,*/ wss.FactoryCode));
                        if (!htwh.ContainsKey(wss.StepSequenceCode))
                        {
                            htwh.Add(wss.StepSequenceCode, wss.WarehouseCode);
                        }
                    }
                }
                objswss = null;

                //循环添加所有项
                foreach (object obj in objs)
                {
                    Warehouse wh = (Warehouse)obj;
                    //已有记录中是否存在当前数据
                    if (!ht.ContainsKey(string.Format("{0}-{1}-{2}", warehouse2StepSequence.StepSequenceCode, warehouse2StepSequence.WarehouseCode, /*warehouse2StepSequence.SegmentCode,*/ wh.FactoryCode)))
                    {
                        //一条产线只能添加一个工厂
                        if (!ht.ContainsValue(string.Format("{0}", /*warehouse2StepSequence.StepSequenceCode,*/ wh.FactoryCode)))
                        {
                            //一条产线只能添加一个仓库名
                            if (!htwh.ContainsKey(warehouse2StepSequence.StepSequenceCode) || htwh[warehouse2StepSequence.StepSequenceCode].ToString() == warehouse2StepSequence.WarehouseCode)
                            {
                                warehouse2StepSequence.FactoryCode = wh.FactoryCode;
                                this._helper.AddDomainObject(warehouse2StepSequence);
                            }
                        }
                    }
                    wh = null;
                }
                ht = null;
                htwh = null;
                objs = null;
            }
            else
            {
                if (this.Warehouse2StepSequenceExistFactory(warehouse2StepSequence.StepSequenceCode, warehouse2StepSequence.FactoryCode))
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Add_Warehouse2StepSequence_FactoryExist");
                }
                if (!(this.QueryWarehouse2StepSequenceBySSCount(warehouse2StepSequence.StepSequenceCode) == 0 || this.IsExist(typeof(Warehouse2StepSequence), new string[] { "StepSequenceCode", "WarehouseCode" }, new object[] { warehouse2StepSequence.StepSequenceCode, warehouse2StepSequence.WarehouseCode })))
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Add_Warehouse2StepSequence_WarehouseExist");
                }
                this._helper.AddDomainObject(warehouse2StepSequence);
            }
        }

        public void UpdateWarehouse2StepSequence(Warehouse2StepSequence warehouse2StepSequence)
        {
            this._helper.UpdateDomainObject(warehouse2StepSequence);
        }

        public void DeleteWarehouse2StepSequence(Warehouse2StepSequence warehouse2StepSequence)
        {
            this._helper.DeleteDomainObject(warehouse2StepSequence);
        }

        public void DeleteWarehouse2StepSequence(Warehouse2StepSequence[] warehouse2StepSequence)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < warehouse2StepSequence.Length; i++)
                {
                    this.DeleteWarehouse2StepSequence(warehouse2StepSequence[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Delete_Domain_Object", ex);
            }
        }

        public object GetWarehouse2StepSequence(string stepSequenceCode, string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            return this.DataProvider.CustomSearch(typeof(Warehouse2StepSequence), new object[] { stepSequenceCode, warehouseCode, /*segmentCode,*/ factoryCode });
        }
        public bool Warehouse2StepSequenceExistFactory(string stepSequenceCode, string factoryCode)
        {
            int iCount = this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWH2SSCODE where SSCODE = '{0}' and FACCODE = '{1}' ", stepSequenceCode, factoryCode)));
            return (iCount > 0);
        }

        /// <summary>
        /// ** 功能描述:	查询Warehouse2StepSequence的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
        /// <param name="warehouseCode">WarehouseCode，模糊查询</param>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <param name="factoryCode">FactoryCode，模糊查询</param>
        /// <returns> Warehouse2StepSequence的总记录数</returns>
        public int QueryWarehouse2StepSequenceCount(string stepSequenceCode, string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWH2SSCODE where 1=1 and SSCODE = '{0}'  and WHCODE = '{1}' and FACCODE = '{2}' ", stepSequenceCode, warehouseCode, /*segmentCode,*/ factoryCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Warehouse2StepSequence
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
        /// <param name="warehouseCode">WarehouseCode，模糊查询</param>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <param name="factoryCode">FactoryCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Warehouse2StepSequence数组</returns>
        public object[] QueryWarehouse2StepSequence(string stepSequenceCode, string warehouseCode, /*string segmentCode,*/ string factoryCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Warehouse2StepSequence), new PagerCondition(string.Format("select {0} from TBLWH2SSCODE where 1=1 and SSCODE = '{1}'  and WHCODE = '{2}' and FACCODE = '{3}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse2StepSequence)), stepSequenceCode, warehouseCode, /*segmentCode,*/ factoryCode), "SSCODE,FACCODE,WHCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Warehouse2StepSequence
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Warehouse2StepSequence的总记录数</returns>
        public object[] GetAllWarehouse2StepSequence()
        {
            return this.DataProvider.CustomQuery(typeof(Warehouse2StepSequence), new SQLCondition(string.Format("select {0} from TBLWH2SSCODE order by SSCODE,WHCODE,FACCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse2StepSequence)))));
        }


        public object[] QueryWarehouse2StepSequenceBySS(string stepSequenceCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Warehouse2StepSequence), new PagerCondition(string.Format("select {0} from TBLWH2SSCODE where 1=1 and SSCODE = '{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse2StepSequence)), stepSequenceCode), "SSCODE,FACCODE,WHCODE", inclusive, exclusive));
        }
        public int QueryWarehouse2StepSequenceBySSCount(string stepSequenceCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWH2SSCODE where SSCODE = '{0}' ", stepSequenceCode)));
        }

        #endregion

        #region WarehouseCycleCount
        /// <summary>
        /// 仓库盘点主档
        /// </summary>
        public WarehouseCycleCount CreateNewWarehouseCycleCount()
        {
            return new WarehouseCycleCount();
        }

        public void AddWarehouseCycleCount(WarehouseCycleCount warehouseCycleCount)
        {
            //计算CycleCountCode
            int iCount = this.QueryWarehouseCycleCountCount(string.Empty);
            iCount++;
            string scode = (new string('0', 8 - iCount.ToString().Length)) + iCount.ToString();
            warehouseCycleCount.CycleCountCode = scode;
            this._helper.AddDomainObject(warehouseCycleCount);
        }

        public void UpdateWarehouseCycleCount(WarehouseCycleCount warehouseCycleCount)
        {
            this._helper.UpdateDomainObject(warehouseCycleCount);
        }

        public void DeleteWarehouseCycleCount(WarehouseCycleCount warehouseCycleCount)
        {
            this._helper.DeleteDomainObject(warehouseCycleCount,
                new ICheck[]{ new DeleteAssociateCheck( warehouseCycleCount,
								this.DataProvider, 
								new Type[]{
											  typeof(WarehouseCycleCountDetail)	})});
        }

        public void DeleteWarehouseCycleCount(WarehouseCycleCount[] warehouseCycleCount)
        {
            this._helper.DeleteDomainObject(warehouseCycleCount,
                new ICheck[]{ new DeleteAssociateCheck( warehouseCycleCount,
								this.DataProvider, 
								new Type[]{
											  typeof(WarehouseCycleCountDetail)	})});
        }

        public object GetWarehouseCycleCount(string cycleCountCode)
        {
            return this.DataProvider.CustomSearch(typeof(WarehouseCycleCount), new object[] { cycleCountCode });
        }

        /// <summary>
        /// ** 功能描述:	查询WarehouseCycleCount的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="cycleCountCode">CycleCountCode，模糊查询</param>
        /// <returns> WarehouseCycleCount的总记录数</returns>
        public int QueryWarehouseCycleCountCount(string cycleCountCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWHCYCLE where 1=1 and CYCLECODE like '{0}%' ", cycleCountCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询WarehouseCycleCount
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="cycleCountCode">CycleCountCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> WarehouseCycleCount数组</returns>
        public object[] QueryWarehouseCycleCount(string cycleCountCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseCycleCount), new PagerCondition(string.Format("select {0} from TBLWHCYCLE where 1=1 and CYCLECODE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseCycleCount)), cycleCountCode), "CYCLECODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的WarehouseCycleCount
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>WarehouseCycleCount的总记录数</returns>
        public object[] GetAllWarehouseCycleCount()
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseCycleCount), new SQLCondition(string.Format("select {0} from TBLWHCYCLE order by CYCLECODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseCycleCount)))));
        }

        /// <summary>
        /// 盘点库存调整
        /// </summary>
        public bool AdjustWarehouseCycleCount(string warehouseCode, /*string segmentCode,*/ string factoryCode, string userId)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(" WHSTATUS = '" + Warehouse.WarehouseStatus_Cycle + "' ");
            System.Text.StringBuilder sbwh = new System.Text.StringBuilder();
            if (warehouseCode != string.Empty)
            {
                sbwh.Append(string.Format(" and WHCODE = '{0}' ", warehouseCode));
            }
            //			if (segmentCode != string.Empty)
            //			{
            //				sbwh.Append(string.Format(" and SEGCODE = '{0}' ", segmentCode));
            //			}
            if (factoryCode != string.Empty)
            {
                sbwh.Append(string.Format(" and FACCODE = '{0}' ", factoryCode));
            }
            sb.Append(sbwh.ToString());
            string strSql = "select {0} from TBLWAREHOURSE where " + sb.ToString();
            //先读取仓库表
            object[] objswh = this.DataProvider.CustomQuery(typeof(Warehouse), new SQLCondition(string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse)))));
            if (objswh == null)
                return false;
            //所有的盘点代码
            string strCycleCode = "";
            for (int i = 0; i < objswh.Length; i++)
            {
                Warehouse wh = (Warehouse)objswh[i];
                if (wh.LastCycleCountCode != "")
                {
                    strCycleCode += ",'" + wh.LastCycleCountCode + "'";
                }
            }
            strCycleCode = strCycleCode.Substring(1);
            //读取物料信息
            object[] objsitem = this.GetAllWarehouseItem();
            Hashtable htitem = new Hashtable();
            for (int i = 0; i < objsitem.Length; i++)
            {
                WarehouseItem itm = (WarehouseItem)objsitem[i];
                htitem.Add(itm.ItemCode, itm.ItemName);
            }
            objsitem = null;
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


            try
            {
                this.DataProvider.BeginTransaction();

                //更新仓库状态
                strSql = "update TBLWAREHOURSE set WHSTATUS = '" + Warehouse.WarehouseStatus_Normal + "' where " + sb.ToString();
                this.DataProvider.CustomExecute(new SQLCondition(strSql));

                //更新盘点主表
                strSql = "update TBLWHCYCLE set CFMUSER=$cfmuser,CFMDATE=$cfmdate,CFMTIME=$cfmtime where CYCLECODE in (" + strCycleCode + ")";
                this.DataProvider.CustomExecute(new SQLParamCondition(strSql,
                    new SQLParameter[]{
										  new SQLParameter("cfmuser", typeof(string), userId), 
										  new SQLParameter("cfmdate", typeof(int), FormatHelper.TODateInt(dtNow)), 
										  new SQLParameter("cfmtime", typeof(int), FormatHelper.TOTimeInt(dtNow))
									  }));

                //更新盘点明细表
                //strSql = "update TBLWHCYLCEDETAIL set ADJQTY=PHQTY-QTY, ADJUSER=$adjuser,ADJDATE=$adjdate,ADJTIME=$adjtime, CFMUSER=$cfmuser,CFMDATE=$cfmdate,CFMTIME=$cfmtime where CYCLECODE in (" + strCycleCode + ")";
                //库存调整数量 ＝ 实盘数量 - 账面数 
                //added by jessie, 2006-3-8, 离散数量 ＝ 实盘数－虚拆数量
                strSql = "update TBLWHCYLCEDETAIL set ADJQTY=PHQTY-WAREHOUSE2LINEQTY, QTY = PHQTY-LINEQTY, ADJUSER=$adjuser,ADJDATE=$adjdate,ADJTIME=$adjtime, CFMUSER=$cfmuser,CFMDATE=$cfmdate,CFMTIME=$cfmtime where CYCLECODE in (" + strCycleCode + ")";
                this.DataProvider.CustomExecute(new SQLParamCondition(strSql,
                    new SQLParameter[]{
										  new SQLParameter("adjuser", typeof(string), userId), 
										  new SQLParameter("adjdate", typeof(int), FormatHelper.TODateInt(dtNow)), 
										  new SQLParameter("adjtime", typeof(int), FormatHelper.TOTimeInt(dtNow)),
										  new SQLParameter("cfmuser", typeof(string), userId), 
										  new SQLParameter("cfmdate", typeof(int), FormatHelper.TODateInt(dtNow)), 
										  new SQLParameter("cfmtime", typeof(int), FormatHelper.TOTimeInt(dtNow))
									  }));

                //更新库存记录
                strSql = "select {0} from TBLWHCYLCEDETAIL where CYCLECODE in (" + strCycleCode + ") order by CYCLECODE ";
                object[] objscycle = this.DataProvider.CustomQuery(typeof(WarehouseCycleCountDetail), new SQLCondition(string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseCycleCountDetail)))));
                if (objscycle == null)
                {
                    this.DataProvider.CommitTransaction();
                    return true;
                }
                strSql = "select {0} from TBLWHSTOCK where 1=1 " + sbwh.ToString();
                object[] objsstock = this.DataProvider.CustomQuery(typeof(WarehouseStock), new SQLCondition(string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseStock)))));
                Hashtable htstock = new Hashtable();
                for (int i = 0; i < objsstock.Length; i++)
                {
                    WarehouseStock stock = (WarehouseStock)objsstock[i];
                    htstock.Add(string.Format("{0}-{1}-{2}", stock.FactoryCode, /* stock.SegmentCode,*/ stock.WarehouseCode, stock.ItemCode), stock);
                    stock = null;
                }
                objsstock = null;
                for (int i = 0; i < objscycle.Length; i++)
                {
                    WarehouseCycleCountDetail dtl = (WarehouseCycleCountDetail)objscycle[i];
                    string strKey = string.Format("{0}-{1}-{2}", dtl.FactoryCode, /*dtl.SegmentCode,*/ dtl.WarehouseCode, dtl.ItemCode);
                    //如果在库存中不存在，则新增
                    if (!htstock.ContainsKey(strKey))
                    {
                        WarehouseStock stock = this.CreateNewWarehouseStock();
                        stock.FactoryCode = dtl.FactoryCode;
                        //stock.SegmentCode = dtl.SegmentCode;
                        stock.WarehouseCode = dtl.WarehouseCode;
                        stock.ItemCode = dtl.ItemCode;
                        //stock.OpenQty = dtl.PhysicalQty;  
                        stock.OpenQty = dtl.PhysicalQty - dtl.LineQty;	//库存数量调整为 ： 实盘数量－产线在制品虚拆数量  （因为实盘动作包含 库房盘点和线上盘点）
                        stock.MaintainUser = userId;
                        this.AddWarehouseStock(stock);
                        stock = null;
                    }
                    else	//如果存在，则更新库存
                    {
                        WarehouseStock stock = (WarehouseStock)htstock[strKey];
                        decimal _openqty = dtl.PhysicalQty - dtl.LineQty;
                        if (stock.OpenQty != _openqty)
                        {
                            //stock.OpenQty = dtl.PhysicalQty;
                            stock.OpenQty = _openqty;	//库存数量调整为 ： 实盘数量－产线在制品虚拆数量  （因为实盘动作包含 库房盘点和线上盘点）
                            this.UpdateWarehouseStock(stock);
                        }
                    }
                }
                htstock = null;

                //加入交易记录
                string strCycleCodePrev = "", strTicketNo = "";
                int iSeq = 0;
                for (int i = 0; i < objscycle.Length; i++)
                {
                    WarehouseCycleCountDetail dtl = (WarehouseCycleCountDetail)objscycle[i];
                    if (dtl.CycleCountCode != strCycleCodePrev)
                    {
                        strTicketNo = this.AddWarehouseTicketFromCycle(dtl, userId);
                        iSeq = 1;
                        strCycleCodePrev = dtl.CycleCountCode;
                    }
                    if (dtl.AdjustQty != 0)
                    {
                        this.AddWarehouseTicketDetailFromCycle(dtl, strTicketNo, iSeq, htitem[dtl.ItemCode].ToString(), userId);
                        iSeq++;
                    }
                }

                //对锁定的在制品解锁。
                this.UnLockSimulation(warehouseCode);
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message, ex);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 盘点调整库存时，将调整记录添加到交易主表
        /// </summary>
        /// <returns>交易号</returns>
        private string AddWarehouseTicketFromCycle(WarehouseCycleCountDetail item, string userId)
        {
            //TODO: 查询调整单代码
            string strTransCode = GetTransactionTypeByName("AdjustTrans");
            return this.AddWarehouseTicket(item.FactoryCode, /*item.SegmentCode,*/ item.WarehouseCode, strTransCode, userId);
            /*
            WarehouseTicket ticket = this.CreateNewWarehouseTicket();
            ticket.TOFactoryCode = item.FactoryCode;
            ticket.TOSegmentCode = item.SegmentCode;
            ticket.TOWarehouseCode = item.WarehouseCode;
            ticket.TransactionTypeCode = TransactionType.TRANSACTION_MAPPING["AdjustTrans"].ToString();
            ticket.MaintainUser = userId;
            ticket.TicketNo = this.GetTicketSeq("-", true);
            ticket.TicketUser = ticket.MaintainUser;
            ticket.TicketDate = FormatHelper.TODateInt(DateTime.Now);
            ticket.TicketTime = FormatHelper.TOTimeInt(DateTime.Now);
            ticket.TransactionStatus = WarehouseTicket.TransactionStatusEnum.Closed.ToString();
            ticket.TransactionUser = userId;
            ticket.TransactionDate = ticket.TicketDate;
            ticket.TransactionTime = ticket.TicketTime;
            this._helper.AddDomainObject( ticket );
            return ticket.TicketNo;
            */
        }
        private void AddWarehouseTicketDetailFromCycle(WarehouseCycleCountDetail item, string ticketNo, int iSequence, string itemName, string userId)
        {
            this.AddWarehouseTicketDetail(ticketNo, iSequence, "", item.ItemCode, itemName, item.AdjustQty, item.PhysicalQty, userId);
            /*
            WarehouseTicketDetail dtl = this.CreateNewWarehouseTicketDetail();
            dtl.TicketNo = ticketNo;
            dtl.Sequence = iSequence;
            dtl.ItemCode = item.ItemCode;
            dtl.ItemName = itemName;
            dtl.Qty = item.AdjustQty;
            dtl.FromWarehouseQty = item.PhysicalQty;
            dtl.MaintainUser = userId;
            this.AddWarehouseTicketDetail(dtl, false);
            */
        }

        #endregion

        #region WarehouseCycleCountDetail
        /// <summary>
        /// 盘点明细
        /// </summary>
        public WarehouseCycleCountDetail CreateNewWarehouseCycleCountDetail()
        {
            return new WarehouseCycleCountDetail();
        }

        public void AddWarehouseCycleCountDetail(WarehouseCycleCountDetail warehouseCycleCountDetail)
        {
            this._helper.AddDomainObject(warehouseCycleCountDetail);
        }

        public void UpdateWarehouseCycleCountDetail(WarehouseCycleCountDetail warehouseCycleCountDetail)
        {
            this._helper.UpdateDomainObject(warehouseCycleCountDetail);
        }

        public void DeleteWarehouseCycleCountDetail(WarehouseCycleCountDetail warehouseCycleCountDetail)
        {
            this._helper.DeleteDomainObject(warehouseCycleCountDetail);
        }

        public void DeleteWarehouseCycleCountDetail(WarehouseCycleCountDetail[] warehouseCycleCountDetail)
        {
            this._helper.DeleteDomainObject(warehouseCycleCountDetail);
        }

        public object GetWarehouseCycleCountDetail(string itemCode, string cycleCountCode)
        {
            return this.DataProvider.CustomSearch(typeof(WarehouseCycleCountDetail), new object[] { itemCode, cycleCountCode });
        }

        /// <summary>
        /// ** 功能描述:	查询WarehouseCycleCountDetail的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="cycleCountCode">CycleCountCode，模糊查询</param>
        /// <returns> WarehouseCycleCountDetail的总记录数</returns>
        public int QueryWarehouseCycleCountDetailCount(string itemCode, string cycleCountCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWHCYLCEDETAIL where 1=1 and ITEMCODE like '{0}%'  and CYCLECODE = '{1}' ", itemCode, cycleCountCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询WarehouseCycleCountDetail
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="cycleCountCode">CycleCountCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> WarehouseCycleCountDetail数组</returns>
        public object[] QueryWarehouseCycleCountDetail(string itemCode, string cycleCountCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseCycleCountDetail), new PagerCondition(string.Format("select {0} from TBLWHCYLCEDETAIL where 1=1 and ITEMCODE like '{1}%'  and CYCLECODE = '{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseCycleCountDetail)), itemCode, cycleCountCode), "CYCLECODE,ITEMCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的WarehouseCycleCountDetail
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>WarehouseCycleCountDetail的总记录数</returns>
        public object[] GetAllWarehouseCycleCountDetail()
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseCycleCountDetail), new SQLCondition(string.Format("select {0} from TBLWHCYLCEDETAIL order by ITEMCODE,CYCLECODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseCycleCountDetail)))));
        }

        /// <summary>
        /// 库存调整中，导入实盘数
        /// </summary>
        /// <returns>成功记录数</returns>
        public int ImportWarehouseCycleCountDetail(object[] items, string userId)
        {
            //获取物料信息
            System.Collections.ArrayList listItem = new System.Collections.ArrayList();
            object[] objs = this.GetAllWarehouseItem();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    listItem.Add(((WarehouseItem)objs[i]).ItemCode);
                }
            }
            objs = null;

            //获取仓库信息
            Hashtable htwh = new Hashtable();
            objs = this.GetAllWarehouse();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    Warehouse wh = (Warehouse)objs[i];
                    htwh.Add(string.Format("{0}-{1}", wh.FactoryCode, /* wh.SegmentCode,*/ wh.WarehouseCode), wh);
                }
            }
            objs = null;

            int iresult = 0;
            this.DataProvider.BeginTransaction();
            try
            {
                Hashtable htcycle = new Hashtable();
                for (int i = 0; i < items.Length; i++)
                {
                    WarehouseCycleCountDetail dtl = (WarehouseCycleCountDetail)items[i];
                    dtl.ItemCode = dtl.ItemCode.ToUpper();
                    //					try
                    //					{
                    //						int iCode = int.Parse(dtl.WarehouseCode);
                    //						if (iCode.ToString().Length < 4)
                    //							dtl.WarehouseCode = new string('0', 4 - dtl.WarehouseCode.Length) + dtl.WarehouseCode;
                    //						else
                    //							dtl.WarehouseCode = iCode.ToString();
                    //					}
                    //					catch
                    //					{}
                    //检查仓库状态
                    string strkey = string.Format("{0}-{1}", dtl.FactoryCode,/* dtl.SegmentCode,*/ dtl.WarehouseCode);
                    Warehouse wh = (Warehouse)htwh[strkey];
                    if (wh != null && wh.WarehouseStatus == Warehouse.WarehouseStatus_Cycle)
                    {
                        //检查导入的项是否在库存盘点明细表中存在
                        if (!htcycle.ContainsKey(wh.LastCycleCountCode))
                        {
                            objs = this.QueryWarehouseCycleCountDetail(string.Empty, wh.LastCycleCountCode, 1, int.MaxValue);
                            if (objs != null)
                            {
                                Hashtable httmp = new Hashtable();
                                for (int m = 0; m < objs.Length; m++)
                                {
                                    WarehouseCycleCountDetail dtltmp = (WarehouseCycleCountDetail)objs[m];
                                    httmp.Add(dtltmp.ItemCode, dtltmp);
                                }
                                htcycle.Add(wh.LastCycleCountCode, httmp);
                            }
                        }

                        Hashtable htdtl = (Hashtable)htcycle[wh.LastCycleCountCode];
                        //如果在盘点明细表中存在，则更新其实际数
                        if (htdtl.ContainsKey(dtl.ItemCode))
                        {
                            WarehouseCycleCountDetail dtltmp = (WarehouseCycleCountDetail)htdtl[dtl.ItemCode];
                            //此处可能有问题，导入数据的数量可能会被客户改变 ,需要避免此问题 TODO ForSimone
                            //							dtltmp.Qty = dtl.Qty;
                            //							dtltmp.LineQty = dtl.LineQty;
                            //							dtltmp.Warehouse2LineQty = dtl.Warehouse2LineQty;

                            dtltmp.PhysicalQty = dtl.PhysicalQty;
                            this.UpdateWarehouseCycleCountDetail(dtltmp);
                            iresult++;
                        }
                        else	//如果在盘点明细表中不存在，则检查在物料主档中是否存在
                        {
                            //如果在物料主档中存在，则在盘点明细中新增
                            if (listItem.Contains(dtl.ItemCode))
                            {
                                WarehouseCycleCountDetail dtltmp = this.CreateNewWarehouseCycleCountDetail();
                                dtltmp.CycleCountCode = wh.LastCycleCountCode;
                                dtltmp.FactoryCode = dtl.FactoryCode;
                                //dtltmp.SegmentCode = dtl.SegmentCode;
                                dtltmp.WarehouseCode = dtl.WarehouseCode;
                                dtltmp.ItemCode = dtl.ItemCode;
                                dtltmp.MaintainUser = userId;
                                dtltmp.Qty = 0;
                                dtltmp.LineQty = 0;
                                dtltmp.Warehouse2LineQty = dtltmp.Qty;
                                dtltmp.PhysicalQty = dtl.PhysicalQty;
                                this.AddWarehouseCycleCountDetail(dtltmp);
                                iresult++;
                            }
                            //如果在物料主档中不存在，则认为不合法
                        }

                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch
            {
                this.DataProvider.RollbackTransaction();
                iresult = 0;
            }
            return iresult;
        }

        //如果库房是盘点状态 ，并且正在导出盘点表，更新盘点结果到数据库
        public void ExportUpdateCycleResult(object[] cycleObjs)
        {
            if (cycleObjs == null || cycleObjs.Length == 0) return;
            this.DataProvider.BeginTransaction();
            try
            {
                Hashtable htcycle = new Hashtable();

                if (cycleObjs != null)
                {
                    for (int m = 0; m < cycleObjs.Length; m++)
                    {
                        WarehouseCycleCountDetail dtltmp = (WarehouseCycleCountDetail)cycleObjs[m];
                        if (!htcycle.Contains(dtltmp.ItemCode))
                            htcycle.Add(dtltmp.ItemCode, dtltmp);
                    }
                }
                object[] dbobjs = this.QueryWarehouseCycleCountDetail(string.Empty, ((WarehouseCycleCountDetail)cycleObjs[0]).CycleCountCode, 1, int.MaxValue);
                if (dbobjs != null && dbobjs.Length > 0)
                    foreach (WarehouseCycleCountDetail dbdtl in dbobjs)
                    {
                        if (htcycle.Contains(dbdtl.ItemCode))
                        {
                            WarehouseCycleCountDetail cycledtl = (WarehouseCycleCountDetail)htcycle[dbdtl.ItemCode];
                            dbdtl.Qty = cycledtl.Qty;
                            dbdtl.LineQty = cycledtl.LineQty;
                            dbdtl.Warehouse2LineQty = cycledtl.Warehouse2LineQty;
                            this.UpdateWarehouseCycleCountDetail(dbdtl);
                        }
                    }


                this.DataProvider.CommitTransaction();
            }
            catch
            {
                this.DataProvider.RollbackTransaction();
            }
        }



        /// <summary>
        /// 查询库存调整数据
        /// </summary>
        /// <returns>WarehouseCycleCountDetail数组</returns>
        public object[] QueryWarehouseCycleDetailInAdjustCheck(string warehouseCode, /*string segmentCode,*/ string factoryCode, object isAdjustResult, int inclusive, int exclusive)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("select {0} from TBLWAREHOURSE where 1=1 ");
            if (isAdjustResult == null || isAdjustResult.ToString() != "yes")
            {
                sb.Append(" and WHSTATUS = '" + Warehouse.WarehouseStatus_Cycle + "' ");
            }
            sb.Append(string.Format(" and WHCODE = '{0}' ", warehouseCode));
            //sb.Append(string.Format(" and SEGCODE = '{0}' ", segmentCode));
            sb.Append(string.Format(" and FACCODE = '{0}' ", factoryCode));
            //先读取仓库表
            object[] objs = this.DataProvider.CustomQuery(typeof(Warehouse), new SQLCondition(string.Format(sb.ToString(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse)))));
            if (objs == null)
                return null;
            //所有的盘点代码
            string strCycleCode = "";
            for (int i = 0; i < objs.Length; i++)
            {
                Warehouse wh = (Warehouse)objs[i];
                if (wh.LastCycleCountCode != "")
                {
                    strCycleCode += ",'" + wh.LastCycleCountCode + "'";
                }
            }
            strCycleCode = strCycleCode.Substring(1);
            //读取盘点数据
            string strSql = "select {0} from TBLWHCYLCEDETAIL where CYCLECODE in (" + strCycleCode + ") ";
            objs = this.DataProvider.CustomQuery(typeof(WarehouseCycleCountDetail), new PagerCondition(string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseCycleCountDetail))), "CYCLECODE,ITEMCODE", inclusive, exclusive));
            return objs;

        }

        /// <summary>
        /// 查询调整数据总数
        /// </summary>
        /// <returns></returns>
        public int QueryWarehouseCycleDetailInAdjustCheckCount(string warehouseCode, /*string segmentCode,*/ string factoryCode, object isAdjustResult)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("select {0} from TBLWAREHOURSE where 1=1 ");
            if (isAdjustResult == null || isAdjustResult.ToString() != "yes")
            {
                sb.Append(" and WHSTATUS = '" + Warehouse.WarehouseStatus_Cycle + "' ");
            }
            sb.Append(string.Format(" and WHCODE = '{0}' ", warehouseCode));
            //sb.Append(string.Format(" and SEGCODE = '{0}' ", segmentCode));
            sb.Append(string.Format(" and FACCODE = '{0}' ", factoryCode));
            //先读取仓库表
            object[] objs = this.DataProvider.CustomQuery(typeof(Warehouse), new SQLCondition(string.Format(sb.ToString(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(Warehouse)))));
            if (objs == null)
                return 0;
            //所有的盘点代码
            string strCycleCode = "";
            for (int i = 0; i < objs.Length; i++)
            {
                Warehouse wh = (Warehouse)objs[i];
                if (wh.LastCycleCountCode != "")
                {
                    strCycleCode += ",'" + wh.LastCycleCountCode + "'";
                }
            }
            strCycleCode = strCycleCode.Substring(1);
            //读取盘点数据
            string strSql = "select count(*) from TBLWHCYLCEDETAIL where CYCLECODE in (" + strCycleCode + ") ";
            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }

        #endregion

        #region WarehouseItem
        /// <summary>
        /// 仓库物料主档
        /// </summary>
        public WarehouseItem CreateNewWarehouseItem()
        {
            return new WarehouseItem();
        }

        public void AddWarehouseItem(WarehouseItem warehouseItem)
        {
            if (this.GetWarehouseItem(warehouseItem.ItemCode) == null)
                this._helper.AddDomainObject(warehouseItem);
        }
        public void AddWarehouseItem(string itemCode, string itemName, string itemUOM, string controlType)
        {
            WarehouseItem warehouseItem = new WarehouseItem();
            warehouseItem.ItemCode = itemCode;
            warehouseItem.ItemName = itemName;
            warehouseItem.ItemControlType = controlType;
            warehouseItem.ItemUOM = itemUOM;
            warehouseItem.MaintainUser = "";
            try
            {
                this._helper.AddDomainObject(warehouseItem);
            }
            catch
            { }
            warehouseItem = null;
        }
        public void AddWarehouseItem(object[] opbomdetailObjs)
        {
            if (opbomdetailObjs == null) return;
            foreach (OPBOMDetail opbomdetail in opbomdetailObjs)
            {
                try
                {
                    this.AddWarehouseItem(opbomdetail);
                }
                catch
                { }
            }
        }

        public void AddWarehouseItem(BenQGuru.eMES.Domain.MOModel.OPBOMDetail opdetail)
        {
            if (this.GetWarehouseItem(opdetail.OPBOMItemCode) == null)
            {
                WarehouseItem item = this.CreateNewWarehouseItem();
                item.ItemCode = opdetail.OPBOMItemCode;
                item.ItemName = opdetail.OPBOMItemName;
                item.ItemUOM = opdetail.OPBOMItemUOM;
                item.MaintainDate = opdetail.MaintainDate;
                item.MaintainTime = opdetail.MaintainTime;
                item.MaintainUser = opdetail.MaintainUser;
                item.ItemControlType = WarehouseItem.WarehouseItemControlType_Lot;
                if (opdetail.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_KEYPARTS)
                {
                    //如果opbomdetail中物料管控类型是keyparts,则物料的管控类型是单件料
                    item.ItemControlType = WarehouseItem.WarehouseItemControlType_Single;
                }
                else if (opdetail.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_LOT)
                {
                    item.ItemControlType = WarehouseItem.WarehouseItemControlType_Lot;
                }
                this.AddWarehouseItem(item);
            }
        }
        public void AddWarehouseItem(BenQGuru.eMES.Domain.MOModel.SBOM[] sboms)
        {
            for (int i = 0; i < sboms.Length; i++)
            {
                if (this.GetWarehouseItem(sboms[i].SBOMItemCode) == null)
                {
                    WarehouseItem item = this.CreateNewWarehouseItem();
                    item.ItemCode = sboms[i].SBOMItemCode;
                    item.ItemName = sboms[i].SBOMItemName;
                    item.ItemUOM = sboms[i].SBOMItemUOM;
                    item.ItemControlType = WarehouseItem.WarehouseItemControlType_Lot;
                    item.MaintainUser = sboms[i].MaintainUser;
                    this.AddWarehouseItem(item);
                }
            }
        }

        public void UpdateWarehouseItem(WarehouseItem warehouseItem)
        {
            this._helper.UpdateDomainObject(warehouseItem);
        }

        public void DeleteWarehouseItem(WarehouseItem warehouseItem)
        {
            /*
            BenQGuru.eMES.MOModel.OPBOMFacade opbom = new BenQGuru.eMES.MOModel.OPBOMFacade(this.DataProvider);
            if (opbom.IsOPBOMItemExist(warehouseItem.ItemCode))
            {
                throw new Exception("$Delete_WarehouseItem_Error_UseInOPBOM");
            }
            */
            if (IsExist(typeof(WarehouseStock), new string[] { "ItemCode" }, new object[] { warehouseItem.ItemCode }))
            {
                ExceptionManager.Raise(this.GetType(), string.Format("$Delete_WarehouseItem_Error_UseInWarehouseStock [$WarehouseItemCode={0}]", warehouseItem.ItemCode));
            }
            if (IsExist(typeof(WarehouseTicketDetail), new string[] { "ItemCode" }, new object[] { warehouseItem.ItemCode }))
            {
                ExceptionManager.Raise(this.GetType(), string.Format("$Delete_WarehouseItem_Error_UseInWarehouseTicketDetail [$WarehouseItemCode={0}]", warehouseItem.ItemCode));
            }
            this._helper.DeleteDomainObject(warehouseItem);
        }

        public void DeleteWarehouseItem(WarehouseItem[] warehouseItem)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < warehouseItem.Length; i++)
                {
                    this.DeleteWarehouseItem(warehouseItem[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message);
            }
        }

        public object GetWarehouseItem(string itemCode)
        {
            return this.DataProvider.CustomSearch(typeof(WarehouseItem), new object[] { itemCode });
        }

        /// <summary>
        /// ** 功能描述:	查询WarehouseItem的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <returns> WarehouseItem的总记录数</returns>
        public int QueryWarehouseItemCount(string itemCode, string itemName)
        {
            return this.QueryWarehouseItemCount(itemCode, itemName, string.Empty);
        }
        public int QueryWarehouseItemCount(string itemCode, string itemName, string controlType)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWHITEM where 1=1 and ITEMCODE like '{0}%' and ITEMNAME like '{1}%' and ITEMCONTROL like '{2}%' ", itemCode, itemName, controlType)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询WarehouseItem
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> WarehouseItem数组</returns>
        public object[] QueryWarehouseItem(string itemCode, string itemName, int inclusive, int exclusive)
        {
            return this.QueryWarehouseItem(itemCode, itemName, string.Empty, inclusive, exclusive);
        }
        public object[] QueryWarehouseItem(string itemCode, string itemName, string controlType, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseItem), new PagerCondition(string.Format("select {0} from TBLWHITEM where 1=1 and ITEMCODE like '{1}%' and ITEMNAME like '{2}%' and ITEMCONTROL like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseItem)), itemCode, itemName, controlType), "ITEMCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的WarehouseItem
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>WarehouseItem的总记录数</returns>
        public object[] GetAllWarehouseItem()
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseItem), new SQLCondition(string.Format("select {0} from TBLWHITEM order by ITEMCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseItem)))));
        }
        public object[] GetAllWarehouseItemSingle()
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseItem), new SQLCondition(string.Format("select {0} from TBLWHITEM where ITEMCONTROL='{1}' order by ITEMNAME", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseItem)), WarehouseItem.WarehouseItemControlType_Single)));
        }

        /// <summary>
        /// ** 功能描述:	物料初始化，导入OP BOM中的物料资料
        /// ** 作 者:		Icyer Yang
        /// ** 日 期:		2005-8-1 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns></returns>
        public void ImportAllItem(string userId)
        {
            BenQGuru.eMES.MOModel.OPBOMFacade facade = new BenQGuru.eMES.MOModel.OPBOMFacade(this.DataProvider);
            object[] objboms = facade.QueryOPBOMDetail(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1, int.MaxValue, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            Hashtable htitems = new Hashtable();
            int iResult = 0;
            for (int i = 0; i < objboms.Length; i++)
            {
                BenQGuru.eMES.Domain.MOModel.OPBOMDetail opbom = (BenQGuru.eMES.Domain.MOModel.OPBOMDetail)objboms[i];
                if (!htitems.ContainsKey(opbom.OPBOMItemCode) && opbom.OPBOMItemControlType != BOMItemControlType.ITEM_CONTROL_NOCONTROL)
                {
                    WarehouseItem item = new WarehouseItem();
                    item.ItemCode = opbom.OPBOMItemCode;
                    if (opbom.OPBOMItemName != string.Empty)
                        item.ItemName = opbom.OPBOMItemName;
                    else
                        item.ItemName = item.ItemCode;
                    item.ItemUOM = opbom.OPBOMItemUOM;
                    if (opbom.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_KEYPARTS)
                        item.ItemControlType = WarehouseItem.WarehouseItemControlType_Single;
                    else
                        item.ItemControlType = WarehouseItem.WarehouseItemControlType_Lot;
                    item.MaintainUser = userId;
                    try
                    {
                        this.AddWarehouseItem(item);
                        htitems.Add(item.ItemCode, item.ItemCode);
                        iResult++;
                        item = null;
                    }
                    catch
                    {
                    }
                }
            }
            htitems = null;
            objboms = null;
        }

        /// <summary>
        /// 获得所有的计量单位
        /// </summary>
        /// <returns>Parameter数组</returns>
        public object[] GetWarehouseItemUOM()
        {
            BenQGuru.eMES.BaseSetting.SystemSettingFacade ssfacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(this._domainDataProvider);
            object[] objs = ssfacade.GetParametersByParameterGroup(BenQGuru.eMES.Domain.Warehouse.WarehouseItem.WarehouseItemUOMGroup);
            ssfacade = null;
            return objs;
        }

        #endregion

        #region WarehouseStock
        /// <summary>
        /// 仓库物料
        /// </summary>
        public WarehouseStock CreateNewWarehouseStock()
        {
            return new WarehouseStock();
        }

        public void AddWarehouseStock(WarehouseStock warehouseStock)
        {
            this._helper.AddDomainObject(warehouseStock);
        }

        public void UpdateWarehouseStock(WarehouseStock warehouseStock)
        {
            this._helper.UpdateDomainObject(warehouseStock);
        }

        public void DeleteWarehouseStock(WarehouseStock warehouseStock)
        {
            this._helper.DeleteDomainObject(warehouseStock);
        }

        public void DeleteWarehouseStock(WarehouseStock[] warehouseStock)
        {
            this._helper.DeleteDomainObject(warehouseStock);
        }

        public object GetWarehouseStock(string itemCode, string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            return this.DataProvider.CustomSearch(typeof(WarehouseStock), new object[] { itemCode, warehouseCode, factoryCode });
        }

        public void UpdateWarehouseStockQty(WarehouseStock warehouseStock, decimal qty)
        {
            string SQL = String.Empty;

            if (qty > 0)
            {
                SQL = "update TBLWHSTOCK set OPENQTY = OPENQTY + " + qty.ToString()
                    + " where FACCODE='" + warehouseStock.FactoryCode
                    + "' and WHCODE='" + warehouseStock.WarehouseCode
                    + "' and ITEMCODE='" + warehouseStock.ItemCode + "'"
                    //+ "' and SEGCODE='" + warehouseStock.SegmentCode + "'" 
                    ;
            }
            else if (qty < 0)
            {
                SQL = "update TBLWHSTOCK set OPENQTY = OPENQTY - " + System.Math.Abs(qty).ToString()
                + " where FACCODE='" + warehouseStock.FactoryCode
                + "' and WHCODE='" + warehouseStock.WarehouseCode
                + "' and ITEMCODE='" + warehouseStock.ItemCode + "'"
                    //+ "' and SEGCODE='" + warehouseStock.SegmentCode + "'"
                    ;
            }
            if (qty != 0)
            {
                //this.DataProvider.CustomExecute(new SQLCondition(SQL));
                //为防止死锁将ＳＱＬ语句缓存起来，最后调用 ExecCacheSQL 一起执行 joe song 20060227
                this._cacheSQL.Add(CacheSequence.WHS + warehouseStock.FactoryCode + warehouseStock.WarehouseCode + warehouseStock.ItemCode + Guid.NewGuid().ToString(),
                                    SQL);
            }
        }

        public object[] GetWarehouseStockPro(string itemCode, string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("select {0} from TBLWHSTOCK where 1=1 ");
            if (itemCode != string.Empty)
            {
                sb.Append(string.Format(" and ITEMCODE = '{0}' ", itemCode));
            }
            if (warehouseCode != string.Empty)
            {
                sb.Append(string.Format(" and WHCODE = '{0}' ", warehouseCode));
            }
            //			if (segmentCode != string.Empty)
            //			{
            //				sb.Append(string.Format(" and SEGCODE = '{0}' ", segmentCode));
            //			}
            if (factoryCode != string.Empty)
            {
                sb.Append(string.Format(" and FACCODE = '{0}' ", factoryCode));
            }
            return this.DataProvider.CustomQuery(typeof(WarehouseStock), new SQLCondition(string.Format(sb.ToString(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseStock)))));
        }

        /// <summary>
        /// ** 功能描述:	查询WarehouseStock的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="warehouseCode">WarehouseCode，模糊查询</param>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <param name="factoryCode">FactoryCode，模糊查询</param>
        /// <returns> WarehouseStock的总记录数</returns>
        public int QueryWarehouseStockCount(string itemCode, string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWHSTOCK where 1=1 and ITEMCODE like '{0}%'  and WHCODE = '{1}' and FACCODE = '{2}' ", itemCode, warehouseCode, /*segmentCode,*/ factoryCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询WarehouseStock
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="warehouseCode">WarehouseCode，模糊查询</param>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <param name="factoryCode">FactoryCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> WarehouseStock数组</returns>
        public object[] QueryWarehouseStock(string itemCode, string warehouseCode, /*string segmentCode,*/ string factoryCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseStock), new PagerCondition(string.Format("select {0} from TBLWHSTOCK where 1=1 and ITEMCODE like '{1}%'  and WHCODE = '{2}' and FACCODE = '{3}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseStock)), itemCode, warehouseCode, /*segmentCode,*/ factoryCode), "FACCODE,WHCODE,ITEMCODE", inclusive, exclusive));
        }
        /// <summary>
        /// 盘点时查询库存数量
        /// </summary>
        /// <returns></returns>
        public object[] QueryWarehouseStockInCheck(string itemCode, string warehouseCode, string factoryCode, int inclusive, int exclusive)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("select {0} from TBLWHSTOCK where 1=1 ");
            sb.Append(string.Format(" and WHCODE = '{0}' ", warehouseCode));
            //sb.Append(string.Format(" and SEGCODE = '{0}' ", segmentCode));
            sb.Append(string.Format(" and FACCODE = '{0}' ", factoryCode));
            sb.Append(" and OPENQTY != 0 ");
            //读取库存表
            object[] objs = this.DataProvider.CustomQuery(typeof(WarehouseStock), new PagerCondition(string.Format(sb.ToString(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseStock))), "FACCODE,WHCODE,ITEMCODE", inclusive, exclusive));
            if (objs == null)
                return objs;

            //读取盘点库
            string strFactory = "", /*strSegment = "",*/ strWarehouseCode = "";
            Hashtable ht = new Hashtable();
            Hashtable htnormalwh = new Hashtable();
            for (int i = 0; i < objs.Length; i++)
            {
                WarehouseStock stock = (WarehouseStock)objs[i];
                if (stock.FactoryCode != strFactory || /*stock.SegmentCode != strSegment ||*/ stock.WarehouseCode != strWarehouseCode)
                {
                    strFactory = stock.FactoryCode;
                    //strSegment = stock.SegmentCode;
                    strWarehouseCode = stock.WarehouseCode;
                    object obj = this.GetWarehouse(strWarehouseCode, strFactory);
                    Warehouse wh = (Warehouse)obj;
                    //如果处于盘点状态，则从盘点表里获取数据
                    if (wh != null && wh.WarehouseStatus == Warehouse.WarehouseStatus_Cycle)
                    {
                        object[] objcycle = this.QueryWarehouseCycleCountDetail(string.Empty, wh.LastCycleCountCode, 1, int.MaxValue);
                        if (objcycle != null)
                        {
                            for (int m = 0; m < objcycle.Length; m++)
                            {
                                WarehouseCycleCountDetail dtl = (WarehouseCycleCountDetail)objcycle[m];
                                dtl.FactoryCode = strFactory;
                                dtl.WarehouseCode = strWarehouseCode;
                                //dtl.SegmentCode = strSegment;
                                dtl.Warehouse2LineQty = dtl.Qty;	//账面数默认为库房离散数量，用于显示
                                ht.Add(ht.Count.ToString(), dtl);
                            }
                        }
                    }
                    else
                    {
                        htnormalwh.Add(string.Format("{0}-{1}", strFactory,/* strSegment,*/ strWarehouseCode), "");
                    }
                }
                if (htnormalwh.ContainsKey(string.Format("{0}-{1}", stock.FactoryCode, /*stock.SegmentCode,*/ stock.WarehouseCode)))
                {
                    ht.Add(ht.Count.ToString(), stock);
                }
            }

            //组建返回值
            objs = new object[ht.Count];
            for (int i = 0; i < ht.Count; i++)
            {
                objs[i] = ht[i.ToString()];
            }
            return objs;
        }


        /// <summary>
        /// 盘点时查询库存数量 , 会盘点在制品
        /// </summary>
        /// <returns></returns>
        public object[] QueryWarehouseStockInCheck2(string itemCode, string warehouseCode,/* string segmentCode,*/ string factoryCode, int inclusive, int exclusive)
        {
            object[] objs = this.QueryWarehouseStockInCheck(itemCode, warehouseCode,/* segmentCode,*/ factoryCode, inclusive, exclusive);

            //匹配在制品盘点结果（上料记录）
            Hashtable _ht = this.CycleSimulation(warehouseCode);

            foreach (WarehouseCycleCountDetail dtl in objs)
            {
                if (_ht.Contains(dtl.ItemCode))
                {
                    //dtl.Qty ;														//离散数量
                    dtl.LineQty = Convert.ToDecimal(_ht[dtl.ItemCode]);				//在制品虚拆数量
                    dtl.Warehouse2LineQty = dtl.Qty + dtl.LineQty;					//账面数
                }
            }

            #region 下料记录无须盘点了

            //下料记录是从上料记录update而来，已经归还给库房,无须盘点
            //匹配在制品下料盘点
            /*
            Hashtable _downht =this.CycleDownSimulation(warehouseCode);

            foreach(WarehouseCycleCountDetail dtl in objs)
            {
                if(_downht.Contains(dtl.ItemCode))
                {
                    //dtl.Qty ;																//离散数量
                    dtl.LineQty = dtl.LineQty - Convert.ToDecimal(_downht[dtl.ItemCode]);	//需要减去下料数量，下料的数量已经返还库房
                    dtl.Warehouse2LineQty = dtl.Qty + dtl.LineQty;							//账面数
                }
            }
            */

            #endregion

            #region 维修换料无须盘点了

            //匹配维修换料记录的上料记录 和 下料记录
            //维修换料没有更新在制品的上料信息
            //上料加在在制品上料记录中,下料从在制品上料记录减掉.
            //			Hashtable _tsitemht =this.CycleTSItem(warehouseCode);
            //			Hashtable _loadinght = (Hashtable)_tsitemht["_loadinght"];		//维系换料上料记录
            //			Hashtable _downht = (Hashtable)_tsitemht["_downht"];			//维修换料下料记录
            //
            //			//加上料
            //			foreach(WarehouseCycleCountDetail dtl in objs)
            //			{
            //				if(_loadinght.Contains(dtl.ItemCode))
            //				{
            //					//dtl.Qty ;																//离散数量
            //					dtl.LineQty = dtl.LineQty + Convert.ToDecimal(_loadinght[dtl.ItemCode]);//从上料记录中加上 维修换料上料记录
            //					dtl.Warehouse2LineQty = dtl.Qty + dtl.LineQty;							//账面数
            //				}
            //			}
            //
            //			//减下料
            //			foreach(WarehouseCycleCountDetail dtl in objs)
            //			{
            //				if(_downht.Contains(dtl.ItemCode))
            //				{
            //					//dtl.Qty ;																//离散数量
            //					dtl.LineQty = dtl.LineQty - Convert.ToDecimal(_downht[dtl.ItemCode]);	//从上料记录中减去 维修换料下料记录
            //					dtl.Warehouse2LineQty = dtl.Qty + dtl.LineQty;							//账面数
            //				}
            //			}

            #endregion

            return objs;
        }
        public int QueryWarehouseStockInCheckCount(string itemCode, string warehouseCode, /*string segmentCode,*/ string factoryCode)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("select count(*) from TBLWHSTOCK where 1=1 ");
            sb.Append(string.Format(" and WHCODE = '{0}' ", warehouseCode));
            //sb.Append(string.Format(" and SEGCODE = '{0}' ", segmentCode));
            sb.Append(string.Format(" and FACCODE = '{0}' ", factoryCode));
            sb.Append(" and OPENQTY != 0 ");
            return this.DataProvider.GetCount(new SQLCondition(sb.ToString()));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的WarehouseStock
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>WarehouseStock的总记录数</returns>
        public object[] GetAllWarehouseStock()
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseStock), new SQLCondition(string.Format("select {0} from TBLWHSTOCK order by ITEMCODE,WHCODE,FACCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseStock)))));
        }

        /// <summary>
        /// 更新库存量
        /// </summary>
        public decimal UpdateWarehouseStockQty(string factoryCode, string warehouseCode, string itemCode, string operation, decimal qty)
        {
            decimal decRet = 0;
            object obj = this.GetWarehouseStock(itemCode, warehouseCode, factoryCode);
            if (obj != null)
            {
                string strSql = string.Format("update TBLWHSTOCK set OPENQTY=OPENQTY{0}{1} where FACCODE = '{2}' and WHCODE = '{3}' and ITEMCODE = '{4}' ", operation, qty.ToString(), factoryCode, warehouseCode, itemCode);
                //this.DataProvider.CustomExecute(new SQLCondition(strSql));
                //为防止死锁将ＳＱＬ语句缓存起来，最后调用 ExecCacheSQL 一起执行 joe song 20060227
                this._cacheSQL.Add(CacheSequence.WHS + factoryCode + warehouseCode + itemCode + Guid.NewGuid().ToString(),
                                strSql);
                decRet = ((WarehouseStock)obj).OpenQty + decimal.Parse(operation + "1") * qty;
            }
            else
            {
                operation += "1";
                WarehouseStock stock = this.CreateNewWarehouseStock();
                stock.FactoryCode = factoryCode;
                //stock.SegmentCode = segmentCode;
                stock.WarehouseCode = warehouseCode;
                stock.ItemCode = itemCode;
                stock.OpenQty = int.Parse(operation) * qty;
                stock.MaintainUser = " ";
                this.AddWarehouseStock(stock);
                decRet = stock.OpenQty;
                stock = null;
            }
            return decRet;
        }

        /// <summary>
        /// ** 功能描述:	导入WarehouseStock
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>成功导入的记录数</returns>
        public int ImportWarehouseStock(object[] items, string userId)
        {
            //读取物料主档资料
            Hashtable htItems = new Hashtable();
            object[] objs = this.GetAllWarehouseItem();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    WarehouseItem item = (WarehouseItem)objs[i];
                    htItems.Add(item.ItemCode, item.ItemName);
                    item = null;
                }
            }
            objs = null;

            //将所有仓库资料取出
            Hashtable htWarehouse = new Hashtable();
            objs = this.GetAllWarehouse();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    Warehouse item = (Warehouse)objs[i];
                    htWarehouse.Add(string.Format("{0}-{1}", item.FactoryCode, /* item.SegmentCode,*/ item.WarehouseCode), "");
                    item = null;
                }
            }
            objs = null;

            //逐条导入
            Hashtable htImported = new Hashtable();
            int iResult = 0;
            for (int i = 0; i < items.Length; i++)
            {
                WarehouseStock item = (WarehouseStock)items[i];
                item.ItemCode = item.ItemCode.ToUpper();
                try
                {
                    int iCode = int.Parse(item.WarehouseCode);
                    item.WarehouseCode = new string('0', 4 - item.WarehouseCode.Length) + item.WarehouseCode;
                }
                catch
                { }
                if (htItems.ContainsKey(item.ItemCode) && item.FactoryCode != "" && /*item.SegmentCode != "" && */
                    item.WarehouseCode != "" && item.ItemCode != "" && item.OpenQty > 0 &&
                    htWarehouse.ContainsKey(string.Format("{0}-{1}", item.FactoryCode, /*item.SegmentCode,*/ item.WarehouseCode)))
                {
                    try
                    {
                        WarehouseStock ws = new WarehouseStock();
                        ws.FactoryCode = item.FactoryCode;
                        //ws.SegmentCode = item.SegmentCode;
                        ws.WarehouseCode = item.WarehouseCode;
                        ws.ItemCode = item.ItemCode;
                        ws.OpenQty = item.OpenQty;
                        ws.MaintainUser = userId;
                        this.AddWarehouseStock(ws);

                        //加入交易Log
                        string strKey = string.Format("{0}-{1}", ws.FactoryCode, /* ws.SegmentCode,*/ ws.WarehouseCode);
                        if (!htImported.ContainsKey(strKey))
                        {
                            string strTicketNo = this.AddWarehouseTicketFromWarehouseInit(ws.FactoryCode, /* ws.SegmentCode,*/ ws.WarehouseCode, userId);
                            htImported[strKey] = new string[] { strTicketNo, "0" };
                        }
                        string[] strTktNo = (string[])htImported[strKey];
                        strTktNo[1] = (int.Parse(strTktNo[1]) + 1).ToString();
                        AddWarehouseTicketDetailFromWarehouseInit(strTktNo[0], int.Parse(strTktNo[1]), ws.ItemCode, htItems[ws.ItemCode].ToString(), ws.OpenQty, userId);
                        htImported[strKey] = strTktNo;

                        item = null;
                        ws = null;
                        iResult++;
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message;
                    }
                }
            }
            htItems = null;
            htImported = null;

            return iResult;
        }
        private string AddWarehouseTicketFromWarehouseInit(string factoryCode, /*string segmentCode, */ string warehouseCode, string userId)
        {
            //TODO: 查询期初单代码
            string strTransCode = GetTransactionTypeByName("WarehouseInit");
            return this.AddWarehouseTicket(factoryCode, /*segmentCode,*/ warehouseCode, strTransCode, userId);
        }
        private void AddWarehouseTicketDetailFromWarehouseInit(string ticketNo, int iSequence, string itemCode, string itemName, decimal qty, string userId)
        {
            this.AddWarehouseTicketDetail(ticketNo, iSequence, "", itemCode, itemName, qty, qty, userId);
        }

        #endregion

        #region WarehouseTicket
        /// <summary>
        /// 交易单主档
        /// </summary>
        public WarehouseTicket CreateNewWarehouseTicket()
        {
            return new WarehouseTicket();
        }

        public void AddWarehouseTicket(WarehouseTicket warehouseTicket)
        {
            warehouseTicket.TicketNo = this.GetTicketSeq(warehouseTicket.TicketNo.Substring(0, 1), true);
            warehouseTicket.TicketUser = warehouseTicket.MaintainUser;
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            warehouseTicket.TicketDate = FormatHelper.TODateInt(dtNow);
            warehouseTicket.TicketTime = FormatHelper.TOTimeInt(dtNow);
            warehouseTicket.TransactionStatus = WarehouseTicket.TransactionStatusEnum.Pending.ToString();
            this._helper.AddDomainObject(warehouseTicket);
            this.AddWarehouseUseCount(warehouseTicket.WarehouseCode, /*warehouseTicket.SegmentCode, */ warehouseTicket.FactoryCode);
            this.AddWarehouseUseCount(warehouseTicket.TOWarehouseCode, /* warehouseTicket.TOSegmentCode,*/ warehouseTicket.TOFactoryCode);
            this.UpdateTransactionTypeInit(warehouseTicket.TransactionTypeCode, "1");
        }

        public void UpdateWarehouseTicket(WarehouseTicket warehouseTicket, string userId)
        {
            WarehouseTicket ticket = (WarehouseTicket)this.GetWarehouseTicket(warehouseTicket.TicketNo);
            //只能编辑自己创建的交易
            if (ticket.TicketUser != userId)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Update_WarehouseTicket_TicketUserError");
                return;
            }
            warehouseTicket.TicketUser = ticket.TicketUser;
            warehouseTicket.TicketDate = ticket.TicketDate;
            warehouseTicket.TicketTime = ticket.TicketTime;
            warehouseTicket.TransactionStatus = WarehouseTicket.TransactionStatusEnum.Pending.ToString();
            this._helper.UpdateDomainObject(warehouseTicket);
            if (ticket.FactoryCode != warehouseTicket.FactoryCode || /*ticket.SegmentCode != warehouseTicket.SegmentCode ||*/ ticket.WarehouseCode != warehouseTicket.WarehouseCode)
            {
                this.DeleteWarehouseUseCount(ticket.WarehouseCode, /*ticket.SegmentCode,*/ ticket.FactoryCode);
                this.AddWarehouseUseCount(warehouseTicket.WarehouseCode, /* warehouseTicket.SegmentCode,*/ warehouseTicket.FactoryCode);
            }
            if (ticket.TOFactoryCode != warehouseTicket.TOFactoryCode || /*ticket.TOSegmentCode != warehouseTicket.TOSegmentCode ||*/ ticket.TOWarehouseCode != warehouseTicket.TOWarehouseCode)
            {
                this.DeleteWarehouseUseCount(ticket.TOWarehouseCode, /*ticket.TOSegmentCode,*/ ticket.TOFactoryCode);
                this.AddWarehouseUseCount(warehouseTicket.TOWarehouseCode, /*warehouseTicket.TOSegmentCode,*/ warehouseTicket.TOFactoryCode);
            }
        }

        public void DeleteWarehouseTicket(WarehouseTicket warehouseTicket, string userId)
        {
            //只能删除自己创建的交易
            if (warehouseTicket.TicketUser != userId)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Delete_WarehouseTicket_TicketUserError");
                return;
            }
            //不能删除已交易的单据
            if (warehouseTicket.TransactionStatus == WarehouseTicket.TransactionStatusEnum.Transaction.ToString() ||
                warehouseTicket.TransactionStatus == WarehouseTicket.TransactionStatusEnum.Closed.ToString())
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Delete_WarehouseTicket_TicketTransactionStatus");
                return;
            }
            this._helper.DeleteDomainObject(warehouseTicket);
            this.DataProvider.CustomExecute(new SQLCondition(string.Format("delete from TBLWHTKTDETAIL where TKTNO = '{0}' ", warehouseTicket.TicketNo)));
            this.DeleteWarehouseUseCount(warehouseTicket.WarehouseCode, /*warehouseTicket.SegmentCode,*/ warehouseTicket.FactoryCode);
            this.DeleteWarehouseUseCount(warehouseTicket.TOWarehouseCode, /* warehouseTicket.TOSegmentCode,*/ warehouseTicket.TOFactoryCode);
        }

        public void DeleteWarehouseTicket(WarehouseTicket[] warehouseTicket, string userId)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < warehouseTicket.Length; i++)
                {
                    this.DeleteWarehouseTicket(warehouseTicket[i], userId);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                ExceptionManager.Raise(this.GetType(), ex.Message, ex);
                this.DataProvider.RollbackTransaction();
            }
        }

        public object GetWarehouseTicket(string ticketNo)
        {
            return this.DataProvider.CustomSearch(typeof(WarehouseTicket), new object[] { ticketNo });
        }

        /// <summary>
        /// ** 功能描述:	查询WarehouseTicket的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="ticketNo">TicketNo，模糊查询</param>
        /// <returns> WarehouseTicket的总记录数</returns>
        public int QueryWarehouseTicketCount(string ticketNo, string transType, string refCode, string status, string dateFrom, string dateTo, string moCode, string userCode)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(string.Format("select count(*) from TBLWHTKT where 1=1 and TKTNO like '{0}%' and TRANSTYPECODE like '{1}%' ", ticketNo, transType));
            sb.Append(" and TKTNO>='0' ");
            if (status == string.Empty)
                sb.Append(string.Format(" and (TRANSSTATUS='{0}' or TRANSSTATUS='{1}' or TRANSSTATUS='{2}') ", WarehouseTicket.TransactionStatusEnum.Pending.ToString(), WarehouseTicket.TransactionStatusEnum.Transaction.ToString(), WarehouseTicket.TransactionStatusEnum.Closed.ToString()));
            else
                sb.Append(string.Format(" and TRANSSTATUS='{0}' ", status));
            if (refCode != string.Empty)
                sb.Append(string.Format(" and REFCODE like '{0}%' ", refCode));
            if (dateFrom != string.Empty)
                sb.Append(string.Format(" and MDATE>={0} ", FormatHelper.TODateInt(dateFrom)));
            if (dateTo != string.Empty)
                sb.Append(string.Format(" and MDATE<={0} ", FormatHelper.TODateInt(dateTo)));
            if (moCode != string.Empty)
                sb.Append(string.Format(" and MOCODE LIKE '{0}%' ", moCode));
            if (userCode != string.Empty)
                sb.Append(string.Format(" and MUSER='{0}' ", userCode));
            return this.DataProvider.GetCount(new SQLCondition(sb.ToString()));
        }

        /// <summary>
        /// ** 功能描述:	分页查询WarehouseTicket
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="ticketNo">TicketNo，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> WarehouseTicket数组</returns>
        public object[] QueryWarehouseTicket(string ticketNo, string transType, string refCode, string status, string dateFrom, string dateTo, string moCode, string userCode, int inclusive, int exclusive)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(string.Format("select {0} from TBLWHTKT where 1=1 and TKTNO like '{1}%' and TRANSTYPECODE like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseTicket)), ticketNo, transType));
            sb.Append(" and TKTNO>='0' ");
            if (status == string.Empty)
                sb.Append(string.Format(" and (TRANSSTATUS='{0}' or TRANSSTATUS='{1}' or TRANSSTATUS='{2}') ", WarehouseTicket.TransactionStatusEnum.Pending.ToString(), WarehouseTicket.TransactionStatusEnum.Transaction.ToString(), WarehouseTicket.TransactionStatusEnum.Closed.ToString()));
            else
                sb.Append(string.Format(" and TRANSSTATUS='{0}' ", status));
            if (refCode != string.Empty)
                sb.Append(string.Format(" and REFCODE like '{0}%' ", refCode));
            if (dateFrom != string.Empty)
                sb.Append(string.Format(" and MDATE>={0} ", FormatHelper.TODateInt(dateFrom)));
            if (dateTo != string.Empty)
                sb.Append(string.Format(" and MDATE<={0} ", FormatHelper.TODateInt(dateTo)));
            if (moCode != string.Empty)
                sb.Append(string.Format(" and MOCODE LIKE '{0}%' ", moCode));
            if (userCode != string.Empty)
                sb.Append(string.Format(" and MUSER='{0}' ", userCode));
            return this.DataProvider.CustomQuery(typeof(WarehouseTicket), new PagerCondition(sb.ToString(), " MDATE desc, MTIME desc ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的WarehouseTicket
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>WarehouseTicket的总记录数</returns>
        public object[] GetAllWarehouseTicket()
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseTicket), new SQLCondition(string.Format("select {0} from TBLWHTKT order by TKTNO", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseTicket)))));
        }

        /// <summary>
        /// ** 功能描述:	获取当前交易序列号
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>交易号</returns>
        public string GetTicketSeq(string prefix)
        {
            return GetTicketSeq(prefix, false);
        }
        public string GetTicketSeq(string prefix, bool update)
        {
            int iseq = 1;
            object[] objs = this.DataProvider.CustomQuery(typeof(WarehouseTicketSeq), new SQLCondition("select NEXTSEQ from TBLTICKETSEQ"));
            if (objs != null)
            {
                WarehouseTicketSeq seq = (WarehouseTicketSeq)objs[0];
                iseq = seq.NextSeq;
                seq = null;
            }
            if (update == true)
            {
                if (objs != null)
                    this.DataProvider.CustomExecute(new SQLCondition("update TBLTICKETSEQ set nextseq=nextseq+1"));
                else
                    this.DataProvider.CustomExecute(new SQLCondition("insert into TBLTICKETSEQ (NEXTSEQ) values (2) "));
            }
            objs = null;
            string strseq = prefix + (iseq.ToString().Length < 7 ? (new string('0', 7 - iseq.ToString().Length)) : "") + iseq.ToString();
            return strseq;
        }

        /// <summary>
        /// 单据做实际交易动作
        /// </summary>
        /// <param name="ticketNo"></param>
        /// <param name="userId"></param>
        public void WarehouseTicketDoTrans(string ticketNo, Hashtable currentQty, string userId)
        {
            try
            {
                bool bPass = false;
                foreach (object objQty in currentQty.Values)
                {
                    if (Convert.ToDecimal(objQty) != 0)
                    {
                        bPass = true;
                        break;
                    }
                }
                if (!bPass)
                    return;

                this.DataProvider.BeginTransaction();
                WarehouseTicket ticket = (WarehouseTicket)this.GetWarehouseTicket(ticketNo);
                if (ticket.TransactionStatus != WarehouseTicket.TransactionStatusEnum.Pending.ToString() &&
                    ticket.TransactionStatus != WarehouseTicket.TransactionStatusEnum.Transaction.ToString())
                {
                    throw new Exception("$Error_Warehouse_Ticket_Transaction_Status");
                }

                //检查仓库状态
                if (this.CheckWarehouseStatus(ticket) == false)
                    return;

                //更新单据
                this.UpdateWarehouseTicketTrans(ticket, userId);

                //更新库存量
                this.WarehouseTicketTransUpdateWarehouseStock(ticket, currentQty);

                //更新工单物料累计
                this.WarehouseTicketTransUpdateMoStock(ticket, currentQty);

                //更新交易明细
                this.WarehouseTicketTransUpdateTicketDetail(ticketNo, currentQty);

                //执行缓存的ＳＱＬ，包括更新工单和库存 joesong 20060223
                this.ExecCacheSQL();

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message);
            }
        }
        /// <summary>
        /// 单据交易时检查仓库状态
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        private bool CheckWarehouseStatus(WarehouseTicket ticket)
        {
            if (ticket.WarehouseCode != "" && /*ticket.SegmentCode != "" && */ ticket.FactoryCode != "" &&
                this.GetWarehouseStatus(ticket.WarehouseCode, /*ticket.SegmentCode,*/ ticket.FactoryCode) != Warehouse.WarehouseStatus_Normal)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_TicketDoTrans_WarehouseCycleCount");
                return false;
            }
            if (ticket.TOWarehouseCode != "" && /*ticket.TOSegmentCode != "" &&*/ ticket.TOFactoryCode != "" &&
                this.GetWarehouseStatus(ticket.TOWarehouseCode, /*ticket.TOSegmentCode,*/ ticket.TOFactoryCode) != Warehouse.WarehouseStatus_Normal)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_TicketDoTrans_WarehouseCycleCount");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 单据交易时更新单据主档
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="userId"></param>
        private void UpdateWarehouseTicketTrans(WarehouseTicket ticket, string userId)
        {
            ticket.TransactionUser = userId;
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            ticket.TransactionDate = FormatHelper.TODateInt(dtNow);
            ticket.TransactionTime = FormatHelper.TOTimeInt(dtNow);
            ticket.TransactionStatus = WarehouseTicket.TransactionStatusEnum.Transaction.ToString();
            this._helper.UpdateDomainObject(ticket);
        }
        /// <summary>
        /// 单据交易时更新库存
        /// </summary>
        private void WarehouseTicketTransUpdateWarehouseStock(WarehouseTicket ticket, Hashtable currentQty)
        {
            if (ticket.FactoryCode != string.Empty && /*ticket.SegmentCode != string.Empty &&*/ ticket.WarehouseCode != string.Empty)
            {
                this.WarehouseTicketTransUpdateWarehouseStockCore(ticket.FactoryCode,/* ticket.SegmentCode,*/ ticket.WarehouseCode, ticket.TicketNo, "from", currentQty);
            }
            if (ticket.TOFactoryCode != string.Empty && /*ticket.TOSegmentCode != string.Empty &&*/ ticket.TOWarehouseCode != string.Empty)
            {
                this.WarehouseTicketTransUpdateWarehouseStockCore(ticket.TOFactoryCode, /*ticket.TOSegmentCode,*/ ticket.TOWarehouseCode, ticket.TicketNo, "to", currentQty);
            }
        }
        private void WarehouseTicketTransUpdateWarehouseStockCore(string factoryCode, /*string segmentCode,*/ string warehouseCode, string ticketNo, string fromto, Hashtable currentQty)
        {
            bool bIsControl = true;
            object objwh = this.GetWarehouse(warehouseCode,/* segmentCode,*/ factoryCode);
            if (objwh != null)
            {
                bIsControl = FormatHelper.StringToBoolean(((Warehouse)objwh).IsControl);
            }

            object[] objs = this.QueryWarehouseTicketDetail(string.Empty, ticketNo, 1, int.MaxValue);
            if (objs == null)
                return;
            string strOperation = "";
            if (fromto == "from")
                strOperation = "-";
            else
                strOperation = "+";
            for (int i = 0; i < objs.Length; i++)
            {
                WarehouseTicketDetail item = (WarehouseTicketDetail)objs[i];
                decimal decStock = 0;
                decimal deCurrentQty = Convert.ToDecimal(currentQty[item.TicketNo + ":" + item.Sequence.ToString()]);
                if (bIsControl == true)
                    decStock = this.UpdateWarehouseStockQty(factoryCode, /*segmentCode,*/ warehouseCode, item.ItemCode, strOperation, deCurrentQty);
                if (fromto == "from")
                    item.FromWarehouseQty = decStock;
                else
                    item.ToWarehouseQty = decStock;
                this.UpdateWarehouseTicketDetail(item);
                item = null;
            }
        }
        /// <summary>
        /// 单据交易时更新工单物料耗用量
        /// </summary>
        /// <param name="ticket"></param>
        private void WarehouseTicketTransUpdateMoStock(WarehouseTicket ticket, Hashtable currentQty)
        {
            if (TransactionType.TRANSACTIONTYPE_MOSTOCK == null || TransactionType.TRANSACTIONTYPE_MOSTOCK.Length == 0)
                return;

            //查询当前单据类型的操作
            string strAttributeName = "", strOperation = "", strToWarehouse = "";
            for (int i = 0; i < TransactionType.TRANSACTIONTYPE_MOSTOCK.Length; i++)
            {
                TransactionType.TransactionTypeMoStock mostock = (TransactionType.TransactionTypeMoStock)TransactionType.TRANSACTIONTYPE_MOSTOCK[i];
                if (mostock != null && mostock.TransactionTypeCode == ticket.TransactionTypeCode)
                {
                    strAttributeName = mostock.AttributeName;
                    strOperation = mostock.Operation;
                    strToWarehouse = mostock.ToWarehouse;

                    /* modified by jessie lee, 2006-3-11, CS144
                     * 
                     * 如果是退料单 
                     * 报废单的逻辑与退料单相同
                     * 
                     * 用“单据名称”为“报废单”将物料发给“仓库类型”为“材料不良品区”的库房时，物料的数量应该加到
                     * “eMES -> 物料管理 -> 查询 -> 工单用料查询 ”界面的“不良品退料数”栏位里。
                     * 目前系统实现的方式是没有将报废单发过来的数量加到不良品退料数”栏位里。*/
                    if (string.Compare(strAttributeName, "ReturnQty", true) == 0)/* 退料单 */
                    {
                        object wh = this.GetWarehouse(ticket.TOWarehouseCode, ticket.TOFactoryCode);
                        if (wh != null)
                        {
                            /* 目标库是不良品区 */
                            if (string.Compare((wh as Warehouse).WarehouseType, "0025", true) == 0)
                            {
                                strAttributeName = "ReturnScrapQty";
                            }
                        }
                    }
                    else if (string.Compare(strAttributeName, "ScrapQty", true) == 0)/* 报废单 */
                    {
                        object wh = this.GetWarehouse(ticket.TOWarehouseCode, ticket.TOFactoryCode);
                        if (wh != null)
                        {
                            /* 目标库是不良品区 */
                            if (string.Compare((wh as Warehouse).WarehouseType, "0025", true) == 0)
                            {
                                /* 先更新报废数量 */
                                if (strAttributeName != "" && strOperation != "" && (strToWarehouse == "" || (strToWarehouse != "" && ticket.TOWarehouseCode == strToWarehouse)))
                                {
                                    //查询交易包含的所有物料清单
                                    object[] objs = this.QueryWarehouseTicketDetail(string.Empty, ticket.TicketNo, 1, int.MaxValue);
                                    if (objs == null)
                                        return;
                                    //更新
                                    for (int m = 0; m < objs.Length; m++)
                                    {
                                        WarehouseTicketDetail item = (WarehouseTicketDetail)objs[m];
                                        if (item.MOCode != string.Empty)
                                        {
                                            decimal deCurrentQty = Convert.ToDecimal(currentQty[item.TicketNo + ":" + item.Sequence.ToString()]);
                                            this.UpdateMOStockInTrans(item.MOCode, item.ItemCode, strAttributeName, deCurrentQty, strOperation);
                                        }
                                        item = null;
                                    }
                                }

                                /* 再更新不良品退料数 */
                                strAttributeName = "ReturnScrapQty";
                            }
                        }
                    }

                    if (strAttributeName != "" && strOperation != "" && (strToWarehouse == "" || (strToWarehouse != "" && ticket.TOWarehouseCode == strToWarehouse)))
                    {
                        //查询交易包含的所有物料清单
                        object[] objs = this.QueryWarehouseTicketDetail(string.Empty, ticket.TicketNo, 1, int.MaxValue);
                        if (objs == null)
                            return;
                        //更新
                        for (int m = 0; m < objs.Length; m++)
                        {
                            WarehouseTicketDetail item = (WarehouseTicketDetail)objs[m];
                            if (item.MOCode != string.Empty)
                            {
                                decimal deCurrentQty = Convert.ToDecimal(currentQty[item.TicketNo + ":" + item.Sequence.ToString()]);
                                this.UpdateMOStockInTrans(item.MOCode, item.ItemCode, strAttributeName, deCurrentQty, strOperation);
                            }
                            item = null;
                        }
                    }
                }
                mostock = null;
            }
        }
        /// <summary>
        /// 更新单据交易明细
        /// </summary>
        private void WarehouseTicketTransUpdateTicketDetail(string ticketNo, Hashtable currentQty)
        {
            object[] objs = this.QueryWarehouseTicketDetail(string.Empty, ticketNo, 1, int.MaxValue);
            if (objs == null)
                return;
            bool bClosed = true;
            for (int i = 0; i < objs.Length; i++)
            {
                WarehouseTicketDetail item = (WarehouseTicketDetail)objs[i];
                item.ActualQty = item.ActualQty + Convert.ToDecimal(currentQty[item.TicketNo + ":" + item.Sequence.ToString()]);
                this.UpdateWarehouseTicketDetail(item);
                if (item.ActualQty < item.Qty)
                {
                    bClosed = false;
                }
            }
            if (bClosed)
            {
                WarehouseTicket tkt = (WarehouseTicket)this.GetWarehouseTicket(ticketNo);
                tkt.TransactionStatus = WarehouseTicket.TransactionStatusEnum.Closed.ToString();
                this._helper.UpdateDomainObject(tkt);
            }
        }

        /// <summary>
        /// 盘点调整库存时，将调整记录添加到交易主表
        /// </summary>
        /// <returns>交易号</returns>
        private string AddWarehouseTicket(string toFactory, /* string toSegment,*/ string toWarehosue, string transType, string userId)
        {
            WarehouseTicket ticket = this.CreateNewWarehouseTicket();
            ticket.TOFactoryCode = toFactory;
            //ticket.TOSegmentCode = toSegment;
            ticket.TOWarehouseCode = toWarehosue;
            ticket.TransactionTypeCode = transType;
            ticket.MaintainUser = userId;
            ticket.TicketNo = this.GetTicketSeq("-", true);
            ticket.TicketUser = ticket.MaintainUser;
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            ticket.TicketDate = FormatHelper.TODateInt(dtNow);
            ticket.TicketTime = FormatHelper.TOTimeInt(dtNow);
            ticket.TransactionStatus = WarehouseTicket.TransactionStatusEnum.Closed.ToString();
            ticket.TransactionUser = userId;
            ticket.TransactionDate = ticket.TicketDate;
            ticket.TransactionTime = ticket.TicketTime;
            this._helper.AddDomainObject(ticket);
            return ticket.TicketNo;
        }
        private string AddWarehouseTicket(string factory, string warehouse, string toFactory, string toWarehosue, string transType, string refCode, string moCode, string userId)
        {
            WarehouseTicket ticket = this.CreateNewWarehouseTicket();
            ticket.FactoryCode = factory;
            //ticket.SegmentCode = segment;
            ticket.WarehouseCode = warehouse;
            ticket.TOFactoryCode = toFactory;
            //ticket.TOSegmentCode = toSegment;
            ticket.TOWarehouseCode = toWarehosue;
            ticket.TransactionTypeCode = transType;
            ticket.MaintainUser = userId;
            ticket.TicketNo = this.GetTicketSeq("-", true);
            ticket.TicketUser = ticket.MaintainUser;
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            ticket.TicketDate = FormatHelper.TODateInt(dtNow);
            ticket.TicketTime = FormatHelper.TOTimeInt(dtNow);
            ticket.TransactionStatus = WarehouseTicket.TransactionStatusEnum.Closed.ToString();
            ticket.TransactionUser = userId;
            ticket.TransactionDate = ticket.TicketDate;
            ticket.TransactionTime = ticket.TicketTime;
            this._helper.AddDomainObject(ticket);
            return ticket.TicketNo;
        }
        private void AddWarehouseTicketDetail(string ticketNo, int iSequence, string moCode, string itemCode, string itemName, decimal qty, decimal toWarehouseQty, string userId)
        {
            AddWarehouseTicketDetail(ticketNo, iSequence, moCode, itemCode, itemName, qty, toWarehouseQty, 0, userId);
        }
        private void AddWarehouseTicketDetail(string ticketNo, int iSequence, string moCode, string itemCode, string itemName, decimal qty, decimal toWarehouseQty, decimal fromWarehouseQty, string userId)
        {
            WarehouseTicketDetail dtl = this.CreateNewWarehouseTicketDetail();
            dtl.TicketNo = ticketNo;
            dtl.Sequence = iSequence;
            dtl.MOCode = moCode;
            dtl.ItemCode = itemCode;
            dtl.ItemName = itemName;
            dtl.Qty = qty;
            dtl.ActualQty = qty;
            dtl.FromWarehouseQty = fromWarehouseQty;
            dtl.ToWarehouseQty = toWarehouseQty;
            dtl.MaintainUser = userId;
            this.AddWarehouseTicketDetail(dtl, false);
        }

        /// <summary>
        /// 根据发料单产生的退料单
        /// </summary>
        public void DoReturnItemFromSend(string sourceTicketNo, Hashtable returnQty, Hashtable returnScrapQty, string userId)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                object objtkt = this.GetWarehouseTicket(sourceTicketNo);
                WarehouseTicket ticket = (WarehouseTicket)objtkt;
                object[] ticketDetail = this.QueryWarehouseTicketDetail(string.Empty, sourceTicketNo, 1, int.MaxValue);
                object[] objitems = this.GetAllWarehouseItem();
                Hashtable itemNameList = new Hashtable();
                for (int i = 0; i < objitems.Length; i++)
                {
                    WarehouseItem item = (WarehouseItem)objitems[i];
                    itemNameList.Add(item.ItemCode, item.ItemName);
                }

                foreach (object objQty in returnScrapQty.Values)
                {
                    if (Convert.ToDecimal(objQty) != 0)
                    {
                        Warehouse towh = DoReturnItemFromSend_GetToWarehouse(ticket);
                        if (towh == null)
                            break;
                        DoReturnItemFromSend_Core(ticket, ticketDetail, towh.WarehouseCode, returnScrapQty, itemNameList, "ReturnScrapQty", userId);
                        break;
                    }
                }
                foreach (object objQty in returnQty.Values)
                {
                    if (Convert.ToDecimal(objQty) != 0)
                    {
                        DoReturnItemFromSend_Core(ticket, ticketDetail, ticket.WarehouseCode, returnQty, itemNameList, "ReturnQty", userId);
                        break;
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }
        private void DoReturnItemFromSend_Core(WarehouseTicket ticket, object[] ticketDetail, string toWarehouseCode, Hashtable returnQty, Hashtable itemNameList, string moStockAttributeName, string userId)
        {
            string strTicketNo = "";
            //增加交易主表
            string strFactory = ticket.TOFactoryCode;
            //string strSegment = ticket.TOSegmentCode;
            string strWarehouse = ticket.TOWarehouseCode;
            string strToFactory = ticket.FactoryCode;
            //string strToSegment = ticket.SegmentCode;
            string strToWarehouse = toWarehouseCode;
            string strTransCode = GetTransactionTypeByName("ReturnItem");
            strTicketNo = AddWarehouseTicket(strFactory, /*strSegment,*/ strWarehouse,
                                strToFactory, /*strToSegment,*/ strToWarehouse, strTransCode, ticket.ReferenceCode, ticket.MOCode, userId);
            //增加交易明细
            int iSequence = 0;
            Hashtable htQty = new Hashtable();
            foreach (object objKey in returnQty.Keys)
            {
                if (Convert.ToDecimal(returnQty[objKey]) != 0)
                {
                    string strItemCode = "";
                    string strMoCode = "";
                    string[] strtmp = objKey.ToString().Split(new char[] { ':' });
                    for (int i = 0; i < ticketDetail.Length; i++)
                    {
                        WarehouseTicketDetail dtl = (WarehouseTicketDetail)ticketDetail[i];
                        if (dtl.Sequence.ToString() == strtmp[1])
                        {
                            strItemCode = dtl.ItemCode;
                            strMoCode = dtl.MOCode;
                            break;
                        }
                    }
                    iSequence++;
                    this.AddWarehouseTicketDetail(strTicketNo, iSequence, strMoCode, strItemCode, itemNameList[strItemCode].ToString(), Convert.ToDecimal(returnQty[objKey]), 0, userId);
                    htQty.Add(string.Format("{0}:{1}", strTicketNo, iSequence), returnQty[objKey]);

                    //更新工单用料
                    if (strMoCode != "")
                    {
                        this.UpdateMOStockInTrans(strMoCode, strItemCode, moStockAttributeName, Convert.ToDecimal(returnQty[objKey]), "add");
                    }
                }
            }
            //更新库存
            WarehouseTicketTransUpdateWarehouseStock((WarehouseTicket)this.GetWarehouseTicket(strTicketNo), htQty);
        }
        /// <summary>
        /// 获取目标仓库
        /// </summary>
        private Warehouse DoReturnItemFromSend_GetToWarehouse(WarehouseTicket ticket)
        {
            /*
             不良品退库：来源是发料单上的目的工厂+工段+仓库
             目的是与来源同工厂，同工段的同类仓库，即如果发料单来源是0021类，那么退料目的是0024类
             如果发料单来源非0021类，那么退料是同工厂，同工段的0025类
             在业务上保证一个工厂，一个工段只有0-1个某类不良品库
            */
            object[] objstmp;
            objstmp = this.QueryWarehouse(string.Empty, /*ticket.TOSegmentCode,*/  ticket.TOFactoryCode, 1, int.MaxValue);
            if (objstmp == null)
                return null;
            Warehouse wh = (Warehouse)this.GetWarehouse(ticket.TOWarehouseCode, /*ticket.TOSegmentCode,*/ ticket.TOFactoryCode);
            if (wh.WarehouseType == "0021")
            {
                for (int i = 0; i < objstmp.Length; i++)
                {
                    if (((Warehouse)objstmp[i]).WarehouseType == "0024")
                    {
                        return (Warehouse)objstmp[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < objstmp.Length; i++)
                {
                    if (((Warehouse)objstmp[i]).WarehouseType == "0025")
                    {
                        return (Warehouse)objstmp[i];
                    }
                }
            }
            return null;
        }
        #endregion

        #region WarehouseTicketDetail
        /// <summary>
        /// 交易单明细
        /// </summary>
        public WarehouseTicketDetail CreateNewWarehouseTicketDetail()
        {
            return new WarehouseTicketDetail();
        }

        public void AddWarehouseTicketDetail(WarehouseTicketDetail warehouseTicketDetail)
        {
            string strSql = "select count(*) from TBLWHTKTDETAIL where TKTNO=$tktno and ITEMCODE=$itemcode ";
            if (warehouseTicketDetail.MOCode != string.Empty)
                strSql += string.Format(" and MOCODE='{0}' ", warehouseTicketDetail.MOCode);
            else
                strSql += string.Format(" and (MOCODE='{0}' or MOCODE is null) ", warehouseTicketDetail.MOCode);
            int iExist = this.DataProvider.GetCount(new SQLParamCondition(strSql, new SQLParameter[]{
																							   new SQLParameter("tktno", typeof(string), warehouseTicketDetail.TicketNo),
																							   new SQLParameter("itemcode", typeof(string), warehouseTicketDetail.ItemCode)
																						   }
                ));
            if (iExist > 0)
            {
                throw new Exception("$Add_Ticket_Detail_ItemExist [$WarehouseItemCode=" + warehouseTicketDetail.ItemCode + "]");
            }
            this.AddWarehouseTicketDetail(warehouseTicketDetail, true);
        }
        public void AddWarehouseTicketDetail(WarehouseTicketDetail warehouseTicketDetail, bool generateSeq)
        {
            if (generateSeq == true)
            {
                object[] objs = this.QueryWarehouseTicketDetail(string.Empty, warehouseTicketDetail.TicketNo, 1, int.MaxValue);
                decimal iseq = 1;
                if (objs != null)
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        if (((WarehouseTicketDetail)objs[i]).Sequence >= iseq)
                            iseq = ((WarehouseTicketDetail)objs[i]).Sequence;
                    }
                    iseq++;
                }
                objs = null;
                warehouseTicketDetail.Sequence = iseq;
            }
            this._helper.AddDomainObject(warehouseTicketDetail);
        }
        public void AddWarehouseTicketDetail(ArrayList detailList)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                for (int i = 0; i < detailList.Count; i++)
                {
                    this.AddWarehouseTicketDetail((WarehouseTicketDetail)detailList[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message, ex);
            }
        }

        public void UpdateWarehouseTicketDetail(WarehouseTicketDetail warehouseTicketDetail)
        {
            string strSql = "select count(*) from TBLWHTKTDETAIL where TKTNO=$tktno and ITEMCODE=$itemcode and MOCODE=$mocode and SEQ!=$seq";
            int iExist = this.DataProvider.GetCount(new SQLParamCondition(strSql, new SQLParameter[]{
																										new SQLParameter("tktno", typeof(string), warehouseTicketDetail.TicketNo),
																										new SQLParameter("itemcode", typeof(string), warehouseTicketDetail.ItemCode),
																										new SQLParameter("mocode", typeof(string), warehouseTicketDetail.MOCode),
																										new SQLParameter("seq", typeof(int), warehouseTicketDetail.Sequence)
																									}
                ));
            if (iExist > 0)
            {
                throw new Exception("$Add_Ticket_Detail_ItemExist");
            }
            this._helper.UpdateDomainObject(warehouseTicketDetail);
        }

        public void DeleteWarehouseTicketDetail(WarehouseTicketDetail warehouseTicketDetail)
        {
            this._helper.DeleteDomainObject(warehouseTicketDetail);
            string strSql = string.Format("UPDATE TBLWHTKTDETAIL SET SEQ=SEQ-1 WHERE SEQ>{0} and TKTNO='{1}' ", warehouseTicketDetail.Sequence.ToString(), warehouseTicketDetail.TicketNo);
            this.DataProvider.CustomExecute(new SQLCondition(strSql));
        }

        public void DeleteWarehouseTicketDetail(WarehouseTicketDetail[] warehouseTicketDetail)
        {
            for (int i = warehouseTicketDetail.Length - 1; i >= 0; i--)
            {
                DeleteWarehouseTicketDetail(warehouseTicketDetail[i]);
            }
        }

        public object GetWarehouseTicketDetail(string sequence, string ticketNo)
        {
            return this.DataProvider.CustomSearch(typeof(WarehouseTicketDetail), new object[] { sequence, ticketNo });
        }

        /// <summary>
        /// ** 功能描述:	查询WarehouseTicketDetail的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <param name="ticketNo">TicketNo，模糊查询</param>
        /// <returns> WarehouseTicketDetail的总记录数</returns>
        public int QueryWarehouseTicketDetailCount(string sequence, string ticketNo)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLWHTKTDETAIL where 1=1 and SEQ like '{0}%'  and TKTNO = '{1}' ", sequence, ticketNo)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询WarehouseTicketDetail
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <param name="ticketNo">TicketNo，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> WarehouseTicketDetail数组</returns>
        public object[] QueryWarehouseTicketDetail(string sequence, string ticketNo, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseTicketDetail), new PagerCondition(string.Format("select {0} from TBLWHTKTDETAIL where 1=1 and SEQ like '{1}%'  and TKTNO = '{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseTicketDetail)), sequence, ticketNo), "TKTNO,SEQ", inclusive, exclusive));
        }

        //打印发料单查询
        public object[] QueryWarehouseTicketDetail3(string sequence, string ticketNo, int inclusive, int exclusive)
        {
            string tktnoSql = string.Format("select count(TKTNO) from TBLWHTKT where MOCODE >0 and TKTNO='{0}'", ticketNo);
            bool isControlByMO = false;
            int count = this.DataProvider.GetCount(new SQLCondition(tktnoSql));
            if (count > 0)
            {
                //如果受工单管控,排除发料单中不在工单工序bom中的物料
                //只返回工单工序bom中有的物料
                string sql = string.Format("select a.* from TBLWHTKTDETAIL a, tblopbomdetail b " +
                "   where 1=1 and SEQ like '{0}%'  and TKTNO = '{1}'  " +
                "	AND a.itemcode = b.obitemcode " +
                "	AND b.actiontype = '0' " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                "	and b.itemcode = (select itemcode from tblmo where mocode = a.mocode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") " +
                "	and b.obcode in (select routecode from tblmo2route where mocode = a.mocode) " +
                "	and b.opcode in " +
                " (select opcode " +
                "	from tblitemroute2op " +
                "	where itemcode = " +
                "		(select itemcode from tblmo where mocode = a.mocode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                "	and (substr(opcontrol, {2}, 1) = '1'))", sequence, ticketNo, ((int)BenQGuru.eMES.BaseSetting.OperationList.ComponentLoading + 1));
                return this.DataProvider.CustomQuery(typeof(WarehouseTicketDetail), new PagerCondition(sql, "TKTNO,SEQ", inclusive, exclusive));
            }
            //不受管控,返回所有
            return this.DataProvider.CustomQuery(typeof(WarehouseTicketDetail), new PagerCondition(string.Format("select {0} from TBLWHTKTDETAIL where 1=1 and SEQ like '{1}%'  and TKTNO = '{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseTicketDetail)), sequence, ticketNo), "TKTNO,SEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的WarehouseTicketDetail
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>WarehouseTicketDetail的总记录数</returns>
        public object[] GetAllWarehouseTicketDetail()
        {
            return this.DataProvider.CustomQuery(typeof(WarehouseTicketDetail), new SQLCondition(string.Format("select {0} from TBLWHTKTDETAIL order by SEQ,TKTNO", DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseTicketDetail)))));
        }

        /// <summary>
        /// 从工单号导入交易物料列表
        /// </summary>
        public void ImportItemFromOPBOM(string moCode, string ticketNo, string userId)
        {
            BenQGuru.eMES.MOModel.MOFacade mof = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
            object obj = mof.GetMO(moCode);
            if (obj == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_MONotExisted");
                return;
            }
            BenQGuru.eMES.Domain.MOModel.MO mo = (BenQGuru.eMES.Domain.MOModel.MO)obj;
            if ((mo.MOStatus == MOManufactureStatus.MOSTATUS_INITIAL) ||
                (mo.MOStatus == MOManufactureStatus.MOSTATUS_CLOSE))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_ImportOPBOM_InTicket_MOStatus");
                return;
            }
            MO2Route currentMO2Route = (MO2Route)mof.GetMONormalRouteByMOCode(mo.MOCode);
            mo = null;
            obj = null;

            BenQGuru.eMES.MOModel.OPBOMFacade opbom = new BenQGuru.eMES.MOModel.OPBOMFacade(this.DataProvider);
            object[] objs = opbom.GetOPBOMDetails(moCode, currentMO2Route.RouteCode, string.Empty);
            if (objs == null || objs.Length == 0)
                return;
            //读取物料主档资料
            Hashtable htItems = new Hashtable();
            object[] objsitem = this.GetAllWarehouseItem();
            if (objsitem != null)
            {
                for (int i = 0; i < objsitem.Length; i++)
                {
                    WarehouseItem item = (WarehouseItem)objsitem[i];
                    htItems.Add(item.ItemCode, item.ItemName);
                    item = null;
                }
            }
            objsitem = null;

            Hashtable ht = new Hashtable();
            int iseq = 1;
            for (int i = 0; i < objs.Length; i++)
            {
                BenQGuru.eMES.Domain.MOModel.OPBOMDetail bom = (BenQGuru.eMES.Domain.MOModel.OPBOMDetail)objs[i];
                if (!ht.ContainsKey(bom.OPBOMItemCode) && htItems.ContainsKey(bom.OPBOMItemCode))
                {
                    WarehouseTicketDetail item = new WarehouseTicketDetail();
                    item.Sequence = iseq;
                    item.TicketNo = ticketNo;
                    item.ItemCode = bom.OPBOMItemCode;
                    item.ItemName = htItems[bom.OPBOMItemCode].ToString();
                    item.MOCode = moCode;
                    item.Qty = bom.OPBOMItemQty;
                    item.MaintainUser = userId;
                    item.TicketUser = userId;
                    //2006/11/17,Laws Lu add get DateTime from db Server
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                    DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                    item.TicketDate = FormatHelper.TODateInt(dtNow);
                    item.TicketTime = FormatHelper.TOTimeInt(dtNow);
                    try
                    {
                        this.AddWarehouseTicketDetail(item, false);
                    }
                    catch { }
                    item = null;
                    iseq++;
                    ht.Add(bom.OPBOMItemCode, "");
                }
                bom = null;
            }
            objs = null;
            opbom = null;
        }

        /// <summary>
        /// 更新交易实际数量
        /// </summary>
        public void UpdateWarehouseTicketDetailActualQty(string sequence, string ticketNo, decimal actualQty, string userId)
        {
            object obj = this.GetWarehouseTicketDetail(sequence, ticketNo);
            if (obj == null)
                return;
            WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
            item.ActualQty = actualQty;
            item.MaintainUser = userId;
            this._helper.UpdateDomainObject(item);
            item = null;
        }


        #endregion

        #region Query
        /// <summary>
        /// 库存交易查询
        /// </summary>
        /// <returns>WarehouseTicket数组</returns>
        public object[] GetWarehouseTicketInQuery(string ticketNo, string transactionTypeCode, string itemCode, int transactionDateFrom, int transactionDateTo, string factoryCode, /*string segmentCode,*/ string warehouseCode, int inclusive, int exclusive, ref object[] objstkt)
        {
            System.Text.StringBuilder sbwhere = new System.Text.StringBuilder();
            sbwhere.Append(" m.TKTNO=d.TKTNO and (m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Closed.ToString() + "' or m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Transaction.ToString() + "') ");
            sbwhere.Append(string.Format(" and (m.FACCODE = '{0}' or m.TOFACCODE = '{0}') ", factoryCode));
            //sbwhere.Append(string.Format(" and (m.SEGCODE = '{0}' or m.TOSEGCODE = '{0}') ", segmentCode));
            sbwhere.Append(string.Format(" and (m.WHCODE = '{0}' or m.TOWHCODE = '{0}') ", warehouseCode));
            if (ticketNo != string.Empty)
                sbwhere.Append(string.Format(" and m.TKTNO like '{0}%' ", ticketNo));
            if (transactionTypeCode != string.Empty)
                sbwhere.Append(string.Format(" and m.TRANSTYPECODE = '{0}' ", transactionTypeCode));
            if (itemCode != string.Empty)
                sbwhere.Append(string.Format(" and d.ITEMCODE like '{0}%' ", itemCode));
            if (transactionDateFrom != 0)
                sbwhere.Append(string.Format(" and m.TRANSACTIONDATE >= {0} ", transactionDateFrom));
            if (transactionDateTo != 0)
                sbwhere.Append(string.Format(" and m.TRANSACTIONDATE <= {0} ", transactionDateTo));
            string strWhere = sbwhere.ToString();
            //读取交易明细
            string strSql = "select tbl.* from (";
            strSql += string.Format("select decode(m.FACCODE,'{0}',m.FACCODE,m.TOFACCODE) AS FACCODE,decode(m.WHCODE,'{1}',m.WHCODE,m.TOWHCODE) AS WHCODE,m.TRANSACTIONDATE AS TRANSDATE,m.TRANSACTIONTIME AS TRANSTIME, d.* from TBLWHTKT m, TBLWHTKTDETAIL d where " + strWhere + ") tbl ", factoryCode, /*segmentCode,*/ warehouseCode);
            object[] objsdtl = this.DataProvider.CustomQuery(typeof(WarehouseTicketDetail), new PagerCondition(strSql, "FACCODE,WHCODE,ITEMCODE, TRANSDATE desc, TRANSTIME desc, TKTNO desc", inclusive, exclusive));
            if (objsdtl == null)
                return null;

            //读取交易主档
            System.Text.StringBuilder sbtkt = new System.Text.StringBuilder();
            for (int i = 0; i < objsdtl.Length; i++)
            {
                WarehouseTicketDetail dtl = (WarehouseTicketDetail)objsdtl[i];
                if (sbtkt.ToString().IndexOf(dtl.TicketNo) < 0)
                {
                    sbtkt.Append(string.Format(" or TKTNO='{0}' ", ((WarehouseTicketDetail)objsdtl[i]).TicketNo));
                }
            }
            strSql = "select {0} from TBLWHTKT where 1=0 " + sbtkt.ToString();
            objstkt = this.DataProvider.CustomQuery(typeof(WarehouseTicket), new SQLCondition(string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseTicket)))));

            return objsdtl;
        }
        /// <summary>
        /// 库存交易查询总数
        /// </summary>
        /// <returns></returns>
        public int GetWarehouseTicketInQueryCount(string ticketNo, string transactionTypeCode, string itemCode, int transactionDateFrom, int transactionDateTo, string factoryCode, /*string segmentCode,*/ string warehouseCode)
        {
            System.Text.StringBuilder sbwhere = new System.Text.StringBuilder();
            sbwhere.Append(" m.TKTNO=d.TKTNO and (m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Closed.ToString() + "' or m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Transaction.ToString() + "') ");
            sbwhere.Append(string.Format(" and (m.FACCODE = '{0}' or m.TOFACCODE = '{0}') ", factoryCode));
            //sbwhere.Append(string.Format(" and (m.SEGCODE = '{0}' or m.TOSEGCODE = '{0}') ", segmentCode));
            sbwhere.Append(string.Format(" and (m.WHCODE = '{0}' or m.TOWHCODE = '{0}') ", warehouseCode));
            if (ticketNo != string.Empty)
                sbwhere.Append(string.Format(" and m.TKTNO like '{0}%' ", ticketNo));
            if (transactionTypeCode != string.Empty)
                sbwhere.Append(string.Format(" and m.TRANSTYPECODE = '{0}' ", transactionTypeCode));
            if (itemCode != string.Empty)
                sbwhere.Append(string.Format(" and d.ITEMCODE like '{0}%' ", itemCode));
            if (transactionDateFrom != 0)
                sbwhere.Append(string.Format(" and m.TRANSACTIONDATE >= {0} ", transactionDateFrom));
            if (transactionDateTo != 0)
                sbwhere.Append(string.Format(" and m.TRANSACTIONDATE <= {0} ", transactionDateTo));
            string strSql = "select count(*) from TBLWHTKT m, TBLWHTKTDETAIL d where " + sbwhere.ToString();
            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }

        /// <summary>
        /// 物料交易查询
        /// </summary>
        /// <returns></returns>
        public object[] GetWarehouseTicketInQueryItem(string ticketNo, string transactionTypeCode, string itemCode, int transactionDateFrom, int transactionDateTo, string moCode, string factoryCodeFrom, /* string segmentCodeFrom,*/ string warehouseCodeFrom, string factoryCodeTo, /* string segmentCodeTo,*/ string warehouseCodeTo, int inclusive, int exclusive)
        {
            System.Text.StringBuilder sbwhere = new System.Text.StringBuilder();
            sbwhere.Append(" m.TKTNO=d.TKTNO and (m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Closed.ToString() + "' or m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Transaction.ToString() + "') ");
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.FACCODE", factoryCodeFrom)));
            //sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.SEGCODE", segmentCodeFrom)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.WHCODE", warehouseCodeFrom)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOFACCODE", factoryCodeTo)));
            //sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOSEGCODE", segmentCodeTo)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOWHCODE", warehouseCodeTo)));
            if (ticketNo != string.Empty)
                sbwhere.Append(string.Format(" and m.TKTNO like '{0}%' ", ticketNo));
            if (transactionTypeCode != string.Empty)
            {
                transactionTypeCode = "'" + transactionTypeCode.Replace(",", "','") + "'";
                sbwhere.Append(string.Format(" and m.TRANSTYPECODE in ({0}) ", transactionTypeCode));
            }
            if (itemCode != string.Empty)
                sbwhere.Append(string.Format(" and d.ITEMCODE like '{0}%' ", itemCode));
            if (transactionDateFrom != 0)
                sbwhere.Append(string.Format(" and m.TRANSACTIONDATE >= {0} ", transactionDateFrom));
            if (transactionDateTo != 0)
                sbwhere.Append(string.Format(" and m.TRANSACTIONDATE <= {0} ", transactionDateTo));
            if (moCode != string.Empty)
                sbwhere.Append(string.Format(" and d.MOCODE like '{0}%' ", moCode));
            string strWhere = sbwhere.ToString();
            //读取交易明细
            string strSql = string.Format(
                @"select {0}  from 
							(select d.ITEMCODE,m.TRANSTYPECODE,d.MOCODE,m.FACCODE,m.WHCODE,m.TOFACCODE,m.TOWHCODE,sum(d.ACTQTY) QTY  
							from TBLWHTKT m, TBLWHTKTDETAIL d where {1}
							group by d.ITEMCODE,m.TRANSTYPECODE,d.MOCODE,m.FACCODE,m.WHCODE,m.TOFACCODE,m.TOWHCODE 
							order by d.MOCODE,d.ITEMCODE,m.TRANSTYPECODE 
							) queryitem",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseTicketQueryItem)), strWhere);
            object[] objs = this.DataProvider.CustomQuery(typeof(WarehouseTicketQueryItem), new PagerCondition(strSql, inclusive, exclusive));

            return objs;
        }
        /// <summary>
        /// 物料交易查询(总数)
        /// </summary>
        /// <returns></returns>
        public int GetWarehouseTicketInQueryItemCount(string ticketNo, string transactionTypeCode, string itemCode, int transactionDateFrom, int transactionDateTo, string moCode, string factoryCodeFrom, /*string segmentCodeFrom,*/ string warehouseCodeFrom, string factoryCodeTo, /* string segmentCodeTo,*/ string warehouseCodeTo)
        {
            System.Text.StringBuilder sbwhere = new System.Text.StringBuilder();
            sbwhere.Append(" m.TKTNO=d.TKTNO and (m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Closed.ToString() + "' or m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Transaction.ToString() + "') ");
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.FACCODE", factoryCodeFrom)));
            //sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.SEGCODE", segmentCodeFrom)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.WHCODE", warehouseCodeFrom)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOFACCODE", factoryCodeTo)));
            //sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOSEGCODE", segmentCodeTo)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOWHCODE", warehouseCodeTo)));
            if (ticketNo != string.Empty)
                sbwhere.Append(string.Format(" and m.TKTNO like '{0}%' ", ticketNo));
            if (transactionTypeCode != string.Empty)
            {
                transactionTypeCode = "'" + transactionTypeCode.Replace(",", "','") + "'";
                sbwhere.Append(string.Format(" and m.TRANSTYPECODE in ({0}) ", transactionTypeCode));
            }
            if (itemCode != string.Empty)
                sbwhere.Append(string.Format(" and d.ITEMCODE like '{0}%' ", itemCode));
            if (transactionDateFrom != 0)
                sbwhere.Append(string.Format(" and m.TRANSACTIONDATE >= {0} ", transactionDateFrom));
            if (transactionDateTo != 0)
                sbwhere.Append(string.Format(" and m.TRANSACTIONDATE <= {0} ", transactionDateTo));
            if (moCode != string.Empty)
                sbwhere.Append(string.Format(" and d.MOCODE like '{0}%' ", moCode));
            string strWhere = sbwhere.ToString();
            //读取交易明细
            string strSql = string.Format(
                @"select count(*)  from 
							(select d.ITEMCODE,m.TRANSTYPECODE,d.MOCODE,m.FACCODE,m.WHCODE,m.TOFACCODE,m.TOWHCODE,sum(d.ACTQTY) QTY  
							from TBLWHTKT m, TBLWHTKTDETAIL d where {0}
							group by d.ITEMCODE,m.TRANSTYPECODE,d.MOCODE,m.FACCODE,m.WHCODE,m.TOFACCODE,m.TOWHCODE 
							) queryitem",
                strWhere);
            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }
        private string BuildWhereCondition(string fieldName, string fieldValue)
        {
            string strRet = "";
            if (fieldValue != string.Empty)
                strRet = string.Format(" {0}='{1}' ", fieldName, fieldValue);
            else
                strRet = string.Format("({0}='' or {0} is null)", fieldName);
            return strRet;
        }

        /// <summary>
        /// 物料交易查询 明细
        /// </summary>
        /// <returns></returns>
        public object[] GetWarehouseTicketQueryItemDetail(string itemCode, string transactionTypeCode, string moCode, string factoryCodeFrom, /*string segmentCodeFrom,*/ string warehouseCodeFrom, string factoryCodeTo, /*string segmentCodeTo,*/ string warehouseCodeTo, int inclusive, int exclusive, ref object[] objstkt)
        {
            System.Text.StringBuilder sbwhere = new System.Text.StringBuilder();
            sbwhere.Append(" m.TKTNO=d.TKTNO and (m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Closed.ToString() + "' or m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Transaction.ToString() + "') ");
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.FACCODE", factoryCodeFrom)));
            //sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.SEGCODE", segmentCodeFrom)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.WHCODE", warehouseCodeFrom)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOFACCODE", factoryCodeTo)));
            //sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOSEGCODE", segmentCodeTo)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOWHCODE", warehouseCodeTo)));
            if (transactionTypeCode != string.Empty)
                sbwhere.Append(string.Format(" and m.TRANSTYPECODE = '{0}' ", transactionTypeCode));
            if (itemCode != string.Empty)
                sbwhere.Append(string.Format(" and d.ITEMCODE = '{0}' ", itemCode));
            if (moCode != string.Empty)
                sbwhere.Append(string.Format(" and d.MOCODE like '{0}%' ", moCode));
            string strWhere = sbwhere.ToString();
            //读取交易明细
            string strSql = "select d.* from TBLWHTKT m, TBLWHTKTDETAIL d where " + strWhere;
            object[] objsdtl = this.DataProvider.CustomQuery(typeof(WarehouseTicketDetail), new PagerCondition(strSql, "m.TRANSACTIONDATE desc, m.TRANSACTIONTIME desc", inclusive, exclusive));
            if (objsdtl == null)
                return null;

            //读取交易主档
            System.Text.StringBuilder sbtkt = new System.Text.StringBuilder();
            for (int i = 0; i < objsdtl.Length; i++)
            {
                WarehouseTicketDetail dtl = (WarehouseTicketDetail)objsdtl[i];
                if (sbtkt.ToString().IndexOf(dtl.TicketNo) < 0)
                {
                    sbtkt.Append(string.Format(" or TKTNO='{0}' ", ((WarehouseTicketDetail)objsdtl[i]).TicketNo));
                }
            }
            strSql = "select {0} from TBLWHTKT where 1=0 " + sbtkt.ToString();
            objstkt = this.DataProvider.CustomQuery(typeof(WarehouseTicket), new SQLCondition(string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(WarehouseTicket)))));

            return objsdtl;
        }

        /// <summary>
        /// 物料交易查询 明细 (总数)
        /// </summary>
        /// <returns></returns>
        public int GetWarehouseTicketQueryItemDetailCount(string itemCode, string transactionTypeCode, string moCode, string factoryCodeFrom, /* string segmentCodeFrom,*/ string warehouseCodeFrom, string factoryCodeTo, /*string segmentCodeTo,*/ string warehouseCodeTo)
        {
            System.Text.StringBuilder sbwhere = new System.Text.StringBuilder();
            sbwhere.Append(" m.TKTNO=d.TKTNO and (m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Closed.ToString() + "' or m.TRANSSTATUS='" + WarehouseTicket.TransactionStatusEnum.Transaction.ToString() + "') ");
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.FACCODE", factoryCodeFrom)));
            //sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.SEGCODE", segmentCodeFrom)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.WHCODE", warehouseCodeFrom)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOFACCODE", factoryCodeTo)));
            //sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOSEGCODE", segmentCodeTo)));
            sbwhere.Append(string.Format(" and {0} ", BuildWhereCondition("m.TOWHCODE", warehouseCodeTo)));
            if (transactionTypeCode != string.Empty)
                sbwhere.Append(string.Format(" and m.TRANSTYPECODE = '{0}' ", transactionTypeCode));
            if (itemCode != string.Empty)
                sbwhere.Append(string.Format(" and d.ITEMCODE = '{0}' ", itemCode));
            if (moCode != string.Empty)
                sbwhere.Append(string.Format(" and d.MOCODE like '{0}%' ", moCode));
            string strWhere = sbwhere.ToString();
            //读取交易明细
            string strSql = "select count(*) from TBLWHTKT m, TBLWHTKTDETAIL d where " + strWhere;
            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }

        /// <summary>
        /// 工单用料查询
        /// </summary>
        /// <returns>MOStock数组</returns>
        public object[] GetMOStockInQuery(string itemCode, string moCode, int inclusive, int exclusive)
        {
            string strWhere = " 1=1 ";
            if (itemCode != string.Empty)
            {
                itemCode = "'" + itemCode.Replace(",", "','") + "'";
                strWhere += string.Format(" and ITEMCODE in ({0}) ", itemCode);
            }
            if (moCode != string.Empty)
                strWhere += string.Format(" and MOCODE like '{0}%' ", moCode);
            string strSql = string.Format(@"select MOCODE,ITEMCODE,sum(RECQTY) RECQTY,sum(ISSUEQTY) ISSUEQTY,sum(SCRAPQTY) SCRAPQTY,sum(RETURNQTY) RETURNQTY, sum(RETURNSCRAPQTY) RETURNSCRAPQTY  
								from TBLMOSTOCK 
								where {0} 
								group by MOCODE,ITEMCODE 

								order by MOCODE,ITEMCODE "
                , strWhere);
            strSql = string.Format("select mstock.* from ({0}) mstock", strSql);
            object[] objs = this.DataProvider.CustomQuery(typeof(MOStock), new PagerCondition(strSql, "MOCODE,ITEMCODE", inclusive, exclusive));

            //从交易单据中查询拆解数量
            string strTransType = GetTransactionTypeByName("TSSplitItem");
            strSql = string.Format("select d.* from tblwhtktdetail d join tblwhtkt m on m.tktno=d.tktno and m.transtypecode='{0}' ", strTransType);
            if (moCode != string.Empty)
                strSql += string.Format(" and d.mocode='{0}' ", moCode);
            object[] objstkt = this.DataProvider.CustomQuery(typeof(WarehouseTicketDetail), new SQLCondition(strSql));
            if (objstkt != null)
            {
                Hashtable ht = new Hashtable();
                for (int i = 0; i < objstkt.Length; i++)
                {
                    WarehouseTicketDetail dtl = (WarehouseTicketDetail)objstkt[i];
                    if (ht.ContainsKey(dtl.MOCode + ":" + dtl.ItemCode))
                        ht[dtl.MOCode + ":" + dtl.ItemCode] = decimal.Parse(ht[dtl.MOCode + ":" + dtl.ItemCode].ToString()) + dtl.ActualQty;
                    else
                        ht[dtl.MOCode + ":" + dtl.ItemCode] = dtl.ActualQty;
                }
                for (int i = 0; i < objs.Length; i++)
                {
                    MOStock stock = (MOStock)objs[i];
                    if (ht.ContainsKey(stock.MOCode + ":" + stock.ItemCode))
                    {
                        stock.IssueQty -= decimal.Parse(ht[stock.MOCode + ":" + stock.ItemCode].ToString());
                    }
                    objs[i] = stock;
                }
            }

            return objs;
        }
        /// <summary>
        /// 工单用料查询，总数
        /// </summary>
        /// <returns></returns>
        public int GetMOStockInQueryCount(string itemCode, string moCode)
        {
            string strWhere = " 1=1 ";
            if (itemCode != string.Empty)
            {
                itemCode = "'" + itemCode.Replace(",", "','") + "'";
                strWhere += string.Format(" and ITEMCODE in ({0}) ", itemCode);
            }
            if (moCode != string.Empty)
                strWhere += string.Format(" and MOCODE like '{0}%' ", moCode);
            string strSql = string.Format(@"select count(*) 
								from TBLMOSTOCK 
								where {0} 
								group by MOCODE,ITEMCODE"
                , strWhere);
            strSql = string.Format("select count(*) from ( {0} ) tbl", strSql);
            try
            {
                return this.DataProvider.GetCount(new SQLCondition(strSql));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 离线维修物料损耗率
        /// </summary>
        /// <returns></returns>
        public object[] GetItemKeyWaste(string moCode, string itemCode, int inclusive, int exclusive)
        {
            /*
             * 查询离线维修物料损耗率
             1. 查询上料数量:dIssueQty		(MOStock)
             2. 查询送维修数量:dTSQty		(MOStock)
             3. 汇总修好量:dTSGoodQty		(TS)
             4. 汇总在修量:dTSDoingQty		(TS)
             5. 汇总报废量:dScrapQty		(TS, TSSplitItem)
             6. 返回结果
            */

            //1. 查询上料数量:dIssueQty		(MOStock)
            string strSql = string.Format("select {0} from TBLMOSTOCK where MOCODE=$mocode and ITEMCODE=$itemcode ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MOStock)));
            object[] objsmostock = this.DataProvider.CustomQuery(typeof(MOStock), new SQLParamCondition(strSql, new SQLParameter[] { new SQLParameter("mocode", typeof(string), moCode), new SQLParameter("itemcode", typeof(string), itemCode) }));
            if (objsmostock == null)
                return null;

            #region 工单报废率,工单损耗率数据加载 .
            /* 计算逻辑 */
            //			对于未关单的工单：
            //			工单物料的损耗率=(在线维修报废数量+离线维修报废数量)/ 工单上料采集该物料的总数
            //			工单物料的报废率=(在线维修报废数量+离线维修报废数量) / (工单上料采集该物料的总数+维修此工单不良所换上该物料的总数)
            //
            //			对于已经关单的工单：
            //			工单物料的损耗率=(在线维修报废数量+离线维修报废数量+该物料未修完数量)/ 工单上料采集该物料的总数
            //			工单物料的报废率=(在线维修报废数量+离线维修报废数量+该物料未修完数量) / (工单上料采集该物料的总数+维修此工单不良所换上该物料的总数)

            this.GetMOByMoStock(objsmostock);							//工单状态加载
            this.GetMOLoadingMaterialCount(objsmostock);				//工单物料上料数量
            this.GetTSLoadingMaterialCount(objsmostock);				//工单物料维修不良数量
            this.GetUnCompletedMaterialCount(objsmostock);				//工单物料未修完数量



            #endregion

            MOStock mostock = (MOStock)objsmostock[0];
            decimal dIssueQty = mostock.IssueQty;

            //2. 查询送维修数量:dTSQty		(MOStock)
            decimal dTSQty = mostock.GainLose;
            objsmostock = null;

            //3. 汇总修好量:dTSGoodQty		(TS)
            //4. 汇总在修量:dTSDoingQty		(TS)
            //5. 汇总报废量:dScrapQty		(TS)
            strSql = string.Format("select {0} from TBLTS where MOCODE=$mocode and ITEMCODE=$itemcode and CARDTYPE=$cardtype ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.TS.TS)));
            object[] objsts = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TS.TS), new SQLParamCondition(
                strSql,
                new SQLParameter[]{
									  new SQLParameter("mocode", typeof(string), moCode),
									  new SQLParameter("itemcode", typeof(string), itemCode),
									  new SQLParameter("cardtype", typeof(string), CardType.CardType_Part)
								  }
                ));
            decimal dTSGoodQty = 0, dTSDoingQty = 0, dScrapQty = 0;
            if (objsts != null)
            {
                for (int i = 0; i < objsts.Length; i++)
                {
                    BenQGuru.eMES.Domain.TS.TS ts = (BenQGuru.eMES.Domain.TS.TS)objsts[i];
                    if (ts.TSStatus == TSStatus.TSStatus_TS)
                        dTSDoingQty++;
                    if (ts.TSStatus == TSStatus.TSStatus_Complete)
                        dTSGoodQty++;
                    if (ts.TSStatus == TSStatus.TSStatus_Scrap)
                        dScrapQty++;
                }
            }

            //5. 汇总报废量:dScrapQty		(TSSplitItem)
            strSql = string.Format("select {0} from TBLTSSPLITITEM where MOCODE=$mocode and MITEMCODE=$itemcode ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.TS.TSSplitItem)));
            object[] objstssplit = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TS.TSSplitItem), new SQLParamCondition(
                strSql,
                new SQLParameter[]{
									  new SQLParameter("mocode", typeof(string), moCode),
									  new SQLParameter("itemcode", typeof(string), itemCode)
								  }
                ));
            if (objstssplit != null)
            {
                for (int i = 0; i < objstssplit.Length; i++)
                {
                    BenQGuru.eMES.Domain.TS.TSSplitItem tss = (BenQGuru.eMES.Domain.TS.TSSplitItem)objstssplit[i];
                    dScrapQty += tss.ScrapQty;
                }
            }

            //6. 返回结果
            decimal dInput = 0, dWearOff = 0, dScrap = 0;
            dInput = dIssueQty - dTSQty;
            if (mostock.MOLoadingQty != 0 && (mostock.MOLoadingQty + mostock.TSLoadingQty) != 0)
            {
                if (mostock.MOStatus == BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_CLOSE)
                {
                    dWearOff = (mostock.ScrapQty + mostock.TSUnCompletedQty) / mostock.MOLoadingQty;
                    dScrap = (mostock.ScrapQty + mostock.TSUnCompletedQty) / (mostock.MOLoadingQty + mostock.TSLoadingQty);
                }
                else
                {
                    dWearOff = mostock.ScrapQty / mostock.MOLoadingQty;
                    dScrap = mostock.ScrapQty / (mostock.MOLoadingQty + mostock.TSLoadingQty);
                }
            }
            else
            {
                if (dIssueQty - dTSGoodQty != 0)
                    dWearOff = (dScrapQty + dTSDoingQty) / (dIssueQty - dTSGoodQty);
                if (dIssueQty - dTSGoodQty - dTSDoingQty != 0)
                    dScrap = (dScrapQty) / (dIssueQty - dTSGoodQty - dTSDoingQty);
            }

            decimal[] decRet = new decimal[] { dInput, dTSQty, dTSGoodQty, dTSDoingQty, dScrapQty, dWearOff, dScrap };
            object[] objRet = new object[] { decRet };
            return objRet;
        }
        /// <summary>
        /// 离线维修物料损耗率，总数
        /// </summary>
        /// <returns></returns>
        public int GetItemKeyWasteCount(string moCode, string itemCode)
        {
            return 1;
        }

        public object[] GetAvailableMO(string moCode, string itemCode, int inclusive, int exclusive)
        {
            string strSql = "select tblmo.mocode,tblmo.itemcode,tblitem.itemname as eattribute1 from tblmo join tblitem on tblmo.itemcode=tblitem.itemcode and tblmo.orgid = tblitem.orgid";
            string[] moStatus = new string[] { MOManufactureStatus.MOSTATUS_RELEASE, MOManufactureStatus.MOSTATUS_OPEN, MOManufactureStatus.MOSTATUS_PENDING };
            strSql += string.Format(" and MOSTATUS in ('{0}','{1}','{2}') ", moStatus[0], moStatus[1], moStatus[2]);
            if (moCode != string.Empty)
                strSql += string.Format(" and tblmo.mocode like '{0}%' ", moCode);
            if (itemCode != string.Empty)
                strSql += string.Format(" and tblitem.itemcode like '{0}%' ", itemCode);

            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                strSql += " where tblmo.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ") ";
            }

            return this.DataProvider.CustomQuery(
                typeof(MO),
                new PagerCondition(strSql, "mocode", inclusive, exclusive));
        }
        public int GetAvailableMOCount(string moCode, string itemCode)
        {
            string strSql = "select count(*) from tblmo join tblitem on tblmo.itemcode=tblitem.itemcode and tblmo.orgid = tblitem.orgid ";
            string[] moStatus = new string[] { MOManufactureStatus.MOSTATUS_RELEASE, MOManufactureStatus.MOSTATUS_OPEN, MOManufactureStatus.MOSTATUS_PENDING };
            strSql += string.Format(" and MOSTATUS in ('{0}','{1}','{2}') ", moStatus[0], moStatus[1], moStatus[2]);
            if (moCode != string.Empty)
                strSql += string.Format(" and tblmo.mocode like '{0}%' ", moCode);
            if (itemCode != string.Empty)
                strSql += string.Format(" and tblitem.itemcode like '{0}%' ", itemCode);

            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                strSql += " where tblmo.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ") ";
            }

            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }

        #region 工单物料损耗率，报废率相关方法

        #region 工单物料上料数量

        public void GetMOLoadingMaterialCount(object[] objs)
        {
            if (objs == null || objs.Length == 0) return;

            Hashtable _ht = this.GetHTMOLoadingMaterialCount(objs);

            foreach (MOStock _mostock in objs)
            {
                string htkey = _mostock.MOCode + _mostock.ItemCode;
                if (_ht.Contains(htkey))
                {
                    _mostock.MOLoadingQty = Convert.ToDecimal(_ht[htkey]);
                }
            }
        }

        //工单上料采集某物料的总数
        private Hashtable GetHTMOLoadingMaterialCount(object[] parmobjs)
        {
            string mocodes = this.GetmocodesCondition(parmobjs);

            //获取要盘点的库房对应的产线资源的在制品上料信息
            //tblonwipitem 表中 ActionType表示上料（0）或者下料（1）

            //此处计算所有上过工单的料号，不论是否已经下料
            string sql = string.Format(@" SELECT MOCODE,MITEMCODE,sum(QTY) as QTY
										FROM tblonwipitem
										WHERE 1 = 1 
										/*AND actiontype = 0*/
										and mocode in ({0})
										group by MOCODE,MITEMCODE ", mocodes);

            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Material.OnWIPItem2), new SQLCondition(sql));

            //对上料信息作统计。
            Hashtable _ht = new Hashtable();	//上料料号作键值，上料数量作Value
            if (objs != null)
                foreach (BenQGuru.eMES.Material.OnWIPItem2 _onwipitem2 in objs)
                {
                    string htkey = _onwipitem2.MOCode + _onwipitem2.MItemCode;	//主键用工单号加料号
                    if (_ht.Contains(htkey))
                    {
                        _ht[htkey] = Convert.ToDecimal(_ht[htkey]) + _onwipitem2.Qty;
                    }
                    else
                    {
                        _ht.Add(htkey, _onwipitem2.Qty);
                    }
                }

            //返回统计结果
            return _ht;
        }

        #endregion


        #region 工单物料维修不良数量
        //维修此工单不良所换上该物料的总数
        public void GetTSLoadingMaterialCount(object[] objs)
        {
            if (objs == null || objs.Length == 0) return;

            Hashtable _ht = this.GetHTTSLoadingMaterialCount(objs);

            foreach (MOStock _mostock in objs)
            {
                string htkey = _mostock.MOCode + _mostock.ItemCode;
                if (_ht.Contains(htkey))
                {
                    _mostock.TSLoadingQty = Convert.ToDecimal(_ht[htkey]);
                }
            }
        }
        private Hashtable GetHTTSLoadingMaterialCount(object[] parmobjs)
        {
            string mocodes = this.GetmocodesCondition(parmobjs);

            //获取要盘点的库房对应的产线资源的在制品上料信息
            //tblonwipitem 表中 ActionType表示上料（0）或者下料（1）
            string sql = string.Format(@" SELECT MOCODE,MITEMCODE,sum(QTY) as QTY
										FROM tbltsitem
										WHERE 1 = 1 
										and mocode in ({0})
										group by MOCODE,MITEMCODE ", mocodes);

            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Material.OnWIPItem2), new SQLCondition(sql));

            //对TS上料信息作统计。
            Hashtable _ht = new Hashtable();	//上料料号作键值，上料数量作Value
            if (objs != null)
                foreach (BenQGuru.eMES.Material.OnWIPItem2 _onwipitem2 in objs)
                {
                    string htkey = _onwipitem2.MOCode + _onwipitem2.MItemCode;	//主键用工单号加料号
                    if (_ht.Contains(htkey))
                    {
                        _ht[htkey] = Convert.ToDecimal(_ht[htkey]) + _onwipitem2.Qty;
                    }
                    else
                    {
                        _ht.Add(htkey, _onwipitem2.Qty);
                    }
                }

            //返回统计结果
            return _ht;

        }

        #endregion


        #region 工单物料未修完数量

        //工单某物料未修完数量
        public void GetUnCompletedMaterialCount(object[] objs)
        {
            if (objs == null || objs.Length == 0) return;

            Hashtable _ht = this.GetHTUnCompletedMaterialCount(objs);

            foreach (MOStock _mostock in objs)
            {
                string htkey = _mostock.MOCode + _mostock.ItemCode;
                if (_ht.Contains(htkey))
                {
                    _mostock.TSUnCompletedQty = Convert.ToDecimal(_ht[htkey]);
                }
            }
        }

        private Hashtable GetHTUnCompletedMaterialCount(object[] parmobjs)
        {
            string mocodes = this.GetmocodesCondition(parmobjs);
            //TS未维修完成没有记录料品信息，需要到上料记录中查询
            //获取要盘点的库房对应的产线资源的在制品上料信息
            //tblonwipitem 表中 ActionType表示上料（0）或者下料（1）
            string sql = string.Format(@" SELECT MOCODE,MITEMCODE,sum(QTY) as QTY
										FROM tbltsitem
										WHERE rcard IN (
											SELECT rcard
												FROM tblts
											WHERE mocode in ({0})
											AND tsstatus IN ('tsstatus_new', 'tsstatus_confirm', 'tsstatus_ts'))
										group by MOCODE,MITEMCODE ", mocodes);

            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Material.OnWIPItem2), new SQLCondition(sql));

            //对TS上料信息作统计。
            Hashtable _ht = new Hashtable();		//上料料号作键值，上料数量作Value
            if (objs != null)
                foreach (BenQGuru.eMES.Material.OnWIPItem2 _onwipitem2 in objs)
                {
                    string htkey = _onwipitem2.MOCode + _onwipitem2.MItemCode;	//主键用工单号加料号
                    if (_ht.Contains(htkey))
                    {
                        _ht[htkey] = Convert.ToDecimal(_ht[htkey]) + _onwipitem2.Qty;
                    }
                    else
                    {
                        _ht.Add(htkey, _onwipitem2.Qty);
                    }
                }

            //返回统计结果
            return _ht;
        }

        #endregion

        //获取工单状态
        public object[] GetMOByMoStock(object[] objs)
        {
            if (objs == null || objs.Length == 0) return objs;

            string mocodes = this.GetmocodesCondition(objs);
            string sql = string.Format(" select {0} from tblmo where mocode in ({1}) ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.MOModel.MO)), mocodes);
            object[] objmos = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.MO), new SQLCondition(sql));
            Hashtable _ht = new Hashtable();
            foreach (MO _mo in objmos)
            {
                _ht.Add(_mo.MOCode, _mo.MOStatus);
            }
            foreach (MOStock _mostock in objs)
            {
                if (_ht.Contains(_mostock.MOCode))
                {
                    _mostock.MOStatus = (string)_ht[_mostock.MOCode];
                }
            }

            return objs;
        }

        //拼出工单状态sql condition
        private string GetmocodesCondition(object[] objs)
        {
            string mocodes = "''";
            Hashtable _ht = new Hashtable();
            foreach (MOStock _mostock in objs)
            {
                if (!_ht.Contains(_mostock.MOCode))
                {
                    _ht.Add(_mostock.MOCode, _mostock.MOCode);
                    mocodes += ",'" + _mostock.MOCode + "'";
                }
            }
            return mocodes;
        }

        #endregion

        #endregion


        #region InvTransfer
        /// <summary>
        /// 库存移转单
        /// </summary>
        public InvTransfer CreateNewInvTransfer()
        {
            return new InvTransfer();
        }

        public void AddInvTransfer(InvTransfer invTransfer)
        {
            this._helper.AddDomainObject(invTransfer);
        }

        public void UpdateInvTransfer(InvTransfer invTransfer)
        {
            this._helper.UpdateDomainObject(invTransfer);
        }

        public void DeleteInvTransfer(InvTransfer invTransfer)
        {
            this._helper.DeleteDomainObject(invTransfer);
        }

        public void DeleteInvTransfer(InvTransfer[] invTransfer)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < invTransfer.Length; i++)
                {
                    this.DeleteInvTransfer(invTransfer[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message);
            }
        }

        public object GetInvTransfer(string transferNO)
        {
            return this.DataProvider.CustomSearch(typeof(InvTransfer), new object[] { transferNO });
        }

        public object[] QueryTransfer(string transferNO, string itemCode, string recType, string trnsfearStatus, int createStartDate, int createEndDate, string fromStorageID, string toStorageID, int inclusive, int exclusive)
        {
            string sql = "select TRANSFERNO, FRMSTORAGEID, TOSTORAGEID, TRANSFERSTATUS, RECTYPE, MEMO,CREATEDATE,CREATETIME, CREATEUSER,ORGID,MDATE , MTIME,MUSER ";
            sql += " FROM (select distinct A.TRANSFERNO, A.FRMSTORAGEID, A.TOSTORAGEID, A.TRANSFERSTATUS, A.RECTYPE, A.MEMO,A.CREATEDATE,A.CREATETIME, A.CREATEUSER,A.ORGID,A.MDATE , A.MTIME,A.MUSER ";
            sql += " FROM TBLINVTRANSFER A";
            if (itemCode.Trim().Length > 0)
            {
                sql += " LEFT JOIN TBLINVTRANSFERDETAIL B ON A.TRANSFERNO = B.TRANSFERNO";
            }
            sql += " WHERE 1=1 ";
            if (transferNO.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.TRANSFERNO in  ({0}) ", FormatHelper.ProcessQueryValues(transferNO));
            }
            if (itemCode.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND B.ITEMCODE in ({0}) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            if (recType.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.RECTYPE = '{0}' ", recType);
            }
            if (trnsfearStatus.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.TRANSFERSTATUS = '{0}' ", trnsfearStatus);
            }
            if (fromStorageID.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.FRMSTORAGEID = {0} ", FormatHelper.ProcessQueryValues(fromStorageID));
            }
            if (toStorageID.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.TOSTORAGEID = {0} ", FormatHelper.ProcessQueryValues(toStorageID));
            }

            if (createStartDate > 0)
            {
                sql = sql + string.Format(" AND A.CREATEDATE >= {0} ", FormatHelper.ProcessQueryValues(createStartDate.ToString()));
            }
            if (createEndDate > 0)
            {
                sql = sql + string.Format(" AND A.CREATEDATE <= {0} ", FormatHelper.ProcessQueryValues(createEndDate.ToString()));
            }
            sql += " )";

            return this.DataProvider.CustomQuery(typeof(Domain.Warehouse.InvTransfer),
                    new PagerCondition(sql, "TRANSFERNO", inclusive, exclusive));
        }

        public int QueryTransferCount(string transferNO, string itemCode, string recType, string trnsfearStatus, int createStartDate, int createEndDate, string fromStorageID, string toStorageID)
        {
            string sql = "select COUNT(*) ";
            sql += " FROM (select distinct A.TRANSFERNO ";
            sql += " FROM TBLINVTRANSFER A";
            if (itemCode.Trim().Length > 0)
            {
                sql += " LEFT JOIN TBLINVTRANSFERDETAIL B ON A.TRANSFERNO = B.TRANSFERNO";
            }
            sql += " WHERE 1=1 ";
            if (transferNO.Trim().Length > 0)
            {
                //sql = sql + string.Format(" AND A.TRANSFERNO LIKE '%{0}%' ", transferNO);
                sql = sql + string.Format(" AND A.TRANSFERNO in  ({0}) ", FormatHelper.ProcessQueryValues(transferNO));
            }
            if (itemCode.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND B.ITEMCODE in ({0}) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            if (recType.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.RECTYPE = '{0}' ", recType);
            }
            if (trnsfearStatus.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.TRANSFERSTATUS = '{0}' ", trnsfearStatus);
            }
            if (fromStorageID.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.FRMSTORAGEID = {0} ", FormatHelper.ProcessQueryValues(fromStorageID));
            }
            if (toStorageID.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.TOSTORAGEID = {0} ", FormatHelper.ProcessQueryValues(toStorageID));
            }

            if (createStartDate > 0)
            {
                sql = sql + string.Format(" AND A.CREATEDATE >= {0} ", FormatHelper.ProcessQueryValues(createStartDate.ToString()));
            }
            if (createEndDate > 0)
            {
                sql = sql + string.Format(" AND A.CREATEDATE <= {0} ", FormatHelper.ProcessQueryValues(createEndDate.ToString()));
            }
            sql += " )";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public decimal QueryTransferQty(string transferNO)
        {
            string sql = "select {0} ";
            sql += " FROM TBLINVTRANSFERDETAIL ";
            sql += " WHERE 1=1 ";
            if (transferNO.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND TRANSFERNO = '{0}' ", transferNO);
            }
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Warehouse.InvTransferDetail)));

            object[] objs = this.DataProvider.CustomQuery(typeof(Domain.Warehouse.InvTransferDetail),
                    new SQLCondition(sql));

            //object[] objs =  this.DataProvider.CustomQuery(typeof(InvTransferDetail), new SQLCondition(sql));
            decimal qty = 0;
            if (objs != null)
            {
                foreach (InvTransferDetail detail in objs)
                {
                    qty += detail.Planqty;
                }
            }
            return qty;
        }

        #endregion
        #region Asndetailsn
        /// <summary>
        /// TBLASNDETAILSN
        /// </summary>
        public Asndetailsn CreateNewAsndetailsn()
        {
            return new Asndetailsn();
        }

        public void AddAsndetailsn(Asndetailsn asndetailsn)
        {
            this.DataProvider.Insert(asndetailsn);
        }

        public void DeleteAsndetailsn(Asndetailsn asndetailsn)
        {
            this.DataProvider.Delete(asndetailsn);
        }

        public void UpdateAsndetailsn(Asndetailsn asndetailsn)
        {
            this.DataProvider.Update(asndetailsn);
        }

        public object GetAsndetailsn(string Sn, string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(Asndetailsn), new object[] { Sn, Stno });
        }

        #endregion
        #region Asndetailitem
        /// <summary>
        /// TBLASNDETAILITEM
        /// </summary>
        public Asndetailitem CreateNewAsndetailitem()
        {
            return new Asndetailitem();
        }

        public void AddAsndetailitem(Asndetailitem asndetailitem)
        {
            this.DataProvider.Insert(asndetailitem);
        }

        public void DeleteAsndetailitem(Asndetailitem asndetailitem)
        {
            this.DataProvider.Delete(asndetailitem);
        }

        public void UpdateAsndetailitem(Asndetailitem asndetailitem)
        {
            this.DataProvider.Update(asndetailitem);
        }

        public object GetAsndetailitem(string Invno, int Stline, string Invline, string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(Asndetailitem), new object[] { Invno, Stline, Invline, Stno });
        }


        #endregion


        #region InvTransferDetail
        /// <summary>
        /// 库存移转单明细
        /// </summary>
        public InvTransferDetail CreateNewInvTransferDetail()
        {
            return new InvTransferDetail();
        }

        public void AddInvTransferDetail(InvTransferDetail invTransferDetail)
        {
            this._helper.AddDomainObject(invTransferDetail);
        }

        public void UpdateInvTransferDetail(InvTransferDetail invTransferDetail)
        {
            this._helper.UpdateDomainObject(invTransferDetail);
        }

        public void DeleteInvTransferDetail(InvTransferDetail invTransferDetail)
        {
            this._helper.DeleteDomainObject(invTransferDetail);
        }

        public void DeleteInvTransferDetail(InvTransferDetail[] invTransferDetail)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < invTransferDetail.Length; i++)
                {
                    this.DeleteInvTransferDetail(invTransferDetail[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message);
            }
        }

        public object GetInvTransferDetail(string transferNO, int transferLine)
        {
            return this.DataProvider.CustomSearch(typeof(InvTransferDetail), new object[] { transferNO, transferLine });
        }

        public object[] GetInvTransferDetailByTransferNO(string transferNO)
        {
            string sql = "select {0} ";
            sql += " FROM TBLINVTRANSFERDETAIL ";
            sql += " WHERE 1=1 ";
            if (transferNO.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND TRANSFERNO = '{0}' ", transferNO);
            }
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.Warehouse.InvTransferDetail)));

            return this.DataProvider.CustomQuery(typeof(Domain.Warehouse.InvTransferDetail),
                    new SQLCondition(sql));
        }

        public object[] QueryTransferDetail(string transferNO, int inclusive, int exclusive)
        {
            string sql = "select TRANSFERNO, TRANSFERLINE, ORDERNO, ORDERLINE, ITEMCODE, MDESC,MOCODE,TransferSTATUS, MEMO,PLANQTY,ACTQTY,CustomerCode,CUSTOMERNAME,TransferDATE,TransferTIME, TransferUSER,MDATE , MTIME,MUSER from ";
            sql += "(select distinct A.TRANSFERNO, A.TRANSFERLINE, A.ORDERNO, A.ORDERLINE, A.ITEMCODE, B.MDESC,A.MOCODE,A.TransferSTATUS,";
            sql += " A.MEMO,A.PLANQTY,A.ACTQTY,A.CustomerCode,A.CUSTOMERNAME,A.TransferDATE,A.TransferTIME, A.TransferUSER,A.MDATE , A.MTIME,A.MUSER ";
            sql += " FROM TBLINVTRANSFERDETAIL A";
            sql += " LEFT JOIN tblinvtransfer tblinvtransfer ON A.transferno = tblinvtransfer.transferno ";
            sql += " LEFT JOIN TBLMATERIAL B ON A.ITEMCODE = B.MCODE AND tblinvtransfer.ORGID = B.ORGID ";
            sql += " WHERE 1=1 ";
            if (transferNO.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.TRANSFERNO = '{0}' ", transferNO);
            }
            sql += " ) ";

            return this.DataProvider.CustomQuery(typeof(Domain.Warehouse.InvTransferDetailForQuey),
                    new PagerCondition(sql, "TRANSFERNO,TRANSFERLINE", inclusive, exclusive));
        }

        public int QueryTransferCountDetail(string transferNO)
        {
            string sql = "select COUNT(*) from ";
            sql += " (select distinct A.TRANSFERNO, A.TRANSFERLINE,B.MDESC FROM TBLINVTRANSFERDETAIL A ";
            sql += " LEFT JOIN tblinvtransfer tblinvtransfer ON A.transferno = tblinvtransfer.transferno ";
            sql += " LEFT JOIN TBLMATERIAL B ON A.ITEMCODE = B.MCODE AND tblinvtransfer.ORGID = B.ORGID ";
            sql += " WHERE 1=1 ";
            if (transferNO.Trim().Length > 0)
            {
                sql = sql + string.Format(" AND A.TRANSFERNO = '{0}' ", transferNO);
            }
            sql += " ) ";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        public object[] GetInvTransferDetailForQuery(string frmStorageID, string transferNo)
        {
            string sql = "SELECT A.TRANSFERLINE, A.ITEMCODE, C.MDESC, (SELECT SUM(TBLSTORAGEINFO.STORAGEQTY) FROM TBLSTORAGEINFO ";
            sql += " WHERE TBLSTORAGEINFO.STORAGEID = B.FRMSTORAGEID AND TBLSTORAGEINFO.MCODE=A.ITEMCODE) AS FRMSTORAGEQTY, A.PLANQTY, A.ACTQTY, A.MEMO, ";
            sql += " A.MOCODE, A.CUSTOMERCODE, A.CUSTOMERNAME FROM TBLINVTRANSFERDETAIL A ";
            sql += " INNER JOIN TBLINVTRANSFER B ON A.TRANSFERNO = B.TRANSFERNO ";
            sql += " INNER JOIN TBLMATERIAL C ON A.ITEMCODE = C.MCODE AND C.ORGID = B.ORGID WHERE 1=1 ";
            if (frmStorageID.Trim() != string.Empty)
            {
                sql += " AND B.FRMSTORAGEID = '" + frmStorageID + "'";
            }
            if (transferNo.Trim() != string.Empty)
            {
                sql += " AND B.TRANSFERNO = '" + transferNo + "'";
            }
            object[] obj = this.DataProvider.CustomQuery(typeof(InvTransferDetailForQuey), new SQLCondition(sql));
            if (obj != null)
            {
                return obj;
            }
            return null;
        }

        public object[] GetInvTransferDetailForQuery(string frmStorageID, string toStorageID, string transferNo)
        {
            string sql = "SELECT A.TRANSFERLINE, A.ITEMCODE, B.TOSTORAGEID, C.MDESC,C.MCONTROLTYPE, (SELECT SUM(TBLSTORAGEINFO.STORAGEQTY) FROM TBLSTORAGEINFO ";
            sql += " WHERE TBLSTORAGEINFO.STORAGEID = B.FRMSTORAGEID AND TBLSTORAGEINFO.MCODE = A.ITEMCODE) AS FRMSTORAGEQTY, (SELECT SUM(TBLSTORAGEINFO.STORAGEQTY) FROM TBLSTORAGEINFO ";
            sql += " WHERE TBLSTORAGEINFO.STORAGEID = B.TOSTORAGEID AND TBLSTORAGEINFO.MCODE = A.ITEMCODE) AS TOSTORAGEQTY,  ";
            //edit by kathy @20140626 查询批次料库存数量-已备料数量
            sql += @" (select nvl(sum(lotqty), 0) as lotqty
          from tblstoragelotinfo
         where mcode = A.ITEMCODE
           and storageid = B.FRMSTORAGEID
           and lotno not in
               (select lotno
                  from tblminno
                 where itemcode = tblstoragelotinfo.mcode)) as frmlotqty,
       (select nvl(sum(lotqty), 0) as lotqty
          from tblstoragelotinfo
         where mcode = A.ITEMCODE
           and storageid = B.TOSTORAGEID
           and lotno not in
               (select lotno
                  from tblminno
                 where itemcode = tblstoragelotinfo.mcode)) as tolotqty, ";
            sql += " A.PLANQTY, A.ACTQTY, A.MEMO, A.MOCODE FROM TBLINVTRANSFERDETAIL A ";
            sql += " INNER JOIN TBLINVTRANSFER B ON A.TRANSFERNO = B.TRANSFERNO INNER JOIN TBLMATERIAL C ON A.ITEMCODE = C.MCODE ";
            sql += " AND C.ORGID = B.ORGID LEFT JOIN TBLSTORAGE D ON B.TOSTORAGEID = D.STORAGECODE ";
            sql += " WHERE 1=1 ";
            if (frmStorageID.Trim() != string.Empty)
            {
                sql += " AND B.FRMSTORAGEID = '" + frmStorageID + "'";
            }
            if (toStorageID.Trim() != string.Empty)
            {
                sql += " AND B.TOSTORAGEID = '" + toStorageID + "'";
            }
            if (transferNo.Trim() != string.Empty)
            {
                sql += " AND  B.TRANSFERNO = '" + transferNo + "'";
            }
            object[] obj = this.DataProvider.CustomQuery(typeof(InvTransferDetailForQuey), new SQLCondition(sql));
            if (obj != null)
            {
                return obj;
            }
            return null;
        }

        public object[] QueryInvTransferDetailForQuery(string frmStorageID, string transferNo)
        {
            string sql = "SELECT A.TRANSFERLINE, A.ITEMCODE, C.MDESC, (SELECT SUM(TBLSTORAGEINFO.STORAGEQTY) FROM TBLSTORAGEINFO ";
            sql += " WHERE TBLSTORAGEINFO.STORAGEID = B.FRMSTORAGEID AND TBLSTORAGEINFO.MCODE=A.ITEMCODE) AS FRMSTORAGEQTY, A.PLANQTY, A.ACTQTY, A.MEMO, ";
            sql += " A.MOCODE, A.CUSTOMERCODE, A.CUSTOMERNAME FROM TBLINVTRANSFERDETAIL A ";
            sql += " INNER JOIN TBLINVTRANSFER B ON A.TRANSFERNO = B.TRANSFERNO ";
            sql += " INNER JOIN TBLMATERIAL C ON A.ITEMCODE = C.MCODE AND C.ORGID = B.ORGID WHERE 1=1 ";
            if (frmStorageID.Trim() != string.Empty)
            {
                sql += " AND B.FRMSTORAGEID = '" + frmStorageID + "'";
            }
            if (transferNo.Trim() != string.Empty)
            {
                sql += " AND B.TRANSFERNO = '" + transferNo + "'";
            }
            object[] obj = this.DataProvider.CustomQuery(typeof(InvTransferDetailForQuey), new SQLCondition(sql));
            if (obj != null)
            {
                return obj;
            }
            return null;
        }


        public int GetInvTransferDetailCount(string transferno, string status)
        {
            string sql = "select count(*) from tblinvtransferdetail where 1=1 ";
            if (transferno.Trim() != string.Empty)
            {
                sql += " and  transferno='" + transferno + "'";
            }

            if (status.Trim() != string.Empty)
            {
                sql += " and  transferstatus<>'" + status + "'";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region InvTransferDetail
        /// <summary>
        /// 库存移转单明细
        /// </summary>
        public InvTransferMerge CreateNewInvTransferMerge()
        {
            return new InvTransferMerge();
        }

        public void AddInvTransferMerge(InvTransferMerge invTransferMerge)
        {
            this._helper.AddDomainObject(invTransferMerge);
        }

        public void UpdateInvTransferMerge(InvTransferMerge invTransferMerge)
        {
            this._helper.UpdateDomainObject(invTransferMerge);
        }

        public void DeleteInvTransferMerge(InvTransferMerge invTransferMerge)
        {
            this._helper.DeleteDomainObject(invTransferMerge);
        }

        #endregion

        #region Undo维修换料库存操作
        /// <summary>
        /// 维修上料扣库存
        /// </summary>
        /// <param name="runningCard">流程卡号</param>
        /// <param name="moCode">工单号</param>
        public void UnDoReplaceMaterialStock(string runningCard, string moCode, string ssCode, object[] objs, string userCode, string opCode)
        {
            // Return by Icyer 2005/0929
            // 取消物料模块
            //Laws Lu,2005/10/20,恢复	使用配置文件来控制物料模块是否使用
            //return;

            /*
             1. 读取对应仓库
             2. 判断上料类型
             3. 读取物料代码及数量
                3.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                3.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
             4. 扣库存
             5. 更新工单使用量
            */


            //			//1. 读取OnWIPItem中的上料操作信息
            //			//Laws Lu,2005/12/22,修改 条件中不能包含RunningCardSequence，RunningCardSequence会递增改变
            //			object[] objs = this.DataProvider.CustomSearch(typeof(OnWIPItem),new string[]{"RunningCard", "MOCode", "TransactionStatus"} , new object[]{ runningCard, moCode, TransactionStatus.TransactionStatus_NO });
            if (objs == null)
                return;


            //2. 判断工序是否上料、扣料工序
            TSItem wip = (TSItem)objs[0];

            //3. 读取对应仓库
            Warehouse wh = this.GetWarehouseFromTSWip(wip, ssCode);
            if (wh == null)
            {
                return;
            }

            #region 更换的物料，加库存
            //4. 判断上料类型
            ArrayList listRemoveItems = new ArrayList();	//待扣物料
            //			if (wip.MCardType == MCardType.MCardType_Keyparts)	//KeyParts上料
            //			{
            //5.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
            listRemoveItems = this.GetKeypartsFromWIP(objs);
            //			}

            //5.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
            foreach (object obj in objs)
            {
                TSItem objItem = obj as TSItem;
                if (objItem.MCardType == MCardType.MCardType_INNO)
                {
                    listRemoveItems.Add(this.GeInnoByMItemCode(objItem.MItemCode, objItem.Qty));
                }
            }

            if (listRemoveItems.Count == 0)
                return;

            //6. 增库存
            this.UpdateWarehouseStockFromList(listRemoveItems, wh, 1, wip.MaintainUser);

            //7. 减少工单使用量
            this.SubMOStockFromWIP(moCode, listRemoveItems);
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


            //8. 修改OnWIPItem.TransactionStatus
            string strSql = string.Format("update TBLONWIPITEM set TRANSSTATUS='{0}',actiontype={1},dropop='TS',dropuser='{2}',dropdate={3},droptime={4} where MCARD=$MCARD AND RCARD=$rcard and mitemcode=$mitemcode and MOCODE=$mocode and TRANSSTATUS=$transstatus"
                , new object[]{
								  TransactionStatus.TransactionStatus_YES
								  ,(int)MaterialType.DropMaterial
								  ,userCode
								  ,FormatHelper.TODateInt(dtNow)
								  ,FormatHelper.TOTimeInt(dtNow)
							  });
            this.DataProvider.CustomExecute(new SQLParamCondition(strSql, new SQLParameter[]{
																								new SQLParameter("mcard", typeof(string), wip.MCard), 
																								new SQLParameter("rcard", typeof(string), runningCard), 
																								new SQLParameter("mitemcode", typeof(string), wip.MItemCode),
																								new SQLParameter("mocode", typeof(string), moCode),
																								new SQLParameter("transstatus", typeof(string), TransactionStatus.TransactionStatus_YES)
																							}));
            #endregion

            #region 换下的物料，减库存
            //4. 是否需要增加库存，不良品是不需要增加库存的
            //			if( !FormatHelper.StringToBoolean(wip.IsReTS))
            //			{
            ArrayList listAddItems = new ArrayList();	//待扣物料
            //				if (wip.MSourceCardType == MCardType.MCardType_Keyparts)	//KeyParts上料
            //				{
            //5.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
            listAddItems = (this.GetRemoveKeypartsFromWIP(objs));
            //				}
            //5.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
            foreach (object obj in objs)
            {
                TSItem objItem = obj as TSItem;
                if (objItem.MSourceCardType == MCardType.MCardType_INNO)
                {
                    listAddItems.Add(this.GeInnoByMItemCode(objItem.SourceItemCode, objItem.Qty));
                }
            }

            if (listAddItems.Count == 0)
                return;

            //6. 减库存
            this.UpdateWarehouseStockFromList(listAddItems, wh, -1, wip.MaintainUser);

            //7. 更新工单使用量
            this.UpdateMOStockFromWIP(moCode, listAddItems);

            //8. 修改OnWIPItem.TransactionStatus
            string strSql1 = string.Format("update TBLONWIPITEM set TRANSSTATUS='{0}',actiontype={1},dropuser='',dropdate='',dropop='',droptime='' where MCARD=$MCARD AND RCARD=$rcard and mitemcode=$mitemcode and MOCODE=$mocode and TRANSSTATUS=$transstatus", TransactionStatus.TransactionStatus_YES, (int)MaterialType.CollectMaterial);
            this.DataProvider.CustomExecute(new SQLParamCondition(strSql1, new SQLParameter[]{
																								new SQLParameter("mcard", typeof(string), wip.MSourceCard), 
																								new SQLParameter("rcard", typeof(string), runningCard), 
																								new SQLParameter("mitemcode", typeof(string), wip.SourceItemCode),
																								new SQLParameter("mocode", typeof(string), moCode),
																								new SQLParameter("transstatus", typeof(string), TransactionStatus.TransactionStatus_YES)
																							}));

            //Laws Lu,2006/01/09,同时执行下料操作
            //			TSItem item = new TSItem();
            //			item = objs[0] as TSItem;
            //
            //			item.SourceItemCode = (objs[0] as TSItem).SourceItemCode;
            //			item.MItemCode = (objs[0] as TSItem).MItemCode;
            //			item.MSourceCardType = (objs[0] as TSItem).MSourceCardType;
            //			item.MCardType = (objs[0] as TSItem).MCardType;

            //object[] objRemoves = new object[]{item};
            //CollectMaterialStock(objs,"('" + runningCard + "')",moCode,userCode,ssCode,opCode);
            //			}

            this.ExecCacheSQL(); //执行缓存的更新库存和工单的ＳＱＬ　joe song　20050223
            #endregion
        }
        #endregion

        #region 维修换料库存操作
        /// <summary>
        /// 维修上料扣库存
        /// </summary>
        /// <param name="runningCard">流程卡号</param>
        /// <param name="moCode">工单号</param>
        public void ReplaceMaterialStock(string runningCard, string moCode, string ssCode, object[] objs, string userCode, string opCode)
        {
            // Return by Icyer 2005/0929
            // 取消物料模块
            //Laws Lu,2005/10/20,恢复	使用配置文件来控制物料模块是否使用
            //return;

            /*
             1. 读取对应仓库
             2. 判断上料类型
             3. 读取物料代码及数量
                3.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                3.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
             4. 扣库存
             5. 更新工单使用量
            */


            //			//1. 读取OnWIPItem中的上料操作信息
            //			//Laws Lu,2005/12/22,修改 条件中不能包含RunningCardSequence，RunningCardSequence会递增改变
            //			object[] objs = this.DataProvider.CustomSearch(typeof(OnWIPItem),new string[]{"RunningCard", "MOCode", "TransactionStatus"} , new object[]{ runningCard, moCode, TransactionStatus.TransactionStatus_NO });
            if (objs == null)
                return;


            //2. 判断工序是否上料、扣料工序
            TSItem wip = (TSItem)objs[0];

            //3. 读取对应仓库
            Warehouse wh = this.GetWarehouseFromTSWip(wip, ssCode);
            if (wh == null)
            {
                return;
            }

            #region 更换的物料，减库存
            //4. 判断上料类型
            ArrayList listItems = new ArrayList();	//待扣物料
            //			if (wip.MCardType == MCardType.MCardType_Keyparts)	//KeyParts上料
            //			{
            //5.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
            listItems = this.GetKeypartsFromWIP(objs);
            //			}

            //5.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
            foreach (object obj in objs)
            {
                TSItem objItem = obj as TSItem;
                if (objItem.MCardType == MCardType.MCardType_INNO)
                {
                    listItems.Add(this.GeInnoByMItemCode(objItem.MItemCode, objItem.Qty));
                }
            }

            if (listItems.Count == 0)
                return;

            //6. 减库存
            this.UpdateWarehouseStockFromList(listItems, wh, -1, wip.MaintainUser);

            //7. 更新工单使用量
            this.UpdateMOStockFromWIP(moCode, listItems);


            #endregion

            #region 换下的物料，增库存
            //Laws Lu,2006/02/13,修改 两次增加了库房
            //4. 是否需要增加库存，不良品是不需要增加库存的
            //			if( !FormatHelper.StringToBoolean(wip.IsReTS))
            //			{
            //			ArrayList listAddItems = new ArrayList();	//待扣物料
            //			//				if (wip.MSourceCardType == MCardType.MCardType_Keyparts)	//KeyParts上料
            //			//				{
            //			//5.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
            //			listAddItems = (this.GetSourceKeypartsFromTSItem(objs));
            //			//				}
            //			//5.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
            //			foreach(object obj in objs)
            //			{
            //				TSItem objItem = obj as TSItem;
            //				if(objItem.MSourceCardType == MCardType.MCardType_INNO)
            //				{
            //					listAddItems.Add(this.GeInnoByMItemCode(objItem.SourceItemCode,objItem.Qty));
            //				}
            //			}
            //
            //			if (listAddItems.Count == 0)
            //				return;
            //			
            //			//6. 增库存
            //			this.UpdateWarehouseStockFromList(listAddItems, wh, 1, wip.MaintainUser);
            //
            //			//7. 更新工单使用量
            //			this.UpdateMOStockFromWIP(moCode, listAddItems);
            //Laws Lu,2006/01/06,同时执行下料操作

            RemoveWarehouse(objs, "('" + runningCard + "')", moCode, userCode, ssCode, opCode);
            //			}

            this.ExecCacheSQL(); //执行缓存的更新库存和工单的ＳＱＬ　joe song　20050223
            #endregion
        }


        #endregion

        #region 上料扣库存
        /// <summary>
        /// 上料扣库存
        /// </summary>
        /// <param name="runningCard">流程卡号</param>
        /// <param name="runningCardSequence">序号</param>
        /// <param name="moCode">工单号</param>
        public void CollectMaterialStock(string runningCard, string runningCardSequence, string moCode)
        {
            // Return by Icyer 2005/0929
            // 取消物料模块
            //Laws Lu,2005/10/20,恢复	使用配置文件来控制物料模块是否使用
            //return;

            /*
             1. 读取OnWIPItem中的上料操作信息
             2. 判断工序是否上料、扣料工序
             3. 读取对应仓库
             4. 判断上料类型
             5. 读取物料代码及数量
                5.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                5.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
             6. 扣库存
             7. 更新工单使用量
             8. 修改OnWIPItem.TransactionStatus
            */


            //1. 读取OnWIPItem中的上料操作信息
            //Laws Lu,2005/12/22,修改 条件中不能包含RunningCardSequence，RunningCardSequence会递增改变
            object[] objs = this.DataProvider.CustomSearch(typeof(OnWIPItem), new string[] { "RunningCard", "MOCode", "TransactionStatus" }, new object[] { runningCard, moCode, TransactionStatus.TransactionStatus_NO });
            if (objs == null)
                return;

            //2. 判断工序是否上料、扣料工序
            OnWIPItem wip = (OnWIPItem)objs[0];
            string strOPCode = wip.OPCode;
            string strRoute = wip.RouteCode;
            string strItemCode = wip.ItemCode;
            if (!CheckOP(strItemCode, strRoute, strOPCode))
            {
                return;
            }

            //3. 读取对应仓库
            Warehouse wh = this.GetWarehouseFromWIP(wip);
            if (wh == null)
            {
                return;
            }

            //4. 判断上料类型
            ArrayList listItems = new ArrayList();	//待扣物料
            if (wip.MCardType == MCardType.MCardType_Keyparts)	//KeyParts上料
            {
                //5.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                listItems = this.GetKeypartsFromWIP(objs);
            }
            else if (wip.MCardType == MCardType.MCardType_INNO)	//集成上料
            {
                //5.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
                listItems = this.GetINNOsFromWIP(wip);
            }
            if (listItems.Count == 0)
                return;

            //6. 扣库存
            this.UpdateWarehouseStockFromList(listItems, wh, -1, wip.MaintainUser);

            //7. 更新工单使用量
            this.UpdateMOStockFromWIP(moCode, listItems);

            //8. 修改OnWIPItem.TransactionStatus
            string strSql = string.Format("update TBLONWIPITEM set TRANSSTATUS='{0}',actiontype={1} where RCARD=$rcard and RCARDSEQ=$rcardseq and MOCODE=$mocode and TRANSSTATUS=$transstatus and actiontype={2}", new object[] { TransactionStatus.TransactionStatus_YES, (int)MaterialType.CollectMaterial, (int)MaterialType.CollectMaterial });
            this.DataProvider.CustomExecute(new SQLParamCondition(strSql, new SQLParameter[]{
																								new SQLParameter("rcard", typeof(string), runningCard), 
																								new SQLParameter("rcardseq", typeof(string), runningCardSequence), 
																								new SQLParameter("mocode", typeof(string), moCode),
																								new SQLParameter("transstatus", typeof(string), TransactionStatus.TransactionStatus_NO)
																							}));



        }
        /// <summary>
        /// 判断工序是否既是上料位也是扣料位
        /// </summary>
        /// <returns></returns>
        private bool CheckOP(string itemCode, string routeCode, string opCode)
        {
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op where itemcode=$itemcode and routecode=$routecode and opcode =$opcode" + GlobalVariables.CurrentOrganizations.GetSQLCondition();
            object[] objop = this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLParamCondition(selectSql, new SQLParameter[] { new SQLParameter("itemcode",typeof(string),itemCode),new SQLParameter("routecode",typeof(string),routeCode),
			                new SQLParameter("opcode",typeof(string), opCode)}));

            if (objop == null)
                return false;
            string strOPControl = ((ItemRoute2OP)objop[0]).OPControl;
            int iCollectMaterial = 0;	//上料位
            int iMaterialStock = 9;		//扣料位
            //判断上料位、扣料位
            bool b = (FormatHelper.StringToBoolean(strOPControl.Substring(iCollectMaterial, 1)) && FormatHelper.StringToBoolean(strOPControl.Substring(iMaterialStock, 1)));
            return b;
        }
        /// <summary>
        /// 根据OnWIPItem读取Keyparts上料物料代码及数量
        /// </summary>
        /// <returns>ArrayList(WarehouseStock列表)</returns>
        private ArrayList GetRemoveKeypartsFromWIP(object[] objs)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] is OnWIPItem)
                {
                    OnWIPItem item = (OnWIPItem)objs[i];

                    if (item.MCardType == MCardType.MCardType_Keyparts)
                    {
                        WarehouseStock stock = new WarehouseStock();
                        stock.ItemCode = item.MItemCode;
                        stock.OpenQty = item.Qty;
                        list.Add(stock);
                    }
                }
                else if (objs[i] is TSItem)
                {
                    TSItem item = (TSItem)objs[i];

                    if (item.MSourceCardType == MCardType.MCardType_Keyparts)
                    {
                        WarehouseStock stock = new WarehouseStock();
                        stock.ItemCode = item.SourceItemCode;
                        stock.OpenQty = item.Qty;
                        list.Add(stock);
                    }
                }
                else if (objs[i] is InnoObject)
                {
                    InnoObject item = (InnoObject)objs[i];

                    if (item.MCardType == MCardType.MCardType_Keyparts)
                    {
                        WarehouseStock stock = new WarehouseStock();
                        stock.ItemCode = item.MItemCode;
                        stock.OpenQty = item.Qty;
                        list.Add(stock);
                    }
                }

            }
            return list;
        }

        /// <summary>
        /// 根据OnWIPItem读取Keyparts上料物料代码及数量
        /// </summary>
        /// <returns>ArrayList(WarehouseStock列表)</returns>
        private ArrayList GetKeypartsFromWIP(object[] objs)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] is OnWIPItem)
                {
                    OnWIPItem item = (OnWIPItem)objs[i];

                    if (item.MCardType == MCardType.MCardType_Keyparts)
                    {
                        WarehouseStock stock = new WarehouseStock();
                        stock.ItemCode = item.MItemCode;
                        stock.OpenQty = item.Qty;
                        list.Add(stock);
                    }
                }
                else if (objs[i] is TSItem)
                {
                    TSItem item = (TSItem)objs[i];

                    if (item.MSourceCardType == MCardType.MCardType_Keyparts)
                    {
                        WarehouseStock stock = new WarehouseStock();
                        stock.ItemCode = item.MItemCode;
                        stock.OpenQty = item.Qty;
                        list.Add(stock);
                    }
                }
                else if (objs[i] is InnoObject)
                {
                    InnoObject item = (InnoObject)objs[i];

                    if (item.MCardType == MCardType.MCardType_Keyparts)
                    {
                        WarehouseStock stock = new WarehouseStock();
                        stock.ItemCode = item.MItemCode;
                        stock.OpenQty = item.Qty;
                        list.Add(stock);
                    }
                }

            }
            return list;
        }

        private ArrayList GetSourceKeypartsFromTSItem(object[] objs)
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < objs.Length; i++)
            {

                TSItem item = (TSItem)objs[i];

                WarehouseStock stock = new WarehouseStock();
                stock.ItemCode = item.SourceItemCode;
                stock.OpenQty = item.Qty;
                list.Add(stock);

            }
            return list;
        }



        private object GeInnoByMItemCode(string mItemCode, decimal qty)
        {
            //ArrayList list = new ArrayList();
            WarehouseStock stock = new WarehouseStock();

            stock.ItemCode = mItemCode;
            stock.OpenQty = qty;

            //list.Add(stock);

            return stock;
        }
        /// <summary>
        /// 更具物料号获取集成上料信息
        /// </summary>
        /// <param name="sCardId">物料序列号</param>
        /// <returns></returns>
        private ArrayList GetInnosByCard(string sCardId)
        {
            BenQGuru.eMES.Material.MaterialFacade mfacade = new BenQGuru.eMES.Material.MaterialFacade(this.DataProvider);
            object[] objsinno = mfacade.GetLastMINNOs(sCardId);
            ArrayList list = new ArrayList();
            if (objsinno != null)
            {
                for (int i = 0; i < objsinno.Length; i++)
                {
                    MINNO inno = (MINNO)objsinno[i];
                    WarehouseStock stock = new WarehouseStock();
                    if (FormatHelper.StringToBoolean(inno.IsTry) == false)
                        stock.ItemCode = inno.MItemCode;
                    else
                        stock.ItemCode = inno.TryItemCode;
                    stock.OpenQty = inno.Qty;
                    list.Add(stock);
                    inno = null;
                }
            }
            objsinno = null;
            return list;
        }
        /// <summary>
        /// 根据OnWIPItem读取集成上料物料代码及数量
        /// </summary>
        /// <returns>ArrayList(WarehouseStock列表)</returns>
        private ArrayList GetINNOsFromWIP(object wip)
        {
            string sCardId = String.Empty;
            ArrayList list = new ArrayList();

            if (wip is OnWIPItem)
            {
                sCardId = (wip as OnWIPItem).MCARD;
            }
            else if (wip is TSItem)
            {
                sCardId = (wip as TSItem).MCard;
            }
            else if (wip is InnoObject)
            {
                sCardId = (wip as TSItem).MCard;
            }
            //string sCardId = wip.MCARD;
            list = GetInnosByCard(sCardId);

            return list;
        }
        /// <summary>
        /// 根据OnWIPItem读取对应仓库
        /// </summary>
        /// <returns>Warehouse</returns>
        private Warehouse GetWarehouseFromWIP(OnWIPItem wip)
        {
            return this.GetWarehouseFromMOSS(wip.MOCode, wip.StepSequenceCode);
        }
        /// <summary>
        /// 根据OnWIPItem读取对应仓库
        /// </summary>
        /// <returns>Warehouse</returns>
        private Warehouse GetWarehouseFromTSWip(TSItem wip, string ssCode)
        {
            return this.GetWarehouseFromMOSS(wip.MOCode, ssCode);
        }
        /// <summary>
        /// 根据MOCode读取对应仓库
        /// </summary>
        /// <returns>Warehouse</returns>
        private Warehouse GetWarehouseFromMoCodeRes(string moCode, string resourceCode)
        {
            //读资源对应的产线
            BenQGuru.eMES.BaseSetting.BaseModelFacade facade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
            object obj = facade.GetResource(resourceCode);
            if (obj == null)
                return null;
            string strSSCode = ((BenQGuru.eMES.Domain.BaseSetting.Resource)obj).StepSequenceCode;
            return this.GetWarehouseFromMOSS(moCode, strSSCode);
        }
        /// <summary>
        /// 根据MOCode读取对应仓库
        /// </summary>
        /// <returns>Warehouse</returns>
        private Warehouse GetWarehouseFromMOSS(string moCode, string sscode)
        {
            //读工厂代码
            BenQGuru.eMES.MOModel.MOFacade mof = new BenQGuru.eMES.MOModel.MOFacade(this.DataProvider);
            object obj = mof.GetMO(moCode);
            string strFactoryCode = ((MO)obj).Factory;
            //读仓库
            string strSSCode = sscode;
            object[] objwh = this.DataProvider.CustomSearch(typeof(Warehouse2StepSequence), new string[] { "StepSequenceCode", "FactoryCode" }, new object[] { strSSCode, strFactoryCode });
            if (objwh == null)
                return null;
            Warehouse2StepSequence w2ss = (Warehouse2StepSequence)objwh[0];
            Warehouse wh = new Warehouse();
            wh.FactoryCode = w2ss.FactoryCode;
            //wh.SegmentCode = w2ss.SegmentCode;
            wh.WarehouseCode = w2ss.WarehouseCode;
            w2ss = null;
            return wh;
        }
        /// <summary>
        /// 扣减物料库存
        /// </summary>
        /// <param name="listItems">WarehouseStock列表</param>
        /// <param name="wh">对应的Warehouse</param>
        /// <param name="add_sub">1 或者 -1</param>
        private void UpdateWarehouseStockFromList(ArrayList listItems, Warehouse wh, int add_sub, string userId)
        {
            //如果只有一条物料，就直接更新了
            if (listItems.Count == 1)
            {
                WarehouseStock stock = (WarehouseStock)listItems[0];
                string strItemCode = stock.ItemCode;
                //读取原有库存
                object obj = this.GetWarehouseStock(strItemCode, wh.WarehouseCode, /* wh.SegmentCode, */ wh.FactoryCode);
                //原库存没有记录
                if (obj == null)
                {
                    //如果在物料主档里有此物料，则新增到库存表
                    if (this.GetWarehouseItem(strItemCode) != null)
                    {
                        WarehouseStock ws = this.CreateNewWarehouseStock();
                        ws.FactoryCode = wh.FactoryCode;
                        //ws.SegmentCode = wh.SegmentCode;
                        ws.WarehouseCode = wh.WarehouseCode;
                        ws.ItemCode = stock.ItemCode;
                        ws.OpenQty = add_sub * stock.OpenQty;
                        ws.MaintainUser = userId;
                        this.AddWarehouseStock(ws);
                        stock.EAttribute1 = ws.OpenQty.ToString();
                    }
                }
                else
                {
                    WarehouseStock ws = (WarehouseStock)obj;
                    ws.OpenQty = ws.OpenQty + add_sub * stock.OpenQty;
                    //this.UpdateWarehouse(ws);
                    this.UpdateWarehouseStockQty(ws, add_sub * stock.OpenQty);
                    stock.EAttribute1 = ws.OpenQty.ToString();
                }
            }
            else
            {
                //读出所有物料
                ArrayList list = new ArrayList();
                object[] objsitem = this.GetAllWarehouseItem();
                for (int i = 0; i < objsitem.Length; i++)
                {
                    list.Add(((WarehouseItem)objsitem[i]).ItemCode);
                }
                objsitem = null;
                //读出库存
                Hashtable htwh = new Hashtable();
                object[] objswh = this.GetWarehouseStockPro(string.Empty, wh.WarehouseCode, /*wh.SegmentCode,*/ wh.FactoryCode);
                if (objswh != null)
                {
                    for (int i = 0; i < objswh.Length; i++)
                    {
                        htwh.Add(((WarehouseStock)objswh[i]).ItemCode, objswh[i]);
                    }
                }
                objswh = null;
                //更新物料库存
                Hashtable htadded = new Hashtable();
                int icount = listItems.Count;
                for (int i = icount - 1; i >= 0; i--)
                {
                    WarehouseStock stock = (WarehouseStock)listItems[i];
                    if (!htwh.ContainsKey(stock.ItemCode))	//是否已有库存
                    {
                        if (list.Contains(stock.ItemCode))	//物料主档中是否存在
                        {
                            if (!htadded.ContainsKey(stock.ItemCode))	//是否重复更新
                            {
                                WarehouseStock ws = this.CreateNewWarehouseStock();
                                ws.FactoryCode = wh.FactoryCode;
                                //ws.SegmentCode = wh.SegmentCode;
                                ws.WarehouseCode = wh.WarehouseCode;
                                ws.ItemCode = stock.ItemCode;
                                ws.OpenQty = add_sub * stock.OpenQty;
                                ws.MaintainUser = userId;
                                this.AddWarehouseStock(ws);
                                htadded.Add(ws.ItemCode, ws);
                                stock.EAttribute1 = ws.OpenQty.ToString();
                            }
                            else
                            {
                                WarehouseStock ws = (WarehouseStock)htadded[stock.ItemCode];
                                ws.OpenQty = ws.OpenQty + add_sub * stock.OpenQty;
                                //this.UpdateWarehouseStock(ws);
                                this.UpdateWarehouseStockQty(ws, add_sub * stock.OpenQty);
                                stock.EAttribute1 = ws.OpenQty.ToString();
                            }
                        }
                        else	//如果物料主档里不存在，则删除此条
                        {
                            listItems.RemoveAt(i);
                        }
                    }
                    else
                    {
                        WarehouseStock ws = (WarehouseStock)htwh[stock.ItemCode];
                        ws.OpenQty = ws.OpenQty + add_sub * stock.OpenQty;
                        this.UpdateWarehouseStockQty(ws, add_sub * stock.OpenQty);
                        //this.UpdateWarehouseStock(ws);
                        stock.EAttribute1 = ws.OpenQty.ToString();
                    }
                }
                htadded = null;
                htwh = null;
                list = null;
            }

        }
        /// <summary>
        /// 上料时更新工单耗用量
        /// </summary>
        private void UpdateMOStockFromWIP(string moCode, ArrayList listItems)
        {
            string attributeName = "IssueQty";
            string operation = "add";
            for (int i = 0; i < listItems.Count; i++)
            {
                WarehouseStock stock = (WarehouseStock)listItems[i];
                this.UpdateMOStockInTrans(moCode, stock.ItemCode, attributeName, stock.OpenQty, operation);
            }
        }

        /// <summary>
        /// 减少工单耗用量
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="listItems"></param>
        private void SubMOStockFromWIP(string moCode, ArrayList listItems)
        {
            string attributeName = "IssueQty";
            string operation = "sub";
            for (int i = 0; i < listItems.Count; i++)
            {
                WarehouseStock stock = (WarehouseStock)listItems[i];
                this.UpdateMOStockInTrans(moCode, stock.ItemCode, attributeName, stock.OpenQty, operation);
            }
        }

        #endregion

        #region 脱离工单扣库存/下料扣库存
        /// <summary>
        /// 脱离工单扣库存
        /// </summary>
        /// <param name="runningCard">流程卡号</param>
        /// <param name="moCode">工单号</param>
        public void DropMaterialStock(string runningCard, string moCode, string opCode)
        {
            //Laws Lu,2005/12/20
            /*
             1. 读取OnWIPItem中的上料操作信息
             2. 读取对应仓库
             3. 判断上料类型
             4. 读取物料代码及数量
                4.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                4.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
             5. 扣库存
             6. 更新工单使用量
             7. 修改OnWIPItem.TransactionStatus
            */


            //1. 读取OnWIPItem中的上料操作信息(经过库存扣减的记录)
            object[] objs = this.DataProvider.CustomQuery(typeof(OnWIPItem), new SQLCondition(String.Format("select {0} from tblonwipitem where rcard in {1} and mocode='{2}' and TRANSSTATUS='{3}' and actiontype=" + (int)MaterialType.CollectMaterial
                , new object[]{
								 DomainObjectUtility.GetDomainObjectFieldsString(typeof(OnWIPItem))
								 ,runningCard
								 ,moCode
								 ,TransactionStatus.TransactionStatus_YES
							 })));


            if (objs == null)
                return;


            //3. 判断上料类型

            OnWIPItem wip = (OnWIPItem)objs[0];

            //2. 读取对应仓库
            Warehouse wh = this.GetWarehouseFromWIP(wip);
            if (wh == null)
            {
                return;
            }

            ArrayList listItems = new ArrayList();	//待扣物料

            listItems = GetKeypartsFromWIP(objs);
            foreach (object obj in objs)
            {
                OnWIPItem onWip = (OnWIPItem)obj;
                //				//4.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                //				if (onWip.MCardType == MCardType.MCardType_Keyparts)
                //				{
                //					listItems.Clear();
                //					listItems.AddRange(GetKeypartsFromWIP(objs).ToArray());
                //					break;
                //				}
                if (onWip.MCardType == MCardType.MCardType_INNO)	//集成上料
                {
                    //4.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
                    listItems.AddRange(this.GetINNOsFromWIP(onWip).ToArray());
                }
            }

            if (listItems.Count == 0)
                return;

            //5. 扣库存
            this.UpdateWarehouseStockFromList(listItems, wh, 1, wip.MaintainUser);

            //6. 更新工单使用量
            this.SubMOStockFromWIP(moCode, listItems);

            //7. 修改OnWIPItem.TransactionStatus
            string strSql = string.Format("update TBLONWIPITEM set TRANSSTATUS='{0}',ActionType={1},DropOP='{2}' where RCARD in {3} and MOCODE=$mocode and TRANSSTATUS=$transstatus and actiontype={4}"
                , TransactionStatus.TransactionStatus_YES
                , (int)Web.Helper.MaterialType.DropMaterial
                , opCode
                , runningCard
                , (int)Web.Helper.MaterialType.CollectMaterial);
            this.DataProvider.CustomExecute(new SQLParamCondition(strSql, new SQLParameter[]{
																								new SQLParameter("mocode", typeof(string), moCode),
																								new SQLParameter("transstatus", typeof(string), TransactionStatus.TransactionStatus_YES),
			}));


            this.ExecCacheSQL(); //执行缓存的更新库存和工单的ＳＱＬ　joe song　20050223
        }
        //TODO
        private void UpdateMaterialByInno(object obj, string userCode, string runningCard, string moCode, string opCode)
        {
            string strSqlInno = String.Empty;
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);


            if (obj is OnWIPItem)//脱离工单
            {

                OnWIPItem onWip = (OnWIPItem)obj;
                if (onWip.MCardType == MCardType.MCardType_INNO)
                {
                    string stWhere = String.Format(" where RCARD in {0} and MOCODE='{1}' and TRANSSTATUS='{2}' and ActionType={3}" +
                        (onWip.MCARD == String.Empty ? " and LOTNO is null " : (" and LOTNO = '" + onWip.MCARD + "'")) +
                        " and MITEMCODE = '" + onWip.MItemCode + "'", new object[]
						{
							runningCard
							,moCode
							,TransactionStatus.TransactionStatus_YES
							,(int)Web.Helper.MaterialType.CollectMaterial
						});

                    strSqlInno = "update TBLONWIPITEM set TRANSSTATUS= (case when qty > 1 then '" + TransactionStatus.TransactionStatus_YES + "' else '" + TransactionStatus.TransactionStatus_NO
                        + "' end),ActionType=" + (int)MaterialType.DropMaterial +
                        ",DropOP = '" + opCode + "'" +
                        ",DropUser='" + userCode + "'" +
                        ",DropDate=" + FormatHelper.TODateInt(dtNow) +
                        ",DropTime=" + FormatHelper.TOTimeInt(dtNow) +
                        ",qty = (case when qty > 1 then qty - " + onWip.Qty + " else 1 end)" +
                        stWhere +
                        " and (rcard,rcardseq,mocode) in (select rcard,rcardseq,mocode from (select rcard,rcardseq,mocode from TBLONWIPITEM "
                        + stWhere + " order by rcardseq desc) where rownum = 1) ";
                }
            }
            else if (obj is InnoObject)//下料
            {
                InnoObject onWip = (InnoObject)obj;
                if (onWip.MCardType == MCardType.MCardType_INNO)
                {
                    string stWhere = String.Format(" where RCARD in {0} and MOCODE='{1}' and TRANSSTATUS='{2}' and ActionType={3}" +
                        (onWip.MCard == String.Empty ? " and MCARD is null " : (" and MCARD = '" + onWip.MCard + "'")) +
                        " and MITEMCODE = '" + onWip.MItemCode + "'", new object[]
						{
							runningCard
							,moCode
							,TransactionStatus.TransactionStatus_YES
							,(int)Web.Helper.MaterialType.CollectMaterial
						});

                    strSqlInno = "update TBLONWIPITEM set TRANSSTATUS= (case when qty > 1 then '" + TransactionStatus.TransactionStatus_YES + "' else '" + TransactionStatus.TransactionStatus_NO
                        + "' end),ActionType=" + (int)MaterialType.DropMaterial +
                        ",DropOP = '" + opCode + "'" +
                        ",DropUser='" + userCode + "'" +
                        ",DropDate=" + FormatHelper.TODateInt(dtNow) +
                        ",DropTime=" + FormatHelper.TOTimeInt(dtNow) +
                        ",qty = (case when qty > 1 then qty - " + onWip.Qty + " else 1 end)" +
                        stWhere +
                        " and (rcard,rcardseq,mocode) in (select rcard,rcardseq,mocode from (select rcard,rcardseq,mocode from TBLONWIPITEM "
                        + stWhere + " order by rcardseq desc) where rownum = 1) ";
                }
            }
            else if (obj is TSItem)//如果是换料
            {
                TSItem onWip = (TSItem)obj;
                if (onWip.MSourceCardType == MCardType.MCardType_INNO)
                {
                    string stWhere = String.Format(" where RCARD in {0} and MOCODE='{1}' and TRANSSTATUS='{2}' and ActionType={3}" +
                        (onWip.MSourceCard == String.Empty ? " and MCARD is null " : (" and MCARD = '" + onWip.MSourceCard + "'")) +
                        " and MITEMCODE = '" + onWip.SourceItemCode + "'", new object[]
						{
							runningCard
							,moCode
							,TransactionStatus.TransactionStatus_YES
							,(int)Web.Helper.MaterialType.CollectMaterial
						});

                    strSqlInno = "update TBLONWIPITEM set TRANSSTATUS= (case when qty > 1 then '" + TransactionStatus.TransactionStatus_YES + "' else '" + TransactionStatus.TransactionStatus_NO
                        + "' end),ActionType=" + (int)MaterialType.DropMaterial +
                        ",DropOP = '" + opCode + "'" +
                        ",DropUser='" + userCode + "'" +
                        ",DropDate=" + FormatHelper.TODateInt(dtNow) +
                        ",DropTime=" + FormatHelper.TOTimeInt(dtNow) +
                        ",qty = (case when qty > 1 then qty - " + onWip.Qty + " else 1 end)" +
                        stWhere +
                        " and (rcard,rcardseq,mocode) in (select rcard,rcardseq,mocode from (select rcard,rcardseq,mocode from TBLONWIPITEM "
                        + stWhere + " order by rcardseq desc) where rownum = 1) ";
                }
            }

            if (strSqlInno != String.Empty)
            {
                this.DataProvider.CustomExecute(new SQLCondition(strSqlInno));
            }
        }

        /// <summary>
        /// 下料,归还库存
        /// </summary>
        /// <param name="objs">上料信息</param>
        /// <param name="runningCard">流程卡号</param>
        /// <param name="moCode">工单号码</param>
        /// <param name="userCode">用户</param>
        /// <param name="ssCode">产线代码</param>
        public void RemoveWarehouse(object[] objs, string runningCard, string moCode, string userCode, string ssCode, string opCode)
        {
            if (objs == null)
            {
                throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL");
            }


            //1. 读取对应仓库
            Warehouse wh = this.GetWarehouseFromMOSS(moCode, ssCode);
            if (wh == null)
            {
                return;
            }

            //2. 判断类型
            ArrayList listItems = new ArrayList();	//待扣物料	


            listItems = GetRemoveKeypartsFromWIP(objs);

            foreach (object obj in objs)
            {
                if (obj is OnWIPItem)
                {
                    OnWIPItem onWip = (OnWIPItem)obj;

                    //					//4.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                    //					if (onWip.MCardType == MCardType.MCardType_Keyparts)
                    //					{
                    //						listItems.Clear();
                    //						listItems.AddRange(GetKeypartsFromWIP(objs).ToArray());
                    //						break;
                    //					}
                    if (onWip.MCardType == MCardType.MCardType_INNO)	//集成上料
                    {
                        //4.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
                        listItems.Add(this.GeInnoByMItemCode(onWip.MItemCode, onWip.Qty));
                        //listItems.AddRange(this.GetINNOsFromWIP(onWip).ToArray());
                    }
                }
                else if (obj is InnoObject)
                {
                    InnoObject onWip = (InnoObject)obj;

                    //					//4.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                    //					if (onWip.MCardType == MCardType.MCardType_Keyparts)
                    //					{
                    //						listItems.Clear();
                    //						listItems.AddRange(GetKeypartsFromWIP(objs).ToArray());
                    //						break;
                    //					}
                    if (onWip.MCardType == MCardType.MCardType_INNO)	//集成上料
                    {
                        //4.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
                        listItems.Add(this.GeInnoByMItemCode(onWip.MItemCode, onWip.Qty));
                    }
                }
                else if (obj is TSItem)
                {
                    TSItem onWip = (TSItem)obj;

                    //					//4.1. 如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                    //					if (onWip.MCardType == MCardType.MCardType_Keyparts)
                    //					{
                    //						listItems.Clear();
                    //						listItems.AddRange(GetKeypartsFromWIP(objs).ToArray());
                    //						break;
                    //					}
                    if (onWip.MSourceCardType == MCardType.MCardType_INNO)	//集成上料
                    {
                        //4.2. 如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
                        listItems.Add(this.GeInnoByMItemCode(onWip.SourceItemCode, onWip.Qty));
                    }
                }

            }
            if (listItems.Count == 0)
                return;

            //4. 加库存
            this.UpdateWarehouseStockFromList(listItems, wh, 1, /*wip.MaintainUser*/userCode);

            //5. 减少工单使用量
            this.SubMOStockFromWIP(moCode, listItems);

            //TODO:需要实现批号为空时，更新最后一笔OnWipItem记录
            //6. 修改OnWIPItem.TransactionStatus
            string strSqlKeyPart = String.Empty;


            ArrayList keyParts = new ArrayList();
            Hashtable lots = new Hashtable();

            #region 获取物料号（集成上料号）和KeyParts序列号
            foreach (object obj in objs)
            {
                if (obj is OnWIPItem)
                {
                    OnWIPItem onWip = (OnWIPItem)obj;

                    if (onWip.MCardType == MCardType.MCardType_Keyparts)
                    {
                        if (!keyParts.Contains(onWip.MCARD))
                        {
                            keyParts.Add("'" + onWip.MCARD + "'");
                        }
                    }

                }
                else if (obj is InnoObject)
                {
                    InnoObject onWip = (InnoObject)obj;

                    if (onWip.MCardType == MCardType.MCardType_Keyparts)
                    {
                        if (!keyParts.Contains(onWip.MCard))
                        {
                            keyParts.Add("'" + onWip.MCard + "'");
                        }
                    }

                }
                else if (obj is TSItem)
                {
                    TSItem onWip = (TSItem)obj;

                    if (onWip.MSourceCardType == MCardType.MCardType_Keyparts)
                    {
                        if (!keyParts.Contains(onWip.MSourceCard))
                        {
                            keyParts.Add("'" + onWip.MSourceCard + "'");
                        }
                    }

                }

                UpdateMaterialByInno(obj, userCode, runningCard, moCode, opCode);
            }
            #endregion

            if (keyParts.Count > 0)
            {
                //2006/11/17,Laws Lu add get DateTime from db Server
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                strSqlKeyPart = string.Format("update TBLONWIPITEM set TRANSSTATUS='{0}',ActionType=" + (int)MaterialType.DropMaterial +
                    ",DropOP = '" + opCode + "'" +
                    ",DropUser='" + userCode + "'" +
                    ",DropDate=" + FormatHelper.TODateInt(dtNow) +
                    ",DropTime=" + FormatHelper.TOTimeInt(dtNow) +
                    " where RCARD in {1} and MOCODE=$mocode and TRANSSTATUS=$transstatus and ActionType=$actiontype" +
                    " and MCARD in (" + String.Join(",", (string[])keyParts.ToArray(typeof(string))) + ")"
                    , TransactionStatus.TransactionStatus_YES
                    , runningCard);


                this.DataProvider.CustomExecute(new SQLParamCondition(strSqlKeyPart, new SQLParameter[]{
																										   new SQLParameter("mocode", typeof(string), moCode),
																										   new SQLParameter("transstatus", typeof(string), TransactionStatus.TransactionStatus_YES),
																										   new SQLParameter("actiontype", typeof(int),(int)Web.Helper.MaterialType.CollectMaterial)
																									   }));
            }

            this.ExecCacheSQL(); //执行缓存的更新库存和工单的ＳＱＬ　joe song　20050223
        }


        #endregion

        #region TS (TBD: 换料时换下的料没有记录)
        /// <summary>
        /// 维修换料时对库存的影响
        /// </summary>
        /// <param name="tsid">tsid</param>
        public void TSItemWarehouseStock(string tsid)
        {
            /*
             * 维修换料时，对换上的物料做库存减
             1. 读TSItem表获取所有换上的物料
             2. 查询对应仓库
             3. 依次修改所有物料的库存
            */

            /*	Removed by Icyer 2005/0823		换料暂不处理
            //1. 读TSItem表获取所有换上的物料
            BenQGuru.eMES.TS.TSFacade tsf = new BenQGuru.eMES.TS.TSFacade(this.DataProvider);
            object[] objs = tsf.ExtraQueryTSItem(tsid);
            if (objs == null)
                return;
			
            //2. 查询对应仓库 (维修站对应的仓库)
            TSItem item0 = (TSItem)objs[0];
            Warehouse wh = this.GetWarehouseFromMoCodeRes(item0.MOCode, item0.RepairResourceCode);
            if (wh == null)
                return;
			
            //3. 依次修改所有物料的库存
            ArrayList listItems = new ArrayList();
            for (int i = 0; i < objs.Length; i++)
            {
                TSItem tsitem = (TSItem)objs[i];
                WarehouseStock stock = new WarehouseStock();
                stock.ItemCode = tsitem.MItemCode;
                stock.OpenQty = tsitem.Qty;
                listItems.Add(stock);
            }
            this.UpdateWarehouseStockFromList(listItems, wh, item0.MaintainUser);
            */
        }
        /// <summary>
        /// 维修拆解时对库存的影响
        /// </summary>
        /// <param name="tsid">tsid</param>
        public void TSSplitItemWarehouseStock(string tsid)
        {
            // Return by Icyer 2005/0929
            // 取消物料模块
            //Laws Lu,2005/12/15,取消	允许根据配置文件来决定是否进行物料操作
            //			return;

            /*
             * 维修拆解时，对RunningCard上的所有上料过的物料做库存加
             1. 读TS表获取RunningCard信息
             2. 查询对应仓库 (资源代码要到TSSplitItem中查询)
             3. 根据RunningCard读取OnWIPItem的所有上料信息
             4. 根据OnWIPItem类型读取所有物料及数量
             5. 依次修改所有物料的库存
             6. 增加交易单据
            */

            //1. 读TS表获取RunningCard信息
            BenQGuru.eMES.Domain.TS.TS ts = (BenQGuru.eMES.Domain.TS.TS)this.DataProvider.CustomSearch(typeof(BenQGuru.eMES.Domain.TS.TS), new object[] { tsid }); ;
            if (ts == null)
                return;
            string strMOCode = ts.MOCode;
            string strRCard = ts.RunningCard;
            string strRCardSeq = ts.RunningCardSequence.ToString();

            //2. 查询对应仓库 (资源代码要到TSSplitItem中查询)
            object[] objsst = this.DataProvider.CustomSearch(typeof(TSSplitItem), new string[] { "TSId" }, new object[] { tsid });
            if (objsst == null)
                return;
            Warehouse wh = this.GetWarehouseFromMoCodeRes(strMOCode, ((TSSplitItem)objsst[0]).RepairResourceCode);
            if (wh == null)
                return;

            //3. 根据RunningCard读取OnWIPItem的所有上料信息
            //条件中不能包含RunningCardSequence，RunningCardSequence会递增改变
            object[] objswip = this.DataProvider.CustomSearch(typeof(OnWIPItem), new string[] { "RunningCard", "MOCode", "TransactionStatus" }, new object[] { strRCard, strMOCode, TransactionStatus.TransactionStatus_YES });
            if (objswip == null)
                return;

            //4. 根据OnWIPItem类型读取所有物料及数量
            ArrayList listItems = new ArrayList();
            foreach (OnWIPItem wip in objswip)
            {
                ArrayList listtmp = new ArrayList();
                if (wip.MCardType == MCardType.MCardType_Keyparts)	//KeyParts上料
                {
                    //如果是KeyParts上料，则读取OnWIPItem的MItemCode(物料代码)及Qty(数量)
                    listtmp = this.GetKeypartsFromWIP(new object[] { wip });
                }
                else if (wip.MCardType == MCardType.MCardType_INNO)	//集成上料
                {
                    //如果是集成上料，则读取MINNO表(集成上料表)中的具体物料代码及数量
                    listtmp = this.GetINNOsFromWIP(wip);
                }
                listItems.AddRange(listtmp);
            }
            if (listItems.Count == 0)
                return;

            //5. 依次修改所有物料的库存
            this.UpdateWarehouseStockFromList(listItems, wh, 1, ts.MaintainUser);

            //6. 增加交易单据
            this.AddWarehouseTicketFromSplit(listItems, wh, strMOCode, ts.MaintainUser);

            this.ExecCacheSQL(); //执行缓存的更新库存和工单的ＳＱＬ　joe song　20050223
        }
        /// <summary>
        /// 拆解更新库存后，加入交易单据
        /// </summary>
        private void AddWarehouseTicketFromSplit(ArrayList listItems, Warehouse wh, string moCode, string userId)
        {
            //TODO: 查询拆解单代码
            string strTransCode = GetTransactionTypeByName("TSSplitItem");
            string ticketNo = this.AddWarehouseTicket(wh.FactoryCode, /* wh.SegmentCode,*/ wh.WarehouseCode, strTransCode, userId);
            int iSeq = 1;
            Hashtable htitems = new Hashtable();
            object[] objsitem = this.GetAllWarehouseItem();
            for (int i = 0; i < objsitem.Length; i++)
            {
                htitems.Add(((WarehouseItem)objsitem[i]).ItemCode, ((WarehouseItem)objsitem[i]).ItemName);
            }
            objsitem = null;
            for (int i = 0; i < listItems.Count; i++)
            {
                WarehouseStock ws = (WarehouseStock)listItems[i];
                this.AddWarehouseTicketDetail(ticketNo, iSeq, moCode, ws.ItemCode, htitems[ws.ItemCode].ToString(), ws.OpenQty, decimal.Parse(ws.EAttribute1), userId);
                iSeq++;
            }
        }
        #endregion

        #region 出入庫類型維護

        public MaterialBusiness CreateNewMaterialBusiness()
        {
            return new MaterialBusiness();
        }

        public void AddMaterialBusiness(MaterialBusiness materialBusiness)
        {
            this._helper.AddDomainObject(materialBusiness);
        }

        public void UpdateMaterialBusiness(MaterialBusiness materialBusiness)
        {
            this._helper.UpdateDomainObject(materialBusiness);
        }

        public void DeleteMaterialBusiness(MaterialBusiness[] materialBusinessList)
        {
            this._helper.DeleteDomainObject(materialBusinessList);
        }

        public object[] QueryMaterialBusiness(string businessCode, string businessDesc, string businessType, int inclusive, int exclusive)
        {
            string sql = string.Empty;

            sql = string.Format("select {0} from tblmaterialbusiness where 1=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialBusiness)));

            if (businessCode != null && businessCode.Length > 0)
            {
                sql = string.Format("{0} AND BUSINESSCODE like '%{1}%'", sql, businessCode);
            }

            if (businessDesc != null && businessDesc.Length > 0)
            {
                sql = string.Format("{0} AND BUSINESSDESC like '%{1}%'", sql, businessDesc);
            }

            if (businessType != null && businessType.Length > 0)
            {
                sql = string.Format("{0} AND BUSINESSTYPE like '%{1}%'", sql, businessType);
            }

            return this.DataProvider.CustomQuery(typeof(MaterialBusiness), new PagerCondition(sql, "BUSINESSCODE", inclusive, exclusive));
        }

        public object[] QueryMaterialBusiness(string businessType)
        {
            string sql = string.Empty;
            sql = string.Format("select {0} from tblmaterialbusiness where 1=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialBusiness)));
            if (businessType != null && businessType.Length > 0)
            {
                sql = string.Format("{0} AND BUSINESSTYPE = '{1}'", sql, businessType);
            }
            return this.DataProvider.CustomQuery(typeof(MaterialBusiness), new SQLCondition(sql));
        }

        public object GetMaterialBusiness(string businessCode)
        {
            return this.DataProvider.CustomSearch(typeof(MaterialBusiness), new object[] { businessCode });
        }

        public int QueryMaterialBusinessCount(string businessCode, string businessDesc, string businessType)
        {
            string sql = string.Empty;

            sql = "select count(*) from tblmaterialbusiness where 1=1 ";

            if (businessCode != null && businessCode.Length > 0)
            {
                sql = string.Format("{0} AND BUSINESSCODE like '%{1}%'", sql, businessCode);
            }

            if (businessDesc != null && businessDesc.Length > 0)
            {
                sql = string.Format("{0} AND BUSINESSDESC like '%{1}%'", sql, businessDesc);
            }

            if (businessType != null && businessType.Length > 0)
            {
                sql = string.Format("{0} AND BUSINESSTYPE like '%{1}%'", sql, businessType);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region SERIALBOOK
        public string GetMaxSerial(string Prefix)
        {
            string sql = string.Format("SELECT  MAXSERIAL FROM TBLSERIALBOOK WHERE SNPREFIX='{0}' ", Prefix);
            object[] result = this.DataProvider.CustomQuery(typeof(SERIALBOOK), new SQLCondition(sql));
            if (result != null && result.Length > 0)
                return ((SERIALBOOK)result[0]).MaxSerial;
            else
                return string.Empty;
        }

        public void AddSerialBook(SERIALBOOK serialBook)
        {
            this.DataProvider.Insert(serialBook);
        }

        public void UpdateSerialBook(SERIALBOOK serialBook)
        {
            this.DataProvider.Update(serialBook);
        }

        public object GetSerialBook(string prefix)
        {
            string sql = string.Format(@"SELECT  * FROM TBLSERIALBOOK WHERE SNPREFIX='{0}' ", prefix);
            object[] result = this.DataProvider.CustomQuery(typeof(SERIALBOOK), new SQLCondition(sql));
            if (result != null && result.Length > 0)
                return result[0];
            else
                return null;
        }
        #endregion


        #region Customer
        /// <summary>
        /// 厂商列表
        /// </summary>
        public Customer CreateNewCustomer()
        {
            return new Customer();
        }

        public void AddCustomer(Customer customer)
        {
            this._helper.AddDomainObject(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            this._helper.UpdateDomainObject(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            if (IsExist(typeof(Customer), new string[] { "CustomerCode" }, new object[] { customer.CustomerCode }))
            {
                throw new Exception(string.Format("$Delete_Customer_Error_UseInCustomer [$CustomerCode={0}]", customer.CustomerCode));
            }
            this._helper.DeleteDomainObject(customer);
        }

        public void DeleteCustomer(Customer[] customer)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < customer.Length; i++)
                {
                    this.DeleteCustomer(customer[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message);
            }
        }

        public object GetCustomer(string customerCode)
        {
            return this.DataProvider.CustomSearch(typeof(Customer), new object[] { customerCode });
        }
        /// <summary>
        /// 条件查询客户--Add by Amy @20160118
        /// </summary>
        /// <param name="customerCode">客户代码</param>
        /// <param name="customerName">客户名称</param>
        /// <returns></returns>
        public object[] GetCustomer(string customerCode, string customerName, int inclusive, int exclusive)
        {
            string sql = "select {0} from TBLCUSTOMER where 1=1 ";
            if (!string.IsNullOrEmpty(customerCode))
                sql += " and customercode like '{1}%'";
            if (!string.IsNullOrEmpty(customerName))
                sql += " and customerName like '%{2}%'";
            sql += " ORDER BY MDATE DESC,MTIME DESC";
            return this.DataProvider.CustomQuery(typeof(Customer), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Customer)), customerCode, customerName), inclusive, exclusive));
        }
        public int GetCustomerCount(string customerCode, string customerName)
        {
            string sql = "select  count(*) from TBLCUSTOMER where 1=1 ";
            if (!string.IsNullOrEmpty(customerCode))
                sql += " and customercode like '{0}%'";
            if (!string.IsNullOrEmpty(customerName))
                sql += " and customerName like '%{1}%'";
            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, customerCode, customerName)));
        }
        #endregion
        #region Vendor
        /// <summary>
        /// 条件查询客户--Add by Amy @20160118
        /// </summary>
        /// <param name="customerCode">厂商代码</param>
        /// <param name="customerName">厂商名称</param>
        /// <returns></returns>
        public object[] GetVendor(string vendorCode, string vendorName, int inclusive, int exclusive)
        {
            string sql = "select {0} from TBLVendor where 1=1 ";
            if (!string.IsNullOrEmpty(vendorCode))
                sql += " and vendorCode like '{1}%'";
            if (!string.IsNullOrEmpty(vendorName))
                sql += " and vendorName like '%{2}%'";
            sql += " ORDER BY MDATE DESC,MTIME DESC";
            return this.DataProvider.CustomQuery(typeof(Vendor), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Vendor)), vendorCode, vendorName), inclusive, exclusive));
        }
        public int GetVendorCount(string vendorCode, string vendorName)
        {
            string sql = "select  count(*) from TBLVendor where 1=1 ";
            if (!string.IsNullOrEmpty(vendorCode))
                sql += " and vendorCode like '{0}%'";
            if (!string.IsNullOrEmpty(vendorName))
                sql += " and vendorName like '%{1}%'";
            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, vendorCode, vendorName)));
        }
        #endregion


        #region   Amy Add
        /// <summary>
        /// SAP关账周期
        /// </summary>
        public Sapcloseperiod CreateNewSapcloseperiod()
        {
            return new Sapcloseperiod();
        }

        public void AddSapcloseperiod(Sapcloseperiod Sapclosep)
        {
            this._helper.AddDomainObject(Sapclosep);
        }

        public void UpdateSapcloseperiod(Sapcloseperiod Sapclosep)
        {
            this._helper.UpdateDomainObject(Sapclosep);
        }

        public void DeleteSapcloseperiod(Sapcloseperiod Sapclosep)
        {
            this._helper.DeleteDomainObject(Sapclosep);
        }

        public void DeleteSapcloseperiod(Sapcloseperiod[] Sapclosep)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < Sapclosep.Length; i++)
                {
                    this.DeleteSapcloseperiod(Sapclosep[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), ex.Message);
            }
        }

        public object GetSapcloseperiod(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Sapcloseperiod), new object[] { Serial });
        }

        public int GetRecordCount(int dateFrom, int timeFrom, int dateEnd, int timeEnd)
        {
            string datetimeFrom = dateFrom.ToString() + timeFrom.ToString().PadLeft(6, '0');
            string datetimeEnd = dateEnd.ToString() + timeEnd.ToString().PadLeft(6, '0');
            string sql = "select COUNT(*) from TBLSAPCLOSEPERIOD WHERE 1=1";
            sql += " AND (( '" + datetimeFrom + "'  BETWEEN STARTDATE*1000000+STARTTIME AND ENDDATE*1000000+ENDTIME) OR ('" + datetimeEnd + "' BETWEEN STARTDATE*1000000+STARTTIME AND ENDDATE*1000000+ENDTIME))  ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetRecordCount(int dateFrom, int timeFrom)
        {
            string datetimeFrom = dateFrom.ToString() + timeFrom.ToString().PadLeft(6, '0');
            string sql = "select COUNT(*) from TBLSAPCLOSEPERIOD WHERE 1=1";
            sql += " AND ( '" + datetimeFrom + "'  BETWEEN STARTDATE*1000000+STARTTIME AND ENDDATE*1000000+ENDTIME)    ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryRecordDetail(int dateFrom, int dateEnd, int inclusive, int exclusive)
        {

            string sql = "select {0} from TBLSAPCLOSEPERIOD WHERE 1=1";
            if (dateFrom > 0)
                sql += " AND STARTDATE >=" + dateFrom;
            if (dateEnd > 0)
                sql += " AND ENDDATE <=" + dateEnd;
            sql += "  order by mdate desc,mtime desc";
            return this.DataProvider.CustomQuery(typeof(Sapcloseperiod), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Sapcloseperiod))), inclusive, exclusive));
        }
        public int QueryRecordDetailCOUNT(int dateFrom, int dateEnd)
        {

            string sql = "select COUNT(*) from TBLSAPCLOSEPERIOD WHERE 1=1";
            if (dateFrom > 0)
                sql += " AND STARTDATE >=" + dateFrom;
            if (dateEnd > 0)
                sql += " AND ENDDATE <=" + dateFrom;
            sql += "  order by mdate desc,mtime desc";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion


        #region private method
        private bool IsExist(System.Type type, string[] attributes, object[] attributeValue)
        {
            BenQGuru.eMES.Common.Domain.TableMapAttribute tableMap =
                BenQGuru.eMES.Common.Domain.DomainObjectUtility.GetTableMapAttribute(type);
            string strTableName = tableMap.TableName;
            Hashtable hs = BenQGuru.eMES.Common.Domain.DomainObjectUtility.GetAttributeMemberInfos(type);
            string strSql = string.Format("select count(*) from {0} where 1=1 ", strTableName);
            SQLParameter[] sqlParam = new SQLParameter[attributes.Length];
            for (int i = 0; i < attributes.Length; i++)
            {
                foreach (FieldMapAttribute fma in hs.Keys)
                {
                    if (((System.Reflection.MemberInfo)hs[fma]).Name == attributes[i])
                    {
                        sqlParam[i] = new SQLParameter(fma.FieldName, typeof(string), attributeValue[i]);
                        strSql += string.Format(" and {0}=${0} ", fma.FieldName);
                        break;
                    }
                }
            }
            int iret = this.DataProvider.GetCount(new SQLParamCondition(strSql, sqlParam));
            return (iret > 0);
        }
        #endregion

        /// <summary>
        /// ** 功能描述:	查询WarehouseTicketDetail的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <param name="ticketNo">TicketNo，模糊查询</param>
        /// <returns> WarehouseTicketDetail的总记录数</returns>
        public int QueryWarehouseTicketDetailCount2(string sequence, string ticketNo)
        {
            string sql = string.Format(
                @"select a.tktno,a.seq,a.itemcode,a.itemnane,a.qty,a.actqty,a.mocode,a.transstatus,a.transuser, " +
                "			a.transactiondate,a.transactiontime,a.ticketuser,a.ticketdate,a.tickettime,a.muser, " +
                "			a.mdate,a.mtime,a.eattribute1,a.frmwhqty,a.towhqty,sum(b.obitemqty) as singleqty " +
                "		from TBLWHTKTDETAIL a, tblopbomdetail b " +
                "		where 1 = 1 " +
                "		and a.SEQ like '{0}%' " +
                "		and a.TKTNO = '{1}' " +
                "		and a.itemcode = b.obitemcode " +
                "		and b.actiontype = '{2}' 	 " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                "		and b.itemcode = (select itemcode from tblmo where mocode = a.mocode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") " +
                "		and b.obcode in (select routecode from tblmo2route where mocode = a.mocode) " +
                "		and b.opcode in " +
                "					(select opcode " +
                "						from tblitemroute2op " +
                "						where itemcode = " +
                "							(select itemcode from tblmo where mocode = a.mocode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                "						and (substr(opcontrol, {3}, 1) = '1'))	 " +
                "		group by a.tktno,a.seq,	a.itemcode,	a.itemnane,a.qty,a.actqty,a.mocode, " +
                "				a.transstatus,a.transuser,a.transactiondate,a.transactiontime,a.ticketuser, " +
                "				a.ticketdate,a.tickettime,a.muser,a.mdate,a.mtime,a.eattribute1, " +
                "				a.frmwhqty,a.towhqty", sequence, ticketNo, "0", ((int)BenQGuru.eMES.BaseSetting.OperationList.ComponentLoading + 1));
            sql = string.Format("select count(*) from ({0})", sql);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// ** 功能描述:	分页查询WarehouseTicketDetail
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-7-28 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <param name="ticketNo">TicketNo，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> WarehouseTicketDetail数组</returns>
        public object[] QueryWarehouseTicketDetail2(string sequence, string ticketNo, int inclusive, int exclusive)
        {
            string sql = string.Format(
       @"select a.tktno,a.seq,a.itemcode,a.itemnane,a.qty,a.actqty,a.mocode,a.transstatus,a.transuser,a.transactiondate, " +
       "a.transactiontime,a.ticketuser,a.ticketdate,a.tickettime,a.muser,a.mdate,a.mtime,a.eattribute1,a.frmwhqty,a.towhqty, " +
       " sum(b.obitemqty) as singleqty " +
       " from TBLWHTKTDETAIL a, tblopbomdetail b " +
       " where 1 = 1 " +
       " and a.SEQ like '{0}%' " +
       " and a.TKTNO = '{1}' " +
       " and a.itemcode = b.obitemcode " +
       " and b.actiontype = '{2}' 	 " +
       GlobalVariables.CurrentOrganizations.GetSQLCondition() +
       " and b.itemcode = (select itemcode from tblmo where mocode = a.mocode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") " +
       " and b.obcode in (select routecode from tblmo2route where mocode = a.mocode) " +
       " and b.opcode in " +
       "        (select opcode " +
       "           from tblitemroute2op " +
       "          where itemcode = " +
       "                (select itemcode from tblmo where mocode = a.mocode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") " +
       GlobalVariables.CurrentOrganizations.GetSQLCondition() +
       "            and (substr(opcontrol, {3}, 1) = '1'))	 " +
       " group by a.tktno,a.seq, a.itemcode,a.itemnane,a.qty, a.actqty,a.mocode,a.transstatus,a.transuser,a.transactiondate, " +
       "   a.transactiontime,a.ticketuser,a.ticketdate,a.tickettime,a.muser,a.mdate,a.mtime,a.eattribute1,a.frmwhqty,a.towhqty"
       , sequence, ticketNo, "0", ((int)BenQGuru.eMES.BaseSetting.OperationList.ComponentLoading + 1));

            return this.DataProvider.CustomQuery(typeof(WarehouseTicketDetail2), new PagerCondition(sql, "TKTNO,SEQ", inclusive, exclusive));
        }

        /// <summary>
        /// 获得带出该库房该物料在上次库房盘点调整后该物料的离散数量
        /// </summary>
        /// <param name="faccode"></param>
        /// <param name="whcode"></param>
        /// <param name="whitemcode"></param>
        /// <returns></returns>
        public string GetLastScatterQty(string faccode, string whcode, string whitemcode)
        {
            string sql = string.Format(@"
				select PHQTY - lineqty as openqty
				from tblwhcylcedetail
				where faccode = '{0}'
				and whcode = '{1}'
				and itemcode = '{2}'
				and cyclecode = (select max(cyclecode)
									from tblwhcylcedetail
									where faccode = '{0}'
									and whcode = '{1}'
									and itemcode = '{2}')", faccode, whcode, whitemcode);

            DataTable dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();

            if (dt.Rows.Count > 0)
            {
                decimal qty = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
                return qty.ToString("##.##");
            }

            return string.Empty;
        }

        public string GetOpenQty(string faccode, string whcode, string whitemcode)
        {
            /* 虚拆数量 */
            string sql = string.Format(
                @"select sum(qty) as qty " +
                "	from tblonwipitem " +
                "	where   1 = 1 " +
                "		AND ACTIONTYPE = 0 " +
                "		AND TRANSSTATUS = 'YES' " +
                "		AND mitemcode = '{0}' " +
                "		and mocode in " +
                "		(select mocode " +
                "			from tblsimulation " +
                "			where EXISTS " +
                "			(SELECT mocode " +
                "					FROM tblmo " +
                "					WHERE tblmo.mocode = tblsimulation.mocode " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                "					AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                "			AND EXISTS " +
                "			(SELECT rescode " +
                "					FROM tblres " +
                "					WHERE tblres.rescode = tblsimulation.rescode " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                "					AND EXISTS " +
                "					(SELECT sscode " +
                "							FROM tblwh2sscode " +
                "							WHERE tblwh2sscode.sscode = tblres.sscode " +
                "							AND whcode = '{1}')))", whitemcode, whcode);
            DataTable dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();

            /* 离散数量 */
            string sql2 = string.Format(
                @"select openqty from tblwhstock where faccode ='{0}' and itemcode ='{1}' and whcode='{2}' ", faccode, whitemcode, whcode);
            DataTable dt2 = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql2).Tables[0].Copy();


            if (dt.Rows.Count > 0 || dt2.Rows.Count > 0)
            {
                decimal qty = ((!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0)
                    + ((!(dt2.Rows[0][0] is System.DBNull) && dt2.Rows[0][0] != null) ? Convert.ToDecimal(dt2.Rows[0][0]) : 0);
                return qty.ToString("##.##");
            }
            return string.Empty;
        }

        public object[] QueryWarehouseCountDetail(string faccode, string whcode, string whitemcode, int inclusive, int exclusive)
        {
            /*
            string sql=
                string.Format(
                @"select distinct mocode
                    from tblsimulation
                    where EXISTS
                    (SELECT mocode
                            FROM tblmo
                            WHERE tblmo.mocode = tblsimulation.mocode
                            AND mostatus IN ('mostatus_pending', 'mostatus_open'))
                    AND EXISTS
                    (SELECT rescode
                            FROM tblres
                            WHERE tblres.rescode = tblsimulation.rescode
                            AND EXISTS
                            (SELECT sscode
                                    FROM tblwh2sscode
                                    WHERE tblwh2sscode.sscode = tblres.sscode
                                    AND whcode = '{0}'))",whcode);
            */
            string sql =
                string.Format(
                @"select mocode  " +
                "	from tblmo " +
                "	where mostatus IN ('mostatus_pending', 'mostatus_open') " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                "	and mocode in " +
                "		(select mocode " +
                "			from tblsimulation " +
                "			where EXISTS (SELECT rescode " +
                "					FROM tblres " +
                "					WHERE tblres.rescode = tblsimulation.rescode " +
                GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                "					AND EXISTS " +
                "					(SELECT sscode " +
                "							FROM tblwh2sscode " +
                "							WHERE tblwh2sscode.sscode = tblres.sscode " +
                "							AND whcode = '{0}')))", whcode);
            DataTable dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
            WarehouseCountDetail[] whcountDts = new WarehouseCountDetail[dt.Rows.Count + 1];

            #region 工单
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    whcountDts[i] = new WarehouseCountDetail();
                    whcountDts[i].MOCode = dt.Rows[i][0].ToString();
                }
                dt = null;

                for (int j = 0; j < whcountDts.Length - 1; j++)
                {
                    WarehouseCountDetail whcountDt = whcountDts[j];

                    /* 1.工单发料数量 
                     * 代表用“有工单管控”的单据发到本库房的本工单该物料的数量汇总*/
                    sql =
                        string.Format(
                        @"select sum(actqty)
							from tblwhtktdetail
							where itemcode = '{0}'
							and tktno in (select tktno
											from tblwhtkt
											where transtypecode = '{1}'
												and tofaccode = '{2}'
												and towhcode = '{3}'
												and mocode = '{4}')",
                        whitemcode, "541", faccode, whcode, whcountDt.MOCode);
                    dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        whcountDt.MOFQty = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
                    }
                    else
                    {
                        whcountDt.MOFQty = 0;
                    }
                    dt = null;
                    /* 2.上料数量 
                     * 在本库房关联的产线资源上生产该工单采集上料该物料的数量汇总
                     * 对于“上料数量”，应为工单采集上料的数量，不应该减去脱离工单数量和下料数量。*/
                    sql =
                        string.Format(
                        @"select sum(qty) as qty  " +
                        "	from tblonwipitem " +
                        "	where   1 = 1 " +
                        "		AND TRANSSTATUS = 'YES' " +
                        "		AND mitemcode = '{0}' " +
                        "		and (eattribute1 is null or eattribute1='') " +
                        "		and mocode in " +
                        "		(select mocode " +
                        "			from tblsimulation " +
                        "			where EXISTS " +
                        "			(SELECT mocode " +
                        "					FROM tblmo " +
                        "					WHERE mocode='{2}' and tblmo.mocode = tblsimulation.mocode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "					AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                        "			AND EXISTS " +
                        "			(SELECT rescode " +
                        "					FROM tblres " +
                        "					WHERE tblres.rescode = tblsimulation.rescode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "					AND EXISTS " +
                        "					(SELECT sscode " +
                        "							FROM tblwh2sscode " +
                        "							WHERE tblwh2sscode.sscode = tblres.sscode " +
                        "							AND whcode = '{1}')))",
                        whitemcode, whcode, whcountDt.MOCode);
                    dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        whcountDt.CollectQty = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
                    }
                    else
                    {
                        whcountDt.CollectQty = 0;
                    }
                    dt = null;
                    /* 3.下料数量 
                     * 在本库房关联的产线资源上生产该工单采集下料该物料的数量汇总
                     * 不包括 维修换下 */
                    sql =
                        string.Format(
                        @"select sum(qty) as qty " +
                        "	from tblonwipitem " +
                        "	where   1 = 1 " +
                        "		AND ACTIONTYPE = 1 " +
                        "		AND TRANSSTATUS = 'YES' " +
                        "		AND mitemcode = '{0}' " +
                        "		and (dropop <> 'TS' and dropop is not null)  " +
                        "		and (rcard,mocode) in " +
                        "		(select rcard,mocode " +
                        "			from tblsimulation " +
                        "			where EXISTS " +
                        "			(SELECT mocode " +
                        "					FROM tblmo " +
                        "					WHERE mocode='{2}' and tblmo.mocode = tblsimulation.mocode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "					AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                        "			AND EXISTS " +
                        "			(SELECT rescode " +
                        "					FROM tblres " +
                        "					WHERE tblres.rescode = tblsimulation.rescode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "					AND EXISTS " +
                        "					(SELECT sscode " +
                        "							FROM tblwh2sscode " +
                        "							WHERE tblwh2sscode.sscode = tblres.sscode " +
                        "							AND whcode = '{1}')) " +
                        "			and productstatus <> '{3}')",
                        whitemcode, whcode, whcountDt.MOCode, ProductStatus.OffMo);
                    dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        whcountDt.DropQty = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
                    }
                    else
                    {
                        whcountDt.DropQty = 0;
                    }
                    dt = null;
                    /* 4.退料数量 
                     * 代表用“有工单管控”的单据从本库房发出的本工单该物料的数量汇总*/
                    sql =
                        string.Format(
                        @"select sum(actqty)
							from tblwhtktdetail
							where itemcode = '{0}'
							and tktno in (select tktno
											from tblwhtkt
											where  transtypecode = '{1}'
												and faccode = '{2}'
												and whcode = '{3}'
												and mocode = '{4}')",
                        whitemcode, "262", faccode, whcode, whcountDt.MOCode);
                    dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        whcountDt.BackQty = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
                    }
                    else
                    {
                        whcountDt.BackQty = 0;
                    }
                    dt = null;
                    /* 5.移库入库数量 
                     * 0（因为移库单为不受工单管控的单据类型)*/
                    whcountDt.YWHQtyIn = 0;
                    /* 6.移库出库数量 
                     * 0（因为移库单为不受工单管控的单据类型)*/
                    whcountDt.YWHQtyOut = 0;
                    /* 7.维修换上数量 
                     * 在本库房关联的产线的维修工资源上维修该工单不良时更换上去的该物料的数量汇总*/
                    sql =
                        string.Format(
                        @"select sum(qty) as qty " +
                        "	from tbltsitem " +
                        "	where mitemcode = '{0}' " +
                        "	and EXISTS (SELECT mocode " +
                        "			FROM tblmo " +
                        "			WHERE mocode = '{2}' " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "			and tblmo.mocode = tbltsitem.mocode " +
                        "			AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                        "	AND EXISTS " +
                        "	(SELECT rescode " +
                        "			FROM tblres " +
                        "			WHERE tblres.rescode = tbltsitem.rrescode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "			AND EXISTS (SELECT sscode " +
                        "					FROM tblwh2sscode " +
                        "					WHERE tblwh2sscode.sscode = tblres.sscode " +
                        "					AND whcode = '{1}'))",
                        whitemcode, whcode, whcountDt.MOCode);
                    dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        whcountDt.TsQtyDown = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
                    }
                    else
                    {
                        whcountDt.TsQtyDown = 0;
                    }
                    dt = null;
                    /* 8.维修换下数量 
                     * 在本库房关联的产线的资源上维修该工单不良时更换下来的该物料的数量汇总*/
                    sql =
                        string.Format(
                        @"select sum(qty) as qty " +
                        "	from tbltsitem " +
                        "	where sitemcode = '{0}' " +
                        "	and EXISTS (SELECT mocode " +
                        "			FROM tblmo " +
                        "			WHERE mocode = '{2}' " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "			and tblmo.mocode = tbltsitem.mocode " +
                        "			AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                        "	AND EXISTS " +
                        "	(SELECT rescode " +
                        "			FROM tblres " +
                        "			WHERE tblres.rescode = tbltsitem.rrescode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "			AND EXISTS (SELECT sscode " +
                        "					FROM tblwh2sscode " +
                        "					WHERE tblwh2sscode.sscode = tblres.sscode " +
                        "					AND whcode = '{1}'))",
                        whitemcode, whcode, whcountDt.MOCode);
                    dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        whcountDt.TsQtyOn = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
                    }
                    else
                    {
                        whcountDt.TsQtyOn = 0;
                    }
                    dt = null;
                    /* 9.脱离工单数量 
                     * 在本库房关联的产线资源上脱离该工单产品上的该物料的数量汇总*/
                    sql =
                        string.Format(
                        @"select sum(qty) as qty " +
                        "	from tblonwipitem " +
                        "	where   1 = 1 " +
                        "		AND ACTIONTYPE = 1 " +
                        "		AND mitemcode = '{0}' " +
                        "		and (dropop <> 'TS' and dropop is not null)  " +
                        "		and (rcard,mocode) in " +
                        "		(select rcard,mocode " +
                        "			from tblsimulation " +
                        "			where EXISTS " +
                        "			(SELECT mocode " +
                        "					FROM tblmo " +
                        "					WHERE mocode='{2}' and tblmo.mocode = tblsimulation.mocode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "					AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                        "			AND EXISTS " +
                        "			(SELECT rescode " +
                        "					FROM tblres " +
                        "					WHERE tblres.rescode = tblsimulation.rescode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "					AND EXISTS " +
                        "					(SELECT sscode " +
                        "							FROM tblwh2sscode " +
                        "							WHERE tblwh2sscode.sscode = tblres.sscode " +
                        "							AND whcode = '{1}')) " +
                        "			and productstatus = '{3}')",
                        whitemcode, whcode, whcountDt.MOCode, ProductStatus.OffMo);
                    dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        whcountDt.OffMoQty = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
                    }
                    else
                    {
                        whcountDt.OffMoQty = 0;
                    }
                    dt = null;
                    /* 10.离散数量 
                     * 离散数量 = "工单发料数量" -“上料数量”+“下料数量”-“退料数量”+“移库入库数量”
                     *  -“移库出库数量”-“维修换上数量”+“维修换下数量”+“脱离工单数量”*/
                    whcountDt.WarehouseQty =
                        whcountDt.MOFQty - whcountDt.CollectQty + whcountDt.DropQty - whcountDt.BackQty
                        - whcountDt.TsQtyOn + whcountDt.TsQtyDown + whcountDt.OffMoQty;
                    /* 11.在制品虚拆数量 
                     * 在本库房关联的资源上该工单在制品所包含的该物料的数量汇总 */
                    sql =
                        string.Format(
                        @"select sum(qty) as qty " +
                        "		from tblonwipitem " +
                        "		where   1 = 1 " +
                        "			AND ACTIONTYPE = 0 " +
                        "			AND TRANSSTATUS = 'YES' " +
                        "			AND mitemcode = '{0}' " +
                        "			and mocode in " +
                        "			(select mocode " +
                        "				from tblsimulation " +
                        "				where EXISTS " +
                        "				(SELECT mocode " +
                        "						FROM tblmo " +
                        "						WHERE mocode='{1}' and tblmo.mocode = tblsimulation.mocode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "						AND mostatus IN ('mostatus_pending', 'mostatus_open')) " +
                        "				AND EXISTS " +
                        "				(SELECT rescode " +
                        "						FROM tblres " +
                        "						WHERE tblres.rescode = tblsimulation.rescode " +
                        GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                        "						AND EXISTS " +
                        "						(SELECT sscode " +
                        "								FROM tblwh2sscode " +
                        "								WHERE tblwh2sscode.sscode = tblres.sscode " +
                        "								AND whcode = '{2}'))) ",
                        whitemcode, whcountDt.MOCode, whcode);
                    dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        whcountDt.Lineqty = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
                    }
                    else
                    {
                        whcountDt.Lineqty = 0;
                    }
                    dt = null;
                    /* 12.账面数 
                     * 工单帐面数=“离散数量”+“在制品虚拆数量”*/
                    whcountDt.BillOpenQty = whcountDt.WarehouseQty + whcountDt.Lineqty;
                }

            }
            #endregion

            #region 移库单
            /* 入库 */
            whcountDts[whcountDts.Length - 1] = new WarehouseCountDetail();
            sql =
                string.Format(
                @"select sum(actqty) as qty
					from tblwhtktdetail
					where itemcode='{3}' and tktno in (select tktno
									from tblwhtkt
									where transtypecode = '{0}'
										and tofaccode = '{1}'
										and towhcode = '{2}')", "311", faccode, whcode, whitemcode);
            dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
            if (dt.Rows.Count > 0)
            {
                whcountDts[whcountDts.Length - 1].YWHQtyIn = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
            }
            else
            {
                whcountDts[whcountDts.Length - 1].YWHQtyIn = 0;
            }
            dt = null;
            /* 出库 */
            sql =
                string.Format(
                @"select sum(actqty) as qty
					from tblwhtktdetail
					where itemcode='{3}' and tktno in (select tktno
									from tblwhtkt
									where transtypecode = '{0}'
										and faccode = '{1}'
										and whcode = '{2}')", "311", faccode, whcode, whitemcode);
            dt = (this.DataProvider as SQLDomainDataProvider).PersistBroker.Query(sql).Tables[0].Copy();
            if (dt.Rows.Count > 0)
            {
                whcountDts[whcountDts.Length - 1].YWHQtyOut = (!(dt.Rows[0][0] is System.DBNull) && dt.Rows[0][0] != null) ? Convert.ToDecimal(dt.Rows[0][0]) : 0;
            }
            else
            {
                whcountDts[whcountDts.Length - 1].YWHQtyOut = 0;
            }
            dt = null;

            whcountDts[whcountDts.Length - 1].WarehouseQty = whcountDts[whcountDts.Length - 1].YWHQtyIn - whcountDts[whcountDts.Length - 1].YWHQtyOut;
            whcountDts[whcountDts.Length - 1].BillOpenQty = whcountDts[whcountDts.Length - 1].WarehouseQty;
            #endregion

            return whcountDts;
        }

        public int QueryWarehouseCountDetailCount(string faccode, string whcode, string whitemcode)
        {
            string sql = string.Format(@"select count( mocode ) " +
                            "	from tblmo " +
                            "	where mostatus IN ('mostatus_pending', 'mostatus_open') " +
                            "	and mocode in " +
                            "		(select mocode " +
                            "			from tblsimulation " +
                            "			where EXISTS (SELECT rescode " +
                            "					FROM tblres " +
                            "					WHERE tblres.rescode = tblsimulation.rescode " +
                            GlobalVariables.CurrentOrganizations.GetSQLCondition() +
                            "					AND EXISTS " +
                            "					(SELECT sscode " +
                            "							FROM tblwh2sscode " +
                            "							WHERE tblwh2sscode.sscode = tblres.sscode " +
                            "							AND whcode = '{0}'))) ", whcode);
            return this.DataProvider.GetCount(new SQLCondition(sql)) + 1;
        }
        #region 解IQC问题   Amy Add @20160330
        public object[] GetIqcdetailsn(string IqcNo, string SN, string CartonNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLASNIQCDETAILSN WHERE IQCNO='{1}' AND SN='{2}' AND CARTONNO='{3}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIqcDetailSN)), IqcNo, SN, CartonNo);
            return this.DataProvider.CustomQuery(typeof(AsnIqcDetailSN), new SQLCondition(sql));
        }
        public object[] GetInvoicesDetailByStnoAndStline(string Stno, string Stline)
        {
            string sql = string.Format("SELECT {0} FROM TBLINVOICESDETAIL WHERE INVNO IN (SELECT INVNO FROM TBLASNDETAILITEM WHERE STNO='{1}' AND STLINE='{2}') ORDER BY PLANDATE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), Stno, Stline);
            return this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
        }
        public int GetStockRecordCount(int dateFrom, int timeFrom, string MCode)
        {
            //string datetimeFrom = dateFrom.ToString() + timeFrom.ToString().PadLeft(6, '0');
            string sql = "select COUNT(*) from tblshiptostock WHERE 1=1";
            sql += " AND ( '" + dateFrom + "'  BETWEEN EFFDATE AND IVLDATE) AND ACTIVE='Y' AND ITEMCODE='" + MCode + "'  ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region Asndetail  Amy add
        /// <summary>
        /// TBLASNDETAIL
        /// </summary>
        public Asndetail CreateNewAsndetail()
        {
            return new Asndetail();
        }

        public void AddAsndetail(Asndetail asndetail)
        {
            this.DataProvider.Insert(asndetail);
        }

        public void DeleteAsndetail(Asndetail asndetail)
        {
            this.DataProvider.Delete(asndetail);
        }

        public void UpdateAsndetail(Asndetail asndetail)
        {
            this.DataProvider.Update(asndetail);
        }

        public object GetAsndetail(int Stline, string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(Asndetail), new object[] { Stline, Stno });
        }

        #endregion
        #region  AsnDetail Fuction  Add by Amy
        public object[] QueryASNDetailBystno(string stno, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql = "select d.*,m.mcontroltype from tblasndetail d left join tblmaterial m on d.dqmcode=m.dqmcode where 1=1 ";
            if (!string.IsNullOrEmpty(stno))
                sql += " and d.stno like '{0}%'";
            sql += " order by d.mdate,d.mtime desc";

            return this.DataProvider.CustomQuery(typeof(AsndetailEX), new PagerCondition(string.Format(sql, stno), inclusive, exclusive));
        }



        public AsndetailEX[] QueryASNDetailByAsnNoOrderByCartonno(string stno)
        {

            List<AsndetailEX> l = new List<AsndetailEX>();
            string sql = "select d.*,m.mcontroltype from tblasndetail d left join tblmaterial m on d.dqmcode=m.dqmcode where 1=1 ";
            if (!string.IsNullOrEmpty(stno))
                sql += " and d.stno like '{0}%'";
            sql += " order by d.cartonno";

            object[] os = this.DataProvider.CustomQuery(typeof(AsndetailEX), new SQLCondition(string.Format(sql, stno)));

            foreach (AsndetailEX d in os)
                l.Add(d);
            return l.ToArray();
        }

        public AsndetailEX[] QueryTrailDetailByAsnNoOrderByCartonno(string stno)
        {

            List<AsndetailEX> l = new List<AsndetailEX>();

            string sql = @"
SELECT T.* ,m.mcontroltype from
(
    select t1.*  from
    (select * from tblasndetail  where stno='" + stno + @"') t1,
    (
        select count(a.dqmcode) ct ,a.stno,a.dqmcode,min(b.minqty) minqty from
        (select * from tblasndetail where stno='" + stno + @"' ) a,
        (select min(qty) minqty, dqmcode from tblasndetail where stno='" + stno + @"' group by dqmcode ) b 
        where a.dqmcode=b.dqmcode  and a.qty=b.minqty group by a.dqmcode,a.stno
    ) t2 where t1.dqmcode=t2.dqmcode and t2.ct=1 and t2.minqty=t1.qty
) T LEFT JOIN tblmaterial M ON m.dqmcode=T.dqmcode";

            sql += " order by t.cartonno";

            object[] os = this.DataProvider.CustomQuery(typeof(AsndetailEX), new SQLCondition(string.Format(sql, stno)));
            if (os != null && os.Length > 0)
            {
                foreach (AsndetailEX d in os)
                    l.Add(d);
            }
            return l.ToArray();
        }

        public object[] QueryASNDetailBystno(string stno)
        {
            string sql = "select d.* from tblasndetail d where 1=1 ";
            if (!string.IsNullOrEmpty(stno))
                sql += " and d.stno = '{0}'";
            sql += " order by d.mdate desc,d.mtime desc";
            List<BenQGuru.eMES.Domain.Warehouse.AsndetailEX> ls = new List<BenQGuru.eMES.Domain.Warehouse.AsndetailEX>();
            object[] oo = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Warehouse.AsndetailEX), new SQLCondition(string.Format(sql, stno)));

            if (oo != null && oo.Length > 0)
            {
                foreach (BenQGuru.eMES.Domain.Warehouse.AsndetailEX a in oo)
                {
                    ls.Add(a);
                }
            }
            return ls.ToArray();

        }


        public int ASNDetailBystnoCount(string stno)
        {
            string sql = string.Empty;
            sql = "select count(*) from tblasndetail d left join tblmaterial m on d.dqmcode=m.dqmcode where 1=1 ";
            if (!string.IsNullOrEmpty(stno))
                sql += " and  d.stno like '{0}%'";
            sql += " order by d.mdate desc,d.mtime desc";
            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, stno)));

        }
        public object GetCustmCodeAndMCodefromInvoicesDetail(string DQMcode)
        {
            string sql = string.Format(@"select {0} from TBLInvoicesDetail where DQMcode='{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), DQMcode);
            object[] objs = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
                return objs[0];
            else
                return null;

        }
        public object GetNewLotNO(string lineCode, string shiftDay)
        {
            string Sql = "select {0} from (select {1} from TBLSerialbook where SNPREFIX='" + lineCode + shiftDay + "' ) ";
            //string Sql = "select {0} from (select {1} from TBLSerialbook where SNPREFIX='" + lineCode + shiftDay + "' and length(SNPREFIX)=7) ";
            object[] objs = this.DataProvider.CustomQuery(typeof(Serialbook)
               , new SQLCondition(String.Format(Sql
               , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Serialbook))
               , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Serialbook)))));
            if (objs != null)
            {
                return objs[0];
            }
            else
            {
                return null;
            }

        }
        public object[] GetSAPNOandLinebyMCODE(string invno, string mcode, string dqmcode)
        {
            string sql = "select {0} from tblinvoicesdetail where 1=1  and invno='{1}' and mcode='{2}' and dqmcode='{3}' and (INVLINESTATUS='Release' or INVLINESTATUS is null) order by  PLANDATE ";
            return this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), invno, mcode, dqmcode)));
        }

        public object GetNeedImportQtyOLD(string invno, int invnoLine, string stno)
        {
            string sql = "select nvl(sum(qty),0) as qty,nvl(sum(receiveqty),0) as receiveqty ,nvl(sum(QCPASSQTY),0) as QCPASSQTY  from tblasndetailitem  where  invno='" + invno + "' and invline='" + invnoLine + "' and stno<>'" + stno + "'";
            sql += " and stno in (select stno from tblasn where status<>'" + ASN_STATUS.ASN_Cancel + "' and sttype<>'SCTR')";
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(string.Format(sql, invno, invnoLine)));
            return objs[0];
        }
        public object GetNeedImportQtyNow(string invno, int invnoLine, string stno)
        {
            string sql = "select nvl(sum(qty),0) as qty,nvl(sum(receiveqty),0) as receiveqty ,nvl(sum(QCPASSQTY),0) as QCPASSQTY  from tblasndetailitem  where  invno='" + invno + "' and invline='" + invnoLine + "' and stno='" + stno + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(string.Format(sql, invno, invnoLine)));
            return objs[0];
        }
        public void DeleteSTNoDatabySTNO(string stno)
        {
            string sql = string.Format(@"delete from tblasndetail where stno='{0}'", stno);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
            sql = string.Format(@"delete from tblasndetailitem where stno='{0}'", stno);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
            sql = string.Format(@"delete from tblasndetailsn  where stno='{0}'", stno);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        public object[] QueryPODistriQTY(string stno, string stline)
        {
            string sql1 = string.Format("select invno,dqmcode from tblasn join tblasndetail on tblasn.stno=tblasndetail.stno and stline='{0}' and tblasn.stno='{1}'", stline, stno);
            DataSet ds1 = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql1);
            if (ds1 != null)
            {
                string sql2 = "select invline, dqmcode,planqty,invno from tblinvoicesdetail where dqmcode='" + ds1.Tables[0].Rows[0]["dqmcode"] + "' and invno='" + ds1.Tables[0].Rows[0]["invno"] + "'";
                object[] objs = this.DataProvider.CustomQuery(typeof(invoicedetailEX), new SQLCondition(sql2));
                if (objs != null)
                {
                    foreach (invoicedetailEX ee in objs)
                    {
                        string ss = "select nvl(sum(qty),0) as EQTY from tblasndetailitem where invno='" + ee.InvNo + "' and invline='" + ee.InvLine + "' and stno='" + stno + "'";
                        DataSet ds2 = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(ss);
                        ee.EQTY = int.Parse(ds2.Tables[0].Rows[0]["EQTY"].ToString());
                        ee.stno = stno;
                        object obj = GetNeedImportQtyOLD(ee.InvNo, ee.InvLine, ee.stno);
                        Asndetailitem subItemOld = obj as Asndetailitem;
                        ee.INQTY = subItemOld.Qty - (subItemOld.Qty - subItemOld.ReceiveQty) - (subItemOld.ReceiveQty - subItemOld.QcpassQty);
                    }
                }


                //string sql = "select l.invline,l.dqmcode,l.planqty,'' as inqty, l.invno ,sum(m.qty) as EQTY,m.stno from tblinvoicesdetail l left join tblasndetailitem m on l.invno=m.invno and l.invline=m.invline  where m.stno='" + stno + "' and  l.dqmcode='" + ds1.Tables[0].Rows[0]["dqmcode"] + "' group by  l.invline,l.dqmcode,l.planqty, l.invno ,m.stno";
                //sql += " union ";
                //sql += " select l.invline,l.dqmcode,l.planqty,'' as inqty, l.invno,0 AS EQTY,'' AS stno from tblinvoicesdetail l where  l.dqmcode='" + ds1.Tables[0].Rows[0]["dqmcode"] + "'  and  l.invno='" + ds1.Tables[0].Rows[0]["invno"] + "' AND INVLINE NOT IN (select l.invline from tblinvoicesdetail l left join tblasndetailitem m on l.invno=m.invno and l.invline=m.invline  where m.stno='" + stno + "' and  l.dqmcode='" + ds1.Tables[0].Rows[0]["dqmcode"] + "')";
                //object[] objs = this.DataProvider.CustomQuery(typeof(invoicedetailEX), new SQLCondition(sql));
                //if (objs != null)
                //{
                //    foreach (invoicedetailEX ee in objs)
                //    {
                //        object obj = GetNeedImportQtyOLD(ee.InvNo, ee.InvLine, ee.stno);
                //        Asndetailitem subItemOld = obj as Asndetailitem;
                //        ee.INQTY = subItemOld.Qty - (subItemOld.Qty - subItemOld.ReceiveQty) - (subItemOld.ReceiveQty - subItemOld.QcpassQty);
                //    }
                //}
                return objs;

            }
            return null;
        }
        public int QueryPODistriQTYcount(string stno, string stline)
        {
            int count = 0;
            string sql = string.Format("select invno,dqmcode from tblasn join tblasndetail on tblasn.stno=tblasndetail.stno and stline='{0}' and tblasn.stno='{1}'", stline, stno);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //count = ds.Tables[0].Rows.Count;
                sql = "select l.invline,l.dqmcode,l.planqty, l.invno ,sum(m.qty) as EQTY,m.stno from tblinvoicesdetail l left join tblasndetailitem m on l.invno=m.invno and l.invline=m.invline  where m.stno='" + stno + "' and  l.dqmcode='" + ds.Tables[0].Rows[0]["dqmcode"] + "' group by  l.invline,l.dqmcode,l.planqty, l.invno ,m.stno";
                sql += " union ";
                sql += " select l.invline,l.dqmcode,l.planqty, l.invno,0 AS EQTY,'' AS stno from tblinvoicesdetail l where  l.dqmcode='" + ds.Tables[0].Rows[0]["dqmcode"] + "'  and  l.invno='" + ds.Tables[0].Rows[0]["invno"] + "' AND INVLINE NOT IN (select l.invline from tblinvoicesdetail l left join tblasndetailitem m on l.invno=m.invno and l.invline=m.invline  where m.stno='" + stno + "' and  l.dqmcode='" + ds.Tables[0].Rows[0]["dqmcode"] + "')";
                DataSet ds1 = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
                if (ds1 != null)
                {
                    count += ds1.Tables[0].Rows.Count;
                }
                return count;
            }
            return 0;

        }
        public object GetSTLineInPOIntridution(string invno, string invline, string stno)
        {
            string sql = "select {0} from tblasndetailitem where invno='" + invno + "' and invline='" + invline + "' and stno='" + stno + "' order by stline desc";
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailitem)))));
            if (objs != null && objs.Length > 0)
                return objs[0];
            return null;
        }
        public object GetDNRLotNObyOrderNO(string InvNo, string DQMcode)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAILMATERIAL M WHERE M.PICKNO=(
                 SELECT PICKNO FROM TBLPICK K WHERE K.ORDERNO=(select L.ORDERNO from tblinvoicesdetail L WHERE L.INVNO='{1}' AND L.DQMCODE='{2}')
                 )
                 AND M.DQMCODE='{2}'  ORDER BY MDATE,MTIME", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterial)), InvNo, DQMcode);
            object[] objs = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            if (objs != null)
                return objs[0];
            return null;

        }
        public object GetSCTRLotNobyTrans(string DQMcode)
        {
            string sql = string.Format(@" SELECT {0} FROM TBLINVINOUTTRANS S WHERE S.TRANSTYPE='{1}' AND S.INVTYPE='OUT' AND S.DQMCODE='{3}' ORDER BY S.MDATE,S.MTIME", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvInOutTrans)), SAP_ImportType.SAP_SCTR, DQMcode);
            object[] objs = this.DataProvider.CustomQuery(typeof(InvInOutTrans), new SQLCondition(sql));
            if (objs != null)
                return objs[0];
            return null;
        }

        #endregion

        #region 入库指令  @20160413  Amy
        public bool GetNeedImportQtyOLD(string invno)
        {
            string sql1 = "SELECT COUNT(*) FROM TBLINVOICESDETAIL WHERE INVNO='" + invno + "' and (INVLINESTATUS='Release' or INVLINESTATUS is null) ";
            int count = this.DataProvider.GetCount(new SQLCondition(sql1));
            string sql = "select invno,invline, nvl(sum(qty),0) as qty,nvl(sum(receiveqty),0) as receiveqty ,nvl(sum(QCPASSQTY),0) as QCPASSQTY  from tblasndetailitem  where  invno='" + invno + "' ";
            sql += " and stno in (select stno from tblasn where status<>'" + ASN_STATUS.ASN_Cancel + "' )  group by invno,invline";
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(string.Format(sql, invno)));
            bool FF = false;
            if (objs != null)
            {
                if (objs.Length < count)
                {
                    FF = true;
                }
                InventoryFacade invFacade = new InventoryFacade(this.DataProvider);
                foreach (Asndetailitem item in objs)
                {
                    object obj = invFacade.GetInvoicesDetail(item.Invno, int.Parse(item.Invline));

                    if (obj != null)
                    {
                        InvoicesDetail invd = obj as InvoicesDetail;
                        decimal inQty = item.Qty - (item.Qty - item.ReceiveQty) - (item.ReceiveQty - item.QcpassQty);
                        if (invd.InvLineStatus != "Release" || string.IsNullOrEmpty(invd.InvLineStatus))
                        {
                            if (invd.PlanQty > inQty)
                            {
                                FF = true;
                                break;
                            }
                        }

                    }

                }
            }
            else
            {
                FF = true;
            }
            return FF;
        }
        #endregion

        #region Pickdetail
        /// <summary>
        /// TBLPICKDETAIL
        /// </summary>
        public PickDetail CreateNewPickdetail()
        {
            return new PickDetail();
        }

        public void AddPickdetail(PickDetail pickdetail)
        {
            this.DataProvider.Insert(pickdetail);
        }

        public void DeletePickdetail(PickDetail pickdetail)
        {
            this.DataProvider.Delete(pickdetail);
        }

        public void UpdatePickdetail(PickDetail pickdetail)
        {
            this.DataProvider.Update(pickdetail);
        }

        public object GetPickdetail(string Pickno, string Pickline)
        {
            return this.DataProvider.CustomSearch(typeof(PickDetail), new object[] { Pickno, Pickline });
        }

        public object[] QueryPickdetail(string pickno, string dqMaterialNO)
        {
            string sql = @"select a.* from TBLPICKDetail a where a.pickno = '" + pickno + "' and a.DQMCODE = '" + dqMaterialNO + "'";
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        }

        #endregion

        #region PickToPo
        /// <summary>
        /// TBLPickToPo
        /// </summary>
        public PickToPo CreateNewPickToPo()
        {
            return new PickToPo();
        }

        public void AddPickToPo(PickToPo PickToPo)
        {
            this.DataProvider.Insert(PickToPo);
        }

        public void DeletePickToPo(PickToPo PickToPo)
        {
            this.DataProvider.Delete(PickToPo);
        }

        public void DeletePickToPo(string pickno, string pickline)
        {
            string sql = string.Format("delete from tblPickToPo where pickno='{0}' and pickline='{1}' ", pickno, pickline);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public int SumPickDetailPQty(string pono, string poline, string mcode, string pickno)
        {
            string sql = string.Format(@"  SELECT nvl(sum(nvl(a.pqty,0)),0) qty FROM TBLPICKDETAIL a 
              left join tblpickToPo b on a.pickno=b.pickno  and a.pickline=b.pickline and b.mcode=a.mcode
              WHERE 1=1 and  b.pono='{0}' and b.poline='{1}' and a.mcode='{2}' and a.pickno='{3}'  ", pono, poline, mcode, pickno);
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public PickDetail SumPickDetailPQty(string pono, string mcode, string pickno)
        {
            string sql = string.Format(@"  SELECT nvl(sum(nvl(a.pqty,0)),0) qty,a.pickline FROM TBLPICKDETAIL a 
              left join tblpickToPo b on a.pickno=b.pickno  and a.pickline=b.pickline and b.mcode=a.mcode
              WHERE 1=1 and  b.pono='{0}'  and a.mcode='{1}' and a.pickno='{2}' group by a.pickline ", pono, mcode, pickno);

            object[] oo = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));

            if (oo != null && oo.Length > 0)
            {
                PickDetail p = new PickDetail();
                foreach (PickDetail o in oo)
                {
                    p.QTY += o.QTY;
                    p.PickLine = o.PickLine;
                }
                return p;

            } return null;
        }

        public InvoicesDetailEx[] GetWWpoForNotDN(string pickno)
        {
            string sql = String.Format(@" select b.pono INVNO,a.mcode,w.unit from tblpickdetail a
                                 inner  join tblpickToPo b 
                                 on a.pickno=b.pickno  and a.pickline=b.pickline
                                  inner  join tblwwpo w 
                                   on w.pono=b.pono  and w.poline=b.poline and w.mcode=a.mcode
                                  where 
                                 a.pickno='{0}' GROUP BY b.pono ,w.unit,a.mcode ", pickno);

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailEx), new SQLCondition(sql));

            List<InvoicesDetailEx> ds = new List<InvoicesDetailEx>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailEx d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();

        }

        public void UpdatePickToPo(PickToPo PickToPo)
        {
            this.DataProvider.Update(PickToPo);
        }

        public object GetPickToPo(string Pickno, string Pickline)
        {
            return this.DataProvider.CustomSearch(typeof(PickToPo), new object[] { Pickno, Pickline });
        }

        #endregion

        #region Serialbook
        /// <summary>
        /// TBLSERIALBOOK
        /// </summary>
        public Serialbook CreateNewSerialbook()
        {
            return new Serialbook();
        }

        public void AddSerialbook(Serialbook serialbook)
        {
            this.DataProvider.Insert(serialbook);
        }

        public void DeleteSerialbook(Serialbook serialbook)
        {
            this.DataProvider.Delete(serialbook);
        }

        public void UpdateSerialbook(Serialbook serialbook)
        {
            this.DataProvider.Update(serialbook);
        }

        public object GetSerialbook(string Snprefix)
        {
            return this.DataProvider.CustomSearch(typeof(Serialbook), new object[] { Snprefix });
        }

        #endregion

        #region Asn  Amy add
        /// <summary>
        /// TBLASN
        /// </summary>
        public Asn CreateNewAsn()
        {
            return new Asn();
        }

        public void AddAsn(Asn asn)
        {
            this.DataProvider.Insert(asn);
        }

        public void DeleteAsn(Asn asn)
        {
            this.DataProvider.Delete(asn);
        }

        public void UpdateAsn(Asn asn)
        {
            this.DataProvider.Update(asn);
        }

        public object GetAsn(string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(Asn), new object[] { Stno });
        }

        #endregion

        #region Pickdetailmaterialsn  Amy add
        /// <summary>
        /// TBLPICKDETAILMATERIALSN
        /// </summary>
        public Pickdetailmaterialsn CreateNewPickdetailmaterialsn()
        {
            return new Pickdetailmaterialsn();
        }

        public void AddPickdetailmaterialsn(Pickdetailmaterialsn pickdetailmaterialsn)
        {
            this.DataProvider.Insert(pickdetailmaterialsn);
        }

        public void DeletePickdetailmaterialsn(Pickdetailmaterialsn pickdetailmaterialsn)
        {
            this.DataProvider.Delete(pickdetailmaterialsn);
        }

        public void UpdatePickdetailmaterialsn(Pickdetailmaterialsn pickdetailmaterialsn)
        {
            this.DataProvider.Update(pickdetailmaterialsn);
        }

        public PickDetailEx[] QueryPickDetailForDN(string PickNo)
        {

            string sql = @"select b.*,a.invno,E.SAPCOUNT from TBLPICKDETAIL b inner join 
tblpick a on a.pickno=b.pickno LEFT JOIN 
(SELECT SUM(D.SAPCOUNT) SAPCOUNT,C.DNBATCHNO,d.DQMCODE  FROM TBLInvoices C,
(SELECT COUNT(*) SAPCOUNT,INVNO,DQMCODE FROM TBLInvoicesDetail GROUP BY DQMCODE,INVNO) D WHERE C.INVNO=D.INVNO GROUP BY DNBATCHNO,DQMCODE) E ON A.INVNO=E.DNBATCHNO AND B.DQMCODE=E.DQMCODE  
WHERE B.PICKNO='" + PickNo + "' AND B.STATUS<>'Cancel'";

            List<PickDetailEx> details = new List<PickDetailEx>();
            object[] objs = this.DataProvider.CustomQuery(typeof(PickDetailEx), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                foreach (PickDetailEx ex in objs)
                    details.Add(ex);
            }
            return details.ToArray();
        }



        public PickDetailEx[] QueryPickDetailForNotDN(string PickNo)
        {
            string sql = @"select b.*,a.invno,C.SAPCOUNT from TBLPICKDETAIL b inner join 
                           tblpick a on a.pickno=b.pickno LEFT JOIN 
                           (select COUNT(*) SAPCOUNT,invno,dqmcode FROM TBLInvoicesDetail GROUP BY INVNO,dqmcode) c on a.invno=c.invno and b.dqmcode=c.dqmcode  
                          WHERE  B.PICKNO='" + PickNo + "' AND B.STATUS<>'Cancel'";

            List<PickDetailEx> details = new List<PickDetailEx>();
            object[] objs = this.DataProvider.CustomQuery(typeof(PickDetailEx), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                foreach (PickDetailEx ex in objs)
                    details.Add(ex);
            }
            return details.ToArray();
        }
        public object[] QueryPickDetail(string PickNo, int inclusive, int exclusive)
        {
            string sql = string.Format(@"select b.*,a.invno from TBLPICKDETAIL b inner join tblpick a
                 on a.pickno=b.pickno WHERE 1=1 ");
            if (!string.IsNullOrEmpty(PickNo))
            {
                sql += string.Format(" AND b.PICKNO='{0}'", PickNo);
            }
            string subSql = string.Empty;
            object[] objs = this.DataProvider.CustomQuery(typeof(PickDetailEx), new PagerCondition(sql, "b.MDATE DESC,b.MTIME DESC", inclusive, exclusive));
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    PickDetailEx obj = objs[i] as PickDetailEx;
                    subSql = string.Format(@"SELECT COUNT(*) FROM TBLPICKDETAILMATERIAL WHERE PICKNO='{0}' AND PICKLINE='{1}' ", obj.PickNo, obj.PickLine);
                    int re = this.DataProvider.GetCount(new SQLCondition(subSql));
                    if (re > 0)
                        obj.sumBox = re.ToString();
                    else
                        obj.sumBox = "0";
                }
            }
            return objs;
        }
        public object GetPickdetailmaterialsn(string Pickno, string Sn)
        {
            return this.DataProvider.CustomSearch(typeof(Pickdetailmaterialsn), new object[] { Pickno, Sn });
        }

        public object[] QueryPickdetailmaterialsn(string Pickno)
        {
            string sql = @"select a.* from TBLPICKDetailMaterialSN a where a.pickno = '" + Pickno + "'";
            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterialsn), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", 1, int.MaxValue));
        }

        public object[] QueryPickdetailmaterialsnByCartonno(string Pickno, string Cartonno)
        {
            string sql = string.Format(@"select a.* from TBLPICKDetailMaterialSN a where a.pickno ='{0}' and  a.Cartonno='{1}' ", Pickno, Cartonno);
            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterialsn), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", 1, int.MaxValue));
        }
        public object[] QueryPickdetailmaterialsnByPickLine(string PickNo, string PickLine)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAILMATERIALSN  WHERE PICKNO='{1}' AND PICKLINE='{2}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterialsn)), PickNo, PickLine);
            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterialsn), new SQLCondition(sql));
        }

        #endregion

        #region Pickdetailmaterial  Amy add
        /// <summary>
        /// TBLPICKDETAILMATERIAL
        /// </summary>
        public Pickdetailmaterial CreateNewPickdetailmaterial()
        {
            return new Pickdetailmaterial();
        }

        public void AddPickdetailmaterial(Pickdetailmaterial pickdetailmaterial)
        {
            this.DataProvider.Insert(pickdetailmaterial);
        }

        public void DeletePickdetailmaterial(Pickdetailmaterial pickdetailmaterial)
        {
            this.DataProvider.Delete(pickdetailmaterial);
        }

        public void DeletePickdetailmaterial(Pickdetailmaterial[] locations)
        {
            this._helper.DeleteDomainObject(locations);
        }

        public void UpdatePickdetailmaterial(Pickdetailmaterial pickdetailmaterial)
        {
            this.DataProvider.Update(pickdetailmaterial);
        }

        public object GetPickdetailmaterial(string Pickno, string Cartonno)
        {
            return this.DataProvider.CustomSearch(typeof(Pickdetailmaterial), new object[] { Pickno, Cartonno });
        }

        #region Pickdetailmaterial add by sam 2016年5月17日
        public int GetPickdetailmaterialCount(string pickno)
        {
            string sql = string.Format("select  count(1) from tblPickdetailmaterial where pickno='{0}' ", pickno);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #endregion
        #region  pickdetailmaterial  function   Amy add @20160202
        /// <summary>
        /// 箱单导入时，DNR,UB类型查找生产日期，批号等
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="pickNO"></param>
        /// <returns></returns>
        public object GetLotNOInformationFromSN(string sn, string pickNO)
        {
            string sql = string.Format("select cartonno from tblpickdetailmaterialsn where sn='{0}' ", sn);
            if (!string.IsNullOrEmpty(pickNO))
                sql += string.Format(" and pickno='{0}'", pickNO);
            sql += " order by mdate desc,mtime desc";
            object[] objs = this.DataProvider.CustomQuery(typeof(Pickdetailmaterialsn), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                sql = "select * from tblpickdetailmaterial where cartonno='" + (objs[0] as Pickdetailmaterialsn).Cartonno + "' ";
                if (!string.IsNullOrEmpty(pickNO))
                    sql += string.Format(" and pickno='{0}'", pickNO);
                sql += "  order by mdate desc,mtime desc";
                object[] objs1 = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
                if (objs1 != null && objs1.Length > 0)
                {
                    return objs1[0];
                }

            }
            return null;
        }
        public object GetLotNOInformationFromDQMCODE(string dqmcode, string pickNO)
        {

            string sql = string.Format("select * from tblpickdetailmaterial where dqmcode='{0}' ", dqmcode);
            sql += string.Format(" and pickno='{0}'", pickNO);
            sql += "  order by mdate,mtime";
            object[] objs1 = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            if (objs1 != null && objs1.Length > 0)
            {
                return objs1[0];
            }


            return null;
        }

        public object GetPickDetailMaterialFromDQMCODE(string dqmcode, string pickNo, string pickline)
        {

            string sql = string.Format("select * from tblpickdetailmaterial where dqmcode='{0}'  and pickno='{1}' and pickline='{2}' ", dqmcode, pickNo, pickline);
            sql += "  order by mdate,mtime";
            object[] objs1 = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            if (objs1 != null && objs1.Length > 0)
            {
                return objs1[0];
            }


            return null;
        }
        /// <summary>
        ///箱单导入时 JCR 测试返工入库 查找生产日期，批号等
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="pickNO"></param>
        /// <returns></returns>
        public object GetLotNOInformationFromSNforJCR(List<string> sns)
        {
            string sql = "select a.* from TBLStorageDetail a ,(select cartonno from tblstoragedetailsn where sn in(" + SqlFormat(sns) + ")) b where a.cartonno=b.cartonno order by MDATE,mtime ";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }




        public object GetLotNOInformationforJCR(string dqmcode, string reworkApplyUser, string storageCode)
        {

            string sql = string.Format("select * from tblstoragedetail where dqmcode='{0}' ", dqmcode);
            if (!string.IsNullOrEmpty(reworkApplyUser))
                sql += string.Format(" and ReworkApplyUser='{0}'", reworkApplyUser);
            sql += string.Format(" and StorageCode='{0}'", storageCode);
            sql += "  order by mdate,mtime";
            object[] objs1 = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));
            if (objs1 != null && objs1.Length > 0)
            {
                return objs1[0];
            }


            return null;
        }

        public bool GetLotNOInformationFromSNforcheck(string sn, bool checkStorageCode, string storagecode)
        {
            string sql = string.Format("select cartonno from tblstoragedetailsn where sn='{0}' ", sn);

            sql += " order by mdate desc,mtime desc";
            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetailSN), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                if (checkStorageCode)
                {
                    sql = "select * from tblstoragedetail where cartonno='" + (objs[0] as StorageDetailSN).CartonNo + "' and storagecode='" + storagecode + "' ";

                    sql += "  order by mdate desc,mtime desc";
                    object[] objs1 = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));
                    if (objs1 != null && objs1.Length > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }

            }
            if (!checkStorageCode)
                return true;
            else
                return false;
        }
        #endregion
        #region  Amy  Add @20160329  箱单导入后期，增加检查sn是否重复逻辑
        public object[] CheckBeforeCloseSN(string SN)
        {
            string sql = string.Format("SELECT SN.* FROM TBLASN H INNER JOIN TBLASNDETAILSN SN ON H.STNO=SN.STNO AND STATUS IN ('{0}','{1}','{2}','{3}','{4}') AND SN.SN='{5}'", ASN_STATUS.ASN_Release, ASN_STATUS.ASN_WaitReceive, ASN_STATUS.ASN_Receive, ASN_STATUS.ASN_IQC, ASN_STATUS.ASN_OnLocation, SN);
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
        }
        public object[] CheckAfterCloseSN(string SN)
        {
            string sql = string.Format("SELECT {0} FROM TBLSTORAGEDETAILSN WHERE SN='{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetailSN)), SN);
            return this.DataProvider.CustomQuery(typeof(StorageDetailSN), new SQLCondition(sql));
        }
        public object[] GetASNDetailByStNoAndDQMCode(string StNo, string DQMCode)
        {
            string sql = string.Format("SELECT {0} FROM TBLASNDETAIL WHERE STNO='{1}' AND DQMCODE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), StNo, DQMCode);
            return this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
        }
        #endregion
        #region   Amy add   --物料上架
        public object[] GetStorageDetailByDQMCodeAndStorageCode(string DQMCode, string StorageCode)
        {
            string sql = string.Format("SELECT {0} FROM TBLSTORAGEDETAIL WHERE DQMCODE='{1}' AND STORAGECODE='{2}' ORDER BY AvailableQTY", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetail)), DQMCode, StorageCode);
            return this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));
        }
        /// <summary>
        /// 物料上架grid信息
        /// </summary>
        /// <param name="CartonNo"></param>
        /// <param name="LacationNo"></param>
        /// <returns></returns>
        //        public object[] QueryOnshelvesDetail(string CartonNo, string LacationNo)
        //        {
        //            //string SQL_1 = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asn)) + " FROM TBLASN WHERE STNO IN (SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='" + CartonNo + "') AND STATUS not in('IQCRejection','ReceiveRejection','Close')";
        //            string SQL_1 = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asn)) + " FROM TBLASN WHERE STNO IN (SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='" + CartonNo + "'  AND STATUS not in('IQCRejection','ReceiveRejection','Close'))";
        //            object[] obj_type = this.DataProvider.CustomQuery(typeof(Asn), new SQLCondition(SQL_1));
        //            if (obj_type != null)
        //            {
        //                Asn asn = obj_type[0] as Asn;
        //                object[] objs = null;
        //                if (asn.StType == InInvType.PD || asn.StType == InInvType.SCTR)
        //                {
        //                    //检查是否都receiveClose啦
        //                    SQL_1 = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)) + " FROM TBLASNDETAIL WHERE STNO='" + asn.Stno + "'";
        //                    bool FF = false;
        //                    object[] objs_asndetail = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(SQL_1));
        //                    foreach (Asndetail asndetail in objs_asndetail)
        //                    {
        //                        if (asn.StType == InInvType.PD)
        //                        {
        //                            if (asndetail.Status == ASNDetail_STATUS.ASNDetail_ReceiveClose || asndetail.Status == ASNDetail_STATUS.ASNDetail_Cancel || asndetail.Status == ASNDetail_STATUS.ASNDetail_IQCClose)
        //                                FF = true;
        //                        }
        //                        else
        //                        {
        //                            if (asn.Rejects_flag == "Y")
        //                            {
        //                                if (asndetail.Status == ASNDetail_STATUS.ASNDetail_ReceiveClose || asndetail.Status == ASNDetail_STATUS.ASNDetail_Cancel || asndetail.Status == ASNDetail_STATUS.ASNDetail_IQCClose)
        //                                {
        //                                    FF = true;
        //                                }

        //                            }
        //                            else if (asndetail.Status == ASNDetail_STATUS.ASNDetail_IQCClose)
        //                            {
        //                                FF = true;
        //                            }
        //                        }
        //                    }
        //                    if (FF)
        //                    {
        //                        SQL_1 = string.Format(@"SELECT {0} FROM TBLASNDETAIL WHERE 1=1  AND STNO IN (
        //                                        SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='{1}'
        //                                        ) 
        //                                        AND DQMCODE=(SELECT DQMCODE FROM TBLASNDETAIL WHERE CARTONNO='{1}' AND STNO='{2}') AND STATUS NOT IN ('" + ASNDetail_STATUS.ASNDetail_Cancel + "','" + ASNDetail_STATUS.ASNDetail_IQCRejection + "') AND InitReceiveStatus<>'Reject'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), CartonNo, asn.Stno);
        //                        objs = this.DataProvider.CustomQuery(typeof(Asndetailexp), new SQLCondition(SQL_1));
        //                    }
        //                }
        //                else
        //                {

        //                    string sql = string.Format(@"SELECT {0} FROM TBLASNDETAIL WHERE 1=1  AND STATUS IN ('{1}','{2}','{3}') AND QCPASSQTY>0 AND STNO IN (
        //                                        SELECT STNO FROM TBLASNDETAIL WHERE STATUS IN ('{1}','{2}','{3}') AND QCPASSQTY>0 AND CARTONNO='{5}'
        //                                        ) 
        //                                        AND DQMCODE=(SELECT DQMCODE FROM TBLASNDETAIL WHERE CARTONNO='{5}' AND STNO='{6}')  AND InitReceiveStatus<>'Reject'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), ASNDetail_STATUS.ASNDetail_IQCClose, ASNDetail_STATUS.ASNDetail_OnLocation, ASNDetail_STATUS.ASNDetail_Close, ASNDetail_STATUS.ASNDetail_IQCClose, CartonNo, asn.Stno);

        //                    objs = this.DataProvider.CustomQuery(typeof(Asndetailexp), new SQLCondition(sql));

        //                }

        //                if (objs != null)
        //                {

        //                    for (int i = 0; i < objs.Length; i++)
        //                    {
        //                        Asndetailexp exp = objs[i] as Asndetailexp;


        //                        string sql1 = string.Format(@"SELECT locationcode  FROM TBLSTORAGEDETAIL L WHERE L.STORAGECODE=(
        //                                                SELECT STORAGECODE FROM TBLASN WHERE STNO='{1}'
        //                                                ) and dqmcode='{0}' and rownum<=1 ORDER BY L.LASTSTORAGEAGEDATE DESC ", exp.DqmCode, asn.Stno);
        //                        object[] locaobjs = this.DataProvider.CustomQuery(typeof(Location), new SQLCondition(sql1));




        //                        if (locaobjs != null)
        //                            exp.ReLocationCode = ((Location)locaobjs[0]).LocationCode;
        //                    }
        //                    return objs;
        //                }
        //            }

        //            return null;
        //        }









        public object[] QueryOnshelvesDetail(string CartonNo, string LacationNo)
        {
            //string SQL_1 = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asn)) + " FROM TBLASN WHERE STNO IN (SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='" + CartonNo + "') AND STATUS not in('IQCRejection','ReceiveRejection','Close')";
            string SQL_1 = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asn)) + " FROM TBLASN WHERE STNO IN (SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='" + CartonNo + "'  AND STATUS not in('IQCRejection','ReceiveRejection','Close'))";
            object[] obj_type = this.DataProvider.CustomQuery(typeof(Asn), new SQLCondition(SQL_1));
            if (obj_type != null)
            {
                Asn asn = obj_type[0] as Asn;
                object[] objs = null;
                if (asn.StType == InInvType.PD || asn.StType == InInvType.SCTR)
                {
                    //检查是否都receiveClose啦
                    SQL_1 = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)) + " FROM TBLASNDETAIL WHERE STNO='" + asn.Stno + "'";
                    bool FF = false;
                    object[] objs_asndetail = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(SQL_1));
                    foreach (Asndetail asndetail in objs_asndetail)
                    {
                        if (asn.StType == InInvType.PD)
                        {
                            if (asndetail.Status == ASNDetail_STATUS.ASNDetail_ReceiveClose || asndetail.Status == ASNDetail_STATUS.ASNDetail_Cancel || asndetail.Status == ASNDetail_STATUS.ASNDetail_IQCClose)
                                FF = true;
                        }
                        else
                        {
                            if (asn.Rejects_flag == "Y")
                            {
                                if (asndetail.Status == ASNDetail_STATUS.ASNDetail_ReceiveClose || asndetail.Status == ASNDetail_STATUS.ASNDetail_Cancel || asndetail.Status == ASNDetail_STATUS.ASNDetail_IQCClose)
                                {
                                    FF = true;
                                }

                            }
                            else if (asndetail.Status == ASNDetail_STATUS.ASNDetail_IQCClose)
                            {
                                FF = true;
                            }
                        }
                    }
                    if (FF)
                    {
                        SQL_1 = string.Format(@"SELECT {0} FROM TBLASNDETAIL WHERE 1=1  AND STNO IN (
                                        SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='{1}'
                                        ) 
                                        AND DQMCODE=(SELECT DQMCODE FROM TBLASNDETAIL WHERE CARTONNO='{1}' AND STNO='{2}') AND STATUS NOT IN ('" + ASNDetail_STATUS.ASNDetail_Cancel + "','" + ASNDetail_STATUS.ASNDetail_IQCRejection + "') AND InitReceiveStatus<>'Reject'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), CartonNo, asn.Stno);
                        objs = this.DataProvider.CustomQuery(typeof(Asndetailexp), new SQLCondition(SQL_1));
                    }
                }
                else
                {
                    List<string> s = new List<string>();
                    foreach (Asn a in obj_type)
                    {
                        s.Add(a.Stno);
                    }
                    string str = SqlFormat(s);

                    string sql = string.Format(@"SELECT {0} FROM TBLASNDETAIL WHERE 1=1  AND STATUS IN ('{1}','{2}','{3}') AND QCPASSQTY>0 AND STNO IN (
                                       " + str + @") 
                                        AND DQMCODE=(SELECT DQMCODE FROM TBLASNDETAIL WHERE CARTONNO='{5}' AND STNO='{6}')  AND InitReceiveStatus<>'Reject'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), ASNDetail_STATUS.ASNDetail_IQCClose, ASNDetail_STATUS.ASNDetail_OnLocation, ASNDetail_STATUS.ASNDetail_Close, ASNDetail_STATUS.ASNDetail_IQCClose, CartonNo, asn.Stno);

                    objs = this.DataProvider.CustomQuery(typeof(Asndetailexp), new SQLCondition(sql));

                }

                if (objs != null)
                {

                    for (int i = 0; i < objs.Length; i++)
                    {
                        Asndetailexp exp = objs[i] as Asndetailexp;


                        string sql1 = string.Format(@"SELECT locationcode  FROM TBLSTORAGEDETAIL L WHERE L.STORAGECODE=(
                                                SELECT STORAGECODE FROM TBLASN WHERE STNO='{1}'
                                                ) and dqmcode='{0}' and rownum<=1 ORDER BY L.LASTSTORAGEAGEDATE DESC ", exp.DqmCode, asn.Stno);
                        object[] locaobjs = this.DataProvider.CustomQuery(typeof(Location), new SQLCondition(sql1));




                        if (locaobjs != null)
                            exp.ReLocationCode = ((Location)locaobjs[0]).LocationCode;
                    }
                    return objs;
                }
            }

            return null;
        }



        public int QueryOnshelvesDetailCount(string CartonNo)
        {
            string SQL_1 = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asn)) + " FROM TBLASN WHERE STNO IN (SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='" + CartonNo + "') AND STATUS not in('IQCRejection','ReceiveRejection','Close')";
            object[] obj_type = this.DataProvider.CustomQuery(typeof(Asn), new SQLCondition(SQL_1));
            if (obj_type != null)
            {
                Asn asn = obj_type[0] as Asn;
                if (asn.StType == InInvType.PD || asn.StType == InInvType.SCTR)
                {
                    //检查是否都receiveClose啦
                    SQL_1 = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)) + " FROM TBLASNDETAIL WHERE STNO='" + asn.Stno + "'";
                    bool FF = true;
                    object[] objs_asndetail = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(SQL_1));
                    foreach (Asndetail asndetail in objs_asndetail)
                    {
                        if (asndetail.Status != ASNDetail_STATUS.ASNDetail_ReceiveClose && asndetail.Status != ASNDetail_STATUS.ASNDetail_Cancel)
                        {
                            FF = false;
                        }
                    }
                    if (FF)
                    {
                        SQL_1 = string.Format(@"SELECT {0} FROM TBLASNDETAIL WHERE 1=1  AND STNO IN (
                                        SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='{1}'
                                        ) 
                                        AND DQMCODE=(SELECT DQMCODE FROM TBLASNDETAIL WHERE CARTONNO='{1}' and stno='{2}') AND STATUS NOT IN ('" + ASNDetail_STATUS.ASNDetail_Cancel + "','" + ASNDetail_STATUS.ASNDetail_IQCRejection + "') AND InitReceiveStatus<>'Reject'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), CartonNo, asn.Stno);
                        return this.DataProvider.GetCount(new SQLCondition(SQL_1));
                    }
                }
                else
                {

                    string sql = string.Format(@"SELECT COUNT(*) FROM TBLASNDETAIL WHERE 1=1  AND STATUS IN ('{1}','{2}','{3}') AND QCPASSQTY>0 AND STNO IN (
                                        SELECT STNO FROM TBLASNDETAIL WHERE QCPASSQTY>0 AND CARTONNO='{5}'
                                        )", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailexp)), ASNDetail_STATUS.ASNDetail_IQCClose, ASNDetail_STATUS.ASNDetail_OnLocation, ASNDetail_STATUS.ASNDetail_Close, ASNDetail_STATUS.ASNDetail_IQCClose, CartonNo);
                    return this.DataProvider.GetCount(new SQLCondition(sql));
                }

            }
            return 0;

        }
        /// <summary>
        /// 应上架信息
        /// </summary>
        /// <param name="cartonno"></param>
        /// <returns></returns>
        public int QueryPlanOnShelvesQTY(string cartonno)
        {
            string sql = @"SELECT  distinct t1.CARTONNO FROM TBLASNDETAIL t1,tblasn t2 WHERE t1.stno=t2.stno 
and t1.status in('IQCClose') 
and qcpassqty>0 
and t1.stno in( SELECT STNO FROM TBLASNDETAIL where CARTONNO='" + cartonno + @"') 
and t1.STATUS not in('IQCRejection','ReceiveRejection','Close') 
and t1.cartonno is not null
";
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            return ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 已上架信息
        /// </summary>
        /// <param name="cartonno"></param>
        /// <returns></returns>
        public int QueryActOnShelvesQTY(string cartonno)
        {
            string sql = @"SELECT  distinct t1.CARTONNO FROM TBLASNDETAIL t1,tblasn t2 WHERE t1.stno=t2.stno 
and t1.status in('Close') 
and qcpassqty>0 
and t1.stno in( SELECT STNO FROM TBLASNDETAIL where CARTONNO='" + cartonno + @"') 
and t2.STATUS not in('IQCRejection','ReceiveRejection','Close') 
and t1.cartonno is not null";


            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            return ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 上架按钮检查信息
        /// </summary>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public int CheckDatabyCartonNo(string cartonNo)
        {
            string sql = "SELECT COUNT(*) FROM TBLASNDETAIL WHERE CARTONNO='{0}' ";
            sql = string.Format(sql, cartonNo);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        /// <summary>
        /// 检查货位号是否存在，库位是否一致
        /// </summary>
        /// <param name="locationNo"></param>
        /// <param name="cartonNo"></param>
        /// <returns></returns>
        public int CheckLocationInfobyLocationNo(string locationNo, string cartonNo)
        {
            int count = 0;
            string sql = "SELECT COUNT(*) FROM TBLLOCATION WHERE LOCATIONCODE='{0}'";
            sql = string.Format(sql, locationNo);
            count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count == 0)
                return 1;
            else
            {
                sql = string.Format(@"SELECT COUNT(*) FROM TBLLOCATION WHERE LOCATIONCODE='{0}' AND STORAGECODE= (
                                    SELECT STORAGECODE FROM TBLASN WHERE STNO IN(
                                    SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='{1}' AND  STATUS not in('IQCRejection','ReceiveRejection','Close')
                                    ) 
                                    )", locationNo, cartonNo);
                //                sql = string.Format(@"SELECT COUNT(*) FROM TBLLOCATION WHERE LOCATIONCODE='{0}' AND STORAGECODE= (
                //                                    SELECT STORAGECODE FROM TBLASN WHERE STNO IN(
                //                                    SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='{1}' 
                //                                    ) AND  STATUS not in('IQCRejection','ReceiveRejection','Close')
                //                                    )", locationNo, cartonNo);
                count = this.DataProvider.GetCount(new SQLCondition(sql));
                if (count == 0)
                    return 2;
                else
                    return 0;
            }
        }

        /// <summary>
        /// 检查货位号是否存在
        /// </summary>
        /// <param name="LocationCode"></param>
        /// <returns></returns>
        public bool IsExistLocationCode(string locationCode)
        {
            string sql = "SELECT COUNT(1) FROM TBLLOCATION WHERE LOCATIONCODE='{0}'";
            sql = string.Format(sql, locationCode);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 上架获取需要更新数据
        /// </summary>
        /// <param name="stno"></param>
        /// <param name="stline"></param>
        /// <returns></returns>
        public object[] GetASNDetailItembyStnoAndStline(string stno, string stline)
        {
            string sql = "SELECT  {0} FROM TBLASNDETAILITEM WHERE STNO='{1}' AND STLINE='{2}'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailitem)), stno, stline);
            return this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql));
        }
        public object[] GetASNDetailSNbyStnoandStline(string Stno, string StLine)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNDETAILSN WHERE STNO='{1}' AND STLINE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailsn)), Stno, StLine);
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
        }

        public object[] GetASNDetailSNbyStnoandStline1(string Stno, string StLine)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNDETAILSN WHERE STNO='{1}' AND STLINE='{2}' AND QCSTATUS<>'N'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailsn)), Stno, StLine);
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
        }



        /// <summary>
        /// 检查asndetail表中的状态
        /// </summary>
        /// <param name="Stno"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public bool JudgeASNDetailStatus(string Stno, string Status)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNDETAIL WHERE STNO='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), Stno);
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
            bool IsStatus = true;
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if ((objs[i] as Asndetail).Status != Status)
                    {
                        IsStatus = false;
                        break;
                    }

                }
                return IsStatus;
            }
            return false;
        }
        public bool JudgeInvoiceDetailStatus(string InvNO)
        {
            string sql = string.Format("SELECT {0} FROM TBLINVOICESDETAIL WHERE INVNO='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), InvNO);

            object[] objs = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            bool IsStatus = true;
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if ((objs[i] as InvoicesDetail).PlanQty != (objs[i] as InvoicesDetail).ActQty)
                    {
                        IsStatus = false;
                        break;
                    }

                }
                return IsStatus;
            }
            return false;
        }
        public string GetCUSCodebyCartonNo(string cartonno)
        {

            string sql = string.Format(@"SELECT * FROM TBLINVOICESDETAIL WHERE INVNO=(
                                        SELECT INVNO FROM TBLASN WHERE STNO IN(
                                        SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='{0}' AND  STATUS not in('IQCRejection','ReceiveRejection','Close')
                                        ) 
                                        )", cartonno);

            //            string sql = string.Format(@"SELECT * FROM TBLINVOICESDETAIL WHERE INVNO=(
            //                                        SELECT INVNO FROM TBLASN WHERE STNO IN(
            //                                        SELECT STNO FROM TBLASNDETAIL WHERE CARTONNO='{0}'
            //                                        ) AND  STATUS not in('IQCRejection','ReceiveRejection','Close')
            //                                        )", cartonno);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["CUSMCODE"].ToString();
            }
            return string.Empty;
        }

        public string GetStorageQtyByMcodeAndStorageCode(string MCode, string StorageCode)
        {
            string sql = string.Format("SELECT NVL(SUM(STORAGEQTY),0) FROM TBLSTORAGEDETAIL WHERE MCODE='{0}' AND STORAGECODE='{1}'", MCode, StorageCode);
            return ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql).Tables[0].Rows[0][0].ToString();
        }
        #endregion

        #region StorageInfo
        /// <summary>
        /// TBLSTORAGEINFO
        /// </summary>
        public StorageInfo CreateNewStorageinfo()
        {
            return new StorageInfo();
        }

        public void AddStorageinfo(StorageInfo storageinfo)
        {
            this.DataProvider.Insert(storageinfo);
        }

        public void DeleteStorageinfo(StorageInfo storageinfo)
        {
            this.DataProvider.Delete(storageinfo);
        }

        public void UpdateStorageinfo(StorageInfo storageinfo)
        {
            //this.DataProvider.Update(storageinfo);
            string sql = "";
            sql += string.Format("UPDATE TBLSTORAGEINFO SET storageqty='{0}',muser='{1}',mdate='{2}',mtime='{3}'  ",
                                 storageinfo.Storageqty, storageinfo.Muser, storageinfo.Mdate, storageinfo.Mtime);
            sql += string.Format(" where  Mcode='{0}' and  StorageCode='{1}' ", storageinfo.Mcode, storageinfo.StorageCode);
            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }

        public object GetStorageinfo(string MCode, string StorageCode)
        {

            //return this.DataProvider.CustomSearch(typeof(StorageInfo), new object[] { StorageCode, MCode });
            string sql = "SELECT a.* from TBLSTORAGEINFO a where a.MCODE='" + MCode + "' and a.StorageCode='" + StorageCode + "' ";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageInfo), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
                return objs[0];
            return null;

        }



        #endregion
        #region InvInOutTrans  Add By Amy
        /// <summary>
        /// TBLINVINOUTTRANS
        /// </summary>
        public InvInOutTrans CreateNewInvInOutTrans()
        {
            return new InvInOutTrans();
        }

        public void AddInvInOutTrans(InvInOutTrans invinouttrans)
        {
            this.DataProvider.Insert(invinouttrans);
        }

        public void DeleteInvInOutTrans(InvInOutTrans invinouttrans)
        {
            this.DataProvider.Delete(invinouttrans);
        }

        public void UpdateInvinouttrans(InvInOutTrans invinouttrans)
        {
            this.DataProvider.Update(invinouttrans);
        }

        public object GetInvInOutTrans(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(InvInOutTrans), new object[] { Serial });
        }

        #endregion


        #region ASN--仓库执行入库指令  add by sam 2016-02-22
        //ASN查询
        /// <summary>
        /// ASN查询
        /// </summary>
        /// <param name="stNo">入库指令号</param>
        /// <param name="stType">入库类型</param>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="cUser">创建人</param>
        /// <param name="status">状态</param>
        /// <param name="bCDate">创建日期开始</param>
        /// <param name="eCDate">创建日期结束</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryExecuteASN(string stNo, string stType, string invNo, string stackCode, string status, int bCDate, int eCDate, string usercode, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT  {0} FROM TBLASN  WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ASN)));
            sql += string.Format(@" AND STATUS  not in ('{0}')", "Release");
            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND STNO ='{0}'", stNo);
            }

            if (!string.IsNullOrEmpty(usercode) && usercode != "ADMIN")
            {
                sql += string.Format(@" AND StorageCode in  ( select distinct  paramcode from TBLSYSPARAM where PARAMGROUPCODE in (
                select USERGROUPCODE ||'{0}' from TBLUSERGROUP2USER where USERCODE='{1}'  ))", "TOSTORAGE", usercode);
            }

            if (!string.IsNullOrEmpty(stType))
            {
                if (stType.IndexOf(",") > 0)
                {

                    sql += string.Format(@" AND STTYPE IN ({0})", stType);
                }
                else
                {
                    sql += string.Format(@" AND STTYPE IN ('{0}')", stType);
                }
            }

            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(stackCode))
            {
                sql += string.Format(@" AND StorageCode = '{0}'", stackCode);
            }
            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND STATUS = '{0}'", status);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            return this.DataProvider.CustomQuery(typeof(ASN), new PagerCondition(sql, "MDATE DESC,MTIME DESC", inclusive, exclusive));
        }

        //ASN总行数
        /// <summary>
        /// ASN总行数
        /// </summary>
        /// <param name="stNo">入库指令号</param>
        /// <param name="stType">入库类型</param>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="cUser">创建人</param>
        /// <param name="status">状态</param>
        /// <param name="bCDate">创建日期开始</param>
        /// <param name="eCDate">创建日期结束</param>
        /// <returns></returns>
        public int QueryExecuteASNCount(string stNo, string stType, string invNo, string stackCode, string status, int bCDate, int eCDate, string usercode)
        {
            string sql = @" SELECT COUNT(1) FROM TBLASN WHERE 1=1 ";
            sql += string.Format(@" AND STATUS  not in ('{0}')", "Release");
            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND STNO ='{0}'", stNo);
            }
            if (!string.IsNullOrEmpty(usercode) && usercode != "ADMIN")
            {
                sql += string.Format(@" AND StorageCode in  ( select distinct  paramcode from TBLSYSPARAM where PARAMGROUPCODE in (
                select USERGROUPCODE ||'{0}' from TBLUSERGROUP2USER where USERCODE='{1}'  ))", "TOSTORAGE", usercode);
            }

            if (!string.IsNullOrEmpty(stType))
            {
                if (stType.IndexOf(",") > 0)
                {

                    sql += string.Format(@" AND STTYPE IN ({0})", stType);
                }
                else
                {
                    sql += string.Format(@" AND STTYPE IN ('{0}')", stType);
                }
            }

            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(stackCode))
            {
                sql += string.Format(@" AND StorageCode = '{0}'", stackCode);
            }
            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND STATUS = '{0}'", status);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region ASN--仓库执行入库指令明细  add by sam 2016-02-22

        public string GetShipToStock(string mcode, int day)
        {
            string sql = string.Format(@"SELECT COUNT(1) FROM TBLShipToStock
                    WHERE itemcode='{0}' AND IVLDATE > '{1}' and  EFFDATE <'{1}'    ", mcode, day);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return "Y";
            }
            return "";
        }

        #endregion

        #region   Add by Amy @20160304   拣料作业

        //public DataTable QueryPickDetail(string PickNo)
        //{
        //    string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)));
        //    if (!string.IsNullOrEmpty(PickNo))
        //    {
        //        sql += string.Format(" AND PICKNO='{0}'", PickNo);
        //    }
        //    DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        return ds.Tables[0];
        //    return null;
        //}

        public DataTable PickedView(string PickNo, string DQMCode)
        {
            string sql = string.Format(@"SELECT M.CARTONNO,M.LOTNO,M.LOCATIONCODE,S.SN,R.USERNAME,M.MDATE FROM TBLPICKDETAILMATERIAL M 
                                    LEFT JOIN TBLPICKDETAILMATERIALSN S ON M.PICKNO=S.PICKNO AND M.PICKLINE=S.PICKLINE AND M.CARTONNO=S.CARTONNO
                                    LEFT JOIN TBLUSER R ON M.MUSER=R.USERCODE
                                    WHERE M.PICKNO='{0}' AND M.DQMCODE='{1}' ", PickNo, DQMCode);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        //add by sam 2016年3月28日
        public DataTable LocStorTransView(string transNo, string dqmCode)
        {
            string sql = string.Format(@"SELECT M.DQMCODE,M.FromLocationCode,M.LocationCode,M.QTY FROM TBLStorLocTransDetailCarton M 
                                    WHERE M.transno='{0}' AND M.DQMCODE='{1}' ", transNo, dqmCode);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable QueryPickNO1()
        {
            string sql = string.Format(@"SELECT PICKNO FROM TBLPICK WHERE STATUS IN ('{0}','{1}')", Pick_STATUS.Status_Pick, Pick_STATUS.Status_WaitPick);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }
        public string[] QueryPickNO(string[] userGroups)
        {
            string sql = string.Format(@"SELECT PICKNO FROM TBLPICK WHERE STATUS IN ('{0}','{1}')  ", Pick_STATUS.Status_Pick, Pick_STATUS.Status_WaitPick);
            sql += @"AND PICKNO IN(SELECT DISTINCT PICKNO FROM
 (SELECT PICKNO FROM TBLPICK WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @"))) T)  ";
            sql += "  order by cdate,ctime desc";
            //DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            object[] obj = this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
            string[] str;
            if (obj != null)
            {
                str = new string[obj.Length];
                int i = 0;
                foreach (Pick pi in obj)
                {
                    str[i++] = pi.PickNo;
                }
                return str;
            }
            return null;
        }
        public string GetKeyPartsInfo(string CartonNo)
        {
            string sql = string.Format(@"SELECT MCONTROLTYPE FROM TBLMATERIAL WHERE MCODE=(SELECT MCODE FROM TBLSTORAGEDETAIL WHERE CARTONNO='{0}')", CartonNo);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();
            return string.Empty;
        }

        public object[] QueryPickDetail(string pickNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)));
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND PICKNO='{0}'", pickNo);
            }
            return this.DataProvider.CustomQuery(typeof(PickDetail), new PagerCondition(sql, "MDATE DESC,MTIME DESC", 1, int.MaxValue));
        }



        public string[] QueryGFHWItemCode(string pickNo)
        {
            string sql = string.Format(@"SELECT DISTINCT GFHWITEMCODE FROM TBLPICKDETAIL WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)));
            sql += string.Format(" AND PICKNO='{0}'", pickNo);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds == null)
            {
                return null;
            }
            string[] str;
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str[i] = ds.Tables[0].Rows[i][0].ToString();
                }
                return str;
            }
            return null;
        }

        public string[] QueryDQMaterialNO(string pickNo)
        {
            string sql = string.Format(@"SELECT DISTINCT DQMCODE FROM TBLPICKDETAIL WHERE 1=1  ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterial)));
            sql += string.Format("AND STATUS not in ( 'Cancel' )   AND PICKNO='{0}' ", pickNo);
            sql += "order by dqmcode ";
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds == null)
            {
                return null;
            }
            string[] str;
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str[i] = ds.Tables[0].Rows[i][0].ToString();
                }
                return str;
            }
            return null;
        }

        public string[] QueryDQMaterialNO(string pickNo, string gfHWItemCode, string gfPackingSEQ)
        {
            string sql = string.Format(@"SELECT DISTINCT DQMCODE FROM TBLPICKDETAIL WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterial)));
            sql += string.Format(" AND PICKNO='{0}'", pickNo);
            sql += string.Format(" AND GFHWITEMCODE='{0}'", gfHWItemCode);
            sql += string.Format(" AND GFPACKINGSEQ='{0}'", gfPackingSEQ);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds == null)
            {
                return null;
            }
            string[] str;
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str[i] = ds.Tables[0].Rows[i][0].ToString();
                }
                return str;
            }
            return null;
        }

        public string[] QueryGFPackingSEQ(string pickNo, string gfHWItemCode)
        {
            string sql = string.Format(@"SELECT DISTINCT GFPACKINGSEQ FROM TBLPICKDETAIL WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)));
            sql += string.Format(" AND PICKNO='{0}'", pickNo);
            sql += string.Format(" AND GFHWITEMCODE='{0}'", gfHWItemCode);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds == null)
            {
                return null;
            }
            string[] str;
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str[i] = ds.Tables[0].Rows[i][0].ToString();
                }
                return str;
            }
            return null;
        }

        // add by sam
        public string[] QueryGFSEQ(string invNo, string gfHWItemCode)
        {
            string sql = string.Format(@"SELECT DISTINCT GFPACKINGSEQ FROM TBLINVOICESDETAIL WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)));
            sql += string.Format(" AND INVNO in (select a.invno from tblinvoices a where a.dnbatchno ='{0}' )", invNo);
            sql += string.Format(" AND GFHWMCODE ='{0}'", gfHWItemCode);
            sql += string.Format(" order by GFPACKINGSEQ ");
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds == null)
            {
                return null;
            }
            string[] str;
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str[i] = ds.Tables[0].Rows[i][0].ToString();
                }
                return str;
            }
            return null;
        }

        // add by sam  2016年7月19日
        public string[] QueryGFDqsMcode(string invNo, string dqmcode)
        {
            string sql = string.Format(@"SELECT DISTINCT DqsMcode FROM TBLINVOICESDETAIL WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)));
            sql += string.Format(" AND INVNO in (select a.invno from tblinvoices a where a.dnbatchno ='{0}' )", invNo);
            sql += string.Format(" AND dqmcode ='{0}'  and DqsMcode is not null ", dqmcode);
            sql += string.Format(" order by DqsMcode ");
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds == null)
            {
                return null;
            }
            string[] str;
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str[i] = ds.Tables[0].Rows[i][0].ToString();
                }
                return str;
            }
            return null;
        }

        // add by sam  2016年7月19日
        public string[] QueryGFDqsMcodeByPickNo(string pickno, string dqmcode)
        {
            string sql = string.Format(@"SELECT DISTINCT DqsMcode FROM TBLINVOICESDETAIL WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)));
            sql += string.Format(" AND INVNO in (select a.invno from tblinvoices a where a.dnbatchno  in ( select invno from tblpick where pickno='{0}' ) )", pickno);
            sql += string.Format(" AND dqmcode ='{0}'  and DqsMcode is not null ", dqmcode);
            sql += string.Format(" order by DqsMcode ");
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds == null)
            {
                return null;
            }
            string[] str;
            if (ds.Tables[0].Rows.Count > 0)
            {
                str = new string[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str[i] = ds.Tables[0].Rows[i][0].ToString();
                }
                return str;
            }
            return null;
        }

        /// <summary>
        /// 拣料作业查询语句
        /// </summary>
        /// <param name="PickNo"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryPickDetail(string PickNo, string InvNo, int inclusive, int exclusive)
        {
            string sql = string.Format(@"select b.*,a.invno from TBLPICKDETAIL b inner join tblpick a
                 on a.pickno=b.pickno WHERE 1=1 ");
            if (!string.IsNullOrEmpty(PickNo))
            {
                sql += string.Format(" AND b.PICKNO='{0}'", PickNo);
            }
            if (!string.IsNullOrEmpty(InvNo))
            {
                sql += string.Format(" AND a.InvNo='{0}'", InvNo);
            }
            string subSql = string.Empty;
            object[] objs = this.DataProvider.CustomQuery(typeof(PickDetailEx), new PagerCondition(sql, "b.MDATE DESC,b.MTIME DESC", inclusive, exclusive));
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    PickDetailEx obj = objs[i] as PickDetailEx;
                    subSql = string.Format(@"SELECT COUNT(*) FROM TBLPICKDETAILMATERIAL WHERE PICKNO='{0}' AND PICKLINE='{1}' ", obj.PickNo, obj.PickLine);
                    int re = this.DataProvider.GetCount(new SQLCondition(subSql));
                    if (re > 0)
                        obj.sumBox = re.ToString();
                    else
                        obj.sumBox = "0";
                }
            }
            return objs;
        }

        public int QueryPickDetailCount(string PickNo, string InvNo)
        {
            //string sql = string.Format(@"SELECT COUNT(*) FROM TBLPICKDETAIL WHERE PICKNO='{0}'", PickNo);
            string sql = string.Format(@"select count(1) from TBLPICKDETAIL b inner join tblpick a
                 on a.pickno=b.pickno WHERE 1=1 ");
            if (!string.IsNullOrEmpty(PickNo))
            {
                sql += string.Format(" AND b.PICKNO='{0}'", PickNo);
            }
            if (!string.IsNullOrEmpty(InvNo))
            {
                sql += string.Format(" AND a.InvNo='{0}'", InvNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }
        /// <summary>
        /// 拣料作业箱号详情
        /// </summary>
        /// <param name="PickNo"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryPickDetailMaterial(string PickNo, string PickLine, string CartonNo, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAILMATERIAL WHERE PICKNO='{1}' AND PICKLINE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterial)), PickNo, PickLine);
            if (!string.IsNullOrEmpty(CartonNo))
            {
                sql += string.Format(@"AND CartonNo='{0}'", CartonNo);
            }
            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new PagerCondition(sql, "MDATE DESC,MTIME DESC", inclusive, exclusive));

        }

        public int QueryPickDetailMaterialCount(string PickNo, string PickLine, string CartonNo)
        {
            string sql = string.Format(@"SELECT COUNT(*) FROM TBLPICKDETAILMATERIAL WHERE PICKNO='{0}' AND PICKLINE='{1}'", PickNo, PickLine);
            if (!string.IsNullOrEmpty(CartonNo))
            {
                sql += string.Format(@"AND CartonNo='{0}'", CartonNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }
        /// <summary>
        /// 拣料作业SN详情
        /// </summary>
        /// <param name="PickNo"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryPickDetailMaterialSN(string PickNo, string PickLine, string CartonNo, string sn, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAILMATERIALSN  WHERE PICKNO='{1}' AND PICKLINE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterialsn)), PickNo, PickLine);
            if (!string.IsNullOrEmpty(CartonNo))
            {
                sql += string.Format(@"AND CartonNo='{0}'", CartonNo);
            }
            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(@"AND sn='{0}'", sn);
            }
            object[] objs = this.DataProvider.CustomQuery(typeof(PickDetailMaterialSNEx), new PagerCondition(sql, "MDATE DESC,MTIME DESC", inclusive, exclusive));
            string subSql = string.Empty;
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    PickDetailMaterialSNEx obj = objs[i] as PickDetailMaterialSNEx;
                    subSql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE PICKNO='{1}' AND PICKLINE='{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), obj.Pickno, obj.Pickline);
                    DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(subSql);
                    obj.CusMCode = ds.Tables[0].Rows[0]["CUSTMCODE"].ToString();
                    obj.DQMCODE = ds.Tables[0].Rows[0]["DQMCODE"].ToString();
                }
            }
            return objs;

        }

        public int QueryPickDetailMaterialSNCount(string PickNo, string PickLine, string CartonNo, string sn)
        {
            string sql = string.Format(@"SELECT COUNT(*) FROM TBLPICKDETAILMATERIALSN WHERE PICKNO='{0}' AND PICKLINE='{1}'", PickNo, PickLine);
            if (!string.IsNullOrEmpty(CartonNo))
            {
                sql += string.Format(@"AND CartonNo='{0}'", CartonNo);
            }
            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(@"AND sn='{0}'", sn);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CartonNo"></param>
        /// <param name="PickNo"></param>
        /// <param name="check">是否先进先出</param>
        /// <param name="IsAll">True: 整箱</param>
        /// <param name="Qty"></param>
        /// <param name="SN"></param>
        /// <returns></returns>
        public int CheckStorageCode(string CartonNo, string PickNo, bool check, bool IsAll, string Qty, string SN)
        {
            string sql = string.Empty;
            bool IsParts = true;
            if (string.IsNullOrEmpty(CartonNo))
            {
                if (string.IsNullOrEmpty(SN))
                {
                    return 8;  //采集数据不全
                }
                sql = string.Format(@"SELECT {0} FROM TBLSTORAGEDETAILSN WHERE SN='{1}' AND PICKBLOCK<>'Y'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetailSN)), SN);
                object[] objs100 = this.DataProvider.CustomQuery(typeof(StorageDetailSN), new SQLCondition(sql));
                if (objs100 == null)
                {
                    return 9;//库存中没有可用SN数据
                }
                CartonNo = (objs100[0] as StorageDetailSN).CartonNo;
            }
            sql = string.Format(@"SELECT {0} FROM TBLSTORAGEDETAIL WHERE CARTONNO='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetail)), CartonNo);
            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));
            if (objs == null)
            {
                return 1;  //没有数据
            }
            else
            {
                sql = string.Format("SELECT MCONTROLTYPE FROM TBLMATERIAL WHERE MCODE='{0}'", (objs[0] as StorageDetail).MCode);
                DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                {
                    return 11; // 没有维护管控类型
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                {
                    IsParts = true;
                }
                else
                {
                    IsParts = false;
                }
            }
            sql = string.Format(@"SELECT {0} FROM TBLPICK WHERE pickno='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pick)), PickNo);
            object[] objs1 = this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
            if (objs1 == null)
            {
                return 2;  //没有数据
            }
            StorageDetail sto = objs[0] as StorageDetail;
            Pick pi = objs1[0] as Pick;
            if (sto.StorageCode != pi.StorageCode)
            {
                return 3;   //库位不同
            }
            sql = string.Format(@"SELECT {0} FROM TBLPICKDETAILMATERIAL WHERE PICKNO='{1}' AND CARTONNO='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterial)), PickNo, CartonNo);
            if (IsAll)
            {
                object[] objs3 = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
                if (objs3 != null)
                {
                    return 7;  //相同的箱号不能录入
                }
            }
            sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE DQMCODE='{1}' AND STATUS IN ('{2}','{3}') AND PICKNO='{4}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), sto.DQMCode, PickDetail_STATUS.Status_WaitPick, PickDetail_STATUS.Status_Pick, PickNo);
            object[] objs2 = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            if (objs2 == null)
            {
                //add by sam 2016年6月7日
                sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE DQMCODE='{1}' AND STATUS IN ('{2}') AND PICKNO='{3}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), sto.DQMCode, PickDetail_STATUS.Status_Owe, PickNo);
                object[] objs14 = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
                if (objs14 != null)
                {
                    return 14;
                }
                return 4;  // 鼎桥物料编码不同
            }
            PickDetail pid = objs2[0] as PickDetail;
            if (IsAll)
            {
                if (sto.AvailableQty == 0)
                {
                    return 12; //没有可用数量不能加箱
                }
                else if (sto.AvailableQty + pid.SQTY > pid.QTY)
                {
                    return 5;  //数量超出需求
                }
            }
            else
            {
                if (IsParts)
                {
                    if (1 + pid.SQTY > pid.QTY)
                    {
                        return 5;  //数量超出需求
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Qty))
                    {
                        return 10; //批量管控必须输入数量
                    }
                    else
                    {
                        if (Int32.Parse(Qty) > (objs[0] as StorageDetail).AvailableQty)
                        {
                            return 13;
                        }
                    }
                    if (int.Parse(Qty) + pid.SQTY > pid.QTY)
                    {
                        return 5;  //数量超出需求
                    }
                }
            }
            if (check)
            {
                sql = string.Format(@"SELECT COUNT(*) FROM TBLSTORAGEDETAIL WHERE LOTNO='{0}' AND STORAGEAGEDATE>{1}", sto.Lotno, sto.StorageAgeDate);
                int re = this.DataProvider.GetCount(new SQLCondition(sql));
                if (re > 0)
                {
                    return 6;//不符合先进先出规则
                }
            }

            return 0;
        }
        public object[] GetPickLine(string PickNo, string DQMcode)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE PICKNO='{1}' AND DQMCODE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), PickNo, DQMcode);
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));

        }
        public object GetPickLineByDQMcode(string PickNo, string DQMcode)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE PICKNO='{1}' AND DQMCODE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), PickNo, DQMcode);
            object[] objs = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
                return objs[0];
            return null;
        }


        public object[] GetStorageDetailSnbyCartonNo(string CartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLSTORAGEDETAILSN WHERE CARTONNO='{1}' AND PICKBLOCK<>'Y'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetailSN)), CartonNo);
            return this.DataProvider.CustomQuery(typeof(StorageDetailSN), new SQLCondition(sql));
        }

        public object[] GetStorageDetailSnbyCartonNoBlock(string CartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLSTORAGEDETAILSN WHERE CARTONNO='{1}' AND PICKBLOCK='Y'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetailSN)), CartonNo);
            return this.DataProvider.CustomQuery(typeof(StorageDetailSN), new SQLCondition(sql));
        }

        public StorloctransDetailSN[] GetStorloctransDetailSNs(string transNo, string fromCartonno)
        {
            string sql = string.Format(@"SELECT * FROM TBLSTORLOCTRANSDETAILSN WHERE TRANSNO='{0}' AND FROMCARTONNO='{1}'", transNo, fromCartonno);

            object[] objs = this.DataProvider.CustomQuery(typeof(StorloctransDetailSN), new SQLCondition(sql));
            List<StorloctransDetailSN> sns = new List<StorloctransDetailSN>();
            if (objs != null && objs.Length > 0)
            {
                foreach (StorloctransDetailSN sn in objs)
                {
                    sns.Add(sn);
                }
            }
            return sns.ToArray();

        }

        public int GetStorageDetailSNPickBlockCount(string CartonNo)
        {
            string sql = string.Format(@"SELECT Count(*) count  FROM TBLSTORAGEDETAILSN WHERE CARTONNO='{0}' AND PICKBLOCK='Y'", CartonNo);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public StorageDetailSN[] GetStorageDetailSNs(string CartonNo)
        {
            List<StorageDetailSN> sns = new List<StorageDetailSN>();
            string sql = string.Format(@"SELECT {0} FROM TBLSTORAGEDETAILSN WHERE CARTONNO='{1}' AND PICKBLOCK<>'Y'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetailSN)), CartonNo);
            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetailSN), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                foreach (StorageDetailSN sn in objs)
                {
                    sns.Add(sn);

                }
            }
            return sns.ToArray();
        }


        public object[] GetStorageDetailSnbyCartonNo(string CartonNo, string PickBlock)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLSTORAGEDETAILSN WHERE CARTONNO='{1}' AND PICKBLOCK='{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetailSN)), CartonNo, PickBlock);
            return this.DataProvider.CustomQuery(typeof(StorageDetailSN), new SQLCondition(sql));
        }


        public void UpdateStorageDetailSnbyCartonNo(string CartonNo, string PickBlock)
        {
            string sql = string.Format(@"update  TBLSTORAGEDETAILSN set  PICKBLOCK='{0}'  where  CARTONNO='{1}'  ", PickBlock, CartonNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void DeletePickDetailMaterialSNByCartonNo(string pickno, string cartonNo)
        {
            string sql = string.Format(@"delete  TBLPICKDETAILMATERIALSN  where   pickno='{0}'   and CARTONNO='{1}'  ", pickno, cartonNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        public object[] GetAllLineByPickNo(string PickNo, string STATUS)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE PICKNO='{1}' AND （STATUS<>'{2}' and STATUS<>'Cancel')", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), PickNo, STATUS);
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        }
        public object[] GetAllLineByPickNo(string PickNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE PICKNO='{1}' AND STATUS<>'{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), PickNo, PickDetail_STATUS.Status_ClosePick);
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        }
        //add by sam
        public object[] GetAllLineByPickNoNotCancel(string PickNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE PICKNO='{1}'  AND STATUS not in ( 'ClosePick', 'Cancel') ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), PickNo);
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        }
        public object[] GetPickLineByPickNoNotCancel(string PickNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLPICKDETAIL WHERE PICKNO='{1}'  AND STATUS not in ('Cancel') ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), PickNo);
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        }
        public object[] QueryInOutInfoByDQMCode(string StorageCode, string DQMCode, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLSTORAGEDETAIL WHERE STORAGECODE='{1}' AND DQMCODE ='{2}' AND AvailableQTY>0 ORDER BY StorageAgeDate", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetail)), StorageCode, DQMCode);
            return this.DataProvider.CustomQuery(typeof(StorageDetail), new PagerCondition(sql, inclusive, exclusive));
        }
        public int QueryInOutInfoByDQMCodeCount(string StorageCode, string DQMCode)
        {
            string sql = string.Format(@"SELECT COUNT(*) FROM TBLSTORAGEDETAIL WHERE STORAGECODE='{0}' AND DQMCODE ='{1}'", StorageCode, DQMCode);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion
        #region  Add by Amy @20160312  系统出库
        public object[] GetPickLineByPickNo(string PickNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLPICKDETAIL WHERE PICKNO='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), PickNo);
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        }
        public object[] GetPickDeMaterialByPickNo(string PickNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLPICKDETAILMATERIAL WHERE PICKNO='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterial)), PickNo);
            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
        }

        public CartonInvDetailMaterial[] GetCartonInvDetailMaterial(string PickNo, string DQMCode, string CARINVNO)
        {
            List<CartonInvDetailMaterial> ms = new List<CartonInvDetailMaterial>();
            string sql = string.Format("SELECT {0} FROM TBLCartonInvDetailMaterial WHERE PICKNO='{1}' and DQMCODE='{2}' and CARINVNO='{3}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(CartonInvDetailMaterial)), PickNo, DQMCode, CARINVNO);
            object[] ooo = this.DataProvider.CustomQuery(typeof(CartonInvDetailMaterial), new SQLCondition(sql));
            if (ooo != null && ooo.Length > 0)
            {
                foreach (CartonInvDetailMaterial m in ooo)
                {
                    ms.Add(m);
                }
            }
            return ms.ToArray();

        }

        public CartonInvDetailMaterial[] GetCartonInvDetailMaterialFromPickNoCarInv(string PickNo, string CARINVNO)
        {
            List<CartonInvDetailMaterial> ms = new List<CartonInvDetailMaterial>();
            string sql = string.Format("SELECT {0} FROM TBLCartonInvDetailMaterial WHERE PICKNO='{1}' and CARINVNO='{3}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(CartonInvDetailMaterial)), PickNo, CARINVNO);
            object[] ooo = this.DataProvider.CustomQuery(typeof(CartonInvDetailMaterial), new SQLCondition(sql));
            if (ooo != null && ooo.Length > 0)
            {
                foreach (CartonInvDetailMaterial m in ooo)
                {
                    ms.Add(m);
                }
            }
            return ms.ToArray();

        }

        public Pickdetailmaterial GetLotNoAndSupplierLotNO(string PickNo)
        {

            string sql = string.Format("SELECT {0} FROM TBLPICKDetailMaterial WHERE PICKNO='{1}' order by CDATE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterial)), PickNo);
            object[] ooo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            if (ooo != null && ooo.Length > 0)
            {
                foreach (Pickdetailmaterial m in ooo)
                {

                    return m;
                }
            }
            Pickdetailmaterial p = new Pickdetailmaterial();
            p.Lotno = " ";
            p.Supplier_lotno = " ";
            return p;

        }


        public string GetLocationCode(string stroageCode)
        {

            string sql = string.Format("SELECT {0} FROM TBLLOCATION  WHERE STORAGECODE ='{1}' order by locationcode ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Location)), stroageCode);
            object[] ooo = this.DataProvider.CustomQuery(typeof(Location), new SQLCondition(sql));
            if (ooo != null && ooo.Length > 0)
            {
                foreach (Location m in ooo)
                {
                    if (!string.IsNullOrEmpty(m.LocationCode))
                        return m.LocationCode;
                }
            }
            return " ";

        }

        public object[] GetPickDeMaterialSNByPickNoAndCartonNo(string PickNo, string CartonNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLPICKDETAILMATERIALSN WHERE PICKNO='{1}' AND CARTONNO='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterialsn)), PickNo, CartonNo);
            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterialsn), new SQLCondition(sql));
        }
        public object[] GetInvoicesDetailByInvNo(string InvNo)
        {
            string sql = string.Format("SELECT a.* FROM TBLINVOICESDETAIL a LEFT JOIN TBLInvoices b	ON a.INVNO=b.INVNO where b.DNBATCHNO ='{0}' ", InvNo);
            return this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
        }



        public string[] GetGFHWMcodeByPickNo(string pickno)
        {
            string sql = string.Format("SELECT  distinct a.GFHWITEMCODE  FROM TBLPICKDETAIL a WHERE A.PICKNO ='{0}' order by a.GFHWITEMCODE ", pickno);
            object[] obj = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            if (obj != null)
            {
                string[] str = new string[obj.Length];
                int i = 0;
                foreach (PickDetail pi in obj)
                {
                    str[i++] = pi.GFHWItemCode;
                }
                return str;
            }
            return null;
        }



        public string[] GetGFHWMcodeByInvNo(string InvNo)
        {
            string sql = string.Format("SELECT  distinct a.GFHWMCODE  FROM TBLINVOICESDETAIL a LEFT JOIN TBLInvoices b	ON a.INVNO=b.INVNO where b.DNBATCHNO ='{0}' ", InvNo);
            object[] obj = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (obj != null)
            {
                string[] str = new string[obj.Length];
                int i = 0;
                foreach (InvoicesDetail pi in obj)
                {
                    str[i++] = pi.GfhwmCode;
                }
                return str;
            }
            return null;
        }
        public object GetQtyByPickNoAndDQMCode(string PickNo, string DQMCode)
        {
            string sql = string.Format("SELECT NVL(SUM(OUTQTY),0) AS OUTQTY FROM TBLPICKDETAIL WHERE PICKNO='{0}' AND DQMCODE='{1}'", PickNo, DQMCode);
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql))[0];
        }
        #endregion
        #region Storagedetaillog
        /// <summary>
        /// TBLSTORAGEDETAILLOG
        /// </summary>
        public Storagedetaillog CreateNewStoragedetaillog()
        {
            return new Storagedetaillog();
        }

        public void AddStoragedetaillog(Storagedetaillog storagedetaillog)
        {
            this.DataProvider.Insert(storagedetaillog);
        }

        public void DeleteStoragedetaillog(Storagedetaillog storagedetaillog)
        {
            this.DataProvider.Delete(storagedetaillog);
        }

        public void UpdateStoragedetaillog(Storagedetaillog storagedetaillog)
        {
            this.DataProvider.Update(storagedetaillog);
        }

        public object GetStoragedetaillog(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Storagedetaillog), new object[] { Serial });
        }

        #endregion

        #region BarCode
        /// <summary>
        /// TBLBARCODE
        /// </summary>
        public BarCode CreateNewBarCode()
        {
            return new BarCode();
        }

        public void AddBarCode(BarCode barcode)
        {
            this.DataProvider.Insert(barcode);
        }

        public void DeleteBarCode(BarCode barcode)
        {
            this.DataProvider.Delete(barcode);
        }

        public void UpdateBarCode(BarCode barcode)
        {
            this.DataProvider.Update(barcode);
        }

        public object GetBarCode(string BarCode)
        {
            return this.DataProvider.CustomSearch(typeof(BarCode), new object[] { BarCode });
        }


        public object[] QueryBarCode(string barCode, string barCodeList, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT  {0} FROM TBLBARCODE  WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BarCode)));



            if (!string.IsNullOrEmpty(barCodeList))
            {
                sql += string.Format(" AND barCode in ({0}) ", barCodeList);
            }

            if (!string.IsNullOrEmpty(barCode))
            {
                sql += string.Format(" AND barCode ='{0}'", barCode);
            }

            return this.DataProvider.CustomQuery(typeof(BarCode), new PagerCondition(sql, "MDATE DESC,MTIME DESC", inclusive, exclusive));
        }

        public int QueryBarCodeCount(string barCode, string barCodeList)
        {
            string sql = @" SELECT COUNT(1) FROM TBLBARCODE WHERE 1=1 ";
            if (!string.IsNullOrEmpty(barCodeList))
            {
                sql += string.Format(" AND barCode in ({0}) ", barCodeList);
            }

            if (!string.IsNullOrEmpty(barCode))
            {
                sql += string.Format(" AND barCode ='{0}'", barCode);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion


        #region Storloctrans
        /// <summary>
        /// TBLSTORLOCTRANS
        /// </summary>
        public Storloctrans CreateNewStorloctrans()
        {
            return new Storloctrans();
        }

        public void AddStorloctrans(Storloctrans storloctrans)
        {
            this.DataProvider.Insert(storloctrans);
        }

        public void DeleteStorloctrans(Storloctrans storloctrans)
        {
            this.DataProvider.Delete(storloctrans);
        }

        public void UpdateStorloctrans(Storloctrans storloctrans)
        {
            this.DataProvider.Update(storloctrans);
        }

        public object GetStorloctrans(string Transno)
        {
            return this.DataProvider.CustomSearch(typeof(Storloctrans), new object[] { Transno });
        }

        public string[] QueryTransNo(string Status, string[] userGroups)
        {
            string sql = string.Format(@" SELECT {0} FROM TBLStorLocTrans WHERE Status='" + Status + "'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storloctrans)));
            sql += @"AND TRANSNO IN(SELECT DISTINCT TRANSNO FROM
 (SELECT TRANSNO FROM TBLStorLocTrans WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @")) UNION 
 SELECT TRANSNO FROM TBLStorLocTrans WHERE FROMSTORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE IN(" + SqlFormat(userGroups) + @"))) T  )
 ";
            object[] objs = this.DataProvider.CustomQuery(typeof(Storloctrans), new SQLCondition(sql));
            string[] str;
            if (objs != null)
            {
                str = new string[objs.Length];
                int i = 0;
                foreach (Storloctrans storloctrans in objs)
                {
                    str[i] = storloctrans.Transno;
                    i++;
                }
                return str;
            }
            return null;
        }

        //转储单查询
        public object[] QueryStorloctrans(string TransNo, string FstorageCode, string TstorageCode, int bCDate, int eCDate, string invno, string transtype, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT  {0} FROM TBLStorLocTrans  WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storloctrans)));

            if (!string.IsNullOrEmpty(TransNo))
            {
                sql += string.Format(" AND TRANSNO like '{0}%'", TransNo);
            }

            if (!string.IsNullOrEmpty(FstorageCode))
            {
                sql += string.Format(@" AND FROMSTORAGECODE IN ('{0}')", FstorageCode);
            }
            if (!string.IsNullOrEmpty(TstorageCode))
            {
                sql += string.Format(@" AND STORAGECODE IN ('{0}')", TstorageCode);
            }

            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(@" AND invno  like '{0}%'", invno);
            }
            sql += string.Format(@" and transtype= '{0}'  ", transtype);
            return this.DataProvider.CustomQuery(typeof(Storloctrans), new PagerCondition(sql, "MDATE DESC,MTIME DESC", inclusive, exclusive));
        }

        public int QueryStorloctransCount(string TransNo, string FstorageCode, string TstorageCode, int bCDate, int eCDate, string invno, string transtype)
        {
            string sql = string.Format(@" SELECT  count(*) FROM TBLStorLocTrans  WHERE 1=1 ");

            if (!string.IsNullOrEmpty(TransNo))
            {
                sql += string.Format(" AND TRANSNO like '{0}%'", TransNo);
            }

            if (!string.IsNullOrEmpty(FstorageCode))
            {
                sql += string.Format(@" AND FROMSTORAGECODE IN ('{0}')", FstorageCode);
            }
            if (!string.IsNullOrEmpty(TstorageCode))
            {
                sql += string.Format(@" AND STORAGECODE IN ('{0}')", TstorageCode);
            }

            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(@" AND invno  like '{0}%'", invno);
            }
            sql += string.Format(@" and transtype= '{0}'  ", transtype);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region StorloctransDetail
        /// <summary>
        /// TBLSTORLOCTRANSDETAIL
        /// </summary>
        public StorloctransDetail CreateNewStorloctransdetail()
        {
            return new StorloctransDetail();
        }

        public void AddStorloctransdetail(StorloctransDetail storloctransdetail)
        {
            this.DataProvider.Insert(storloctransdetail);
        }

        public void DeleteStorloctransdetail(StorloctransDetail storloctransdetail)
        {
            this.DataProvider.Delete(storloctransdetail);
        }

        public void UpdateStorloctransdetail(StorloctransDetail storloctransdetail)
        {
            this.DataProvider.Update(storloctransdetail);
        }

        public object GetStorloctransdetail(string Transno, string MCode)
        {
            return this.DataProvider.CustomSearch(typeof(StorloctransDetail), new object[] { Transno, MCode });
        }

        public object[] QueryDetailBYNo(string transNo)
        {
            string sql = "select a.* from tblstorloctransdetail a where a.TRANSNO = '" + transNo + "' ";
            return this.DataProvider.CustomQuery(typeof(StorloctransDetail), new SQLCondition(sql));
        }

        //转储单明细查询
        public object[] QueryStorloctransDetail(string TransNo, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT a.*,nvl(b.transqty,0) as transqty
                                          FROM     tblstorloctransdetail a
                                               LEFT JOIN
                                                   (SELECT nvl(SUM (m.qty),0) AS transqty, m.transno, m.mcode
                                                      FROM     tblstorloctransdetailcarton m
                                                           INNER JOIN
                                                               tblstorloctransdetail n
                                                           ON m.transno = n.transno AND m.mcode = n.mcode
                                                      where m.transno = '{0}'
                                                    GROUP BY m.transno, m.mcode) b
                                               ON b.transno = a.transno AND b.mcode = a.mcode
                                               where a.transno = '{0}'", TransNo);

            return this.DataProvider.CustomQuery(typeof(StorloctransDetailEX), new PagerCondition(sql, "a.mdate desc,a.mtime desc", inclusive, exclusive));
        }

        public int QueryStorloctransDetailCount(string TransNo)
        {
            string sql = string.Format(@"SELECT count(*)
                                          FROM     tblstorloctransdetail a
                                               LEFT JOIN
                                                   (SELECT nvl(SUM (m.qty),0) AS transqty, m.transno, m.mcode
                                                      FROM     tblstorloctransdetailcarton m
                                                           INNER JOIN
                                                               tblstorloctransdetail n
                                                           ON m.transno = n.transno AND m.mcode = n.mcode
                                                      where m.transno = '{0}'
                                                    GROUP BY m.transno, m.mcode) b
                                               ON b.transno = a.transno AND b.mcode = a.mcode
                                               where a.transno = '{0}'", TransNo);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region StorloctransDetailCarton
        /// <summary>
        /// TBLSTORLOCTRANSDETAILCARTON
        /// </summary>
        public StorloctransDetailCarton CreateNewStorloctransdetailcarton()
        {
            return new StorloctransDetailCarton();
        }

        public void AddStorloctransdetailcarton(StorloctransDetailCarton storloctransdetailcarton)
        {
            this.DataProvider.Insert(storloctransdetailcarton);
        }

        public void DeleteStorloctransdetailcarton(StorloctransDetailCarton storloctransdetailcarton)
        {
            this.DataProvider.Delete(storloctransdetailcarton);
        }

        public void UpdateStorloctransdetailcarton(StorloctransDetailCarton storloctransdetailcarton)
        {
            this.DataProvider.Update(storloctransdetailcarton);
        }

        public object GetStorloctransdetailcarton(string Transno, string Fromcartonno, string Cartonno)
        {
            return this.DataProvider.CustomSearch(typeof(StorloctransDetailCarton), new object[] { Transno, Fromcartonno, Cartonno });
        }


        #region 货位移动

        public void UpdateStorloctransdetailcarton(string TransNo, string Fromcartonno, string tLocationCartonNo, string locationCode)
        {
            string sql = string.Format(@"update  tblstorloctransdetailcarton set TransNo='{0}' , CARTONNO='{1}', 
             LOCATIONCODE='{2}' where 1=1 and TRANSNO = '{0}' and Fromcartonno='{3}' and CARTONNO='Move' ", TransNo, tLocationCartonNo, locationCode, Fromcartonno);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }



        public void UpdateStorageDetailSN(string tLocationCartonNo, string Fromcartonno)
        {
            string sql = string.Format(@"update  TBLSTORAGEDETAILSN set  CARTONNO='{0}' 
                  where 1=1 and CARTONNO = '{1}'   ", tLocationCartonNo, Fromcartonno);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        #endregion

        #region Update
        public void UpdateStorloctransdetailcarton(string TransNo, string tLocationCartonNo, string locationCode)
        {
            string sql = string.Format(@"update  tblstorloctransdetailcarton set TransNo='{0}' , CARTONNO='{1}',
              LOCATIONCODE='{2}' where 1=1 and TRANSNO = '{0}'  ", TransNo, tLocationCartonNo, locationCode);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        public void UpdateStorloctransdetailcarton1(string TransNo, string tLocationCartonNo, string locationCode, string fromCartonno)
        {
            string sql = string.Format(@"update  tblstorloctransdetailcarton set CARTONNO='{1}',
              LOCATIONCODE='{2}' where 1=1 and TRANSNO = '{0}' and fromcartonno='{3}' ", TransNo, tLocationCartonNo, locationCode, fromCartonno);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateStorloctransDetailSN(string TransNo, string tCartonNo, string fromCartonNo)
        {
            string sql = string.Format(@"update  TBLSTORLOCTRANSDETAILSN set  CARTONNO='{1}'   where 1=1 and TRANSNO = '{0}' and fromcartonno='{2}' ", TransNo, tCartonNo, fromCartonNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateStorloctransDetailSN(string TransNo, string tCartonNo)
        {
            string sql = string.Format(@"update  TBLSTORLOCTRANSDETAILSN set  CARTONNO='{1}'   where 1=1 and TRANSNO = '{0}'  ", TransNo, tCartonNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateStorloctransDetailSN1(string TransNo, string tCartonNo, string fromCartonNo)
        {
            string sql = string.Format(@"update  TBLSTORLOCTRANSDETAILSN set CARTONNO='{1}'  
              where 1=1 and TRANSNO = '{0}' and FROMCARTONNO='{2}' ", TransNo, tCartonNo, fromCartonNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        #endregion


        public object[] GetStorloctransdetailcarton(string TransNo, string MCode)
        {
            string sql = @"SELECT {0} FROM  tblstorloctransdetailcarton where 1=1 and TRANSNO = '{1}' and MCODE = '{2}'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailCarton)), TransNo, MCode);
            return this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));
        }

        public object[] GetStorloctransdetailcarton(string TransNo)
        {
            string sql = @"SELECT {0} FROM  tblstorloctransdetailcarton where 1=1 and TRANSNO = '{1}'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailCarton)), TransNo);
            return this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));
        }



        public object[] GetStorloctransdetailSameCarton(string TransNo)
        {
            string sql = @"SELECT {0} FROM  tblstorloctransdetailcarton where 1=1 and TRANSNO = '{1}' and  cartonno=fromcartonno ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailCarton)), TransNo);
            return this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));
        }

        public DataTable GetFromCartonNoAndSQTY(string TransNo)
        {
            string sql = @"select FromCARTONNO, sum(QTY) as sqty FROM TBLStorLocTransDetailCarton WHERE TRANSNO = '{0}' group by FromCARTONNO";
            sql = string.Format(sql, TransNo);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        public DataTable GetToCartonNoAndSQTY(string TransNo)
        {
            string sql = @"select CARTONNO, sum(QTY) as sqty FROM TBLStorLocTransDetailCarton WHERE TRANSNO = '{0}' group by  CARTONNO";
            sql = string.Format(sql, TransNo);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable GetCartonNoAndSQTY(string TransNo)
        {
            string sql = @"select * FROM TBLStorLocTransDetailCarton WHERE TRANSNO = '{0}' group by CARTONNO";
            sql = string.Format(sql, TransNo);
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public object[] GetStorloctransdetailcartons(string TransNo, string CartonNo)
        {
            string sql = @"SELECT {0} FROM  tblstorloctransdetailcarton where 1=1 and TRANSNO = '{1}'";
            if (!string.IsNullOrEmpty(CartonNo))
            {
                sql += " and CartonNo = '{2}'";
                sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailCarton)), TransNo, CartonNo);
            }
            else
            {
                sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailCarton)), TransNo);
            }

            return this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));
        }


        //public StorloctransDetailCarton[] GetStorLoctransDetailCartonsMergeByCartonDQMCode(string transNo, string toCartonno)
        //{

        //    string sql = @"SELECT min(lotno) lotno,min(faccode) faccode,min(locationcode) locationcode, MIN(Production_Date) Production_Date FROM  tblstorloctransdetailcarton where 1=1 and TRANSNO = '{1}' GROUP BY CARTONNO , DQMCODE,MCODE";
        //    if (!string.IsNullOrEmpty(CartonNo))
        //    {
        //        sql += " and CartonNo = '{2}'";
        //        sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailCarton)), TransNo, CartonNo);
        //    }
        //    else
        //    {
        //        sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailCarton)), TransNo);
        //    }

        //    return this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));
        //}
        public object[] GetStorloctransdetail(string TransNo)
        {
            string sql = string.Format(@"SELECT {0} FROM  tblstorloctransdetail where TRANSNO = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetail)), TransNo);
            return this.DataProvider.CustomQuery(typeof(StorloctransDetail), new SQLCondition(sql));
        }


        public object[] GetStorloctransdetailExCludeCancel(string TransNo)
        {
            string sql = @"SELECT {0} FROM  tblstorloctransdetail where 1=1 and TRANSNO = '{1}' and status<>'Cancel'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetail)), TransNo);
            return this.DataProvider.CustomQuery(typeof(StorloctransDetail), new SQLCondition(sql));
        }

        public object[] GetStorloctransdetailsn(string TransNo)
        {
            string sql = string.Format(@"SELECT {0} FROM tblstorloctransdetailsn where TRANSNO = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailSN)), TransNo);
            return this.DataProvider.CustomQuery(typeof(StorloctransDetailSN), new SQLCondition(sql));
        }

        //转储单明细箱号查询 edit by sam mcode->DQMcode
        public object[] QueryStorloctransDetailC(string TransNo, string DQMcode, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT {0} FROM  tblstorloctransdetailcarton where  TRANSNO = '{1}' AND DQMcode = '{2}'
                                               ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailCarton)), TransNo, DQMcode);

            return this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new PagerCondition(sql, "mdate desc,mtime desc", inclusive, exclusive));
        }

        public int QueryStorloctransDetailCCount(string TransNo, string DQMcode)
        {
            string sql = string.Format(@"SELECT count(*) FROM  tblstorloctransdetailcarton where  TRANSNO = '{0}' AND DQMcode = '{1}'", TransNo, DQMcode);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region StorloctransDetailSN
        /// <summary>
        /// TBLSTORLOCTRANSDETAILSN
        /// </summary>
        public StorloctransDetailSN CreateNewStorloctransdetailsn()
        {
            return new StorloctransDetailSN();
        }

        public void AddStorloctransdetailsn(StorloctransDetailSN storloctransdetailsn)
        {
            this.DataProvider.Insert(storloctransdetailsn);
        }

        public void DeleteStorloctransdetailsn(StorloctransDetailSN storloctransdetailsn)
        {
            this.DataProvider.Delete(storloctransdetailsn);
        }

        public void UpdateStorloctransdetailsn(StorloctransDetailSN storloctransdetailsn)
        {
            this.DataProvider.Update(storloctransdetailsn);
        }

        public object GetStorloctransdetailsn(string Transno, string Sn)
        {
            string sql = string.Format(@"SELECT {0} FROM tblstorloctransdetailsn where TRANSNO = '{1}' and Sn='{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorloctransDetailSN)), Transno, Sn);
            return this.DataProvider.CustomQuery(typeof(StorloctransDetailSN), new SQLCondition(sql));
        }

        //转储单明细条码查询
        public object[] QueryStorloctransDetailSN(string TransNo, string FCarton, string TCarton, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT c.*, a.sn
                                                  FROM tblstorloctransdetailsn a
                                                       INNER JOIN tblstorloctransdetailcarton b
                                                           ON     b.transno = a.transno
                                                              AND b.fromcartonno = a.fromcartonno
                                                              AND b.cartonno = a.cartonno
                                                       INNER JOIN tblstorloctransdetail c
                                                           ON c.transno = b.transno AND c.mcode = b.mcode
                                                 WHERE a.transno = '{0}' 
                                                AND a.fromcartonno = '{1}' 
                                                AND a.cartonno = '{2}' ", TransNo, FCarton, TCarton);

            return this.DataProvider.CustomQuery(typeof(StorloctransDetailEX), new PagerCondition(sql, "C.mdate desc,C.mtime desc", inclusive, exclusive));
        }
        public int QueryStorloctransDetailSNCount(string TransNo, string FCarton, string TCarton)
        {
            string sql = string.Format(@"SELECT COUNT(*)
                                              FROM tblstorloctransdetailsn a
                                                   INNER JOIN tblstorloctransdetailcarton b
                                                       ON     b.transno = a.transno
                                                          AND b.fromcartonno = a.fromcartonno
                                                          AND b.cartonno = a.cartonno
                                                   INNER JOIN tblstorloctransdetail c
                                                       ON c.transno = b.transno AND c.mcode = b.mcode
                                             WHERE a.transno = '{0}' 
                                            AND a.fromcartonno = '{1}' 
                                            AND a.cartonno = '{2}' ", TransNo, FCarton, TCarton);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion


        public object[] QueryPackageReceipts(string carInvNo, string pickNo, string CARTONNO, string pickType, string itemName, string orderNo, int bCDate, int eCDate,
            string chk, string[] userGroups, int inclusive, int exclusive)
        {
            string sql = @" SELECT c.CARINVNO CARINVNO,c.PICKNO PICKNO,a.STATUS STATUS,
            a.PICKTYPE PICKTYPE,
            a.INVNO INVNO,a.ORDERNO ORDERNO,a.StorageCode StorageCode,a.ReceiverUser ReceiverUser,a.ReceiverAddr ReceiverAddr,a.Plan_Date Plan_Date ,
            a.PLANGIDATE PLANGIDATE,a.GFFLAG GFFLAG,a.OANO OANO,a.Packing_List_Date Packing_List_Date,a.Packing_List_Time Packing_List_Time,
            a.Shipping_Mark_Date Shipping_Mark_Date,a.Shipping_Mark_Time Shipping_Mark_Time,c.GROSS_WEIGHT GROSS_WEIGHT,c.VOLUME VOLUME FROM TBLCartonInvoices c 
            LEFT JOIN TBLPICK a on c.pickno=a.pickno where 1=1 AND c.PICKNO IN (SELECT DISTINCT PICKNO FROM
 (SELECT PICKNO FROM TBLPICK WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @"))) T) ";
            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND c.CARINVNO LIKE '%{0}%'", carInvNo);
            }

            if (!string.IsNullOrEmpty(chk))
            {
                //TODO：去除最后的 ,
                if (chk.EndsWith(","))
                {
                    chk = chk.TrimEnd(',');
                }
                sql += string.Format(@" AND   a.STATUS  in ({0})", chk);
            }

            //add by sam
            if (!string.IsNullOrEmpty(CARTONNO))
            {
                sql += string.Format(" AND c.CARINVNO in ( select CARINVNO from  TBLCartonInvDetail  where CARTONNO LIKE '%{0}%' ) ", CARTONNO);
            }
            //add end
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND c.PICKNO LIKE '%{0}%'", pickNo);
            }

            if (!string.IsNullOrEmpty(pickType))
            {
                sql += string.Format(" AND a.PICKTYPE = '{0}'", pickType);
            }

            if (!string.IsNullOrEmpty(itemName))
            {
                sql += string.Format(" AND c.ProjectName LIKE '%{0}%'", itemName);
            }
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql += string.Format(" AND a.ORDERNO LIKE '%{0}%'", orderNo);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND c.CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND c.CDATE <= {0}", eCDate);
            }


            return this.DataProvider.CustomQuery(typeof(PickInfo), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", inclusive, exclusive));
        }

        public string GetGFCONTRACTNO(string pickNo)
        {
            string sql = "select a.*  from TBLPICKDETAIL a where a.PICKNO='" + pickNo + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder(50);
            if (o != null && o.Length > 0)
            {
                foreach (PickDetail p in o)
                {
                    if (!string.IsNullOrEmpty(p.GFContractNo))
                    {
                        list.Add(p.GFContractNo);
                        list.Add(",");
                    }

                }
            }

            return sb.ToString().Trim(',');
        }


        //public object[] QueryPackageReceipts1(string carInvNo, string pickNo, string pickType, string itemName, string orderNo, int bCDate, int eCDate, string GFFLAG)
        //{
        //    string sql = @" SELECT a.CUSBATCHNO, c.CARINVNO CARINVNO,c.PICKNO PICKNO,c.STATUS STATUS,a.INVNO INVNO,a.ORDERNO ORDERNO,a.StorageCode StorageCode,a.ReceiverUser ReceiverUser,a.ReceiverAddr ReceiverAddr,a.Plan_Date Plan_Date ,a.PLANGIDATE PLANGIDATE,b.GFCONTRACTNO GFCONTRACTNO,a.GFFLAG GFFLAG,a.OANO OANO,a.Packing_List_Date Packing_List_Date,a.Packing_List_Time Packing_List_Time,a.Shipping_Mark_Date Shipping_Mark_Date,a.Shipping_Mark_Time Shipping_Mark_Time,c.GROSS_WEIGHT GROSS_WEIGHT,c.VOLUME VOLUME,d.CARTONNO,b.GFHWITEMCODE,b.GFPACKINGSEQ,e.PICKLINE,e.SN,b.PICKLINE P22,b.VENDERITEMCODE VENDERITEMCODE,b.DQMCODE DQMCODE,b.HWTYPEINFO HWTYPEINFO,b.MDESC MDESC,b.QTY QTY,b.UNIT UNIT,b.CUSITEMDESC CUSITEMDESC FROM TBLCartonInvoices c LEFT JOIN TBLPICK a on c.pickno=a.pickno LEFT join TBLPICKDETAIL b on c.pickno=b.pickno left join TBLPICKDetailMaterial d on c.pickno=d.pickno left join TBLPICKDetailMaterialSN e on c.pickno=e.pickno where 1=1 ";
        //    if (!string.IsNullOrEmpty(carInvNo))
        //    {
        //        sql += string.Format(" AND c.CARINVNO LIKE '%{0}%'", carInvNo);
        //    }

        //    if (!string.IsNullOrEmpty(pickNo))
        //    {
        //        sql += string.Format(" AND c.PICKNO LIKE '%{0}%'", pickNo);
        //    }

        //    if (!string.IsNullOrEmpty(pickType))
        //    {
        //        sql += string.Format(" AND a.PICKTYPE = '{0}'", pickType);
        //    }

        //    if (!string.IsNullOrEmpty(itemName))
        //    {
        //        sql += string.Format(" AND c.ProjectName LIKE '%{0}%'", itemName);
        //    }
        //    if (!string.IsNullOrEmpty(orderNo))
        //    {
        //        sql += string.Format(" AND a.ORDERNO LIKE '%{0}%'", orderNo);
        //    }
        //    if (bCDate > 0)
        //    {
        //        sql += string.Format(@" AND a.CDATE >= {0}", bCDate);
        //    }
        //    if (eCDate > 0)
        //    {
        //        sql += string.Format(@" AND a.CDATE <= {0}", eCDate);
        //    }
        //    if (!string.IsNullOrEmpty(GFFLAG))
        //    {
        //        sql += string.Format(" AND a.GFFLAG ='{0}'", GFFLAG);
        //    }


        //    return this.DataProvider.CustomQuery(typeof(PickInfo1), new SQLCondition(sql));
        //}

        public int QueryPackageReceiptsCount(string carInvNo, string pickNo, string CARTONNO, string pickType, string itemName, string orderNo, int bCDate, int eCDate, string chk, string[] userGroups)
        {
            string sql = @" SELECT count(*) FROM TBLCartonInvoices c LEFT JOIN TBLPICK a on c.pickno=a.pickno where 1=1 AND c.PICKNO IN (SELECT DISTINCT PICKNO FROM
 (SELECT PICKNO FROM TBLPICK WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @")) ) T) ";
            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND c.CARINVNO LIKE '%{0}%'", carInvNo);
            }

            if (!string.IsNullOrEmpty(chk))
            {
                //TODO：去除最后的 ,
                if (chk.EndsWith(","))
                {
                    chk = chk.TrimEnd(',');
                }
                sql += string.Format(@" AND   a.STATUS  in ({0})", chk);
            }

            //add by sam
            if (!string.IsNullOrEmpty(CARTONNO))
            {
                sql += string.Format(" AND c.CARINVNO in ( select CARINVNO from  TBLCartonInvDetail  where CARTONNO LIKE '%{0}%' ) ", CARTONNO);
            }
            //add end
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND c.PICKNO LIKE '%{0}%'", pickNo);
            }

            if (!string.IsNullOrEmpty(pickType))
            {
                sql += string.Format(" AND a.PICKTYPE = '{0}'", pickType);
            }

            if (!string.IsNullOrEmpty(itemName))
            {
                sql += string.Format(" AND c.ProjectName LIKE '%{0}%'", itemName);
            }
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql += string.Format(" AND a.ORDERNO LIKE '%{0}%'", orderNo);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND c.CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND c.CDATE <= {0}", eCDate);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public object[] QueryPackageReceiptsDetails(string carInvNo, string pickNo, string cartonNO, string DQMCode, string huiweiCode, int inclusive, int exclusive)
        {
            string sql = @" SELECT b.CARTONNO,a.GFHWITEMCODE,a.HWCODEQTY,a.CUSITEMDESC
            ,b.DQMCODE,a.MDESC,a.CUSITEMSPEC,b.QTY,b.UNIT,a.VENDERITEMCODE,a.PACKINGWAYNO FROM TBLCartonInvoices c ,
            TBLPICKDETAIL a ,TBLCartonInvDetailMaterial b where c.pickno=a.pickno and c.carinvno=b.carinvno 
            and a.dqmcode=b.dqmcode
            and  c.PICKNO= '" + pickNo + "' and c.carinvno='" + carInvNo + "'";
            if (!string.IsNullOrEmpty(cartonNO))
            {

                sql += string.Format(" AND b.CARTONNO ='{0}'", cartonNO);
            }

            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND b.DQMCODE LIKE'%{0}%'", DQMCode);
            }

            if (!string.IsNullOrEmpty(huiweiCode))
            {
                sql += string.Format(" AND a.GFHWITEMCODE LIKE '%{0}%'", huiweiCode);
            }


            return this.DataProvider.CustomQuery(typeof(SendCaseDetails), new PagerCondition(sql, "c.MDATE DESC,c.MTIME DESC", inclusive, exclusive));
        }

        public int QueryPackageReceiptsDetailsCount(string carInvNo, string pickNo, string cartonNO, string DQMCode, string huiweiCode)
        {
            string sql = @" SELECT COUNT(*) FROM TBLCartonInvoices c ,TBLPICKDETAIL a ,TBLCartonInvDetailMaterial b where c.pickno=a.pickno 
                and c.carinvno=b.carinvno and a.dqmcode=b.dqmcode
                and  c.PICKNO= '" + pickNo + "' and c.carinvno='" + carInvNo + "'";
            if (!string.IsNullOrEmpty(cartonNO))
            {

                sql += string.Format(" AND b.CARTONNO ='{0}'", cartonNO);
            }

            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND b.DQMCODE LIKE'%{0}%'", DQMCode);
            }

            if (!string.IsNullOrEmpty(huiweiCode))
            {
                sql += string.Format(" AND a.GFHWITEMCODE LIKE '%{0}%'", huiweiCode);
            }


            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public object[] QueryPackageReceiptsDetailsSN(string carInvNo, string pickNo, string cartonNO, string DQMCode, int inclusive, int exclusive)
        {
            //string sql = @" SELECT b.CARTONNO,a.GFHWITEMCODE,a.HWCODEQTY,a.CUSITEMDESC,b.DQMCODE,a.MDESC,a.CUSITEMSPEC,b.QTY,b.UNIT,a.VENDERITEMCODE,a.PACKINGWAYNO,d.SN FROM TBLCartonInvDetailSN d,TBLCartonInvoices c,TBLPICKDETAIL a ,TBLCartonInvDetailMaterial b where c.pickno=a.pickno and c.carinvno=b.carinvno and d.carinvno=c.carinvno and  c.PICKNO= '" + pickNo + "' and c.carinvno='" + carInvNo + "'";
            string sql = string.Format(@"SELECT d.sn,a.GFHWITEMCODE,a.HWCODEQTY,a.CUSITEMDESC,b.DQMCODE,a.MDESC,
                a.CUSITEMSPEC,d.CARTONNO,b.UNIT,a.VENDERITEMCODE,a.PACKINGWAYNO,c.carinvno 
                 FROM TBLCartonInvoices c,TBLPICKDETAIL a ,TBLCartonInvDetailMaterial b 
                ,TBLCartonInvDetailSN d 
                where 
                c.pickno=a.pickno   and c.carinvno=b.carinvno  and d.carinvno=c.carinvno and d.cartonno=b.cartonno 
                AND a.mcode=b.mcode and a.dqmcode=b.dqmcode
                and  c.PICKNO= '{0}' and c.carinvno='{1}' ", pickNo, carInvNo);

            if (!string.IsNullOrEmpty(cartonNO))
            {

                sql += string.Format(" AND b.CARTONNO ='{0}'", cartonNO);
            }

            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND b.DQMCODE LIKE'%{0}%'", DQMCode);
            }



            return this.DataProvider.CustomQuery(typeof(SendCaseDetails), new PagerCondition(sql, "c.MDATE DESC,c.MTIME DESC", inclusive, exclusive));
        }

        public int QueryPackageReceiptsDetailsSNCount(string carInvNo, string pickNo, string cartonNO, string DQMCode)
        {
            //string sql = @" SELECT COUNT(*) FROM TBLCartonInvDetailSN d,TBLCartonInvoices c,TBLPICKDETAIL a ,TBLCartonInvDetailMaterial b where c.pickno=a.pickno and c.carinvno=b.carinvno and d.carinvno=c.carinvno and  c.PICKNO= '" + pickNo + "' and c.carinvno='" + carInvNo + "'";

            string sql = string.Format(@"SELECT count(1)
                 FROM TBLCartonInvoices c,TBLPICKDETAIL a ,TBLCartonInvDetailMaterial b 
                ,TBLCartonInvDetailSN d 
                where 
                c.pickno=a.pickno   and c.carinvno=b.carinvno  and d.carinvno=c.carinvno and d.cartonno=b.cartonno 
                AND a.mcode=b.mcode and a.dqmcode=b.dqmcode
                and  c.PICKNO= '{0}' and c.carinvno='{1}' ", pickNo, carInvNo);
            if (!string.IsNullOrEmpty(cartonNO))
            {

                sql += string.Format(" AND b.CARTONNO ='{0}'", cartonNO);
            }

            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND b.DQMCODE LIKE'%{0}%'", DQMCode);
            }



            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        #region 包装作业 add by Amy @20160318
        public object[] GetAllPickDetailMaterialByPickNoAndLine(string PickNo, string PickLine)
        {
            string sql = "SELECT {0} FROM TBLPICKDETAILMATERIAL WHERE PICKNO='{1}' AND PICKLINE='{2}'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterial)), PickNo, PickLine);
            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
        }
        public object[] GetAllPickDetailMaterialByPickNoAndDQMCode(string PickNo, string DQMCode)
        {
            string sql = "SELECT {0} FROM TBLPICKDETAILMATERIAL WHERE PICKNO='{1}' AND DQMCode='{2}'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterial)), PickNo, DQMCode);
            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
        }
        public object[] GetAllPickDetailByPickNoAndDQMCode(string PickNo, string DQMCode)
        {
            string sql = "SELECT {0} FROM TBLPICKDETAIL WHERE PICKNO='{1}' AND DQMCODE='{2}'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(PickDetail)), PickNo, DQMCode);
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        }
        public object[] GetPickDetailMaterialSN(string PickNo, string CartonNo)
        {
            string sql = "SELECT {0} FROM TBLPickDetailMaterialSN WHERE PICKNO='{1}' AND CARTONNO='{2}'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pickdetailmaterialsn)), PickNo, CartonNo);
            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterialsn), new SQLCondition(sql));
        }
        public object[] GetCartonInvDetailMaterial(string carInvNo, string CartonNo)
        {
            string sql = "SELECT {0} FROM TBLCARTONINVDETAILMATERIAL WHERE  CARTONNO='{1}' AND CARINVNO='{2}'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(CartonInvDetailMaterial)), CartonNo, carInvNo);
            return this.DataProvider.CustomQuery(typeof(CartonInvDetailMaterial), new SQLCondition(sql));
        }
        #endregion
        #region 包装作业 add by Chris.H.Wang @20160305
        public object[] GetMaterialInfoByQDMCode(string DQMCode)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLMATERIAL WHERE DQMCODE='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.MOModel.Material)), DQMCode);
            return this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
        }
        public object GetCartonInvoices(string pickNo)
        {
            string sql = @" SELECT a.* 
                            FROM TBLCartonInvoices a
                            WHERE a.PICKNO = '" + pickNo + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(CARTONINVOICES), new PagerCondition(sql, "a.MDATE DESC, a.MTIME DESC", 1, int.MaxValue));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }

            return null;
        }

        public object[] QueryPackagingOperations(string pickNo, string CARTONNO, int inclusive, int exclusive)
        {
            string sql = @" SELECT a.CARINVNO, a.CARTONNO, a.DQMCODE, a.GFHWITEMCODE, a.GFPACKINGSEQ, b.SQTY, a.QTY,a.DQSMCODE
                            FROM TBLCartonInvDetailMaterial a, TBLPICKDetail b
                            WHERE a.PICKNO = b.PICKNO AND a.PICKLINE = b.PICKLINE";
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO like '%{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(CARTONNO))
            {
                sql += string.Format(" AND a.CARTONNO like '%{0}'", CARTONNO);
            }
            return this.DataProvider.CustomQuery(typeof(PackagingOperations), new PagerCondition(sql, "a.MDATE DESC, a.MTIME DESC", inclusive, exclusive));
        }

        public int QueryPackagingOperationsCount(string pickNo, string CARTONNO)
        {
            string sql = @" SELECT COUNT(1)
                            FROM TBLCartonInvDetailMaterial a, TBLPICKDetail b
                            WHERE a.PICKNO = b.PICKNO AND a.PICKLINE = b.PICKLINE";
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO like '%{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(CARTONNO))
            {
                sql += string.Format(" AND a.CARTONNO like '%{0}'", CARTONNO);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryPackagingOperationsSN(string cartonNo, string sn, int inclusive, int exclusive)
        {
            string sql = @" select d.CARTONNO, c.DQMCODE, c.CUSTMCODE, c.GFHWITEMCODE, c.GFPACKINGSEQ, d.SN,d.carinvno
                              from TBLPICKDETAIL c
                             inner join (select a.CARTONNO, a.SN, a.PICKNO, a.pickline,a.carinvno
                                           from TBLCartonInvDetailSN a
                                          inner join TBLPICKDETAILMATERIALSN b
                                             on a.SN = b.SN and a.pickno=b.pickno) d
                                on c.PICKNO = d.PICKNO
                               and c.PICKLINE = d.pickline
                             where d.CARTONNO = '" + cartonNo + "'";
            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(@"AND d.sn='{0}'", sn);
            }
            return this.DataProvider.CustomQuery(typeof(PackagingOperations), new PagerCondition(sql, "c.MDATE DESC, c.MTIME DESC", inclusive, exclusive));
        }

        public object[] QueryPackagingOperationsSN1(string cartonNo)
        {
            string sql = @" select d.CARTONNO, c.DQMCODE, c.CUSTMCODE, c.GFHWITEMCODE, c.GFPACKINGSEQ, d.SN,d.carinvno
                              from TBLPICKDETAIL c
                             inner join (select a.CARTONNO, a.SN, a.PICKNO, a.pickline,a.carinvno
                                           from TBLCartonInvDetailSN a
                                          inner join TBLPICKDETAILMATERIALSN b
                                             on a.SN = b.SN) d
                                on c.PICKNO = d.PICKNO
                               and c.PICKLINE = d.pickline
                             where d.CARTONNO = '" + cartonNo + "'";

            return this.DataProvider.CustomQuery(typeof(PackagingOperations), new SQLCondition(sql));
        }

        public int QueryPackagingOperationsSNCount(string cartonNo, string sn)
        {
            string sql = @" SELECT COUNT(1)
                              from TBLPICKDETAIL c
                             inner join (select a.CARTONNO, a.SN, a.PICKNO, a.pickline
                                           from TBLCartonInvDetailSN a
                                          inner join TBLPICKDETAILMATERIALSN b
                                             on a.SN = b.SN) d
                                on c.PICKNO = d.PICKNO
                               and c.PICKLINE = d.pickline
                             where d.CARTONNO = '" + cartonNo + "'";
            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(@"AND d.sn='{0}'", sn);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object QueryPICKDetailMaterial(string pickNo, string cartonNo)
        {
            string sql = @" SELECT a.*
                            FROM TBLPICKDetailMaterial a
                            WHERE a.PICKNO = '" + pickNo + "' AND a.CARTONNO = '" + cartonNo + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new PagerCondition(sql, "a.MDATE DESC, a.MTIME DESC", 1, int.MaxValue));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }

            return null;
        }

        public object[] QueryPICKDetailMaterials(string pickNo, string dqMCode)
        {
            string sql = @" SELECT a.*
                            FROM TBLPICKDetailMaterial a
                            WHERE a.QTY > a.PQTY
                            AND a.PICKNO = '" + pickNo + "' AND a.DQMCODE = '" + dqMCode + "'";

            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new PagerCondition(sql, "a.MDATE DESC, a.MTIME DESC", 1, int.MaxValue));
        }
        public object[] QueryPICKDetailMaterialBydqMCode(string pickNo, string dqMCode)
        {
            string sql = @" SELECT a.*
                            FROM TBLPICKDetailMaterial a
                            WHERE  
                             a.PICKNO = '" + pickNo + "' AND a.DQMCODE = '" + dqMCode + "'";

            return this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new PagerCondition(sql, "a.MDATE DESC, a.MTIME DESC", 1, int.MaxValue));
        }
        public DataTable QueryPDAPackagingOperations(string pickNo)
        {
            string sql = @"SELECT a.DQMCODE, b.CUSTMCODE, a.QTY FROM TBLCartonInvDetailMaterial a, TBLPICKDETAIL b WHERE 1=1 AND a.PICKNO = b.PICKNO AND a.PICKLINE = b.PICKLINE";
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO='{0}'", pickNo);
            }
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable QueryPDAPickMaterial(string pickNo)
        {
            string sql = @"SELECT a.PICKLINE, a.DQMCODE, a.CUSTMCODE, (a.SQTY-a.PQTY) as QTY  FROM TBLPICKDETAIL a, TBLMATERIAL b WHERE 1=1 AND a.MCODE = b.MCODE AND b.MCONTROLTYPE = 'item_control_lot' AND (a.SQTY - a.PQTY) > 0";
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO = '{0}'", pickNo);
            }
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable QueryPDAPickMaterial(string pickNo, string gfHWItemCode, string gfPackingSEQ)
        {
            string sql = @"SELECT a.PICKLINE, a.DQMCODE, a.GFHWITEMCODE, (a.SQTY-a.PQTY) as QTY  FROM TBLPICKDETAIL a, TBLMATERIAL b WHERE 1=1 AND a.MCODE = b.MCODE AND b.MCONTROLTYPE = 'item_control_lot' AND (a.SQTY - a.PQTY) > 0 AND a.GFHWITEMCODE = '" + gfHWItemCode + "' AND a.GFPACKINGSEQ = '" + gfPackingSEQ + "'";
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO = '{0}'", pickNo);
            }
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable QueryPDAPackingDetail(string pickNo)
        {
            string sql = @"SELECT a.DQMCODE, b.CUSTMCODE, b.SQTY, a.QTY, a.CARTONNO FROM TBLCartonInvDetailMaterial a, TBLPICKDetail b WHERE 1=1 AND a.PICKNO = b.PICKNO AND a.PICKLINE = b.PICKLINE";
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO = '{0}'", pickNo);
            }
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable _QueryPDAPackingDetail(string pickNo)
        {
            string sql = @"SELECT b.GFHWITEMCODE, b.GFPACKINGSEQ, a.DQMCODE, b.SQTY, a.QTY, a.CARTONNO FROM TBLCartonInvDetailMaterial a, TBLPICKDetail b WHERE 1=1 AND a.PICKNO = b.PICKNO AND a.PICKLINE = b.PICKLINE";
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO = '{0}'", pickNo);
            }
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public int GetCartonNoCount(string pickNo, string gfHWItemCode, string gfPackingSEQ)
        {
            string sql = @"SELECT count(1) FROM TBLCartonInvDetailMaterial a WHERE 1=1 AND a.PICKNO = '" + pickNo + "' AND a.GFHWITEMCODE = '" + gfHWItemCode + "' AND a.GFPACKINGSEQ = '" + gfPackingSEQ + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region 转储作业 add by Chris.H.Wang @20160315
        public object[] QueryStorLocTransOperations(string transNo, int inclusive, int exclusive)
        {
            string sql = @" SELECT a.Transno,b.DQMCODE,a.invno,b.mcode,
                                   b.QTY as QTY,
                                   c.HasReMoveQty,
                                   b.UNIT,
                                   a.FromStorageCode,
                                   a.StorageCode,
                                   c.CUSER,
                                   c.CTIME,
                                   c.locationcode,
                                   c.fromlocationcode,
                                   c.cartonno,
                                   c.fromcartonno
                              from TBLStorLocTrans a
                              left join TBLStorLocTransDetail b
                                on a.TransNo = b.TransNo
                              left join (select TransNo, MCODE, sum(QTY) as HasReMoveQty, CUSER, CTIME,locationcode,fromlocationcode,cartonno,fromcartonno
                                           from TBLStorLocTransDetailCarton
                                          group by TransNo, MCODE, CUSER, CTIME,locationcode,fromlocationcode,cartonno,fromcartonno) c
                                on a.TransNo = c.TransNo and b.MCODE = c.MCODE where 1=1 ";
            if (!string.IsNullOrEmpty(transNo))
            {
                sql += string.Format(" and a.transNo like '%{0}'", transNo);
            }
            sql += string.Format(@" and a.transtype= '{0}'  ", "Transfer");//转储
            sql += string.Format(@" and b.status not in ('Cancel') ");//转储
            return this.DataProvider.CustomQuery(typeof(StorLocTransOperations), new PagerCondition(sql, "a.MDATE DESC, a.MTIME DESC", inclusive, exclusive));
        }

        public int QueryStorLocTransOperationsCount(string transNo)
        {
            string sql = @" SELECT count(1)
                              from TBLStorLocTrans a
                              left join TBLStorLocTransDetail b
                                on a.TransNo = b.TransNo
                              left join (select TransNo, MCODE, sum(QTY) as HasReMoveQty, CUSER, CTIME
                                           from TBLStorLocTransDetailCarton
                                          group by TransNo, MCODE, CUSER, CTIME) c
                                on a.TransNo = c.TransNo and b.MCODE = c.MCODE where 1=1 ";
            if (!string.IsNullOrEmpty(transNo))
            {
                sql += string.Format(" and a.transNo like '%{0}'", transNo);
            }
            sql += string.Format(@" and a.transtype= '{0}'  ", "Transfer");//转储
            sql += string.Format(@" and b.status not in ('Cancel') ");//转储
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public DataTable QueryPDAStorLocTransOperations(string transNo)
        {
            string sql = @" SELECT a.DQMCODE, b.FromLocationCode, a.QTY, b.SQTY,a.CUSTMCODE,A.STATUS,c.status headstatus
                              from TBLStorLocTransDetail a
                              left join (select TransNo, MCODE, sum(QTY) as SQTY, FromLocationCode
                                           from TBLStorLocTransDetailCarton
                                          group by TransNo, MCODE, FromLocationCode) b
                                on a.TransNo = b.TransNo
                               and a.MCODE = b.MCODE inner join TBLStorLocTrans c on a.transno=c.transno WHERE A.STATUS<>'Cancel' and c.status<>'Cancel' ";
            if (!string.IsNullOrEmpty(transNo))
            {
                sql += string.Format(" AND a.transNo = '{0}'", transNo);
            }
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }
        #endregion

        #region ASNIQC

        public string[] QueryIQCNo()
        {
            string sql = string.Format(@" SELECT {0} FROM TBLASNIQC where STATUS = 'WaitCheck' order by substr(IQCNO,5)", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQC)));
            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQC), new SQLCondition(sql));
            string[] str;
            if (objs != null)
            {
                str = new string[objs.Length];
                int i = 0;
                foreach (AsnIQC asnIQC in objs)
                {
                    str[i] = asnIQC.IqcNo;
                    i++;
                }
                return str;
            }
            return null;
        }


        public string[] QueryIQCNo1()
        {
            string sql = string.Format(@" SELECT {0} FROM TBLASNIQC where (STATUS = 'WaitCheck' or STATUS='Release') order by substr(IQCNO,5)", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AsnIQC)));
            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQC), new SQLCondition(sql));
            string[] str;
            if (objs != null)
            {
                str = new string[objs.Length];
                int i = 0;
                foreach (AsnIQC asnIQC in objs)
                {
                    str[i] = asnIQC.IqcNo;
                    i++;
                }
                return str;
            }
            return null;
        }

        public object GetAsnIQC(string Iqcno)
        {
            return this.DataProvider.CustomSearch(typeof(AsnIQC), new object[] { Iqcno });
        }

        public DataTable QueryAQLStandard()
        {
            string sql = @" SELECT AQLSEQ, AQLLEVEL FROM TBLAQL ORDER BY AQLLEVEL";
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public DataTable QueryAQLStandard1(int qty)
        {
            string sql = @" SELECT AQLSEQ, AQLLEVEL FROM TBLAQL WHERE " + qty + " BETWEEN LOTSIZEMIN AND LOTSIZEMAX ORDER BY AQLLEVEL";
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        public DataTable QueryAQLStandard(int qty)
        {
            string sql = string.Format("SELECT AQLSEQ, AQLLEVEL FROM TBLAQL WHERE {1} BETWEEN LOTSIZEMIN AND LOTSIZEMAX ", qty);


            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }


        public object GetAQLByLotSize(int lotSize)
        {
            string sql = string.Format("SELECT {0} FROM tblaql WHERE LOTSIZEMIN<=" + lotSize + " AND LOTSIZEMAX>=" + lotSize + "'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)));
            object[] aqlList = this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
            if (aqlList != null)
            {
                return aqlList[0];
            }
            return null;
        }

        public object GetAQLByLevel(string aqlLevel)
        {
            string sql = string.Format("SELECT {0} FROM tblaql WHERE aqllevel='" + aqlLevel + "'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)));
            object[] aqlList = this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
            if (aqlList != null)
            {
                return aqlList[0];
            }

            return null;
        }

        public DataTable GetIQCCheckResultMPDataGrid(string iqcNo)
        {
            string sql = @"SELECT b.CARTONNO, b.QTY, b.QCSTATUS, a.DQMCODE,a.IQCTYPE, b.NGQTY FROM TBLASNIQC a, TBLASNIQCDETAIL b WHERE 1=1 AND a.IQCNO = b.IQCNO";
            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND a.IQCNO='{0}'", iqcNo);
            }
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable GetIQCCheckResultMPDataGrid(string iqcNo, string dqmcode)
        {
            string sql = @"SELECT b.CARTONNO, b.QTY, b.QCSTATUS, a.DQMCODE,a.IQCTYPE, b.NGQTY FROM TBLASNIQC a, TBLASNIQCDETAIL b WHERE 1=1 AND a.IQCNO = b.IQCNO";
            if (!string.IsNullOrEmpty(iqcNo))
            {
                sql += string.Format(" AND a.IQCNO='{0}'", iqcNo);
            }
            if (!string.IsNullOrEmpty(dqmcode))
            {
                sql += string.Format(" AND a.DQMCODE='{0}'", dqmcode);
            }
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable GetIQCNGRecordDataGrid(string iqcNo)
        {
            string sql = @"SELECT a.STNO, a.STLINE, a.CARTONNO, a.ECGCODE, a.ECODE, a.SN, a.NGQTY, a.REMARK1 FROM TBLASNIQCDETAILEC a WHERE 1=1 AND a.IQCNO = '" + iqcNo + "'";
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public DataTable GetDataGridDoc(string iqcNo)
        {
            string sql = @"SELECT a.DOCNAME, a.DOCSIZE, a.UPUSER, a.UPFILEDATE FROM TBLINVDOC a WHERE 1=1 AND a.INVDOCNO = '" + iqcNo + "'";
            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0];
            return null;
        }

        #endregion

        public void UpdateASNForCancelDown(string[] asns, string status)
        {


            foreach (string asn in asns)
            {
                string sql = "UPDATE TBLASN SET status = '" + status + "',InitRejectQty=0,InitReceiveQty=0,InitReceiveDesc='',InitGiveinQty=0 WHERE STNO = '" + asn.ToUpper() + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(sql));
            }


        }

        public void UpdateASNForFirstCheck(string[] asns, string status)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                foreach (string asn in asns)
                {
                    string sql1 = "UPDATE TBLASN SET status = '" + status + "' WHERE STNO = '" + asn.ToUpper() + "' ";
                    this.DataProvider.CustomExecute(new SQLCondition(sql1));

                    string sql2 = "UPDATE TBLASNDETAIL SET status = '" + status + "' WHERE STNO = '" + asn.ToUpper() + "' ";
                    this.DataProvider.CustomExecute(new SQLCondition(sql2));
                }

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            this.DataProvider.CommitTransaction();
        }
        public string[] ValidateASNStatusForIQC(string[] asns)
        {
            List<string> errorList = new List<string>();
            foreach (string asn in asns)
            {
                string sql = string.Format(@" SELECT  {0} FROM TBLASNDETAIL  WHERE stno='" + asn + @"' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)));



                object[] objs = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql)) ?? new object[0];

                foreach (Asndetail asnDetail in objs)
                {
                    if (asnDetail.Status != "ReceiveClose" || asnDetail.InitreceiveStatus == "Reject")
                    {
                        errorList.Add(asn);
                        break;
                    }
                }


            }

            return errorList.ToArray();
        }



        public object[] QueryExecuteASN2(string no, string status, string invNo, string[] userGroups)
        {
            string sql = string.Format(@" SELECT  {0} FROM TBLASN  WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ASN)));


            if (!string.IsNullOrEmpty(no))
                sql += string.Format(" AND STNO ='{0}'", no);


            if (!string.IsNullOrEmpty(status))
                sql += string.Format(" AND status ='{0}'", status);
            else
                sql += " AND (STATUS='WaitReceive' OR STATUS='Receive' OR STATUS='ReceiveClose') ";
            if (!string.IsNullOrEmpty(invNo))
                sql += string.Format(" AND invno ='{0}'", invNo);
            sql += @"AND STNO IN(SELECT DISTINCT STNO FROM
 (SELECT STNO FROM TBLASN WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @")) UNION 
 SELECT STNO FROM TBLASN WHERE FROMSTORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE IN(" + SqlFormat(userGroups) + @"))) T  )";
            sql += "order by cdate,ctime desc";
            return this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql));

        }

        public string[] ValidateASNSTTypeForIQC(string[] asns)
        {
            List<string> errorList = new List<string>();
            foreach (string asn in asns)
            {
                string sql = string.Format(@" SELECT  {0} FROM TBLASN  WHERE stno='" + asn + @"' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ASN)));

                object[] objs = this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql)) ?? new object[0];
                foreach (ASN asnEntity in objs)
                {
                    if (asnEntity.StType == "PD" || (asnEntity.StType == "POR" && asnEntity.DirectFlag == "Y") || (asnEntity.StType == "SCTR" && asnEntity.RejectsFlag == "Y"))
                    {
                        errorList.Add(asn);
                        break;
                    }
                }


            }

            return errorList.ToArray();
        }

        public void SaveIQCInfo(string[] asns, string userCode)
        {
            this.DataProvider.BeginTransaction();

            try
            {
                foreach (string asn in asns)
                {
                    string sql1 = string.Format(@" SELECT  {0} FROM TBLASNDETAIL  WHERE stno='" + asn + @"' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)));
                    object[] objs = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql1)) ?? new object[0];
                    ASN asnEntity = (ASN)this.DataProvider.CustomSearch(typeof(ASN), new object[] { asn });

                    int sequenceNo = 0;
                    Dictionary<string, List<Asndetail>> marterialDic = new Dictionary<string, List<Asndetail>>();
                    foreach (Asndetail d in objs)
                    {
                        if (d.Status == "ReceiveClose" && d.InitreceiveStatus != "Reject")
                        {
                            if (!marterialDic.ContainsKey(d.DqmCode))
                            {
                                marterialDic.Add(d.DqmCode, new List<Asndetail>());
                                marterialDic[d.DqmCode].Add(d);
                            }
                            else
                            {
                                marterialDic[d.DqmCode].Add(d);
                            }
                        }
                    }
                    foreach (string key in marterialDic.Keys)
                    {
                        string IQCNO = "IQC" + asn + (++sequenceNo).ToString().PadLeft(2, '0');
                        BenQGuru.eMES.Domain.IQC.AsnIQC iqc = new BenQGuru.eMES.Domain.IQC.AsnIQC();
                        iqc.IqcNo = IQCNO;
                        iqc.StNo = asn;
                        iqc.InvNo = string.IsNullOrEmpty(asnEntity.InvNo) ? " " : asnEntity.InvNo;
                        iqc.StType = asnEntity.StType;
                        iqc.Status = "Release";
                        iqc.DQMCode = key;
                        iqc.CustmCode = marterialDic[key].Count > 0 ? marterialDic[key][0].CustmCode : string.Empty;
                        iqc.MCode = marterialDic[key].Count > 0 ? marterialDic[key][0].MCode : string.Empty;
                        iqc.MCode = marterialDic[key].Count > 0 ? marterialDic[key][0].MCode : string.Empty;
                        iqc.MDesc = marterialDic[key].Count > 0 ? marterialDic[key][0].MDesc : string.Empty;
                        iqc.Qty = marterialDic[key].Count > 0 ? marterialDic[key][0].Qty : 0;
                        iqc.VendorCode = asnEntity.VendorCode;
                        iqc.VendorMCode = marterialDic[key].Count > 0 ? marterialDic[key][0].VEndormCode : string.Empty;
                        iqc.AppDate = FormatHelper.TODateInt(DateTime.Now);
                        iqc.AppTime = FormatHelper.TOTimeInt(DateTime.Now);
                        iqc.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                        iqc.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                        iqc.CUser = userCode;
                        iqc.CDate = FormatHelper.TODateInt(DateTime.Now);
                        iqc.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                        iqc.MaintainUser = userCode;
                        this.DataProvider.Insert(iqc);

                        foreach (Asndetail d in marterialDic[key])
                        {
                            BenQGuru.eMES.Domain.IQC.AsnIQCDetail iqcD = new BenQGuru.eMES.Domain.IQC.AsnIQCDetail();
                            iqcD.IqcNo = IQCNO;
                            iqcD.StNo = asn;
                            iqcD.StLine = d.Stline;
                            iqcD.CartonNo = d.Cartonno;
                            iqcD.Qty = d.ReceiveQty;
                            iqcD.Unit = d.Unit;
                            iqcD.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            iqcD.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            iqcD.MaintainUser = userCode;
                            iqcD.CUser = userCode;
                            iqcD.CDate = FormatHelper.TODateInt(DateTime.Now);
                            iqc.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                            this.DataProvider.Insert(iqcD);

                            BenQGuru.eMES.Domain.IQC.AsnIqcDetailSN sn = new BenQGuru.eMES.Domain.IQC.AsnIqcDetailSN();

                            string sql2 = " SELECT   ctime, muser, mdate, mtime, cartonno, cuser, sn, stno, stline, cdate FROM TBLASNDETAILSN  WHERE stno='" + asn + @"' and STLINE='" + d.Stline + @"'";
                            object[] sns = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.IQC.AsnIqcDetailSN), new SQLCondition(sql2)) ?? new object[0];
                            sn.Sn = sns.Length > 0 ? ((BenQGuru.eMES.Domain.IQC.AsnIqcDetailSN)sns[0]).Sn : " ";
                            sn.StLine = d.Stline;
                            sn.StNo = asn;
                            sn.CartonNo = d.Cartonno;
                            sn.IqcNo = IQCNO;
                            sn.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            sn.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            sn.MaintainUser = userCode;
                            sn.CUser = userCode;
                            sn.CDate = FormatHelper.TODateInt(DateTime.Now);
                            sn.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                            this.DataProvider.Insert(sn);

                        }


                    }

                    string sql3 = "UPDATE TBLASN SET status = 'IQC' WHERE STNO = '" + asn.ToUpper() + "' ";
                    this.DataProvider.CustomExecute(new SQLCondition(sql3));
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            this.DataProvider.CommitTransaction();
        }

        public int GetMaxSequence()
        {
            string sql = "select max(serialno) from TBLBARCODE ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public void UpdateTBLCartonInvoices(CARTONINVOICES CARINVNO)
        {
            this.DataProvider.Update(CARINVNO);
        }
        #region Cartoninvoices
        /// <summary>
        /// TBLCARTONINVOICES
        /// </summary>
        public CARTONINVOICES CreateNewCartoninvoices()
        {
            return new CARTONINVOICES();
        }

        public void AddCartoninvoices(CARTONINVOICES cartoninvoices)
        {
            this.DataProvider.Insert(cartoninvoices);
        }

        public void DeleteCartoninvoices(CARTONINVOICES cartoninvoices)
        {
            this.DataProvider.Delete(cartoninvoices);
        }

        public void UpdateCartoninvoices(CARTONINVOICES cartoninvoices)
        {
            this.DataProvider.Update(cartoninvoices);
        }

        public object GetCartoninvoices(string Carinvno)
        {
            return this.DataProvider.CustomSearch(typeof(CARTONINVOICES), new object[] { Carinvno });
        }

        //根据OQC检验单获取发货箱单头信息 add by jinger 20160309
        /// <summary>
        /// 根据OQC检验单获取发货箱单头信息
        /// </summary>
        /// <param name="oqcNo">OQC检验单</param>
        /// <returns></returns>
        public object GetCartoninvoicesByOqcNo(string oqcNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLCARTONINVOICES WHERE CARINVNO = (SELECT CARINVNO FROM TBLOQC WHERE OQCNO='{1}')",
                                                                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(CARTONINVOICES)), oqcNo);
            object[] objCartoninvoices = this.DataProvider.CustomQuery(typeof(CARTONINVOICES), new SQLCondition(sql));
            if (objCartoninvoices != null && objCartoninvoices.Length > 0)
            {
                return objCartoninvoices[0];
            }
            return null;
        }

        public object[] GetCartoninvoicesByPickNo(string pickNo)
        {
            string sql = "select a.* from TBLCARTONINVOICES a where a.pickno='" + pickNo + "'";
            return this.DataProvider.CustomQuery(typeof(CARTONINVOICES), new SQLCondition(sql));
        }

        #endregion

        public Pickdetailmaterial[] GetPickMaterials1(string pickNo)
        {

            string sql = "select a.dqmcode,a.mcode,a.pickline,sum(a.qty) qty from TBLPICKDetailMaterial a where a.pickno='" + pickNo + "' group by a.dqmcode,a.mcode,a.pickline";
            object[] o = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            List<Pickdetailmaterial> l = new List<Pickdetailmaterial>();
            if (o != null && o.Length > 0)
            {
                foreach (Pickdetailmaterial p in o)
                    l.Add(p);
            }

            return l.ToArray();
        }

        public Pickdetailmaterial[] GetPickMaterials(string pickNo)
        {

            string sql = "select a.* from TBLPICKDetailMaterial a where a.pickno='" + pickNo + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            List<Pickdetailmaterial> l = new List<Pickdetailmaterial>();
            if (o != null && o.Length > 0)
            {
                foreach (Pickdetailmaterial p in o)
                    l.Add(p);
            }

            return l.ToArray();
        }

        public PickDetail GetPickDetail(string pickNo, string DQMCode)
        {
            string sql = "select a.* from TBLPICKDETAIL a where a.pickno='" + pickNo + "' and DQMCODE='" + DQMCode + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            if (o != null && o.Length > 0)
            {
                return (PickDetail)o[0];
            }

            return null;

        }

        public Pickdetailmaterialsn[] GetPickMaterialSN(string pickNo)
        {

            string sql = "select a.* from TBLPICKDetailMaterialSN a where a.pickno='" + pickNo + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(Pickdetailmaterialsn), new SQLCondition(sql));
            List<Pickdetailmaterialsn> l = new List<Pickdetailmaterialsn>();
            if (o != null && o.Length > 0)
            {
                foreach (Pickdetailmaterialsn p in o)
                    l.Add(p);
            }

            return l.ToArray();
        }

        public PickDetail[] GetPickDetails(string pickNo)
        {
            string sql = "select a.* from TBLPICKDETAIL a where a.pickno='" + pickNo + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            List<PickDetail> l = new List<PickDetail>();
            if (o != null && o.Length > 0)
            {
                foreach (PickDetail p in o)
                    l.Add(p);
            }

            return l.ToArray();

        }
        public void UpdateCartonInvoices(CARTONINVOICES c)
        {
            this.DataProvider.Update(c);
        }
        public object GetTBLCartonInvoices(string CARINVNO)
        {
            return this.DataProvider.CustomSearch(typeof(CARTONINVOICES), new object[] { CARINVNO });
        }

        #region CartonInvDetail


        public void AddCartonInvDetail(CartonInvDetail car)
        {
            this.DataProvider.Insert(car);
        }

        public void DeleteCartonInvDetail(CartonInvDetail car)
        {
            this.DataProvider.Delete(car);
        }

        public void UpdateCartonInvDetail(CartonInvDetail car)
        {
            this.DataProvider.Update(car);
        }

        public object GetCartonInvDetail(string CARINVNO, string CARTONNO)
        {
            return this.DataProvider.CustomSearch(typeof(CartonInvDetail), new object[] { CARINVNO, CARTONNO });
        }
        public void DeleteCartoninvdetailByCartonNo(string CARINVNO, string CARTONNO)
        {
            string sql = string.Format(@"delete  TBLCartonInvDetail  where   CARINVNO='{0}' and CARTONNO='{1}' ", CARINVNO, CARTONNO);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateCartoninvdetailByCARINVNO(string CARINVNO, string status)
        {
            string sql = string.Format("update TBLCartonInvDetail set STATUS='{0}'  where CARINVNO='{1}'", status, CARINVNO);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        public int GetCartonCount(string CARINVNO, string status)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(1) from TBLCartonInvDetail where 1=1 and CARINVNO = '{0}' and STATUS = '{1}' ", CARINVNO, status)));
        }

        #endregion

        #region Cartoninvdetailsn
        public void AddCARTONINVDETAILSN(CARTONINVDETAILSN sn)
        {
            this.DataProvider.Insert(sn);
        }

        public void UpdateCARTONINVDETAILSN(CARTONINVDETAILSN sn)
        {
            this.DataProvider.Update(sn);
        }
        public void DELETECARTONINVDETAILSN(CARTONINVDETAILSN sn)
        {
            this.DataProvider.Delete(sn);
        }

        public object GetCartoninvdetailsn(string Carinvno, string Sn)
        {
            return this.DataProvider.CustomSearch(typeof(CARTONINVDETAILSN), new object[] { Carinvno, Sn });
        }
        public object QueryCARTONINVDETAILSN(string CARINVNO, string SN)
        {
            return this.DataProvider.CustomSearch(typeof(CARTONINVDETAILSN), new object[] { CARINVNO, SN });
        }
        public object[] GetCartoninvdetailsnByCartonno(string Carinvno, string cartonno)
        {
            string sql = string.Format(@"select a.* from TBLCARTONINVDETAILSN a where a.Carinvno ='{0}' and a.cartonno='{1}'  ", Carinvno, cartonno);
            return this.DataProvider.CustomQuery(typeof(CARTONINVDETAILSN), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", 1, int.MaxValue));
        }

        public object[] GetCartoninvdetailsn(string Carinvno)
        {
            string sql = string.Format(@"select a.* from TBLCARTONINVDETAILSN a where a.Carinvno ='{0}'   ", Carinvno);
            return this.DataProvider.CustomQuery(typeof(CARTONINVDETAILSN), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", 1, int.MaxValue));
        }

        public object[] GetCartoninvdetailsnBySn(string sn)
        {
            string sql = string.Format(@"select a.* from TBLCARTONINVDETAILSN a where a.SN ='{0}'   ", sn);
            return this.DataProvider.CustomQuery(typeof(CARTONINVDETAILSN), new SQLCondition(sql));
        }

        public void DeleteCartoninvdetailsnByCartonNo(string cartonNo)
        {
            string sql = string.Format(@"delete  TBLCARTONINVDETAILSN  where   CARTONNO='{0}'  ", cartonNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void DeleteCartoninvdetailsnByCartonNo(string pickno, string cartonNo, string dqmCode)
        {

            string sql = @"delete  TBLCARTONINVDETAILSN  where   CARTONNO='" + cartonNo + "' and pickno='" + pickno + "' AND PICKLINE IN(SELECT pickline FROM TBLPICKDETAIL where pickno='" + pickno + "' and DQMCODE='" + dqmCode + "' )";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region CartonInvDetailMaterial
        public void AddCartonInvDetailMaterial(CartonInvDetailMaterial c)
        {
            this.DataProvider.Insert(c);
        }

        public void UpdateCartonInvDetailMaterial(CartonInvDetailMaterial c)
        {
            this.DataProvider.Update(c);
        }

        public void DeleteCartonInvDetailMaterial(CartonInvDetailMaterial c)
        {
            this.DataProvider.Delete(c);
        }

        public object QueryCartonInvDetailMaterial(string CARINVNO, string CARTONNO, string DqMCode)
        {
            return this.DataProvider.CustomSearch(typeof(CartonInvDetailMaterial), new object[] { CARINVNO, CARTONNO, DqMCode });
        }
        public object[] QueryCartonInvDetailMaterial(string CARINVNO)
        {
            string sql = "SELECT a.* FROM TBLCartonInvDetailMaterial  a WHERE a.CARINVNO='" + CARINVNO + "'";
            return this.DataProvider.CustomQuery(typeof(CartonInvDetailMaterial), new SQLCondition(sql));
        }
        #endregion


        public void UpdatePick(Pick pick)
        {
            this.DataProvider.Update(pick);
        }

        public void UpdatePickStatusByPickNo(string status, string pickNo)
        {
            string sql = string.Format("update tblpick  set Status='{0}',mdate=sys_date,mtime=sys_time where PickNo='{1}'", status, pickNo);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdatePickDetailStatusByPickNo(string status, string pickNo, string oldstatus)
        {
            string sql = string.Format("update tblpickdetail  set Status='{0}',mdate=sys_date,mtime=sys_time where PickNo='{1}' and STATUS='{2}' ", status, pickNo, oldstatus);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public void UpdatePickDetailToPickingByPickNo(string status, string pickNo)
        {


            string sql = string.Format("update tblpickdetail  set Status='{0}',mdate=sys_date,mtime=sys_time where PickNo='{1}'", status, pickNo);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        public BenQGuru.eMES.Domain.MOModel.Material GetMaterialFromDQMCode(string DQMCode)
        {

            string sql = "SELECT a.* FROM TBLMATERIAL a WHERE DQMCODE='" + DQMCode + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material), new SQLCondition(sql));
            if (o != null && o.Length > 0)
            {
                return (BenQGuru.eMES.Domain.MOModel.Material)(o[0]);
            }
            return new BenQGuru.eMES.Domain.MOModel.Material();
        }

        public string GetGfhwDescFromGfhwmcodeForNotDN(string gfhwmcode, string invno)
        {

            string sql = string.Format(@"SELECT GFHWDESC FROM tblinvoicesdetail WHERE GFHWMCODE='{0}' and invno='{1}' ", gfhwmcode, invno);
            object[] o = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (o != null && o.Length > 0)
            {
                return ((InvoicesDetail)(o[0])).GfhwDesc;
            }
            return "";
        }

        public string GetGfhwDescFromGfhwmcodeForDN(string gfhwmcode, string invno)
        {

            string sql = string.Format(@"SELECT A.GFHWDESC FROM tblinvoicesdetail A,TBLINVOICES B WHERE A.INVNO=B.INVNO AND A.GFHWMCODE='{0}' and B.DNBATCHNO='{1}' ", gfhwmcode, invno);
            object[] o = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (o != null && o.Length > 0)
            {
                return ((InvoicesDetail)(o[0])).GfhwDesc;
            }
            return "";
        }

        public object[] QueryMaterialFromDQMCode(string DQMCode)
        {
            string sql = "SELECT a.* FROM TBLMATERIAL a WHERE DQMCODE='" + DQMCode + "'";
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material), new SQLCondition(sql));
        }




        public OQCNew[] GetOQCDetailsForN(string CARINVNO)
        {
            List<OQCNew> os = new List<OQCNew>();
            string sql = "select  b.GFHWITEMCODE,b.MCODE,b.DQMCODE,b.QTY,b.UNIT,b.GFPACKINGSEQ,b.CARTONNO from TBLCartonInvoices a,TBLCartonInvDetailMaterial b where a.CARINVNO= b.CARINVNO  and a.CARINVNO='" + CARINVNO + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(OQCNew), new SQLCondition(sql));
            if (o == null)
                return new OQCNew[0];
            else
            {
                foreach (OQCNew oqc in o)
                {
                    os.Add(oqc);
                }
            }
            return os.ToArray();
        }

        public OQCNew[] GetOQCDetailSNForN(string CARINVNO)
        {
            List<OQCNew> os = new List<OQCNew>();
            string sql = "select  c.CARTONNO,c.SN,a.CARINVNO from TBLCartonInvoices a, TBLCartonInvDetailSN c where a.CARINVNO= c.CARINVNO  and a.CARINVNO='" + CARINVNO + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(OQCNew), new SQLCondition(sql));
            if (o == null)
                return new OQCNew[0];
            else
            {
                foreach (OQCNew oqc in o)
                {
                    os.Add(oqc);
                }
            }
            return os.ToArray();
        }

        public PickDetail[] GetPickdetails(string pickNo)
        {
            List<PickDetail> os = new List<PickDetail>();
            string sql = "select a.*   from TBLPICKDETAIL a where a.PICKNO ='" + pickNo + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            if (o == null)
                return new PickDetail[0];
            else
            {
                foreach (PickDetail oqc in o)
                {
                    os.Add(oqc);
                }
            }
            return os.ToArray();

        }


        public OQCNew[] GetOQCDetailsForY(string CARINVNO, string GFHWITEMCODE)
        {
            List<OQCNew> os = new List<OQCNew>();
            string sql = "select  b.GFHWITEMCODE,b.MCODE,b.DQMCODE,b.QTY,b.UNIT,b.GFPACKINGSEQ,b.CARTONNO from TBLCartonInvoices a,TBLCartonInvDetailMaterial b where a.CARINVNO= b.CARINVNO  and a.CARINVNO='" + CARINVNO + "' AND b.GFHWITEMCODE='" + GFHWITEMCODE + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(OQCNew), new SQLCondition(sql));
            if (o == null)
                return new OQCNew[0];
            else
            {
                foreach (OQCNew oqc in o)
                {
                    os.Add(oqc);
                }
            }
            return os.ToArray();

        }

        public OQCNew[] GetOQCDetailSNForY(string CARINVNO, string GFHWITEMCODE)
        {

            List<OQCNew> os = new List<OQCNew>();

            string sql = @"select  c.CARTONNO,c.SN,c.CARINVNO,b.mcode,b.dqmcode from TBLCartonInvDetailSN c,(select GFHWITEMCODE ,carinvno,cartonno,dqmcode,mcode 
                     from TBLCartonInvDetailMaterial group by GFHWITEMCODE ,mcode,carinvno,cartonno,dqmcode,mcode ) b  ,tblpickdetail a 
                       where c.carinvno=b.carinvno and c.CARTONNO=b.CARTONNO 
                       and a.pickno=c.pickno and a.pickline=c.pickline and b.mcode=a.mcode and b.dqmcode=a.dqmcode
                   and b.CARINVNO='" + CARINVNO + "' AND b.GFHWITEMCODE='" + GFHWITEMCODE + "'";

            object[] o = this.DataProvider.CustomQuery(typeof(OQCNew), new SQLCondition(sql));
            if (o == null)
                return new OQCNew[0];
            else
            {
                foreach (OQCNew oqc in o)
                {
                    os.Add(oqc);
                }
            }
            return os.ToArray();

        }

        public object[] QueryPicksForLoad(string DQMCode)
        {
            string sql = "SELECT a.* FROM TBLMATERIAL a WHERE DQMCODE='" + DQMCode + "'";
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Material), new SQLCondition(sql));
        }

        public List<string> GetGFHWITEMCODE(string CARINVNO)
        {
            string sql = "select  b.GFHWITEMCODE from TBLCartonInvDetailMaterial b   where  b.CARINVNO='" + CARINVNO + "' group by b.GFHWITEMCODE";
            List<string> os = new List<string>();
            object[] o = this.DataProvider.CustomQuery(typeof(CartonInvDetailMaterial), new SQLCondition(sql));
            if (o != null)
            {

                foreach (CartonInvDetailMaterial oqc in o)
                {
                    os.Add(oqc.GFHWITEMCODE);
                }
            }
            return os;

        }


        public OQCNew[] GetOQCNew(string CARINVNO)
        {
            List<OQCNew> os = new List<OQCNew>();
            string sql = "select  b.GFHWITEMCODE,b.MCODE,b.DQMCODE,b.QTY,b.UNIT,b.GFPACKINGSEQ,b.CARTONNO,c.SN from TBLCartonInvoices a, TBLCartonInvDetailSN c where a.CARINVNO= b.CARINVNO and a.CARINVNO=c.CARINVNO  and a.CARINVNO='" + CARINVNO + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(OQCNew), new SQLCondition(sql));
            if (o == null)
                return new OQCNew[0];
            else
            {
                foreach (OQCNew oqc in o)
                {
                    os.Add(oqc);
                }
            }
            return os.ToArray();

        }

        public void AddDNLOG(DNLOG LOG)
        {
            string sql = string.Empty;
            sql = sql + "INSERT INTO DNLOG ";
            sql = sql + "(DNLINE,DNNO,QTY,UNIT,ISSUCCESS,MDATE,MTIME,MUSER,ISALL) ";
            sql = sql + "VALUES ";
            sql = sql + "(" + LOG.DNLINE + "," + "'" + LOG.DNNO + "'," + LOG.Qty + ",'" + LOG.Unit + "','" + LOG.RESULT + "'," + LOG.MDATE + "," + LOG.MTIME + ",'" + LOG.MUSER + "','" + LOG.ISALL + "')";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        //modify by jinger 20160309 
        public void AddOQC(BenQGuru.eMES.Domain.OQC.OQC OQC)
        {
            this.DataProvider.Insert(OQC);
        }
        //end
        public void AddOQCDetail(BenQGuru.eMES.Domain.OQC.OQCDetail D)
        {
            this.DataProvider.Insert(D);
        }

        public void AddOQCDetailSN(BenQGuru.eMES.Domain.OQC.OQCDetailSN SN)
        {
            this.DataProvider.Insert(SN);
        }
        public object GetOqcdetailsn(string Carinvno, string Oqcno, string Sn)
        {
            return this.DataProvider.CustomSearch(typeof(OQCDetailSN), new object[] { Carinvno, Oqcno, Sn });
        }
        public object[] GetOqcdetailsn(string Carinvno, string Oqcno, string Sn, string CartonNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLOQCDETAILSN WHERE CARINVNO='{1}' AND OQCNO='{2}' AND SN='{3}' AND CARTONNO='{4}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailSN)), Carinvno, Oqcno, Sn, CartonNo);
            return this.DataProvider.CustomQuery(typeof(OQCDetailSN), new SQLCondition(sql));
        }
        public Storage[] GetStorageCode()
        {
            string sql = "select a.* from TBLStorage a ";
            List<Storage> l = new List<Storage>();
            object[] o = this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
            if (o != null && o.Length != 0)
            {
                foreach (Storage s in o)
                {
                    l.Add(s);
                }
            }

            return l.ToArray();

        }

        public List<string> GetPortionStockChecks()
        {
            string sql = "select  a.* from TBLStockCheck a order by a.CheckNo";
            List<string> os = new List<string>();
            object[] o = this.DataProvider.CustomQuery(typeof(StockCheck), new SQLCondition(sql));
            if (o != null)
            {

                foreach (StockCheck oqc in o)
                {
                    os.Add(oqc.CheckNo);
                }
            }
            return os;

        }


        public StockCheckDetail GetPortionStockChecksFormCheckNo(string checkNo)
        {
            string sql = "select  a.* from TBLStockCheckDetail a  where a.checkno='" + checkNo + "'";
            List<StockCheckDetail> os = new List<StockCheckDetail>();
            object[] o = this.DataProvider.CustomQuery(typeof(StockCheckDetail), new SQLCondition(sql));
            if (o != null)
            {

                foreach (StockCheckDetail oqc in o)
                {
                    os.Add(oqc);
                }
            }
            if (os.Count > 0)
            {
                return os[0];
            }
            return null;

        }

        public object GetStockCheck(string checkNo)
        {
            return this.DataProvider.CustomSearch(typeof(StockCheck), new object[] { checkNo });
        }

        public void AddOQCDetailSN(BenQGuru.eMES.Domain.Warehouse.StockCheckDetail s)
        {
            this.DataProvider.Insert(s);
        }

        public int GetStockCheckDetailCount(string checkNo, string storageCode, string DQMCODE)
        {
            string sql = "select count(*) from TBLSTOCKCHECKDETAIL where CHECKNO='" + checkNo + "' AND STORAGECODE='" + storageCode + "' AND DQMCODE='" + DQMCODE + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        public StockCheckDetail[] GetStockCheckDetail(string checkNo, string storageCode, string DQMCODE)
        {
            string sql = "select a.* from TBLSTOCKCHECKDETAIL a where CHECKNO='" + checkNo + "' AND STORAGECODE='" + storageCode + "' AND DQMCODE='" + DQMCODE + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(StockCheckDetail), new SQLCondition(sql));

            List<StockCheckDetail> ss = new List<StockCheckDetail>();
            if (o != null && o.Length != 0)
            {
                foreach (StockCheckDetail s in o)
                    ss.Add(s);
            }
            return ss.ToArray();

        }

        public void InsertStockCheckDetail(BenQGuru.eMES.Domain.Warehouse.StockCheckDetail s)
        {
            this.DataProvider.Insert(s);
        }

        public StockCheckDetail[] GetStockCheckDetails(string checkNo)
        {
            string sql = "select a.* from TBLSTOCKCHECKDETAIL a where CHECKNO='" + checkNo + "' order by a.STORAGECODE,a.DQMCODE,a.CHECKQTY"; ;
            object[] o = this.DataProvider.CustomQuery(typeof(StockCheckDetail), new SQLCondition(sql));

            List<StockCheckDetail> ss = new List<StockCheckDetail>();
            if (o != null && o.Length != 0)
            {
                foreach (StockCheckDetail s in o)
                    ss.Add(s);
            }
            return ss.ToArray();

        }

        public StockCheckDetail[] GetDisStockCheckDetails(string checkNo)
        {
            string sql = "select distinct a.STORAGECODE,a.DQMCODE,a.CHECKQTY   from TBLSTOCKCHECKDETAIL a where CHECKNO='" + checkNo + "' order by a.STORAGECODE,a.DQMCODE,a.CHECKQTY"; ;
            object[] o = this.DataProvider.CustomQuery(typeof(StockCheckDetail), new SQLCondition(sql));

            List<StockCheckDetail> ss = new List<StockCheckDetail>();
            if (o != null && o.Length != 0)
            {
                foreach (StockCheckDetail s in o)
                    ss.Add(s);
            }
            return ss.ToArray();

        }


        public void UpdateStockCheckDetail(StockCheckDetail c)
        {
            this.DataProvider.Update(c);
        }

        public void UpdateStockCheck(StockCheck c)
        {
            this.DataProvider.Update(c);
        }

        public void AddStockCheck(BenQGuru.eMES.Domain.Warehouse.StockCheck s)
        {
            this.DataProvider.Insert(s);
        }

        public void AddStockCheckDetails(BenQGuru.eMES.Domain.Warehouse.StockCheckDetail s)
        {
            this.DataProvider.Insert(s);
        }

        public StockCheck[] QueryStockCheck(string checkNo, string storageNo, string type, int bDate, int eDate, int inclusive, int exclusive)
        {
            string sql = @" select a.* from TBLStockCheck a where 1=1";
            if (!string.IsNullOrEmpty(checkNo))
            {

                sql += string.Format(" AND a.checkno like '%{0}%'", checkNo);
            }

            if (!string.IsNullOrEmpty(type))
            {
                sql += string.Format(" AND a.CheckType LIKE'%{0}%'", type);
            }

            if (!string.IsNullOrEmpty(storageNo))
            {
                sql += string.Format(" AND a.StorageCode LIKE '%{0}%'", storageNo);
            }
            if (bDate > 0)
            {
                sql += string.Format(@" AND a.CDATE >= {0}", bDate);
            }
            if (eDate > 0)
            {
                sql += string.Format(@" AND a.CDATE <= {0}", eDate);
            }

            object[] o = this.DataProvider.CustomQuery(typeof(StockCheck), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", inclusive, exclusive));
            List<StockCheck> ss = new List<StockCheck>();
            if (o != null && o.Length != 0)
            {
                foreach (StockCheck s in o)
                    ss.Add(s);
            }
            return ss.ToArray();
        }


        public StockCheck[] QueryStockCheck2(string[] userGroups, string checkNo, string storageNo, string type, int bDate, int eDate, int inclusive, int exclusive)
        {
            string sql = @" select a.* from TBLStockCheck a where 1=1";
            if (!string.IsNullOrEmpty(checkNo))
            {

                sql += string.Format(" AND a.checkno like '%{0}%'", checkNo);
            }

            if (!string.IsNullOrEmpty(type))
            {
                sql += string.Format(" AND a.CheckType LIKE'%{0}%'", type);
            }

            if (!string.IsNullOrEmpty(storageNo))
            {
                sql += string.Format(" AND a.StorageCode LIKE '%{0}%'", storageNo);
            }
            if (bDate > 0)
            {
                sql += string.Format(@" AND a.CDATE >= {0}", bDate);
            }
            if (eDate > 0)
            {
                sql += string.Format(@" AND a.CDATE <= {0}", eDate);
            }


            sql += @" and a.StorageCode IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @"))";
            object[] o = this.DataProvider.CustomQuery(typeof(StockCheck), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", inclusive, exclusive));
            List<StockCheck> ss = new List<StockCheck>();
            if (o != null && o.Length != 0)
            {
                foreach (StockCheck s in o)
                    ss.Add(s);
            }
            return ss.ToArray();
        }

        public int QueryStockCheckCount2(string[] userGroups, string checkNo, string storageNo, string type, int bDate, int eDate)
        {
            string sql = @" select count(*) from TBLStockCheck a where 1=1 ";
            if (!string.IsNullOrEmpty(checkNo))
            {

                sql += string.Format(" AND a.checkno like '%{0}%'", checkNo);
            }

            if (!string.IsNullOrEmpty(type))
            {
                sql += string.Format(" AND a.CheckType LIKE'%{0}%'", type);
            }

            if (!string.IsNullOrEmpty(storageNo))
            {
                sql += string.Format(" AND a.StorageCode LIKE '%{0}%'", storageNo);
            }
            if (bDate > 0)
            {
                sql += string.Format(@" AND a.CDATE >= {0}", bDate);
            }
            if (eDate > 0)
            {
                sql += string.Format(@" AND a.CDATE <= {0}", eDate);
            }
            sql += @" and a.StorageCode IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @"))";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public int QueryStockCheckCount(string checkNo, string storageNo, string type, int bDate, int eDate)
        {
            string sql = @" select count(*) from TBLStockCheck a where 1=1";
            if (!string.IsNullOrEmpty(checkNo))
            {

                sql += string.Format(" AND a.checkno like '%{0}%'", checkNo);
            }

            if (!string.IsNullOrEmpty(type))
            {
                sql += string.Format(" AND a.CheckType LIKE'%{0}%'", type);
            }

            if (!string.IsNullOrEmpty(storageNo))
            {
                sql += string.Format(" AND a.StorageCode LIKE '%{0}%'", storageNo);
            }
            if (bDate > 0)
            {
                sql += string.Format(@" AND a.CDATE >= {0}", bDate);
            }
            if (eDate > 0)
            {
                sql += string.Format(@" AND a.CDATE <= {0}", eDate);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public object GetPick(string pickNo)
        {
            return this.DataProvider.CustomSearch(typeof(Pick), new object[] { pickNo });
        }

        public void DeletePick(object obj)
        {
            this.DataProvider.Delete(obj);

        }

        public void AddPick(Pick car)
        {
            this.DataProvider.Insert(car);
        }

        public void DeleteDeteils(string pickNo)
        {
            string sql = string.Format(@"delete from TBLPICKDETAIL where pickno='{0}'", pickNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }
        public int GetPicksCount(string pickNo, string type, string storageCode, string invNO, string status, int bDate, int eDate, string usercode)
        {
            string sql = @"SELECT count(*) FROM TBLPICK a  
                         left join tblinvoices b on a.invno=b.invno
                              WHERE 1=1";

            if (usercode != "ADMIN")
            {
                sql += string.Format(@" and b.vendorcode in ( SELECT VENDORCODE FROM  TBLUSERGROUP2VENDOR
                            WHERE USERGROUPCODE in 
                      ( select distinct USERGROUPCODE from tblusergroup2user  where usercode='{0}') )", usercode);
            }

            if (!string.IsNullOrEmpty(pickNo))
            {

                sql += string.Format(" AND a.pickno like '%{0}%'", pickNo);
            }

            if (!string.IsNullOrEmpty(type))
            {
                sql += string.Format(" AND a.PICKTYPE ='{0}'", type);
            }
            else
            {
                sql += string.Format(" AND a.PICKTYPE ='{0}' ", PickType.PickType_WWPOC);
            }

            if (!string.IsNullOrEmpty(invNO))
            {
                sql += string.Format(" AND a.INVNO like '%{0}%'", invNO);
            }

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND a.STATUS = '{0}'", status);
            }
            if (bDate > 0)
            {
                sql += string.Format(@" AND a.CDATE >= {0}", bDate);
            }
            if (eDate > 0)
            {
                sql += string.Format(@" AND a.CDATE <= {0}", eDate);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int IsHasVenderCode(string invno, string usercode)
        {
            string sql = string.Format(@"SELECT count(1) FROM  tblinvoices where invno='{0}' and 
                         VENDORCODE in   ( SELECT distinct VENDORCODE FROM  TBLUSERGROUP2VENDOR 
                                       WHERE USERGROUPCODE in 
                         ( select distinct USERGROUPCODE from tblusergroup2user
                      where usercode='{1}' ) )   ", invno, usercode);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetPicks(string pickNo, string type, string storageCode, string invNO, List<string> status, int bDate, int eDate,
            string usercode, int inclusive, int exclusive)
        {
            string sql = @"SELECT a.* FROM TBLPICK a 
                    left join tblinvoices b on a.invno=b.invno
                              WHERE 1=1";
            if (usercode != "ADMIN")
            {
                sql += string.Format(@" and b.vendorcode in ( SELECT VENDORCODE FROM  TBLUSERGROUP2VENDOR WHERE USERGROUPCODE in 
                      ( select distinct USERGROUPCODE from tblusergroup2user  where usercode='{0}') )", usercode);
            }

            if (!string.IsNullOrEmpty(pickNo))
            {

                sql += string.Format(" AND a.pickno like '%{0}%'", pickNo);
            }

            if (!string.IsNullOrEmpty(type))
            {
                sql += string.Format(" AND a.PICKTYPE ='{0}'", type);
            }
            else
            {
                sql += string.Format(" AND a.PICKTYPE ='{0}' ", PickType.PickType_WWPOC);
            }

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND a.storageCode ='{0}' ", storageCode);
            }

            if (!string.IsNullOrEmpty(invNO))
            {
                sql += string.Format(" AND a.INVNO like '%{0}%'", invNO);
            }

            if (status.Count > 0)
            {
                sql += string.Format(" AND a.STATUS in (" + SqlFormat(status) + ")", status);
            }
            if (bDate > 0)
            {
                sql += string.Format(@" AND a.CDATE >= {0}", bDate);
            }
            if (eDate > 0)
            {
                sql += string.Format(@" AND a.CDATE <= {0}", eDate);
            }

            return this.DataProvider.CustomQuery(typeof(Pick), new PagerCondition(sql, "a.MDATE DESC, a.MTIME DESC", inclusive, exclusive));
        }

        public object[] GetPickDetails(string pickNo, string DQMCODE, int inclusive, int exclusive)
        {
            string sql = "SELECT a.* FROM TBLPICKDETAIL a WHERE 1=1";
            if (!string.IsNullOrEmpty(pickNo))
            {

                sql += string.Format(" AND a.pickno = '{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(DQMCODE))
            {
                sql += string.Format(" AND a.DQMCODE lIKE'%{0}%'", DQMCODE);
            }



            return this.DataProvider.CustomQuery(typeof(PickDetail), new PagerCondition(sql, "a.MDATE DESC, a.MTIME DESC", inclusive, exclusive));
        }




        public int GetPickDetailsCount(string pickNo, string DQMCODE)
        {
            string sql = "SELECT  count(*) FROM TBLPICKDETAIL a WHERE 1=1";
            if (!string.IsNullOrEmpty(pickNo))
            {

                sql += string.Format(" AND a.pickno = '{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(DQMCODE))
            {
                sql += string.Format(" AND a.DQMCODE lIKE'%{0}%'", DQMCODE);
            }


            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        public int GetMaxLine(string pickNo)
        {
            string sql = "SELECT nvl(max(nvl(to_number(a.pickline),0)),0) line FROM TBLPICKDETAIL a WHERE 1=1";
            if (!string.IsNullOrEmpty(pickNo))
            {

                sql += string.Format(" AND a.pickno = '{0}'", pickNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public Do[] GetStorageQTY(string StorageCode)
        {
            string sql = "select sum(a.STORAGEQTY) sum,a.CARTONNO, a.MDESC MDESC,a.UNIT,a.DQMCODE from TBLStorageDetail a where StorageCode='" + StorageCode + "' group by a.DQMCODE,a.MDESC,a.UNIT,a.CARTONNO";

            object[] o = this.DataProvider.CustomQuery(typeof(Do), new SQLCondition(sql));
            List<Do> ds = new List<Do>();

            if (o != null && o.Length > 0)
            {
                foreach (Do d in o)
                    ds.Add(d);
            }
            return ds.ToArray();
        }



        public Do[] GetStorageQTY123(string StorageCode)
        {
            string sql = "select sum(a.STORAGEQTY) sum, a.DQMCODE from TBLStorageDetail a where StorageCode='" + StorageCode + "' group by a.DQMCODE";

            object[] o = this.DataProvider.CustomQuery(typeof(Do), new SQLCondition(sql));
            List<Do> ds = new List<Do>();

            if (o != null && o.Length > 0)
            {
                foreach (Do d in o)
                    ds.Add(d);
            }
            return ds.ToArray();
        }


        public Do[] GetPortionStorageQTY(string StorageCode, int cdate, int enddate)
        {

            string sql = "select sum(a.STORAGEQTY) sum, a.CARTONNO,a.MDESC MDESC,a.UNIT,a.DQMCODE from TBLStorageDetail a,TBLInvInOutTrans b where a.DQMCODE=b.DQMCODE and a.StorageCode=b.StorageCode and a.StorageCode='" + StorageCode + "' and b.mdate>" + cdate + " and b.mdate<" + enddate + " group by a.DQMCODE,a.MDESC,a.UNIT,a.CARTONNO";

            object[] o = this.DataProvider.CustomQuery(typeof(Do), new SQLCondition(sql));
            List<Do> ds = new List<Do>();

            if (o != null && o.Length > 0)
            {
                foreach (Do d in o)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        //add by sam
        public Do[] GetPortionStorageQty(string StorageCode, int cdate, int enddate)
        {

            string sql = "select  a.STORAGEQTY sum , a.locationcode ,  a.CARTONNO,a.MDESC MDESC,a.UNIT,a.DQMCODE from TBLStorageDetail a,TBLInvInOutTrans b where a.DQMCODE=b.DQMCODE and a.StorageCode=b.StorageCode and a.StorageCode='" + StorageCode + "' and b.mdate>" + cdate + " and b.mdate<" + enddate + "  group by a.DQMCODE,a.MDESC,a.UNIT,a.CARTONNO,a.locationcode ,a.STORAGEQTY ";

            object[] o = this.DataProvider.CustomQuery(typeof(Do), new SQLCondition(sql));
            List<Do> ds = new List<Do>();

            if (o != null && o.Length > 0)
            {
                foreach (Do d in o)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        public object[] GetStockCheckDetails(string checkNo, string storageCode, string DQMCODE, int inclusive, int exclusive)
        {
            string sql = "SELECT a.StorageCode,a.DQMCODE,sum(a.STORAGEQTY) STORAGEQTY,sum(a.CheckQty) CheckQty,a.diffdesc FROM TBLStockCheckDetail a WHERE 1=1";
            if (!string.IsNullOrEmpty(checkNo))
            {

                sql += string.Format(" AND a.checkNo = '{0}'", checkNo);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {

                sql += string.Format(" AND a.StorageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(DQMCODE))
            {
                sql += string.Format(" AND a.DQMCODE lIKE'%{0}%'", DQMCODE);
            }

            sql += string.Format("group by   a.StorageCode,a.DQMCODE,a.diffdesc ");

            return this.DataProvider.CustomQuery(typeof(StockCheckDetail), new PagerCondition(sql, "a.DQMCODE DESC", inclusive, exclusive));
        }

        public int GetStockCheckDetailsCount(string checkNo, string storageCode, string DQMCODE)
        {
            string sql = "SELECT a.StorageCode,a.DQMCODE,sum(a.STORAGEQTY),sum(a.CheckQty) FROM TBLStockCheckDetail a WHERE 1=1";
            if (!string.IsNullOrEmpty(checkNo))
            {

                sql += string.Format(" AND a.checkNo = '{0}'", checkNo);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {

                sql += string.Format(" AND a.StorageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(DQMCODE))
            {
                sql += string.Format(" AND a.DQMCODE lIKE'%{0}%'", DQMCODE);
            }
            sql += string.Format("group by   a.StorageCode,a.DQMCODE,a.diffdesc ");

            sql = string.Format(" select count (1) from ( {0} )", sql);


            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        public object[] GetStockPortionCheckDetails(string checkNo, string locationCode, string storageCode, string DQMCODE, int inclusive, int exclusive)
        {
            string sql = @"SELECT a.StorageCode,b.LocationCode,a.DQMCODE,b.CARTONNO,a.STORAGEQTY,b.checkqty FROM TBLStockCheckDetail a 
                                left join TBLStockCheckDetailCarton b on a.CheckNo=b.CheckNo and   a.cartonno=b.scartonno and   a.LocationCode=b.sLocationCode     where 1=1 ";
            if (!string.IsNullOrEmpty(checkNo))
            {

                sql += string.Format(" AND a.checkNo = '{0}'", checkNo);
            }

            if (!string.IsNullOrEmpty(locationCode))
            {

                sql += string.Format(" AND a.locationCode = '{0}'", locationCode);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {

                sql += string.Format(" AND a.StorageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(DQMCODE))
            {
                sql += string.Format(" AND a.DQMCODE lIKE'%{0}%'", DQMCODE);
            }




            return this.DataProvider.CustomQuery(typeof(StockCheckDetailDo), new PagerCondition(sql, "a.DQMCODE DESC", inclusive, exclusive));
        }


        public StockCheckDetail[] GetPortionCheckDetailsForWebService()
        {
            string sql = "SELECT a.DQMCODE,b.locationcode,b.CARTONNO,b.CheckQTY FROM TBLStockCheckDetail a,TBLStockCheckDetailCarton b on a.CheckNo=b.CheckNo and a.DQMCODE=b.DQMCODE";

            object[] oo = this.DataProvider.CustomQuery(typeof(StockCheckDetail), new SQLCondition(sql));

            List<StockCheckDetail> ds = new List<StockCheckDetail>();

            if (oo != null && oo.Length > 0)
            {
                foreach (StockCheckDetail d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }


        public int GetStockPortionCheckDetailsCount(string checkNo, string locationCode, string storageCode, string DQMCODE)
        {
            string sql = @"SELECT count(*) FROM TBLStockCheckDetail a left join TBLStockCheckDetailCarton b 
              on a.CheckNo=b.CheckNo and a.StorageCode=b.StorageCode and a.DQMCODE=b.DQMCODE  left join TBLStockCheckDetailCarton b on a.CheckNo=b.CheckNo 
              and   a.cartonno=b.scartonno and   a.LocationCode=b.sLocationCode  where 1=1 ";
            if (!string.IsNullOrEmpty(checkNo))
            {

                sql += string.Format(" AND a.checkNo = '{0}'", checkNo);
            }

            if (!string.IsNullOrEmpty(locationCode))
            {

                sql += string.Format(" AND b.locationCode = '{0}'", locationCode);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {

                sql += string.Format(" AND a.StorageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(DQMCODE))
            {
                sql += string.Format(" AND a.DQMCODE lIKE'%{0}%'", DQMCODE);
            }



            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetLocations(string checkNo, string storageCode, string DQMCODE)
        {
            string sql = "SELECT locationCode FROM TBLStockCheckDetailCarton a where 1=1 ";
            if (!string.IsNullOrEmpty(checkNo))
            {

                sql += string.Format(" AND a.checkNo = '{0}'", checkNo);
            }


            if (!string.IsNullOrEmpty(storageCode))
            {

                sql += string.Format(" AND a.StorageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(DQMCODE))
            {
                sql += string.Format(" AND a.DQMCODE lIKE'%{0}%'", DQMCODE);
            }
            return this.DataProvider.CustomQuery(typeof(Location), new SQLCondition(sql));



        }

        public StockCheckDetailOp[] GetPortionStockCheckOpsNoPager(string checkNo)
        {
            List<StockCheckDetailOp> ops = new List<StockCheckDetailOp>();
            string sql = string.Format(@"select a.checkNo,a.StorageCode,a.DQMCODE,a.LocationCode slocationcode ,b.LocationCode LocationCode,
                a.CARTONNO scartonno , b.diffdesc,
                b.CARTONNO,   a.STORAGEQTY,b.CheckQty,a.CTIME from TBLStockCheckDetail a left join TBLStockCheckDetailCarton b 
                on a.DQMCODE=b.DQMCODE and a.CheckNo=b.checkno   and a.StorageCode=b.StorageCode where 1=1");
            // string sql = "select a.checkNo,a.StorageCode,a.DQMCODE,a.LocationCode,b.LocationCode LocationCode2,b.CARTONNO,a.STORAGEQTY,b.CheckQty,a.CTIME from TBLStockCheckDetail a left join TBLStockCheckDetailCarton b on a.DQMCODE=b.DQMCODE and a.CheckNo=b.checkno   and a.StorageCode=b.StorageCode where 1=1 ";
            if (!string.IsNullOrEmpty(checkNo))
            {
                sql += string.Format(" AND a.checkNo lIKE'%{0}%'", checkNo);
            }
            object[] os = this.DataProvider.CustomQuery(typeof(StockCheckDetailOp), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (StockCheckDetailOp op in os)
                    ops.Add(op);
            }
            return ops.ToArray();

        }

        public object[] GetPortionStockCheckOps(string checkNo, int inclusive, int exclusive)
        {
            string sql = string.Format(@"select a.checkNo,a.StorageCode,case 
                                                when b.dqmcode is null 
                                                    then a.dqmcode 
                                                 when a.dqmcode=' ' 
                                                    then b.dqmcode
                                             end dqmcode ,a.LocationCode sLocationCode ,b.LocationCode,
                a.CARTONNO scartonno , b.diffdesc,
                b.CARTONNO,   a.STORAGEQTY,b.CheckQty,a.CTIME from TBLStockCheckDetail a left join TBLStockCheckDetailCarton b 
                on  a.CheckNo=b.checkno  and  a.cartonno=b.scartonno where 1=1");
            // string sql = "select a.checkNo,a.StorageCode,a.DQMCODE,a.LocationCode,b.LocationCode LocationCode2,b.CARTONNO,a.STORAGEQTY,b.CheckQty,a.CTIME from TBLStockCheckDetail a left join TBLStockCheckDetailCarton b on a.DQMCODE=b.DQMCODE and a.CheckNo=b.checkno   and a.StorageCode=b.StorageCode where 1=1 ";
            if (!string.IsNullOrEmpty(checkNo))
            {
                sql += string.Format(" AND a.checkNo lIKE'%{0}%'", checkNo);
            }
            return this.DataProvider.CustomQuery(typeof(StockCheckDetailOp), new PagerCondition(sql, inclusive, exclusive));
        }


        public object[] GetPortionStockCheckOps(string checkNo)
        {
            string sql = string.Format(@"select a.checkNo,a.StorageCode,B.DQMCODE DQMCODE,B.sLocationCode ,b.LocationCode,
                B.scartonno , b.diffdesc,
                b.CARTONNO,   a.STORAGEQTY,b.CheckQty,a.CTIME from TBLStockCheckDetailCarton b  left join  TBLStockCheckDetail a
                on a.CheckNo=b.checkno  and  a.cartonno=b.scartonno and a.StorageCode=b.StorageCode and
               a.locationcode=b.slocationcode  where 1=1");
            // string sql = "select a.checkNo,a.StorageCode,a.DQMCODE,a.LocationCode,b.LocationCode LocationCode2,b.CARTONNO,a.STORAGEQTY,b.CheckQty,a.CTIME from TBLStockCheckDetail a left join TBLStockCheckDetailCarton b on a.DQMCODE=b.DQMCODE and a.CheckNo=b.checkno   and a.StorageCode=b.StorageCode where 1=1 ";
            if (!string.IsNullOrEmpty(checkNo))
            {
                sql += string.Format(" AND a.checkNo lIKE'%{0}%'", checkNo);
            }
            return this.DataProvider.CustomQuery(typeof(StockCheckDetailOp), new SQLCondition(sql));
        }


        public int GetPortionStockCheckOpsCount(string checkNo)
        {
            string sql = @"select count(*) from TBLStockCheckDetail a left join TBLStockCheckDetailCarton b 
               on a.CheckNo=b.checkno  and  a.cartonno=b.scartonno and a.StorageCode=b.StorageCode and
               a.locationcode=b.slocationcode
            where 1=1 ";
            if (!string.IsNullOrEmpty(checkNo))
            {
                sql += string.Format(" AND a.checkNo lIKE'%{0}%'", checkNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public StockCheckDetail GetStockCheckDetail(string checkNo, string StorageCode, string LocationCode, string CARTONNO, string DQMCODE)
        {
            string sql = @"SELECT A.* FROM TBLStockCheckDetail A WHERE A.CheckNo='" + checkNo + "' AND A.StorageCode='" + StorageCode + "' AND A.LocationCode='" + LocationCode + "' AND A.CARTONNO='" + CARTONNO + "' AND A.DQMCODE='" + DQMCODE + "'";


            object[] objs = this.DataProvider.CustomQuery(typeof(StockCheckDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return (StockCheckDetail)objs[0];
            else

                return null;

        }






        public int GetStockCheckDetailCartonCount(string checkNo, string DQMCODE, string CARTONNO)
        {
            string sql = "select count(*) from TBLStockCheckDetailCarton a where CheckNo='" + checkNo + "' and DQMCODE='" + DQMCODE + "' and CARTONNO='" + CARTONNO + "' ";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetStockCheckDetailCarton(string checkNo, string DQMCODE, string CARTONNO)
        {
            string sql = "select a.* from TBLStockCheckDetailCarton a where CheckNo='" + checkNo + "' and DQMCODE='" + DQMCODE + "' and CARTONNO='" + CARTONNO + "' ";

            return this.DataProvider.CustomQuery(typeof(StockCheckDetailCarton), new SQLCondition(sql));
        }

        public void UpdateStockCheckDetailCarton(StockCheckDetailCarton car)
        {
            this.DataProvider.Update(car);
        }

        public object GetStockCheckDetailCarton(string Dqmcode, string Checkno, string Storagecode,
            string Locationcode, string Cartonno, string sLocationcode, string sCartonno)
        {
            string sql = "select a.* from TBLStockCheckDetailCarton a where CheckNo='" + Checkno + "' and DQMCODE='" + Dqmcode + "' and Storagecode='" + Storagecode + "' ";
            sql += string.Format(" and Locationcode='{0}' and CARTONNO='{1}' and sLocationcode='{2}' and sCARTONNO='{3}'    ",
                Locationcode, Cartonno, sLocationcode, sCartonno);
            object[] list = this.DataProvider.CustomQuery(typeof(StockCheckDetailCarton), new SQLCondition(sql));
            if (list != null)
            {
                return list[0] as StockCheckDetailCarton;
            }
            return null;
        }

        public object GetStockCheckDetailCartonBysLocationcode(string Checkno, string sLocationcode, string sCartonno, string dqmcode)
        {
            string sql = string.Format(@" select a.* from TBLStockCheckDetailCarton a where CheckNo='{0}' 
             and sLocationcode='{1}' and sCARTONNO='{2}' and   dqmcode='{3}' ",
             Checkno, sLocationcode, sCartonno, dqmcode);
            object[] list = this.DataProvider.CustomQuery(typeof(StockCheckDetailCarton), new SQLCondition(sql));
            if (list != null)
            {
                return list[0] as StockCheckDetailCarton;
            }
            return null;
        }



        public void InsertStockCheckDetailCarton(BenQGuru.eMES.Domain.Warehouse.StockCheckDetailCarton s)
        {
            this.DataProvider.Insert(s);
        }

        public PickDetailDN[] GetPickDetailsForDN(string pickNo)
        {
            string sql = "SELECT b.INVNO,a.PQTY,a.DQMCODE FROM TBLPICKDETAIL a LEFT JOIN TBLPICK b ON a.pickno=b.pickno where b.pickno='" + pickNo + "' AND a.STATUS not in ('Cancel')  ";

            object[] oo = this.DataProvider.CustomQuery(typeof(PickDetailDN), new SQLCondition(sql));

            List<PickDetailDN> ds = new List<PickDetailDN>();

            if (oo != null && oo.Length > 0)
            {
                foreach (PickDetailDN d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }




        #region add by sam 转储作业
        public StorLocTransDN[] GeStorLocTransForDN(string TransNo)
        {
            string sql = @"SELECT b.INVNO,a.QTY,a.DQMCODE FROM TBLStorLocTransDetail a LEFT JOIN TBLStorLocTrans b ON a.TransNo=b.TransNo where b.TransNo='" + TransNo + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(StorLocTransDN), new SQLCondition(sql));

            List<StorLocTransDN> ds = new List<StorLocTransDN>();

            if (oo != null && oo.Length > 0)
            {
                foreach (StorLocTransDN d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }


        #endregion

        public InvoicesDetailEx[] GetInVoicesDetails(string batchCode)
        {
            string sql = @"SELECT a.INVNO,a.INVLINE,a.DQMCODE,a.MCODE,a.FromStorageCode,b.INVTYPE,a.PLANQTY,a.UNIT,
              a.FACCODE,a.MovementType  FROM TBLInvoicesDetail a LEFT JOIN TBLInvoices b ON a.INVNO=b.INVNO  where b.DNBATCHNO='" + batchCode + "'" +
                         " and a.MOVEMENTTYPE is not null AND a.STATUS not in ('Cancel')  ";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailEx), new SQLCondition(sql));

            List<InvoicesDetailEx> ds = new List<InvoicesDetailEx>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailEx d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        //add by sam 2016年5月26日
        public InvoicesDetailEx[] GetDNInVoicesDetails(string batchCode)
        {
            string sql = @"SELECT a.INVNO,a.INVLINE,a.DQMCODE,a.MCODE,a.FromStorageCode,b.INVTYPE,a.PLANQTY,a.UNIT,
              a.FACCODE,a.MovementType  FROM TBLInvoicesDetail a LEFT JOIN TBLInvoices b ON a.INVNO=b.INVNO  where b.DNBATCHNO='" + batchCode + "'" +
                         "  AND a.INVLINESTATUS not in ('Cancel')  ";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailEx), new SQLCondition(sql));

            List<InvoicesDetailEx> ds = new List<InvoicesDetailEx>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailEx d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        //add by sam 2016年6月23日
        public InvoicesDetailExt[] GetDNInVoicesDetailExt(string batchCode)
        {
            string sql = @"SELECT a.*,b.INVTYPE,b.ORDERNO,b.CUSORDERNO,b.CUSORDERNOTYPE, b.CUSBATCHNO,  b.SHIPPINGLOCATION,
            b.GFCONTRACTNO,b.ORDERREASON, b.POSTWAY,b.PICKCONDITION,b.GFFLAG,   b.INVSTATUS  FROM TBLInvoicesDetail a 
            LEFT JOIN TBLInvoices b ON a.INVNO=b.INVNO 
                  where b.DNBATCHNO='" + batchCode + "'" + "  AND a.INVLINESTATUS not in ('Cancel')  ";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailExt), new SQLCondition(sql));

            List<InvoicesDetailExt> ds = new List<InvoicesDetailExt>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailExt d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        //add by sam 2016年6月23日
        public InvoicesDetailExt[] GetDNInVoicesDetailExtByInvno(string invno)
        {
            string sql = @"SELECT a.*,b.INVTYPE,b.ORDERNO,b.CUSORDERNO,b.CUSORDERNOTYPE, b.CUSBATCHNO,  b.SHIPPINGLOCATION,
            b.GFCONTRACTNO,b.ORDERREASON, b.POSTWAY,b.PICKCONDITION,b.GFFLAG,   b.INVSTATUS  FROM TBLInvoicesDetail a 
            LEFT JOIN TBLInvoices b ON a.INVNO=b.INVNO 
                  where b.INVNO='" + invno + "'" + "  AND a.INVLINESTATUS not in ('Cancel')  ";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailExt), new SQLCondition(sql));

            List<InvoicesDetailExt> ds = new List<InvoicesDetailExt>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailExt d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }


        public InvoicesDetailEx[] GetInVoicesDetailsForNotDN(string batchCode)
        {
            string sql = @"SELECT a.INVNO,a.INVLINE,a.DQMCODE,a.MCODE,a.StorageCode,a.FromStorageCode,b.INVTYPE,a.PLANQTY,a.UNIT,a.FACCODE FROM TBLInvoicesDetail a LEFT JOIN TBLInvoices b ON a.INVNO=b.INVNO  where b.INVNO='" + batchCode + "'" +
                         " AND a.invlinestatus not in ('Cancel')  ";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailEx), new SQLCondition(sql));

            List<InvoicesDetailEx> ds = new List<InvoicesDetailEx>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailEx d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }



        public StorageDetail[] GetStorageDetailsFromCARTONNO(string CARTONNO)
        {
            string sql = "SELECT a.* from tblstoragedetail a where a.CARTONNO='" + CARTONNO + "' ";

            object[] oo = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));

            List<StorageDetail> ds = new List<StorageDetail>();

            if (oo != null && oo.Length > 0)
            {
                foreach (StorageDetail d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        public int GetStorageDetailsForQty(string MCode, string StorageCode)
        {
            string sql = "SELECT sum(a.StorageQty) as  StorageQty from tblstoragedetail a where a.MCODE='" + MCode + "' and a.StorageCode='" + StorageCode + "' ";
            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));
            if (objs != null)
            {
                StorageDetail StorageDetail = objs[0] as StorageDetail;
                if (StorageDetail != null)
                {
                    return StorageDetail.StorageQty;
                }
            }
            return 0;
        }

        public StorageDetail[] GetStorageDetails(string MCode, string StorageCode)
        {
            string sql = "SELECT a.* from tblstoragedetail a where a.MCODE='" + MCode + "' and a.StorageCode='" + StorageCode + "' ";

            object[] oo = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));

            List<StorageDetail> ds = new List<StorageDetail>();

            if (oo != null && oo.Length > 0)
            {
                foreach (StorageDetail d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        public Pickdetailmaterial GetFirstCheckInFromPickMaterial(string pickNo, List<string> sns)
        {
            string sql = "SELECT b.* FROM(select * from TBLPICKDetailMaterial where pickno='" + pickNo + "') b  ,(select * from TBLPICKDetailMaterialSN where pickno='" + pickNo + "' and sn in (" + SqlFormat(sns) + ")) a  WHERE a.pickno=b.pickno and a.CARTONNO=b.CARTONNO";
            object[] oo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            List<Pickdetailmaterial> ds = new List<Pickdetailmaterial>();

            if (oo != null && oo.Length > 0)
            {
                foreach (Pickdetailmaterial d in oo)
                    ds.Add(d);
            }
            if (ds.Count > 0)
            {
                return ds[0];
            }
            return null;

        }

        public Asndetail[] GetIQCCloseAsnDetails(string stno)
        {
            string sql = "SELECT * FROM TBLASNDETAIL WHERE STNO='" + stno + "' AND STATUS='IQCClose'";
            object[] oo = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));

            List<Asndetail> ds = new List<Asndetail>();

            if (oo != null && oo.Length > 0)
            {
                foreach (Asndetail d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }


        public Asndetail[] GetAsnDetails(string stno)
        {
            string sql = "SELECT * FROM TBLASNDETAIL WHERE STNO='" + stno + "'";
            object[] oo = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));

            List<Asndetail> ds = new List<Asndetail>();

            if (oo != null && oo.Length > 0)
            {
                foreach (Asndetail d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        //public StorageDetail[] GetStorageDetailsFromDQMCODE(string DQMCODE)
        //{
        //    string sql = "SELECT a.* from tblstoragedetail a where a.DQMCODE='" + DQMCODE + "' ";

        // }
        public StorageDetail[] GetStockCheckDetailCartons(string checkNo, string storageCode, string locationCode, string CARTONNO, string DQMCode, int checkQty, string userCode)
        {
            string sql = "SELECT a.* from tblstoragedetail";

            object[] oo = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));

            List<StorageDetail> ds = new List<StorageDetail>();

            if (oo != null && oo.Length > 0)
            {
                foreach (StorageDetail d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }


        public void InsertDNLOG(DNLOG LOG)
        {
            string sql = string.Empty;
            sql = sql + "INSERT INTO TBLDNLOG ";
            sql = sql + "(DNLINE,DNNO,QTY,UNIT,MDATE,MTIME,MUSER,ISALL,MESSAGE,DNBATCHNO,RESULT)";
            sql = sql + "VALUES ";
            sql = sql + "( " + LOG.DNLINE + ",'" + LOG.DNNO + "',";
            sql = sql + LOG.Qty + ",'" + LOG.Unit + "'," + LOG.MDATE + "," + LOG.MTIME + ",'" + LOG.MUSER + "','" + LOG.ISALL + "','" + LOG.MESSAGE + "','" + LOG.DNBATCHNO + "','" + LOG.RESULT + "')";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }

        public void AddStockCheckDetailCarton(string checkNo, string storageCode, string locationCode, string CARTONNO, string DQMCode, int checkQty, string userCode)
        {
            StockCheckDetailCarton s = new StockCheckDetailCarton();
            s.CARTONNO = CARTONNO;
            s.CheckNo = checkNo;
            s.CheckQty = checkQty;
            s.DQMCODE = DQMCode;
            s.LocationCode = locationCode;
            s.StorageCode = storageCode;
            s.CDATE = FormatHelper.TODateInt(DateTime.Now);
            s.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
            s.CUSER = userCode;
            s.MDATE = FormatHelper.TODateInt(DateTime.Now);
            s.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
            s.MUSER = userCode;
            InsertStockCheckDetailCarton(s);
        }

        public void AddStockCheckDetailCarton(string checkNo, string storageCode, string locationCode, string CARTONNO, string DQMCode, int checkQty, string userCode, string SLocationCode, string SCARTONNO)
        {
            StockCheckDetailCarton s = new StockCheckDetailCarton();
            s.CARTONNO = CARTONNO;
            s.CheckNo = checkNo;
            s.CheckQty = checkQty;
            s.DQMCODE = DQMCode;
            s.LocationCode = locationCode;
            s.StorageCode = storageCode;
            s.SLocationCode = SLocationCode;
            s.SCARTONNO = SCARTONNO;
            s.CDATE = FormatHelper.TODateInt(DateTime.Now);
            s.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
            s.CUSER = userCode;
            s.MDATE = FormatHelper.TODateInt(DateTime.Now);
            s.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
            s.MUSER = userCode;
            InsertStockCheckDetailCarton(s);
        }

        public PickDetail[] QueryPicksForLoadCases(List<string> PickNos)
        {
            StringBuilder sb = new StringBuilder(200);
            foreach (string pickNo in PickNos)
            {
                sb.Append("'");
                sb.Append(pickNo);
                sb.Append("',");
            }

            string sql = "SELECT b.DQSITEMCODE,b.CUSITEMDESC,b.qty,b.unit,b.GFHWITEMCODE FROM TBLPICK a,TBLPICKDETAIL b where a.PICKNO=b.PICKNO AND a.GFFLAG='X' AND B.PICKNO in(" + sb.ToString().TrimEnd(',') + ")";
            object[] oo = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));

            List<PickDetail> ps = new List<PickDetail>();

            if (oo != null && oo.Length > 0)
            {
                foreach (PickDetail d in oo)
                    ps.Add(d);
            }
            return ps.ToArray();
        }



        public PickInfo1[] QueryPackageReceipts1(List<string> carInvNos)
        {
            string sql = @"select a.CARINVNO, a.CARTONNO,b.VENDERITEMCODE,c.CUSBATCHNO,a.DQMCODE,b.HWTYPEINFO,
                            a.GFHWITEMCODE,c.invno,
                            b.MDESC,b.PACKINGWAYNO ,b.unit,a.qty,C.PICKTYPE,b.CUSTMCODE from
                            TBLCartonInvDetailMaterial a , TBLPICKDETAIL b,tblpick c where 
                            a.PICKNO=b.PICKNO  and a.DQMCODE = b.DQMCODE and a.pickno=c.pickno ";

            StringBuilder sb = new StringBuilder(200);
            foreach (string carinvNo in carInvNos)
            {
                sb.Append("'");
                sb.Append(carinvNo);
                sb.Append("',");
            }



            sql += string.Format(" AND a.CARINVNO in ({0})", sb.ToString().TrimEnd(','));

            object[] oo = this.DataProvider.CustomQuery(typeof(PickInfo1), new SQLCondition(sql));
            List<PickInfo1> picks = new List<PickInfo1>();
            if (oo != null && oo.Length > 0)
            {
                foreach (PickInfo1 pick in oo)
                {
                    picks.Add(pick);
                }
            }
            return picks.ToArray();
        }

        public CARTONINVDETAILSN[] QueryCARTONINVDETAILSNs(string CARTONINV, string CARTONNO)
        {
            string sql = @"select distinct SN from TBLCartonInvDetailSN where CARINVNO='" + CARTONINV + "' and CARTONNO='" + CARTONNO + "'";
            object[] oo = this.DataProvider.CustomQuery(typeof(CARTONINVDETAILSN), new SQLCondition(sql));
            List<CARTONINVDETAILSN> cs = new List<CARTONINVDETAILSN>();
            if (oo != null && oo.Length > 0)
            {
                foreach (CARTONINVDETAILSN o in oo)
                {
                    cs.Add(o);
                }
            }
            return cs.ToArray();
        }

        public CARTONINVDETAILSN[] QueryCARTONINVDETAILSNs(string cartonno)
        {
            string sql = "select sn from TBLCARTONINVDETAILSN where cartonno='" + cartonno + "'";
            object[] oo = this.DataProvider.CustomQuery(typeof(CARTONINVDETAILSN), new SQLCondition(sql));
            List<CARTONINVDETAILSN> cs = new List<CARTONINVDETAILSN>();
            if (oo != null && oo.Length > 0)
            {
                foreach (CARTONINVDETAILSN o in oo)
                {
                    cs.Add(o);
                }
            }
            return cs.ToArray();
        }

        public CARTONINVDETAILSN[] QuerySNsFromPickNoAndDQMCode(string pickno, string dqmCode)
        {
            string sql = "select distinct b.sn from TBLPICKDETAILMATERIAL a,TBLPICKDETAILMATERIALSN b where a.pickno=b.pickno and a.pickline=b.pickline and a.pickno='" + pickno + "' and a.dqmcode='" + dqmCode + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(CARTONINVDETAILSN), new SQLCondition(sql));
            List<CARTONINVDETAILSN> cs = new List<CARTONINVDETAILSN>();
            if (oo != null && oo.Length > 0)
            {
                foreach (CARTONINVDETAILSN o in oo)
                {
                    cs.Add(o);
                }
            }
            return cs.ToArray();
        }

        public BenQGuru.eMES.Domain.MOModel.Material GetMaterial(string mCode)
        {
            object o = this.DataProvider.CustomSearch(typeof(BenQGuru.eMES.Domain.MOModel.Material), new object[] { mCode });
            if (o != null)
                return (BenQGuru.eMES.Domain.MOModel.Material)o;
            else
                return new BenQGuru.eMES.Domain.MOModel.Material();
        }

        //public void UpdateStorageDetail(StorageDetail s)
        //{
        //    this.DataProvider.Update(s);
        //}

        public void InsertStorageDetail(StorageDetail s)
        {
            this.DataProvider.Insert(s);
        }

        public StorageDetail GetStorageDetailsFromCARTONNOAndStorageCode(string CARTONNO, string StorageCode)
        {
            string sql = "SELECT a.* from tblstoragedetail a where a.CARTONNO='" + CARTONNO + "' and StorageCode='" + StorageCode + "' ";

            object[] oo = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));



            if (oo != null && oo.Length > 0)
            {
                return (StorageDetail)oo[0];
            }
            return null;
        }



        public PickDetailMaterialSn[] GetPickDetailMaterialSns(string pickNo)
        {
            string sql = "SELECT a.* FROM TBLPICKDetailMaterialSN a where PickNo='" + pickNo + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(PickDetailMaterialSn), new SQLCondition(sql));
            List<PickDetailMaterialSn> ds = new List<PickDetailMaterialSn>();

            if (oo != null && oo.Length > 0)
            {
                foreach (PickDetailMaterialSn d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }


        public CartonInvDetailMaterial[] GetCartonInvDetailMaterial(string pickNo)
        {
            string sql = "select a.dqmcode,a.CARTONNO,a.mcode,sum(qty) qty from TBLCartonInvDetailMaterial a where PICKNO='" + pickNo + "' group by dqmcode,CARTONNO,mcode";
            object[] oo = this.DataProvider.CustomQuery(typeof(CartonInvDetailMaterial), new SQLCondition(sql));

            List<CartonInvDetailMaterial> ds = new List<CartonInvDetailMaterial>();
            if (oo != null && oo.Length > 0)
            {
                foreach (CartonInvDetailMaterial d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        public CARTONINVDETAILSN[] GetCartonInvDetailSn(string cartonno, string pickNo)
        {
            string sql = "select distinct a.sn,a.CARTONNO from TBLCartonInvDetailSN a where CARTONNO='" + cartonno + "' and pickno='" + pickNo + "'";
            object[] oo = this.DataProvider.CustomQuery(typeof(CARTONINVDETAILSN), new SQLCondition(sql));
            List<CARTONINVDETAILSN> ds = new List<CARTONINVDETAILSN>();
            if (oo != null && oo.Length > 0)
            {
                foreach (CARTONINVDETAILSN d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();

        }


        public CARTONINVDETAILSN[] GetCartonInvDetailSn(string pickNo)
        {
            string sql = "select a.* from TBLCartonInvDetailSN a where  pickno='" + pickNo + "'";
            object[] oo = this.DataProvider.CustomQuery(typeof(CARTONINVDETAILSN), new SQLCondition(sql));
            List<CARTONINVDETAILSN> ds = new List<CARTONINVDETAILSN>();
            if (oo != null && oo.Length > 0)
            {
                foreach (CARTONINVDETAILSN d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();

        }

        public void InsertStorageDetailSN(StorageDetailSN s)
        {
            this.DataProvider.Insert(s);
        }

        public Asndetail GetFirstCheckInAsnDetail(List<string> sns)
        {
            string sql = "select a.* from TBLASNDETAIL a,(SELECT stno,sn,CARTONNO from TBLASNDETAILSN where SN in(" + SqlFormat(sns) + ")) b where a.STATUS='Close' and a.STNO=b.stno and a.CARTONNO=b.CARTONNO order by a.MDATE,a.mtime ";
            object[] oo = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
            List<Asndetail> ds = new List<Asndetail>();

            if (oo != null && oo.Length > 0)
            {
                foreach (Asndetail d in oo)
                    ds.Add(d);
            }
            if (ds.Count > 0)
            {
                return ds[0];
            }
            return null;

        }


        public Pickdetailmaterial GetFirstCheckInPickMaterialFromDQMCode(string DQMCode)
        {

            string sql = "select a.* from TBLPICKDetailMaterial a where DQMCODE='" + DQMCode + "' order by MDATE,mtime";
            object[] oo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            List<Pickdetailmaterial> ds = new List<Pickdetailmaterial>();

            if (oo != null && oo.Length > 0)
            {
                foreach (Pickdetailmaterial d in oo)
                    ds.Add(d);
            }
            if (ds.Count > 0)
            {
                return ds[0];
            }
            return null;

        }


        private string SqlFormat(string[] strs)
        {
            if (strs.Length == 0)
                return "''";
            System.Text.StringBuilder sb = new System.Text.StringBuilder(200);

            foreach (string str in strs)
            {
                sb.Append("'");
                sb.Append(str);
                sb.Append("',");

            }
            return sb.ToString().TrimEnd(',');
        }


        private string SqlFormat(List<string> strs)
        {
            if (strs.Count == 0)
                return "''";
            System.Text.StringBuilder sb = new System.Text.StringBuilder(200);

            foreach (string str in strs)
            {
                sb.Append("'");
                sb.Append(str);
                sb.Append("',");

            }
            return sb.ToString().TrimEnd(',');
        }
        public Pickdetailmaterial[] GetPickdetailmaterials(string pickNo)
        {
            string sql = "SELECT a.* FROM TBLPICKDetailMaterial	a where a.PickNo='" + pickNo + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            List<Pickdetailmaterial> ds = new List<Pickdetailmaterial>();

            if (oo != null && oo.Length > 0)
            {
                foreach (Pickdetailmaterial d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }


        public CARTONINVOICES[] GetGrossAndWeight(string pickNo)
        {
            string sql = "SELECT a.* FROM TBLCartonInvoices	a where a.PickNo='" + pickNo + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(CARTONINVOICES), new SQLCondition(sql));
            List<CARTONINVOICES> ds = new List<CARTONINVOICES>();

            if (oo != null && oo.Length > 0)
            {
                foreach (CARTONINVOICES d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }


        public bool CompareStorageAvailableQty(string storageCode, string DQMCode, decimal compareQty)
        {
            string sql = "SELECT SUM(AVAILABLEQTY) FROM TBLSTORAGEDETAIL WHERE STORAGECODE='" + storageCode + "' AND DQMCODE='" + DQMCode + "'";

            int availableQty = this.DataProvider.GetCount(new SQLCondition(sql));
            if ((decimal)availableQty < compareQty)
            {
                return false;
            }
            return true;
        }


        public Pickdetailmaterial GetLotProductDateInfoFromSN(string[] sns, string invno)
        {
            string sql = "select c.* from (select a.* from TBLInvoices a where ORDERNO=(select ORDERNO from TBLInvoices where INVNO='" + invno + "'))a,tblpick b ,TBLPICKDetailMaterial c,TBLPICKDetailMaterialSN d where a.DNBATCHNO=b.invno and b.pickno=c.pickno and c.pickno=d.pickno and c.pickline=d.pickline and d.sn in(" + SqlFormat(sns) + ") ";


            object[] oo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
                return (Pickdetailmaterial)oo[0];
            else
                return null;
        }

        public Pickdetailmaterial GetLotProductDateInfoFromMCode(string DQMCode)
        {
            string sql = "SELECT b.* FROM TBLPICKDetailMaterial b where b.DQMCODE='" + DQMCode + "' order by MDATE,mtime";

            object[] oo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
                return (Pickdetailmaterial)oo[0];
            else
                return null;
        }

        public Pickdetailmaterial GetFirstCheckInPickMaterialFromDQMCode(string invno, string DQMCode)
        {

            string sql = "select c.* from (select a.* from TBLInvoices a where ORDERNO=(select ORDERNO from TBLInvoices where INVNO='" + invno + "'))a,tblpick b ,TBLPICKDetailMaterial c where a.DNBATCHNO=b.invno and b.pickno=c.pickno and c.DQMCODE='" + DQMCode + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
                return (Pickdetailmaterial)oo[0];
            else
                return null;

        }

        #region wwpo add by sam  2016年4月18日
        #region Wwpo
        /// <summary>
        /// TBLWWPO
        /// </summary>
        public MesWWPO CreateNewMesWWPO()
        {
            return new MesWWPO();
        }

        public void AddMesWWPO(MesWWPO wwpo)
        {
            this.DataProvider.Insert(wwpo);
        }

        public void DeleteMesWWPO(MesWWPO wwpo)
        {
            this.DataProvider.Delete(wwpo);
        }

        public void UpdateMesWWPO(MesWWPO wwpo)
        {
            this.DataProvider.Update(wwpo);
        }

        public object GetMesWWPO(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(MesWWPO), new object[] { Serial });
        }

        public void DeleteWWPOByPoNo(string pono)
        {
            string sql = "delete tblwwpo where pono='" + pono + "'  ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public decimal GetWWPOQty(string invno, int invline, string mcode)
        {
            string sql = string.Format(@" select a.pono,a.poline,a.mcode , NVL(sum(a.qty),0) qty  from  
               TBLWWPO a  where  a.pono='{0}' and  a.poline='{1}'  and a.mcode='{2}'  group  by  a.pono,a.poline,a.mcode ", invno, invline, mcode);
            object[] objSpecStorageInfo = this.DataProvider.CustomQuery(typeof(MesWWPO), new SQLCondition(sql));
            if (objSpecStorageInfo != null && objSpecStorageInfo.Length > 0)
            {
                return ((MesWWPO)objSpecStorageInfo[0]).Qty;
            }
            return 0;
        }

        public decimal GetPickDetailQty(string invno, int invline, string mcode)
        {
            string sql = string.Format(@" select a.qty from 
                (select a.invno,b.invline, b.mcode , NVL(sum(b.qty),0) qty  from tblpick a,tblpickdetail b where
                a.pickno=b.pickno group by a.invno,b.invline , b.mcode) a
                where  a.invno='{0}' and  a.invline='{1}' and  a.mcode='{2}' ", invno, invline, mcode);
            object[] objSpecStorageInfo = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            if (objSpecStorageInfo != null && objSpecStorageInfo.Length > 0)
            {
                return ((PickDetail)objSpecStorageInfo[0]).QTY;
            }
            return 0;
        }

        #endregion



        public object[] QuerySelectedWWpoInvNo(string codes)
        {
            string sql = string.Format(
                @"  SELECT a.*,b.dqmcode,b.mchlongdesc,c.dqmcode cpdqmcode ,c.menshortdesc cpmdesc  FROM tblwwpo a
                            left join tblmaterial b on a.mcode=b.mcode
                           left join tblinvoicesdetail c on  c.invno = a.pono and c.invline= a.poline
                           where 1=1  and a.serial in ({0}) ", codes);
            return this.DataProvider.CustomQuery(typeof(MesWWPOExc), new SQLCondition(sql));
        }

        #endregion

        public bool IsAsnFinish(string stNo)
        {
            string sql = "SELECT a.TOTAL,b.CLOSECOUNT,C.REJECTCOUNT FROM (SELECT COUNT(*) TOTAL,stno FROM TBLASNDETAIL  group by stno) a LEFT JOIN" +
                                        "(SELECT COUNT(*) CloseCount,stno FROM TBLASNDETAIL WHERE  STATUS='Close'  group by stno) b ON A.STNO=B.STNO LEFT JOIN" +
                                        "(SELECT  COUNT(*) RejectCount,stno FROM TBLASNDETAIL WHERE  STATUS IN('IQCReject','Cancel','IQCRejection') or InitReceiveStatus='Reject'  group by stno) c ON A.STNO=C.STNO " +
                                        "WHERE  a.stno='" + stNo + "'";

            object[] o = this.DataProvider.CustomQuery(typeof(AsnDetailCountCollection), new SQLCondition(sql));

            if (o == null || o.Length == 0)
                return false;


            AsnDetailCountCollection collection = (AsnDetailCountCollection)o[0];
            if (collection.CloseCount == 0)
                Log.Error(stNo + "的条目信息必须至少有一个为Close");
            //Log.Error("-------------------------------------CloseCount" + collection.CloseCount + "-------------------------------------");
            //Log.Error("------------------------------------RejectCount+CloseCount" + collection.RejectCount + collection.CloseCount + "-----------------------------------");

            if ((collection.CloseCount > 0) && ((collection.RejectCount + collection.CloseCount) == collection.Total))
                return true;
            else
                return false;
        }

        public InvoicesDetailDN[] GetDQMCodeForDN(string pickNo, List<string> dqmCodes)
        {
            string sql = "SELECT distinct a.InvNo FROM TBLINVOICESDETAIL a,TBLINVOICES b,tblpick c where a.INVNO=b.INVNO and b.DNBATCHNO=c.INVnO  and c.pickno='" + pickNo + "' and a.DQMCODE in(" + SqlFormat(dqmCodes) + ") ";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailDN), new SQLCondition(sql));

            List<InvoicesDetailDN> ds = new List<InvoicesDetailDN>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailDN d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();

        }

        public Pickdetailmaterial GetLotForSCTR(string stNo, string[] sns)
        {

            string sql = "select e.* from TBLASN a,TBLInvoices b,TBLPICK c,(select * from TBLPICKDetailMaterialSN where sn in(" + SqlFormat(sns) + ") ) d,TBLPICKDetailMaterial e  where a.VENDORCODE=b.VENDORCODE and b.INVNO=c.INVNO and c.PICKNO=d.PICKNO and d.pickno=e.pickno and d.CARTONNO=e.CARTONNO and a.stno='" + stNo + "' order by d.mdate,d.mtime";

            object[] oo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));



            if (oo != null && oo.Length > 0)
            {
                return (Pickdetailmaterial)oo[0];
            }
            return null;

        }

        public Pickdetailmaterial GetLotForSCTRFromDQMCode(string stNo, string dqmCode)
        {

            string sql = "select d.* from TBLASN a,TBLInvoices b,TBLPICK c,TBLPICKDetailMaterial d  where a.VENDORCODE=b.VENDORCODE and b.INVNO=c.INVNO and c.PICKNO=d.PICKNO and d.DQMCODE='" + dqmCode + "' and a.stno='" + stNo + "' order by d.mdate,d.mtime";

            object[] oo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));

            if (oo != null && oo.Length > 0)
            {
                return (Pickdetailmaterial)oo[0];
            }
            return null;

        }

        public StorageDetailSN GetStorageDetailSnFromCartonnos(List<string> cartonnos)
        {
            string sql = "select a.* from TBLStorageDetailSN a where CARTONNO in(" + SqlFormat(cartonnos) + ") order by mdate ,mtime ";
            object[] oo = this.DataProvider.CustomQuery(typeof(StorageDetailSN), new SQLCondition(sql));

            if (oo != null && oo.Length > 0)
            {
                return (StorageDetailSN)oo[0];
            }
            else
                return null;
        }

        public Asndetailsn[] GetAsnDetailSNs(string stno, string stLine)
        {
            string sql = "select a.* from TBLASNDETAILSN a where stno='" + stno + "' and stline='" + stLine + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));

            List<Asndetailsn> ds = new List<Asndetailsn>();

            if (oo != null && oo.Length > 0)
            {
                foreach (Asndetailsn d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();

        }



        #region StorageDetail--库存明细信息 add by sam
        /// <summary>
        /// TBLSTORAGEDETAIL--库存明细信息
        /// </summary>
        public StorageDetail CreateNewStorageDetail()
        {
            return new StorageDetail();
        }

        public void AddStorageDetail(StorageDetail storagedetail)
        {
            this.DataProvider.Insert(storagedetail);

        }

        public void DeleteStorageDetail(StorageDetail storagedetail)
        {
            this._helper.DeleteDomainObject(storagedetail);
        }

        public void UpdateStorageDetail(StorageDetail storagedetail)
        {

            this.DataProvider.Update(storagedetail);

        }

        public object GetStorageDetail(string Cartonno)
        {
            return this.DataProvider.CustomSearch(typeof(StorageDetail), new object[] { Cartonno });
        }


        public void UpdatePickDetailForDNCZ(PickDetail pd)
        {
            string sql = "UPDATE tblpickdetail  SET STATUS='" + pd.Status + "',PQTY= " + pd.PQTY + " WHERE  pickNo='" + pd.PickNo + "' and PICKLINE='" + pd.PickLine + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdatePickForDNCZ(Pick pick, string user)
        {
            string sql = "UPDATE tblpick  SET STATUS='" + pick.Status + "',Down_User='" + user + "', Down_Date=sys_date,Down_Time=sys_time WHERE  pickNo='" + pick.PickNo + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        #endregion




        public CartonInvDetailMaterial[] GetGridTableForPackageWinc(string pickNo)
        {
            List<CartonInvDetailMaterial> lll = new List<CartonInvDetailMaterial>();
            string sql = "select QTY,CARTONNO ,DQMCODE from TBLCartonInvDetailMaterial where pickno='" + pickNo + "'";
            object[] oo = this.DataProvider.CustomQuery(typeof(CartonInvDetailMaterial), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {
                foreach (CartonInvDetailMaterial c in oo)
                {
                    lll.Add(c);
                }
            }
            return lll.ToArray();
        }



        public InvoicesDetail[] GetGetInvoicesDetailFromInvNo(string INVNO)
        {
            List<InvoicesDetail> lll = new List<InvoicesDetail>();
            string sql = "select a.* from TBLInvoicesDetail a where INVNO='" + INVNO + "'";
            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetail c in oo)
                {
                    lll.Add(c);
                }
            }
            return lll.ToArray();
        }

        public object[] GetORDERNOInInvoicesbyInvNo(string invno)
        {
            string sql = string.Format(@"(SELECT  distinct  ORDERNO FROM TBLINVOICES   WHERE dnbatchno in ({0})  
                    union 
                    SELECT distinct   ORDERNO FROM TBLINVOICES  WHERE INVNO in ({0}) ) ORDER BY ORDERNO "
                                      , invno);
            return this.DataProvider.CustomQuery(typeof(Invoices), new SQLCondition(sql));
        }





        public bool CheckAlter(string invNo)
        {
            string sql1 = @"SELECT t3.dqmcode,t2.qty,t3.planqty FROM (SELECT e.invno invno,e.invline invline,NVL (SUM (e.planqty), 0) planqty,e.dqmcode
          FROM TBLInvoicesDetail e where (e.INVLINESTATUS='Release' or e.INVLINESTATUS is null) AND E.INVNO='" + invNo + @"'
           group by e.invno,e.invline,e.dqmcode  ) t3 LEFT JOIN (SELECT NVL (SUM (a.QCPASSQTY), 0) qty,
                 a.INVNO,a.invline,A.DQMCODE
            FROM TBLASNDETAILITEM a, TBLASNDETAIL c,tblasn d 
           WHERE     c.STLINE = a.stline
                 AND c.STNO = a.stno and d.stno=a.stno
                 AND a.INVNO = '" + invNo + @"'
                 AND (c.status <> 'Cancel') and d.sttype<>'SCTR'
        GROUP BY a.INVNO, a.INVLINE,A.DQMCODE) T2 on t2.INVNO = t3.invno AND t2.INVLINE = t3.INVLINE and t2.dqmcode=t3.dqmcode";

            object[] oo1 = this.DataProvider.CustomQuery(typeof(QTYN), new SQLCondition(sql1));

            string sql2 = @"SELECT f.planqty,d.qty,d.dqmcode
  FROM (  SELECT NVL (SUM (a.QCPASSQTY), 0) qty,
                 a.INVNO,a.invline,a.dqmcode
            FROM TBLASNDETAILITEM a, TBLASNDETAIL c,TBLASN D
           WHERE     c.STLINE = a.stline 
                 AND c.STNO = a.stno AND D.STNO=A.STNO
                 AND a.INVNO = '" + invNo + @"'
                 AND (c.status <> 'Cancel') AND D.STTYPE<>'SCTR'
        GROUP BY a.INVNO, a.INVLINE,a.dqmcode) d left join
       (SELECT e.invno invno,e.invline invline,  NVL (SUM (e.planqty), 0) planqty
          FROM TBLInvoicesDetail e where (e.INVLINESTATUS='Release' or e.INVLINESTATUS is null) 
          and e.invno='" + invNo + @"' group by e.invno,e.invline,e.dqmcode ) f
 on f.INVNO = d.invno AND f.INVLINE = d.INVLINE";
            object[] oo2 = this.DataProvider.CustomQuery(typeof(QTYN), new SQLCondition(sql2));
            if (oo2 == null)
                return true;
            if (oo1 == null)
                throw new Exception("单据" + invNo + "已经变更!");

            bool isCheck1Pass = false;
            foreach (QTYN q in oo1)
            {
                if (q.PlanQTY > q.QTY)
                    isCheck1Pass = true;
            }

            bool isCheck2Pass = false;

            foreach (QTYN q in oo2)
            {
                if (q.PlanQTY > q.QTY)
                    isCheck2Pass = true;
            }

            if (isCheck1Pass || isCheck2Pass)
                return true;
            else
                return false;
        }


        public bool IsInvoicesAvailable(string invNo)
        {
            string sql1 = @"SELECT t3.dqmcode,t2.qty,t3.planqty FROM (SELECT e.invno invno,e.invline invline,NVL (SUM (e.planqty), 0) planqty,e.dqmcode
          FROM TBLInvoicesDetail e where (e.INVLINESTATUS='Release' or e.INVLINESTATUS is null) AND E.INVNO='" + invNo + @"'
           group by e.invno,e.invline,e.dqmcode  ) t3 LEFT JOIN (SELECT NVL (SUM (a.QCPASSQTY), 0) qty,
                 a.INVNO,a.invline,A.DQMCODE
            FROM TBLASNDETAILITEM a, TBLASNDETAIL c,TBLASN D
           WHERE     c.STLINE = a.stline
                 AND c.STNO = a.stno
                 AND a.INVNO = '" + invNo + @"'
                 AND (c.status <> 'Cancel') AND D.STNO=A.STNO AND D.STTYPE<>'SCTR'
        GROUP BY a.INVNO, a.INVLINE,A.DQMCODE) T2 on t2.INVNO = t3.invno AND t2.INVLINE = t3.INVLINE and t2.dqmcode=t3.dqmcode
";

            object[] oo1 = this.DataProvider.CustomQuery(typeof(QTYN), new SQLCondition(sql1));

            string sql2 = @"SELECT f.planqty,d.qty,d.dqmcode
  FROM (  SELECT NVL (SUM (a.QCPASSQTY), 0) qty,
                 a.INVNO,a.invline,a.dqmcode
            FROM TBLASNDETAILITEM a, TBLASNDETAIL c ,tblasn d
           WHERE     c.STLINE = a.stline
                 AND c.STNO = a.stno and d.stno=c.stno
                 AND a.INVNO = '" + invNo + @"'
                 AND (c.status <> 'Cancel') and d.sttype<>'SCTR'
        GROUP BY a.INVNO, a.INVLINE,a.dqmcode) d left join
       (SELECT e.invno invno,e.invline invline,  NVL (SUM (e.planqty), 0) planqty
          FROM TBLInvoicesDetail e where (e.INVLINESTATUS='Release' or e.INVLINESTATUS is null) 
          and e.invno='" + invNo + @"' group by e.invno,e.invline,e.dqmcode ) f
 on f.INVNO = d.invno AND f.INVLINE = d.INVLINE";
            object[] oo2 = this.DataProvider.CustomQuery(typeof(QTYN), new SQLCondition(sql2));
            if (oo2 == null)
                return true;
            if (oo1 == null)
                return false;

            bool isCheck1Pass = false;
            foreach (QTYN q in oo1)
            {
                if (q.PlanQTY > q.QTY)
                    isCheck1Pass = true;
            }

            bool isCheck2Pass = false;

            foreach (QTYN q in oo2)
            {
                if (q.PlanQTY > q.QTY)
                    isCheck2Pass = true;
            }

            if (isCheck1Pass || isCheck2Pass)
                return true;
            else
                return false;
        }

        public bool CheckAlterIncludeEQ(string invNo)
        {
            string sql1 = @"SELECT t3.dqmcode,t2.qty,t3.planqty FROM (SELECT e.invno invno,e.invline invline,NVL (SUM (e.planqty), 0) planqty,e.dqmcode
          FROM TBLInvoicesDetail e where (e.INVLINESTATUS='Release' or e.INVLINESTATUS is null) AND E.INVNO='" + invNo + @"'
           group by e.invno,e.invline,e.dqmcode  ) t3 LEFT JOIN (SELECT NVL (SUM (a.QCPASSQTY), 0) qty,
                 a.INVNO,a.invline,A.DQMCODE
            FROM TBLASNDETAILITEM a, TBLASNDETAIL c,tblasn d
           WHERE     c.STLINE = a.stline 
                 AND c.STNO = a.stno and a.stno=d.stno
                 AND a.INVNO = '" + invNo + @"'
                 AND (c.status <> 'Cancel') and d.sttype<>'SCTR'
        GROUP BY a.INVNO, a.INVLINE,A.DQMCODE) T2 on t2.INVNO = t3.invno AND t2.INVLINE = t3.INVLINE and t2.dqmcode=t3.dqmcode";

            object[] oo1 = this.DataProvider.CustomQuery(typeof(QTYN), new SQLCondition(sql1));

            string sql2 = @"SELECT f.planqty,d.qty,d.dqmcode
  FROM (  SELECT NVL (SUM (a.QCPASSQTY), 0) qty,
                 a.INVNO,a.invline,a.dqmcode
            FROM TBLASNDETAILITEM a, TBLASNDETAIL c,TBLASN D
           WHERE     c.STLINE = a.stline
                 AND c.STNO = a.stno AND A.STNO=D.STNO
                 AND a.INVNO = '" + invNo + @"'
                 AND (c.status <> 'Cancel') AND D.STTYPE<>'SCTR'
        GROUP BY a.INVNO, a.INVLINE,a.dqmcode) d left join
       (SELECT e.invno invno,e.invline invline,  NVL (SUM (e.planqty), 0) planqty
          FROM TBLInvoicesDetail e where (e.INVLINESTATUS='Release' or e.INVLINESTATUS is null) 
          and e.invno='" + invNo + @"' group by e.invno,e.invline,e.dqmcode ) f
 on f.INVNO = d.invno AND f.INVLINE = d.INVLINE";
            object[] oo2 = this.DataProvider.CustomQuery(typeof(QTYN), new SQLCondition(sql2));
            if (oo2 == null)
                return true;


            bool isCheck1Pass = false;
            if (oo1 == null)
                throw new Exception("单据" + invNo + "已经更改变!");
            foreach (QTYN q in oo1)
            {
                if (q.PlanQTY >= q.QTY)
                    isCheck1Pass = true;
            }

            bool isCheck2Pass = true;

            foreach (QTYN q in oo2)
            {
                if (q.PlanQTY < q.QTY)
                    isCheck2Pass = false;
            }

            if (isCheck1Pass && isCheck2Pass)
                return true;
            else
                return false;
        }



        public bool CheckAlterIncludeEQ(string invNo, string DQMCode)
        {

            string sql1 = @"SELECT t3.dqmcode,t2.qty,t3.planqty FROM (SELECT e.invno invno,e.invline invline,NVL (SUM (e.planqty), 0) planqty,e.dqmcode
          FROM TBLInvoicesDetail e where (e.INVLINESTATUS='Release' or e.INVLINESTATUS is null) AND E.INVNO='" + invNo + @"' and e.dqmcode='" + DQMCode + @"'
           group by e.invno,e.invline,e.dqmcode  ) t3 LEFT JOIN (SELECT NVL (SUM (a.QCPASSQTY), 0) qty,
                 a.INVNO,a.invline,A.DQMCODE
            FROM TBLASNDETAILITEM a, TBLASNDETAIL c,tblasn d
           WHERE     c.STLINE = a.stline
                 AND c.STNO = a.stno and a.stno=d.stno
                 AND a.INVNO = '" + invNo + @"'
                 AND (c.status <> 'Cancel') and d.sttype<>'SCTR'
        GROUP BY a.INVNO, a.INVLINE,A.DQMCODE) T2 on t2.INVNO = t3.invno AND t2.INVLINE = t3.INVLINE and t2.dqmcode=t3.dqmcode ";

            object[] oo1 = this.DataProvider.CustomQuery(typeof(QTYN), new SQLCondition(sql1));

            string sql2 = @"SELECT f.planqty,d.qty,d.dqmcode
  FROM (  SELECT NVL (SUM (a.QCPASSQTY), 0) qty,
                 a.INVNO,a.invline,a.dqmcode
            FROM TBLASNDETAILITEM a, TBLASNDETAIL c,TBLASN D
           WHERE     c.STLINE = a.stline
                 AND c.STNO = a.stno AND A.STNO=D.STNO
                 AND a.INVNO = '" + invNo + @"'
                 AND (c.status <> 'Cancel') AND D.STTYPE<>'SCTR' and a.dqmcode='" + DQMCode + @"'
        GROUP BY a.INVNO, a.INVLINE,a.dqmcode) d left join
       (SELECT e.invno invno,e.invline invline,  NVL (SUM (e.planqty), 0) planqty
          FROM TBLInvoicesDetail e where (e.INVLINESTATUS='Release' or e.INVLINESTATUS is null) 
          and e.invno='" + invNo + @"' group by e.invno,e.invline,e.dqmcode ) f
 on f.INVNO = d.invno AND f.INVLINE = d.INVLINE";
            object[] oo2 = this.DataProvider.CustomQuery(typeof(QTYN), new SQLCondition(sql2));
            if (oo2 == null)
                return true;


            bool isCheck1Pass = false;
            if (oo1 == null)
                throw new Exception("单据" + invNo + "可能已经改变!");
            foreach (QTYN q in oo1)
            {
                if (q.PlanQTY >= q.QTY)
                    isCheck1Pass = true;
            }

            bool isCheck2Pass = false;

            foreach (QTYN q in oo2)
            {
                if (q.PlanQTY >= q.QTY)
                    isCheck2Pass = true;
            }

            if (isCheck1Pass && isCheck2Pass)
                return true;
            else
                return false;
        }

        public object[] QueryASNDetailSN(string stno, string stLine, int inclusive, int exclusive)
        {
            string sql = "SELECT A.* FROM TBLASNDETAILSN A WHERE STNO='" + stno + "' AND STLINE='" + stLine + "'";
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new PagerCondition(sql, inclusive, exclusive));

        }


        public int QueryASNDetailSNCount(string stno, string stLine)
        {
            string sql = "SELECT count(*) FROM TBLASNDETAILSN A WHERE STNO='" + stno + "' AND STLINE='" + stLine + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        #region Storagesap2mes
        /// <summary>
        /// TBLSTORAGESAP2MES
        /// </summary>
        public Storagesap2mes CreateNewStoragesap2mes()
        {
            return new Storagesap2mes();
        }

        public void AddStoragesap2mes(Storagesap2mes storagesap2mes)
        {
            this.DataProvider.Insert(storagesap2mes);
        }

        public void DeleteStoragesap2mes(Storagesap2mes storagesap2mes)
        {
            this.DataProvider.Delete(storagesap2mes);
        }

        public void UpdateStoragesap2mes(Storagesap2mes storagesap2mes)
        {
            this.DataProvider.Update(storagesap2mes);
        }

        public object GetStoragesap2mes()
        {
            return this.DataProvider.CustomSearch(typeof(Storagesap2mes), new object[] { });
        }

        public object[] QueryStoragesap2mes(string MCode, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT  {0} FROM tblStoragesap2mes  WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storagesap2mes)));

            if (!string.IsNullOrEmpty(MCode))
            {
                sql += string.Format(" AND MCODE ='{0}'", MCode);
            }
            return this.DataProvider.CustomQuery(typeof(Storagesap2mes), new PagerCondition(sql, "MDATE DESC,MTIME DESC", inclusive, exclusive));
        }

        public int QueryStoragesap2mesCount(string MCode)
        {
            string sql = @" SELECT COUNT(1) FROM tblStoragesap2mes WHERE 1=1 ";
            if (!string.IsNullOrEmpty(MCode))
            {
                sql += string.Format(" AND MCode ='{0}'", MCode);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        public object[] QuerySPECSTORAGEINFO(string DQMCode, string StorageCode, int inclusive, int exclusive)
        {
            string sql = @"SELECT a.* FROM TBLSPECSTORAGEINFO a where 1=1";
            if (!string.IsNullOrEmpty(DQMCode))
            {

                sql += string.Format(" AND a.DQMCODE = '{0}'", DQMCode);
            }

            if (!string.IsNullOrEmpty(StorageCode))
            {

                sql += string.Format(" AND a.STORAGECODE = '{0}'", StorageCode);
            }
            return this.DataProvider.CustomQuery(typeof(SPECSTORAGEINFO), new PagerCondition(sql, "a.DQMCODE DESC", inclusive, exclusive));
        }


        public int QuerySPECSTORAGEINFOCOUNT(string DQMCode, string StorageCode)
        {
            string sql = @"SELECT COUNT(*) FROM TBLSPECSTORAGEINFO a where 1=1";
            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND a.DQMCODE = '{0}'", DQMCode);
            }

            if (!string.IsNullOrEmpty(StorageCode))
            {

                sql += string.Format(" AND a.STORAGECODE = '{0}'", StorageCode);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public string GetProcessPhaseTime1(string stno, string status)
        {
            string sql = @"SELECT T.* from TBLINVINOUTTRANS T WHERE T.PROCESSTYPE='" + status + @"' AND T.TransNo='" + stno + @"' order by t.mdate desc";
            object[] TRANS = this.DataProvider.CustomQuery(typeof(InvInOutTrans), new SQLCondition(sql));
            if (TRANS != null && TRANS.Length > 0)
            {
                string d = FormatHelper.ToDateString(((InvInOutTrans)TRANS[0]).MaintainDate);
                string t = FormatHelper.ToTimeString(((InvInOutTrans)TRANS[0]).MaintainTime);
                return d + " " + t;

            }
            else
                return string.Empty;
        }


        public int GetProcessPhaseDate(string stno, string status)
        {
            string sql = @"SELECT T.* from TBLINVINOUTTRANS T WHERE T.PROCESSTYPE='" + status + @"' AND T.TransNo='" + stno + @"' order by t.mdate,t.mtime desc";
            object[] TRANS = this.DataProvider.CustomQuery(typeof(InvInOutTrans), new SQLCondition(sql));
            if (TRANS != null && TRANS.Length > 0)
            {
                return ((InvInOutTrans)TRANS[0]).StorageAgeDate;


            }
            return 0;
        }

        public AsnIQC[] GetASNIQCFromASN(List<string> stnos)
        {
            string sql = @"select a.* from TBLASNIQC a where STNO in(" + SqlFormat(stnos) + ")";
            object[] oo = this.DataProvider.CustomQuery(typeof(AsnIQC), new SQLCondition(sql));
            List<AsnIQC> l = new List<AsnIQC>();
            if (oo != null && oo.Length > 0)
            {

                foreach (AsnIQC o in oo)
                {
                    l.Add(o);
                }
            }

            return l.ToArray();
        }

        public AsnIQCDetailEc[] GetIQCECFromIQCNo(List<string> IQCNo)
        {
            string sql = "SELECT A.STNO,A.STLINE FROM TBLASNIQCDETAILEC A,TBLASNIQCDETAIL B WHERE B.QCSTATUS='N' AND (A.SQESTATUS='Return' OR A.SQESTATUS='Reform') AND A.IQCNO IN (" + SqlFormat(IQCNo) + ") GROUP BY A.STNO,A.STLINE";
            object[] oo = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
            List<AsnIQCDetailEc> l = new List<AsnIQCDetailEc>();
            if (oo != null && oo.Length > 0)
            {

                foreach (AsnIQCDetailEc o in oo)
                {
                    l.Add(o);
                }
            }

            return l.ToArray();
        }

        //IQC 
        public bool IsSNNotRepeat(string sn)
        {
            //string sql = "SELECT COUNT(*) FROM TBLASNDETAILSN WHERE SN='" + sn + "' and STNO in( SELECT stno FROM TBLASN WHERE STATUS<>'Close')";
            //edit by sam  2016年7月4日
            string sql = string.Format(@"SELECT COUNT(*) FROM TBLASNDETAILSN WHERE SN='{0}' and 
                    STNO in( SELECT stno FROM TBLASN WHERE STATUS not in ( 'ReceiveRejection','IQCRejection' ))", sn);

            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
                return false;
            else
                return true;
        }

        //IQC 
        public Asn[] GetASNIncludesThisSNs(List<string> sns)
        {
            //string sql = "SELECT COUNT(*) FROM TBLASNDETAILSN WHERE SN='" + sn + "' and STNO in( SELECT stno FROM TBLASN WHERE STATUS<>'Close')";
            //edit by sam  2016年7月4日

            List<Asn> asns = new List<Asn>();
            string sql = "select * from tblasn where stno in (select stno from tblasndetailsn where sn in(" + SqlFormat(sns) + "))";

            object[] oo = this.DataProvider.CustomQuery(typeof(Asn), new SQLCondition(sql));


            if (oo != null && oo.Length > 0)
            {
                foreach (Asn asn in asns)
                {
                    asns.Add(asn);

                }

            }
            return asns.ToArray();

        }

        public bool IsCartonnoHaveOtherDQMCode(string tCartonno, string transNo, string dqmCode, out string dqmCodeHave)
        {
            dqmCodeHave = string.Empty;
            string sql = "SELECT dqmcode FROM TBLSTORLOCTRANSDETAILCARTON A WHERE A.CARTONNO='" + tCartonno + "' AND TRANSNO='" + transNo + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));


            if (objs != null && objs.Length > 0)
            {
                foreach (StorloctransDetailCarton trans in objs)
                {
                    if (trans.DqmCode != dqmCode)
                    {
                        dqmCodeHave = trans.DqmCode;
                        return true;
                    }
                }

                return false;
            }
            else
                return false;

        }


        public void UpdatePickDetailStatusToPick(string pickNo)
        {
            string sql = "UPDATE TBLPICKDETAIL SET STATUS = '" + Pick_STATUS.Status_Pick + "' WHERE pickno = '" + pickNo + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateCartonInvoicesStatusToRelease(string CARINVNO)
        {
            string sql = "UPDATE TBLCartonInvoices SET STATUS = '" + CartonInvoices_STATUS.Status_Release + "' WHERE CARINVNO = '" + CARINVNO + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateCartonInvDetailStatusToPack(string CARINVNO, string cartonno)
        {
            string sql = "UPDATE TBLCartonInvDetail SET STATUS = '" + CartonInvoices_STATUS.Status_Pack + "' WHERE CARINVNO = '" + CARINVNO + "' and CARTONNO='" + cartonno + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        public bool IsDQMCodeAndSNMatch(string dqmCode, string sn)
        {
            string sql = @"select count(*)
                              from TBLPICKDetailMaterial a
                             inner join (select SN ,CARTONNO,pickno,pickline from TBLPICKDetailMaterialSN ) b
                                on a.PICKNO = b.PICKNO
                               and a.PICKLINE = b.pickline and a.CARTONNO=b.CARTONNO
                             where b.sn = '" + sn + "' and a.dqmcode='" + dqmCode + "'";

            int count = this.DataProvider.GetCount(new SQLCondition(sql));

            if (count > 0)
                return true;
            else
                return false;
        }




        public int GetEarliestInStorageDate(string invno, string transType, string dqmCode)
        {
            string sql = @"SELECT T.* from TBLINVINOUTTRANS T WHERE T.TransType='" + transType + @"' AND T.INVNO='" + invno + @"' AND T.DQMCODE='" + dqmCode + "' and STORAGEAGEDATE<>0 order by t.mdate ,t.mtime";
            object[] TRANS = this.DataProvider.CustomQuery(typeof(InvInOutTrans), new SQLCondition(sql));
            if (TRANS != null && TRANS.Length > 0)
            {

                return ((InvInOutTrans)TRANS[0]).StorageAgeDate;


            }
            return 0;
        }



        public Asndetail[] GetAsnDetailStatusEQReceive(string stno)
        {
            List<Asndetail> asndetails = new List<Asndetail>();
            string sql = @"SELECT T.* from TBLASNDETAIL T WHERE T.InitReceiveStatus In('" + SAP_LineStatus.SAP_LINE_RECEIVE + "','" + SAP_LineStatus.SAP_LINE_GIVEIN + "') AND t.STNO='" + stno + "'";
            object[] details = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
            if (details != null && details.Length > 0)
            {

                foreach (Asndetail d in details)
                {
                    asndetails.Add(d);

                }

            }
            return asndetails.ToArray();

        }



        public OQCNew[] GetOQCDQMCodes(string CARINVNO)
        {
            List<OQCNew> os = new List<OQCNew>();
            string sql = "select distinct b.DQMCODE from TBLCartonInvDetailMaterial b where  b.CARINVNO='" + CARINVNO + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(OQCNew), new SQLCondition(sql));
            if (o == null)
                return new OQCNew[0];
            else
            {
                foreach (OQCNew oqc in o)
                {
                    os.Add(oqc);
                }
            }
            return os.ToArray();
        }


        public OQCNew[] GetOQCDetailSNForN1(string CARINVNO)
        {
            List<OQCNew> os = new List<OQCNew>();
            string sql = "select  a.sn,b.MCODE,b.DQMCODE,b.cartonno from TBLCartonInvDetailSN a,TBLCartonInvDetailMaterial b where a.CARINVNO= b.CARINVNO and a.cartonno=b.cartonno and a.CARINVNO='" + CARINVNO + "' group by b.mcode,a.sn,b.dqmcode,b.cartonno";
            object[] o = this.DataProvider.CustomQuery(typeof(OQCNew), new SQLCondition(sql));
            if (o == null)
                return new OQCNew[0];
            else
            {
                foreach (OQCNew oqc in o)
                {
                    os.Add(oqc);
                }
            }
            return os.ToArray();
        }


        public OQCNew[] GetOQCDetailsForN(string CARINVNO, string dqmCode)
        {
            List<OQCNew> os = new List<OQCNew>();
            string sql = "select  b.GFHWITEMCODE,b.MCODE,b.DQMCODE,b.QTY,b.UNIT,b.GFPACKINGSEQ,b.CARTONNO from TBLCartonInvoices a,TBLCartonInvDetailMaterial b where a.CARINVNO= b.CARINVNO  and a.CARINVNO='" + CARINVNO + "' and b.dqmCode='" + dqmCode + "'";
            object[] o = this.DataProvider.CustomQuery(typeof(OQCNew), new SQLCondition(sql));
            if (o == null)
                return new OQCNew[0];
            else
            {
                foreach (OQCNew oqc in o)
                {
                    os.Add(oqc);
                }
            }
            return os.ToArray();
        }



        public InvoicesDetail[] GetInvoiceDetailsDescByPlanDate(string stno, string dqmCode)
        {
            List<InvoicesDetail> os = new List<InvoicesDetail>();
            string sql = "select b.* from TBLInvoicesDetail b,tblASN a where a.invno=b.invno and a.STNO= '" + stno + "' and b.dqmcode='" + dqmCode + "' ORDER BY PLANDATE desc";
            object[] o = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));

            if (o != null && o.Length > 0)
            {
                foreach (InvoicesDetail i in o)
                {
                    os.Add(i);
                }
            }
            return os.ToArray();
        }

        public InvDoc GetCartonnoDoc(string stno)
        {
            string sql = "SELECT * FROM TBLINVDOC WHERE INVDOCNO='" + stno + "' and INVDOCTYPE='CartonDataImp' ORDER BY MDATE ,MTIME DESC";

            object[] o = this.DataProvider.CustomQuery(typeof(InvDoc), new SQLCondition(sql));
            if (o != null && o.Length > 0)
            {
                return (InvDoc)o[0];
            }
            return null;
        }


        public Asndetailitem[] GetAsnItems(string stno, string invNo, string invLine)
        {
            List<Asndetailitem> os = new List<Asndetailitem>();
            string sql = "select a.* from TBLASNDETAILITEM A,TBLASNDETAIL B where A.STNO=B.STNO AND A.STLINE=B.STLINE AND a.STNO= '" + stno + "' and a.INVNO='" + invNo + "' and a.INVLINE='" + invLine + "' AND B.CARTONNO IS NULL";
            object[] o = this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql));

            if (o != null && o.Length > 0)
            {
                foreach (Asndetailitem i in o)
                {
                    os.Add(i);
                }
            }
            return os.ToArray();
        }


        public PickPak[] QueryPicksForLoadCases1(string carinvno)
        {
            string sql = "SELECT b.DQSITEMCODE,b.CUSITEMDESC,b.qty,b.unit,b.GFHWITEMCODE,A.CARTONNO FROM TBLCartonInvDetailMaterial A,TBLPICKDETAIL B ,TBLPICK C WHERE CARINVNO='" + carinvno + "' AND C.GFFLAG='X' AND A.PICKNO=B.PICKNO AND A.PICKNO=C.PICKNO";

            object[] oo = this.DataProvider.CustomQuery(typeof(PickPak), new SQLCondition(sql));

            List<PickPak> ps = new List<PickPak>();

            if (oo != null && oo.Length > 0)
            {
                foreach (PickPak d in oo)
                    ps.Add(d);
            }
            return ps.ToArray();
        }

        public InvoicesDetailEx1[] QueryInvoicesDetailsForDN(string pickNo, string dqmcode)
        {

            string sql = "SELECT T1.DQMCODE,T1.INVNO,T1.INVLINE,T1.PLANQTY,T1.DNBATCHNO,T2.PICKNO,T1.MENSHORTDESC FROM (SELECT A.DQMCODE,A.INVNO,A.INVLINE,A.PLANQTY,A.MENSHORTDESC,B.DNBATCHNO FROM TBLINVOICESDETAIL A,TBLINVOICES B WHERE A.INVNO=B.INVNO) T1,TBLPICK T2 WHERE T1.DNBATCHNO =T2.INVNO AND T2.PICKNO='" + pickNo + "' AND T1.DQMCODE='" + dqmcode + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailEx1), new SQLCondition(sql));

            List<InvoicesDetailEx1> ps = new List<InvoicesDetailEx1>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailEx1 d in oo)
                    ps.Add(d);
            }
            return ps.ToArray();
        }

        public InvoicesDetailEx1[] QueryInvoicesDetailsForNotDN(string pickNo, string dqmcode)
        {

            string sql = "SELECT T1.DQMCODE,T1.INVNO,T1.INVLINE,T1.PLANQTY,T2.PICKNO,T1.MENSHORTDESC FROM (SELECT A.DQMCODE,A.INVNO,A.INVLINE,A.PLANQTY,A.MENSHORTDESC FROM TBLINVOICESDETAIL A,TBLINVOICES B WHERE A.INVNO=B.INVNO) T1,TBLPICK T2 WHERE T1.INVNO =T2.INVNO AND T2.PICKNO='" + pickNo + "' AND T1.DQMCODE='" + dqmcode + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailEx1), new SQLCondition(sql));

            List<InvoicesDetailEx1> ps = new List<InvoicesDetailEx1>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailEx1 d in oo)
                    ps.Add(d);
            }
            return ps.ToArray();
        }


        public InvoicesDetail[] QueryInvoicesDetails(string pickNo)
        {

            string sql = "SELECT  * FROM TBLINVOICESDETAIL A,TBLPICK B WHERE A.INVNO=B.INVNO AND B.PICKNO='" + pickNo + "' ";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));

            List<InvoicesDetail> ps = new List<InvoicesDetail>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetail d in oo)
                    ps.Add(d);
            }
            return ps.ToArray();
        }

        public StorageInOperationRecord[] QueryASNs(string storageCode, string stno, string sapNo, int receiveDateBegin, int receiveDateEnd)
        {

            List<StorageInOperationRecord> records = new List<StorageInOperationRecord>();
            string sql = "SELECT  A.STORAGECODE,A.INVNO,A.STNO,B.ProcessType,b.mdate,B.MUSER FROM tblasn A,TBLINVINOUTTRANS B WHERE A.STNO=B.TRANSNO AND A.INVNO=B.INVNO AND TRANSTYPE='IN' ";
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND A.STORAGECODE='" + storageCode + "' ";

            }
            if (!string.IsNullOrEmpty(stno))
            {
                sql += " AND A.STNO ='" + stno + "' ";
            }
            if (!string.IsNullOrEmpty(sapNo))
            {
                sql += " AND A.INVNO='" + sapNo + "' ";
            }
            if (receiveDateBegin > 0)
            {
                sql += " AND B.MDATE>=" + receiveDateBegin;
            }
            if (receiveDateEnd > 0)
            {
                sql += " AND B.MDATE<=" + receiveDateEnd;
            }
            sql += "order by a.stno,B.ProcessType,b.mdate";

            object[] oo = this.DataProvider.CustomQuery(typeof(StorageInOperationRecord), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {
                string sTNo = string.Empty;
                string invno = string.Empty;
                StorageInOperationRecord record = null;
                foreach (StorageInOperationRecord d in oo)
                {
                    if (d.STNO != sTNo)
                    {


                        sTNo = d.STNO;
                        invno = d.INVNO;
                        record = new StorageInOperationRecord();
                        record.INVNO = d.INVNO;
                        record.STNO = d.STNO;
                        record.STORAGECODE = d.STORAGECODE;
                        records.Add(record);
                    }
                    if (d.ProcessType == "INSTORAEE")
                    {
                        record.InStorageDate = d.MDATE;
                        if (!record.InStorageOpers.Contains(d.MUSER))
                            record.InStorageOpers.Add(d.MUSER);
                    }
                    else if (d.ProcessType == "IQC")
                    {
                        record.IQCDate = d.MDATE;
                        if (!record.IQCOpers.Contains(d.MUSER))
                            record.IQCOpers.Add(d.MUSER);
                    }
                    else if (d.ProcessType == "Receive")
                    {
                        record.ReceiveDate = d.MDATE;
                        if (!record.ReceiveOpers.Contains(d.MUSER))
                            record.ReceiveOpers.Add(d.MUSER);
                    }
                    else if (d.ProcessType == "IQCSQE")
                    {
                        record.IQCSQEDate = d.MDATE;
                        if (!record.IQCSQEOpers.Contains(d.MUSER))
                            record.IQCSQEOpers.Add(d.MUSER);
                    }


                }
            }

            return records.ToArray();
        }



        public IQCDetailRecord[] QueryIQCs(string storageCode, string vendorCode, string storageType, int dateBegin, int dateEnd, int inclusive, int exclusive)
        {
            List<IQCDetailRecord> records = new List<IQCDetailRecord>();
            string sql = @"SELECT TBIG.CDATE,TBIG.CTIME,TBIG.STNO,TBIG.IQCNO,TBIG.INVNO,TBIG.STTYPE,TBIG.StorageCode,TBIG.VENDORCODE,TBIG.VendorName,TBIG.DQMCODE,TBIG.VENDORMCODE,TBIG.MDESC,TBIG.IQCTYPE,TBIG.AQLLEVEL,TBIG.QTY,TBIG.IQCQTY,TBIG.NGQTY,TBIG.RETURNQTY,TBIG.ReformQTY,TBIG.GiveQTY,TBIG.AcceptQTY,TBIG.SAMPLESIZE FROM
(
       SELECT T1.CDATE,T1.CTIME,T1.STNO,T1.IQCNO,T1.INVNO,T1.STTYPE,T1.StorageCode,T1.VENDORCODE,T1.VendorName,T1.DQMCODE,T1.VENDORMCODE,T1.MDESC,T1.IQCTYPE,T1.AQLLEVEL,T2.QTY,T2.IQCQTY,T2.NGQTY,T2.RETURNQTY,T2.ReformQTY,T2.GiveQTY,T2.AcceptQTY,T1.SAMPLESIZE FROM
       (SELECT A.IQCNO,A.CDATE,A.CTIME,B.STNO,B.INVNO ,B.STTYPE,B.StorageCode,d.VENDORCODE,C.VendorName,A.DQMCODE,A.VENDORMCODE,A.MDESC,A.IQCTYPE,A.AQLLEVEL,E.SAMPLESIZE FROM TBLASNIQC A INNER JOIN TBLASN B ON A.STNO=B.STNO inner join tblinvoices d on b.invno=d.invno LEFT JOIN TBLVendor C ON d.VENDORCODE=C.VendorCode left join TBLAQL E ON E.AQLLEVEL||','||E.AQLSEQ=A.AQLLEVEL WHERE 1=1) T1,
       (SELECT A.IQCNO,SUM(B.QTY) QTY, SUM(A.QTY) IQCQTY,SUM(A.NGQTY) NGQTY,SUM(A.ReturnQTY) RETURNQTY,SUM(A.ReformQTY) ReformQTY,SUM(A.GiveQTY) GiveQTY,SUM(A.AcceptQTY) AcceptQTY FROM TBLASNIQCDETAIL A,TBLASNDETAILITEM B WHERE A.STNO=B.STNO AND A.STLINE=B.STLINE GROUP BY IQCNO) T2 WHERE T1.IQCNO=T2.IQCNO 
        
";

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND T1.STORAGECODE='" + storageCode + "' ";

            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND T1.VENDORCODE='" + vendorCode + "' ";
            }
            if (!string.IsNullOrEmpty(storageType))
            {

                sql += " AND T1.STTYPE='" + storageType + "' ";
            }
            if (dateBegin > 0)
            {
                sql += " AND T1.CDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND T1.CDATE<=" + dateEnd;
            }
            sql += @" 
       
        ) TBIG ";

            object[] oo = this.DataProvider.CustomQuery(typeof(IQCDetailRecord), new PagerCondition(sql, "TBIG.IQCNO desc", inclusive, exclusive));

            if (oo != null && oo.Length > 0)
            {
                foreach (IQCDetailRecord o in oo)
                    records.Add(o);

            }
            return records.ToArray();
        }


        public int GetErrorGroupNum(string iqcNo, string ErrorCodeGroup)
        {
            string sql = "SELECT nvl(SUM(NGQTY),0) NGQTY FROM tblasniqcdetailec WHERE ECGCODE='" + ErrorCodeGroup + "' AND IQCNO='" + iqcNo + "'";


            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public InvInOutTrans QCFinishDateTimeTrans(string iqcNo)
        {
            string sql = @" SELECT B.mdate,B.mtime FROM TBLASNIQC A, TBLINVINOUTTRANS B WHERE  A.IQCNO=B.TRANSNO AND (A.STATUS='IQCClose' or A.STATUS='IQCRejection') AND  B.ProcessType='IQC' AND A.IQCNO='" + iqcNo + @"' order by B.mdate desc,B.mtime desc";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvInOutTrans), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
                return (InvInOutTrans)oo[0];
            else
                return null;

        }

        public InvInOutTrans SQEFinishDateTimeTrans(string iqcNo)
        {
            string sql = @" SELECT B.mdate, B.mtime FROM TBLASNIQC A, TBLINVINOUTTRANS B WHERE  A.IQCNO=B.TRANSNO AND (A.STATUS='IQCClose' or A.STATUS='IQCRejection') AND  B.ProcessType='IQCSQE' AND A.IQCNO='" + iqcNo + @"' order by  B.mdate desc, B.mtime desc";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvInOutTrans), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
                return (InvInOutTrans)oo[0];
            else
                return null;

        }


        public IQCDetailRecord[] QueryIQCs(string storageCode, string vendorCode, string storageType, int dateBegin, int dateEnd)
        {
            List<IQCDetailRecord> records = new List<IQCDetailRecord>();
            string sql = @"SELECT TBIG.CDATE,TBIG.CTIME,TBIG.STNO,TBIG.IQCNO,TBIG.INVNO,TBIG.STTYPE,TBIG.StorageCode,TBIG.VENDORCODE,TBIG.VendorName,TBIG.DQMCODE,TBIG.VENDORMCODE,TBIG.MDESC,TBIG.IQCTYPE,TBIG.AQLLEVEL,TBIG.QTY,TBIG.IQCQTY,TBIG.NGQTY,TBIG.RETURNQTY,TBIG.ReformQTY,TBIG.GiveQTY,TBIG.AcceptQTY,TBIG.SAMPLESIZE FROM
(
       SELECT T1.CDATE,T1.CTIME,T1.STNO,T1.IQCNO,T1.INVNO,T1.STTYPE,T1.StorageCode,T1.VENDORCODE,T1.VendorName,T1.DQMCODE,T1.VENDORMCODE,T1.MDESC,T1.IQCTYPE,T1.AQLLEVEL,T2.QTY,T2.IQCQTY,T2.NGQTY,T2.RETURNQTY,T2.ReformQTY,T2.GiveQTY,T2.AcceptQTY,T1.SAMPLESIZE FROM
       (SELECT A.IQCNO,A.CDATE,A.CTIME,B.STNO,B.INVNO ,B.STTYPE,B.StorageCode,d.VENDORCODE,C.VendorName,A.DQMCODE,A.VENDORMCODE,A.MDESC,A.IQCTYPE,A.AQLLEVEL,E.SAMPLESIZE FROM TBLASNIQC A INNER JOIN TBLASN B ON A.STNO=B.STNO inner join tblinvoices d on b.invno=d.invno LEFT JOIN TBLVendor C ON d.VENDORCODE=C.VendorCode left join TBLAQL E ON E.AQLLEVEL||','||E.AQLSEQ=A.AQLLEVEL WHERE 1=1) T1,
       (SELECT A.IQCNO,SUM(B.QTY) QTY, SUM(A.QTY) IQCQTY,SUM(A.NGQTY) NGQTY,SUM(A.ReturnQTY) RETURNQTY,SUM(A.ReformQTY) ReformQTY,SUM(A.GiveQTY) GiveQTY,SUM(A.AcceptQTY) AcceptQTY FROM TBLASNIQCDETAIL A,TBLASNDETAILITEM B WHERE A.STNO=B.STNO AND A.STLINE=B.STLINE GROUP BY IQCNO) T2 WHERE T1.IQCNO=T2.IQCNO 
         ";

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND T1.STORAGECODE='" + storageCode + "' ";

            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND T1.VENDORCODE='" + vendorCode + "' ";
            }
            if (!string.IsNullOrEmpty(storageType))
            {

                sql += " AND T1.STTYPE='" + storageType + "' ";
            }
            if (dateBegin > 0)
            {
                sql += " AND T1.CDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND T1.CDATE<=" + dateEnd;
            }
            sql += @" ) TBIG ";

            object[] oo = this.DataProvider.CustomQuery(typeof(IQCDetailRecord), new SQLCondition(sql));




            if (oo != null && oo.Length > 0)
            {
                foreach (IQCDetailRecord o in oo)
                    records.Add(o);

            }
            return records.ToArray();
        }

        public int QueryIQCsCount(string storageCode, string vendorCode, string storageType, int dateBegin, int dateEnd)
        {

            string sql = @"SELECT COUNT(*) FROM
(
       SELECT T1.CDATE,T1.CTIME,T1.STNO,T1.IQCNO,T1.INVNO,T1.STTYPE,T1.StorageCode,T1.VENDORCODE,T1.VendorName,T1.DQMCODE,T1.VENDORMCODE,T1.MDESC,T1.IQCTYPE,T1.AQLLEVEL,T2.QTY,T2.IQCQTY,T2.NGQTY,T2.RETURNQTY,T2.ReformQTY,T2.GiveQTY,T2.AcceptQTY,T1.SAMPLESIZE FROM
       (SELECT A.IQCNO,A.CDATE,A.CTIME,B.STNO,B.INVNO ,B.STTYPE,B.StorageCode,d.VENDORCODE,C.VendorName,A.DQMCODE,A.VENDORMCODE,A.MDESC,A.IQCTYPE,A.AQLLEVEL,E.SAMPLESIZE FROM TBLASNIQC A INNER JOIN TBLASN B ON A.STNO=B.STNO inner join tblinvoices d on b.invno=d.invno LEFT JOIN TBLVendor C ON d.VENDORCODE=C.VendorCode left join TBLAQL E ON E.AQLLEVEL||','||E.AQLSEQ=A.AQLLEVEL WHERE 1=1) T1,
       (SELECT A.IQCNO,SUM(B.QTY) QTY, SUM(A.QTY) IQCQTY,SUM(A.NGQTY) NGQTY,SUM(A.ReturnQTY) RETURNQTY,SUM(A.ReformQTY) ReformQTY,SUM(A.GiveQTY) GiveQTY,SUM(A.AcceptQTY) AcceptQTY FROM TBLASNIQCDETAIL A,TBLASNDETAILITEM B WHERE A.STNO=B.STNO AND A.STLINE=B.STLINE GROUP BY IQCNO) T2 WHERE T1.IQCNO=T2.IQCNO 
        ";

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND T1.STORAGECODE='" + storageCode + "' ";

            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND T1.VENDORCODE='" + vendorCode + "' ";
            }
            if (!string.IsNullOrEmpty(storageType))
            {

                sql += " AND T1.STTYPE='" + storageType + "' ";
            }
            if (dateBegin > 0)
            {
                sql += " AND T1.CDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND T1.CDATE<=" + dateEnd;
            }
            sql += @" 
       
        ) TBIG ";

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public InStorageDetailRecord[] QueryInStorageDetails(string stno, string invno, string storageCode, string vendorCode, string storageType, int dateBegin, int dateEnd)
        {
            List<InStorageDetailRecord> records = new List<InStorageDetailRecord>();


            string sql = @"select T4.cdate,T1_3.STNO,T1_3.INVNO,T1_3.STTYPE,T1_3.STORAGECODE,T1_3.VENDORCODE,T1_3.VENDORNAME,T1_3.VOLUME,T1_3.GROSS_WEIGHT,T1_3.ASNCDATE,T1_3.ASNCTIME,T1_3.CARTONNOS,T2.receivebeginDate,T6.IQCDate,T7.INSTORAEEDate,t8.IQCSQEdate,t9.receiveclosedate,t10.issuedate,t11.INSTORAEEBeginDate from 
(   
                   select t1.STNO,t1.INVNO,t1.STTYPE,t1.STORAGECODE,t1.VENDORCODE,t1.VendorName,t1.VOLUME,t1.GROSS_WEIGHT,t1.ASNCDATE,t1.ASNCTIME,t3.CARTONNOS from
                    (SELECT A.STNO,A.INVNO,A.STTYPE,A.STORAGECODE,B.VENDORCODE,B.VendorName, A.VOLUME,A.GROSS_WEIGHT ,A.CDATE ASNCDATE,A.CTIME ASNCTIME FROM TBLASN A LEFT JOIN TBLVendor B ON A.VENDORCODE=B.VendorCode) T1,
                    (SELECT COUNT(*) CARTONNOS,STNO  FROM TBLASNDETAIL GROUP BY STNO) T3 where t1.stno=t3.stno) T1_3 LEFT JOIN 
                    (SELECT  TRANSNO,MAX(mdate||' '||mtime) receivebeginDate FROM TBLINVINOUTTRANS  WHERE  TRANSTYPE='IN' AND ProcessType='RECEIVEBEGIN' GROUP BY TRANSNO)T2 on T1_3.stno=t2.TRANSNO LEFT JOIN
                    (SELECT  b.stno,max(a.mdate||' '||a.mtime) IQCDate FROM TBLINVINOUTTRANS a,tblasniqc b WHERE (a.transno=b.iqcno or a.transno=b.stno) and TRANSTYPE='IN' AND ProcessType='IQC' GROUP BY b.stno) T6 ON T6.stno=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,max(mdate||' '||mtime) INSTORAEEDate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='INSTORAEE' GROUP BY TRANSNO) T7 ON T7.TRANSNO=T1_3.STNO LEFT JOIN
                    (SELECT  b.stno,max(a.mdate||' '||a.mtime) IQCSQEdate FROM TBLINVINOUTTRANS a,tblasniqc b WHERE (a.transno=b.iqcno or a.transno=b.stno) and TRANSTYPE='IN' AND ProcessType='IQCSQE' GROUP BY b.stno) T8 ON T8.stno=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,max(mdate||' '||mtime) receiveclosedate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='Receive' GROUP BY TRANSNO) T9 ON T9.TRANSNO=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,min(mdate||' '||mtime) INSTORAEEBeginDate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='INSTORAEE' GROUP BY TRANSNO) T11 ON T11.TRANSNO=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,max(mdate||' '||mtime) issuedate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='ISSUE' GROUP BY TRANSNO) T10 ON T10.TRANSNO=T1_3.STNO LEFT JOIN 
                    (SELECT STNO,max(CDATE||' '||CTIME) CDATE FROM TBLASNIQC GROUP BY STNO ) T4 ON T1_3.STNO=T4.STNO  where 1=1 ";

            if (!string.IsNullOrEmpty(invno))
            {
                sql += " AND T1_3.INVNO='" + invno + "' ";
            }

            if (!string.IsNullOrEmpty(stno))
            {
                sql += " AND T1_3.STNO='" + stno + "' ";
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND STORAGECODE='" + storageCode + "' ";

            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND VENDORCODE='" + vendorCode + "' ";
            }
            if (!string.IsNullOrEmpty(storageType))
            {

                sql += " AND STTYPE='" + storageType + "' ";
            }
            if (dateBegin > 0)
            {
                sql += " AND ASNCDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND ASNCDATE<=" + dateEnd;
            }

            object[] oo = this.DataProvider.CustomQuery(typeof(InStorageDetailRecord), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {
                foreach (InStorageDetailRecord d in oo)
                    records.Add(d);

            }

            return records.ToArray();
        }
        public object[] QueryInStorageDetails(string stno, string invno, string storageCode, string vendorCode, string storageType, int dateBegin, int dateEnd, int inclusive, int exclusive)
        {
            string sql = @"select T4.cdate,T1_3.STNO,T1_3.INVNO,T1_3.STTYPE,T1_3.STORAGECODE,T1_3.VENDORCODE,T1_3.VENDORNAME,T1_3.VOLUME,T1_3.GROSS_WEIGHT,T1_3.ASNCDATE,T1_3.ASNCTIME,T1_3.CARTONNOS,T2.receivebeginDate,T6.IQCDate,T7.INSTORAEEDate,t8.IQCSQEdate,t9.receiveclosedate,t10.issuedate,t11.INSTORAEEBeginDate from 
(   
                   select t1.STNO,t1.INVNO,t1.STTYPE,t1.STORAGECODE,t1.VENDORCODE,t1.VendorName,t1.VOLUME,t1.GROSS_WEIGHT,t1.ASNCDATE,t1.ASNCTIME,t3.CARTONNOS from
                    (SELECT A.STNO,A.INVNO,A.STTYPE,A.STORAGECODE,B.VENDORCODE,B.VendorName, A.VOLUME,A.GROSS_WEIGHT ,A.CDATE ASNCDATE,A.CTIME ASNCTIME FROM TBLASN A LEFT JOIN TBLVendor B ON A.VENDORCODE=B.VendorCode) T1,
                    (SELECT COUNT(*) CARTONNOS,STNO  FROM TBLASNDETAIL GROUP BY STNO) T3 where t1.stno=t3.stno) T1_3 LEFT JOIN 
                    (SELECT  TRANSNO,MAX(mdate||' '||mtime) receivebeginDate FROM TBLINVINOUTTRANS  WHERE  TRANSTYPE='IN' AND ProcessType='RECEIVEBEGIN' GROUP BY TRANSNO)T2 on T1_3.stno=t2.TRANSNO LEFT JOIN
                    (SELECT  b.stno,max(a.mdate||' '||a.mtime) IQCDate FROM TBLINVINOUTTRANS a,tblasniqc b WHERE (a.transno=b.iqcno or a.transno=b.stno) and TRANSTYPE='IN' AND ProcessType='IQC' GROUP BY b.stno) T6 ON T6.stno=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,max(mdate||' '||mtime) INSTORAEEDate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='INSTORAEE' GROUP BY TRANSNO) T7 ON T7.TRANSNO=T1_3.STNO LEFT JOIN
                    (SELECT  b.stno,max(a.mdate||' '||a.mtime) IQCSQEdate FROM TBLINVINOUTTRANS a,tblasniqc b WHERE (a.transno=b.iqcno or a.transno=b.stno) and TRANSTYPE='IN' AND ProcessType='IQCSQE' GROUP BY b.stno) T8 ON T8.stno=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,max(mdate||' '||mtime) receiveclosedate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='Receive' GROUP BY TRANSNO) T9 ON T9.TRANSNO=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,min(mdate||' '||mtime) INSTORAEEBeginDate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='INSTORAEE' GROUP BY TRANSNO) T11 ON T11.TRANSNO=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,max(mdate||' '||mtime) issuedate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='ISSUE' GROUP BY TRANSNO) T10 ON T10.TRANSNO=T1_3.STNO LEFT JOIN 
                    (SELECT STNO,max(CDATE||' '||CTIME) CDATE FROM TBLASNIQC GROUP BY STNO ) T4 ON T1_3.STNO=T4.STNO  where 1=1 ";


            if (!string.IsNullOrEmpty(invno))
            {
                sql += " AND T1_3.INVNO='" + invno + "' ";
            }

            if (!string.IsNullOrEmpty(stno))
            {
                sql += " AND T1_3.STNO='" + stno + "' ";
            }

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND STORAGECODE='" + storageCode + "' ";

            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND VENDORCODE='" + vendorCode + "' ";
            }
            if (!string.IsNullOrEmpty(storageType))
            {

                sql += " AND STTYPE='" + storageType + "' ";
            }
            if (dateBegin > 0)
            {
                sql += " AND ASNCDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND ASNCDATE<=" + dateEnd;
            }
            object[] oo = this.DataProvider.CustomQuery(typeof(InStorageDetailRecord), new PagerCondition(sql, "T1_3.STNO DESC", inclusive, exclusive));
            return oo;
        }



        public int QueryInStorageDetailsCount(string stno, string invno, string storageCode, string vendorCode, string storageType, int dateBegin, int dateEnd)
        {
            List<InStorageDetailRecord> records = new List<InStorageDetailRecord>();


            string sql = @"select count(*) from 
(   
                   select t1.STNO,t1.INVNO,t1.STTYPE,t1.STORAGECODE,t1.VENDORCODE,t1.VendorName,t1.VOLUME,t1.GROSS_WEIGHT,t1.ASNCDATE,t1.ASNCTIME,t3.CARTONNOS from
                    (SELECT A.STNO,A.INVNO,A.STTYPE,A.STORAGECODE,B.VENDORCODE,B.VendorName, A.VOLUME,A.GROSS_WEIGHT ,A.CDATE ASNCDATE,A.CTIME ASNCTIME FROM TBLASN A LEFT JOIN TBLVendor B ON A.VENDORCODE=B.VendorCode) T1,
                    (SELECT COUNT(*) CARTONNOS,STNO  FROM TBLASNDETAIL GROUP BY STNO) T3 where t1.stno=t3.stno) T1_3 LEFT JOIN 
                    (SELECT  TRANSNO,MAX(mdate||' '||mtime) receivebeginDate FROM TBLINVINOUTTRANS  WHERE  TRANSTYPE='IN' AND ProcessType='RECEIVEBEGIN' GROUP BY TRANSNO)T2 on T1_3.stno=t2.TRANSNO LEFT JOIN
                    (SELECT  b.stno,max(a.mdate||' '||a.mtime) IQCDate FROM TBLINVINOUTTRANS a,tblasniqc b WHERE (a.transno=b.iqcno or a.transno=b.stno) and TRANSTYPE='IN' AND ProcessType='IQC' GROUP BY b.stno) T6 ON T6.stno=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,max(mdate||' '||mtime) INSTORAEEDate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='INSTORAEE' GROUP BY TRANSNO) T7 ON T7.TRANSNO=T1_3.STNO LEFT JOIN
                    (SELECT  b.stno,max(a.mdate||' '||a.mtime) IQCSQEdate FROM TBLINVINOUTTRANS a,tblasniqc b WHERE (a.transno=b.iqcno or a.transno=b.stno) and TRANSTYPE='IN' AND ProcessType='IQCSQE' GROUP BY b.stno) T8 ON T8.stno=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,max(mdate||' '||mtime) receiveclosedate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='Receive' GROUP BY TRANSNO) T9 ON T9.TRANSNO=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,min(mdate||' '||mtime) INSTORAEEBeginDate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='INSTORAEE' GROUP BY TRANSNO) T11 ON T11.TRANSNO=T1_3.STNO LEFT JOIN
                    (SELECT  TRANSNO,max(mdate||' '||mtime) issuedate FROM TBLINVINOUTTRANS WHERE  TRANSTYPE='IN' AND ProcessType='ISSUE' GROUP BY TRANSNO) T10 ON T10.TRANSNO=T1_3.STNO LEFT JOIN 
                    (SELECT STNO,max(CDATE||' '||CTIME) CDATE FROM TBLASNIQC GROUP BY STNO ) T4 ON T1_3.STNO=T4.STNO  where 1=1   ";
            if (!string.IsNullOrEmpty(invno))
            {
                sql += " AND T1_3.INVNO='" + invno + "' ";
            }

            if (!string.IsNullOrEmpty(stno))
            {
                sql += " AND T1_3.STNO='" + stno + "' ";
            }


            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND STORAGECODE='" + storageCode + "' ";

            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND VENDORCODE='" + vendorCode + "' ";
            }
            if (!string.IsNullOrEmpty(storageType))
            {

                sql += " AND STTYPE='" + storageType + "' ";
            }
            if (dateBegin > 0)
            {
                sql += " AND ASNCDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND ASNCDATE<=" + dateEnd;
            }



            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public AsnIssueCancelRecord[] QueryAsnIssueCancels(int dateBegin, int dateEnd, int inclusive, int exclusive)
        {
            List<AsnIssueCancelRecord> records = new List<AsnIssueCancelRecord>();


            string sql = @"SELECT A.STNO,A.INVNO,A.STTYPE,A.STORAGECODE,A.VENDORCODE,C.VendorName,A.CDATE,A.CTIME,A.CUSER,D.CANCELCOUNT,D.cancelISSUEDATE,E.ISSUEDATE FROM TBLASN A  left join TBLVendor C on A.VendorCode=C.VendorCode 
LEFT JOIN (SELECT TransNO,COUNT(*) CANCELCOUNT,max(MDATE||' '||MTIME) cancelISSUEDATE FROM TBLINVINOUTTRANS WHERE PROCESSTYPE='CANCELISSUE' GROUP BY TransNO) D ON D.TransNO=A.STNO LEFT JOIN
(SELECT TransNO, MAX(MDATE||' '||MTIME) ISSUEDATE FROM TBLINVINOUTTRANS WHERE PROCESSTYPE='ISSUE' GROUP BY TransNO) E ON E.TransNO=A.STNO  where D.CANCELCOUNT>0";

            if (dateBegin > 0)
            {
                sql += " AND A.CDATE >= " + dateBegin;

            }
            if (dateEnd > 0)
            {
                sql += " AND A.CDATE <= " + dateEnd;
            }

            object[] oo = this.DataProvider.CustomQuery(typeof(AsnIssueCancelRecord), new PagerCondition(sql, "a.STNO", inclusive, exclusive));
            if (oo != null && oo.Length > 0)
            {

                foreach (AsnIssueCancelRecord d in oo)
                {
                    records.Add(d);
                }
            }
            return records.ToArray();
        }

        public AsnIssueCancelRecord[] QueryAsnIssueCancels(int dateBegin, int dateEnd)
        {
            List<AsnIssueCancelRecord> records = new List<AsnIssueCancelRecord>();


            string sql = @"SELECT A.STNO,A.INVNO,A.STTYPE,A.STORAGECODE,A.VENDORCODE,C.VendorName,A.CDATE,A.CTIME,A.CUSER,D.CANCELCOUNT,D.cancelISSUEDATE,E.ISSUEDATE FROM TBLASN A  left join TBLVendor C on A.VendorCode=C.VendorCode 
LEFT JOIN (SELECT TransNO,COUNT(*) CANCELCOUNT,max(MDATE||' '||MTIME) cancelISSUEDATE FROM TBLINVINOUTTRANS WHERE PROCESSTYPE='CANCELISSUE' GROUP BY TransNO) D ON D.TransNO=A.STNO LEFT JOIN
(SELECT TransNO, MAX(MDATE||' '||MTIME) ISSUEDATE FROM TBLINVINOUTTRANS WHERE PROCESSTYPE='ISSUE' GROUP BY TransNO) E ON E.TransNO=A.STNO  where D.CANCELCOUNT>0";

            if (dateBegin > 0)
            {
                sql += " AND A.CDATE >= " + dateBegin;

            }
            if (dateEnd > 0)
            {
                sql += " AND A.CDATE <= " + dateEnd;
            }
            sql += "order by A.STNO";
            object[] oo = this.DataProvider.CustomQuery(typeof(AsnIssueCancelRecord), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {

                foreach (AsnIssueCancelRecord d in oo)
                {
                    records.Add(d);
                }
            }
            return records.ToArray();
        }


        public int QueryAsnIssueCancelsCount(int dateBegin, int dateEnd)
        {

            string sql = @"SELECT count(*) FROM TBLASN A  left join TBLVendor C on A.VendorCode=C.VendorCode 
LEFT JOIN (SELECT TransNO,COUNT(*) CANCELCOUNT FROM TBLINVINOUTTRANS WHERE PROCESSTYPE='CANCELISSUE' GROUP BY TransNO) D ON D.TransNO=A.STNO LEFT JOIN
(SELECT TransNO, MAX(MDATE||' '||MTIME) ISSUEDATE FROM TBLINVINOUTTRANS WHERE PROCESSTYPE='ISSUE' GROUP BY TransNO) E ON E.TransNO=A.STNO where D.CANCELCOUNT>0";

            if (dateBegin > 0)
            {
                sql += " AND A.CDATE >= " + dateBegin;

            }
            if (dateEnd > 0)
            {
                sql += " AND A.CDATE <= " + dateEnd;
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public PickAlterRecord[] QueryPickAlters(int dateBegin, int dateEnd, int inclusive, int exclusive)
        {
            List<PickAlterRecord> records = new List<PickAlterRecord>();


            string sql = @"SELECT A.PICKNO,A.INVNO,A.PICKTYPE,A.STORAGECODE,A.CUSER,A.Down_Date,A.Down_Time,B.CANCELCOUNT,B.CANCELDATE FROM TBLPICK A LEFT JOIN 
(SELECT TRANSNO,COUNT(*) CANCELCOUNT,MAX(MDATE||' '||MTIME) CANCELDATE FROM TBLINVINOUTTRANS WHERE TransType='OUT' AND PROCESSTYPE='CANCELISSUE' GROUP BY TRANSNO) B ON A.PICKNO=B.TRANSNO
WHERE B.CANCELCOUNT>0 ";

            if (dateBegin > 0)
            {
                sql += " AND a.Down_Date >= " + dateBegin;

            }
            if (dateEnd > 0)
            {
                sql += " AND a.Down_Date <= " + dateEnd;
            }

            object[] oo = this.DataProvider.CustomQuery(typeof(PickAlterRecord), new PagerCondition(sql, "A.PICKNO", inclusive, exclusive));
            if (oo != null && oo.Length > 0)
            {


                foreach (PickAlterRecord d in oo)
                {
                    records.Add(d);
                }
            }



            return records.ToArray();

        }

        public PickAlterRecord[] QueryPickAlters(int dateBegin, int dateEnd)
        {
            List<PickAlterRecord> records = new List<PickAlterRecord>();


            string sql = @"SELECT A.PICKNO,A.INVNO,A.PICKTYPE,A.STORAGECODE,A.CUSER,A.Down_Date,A.Down_Time,B.CANCELCOUNT,B.CANCELDATE FROM TBLPICK A LEFT JOIN 
(SELECT TRANSNO,COUNT(*) CANCELCOUNT,MAX(MDATE||' '||MTIME) CANCELDATE FROM TBLINVINOUTTRANS WHERE TransType='OUT' AND PROCESSTYPE='CANCELISSUE' GROUP BY TRANSNO) B ON A.PICKNO=B.TRANSNO
WHERE B.CANCELCOUNT>0 ";

            if (dateBegin > 0)
            {
                sql += " AND a.Down_Date >= " + dateBegin;

            }
            if (dateEnd > 0)
            {
                sql += " AND a.Down_Date <= " + dateEnd;
            }

            sql += " order by a.PICKNO";

            object[] oo = this.DataProvider.CustomQuery(typeof(PickAlterRecord), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {
                foreach (PickAlterRecord d in oo)
                {

                    records.Add(d);
                }
            }



            return records.ToArray();

        }

        public int QueryPickAltersCount(int dateBegin, int dateEnd)
        {
            List<PickAlterRecord> records = new List<PickAlterRecord>();


            string sql = @"SELECT count(*) FROM TBLPICK A LEFT JOIN 
(SELECT TRANSNO,COUNT(*) CANCELCOUNT,MAX(MDATE||' '||MTIME) CANCELDATE FROM TBLINVINOUTTRANS WHERE TransType='OUT' AND PROCESSTYPE='CANCELISSUE' GROUP BY TRANSNO) B ON A.PICKNO=B.TRANSNO
WHERE B.CANCELCOUNT>0 ";

            if (dateBegin > 0)
            {
                sql += " AND a.Down_Date >= " + dateBegin;

            }
            if (dateEnd > 0)
            {
                sql += " AND a.Down_Date <= " + dateEnd;
            }

            sql += " order by a.PICKNO";

            return this.DataProvider.GetCount(new SQLCondition(sql));


        }

        public int QueryProductInstoragesCount(string invno, int dateBegin, int dateEnd, string vendor, string dqmCode, string dqmDesc, string sn, string supplierLotNo)
        {
            List<ProductInstorageRecord> records = new List<ProductInstorageRecord>();


            //string sql = @"SELECT count(*) FROM TBLStorageDetail A left JOIN TBLASNDETAILSN B ON A.CARTONNO=B.CARTONNO  LEFT JOIN  TBLASN C ON a.STNO=C.STNO LEFT JOIN TBLVendor D ON C.VendorCode=D.VendorCode where 1=1 ";

            //if (!string.IsNullOrEmpty(invno))
            //{
            //    sql += " AND C.INVNO='" + invno + "' ";

            //}


            //if (!string.IsNullOrEmpty(vendor))
            //{
            //    sql += " AND C.VendorCode LIKE'%" + vendor + "%' ";

            //}
            //if (!string.IsNullOrEmpty(dqmCode))
            //{
            //    sql += " AND A.DQMCODE LIKE'%" + dqmCode + "%' ";

            //}
            //if (!string.IsNullOrEmpty(dqmDesc))
            //{
            //    sql += " AND A.MDESC LIKE'%" + dqmDesc + "%' ";

            //}
            //if (!string.IsNullOrEmpty(sn))
            //{
            //    sql += " AND B.SN LIKE'%" + sn + "%' ";

            //}
            //if (!string.IsNullOrEmpty(supplierLotNo))
            //{
            //    sql += " AND A.Supplier_LotNo LIKE'%" + supplierLotNo + "%' ";

            //}
            //if (dateBegin > 0)
            //{
            //    sql += " AND A.LastStorageAgeDate >= " + dateBegin;
            //}
            //if (dateEnd > 0)
            //{
            //    sql += " AND A.LastStorageAgeDate <= " + dateEnd;
            //}



            string sql = @"SELECT count(*)  
from 
tblasn A LEFT JOIN
TBLASNDETAIL B ON A.STNO = B.STNO LEFT JOIN
tblasndetailsn D on B.CARTONNO = D.CARTONNO and B.STNO = D.STNO LEFT JOIN
TBLVENDOR C ON A.VENDORCODE = C.VENDORCODE WHERE A.STATUS = 'Close'";




            if (!string.IsNullOrEmpty(invno))
            {
                sql += " AND a.INVNO='" + invno + "' ";

            }


            if (!string.IsNullOrEmpty(vendor))
            {
                sql += " AND A.VendorCode LIKE'%" + vendor + "%' ";

            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND b.DQMCODE LIKE'%" + dqmCode + "%' ";

            }
            if (!string.IsNullOrEmpty(dqmDesc))
            {
                sql += " AND b.MDESC LIKE'%" + dqmDesc + "%' ";

            }
            if (!string.IsNullOrEmpty(sn))
            {
                sql += " AND d.SN LIKE'%" + sn + "%' ";

            }
            if (!string.IsNullOrEmpty(supplierLotNo))
            {
                sql += " AND b.Supplier_LotNo LIKE'%" + supplierLotNo + "%' ";

            }
            if (dateBegin > 0)
            {
                sql += " ANd a.mdate >= " + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND a.mdate <= " + dateEnd;
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));


        }

        public ProductInstorageRecord[] QueryProductInstorages(string invno, int dateBegin, int dateEnd, string vendor, string dqmCode, string dqmDesc, string sn, string supplierLotNo, int inclusive, int exclusive)
        {
            List<ProductInstorageRecord> records = new List<ProductInstorageRecord>();


            //string sql = @"SELECT A.Supplier_LotNo,B.SN,A.STORAGEQTY,A.MDESC,A.DQMCODE,D.VendorName ,C.VendorCode ,A.LastStorageAgeDate,A.StorageAgeDate,C.STTYPE,C.INVNO,C.STNO FROM TBLStorageDetail A left JOIN TBLASNDETAILSN B ON A.CARTONNO=B.CARTONNO  LEFT JOIN  TBLASN C ON a.STNO=C.STNO LEFT JOIN TBLVendor D ON C.VendorCode=D.VendorCode where 1=1 ";

            //if (!string.IsNullOrEmpty(invno))
            //{
            //    sql += " AND C.INVNO='" + invno + "' ";

            //}


            //if (!string.IsNullOrEmpty(vendor))
            //{
            //    sql += " AND C.VendorCode LIKE'%" + vendor + "%' ";

            //}
            //if (!string.IsNullOrEmpty(dqmCode))
            //{
            //    sql += " AND A.DQMCODE LIKE'%" + dqmCode + "%' ";

            //}
            //if (!string.IsNullOrEmpty(dqmDesc))
            //{
            //    sql += " AND A.MDESC LIKE'%" + dqmDesc + "%' ";

            //}
            //if (!string.IsNullOrEmpty(sn))
            //{
            //    sql += " AND B.SN LIKE'%" + sn + "%' ";

            //}
            //if (!string.IsNullOrEmpty(supplierLotNo))
            //{
            //    sql += " AND A.Supplier_LotNo LIKE'%" + supplierLotNo + "%' ";

            //}
            //if (dateBegin > 0)
            //{
            //    sql += " AND A.LastStorageAgeDate >= " + dateBegin;
            //}
            //if (dateEnd > 0)
            //{
            //    sql += " AND A.LastStorageAgeDate <= " + dateEnd;
            //}



            string sql = @"SELECT
A.STNO,A.INVNO,A.STTYPE,B.STORAGEAGEDATE,A.MDATE,A.VENDORCODE,C.VENDORNAME,B.DQMCODE,B.MDESC,B.ACTQTY,D.SN,B.SUPPLIER_LOTNO
from 
tblasn A LEFT JOIN
TBLASNDETAIL B ON A.STNO = B.STNO LEFT JOIN
tblasndetailsn D on B.CARTONNO = D.CARTONNO and B.STNO = D.STNO LEFT JOIN
TBLVENDOR C ON A.VENDORCODE = C.VENDORCODE WHERE A.STATUS = 'Close'";




            if (!string.IsNullOrEmpty(invno))
            {
                sql += " AND a.INVNO='" + invno + "' ";

            }


            if (!string.IsNullOrEmpty(vendor))
            {
                sql += " AND A.VendorCode LIKE'%" + vendor + "%' ";

            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND b.DQMCODE LIKE'%" + dqmCode + "%' ";

            }
            if (!string.IsNullOrEmpty(dqmDesc))
            {
                sql += " AND b.MDESC LIKE'%" + dqmDesc + "%' ";

            }
            if (!string.IsNullOrEmpty(sn))
            {
                sql += " AND d.SN LIKE'%" + sn + "%' ";

            }
            if (!string.IsNullOrEmpty(supplierLotNo))
            {
                sql += " AND b.Supplier_LotNo LIKE'%" + supplierLotNo + "%' ";

            }
            if (dateBegin > 0)
            {
                sql += " AND a.mdate >= " + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND a.mdate <= " + dateEnd;
            }



            object[] oo = this.DataProvider.CustomQuery(typeof(ProductInstorageRecord), new PagerCondition(sql, "a.stno", inclusive, exclusive));
            if (oo != null && oo.Length > 0)
            {

                foreach (ProductInstorageRecord d in oo)
                    records.Add(d);

            }



            return records.ToArray();

        }
        public ProductInstorageRecord[] QueryProductInstorages(string invno, int dateBegin, int dateEnd, string vendor, string dqmCode, string dqmDesc, string sn, string supplierLotNo)
        {
            List<ProductInstorageRecord> records = new List<ProductInstorageRecord>();


            //string sql = @"SELECT A.Supplier_LotNo,a.sttype,B.SN,A.STORAGEQTY,A.MDESC,A.DQMCODE,D.VendorName ,C.VendorCode ,A.LastStorageAgeDate,A.StorageAgeDate,C.STTYPE,C.INVNO,C.STNO FROM TBLStorageDetail A left JOIN TBLASNDETAILSN B ON A.CARTONNO=B.CARTONNO  LEFT JOIN  TBLASN C ON a.STNO=C.STNO LEFT JOIN TBLVendor D ON C.VendorCode=D.VendorCode where 1=1 ";

            //if (!string.IsNullOrEmpty(invno))
            //{
            //    sql += " AND C.INVNO='" + invno + "' ";

            //}


            //if (!string.IsNullOrEmpty(vendor))
            //{
            //    sql += " AND C.VendorCode LIKE'%" + vendor + "%' ";

            //}
            //if (!string.IsNullOrEmpty(dqmCode))
            //{
            //    sql += " AND A.DQMCODE LIKE'%" + dqmCode + "%' ";

            //}
            //if (!string.IsNullOrEmpty(dqmDesc))
            //{
            //    sql += " AND A.MDESC LIKE'%" + dqmDesc + "%' ";

            //}
            //if (!string.IsNullOrEmpty(sn))
            //{
            //    sql += " AND A.SN LIKE'%" + sn + "%' ";

            //}
            //if (!string.IsNullOrEmpty(supplierLotNo))
            //{
            //    sql += " AND A.Supplier_LotNo LIKE'%" + supplierLotNo + "%' ";

            //}
            //if (dateBegin > 0)
            //{
            //    sql += " AND A.LastStorageAgeDate >= " + dateBegin;
            //}
            //if (dateEnd > 0)
            //{
            //    sql += " AND A.LastStorageAgeDate <= " + dateEnd;
            //}

            //sql += " order by c.stno ";
            string sql = @"SELECT
A.STNO,A.INVNO,A.STTYPE,B.STORAGEAGEDATE,A.MDATE,A.VENDORCODE,C.VENDORNAME,B.DQMCODE,B.MDESC,B.ACTQTY,D.SN,B.SUPPLIER_LOTNO
from 
tblasn A LEFT JOIN
TBLASNDETAIL B ON A.STNO = B.STNO LEFT JOIN
tblasndetailsn D on B.CARTONNO = D.CARTONNO and B.STNO = D.STNO LEFT JOIN
TBLVENDOR C ON A.VENDORCODE = C.VENDORCODE WHERE A.STATUS = 'Close'";




            if (!string.IsNullOrEmpty(invno))
            {
                sql += " AND a.INVNO='" + invno + "' ";

            }


            if (!string.IsNullOrEmpty(vendor))
            {
                sql += " AND A.VendorCode LIKE'%" + vendor + "%' ";

            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND b.DQMCODE LIKE'%" + dqmCode + "%' ";

            }
            if (!string.IsNullOrEmpty(dqmDesc))
            {
                sql += " AND b.MDESC LIKE'%" + dqmDesc + "%' ";

            }
            if (!string.IsNullOrEmpty(sn))
            {
                sql += " AND d.SN LIKE'%" + sn + "%' ";

            }
            if (!string.IsNullOrEmpty(supplierLotNo))
            {
                sql += " AND b.Supplier_LotNo LIKE'%" + supplierLotNo + "%' ";

            }
            if (dateBegin > 0)
            {
                sql += " AND a.mdate >= " + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND a.mdate <= " + dateEnd;
            }

            sql += " order by a.stno ";





            object[] oo = this.DataProvider.CustomQuery(typeof(ProductInstorageRecord), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {

                foreach (ProductInstorageRecord d in oo)
                    records.Add(d);

            }



            return records.ToArray();

        }

        public ProductOutstorageRecord[] QueryProductOutstorages(int dateBegin, int dateEnd, string dqmCode, string dqmDesc, string sn, string supplierLotNo, string projectName, string orderNo, string cusorderNo)
        {
            List<ProductOutstorageRecord> records = new List<ProductOutstorageRecord>();


            string sql = @"select
T1.DQMCODE,F.MCHSHORTDESC MDESC,T1.CUSORDERNO,T1.pqty,T1.CARINVNO,T1.ProjectName,T1.PICKNO,T1.INVNO,T1.PICKTYPE,T1.Delivery_Date,T1.ORDERNO,T1.ReceiverAddr,t1.SN,T1.Supplier_LotNo,E.VENDORCODE 
from(
SELECT A.DQMCODE,A.MDESC,A.CUSORDERNO,A.pqty, B.CARINVNO,B.ProjectName,C.PICKNO,C.INVNO,C.PICKTYPE,C.Delivery_Date,
C.ORDERNO,C.ReceiverAddr ,F.Supplier_LotNo,A.PICKLINE,d.sn
FROM TBLPICKDETAIL A INNER JOIN TBLCartonInvoices B ON A.PICKNO=B.PICKNO 
INNER JOIN  TBLPICK C ON A.PICKNO=C.PICKNO INNER JOIN
(SELECT MIN(Supplier_LotNo) Supplier_LotNo,PICKNO,PICKLINE FROM TBLPICKDetailMaterial GROUP BY PICKNO,PICKLINE) F ON A.PICKNO=F.PICKNO AND A.PICKLINE=F.PICKLINE
LEFT JOIN TBLPICKDetailMaterialSN D ON  A.PICKNO=D.PICKNO AND A.PICKLINE=D.PICKLINE

)T1  LEFT JOIN TBLInvoices E ON T1.INVNO=E.INVNO LEFT JOIN TBLMATERIAL F ON T1.DQMCODE=F.DQMCODE WHERE 1=1  ";

            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND  t1.DQMCODE='" + dqmCode + "' ";

            }

            if (!string.IsNullOrEmpty(dqmDesc))
            {
                sql += " AND t1.MDESC LIKE'%" + dqmDesc + "%' ";

            }


            if (!string.IsNullOrEmpty(sn))
            {
                sql += " AND t1.SN LIKE'%" + sn + "%' ";

            }
            if (!string.IsNullOrEmpty(supplierLotNo))
            {
                sql += " AND t1.Supplier_LotNo LIKE'%" + supplierLotNo + "%' ";

            }
            if (!string.IsNullOrEmpty(projectName))
            {
                sql += " AND t1.ProjectName LIKE'%" + projectName + "%' ";

            }

            if (!string.IsNullOrEmpty(orderNo))
            {
                sql += " AND t1.ORDERNO LIKE'%" + orderNo + "%' ";

            }
            if (!string.IsNullOrEmpty(cusorderNo))
            {
                sql += " AND  t1.CUSORDERNO LIKE'%" + cusorderNo + "%' ";

            }


            if (dateBegin > 0)
            {
                sql += " AND t1.Delivery_Date >= " + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND t1.Delivery_Date <= " + dateEnd;
            }

            sql += " order by t1.PICKNO ";



            object[] oo = this.DataProvider.CustomQuery(typeof(ProductOutstorageRecord), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {

                foreach (ProductOutstorageRecord d in oo)
                    records.Add(d);

            }
            return records.ToArray();
        }


        public ProductOutstorageRecord[] QueryProductOutstorages(int dateBegin, int dateEnd, string dqmCode, string dqmDesc, string sn, string supplierLotNo, string projectName, string orderNo, string cusorderNo, int inclusive, int exclusive)
        {
            List<ProductOutstorageRecord> records = new List<ProductOutstorageRecord>();


            string sql = @"select
T1.DQMCODE,F.MCHSHORTDESC MDESC,T1.CUSORDERNO,T1.pqty,T1.CARINVNO,T1.ProjectName,T1.PICKNO,T1.INVNO,T1.PICKTYPE,T1.Delivery_Date,T1.ORDERNO,T1.ReceiverAddr,t1.SN,T1.Supplier_LotNo,E.VENDORCODE 
from(
SELECT A.DQMCODE,A.MDESC,A.CUSORDERNO,A.pqty, B.CARINVNO,B.ProjectName,C.PICKNO,C.INVNO,C.PICKTYPE,C.Delivery_Date,
C.ORDERNO,C.ReceiverAddr ,F.Supplier_LotNo,A.PICKLINE,d.sn
FROM TBLPICKDETAIL A INNER JOIN TBLCartonInvoices B ON A.PICKNO=B.PICKNO 
INNER JOIN  TBLPICK C ON A.PICKNO=C.PICKNO INNER JOIN
(SELECT MIN(Supplier_LotNo) Supplier_LotNo,PICKNO,PICKLINE FROM TBLPICKDetailMaterial GROUP BY PICKNO,PICKLINE) F ON A.PICKNO=F.PICKNO AND A.PICKLINE=F.PICKLINE
LEFT JOIN TBLPICKDetailMaterialSN D ON  A.PICKNO=D.PICKNO AND A.PICKLINE=D.PICKLINE

)T1  LEFT JOIN TBLInvoices E ON T1.INVNO=E.INVNO LEFT JOIN TBLMATERIAL F ON T1.DQMCODE=F.DQMCODE WHERE 1=1  ";

            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND  t1.DQMCODE='" + dqmCode + "' ";

            }

            if (!string.IsNullOrEmpty(dqmDesc))
            {
                sql += " AND t1.MDESC LIKE'%" + dqmDesc + "%' ";

            }


            if (!string.IsNullOrEmpty(sn))
            {
                sql += " AND t1.SN LIKE'%" + sn + "%' ";

            }
            if (!string.IsNullOrEmpty(supplierLotNo))
            {
                sql += " AND t1.Supplier_LotNo LIKE'%" + supplierLotNo + "%' ";

            }
            if (!string.IsNullOrEmpty(projectName))
            {
                sql += " AND t1.ProjectName LIKE'%" + projectName + "%' ";

            }

            if (!string.IsNullOrEmpty(orderNo))
            {
                sql += " AND t1.ORDERNO LIKE'%" + orderNo + "%' ";

            }
            if (!string.IsNullOrEmpty(cusorderNo))
            {
                sql += " AND  t1.CUSORDERNO LIKE'%" + cusorderNo + "%' ";

            }


            if (dateBegin > 0)
            {
                sql += " AND t1.Delivery_Date >= " + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND t1.Delivery_Date <= " + dateEnd;
            }



            object[] oo = this.DataProvider.CustomQuery(typeof(ProductOutstorageRecord), new PagerCondition(sql, "t1.PICKNO", inclusive, exclusive));
            if (oo != null && oo.Length > 0)
            {

                foreach (ProductOutstorageRecord d in oo)
                    records.Add(d);

            }
            return records.ToArray();
        }

        public int QueryProductOutstoragesCount(int dateBegin, int dateEnd, string dqmCode, string dqmDesc, string sn, string supplierLotNo, string projectName, string orderNo, string cusorderNo)
        {
            string sql = @"select count(*)
from(
SELECT A.DQMCODE,A.MDESC,A.CUSORDERNO,A.pqty, B.CARINVNO,B.ProjectName,C.PICKNO,C.INVNO,C.PICKTYPE,C.Delivery_Date,
C.ORDERNO,C.ReceiverAddr ,F.Supplier_LotNo,A.PICKLINE,d.sn
FROM TBLPICKDETAIL A INNER JOIN TBLCartonInvoices B ON A.PICKNO=B.PICKNO 
INNER JOIN  TBLPICK C ON A.PICKNO=C.PICKNO INNER JOIN
(SELECT MIN(Supplier_LotNo) Supplier_LotNo,PICKNO,PICKLINE FROM TBLPICKDetailMaterial GROUP BY PICKNO,PICKLINE) F ON A.PICKNO=F.PICKNO AND A.PICKLINE=F.PICKLINE
LEFT JOIN TBLPICKDetailMaterialSN D ON  A.PICKNO=D.PICKNO AND A.PICKLINE=D.PICKLINE

)T1  LEFT JOIN TBLInvoices E ON T1.INVNO=E.INVNO LEFT JOIN TBLMATERIAL F ON T1.DQMCODE=F.DQMCODE WHERE 1=1  ";

            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND  t1.DQMCODE='" + dqmCode + "' ";

            }

            if (!string.IsNullOrEmpty(dqmDesc))
            {
                sql += " AND t1.MDESC LIKE'%" + dqmDesc + "%' ";

            }


            if (!string.IsNullOrEmpty(sn))
            {
                sql += " AND t1.SN LIKE'%" + sn + "%' ";

            }
            if (!string.IsNullOrEmpty(supplierLotNo))
            {
                sql += " AND t1.Supplier_LotNo LIKE'%" + supplierLotNo + "%' ";

            }
            if (!string.IsNullOrEmpty(projectName))
            {
                sql += " AND t1.ProjectName LIKE'%" + projectName + "%' ";

            }

            if (!string.IsNullOrEmpty(orderNo))
            {
                sql += " AND t1.ORDERNO LIKE'%" + orderNo + "%' ";

            }
            if (!string.IsNullOrEmpty(cusorderNo))
            {
                sql += " AND  t1.CUSORDERNO LIKE'%" + cusorderNo + "%' ";

            }


            if (dateBegin > 0)
            {
                sql += " AND t1.Delivery_Date >= " + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND t1.Delivery_Date <= " + dateEnd;
            }


            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public InstorageSummary[] QueryInstorageSummarys(int dateBegin, int dateEnd, string storageCode, string sttype)
        {
            List<InstorageSummary> records = new List<InstorageSummary>();


            string sql = @"SELECT A.STORAGECODE,A.ASNCCOUNT,C.ASNDOWNCOUNT,D.ASNRECEIVECOUNT,E.ASNINSTORAGECOUNT FROM
                           (SELECT STORAGECODE,COUNT(*) ASNCCOUNT FROM TBLASN where 1=1 and storagecode is not null {0} {4} GROUP BY STORAGECODE) A LEFT JOIN
                           ( SELECT COUNT(*) ASNDOWNCOUNT,STORAGECODE  FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='ISSUE' and storagecode is not null {1} {5} GROUP BY STORAGECODE) C ON A.STORAGECODE=C.STORAGECODE LEFT JOIN
                           ( SELECT COUNT(*) ASNRECEIVECOUNT,STORAGECODE  FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='Receive' and storagecode is not null  {2} {5} GROUP BY STORAGECODE) D ON A.STORAGECODE=D.STORAGECODE LEFT JOIN
                           ( SELECT COUNT(*) ASNINSTORAGECOUNT,STORAGECODE  FROM TBLASN WHERE STATUS='Close' and storagecode is not null {3} {4} GROUP BY STORAGECODE) E ON A.STORAGECODE=E.STORAGECODE WHERE 1=1 ";

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += "  AND A.STORAGECODE='" + storageCode + "' ";

            }

            string dateFilter = string.Empty;
            if (dateBegin > 0)
                dateFilter += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                dateFilter += " AND  MDATE <=" + dateEnd;

            string typeFilter = string.Empty;
            string typeFilter1 = string.Empty;
            if (!string.IsNullOrEmpty(sttype))
            {
                typeFilter = " AND STTYPE='" + sttype + "'";
                typeFilter1 = " AND INVTYPE='" + sttype + "'";
            }
            sql = string.Format(sql, dateFilter, dateFilter, dateFilter, dateFilter, typeFilter, typeFilter1);




            sql += " order by A.STORAGECODE ";

            object[] oo = this.DataProvider.CustomQuery(typeof(InstorageSummary), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {

                foreach (InstorageSummary d in oo)
                    records.Add(d);

            }
            return records.ToArray();
        }

        public int ReceiveSummaryCartonno(string stroageCode, int dateBegin, int dateEnd)
        {

            string sql = "SELECT COUNT(*) FROM TBLASNDETAIL WHERE STNO IN( SELECt transno FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='Receive' ";
            if (!string.IsNullOrEmpty(stroageCode))
            {
                sql += " AND STORAGECODE='" + stroageCode + "' ";
            }
            if (dateBegin > 0)
                sql += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  MDATE <=" + dateEnd;
            sql += ")";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public decimal ReceiveSummaryWeight(string stroageCode, int dateBegin, int dateEnd)
        {

            string sql = "SELECT SUM(GROSS_WEIGHT) GROSS_WEIGHT  FROM TBLASN WHERE STNO IN( SELECt transno FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='Receive' ";
            if (!string.IsNullOrEmpty(stroageCode))
            {
                sql += " AND STORAGECODE='" + stroageCode + "' ";
            }
            if (dateBegin > 0)
                sql += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  MDATE <=" + dateEnd;
            sql += ")";
            object[] os = this.DataProvider.CustomQuery(typeof(WeightVolume), new SQLCondition(sql));
            return Math.Round(((WeightVolume)os[0]).GROSS_WEIGHT, 2);

        }

        public decimal ReceiveSummaryVolume(string stroageCode, int dateBegin, int dateEnd)
        {

            string sql = "SELECT SUM(VOLUME) VOLUME FROM TBLASN WHERE STNO IN( SELECt transno FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='Receive' ";
            if (!string.IsNullOrEmpty(stroageCode))
            {
                sql += " AND STORAGECODE='" + stroageCode + "' ";
            }
            if (dateBegin > 0)
                sql += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  MDATE <=" + dateEnd;
            sql += ")";

            object[] os = this.DataProvider.CustomQuery(typeof(WeightVolume), new SQLCondition(sql));
            return Math.Round(((WeightVolume)os[0]).Volume, 2);
        }

        public int OnShelfSummaryCartonno(string stroageCode, int dateBegin, int dateEnd)
        {

            string sql = "SELECT COUNT(*) FROM TBLASNDETAIL WHERE STNO IN( SELECt transno FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='INSTORAEE' ";
            if (!string.IsNullOrEmpty(stroageCode))
            {
                sql += " AND STORAGECODE='" + stroageCode + "' ";
            }
            if (dateBegin > 0)
                sql += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  MDATE <=" + dateEnd;
            sql += ")";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public decimal OnShelfSummaryWeight(string stroageCode, int dateBegin, int dateEnd)
        {

            string sql = "SELECT SUM(GROSS_WEIGHT) GROSS_WEIGHT FROM TBLASN WHERE STNO IN( SELECt transno FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='INSTORAEE' ";
            if (!string.IsNullOrEmpty(stroageCode))
            {
                sql += " AND STORAGECODE='" + stroageCode + "' ";
            }
            if (dateBegin > 0)
                sql += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  MDATE <=" + dateEnd;
            sql += ")";

            object[] os = this.DataProvider.CustomQuery(typeof(WeightVolume), new SQLCondition(sql));
            return Math.Round(((WeightVolume)os[0]).GROSS_WEIGHT, 2);
        }

        public decimal OnShelfSummaryVolume(string stroageCode, int dateBegin, int dateEnd)
        {

            string sql = "SELECT SUM(VOLUME) VOLUME FROM TBLASN WHERE STNO IN( SELECt transno FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='INSTORAEE' ";
            if (!string.IsNullOrEmpty(stroageCode))
            {
                sql += " AND STORAGECODE='" + stroageCode + "' ";
            }
            if (dateBegin > 0)
                sql += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  MDATE <=" + dateEnd;
            sql += ")";

            object[] os = this.DataProvider.CustomQuery(typeof(WeightVolume), new SQLCondition(sql));
            return Math.Round(((WeightVolume)os[0]).Volume, 2);
        }

        public decimal ReceiveAverPeriod(string stroageCode, int dateBegin, int dateEnd)
        {

            string sql = "select transno,MAX(MDATE||'-'||MTIME) maxdate ,MIN(MDATE||'-'||MTIME) mindate FROM TBLINVINOUTTRANS WHERE TransType='IN' AND (PROCESSTYPE='Receive' OR PROCESSTYPE='RECEIVEBEGIN') ";
            if (!string.IsNullOrEmpty(stroageCode))
            {
                sql += " AND STORAGECODE='" + stroageCode + "' ";
            }
            if (dateBegin > 0)
                sql += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  MDATE <=" + dateEnd;
            sql += "GROUP BY  transno";

            object[] oo = this.DataProvider.CustomQuery(typeof(DateWithTimeRange), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (oo != null && oo.Length > 0)
            {


                foreach (DateWithTimeRange d in oo)
                {
                    string[] maxStrs = d.MaxDate.Split('-');

                    string maxDateStr = maxStrs[0];
                    string maxTimeStr = maxStrs[1];

                    string[] minStrs = d.MinDate.Split('-');
                    string minxDateStr = minStrs[0];
                    string minTimeStr = minStrs[1];


                    ls.Add(
                        Totalday(int.Parse(maxDateStr),
                                 int.Parse(maxTimeStr),
                                 int.Parse(minxDateStr),
                                 int.Parse(minTimeStr)));

                }
            }

            decimal sum = 0;
            foreach (decimal one in ls)
            {
                sum += one;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);

            return 0;
        }

        public decimal IQCAverPeriod(string stroageCode, string vendorCode, string sttype, int dateBegin, int dateEnd)
        {

            string sql = "select transno,MAX(A.MDATE||'-'||A.MTIME) maxdate ,MIN(B.CDATE||'-'||B.CTIME) mindate FROM TBLINVINOUTTRANS A,TBLASNIQC B,TBLASN C WHERE (A.TRANSNO=B.STNO OR A.TRANSNO=B.IQCNO) AND B.STNO=C.STNO AND A.TransType='IN' AND A.PROCESSTYPE='IQC' ";
            if (!string.IsNullOrEmpty(stroageCode))
                sql += " AND C.STORAGECODE='" + stroageCode + "' ";
            if (!string.IsNullOrEmpty(sttype))
                sql += " AND c.sttype='" + sttype + "' ";
            if (!string.IsNullOrEmpty(vendorCode))
                sql += " AND c.VENDORCODE='" + vendorCode + "' ";
            if (dateBegin > 0)
                sql += " AND  A.MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.MDATE <=" + dateEnd;
            sql += "GROUP BY  A.transno";

            object[] oo = this.DataProvider.CustomQuery(typeof(DateWithTimeRange), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (oo != null && oo.Length > 0)
            {


                foreach (DateWithTimeRange d in oo)
                {
                    string[] maxStrs = d.MaxDate.Split('-');
                    string maxDateStr = maxStrs[0];
                    string maxTimeStr = maxStrs[1];

                    string[] minxStrs = d.MinDate.Split('-');
                    string minDateStr = minxStrs[0];
                    string minTimeStr = minxStrs[1];

                    decimal d1 = Totalday(int.Parse(maxDateStr),
                                 int.Parse(maxTimeStr),
                                 int.Parse(minDateStr),
                                 int.Parse(minTimeStr));
                    if (d1 != 0.0M)
                        ls.Add(d1);

                }
            }

            decimal sum = 0;

            foreach (decimal one in ls)
            {

                sum += one;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }

        public decimal Totalday(int ocCheckFinishDate, int ocCheckFinishTime, int CDate, int CTime)
        {
            if (ocCheckFinishDate == 0 || CDate == 0)
                return -1;
            DateTime dtone = FormatHelper.ToDateTime(ocCheckFinishDate, ocCheckFinishTime);
            DateTime dttwo = FormatHelper.ToDateTime(CDate, CTime);
            TimeSpan ts = dtone - dttwo;
            decimal days = (decimal)ts.Days;


            if (ts.Hours >= 8)
                days += 1;
            else
                days += (ts.Hours / 8M);


            return Math.Round(days, 2);
        }



        public decimal QCAverPeriod(string stroageCode, string sttype, string verdorCode, int dateBegin, int dateEnd)
        {

            string sql = "select transno,MAX(A.MDATE||'-'||A.MTIME) maxdate ,MIN(B.CDATE||'-'||B.CTIME) mindate FROM TBLINVINOUTTRANS A,TBLASNIQC B ,TBLASN C WHERE A.TRANSNO=B.STNO AND A.TRANSNO=C.STNO AND A.TransType='IN' AND (A.PROCESSTYPE='IQC' OR A.PROCESSTYPE='IQCSQE') ";
            if (!string.IsNullOrEmpty(stroageCode))
                sql += " AND A.STORAGECODE='" + stroageCode + "' ";
            if (!string.IsNullOrEmpty(sttype))
                sql += " AND c.sttype='" + sttype + "' ";

            if (!string.IsNullOrEmpty(verdorCode))
                sql += " AND c.VENDORCODE='" + verdorCode + "' ";
            if (dateBegin > 0)
                sql += " AND  A.MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.MDATE <=" + dateEnd;


            sql += "GROUP BY  A.transno";

            object[] oo = this.DataProvider.CustomQuery(typeof(DateWithTimeRange), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (oo != null && oo.Length > 0)
            {
                foreach (DateWithTimeRange d in oo)
                {
                    string[] maxStrs = d.MaxDate.Split('-');
                    string maxDateStr = maxStrs[0];
                    string maxTimeStr = maxStrs[1];

                    string[] minxStrs = d.MinDate.Split('-');
                    string minDateStr = maxStrs[0];
                    string minTimeStr = maxStrs[1];


                    ls.Add(
                        Totalday(int.Parse(maxDateStr),
                                 int.Parse(maxTimeStr),
                                 int.Parse(minDateStr),
                                 int.Parse(minTimeStr)));

                }
            }

            decimal sum = 0;
            foreach (decimal one in ls)
            {
                sum += one;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }

        public decimal SQEAverPeriod(string stroageCode, string sttype, string verdorCode, int dateBegin, int dateEnd)
        {

            string sql = "select transno,MAX(A.MDATE||'-'||A.MTIME) maxdate ,MIN(A.MDATE||'-'||A.MTIME) mindate FROM TBLINVINOUTTRANS A,TBLASN C WHERE A.TRANSNO=C.STNO AND A.TransType='IN' AND (A.PROCESSTYPE='IQC' OR A.PROCESSTYPE='IQCSQE') ";
            if (!string.IsNullOrEmpty(stroageCode))
                sql += " AND A.STORAGECODE='" + stroageCode + "' ";
            if (!string.IsNullOrEmpty(sttype))
                sql += " AND c.sttype='" + sttype + "' ";
            if (!string.IsNullOrEmpty(verdorCode))
                sql += " AND c.VENDORCODE='" + verdorCode + "' ";
            if (dateBegin > 0)
                sql += " AND  A.MDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.MDATE <=" + dateEnd;
            sql += "GROUP BY  A.transno";

            object[] oo = this.DataProvider.CustomQuery(typeof(DateWithTimeRange), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (oo != null && oo.Length > 0)
            {
                foreach (DateWithTimeRange d in oo)
                {
                    string[] maxStrs = d.MaxDate.Split('-');
                    string maxDateStr = maxStrs[0];
                    string maxTimeStr = maxStrs[1];

                    string[] minxStrs = d.MinDate.Split('-');
                    string minDateStr = maxStrs[0];
                    string minTimeStr = maxStrs[1];


                    ls.Add(
                        Totalday(int.Parse(maxDateStr),
                                 int.Parse(maxTimeStr),
                                 int.Parse(minDateStr),
                                 int.Parse(minTimeStr)));

                }
            }

            decimal sum = 0;
            foreach (decimal one in ls)
            {
                sum += one;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }


        public IQCSUMMARY[] QueryIQCSummarys(int dateBegin, int dateEnd, string storageCode, string sttype, string verdorCode)
        {
            List<IQCSUMMARY> records = new List<IQCSUMMARY>();


            string sql = @"SELECT T0.STNO,T0.STORAGECODE,T0.VENDORCODE,T0.STTYPE, T1.IQCCCOUNT ,T2.QCCOUNT,T3.SQECOUNT,T4.SAMPLECOUNT,T5.QTY,T6.NGQTY,T6.RETURNQTY,T6.REFORMQTY,T6.GIVEQTY,T6.ACCEPTQTY,T1.STNO FROM
                           (SELECT STNO,STORAGECODE,VENDORCODE,STTYPE FROM TBLASN) T0 LEFT JOIN
                           (SELECT COUNT(A.IQCNO) IQCCCOUNT,A.STNO  FROM TBLASNIQC A WHERE 1=1 {0} GROUP BY A.STNO ) T1 ON T0.STNO=T1.STNO LEFT JOIN
                           (SELECT COUNT(A.IQCNO) QCCOUNT,A.STNO  FROM TBLASNIQC A WHERE A.STATUS='IQCClose' {0} GROUP BY A.STNO ) T2 ON T0.STNO=T2.STNO LEFT JOIN 
                           (SELECT COUNT(*) SQECOUNT,A.STNO FROM TBLINVINOUTTRANS B,TBLASNIQC A WHERE A.STNO=B.TRANSNO AND B.PROCESSTYPE='IQCSQE' {0}  GROUP BY A.STNO ) T3 ON T0.STNO=T3.STNO LEFT JOIN
                           (SELECT SUM(B.SampleSize) SAMPLECOUNT,A.STNO FROM TBLAQL B,TBLASNIQC A WHERE B.AQLLEVEL=A.AQLLEVEL GROUP BY A.STNO) T4 ON T0.STNO=T4.STNO LEFT JOIN
                           (SELECT SUM(A.QTY) QTY,A.STNO FROM TBLASNDETAILITEM A  GROUP BY A.STNO) T5 ON T0.STNO=T5.STNO LEFT JOIN 
                           (SELECT A.STNO, SUM(A.NGQTY) NGQTY,SUM(A.RETURNQTY) RETURNQTY,SUM(A.REFORMQTY) REFORMQTY,SUM(A.GIVEQTY) GIVEQTY,SUM(A.ACCEPTQTY) ACCEPTQTY FROM TBLASNIQCDETAIL A WHERE 1=1 {0} GROUP BY A.STNO) T6 ON T0.STNO=T6.STNO WHERE 1=1 ";

            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND T0.storageCode='" + storageCode + "' ";
            if (!string.IsNullOrEmpty(sttype))
                sql += " AND T0.STTYPE='" + sttype + "' ";
            if (!string.IsNullOrEmpty(verdorCode))
                sql += " AND T0.VENDORCODE='" + verdorCode + "' ";

            string dateFilter = string.Empty;
            if (dateBegin > 0)
                dateFilter += " AND A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                dateFilter += " AND A.CDATE <=" + dateEnd;
            sql = string.Format(sql, dateFilter);



            object[] oo = this.DataProvider.CustomQuery(typeof(IQCSUMMARY), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
            {
                IQCSUMMARY summary = new IQCSUMMARY();
                foreach (IQCSUMMARY d in oo)
                {
                    summary.NGQTY += d.NGQTY;
                    summary.ACCEPTQTY += d.ACCEPTQTY;
                    summary.GIVEQTY += d.GIVEQTY;
                    summary.IQCCCOUNT += d.IQCCCOUNT;
                    summary.QCCOUNT += d.QCCOUNT;
                    summary.QTY += d.QTY;
                    summary.REFORMQTY += d.REFORMQTY;
                    summary.RETURNQTY += d.RETURNQTY;
                    summary.SAMPLECOUNT += d.SAMPLECOUNT;
                    summary.SQECOUNT += d.SQECOUNT;


                }

                records.Add(summary);
            }



            return records.ToArray();
        }

        public IQCSUMMARY[] EGGroupQty(string ECGGroup, string storageCode, string sttype, string vendorCode, int dateBegin, int dateEnd)
        {

            string sql = "SELECT SUM(NGQTY) NGQTY,B.STNO FROM TBLASNIQCDETAILEC A ,TBLASN B WHERE A.STNO=B.STNO ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND B.storageCode='" + storageCode + "' ";
            if (!string.IsNullOrEmpty(sttype))
                sql += " AND B.STTYPE='" + sttype + "' ";
            if (!string.IsNullOrEmpty(vendorCode))
                sql += " AND B.VENDORCODE='" + vendorCode + "' ";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            if (!string.IsNullOrEmpty(ECGGroup))
                sql += "AND A.ECGCODE='" + ECGGroup + "'";
            sql += "GROUP BY B.STNO";

            object[] os = this.DataProvider.CustomQuery(typeof(IQCSUMMARY), new SQLCondition(sql));
            List<IQCSUMMARY> summarys = new List<IQCSUMMARY>();
            if (os != null && os.Length > 0)
            {
                foreach (IQCSUMMARY s in os)
                    summarys.Add(s);

            }
            return summarys.ToArray();
        }

        public OutStorageDetail[] QueryOutStorageDetails(string storageCode, string pickType, int dateBegin, int dateEnd, int inclusive, int exclusive)
        {
            string sql = @"SELECT A.Delivery_Date,A.Delivery_Time,A.Packing_List_Date,A.Packing_List_Time,A.PICKNO,A.INVNO,A.PICKTYPE,A.STORAGECODE,A.CDATE,A.CTIME,A.Down_Date,A.Down_Time,B.CARTONNOS,B.VOLUMN,B.GROSS_WEIGHT,B.CDATETIME,TPICK.MAXPICK,TPICK.MINPICK,TPACK.MAXPACK,TPACK.MINPACK,TOQC.OQCCDATE,TOQC.OQCDATE FROM TBLPICK A LEFT JOIN 
                           (SELECT T1.PICKNO,SUM(CARTONNOS) CARTONNOS,SUM(TO_NUMBER(VOLUME)) VOLUMN,SUM(GROSS_WEIGHT) GROSS_WEIGHT,MAX(CDATE||'-'||CTIME) CDATETIME FROM TBLCartonInvoices T1,(SELECT COUNT(CARTONNO) CARTONNOS,CARINVNO FROM TBLCartonInvDetailMaterial GROUP BY CARINVNO) T2 WHERE T1.CARINVNO=T2.CARINVNO GROUP BY T1.PICKNO ) B  ON A.PICKNO=B.PICKNO LEFT JOIN 
                           (SELECT MAX(MDATE||'-'||MTIME) MAXPICK,(select MIN(CDATE||'-'||CTIME) from TBLPICKDETAILMATERIAL PM WHERE PM.PICKNO=trif.transno ) MINPICK,TRANSNO FROM TBLInvInOutTrans trif WHERE TransType='OUT' AND (PROCESSTYPE='PICK' OR PROCESSTYPE='ClosePick') GROUP BY TRANSNO ) TPICK ON A.PICKNO=TPICK.TRANSNO LEFT JOIN
                           (SELECT MAX(MDATE||'-'||MTIME) MAXPACK,(select MIN(CDATE||'-'||CTIME) from TBLCARTONINVDETAILMATERIAL tcm WHERE tcm.PICKNO=trif.transno ) MINPACK,TRANSNO FROM TBLInvInOutTrans trif WHERE TransType='OUT' AND (PROCESSTYPE='PACK' OR PROCESSTYPE='ClosePack') GROUP BY TRANSNO ) TPACK ON A.PICKNO=TPACK.TRANSNO LEFT JOIN 
                           (SELECT TP.PICKNO,MIN(TC.CDATE||'-'||TC.CTIME) OQCCDATE,MAX(TT.MDATE||'-'||TT.MTIME) OQCDATE FROM TBLPICK TP LEFT JOIN TBLOQC  TC ON TP.PICKNO=TC.PICKNO LEFT JOIN (SELECT TRANSNO,MDATE,MTIME FROM TBLInvInOutTrans WHERE PROCESSTYPE='OQC' AND  TransType='OUT' ) TT ON TP.PICKNO=TT.TRANSNO GROUP BY TP.PICKNO) TOQC ON TOQC.PICKNO=A.PICKNO where 1=1";

            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.storageCode='" + storageCode + "' ";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.picktype='" + pickType + "' ";

            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;

            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new PagerCondition(sql, "a.pickno", inclusive, exclusive));
            List<OutStorageDetail> summarys = new List<OutStorageDetail>();
            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail s in os)
                    summarys.Add(s);

            }
            return summarys.ToArray();
        }


        public int QueryOutStorageDetailsCount(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT count(*) FROM TBLPICK A LEFT JOIN 
                           (SELECT T1.PICKNO,SUM(CARTONNOS) CARTONNOS,SUM(TO_NUMBER(VOLUME)) VOLUMN,SUM(GROSS_WEIGHT) GROSS_WEIGHT,MAX(CDATE||'-'||CTIME) CDATETIME FROM TBLCartonInvoices T1,(SELECT COUNT(CARTONNO) CARTONNOS,CARINVNO FROM TBLCartonInvDetailMaterial GROUP BY CARINVNO) T2 WHERE T1.CARINVNO=T2.CARINVNO GROUP BY T1.PICKNO ) B  ON A.PICKNO=B.PICKNO LEFT JOIN 
                           (SELECT MAX(MDATE||'-'||MTIME) MAXPICK,MIN(MDATE||'-'||MTIME) MINPICK,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND (PROCESSTYPE='PICK' OR PROCESSTYPE='ClosePick') GROUP BY TRANSNO ) TPICK ON A.PICKNO=TPICK.TRANSNO LEFT JOIN
                           (SELECT MAX(MDATE||'-'||MTIME) MAXPACK,MIN(MDATE||'-'||MTIME) MINPACK,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND (PROCESSTYPE='PACK' OR PROCESSTYPE='ClosePack') GROUP BY TRANSNO ) TPACK ON A.PICKNO=TPACK.TRANSNO LEFT JOIN 
                           (SELECT TP.PICKNO,MIN(TC.CDATE||'-'||TC.CTIME) OQCCDATE,MAX(TT.MDATE||'-'||TT.MTIME) OQCDATE FROM TBLPICK TP LEFT JOIN TBLOQC  TC ON TP.PICKNO=TC.PICKNO LEFT JOIN (SELECT TRANSNO,MDATE,MTIME FROM TBLInvInOutTrans WHERE PROCESSTYPE='OQC' AND  TransType='OUT' ) TT ON TP.PICKNO=TT.TRANSNO GROUP BY TP.PICKNO) TOQC ON TOQC.PICKNO=A.PICKNO where 1=1";

            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.storageCode='" + storageCode + "' ";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.picktype='" + pickType + "' ";

            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public OutStorageDetail[] QueryOutStorageDetails(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.Delivery_Date,A.Delivery_Time,A.Packing_List_Date,A.Packing_List_Time,A.PICKNO,A.INVNO,A.PICKTYPE,A.STORAGECODE,A.CDATE,A.CTIME,A.Down_Date,A.Down_Time,B.CARTONNOS,B.VOLUMN,B.GROSS_WEIGHT,B.CDATETIME,TPICK.MAXPICK,TPICK.MINPICK,TPACK.MAXPACK,TPACK.MINPACK,TOQC.OQCCDATE,TOQC.OQCDATE FROM TBLPICK A LEFT JOIN 
                           (SELECT T1.PICKNO,SUM(CARTONNOS) CARTONNOS,SUM(TO_NUMBER(VOLUME)) VOLUMN,SUM(GROSS_WEIGHT) GROSS_WEIGHT,MAX(CDATE||'-'||CTIME) CDATETIME FROM TBLCartonInvoices T1,(SELECT COUNT(CARTONNO) CARTONNOS,CARINVNO FROM TBLCartonInvDetailMaterial GROUP BY CARINVNO) T2 WHERE T1.CARINVNO=T2.CARINVNO GROUP BY T1.PICKNO ) B  ON A.PICKNO=B.PICKNO LEFT JOIN 
                           (SELECT MAX(MDATE||'-'||MTIME) MAXPICK,(select MIN(CDATE||'-'||CTIME) from TBLPICKDETAILMATERIAL PM WHERE PM.PICKNO=trif.transno ) MINPICK,TRANSNO FROM TBLInvInOutTrans trif WHERE TransType='OUT' AND (PROCESSTYPE='PICK' OR PROCESSTYPE='ClosePick') GROUP BY TRANSNO ) TPICK ON A.PICKNO=TPICK.TRANSNO LEFT JOIN
                           (SELECT MAX(MDATE||'-'||MTIME) MAXPACK,(select MIN(CDATE||'-'||CTIME) from TBLCARTONINVDETAILMATERIAL tcm WHERE tcm.PICKNO=trif.transno ) MINPACK,TRANSNO FROM TBLInvInOutTrans trif WHERE TransType='OUT' AND (PROCESSTYPE='PACK' OR PROCESSTYPE='ClosePack') GROUP BY TRANSNO ) TPACK ON A.PICKNO=TPACK.TRANSNO LEFT JOIN 
                           (SELECT TP.PICKNO,MIN(TC.CDATE||'-'||TC.CTIME) OQCCDATE,MAX(TT.MDATE||'-'||TT.MTIME) OQCDATE FROM TBLPICK TP LEFT JOIN TBLOQC  TC ON TP.PICKNO=TC.PICKNO LEFT JOIN (SELECT TRANSNO,MDATE,MTIME FROM TBLInvInOutTrans WHERE PROCESSTYPE='OQC' AND  TransType='OUT' ) TT ON TP.PICKNO=TT.TRANSNO GROUP BY TP.PICKNO) TOQC ON TOQC.PICKNO=A.PICKNO where 1=1";




            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.storageCode='" + storageCode + "' ";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.picktype='" + pickType + "' ";

            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            sql += " order by a.pickno ";
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<OutStorageDetail> summarys = new List<OutStorageDetail>();
            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail s in os)
                    summarys.Add(s);

            }
            return summarys.ToArray();
        }

        public int QueryOrderLines(string pickNo)
        {
            if (string.IsNullOrEmpty(pickNo))
                return 0;
            string sql = @" SELECT A.PICKNO,A.CUSER,B.PICKLINES,C.INVLINES FROM TBLPICK A LEFT JOIN 
                           (SELECT PICKNO, COUNT(*) PICKLINES FROM TBLPICKDETAIL GROUP BY PICKNO ) B ON A.PICKNO=B.PICKNO LEFT JOIN (SELECT COUNT(*) INVLINES,INVNO FROM TBLInvoicesDetail GROUP BY INVNO ) C ON C.INVNO=A.INVNO WHERE A.PICKNO='" + pickNo + "'";


            object[] os = this.DataProvider.CustomQuery(typeof(OrderLines), new SQLCondition(sql));

            int sum = 0;
            if (os != null && os.Length > 0)
            {
                foreach (OrderLines line in os)
                {
                    if (line.INVLINES != 0 && line.CUSER == "JOB")
                        sum += line.INVLINES;
                    else
                        sum += line.PICKLINES;
                }


            }
            return sum;
        }


        public DateWithTime StrToDateWithTime(string dateString, char spliteChar)
        {
            if (!string.IsNullOrEmpty(dateString))
            {
                string[] strs = dateString.Split(spliteChar);
                if (strs.Length == 2)
                    return new DateWithTime { Date = !string.IsNullOrEmpty(strs[0]) ? int.Parse(strs[0]) : (int)0, Time = !string.IsNullOrEmpty(strs[1]) ? int.Parse(strs[1]) : (int)0 };
                else
                    return new DateWithTime();
            }
            else
                return new DateWithTime();
        }

        public OutStorageSummary[] QueryOutStorageSummarys(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.STORAGECODE,sum(picktotal) picktotal, SUM(B.ISSUECOUNT) ISSUECOUNT,SUM(C.PICKCOUNT) PICKCOUNT,SUM(D.PACKCOUNT) PACKCOUNT,SUM(E.FINISHCARTONNOCOUNT) FINISHCARTONNOCOUNT,SUM(F.CARTONNOS) CARTONNOS ,SUM(G.VOLUME) VOLUME,SUM(GROSS_WEIGHT) GROSS_WEIGHT FROM 
                                   (SELECT STORAGECODE,pickno,count(*) picktotal FROM TBLPICK WHERE storageCode IS NOT NULL {0} group by storagecode,pickno) A LEFT JOIN 
                                   (SELECT COUNT(*) ISSUECOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ISSUE'GROUP BY TRANSNO) B ON A.PICKNO=B.TRANSNO LEFT JOIN
                                   (SELECT 1 PICKCOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ClosePick' GROUP BY TRANSNO) C ON A.PICKNO=C.TRANSNO LEFT JOIN
                                   (SELECT 1 PACKCOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ClosePack' GROUP BY TRANSNO) D ON A.PICKNO=D.TRANSNO LEFT JOIN  
                                   (SELECT 1 FINISHCARTONNOCOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ClosePackingList' GROUP BY TRANSNO) E ON A.PICKNO=E.TRANSNO LEFT JOIN 
                                   (SELECT COUNT(*) CARTONNOS,PICKNO FROM TBLCartonInvDetailMaterial GROUP BY PICKNO ) F ON A.PICKNO=F.PICKNO LEFT JOIN
                                   (SELECT SUM(TO_NUMBER(VOLUME)) VOLUME,SUM(GROSS_WEIGHT) GROSS_WEIGHT,PICKNO FROM TBLCartonInvoices GROUP BY PICKNO ) G ON A.PICKNO=G.PICKNO WHERE 1=1 ";

            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND storageCode='" + storageCode + "' ";


            string filter = string.Empty;
            if (!string.IsNullOrEmpty(pickType))
                filter += " AND picktype='" + pickType + "' ";

            if (dateBegin > 0)
                filter += " AND  CDATE >=" + dateBegin;
            if (dateEnd > 0)
                filter += " AND  CDATE <=" + dateEnd;
            sql += " group by a.storageCode ";
            sql = string.Format(sql, filter);
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageSummary), new SQLCondition(sql));
            List<OutStorageSummary> summarys = new List<OutStorageSummary>();
            if (os != null && os.Length > 0)
            {
                foreach (OutStorageSummary s in os)
                    summarys.Add(s);

            }
            return summarys.ToArray();

        }


        public OutStorageSummary[] QueryOutStorageSummarys(string storageCode, string pickType, int dateBegin, int dateEnd, int inclusive, int exclusive)
        {
            string sql = @"SELECT A.STORAGECODE,sum(picktotal) picktotal, SUM(B.ISSUECOUNT) ISSUECOUNT,SUM(C.PICKCOUNT) PICKCOUNT,SUM(D.PACKCOUNT) PACKCOUNT,SUM(E.FINISHCARTONNOCOUNT) FINISHCARTONNOCOUNT,SUM(F.CARTONNOS) CARTONNOS ,SUM(G.VOLUME) VOLUME,SUM(GROSS_WEIGHT) GROSS_WEIGHT FROM 
                                   (SELECT STORAGECODE,pickno,count(*) picktotal FROM TBLPICK WHERE storageCode IS NOT NULL {0} group by storagecode,pickno) A LEFT JOIN 
                                   (SELECT COUNT(*) ISSUECOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ISSUE'GROUP BY TRANSNO) B ON A.PICKNO=B.TRANSNO LEFT JOIN
                                   (SELECT 1 PICKCOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ClosePick' GROUP BY TRANSNO) C ON A.PICKNO=C.TRANSNO LEFT JOIN
                                   (SELECT 1 PACKCOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ClosePack' GROUP BY TRANSNO) D ON A.PICKNO=D.TRANSNO LEFT JOIN  
                                   (SELECT 1 FINISHCARTONNOCOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ClosePackingList' GROUP BY TRANSNO) E ON A.PICKNO=E.TRANSNO LEFT JOIN 
                                   (SELECT COUNT(*) CARTONNOS,PICKNO FROM TBLCartonInvDetailMaterial GROUP BY PICKNO ) F ON A.PICKNO=F.PICKNO LEFT JOIN
                                   (SELECT SUM(TO_NUMBER(VOLUME)) VOLUME,SUM(GROSS_WEIGHT) GROSS_WEIGHT,PICKNO FROM TBLCartonInvoices GROUP BY PICKNO ) G ON A.PICKNO=G.PICKNO WHERE 1=1 ";

            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND storageCode='" + storageCode + "' ";


            string filter = string.Empty;
            if (!string.IsNullOrEmpty(pickType))
                filter += " AND picktype='" + pickType + "' ";

            if (dateBegin > 0)
                filter += " AND  CDATE >=" + dateBegin;
            if (dateEnd > 0)
                filter += " AND  CDATE <=" + dateEnd;
            sql += " group by a.storageCode ";
            sql = string.Format(sql, filter);
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageSummary), new PagerCondition(sql, "a.storageCode", inclusive, exclusive));
            List<OutStorageSummary> summarys = new List<OutStorageSummary>();
            if (os != null && os.Length > 0)
            {
                foreach (OutStorageSummary s in os)
                    summarys.Add(s);

            }
            return summarys.ToArray();
        }



        public int QueryOutStorageSummarysCount(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = "select count(*) from (";
            sql += @"SELECT count(*) FROM 
                                   (SELECT STORAGECODE,pickno,count(*) picktotal FROM TBLPICK WHERE storageCode IS NOT NULL {0} group by storagecode,pickno) A LEFT JOIN 
                                   (SELECT COUNT(*) ISSUECOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ISSUE'GROUP BY TRANSNO) B ON A.PICKNO=B.TRANSNO LEFT JOIN
                                   (SELECT 1 PICKCOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ClosePick' GROUP BY TRANSNO) C ON A.PICKNO=C.TRANSNO LEFT JOIN
                                   (SELECT 1 PACKCOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ClosePack' GROUP BY TRANSNO) D ON A.PICKNO=D.TRANSNO LEFT JOIN  
                                   (SELECT 1 FINISHCARTONNOCOUNT,TRANSNO FROM TBLInvInOutTrans WHERE TransType='OUT' AND PROCESSTYPE='ClosePackingList' GROUP BY TRANSNO) E ON A.PICKNO=E.TRANSNO LEFT JOIN 
                                   (SELECT COUNT(*) CARTONNOS,PICKNO FROM TBLCartonInvDetailMaterial GROUP BY PICKNO ) F ON A.PICKNO=F.PICKNO LEFT JOIN
                                   (SELECT SUM(TO_NUMBER(VOLUME)) VOLUME,SUM(GROSS_WEIGHT) GROSS_WEIGHT,PICKNO FROM TBLCartonInvoices GROUP BY PICKNO ) G ON A.PICKNO=G.PICKNO WHERE 1=1  ";

            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND storageCode='" + storageCode + "' ";


            string filter = string.Empty;
            if (!string.IsNullOrEmpty(pickType))
                filter += " AND picktype='" + pickType + "' ";

            if (dateBegin > 0)
                filter += " AND  CDATE >=" + dateBegin;
            if (dateEnd > 0)
                filter += " AND  CDATE <=" + dateEnd;
            sql += " group by a.storageCode ";
            sql += ")";
            sql = string.Format(sql, filter);

            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public decimal averOutStoragePeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = "SELECT Delivery_Date,Delivery_Time,CDATE,CTIME FROM TBLPICK A WHERE 1=1 ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (os != null && os.Length > 0)
            {
                foreach (Pick d in os)
                {
                    decimal t = Totalday(d.DeliveryDate, d.DeliveryTime, d.CDate, d.CTime);
                    if (t > 0)
                        ls.Add(t);
                }


            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }

        public decimal averWarePeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.STORAGECODE ,A.CDATE,A.PICKTYPE,a.PACKING_LIST_DATE,a.PACKING_LIST_TIME,B.MAXPICK FROM TBLPICK A inner JOIN 
                          (SELECT min(MDATE||'-'||MTIME) MAXPICK,TRANSNO FROM TBLInvInOutTrans B WHERE TransType='OUT' AND PROCESSTYPE='PICK' GROUP BY TRANSNO ) B ON B.TRANSNO=A.PICKNO  where 1=1 AND A.STORAGECODE='Y101' and a.packing_list_date<>0 ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail d in os)
                {
                    DateWithTime dwt = StrToDateWithTime(d.MAXPICK, '-');
                    decimal t = Totalday(d.Packing_List_Date, d.Packing_List_Time, dwt.Date, dwt.Time);
                    if (t > 0)
                        ls.Add(t);
                }
            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }


        public decimal averDownPeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = "SELECT Down_Date,Down_Time,CDATE,CTIME FROM TBLPICK A WHERE 1=1 ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (os != null && os.Length > 0)
            {
                foreach (Pick d in os)
                {
                    decimal t = Totalday(d.DownDate, d.DownTime, d.CDate, d.CTime);
                    if (t >= 0)
                        ls.Add(t);
                }

            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;

        }


        public decimal averPickPeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.STORAGECODE ,A.CDATE,A.PICKTYPE,B.MAXPICK,B.MINPICK FROM TBLPICK A LEFT JOIN 
                          (SELECT MAX(MDATE||'-'||MTIME) MAXPICK,MIN(MDATE||'-'||MTIME) MINPICK,TRANSNO FROM TBLInvInOutTrans B WHERE TransType='OUT' AND (PROCESSTYPE='ClosePick' OR PROCESSTYPE='PICK') GROUP BY TRANSNO ) B ON B.TRANSNO=A.PICKNO where 1=1 and B.MAXPICK is not null and B.MINPICK is not null ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail d in os)
                {
                    if (!string.IsNullOrEmpty(d.MINPICK) && !string.IsNullOrEmpty(d.MAXPICK))
                    {
                        DateWithTime dwtB = StrToDateWithTime(d.MINPICK, '-');
                        DateWithTime dwtE = StrToDateWithTime(d.MAXPICK, '-');
                        decimal t = Totalday(dwtE.Date, dwtE.Time, dwtB.Date, dwtB.Time);

                        ls.Add(t);
                    }

                }

            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }


        public decimal averPickOrderLinePeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.STORAGECODE,A.PICKNO,A.CDATE,A.PICKTYPE,B.MAXPICK,B.MINPICK FROM TBLPICK A LEFT JOIN 
                          (SELECT MAX(MDATE||'-'||MTIME) MAXPICK,MIN(MDATE||'-'||MTIME) MINPICK,TRANSNO FROM TBLInvInOutTrans B WHERE TransType='OUT' AND (PROCESSTYPE='ClosePick' OR PROCESSTYPE='PICK') GROUP BY TRANSNO ) B ON B.TRANSNO=A.PICKNO where 1=1 and B.MAXPICK is not null and B.MINPICK is not null ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            int totalOrderLine = 0;

            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail d in os)
                {
                    DateWithTime dwtB = StrToDateWithTime(d.MINPICK, '-');
                    DateWithTime dwtE = StrToDateWithTime(d.MAXPICK, '-');

                    decimal t = Totalday(dwtE.Date, dwtE.Time, dwtB.Date, dwtB.Time);
                    if (t > 0)
                        ls.Add(t);
                    totalOrderLine += QueryOrderLines(d.PICKNO);
                }

            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (totalOrderLine > 0)
                return Math.Round(sum / totalOrderLine, 2);
            return 0;
        }


        public decimal averPackPeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.STORAGECODE ,A.CDATE,A.PICKTYPE,B.MAXPACK,B.MINPACK FROM TBLPICK A LEFT JOIN 
                          (SELECT MAX(MDATE||'-'||MTIME) MAXPACK,MIN(MDATE||'-'||MTIME) MINPACK,TRANSNO FROM TBLInvInOutTrans B WHERE TransType='OUT' AND (PROCESSTYPE='ClosePack' OR PROCESSTYPE='PACK') GROUP BY TRANSNO ) B ON B.TRANSNO=A.PICKNO where B.MAXPACK is not null and B.MINPACK is not null ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail d in os)
                {
                    if (!string.IsNullOrEmpty(d.MINPACK) && !string.IsNullOrEmpty(d.MAXPACK))
                    {
                        DateWithTime dwtB = StrToDateWithTime(d.MINPACK, '-');
                        DateWithTime dwtE = StrToDateWithTime(d.MAXPACK, '-');
                        decimal t = Totalday(dwtE.Date, dwtE.Time, dwtB.Date, dwtB.Time);
                        if (t < 0)
                            t = 0M;
                        ls.Add(t);
                    }
                }

            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }

        public decimal averPackOrderLinePeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.STORAGECODE,A.PICKNO,A.CDATE,A.PICKTYPE,B.MAXPACK,B.MINPACK FROM TBLPICK A LEFT JOIN 
                          (SELECT MAX(MDATE||'-'||MTIME) MAXPACK,MIN(MDATE||'-'||MTIME) MINPACK,TRANSNO FROM TBLInvInOutTrans B WHERE TransType='OUT' AND (PROCESSTYPE='ClosePack' OR PROCESSTYPE='PACK') GROUP BY TRANSNO ) B ON B.TRANSNO=A.PICKNO where 1=1 and B.MAXPACK is not null and B.MINPACK is not null ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            int totalOrderLine = 0;

            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail d in os)
                {
                    DateWithTime dwtB = StrToDateWithTime(d.MINPACK, '-');
                    DateWithTime dwtE = StrToDateWithTime(d.MAXPACK, '-');
                    decimal t = Totalday(dwtE.Date, dwtE.Time, dwtB.Date, dwtB.Time);
                    if (t > 0)
                        ls.Add(t);
                    totalOrderLine += QueryOrderLines(d.PICKNO);
                }

            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (totalOrderLine > 0)
                return Math.Round(sum / totalOrderLine, 2);
            return 0;
        }



        public decimal averOQCPeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.STORAGECODE ,A.CDATE,A.PICKTYPE,B.OQCDATE,C.OQCCDATE FROM TBLPICK A LEFT JOIN 
                          (SELECT MAX(MDATE||'-'||MTIME) OQCDATE,TRANSNO FROM TBLInvInOutTrans B WHERE TransType='OUT' AND PROCESSTYPE='OQC' GROUP BY TRANSNO ) B ON B.TRANSNO=A.PICKNO LEFT JOIN
                          (SELECT MIN(MDATE||'-'||MTIME) OQCCDATE,PICKNO FROM TBLOQC GROUP BY PICKNO )C ON C.PICKNO= A.PICKNO WHERE 1=1 ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail d in os)
                {
                    if (!string.IsNullOrEmpty(d.OQCCDATE) && !string.IsNullOrEmpty(d.OQCDATE))
                    {
                        DateWithTime dwtB = StrToDateWithTime(d.OQCCDATE, '-');
                        DateWithTime dwtE = StrToDateWithTime(d.OQCDATE, '-');
                        decimal t = Totalday(dwtE.Date, dwtE.Time, dwtB.Date, dwtB.Time);

                        ls.Add(t);
                    }
                }

            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }


        public decimal averCARTONNOPeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.STORAGECODE ,A.CDATE,A.PICKTYPE,A.Packing_List_Date,A.Packing_List_Time,B.CDATETIME FROM TBLPICK A LEFT JOIN 
                            (SELECT MIN(CDATE||'-'||CTIME) CDATETIME,PICKNO FROM TBLCartonInvoices GROUP BY PICKNO) B ON A.PICKNO=B.PICKNO WHERE A.Packing_List_Date<>0 ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail d in os)
                {

                    DateWithTime dwtE = StrToDateWithTime(d.CDATETIME, '-');
                    decimal t = Totalday(d.Packing_List_Date, d.Packing_List_Time, dwtE.Date, dwtE.Time);

                    ls.Add(t);
                }

            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }

        public decimal averCARTONNOOrderLinesPeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.PICKNO,A.STORAGECODE ,A.CDATE,A.PICKTYPE,A.Packing_List_Date,A.Packing_List_Time,B.CDATETIME FROM TBLPICK A LEFT JOIN 
                            (SELECT MIN(CDATE||'-'||CTIME) CDATETIME,PICKNO FROM TBLCartonInvoices GROUP BY PICKNO) B ON A.PICKNO=B.PICKNO WHERE A.Packing_List_Date<>0";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();
            int totalOrderLine = 0;

            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail d in os)
                {
                 
                    DateWithTime dwtE = StrToDateWithTime(d.CDATETIME, '-');
                    decimal t = Totalday(d.Packing_List_Date, d.Packing_List_Time, dwtE.Date, dwtE.Time);
                    if (t >= 0)
                        ls.Add(t);

                    totalOrderLine += QueryOrderLines(d.PICKNO);
                }

            }

            decimal sum = 0;
            foreach (decimal d in ls)
            {
                sum += d;

            }
            if (totalOrderLine > 0)
                return Math.Round(sum / totalOrderLine, 2);
            return 0;
        }

        public decimal averDeliveryPeriod(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT A.PICKNO,A.STORAGECODE ,A.CDATE,A.PICKTYPE,A.Packing_List_Date,A.Packing_List_Time,A.Delivery_Time,A.Delivery_Date FROM TBLPICK A WHERE 1=1 ";
            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OutStorageDetail), new SQLCondition(sql));
            List<decimal> ls = new List<decimal>();


            if (os != null && os.Length > 0)
            {
                foreach (OutStorageDetail d in os)
                {
                    decimal t = Totalday(d.Delivery_Date, d.Delivery_Time, d.Packing_List_Date, d.Packing_List_Time);
                    if (t >= 0)
                        ls.Add(t);

                }
            }
            decimal sum = 0;
            foreach (decimal d in ls)
                sum += d;


            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }

        public int SumOrderLines(string storageCode, string pickType, int dateBegin, int dateEnd)
        {
            if (string.IsNullOrEmpty(storageCode))
                return 0;
            string sql = @" SELECT A.PICKNO, A.CUSER,A.PICKTYPE,A.CDATE,A.STORAGECODE,B.PICKLINES
 ,C.INVLINES
 ,D.DNBATCHLINES,A.STORAGECODE 
 FROM TBLPICK A LEFT JOIN 
                           (SELECT PICKNO, COUNT(*) PICKLINES FROM TBLPICKDETAIL GROUP BY PICKNO ) B ON A.PICKNO=B.PICKNO 
                           
                           LEFT JOIN (SELECT COUNT(*) INVLINES,INVNO FROM TBLInvoicesDetail GROUP BY INVNO ) C ON C.INVNO=A.INVNO
                           LEFT JOIN (
                           select sum(DNBATCHLINES) DNBATCHLINES ,DNBATCHNO  from (
                           SELECT T1.DNBATCHNO,T1.INVNO,T2.DNBATCHLINES  FROM TBLINVOICES T1 
                           LEFT JOIN (SELECT COUNT(*) DNBATCHLINES ,INVNO FROM TBLInvoicesDetail GROUP BY INVNO) T2 ON T1.INVNO=T2.INVNO) group by DNBATCHNO
                           ) D ON D.DNBATCHNO=A.INVNO
                            WHERE 1=1  ";

            if (!string.IsNullOrEmpty(storageCode))
                sql += " AND A.STORAGECODE='" + storageCode + "'";
            if (!string.IsNullOrEmpty(pickType))
                sql += " AND A.PICKTYPE='" + pickType + "'";
            if (dateBegin > 0)
                sql += " AND  A.CDATE >=" + dateBegin;
            if (dateEnd > 0)
                sql += " AND  A.CDATE <=" + dateEnd;
            object[] os = this.DataProvider.CustomQuery(typeof(OrderLines), new SQLCondition(sql));

            int sum = 0;
            if (os != null && os.Length > 0)
            {
                foreach (OrderLines line in os)
                {
                    if (line.CUSER.ToUpper() == "JOB")
                        sum += (line.INVLINES == 0) ? line.DNBATCHLINES : line.INVLINES;

                    else
                        sum += line.PICKLINES;
                }


            }
            return sum;
        }



        public CheckSummary[] QueryStockCheckSummarys(string[] storageCode, string stockCheckType, int dateBegin, int dateEnd)
        {

            string sql = @" SELECT A.STORAGECODE, SUM(PASSCOUNT) PASSCOUNT,SUM(TOTAL) TOTAL,SUM(CHECKNOTOTAL) CHECKNOTOTAL FROM ( SELECT CHECKNO,STORAGECODE FROM TBLSTOCKCHECK WHERE 1=1 ) A LEFT JOIN 
                            (SELECT COUNT(*) PASSCOUNT,CHECKNO FROM TBLSTOCKCHECKDETAIL WHERE CHECKQTY=STORAGEQTY GROUP BY CHECKNO )B ON A.CHECKNO=B.CHECKNO LEFT JOIN
                            (SELECT COUNT(*) TOTAL,CHECKNO FROM TBLSTOCKCHECKDETAIL GROUP BY CHECKNO) C ON A.CHECKNO=C.CHECKNO LEFT JOIN
                            (SELECT COUNT(*) CHECKNOTOTAL ,CHECKNO FROM TBLSTOCKCHECK GROUP BY CHECKNO ) D ON D.CHECKNO=C.CHECKNO
                             WHERE 1=1 ";

            if (storageCode.Length > 0)
                sql += "AND A.STORAGECODE IN(" + SqlFormat(storageCode) + ")";
            sql += " GROUP BY A.STORAGECODE";

            string filter = string.Empty;
            if (!string.IsNullOrEmpty(stockCheckType))
                filter += " AND CHECKTYPE='" + stockCheckType + "'";
            if (dateBegin > 0)
                filter += " AND  CDATE >=" + dateBegin;
            if (dateEnd > 0)
                filter += " AND  CDATE <=" + dateEnd;
            sql = string.Format(sql, filter);
            sql += " order by a.STORAGECODE";

            object[] os = this.DataProvider.CustomQuery(typeof(CheckSummary), new SQLCondition(sql));

            List<CheckSummary> summarys = new List<CheckSummary>();
            if (os != null && os.Length > 0)
            {
                foreach (CheckSummary line in os)
                    summarys.Add(line);
            }
            return summarys.ToArray();
        }


        public AlertMailSetting[] QueryAlertMailSetting()
        {
            string sql = "SELECT * FROM TBLALERTMAILSETTING";

            object[] objs = this.DataProvider.CustomQuery(typeof(AlertMailSetting), new SQLCondition(sql));
            List<AlertMailSetting> settings = new List<AlertMailSetting>();
            if (objs != null && objs.Length > 0)
            {
                foreach (AlertMailSetting setting in objs)
                    settings.Add(setting);
            }
            return settings.ToArray();
        }

        public string Recipients(string forewarningName)
        {
            string sql = @"SELECT * FROM TBLALERTMAILSETTING WHERE ITEMSEQUENCE='" + forewarningName + "' ";


            object[] objs = this.DataProvider.CustomQuery(typeof(AlertMailSetting), new SQLCondition(sql));

            StringBuilder sb = new StringBuilder(200);
            if (objs != null && objs.Length > 0)
            {
                foreach (AlertMailSetting setting in objs)
                {
                    sb.Append(setting.Recipients);
                    sb.Append(";");
                }
            }
            return sb.ToString().TrimEnd(';');
        }

        public void AddSendMail(SendMail mail)
        {
            this.DataProvider.Insert(mail);
        }

        public MailIQCEc[] GetIQCMailEc(string IQCNO)
        {
            string sql = "SELECT A.*,B.DQMCODE,C.VENDORMCODE,B.CUSER IQCCUSER  FROM TBLASNIQCDETAILEC A,TBLASNIQC B,TBLASNDETAIL C WHERE A.IQCNO=B.IQCNO AND A.CARTONNO=C.CARTONNO AND A.IQCNO='" + IQCNO + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(MailIQCEc), new SQLCondition(sql));
            List<MailIQCEc> ecs = new List<MailIQCEc>();

            if (objs != null && objs.Length > 0)
            {
                foreach (MailIQCEc ec in objs)
                    ecs.Add(ec);
            }
            return ecs.ToArray();

        }

        public MailOQCEc[] GetOQCMailEc(string OQCNO)
        {
            string sql = @"SELECT A.*,D.VENDERITEMCODE,B.CUSER OQCCUSER  FROM TBLOQCDETAILEC A INNER JOIN TBLOQC B ON A.OQCNO =B.OQCNO LEFT JOIN TBLCartonInvDetailMaterial C ON A.CARTONNO=C.CARTONNO LEFT JOIN TBLPICKDETAIL D ON C.PICKNO=D.PICKNO AND C.PICKLINE=D.PICKLINE WHERE A.OQCNO='" + OQCNO + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(MailOQCEc), new SQLCondition(sql));
            List<MailOQCEc> ecs = new List<MailOQCEc>();

            if (objs != null && objs.Length > 0)
            {
                foreach (MailOQCEc ec in objs)
                    ecs.Add(ec);
            }
            return ecs.ToArray();
        }

        public StorageDetailValidity[] QueryStorageDetails(string storageCode, string validityCountStr, int inclusive, int exclusive)
        {

            List<StorageDetailValidity> validaties = new List<StorageDetailValidity>();
            string sql = @"
select t2.* from (
select t1.*,(case when spand>=0.5 and spand<0.75 then 1 when spand>=0.75 and spand<1 then 2 when spand>=1 then 3 when spand is null then 0 else 0 end
) ValidityCount  from (
SELECT A.*,B.VALIDITY,(case when b.VALIDITY>0 then ((sysdate - to_date( to_char(a.STORAGEAGEDATE),'yyyymmdd'))/b.VALIDITY) else 0 end) spand
  FROM TBLStorageDetail A  LEFT JOIN TBLMATERIAL B ON A.MCODE=B.MCODE WHERE 1=1 and STORAGEAGEDATE>0  
) t1
) t2 where 1=1

                     ";
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND t2.StorageCode='" + storageCode + "'";
            }

            if (!string.IsNullOrEmpty(validityCountStr))
            {
                int count = 0;
                if (int.TryParse(validityCountStr, out count))
                {
                    sql += " AND t2.ValidityCount=" + count;
                }
            }
            else
            {
                sql += " AND t2.ValidityCount <> 0";
            }


            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetailValidity), new PagerCondition(sql, "t2.storagecode", inclusive, exclusive));

            if (objs != null && objs.Length > 0)
            {
                foreach (StorageDetailValidity v in objs)
                {
                    validaties.Add(v);
                }

            }


            return validaties.ToArray();
        }



        public StorageDetailValidity[] QueryStorageDetails(string storageCode, string validityCountStr)
        {

            List<StorageDetailValidity> validaties = new List<StorageDetailValidity>();
            string sql = @"
select t2.* from (
select t1.*,(case when spand>=0.5 and spand<0.75 then 1 when spand>=0.75 and spand<1 then 2 when spand>=1 then 3 when spand is null then 0 else 0 end
) ValidityCount  from (
SELECT A.*,B.VALIDITY,(case when b.VALIDITY>0 then ((sysdate - to_date( to_char(a.STORAGEAGEDATE),'yyyymmdd'))/b.VALIDITY) else 0 end) spand
  FROM TBLStorageDetail A  LEFT JOIN TBLMATERIAL B ON A.MCODE=B.MCODE WHERE 1=1 and STORAGEAGEDATE>0  
) t1
) t2 where 1=1

                     ";
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND t2.StorageCode='" + storageCode + "'";
            }

            if (!string.IsNullOrEmpty(validityCountStr))
            {
                int count = 0;
                if (int.TryParse(validityCountStr, out count))
                {
                    sql += " AND t2.ValidityCount=" + count;
                }
            }
            else
            {
                sql += " AND t2.ValidityCount <> 0";
            }


            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetailValidity), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                foreach (StorageDetailValidity v in objs)
                {
                    validaties.Add(v);
                }

            }


            return validaties.ToArray();
        }


        public int QueryStorageDetailsCount(string storageCode, string validityCountStr)
        {


            string sql = @"
select count(*) from (
select t1.*,(case when spand>=0.5 and spand<0.75 then 1 when spand>=0.75 and spand<1 then 2 when spand>=1 then 3 when spand is null then 0 else 0 end
) ValidityCount  from (
SELECT A.*,B.VALIDITY,(case when b.VALIDITY>0 then ((sysdate - to_date( to_char(a.STORAGEAGEDATE),'yyyymmdd'))/b.VALIDITY) else 0 end) spand
  FROM TBLStorageDetail A  LEFT JOIN TBLMATERIAL B ON A.MCODE=B.MCODE WHERE 1=1 and STORAGEAGEDATE>0 
) t1
) t2 where 1=1

                     ";
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND t2.StorageCode='" + storageCode + "'";
            }

            if (!string.IsNullOrEmpty(validityCountStr))
            {
                int count = 0;
                if (int.TryParse(validityCountStr, out count))
                {
                    sql += " AND t2.ValidityCount=" + count;
                }
            }
            else
            {
                sql += " AND t2.ValidityCount <> 0";
            }





            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public StorageDetail[] QueryStorageAges(string mcode, string dqmCode, string storageCode)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(storageCode))
                sql = "SELECT A.MCODE,A.DQMCODE,A.QTY STORAGEQTY ,B.MCHSHORTDESC MDESC,B.MUOM UNIT,storagecode FROM (SELECT MCODE,DQMCODE,SUM(STORAGEQTY) QTY,storagecode FROM TBLStorageDetail GROUP BY MCODE,DQMCODE,storagecode) A,TBLMATERIAL B WHERE A.MCODE=B.MCODE ";
            else
                sql = "SELECT A.MCODE,A.DQMCODE,A.QTY STORAGEQTY,A.StorageCode,B.MCHSHORTDESC MDESC,B.MUOM UNIT FROM (SELECT MCODE,DQMCODE,SUM(STORAGEQTY) QTY,StorageCode FROM TBLStorageDetail GROUP BY MCODE,DQMCODE,StorageCode) A,TBLMATERIAL B WHERE A.MCODE=B.MCODE AND StorageCode='" + storageCode + "' ";

            if (!string.IsNullOrEmpty(mcode))
                sql += "AND a.MCODE like '%" + mcode + "%' ";
            if (!string.IsNullOrEmpty(dqmCode))
                sql += "AND a.DQMCODE like '%" + dqmCode + "%' ";
            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));
            List<StorageDetail> ss = new List<StorageDetail>();
            if (objs != null && objs.Length > 0)
            {
                foreach (StorageDetail s in objs)
                    ss.Add(s);
            }
            return ss.ToArray();
        }

        //public StorageDetail[] QueryStorageAges(string mcode, string dqmCode, string storageCode)
        //{
        //    string sql = string.Empty;
        //    if (string.IsNullOrEmpty(storageCode))
        //        sql = "SELECT A.MCODE,A.DQMCODE,A.QTY STORAGEQTY ,B.MCHSHORTDESC MDESC,B.MUOM UNIT FROM (SELECT MCODE,DQMCODE,SUM(STORAGEQTY) QTY FROM TBLStorageDetail GROUP BY MCODE,DQMCODE) A,TBLMATERIAL B WHERE A.MCODE=B.MCODE ";
        //    else
        //        sql = "SELECT A.MCODE,A.DQMCODE,A.QTY,A.StorageCode,B.MCHSHORTDESC,B.MUOM FROM (SELECT MCODE,DQMCODE,SUM(STORAGEQTY) QTY,StorageCode FROM TBLStorageDetail GROUP BY MCODE,DQMCODE,StorageCode) A,TBLMATERIAL B WHERE A.MCODE=B.MCODE AND StorageCode='" + storageCode + "' ";

        //    if (!string.IsNullOrEmpty(mcode))
        //        sql += "AND a.MCODE='%" + mcode + "%' ";
        //    if (!string.IsNullOrEmpty(dqmCode))
        //        sql += "AND a.DQMCODE='%" + dqmCode + "%' ";
        //    object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));
        //    List<StorageDetail> ss = new List<StorageDetail>();
        //    if (objs != null && objs.Length > 0)
        //    {
        //        foreach (StorageDetail s in objs)
        //            ss.Add(s);
        //    }
        //    return ss.ToArray();
        //}

        public StorageDetail[] QueryStorageAges(string mcode, string dqmCode, string storageCode, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(storageCode))
                sql = "SELECT A.MCODE,A.DQMCODE,A.QTY STORAGEQTY ,B.MCHSHORTDESC MDESC,B.MUOM UNIT,storagecode FROM (SELECT MCODE,DQMCODE,SUM(STORAGEQTY) QTY,storagecode FROM TBLStorageDetail GROUP BY MCODE,DQMCODE,storagecode) A,TBLMATERIAL B WHERE A.MCODE=B.MCODE ";
            else
                sql = "SELECT A.MCODE,A.DQMCODE,A.QTY STORAGEQTY,A.StorageCode,B.MCHSHORTDESC MDESC,B.MUOM UNIT FROM (SELECT MCODE,DQMCODE,SUM(STORAGEQTY) QTY,StorageCode FROM TBLStorageDetail GROUP BY MCODE,DQMCODE,StorageCode) A,TBLMATERIAL B WHERE A.MCODE=B.MCODE AND StorageCode='" + storageCode + "' ";

            if (!string.IsNullOrEmpty(mcode))
                sql += "AND a.MCODE like '%" + mcode + "%' ";
            if (!string.IsNullOrEmpty(dqmCode))
                sql += "AND a.DQMCODE like '%" + dqmCode + "%' ";



            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetail), new PagerCondition(sql, "A.MCODE", inclusive, exclusive));
            List<StorageDetail> ss = new List<StorageDetail>();
            if (objs != null && objs.Length > 0)
            {
                foreach (StorageDetail s in objs)
                    ss.Add(s);
            }
            return ss.ToArray();
        }


        public int QueryStorageAgesCount(string mcode, string dqmCode, string storageCode)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(storageCode))
                sql = "SELECT count(*) FROM (SELECT MCODE,DQMCODE,SUM(STORAGEQTY) QTY,storagecode FROM TBLStorageDetail GROUP BY MCODE,DQMCODE,storagecode) A,TBLMATERIAL B WHERE A.MCODE=B.MCODE ";
            else
                sql = "SELECT count(*) FROM (SELECT MCODE,DQMCODE,SUM(STORAGEQTY) QTY,StorageCode FROM TBLStorageDetail GROUP BY MCODE,DQMCODE,StorageCode) A,TBLMATERIAL B WHERE A.MCODE=B.MCODE AND StorageCode='" + storageCode + "' ";

            if (!string.IsNullOrEmpty(mcode))
                sql += "AND a.MCODE='%" + mcode + "%' ";
            if (!string.IsNullOrEmpty(dqmCode))
                sql += "AND a.DQMCODE='%" + dqmCode + "%' ";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public int QueryStorageLE3MonthAgesQty(int startDate, string MCode, string storageCode)
        {
            return QueryStorageLEMonthsAgesQty(startDate, MCode, -3, storageCode);
        }


        public int QueryStorageLE6MonthAgesQty(int startDate, string MCode, string storageCode)
        {
            return QueryStorageLEMonthsAgesQty(startDate, MCode, -6, storageCode);
        }

        public int QueryStorageLE12MonthAgesQty(int startDate, string MCode, string storageCode)
        {
            return QueryStorageLEMonthsAgesQty(startDate, MCode, -12, storageCode);
        }

        public int QueryStorageLE18MonthAgesQty(int startDate, string MCode, string storageCode)
        {
            return QueryStorageLEMonthsAgesQty(startDate, MCode, -18, storageCode);
        }

        public int QueryStorageLE24MonthAgesQty(int startDate, string MCode, string storageCode)
        {
            return QueryStorageLEMonthsAgesQty(startDate, MCode, -24, storageCode);
        }

        public int QueryStorageLE30MonthAgesQty(int startDate, string MCode, string storageCode)
        {
            return QueryStorageLEMonthsAgesQty(startDate, MCode, -30, storageCode);
        }

        public int QueryStorageLE36MonthAgesQty(int startDate, string MCode, string storageCode)
        {
            return QueryStorageLEMonthsAgesQty(startDate, MCode, -36, storageCode);
        }

        public int QueryStorageLEMonthsAgesQty(int startDate, string MCode, int priviousMonths, string storageCode)
        {

            BenQGuru.eMES.Domain.MOModel.Material m = GetMaterial(MCode);
            DateTime st = FormatHelper.ToDateTime(startDate);
            DateTime end = st.AddMonths(priviousMonths);
            int endInt = FormatHelper.TODateInt(end);

            string sql = string.Empty;
            if (m.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                sql = "SELECT SUM(B.STORAGEQTY) STORAGEQTY FROM (SELECT CARTONNO FROM TBLStorageDetailSN WHERE CDATE>=" + endInt + " AND CDATE <=" + startDate + " group by CARTONNO) A, TBLStorageDetail B WHERE A.CARTONNO=B.CARTONNO AND B.MCODE='" + MCode + "' and B.storageCODE='" + storageCode + "'";
            else
                sql = "SELECT SUM(B.STORAGEQTY) STORAGEQTY FROM  TBLStorageDetail B WHERE  B.MCODE='" + MCode + "' AND B.LastStorageAgeDate>=" + endInt + " AND B.LastStorageAgeDate <=" + startDate + " and B.storageCODE='" + storageCode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return ((StorageDetail)objs[0]).StorageQty;
            return 0;

        }

        public AsnIQC[] QueryAsnIQCFromCartonno(string cartonno)
        {

            string sql = "SELECT A.IQCNO FROM TBLASNIQCDETAIL A WHERE CARTONNO='" + cartonno + "' group by A.IQCNO";

            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQC), new SQLCondition(sql));

            List<AsnIQC> iqcs = new List<AsnIQC>();
            if (objs != null && objs.Length > 0)
            {
                foreach (AsnIQC iqc in objs)
                    iqcs.Add(iqc);
            }
            return iqcs.ToArray();

        }

        public StockCheckDetailCarton GetStockCheckDetailCarton1(string checkno, string cartonn)
        {
            string sql = string.Format(@" select a.* from TBLStockCheckDetailCarton a where CheckNo='{0}' and CARTONNO='{1}' ", checkno, cartonn);
            object[] list = this.DataProvider.CustomQuery(typeof(StockCheckDetailCarton), new SQLCondition(sql));
            if (list != null && list.Length > 0)
            {
                return list[0] as StockCheckDetailCarton;
            }
            return null;
        }

        public StockCheckDetail GetStockCheckDetail1mprove(string checkNo, string CARTONNO, string dqmCode)
        {
            string sql = @"SELECT A.* FROM TBLStockCheckDetail A WHERE A.CheckNo='" + checkNo + "'  AND A.CARTONNO='" + CARTONNO + "' AND A.DQMCODE='" + dqmCode + "'";


            object[] objs = this.DataProvider.CustomQuery(typeof(StockCheckDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return (StockCheckDetail)objs[0];
            else

                return null;

        }

        public StockCheckDetail GetStockCheckDetail1(string checkNo, string CARTONNO)
        {
            string sql = @"SELECT A.* FROM TBLStockCheckDetail A WHERE A.CheckNo='" + checkNo + "'  AND A.CARTONNO='" + CARTONNO + "'";


            object[] objs = this.DataProvider.CustomQuery(typeof(StockCheckDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return (StockCheckDetail)objs[0];
            else

                return null;

        }

        public string[] GetWaitStockCheckNo(string[] userGroups)
        {
            List<string> checkNos = new List<string>();
            string sql = @"SELECT A.CHECKNO FROM TBLSTOCKCHECK A WHERE A.status='WaitCheck'";
            sql += @"AND A.CHECKNO IN(SELECT DISTINCT CHECKNO FROM
 (SELECT CHECKNO FROM TBLSTOCKCHECK WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @"))) T  )";
            sql += " order by a.cdate,a.ctime desc";
            object[] objs = this.DataProvider.CustomQuery(typeof(StockCheck), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                foreach (StockCheck obj in objs)
                    checkNos.Add(obj.CheckNo);
            }
            return checkNos.ToArray();
        }

        public StockCheckDetail GetStockCheckDetailFrom(string checkNo, string CARTONNO)
        {
            string sql = @"SELECT A.* FROM TBLStockCheckDetail A WHERE A.CheckNo='" + checkNo + "'  AND A.CARTONNO='" + CARTONNO + "'";


            object[] objs = this.DataProvider.CustomQuery(typeof(StockCheckDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return (StockCheckDetail)objs[0];
            else

                return null;

        }


        public void UpdateStockCheckDetailCartonImprov(StockCheckDetailCarton car)
        {

            string sql = "UPDATE TBLStockCheckDetailCarton SET Locationcode='{0}',CheckQty={1},MDATE={2},MTIME={3},diffdesc='{4}',MUSER='{5}' ";
            sql += " where CARTONNO='{6}' and checkno='{7}'";
            sql = string.Format(sql,
                               car.LocationCode,
                               car.CheckQty,
                               car.MDATE,
                               car.MTIME,
                               car.DiffDesc,
                               car.MUSER,
                               car.CARTONNO,
                               car.CheckNo);

            // End Added

            //ishold 为 "-1" 表示 正在盘点,CS将不能采集

            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }


        public string GetCustMCodeForUB(string pickNo, string dqmCode)
        {

            string sql = "SELECT custmcode FROM TBLPICKDETAIL WHERE PICKNO='" + pickNo + "' AND DQMCODE='" + dqmCode + "'";


            object[] objs = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return ((PickDetail)objs[0]).CustMCode;
            return string.Empty;


        }


        public void DeleteAllStockDetail(string checkNo)
        {
            string sql = string.Format(@"delete from TBLStockCheckDetail where checkno='{0}'", checkNo.ToUpper());
            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }

        public void UpdateStockCheckDetail(string checkNo, string dqmcode, string storageCode, string diffDesc)
        {

            string sql = "";
            sql += " UPDATE TBLStockCheckDetail SET DIFFDESC = '" + diffDesc + "' ";
            sql += " where checkNo ='" + checkNo + "' AND STORAGECODE='" + storageCode + "' and DQMCODE='" + dqmcode + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public UserGroup2User[] GetUserFromUserGroup(string group)
        {
            string sql = " SELECT * FROM TBLUSERGROUP2USER WHERE USERGROUPCODE='" + group + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(UserGroup2User), new SQLCondition(sql));

            List<UserGroup2User> user2Groups = new List<UserGroup2User>();
            if (objs != null && objs.Length > 0)
            {
                foreach (UserGroup2User user2Group in objs)
                    user2Groups.Add(user2Group);
            }
            return user2Groups.ToArray();
        }


        public Pickdetailmaterialsn[] GetPickedSNFromDQMCode(string dqmCode, string pickNo)
        {
            string sql = "SELECT A.SN,A.PICKNO,A.PICKLINE FROM TBLPICKDETAILMATERIALSN A,TBLPICKDETAIL B WHERE A.PICKNO=B.PICKNO AND A.PICKLINE=B.PICKLINE AND B.DQMCODE='" + dqmCode + "' AND B.PICKNO='" + pickNo + "' ";
            object[] objs = this.DataProvider.CustomQuery(typeof(Pickdetailmaterialsn), new SQLCondition(sql));
            List<Pickdetailmaterialsn> sns = new List<Pickdetailmaterialsn>();
            if (objs != null && objs.Length > 0)
            {
                foreach (Pickdetailmaterialsn sn in objs)
                    sns.Add(sn);
            }
            return sns.ToArray();
        }


        public CARTONINVOICES GetCartonInvoicesFromPickNo(string pickNo)
        {
            string sql = "SELECT A.* FROM TBLCartonInvoices A WHERE A.PICKNO='" + pickNo + "' ";
            object[] objs = this.DataProvider.CustomQuery(typeof(CARTONINVOICES), new SQLCondition(sql));
            List<CARTONINVOICES> sns = new List<CARTONINVOICES>();
            if (objs != null && objs.Length > 0)
            {
                return ((CARTONINVOICES)objs[0]);
            }
            return null;
        }



        public int GetPickDetailMarterialStorageAgeDate(string pickNo, string dqmCode)
        {
            string sql = "SELECT A.* FROM TBLPICKDetailMaterial A WHERE A.PICKNO='" + pickNo + "' AND A.DQMCODE='" + dqmCode + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            List<Pickdetailmaterial> sns = new List<Pickdetailmaterial>();
            if (objs != null && objs.Length > 0)
            {
                return ((Pickdetailmaterial)objs[0]).StorageageDate;
            }
            return 0;
        }

        public void DeleteStockCheck(StockCheck ch)
        {
            this.DataProvider.Delete(ch);
        }


        public void UpdatePickDetailSQty(string pickNo, string pickLine, int sQty)
        {
            string sql = "update TBLPICKDETAIL set SQTY=SQTY+" + sQty + "WHERE PICKNO='" + pickNo + "' AND PICKLINE='" + pickLine + "'";



            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        public int QueryPickMaterialTotal(string pickNo, string pickLine, string dqMCode)
        {
            string sql = "SELECT nvl(SUM(QTY),0) QTY FROM TBLPICKDetailMaterial WHERE PICKNO='" + pickNo + "' AND PICKLINE='" + pickLine + "' AND DQMCODE='" + dqMCode + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public StorloctransDetailCarton GetStorLocTransDetailCarton(string TransNo, string fromCartonno)
        {
            string sql = string.Format(@"SELECT * FROM TBLSTORLOCTRANSDETAILCARTON WHERE TransNo='{0}'  and fromCartonno = '{1}' ", TransNo, fromCartonno);
            object[] objs = this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                return (StorloctransDetailCarton)objs[0];
            }
            return null;
        }


        public Asndetailsn[] GetAsnDetailSN(string stno, string stline)
        {
            List<Asndetailsn> sns = new List<Asndetailsn>();
            string sql = @"select * from TBLASNDETAILSN where stno='" + stno + "' and stline='" + stline + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                foreach (Asndetailsn sn in objs)
                {
                    sns.Add(sn);
                }
            }
            return sns.ToArray();
        }

        public void UpdateStockCheckDetailIfTypeIsCheckALL(string checkNo, string dqmCode, int checkQty, int date, int time)
        {
            string sql = "update TBLStockCheckDetail set checkqty = " + checkQty + ",mdate=" + date + ",mtime=" + time + " where checkNo = '" + checkNo + "' and dqmCode = '" + dqmCode + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public string GetRejectDESC(string value)
        {
            string sql = "select a.* from tblsysparam a where a.paramgroupcode='REJECTRESULT' AND PARAMCODE='" + value + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return ((Parameter)objs[0]).ParameterDescription;
            return string.Empty;
        }


        public ReceiveRejectSummary[] GetReceiveRejectSummarys(int dateBegin, int dateEnd, string storageCode, string storageType, int inclusive, int exclusive)
        {
            List<ReceiveRejectSummary> rs = new List<ReceiveRejectSummary>();
            string sql = "select a.STNO,a.INVNO,case when a.sttype = 'POR' then 'PO入库' when a.sttype = 'DNR' then '退货入库' when a.sttype = 'UB' then '调拨入库' when a.sttype = 'JCR' then '检测返工入库' when a.sttype = 'BLR' then '不良品入库' when a.sttype = 'CAR' then 'CLAIM入库' when a.sttype = 'YFR' then '研发入库' when a.sttype = 'PD' then '盘点' when a.sttype = 'SCTR' then '生产退料' when a.sttype = 'PGIR' then 'PGI退料' end as sttype,a.VENDORCODE,c.VENDORNAME,b.MDATE,d.PARAMDESC,a.STORAGECODE from tblASN a FULL JOIN TBLVENDOR c on a.VENDORCODE = c.VENDORCODE FULL JOIN TBLINVINOUTTRANS b on a.stno = b.TRANSNO FULL Join tblsysparam d on a.InitReceiveDesc = d.PARAMCODE where a.InitRejectQty>0 and b.PROCESSTYPE = 'Receive'";

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND a.STORAGECODE='" + storageCode + "' ";

            }

            if (!string.IsNullOrEmpty(storageType))
            {

                sql += " AND STTYPE='" + storageType + "' ";
            }
            if (dateBegin > 0)
            {
                sql += " AND b.MDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND b.MDATE<=" + dateEnd;
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(ReceiveRejectSummary), new PagerCondition(sql, "a.STNO", inclusive, exclusive));


            if (objs != null && objs.Length > 0)
            {
                foreach (ReceiveRejectSummary s in objs)
                {
                    rs.Add(s);
                }
            }
            return rs.ToArray();
        }


        public ReceiveRejectSummary[] GetReceiveRejectSummarys(int dateBegin, int dateEnd, string storageCode, string storageType)
        {
            List<ReceiveRejectSummary> rs = new List<ReceiveRejectSummary>();
            string sql = "select a.STNO,a.INVNO,case when a.sttype = 'POR' then 'PO入库' when a.sttype = 'DNR' then '退货入库' when a.sttype = 'UB' then '调拨入库' when a.sttype = 'JCR' then '检测返工入库' when a.sttype = 'BLR' then '不良品入库' when a.sttype = 'CAR' then 'CLAIM入库' when a.sttype = 'YFR' then '研发入库' when a.sttype = 'PD' then '盘点' when a.sttype = 'SCTR' then '生产退料' when a.sttype = 'PGIR' then 'PGI退料' end as sttype,a.VENDORCODE,c.VENDORNAME,b.MDATE,d.PARAMDESC,a.STORAGECODE from tblASN a FULL JOIN TBLVENDOR c on a.VENDORCODE = c.VENDORCODE FULL JOIN TBLINVINOUTTRANS b on a.stno = b.TRANSNO FULL Join tblsysparam d on a.InitReceiveDesc = d.PARAMCODE where a.InitRejectQty>0 and b.PROCESSTYPE = 'Receive'";

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND a.STORAGECODE='" + storageCode + "' ";

            }

            if (!string.IsNullOrEmpty(storageType))
            {

                sql += " AND STTYPE='" + storageType + "' ";
            }
            if (dateBegin > 0)
            {
                sql += " AND b.MDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND b.MDATE<=" + dateEnd;
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(ReceiveRejectSummary), new SQLCondition(sql));


            if (objs != null && objs.Length > 0)
            {
                foreach (ReceiveRejectSummary s in objs)
                {
                    rs.Add(s);
                }
            }
            return rs.ToArray();
        }

        public int GetReceiveRejectSummarysCount(int dateBegin, int dateEnd, string storageCode, string storageType)
        {
            List<ReceiveRejectSummary> rs = new List<ReceiveRejectSummary>();
            string sql = "select count(*) from tblASN a FULL JOIN TBLVENDOR c on a.VENDORCODE = c.VENDORCODE FULL JOIN TBLINVINOUTTRANS b on a.stno = b.TRANSNO FULL Join tblsysparam d on a.InitReceiveDesc = d.PARAMCODE where a.InitRejectQty>0 and b.PROCESSTYPE = 'Receive'";

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += " AND a.STORAGECODE='" + storageCode + "' ";

            }

            if (!string.IsNullOrEmpty(storageType))
            {

                sql += " AND STTYPE='" + storageType + "' ";
            }
            if (dateBegin > 0)
            {
                sql += " AND b.MDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND b.MDATE<=" + dateEnd;
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));


        }



        public ReceiveRejectSummaryBuyer[] GetReceiveRejectSummarysBuyer(int dateBegin, int dateEnd, string dqmCode, string mdesc, string vendorCode, string vendorName, int inclusive, int exclusive)
        {
            List<ReceiveRejectSummaryBuyer> rs = new List<ReceiveRejectSummaryBuyer>();
            string sql = "select t.* from (select a.DQMCODE,a.MDESC,b.VENDORCODE,c.VENDORNAME,b.CDATE,a.qty,(a.QTY-a.RECEIVEQTY)as n,ROUND((a.QTY-a.RECEIVEQTY)/a.qty,2)*100||'%' as l from tblasndetail a left join tblasn b on a.stno = b.STNO left join TBLVENDOR c on b.VENDORCODE = c.VENDORCODE) t where 1=1";

            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND t.DQMCODE = '" + dqmCode + "'";

            }

            if (!string.IsNullOrEmpty(mdesc))
            {

                sql += " AND t.MDESC like '%" + mdesc + "%'";
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND t.VENDORCODE = '" + vendorCode + "'";
            }
            if (!string.IsNullOrEmpty(vendorName))
            {

                sql += " AND t.VENDORNAME like '%" + vendorName + "%'";
            }

            if (dateBegin > 0)
            {
                sql += " AND t.CDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND t.CDATE<=" + dateEnd;
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(ReceiveRejectSummaryBuyer), new PagerCondition(sql, "T.DQMCODE", inclusive, exclusive));


            if (objs != null && objs.Length > 0)
            {
                foreach (ReceiveRejectSummaryBuyer s in objs)
                {
                    rs.Add(s);
                }
            }
            return rs.ToArray();


        }



        public int GetReceiveRejectSummarysBuyerCount(int dateBegin, int dateEnd, string dqmCode, string mdesc, string vendorCode, string vendorName)
        {
            string sql = "select count(*) from (select a.DQMCODE,a.MDESC,b.VENDORCODE,c.VENDORNAME,b.CDATE,a.qty,(a.QTY-a.RECEIVEQTY)as n,ROUND((a.QTY-a.RECEIVEQTY)/a.qty,2)*100||'%' as l from tblasndetail a left join tblasn b on a.stno = b.STNO left join TBLVENDOR c on b.VENDORCODE = c.VENDORCODE) t where 1=1";

            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND t.DQMCODE = '" + dqmCode + "'";

            }

            if (!string.IsNullOrEmpty(mdesc))
            {

                sql += " AND t.MDESC like '%" + mdesc + "%'";
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND t.VENDORCODE = '" + vendorCode + "'";
            }
            if (!string.IsNullOrEmpty(vendorName))
            {

                sql += " AND t.VENDORNAME like '%" + vendorName + "%'";
            }

            if (dateBegin > 0)
            {
                sql += " AND t.CDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND t.CDATE<=" + dateEnd;
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        public ReceiveRejectSummaryBuyer[] GetReceiveRejectSummarysBuyer(int dateBegin, int dateEnd, string dqmCode, string mdesc, string vendorCode, string vendorName)
        {
            List<ReceiveRejectSummaryBuyer> rs = new List<ReceiveRejectSummaryBuyer>();
            string sql = "select t.* from (select a.DQMCODE,a.MDESC,b.VENDORCODE,c.VENDORNAME,b.CDATE,a.qty,(a.QTY-a.RECEIVEQTY)as n,ROUND((a.QTY-a.RECEIVEQTY)/a.qty,2)*100||'%' as l from tblasndetail a left join tblasn b on a.stno = b.STNO left join TBLVENDOR c on b.VENDORCODE = c.VENDORCODE) t where 1=1";

            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND t.DQMCODE = '" + dqmCode + "'";

            }

            if (!string.IsNullOrEmpty(mdesc))
            {

                sql += " AND t.MDESC like '%" + mdesc + "%'";
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND t.VENDORCODE = '" + vendorCode + "'";
            }
            if (!string.IsNullOrEmpty(vendorName))
            {

                sql += " AND t.VENDORNAME like '%" + vendorName + "%'";
            }

            if (dateBegin > 0)
            {
                sql += " AND t.CDATE>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND t.CDATE<=" + dateEnd;
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(ReceiveRejectSummaryBuyer), new SQLCondition(sql));


            if (objs != null && objs.Length > 0)
            {
                foreach (ReceiveRejectSummaryBuyer s in objs)
                {
                    rs.Add(s);
                }
            }
            return rs.ToArray();


        }


        public IQCExecSummaryBuyer[] GetIQCExecBuyerSummary(int dateBegin, int dateEnd, string dqmCode, string mdesc, string vendorCode, string vendorName)
        {
            List<IQCExecSummaryBuyer> rs = new List<IQCExecSummaryBuyer>();
            string sql = @"SELECT T.Sdate,T.Edate,T.stno,T.DQMCODE,T.MDESC,T.VENDORCODE,T.VENDORNAME,T.stline,T.qty,T.NGQTY,T.NGPER,T.ReformQTY,T.GiveQTY,T.ReturnQTY,T.AcceptQTY,T.AQLLEVEL,T.samplesize  FROM(
 select e.CDATE as Sdate,e.CDATE as Edate,a.stno,b.DQMCODE,b.MDESC,e.VENDORCODE,f.VENDORNAME,a.stline,b.qty,a.ReturnQTY,a.NGQTY,round(a.NGQTY/b.qty,2)*100||'%' as NGPER,a.ReformQTY,a.GiveQTY,a.AcceptQTY,c.AQLLEVEL,d.samplesize from 
 (select a.iqcno,a.stno,a.stline,sum(a.ReturnQTY) as ReturnQTY,sum(a.ReformQTY) as ReformQTY,sum(a.GiveQTY) as GiveQTY,sum(a.AcceptQTY) as AcceptQTY,sum(a.NGQTY) as NGQTY from tblasniqcdetail a group by a.iqcno,a.stno,a.STLINE) a LEFT join 
 tblasndetail b  on a.stno = b.stno and a.stline = b.stline left join 
 tblasniqc c on a.stno = c.stno and a.iqcno = c.IQCNO LEFT join 
 tblaql d on c.AQLLEVEL = d.AQLLEVEL left join 
 tblasn e on a.stno = e.stno left join 
 TBLVENDOR f on e.VENDORCODE = f.VENDORCODE WHERE 1=1 
 ) T where 1=1";

            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND T.DQMCODE = '" + dqmCode + "'";

            }

            if (!string.IsNullOrEmpty(mdesc))
            {

                sql += " AND T.MDESC like '%" + mdesc + "%'";
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND T.VENDORCODE = '" + vendorCode + "'";
            }
            if (!string.IsNullOrEmpty(vendorName))
            {

                sql += " AND T.VENDORNAME like '%" + vendorName + "%'";
            }

            if (dateBegin > 0)
            {
                sql += " AND T.Sdate>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND T.Sdate<=" + dateEnd;
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(IQCExecSummaryBuyer), new SQLCondition(sql));


            if (objs != null && objs.Length > 0)
            {
                foreach (IQCExecSummaryBuyer s in objs)
                {
                    rs.Add(s);
                }
            }
            return rs.ToArray();
        }


        public int GetIQCExecBuyerSummaryCount(int dateBegin, int dateEnd, string dqmCode, string mdesc, string vendorCode, string vendorName)
        {
            string sql = @"SELECT count(*)  FROM(
 select e.CDATE as Sdate,e.CDATE as Edate,a.stno,b.DQMCODE,b.MDESC,e.VENDORCODE,f.VENDORNAME,a.stline,b.qty,a.ReturnQTY,a.NGQTY,round(a.NGQTY/b.qty,2)*100||'%' as NGPER,a.ReformQTY,a.GiveQTY,a.AcceptQTY,c.AQLLEVEL,d.samplesize from 
 (select a.iqcno,a.stno,a.stline,sum(a.ReturnQTY) as ReturnQTY,sum(a.ReformQTY) as ReformQTY,sum(a.GiveQTY) as GiveQTY,sum(a.AcceptQTY) as AcceptQTY,sum(a.NGQTY) as NGQTY from tblasniqcdetail a group by a.iqcno,a.stno,a.STLINE) a LEFT join 
 tblasndetail b  on a.stno = b.stno and a.stline = b.stline left join 
 tblasniqc c on a.stno = c.stno and a.iqcno = c.IQCNO LEFT join 
 tblaql d on c.AQLLEVEL = d.AQLLEVEL left join 
 tblasn e on a.stno = e.stno left join 
 TBLVENDOR f on e.VENDORCODE = f.VENDORCODE WHERE 1=1 
 ) T where 1=1";

            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND T.DQMCODE = '" + dqmCode + "'";

            }

            if (!string.IsNullOrEmpty(mdesc))
            {

                sql += " AND T.MDESC like '%" + mdesc + "%'";
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND T.VENDORCODE = '" + vendorCode + "'";
            }
            if (!string.IsNullOrEmpty(vendorName))
            {

                sql += " AND T.VENDORNAME like '%" + vendorName + "%'";
            }

            if (dateBegin > 0)
            {
                sql += " AND T.Sdate>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND T.Sdate<=" + dateEnd;
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public IQCExecSummaryBuyer[] GetIQCExecBuyerSummary(int dateBegin, int dateEnd, string dqmCode, string mdesc, string vendorCode, string vendorName, int inclusive, int exclusive)
        {
            List<IQCExecSummaryBuyer> rs = new List<IQCExecSummaryBuyer>();
            string sql = @" SELECT T.Sdate,T.Edate,T.stno,T.DQMCODE,T.MDESC,T.VENDORCODE,T.VENDORNAME,T.stline,T.qty,T.NGQTY,T.NGPER,T.ReformQTY,T.GiveQTY,T.ReturnQTY,T.AcceptQTY,T.AQLLEVEL,T.samplesize  FROM(
 select e.CDATE as Sdate,e.CDATE as Edate,a.stno,b.DQMCODE,b.MDESC,e.VENDORCODE,f.VENDORNAME,a.stline,b.qty,a.ReturnQTY,a.NGQTY,round(a.NGQTY/b.qty,2)*100||'%' as NGPER,a.ReformQTY,a.GiveQTY,a.AcceptQTY,c.AQLLEVEL,d.samplesize from 
 (select a.iqcno,a.stno,a.stline,sum(a.ReturnQTY) as ReturnQTY,sum(a.ReformQTY) as ReformQTY,sum(a.GiveQTY) as GiveQTY,sum(a.AcceptQTY) as AcceptQTY,sum(a.NGQTY) as NGQTY from tblasniqcdetail a group by a.iqcno,a.stno,a.STLINE) a LEFT join 
 tblasndetail b  on a.stno = b.stno and a.stline = b.stline left join 
 tblasniqc c on a.stno = c.stno and a.iqcno = c.IQCNO LEFT join 
 tblaql d on c.AQLLEVEL = d.AQLLEVEL left join 
 tblasn e on a.stno = e.stno left join 
 TBLVENDOR f on e.VENDORCODE = f.VENDORCODE WHERE 1=1 
 ) T where 1=1
  ";

            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += " AND T.DQMCODE = '" + dqmCode + "'";

            }

            if (!string.IsNullOrEmpty(mdesc))
            {

                sql += " AND T.MDESC like '%" + mdesc + "%'";
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {

                sql += " AND T.VENDORCODE = '" + vendorCode + "'";
            }
            if (!string.IsNullOrEmpty(vendorName))
            {

                sql += " AND T.VENDORNAME like '%" + vendorName + "%'";
            }

            if (dateBegin > 0)
            {
                sql += " AND T.Sdate>=" + dateBegin;
            }
            if (dateEnd > 0)
            {
                sql += " AND T.Sdate<=" + dateEnd;
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(IQCExecSummaryBuyer), new PagerCondition(sql, "T.DQMCODE", inclusive, exclusive));


            if (objs != null && objs.Length > 0)
            {
                foreach (IQCExecSummaryBuyer s in objs)
                {
                    rs.Add(s);
                }
            }
            return rs.ToArray();


        }

        public int GetPackCount(string pickNo, string ProcessType)
        {

            string sql = @"SELECT COUNT(*) FROM TBLINVINOUTTRANS WHERE TRANSNO='" + pickNo + "' AND PROCESSTYPE='" + ProcessType + "'";



            return this.DataProvider.GetCount(new SQLCondition(sql));





        }



        public decimal InstorageAverPeriod(string stroageCode, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT T1.TRANSNO, T1.BeginDate,T2.ENDDATE FROM TBLASN T,
                            (select transno,MAX(MDATE||'-'||MTIME) BeginDate  FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='RECEIVEBEGIN' {0} GROUP BY TRANSNO) T1，
                            (select transno,MAX(MDATE||'-'||MTIME) ENDDATE  FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='INSTORAEE' {0} GROUP BY TRANSNO) T2 WHERE T.STNO=T1.TRANSNO AND T.STNO=T2.TRANSNO ";
            if (!string.IsNullOrEmpty(stroageCode))
            {
                sql += " AND T.STORAGECODE='" + stroageCode + "' ";
            }

            string dateFilter = string.Empty;
            if (dateBegin > 0)
                dateFilter += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                dateFilter += " AND  MDATE <=" + dateEnd;

            sql = string.Format(sql, dateFilter);
            object[] oo = this.DataProvider.CustomQuery(typeof(DateTimeRange), new SQLCondition(sql));




            List<decimal> ls = new List<decimal>();

            if (oo != null && oo.Length > 0)
            {

                foreach (DateTimeRange d in oo)
                {
                    string[] maxStrs = d.EndDate.Split('-');

                    string maxDateStr = maxStrs[0];
                    string maxTimeStr = maxStrs[1];

                    string[] minStrs = d.BeginDate.Split('-');
                    string minxDateStr = minStrs[0];
                    string minTimeStr = minStrs[1];


                    ls.Add(
                        Totalday(int.Parse(maxDateStr),
                                 int.Parse(maxTimeStr),
                                 int.Parse(minxDateStr),
                                 int.Parse(minTimeStr)));
                }

            }



            decimal sum = 0;
            foreach (decimal one in ls)
            {
                sum += one;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }



        public decimal InstorageAverPeriod1(string stroageCode, int dateBegin, int dateEnd)
        {
            string sql = @"SELECT T1.TRANSNO, T1.BeginDate,T2.ENDDATE FROM TBLASN T,
                            (select transno,min(MDATE||'-'||MTIME) BeginDate  FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='INSTORAEE' {0} GROUP BY TRANSNO) T1，
                            (select transno,MAX(MDATE||'-'||MTIME) ENDDATE  FROM TBLINVINOUTTRANS WHERE TransType='IN' AND PROCESSTYPE='INSTORAEE' {0} GROUP BY TRANSNO) T2 WHERE T.STNO=T1.TRANSNO AND T.STNO=T2.TRANSNO ";
            if (!string.IsNullOrEmpty(stroageCode))
            {
                sql += " AND T.STORAGECODE='" + stroageCode + "' ";
            }

            string dateFilter = string.Empty;
            if (dateBegin > 0)
                dateFilter += " AND  MDATE >=" + dateBegin;
            if (dateEnd > 0)
                dateFilter += " AND  MDATE <=" + dateEnd;

            sql = string.Format(sql, dateFilter);
            object[] oo = this.DataProvider.CustomQuery(typeof(DateTimeRange), new SQLCondition(sql));




            List<decimal> ls = new List<decimal>();

            if (oo != null && oo.Length > 0)
            {

                foreach (DateTimeRange d in oo)
                {
                    string[] maxStrs = d.EndDate.Split('-');

                    string maxDateStr = maxStrs[0];
                    string maxTimeStr = maxStrs[1];

                    string[] minStrs = d.BeginDate.Split('-');
                    string minxDateStr = minStrs[0];
                    string minTimeStr = minStrs[1];


                    ls.Add(
                        Totalday(int.Parse(maxDateStr),
                                 int.Parse(maxTimeStr),
                                 int.Parse(minxDateStr),
                                 int.Parse(minTimeStr)));
                }

            }



            decimal sum = 0;
            foreach (decimal one in ls)
            {
                sum += one;

            }
            if (ls.Count > 0)
                return Math.Round(sum / ls.Count, 2);
            return 0;
        }

        public Pickdetailmaterial[] GetPickdetailMaterialArry(string pickno, string dqmCode)
        {
            string sql = "select * from tblpickdetailmaterial where pickno='" + pickno + "' AND DQMCODE='" + dqmCode + "'";

            object[] oo = this.DataProvider.CustomQuery(typeof(Pickdetailmaterial), new SQLCondition(sql));
            List<Pickdetailmaterial> list = new List<Pickdetailmaterial>();
            foreach (Pickdetailmaterial o in oo)
            {
                list.Add((Pickdetailmaterial)o);

            }

            return list.ToArray();
        }


        public CartonInvDetailMaterial[] GetCartonInvDetailMaterialByCa(string carinv)
        {
            List<CartonInvDetailMaterial> ms = new List<CartonInvDetailMaterial>();
            string sql = "select a.* from TBLCartonInvDetailMaterial a where a.carinvno='" + carinv + "'";
            object[] os = this.DataProvider.CustomQuery(typeof(CartonInvDetailMaterial), new SQLCondition(sql));
            if (os != null && os.Length > 0)
            {
                foreach (CartonInvDetailMaterial c in os)
                    ms.Add(c);
            }
            return ms.ToArray();
        }


        public string GetVendorName(string vendorCode)
        {
            string sql = "select a.* from TBLVENDOR a where a.VENDORCODE='" + vendorCode + "'";
            object[] os = this.DataProvider.CustomQuery(typeof(Vendor), new SQLCondition(sql));

            if (os != null && os.Length > 0)
                return ((Vendor)(os[0])).VendorName;
            return string.Empty;
        }


        public decimal GetStorloctransDetailDqmCodeQtySum(string transNo, string dqmcode)
        {
            string sql = "select a.* from TBLSTORLOCTRANSDETAIL a where a.TRANSNO='" + transNo + "' and a.DQMCODE='" + dqmcode + "'";


            object[] os = this.DataProvider.CustomQuery(typeof(StorloctransDetail), new SQLCondition(sql));
            decimal sum = 0;
            if (os != null && os.Length > 0)
            {
                foreach (StorloctransDetail s in os)
                    sum += s.Qty;
            }
            return sum;
        }


        public decimal GetStorloctransDetailCartonDqmCodeQtySum(string transNo, string dqmcode)
        {
            string sql = "select a.* from TBLSTORLOCTRANSDETAILCARTON a where a.TRANSNO='" + transNo + "' and a.DQMCODE='" + dqmcode + "'";


            object[] os = this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));
            decimal sum = 0;
            if (os != null && os.Length > 0)
            {
                foreach (StorloctransDetailCarton s in os)
                    sum += s.Qty;
            }
            return sum;
        }


        public StorloctransDetailCarton[] GetStorloctransdetailcartons(string transNo)
        {

            List<StorloctransDetailCarton> cartons = new List<StorloctransDetailCarton>();
            string sql = @"SELECT a.* FROM  tblstorloctransdetailcarton a where 1=1 and a.TRANSNO = '" + transNo + "'";


            object[] objs = this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                foreach (StorloctransDetailCarton car in objs)
                    cartons.Add(car);
            }
            return cartons.ToArray();
        }

        public void DeleteAllCartonInvDetailMaterial(string pickno)
        {
            string sql = string.Format(@"delete from TBLCartonInvDetailMaterial where PICKNO='{0}'", pickno);

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        public int GetPackageMaterialCartonnos(string pickno)
        {
            string sql = string.Format(@"select count(*) from TBLCARTONINVDETAILMATERIAL WHERE PICKNO='{0}'", pickno);

            int count = this.DataProvider.GetCount(new SQLCondition(sql));


            return count;
        }

        public StorloctransDetailCarton[] GetStorloctransdetailcarton1s(string transNo, string fromCartonno)
        {

            List<StorloctransDetailCarton> cartons = new List<StorloctransDetailCarton>();
            string sql = @"SELECT a.* FROM  tblstorloctransdetailcarton a where 1=1 and a.TRANSNO = '" + transNo + "' and fromcartonno='" + fromCartonno + "'";


            object[] objs = this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                foreach (StorloctransDetailCarton car in objs)
                    cartons.Add(car);
            }
            return cartons.ToArray();
        }


        public string[] GetStorloctransdetailcartonMCODE(string TransNo)
        {
            string sql = @"SELECT MCODE FROM  tblstorloctransdetailcarton where 1=1 and TRANSNO = '{1}' group by Mcode";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));
            List<string> mcodes = new List<string>();
            if (objs != null && objs.Length > 0)
            {
                foreach (StorloctransDetailCarton s in objs)
                    mcodes.Add(s.MCode);

            }
            return mcodes.ToArray();
        }
        //public int GetStorloctransdetailcartons(string transNo)
        //{

        //    List<StorloctransDetailCarton> cartons = new List<StorloctransDetailCarton>();
        //    string sql = @"SELECT a.* FROM  tblstorloctransdetailcarton a where 1=1 and a.TRANSNO = '" + transNo + "'";


        //    object[] objs = this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));

        //    if (objs != null && objs.Length > 0)
        //    {
        //        foreach (StorloctransDetailCarton car in objs)
        //            cartons.Add(car);
        //    }
        //    return cartons.ToArray();
        //}

    }
    public class DateTimeRange : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public DateTimeRange()
        {

        }

        [FieldMapAttribute("TRANSNO", typeof(string), 22, false)]
        public string TRANSNO;
        [FieldMapAttribute("BeginDate", typeof(string), 22, false)]
        public string BeginDate;

        [FieldMapAttribute("ENDDATE", typeof(string), 22, false)]
        public string EndDate;

    }

    public class IQCExecSummaryBuyer : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public IQCExecSummaryBuyer()
        {
        }

        [FieldMapAttribute("DQMCODE", typeof(string), 22, false)]
        public string DQMCODE;
        [FieldMapAttribute("MDESC", typeof(string), 40, false)]
        public string MDESC;

        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VENDORCODE;

        [FieldMapAttribute("VENDORNAME", typeof(string), 40, true)]
        public string VENDORNAME;

        [FieldMapAttribute("QTY", typeof(int), 22, true)]
        public int QTY;

        [FieldMapAttribute("SAMPLESIZE", typeof(int), 22, true)]
        public int SAMPLESIZE;
        [FieldMapAttribute("NGQTY", typeof(int), 22, true)]
        public int NGQTY;
        [FieldMapAttribute("NGPER", typeof(string), 22, true)]
        public string NGPER;
        [FieldMapAttribute("ReturnQTY", typeof(int), 22, true)]
        public int ReturnQTY;

        [FieldMapAttribute("ReformQTY", typeof(int), 22, true)]
        public int ReformQTY;
        [FieldMapAttribute("GiveQTY", typeof(int), 22, true)]
        public int GiveQTY;
        [FieldMapAttribute("AcceptQTY", typeof(int), 22, true)]
        public int AcceptQTY;


    }


    public class ReceiveRejectSummaryBuyer : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ReceiveRejectSummaryBuyer()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 22, false)]
        public string DQMCODE;


        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 40, false)]
        public string MDESC;



        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VENDORCODE;

        ///<summary>
        ///检测返工申请人
        ///</summary>
        [FieldMapAttribute("VENDORNAME", typeof(string), 40, true)]
        public string VENDORNAME;





        [FieldMapAttribute("CDATE", typeof(int), 22, true)]
        public int CDATE;

        [FieldMapAttribute("QTY", typeof(int), 22, true)]
        public int QTY;

        [FieldMapAttribute("N", typeof(int), 22, true)]
        public int N;


        [FieldMapAttribute("L", typeof(string), 40, false)]
        public string L;




    }

    public class ReceiveRejectSummary : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public ReceiveRejectSummary()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 22, false)]
        public string STNO;


        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string INVNO;



        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("sttype", typeof(string), 40, false)]
        public string sttype;

        ///<summary>
        ///检测返工申请人
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VENDORCODE;

        ///<summary>
        ///工厂
        ///</summary>
        [FieldMapAttribute("VENDORNAME", typeof(string), 40, false)]
        public string VENDORNAME;


        [FieldMapAttribute("MDATE", typeof(int), 22, true)]
        public int MDATE;



        [FieldMapAttribute("PARAMDESC", typeof(string), 40, false)]
        public string PARAMDESC;

        ///<summary>
        ///供应商批次号
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string STORAGECODE;


    }


    [Serializable, TableMap("TBLUSERGROUP2USER", "USERCODE,USERGROUPCODE")]
    public class UserGroup2User : DomainObject
    {

        [FieldMapAttribute("USERCODE", typeof(string), 40, false)]
        public string USERCODE;


        [FieldMapAttribute("USERGROUPCODE", typeof(string), 40, false)]
        public string MOCode;


    }

    public class StorageDetailValidity : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorageDetailValidity()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///检测返工申请人
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 40, true)]
        public string ReworkApplyUser;

        ///<summary>
        ///工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FacCode;

        ///<summary>
        ///最后一次入库时间
        ///</summary>
        [FieldMapAttribute("LASTSTORAGEAGEDATE", typeof(int), 22, true)]
        public int LastStorageAgeDate;

        ///<summary>
        ///第一次入库时间
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(int), 22, true)]
        public int StorageAgeDate;

        ///<summary>
        ///生产日期
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int ProductionDate;

        ///<summary>
        ///鼎桥批次号
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string Lotno;

        ///<summary>
        ///供应商批次号
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string SupplierLotNo;

        ///<summary>
        ///冻结数量
        ///</summary>
        [FieldMapAttribute("FREEZEQTY", typeof(int), 22, false)]
        public int FreezeQty;

        ///<summary>
        ///可用数量
        ///</summary>
        [FieldMapAttribute("AVAILABLEQTY", typeof(int), 22, false)]
        public int AvailableQty;

        ///<summary>
        ///库存数量
        ///</summary>
        [FieldMapAttribute("STORAGEQTY", typeof(int), 22, false)]
        public int StorageQty;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///物料描述
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///货位条码
        ///</summary>
        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, false)]
        public string LocationCode;

        ///<summary>
        ///库位
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///有效期起算时间
        ///</summary>
        [FieldMapAttribute("ValidStartDate", typeof(int), 22, false)]
        public int ValidStartDate;

        ///<summary>
        ///有效期起算时间
        ///</summary>
        [FieldMapAttribute("VALIDITY", typeof(int), 22, false)]
        public int VALIDITY;
        [FieldMapAttribute("ValidityCount", typeof(int), 22, false)]
        public int ValidityCount;
    }

    public class MailOQCEc : OQCDetailEC
    {


        [FieldMapAttribute("VENDERITEMCODE", typeof(string), 40, false)]
        public string VENDERITEMCODE;

        [FieldMapAttribute("OQCCUSER", typeof(string), 40, false)]
        public string IQCCUSER;
    }

    public class MailIQCEc : AsnIQCDetailEc
    {
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCODE;

        [FieldMapAttribute("VENDORMCODE", typeof(string), 40, false)]
        public string VENDORMCODE;

        [FieldMapAttribute("IQCCUSER", typeof(string), 40, false)]
        public string IQCCUSER;
    }

    [Serializable, TableMap("TBLALERTMAILSETTING", "SERIAL")]
    public class AlertMailSetting : DomainObject
    {
        public AlertMailSetting()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summ
        [FieldMapAttribute("BIGSSCode", typeof(string), 40, true)]
        public string BIGSSCode;

        ///<summary>
        ///ItemFirstClass
        ///</summary>	
        [FieldMapAttribute("ITEMFIRSTCLASS", typeof(string), 40, true)]
        public string ItemFirstClass;

        ///<summary>
        ///ItemSecondClass
        ///</summary>	
        [FieldMapAttribute("ITEMSECONDCLASS", typeof(string), 40, true)]
        public string ItemSecondClass;

        ///<summary>
        ///Recipients
        ///</summary>	
        [FieldMapAttribute("RECIPIENTS", typeof(string), 2000, false)]
        public string Recipients;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    public class CheckSummary : DomainObject
    {
        [FieldMap("StorageCode", typeof(string), 40, false)]
        public string StorageCode;

        [FieldMap("PASSCOUNT", typeof(int), 40, false)]
        public int PASSCOUNT;

        [FieldMap("TOTAL", typeof(int), 40, false)]
        public int TOTAL;

        [FieldMap("CHECKNOTOTAL", typeof(int), 40, false)]
        public int CHECKNOTOTAL;
    }

    public class OutStorageSummary : DomainObject
    {

        [FieldMap("StorageCode", typeof(string), 40, false)]
        public string StorageCode;
        [FieldMap("ISSUECOUNT", typeof(int), 40, false)]
        public int ISSUECOUNT;
        [FieldMap("PICKCOUNT", typeof(int), 40, false)]
        public int PICKCOUNT;
        [FieldMap("PACKCOUNT", typeof(int), 40, false)]
        public int PACKCOUNT;
        [FieldMap("FINISHCARTONNOCOUNT", typeof(int), 40, false)]
        public int FINISHCARTONNOCOUNT;
        [FieldMap("CARTONNOS", typeof(int), 40, false)]
        public int CARTONNOS;
        [FieldMap("VOLUME", typeof(decimal), 40, false)]
        public decimal VOLUME;
        [FieldMap("GROSS_WEIGHT", typeof(decimal), 40, false)]
        public decimal GROSS_WEIGHT;
        [FieldMap("picktotal", typeof(decimal), 40, false)]
        public decimal picktotal;

    }

    public class OrderLines : DomainObject
    {

        [FieldMap("PICKLINES", typeof(int), 40, false)]
        public int PICKLINES;

        [FieldMap("INVLINES", typeof(int), 40, false)]
        public int INVLINES;



        [FieldMap("PICKNO", typeof(string), 40, false)]
        public string PICKNO;

        [FieldMap("DNBATCHLINES", typeof(int), 40, false)]
        public int DNBATCHLINES;

        [FieldMap("CUSER", typeof(string), 40, false)]
        public string CUSER;
    }



    public class OutStorageDetail : DomainObject
    {

        [FieldMap("Delivery_Date", typeof(int), 40, false)]
        public int Delivery_Date;
        [FieldMap("Delivery_Time", typeof(int), 40, false)]
        public int Delivery_Time;
        [FieldMap("Packing_List_Date", typeof(int), 40, false)]
        public int Packing_List_Date;
        [FieldMap("Packing_List_Time", typeof(int), 40, false)]
        public int Packing_List_Time;
        [FieldMap("PICKNO", typeof(string), 40, false)]
        public string PICKNO;
        [FieldMap("INVNO", typeof(string), 40, false)]
        public string INVNO;
        [FieldMap("PICKTYPE", typeof(string), 40, false)]
        public string PICKTYPE;
        [FieldMap("STORAGECODE", typeof(string), 40, false)]
        public string STORAGECODE;
        [FieldMap("CDATE", typeof(int), 40, false)]
        public int CDATE;
        [FieldMap("CTIME", typeof(int), 40, false)]
        public int CTIME;
        [FieldMap("Down_Date", typeof(int), 40, false)]
        public int Down_Date;
        [FieldMap("Down_Time", typeof(int), 40, false)]
        public int Down_Time;
        [FieldMap("CARTONNOS", typeof(int), 40, false)]
        public int CARTONNOS;
        [FieldMap("VOLUMN", typeof(decimal), 40, false)]
        public decimal VOLUMN;
        [FieldMap("GROSS_WEIGHT", typeof(decimal), 40, false)]
        public decimal GROSS_WEIGHT;
        [FieldMap("CDATETIME", typeof(string), 40, false)]
        public string CDATETIME;
        [FieldMap("MAXPICK", typeof(string), 40, false)]
        public string MAXPICK;
        [FieldMap("MINPICK", typeof(string), 40, false)]
        public string MINPICK;
        [FieldMap("MAXPACK", typeof(string), 40, false)]
        public string MAXPACK;
        [FieldMap("MINPACK", typeof(string), 40, false)]
        public string MINPACK;
        [FieldMap("OQCCDATE", typeof(string), 40, false)]
        public string OQCCDATE;
        [FieldMap("OQCDATE", typeof(string), 40, false)]
        public string OQCDATE;
    }

    public class IQCSUMMARY : DomainObject
    {
        [FieldMap("STNO", typeof(string), 40, false)]
        public string STNO;
        [FieldMap("IQCCCOUNT", typeof(int), 40, false)]
        public int IQCCCOUNT;
        [FieldMap("QCCOUNT", typeof(int), 40, false)]
        public int QCCOUNT;
        [FieldMap("SQECOUNT", typeof(int), 40, false)]
        public int SQECOUNT;
        [FieldMap("SAMPLECOUNT", typeof(int), 40, false)]
        public int SAMPLECOUNT;
        [FieldMap("QTY", typeof(int), 40, false)]
        public int QTY;
        [FieldMap("NGQTY", typeof(int), 40, false)]
        public int NGQTY;
        [FieldMap("RETURNQTY", typeof(int), 40, false)]
        public int RETURNQTY;
        [FieldMap("REFORMQTY", typeof(int), 40, false)]
        public int REFORMQTY;
        [FieldMap("GIVEQTY", typeof(int), 40, false)]
        public int GIVEQTY;
        [FieldMap("ACCEPTQTY", typeof(int), 40, false)]
        public int ACCEPTQTY;
    }

    public class DateWithTimeRange : DomainObject
    {

        [FieldMap("TransnO", typeof(string), 40, false)]
        public string TransNo;
        [FieldMap("maxdate", typeof(string), 40, false)]
        public string MaxDate;
        [FieldMap("mindate", typeof(string), 40, false)]
        public string MinDate;
    }

    public class WeightVolume : DomainObject
    {
        [FieldMap("GROSS_WEIGHT", typeof(decimal), 40, false)]
        public decimal GROSS_WEIGHT;
        [FieldMap("Volume", typeof(decimal), 40, false)]
        public decimal Volume;
    }

    public class InstorageSummary : DomainObject
    {
        [FieldMap("StorageCode", typeof(string), 40, false)]
        public string StorageCode;
        [FieldMap("ASNCCOUNT", typeof(int), 40, false)]
        public int ASNCCOUNT;
        [FieldMap("ASNDOWNCOUNT", typeof(int), 40, false)]
        public int ASNDOWNCOUNT;
        [FieldMap("ASNRECEIVECOUNT", typeof(int), 40, false)]
        public int ASNRECEIVECOUNT;
        [FieldMap("ASNINSTORAGECOUNT", typeof(int), 40, false)]
        public int ASNINSTORAGECOUNT;

    }


    public class ProductOutstorageRecord : DomainObject
    {
        [FieldMap("DQMCODE", typeof(string), 40, false)]
        public string DQMCODE;

        [FieldMap("MDESC", typeof(string), 40, false)]
        public string MDESC;

        [FieldMap("CARINVNO", typeof(string), 40, false)]
        public string CARINVNO;
        [FieldMap("PICKNO", typeof(string), 40, false)]
        public string PICKNO;

        [FieldMap("INVNO", typeof(string), 40, false)]
        public string INVNO;

        [FieldMap("PICKTYPE", typeof(String), 40, false)]
        public string PICKTYPE;
        [FieldMap("Delivery_Date", typeof(int), 40, false)]
        public int DeliveryDate;

        [FieldMap("PQTY", typeof(decimal), 50, false)]
        public decimal PQTY;
        [FieldMap("SN", typeof(String), 40, false)]
        public string SN;

        [FieldMap("VENDORCODE", typeof(String), 40, false)]
        public string VENDORCODE;

        [FieldMap("Supplier_LotNo", typeof(String), 40, false)]
        public string Supplier_LotNo;

        [FieldMap("ProjectName", typeof(String), 40, false)]
        public string ProjectName;


        [FieldMap("ORDERNO", typeof(String), 40, false)]
        public string ORDERNO;


        [FieldMap("CUSORDERNO", typeof(String), 40, false)]
        public string CUSORDERNO;
        [FieldMap("ReceiverAddr", typeof(String), 40, false)]
        public string ReceiverAddr;

        [FieldMap("SHIPTOPARTY", typeof(String), 40, false)]
        public string SHIPTOPARTY;

    }

    public class ProductInstorageRecord : DomainObject
    {
        [FieldMap("Supplier_LotNo", typeof(String), 40, false)]
        public string Supplier_LotNo;

        [FieldMap("SN", typeof(String), 40, false)]
        public string SN;

        [FieldMap("STORAGEQTY", typeof(int), 40, false)]
        public int STORAGEQTY;

        [FieldMap("MDESC", typeof(string), 40, false)]
        public string MDESC;

        [FieldMap("DQMCODE", typeof(string), 40, false)]
        public string DQMCODE;

        [FieldMap("VendorName", typeof(string), 40, false)]
        public string VendorName;

        [FieldMap("VendorCode", typeof(string), 40, false)]
        public string VendorCode;

        [FieldMap("LastStorageAgeDate", typeof(int), 40, false)]
        public int LastStorageAgeDate;

        [FieldMap("StorageAgeDate", typeof(int), 40, false)]
        public int StorageAgeDate;

        [FieldMap("STTYPE", typeof(string), 40, false)]
        public string STTYPE;

        [FieldMap("INVNO", typeof(string), 40, false)]
        public string INVNO;

        [FieldMap("STNO", typeof(string), 40, false)]
        public string STNO;

        [FieldMap("ACTQTY", typeof(int), 40, false)]
        public int ACTQTY;
        [FieldMap("MDATE", typeof(int), 40, false)]
        public int MDATE;





    }

    public class PickAlterRecord : DomainObject
    {
        [FieldMap("PICKNO", typeof(String), 40, false)]
        public string PICKNO;

        [FieldMap("INVNO", typeof(String), 40, false)]
        public string INVNO;

        [FieldMap("PICKTYPE", typeof(String), 40, false)]
        public string PICKTYPE;

        [FieldMap("StorageCode", typeof(String), 100, false)]
        public string StorageCode;

        [FieldMap("CUSER", typeof(string), 40, false)]
        public string CUSER;

        [FieldMap("ProcessType", typeof(String), 40, false)]
        public String ProcessType;

        [FieldMap("Down_Date", typeof(int), 40, false)]
        public int DownDate;
        [FieldMap("Down_Time", typeof(int), 40, false)]
        public int DownTime;


        [FieldMap("CANCELCOUNT", typeof(int), 40, false)]
        public int CANCELCOUNT;

        [FieldMap("CANCELDATE", typeof(string), 40, false)]
        public string CANCELDATE;


    }


    public class AsnIssueCancelRecord : DomainObject
    {
        [FieldMap("STNO", typeof(String), 40, false)]
        public string STNO;

        [FieldMap("INVNO", typeof(String), 40, false)]
        public string INVNO;

        [FieldMap("STTYPE", typeof(String), 100, false)]
        public string STTYPE;

        [FieldMap("StorageCode", typeof(String), 100, false)]
        public string StorageCode;

        [FieldMap("VENDORCODE", typeof(String), 100, false)]
        public string VENDORCODE;

        [FieldMap("VendorName", typeof(String), 100, false)]
        public string VendorName;

        [FieldMap("CDATE", typeof(int), 40, false)]
        public int CDATE;

        [FieldMap("CTIME", typeof(int), 40, false)]
        public int CTIME;

        [FieldMap("CUSER", typeof(string), 40, false)]
        public string CUSER;

        [FieldMap("ProcessType", typeof(String), 40, false)]
        public String ProcessType;

        [FieldMap("CANCELCOUNT", typeof(int), 40, false)]
        public int CANCELCOUNT;
        [FieldMap("cancelISSUEDATE", typeof(string), 40, false)]
        public string CancelISSUEDATE;
        [FieldMap("ISSUEDATE", typeof(string), 40, false)]
        public string ISSUEDATE;




    }

    public class InStorageDetailRecord : DomainObject
    {

        [FieldMap("STNO", typeof(String), 40, false)]
        public string STNO;

        [FieldMap("INVNO", typeof(String), 40, false)]
        public string INVNO;

        [FieldMap("STTYPE", typeof(String), 100, false)]
        public string STTYPE;

        [FieldMap("StorageCode", typeof(String), 100, false)]
        public string StorageCode;


        [FieldMap("VENDORCODE", typeof(String), 100, false)]
        public string VENDORCODE;

        [FieldMap("VendorName", typeof(String), 100, false)]
        public string VendorName;



        [FieldMap("CDATE", typeof(string), 40, false)]
        public string CDATE;


        [FieldMap("CTIME", typeof(int), 40, false)]
        public int CTIME;

        [FieldMap("ASNCDATE", typeof(int), 40, false)]
        public int ASNCDATE;

        [FieldMap("ASNCTIME", typeof(int), 40, false)]
        public int ASNCTIME;

        [FieldMap("CARTONNOS", typeof(int), 40, false)]
        public int CARTONNOCount;

        [FieldMap("GROSS_WEIGHT", typeof(decimal), 40, false)]
        public decimal GROSS_WEIGHT;

        [FieldMap("VOLUME", typeof(String), 40, false)]
        public String VOLUME;

        [FieldMap("ProcessType", typeof(String), 40, false)]
        public String ProcessType;

        [FieldMap("receivebeginDate", typeof(String), 40, false)]
        public string ReceiveBeginDate;
        [FieldMap("IQCDate", typeof(String), 40, false)]
        public string IQCDate;
        [FieldMap("INSTORAEEDate", typeof(String), 40, false)]
        public string INSTORAEEDate;

        [FieldMap("IQCSQEdate", typeof(String), 40, false)]
        public string IQCSQEdate;
        [FieldMap("receiveCLOSEdate", typeof(String), 40, false)]
        public string ReceiveCloseDate;
        [FieldMap("issuedate", typeof(String), 40, false)]
        public string IssueDate;

        [FieldMap("ReceiveEndDate", typeof(String), 40, false)]
        public string ReceiveEndDate;
        [FieldMap("INSTORAEEBeginDate", typeof(String), 40, false)]
        public string INSTORAEEBeginDate;

        private int inStoraeeBeginDateInt;
        private int inStoraeeBeginTimeInt;
        public int InStoraeeBeginDateInt
        {
            get
            {
                InitDateTime(INSTORAEEBeginDate, ref inStoraeeBeginDateInt, ref inStoraeeBeginTimeInt);
                return inStoraeeBeginDateInt;
            }
        }
        public int InStoraeeBeginTimeInt
        {
            get
            {
                InitDateTime(INSTORAEEBeginDate, ref inStoraeeBeginDateInt, ref inStoraeeBeginTimeInt);
                return inStoraeeBeginTimeInt;
            }
        }
        private int receiveBeginDateInt;
        private int receiveBeginTimeInt;
        public int ReceiveBeginDateInt
        {
            get
            {
                InitDateTime(ReceiveBeginDate, ref receiveBeginDateInt, ref receiveBeginTimeInt);
                return receiveBeginDateInt;
            }
        }

        public int ReceiveBeginTimeInt
        {
            get
            {
                InitDateTime(ReceiveBeginDate, ref receiveBeginDateInt, ref receiveBeginTimeInt);
                return receiveBeginTimeInt;
            }
        }

        private int iqcDateInt;
        private int iqcTimeInt;
        public int IQCDateInt
        {
            get
            {
                InitDateTime(IQCDate, ref iqcDateInt, ref iqcTimeInt);
                return iqcDateInt;
            }
        }
        public int IQCTimeInt
        {
            get
            {
                InitDateTime(IQCDate, ref iqcDateInt, ref iqcTimeInt);
                return iqcTimeInt;
            }
        }

        private int receiveEndDateInt;
        private int receiveEndTimeInt;
        public int ReceiveEndDateInt
        {
            get
            {

                InitDateTime(ReceiveCloseDate, ref receiveEndDateInt, ref receiveEndTimeInt);
                return receiveEndDateInt;

            }
        }
        public int ReceiveEndTimeInt
        {
            get
            {

                InitDateTime(ReceiveEndDate, ref receiveEndDateInt, ref receiveEndTimeInt);
                return receiveEndTimeInt;

            }
        }

        private int instoraeeDateInt;
        private int instoraeeTimeInt;
        public int InStoraeeDateInt
        {
            get
            {

                InitDateTime(INSTORAEEDate, ref instoraeeDateInt, ref instoraeeTimeInt);
                return instoraeeDateInt;

            }
        }
        public int InStoraeeTimeInt
        {
            get
            {

                InitDateTime(INSTORAEEDate, ref instoraeeDateInt, ref instoraeeTimeInt);
                return instoraeeTimeInt;

            }
        }


        private int iqcsqeDateInt;
        private int iqcsqeTimeInt;
        public int IQCSQEDateInt
        {
            get
            {

                InitDateTime(IQCSQEdate, ref iqcsqeDateInt, ref iqcsqeTimeInt);
                return iqcsqeDateInt;

            }
        }
        public int IQCSQETimeInt
        {
            get
            {

                InitDateTime(IQCSQEdate, ref iqcsqeDateInt, ref iqcsqeTimeInt);
                return iqcsqeTimeInt;

            }
        }


        private int receiveCloseDateInt;
        private int receiveCloseTimeInt;
        public int ReceiveCloseDateInt
        {
            get
            {

                InitDateTime(ReceiveCloseDate, ref receiveCloseDateInt, ref receiveCloseTimeInt);
                return receiveCloseTimeInt;

            }
        }
        public int ReceiveCloseTimeInt
        {
            get
            {

                InitDateTime(ReceiveCloseDate, ref receiveCloseDateInt, ref receiveCloseTimeInt);
                return receiveCloseTimeInt;

            }
        }

        private int issueDateInt;
        private int issueTimeInt;
        public int IssueDateInt
        {
            get
            {

                InitDateTime(IssueDate, ref issueDateInt, ref issueTimeInt);
                return issueDateInt;

            }
        }
        public int IssueTimeInt
        {
            get
            {

                InitDateTime(IssueDate, ref issueDateInt, ref issueTimeInt);
                return issueTimeInt;

            }
        }

        private int cdateInt;
        private int ctimeInt;
        public int CDateInt
        {
            get
            {
                InitDateTime(CDATE, ref cdateInt, ref ctimeInt);
                return cdateInt;

            }
        }

        public int CTimeInt
        {
            get
            {
                InitDateTime(CDATE, ref cdateInt, ref ctimeInt);
                return ctimeInt;

            }
        }

        public void InitDateTime(string dateTimeString, ref int date, ref int time)
        {

            if (date == 0)
            {
                if (!string.IsNullOrEmpty(dateTimeString))
                {
                    string[] sss = dateTimeString.Split(' ');
                    date = int.Parse(sss[0]);
                    time = int.Parse(sss[1]);
                }
            }
        }

    }



    public class IQCDetailRecord : DomainObject
    {

        [FieldMap("STNO", typeof(String), 40, false)]
        public string STNO;

        [FieldMap("CDATE", typeof(int), 40, false)]
        public int CDATE;

        [FieldMap("CTIME", typeof(int), 40, false)]
        public int CTIME;

        [FieldMap("IQCNO", typeof(String), 100, false)]
        public string IQCNO;

        [FieldMap("INVNO", typeof(String), 100, false)]
        public string INVNO;

        [FieldMap("STTYPE", typeof(String), 100, false)]
        public string STTYPE;

        [FieldMap("StorageCode", typeof(String), 100, false)]
        public string StorageCode;

        [FieldMap("VENDORCODE", typeof(String), 100, false)]
        public string VENDORCODE;


        [FieldMap("VendorName", typeof(String), 100, false)]
        public string VendorName;


        [FieldMap("DQMCODE", typeof(String), 100, false)]
        public string DQMCODE;

        [FieldMap("VENDORMCODE", typeof(String), 100, false)]
        public string VENDORMCODE;


        [FieldMap("MDESC", typeof(String), 100, false)]
        public string MDESC;

        [FieldMap("IQCTYPE", typeof(String), 100, false)]
        public string IQCTYPE;

        [FieldMap("AQLLEVEL", typeof(String), 100, false)]
        public string AQLLEVEL;

        [FieldMap("QTY", typeof(int), 100, false)]
        public int QTY;

        [FieldMap("IQCQTY", typeof(int), 100, false)]
        public int IQCQTY;

        [FieldMap("NGQTY", typeof(int), 100, false)]
        public int NGQTY;

        [FieldMap("RETURNQTY", typeof(int), 100, false)]
        public int RETURNQTY;

        [FieldMap("ReformQTY", typeof(int), 100, false)]
        public int ReformQTY;

        [FieldMap("GiveQTY", typeof(int), 100, false)]
        public int GiveQTY;

        [FieldMap("AcceptQTY", typeof(int), 100, false)]
        public int AcceptQTY;

        [FieldMap("ECGCODE", typeof(string), 100, false)]
        public string ECGCODE;

        [FieldMap("ECODE", typeof(string), 100, false)]
        public string ECODE;

        [FieldMap("ProcessType", typeof(string), 100, false)]
        public string ProcessType;

        [FieldMap("mdate", typeof(int), 100, false)]
        public int mdate;
        [FieldMap("MTime", typeof(int), 100, false)]
        public int MTime;
        [FieldMap("SAMPLESIZE", typeof(int), 100, false)]
        public int SAMPLESIZE;



        public List<string> ECGCODEList = new List<string>();
        public Dictionary<string, List<string>> ECGCODEDic = new Dictionary<string, List<string>>();
        public List<DateWithTime> IQCSQEDateRange = new List<DateWithTime>();
        public List<DateWithTime> IQCDateRange = new List<DateWithTime>();
    }

    public class DateWithTimeComper : IComparer<DateWithTime>
    {
        #region IComparer<DateWithTime> 成员

        public int Compare(DateWithTime x, DateWithTime y)
        {
            int daySpan = x.Date - y.Date;
            return daySpan == 0 ? x.Time - y.Time : daySpan;
        }

        #endregion
    }

    public class DateWithTime
    {
        public int Date;
        public int Time;
    }

    public class StorageInOperationRecord : DomainObject
    {
        [FieldMap("STORAGECODE", typeof(String), 40, false)]
        public string STORAGECODE;

        [FieldMap("INVNO", typeof(String), 40, false)]
        public string INVNO;

        [FieldMap("STNO", typeof(String), 40, false)]
        public string STNO;

        [FieldMap("ProcessType", typeof(String), 40, false)]
        public string ProcessType;


        [FieldMap("MDATE", typeof(int), 40, false)]
        public int MDATE;

        [FieldMap("MUSER", typeof(string), 40, false)]
        public string MUSER;

        public int InStorageDate;
        public int ReceiveDate;
        public int IQCSQEDate;
        public int IQCDate;
        public List<string> IQCOpers = new List<string>();
        public List<string> ReceiveOpers = new List<string>();
        public List<string> InStorageOpers = new List<string>();
        public List<string> IQCSQEOpers = new List<string>();
    }

    public class PickPak : DomainObject
    {
        [FieldMap("DQSITEMCODE", typeof(String), 40, false)]
        public string DQSITEMCODE;


        [FieldMap("CUSITEMDESC", typeof(String), 40, false)]
        public string CUSITEMDESC;

        [FieldMap("unit", typeof(String), 40, false)]
        public string unit;


        [FieldMap("GFHWITEMCODE", typeof(String), 40, false)]
        public string GFHWITEMCODE;

        [FieldMap("CARTONNO", typeof(String), 40, false)]
        public string CARTONNO;

        [FieldMap("QTY", typeof(decimal), 40, false)]
        public decimal QTY;
    }

    #region
    public class StorLocTransDN : DomainObject
    {
        [FieldMap("INVNO", typeof(String), 40, false)]
        public string BatchCode;

        [FieldMap("QTY", typeof(decimal), 40, false)]
        public decimal QTY;

        [FieldMap("DQMCODE", typeof(String), 40, false)]
        public string DQMCODE;
    }

    public class QTYN : DomainObject
    {
        [FieldMap("QTY", typeof(decimal), 40, false)]
        public decimal QTY;

        [FieldMap("PLANQTY", typeof(decimal), 40, false)]
        public decimal PlanQTY;

        [FieldMap("dqmcode", typeof(string), 40, false)]
        public string DQMCode;
    }


    public class InvoicesDetailDN : InvoicesDetail
    {
        [FieldMap("BatchCode", typeof(String), 40, false)]
        public string BatchCode;
    }

    public class AsnDetailCountCollection : DomainObject
    {
        [FieldMap("TOTAL", typeof(int), 40, false)]
        public int Total;
        [FieldMap("CloseCount", typeof(int), 40, false)]
        public int CloseCount;

        [FieldMap("RejectCount", typeof(int), 40, false)]
        public int RejectCount;
    }
    #endregion

    public class PickDetailDN : DomainObject
    {
        [FieldMap("INVNO", typeof(String), 40, false)]
        public string BatchCode;

        [FieldMap("PQTY", typeof(decimal), 40, false)]
        public decimal PQTY;

        [FieldMap("DQMCODE", typeof(String), 40, false)]
        public string DQMCODE;
    }

    public class StockCheckDetailDo : DomainObject
    {
        [FieldMap("StorageCode", typeof(String), 40, false)]
        public string StorageCode;
        [FieldMap("LocationCode", typeof(String), 50, false)]
        public string LocationCode;
        [FieldMap("DQMCODE", typeof(String), 200, false)]
        public string DQMCODE;

        [FieldMap("CARTONNO", typeof(String), 40, false)]
        public string CARTONNO;
        [FieldMap("STORAGEQTY", typeof(decimal), 40, false)]
        public decimal STORAGEQTY;
        [FieldMap("CheckQty", typeof(decimal), 40, false)]
        public decimal Qty;


    }

    public class StockCheckDetailOp : DomainObject
    {
        [FieldMap("StorageCode", typeof(String), 40, false)]
        public string StorageCode;
        [FieldMap("LocationCode", typeof(String), 50, false)]
        public string LocationCode;
        //[FieldMap("LocationCode2", typeof(String), 50, false)]
        //public string LocationCode2;
        [FieldMap("DQMCODE", typeof(String), 200, false)]
        public string DQMCODE;
        [FieldMap("CTIME", typeof(int), 40, false)]
        public int CTIME;
        [FieldMap("CheckNo", typeof(String), 40, false)]
        public string CheckNo;

        [FieldMap("SCARTONNO", typeof(String), 40, false)]
        public string SCARTONNO;

        [FieldMap("SLocationCode", typeof(String), 50, false)]
        public string SLocationCode;

        [FieldMap("CARTONNO", typeof(String), 40, false)]
        public string CARTONNO;
        [FieldMap("STORAGEQTY", typeof(decimal), 40, false)]
        public decimal STORAGEQTY;
        [FieldMap("CheckQty", typeof(decimal), 40, false)]
        public decimal Qty;

        [FieldMap("DIFFDESC", typeof(string), 200, false)]
        public string DiffDesc;


    }
    public class InvoicesDetailEx1 : InvoicesDetail
    {
        [FieldMapAttribute("INVTYPE", typeof(string), 40, true)]
        public string INVTYPE;
        [FieldMapAttribute("PICKNO", typeof(string), 40, true)]
        public string PICKNO;
        [FieldMapAttribute("DNBATCHNO", typeof(string), 40, true)]
        public string DNBATCHNO;

    }

    public class Do : DomainObject
    {
        [FieldMapAttribute("sum", typeof(decimal), 40, true)]
        public decimal sum;
        [FieldMapAttribute("CARTONNO", typeof(string), 50, true)]
        public string CARTONNO;
        [FieldMapAttribute("MDESC", typeof(string), 40, true)]
        public string MDESC;

        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string UNIT;

        [FieldMapAttribute("DQMCODE", typeof(string), 40, true)]
        public string DQMCODE;

        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, true)]
        public string LOCATIONCODE;
    }

    public class InvoicesDetailEx : InvoicesDetail
    {
        [FieldMapAttribute("INVTYPE", typeof(string), 40, true)]
        public string INVTYPE;
    }
    public class PickInfo1 : DomainObject
    {
        [FieldMapAttribute("CARINVNO", typeof(string), 40, true)]
        public string CARINVNO;

        [FieldMapAttribute("PICKNO", typeof(string), 40, true)]
        public string PICKNO;

        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string STATUS;

        [FieldMapAttribute("INVNO", typeof(string), 40, true)]
        public string INVNO;

        [FieldMapAttribute("ORDERNO", typeof(string), 40, true)]
        public string ORDERNO;

        [FieldMapAttribute("StorageCode", typeof(string), 40, true)]
        public string StorageCode;
        [FieldMapAttribute("ReceiverUser", typeof(string), 40, true)]
        public string ReceiverUser;

        [FieldMapAttribute("ReceiverAddr", typeof(string), 40, true)]
        public string ReceiverAddr;

        [FieldMapAttribute("Plan_Date", typeof(int), 22, true)]
        public int Plan_Date;

        [FieldMapAttribute("PLANGIDATE", typeof(string), 40, true)]
        public string PLANGIDATE;
        [FieldMapAttribute("GFCONTRACTNO", typeof(string), 40, true)]
        public string GFCONTRACTNO;

        [FieldMapAttribute("GFFLAG", typeof(string), 40, true)]
        public string GFFLAG;


        [FieldMapAttribute("OANO", typeof(string), 40, true)]
        public string OANO;

        [FieldMapAttribute("Packing_List_Date", typeof(int), 22, true)]
        public int Packing_List_Date;


        [FieldMapAttribute("Packing_List_Time", typeof(int), 22, true)]
        public int Packing_List_Time;

        [FieldMapAttribute("Shipping_Mark_Date", typeof(int), 22, true)]
        public int Shipping_Mark_Date;


        [FieldMapAttribute("Shipping_Mark_Time", typeof(int), 22, true)]
        public int Shipping_Mark_Time;
        [FieldMapAttribute("GROSS_WEIGHT", typeof(double), 32, true)]
        public double GROSS_WEIGHT;
        [FieldMapAttribute("VOLUME", typeof(string), 40, true)]
        public string VOLUME;
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string CARTONNO;
        [FieldMapAttribute("GFHWITEMCODE", typeof(string), 40, true)]
        public string GFHWITEMCODE;
        [FieldMapAttribute("GFPACKINGSEQ", typeof(string), 40, true)]
        public string GFPACKINGSEQ;
        [FieldMapAttribute("PICKLINE", typeof(string), 40, true)]
        public string PICKLINE;

        [FieldMapAttribute("SN", typeof(string), 40, true)]
        public string SN;

        [FieldMapAttribute("P22", typeof(string), 40, true)]
        public string P22;

        [FieldMapAttribute("CUSBATCHNO", typeof(string), 40, true)]
        public string CUSBATCHNO;

        [FieldMapAttribute("VENDERITEMCODE", typeof(string), 40, true)]
        public string VENDERITEMCODE;
        [FieldMapAttribute("DQMCODE", typeof(string), 40, true)]
        public string DQMCODE;
        [FieldMapAttribute("HWTYPEINFO", typeof(string), 40, true)]
        public string HWTYPEINFO;
        [FieldMapAttribute("MDESC", typeof(string), 40, true)]
        public string MDESC;

        [FieldMapAttribute("QTY", typeof(double), 40, true)]
        public double QTY;


        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string UNIT;

        [FieldMapAttribute("CUSITEMDESC", typeof(string), 40, true)]
        public string CUSITEMDESC;


        [FieldMapAttribute("PICKTYPE", typeof(string), 40, true)]
        public string PICKTYPE;

        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string CUSTMCODE;


    }
    public class OQCNew : DomainObject
    {
        [FieldMapAttribute("CARINVNO", typeof(string), 40, true)]
        public string CARINVNO;

        [FieldMapAttribute("GFHWITEMCODE", typeof(string), 40, true)]
        public string GFHWITEMCODE;
        [FieldMapAttribute("MCODE", typeof(string), 40, true)]
        public string MCODE;

        [FieldMapAttribute("DQMCODE", typeof(string), 40, true)]
        public string DQMCODE;

        [FieldMapAttribute("QTY", typeof(decimal), 40, true)]
        public decimal QTY;
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string UNIT;
        [FieldMapAttribute("GFPACKINGSEQ", typeof(string), 40, true)]
        public string GFPACKINGSEQ;
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string CARTONNO;
        [FieldMapAttribute("SN", typeof(string), 40, true)]
        public string SN;

    }

    public class SendCaseDetails : DomainObject
    {
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string CARTONNO;

        [FieldMapAttribute("GFHWITEMCODE", typeof(string), 40, true)]
        public string GFHWITEMCODE;

        [FieldMapAttribute("HWCODEQTY", typeof(string), 40, true)]
        public string HWCODEQTY;

        [FieldMapAttribute("CUSITEMDESC", typeof(string), 40, true)]
        public string CUSITEMDESC;

        [FieldMapAttribute("DQMCODE", typeof(string), 40, true)]
        public string DQMCODE;

        [FieldMapAttribute("MDESC", typeof(string), 40, true)]
        public string MDESC;
        [FieldMapAttribute("CUSITEMSPEC", typeof(string), 40, true)]
        public string CUSITEMSPEC;
        [FieldMapAttribute("QTY", typeof(double), 40, true)]
        public double QTY;
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string UNIT;
        [FieldMapAttribute("VENDERITEMCODE", typeof(string), 40, true)]
        public string VENDERITEMCODE;
        [FieldMapAttribute("PACKINGWAYNO", typeof(string), 40, true)]
        public string PACKINGWAYNO;

        [FieldMapAttribute("SN", typeof(string), 40, true)]
        public string SN;

    }




    /// <summary>
    /// 这个类，用在拆解中
    /// </summary>
    public class InnoObject
    {
        public override string ToString()
        {
            return MItemCode + ":" + MCard;
        }

        //行号
        public int LineIndex;
        //项次
        public int ItemIndex;
        //料号
        public string MItemCode;
        //批号或者KeyPart的SN号
        public string MCard;
        //数量
        public int Qty;
        //类型
        public string MCardType;
        //工单
        public string MOCode;

        //新条码
        public string NewBarcode;

        //新解析出来的信息
        public string NewLotNo;
        public string NewPCBA;
        public string NewBIOS;
        public string NewVersion;
        public string NewVendorItemCode;
        public string NewVendorCode;
        public string NewDateCode;
    }

    public class WarehouseTicketDetail2 : WarehouseTicketDetail
    {
        public WarehouseTicketDetail2()
        {
        }

        /// <summary>
        /// 数量
        /// </summary>
        [FieldMapAttribute("SingleQTY", typeof(decimal), 15, false)]
        public decimal SingleQTY;
    }

    public class WarehouseCountDetail
    {
        public WarehouseCountDetail()
        {
        }

        /// <summary>
        /// 工单
        /// </summary>
        public string MOCode = string.Empty;
        /// <summary>
        /// 工单发料数量
        /// </summary>
        public decimal MOFQty = 0;
        /// <summary>
        /// 上料数量
        /// </summary>
        public decimal CollectQty = 0;
        /// <summary>
        /// 下料数量
        /// </summary>
        public decimal DropQty = 0;
        /// <summary>
        /// 退料数量
        /// </summary>
        public decimal BackQty = 0;
        /// <summary>
        /// 移库入库数量
        /// </summary>
        public decimal YWHQtyIn = 0;
        /// <summary>
        /// 移库出库数量
        /// </summary>
        public decimal YWHQtyOut = 0;
        /// <summary>
        /// 维修换上数量
        /// </summary>
        public decimal TsQtyOn = 0;
        /// <summary>
        /// 维修换下数量
        /// </summary>
        public decimal TsQtyDown = 0;
        /// <summary>
        /// 脱离工单数量
        /// </summary>
        public decimal OffMoQty = 0;
        /// <summary>
        /// 离散数量
        /// </summary>
        public decimal WarehouseQty = 0;
        /// <summary>
        /// 在制品虚拆数量
        /// </summary>
        public decimal Lineqty = 0;
        /// <summary>
        /// 账面数
        /// </summary>
        public decimal BillOpenQty = 0;
    }

    [Serializable, TableMap("TBLSENDMAIL", "SERIAL")]
    public class SendMail : DomainObject
    {
        public SendMail()
        {
        }


        [FieldMapAttribute("SERIAL", typeof(int), 40, false)]
        public int SERIAL;

        [FieldMapAttribute("BLOCKNAME", typeof(string), 40, true)]
        public string BLOCKNAME;

        [FieldMapAttribute("BLOCKINX", typeof(int), 40, true)]
        public int BLOCKINX;

        [FieldMapAttribute("MAILTYPE", typeof(string), 40, true)]
        public string MAILTYPE;

        ///<summary>
        ///ItemFirstClass
        ///</summary>	
        [FieldMapAttribute("SENDFLAG", typeof(string), 40, true)]
        public string SENDFLAG;

        ///<summary>
        ///ItemSecondClass
        ///</summary>	
        [FieldMapAttribute("MAILCONTENT", typeof(string), 40, true)]
        public string MAILCONTENT;



        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUSER;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("CDATE", typeof(int), 8, false)]
        public int CDATE;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("CTIME", typeof(int), 6, false)]
        public int CTIME;

        [FieldMapAttribute("Recipients", typeof(string), 6, false)]
        public string Recipients;

    }
}

