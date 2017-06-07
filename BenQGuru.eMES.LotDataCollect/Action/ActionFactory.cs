using System;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.LotDataCollect.Action
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	public class ActionFactory
	{
		private IDomainDataProvider _domainDataProvider = null;

//		public ActionFactory()
//		{	
//		}

		public ActionFactory(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public IDomainDataProvider DataProvider
		{
			get
			{
//				if (_domainDataProvider == null)
//				{
//					_domainDataProvider = DomainDataProviderManager.DomainDataProvider();
//				}

				return _domainDataProvider;
			}
		}

		public  IAction CreateAction(string actionType)
		{			
			switch (actionType)
			{
				case ActionType.DataCollectAction_GoMO:
					return new ActionGoToMO(this.DataProvider);
				case ActionType.DataCollectAction_GOOD :
					return new ActionGood(this.DataProvider);
				case ActionType.DataCollectAction_SMTGOOD :
					return new ActionGood(this.DataProvider);
				case ActionType.DataCollectAction_CollectINNO :
					return new ActionItem(this.DataProvider);
                case ActionType.DataCollectAction_NG:
                    return new ActionTS(this.DataProvider);
			}
			throw new Exception("$CS_Error_CreateAction_Failed");
		}
	}
}
