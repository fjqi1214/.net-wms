#region System
using System;
using System.Text;
using System.Runtime.Remoting;  
using System.Collections;
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
using BenQGuru.eMES.Domain.Rework;
#endregion

namespace BenQGuru.eMES.SAPData
{
	/// <summary>
	/// SAPMaper 导入数据到MES
	/// </summary>
	public class SAPImporter
	{
		private  IDomainDataProvider _domainDataProvider= null;
		private  FacadeHelper _helper					= null;
		
		
		public SAPImportLoger importLogger; 
		public int pageCountNum = 0;	    //导入数据的当前页数
		public int SucceedImportNum = 0;	//成功导入数据的数量

		#region 缓存对象

		private Hashtable InitailMOCodeHT = null;					//所有初始状态的工单代码

		private static Hashtable AllItemCodeHT;						//MES 系统中所有的产品代码

		private static Hashtable DeleteSbomItemCodeHT;				//已删除标准bom的产品代码

		private static Hashtable ToDBMOCodesHT = new Hashtable();	//当前导入已经处理(新增或者修改的)的MOCode

		private static Hashtable AllSAPMOCodesHT = new Hashtable();	//当前获取的所有sap最新的MOCode

		#endregion
		
		public SAPImporter(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
			this._helper = new FacadeHelper(DataProvider);
		}

		protected IDomainDataProvider DataProvider
		{
			get
			{
				if (_domainDataProvider == null)
				{
					//TODO 此处应该连接MES的数据库
					_domainDataProvider = DomainDataProviderManager.DomainDataProvider(DBName.MES);
				}
				return _domainDataProvider;
			}	
		}

		#region 导入已经映射的MES数据

		public bool Import(ArrayList importDatas)
		{
			#region 参数判断
			
			if(importDatas == null)return false;
			if(!(importDatas.Count>0))return false;
			//类型判断
			
			#endregion

			string messagePage =  string.Format("第{0}页 ",pageCountNum.ToString(),DateTime.Now.ToString());
			int addCount = 0;
			int updateCount = 0;
			
			this.DataProvider.BeginTransaction();

			//导入标准bom之前删除标准bom
			DeleteSBomByItemCode(importDatas);

			try
			{
				foreach(DomainObject obj in importDatas)
				{
					bool isMO = (obj.GetType() == typeof(MO));		 //判断当前对象是否MO
					
					
					//检查产品是否存在,如果不存在则不导入
					if(!CheckIfExistItem(obj)) continue;

					if(isMO)
					{
						AllSAPMOCodesHT.Add(((MO)obj).MOCode,(MO)obj);
						DealMOObject(obj);	//对象关联处理
					}

					//检查是否存在
					if(isMO && this.CheckIfExist(obj))//如果是工单,则只更新初始状态的工单
					{
						//如果是工单,则只更新初始状态的工单
						if(obj.GetType() == typeof(MO))
						{
							if(!CheckIfInitailMO((MO)obj))
							{
								continue;
							}
						}
						
						//如果记录已经存在,则更新
						this._helper.UpdateDomainObject(obj);
						updateCount++;
						if(isMO)
						{
							//记录下发生数据库操作的工单
							ToDBMOCodesHT.Add(((MO)obj).MOCode,(MO)obj);
						}
					}
					else	//处理不是MO的数据
					{
						System.Type entityType = obj.GetType() ;
						//否则新增
						if(entityType == typeof(MOBOM))
						{
							//工单bom
							//if(ToDBMOCodesHT.Contains(((MOBOM)obj).MOCode))	//只更新发生过数据操作的工单对应的工单bom ,此逻辑已注销.
							//只要是sap更新的工单,其mobom一定要更新,AllSAPMOCodesHT为所有sap更新的工单
							if(AllSAPMOCodesHT.Contains(((MOBOM)obj).MOCode))
							{
								try
								{
									addCount ++;
									this._helper.AddDomainObject(obj,false);
								}
								catch
								{
									//工单BOM可能有重复数据.累加单机用量
									this.UpdateMOBom(((MOBOM)obj));
								}
								
							}
						}
						else if(entityType == typeof(SBOM))
						{
							try
							{
								this._helper.AddDomainObject(obj,false);
								addCount ++;
							}
							catch
							{
								//标准BOM可能有重复数据.在log中记录重复的数据.
								string msg = string.Format("标准bom数据重复 产品代码{0}  子阶料号{1}  单机用量{2}  产品生效日期{3}",((SBOM)obj).ItemCode,((SBOM)obj).SBOMItemCode,((SBOM)obj).SBOMItemQty,((SBOM)obj).SBOMItemEffectiveDate);
								if(importLogger!=null){importLogger.Write(msg);}
							}
						}
						else
						{
							try
							{
								this._helper.AddDomainObject(obj);
								addCount ++;
							}
							catch
							{
								//工单可能有重复数据.在log中记录重复的数据.
								string msg = string.Format("工单数据重复 工单{0} 产品代码{1} ",((MO)obj).MOCode,((MO)obj).ItemCode);
								if(importLogger!=null){importLogger.Write(msg);}
							}
							
							if(isMO)
							{
								//记录下发生数据库操作的工单
								ToDBMOCodesHT.Add(((MO)obj).MOCode,(MO)obj);
							}
						}

						//if(importLogger !=null){importLogger.Write(this.GetLog(obj,JobActionResult.ADD));}	//因为数据量较大,新增的数据暂时不写log
					}
				}
				//messagePage += "  " + DateTime.Now.ToString();
				this.DataProvider.CommitTransaction();
				
			}
			catch(Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				this.RobackDeleteItemHT(importDatas);
				if(importLogger !=null){importLogger.Write("导入出现异常 " +ex.Message);}
				return false;
			}
			SucceedImportNum = addCount + updateCount;
			if(importLogger !=null){importLogger.Write(string.Format("{0}新增数据{1}条 , 修改数据{2}条 共导入数据{3}条",messagePage,addCount,updateCount,SucceedImportNum));}
			return true;
		}

