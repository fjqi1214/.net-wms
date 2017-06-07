using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;


//public class Op
//{
//    public Op()
//    {

//    }

//    public Op(string opCode, string opDesc, int opSeq, string opControl)
//    {
//        this.m_opCode = opCode;
//        this.m_opDesc = opDesc;
//        this.m_opSeq = opSeq;
//        this.m_opControl = opControl;
//    }

//    public Op(string opCode, string opDesc, int opSeq, string opControl, int dataType)
//    {
//        this.m_opCode = opCode;
//        this.m_opDesc = opDesc;
//        this.m_opSeq = opSeq;
//        this.m_opControl = opControl;
//        this.m_dataType = dataType;
//    }

//    private string m_opCode;

//    public string OpCode
//    {
//        get { return m_opCode; }
//        set { m_opCode = value; }
//    }

//    private string m_opDesc;

//    public string OpDesc
//    {
//        get { return m_opDesc; }
//        set { m_opDesc = value; }
//    }

//    private int m_opSeq;

//    public int OpSeq
//    {
//        get { return m_opSeq; }
//        set { m_opSeq = value; }
//    }

//    private string m_opControl;

//    public string OpControl
//    {
//        get { return m_opControl; }
//        set { m_opControl = value; }
//    }

//    private int m_dataType;

//    public int DataType
//    {
//        get { return m_dataType; }
//        set { m_dataType = value; }
//    }

//}


//public class Route
//{
//    public Route()
//    {

//    }

//    public Route(string routecode, string routename)
//    {
//        this.m_routecode = routecode;
//        this.m_routename = routename;
//    }

//    private string m_routecode;

//    public string Routecode
//    {
//        get { return m_routecode; }
//        set { m_routecode = value; }
//    }
//    private string m_routename;

//    public string Routename
//    {
//        get { return m_routename; }
//        set { m_routename = value; }
//    }
//}

public class OracleDbHelper
{
    public string connectionString;
    public OracleDbHelper(string connectionName)
    {
        connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
    }

    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public int ExecuteSql(string SQLString)
    {
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            using (OracleCommand cmd = new OracleCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.OracleClient.OracleException E)
                {
                    connection.Close();
                    throw new Exception(E.Message);
                }
            }
        }
    }

    /// <summary>
    /// 执行查询语句，返回OracleDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
    /// </summary>
    /// <param name="strSQL">查询语句</param>
    /// <returns>OracleDataReader</returns>
    public OracleDataReader ExecuteReader(string strSQL)
    {
        OracleConnection connection = new OracleConnection(connectionString);
        OracleCommand cmd = new OracleCommand(strSQL, connection);
        try
        {
            connection.Open();
            OracleDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return myReader;
        }
        catch (System.Data.OracleClient.OracleException e)
        {
            throw new Exception(e.Message);
        }

    }
    /// <summary>
    /// 执行查询语句，返回DataSet
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataSet</returns>
    public DataSet Query(string SQLString)
    {
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                command.Fill(ds);
            }
            catch (System.Data.OracleClient.OracleException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }
    }
    /// <summary>
    /// 将ds中的数据更新到数据库中
    /// </summary>
    /// <param name="ds"></param>
    public void UpdateData(DataSet ds)
    {
        string sql = "";
        string itemCode = "";
        string itemDescription = "";
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            connection.Open();
            using (OracleTransaction tran = connection.BeginTransaction())
            {
                try
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row["description"] == null)
                            row["description"] = row["code"].ToString().Trim();

                        itemCode = row["code"].ToString().Trim();
                        itemDescription = row["description"].ToString().Trim();
                        sql = "select item_code,item_description from ItemGawain where Item_code=:code";
                        using (OracleCommand cmdCheck = new OracleCommand(sql, connection))
                        {
                            cmdCheck.Transaction = tran;
                            cmdCheck.Parameters.Add(new OracleParameter(":code", itemCode));
                            OracleDataReader reader = cmdCheck.ExecuteReader(CommandBehavior.CloseConnection);
                            if (reader.Read())
                            {
                                itemDescription += "-" + reader["item_description"].ToString();
                                sql = "update ItemGawain set item_description=:description where Item_code=:code";
                                using (OracleCommand cmdUpdate = new OracleCommand(sql, connection))
                                {
                                    cmdUpdate.Transaction = tran;
                                    cmdUpdate.Parameters.Add(new OracleParameter(":code", itemCode));
                                    cmdUpdate.Parameters.Add(new OracleParameter(":description", itemDescription));
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                sql = "insert into ItemGawain values(:code,:description)";

                                using (OracleCommand cmdInsert = new OracleCommand(sql, connection))
                                {
                                    cmdInsert.Transaction = tran;
                                    cmdInsert.Parameters.Add(new OracleParameter(":code", itemCode));
                                    cmdInsert.Parameters.Add(new OracleParameter(":description", itemDescription));
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }

                        }
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                    tran.Rollback();
                }
            }
        }
    }


//    public void SaveOpList(Op[] opList,string routecode)
//    {
//        string sql = "";

//        using (OracleConnection connection = new OracleConnection(connectionString))
//        {
//            connection.Open();
//            using (OracleTransaction tran = connection.BeginTransaction())
//            {
//                try
//                {
//                    foreach (Op op in opList)
//                    {

//                        if (op.DataType == 0)
//                        {
//                            sql = string.Format(@"INSERT INTO tblroute2op
//                                              (routecode, OPCODE, opseq, opcontrol, muser, mdate, mtime)
//                                            VALUES
//                                              ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
//                                      routecode, op.OpCode, op.OpSeq, op.OpControl + "0000000000", "Gawain",                                             DateTime.Now.ToString("yyyyMMdd"), 
//                                      DateTime.Now.ToString("HHmmss"));
//                        }
//                        else if (op.DataType == 1)
//                        {
//                            sql=string.Format(@"UPDATE tblroute2op
//                                           SET 
//                                               opseq = '{2}',
//                                               opcontrol = '{3}'||substr(opcontrol,6,10),
//                                               muser = '{4}',
//                                               mdate = '{5}',
//                                               mtime = '{6}'
//                                         WHERE routecode = '{0}'
//                                           AND OPCODE = '{1}'", 
//                                             routecode, op.OpCode, op.OpSeq, op.OpControl,
//                                             "Gawain", DateTime.Now.ToString("yyyyMMdd"),
//                                      DateTime.Now.ToString("HHmmss"));
//                        }
//                        else if (op.DataType == 2)
//                        {
//                            sql = string.Format(@"delete from tblroute2op
//                                         WHERE routecode = '{0}'
//                                           AND OPCODE = '{1}'",
//                                             routecode, op.OpCode);
//                        }
//                        using (OracleCommand oraclecmd = new OracleCommand(sql, connection))
//                        {
//                            oraclecmd.Transaction = tran;
//                            oraclecmd.ExecuteNonQuery();
//                        }
                        
//                    }
//                    tran.Commit();
//                }
//                catch (Exception ex)
//                {
//                    tran.Rollback();
//                    throw ex;
                    
//                }
//            }
//        }
//    }
}

