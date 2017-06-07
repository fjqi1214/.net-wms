using System;
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

namespace BenQGuru.eMES.DataCollect.Action
{
	/// <summary>
	/// ActionUndoNG 的摘要说明。
	/// </summary>
	public class ActionUndoNG
	{
		private IDomainDataProvider _domainDataProvider = null;

		public ActionUndoNG(IDomainDataProvider domainDataProvider)
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

		/// <summary>
		/// 采集NG后，可以再次测试采集，这里做自动Undo
		/// </summary>
		public Messages UndoNG(ActionEventArgs actionEventArgs)
		{
			Messages msg = new Messages();
			// 是否配置需要Undo
			if (System.Configuration.ConfigurationSettings.AppSettings["UndoNG"] != "1")
				return msg;
			bool bNeedUndo = false;
			BenQGuru.eMES.TS.TSFacade tsFacade = null;
			BenQGuru.eMES.Domain.TS.TS ts = null;
			object objTmp;
			if (actionEventArgs.ProductInfo != null && actionEventArgs.ProductInfo.LastSimulation != null)
			{
				// 上次采NG的才可以Undo
				if (actionEventArgs.ProductInfo.LastSimulation.ProductStatus == ProductStatus.NG)
				{
					// 产品是否还在本OP
					BenQGuru.eMES.BaseSetting.BaseModelFacade modelFacade = new BaseModelFacade(this.DataProvider);
					objTmp = modelFacade.GetOperation2Resource(actionEventArgs.ProductInfo.LastSimulation.OPCode, actionEventArgs.ResourceCode);
					if (objTmp == null)
						return msg;
					// 是否已在做维修
					tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider);
					ts = (BenQGuru.eMES.Domain.TS.TS)tsFacade.GetCardLastTSRecord(actionEventArgs.ProductInfo.LastSimulation.RunningCard);
					if (ts == null || ts.TSStatus != TSStatus.TSStatus_New)
					{
						return msg;
					}
					bNeedUndo = true;
				}
			}
			if (bNeedUndo == false)
				return msg;
			// 开始Undo
			// 查询OnWIP
			Simulation simulation = actionEventArgs.ProductInfo.LastSimulation;
			string strSql = "SELECT * FROM tblOnWIP WHERE RCard='" + simulation.RunningCard + "' AND MOCode='" + simulation.MOCode + "' ORDER BY RCardSeq ";
			object[] objsWip = this.DataProvider.CustomQuery(typeof(OnWIP), new SQLCondition(strSql));
			if (objsWip == null || objsWip.Length < 2)
				return msg;
			OnWIP wip = (OnWIP)objsWip[objsWip.Length - 1];
			BenQGuru.eMES.Report.ReportFacade reportFacade = new BenQGuru.eMES.Report.ReportFacade(this.DataProvider);
			DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

			#region 更新TS相关
			/*	不用再清除不良信息
			// 查询TBLTSERRORCODE
			strSql = "SELECT * FROM TBLTSERRORCODE WHERE TSID='" + ts.TSId + "' ";
			object[] objsErrorCode = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TS.TSErrorCode), new SQLCondition(strSql));
			string strErrorCodeList = string.Empty;
			if (objsErrorCode != null)
			{
				// 更新TBLRPTRESECG
				for (int i = 0; i < objsErrorCode.Length; i++)
				{
					BenQGuru.eMES.Domain.TS.TSErrorCode errorCode = (BenQGuru.eMES.Domain.TS.TSErrorCode)objsErrorCode[i];
					objTmp = reportFacade.GetReportErrorCode2Resource(wip.ModelCode, wip.ItemCode, wip.MOCode, wip.ShiftDay, wip.ShiftCode, wip.TimePeriodCode, wip.OPCode, wip.ResourceCode, wip.SegmentCode, wip.StepSequenceCode, errorCode.ErrorCodeGroup, errorCode.ErrorCode);
					if (objTmp != null)
					{
						BenQGuru.eMES.Domain.Report.ReportErrorCode2Resource rptError = (BenQGuru.eMES.Domain.Report.ReportErrorCode2Resource)objTmp;
						if (rptError.NGTimes == 1)
						{
							reportFacade.DeleteReportErrorCode2Resource(rptError);
						}
						else
						{
							rptError.NGTimes = rptError.NGTimes - 1;
							reportFacade.UpdateReportErrorCode2Resource(rptError);
						}
					}
					strErrorCodeList = strErrorCodeList + errorCode.ErrorCodeGroup + ":" + errorCode.ErrorCode + ";";
				}
				// 删除TBLTSERRORCODE
				if (objsErrorCode.Length == 1)
					tsFacade.DeleteTSErrorCode((BenQGuru.eMES.Domain.TS.TSErrorCode)objsErrorCode[0]);
				else
				{
					strSql = "DELETE FROM TBLTSERRORCODE WHERE TSID='" + ts.TSId + "' ";
					this.DataProvider.CustomExecute(new SQLCondition(strSql));
				}
			}
			// 删除TS
			tsFacade.DeleteTS(ts);
			*/
			// 更新TS
			ts.TSStatus = TSStatus.TSStatus_RepeatNG;	// 不良品重复测试
			ts.ReflowMOCode = simulation.MOCode;
			ts.ReflowOPCode = simulation.OPCode;
			ts.ReflowRouteCode = simulation.RouteCode;
			tsFacade.UpdateTS(ts);
			#endregion

