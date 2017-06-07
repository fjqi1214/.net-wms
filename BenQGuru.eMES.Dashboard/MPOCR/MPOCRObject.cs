using System;

using BenQGuru.eMES.Common.Domain; 

namespace BenQGuru.eMES.Dashboard
{
	
	public class MPOCRObject:DomainObject
	{
		[FieldMapAttribute("mocode", typeof(string), 40,false)]
		public string  MoCode;
		[FieldMapAttribute("itemcode", typeof(string), 40,false)]
		public string  ItemCode;
		[FieldMapAttribute("moprodcutstatus", typeof(string), 40,false)]
		public string  MoProductStatus;
		[FieldMapAttribute("moplanenddate", typeof(string), 40,false)]
		public string  PlanEndDate;
		[FieldMapAttribute("planendweek", typeof(string), 40,false)]
		public string PlanEndWeek;
		[FieldMapAttribute("planendmonth", typeof(string), 40,false)]
		public string  PlanEndMonth;
		[FieldMapAttribute("moactenddate", typeof(string), 40,false)]
		public string  ActEndDate;
		[FieldMapAttribute("moactqty", typeof(string), 40,false)]
		public string  OutPutQty;
	}
	//对应XML中item对象
	public struct MoObject
	{
		public static string OnTimeShip = "OnTimeShip";
		public static string NotOnTimeShip = "NotOnTimeShip";

		public string MoProductStatus;

		public double MoCount;

		public double TotalMoCount;

		public double Scale;

		public System.Collections.SortedList DailyMos;
	}
	//对应XML中DateShip对象
	public struct DailyMo
	{
		public string CountScale;

		public int MoCount;

		public System.Collections.ArrayList MoDetails;
	}
}
