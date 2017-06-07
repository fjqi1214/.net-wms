using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.DataCollect;
using UserControl;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.OQC;

namespace BenQGuru.eMES.DataCollect.Action
{
    public class ActionCartonPack : IAction
    {
        private IDomainDataProvider _domainDataProvider = null;

        public ActionCartonPack(IDomainDataProvider domainDataProvider)
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

        public UserControl.Messages Execute(ActionEventArgs cartonPackEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);

            try
            {
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);           

                messages.AddMessages(dataCollect.CheckID(cartonPackEventArgs));

                if (messages.IsSuccess())
                {
                    if (((CartonPackEventArgs)cartonPackEventArgs).ProductInfo.NowSimulation == null)
                    {
                        throw new Exception("$System_Error");
                    }                    

                    string cartonno = (cartonPackEventArgs as CartonPackEventArgs).CartonNo;
                    string oldCartonNo = cartonPackEventArgs.ProductInfo.NowSimulation.CartonCode;
                    if (cartonno != string.Empty)
                    {
                        cartonPackEventArgs.ProductInfo.NowSimulation.CartonCode = (cartonPackEventArgs as CartonPackEventArgs).CartonNo;
                    }

                    //Laws Lu,2005/08/15,新增	完工逻辑，在其他Check都通过的情况下，所有的RunningCard应该是GOOD状态
                    //暂时不考虑线外工序
                    DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                    if (cartonPackEventArgs.ProductInfo.NowSimulation.RouteCode != "" && dataCollectFacade.OPIsMORouteLastOP(
                        cartonPackEventArgs.ProductInfo.NowSimulation.MOCode
                        , cartonPackEventArgs.ProductInfo.NowSimulation.RouteCode
                        , cartonPackEventArgs.ProductInfo.NowSimulation.OPCode))
                    {
                        cartonPackEventArgs.ProductInfo.NowSimulation.IsComplete = "1";
                        cartonPackEventArgs.ProductInfo.NowSimulation.EAttribute1 = "GOOD";
                        //完工自动入库
                        dataCollectFacade.AutoInventory(cartonPackEventArgs.ProductInfo.NowSimulation, cartonPackEventArgs.UserCode);
                    }
                    //End Laws Lu

                    messages.AddMessages(dataCollect.Execute(cartonPackEventArgs));
                    DBDateTime dbDateTime;
                    if (cartonPackEventArgs.ProductInfo.WorkDateTime != null)
                    {
                        dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                        cartonPackEventArgs.ProductInfo.WorkDateTime = dbDateTime;
                    }
                    else
                    {
                        dbDateTime = cartonPackEventArgs.ProductInfo.WorkDateTime;
                    }
                    if (messages.IsSuccess())
                    {
                        if (cartonno != string.Empty && cartonno != oldCartonNo)
                        {
                            Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(DataProvider);

                            object objCarton = pf.GetCARTONINFO(cartonno);

                            if (objCarton != null)
                            {
                                BenQGuru.eMES.Domain.Package.CARTONINFO carton = objCarton as BenQGuru.eMES.Domain.Package.CARTONINFO;
                                if (carton.CAPACITY == carton.COLLECTED + 1)
                                {
                                    messages.Add(new UserControl.Message(MessageType.Normal, "$CARTON_ALREADY_FULL_PlEASE_CHANGE"));
                                }

                                if (carton.CAPACITY <= carton.COLLECTED)
                                {
                                    messages.Add(new UserControl.Message(MessageType.Error, "$CARTON_ALREADY_FILL_OUT"));
                                }
                                else
                                {
                                    pf.UpdateCollected((carton as BenQGuru.eMES.Domain.Package.CARTONINFO).CARTONNO);
                                }
                            }
                            else if (cartonno != String.Empty)
                            { 
                                object objExistCTN = pf.GetExistCARTONINFO(cartonno);

                                if (objExistCTN != null)
                                {
                                    messages.Add(new UserControl.Message(MessageType.Error, "$CARTON_ALREADY_FULL_PlEASE_CHANGE"));
                                }
                                else
                                {
                                    BenQGuru.eMES.Domain.Package.CARTONINFO carton = new BenQGuru.eMES.Domain.Package.CARTONINFO();

                                    carton.CAPACITY = ((new ItemFacade(DataProvider)).GetItem(cartonPackEventArgs.ProductInfo.NowSimulation.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID) as Item).ItemCartonQty;
                                    carton.COLLECTED = 1 /** oqcLotAddIDEventArgs.ProductInfo.NowSimulation.IDMergeRule*/;
                                    carton.PKCARTONID = System.Guid.NewGuid().ToString().ToUpper();
                                    carton.CARTONNO = cartonno;
                                    carton.MUSER = cartonPackEventArgs.UserCode;
                                    carton.MDATE = dbDateTime.DBDate;
                                    carton.MTIME = dbDateTime.DBTime;
                                    //carton.

                                    //joe song 20060630 Carton Memo
                                    carton.EATTRIBUTE1 = (cartonPackEventArgs as CartonPackEventArgs).CartonMemo;

                                    if (carton.CAPACITY == 0)//Get carton capacity by item
                                    {
                                        messages.Add(new UserControl.Message(MessageType.Error
                                            , "$CS_PLEASE_MAINTEIN_ITEMCARTON $CS_Param_ID =" + cartonPackEventArgs.RunningCard));
                                    }
                                    else
                                    {
                                        if (carton.CAPACITY == carton.COLLECTED)
                                        {
                                            messages.Add(new UserControl.Message(MessageType.Normal, "$CARTON_ALREADY_FULL_PlEASE_CHANGE"));
                                        }

                                        pf.AddCARTONINFO(carton);
                                    }
                                }
                            }

                            // Update lot2card.eattribute1=cartonno
                            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                            oqcFacade.UpdateLot2CardCartonCode(cartonPackEventArgs.ProductInfo.NowSimulation.RunningCard,
                                cartonPackEventArgs.ProductInfo.NowSimulation.MOCode,
                                cartonPackEventArgs.ProductInfo.NowSimulation.ItemCode,
                                cartonno);
                        }
                    }

                    // 产生汇总报表
                    //if (messages.IsSuccess())
                    //{
                    //    ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                    //    messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, cartonPackEventArgs.ActionType, cartonPackEventArgs.ProductInfo));
                    //    messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider
                    //        , cartonPackEventArgs.ActionType, cartonPackEventArgs.ProductInfo));
                    //}
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