			// 更新报表
			//UndoNGReport(simulation, wip, true, strErrorCodeList, (OnWIP)objsWip[objsWip.Length - 2]);
			if (System.Configuration.ConfigurationSettings.AppSettings["UndoNGUndoReport"] == "1")
			{
				// 查询TBLTSERRORCODE
				strSql = "SELECT * FROM TBLTSERRORCODE WHERE TSID='" + ts.TSId + "' ";
				object[] objsErrorCode = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TS.TSErrorCode), new SQLCondition(strSql));
				string strErrorCodeList = string.Empty;
				if (objsErrorCode != null)
				{
					// 更新TBLRPTRESECG
					for (int i = 0; i < objsErrorCode.Length; i++)
					{
						BenQGuru.eMES.Domain.TS.TSErrorCode errorCode = (BenQGuru.eMES.Domain.TS.TSErrorCode)objsErrorCode[i];
						strErrorCodeList = strErrorCodeList + errorCode.ErrorCodeGroup + ":" + errorCode.ErrorCode + ";";
					}
				}
				
				//UndoNGReport(simulation, wip, true, strErrorCodeList, (OnWIP)objsWip[objsWip.Length - 2]);
			}

			// 更新Simulation
			UndoNGSimulation(simulation, true, (OnWIP)objsWip[objsWip.Length - 2]);
			//UpdateUndoWIP((OnWIP)objsWip[objsWip.Length - 1]);		// 保留测试信息
			
			// 如果上次采集是GOOD->NG，这次采GOOD，那还需要Undo再上次的GOOD
			wip = (OnWIP)objsWip[objsWip.Length - 2];
			if (wip.Action == ActionType.DataCollectAction_GOOD 
				&& wip.OPCode == simulation.OPCode)
			{
				if (System.Configuration.ConfigurationSettings.AppSettings["UndoNGUndoReport"] == "1")
				{
					// 更新报表
                    //UndoNGReport(simulation, wip, false, string.Empty, (OnWIP)objsWip[objsWip.Length - 3]);
				}
				// 更新Simulation
				UndoNGSimulation(simulation, false, (OnWIP)objsWip[objsWip.Length - 3]);
				//UpdateUndoWIP((OnWIP)objsWip[objsWip.Length - 2]);		// 保留测试信息
			}

			ActionOnLineHelper onlineHelper = new ActionOnLineHelper(this.DataProvider);
			Messages msgTmp = onlineHelper.GetIDInfo(simulation.RunningCard);
			actionEventArgs.ProductInfo = (ProductInfo)msgTmp.GetData().Values[0];
			
			return msg;
		}
		/// <summary>
		/// Undo时更新报表数据
		/// </summary>
		private void UndoNGReport(Simulation simulation, OnWIP wip, bool isNG, string errorCodeList, OnWIP prevWip)
		{
			BenQGuru.eMES.Report.ReportFacade reportFacade = new BenQGuru.eMES.Report.ReportFacade(this.DataProvider);
			// 更新TBLRPTHISOPQTY
			string qtyFlag = "N";
			object objTmp = reportFacade.GetReportHistoryOPQty(wip.ModelCode, wip.ShiftDay, wip.MOCode, wip.TimePeriodCode, wip.StepSequenceCode, wip.SegmentCode, wip.ItemCode, wip.ShiftCode, wip.OPCode, wip.ResourceCode, "N");
			if (objTmp == null)
			{
				objTmp = reportFacade.GetReportHistoryOPQty(wip.ModelCode, wip.ShiftDay, wip.MOCode, wip.TimePeriodCode, wip.StepSequenceCode, wip.SegmentCode, wip.ItemCode, wip.ShiftCode, wip.OPCode, wip.ResourceCode, "Y");
				qtyFlag = "Y";
			}
			if (objTmp != null)
			{
				BenQGuru.eMES.Domain.Report.ReportHistoryOPQty hisQty = (BenQGuru.eMES.Domain.Report.ReportHistoryOPQty)objTmp;
				// 如果上次是NG
				if (isNG == true)
				{
					// NG次数减1
					hisQty.NGTimes = hisQty.NGTimes - 1 * simulation.IDMergeRule;
					// 更新不良代码列表
					if (hisQty.ErrorGroup2Err.Length >= errorCodeList.Length && errorCodeList != string.Empty)
					{
						hisQty.ErrorGroup2Err = hisQty.ErrorGroup2Err.Substring(0, hisQty.ErrorGroup2Err.Length - errorCodeList.Length);
					}
				}
				else	// 如果上次是GOOD
				{
					// 如果是Undo GOOD，那OutputQty要不要减 1 ？
					// 由于Undo GOOD之前一定做过Undo NG，所以OutputQty不用减 1
					// 也就是这个OP上最后采集的结果是NG，本来就没有记OutputQty
					//hisQty.OuputQty = hisQty.OuputQty - 1;
				}
				if (prevWip.Action == ActionType.DataCollectAction_GoMO)
				{
					// 减去OP采集数
					hisQty.EAttribute2 = hisQty.EAttribute2 - 1 * simulation.IDMergeRule;
				}
				this.DataProvider.Update(hisQty);
			}
			// 更新tblrptreallineecqty
			if (isNG == true)
			{
				string[] errorCodes = errorCodeList.Split(';');
				for (int i = 0; i < errorCodes.Length; i++)
				{
					if (errorCodes[i] == string.Empty)
						continue;
					string[] errorCode = errorCodes[i].Split(':');
					BenQGuru.eMES.Domain.Report.ReportRealtimeLineErrorCodeQty rptErrorCode = 
						(BenQGuru.eMES.Domain.Report.ReportRealtimeLineErrorCodeQty)reportFacade.GetReportRealtimeLineErrorCodeQty(wip.ModelCode, wip.ShiftDay, wip.MOCode, wip.TimePeriodCode, wip.StepSequenceCode, wip.SegmentCode, wip.ItemCode, wip.ShiftCode, errorCode[1], errorCode[0]);
					if (rptErrorCode != null)
					{
						if (rptErrorCode.ErrorCodeTimes <= 1)
							reportFacade.DeleteReportRealtimeLineErrorCodeQty(rptErrorCode);
						else
						{
							string strSql = "UPDATE tblrptreallineecqty SET ECTimes=ECTimes-1 WHERE ModelCode='" + wip.ModelCode + "' AND ShiftDay=" + wip.ShiftDay.ToString() +
								" AND MOCode='" + wip.MOCode + "' AND TPCode='" + wip.TimePeriodCode + "' AND SSCode='" + wip.StepSequenceCode + "' AND SegCode='" + wip.SegmentCode + "' AND ItemCode='" + wip.ItemCode + "' AND ShiftCode='" + wip.ShiftCode + "' " +
								" AND ECODE='" + errorCode[1] + "' AND ECGCODE='" + errorCode[0] + "' ";
							this.DataProvider.CustomExecute(new SQLCondition(strSql));
						}
					}
				}
			}
			// 更新TBLRPTREALLINEQTY
			string updateField = string.Empty;
			// 如果上次是NG
			if (isNG == true)
			{
				// NG次数减1
				updateField = "NGTimes=NGTimes-" + (1 * simulation.IDMergeRule).ToString();
			}
			else
			{
				// 如果完工，则更新完工数量
				if (FormatHelper.StringToBoolean(simulation.IsComplete) == true)
				{
					updateField = "OutputQty=OutputQty-" + (1 * simulation.IDMergeRule).ToString();
					if (simulation.NGTimes == 0)
					{
						BenQGuru.eMES.Rework.ReworkFacade reworkFacade = new BenQGuru.eMES.Rework.ReworkFacade(this.DataProvider);
						if (reworkFacade.GetRejectCountByMO(simulation.RunningCard, simulation.MOCode) == 0)
						{
							updateField += "AllGoodQty=AllGoodQty-" + (1 * simulation.IDMergeRule).ToString();
						}
					}
					// 更新工单产出
					string strSql = "UPDATE tblMO SET MOActQty=MOActQty-" + (1 * simulation.IDMergeRule).ToString() + " WHERE MOCode='" + simulation.MOCode + "' ";
					this.DataProvider.CustomExecute(new SQLCondition(strSql));
				}
			}
			if (updateField != string.Empty)
			{
				string strSql = "UPDATE TBLRPTREALLINEQTY SET " + updateField + " WHERE ModelCode='" + wip.ModelCode + "' AND ShiftDay=" + wip.ShiftDay.ToString() +
					" AND MOCode='" + wip.MOCode + "' AND TPCode='" + wip.TimePeriodCode + "' AND SSCode='" + wip.StepSequenceCode + "' AND SegCode='" + wip.SegmentCode + "' AND ItemCode='" + wip.ItemCode + "' AND ShiftCode='" + wip.ShiftCode + "' " +
					" AND QtyFlag='" + qtyFlag + "' ";
				this.DataProvider.CustomExecute(new SQLCondition(strSql));
			}
		}
		/// <summary>
		/// Undo时更新Simulation数据
		/// </summary>
		private void UndoNGSimulation(Simulation simulation, bool isNG, OnWIP wip)
		{
			// 更新Simulation
			DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
			string lastAction = wip.Action;
			simulation.LastAction = lastAction;
			string[] actionList = simulation.ActionList.Split(';');
			simulation.ActionList = string.Join(";", actionList, 0, actionList.Length - 2) + ";";
			simulation.ProductStatus = wip.ActionResult;
			if (isNG == true)
				simulation.NGTimes = simulation.NGTimes - 1;
			simulation.IsComplete = FormatHelper.BooleanToString(false);
			//simulation.RunningCardSequence = wip.RunningCardSequence;		// 保留测试信息
			dataCollectFacade.UpdateSimulation(simulation);
			// 更新SimulationReport
			SimulationReport simulationReport = (SimulationReport)dataCollectFacade.GetLastSimulationReport(simulation.RunningCard);
			simulationReport.LastAction = lastAction;
			simulationReport.Status = wip.ActionResult;
			if (isNG == true)
				simulationReport.NGTimes = simulationReport.NGTimes - 1;
			simulationReport.IsComplete = FormatHelper.BooleanToString(false);
			//simulationReport.RunningCardSequence = wip.RunningCardSequence;		// 保留测试信息
			dataCollectFacade.UpdateSimulationReport(simulationReport);
		}
		/// <summary>
		/// 更新Undo的WIP
		/// </summary>
		/// <param name="wip"></param>
		private void UpdateUndoWIP(OnWIP wip)
		{
			string strSql = "INSERT INTO tblOnWIPUndo SELECT * FROM tblOnWIP WHERE MOCode='" + wip.MOCode + "' AND RCard='" + wip.RunningCard + "' AND RCardSeq=" + wip.RunningCardSequence.ToString();
			this.DataProvider.CustomExecute(new SQLCondition(strSql));
			this.DataProvider.Delete(wip);
		}
		
	}
}
