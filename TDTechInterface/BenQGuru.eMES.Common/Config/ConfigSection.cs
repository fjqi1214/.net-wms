using System;
using System.IO;
using System.Xml;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;
using BenQGuru.eMES.Common.Helper;   

[assembly: BenQGuru.eMES.Common.Config.DomainConfigurator(ConfigFile="Domain.xml", Watch=true)]
namespace BenQGuru.eMES.Common.Config
{
	/// <summary>
	/// Laws Lu,2005/12/20
	/// 配置节
	/// </summary>
	public class ConfigSection
	{
		private static ConfigSection _configSection = null;
		private static DomainSetting _domainSetting = null;

		public ConfigSection()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 获取当前的配置节
		/// </summary>
		public static ConfigSection Current
		{
			get
			{
				if (_configSection == null)
				{
					_configSection = new ConfigSection();
					_configSection.InitDomainSetting(); 
				}
				return _configSection;
			}
		}

		private void InitDomainSetting()
		{
			string fileFullPath = this.CurrentDomainConfiguratorAttribute(SystemInfo.CallingAssembly).ConfigFileFullPath(SystemInfo.CallingAssembly); 
			_domainSetting =  new DomainSetting();
			Configure( this, new FileInfo(fileFullPath) );

            // Added by Hi1/venus.Feng on 20080813 for Hisense Version : add change NLS_LANG register
            // this.ChangeRegisterForNLS();
            // End Added
		}

        private void ChangeRegisterForNLS(string dbName)
        {
            if (this.DomainSetting.NLSDIR == string.Empty || this.DomainSetting.GetSetting(dbName).NLS == string.Empty)
            {
                return;
            }

            try
            {
                string[] dirs = this.DomainSetting.NLSDIR.Split(';');
                for (int i = 0; i < dirs.Length; i++)
                {
                    string dir = dirs[i];
                    RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(dir, true);
                    if (registryKey != null)
                    {
                        if (registryKey.GetValue("NLS_LANG", null) != null)
                        {
                            string keyValue = registryKey.GetValue("NLS_LANG") as string;
                            if (string.Compare(keyValue, this.DomainSetting.GetSetting(dbName).NLS, true) != 0)
                            {
                                registryKey.SetValue("NLS_LANG", this.DomainSetting.GetSetting(dbName).NLS);
                            }
                        }
                    }
                }                
            }
            catch
            {
                
            }
        }
		
