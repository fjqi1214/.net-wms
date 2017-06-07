using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using System.Net;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Domain.IQC;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MaterialPOTransfer : ICommand
    {

        private MaterialPOTransferArgument m_Argument = null;

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

        public void Receive(MATPO_REQ request)
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
                    log.JobID = TransferFacade.MaterialPOJobID;
                    log.OrganizationID = 2000;
                    log.ReceivedRecordCount = (request.POList == null || request.POList.Count == 0) ? 0 : request.POList.Count;
                    log.TransactionCode = TransferFacade.MaterialPOJobID + "_"
                                        + currentDateTime.DBDate.ToString() + "_"
                                        + currentDateTime.DBTime.ToString()
                                        + DateTime.Now.Millisecond.ToString();
                    log.TransactionSequence = 1;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(MATPO_REQ), request);
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
                    log.ReceivedRecordCount = (request.POList == null || request.POList.Count == 0) ? 0 : request.POList.Count;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(MATPO_REQ), request);
                    log.ResponseContent = xmlFilePath;
                    log.ResponseDate = currentDateTime.DBDate;
                    log.ResponseTime = currentDateTime.DBTime;
                    //log.Result
                    transferFacade.UpdateSAPDataTransferLog(log);
                }

                if (request.POList == null || request.POList.Count == 0)
                {
                    log.Result = "Fail";
                    log.ErrorMessage = "PO list is null.";
                    log.FinishedDate = 0;
                    log.FinishedTime = 0;

                    transferFacade.UpdateSAPDataTransferLog(log);
                    return;
                }
                else
                {
                    MaterialStockFacade msf = new MaterialStockFacade(this.DataProvider);
                    IQCFacade iqcFacade = new IQCFacade(this.DataProvider);
                    RawReceive4SAP rawReceive4SAP;

                    List<MaterialReceive> confirmMaterialReceiveList = new List<MaterialReceive>();

                    try
                    {
                        this.DataProvider.BeginTransaction();

                        foreach (MES_MATPO mpo in request.POList)
                        {
                            rawReceive4SAP = msf.CreateNewRawReceive4SAP();
                            rawReceive4SAP.ErrorMessage = mpo.Message;
                            rawReceive4SAP.Flag = mpo.Flag;
                            rawReceive4SAP.MaintainDate = currentDateTime.DBDate;
                            rawReceive4SAP.MaintainTime = currentDateTime.DBTime;
                            rawReceive4SAP.MaintainUser = "SAP";
                            rawReceive4SAP.MaterialDocument = mpo.MaterialDocument;
                            rawReceive4SAP.MaterialDocumentYear = mpo.MaterialDocumentYear;
                            rawReceive4SAP.PONo = mpo.PONo;
                            rawReceive4SAP.PostSequence = msf.GetRawReceive4SAPMaxPostSequence(mpo.PONo.ToUpper());
                            rawReceive4SAP.TransactionCode = request.TransactionCode;

                            msf.AddRawReceive4SAP(rawReceive4SAP);

                            if (string.Compare(mpo.Flag, "Y", true) == 0)  // Post OK
                            {
                                object[] materialReceiveList = iqcFacade.QueryMaterialReceive(mpo.PONo, request.TransactionCode);

                                if (materialReceiveList != null)
                                {
                                    foreach (MaterialReceive materialReceive in materialReceiveList)
                                    {
                                        if (materialReceive.Flag == FlagStatus.FlagStatus_POST)
                                        {
                                            confirmMaterialReceiveList.Add(materialReceive);
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

                        foreach (MaterialReceive materialReceive in confirmMaterialReceiveList)
                        {
                            iqcFacade.ConfirmMaterialReceive(new MaterialReceive[] { materialReceive }, materialReceive.MaintainUser);
                        }
                    }
                    catch (Exception ex)
                    {
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

            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("MaterialPOTransferConfig") as SAPWebServiceEntity;
            if (webServiceEntity == null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["MPOWebServiceUrl"] != null
                    && System.Configuration.ConfigurationManager.AppSettings["MPOWebServiceUserName"] != null
                    && System.Configuration.ConfigurationManager.AppSettings["MPOWebServicePassword"] != null)
                {
                    webServiceEntity = new SAPWebServiceEntity();
                    webServiceEntity.Url = System.Configuration.ConfigurationManager.AppSettings["MPOWebServiceUrl"];
                    webServiceEntity.UserName = System.Configuration.ConfigurationManager.AppSettings["MPOWebServiceUserName"];
                    webServiceEntity.Password = System.Configuration.ConfigurationManager.AppSettings["MPOWebServicePassword"];
                }
                else
                {
                    return new ServiceResult(false, "没有维护MaterialPOTransfer对应的Service地址", this.m_Argument.TransactionCode);
                }
            }                      

            #region Begin for Prepare input Paremeter

            IQCFacade iqcFacade = new IQCFacade(this.DataProvider);  

            object[] materialReceiveList = null;
            if (runMethod == RunMethod.Auto)
            {
                materialReceiveList = iqcFacade.QueryMaterialReceiveNotConfirmed(100);
            }
            else if (runMethod == RunMethod.Manually)
            {
                MaterialReceive materialReceive = (MaterialReceive)iqcFacade.GetMaterialReceive(m_Argument.IQCNo, m_Argument.STLine);

                if (materialReceive != null && materialReceive.Flag == FlagStatus.FlagStatus_MES)
                {
                    materialReceiveList = new MaterialReceive[] { materialReceive };
                }
            }

            #endregion

            if (materialReceiveList != null)
            {
                List<DT_MES_SOURCESTOCK_REQLIST> inventoryList = new List<DT_MES_SOURCESTOCK_REQLIST>();
                foreach (MaterialReceive materialReceive in materialReceiveList)
                {
                    System.Threading.Thread.Sleep(500);

                    this.NewTransactionCode();
                    inventoryList.Clear();

                    DT_MES_SOURCESTOCK_REQLIST post = new DT_MES_SOURCESTOCK_REQLIST();
                    post.PSTNG_DATE = materialReceive.AccountDate.ToString();
                    post.DOC_DATE = materialReceive.VoucherDate.ToString();
                    post.PO_NUMBER = materialReceive.OrderNo;
                    post.PO_ITEM = materialReceive.OrderLine.ToString();
                    post.MATERIAL = materialReceive.ItemCode;
                    post.PLANT = materialReceive.OrganizationID.ToString();
                    post.STGE_LOC = materialReceive.StorageID;
                    post.ENTRY_QNT = materialReceive.RealReceiveQty.ToString();
                    post.ENTRY_UOM = materialReceive.Unit;
                    post.HEADER_TXT = materialReceive.ReceiveMemo;
                    post.MOVE_TYPE = "101";

                    inventoryList.Add(post);

                    DT_MES_SOURCESTOCK_REQ materialPOParameter = new DT_MES_SOURCESTOCK_REQ();
                    materialPOParameter.TRANSCODE = this.m_Argument.TransactionCode;
                    materialPOParameter.LIST = inventoryList.ToArray();

                    returnValue = RunOne(webServiceEntity, materialReceive, materialPOParameter);
                    if (!returnValue.Result)
                    {
                        break;
                    }
                }
            }

            if (returnValue == null)
            {
                returnValue = new ServiceResult(true, "", this.m_Argument.TransactionCode);
            }
            return returnValue;
        }

        private ServiceResult RunOne(SAPWebServiceEntity webServiceEntity, MaterialReceive materialReceive, DT_MES_SOURCESTOCK_REQ materialPOParameter)
        {
            IQCFacade iqcFacade = new IQCFacade(this.DataProvider);

            string xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
                typeof(DT_MES_SOURCESTOCK_REQ), materialPOParameter);

            #region TransferLog

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SAPDataTransferLog materialPOLog = new SAPDataTransferLog();
            TransferFacade transferFacade = new TransferFacade(this.DataProvider);

            materialPOLog.JobID = TransferFacade.MaterialPOJobID;
            materialPOLog.TransactionCode = this.m_Argument.TransactionCode;
            materialPOLog.TransactionSequence = 1;
            materialPOLog.RequestDate = dateTime.DBDate;
            materialPOLog.RequestTime = dateTime.DBTime;
            materialPOLog.RequestContent = xmlFilePath;
            materialPOLog.OrganizationID = 2000;
            materialPOLog.SendRecordCount = 1;

            transferFacade.AddSAPDataTransferLog(materialPOLog);

            #endregion

            #region Begin for Calling WebService

            try
            {
                MaterialPOServiceClientProxy clientProxy = new MaterialPOServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                clientProxy.MI_MES_SOURCESTOCK_REQ(materialPOParameter);
                clientProxy.Dispose();
                clientProxy = null;
            }
            catch (Exception e)
            {
                materialPOLog.Result = "Fail";
                materialPOLog.ErrorMessage = e.Message;
                transferFacade.UpdateSAPDataTransferLog(materialPOLog);
                return new ServiceResult(false, e.Message, materialPOLog.TransactionCode);
            }

            #endregion

            #region UPDATE tblmaterialreceive

            try
            {
                this.DataProvider.BeginTransaction();

                foreach (DT_MES_SOURCESTOCK_REQLIST post in materialPOParameter.LIST)
                {
                    materialReceive = (MaterialReceive)iqcFacade.GetMaterialReceive(materialReceive.IQCNo, materialReceive.STLine);
                    if (materialReceive != null)
                    {
                        materialReceive.Flag = FlagStatus.FlagStatus_POST;
                        materialReceive.TransactionCode = this.m_Argument.TransactionCode;
                        iqcFacade.UpdateMaterialReceive(materialReceive);
                    }
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return new ServiceResult(false, ex.Message, this.m_Argument.TransactionCode);
            }

            #endregion

            return new ServiceResult(true, "", this.m_Argument.TransactionCode);
        }

        public bool ArgumentValid(ref string returnMessage)
        {
            if (m_Argument.IQCNo == null || m_Argument.IQCNo.Trim().Length <= 0)
            {
                returnMessage = "IQC单号不能为空";
                return false;
            }
         
            IQCFacade iqcFacade = new IQCFacade(this.DataProvider);
            MaterialReceive materialReceive = (MaterialReceive)iqcFacade.GetMaterialReceive(m_Argument.IQCNo, m_Argument.STLine);
            if (materialReceive == null)
            {
                returnMessage = "MaterialReceive数据不存在";
                return false;
            }
            else if (materialReceive.Flag != FlagStatus.FlagStatus_MES)
            {
                returnMessage = "MaterialReceive.Flag不为MES";
                return false;
            }

            return true;
        }

        public object GetArguments()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MaterialPOTransferArgument(this.DataProvider);
            }

            return m_Argument;
        }

        public object NewTransactionCode()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MaterialPOTransferArgument(this.DataProvider);
            }
            else
            {
                this.m_Argument.GenerateNewTransactionCode(this.DataProvider);
            }

            return m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as MaterialPOTransferArgument;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
    }
}
