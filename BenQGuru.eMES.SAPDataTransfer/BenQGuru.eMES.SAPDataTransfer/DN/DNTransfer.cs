using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using System.Net;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Delivery;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class DNTransfer : ICommand
    {
        private DNTransferArgument m_Argument = null;

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

        public DNTransfer()
        {

        }

        public void Receive(DNRecive_REQ request)
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
                    log.JobID = TransferFacade.DNTransferJobID;
                    log.OrganizationID = 2000;
                    log.ReceivedRecordCount = (request.DNList == null || request.DNList.Count == 0) ? 0 : request.DNList.Count;
                    log.TransactionCode = TransferFacade.DNTransferJobID + "_"
                                        + currentDateTime.DBDate.ToString() + "_"
                                        + currentDateTime.DBTime.ToString()
                                        + DateTime.Now.Millisecond.ToString();
                    log.TransactionSequence = 1;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(DNRecive_REQ), request);
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
                    log.ReceivedRecordCount = (request.DNList == null || request.DNList.Count == 0) ? 0 : request.DNList.Count;
                    xmlFilePath = SerializeUtil.SerializeFile(log.TransactionCode + "_Response.xml",
                                                                typeof(DNRecive_REQ), request);
                    log.ResponseContent = xmlFilePath;
                    log.ResponseDate = currentDateTime.DBDate;
                    log.ResponseTime = currentDateTime.DBTime;
                    //log.Result
                    transferFacade.UpdateSAPDataTransferLog(log);
                }

                if (string.Compare(request.Flag, "Y", true) == 0)
                {
                    if (request.DNList == null || request.DNList.Count == 0)
                    {
                        log.Result = "Fail";
                        log.ErrorMessage = "DN list is null.";
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
                            this.DealDN(request.DNList, currentDateTime);

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

        private void DealDN(List<MES_DN> dnlist, DBDateTime currentDateTime)
        {
            DeliveryFacade deliveryFacade = new DeliveryFacade(this.DataProvider);

            foreach (MES_DN dn in dnlist)
            {
                deliveryFacade.DeleteDN4Receive(dn.DNCode);
            }

            DeliveryNote deliveryNote;
            object oldDeliveryNote;
            foreach (MES_DN dn in dnlist)
            {
                deliveryNote = deliveryFacade.CreateNewDN();
                deliveryNote.DNCode = FormatHelper.PKCapitalFormat(dn.DNCode);
                deliveryNote.DNLine = FormatHelper.PKCapitalFormat(dn.DNLine);

                deliveryNote.CustomerOrderNo = dn.CustomerOrderNo;
                deliveryNote.CustomerOrderNoType = dn.CustomerOrderNoType;
                deliveryNote.DNQuantity = dn.DNQuantity;
                deliveryNote.SAPStorage = dn.FromStorage;
                deliveryNote.FromStorage = dn.OrgID.ToString() + "-" + dn.FromStorage;
                deliveryNote.ItemCode = dn.ItemCode;
                //deliveryNote.ItemDescription = dn.ItemDescription;
                deliveryNote.ItemDescription = ""; // Temp save null
 
                //deliveryNote.ItemGrade = dn.ItemGrade;
                deliveryNote.MaintainDate = currentDateTime.DBDate;
                deliveryNote.MaintainTime = currentDateTime.DBTime;
                deliveryNote.MaintainUser = "SAP";
                deliveryNote.MOCode = dn.MOCode;
                deliveryNote.MovementType = dn.MovementType;
                deliveryNote.OrderNo = dn.OrderNo;
                deliveryNote.OrganizationID = dn.OrgID;
                deliveryNote.ShipTo = dn.ShipTo;
                deliveryNote.ToStorage = dn.ToStorage;
                deliveryNote.Unit = dn.Unit;
                //deliveryNote.Status = dn.Status;
                deliveryNote.DNStatus = DNStatus.StatusInit;
                deliveryNote.DNFrom = DNFrom.ERP;

                oldDeliveryNote = deliveryFacade.GetDeliveryNote(deliveryNote.DNCode, deliveryNote.DNLine);
                if (oldDeliveryNote == null)
                {
                    deliveryNote.RealQuantity = 0;
                    deliveryFacade.AddDeliveryNote(deliveryNote);
                }
                //else
                //{
                //    deliveryNote.RealQuantity = (oldDeliveryNote as DeliveryNote).RealQuantity;

                //    deliveryFacade.UpdateDeliveryNote(deliveryNote);
                //}
            }
        }

        #region ICommand Members

        public ServiceResult Run(RunMethod runMethod)
        {
            /*====Get WebService URL and UserName and Password====*/
            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("DNTransferConfig") as SAPWebServiceEntity;
            if (webServiceEntity == null)
            {
                return new ServiceResult(false, "没有维护DNTransfer对应的Service地址", this.m_Argument.TransactionCode);
            }

            #region Begin for Prepare input Paremente
            // Prepare input parameter
            DT_MES_DN_REQ dnParameter = new DT_MES_DN_REQ();
            dnParameter.TRANS = this.m_Argument.TransactionCode;

            if (this.m_Argument.DNCode.Trim().Length == 0)
            {
                dnParameter.DATE_B = this.m_Argument.MaintainDate_B.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
                dnParameter.DATE_E = this.m_Argument.MaintainDate_E.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
                dnParameter.WERKS = this.m_Argument.OrgList;
                dnParameter.VBELN = "";
            }
            else
            {
                dnParameter.DATE_B = "";
                dnParameter.DATE_E = "";
                dnParameter.WERKS = null;
                dnParameter.VBELN = this.m_Argument.DNCode.Trim().ToUpper();
            }
            #endregion

            // Serialize the Input Parameter
            string xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
                typeof(DT_MES_DN_REQ), dnParameter);

            #region For Request Log
            DBDateTime requestDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SAPDataTransferLog log = new SAPDataTransferLog();
            TransferFacade transferFacade = new TransferFacade(this.DataProvider);
            log.JobID = TransferFacade.DNTransferJobID;
            log.TransactionCode = this.m_Argument.TransactionCode;
            log.TransactionSequence = 1;
            log.RequestDate = requestDateTime.DBDate;
            log.RequestTime = requestDateTime.DBTime;
            log.RequestContent = xmlFilePath;
            log.OrganizationID = 2000;
            log.SendRecordCount = 1;
            transferFacade.AddSAPDataTransferLog(log);
            #endregion

            #region Begin for Calling WebService
            // Call Web Service through DNServiceClientProxy
            try
            {
                DNServiceClientProxy clientProxy = new DNServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                clientProxy.MI_MES_DN_REQ(dnParameter);
                clientProxy.Dispose();
                clientProxy = null;
            }
            catch (Exception e)
            {
                log.Result = "Fail";
                log.ErrorMessage = e.Message;
                transferFacade.UpdateSAPDataTransferLog(log);
                return new ServiceResult(false, e.Message, log.TransactionCode);
            }
            #endregion

            return new ServiceResult(true, "", this.m_Argument.TransactionCode);
        }

        public object GetArguments()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new DNTransferArgument(this.DataProvider);
            }
            return this.m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as DNTransferArgument;
        }

        public bool ArgumentValid(ref string returnMessage)
        {
            if (this.m_Argument == null)
            {
                returnMessage = "参数没有维护";
                return false;
            }
            if (this.m_Argument.DNCode == null || this.m_Argument.DNCode.Trim().Length == 0)
            {
                if (this.m_Argument.OrgList == null || this.m_Argument.OrgList.Length == 0)
                {
                    returnMessage = "工厂列表需要输入大于0的值";
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
                this.m_Argument = new DNTransferArgument(this.DataProvider);
            }
            else
            {
                this.m_Argument.GenerateNewTransactionCode(this.DataProvider);
            }
            return this.m_Argument;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
