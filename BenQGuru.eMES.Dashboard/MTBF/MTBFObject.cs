using System;
using BenQGuru.eMES.Common.Domain; 

namespace BenQGuru.eMES.Dashboard
{
	
	public class MTBFQueryObject:DomainObject
	{
		[FieldMapAttribute("MODELCODE", typeof(string), 40,false)]
		public string  ModelCode;
		[FieldMapAttribute("ITEMCODE", typeof(string), 40,false)]
		public string  ItemCode;
		[FieldMapAttribute("SSCODE", typeof(string), 40,false)]
		public string  SSCode;
		[FieldMapAttribute("RCARD", typeof(string), 40,false)]
		public string  SN;
		[FieldMapAttribute("SHIFTDAY", typeof(string), 40,false)]
		public string NGDate;
		[FieldMapAttribute("WEEK", typeof(string), 40,false)]
		public string NGWeek;
		[FieldMapAttribute("MONTH", typeof(string), 40,false)]
		public string NGMonth;
		[FieldMapAttribute("BEGINTIME", typeof(string), 40,false)]
		public string begintime;
		[FieldMapAttribute("ENDTIME", typeof(string), 40,false)]
		public string endtime;
		
		public double  ManufactureTime
		{
			get
			{
				if (begintime.Length < 6)
					begintime = begintime.PadLeft(6, '0');
				if (endtime.Length < 6)
					endtime = endtime.PadLeft(6, '0');
				double dbegintime = Convert.ToDouble(begintime.Substring(0,begintime.Length-4)) + Convert.ToDouble(begintime.Substring(begintime.Length-4,2))/60;
				double dendtime = Convert.ToDouble(endtime.Substring(0,endtime.Length-4)) + Convert.ToDouble(endtime.Substring(endtime.Length-4,2))/60;

				return System.Math.Round(dendtime - dbegintime,2);
			}
		}
		
	}
	//对应XML中item对象
	public struct MTBFObject
	{
		public string FieldCode;

		public double NGQty;

		public double ManufactureTime;

		public double MTBF
		{
			get
			{
				return System.Math.Round(ManufactureTime/NGQty,2);
			}
		}

		public double TotalMTBF;

		public System.Collections.SortedList DailyMTBFs;		//Key=field
		public System.Collections.Hashtable HTDayData;		//记录已经统计的查询对象 Key＝field ＋ shiftday

	}
	//对应XML中DateShip对象
	public struct SubMTBF
	{
		public string Date;

		public string FieldCode;

		public int NGQty;

		public double ManufactureTime;

		public double MTBF
		{
			get
			{
				return System.Math.Round(ManufactureTime/NGQty,2);
			}
		}

		public System.Collections.ArrayList MTBFDetails;
		public System.Collections.Hashtable HTDetailDayData;		//记录已经统计的查询对象 Key＝field + Date ＋ shiftday ,Date为天，周，月
	}
}
