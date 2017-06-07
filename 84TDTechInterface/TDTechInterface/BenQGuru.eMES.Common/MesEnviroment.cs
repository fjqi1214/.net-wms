using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Common
{
    public class MesEnviroment
    {
        private const string _DatabasePositionSession = "Enviroment.DatabasePosition";
        private const string _CommandTimeoutSession = "Enviroment.CommandTimeout";

        //modified by carey.cheng on 2010-05-20 for muti db support
        /*
        public static DBName DatabasePosition
        {
            get
            {
                DBName databasePosition = DBName.MES;

                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[_DatabasePositionSession] != null)
                {
                    databasePosition = (DBName)HttpContext.Current.Session[_DatabasePositionSession];
                }

                return databasePosition;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session[_DatabasePositionSession] = value;
                }
            }
        }*/

        public static string DatabasePosition
        {
            get
            {
                string databasePosition = string.Empty;

                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[_DatabasePositionSession] != null)
                {
                    databasePosition = HttpContext.Current.Session[_DatabasePositionSession].ToString();
                }

                return databasePosition;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session[_DatabasePositionSession] = value;
                }
            }
        }
        //end modified by carey.cheng on 2010-05-20 for muti db support


        //added by carey.cheng on 2010-05-20 for muti db support
        private static string m_LoginDB;
        public static string LoginDB
        {
            get { return m_LoginDB; }
            set { m_LoginDB = value; }
        }
        //end added by carey.cheng on 2010-05-20 for muti db support

        

        private static int _CommandTimeout;
        public static int CommandTimeout
        {
            get
            {
                //CS
                if (HttpContext.Current == null)  
                {
                    if (_CommandTimeout <= 0)
                    {
                        int timeout = 0;
                        int.TryParse(System.Configuration.ConfigurationManager.AppSettings["CommandTimeout"], out timeout);
                        if (timeout <= 0)
                        {
                            timeout = 600;
                        }
                        _CommandTimeout = timeout;
                    }
                }
                //BS
                else 
                {
                    if (HttpContext.Current.Session[_CommandTimeoutSession] == null)
                    {
                        int timeout = 0;
                        int.TryParse(System.Configuration.ConfigurationManager.AppSettings["CommandTimeout"], out timeout);
                        if (timeout <= 0)
                        {
                            timeout = 600;
                        }
                        _CommandTimeout = timeout;

                        HttpContext.Current.Session[_CommandTimeoutSession] = timeout;
                    }
                    else
                    {
                        _CommandTimeout = (int)HttpContext.Current.Session[_CommandTimeoutSession];
                    }
                }

                return _CommandTimeout;
            }
        }
    }
}
