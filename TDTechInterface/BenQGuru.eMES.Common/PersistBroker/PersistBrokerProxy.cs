using System;
using System.Data; 
using BenQGuru.eMES.Common;


namespace BenQGuru.eMES.Common.PersistBroker
{
//	/// <summary>
//	/// PersistBroker 的代理类
//	/// </summary>
//	public class PersistBrokerProxy
//	{
//		private  IPersistBroker _persistBroker = null;
//		private static readonly log4net.ILog  log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
//		
//		/// <summary>
//		/// PersistBrokerProxy 的构造函数
//		/// </summary>
//		public PersistBrokerProxy(): this(TestConfig.DALConnectionString)
//		{
//		}
//
//		/// <summary>
//		/// PersistBrokerProxy 的构造函数
//		/// </summary>
//		/// <param name="connectionString">connectionString</param>
//		public PersistBrokerProxy(string connectionString, System.Globalization.CultureInfo  cultureInfo)
//		{
//			_persistBroker  = PersistBrokerManager.PersistBroker(); 
//		}
//
//
//
//		/// <summary>
//		/// Begin Transaction
//		/// </summary>
//		public void BeginTransaction()
//		{ 
//			try
//			{
//				this.RealPersistBroker.BeginTrans();  
//				_transactionId = Token.GetTokenId(); 
//				LogInfo(string.Format(loyoutString, "transaction", _sessionId, _transactionId,0,"Start", ""));
//			}
//			catch(Exception ex)
//			{
//				LogError(string.Format(loyoutString, "transaction", _sessionId, _transactionId,0,ex.Message, ""));
//				_transactionId =_nonTransaction;
//				throw(ex);
//			}
//		}
//
//		/// <summary>
//		/// Commit Transaction
//		/// </summary>
//		public void CommitTransaction()
//		{
//			try
//			{
//				if (RealPersistBroker.CheckInTransaction())
//				{
//					this.RealPersistBroker.CommitTrans();  
//					LogInfo(string.Format(loyoutString, "transaction", _sessionId, _transactionId,0,"Commit", ""));
//				}
//			}
//			catch(Exception ex)
//			{
//				LogError(string.Format(loyoutString, "transaction", _sessionId, _transactionId,0,ex.Message, ""));
//				
//				throw(ex);
//			}
//			finally
//			{
//				_transactionId = _nonTransaction;
//			}
//		}
//
//		/// <summary>
//		/// Close Connection
//		/// </summary>
//		public void CloseConnection()
//		{
//			if (this.RealPersistBroker!=null)
//			{
//				this.RealPersistBroker.Close(true); 
//			}
//		}
//
//
//		/// <summary>
//		/// Rollback Transaction
//		/// </summary>
//		public void RollbackTransaction()
//		{
//			try
//			{
//				if (RealPersistBroker.CheckInTransaction())
//				{
//					this.RealPersistBroker.RollbackTrans(); 
//					LogInfo(string.Format(loyoutString, "transaction", _sessionId, _transactionId,0,"rollback", ""));
//				}
//			}
//			catch(Exception ex)
//			{
//				LogError(string.Format(loyoutString, "transaction", _sessionId, _transactionId,0,ex.Message, ""));
//				throw(ex);
//			}
//			finally
//			{
//				_transactionId = _nonTransaction;
//			}
//		}
//
//		/// <summary>
//		/// Execute
//		/// </summary>
//		/// <param name="sql">SQL</param>
//		public  void Execute(string sql)
//		{
//			DateTime dt= Token.GetToken();
//			try
//			{
//				this.RealPersistBroker.ExecuteNonQuery(sql); 
//				LogInfo(string.Format(loyoutString, "execute    ", _sessionId, _transactionId, Token.GetTokenDiff(dt),"excute", sql));
//			}
//			catch(Exception ex)
//			{
//				LogError(string.Format(loyoutString, "execute    ", _sessionId, _transactionId, Token.GetTokenDiff(dt), ex.Message , sql));
//				throw(ex);
//			}
//		}
//
//		/// <summary>
//		/// Query
//		/// </summary>
//		/// <param name="sql">SQL</param>
//		/// <returns></returns>
//		public DataSet Query(string sql)
//		{
//			DateTime dt= Token.GetToken();
//			try
//			{
//				DataSet ds= this.RealPersistBroker.ExecuteDataset(sql);  
//				LogInfo(string.Format(loyoutString, "query      ", _sessionId, _transactionId, Token.GetTokenDiff(dt) ,"query", sql));
//				return ds;
//			}
//			catch(Exception ex)
//			{
//				LogError(string.Format(loyoutString, "query      ", _sessionId, _transactionId, Token.GetTokenDiff(dt), ex.Message , sql));
//				throw(ex);
//			}
//		}
//
//		/// <summary>
//		/// PersistBroker 的代理类
//		/// </summary>
//		/// <returns>实际的PersistBroker</returns>
//		protected IPersistBroker RealPersistBroker
//		{
//			get
//			{
//				if (this._persistBroker == null)
//				{
//					_persistBroker = PersistBrokerManager.PersistBroker();
//				}
//				return this._persistBroker;
//			}
//		}
//	}
//
//
//
//	/// <summary>
//	/// 
//	/// </summary>
//	public class Session
//	{
//		/// <summary>
//		/// 
//		/// </summary>
//		private string _sessionId = string.Empty; 
//		/// <summary>
//		/// 
//		/// </summary>
//		public string SessionId
//		{
//			get
//			{
//				return _sessionId;
//			}
//		}
//	}
//
//	/// <summary>
//	/// 
//	/// </summary>
//	internal class Token
//	{
//		/// <summary>
//		/// 
//		/// </summary>
//		public Token()
//		{
//		}
//
//		/// <summary>
//		/// 
//		/// </summary>
//		/// <returns></returns>
//		public static DateTime GetToken()
//		{
//			return System.DateTime.Now;  
//		}
//
//		/// <summary>
//		/// 
//		/// </summary>
//		/// <param name="dateTime"></param>
//		/// <returns></returns>
//		public static int GetTokenDiff(DateTime dateTime)
//		{
//			return System.DateTime.Compare(System.DateTime.Now, dateTime);  
//		}
//
//		/// <summary>
//		/// 
//		/// </summary>
//		/// <returns></returns>
//		public static string GetTokenId()
//		{
//			DateTime dataTime = DateTime.Now; 
//			return string.Format("{0}{1}{2}{3}{4}{5}", dataTime.Year, dataTime.DayOfYear, dataTime.Hour, dataTime.Minute, dataTime.Second, dataTime.Millisecond);  
//		}
//	}
}
