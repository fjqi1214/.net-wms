using System;
using System.Collections.Generic;

using System.Web;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public  class Common
    {
        public static decimal Totalday(int ocCheckFinishDate, int ocCheckFinishTime, int CDate, int CTime)
        {
            if (ocCheckFinishDate == 0 || CDate == 0)
                return -1;
            DateTime dtone = FormatHelper.ToDateTime(ocCheckFinishDate, ocCheckFinishTime);
            DateTime dttwo = FormatHelper.ToDateTime(CDate, CTime);
            TimeSpan ts = dtone - dttwo;

            decimal hours = ts.Days * 24 + ts.Hours + (decimal)ts.Minutes / 60;
            int days = (int)hours / 24;
            hours -= days * 24;
            int days2 = (int)(hours / 8);
            days += days2;
            hours = hours % 8;
            decimal _hours = (decimal)hours / 8;
            return Math.Round((decimal)days + _hours, 2);
        }
    }
}