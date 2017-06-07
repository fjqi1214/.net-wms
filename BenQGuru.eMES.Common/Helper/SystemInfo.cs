using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;

namespace BenQGuru.eMES.Common.Helper
{
	public sealed class SystemInfo
	{
		// Methods
		static SystemInfo()
		{
			SystemInfo.EmptyTypes = new Type[0];
		}

		private SystemInfo()
		{
		}

		public static string AssemblyFileName(Assembly myAssembly)
		{
			return Path.GetFileName(myAssembly.Location);
		}

		public static string AssemblyLocationInfo(Assembly myAssembly)
		{
			string text1;
			if (myAssembly.GlobalAssemblyCache)
			{
				return "Global Assembly Cache";
			}
			try
			{
				text1 = myAssembly.Location;
			}
			catch (SecurityException)
			{
				text1 = "Location Permission Denied";
			}
			return text1;
		}

		public static string AssemblyQualifiedName(Type type)
		{
			return (type.FullName + ", " + type.Assembly.FullName);
		}

		public static string AssemblyShortName(Assembly myAssembly)
		{
			string text1 = myAssembly.FullName;
			int num1 = text1.IndexOf(',');
			if (num1 > 0)
			{
				text1 = text1.Substring(0, num1);
			}
			return text1.Trim();
		}

		public static Type GetTypeFromString(string typeName, bool throwOnError, bool ignoreCase)
		{
			return SystemInfo.GetTypeFromString(Assembly.GetCallingAssembly(), typeName, throwOnError, ignoreCase);
		}

		public static Type GetTypeFromString(Assembly relativeAssembly, string typeName, bool throwOnError, bool ignoreCase)
		{
			if (typeName.IndexOf(',') == -1)
			{
				return relativeAssembly.GetType(typeName, throwOnError, ignoreCase);
			}
			return Type.GetType(typeName, throwOnError, ignoreCase);
		}

		public static Type GetTypeFromString(Type relativeType, string typeName, bool throwOnError, bool ignoreCase)
		{
			return SystemInfo.GetTypeFromString(relativeType.Assembly, typeName, throwOnError, ignoreCase);
		}

		public static Guid NewGuid()
		{
			return Guid.NewGuid();
		}


		// Properties
		public static string ApplicationBaseDirectory
		{
			get
			{
				return AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		public static string ApplicationFriendlyName
		{
			get
			{
				if (SystemInfo._appFriendlyName == null)
				{
					try
					{
						SystemInfo._appFriendlyName = AppDomain.CurrentDomain.FriendlyName;
					}
					catch (SecurityException)
					{
						//LogLog.Debug("SystemInfo: Security exception while trying to get current domain friendly name. Error Ignored.");
					}
					if ((SystemInfo._appFriendlyName == null) || (SystemInfo._appFriendlyName.Length == 0))
					{
						try
						{
							SystemInfo._appFriendlyName = Path.GetFileName(SystemInfo.EntryAssemblyLocation);
						}
						catch (SecurityException)
						{
						}
					}
					if ((SystemInfo._appFriendlyName == null) || (SystemInfo._appFriendlyName.Length == 0))
					{
						SystemInfo._appFriendlyName = "NOT AVAILABLE";
					}
				}
				return SystemInfo._appFriendlyName;
			}
		}

		public static string ConfigurationFileLocation
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
		}

		public static int CurrentThreadId
		{
			get
			{
				return AppDomain.GetCurrentThreadId();
			}
		}

		public static System.Reflection.Assembly   CallingAssembly
		{
			get
			{
				return Assembly.GetCallingAssembly();
			}
		}

		public static string EntryAssemblyLocation
		{
			get
			{
				return Assembly.GetEntryAssembly().Location;
			}
		}

		public static string HostName
		{
			get
			{
				if (SystemInfo._hostName == null)
				{
					try
					{
						SystemInfo._hostName = Dns.GetHostName();
					}
					catch
					{
					}
					if ((SystemInfo._hostName == null) || (SystemInfo._hostName.Length == 0))
					{
						try
						{
							SystemInfo._hostName = Environment.MachineName;
						}
						catch
						{
						}
					}
					if ((SystemInfo._hostName == null) || (SystemInfo._hostName.Length == 0))
					{
						SystemInfo._hostName = "NOT AVAILABLE";
					}
				}
				return SystemInfo._hostName;
			}
		}

		public static string NewLine
		{
			get
			{
				return Environment.NewLine;
			}
		}


		// Fields
		public static readonly Type[] EmptyTypes;
		private static string _appFriendlyName;
		private static string _hostName;
	}
}
