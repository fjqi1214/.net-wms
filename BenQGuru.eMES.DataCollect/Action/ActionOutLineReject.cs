using System;

using UserControl;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.TSModel;

namespace BenQGuru.eMES.DataCollect.Action
{
	/// <summary>
	/// 归属工单采集
	/// </summary>
	public class ActionOutLineReject:IAction
	{
		
		private IDomainDataProvider _domainDataProvider = null;

//		public ActionOutLine()
//		{	
//		}

		public ActionOutLineReject(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public IDomainDataProvider DataProvider
		{
			get
			{
				if (_domainDataProvider == null)
				{
					_domainDataProvider = DomainDataProviderManager.DomainDataProvider();
				}

				return _domainDataProvider;
			}
		}

		#region 已经注释
		/// <summary>
		/// 检查出TS是否允许，并填写部分SIMULATION
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="actionType"></param>
		/// <param name="resourceCode"></param>
		/// <param name="userCode"></param>
		/// <param name="product"></param>
		/// <returns></returns>
//		public Messages CheckID(ActionEventArgs actionEventArgs)
//		{
//			
//			Messages messages=new Messages();
//			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"CheckID");
//			dataCollectDebug.WhenFunctionIn(messages);
//			try
//			{
//				if (actionEventArgs.ActionType ==ActionType.DataCollectAction_OutLineNG)
//				{
//					//线外不良品，到TS检查
//					ActionTS ts= new ActionTS(this.DataProvider);
//					messages.AddMessages( ts.CheckID(actionEventArgs));
//				}
//				else
//				{
//					MOFacade moFacade=new MOFacade(this.DataProvider);				 
//					BaseModelFacade dataModel=new BaseModelFacade(this.DataProvider);
//					DataCollectFacade dataCollectFacade=new DataCollectFacade(this.DataProvider);
//									
//					#region 检查工单
//					MO mo=(MO)moFacade.GetMO(actionEventArgs.ProductInfo.LastSimulation.MOCode);
//					//工单状态检查
//					if (!dataCollectFacade.CheckMO(mo))
//					{
//						throw new Exception("$MOStatus_Error"+mo.MOStatus);
//					}		
//					#endregion
//					#region 检查ID状态				
//					if (actionEventArgs.ProductInfo.LastSimulation.ProductStatus !=ProductStatus.GOOD)
//					{
//						throw new Exception("$CS_ProductStatusError $CS_Param_ProductStatus="+ actionEventArgs.ProductInfo.LastSimulation.ProductStatus);
//					}
//					#endregion
//					#region 检查途程
//					//根据FROM ROUTE 和OP推途程
//					//检查重复采集
//					ItemRoute2OP op=new ItemRoute2OP();
//					dataCollectDebug.DebugPoint(messages,"下一站采集");
//					op=dataCollectFacade.GetMORouteNextOP(actionEventArgs.ProductInfo.LastSimulation.MOCode,
//						                            actionEventArgs.ProductInfo.LastSimulation.FromRoute,
//						                            actionEventArgs.ProductInfo.LastSimulation.FromOP);
//					if (op==null)
//						throw new Exception("$CS_Route_Failed_GetNotNextOP");
//					if (dataModel.GetOperation2Resource(op.OPCode, actionEventArgs.ResourceCode)==null)
//					{
//						throw new Exception("$CS_Route_Failed $CS_Param_OPCode"+op.OPCode);						
//					}
//					#endregion
//					#region 检查ACTION
//					if (dataCollectFacade.CheckAction(actionEventArgs.ProductInfo, op, actionEventArgs.ActionType))
//					{
//					}				
//					#endregion
//					#region 填写新SIMULATION
//					messages.AddMessages( dataCollectFacade.WriteSimulation(actionEventArgs.RunningCard ,actionEventArgs.ActionType ,actionEventArgs.ResourceCode ,actionEventArgs.UserCode ,actionEventArgs.ProductInfo));
//					if (messages.IsSuccess())
//					{
//						//修改
//						actionEventArgs.ProductInfo.NowSimulation.FromOP=			ActionOnLineHelper.StringNull;
//						actionEventArgs.ProductInfo.NowSimulation.FromRoute=		ActionOnLineHelper.StringNull;
//						actionEventArgs.ProductInfo.NowSimulation.RouteCode =	op.RouteCode;
//						actionEventArgs.ProductInfo.NowSimulation.OPCode =		op.OPCode;				
//						actionEventArgs.ProductInfo.NowSimulation.ResourceCode=	 actionEventArgs.ResourceCode;					
//						actionEventArgs.ProductInfo.NowSimulation.ActionList =";"+actionEventArgs.ActionType+";";				
//					}
//					#endregion
//				}
//			}
//			catch (Exception e)
//			{
//				messages.Add(new Message(e));
//			}
//			dataCollectDebug.WhenFunctionOut(messages);
//			return messages;
//		}
		public Messages CheckIDIn(ActionEventArgs actionEventArgs)
		{
			Messages messages=new Messages();
			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"GetIDInfo");
			dataCollectDebug.WhenFunctionIn(messages);
			try
			{
				if (((OutLineActionEventArgs)actionEventArgs).OPCode == null)
				{
					throw new Exception("$CS_Sys_OutLine_LostOPParam");
				}

				if (((OutLineActionEventArgs)actionEventArgs).OPCode.Length < 1)
				{
					throw new Exception("$CS_Sys_OutLine_LostOPParamLengthError");
				}

				ActionOnLineHelper helper =new ActionOnLineHelper(this.DataProvider);
				messages.AddMessages(helper.CheckIDOutline(actionEventArgs));
				#region code not to use
				//string opCode= ((OutLineActionEventArgs)actionEventArgs).OPCode;
				//DataCollectFacade dataCollectFacade=new DataCollectFacade(this.DataProvider);
				//dataCollectFacade.CheckIDOutline(actionEventArgs.RunningCard, actionEventArgs.ActionType, actionEventArgs.ResourceCode, ((OutLineActionEventArgs)actionEventArgs).OPCode, actionEventArgs.UserCode, actionEventArgs.ProductInfo);
//				MOFacade moFacade=new MOFacade(this.DataProvider);
//				
//				//填写SIMULATION 检查工单、ID、途程、操作
//				//messages.AddMessages( dataCollect.CheckID(iD,actionType,resourceCode,userCode,product));
//				
//				#region 检查工单
//				MO mo=(MO)moFacade.GetMO(actionEventArgs.ProductInfo.LastSimulation.MOCode);
//
//				//工单状态检查
//				if (!dataCollectFacade.CheckMO(mo))
//				{
//					throw new Exception("$CS_MOStatus_Error $CS_Param_MOStatus="+mo.MOStatus);
//				}		
//				#endregion
//				#region 检查ID状态
//				if (actionEventArgs.ProductInfo.LastSimulation.ProductStatus !=ProductStatus.GOOD)
//				{
//					throw new Exception("$CS_ProductStatusError $CS_Param_ProductStatus="+actionEventArgs.ProductInfo.LastSimulation.ProductStatus);
//				}
//				#endregion
//				#region Action检查
//				dataCollectFacade.CheckRepeatCollect(actionEventArgs.ProductInfo.LastSimulation.ActionList,actionEventArgs.ActionType);
//				#endregion
//				#region 检查途程
//
//				#endregion
//				#region 填写新SIMULATION
//				messages.AddMessages( dataCollectFacade.WriteSimulation(actionEventArgs.RunningCard,actionEventArgs.ActionType,actionEventArgs.ResourceCode,actionEventArgs.UserCode,actionEventArgs.ProductInfo));
//				//线外站已经做过良品采集
//				if (actionEventArgs.ProductInfo.LastSimulation.LastAction ==ActionType.DataCollectAction_OutLineGood)
//				{
//					dataCollectDebug.DebugPoint(messages,"线外站已经做过良品采集");
//					actionEventArgs.ProductInfo.NowSimulation.ActionList=actionEventArgs.ProductInfo.LastSimulation.ActionList +actionEventArgs.ActionType+";";					
//					actionEventArgs.ProductInfo.NowSimulation.FromOP =actionEventArgs.ProductInfo.LastSimulation.FromOP;
//					actionEventArgs.ProductInfo.NowSimulation.FromRoute =actionEventArgs.ProductInfo.LastSimulation.FromRoute;
//				}
//				else
//				{
//					dataCollectDebug.DebugPoint(messages,"进入线外站");
//					actionEventArgs.ProductInfo.NowSimulation.ActionList=";"+actionEventArgs.ActionType+";";
//					actionEventArgs.ProductInfo.NowSimulation.FromOP =actionEventArgs.ProductInfo.LastSimulation.OPCode;
//					actionEventArgs.ProductInfo.NowSimulation.FromRoute =actionEventArgs.ProductInfo.LastSimulation.RouteCode;
//				}
//				actionEventArgs.ProductInfo.NowSimulation.MOCode=actionEventArgs.ProductInfo.LastSimulation.MOCode;
//				actionEventArgs.ProductInfo.NowSimulation.ItemCode=actionEventArgs.ProductInfo.LastSimulation.ItemCode;				;
//				actionEventArgs.ProductInfo.NowSimulation.ModelCode=actionEventArgs.ProductInfo.LastSimulation.ModelCode;
//				actionEventArgs.ProductInfo.NowSimulation.IDMergeRule =actionEventArgs.ProductInfo.LastSimulation.IDMergeRule;
//
//				actionEventArgs.ProductInfo.NowSimulation.RunningCard=actionEventArgs.RunningCard;
//				actionEventArgs.ProductInfo.NowSimulation.RunningCardSequence =actionEventArgs.ProductInfo.LastSimulation.RunningCardSequence+1;
//				actionEventArgs.ProductInfo.NowSimulation.TranslateCard=actionEventArgs.ProductInfo.LastSimulation.TranslateCard;
//				actionEventArgs.ProductInfo.NowSimulation.TranslateCardSequence=actionEventArgs.ProductInfo.LastSimulation.TranslateCardSequence;
//				actionEventArgs.ProductInfo.NowSimulation.SourceCard=actionEventArgs.ProductInfo.LastSimulation.SourceCard;
//				actionEventArgs.ProductInfo.NowSimulation.SourceCardSequence=actionEventArgs.ProductInfo.LastSimulation.SourceCardSequence;
//
//				actionEventArgs.ProductInfo.NowSimulation.RouteCode =	ActionOnLineHelper.StringNull;
//				actionEventArgs.ProductInfo.NowSimulation.OPCode =opCode;	
//				actionEventArgs.ProductInfo.NowSimulation.ResourceCode=actionEventArgs.ResourceCode;	
//
//				
//				actionEventArgs.ProductInfo.NowSimulation.LastAction =actionEventArgs.ActionType;
//								
//				actionEventArgs.ProductInfo.NowSimulation.IsComplete =ProductComplete.NoComplete;	
//			
//				actionEventArgs.ProductInfo.NowSimulation.CartonCode =actionEventArgs.ProductInfo.LastSimulation.CartonCode;
//				actionEventArgs.ProductInfo.NowSimulation.LOTNO =actionEventArgs.ProductInfo.LastSimulation.LOTNO;
//				actionEventArgs.ProductInfo.NowSimulation.PalletCode =actionEventArgs.ProductInfo.LastSimulation.PalletCode;
//				if (actionEventArgs.ActionType ==ActionType.DataCollectAction_OutLineNG)
//				{					
//					actionEventArgs.ProductInfo.NowSimulation.NGTimes =actionEventArgs.ProductInfo.LastSimulation.NGTimes+1;
//					dataCollectDebug.DebugPoint(messages,"NG+1="+actionEventArgs.ProductInfo.NowSimulation.NGTimes.ToString());
//					actionEventArgs.ProductInfo.NowSimulation.ProductStatus =ProductStatus.NG;
//				}
//				else
//				{
//					dataCollectDebug.DebugPoint(messages,"GOOD");
//					actionEventArgs.ProductInfo.NowSimulation.ProductStatus=ProductStatus.GOOD;
//				}
//				#endregion
				#endregion
			}
			catch (Exception e)
			{
				messages.Add(new Message(e));
			}
			dataCollectDebug.WhenFunctionOut(messages);
			return messages;
		}
		#endregion
		/// <summary>
		/// 不良品采集
		/// </summary>
		/// <param name="domainDataProvider"></param>
		/// <param name="iD"></param>
		/// <param name="actionType"></param>
		/// <param name="resourceCode"></param>
		/// <param name="userCode"></param>
		/// <param name="product"></param>
		/// <param name="datas1"></param>
		/// <param name="datas2"></param>
		/// <returns></returns>
		public Messages Execute(ActionEventArgs actionEventArgs)
		{				
			Messages messages=new Messages();
			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"Collect");
			dataCollectDebug.WhenFunctionIn(messages);
			try
			{
				if (((TSActionEventArgs)actionEventArgs).OPCode == string.Empty)
				{
					throw new Exception("$CS_Sys_OutLine_LostOPParam");
				}

				string opCode = ((TSActionEventArgs)actionEventArgs).OPCode;
				
				

				messages.AddMessages(this.CheckIDIn(actionEventArgs));
				if (messages.IsSuccess())
				{
					ActionOnLineHelper dataCollect=new ActionOnLineHelper(this.DataProvider);															
					//补充SIMULATION 不良信息
					if (actionEventArgs.ActionType == ActionType.DataCollectAction_OutLineReject)
					{
						actionEventArgs.ProductInfo.NowSimulation.ProductStatus =	ProductStatus.Reject;
						actionEventArgs.ProductInfo.NowSimulation.NGTimes = actionEventArgs.ProductInfo.NowSimulation.NGTimes + 1;

						//2006/01/10,新增	OQCNG后将产品脱离批
						actionEventArgs.ProductInfo.NowSimulation.LOTNO = String.Empty;
					}

					messages.AddMessages( dataCollect.Execute(actionEventArgs));
					if (messages.IsSuccess())
					{
						//填写Reject报表 TODO
						ReworkFacade rework = new ReworkFacade(this.DataProvider);
						Reject reject = rework.CreateNewReject();
						reject.ItemCode =		actionEventArgs.ProductInfo.NowSimulationReport.ItemCode;
						reject.MaintainDate =	actionEventArgs.ProductInfo.NowSimulationReport.MaintainDate;
						reject.MaintainTime =	actionEventArgs.ProductInfo.NowSimulationReport.MaintainTime;
						reject.MaintainUser =	actionEventArgs.ProductInfo.NowSimulationReport.MaintainUser;
						reject.MOCode =			actionEventArgs.ProductInfo.NowSimulationReport.MOCode;
						reject.ModelCode =		actionEventArgs.ProductInfo.NowSimulationReport.ModelCode;
						reject.OPCode =			actionEventArgs.ProductInfo.NowSimulationReport.OPCode;
						reject.RejectDate =		actionEventArgs.ProductInfo.NowSimulationReport.MaintainDate;
						reject.RejectStatus =	RejectStatus.Reject;
						reject.RejectTime=		actionEventArgs.ProductInfo.NowSimulationReport.MaintainTime;
						reject.RejectUser =		actionEventArgs.ProductInfo.NowSimulationReport.MaintainUser;
						reject.ResourceCode =	actionEventArgs.ProductInfo.NowSimulationReport.ResourceCode;
						reject.RouteCode =		((ExtendSimulation)actionEventArgs.ProductInfo.LastSimulation).NextRouteCode;
						reject.RunningCard  =	actionEventArgs.ProductInfo.NowSimulationReport.RunningCard;
						reject.RunningCardSequence =	actionEventArgs.ProductInfo.NowSimulationReport.RunningCardSequence;
						reject.SegmentCode =	actionEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
						reject.SourceCard =		actionEventArgs.ProductInfo.NowSimulationReport.SourceCard;
						reject.SourceCardSequence =		actionEventArgs.ProductInfo.NowSimulationReport.SourceCardSequence;
						reject.StepSequenceCode =		actionEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
						reject.TranslateCard =				actionEventArgs.ProductInfo.NowSimulationReport.TranslateCard;
						reject.TranslateCardSequence =	actionEventArgs.ProductInfo.NowSimulationReport.TranslateCardSequence;
						//reject.LOTNO  ="~";
						reject.OPId = "~";
						reject.EAttribute1 = ((TSActionEventArgs)actionEventArgs).Memo;
                        reject.MOSeq = actionEventArgs.ProductInfo.NowSimulation.MOSeq;      // Added by Icyer 2007/07/02
						rework.AddReject(reject);
						
						
						//reject errorcode TODO
						//填写测试报表 TODO
						object[] obj =((TSActionEventArgs)actionEventArgs).ErrorCodes;
						if (obj != null)
						{
							Reject2ErrorCode reject2ErrorCode = rework.CreateNewReject2ErrorCode();
							for (int i=0; i<obj.Length; i++)
							{
								reject2ErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)obj[i]).ErrorCode;
								reject2ErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)obj[i]).ErrorCodeGroup;
								reject2ErrorCode.MaintainDate = reject.MaintainDate;
								reject2ErrorCode.MaintainTime = reject.MaintainTime;
								reject2ErrorCode.MaintainUser = reject.MaintainUser;
								reject2ErrorCode.MOCode = reject.MOCode;
								reject2ErrorCode.RunningCard = reject.RunningCard;
								reject2ErrorCode.RunningCardSequence = reject.RunningCardSequence;
								reject2ErrorCode.LotNo = reject.LOTNO;
								//reject2ErrorCode.LotSeq = reject.l
                                reject2ErrorCode.MOSeq = reject.MOSeq;  // Added by Icyer 2007/07/03
								rework.AddReject2ErrorCode(reject2ErrorCode);
							}
						}
				

