using System;
using System.Collections;

using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.MutiLanguage;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// SessionHelper 的摘要说明。
	/// </summary>
	public class SessionHelper
	{	
		private System.Web.SessionState.HttpSessionState  _httpSessionState = null;
		private SessionHelper(System.Web.SessionState.HttpSessionState  httpSessionState)
		{
			_httpSessionState = httpSessionState;
		}

		public static SessionHelper Current(System.Web.SessionState.HttpSessionState  httpSessionState)
		{
			return  new SessionHelper(httpSessionState); 
		}

		public bool IsLogin
		{
			get
			{
				if (this.UserCode == null) 
				{
					return false;
				}
				return true;
			}
		}

		public string UserCode
		{
			get
			{
				if (_httpSessionState == null)
				{
					return null; 
				}

				if (_httpSessionState["$UserCode"] == null)
				{
					return null; 
				}

				return _httpSessionState["$UserCode"].ToString();
			}
			set
			{
				_httpSessionState["$UserCode"] = value;
			}
		}

		public string UserName
		{
			get
			{
				if (_httpSessionState == null)
				{
					return null; 
				}

				if (_httpSessionState["$UserName"] == null)
				{
					return null; 
				}

				return _httpSessionState["$UserName"].ToString();
			}
			set
			{
				_httpSessionState["$UserName"] = value;
			}
		}

		public string UserMail
		{
			get
			{
				if (_httpSessionState == null)
				{
					return null; 
				}

				if (_httpSessionState["$UserMail"] == null)
				{
					return null; 
				}

				return _httpSessionState["$UserMail"].ToString();
			}
			set
			{
				_httpSessionState["$UserMail"] = value;
			}
		}

		public bool IsBelongToAdminGroup
		{
			get
			{
				if (_httpSessionState == null)
				{
					return false; 
				}

				if (_httpSessionState["$IsBelongToAdminGroup"] == null)
				{
					return false; 
				}

				return bool.Parse( _httpSessionState["$IsBelongToAdminGroup"].ToString() );
			}
			set
			{
				_httpSessionState["$IsBelongToAdminGroup"] = value;
			}
		}
		
		public string Url
		{
			get
			{
				if (_httpSessionState == null)
				{
					return null; 
				}

				if (_httpSessionState["$Url"] == null)
				{
					return null; 
				}

				return _httpSessionState["$Url"].ToString();
			}
			set
			{
				_httpSessionState["$Url"] = value;
			}
		}

		public string ModuleCode
		{
			get
			{
				if (_httpSessionState == null)
				{
					return null; 
				}

				if (_httpSessionState["$ModuleCode"] == null)
				{
					return null; 
				}

				return _httpSessionState["$ModuleCode"].ToString();
			}
			set
			{
				_httpSessionState["$ModuleCode"] = value;
			}
		}

		public string Language
		{
			get
			{
				if (_httpSessionState == null)
				{
					return LanguageType.SimplifiedChinese; 
				}

				if (_httpSessionState["$Language"] == null)
				{
					return LanguageType.SimplifiedChinese; 
				}

				return (string)_httpSessionState["$Language"];
			}
			set
			{
				_httpSessionState["$Language"] = value;
			}
		}

		public Hashtable Urls
		{
			get
			{
				if (_httpSessionState == null)
				{
					return new Hashtable(); 
				}

				if (_httpSessionState["$Urls"] == null)
				{
					return new Hashtable(); 
				}

				return (Hashtable)_httpSessionState["$Urls"];
			}
			set
			{
				_httpSessionState["$Urls"] = value;
			}
		}


		public void InitMutiLanguage(ControlLibrary.Web.Language.LanguageComponent languageControl, System.Web.UI.Page page, bool forceLoad)
		{
			if ((forceLoad) || (!page.IsPostBack))
			{
				languageControl.Language = this.Language;
				languageControl.RuntimePage = page;
				languageControl.LoadLanguage(); 
			}
		}

		public void AddStoredObject(string key,object obj,bool replaced)
		{
			if( this._httpSessionState.Contents[key.ToUpper()] != null)
			{
				if( replaced )
				{
					this._httpSessionState.Remove( key.ToUpper() );
					this._httpSessionState.Add(key.ToUpper(),obj);
				}
			}
			else
			{
				this._httpSessionState.Add(key.ToUpper(),obj);
			}
		}

		public object LoadStoredObject(string key)
		{
			if( this._httpSessionState.Contents[key.ToUpper()] != null)
			{				
				return this._httpSessionState.Contents[key.ToUpper()];
			}
			return null;
		}

		public void RemoveAll()
		{
			this._httpSessionState.RemoveAll();
		}
	}
}
