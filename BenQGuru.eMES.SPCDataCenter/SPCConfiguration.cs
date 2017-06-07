using System;
using System.Collections;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.SPC;

namespace BenQGuru.eMES.SPCDataCenter
{
    /// <summary>
    /// SPCConfiguration 的摘要说明。
    /// </summary>
    public class SPCConfiguration
    {
        public SPCConfiguration()
        {
        }
        public SPCConfiguration(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

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

        private static string _databaseType = string.Empty;
        /// <summary>
        /// 数据库类型：Oracle/SQLServer
        /// </summary>
        public string DatabaseType
        {
            get
            {
                if (_databaseType == string.Empty)
                {
                    string strSql = "SELECT * FROM tblSPCConfig WHERE ParamName='SPCDatabaseType'";
                    object[] objs = this.DataProvider.CustomQuery(typeof(SPCConfig), new SQLCondition(strSql));
                    if (objs != null && objs.Length > 0)
                    {
                        SPCConfig cfg = (SPCConfig)objs[0];
                        _databaseType = cfg.ParamValue;
                    }
                }
                return _databaseType;
            }
        }

        private static string _sequenceName = string.Empty;
        /// <summary>
        /// Oracle产生ID的Sequence
        /// </summary>
        public string SequenceName
        {
            get
            {
                if (_sequenceName == string.Empty)
                {
                    string strSql = "SELECT * FROM tblSPCConfig WHERE ParamName='OracleSequence'";
                    object[] objs = this.DataProvider.CustomQuery(typeof(SPCConfig), new SQLCondition(strSql));
                    if (objs != null && objs.Length > 0)
                    {
                        SPCConfig cfg = (SPCConfig)objs[0];
                        _sequenceName = cfg.ParamValue;
                    }
                }
                return _sequenceName;
            }
        }

        private static Hashtable htSpcDataIdx = null;
        /// <summary>
        /// 查询产品控制对象的数据存储栏位索引
        /// </summary>
        /// <returns></returns>
        public int GetDataColumnIndex(string itemCode, string spcObjectCode, decimal groupSequence)
        {
            if (htSpcDataIdx == null)
                htSpcDataIdx = new Hashtable();
            string strKey = itemCode + "||" + spcObjectCode + "||" + Convert.ToInt32(groupSequence).ToString();
            if (htSpcDataIdx.ContainsKey(strKey) == true)
            {
                return Convert.ToInt32(htSpcDataIdx[strKey]);
            }
            else
            {

                string strSql = "SELECT * FROM TBLSPCOBJECTSTORE WHERE ObjectCode='" + spcObjectCode + "' AND GroupSeq=" + groupSequence.ToString();
                object[] objs = this.DataProvider.CustomQuery(typeof(SPCObjectStore), new SQLCondition(strSql));
                if (objs != null && objs.Length > 0)
                {
                    SPCObjectStore spcStore = (SPCObjectStore)objs[0];
                    //return Convert.ToInt32(spcStore.StoreColumn);
                }
            }
            return -1;
        }

        private static Hashtable htSpcTestCount = null;
        /// <summary>
        /// 查询控制对象测试数据次数
        /// </summary>
        /// <param name="spcObjectCode"></param>
        /// <returns></returns>
        public int GetObjectTestCount(string spcObjectCode)
        {
            if (htSpcTestCount == null)
                htSpcTestCount = new Hashtable();
            if (htSpcTestCount.ContainsKey(spcObjectCode) == true)
                return Convert.ToInt32(htSpcTestCount[spcObjectCode]);
            else
            {
                SPCFacade spcFacade = new SPCFacade(this.DataProvider);
                SPCObject spcObj = (SPCObject)spcFacade.GetSPCObject(spcObjectCode);
                if (spcObj != null)
                {
                    //					htSpcTestCount.Add(spcObjectCode, spcObj.TestTimes);
                    //return Convert.ToInt32(spcObj.TestTimes);
                }
            }
            return 1;
        }

        private static Hashtable htItemObjGroupCount = null;
        /// <summary>
        /// 查询控制对象测试组数
        /// </summary>
        /// <returns></returns>
        public int GetItemObjectGroupCount(string itemCode, string spcObjectCode)
        {
            if (htItemObjGroupCount == null)
                htItemObjGroupCount = new Hashtable();
            string strKey = itemCode + "||" + spcObjectCode;
            if (htItemObjGroupCount.ContainsKey(strKey) == true)
                return Convert.ToInt32(htItemObjGroupCount[strKey]);
            else
            {
                string strSql = string.Empty;
                if (itemCode.IndexOf(",") < 0)
                    strSql = "SELECT COUNT(*) FROM tblSPCItemSpec WHERE ItemCode='" + itemCode + "' AND ObjectCode='" + spcObjectCode + "'";
                else
                    strSql = "SELECT MAX(cnt) FROM (SELECT ItemCode,COUNT(*) cnt FROM tblSPCItemSpec WHERE ItemCode IN ('" + itemCode.Replace(",", "','") + "') AND ObjectCode='" + spcObjectCode + "' GROUP BY ItemCode ) ";
                int iGroup = 0;
                try
                {
                    iGroup = this.DataProvider.GetCount(new SQLCondition(strSql));
                    //					if (itemCode.IndexOf(",") < 0)
                    //						htItemObjGroupCount.Add(strKey, iGroup);
                }
                catch { }
                return iGroup;
            }
        }

    }
}
