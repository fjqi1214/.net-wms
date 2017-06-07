using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.BaseSetting;
using System.Net;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class InventoryTransfer : ICommand
    {

        public InventoryTransferArgument m_Argument = null;

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

        public void Receive(INVReceive_REQ request)
        {
            ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            try
            {
                TransferFacade transferFacade = new TransferFacade(this.DataProvider);
                SAPDataTransferLog log;
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                string xmlFilePath;
                if (request.TransactionCode == null || request.TransactionCode == string.Empty)
                {
                    // New Log
                    log = transferFacade.CreateNewSAPDataTransferLog();
                    //log.ErrorMessage
                    //log.FinishedDate
                    //log.FinishedTime
                    log.JobID = TransferFacade.InvertoryJobID;
                    log.OrganizationID = 2000;
                    log.ReceivedRecordCount = (request.INVList == null || request.INVList.Count == 0) ? 0 : request.INVList.Count;
                    log.TransactionCode = TransferFacade.InvertoryJobID + "_"
                                        + currentDateTime.DBDate.ToString() + "_"
                                        + currentDateTime.DBTime.ToString()
                                        + DateTime.Now.Millisecond.ToString();
                    log.TransactionSequence = 1;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(INVReceive_REQ), request);
                    log.RequestContent = xmlFilePath;
                    log.RequestDate = currentDateTime.DBDate;
                    log.RequestTime = currentDateTime.DBTime;
                    //log.Result
                    log.SendRecordCount = 0;
                    transferFacade.AddSAPDataTransferLog(log);

                    request.TransactionCode = log.TransactionCode;
                }
                else
                {
                    // Update log
                    log = transferFacade.GetSAPDataTransferLog(request.TransactionCode, 1) as SAPDataTransferLog;
                    //log.ErrorMessage
                    //log.FinishedDate
                    //log.FinishedTime
                    log.ReceivedRecordCount = (request.INVList == null || request.INVList.Count == 0) ? 0 : request.INVList.Count;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(INVReceive_REQ), request);
                    log.ResponseContent = xmlFilePath;
                    log.ResponseDate = currentDateTime.DBDate;
                    log.ResponseTime = currentDateTime.DBTime;
                    //log.Result
                    transferFacade.UpdateSAPDataTransferLog(log);
                }

                if (string.Compare(request.Flag, "Y", true) == 0)
                {
                    if (request.INVList == null || request.INVList.Count == 0)
                    {
                        log.Result = "Fail";
                        log.ErrorMessage = "Inventory list is null.";
                        log.FinishedDate = 0;
                        log.FinishedTime = 0;

                        transferFacade.UpdateSAPDataTransferLog(log);
                        return;
                    }
                    else
                    {
                        this.DataProvider.BeginTransaction();
                        try
                        {
                            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                            SAPStorageInfo sapStorageInfo;
                            foreach (MES_INV inv in request.INVList)
                            {
                                sapStorageInfo = inventoryFacade.CreateNewSAPStorageInfo();
                                sapStorageInfo.ItemCode = inv.ItemCode;
                                sapStorageInfo.OrgID = inv.OrgID;
                                sapStorageInfo.StorageID = inv.StorageID;
                                sapStorageInfo.CINSMQty = inv.CINSMQTY;
                                sapStorageInfo.CLABSQty = inv.CLABSQTY;
                                sapStorageInfo.CSPEMQty = inv.CSPEMQTY;
                                sapStorageInfo.CUMLQty = inv.CUMLQTY;
                                sapStorageInfo.Division = inv.Division;
                                //sapStorageInfo.ItemDescription = inv.ItemDescription;
                                sapStorageInfo.ItemDescription = "";
                                sapStorageInfo.ItemGrade = inv.ItemGrade;
                                sapStorageInfo.ModelCode = string.Empty;
                                sapStorageInfo.StorageName = inv.StorageName;
                                sapStorageInfo.MaintainDate = currentDateTime.DBDate;
                                sapStorageInfo.MaintainTime = currentDateTime.DBTime;
                                sapStorageInfo.MaintainUser = "SAP";

                                object oldStorageInfo = inventoryFacade.GetSAPStorageInfo(inv.ItemCode.ToUpper(), inv.OrgID, inv.StorageID.ToUpper(), inv.ItemGrade.ToUpper());

                                if (oldStorageInfo == null)
                                {
                                    inventoryFacade.AddSAPStorageInfo(sapStorageInfo);
                                }
                                else
                                {
                                    inventoryFacade.DeleteSAPStorageInfo(oldStorageInfo as SAPStorageInfo);
                                    inventoryFacade.AddSAPStorageInfo(sapStorageInfo);
                                }
                            }

                            object[] queryList = inventoryFacade.QuerySAPStorageQuery(string.Empty, request.TransactionCode);
                            if (queryList != null)
                            {
                                foreach (SAPStorageQuery query in queryList)
                                {
                                    query.Flag = FlagStatus.FlagStatus_SAP;
                                    inventoryFacade.UpdateSAPStorageQuery(query);
                                }
                            }

                            this.DataProvider.CommitTransaction();

                            log.Result = "OK";
                            log.ErrorMessage = "";
                            currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                            log.FinishedDate = currentDateTime.DBDate;
                            log.FinishedTime = currentDateTime.DBTime;

                            transferFacade.UpdateSAPDataTransferLog(log);

                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();

                            log.Result = "Fail";
                            log.ErrorMessage = ex.Message;
                            log.FinishedDate = 0;
                            log.FinishedTime = 0;

                            transferFacade.UpdateSAPDataTransferLog(log);
                        }
                    }
                }
                else
                {
                    log.Result = "Fail";
                    log.ErrorMessage = request.Message;
                    log.FinishedDate = 0;
                    log.FinishedTime = 0;

                    transferFacade.UpdateSAPDataTransferLog(log);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }
        }

        #region ICommand Members

        public ServiceResult Run(RunMethod runMethod)
        {
            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("InventoryTransferConfig") as SAPWebServiceEntity;

            if (webServiceEntity == null)
            {
                return new ServiceResult(false, "没有维护InvertoryTransferConfig对应的Service地址", this.m_Argument.TransactionCode);
            }

            InventoryFacade invFacade = new InventoryFacade(this.DataProvider);

            #region Begin for Prepare input Paremeter

            DT_MES_MATSTARCE_REQ inventoryParameter = new DT_MES_MATSTARCE_REQ();
            object[] sapStorageQueryList = null;

            if (runMethod == RunMethod.Auto)
            {
                sapStorageQueryList = invFacade.QuerySAPStorageQueryNotDealed(1);

                if (sapStorageQueryList == null)
                {
                    return new ServiceResult(true, "", this.m_Argument.TransactionCode);
                }

                SAPStorageQuery query = (SAPStorageQuery)sapStorageQueryList[0];
                inventoryParameter.LGORT = query.StorageID.Split(',');
                inventoryParameter.MATNR = query.ItemCode;
                inventoryParameter.TRANS = this.m_Argument.TransactionCode;
                inventoryParameter.WERKS = query.OrganizationID.Split(',');                
            }
            else if (runMethod == RunMethod.Manually)
            {
                inventoryParameter.LGORT = this.m_Argument.Location;
                inventoryParameter.MATNR = this.m_Argument.MaterialNumber;
                inventoryParameter.TRANS = this.m_Argument.TransactionCode;
                inventoryParameter.WERKS = this.m_Argument.OrgList;
            }

            invFacade.DeleteSAPStorageInfo(inventoryParameter.WERKS, inventoryParameter.LGORT, inventoryParameter.MATNR);

            #endregion

            string xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
            typeof(DT_MES_MATSTARCE_REQ), inventoryParameter);

            #region InventoryLog

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SAPDataTransferLog inventoryLog = new SAPDataTransferLog();
            TransferFacade transferFacade = new TransferFacade(this.DataProvider);

            inventoryLog.JobID = TransferFacade.InvertoryJobID;
            inventoryLog.TransactionCode = inventoryParameter.TRANS;
            inventoryLog.TransactionSequence = 1;
            inventoryLog.RequestDate = dateTime.DBDate;
            inventoryLog.RequestTime = dateTime.DBTime;
            inventoryLog.RequestContent = xmlFilePath;
            inventoryLog.OrganizationID = 2000;
            inventoryLog.SendRecordCount = 1;

            transferFacade.AddSAPDataTransferLog(inventoryLog);

            #endregion

            #region Begin for Calling WebService

            try
            {
                //Delete sapstorageInfo
                //invFacade.DeleteSAPStorageInfoByItemCode(this.m_Argument.MaterialNumber.ToUpper());

                InvertoryServiceClientProxy clientProxy = new InvertoryServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                clientProxy.MI_MES_MATSTARCE_REQ(inventoryParameter);
                clientProxy.Dispose();
                clientProxy = null;
            }
            catch (Exception e)
            {
                inventoryLog.Result = "Fail";
                inventoryLog.ErrorMessage = e.Message;
                transferFacade.UpdateSAPDataTransferLog(inventoryLog);
                return new ServiceResult(false, e.Message, inventoryLog.TransactionCode);
            }

            if (runMethod == RunMethod.Auto && sapStorageQueryList != null)
            {
                try
                {
                    SAPStorageQuery query = (SAPStorageQuery)sapStorageQueryList[0];
                    query.Flag = FlagStatus.FlagStatus_POST;
                    query.TransactionCode = this.m_Argument.TransactionCode;
                    invFacade.UpdateSAPStorageQuery(query);
                }
                catch (Exception ex)
                {
                    return new ServiceResult(false, ex.Message, this.m_Argument.TransactionCode);
                }
            }

            #endregion

            return new ServiceResult(true, "", this.m_Argument.TransactionCode);

        }

        public bool ArgumentValid(ref string returnMessage)
        {
            if (this.m_Argument.OrgList == null || this.m_Argument.OrgList.Length == 0)
            {
                returnMessage = "工厂列表不能为空";
                return false;
            }

            if (this.m_Argument.Location == null || this.m_Argument.Location.Length == 0)
            {
                returnMessage = "库存地点不能为空";
                return false;
            }

            //if (this.m_Argument.MaterialNumber == null || this.m_Argument.MaterialNumber == string.Empty)
            //{
            //    returnMessage = "物料号不能为空";
            //    return false;
            //}

            return true;
        }

        public object GetArguments()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new InventoryTransferArgument(this.DataProvider);
            }

            return m_Argument;
        }

        public object NewTransactionCode()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new InventoryTransferArgument(this.DataProvider);
            }
            else
            {
                this.m_Argument.GenerateNewTransactionCode(this.DataProvider);
            }

            return m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as InventoryTransferArgument;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
    }
}
