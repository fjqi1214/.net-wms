using System;
using System.Data;
using System.Data.OleDb;


namespace BenQGuru.eMES.Common
{
	/// <summary>
	/// dblog 的摘要说明。
	/// </summary>
	public class dblog
	{
		public dblog()
		{
		}

		public void dblog1(string constr,string sqltext)
		{
			OleDbConnection thisConnection = new OleDbConnection(constr);			
			thisConnection.Open();	

			try
			{
				string sqlstring ="INSERT INTO TBLLOG  VALUES ('" + getdatestring() + "','" + sqltext.ToString().Replace("'","''").ToString() + "')";
				OleDbCommand thiscom=new OleDbCommand(sqlstring,thisConnection);
				thiscom.ExecuteNonQuery();
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				thisConnection.Close();
			}
		}


		public void dblog1(string constr,string sqltext,string exeUser)
		{
			OleDbConnection thisConnection = new OleDbConnection(constr);			
			thisConnection.Open();	

			try
			{
				string sqlstring ="INSERT INTO TBLLOG  VALUES ('" 
					+ getdatestring() + "','" 
					+ sqltext.ToString().Replace("'","''").ToString() 
					+ "','" + exeUser + 
					"')";
				OleDbCommand thiscom=new OleDbCommand(sqlstring,thisConnection);
				thiscom.ExecuteNonQuery();
				
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				thisConnection.Close();
			}
		}

		public string getdatestring()
		{
			string str= DateTime.Now.Year.ToString("0000");
			str=str+DateTime.Now.Month.ToString("00");
			str=str+DateTime.Now.Day.ToString("00");
			str=str+DateTime.Now.Hour.ToString("00");
			str=str+DateTime.Now.Minute.ToString("00");
			str=str+DateTime.Now.Second.ToString("00");
			return str.ToUpper();
		}
	}
}
