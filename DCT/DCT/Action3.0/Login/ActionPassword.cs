using System;

using UserControl;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.Action
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public class ActionPassword:BaseDCTAction	
	{
		public ActionPassword()
		{
            this.NeedAuthorized = false;
			this.OutMesssage 
				= 	new Message(MessageType.Normal,"$DCT_PLEASE_INPUT_PASSWORD");

			//base.FlowDirect = FlowDirect.WaitingOutPut;
		}

		public override Messages PreAction(object act)
		{
			base.PreAction (act);

			Messages msg = new Messages();
			msg.Add(this.OutMesssage);

			//base.FlowDirect = FlowDirect.WaitingInput;
			
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
			

			DataCollect.Action.ActionEventArgs args = null;
			if(ObjectState == null)
			{
				args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
			}
			else
			{
				args = ObjectState as DataCollect.Action.ActionEventArgs;
			}

			string data = act.ToString().ToUpper().Trim();

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

			msg = CheckData(data);

			if(msg.IsSuccess())
			{
				//关键逻辑,将ActionEventArgs对象传递到下一个Action

				args.Passwod = data;
				
				object[] objUserGroup = null;
                User user = null;
                try
                {                    
                       user = new Security.SecurityFacade(domainProvider)
                        .LoginCheck(args.UserCode, args.Passwod, out objUserGroup);
                }
                catch(Exception ex)
                {
                    if (ex.Message == "$Error_User_Not_Exist")
                    {
                        base.Action(act);
                        ActionUser acUser = new ActionUser();
                        this.NextAction = acUser;
                    }
                    else
                    {
                        throw ex;
                    }

                }
				
				args.UserGroup = objUserGroup;
				ObjectState = args;

				// 用户名不存在
				if ( user == null )
				{
					msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_User_Not_Exist"));

                    base.Action(act);
                    ActionUser acUser = new ActionUser();
                    this.NextAction = acUser;
				}

				msg.Add(new Message(MessageType.Data,"",new object[]{args}));
			}

			if(msg.IsSuccess())
			{
				base.Action (act);

				ActionResource acRes = new ActionResource();

				//acRes.LastAction = this;

				acRes.ObjectState = ObjectState;
				this.NextAction = acRes;
			}
			else
			{
				base.Action (act);

				ActionUser acUser = new ActionUser();

				//acRes.LastAction = this;

				//acUser.ObjectState = ObjectState;
				this.NextAction = acUser;
				
			}

			

			return msg;
				
		}

		public override Messages AftAction(object act)
		{
			base.AftAction (act);

			return null;
		}

		#region Check Data
		public Messages CheckData(string data)
		{
			Messages msg = new Messages();
			if ( data == string.Empty )
			{
				msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_Password_Empty"));
			}

			return msg;
		}
		#endregion
	}
}
