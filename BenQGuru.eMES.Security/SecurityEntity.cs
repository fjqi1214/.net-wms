using System;
using System.Runtime.Remoting;
using System.Collections;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;


namespace BenQGuru.eMES.Security
{
	/// <summary>
	/// UserSecurityEntity 的摘要说明。
	/// 权限建立步骤
	/// 1。通过用户找到用户归属的用户组
	/// 2。通过用户组找到改组拥有的权限，目前只有模块部分
	/// 3。解析该用户组在该模块中的操作权限
	/// 4。将各个用户组中的权限合并，形成用户的权限
	/// </summary>
	public class UserSecurityEntity
	{
		private IDomainDataProvider _domainDataProvider = null;
		public IDomainDataProvider DataProvider
		{
			get
			{
				if (_domainDataProvider == null)
				{
					_domainDataProvider = DomainDataProviderManager.DomainDataProvider(); 
				}

				return _domainDataProvider;
			}	
		}

		private User _user = null;
		private Set _userGroupSecurityEntities = new Set();
		/// <summary>
		/// 各个用户组的权限
		/// </summary>
		public Set UserGroupSecurityEntities
		{
			get
			{
				return this._userGroupSecurityEntities;
			}
		}

		/// <summary>
		/// 合并后用户的权限
		/// </summary>
		private Set _securitySet = new Set();
		public Set SecuritySet
		{
			get
			{
				return this._securitySet;
			}
		}

		#region Constructor
		public UserSecurityEntity(User user) : this(user,null)
		{
		}
		public UserSecurityEntity(string userCode) : this(userCode,null)
		{			
		}

		public UserSecurityEntity(User user,IDomainDataProvider domainDataProvider)
		{			
			this._domainDataProvider = domainDataProvider;
			this._user = user;

			this._loadUserGroupSecurityEntity();

			this._mergeSecurity();
		}

		public UserSecurityEntity(string userCode,IDomainDataProvider domainDataProvider)
		{		
			this._domainDataProvider = domainDataProvider;
			UserFacade manager = new UserFacade(this.DataProvider);
			this._user = manager.GetUser( userCode ) as User;
			if( this._user == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_User_Not_Exist");	
			}

			this._loadUserGroupSecurityEntity();

			this._mergeSecurity();
		}
		#endregion

		public ModuleSecurityEntity GetModuleSecurityByCode(string moduleCode)
		{
			ModuleSecurityEntity moduleSecurityEntity = null;
			foreach(ModuleSecurityEntity module in this._securitySet)
			{
				if( module.TheModule.ModuleCode.ToUpper() == moduleCode.ToUpper() )
				{
					moduleSecurityEntity = module;
					break;
				}
			}
			return moduleSecurityEntity;
		}

		public ModuleSecurityEntity GetModuleSecurityByUrl(string requestUrl)
		{
			ModuleSecurityEntity moduleSecurityEntity = null;
			foreach(ModuleSecurityEntity module in this._securitySet)
			{
				if( module.TheModule.FormUrl.ToUpper() == requestUrl.ToUpper() )
				{
					moduleSecurityEntity = module;
					break;
				}
			}
			return moduleSecurityEntity;
		}

		/// <summary>
		/// 获得各个用户的权限
		/// </summary>
		private void _loadUserGroupSecurityEntity()
		{
			UserFacade manager = new UserFacade(this.DataProvider);
			object[] userGroups = manager.GetUserGroupofUser( this._user.UserCode );	//获得用户归属的用户组
			if( userGroups != null )
			{
				foreach(UserGroup userGroup in userGroups)
				{
					//加入用户组安全实体
					this._userGroupSecurityEntities.Add(
						new UserGroupSecurityEntity( userGroup ) );
				}
			}
		}

		/// <summary>
		/// 合并各个用户组中的权限
		/// </summary>
		private void _mergeSecurity()
		{
			//遍历各个用户组
			foreach(UserGroupSecurityEntity userGroupSecurityEntity in this._userGroupSecurityEntities)
			{
				//遍历该用户中的模块权限实体
				foreach(ModuleSecurityEntity moduleSecurityEntity1 in userGroupSecurityEntity.Modules)
				{
					bool isIn = false;
					//在已经合并好的模块权限实体中处理
					foreach(ModuleSecurityEntity moduleSecurityEntity2 in this._securitySet)
					{
						if( moduleSecurityEntity1.Equals(moduleSecurityEntity2) )	//该模块权限实体已经存在
						{		
							//合并操作权限，并集
							moduleSecurityEntity2.Read = moduleSecurityEntity1.Read || moduleSecurityEntity2.Read;
							moduleSecurityEntity2.Write = moduleSecurityEntity1.Read || moduleSecurityEntity2.Write;
							moduleSecurityEntity2.Delete = moduleSecurityEntity1.Delete || moduleSecurityEntity2.Delete;
							moduleSecurityEntity2.Export = moduleSecurityEntity1.Export || moduleSecurityEntity2.Export;

							isIn = true;
							break;
						}						
					}
					if( !isIn )
					{
						this._securitySet.Add( moduleSecurityEntity1 );
					}
				}
			}
		}
	}


