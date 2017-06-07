using System;
using UserControl;
using BenQGuru.eMES.Common.Domain;



namespace BenQGuru.eMES.LotDataCollect.Action
{
    /// <summary>
    /// 归属工单采集
    /// </summary>
    public class ActionGood : IActionWithStatus
    {
        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionGood()
        //		{	
        //		}

        public ActionGood(IDomainDataProvider domainDataProvider)
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

        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            Messages msgAutoGoMO = new Messages();		// Added by Icyer 2007/03/09
            try
            {
                // Added by Icyer 2007/03/09
                // 检测自动归属工单
                ActionGoToMO actionGoToMO = new ActionGoToMO(this.DataProvider);
                msgAutoGoMO = actionGoToMO.AutoGoMO(actionEventArgs);
                // Added end

                // Added end
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs));
                if (messages.IsSuccess())
                {
                 
                    messages.AddMessages(dataCollect.Execute(actionEventArgs));
                    if (messages.IsSuccess())
                    {
                        //// 自动产生送检批
                        //messages.AddMessages(this.GenerateLot(actionEventArgs));
                        //if (messages.IsSuccess())
                        //{
                        //    //填写测试报表 TODO
                        //    ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                        //    messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo));
                        //    messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo));
                        //}
                    }
                }
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            //return messages;
            if (msgAutoGoMO.Count() < 1 || (msgAutoGoMO.IsSuccess() == true && messages.IsSuccess() == false))
                return messages;
            else
            {
                msgAutoGoMO.IgnoreError();
                msgAutoGoMO.AddMessages(messages);
                return msgAutoGoMO;
            }
        }

        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            Messages msgAutoGoMO = new Messages();		// Added by Icyer 2007/03/09
            try
            {
                // Added by Icyer 2007/03/09
                // 检测自动归属工单
                ActionGoToMO actionGoToMO = new ActionGoToMO(this.DataProvider);
                msgAutoGoMO = actionGoToMO.AutoGoMO(actionEventArgs);
                // Added end
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs, actionCheckStatus));

                if (messages.IsSuccess())
                {
                    //End Laws Lu
                    if (actionCheckStatus.NeedUpdateSimulation == true)
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs));
                    }
                    else
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs, actionCheckStatus));
                    }
                    if (messages.IsSuccess())
                    {
                        // 自动产生送检批
                        //messages.AddMessages(this.GenerateLot(actionEventArgs));
                        if (messages.IsSuccess())
                        {
                            ////填写测试报表 TODO
                            //if (actionCheckStatus.NeedFillReport == true)
                            //{
                            //    ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                            //    messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, actionCheckStatus));
                            //    messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, actionCheckStatus));
                            //}

                            //将Action加入列表
                            actionCheckStatus.ActionList.Add(actionEventArgs);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            //return messages;
            if (msgAutoGoMO.Count() < 1 || (msgAutoGoMO.IsSuccess() == true && messages.IsSuccess() == false))
                return messages;
            else
            {
                msgAutoGoMO.IgnoreError();
                msgAutoGoMO.AddMessages(messages);
                return msgAutoGoMO;
            }
        }

    }
}
