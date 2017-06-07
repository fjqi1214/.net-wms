using System;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// LoginInfo 的摘要说明。
	/// </summary>
	public class LoginInfo
	{
		private string _userCode = null;
		public string UserCode
		{
			get
			{
				return this._userCode;
			}
			set
			{
				this._userCode = value;
			}
		}

		private Resource _resource = null;
		public Resource Resource
		{
			get
			{
				return this._resource;
			}
			set
			{
				this._resource = value;
			}
		}
		
		private object[] _usergroups = null;
		public object[] UserGroups
		{
			get
			{
				return this._usergroups;
			}
			set
			{
				this._usergroups = value;
			}
		}
		
		public LoginInfo( string userCode, Resource resource ,object[] userGroups)
		{
			this._userCode = userCode;
			this._resource = resource;
			_usergroups = userGroups;
		}
	}
}
