using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using System.Net;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using System.Collections;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MOConfirm : ICommand
    {
        private MOConfirmArgument m_Argument = null;
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

        #region ICommand Members

        public ServiceResult Run(RunMethod runMethod)
        {
            /*---------- Get WebService URL and UserName and Password -------------*/
            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("MOConfirmConfig") as SAPWebServiceEntity;
            if (webServiceEntity == null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["WebServiceUrl"] != null
                    && System.Configuration.ConfigurationManager.AppSettings["WebServiceUserName"] != null
                    && System.Configuration.ConfigurationManager.AppSettings["WebServicePassword"] != null)
                {
                    webServiceEntity = new SAPWebServiceEntity();
                    webServiceEntity.Url = System.Configuration.ConfigurationManager.AppSettings["WebServiceUrl"];
                    webServiceEntity.UserName = System.Configuration.ConfigurationManager.AppSettings["WebServiceUserName"];
                    webServiceEntity.Password = System.Configuration.ConfigurationManager.AppSettings["WebServicePassword"];
                }
                else
                {
                    return new ServiceResult(false, "没有维护MOConfirmConfig对应的Service地址", this.m_Argument.TransactionCode);
                }
            }

            #region Begin for Prepare input Paremeter
            // Prepare input parameter
            DT_MES_POCONFIRM_REQ moInputParas = new DT_MES_POCONFIRM_REQ();
            moInputParas.Trsaction_code = this.m_Argument.TransactionCode;
            moInputParas.POLIST = new DT_MES_POCONFIRM_REQPOLIST[this.m_Argument.MOList.Count];
            for (int i = 0; i < this.m_Argument.MOList.Count; i++)
            {
                moInputParas.POLIST[i] = this.m_Argument.MOList[i];
            }
            #endregion

            // Serialize the Input Parameter
            string inputXmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
                typeof(DT_MES_POCONFIRM_REQ), moInputParas);
            DBDateTime requestDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            TransferFacade transferFacade = new TransferFacade(this.DataProvider);
            SAPDataTransferLog log;

            #region Begin for Calling WebService
            // Call Web Service through StandardBOMServiceClientProxy
            DT_MES_POCONFIRM_RESP returnValue;
            string outputXmlFilePath;
            DBDateTime responseDateTime;
            try
            {
                MOConfirmServiceClientProxy clientProxy = new MOConfirmServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                returnValue = clientProxy.MI_MES_POCONFIRM_REQ(moInputParas);
                clientProxy.Dispose();
                clientProxy = null;
            }
            catch (Exception e)
            {
                log = new SAPDataTransferLog();
                log.JobID = TransferFacade.MOCompleteTransferJobID;
                log.TransactionCode = this.m_Argument.TransactionCode;
                log.TransactionSequence = 1;
                log.RequestDate = requestDateTime.DBDate;
                log.RequestTime = requestDateTime.DBTime;
                log.RequestContent = inputXmlFilePath;
                log.OrganizationID = this.m_Argument.OrgID;
                log.SendRecordCount = this.m_Argument.MOList.Count;
                log.ReceivedRecordCount = 0;
                log.ResponseDate = 0;
                log.ResponseTime = 0;
                log.ResponseContent = "";
                log.FinishedDate = 0;
                log.FinishedTime = 0;
                log.Result = "Fail";
                log.ErrorMessage = e.Message;
                transferFacade.AddSAPDataTransferLog(log);
                return new ServiceResult(false, e.Message, log.TransactionCode);
            }
            #endregion

            //Serialize the output Parameter
            outputXmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Response.xml",
                typeof(DT_MES_POCONFIRM_RESP), returnValue);
            // Update Log
            responseDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            Dictionary<string, List<DT_MES_POCONFIRM>> result = this.ParseMOResult(returnValue.PO_lIST);

            int sMOCount = result.Count;
            List<DT_MES_POCONFIRM> moMessageList;
            DBDateTime finishedDateTime;
            MOFacade moFacade = new MOFacade(this.DataProvider);
            MO2SAPLog mo2saplog;
            int counter = 0;
            foreach (string moCode in result.Keys)
            {
                moMessageList = result[moCode];

                // Add log for Single MO
                log = new SAPDataTransferLog();
                log.JobID = TransferFacade.MOCompleteTransferJobID;
                log.TransactionCode = this.m_Argument.TransactionCode;
                log.TransactionSequence = counter + 1;
                log.RequestDate = requestDateTime.DBDate;
                log.RequestTime = requestDateTime.DBTime;
                log.RequestContent = inputXmlFilePath;
                log.OrganizationID = this.m_Argument.OrgID;
                log.SendRecordCount = 1;
                log.ReceivedRecordCount = moMessageList.Count;
                log.ResponseDate = responseDateTime.DBDate;
                log.ResponseTime = responseDateTime.DBTime;
                log.ResponseContent = outputXmlFilePath;
                log.Result = "OK";
                log.ErrorMessage = "";
                finishedDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                log.FinishedDate = finishedDateTime.DBDate;
                log.FinishedTime = finishedDateTime.DBTime;
                transferFacade.AddSAPDataTransferLog(log);

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                this.DataProvider.BeginTransaction();
                try
                {
                    if (moMessageList.Count == 1)
                    {
                        DT_MES_POCONFIRM moConfirm = moMessageList[0];
                        if (string.Compare(moConfirm.FLAG, "Y", true) == 0)
                        {
                            // Update MO2SAP.Flag
                            moFacade.UpdateMO2SAPFlag(moConfirm.MOCode, decimal.Parse(moConfirm.PostSeq));
                        }
                        else
                        {
                            // Fail
                            // Insert new MO2SAPLog
                            mo2saplog = new MO2SAPLog();
                            mo2saplog.MOCode = moConfirm.MOCode;
                            mo2saplog.Active = "Y";
                            mo2saplog.ErrorMessage = moConfirm.message;
                            mo2saplog.MaintainDate = finishedDateTime.DBDate;
                            mo2saplog.MaintainTime = finishedDateTime.DBTime;
                            mo2saplog.MaintainUser = "AUTO";
                            mo2saplog.OrganizationID = this.m_Argument.OrgID;
                            mo2saplog.PostSequence = decimal.Parse(moConfirm.PostSeq);
                            mo2saplog.Sequence = moFacade.GetMaxMO2SAPSequence(mo2saplog.MOCode, mo2saplog.PostSequence);
                            moFacade.AddMO2SAPLog(mo2saplog);
                        }
                    }
                    else // 返回的是ErrorMessage
                    {
                        foreach (DT_MES_POCONFIRM moConfirm in moMessageList)
                        {
                            // Insert New MO2SAPLog
                            mo2saplog = new MO2SAPLog();
                            mo2saplog.MOCode = moConfirm.MOCode;
                            mo2saplog.Active = "Y";
                            mo2saplog.ErrorMessage = moConfirm.message;
                            mo2saplog.MaintainDate = finishedDateTime.DBDate;
                            mo2saplog.MaintainTime = finishedDateTime.DBTime;
                            mo2saplog.MaintainUser = "AUTO";
                            mo2saplog.OrganizationID = this.m_Argument.OrgID;
                            mo2saplog.PostSequence = decimal.Parse(moConfirm.PostSeq);
                            mo2saplog.Sequence = moFacade.GetMaxMO2SAPSequence(mo2saplog.MOCode, mo2saplog.PostSequence);
                            moFacade.AddMO2SAPLog(mo2saplog);
                        }
                    }
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    return new ServiceResult(false, ex.Message, this.m_Argument.TransactionCode);
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                }

                counter++;
            }

            return new ServiceResult(true, "", this.m_Argument.TransactionCode);
        }

        private Dictionary<string, List<DT_MES_POCONFIRM>> ParseMOResult(DT_MES_POCONFIRM[] poList)
        {
            Dictionary<string, List<DT_MES_POCONFIRM>> returnValue = new Dictionary<string, List<DT_MES_POCONFIRM>>();
            List<DT_MES_POCONFIRM> temp;
            for (int i = 0; i < poList.Length; i++)
            {
                if (returnValue.ContainsKey(poList[i].MOCode))
                {
                    temp = returnValue[poList[i].MOCode];
                    if (!temp.Contains(poList[i]))
                    {
                        temp.Add(poList[i]);
                    }
                }
                else
                {
                    temp = new List<DT_MES_POCONFIRM>();
                    temp.Add(poList[i]);
                    returnValue.Add(poList[i].MOCode, temp);
                }
            }
            return returnValue;
        }

        public object GetArguments()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MOConfirmArgument(this.DataProvider);
            }
            return this.m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as MOConfirmArgument;
        }

        public bool ArgumentValid(ref string returnMessage)
        {
            return true;
        }

        public object NewTransactionCode()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MOConfirmArgument(this.DataProvider);
            }
            else
            {
                this.m_Argument.GenerateNewTransactionCode(this.DataProvider);
            }
            return this.m_Argument;
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
