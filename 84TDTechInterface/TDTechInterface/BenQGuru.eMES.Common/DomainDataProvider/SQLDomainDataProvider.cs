using System;
using System.Collections; 
using System.Collections.Specialized;
using System.Reflection;
using System.Data; 
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.PersistBroker;

namespace BenQGuru.eMES.Common.DomainDataProvider
{
	/// <summary>
	///  MES数据访问提供类
	/// </summary>
	public class SQLDomainDataProvider:MarshalByRefObject,IDomainDataProvider
	{

		private IPersistBroker _persistBroker;
		private System.Globalization.CultureInfo _cultureInfo;

		public SQLDomainDataProvider(IPersistBroker persistBroker, System.Globalization.CultureInfo  cultureInfo)
		{
			this._cultureInfo   = cultureInfo;
			this._persistBroker = persistBroker;
		}

		public SQLDomainDataProvider()
		{
		}

		//Laws Lu,max life time to unlimited
		public override object InitializeLifetimeService()
		{
			return null;
		}


		/// <summary>
		/// 数据库连接
		/// </summary>
		public IPersistBroker PersistBroker
		{
			get
			{
				return _persistBroker;
			}
		}

		public System.Globalization.CultureInfo CultureInfo
		{
			get
			{
				return _cultureInfo;
			}
		}

		/// <summary>
		/// 开始事务
		/// </summary>
		public virtual void BeginTransaction()
		{
			this.PersistBroker.BeginTransaction();  
		}

		/// <summary>
		/// 回滚事务
		/// </summary>
		public virtual void RollbackTransaction()
		{
			this.PersistBroker.RollbackTransaction(); 
		}

		/// <summary>
		/// 提交事务
		/// </summary>
		public virtual void CommitTransaction()		
		{
			this.PersistBroker.CommitTransaction(); 
		}

//		public virtual void Insert(object domainObject)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
//			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(domainObject); 
//			int  num1 = 0;
//			string fieldText = "";
//			string valueText = "";
//			
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{				
//				Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//				fieldText = string.Format("{0}{1}{2}",fieldText , ((num1>0) ? "," : "") , ((FieldMapAttribute)myEnumerator.Key).FieldName);
//				valueText = string.Format("{0}{1}{2}"
//					, valueText 
//					, ((num1>0) ? "," : "") 
//					,DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)));
//	
//				num1 = num1 +1;				
//			}		
//
//			string sqlText = string.Format("INSERT INTO {0}({1}) VALUES ({2})", tableAttribute.TableName, fieldText, valueText);
//			
//			this.PersistBroker.Execute(sqlText);
//		}

		private char parameterPrefix = '$';
		/// <summary>
		/// 插入实体
		/// </summary>
		/// <param name="domainObject">实体对象</param>
		public virtual void Insert(object domainObject)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(domainObject); 

			int  num1 = 0;
			string fieldText = "";
			string valueText = "";

			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();
			
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{				
				Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

				if ((((FieldMapAttribute)myEnumerator.Key).BlobType != BlobTypes.None) && ((DomainObject)domainObject).IsBlobIgnored)
				{
					continue;
				}
				
				fieldText = string.Format("{0}{1}{2}",fieldText , ((num1>0) ? "," : "") , ((FieldMapAttribute)myEnumerator.Key).FieldName);
				valueText = string.Format("{0}{1}{2}"
						, valueText 
						, ((num1>0) ? "," : "") 
						, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName).Replace("\"",""));
						//,DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)));
			
				parameters.Add(((FieldMapAttribute)myEnumerator.Key).FieldName);
				parameterTypes.Add(((FieldMapAttribute)myEnumerator.Key).DataType);
				parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null) ));	
	
				num1 = num1 +1;
			}

			string sqlText = string.Format("INSERT INTO {0}({1}) VALUES ({2})", tableAttribute.TableName, fieldText, valueText);

			if (num1>0)
			{
				this.PersistBroker.Execute(sqlText
									,(string[])parameters.ToArray(typeof(string))
									,(Type[] )parameterTypes.ToArray(typeof(Type))
									,(object[])parameterValues.ToArray(typeof(object)));
			}
		}

//		public virtual void CustomInsert(object domainObject, string[] attributes)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
//			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(domainObject); 
//
//			int  num1 = 0;
//			string fieldText = "";
//			string valueText = "";
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(attributes); 
//			
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if (sc.IndexOf(((MemberInfo)myEnumerator.Value).Name)!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//					fieldText = string.Format("{0}{1}{2}",fieldText , ((num1>0) ? "," : "") , ((FieldMapAttribute)myEnumerator.Key).FieldName);
//					valueText = string.Format("{0}{1}{2}"
//						, valueText 
//						, ((num1>0) ? "," : "") 
//						,DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)));
//					num1 = num1 +1;
//				}
//			}
//
//			string sqlText = string.Format("INSERT INTO {0}({1}) VALUES ({2})", tableAttribute.TableName, fieldText, valueText);
//			this.PersistBroker.Execute(sqlText);  
//		}
		/// <summary>
		/// 自定义插入实体
		/// </summary>
		/// <param name="domainObject">实体对象</param>
		/// <param name="attributes">属性</param>
		public virtual void CustomInsert(object domainObject, string[] attributes)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(domainObject); 

			int  num1 = 0;
			string fieldText = "";
			string valueText = "";

			StringCollection sc = new StringCollection();
			sc.AddRange(attributes); 
			
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();
			
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{				
				Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

				if ((((FieldMapAttribute)myEnumerator.Key).BlobType != BlobTypes.None) && ((DomainObject)domainObject).IsBlobIgnored)
				{
					continue;
				}
				
				fieldText = string.Format("{0}{1}{2}",fieldText , ((num1>0) ? "," : "") , ((FieldMapAttribute)myEnumerator.Key).FieldName);
				valueText = string.Format("{0}{1}{2}"
					, valueText 
					, ((num1>0) ? "," : "") 
					, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName));
				//,DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)));
			
				parameters.Add(((FieldMapAttribute)myEnumerator.Key).FieldName);
				parameterTypes.Add(((FieldMapAttribute)myEnumerator.Key).DataType);
				parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null) ));	
	
				num1 = num1 +1;
			}

			string sqlText = string.Format("INSERT INTO {0}({1}) VALUES ({2})", tableAttribute.TableName, fieldText, valueText);

			if (num1>0)
			{
				this.PersistBroker.Execute(sqlText
					,(string[])parameters.ToArray(typeof(string))
					,(Type[] )parameterTypes.ToArray(typeof(Type))
					,(object[])parameterValues.ToArray(typeof(object)));
			}
		}

