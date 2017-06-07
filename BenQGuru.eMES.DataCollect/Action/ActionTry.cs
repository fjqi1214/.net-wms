using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.DataCollect.Action
{

    /// <summary>
    /// 试流单采集
    /// </summary>
    public class ActionTry : IActionWithStatus
    {
        private IDomainDataProvider _domainDataProvider = null;

        public ActionTry(IDomainDataProvider domainDataProvider)
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

        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");

            TryEventArgs tryEventArgs = actionEventArgs as TryEventArgs;
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int date = dbDateTime.DBDate;
            int time = dbDateTime.DBTime;
            string userCode = tryEventArgs.UserCode;

            string tryCode = tryEventArgs.TryCode.Trim().ToUpper();
            string mItemCode = tryEventArgs.MItemCode.Trim().ToUpper();
            string mCard = tryEventArgs.MRunningCard.Trim().ToUpper();
            string itemCode = tryEventArgs.ItemCode.Trim().ToUpper();
            string rCard = tryEventArgs.RunningCard.Trim().ToUpper();

            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                TryFacade tryFacade = new TryFacade(this.DataProvider);

                //用于上料或者carton采集
                if (tryEventArgs.ForCollect)
                {
                    if (string.IsNullOrEmpty(tryEventArgs.MRunningCard))
                    {
                        List<string> TryCodeList = new List<string>();
                        if (tryEventArgs.TryCode.Trim().Length > 0)
                        {
                            if (tryEventArgs.TryCode.IndexOf(",") >= 0)
                            {
                                string[] TryCode = tryEventArgs.TryCode.Split(',');
                                for (int i = 0; i < TryCode.Length; i++)
                                {
                                    TryCodeList.Add(TryCode[i]);
                                }
                            }
                            else
                            {
                                TryCodeList.Add(tryEventArgs.TryCode.Trim());
                            }

                            for (int i = 0; i < TryCodeList.Count; i++)
                            {
                                string TryCode = TryCodeList[i].ToString().Trim().ToUpper();

                                Try2RCard try2RCard = (Try2RCard)tryFacade.GetTry2RCard(TryCode, tryEventArgs.RunningCard, tryEventArgs.ItemCode);
                                if (try2RCard == null)
                                {
                                    try2RCard = tryFacade.CreateNewTry2RCard();

                                    try2RCard.TryCode = TryCode;
                                    try2RCard.RCard = tryEventArgs.RunningCard;
                                    try2RCard.ItemCode = tryEventArgs.ItemCode;
                                    try2RCard.OPCode = tryEventArgs.OPCode;
                                    try2RCard.MaintainDate = date;
                                    try2RCard.MaintainTime = time;
                                    try2RCard.MaintainUser = userCode;
                                    try2RCard.EAttribute1 = " ";

                                    tryFacade.AddTry2RCard(try2RCard);

                                    object objectTry = tryFacade.GetTry(TryCode);
                                    if (objectTry != null)
                                    {
                                        ((Try)objectTry).ActualQty += 1;
                                        ((Try)objectTry).Status = TryStatus.STATUS_PRODUCE;
                                        ((Try)objectTry).MaintainDate = date;
                                        ((Try)objectTry).MaintainTime = time;
                                        ((Try)objectTry).MaintainUser = userCode;
                                        if (tryEventArgs.ForLinkLot)
                                        {
                                            ((Try)objectTry).LinkLot = "Y";
                                        }
                                        else
                                        {
                                            if (((Try)objectTry).LinkLot != "Y")
                                            {
                                                ((Try)objectTry).LinkLot = "N";
                                            }

                                        }

                                        if (string.IsNullOrEmpty(((Try)objectTry).LinkLot))
                                        {
                                            ((Try)objectTry).LinkLot = "N";
                                        }

                                        tryFacade.UpdateTry((Try)objectTry);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        object[] try2RCardList = tryFacade.QueryTry2RCard(tryCode, mCard, mItemCode);
                        if (try2RCardList != null)
                        {
                            foreach (Try2RCard try2RCard in try2RCardList)
                            {
                                Try2RCard try2RCardParent = (Try2RCard)tryFacade.GetTry2RCard(try2RCard.TryCode, rCard, itemCode);
                                if (try2RCardParent == null)
                                {
                                    try2RCardParent = tryFacade.CreateNewTry2RCard();

                                    try2RCardParent.TryCode = try2RCard.TryCode;
                                    try2RCardParent.RCard = rCard;
                                    try2RCardParent.ItemCode = itemCode;
                                    try2RCardParent.OPCode = tryEventArgs.OPCode;
                                    try2RCardParent.MaintainDate = date;
                                    try2RCardParent.MaintainTime = time;
                                    try2RCardParent.MaintainUser = userCode;
                                    try2RCardParent.EAttribute1 = " ";

                                    tryFacade.AddTry2RCard(try2RCardParent);

                                    object objectTry = tryFacade.GetTry(try2RCard.TryCode);
                                    if (objectTry != null)
                                    {
                                        //((Try)objectTry).ActualQty += 1;
                                        ////((Try)objectTry).Status = TryStatus.STATUS_PRODUCE;
                                        //((Try)objectTry).MaintainDate = date;
                                        //((Try)objectTry).MaintainTime = time;
                                        //((Try)objectTry).MaintainUser = userCode;
                                        ////隐形的LINKLOT不改变 changed by hiro 2009/01/19
                                        ////if (tryEventArgs.ForLinkLot)
                                        ////{
                                        ////    ((Try)objectTry).LinkLot = "Y";
                                        ////}
                                        ////else
                                        ////{
                                        ////    if (((Try)objectTry).LinkLot != "Y")
                                        ////    {
                                        ////        ((Try)objectTry).LinkLot = "N";
                                        ////    }
                                        ////}

                                        ////if (string.IsNullOrEmpty(((Try)objectTry).LinkLot))
                                        ////{
                                        ////    ((Try)objectTry).LinkLot = "N";
                                        ////}
                                        //tryFacade.UpdateTry((Try)objectTry);
                                    }
                                }
                            }
                        }
                    }
                }
                //用于拆解
                else
                {
                    object[] try2RCardList = tryFacade.QueryTry2RCard(tryCode, mCard, mItemCode);

                    if (try2RCardList != null)
                    {
                        foreach (Try2RCard try2RCard in try2RCardList)
                        {
                            Try2RCard try2RCardParent = (Try2RCard)tryFacade.GetTry2RCard(try2RCard.TryCode, rCard, itemCode);

                            if (try2RCardParent != null)
                            {
                                Try objectTry = (Try)tryFacade.GetTry(try2RCard.TryCode);
                                if (objectTry != null)
                                {
                                    objectTry.ActualQty -= 1;
                                    objectTry.MaintainDate = date;
                                    objectTry.MaintainTime = time;
                                    objectTry.MaintainUser = userCode;

                                    tryFacade.UpdateTry(objectTry);
                                }

                                tryFacade.DeleteTry2RCard(try2RCardParent);
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

        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            return messages;
        }
    }

}
