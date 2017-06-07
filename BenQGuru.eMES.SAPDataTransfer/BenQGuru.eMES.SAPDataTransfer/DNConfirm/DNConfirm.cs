using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Delivery;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class DNConfirm : ICommand
    {
        private DNConfrimArgument m_Argument = null;

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

        public DNConfirm()
        {

        }

        public void Receive(DNCONFIRM_REQ request)
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
                    log.ReceivedRecordCount = (request.DNConfirmList == null || request.DNConfirmList.Count == 0) ? 0 : request.DNConfirmList.Count;
                    log.TransactionCode = TransferFacade.DNConfirmJobID + "_"
                                        + currentDateTime.DBDate.ToString() + "_"
                                        + currentDateTime.DBTime.ToString()
                                        + DateTime.Now.Millisecond.ToString();
                    log.TransactionSequence = 1;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(DNCONFIRM_REQ), request);
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
                    log.ReceivedRecordCount = (request.DNConfirmList == null || request.DNConfirmList.Count == 0) ? 0 : request.DNConfirmList.Count;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(DNCONFIRM_REQ), request);
                    log.ResponseContent = xmlFilePath;
                    log.ResponseDate = currentDateTime.DBDate;
                    log.ResponseTime = currentDateTime.DBTime;
                    //log.Result
                    transferFacade.UpdateSAPDataTransferLog(log);
                }

                this.DataProvider.BeginTransaction();
                try
                {
                    DeliveryFacade deliveryFacade = new DeliveryFacade(this.DataProvider);
                    DN2SAP dn2sap;
                    foreach (MES_DNConfirm dnconfirm in request.DNConfirmList)
                    {
                        dn2sap = deliveryFacade.CreateNewDN2SAP();
                        dn2sap.Active = "Y";
                        dn2sap.DNCode = dnconfirm.DNNo;
                        dn2sap.ErrorMessage = dnconfirm.Message;
                        dn2sap.Flag = dnconfirm.Flag;
                        dn2sap.MaintainDate = currentDateTime.DBDate;
                        dn2sap.MaintainTime = currentDateTime.DBTime;
                        dn2sap.MaintainUser = "SAP";

                        deliveryFacade.UpdateDN2SAPStatusByDNNo(dn2sap.DNCode.ToUpper());

                        deliveryFacade.AddDN2SAP(dn2sap);

                        if (string.Compare(dnconfirm.Flag, "Y", true) == 0)  // Post OK
                        {
                            // Update tbldn.flag to SAP
                            deliveryFacade.UpdateDNFlag(dnconfirm.DNNo, FlagStatus.FlagStatus_SAP);
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
            /*====Get WebService URL and UserName and Password====*/
            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("DNConfirmConfig") as SAPWebServiceEntity;
            if (webServiceEntity == null)
            {
                return new ServiceResult(false, "没有维护DNConfirm对应的Service地址", this.m_Argument.TransactionCode);
            }

            DeliveryFacade deliveryFacade = new DeliveryFacade(this.DataProvider);

            #region Begin for Prepare input Paremeter

            DT_MES_DNPOST_REQ inputParameter = new DT_MES_DNPOST_REQ();

            if (runMethod == RunMethod.Auto)
            {
                this.NewTransactionCode();
                this.m_Argument.DNList.Clear();

                object[] deliveryNoteList = deliveryFacade.QueryDNNotConfirmed();
                if (deliveryNoteList != null)
                {
                    foreach (DeliveryNote deliveryNote in deliveryNoteList)
                    {
                        DT_MES_DNPOST post = new DT_MES_DNPOST();
                        post.VBELN = deliveryNote.DNCode;
                        post.POSNR = deliveryNote.DNLine;
                        post.MATNR = deliveryNote.ItemCode;
                        post.G_LFIMG = deliveryNote.RealQuantity.ToString();
                        post.LGORT = deliveryNote.SAPStorage;

                        this.m_Argument.DNList.Add(post);
                    }
                }
            }

            if (this.m_Argument.DNList == null || this.m_Argument.DNList.Count <= 0)
            {
                return new ServiceResult(true, "", this.m_Argument.TransactionCode);
            }

            inputParameter.TRANS = this.m_Argument.TransactionCode;
            inputParameter.DNPARAM = this.m_Argument.DNList.ToArray();

            #endregion

            string xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
                typeof(DT_MES_DNPOST_REQ), inputParameter);

            #region TransferLog

            DBDateTime requestDataTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SAPDataTransferLog transferLog = new SAPDataTransferLog();
            TransferFacade transferFacade = new TransferFacade(this.DataProvider);

            transferLog.JobID = TransferFacade.DNConfirmJobID;
            transferLog.TransactionCode = this.m_Argument.TransactionCode;
            transferLog.TransactionSequence = 1;
            transferLog.RequestDate = requestDataTime.DBDate;
            transferLog.RequestTime = requestDataTime.DBTime;
            transferLog.RequestContent = xmlFilePath;
            transferLog.OrganizationID = 2000;
            transferLog.SendRecordCount = 1;

            transferFacade.AddSAPDataTransferLog(transferLog);

            #endregion

            #region Begin for Calling WebService

            try
            {
                DNConfirmServiceClientProxy clientProxy = new DNConfirmServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                clientProxy.MI_MES_DNPOST_REQ(inputParameter);
                clientProxy.Dispose();
                clientProxy = null;
            }
            catch (Exception e)
            {
                transferLog.Result = "Fail";
                transferLog.ErrorMessage = e.Message;
                transferFacade.UpdateSAPDataTransferLog(transferLog);
                return new ServiceResult(false, e.Message, transferLog.TransactionCode);
            }

            #endregion

            #region Update tbldn

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (DT_MES_DNPOST post in this.m_Argument.DNList)
                {
                    DeliveryNote deliveryNote = (DeliveryNote)deliveryFacade.GetDeliveryNote(post.VBELN, post.POSNR);
                    if (deliveryNote != null)
                    {
                        deliveryNote.Flag = FlagStatus.FlagStatus_POST;
                        deliveryNote.TransactionCode = this.m_Argument.TransactionCode;
                        deliveryFacade.UpdateDeliveryNote(deliveryNote);
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
            returnMessage = string.Empty;

            if (this.m_Argument.DNList == null || this.m_Argument.DNList.Count == 0)
            {
                returnMessage = "交货单列表不能为空";
                return false;
            }

            DeliveryFacade deliveryFacade = new DeliveryFacade(this.DataProvider);
            foreach (DT_MES_DNPOST dn in this.m_Argument.DNList)
            {
                DeliveryNote deliveryNote = (DeliveryNote)deliveryFacade.GetDeliveryNote(dn.VBELN.Trim().ToUpper(), dn.POSNR.Trim().ToUpper());
                if (deliveryNote == null)
                {
                    returnMessage += "交货单不存在(交货单号:" + dn.VBELN.Trim().ToUpper() + ",交货单行项目:" + dn.POSNR.Trim().ToUpper() + ")" + "\n";
                }
                else
                {
                    if (deliveryNote.Flag != FlagStatus.FlagStatus_MES)
                    {
                        returnMessage += "交货单尚未出货，或者已经同步(交货单号:" + dn.VBELN.Trim().ToUpper() + ",交货单行项目:" + dn.POSNR.Trim().ToUpper() + ")" + "\n";
                    }
                }
            }
            if (returnMessage.Trim().Length > 0)
            {
                return false;
            }

            return true;
        }

        public object GetArguments()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new DNConfrimArgument(this.DataProvider);
            }

            return m_Argument;
        }

        public object NewTransactionCode()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new DNConfrimArgument(this.DataProvider);
            }
            else
            {
                this.m_Argument.GenerateNewTransactionCode(this.DataProvider);
            }
            return this.m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as DNConfrimArgument;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion


    }
}
