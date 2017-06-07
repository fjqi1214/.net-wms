using System;
using System.Collections;

using UserControl;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.OQC;

namespace BenQGuru.eMES.DataCollect.Action
{
	/// <summary>
	/// ActionOQCFuncTest 的摘要说明。
	/// </summary>
	public class ActionOQCFuncTest : IActionWithStatus
	{
		private IDomainDataProvider _domainDataProvider = null;

		public ActionOQCFuncTest(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}
		public Messages Execute(ActionEventArgs actionEventArgs)
		{
			return Execute(actionEventArgs, null);
		}
		
		public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
		{
			Messages messages=new Messages();
			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"Collect");
			dataCollectDebug.WhenFunctionIn(messages);
			try
			{
				ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
				//填写SIMULATION 检查工单、ID、途程、操作
				messages.AddMessages( dataCollect.CheckID(actionEventArgs, actionCheckStatus));
				
				if (messages.IsSuccess())
				{
					// 0. 检查OQC Lot
					// 1. OQCFuncTestValue
					// 2. OQCFuncTestValueDetail
					// 3. OQCFuncTestValueEleDetail
					OQCFuncTestActionEventArgs args = (OQCFuncTestActionEventArgs)actionEventArgs;
					Simulation sim = args.ProductInfo.LastSimulation;
					OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
					OQCLot oqcLot = (OQCLot)oqcFacade.GetOQCLot(sim.LOTNO, OQCFacade.Lot_Sequence_Default);
					if (oqcLot.LOTStatus != OQCLotStatus.OQCLotStatus_Examing && 
						oqcLot.LOTStatus != OQCLotStatus.OQCLotStatus_NoExame)
					{
						messages.Add(new Message(MessageType.Error, "$OQCLot_Status_Must_Be_Examing"));
					}
					if (messages.IsSuccess())
					{
						BenQGuru.eMES.BaseSetting.BaseModelFacade baseFacade = new BaseModelFacade(this.DataProvider);
						Resource resource = (Resource)baseFacade.GetResource(args.ResourceCode);
						Operation operation = (Operation)baseFacade.GetOperationByRouteAndResource(sim.RouteCode, resource.ResourceCode);
						if (operation == null)
							operation = new Operation();
						// 1. OQCFuncTestValue
						OQCFuncTestValue testValue = new OQCFuncTestValue();

						testValue.RunningCard = args.RunningCard;
						testValue.RunningCardSequence = sim.RunningCardSequence;
						testValue.ModelCode = sim.ModelCode;
						testValue.ItemCode = sim.ItemCode;
						testValue.MOCode = sim.MOCode;
						testValue.LotNO = sim.LOTNO;
						testValue.LotSequence = oqcLot.LotSequence;
						testValue.FuncTestGroupCount = args.oqcFuncTest.FuncTestGroupCount;
						testValue.MinDutyRatoMin = args.oqcFuncTest.MinDutyRatoMin;
						testValue.MinDutyRatoMax = args.oqcFuncTest.MinDutyRatoMax;
						testValue.MinDutyRatoValue = args.minDutyRatoValue;
						testValue.BurstMdFreMin = args.oqcFuncTest.BurstMdFreMin;
						testValue.BurstMdFreMax = args.oqcFuncTest.BurstMdFreMax;
						testValue.BurstMdFreValue = args.burstMdFreValue;
						testValue.ElectricTestCount = args.oqcFuncTest.ElectricTestCount;
						testValue.OPCode = operation.OPCode;
						testValue.StepSequenceCode = resource.StepSequenceCode;
						testValue.SegmentCode = resource.SegmentCode;
						testValue.ResourceCode = args.ResourceCode;
						if (args.Result == true)
							testValue.ProductStatus = ProductStatus.GOOD;
						else
							testValue.ProductStatus = ProductStatus.NG;
						testValue.MaintainUser = args.UserCode;

						DBDateTime dbDateTime;
						//Laws Lu,2006/11/13 uniform system collect date
						if(actionEventArgs.ProductInfo.WorkDateTime != null)
						{
							dbDateTime = actionEventArgs.ProductInfo.WorkDateTime;
							
						}
						else
						{
							dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
							actionEventArgs.ProductInfo.WorkDateTime = dbDateTime;
						}

						testValue.MaintainDate = dbDateTime.DBDate;
						testValue.MaintainTime = dbDateTime.DBTime;

						/* modified by jessie lee, 2006/8/10 */
						object obj = oqcFacade.GetOQCFuncTestValue( args.RunningCard, sim.RunningCardSequence );
						if( obj==null )
						{
							oqcFacade.AddOQCFuncTestValue(testValue);
						}
						else
						{
							oqcFacade.UpdateOQCFuncTestValue(testValue);
							
                            //// Added by Icyer 2007/01/12	如果是重复测试，则需要先删除原来的数据
                            //OQCFuncTestValue testValueTmp = (OQCFuncTestValue)obj;
                            //BenQGuru.eMES.SPCDataCenter.DataHandler deleteHandler = new BenQGuru.eMES.SPCDataCenter.DataHandler(this.DataProvider);
                            //deleteHandler.DeleteData(SPCObjectList.OQC_FT_FREQUENCY, testValueTmp.ItemCode, testValueTmp.MOCode, testValueTmp.RunningCard, testValueTmp.RunningCardSequence, testValueTmp.MaintainDate);
                            //// Added end
						}

                        //// Added by Icyer 2006-08-11
                        //BenQGuru.eMES.SPCDataCenter.DataEntry dataEntry = new BenQGuru.eMES.SPCDataCenter.DataEntry();
                        //dataEntry.ModelCode = sim.ModelCode;
                        //dataEntry.ItemCode = sim.ItemCode;
                        //dataEntry.MOCode = sim.MOCode;
                        //dataEntry.RunningCard = sim.RunningCard;
                        //dataEntry.RunningCardSequence = sim.RunningCardSequence;
                        //dataEntry.SegmentCode = resource.SegmentCode;
                        //dataEntry.LineCode = resource.StepSequenceCode;
                        //dataEntry.ResourceCode = resource.ResourceCode;
                        //dataEntry.OPCode = sim.OPCode;
                        //dataEntry.LotNo = sim.LOTNO;
                        //dataEntry.TestDate = testValue.MaintainDate;
                        //dataEntry.TestTime = testValue.MaintainTime;
                        //if (args.Result == true)
                        //    dataEntry.TestResult = "P";
                        //else
                        //    dataEntry.TestResult = "F";
                        //dataEntry.TestUser = args.UserCode;
                        //dataEntry.AddTestData(SPCObjectList.OQC_DUTY_RATO, args.minDutyRatoValue);
                        //dataEntry.AddTestData(SPCObjectList.OQC_BURST_MD, args.burstMdFreValue);
                        //for (int i = 0; i < args.oqcFuncTest.FuncTestGroupCount; i++)
                        //{
                        //    dataEntry.AddTestData(SPCObjectList.OQC_FT_FREQUENCY, i + 1, Convert.ToDecimal(args.listTestValueFre[i]));
                        //    Hashtable ht = (Hashtable)args.listTestValueEle[i];
                        //    decimal[] dataEle = new decimal[Convert.ToInt32(args.oqcFuncTest.ElectricTestCount)];
                        //    for (int n = 0; n < args.oqcFuncTest.ElectricTestCount; n++)
                        //    {
                        //        dataEle[n] = Convert.ToDecimal(ht[(n + 1).ToString()]);
                        //    }
                        //    dataEntry.AddTestData(SPCObjectList.OQC_FT_ELECTRIC, i + 1, dataEle);
                        //}
                        //BenQGuru.eMES.SPCDataCenter.DataHandler handler = new BenQGuru.eMES.SPCDataCenter.DataHandler(this.DataProvider);
                        //messages.AddMessages(handler.CollectData(dataEntry));
                        //// Added end						
					
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