		#region 导入检查

		//检查记录是否存在
		private bool CheckIfExist(DomainObject obj)
		{
			bool ifExist = false;	//记录是否存在,默认不存在
			ICheck[] checkList= new ICheck[]{new ExistenceCheck( obj, this.DataProvider )};
			foreach( ExistenceCheck check in checkList )
			{
				try 
				{
					ifExist = check.CheckWithNoException();
				}
				catch(Exception ex)
				{
					return false;
				}
			}

			return ifExist;
		}
		
		//检查是否存在产品
		private bool CheckIfExistItem(DomainObject obj)
		{
			if(AllItemCodeHT == null){AllItemCodeHT = this.GetAllItemCode();} // 获取MES系统中所有的产品

			bool ifExist = false;	//默认不存在

			if(obj.GetType() == typeof(MO))
			{
				if(AllItemCodeHT.Contains(((MO)obj).ItemCode.Trim())){ ifExist = true; }
			}
			else if(obj.GetType() == typeof(MOBOM))
			{
				if(AllItemCodeHT.Contains(((MOBOM)obj).ItemCode.Trim())){ ifExist = true; } //MOBOM 暂时没有产品代码
				ifExist = true;
			}
			else if(obj.GetType() == typeof(SBOM))
			{
				if(AllItemCodeHT.Contains(((SBOM)obj).ItemCode.Trim())){ ifExist = true; }
			}

			return ifExist;
		}

		//检查是否初始状态的工单
		private bool CheckIfInitailMO(MO _MOobj)
		{
			if(InitailMOCodeHT == null){InitailMOCodeHT = this.GetInitialMOCode();} // 获取MES系统中初始状态的工单

			bool ifInitail = false;	//默认不是初始
			if(InitailMOCodeHT.Contains(((MO)_MOobj).MOCode.Trim())){ ifInitail = true; }

			return ifInitail;
		}

		#endregion 

		#region 导入产品bom前 先删除产品bom

		public bool DeleteSBOM(ArrayList importDatas)
		{
			#region 参数判断
			
			if(importDatas == null)return false;
			if(!(importDatas.Count>0))return false;
			//类型判断
			
			#endregion
			
			this.DataProvider.BeginTransaction();
			try
			{

				DeleteSBomByItemCode(importDatas);					//删除产品的标准bom
				
				this.DataProvider.CommitTransaction();
				
			}
			catch(Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				return false;
			}
			return true;
		}

		#endregion

		#endregion

		#region 获取mes系统中的所有产品代码

