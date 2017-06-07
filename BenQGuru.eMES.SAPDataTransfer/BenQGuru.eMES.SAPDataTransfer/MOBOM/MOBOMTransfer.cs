using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using System.Net;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MOBOMTransfer : ICommand
    {
        private MOBOMTransferArgument m_Argument = null;
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

        public MOBOMTransfer()
        {
        }

        public MOBOMTransfer(IDomainDataProvider dataProvider)
        {
            this._domainDataProvider = dataProvider;
        }

        #region ICommand Members

        public ServiceResult Run(RunMethod runMethod)
        {

            /*------ Get WebService URL and UserName and Password ------*/
            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("MOBOMTransferConfig") as SAPWebServiceEntity;
            if (webServiceEntity == null)
            {
                return new ServiceResult(false, "没有维护MOBOMTransferConfig对应的Service地址", this.m_Argument.TransactionCode);
            }

            #region Begin for Prepare input Paremente
            // Prepare input parameter
            DT_MES_POBOM_REQ moBOMParameter = new DT_MES_POBOM_REQ();
            moBOMParameter.Mocode = this.m_Argument.MOCodeList;
            moBOMParameter.Transaction_code = this.m_Argument.TransactionCode;
            #endregion

            // Serialize the Input Parameter
            string xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
                typeof(DT_MES_POBOM_REQ), moBOMParameter);

            #region For Request Log
            DBDateTime requestDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SAPDataTransferLog log = new SAPDataTransferLog();
            // new trans
            TransferFacade transferFacade = new TransferFacade(DomainDataProviderManager.DomainDataProvider());
            log.JobID = TransferFacade.MOMaterialTransferJobID;
            log.TransactionCode = this.m_Argument.TransactionCode;
            log.TransactionSequence = 1;
            log.RequestDate = requestDateTime.DBDate;
            log.RequestTime = requestDateTime.DBTime;
            log.RequestContent = xmlFilePath;
            log.OrganizationID = InternalVariables.MS_OrganizationID;
            log.SendRecordCount = 1;
            transferFacade.AddSAPDataTransferLog(log);
            #endregion

            #region Begin for Calling WebService
            // Call Web Service through MaterialServiceClientProxy
            DT_MES_POBOM_RESP returnValue;
            try
            {
                MOBOMServiceClientProxy clientProxy = new MOBOMServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                returnValue = clientProxy.MI_MES_POBOM_REQ(moBOMParameter);
                clientProxy.Dispose();
                clientProxy = null;

                //Serialize the output Parameter
                xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Response.xml",
                    typeof(DT_MES_POBOM_RESP), returnValue);

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

            if (string.Compare(returnValue.FLAG, "Y", true) == 0)
            {
                int moBOMCount = returnValue.POCONFIRM_LIST.Length;

                if (runMethod == RunMethod.Manually)
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                    this.DataProvider.BeginTransaction();
                }

                try
                {
                    #region Main flow
                    MOFacade moBOMFacade = new MOFacade(this.DataProvider);
                    MOBOM moBOM;
                    DT_MES_POBOM_RESPPOCONFIRM_LIST moBOMObject;

                    for (int j = 0; j < this.m_Argument.MOCodeList.Length; j++)
                    {
                        moBOMFacade.DeleteMOBOMByMOCode(this.m_Argument.MOCodeList[j].ToUpper());

                        for (int i = 0; i < moBOMCount; i++)
                        {
                            if (string.Compare(returnValue.POCONFIRM_LIST[i].MOCODE, this.m_Argument.MOCodeList[j], true) != 0)
                            {
                                continue;
                            }
                            else
                            {
                                moBOMObject = returnValue.POCONFIRM_LIST[i];

                                moBOM = moBOMFacade.CreateNewMOBOM();
                                moBOM.MOCode = moBOMObject.MOCODE;
                                moBOM.ItemCode = InternalVariables.MatchItemCode(moBOMObject.ITEMCODE);
                                moBOM.MOBOMItemCode = InternalVariables.MatchItemCode(moBOMObject.MOBITEMCODE);
                                //moBOM.MOBOMItemName = moBOMObject.MOBITEMDESC;
                                //moBOM.MOBOMItemDescription = moBOMObject.MOBITEMDESC;
                                moBOM.MOBOMItemName = "";
                                moBOM.MOBOMItemDescription = "";
                                moBOM.MOBOMItemQty = decimal.Parse(moBOMObject.MOBITEMQTY);
                                moBOM.MOBOMSourceItemCode = InternalVariables.MatchItemCode(moBOMObject.MOBITEMCODE);
                                moBOM.MOBOMItemUOM = moBOMObject.MOBOMITEMUOM;
                                moBOM.Sequence = moBOMFacade.GetMOBOMMaxSequence(moBOMObject.MOCODE);
                                moBOM.MOBOMItemStatus = "0";
                                moBOM.MOBOMItemEffectiveDate = 20080101;
                                moBOM.MOBOMItemEffectiveTime = 1;
                                moBOM.MOBOMItemInvalidDate = 29991231;
                                moBOM.MOBOMItemInvalidTime = 1;
                                moBOM.MaintainUser = "SAP";
                                moBOM.MOBOMItemECN = "";
                                moBOM.MOBOMItemLocation = "";
                                moBOM.MOBOMItemVersion = "";
                                moBOM.MOBOMItemControlType = "";
                                moBOM.EAttribute1 = "";
                                moBOM.OPCode = "";
                                moBOM.MoBOM = moBOMObject.MOBOM;
                                moBOM.MOBOMLine = moBOMObject.MOBOMLINE;
                                moBOM.MOFactory = moBOMObject.MOFAC;
                                moBOM.MOResource = moBOMObject.MORESOURCE;

                                moBOMFacade.AddMOBOM(moBOM);
                            }
                        }
                    }

                    if (runMethod == RunMethod.Manually)
                    {
                        this.DataProvider.CommitTransaction();
                    }
                    #endregion

                    log.Result = "OK";
                    log.ErrorMessage = "";
                }
                catch (Exception ex)
                {
                    if (runMethod == RunMethod.Manually)
                    {
                        this.DataProvider.RollbackTransaction();
                    }

                    // Log
                    log.Result = "Fail";
                    log.ErrorMessage = ex.Message;
                }
                finally
                {
                    if (runMethod == RunMethod.Manually)
                    {
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                    }
                }

                log.ReceivedRecordCount = moBOMCount;
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
                log.ErrorMessage = returnValue.message;
                log.ReceivedRecordCount = 0;
                transferFacade.UpdateSAPDataTransferLog(log);
                return new ServiceResult(false, log.ErrorMessage, log.TransactionCode);
            }
        }

        public object GetArguments()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MOBOMTransferArgument(this.DataProvider);
            }
            return this.m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as MOBOMTransferArgument;
        }

        public bool ArgumentValid(ref string returnMessage)
        {
            return true;
        }

        public void Dispose()
        {
            
        }

        public object NewTransactionCode()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MOBOMTransferArgument(this.DataProvider);
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