		/// <summary>
		/// 使用这个方法的DLL,都要增加这个[assembly: BenQGuru.eMES.Common.Config.DomainConfigurator(ConfigFile="Domain.xml", Watch=true)]
		/// 如同这个DLL一样
		/// </summary>
		/// <returns></returns>
		public string ConfigFileFullPath()
		{
			return this.CurrentDomainConfiguratorAttribute(SystemInfo.CallingAssembly).ConfigFileFullPath(SystemInfo.CallingAssembly); 
		}
		private DomainConfiguratorAttribute CurrentDomainConfiguratorAttribute(System.Reflection.Assembly  assembly)
		{
			object[] objs = assembly.GetCustomAttributes(typeof(DomainConfiguratorAttribute),false);
			if (objs != null)
			{
				return (DomainConfiguratorAttribute)objs[0];
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// 获取DomainSetting
		/// </summary>
		public DomainSetting DomainSetting
		{
			get
			{
				return _domainSetting;
			}
		}
		/// <summary>
		/// 初始化配置
		/// </summary>
		/// <param name="config">配置对象</param>
		/// <param name="configFile">文件路径</param>
		public static void Configure(ConfigSection config, FileInfo configFile)
		{
			if (File.Exists(configFile.FullName))
			{
				FileStream stream1 = null;
				int num1 = 5;
				while (--num1 >= 0)
				{
					try
					{
						stream1 = configFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
						break;
					}
					catch (IOException exception1)
					{
						if (num1 == 0)
						{	
							stream1 = null;
							//throw new Exception("ConfigSection: Failed to open XML config file [" + configFile.Name + "]", exception1);
							ExceptionManager.Raise(typeof(ConfigSection),"$Error_XMLFailed",string.Format("[$configFileName={0}]", configFile.Name),exception1);
						}
						Thread.Sleep(250);
						continue;
					}
				}
				if (stream1 != null)
				{
					try
					{
						ConfigSection.Configure(config, stream1);
					}
					finally
					{
						stream1.Close();
					}
				}
			}
			else
			{
				//throw new Exception("ConfigSection: config file [" + configFile.FullName + "] not found. Configuration unchanged.");
				ExceptionManager.Raise(typeof(ConfigSection),"$Error_ConfigFile",string.Format("[$ConfigFullName={0}]", configFile.FullName),null);
			}
		}
		/// <summary>
		/// 初始化配置
		/// </summary>
		/// <param name="config">配置对象</param>
		/// <param name="configStream">字符流</param>
		public static void Configure(ConfigSection config, Stream configStream)
		{
			if (configStream == null)
			{
				//throw new Exception("ConfigSection: Configure called with null 'configStream' parameter");
				ExceptionManager.Raise(typeof(ConfigSection),"$Error_Configure_with_null",null,null);
			}
			else
			{
				XmlDocument document1 = new XmlDocument();
				try
				{
					XmlValidatingReader reader1 = new XmlValidatingReader(new XmlTextReader(configStream));
					reader1.ValidationType = ValidationType.None;
					reader1.EntityHandling = EntityHandling.ExpandEntities;
					document1.Load(reader1);
				}
				catch (Exception exception1)
				{
					document1 = null;
					//throw new Exception("ConfigSection: Error while loading XML configuration", exception1);
					ExceptionManager.Raise(typeof(ConfigSection),"$Error_while_loading_XML",null,exception1);
				}
				if (document1 == null)
				{
					return;
				}

				XmlNodeList list1 = document1.GetElementsByTagName("DomainSetting");
				if (list1.Count == 0)
				{
					//throw new Exception("ConfigSection: XML configuration does not contain a <DomainSetting> element. Configuration Aborted.");
					ExceptionManager.Raise(typeof(ConfigSection),"$Error_XML_Configuration",null,null);
				}
				else if (list1.Count > 1)
				{
					//throw new Exception("ConfigSection: XML configuration contains [" + list1.Count + "] <DomainSetting> elements. Only one is allowed. Configuration Aborted.");
					ExceptionManager.Raise(typeof(ConfigSection),"$Error_XML_Configuration_Contain",string.Format("[$listCount={0}]", list1.Count),null);
				}
				else
				{
					ConfigSection.ConfigureFromXML(config, list1[0] as XmlElement);
				}
			}
		}
		/// <summary>
		/// 初始化配置
		/// </summary>
		/// <param name="config">配置对象</param>
		/// <param name="element">XML流</param>
		public static void ConfigureFromXML(ConfigSection config, XmlElement element)
		{
			XmlNodeList list1 = element.GetElementsByTagName("DomainDataProvider");
			if (list1.Count == 0)
			{
				//throw new Exception("ConfigSection: XML configuration does not contain a <DomainDataProvider> element. Configuration Aborted.");
				ExceptionManager.Raise(typeof(ConfigSection),"$Error_XML_Configuration_DataProvider",null,null);
			}
			if (list1.Count > 1)
			{
				//throw new Exception("ConfigSection: XML configuration contains [" + list1.Count + "] <DomainDataProvider> elements. Only one is allowed. Configuration Aborted.");
               ExceptionManager.Raise(typeof(ConfigSection),"$Error_XML_Configuration_Contain_DataProvider",string.Format("[$ListCount={0}]", list1.Count), null);
			}

			if (list1[0].Attributes["Type"] == null)
			{
				//throw new Exception("ConfigSection: element <DomainDataProvider> does not contain an attribute <Type> . Configuration Aborted.");
				ExceptionManager.Raise(typeof(ConfigSection),"$Error_DomainDataProvider_Type",null,null);
			}
            config.DomainSetting.DataProviderType = list1[0].Attributes["Type"].Value;

            XmlNodeList listPersistBrokers = element.GetElementsByTagName("PersistBroker");
            if (listPersistBrokers.Count == 0)
            {
                //throw new Exception("ConfigSection: XML configuration does not contain a <PersistBroker> element. Configuration Aborted.");
                ExceptionManager.Raise(typeof(ConfigSection), "$Error_DomainDataProvider_PersistBroker", null, null);
            }
            else
            {
                foreach (XmlNode item in listPersistBrokers)
                {
                    PersistBrokerSetting setting = new PersistBrokerSetting();
                    setting.Text = item.Attributes["Text"].Value;
                    setting.Name = item.Attributes["Name"].Value;
                    setting.Default = bool.Parse(item.Attributes["Default"].Value);
                    setting.PersistBrokerType = item.Attributes["Type"].Value;
                    setting.NLS = item.Attributes["NLS"].Value;
                    setting.ConnectString = EncryptionHelper.DESDecryption(item.Attributes["ConnectString"].Value);

                    if (setting.PersistBrokerType == "ODPPersistBroker" && setting.ConnectString.ToUpper().IndexOf("PROVIDER") >= 0)
                    {
                        int i1 = setting.ConnectString.ToUpper().IndexOf("PROVIDER");
                        int i2 = setting.ConnectString.ToUpper().IndexOf(";", i1);
                        if (i2 < 0)
                            i2 = setting.ConnectString.Length - 1;
                        setting.ConnectString = setting.ConnectString.Remove(i1, i2 - i1 + 1);
                    }
                    
                    config.DomainSetting.AddSetting(setting);

                    //if (setting.Default)
                    //{
                    //    config.DomainSetting.PersistBrokerType = setting.PersistBrokerType;
                    //    config.DomainSetting.ConnectString = setting.ConnectString;
                    //}
                }
            }

            #region old flow
            /* //marked by carey.cheng on 2010-05-19 for muti db support
			XmlNodeList list2 = element.GetElementsByTagName("PersistBroker");
			if (list2.Count == 0)
			{
				//throw new Exception("ConfigSection: XML configuration does not contain a <PersistBroker> element. Configuration Aborted.");
				ExceptionManager.Raise(typeof(ConfigSection),"$Error_DomainDataProvider_PersistBroker",null,null);

			}
			if (list2.Count > 1)
			{
				//throw new Exception("ConfigSection: XML configuration contains [" + list2.Count + "] <PersistBroker> elements. Only one is allowed. Configuration Aborted.");
				ExceptionManager.Raise(typeof(ConfigSection),"$Error_DomainDataProvider_PersistBroker_Contain_More_Than_One",string.Format("[$listCount={0}]", list2.Count),null);
			}

			if (list2[0].Attributes["Type"] == null)
			{
				//throw new Exception("ConfigSection: element <PersistBroker> does not contain an attribute <Type> . Configuration Aborted.");
				ExceptionManager.Raise(typeof(ConfigSection),"$Error_DomainDataProvider_PersistBroker_Type",null,null);
			}

			if (list2[0].Attributes["ConnectString"] == null)
			{
				//throw new Exception("ConfigSection: element <PersistBroker> does not contain an attribute <ConnectString> . Configuration Aborted.");
				ExceptionManager.Raise(typeof(ConfigSection),"$Error_DomainDataProvider_PersistBroker_ConnectString",null,null);
			}

			if (list2[0].Attributes["IsPool"] == null)
			{
				config.DomainSetting.IsPool  = 0;	
			}
			else
			{
				config.DomainSetting.IsPool = config.DomainSetting.PoolSize = System.Int32.Parse (list2[0].Attributes["IsPool"].Value);
			}

			if (list2[0].Attributes["PoolSize"] == null)
			{
				config.DomainSetting.PoolSize = 1;
				//				//throw new Exception("ConfigSection: element <PersistBroker> does not contain an attribute <ConnectString> . Configuration Aborted.");
				//				ExceptionManager.Raise(typeof(ConfigSection),"$Error_DomainDataProvider_PersistBroker_PoolSize",null,null);
			}
			else
			{
				config.DomainSetting.PoolSize = System.Int32.Parse (list2[0].Attributes["PoolSize"].Value);
			}

			XmlNodeList list3 = element.GetElementsByTagName("RealTimeReportAutoRefresh");
			if (list3.Count == 0)
			{
				//throw new Exception("ConfigSection: XML configuration does not contain a <DomainDataProvider> element. Configuration Aborted.");
				//ExceptionManager.Raise(typeof(ConfigSection),"$Error_XML_Configuration_RealTimeReportAutoRefresh",null,null);
				config.DomainSetting.Interval  = 300000;
			}
			else
			{
				if(list3[0].Attributes["Seconds"] != null)
				{
					config.DomainSetting.Interval = System.Int32.Parse(list3[0].Attributes["Seconds"].Value);
				}
				else
				{
					config.DomainSetting.Interval  = 300000;
				 }
			}

			XmlNodeList list4 = element.GetElementsByTagName("DateRange");
			if (list4.Count == 0)
			{
				//throw new Exception("ConfigSection: XML configuration does not contain a <DomainDataProvider> element. Configuration Aborted.");
				//ExceptionManager.Raise(typeof(ConfigSection),"$Error_XML_Configuration_RealTimeReportAutoRefresh",null,null);
				config.DomainSetting.MaxDateRange  = 30;
			}
			else
			{
				if(list4[0].Attributes["Max"] != null)
				{
					config.DomainSetting.MaxDateRange = System.Int32.Parse(list4[0].Attributes["Max"].Value);
				}
				else
				{
					config.DomainSetting.MaxDateRange  = 30;
				}
			}
            
            
			config.DomainSetting.PersistBrokerType = list2[0].Attributes["Type"].Value;
			//sammer kong encryption
			config.DomainSetting.ConnectString = EncryptionHelper.DESDecryption(list2[0].Attributes["ConnectString"].Value);
			// Added by Icyer 2006/11/03
			if (config.DomainSetting.PersistBrokerType == "ODPPersistBroker")
			{
				if (config.DomainSetting.ConnectString.ToUpper().IndexOf("PROVIDER") >= 0)
				{
					int i1 = config.DomainSetting.ConnectString.ToUpper().IndexOf("PROVIDER");
					int i2 = config.DomainSetting.ConnectString.ToUpper().IndexOf(";", i1);
					if (i2 < 0)
						i2 = config.DomainSetting.ConnectString.Length - 1;
					config.DomainSetting.ConnectString = config.DomainSetting.ConnectString.Remove(i1, i2 - i1 + 1);
				}
			}
			// Added end

			//SPC connection string
			XmlNodeList list5 = element.GetElementsByTagName("SPCPersistBroker");
			if(list5.Count > 0)
			{
				config.DomainSetting.SPCConnectString = EncryptionHelper.DESDecryption(list5[0].Attributes["ConnectString"].Value);
				config.DomainSetting.SPCPersistBrokerType = list5[0].Attributes["Type"].Value;
				// Added by Icyer 2006/11/03
				if (config.DomainSetting.SPCPersistBrokerType == "ODPPersistBroker")
				{
					if (config.DomainSetting.SPCConnectString.ToUpper().IndexOf("PROVIDER") >= 0)
					{
						int i1 = config.DomainSetting.SPCConnectString.ToUpper().IndexOf("PROVIDER");
						int i2 = config.DomainSetting.SPCConnectString.ToUpper().IndexOf(";", i1);
						if (i2 < 0)
							i2 = config.DomainSetting.SPCConnectString.Length - 1;
						config.DomainSetting.SPCConnectString = config.DomainSetting.SPCConnectString.Remove(i1, i2 - i1 + 1);
					}
				}
				// Added end
			}

			//SAP DB connection string
			XmlNodeList listSAP = element.GetElementsByTagName("SAPDBPersistBroker");
			if(listSAP.Count > 0)
			{
				config.DomainSetting.SAPDBConnectString = EncryptionHelper.DESDecryption(listSAP[0].Attributes["ConnectString"].Value);
				config.DomainSetting.SAPDBPersistBrokerType = listSAP[0].Attributes["Type"].Value;
				// Added by Icyer 2006/11/03
				if (config.DomainSetting.SAPDBPersistBrokerType == "ODPPersistBroker")
				{
					if (config.DomainSetting.SAPDBConnectString.ToUpper().IndexOf("PROVIDER") >= 0)
					{
						int i1 = config.DomainSetting.SAPDBConnectString.ToUpper().IndexOf("PROVIDER");
						int i2 = config.DomainSetting.SAPDBConnectString.ToUpper().IndexOf(";", i1);
						if (i2 < 0)
							i2 = config.DomainSetting.SAPDBConnectString.Length - 1;
						config.DomainSetting.SAPDBConnectString = config.DomainSetting.SAPDBConnectString.Remove(i1, i2 - i1 + 1);
					}
				}
				// Added end
			}

			

			//ERP DB connection string
			XmlNodeList listERP = element.GetElementsByTagName("ODBCPersistBroker");
			if(listERP.Count > 0)
			{
				config.DomainSetting.ERPConnectString = EncryptionHelper.DESDecryption(listERP[0].Attributes["ConnectString"].Value);
				config.DomainSetting.ERPPersistBrokerType = listERP[0].Attributes["Type"].Value;
				// Added by Icyer 2006/11/03
				if (config.DomainSetting.ERPPersistBrokerType == "ODPPersistBroker")
				{
					if (config.DomainSetting.ERPConnectString.ToUpper().IndexOf("PROVIDER") >= 0)
					{
						int i1 = config.DomainSetting.ERPConnectString.ToUpper().IndexOf("PROVIDER");
						int i2 = config.DomainSetting.ERPConnectString.ToUpper().IndexOf(";", i1);
						if (i2 < 0)
							i2 = config.DomainSetting.ERPConnectString.Length - 1;
						config.DomainSetting.ERPConnectString = config.DomainSetting.ERPConnectString.Remove(i1, i2 - i1 + 1);
					}
				}
				// Added end
			}

			//His DB connection string
			XmlNodeList listHIS = element.GetElementsByTagName("HisPersistBroker");
			if(listHIS.Count > 0)
			{
				config.DomainSetting.HisConnectString = EncryptionHelper.DESDecryption(listHIS[0].Attributes["ConnectString"].Value);
				config.DomainSetting.HisPersistBrokerType = listHIS[0].Attributes["Type"].Value;
				// Added by Icyer 2006/11/03
				if (config.DomainSetting.HisPersistBrokerType == "ODPPersistBroker")
				{
					if (config.DomainSetting.HisConnectString.ToUpper().IndexOf("PROVIDER") >= 0)
					{
						int i1 = config.DomainSetting.HisConnectString.ToUpper().IndexOf("PROVIDER");
						int i2 = config.DomainSetting.HisConnectString.ToUpper().IndexOf(";", i1);
						if (i2 < 0)
							i2 = config.DomainSetting.HisConnectString.Length - 1;
						config.DomainSetting.HisConnectString = config.DomainSetting.HisConnectString.Remove(i1, i2 - i1 + 1);
					}
				}
				// Added end
			}
            */
            #endregion

            //JobClassName 导入日志类型
            XmlNodeList listJobClass = element.GetElementsByTagName("MESJobClassName");
            if (listJobClass.Count > 0)
            {
                config.DomainSetting.MESJobClassName = listJobClass[0].Attributes["Type"].Value;
            }

            // Added By Hi1/venus.feng on 20080813 for Hisense Version : Add change NLS_LANG register
            XmlNodeList listNLS = element.GetElementsByTagName("NLSRegister");
            if (listNLS.Count > 0)
            {
                if (listNLS[0].Attributes["NLSDIR"] != null)
                {
                    config.DomainSetting.NLSDIR = listNLS[0].Attributes["NLSDIR"].Value;
                }
                else
                {
                    config.DomainSetting.NLSDIR = string.Empty;
                }
                //marked by carey.cheng on 2010-05-19 for muti db support
                //if (listNLS[0].Attributes["NLS"] != null)
                //{
                //    config.DomainSetting.NLS = listNLS[0].Attributes["NLS"].Value;
                //}
                //else
                //{
                //    config.DomainSetting.NLS = string.Empty;
                //}
                //end marked by carey.cheng on 2010-05-19 for muti db support
            }

            XmlNodeList list3 = element.GetElementsByTagName("RealTimeReportAutoRefresh");
            if (list3.Count == 0)
            {
                //throw new Exception("ConfigSection: XML configuration does not contain a <DomainDataProvider> element. Configuration Aborted.");
                //ExceptionManager.Raise(typeof(ConfigSection),"$Error_XML_Configuration_RealTimeReportAutoRefresh",null,null);
                config.DomainSetting.Interval = 300000;
            }
            else
            {
                if (list3[0].Attributes["Seconds"] != null)
                {
                    config.DomainSetting.Interval = System.Int32.Parse(list3[0].Attributes["Seconds"].Value);
                }
                else
                {
                    config.DomainSetting.Interval = 300000;
                }
            }
            // End Added
		}
	}
}
