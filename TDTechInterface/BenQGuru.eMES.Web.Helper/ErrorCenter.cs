using System;
using System.Collections;
using BenQGuru.eMES.Common.MutiLanguage;


namespace BenQGuru.eMES.Web.Helper
{
	public class ErrorCenter
	{
		
		private static Hashtable _hashtable = null;

		//this is  for module BOMFileParser
		#region this is for BOMFileParser
		//key
		public static string ERROR_UPLOAD_UPPERLIMIT = "error_upload_upperlimit";
		public static string SYSTEM_ERROR = "system_error";
		public static string FILE_COLUMN_ERROR = "error_file_column";
		public static string NOT_EMPTY_ERROR = "error_not_empty";
		public static string EFFECTDATE_ERROR = "error_effectdate_error";
		public static string INEFFECTDATE_ERROR = "error_ineffectdate_error";
		public static string ERROR_CREATENEWREWORKMOCODE="生成重工工单号失败！";

		//value
		private static string bomfileparser_error_upload_upperlimit = "文件过大超过1M!";
		private static string bomfileparser_system_error ="系统错误，解析文件失败！";
		private static string bomfileparser_error_file_column = "解析文件必须大于等于8列！";
		private static string bomfileparser_error_not_empty ="列不能为空！";
		private static string bomfileparser_error_effectdate_error ="失效日期必须大于生效日期！";
		private static string bomfileparser_error_ineffectdate_error ="失效日期必须大于今天！";
	
		#endregion

		#region this is for model
		//key
		public static string ADDMODEL_ERROR = "";
		#endregion

		#region this is for ModelFacade
		/// <summary>
		/// crystal chu 20050411
		/// </summary>
		public static string ERROR_ASSIGNITEMSTOMODEL="assign items to Model {0} failure!";
		public static string ERROR_REMOVEITEMSFROMMODEL ="remove items from Model {0} failure!";
		public static string ERROR_ASSGINROUTEALTSTOMODEL ="assign routealts to model {0} failure!";
		public static string ERROR_REMOVEROUTEALTSTOMODEL = "remove routealts from Model {0} failure!";
		public static string ERROR_ADDMODELROUTE =" Add model2Route error, modelcode ='{0}' and routecode='{1}'";
		public static string ERROR_MODELROUTEUSED =" The Model2Route modelcode='{0}',routecode='{1}' has used! ";
		public static string ERROR_DELETEMODELROUTE=" Delete model2route failure,the model2route,modelcode='{0}',routecode='{1}'";
		public static string ERROR_DELETEMODELROUTES=" Delete model2routes failure!";
		public static string ERROR_UPDATEMODEL2OPERATION=" update model2operation failure!modelcode='{0}',opcode='{1}'";

		#endregion

		#region this is for MOFacade
		public static string ERROR_DOWNLOADMO =" dowload mos error!";
		public static string ERROR_MOSTATUS =" the MO status error!the status must be {0}";
		public static string ERROR_DELETEMO =" Delete MO error! mocode='{0}'";
		public static string ERROR_DELETEMOS =" Delete MOs error!";
		public static string ERROR_UPDATEMO =" Update MO error! mocode='{0}'";
		public static string ERROR_MOSTATUSCHANGED = "The MO mocode='{0}', can not turn status into '{1}'!";
		public static string ERROR_GETMONORMALROUTEBYMOCODE = " The MO mocode='{0}' has  no normal route!";
		#endregion

		#region this is for opBOMFacade
		public static string ERROR_OPBOMUSED =" The opBOM has been used! opBOMCode ='{0}'";
		public static string ERROR_BOMCOMPONENTLOADINGUSED =" The opBOM opBOMCode ='{0}' has maintianed the component loading information can not delete!";
		public static string ERROR_DELETEOPBOM ="Delete opBOM opBOMCode='{0}' failure!";
		public static string ERROR_ASSIGNBOMITEMTOOPERATION =" Assign bom item to operation opcode='{0}' failure!";
		public static string ERROR_OPBOMITEMCONTROL="opBOMitem itemcode='{0}' control information has existed!";
		public static string ERROR_DELETEOPBOMITEM =" Delete opBOMitem  itemcode='{0}' failure!";
		#endregion

		#region this is for opBOMItemControlFacade
		public static string ERROR_ADDOPITEMCONTROL="Add OPBOMItem itemcode='{0}' failure!";
		public static string ERROR_DELETEITEMCONTROL =" Delete item itemcode='{0}''s itemcontrol failure!";
		#endregion

		#region this is for rework
		public static string ERROR_REWORKSTATUS = "重工需求单的状态必须为{0}";
		public static string ERROR_APPROVESTATUS = "签核状态必须为{0}";
		public static string ERROR_APPROVER ="未到该用户{0}签核";
		public static string ERROR_PASSREWORKAPPROVE="签核失败";
		#endregion

