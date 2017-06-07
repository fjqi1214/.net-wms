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
	public class ActionRework:IAction
	{
		
		private IDomainDataProvider _domainDataProvider = null;

//		public ActionRework()
//		{	
//		}

		public ActionRework(IDomainDataProvider domainDataProvider)
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
		/// <summary>
		/// 检查出TS是否允许，并填写部分SIMULATION
		/// </summary>
		/// <param name="iD"></param>
		/// <param name="actionType"></param>
		/// <param name="resourceCode"></param>
		/// <param name="userCode"></param>
		/// <param name="product"></param>
		/// <returns></returns>
//		public Messages CheckID(string iD,string actionType,string resourceCode,string userCode, ProductInfo product)
//		{
//			Messages messages=new Messages();
//			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"CheckID");
//			dataCollectDebug.WhenFunctionIn(messages);
//			try
//			{
//				MOFacade moFacade=new MOFacade(this.DataProvider);				 
//				BaseModelFacade dataModel=new BaseModelFacade(this.DataProvider);
//				DataCollectFacade dataCollectFacade=new DataCollectFacade(this.DataProvider);
//				//IDataCollectModule dataCollect=new DataCollectOnLine();
//				ReworkFacade reworkFacade=new ReworkFacade(this.DataProvider);
//
//				#region 检查工单
//				MO mo=(MO)moFacade.GetMO(product.LastSimulation.MOCode);
//				//工单状态检查
//				if (!dataCollectFacade.CheckMO(mo))
//				{
//					throw new Exception("$MOStatus_Error"+mo.MOStatus);
//				}		
//				#endregion
//				#region 检查ID状态
//				//根据TS中ID状态决定 TODO
//				Reject reject=(Reject)reworkFacade.GetReject(product.LastSimulation.RunningCard,
//					product.LastSimulation.RunningCardSequence);
//				if (reject==null)
//					throw new Exception("$CS_SystemError_LostRejectInfo $CS_Param_ID="+product.LastSimulation.RunningCard
//						+" $CS_Param_RunSeq="+product.LastSimulation.RunningCardSequence);
//				ItemRoute2OP op ;
//				if (reject.RejectStatus ==RejectStatus.UnReject)
//				{
//					//按正常走
//                    dataCollectDebug.DebugPoint(messages,"取消判退");
//					#region 检查途程					
//					if ((actionType ==ActionType.DataCollectAction_OutLineGood )
//						||(actionType ==ActionType.DataCollectAction_OutLineNG )
//						)
//					{
//						dataCollectDebug.DebugPoint(messages,"线外站采集");
//						//线外站不需要检查Route,请自己填写SIMULATION信息					
//						dataCollectDebug.WhenFunctionOut(messages);
//						return messages;
//					}
//					else
//					{	
//						//当前站重复采集
//						if (dataModel.GetOperation2Resource(product.LastSimulation.OPCode,resourceCode)==null)
//						{	
//							dataCollectDebug.DebugPoint(messages,"下一站采集");
//							op=dataCollectFacade.GetMORouteNextOP(product.LastSimulation.MOCode,product.LastSimulation.RouteCode,product.LastSimulation.OPCode);
//							if (op==null)
//								throw new Exception("$CS_Route_Failed_GetNotNextOP");
//							if (dataModel.GetOperation2Resource(op.OPCode,resourceCode)==null)
//							{
//								throw new Exception("$CS_Route_Failed $CS_Param_OPCode"+op.OPCode);						
//							}
//						}
//						else
//						{
//							dataCollectDebug.DebugPoint(messages,"当前站重复采集");
//							//检查是否重复采集
//							dataCollectFacade.CheckRepeatCollect(product.LastSimulation.ActionList,actionType);
//							op=dataCollectFacade.GetMORouteOP(product.LastSimulation.ItemCode,product.LastSimulation.MOCode,
//								product.LastSimulation.RouteCode,product.LastSimulation.OPCode);
//						}
//					}
//					#endregion
//				}
//				else
//				if (reject.RejectStatus ==RejectStatus.Confirm)
//				{
//					ReworkSheet rework=(ReworkSheet) reworkFacade.GetReworkSheet( reject.ReworkCode );				
//					if (rework.Status ==ReworkStatus.REWORKSTATUS_OPEN)
//					{
//						op=dataCollectFacade.GetMORouteFirstOP(product.LastSimulation.MOCode
//							,rework.NewRouteCode);
//					}
//					else
//					{
//						throw new Exception("$CS_IDisReject");
//					}
//				}
//				else				
//				  throw new Exception("$CS_IDisReject");
//				#endregion
//				#region 检查ACTION
//				if (dataCollectFacade.CheckAction(product,op,actionType))
//				{
//				}
//				#endregion
//				#region 填写新SIMULATION
//				messages.AddMessages( dataCollectFacade.WriteSimulation(iD,actionType,resourceCode,userCode,product));
//				if (messages.IsSuccess())
//				{
//					//修改
//					product.NowSimulation.RouteCode =product.LastSimulation.RouteCode;
//					product.NowSimulation.OPCode =op.OPCode;				
//					product.NowSimulation.ResourceCode=resourceCode;	
//					
//					product.NowSimulation.ActionList =";"+actionType+";";				
//				}
//				#endregion
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
			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"CheckIDIn");
			dataCollectDebug.WhenFunctionIn(messages);
			try
			{
				ActionOnLineHelper dataCollect=new ActionOnLineHelper(this.DataProvider);
				messages.AddMessages( dataCollect.CheckID(actionEventArgs));
				if (messages.IsSuccess())
				{	
					dataCollectDebug.DebugPoint(messages,"推途程成功");
					//补充SIMULATION 不良信息
					actionEventArgs.ProductInfo.NowSimulation.ProductStatus =ProductStatus.Reject;
					actionEventArgs.ProductInfo.NowSimulation.NGTimes = actionEventArgs.ProductInfo.NowSimulation.NGTimes+1;
				}
			}
			catch (Exception e)
			{
				messages.Add(new Message(e));
			}
			dataCollectDebug.WhenFunctionOut(messages);
			return messages;
		}

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
				//检查ErrorCode,ErrorGroup是否正确 TODO
				//如果CS能确保ErrorCode,ErrorGroup是正确的，此处逻辑可以去掉


				ActionOnLineHelper dataCollect=new ActionOnLineHelper(this.DataProvider);
				//填写SIMULATION 检查工单、ID、途程、操作
				messages.AddMessages( this.CheckIDIn(actionEventArgs));
				if (messages.IsSuccess())
				{						
					messages.AddMessages( dataCollect.Execute(actionEventArgs));
					if (messages.IsSuccess())
					{
						//填写Reject报表 TODO
						ReworkFacade rework=new ReworkFacade(this.DataProvider);
						Reject reject= rework.CreateNewReject();
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
						reject.RouteCode =		actionEventArgs.ProductInfo.NowSimulationReport.RouteCode;
						reject.RunningCard  =	actionEventArgs.ProductInfo.NowSimulationReport.RunningCard;
						reject.RunningCardSequence =	actionEventArgs.ProductInfo.NowSimulationReport.RunningCardSequence;
						reject.SegmentCode =	actionEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
						reject.SourceCard =		actionEventArgs.ProductInfo.NowSimulationReport.SourceCard;
						reject.SourceCardSequence =		actionEventArgs.ProductInfo.NowSimulationReport.SourceCardSequence;
						reject.StepSequenceCode =		actionEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
						reject.TranslateCard =				actionEventArgs.ProductInfo.NowSimulationReport.TranslateCard;
						reject.TranslateCardSequence =	actionEventArgs.ProductInfo.NowSimulationReport.TranslateCardSequence;
						//reject.LOTNO  ="~";
						reject.OPId="~";
						reject.EAttribute1 = ((TSActionEventArgs)actionEventArgs).Memo;
                        reject.MOSeq = actionEventArgs.ProductInfo.NowSimulationReport.MOSeq;   // Added by Icyer 2007/07/02
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
                                reject2ErrorCode.MOSeq = reject.MOSeq;  // Added by Icyer 2007/07/03
								rework.AddReject2ErrorCode(reject2ErrorCode);
							}
						}
					}

                    //ReportHelper reportCollect= new ReportHelper(this.DataProvider);
                    //messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));
                    ////messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));
                    //messages.AddMessages(reportCollect.ReportLineECOQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo,((TSActionEventArgs)actionEventArgs).ErrorCodes));

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
