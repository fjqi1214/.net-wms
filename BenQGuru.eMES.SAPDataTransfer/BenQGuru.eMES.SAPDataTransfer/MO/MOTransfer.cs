using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Web.Helper;
using System.Diagnostics;
using System.Threading;
using System.Configuration;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using System.Net;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using System.Collections;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MOTransfer : ICommand
    {
        private MOTransferArgument m_Argument = null;
        private IDomainDataProvider _domainDataProvider = null;
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

        public MOTransfer()
        {

        }

        #region ICommand Members

        public ServiceResult Run(RunMethod runMethod)
        {
            /*--------- Get WebService URL and UserName and Password ---------*/
            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("MOHeaderTransferConfig") as SAPWebServiceEntity;
            if (webServiceEntity == null)
            {
                return new ServiceResult(false, "没有维护MOHeaderTransferConfig对应的Service地址", this.m_Argument.TransactionCode);
            }

            #region Begin for Prepare input Paremente
            // Prepare input parameter
            DT_MES_PO_REQ moParameter = new DT_MES_PO_REQ();
            moParameter.Trsaction_Code = this.m_Argument.TransactionCode;
            if (this.m_Argument.MOCode.Trim().Length == 0)
            {
                moParameter.MaintainDate_B = this.m_Argument.MaintainDate_B.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
                moParameter.MaintainDate_E = this.m_Argument.MaintainDate_E.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
                moParameter.OrgID = this.m_Argument.OrgID.ToString();
                moParameter.Mocode = "";
            }
            else
            {
                moParameter.MaintainDate_B = "";
                moParameter.MaintainDate_E = "";
                moParameter.OrgID = "";
                moParameter.Mocode = this.m_Argument.MOCode.ToUpper().Trim();
            }
            #endregion

            // Serialize the Input Parameter
            string xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
                typeof(DT_MES_PO_REQ), moParameter);

            #region For Request Log
            DBDateTime requestDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SAPDataTransferLog log = new SAPDataTransferLog();
            TransferFacade transferFacade = new TransferFacade(this.DataProvider);
            log.JobID = TransferFacade.MOHeaderTransferJobID;
            log.TransactionCode = this.m_Argument.TransactionCode;
            log.TransactionSequence = 1;
            log.RequestDate = requestDateTime.DBDate;
            log.RequestTime = requestDateTime.DBTime;
            log.RequestContent = xmlFilePath;
            log.OrganizationID = this.m_Argument.OrgID;
            log.SendRecordCount = 1;
            transferFacade.AddSAPDataTransferLog(log);
            #endregion

            #region Begin for Calling WebService
            // Call Web Service through MOServiceClientProxy
            DT_MES_PO_RESP returnValue;
            try
            {
                MOServiceClientProxy clientProxy = new MOServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                returnValue = clientProxy.MI_MES_PO_REQ(moParameter);
                clientProxy.Dispose();
                clientProxy = null;

                //Serialize the output Parameter
                xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Response.xml",
                    typeof(DT_MES_PO_RESP), returnValue);

                // Update Log
                DBDateTime responseDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                log.ResponseDate = responseDateTime.DBDate;
                log.ResponseTime = responseDateTime.DBTime;
                log.ResponseContent = xmlFilePath;
                transferFacade.UpdateSAPDataTransferLog(log);
            }
            catch (Exception e)
            {
                log.Result = "Fail";
                log.ErrorMessage = e.Message;
                transferFacade.UpdateSAPDataTransferLog(log);
                return new ServiceResult(false, e.Message, log.TransactionCode);
            }
            #endregion

            // Generate MO Object and Save it
            if (string.Compare(returnValue.FLAG, "Y", true) == 0)
            {
                int moCount = returnValue.POLIST.Length;
                string moList = "";

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                this.DataProvider.BeginTransaction();

                try
                {
                    MOFacade moFacade = new MOFacade(this.DataProvider);
                    ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                    MO mo;
                    DT_MES_PO_RESPPOLIST moObject;
                    DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                    for (int i = 0; i < moCount; i++)
                    {
                        moObject = returnValue.POLIST[i];
                        moList += moObject.MOCODE + "|";

                        if (moFacade.GetMO(moObject.MOCODE) != null)
                        {
                            continue;
                        }

                        mo = moFacade.CreateNewMO();

                        /*---------------------  Web Service 节点数据 ---------------------*/
                        mo.MOCode = moObject.MOCODE;
                        mo.MOType = moObject.MoType;
                        mo.MOPlanQty = decimal.Parse(moObject.MOPlanQty);
                        mo.MOPlanStartDate = int.Parse(moObject.MOPlanstart);
                        mo.MOPlanEndDate = int.Parse(moObject.MOPlanEndDate);
                        mo.ItemCode = InternalVariables.MatchItemCode(moObject.ItemCode);
                        mo.OrganizationID = int.Parse(moObject.ORGID);
                        mo.BOMVersion = moObject.MOBOM;
                        mo.MOOP = moObject.MOOP;
                        mo.EAttribute2 = moObject.StorNo;
                        
                        /*-------------- 固定数据 ----------*/
                        object material = itemFacade.GetMaterial(mo.ItemCode, int.Parse(moObject.ORGID));
                        if (material == null)
                        {
                            mo.MaterialDescription = "";
                        }
                        else
                        {
                            mo.MaterialDescription = (material as BenQGuru.eMES.Domain.MOModel.Material).MaterialDescription;
                        }
                        mo.MOMemo = "";
                        mo.MODescription = "";
                        mo.MOInputQty = 0;
                        mo.MOScrapQty = 0;
                        mo.MOActualQty = 0;
                        mo.MOActualStartDate = 0;
                        mo.MOActualEndDate = 0;
                        mo.Factory = mo.OrganizationID.ToString();
                        mo.OrderSequence = 0;
                        mo.MOUser = "SAP";
                        mo.MODownloadDate = currentDateTime.DBDate;
                        mo.MOStatus = MOManufactureStatus.MOSTATUS_INITIAL;
                        mo.MOVersion = "1.0";
                        mo.IsControlInput = "1";
                        mo.IsBOMPass = "9";
                        mo.IDMergeRule = 1;
                        mo.MaintainUser = "SAP";
                        mo.MOReleaseDate = 0;
                        mo.MOReleaseTime = 0;
                        mo.MOPendingCause = " ";
                        mo.MOImportDate = currentDateTime.DBDate;
                        mo.MOImportTime = currentDateTime.DBTime;
                        mo.CustomerCode = "";
                        mo.CustomerName = "";
                        mo.CustomerOrderNO = "";
                        mo.CustomerItemCode = "";
                        mo.OrderNO = "";
                        mo.EAttribute1 = "";
                        mo.MOOffQty = 0;
                        mo.IsCompareSoft = 0;
                        mo.RMABillCode = "";
                        mo.MOSeq = 0;
                        mo.MOPCBAVersion = "";
                        mo.MOBIOSVersion = "";                        

                        moFacade.AddMO(mo);
                    }

                    // Call MOBOM Logic in one transaction
                    if (moList.Trim().Length > 0)
                    {
                        moList = moList.Substring(0, moList.Length - 1);
                        int requestCount = this.GetRequestCount(moList);
                        MOBOMTransfer moBOMTransfer = new MOBOMTransfer(this.DataProvider);
                        MOBOMTransferArgument moBOMArgument = new MOBOMTransferArgument(this.DataProvider);
                        ServiceResult sr;
                        for (int j = 0; j < requestCount; j++)
                        {
                            moBOMArgument.GenerateNewTransactionCode(this.DataProvider);
                            moBOMArgument.MOCodeList = this.GetMOListPerRequest(moList, j);
                            moBOMTransfer.SetArguments(moBOMArgument);
                            sr = moBOMTransfer.Run(RunMethod.Auto);
                            if (sr.Result == false)
                            {
                                throw new Exception(sr.Message);
                            }
                        }
                    }

                    this.DataProvider.CommitTransaction();
                    log.Result = "OK";
                    log.ErrorMessage = "";
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    // Log
                    log.Result = "Fail";
                    log.ErrorMessage = ex.Message;
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                }

                log.ReceivedRecordCount = moCount;
                DBDateTime finishedDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                log.FinishedDate = finishedDateTime.DBDate;
                log.FinishedTime = finishedDateTime.DBTime;
                transferFacade.UpdateSAPDataTransferLog(log);

                if (log.Result == "OK")
                {
                    return new ServiceResult(true, "", log.TransactionCode);
                }
                else
                {
                    return new ServiceResult(false, log.ErrorMessage, log.TransactionCode);
                }
            }
            else //  status!="OK"
            {
                log.Result = "Fail";
                log.ErrorMessage = returnValue.Message;
                log.ReceivedRecordCount = 0;
                transferFacade.UpdateSAPDataTransferLog(log);
                return new ServiceResult(false, log.ErrorMessage, log.TransactionCode);
            }
        }

        private int GetRequestCount(string moList)
        {
            if (moList.Length == 0)
            {
                return 0;
            }
            int maxMO = InternalVariables.MS_MaxMOPerRequest;
            if (moList.Split('|').Length % maxMO == 0)
            {
                return moList.Split('|').Length / maxMO;
            }
            else
            {
                return moList.Split('|').Length / maxMO + 1;
            }
        }

        private string[] GetMOListPerRequest(string moList, int count)
        {
            string[] mos = moList.Split('|');
            int maxMO = InternalVariables.MS_MaxMOPerRequest;
            if (maxMO >= mos.Length)
            {
                return mos;
            }
            else
            {
                List<string> rr = new List<string>();
                int toIndex = maxMO * count + maxMO;
                if (toIndex > mos.Length)
                {
                    toIndex = mos.Length;
                }
                for (int i = maxMO * count; i < toIndex; i++)
                {
                    rr.Add(mos[i]);
                }
                return rr.ToArray();
            }
        }

        public void Dispose()
        {

        }

        public object GetArguments()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MOTransferArgument(this.DataProvider);
            }
            return this.m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as MOTransferArgument;
        }

        public bool ArgumentValid(ref string returnMessage)
        {
            if (this.m_Argument == null)
            {
                returnMessage = "参数没有维护";
                return false;
            }

            if (this.m_Argument.MOCode == null || this.m_Argument.MOCode.Trim().Length == 0)
            {
                if (this.m_Argument.OrgID <= 0)
                {
                    returnMessage = "组织代码需要输入大于0的值";
                    return false;
                }

                if (this.m_Argument.MaintainDate_B.AddDays(InternalVariables.MS_MaxDaysOfPeriod) < this.m_Argument.MaintainDate_E)
                {
                    returnMessage = "时间区间应该小于" + InternalVariables.MS_MaxDaysOfPeriod.ToString() + "天";
                    return false;
                }
            }

            return true;
        }

        public object NewTransactionCode()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MOTransferArgument(this.DataProvider);
            }
            else
            {
                this.m_Argument.GenerateNewTransactionCode(this.DataProvider);
            }
            return this.m_Argument;
        }

        #endregion
    }
}
