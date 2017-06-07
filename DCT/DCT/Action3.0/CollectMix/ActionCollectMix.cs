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
    public class ActionCollectMix : BaseDCTAction
    {
        public Hashtable keypartsHT = null;
        public ArrayList opBomDetailList = new ArrayList();
        public ArrayList opBomdetailRealCollectList = new ArrayList();
        public int opBomDetailCount = 0;
        public int opBomDetailCollectNum = 0;
        public string ID = string.Empty;
        public string moCode = string.Empty;

        public ActionCollectMix()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
        }

        public override Messages PreAction(object act)
        {
            // Added by Icyer 2006/12/14
            // 输入产品序列号是检查
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
                    this.ObjectState = null;
                    this.keypartsHT = null;
                    opBomDetailList.Clear();
                    opBomdetailRealCollectList.Clear();
                    opBomDetailCount = 0;
                    opBomDetailCollectNum = 0;
                    ID = string.Empty;

                    ProcessBeforeReturn(this.Status, msgCheck);
                    return msgCheck;
                }

                msg = msgCheck;
            }

            this.ObjectState = args;
            base.PreAction(act);

            ProcessBeforeReturn(this.Status, msg);
            return msg;
        }

        // Added by Icyer 2006/12/15
        // 检查产品
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
                object[] objBomDetail = null;
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
                    MO moNew = (MO)moFacade.GetMO(product.NowSimulation.MOCode);

                    objBomDetail = opBOMFacade.QueryOPBOMDetail(product.NowSimulation.ItemCode, string.Empty, string.Empty, moNew.BOMVersion,
                        product.NowSimulation.RouteCode, product.NowSimulation.OPCode, (int)MaterialType.CollectMaterial,
                        int.MinValue, int.MaxValue, moNew.OrganizationID, true);

                    if (objBomDetail == null)
                    {
                        messages1.Add(new UserControl.Message(MessageType.Error, "$CS_OPBOM_NotFound"));
                    }
                }
                else
                {
                    return messages1;
                }

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

                    this.filterOpBomDetail(ref objBomDetail);

                    if (objBomDetail == null || objBomDetail.Length <= 0)
                    {
                        msgProduct.Add(new Message(MessageType.Error, "$CS_OPBOM_NotFound"));
                        return msgProduct;
                    }

                    for (int i = 0; i < objBomDetail.Length; i++)
                    {
                        if (((OPBOMDetail)objBomDetail[i]).OPBOMItemControlType == "item_control_lot")
                        {
                            opBomDetailCount += 1;
                        }
                        else
                        {
                            opBomDetailCount += Convert.ToInt32(((OPBOMDetail)objBomDetail[i]).OPBOMItemQty);
                        }
                    }
                    for (int i = 0; i < objBomDetail.Length; i++)
                    {
                        if (((OPBOMDetail)objBomDetail[i]).OPBOMItemControlType == "item_control_lot")
                        {
                            opBomDetailList.Add(objBomDetail[i]);
                        }
                        else
                        {
                            int number = Convert.ToInt32(((OPBOMDetail)objBomDetail[i]).OPBOMItemQty);
                            for (int j = 0; j < number; j++)
                            {
                                opBomDetailList.Add(objBomDetail[i]);
                            }
                        }
                    }

                    if (((OPBOMDetail)objBomDetail[0]).OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_LOT)
                    {
                        msgProduct.Add(new UserControl.Message(MessageType.Normal, ">>$DCT_PLEASE_INPUT_Lot " + ((OPBOMDetail)objBomDetail[0]).OPBOMSourceItemCode));
                    }
                    else
                    {
                        msgProduct.Add(new UserControl.Message(MessageType.Normal, ">>$DCT_PLEASE_INPUT_Keyparts " + ((OPBOMDetail)objBomDetail[0]).OPBOMSourceItemCode));
                    }
                    // keypartsHT.Add("KeypartsInfo", opBomKeyparts);
                    keypartsHT.Add("Opbomdetail", objBomDetail);
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
        // Added end

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

                if (opBomDetailCount > opBomDetailCollectNum)
                {
                    try
                    {
                        object[] opBomDetailCompare = new object[opBomDetailList.Count];
                        opBomDetailList.CopyTo(opBomDetailCompare);
                        string ItemCode = string.Empty;

                        string parseTypeSetting = "," + ((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).OPBOMParseType + ",";
                        string checkTypeSetting = "," + ((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).OPBOMCheckType + ",";
                        bool checkStatus = ((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).CheckStatus == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING;

                        string CheckNeedVendor = string.Empty;
                        if (!string.IsNullOrEmpty(((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).NeedVendor))
                        {
                            CheckNeedVendor = ((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).NeedVendor;
                        }

                        string MItemCode = ((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).OPBOMItemCode;
                        string OBSItemCode = ((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).OPBOMSourceItemCode;
                        int inputLength = ((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).SerialNoLength;

                        MaterialFacade facade = new MaterialFacade(domainProvider);
                        MINNO newMINNO = facade.CreateNewMINNO();
                        newMINNO.MItemCode = MItemCode;

                        Messages parseSuccess = new Messages();
                        Messages oldParseSuccess = new Messages();

                        parseSuccess.Add(new UserControl.Message(MessageType.Error, "$CS_Error_ParseFailed:"));
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
                                opBOMDetailForItemCode = (OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum];
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
                                opBOMDetailForItemCode = (OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum];
                            }
                            oldParseSuccess.AddMessages(parseSuccess);
                            parseSuccess = dataCollect.ParseFromPrepare(ref newMINNO, data, opBOMDetailForItemCode, inputLength);

                            num += 1;
                        }
                        //Parse from product
                        if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PRODUCT.ToLower() + ",") >= 0)
                        {
                            OPBOMDetail opBOMDetailForItemCode = null;
                            if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                            {
                                opBOMDetailForItemCode = (OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum];
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

                        if (((OPBOMDetail)opBomDetailCompare[opBomDetailCollectNum]).OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_KEYPARTS
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

                        if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_LINKBARCODE.ToLower() + ",") >= 0)
                        {
                            //Link barcode
                            if (string.IsNullOrEmpty(newMINNO.MItemCode))
                            {
                                newMINNO.MItemCode = MItemCode;
                            }

                            newMINNO.MItemPackedNo = data;
                            if (((OPBOMDetail)opBomDetailList[opBomDetailCollectNum]).OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_KEYPARTS)
                            {
                                newMINNO.EAttribute1 = MCardType.MCardType_Keyparts;
                            }
                            else if (((OPBOMDetail)opBomDetailList[opBomDetailCollectNum]).OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_LOT)
                            {
                                newMINNO.EAttribute1 = MCardType.MCardType_INNO;
                                newMINNO.Qty = ((OPBOMDetail)opBomDetailList[opBomDetailCollectNum]).OPBOMItemQty;
                            }

                            opBomdetailRealCollectList.Add((object)newMINNO);
                        }

                        ++opBomDetailCollectNum;
                    Label2:

                        if (opBomDetailCount > opBomDetailCollectNum)
                        {
                            if (((OPBOMDetail)opBomDetailList[opBomDetailCollectNum]).OPBOMItemControlType != null &&
                                    ((OPBOMDetail)opBomDetailList[opBomDetailCollectNum]).OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_LOT)
                            {
                                msg.Add(new UserControl.Message(MessageType.Normal, ">>$DCT_PLEASE_INPUT_Lot:" + ((OPBOMDetail)opBomDetailList[opBomDetailCollectNum]).OPBOMSourceItemCode));

                                ProcessBeforeReturn(this.Status, msg);
                                return msg;
                            }
                            else
                            {

                                msg.Add(new UserControl.Message(MessageType.Normal, ">>$CS_PleaseInputKeypart:" + ((OPBOMDetail)opBomDetailList[opBomDetailCollectNum]).OPBOMSourceItemCode));

                                ProcessBeforeReturn(this.Status, msg);
                                return msg;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, ex.Message));
                    }
                }


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



                        msg.AddMessages(_helper.ActionWithTransaction(
                            new CINNOActionEventArgs(
                            ActionType.DataCollectAction_CollectINNO, strRCard,
                            client.LoginedUser,
                            client.ResourceCode,
                            product,
                            string.Empty,
                            wfacade), actionCheckStatus, objOpBomdetailRealCollect));
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
                                    //actionCheckStatus.ProductInfo.LastSimulation =
                                    //    new ExtendSimulation(((ActionEventArgs)actionCheckStatus.ActionList[actionCheckStatus.ActionList.Count - 1]).ProductInfo.NowSimulation);
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

            }

            if (msg.IsSuccess())
            {
                base.Action(act);

                this.ObjectState = null;
                this.keypartsHT = null;
                this.opBomDetailList.Clear();
                this.opBomDetailCount = 0;
                this.opBomDetailCollectNum = 0;
                this.opBomdetailRealCollectList.Clear();
                ID = string.Empty;
            }

            ProcessBeforeReturn(this.Status, msg);
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



        private void filterOpBomDetail(ref object[] _objBomDetail)
        {
            ArrayList filterList = new ArrayList();
            bool checkFilter = false;
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
