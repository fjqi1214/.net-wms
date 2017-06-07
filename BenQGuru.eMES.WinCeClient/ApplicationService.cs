using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BenQGuru.eMES.WinCeClient
{
	/// <summary>
	/// Service 的摘要说明。
	/// </summary>
	public class ApplicationService
	{

		private static ApplicationService _applicationService = null;

		private ApplicationService()
		{
		}


        public static ApplicationService Current()
        {
            if (_applicationService == null)
            {
                _applicationService = new ApplicationService();
            }
            return _applicationService;
        }

		private LoginInfo loginInfo = null;
		public LoginInfo LoginInfo
		{
			get
			{
				return this.loginInfo;
			}
			set
			{
				this.loginInfo = value;
			}
		}

		public string UserCode
		{
			get
			{
				if ( LoginInfo == null )
				{
					return null;
				}

				return LoginInfo.UserCode;
			}
		}

		public string ResourceCode
		{
			get
			{
				if ( LoginInfo == null )
				{
					return null;
				}

				return LoginInfo.Resource;
			}
		}

        private MaterInfo materInfo = null;
        public MaterInfo MaterInfo
        {
            get
            {
                return this.materInfo;
            }
            set
            {
                this.materInfo = value;
            }
        }
        public string PickNo
        {
            get
            {
                if (MaterInfo == null)
                {
                    return null;
                }

                return MaterInfo.PickNo;
            }
        }

        public string DQMCode
        {
            get
            {
                if (MaterInfo == null)
                {
                    return null;
                }

                return MaterInfo.DQMCode;
            }
        }
	}


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

        private string _resource = null;
        public string Resource
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

        public LoginInfo(string userCode, string resource, object[] userGroups)
        {
            this._userCode = userCode;
            this._resource = resource;
            _usergroups = userGroups;
        }
    }

    public class MaterInfo
    {
        private string _pickNo = null;
        public string PickNo
        {
            get
            {
                return this._pickNo;
            }
            set
            {
                this._pickNo = value;
            }
        }
        private string _dqMCode = null;
        public string DQMCode
        {
            get
            {
                return this._dqMCode;
            }
            set
            {
                this._dqMCode = value;
            }
        }
        public MaterInfo(string PickNo, string DQMCode)
        {
            this._pickNo = PickNo;
            this._dqMCode = DQMCode;
        }

    }
}
