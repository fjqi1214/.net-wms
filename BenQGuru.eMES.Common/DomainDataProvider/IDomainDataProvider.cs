using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.MutiLanguage;

namespace BenQGuru.eMES.Common.Domain
{
	public interface IDomainTransaction
	{
		void BeginTransaction();
		void RollbackTransaction();
		void CommitTransaction();

		void Insert(object domainObject);
		void CustomInsert(object domainObject, string[] attributes);
		void CustomInsert(Type type, string[] attributes, object[] attributeValus);

		//
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
		object CustomSearch(Type type, object[] keyAttributeValus);
		object[]  CustomSearch(Type type, Condition[] conditions);
		object[]  CustomSearch(Type type,  string[] attributes, object[] attributeValus);

		int GetDomainObjectCount(Type type, Condition[] conditions);
		int GetDomainObjectCount(Type type, string[] attributes, object[] attributeValus);
	}


	/// <summary>
	/// IDomainObjectDataProvider 的摘要说明。
	/// </summary>
	public interface IDomainDataProvider:IDomainTransaction, IDomainSearch, ILanguage
	{	
	}
}
