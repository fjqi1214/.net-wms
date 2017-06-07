using System;
using System.IO;

namespace Tools
{
	/// <summary>
	/// FileLog 的摘要说明。
	/// </summary>
	public class FileLog
	{
		public FileLog()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public static string FileName = "UpdateLog.txt";
		public static string TableName = "TBLUPDATELOG";

		public static void  FileLogOut(string logStr)
		{
			StreamWriter fs = null;
			try
			{
				if(!File.Exists(FileName))
				{
					File.Create(FileName);
				}
				else
				{
					try
					{
						FileInfo fi = new FileInfo(FileName);
						if(fi.Length > 2048000)
						{
							fi.Delete();
							fi.Create();
						}
					}
					catch{}
				}

				fs = File.AppendText(FileName);
				fs.WriteLine(logStr + "\t" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));	
			}
			catch
			{
			}
			finally
			{
				if(fs != null)
				{
					fs.Flush();
					fs.Close();
				}
			}
		}

		public static void  FileLogContentOut(string logStr)
		{
			StreamWriter fs = null;
			try
			{
				if(!File.Exists(FileName))
				{
					File.Create(FileName);
				}
				else
				{
					try
					{
						FileInfo fi = new FileInfo(FileName);
						if(fi.Length > 2048000)
						{
							fi.Delete();
							fi.Create();
						}
					}
					catch{}
				}

				fs = File.AppendText(FileName);
				fs.WriteLine("\t" + logStr + "\t" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));	
			}
			catch
			{
			}
			finally
			{
				if(fs != null)
				{
					fs.Flush();
					fs.Close();
				}
			}
		}

		public static void  FileLogOut(string fileName,string logStr)
		{
			StreamWriter fs = null;
			try
			{
				if(!File.Exists(fileName))
				{
					File.Create(fileName);
				}
				else
				{
					try
					{
						FileInfo fi = new FileInfo(FileName);
						if(fi.Length > 2048000)
						{
							fi.Delete();
							fi.Create();
						}
					}
					catch{}
				}
				fs = File.AppendText(fileName);
				fs.WriteLine(logStr + "\t" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));	
			}
			catch
			{
			}
			finally
			{
				if(fs != null)
				{
					fs.Flush();
					fs.Close();
				}
			}
		}

		public static void  DBLogOut(UpdateLog[] logDatas)
		{
			if(System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"] == null)
			{
				return;
			}
			string connString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].Trim();
			connString = BenQGuru.eMES.Common.Helper.EncryptionHelper.DESDecryption(connString);
			System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connString);
			System.Data.OleDb.OleDbTransaction trans = null;
			try
			{
				conn.Open();
				
				System.Data.OleDb.OleDbCommand comm = conn.CreateCommand();
				
				trans = conn.BeginTransaction();
				comm.Transaction = trans;
				foreach(UpdateLog log in logDatas)
				{
					string updateSQL = "INSERT INTO TBLUPDATELOG " 
						+ " (UPDATELOGID,FILENAME,VERSION,MACHINENAME,MACHINEIP,UPDATETIME,RESULT)"
						+ " values ('"  + log.PKID + "'"
						+ " ,'" + log.FileName + "'"
						+ " ,'" + log.Version + "'"
						+ " ,'" + log.MachineName + "'"
						+ " ,'" + log.MachineIP + "'"
						+ " ,'" + log.UpdateTime + "'"
						+ " ,'" + log.Result + "')";
					
					comm.CommandText = updateSQL;
					comm.CommandType = System.Data.CommandType.Text;
					comm.ExecuteNonQuery();		

				}
				trans.Commit();
			}
			catch(Exception ex)
			{
				if(trans != null)
				{
					trans.Rollback();
				}
			}
			finally
			{
				conn.Close();
			}
		}

		#region write off ,drop by Laws
//		public static void  DBLogOut(string tableName,UpdateLog[] logDatas)
//		{
//			if(System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"] == null)
//			{
//				return;
//			}
//			string connString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].Trim();
//			connString = BenQGuru.eMES.Common.Helper.EncryptionHelper.DESDecryption(connString);
//			System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connString);
//
//			try
//			{
//				conn.Open();
//				System.Data.OleDb.OleDbCommand comm = conn.CreateCommand();
//
//				string updateSQL = String.Empty;
//
//				updateSQL = @"
//CREATE TABLE TBLUPDATELOG(
//    UPDATELOGID    VARCHAR2(40)    NOT NULL,
//    FILENAME       VARCHAR2(40)    NOT NULL,
//    VERSION        VARCHAR2(40)    NOT NULL,
//    MACHINENAME    VARCHAR2(40)    NOT NULL,
//    MACHINEIP      VARCHAR2(40)    NOT NULL,
//    CONSTRAINT PK508 PRIMARY KEY (UPDATELOGID)
//)
//;";
//				comm.CommandText = updateSQL;
//				comm.ExecuteNonQuery();
//
//				foreach(UpdateLog log in logDatas)
//				{
//					updateSQL = "INSERT INTO TBLUPDATELOG SET" 
//						+ " UPDATELOGID = '" + log.PKID + "'"
//						+ " ,FILENAME = '" + log.FileName + "'"
//						+ " ,VERSION = '" + log.Version + "'"
//						+ " ,MACHINENAME = '" + log.MachineName + "'"
//						+ " ,MACHINEIP = '" + log.MachineIP + "'";
//						
//					comm.CommandText = updateSQL;
//					comm.ExecuteNonQuery();			
//				}
//			}
//			catch
//			{}
//			finally
//			{
//				conn.Close();
//			}
//
//		}
		#endregion

		public static string VersionFileName="Version.txt";
		public static string  GetLocalCSVersion(string fileName)
		{
			string str = String.Empty;

			if(File.Exists(fileName))
			{
				FileStream fs = File.OpenRead(fileName);
				byte[] vr = new byte[fs.Length];
				fs.Read(vr,0,vr.Length);

				fs.Close();
				str = System.Text.Encoding.Default.GetString(vr);
			}

			return str.Trim();
		}

	}
}
