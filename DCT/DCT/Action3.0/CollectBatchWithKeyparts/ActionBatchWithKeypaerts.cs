using System;
using System.Collections;
using System.Text.RegularExpressions;

using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;

using UserControl;

namespace BenQGuru.eMES.Common.DCT.Action
{
    public class ActionBatchWithKeypaerts : BaseDCTAction
    {
        public Hashtable keypartsHT = null;
        public ArrayList opBomDetailList = new ArrayList();
        public ArrayList opBomdetailRealCollectList = new ArrayList();
        public ArrayList opBomdetailKeypartList = new ArrayList();
        public int opBomDetailCount = 0;
        public int opBomDetailCollectNum = 0;
        public int opBomDetailkeypartCount = 0;
        public object[] objMinNo;
        public object[] objBomDetail;
        public object[] objBomDetailNotFilter;
        public string ID = string.Empty;
        public string moCode = string.Empty;

        public ActionBatchWithKeypaerts()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
        }
        public override Messages PreAction(object act)
        {
            DataCollect.Action.ActionEventArgs args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
            if (this.ObjectState != null)
            {
                args = (DataCollect.Action.ActionEventArgs)this.ObjectState;
            }
            else
            {
                args.RunningCard = act.ToString().ToUpper();

            }

            Messages msg = new Messages();
            if (this.keypartsHT == null)
            {
                ID = act.ToString().ToUpper();
                Messages msgCheck = CheckProduct(act, args.RunningCard);
                if (msgCheck.IsSuccess() == false)
                {
                    this.RefreshNumber();

                    ProcessBeforeReturn(this.Status, msgCheck);
                    return msgCheck;
                }

                msg = msgCheck;
            }

            base.PreAction(act);

            ProcessBeforeReturn(this.Status, msg);
            if (opBomdetailKeypartList.Count == 0)
            {
                this.FlowDirect = FlowDirect.WaitingOutput;
            }
            return msg;
        }

