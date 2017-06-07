using System;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Common.DCT.Action
{
	/// <summary>
	/// ActionCollectGood 的摘要说明。
	/// </summary>
	public class ActionCollectOutlineGood:BaseDCTAction
	{
		public ActionCollectOutlineGood()
		{
			
		}

		public override Messages Do(object act)
		{
			Messages msgs = new Messages();

			if(this.Status == ActionStatus.PrepareData || this.Status == ActionStatus.Working)
			{
				msgs =  Action(act);
			}
			else if(this.Status == ActionStatus.Pass)
			{
				msgs =  AftAction(act);
			}
			return msgs;
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
				args.RunningCard = act.ToString().ToUpper().Trim();
			}
			else
			{
				args = ObjectState as DataCollect.Action.ActionEventArgs;
			}

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


			if(msg.IsSuccess())
			{
				
				//检查序列号
				ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider); 
				
				msg =  _helper.GetIDInfo( args.RunningCard ) ;

				if( msg.IsSuccess() )
				{
					ProductInfo product= (ProductInfo)msg.GetData().Values[0];

					if(product.LastSimulation == null)
					{
						msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$NoSimulation"));
					}

					object[] oplist = null;
					if( msg.IsSuccess() )
					{
						oplist = GetOutLineOP( domainProvider, (act as IDCTClient).ResourceCode );

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
					if( msg.IsSuccess() )
					{
						//((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()).PersistBroker.OpenConnection();
						domainProvider.BeginTransaction();
						try
						{
							IDCTClient client = act as IDCTClient;

							IAction dataCollectModule = new BenQGuru.eMES.DataCollect.Action.ActionFactory(domainProvider).CreateAction(ActionType.DataCollectAction_OutLineGood);

							msg.AddMessages( (dataCollectModule).Execute(new OutLineActionEventArgs(
								ActionType.DataCollectAction_OutLineGood, 
								args.RunningCard,
								client.LoginedUser,
								client.ResourceCode,
								product,
								opcode )));

							if ( msg.IsSuccess() )
							{
								domainProvider.CommitTransaction();
                                msg.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_GOODSUCCESS")));
						
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
		
			base.Action (act);
//
//			Web.Helper.ActionType acttype = new Web.Helper.ActionType();
//			if(acttype.Items.Contains(args.RunningCard))
//			{
//				(act as IDCTClient).CachedAction = null;
//			}	
//		

			ActionRCard actRcard = new ActionRCard();
			this.NextAction = actRcard;

			

			return msg;
			
		}

		public override Messages AftAction(object act)
		{
			base.AftAction (act);

			return null;
		}

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