	public class UserGroupSecurityEntity
	{	
		private IDomainDataProvider _domainDataProvider = null;
		public IDomainDataProvider DataProvider
		{
			get
			{
				if (_domainDataProvider == null)
				{
					_domainDataProvider = DomainDataProviderManager.DomainDataProvider(); 
				}

				return _domainDataProvider;
			}	
		}

		private Set _moduleSet = new Set();
		public Set Modules
		{
			get
			{
				return this._moduleSet;
			}
		}

		private UserGroup _userGroup = null;
		public UserGroup TheUserGroup
		{
			get
			{
				return this._userGroup;
			}
		}		

		#region Constructor
		public UserGroupSecurityEntity(UserGroup userGroup) : this(userGroup,null)
		{	
			this._userGroup = userGroup;
		}

		public UserGroupSecurityEntity(string userGroupCode) : this(userGroupCode,null)
		{
			UserFacade manager = new UserFacade(this.DataProvider);
			this._userGroup = manager.GetUserGroup( userGroupCode ) as UserGroup;
			if( this._userGroup == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_User_Group_Not_Exist");	
			}
		}

		public UserGroupSecurityEntity(UserGroup userGroup,IDomainDataProvider domainDataProvider)
		{	
			this._domainDataProvider = domainDataProvider;
			this._userGroup = userGroup;
			this._loadModule();
		}

		public UserGroupSecurityEntity(string userGroupCode,IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			UserFacade manager = new UserFacade(this.DataProvider);
			this._userGroup = manager.GetUserGroup( userGroupCode ) as UserGroup;
			if( this._userGroup == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_User_Group_Not_Exist");	
			}
			this._loadModule();
		}
		#endregion

		private void _loadModule()
		{
			SystemSettingFacade facade = new SystemSettingFacade(this.DataProvider);
			//取得该用户组享有的所有模块，只有代码信息和操作权限
			object[] relations = facade.GetUserGroup2ModuleByUserGroup( this._userGroup.UserGroupCode );
			if( relations != null )
			{				
				foreach(UserGroup2Module relation in relations)
				{
					object obj = facade.GetModule( relation.ModuleCode );	////取得各个模块的实体
					if( obj != null && obj is Module)
					{
						//加入模块的权限实体
						this._moduleSet.Add( new ModuleSecurityEntity(obj as Module,relation.ViewValue) );						
					}
				}
			}
		}		
	}


	public class ModuleSecurityEntity
	{
		private IDomainDataProvider _domainDataProvider = null;
		public IDomainDataProvider DataProvider
		{
			get
			{
				if (_domainDataProvider == null)
				{
					_domainDataProvider = DomainDataProviderManager.DomainDataProvider(); 
				}

				return _domainDataProvider;
			}	
		}
		private Module _module = null;
		public Module TheModule
		{
			get
			{
				return this._module;
			}
		}

		private bool _export = false;
		public bool Export
		{
			get
			{
				return true;
			}
			set
			{
				this._export = value;
			}
		}

		private bool _read = false;
		public bool Read
		{
			get
			{
				return true;
			}
			set
			{
				this._read = value;
			}
		}

		private bool _write = false;
		public bool Write
		{
			get
			{
				return true;
			}
			set
			{
				this._write = value;
			}
		}

		private bool _delete = false;
		public bool Delete
		{
			get
			{
				return true;
			}
			set
			{
				this._delete = value;
			}
		}
		private string _viewRights = "";

		public ModuleSecurityEntity(Module module,string viewRights) : this(module,viewRights,null)
		{
		}

		public ModuleSecurityEntity(string moduleCode,string viewRights) : this(moduleCode,viewRights,null)
		{
		}

		public ModuleSecurityEntity(Module module,string viewRights,IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._module = module;
			this._viewRights = viewRights;

			this._distributeViewRights();
		}

		public ModuleSecurityEntity(string moduleCode,string viewRights,IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._viewRights = viewRights;
			SystemSettingFacade facade = new SystemSettingFacade(this.DataProvider);
			object module = facade.GetModule( moduleCode );
			if( module == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_Module_Not_Exist");	
			}
			this._distributeViewRights();
		}

		private const char HasRightChar = '1';
		private const char NoRightChar = '0';

		private void _distributeViewRights()
		{
			//write			
			this._write = ( this._viewRights[2] == HasRightChar );			

			//delete
			this._delete = ( this._viewRights[3] == HasRightChar );

			//read ，拥有任何一个权限，都有了读权限
			this._read = ( this._viewRights[1] == HasRightChar ) ||
						this._write  ||
						this._export  ||
						this._delete;
			//export
			this._export = ( this._viewRights[4] == HasRightChar );
		}
	}
}