//		public virtual void CustomInsert(Type type, string[] attributes, object[] attributeValus)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
//			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 
//
//			int  num1 = 0;
//			int  num2 = 0;
//			string fieldText = "";
//			string valueText = "";
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(attributes); 
//			
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//					fieldText = string.Format("{0}{1}{2}",fieldText , ((num1>0) ? "," : "") , ((FieldMapAttribute)myEnumerator.Key).FieldName);
//					valueText = string.Format("{0}{1}{2}"
//						, valueText 
//						, ((num1>0) ? "," : "") 
//						,DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, attributeValus[num2]));
//					num1 = num1 +1;
//				}
//			}
//
//			string sqlText = string.Format("INSERT INTO {0}({1}) VALUES ({2})", tableAttribute.TableName, fieldText, valueText);
//			this.PersistBroker.Execute(sqlText);  
//		}
		/// <summary>
		/// 自定义插入实体
		/// </summary>
		/// <param name="type">实体对象</param>
		/// <param name="attributes">属性</param>
		/// <param name="attributeValus">值</param>
		public virtual void CustomInsert(Type type, string[] attributes, object[] attributeValus)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 

			int  num1 = 0;
			int  num2 = 0;
			string fieldText = "";
			string valueText = "";
			
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();

			StringCollection sc = new StringCollection();
			sc.AddRange(attributes); 
			
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
				{
					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

//					if ((((FieldMapAttribute)myEnumerator.Key).BlobType != BlobTypes.None) && ((DomainObject)domainObject).IsBlobIgnored)
//					{
//						continue;
//					}

					fieldText = string.Format("{0}{1}{2}",fieldText , ((num1>0) ? "," : "") , ((FieldMapAttribute)myEnumerator.Key).FieldName);
					valueText = string.Format("{0}{1}{2}"
						, valueText 
						, ((num1>0) ? "," : "") 
						, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName));
					//,DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)));
			
					parameters.Add(((FieldMapAttribute)myEnumerator.Key).FieldName);
					parameterTypes.Add(((FieldMapAttribute)myEnumerator.Key).DataType);
					parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, attributeValus[num2] ));	

					num1 = num1 +1;
				}
			}

			string sqlText = string.Format("INSERT INTO {0}({1}) VALUES ({2})", tableAttribute.TableName, fieldText, valueText);

			if (num1>0)
			{
				this.PersistBroker.Execute(sqlText
					,(string[])parameters.ToArray(typeof(string))
					,(Type[] )parameterTypes.ToArray(typeof(Type))
					,(object[])parameterValues.ToArray(typeof(object)));
			}
		}


//		public virtual void Delete(object domainObject)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
//			Hashtable hs = 	DomainObjectUtility.GetKeyAttributeMemberInfos(domainObject); 
//			int  num1 = 0;
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//				filterText = string.Format("{0}{1}{2}={3}" 
//											, filterText
//											,  ((num1>0) ? " AND " : "")
//											,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//											, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)));
//				num1 = num1 +1;
//			}
//
//			string sqlText = string.Format("DELETE FROM {0} WHERE {1}", tableAttribute.TableName, filterText);
//			this.PersistBroker.Execute(sqlText);  
//		}
		/// <summary>
		/// 删除实体
		/// </summary>
		/// <param name="domainObject">实体对象</param>
		public virtual void Delete(object domainObject)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
			Hashtable hs = 	DomainObjectUtility.GetKeyAttributeMemberInfos(domainObject); 
			int  num1 = 0;
			string filterText = "";
					
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();

			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

				filterText = string.Format("{0}{1}{2}={3}" 
					, filterText
					,  ((num1>0) ? " AND " : "")
					,  ((FieldMapAttribute)myEnumerator.Key).FieldName
					, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName));
				
				parameters.Add(((FieldMapAttribute)myEnumerator.Key).FieldName);
				parameterTypes.Add(((FieldMapAttribute)myEnumerator.Key).DataType);
				parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null) ));	
			
				num1 = num1 +1;
			}

			string sqlText = string.Format("DELETE FROM {0} WHERE {1}", tableAttribute.TableName, filterText);
	
			if (num1>0)
			{
				this.PersistBroker.Execute(sqlText
					,(string[])parameters.ToArray(typeof(string))
					,(Type[] )parameterTypes.ToArray(typeof(Type))
					,(object[])parameterValues.ToArray(typeof(object)));
			}
		}
		/// <summary>
		/// 自定义删除
		/// </summary>
		/// <param name="domainObject">实体对象</param>
		/// <param name="conditions">条件</param>
		public virtual void CustomDelete(object domainObject, Condition[] conditions)		
		{
			//throw new Exception("CustomDelete: Not implamention!");
			ExceptionManager.Raise(this.GetType(),"$Error_Not_Implamention");
		}

