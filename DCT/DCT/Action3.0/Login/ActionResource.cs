using UserControl;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Common.DCT.Action
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public class ActionResource:BaseDCTAction	
	{
		public ActionResource()
		{
            this.NeedAuthorized = false;
            this.OutMesssage 
				= 	new Message(MessageType.Normal,"$DCT_PLEASE_INPUT_RESOURCE");

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

			DataCollect.Action.ActionEventArgs args;
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

			msg = CheckData(data,domainProvider);

			if(msg.IsSuccess())
			{
				
				//关键逻辑,将ActionEventArgs对象传递到下一个Action

				args.ResourceCode = data;

				//ActionPassword acPwd = new ActionPassword();

				//			acPwd.ObjectState = ObjectState;
				//			this.NextAction = acPwd;

				object[] objUserGroup = args.UserGroup;
				bool bIsAdmin = false;
				if(objUserGroup != null)
				{
					foreach(object o in objUserGroup)
					{
						if(((UserGroup)o).UserGroupType == "ADMIN")
						{
							bIsAdmin = true;
							break;
						}
					}
				}

				if (!bIsAdmin)
				{
					if ( !(new Security.SecurityFacade(domainProvider))
						.CheckResourceRight(args.UserCode, args.ResourceCode))
					{
						msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_No_Resource_Right"));
					}
				}

				
				ObjectState = args;

				msg.Add(new Message(MessageType.Data,"",new object[]{args}));

                // Added By Scott Gu for Hisense Version : Add Org ID
                object obj = new BaseModelFacade(domainProvider).GetResource(args.ResourceCode);
                object org = (new BaseModelFacade(domainProvider)).GetOrg(((Resource)obj).OrganizationID);
                if (org != null)
                {
                    GlobalVariables.CurrentOrganizations.Clear();
                    GlobalVariables.CurrentOrganizations.Add((Organization)org);
                }
                else
                {
                    msg.Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_NoOrganizationOfTheResource"));
                }
            }

			if(msg.IsSuccess())
			{
				base.Action (act);
                msg.Add(new UserControl.Message(UserControl.MessageType.Success, "$DCT_WELCOME"));
				
				if(act is IDCTClient)
				{
					IDCTClient client = act as IDCTClient;
					client.Authorized = true;
					client.LoginedUser = args.UserCode;
					client.LoginedPassword = args.Passwod;
					client.ResourceCode  = args.ResourceCode;
				}

				ActionRCard actRcard = new ActionRCard();
				actRcard.OutMesssage = new Message(MessageType.Normal, "$DCT_PLEASE_ACTION");
				actRcard.NeedInputActionCommand = true;
				
				//actRcard.LastAction = this;
				this.NextAction = actRcard;
			}

			return msg;
				
		}

		public override Messages AftAction(object act)
		{
			base.AftAction (act);

			Messages msg = new Messages();


			return null;
		}

		#region Check Data
		public Messages CheckData(string data,Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
		{
			Messages msg = new Messages();
			if ( data == string.Empty )
			{
				msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_Resource_Empty"));
			}
			else
			{

				object obj = new BaseModelFacade(domainProvider).GetResource(data);

				if ( obj == null )
				{
					msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$Error_Resource_Not_Exist"));
				}
			}

			return msg;
		}
		#endregion

	}
}
