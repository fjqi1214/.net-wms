using System;
using System.IO;
using System.Reflection;
using BenQGuru.eMES.Common.Helper;   

namespace BenQGuru.eMES.Common.Config
{

	/// <summary>
	/// Laws Lu,2005/11/23
	/// 此类不公开使用
	/// </summary>
	[Serializable, AttributeUsage(AttributeTargets.Assembly)]
	public sealed class DomainConfiguratorAttribute : Attribute
	{
		// Fields
		private string _configFile;
		private string _configFileExtension;
		private bool _configureAndWatch;

		// Methods
		public DomainConfiguratorAttribute()
		{
			this._configFile = null;
			this._configFileExtension = null;
			this._configureAndWatch = false;
		}

		public void Configure(Assembly assembly)
		{
			
		}

		public string ConfigFileFullPath(Assembly assembly)
		{
				string text1 = null;
				if ((this._configFile == null) || (this._configFile.Length == 0))
				{
					if ((this._configFileExtension == null) || (this._configFileExtension.Length == 0))
					{
						text1 = SystemInfo.ConfigurationFileLocation;
					}
					else
					{
						if (this._configFileExtension[0] != '.')
						{
							this._configFileExtension = "." + this._configFileExtension;
						}
						text1 = Path.Combine(SystemInfo.ApplicationBaseDirectory, SystemInfo.AssemblyFileName(assembly) + this._configFileExtension);
					}
				}
				else
				{
					text1 = Path.Combine(SystemInfo.ApplicationBaseDirectory, this._configFile);
				}

				return text1;
		}

		// Properties
		public string ConfigFile
		{
			get
			{
				return this._configFile;
			}
			set
			{
				this._configFile = value;
			}
		}

		public string ConfigFileExtension
		{
			get
			{
				return this._configFileExtension;
			}
			set
			{
				this._configFileExtension = value;
			}
		}

		public bool Watch
		{
			get
			{
				return this._configureAndWatch;
			}
			set
			{
				this._configureAndWatch = value;
			}
		}

	}
}