		public Hashtable GetAllItemCode()
		{
            string sql = string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)) + " from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");
			object[] allItems =  this.DataProvider.CustomQuery(typeof(Item), new SQLCondition(sql));

			Hashtable allItemsHT = new Hashtable();
			if(allItems != null && allItems.Length>0)
			{
				foreach(Item _item in allItems)
				{
					allItemsHT.Add(_item.ItemCode,_item.ItemCode);
				}
			}

			return allItemsHT;
		}

		#endregion

		#region 获取所有初始状态的工单

		public Hashtable GetInitialMOCode()
		{
			string sql = string.Format("select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO))+" from TBLMO where mostatus = '{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition(),MOManufactureStatus.MOSTATUS_INITIAL);
			object[] initialMOes =  this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(sql));

			Hashtable initialMOHT = new Hashtable();
			if(initialMOes != null && initialMOes.Length>0)
			{
				foreach(MO _mo in initialMOes)
				{
					initialMOHT.Add(_mo.MOCode,_mo.MOCode);
				}
			}

			return initialMOHT;
		}

		#endregion

		#region	删除工单对应的mobom

		//预处理对象
		private void DealMOObject(DomainObject obj)
		{
			if(obj.GetType() == typeof(MO))
			{
				//对工单 则删除工单对应的MOBOM
				this.DeleteMOBomByMoCode(((MO)obj).MOCode);
			}
		}

		//删除工单对应的MOBOM
		private void DeleteMOBomByMoCode(string mocode)
		{
			string sql = string.Format(" delete from tblmobom where mocode =  '{0}' ",mocode);
			this.DataProvider.CustomExecute(new SQLCondition(sql));
		}


		#endregion

		#region 删除产品对应的标准bom

		//删除产品对应的标准BOM
		private void DeleteSBomByItemCode(string itemCode)
		{
            string sql = string.Format(" delete from tblsbom where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode = '{0}' ", itemCode);
			this.DataProvider.CustomExecute(new SQLCondition(sql));
		}

		private void DeleteSBomByItemCode(ArrayList importDatas)
		{
			if(importDatas ==null || importDatas.Count==0)return;
			if(importDatas[0].GetType() != typeof(SBOM)) return;
			Hashtable itemCodeHT = new Hashtable();
			foreach(SBOM sbom in importDatas)
			{
				if(!itemCodeHT.Contains(sbom.ItemCode))
				{
					itemCodeHT.Add(sbom.ItemCode,sbom.ItemCode);
				}
			}
			if(DeleteSbomItemCodeHT == null)DeleteSbomItemCodeHT = new Hashtable();

			foreach(DictionaryEntry _entry in itemCodeHT)
			{
				string htKey = _entry.Key.ToString(); //物料代码作为Hashtalbe主键
				if(!DeleteSbomItemCodeHT.Contains(htKey))
				{
					this.DeleteSBomByItemCode(htKey);
					DeleteSbomItemCodeHT.Add(htKey,htKey);
				}
			}
			
		}

		//删除出错,回滚Hashtable中已删除记录
		private void RobackDeleteItemHT(ArrayList importDatas)
		{
			if(importDatas ==null || importDatas.Count==0)return;
			if(importDatas[0].GetType() != typeof(SBOM)) return;
			Hashtable itemCodeHT = new Hashtable();
			foreach(SBOM sbom in importDatas)
			{
				if(!itemCodeHT.Contains(sbom.ItemCode))
				{
					itemCodeHT.Add(sbom.ItemCode,sbom.ItemCode);
				}
			}
			if(DeleteSbomItemCodeHT == null)DeleteSbomItemCodeHT = new Hashtable();
			foreach(DictionaryEntry _entry in itemCodeHT)
			{
				string htKey = _entry.Key.ToString();
				if(DeleteSbomItemCodeHT.Contains(htKey))
				{
					DeleteSbomItemCodeHT.Remove(htKey);
				}
			}
		}

		#endregion

		#region 更新标准BOM首选料

		/// <summary>
		/// 更新标准BOM首选料
		/// </summary>
		/// <param name="updateDatas">sap夏新首选料BOM</param>
		/// <returns></returns>
		public bool UpdateSBom(object[] updateDatas)
		{
			#region 参数判断
			
			if(updateDatas == null)return false;
			if(!(updateDatas.Length>0))return false;
			//类型判断
			
			#endregion

			string messagePage =  string.Format("第{0}页",pageCountNum.ToString());
			
			this.DataProvider.BeginTransaction();
			try
			{
				foreach(DomainObject obj in updateDatas)
				{
					this.UpdateSBOMSourceItemCode(obj);
				}
				this.DataProvider.CommitTransaction();
				
			}
			catch(Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				if(importLogger !=null){importLogger.Write("导入同步标准bom首选料出现异常 " +ex.Message);}
				return false;
			}
			return true;
		}

		/// <summary>
		/// 更新标准BOM首选料
		/// </summary>
		/// <param name="obj">sap bom对象</param>
		private void UpdateSBOMSourceItemCode(DomainObject obj )
		{
			SAPBOM _sapEntity = (obj as SAPBOM);
			//SAPBom优先级栏位为"1" 表示首选料
			if(_sapEntity != null && _sapEntity.ZPRI.Trim() == "1" && _sapEntity.ALPGR != string.Empty)
			{
				this.UpdateSBOMSourceItemCode(_sapEntity.SBOMItemCode,_sapEntity.FItemCode,_sapEntity.SBOMItemCode,_sapEntity.ALPGR);
			}
		}
		

		/// <summary>
		/// 更新标准BOM首选料
		/// </summary>
		/// <param name="_sbsitemcode">首选料</param>
		/// <param name="_itemcode">产品代码</param>
		/// <param name="_sbitemcode">子阶料号</param>
		/// <param name="_alpgr">替代项目组</param>
		private void UpdateSBOMSourceItemCode(string _sbsitemcode,string _itemcode,string _sbitemcode ,string _alpgr)
		{
            string sql = string.Format(" update TBLSBOM set SBSITEMCODE = '{0}' where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ITEMCODE = '{1}'  and ALPGR = '{3}' "
										,_sbsitemcode,_itemcode,_sbitemcode,_alpgr);
			this.DataProvider.CustomExecute(new SQLCondition(sql));
		}

		#endregion

		#region 累加mobom 工单bom

		private void UpdateMOBom(MOBOM mobomObj)
		{
			try
			{
				string sql = string.Format("update tblmobom set MOBITEMQTY = MOBITEMQTY + {0} where 1=1  and MOCODE = '{1}' and ITEMCODE = '{2}' and MOBITEMCODE = '{3}'",mobomObj.MOBOMItemQty,mobomObj.MOCode,mobomObj.ItemCode,mobomObj.MOBOMItemCode);
				this.DataProvider.CustomExecute(new SQLCondition(sql));
			}
			catch
			{}
		}

		#endregion

		#region

		private string GetLog(DomainObject obj,JobActionResult importResult)
		{
			string dataType = string.Empty;		//数据类型
			string dataCode = string.Empty;		//数据Code
			if(obj.GetType() == typeof(MO))
			{
				dataType = "工单单号";
				dataCode = ((MO)obj).MOCode;
			}
			else if(obj.GetType() == typeof(MOBOM))
			{

				dataType = "工单BOM子阶料号";
				dataCode = ((MOBOM)obj).MOBOMItemCode;
			}
			else if(obj.GetType() == typeof(SBOM))
			{
				dataType = "BOM料号子阶料号";
				dataCode = ((SBOM)obj).SBOMItemCode;
			}

			string returnStr = string.Format("导入结果: {2} {0} 为 {1} 的数据",dataType,dataCode,importResult.ToString());
			return returnStr;
		}
		#endregion

		#region 获取工单类型参数实体
		private object[] GetMOType()
		{
			string parameterGroup = "MOTYPE";	//工单类型参数 
			string parameterCode = "";			//参数代码
			string condition = "";				//sqlCondition

			if ( parameterCode != null && parameterCode.Length != 0)
			{
				condition = string.Format("{0} and PARAMCODE like '{1}%'", condition, parameterCode);
			}

			if ( parameterGroup != null && parameterGroup.Length != 0)
			{
				condition = string.Format("{0} and PARAMGROUPCODE  = '{1}'", condition, parameterGroup);
			}

			return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(string.Format("select {0} from TBLSYSPARAM where 1=1 {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Parameter)), condition)));
		}

		#endregion
	}
}