        public override Messages Action(object act)
        {
            Messages msg = new Messages();
            ActionOnLineHelper _helper = null;

            BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;

            if (act == null)
            {
                return msg;
            }

            DataCollect.Action.ActionEventArgs args;
            if (ObjectState == null)
            {
                args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
                args.RunningCard = act.ToString().ToUpper().Trim();
            }
            else
            {
                args = ObjectState as DataCollect.Action.ActionEventArgs;
            }

            string data = act.ToString().ToUpper().Trim();
            //Laws Lu,2006/06/03	添加	获取已有连接
            if ((act as IDCTClient).DBConnection != null)
            {
                domainProvider = (act as IDCTClient).DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
            }
            else
            {
                domainProvider = Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()
                    as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
                (act as IDCTClient).DBConnection = domainProvider;
            }

            DataCollectFacade dataCollect = new DataCollect.DataCollectFacade(domainProvider);
            TSFacade tsFacade = new TSFacade(domainProvider);

            if (msg.IsSuccess())
            {

                MO moWillGo = (MO)keypartsHT["MOWillGo"];
                ProductInfo product = (ProductInfo)keypartsHT["ProdcutInfo"];

                Messages msgResult = new Messages();
                if (opBomDetailkeypartCount > opBomDetailCollectNum)
                {
                    try
                    {
                        object[] opBomDetailCompare = new object[opBomdetailKeypartList.Count];
                        opBomdetailKeypartList.CopyTo(opBomDetailCompare);

                        string parseTypeSetting = "," + ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).OPBOMParseType + ",";
                        string checkTypeSetting = "," + ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).OPBOMCheckType + ",";
                        bool checkStatus = ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).CheckStatus == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING;

                        string CheckNeedVendor = string.Empty;
                        if (!string.IsNullOrEmpty(((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).NeedVendor))
                        {
                            CheckNeedVendor = ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).NeedVendor;
                        }
                        string MItemCode = ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).OPBOMItemCode;
                        string OBSItemCode = ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).OPBOMSourceItemCode;
                        int inputLength = ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).SerialNoLength;

                        MaterialFacade facade = new MaterialFacade(domainProvider);
                        MINNO newMINNO = facade.CreateNewMINNO();
                        newMINNO.MItemCode = MItemCode;

                        Messages parseSuccess = new Messages();
                        Messages oldParseSuccess = new Messages();
                        Messages checkSuccess = new Messages();

                        parseSuccess.Add(new UserControl.Message(MessageType.Error, "$CS_Error_ParseFailed"));

                        int num = 0;

                        //勾选了料号比对,必须选择解析方式
                        if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                        {
                            if (parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_BARCODE.ToLower() + ",") < 0
                              && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PREPARE.ToLower() + ",") < 0
                              && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PRODUCT.ToLower() + ",") < 0)
                            {
                                msg.Add(new UserControl.Message(MessageType.Error, " $CS_Error_ParseFailed:$CheckCompareItem_Must_CheckOneParse"));
                                goto Label2;
                            }
                        }

                        //Parse from barcode
                        if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_BARCODE.ToLower() + ",") >= 0)
                        {
                            OPBOMDetail opBOMDetailForItemCode = null;
                            if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                            {
                                opBOMDetailForItemCode = ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]);
                            }
                            oldParseSuccess.AddMessages(parseSuccess);
                            parseSuccess = dataCollect.ParseFromBarcode(ref newMINNO, data, opBOMDetailForItemCode, inputLength);

                            num += 1;
                        }

                        //Parse from prepare
                        if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PREPARE.ToLower() + ",") >= 0)
                        {
                            Simulation sim = (Simulation)dataCollect.GetSimulation(ID);
                            newMINNO.MOCode = sim.MOCode;
                            OPBOMDetail opBOMDetailForItemCode = null;
                            if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                            {
                                opBOMDetailForItemCode = ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]);
                            }
                            oldParseSuccess.AddMessages(parseSuccess);
                            parseSuccess = dataCollect.ParseFromPrepare(ref newMINNO, data, opBOMDetailForItemCode, inputLength);

                            num += 1;
                        }
                        //Parse from product
                        if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PRODUCT.ToLower() + ",") >= 0)
                        {
                            Simulation sim = (Simulation)dataCollect.GetSimulation(ID);
                            newMINNO.MOCode = sim.MOCode;
                            OPBOMDetail opBOMDetailForItemCode = null;
                            if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                            {
                                opBOMDetailForItemCode = ((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]);
                            }
                            oldParseSuccess.AddMessages(parseSuccess);
                            parseSuccess = dataCollect.ParseFromProduct(ref newMINNO, checkStatus, data, opBOMDetailForItemCode, inputLength);

                            num += 1;
                        }

                        if (!parseSuccess.IsSuccess())
                        {
                            if (num > 0)
                            {
                                oldParseSuccess.AddMessages(parseSuccess);
                                msg.AddMessages(oldParseSuccess);
                                goto Label2;
                            }

                            if (num == 0)
                            {
                                if (inputLength > 0 && data.Trim().Length != inputLength)
                                {
                                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_SNLength_Wrong"));
                                    goto Label2;
                                }
                            }

                        }

                        //检查新上料是否在TS中而不可用
                        if (!tsFacade.RunningCardCanBeClollected(data, CardType.CardType_Part))
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$Error_Material_InTSOrScrapped $SERIAL_NO=" + data));
                            goto Label2;
                        }

                        #region Check if key part was used

                        if (((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_KEYPARTS
                            && KeyPartUsed(domainProvider, data, newMINNO.MItemCode, true))
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$CS_Error_KeyPartUsed"));
                            goto Label2;
                        }

                        #endregion


                        #region check NeedVendor

                        //如果VendorCode为空，到tblmaterial中获取
                        if (newMINNO.VendorCode == null || newMINNO.VendorCode.Trim().Length <= 0)
                        {
                            ItemFacade itemfacade = new ItemFacade(domainProvider);
                            object objMaterial = itemfacade.GetMaterial(newMINNO.MItemCode.Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
                            if (objMaterial != null)
                            {
                                newMINNO.VendorCode = ((BenQGuru.eMES.Domain.MOModel.Material)objMaterial).VendorCode;
                            }
                        }

                        if (CheckNeedVendor == NeedVendor.NeedVendor_Y)
                        {
                            Messages checkNeedVendor = new Messages();
                            checkNeedVendor = dataCollect.CheckNeedVebdor(newMINNO);

                            if (!checkNeedVendor.IsSuccess())
                            {
                                msg.AddMessages(checkNeedVendor);
                                goto Label2;
                            }
                        }

                        #endregion

                        if (checkSuccess.IsSuccess() && checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_LINKBARCODE.ToLower() + ",") >= 0)
                        {
                            //Link barcode
                            if (string.IsNullOrEmpty(newMINNO.MItemCode))
                            {
                                newMINNO.MItemCode = MItemCode;
                            }

                            newMINNO.MItemPackedNo = data;
                            newMINNO.EAttribute1 = MCardType.MCardType_Keyparts;

                            opBomdetailRealCollectList.Add((object)newMINNO);
                        }

                        ++opBomDetailCollectNum;
                    Label2:
                        if (opBomDetailkeypartCount > opBomDetailCollectNum)
                        {
                            if (((OPBOMDetail)opBomdetailKeypartList[opBomDetailCollectNum]).OPBOMItemControlType != null)
                            {
                                msg.Add(new UserControl.Message(MessageType.Normal, ">>$DCT_PLEASE_INPUT_Keyparts:" + ((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).OPBOMSourceItemCode));

                                ProcessBeforeReturn(this.Status, msg);
                                return msg;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msgResult.Add(new UserControl.Message(MessageType.Error, ex.Message));
                    }
                }

                if (opBomDetailkeypartCount == opBomDetailCollectNum)
                {
                    msg.AddMessages(this.minnoResolve(domainProvider));
                }
                if (!msg.IsSuccess())
                {
                    this.RefreshNumber();
                    base.Action(act);
                    ProcessBeforeReturn(this.Status, msg);
                    return msg;
                }

                #region Save
                domainProvider.BeginTransaction();
                try
                {
                    BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(domainProvider);
                    Resource resource = (Resource)dataModel.GetResource(args.ResourceCode);

                    _helper = new ActionOnLineHelper(domainProvider);
                    ActionCheckStatus actionCheckStatus = new ActionCheckStatus();
                    actionCheckStatus.ProductInfo = product;
                    actionCheckStatus.ProductInfo.Resource = resource;
                    ExtendSimulation lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;

                    BenQGuru.eMES.Material.WarehouseFacade wfacade = null;
                    if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                    {
                        wfacade = new WarehouseFacade(domainProvider);
                    }

                    IDCTClient client = act as IDCTClient;

                    //检查新上料是否在TS中而不可用
                    if (opBomdetailRealCollectList != null)
                    {
                        foreach (MINNO minno in opBomdetailRealCollectList)
                        {
                            if (!tsFacade.RunningCardCanBeClollected(minno.MItemPackedNo, CardType.CardType_Part))
                            {
                                msg.Add(new UserControl.Message(MessageType.Error, "$Error_Material_InTSOrScrapped $SERIAL_NO=" + minno.MItemPackedNo));
                                break;
                            }
                        }
                    }

                    #region Check if key part was used

                    if (opBomdetailRealCollectList != null)
                    {
                        foreach (MINNO minno in opBomdetailRealCollectList)
                        {
                            if (minno.EAttribute1 == MCardType.MCardType_Keyparts)
                            {
                                if (KeyPartUsed(domainProvider, minno.MItemPackedNo, minno.MItemCode, false))
                                {
                                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_Error_KeyPartUsed"));
                                    break;
                                }

                            }
                        }

                    }

                    #endregion

                    if (msg.IsSuccess())
                    {
                        object[] objOpBomdetailRealCollect = new object[opBomdetailRealCollectList.Count];
                        opBomdetailRealCollectList.CopyTo(objOpBomdetailRealCollect);

                        string strRCard = product.NowSimulation.RunningCard;


                        if (objOpBomdetailRealCollect != null)
                        {
                            msg.AddMessages(_helper.ActionWithTransaction(
                                new CINNOActionEventArgs(
                                ActionType.DataCollectAction_CollectINNO, strRCard,
                                client.LoginedUser,
                                client.ResourceCode,
                                product,
                                string.Empty,
                                wfacade), actionCheckStatus, objOpBomdetailRealCollect));
                        }
                    }

                    #region 物料过账

                    if (msg.IsSuccess())
                    {
                        BaseModelFacade bMfacade = new BaseModelFacade(domainProvider);
                        object objOP = bMfacade.GetOperationByResource(client.ResourceCode);
                        string strRCard = product.NowSimulation.RunningCard;

                        object[] objOpBomdetailRealCollect = new object[opBomdetailRealCollectList.Count];
                        opBomdetailRealCollectList.CopyTo(objOpBomdetailRealCollect);

                        if (opBomdetailRealCollectList != null && opBomdetailRealCollectList.Count > 0)
                        {
                            Messages messagesNew = new Messages();
                            string ItemCode = string.Empty;

                            DataCollectFacade dataCollectFacade = new DataCollectFacade(domainProvider);
                            object objectSimulation = dataCollectFacade.GetSimulation(strRCard);
                            if (objectSimulation != null)
                            {
                                ItemCode = ((Simulation)objectSimulation).ItemCode;
                            }

                            foreach (MINNO minno in opBomdetailRealCollectList)
                            {
                                messagesNew.AddMessages(_helper.ActionWithTransaction(new TryEventArgs(
                                    ActionType.DataCollectAction_TryNew, client.LoginedUser, ((Operation2Resource)objOP).OPCode, client.ResourceCode,
                                    ItemCode, strRCard, minno.MItemCode, minno.MItemPackedNo, string.Empty, true, true)));
                            }
                            msg.AddMessages(messagesNew);
                        }
                    }

                    #endregion

                    if (msg.IsSuccess())
                    {
                        if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                        {
                            if (wfacade != null)
                                wfacade.ExecCacheSQL();
                        }

                        #region 增加良品采集
                        {
                            Resource Resource = (Resource)dataModel.GetResource(client.ResourceCode);

                            Messages messages1 = new Messages();
                            if (actionCheckStatus.ProductInfo == null)
                            {
                                messages1 = _helper.GetIDInfo(ID);
                                actionCheckStatus.ProductInfo = (ProductInfo)messages1.GetData().Values[0];
                                actionCheckStatus.ProductInfo.Resource = Resource;
                                lastSimulation = actionCheckStatus.ProductInfo.LastSimulation;
                                msg.AddMessages(messages1);
                            }
                            else	//将上一个Action的NowSimulation设置为本Action的LastSimulation
                            {
                                if (actionCheckStatus.ActionList.Count > 0)
                                {
                                    actionCheckStatus.ProductInfo = new ProductInfo();
                                    actionCheckStatus.ProductInfo.NowSimulation = new Simulation();
                                    actionCheckStatus.ProductInfo.Resource = Resource;
                                    actionCheckStatus.ProductInfo.LastSimulation =
                                        new ExtendSimulation((Simulation)(new DataCollectFacade(domainProvider)).GetLastSimulationOrderByDateAndTime(ID));
                                }
                            }

                            product = actionCheckStatus.ProductInfo;

                            // Changed end
                            if (msg.IsSuccess())
                            {

                                messages1.AddMessages(_helper.ActionWithTransaction(new ActionEventArgs(ActionType.DataCollectAction_GOOD, ID,
                                    client.LoginedUser, client.ResourceCode,
                                    product), actionCheckStatus));
                            }

                        }
                        #endregion

                    }

                    //检查新上料是否在TS中而不可用                    
                    if (opBomdetailRealCollectList != null)
                    {
                        foreach (MINNO minno in opBomdetailRealCollectList)
                        {
                            if (!tsFacade.RunningCardCanBeClollected(minno.MItemPackedNo, CardType.CardType_Part))
                            {
                                msg.Add(new UserControl.Message(MessageType.Error, "$Error_Material_InTSOrScrapped $SERIAL_NO=" + minno.MItemPackedNo));
                            }
                        }
                    }

                    if (msg.IsSuccess())
                    {
                        domainProvider.CommitTransaction();
                        msg.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_CollectSuccess")));
                    }
                    else
                    {
                        domainProvider.RollbackTransaction();
                    }
                }
                catch (Exception ex)
                {
                    domainProvider.RollbackTransaction();

                    msg.Add(new UserControl.Message(ex));
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)domainProvider).PersistBroker.CloseConnection();
                }                

                #endregion
            }

            if (msg.IsSuccess())
            {                
                base.Action(act);

                this.RefreshNumber();
            }

            ProcessBeforeReturn(this.Status, msg);
            return msg;
        }

        private void RefreshNumber()
        {
            keypartsHT = null;
            opBomDetailList.Clear();
            opBomdetailRealCollectList.Clear();
            opBomdetailKeypartList.Clear();
            opBomDetailCount = 0;
            opBomDetailCollectNum = 0;
            opBomDetailkeypartCount = 0;
            ID = string.Empty;
            moCode = string.Empty;
            objMinNo = null;
            objBomDetail = null;
        }


        public override Messages AftAction(object act)
        {
            base.AftAction(act);

            return null;
        }
        private Messages minnoResolve(Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
        {
            Messages msg = new Messages();
            if (objMinNo != null && objMinNo.Length > 0)
            {
                for (int i = 0; i < objMinNo.Length; i++)
                {
                    try
                    {

                        OPBOMFacade opBOMFacade = new OPBOMFacade(domainProvider);
                        DataCollectFacade dataCollect = new DataCollectFacade(domainProvider);

                        string routeCode = ((MINNO)objMinNo[i]).RouteCode;
                        string moCode = ((MINNO)objMinNo[i]).MOCode;
                        string opCode = ((MINNO)objMinNo[i]).OPCode;
                        string mItemCode = ((MINNO)objMinNo[i]).MItemCode;
                        string barcode = ((MINNO)objMinNo[i]).MItemPackedNo;
                        int Qty = 0;
                        object opBomDetailNew = objBomDetailNotFilter[0];
                        if (objBomDetailNotFilter != null && objBomDetailNotFilter.Length > 0)
                        {
                            for (int j = 0; j < objBomDetailNotFilter.Length; j++)
                            {
                                if (((OPBOMDetail)objBomDetailNotFilter[j]).OPBOMItemCode == mItemCode
                                    && ((OPBOMDetail)objBomDetailNotFilter[j]).OPBOMSourceItemCode == ((MINNO)objMinNo[i]).MSourceItemCode)
                                {
                                    opBomDetailNew = objBomDetailNotFilter[j];
                                    break;
                                }
                            }
                        }

                        string parseTypeSetting = "," + ((OPBOMDetail)opBomDetailNew).OPBOMParseType + ",";
                        string checkTypeSetting = "," + ((OPBOMDetail)opBomDetailNew).OPBOMCheckType + ",";
                        bool checkStatus = ((OPBOMDetail)opBomDetailNew).CheckStatus == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING;

                        string CheckNeedVendor = string.Empty;
                        if (!string.IsNullOrEmpty(((OPBOMDetail)opBomDetailNew).NeedVendor))
                        {
                            CheckNeedVendor = ((OPBOMDetail)opBomDetailNew).NeedVendor;
                        }

                        string MItemCode = ((OPBOMDetail)opBomDetailNew).OPBOMItemCode;
                        string OBSItemCode = ((MINNO)objMinNo[i]).MSourceItemCode;
                        int inputLength = ((OPBOMDetail)opBomDetailNew).SerialNoLength;

                        Qty = Convert.ToInt32(((OPBOMDetail)opBomDetailNew).OPBOMItemQty);
                        MaterialFacade materialFacade = new MaterialFacade(domainProvider);
                        materialFacade = new MaterialFacade(domainProvider);
                        MINNO newMINNO = materialFacade.CreateNewMINNO();
                        newMINNO.MItemCode = mItemCode;

                        Messages parseSuccess = new Messages();
                        Messages oldParseSuccess = new Messages();
                        Messages checkSuccess = new Messages();


                        parseSuccess.Add(new UserControl.Message(MessageType.Error, "$CS_Error_CollectLot$CS_Error_ParseFailed"));

                        int num = 0;


                        //勾选了料号比对,必须选择解析方式
                        if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                        {
                            if (parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_BARCODE.ToLower() + ",") < 0
                              && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PREPARE.ToLower() + ",") < 0
                              && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PRODUCT.ToLower() + ",") < 0)
                            {
                                msg.Add(new UserControl.Message(MessageType.Error, "$CS_Error_CollectLot$CS_Error_ParseFailed:$CheckCompareItem_Must_CheckOneParse:" + mItemCode + ""));
                                return msg;
                            }
                        }

                        //Parse from barcode
                        if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_BARCODE.ToLower() + ",") >= 0)
                        {
                            OPBOMDetail opBOMDetailForItemCode = null;
                            if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                            {
                                opBOMDetailForItemCode = ((OPBOMDetail)opBomDetailNew);
                            }
                            oldParseSuccess.AddMessages(parseSuccess);
                            parseSuccess = dataCollect.ParseFromBarcode(ref newMINNO, barcode, opBOMDetailForItemCode, inputLength);

                            num += 1;
                        }
                        //Parse from prepare
                        if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PREPARE.ToLower() + ",") >= 0)
                        {
                            Simulation sim = (Simulation)dataCollect.GetSimulation(ID);
                            newMINNO.MOCode = sim.MOCode;
                            OPBOMDetail opBOMDetailForItemCode = null;
                            if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                            {
                                opBOMDetailForItemCode = ((OPBOMDetail)opBomDetailNew);
                            }
                            oldParseSuccess.AddMessages(parseSuccess);
                            parseSuccess = dataCollect.ParseFromPrepare(ref newMINNO, barcode, opBOMDetailForItemCode, inputLength);

                            num += 1;
                        }
                        //Parse from product
                        if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PRODUCT.ToLower() + ",") >= 0)
                        {
                            OPBOMDetail opBOMDetailForItemCode = null;
                            if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                            {
                                opBOMDetailForItemCode = ((OPBOMDetail)opBomDetailNew);
                            }
                            oldParseSuccess.AddMessages(parseSuccess);
                            parseSuccess = dataCollect.ParseFromProduct(ref newMINNO, checkStatus, barcode, opBOMDetailForItemCode, inputLength);

                            num += 1;
                        }

                        if (!parseSuccess.IsSuccess())
                        {
                            if (num > 0)
                            {
                                oldParseSuccess.AddMessages(parseSuccess);
                                msg.AddMessages(oldParseSuccess);
                                return msg;
                            }

                            if (num == 0)
                            {
                                if (inputLength > 0 && barcode.Trim().Length != inputLength)
                                {
                                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_SNLength_Wrong"));
                                    return msg;
                                }
                            }
                        }

                        #region check NeedVendor

                        //如果VendorCode为空，到tblmaterial中获取
                        if (newMINNO.VendorCode == null || newMINNO.VendorCode.Trim().Length <= 0)
                        {
                            ItemFacade itemfacade = new ItemFacade(domainProvider);
                            object objMaterial = itemfacade.GetMaterial(newMINNO.MItemCode.Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
                            if (objMaterial != null)
                            {
                                newMINNO.VendorCode = ((BenQGuru.eMES.Domain.MOModel.Material)objMaterial).VendorCode;
                            }
                        }

                        if (CheckNeedVendor == NeedVendor.NeedVendor_Y)
                        {
                            Messages checkNeedVendor = new Messages();
                            checkNeedVendor = dataCollect.CheckNeedVebdor(newMINNO);

                            if (!checkNeedVendor.IsSuccess())
                            {
                                msg.AddMessages(checkNeedVendor);
                                return msg;
                            }
                        }
                        #endregion

                        //Link barcode
                        if (checkSuccess.IsSuccess() && checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_LINKBARCODE.ToLower() + ",") >= 0)
                        {
                            if (string.IsNullOrEmpty(newMINNO.MItemCode))
                            {
                                newMINNO.MItemCode = MItemCode;
                            }

                            newMINNO.Qty = Qty;
                            newMINNO.MItemPackedNo = barcode;

                            if (((OPBOMDetail)opBomDetailNew).OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_KEYPARTS)
                            {
                                newMINNO.EAttribute1 = MCardType.MCardType_Keyparts;
                            }
                            else if (((OPBOMDetail)opBomDetailNew).OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_LOT)
                            {
                                newMINNO.EAttribute1 = MCardType.MCardType_INNO;
                            }

                            opBomdetailRealCollectList.Add((object)newMINNO);
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, ex.Message));
                    }
                }
            }
            return msg;
        }

        private bool KeyPartUsed(SQLDomainDataProvider sqlDomainDataProvider, string keyPartRCard, string MItemCode, bool checkCache)
        {
            if (checkCache)
            {
                //Check objAllList
                if (opBomdetailRealCollectList != null)
                {
                    foreach (MINNO minno in opBomdetailRealCollectList)
                    {
                        if (minno.EAttribute1 == MCardType.MCardType_Keyparts
                            && minno.MItemPackedNo.Trim().ToUpper() == keyPartRCard.Trim().ToUpper())
                        {
                            return true;
                        }
                    }
                }
            }

            //Check DB
            DataCollectFacade dataCollectFacade = new DataCollectFacade(sqlDomainDataProvider);
            object[] onWIPItemList = dataCollectFacade.QueryOnWIPItem(keyPartRCard, MItemCode);
            if (onWIPItemList != null)
            {
                foreach (OnWIPItem onWIPItem in onWIPItemList)
                {
                    if (onWIPItem.MCardType == MCardType.MCardType_Keyparts
                        && onWIPItem.ActionType == (int)MaterialType.CollectMaterial)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private Messages CheckProduct(object act, string runningCard)
        {
            // 查询产品信息
            BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;
            if ((act as IDCTClient).DBConnection != null)
            {
                domainProvider = (act as IDCTClient).DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
            }
            else
            {
                domainProvider = Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()
                    as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
                (act as IDCTClient).DBConnection = domainProvider;
            }

            try
            {
                //为改善性能               
                ((SQLDomainDataProvider)domainProvider).PersistBroker.AutoCloseConnection = false;
                ((SQLDomainDataProvider)domainProvider).PersistBroker.OpenConnection();

                ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider);

                // Added by Icyer 2007/03/16	如果归属工单，则做归属工单检查，否则做序列号途程检查

                UserControl.Messages msgProduct = _helper.GetIDInfo(runningCard);
                ProductInfo product = (ProductInfo)msgProduct.GetData().Values[0];

                MO moWillGo = null;
                ActionGoToMO actionGoMO = new ActionGoToMO(domainProvider);
                Messages msgMo = actionGoMO.GetItemCodeFromGoMoRCard(((IDCTClient)act).ResourceCode, runningCard);
                if (msgMo.IsSuccess() == false)		// 如果有错误，表示需要归属工单，但是解析工单或查询工单发生错误
                {
                    return msgMo;
                }
                if (msgMo.GetData() != null)	// 需要归属工单，做归属工单检查
                {
                    product.NowSimulation = new BenQGuru.eMES.Domain.DataCollect.Simulation();
                    UserControl.Message msgMoData = msgMo.GetData();
                    moWillGo = (MO)msgMoData.Values[0];
                    moCode = moWillGo.MOCode;
                    ActionGoToMO goToMO = new ActionGoToMO(domainProvider);
                    GoToMOActionEventArgs MOActionEventArgs = new GoToMOActionEventArgs(
                        ActionType.DataCollectAction_GoMO,
                        runningCard,
                        ((IDCTClient)act).LoginedUser,
                        ((IDCTClient)act).ResourceCode,
                        product,
                        moWillGo.MOCode);
                    msgMo = goToMO.CheckIn(MOActionEventArgs);
                    if (!MOActionEventArgs.PassCheck)
                    {
                        msgMo = _helper.CheckID(new CKeypartsActionEventArgs(
                            ActionType.DataCollectAction_CollectINNO,
                            runningCard,
                            ((IDCTClient)act).LoginedUser,
                            ((IDCTClient)act).ResourceCode,
                            product, null, null));
                    }
                }
                else	// 不需要归属工单，检查序列号途程
                {
                    if (product == null || product.LastSimulation == null)
                    {
                        msgProduct.ClearMessages();
                        msgProduct.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
                        return msgProduct;
                    }
                    msgMo = _helper.CheckID(new CKeypartsActionEventArgs(
                        ActionType.DataCollectAction_CollectINNO,
                        runningCard,
                        ((IDCTClient)act).LoginedUser,
                        ((IDCTClient)act).ResourceCode,
                        product, null, null));
                }
                if (msgMo.IsSuccess() == false)
                {
                    return msgMo;
                }
                // Added end

                keypartsHT = new Hashtable();
                keypartsHT.Add("ProdcutInfo", product);
                keypartsHT.Add("MOWillGo", moWillGo);

                IDCTClient client = act as IDCTClient;
                OPBOMFacade opBOMFacade = new OPBOMFacade(domainProvider);
                MOFacade moFacade = new MOFacade(domainProvider);
                MO moNew = (MO)moFacade.GetMO(product.NowSimulation.MOCode);
                MaterialFacade materialFacade = new MaterialFacade(domainProvider);

                object[] objBomDetailKeyPart = null;

                Messages messages1 = new Messages();
                if (moWillGo == null)
                {
                    // 检查途程
                    messages1 = _helper.CheckID(
                        new CKeypartsActionEventArgs(
                        ActionType.DataCollectAction_CollectINNO,
                        product.LastSimulation.RunningCard,
                        client.LoginedUser,
                        client.ResourceCode,
                        product,
                        null,
                        null));
                }
                if (messages1.IsSuccess() == true)
                {
                    //objBomDetail = opBOMFacade.GetOPBOMDetails(
                    //    product.NowSimulation.MOCode,
                    //    product.NowSimulation.RouteCode,
                    //    product.NowSimulation.OPCode);

                    //Miodified by Scott
                    objBomDetail = opBOMFacade.QueryOPBOMDetail(product.NowSimulation.ItemCode, string.Empty, string.Empty, moNew.BOMVersion,
                        product.NowSimulation.RouteCode, product.NowSimulation.OPCode, (int)MaterialType.CollectMaterial,
                        int.MinValue, int.MaxValue, moNew.OrganizationID, true);

                    if (objBomDetail == null)
                    {
                        messages1.Add(new UserControl.Message(MessageType.Error, "$CS_OPBOM_NotFound"));
                        return messages1;
                    }
                }
                else
                {
                    return messages1;
                }

                objBomDetailNotFilter = objBomDetail;

                this.filterOpBomDetail(ref objBomDetail);

                if (objBomDetail == null || objBomDetail.Length <= 0)
                {
                    messages1.Add(new UserControl.Message(MessageType.Error, "$CS_OPBOM_NotFound"));
                    return messages1;
                }

                if (messages1.IsSuccess() == true)
                {
                    objBomDetailKeyPart = opBOMFacade.GetOPBOMDetails(product.NowSimulation.MOCode,
                        product.NowSimulation.RouteCode,
                        product.NowSimulation.OPCode, true, true);
                }
                else
                {
                    return messages1;
                }

                if (objBomDetailKeyPart != null)
                {
                    this.filterOpBomDetailKeypart(ref objBomDetailKeyPart);
                }

                if (messages1.IsSuccess() == true)
                {
                    //objMinNo = materialFacade.QueryMINNO(product.NowSimulation.MOCode, 
                    //    product.NowSimulation.RouteCode,
                    //    product.NowSimulation.OPCode,
                    //    client.ResourceCode, 
                    //    moNew.BOMVersion);


                    //Miodified by Scott
                    //获取批控管上料资料
                    //注意：检查tblminno中的数据是否能对应到tblopbomdetail中
                    object[] tempMinNo = materialFacade.QueryMINNO(product.NowSimulation.MOCode, product.NowSimulation.RouteCode,
                        product.NowSimulation.OPCode, client.ResourceCode, moNew.BOMVersion);
                    if (tempMinNo == null)
                    {
                        objMinNo = null;
                    }
                    else
                    {
                        ArrayList minnoList = new ArrayList();
                        foreach (MINNO minno in tempMinNo)
                        {
                            bool found = false;

                            if (objBomDetail != null)
                            {
                                foreach (OPBOMDetail opBOMDetail in objBomDetailNotFilter)
                                {
                                    if (minno.MSourceItemCode == opBOMDetail.OPBOMSourceItemCode
                                        && opBOMDetail.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_LOT
                                        && minno.MItemCode == opBOMDetail.OPBOMItemCode)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }

                            if (found)
                                minnoList.Add(minno);
                        }
                        objMinNo = minnoList.ToArray();
                    }
                }
                else
                {
                    return messages1;
                }

                //MOFacade moFacade = new MOFacade(domainProvider);
                //object mo = moFacade.GetMO( product.LastSimulation.MOCode );
                object mo = moWillGo;
                if (moWillGo == null)
                {
                    mo = moFacade.GetMO(product.LastSimulation.MOCode);
                }

                //OPBomKeyparts opBomKeyparts = new OPBomKeyparts(objBomDetail, Convert.ToInt32(((MO)mo).IDMergeRule), domainProvider);
                // 查询OPBOM
                if (objBomDetail == null)
                {
                    msgProduct.Add(new Message(MessageType.Error, "$CS_NOOPBomInfo $CS_Param_MOCode=" + product.NowSimulation.MOCode
                        + " $CS_Param_RouteCode=" + product.NowSimulation.RouteCode
                        + " $CS_Param_OPCode =" + product.NowSimulation.OPCode));
                    return msgProduct;
                }
                else
                {
                    int KeypartNum = 0;
                    int MinnoNum = 0;
                    if (objBomDetailKeyPart != null)
                        KeypartNum = objBomDetailKeyPart.Length;
                    if (objMinNo != null)
                        MinnoNum = objMinNo.Length;
                    if (objBomDetail.Length > (KeypartNum + MinnoNum))
                    {
                        msgProduct.Add(new UserControl.Message(MessageType.Error, "$CS_LotControl_notFull"));
                        return msgProduct;
                    }
                    else
                    {
                        for (int i = 0; i < objBomDetail.Length; i++)
                        {
                            opBomDetailCount += Convert.ToInt32(((OPBOMDetail)objBomDetail[i]).OPBOMItemQty);
                        }
                        for (int i = 0; i < objBomDetail.Length; i++)
                        {
                            int number = Convert.ToInt32(((OPBOMDetail)objBomDetail[i]).OPBOMItemQty);
                            for (int j = 0; j < number; j++)
                            {
                                opBomDetailList.Add(objBomDetail[i]);
                            }
                        }

                        if (objBomDetailKeyPart != null)
                        {
                            for (int i = 0; i < objBomDetailKeyPart.Length; i++)
                            {
                                opBomDetailkeypartCount += Convert.ToInt32(((OPBOMDetail)objBomDetailKeyPart[i]).OPBOMItemQty);
                            }

                            if (objBomDetailKeyPart != null && objBomDetailKeyPart.Length > 0)
                            {
                                for (int i = 0; i < objBomDetailKeyPart.Length; i++)
                                {
                                    int number = Convert.ToInt32(((OPBOMDetail)objBomDetailKeyPart[i]).OPBOMItemQty);
                                    for (int j = 0; j < number; j++)
                                    {
                                        opBomdetailKeypartList.Add(objBomDetailKeyPart[i]);
                                    }
                                }
                            }
                        }
                        if (objBomDetailKeyPart != null)
                        {
                            msgProduct.Add(new UserControl.Message(MessageType.Normal, ">>$DCT_PLEASE_INPUT_Keyparts:" + ((OPBOMDetail)objBomDetailKeyPart[0]).OPBOMSourceItemCode));
                            //this.OutMesssage = new UserControl.Message(MessageType.Normal, ">>$DCT_PLEASE_INPUT_Keyparts:" + ((OPBOMDetail)objBomDetailKeyPart[0]).OPBOMItemCode);
                        }

                        keypartsHT.Add("Opbomdetail", objBomDetail);
                        keypartsHT.Add("OpbomdetailKeypart", objBomDetailKeyPart);
                    }
                }

                return msgProduct;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ((SQLDomainDataProvider)domainProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)domainProvider).PersistBroker.AutoCloseConnection = true;
            }
        }




        private void filterOpBomDetailKeypart(ref object[] _objBomDetailKeyPart)
        {
            ArrayList filterList = new ArrayList();
            bool checkFilter = false;
            for (int i = 0; i < _objBomDetailKeyPart.Length; i++)
            {
                if (((OPBOMDetail)_objBomDetailKeyPart[i]).OPBOMItemControlType != BOMItemControlType.ITEM_CONTROL_KEYPARTS
                   && ((OPBOMDetail)_objBomDetailKeyPart[i]).OPBOMItemControlType != BOMItemControlType.ITEM_CONTROL_LOT)
                {
                    continue;
                }

                if (i == 0)
                {
                    filterList.Add(_objBomDetailKeyPart[i]);
                }
                else
                {
                    for (int j = 0; j < filterList.Count; j++)
                    {
                        if (((OPBOMDetail)_objBomDetailKeyPart[i]).OPBOMSourceItemCode == ((OPBOMDetail)filterList[j]).OPBOMSourceItemCode)
                        {
                            checkFilter = false;
                            break;
                        }
                        else
                        {
                            checkFilter = true;
                        }
                    }
                    if (checkFilter)
                        filterList.Add(_objBomDetailKeyPart[i]);
                }
            }
            _objBomDetailKeyPart = new object[filterList.Count];
            filterList.CopyTo(_objBomDetailKeyPart);

        }

        private void filterOpBomDetail(ref object[] _objBomDetail)
        {
            ArrayList filterList = new ArrayList();
            bool checkFilter = true;
            for (int i = 0; i < _objBomDetail.Length; i++)
            {
                if (((OPBOMDetail)_objBomDetail[i]).OPBOMItemControlType != BOMItemControlType.ITEM_CONTROL_KEYPARTS
                    && ((OPBOMDetail)_objBomDetail[i]).OPBOMItemControlType != BOMItemControlType.ITEM_CONTROL_LOT)
                {
                    continue;
                }

                if (i == 0)
                {
                    filterList.Add(_objBomDetail[i]);
                }
                else
                {
                    for (int j = 0; j < filterList.Count; j++)
                    {
                        if (((OPBOMDetail)_objBomDetail[i]).OPBOMSourceItemCode == ((OPBOMDetail)filterList[j]).OPBOMSourceItemCode)
                        {
                            checkFilter = false;
                            break;
                        }
                        else
                        {
                            checkFilter = true;
                        }
                    }

                    if (checkFilter)
                        filterList.Add(_objBomDetail[i]);
                }
            }
            _objBomDetail = new object[filterList.Count];
            filterList.CopyTo(_objBomDetail);
        }
    }
}
