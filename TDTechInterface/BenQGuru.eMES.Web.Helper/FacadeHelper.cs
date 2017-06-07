using System;
using System.Collections;
using System.Runtime.Remoting;  

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// FacadeHelper 的摘要说明。
	/// </summary>
	
	#region Check

	public interface ICheck
	{
		bool Check();
	}

	public class ExistenceCheck : ICheck
	{
		private DomainObject _obj = null;
		private IDomainDataProvider _provider = null;

		public ExistenceCheck(DomainObject obj, IDomainDataProvider dataProvider )
		{
			this._obj	= obj;
			this._provider   = dataProvider;
		}

		#region ICheck 成员

		/// <summary>
		/// 检查记录是否存在
		/// </summary>
		/// <returns>true：记录存在， false：记录不存在</returns>
		public bool Check()
		{
			if (this._obj == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", "[$Argument=DomainObject]");	
				return false;
			}
			
			if ( this._provider == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", "[$Argument=DataProvider]");	
				return false;
			}

			if (this._provider.CustomSearch(this._obj.GetType(), DomainObjectUtility.GetDomainObjectKeyScheme(this._obj.GetType()), DomainObjectUtility.GetDomainObjectKeyValues(this._obj)) == null )
			{				
				ExceptionManager.Raise(this.GetType(), "$Error_Record_Not_Exist");
				return false;
			}	
	
			return true;
		}

		#endregion

		//Check 不抛出异常,只返回true false
		public bool CheckWithNoException()
		{
			if (this._obj == null )
			{
				return false;
			}
			
			if ( this._provider == null )
			{
				return false;
			}

			if (this._provider.CustomSearch(this._obj.GetType(), DomainObjectUtility.GetDomainObjectKeyScheme(this._obj.GetType()), DomainObjectUtility.GetDomainObjectKeyValues(this._obj)) == null )
			{				
				//记录不存在
				return false;
			}	
	
			return true;
		}
	}

	public class PrimaryKeyOverlapCheck : ICheck
	{
		private DomainObject _obj = null;
		private IDomainDataProvider _provider = null;

		public PrimaryKeyOverlapCheck(DomainObject obj, IDomainDataProvider dataProvider )
		{
			this._obj	= obj;
			this._provider   = dataProvider;
		}

		#region ICheck 成员

		/// <summary>
		/// 检查记录是否存在
		/// </summary>
		/// <returns>true：记录存在， false：记录不存在</returns>
		public bool Check()
		{
			if (this._obj == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", "[$Argument=DomainObject]");	
				return false;
			}
			
			if ( this._provider == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", "[$Argument=DataProvider]");	
				return false;
			}

			if (this._provider.CustomSearch(this._obj.GetType(), DomainObjectUtility.GetDomainObjectKeyScheme(this._obj.GetType()), DomainObjectUtility.GetDomainObjectKeyValues(this._obj)) != null )
			{				
				ExceptionManager.Raise(this.GetType(), "$Error_Primary_Key_Overlap");
				return false;
			}	
	
			return true;
		}

		#endregion
	}

	public class DeleteAssociateCheck : ICheck
	{
		private DomainObject[] _objs = null;
		private IDomainDataProvider _provider = null;
		private Type[] _types;

		public DeleteAssociateCheck(DomainObject obj, IDomainDataProvider dataProvider, Type[] associateTypes )
		{
			this._objs = new DomainObject[1]{ obj };
			this._provider   = dataProvider;
			this._types = associateTypes;
		}

		public DeleteAssociateCheck(DomainObject[] objs, IDomainDataProvider dataProvider, Type[] associateTypes )
		{
			this._objs = objs;
			this._provider   = dataProvider;
			this._types = associateTypes;
		}

		#region ICheck 成员

		public bool Check()
		{			
			if (this._objs == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", "[$Argument=DomainObject]");	
				return false;
			}
			
			if ( this._provider == null )
			{
				ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", "[$Argument=DataProvider]");	
				return false;
			}

			if ( this._types == null )
			{
				return true;
			}

			foreach ( DomainObject obj in this._objs )
			{
				string[] keyAttributes = DomainObjectUtility.GetDomainObjectKeyScheme(obj.GetType());
				object[] keyAttributeValues = DomainObjectUtility.GetDomainObjectKeyValues(obj);

				foreach (Type type in this._types)
				{
					if ( type.BaseType != typeof(DomainObject) )
					{
						ExceptionManager.Raise(obj.GetType(), "$Error_Type_Should_Be_DomainObject", string.Format("[$Type=${0}]", obj.GetType().Name));	
						return false;
					}

					ArrayList array = DomainObjectUtility.GetDomainObjectScheme(type);

					foreach ( string keyAttribute in keyAttributes)
					{
						if ( !array.Contains(keyAttribute) )
						{
							ExceptionManager.Raise(obj.GetType(), "$Error_Types_Not_Associated", string.Format("[${0}, ${1}]", obj.GetType().Name, type.Name));	
							return false;
						}
					}

					if ( this._provider.CustomSearch(type, keyAttributes, keyAttributeValues) != null )
					{
						ExceptionManager.Raise(obj.GetType(), "$Error_Associate_Exist", string.Format("[$Domain_{0}, $Domain_{1}]", obj.GetType().Name, type.Name));
						return false;
					}
				}
			}

			return true;
		}

		#endregion
	}

	#endregion

	public class FacadeHelper
	{
		private IDomainDataProvider _provider;

		public FacadeHelper(IDomainDataProvider provider)
		{
			this._provider = provider;
		} 

		public void AddDomainObject(DomainObject obj)
		{
			this.AddDomainObject( obj, new ICheck[]{ new PrimaryKeyOverlapCheck( obj, this._provider ) } );
		}

		//不检查的插入 , 用于明确不存在于数据库的数据insert
		public void AddDomainObject(DomainObject obj ,bool isCheck)
		{
			if( isCheck)
			{
				this.AddDomainObject( obj, new ICheck[]{ new PrimaryKeyOverlapCheck( obj, this._provider ) } );

			}
			else
			{
				
				try
				{
					//Laws Lu,2006/11/13 uniform system collect date
					DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(_provider);
				
					DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);

					DomainObjectUtility.SetValue( obj, "MaintainDate", FormatHelper.TODateInt(workDateTime) );
					DomainObjectUtility.SetValue( obj, "MaintainTime", FormatHelper.TOTimeInt(workDateTime) );

//					DomainObjectUtility.SetValue( obj, "MDate", FormatHelper.TODateInt(workDateTime) );
//					DomainObjectUtility.SetValue( obj, "MTime", FormatHelper.TOTimeInt(workDateTime) );
				}
				catch
				{
				}

				this._provider.Insert(obj); 
			}
		}

		public void AddDomainObject(DomainObject obj, ICheck[] checkList )
		{
			foreach( ICheck check in checkList )
			{
				bool pass = true;

				try 
				{
					pass = check.Check();
				}
				catch(Exception ex)
				{
					ExceptionManager.Raise(obj.GetType(), "$Error_Add_Check", ex);

					return;
				}

				if ( !pass )
				{
					ExceptionManager.Raise(obj.GetType(), "$Error_Add_Check");

					return;
				}
			}

			try
			{
				try
				{
					//Laws Lu,2006/11/13 uniform system collect date
					DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(_provider);
				
					DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);

					DomainObjectUtility.SetValue( obj, "MaintainDate", FormatHelper.TODateInt(workDateTime) );
					DomainObjectUtility.SetValue( obj, "MaintainTime", FormatHelper.TOTimeInt(workDateTime) );
				}
				catch
				{
				}

				this._provider.Insert(obj); 
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(obj.GetType(), "$Error_Add_Domain_Object", ex);
			}
		}

		public void AddDomainObject(DomainObject[] objs)
		{
			this._provider.BeginTransaction();

			try
			{
				foreach(DomainObject obj in objs)
				{					
					this.AddDomainObject( obj );
				}

				this._provider.CommitTransaction();
			}
			catch(Exception ex)
			{	
				this._provider.RollbackTransaction();

				ExceptionManager.Raise(objs[0].GetType(), "$Error_Add_Domain_Object", ex);
			}
			finally
			{
			}
		}

		public void AddDomainObject(DomainObject[] objs, ICheck[] checkList )
		{
			if ( objs == null || objs.Length == 0 )
			{
				return;
			}

			if ( checkList != null)
			{
				foreach( ICheck check in checkList )
				{
					bool pass = true;

					try 
					{
						pass = check.Check();
					}
					catch(Exception ex)
					{
						ExceptionManager.Raise(objs[0].GetType(), "$Error_Add_Check", ex);
						return;
					}

					if ( !pass )
					{
						ExceptionManager.Raise(objs[0].GetType(), "$Error_Add_Check");	
						return;
					}
				}

				AddDomainObject(objs);
			}
		}

		public void UpdateDomainObject(DomainObject obj)
		{
			this.UpdateDomainObject( obj, new ICheck[]{ new ExistenceCheck( obj, this._provider ) } ) ;
		}
		
		public void UpdateDomainObject(DomainObject obj, ICheck[] checkList)
		{
			foreach( ICheck check in checkList )
			{
				bool pass = true;

				try 
				{
					pass = check.Check();
				}
				catch(Exception ex)
				{
					ExceptionManager.Raise(obj.GetType(), "$Error_Update_Check", ex);		
					return;
				}

				if ( !pass )
				{
					ExceptionManager.Raise(obj.GetType(), "$Error_Update_Check");	
					return;
				}
			}

			try
			{
				try
				{
					//Laws Lu,2006/11/13 uniform system collect date
					DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(_provider);
				
					DateTime workDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate,dbDateTime.DBTime);

					DomainObjectUtility.SetValue( obj, "MaintainDate", FormatHelper.TODateInt(workDateTime) );
					DomainObjectUtility.SetValue( obj, "MaintainTime", FormatHelper.TOTimeInt(workDateTime) );

				}
				catch
				{
				}

				this._provider.Update(obj); 
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(obj.GetType(), "$Error_Update_Domain_Object", ex);
			}		
		}

		public void DeleteDomainObject(DomainObject obj)
		{
			try
			{
				this._provider.Delete(obj); 
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(obj.GetType(), "$Error_Delete_Domain_Object", ex);
				return;
			}
		}

		public void DeleteDomainObject(DomainObject[] objs)
		{
			if ( objs != null )
			{
				this._provider.BeginTransaction();

				try
				{
					foreach( DomainObject obj in objs)
					{
						this.DeleteDomainObject( obj );
					}

					this._provider.CommitTransaction();
				}
				catch(Exception e)
				{
					this._provider.RollbackTransaction();
					
					ExceptionManager.Raise(objs[0].GetType(), "$Error_Delete_Domain_Object", e);
				}
			}
		}

		public void DeleteDomainObject(DomainObject obj, ICheck[] checkList)
		{
			if ( obj == null )
			{
				return;
			}

			foreach( ICheck check in checkList )
			{
				bool pass = true;

				try 
				{
					pass = check.Check();
				}
				catch(Exception ex)
				{
					ExceptionManager.Raise(obj.GetType(), "$Error_Delete_Check", ex);	
					return;
				}

				if ( !pass )
				{
					ExceptionManager.Raise(obj.GetType(), "$Error_Delete_Check");	
					return;
				}
			}

			this.DeleteDomainObject(obj);
		}

		public void DeleteDomainObject(DomainObject[] objs, ICheck[] checkList)
		{
			if ( objs == null || objs.Length == 0 )
			{
				return;
			}

			foreach( ICheck check in checkList )
			{
				bool pass = true;

				try 
				{
					pass = check.Check();
				}
				catch(Exception ex)
				{
					ExceptionManager.Raise(objs[0].GetType(), "$Error_Delete_Check", ex);	
					return;
				}

				if ( !pass )
				{
					ExceptionManager.Raise(objs[0].GetType(), "$Error_Delete_Check");	
					return;
				}
			}

			this.DeleteDomainObject( objs );
		}
	}
}
