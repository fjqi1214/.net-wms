using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using System.Net;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class StandardBOMTransfer : ICommand
    {
        private StandardBOMTransferArgument m_Argument;
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
            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("StandardBOMTransferConfig") as SAPWebServiceEntity;
            if (webServiceEntity == null)
            {
                return new ServiceResult(false, "没有维护StandardBOMTransferConfig对应的Service地址", this.m_Argument.TransactionCode);
            }

            #region Begin for Prepare input Paremente
            // Prepare input parameter
            DT_MES_MATBOM_REQ standardBOMParameter = new DT_MES_MATBOM_REQ();
            standardBOMParameter.Trsactioncode = this.m_Argument.TransactionCode.ToString();
            standardBOMParameter.OrgID = this.m_Argument.OrgID.ToString();
            if (this.m_Argument.MaterialCode.Trim().Length == 0)
            {
                standardBOMParameter.MaintainDate_B = this.m_Argument.MaintainDate_B.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
                standardBOMParameter.MaintainDate_E = this.m_Argument.MaintainDate_E.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
                standardBOMParameter.MaterialCode = "";
            }
            else
            {
                standardBOMParameter.MaintainDate_B = "";
                standardBOMParameter.MaintainDate_E = "";
                standardBOMParameter.MaterialCode = this.m_Argument.MaterialCode.Trim().ToUpper();
            }
            #endregion

            // Serialize the Input Parameter
            string xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
                typeof(DT_MES_MATBOM_REQ), standardBOMParameter);

            #region For Request Log
            DBDateTime requestDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SAPDataTransferLog log = new SAPDataTransferLog();
            TransferFacade transferFacade = new TransferFacade(this.DataProvider);
            log.JobID = TransferFacade.StandardBOMTransferJobID;
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
            // Call Web Service through StandardBOMServiceClientProxy
            DT_MES_MATBOM_RSP returnValue;
            try
            {
                StandardBOMServiceClientProxy clientProxy = new StandardBOMServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                returnValue = clientProxy.MI_MES_MATBOM(standardBOMParameter);
                clientProxy.Dispose();
                clientProxy = null;

                //Serialize the output Parameter
                xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Response.xml",
                    typeof(DT_MES_MATBOM_RSP), returnValue);

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

            /*--- 获取Flag字段，如果FLAG字段值是DEL的话，删除该条；FLAG是空，则判断是否有该数据，有则更新，否则插入该条 ----*/
            if (string.Compare(returnValue.FLAG, "Y", true) == 0)
            {
                int sBOMCount = returnValue.ITEM.Length;

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                this.DataProvider.BeginTransaction();

                try
                {
                    SBOMFacade sBOMFacade = new SBOMFacade(this.DataProvider);
                    SBOM sBOM;
                    DT_MES_MATBOM_RSPITEM sBOMObject;

                    bool needAddNew = false;
                    for (int i = 0; i < sBOMCount; i++)
                    {
                        sBOMObject = returnValue.ITEM[i];

                        if (sBOMObject.ItemCode == null || sBOMObject.ItemCode == string.Empty ||
                            sBOMObject.SBItemCode == null || sBOMObject.SBItemCode == string.Empty ||
                            sBOMObject.SBOMVer == null || sBOMObject.SBOMVer == string.Empty ||
                            sBOMObject.SBItemProject == null || sBOMObject.SBItemProject == string.Empty ||
                            sBOMObject.SBItemSeq == null || sBOMObject.SBItemSeq == string.Empty)
                        {
                            continue;
                        }

                        object oldSBOM = sBOMFacade.GetSBOM(InternalVariables.MatchItemCode(sBOMObject.ItemCode).ToUpper(),
                            InternalVariables.MatchItemCode(sBOMObject.SBItemCode).ToUpper(),
                            sBOMObject.SBOMVer, sBOMObject.SBItemProject, sBOMObject.SBItemSeq, int.Parse(standardBOMParameter.OrgID));

                        if (string.Compare(sBOMObject.FLAG, "DEL", true) == 0)  //如果FLAG字段值是DEL的，执行删除操作
                        {
                            if (oldSBOM != null)
                            {
                                sBOMFacade.DeleteSBOM(oldSBOM as SBOM);
                            }
                        }
                        else if (string.Compare(sBOMObject.FLAG, "", true) == 0) //如果FLAG字段值是空，执行Insert/Update tblsbom操作
                        {
                            if (oldSBOM == null) //如果为空，则Insert tblsbom
                            {
                                sBOM = sBOMFacade.CreateSBOM();

                                sBOM.SBOMItemEffectiveDate = 20080101;
                                sBOM.SBOMWH = "";
                                sBOM.Sequence = sBOMFacade.GetSBOMMaxSequence(sBOM.ItemCode, sBOM.SBOMVersion);
                                sBOM.SBOMItemECN = "";
                                sBOM.SBOMItemStatus = "0";
                                sBOM.SBOMItemLocation = "";
                                sBOM.SBOMItemEffectiveTime = 1;
                                sBOM.SBOMItemInvalidDate = 29991231;
                                sBOM.SBOMItemInvalidTime = 1;
                                sBOM.SBOMItemVersion = "";
                                sBOM.SBOMItemControlType = "";
                                sBOM.SBOMParentItemCode = "";
                                sBOM.ALPGR = "";
                                sBOM.MaintainUser = "SAP";
                                sBOM.EAttribute1 = "";

                                needAddNew = true;
                            }
                            else
                            {
                                sBOM = oldSBOM as SBOM;

                                needAddNew = false;
                            }

                            sBOM.ItemCode = InternalVariables.MatchItemCode(sBOMObject.ItemCode); //tblsbom 的 PK
                            sBOM.SBOMItemCode = InternalVariables.MatchItemCode(sBOMObject.SBItemCode);
                            sBOM.SBOMSourceItemCode = InternalVariables.MatchItemCode(sBOMObject.SBItemCode);
                            sBOM.SBOMItemQty = decimal.Parse(sBOMObject.SBItemQTY);
                            sBOM.OrganizationID = int.Parse(standardBOMParameter.OrgID);
                            sBOM.SBOMVersion = sBOMObject.SBOMVer;
                            //sBOM.SBOMItemName = sBOMObject.SBItemDesc;
                            //sBOM.SBOMItemDescription = sBOMObject.SBItemDesc;
                            sBOM.SBOMItemName = "";
                            sBOM.SBOMItemDescription = "";
                            sBOM.SBOMItemUOM = sBOMObject.SBItemUOM;
                            //sBOM.ItemDescription = sBOMObject.ItemDesc;
                            sBOM.ItemDescription = "";
                            sBOM.SBOMFactory = sBOMObject.SBFactory;
                            sBOM.SBOMUsage = sBOMObject.SBUsage;
                            sBOM.SBOMItemProject = sBOMObject.SBItemProject;
                            sBOM.SBOMItemSequence = sBOMObject.SBItemSeq;
                            sBOM.Location = sBOMObject.SBItemPotx1;

                            if (needAddNew)
                            {
                                sBOMFacade.AddSBOM(sBOM);
                            }
                            else
                            {
                                sBOMFacade.UpdateSBOM(sBOM);
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

                log.ReceivedRecordCount = sBOMCount;
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
            else  //FLAG != "Y"
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
                this.m_Argument = new StandardBOMTransferArgument(this.DataProvider);
            }
            return this.m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as StandardBOMTransferArgument;
        }

        public bool ArgumentValid(ref string returnMessage)
        {
            if (this.m_Argument == null)
            {
                returnMessage = "参数没有维护";
                return false;
            }

            if (this.m_Argument.OrgID <= 0)
            {
                returnMessage = "组织代码需要输入大于0的值";
                return false;
            }

            if (this.m_Argument.MaterialCode == null || this.m_Argument.MaterialCode.Trim().Length == 0)
            {
                if (this.m_Argument.MaintainDate_B == DateTime.MinValue && this.m_Argument.MaintainDate_E == DateTime.MinValue)
                {
                    returnMessage = "运行参数-方式1和运行参数-方式2有且必须要有一组参数需要输入";
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

        public void Dispose()
        {

        }

        public object NewTransactionCode()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new StandardBOMTransferArgument(this.DataProvider);
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
