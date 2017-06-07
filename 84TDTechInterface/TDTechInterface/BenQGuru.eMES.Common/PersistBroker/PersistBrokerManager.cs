using System;
using BenQGuru.eMES.Common.Config;

namespace BenQGuru.eMES.Common.PersistBroker
{
    /// <summary>
    /// PersistBrokerManager 的摘要说明。
    /// </summary>
    public class PersistBrokerManager
    {
        public PersistBrokerManager()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static IPersistBroker PersistBroker()
        {
            return PersistBrokerManager.PersistBroker(Config.ConfigSection.Current.DomainSetting.GetSelectedConnectString(), new System.Globalization.CultureInfo("en-US", false), false);
        }

        public static IPersistBroker PersistBroker(bool usePool)
        {
            return PersistBrokerPool.Pool(new System.Globalization.CultureInfo("en-US", false)).RetriveFromPool(usePool);
        }

        public static IPersistBroker PersistBroker(System.Globalization.CultureInfo cultureInfo)
        {
            return PersistBrokerManager.PersistBroker(Config.ConfigSection.Current.DomainSetting.GetSelectedConnectString(), cultureInfo, false);
        }

        public static IPersistBroker PersistBroker(string connectString, System.Globalization.CultureInfo cultureInfo)
        {
            return PersistBrokerManager.PersistBroker(connectString, cultureInfo, true);
        }

        public static IPersistBroker PersistBroker(string connectString, System.Globalization.CultureInfo cultureInfo, string type)
        {
            switch (type)
            {
                case "SqlPersistBroker":
                    return new BenQGuru.eMES.Common.PersistBroker.SqlPersistBroker(connectString, cultureInfo);
                case "ODBCPersistBroker":
                    return new BenQGuru.eMES.Common.PersistBroker.ODBCPersistBroker(connectString, cultureInfo);
                case "ODPPersistBroker":
                    return new BenQGuru.eMES.Common.PersistBroker.ODPPersistBroker(connectString, cultureInfo);
                default:
                    return PersistBrokerManager.PersistBroker(connectString, cultureInfo, true);
            }
        }

        protected static IPersistBroker PersistBroker(string connectString, System.Globalization.CultureInfo cultureInfo, bool isUserInit)
        {
            //added by carey.cheng on 2010-05-20 for muti db support
            if (string.IsNullOrEmpty(connectString))
                return null;
            //end added by carey.cheng on 2010-05-20 for muti db support

            if (cultureInfo == null)
            {
                cultureInfo = new System.Globalization.CultureInfo("en-US", false);
            }

            if (isUserInit)
            {
                return PersistBrokerPool.Pool(cultureInfo).RetriveFromPool(connectString);
            }
            else
            {
                return PersistBrokerPool.Pool(cultureInfo).RetriveFromPool();
            }
        }

        //连接到不同的数据库
        //add by Simone Xu
        public static IPersistBroker PersistBroker(string ConnectDB)
        {
            //added by carey.cheng on 2010-05-20 for muti db support
            PersistBrokerSetting setting = Config.ConfigSection.Current.DomainSetting.GetSetting(ConnectDB);
            if (setting == null)
                return null;
            return PersistBrokerManager.PersistBroker(setting.ConnectString, new System.Globalization.CultureInfo("en-US", false), setting.PersistBrokerType);
            //end added by carey.cheng on 2010-05-20 for muti db support
            #region marked by carey.cheng on 2010-05-19 for muti db support
            /* //marked by carey.cheng on 2010-05-19 for muti db support
             * //ConnectDB表示连接到不同的数据库
            //DBName.MES 表示连接MES的数据库连接		(Oracle)
            //DBName.SAP 表示连接SAP的数据库连接		(Oracle)
            //DBName.SPC 表示连接SPC的数据库连接		(SqlServer)
            //默认返回连接MES的数据库连接
            string conString = Config.ConfigSection.Current.DomainSetting.ConnectString;
            string type = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.PersistBrokerType;
            if (ConnectDB == BenQGuru.eMES.Common.Domain.DBName.MES)
            {
                return PersistBrokerManager.PersistBroker();
            }
            else if (ConnectDB == BenQGuru.eMES.Common.Domain.DBName.SAP)
            {
                conString = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.SAPDBConnectString;
                type = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.SAPDBPersistBrokerType;
                return PersistBrokerManager.PersistBroker(conString, new System.Globalization.CultureInfo("en-US", false), type);
            }
            else if (ConnectDB == BenQGuru.eMES.Common.Domain.DBName.SPC)
            {
                conString = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.SPCConnectString;
                type = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.SPCPersistBrokerType;
                return PersistBrokerManager.PersistBroker(conString, new System.Globalization.CultureInfo("en-US", false), type);
            }
            else if (ConnectDB == BenQGuru.eMES.Common.Domain.DBName.ERP)
            {
                conString = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.ERPConnectString;
                type = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.ERPPersistBrokerType;
                return PersistBrokerManager.PersistBroker(conString, new System.Globalization.CultureInfo("en-US", false), type);
            }
            else if (ConnectDB == BenQGuru.eMES.Common.Domain.DBName.HIS)
            {
                conString = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.HisConnectString;
                type = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.HisPersistBrokerType;
                return PersistBrokerManager.PersistBroker(conString, new System.Globalization.CultureInfo("en-US", false), type);
            }

            return PersistBrokerManager.PersistBroker();*/
            #endregion
        }

    }
}