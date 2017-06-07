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



namespace  BenQGuru.eMES.DataCollect.Action
{
	/// <summary>
	/// 归属工单采集
	/// </summary>
	public class ActionOQC: IAction
	{
		private IDomainDataProvider _domainDataProvider = null;

		public ActionOQC()
		{	
		}

		public ActionOQC(IDomainDataProvider domainDataProvider)
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
		/// 
		/// </summary>
		/// <param name="actionEventArgs"> </param> params (0,lotno)
		/// <returns></returns>
		public Messages Execute(ActionEventArgs actionEventArgs)
		{				
			Messages messages=new Messages();
			DataCollectDebug dataCollectDebug =new DataCollectDebug(this.GetType().ToString()+"Collect");
			dataCollectDebug.WhenFunctionIn(messages);
			try
			{
				ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
				ActionOQCHelper oqcHelper = new ActionOQCHelper(this.DataProvider);
				
				//填写SIMULATION 检查工单、ID、途程、操作
				messages.AddMessages( dataCollect.CheckID(actionEventArgs));
				if (messages.IsSuccess())
				{				
					//
					if(actionEventArgs.ProductInfo.NowSimulation == null)
					{
						throw new Exception("$System_Error");
					}
					//check oqclotstatus
					if(oqcHelper.IsOQCLotComplete(actionEventArgs.Params[0].ToString()))
					{
						throw new Exception("$Error_OQCLotNOHasComplete");
					}
					//检查在批中检查的产品的最后一笔信息全部为Good

					messages.AddMessages( dataCollect.Execute(actionEventArgs));
					if (messages.IsSuccess())
					{
						//修改批状态
						//批退修改每个板子的最后一笔状态为reject

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
