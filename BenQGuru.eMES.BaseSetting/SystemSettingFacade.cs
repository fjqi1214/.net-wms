using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;  

using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Web.Helper;
using System.Data;
using BenQGuru.eMES.Domain.TSModel;

namespace BenQGuru.eMES.BaseSetting
{
	/// <summary>
	/// SystemSettingFacade 的摘要说明。
    /// 
	/// 文件名:		SystemSettingFacade.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Jane Shu
	/// 创建日期:	2005/03/09
	/// 修改人:		Jane Shu
	/// 修改日期:	2005-04-05  
	///					主键大写，去掉upper
	/// 描 述:		系统设置模型维护后台
	/// 版 本:	
	/// </summary>
	public class SystemSettingFacade:MarshalByRefObject
	{
		private IDomainDataProvider _domainDataProvider = null;
		private FacadeHelper _helper = null;

		public SystemSettingFacade(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._helper = new FacadeHelper( DataProvider );
		}

		//Laws Lu,max life time to unlimited
		public override object InitializeLifetimeService()
		{
			return null;
		}

		public SystemSettingFacade()
		{
			this._helper = new FacadeHelper( DataProvider );
		}

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

		#region ParameterGroup
		public ParameterGroup CreateNewParameterGroup()
		{
			return new ParameterGroup();
		}

		public void AddParameterGroup( ParameterGroup parameterGroup)
		{
			this._helper.AddDomainObject( parameterGroup );
		}

		public void AddParameterGroup( ParameterGroup[] parameterGroups)
		{
            foreach(ParameterGroup parameterGroup in parameterGroups)
            {
                this._helper.AddDomainObject( parameterGroup );
            }
		}

		public void UpdateParameterGroup(ParameterGroup parameterGroup)
		{
			this._helper.UpdateDomainObject( parameterGroup );
		}

		public void DeleteParameterGroup(ParameterGroup parameterGroup)
		{
			this._helper.DeleteDomainObject( parameterGroup, new ICheck[]{ new DeleteAssociateCheck( parameterGroup, 
																			 this.DataProvider,
																			 new Type[]{typeof(Parameter)}) });

		}

		public void DeleteParameterGroup(ParameterGroup[] parameterGroups)
		{
			this._helper.DeleteDomainObject( parameterGroups, new ICheck[]{ new DeleteAssociateCheck( parameterGroups, 
																			 this.DataProvider,
																			 new Type[]{typeof(Parameter)}) });

		}

		/// <summary>
		/// ** 功能描述:	由ParameterGroupCode获得ParameterGroup
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-09
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="parameterGroupCode">ParameterGroupCode</param>
		/// <returns>ParameterGroup</returns>
		public object GetParameterGroup( string parameterGroupCode )
		{
			return this.DataProvider.CustomSearch(typeof(ParameterGroup), new object[]{parameterGroupCode}); 
		}

		/// <summary>
		/// ** 功能描述:	查询ParameterGroup的总行数
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-09
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="routeCode">ParameterGroupCode，模糊查询</param>
		/// <returns>ParameterGroup的总记录数</returns>
		public int QueryParameterGroupCount(string parameterGroupCode, string parameterGroupType)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLSYSPARAMGROUP where PARAMGROUPCODE like '{0}%' and UPPER(PARAMGROUPTYPE) like '{1}%'", parameterGroupCode, parameterGroupType.ToUpper() )));
		}