                        //ReportHelper reportCollect= new ReportHelper(this.DataProvider);
                        //messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));
                        ////messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));
                        //messages.AddMessages(reportCollect.ReportLineECOQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo,((TSActionEventArgs)actionEventArgs).ErrorCodes));

						//				}				
						//					if (messages.IsSuccess())
						//					{
						//						actionEventArgs.ProductInfo.ECG2ErrCodes =  ActionTS.ParseECG2Errs(((OutLineActionEventArgs)actionEventArgs).ErrorCodes,actionEventArgs.ActionType);
						//						//							//填写测试报表 TODO
						//						ReportHelper reportCollect= new ReportHelper(this.DataProvider);
						//						messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));
						//						messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));
						//
						//						if (actionEventArgs.ActionType == ActionType.DataCollectAction_OutLineReject)
						//						{
						//							messages.AddMessages( dataCollect.CollectErrorInformation(this.DataProvider, actionEventArgs.ActionType, 
						//								actionEventArgs.ProductInfo, 
						//								((OutLineActionEventArgs)actionEventArgs).ErrorCodes, null, 
						//								((OutLineActionEventArgs)actionEventArgs).Memo));
						//							messages.AddMessages(reportCollect.ReportLineECOQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo,((OutLineActionEventArgs)actionEventArgs).ErrorCodes));
						//						}
						//
						//					}
						//						
					}
				}
			}
			catch (Exception e)
			{
				messages.Add(new Message(e));
			}
			dataCollectDebug.WhenFunctionOut(messages);
			return messages;
		}

		public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
		{				
			Messages messages=new Messages();
			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"Collect");
			dataCollectDebug.WhenFunctionIn(messages);
			try
			{
				if (((OutLineActionEventArgs)actionEventArgs).OPCode == string.Empty)
				{
					throw new Exception("$CS_Sys_OutLine_LostOPParam");
				}

				string opCode = ((OutLineActionEventArgs)actionEventArgs).OPCode;
				//检查ErrorCode,ErrorGroup是否正确 TODO
				//如果CS能确保ErrorCode,ErrorGroup是正确的，此处逻辑可以去掉
				if (actionEventArgs.ActionType ==ActionType.DataCollectAction_OutLineNG)
				{
				}
				messages.AddMessages(this.CheckIDIn(actionEventArgs));
				if (messages.IsSuccess())
				{
					ActionOnLineHelper dataCollect=new ActionOnLineHelper(this.DataProvider);															
					//补充SIMULATION 不良信息
					if (actionEventArgs.ActionType == ActionType.DataCollectAction_OutLineNG)
					{
						actionEventArgs.ProductInfo.NowSimulation.ProductStatus =	ProductStatus.NG;
						actionEventArgs.ProductInfo.NowSimulation.NGTimes = actionEventArgs.ProductInfo.NowSimulation.NGTimes+1;
					}

					if (actionEventArgs.ActionType == ActionType.DataCollectAction_OutLineGood)
					{
						actionEventArgs.ProductInfo.NowSimulation.ProductStatus =	ProductStatus.GOOD;
						//Laws Lu,2005/08/15,新增	完工逻辑，在其他Check都通过的情况下，所有的RunningCard应该是GOOD状态
						//暂时不考虑线外工序
						//						DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
						//						if (actionEventArgs.ProductInfo.NowSimulation.RouteCode == "" && dataCollectFacade.OPIsMORouteLastOP(
						//							actionEventArgs.ProductInfo.NowSimulation.MOCode
						//							,actionEventArgs.ProductInfo.NowSimulation.FromRoute
						//							,actionEventArgs.ProductInfo.NowSimulation.FromOP))
						//						{
						//							actionEventArgs.ProductInfo.NowSimulation.IsComplete = "1";
						//							actionEventArgs.ProductInfo.NowSimulation.EAttribute1 = "GOOD";
						//						}
						//End Laws Lu

					}
					if (actionCheckStatus.NeedUpdateSimulation == true)
					{
						messages.AddMessages( dataCollect.Execute(actionEventArgs));
					}
					else
					{
						messages.AddMessages( dataCollect.Execute(actionEventArgs, actionCheckStatus));
					}
					if (messages.IsSuccess())
					{
						if (actionCheckStatus.NeedFillReport == true)
						{
							//							//填写测试报表 TODO
                            //ReportHelper reportCollect= new ReportHelper(this.DataProvider);
                            //messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));
                            //messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));

							if (actionEventArgs.ActionType ==ActionType.DataCollectAction_OutLineNG)
							{
								messages.AddMessages( dataCollect.CollectErrorInformation(this.DataProvider, actionEventArgs.ActionType, 
									actionEventArgs.ProductInfo, 
									((OutLineActionEventArgs)actionEventArgs).ErrorCodes, null, 
									((OutLineActionEventArgs)actionEventArgs).Memo));
							
                                //messages.AddMessages(reportCollect.ReportLineECOQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo,((OutLineActionEventArgs)actionEventArgs).ErrorCodes));
							
							}
						}
					}
						
				}
			}
			catch (Exception e)
			{
				messages.Add(new Message(e));
			}
			dataCollectDebug.WhenFunctionOut(messages);
			return messages;
		}
		
	}
}
