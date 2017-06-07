using System;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;

using BenQGuru.eMES.Common.MutiLanguage;
using System.Collections;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Common.PersistBroker
{
    /// <summary>
    /// Laws Lu，2004/11/22
    /// 数据访问器 支持OLE DB类型的数据提供程序
    /// </summary>
    /// 
    public class OLEDBPersistBroker : AbstractSQLPersistBroker, IPersistBroker
    {
        //		public static string ProgramBool = "false"; 
        public static string ResourceCode = String.Empty;
        public dblog db = new dblog();
        /// <summary>
        /// Laws Lu,2006/12/20 修改,支持手动关闭连接
        /// 是否 自动关闭连接
        /// </summary>
        public override bool AutoCloseConnection
        {
            get
            {
                return base.AutoCloseConnection;
            }
            set
            {
                base.AutoCloseConnection = value;
            }
        }
        /// <summary>
        /// Laws Lu,2007/04/03 修改,允许记录当前用户
        /// 获取或者设置执行用户
        /// </summary>
        public override string ExecuteUser
        {
            get
            {
                return base.ExecuteUser;
            }
            set
            {
                base.ExecuteUser = value;
            }
        }


        public OLEDBPersistBroker(string connectionString)
            : base(new OleDbConnection(connectionString))
        {

        }

        //Laws Lu,max life time to unlimited
        public override object InitializeLifetimeService()
        {
            return null;
        }

        public OLEDBPersistBroker(string connectionString, System.Globalization.CultureInfo cultureInfo)
            : base(new OleDbConnection(connectionString), cultureInfo)
        {
        }

        //		~OLEDBPersistBroker()
        //		{
        //			this.CloseConnection(); 
        //		}

        public override IDbDataAdapter GetDbDataAdapter()
        {
            return new OleDbDataAdapter();
        }

        /// <summary>
        /// 执行SQL语句，并返回影响行数
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parameters">参数名称列表</param>
        /// <param name="parameterTypes">参数类型列表</param>
        /// <param name="parameterValues">参数值列表</param>
        /// <returns>影响行数</returns>
        public override int ExecuteWithReturn(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
        {
            int iReturn = 0;
            OpenConnection();
            using (OleDbCommand command = (OleDbCommand)this.Connection.CreateCommand())
            {
                command.CommandText = this.changeParameterPerfix(commandText);

                for (int i = 0; i < parameters.Length; i++)
                {
                    command.Parameters.Add(parameters[i], CSharpType2OleDbType(parameterTypes[i])).Value = parameterValues[i];
                    //					command.CommandText = 	ChangeParameterPerfix(command.CommandText, parameters[i]);
                }
                if (this.Transaction != null)
                {
                    command.Transaction = (OleDbTransaction)this.Transaction;
                }
#if DEBUG
                DateTime dtStart = DateTime.Now;
                string sqlText = this.spellCommandText(command.CommandText, parameterValues);
#endif
                try
                {
                    //2006/09/12 新增 读取配置文件Log日志文件
                    XmlDocument xmldoc = null;
                    XmlNodeReader xr = null;
                    try
                    {
                        string constr = "";
                        if (!this.AllowSQLLog && this.SQLLogConnectString == String.Empty)
                        {
                            xmldoc = new XmlDocument();

                            xmldoc.Load(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\dblog.xml");

                            xr = new XmlNodeReader(xmldoc);

                            while (xr.Read())
                            {
                                if (xr.GetAttribute("name") == "BS")
                                {
                                    this.AllowSQLLog = (xr.ReadString() == "false" ? false : true);
                                }
                                if (xr.GetAttribute("name") == "Constr")
                                {
                                    constr = xr.ReadString();
                                }
                            }
                        }


                        if (this.AllowSQLLog && this.SQLLogConnectString != String.Empty)
                        {
                            //Laws Lu,2007/04/03 Log executing user                			
                            //db.dblog1(constr,this.spellCommandText(command.CommandText,parameterValues);
                            db.dblog1(this.SQLLogConnectString, this.spellCommandText(command.CommandText, parameterValues), this.ExecuteUser);
                        }
                    }
                    catch (Exception ex)
                    {
                        //added by leon.li @20130311
                        Log.Error(ex.StackTrace);
                        //end added
                        Log.Error(ex.Message);
                    }
                    finally
                    {
                        if (xr != null)
                        {
                            xr.Close();
                        }
                        if (xmldoc != null)
                        {
                            xmldoc.Clone();
                        }
                    }

                    iReturn = command.ExecuteNonQuery();
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
                    Log.Error(e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues));
#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif

                    if (e.ErrorCode == -2147217873)
                    {
                        //ExceptionManager.Raise(this.GetType(), "$ERROR_DATA_ALREADY_EXIST", e);
                        ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", e);
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
                    Log.Error(e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues));
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
                return iReturn;
            }
        }

        //Laws Lu,2005/10/28,修改	缓解性能问题,改用Reader
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parameters">参数名称列表</param>
        /// <param name="parameterTypes">参数类型列表</param>
        /// <param name="parameterValues">参数值列表</param>
        public override void Execute(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
        {
            OpenConnection();
            using (OleDbCommand command = (OleDbCommand)this.Connection.CreateCommand())
            {
                command.CommandText = this.changeParameterPerfix(commandText);

                for (int i = 0; i < parameters.Length; i++)
                {
                    command.Parameters.Add(parameters[i], CSharpType2OleDbType(parameterTypes[i])).Value = parameterValues[i];
                    //					command.CommandText = 	ChangeParameterPerfix(command.CommandText, parameters[i]);
                }
                if (this.Transaction != null)
                {
                    command.Transaction = (OleDbTransaction)this.Transaction;
                }

#if DEBUG
                DateTime dtStart = DateTime.Now;
                string sqlText = this.spellCommandText(command.CommandText, parameterValues);
#endif
                try
                {
                    //2006/09/12 新增 读取配置文件Log日志文件
                    XmlDocument xmldoc = null;
                    XmlNodeReader xr = null;
                    try
                    {
                        string constr = "";
                        if (!this.AllowSQLLog && this.SQLLogConnectString == String.Empty)
                        {
                            xmldoc = new XmlDocument();

                            xmldoc.Load(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\dblog.xml");

                            xr = new XmlNodeReader(xmldoc);

                            while (xr.Read())
                            {
                                if (xr.GetAttribute("name") == "BS")
                                {
                                    this.AllowSQLLog = (xr.ReadString() == "false" ? false : true);
                                }
                                if (xr.GetAttribute("name") == "Constr")
                                {
                                    this.SQLLogConnectString = xr.ReadString();
                                }
                            }
                        }


                        if (this.AllowSQLLog && this.SQLLogConnectString != String.Empty)
                        {
                            //Laws Lu,2007/04/03 Log executing user                			
                            //db.dblog1(constr,this.spellCommandText(command.CommandText,parameterValues);
                            db.dblog1(this.SQLLogConnectString, this.spellCommandText(command.CommandText, parameterValues), this.ExecuteUser);
                        }
                    }
                    catch (Exception ex)
                    {
                        //added by leon.li @20130311
                        Log.Error(ex.StackTrace);
                        //end added
                        Log.Error(ex.Message);
                    }
                    finally
                    {
                        if (xr != null)
                        {
                            xr.Close();
                        }
                        if (xmldoc != null)
                        {
                            xmldoc.Clone();
                        }
                    }


                    //修改	在Debug模式下不允许Log日志文件

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
                    Log.Error(e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues));
#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif

                    if (e.ErrorCode == -2147217873)
                    {
                        //ExceptionManager.Raise(this.GetType(), "$ERROR_DATA_ALREADY_EXIST", e);
                        ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", e);
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
                    Log.Error(e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues));
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
            }
        }
        //Laws Lu,2005/10/28,修改	缓解性能问题,改用Reader

        /// <summary>
        /// 查询，返回DataSet
        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="parameters">参数名称列表</param>
        /// <param name="parameterTypes">参数类型列表</param>
        /// <param name="parameterValues">参数值列表</param>
        /// <returns>DataSet</returns>
        public override DataSet Query(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
        {
            OpenConnection();
            //OleDbDataAdapter dataAdapter = (OleDbDataAdapter)this.GetDbDataAdapter();
            using (OleDbCommand command = (OleDbCommand)this.Connection.CreateCommand())
            {
                command.CommandTimeout = MesEnviroment.CommandTimeout;
                command.CommandText = this.changeParameterPerfix(commandText);

                for (int i = 0; i < parameters.Length; i++)
                {
                    command.Parameters.Add(parameters[i], CSharpType2OleDbType(parameterTypes[i])).Value = parameterValues[i];
                    //					dataAdapter.SelectCommand.CommandText = ChangeParameterPerfix(dataAdapter.SelectCommand.CommandText, parameters[i]);
                }
                if (this.Transaction != null)
                {
                    command.Transaction = (OleDbTransaction)this.Transaction;
                }

                DataSet dataSet = new DataSet();
                OleDbDataReader reader = null;

#if DEBUG
                DateTime dtStart = DateTime.Now;
                string sqlText = this.spellCommandText(command.CommandText, parameterValues);
#endif

                try
                {
                    reader = command.ExecuteReader();

#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif


                    DataTable dt = new DataTable();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (!dt.Columns.Contains(reader.GetName(i)))
                            {
                                DataColumn dc = new DataColumn(reader.GetName(i), reader.GetFieldType(i));

                                dt.Columns.Add(dc);
                            }
                        }

                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
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
                    Log.Error(e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues));
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

        private string changeParameterPerfix(string commandText)
        {
            return new Regex(@"\$([0-9A-Za-z_]+)").Replace(commandText, "?");
        }

        private string spellCommandText(string commandText, object[] parameterValues)
        {
            StringBuilder builder = new StringBuilder();
            string[] texts = commandText.Split('?');

            for (int i = 0; i < texts.Length; i++)
            {
                if (i < parameterValues.Length)
                {
                    builder.Append(string.Format("{0}'{1}'", texts[i], parameterValues[i]));
                }
                else
                {
                    builder.Append(texts[i]);
                }
            }

            return builder.ToString();
        }

        private System.Data.OleDb.OleDbType CSharpType2OleDbType(Type cSharpType)
        {
            if (cSharpType == typeof(string))
            {
                return OleDbType.VarChar;
            }

            if (cSharpType == typeof(int))
            {
                return OleDbType.Integer;
            }

            if (cSharpType == typeof(long))
            {
                return OleDbType.VarNumeric;
            }

            if (cSharpType == typeof(double))
            {
                return OleDbType.Double;
            }

            if (cSharpType == typeof(float))
            {
                return OleDbType.Single;
            }

            if (cSharpType == typeof(decimal))
            {
                return OleDbType.Decimal;
            }

            if (cSharpType == typeof(bool))
            {
                return OleDbType.VarChar;
            }

            if (cSharpType == typeof(DateTime))
            {
                return OleDbType.VarChar;
            }

            if (cSharpType == typeof(byte[]))
            {
                return OleDbType.Binary;
            }

            return OleDbType.IUnknown;
        }

        /// <summary>
        /// 执行Procedure
        /// </summary>
        /// <param name="parameters">参数列表</param>
        public override void ExecuteProcedure(string commandText, ref ArrayList parameters)
        {
            OpenConnection();
            using (OleDbCommand command = (OleDbCommand)this.Connection.CreateCommand())
            {
                command.CommandText = commandText;
                command.CommandType = CommandType.StoredProcedure;

                object[] parameterValues = new object[parameters.Count];
                for (int i = 0; i < parameters.Count; i++)
                {
                    OleDbParameter para = new OleDbParameter();
                    para.ParameterName = ((ProcedureParameter)parameters[i]).Name;
                    para.OleDbType = CSharpType2OleDbType(((ProcedureParameter)parameters[i]).Type);
                    if (((ProcedureParameter)parameters[i]).Length != 0)
                    {
                        para.Size = ((ProcedureParameter)parameters[i]).Length;
                    }
                    else
                    {
                        para.Size = 100;
                    }
                    if (((ProcedureParameter)parameters[i]).Direction != DirectionType.Input)
                    {
                        para.Direction = (ParameterDirection)System.Enum.Parse(typeof(ParameterDirection), System.Enum.GetName(typeof(DirectionType), ((ProcedureParameter)parameters[i]).Direction));
                    }
                    else
                    {
                        para.Direction = ParameterDirection.Input;
                    }
                    para.Value = ((ProcedureParameter)parameters[i]).Value;
                    parameterValues[i] = para;
                    command.Parameters.Add(para);
                }
                if (this.Transaction != null)
                {
                    command.Transaction = (OleDbTransaction)this.Transaction;
                }
#if DEBUG
                DateTime dtStart = DateTime.Now;
                string sqlText = this.spellCommandText(command.CommandText, parameterValues);
#endif
                try
                {
                    //2006/09/12 新增 读取配置文件Log日志文件
                    XmlDocument xmldoc = null;
                    XmlNodeReader xr = null;
                    try
                    {
                        string constr = "";
                        if (!this.AllowSQLLog && this.SQLLogConnectString == String.Empty)
                        {
                            xmldoc = new XmlDocument();

                            xmldoc.Load(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\dblog.xml");

                            xr = new XmlNodeReader(xmldoc);

                            while (xr.Read())
                            {
                                if (xr.GetAttribute("name") == "BS")
                                {
                                    this.AllowSQLLog = (xr.ReadString() == "false" ? false : true);
                                }
                                if (xr.GetAttribute("name") == "Constr")
                                {
                                    this.SQLLogConnectString = xr.ReadString();
                                }
                            }
                        }


                        if (this.AllowSQLLog && this.SQLLogConnectString != String.Empty)
                        {
                            //Laws Lu,2007/04/03 Log executing user                			
                            //db.dblog1(constr,this.spellCommandText(command.CommandText,parameterValues);
                            //db.dblog1(this.SQLLogConnectString, this.spellCommandText(command.CommandText, parameterValues), this.ExecuteUser);
                        }
                    }
                    catch (Exception ex)
                    {
                        //added by leon.li @20130311
                        Log.Error(ex.StackTrace);
                        //end added
                        Log.Error(ex.Message);
                    }
                    finally
                    {
                        if (xr != null)
                        {
                            xr.Close();
                        }
                        if (xmldoc != null)
                        {
                            xmldoc.Clone();
                        }
                    }

                    command.ExecuteNonQuery();
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (((ProcedureParameter)parameters[i]).Direction != DirectionType.Input)
                        {
                            ((ProcedureParameter)parameters[i]).Value = ((OleDbParameter)parameterValues[i]).Value;
                        }
                    }

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
                    //Log.Error(e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues));
#if DEBUG
                    DateTime dtEnd = DateTime.Now;
                    RecordLog("Execute SQL", sqlText, dtStart, dtEnd);
#endif

                    if (e.ErrorCode == -2147217873)
                    {
                        //ExceptionManager.Raise(this.GetType(), "$ERROR_DATA_ALREADY_EXIST", e);
                        ExceptionManager.Raise(this.GetType(), "$Error_Command_Execute", e);
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
                    //Log.Error(e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues));
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
                        CloseConnection();
                    }
                }
            }
        }
    }
}

