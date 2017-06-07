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
	public class ActionCollectIDMerge:BaseDCTAction
	{
		public ActionCollectIDMerge()
		{
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$CS_Please_Input_SN_For_Merge");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$CS_Please_Input_SN_For_Merge");
		}

		private int IDMergeRule = 0;	// 分板比例
		private string mergeIdType = string.Empty;	// 序号转换还是分板
		private bool isSameMO = false;	// 是否相同工单
		private decimal existIMEISeq = 0;
		private bool updateSimulation = false;
		private Hashtable mergeList = null;	// "ProdcutInfo"中存放包含ProductInfo的Messages; "MergeIDList"中存放转换后序列号的ArrayList
		
		private void ResetData()
		{
			IDMergeRule = 0;
			mergeIdType = string.Empty;
			isSameMO = false;
			existIMEISeq = 0;
			updateSimulation = true;
			mergeList = null;
		}
		
		public override Messages PreAction(object act)
		{
			// Added by Icyer 2006/12/14
			// 输入产品序列号是检查
			DataCollect.Action.ActionEventArgs args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
			if (this.ObjectState != null)
			{
				args = (DataCollect.Action.ActionEventArgs)this.ObjectState;
			}
			else
			{
				args.RunningCard = act.ToString().ToUpper();
			}
			
			if (this.mergeList == null)
			{
				Messages msgCheck = CheckProduct(act, args.RunningCard);
				if (msgCheck.IsSuccess() == false)
				{
					ResetData();

                    ProcessBeforeReturn(this.Status, msgCheck);
					return msgCheck;
				}
			}
			
			this.ObjectState = args;
			// Added end
			
			base.PreAction (act);

			int iCurrent = 1;
			if (this.mergeList != null && this.mergeList["MergeIdList"] != null)
				iCurrent = ((ArrayList)this.mergeList["MergeIdList"]).Count + 1;
			this.OutMesssage = new UserControl.Message(MessageType.Normal,"$CS_Please_Input_Merge_ID " + iCurrent.ToString() + "/" + this.IDMergeRule.ToString());
			Messages msg = new Messages();
			msg.Add(this.OutMesssage);

            ProcessBeforeReturn(this.Status, msg);
			return  msg;
		}

		// Added by Icyer 2006/12/15
		// 检查产品
		private Messages CheckProduct(object act, string runningCard)
		{
			// 查询产品信息
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
			
			ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider ); 
			UserControl.Messages msgProduct =  _helper.GetIDInfo( runningCard ) ;
			ProductInfo product= (ProductInfo)msgProduct.GetData().Values[0];
			if (product == null || product.LastSimulation == null)
			{
				msgProduct.ClearMessages();
				msgProduct.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
				return msgProduct;
			}
			mergeList = new Hashtable();
			mergeList.Add("ProdcutInfo", msgProduct);

			// 转换比例
			//this.IDMergeRule = Convert.ToInt32(product.LastSimulation.IDMergeRule);	// 转换比例从工序中读
			
			IDCTClient client = act as IDCTClient;
			OPBOMFacade opBOMFacade=new OPBOMFacade( domainProvider);
			// 检查途程
			Messages messages1= _helper.CheckID(
				new CKeypartsActionEventArgs(
				ActionType.DataCollectAction_Split,
				product.LastSimulation.RunningCard,
				client.LoginedUser,
				client.ResourceCode,
				product,
				null,
				null));
			if (messages1.IsSuccess() == true)
			{
				object op = new ItemFacade(domainProvider).GetItemRoute2Operation( 
					product.NowSimulation.ItemCode,
					product.NowSimulation.RouteCode,
					product.NowSimulation.OPCode );
				if (op == null)
				{
					messages1.Add(new UserControl.Message(MessageType.Error, "$Error_CS_Current_OP_Not_Exist"));
					return messages1;
				}
				if ( ((ItemRoute2OP)op).OPControl[(int) BenQGuru.eMES.BaseSetting.OperationList.IDTranslation] != '1' )
				{
					messages1.Add(new UserControl.Message(MessageType.Error,"$CS_OP_Not_SplitOP"));		//当前工序不是序号转换工序
					return messages1;
				}
				// 转换比例
				this.IDMergeRule = 1;
				if ( ((ItemRoute2OP)op).IDMergeType == IDMergeType.IDMERGETYPE_ROUTER )
				{
					this.IDMergeRule = (int)((ItemRoute2OP)op).IDMergeRule;
				}
				// 序号转换类型
				mergeIdType = ((ItemRoute2OP)op).IDMergeType;
				ArrayList listId = new ArrayList();
				mergeList.Add("MergeIdList", listId);
			}
			else
			{
				return messages1;
			}

			return msgProduct;
		}
		// Added end

		public override Messages Action(object act)
		{
			Messages msg = new Messages();

			BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;

			if(act == null)
			{
				return msg;
			}

			if (this.mergeList == null)
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

			string data = act.ToString().ToUpper().Trim();	//转换后序列号
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

			if( mergeList == null )
			{
				msg = this.CheckProduct(act, args.RunningCard);
			}

			msg = CheckData(data, domainProvider);
			if (msg.IsSuccess() == false)
			{
                ProcessBeforeReturn(this.Status, msg);
				return msg;
			}

			if( msg.IsSuccess() )
			{
				msg = mergeList["ProdcutInfo"] as Messages;

				ProductInfo product = (ProductInfo)msg.GetData().Values[0];

				ArrayList listId = (ArrayList)mergeList["MergeIdList"];
				if( this.IDMergeRule > listId.Count )
				{
					listId.Add(data);

					mergeList["MergeIdList"] = listId;
				}

				if( this.IDMergeRule > listId.Count )
				{
					this.OutMesssage = new UserControl.Message(MessageType.Normal,"$CS_Please_Input_Merge_ID " +  (listId.Count + 1).ToString() + "/" + this.IDMergeRule.ToString());

                    msg.Add(this.OutMesssage);
                    ProcessBeforeReturn(this.Status, msg);
					return msg;
				}

				msg = this.DoDataCollectAction(domainProvider, (IDCTClient)act, this.isSameMO, Convert.ToInt32(this.existIMEISeq), this.updateSimulation);
			}

            if (msg.IsSuccess())
            {
                base.Action(act);
                this.ObjectState = null;
                ResetData();
            }

            ProcessBeforeReturn(this.Status, msg);
			return msg;
		}
		// 
		private Messages DoDataCollectAction(Common.DomainDataProvider.SQLDomainDataProvider domainProvider, IDCTClient client, bool IsSameMO, int ExistIMEISeq, bool UpdateSimulation)
		{
			Messages messages = new Messages();

			ProductInfo product = (ProductInfo)(((UserControl.Messages)mergeList["ProdcutInfo"]).GetData().Values[0]);
			ArrayList listId = (ArrayList)mergeList["MergeIdList"];
			SplitIDActionEventArgs args = new SplitIDActionEventArgs(
				ActionType.DataCollectAction_Split, 
				product.LastSimulation.RunningCard, 
				client.LoginedUser,
				client.ResourceCode,
				product, 
				(object[])listId.ToArray(),
				this.mergeIdType,
				IsSameMO,
				ExistIMEISeq,
				UpdateSimulation);

			IAction action = new BenQGuru.eMES.DataCollect.Action.ActionFactory(domainProvider).CreateAction(ActionType.DataCollectAction_Split);
			
			domainProvider.BeginTransaction();
			try
			{
				messages.AddMessages(action.Execute(args));	

				if ( messages.IsSuccess() )
				{
					domainProvider.CommitTransaction();
                    messages.Add(new UserControl.Message(MessageType.Success, "$CS_SplitID_CollectSuccess"));
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

		/// <returns></returns>
		public Messages CheckData(string data, Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
		{
			Messages msg = new Messages();
			isSameMO = false;
			existIMEISeq = 0;
			updateSimulation = false;
			if (mergeList.ContainsKey("MergeIdList") == true)
			{
				ArrayList list = (ArrayList)mergeList["MergeIdList"];
				if (list.Contains(data) == true)
				{
					msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_Merge_ID_Exist"));
					return msg;
				}
			}
			ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider);
			msg = _helper.GetIDInfo(data.ToUpper());
			if ( ((ProductInfo)msg.GetData().Values[0]).LastSimulation != null )
			{
				if( string.Compare( this.mergeIdType, IDMergeType.IDMERGETYPE_IDMERGE, true) != 0 )
				{
					msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_ID_Already_Exist"));//序列号已存在
					return msg;
				}

				ProductInfo newProduct = (ProductInfo)msg.GetData().Values[0];
				ProductInfo oriProduct = (ProductInfo)(((UserControl.Messages)this.mergeList["ProdcutInfo"]).GetData().Values[0]);
				/* 是序列号转换工序 
					 * 转换前的rcard 和 转换后的rcard  不相同
					 * 不同， check IMEI 重复使用
					 * */
				if( string.Compare( oriProduct.LastSimulation.RunningCard, newProduct.LastSimulation.RunningCard, true) != 0 )  
				{
					string bMoCode = oriProduct.LastSimulation.MOCode;
					string aMoCode = newProduct.LastSimulation.MOCode;
					
					/* 判断这个IMEI号是否报废或者拆解 */
					bool isSpliteOrScrape = CheckIMEISpliteOrScrape(
						domainProvider,
						newProduct.LastSimulation.RunningCard,
						newProduct.LastSimulation.RunningCardSequence,
						aMoCode) ;
					if( !isSpliteOrScrape )
					{
						/* rcard 完工，工单未关 */
						if(newProduct.LastSimulation.IsComplete != "1"
							&& newProduct.LastSimulation.ProductStatus != ProductStatus.OffMo)
						{
							msg.Add(new UserControl.Message(MessageType.Error,"$Error_CS_ID_Already_Exist"));//序列号已存在
							return msg;
						}
					}
					
					/* 归属同一张工单 */
					if( string.Compare( bMoCode,aMoCode,true )==0 )
					{
						isSameMO = true ;
					}
					else
					{
						/* 归属不同工单 */
						isSameMO = false ;
					}
					existIMEISeq = newProduct.LastSimulation.RunningCardSequence ;
					updateSimulation = true;
					
				}
				else /* rcard == tcard */
				{
					isSameMO = true ;
					existIMEISeq = newProduct.LastSimulation.RunningCardSequence ;
				}
			}
			// 检查是否在工单序列号范围中
			if (mergeList != null && mergeList.ContainsKey("ProdcutInfo") == true)
			{
				if (System.Configuration.ConfigurationSettings.AppSettings["CheckRCardRange"] == "1")
				{
					msg.AddMessages(CheckSNRange(data, domainProvider));
				}
			}
			return msg;
		}
		public Messages CheckSNRange(string data, Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
		{
			Messages msg = new Messages();
			MORunningCardFacade rcardFacade = new MORunningCardFacade(domainProvider);
			Messages msgProduct = mergeList["ProdcutInfo"] as Messages;
			ProductInfo product = (ProductInfo)msgProduct.GetData().Values[0];
			if (rcardFacade.CheckRunningCardInRange(product.LastSimulation.MOCode, MORunningCardType.AfterConvert, data) == false)
			{
				msg.Add(new Message(MessageType.Error, "$DCT_GOMO_SN_Not_In_Range"));
			}
			return msg;
		}

		// 检查序列号是否拆解或报废
		private bool CheckIMEISpliteOrScrape(Common.DomainDataProvider.SQLDomainDataProvider domainProvider, string rcard, decimal rcardseq, string mocode )
		{
			string sql = string.Format(" select count(*) from tblts where rcard='{0}' and rcardseq={1} and mocode='{2}' and tsstatus in ('{3}','{4}')",
				rcard,rcardseq,mocode,TSStatus.TSStatus_Scrap,TSStatus.TSStatus_Split);
			int count = domainProvider.GetCount( new Common.Domain.SQLCondition(sql) );
			if( count>0 )
			{
				return true;
			}
			return false;
		}
		
	}
}
