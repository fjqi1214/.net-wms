using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.Helper
{
    public static class GlobalVariables
    {
        public class CurrentOrganizations
        {
            private static List<Organization> ms_OrganizationList;
            private static string C_ORGNAME_IN_SESSION = "CurrentOrganization";

            public static void Add(Organization org)
            {
                List<Organization> tempList = OrganizationList;
                tempList.Add(org);
                OrganizationList = tempList;
            }

            public static void Clear()
            {
                OrganizationList = new List<Organization>();
            }

            public static List<Organization> GetOrganizationList()
            {
                return OrganizationList;
            }

            public static Organization First()
            {
                if (OrganizationList.Count > 0)
                {
                    return OrganizationList[0];
                }
                return null;
            }

            public static string GetSQLCondition()
            {
                string sqlCondition = "";
                foreach (Organization org in OrganizationList)
                {
                    sqlCondition += org.OrganizationID + ",";
                }
                if (sqlCondition.Length > 0)
                {
                    sqlCondition = sqlCondition.Substring(0, sqlCondition.Length - 1);

                    sqlCondition = " AND orgid IN (" + sqlCondition + ") ";
                }
                return sqlCondition;
            }

            public static string GetSQLConditionWithoutColumnName()
            {
                string sqlCondition = "";
                foreach (Organization org in OrganizationList)
                {
                    sqlCondition += org.OrganizationID + ",";
                }
                if (sqlCondition.Length > 0)
                {
                    sqlCondition = sqlCondition.Substring(0, sqlCondition.Length - 1);
                }
                return sqlCondition;
            }

            /// <summary>
            /// set or get the Organization of current user
            /// </summary>
            private static List<Organization> OrganizationList
            {
                get
                {
                    if (HttpContext.Current == null)  //CS
                    {
                        if (ms_OrganizationList == null)
                        {
                            ms_OrganizationList = new List<Organization>();
                        }
                    }
                    else //BS
                    {
                        if (HttpContext.Current.Session[C_ORGNAME_IN_SESSION] == null)
                        {
                            ms_OrganizationList = new List<Organization>();
                        }
                        else
                        {
                            ms_OrganizationList = (List<Organization>)HttpContext.Current.Session[C_ORGNAME_IN_SESSION];
                        }

                    }
                    return ms_OrganizationList;
                }
                set
                {
                    ms_OrganizationList = value;
                    if (HttpContext.Current != null)  //BS
                    {
                        HttpContext.Current.Session[C_ORGNAME_IN_SESSION] = ms_OrganizationList;
                    }
                }
            }
        }
    }
}
