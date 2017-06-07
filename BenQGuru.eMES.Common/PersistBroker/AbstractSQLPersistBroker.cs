using System;
using BenQGuru.eMES.Common.MutiLanguage;
using System.Data;
using System.Data.OleDb;
using System.Runtime.Remoting;
using System.Collections;

namespace BenQGuru.eMES.Common.PersistBroker
{
    /// <summary>
    /// 数据访问控制类
    /// </summary>
    public abstract class AbstractSQLPersistBroker : MarshalByRefObject, IPersistBroker
    {
        public bool allowSQLLog = false;
        public string sqlLogConnString = String.Empty;

        public string executeUser = "MESDefaultUser";
        public bool autoCloseConn = true;
        /// <summary>
        /// Laws Lu,2006/12/20 修改,支持手动关闭连接
        /// 是否 自动关闭连接
        /// </summary>
        public virtual bool AutoCloseConnection
        {
            get
            {
                return autoCloseConn;
            }
            set
            {
                autoCloseConn = value;
            }
        }

        /// <summary>
        /// Laws Lu,2007/04/03 是否Log用户操作
        /// </summary>
        public virtual bool AllowSQLLog
        {
            get
            {
                return allowSQLLog;
            }
            set
            {
                allowSQLLog = value;
            }
        }

        /// <summary>
        /// Laws Lu,2007/04/03 Log数据库
        /// </summary>
        public virtual string SQLLogConnectString
        {
            get
            {
                return sqlLogConnString;
            }
            set
            {
                sqlLogConnString = value;
            }
        }

        /// <summary>
        /// Laws Lu,2007/04/03 修改,允许记录当前用户
        /// 获取或者设置执行用户
        /// </summary>
        public virtual string ExecuteUser
        {
            get
            {
                return executeUser;
            }
            set
            {
                executeUser = value;
            }
        }

        private System.Globalization.CultureInfo _cultureInfo = null;
        private IDbConnection _connection = null;
        private IDbTransaction _transaction = null;
        private object _lock = new object();


