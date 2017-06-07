using System;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.DataCollect.Action
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
				case ActionType.DataCollectAction_NG :
					return new ActionTS(this.DataProvider);
				case ActionType.DataCollectAction_SMTGOOD :
					return new ActionGood(this.DataProvider);
				case ActionType.DataCollectAction_SMTNG :
					return new ActionTS(this.DataProvider);
				case ActionType.DataCollectAction_CollectINNO :
					return new ActionItem(this.DataProvider);
				case ActionType.DataCollectAction_CollectKeyParts :
					return new ActionItem(this.DataProvider);
				case ActionType.DataCollectAction_ECN :
					return new ActionECNTRY(this.DataProvider);
				case ActionType.DataCollectAction_TRY :
					return new ActionECNTRY(this.DataProvider);
				case ActionType.DataCollectAction_SoftINFO :
					return new ActionSoftWare(this.DataProvider);
				case ActionType.DataCollectAction_Split :
					return new ActionSplit(this.DataProvider);
				case ActionType.DataCollectAction_Reject :
					return new ActionRework(this.DataProvider);
				case ActionType.DataCollectAction_OutLineGood :
					return new ActionOutLine(this.DataProvider);
				//Laws Lu,2006/06/19 add support reject action when out of line
				case ActionType.DataCollectAction_OutLineReject :
					return new ActionOutLineReject(this.DataProvider);
				case ActionType.DataCollectAction_OutLineNG :
					return new ActionOutLine(this.DataProvider);
				case ActionType.DataCollectAction_LOT:
					return new ActionPack(this.DataProvider);
				case ActionType.DataCollectAction_OQCLotAddID:
					return new ActionOQCLotAddID(this.DataProvider);
				case ActionType.DataCollectAction_OQCLotRemoveID:
					return new ActionOQCLotRemoveID(this.DataProvider);
				case ActionType.DataCollectAction_OQCGood:
					return new ActionOQCGood(this.DataProvider);
				case ActionType.DataCollectAction_OQCNG:
					return new ActionOQCNG(this.DataProvider);
				case ActionType.DataCollectAction_OQCPass:
					return new ActionOQCPass(this.DataProvider);
				case ActionType.DataCollectAction_OQCReject:
					return new ActionOQCReject(this.DataProvider);
				case ActionType.DataCollectAction_TSConfirm:
					return new ActionTSConfirm(this.DataProvider);
				case ActionType.DataCollectAction_TSComplete:
					return new ActionTSComplete(this.DataProvider);
				case ActionType.DataCollectAction_OffMo:
					return new ActionOffMo(this.DataProvider);
				case ActionType.DataCollectAction_DropMaterial:
					return new ActionDropMaterial(this.DataProvider);
				case ActionType.DataCollectAction_BurnIn:
					return new ActionBurnIn(this.DataProvider);
				case ActionType.DataCollectAction_BurnOutGood:  //Modify by sandy on 20140528
					return new ActionBurnOut(this.DataProvider);
				case ActionType.DataCollectAction_OQCFuncTest:
					return new ActionOQCFuncTest(this.DataProvider);
                // Added By Hi1/Venus.Feng on 20080715 for Hisense Version
                case ActionType.DataCollectAction_Carton:
                    return new ActionCartonPack(this.DataProvider);
                case ActionType.DataCollectAction_TryNew:
                    return new ActionTry(this.DataProvider);
                case ActionType.DataCollectAction_Convert:
                    return new ActionConvertCard(this.DataProvider);
                case ActionType.DataCollectAction_Split_OutLine:
                    return new ActionSplitOutLine(this.DataProvider);
                case ActionType.DataCollectAction_BurnOutNG:  //Add by sandy on 20140528
                    return new ActionTS(this.DataProvider);

			}
			throw new Exception("$CS_Error_CreateAction_Failed");
		}
	}
}