//		public virtual void CustomDelete(object domainObject, string[] attributes)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
//			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(domainObject); 
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(attributes); 
//
//			int  num1 = 0;
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if (sc.IndexOf(((MemberInfo)myEnumerator.Value).Name)!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//					filterText = string.Format("{0}{1}{2}={3}" 
//						, filterText
//						,  ((num1>0) ? " AND " : "")
//						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//						, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)));
//					num1 = num1 +1;
//				}
//			}
//
//			string sqlText = string.Format("DELETE FROM {0} WHERE {1}", tableAttribute.TableName, filterText);
//			this.PersistBroker.Execute(sqlText);  
//		}
		/// <summary>
		/// 自定义删除实体
		/// </summary>
		/// <param name="domainObject">实体对象</param>
		/// <param name="attributes">属性</param>
		public virtual void CustomDelete(object domainObject, string[] attributes)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(domainObject); 

			StringCollection sc = new StringCollection();
			sc.AddRange(attributes); 

			int  num1 = 0;
			string filterText = "";
							
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();
			
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				if (sc.IndexOf(((MemberInfo)myEnumerator.Value).Name)!=-1)
				{
					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;


					filterText = string.Format("{0}{1}{2}={3}" 
						, filterText
						,  ((num1>0) ? " AND " : "")
						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
						, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName));
				
					parameters.Add(((FieldMapAttribute)myEnumerator.Key).FieldName);
					parameterTypes.Add(((FieldMapAttribute)myEnumerator.Key).DataType);
					parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null) ));	

					num1 = num1 +1;
				}
			}

			string sqlText = string.Format("DELETE FROM {0} WHERE {1}", tableAttribute.TableName, filterText);
	
			if (num1>0)
			{
				this.PersistBroker.Execute(sqlText
					,(string[])parameters.ToArray(typeof(string))
					,(Type[] )parameterTypes.ToArray(typeof(Type))
					,(object[])parameterValues.ToArray(typeof(object)));
			}
		}

//		public virtual void CustomDelete(Type type, string[] attributes, object[] attributeValus)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
//			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(attributes); 
//
//			int  num1 = 0;
//			int  num2 = 0;
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//					filterText = string.Format("{0}{1}{2}={3}" 
//						, filterText
//						,  ((num1>0) ? " AND " : "")
//						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//						, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, attributeValus[num2]));
//					num1 = num1 +1;
//				}
//			}
//
//			string sqlText = string.Format("DELETE FROM {0} WHERE {1}", tableAttribute.TableName, filterText);
//			this.PersistBroker.Execute(sqlText);  
//		}
		/// <summary>
		/// 自定义删除实体
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="attributes">属性</param>
		/// <param name="attributeValus">值</param>
		public virtual void CustomDelete(Type type, string[] attributes, object[] attributeValus)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 

			StringCollection sc = new StringCollection();
			sc.AddRange(attributes); 

			int  num1 = 0;
			int  num2 = 0;
			string filterText = "";		
										
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();

			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
				{
					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

					filterText = string.Format("{0}{1}{2}={3}" 
						, filterText
						,  ((num1>0) ? " AND " : "")
						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
						, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName));
				
					parameters.Add(((FieldMapAttribute)myEnumerator.Key).FieldName);
					parameterTypes.Add(((FieldMapAttribute)myEnumerator.Key).DataType);
					parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, attributeValus[num2] ));	

					num1 = num1 +1;
				}
			}

			string sqlText = string.Format("DELETE FROM {0} WHERE {1}", tableAttribute.TableName, filterText);
	
			if (num1>0)
			{
				this.PersistBroker.Execute(sqlText
					,(string[])parameters.ToArray(typeof(string))
					,(Type[] )parameterTypes.ToArray(typeof(Type))
					,(object[])parameterValues.ToArray(typeof(object)));
			}
		}

//		public virtual void CustomDelete(Type type, object[] keyAttributeValus)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
//			Hashtable hs = 	DomainObjectUtility.GetKeyAttributeMemberInfos(type); 
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(tableAttribute.GetKeyFields()); 
//
//			int  num1 = 0;
//			int  num2 = 0;
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if ((num2 = sc.IndexOf(((FieldMapAttribute)myEnumerator.Key).FieldName))!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//					filterText = string.Format("{0}{1}{2}={3}" 
//						, filterText
//						,  ((num1>0) ? " AND " : "")
//						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//						, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  keyAttributeValus[num2]));
//					num1 = num1 +1;
//				}
//			}
//
//			string sqlText = string.Format("DELETE FROM {0} WHERE {1}", tableAttribute.TableName, filterText);
//			this.PersistBroker.Execute(sqlText);  
//		}
		/// <summary>
		/// 自定义删除实体
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="keyAttributeValus">值</param>
		public virtual void CustomDelete(Type type, object[] keyAttributeValus)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
			Hashtable hs = 	DomainObjectUtility.GetKeyAttributeMemberInfos(type); 

			StringCollection sc = new StringCollection();
			sc.AddRange(tableAttribute.GetKeyFields()); 

			int  num1 = 0;
			int  num2 = 0;
			string filterText = "";
												
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();

			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				if ((num2 = sc.IndexOf(((FieldMapAttribute)myEnumerator.Key).FieldName))!=-1)
				{
					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

					filterText = string.Format("{0}{1}{2}={3}" 
						, filterText
						,  ((num1>0) ? " AND " : "")
						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
						, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName));
				
					parameters.Add(((FieldMapAttribute)myEnumerator.Key).FieldName);
					parameterTypes.Add(((FieldMapAttribute)myEnumerator.Key).DataType);
					parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, keyAttributeValus[num2] ));	

					num1 = num1 +1;
				}
			}

			string sqlText = string.Format("DELETE FROM {0} WHERE {1}", tableAttribute.TableName, filterText);
	
			if (num1>0)
			{
				this.PersistBroker.Execute(sqlText
					,(string[])parameters.ToArray(typeof(string))
					,(Type[] )parameterTypes.ToArray(typeof(Type))
					,(object[])parameterValues.ToArray(typeof(object)));
			}
		}

