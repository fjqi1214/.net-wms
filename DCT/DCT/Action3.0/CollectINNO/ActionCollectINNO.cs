using System;
using System.Collections;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Common.DCT.Action
{
	/// <summary>
	/// ActionCollectIDMerge 的摘要说明。
	/// </summary>
	public class ActionCollectINNO:BaseDCTAction
	{
		public ActionCollectINNO()
		{
			//this.OutMesssage = new UserControl.Message(MessageType.Normal,"$DCT_Please_Input_SN_OR_Directive");
			this.OutMesssage = new UserControl.Message(MessageType.Normal,"$CS_Please_Input_RunningCard");
		}

		public string INNOCode = string.Empty;	// 集成上料号
		
		public override Messages PreAction(object act)
		{
			// Added by Icyer 2006/12/14
			// 输入集成上料号
			if (INNOCode == string.Empty)
			{
				Messages msgChk = CheckINNO(act);
				if (msgChk.IsSuccess() == false)
				{
					msgChk.Add(new UserControl.Message(MessageType.Normal,"$CS_CMPleaseInputINNOinEdt"));
					return msgChk;
				}
				INNOCode = act.ToString().ToUpper();
			}
			// Added end
			
			base.PreAction (act);

			Messages msg = new Messages();
			msg.Add(this.OutMesssage);
			
			return  msg;
		}

		private Messages CheckINNO(object act)
		{
			Messages msg = new Messages();
			BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;
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
			string strInno = act.ToString().ToUpper();
			MaterialFacade material = new MaterialFacade(domainProvider);
			object[] objsInno = material.GetLastMINNOs(strInno);
			if (objsInno == null)
			{
				msg.Add(new Message(MessageType.Error, "$CS_INNO_NOT_EXIST"));
				return msg;
			}
			return msg;
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

			string data = act.ToString().ToUpper().Trim();	//产品序列号
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

			if( msg.IsSuccess() )
			{
				msg = this.DoDataCollectAction(domainProvider, (IDCTClient)act, data);
			}

			base.Action (act);

			this.ObjectState = null;
			this.Status = ActionStatus.PrepareData;
			this.NextAction = this;

			return msg;
		}
		// 
		private Messages DoDataCollectAction(Common.DomainDataProvider.SQLDomainDataProvider domainProvider, IDCTClient client, string runningCard)
		{
			Messages messages = new Messages();

			ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider);
			messages = _helper.GetIDInfo(runningCard);
			ProductInfo product = (ProductInfo)messages.GetData().Values[0];
			/*
			if (product == null || product.LastSimulation == null)
			{
				messages.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
				return messages;
			}
			*/
			
			CINNOActionEventArgs args = new CINNOActionEventArgs( 
				ActionType.DataCollectAction_CollectINNO,
				runningCard,
				client.LoginedUser,
				client.ResourceCode,
				product,
				INNOCode,
				null
				);

			IAction action = new BenQGuru.eMES.DataCollect.Action.ActionFactory(domainProvider).CreateAction(ActionType.DataCollectAction_CollectINNO);
			
			domainProvider.BeginTransaction();
			try
			{
				messages.AddMessages(action.Execute(args));	

				if ( messages.IsSuccess() )
				{
					domainProvider.CommitTransaction();
                    messages.Add(new UserControl.Message(MessageType.Success, "$CS_INNO_CollectSuccess"));
				}
				else
				{
					domainProvider.RollbackTransaction();
				}

				return messages;
			}
			catch(Exception ex)
			{
				domainProvider.RollbackTransaction();
				messages.Add(new UserControl.Message(ex));
				return messages;
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)domainProvider).PersistBroker.CloseConnection();
			}
		}

		public override Messages AftAction(object act)
		{
			base.AftAction (act);

			return null;
		}
		
	}
}