		#region TS

		#endregion
		
		/// <summary>
		/// sammer kong 20050308 for xml config class
		/// </summary>
		public static string ERROR_ARGUMENT_NULL = "{0} is null!Please check!";
		public static string ERROR_FILE_LOST = "The file {0} is lost!";
		public static string ERROR_CONFIG = "Please check {0}! The configuration is error!";
		public static string ERROR_TYPE_CONVERTOR = "Please check type {0}!";
	
		public static string ERROR_USERGROUPNOTEXIST = "ERROR_USERGROUPNOTEXIST";
		/// <summary>
		/// Jane Shu 2005/03/09 for database error
		/// </summary>
		public static string ERROR_PKOVERLAP = "主键重复";
		public static string ERROR_NOTEXIST  = "记录不存在";
		public static string ERROR_ADD		 = "新增记录出错";
		public static string ERROR_UPDATE	 = "更新记录出错";	
		public static string ERROR_DELETE	 = "删除记录出错";
		public static string ERROR_ADDCHECK  = "新增记录检查出错";
		public static string ERROR_UPDATECHECK = "更新记录检查出错";
		public static string ERROR_DELETECHECK = "删除记录检查出错";
		public static string ERROR_ASSOCIATEEXIST = "与{0}存在关联记录";
		public static string ERROR_PASSWORMATCH = "密码不匹配";

		/// <summary>
		/// Jane Shu 2005/03/09 for user input check
		/// </summary>
		public static string ERROR_WITHOUTINPUT = "{0}缺少输入";
		public static string ERROR_FORMAT		= "{0}输入格式错误";
		public static string ERROR_TOLONG		= "{0}输入字符串过长，应小于{1}";
		public static string ERROR_NUMNERTOLONG	= "{0}超出有效值范围，应小于{1}";
		public static string ERROR_PARENTISCHILDREN = "{0}不能移至本身的子节点";

		#region Login & Rights Check
		public static string ERROR_NOUSERCODE				= "缺少用户名！";		
		public static string ERROR_NOPASSWORD				= "缺少密码！";
		public static string ERROR_USERNOTEXIST				= "用户名不存在！";
		public static string ERROR_PASSWORDNOTMARCH			= "用户密码错误！";
		public static string ERROR_USERNOTBELONGTOANYGROUP  = "用户不属于任何用户组！";
		public static string ERROR_USERGROUPHASNORIGHTS		= "用户组没有任何权限！";
		public static string ERROR_NOACCESSRIGHT			= "没有访问权限！";
		public static string ERROR_USERNOTINUSERGROUP		= "此用户不属于任何用户组！";
		public static string ERROR_MODULENOTEXIST			= "模块{0}不在系统中，请通知管理员！";
		public static string ERROR_LOGINOVERTIME			= "未登录或登录超时，请重新登录！";
		public static string ERROR_NOMODULECODE				= "页面不属于任何模块，请通知管理员！";
		#endregion
	
		static ErrorCenter()
		{
			_hashtable  = new Hashtable();
			_hashtable.Add("bomfileparser_error_upload_upperlimit",bomfileparser_error_upload_upperlimit);
			_hashtable.Add("bomfileparser_system_error",bomfileparser_system_error);
			_hashtable.Add("bomfileparser_error_file_column",bomfileparser_error_file_column);
			_hashtable.Add("bomfileparser_error_not_empty",bomfileparser_error_not_empty);
			_hashtable.Add("bomfileparser_error_effectdate_error",bomfileparser_error_effectdate_error);
			_hashtable.Add("bomfileparser_error_ineffectdate_error",bomfileparser_error_ineffectdate_error);
			//_hashtable.Add("additemroute_error",additemroute_error);
		}

		public static string GetErrorServerDescription(Type type,string errorCode, LanguageType  languageType)
		{
			return errorCode;
		}

		public static string GetErrorServerDescription(Type type,string errorCode, System.Globalization.CultureInfo cultureInfo)
		{
			return errorCode;
		}

		public static string GetErrorServerDescription(Type type,string errorCode)
		{
			return errorCode;
		}

		public static string GetErrorUserDescription(Type type,string errorCode, LanguageType  languageType)
		{
			return errorCode;
		}

		public static string GetErrorUserDescription(Type type,string errorCode, System.Globalization.CultureInfo cultureInfo)
		{
			return errorCode;
		}

		public static string GetErrorUserDescription(Type type,string errorCode)
		{
			return errorCode;
		}
		//	
		//
		//		public static string GetErrorDescription(Type type,string errorCode)
		//		{
		//			return _hashtable[type.Name.ToLower()+"_"+errorCode.ToLower()].ToString();
		//		}
		//
		//		public static string GetParseErrorDescription(Type type,string errorCode,int errorLine)
		//		{
		//			return string.Empty;
		//		}
	}
}