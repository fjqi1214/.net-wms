using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Domain.LotPackage;
using BenQGuru.eMES.LotDataCollect.Action;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.LotDataCollect;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.LotDataCollect
{
    /// <summary>
    /// DataCollectFacade 的摘要说明。
    /// 文件名:		DataCollectFacade.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
    /// 创建日期:	2005-03-31 14:21:26
    /// 修改人:Mark Lee
    /// 修改日期:20050331
    /// 描 述:	
    /// 版 本:	
    /// </summary>
    public class DataCollectFacade
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        //Laws Lu,2006/06/27 add support variant factory
        public const string PID = "PID";
        public const string HID = "HID";
        public const string POWER = "POWER";

        public DataCollectFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        #region 检查
        /// <summary>
        /// 只支持线上的采集，TS、REWORK、线外站应该自己检查、填写
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="actionType"></param>
        /// <param name="resourceCode"></param>
        /// <param name="userCode"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public Messages CheckID(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            return CheckID(iD, actionType, resourceCode, userCode, product, null);
        }
        // Added by Icyer 2005/10/28
        // 扩展一个带ActionCheckStatus参数的方法
        public Messages CheckID(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, ActionCheckStatus actionCheckStatus)
        {
            // Added by Icyer 2005/11/11
            // 检查actionCheckStatus中的MOCode是否与product中的一致
            if (actionCheckStatus != null)
            {
                if (actionCheckStatus.ProductInfo != null)
                {
                    //Laws Lu，2005/11/11，新增	判断LastSimulation为Null的情况
                    if (actionCheckStatus.ProductInfo.LastSimulation != null)
                    {
                        if (actionCheckStatus.ProductInfo.LastSimulation.MOCode != product.LastSimulation.MOCode)
                        {
                            actionCheckStatus = new ActionCheckStatus();
                        }
                    }
                }
            }
            // Added end

            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "CheckID");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {

                //对于全部送修样本都是“送修”状态（没有经过送修确认），此时允许批退
                //批退后的不良不用维修也不可以维修，系统实现方式上请你们做考虑。

                BenQGuru.eMES.TS.TSFacade tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider);
                //Laws Lu,需要修改	曾经走过TS处理,没有考虑多次TS的情况,MOCode和Sequence也应该作为关键参数,
                //Laws Lu,2005/10/26,修改	改善性能

                BenQGuru.eMES.Domain.TS.TS ts = null;
                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_NG ||
                    product.LastSimulation.LastAction == ActionType.DataCollectAction_SMTNG ||
                    product.LastSimulation.LastAction == ActionType.DataCollectAction_OQCNG ||
                    product.LastSimulation.LastAction == ActionType.DataCollectAction_OutLineNG ||
                    product.LastSimulation.LastAction == ActionType.DataCollectAction_BurnOutNG)  //Add by sandy on 20140530
                {

                    ts = (BenQGuru.eMES.Domain.TS.TS)tsFacade.GetCardLastTSRecord(product.LastSimulation.LotCode);
                    product.LastTS = ts;
                }

                //2005/08/29,修改	不考虑FQC批退时，送修没有确认的情况
                if (actionType == ActionType.DataCollectAction_OQCReject
                    && ts != null
                    && ts.FromInputType == TS.TSFacade.TSSource_OnWIP
                    && ts.TSStatus == BenQGuru.eMES.Web.Helper.TSStatus.TSStatus_New
                    && ts.RunningCardSequence == product.LastSimulation.LotSeq)
                {
                    //什么都不做return messages;
                }
                else
                {
                    if (ts != null
                        && ts.FromInputType == TS.TSFacade.TSSource_OnWIP
                        && ts.TSStatus == BenQGuru.eMES.Web.Helper.TSStatus.TSStatus_New
                        && actionType != ActionType.DataCollectAction_OffMo)/*2005/12/21,Laws Lu,修改	允许脱离工单*/
                    {
                        //Laws Lu,2005/09/09,新增	检查NG重复的情况

                        switch (actionType)
                        {
                            case ActionType.DataCollectAction_OQCNG:
                            case ActionType.DataCollectAction_SMTNG:
                            case ActionType.DataCollectAction_NG:
                            case ActionType.DataCollectAction_OutLineNG:
                            case ActionType.DataCollectAction_OutLineReject:
                            case ActionType.DataCollectAction_BurnOutNG:   //Add by sandy on 20140530
                                {
                                    if (product.LastSimulation.LastAction == actionType)
                                    {
                                        throw new Exception("$CS_NG_PLEASE_SEND_TS");
                                    }
                                    break;
                                }
                            //Karron Qiu,2005-10-25
                            //如果用户采集良品信息时，被采集的产品已经是不良品，
                            //请将提示信息调整到与不良品采集时一致，“该产品是不良品，请送维修处理”
                            case ActionType.DataCollectAction_GOOD:
                            case ActionType.DataCollectAction_Carton:   // Added By Hi1/venus.Feng on 20080814 for Hisense Version
                            case ActionType.DataCollectAction_BurnIn:   //Add by sandy on 20140530
                            case ActionType.DataCollectAction_BurnOutGood:   //Add by sandy on 20140530
                                {
                                    if (product.LastSimulation.LastAction == ActionType.DataCollectAction_NG ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_OQCNG ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_SMTNG ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_OutLineNG ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_OutLineReject ||
                                        product.LastSimulation.LastAction == ActionType.DataCollectAction_BurnOutNG)   //Modify by sandy on 20140530
                                    {
                                        throw new Exception("$CS_NG_PLEASE_SEND_TS");
                                    }
                                    break;
                                }
                        }
                    }
                    DoCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                }

            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }
        // Added end


        //Laws Lu,2005/09/09,新增	检查
        private void DoCheck(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            if (product.CurrentMO == null)
            {
                this.CheckMO(product.LastSimulation.MOCode, product);
            }
            else
            {
                this.CheckMO(product.CurrentMO);
            }

            this.GetRouteOPOnline(iD, actionType, resourceCode, userCode, product);
            this.CheckActionOnlineAndOutline(actionType, product);
            this.CheckCardStatus(product);
            this.CheckRepeatCollect(actionType, product);

            //Laws Lu,2006/05/30	if first op,break off carton information
            if (GetMORouteFirstOP(product.NowSimulation.MOCode, product.NowSimulation.RouteCode).OPCode
                == product.CurrentItemRoute2OP.OPCode)
            {
                if (product.NowSimulation.CartonCode != String.Empty)
                {
                    Package.PackageFacade pf = new Package.PackageFacade(DataProvider);
                    pf.SubtractCollected(((ExtendSimulation)product.LastSimulation).CartonCode);
                }

                ((ExtendSimulation)product.LastSimulation).CartonCode = String.Empty;

                product.NowSimulation.CartonCode = String.Empty;


            }
        }
        // Added by Icyer 2005/10/28
        private void DoCheck(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, ActionCheckStatus actionCheckStatus)
        {
            if (actionCheckStatus == null || actionCheckStatus.CheckedID == false)
            {
                if (actionCheckStatus == null || actionCheckStatus.CheckedMO == false)
                {
                    this.CheckMO(product.LastSimulation.MOCode, product);
                    if (actionCheckStatus != null)
                    {
                        actionCheckStatus.CheckedMO = true;
                    }
                }
                this.GetRouteOPOnline(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                this.CheckActionOnlineAndOutline(actionType, product);

                // Move the follow code after if
                //this.CheckRepeatCollect(actionType, product); 

                if (actionCheckStatus != null)
                {
                    actionCheckStatus.CheckedID = true;
                }
            }
            else
            {
                this.GetRouteOPOnline(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
            }
            // 是否重复采集
            this.CheckCardStatus(product);
            this.CheckRepeatCollect(actionType, product);

        }
        // Added end

        public Messages CheckIDOutline(string iD, string actionType, string resourceCode, string opCode, string userCode, ProductInfo product)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "CheckID");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                /* check the status of mo whether is Release or open */
                this.CheckMO(product.LastSimulation.MOCode, product);
                this.GetRouteOPOutline(iD, actionType, resourceCode, opCode, userCode, product);
                this.CheckActionOnlineAndOutline(actionType, product);
                this.CheckCardStatus(product);
                this.CheckRepeatCollect(actionType, product);

                //				#region 填写新SIMULATION
                //				if (messages.IsSuccess())
                //				{
                //					//更改
                //					if (!(product.LastSimulation.OPCode == product.NowSimulation.OPCode))
                //					{
                //						product.NowSimulation.ActionList= ";"+actionType+";";
                //					}
                //				}
                //				#endregion
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        //根据前一次Simulation 结果, 预先填 新的 Simulation 记录

        public Messages WriteLotSimulation(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "CheckID");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                product.NowSimulation.MOCode = product.LastSimulation.MOCode;
                product.NowSimulation.ItemCode = product.LastSimulation.ItemCode;
                product.NowSimulation.ModelCode = product.LastSimulation.ModelCode;
                product.NowSimulation.LotCode = iD;
                product.NowSimulation.LotSeq = product.LastSimulation.LotSeq + 1;
                product.NowSimulation.FromRoute = ActionOnLineHelper.StringNull;
                product.NowSimulation.FromOP = ActionOnLineHelper.StringNull;
                product.NowSimulation.RouteCode = product.LastSimulation.RouteCode;
                product.NowSimulation.OPCode = product.LastSimulation.OPCode;
                product.NowSimulation.ResCode = resourceCode;
                product.NowSimulation.ProductStatus = ProductStatus.GOOD;
                product.NowSimulation.LastAction = actionType;
                product.NowSimulation.ActionList = string.Format("{0}{1};", product.LastSimulation.ActionList, actionType);
                //检查是否完工  TODO
                product.NowSimulation.IsComplete = ProductComplete.NoComplete;
                product.NowSimulation.CollectStatus = CollectStatus.CollectStatus_BEGIN;
                product.NowSimulation.LotStatus = LotStatusForMO2LotLink.LOTSTATUS_USE;
                product.NowSimulation.LotQty = product.LastSimulation.LotQty;
                product.NowSimulation.GoodQty = product.LastSimulation.GoodQty;
                product.NowSimulation.NGQty = product.LastSimulation.NGQty;
                product.NowSimulation.CartonCode = product.LastSimulation.CartonCode;
                product.NowSimulation.LotNo = product.LastSimulation.LotNo;
                product.NowSimulation.PalletCode = product.LastSimulation.PalletCode;
                product.NowSimulation.NGTimes = product.LastSimulation.NGTimes;
                //Laws Lu,2006/07/05 support RMA
                product.NowSimulation.RMABillCode = product.LastSimulation.RMABillCode;
                product.NowSimulation.MOSeq = product.LastSimulation.MOSeq;     // Added by icyer 2007/07/03

            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        public LotSimulation CloneLotSimulation(LotSimulation simulation)
        {
            LotSimulation returnValue = CreateNewLotSimulation();

            Type simulationType = typeof(LotSimulation);
            FieldInfo[] fieldInfoList = simulationType.GetFields();
            if (fieldInfoList != null)
            {
                foreach (FieldInfo fieldInfo in fieldInfoList)
                {
                    fieldInfo.SetValue(returnValue, fieldInfo.GetValue(simulation));
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 检查ID在一个工单中是否已经被使用
        /// 修 改:		Jane Shu	2005-07-21
        ///					改传参SQL
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="mOCode"></param>
        /// <returns></returns>
        public LotOnWip CheckIDIsUsed(string id, string moCode)
        {
            object[] onWips = this.DataProvider.CustomQuery(typeof(LotOnWip),
                new SQLParamCondition(
                string.Format("select {0} from TBLLotONWIP where LOTCODE = $LOTCODE and MOCODE = $MOCODE order by LOTSEQ desc",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotOnWip))),
                new SQLParameter[]{ new SQLParameter( "LOTCODE", typeof(string), id ) ,
									  new SQLParameter( "MOCODE", typeof(string), moCode) }));

            if (onWips == null)
                return null;
            if (onWips.Length > 0)
                return (LotOnWip)onWips[0];
            return null;
        }

        #region 检查
        //ID状态检查
        public bool CheckCardStatus(ProductInfo productInfo)
        {
            if (productInfo.LastSimulation.ProductStatus != ProductStatus.GOOD)
            {
                if (((ExtendSimulation)productInfo.LastSimulation).AdjustProductStatus != ProductStatus.GOOD)
                /*&& productInfo.NowSimulation.LastAction != ActionType.DataCollectAction_OffMo)/*2005/12/21,Laws Lu,新增 允许脱离工单*/
                {
                    throw new Exception("$CS_ProductStatusError $CS_Param_ProductStatus=" + MutiLanguages.ParserString(productInfo.LastSimulation.ProductStatus)
                        + " $CS_Param_ID=" + productInfo.LastSimulation.LotCode);
                }
            }

            if (productInfo.LastSimulation.LotStatus == LotStatusForMO2LotLink.LOTSTATUS_STOP)
            {
                throw new Exception("$CS_LotStatusError $CS_Param_LotStatus=" + MutiLanguages.ParserString(productInfo.LastSimulation.LotStatus)
                   + " $CS_Param_ID=" + productInfo.LastSimulation.LotCode);
            }


            return true;
        }

        public bool CheckMO(string moCode, ProductInfo product)
        {
            MOFacade moFacade = new MOFacade(this.DataProvider);

            MO mo = null;
            if (product.CurrentMO == null)
            {
                mo = (MO)moFacade.GetMO(moCode);
            }
            else
            {
                mo = product.CurrentMO;
            }
            //工单状态检查
            bool moStatus = this.CheckMO(mo);
            if (!moStatus)
            {
                throw new Exception("$CS_MOStatus_Error $CS_Param_MOStatus=$" + mo.MOStatus);
            }

            product.CurrentMO = mo;

            return moStatus;
        }

        //工单状态检查
        public bool CheckMO(MO mo)
        {
            if ((mo.MOStatus == MOManufactureStatus.MOSTATUS_RELEASE) ||
                (mo.MOStatus == MOManufactureStatus.MOSTATUS_OPEN))
                return true;
            else
                return false;
        }

        private const char isSelected = '1';

        //根据工序设定检查动作可否在该工序做
        public bool CheckAction(ProductInfo productInfo, object op, string actionType)
        {
            string opCode = string.Empty;
            string opControl = "0000000000000000000000";

            if (op is Operation)
            {
                opCode = ((Operation)op).OPCode;
                opControl = ((Operation)op).OPControl;
            }

            if (op is ItemRoute2OP)
            {
                opCode = ((ItemRoute2OP)op).OPCode;
                opControl = ((ItemRoute2OP)op).OPControl;
            }

            return this.CheckActionOnlineAndOutline(actionType, opCode, opControl);
        }

        public bool CheckActionOnlineAndOutline(string actionType, ProductInfo productInfo)
        {
            string opCode = productInfo.NowSimulation.OPCode;
            string opControl = "0000000000000000000000";

            if ((actionType == ActionType.DataCollectAction_OutLineGood)
                || (actionType == ActionType.DataCollectAction_OutLineNG)
                || (actionType == ActionType.DataCollectAction_OutLineReject))
            {
                BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
                Operation op = (Operation)dataModel.GetOperation(opCode);
                if (op != null)
                {
                    opControl = op.OPControl;
                }
            }
            else
            {
                //Laws Lu,2005/12/27，修改	允许缓存工单途程信息
                ItemRoute2OP itemRoute2OP = (ItemRoute2OP)this.GetMORouteOP(productInfo.NowSimulation.ItemCode, productInfo.NowSimulation.MOCode, productInfo.NowSimulation.RouteCode, productInfo.NowSimulation.OPCode, productInfo);

                if (itemRoute2OP != null)
                {
                    opControl = itemRoute2OP.OPControl;
                }
            }
            return this.CheckActionOnlineAndOutline(actionType, opCode, opControl);
        }
        public bool CheckActionOnlineAndOutline(string actionType, string opCode, string opControl)
        {
            switch (actionType)
            {
                case ActionType.DataCollectAction_GOOD:
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTGOOD:
                case ActionType.DataCollectAction_SMTNG:
                    if (opControl[(int)OperationList.Testing] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_TestOP $CS_Param_OPCode =" + opCode);
                    }
                    break;

                case ActionType.DataCollectAction_GoMO:
                /*2005/12/15，Laws Lu，新增脱离工单采集*/
                case ActionType.DataCollectAction_OffMo:
                    break;
                // Added by hi1/Venus.Feng on 20080716 for Hisense Version
                case ActionType.DataCollectAction_Carton:
                    if (opControl[(int)OperationList.Packing] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_PackOP $CS_Param_OPCode =" + opCode);
                    }
                    break;
                case ActionType.DataCollectAction_DropMaterial:
                    break;
                case ActionType.DataCollectAction_ECN:
                    break;
                case ActionType.DataCollectAction_TRY:
                    break;
                case ActionType.DataCollectAction_SoftINFO:
                    break;
                case ActionType.DataCollectAction_Reject:
                    break;

                case ActionType.DataCollectAction_CollectINNO:
                case ActionType.DataCollectAction_CollectKeyParts:
                    if (opControl[(int)OperationList.ComponentLoading] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_INNOOP $CS_Param_OPCode =" + opCode);
                    }
                    break;

                case ActionType.DataCollectAction_Split:
                case ActionType.DataCollectAction_Convert:
                case ActionType.DataCollectAction_IDTran:
                    if (opControl[(int)OperationList.IDTranslation] != isSelected)
                        throw new Exception("$CS_OP_Not_SplitOP $CS_Param_OPCode =" + opCode);
                    break;

                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_OutLineReject:
                    if (opControl[(int)OperationList.OutsideRoute] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_OutLineOP $CS_Param_OPCode = " + opCode);
                    }
                    break;

                case ActionType.DataCollectAction_LOT:
                case ActionType.DataCollectAction_OQCLotAddID:
                case ActionType.DataCollectAction_OQCLotRemoveID:
                    if (opControl[(int)OperationList.Packing] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_PackOP $CS_Param_OPCode =" + opCode);
                    }
                    break;
                case ActionType.DataCollectAction_OQCPass:
                case ActionType.DataCollectAction_OQCReject:
                case ActionType.DataCollectAction_OQCGood:
                case ActionType.DataCollectAction_OQCNG:
                    // Marked By Hi1/venus.feng on 20080721 for Hisense Version : Don't check op for OQCNG、OQCPass、OQCReject
                    /*
                    if (opControl[(int) OperationList.OQC]!=isSelected)
					{
						throw new Exception("$CS_OP_Not_OQCOP $CS_Param_OPCode =" + opCode);
					}
                    */
                    break;

                /* added by jessie lee, 2006-5-30, 添加burn in action */
                case ActionType.DataCollectAction_BurnIn:
                    if (opControl[(int)OperationList.BurnIn] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_BurnInOP $CS_Param_OPCode =" + opCode);
                    }
                    break;
                /* added by jessie lee, 2006-5-30, 添加burn out action */
                case ActionType.DataCollectAction_BurnOutGood:
                case ActionType.DataCollectAction_BurnOutNG:  //Add by sandy on 20140530
                    if (opControl[(int)OperationList.BurnOut] != isSelected)
                    {
                        throw new Exception("$CS_OP_Not_BurnOutOP $CS_Param_OPCode =" + opCode);
                    }
                    break;
                default:
                    throw new Exception("$CS_SystemError_CheckID_Not_SupportAction:" + actionType);
            }
            return true;
        }


        public void CheckRepeatCollect(string actionType, ProductInfo product)
        {
            if (product.LastSimulation.OPCode != product.LastSimulation.NextOPCode &&
                product.LastSimulation.CollectStatus == CollectStatus.CollectStatus_BEGIN)
            {
                throw new Exception("$CS_LotSimulation_ISCollect");
            }
            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_OutLineReject:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                            //Laws Lu,2006/06/08	线外工序允许二次采集NG
                            if (product.LastTS != null && product.LastTS.RunningCardSequence != product.LastSimulation.LotSeq)
                            {
                                //this.CheckRepeatCollect(product.LastSimulation.LotCode, product.LastSimulation.ActionList, actionType);
                            }
                            product.NowSimulation.ActionList = string.Format("{0}{1};", product.LastSimulation.ActionList, actionType);
                            break;
                        default:
                            product.NowSimulation.ActionList = string.Format(";{0};", actionType);
                            break;
                    }
                    break;

                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_BurnOutNG:  //Add by sandy on 20140530，上一站为NG时，要本站的action替换ActionList
                    product.NowSimulation.ActionList = string.Format(";{0};", actionType);
                    break;

                default:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                            product.NowSimulation.ActionList = string.Format(";{0};", actionType);
                            break;

                        default:
                            if (product.LastSimulation.OPCode == product.NowSimulation.OPCode)
                            {
                                this.CheckRepeatCollect(product.LastSimulation.LotCode, product.LastSimulation.ActionList, actionType, product.LastSimulation.CollectStatus);
                                product.NowSimulation.ActionList = string.Format("{0}{1};", product.LastSimulation.ActionList, actionType);
                            }
                            else
                            {
                                product.NowSimulation.ActionList = string.Format(";{0};", actionType);
                            }
                            break;
                    }
                    break;
            }
        }
        public void CheckRepeatCollect(string rcard, string actionList, string action, string collectStatus)
        {
            if (collectStatus == CollectStatus.CollectStatus_BEGIN)
            {
                return;
            }
            actionList = ";" + actionList + ";";

            switch (action)
            {
                case ActionType.DataCollectAction_GoMO:
                    if ((actionList.IndexOf(";" + ActionType.DataCollectAction_GoMO + ";") >= 0))
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                /*添加脱离工单*/
                case ActionType.DataCollectAction_OffMo:
                    {
                        if (actionList.IndexOf(";" + ActionType.DataCollectAction_OffMo + ";") >= 0)
                        {
                            throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                        }
                        break;
                    }
                case ActionType.DataCollectAction_DropMaterial:
                    {
                        if (actionList.IndexOf(";" + ActionType.DataCollectAction_DropMaterial + ";") >= 0)
                        {
                            throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                        }
                        break;
                    }
                case ActionType.DataCollectAction_SMTGOOD:
                case ActionType.DataCollectAction_GOOD:
                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OQCGood:
                    string[] actions = actionList.Split(new char[] { ';' });

                    //if (actions.Length > 2) //因为上面在actionList的前后都加了分号
                    if (actions.Length > 3)
                    {
                        //string lastAction = actions[actions.Length - 2]; //因为上面在actionList的前后都加了分号
                        string lastAction = actions[actions.Length - 3];
                        if (lastAction == ActionType.DataCollectAction_GOOD
                            || lastAction == ActionType.DataCollectAction_GOOD
                            || lastAction == ActionType.DataCollectAction_SMTGOOD
                            || lastAction == ActionType.DataCollectAction_OutLineGood
                            || lastAction == ActionType.DataCollectAction_OQCGood)
                        {
                            throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                        }
                    }


                    break;

                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_OutLineReject:
                case ActionType.DataCollectAction_BurnOutNG:   //Add by sandy on 20140530
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_NG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_SMTNG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OutLineNG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCReject + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }

                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCNG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    //Add by sandy on 20140530
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_BurnOutNG + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_CollectINNO:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_CollectINNO + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_CollectKeyParts:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_CollectKeyParts + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_ECN:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_ECN + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_OQCLotAddID:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCLotAddID + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_OQCLotRemoveID:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCLotRemoveID + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;


                case ActionType.DataCollectAction_TRY:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_TRY + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_SoftINFO:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_SoftINFO + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_Split:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_Split + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_Convert:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_Convert + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_IDTran:
                    //					if (actionList.IndexOf(";"+ActionType.DataCollectAction_IDTran +";")>=0)
                    //					{
                    //						throw new Exception("$CS_RepeatCollect_OnOneOP");
                    //					}
                    break;

                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_OQCReject:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_Reject + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }

                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCReject + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }

                    break;
                case ActionType.DataCollectAction_OQCPass:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_OQCPass + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_BurnIn:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_BurnIn + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;

                case ActionType.DataCollectAction_BurnOutGood:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_BurnOutGood + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                case ActionType.DataCollectAction_Carton:
                    if (actionList.IndexOf(";" + ActionType.DataCollectAction_Carton + ";") >= 0)
                    {
                        throw new Exception("$CS_RepeatCollect_OnOneOP $CS_Param_ID :" + rcard);
                    }
                    break;
                default:
                    throw new Exception("$CS_Sys_Error_CheckRepeatCollect_Failed $CS_Param_Action=" + action + "  $CS_Param_ID :" + rcard);
            }
        }


        //Online 检查途程OP是否存在
        public void GetRouteOPOnline(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            GetRouteOPOnline(iD, actionType, resourceCode, userCode, product, null);
        }
        public void GetRouteOPOnline(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {


            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            //			BenQGuru.eMES.TS.TSFacade tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider);  
            //			//Laws Lu,需要修改	曾经走过TS处理,没有考虑多次TS的情况,MOCode和Sequence也应该作为关键参数,
            //			//Laws Lu,2005/10/26,修改	改善性能
            //			BenQGuru.eMES.Domain.TS.TS ts =  null;
            //			if(product.LastSimulation.ISTS == 1)
            //			{
            //				ts = (BenQGuru.eMES.Domain.TS.TS)tsFacade.GetCardLastTSRecord(product.LastSimulation.LotCode);
            //			}

            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_OutLineReject:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOutline");
                        default:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            //this.CheckOnlineOPSingle(iD, actionType, resourceCode, userCode, product); 
                            this.CheckOnlineOP(iD, actionType, resourceCode, userCode, product);
                            break;
                    }
                    break;
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_OQCReject:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_BurnOutNG:   //Add by sandy on 20140530
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOutline");
                        case ActionType.DataCollectAction_OQCReject:
                            {

                                //								if(ts != null 
                                //									&& (ts.TSStatus == Web.Helper.TSStatus.TSStatus_Scrap
                                //									|| ts.TSStatus == Web.Helper.TSStatus.TSStatus_Split))
                                //								{
                                //									return;
                                //								}

                                this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                                this.CheckOnlineOPSingle(iD, actionType, resourceCode, userCode, product);

                                break;

                            }
                        default:
                            //								if(ts != null 
                            //									&& (ts.TSStatus == Web.Helper.TSStatus.TSStatus_Scrap
                            //									|| ts.TSStatus == Web.Helper.TSStatus.TSStatus_Split))
                            //								{
                            //									return;
                            //								}

                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOnlineOPSingle(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                            break;
                    }
                    break;

                default:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOutline");

                        default:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOnlineOP(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                            break;
                    }
                    break;
            }
        }

        private void CheckMaterialAndTest(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            CheckMaterialAndTest(iD, actionType, resourceCode, userCode, product, null);
        }
        private void CheckMaterialAndTest(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {
            #region 处理测试和上料在同一道工序
            //Laws Lu,AM0133
            //默认会在当前站做下一个动作{hight}
            //Laws Lu,2005/09/01,测试和上料Action 在进入下一站前必须完成上料
            //Laws Lu,2005/09/27,修改	FQC判GOOD、判Pass、判退排除在外
            /*增加逻辑检查上一个工序是否按照用户的设置完整了集成上料Action和KeyParts上料Action的检查；
                     * 试流料部分有客户在业务上保证。
                     * 具体的逻辑将更新在数据采集模组的Spec中，由RD决定整合方式。
                     * 目前没有做Rework的检查
                     */
            if (
                ActionType.DataCollectAction_SMTGOOD == actionType
                || ActionType.DataCollectAction_SMTNG == actionType
                || ActionType.DataCollectAction_GOOD == actionType
                || ActionType.DataCollectAction_NG == actionType
                || ActionType.DataCollectAction_OQCGood == actionType
                || ActionType.DataCollectAction_OQCNG == actionType
                || ActionType.DataCollectAction_OQCPass == actionType
                /*|| ActionType.DataCollectAction_OQCReject == actionType*/)/* && product.NowSimulation.FromRoute != ""*/
            {
                //(new BaseSetting.BaseModelFacade()).getop
                //Laws Lu,2005/11/09,修改	改善性能

                object currentOP = null;

                if (product.CurrentItemRoute2OP != null)
                {
                    currentOP = product.CurrentItemRoute2OP;
                }
                else
                {
                    currentOP = (new ItemFacade(this._domainDataProvider)).GetItemRoute2Operation(product.NowSimulation.ItemCode, product.NowSimulation.RouteCode, product.NowSimulation.OPCode);
                    product.CurrentItemRoute2OP = currentOP as ItemRoute2OP;
                }

                if (currentOP == null)
                {
                    //Laws Lu,2006/12/28
                    /*Burn In 漏扫时，如果到了FT，提示“该产品已经完工或者被替换”这个提示要修改，以便让USER知道是在哪里完工了，并提示要从第一站投入。
                    建议如果报该提示的时候，即发现该产品已经完工，先不要直接报出来，在后面再加一条检查，检查其完工工序，并报出来。毕竟这种情况不是很多，所以性能方面的顾虑可以排除掉。
                     * */
                    if (product.LastSimulation != null && product.LastSimulation.IsComplete == "1")
                    {
                        throw new Exception("$CS_PRODUCT_ALREADY_COMPLETE $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                    }
                    else
                    {
                        throw new Exception("$CS_Route_Failed_GetNotNextOP");
                    }
                }


                string opControls = ((ItemRoute2OP)currentOP).OPControl;

                if (opControls != null && opControls.Length > 2)
                {
                    if (opControls.Substring(0, 2).Trim() == "11"
                        || (FormatHelper.StringToBoolean(opControls, (int)OperationList.Testing)
                        && FormatHelper.StringToBoolean(opControls, (int)OperationList.ComponentDown)))
                    {

                        //统计Inno的个数
                        int innoTimes = 0;

                        object[] objs = null;
                        ArrayList listTmp = new ArrayList();
                        if (actionCheckStatus != null)
                        {
                            for (int iaction = 0; iaction < actionCheckStatus.ActionList.Count; iaction++)
                            {
                                ActionEventArgs actionEventArgs = (ActionEventArgs)actionCheckStatus.ActionList[iaction];
                                if (actionEventArgs is CINNOActionEventArgs)
                                {
                                    for (int iwip = 0; iwip < actionEventArgs.OnWIP.Count; iwip++)
                                    {
                                        if (actionEventArgs.OnWIP[iwip] is LotOnWipItem)
                                        {
                                            LotOnWipItem wipItem = (LotOnWipItem)actionEventArgs.OnWIP[iwip];
                                            ONWIPItemObject wipObj = new ONWIPItemObject();
                                            wipObj.OPCODE = wipItem.OPCode;
                                            wipObj.LotCode = wipItem.LotCode;
                                            wipObj.LotSeq = wipItem.LotSeq.ToString();
                                            listTmp.Add(wipObj);
                                        }
                                    }
                                }
                            }
                        }
                        if (listTmp.Count > 0)
                        {
                            objs = new object[listTmp.Count];
                            listTmp.CopyTo(objs);
                        }
                        if (objs == null || objs.Length == 0)
                        {
                            objs = this.ExtraQuery(product.NowSimulation.LotCode
                                , product.NowSimulation.OPCode
                                , product.NowSimulation.MOCode
                                , product.LastSimulation.LastAction, product);
                        }
                        string opBOMType = actionCheckStatus == null ? String.Empty : actionCheckStatus.opBOMType;
                        if (opBOMType == string.Empty)
                        {
                            //获取OPBOM中维护的料信息
                            OPBOMFacade opFac = new OPBOMFacade(this._domainDataProvider);
                            if (FormatHelper.StringToBoolean(opControls, (int)OperationList.ComponentDown))
                            {
                                opBOMType = opFac.GetOPDropBOMDetailType(
                                    product.NowSimulation.MOCode
                                    , product.NowSimulation.RouteCode
                                    , ((ItemRoute2OP)currentOP).OPCode
                                    , out innoTimes);

                            }
                            else
                            {
                                opBOMType = opFac.GetOPBOMDetailType(
                                    product.NowSimulation.MOCode
                                    , product.NowSimulation.RouteCode
                                    , ((ItemRoute2OP)currentOP).OPCode
                                    , out innoTimes);

                            }

                            if (actionCheckStatus != null)
                            {
                                actionCheckStatus.opBOMType = opBOMType;
                                actionCheckStatus.innoTimes = innoTimes;
                            }
                        }
                        else
                        {
                            if (actionCheckStatus != null)
                            {

                                innoTimes = actionCheckStatus.innoTimes;
                            }
                        }



                        if (objs == null || (objs != null && objs.Length < 1))
                        {
                            if (BenQGuru.eMES.Web.Helper.FormatHelper.StringToBoolean(product.CurrentItemRoute2OP.OPControl, (int)OperationList.ComponentLoading))
                            {
                                throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL $CS_Param_ID =" + product.LastSimulation.LotCode);
                            }
                            if (BenQGuru.eMES.Web.Helper.FormatHelper.StringToBoolean(product.CurrentItemRoute2OP.OPControl, (int)OperationList.ComponentDown))
                            {
                                throw new Exception("$CS_PLEASE_COMPLETE_DROPMATERIAL $CS_Param_ID =" + product.LastSimulation.LotCode);
                            }
                            if (product.LastSimulation.LastAction == ActionType.DataCollectAction_DropMaterial)
                            {
                                throw new Exception("$CS_PLEASE_COMPLETE_DROPMATERIAL $CS_Param_ID =" + product.LastSimulation.LotCode);
                            }
                            else
                            {
                                throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL $CS_Param_ID =" + product.LastSimulation.LotCode);
                            }
                        }


                        string actionList = String.Empty;

                        //实际上了Inno个数
                        int actualInnos = 0;

                        string INNOString = ActionType.DataCollectAction_CollectINNO;


                        string bomINNOString = BOMItemControlType.ITEM_CONTROL_LOT;

                        if (objs != null)
                        {
                            foreach (ONWIPItemObject wip in objs)
                            {
                                actionList = actionList + INNOString + ";";
                                //Laws Lu，2006/03/10，修改	只允许一次集成上料
                                actualInnos = 1;

                            }
                        }


                        //检查集成上料的情况
                        if (opBOMType.IndexOf(bomINNOString) >= 0)
                        {
                            if (actionList.IndexOf(INNOString) < 0
                                || actualInnos < innoTimes)
                            {

                                throw new Exception("$CS_PLEASE_COMPLETE_MATERIAL $CS_Param_ID =" + product.LastSimulation.LotCode);
                            }

                            if (actionList.IndexOf(INNOString) >= 0 && actualInnos > innoTimes && actualInnos != 0)
                            {
                                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_CollectINNO)
                                {
                                    throw new Exception("$CS_ALREADY_COLLECTMATIAL $CS_Param_ID =" + product.LastSimulation.LotCode);
                                }

                            }
                        }

                    }
                }
            }
            #endregion
        }
        //OUT_Line	-> OQC  OUT_Line	-> Normal 必须走变换后指定 Route + OP
        public ItemRoute2OP CheckOnlineOPSingle(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            return CheckOnlineOPSingle(iD, actionType, resourceCode, userCode, product, null);
        }
        // Added by Icyer 2005/11/01
        // 扩展ActionCheckStatus
        public ItemRoute2OP CheckOnlineOPSingle(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {
            // Added by Icyer 2005/11/02
            if (actionCheckStatus != null)
            {
                if (actionCheckStatus.CheckedNextOP == true && actionCheckStatus.OP != null)
                {
                    bool bPass = false;
                    //当前站
                    if (product.LastSimulation.NextOPCode == actionCheckStatus.OP.OPCode &&
                        product.LastSimulation.NextRouteCode == actionCheckStatus.OP.RouteCode)
                    {
                        bPass = true;
                    }
                    if (bPass == true)
                    {
                        this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);
                        OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                        return actionCheckStatus.OP;
                    }
                }
            }
            // Added end

            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            //下一站必须采集
            if (dataModel.GetOperation2Resource(((ExtendSimulation)product.LastSimulation).NextOPCode, resourceCode) != null)
            {
                ItemRoute2OP op = this.GetMORouteOP(product.LastSimulation.ItemCode, product.LastSimulation.MOCode, ((ExtendSimulation)product.LastSimulation).NextRouteCode, ((ExtendSimulation)product.LastSimulation).NextOPCode, product);
                if (op == null)
                {
                    //Laws Lu,2006/12/28
                    /*Burn In 漏扫时，如果到了FT，提示“该产品已经完工或者被替换”这个提示要修改，以便让USER知道是在哪里完工了，并提示要从第一站投入。
                    建议如果报该提示的时候，即发现该产品已经完工，先不要直接报出来，在后面再加一条检查，检查其完工工序，并报出来。毕竟这种情况不是很多，所以性能方面的顾虑可以排除掉。
                     * */
                    if (product.LastSimulation != null && product.LastSimulation.IsComplete == "1")
                    {
                        throw new Exception("$CS_PRODUCT_ALREADY_COMPLETE $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                    }
                    else
                    {
                        throw new Exception("$CS_Route_Failed_GetNotNextOP");
                    }
                }
                this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);

                OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);

                // Added by Icyer 2005/11/02
                if (actionCheckStatus != null)
                {
                    actionCheckStatus.OP = op;
                    actionCheckStatus.CheckedNextOP = true;
                    actionCheckStatus.CheckedNextOPCode = product.LastSimulation.OPCode;
                    actionCheckStatus.CheckedNextRouteCode = product.LastSimulation.NextRouteCode;
                }
                // Added end

                return op;
            }
            else
            {
                //Laws Lu,2005/11/22,新增	Check维修回流的情况
                if (product.LastTS != null
                    && product.LastTS.TSStatus == TSStatus.TSStatus_Reflow
                    && product.LastTS.RunningCardSequence == product.LastSimulation.LotSeq)
                {
                    throw new Exception("$CS_Route_Failed $CS_Param_OPCode =" + product.LastTS.ReflowOPCode
                        + " $CS_Param_ID =" + product.LastSimulation.LotCode);
                }
                else
                {//Laws Lu,2005/12/27,注释	可能以后会需要把正确的工序代码带出来
                    throw new Exception("$CS_Route_Failed_GetNextOP_Online_Line $CS_Param_ID =" + product.LastSimulation.LotCode);
                }
            }
        }
        // Added end

        public void OtherCheck(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            OtherCheck(iD, actionType, resourceCode, userCode, product, null);
        }
        public void OtherCheck(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {
            //Laws Lu,2005/09/13,新增	上料和测试同时做的情况
            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                    //检查上料Action和测试Action是否在同一道工序
                    string route = product.LastSimulation.RouteCode;
                    string opcode = product.LastSimulation.OPCode;
                    //临时赋值
                    product.LastSimulation.RouteCode = product.LastSimulation.FromRoute;
                    product.LastSimulation.OPCode = product.LastSimulation.FromOP;

                    CheckMaterialAndTest(iD, actionType, resourceCode, userCode, product, actionCheckStatus);

                    //恢复正常值
                    product.LastSimulation.RouteCode = route;
                    product.LastSimulation.OPCode = opcode;

                    break;

                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_OQCNG:

                    //检查上料Action和测试Action是否在同一道工序
                    CheckMaterialAndTest(iD, actionType, resourceCode, userCode, product, actionCheckStatus);

                    break;


                case ActionType.DataCollectAction_Reject:

                default:
                    //检查上料Action和测试Action是否在同一道工序
                    CheckMaterialAndTest(iD, actionType, resourceCode, userCode, product, actionCheckStatus);


                    break;
            }

        }

        public ItemRoute2OP CheckOnlineOP(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            return CheckOnlineOP(iD, actionType, resourceCode, userCode, product, null);
        }
        public ItemRoute2OP CheckOnlineOP(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, Action.ActionCheckStatus actionCheckStatus)
        {
            // Added by Icyer 2005/11/02
            if (actionCheckStatus != null)
            {
                if (actionCheckStatus.CheckedNextOP == true && actionCheckStatus.OP != null)
                {
                    bool bPass = false;
                    //当前站
                    if (product.LastSimulation.NextOPCode == actionCheckStatus.OP.OPCode &&
                        product.LastSimulation.NextRouteCode == actionCheckStatus.OP.RouteCode)
                    {
                        bPass = true;
                    }
                    else if (product.LastSimulation.NextOPCode == actionCheckStatus.CheckedNextOPCode &&
                        product.LastSimulation.NextRouteCode == actionCheckStatus.CheckedNextRouteCode)
                    {
                        //下一站
                        bPass = true;
                        ((ExtendSimulation)product.LastSimulation).NextOPCode = actionCheckStatus.OP.OPCode;
                    }
                    if (bPass == true)
                    {
                        this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);
                        OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);
                        return actionCheckStatus.OP;
                    }
                }
            }
            // Added end

            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            ItemRoute2OP op;


            //下一站,当前站可以走
            if (dataModel.GetOperation2Resource(((ExtendSimulation)product.LastSimulation).NextOPCode, resourceCode) != null)
            {
                op = this.GetMORouteOP(product.LastSimulation.ItemCode, product.LastSimulation.MOCode, ((ExtendSimulation)product.LastSimulation).NextRouteCode, ((ExtendSimulation)product.LastSimulation).NextOPCode, product);
                if (op == null)
                {
                    //Laws Lu,2006/12/28
                    /*Burn In 漏扫时，如果到了FT，提示“该产品已经完工或者被替换”这个提示要修改，以便让USER知道是在哪里完工了，并提示要从第一站投入。
                    建议如果报该提示的时候，即发现该产品已经完工，先不要直接报出来，在后面再加一条检查，检查其完工工序，并报出来。毕竟这种情况不是很多，所以性能方面的顾虑可以排除掉。
                     * */
                    if (product.LastSimulation != null && product.LastSimulation.IsComplete == "1")
                    {
                        throw new Exception("$CS_PRODUCT_ALREADY_COMPLETE $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                    }
                    else
                    {
                        throw new Exception("$CS_Route_Failed_GetNotNextOP");
                    }
                }

                this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);

                //Laws Lu,2005/09/13,新增
                OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);

                // Added by Icyer 2005/11/02
                if (actionCheckStatus != null)
                {
                    actionCheckStatus.OP = op;
                    actionCheckStatus.CheckedNextOP = true;
                    actionCheckStatus.CheckedNextOPCode = product.LastSimulation.OPCode;
                    actionCheckStatus.CheckedNextRouteCode = product.LastSimulation.NextRouteCode;
                }
                // Added end

                return op;
            }
            else
            {

                op = this.GetMORouteNextOP(product.LastSimulation.MOCode, ((ExtendSimulation)product.LastSimulation).NextRouteCode, ((ExtendSimulation)product.LastSimulation).NextOPCode);
                if (op == null)
                {
                    //Laws Lu,2006/12/28
                    /*Burn In 漏扫时，如果到了FT，提示“该产品已经完工或者被替换”这个提示要修改，以便让USER知道是在哪里完工了，并提示要从第一站投入。
                    建议如果报该提示的时候，即发现该产品已经完工，先不要直接报出来，在后面再加一条检查，检查其完工工序，并报出来。毕竟这种情况不是很多，所以性能方面的顾虑可以排除掉。
                     * */
                    if (product.LastSimulation != null && product.LastSimulation.IsComplete == "1")
                    {
                        throw new Exception("$CS_PRODUCT_ALREADY_COMPLETE $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                    }
                    else
                    {
                        throw new Exception("$CS_Route_Failed_GetNotNextOP");
                    }
                }
                //Laws Lu,2005/11/05,修改	直接做归属工单，不允许到下一站采集
                if (product.LastSimulation.LastAction == ActionType.DataCollectAction_GoMO
                    && product.LastSimulation.OPCode != op.OPCode)
                {
                    throw new Exception("$CS_Route_Failed_GetNotNextOP $CS_Param_OPCode =" + product.LastSimulation.OPCode);
                }

                if (product.LastTS != null && product.LastTS.TSStatus == TSStatus.TSStatus_Reflow)
                {
                    throw new Exception("$CS_Route_Failed_GetNotNextOP $CS_Param_OPCode =" + ((ExtendSimulation)product.LastSimulation).NextOPCode);
                }

                // Edited By Hi1/Venus.Feng on 20080721 for Hisense Version
                if (actionType != ActionType.DataCollectAction_OQCNG
                    && actionType != ActionType.DataCollectAction_OQCPass
                    && actionType != ActionType.DataCollectAction_OQCReject
                    && actionType != ActionType.DataCollectAction_OQCGood)
                {
                    if (dataModel.GetOperation2Resource(op.OPCode, resourceCode) == null)
                    {
                        throw new Exception("$CS_Route_Failed $CS_Param_OPCode  =" + op.OPCode + "[" + op.EAttribute1 + "]");
                    }
                }
                // End Added

                // Added by Icyer 2005/11/02
                if (actionCheckStatus != null)
                {
                    actionCheckStatus.OP = op;
                    actionCheckStatus.CheckedNextOP = true;
                    actionCheckStatus.CheckedNextOPCode = product.LastSimulation.OPCode;
                    actionCheckStatus.CheckedNextRouteCode = product.LastSimulation.NextRouteCode;
                }
                // Added end

                //修正:
                ((ExtendSimulation)product.LastSimulation).NextOPCode = op.OPCode;
                this.WriteSimulationCheckOnlineOP(iD, actionType, resourceCode, userCode, product);

                //Laws Lu,2005/09/13,新增	
                OtherCheck(iD, actionType, resourceCode, userCode, product, actionCheckStatus);


                return op;
            }
        }

        public void WriteSimulationCheckOnlineOP(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            this.WriteLotSimulation(iD, actionType, resourceCode, userCode, product);
            product.NowSimulation.RouteCode = ((ExtendSimulation)product.LastSimulation).NextRouteCode;
            product.NowSimulation.OPCode = ((ExtendSimulation)product.LastSimulation).NextOPCode;
        }


        public void AdjustRouteOPOnline(string iD, string actionType, string resourceCode, string userCode, ProductInfo product)
        {
            //BenQGuru.eMES.TS.TSFacade tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider); 
            //Laws Lu,需要修改	曾经走过TS处理,没有考虑多次TS的情况,MOCode和Sequence也应该作为关键参数,
            //Laws Lu,2005/10/26,修改	改善性能
            BenQGuru.eMES.Domain.TS.TS ts = null;
            if (product.LastTS != null)
            {
                ts = product.LastTS;
            }
            else
            {
                object obj = (new TS.TSFacade(DataProvider)).GetCardLastTSRecord(iD, product.LastSimulation.MOCode);
                if (obj != null)
                {
                    ts = (BenQGuru.eMES.Domain.TS.TS)obj;
                }
            }


            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                    ((ExtendSimulation)product.LastSimulation).NextRouteCode = product.LastSimulation.FromRoute;
                    ((ExtendSimulation)product.LastSimulation).NextOPCode = product.LastSimulation.FromOP;
                    break;

                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_BurnOutNG:  //Add by sandy on 20140530

                    //Laws Lu,2005/08/22,取NG TS 的回流信息, 填写 	NextRouteCode, NextOPCode
                    //Laws Lu,2005/08/25,不是回流的情况处理
                    //Laws Lu,2005/08/29	送修未确认
                    #region	维修中新增、拆解、报废状态的处理

                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_New)
                    {
                        if (actionType == ActionType.DataCollectAction_OffMo)
                        {
                            ((ExtendSimulation)product.LastSimulation).NextRouteCode = ts.FromRouteCode;
                            ((ExtendSimulation)product.LastSimulation).NextOPCode = ts.FromOPCode;
                            if (product.LastSimulation.ProductStatus != ProductStatus.OffMo)
                            {
                                ((ExtendSimulation)product.LastSimulation).ProductStatus = ProductStatus.GOOD;
                            }

                            return;
                        }

                        else if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            ts.RunningCard == product.LastSimulation.LotCode)
                        {
                            return;
                        }
                    }

                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_Scrap)
                    {
                        if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            ts.RunningCard == product.LastSimulation.LotCode)
                        {
                            throw new Exception("$CS_Error_Product_Already_Scrap");
                        }


                    }
                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_Split)
                    {
                        if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            ts.RunningCardSequence == product.LastSimulation.LotSeq &&
                            ts.RunningCard == product.LastSimulation.LotCode)
                        {
                            return;
                            throw new Exception("$CS_Error_Product_Already_Split");
                        }


                    }
                    #endregion
                    //End Laws Lu

                    /*2005/12/21，Laws Lu，新增	允许脱离工单*/
                    if (actionType == ActionType.DataCollectAction_OffMo)
                    {
                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                        ((ExtendSimulation)product.LastSimulation).NextRouteCode = ts.FromRouteCode;
                        ((ExtendSimulation)product.LastSimulation).NextOPCode = ts.FromOPCode;

                    }
                    else if (ts != null && ts.TSStatus == TSStatus.TSStatus_RepeatNG)	// Added by Icyer 2007/03/15	增加不良品重复测试类型
                    {
                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                        ((ExtendSimulation)product.LastSimulation).NextRouteCode = product.LastSimulation.RouteCode;
                        ((ExtendSimulation)product.LastSimulation).NextOPCode = product.LastSimulation.OPCode;
                    }
                    else if (ts == null || ts.TSStatus != TSStatus.TSStatus_Reflow)
                    {
                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.NG;
                        //add by hiro 08/09/26
                        if (ts == null || ts.ConfirmResourceCode == string.Empty)
                            throw new Exception("$CS_REPAIR_NOT_READY:" + product.LastSimulation.LotCode);
                        else
                            throw new Exception("$CS_REPAIR_NOT_READY:" + product.LastSimulation.LotCode + "\n" + "TS" + "$ALERT_Resource:" + ts.ConfirmResourceCode);
                        //end by hiro 
                    }
                    else
                    {
                        if (ts.RunningCardSequence == product.LastSimulation.LotSeq)
                        {
                            ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            ((ExtendSimulation)product.LastSimulation).NextRouteCode = ts.ReflowRouteCode;
                            ((ExtendSimulation)product.LastSimulation).NextOPCode = ts.ReflowOPCode;
                            if (ts.ReflowOPCode.Length == 0)
                            {
                                ItemRoute2OP itemOP = this.GetMORouteFirstOP(product.LastSimulation.MOCode, ts.ReflowRouteCode);
                                if (itemOP != null)
                                {
                                    if (((ExtendSimulation)product.LastSimulation).NextOPCode == null
                                        || ((ExtendSimulation)product.LastSimulation).NextOPCode.Trim().Length <= 0)
                                    {
                                        ((ExtendSimulation)product.LastSimulation).NextOPCode = itemOP.OPCode;
                                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                                    }
                                }
                                else
                                {
                                    throw new Exception("$CS_Route_Lost_First_OP_Of_Route" + " : " + ts.ReflowRouteCode);
                                }
                            }
                        }
                        else
                        {
                            if (((ExtendSimulation)product.LastSimulation).NextRouteCode == null
                                || ((ExtendSimulation)product.LastSimulation).NextRouteCode.Trim().Length <= 0)
                            {
                                ((ExtendSimulation)product.LastSimulation).NextRouteCode = product.LastSimulation.RouteCode;
                            }
                            if (((ExtendSimulation)product.LastSimulation).NextOPCode == null
                                || ((ExtendSimulation)product.LastSimulation).NextOPCode.Trim().Length <= 0)
                            {
                                ((ExtendSimulation)product.LastSimulation).NextOPCode = product.LastSimulation.OPCode;
                            }
                        }
                    }
                    break;

                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_OQCReject:
                    break;
                default:

                    #region 处理报废、拆解的情况
                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_Scrap)
                    {
                        if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            /*ts.RunningCardSequence == product.LastSimulation.RunningCardSequence &&*/
                            ts.RunningCard == product.LastSimulation.LotCode)
                        {
                            //Laws Lu,2005/09/27,注释	报废和拆解不能够再次进入产线
                            throw new Exception("$CS_Error_Product_Already_Scrap");
                        }

                    }
                    if (ts != null && ts.FromInputType == TS.TSFacade.TSSource_OnWIP && ts.TSStatus == Web.Helper.TSStatus.TSStatus_Split)
                    {
                        if (ts.FromRouteCode == product.LastSimulation.RouteCode &&
                            ts.FromOPCode == product.LastSimulation.OPCode &&
                            ts.MOCode == product.LastSimulation.MOCode &&
                            /*ts.RunningCardSequence == product.LastSimulation.RunningCardSequence &&*/
                            ts.RunningCard == product.LastSimulation.LotCode)
                        {
                            throw new Exception("$CS_Error_Product_Already_Split");
                        }

                    }
                    #endregion

                    #region 处理回流的情况
                    if (ts != null && ts.TSStatus == TSStatus.TSStatus_Reflow)
                    {
                        if (ts.RunningCardSequence == product.LastSimulation.LotSeq)
                        {
                            ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                            ((ExtendSimulation)product.LastSimulation).NextRouteCode = ts.ReflowRouteCode;
                            ((ExtendSimulation)product.LastSimulation).NextOPCode = ts.ReflowOPCode;
                            if (ts.ReflowOPCode.Length == 0)
                            {
                                ItemRoute2OP itemOP = this.GetMORouteFirstOP(product.LastSimulation.MOCode, ts.ReflowRouteCode);
                                if (itemOP != null)
                                {
                                    if (((ExtendSimulation)product.LastSimulation).NextOPCode == null
                                        || ((ExtendSimulation)product.LastSimulation).NextOPCode.Trim().Length <= 0)
                                    {
                                        ((ExtendSimulation)product.LastSimulation).NextOPCode = itemOP.OPCode;
                                        ((ExtendSimulation)product.LastSimulation).AdjustProductStatus = ProductStatus.GOOD;
                                    }
                                }
                                else
                                {
                                    throw new Exception("$CS_Route_Lost_First_OP_Of_Route" + " : " + ts.ReflowRouteCode);
                                }
                            }
                            //无需返回
                            //return;
                        }
                    }
                    #endregion


                    if (((ExtendSimulation)product.LastSimulation).NextRouteCode == null
                        || ((ExtendSimulation)product.LastSimulation).NextRouteCode.Trim().Length <= 0)
                    {
                        ((ExtendSimulation)product.LastSimulation).NextRouteCode = product.LastSimulation.RouteCode;
                    }

                    if (((ExtendSimulation)product.LastSimulation).NextOPCode == null
                        || ((ExtendSimulation)product.LastSimulation).NextOPCode.Trim().Length <= 0)
                    {
                        ((ExtendSimulation)product.LastSimulation).NextOPCode = product.LastSimulation.OPCode;
                    }
                    break;
            }

        }


        //OutLine
        public void GetRouteOPOutline(string iD, string actionType, string resourceCode, string opCode, string userCode, ProductInfo product)
        {
            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            switch (product.LastSimulation.LastAction)
            {
                case ActionType.DataCollectAction_OutLineGood:
                case ActionType.DataCollectAction_OutLineNG:
                case ActionType.DataCollectAction_OutLineReject:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOutlineOP(iD, actionType, resourceCode, opCode, userCode, product, false);
                            break;
                        default:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOnline");
                    }
                    break;

                case ActionType.DataCollectAction_NG:
                case ActionType.DataCollectAction_SMTNG:
                case ActionType.DataCollectAction_Reject:
                case ActionType.DataCollectAction_OQCNG:
                case ActionType.DataCollectAction_OQCReject:
                case ActionType.DataCollectAction_BurnOutNG:  //Add by sandy on 20140530
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOutlineOP(iD, actionType, resourceCode, opCode, userCode, product, true);
                            break;
                        //throw new Exception("$CS_Route_Failed_Not_Support_NG(TS)2Outline");
                        default:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOnline");
                    }
                    break;

                default:
                    switch (actionType)
                    {
                        case ActionType.DataCollectAction_OutLineGood:
                        case ActionType.DataCollectAction_OutLineNG:
                        case ActionType.DataCollectAction_OutLineReject:
                            this.AdjustRouteOPOnline(iD, actionType, resourceCode, userCode, product);
                            this.CheckOutlineOP(iD, actionType, resourceCode, opCode, userCode, product, true);
                            break;
                        default:
                            throw new Exception("$CS_Route_Failed_Need_Call_CheckRouteOPOnline");
                    }
                    break;
            }
        }

        public void CheckOutlineOP(string iD, string actionType, string resourceCode, string opCode, string userCode, ProductInfo product, bool fristIn)
        {
            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            //下一站是OutLine 不需要检查Route
            if (dataModel.GetOperation2Resource(opCode, resourceCode) != null)
            {
                ((ExtendSimulation)product.LastSimulation).NextOPCode = opCode;
                this.WriteSimulationCheckOutlineOP(iD, actionType, resourceCode, userCode, product, fristIn);
            }
            else
            {
                //Laws Lu,2005/11/22,新增	Check维修回流的情况
                //if (product.LastTS != null
                //    && product.LastTS.TSStatus == TSStatus.TSStatus_Reflow
                //    && product.LastTS.RunningCardSequence == product.NowSimulation.RunningCardSequence)
                //{
                //    throw new Exception("$CS_Route_Failed_GetNextOP_OUT_Line $CS_Param_OPCode = " + product.LastTS.ReflowOPCode);
                //}
                //else
                //{
                //    throw new Exception("$CS_Route_Failed_GetNextOP_OUT_Line");
                //}

            }
        }

        public void WriteSimulationCheckOutlineOP(string iD, string actionType, string resourceCode, string userCode, ProductInfo product, bool fristIn)
        {
            this.WriteLotSimulation(iD, actionType, resourceCode, userCode, product);

            if (fristIn)
            {
                product.NowSimulation.FromRoute = product.LastSimulation.RouteCode;
                product.NowSimulation.FromOP = product.LastSimulation.OPCode;
            }
            else
            {
                product.NowSimulation.FromRoute = product.LastSimulation.FromRoute;
                product.NowSimulation.FromOP = product.LastSimulation.FromOP;
            }

            product.NowSimulation.RouteCode = string.Empty;
            //Laws Lu,2006/06/12 modify support ts 
            if (((ExtendSimulation)product.LastSimulation).NextOPCode != String.Empty)
            {
                product.NowSimulation.OPCode = ((ExtendSimulation)product.LastSimulation).NextOPCode;
            }
            else
            {
                product.NowSimulation.OPCode = product.LastSimulation.OPCode;//((ExtendSimulation)product.LastSimulation).NextOPCode;
            }
        }


        #endregion 检查
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="outPutQty">只需要填写增加数</param>
        public void UpdateMOOutPut(string moCode, int outPutQty)
        {
            string updateSql = string.Format("update TBLMO set MOACTQTY=MOACTQTY+{0} where MOCODE=$moCode1", outPutQty.ToString());
            this.DataProvider.CustomExecute(new SQLParamCondition(updateSql, new SQLParameter[] { new SQLParameter("moCode1", typeof(string), moCode.ToUpper()) }));
        }

        #region 工单途程信息
        //获取工单途程第一个工序
        public ItemRoute2OP GetMORouteFirstOP(string moCode, string routeCode)
        {
            MOModel.ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            ItemRoute2OP op = itemFacade.GetMORouteFirstOperation(moCode, routeCode);
            return op;
        }

        //获取工单途程下一个工序
        public ItemRoute2OP GetMORouteNextOP(string moCode, string routeCode, string curOp)
        {
            MOModel.ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            ItemRoute2OP op = itemFacade.GetMORouteNextOperation(moCode, routeCode, curOp);
            return op;
        }

        //工序是否为途程最后一个工序
        public bool OPIsMORouteLastOP(string moCode, string routeCode, string opCode)
        {
            MOModel.ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            return itemFacade.OperationIsRouteLastOperation(moCode, routeCode, opCode);
        }

        //工单途程工序信息
        public ItemRoute2OP GetMORouteOP(string itemCode, string moCode, string routeCode, string curOp, ProductInfo e)
        {
            MOModel.ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            if (e.CurrentItemRoute2OP == null)
            {
                e.CurrentItemRoute2OP = (ItemRoute2OP)itemFacade.GetItemRoute2Operation(itemCode, routeCode, curOp);

            }//Laws Lu,2005/12/27，新增	考虑到途程和工序变换的case
            else if (e.CurrentItemRoute2OP != null
                && (e.CurrentItemRoute2OP.OPCode != curOp
                || e.CurrentItemRoute2OP.RouteCode != routeCode))
            {
                e.CurrentItemRoute2OP = (ItemRoute2OP)itemFacade.GetItemRoute2Operation(itemCode, routeCode, curOp);
            }
            return e.CurrentItemRoute2OP;

        }
        #endregion 工单途程信息

        #endregion

        #region LotSimulation
        /// <summary>
        /// 
        /// </summary>
        public LotSimulation CreateNewLotSimulation()
        {
            return new LotSimulation();
        }

        public void AddLotSimulation(LotSimulation simulation)
        {
            this.DataProvider.Insert(simulation);
        }

        public void UpdateLotSimulation(LotSimulation simulation)
        {
            this.DataProvider.Update(simulation);
        }

        public void DeleteLotSimulation(LotSimulation simulation)
        {
            this.DataProvider.Delete(simulation);
        }

        //		public void DeleteSimulation(Simulation[] simulation)
        //		{
        //			//this.DataProvider.Delete ( simulation );
        //		}

        // Added By Hi1/Venus.Feng on 20080718 For Hisense Version
        public void UpdateLotSimulationForLot(string LotCode, string moCode, string lotNo)
        {
            string sql = "UPDATE tblLotsimulation SET lotno='" + lotNo + "' WHERE LotCode='" + LotCode + "' AND mocode='" + moCode + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        // End Added

        /// <summary>
        /// ** 修 改:		Jane Shu	2005-07-21
        ///					改传参SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object GetLotSimulation(string LotCode)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(LotSimulation),
                new SQLParamCondition(string.Format(
                @"select {0} from tblLotsimulation where
					(LotCode,mocode) in (
					select LotCode, mocode
					from (select LotCode, mocode
							from tblLotsimulation
							where LotCode = $LOTCODE
							order by BeginDATE desc, BeginTIME desc)
							where rownum = 1)",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulation))),
                new SQLParameter[] { new SQLParameter("LOTCODE", typeof(string), LotCode.ToUpper()) }));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations[0];
            else
                return null;

        }

        public object[] QueryLotSimulation(string LotCode)
        {
            string sql = "SELECT {0} FROM tblLotsimulation WHERE LotCode = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulation)), LotCode.ToUpper());

            return this.DataProvider.CustomQuery(typeof(LotSimulation), new SQLCondition(sql));
        }

        public object[] QueryLotSimulationReport(string LotCode)
        {
            string sql = "SELECT {0} FROM tbllotsimulationreport WHERE LotCode = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulationReport)), LotCode.ToUpper());

            return this.DataProvider.CustomQuery(typeof(LotSimulationReport), new SQLCondition(sql));
        }

        /// <summary>
        /// ** 修 改:		Jane Shu	2012-03-22
        ///					改传参SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object[] GetOnlineLotSimulation(string moCode, string rescode)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(LotSimulation),
                new SQLCondition(string.Format(
                @"select {0} from tbllotsimulation where
					mocode = '{1}' and rescode = '{2}' order by beginDate desc, beginTime desc  ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulation)),
                moCode.ToUpper(), rescode)));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations;
            else
                return null;

        }

        /// <summary>
        /// ** 修 改:		Jane Shu	2005-07-21
        ///					改传参SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object[] GetOnlineLotSimulationByMoCode(string moCode)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(LotSimulation),
                new SQLCondition(string.Format(
                @"select {0} from tbllotsimulation where
					mocode = '{1}' and iscom = 0",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulation)),
                moCode.ToUpper())));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations;
            else
                return null;

        }

        /// <summary>
        /// ** 修 改:		Jane Shu	2005-07-21
        ///					改传参SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object[] GetLotSimulationFromCarton(string cartonno)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(LotSimulation),
                //new SQLParamCondition(string.Format("select {0} from TBLSIMULATION where RCARD = $RCARD order by MDATE desc,MTIME desc",
                new SQLCondition(string.Format(
                @"select {0} from tblLotsimulation where
					cartoncode = '{1}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulation)),
                cartonno.ToUpper())));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations;
            else
                return null;

        }

        // Added by HI1/Venus.Feng on 20081118 for Hisense Version
        public object GetLotCodeByCartonCode(string cartonCode)
        {
            string strSql = "";
            strSql += "SELECT {0}";
            strSql += "  FROM (SELECT   {0}";
            strSql += "            FROM tblLotsimulationreport";
            strSql += "           WHERE cartoncode = '" + cartonCode + "'";
            strSql += "        ORDER BY begindate DESC, begintime DESC)";
            strSql += " WHERE ROWNUM = 1";

            object[] list = this.DataProvider.CustomQuery(typeof(LotSimulationReport),
                new SQLCondition(string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulationReport)))));

            if (list == null || list.Length == 0)
            {
                return null;
            }
            else
            {
                return list[0];
            }
        }
        // End Added

        /// <summary>
        /// ** 修 改:		Laws Lu	2005-08-19
        ///					改传参SQL
        /// </summary>
        /// <param name="runningCard"></param>
        /// <returns></returns>
        public object GetLotSimulation(string moCode, string LotCode)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(LotSimulation),
                new SQLParamCondition(string.Format("select {0} from TBLLotSIMULATION where LOTCODE = $LOTCODE and MOCODE = $MOCODE order by BeginDATE desc,BeginTIME desc",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulation))),
                new SQLParameter[] { new SQLParameter("LOTCODE", typeof(string), LotCode.ToUpper()), new SQLParameter("MOCODE", typeof(string), moCode.ToUpper()) }));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations[0];
            else
                return null;

        }

        public Object GetLastLotSimulation(string LotCode)
        {
            string strSql = "SELECT * FROM tblLotSimulation WHERE LotCode='" + LotCode + "' and mocode in(select mocode from tblmo where mostatus='mostatus_open')";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(LotSimulation), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return objs[0];
        }
        //end 


        //add by hiro 2008/11/08 
        public Object GetLastLotSimulationOrderByDateAndTime(string LotCode)
        {
            string strSql = "SELECT * FROM tblLotSimulation WHERE LotCode='" + LotCode + "' and mocode in(select mocode from tblmo where mostatus='mostatus_open')";
            strSql = strSql + " and productstatus in ('GOOD','OFFLINE','OFFMO','OUTLINE') order by Begindate desc, Begintime desc";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(LotSimulation), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return objs[0];
        }

        public void IsComp(LotSimulation lotSimulation)
        {
            //Laws Lu,2005/08/15,新增	完工逻辑，在其他Check都通过的情况下，所有的RunningCard应该是GOOD状态
            //暂时不考虑线外工序

            if (lotSimulation.RouteCode != "" && this.OPIsMORouteLastOP(
                lotSimulation.MOCode
                , lotSimulation.RouteCode
                , lotSimulation.OPCode))
            {
                lotSimulation.IsComplete = "1";
                lotSimulation.EAttribute1 = "GOOD";
            }
        }

        //add end 
        #endregion

        #region LOTONWIP
        /// <summary>
        /// TBLLOTONWIP
        /// </summary>
        public LotOnWip CreateNewLotOnWip()
        {
            return new LotOnWip();
        }

        public void AddLotOnWip(LotOnWip lotonwip)
        {
            this.DataProvider.Insert(lotonwip);
        }

        public void DeleteLotOnWip(LotOnWip lotonwip)
        {
            this.DataProvider.Delete(lotonwip);
        }

        public void UpdateLotOnWip(LotOnWip lotonwip)
        {
            this.DataProvider.Update(lotonwip);
        }

        public object GetLotOnWip(string MOCODE, string LOTCODE, int LOTSEQ)
        {
            return this.DataProvider.CustomSearch(typeof(LotOnWip), new object[] { MOCODE, LOTCODE, LOTSEQ });
        }

        public int GetLotInfoCount(string lotCode, string moCode, string ssCode, string opCode)
        {
            string sql = "SELECT COUNT(*) FROM tblLotonwip WHERE 1=1 ";
            if (lotCode.Trim() != string.Empty)
            {
                sql += " AND lotCode='" + lotCode.Trim().ToUpper() + "'";
            }
            if (moCode.Trim() != string.Empty)
            {
                sql += " AND mocode='" + moCode.Trim().ToUpper() + "'";
            }
            if (ssCode.Trim() != string.Empty)
            {
                sql += " AND SSCode='" + ssCode.Trim().ToUpper() + "'";
            }
            if (opCode.Trim() != string.Empty)
            {
                sql += " AND opCode='" + opCode.Trim().ToUpper() + "'";
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryLotOnWip(string moCode, string lotCode, string opCode)
        {
            string sql = "SELECT * FROM tblLotonwip WHERE 1=1 ";
            if (lotCode.Trim() != string.Empty)
            {
                sql += " AND lotCode='" + lotCode.Trim().ToUpper() + "'";
            }
            if (moCode.Trim() != string.Empty)
            {
                sql += " AND mocode='" + moCode.Trim().ToUpper() + "'";
            }
            if (opCode.Trim() != string.Empty)
            {
                sql += " AND opCode='" + opCode.Trim().ToUpper() + "'";
            }
            sql += " AND collectStatus='COLLECT_BEGIN' ";
            return this.DataProvider.CustomQuery(typeof(LotOnWip), new SQLCondition(sql));
        }

        //add By Jarvis For DeductQty 20120315
        public object[] QueryLotOnWIP(string lotCode, string moCode, string opCode, string action)
        {
            string sql = "";
            sql += " SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotOnWip)) + " ";
            sql += " FROM tbllotonwip ";
            sql += " WHERE 1 = 1 ";

            sql += " AND lotcode = '" + lotCode.Trim().ToUpper() + "' ";
            sql += " AND action = '" + action.Trim().ToUpper() + "' ";
            sql += " AND moCode ='" + moCode + "' ";
            sql += " AND opCode ='" + opCode + "' ";

            return this.DataProvider.CustomQuery(typeof(LotOnWipItem), new SQLCondition(sql));
        }

        #endregion

        #region LotSimulationReport
        /// <summary>
        /// 
        /// </summary>
        public LotSimulationReport CreateNewLotSimulationReport()
        {
            return new LotSimulationReport();
        }

        public void AddLotSimulationReport(LotSimulationReport simulationReport)
        {
            this.DataProvider.Insert(simulationReport);
        }

        public void UpdateLotSimulationReport(LotSimulationReport simulationReport)
        {
            this.DataProvider.Update(simulationReport);
        }

        // Added By Hi1/Venus.Feng on 20080718 For Hisense Version
        public void UpdateLotSimulationReportForLot(string lotCode, string moCode, string lotNo)
        {
            string sql = "UPDATE tbllotsimulationReport SET lotno='" + lotNo + "' WHERE lotCode='" + lotCode + "' AND mocode='" + moCode + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        // End Added

        //Laws Lu,2006/06/01 更新Carton号
        public void UpdateLotSimulationReportCartonNo(string lotCode, string mocode, string ctnno)
        {
            this.DataProvider.CustomExecute(
                new SQLCondition("update tbllotsimulationreport set cartoncode='" + ctnno + "'"
                + " where lotCode='" + lotCode + "' and mocode='" + mocode + "'"));
        }

        public void DeleteLotSimulationReport(LotSimulation simulation)
        {
            this.DataProvider.CustomExecute(new SQLParamCondition("delete from  TBLLotSIMULATIONREPORT where LOTCODE=$LOTCODE and MOCODE=$MOCODE",
                new SQLParameter[] {
									   new SQLParameter("LOTCODE", typeof(string), simulation.LotCode),
									   new SQLParameter("MOCODE", typeof(string), simulation.MOCode)
								   }));
        }

        public void DeleteLotSimulationReport(LotSimulationReport simulationReport)
        {
            this.DataProvider.Delete(simulationReport);
        }

        /// <summary>
        /// 获取最后一个SimulationReport
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public LotSimulationReport GetLastLotSimulationReport(string LOTCODE)
        {
            string strSql = "SELECT * FROM tblLotSimulationReport WHERE LOTCODE='" + LOTCODE + "' ";
            strSql += "ORDER BY BeginDate DESC,BeginTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(LotSimulationReport), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return (LotSimulationReport)objs[0];
        }

        public LotSimulationReport GetLastSimulationReportByRMA(string LotCode, string rmaBillCode)
        {
            string strSql = "SELECT * FROM tblLotSimulationReport WHERE LotCode='" + LotCode + "' and RMABILLCODE='" + rmaBillCode + "'";
            strSql += "ORDER BY BeginDate DESC,BeginTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(LotSimulationReport), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return (LotSimulationReport)objs[0];
        }

        public LotSimulationReport GetLastLotSimulationReport(string lotCode, bool isComp)
        {
            string strSql = "SELECT * FROM tblLotSimulationReport WHERE lotCode='" + lotCode + "' and ISCOM='" + isComp + "'";
            strSql += "ORDER BY BeginDate DESC,BeginTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(LotSimulationReport), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return (LotSimulationReport)objs[0];
        }

        public LotSimulationReport GetLastLotSimulationReportByCarton(string cartonCode)
        {
            string strSql = "SELECT * FROM tblLotSimulationReport WHERE cartoncode='" + cartonCode + "' ";
            strSql += "ORDER BY BeginDate DESC,BeginTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(LotSimulationReport), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            return (LotSimulationReport)objs[0];
        }

        public object[] QueryLotSimulationReportByCarton(string cartonno)
        {
            object[] simulationReports = this.DataProvider.CustomQuery(typeof(LotSimulationReport),
                new SQLCondition(string.Format(
                @"select {0} from tblLotSimulationReport where
					cartoncode = '{1}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulationReport)),
                cartonno.ToUpper())));

            if (simulationReports == null)
                return null;
            if (simulationReports.Length > 0)
                return simulationReports;
            else
                return null;
        }

        public object GetLotSimulationReport(string moCode, string lotCode)
        {
            object[] simulationReports = this.DataProvider.CustomQuery(typeof(LotSimulationReport),
                new SQLParamCondition(string.Format("select {0} from tblLotSimulationReport where LOTCODE = $LOTCODE and MOCODE = $MOCODE order by BeginDATE desc,BeginTIME desc",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotSimulationReport))),
                new SQLParameter[] { new SQLParameter("LOTCODE", typeof(string), lotCode.ToUpper()), new SQLParameter("MOCODE", typeof(string), moCode.ToUpper()) }));

            if (simulationReports == null)
                return null;
            if (simulationReports.Length > 0)
                return simulationReports[0];
            else
                return null;

        }
        #endregion

        #region LotOnWIPItem
        /// <summary>
        /// 
        /// </summary>
        public LotOnWipItem CreateNewLotOnWipItem()
        {
            return new LotOnWipItem();
        }

        public void AddLotOnWIPItem(LotOnWipItem onWIPItem)
        {
            this.DataProvider.Insert(onWIPItem);
        }

        public void UpdateLotOnWIPItem(LotOnWipItem onWIPItem)
        {
            this.DataProvider.Update(onWIPItem);
        }

        public void DeleteLotOnWIPItem(LotOnWipItem onWIPItem)
        {
            this.DataProvider.Delete(onWIPItem);
        }

        public object GetLotOnWIPItem(string lotCode, decimal lotSeq, string mOCode)
        {
            return this.DataProvider.CustomSearch(typeof(LotOnWipItem), new object[] { lotCode, lotSeq, mOCode });
        }

        public object[] GetLastLotOnWIPItem(string lotCode, string moCode)
        {
            string selectSQL = " select {0} from (select * from tblLotonwipitem where lotCode in ({1}) and mocode = '{2}' order by mdate desc,mseq desc) where rownum = 1";

            return this.DataProvider.CustomQuery(typeof(LotOnWipItem), new SQLCondition(String.Format(selectSQL, new object[] { DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotOnWipItem)), lotCode, moCode })));
        }


        public object[] ExtraQuery(string lotCode, string opCODE, string moCode, string actionType, ProductInfo product)
        {
            string selectSQL = String.Empty;
            //Laws Lu,2005/12/17 查询上料记录和下料记录
            if (actionType == ActionType.DataCollectAction_DropMaterial)
            {
                string lotCodes = lotCode;
                if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                {
                    ArrayList arlot = new ArrayList();
                    BenQGuru.eMES.Material.CastDownHelper castHelper = new BenQGuru.eMES.Material.CastDownHelper(DataProvider);

                    //castHelper.GetAllRCard(ref arRcard, lotCode);
                    arlot.Add(lotCode);

                    lotCodes = "('" + String.Join("','", (string[])arlot.ToArray(typeof(string))) + "')";

                    selectSQL = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ONWIPItemObject))
                        + " from tblLotonwipitem where Lotcode in {0} and dropOP in ('{1}') and actiontype="
                        + (int)MaterialType.DropMaterial;

                }
                else
                {
                    selectSQL = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ONWIPItemObject))
                        + " from tblLotonwipitem where Lotcode = '{0}' and dropOP in ('{1}') and actiontype="
                        + (int)MaterialType.DropMaterial;


                }
                return this.DataProvider.CustomQuery(typeof(ONWIPItemObject)
                    , new SQLCondition(String.Format(selectSQL, new object[] { lotCode, String.Join("','", new string[] { opCODE, "TS" }) })));

            }
            else
            {
                selectSQL = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ONWIPItemObject))
                    + " from tblLotonwipitem where  Lotcode = '{0}' and opcode = '{1}' and mocode = '{2}' and actiontype="
                    + (int)MaterialType.CollectMaterial;

                return this.DataProvider.CustomQuery(typeof(ONWIPItemObject)
                    , new SQLCondition(String.Format(selectSQL, new object[] { lotCode, opCODE, moCode })));
            }


        }

        public object[] QueryLotOnWIPItem(string lotCode, string moCode, string opcode)
        {
            string selectSQL = " select {0} from tblLotonwipitem where 1=1 and collectstatus = 'COLLECT_BEGIN'  and lotCode = '{1}' and mocode ='{2}' and opcode ='{3}'  ";

            return this.DataProvider.CustomQuery(typeof(LotOnWipItem), new SQLCondition(String.Format(selectSQL, new object[] { DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotOnWipItem)), lotCode, moCode, opcode })));
        }

        public object[] QueryLotOnWIPItem(string lotCode)
        {
            string selectSQL = " select {0} from tblLotonwipitem where 1=1 and lotCode = '{1}'";

            return this.DataProvider.CustomQuery(typeof(LotOnWipItem), new SQLCondition(String.Format(selectSQL, new object[] { DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotOnWipItem)), lotCode })));
        }

        public object[] QueryLotOnWIPItem(string lotNo, string MItemCode)
        {
            string selectSQL = " select {0} from tblLotonwipitem where 1=1 and lotNo = '{1}' and mitemcode='{2}'";

            return this.DataProvider.CustomQuery(typeof(LotOnWipItem), new SQLCondition(String.Format(selectSQL, new object[] { DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotOnWipItem)), lotNo, MItemCode })));
        }

        public object[] QueryLotOnWIPItemWithmoCode(string lotNo, string MItemCode, string moCode)
        {
            string selectSQL = " select {0} from tblLotonwipitem where 1=1 and lotNo = '{1}' and mitemcode='{2}' and mocode='{3}'";

            return this.DataProvider.CustomQuery(typeof(LotOnWipItem), new SQLCondition(String.Format(selectSQL, new object[] { DomainObjectUtility.GetDomainObjectFieldsString(typeof(LotOnWipItem)), lotNo, MItemCode, moCode })));
        }


        #endregion

        #region OnWIPLotTransfer
        /// <summary>
        /// 
        /// </summary>
        public OnWipLotTrans CreateNewOnWIPLotTransfer()
        {
            return new OnWipLotTrans();
        }

        public void AddOnWIPLotTransfer(OnWipLotTrans onWIPLotTransfer)
        {
            this._helper.AddDomainObject(onWIPLotTransfer);
        }

        public void UpdateOnWIPLotTransfer(OnWipLotTrans onWIPLotTransfer)
        {
            this._helper.UpdateDomainObject(onWIPLotTransfer);
        }

        public void DeleteOnWIPLotTransfer(OnWipLotTrans onWIPLotTransfer)
        {
            this._helper.DeleteDomainObject(onWIPLotTransfer);
        }

        public object GetOnWIPLotTransfer(decimal serial)
        {
            return this.DataProvider.CustomSearch(typeof(OnWipLotTrans), new object[] { serial });
        }
        #endregion

        #region 解析方式与检查类型相关方法

        public Messages ParseFromBarcode(ref MINNO newMINNO, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength)
        {
            return ParseFromBarcode(ref newMINNO, barcode, opBOMDetailForItemCode, opBOMDetailSNLength, true);
        }
        public Messages ParseFromBarcode(ref MINNO newMINNO, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength, bool dealSecondSource)
        {
            Messages returnValue = new Messages();

            string splitter = "-";

            string itemCode = string.Empty;
            string vendorCode = string.Empty;
            string lotNo = string.Empty;
            string productDate = string.Empty;

            string pattern = @"^.+-.+-.+-.+[Rr]?$";
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = reg.Match(barcode);
            if (!match.Success)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_barcode: $CS_Error_Format" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            lotNo = barcode;

            int pos = 0;

            //Vendor Code
            pos = barcode.IndexOf(splitter);
            vendorCode = barcode.Substring(0, pos);
            barcode = barcode.Substring(pos + 1);

            //Item code
            pos = barcode.IndexOf(splitter);
            itemCode = barcode.Substring(0, pos);
            barcode = barcode.Substring(pos + 1);

            //Product date
            pos = barcode.IndexOf(splitter);
            productDate = barcode.Substring(0, pos);
            barcode = barcode.Substring(pos + 1);

            //Return
            if (itemCode == string.Empty
                || vendorCode == string.Empty
                || lotNo == string.Empty
                || productDate == string.Empty)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_barcode: $CS_Error_LackOfInfo" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            //比对料号和序列号长度
            returnValue.AddMessages(this.CheckAfterParse(itemCode, opBOMDetailForItemCode, lotNo, opBOMDetailSNLength, "$parse_barcode:", dealSecondSource));

            if (!returnValue.IsSuccess())
            {
                return returnValue;
            }

            if (returnValue.IsSuccess())
            {
                newMINNO.MItemCode = itemCode;
                newMINNO.VendorCode = vendorCode;
                newMINNO.LotNO = lotNo;
                newMINNO.DateCode = productDate;

                returnValue.ClearMessages();
                returnValue.Add(new UserControl.Message(MessageType.Success, "$parse_barcode$CS_Error_ParseSuccess"));
            }

            return returnValue;
        }

        public Messages ParseFromPrepare(ref MINNO newMINNO, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength)
        {
            return ParseFromPrepare(ref newMINNO, barcode, opBOMDetailForItemCode, opBOMDetailSNLength, true);
        }

        public Messages ParseFromPrepare(ref MINNO newMINNO, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength, bool dealSecondSource)
        {
            Messages returnValue = new Messages();

            string sql = "select " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MKeyPart)) + " ";
            sql += "from tblmkeypart, tblmkeypartdetail ";
            sql += "where tblmkeypart.mitemcode = tblmkeypartdetail.mitemcode ";
            sql += "and tblmkeypart.seq = tblmkeypartdetail.seq ";
            sql += "and serialno = '" + barcode.Trim().ToUpper() + "' ";

            //支持批次料从tblmkeypart获取信息
            if (newMINNO.EAttribute1 == BOMItemControlType.ITEM_CONTROL_LOT)
            {
                sql = "select " + DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(MKeyPart)) + " ";
                sql += "from tblmkeypart where LOTNO = '" + barcode.Trim().ToUpper() + "' ";
            }
            //End

            object[] mKeyParts = this.DataProvider.CustomQuery(typeof(MKeyPart), new SQLCondition(sql));

            if (mKeyParts == null)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_prepare: $CS_Error_NoPrepareInfo" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            for (int i = 0; i < mKeyParts.Length; i++)
            {
                returnValue = new Messages();
                string prepareMOCode = ((MKeyPart)mKeyParts[i]).MoCode == null ? "" : ((MKeyPart)mKeyParts[i]).MoCode.Trim().ToUpper();
                string keyPartMOCode = newMINNO.MOCode == null ? "" : newMINNO.MOCode.Trim().ToUpper();

                if (prepareMOCode.Length > 0 && prepareMOCode != keyPartMOCode)
                {
                    if (i == mKeyParts.Length - 1)
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_prepare: $CS_Error_MOLimited " + ((MKeyPart)mKeyParts[i]).MoCode.Trim().ToUpper() + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                        return returnValue;
                    }
                    else
                    {
                        continue;
                    }
                }

                //比对料号和序列号长度
                returnValue.AddMessages(this.CheckAfterParse(((MKeyPart)mKeyParts[i]).MItemCode, opBOMDetailForItemCode, barcode, opBOMDetailSNLength, "$parse_prepare:", dealSecondSource));
                if (!returnValue.IsSuccess())
                {
                    if (i == mKeyParts.Length - 1)
                    {
                        return returnValue;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (returnValue.IsSuccess())
                {
                    newMINNO.MItemCode = ((MKeyPart)mKeyParts[i]).MItemCode;
                    newMINNO.VendorCode = ((MKeyPart)mKeyParts[i]).VendorCode;
                    newMINNO.LotNO = ((MKeyPart)mKeyParts[i]).LotNO;
                    newMINNO.DateCode = ((MKeyPart)mKeyParts[i]).DateCode;
                    newMINNO.VendorItemCode = ((MKeyPart)mKeyParts[i]).VendorItemCode;
                    newMINNO.Version = ((MKeyPart)mKeyParts[i]).Version;
                    newMINNO.PCBA = ((MKeyPart)mKeyParts[i]).PCBA;
                    newMINNO.BIOS = ((MKeyPart)mKeyParts[i]).BIOS;

                    returnValue.ClearMessages();
                    returnValue.Add(new UserControl.Message(MessageType.Success, "$parse_prepare$CS_Error_ParseSuccess"));
                    break;
                }
            }
            return returnValue;
        }

        public Messages ParseFromProduct(ref MINNO newMINNO, bool checkComplete, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength)
        {
            return ParseFromProduct(ref newMINNO, checkComplete, barcode, opBOMDetailForItemCode, opBOMDetailSNLength, true, false);
        }

        public Messages ParseFromProduct(ref MINNO newMINNO, bool checkComplete, string barcode, OPBOMDetail opBOMDetailForItemCode, int opBOMDetailSNLength, bool dealSecondSource, bool isSKDCartonCheck)
        {
            Messages returnValue = new Messages();

            LotSimulationReport simRpt = GetLastLotSimulationReport(barcode);

            if (simRpt == null)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_product: $CS_Error_NoSimulationReport" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }


            if (checkComplete && simRpt.IsComplete == FormatHelper.FALSE_STRING)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_product: $CS_Error_NotComplete" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            if (simRpt.Status != ProductStatus.GOOD
                && simRpt.Status != ProductStatus.OffLine
                && simRpt.Status != ProductStatus.OffMo
                && simRpt.Status != ProductStatus.OutLine)
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$parse_product: $CS_ProductStatusError" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            //比对料号和序列号长度
            returnValue.AddMessages(this.CheckAfterParse(simRpt.ItemCode, opBOMDetailForItemCode, barcode, opBOMDetailSNLength, "$parse_product:", dealSecondSource));
            if (!returnValue.IsSuccess())
            {
                return returnValue;
            }

            if (returnValue.IsSuccess())
            {
                newMINNO.MItemCode = simRpt.ItemCode;
                newMINNO.LotNO = simRpt.LotNo;

                returnValue.ClearMessages();
                returnValue.Add(new UserControl.Message(MessageType.Success, "$parse_product$CS_Error_ParseSuccess"));
            }

            return returnValue;
        }

        //add by hiro 08/10/13
        private Messages CheckAfterParse(string inputItemCode, OPBOMDetail opBOMDetail, string input, int opBOMDetailSNLength, string msgPrefix, bool dealSecondSource)
        {
            Messages returnValue = new Messages();

            //料号比对
            if (opBOMDetail != null)
            {
                if (dealSecondSource)
                {
                    string itemCode = opBOMDetail.ItemCode;
                    string opID = opBOMDetail.OPID;
                    string opBOMCode = opBOMDetail.OPBOMCode;
                    string opBOMVersion = opBOMDetail.OPBOMVersion;
                    string opBOMSourceItemcode = opBOMDetail.OPBOMSourceItemCode;
                    int orgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

                    OPBOMFacade opbomfacade = new OPBOMFacade(this.DataProvider);
                    object[] objs = opbomfacade.QueryOPBOMDetail(opID, itemCode, opBOMCode, opBOMVersion, orgID, inputItemCode);
                    if (objs != null)
                    {
                        if (((OPBOMDetail)objs[0]).OPBOMSourceItemCode.Trim().ToUpper() != opBOMSourceItemcode.Trim().ToUpper())
                        {
                            returnValue.Add(new UserControl.Message(MessageType.Error, msgPrefix + " $CS_LotControlMaterial_CompareItemFailed"));
                            return returnValue;
                        }
                    }
                    else
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, msgPrefix + " $CS_LotControlMaterial_CompareItemFailed"));
                        return returnValue;
                    }
                }
                else
                {
                    if (string.Compare(inputItemCode, opBOMDetail.OPBOMItemCode, true) != 0)
                    {
                        returnValue.Add(new UserControl.Message(MessageType.Error, msgPrefix + "$CS_LotControlMaterial_CompareItemFailed"));
                        return returnValue;
                    }
                }
            }

            //检查序列号长度
            if (opBOMDetailSNLength > 0)
            {
                if (input.Length != opBOMDetailSNLength)
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, msgPrefix + " $Error_SNLength_Wrong"));
                    return returnValue;
                }
            }

            return returnValue;
        }
        //end by hiro 08/10/13

        //必须检查供应商
        public Messages CheckNeedVebdor(MINNO newMINNO)
        {
            Messages returnValue = new Messages();

            if (string.IsNullOrEmpty(newMINNO.VendorCode))
            {
                returnValue.Add(new UserControl.Message(MessageType.Error, "$CS_Material_Have_No_Vendor" + "[$CS_MItemCode:" + newMINNO.MItemCode.Trim() + "]"));
                return returnValue;
            }

            return returnValue;
        }

        public Messages GetMINNOByBarcode(OPBOMDetail opBOMDetail, string barcode, string moCode, ArrayList inputPartList, bool dealSecondSource, bool isSKDCartonCheck, out MINNO minno)
        {
            Messages returnValue = new Messages();
            minno = null;


            //解析方式
            string parseTypeSetting = "," + opBOMDetail.OPBOMParseType + ",";
            bool checkStatus = opBOMDetail.CheckStatus == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING;

            //检查类型
            string checkTypeSetting = "," + opBOMDetail.OPBOMCheckType + ",";
            int inputLength = opBOMDetail.SerialNoLength;
            string checkNeedVendor = !string.IsNullOrEmpty(opBOMDetail.NeedVendor) ? checkNeedVendor = opBOMDetail.NeedVendor : string.Empty;

            string mItemCode = opBOMDetail.OPBOMItemCode;

            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            MINNO newMINNO = materialFacade.CreateNewMINNO();
            newMINNO.MItemCode = mItemCode;

            Messages parseSuccess = new Messages();
            Messages oldParseSuccess = new Messages();
            parseSuccess.Add(new UserControl.Message(MessageType.Error, "$CS_Error_ParseFailed"));

            //勾选了料号比对,必须选择解析方式
            if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
            {
                if (parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_BARCODE.ToLower() + ",") < 0
                  && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PREPARE.ToLower() + ",") < 0
                  && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PRODUCT.ToLower() + ",") < 0)
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, ">>$CS_Error_ParseFailed:$CheckCompareItem_Must_CheckOneParse:" + mItemCode + ""));
                    return returnValue;
                }
            }

            if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_BARCODE.ToLower() + ",") >= 0)
            {
                //Parse from barcode
                OPBOMDetail opBOMDetailForItemCode = null;
                if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                {
                    opBOMDetailForItemCode = opBOMDetail;
                }
                oldParseSuccess.AddMessages(parseSuccess);
                parseSuccess = ParseFromBarcode(ref newMINNO, barcode, opBOMDetailForItemCode, inputLength, dealSecondSource);
            }

            if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PREPARE.ToLower() + ",") >= 0)
            {
                //Parse from prepare
                newMINNO.MOCode = moCode;
                newMINNO.EAttribute1 = opBOMDetail.OPBOMItemControlType;

                OPBOMDetail opBOMDetailForItemCode = null;
                if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                {
                    opBOMDetailForItemCode = opBOMDetail;
                }
                oldParseSuccess.AddMessages(parseSuccess);
                parseSuccess = ParseFromPrepare(ref newMINNO, barcode, opBOMDetailForItemCode, inputLength, dealSecondSource);
            }

            if (!parseSuccess.IsSuccess() && parseTypeSetting.IndexOf("," + OPBOMDetailParseType.PARSE_PRODUCT.ToLower() + ",") >= 0)
            {
                //Parse from product
                OPBOMDetail opBOMDetailForItemCode = null;
                if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_COMPAREITEM.ToLower() + ",") >= 0)
                {
                    opBOMDetailForItemCode = opBOMDetail;
                }
                oldParseSuccess.AddMessages(parseSuccess);
                parseSuccess = ParseFromProduct(ref newMINNO, checkStatus, barcode, opBOMDetailForItemCode, inputLength, dealSecondSource, isSKDCartonCheck);
            }

            //如果VendorCode为空，到tblmaterial中获取
            if (newMINNO.VendorCode == null || newMINNO.VendorCode.Trim().Length <= 0)
            {
                ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                object objMaterial = itemfacade.GetMaterial(newMINNO.MItemCode.Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (objMaterial != null)
                {
                    newMINNO.VendorCode = ((Domain.MOModel.Material)objMaterial).VendorCode;
                }
            }

            //oldParseSuccess中记录的是所有解析结果，若果未经过任何解析，oldParseSuccess是空的，也表示成功
            if (!parseSuccess.IsSuccess() && opBOMDetail.OPBOMParseType.Trim().Length > 0)
            {
                oldParseSuccess.AddMessages(parseSuccess);
                returnValue.AddMessages(oldParseSuccess);
                return returnValue;
            }

            //序列号长度比对
            if (opBOMDetail.OPBOMParseType.Trim().Length <= 0)
            {
                if (inputLength > 0 && barcode.Trim().Length != inputLength)
                {
                    returnValue.Add(new UserControl.Message(MessageType.Error, "$Error_SNLength_Wrong"));
                    return returnValue;
                }
            }

            //check NeedVendor
            if (!isSKDCartonCheck && checkNeedVendor == NeedVendor.NeedVendor_Y)
            {
                Messages checkNeedVendorMsg = new Messages();
                checkNeedVendorMsg = CheckNeedVebdor(newMINNO);

                if (!checkNeedVendorMsg.IsSuccess())
                {
                    returnValue.AddMessages(checkNeedVendorMsg);
                    return returnValue;
                }
            }

            if (!isSKDCartonCheck)
            {
                if (checkTypeSetting.IndexOf("," + OPBOMDetailCheckType.CHECK_LINKBARCODE.ToLower() + ",") >= 0)
                {
                    //Link barcode
                    if (string.IsNullOrEmpty(newMINNO.MItemCode))
                    {
                        newMINNO.MItemCode = mItemCode;
                    }

                    newMINNO.MItemPackedNo = barcode;
                    newMINNO.MSourceItemCode = opBOMDetail.OPBOMSourceItemCode;

                    if (opBOMDetail.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_KEYPARTS)
                    {
                        newMINNO.EAttribute1 = MCardType.MCardType_Keyparts;
                    }
                    else if (opBOMDetail.OPBOMItemControlType == BOMItemControlType.ITEM_CONTROL_LOT)
                    {
                        newMINNO.EAttribute1 = MCardType.MCardType_INNO;
                        newMINNO.Qty = opBOMDetail.OPBOMItemQty;
                    }

                    minno = newMINNO;
                }
                else
                {
                    minno = null;
                }
            }

            return returnValue;
        }

        #endregion

        #region 批次自动产生不良送修信息
        public void AutoCollectErrorInfo(IDomainDataProvider domainDataProvider, string moCode, string lotNo, string memo)
        {
            TSFacade tsFacade = new TSFacade(domainDataProvider);
            TSModelFacade tsModelFacade = new TSModelFacade(domainDataProvider);
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(domainDataProvider);
            BenQGuru.eMES.Domain.TS.TS tS = new BenQGuru.eMES.Domain.TS.TS();
            TSErrorCode tsErrorCode = new TSErrorCode();
            object parameter = systemSettingFacade.GetParameter("DEFAULTERRORCODE", "NGCOLLECTDEFAULTERRORCODE");
            if (parameter == null)
            {
                throw new Exception("$Error_NoDefaultErrorCode");
            }
            Parameter errorCodeParameter = parameter as Parameter;
            string errorCode = errorCodeParameter.ParameterAlias;
            object ecg2ec = tsModelFacade.GetErrorCodeGroup2ErrorCodeByecCode(errorCode);
            if (ecg2ec == null)
            {
                throw new Exception("$Error_ErrorCodeNoErrorGroup");
            }

            LotSimulationReport lotSimulationReport = GetLotSimulationReport(moCode, lotNo) as LotSimulationReport;

            tS.CardType = CardType.CardType_Product; ;
            tS.FormTime = lotSimulationReport.BeginTime;
            tS.FromDate = lotSimulationReport.BeginDate;
            tS.FromInputType = TSFacade.TSSource_OnWIP;
            tS.FromUser = lotSimulationReport.MaintainUser;
            tS.FromMemo = memo;
            tS.RMABillCode = lotSimulationReport.RMABillCode;
            tS.FromOPCode = lotSimulationReport.OPCode;
            tS.FromResourceCode = lotSimulationReport.ResCode;
            tS.FromRouteCode = lotSimulationReport.RouteCode;
            tS.FromSegmentCode = lotSimulationReport.SegmentCode;
            tS.FromShiftCode = lotSimulationReport.BeginShiftCode;
            tS.FromShiftDay = lotSimulationReport.BeginShiftDay;
            tS.FromShiftTypeCode = lotSimulationReport.ShiftTypeCode;
            tS.FromStepSequenceCode = lotSimulationReport.StepSequenceCode;
            tS.FromTimePeriodCode = lotSimulationReport.BeginTimePeriodCode;
            tS.ItemCode = lotSimulationReport.ItemCode;
            tS.MaintainDate = lotSimulationReport.BeginDate;
            tS.MaintainTime = lotSimulationReport.BeginTime;
            tS.MaintainUser = lotSimulationReport.MaintainUser;
            tS.MOCode = lotSimulationReport.MOCode;
            tS.ModelCode = lotSimulationReport.ModelCode;
            tS.RunningCard = lotSimulationReport.LotCode;
            tS.RunningCardSequence = lotSimulationReport.LotSeq;
            //tS.SourceCard = lotSimulationReport.SourceCard;
            //tS.SourceCardSequence = lotSimulationReport.SourceCardSequence;
            tS.FromOutLineRouteCode = lotSimulationReport.RouteCode;
            tS.TransactionStatus = TSFacade.TransactionStatus_None;
            //tS.TranslateCard = lotSimulationReport.TranslateCard;
            //tS.TranslateCardSequence = lotSimulationReport.TranslateCardSequence;
            tS.TSId = FormatHelper.GetUniqueID(lotSimulationReport.MOCode
                , tS.RunningCard, tS.RunningCardSequence.ToString());
            tS.TSStatus = TSStatus.TSStatus_New;
            //tS.Week = (new ReportHelper(DataProvider)).WeekOfYear(tS.FromShiftDay.ToString());
            tS.Month = int.Parse(tS.FromShiftDay.ToString().Substring(4, 2));
            tS.TSTimes = lotSimulationReport.NGTimes;
            tS.MOSeq = lotSimulationReport.MOSeq;
            tsFacade.AddTS(tS);

            tsErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)ecg2ec).ErrorCode;
            tsErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)ecg2ec).ErrorCodeGroup;
            tsErrorCode.ItemCode = tS.ItemCode;
            tsErrorCode.MaintainDate = tS.MaintainDate;
            tsErrorCode.MaintainTime = tS.MaintainTime;
            tsErrorCode.MaintainUser = tS.MaintainUser;
            tsErrorCode.MOCode = tS.MOCode;
            tsErrorCode.ModelCode = tS.ModelCode;
            tsErrorCode.RunningCard = tS.RunningCard;
            tsErrorCode.RunningCardSequence = tS.RunningCardSequence;
            tsErrorCode.TSId = tS.TSId;
            tsErrorCode.MOSeq = tS.MOSeq;
            tsFacade.AddTSErrorCode(tsErrorCode);
        }
        #endregion

        #region 不良送修信息 kathy

        //add by kathy @20130830
        //根据产品代码加载不良代码信息
        public object[] GetTSErrorCode(string itemCode)
        {
            object[] tsErrorCodes = this.DataProvider.CustomQuery(typeof(ErrorCodeForLot),
                          new SQLCondition(string.Format(
                          @"select a.ecode,a.ecgcode as ecgcode,b.ecgdesc from TBLECG2EC a
                            left join tblecg b
                            on a.ecgcode=b.ecgcode
                            left join TBLMODEL2ECG c
                            on b.ecgcode=c.ecgcode
                            left join TBLMODEL2ITEM d
                            on d.modelcode=c.modelcode
                            where d.itemcode = '{0}'
                            order by ecgcode", itemCode)));
            if (tsErrorCodes == null)
                return null;
            if (tsErrorCodes.Length > 0)
                return tsErrorCodes;
            else
                return null;
        }

        #endregion
        #region 拆箱、拆栈板

        public Messages RemoveFromCarton(string lotCode, string oldCartonCode , string userCode)
        {
            //次函数中未使用Trans

            Messages returnValue = new Messages();

            BenQGuru.eMES.Material.InventoryFacade inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(DataProvider);
            Package.PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(DataProvider);

            //删除Carton2Lot
            decimal cartonQty = 0;
            Carton2Lot oldCarton2Lot = (Carton2Lot)packageFacade.GetCarton2Lot(oldCartonCode.Trim().ToUpper(), lotCode.Trim().ToUpper());
            if (oldCarton2Lot != null)
            {
                cartonQty = oldCarton2Lot.CartonQty;
                packageFacade.DeleteCarton2Lot(oldCarton2Lot);

            }
            //end

            packageFacade.SubtractCollected(oldCartonCode, cartonQty);

            CARTONINFO oldCarton = packageFacade.GetCARTONINFO(oldCartonCode) as CARTONINFO;
            if (oldCarton.COLLECTED == 0)
            {
                packageFacade.DeleteCARTONINFO(oldCarton);
            }

            returnValue.Add(new UserControl.Message(MessageType.Success, "$CS_Delete_Success"));

            //记log
            packageFacade.SaveRemoveCarton2LotLog(oldCartonCode, lotCode, cartonQty, userCode);

            return returnValue;
        }

        //public Messages RemoveFromPallet(string rcard, string userCode, bool checkIsInStack)
        //{
        //    //次函数中未使用Trans

        //    Messages returnValue = new Messages();

        //    Package.PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(DataProvider);

        //    Pallet2Lot pallet2Lot = (Pallet2Lot)packageFacade.GetPallet2RCardByRCard(rcard.Trim().ToUpper());
        //    if (pallet2RCard == null)
        //    {
        //        return returnValue;
        //        //returnValue.Add(new Message(MessageType.Error, "$CS_Pallet2RCardNotFound"));
        //    }
        //    else
        //    {
        //        Pallet pallet = (Pallet)packageFacade.GetPallet(pallet2RCard.PalletCode);
        //        if (pallet == null)
        //        {
        //            return returnValue;
        //            //returnValue.Add(new UserControl.Message(MessageType.Error, "$CS_PALLETNO_IS_NOT_EXIT"));
        //        }
        //        else
        //        {
        //            if (checkIsInStack)
        //            {
        //                SimulationReport simulationReport = (SimulationReport)this.GetLastSimulationReport(rcard);
        //                string cartonCode = string.Empty;
        //                if (simulationReport != null)
        //                {
        //                    cartonCode = simulationReport.CartonCode;
        //                }

        //                InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);

        //                object[] StacktoRcardByRcardList = inventoryFacade.QueryStacktoRcardByRcardAndCarton(rcard, cartonCode);

        //                if (StacktoRcardByRcardList != null)
        //                {
        //                    returnValue.Add(new UserControl.Message(MessageType.Error, rcard + " $CS_SERIAL_EXIST_Storge_Not_Remove"));
        //                    return returnValue;
        //                }
        //            }

        //            packageFacade.DeletePallet2RCard(pallet2RCard);

        //            pallet.RCardCount--;

        //            if (pallet.RCardCount == 0)
        //            {
        //                packageFacade.DeletePallet(pallet);
        //            }
        //            else
        //            {
        //                packageFacade.UpdatePallet(pallet);
        //            }

        //            returnValue.Add(new UserControl.Message(MessageType.Success, "$CS_Delete_Success"));

        //            //记log
        //            packageFacade.SaveRemovePallet2RcardLog(pallet2RCard.PalletCode, rcard, userCode);


        //        }
        //    }

        //    return returnValue;
        //}



        //public void TryToDeleteRCardFromLot(string rCard)
        //{
        //    this.TryToDeleteRCardFromLot(rCard, true);
        //}

        //public void TryToDeleteRCardFromLot(string rCard, bool needCheckLotStatusAndType)
        //{
        //    rCard = rCard.Trim().ToUpper();
        //    OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
        //    TryFacade tryFacade = new TryFacade(this.DataProvider);
        //    ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);

        //    object[] lot2CardList = oqcFacade.ExactQueryOQCLot2Card(rCard);
        //    if (lot2CardList != null)
        //    {
        //        foreach (OQCLot2Card lot2Card in lot2CardList)
        //        {
        //            bool needToDelete = true;

        //            //判断条件一：采集不良时（除FQC采集样本不良）
        //            //              如果该产品已经存在于批中，
        //            //              批是正常批tbllot.oqclottype=oqclottype_normal
        //            //              批尚未判定Reject或Pass
        //            OQCLot lot = (OQCLot)oqcFacade.GetOQCLot(lot2Card.LOTNO, lot2Card.LotSequence);
        //            if (lot == null)
        //            {
        //                needToDelete = false;
        //            }
        //            else
        //            {
        //                if (needCheckLotStatusAndType)
        //                {
        //                    if (lot.OQCLotType != OQCLotType.OQCLotType_Normal)
        //                    {
        //                        needToDelete = false;
        //                    }
        //                    else if (lot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || lot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject)
        //                    {
        //                        needToDelete = false;
        //                    }
        //                }
        //            }

        //            //判断条件二：产品不是所在批的检验样本
        //            if (needToDelete)
        //            {
        //                object[] lot2CardCheckList = oqcFacade.ExtraQueryOQCLot2CardCheck(rCard, string.Empty, string.Empty, lot2Card.LOTNO, lot2Card.LotSequence.ToString());

        //                if (lot2CardCheckList != null && lot2CardCheckList.Length > 0)
        //                {
        //                    needToDelete = false;
        //                }
        //            }

        //            if (needToDelete)
        //            {
        //                //删除动作一：
        //                //              将产品从tbllot2card中删除，
        //                //              将tbllot的lotsize减一，
        //                //              如果lotsize为零则删除tbllot，tbltry2lot的内容
        //                oqcFacade.DeleteOQCLot2Card(lot2Card);

        //                lot.LotSize--;
        //                oqcFacade.UpdateOQCLot(lot);

        //                if (lot.LotSize <= 0)
        //                {
        //                    OQCLOTCheckList lotCheckList = (OQCLOTCheckList)oqcFacade.GetOQCLOTCheckList(lot.LOTNO, lot.LotSequence);
        //                    if (lotCheckList != null)
        //                    {
        //                        oqcFacade.DeleteOQCLOTCheckList(lotCheckList);
        //                    }

        //                    oqcFacade.DeleteOQCLot(lot);

        //                    object[] try2LotList = tryFacade.GetTry2LotList(lot.LOTNO);
        //                    if (try2LotList != null)
        //                    {
        //                        foreach (Try2Lot try2Lot in try2LotList)
        //                        {
        //                            tryFacade.DeleteTry2Lot(try2Lot);
        //                        }
        //                    }
        //                }

        //                //删除动作二：
        //                //              根据lotno+rcard删除tblfrozen中的值
        //                object[] frozenList = oqcFacade.QueryFrozen(rCard, rCard, lot.LOTNO, string.Empty, string.Empty, string.Empty, -1, -1, -1, -1, int.MinValue, int.MaxValue);
        //                if (frozenList != null)
        //                {
        //                    foreach (Frozen frozen in frozenList)
        //                    {
        //                        oqcFacade.DeleteFrozen(frozen);
        //                    }
        //                }

        //                //删除动作三：
        //                //              删除tbltempreworkrcard中的值
        //                //              如果该lot建议返工的序列号已经全部删除，则删除tbltempreworklotno的值。 
        //                ReworkRcard reworkRcard = (ReworkRcard)reworkFacade.GetReworkRcard(lot.LOTNO, rCard);
        //                if (reworkRcard != null)
        //                {
        //                    reworkFacade.DeleteReworkRcard(reworkRcard);

        //                    object[] reworkRcardList = reworkFacade.QueryReworkRcard(lot.LOTNO);
        //                    if (reworkRcardList == null || reworkRcardList.Length <= 0)
        //                    {
        //                        ReworkLotNo reworkLotNo = (ReworkLotNo)reworkFacade.GetReworkLotNo(lot.LOTNO);
        //                        if (reworkLotNo != null)
        //                        {
        //                            reworkFacade.DeleteReworkLotNo(reworkLotNo);
        //                        }
        //                    }
        //                }

        //                //删除动作四：
        //                //              清空字段tblsimulation.lotno，tblsimulationreport.lotno
        //                object[] simulationList = QuerySimulation(rCard);
        //                if (simulationList != null)
        //                {
        //                    foreach (Simulation simulation in simulationList)
        //                    {
        //                        simulation.LOTNO = string.Empty;
        //                        UpdateSimulation(simulation);
        //                    }
        //                }

        //                object[] simulationReportList = QuerySimulationReport(rCard);
        //                if (simulationReportList != null)
        //                {
        //                    foreach (SimulationReport simulationReport in simulationReportList)
        //                    {
        //                        simulationReport.LOTNO = string.Empty;
        //                        UpdateSimulationReport(simulationReport);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion

    }


    [Serializable]
    //工单状态 Note By Simone Xu
    public class MOManufactureStatus
    {
        private ArrayList _list = new ArrayList();

        public MOManufactureStatus()
        {
            this._list.Add(MOManufactureStatus.MOSTATUS_INITIAL);
            this._list.Add(MOManufactureStatus.MOSTATUS_RELEASE);
            this._list.Add(MOManufactureStatus.MOSTATUS_OPEN);
            this._list.Add(MOManufactureStatus.MOSTATUS_CLOSE);
            this._list.Add(MOManufactureStatus.MOSTATUS_PENDING);
        }

        public const string MOSTATUS_INITIAL = "mostatus_initial";		//初始
        public const string MOSTATUS_RELEASE = "mostatus_release";		//下发
        public const string MOSTATUS_OPEN = "mostatus_open";			//生产中
        public const string MOSTATUS_CLOSE = "mostatus_close";			//关单
        public const string MOSTATUS_PENDING = "mostatus_pending";		//暂停

        #region IInternalSystemVariable 成员

        public string Group
        {
            get
            {
                return "MOManufactureStatus";
            }
        }

        public ArrayList Items
        {
            get
            {
                return this._list;
            }
        }

        #endregion
    }




    [Serializable]
    public class ONWIPItemQueryObject : DomainObject
    {
        [FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
        public string MItemCode;
    }
    [Serializable]
    public class ONWIPItemObject : DomainObject
    {
        [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
        public string LotCode;
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, false)]
        public string LotSeq;
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCODE;
    }

}

