using System;

namespace BenQGuru.eMES.Domain.BaseSetting
{
	public class IsSystemType
	{
		public static string System		= "0";
		public static string UserDefine	= "1";
	}

	public class IsActiveType
	{
		public static string Active		= "0";
		public static string InActive	= "1";
	}

	public class ParameterGroupType
	{
		public static  string MOType		= "MOType";
		public static  string ModelType	= "ModelType";
	}

	public class MOType
	{
		public static  string Rework		= "Rework";
		public static  string Normal			=  "Normal";
		public static  string Outsourcing =  "Outsourcing";
	}

	public class ShiftModelElement
	{
		public static  string ShiftType		= "ShiftType";
		public static  string Shift			=  "Shift";
		public static  string TimePeriod	=  "TimePeriod";
	}

	public class MOStatus
	{
		public static string Initial        = "Initial";
		public static string Release        = "Release";
		public static string Open           = "Open";
		public static string Pending        = "Pending";
		public static string Complete       = "Complete";
		public static string Close          = "Close";
	}
}
