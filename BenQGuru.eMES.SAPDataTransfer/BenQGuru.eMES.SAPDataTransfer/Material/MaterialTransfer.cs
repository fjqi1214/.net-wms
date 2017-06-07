using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.SAPDataTransferInterface;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using System.Diagnostics;
using System.Threading;
using BenQGuru.eMES.SAPDataTransfer;
using System.IO;
using System.Xml;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using System.Net;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MaterialTransfer : ICommand
    {
        private MaterialTransferArgument m_Argument = null;
        private int m_RetriedTimes = -1;
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

        public MaterialTransfer()
        {

        }

        #region ICommand Members

        public ServiceResult Run(RunMethod runMethod)
        {
            /*====Get WebService URL and UserName and Password====*/
            SAPWebServiceEntity webServiceEntity = System.Configuration.ConfigurationManager.GetSection("MaterialTransferConfig") as SAPWebServiceEntity;
            if (webServiceEntity == null)
            {
                return new ServiceResult(false, "没有维护MaterialTransferConfig对应的Service地址", this.m_Argument.TransactionCode);
            }

            #region Begin for Prepare input Paremente
            // Prepare input parameter
            DT_MES_MATERIAL_REQ materialParameter = new DT_MES_MATERIAL_REQ();
            materialParameter.MaintainDate_B = this.m_Argument.MaintainDate_B.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
            materialParameter.MaintainDate_E = this.m_Argument.MaintainDate_E.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
            materialParameter.OrgID = this.m_Argument.OrgID.ToString();
            materialParameter.Transaction_code = this.m_Argument.TransactionCode;
            #endregion

            // Serialize the Input Parameter
            string xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Request.xml",
                typeof(DT_MES_MATERIAL_REQ), materialParameter);

            #region For Request Log
            DBDateTime requestDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SAPDataTransferLog log = new SAPDataTransferLog();
            TransferFacade transferFacade = new TransferFacade(this.DataProvider);
            log.JobID = TransferFacade.MaterialTransferJobID;
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
            // Call Web Service through MaterialServiceClientProxy
            DT_MES_MATERIAL_RESP returnValue;
        ReTryLabel:
            m_RetriedTimes++;
            try
            {
                MaterialServiceClientProxy clientProxy = new MaterialServiceClientProxy();
                clientProxy.RequestEncoding = Encoding.UTF8;
                clientProxy.Timeout = InternalVariables.MS_TimeOut * 1000;
                clientProxy.Url = webServiceEntity.Url;
                clientProxy.PreAuthenticate = true;
                System.Uri uri = new Uri(clientProxy.Url);
                clientProxy.Credentials = new NetworkCredential(webServiceEntity.UserName, webServiceEntity.Password).GetCredential(uri, "");
                returnValue = clientProxy.MI_MES_MATERIAL_REQ(materialParameter);
                clientProxy.Dispose();
                clientProxy = null;

                //Serialize the output Parameter
                xmlFilePath = SerializeUtil.SerializeFile(this.m_Argument.TransactionCode + "_Response.xml",
                    typeof(DT_MES_MATERIAL_RESP), returnValue);

                // Update Log
                DBDateTime responseDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                log.ResponseDate = responseDateTime.DBDate;
                log.ResponseTime = responseDateTime.DBTime;
                log.ResponseContent = xmlFilePath;
                transferFacade.UpdateSAPDataTransferLog(log);
            }
            catch (Exception e)
            {
                #region Retry or log it
                if (e.Message.IndexOf("Server Error", StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    if (m_RetriedTimes < InternalVariables.MS_ReTryTimes)
                    {
                        System.Threading.Thread.Sleep(InternalVariables.MS_ReTryInterval * 1000);
                        goto ReTryLabel;
                    }
                    else
                    {
                        log.Result = "Fail";
                        log.ErrorMessage = e.Message;
                        transferFacade.UpdateSAPDataTransferLog(log);
                        return new ServiceResult(false, e.Message, log.TransactionCode);
                    }
                }
                else
                {
                    if (e is System.Net.WebException)
                    {
                        if (((System.Net.WebException)e).Status == WebExceptionStatus.Timeout)
                        {
                            if (m_RetriedTimes < InternalVariables.MS_ReTryTimes)
                            {
                                System.Threading.Thread.Sleep(InternalVariables.MS_ReTryInterval * 1000);
                                goto ReTryLabel;
                            }
                            else
                            {
                                log.Result = "Fail";
                                log.ErrorMessage = e.Message;
                                transferFacade.UpdateSAPDataTransferLog(log);
                                return new ServiceResult(false, e.Message, log.TransactionCode);
                            }
                        }
                        else
                        {
                            log.Result = "Fail";
                            log.ErrorMessage = e.Message;
                            transferFacade.UpdateSAPDataTransferLog(log);
                            return new ServiceResult(false, e.Message, log.TransactionCode);
                        }
                    }
                    else
                    {
                        log.Result = "Fail";
                        log.ErrorMessage = e.Message;
                        transferFacade.UpdateSAPDataTransferLog(log);
                        return new ServiceResult(false, e.Message, log.TransactionCode);
                    }
                }
                #endregion
            }
            #endregion

            // Generate Material Object and Save it
            if (string.Compare(returnValue.FLAG, "Y", true) == 0)
            {
                int materialCount = returnValue.Material_Tab.Length;

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                this.DataProvider.BeginTransaction();

                try
                {
                    ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                    ModelFacade modelFacade = new ModelFacade(this.DataProvider);
                    BenQGuru.eMES.Domain.MOModel.Material material;
                    DT_MES_MATERIAL_TAB_RESP materialObj;
                    DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                    #region Get Parameters
                    // Get CheckOPCode and LotSize
                    SystemSettingFacade ssf = new SystemSettingFacade(this.DataProvider);
                    string finishGoodCheckOPCode = "AWGJC1";
                    object para = ssf.GetParameter("FINISHEDPRODUCTOPCODE", "ITEMCHECKOP");
                    if (para != null)
                    {
                        finishGoodCheckOPCode = (para as Parameter).ParameterAlias.Trim().ToUpper();
                    }
                    string semiGoodCheckOPCode = "ATSH1";
                    para = ssf.GetParameter("SEMIMANUFACTUREOPCODE", "ITEMCHECKOP");
                    if (para != null)
                    {
                        semiGoodCheckOPCode = (para as Parameter).ParameterAlias.Trim().ToUpper();
                    }
                    int finishGoodLotSize = 200;
                    para = ssf.GetParameter("FINISHEDPRODUCTLOTSIZE", "ITEMLOTSIZE");
                    if (para != null)
                    {
                        finishGoodLotSize = int.Parse((para as Parameter).ParameterAlias.Trim());
                    }
                    int semiGoodLotSize = 500;
                    para = ssf.GetParameter("SEMIMANUFACTURELOTSIZE", "ITEMLOTSIZE");
                    if (para != null)
                    {
                        semiGoodLotSize = int.Parse((para as Parameter).ParameterAlias.Trim());
                    }

                    // Get NeedCheckCarton and NeedCheckAccessory from parameter table
                    string importFGCheckCarton = "1";
                    para = ssf.GetParameter("FGIMPORTCHKCARTON", "CHECKPACKING");
                    if (para != null)
                    {
                        importFGCheckCarton = (para as Parameter).ParameterAlias.Trim();
                    }
                    string exportFGCheckCarton = "1";
                    para = ssf.GetParameter("FGEXPORTCHKCARTON", "CHECKPACKING");
                    if (para != null)
                    {
                        exportFGCheckCarton = (para as Parameter).ParameterAlias.Trim();
                    }
                    string importFGCheckAccessory = "1";
                    para = ssf.GetParameter("FGIMPORTCHKACCESSORY", "CHECKPACKING");
                    if (para != null)
                    {
                        importFGCheckAccessory = (para as Parameter).ParameterAlias.Trim();
                    }
                    string exportFGCheckAccessory = "0";
                    para = ssf.GetParameter("FGEXPORTCHKACCESSORY", "CHECKPACKING");
                    if (para != null)
                    {
                        exportFGCheckAccessory = (para as Parameter).ParameterAlias.Trim();
                    }
                    #endregion
                                        
                    for (int i = 0; i < materialCount; i++)
                    {
                        #region Material Object
                        bool needAddNew = false;
                        materialObj = returnValue.Material_Tab[i];
                        object oldMaterial = itemFacade.GetMaterial(InternalVariables.MatchItemCode(materialObj.Itemcode.ToUpper()), int.Parse(materialObj.OrgID.Trim()));
                                                
                        if (oldMaterial == null)
                        {
                            material = itemFacade.CreateNewMaterial();
                            material.MaintainUser = "SAP";
                            material.EAttribute1 = "";
                            material.CheckStatus = "";
                            material.MaterialParseType = "";
                            material.MaterialCheckType = "";
                            material.MaterialModelGroup = "";

                            if (string.Compare(materialObj.ITEMCONTROL, "A", true) == 0
                                || string.Compare(materialObj.ITEMCONTROL, "B", true) == 0)
                            {
                                material.MaterialCheckType = OPBOMDetailCheckType.CHECK_LINKBARCODE;
                            }

                            needAddNew = true;                            
                        }
                        else
                        {
                            material = oldMaterial as BenQGuru.eMES.Domain.MOModel.Material;
                            needAddNew = false;
                        }

                        #region 属性赋值
                        /*---------------------  Web Service 节点数据 ---------------------*/
                        material.MaterialCode = InternalVariables.MatchItemCode(materialObj.Itemcode.ToUpper());
                        material.OrganizationID = int.Parse(materialObj.OrgID);
                        material.MaterialName = materialObj.itemname.ToUpper();
                        material.MaterialDescription = materialObj.itemname.ToUpper();
                        material.MaterialUOM = materialObj.ITEMUOM;

                        if (string.Compare(materialObj.ITEMTYPE, "FERT", true) == 0)
                        {
                            material.MaterialType = ItemType.ITEMTYPE_FINISHEDPRODUCT;
                        }
                        else if (string.Compare(materialObj.ITEMTYPE, "HALB", true) == 0)
                        {
                            material.MaterialType = ItemType.ITEMTYPE_SEMIMANUFACTURE;
                        }
                        else if (string.Compare(materialObj.ITEMTYPE, "ROH", true) == 0)
                        {
                            material.MaterialType = ItemType.ITEMTYPE_RAWMATERIAL;
                        }
                        else
                        {
                            continue;
                        }

                        material.MaterialMachineType = materialObj.ItemMachineType;
                        material.MaterialVolume = materialObj.Itemvolume;
                        material.MaterialGroup = materialObj.ItemGroup;
                        material.MaterialGroupDescription = materialObj.ItemGroupDesc.ToUpper();
                        material.MaterialModelGroup = "";

                        if (string.Compare(materialObj.ITEMCONTROL, "A", true) == 0)
                        {
                            material.MaterialControlType = BOMItemControlType.ITEM_CONTROL_KEYPARTS;
                        }
                        else if (string.Compare(materialObj.ITEMCONTROL, "B", true) == 0)
                        {
                            material.MaterialControlType = BOMItemControlType.ITEM_CONTROL_LOT;
                        }
                        else
                        {
                            material.MaterialControlType = BOMItemControlType.ITEM_CONTROL_NOCONTROL;
                        }

                        if (materialObj.itemname.Split('.').Length > 3)
                        {
                            material.MaterialModelCode = materialObj.itemname.Split('.')[2];
                        }
                        else
                        {
                            material.MaterialModelCode = "";
                        }

                        if (materialObj.itemname.Contains("中国"))
                        {
                            material.MaterialExportImport = "IMPORT";
                        }
                        else
                        {
                            material.MaterialExportImport = "EXPORT";
                        }

                        if (material.MaterialDescription.Length >= 3)
                        {
                            if (string.Compare(material.MaterialDescription.Substring(material.MaterialDescription.Length - 3, 3),
                                "ROH", true) == 0)
                            {
                                material.ROHS = "Y";
                            }
                            else
                            {
                                material.ROHS = "";
                            }
                        }
                        else
                        {
                            material.ROHS = "";
                        } 
                        #endregion

                        // Save
                        if (needAddNew)
                        {
                            itemFacade.AddMaterial(material);
                        }
                        else
                        {
                            itemFacade.UpdateMaterial(material);
                        }
                        #endregion

                        #region Finish Good & Semi Finish Good
                        /*------------ 如果ItemType是半成品或者成品,则同时更新或插入tblitem----------*/
                        if (material.MaterialType == ItemType.ITEMTYPE_FINISHEDPRODUCT ||
                            material.MaterialType == ItemType.ITEMTYPE_SEMIMANUFACTURE)
                        {
                            needAddNew = false;
                            BenQGuru.eMES.Domain.MOModel.Item item;
                            object oldItem = itemFacade.GetItem(material.MaterialCode.ToUpper(), material.OrganizationID);

                            if (oldItem == null)
                            {
                                item = itemFacade.CreateNewItem();

                                /*-------------- 固定数据 ----------*/
                                item.ItemVersion = "";
                                item.MaintainUser = "SAP";
                                item.ItemUser = "SAP";
                                item.ItemDate = currentDateTime.DBDate;
                                item.EAttribute1 = "";
                                item.ItemConfigration = "";
                                item.ItemCartonQty = 1;
                                item.ItemBurnInQty = 0;
                                item.ElectricCurrentMaxValue = 0;
                                item.ElectricCurrentMinValue = 0;
                                item.ItemProductCode = "";

                                if (material.MaterialType == ItemType.ITEMTYPE_FINISHEDPRODUCT)
                                {
                                    item.CheckItemOP = finishGoodCheckOPCode;
                                    item.LotSize = finishGoodLotSize;
                                }
                                else if (material.MaterialType == ItemType.ITEMTYPE_SEMIMANUFACTURE)
                                {
                                    item.CheckItemOP = semiGoodCheckOPCode;
                                    item.LotSize = semiGoodLotSize;
                                }

                                if (material.MaterialType == ItemType.ITEMTYPE_FINISHEDPRODUCT)
                                {
                                    if (material.MaterialExportImport == "IMPORT")
                                    {
                                        item.NeedCheckCarton = importFGCheckCarton;
                                        item.NeedCheckAccessory = importFGCheckAccessory;
                                    }
                                    else
                                    {
                                        item.NeedCheckCarton = exportFGCheckCarton;
                                        item.NeedCheckAccessory = exportFGCheckAccessory;
                                    }
                                }
                                else
                                {
                                    item.NeedCheckCarton = "0";
                                    item.NeedCheckAccessory = "0";
                                }

                                needAddNew = true;                                
                            }
                            else
                            {
                                item = oldItem as Item;

                                needAddNew = false;                                
                            }                           

                            /*----- Web Service 节点数据 -----*/
                            item.ItemCode = material.MaterialCode;
                            item.OrganizationID = material.OrganizationID;
                            item.ItemName = material.MaterialName.ToUpper();
                            item.ItemDescription = material.MaterialName.ToUpper();
                            item.ItemUOM = material.MaterialUOM;
                            item.ItemType = material.MaterialType;
                            item.ItemControlType = material.MaterialControlType;

                            if (needAddNew)
                            {
                                itemFacade.AddItem(item);
                            }
                            else
                            {
                                itemFacade.UpdateItem(item);
                            }

                            // ItemSNCheck
                            if (itemFacade.GetItem2SNCheck(item.ItemCode, ItemCheckType.ItemCheckType_SERIAL) == null)
                            {
                                Item2SNCheck item2SNCheck = new Item2SNCheck();
                                item2SNCheck.ItemCode = item.ItemCode;
                                item2SNCheck.EAttribute1 = "";
                                item2SNCheck.MaintainUser = "SAP";
                                if (material.MaterialType == ItemType.ITEMTYPE_FINISHEDPRODUCT)
                                {
                                    item2SNCheck.SNLength = 24;
                                    item2SNCheck.SNPrefix = item.ItemCode;
                                }
                                else // ITEMTYPE_SEMIMANUFACTURE
                                {
                                    item2SNCheck.SNLength = 10;
                                    item2SNCheck.SNPrefix = "";
                                }
                                item2SNCheck.SNContentCheck = "Y";
                                item2SNCheck.Type = "SERIAL";
                                itemFacade.AddItem2SNCheck(item2SNCheck);
                            }

                            /*----------- 物料所属物料别插入tblmodel2item中，取物料料号的第二位为物料别 ---------*/
                            object oldModel2Item = modelFacade.GetModel2Item(item.ItemCode.Substring(1, 1), item.ItemCode, item.OrganizationID);
                            if (oldModel2Item == null)
                            {
                                Model2Item model2Item = new Model2Item();
                                model2Item.ModelCode = item.ItemCode.Substring(1, 1);
                                model2Item.ItemCode = item.ItemCode;
                                model2Item.OrganizationID = item.OrganizationID;
                                model2Item.MaintainUser = "SAP";

                                modelFacade.AddModel2Item(model2Item);
                            }

                            /*---- 在tblmodel2item中Distinct tblmodel2item.modelcode,如果不存在于tblmodel中，则插入tblmodel ----*/
                            object oldModel = modelFacade.GetModel(item.ItemCode.Substring(1, 1), item.OrganizationID);
                            if (oldModel == null)
                            {
                                Model model = new Model();
                                model.ModelCode = item.ItemCode.Substring(1, 1);
                                model.OrganizationID = item.OrganizationID;
                                model.MaintainUser = "SAP";
                                model.ModelDescription = model.ModelCode;
                                model.DataLinkQty = 0;
                                model.DimQty = 0;
                                model.EAttribute1 = "";
                                model.IsCheckDataLink = "0";
                                model.IsDim = "0";
                                model.IsInventory = "1";
                                model.IsReflow = "0";

                                modelFacade.AddModel(model);
                            }
                        }
                        #endregion
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

                log.ReceivedRecordCount = materialCount;
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

        public void Dispose()
        {

        }

        public object GetArguments()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MaterialTransferArgument(this.DataProvider);
            }
            return this.m_Argument;
        }

        public void SetArguments(object arguments)
        {
            this.m_Argument = arguments as MaterialTransferArgument;
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

            if (this.m_Argument.MaintainDate_B.AddDays(InternalVariables.MS_MaxDaysOfPeriod) < this.m_Argument.MaintainDate_E)
            {
                returnMessage = "时间区间应该小于" + InternalVariables.MS_MaxDaysOfPeriod.ToString() + "天";
                return false;
            }

            return true;
        }

        public object NewTransactionCode()
        {
            if (this.m_Argument == null)
            {
                this.m_Argument = new MaterialTransferArgument(this.DataProvider);
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
