using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BenQGuru.eMES.PDAClient
{
    public class MultiLanguageHelper
    {
        private OleDbConnection m_Connection = null;
        private OleDbCommand m_Command = null;
        //modify by klaus 
        //private string m_ConnStr = "PROVIDER=Microsoft.Jet.OLEdb.4.0;Data Source=" + Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MyLanguage.mdb");
        //private string m_CheckUnique = "PROVIDER=Microsoft.Jet.OLEdb.4.0;Data Source=" + Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Language.mdb");
        private string m_ConnStr = "PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MyLanguage.mdb");
        private string m_CheckUnique = "PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Language.mdb");
        //end
        public MultiLanguageHelper()
        {
            m_Connection = new OleDbConnection(m_ConnStr);
            m_Command = new OleDbCommand();
        }

        public OleDbConnection GetCon()
        {
            if (m_Connection.State == ConnectionState.Closed)
                m_Connection.Open();
            return m_Connection;
        }

        public void Clear()
        {
            if (m_Connection.State == ConnectionState.Open)
                m_Connection.Close();
        }

        public OleDbCommand GetCmd()
        {
            m_Command.Connection = GetCon();
            return m_Command;
        }

        public void ExecuteNonQuery(OleDbCommand cmd)
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Clear();
            }
        }

        public void AddControlLanguage(string controlType, string controlId, string controlEn, string controlChs)
        {
            if (IsExist(controlId, m_ConnStr))
            {
                return;
            }
            if (!m_ConnStr.Equals(m_CheckUnique))
            {
                if (IsExist(controlId, m_CheckUnique))
                {
                    return;
                }
            }
            OleDbCommand cmd = GetCmd();
            DateTime dt = DateTime.Now;

            cmd.CommandText = "insert into Control values(@ConType,@ConId,@ConClass,@ConEn,@ConChs,@ConCht,'" + "Jarvis" + "','" + int.Parse(dt.ToString("yyyyMMdd")) + "','" + int.Parse(dt.ToString("HHmmss")) + "')";
            //cmd.CommandText = "insert into Control(ControlType,ControlID,ControlClass,ControlENText,ControlCHSText,ControlCHTText,MUSER,MDATE,MTIME) values(@ConType,@ConId,@ConClass,@ConEn,@ConChs,@ConCht,'" + "Jarvis" + "','" + int.Parse(dt.ToString("yyyyMMdd")) + "','" + int.Parse(dt.ToString("HHmmss")) + "')";
            OleDbParameter paratype = new OleDbParameter("@ConType", OleDbType.VarChar, 100);
            paratype.Value = controlType;
            cmd.Parameters.Add(paratype);
            OleDbParameter paraid = new OleDbParameter("@ConId", OleDbType.VarChar, 100);
            paraid.Value = controlId;

            cmd.Parameters.Add(paraid);
            OleDbParameter paraclass = new OleDbParameter("@ConClass", OleDbType.VarChar, 100);
            paraclass.Value = controlType;
            cmd.Parameters.Add(paraclass);

            OleDbParameter paraen = new OleDbParameter("@ConEn", OleDbType.VarChar, 200);
            paraen.Value = controlEn;
            cmd.Parameters.Add(paraen);
            OleDbParameter parachs = new OleDbParameter("@ConChs", OleDbType.VarChar, 200);
            parachs.Value = controlChs;
            cmd.Parameters.Add(parachs);
            OleDbParameter paracht = new OleDbParameter("@ConCht", OleDbType.VarChar, 200);
            paracht.Value = StrConv(controlChs, false);
            cmd.Parameters.Add(paracht);

            ExecuteNonQuery(cmd);
        }

        public void DeleteControlLanguage(string controlId)
        {
            OleDbCommand cmd = GetCmd();
            DateTime dt = DateTime.Now;
            cmd.CommandText = "delete from Control where ControlID = @ConId";

            OleDbParameter paraid = new OleDbParameter("@ConId", OleDbType.VarChar, 100);
            paraid.Value = controlId;
            cmd.Parameters.Add(paraid);

            ExecuteNonQuery(cmd);
        }

        public bool IsExist(string controlId, string connStr)
        {
            OleDbConnection connection = new OleDbConnection(connStr);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select * from Control where ControlID = @Id  ";
            OleDbParameter paraid = new OleDbParameter("@Id", OleDbType.VarChar, 200);
            paraid.Value = controlId;
            cmd.Parameters.Add(paraid);
            connection.Open();
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Close();
                connection.Close();
                return true;
            }
            dr.Close();
            connection.Close();
            return false;
        }

        /// <summary>   
        /// 简体繁体互转   
        /// </summary>   
        /// <param name="convStr">需要转换的字符串</param>   
        /// <param name="toSimple">是否转换为简体,true,转换为简体;false,转换为繁体</param>   
        /// <returns></returns>   
        private string StrConv(string convStr, bool toSimple)
        {
            if (String.IsNullOrEmpty(convStr))
                return "";
            return Microsoft.VisualBasic.Strings.StrConv(convStr, toSimple ? Microsoft.VisualBasic.VbStrConv.SimplifiedChinese : Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0);
        }
    }
}
