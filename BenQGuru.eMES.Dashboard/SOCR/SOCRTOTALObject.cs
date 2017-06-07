using System;

using BenQGuru.eMES.Common.Domain; 

namespace BenQGuru.eMES.Dashboard
{
	
	public class SOCRTOTALObject:DomainObject
	{
		[FieldMapAttribute("shiptype", typeof(string), 40,false)]
		public string  ItemType;
		[FieldMapAttribute("planshipdate", typeof(string), 40,false)]
		public string  PlanShipDate;
		[FieldMapAttribute("ordernumber", typeof(string), 40,false)]
		public string  OrderNO;
		[FieldMapAttribute("partnercode", typeof(string), 40,false)]
		public string  PartnerCode;
		[FieldMapAttribute("itemcode", typeof(string), 40,false)]
		public string  ItemCode;
		[FieldMapAttribute("planqty", typeof(string), 40,false)]
		public string  PlanQty;
		[FieldMapAttribute("actdate", typeof(string), 40,false)]
		public string  ActDate;
		[FieldMapAttribute("planshipweek", typeof(string), 40,false)]
		public string  PlanShipWeek;
		[FieldMapAttribute("planshipmonth", typeof(string), 40,false)]
		public string  PlanShipMonth;
		[FieldMapAttribute("actqty", typeof(string), 40,false)]
		public string  ActQty;
	}
	//对应XML中item对象
	public struct TotalObject
	{
		public static string OnTimeShip = "OnTimeShip";
		public static string NotOnTimeShip = "NotOnTimeShip";

		public string ItemType;

		public double ShippedOrderCount;

		public double OrderCount;

		public double Scale;

		public System.Collections.Hashtable DailyShips;
	}
	//对应XML中DateShip对象
	public struct DailyShip
	{
		public string ActShipDate;

		public int OrderCount;

		public System.Collections.ArrayList ShipDetails;
	}
}
