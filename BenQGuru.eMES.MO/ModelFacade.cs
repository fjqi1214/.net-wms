#region System
using System;
using System.Runtime.Remoting;  
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Common.Helper;  
using BenQGuru.eMES.Common.DomainDataProvider;   
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.TSModel;
#endregion

/// ModelFacade 的摘要说明。
/// 文件名:
/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
/// 创建人:Crystal Chu
/// 创建日期:2005/03/22
/// 修改人: crystal chu 
/// 修改日期: 20050420  
/// 描 述: 对Model简单的操作控制，其中包括Model->Item,Model->RouteAlt,Model->之间的关系的建立
///        crystal chu  20050420   增加对一个Item只能属于Model的限制 
/// 版 本:	
/// </summary>  
namespace BenQGuru.eMES.MOModel
{
	public class ModelFacade:MarshalByRefObject
	{
		//private static readonly log4net.ILog _log = BenQGuru.eMES.Common.Log.GetLogger(typeof(MOFacade));
		private  IDomainDataProvider _domainDataProvider= null;
		private  FacadeHelper _helper					= null;

		public ModelFacade(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._helper = new FacadeHelper(DataProvider);
		}

		public override object InitializeLifetimeService()
		{
			return null;
		}

		public ModelFacade()
		{
			this._helper = new FacadeHelper(DataProvider);
		}

		protected IDomainDataProvider DataProvider
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
		

		#region Model
		public Model CreateNewModel()
		{
			return new Model();
		}


		public object GetModel(string modelCode, int orgID)
		{
            return this.DataProvider.CustomSearch(typeof(Model), new object[] { modelCode, orgID });
		}

		public void AddModel(Model model)
		{
			this._helper.AddDomainObject(model);
		}

		/// <summary>
		/// 修改机种的基本信息,只能修改对应的description
		/// </summary>
		/// <param name="model"></param>
		public void UpdateModel(Model model)
		{
			this._helper.UpdateDomainObject(model);
		}

		/// <summary>
		/// 删除model, 如果被使用则不能被删除,主要检查的关系有item->model,model->routealt,model->route
		/// </summary>
		/// <param name="model"></param>
		public void DeleteModel(Model model)
		{
			this._helper.DeleteDomainObject(
				model,
				new ICheck[]{
								new DeleteAssociateCheck(
								model,this.DataProvider,
								new Type[]{
											  typeof(Item),
											  typeof(Model2Item),
							typeof(Model2ErrorCause),
							typeof(Model2ErrorCodeGroup),
							typeof(Model2Solution)})});
		}


