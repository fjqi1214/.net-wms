using System;
using System.Data;
using System.Collections;
using BenQGuru.eMES.Common.Domain ;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.DataImportConsole
{
	/// <summary>
	/// DataImportHelper 的摘要说明。
	/// </summary>
	public class DataImportHelper
	{
		public DataImportHelper()
		{
		}

		private ImportSchema importSchema = new ImportSchema();
		#region DataProvider
		BenQGuru.eMES.Common.Domain.IDomainDataProvider _erpDataProvider;
		BenQGuru.eMES.Common.Domain.IDomainDataProvider _mesDataProvider;

		private IDomainDataProvider ERPProvider
		{
			get
			{
				if (_erpDataProvider == null)
				{
					_erpDataProvider = DomainDataProviderManager.DomainDataProvider(DBName.ERP);
				}
				return _erpDataProvider;
			}	
		}

		private IDomainDataProvider MESProvider
		{
			get
			{
				if (_mesDataProvider == null)
				{
					_mesDataProvider = DomainDataProviderManager.DomainDataProvider(DBName.MES);
				}
				return _mesDataProvider;
			}	
		}

		private void OpenProvider()
		{
			if(_erpDataProvider != null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_erpDataProvider).PersistBroker.OpenConnection();

			if(_mesDataProvider != null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_mesDataProvider).PersistBroker.OpenConnection();
		}

		private void ReleaseProvider()
		{
			if(_erpDataProvider != null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_erpDataProvider).PersistBroker.CloseConnection();

			if(_mesDataProvider != null)
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_mesDataProvider).PersistBroker.CloseConnection();
		}

		#endregion

		public void Import()
		{
			string logfilename = string.Format("ERPImp-{0}{1}",
				FormatHelper.TODateInt(DateTime.Now).ToString(),
				FormatHelper.TOTimeInt(DateTime.Now).ToString().PadLeft(6,'0'));
			Log log = new Log( logfilename );

			log.Info("::开始导入");
			log.Info(string.Format("::Factory:{0}",importSchema.Factory));
			log.Info(string.Format("::时间:{0}", DateTime.Now));
			/* 导入顺序 */
			if( importSchema.ImportSequence!=null )
			{
				try
				{
					this.ERPProvider.BeginTransaction();
					this.MESProvider.BeginTransaction();

					for( long i=0; i<importSchema.ImportSequence.Length; i++ )
					{				

						/* step1:从ERP的数据库获取相应的数据 */
						string importType = importSchema.ImportSequence[i];
						log.Info("::获取数据:"+importType);
						object[] objs = this.ERPProvider.CustomQuery( 
							importSchema.ImportType(importType) , 
							new SQLCondition( importSchema.BuildQuerySQL( importType ) )); 

						if( objs!=null )
						{
							log.Info("::"+importType+":"+objs.Length);
							#region
							try
							{
								ImportByType(objs, importType, log);
								log.Info("::导入"+importType+"结束");
							}
							catch(Exception er)
							{
								log.Info("::错误信息:"+er.Message);
							}
								
							#endregion
						}
						else
						{
							log.Info("::没有相应数据:"+importType);
							continue;
						}
					}
					this.ERPProvider.CommitTransaction();
					this.MESProvider.CommitTransaction();
				}
				catch(Exception ex )
				{
					this.ERPProvider.RollbackTransaction();
					this.MESProvider.RollbackTransaction();
					log.Info("::错误信息:"+ex.Message);
				}
				finally
				{
					ReleaseProvider();
				}
			}
		}

		
		private object CheckRpeat(object obj,string type)
		{
			if( obj==null )
			{
				return null;
			}
			
			switch(type)
			{
				case "Model2Item":
					BenQGuru.eMES.Domain.MOModel.Model2Item model2item = obj as BenQGuru.eMES.Domain.MOModel.Model2Item;
					ModelFacade modelfacade = new ModelFacade( this.MESProvider );
					return modelfacade.GetModel2ItemByItemCode(model2item.ItemCode);	
				case "Item":
					BenQGuru.eMES.Domain.MOModel.Item item = obj as BenQGuru.eMES.Domain.MOModel.Item;
					ItemFacade itemfacade = new ItemFacade( this.MESProvider );
                    return itemfacade.GetItem(item.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);				
				case "MO":
					BenQGuru.eMES.Domain.MOModel.MO mo= obj as BenQGuru.eMES.Domain.MOModel.MO;
					MOFacade mofacade = new MOFacade( this.MESProvider ); 
					return mofacade.GetMO(mo.MOCode);
				default:
					return null;
			}

		}


		private void ImportByType( object[] objs, string type, Log log )
		{
			switch(type)
			{
				case "Model2Item":
					ImportModel2Item(objs, type, log);
					break;
				case "Item":
					ImportItem(objs, type, log);
					break;
				case "SBOM":
					ImportSBOM(objs, type, log);
					break;
				case "MO":
					ImportMO(objs, type, log);
					break;
				case "MOBOM":
					ImportMOBOM(objs, type, log);
					break;
				case "ERPBOM":
					ImportERPBOM(objs, type, log);
					break;
				default:
					break;
			}
		}

		private void ImportModel2Item( object[] objs, string type, Log log )
		{
			for(int j=0; j<objs.Length; j++)
			{
				object impObj = importSchema.FillImportObject( objs[j], type);

				if( CheckRpeat(impObj, type)==null )
				{
					/* step2:导入MES的数据库 */
					this.MESProvider.Insert(impObj);
				}
			}
		}

		private void ImportItem( object[] objs, string type, Log log )
		{
			for(int j=0; j<objs.Length; j++)
			{
				object impObj = importSchema.FillImportObject( objs[j], type);

				if( CheckRpeat(impObj, type)==null)
				{
					/* step2:导入MES的数据库 */
					this.MESProvider.Insert(impObj);

					/* step3:删除ERP临时表中的数据 */
					this.ERPProvider.Delete(objs[j]);
				}
				else/* 重复的，只删除, 不导入 */
				{
					/* step3:删除ERP临时表中的数据 */
					this.ERPProvider.Delete(objs[j]);
				}
			}
		}

		private void ImportSBOM( object[] objs, string type, Log log )
		{
			SBOMFacade sbomFacade = new SBOMFacade( this.MESProvider );
			ArrayList items = new ArrayList();
			ArrayList sboms = new ArrayList();
			for(int j=0; j<objs.Length; j++)
			{
				object impObj = importSchema.FillImportObject( objs[j], type);
				sboms.Add( impObj );
				items.Add( (impObj as SBOM ).ItemCode );
			}

			sbomFacade.DeleteSBOMWithoutTransaction( (string[])items.ToArray( typeof(string) ) );
			sbomFacade.AddSBOMsWithoutTransaction( (SBOM[])sboms.ToArray(typeof(SBOM)) );

			for(int j=0; j<objs.Length; j++)
			{
				this.ERPProvider.Delete(objs[j]);
			}
		}

		private void ImportMO( object[] objs, string type, Log log )
		{
			for(int j=0; j<objs.Length; j++)
			{
				object impObj = importSchema.FillImportObject( objs[j], type);
				object mo = CheckRpeat(impObj, type);
				if( mo ==null)
				{
					/* step2:导入MES的数据库 */
					this.MESProvider.Insert(impObj);

					/* step3:删除ERP临时表中的数据 */
					this.ERPProvider.Delete(objs[j]);
				}
				else
				{
					if( string.Compare( (mo as MO).MOStatus, MOManufactureStatus.MOSTATUS_CLOSE, true)!=0 
						&& (impObj as MO).MOPlanQty > (mo as MO).MOInputQty   )
					{
						(mo as MO).MOPlanQty  = (impObj as MO).MOPlanQty;
						/* step2:导入MES的数据库 */
						this.MESProvider.Update(mo);
						log.Info(string.Format("::更新工单{0}的计划数量为{1}", (mo as MO).MOCode, (mo as MO).MOPlanQty));

						/* step3:删除ERP临时表中的数据 */
						this.ERPProvider.Delete(objs[j]);
					}
					else
					{
						/* step3:删除ERP临时表中的数据 */
						this.ERPProvider.Delete(objs[j]);
					}
										
				}
			}
		}

		private void ImportMOBOM( object[] objs, string type, Log log )
		{
			MOFacade mofacade = new MOFacade( this.MESProvider );
			Hashtable ht =new Hashtable();
			object[] impobjs = new object[objs.Length];
			for(int j=0; j<objs.Length; j++)
			{
				object impObj = importSchema.FillImportObject( objs[j], type);
				object moobj = mofacade.GetMO( (impObj as MOBOM).MOCode );
				impobjs.SetValue(impObj, j);
				if(!ht.ContainsKey( (impObj as MOBOM).MOCode ))
				{
					(impObj as MOBOM).ItemCode = (moobj as MO).ItemCode;
					ht.Add(  (impObj as MOBOM).MOCode,(moobj as MO).ItemCode );
				}
				else
				{
					(impObj as MOBOM).ItemCode = ht[(impObj as MOBOM).MOCode].ToString();
				}
			}

			foreach( DictionaryEntry dic in ht )
			{
				object[] delobjs = mofacade.GetMOBOM( dic.Key.ToString() );
				if( delobjs!=null )
				{
					for( int k=0; k<delobjs.Length; k++ )
					{
						this.MESProvider.Delete(delobjs[k]);
					}
				}
			}
							
			for(int j=0; j<objs.Length; j++)
			{
				this.MESProvider.Insert(impobjs[j]);

				this.ERPProvider.Delete(objs[j]);
			}
		}

		private void ImportERPBOM( object[] objs, string type, Log log )
		{
			MOFacade mofacade = new MOFacade( this.MESProvider );
			Hashtable ht =new Hashtable();
			object[] impobjs = new object[objs.Length];
			for(int j=0; j<objs.Length; j++)
			{
				object impObj = importSchema.FillImportObject( objs[j], type);
				//object moobj = mofacade.GetMO( (impObj as ERPBOM).MOCODE );
				impobjs.SetValue(impObj, j);
//				if(!ht.ContainsKey( (impObj as ERPBOM).MOCode ))
//				{
//					(impObj as ERPBOM).ItemCode = (moobj as MO).ItemCode;
//					ht.Add(  (impObj as ERPBOM).MOCode,(moobj as MO).ItemCode );
//				}
//				else
//				{
//					(impObj as ERPBOM).ItemCode = ht[(impObj as ERPBOM).MOCode].ToString();
//				}
			}
//
//			foreach( DictionaryEntry dic in ht )
//			{
//				object[] delobjs = mofacade.GetERPBOM( dic.Key.ToString() );
//				if( delobjs!=null )
//				{
//					for( int k=0; k<delobjs.Length; k++ )
//					{
//						this.MESProvider.Delete(delobjs[k]);
//					}
//				}
//			}
//							
			for(int j=0; j<impobjs.Length; j++)
			{
				try
				{
					this.MESProvider.Insert(impobjs[j]);
				}
				catch(Exception ex)
				{
					if(ex.Message != "$ERROR_DATA_ALREADY_EXIST")
					{
						throw ex;		
					}
					
					Common.Log.Error("\t" + (impobjs[j] as ERPBOM).SEQUENCE.ToString() + "\t" +  ex.Message);
				}
				//this.ERPProvider.Delete(objs[j]);
			}
		}
	}
}
