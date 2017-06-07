using System;
using System.Xml; 
using System.Configuration;
using System.Collections.Generic;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Helper;  

namespace BenQGuru.eMES.Common.Config
{
	/// <summary>
	/// Laws Lu,2005/12/03
	/// 配置节控制类
	/// </summary>
	public class ConfigSectionHandle:IConfigurationSectionHandler
	{
		public ConfigSectionHandle()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region IConfigurationSectionHandler 成员
		/// <summary>
		/// 创建新节点
		/// </summary>
		/// <param name="parent">父节点</param>
		/// <param name="input">输入对象</param>
		/// <param name="node">XML Node</param>
		/// <returns>新配置节</returns>
		public Object Create(Object parent, Object input, XmlNode node)
		{
			// TODO:  添加 ConfigSectionHandle.Create 实现
			return null;
		}

		#endregion
	}

    public class PersistBrokerSetting
    {
        //persist broker property
        public string Text { get; set; }
        public string Name { get; set; }
        public string PersistBrokerType { get; set; }
        public bool Default { get; set; }
        public string NLS { get; set; }
        public string ConnectString { get; set; }
    }

	/// <summary>
	/// Domain设置
	/// </summary>
	public class DomainSetting
	{
		//<DomainSetting>
		//   <DomainDataProvider  Type="">  
		//   <PersistBroker Type="" ConnectString=""/>
		//</DomainSetting>
		private 	string _dataProviderType = string.Empty; 
		private 	string _persistBrokerType = string.Empty; 
		private 	string _connectString = string.Empty; 
		private 	int _poolSize = 1; 
		private 	int _isPool = 0; 
		private     int _interval = 0;
		private     int _maxDateRange = 0;
		private		string _spcConnectString = string.Empty;
		private 	string _spcPersistBrokerType = string.Empty;
		private		string _SAPDBConnectString = string.Empty;
		private 	string _SAPDBPersistBrokerType = string.Empty;
		private 	string _MESJobClassName = string.Empty;

		private 	string _erpConnectString = string.Empty;
		private 	string _erpPersistBrokerType = string.Empty;

		private 	string _hisConnectString = string.Empty;
		private 	string _hisPersistBrokerType = string.Empty;

        private     string m_NLSDIR = string.Empty;
        private     string m_NLS = string.Empty;

        private List<PersistBrokerSetting> m_Settings = new List<PersistBrokerSetting>();
		
		public DomainSetting()
		{
		}

        public PersistBrokerSetting GetSetting(string name)
        {
            foreach (var item in m_Settings)
            {
                if (item.Name.Equals(name))
                    return item;
            }
            return null;
        }

        public void AddSetting(PersistBrokerSetting setting)
        {
            bool isExist = false;
            for (int i = 0; i < m_Settings.Count; i++)
            {
                if (m_Settings[i].Name.Equals(setting.Name))
                {
                    isExist = true;
                    m_Settings[i] = setting;
                    break;
                }
            }
            if (!isExist)
                m_Settings.Add(setting);
        }

        public List<PersistBrokerSetting> Settings
        {
            get
            {
                return m_Settings;
            }
        }


		/// <summary>
		/// 获取/设置DataProvider类型
		/// </summary>
		public string DataProviderType
		{
			get
			{
				return _dataProviderType;
			}
			set
			{
				_dataProviderType = value;
			}
        }

        /// <summary>
        /// 获取/设置PersistBroker类型
        /// </summary>
        public string PersistBrokerType
        {
            get
            {
                return _persistBrokerType;
            }
            set
            {
                _persistBrokerType = value;
            }
        }

        /// <summary>
        /// 获取/设置PoolSize（目前未使用）
        /// </summary>
        public int PoolSize
        {
            get
            {
                return _poolSize;
            }
            set
            {
                _poolSize = value;
            }
        }
        /// <summary>
        /// 获取/设置是否启用池（目前未使用）
        /// </summary>
        public int IsPool
        {
            get
            {
                return _isPool;
            }
            set
            {
                _isPool = value;
            }
        }

        /// <summary>
        /// SAP数据导入 JobClass 日志类型
        /// </summary>
        public string MESJobClassName
        {
            get
            {
                return _MESJobClassName;
            }
            set
            {
                _MESJobClassName = value;
            }
        }

        /// <summary>
        /// （目前未使用）
        /// </summary>
        public int Interval
        {
            get
            {
                return this._interval;
            }
            set
            {
                this._interval = value;
            }
        }

