using System;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;

namespace BenQGuru.eMES.Common.DCT.ATop.GW28
{
	/// <summary>
	/// ActionCollectGood 的摘要说明。
	/// </summary>
	public class ActionCollectGood:BaseDCTAction
	{
		public ActionCollectGood()
		{
			
		}

		public override Messages Do(object act)
		{
			Messages msgs = new Messages();

            if (this.Status == ActionStatus.PrepareData || this.Status == ActionStatus.Working)
			{
				msgs =  Action(act);
			}
            else if (this.Status == ActionStatus.Pass)
			{
				msgs =  AftAction(act);
			}
			return msgs;
		}


		public override Messages Action(object act)
		{
			Messages msg = new Messages();

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

			if(msg.IsSuccess())
			{
				base.Action (act);

				
				//检查序列号
				ActionOnLineHelper _helper = new ActionOnLineHelper( Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider() ); 
				//msg.AddMessages(  _helper.GetIDInfo( args.RunningCard ) );
				msg =  _helper.GetIDInfo( args.RunningCard ) ;

				if( msg.IsSuccess() )
				{
					ProductInfo product= (ProductInfo)msg.GetData().Values[0];

					IAction dataCollectModule = new BenQGuru.eMES.DataCollect.Action.ActionFactory( Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider() ).CreateAction(ActionType.DataCollectAction_GOOD);

					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()).PersistBroker.OpenConnection();
					Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider().BeginTransaction();
					try
					{
						//msg.AddMessages(((IActionWithStatus)action).Execute( gotoMOArgs));	
						msg.AddMessages( ((IActionWithStatus)dataCollectModule).Execute(
							new ActionEventArgs(
							ActionType.DataCollectAction_GOOD,
							args.RunningCard,
							args.UserCode,
							args.ResourceCode,
							product)));

						if ( msg.IsSuccess() )
						{
							Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider().CommitTransaction();					
							msg.Add( new UserControl.Message(MessageType.Success,string.Format("$CS_GOODSUCCESS,$CS_Param_ID: {0}",args.RunningCard)) );
						
						}
						else
						{
							Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider().RollbackTransaction();
						}
					}
					catch(Exception ex)
					{
						Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider().RollbackTransaction();

						msg.Add(new UserControl.Message(ex));
					}
					finally
					{
						//Laws Lu,2005/10/19,新增	缓解性能问题
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()).PersistBroker.CloseConnection();
					}

				}
				
			}
				
			ActionRCard actRcard = new ActionRCard();
            NextAction = actRcard;

			return msg;
			
		}

		public override Messages AftAction(object act)
		{
			base.AftAction (act);

			return null;
		}

	}
}