//		public virtual void Update(object domainObject)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
//			Hashtable hs = 	DomainObjectUtility.GetNonKeyAttributeMemberInfos(domainObject); 
//			int  num1 = 0;
//			string fieldText = "";
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//				fieldText = string.Format("{0}{1}{2}={3}" 
//					, fieldText
//					,  ((num1>0) ? "," : "")
//					,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//					, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)));
//				num1 = num1 +1;
//			}
//
//			num1 = 0;
//			Hashtable hs2 = 	DomainObjectUtility.GetKeyAttributeMemberInfos(domainObject); 
//
//			IDictionaryEnumerator myEnumerator2 = hs2.GetEnumerator();
//			while ( myEnumerator2.MoveNext())
//			{
//				Type type1 = ((MemberInfo) myEnumerator2.Value is FieldInfo) ? ((FieldInfo) myEnumerator2.Value).FieldType : ((PropertyInfo) myEnumerator2.Value).PropertyType;
//
//				filterText = string.Format("{0}{1}{2}={3}" 
//					, filterText
//					,  ((num1>0) ? " AND " : "")
//					,  ((FieldMapAttribute)myEnumerator2.Key).FieldName
//					, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator2.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator2.Value), null)));
//				num1 = num1 +1;
//			}
//
//			string sqlText = string.Format("UPDATE {0} SET {1} WHERE {2}", tableAttribute.TableName, fieldText, filterText);
//			this.PersistBroker.Execute(sqlText);  
//		}
		/// <summary>
		/// 更新实体
		/// </summary>
		/// <param name="domainObject">实体对象</param>
		public virtual void Update(object domainObject)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
			Hashtable hs = 	DomainObjectUtility.GetNonKeyAttributeMemberInfos(domainObject); 
			int  num1 = 0;
			string fieldText = "";
			string filterText = "";
		
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();

			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
			

				if ((((FieldMapAttribute)myEnumerator.Key).BlobType != BlobTypes.None) && ((DomainObject)domainObject).IsBlobIgnored)
				{
					continue;
				}

				fieldText = string.Format("{0}{1}{2}={3}" 
					, fieldText
					,  ((num1>0) ? "," : "")
					,  ((FieldMapAttribute)myEnumerator.Key).FieldName
					, string.Format("{0}{1}1", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName));

				parameters.Add( ((FieldMapAttribute)myEnumerator.Key).FieldName + "1");
				parameterTypes.Add( ((FieldMapAttribute)myEnumerator.Key).DataType );
				parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)) );
					
				num1 = num1 +1;
			}

			int num3 = 0;
			Hashtable hs2 = 	DomainObjectUtility.GetKeyAttributeMemberInfos(domainObject); 

			IDictionaryEnumerator myEnumerator2 = hs2.GetEnumerator();
			while ( myEnumerator2.MoveNext())
			{
				Type type1 = ((MemberInfo) myEnumerator2.Value is FieldInfo) ? ((FieldInfo) myEnumerator2.Value).FieldType : ((PropertyInfo) myEnumerator2.Value).PropertyType;

				filterText = string.Format("{0}{1}{2}={3}" 
					, filterText
					,  ((num3>0) ? " AND " : "")
					,  ((FieldMapAttribute)myEnumerator2.Key).FieldName
					, string.Format("{0}{1}2", parameterPrefix, ((FieldMapAttribute)myEnumerator2.Key).FieldName));
				//	, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator2.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator2.Value), null)));
				
				parameters.Add( ((FieldMapAttribute)myEnumerator2.Key).FieldName + "2" );
				parameterTypes.Add( ((FieldMapAttribute)myEnumerator2.Key).DataType );
				parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator2.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator2.Value), null)) );
		
				num3 = num3 +1;
			}

			string sqlText = string.Format("UPDATE {0} SET {1} WHERE {2}", tableAttribute.TableName, fieldText, filterText);

			if ( num1 > 0)
			{
				this.PersistBroker.Execute(sqlText, 
					(string[])parameters.ToArray(typeof(string)), 
					( Type[] )parameterTypes.ToArray(typeof(Type)), 
					(object[])parameterValues.ToArray(typeof(object)));
			}
		}


//		public virtual void CustomUpdate(object domainObject, string[] attributes)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
//			Hashtable hs = 	DomainObjectUtility.GetNonKeyAttributeMemberInfos(domainObject); 
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(attributes); 
//
//			int  num1 = 0;
//			string fieldText = "";
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if (sc.IndexOf(((MemberInfo)myEnumerator.Value).Name)!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//					fieldText = string.Format("{0}{1}{2}={3}" 
//						, fieldText
//						,  ((num1>0) ? "," : "")
//						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//						, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)));
//					num1 = num1 +1;
//				}
//			}
//
//			num1 = 0;
//			Hashtable hs2 = 	DomainObjectUtility.GetKeyAttributeMemberInfos(domainObject); 
//
//			IDictionaryEnumerator myEnumerator2 = hs2.GetEnumerator();
//			while ( myEnumerator2.MoveNext())
//			{
//				Type type1 = ((MemberInfo) myEnumerator2.Value is FieldInfo) ? ((FieldInfo) myEnumerator2.Value).FieldType : ((PropertyInfo) myEnumerator2.Value).PropertyType;
//
//				filterText = string.Format("{0}{1}{2}={3}" 
//					, filterText
//					,  ((num1>0) ? " AND " : "")
//					,  ((FieldMapAttribute)myEnumerator2.Key).FieldName
//					, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator2.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator2.Value), null)));
//				num1 = num1 +1;
//			}
//
//			string sqlText = string.Format("UPDATE {0} SET {1} WHERE {2}", tableAttribute.TableName, fieldText, filterText);
//			this.PersistBroker.Execute(sqlText);  
//		}
		/// <summary>
		/// 自定义更新实体
		/// </summary>
		/// <param name="domainObject">实体对象</param>
		/// <param name="attributes">属性</param>
		public virtual void CustomUpdate(object domainObject, string[] attributes)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
			Hashtable hs = 	DomainObjectUtility.GetNonKeyAttributeMemberInfos(domainObject); 

			StringCollection sc = new StringCollection();
			sc.AddRange(attributes); 

			int  num1 = 0;
			string fieldText = "";
			string filterText = "";
		
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();
		
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				if (sc.IndexOf(((MemberInfo)myEnumerator.Value).Name)!=-1)
				{
					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

					fieldText = string.Format("{0}{1}{2}={3}" 
						, fieldText
						,  ((num1>0) ? "," : "")
						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
						, string.Format("{0}{1}1", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName));

					parameters.Add( ((FieldMapAttribute)myEnumerator.Key).FieldName + "1");
					parameterTypes.Add( ((FieldMapAttribute)myEnumerator.Key).DataType );
					parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator.Value), null)) );
		
					num1 = num1 +1;
				}
			}

			int num3 = 0;
			Hashtable hs2 = 	DomainObjectUtility.GetKeyAttributeMemberInfos(domainObject); 

			IDictionaryEnumerator myEnumerator2 = hs2.GetEnumerator();
			while ( myEnumerator2.MoveNext())
			{
				Type type1 = ((MemberInfo) myEnumerator2.Value is FieldInfo) ? ((FieldInfo) myEnumerator2.Value).FieldType : ((PropertyInfo) myEnumerator2.Value).PropertyType;

				filterText = string.Format("{0}{1}{2}={3}" 
					, filterText
					,  ((num3>0) ? " AND " : "")
					,  ((FieldMapAttribute)myEnumerator2.Key).FieldName
					, string.Format("{0}{1}2", parameterPrefix, ((FieldMapAttribute)myEnumerator2.Key).FieldName));
				//	, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator2.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator2.Value), null)));
				
				parameters.Add( ((FieldMapAttribute)myEnumerator2.Key).FieldName + "2" );
				parameterTypes.Add( ((FieldMapAttribute)myEnumerator2.Key).DataType );
				parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator2.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator2.Value), null)) );
			
				num3 = num3 +1;
			}

			string sqlText = string.Format("UPDATE {0} SET {1} WHERE {2}", tableAttribute.TableName, fieldText, filterText);

			if ( num1 > 0)
			{
				this.PersistBroker.Execute(sqlText, 
					(string[])parameters.ToArray(typeof(string)), 
					( Type[] )parameterTypes.ToArray(typeof(Type)), 
					(object[])parameterValues.ToArray(typeof(object)));
			}
		}

