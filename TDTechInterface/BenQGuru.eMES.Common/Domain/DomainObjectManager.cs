using System;

namespace BenQGuru.eMES.Common.Domain
{
	/// <summary>
	/// DomainObjectManager 的摘要说明。
	/// </summary>
	public class DomainObjectManager
	{
		public DomainObjectManager()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static object CreateDomainObject(Type type)
		{
			return type.Assembly.CreateInstance(type.FullName); 
		}

		public static object CreateDomainObject ( Type type , System.Boolean ignoreCase)
		{
			return type.Assembly.CreateInstance(type.FullName, ignoreCase); 
		}

		public static object CreateDomainObject(Type type , System.Boolean ignoreCase , System.Reflection.BindingFlags bindingAttr , System.Reflection.Binder binder , object[] args , System.Globalization.CultureInfo culture , object[] activationAttributes)
		{
			return type.Assembly.CreateInstance( type.FullName,  ignoreCase,  bindingAttr, binder, args, culture, activationAttributes);
		}
	}
}