		/// <summary>
		/// 删除model,删除的群体操作,
		/// </summary>
		/// <param name="models"></param>
		public void DeleteModel(Model[] models)
		{
			this._helper.DeleteDomainObject(models,new ICheck[]{new DeleteAssociateCheck(models,this.DataProvider,new Type[]{typeof(Model2Item)})});
		}
		/// <summary>
		/// QueryModels不支持模糊查询
		/// </summary>
		/// <param name="model">可以为空，返回所有</param>
		/// <returns></returns>
		/// 
		public object[] QueryModels(string model)
		{
            string selectSql = "select  " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model)) + " from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {0}";
			object[] objs = new object[1];
			string tmpString = string.Empty;
			if((model != string.Empty)&&(model.Trim() != string.Empty))
			{
				tmpString += " and modelcode ='"+model.Trim()+"'";
			}
			objs[0] = tmpString;
			return this.DataProvider.CustomQuery(typeof(Model),new SQLCondition(String.Format(selectSql+" order by modelcode ",objs)));
		}

		//通过产品获取机种
		public Model QueryModelByItem(string _itemCode)
		{
            string selectSql = string.Format("select  " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model)) + " from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode='{0}' ", _itemCode);
			object[] objs = this.DataProvider.CustomQuery(typeof(Model),new SQLCondition(selectSql));
			if(objs!=null && objs.Length>0)
			{
				return (Model)objs[0];
			}
			return null;
		}

		public object[] QueryModelByItems(string _itemCodes)
		{
			//string selectSql = string.Format("select  "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model))+" from tblmodel where itemcode in (" + FormatHelper.ProcessQueryValues( _itemCodes.ToUpper() ) + ")");
            string selectSql = string.Format(" select  {0} FROM tblmodel WHERE 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and modelcode IN ( select modelcode from tblmodel2item where itemcode in ({1}) " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model)), FormatHelper.ProcessQueryValues(_itemCodes.ToUpper()));
			object[] objs = this.DataProvider.CustomQuery(typeof(Model),new SQLCondition(selectSql));
			return objs;
		}


		public object[] GetAllModels()
		{
            return this.DataProvider.CustomQuery(typeof(Model), new SQLCondition(string.Format("select {0} from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by modelcode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model)))));
		}


		/// <summary>
		/// 对字段model使用模糊查询
		/// </summary>
		/// <param name="model"></param>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns></returns>
		public object[] QueryModels(string model,int inclusive,int exclusive)
		{
            string selectSql = "select" + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model)) + "  from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {0}";
			object[] objs = new object[1];
			string tmpString = string.Empty;
			if((model != string.Empty)&&(model.Trim() != string.Empty))
			{
				tmpString += " and modelcode like '"+model.Trim()+"%'";
			}
			objs[0] = tmpString;
			return this.DataProvider.CustomQuery(typeof(Model),new PagerCondition(String.Format(selectSql,objs),inclusive,exclusive));
		}


		public int QueryModelsCount(string model)
		{
            string selectSql = "select count(modelcode) from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {0}";
			object[] objs = new object[1];
			string tmpString = string.Empty;
			if((model != string.Empty)&&(model.Trim() != string.Empty))
			{
				tmpString += " and modelcode like '"+model.Trim()+"%'";
			}
			objs[0] = tmpString;
			return this.DataProvider.GetCount(new SQLCondition(String.Format( selectSql,objs)));
		}

		#endregion

		#region  private method
	
		/// <summary>
		/// 用来判断该model是否使用，只检查一层关系
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		private bool IsModelUsed(Model model)
		{
			if(model == null)
			{ 
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
				//throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),"ISMODELUSED_ERROR"));
			}
			if (this.DataProvider.CustomQuery(typeof(Model2Item), new SQLCondition(String.Format("select modelcode,itemcode,muser,mdate,mtime from tblmodel2item where model ='{0}' and orgid=" + model.OrganizationID,model.ModelCode)))!=null)
			{
				return true;
			}
			return false;
			
		}
		public object GetModel2Item(string modelCode,string itemCode,int orgID)
		{
			return this.DataProvider.CustomSearch(typeof(Model2Item),new object[]{modelCode,itemCode,orgID});
		}

        public void AddModel2Item(Model2Item model2Item)
        {
            this._helper.AddDomainObject(model2Item);
        }
        public void UpdateModel2Item(Model2Item model2Item)
        {
            this._helper.UpdateDomainObject(model2Item);
        }

			
		#endregion

		#region Model->Item
		public Model2Item CreateModel2Item()
		{
			return new Model2Item();
		}
		/// <summary>
		/// 通过Item获取Model2Item
		/// </summary>
		/// <param name="itemCode"></param>
		/// <returns></returns>
		public object[] GetModel2ItemByItemCode(string itemCode)
		{
			if( itemCode == "" )
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Argument_Null");
				return null;
			}
			
			return this.DataProvider.CustomQuery(typeof(Model2Item),new SQLCondition(String.Format("select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2Item))+" from tblmodel2item where itemcode ='{0}'" + GlobalVariables.CurrentOrganizations.GetSQLCondition(), itemCode)));
		}

        public object GetModel2ItemByItemCode(string itemCode, int orgid)
        {
            string sqlStr = string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2Item)) + " from tblmodel2item where itemcode ='{0}' and ORGID='{1}'", itemCode, orgid);
            object[] objs = this.DataProvider.CustomQuery(typeof(Model2Item), new SQLCondition(sqlStr));
            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }

		/// <summary>
		/// 完成把item的数组assign给model,含事务
		/// </summary>
		/// <param name="model">model不能为空</param>
		/// <param name="items">Items不能为null,同时一个item只能属于一个Model</param>
		public void AssignItemsToModel(Model2Item[] model2Items)
		{
			if( model2Items == null )
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Argument_Null");
				return ;
			}
			try
			{
				this.DataProvider.BeginTransaction();
				for(int i=0;i<model2Items.Length;i++)
				{
					//update by crystalchu 20050420
					//增加对一个Item只能属于Model的限制 
					object[] objs= this.DataProvider.CustomQuery(typeof(Model2Item),new SQLCondition(String.Format("select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2Item))+" from tblmodel2item where itemcode ='{0}'"+GlobalVariables.CurrentOrganizations.GetSQLCondition(), model2Items[i].ItemCode)));
					if(objs != null)
					{
						ExceptionManager.Raise(this.GetType(),"$Error_Model2Item_ItemExisted",String.Format("[$ItemCode='{0}']",model2Items[i].ItemCode),null);
					}
					this.DataProvider.Insert(model2Items[i]);
				}
				this.DataProvider.CommitTransaction();
			}
			catch(Exception ex)
			{
				//_log.Error(ex.Message,ex);
				this.DataProvider.RollbackTransaction();
				ExceptionManager.Raise(this.GetType(),"$Error_AssignItemToModel_Failure",ex);
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ASSIGNITEMSTOMODEL,modelCode)),ex);
			}
		}

		//没有事务的方法,供ItemFacade使用
		public void AssignItemsToModelNoTransAction(Model2Item[] model2Items)
		{
			if( model2Items == null )
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Argument_Null");
				return ;
			}
			try
			{
				for(int i=0;i<model2Items.Length;i++)
				{
					//update by crystalchu 20050420
					//增加对一个Item只能属于Model的限制 
					object[] objs= this.DataProvider.CustomQuery(typeof(Model2Item),new SQLCondition(String.Format("select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2Item))+" from tblmodel2item where itemcode ='{0}' and orgid=" + model2Items[i].OrganizationID, model2Items[i].ItemCode)));
					if(objs != null)
					{
						ExceptionManager.Raise(this.GetType(),"$Error_Model2Item_ItemExisted",String.Format("[$ItemCode='{0}']",model2Items[i].ItemCode),null);
					}
					this.DataProvider.Insert(model2Items[i]);
				}
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_AssignItemToModel_Failure",ex);
			}
		}

		//没有事务的方法,供ItemFacade使用
		public void RemoveItemsFromModelNoTransAction(Model2Item[] model2Items)
		{
			if( model2Items == null )
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
			}
			try
			{
				for(int i=0;i<model2Items.Length;i++)
				{
					this.DataProvider.Delete(model2Items[i]);
				}
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_RemoveItemsFromModel_Failure",ex);
			}
		}

		/// <summary>
		/// 去掉把item的数组和model之间的关系,含事务
		/// </summary>
		/// <param name="model">model不能为空</param>
		/// <param name="items">Items不能为null</param>
		public void RemoveItemsFromModel(Model2Item[] model2Items)
		{
			if( model2Items == null )
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
			}
			try
			{
				this.DataProvider.BeginTransaction();
				for(int i=0;i<model2Items.Length;i++)
				{
					this.DataProvider.Delete(model2Items[i]);
				}
				this.DataProvider.CommitTransaction();
			}
			catch(Exception ex)
			{
				//_log.Error(ex.Message,ex);
				this.DataProvider.RollbackTransaction();
				ExceptionManager.Raise(this.GetType(),"$Error_RemoveItemsFromModel_Failure",ex);
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_REMOVEITEMSFROMMODEL,modelCode)),ex);
			}
		}

		/// <summary>
		/// 获得于该modelcode没有建立关系的item的信息
		/// sammer kong 20050411
		/// </summary>
		/// <param name="modelCode"></param>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns>返回object的数组</returns>
		public object[] GetUnSelectedItems(string modelCode,string itemCode,string itemName,string itemType,int inclusive,int exclusive)
		{
			if((modelCode == string.Empty)||(modelCode.Trim() == string.Empty))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"modelCode")));
			}
			string tmpString = string.Empty;
			if((itemName != null)&&(itemName.Trim() != string.Empty))
			{
				tmpString +=" and upper(itemname) like '"+itemName.Trim().ToUpper()+"%'";
			}
			if((itemType != null)&&(itemType.Trim() != string.Empty))
			{
				tmpString +=" and itemtype= '"+itemType.Trim()+"'";
			}
            return this.DataProvider.CustomQuery(typeof(Item), new PagerCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)) + " from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode not in (select itemcode from tblmodel2item where 1=1" + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and itemcode like '{0}%' {1}", itemCode, tmpString), inclusive, exclusive));
		}


		public int GetUnSelectedItemsCounts(string modelCode,string itemCode,string itemName,string itemType)
		{
			if((modelCode == string.Empty)||(modelCode.Trim() == string.Empty))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"modelCode")));
			}
			string tmpString = string.Empty;
			if((itemName != null)&&(itemName.Trim() != string.Empty))
			{
				tmpString +=" and upper(itemname) like '"+itemName.Trim().ToUpper()+"%'";
			}
			if((itemType != null)&&(itemType.Trim() != string.Empty))
			{
				tmpString +=" and itemtype= '"+itemType.Trim()+"'";
			}
            return this.DataProvider.GetCount(new SQLCondition(String.Format("select count(itemcode) from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode not in (select itemcode from tblmodel2item where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and itemcode like '{0}%'  {1} ", itemCode, tmpString)));
		}
		
		/// <summary>
		///获得于机种建立关系的items
		/// </summary>
		/// <param name="modelCode"></param>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns>返回object数组</returns>
		public object[] GetSelectedItems(string modelCode,string itemCode,int inclusive,int exclusive)
		{
			if((modelCode == string.Empty)||(modelCode.Trim() == string.Empty))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"modelCode")));
			}
            return this.DataProvider.CustomQuery(typeof(Item), new PagerCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)) + " from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode in ( select itemcode from tblmodel2item where modelcode='{0}'" + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and itemcode like '{1}%'", modelCode, itemCode), inclusive, exclusive));
		}

		public int GetSelectedItemsCounts(string modelCode,string itemCode)
		{
			if((modelCode == string.Empty)||(modelCode.Trim() == string.Empty))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"modelCode")));
			}
            return this.DataProvider.GetCount(new SQLCondition(String.Format("select count(itemcode) from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode in(select itemcode from tblmodel2item where modelcode='{0}' and itemcode like '{1}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")", modelCode, itemCode)));
		}

		public Model2Item CreateNewModel2Item()
		{
			return new Model2Item();
		}
		public Model GetModelByItemCode(string itemCode)
		{
            object[] objs = this.DataProvider.CustomQuery(typeof(Model), new SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model)) + "  from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and modelcode in (select modelcode from tblmodel2item where itemcode ='{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")", itemCode)));
			if(objs != null)
			{
				return  (Model)objs[0];
			}
			else
			{
				return null;
			}
		}
		
		public object[] GetModelAllItem(string modelcode)
		{
			return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model2Item),
														new SQLCondition(string.Format("select * from tblmodel2item where modelcode ='{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by itemcode",modelcode)));
		}

		#endregion

		#region Model->Route
		public  Model2Route CreateNewModel2Route()
		{
			return new Model2Route();
		}
		/// <summary>
		/// 增加Model2Route,同时不Route下面所有的Operation复制过来
		/// </summary>
		/// <param name="model2Route"></param>
		public void AddModelRoute(Model2Route model2Route)
		{
			BaseModelFacade _baseModelFacade = new BaseModelFacade(this.DataProvider);
			try
			{
				//DataProvider.BeginTransaction();
				//add item2route]
				//Laws Lu,2006/11/13 uniform system collect date
				DBDateTime dbDateTime;
						
				dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

				model2Route.MaintainDate = dbDateTime.DBDate;
				model2Route.MaintainTime = dbDateTime.DBTime;
				this._helper.AddDomainObject(model2Route);
				//this.DataProvider.Insert(model2Route); 
				//add item2op
				object[] operations = _baseModelFacade.GetSelectedOperationByRouteCode(model2Route.RouteCode,string.Empty,int.MinValue,int.MaxValue);
				if(operations == null)
				{
					ExceptionManager.Raise(this.GetType().BaseType,"$Error_RouteHasNoOperations",string.Format("[$RouteCode='{0}']",model2Route.RouteCode),null);
				}
				for(int i=0;i<operations.Length;i++)
				{
					Model2OP model2Operation = new Model2OP();
					
					model2Operation.RouteCode = model2Route.RouteCode;
					model2Operation.ModelCode = model2Route.ModelCode;
					model2Operation.OPCode = ((Operation)operations[i]).OPCode;
					model2Operation.OPSequence = i;
					model2Operation.OPControl = ((Operation)operations[i]).OPControl;
					model2Operation.IDMergeType = IDMergeType.IDMERGETYPE_IDMERGE;
					model2Operation.IDMergeRule = 1;
					model2Operation.MaintainUser = model2Route.MaintainUser;
					model2Operation.MaintainDate = model2Route.MaintainDate;
					model2Operation.MaintainTime = model2Route.MaintainTime;
					model2Operation.OPID = CreateOPID(model2Route.ModelCode,model2Route.RouteCode, ((Operation)operations[i]).OPCode);
                    model2Operation.OrganizationID = model2Route.OrganizationID;
					this._helper.AddDomainObject(model2Operation);
					//DataProvider.Insert(model2Operation);
				}
				//DataProvider.CommitTransaction();
			}
			catch(Exception ex)
			{
				//_log.Error(ex.Message);
				//DataProvider.RollbackTransaction();
				ExceptionManager.Raise(this.GetType(),"$Error_AddModelRoute_Failure",String.Format("[$ModelCode='{0}',$RouteCode='{1}' ]",model2Route.ModelCode,model2Route.RouteCode),ex);
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ADDMODELROUTE,model2Route.ModelCode,model2Route.RouteCode)),ex);
			}
		}

		private string CreateOPID(string modelCode,string routeCode,string opCode)
		{
			return modelCode+routeCode+opCode;
		}


		public void AddModelRoute(Model2Route[] model2Routes)
		{
			this.DataProvider.BeginTransaction();
			try
			{
				for(int i=0;i<model2Routes.Length;i++)
				{
					AddModelRoute(model2Routes[i]);
				}
				this.DataProvider.CommitTransaction();
			}
			catch(Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				ExceptionManager.Raise(this.GetType(),"$Error_AddModelRoutes_Failure",ex);
			}
		}

		/// <summary>
		/// 通过察看有没有在工单选择该route如果有则不能删除
		/// 没有则可以删除，删除也删除对应copy途程的详细的信息
		/// </summary>
		/// <param name="model2Route"></param>
		private void DeleteModelRoute(Model2Route model2Route)
		{
			if(model2Route == null)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"model2Route")));
			}
			MOFacade _moFacade = new MOFacade(this.DataProvider);
			if(_moFacade.IsModelRouteUsed(model2Route.RouteCode))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_ModelRoute_Used",String.Format("[$ModelCode='{0}']",model2Route.ModelCode),null);
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),string.Format(ErrorCenter.ERROR_MODELROUTEUSED,model2Route.ModelCode,model2Route.RouteCode)));
			}
			try
			{
				object[] modelOperations = GetModel2Operations(model2Route);
				for(int i=0;i<modelOperations.Length;i++)
				{
					this.DataProvider.Delete(modelOperations[i]);
				}
				this.DataProvider.Delete(model2Route);
			}
			catch(Exception ex)
			{
				//_log.Error(ex.Message);
				ExceptionManager.Raise(this.GetType(),"$Error_DeleteModelRoute_Failure",String.Format("[$ModelCode='{0}',$RouteCode='{1}']",model2Route.ModelCode,model2Route.RouteCode),ex);
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_DELETEMODELROUTE,model2Route.ModelCode,model2Route.RouteCode)),ex);
			}
		}

		public  void DeleteModelRoute(Model2Route[] model2Routes)
		{
			try
			{
				this.DataProvider.BeginTransaction();
				for(int i=0;i<model2Routes.Length;i++)
				{
					DeleteModelRoute(model2Routes[i]);
				}
				this.DataProvider.CommitTransaction();
			}
			catch(Exception ex)
			{
				//_log.Error(ex.Message);
				this.DataProvider.RollbackTransaction();
				ExceptionManager.Raise(this.GetType(),"$Error_DeleteModelRoute",ex);
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),ErrorCenter.ERROR_DELETEMODELROUTES),ex);
			}
		}

		public  object GetModel2Route(string routeCode,string modelCode, int orgID)
		{
			return this.DataProvider.CustomSearch(typeof(Model2Route),new object[] {routeCode,modelCode,orgID});
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="model2Operation">只能修改sequence和control</param>
		public void UpdateModel2Operation(Model2OP model2Operation)
		{
			try
			{
				//Laws Lu,2006/11/13 uniform system collect date
				DBDateTime dbDateTime;
						
				dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

				model2Operation.MaintainDate = dbDateTime.DBDate;
				model2Operation.MaintainTime = dbDateTime.DBTime;
				this.DataProvider.Update(model2Operation); 
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_UpdateModel2Operation_Failure",String.Format("[$ModelCode='{0}',$OPCode='{1}']",model2Operation.ModelCode,model2Operation.OPCode),ex);
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade), string.Format(ErrorCenter.ERROR_UPDATEMODEL2OPERATION,model2Operation.ModelCode,model2Operation.OPCode)), ex);
			}
		}

		public object[] GetModel2Routes(string modelCode,string routeCode,int orgID,int inclusive,int exclusive)
		{
			string selectSql = "select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2Route)) +" from tblmodel2route where  1=1 {0}";
			object[] objs = new object[1];
			string tmpString = string.Empty;
			if((modelCode != string.Empty)&&(modelCode.Trim() != string.Empty))
			{
				tmpString += " and modelcode ='"+modelCode.Trim()+"'";
			}
			if((routeCode != string.Empty)&&(routeCode.Trim() != string.Empty))
			{
				tmpString += " and routecode ='"+routeCode.Trim()+"'";
			}

            if (orgID != 0)
            {
                tmpString += " and orgid=" + orgID;
            }

			objs[0] = tmpString;
			return this.DataProvider.CustomQuery(typeof(Model2Route),new PagerCondition(String.Format(selectSql,objs),inclusive,exclusive));
		}

		public int GetModel2RoutesCounts(string modelCode,string routeCode,int orgID)
		{
			string selectSql = "select count(*) from tblmodel2route where  1=1 {0}";
			object[] objs = new object[1];
			string tmpString = string.Empty;
			if((modelCode != string.Empty)&&(modelCode.Trim() != string.Empty))
			{
				tmpString += " and modelcode ='"+modelCode.Trim()+"'";
			}
			if((routeCode != string.Empty)&&(routeCode.Trim() != string.Empty))
			{
				tmpString += " and routecode ='"+routeCode.Trim()+"'";
			}
            if (orgID != 0)
            {
                tmpString += " and orgid=" + orgID;
            }
			objs[0] = tmpString;
			return this.DataProvider.GetCount(new SQLCondition(String.Format(selectSql,objs)));
		}

		public object[] GetModel2Operations(Model2Route model2Route)
		{
			if(model2Route == null)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"model2Route")));
			}
			return this.DataProvider.CustomQuery(typeof(Model2OP),
				new SQLCondition(String.Format("select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2OP))+" from tblmodel2op where modelcode='{0}' and routecode='{1}' and orgid={2}",model2Route.ModelCode,model2Route.RouteCode,model2Route.OrganizationID)));
		}

		public object[] GetModel2Operations(string modelCode,string routeCode,int orgID)
		{
			if((modelCode == null)||(modelCode==string.Empty)||(routeCode == null)||(routeCode==string.Empty))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
				//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"model2Route")));
			}
			return this.DataProvider.CustomQuery(typeof(Model2OP),
				new SQLCondition(String.Format("select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2OP))+" from tblmodel2op where modelcode='{0}' and routecode='{1}' and orgid={2}",modelCode,routeCode,orgID)));
		}

		public object[] GetSelectedRoutesByModelCode(string modelCode,int inclusive,int exclusive)
		{
			string selectSql =" select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route))+" from tblroute where routecode in (select routecode from tblmodel2route where 1=1 {0} )";
			string tmpString = string.Empty;
			if((modelCode != string.Empty)&&(modelCode.Trim() != string.Empty))
			{
				tmpString += " and modelcode ='"+modelCode.Trim()+"'";
			}
            if (GlobalVariables.CurrentOrganizations.First() != null)
            {
                tmpString += " and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            }
			return this.DataProvider.CustomQuery(typeof(Route),new PagerCondition(String.Format(selectSql,tmpString),inclusive,exclusive));
		}

		public int GetSelectedRouteCountByModelCode(string modelCode)
		{
			string selectSql =" select count(*) from tblroute where routecode in (select routecode from tblmodel2route where 1=1 {0} )";
			string tmpString = string.Empty;
			if((modelCode != string.Empty)&&(modelCode.Trim() != string.Empty))
			{
				tmpString += " and modelcode ='"+modelCode.Trim()+"'";
			}
            if (GlobalVariables.CurrentOrganizations.First() != null)
            {
                tmpString += " and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            }
			return this.DataProvider.GetCount(new SQLCondition(String.Format(selectSql,tmpString)));
		}


		public object[] GetModel2RoutesByItemCode(string itemCode)
		{
			string selectSql="select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2Route))+"  from tblmodel2route where modelcode in (select modelcode from tblmodel2item where 1=1 {0}) ";
            if (GlobalVariables.CurrentOrganizations.First() != null)
            {
                selectSql += " and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            }
			string tmpString = string.Empty;
			if((itemCode != string.Empty)&&(itemCode.Trim() != string.Empty))
			{
				tmpString += " and itemcode ='"+itemCode.Trim()+"'";
			}
            if (GlobalVariables.CurrentOrganizations.First() != null)
            {
                tmpString += " and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            }
			return this.DataProvider.CustomQuery(typeof(Model2Route),new SQLCondition(String.Format(selectSql,tmpString)));
		}
	

		public object[] GetUnSelectedRoutesByModelCode(string modelCode,string routeCode,int inclusive,int exclusive)
		{
			string selectSql =" select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route))+" from tblroute where routecode  not in (select routecode from tblmodel2route where 1=1 {0} ) {1}";
		
			object[] objs = new object[2];
			if((modelCode != string.Empty)&&(modelCode.Trim() != string.Empty))
			{
				objs[0] = " and modelcode ='"+modelCode.Trim()+"'";
			}
			if((routeCode != string.Empty)&&(routeCode.Trim() != string.Empty))
			{
				objs[1]= " and routecode ='"+routeCode.Trim()+"'";
			}
            if (GlobalVariables.CurrentOrganizations.First() != null)
            {
                objs[0] += " and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            }
			return this.DataProvider.CustomQuery(typeof(Route),new PagerCondition(String.Format(selectSql,objs),inclusive,exclusive));
		}

		public int GetUnSelectedRouteCountsByModelCode(string modelCode,string routeCode)
		{
			string selectSql =" select count(*) from tblroute where routecode not in (select routecode from tblmodel2route where 1=1 {0} ) {1}";
			object[] objs = new object[2];
			if((modelCode != string.Empty)&&(modelCode.Trim() != string.Empty))
			{
				objs[0] = " and modelcode ='"+modelCode.Trim()+"'";
			}
			if((routeCode != string.Empty)&&(routeCode.Trim() != string.Empty))
			{
				objs[1]= " and routecode ='"+routeCode.Trim()+"'";
			}
            if (GlobalVariables.CurrentOrganizations.First() != null)
            {
                objs[0] += " and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;
            }
			return this.DataProvider.GetCount(new SQLCondition(String.Format(selectSql,objs)));
		}



		public object[] GetModel2Operations(string modelCode,string routeCode,int orgID,int inclusive,int exclusive)
		{
			if((modelCode ==string.Empty)||(routeCode == string.Empty))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"modelCode")));
			}
			return this.DataProvider.CustomQuery(typeof(Model2OP),
				new PagerCondition(String.Format("select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2OP))+" from tblmodel2op where modelcode='{0}' and routecode='{1}' and orgid={2}",modelCode,routeCode,orgID),inclusive,exclusive));

		}

		public int GetModel2OperationsCounts(string modelCode,string routeCode,int orgID)
		{
			if((modelCode ==string.Empty)||(routeCode == string.Empty))
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
//				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"modelCode")));
			}
			return this.DataProvider.GetCount(
				new SQLCondition(String.Format("select count(opid) from tblmodel2op where modelcode='{0}' and routecode='{1}' and orgid={2}",modelCode,routeCode,orgID)));
		}
		



//
//		public int GetComponentLoadingOperationsCounts(string itemCode,string routeCode)
//		{
//			if((itemCode ==string.Empty)||(routeCode == string.Empty))
//			{
//				ExceptionManager.Raise(this.GetType(),"$Error_Null_Paramter");
////				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(ModelFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"itemCode")));
//			}
//			return this.DataProvider.GetCount(
//				new SQLCondition(String.Format("select count(*) from tblmodel2op where routecode='{1}'"+
//				" and modelcode in(select modelcode from tblmodel2item where itemcode='{0}') ",itemCode,routeCode)));
//		}

		public object GetModel2Operation(string opID,int orgID)
		{
			return this.DataProvider.CustomSearch(typeof(Model2OP),new object[]{opID,orgID});
		}


		public Model2OP CreateNewModel2Operation()
		{
			return new Model2OP();
		}
		

		#endregion


		#region ModelBarcodeRule
		public BarcodeRule CreateNewBarcodeRule()
		{
			return new BarcodeRule();
		}


		public object GetBarcodeRule(string modelCode,string aModelCode)
		{
			return this.DataProvider.CustomSearch(typeof(BarcodeRule),new object[]{modelCode,aModelCode});
		}

		public void AddBarcodeRule(BarcodeRule barcodeRule)
		{
			this._helper.AddDomainObject(barcodeRule);
		}

		public void UpdateBarcodeRule(BarcodeRule barcodeRule)
		{
			this._helper.UpdateDomainObject(barcodeRule);
		}

		public void DeleteBarcodeRule(BarcodeRule[] barcodeRules)
		{
			this._helper.DeleteDomainObject(barcodeRules);
		}

		public object[] QueryIllegibility(string modelCode,string aModelCode,int inclusive,int exclusive)
		{
			string selectSql = " select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(BarcodeRule))+" from tblbarcoderule where 1=1 {0}";
			string tmpString = string.Empty;
			if((modelCode != string.Empty)&&(modelCode.Trim() != string.Empty))
			{
				tmpString += " and modelcode like '"+modelCode.ToUpper()+"%'";
			}
			if((aModelCode != string.Empty)&&(aModelCode.Trim() != string.Empty))
			{
				tmpString += " and upper(amodelcode) like '"+aModelCode.ToUpper()+"%'";
			}
			return this.DataProvider.CustomQuery(typeof(BarcodeRule),new PagerCondition(String.Format(selectSql,tmpString),inclusive,exclusive));
		}

		public int  QueryIllegibilityCounts(string modelCode,string aModelCode)
		{
			string selectSql = " select count(*) from tblbarcoderule where 1=1 {0}";
			string tmpString = string.Empty;
			if((modelCode != string.Empty)&&(modelCode.Trim() != string.Empty))
			{
				tmpString += " and modelcode like '"+modelCode.ToUpper()+"%'";
			}
			if((aModelCode != string.Empty)&&(aModelCode.Trim() != string.Empty))
			{
				tmpString += " and upper(amodelcode) like '"+aModelCode.ToUpper()+"%'";
			}
			return this.DataProvider.GetCount(new SQLCondition(String.Format(selectSql,tmpString)));
		}
		
		
		#endregion

       
    }
		
}