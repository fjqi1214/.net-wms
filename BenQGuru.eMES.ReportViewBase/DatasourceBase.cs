using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.ReportView;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.ReportViewBase
{
    public class DatasourceBase
    {
        private RptViewDataConnect _dataConnect = null;
        /// <summary>
        /// 数据库连接配置
        /// </summary>
        public RptViewDataConnect DataConnect
        {
            get { return _dataConnect; }
            set { _dataConnect = value; }
        }
        private IDbConnection _connection;
        /// <summary>
        /// 透过DataConnect生成PersistBroker
        /// </summary>
        /// <returns></returns>
        public IPersistBroker GetPersistBroker(RptViewDataConnect dataConnect)
        {
            if (dataConnect == null)
                return null;
            IPersistBroker persistBroker = null;
            string strPassword = BenQGuru.eMES.Common.Helper.EncryptionHelper.DESDecryption(dataConnect.Password);

            if (dataConnect.ServerType == DatabaseType.SQLServer)
            {
                string strConn = string.Format("Data Source={0};User ID={1};Password={2};Initial Catalog={3};", dataConnect.ServiceName, dataConnect.UserName, strPassword, dataConnect.DefaultDatabase);
                SqlPersistBroker sql = new SqlPersistBroker(strConn);
                _connection = sql.Connection;
                persistBroker = new BenQGuru.eMES.Common.PersistBroker.SqlPersistBroker(strConn);
            }
            else if (dataConnect.ServerType == DatabaseType.Oracle)
            {
                string strConn = string.Format("Provider=OraOLEDB.Oracle;Data Source={0};User ID={1};Password={2};Persist Security Info=True;", dataConnect.ServiceName, dataConnect.UserName, strPassword);
                OLEDBPersistBroker ole = new OLEDBPersistBroker(strConn);
                _connection = ole.Connection;
                persistBroker = new BenQGuru.eMES.Common.PersistBroker.OLEDBPersistBroker(strConn);
            }
            return persistBroker;
        }

        /// <summary>
        /// 检查数据库连接是否能连接
        /// </summary>
        /// <param name="dataConnect"></param>
        /// <returns></returns>
        public bool CheckDataConnect(RptViewDataConnect dataConnect)
        {
            IPersistBroker persistBroker = this.GetPersistBroker(dataConnect);

            if (persistBroker == null)
                return false;
            try
            {

                _connection.Open();
                _connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 执行SQL查询，返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet ExecuteSql(string sql)
        {
            IPersistBroker persistBroker = this.GetPersistBroker(_dataConnect);
            if (persistBroker == null)
                return null;
            DataSet ds = new DataSet();
            ds = persistBroker.Query(sql);
            return ds;
        }

        /// <summary>
        /// 执行SQL查询，返回Schema
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet QuerySchemaSql(string sql)
        {
            IPersistBroker persistBroker = this.GetPersistBroker(_dataConnect);
            if (persistBroker == null)
                return null;
            DataSet ds = new DataSet();
            string strSql = "";
            if (_dataConnect.ServerType == DatabaseType.Oracle)
                //strSql = "select * from (" + sql + ") where rownum=1 ";
                strSql = "select * from (" + sql + ") where 1<>1 ";
            else
                strSql = "select top 1 * from (" + sql + ") TempTableName ";
            ds = persistBroker.Query(strSql);
            return ds;
        }

        /// <summary>
        /// 通过参数执行查询，返回DataSet
        /// </summary>
        /// <param name="inputParam"></param>
        /// <returns></returns>
        public virtual DataSet Execute(object[] inputParam)
        {
            return null;
        }

        /// <summary>
        /// 获取Schema
        /// </summary>
        /// <param name="inputParam"></param>
        /// <returns></returns>
        public virtual DataSet QuerySchema(object[] inputParam)
        {
            return null;
        }

    }
}
