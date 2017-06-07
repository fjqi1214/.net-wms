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
	/// ActionMO 的摘要说明。
	/// </summary>
	public class ActionGotoMO:BaseDCTAction
	{
		public ActionGotoMO()
		{
            base.OutMesssage 
				= 	new Message(MessageType.Normal,"$DCT_PLEASE_INPUT_MO");
		}

		public override Messages PreAction(object act)
		{
			base.PreAction (act);

			Messages msg = new Messages();
            msg.Add(this.OutMesssage);
			
			return  msg;
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

			string data = act.ToString().ToUpper().Trim();//工单代码

			msg = CheckData(data);
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

					GoToMOActionEventArgs gotoMOArgs = new GoToMOActionEventArgs( 
						ActionType.DataCollectAction_GoMO, 
						args.RunningCard, 
						args.UserCode,
						args.ResourceCode,
						product, 
						data);

					IAction action = new BenQGuru.eMES.DataCollect.Action.ActionFactory(Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()).CreateAction(ActionType.DataCollectAction_GoMO);

					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()).PersistBroker.OpenConnection();
					Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider().BeginTransaction();
					try
					{
						msg = ((IActionWithStatus)action).Execute( gotoMOArgs) ;

						if ( msg.IsSuccess() )
						{
							Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider().CommitTransaction();					
							msg.Add( new UserControl.Message(MessageType.Success,string.Format("$CS_GOMO_CollectSuccess")) );
						
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

		#region Check Data
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data">工单代码</param>
		/// <returns></returns>
		public Messages CheckData(string data)
		{
			Messages msg = new Messages();
			if ( data == string.Empty )
			{
				msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_MO_Empty"));
			}
			else
			{

				object obj = new MOFacade(Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()).GetMO(data);

				if ( obj == null )
				{
					msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_MO_Not_Exist"));
				}
				else
				{

					if ( (((MO)obj).MOStatus != Web.Helper.MOManufactureStatus.MOSTATUS_RELEASE) && 
						(((MO)obj).MOStatus !=  Web.Helper.MOManufactureStatus.MOSTATUS_OPEN) )
					{
						msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_CS_MO_Should_be_Release_or_Open2"));
					}
				}
			}

			return msg;
		}
		#endregion
	}
}