		/// <summary>
		/// ** 功能描述:	由ParameterGroupCode模糊查询ParameterGroup，分页
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-09
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="parameterGroupCode">ParameterGroupCode，模糊查询</param>
		/// <param name="parameterGroupType">ParameterGroupType，模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns></returns>
		public object[] QueryParameterGroup( string parameterGroupCode, string parameterGroupType, int inclusive, int exclusive )
		{	
			return this.DataProvider.CustomQuery(typeof(ParameterGroup), new PagerCondition(string.Format("select {0} from TBLSYSPARAMGROUP where PARAMGROUPCODE like '{1}%' and UPPER(PARAMGROUPTYPE) like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ParameterGroup)), parameterGroupCode, parameterGroupType.ToUpper()),"PARAMGROUPCODE", inclusive, exclusive));
		}

		/// <summary>
		/// 获得所有的ParameterGroup
		/// </summary>
		/// <returns></returns>
		public object[] GetAllParameterGroups()
		{
			return this.DataProvider.CustomQuery(typeof(ParameterGroup), new SQLCondition(string.Format("select {0} from TBLSYSPARAMGROUP order by PARAMGROUPCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ParameterGroup)))));
		}
		#endregion

		#region Parameter
		public Parameter CreateNewParameter()
		{
			return new Parameter();
		}

		public void AddParameter( Parameter parameter)
		{
			this._helper.AddDomainObject( parameter );
		}

		public void AddParameter( Parameter[] parameters)
		{
            foreach(Parameter parameter in parameters)
            {
                this._helper.AddDomainObject( parameter );
            }
		}

		public void UpdateParameter(Parameter parameter)
		{
			this._helper.UpdateDomainObject( parameter );
		}

		public void DeleteParameter(Parameter parameter)
		{
			string strSql = "SELECT COUNT(*) FROM TBLSYSPARAM WHERE PARENTPARAM='" + parameter.ParameterCode + "'";
			if (this.DataProvider.GetCount(new SQLCondition(strSql)) > 0)
			{
				throw new Exception("$Parameter_Exist_Children [" + parameter.ParameterCode + "]");
			}
			this._helper.DeleteDomainObject( parameter );
		}

        public void DeleteParameter(Parameter[] parameters)
        {
            //this._helper.DeleteDomainObject( parameters );
			this.DataProvider.BeginTransaction();
			try
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					DeleteParameter(parameters[i]);
				}
				this.DataProvider.CommitTransaction();
			}
			catch (Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				throw ex;
			}
        }


		/// <summary>
		/// ** 功能描述:	由ParameterCode获得Parameter
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-09
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="parameterCode">ParameterCode</param>
		/// <returns>Parameter</returns>
		public object GetParameter( string parameterCode, string parameterGroupCode )
		{
			return this.DataProvider.CustomSearch(typeof(Parameter), new object[]{parameterCode, parameterGroupCode}); 
		}

        public string GetGetParameterFileName(string parameterCode, string parameterGroupCode)
        {
            string sql = " SELECT paramcode, paramgroupcode, paramalias, paramdesc, paramvalue, muser, mdate, mtime, isactive, issys, eattribute1 FROM tblsysparam where 1=1 ";

            if (parameterGroupCode.Trim() != string.Empty)
            {
                sql += " AND paramgroupcode='" + parameterGroupCode.Trim().ToUpper() + "'";
            }

            if (parameterCode.Trim() != string.Empty)
            {
                sql += " AND paramcode='" + parameterCode.Trim().ToUpper() + "'";
            }

            object[] FileNameObjects = this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));

            if (FileNameObjects == null)
            {
                return null;
            }

            return ((Parameter)FileNameObjects[0]).ParameterAlias;
        }

		/// <summary>
		/// ** 功能描述:	由ParameterCode模糊查询Parameter的记录行数
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-22
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="parameterCode">ParameterCode，模糊查询</param>
		/// <param name="parameterGroupCode">ParameterGroupCode，精确查询，空字符串不作查询条件</param>
		/// <returns>Parameter的记录行数</returns>
		public int QueryParameterCount(string parameterCode, string parameterGroupCode)
		{			
			string condition = "";

			if ( parameterGroupCode != null && parameterGroupCode.Length != 0)
			{
				condition = string.Format("{0} and PARAMGROUPCODE  = '{1}'", condition, parameterGroupCode);
			}

			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLSYSPARAM where PARAMCODE like '{0}%' {1}", parameterCode, condition)));
		}


       

		/// <summary>
		/// ** 功能描述:	由ParameterCode模糊查询Parameter的记录行数
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-22
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="parameterCode">ParameterCode，模糊查询</param>
		/// <param name="parameterGroupCode">ParameterGroupCode，精确查询，空字符串不作查询条件</param>
		/// <returns>Parameter的记录行数</returns>
		public int QueryParameterCount(string parameterCode, string parameterGroupCode, string parentParameterCode, bool queryAllChildren)
		{			
			string condition = "";

			if ( parameterGroupCode != null && parameterGroupCode.Length != 0)
			{
				condition = string.Format("{0} and PARAMGROUPCODE  = '{1}'", condition, parameterGroupCode);
			}
			if (parentParameterCode != string.Empty)
			{
				condition = string.Format("{0} and PARENTPARAM='{1}' ", condition, parentParameterCode);
			}
			else if (queryAllChildren == false)
			{
				condition = string.Format("{0} and (PARENTPARAM is null OR PARENTPARAM='') ", condition);
			}

			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLSYSPARAM where PARAMCODE like '{0}%' {1}", parameterCode, condition)));
		}



		/// <summary>
		/// ** 功能描述:	 由ParameterCode和ParameterGroup查询Parameter，分页
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-09
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="parameterCode">ParameterCode，模糊查询</param>
		/// <param name="parameterGroup">ParameterGroup，精确查询，空字符串不作查询条件</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns>Parameter数组</returns>
		public object[] QueryParameter( string parameterCode, string parameterGroup, int inclusive, int exclusive )
		{	
			string condition = "";

			if ( parameterCode != null && parameterCode.Length != 0)
			{
				condition = string.Format("{0} and PARAMCODE like '{1}%'", condition, parameterCode);
			}

			if ( parameterGroup != null && parameterGroup.Length != 0)
			{
				condition = string.Format("{0} and PARAMGROUPCODE  = '{1}'", condition, parameterGroup);
			}

			return this.DataProvider.CustomQuery(typeof(Parameter), new PagerCondition(string.Format("select {0} from TBLSYSPARAM where 1=1 {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)), condition),"PARAMCODE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	 由ParameterCode和ParameterGroup查询Parameter，分页
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-09
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="parameterCode">ParameterCode，模糊查询</param>
		/// <param name="parameterGroup">ParameterGroup，精确查询，空字符串不作查询条件</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns>Parameter数组</returns>
		public object[] QueryParameter( string parameterCode, string parameterGroup, string parentParameterCode, bool queryAllChildren, int inclusive, int exclusive )
		{	
			string condition = "";

			if ( parameterCode != null && parameterCode.Length != 0)
			{
				condition = string.Format("{0} and PARAMCODE like '{1}%'", condition, parameterCode);
			}

			if ( parameterGroup != null && parameterGroup.Length != 0)
			{
				condition = string.Format("{0} and PARAMGROUPCODE  = '{1}'", condition, parameterGroup);
			}

			if (parentParameterCode != string.Empty)
			{
				condition = string.Format("{0} and PARENTPARAM='{1}' ", condition, parentParameterCode);
			}
			else if (queryAllChildren == false)
			{
				condition = string.Format("{0} and (PARENTPARAM is null OR PARENTPARAM='') ", condition);
			}

			return this.DataProvider.CustomQuery(typeof(Parameter), new PagerCondition(string.Format("select {0} from TBLSYSPARAM where 1=1 {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)), condition),"PARAMCODE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的Parameter
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-09
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns></returns>
		public object[] GetAllParameters()
		{
			return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(string.Format("select {0} from TBLSYSPARAM order by PARAMCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)))));
		}


        public object[] GetAllBIGSSCODE()
        {
            return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(string.Format("select {0} from TBLSYSPARAM where PARAMGROUPCODE='BIGLINEGROUP' order by PARAMALIAS", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)))));
        }
		/// <summary>
		/// ** 功能描述:	获得ParameterGroup下的所有Parameter
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-09
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="parameterGroup"></param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns></returns>
		public object[] GetParametersByParameterGroup( string parameterGroup )
		{
			//return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(string.Format("select {0} from TBLSYSPARAM where PARAMGROUPCODE='{1}' order by PARAMCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)), parameterGroup)));
			 return this.GetParametersByParameterGroup(parameterGroup,string.Empty);
		}

		/// <summary>
		/// ** 功能描述:	获得ParameterGroup下的所有Parameter,排序字段paramcode,EATTRIBUTE1
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-09
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="parameterGroup"></param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns></returns>
		public object[] GetParametersByParameterGroup( string parameterGroup ,string orderbyColumn)
		{
			//EATTRIBUTE1 是序号字段,默认按照序号排序
			string orderbyCondition = " order by EATTRIBUTE1,paramcode ";	
			if(orderbyColumn.ToUpper() == "PARAMCODE")
			{
				orderbyCondition = " order by paramcode,EATTRIBUTE1 ";
			}
			string sql = string.Format("select {0} from TBLSYSPARAM where PARAMGROUPCODE='{1}'  {2}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)), parameterGroup,orderbyCondition);
			return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));
		}

        public object[] GetParamGroupByParamGroupType(string paramgrouptype)
        {
            return this.DataProvider.CustomQuery(typeof(ParameterGroup), new SQLCondition(string.Format("select {0} from tblsysparamgroup where paramgrouptype='{1}'  ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ParameterGroup)), paramgrouptype)));
        }

        //add by sam
        public object[] GetDistinctParaInParameterGroup(string[] parameterGroup)
        {
            //EATTRIBUTE1 是序号字段,默认按照序号排序
            //string orderbyCondition = " order by PARAMGROUPCODE ,EATTRIBUTE1,paramcode ";
            string sql = string.Format(@"select  paramcode,paramdesc  from TBLSYSPARAM where PARAMGROUPCODE in ({0}) group by    paramcode,paramdesc order by paramcode", 
                parameterGroup);
            return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));
        }

		/// <summary>
		/// ** 功能描述:	获得ParameterGroup下的所有指定ParameterValue的Parameter
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-05-20
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="parameterGroup"></param>
		/// <param name="parameterValue"></param>
		/// <returns></returns>
		public object[] GetParametersByParameterValue( string parameterGroup, string parameterValue )
		{
			return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(string.Format("select {0} from TBLSYSPARAM where PARAMGROUPCODE='{1}' and PARAMVALUE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)), parameterGroup, parameterValue)));
		}
			
		/// <summary>
		/// ** 功能描述:	获得所有的Parameter,按ParameterSequence排序
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-04-19
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <returns>Module数组</returns>
		public object[] GetAllParametersOrderBySequence(string parameterGroupCode)
		{
			string strSql = string.Format("select {0} from TBLSYSPARAM WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)));
			if (parameterGroupCode != string.Empty)
				strSql += " AND PARAMGROUPCODE='" + parameterGroupCode + "' ";
			strSql += " order by PARAMGROUPCODE,PARAMCODE,EATTRIBUTE1 ";
			return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(strSql));
		}

        public object[] GetAllParametersOrderByEattribute1(string parameterGroupCode)
        {
            string strSql = string.Format("select {0} from TBLSYSPARAM WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)));
            if (parameterGroupCode != string.Empty)
                strSql += " AND PARAMGROUPCODE='" + parameterGroupCode + "' ";
            strSql += " order by EATTRIBUTE1 ";
            return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(strSql));
        }


        public string GetParameterAlias(string parameterGroupCode, string parameterCode)
        {
            string returnValue = string.Empty;

            Parameter setting = (Parameter)GetParameter(parameterCode, parameterGroupCode);
            if (setting != null)
            {
                returnValue = setting.ParameterAlias;
            }

            return returnValue;
        }

        public string GetParameterDescription(string parameterGroupCode, string parameterCode)
        {
            string returnValue = string.Empty;

            Parameter setting = (Parameter)GetParameter(parameterCode, parameterGroupCode);
            if (setting != null)
            {
                returnValue = setting.ParameterDescription;
            }

            return returnValue;
        }

        public object[] GetAllDocType()
        {
            return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(string.Format("select {0} from TBLSYSPARAM where PARAMGROUPCODE='DOCTYPEGROUP' order by PARAMALIAS", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)))));
        }

		#endregion

		#region Module
		public Module CreateNewModule()
		{
			return new Module();
		}

		public ModuleWithViewValue CreateNewModuleWithViewValue(Module module)
		{
			return new ModuleWithViewValue();
		}

		public object[] GetSelectedModuleWithViewValueByUserGroupCode(string userGroupCode,string moduleCode,int inclusive,int exclusive)
		{	
			return this.DataProvider.CustomQuery(typeof(ModuleWithViewValue), 
				new PagerCondition(string.Format("select {0},TBLUSERGROUP2MODULE.VIEWVALUE from TBLUSERGROUP2MODULE, TBLMDL where TBLUSERGROUP2MODULE.MDLCODE = TBLMDL.MDLCODE and TBLUSERGROUP2MODULE.USERGROUPCODE ='{1}' and TBLMDL.MDLCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Module)), userGroupCode, moduleCode),"TBLMDL.PMDLCODE, TBLMDL.MDLSEQ", inclusive, exclusive));
		}

		public object[] GetSelectedModuleWithViewValueByUserGroupCode(string userGroupCode,string moduleType, string moduleCode,string moduleDesc, int inclusive, int exclusive)
		{
			string Condition = string.Empty;
			if(moduleType != null && moduleType!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLTYPE = '{0}' ",moduleType);
			}
			if(moduleCode != null && moduleCode!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLCODE like '{0}%' ",moduleCode);
			}
			if(moduleDesc != null && moduleDesc!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLDESC like '{0}%' ",moduleDesc);
			}
			string sql =string.Format("select {0},TBLUSERGROUP2MODULE.VIEWVALUE from TBLUSERGROUP2MODULE, TBLMDL where TBLUSERGROUP2MODULE.MDLCODE = TBLMDL.MDLCODE and TBLUSERGROUP2MODULE.USERGROUPCODE ='{1}' {2}", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Module)), userGroupCode, Condition);
			return this.DataProvider.CustomQuery(typeof(ModuleWithViewValue), 
				new PagerCondition(sql,"TBLMDL.PMDLCODE, TBLMDL.MDLSEQ", inclusive, exclusive));
		}

		public object[] GetSelectedModuleWithViewValueByUserGroupCode(string userGroupCode,string moduleType, string moduleCode,string moduleDesc, string parentModuleCode)
		{
			string Condition = string.Empty;
			if(moduleType != null && moduleType!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLTYPE = '{0}' ",moduleType);
			}
			if(moduleCode != null && moduleCode!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLCODE like '{0}%' ",moduleCode);
			}
			if(moduleDesc != null && moduleDesc!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLDESC like '{0}%' ",moduleDesc);
			}
			if (parentModuleCode == string.Empty)
				Condition += " and (TBLMDL.PMDLCODE='' OR TBLMDL.PMDLCODE IS NULL) ";
			else
				Condition += string.Format(" and (TBLMDL.PMDLCODE='' OR TBLMDL.PMDLCODE IS NULL OR TBLMDL.PMDLCODE IN ({0}) ) ", "'" + parentModuleCode.Replace(";","','") + "'");
			
			string sql =string.Format("select {0},TBLUSERGROUP2MODULE.VIEWVALUE from TBLUSERGROUP2MODULE, TBLMDL where TBLUSERGROUP2MODULE.MDLCODE = TBLMDL.MDLCODE and TBLUSERGROUP2MODULE.USERGROUPCODE ='{1}' {2}", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Module)), userGroupCode, Condition);
			sql += " ORDER BY TBLMDL.PMDLCODE,TBLMDL.MDLSEQ ";
			return this.DataProvider.CustomQuery(typeof(ModuleWithViewValue), 
				new SQLCondition(sql));
		}
		public object[] GetModuleWithViewValueVisibility(string parentModuleCode)
		{
			string Condition = " and (ISRESTRAIN is null or ISRESTRAIN='0') ";
			if (parentModuleCode == string.Empty)
				Condition += " and (PMDLCODE='' OR PMDLCODE IS NULL) ";
			else
				Condition += string.Format(" and (PMDLCODE='' OR PMDLCODE IS NULL OR PMDLCODE IN ({0}) ) ", "'" + parentModuleCode.Replace(";","','") + "'");
			
			string sql =string.Format("select {0} from TBLMDL where 1=1 and ISACTIVE=1 {1}", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Module)), Condition);
			sql += " ORDER BY PMDLCODE,MDLSEQ ";
			return this.DataProvider.CustomQuery(typeof(Module), 
				new SQLCondition(sql));
		}

        public DataSet GetHierarchicalModules(string functionGroupCode)
        {
              string sql = string.Format(@"SELECT  a.pmdlcode as ParentModuleCode,a.mdlcode ModuleCode,a.mdldesc AS                     ModuleDescription,a.formurl AS FormUrl, 
              DECODE(MOD(TRUNC(TRUNC(TRUNC(nvl(b.viewvalue,0)/2)/2)/2),2),0,'false','true') AS Export,
               DECODE(MOD(TRUNC(nvl(b.viewvalue,0)/2),2),0,'false','true') AS Write,
                DECODE(MOD(TRUNC(TRUNC(nvl(b.viewvalue,0)/2)/2),2),0,'false','true') AS Read,
              DECODE(MOD(nvl(b.viewvalue,0),2),0,'false','true') AS ""Delete"", b.viewvalue as SecurityList
              FROM(
            SELECT LEVEL lvl,mdlseq,t.pmdlcode,t.mdlcode,t.mdldesc,t.formurl AS
            from tblmdl t
            START WITH (t.pmdlcode is null) CONNECT BY PRIOR t.mdlcode=t.pmdlcode
            ) a LEFT JOIN
            (SELECT k.mdlcode,k.viewvalue FROM TBLFUNCTIONGROUP2FUNCTION k WHERE k.functiongroupcode='{0}') b
            ON a.mdlcode=b.mdlcode
            ORDER BY lvl,a.pmdlcode,a.mdlseq", functionGroupCode);
            return this.DataProvider.QueryData(
                new SQLCondition(sql));
        }
		public object[] GetModuleWithChild()
		{
			string strSql = "SELECT * FROM TBLMDL WHERE MDLCODE IN (SELECT PMDLCODE FROM TBLMDL)";
			return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(strSql));
		}

		public int GetSelectedModuleByUserGroupCodeCount(string userGroupCode,string moduleType, string moduleCode,string moduleDesc, string parentModuleCode)
		{
			string Condition = string.Empty;
			if(moduleType != null && moduleType!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLTYPE = '{0}' ",moduleType);
			}
			if(moduleCode != null && moduleCode!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLCODE like '{0}%' ",moduleCode);
			}
			if(moduleDesc != null && moduleDesc!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLDESC like '{0}%' ",moduleDesc);
			}
			if (parentModuleCode == string.Empty)
				Condition += " and (TBLMDL.PMDLCODE='' OR TBLMDL.PMDLCODE IS NULL) ";
			else
				Condition += string.Format(" and (TBLMDL.PMDLCODE='' OR TBLMDL.PMDLCODE IS NULL OR TBLMDL.PMDLCODE IN ({0}) ) ", "'" + parentModuleCode.Replace(";","','") + "'");
			
			string sql =string.Format("select Count(TBLMDL.MDLCODE) from TBLUSERGROUP2MODULE, TBLMDL where TBLUSERGROUP2MODULE.MDLCODE = TBLMDL.MDLCODE and TBLUSERGROUP2MODULE.USERGROUPCODE ='{0}' {1}", userGroupCode, Condition);

			return this.DataProvider.GetCount(new SQLCondition(sql));
		}

		public int GetModuleWithViewValueVisibilityCount(string parentModuleCode)
		{
			string Condition = " and (ISRESTRAIN is null or ISRESTRAIN='0') ";
			if (parentModuleCode == string.Empty)
				Condition += " and (PMDLCODE='' OR PMDLCODE IS NULL) ";
			else
				Condition += string.Format(" and (PMDLCODE='' OR PMDLCODE IS NULL OR PMDLCODE IN ({0}) ) ", "'" + parentModuleCode.Replace(";","','") + "'");

            string sql = string.Format("select Count(MDLCODE) from TBLMDL where 1=1 and ISACTIVE=1 {0}", Condition);

			return this.DataProvider.GetCount(new SQLCondition(sql));
		}

		public void AddModule( Module module)
		{
			this._helper.AddDomainObject( module );
		}

        public void AddModule( Module[] modules)
        {
            foreach(Module module in modules)
            {
                this._helper.AddDomainObject( module );
            }
        }

		public void UpdateModule(Module module)
		{
			this._helper.UpdateDomainObject( module );
		}

		public void DeleteModule(Module module)
		{
			this._helper.DeleteDomainObject( module, new ICheck[]{ new DeleteAssociateCheck( module, 
																	 this.DataProvider,
																	 new Type[]{typeof(Menu),typeof(UserGroup2Module)}) });

		}

        public void DeleteModule(Module[] modules)
        {
            this._helper.DeleteDomainObject( modules, new ICheck[]{ new DeleteAssociateCheck( modules, 
                                                                     this.DataProvider,
                                                                     new Type[]{typeof(Menu),typeof(UserGroup2Module)}) });

        }

		public object GetModule( string moduleCode )
		{
			return this.DataProvider.CustomSearch(typeof(Module), new object[]{moduleCode}); 
		}

		/// <summary>
		/// ** 功能描述:	获得ModuleCode下的次级Module，按ModuleSequence排序
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="parentModuleCode">精确查询，空字符串返回根节点</param>
		/// <returns>Module数组</returns>
		public object[] GetSubModulesByModuleCode( string parentModuleCode )
		{
			if (parentModuleCode == "")
			{
				return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(string.Format("select {0} from TBLMDL where PMDLCODE is null order by MDLSEQ ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)))));
			}
			else
			{
				return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(string.Format("select {0} from TBLMDL where PMDLCODE = '{1}' order by MDLSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), parentModuleCode)));
			}
		}

		/// <summary>
		/// added by jessie lee
		/// </summary>
		/// <param name="parentModuleCode">模糊查询，空字符串返回根节点</param>
		/// <returns>Module数组</returns>
		public object[] GetSubModulesByParentModuleCode( string parentModuleCode )
		{
			if (parentModuleCode == "")
			{
				return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(string.Format("select {0} from TBLMDL where PMDLCODE is null order by MDLSEQ ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)))));
			}
			else
			{
				return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(string.Format("select {0} from TBLMDL where PMDLCODE like '{1}%' order by MDLSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), parentModuleCode)));
			}
		}

		/// <summary>
		/// ** 功能描述:	获得ModuleCode下的次级Module的行数
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="parentModuleCode">精确查询，空字符串返回根节点</param>
		/// <returns>Module的行数</returns>
		public int GetSubModulesByModuleCodeCount( string parentModuleCode )
		{
			if (parentModuleCode == "")
			{
				return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMDL where PMDLCODE is null")));
			}
			else
			{
				return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMDL where PMDLCODE = '{0}'", parentModuleCode)));
			}
		}

		/// <summary>
		/// ** 功能描述:	获得ModuleCode下的次级Module，按ModuleSequence排序，分页
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="parentModuleCode">精确查询，空字符串返回根节点</param>
		/// <returns>Module数组</returns>
		public object[] GetSubModulesByModuleCode( string parentModuleCode, int inclusive, int exclusive )
		{	
			if (parentModuleCode == "")
			{
				return this.DataProvider.CustomQuery(typeof(Module), new PagerCondition(string.Format("select {0} from TBLMDL where PMDLCODE is null", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module))),"MDLSEQ", inclusive, exclusive));
			}
			else
			{
				return this.DataProvider.CustomQuery(typeof(Module), new PagerCondition(string.Format("select {0} from TBLMDL where PMDLCODE = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), parentModuleCode),"MDLSEQ", inclusive, exclusive));
			}
		}
		
		/// <summary>
		/// ** 功能描述:	获得所有的Module
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <returns>Module数组</returns>
		public object[] GetAllModules()
		{
			return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(string.Format("select {0} from TBLMDL order by MDLCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)))));
		}

        /// <returns>Module数组</returns>
        public object[] GetAllNewReportModules()
        {
            return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(string.Format("SELECT {0} FROM tblmdl WHERE UPPER(formurl) LIKE '%FNEWREPORT%' ORDER BY pmdlcode, mdlseq ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)))));
        }
			
		/// <summary>
		/// ** 功能描述:	获得所有的Module,按ModuleSequence排序
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-04-19
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <returns>Module数组</returns>
		public object[] GetAllModulesOrderBySequence()
		{
			return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(string.Format("select {0} from TBLMDL order by PMDLCODE, MDLSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)))));
		}

		/// <summary>
		/// ** 功能描述:	用url在模块树中查询该url的ModuleCode
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-23
		/// ** 修 改:		Jane Shu	2005-07-21
		///					改传参SQL
		/// ** nunit
		/// </summary>
		/// <param name="moduleTree">模块树的根节点</param>
		/// <param name="uri">页面的Uri</param>
		/// <returns>ModuleCode</returns>
		public string GetModuleCodeByUri(string url)
		{
			object[] objs = this.DataProvider.CustomQuery(typeof(Module), new SQLParamCondition(
				string.Format("select {0} from TBLMDL where upper(FORMURL)=$FORMURL", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module))), 
				new SQLParameter[]{ new SQLParameter("FORMURL", typeof(string), url.ToUpper() ) } ));
		
			if ( objs != null )
			{
				return ((Module)objs[0]).ModuleCode;
			}

			return null;
		}
		
		/// <summary>
		/// ** 功能描述:	由用户Code和页面Url获得用户所属的用户组对页面的所有权限
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-04-18
		/// ** 修 改:		Jane Shu	2005-07-21
		///					改传参SQL
		/// </summary>
		/// <param name="userCode">用户名</param>
		/// <param name="url">页面Url</param>
		/// <returns> Security数组，含ModuleCode和ViewValue（权限字符串）</returns>
		public object[] GetFormRightsByUserAndUrl(string userCode, string url)
		{
			return this.DataProvider.CustomQuery(typeof(FormRight), new SQLParamCondition(
				@"select MDLCODE, VIEWVALUE from TBLUSERGROUP2MODULE where USERGROUPCODE in 
					(select USERGROUPCODE from TBLUSERGROUP2USER where USERCODE=$USERCODE) 
					and MDLCODE in (select MDLCODE from TBLMDL where upper(FORMURL)=$FORMURL)", 
				new SQLParameter[]{ new SQLParameter( "USERCODE", typeof(string), userCode.ToUpper() ),
									  new SQLParameter( "FORMURL", typeof(string), url.ToUpper() ) } ));
		}
		#endregion

		#region Menu
		public Menu CreateNewMenu()
		{
			return new Menu();
		}

		public void AddMenu( Menu menu )
		{
			this._helper.AddDomainObject(menu);
		}

        public void AddMenu( Menu[] menus )
        {
            foreach(Menu menu in menus)
            {
                this._helper.AddDomainObject(menu);
            }
        }

		public void UpdateMenu( Menu menu )
		{
			this._helper.UpdateDomainObject(menu);
		}

		public void DeleteMenu( Menu menu )
		{
			this._helper.DeleteDomainObject( menu );
		}

        public void DeleteMenu( Menu[] menus )
        {
            this._helper.DeleteDomainObject( menus );
        }

		/// <summary>
		/// ** 功能描述:	由MenuCode获得Menu
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="menuCode">MenuCode</param>
		/// <returns>Menu数组</returns>
		public object GetMenu( string menuCode )
		{
			return this.DataProvider.CustomSearch(typeof(Menu), new object[]{menuCode}); 
		}

		/// <summary>
		/// ** 功能描述:	获得ModuleCode对应的Menu
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="moduleCode">ModuleCode</param>
		/// <returns>Menu数组</returns>
		public object[] GetMenuByModuleCode( string moduleCode )
		{
			return this.DataProvider.CustomQuery(typeof(Menu), new SQLCondition(string.Format("select {0} from TBLMENU where MDLCODE = '{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Menu)), moduleCode)));
		}

		/// <summary>
		/// ** 功能描述:	获得Menu对应的Module
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="moduleCode">MenuCode</param>
		/// <returns>Module</returns>
		public Module GetModuleByMenuCode( string menuCode )
		{
			object[] objs = this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(string.Format("select {0} from TBLMDL where MDLCODE in (select MDLCODE from TBLMENU where MENUCODE = '{1}') ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), menuCode)));

			if ( objs != null && objs.Length > 0 )
			{
				return (Module)objs[0];
			}

			return null;
		}

		/// <summary>
		/// ** 功能描述:	获得ParentMenuCode下的次级Menu，按MenuSequence排序
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="parentMenuCode">精确查询，空字符串返回根节点</param>
		/// <returns>Menu数组</returns>
		public object[] GetSubMenusByMenuCode( string parentMenuCode )
		{
			if ( parentMenuCode == "" )
			{
				return this.DataProvider.CustomQuery(typeof(Menu), new SQLCondition(string.Format("select {0} from TBLMENU where PMENUCODE is null order by MENUSEQ ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Menu)))));
			}
			else
			{
				return this.DataProvider.CustomQuery(typeof(Menu), new SQLCondition(string.Format("select {0} from TBLMENU where PMENUCODE = '{1}' order by MENUSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Menu)), parentMenuCode)));
			}
		}

		/// <summary>
		/// ** 功能描述:	获得ParentMenuCode下的次级Menu，按MenuSequence排序，分页
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="parentMenuCode">精确查询，空字符串返回根节点</param>
		/// <returns>Menu数组</returns>
		public object[] GetSubMenusByMenuCode( string parentMenuCode, int inclusive, int exclusive )
		{
			if ( parentMenuCode == "" )
			{
				return this.DataProvider.CustomQuery(typeof(Menu), new PagerCondition(string.Format("select {0} from TBLMENU where PMENUCODE is null", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Menu))),"MENUSEQ", inclusive, exclusive));
			}
			else
			{
				return this.DataProvider.CustomQuery(typeof(Menu), new PagerCondition(string.Format("select {0} from TBLMENU where PMENUCODE = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Menu)), parentMenuCode),"MENUSEQ", inclusive, exclusive));
			}
		}

		/// <summary>
		/// ** 功能描述:	获得ParentMenuCode下的次级Menu的数量
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="parentMenuCode">精确查询，空字符串返回根节点</param>
		/// <returns>Menu数量</returns>
		public int GetSubMenusByMenuCodeCount( string parentMenuCode )
		{
			if ( parentMenuCode == "" )
			{
				return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMENU where PMENUCODE is null ")));
			}
			else
			{
				return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMENU where PMENUCODE = '{0}'", parentMenuCode)));
			}
		}

		/// <summary>
		/// ** 功能描述:	获得所有的Menu
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <returns>Menu数量</returns>
		public object[] GetAllMenus()
		{
			return this.DataProvider.CustomQuery(typeof(Menu), new SQLCondition(string.Format("select {0} from TBLMENU order by MENUCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Menu)))));
		}
		
		/// <summary>
		/// ** 功能描述:	获得所有的Menu，包含Url
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <returns>Menu数量</returns>
		public object[] GetAllMenuWithUrl()
		{
			return this.DataProvider.CustomQuery(typeof(MenuWithUrl), new SQLCondition(string.Format("select {0}, TBLMDL.FORMURL from TBLMENU, TBLMDL where TBLMENU.MDLCODE = TBLMDL.MDLCODE(+) order by TBLMENU.PMENUCODE, TBLMENU.MENUSEQ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Menu)))));
        }
        //melo zheng,2007.5.14,获得所有RPT菜单
        public object[] GetAllMenuWithUrlRPT()
        {
            return this.DataProvider.CustomQuery(typeof(MenuWithUrl), new SQLCondition(string.Format("select {0}, TBLMDL.FORMURL from TBLMENU, TBLMDL where TBLMENU.MDLCODE = TBLMDL.MDLCODE(+) and TBLMENU.mdlcode != 'REPORT' and TBLMENU.menutype = 'B/S RPT' order by TBLMENU.PMENUCODE, TBLMENU.MENUSEQ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Menu)))));
        }
		
		/// <summary>
		/// ** 功能描述:	获得所有的Menu，包含Url
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <returns>Menu数量</returns>
		public object[] GetAllMenuWithUrlWithTypePermission(string menuType, string userCode)
		{
			string strSql = string.Format("select {0}, TBLMDL.FORMURL from TBLMENU, TBLMDL where TBLMENU.MDLCODE = TBLMDL.MDLCODE(+) ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Menu)));
			if (menuType != string.Empty)
				strSql += " AND MENUTYPE='" + menuType + "' ";
			if (userCode != string.Empty)
			{
				strSql += " and tblmdl.mdlcode in ";
				strSql += " (select mdlcode from tblusergroup2module where usergroupcode in ";
				strSql += "(select usergroupcode from tblusergroup2user where usercode='" + userCode + "') ) ";
			}
			strSql += " order by TBLMENU.PMENUCODE, TBLMENU.MENUSEQ ";
			return this.DataProvider.CustomQuery(typeof(MenuWithUrl), new SQLCondition(strSql));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的Menu，包含Url
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <returns>Menu数量</returns>
		public object[] GetMenuWithUrlByMenuCode(string menuCode)
		{
			return this.DataProvider.CustomQuery(typeof(MenuWithUrl), new SQLCondition(string.Format("select {0}, TBLMDL.FORMURL from TBLMENU, TBLMDL where TBLMENU.MDLCODE = TBLMDL.MDLCODE(+) AND PMENUCODE = '" + menuCode + "'order by TBLMENU.PMENUCODE, TBLMENU.MENUSEQ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Menu)))));
		}
		
		/// <summary>
		/// 获得所有抑制显示的Module
		/// </summary>
		public object[] GetAllMenuNotView()
		{
			return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition("select TBLMDL.* from TBLMENU,TBLMDL where TBLMENU.MDLCODE = TBLMDL.MDLCODE and TBLMDL.ISRESTRAIN = 1 order by TBLMENU.PMENUCODE, TBLMENU.MENUSEQ"));
		}
		
		/// <summary>
		/// 获得所有抑制显示的Module
		/// </summary>
		public object[] GetAllMenuUnVisibility(string menuType)
		{
			string strSql = "select * from tblmenu where visibility='1'";
			if (menuType != string.Empty)
				strSql += " and menutype='" + menuType + "'";
			return this.DataProvider.CustomQuery(typeof(Menu), new SQLCondition(strSql));
		}
		#endregion

		#region UserGroup2Module
		public UserGroup2Module CreateNewUserGroup2Module()
		{
			return new UserGroup2Module();
		}

		public void AddUserGroup2Module( UserGroup2Module module)
		{
			this._helper.AddDomainObject(module) ;
		}

		public void AddUserGroup2Module( UserGroup2Module[] modules)
		{
            foreach(UserGroup2Module module in modules)
            {
                this._helper.AddDomainObject(module) ;
            }
		}

		public void UpdateUserGroup2Module(UserGroup2Module module)
		{
			this._helper.UpdateDomainObject(module);
		}

		public void DeleteUserGroup2Module(UserGroup2Module module)
		{
			this._helper.DeleteDomainObject( module );
		}

		public void DeleteUserGroup2Module(UserGroup2Module[] modules)
		{
			this._helper.DeleteDomainObject( modules );
		}

		public object GetUserGroup2Module( string userGroupCode, string moduleCode )
		{
			return this.DataProvider.CustomSearch(typeof(UserGroup2Module), new object[]{moduleCode,userGroupCode});
		}

		/// <summary>
		/// ** 功能描述:	获得所有的Menu
		/// ** 作 者:		Sammer Kong
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="userGroupCode">UserGroupCode</param>
		/// <returns>UserGroup2Module数组</returns>
		public object[] GetUserGroup2ModuleByUserGroup( string userGroupCode )
		{
			return this.DataProvider.CustomSearch(typeof(UserGroup2Module), new string[]{"UserGroupCode"}, new object[]{userGroupCode});
		}

		/// <summary>
		/// ** 功能描述:	由UserGroup和Module获得ViewValue
		/// ** 作 者:		Sammer Kong
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="userGroupCode"></param>
		/// <param name="moduleCode"></param>
		/// <returns></returns>
		public string GetModuleViewValue( string userGroupCode, string moduleCode )
		{
			UserGroup2Module module = (UserGroup2Module) this.DataProvider.CustomSearch(typeof(UserGroup2Module), new object[]{moduleCode,userGroupCode});
			
			if ( module != null )
			{
				return module.ViewValue ;
			}

			return "";
		}	

		/// <summary>
		/// ** 功能描述:	由UserGroup获得Module
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="userGroupCode">精确查询</param>
		/// <param name="viewValue">精确查询，为空不作查询条件</param>
		/// <returns>Module数组</returns>
		public object[] GetModuleByUserGroup( string userGroupCode, string viewValue )
		{
			return this.GetModuleByUserGroup(userGroupCode,viewValue,0,int.MaxValue);
		}

		/// <summary>
		/// ** 功能描述:	由UserGroup获得Module,分页
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="userGroupCode">精确查询</param>
		/// <param name="viewValue">精确查询，为空不作查询条件</param>
		/// <param name="viewValue">精确查询，为空不作查询条件</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns>Module数组</returns>
		public object[] GetModuleByUserGroup( string userGroupCode, string viewValue, int inclusive, int exclusive )
		{
			if(viewValue!=null && viewValue.Length>0)
			{
				return this.DataProvider.CustomQuery(typeof(Module), new PagerCondition(string.Format("select {0} from TBLMDL where MDLCODE in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE='{1}' and VIEWVALUE = '{2}')", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), userGroupCode,viewValue),"MDLCODE", inclusive,exclusive));
			}
			else
			{
				return this.DataProvider.CustomQuery(typeof(Module), new PagerCondition(string.Format("select {0} from TBLMDL where MDLCODE in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE='{1}' )", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), userGroupCode),"MDLCODE", inclusive,exclusive));
			}
		}

		/// <summary>
		/// ** 功能描述:	由UserGroup获得Module
		/// ** 作 者:		Sammer Kong
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// </summary>
		/// <param name="userGroupCode">模糊查询</param>
		/// <param name="viewValue">精确查询，为空不作查询条件</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns></returns>
		public object[] QueryModuleByUserGroup( string userGroupCode, string viewValue, int inclusive, int exclusive )
		{
			if(viewValue!=null && viewValue.Length>0)
			{
				return this.DataProvider.CustomQuery(typeof(Module), new PagerCondition(string.Format("select {0} from TBLMDL where MDLCODE in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE like '{1}%' and VIEWVALUE = '{2}')", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), userGroupCode,viewValue),"MDLCODE",inclusive,exclusive));
			}
			else
			{
				return this.DataProvider.CustomQuery(typeof(Module), new PagerCondition(string.Format("select {0} from TBLMDL where MDLCODE in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE like '{1}%' )", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), userGroupCode),"MDLCODE",inclusive,exclusive));
			}
		}

		#region UserGroup --> Module
		/// <summary>
		/// ** 功能描述:	由UserGroupCode获得Module
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-03-22 11:36:52
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userGroupCode">UserGroupCode,精确查询</param>
		/// <returns>Module数组</returns>
		public object[] GetModuleByUserGroupCode(string userGroupCode)
		{
			return this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(string.Format("select {0} from TBLMDL where MDLCODE in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE='{1}')", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), userGroupCode)));
		}

		/// <summary>
		/// ** 功能描述:	由UserGroupCode获得属于UserGroup的Module的数量
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-03-22 11:36:52
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userGroupCode">UserGroupCode,精确查询</param>
		/// <param name="moduleCode">ModuleCode,模糊查询</param>
		/// <returns>Module的数量</returns>
		public int GetSelectedModuleByUserGroupCodeCount(string userGroupCode, string moduleCode)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLUSERGROUP2MODULE where USERGROUPCODE ='{0}' and MDLCODE like '{1}%'", userGroupCode, moduleCode)));
		}

		public int GetSelectedModuleByUserGroupCodeCount(string userGroupCode,string moduleType, string moduleCode,string moduleDesc)
		{
			string Condition = string.Empty;
			if(moduleType != null && moduleType!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLTYPE = '{0}' ",moduleType);
			}
			if(moduleCode != null && moduleCode!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLCODE like '{0}%' ",moduleCode);
			}
			if(moduleDesc != null && moduleDesc!=string.Empty)
			{
				Condition += string.Format(" and TBLMDL.MDLDESC like '{0}%' ",moduleDesc);
			}
			string sql =string.Format("select Count(TBLMDL.MDLCODE) from TBLUSERGROUP2MODULE, TBLMDL where TBLUSERGROUP2MODULE.MDLCODE = TBLMDL.MDLCODE and TBLUSERGROUP2MODULE.USERGROUPCODE ='{0}' {1}", userGroupCode, Condition);

			return this.DataProvider.GetCount(new SQLCondition(sql));
		}

		public void UpdateSelectModuleInUserGroup(string userGroupCode, Hashtable htSelected, string userCode)
		{
			if (htSelected == null)
				return;
			Hashtable htChild = new Hashtable();
			foreach (object objModule in htSelected.Keys)
			{
				string[] savedObj = (string[])htSelected[objModule];
				string strModule = objModule.ToString();
				string strParentModule = savedObj[0];
				string strRightString = savedObj[1];
				ArrayList listTmp = new ArrayList();
				if (htChild.ContainsKey(strParentModule) == true)
					listTmp = (ArrayList)htChild[strParentModule];
				else
					htChild.Add(strParentModule, listTmp);
				listTmp.Add(new string[]{strModule, strParentModule, strRightString});
			}
			this.DataProvider.BeginTransaction();
			try
			{
				UpdateSelectModuleInUserGroupByModule(userGroupCode, string.Empty, htChild, userCode);
				this.DataProvider.CommitTransaction();
			}
			catch (Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				throw ex;
			}
		}
		private void UpdateSelectModuleInUserGroupByModule(string userGroupCode, string parentModuleCode, Hashtable htChild, string userCode)
		{
			ArrayList listChild = (ArrayList)htChild[parentModuleCode];
			if (listChild == null || listChild.Count == 0)
				return;
			for (int i = 0; i < listChild.Count; i++)
			{
				string[] savedObj = (string[])listChild[i];
				string strModule = savedObj[0];
				string strRightString = savedObj[2];
				
				/*
				string strDeleteExist = "delete from TBLUSERGROUP2MODULE where USERGROUPCODE='" + userGroupCode + "' and MDLCODE='" + strModule + "' ";
				this.DataProvider.CustomExecute(new SQLCondition(strDeleteExist));
				*/
				object objMdl = this.GetUserGroup2Module(userGroupCode, strModule);
				if (objMdl != null)
				{
					this.DeleteUserGroup2Module((UserGroup2Module)objMdl);
				}
				
				if (strRightString != "" && strRightString != "0")
				{
					UserGroup2Module userGroup2Module = new UserGroup2Module();
					userGroup2Module.UserGroupCode = userGroupCode;
					userGroup2Module.ModuleCode = strModule;
					userGroup2Module.ViewValue = strRightString;
					userGroup2Module.MaintainUser = userCode;
					this.AddUserGroup2Module(userGroup2Module);
				}
				
				ArrayList listTmp = (ArrayList)htChild[strModule];
				if (listTmp != null)
				{
					UpdateSelectModuleInUserGroupByModule(userGroupCode, strModule, htChild, userCode);
				}
				else
				{
					if (objMdl == null || ((UserGroup2Module)objMdl).ViewValue != strRightString)
					{
						UpdateSelectModuleInUserGroupByParentModule(userGroupCode, strModule, strRightString, userCode);
					}
				}
			}
		}
		private void UpdateSelectModuleInUserGroupByParentModule(string userGroupCode, string parentModuleCode, string viewValue, string userCode)
		{
			object[] objs = this.GetSubModulesByModuleCode(parentModuleCode);
			if (objs == null || objs.Length == 0)
				return;
			for (int i = 0; i < objs.Length; i++)
			{
				UserGroup2Module userGroup2Module = new UserGroup2Module();
				userGroup2Module.UserGroupCode = userGroupCode;
				userGroup2Module.ModuleCode = ((Module)objs[i]).ModuleCode;
				userGroup2Module.ViewValue = viewValue;
				userGroup2Module.MaintainUser = userCode;
				
				string strDeleteExist = "delete from TBLUSERGROUP2MODULE where USERGROUPCODE='" + userGroupCode + "' and MDLCODE='" + ((Module)objs[i]).ModuleCode + "' ";
				this.DataProvider.CustomExecute(new SQLCondition(strDeleteExist));
				
				if (viewValue != "" && viewValue != "0")
				{
					this.AddUserGroup2Module(userGroup2Module);
				}
				UpdateSelectModuleInUserGroupByParentModule(userGroupCode, ((Module)objs[i]).ModuleCode, viewValue, userCode);
			}
		}
		public string GetQueryParentList(string moduleType, string moduleCode, string moduleDesc, string formUrl, string showExistModule, string userGroupCode)
		{
			if (moduleType == string.Empty && moduleCode == string.Empty && moduleDesc == string.Empty && formUrl == string.Empty && (showExistModule == string.Empty || Convert.ToBoolean(showExistModule) == false))
				return "";
			ArrayList listParent = new ArrayList();
			string strSql = "select * from TBLMDL where 1=1 ";
			if (moduleType != string.Empty)
				strSql += " and MDLTYPE='" + moduleType + "' ";
			if (moduleCode != string.Empty)
				strSql += " and MDLCODE LIKE '" + moduleCode + "%' ";
			if (moduleDesc != string.Empty)
				strSql += " and MDLDESC LIKE '" + moduleDesc + "%' ";
			if (formUrl != string.Empty)
				strSql += " and UPPER(FORMURL) LIKE '%" + formUrl + "%' ";
			if (!(moduleType == string.Empty && moduleCode == string.Empty && moduleDesc == string.Empty && formUrl == string.Empty))
			{
				object[] objs = this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(strSql));
				if (objs == null || objs.Length == 0)
					return "";
				for (int i = 0; i < objs.Length; i++)
				{
					Module mdl = (Module)objs[i];
					if (listParent.Contains(mdl.ParentModuleCode) == false)
						listParent.Add(mdl.ParentModuleCode);
					GetQueryParentList(mdl.ParentModuleCode, listParent);
				}
			}
			if (showExistModule != string.Empty && Convert.ToBoolean(showExistModule) == true)
			{
				strSql = string.Format("select distinct pmdlcode from tblmdl where pmdlcode in (select pmdlcode from tblmdl where mdlcode in (select mdlcode from tblusergroup2module where usergroupcode='{0}')) and (pmdlcode,mdlcode) not in (select mdl.pmdlcode,mdl.mdlcode from tblusergroup2module ug2m,tblmdl mdl where ug2m.mdlcode=mdl.mdlcode and ug2m.usergroupcode='{0}')", userGroupCode);
				object[] objsTmp = this.DataProvider.CustomQuery(typeof(Module), new SQLCondition(strSql));
				if (objsTmp != null)
				{
					for (int i = 0; i < objsTmp.Length; i++)
					{
						Module mdl = (Module)objsTmp[i];
						if (listParent.Contains(mdl.ParentModuleCode) == false)
							listParent.Add(mdl.ParentModuleCode);
					}
				}
			}
			if (listParent.Count == 0)
				return "";
			string[] strListTmp = new string[listParent.Count];
			listParent.CopyTo(strListTmp);
			string strRet = string.Join(";", strListTmp);
			return strRet;
		}
		private void GetQueryParentList(string moduleCode, ArrayList listParent)
		{
			if (moduleCode == string.Empty)
				return;
			object obj = this.GetModule(moduleCode);
			if (obj == null)
				return;
			Module mdl = (Module)obj;
			if (listParent.Contains(mdl.ParentModuleCode) == false)
				listParent.Add(mdl.ParentModuleCode);
			GetQueryParentList(mdl.ParentModuleCode, listParent);
		}

		/// <summary>
		/// ** 功能描述:	由UserGroupCode获得属于UserGroup的Module，分页
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-03-22 11:36:52
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userGroupCode">UserGroupCode,精确查询</param>
		/// <param name="moduleCode">ModuleCode,模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns>Module数组</returns>
		public object[] GetSelectedModuleByUserGroupCode(string userGroupCode, string moduleCode, int inclusive, int exclusive)
		{
			return this.DataProvider.CustomQuery(typeof(Module), 
				new PagerCondition(string.Format("select {0} from TBLMDL where MDLCODE in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE ='{1}') and MDLCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), userGroupCode, moduleCode),"MDLCODE", inclusive, exclusive));
		}
		
		/// <summary>
		/// ** 功能描述:	由UserGroupCode获得不属于UserGroup的Module的数量
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-03-22 11:36:52
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userGroupCode">UserGroupCode,精确查询</param>
		/// <param name="moduleCode">ModuleCode,模糊查询</param>
		/// <returns>Module的数量</returns>
		public int GetUnselectedModuleByUserGroupCodeCount(string userGroupCode, string moduleCode)
		{
			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMDL where MDLCODE not in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE ='{0}') and MDLCODE like '{1}%'", userGroupCode, moduleCode)));
		}

		/// <summary>
		/// ** 功能描述:	由UserGroupCode获得不属于UserGroup的Module，分页
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-03-22 11:36:52
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userGroupCode">UserGroupCode,精确查询</param>
		/// <param name="moduleCode">ModuleCode,模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns>Module数组</returns>
		public object[] GetUnselectedModuleByUserGroupCode(string userGroupCode, string moduleCode, int inclusive, int exclusive)
		{
			return this.DataProvider.CustomQuery(typeof(Module), 
				new PagerCondition(string.Format("select {0} from TBLMDL where MDLCODE not in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE ='{1}') and MDLCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), userGroupCode, moduleCode),"MDLCODE", inclusive, exclusive));
		}

		/// <summary>
		/// ** 功能描述:	由UserGroupCode获得不属于UserGroup的Module，分页
		/// ** 作 者:		Simone Xu
		/// ** 日 期:		2005-11-17 
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userGroupCode">UserGroupCode,精确查询</param>
		/// <param name="moduleCode">ModuleCode,模糊查询</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns>Module数组</returns>
		public object[] GetUnselectedModuleByUserGroupCode(string userGroupCode,string moduleType, string moduleCode,string moduleDesc, int inclusive, int exclusive)
		{
			string Condition = string.Empty;
			if(moduleType != null && moduleType!=string.Empty)
			{
				Condition += string.Format(" and MDLTYPE = '{0}' ",moduleType);
				if(moduleType == "B/S")
				{
					Condition += string.Format(" AND FORMURL is not null ");
				}
			}
			if(moduleCode != null && moduleCode!=string.Empty)
			{
				Condition += string.Format(" and MDLCODE like '{0}%' ",moduleCode);
			}
			if(moduleDesc != null && moduleDesc!=string.Empty)
			{
				Condition += string.Format(" and MDLDESC like '{0}%' ",moduleDesc);
			}
			string sql =string.Format("select {0} from TBLMDL where MDLCODE not in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE ='{1}') {2}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), userGroupCode, Condition);
			return this.DataProvider.CustomQuery(typeof(Module), 
				new PagerCondition(sql,"MDLCODE", inclusive, exclusive));
		}


		/// <summary>
		/// ** 功能描述:	由UserGroupCode获得不属于UserGroup的Module的数量
		/// ** 作 者:		Simone Xu
		/// ** 日 期:		2005-11-17 
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="userGroupCode">UserGroupCode,精确查询</param>
		/// <param name="moduleCode">ModuleCode,模糊查询</param>
		/// <returns>Module的数量</returns>
		public int GetUnselectedModuleByUserGroupCodeCount(string userGroupCode,string moduleType, string moduleCode,string moduleDesc)
		{
			string Condition = string.Empty;
			if(moduleType != null && moduleType!=string.Empty)
			{
				Condition += string.Format(" and MDLTYPE = '{0}' ",moduleType);
			}
			if(moduleCode != null && moduleCode!=string.Empty)
			{
				Condition += string.Format(" and MDLCODE like '{0}%' ",moduleCode);
			}
			if(moduleDesc != null && moduleDesc!=string.Empty)
			{
				Condition += string.Format(" and MDLDESC like '{0}%' ",moduleDesc);
			}
			string sql =string.Format("select Count(MDLCODE) from TBLMDL where MDLCODE not in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE ='{0}') {1}",  userGroupCode, Condition);
			return this.DataProvider.GetCount(new SQLCondition(sql));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userGroupCode"></param>
		/// <param name="moduleType"></param>
		/// <param name="moduleCode"></param>
		/// <param name="pmdlcode"></param>
		/// <param name="moduleDesc"></param>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns></returns>
		public object[] GetUnselectedModuleByUserGroupCode(string userGroupCode,string moduleType, string moduleCode, string pmdlcode, string moduleDesc, int inclusive, int exclusive)
		{
			string Condition = string.Empty;
			if(moduleType != null && moduleType!=string.Empty)
			{
				Condition += string.Format(" and MDLTYPE = '{0}'  AND FORMURL is not null ",moduleType);
			}
			if(moduleCode != null && moduleCode!=string.Empty)
			{
				if(pmdlcode != null && pmdlcode!=string.Empty)
				{
					Condition += string.Format(" and MDLCODE like '{0}%' and PMDLCODE like '{1}%' ", moduleCode, pmdlcode);
				}
				else
				{
					Condition += string.Format(" and MDLCODE like '{0}%' ",moduleCode);
				}
			}
			else
			{
				if(pmdlcode != null && pmdlcode!=string.Empty)
				{
					object[] mdls = GetSubModulesByParentModuleCode(pmdlcode) ;//模糊查询
					ArrayList array = new ArrayList();

					if(mdls!=null && mdls.Length>0)
					{
						for(int i=0; i<mdls.Length; i++)
						{
							array.Add((mdls[i] as Module).ModuleCode);
							GetAllSubModules( array, (mdls[i] as Module).ModuleCode );
						}
					}

					if( array.Count>0 )
					{
						string[] mdlcodes = (string[])array.ToArray(typeof(string));
						Condition += string.Format(" and MDLCODE in ({0}) ", FormatHelper.ProcessQueryValues(mdlcodes) );
					}
					else
					{
						return null ;
					}
				}
			}

			if(moduleDesc != null && moduleDesc!=string.Empty)
			{
				Condition += string.Format(" and MDLDESC like '{0}%' ",moduleDesc);
			}

			string sql =string.Format("select {0} from TBLMDL where MDLCODE not in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE ='{1}') {2}", 
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(Module)), userGroupCode, Condition);
			return this.DataProvider.CustomQuery(typeof(Module), 
				new PagerCondition(sql,"PMDLCODE, MDLSEQ", inclusive, exclusive));
		}

		private void GetAllSubModules(ArrayList array, string pmdlcode )
		{
			if(CheckHasSon(pmdlcode))
			{
				object[] mdls = GetSubModulesByModuleCode(pmdlcode);
				if(mdls!=null && mdls.Length>0)
				{
					for(int i=0; i<mdls.Length; i++)
					{
						array.Add( (mdls[i] as Module).ModuleCode );
						GetAllSubModules(array, (mdls[i] as Module).ModuleCode);
					}
				}
			}
		}

		private bool CheckHasSon( string parentModuleCode )
		{
			string sql = " select count(*) from tblmdl where pmdlcode=$PMDLCODE ";
			int count = this.DataProvider.GetCount(
				new SQLParamCondition(sql, new SQLParameter[]{new SQLParameter("PMDLCODE",typeof(String),parentModuleCode)}));
			return count>0 ? true : false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="userGroupCode"></param>
		/// <param name="moduleType"></param>
		/// <param name="moduleCode"></param>
		/// <param name="pmdlcode"></param>
		/// <param name="moduleDesc"></param>
		/// <returns></returns>
		public int GetUnselectedModuleByUserGroupCodeCount(string userGroupCode,string moduleType, string moduleCode, string pmdlcode,string moduleDesc)
		{
			string Condition = string.Empty;
			if(moduleType != null && moduleType!=string.Empty)
			{
				Condition += string.Format(" and MDLTYPE = '{0}'  AND FORMURL is not null ",moduleType);
			}
			if(moduleCode != null && moduleCode!=string.Empty)
			{
				if(pmdlcode != null && pmdlcode!=string.Empty)
				{
					Condition += string.Format(" and MDLCODE like '{0}%' and PMDLCODE like '{1}%' ", moduleCode, pmdlcode);
				}
				else
				{
					Condition += string.Format(" and MDLCODE like '{0}%' ",moduleCode);
				}
			}
			else
			{
				if(pmdlcode != null && pmdlcode!=string.Empty)
				{
					object[] mdls = GetSubModulesByParentModuleCode(pmdlcode) ;//模糊查询
					ArrayList array = new ArrayList();

					if(mdls!=null && mdls.Length>0)
					{
						for(int i=0; i<mdls.Length; i++)
						{
							array.Add((mdls[i] as Module).ModuleCode);
							GetAllSubModules( array, (mdls[i] as Module).ModuleCode );
						}
					}

					if( array.Count>0 )
					{
						string[] mdlcodes = (string[])array.ToArray(typeof(string));
						Condition += string.Format(" and MDLCODE in ({0}) ", FormatHelper.ProcessQueryValues(mdlcodes) );
					}
					else
					{
						return 0;
					}
				}
			}

			if(moduleDesc != null && moduleDesc!=string.Empty)
			{
				Condition += string.Format(" and MDLDESC like '{0}%' ",moduleDesc);
			}

			string sql =string.Format("select Count(MDLCODE) from TBLMDL where MDLCODE not in ( select MDLCODE from TBLUSERGROUP2MODULE where USERGROUPCODE ='{0}') {1}",  userGroupCode, Condition);
			return this.DataProvider.GetCount(new SQLCondition(sql));
		}
		#endregion 

		#endregion

        #region UserGroup2FunctionGroup

        public UserGroup2FunctionGroup CreateNewUserGroup2FunctionGroup()
        {
            return new UserGroup2FunctionGroup();
        }

        public void AddUserGroup2FunctionGroup(UserGroup2FunctionGroup userGroup2FunctionGroup)
        {
            this._helper.AddDomainObject(userGroup2FunctionGroup);
        }

        public void AddUserGroup2FunctionGroup(UserGroup2FunctionGroup[] userGroup2FunctionGroups)
        {
            foreach (UserGroup2FunctionGroup group2Group in userGroup2FunctionGroups)
            {
                this._helper.AddDomainObject(group2Group);
            }
        }

        public void UpdateUserGroup2FunctionGroup(UserGroup2FunctionGroup userGroup2FunctionGroup)
        {
            this._helper.UpdateDomainObject(userGroup2FunctionGroup);
        }

        public void DeleteUserGroup2FunctionGroup(UserGroup2FunctionGroup userGroup2FunctionGroup)
        {
            this._helper.DeleteDomainObject(userGroup2FunctionGroup);
        }

        public void DeleteUserGroup2FunctionGroup(UserGroup2FunctionGroup[] userGroup2FunctionGroup)
        {
            this._helper.DeleteDomainObject(userGroup2FunctionGroup);
        }

        public object GetUserGroup2FunctionGroup(string userGroupCode, string functionGroupCode)
        {
            return this.DataProvider.CustomSearch(typeof(UserGroup2FunctionGroup), new object[] { userGroupCode, functionGroupCode });
        }

        public int GetSelectedFunctionGroupByUserGroupCodeCount(string userGroupCode, string functionGroupCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLUSERGROUP2FUNCTIONGROUP where USERGROUPCODE ='{0}' and FUNCTIONGROUPCODE like '{1}%'", userGroupCode, functionGroupCode)));
        }

        public object[] GetSelectedFunctionGroupByUserGroupCode(string userGroupCode, string functionGroupCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(FunctionGroup),
                new PagerCondition(string.Format("select {0} from TBLFUNCTIONGROUP where FUNCTIONGROUPCODE in ( select FUNCTIONGROUPCODE from TBLUSERGROUP2FUNCTIONGROUP where USERGROUPCODE ='{1}') and FUNCTIONGROUPCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FunctionGroup)), userGroupCode, functionGroupCode), inclusive, exclusive));
        }

        public int GetUnselectedFunctionGroupByUserGroupCodeCount(string userGroupCode, string functionGroupCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLFUNCTIONGROUP where FUNCTIONGROUPCODE not in ( select FUNCTIONGROUPCODE from TBLUSERGROUP2FUNCTIONGROUP where USERGROUPCODE ='{0}') and FUNCTIONGROUPCODE like '{1}%'", userGroupCode, functionGroupCode)));
        }

        public object[] GetUnselectedFunctionGroupByUserGroupCode(string userGroupCode, string functionGroupCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(FunctionGroup),
                new PagerCondition(string.Format("select {0} from TBLFUNCTIONGROUP where FUNCTIONGROUPCODE not in ( select FUNCTIONGROUPCODE from TBLUSERGROUP2FUNCTIONGROUP where USERGROUPCODE ='{1}') and FUNCTIONGROUPCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FunctionGroup)), userGroupCode, functionGroupCode), inclusive, exclusive));
        }

        #endregion

        #region FunctionGroup

        /// <summary>
        /// 
        /// </summary>
        public FunctionGroup CreateNewFunctionGroup()
        {
            return new FunctionGroup();
        }

        public void AddFunctionGroup(FunctionGroup functionGroup)
        {
            this._helper.AddDomainObject(functionGroup);
        }

        public void UpdateFunctionGroup(FunctionGroup functionGroup)
        {
            this._helper.UpdateDomainObject(functionGroup);
        }

        public void DeleteFunctionGroup(FunctionGroup functionGroup)
        {
            this._helper.DeleteDomainObject(functionGroup,
                new ICheck[]{ new DeleteAssociateCheck( functionGroup,
								this.DataProvider, 
								new Type[]{
											  typeof(FunctionGroup2Function),
											  typeof(UserGroup2FunctionGroup)	})});
        }

        public void DeleteFunctionGroup(FunctionGroup[] functionGroup)
        {
            this._helper.DeleteDomainObject(functionGroup,
                new ICheck[]{ new DeleteAssociateCheck( functionGroup,
								this.DataProvider, 
								new Type[]{
											  typeof(FunctionGroup2Function),
											  typeof(UserGroup2FunctionGroup)	})});
        }

        public object GetFunctionGroup(string functionGroupCode)
        {
            return this.DataProvider.CustomSearch(typeof(FunctionGroup), new object[] { functionGroupCode });
        }

        public int QueryFunctionGroupCount(string functionGroupCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLFUNCTIONGROUP where 1=1 and FUNCTIONGROUPCODE like '{0}%' ", functionGroupCode)));
        }

        public object[] QueryFunctionGroup(string functionGroupCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(FunctionGroup), new PagerCondition(string.Format("select {0} from TBLFUNCTIONGROUP where 1=1 and FUNCTIONGROUPCODE like '{1}%' order by FUNCTIONGROUPCODE ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(FunctionGroup)), functionGroupCode), inclusive, exclusive));
        }

        public object[] GetAllFunctionGroup()
        {
            return this.DataProvider.CustomQuery(typeof(FunctionGroup), new SQLCondition(string.Format("select {0} from TBLFUNCTIONGROUP order by FUNCTIONGROUPCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FunctionGroup)))));
        }

        public object[] GetAllFunctionGroup(string functionCode)
        {
            return this.DataProvider.CustomQuery(typeof(FunctionGroup), new SQLCondition(string.Format("select {0} from TBLFUNCTIONGROUP where FUNCTIONGROUPCODE in ( select FUNCTIONGROUPCODE from TBLFUNCTIONGROUP2FUNCTION where MDLCODE ='" + functionCode + "') order by FUNCTIONGROUPCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(FunctionGroup)))));
        }

        #endregion

        #region FunctionGroup2Function
        public FunctionGroup2Function CreateNewFunctionGroup2Function()
        {
            return new FunctionGroup2Function();
        }

        public void AddFunctionGroup2Function(FunctionGroup2Function functionGroup2Function)
        {
            this._helper.AddDomainObject(functionGroup2Function);
        }

        public void AddFunctionGroup2Function(FunctionGroup2Function[] functionGroup2Functions)
        {
            foreach (FunctionGroup2Function function in functionGroup2Functions)
            {
                this._helper.AddDomainObject(function);
            }
        }

        public void UpdateFunctionGroup2Function(FunctionGroup2Function functionGroup2Function)
        {
            this._helper.UpdateDomainObject(functionGroup2Function);
        }

        public void DeleteFunctionGroup2Function(FunctionGroup2Function functionGroup2Function)
        {
            this._helper.DeleteDomainObject(functionGroup2Function);
        }

        public void DeleteFunctionGroup2Function(FunctionGroup2Function[] functionGroup2Functions)
        {
            this._helper.DeleteDomainObject(functionGroup2Functions);
        }

        public object GetFunctionGroup2Function(string functionGroupCode, string moduleCode)
        {
            return this.DataProvider.CustomSearch(typeof(FunctionGroup2Function), new object[] { moduleCode, functionGroupCode });
        }

        public void UpdateSelectModuleInFunctionGroup(string functionGroupCode, Hashtable htSelected, string userCode)
        {
            if (htSelected == null)
                return;
            Hashtable htChild = new Hashtable();
            foreach (object objModule in htSelected.Keys)
            {
                string[] savedObj = (string[])htSelected[objModule];
                string strModule = objModule.ToString();
                string strParentModule = savedObj[0];
                string strRightString = savedObj[1];
                ArrayList listTmp = new ArrayList();
                if (htChild.ContainsKey(strParentModule) == true)
                    listTmp = (ArrayList)htChild[strParentModule];
                else
                    htChild.Add(strParentModule, listTmp);
                listTmp.Add(new string[] { strModule, strParentModule, strRightString });
            }
            this.DataProvider.BeginTransaction();
            try
            {
                UpdateSelectModuleInFunctionGroupByModule(functionGroupCode, string.Empty, htChild, userCode);
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        private void UpdateSelectModuleInFunctionGroupByModule(string functionGroupCode, string parentModuleCode, Hashtable htChild, string userCode)
        {
            ArrayList listChild = (ArrayList)htChild[parentModuleCode];
            if (listChild == null || listChild.Count == 0)
                return;
            for (int i = 0; i < listChild.Count; i++)
            {
                string[] savedObj = (string[])listChild[i];
                string strModule = savedObj[0];
                string strRightString = savedObj[2];

                object objMdl = this.GetFunctionGroup2Function(functionGroupCode, strModule);
                if (objMdl != null)
                {
                    this.DeleteFunctionGroup2Function((FunctionGroup2Function)objMdl);
                }

                if (strRightString != "" && strRightString != "0")
                {
                    FunctionGroup2Function functionGroup2Function = new FunctionGroup2Function();
                    functionGroup2Function.FunctionGroupCode = functionGroupCode;
                    functionGroup2Function.ModuleCode = strModule;
                    functionGroup2Function.ViewValue = strRightString;
                    functionGroup2Function.MaintainUser = userCode;
                    this.AddFunctionGroup2Function(functionGroup2Function);
                }

                ArrayList listTmp = (ArrayList)htChild[strModule];
                if (listTmp != null)
                {
                    UpdateSelectModuleInFunctionGroupByModule(functionGroupCode, strModule, htChild, userCode);
                }
                else
                {
                    if (objMdl == null || ((FunctionGroup2Function)objMdl).ViewValue != strRightString)
                    {
                        UpdateSelectModuleInFunctionGroupByParentModule(functionGroupCode, strModule, strRightString, userCode);
                    }
                }
            }
        }

        private void UpdateSelectModuleInFunctionGroupByParentModule(string functionGroupCode, string parentModuleCode, string viewValue, string userCode)
        {
            object[] objs = this.GetSubModulesByModuleCode(parentModuleCode);
            if (objs == null || objs.Length == 0)
                return;
            for (int i = 0; i < objs.Length; i++)
            {
                FunctionGroup2Function functionGroup2Function = new FunctionGroup2Function();
                functionGroup2Function.FunctionGroupCode = functionGroupCode;
                functionGroup2Function.ModuleCode = ((Module)objs[i]).ModuleCode;
                functionGroup2Function.ViewValue = viewValue;
                functionGroup2Function.MaintainUser = userCode;

                string strDeleteExist = "delete from TBLFUNCTIONGROUP2FUNCTION where FUNCTIONGROUPCODE='" + functionGroupCode + "' and MDLCODE='" + ((Module)objs[i]).ModuleCode + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strDeleteExist));

                if (viewValue != "" && viewValue != "0")
                {
                    this.AddFunctionGroup2Function(functionGroup2Function);
                }
                UpdateSelectModuleInFunctionGroupByParentModule(functionGroupCode, ((Module)objs[i]).ModuleCode, viewValue, userCode);
            }
        }

        public object[] GetSelectedModuleWithViewValueByFunctionGroupCode(string functionGroupCode, string moduleCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ModuleWithViewValue),
                new PagerCondition(string.Format("select {0},TBLFUNCTIONGROUP2FUNCTION.VIEWVALUE from TBLFUNCTIONGROUP2FUNCTION, TBLMDL where TBLFUNCTIONGROUP2FUNCTION.MDLCODE = TBLMDL.MDLCODE and TBLFUNCTIONGROUP2FUNCTION.FUNCTIONGROUPCODE ='{1}' and TBLMDL.MDLCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Module)), functionGroupCode, moduleCode), "TBLMDL.PMDLCODE, TBLMDL.MDLSEQ", inclusive, exclusive));
        }

        public object[] GetSelectedModuleWithViewValueByFunctionGroupCode(string functionGroupCode, string moduleType, string moduleCode, string moduleDesc, string parentModuleCode)
        {
            string Condition = string.Empty;
            if (moduleType != null && moduleType != string.Empty)
            {
                Condition += string.Format(" and TBLMDL.MDLTYPE = '{0}' ", moduleType);
            }
            if (moduleCode != null && moduleCode != string.Empty)
            {
                Condition += string.Format(" and TBLMDL.MDLCODE like '{0}%' ", moduleCode);
            }
            if (moduleDesc != null && moduleDesc != string.Empty)
            {
                Condition += string.Format(" and TBLMDL.MDLDESC like '{0}%' ", moduleDesc);
            }
            if (parentModuleCode == string.Empty)
                Condition += " and (TBLMDL.PMDLCODE='' OR TBLMDL.PMDLCODE IS NULL) ";
            else
                Condition += string.Format(" and (TBLMDL.PMDLCODE='' OR TBLMDL.PMDLCODE IS NULL OR TBLMDL.PMDLCODE IN ({0}) ) ", "'" + parentModuleCode.Replace(";", "','") + "'");

            string sql = string.Format("select {0},TBLFUNCTIONGROUP2FUNCTION.VIEWVALUE from TBLFUNCTIONGROUP2FUNCTION, TBLMDL where TBLFUNCTIONGROUP2FUNCTION.MDLCODE = TBLMDL.MDLCODE and TBLFUNCTIONGROUP2FUNCTION.FUNCTIONGROUPCODE ='{1}' {2}", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Module)), functionGroupCode, Condition);
            sql += " ORDER BY TBLMDL.PMDLCODE,TBLMDL.MDLSEQ ";
            return this.DataProvider.CustomQuery(typeof(ModuleWithViewValue),
                new SQLCondition(sql));
        }
        #endregion

        #region SystemError
        /// <summary>
		/// 系统错误记录
		/// </summary>
		public SystemError CreateNewSystemError()
		{
			return new SystemError();
		}

		public void AddSystemError( SystemError systemError)
		{
			this._helper.AddDomainObject( systemError );
		}

		public void UpdateSystemError(SystemError systemError)
		{
			this._helper.UpdateDomainObject( systemError );
		}

		public void UpdateSystemError(SystemError[] systemErrors)
		{
			this.DataProvider.BeginTransaction();

			try
			{
				foreach( SystemError systemError in systemErrors)
				{
					this.UpdateSystemError( systemError );
				}

				this.DataProvider.CommitTransaction();
			}
			catch(Exception e)
			{
				this.DataProvider.RollbackTransaction();					
				
				ExceptionManager.Raise(typeof(SystemError), "$Error_Update_System_Error", e);	
			}
		}

		public void DeleteSystemError(SystemError systemError)
		{
			this._helper.DeleteDomainObject( systemError );
		}

		public void DeleteSystemError(SystemError[] systemError)
		{
			this._helper.DeleteDomainObject( systemError );
		}

		public object GetSystemError( string systemErrorCode )
		{
			return this.DataProvider.CustomSearch(typeof(SystemError), new object[]{ systemErrorCode });
		}

		/// <summary>
		/// ** 功能描述:	查询SystemError的总行数
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-03-23 11:00:22
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="systemErrorCode">SystemErrorCode，模糊查询</param>
		/// <returns> SystemError的总记录数</returns>
		public int QuerySystemErrorCount( string systemErrorCode, string isResolved, int sendBeginDate, int sendBeginTime, int sendEndDate, int sendEndTime )
		{
			string condition = "";

			if ( isResolved != null && isResolved != string.Empty )
			{
				condition = string.Format( "{0} and ISRES='{1}'", condition, isResolved);
			}

			condition = string.Format("{0} and {1}", condition, new DateTimeRangeCondition("SENDDATE","SENDTIME", sendBeginDate, sendBeginTime, sendEndDate, sendEndTime).SQLText );

			return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLSYSERROR where 1=1 and SYSERRCODE like '{0}%'{1}" , systemErrorCode, condition)));
		}

		/// <summary>
		/// ** 功能描述:	分页查询SystemError
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-03-23 11:00:22
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <param name="systemErrorCode">SystemErrorCode，模糊查询</param>
		/// <param name="isResolved">解决状态，精确查询</param>
		/// <param name="sendBeginDate">提交日期 开始， 为空不作判断条件</param>
		/// <param name="sendBeginTime">提交时间 开始， 为空不作判断条件</param>
		/// <param name="sendEndDate">提交日期 结束， 为空不作判断条件</param>
		/// <param name="sendEndTime">提交时间 结束， 为空不作判断条件</param>
		/// <param name="inclusive">开始行数</param>
		/// <param name="exclusive">结束行数</param>
		/// <returns> SystemError数组</returns>
		public object[] QuerySystemError( string systemErrorCode, string isResolved, int sendBeginDate, int sendBeginTime, int sendEndDate, int sendEndTime, int inclusive, int exclusive )
		{
			string condition = "";

			if ( isResolved != null && isResolved != string.Empty )
			{
				condition = string.Format( "{0} and ISRES='{1}'", condition, isResolved);
			}

			condition = string.Format("{0} and {1}", condition, new DateTimeRangeCondition("SENDDATE","SENDTIME", sendBeginDate, sendBeginTime, sendEndDate, sendEndTime).SQLText );

			return this.DataProvider.CustomQuery(typeof(SystemError), 
				new PagerCondition(string.Format("select {0} from TBLSYSERROR where 1=1 and SYSERRCODE like '{1}%' {2} ", 
				DomainObjectUtility.GetDomainObjectFieldsString(typeof(SystemError)) , 
				systemErrorCode, 
				condition),
				"SENDDATE DESC, SENDTIME DESC",
				inclusive, 
				exclusive));
		}

		/// <summary>
		/// ** 功能描述:	获得所有的SystemError
		/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
		/// ** 日 期:		2005-03-23 11:00:22
		/// ** 修 改:
		/// ** 日 期:
		/// </summary>
		/// <returns>SystemError的总记录数</returns>
		public object[] GetAllSystemError()
		{
			return this.DataProvider.CustomQuery(typeof(SystemError), new SQLCondition(string.Format("select {0} from TBLSYSERROR", DomainObjectUtility.GetDomainObjectFieldsString(typeof(SystemError)))));
		}


		#endregion

		#region Module Tree
		/// <summary>
		/// ** 功能描述:	构建Module树
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// ** nunit
		/// </summary>
		/// <returns>根节点，ModuleCode为""</returns>
		public ITreeObjectNode BuildModuleTree()
		{			
			Module module = new Module();
			module.ModuleCode = "";

			ModuleTreeNode node = new ModuleTreeNode(module);

			object[] objs = this.GetAllModulesOrderBySequence();

			node.AddSubTreeObjectNodeRange( this.buildSubModuleTree(node, objs) );

			return node;
		}

		/// <summary>
		/// ** 功能描述:	构建parent的下一级模块
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:			一次取出所有的Module
		/// ** 日 期:  		2005-04-19
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="modules"></param>
		/// <returns></returns>
		private ITreeObjectNode[] buildSubModuleTree( ModuleTreeNode parent, object[] modules )
		{
			object[] objs = this.GetSubModuleFromModuleList( parent.Module.ModuleCode, modules );

			if ( objs != null )
			{
				ArrayList array = new ArrayList( objs.Length );
				ModuleTreeNode node = null;

				foreach ( Module module in objs )
				{
					node = new ModuleTreeNode(module, parent);
					node.AddSubTreeObjectNodeRange( this.buildSubModuleTree(node, modules) ); 

					array.Add( node );
				}

				return (ITreeObjectNode[])array.ToArray(typeof(ModuleTreeNode));
			}
		
			return null;
		}

		/// <summary>
		/// ** 功能描述:	由父模块代码和包含所有模块的数组获得父模块下一级的模块
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-04-19
		/// ** 修 改:		
		/// ** 日 期:  		
		/// </summary>
		/// <param name="parentModuleCode"></param>
		/// <param name="modules"></param>
		/// <returns></returns>
		private Module[] GetSubModuleFromModuleList(string parentModuleCode, object[] modules)
		{
			ArrayList array = new ArrayList();

			foreach( Module module in modules )
			{
				if ( module.ParentModuleCode.ToUpper() == parentModuleCode.ToUpper() )
				{
					array.Add( module );
				}
			}

			return (Module[])array.ToArray(typeof(Module));
		}
		#endregion

		#region Menu Tree

		/// <summary>
		/// ** 功能描述:	构建Menu树
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// ** nunit
		/// </summary>
		/// <returns>根节点，MenuCode为""</returns>
		public ITreeObjectNode BuildMenuTree()
		{
			MenuWithUrl menu = new MenuWithUrl();
			menu.MenuCode = "";

			MenuTreeNode node = new MenuTreeNode(menu);

			object[] objs = GetAllMenuWithUrl();

			if ( objs != null )
			{
				node.AddSubTreeObjectNodeRange( this.buildSubMenuTree(node, objs) );
			}

			return node;
		}
        //melo zheng,2007.5.14
        public ITreeObjectNode BuildMenuTreeRPT()
        {
            MenuWithUrl menu = new MenuWithUrl();
            menu.MenuCode = "REPORT";

            MenuTreeNode node = new MenuTreeNode(menu);

            object[] objs = GetAllMenuWithUrlRPT();

            if (objs != null)
            {
                node.AddSubTreeObjectNodeRange(this.buildSubMenuTree(node, objs));
            }

            return node;
        }

		/// <summary>
		/// ** 功能描述:	构建Menu树
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// ** nunit
		/// </summary>
		/// <returns>根节点，MenuCode为""</returns>
		public ITreeObjectNode BuildMenuTreeCS(string userCode)
		{
			MenuWithUrl menu = new MenuWithUrl();
			menu.MenuCode = "CS";	// C/S根模块代码

			MenuTreeNode node = new MenuTreeNode(menu);

			object[] objs = GetAllMenuWithUrlWithTypePermission(MenuType.MenuType_CS, userCode);

			if ( objs != null )
			{
				node.AddSubTreeObjectNodeRange( this.buildSubMenuTree(node, objs) );
			}

			return node;
		}

        /// <summary>
        /// ** 功能描述:	构建Menu树PDA
        /// ** 作 者:		Jarvis Chen
        /// ** 日 期:		2012-04-27
        /// ** 修 改:
        /// ** 日 期: 
        /// ** nunit
        /// </summary>
        /// <returns>根节点，MenuCode为""</returns>
        public ITreeObjectNode BuildMenuTreePDA(string userCode)
        {
            MenuWithUrl menu = new MenuWithUrl();
            menu.MenuCode = MenuType.MenuType_PDA;	// PDA根模块代码

            MenuTreeNode node = new MenuTreeNode(menu);

            object[] objs = GetAllMenuWithUrlWithTypePermission(MenuType.MenuType_PDA, userCode);

            if (objs != null)
            {
                node.AddSubTreeObjectNodeRange(this.buildSubMenuTree(node, objs));
            }

            return node;
        }

		/// <summary>
		/// ** 功能描述:	构建Menu树
		/// ** 作 者:		Laws Lu
		/// ** 日 期:		2006-10-23
		/// ** 修 改:
		/// ** 日 期: 
		/// ** nunit
		/// </summary>
		/// <param name="menucode">菜单代码</param>
		/// <returns>根节点，MenuCode为""</returns>
		public ITreeObjectNode BuildMenuTree(string menucode)
		{
			MenuWithUrl menu = new MenuWithUrl();
			menu.MenuCode = menucode;

			MenuTreeNode node = new MenuTreeNode(menu);

			object[] objs = GetMenuWithUrlByMenuCode(menucode);

			if ( objs != null )
			{
				node.AddSubTreeObjectNodeRange( this.buildSubMenuTree(node, objs) );
			}

			return node;
		}

		private ITreeObjectNode[] buildSubMenuTree( MenuTreeNode parent,object[] menus )
		{
			object[] objs = this.GetSubMenuWithUrlsFromMenuList( parent.MenuWithUrl.MenuCode, menus );

			if ( objs != null )
			{
				ArrayList array = new ArrayList( objs.Length );
				MenuTreeNode node = null;

				foreach ( MenuWithUrl menu in objs )
				{
					node = new MenuTreeNode(menu, parent);
					node.AddSubTreeObjectNodeRange( this.buildSubMenuTree(node, menus) ); 

					array.Add( node );
				}

				return (ITreeObjectNode[])array.ToArray(typeof(MenuTreeNode));
			}
		
			return null;
		}

		private MenuWithUrl[] GetSubMenuWithUrlsFromMenuList(string parentMenuCode, object[] menus)
		{
			ArrayList array = new ArrayList();

			foreach( MenuWithUrl menu in menus )
			{
				if ( menu.ParentMenuCode.ToUpper() == parentMenuCode.ToUpper() )
				{
					array.Add( menu );
				}
			}

			return (MenuWithUrl[])array.ToArray(typeof(MenuWithUrl));
		}
		#endregion

		#region Parameter Tree
		/// <summary>
		/// ** 功能描述:	构建Parameter树
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// ** nunit
		/// </summary>
		/// <returns>根节点，ParameterCode为""</returns>
		public ITreeObjectNode BuildParameterTree()
		{			
			return BuildParameterTree(string.Empty);
		}

		/// <summary>
		/// ** 功能描述:	构建Parameter树
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:
		/// ** 日 期: 
		/// ** nunit
		/// </summary>
		/// <returns>根节点，ParameterCode为""</returns>
		public ITreeObjectNode BuildParameterTree(string parameterGroupCode)
		{			
			Parameter Parameter = new Parameter();
			Parameter.ParameterCode = "";

			ParameterTreeNode node = new ParameterTreeNode(Parameter);

			object[] objs = this.GetAllParametersOrderBySequence(parameterGroupCode);

			node.AddSubTreeObjectNodeRange( this.buildSubParameterTree(node, objs) );

			return node;
		}

		/// <summary>
		/// ** 功能描述:	构建parent的下一级模块
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-03-18
		/// ** 修 改:			一次取出所有的Parameter
		/// ** 日 期:  		2005-04-19
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="Parameters"></param>
		/// <returns></returns>
		private ITreeObjectNode[] buildSubParameterTree( ParameterTreeNode parent, object[] Parameters )
		{
			object[] objs = this.GetSubParameterFromParameterList( parent.Parameter.ParameterCode, Parameters );

			if ( objs != null )
			{
				ArrayList array = new ArrayList( objs.Length );
				ParameterTreeNode node = null;

				foreach ( Parameter Parameter in objs )
				{
					node = new ParameterTreeNode(Parameter, parent);
					node.AddSubTreeObjectNodeRange( this.buildSubParameterTree(node, Parameters) ); 

					array.Add( node );
				}

				return (ITreeObjectNode[])array.ToArray(typeof(ParameterTreeNode));
			}
		
			return null;
		}

		/// <summary>
		/// ** 功能描述:	由父模块代码和包含所有模块的数组获得父模块下一级的模块
		/// ** 作 者:		Jane Shu
		/// ** 日 期:		2005-04-19
		/// ** 修 改:		
		/// ** 日 期:  		
		/// </summary>
		/// <param name="parentParameterCode"></param>
		/// <param name="Parameters"></param>
		/// <returns></returns>
		private Parameter[] GetSubParameterFromParameterList(string parentParameterCode, object[] Parameters)
		{
			if (Parameters == null)
				return null;
			ArrayList array = new ArrayList();

			foreach( Parameter Parameter in Parameters )
			{
				if ( Parameter.ParentParameterCode.ToUpper() == parentParameterCode.ToUpper() )
				{
					array.Add( Parameter );
				}
			}

			return (Parameter[])array.ToArray(typeof(Parameter));
		}
		#endregion

        #region BSHomeSetting

        public BSHomeSetting CreateNewBSHomeSetting()
        {
            return new BSHomeSetting();
        }

        public void AddBSHomeSetting(BSHomeSetting bsHomeSetting)
        {
            this._helper.AddDomainObject(bsHomeSetting);
        }

        public void DeleteBSHomeSetting(BSHomeSetting bsHomeSetting)
        {
            this._helper.DeleteDomainObject(bsHomeSetting);
        }

        public void DeleteBSHomeSetting(BSHomeSetting[] bsHomeSettingArray)
        {
            this._helper.DeleteDomainObject(bsHomeSettingArray);
        }

        public void UpdateBSHomeSetting(BSHomeSetting bsHomeSetting)
        {
            this._helper.UpdateDomainObject(bsHomeSetting);
        }

        public object GetBSHomeSetting(int reportSeq)
        {
            return this.DataProvider.CustomSearch(typeof(BSHomeSetting), new object[] { reportSeq });
        }

        public object[] QueryBSHomeSetting(int reportSeq, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql += string.Format("SELECT {0}  ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BSHomeSetting)));
            sql += GetQueryBSHomeSettingSQL(reportSeq);

            return this.DataProvider.CustomQuery(typeof(BSHomeSetting), new PagerCondition(sql, "reportseq", inclusive, exclusive));
        }

        public int QueryBSHomeSettingCount(int reportSeq)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += GetQueryBSHomeSettingSQL(reportSeq);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetQueryBSHomeSettingSQL(int reportSeq)
        {
            string sql = string.Empty;
            sql += "FROM tblbshomesetting ";
            sql += "WHERE 1 = 1 ";
            if (reportSeq > 0)
            {
                sql += string.Format("AND reportseq = {0} ", reportSeq.ToString());
            }

            return sql;
        }

        #endregion

        #region BSHomeSettingDetail

        public BSHomeSettingDetail CreateNewBSHomeSettingDetail()
        {
            return new BSHomeSettingDetail();
        }

        public void AddBSHomeSettingDetail(BSHomeSettingDetail bsHomeSettingDetail)
        {
            this._helper.AddDomainObject(bsHomeSettingDetail);
        }

        public void DeleteBSHomeSettingDetail(BSHomeSettingDetail bsHomeSettingDetail)
        {
            this._helper.DeleteDomainObject(bsHomeSettingDetail);
        }

        public void DeleteBSHomeSettingDetail(BSHomeSettingDetail[] bsHomeSettingDetailArray)
        {
            this._helper.DeleteDomainObject(bsHomeSettingDetailArray);
        }

        public void UpdateBSHomeSettingDetail(BSHomeSettingDetail bsHomeSettingDetail)
        {
            this._helper.UpdateDomainObject(bsHomeSettingDetail);
        }

        public object GetBSHomeSettingDetail(int reportSeq, string parameterName)
        {
            return this.DataProvider.CustomSearch(typeof(BSHomeSettingDetail), new object[] { reportSeq, parameterName });
        }

        public object[] QueryBSHomeSettingDetail(int reportSeq, string parameterName, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql += string.Format("SELECT {0}  ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BSHomeSettingDetail)));
            sql += GetQueryBSHomeSettingDetailSQL(reportSeq, parameterName);

            return this.DataProvider.CustomQuery(typeof(BSHomeSettingDetail), new PagerCondition(sql, "reportseq, paramname", inclusive, exclusive));
        }

        public int QueryBSHomeSettingDetailCount(int reportSeq, string parameterName)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += GetQueryBSHomeSettingDetailSQL(reportSeq, parameterName);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetQueryBSHomeSettingDetailSQL(int reportSeq, string parameterName)
        {
            string sql = string.Empty;
            sql += "FROM tblbshomesettingdetail ";
            sql += "WHERE 1 = 1 ";
            if (reportSeq > 0)
            {
                sql += string.Format("AND reportseq = {0} ", reportSeq.ToString());
            }

            if (parameterName.Trim().Length > 0)
            {
                sql += string.Format("AND paramname = '{0}' ", parameterName);
            }

            return sql;
        }

        public void GetBSHomeReportURL(int reportSeq, out string moduleCode, out string url)
        {
            moduleCode = string.Empty;
            url = string.Empty;

            Dictionary<string, Type> parameterDic = FormatHelper.GetReportParameterDic();

            if (reportSeq > 0)
            {
                BSHomeSetting setting = (BSHomeSetting)GetBSHomeSetting(reportSeq);
                if (setting != null)
                {
                    moduleCode = setting.ModuleCode;

                    Module module = (Module)GetModule(setting.ModuleCode);
                    if (module != null)
                    {
                        url += module.FormUrl + "?__Page.IsForBSHome=True";
                    }

                    url += "&UCDisplayConditions1.rblReportDisplayType=" + setting.ChartType;

                    object[] detailArray = QueryBSHomeSettingDetail(reportSeq, string.Empty, int.MinValue, int.MaxValue);
                    if (detailArray != null)
                    {
                        foreach (BSHomeSettingDetail detail in detailArray)
                        {

                            if (parameterDic.ContainsKey(detail.ParameterName) && parameterDic[detail.ParameterName] == typeof(DateTime))
                            {
                                int days = 0;
                                int.TryParse(detail.ParameterValue, out days);
                                DateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider).DateTime.AddDays(days);

                                url += "&" + detail.ParameterName + "=" + FormatHelper.TODateInt(dateTime).ToString();
                            }
                            else
                            {
                                url += "&" + detail.ParameterName + "=" + detail.ParameterValue;
                            }
                        }
                    }

                    url += "&Width=500&Height=230";
                }
            }
        }

        #endregion

        #region GetParamtersByGroupAndparentParamter 三级参数时，由参数组和父参数得到的参数值
        public object[] GetParamtersByGroupAndparentParamter(string paramterGroupCode, string parentParamter)
        {
            string sql = string.Format(@"SELECT {0} FROM Tblsysparam
                                        WHERE paramgroupcode = '{1}' AND parentparam='{2}' ORDER  BY  eattribute1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)), paramterGroupCode, parentParamter);

            return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));
        }
        #endregion
        #region  EC
        public object[] GetErrorcode(string ECGCODE)
        {
            string sql = string.Format("select ecode,ecdesc from tblec where ecode in (select ecode from tblecg2ec where ecgcode='{0}')", ECGCODE);
            return this.DataProvider.CustomQuery(typeof(ErrorCodeA), new SQLCondition(sql));
        }
        public object[] GetErrorGroupcode()
        {
            string sql = "select ecgcode,ecgdesc from tblecg";
            return this.DataProvider.CustomQuery(typeof(ErrorCodeGroupA), new SQLCondition(sql));
        }
        public object[] GetErrorGroupcode(string ecgcode)
        {
            string sql = "select ecgcode,ecgdesc from tblecg where ecgcode='" + ecgcode + "'";
            return this.DataProvider.CustomQuery(typeof(ErrorCodeGroupA), new SQLCondition(sql));
        }
        public object[] GetErrorcodeByEcode(string ecode)
        {
            string sql = string.Format("select ecode,ecdesc from tblec where ecode='{0}'", ecode);
            return this.DataProvider.CustomQuery(typeof(ErrorCodeA), new SQLCondition(sql));
        }
        #endregion
    }

	#region ModuleTreeNode
	[Serializable]
	public class ModuleTreeNode : ITreeObjectNode
	{
		private Module _module = null;
		[NonSerialized]private TreeObjectNodeHelper _helper = null;
		private ModuleTreeNode _parent = null;

		public Module Module
		{
			get
			{
				return this._module;
			}
			set
			{
				this._module = value;
			}
		}

		public TreeObjectNodeHelper TreeObjectNodeHelper
		{
			get
			{
				return this._helper;
			}
			set
			{
				this._helper = value;
			}
		}

		public ModuleTreeNode(Module module)
		{
			this._module = module;
			this._parent = null;
			this._helper = new TreeObjectNodeHelper();
		}

		public ModuleTreeNode(Module module, ModuleTreeNode parentNode)
		{
			this._module = module;
			this._parent = parentNode;
			this._helper = parentNode.TreeObjectNodeHelper;
		}

		#region ITreeObjectNode 成员

		public string ID
		{
			get
			{
				return this._module.ModuleCode;
			}
			set
			{
				// 不支持
			}
		}

		public string Text
		{
			get
			{
				return this._module.ModuleCode;
			}
			set
			{
				// 不支持
			}
		}

		public ITreeObjectNode Parent
		{
			get
			{
				return this._parent;
			}
			set
			{				
				this._parent = (ModuleTreeNode)value;
				this._helper = this._parent._helper;
			}
		}

		public ITreeObjectNode Root
		{
			get
			{
				return this._helper.GetRoot(  );
			}
		}

		public void AddSubTreeObjectNode(ITreeObjectNode node)
		{		
			this._helper.AddSubTreeObjectNode(this, node );
		}

		public void AddSubTreeObjectNodeRange(ITreeObjectNode[] nodes)
		{		
			this._helper.AddSubTreeObjectNodeRange(this, nodes );
		}

		public void DeleteSubTreeObjectNode(ITreeObjectNode node)
		{
			this._helper.DeleteSubTreeObjectNode(this, node );
		}

		public void MoveTreeObjectNode(ITreeObjectNode parent)
		{
			this._helper.MoveTreeObjectNode(this,parent);
		}

		public void Update()
		{
			this._helper.Update( this );
		}

		public TreeObjectNodeSet GetSubLevelChildrenNodes()
		{
			return this._helper.GetSubLevelChildrenNodes(this);
		}

		public ITreeObjectNode GetTreeObjectNodeByID(string id)
		{
			return this._helper.GetTreeObjectNodeByID( id );
		}

		public TreeObjectNodeSet GetChainFromRoot()
		{
			return this._helper.GetChainFromRoot( this );
		}

		public bool IsEqual(ITreeObjectNode node)
		{
			return this._helper.IsEqual(this,node);
		}

		public TreeObjectNodeSet GetAllNodes()
		{
			return this._helper.GetAllNodes(this);			
		}

		#endregion
	}
	#endregion

	#region MenuTreeNode
	[Serializable]
	public class MenuTreeNode : ITreeObjectNode
	{
		private MenuWithUrl _menu = null;
		[NonSerialized]private TreeObjectNodeHelper _helper = null;
		private MenuTreeNode _parent = null;

		public MenuWithUrl MenuWithUrl
		{
			get
			{
				return this._menu;
			}
			set
			{
				this._menu = value;
			}
		}

		public TreeObjectNodeHelper TreeObjectNodeHelper
		{
			get
			{
				return this._helper;
			}
			set
			{
				this._helper = value;
			}
		}

		public MenuTreeNode(MenuWithUrl menu)
		{
			this._menu = menu;
			this._parent = null;
			this._helper = new TreeObjectNodeHelper();
		}

		public MenuTreeNode(MenuWithUrl menu, MenuTreeNode parentNode)
		{
			this._menu = menu;
			this._parent = parentNode;
			this._helper = parentNode.TreeObjectNodeHelper;
		}

		#region ITreeObjectNode 成员

		public string ID
		{
			get
			{
				return this._menu.MenuCode;
			}
			set
			{
				// 不支持
			}
		}

		public string Text
		{
			get
			{
				return this._menu.MenuCode;
			}
			set
			{
				// 不支持
			}
		}

		public ITreeObjectNode Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = (MenuTreeNode)value;
			}
		}

		public ITreeObjectNode Root
		{
			get
			{
				return this._helper.GetRoot(  );
			}
		}

		public void AddSubTreeObjectNode(ITreeObjectNode node)
		{		
			this._helper.AddSubTreeObjectNode(this, node );
		}

		public void AddSubTreeObjectNodeRange(ITreeObjectNode[] nodes)
		{		
			this._helper.AddSubTreeObjectNodeRange(this, nodes );
		}

		public void DeleteSubTreeObjectNode(ITreeObjectNode node)
		{
			this._helper.DeleteSubTreeObjectNode(this, node );
		}

		public void MoveTreeObjectNode(ITreeObjectNode parent)
		{
			this._helper.MoveTreeObjectNode(this,parent);
		}

		public void Update()
		{
			this._helper.Update( this );
		}

		public TreeObjectNodeSet GetSubLevelChildrenNodes()
		{
			return this._helper.GetSubLevelChildrenNodes(this);
		}

		public ITreeObjectNode GetTreeObjectNodeByID(string id)
		{
			return this._helper.GetTreeObjectNodeByID( id );
		}

		public TreeObjectNodeSet GetChainFromRoot()
		{
			return this._helper.GetChainFromRoot( this );
		}

		public bool IsEqual(ITreeObjectNode node)
		{
			return this._helper.IsEqual(this,node);
		}

		public TreeObjectNodeSet GetAllNodes()
		{
			return this._helper.GetAllNodes(this);			
		}

		#endregion
	}
	#endregion

	#region ParameterTreeNode
	[Serializable]
	public class ParameterTreeNode : ITreeObjectNode
	{
		private Parameter _Parameter = null;
		[NonSerialized]private TreeObjectNodeHelper _helper = null;
		private ParameterTreeNode _parent = null;

		public Parameter Parameter
		{
			get
			{
				return this._Parameter;
			}
			set
			{
				this._Parameter = value;
			}
		}

		public TreeObjectNodeHelper TreeObjectNodeHelper
		{
			get
			{
				return this._helper;
			}
			set
			{
				this._helper = value;
			}
		}

		public ParameterTreeNode(Parameter Parameter)
		{
			this._Parameter = Parameter;
			this._parent = null;
			this._helper = new TreeObjectNodeHelper();
		}

		public ParameterTreeNode(Parameter Parameter, ParameterTreeNode parentNode)
		{
			this._Parameter = Parameter;
			this._parent = parentNode;
			this._helper = parentNode.TreeObjectNodeHelper;
		}

		#region ITreeObjectNode 成员

		public string ID
		{
			get
			{
				return this._Parameter.ParameterCode;
			}
			set
			{
				// 不支持
			}
		}

		public string Text
		{
			get
			{
				return this._Parameter.ParameterCode;
			}
			set
			{
				// 不支持
			}
		}

		public ITreeObjectNode Parent
		{
			get
			{
				return this._parent;
			}
			set
			{				
				this._parent = (ParameterTreeNode)value;
				this._helper = this._parent._helper;
			}
		}

		public ITreeObjectNode Root
		{
			get
			{
				return this._helper.GetRoot(  );
			}
		}

		public void AddSubTreeObjectNode(ITreeObjectNode node)
		{		
			this._helper.AddSubTreeObjectNode(this, node );
		}

		public void AddSubTreeObjectNodeRange(ITreeObjectNode[] nodes)
		{		
			this._helper.AddSubTreeObjectNodeRange(this, nodes );
		}

		public void DeleteSubTreeObjectNode(ITreeObjectNode node)
		{
			this._helper.DeleteSubTreeObjectNode(this, node );
		}

		public void MoveTreeObjectNode(ITreeObjectNode parent)
		{
			this._helper.MoveTreeObjectNode(this,parent);
		}

		public void Update()
		{
			this._helper.Update( this );
		}

		public TreeObjectNodeSet GetSubLevelChildrenNodes()
		{
			return this._helper.GetSubLevelChildrenNodes(this);
		}

		public ITreeObjectNode GetTreeObjectNodeByID(string id)
		{
			return this._helper.GetTreeObjectNodeByID( id );
		}

		public TreeObjectNodeSet GetChainFromRoot()
		{
			return this._helper.GetChainFromRoot( this );
		}

		public bool IsEqual(ITreeObjectNode node)
		{
			return this._helper.IsEqual(this,node);
		}

		public TreeObjectNodeSet GetAllNodes()
		{
			return this._helper.GetAllNodes(this);			
		}

		#endregion
	}
	#endregion


}
