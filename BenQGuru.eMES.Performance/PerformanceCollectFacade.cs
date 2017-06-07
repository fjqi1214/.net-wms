using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Performance
{
    public class PerformanceCollectFacade : MarshalByRefObject
    {
        private FacadeHelper _helper = null;
        private IDomainDataProvider _domainDataProvider = null;

        public override object InitializeLifetimeService()
        {
            return null;
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

        public PerformanceCollectFacade()
        {
        }

        public PerformanceCollectFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public Messages PerformanceCollect(OnWIP onWIP)
        {
            Messages returnValue = new Messages();

            try
            {
                //检查
                if (returnValue.IsSuccess())
                {
                    returnValue.AddMessages(CheckBeforePerformanceCollect(onWIP));
                }

                //获取环境信息
                DBDateTime nowDateTime = new DBDateTime(FormatHelper.ToDateTime(onWIP.MaintainDate, onWIP.MaintainTime));
                OnOffPostEnvirenment nowEnv = new OnOffPostEnvirenment();
                DBDateTime lastSecondDateTime = new DBDateTime(FormatHelper.ToDateTime(onWIP.MaintainDate, onWIP.MaintainTime).AddSeconds(-1));
                OnOffPostEnvirenment lastSecondEnv = new OnOffPostEnvirenment();
                if (returnValue.IsSuccess())
                {
                    if (!nowEnv.InitWithoutResAndOP(this.DataProvider, onWIP.StepSequenceCode, nowDateTime))
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_CannotGetEnvironmentInfo"));
                    }
                }
                if (returnValue.IsSuccess())
                {
                    if (!lastSecondEnv.InitWithoutResAndOP(this.DataProvider, onWIP.StepSequenceCode, lastSecondDateTime))
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_CannotGetEnvironmentInfo"));
                    }
                }

                //InputTimes+1之后的绩效采集
                if (returnValue.IsSuccess())
                {
                    returnValue.AddMessages(CollectAfterInputTimesAdded(onWIP, nowEnv, lastSecondEnv));
                }

                //Output工序的绩效采集
                if (returnValue.IsSuccess())
                {
                    returnValue.AddMessages(CollectAtLineOutputOP(onWIP, nowEnv, lastSecondEnv));
                }
            }
            catch (Exception ex)
            {
                returnValue.Add(new UserControl.Message(ex));
            }

            return returnValue;
        }

        private Messages CheckBeforePerformanceCollect(OnWIP onWIP)
        {
            Messages returnValue = new Messages();

            PerformanceFacade performanceFacade = new PerformanceFacade(this.DataProvider);

            //检查是否有上岗人员
            if (returnValue.IsSuccess())
            {
                int manCount = performanceFacade.QueryLine2ManDetailCount(string.Empty, onWIP.StepSequenceCode, string.Empty, string.Empty, 0, string.Empty, performanceFacade.GetLine2ManDetailStatusList(true));
                if (manCount <= 0)
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_NoManCountOnLine"));
                }
            }

            return returnValue;
        }

        private Messages CollectAfterInputTimesAdded(OnWIP onWIP, OnOffPostEnvirenment nowEnv, OnOffPostEnvirenment lastSecondEnv)
        {
            Messages returnValue = new Messages();

            PerformanceFacade performanceFacade = new PerformanceFacade(this.DataProvider);

            //获取是否InputTimes需要加一：Job中的逻辑如下
            //IF INSTR(v_ActionList, '|' || v_ONWIPData.action || '|') > 0 THEN
            //    v_RPTOPQTY.inputtimes := v_RPTOPQTY.inputtimes + 1;
            //END IF;
            string validActionList = "|BURNIN|BURNOUT|CARTON|GOOD|NG|SMTGOOD|SMTNG|";
            bool inputTimesAdded = validActionList.IndexOf("|" + onWIP.Action + "|") >= 0;

            if (inputTimesAdded)
            {
                object[] line2ManDetailArray = performanceFacade.QueryUserCurrentLine2ManDetail(string.Empty, onWIP.StepSequenceCode, onWIP.ResourceCode, onWIP.OPCode, 0, string.Empty, performanceFacade.GetLine2ManDetailStatusList(true), true);
                if (line2ManDetailArray != null && line2ManDetailArray.Length > 0)
                {
                    foreach (Line2ManDetail line2ManDetail in line2ManDetailArray)
                    {
                        //是否跨Shift
                        if (line2ManDetail.ShiftDate == nowEnv.ShiftDate && line2ManDetail.ShiftCode == nowEnv.Shift.ShiftCode)
                        {
                            //是否变工单
                            if (string.Compare(line2ManDetail.MOCode, onWIP.MOCode, true) == 0)
                            {
                                line2ManDetail.ManActQty++;
                                line2ManDetail.MaintainUser = onWIP.MaintainUser;
                                performanceFacade.UpdateLine2ManDetail(line2ManDetail);
                            }
                            else
                            {
                                //是否第一笔工单
                                if (string.Compare(line2ManDetail.MOCode, " ", true) == 0)
                                {
                                    line2ManDetail.MOCode = onWIP.MOCode;
                                    line2ManDetail.ManActQty++;
                                    line2ManDetail.MaintainUser = onWIP.MaintainUser;
                                    performanceFacade.UpdateLine2ManDetail(line2ManDetail);
                                }
                                else
                                {
                                    line2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_AutoOff;
                                    line2ManDetail.OffDate = lastSecondEnv.DBDateTime.DBDate;
                                    line2ManDetail.OffTime = lastSecondEnv.DBDateTime.DBTime;
                                    line2ManDetail.Duration = FormatHelper.GetSpanSeconds(line2ManDetail.OnDate, line2ManDetail.OnTime, line2ManDetail.OffDate, line2ManDetail.OffTime);
                                    line2ManDetail.MaintainUser = onWIP.MaintainUser;
                                    performanceFacade.UpdateLine2ManDetail(line2ManDetail);

                                    Line2ManDetail newLine2ManDetail = performanceFacade.CreateNewLine2ManDetail();
                                    newLine2ManDetail.UserCode = line2ManDetail.UserCode;
                                    newLine2ManDetail.OPCode = onWIP.OPCode;
                                    newLine2ManDetail.ResourceCode = onWIP.ResourceCode;
                                    newLine2ManDetail.SSCode = onWIP.StepSequenceCode;
                                    newLine2ManDetail.ShiftDate = nowEnv.ShiftDate;
                                    newLine2ManDetail.ShiftCode = nowEnv.Shift.ShiftCode;
                                    newLine2ManDetail.OnDate = nowEnv.DBDateTime.DBDate;
                                    newLine2ManDetail.OnTime = nowEnv.DBDateTime.DBTime;
                                    newLine2ManDetail.OffDate = nowEnv.GetShiftEndDate();
                                    newLine2ManDetail.OffTime = nowEnv.Shift.ShiftEndTime;
                                    newLine2ManDetail.Duration = FormatHelper.GetSpanSeconds(newLine2ManDetail.OnDate, newLine2ManDetail.OnTime, newLine2ManDetail.OffDate, newLine2ManDetail.OffTime);
                                    newLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_AutoOn;
                                    newLine2ManDetail.MOCode = onWIP.MOCode;
                                    newLine2ManDetail.ManActQty = 1;
                                    newLine2ManDetail.MaintainUser = onWIP.MaintainUser;
                                    performanceFacade.AddLine2ManDetail(newLine2ManDetail);
                                }
                            }
                        }
                        else
                        {
                            line2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_AutoOff;
                            line2ManDetail.MaintainUser = onWIP.MaintainUser;
                            performanceFacade.UpdateLine2ManDetail(line2ManDetail);

                            Line2ManDetail newLine2ManDetail = performanceFacade.CreateNewLine2ManDetail();
                            newLine2ManDetail.UserCode = line2ManDetail.UserCode;
                            newLine2ManDetail.OPCode = onWIP.OPCode;
                            newLine2ManDetail.ResourceCode = onWIP.ResourceCode;
                            newLine2ManDetail.SSCode = onWIP.StepSequenceCode;
                            newLine2ManDetail.ShiftDate = nowEnv.ShiftDate;
                            newLine2ManDetail.ShiftCode = nowEnv.Shift.ShiftCode;
                            newLine2ManDetail.OnDate = nowEnv.GetShiftBeginDate();
                            newLine2ManDetail.OnTime = nowEnv.Shift.ShiftBeginTime;
                            newLine2ManDetail.OffDate = nowEnv.GetShiftEndDate();
                            newLine2ManDetail.OffTime = nowEnv.Shift.ShiftEndTime;
                            newLine2ManDetail.Duration = FormatHelper.GetSpanSeconds(newLine2ManDetail.OnDate, newLine2ManDetail.OnTime, newLine2ManDetail.OffDate, newLine2ManDetail.OffTime);
                            newLine2ManDetail.Status = Line2ManDetailStatus.Line2ManDetailStatus_AutoOn;
                            newLine2ManDetail.MOCode = onWIP.MOCode;
                            newLine2ManDetail.ManActQty = 1;
                            newLine2ManDetail.MaintainUser = onWIP.MaintainUser;
                            performanceFacade.AddLine2ManDetail(newLine2ManDetail);
                        }
                    }
                }
            }

            return returnValue;
        }

        private Messages CollectAtLineOutputOP(OnWIP onWIP, OnOffPostEnvirenment nowEnv, OnOffPostEnvirenment lastSecondEnv)
        {
            Messages returnValue = new Messages();

            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            PerformanceFacade performanceFacade = new PerformanceFacade(this.DataProvider);

            //获取是否LineOutputCount需要加一：仅判断是否为产量工序            
            ItemRoute2OP itemRoute2OP = (ItemRoute2OP)itemFacade.GetItemRoute2OP(onWIP.ItemCode, onWIP.RouteCode, onWIP.OPCode);
            bool lineOutpitCountAdded = false;
            if (itemRoute2OP != null && itemRoute2OP.OPControl.Substring(10, 1) == "1")
            {
                lineOutpitCountAdded = true;
            }

            if (lineOutpitCountAdded)
            {
                ProduceDetail produceDetail = (ProduceDetail)performanceFacade.GetLatestProduceDetail(onWIP.StepSequenceCode, 0, string.Empty, true);

                if (produceDetail == null)
                {
                    NewProduceDetail(onWIP, nowEnv, performanceFacade);
                }
                else
                {
                    if (produceDetail.ShiftDate == nowEnv.ShiftDate && produceDetail.ShiftCode == nowEnv.Shift.ShiftCode)
                    {
                        if (string.Compare(produceDetail.MOCode, onWIP.MOCode, true) != 0)
                        {
                            if (produceDetail.Status == ProduceDetailStatus.ProduceDetailStatus_Open)
                            {
                                produceDetail.EndDate = lastSecondEnv.DBDateTime.DBDate;
                                produceDetail.EndTime = lastSecondEnv.DBDateTime.DBTime;
                                produceDetail.Duration = FormatHelper.GetSpanSeconds(produceDetail.BeginDate, produceDetail.BeginTime, produceDetail.EndDate, produceDetail.EndTime);
                                produceDetail.Status = ProduceDetailStatus.ProduceDetailStatus_Close;
                                produceDetail.MaintainUser = onWIP.MaintainUser;
                                performanceFacade.UpdateProduceDetail(produceDetail);
                            }

                            int manCount = performanceFacade.QueryLine2ManDetailCount(string.Empty, nowEnv.StepSequence.StepSequenceCode, string.Empty, string.Empty, 0, string.Empty, performanceFacade.GetLine2ManDetailStatusList(true));

                            ProduceDetail newProduceDetail = performanceFacade.CreateNewProduceDetail();
                            newProduceDetail.SSCode = onWIP.StepSequenceCode;
                            newProduceDetail.ShiftDate = nowEnv.ShiftDate;
                            newProduceDetail.ShiftCode = nowEnv.Shift.ShiftCode;
                            newProduceDetail.BeginDate = nowEnv.DBDateTime.DBDate;
                            newProduceDetail.BeginTime = nowEnv.DBDateTime.DBTime;
                            newProduceDetail.EndDate = nowEnv.GetShiftEndDate();
                            newProduceDetail.EndTime = nowEnv.Shift.ShiftEndTime;
                            newProduceDetail.Duration = FormatHelper.GetSpanSeconds(newProduceDetail.BeginDate, newProduceDetail.BeginTime, newProduceDetail.EndDate, newProduceDetail.EndTime);
                            newProduceDetail.Status = ProduceDetailStatus.ProduceDetailStatus_Open;
                            newProduceDetail.ManCount = manCount;
                            newProduceDetail.MOCode = onWIP.MOCode;
                            newProduceDetail.MaintainUser = onWIP.MaintainUser;
                            performanceFacade.AddProduceDetail(newProduceDetail);
                        }
                    }
                    else
                    {
                        if (produceDetail.Status == ProduceDetailStatus.ProduceDetailStatus_Open)
                        {
                            produceDetail.Status = ProduceDetailStatus.ProduceDetailStatus_Close;
                            produceDetail.MaintainUser = onWIP.MaintainUser;
                            performanceFacade.UpdateProduceDetail(produceDetail);
                        }

                        NewProduceDetail(onWIP, nowEnv, performanceFacade);
                    }
                }
            }

            return returnValue;
        }

        private void NewProduceDetail(OnWIP onWIP, OnOffPostEnvirenment nowEnv, PerformanceFacade performanceFacade)
        {
            int manCount = performanceFacade.QueryLine2ManDetailCount(string.Empty, nowEnv.StepSequence.StepSequenceCode, string.Empty, string.Empty, 0, string.Empty, performanceFacade.GetLine2ManDetailStatusList(true));

            int shiftBeginDate = nowEnv.GetShiftBeginDate();
            int shiftBeginTime = nowEnv.Shift.ShiftBeginTime;            

            LinePause linePause = (LinePause)performanceFacade.GetLatestLinePause(nowEnv.StepSequence.StepSequenceCode);
            if (linePause != null && linePause.EndDate == shiftBeginDate && linePause.EndTime > shiftBeginTime)
            {
                shiftBeginTime = FormatHelper.TOTimeInt(FormatHelper.ToDateTime(linePause.EndDate, linePause.EndTime).AddSeconds(1));
            }

            ProduceDetail newProduceDetail = performanceFacade.CreateNewProduceDetail();
            newProduceDetail.SSCode = nowEnv.StepSequence.StepSequenceCode;
            newProduceDetail.ShiftDate = nowEnv.ShiftDate;
            newProduceDetail.ShiftCode = nowEnv.Shift.ShiftCode;
            newProduceDetail.BeginDate = shiftBeginDate;
            newProduceDetail.BeginTime = shiftBeginTime;
            newProduceDetail.EndDate = nowEnv.GetShiftEndDate();
            newProduceDetail.EndTime = nowEnv.Shift.ShiftEndTime;
            newProduceDetail.Duration = FormatHelper.GetSpanSeconds(newProduceDetail.BeginDate, newProduceDetail.BeginTime, newProduceDetail.EndDate, newProduceDetail.EndTime);
            newProduceDetail.Status = ProduceDetailStatus.ProduceDetailStatus_Open;
            newProduceDetail.ManCount = manCount;
            newProduceDetail.MOCode = onWIP.MOCode;
            newProduceDetail.MaintainUser = onWIP.MaintainUser;
            performanceFacade.AddProduceDetail(newProduceDetail);
        }
    }
}
