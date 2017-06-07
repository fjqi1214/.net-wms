using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Report;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WebQuery
{
    public class ReportHelpher
    {
        private IDomainDataProvider _domainDataProvider = null;

        public ReportHelpher(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        #region 日期处理相关函数

        public int GetWeekByDate(int date)
        {
            int returnValue = 0;

            string sql = "SELECT dweek FROM tbltimedimension WHERE ddate = " + date.ToString() + " ";

            object[] domainObejct = this.DataProvider.CustomQuery(typeof(TimeDimension), new SQLCondition(sql));
            if (domainObejct != null)
            {
                returnValue = ((TimeDimension)domainObejct[0]).Week;
            }

            return returnValue;
        }

        public void GetAdjustYear(int adjust, ref string yearString)
        {
            int year = (yearString == null || yearString == "") ? 2000 : int.Parse(yearString);

            year += adjust;

            yearString = year.ToString();
        }

        public void GetAdjustMonth(int adjust, ref string yearString, ref string monthString)
        {
            int year = (yearString == null || yearString == "") ? 2000 : int.Parse(yearString);
            int month = (monthString == null || monthString == "") ? 1 : int.Parse(monthString);

            year += (month + adjust - 1) / 12;
            month = (month + adjust - 1) % 12 + 1;

            yearString = year.ToString();
            monthString = month.ToString();
        }

        public void GetAdjustWeek(int adjust, ref string yearString, ref string weekString)
        {
            int year = (yearString == null || yearString == "") ? 2000 : int.Parse(yearString);
            int week = (weekString == null || weekString == "") ? 1 : int.Parse(weekString);

            if (week + adjust >= 1 && week + adjust <= 52)
            {
                weekString = Convert.ToString(week + adjust);
                return; 
            }

            //出现跨年的情况才查数据库
            string sql = string.Empty;
            object[] obj = null;
            string dayString = string.Empty;

            sql = "SELECT ddate FROM tbltimedimension WHERE year = " + year.ToString() + " AND dweek = " + week.ToString() + " ORDER BY ddate DESC ";
            obj = this.DataProvider.CustomQuery(typeof(TimeDimension), new SQLCondition(sql));
            if (obj != null)
            {
                dayString = ((TimeDimension)obj[0]).Date.ToString();
                dayString = dayString.Substring(0, 4) + "-" + dayString.Substring(4, 2) + "-" + dayString.Substring(6, 2);
                DateTime temp = DateTime.Parse(dayString);
                temp = temp.AddDays(adjust * 7);
                dayString = temp.ToString("yyyyMMdd");

                sql = "SELECT year, dweek FROM tbltimedimension WHERE ddate = " + dayString + " ";
                obj = this.DataProvider.CustomQuery(typeof(TimeDimension), new SQLCondition(sql));
                if (obj != null)
                {
                    yearString = ((TimeDimension)obj[0]).Year.ToString();
                    weekString = ((TimeDimension)obj[0]).Week.ToString();
                }
            }
        }

        public void GetAdjustDay(int adjust, ref string dayString)
        {
            if (dayString == null)
            {
                dayString = "2000-01-01";
            }

            DateTime temp = DateTime.Parse(dayString.Substring(0, 4) + "-" + dayString.Substring(4, 2) + "-" + dayString.Substring(6, 2));
            temp = temp.AddDays(adjust);

            dayString = temp.ToString("yyyyMMdd");
        }

        public string RoundDateToBegin(string byTimeType, DateTime date)
        {
            string filed = string.Empty;
            DateTime smallestDate = date;

            switch (byTimeType)
            {
                case NewReportByTimeType.Year:
                    filed = "year";
                    smallestDate = smallestDate.AddYears(-1);
                    break;

                case NewReportByTimeType.Month:
                    filed = "dmonth";
                    smallestDate = smallestDate.AddMonths(-1);
                    break;

                case NewReportByTimeType.Week:
                    smallestDate = smallestDate.AddDays(-7);
                    filed = "dweek";
                    break;

                default:
                    smallestDate = smallestDate.AddDays(-1);
                    filed = "ddate";
                    break;
            }

            string sql = "SELECT MIN(ddate) AS ddate FROM tbltimedimension WHERE " + filed + " IN ";
            sql += "(SELECT " + filed + " FROM tbltimedimension WHERE ddate = " + date.ToString("yyyyMMdd") + " ) ";
            sql += "AND ddate >= " + smallestDate.ToString("yyyyMMdd") + " ";

            object[] obj = this.DataProvider.CustomQuery(typeof(TimeDimension), new SQLCondition(sql));
            if (obj != null)
            {
                if (((TimeDimension)obj[0]).Date > 0)
                {
                    return ((TimeDimension)obj[0]).Date.ToString();
                }
            }

            return date.ToString("yyyyMMdd");
        }

        public string RoundDateToEnd(string byTimeType, DateTime date)
        {
            string filed = string.Empty;
            DateTime biggestDate = date;

            switch (byTimeType)
            {
                case NewReportByTimeType.Year:
                    filed = "year";
                    biggestDate = biggestDate.AddYears(1);
                    break;

                case NewReportByTimeType.Month:
                    filed = "dmonth";
                    biggestDate = biggestDate.AddMonths(1);
                    break;

                case NewReportByTimeType.Week:
                    biggestDate = biggestDate.AddDays(7);
                    filed = "dweek";
                    break;

                default:
                    biggestDate = biggestDate.AddDays(1);
                    filed = "ddate";
                    break;
            }

            string sql = "SELECT MAX(ddate) AS ddate FROM tbltimedimension WHERE " + filed + " IN ";
            sql += "(SELECT " + filed + " FROM tbltimedimension WHERE ddate = " + date.ToString("yyyyMMdd") + " ) ";
            sql += "AND ddate <= " + biggestDate.ToString("yyyyMMdd") + " ";

            object[] obj = this.DataProvider.CustomQuery(typeof(TimeDimension), new SQLCondition(sql));
            if (obj != null)
            {
                if (((TimeDimension)obj[0]).Date > 0)
                {
                    return ((TimeDimension)obj[0]).Date.ToString();
                }
            }

            return date.ToString("yyyyMMdd");
        }

        #endregion
    }
}
