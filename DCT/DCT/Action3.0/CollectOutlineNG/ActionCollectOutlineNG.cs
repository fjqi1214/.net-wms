using System;
using System.Collections;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;

namespace BenQGuru.eMES.Common.DCT.Action
{
	/// <summary>
	/// ActionCollectOutlineNG 的摘要说明。
	/// </summary>
	public class ActionCollectOutlineNG:BaseDCTAction
	{
		private Hashtable errorCodesHT = null;
		public ActionCollectOutlineNG()
		{
			this.OutMesssage=new UserControl.Message(MessageType.Normal,"$DCT_PLEASE_INPUT_ErrorCode");
		}

		public override Messages PreAction(object act)
		{
			base.PreAction (act);

			DataCollect.Action.ActionEventArgs args;
			if(this.ObjectState == null)
			{
				args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
				args.RunningCard = act.ToString().ToUpper().Trim();
				this.ObjectState = args;
			}
			

			Messages msg = new Messages();
			msg.Add(this.OutMesssage);
			
			return  msg;
		}

		public override Messages Action(object act)
		{
			Messages msg = new Messages();
			BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;

			if(act == null)
			{
				return msg;
			}

			DataCollect.Action.ActionEventArgs args;
			if(ObjectState == null)
			{
				args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
			}
			else
			{
				args = ObjectState as DataCollect.Action.ActionEventArgs;
			}

			string data = act.ToString().ToUpper().Trim();//Errorcode
			//Laws Lu,2006/06/03	添加	获取已有连接
			if((act as IDCTClient).DBConnection != null)
			{
				domainProvider = (act as IDCTClient).DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
			}
			else
			{
				domainProvider = Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider() 
					as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
				(act as IDCTClient).DBConnection = domainProvider;
			}


			if( string.Compare(data,BaseDCTDriver.FINERROR,true) != 0 )//结束的标志
			{
				msg = CheckData(data,domainProvider);

				if(msg.IsSuccess())
				{
					if( errorCodesHT==null )
					{
						errorCodesHT = new Hashtable();
					}
				
					if( !errorCodesHT.ContainsKey(data) )
					{
						errorCodesHT.Add( data, data );
					}

					base.Action (act);
					ActionCollectOutlineNG actionCollectOutlineNG = this;

					actionCollectOutlineNG.IsTopAction = false;
					this.NextAction = actionCollectOutlineNG;
					this.Status = ActionStatus.PrepareData;
					//Laws Lu,2006/07/11 write back error code
                    msg.Add(new UserControl.Message(MessageType.Success, data));

					return msg;
				}
			}

			if(msg.IsSuccess())
			{
				//检查序列号
				
				ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider); 
				
				msg =  _helper.GetIDInfo( args.RunningCard ) ;

				if( msg.IsSuccess() )
				{
					ProductInfo product= (ProductInfo)msg.GetData().Values[0]; 
					
					TSModelFacade tsmodelFacade = new TSModelFacade( domainProvider );

					if(errorCodesHT == null)
					{
						msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$ErrorCode_Not_Exist"));
					}

					if(msg.IsSuccess())
					{
						string[] errorcs = new string[errorCodesHT.Count];
						int i=0;
						foreach( DictionaryEntry dic in errorCodesHT )
						{
							errorcs[i] = dic.Key.ToString();
							i++;
						}
						//Laws Lu,2006/06/22 modify fix the bug that system alert object not set an instance when the product.LastSimulation is null
						if(product.LastSimulation == null)
						{
							msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$NoSimulation"));
						}
						object[] errorcodes = null;
						if(msg.IsSuccess())
						{
							errorcodes = tsmodelFacade.QueryECG2ECByECAndModelCode(errorcs, product.LastSimulation.ModelCode);

							if( errorcodes==null || errorcodes.Length==0 )
							{
								msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$ErrorCode_Not_BelongTo_ModelCode"));
							}
						}

						object[] oplist = null;
						if(msg.IsSuccess())
						{
							oplist = GetOutLineOP( domainProvider,  (act as IDCTClient).ResourceCode );

							if(oplist==null || oplist.Length == 0)
							{
								msg.Add(new UserControl.Message(MessageType.Error ,"$CSError_Res_Cannot_Collect_OUtlineGood"));
							}
						}

						string opcode = string.Empty;
						if( msg.IsSuccess() )
						{
							opcode = (oplist[0] as Operation).OPCode;
							if (CheckOutlineOPInRoute(domainProvider, product.LastSimulation.RouteCode, opcode))
							{
								msg.Add(new UserControl.Message(MessageType.Error ,"$CS_OutLineOP_In_ThisRoute"));
							}
						}

						if( msg.IsSuccess() )
						{
							if( IsLastOP(domainProvider, product.LastSimulation.MOCode, product.LastSimulation.RouteCode, product.LastSimulation.OPCode ) )
							{
								msg.Add(new UserControl.Message(MessageType.Error ,"$CS_Op_IsLast_OutLineOP_Cannot_Collect"));
							}
						}
					
						if(msg.IsSuccess())
						{
							IAction dataCollectModule 
								= new BenQGuru.eMES.DataCollect.Action.ActionFactory( domainProvider ).CreateAction(ActionType.DataCollectAction_OutLineNG);

							domainProvider.BeginTransaction();
							try
							{
								IDCTClient client = act as IDCTClient;

								msg.AddMessages((dataCollectModule).Execute(new OutLineActionEventArgs(
									ActionType.DataCollectAction_OutLineNG, 
									args.RunningCard,
									client.LoginedUser,
									client.ResourceCode,
									product,
									opcode,
									errorcodes,
									"")));

								if (msg.IsSuccess())
								{
									domainProvider.CommitTransaction();
                                    msg.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_NGSUCCESS")));
								
									//msg.Add( new UserControl.Message(MessageType.Succes ,string.Format("$CS_NGSUCCESS,$CS_Param_ID: {0}", args.RunningCard)));
								}
								else
								{
									domainProvider.RollbackTransaction();
								}
							}
							catch(Exception ex)
							{
								domainProvider.RollbackTransaction();

								msg.Add(new UserControl.Message(ex));
							}
							finally
							{
							
								((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)domainProvider).PersistBroker.CloseConnection();
							}
						}

					}
				}
			}

