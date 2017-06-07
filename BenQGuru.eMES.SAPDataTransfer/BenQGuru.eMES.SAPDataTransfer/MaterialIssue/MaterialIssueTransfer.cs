using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using System.Net;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MaterialIssueTransfer : ICommand
    {
        private MaterialIssueTransferArgument m_Argument = null;

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

        public void Receive(MATISSUE_REQ request)
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
                    log.JobID = TransferFacade.MaterialIssueJobID;
                    log.OrganizationID = 2000;
                    log.ReceivedRecordCount = (request.IssueList == null || request.IssueList.Count == 0) ? 0 : request.IssueList.Count;
                    log.TransactionCode = TransferFacade.MaterialIssueJobID + "_"
                                        + currentDateTime.DBDate.ToString() + "_"
                                        + currentDateTime.DBTime.ToString()
                                        + DateTime.Now.Millisecond.ToString();
                    log.TransactionSequence = 1;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(MATISSUE_REQ), request);
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
                    log.ReceivedRecordCount = (request.IssueList == null || request.IssueList.Count == 0) ? 0 : request.IssueList.Count;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(MATISSUE_REQ), request);
                    log.ResponseContent = xmlFilePath;
                    log.ResponseDate = currentDateTime.DBDate;
                    log.ResponseTime = currentDateTime.DBTime;
                    //log.Result
                    transferFacade.UpdateSAPDataTransferLog(log);
                }

                if (request.IssueList == null || request.IssueList.Count == 0)
                {
                    log.Result = "Fail";
                    log.ErrorMessage = "Raw Issue list is null.";
                    log.FinishedDate = 0;
                    log.FinishedTime = 0;

                    transferFacade.UpdateSAPDataTransferLog(log);
                    return;
                }
                else
                {
                    MaterialStockFacade msf = new MaterialStockFacade(this.DataProvider);
                    RawIssue4SAP rawIssue4SAP;
                    InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);

                    List<SAPMaterialTrans> confirmSAPMaterialTransList = new List<SAPMaterialTrans>();

                    try
                    {
                        this.DataProvider.BeginTransaction();

                        foreach (MES_MATISSUE matIssue in request.IssueList)
                        {
                            rawIssue4SAP = msf.CreateNewRawIssue4SAP();
                            rawIssue4SAP.ErrorMessage = matIssue.ErrorMessage;
                            rawIssue4SAP.Flag = matIssue.Flag;
                            rawIssue4SAP.MaintainDate = currentDateTime.DBDate;
                            rawIssue4SAP.MaintainTime = currentDateTime.DBTime;
                            rawIssue4SAP.MaintainUser = "SAP";
                            rawIssue4SAP.MaterialDocument = matIssue.MaterialDocument;
                            rawIssue4SAP.MaterialDocumentYear = matIssue.MaterialDocumentYear;
                            rawIssue4SAP.TransactionCode = request.TransactionCode;
                            rawIssue4SAP.VendorCode = matIssue.VendorCode;

                            msf.AddRawIssue4SAP(rawIssue4SAP);

                            if (string.Compare(matIssue.Flag, "Y", true) == 0)  // Post OK
                            {
                                object[] sapMaterialTransList = inventoryFacade.QuerySAPMaterialTrans(request.TransactionCode);

                                if (sapMaterialTransList != null)
                                {
                                    foreach (SAPMaterialTrans sapMaterialTrans in sapMaterialTransList)
                                    {
                                        if (sapMaterialTrans.Flag == FlagStatus.FlagStatus_POST)
                                        {
                                            confirmSAPMaterialTransList.Add(sapMaterialTrans);
                                        }
                                    }
                                }
                            }
                        }

                        this.DataProvider.CommitTransaction();
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

                    try
                    {
                        log.Result = "OK";
                        log.ErrorMessage = "";
                        currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                        log.FinishedDate = currentDateTime.DBDate;
                        log.FinishedTime = currentDateTime.DBTime;

                        transferFacade.UpdateSAPDataTransferLog(log);

                        foreach (SAPMaterialTrans sapMaterialTrans in confirmSAPMaterialTransList)
                        {
                            sapMaterialTrans.Flag = FlagStatus.FlagStatus_SAP;
                            inventoryFacade.UpdateSAPMaterialTrans(sapMaterialTrans);
                        }
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
            ServiceResult returnValue = null;

            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("MaterialIssueTransferConfig") as SAPWebServiceEntity;
            if (webServiceEntity == null)
            {
                return new ServiceResult(false, "没有维护MaterialIssueTransfer对应的Service地址", this.m_Argument.TransactionCode);
            }

            if (runMethod == RunMethod.Auto)
            {
                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                object[] sapMaterialTransList = inventoryFacade.QuerySAPMaterialTransNotDealed(100);
                if (sapMaterialTransList == null)
                {
                    return new ServiceResult(true, "", this.m_Argument.TransactionCode);
                }

                foreach (SAPMaterialTrans materialTrans in sapMaterialTransList)
                {
                    System.Threading.Thread.Sleep(10);
                    this.NewTransactionCode();

                    List<DT_MES_TRANSFERPOST_REQTRANSFERITEM> transferItemList = new List<DT_MES_TRANSFERPOST_REQTRANSFERITEM>();
                    DT_MES_TRANSFERPOST_REQTRANSFERITEM transferItem = new DT_MES_TRANSFERPOST_REQTRANSFERITEM();
                    transferItem.PSTNG_DATE = materialTrans.AccountDate.ToString();
                    transferItem.DOC_DATE = materialTrans.VoucherDate.ToString();
                    transferItem.MATERIAL = materialTrans.ItemCode;
                    transferItem.PLANT = materialTrans.OrganizationID.ToString();
                    transferItem.STGE_LOC = materialTrans.FRMStorageID;
                    transferItem.MOVE_STLOC = materialTrans.TOStorageID;
                    transferItem.ENTRY_QNT = materialTrans.TransQTY.ToString();
                    transferItem.ENTRY_UOM = materialTrans.Unit;
                    transferItem.HEADER_TXT = materialTrans.ReceiveMemo;
                    transferItem.VENDOR = materialTrans.VendorCode;
                    transferItem.REF_DOC_NO = materialTrans.MoCode;

                    //transferItem.MOVE_TYPE = "411";
                    transferItem.MOVE_TYPE = materialTrans.SAPCode;

                    transferItemList.Add(transferItem);

                    DT_MES_TRANSFERPOST_REQ materialIssueParameter = new DT_MES_TRANSFERPOST_REQ();
                    materialIssueParameter.TRANSCODE = this.m_Argument.TransactionCode;
                    materialIssueParameter.TRANSFERITEM = transferItemList.ToArray();

                    returnValue = RunOne(webServiceEntity, materialTrans, materialIssueParameter);
                    if (!returnValue.Result)
                    {
                        break;
                    }
                }
            }
            else if (runMethod == RunMethod.Manually)
            {
                DT_MES_TRANSFERPOST_REQ materialIssueParameter = new DT_MES_TRANSFERPOST_REQ();
                materialIssueParameter.TRANSCODE = this.m_Argument.TransactionCode;
                materialIssueParameter.TRANSFERITEM = this.m_Argument.InventoryList.ToArray();

                returnValue = RunOne(webServiceEntity, null, materialIssueParameter);
            }

            if (returnValue == null)
            {
                returnValue = new ServiceResult(true, "", this.m_Argument.TransactionCode);
            }

            return returnValue;
        }

        private ServiceResult RunOne(SAPWebServiceEntity webServiceEntity, SAPMaterialTrans sapMaterialTrans, DT_MES_TRANSFERPOST_REQ req)
        {
            string xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
                typeof(DT_MES_TRANSFERPOST_REQ), req);

            #region TransferLog

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SAPDataTransferLog materialIssueLog = new SAPDataTransferLog();
            TransferFacade transferFacade = new TransferFacade(this.DataProvider);

            materialIssueLog.JobID = TransferFacade.MaterialIssueJobID;
            materialIssueLog.TransactionCode = this.m_Argument.TransactionCode;
            materialIssueLog.TransactionSequence = 1;
            materialIssueLog.RequestDate = dateTime.DBDate;
            materialIssueLog.RequestTime = dateTime.DBTime;
            materialIssueLog.RequestContent = xmlFilePath;
            materialIssueLog.OrganizationID = 2000;
            materialIssueLog.SendRecordCount = 1;

            transferFacade.AddSAPDataTransferLog(materialIssueLog);

            #endregion

            #region Begin for Calling WebService

            try
            {
                MaterialIssueServiceClientProxy clientProxy = new MaterialIssueServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                clientProxy.MI_MES_TRANSFERPOST_REQ(req);
                clientProxy.Dispose();
                clientProxy = null;
            }
            catch (Exception e)
            {
                materialIssueLog.Result = "Fail";
                materialIssueLog.ErrorMessage = e.Message;
                transferFacade.UpdateSAPDataTransferLog(materialIssueLog);
                return new ServiceResult(false, e.Message, materialIssueLog.TransactionCode);
            }

            #endregion

            #region Update tblsapmaterialtrans

            if (sapMaterialTrans != null)
            {
                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
                try
                {
                    sapMaterialTrans.Flag = FlagStatus.FlagStatus_POST;
                    sapMaterialTrans.TransactionCode = this.m_Argument.TransactionCode;
                    inventoryFacade.UpdateSAPMaterialTrans(sapMaterialTrans);
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
            if (this.m_Argument.InventoryList == null || this.m_Argument.InventoryList.Count == 0)
            {
                returnMessage = "出库信息列表不能为空";
                return false;
            }
            return true;
        }

        public object GetArguments()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MaterialIssueTransferArgument(this.DataProvider);
            }

            return m_Argument;
        }

        public object NewTransactionCode()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MaterialIssueTransferArgument(this.DataProvider);
            }
            else
            {
                this.m_Argument.GenerateNewTransactionCode(this.DataProvider);
            }

            return m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as MaterialIssueTransferArgument;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
