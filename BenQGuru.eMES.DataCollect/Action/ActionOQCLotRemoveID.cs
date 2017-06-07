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
	public class ActionOQCLotRemoveID:IAction
	{
		
		private IDomainDataProvider _domainDataProvider = null;

//		public ActionOQCLotRemoveID()
//		{	
//		}

		public ActionOQCLotRemoveID(IDomainDataProvider domainDataProvider)
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

		public Messages Execute(ActionEventArgs oqcLotRemoveIDEventArgs)
		{				
			Messages messages=new Messages();
			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"Collect");
			dataCollectDebug.WhenFunctionIn(messages);
			
			try
			{
				ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
				ActionOQCHelper actionOQCHelper = new ActionOQCHelper(this.DataProvider);
				OQCFacade oqcFacade = new OQCFacade(this.DataProvider);

				//检查批的状态
				if( !actionOQCHelper.IsOQCLotInitial( ((OQCLotRemoveIDEventArgs)oqcLotRemoveIDEventArgs).OQCLotNO))
				{
					throw new Exception("$Error_OQCLotNOIsNotInitial");
				}
				//填写SIMULATION 检查工单、ID、途程、操作
				messages.AddMessages( dataCollect.CheckID(oqcLotRemoveIDEventArgs));
				if (messages.IsSuccess())
				{				
					
					#region update FQCLotSize,Laws Lu,2005/10/24，修改	
					object obj = oqcFacade.GetOQCLot(((OQCLotRemoveIDEventArgs)oqcLotRemoveIDEventArgs).OQCLotNO,OQCFacade.Lot_Sequence_Default);
					if(obj == null)
					{
						throw new Exception("$Error_OQCLotNotExisted");
					}
					OQCLot oqcLot = obj as OQCLot;
					oqcLot.LotSize = -1;//oqcFacade.GetOQCLotSizeFromOQCLot2Card( ((OQCLotRemoveIDEventArgs)oqcLotRemoveIDEventArgs).OQCLotNO);
					oqcFacade.UpdateOQCLotSize(oqcLot);
					#endregion

					#region OQCADDID 自身的检查

					//检查批的状态
					if( actionOQCHelper.IsOQCLotComplete( ((OQCLotRemoveIDEventArgs)oqcLotRemoveIDEventArgs).OQCLotNO))
					{
						throw new Exception("$Error_OQCLotNOHasComplete");
					}
					#endregion

					//把nowsimulation 的actionlist的OqcAdd,oQCDelete,replace为空
					oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.ActionList = oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.ActionList.Replace(ActionType.DataCollectAction_OQCLotAddID,""); 
					oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.ActionList = oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.ActionList.Replace(ActionType.DataCollectAction_OQCLotRemoveID,""); 


					oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.LOTNO = String.Empty;
					/* added by jessie lee, 2006/6/20
					 * Power0063:包装时，如果从抽检批中删除一个产品，应该从包装箱中删除此产品。 */
					string cartoncode = string.Empty;
					if( oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.CartonCode !=null 
						&& oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.CartonCode.Length>0)
					{
						cartoncode = oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.CartonCode;
						oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.CartonCode = string.Empty;
					}

					messages.AddMessages( dataCollect.Execute(oqcLotRemoveIDEventArgs));
					
					if (messages.IsSuccess())
					{
						//删除对应OQCLot2Card,如果是最后一个则删除对应OQCLot
						#region OQC
						object objLot2Card = oqcFacade.GetOQCLot2Card(oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.RunningCard, oqcLotRemoveIDEventArgs.ProductInfo.NowSimulation.MOCode,((OQCLotRemoveIDEventArgs)oqcLotRemoveIDEventArgs).OQCLotNO,OQCFacade.Lot_Sequence_Default);
						if(objLot2Card == null)
						{
							throw new Exception("$Error_OQClot2CardNotExisted");
						}
						oqcFacade.DeleteOQCLot2Card( (OQCLot2Card)objLot2Card);
						object[] objs = oqcFacade.ExactQueryOQCLot2Card(((OQCLotRemoveIDEventArgs)oqcLotRemoveIDEventArgs).OQCLotNO,OQCFacade.Lot_Sequence_Default);
						if(objs == null)
						{
							//删除OQCLot2ErrorCode
							oqcFacade.DeleteOQCLot2ErrorCode( ((OQCLotRemoveIDEventArgs)oqcLotRemoveIDEventArgs).OQCLotNO,OQCFacade.Lot_Sequence_Default.ToString());
							//删除OQCLOTCheckList
							obj = oqcFacade.GetOQCLOTCheckList(((OQCLotRemoveIDEventArgs)oqcLotRemoveIDEventArgs).OQCLotNO,OQCFacade.Lot_Sequence_Default);
							if(obj != null)
							{
								oqcFacade.DeleteOQCLOTCheckList((OQCLOTCheckList)obj);
							}
							
							//删除OQCLot
							obj = oqcFacade.GetOQCLot(((OQCLotRemoveIDEventArgs)oqcLotRemoveIDEventArgs).OQCLotNO,OQCFacade.Lot_Sequence_Default);
							if(obj == null)
							{
								throw new Exception("$Error_OQClotNotExisted");
							}
							oqcFacade.DeleteOQCLot((OQCLot)obj);
						}
						
						#endregion
						/* added by jessie lee, 2006/6/20
						 * Power0063:包装时，如果从抽检批中删除一个产品，应该从包装箱中删除此产品。 */
						#region Carton
						if(cartoncode.Length>0)
						{
							Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);
							object objCarton = pf.GetCARTONINFO(cartoncode);
 
							if(objCarton != null)
							{
								BenQGuru.eMES.Domain.Package.CARTONINFO carton = objCarton as BenQGuru.eMES.Domain.Package.CARTONINFO;
								pf.SubtractCollected((carton as BenQGuru.eMES.Domain.Package.CARTONINFO).CARTONNO);
							}
						}
						#endregion
						//AMOI  MARK  START  20050806 增加按资源统计产量
						#region 填写统计报表 按资源统计
                        //ReportHelper reportCollect= new ReportHelper(this.DataProvider);
                        //messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider
                        //    ,oqcLotRemoveIDEventArgs.ActionType,oqcLotRemoveIDEventArgs.ProductInfo));
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
