using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.MutiLanguage;
using System.Data;

namespace BenQGuru.eMES.Common.Domain
{
	public interface IDomainTransaction
	{
		/// <summary>
		/// 开始事务
		/// </summary>
		void BeginTransaction();
		/// <summary>
		/// 回滚事务
		/// </summary>
		void RollbackTransaction();
		/// <summary>
		/// 提交事务
		/// </summary>
		void CommitTransaction();

		/// <summary>
		/// 插入映射实体
		/// </summary>
		/// <param name="domainObject">实体</param>
		void Insert(object domainObject);
		/// <summary>
		/// 自定义插入实体
		/// </summary>
		/// <param name="domainObject">实体</param>
		/// <param name="attributes">值</param>
		void CustomInsert(object domainObject, string[] attributes);
		/// <summary>
		/// 自定义插入实体
		/// </summary>
		/// <param name="type">实体</param>
		/// <param name="attributes">属性列表</param>
		/// <param name="attributeValus">值列表</param>
		void CustomInsert(Type type, string[] attributes, object[] attributeValus);

		//
		/// <summary>
		/// 删除实体
		/// </summary>
		/// <param name="domainObject">实体</param>
		void Delete(object domainObject);
		void CustomDelete(object domainObject, Condition[] conditions);
		void CustomDelete(object domainObject, string[] attributes);

		void CustomDelete(Type type, object[] keyAttributeValus);
		void CustomDelete(Type type, string[] attributes, object[] attributeValus);

		void Update(object domainObject);
		void CustomUpdate(object domainObject, string[] attributes);
		void CustomUpdate(object domainObject, string[] attributes, object[] attributeValus);
	}

	public interface IDomainSearch
	{
		/// <summary>
		/// 查询数据
		/// </summary>
		/// <param name="type">返回实体类型</param>
		/// <param name="keyAttributeValus">主键字段值列表</param>
		/// <returns>实体实例</returns>
		object CustomSearch(Type type, object[] keyAttributeValus);
		/// <summary>
		/// 自定义查询
		/// </summary>
		/// <param name="type">返回实体类型</param>
		/// <param name="condition">查询条件</param>
		/// <returns>返回实体列表</returns>
		object[]  CustomSearch(Type type, Condition condition);
		/// <summary>
		/// 自定义查询
		/// </summary>
		/// <param name="type">返回实体类型</param>
		/// <param name="attributes">属性名称列表</param>
		/// <param name="attributeValus">属性值列表</param>
		/// <returns>返回实体列表</returns>
		object[]  CustomSearch(Type type,  string[] attributes, object[] attributeValus);

		/// <summary>
		/// 查询记录数量
		/// </summary>
		/// <param name="type">实体类型</param>
		/// <param name="condition">查询条件</param>
		/// <returns>记录数量</returns>
		int GetDomainObjectCount(Type type, Condition condition);
		/// <summary>
		/// 查询记录数量
		/// </summary>
		/// <param name="type">实体类型</param>
		/// <param name="attributes">属性名称列表</param>
		/// <param name="attributeValus">属性值列表</param>
		/// <returns>记录数量</returns>
		int GetDomainObjectCount(Type type, string[] attributes, object[] attributeValus);
	}

	public interface IDomainQuery
	{
		/// <summary>
		/// 查询数据
		/// </summary>
		/// <param name="type">返回数据的实体类型</param>
		/// <param name="condition">查询条件</param>
		/// <returns>返回对象列表</returns>
		/// <example>
		/// BenQGuru.eMES.Domain.MOModel.MO[] moList = 
		///		this.DataProvider.CustomQuery(
		///			typeof(BenQGuru.eMES.Domain.MOModel.MO),
		///			new SQLCondition("select * from tblmo")
		///		);
		///</example>
		object[]  CustomQuery(Type type, Condition condition);
		/// <summary>
		/// 查询记录行数
		/// </summary>
		/// <param name="conditions">查询条件，其中需要包含count(*)的格式返回记录数</param>
		/// <returns>记录数</returns>
		/// <example>
		/// int iCount = 
		///		this.DataProvider.GetCount(new SQLCondition("select count(*) from tblmo"));
		/// </example>
		int GetCount(Condition conditions);
		/// <summary>
		/// 执行SQL语句
		/// </summary>
		/// <param name="conditions">SQL条件</param>
		/// <example>
		/// this.DataProvider.CustomExecute(new SQLCondition("update tblmo set itemcode='ITEM1' where mocode='MO_TEST_1'"));
		/// </example>
		void CustomExecute(Condition conditions);
        /// <summary>
        /// 执行Procedure
        /// </summary>
        /// <param name="conditions">Procedure条件</param>
        /// <example>
        /// 
        /// </example>
        void CustomProcedure(ref ProcedureCondition condition);

        /// <summary>
        /// 获取单列查询的结果,单列的查询SQL不需要额外的增加实体类
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        string[] GetStringResult(Condition condition);
        DataSet QueryData(Condition condition);
	}


	/// <summary>
	/// IDomainObjectDataProvider 的摘要说明。
	/// </summary>
	public interface IDomainDataProvider:IDomainTransaction, IDomainSearch, IDomainQuery, ILanguage
	{	
	}
}