        public AbstractSQLPersistBroker(IDbConnection connection)
            : this(connection, new System.Globalization.CultureInfo("en-US", false))
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public AbstractSQLPersistBroker(IDbConnection connection, System.Globalization.CultureInfo cultureInfo)
        {
            this._connection = connection;
            this._cultureInfo = cultureInfo;
        }
        //modify by benny 从protected改为public
        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        protected IDbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        public bool IsInTransaction
        {
            get
            {
                lock (_lock)
                {
                    if (this._transaction != null)
                    {
                        return true;
                    }

                    return false;
                }
            }
        }
        /// <summary>
        /// 执行SQL并返回影响行数
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <returns>影响行数</returns>
        public int ExecuteWithReturn(string commandText)
        {
            int i = 0;
            OpenConnection();
            using (IDbCommand command = this.Connection.CreateCommand())
            {
                command.CommandText = commandText;
                if (!(command is Oracle.DataAccess.Client.OracleCommand))
                    command.Transaction = this.Transaction;

#if DEBUG
                DateTime dtStart = DateTime.Now;
                string sqlText = command.CommandText;
#endif

                try
                {

                    i = command.ExecuteNonQuery();

#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif
                }
                catch (Exception e)
                {
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added

                    Log.Error(e.Message + " Text SQL:" + command.CommandText);
#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif
                    ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", e);
                }
                finally
                {
                    if (this.Transaction == null)
                    {
                        CloseConnection();
                    }
                }

                return i;
            }
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        public void Execute(string commandText)
        {
            OpenConnection();
            using (IDbCommand command = this.Connection.CreateCommand())
            {
                command.CommandText = commandText;
                if (!(command is Oracle.DataAccess.Client.OracleCommand))
                    command.Transaction = this.Transaction;
#if DEBUG
                DateTime dtStart = DateTime.Now;
                string sqlText = command.CommandText;
#endif
                try
                {

                    command.ExecuteNonQuery();

#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif
                }
                catch (System.Data.OleDb.OleDbException e)
                {
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
                    Log.Error(e.Message + " Text SQL:" + command.CommandText);
#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif

                    if (e.ErrorCode == -2147217873)
                    {
                        ExceptionManager.Raise(this.GetType(), "$ERROR_DATA_ALREADY_EXIST", e);
                    }
                    else
                    {
                        ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", e);
                    }
                }
                catch (Exception e)
                {
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
                    Log.Error(e.Message + " Text SQL:" + command.CommandText);
#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif
                    ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", e);
                }
                finally
                {
                    if (this.Transaction == null)
                    {
                        CloseConnection();
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL 语句查询并返回弱类型DataSet
        /// Laws Lu,2005/10/28,修改	缓解性能问题,改用Reader
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <returns>查询结果（DataSet）</returns>
        public DataSet Query(string commandText)
        {
            OpenConnection();
            //OleDbDataAdapter dataAdapter = (OleDbDataAdapter)this.GetDbDataAdapter();
            //using(OleDbCommand command = (OleDbCommand)this.Connection.CreateCommand()) 
            using (IDbCommand command = this.Connection.CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandTimeout = MesEnviroment.CommandTimeout;

                //				for (int i = 0; i < parameters.Length; i++)
                //				{
                //					command.Parameters.Add(parameters[i], CSharpType2OleDbType(parameterTypes[i])).Value = parameterValues[i];
                //					//					dataAdapter.SelectCommand.CommandText = ChangeParameterPerfix(dataAdapter.SelectCommand.CommandText, parameters[i]);
                //				}
                if (this.Transaction != null)
                {
                    /* modified by jessie lee, 2006-6-4 */
                    //command.Transaction = (OleDbTransaction)this.Transaction; 
                    if (!(command is Oracle.DataAccess.Client.OracleCommand))
                        command.Transaction = this.Transaction;
                }

                DataSet dataSet = new DataSet();
                /* modified by jessie lee, 2006-6-4 */
                //OleDbDataReader reader  = null;
                IDataReader reader = null;

#if DEBUG
                DateTime dtStart = DateTime.Now;
                string sqlText = command.CommandText;
#endif

                try
                {
                    //修改	在Debug模式下不允许Log日志文件


                    reader = command.ExecuteReader();
                    //command.

#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif

                    DataTable dt = new DataTable();

                    //挪到循环外面，为了没有数据的情况下也能得到Column 20090626 hiro 
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (!dt.Columns.Contains(reader.GetName(i)))
                        {
                            DataColumn dc;
                            if (reader.GetName(i).ToUpper() == "EXPORT" || reader.GetName(i).ToUpper() == "READ" || reader.GetName(i).ToUpper() == "WRITE" || reader.GetName(i).ToUpper() == "DELETE")
                            {
                                dc = new DataColumn(reader.GetName(i), typeof(bool));
                            }
                            else
                            {
                                dc = new DataColumn(reader.GetName(i), reader.GetFieldType(i));
                            }

                            dt.Columns.Add(dc);
                        }
                    }

                    //end 

                    while (reader.Read())
                    {
                        //for (int i = 0; i < reader.FieldCount; i++)
                        //{
                        //    if (!dt.Columns.Contains(reader.GetName(i)))
                        //    {
                        //        DataColumn dc = new DataColumn(reader.GetName(i), reader.GetFieldType(i));

                        //        dt.Columns.Add(dc);
                        //    }
                        //}

                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            /*
                            if(reader.GetValue(i) is String)
                            {
                                string dbValue = reader.GetValue(i).ToString();
                                dbValue = dbValue.Replace("\0","");
                                dbValue = dbValue.Replace("\r\n","");
                                dbValue = dbValue.Replace("\r","");
                                dbValue = dbValue.Replace("\n","");
                                dr[reader.GetName(i)] = dbValue ;
                            }
                            else
                            {
                                dr[reader.GetName(i)] = reader.GetValue(i);
                            }
                            */
                            if (this.Connection is Oracle.DataAccess.Client.OracleConnection)
                            {
                                object objOraVal = ((Oracle.DataAccess.Client.OracleDataReader)reader).GetOracleValue(i);
                                if (objOraVal is Oracle.DataAccess.Types.OracleString)
                                {
                                    string dbValue = objOraVal.ToString();
                                    dbValue = dbValue.Replace("\0", "");
                                    dbValue = dbValue.Replace("\r\n", "");
                                    dbValue = dbValue.Replace("\r", "");
                                    dbValue = dbValue.Replace("\n", "");
                                    dr[reader.GetName(i)] = dbValue;
                                }
                                else
                                {
                                    if ((dt.Columns[i].DataType == typeof(int) ||
                                        dt.Columns[i].DataType == typeof(decimal)) &&
                                        (objOraVal == DBNull.Value || objOraVal.ToString() == string.Empty))
                                        dr[reader.GetName(i)] = 0;
                                    else
                                    {
                                        dr[reader.GetName(i)] = objOraVal.ToString();
                                        if (objOraVal.ToString().ToUpper() == "FALSE" || objOraVal.ToString().ToUpper() == "TRUE")
                                        {
                                            dr[reader.GetName(i)] = Convert.ToBoolean(objOraVal);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (reader.GetValue(i) is String)
                                {
                                    string dbValue = reader.GetValue(i).ToString();
                                    dbValue = dbValue.Replace("\0", "");
                                    dbValue = dbValue.Replace("\r\n", "");
                                    dbValue = dbValue.Replace("\r", "");
                                    dbValue = dbValue.Replace("\n", "");
                                    dr[reader.GetName(i)] = dbValue;
                                }
                                else
                                {
                                    dr[reader.GetName(i)] = reader.GetValue(i);
                                }
                            }
                        }

                        dt.Rows.Add(dr);

                    }

                    reader.Close();
                    dataSet.Tables.Add(dt);
                    //dataSet.Tables.Add(dt);

                    //dataAdapter.Fill(dataSet);		
                }
                catch (Exception e)
                {

                    try
                    {
                        reader.Close();
                    }
                    catch { }

                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added

                    Log.Error(e.Message + " Text SQL:" + command.CommandText);

#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif

                    ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", e);


                }
                finally
                {
                    //Laws Lu,2006/12/20 修改如果自动关闭为True并且不在Transaction中时才会自动关闭Connection
                    if (this.Transaction == null && AutoCloseConnection == true)
                    {
                        //CloseConnection();
                    }
                }

                return dataSet;
            }
        }

        public abstract void Execute(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues);

        public abstract int ExecuteWithReturn(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues);

        public abstract DataSet Query(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues);

        public abstract IDbDataAdapter GetDbDataAdapter();
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void OpenConnection()
        {
            if (this.Connection != null)
            {
                if (this.Connection.State != ConnectionState.Open)
                {
                    try
                    {
#if DEBUG
                        DateTime dtStart = DateTime.Now;
#endif
                        this.Connection.Open();

#if DEBUG
                        DateTime dtEnd = DateTime.Now;
                        RecordLog("Open Connection", "", dtStart, dtEnd);
#endif

                    }
                    catch (Exception ex)
                    {
                        //						//Laws Lu,2006/12/13 if connection error ,try three times
                        //						if(ex.Message.IndexOf("ORA-12535") >= 0 || ex.Message.IndexOf("ORA-12560") >= 0 )
                        //						{
                        //							try
                        //							{
                        //								this.Connection.Open();
                        //							}
                        //							catch
                        //							{
                        //								try
                        //								{
                        //									this.Connection.Open();
                        //								}
                        //								catch(Exception exFianlly)
                        //								{
                        //									Log.Error("Open Connection Failure\t" + exFianlly.Message + "\t" + exFianlly.Source + "\t" + exFianlly.StackTrace + "\t" + exFianlly.TargetSite);
                        //								}
                        //							}
                        //						}
                        //						else
                        //						{
                        Log.Error("Open Connection Failure\t" + ex.Message + "\t" + ex.Source + "\t" + ex.StackTrace + "\t" + ex.TargetSite);
                        //						}
                        //						this.Connection.Close();
                        //						this.Connection.Open();
                    }
                }
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnection()
        {
            if (this.Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    try
                    {
#if DEBUG
                        DateTime dtStart = DateTime.Now;
#endif
                        this.Connection.Close();


#if DEBUG
                        DateTime dtEnd = DateTime.Now;
                        RecordLog("Close Connection", "", dtStart, dtEnd);
#endif
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Close Connection Failure\t" + ex.Message + "\t" + ex.Source + "\t" + ex.StackTrace + "\t" + ex.TargetSite);
                        //						this.Connection.Close();
                        //						this.Connection.Open();
                    }

                }
            }
        }

        #region ILanguage 成员

        public System.Globalization.CultureInfo CultureInfo
        {
            get
            {
                // TODO:  添加 SQLPersistBroker.CultureInfo getter 实现
                return _cultureInfo;
            }
        }

        #endregion

        #region IPersistBrokerTransaction 成员

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            lock (_lock)
            {
                if (this._transaction != null)
                {
                    //throw new RemotingException("Internal: 不支持嵌套的事务.");
                    ExceptionManager.Raise(this.GetType(), "$Error_transaction", null, null);
                }
                OpenConnection();

#if DEBUG
                DateTime dtStart = DateTime.Now;
#endif

                this._transaction = this.Connection.BeginTransaction();

#if DEBUG
                DateTime dtEnd = DateTime.Now;
                RecordLog("Begin Transaction", "", dtStart, dtEnd);
#endif
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            lock (_lock)
            {
                if (this._transaction == null)
                {
                    return;
                }

                try
                {

#if DEBUG
                    DateTime dtStart = DateTime.Now;
#endif

                    this._transaction.Rollback();

#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Rollback Transaction", "", dtStart, dtEnd);
#endif

                }
                finally
                {
                    this._transaction = null;
                }
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            lock (_lock)
            {
                if (this._transaction == null)
                {
                    return;
                }

                try
                {

#if DEBUG
                    DateTime dtStart = DateTime.Now;
#endif

                    this._transaction.Commit();

#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Commit Transaction", "", dtStart, dtEnd);
#endif
                }
                finally
                {
                    this._transaction = null;
                }
            }
        }

        #endregion

        #region IPersistBroker Members


        public abstract void ExecuteProcedure(string commandText, ref ArrayList parameters);

        #endregion

        protected virtual void RecordLog(string title, string text, DateTime start, DateTime end)
        {
            TimeSpan span = end - start;

            if (title.Trim().Length > 0)
            {
                Log.Info("****** " + title);
            }
            if (text.Trim().Length > 0)
            {
                text = text.Replace('\n', ' ');
                Log.Info("****** Text : " + text);
            }
            Log.Info("****** Start: " + start.ToString("yyyy/MM/dd HH:mm:ss") + "." + start.Millisecond.ToString("000") 
                + ", End: " + end.ToString("yyyy/MM/dd HH:mm:ss") + "." + end.Millisecond.ToString("000")
                + ", Cost: " + span.TotalMilliseconds.ToString("0.000"));
            Log.Info("");
        }
    }
}
