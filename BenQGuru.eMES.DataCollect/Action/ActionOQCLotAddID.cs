#region system;
using System;
using UserControl;
#endregion

#region project
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
#endregion


namespace BenQGuru.eMES.DataCollect.Action
{
	/// <summary>
	/// 归属工单采集
	/// </summary>
	public class ActionOQCLotAddID:IAction
	{
		
		private IDomainDataProvider _domainDataProvider = null;

//		public ActionOQCLotAddID()
//		{	
//		}

		public ActionOQCLotAddID(IDomainDataProvider domainDataProvider)
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

		public Messages Execute(ActionEventArgs oqcLotAddIDEventArgs)
		{				
			Messages messages=new Messages();
			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"Collect");
			dataCollectDebug.WhenFunctionIn(messages);
			
			try
			{
				ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
				ActionOQCHelper oqcHelper = new ActionOQCHelper(this.DataProvider);
				OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
				//填写SIMULATION 检查工单、ID、途程、操作
				messages.AddMessages( dataCollect.CheckID(oqcLotAddIDEventArgs));
				
				if (messages.IsSuccess())
				{	
					if(((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation == null)
					{
						throw new Exception("$System_Error");
					}

					#region update FQCLotSize
					object obj = oqcFacade.GetOQCLot(((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO,OQCFacade.Lot_Sequence_Default);
					if(obj == null)
					{
						throw new Exception("$Error_OQCLotNotExisted");
					}
					OQCLot oqcLot = obj as OQCLot;
					oqcLot.LotSize = 1;//oqcFacade.GetOQCLotSizeFromOQCLot2Card( ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO);
					//TODO:Laws Lu,2005/10/17,需要修改	更新数量
						
					oqcFacade.UpdateOQCLotSize(oqcLot);
					#endregion

					#region OQCADDID 自身的检查
					#region 只是forcase工具使用，真实的情况是从前台已经产生
					object objOQCLot = oqcFacade.GetOQCLot(((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO,OQCFacade.Lot_Sequence_Default);
					if(objOQCLot == null)
					{
						OQCLot  newOQCLot = oqcFacade.CreateNewOQCLot();
						newOQCLot.LOTNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO));
						newOQCLot.AcceptSize = 0;
						newOQCLot.AcceptSize1 = 0;
						newOQCLot.AcceptSize2 = 0;
						newOQCLot.AQL =0;
						newOQCLot.AQL1 =0;
						newOQCLot.AQL2 =0;
						newOQCLot.LotSequence = OQCFacade.Lot_Sequence_Default;
						newOQCLot.OQCLotType = "oqclottype_normal";
						newOQCLot.LotSize= 0;
						newOQCLot.LOTStatus = OQCLotStatus.OQCLotStatus_Initial;
						newOQCLot.LOTTimes =0;
						newOQCLot.MaintainUser = oqcLotAddIDEventArgs.UserCode;
						newOQCLot.RejectSize = 0;
						newOQCLot.RejectSize1 =0;
						newOQCLot.RejectSize2 =0;
						newOQCLot.SampleSize =0;
						oqcFacade.AddOQCLot(newOQCLot);
					}
					#endregion
				

					//检查批的状态
					if( !oqcHelper.IsOQCLotComplete( ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO))
					{
						//Write off by Laws Lu/2006/05/25
//						//是否同一ItemCode
//						if( oqcHelper.IsRemixItemCode( ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.ItemCode,((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO))
//						{
//							throw new Exception("$Error_RemixItemCode");
//						}
						//End Write off

						//是否混单
						if( !((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).IsRemixMO)
						{
							//如除了正准备的工单号外，还有其他的工单存在则报错
							if( oqcHelper.IsRemixMOCode(((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO, ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.MOCode))
							{
								throw new Exception("$Error_RemixMO");
							}
						}
						//一个ID只能属于一个批（reject,pass状态除外)
						if ( !(oqcHelper.IsIDHasOnlyOQCLotNo(((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).RunningCard,((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.MOCode,((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO)))
						{
							throw new Exception("$Error_IDHasExistedInOtherOQCLotNO");
						}
						//检查完工数量
						if( ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).IsCheckOQCLotMaxSize)
						{
							if( oqcHelper.GetIDCountInOQCLotNo(((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO) >= ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotMaxSize)
							{
								throw new Exception("$Error_OQClotQtyExceedMaxQty");
							}
						}

					}
					else
					{
						throw new Exception("$Error_OQCLotNOHasComplete");
					}
					#endregion
					string cartonno = (oqcLotAddIDEventArgs as OQCLotAddIDEventArgs).CartonNo;

					oqcLotAddIDEventArgs.ProductInfo.NowSimulation.LOTNO = (oqcLotAddIDEventArgs as OQCLotAddIDEventArgs).OQCLotNO;

					//oqcLotAddIDEventArgs.ProductInfo.NowSimulation.CartonCode = (oqcLotAddIDEventArgs as OQCLotAddIDEventArgs).CartonNo;
					// Added by Icyer 2006/06/07
					string oldCartonNo = oqcLotAddIDEventArgs.ProductInfo.NowSimulation.CartonCode;
					if (cartonno != string.Empty)
					{
						oqcLotAddIDEventArgs.ProductInfo.NowSimulation.CartonCode = (oqcLotAddIDEventArgs as OQCLotAddIDEventArgs).CartonNo;
					}
					// Added end
					//是否该产品是否属于wip的其他站
					messages.AddMessages( dataCollect.Execute(oqcLotAddIDEventArgs));

					DBDateTime dbDateTime;
					//Laws Lu,2006/11/13 uniform system collect date
					if(oqcLotAddIDEventArgs.ProductInfo.WorkDateTime != null)
					{
						dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
						oqcLotAddIDEventArgs.ProductInfo.WorkDateTime = dbDateTime;
					}
					else
					{
						dbDateTime = oqcLotAddIDEventArgs.ProductInfo.WorkDateTime;
					}

					if (messages.IsSuccess())
					{
						if (cartonno != string.Empty && cartonno != oldCartonNo)
						{
							//Laws Lu,2006/05/27	包装到Carton
							Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
						
							object objCarton = pf.GetCARTONINFO(cartonno);
 
							if(objCarton != null)
							{
								BenQGuru.eMES.Domain.Package.CARTONINFO carton = objCarton as BenQGuru.eMES.Domain.Package.CARTONINFO;
								/* added by jessie lee, 20006/6/20
								 * Power0086:达到最大包装数量时及时提示 */
								if(carton.CAPACITY == carton.COLLECTED + 1)
								{
									messages.Add( new UserControl.Message(MessageType.Normal,"$CARTON_ALREADY_FULL_PlEASE_CHANGE"));
								}
								//Laws Lu,2006/06/22 modify cancle exception and use message 
								if(carton.CAPACITY <= carton.COLLECTED)
								{
									messages.Add( new UserControl.Message(MessageType.Error,"$CARTON_ALREADY_FILL_OUT"));
								}
								else
								{
									pf.UpdateCollected((carton as BenQGuru.eMES.Domain.Package.CARTONINFO).CARTONNO);
								}
							}
							else if(cartonno != String.Empty)
							{
								//Laws Lu,2006/06/22	modify check if carton exist
								object objExistCTN = pf.GetExistCARTONINFO(cartonno);

								if(objExistCTN != null)
								{
									messages.Add( new UserControl.Message(MessageType.Error,"$CARTON_ALREADY_FULL_PlEASE_CHANGE"));
								}
								else
								{
									BenQGuru.eMES.Domain.Package.CARTONINFO  carton = new BenQGuru.eMES.Domain.Package.CARTONINFO();

                                    carton.CAPACITY = ((new ItemFacade(DataProvider)).GetItem(oqcLotAddIDEventArgs.ProductInfo.NowSimulation.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID) as Item).ItemCartonQty;
									carton.COLLECTED = 1 /** oqcLotAddIDEventArgs.ProductInfo.NowSimulation.IDMergeRule*/;
									carton.PKCARTONID = System.Guid.NewGuid().ToString().ToUpper();
									carton.CARTONNO = cartonno;
									carton.MUSER = oqcLotAddIDEventArgs.UserCode;

									

									carton.MDATE = dbDateTime.DBDate;
									carton.MTIME = dbDateTime.DBTime;
									//carton.

									//joe song 20060630 Carton Memo
									carton.EATTRIBUTE1 = (oqcLotAddIDEventArgs as OQCLotAddIDEventArgs).CartonMemo;

									if(carton.CAPACITY == 0)//Get carton capacity by item
									{
										messages.Add(new UserControl.Message(MessageType.Error
											,"$CS_PLEASE_MAINTEIN_ITEMCARTON $CS_Param_ID =" + oqcLotAddIDEventArgs.RunningCard));
									}
									else
									{
										pf.AddCARTONINFO(carton);
									}
								}
							}
							//End
						}
					}

					if (messages.IsSuccess())
					{
						//填写对应的runningcard 到OQCLotNO
						#region add OQCLot2Card
						OQCLot2Card oqcLot2Card = oqcFacade.CreateNewOQCLot2Card();
						oqcLot2Card.ItemCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.ItemCode;
						oqcLot2Card.CollectType = oqcLotAddIDEventArgs.CollectType;
						oqcLot2Card.LOTNO = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).OQCLotNO;
						oqcLot2Card.LotSequence = OQCFacade.Lot_Sequence_Default;
						oqcLot2Card.MaintainUser = oqcLotAddIDEventArgs.UserCode;
						oqcLot2Card.MaintainDate = dbDateTime.DBDate;
						oqcLot2Card.MaintainTime = dbDateTime.DBTime;
						oqcLot2Card.MOCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.MOCode;
						oqcLot2Card.ModelCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.ModelCode;
						oqcLot2Card.OPCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.OPCode;
						oqcLot2Card.ResourceCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.ResourceCode;
						oqcLot2Card.RouteCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.RouteCode;
						oqcLot2Card.RunningCard = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.RunningCard;
						oqcLot2Card.RunningCardSequence = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.RunningCardSequence;
						oqcLot2Card.SegmnetCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulationReport.SegmentCode;
						oqcLot2Card.ShiftCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulationReport.ShiftCode;
						oqcLot2Card.ShiftTypeCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulationReport.ShiftTypeCode;
						oqcLot2Card.Status = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulationReport.Status;
						oqcLot2Card.StepSequenceCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulationReport.StepSequenceCode;
						oqcLot2Card.TimePeriodCode = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulationReport.TimePeriodCode;
						//oqcLot2Card.EAttribute1 =  (oqcLotAddIDEventArgs as OQCLotAddIDEventArgs).CartonNo;
						oqcLot2Card.EAttribute1 = oqcLotAddIDEventArgs.ProductInfo.NowSimulation.CartonCode;	// Added by Icyer 2006/06/08
                        oqcLot2Card.MOSeq = ((OQCLotAddIDEventArgs)oqcLotAddIDEventArgs).ProductInfo.NowSimulation.MOSeq;   // Added by Icyer 2007/07/02    增加 MOSeq 栏位

						oqcFacade.AddOQCLot2Card(oqcLot2Card);
						#endregion
						//AMOI  MARK  START  20050806 增加按资源统计产量
						#region 填写统计报表 按资源统计
                        //ReportHelper reportCollect= new ReportHelper(this.DataProvider);
                        //messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider
                        //    ,oqcLotAddIDEventArgs.ActionType,oqcLotAddIDEventArgs.ProductInfo));
						#endregion
						//AMOI  MARK  END
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
