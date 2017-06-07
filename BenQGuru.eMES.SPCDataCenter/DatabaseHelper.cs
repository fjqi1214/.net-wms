using System;
using System.Collections;
using System.Text;
using System.Data;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.SPCDataCenter
{
    /// <summary>
    /// DatabaseHelper 的摘要说明。
    /// </summary>
    public class DatabaseHelper
    {
        public DatabaseHelper()
        {
        }

        private IDomainDataProvider _spcDomainDataProvider = null;
        private BenQGuru.eMES.Common.PersistBroker.IPersistBroker _spcBroker;
        public BenQGuru.eMES.Common.PersistBroker.IPersistBroker SPCBroker
        {
            get
            {
                if (this._spcBroker == null)
                {
                    //modified by carey.cheng on 2010-05-20 for muti db support
                    string connStr = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.GetSetting(DBName.SPC.ToString()).ConnectString;
                    //end modified by carey.cheng on 2010-05-20 for muti db support
                    if (connStr == null || connStr == String.Empty)
                        ExceptionManager.Raise(typeof(DatabaseHelper), "$Error_SPC_Connection_String", null, null);

                    string type = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.GetSetting(DBName.SPC.ToString()).PersistBrokerType;
                    this._spcBroker = BenQGuru.eMES.Common.PersistBroker.PersistBrokerManager.PersistBroker(connStr, null, type);
                }
                return this._spcBroker;
            }
        }

        public IDomainDataProvider SPCDataProvider
        {
            get
            {
                if (_spcDomainDataProvider == null)
                {
                    _spcDomainDataProvider = DomainDataProviderManager.DomainDataProvider(SPCBroker, null);
                }

                return _spcDomainDataProvider;
            }
        }

        public void BeginTransaction()
        {
            this.SPCDataProvider.BeginTransaction();
        }
        public void RollTransaction()
        {
            this.SPCDataProvider.RollbackTransaction();
        }
        public void CommitTransaction()
        {
            this.SPCDataProvider.CommitTransaction();
        }
        public void OpenConnection()
        {
            this.SPCBroker.OpenConnection();
        }
        public void CloseConnection()
        {
            this.SPCBroker.CloseConnection();
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        public string[][] QuerySPCDataNew(int groupSeq, string where)
        {
            // 组建SQL语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT distinct ResCode,TestingDate,TestingTime,TESTINGVALUE");
            sb.Append(" FROM  TBLTESTDATA INNER JOIN TBLMESENTITYLIST ON TBLTESTDATA.TBLMESENTITYLIST_SERIAL =TBLMESENTITYLIST.SERIAL ");
            sb.Append(" INNER JOIN TBLSPCOBJECTSTORE ON TBLTESTDATA.Ckgroup = TBLSPCOBJECTSTORE.ckgroup ");
            sb.Append(where + " ORDER BY TestingDate,TestingTime");

            DataSet ds = this.SPCBroker.Query(sb.ToString());
            int iCount = ds.Tables[0].Rows.Count;
            ArrayList list = new ArrayList();
            for (int i = 0; i < iCount; i++)
            {
                string[] v = new string[4];
                v[0] = GetDataWithNull(ds.Tables[0].Rows[i]["ResCode"]);
                v[1] = GetDataWithNull(ds.Tables[0].Rows[i]["TestingDate"]);
                v[2] = GetDataWithNull(ds.Tables[0].Rows[i]["TestingTime"]);
                v[3] = GetDataWithNull(ds.Tables[0].Rows[i]["TESTINGVALUE"]);
                list.Add(v);

            }
            string[][] rets = new string[list.Count][];
            list.CopyTo(rets);
            return rets;
        }

        /// <summary>
        /// 查询SPC格式数据的资源列表
        /// </summary>
        public string[] QuerySPCDataResource(int groupSeq, string where)
        {
            // 组建SQL语句
            string strSql = "SELECT DISTINCT ResCode FROM TBLTESTDATA "
                + " INNER JOIN TBLMESENTITYLIST ON TBLTESTDATA.TBLMESENTITYLIST_SERIAL =TBLMESENTITYLIST.SERIAL  "
                + where;

            DataSet ds = this.SPCBroker.Query(strSql);
            ArrayList listRes = new ArrayList();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string strRes = GetDataWithNull(ds.Tables[0].Rows[i]["ResCode"]);
                if (strRes != string.Empty)
                    listRes.Add(strRes);
            }
            string[] rets = new string[listRes.Count];
            listRes.CopyTo(rets);
            return rets;
        }

        private string GetDataWithNull(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return string.Empty;
            else
                return obj.ToString();
        }
    }
}