//		public virtual void CustomUpdate(object domainObject, string[] attributes, object[] attributeValus)		
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
//			Hashtable hs = 	DomainObjectUtility.GetNonKeyAttributeMemberInfos(domainObject); 
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(attributes); 
//
//			int  num1 = 0;
//			int  num2 = 0;
//			string fieldText = "";
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//					fieldText = string.Format("{0}{1}{2}={3}" 
//						, fieldText
//						,  ((num1>0) ? "," : "")
//						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//						, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, attributeValus[num2]));
//					num1 = num1 +1;
//				}
//			}
//
//			num1 = 0;
//			Hashtable hs2 = 	DomainObjectUtility.GetKeyAttributeMemberInfos(domainObject); 
//
//			IDictionaryEnumerator myEnumerator2 = hs2.GetEnumerator();
//			while ( myEnumerator2.MoveNext())
//			{
//				Type type1 = ((MemberInfo) myEnumerator2.Value is FieldInfo) ? ((FieldInfo) myEnumerator2.Value).FieldType : ((PropertyInfo) myEnumerator2.Value).PropertyType;
//
//				filterText = string.Format("{0}{1}{2}={3}" 
//					, filterText
//					,  ((num1>0) ? " AND " : "")
//					,  ((FieldMapAttribute)myEnumerator2.Key).FieldName
//					, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator2.Key).DataType, type1, DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator2.Value), null)));
//				num1 = num1 +1;
//			}
//
//			string sqlText = string.Format("UPDATE {0} SET {1} WHERE {2}", tableAttribute.TableName, fieldText, filterText);
//			this.PersistBroker.Execute(sqlText);  
//		}
		/// <summary>
		/// 自定义更新实体
		/// </summary>
		/// <param name="domainObject">实体对象</param>
		/// <param name="attributes">属性</param>
		/// <param name="attributeValus">值</param>
		public virtual void CustomUpdate(object domainObject, string[] attributes, object[] attributeValus)		
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(domainObject);
			Hashtable hs = 	DomainObjectUtility.GetNonKeyAttributeMemberInfos(domainObject); 

			StringCollection sc = new StringCollection();
			sc.AddRange(attributes); 

			int  num1 = 0;
			int  num2 = 0;
			string fieldText = "";
			string filterText = "";
				
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();
		
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
				{
					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
	
					fieldText = string.Format("{0}{1}{2}={3}" 
						, fieldText
						,  ((num1>0) ? "," : "")
						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
						, string.Format("{0}{1}1", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName));

					parameters.Add( ((FieldMapAttribute)myEnumerator.Key).FieldName + "1");
					parameterTypes.Add( ((FieldMapAttribute)myEnumerator.Key).DataType );
					parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  attributeValus[num2]) );

					num1 = num1 +1;
				}
			}

			int num3 = 0;
			Hashtable hs2 =	DomainObjectUtility.GetKeyAttributeMemberInfos(domainObject); 

			IDictionaryEnumerator myEnumerator2 = hs2.GetEnumerator();
			while ( myEnumerator2.MoveNext())
			{
				Type type1 = ((MemberInfo) myEnumerator2.Value is FieldInfo) ? ((FieldInfo) myEnumerator2.Value).FieldType : ((PropertyInfo) myEnumerator2.Value).PropertyType;

				filterText = string.Format("{0}{1}{2}={3}" 
					, filterText
					,  ((num3>0) ? " AND " : "")
					,  ((FieldMapAttribute)myEnumerator2.Key).FieldName
					, string.Format("{0}{1}2", parameterPrefix, ((FieldMapAttribute)myEnumerator2.Key).FieldName));
				//	, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator2.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator2.Value), null)));
				
				parameters.Add( ((FieldMapAttribute)myEnumerator2.Key).FieldName + "2" );
				parameterTypes.Add( ((FieldMapAttribute)myEnumerator2.Key).DataType );
				parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator2.Key).DataType,type1,  DomainObjectUtility.GetValue(domainObject, ((MemberInfo)myEnumerator2.Value), null)) );
				
				num3 = num3 +1;
			}

			string sqlText = string.Format("UPDATE {0} SET {1} WHERE {2}", tableAttribute.TableName, fieldText, filterText);
	
			if ( num1 > 0)
			{
				this.PersistBroker.Execute(sqlText, 
					(string[])parameters.ToArray(typeof(string)), 
					( Type[] )parameterTypes.ToArray(typeof(Type)), 
					(object[])parameterValues.ToArray(typeof(object)));
			}
		}

