using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using System.Runtime.Remoting;

[assembly: log4net.Config.DOMConfigurator(ConfigFile="log4net.dll.log4net", Watch=true)]
namespace BenQGuru.eMES.Common.DCT.Core
{
	public class Log 
	{
		private static ILog Logger;
	    private static string configFile = "log4net.dll.log4net";

		static Log()
		{
			if(File.Exists(Log4NetConfigFile))
			{
				if(Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					DOMConfigurator.ConfigureAndWatch(new FileInfo(Log4NetConfigFile));
				}
				else
				{
					DOMConfigurator.Configure(new FileInfo(Log4NetConfigFile));
				}
			}
			else
			{
				BasicConfigurator.Configure();
			}
			Logger = GetLogger(typeof(Log));
		}

		#region Abbributes
		public static string Log4NetConfigFile
		{
			get
			{
				return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),configFile);
			}
		}

		public static ILog GetLogger(System.Type type)
		{
			return LogManager.GetLogger(type);
		}
		#endregion

		#region wrapper log type
		public static void Error(string message)
		{
			try
			{
				Logger.Error(message);
			}
			catch(Exception e)
			{
				ExceptionManager.Raise(typeof(Log),"$Error_Log_Fail",string.Format("Logger.Error() failed on logging '{0}'.", message ), e);
			}
		}

		public static void Warning(string message,Exception exception)
		{
			try
			{
				Logger.Warn(message,exception);
			}
			catch(Exception e)
			{
				ExceptionManager.Raise(typeof(Log),"$Error_Log_Warn",String.Format("Logger.Warn() failed on logging '{0}'.", message +"::"+ exception.Message ), e);
			}
		}

		public static void Warning(string message)
		{
			try
			{
				Logger.Warn(message);
			}
			catch(Exception e)
			{
				ExceptionManager.Raise(typeof(Log),"$Error_Log_Warn",String.Format("Logger.Warn() failed on logging '{0}'.", message), e);
			}
		}

		public static void Fatal(string message,Exception exception)
		{
			try
			{
				Logger.Fatal(message,exception);
			}
			catch(Exception e)
			{
				ExceptionManager.Raise(typeof(Log),"$Error_Log_Fatal",String.Format("Logger.Fatal() failed on logging '{0}'.", message +"::"+ exception.Message), e);
			}
		}

		public static void Fatal(string message)
		{
			try
			{
				Logger.Fatal(message);
			}
			catch(Exception e)
			{
				ExceptionManager.Raise(typeof(Log),"$Error_Log_Fatal",String.Format("Logger.Fatal() failed on logging '{0}'.", message), e);
			}
		}

		public static void Info(string message,Exception exception)
		{
			try
			{
				Logger.Info(message,exception);
			}
			catch(Exception e)
			{
				ExceptionManager.Raise(typeof(Log),"$Error_Log_Info",String.Format("Logger.Info() failed on logging '{0}'.", message +"::"+ exception.Message), e);
			}
		}

		public static void Info(string message)
		{
			try
			{
				Logger.Info(message);
			}
			catch(Exception e)
			{
				ExceptionManager.Raise(typeof(Log),"$Error_Log_Info",String.Format("Logger.Info() failed on logging '{0}'.", message), e);
			}
		}

		
		#endregion
	}

	/// <summary>
	/// Author:
	///		sammer kong
	///	Date :
	///		2005/04/15
	///	Description:
	/// 这个的设计思路是为了统一处理异常的抛出，以及记录
	/// 这边可以根据type信息中的namespace来决定采取那个类型的exception，避免后期的改动
	/// 同时根据不同的方法来表达不同情况下的一场信息
	/// </summary>
	public class ExceptionManager
	{
//		private static ExceptionManager s_exMgr = null;
//		public static ExceptionManager Instance()
//		{
//			if( ExceptionManager.s_exMgr == null )
//			{
//				ExceptionManager.s_exMgr = new ExceptionManager();
//			}
//			return ExceptionManager.s_exMgr;
//		}

		public static void Raise(Type type,string errorCode)
		{
			ExceptionManager.Raise(type,errorCode,"",null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="errorCode"></param>
		/// <param name="innerException"></param>
		public static void Raise(Type type,string errorCode,string message)
		{
			ExceptionManager.Raise(type,errorCode,message,null);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="errorCode"></param>
		/// <param name="innerException"></param>
		public static void Raise(Type type,string errorCode,Exception innerException)
		{
			ExceptionManager.Raise(type,errorCode,"",innerException);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="message">rd自己定义的一些信息</param>
		/// <param name="innerException"></param>
		public static void Raise(Type type,string errorCode,string message,Exception innerException)
		{
			if ( message == null )
			{
				message = string.Empty;
			}

			if ( innerException == null )
			{
				throw new RemotingException(string.Format("{0}{1}",errorCode,message ));
			}
			
			throw new RemotingException(string.Format("{0}{1}",errorCode,message ),innerException);
		}

//		/// <summary>
//		/// 这个方法通常用来表达复杂信息
//		/// 例如工单状态变更的时候，需要抛出异常告诉用户信息，可以设定这样调用
//		/// ExceptionCenter.Raise(this.GetType(),MOModel,new string[]{mocode,mostatus},new object[]{"mo1","released"});
//		/// </summary>
//		/// <param name="type"></param>
//		/// <param name="domainObject">领域类</param>
//		/// <param name="propertys">属性列表</param>
//		/// <param name="values">与属性对性的值</param>
//		/// <param name="innerException"></param>
//		public static void Raise(Type type,string errorCode,string message,DomainObject domainObject,string[] propertys,object[] values,Exception innerException)
//		{
//			string[] prots = null;
//			if( propertys != null &&
//				values != null &&
//				propertys.Length == values.Length)
//			{
//				prots = new string[propertys.Length];
//				for(int i=0;i<propertys.Length;i++)
//				{
//					prots[i] = domainObject.GetType().ToString() + "." + propertys[i];
//				}
//			}
//
//			ExceptionManager.Raise(type,errorCode,message,prots,values,innerException);
//		}
//
//		/// <summary>
//		/// 如果这个异常信息需要跨越单个domainobject,则使用这个方法。
//		/// 例如，ExceptionCenter.Raise(this.GetType(),new string[]{MOModel.mocode,Item.ItemCode},new object[]{"mo1","item1"});
//		/// </summary>
//		/// <param name="type"></param>
//		/// <param name="propertys">全名称，例如Mo.MoCode</param>
//		/// <param name="values">对应于名称的值</param>
//		/// <param name="innerException"></param>
//		public static void Raise(Type type,string errorCode,string message,string[] propertys,object[] values,Exception innerException)
//		{			
//			string exceptionMessage = "";
//			if( propertys != null &&
//				values != null &&
//				propertys.Length == values.Length )
//			{
//				for(int i=0;i<propertys.Length;i++)
//				{
//					exceptionMessage += propertys[i] + ":" + values[i].ToString() +"\n";
//				}
//			}
//			else
//			{
//				exceptionMessage += "$Error_ExceptionMessage_Null";
//			}
//			exceptionMessage = string.Format("{0}\n{\n {1}\n {2}\n }",errorCode,message,exceptionMessage );
//
//			throw new RemotingException(exceptionMessage,innerException);
//		}
	}
}
