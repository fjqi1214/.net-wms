using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for SystemSetting
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2005-04-29 10:36:31
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.BaseSetting
{

	#region Menu 菜单
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLMENU", "MENUCODE")]
	public class Menu : DomainObject
	{
		public Menu()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MENUCODE", typeof(string), 40, true)]
		public string  MenuCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MENUDESC", typeof(string), 100, false)]
		public string  MenuDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDLCODE", typeof(string), 40, false)]
		public string  ModuleCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MENUSEQ", typeof(decimal), 10, true)]
		public decimal  MenuSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PMENUCODE", typeof(string), 40, false)]
		public string  ParentMenuCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MENUTYPE", typeof(string), 40, true)]
		public string  MenuType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("VISIBILITY", typeof(string), 40, true)]
		public string  Visibility;

	}
	#endregion

	#region Module 功能模块
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLMDL", "MDLCODE")]
	public class Module : DomainObject
	{
		public Module()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDLCODE", typeof(string), 40, true)]
		public string  ModuleCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDLDESC", typeof(string), 100, false)]
		public string  ModuleDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDLSEQ", typeof(decimal), 10, true)]
		public decimal  ModuleSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDLHFNAME", typeof(string), 100, false)]
		public string  ModuleHelpFileName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ISACTIVE", typeof(string), 1, true)]
		public string  IsActive;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ISSYS", typeof(string), 1, true)]
		public string  IsSystem;

		/// <summary>
		/// C/S  B/S
		/// </summary>
		[FieldMapAttribute("MDLTYPE", typeof(string), 40, true)]
		public string  ModuleType;

		/// <summary>
		/// Alpha/Beta/Release
		/// </summary>
		[FieldMapAttribute("MDLSTATUS", typeof(string), 40, true)]
		public string  ModuleStatus;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FORMURL", typeof(string), 100, false)]
		public string  FormUrl;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDLVER", typeof(string), 40, false)]
		public string  ModuleVersion;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PMDLCODE", typeof(string), 40, false)]
		public string  ParentModuleCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ISRESTRAIN", typeof(string), 1, false)]
		public string  IsRestrain;

	}
	#endregion

	#region Parameter 参数
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSYSPARAM", "PARAMCODE,PARAMGROUPCODE")]
	public class Parameter : DomainObject
	{
		public Parameter()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMCODE", typeof(string), 40, true)]
		public string  ParameterCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMDESC", typeof(string), 100, false)]
		public string  ParameterDescription;

		/// <summary>
		/// 1 -  使用中
		/// 0 -  正常
		/// 
		/// </summary>
		[FieldMapAttribute("ISACTIVE", typeof(string), 1, true)]
		public string  IsActive;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 1  系统
		/// 0  用户
		/// </summary>
		[FieldMapAttribute("ISSYS", typeof(string), 1, true)]
		public string  IsSystem;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMALIAS", typeof(string), 40, false)]
		public string  ParameterAlias;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMVALUE", typeof(string), 100, false)]
		public string  ParameterValue;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMGROUPCODE", typeof(string), 40, true)]
		public string  ParameterGroupCode;

		/// <summary>
		/// sequence 参数顺序
		/// </summary>
		[FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
		public string  ParameterSequence;

		/// <summary>
		/// 父级参数代码
		/// </summary>
		[FieldMapAttribute("PARENTPARAM", typeof(string), 40, false)]
		public string  ParentParameterCode;

	}
	#endregion

	#region ParameterGroup 参数组
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSYSPARAMGROUP", "PARAMGROUPCODE")]
	public class ParameterGroup : DomainObject
	{
		public ParameterGroup()
		{
		}
 
		/// <summary>
		/// Static/System Parameter/ User Parameter
		/// </summary>
		[FieldMapAttribute("PARAMGROUPTYPE", typeof(string), 40, true)]
		public string  ParameterGroupType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMGROUPDESC", typeof(string), 100, false)]
		public string  ParameterGroupDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ISSYS", typeof(string), 1, true)]
		public string  IsSystem;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMGROUPCODE", typeof(string), 40, true)]
		public string  ParameterGroupCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region User 用户
	/// <summary>
	/// 系统用户
	/// </summary>
	[Serializable, TableMap("TBLUSER", "USERCODE")]
	public class User : DomainObject
	{
		public User()
		{
		}
 
		/// <summary>
		/// 用户名
		/// </summary>
		[FieldMapAttribute("USERNAME", typeof(string), 40, false)]
		public string  UserName;

		/// <summary>
		/// 用户密码
		/// </summary>
		[FieldMapAttribute("USERPWD", typeof(string), 40, true)]
		public string  UserPassword;

		/// <summary>
		/// 电话号码
		/// </summary>
		[FieldMapAttribute("USERTEL", typeof(string), 40, false)]
		public string  UserTelephone;

		/// <summary>
		/// 电子信箱
		/// </summary>
		[FieldMapAttribute("USEREMAIL", typeof(string), 100, false)]
		public string  UserEmail;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]

		public string  MaintainUser;

		/// <summary>
		/// 用户所在的部门
		/// 来自用户系统参数的定义, 系统参数类型 USERDEPART
		/// </summary>
		[FieldMapAttribute("USERDEPART", typeof(string), 40, false)]
		public string  UserDepartment;

		/// <summary>
		/// 用户代码
		/// </summary>
		[FieldMapAttribute("USERCODE", typeof(string), 40, true)]
		public string  UserCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		[FieldMapAttribute("USERSTAT", typeof(string), 40, true)]
		public string  UserStatus;
	}
	#endregion

	#region UserGroup 用户组
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLUSERGROUP", "USERGROUPCODE")]
	public class UserGroup : DomainObject
	{
		public UserGroup()
		{
		}
 
		/// <summary>
		/// 用户组描述
		/// </summary>
		[FieldMapAttribute("USERGROUPDESC", typeof(string), 100, false)]
		public string  UserGroupDescription;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 用户组代码
		/// </summary>
		[FieldMapAttribute("USERGROUPCODE", typeof(string), 40, true)]
		public string  UserGroupCode;

		/// <summary>
		/// 用户组类别
		/// 来自系统参数的定义, 系统参数类型 USERGROUPTYPE
		/// 1. Administrator
		/// 2. Guest
		/// </summary>
		[FieldMapAttribute("USERGROUPTYPE", typeof(string), 40, true)]
		public string  UserGroupType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region UserGroup2Module
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLUSERGROUP2MODULE", "MDLCODE,USERGROUPCODE")]
	public class UserGroup2Module : DomainObject
	{
		public UserGroup2Module()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDLCODE", typeof(string), 40, true)]
		public string  ModuleCode;

		/// <summary>
		/// 用户组代码
		/// </summary>
		[FieldMapAttribute("USERGROUPCODE", typeof(string), 40, true)]
		public string  UserGroupCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("VIEWVALUE", typeof(string), 40, false)]
		public string  ViewValue;

	}
	#endregion

	#region UserGroup2Resource
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLUSERGROUP2RES", "RESCODE,USERGROUPCODE")]
	public class UserGroup2Resource : DomainObject
	{
		public UserGroup2Resource()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string  ResourceCode;

		/// <summary>
		/// 用户组代码
		/// </summary>
		[FieldMapAttribute("USERGROUPCODE", typeof(string), 40, true)]
		public string  UserGroupCode;

	}
	#endregion

	#region UserGroup2User
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLUSERGROUP2USER", "USERCODE,USERGROUPCODE")]
	public class UserGroup2User : DomainObject
	{
		public UserGroup2User()
		{
		}
 
		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 用户代码
		/// </summary>
		[FieldMapAttribute("USERCODE", typeof(string), 40, true)]
		public string  UserCode;

		/// <summary>
		/// 用户组代码
		/// </summary>
		[FieldMapAttribute("USERGROUPCODE", typeof(string), 40, true)]
		public string  UserGroupCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

    #region UserGroup2Vendor-- 供应商所在用户组 add by jinger 2016-01-25
    /// <summary>
    /// TBLUSERGROUP2VENDOR-- 供应商所在用户组 
    /// </summary>
    [Serializable, TableMap("TBLUSERGROUP2VENDOR", "USERGROUPCODE,VENDORCODE")]
    public class UserGroup2Vendor : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public UserGroup2Vendor()
        {
        }

        ///<summary>
        ///保留字段
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///用户组代码
        ///</summary>
        [FieldMapAttribute("USERGROUPCODE", typeof(string), 40, false)]
        public string UserGroupCode;

        ///<summary>
        ///供应商代码
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

    }
    #endregion

	#region SystemError
	/// <summary>
	/// 系统错误记录
	/// </summary>
	[Serializable, TableMap("TBLSYSERROR", "SYSERRCODE")]
	public class SystemError : DomainObject
	{
		public SystemError()
		{
		}
 
		/// <summary>
		/// 系统错误代码
		/// </summary>
		[FieldMapAttribute("SYSERRCODE", typeof(string), 40, true)]
		public string  SystemErrorCode;

		/// <summary>
		/// 错误信息
		/// </summary>
		[FieldMapAttribute("ERRMSG", typeof(string), 100, true)]
		public string  ErrorMessage;

		/// <summary>
		/// 内部错误信息
		/// </summary>
		[FieldMapAttribute("INNERERRMSG", typeof(string), 100, false)]
		public string  InnerErrorMessage;

		/// <summary>
		/// 提交用户
		/// </summary>
		[FieldMapAttribute("SENDUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  SendUser;

		/// <summary>
		/// 提交日期
		/// </summary>
		[FieldMapAttribute("SENDDATE", typeof(int), 8, true)]
		public int  SendDate;

		/// <summary>
		/// 提交时间
		/// </summary>
		[FieldMapAttribute("SENDTIME", typeof(int), 6, true)]
		public int  SendTime;

		/// <summary>
		/// 是否已解决
		/// </summary>
		[FieldMapAttribute("ISRES", typeof(string), 1, true)]
		public string  IsResolved;

		/// <summary>
		/// 解决备注
		/// </summary>
		[FieldMapAttribute("RESNOTES", typeof(string), 100, false)]
		public string  ResolveNotes;

		/// <summary>
		/// 解决日期
		/// </summary>
		[FieldMapAttribute("RESDATE", typeof(int), 8, false)]
		public int  ResolveDate;

		/// <summary>
		/// 解决时间
		/// </summary>
		[FieldMapAttribute("RESTIME", typeof(int), 6, false)]
		public int  ResolveTime;

		/// <summary>
		/// 维护用户
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 维护日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 维护时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 解决用户
		/// </summary>
		[FieldMapAttribute("RESUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  ResolveUser;

		/// <summary>
		/// 触发错误模块
		/// </summary>
		[FieldMapAttribute("TRGMDLCODE", typeof(string), 40, false)]
		public string  TriggerModuleCode;

		/// <summary>
		/// 触发错误动作
		/// </summary>
		[FieldMapAttribute("TRIGACTION", typeof(string), 40, false)]
		public string  TriggerAction;

	}
	#endregion

	#region USERGROUP2ITEM
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("USERGROUP2ITEM", "USERGROUPCODE,ITEMCODE")]
	public class USERGROUP2ITEM : DomainObject
	{
		public USERGROUP2ITEM()
		{
		}
 
		/// <summary>
		/// 用户组代码
		/// </summary>
		[FieldMapAttribute("USERGROUPCODE", typeof(string), 40, true)]
		public string  USERGROUPCODE;

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ITEMCODE;

		/// <summary>
		/// 维护人员
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MUSER;

		/// <summary>
		/// 维护日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MDATE;

		/// <summary>
		/// 维护时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MTIME;

		/// <summary>
		/// 是否有效
		/// </summary>
		[FieldMapAttribute("ISAVAILABLE", typeof(decimal), 10, true)]
		public decimal  ISAVAILABLE;

		/// <summary>
		/// 备注
		/// </summary>
		[FieldMapAttribute("EATTRIBUTE1", typeof(string), 100, false)]
		public string  EATTRIBUTE1;

	}
	#endregion

    #region BSHomeSetting

    /// <summary>
    ///	BSHomeSetting
    /// </summary>
    [Serializable, TableMap("TBLBSHOMESETTING", "REPORTSEQ")]
    public class BSHomeSetting : DomainObject
    {
        public BSHomeSetting()
        {
        }

        ///<summary>
        ///ReportSeq
        ///</summary>	
        [FieldMapAttribute("REPORTSEQ", typeof(int), 38, false)]
        public int ReportSeq;

        ///<summary>
        ///ModuleCode
        ///</summary>	
        [FieldMapAttribute("MDLCODE", typeof(string), 40, false)]
        public string ModuleCode;

        ///<summary>
        ///ChartType
        ///</summary>	
        [FieldMapAttribute("CHARTTYPE", typeof(string), 40, false)]
        public string ChartType;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 38, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 38, false)]
        public int MaintainTime;

    }

    #endregion

    #region BSHomeSettingDetail

    /// <summary>
    ///	BSHomeSettingDetail
    /// </summary>
    [Serializable, TableMap("TBLBSHOMESETTINGDETAIL", "REPORTSEQ, PARAMNAME")]
    public class BSHomeSettingDetail : DomainObject
    {
        public BSHomeSettingDetail()
        {
        }

        ///<summary>
        ///ReportSeq
        ///</summary>	
        [FieldMapAttribute("REPORTSEQ", typeof(int), 38, false)]
        public int ReportSeq;

        ///<summary>
        ///ParameterName
        ///</summary>	
        [FieldMapAttribute("PARAMNAME", typeof(string), 100, false)]
        public string ParameterName;

        ///<summary>
        ///ParameterValue
        ///</summary>	
        [FieldMapAttribute("PARAMVALUE", typeof(string), 2000, false)]
        public string ParameterValue;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 38, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 38, false)]
        public int MaintainTime;

    }

    #endregion
}

