using System;
using BenQGuru.eMES.Common.Domain;  
using BenQGuru.eMES.Common.DomainDataProvider ;  
using BenQGuru.eMES.Common.PersistBroker  ; 

namespace ControlLibrary.Domian
{
	/// <summary>
	/// language 的摘要说明。
	/// </summary>
	[Serializable, TableMap("Control", "ControlID")]
	public class LanguageWord: DomainObject
	{
		public LanguageWord()
		{
			ControlType = "";
			ControlID = "";
			ControlClass = "";
			ControlText  = "";
			ControlENText = "";
			ControlCHSText = "";
			ControlCHTText = "";
		}
		
		// Fields
		[FieldMapAtrribute("ControlType", typeof(string), 100, true)]
		public string ControlType;

		[FieldMapAtrribute("ControlID", typeof(string), 100, true)]
		public string ControlID;

		[FieldMapAtrribute("ControlClass", typeof(string), 100, true)]
		public string ControlClass;

		public string ControlText;

		[FieldMapAtrribute("ControlENText", typeof(string), 200, true)]
		public string ControlENText;

		[FieldMapAtrribute("ControlCHSText", typeof(string), 200, true)]
		public string ControlCHSText;

		[FieldMapAtrribute("ControlCHTText", typeof(string), 200, true)]
		public string ControlCHTText;
	}

	public class LanguagePack
	{
		private static string _fullPath = string.Empty; 
		private static LanguagePack  _languagePack = null;
		private static IPersistBroker	_persistBroker = null;
		private static IDomainDataProvider _domainDataProvider= null;

		public LanguagePack()
		{
		}

		private void Init()
		{
			_persistBroker = PersistBrokerManager.PersistBroker(@"Provider=Microsoft.Jet.OLEDB.4.0;Password=;Data Source=" + this.FullPath  + ";Persist Security Info=True", null);
			_domainDataProvider = DomainDataProviderManager.DomainDataProvider(_persistBroker, null);
		}

		public static LanguagePack Current(string fullPath) 
		{
			if(_languagePack == null)
			{
				_languagePack = new LanguagePack(); 
				_languagePack.FullPath = fullPath;
				_languagePack.Init(); 
			}	
			return _languagePack;
		}

		protected string FullPath
		{
			get
			{
				return _fullPath;
			}
			set
			{
				_fullPath = value;	
			}
		}
		public void Insert(LanguageWord languageObject, string language)
		{
			try
			{
				CheckLanguage(language);

				if (_domainDataProvider.CustomSearch(typeof(LanguageWord), new string[]{"ControlID"}, new object[]{languageObject.ControlID}) != null)
				{
					if (language.IndexOf("ENU")!= -1)
					{
						_domainDataProvider.CustomUpdate(languageObject, new string[]{"ControlENText"}, new object[]{languageObject.ControlENText });
					}
						
					if (language.IndexOf("CHS")!= -1)
					{
						_domainDataProvider.CustomUpdate(languageObject, new string[]{"ControlCHSText"}, new object[]{languageObject.ControlCHSText  });
					}

					if (language.IndexOf("CHT")!= -1)
					{
						_domainDataProvider.CustomUpdate(languageObject, new string[]{"ControlCHTText"}, new object[]{languageObject.ControlCHTText});
					}
				}
				else
				{
					_domainDataProvider.Insert(languageObject); 
				}
			}
			catch(Exception ex)
			{
				throw new Exception(languageObject.ControlID + ":" + languageObject.ControlCHSText + ":" + ex.Message, ex);  
			}
		}

		public  LanguageWord GetLanguageWord(string keyWord, string language)
		{
			CheckLanguage(language);

			LanguageWord lw =null;
			object[] objs = _domainDataProvider.CustomSearch(typeof(LanguageWord), new string[]{"ControlID"}, new object[]{keyWord}); 
			if (objs != null)
			{
				lw = (LanguageWord)objs[0];

				if (language.IndexOf("ENU")!= -1)
				{
					lw.ControlText = lw.ControlENText; 
				}
						
				if (language.IndexOf("CHS")!= -1)
				{
					lw.ControlText = lw.ControlCHSText; 
				}

				if (language.IndexOf("CHT")!= -1)
				{
					lw.ControlText = lw.ControlCHTText; 
				}
			}
			return lw;
		}

		private void CheckLanguage(string language)
		{
			if (!((language.IndexOf("ENU")!= -1) || (language.IndexOf("CHS")!= -1) || (language.IndexOf("CHT")!= -1)))
			{
				throw new Exception("Only support language: ENU, CHS,CHT!"); 
			}
		}
	}
}