//		public virtual object CustomSearch(Type type, object[] keyAttributeValus)
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
//			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(tableAttribute.GetKeyFields()); 
//
//			int  num1 = 0;
//			int  num2 = 0;
//			int  num3 = 0;
//			string fieldText = "";
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if ((num2 = sc.IndexOf(((FieldMapAttribute)myEnumerator.Key).FieldName))!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//					filterText = string.Format("{0}{1}{2}={3}" 
//						, filterText
//						,  ((num3>0) ? " AND " : "")
//						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//						, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  keyAttributeValus[num2]));
//					num3 = num3 +1;
//				}
//				
//				fieldText = string.Format("{0}{1}{2}" 
//					, fieldText
//					,((num1>0) ? "," : "")
//					,((FieldMapAttribute)myEnumerator.Key).FieldName);
//
//				num1 = num1 +1;
//			}
//
//			string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", fieldText, tableAttribute.TableName,  filterText);
//			DataSet ds = this.PersistBroker.Query(sqlText);
//			if (ds!=null)
//			{
//				if (ds.Tables[0].Rows.Count >0)
//				{
//					return DomainObjectUtility.FillDomainObject(DomainObjectUtility.CreateTypeInstance(type), ds.Tables[0].Rows[0]);
//				}
//				else
//				{
//					return null;
//				}
//			}
//			
//			return null;
//		}
		/// <summary>
		/// 自定义查找
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="keyAttributeValus">主键值</param>
		/// <returns>实体对象</returns>
		public virtual object CustomSearch(Type type, object[] keyAttributeValus)
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 

			StringCollection sc = new StringCollection();
			sc.AddRange(tableAttribute.GetKeyFields()); 

			int  num1 = 0;
			int  num2 = 0;
			int  num3 = 0;
			string fieldText = "";
			string filterText = "";
						
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();

			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				if ((num2 = sc.IndexOf(((FieldMapAttribute)myEnumerator.Key).FieldName))!=-1)
				{
					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

					filterText = string.Format("{0}{1}{2}={3}" 
						, filterText
						,  ((num3>0) ? " AND " : "")
						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
						, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName) );


					parameters.Add( ((FieldMapAttribute)myEnumerator.Key).FieldName );
					parameterTypes.Add( ((FieldMapAttribute)myEnumerator.Key).DataType );
					parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  keyAttributeValus[num2]) );

					num3 = num3 +1; 
				}
				
				fieldText = string.Format("{0}{1}{2}" 
					, fieldText
					,((num1>0) ? "," : "")
					,((FieldMapAttribute)myEnumerator.Key).FieldName);

				num1 = num1 +1;
			}

			string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", fieldText, tableAttribute.TableName,  filterText);
			
			DataSet ds = null;

			if ( num1 > 0 )
			{
				ds = this.PersistBroker.Query(sqlText,
					(string[])parameters.ToArray(typeof(string)), 
					( Type[] )parameterTypes.ToArray(typeof(Type)), 
					(object[])parameterValues.ToArray(typeof(object)));
			}

			if (ds!=null)
			{
				if (ds.Tables[0].Rows.Count >0)
				{
					return DomainObjectUtility.FillDomainObject(DomainObjectUtility.CreateTypeInstance(type), ds.Tables[0].Rows[0]);
				}
				else
				{
					return null;
				}
			}
			
			return null;
		}

		/// <summary>
		/// 自定义查找
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="keyAttributeValus">查询条件</param>
		/// <returns>实体对象</returns>
		public virtual object[] CustomSearch(Type type, Condition condition)
		{
			if (condition == null)
			{
				TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
				Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 

				int  num1 = 0;
				string fieldText = "";
				string filterText = "1=1";
		
				IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
				while ( myEnumerator.MoveNext())
				{
					fieldText = string.Format("{0}{1}{2}" 
						, fieldText
						,((num1>0) ? "," : "")
						,((FieldMapAttribute)myEnumerator.Key).FieldName);
					num1 = num1 + 1;
				}

				string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", fieldText, tableAttribute.TableName,  filterText);
				DataSet ds = this.PersistBroker.Query(sqlText);
				return DomainObjectUtility.FillDomainObject(type, ds);	
			}
			else
			{
				//throw new Exception("CustomSearch(object domainObject, Condition[] conditions): Not implamention!");
				ExceptionManager.Raise(this.GetType(),"$Error_Not_Implamention");
				return null;
			}
		}

