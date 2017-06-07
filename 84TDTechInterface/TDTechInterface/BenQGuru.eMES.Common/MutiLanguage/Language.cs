using System;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.PersistBroker;

namespace BenQGuru.eMES.Common.MutiLanguage
{

    public class LanguageType
    {
        public static string English = "ENU";
        public static string SimplifiedChinese = "CHS";
        public static string TraditionalChinese = "CHT";
    }

    /// <summary>
    /// language 的摘要说明。
    /// </summary>
    [Serializable, TableMap("Control", "ControlID")]
    public class LanguageWord : DomainObject
    {
        public LanguageWord()
        {
            ControlType = "";
            ControlID = "";
            ControlClass = "";
            ControlText = "";
            ControlENText = "";
            ControlCHSText = "";
            ControlCHTText = "";
        }

        // Fields
        [FieldMapAttribute("ControlType", typeof(string), 100, true)]
        public string ControlType;

        [FieldMapAttribute("ControlID", typeof(string), 100, true)]
        public string ControlID;

        [FieldMapAttribute("ControlClass", typeof(string), 100, true)]
        public string ControlClass;

        public string ControlText;

        [FieldMapAttribute("ControlENText", typeof(string), 200, true)]
        public string ControlENText;

        [FieldMapAttribute("ControlCHSText", typeof(string), 200, true)]
        public string ControlCHSText;

        [FieldMapAttribute("ControlCHTText", typeof(string), 200, true)]
        public string ControlCHTText;
    }

    public class LanguagePack
    {
        private static string _fullPath = string.Empty;
        private static LanguagePack _languagePack = null;
        private static IPersistBroker _persistBroker = null;
        private static IDomainDataProvider _domainDataProvider = null;
        private System.Collections.Hashtable _languageHashtable = null;

        public LanguagePack()
        {
        }

        private void Init()
        {
            //_persistBroker = PersistBrokerManager.PersistBroker(@"Provider=Microsoft.Jet.OLEDB.4.0;Password=;Data Source=" + this.FullPath + ";Persist Security Info=True", null);
            //add by klaus 
            _persistBroker = PersistBrokerManager.PersistBroker(@"Provider=Microsoft.ACE.OLEDB.12.0;Password=;Data Source=" + this.FullPath + ";Persist Security Info=False", null);
            //end
            _domainDataProvider = DomainDataProviderManager.DomainDataProvider(_persistBroker, null);
            _languageHashtable = new System.Collections.Hashtable();
            this.CacheLanguage();
        }

        public object[] RetriveAll()
        {
            return _domainDataProvider.CustomQuery(typeof(LanguageWord), new SQLCondition("select * from Control"));
        }

        public void CacheLanguage()
        {
            //测试多线程时添加下面的语句
            //System.Threading.Thread.Sleep(500);

            object[] objs = this.RetriveAll();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (!_languageHashtable.ContainsKey(((LanguageWord)objs[i]).ControlID.ToUpper()))
                    {
                        _languageHashtable.Add(((LanguageWord)objs[i]).ControlID.ToUpper(), objs[i]);
                    }
                }
            }
        }

        private static object objectForLockobject = new object();
        public static LanguagePack Current(string fullPath)
        {
            lock (objectForLockobject)
            {
                if (_languagePack == null)
                {
                    _languagePack = new LanguagePack();
                    _languagePack.FullPath = fullPath;
                    _languagePack.Init();
                }
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

                if (_domainDataProvider.CustomSearch(typeof(LanguageWord), new string[] { "ControlID" }, new object[] { languageObject.ControlID }) != null)
                {
                    if (language.IndexOf(LanguageType.English) != -1)
                    {
                        _domainDataProvider.CustomUpdate(languageObject, new string[] { "ControlENText" }, new object[] { languageObject.ControlENText });
                    }

                    if (language.IndexOf(LanguageType.SimplifiedChinese) != -1)
                    {
                        _domainDataProvider.CustomUpdate(languageObject, new string[] { "ControlCHSText" }, new object[] { languageObject.ControlCHSText });
                    }

                    if (language.IndexOf(LanguageType.TraditionalChinese) != -1)
                    {
                        _domainDataProvider.CustomUpdate(languageObject, new string[] { "ControlCHTText" }, new object[] { languageObject.ControlCHTText });
                    }
                }
                else
                {
                    _domainDataProvider.Insert(languageObject);
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(languageObject.ControlID + ":" + languageObject.ControlCHSText + ":" + ex.Message, ex);  
                ExceptionManager.Raise(this.GetType(), "$Error_Language_Object", string.Format("[$ControlCHSText={0}]", languageObject.ControlCHSText), ex);
            }
        }

        public LanguageWord GetLanguageWord(string keyWord, string language)
        {
            CheckLanguage(language);

            if (keyWord == null)
            {
                return null;
            }

            if (keyWord.Trim().Length == 0)
            {
                return null;
            }

            LanguageWord lw = null;
            //object[] objs = _domainDataProvider.CustomSearch(typeof(LanguageWord), new string[]{"ControlID"}, new object[]{keyWord}); 
            if (_languageHashtable.ContainsKey(keyWord.ToUpper()))
            {
                lw = (LanguageWord)_languageHashtable[keyWord.ToUpper()];

                if (language.IndexOf(LanguageType.English) != -1)
                {
                    lw.ControlText = lw.ControlENText;
                }

                if (language.IndexOf(LanguageType.SimplifiedChinese) != -1)
                {
                    lw.ControlText = lw.ControlCHSText;
                }

                if (language.IndexOf(LanguageType.TraditionalChinese) != -1)
                {
                    lw.ControlText = lw.ControlCHTText;
                }
            }
            return lw;
        }

        public string GetString(string keyWord, string language)
        {
            LanguageWord lw = this.GetLanguageWord(keyWord, language);

            if (lw == null)
            {
                return string.Empty;
            }

            return lw.ControlText;
        }

        private void CheckLanguage(string language)
        {
            if (!((language.IndexOf(LanguageType.English) != -1) || (language.IndexOf(LanguageType.SimplifiedChinese) != -1) || (language.IndexOf(LanguageType.TraditionalChinese) != -1)))
            {
                //throw new Exception("Only support language: ENU, CHS,CHT!"); 
                ExceptionManager.Raise(this.GetType(), "$Error_Language", null, null);

            }
        }
    }
}