        /// <summary>
        /// （目前未使用）
        /// </summary>
        public int MaxDateRange
        {
            get
            {
                return this._maxDateRange;
            }
            set
            {
                this._maxDateRange = value;
            }
        }
        #region old flow
        /* //marked by carey.cheng on 2010-05-19 for muti db support
		
		/// <summary>
		/// 获取/设置ConnectionString
		/// </summary>
		public string ConnectString
		{
			get
			{
				return _connectString;
			}
			set
			{
				_connectString = value;
			}
		}
		
		
		

		/// <summary>
		/// 获取/设置SPC 连接字符串
		/// </summary>
		public string SPCConnectString
		{
			get
			{
				return _spcConnectString;
			}
			set
			{
				_spcConnectString = value;
			}
		}

		/// <summary>
		/// SPC 数据库连接类型 SQLSERVER
		/// </summary>
		public string SPCPersistBrokerType
		{
			get
			{
				return _spcPersistBrokerType;
			}
			set
			{
				_spcPersistBrokerType = value;
			}
		}

		/// <summary>
		/// SAP数据库导入连接字符串
		/// </summary>
		public string SAPDBConnectString
		{
			get
			{
				return _SAPDBConnectString;
			}
			set
			{
				_SAPDBConnectString = value;
			}
		}

		/// <summary>
		/// SAP数据库 连接类型 ORACLE OLEDB
		/// </summary>
		public string SAPDBPersistBrokerType
		{
			get
			{
				return _SAPDBPersistBrokerType;
			}
			set
			{
				_SAPDBPersistBrokerType = value;
			}
		}

		

		/// <summary>
		/// ERP 连接字符串 ODBC方式
		/// </summary>
		public string ERPConnectString
		{
			get
			{
				return _erpConnectString;
			}
			set
			{
				_erpConnectString = value;
			}
		}

		/// <summary>
		/// ERP 连接方式
		/// </summary>
		public string ERPPersistBrokerType
		{
			get
			{
				return _erpPersistBrokerType;
			}
			set
			{
				_erpPersistBrokerType = value;
			}
		}

		/// <summary>
		/// 历史库连接字符串  OLEDB
		/// </summary>
		public string HisConnectString
		{
			get
			{
				return _hisConnectString;
			}
			set
			{
				_hisConnectString = value;
			}
		}

		/// <summary>
		/// 历史库 数据库连接类型 OLEDBPersistBroker
		/// </summary>
		public string HisPersistBrokerType
		{
			get
			{
				return _hisPersistBrokerType;
			}
			set
			{
				_hisPersistBrokerType = value;
			}
		}
        */
        #endregion

        // Added By Hi1/Venus.feng on 20080813 for Hisence Version : Add register change logic       

        public string NLSDIR
        {
            get { return m_NLSDIR; }
            set { m_NLSDIR = value; }
        }

        //marked by carey.cheng on 2010-05-19 for muti db support
        //public string NLS
        //{
        //    get { return m_NLS; }
        //    set { m_NLS = value; }
        //}
        //end marked by carey.cheng on 2010-05-19 for muti db support
        
        // End Added

        //modified by carey.cheng on 2010-05-20 for muti db support
        public string GetSelectedConnectString()
        {
            //string connectString = this.GetSetting(BenQGuru.eMES.Common.Domain.DBName.MES.ToString()).ConnectString;
            string connectString = string.Empty;
            if (!string.IsNullOrEmpty(MesEnviroment.DatabasePosition))
            {
                connectString = this.GetSetting(MesEnviroment.DatabasePosition).ConnectString;
            }
            if (!string.IsNullOrEmpty(MesEnviroment.LoginDB))
            {
                connectString = this.GetSetting(MesEnviroment.LoginDB).ConnectString;
            }
            //Add by Johnson.shao on 2010-08-31
            if (string.IsNullOrEmpty(MesEnviroment.DatabasePosition) && string.IsNullOrEmpty(MesEnviroment.LoginDB))
            {
                connectString = GetDBName();
            }
            //End Added
            return connectString;
        }

        private static string GetDBName()
        {
            string returnValue = string.Empty;
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "Domain.xml";
            XmlDocument document = new XmlDocument();
            document.Load(xmlPath);
            XmlNodeList nodes = document.SelectNodes("/DomainSetting/PersistBrokers/PersistBroker");
            foreach (XmlNode item in nodes)
            {
                if (item.Attributes["Default"].Value.Trim().ToUpper() == "TRUE")
                {
                    returnValue = EncryptionHelper.DESDecryption(item.Attributes["ConnectString"].Value);
                }
            }

            return returnValue;
        }
	}
}