//		public virtual object[]  CustomSearch(Type type,  string[] attributes, object[] attributeValus)
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
//			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(attributes); 
//
//			int  num1 = 0;
//			int  num2 = 0;
//			int  num3 = 0;
//			string fieldText = "";
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//					filterText = string.Format("{0}{1}{2}={3}" 
//						, filterText
//						,  ((num3>0) ? " AND " : "")
//						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//						, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, attributeValus[num2]));
//					num3 = num3 +1;
//				}
//				
//				fieldText = string.Format("{0}{1}{2}" 
//					, fieldText
//					,((num1>0) ? "," : "")
//					,((FieldMapAttribute)myEnumerator.Key).FieldName);
//
//				num1 = num1 +1;
//			}
//
//			string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", fieldText, tableAttribute.TableName,  filterText);
//			DataSet ds = this.PersistBroker.Query(sqlText);
//			return DomainObjectUtility.FillDomainObject(type, ds);	
//		}
		/// <summary>
		/// 自定义查找
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="keyAttributeValus">属性</param>
		/// /// <param name="keyAttributeValus">值</param>
		/// <returns>实体对象</returns>
		public virtual object[]  CustomSearch(Type type,  string[] attributes, object[] attributeValus)
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 

			StringCollection sc = new StringCollection();
			sc.AddRange(attributes); 

			int  num1 = 0;
			int  num2 = 0;
			int  num3 = 0;
			string fieldText = "";
			string filterText = "";

			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();
		
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
				{
					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

					filterText = string.Format("{0}{1}{2}={3}" 
						, filterText
						,  ((num3>0) ? " AND " : "")
						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
						, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName) );

					parameters.Add( ((FieldMapAttribute)myEnumerator.Key).FieldName );
					parameterTypes.Add( ((FieldMapAttribute)myEnumerator.Key).DataType );
					parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  attributeValus[num2]) );

					num3 = num3 +1;
				}
				
				fieldText = string.Format("{0}{1}{2}" 
					, fieldText
					,((num1>0) ? "," : "")
					,((FieldMapAttribute)myEnumerator.Key).FieldName);

				num1 = num1 +1;
			}

			string sqlText = string.Format("SELECT {0} FROM {1} WHERE {2}", fieldText, tableAttribute.TableName,  filterText);

			DataSet ds = null;

			if ( num1 > 0 )
			{
				ds = this.PersistBroker.Query(sqlText,
											(string[])parameters.ToArray(typeof(string)), 
											( Type[] )parameterTypes.ToArray(typeof(Type)), 
											(object[])parameterValues.ToArray(typeof(object)));
			}

			return DomainObjectUtility.FillDomainObject(type, ds);	
		}
		/// <summary>
		/// 获取记录统计
		/// </summary>
		/// <param name="type">类型</param>
		///<param name="condition">条件</param>
		/// <returns>记录数</returns>
		public virtual int GetDomainObjectCount(Type type, Condition condition)
		{
			if (condition!=null)
			{
				//throw new Exception("GetDomainObjectCount(DomainObject domainObject, Condition[] conditions): Not implamention!");
				ExceptionManager.Raise(this.GetType(),"$Error_Get_Domain_Object_Count",null,null);
			}

			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);

			string sqlText = string.Format("SELECT count({0}) FROM {1} WHERE {2}", "*", tableAttribute.TableName,  "1=1");
			DataSet ds = this.PersistBroker.Query(sqlText);
			return System.Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
		}

