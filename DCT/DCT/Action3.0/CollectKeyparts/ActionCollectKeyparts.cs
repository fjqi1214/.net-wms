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
	/// ActionCollectKeyparts 的摘要说明。
	/// </summary>
	public class ActionCollectKeyparts:BaseDCTAction
	{
		public Hashtable keypartsHT = null;

		public ActionCollectKeyparts()
		{
			this.OutMesssage=new UserControl.Message(MessageType.Normal,"$DCT_PLEASE_INPUT_Keyparts");
		}

		public bool NeedCollectINNO = false;		// 是否需要同时做继承上料
		public string INNOCode = string.Empty;	// 继承上料号
		
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
			
			if (this.keypartsHT == null)
			{
				Messages msgCheck = CheckProduct(act, args.RunningCard);
				if (msgCheck.IsSuccess() == false)
				{
					/*
					msgCheck.Add(new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard"));
					keypartsHT = null;
					this.NeedCancel = true;
					this.ObjectState = null;
					base.PreAction (act);
					
					ActionCollectKeyparts actionCollectKeyparts = new ActionCollectKeyparts();
					actionCollectKeyparts.NeedCollectINNO = this.NeedCollectINNO;
					actionCollectKeyparts.INNOCode = this.INNOCode;
					this.NextAction = actionCollectKeyparts;
					this.ObjectState = null;
					this.keypartsHT = null;
					this.Status = ActionStatus.PrepareData;
					*/
					base.Action (act);
					ActionRCard actRcard1 = new ActionRCard();
					actRcard1.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
					this.NextAction = actRcard1;
					this.ObjectState = null;
					this.keypartsHT = null;
					this.Status = ActionStatus.PrepareData;
					
					return msgCheck;
				}
			}
			
			this.ObjectState = args;
			// Added end
			
			base.PreAction (act);

			Messages msg = new Messages();
			msg.Add(this.OutMesssage);
			
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
			/*	Removed by Icyer 2007/03/16
			UserControl.Messages msgProduct =  _helper.GetIDInfo( runningCard ) ;
			ProductInfo product= (ProductInfo)msgProduct.GetData().Values[0];
			if (product == null || product.LastSimulation == null)
			{
				msgProduct.ClearMessages();
				msgProduct.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
				return msgProduct;
			}
			*/
			
			// Added by Icyer 2007/03/16	如果归属工单，则做归属工单检查，否则做序列号途程检查
			UserControl.Messages msgProduct =  _helper.GetIDInfo( runningCard ) ;
			ProductInfo product= (ProductInfo)msgProduct.GetData().Values[0];
			MO moWillGo = null;
			ActionGoToMO actionGoMO = new ActionGoToMO(domainProvider);
			Messages msgMo = actionGoMO.GetItemCodeFromGoMoRCard(((IDCTClient)act).ResourceCode, runningCard);
			if (msgMo.IsSuccess() == false)		// 如果有错误，表示需要归属工单，但是解析工单或查询工单发生错误
			{
				return msgMo;
			}
			if (msgMo.GetData() != null)	// 需要归属工单，做归属工单检查
			{
				product.NowSimulation = new BenQGuru.eMES.Domain.DataCollect.Simulation();
				UserControl.Message msgMoData = msgMo.GetData();
				moWillGo = (MO)msgMoData.Values[0];
				ActionGoToMO goToMO=new ActionGoToMO(domainProvider);
				GoToMOActionEventArgs MOActionEventArgs=new GoToMOActionEventArgs(
					ActionType.DataCollectAction_GoMO,
					runningCard,
					((IDCTClient)act).LoginedUser,
					((IDCTClient)act).ResourceCode,
					product,
					moWillGo.MOCode);
				msgMo=goToMO.CheckIn(MOActionEventArgs);
				if (!MOActionEventArgs.PassCheck)
				{
					msgMo= _helper.CheckID(new CKeypartsActionEventArgs(
						ActionType.DataCollectAction_CollectKeyParts,
						runningCard,
						((IDCTClient)act).LoginedUser,
						((IDCTClient)act).ResourceCode,
						product,null,null));
				}
			}
			else	// 不需要归属工单，检查序列号途程
			{
				if (product == null || product.LastSimulation == null)
				{
					msgProduct.ClearMessages();
					msgProduct.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
					return msgProduct;
				}
				msgMo= _helper.CheckID(new CKeypartsActionEventArgs(
					ActionType.DataCollectAction_CollectKeyParts,
					runningCard,
					((IDCTClient)act).LoginedUser,
					((IDCTClient)act).ResourceCode,
					product,null,null));
			}
			if (msgMo.IsSuccess() == false)
			{
				return msgMo;
			}
			// Added end
			
			keypartsHT = new Hashtable();
			keypartsHT.Add("ProdcutInfo", msgProduct);
			keypartsHT.Add("MOWillGo", moWillGo);
			
			IDCTClient client = act as IDCTClient;
			OPBOMFacade opBOMFacade=new OPBOMFacade( domainProvider);
			object[] objBomDetail = null;
			Messages messages1 = new Messages();
			if (moWillGo == null)
			{
				// 检查途程
				messages1= _helper.CheckID(
					new CKeypartsActionEventArgs(
					ActionType.DataCollectAction_CollectKeyParts,
					product.LastSimulation.RunningCard,
					client.LoginedUser,
					client.ResourceCode,
					product,
					null,
					null));
			}
			if (messages1.IsSuccess() == true)
			{
				objBomDetail = opBOMFacade.GetOPBOMDetails(
					product.NowSimulation.MOCode,
					product.NowSimulation.RouteCode,
					product.NowSimulation.OPCode);
			}
			else
			{
				return messages1;
			}

			MOFacade moFacade = new MOFacade( domainProvider);
			//object mo = moFacade.GetMO( product.LastSimulation.MOCode );
			object mo = moWillGo;
			if (moWillGo == null)
			{
				mo = moFacade.GetMO( product.LastSimulation.MOCode );
			}

			// 查询OPBOM
			OPBomKeyparts opBomKeyparts=new OPBomKeyparts(objBomDetail,Convert.ToInt32( ((MO)mo).IDMergeRule),domainProvider);

			if (opBomKeyparts.Count==0)
			{
				msgProduct.Add(new Message(MessageType.Error, "$CS_NOOPBomInfo $CS_Param_MOCode="+product.NowSimulation.MOCode
					+" $CS_Param_RouteCode="+product.NowSimulation.RouteCode
					+" $CS_Param_OPCode ="+product.NowSimulation.OPCode));
				return msgProduct;
			}
			else
			{
				this.OutMesssage = new UserControl.Message(MessageType.Normal,"$DCT_PLEASE_INPUT_Keyparts " + (opBomKeyparts.GetbomKeypartCount() + 1).ToString() + "/" + opBomKeyparts.Count.ToString());
				keypartsHT.Add("KeypartsInfo",opBomKeyparts);
			}
			return msgProduct;
		}
		// Added end

		public override Messages Action(object act)
		{
			Messages msg = new Messages();
			ActionOnLineHelper _helper = null;

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

			string data = act.ToString().ToUpper().Trim();//Keyparts
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

			if( keypartsHT == null )
			{
				// 按照逻辑，走到这个分支应该是错误的
				string strFile = @"C:\DCT_EmptyMessage.txt";
				string strLine = DateTime.Now.ToString("yyyy-MM-dd HH:mi:ss") + "\t" + "ActionCollectKeyParts" + "\t" + "Missing Product In Action, RunningCard:" + args.RunningCard + ",Input:" + data;
				System.IO.StreamWriter writer = new System.IO.StreamWriter(strFile, true);
				writer.WriteLine(strLine);
				writer.Close();
				//
				if(msg.IsSuccess()
					&& (keypartsHT == null || keypartsHT.ContainsKey("ProdcutInfo") == false) )
				{
					//检查序列号
					_helper = new ActionOnLineHelper(domainProvider ); 
					msg =  _helper.GetIDInfo( args.RunningCard ) ;
					// Added by Icyer 2006/12/14
					// 检查序列号
					ProductInfo product= (ProductInfo)msg.GetData().Values[0];
					if (product == null || product.LastSimulation == null)
					{
						msg.ClearMessages();
						msg.Add(new Message(MessageType.Error, "$NoSimulation"));
					}
//					base.Action (act);
//					ActionCollectKeyparts actionCollectKeyparts = this;
//					this.NextAction = actionCollectKeyparts;
//					this.Status = ActionStatus.PrepareData;
//					return msg;
					// Added end
				}

				if( msg.IsSuccess() 
					&& (keypartsHT == null || keypartsHT.ContainsKey("KeypartsInfo") == false) )
				{
					try
					{
						keypartsHT = new Hashtable();
						keypartsHT.Add("ProdcutInfo",msg);

						ProductInfo product= (ProductInfo)msg.GetData().Values[0];

						/*	Removed by Icyer 2006/12/14
						 * 如果序列号已在本上料工序，则GetMORouteNextOperation会取下一个工序，导致找不到OPBOM
						 * 改变推导OPBOM的逻辑
						ItemFacade itemFacade = new ItemFacade(domainProvider);
						object op = itemFacade.GetMORouteNextOperation( 
							product.LastSimulation.MOCode,
							product.LastSimulation.RouteCode,
							product.LastSimulation.OPCode);

						OPBOMFacade opBOMFacade=new OPBOMFacade( domainProvider);
						object[] objBomDetail = opBOMFacade.GetOPBOMDetails(
							product.LastSimulation.MOCode,
							product.LastSimulation.RouteCode,
							(op as ItemRoute2OP).OPCode);
						*/
						// Added by Icyer 2006/12/14
						IDCTClient client = act as IDCTClient;
						OPBOMFacade opBOMFacade=new OPBOMFacade( domainProvider);
						object[] objBomDetail = null;
						DataCollect.ActionOnLineHelper onLine=new ActionOnLineHelper(domainProvider);
						Messages messages1= onLine.CheckID(
							new CKeypartsActionEventArgs(
							ActionType.DataCollectAction_CollectKeyParts,
							product.LastSimulation.RunningCard,
							client.LoginedUser,
							client.ResourceCode,
							product,
							null,
							null));
						if (messages1.IsSuccess() == true)
						{
							objBomDetail = opBOMFacade.GetOPBOMDetails(
								product.NowSimulation.MOCode,
								product.NowSimulation.RouteCode,
								product.NowSimulation.OPCode);
						}
						else
						{
							throw new Exception(messages1.OutPut());
						}
						// Added end

						MOFacade moFacade = new MOFacade( domainProvider);
						object mo = moFacade.GetMO( product.LastSimulation.MOCode );

						OPBomKeyparts opBomKeyparts=new OPBomKeyparts(objBomDetail,Convert.ToInt32( ((MO)mo).IDMergeRule),domainProvider);

						if (opBomKeyparts.Count==0)
						{
							/*	Removed by Icyer 2006/12/27 @ YHI
							msg.Add(new Message(MessageType.Error, "$CS_NOOPBomInfo $CS_Param_MOCode="+product.LastSimulation.MOCode
								+" $CS_Param_RouteCode="+product.LastSimulation.RouteCode
								+" $CS_Param_OPCode ="+product.LastSimulation.OPCode));
							throw new Exception(msg.OutPut());
							*/
							throw new Exception("$CS_NOOPBomInfo");
						}
						else
						{
							//opBomKeyparts.AddKeyparts(data, product.LastSimulation.MOCode);	// Removed by Icyer 2006/12/14

							keypartsHT.Add("KeypartsInfo",opBomKeyparts);
						}
					}
					catch (Exception ex)
					{
						/*	Removed by Icyer 2006/12/27 @ YHI
						keypartsHT = null;
						this.NeedCancel = true;
						throw ex;
						*/
						// Added by Icyer 2006/12/27 @ YHI
						base.Action (act);
						ActionCollectKeyparts actionCollectKeyparts = this;
						this.NextAction = actionCollectKeyparts;
						this.Status = ActionStatus.PrepareData;
						msg.ClearMessages();
						msg.Add(new UserControl.Message(MessageType.Error, ex.Message));
						msg.Add(new UserControl.Message(MessageType.Normal, "$DCT_PLEASE_INPUT_Keyparts"));
						return msg;
						// Added end
					}
				}
			}

			if( msg.IsSuccess() )
			{
				OPBomKeyparts opBomKeyparts = (OPBomKeyparts)keypartsHT["KeypartsInfo"];
				msg = keypartsHT["ProdcutInfo"] as Messages;
				MO moWillGo = (MO)keypartsHT["MOWillGo"];

				ProductInfo product = (ProductInfo)msg.GetData().Values[0];

				Messages msgResult = new Messages();
				if( opBomKeyparts.Count > opBomKeyparts.GetbomKeypartCount() )
				{
					try
					{
						//opBomKeyparts.AddKeyparts(data, product.LastSimulation.MOCode);
						if (moWillGo == null)
							msgResult.AddMessages(opBomKeyparts.AddKeyparts(data, product.LastSimulation.MOCode));
						else
							msgResult.AddMessages(opBomKeyparts.AddKeyparts(data, moWillGo.MOCode));
					}
					catch (Exception ex)
					{
						msgResult.Add(new UserControl.Message(MessageType.Error, ex.Message));
					}

					keypartsHT["KeypartsInfo"] = opBomKeyparts;
				}

				if(msgResult.IsSuccess() == false || opBomKeyparts.Count > opBomKeyparts.GetbomKeypartCount() )
				{
					base.Action (act);
					ActionCollectKeyparts actionCollectKeyparts = this;
					this.NextAction = actionCollectKeyparts;

					this.Status = ActionStatus.PrepareData;

					this.OutMesssage = new UserControl.Message(MessageType.Normal,"$DCT_PLEASE_INPUT_Keyparts " + (opBomKeyparts.GetbomKeypartCount() + 1).ToString() + "/" + opBomKeyparts.Count.ToString());
					if (msgResult.IsSuccess() == false)
					{
						//msgResult.Add(new UserControl.Message(MessageType.Normal, "$DCT_PLEASE_INPUT_Keyparts"));
						return msgResult;
					}
					else
						return msg;
				}

				//((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)domainProvider).PersistBroker.OpenConnection();
				domainProvider.BeginTransaction();
				try
				{
					BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel=new BenQGuru.eMES.BaseSetting.BaseModelFacade(domainProvider);
					Resource resource= (Resource)dataModel.GetResource(args.ResourceCode);

					_helper = new ActionOnLineHelper( domainProvider ); 
					DataCollectFacade dataCollect = new DataCollect.DataCollectFacade( domainProvider);
					ActionCheckStatus actionCheckStatus = new ActionCheckStatus();
					actionCheckStatus.ProductInfo = product;
					actionCheckStatus.ProductInfo.Resource = resource;
					ExtendSimulation lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;

					BenQGuru.eMES.Material.WarehouseFacade wfacade = null;
					if(System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
					{
						wfacade = new WarehouseFacade(domainProvider );
					}
	
					IDCTClient client = act as IDCTClient;

					msg.AddMessages(_helper.ActionWithTransaction(
						new CKeypartsActionEventArgs( 
						ActionType.DataCollectAction_CollectKeyParts,
						product.NowSimulation.RunningCard,
						client.LoginedUser,
						client.ResourceCode,
						product,
						opBomKeyparts,
						wfacade), actionCheckStatus));

					// Added by Icyer 2006/12/28 @ YHI	采集集成上料
					bool bLoadedINNO = false;
					if (this.NeedCollectINNO == true && msg.IsSuccess() == true)
					{
						string strRCard = product.NowSimulation.RunningCard;
						UserControl.Messages msgProduct =  _helper.GetIDInfo( strRCard ) ;
						product= (ProductInfo)msgProduct.GetData().Values[0];
						CINNOActionEventArgs argsInno = new CINNOActionEventArgs( 
							ActionType.DataCollectAction_CollectINNO,
							strRCard,
							client.LoginedUser,
							client.ResourceCode,
							product,
							INNOCode,
							wfacade
							);
						IAction action = new BenQGuru.eMES.DataCollect.Action.ActionFactory(domainProvider).CreateAction(ActionType.DataCollectAction_CollectINNO);
						msg.AddMessages(action.Execute(argsInno));
						if (msg.IsSuccess() == true)
						{
							bLoadedINNO = true;
                            msg.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_INNO_CollectSuccess[" + this.INNOCode + "] $CS_Keyparts_CollectSuccess")));
						}
					}
					// Added end
					if ( msg.IsSuccess() )
					{
						if(System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
						{
							if(wfacade != null)
								wfacade.ExecCacheSQL();
						}

						domainProvider.CommitTransaction();					
						if (bLoadedINNO == false)
						{
                            msg.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_Keyparts_CollectSuccess")));						
						}
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
				base.Action (act);
				ActionRCard actRcard1 = new ActionRCard();
				actRcard1.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
				this.NextAction = actRcard1;
				this.ObjectState = null;
				this.keypartsHT = null;
				this.Status = ActionStatus.PrepareData;
				return msg;
			}

			base.Action (act);

            //Web.Helper.ActionType acttype = new Web.Helper.ActionType();
//			if(acttype.Items.Contains(args.RunningCard))
//			{
//				(act as IDCTClient).CachedAction = null;
//			}	

			ActionRCard actRcard = new ActionRCard();
			actRcard.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
			this.NextAction = actRcard;
			// Added by Icyer 2006/12/14
			this.ObjectState = null;
			this.keypartsHT = null;
			this.Status = ActionStatus.PrepareData;
			// Added end

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
		/// <param name="data">Keyparts</param>
		/// <returns></returns>
		public Messages CheckData(string data,Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
		{
			Messages msg = new Messages();

			return msg;
		}
		#endregion
	}
}