			if((act as IDCTClient).CachedAction is ActionCollectOutlineNG)
			{
				if(this.Status == BenQGuru.eMES.Common.DCT.Core.ActionStatus.Working)
				{
					this.ObjectState = null;
					if(this.errorCodesHT != null)
					{
						this.errorCodesHT.Clear();
					}
				}

				this.Status = BenQGuru.eMES.Common.DCT.Core.ActionStatus.PrepareData;
			

				(act as IDCTClient).CachedAction = this;
			}

			base.Action (act);

			ActionRCard actRcard = new ActionRCard();
			this.NextAction = actRcard;

		
			return msg;
		}


		public override Messages AftAction(object act)
		{
			base.AftAction (act);

			return null;
		}

		#region Check Data
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data">ErrorCode</param>
		/// <returns></returns>
		public Messages CheckData(string data,Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
		{
			Messages msg = new Messages();
			if ( data == string.Empty )
			{
				msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$DCT_PLEASE_INPUT_ErrorCode"));
			}
			else
			{

				TSModelFacade tsmodelFacade = new TSModelFacade(domainProvider);
				object obj = tsmodelFacade.GetErrorCode(data);

				if ( obj == null )
				{
					msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$ErrorCode_Not_Exist"));
				}
			}

			return msg;
		}
		#endregion

		private bool CheckOutlineOPInRoute(DomainDataProvider.SQLDomainDataProvider dataProvider, string routeCode, string opCode )
		{
			BaseModelFacade bsmodel = new BaseModelFacade(dataProvider);
			return bsmodel.IsOperationInRoute(routeCode, opCode);
		}

		private bool IsLastOP(DomainDataProvider.SQLDomainDataProvider dataProvider, string moCode,string routeCode,string opCode)
		{
			if (routeCode==string.Empty)
				return false;
			DataCollectFacade dataCollectFacade = new DataCollectFacade(dataProvider);

			return dataCollectFacade.OPIsMORouteLastOP(moCode,routeCode,opCode);
		}

		private object[] GetOutLineOP(DomainDataProvider.SQLDomainDataProvider dataProvider, string resCode)
		{
			//初始化线外工序下拉框。
			BaseModelFacade bsmodel = new BaseModelFacade(dataProvider);
			object[] oplist = bsmodel.GetAllOutLineOperationsByResource( resCode );
			return oplist;
		}

	}
}