//		public virtual int GetDomainObjectCount(Type type, string[] attributes, object[] attributeValus)
//		{
//			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
//			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 
//
//			StringCollection sc = new StringCollection();
//			sc.AddRange(attributes); 
//
//			int  num1 = 0;
//			int  num2 = 0;
//			int  num3 = 0;
//			string fieldText = "";
//			string filterText = "";
//		
//			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
//			while ( myEnumerator.MoveNext())
//			{
//				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
//				{
//					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;
//
//					filterText = string.Format("{0}{1}{2}={3}" 
//						, filterText
//						,  ((num3>0) ? " AND " : "")
//						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
//						, DomainObjectUtility.EncodeValue(((FieldMapAttribute)myEnumerator.Key).DataType, type1, attributeValus[num2]));
//					num3 = num3 +1;
//				}
//				
//				fieldText = string.Format("{0}{1}{2}" 
//					, fieldText
//					,((num1>0) ? "," : "")
//					,((FieldMapAttribute)myEnumerator.Key).FieldName);
//
//				num1 = num1 +1;
//			}
//
//			string sqlText = string.Format("SELECT count({0}) FROM {1} WHERE {2}", "*", tableAttribute.TableName,  filterText);
//			DataSet ds = this.PersistBroker.Query(sqlText);
//			return System.Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
//		}
		/// <summary>
		/// 获取记录统计
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="attributes">属性</param>
		/// <param name="attributeValus">值</param>
		/// <returns>记录数</returns>
		public virtual int GetDomainObjectCount(Type type, string[] attributes, object[] attributeValus)
		{
			TableMapAttribute tableAttribute = DomainObjectUtility.GetTableMapAttribute(type);
			Hashtable hs = 	DomainObjectUtility.GetAttributeMemberInfos(type); 

			StringCollection sc = new StringCollection();
			sc.AddRange(attributes); 

			int  num1 = 0;
			int  num2 = 0;
			int  num3 = 0;
			string fieldText = "";
			string filterText = "";
		
			ArrayList parameters		= new ArrayList();
			ArrayList parameterTypes	= new ArrayList();
			ArrayList parameterValues	= new ArrayList();
		
			IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
			while ( myEnumerator.MoveNext())
			{
				if ((num2 = sc.IndexOf(((MemberInfo)myEnumerator.Value).Name))!=-1)
				{
					Type type1 = ((MemberInfo) myEnumerator.Value is FieldInfo) ? ((FieldInfo) myEnumerator.Value).FieldType : ((PropertyInfo) myEnumerator.Value).PropertyType;

					filterText = string.Format("{0}{1}{2}={3}" 
						, filterText
						,  ((num3>0) ? " AND " : "")
						,  ((FieldMapAttribute)myEnumerator.Key).FieldName
						, string.Format("{0}{1}", parameterPrefix, ((FieldMapAttribute)myEnumerator.Key).FieldName) );

					parameters.Add( ((FieldMapAttribute)myEnumerator.Key).FieldName );
					parameterTypes.Add( ((FieldMapAttribute)myEnumerator.Key).DataType );
					parameterValues.Add( DomainObjectUtility.CSharpValue2DbValue(((FieldMapAttribute)myEnumerator.Key).DataType,type1,  attributeValus[num2]) );
					
					num3 = num3 +1;
				}
				
				fieldText = string.Format("{0}{1}{2}" 
					, fieldText
					,((num1>0) ? "," : "")
					,((FieldMapAttribute)myEnumerator.Key).FieldName);

				num1 = num1 + 1;
			}

			string sqlText = string.Format("SELECT count({0}) FROM {1} WHERE {2}", "*", tableAttribute.TableName,  filterText);
			
			DataSet ds = null;

			if ( num1 > 0 )
			{
				ds = this.PersistBroker.Query(sqlText,
											(string[])parameters.ToArray(typeof(string)), 
											( Type[] )parameterTypes.ToArray(typeof(Type)), 
											(object[])parameterValues.ToArray(typeof(object)));
			}

			if ( ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 )
			{
				return System.Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
			}

			return 0;
		}
		/// <summary>
		/// 自定义查询
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="condition">查询条件</param>
		/// <returns>实体对象列表</returns>
		public virtual object[]  CustomQuery(Type type, Condition condition)
		{	
			return DomainObjectUtility.FillDomainObject( type, this.ConditionQuery(condition) );	
		}
		/// <summary>
		/// 获取记录统计
		/// </summary>
		/// <param name="condition">查询条件</param>
		/// <returns>记录数</returns>
		public virtual int GetCount(Condition condition)
		{	
			return System.Int32.Parse( this.ConditionQuery(condition).Tables[0].Rows[0][0].ToString() );
		}

        /// <summary>
        /// 获取单列查询的结果
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual string[] GetStringResult(Condition condition)
        {
            DataTable dtResult = this.ConditionQuery(condition).Tables[0];
            string[] strResults = new string[dtResult.Rows.Count];
            int num1 = 0;
            foreach (DataRow dr in dtResult.Rows)
            {
                strResults[num1] = dr[0].ToString();
                num1 = num1 + 1;
            }

            return strResults;
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual DataSet QueryData(Condition condition)
        {
            return this.ConditionQuery(condition);
        }
		/// <summary>
		/// 自定义查询
		/// </summary>
		/// <param name="condition">查询条件</param>
		/// <returns>DataSet</returns>
		private DataSet ConditionQuery( Condition condition )
		{				
			if ( condition.SQLType == SQLType.Command )
			{
				SQLParamCondition paramCondition = (SQLParamCondition)condition;

				ArrayList parameterNames = new ArrayList( paramCondition.Parameters.Length );
				ArrayList parameterTypes = new ArrayList( paramCondition.Parameters.Length );
				ArrayList parameterValues = new ArrayList( paramCondition.Parameters.Length );

				foreach ( SQLParameter param in paramCondition.Parameters )
				{
					parameterNames.Add( param.Name );
					parameterTypes.Add( param.Type );
					parameterValues.Add( param.Value );
				}

				return this.PersistBroker.Query( condition.SQLText,
												(string[])parameterNames.ToArray(typeof(string)),
												(Type[])parameterTypes.ToArray(typeof(Type)),
												(object[])parameterValues.ToArray(typeof(object)) );
			}
			
			return this.PersistBroker.Query( condition.SQLText );
		}
		/// <summary>
		/// 自定义操作
		/// </summary>
		/// <param name="condition">SQL条件</param>
		public virtual void CustomExecute( Condition condition )
		{
			if ( condition.SQLType == SQLType.Command )
			{
				SQLParamCondition paramCondition = (SQLParamCondition)condition;

				ArrayList parameterNames = new ArrayList( paramCondition.Parameters.Length );
				ArrayList parameterTypes = new ArrayList( paramCondition.Parameters.Length );
				ArrayList parameterValues = new ArrayList( paramCondition.Parameters.Length );

				foreach ( SQLParameter param in paramCondition.Parameters )
				{
					parameterNames.Add( param.Name );
					parameterTypes.Add( param.Type );
					parameterValues.Add( param.Value );
				}

				this.PersistBroker.Execute( condition.SQLText,
											(string[])parameterNames.ToArray(typeof(string)),
											(Type[])parameterTypes.ToArray(typeof(Type)),
											(object[])parameterValues.ToArray(typeof(object)) );
			}
			else
			{			
				this.PersistBroker.Execute( condition.SQLText );
			}
		}
		/// <summary>
		/// 自定义操作
		/// </summary>
		/// <param name="condition">SQL条件</param>
		/// <returns>影响的记录行数</returns>
		public virtual int CustomExecuteWithReturn( Condition condition )
		{
			if ( condition.SQLType == SQLType.Command )
			{
				SQLParamCondition paramCondition = (SQLParamCondition)condition;

				ArrayList parameterNames = new ArrayList( paramCondition.Parameters.Length );
				ArrayList parameterTypes = new ArrayList( paramCondition.Parameters.Length );
				ArrayList parameterValues = new ArrayList( paramCondition.Parameters.Length );

				foreach ( SQLParameter param in paramCondition.Parameters )
				{
					parameterNames.Add( param.Name );
					parameterTypes.Add( param.Type );
					parameterValues.Add( param.Value );
				}

				return this.PersistBroker.ExecuteWithReturn( condition.SQLText,
					(string[])parameterNames.ToArray(typeof(string)),
					(Type[])parameterTypes.ToArray(typeof(Type)),
					(object[])parameterValues.ToArray(typeof(object)) );
			}
			else
			{			
				return this.PersistBroker.ExecuteWithReturn( condition.SQLText );
			}
		}

        /// <summary>
        /// 自定义操作
        /// </summary>
        /// <param name="condition">SQL条件</param>
        public virtual void CustomProcedure(ref ProcedureCondition condition)
        {
            if (condition.SQLType == SQLType.StoredProcedure)
            {
                ProcedureCondition paramCondition = (ProcedureCondition)condition;

                ArrayList paras = new ArrayList(paramCondition.Parameters.Length);

                foreach (ProcedureParameter param in paramCondition.Parameters)
                {
                    paras.Add(param);

                }

                this.PersistBroker.ExecuteProcedure(condition.SQLText, ref paras);

                for (int i = 0; i < paramCondition.Parameters.Length; i++)
                {
                    if ((paramCondition.Parameters[i].Direction == DirectionType.InputOutput) || (paramCondition.Parameters[i].Direction == DirectionType.Output) || (paramCondition.Parameters[i].Direction == DirectionType.ReturnValue))
                    {
                        paramCondition.Parameters[i].Value = ((ProcedureParameter)paras[i]).Value;
                    }
                }
            }
            else
            {
                this.PersistBroker.Execute(condition.SQLText);
            }
        }
	}
}
