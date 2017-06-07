using System;
using UserControl;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.LotDataCollect.Action
{
    /// <summary>
    /// 归属工单采集
    /// 问题：多站同时投入同一工单时，会发生冲突
    /// </summary>
    public class ActionGoToMO : IActionWithStatus
    {

        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionGoToMO()
        //		{	
        //		}

        public ActionGoToMO(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                //				if (_domainDataProvider == null)
                //				{
                //					_domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                //				}

                return _domainDataProvider;
            }
        }

        public Messages CheckIn(ActionEventArgs actionEventArgs)
        {
            ((GoToMOActionEventArgs)actionEventArgs).PassCheck = true;

            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "GetIDInfo");
            dataCollectDebug.WhenFunctionIn(messages);

            try
            {
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                MOFacade moFacade = new MOFacade(this.DataProvider);
                MOLotFacade moLotFacade = new MOLotFacade(this.DataProvider);
                BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
                ModelFacade mf = new ModelFacade(this.DataProvider);
                SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);

                //AMOI  MARK  START  20050803 序号默认为初始，FOR 重工序列号重复
                actionEventArgs.ProductInfo.NowSimulation.LotSeq = ActionOnLineHelper.StartSeq;
                //AMOI  MARK  END

                if (((GoToMOActionEventArgs)actionEventArgs).MOCode == null || ((GoToMOActionEventArgs)actionEventArgs).MOCode.Trim() == string.Empty)
                {
                    throw new Exception("$CS_Sys_GoToMO_Lost_MOParam");
                }


                #region 检查途程
                MO mo = null;
                if (actionEventArgs.CurrentMO != null)
                {
                    mo = actionEventArgs.CurrentMO;
                }
                else
                {
                    mo = (MO)moFacade.GetMO(((GoToMOActionEventArgs)actionEventArgs).MOCode);
                    actionEventArgs.CurrentMO = mo;
                }
                if (mo == null)
                {
                    throw new Exception("$CS_MO_Not_Exit");
                }

                MO2Route route = (MO2Route)moFacade.GetMONormalRouteByMOCode(mo.MOCode);
                if (route == null)
                {
                    throw new Exception("$CS_MOnotNormalRoute");
                }

                ItemRoute2OP op = dataCollectFacade.GetMORouteFirstOP(mo.MOCode, route.RouteCode);

                if (dataModel.GetOperation2Resource(op.OPCode, actionEventArgs.ResourceCode) == null)
                {
                    throw new Exception("$CS_Route_Failed_FirstOP $Domain_MO =" + mo.MOCode);
                }
                #endregion

                #region 检查工单

                //工单状态检查
                if (!dataCollectFacade.CheckMO(mo))
                {
                    throw new Exception("$CS_MO_Status_Error $CS_Param_MOStatus=$" + mo.MOStatus);
                }

                /*1.Simulation 没有记录
                 *	    a. MO Running Card 没有重复		OK
                 *     b. MO Running Card 有重复		Exception
                 * 
                 * 2. Simulation 有记录
                 *		a.  Simulation 工单 和 当前工单 一样			OK
                 *		b.  Simulation 工单 和 当前工单 不一样 
                 *			 b1.  当前工单 是重工工单				
                 *					b11.  MO Running Card 没有重复		OK
                 *					b12.  MO Running Card 有重复			Exception
                 *			 b2.  当前工单不是重工工单						Exception
                 *					
                 */

                Parameter parameter = (Parameter)systemFacade.GetParameter(mo.MOType, BenQGuru.eMES.Web.Helper.MOType.GroupType);

                if (parameter == null)
                {
                    throw new Exception("$CS_MOType_Lost");
                }
                mo.MOType = parameter.ParameterValue;

                if (actionEventArgs.ProductInfo.LastSimulation != null)
                {
                    //归属工单和归属工序相同也不允许
                    if ((mo.MOCode == actionEventArgs.ProductInfo.LastSimulation.MOCode)                                                                                          )
                    {
                        ((GoToMOActionEventArgs)actionEventArgs).PassCheck = false;
                        return messages;
                    }
                    //Laws Lu,2005/10/20,修改	Lucky的需求	CS112
                    //建议返工工单归属采集时增加判断条件，也就是说只有没有在制记录的或者已经拆解的产品序列号才能归属返工工单
                    if (actionEventArgs.ProductInfo.LastSimulation.IsComplete == "0")
                    {
                        throw new Exception("$CS_PRODUCT_STILL_INLINE_NOT_BELONG_MO ,$CS_Param_ID=" + actionEventArgs.LotCode);
                    }


                }
               
                // 投入量检查 
                if (mo.IsControlInput == "1")	// 客户在工单中勾选了“限制投入量”则检查工单可投入量，否则不检查
                {
                    if (mo.MOPlanQty - mo.MOInputQty + mo.MOScrapQty + mo.MOOffQty - mo.IDMergeRule < 0)
                    {
                        throw new Exception("$CS_MOInputOut $Domain_MO =" + mo.MOCode);
                    }
                }
                #endregion

                #region 检查库房状态
                if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"] != null
                    && System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                {
                    Material.WarehouseFacade wFAC = new BenQGuru.eMES.Material.WarehouseFacade(DataProvider);
                    //读资源对应的产线
                    BenQGuru.eMES.BaseSetting.BaseModelFacade facade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                    object objResource = facade.GetResource(actionEventArgs.ResourceCode);

                    string strSSCode = ((BenQGuru.eMES.Domain.BaseSetting.Resource)objResource).StepSequenceCode;
                    object obj = wFAC.GetWarehouseByMoSS(mo.MOCode, strSSCode);
                    if (obj != null)
                    {
                        Domain.Warehouse.Warehouse wh = obj as Domain.Warehouse.Warehouse;
                        //Laws Lu，2006/02/20，修改/无需工段代码
                        string strStatus = wFAC.GetWarehouseStatus(wh.WarehouseCode, wh.FactoryCode);
                        if (strStatus == Domain.Warehouse.Warehouse.WarehouseStatus_Cycle)
                        {
                            throw new Exception("$CS_LINE_IS_HOLD");
                        }
                    }
                }
                #endregion

                #region 填写新SIMULATION 归属工单 批次数量从tblmo2lotlink 中获取

                Domain.MOModel.MO2LotLink mo2lotlink = moLotFacade.GetMO2LotLink(actionEventArgs.LotCode, mo.MOCode) as Domain.MOModel.MO2LotLink;
                if (mo2lotlink != null)
                {
                    if (mo2lotlink.LotStatus != LotStatusForMO2LotLink.LOTSTATUS_STOP)
                    {
                        mo2lotlink.LotStatus = LotStatusForMO2LotLink.LOTSTATUS_USE;
                        moLotFacade.UpdateMO2LotLink(mo2lotlink);

                        actionEventArgs.ProductInfo.NowSimulation.LotQty = mo2lotlink.LotQty;
                        actionEventArgs.ProductInfo.NowSimulation.GoodQty = mo2lotlink.LotQty;
                        actionEventArgs.ProductInfo.NowSimulation.NGQty = 0;
                    }
                    else
                    {
                        throw new Exception("$CS_Mo2LotLink_Error_Status");
                    }
                }
                else
                {
                    throw new Exception("$CS_Mo2LotLink_Not_Exist");
                }
              

                //messages.AddMessages( dataCollectFacade.WriteSimulation(id,actionType,resourceCode,userCode,product));
                actionEventArgs.ProductInfo.NowSimulation.RouteCode = route.RouteCode;
                actionEventArgs.ProductInfo.NowSimulation.OPCode = op.OPCode;
                actionEventArgs.ProductInfo.NowSimulation.LotStatus =  LotStatusForMO2LotLink.LOTSTATUS_USE ;
                actionEventArgs.ProductInfo.NowSimulation.LastAction = ActionType.DataCollectAction_GoMO;
                actionEventArgs.ProductInfo.NowSimulation.ActionList = ";" + ActionType.DataCollectAction_GoMO + ";";
                actionEventArgs.ProductInfo.NowSimulation.LotCode = actionEventArgs.LotCode;
                actionEventArgs.ProductInfo.NowSimulation.MOCode = mo.MOCode;
                actionEventArgs.ProductInfo.NowSimulation.ItemCode = mo.ItemCode;
                Model model = mf.GetModelByItemCode(mo.ItemCode);
                if (model == null)
                {
                    throw new Exception("$CS_Model_Lost $CS_Param_ItemCode=" + mo.ItemCode);
                }
                actionEventArgs.ProductInfo.NowSimulation.ModelCode = model.ModelCode;
                actionEventArgs.ProductInfo.NowSimulation.IsComplete = ProductComplete.NoComplete;
                actionEventArgs.ProductInfo.NowSimulation.CollectStatus = CollectStatus.CollectStatus_BEGIN;
                actionEventArgs.ProductInfo.NowSimulation.ResCode = actionEventArgs.ResourceCode;
                actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.GOOD;
                actionEventArgs.ProductInfo.NowSimulation.FromOP = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.FromRoute = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.CartonCode = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.LotNo = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.PalletCode = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.NGTimes = ActionOnLineHelper.StartNGTimes;
                actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.GOOD;
                actionEventArgs.ProductInfo.NowSimulation.LotStatus = LotStatusForMO2LotLink.LOTSTATUS_USE;
                actionEventArgs.ProductInfo.NowSimulation.IsHold = 0;
                actionEventArgs.ProductInfo.NowSimulation.MOSeq = (int)mo.MOSeq;     // Added by Icyer 2007/07/03
                //update by andy.xin rmaBillCode
                //actionEventArgs.ProductInfo.NowSimulation.RMABillCode = rmaBillCode; //mo.RMABillCode;

                #endregion
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }

            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        // Added by Icyer 2005/10/28
        public Messages CheckIn(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            ((GoToMOActionEventArgs)actionEventArgs).PassCheck = true;

            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "GetIDInfo(WithCheck)");
            dataCollectDebug.WhenFunctionIn(messages);

            try
            {
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                MOFacade moFacade = new MOFacade(this.DataProvider);
                MOLotFacade moLotFacade = new MOLotFacade(this.DataProvider);
                BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
                ModelFacade mf = new ModelFacade(this.DataProvider);
                SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);

                //AMOI  MARK  START  20050803 序号默认为初始，FOR 重工序列号重复
                actionEventArgs.ProductInfo.NowSimulation.LotSeq = ActionOnLineHelper.StartSeq;
                //AMOI  MARK  END

                if (((GoToMOActionEventArgs)actionEventArgs).MOCode == null || ((GoToMOActionEventArgs)actionEventArgs).MOCode.Trim() == string.Empty)
                {
                    throw new Exception("$CS_Sys_GoToMO_Lost_MOParam");
                }

                #region 检查途程
                //如果ActionCheckStatus中没有检查过途程，则检查途程，并将CheckedOP设为True
                MO mo = actionCheckStatus.MO;
                if (mo == null || mo.IsControlInput == "1")
                {
                    //					MO mo = null;
                    if (actionEventArgs.CurrentMO != null)
                    {
                        mo = actionEventArgs.CurrentMO;
                    }
                    else
                    {
                        mo = (MO)moFacade.GetMO(((GoToMOActionEventArgs)actionEventArgs).MOCode);
                        actionEventArgs.CurrentMO = mo;
                    }
                    //					mo = (MO)moFacade.GetMO(((GoToMOActionEventArgs)actionEventArgs).MOCode);
                    if (mo == null)
                    {
                        throw new Exception("$CS_MO_Not_Exit");
                    }
                    actionCheckStatus.MO = mo;
                }
                MO2Route route = actionCheckStatus.Route;
                if (route == null)
                {
                    route = (MO2Route)moFacade.GetMONormalRouteByMOCode(mo.MOCode);
                    if (route == null)
                    {
                        throw new Exception("$CS_MOnotNormalRoute");
                    }
                    actionCheckStatus.Route = route;
                }
                ItemRoute2OP op = actionCheckStatus.OP;
                if (op == null)
                {
                    op = dataCollectFacade.GetMORouteFirstOP(mo.MOCode, route.RouteCode);
                    actionCheckStatus.OP = op;
                }
                if (actionCheckStatus.CheckedOP == false)
                {
                    if (dataModel.GetOperation2Resource(op.OPCode, actionEventArgs.ResourceCode) == null)
                    {
                        throw new Exception("$CS_Route_Failed_FirstOP $Domain_MO =" + mo.MOCode);
                    }
                    actionCheckStatus.CheckedOP = true;
                }
                #endregion

                #region 检查工单

                //工单状态检查
                //如果ActionCheckStatus中没有检查过工单状态，则检查，并将CheckedMO设为True
                if (actionCheckStatus.CheckedMO == false)
                {
                    if (!dataCollectFacade.CheckMO(mo))
                    {
                        throw new Exception("$CS_MO_Status_Error $CS_Param_MOStatus=$" + mo.MOStatus);
                    }
                    actionCheckStatus.CheckedMO = true;
                }

                /*1.Simulation 没有记录
                 *	    a. MO Running Card 没有重复		OK
                 *     b. MO Running Card 有重复		Exception
                 * 
                 * 2. Simulation 有记录
                 *		a.  Simulation 工单 和 当前工单 一样			OK
                 *		b.  Simulation 工单 和 当前工单 不一样 
                 *			 b1.  当前工单 是重工工单				
                 *					b11.  MO Running Card 没有重复		OK
                 *					b12.  MO Running Card 有重复			Exception
                 *			 b2.  当前工单不是重工工单						Exception
                 *					
                 */

                //如果ActionCheckStatus中没有检查过工单状态，则检查，并将CheckedMO设为True
                string rmaBillCode = "";
                string strMOTypeParamValue = actionCheckStatus.MOTypeParamValue;
                if (strMOTypeParamValue == string.Empty)
                {
                    Parameter parameter = (Parameter)systemFacade.GetParameter(mo.MOType, BenQGuru.eMES.Web.Helper.MOType.GroupType);

                    if (parameter == null)
                    {
                        throw new Exception("$CS_MOType_Lost");
                    }
                    //mo.MOType = parameter.ParameterValue;
                    strMOTypeParamValue = parameter.ParameterValue;
                    actionCheckStatus.MOTypeParamValue = strMOTypeParamValue;

  
                }



                object objOffCard = null;

                if (actionEventArgs.ProductInfo.LastSimulation != null)
                {
                    if ((mo.MOCode == actionEventArgs.ProductInfo.LastSimulation.MOCode))
                    {
                        ((GoToMOActionEventArgs)actionEventArgs).PassCheck = false;
                        return messages;
                    }
                    //Laws Lu,2005/10/20,修改	Lucky的需求	CS112
                    //建议返工工单归属采集时增加判断条件，也就是说只有没有在制记录的或者已经拆解的产品序列号才能归属返工工单
                    if (actionEventArgs.ProductInfo.LastSimulation.IsComplete == "0")
                    {
                        throw new Exception("$CS_PRODUCT_STILL_INLINE_NOT_BELONG_MO ,$CS_Param_ID " + actionEventArgs.LotCode);
                    }
                }

                if (mo.IsControlInput == "1")	// 客户在工单中勾选了“限制投入量”则检查工单可投入量，否则不检查
                {
                    if (mo.MOPlanQty - mo.MOInputQty + mo.MOScrapQty + mo.MOOffQty - mo.IDMergeRule < 0)
                    {
                        throw new Exception("$CS_MOInputOut");
                    }
                }
                #endregion

                #region 检查库房状态
                if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"] != null
                    && System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                {
                    Material.WarehouseFacade wFAC = new BenQGuru.eMES.Material.WarehouseFacade(DataProvider);
                    //读资源对应的产线
                    BenQGuru.eMES.BaseSetting.BaseModelFacade facade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                    object objResource = facade.GetResource(actionEventArgs.ResourceCode);

                    string strSSCode = ((BenQGuru.eMES.Domain.BaseSetting.Resource)objResource).StepSequenceCode;

                    object obj = wFAC.GetWarehouseByMoSS(mo.MOCode, strSSCode);
                    if (obj != null)
                    {
                        Domain.Warehouse.Warehouse wh = obj as Domain.Warehouse.Warehouse;
                        string strStatus = wFAC.GetWarehouseStatus(wh.WarehouseCode, wh.FactoryCode);
                        //Laws Lu，2006/02/20，修改/无需工段代码
                        if (strStatus == Domain.Warehouse.Warehouse.WarehouseStatus_Cycle)
                        {
                            throw new Exception("$CS_LINE_IS_HOLD");
                        }
                    }
                }
                #endregion

                #region 填写新SIMULATION

                Domain.MOModel.MO2LotLink mo2lotlink = moLotFacade.GetMO2LotLink(actionEventArgs.LotCode, mo.MOCode) as Domain.MOModel.MO2LotLink;
                if (mo2lotlink != null)
                {
                    if (mo2lotlink.LotStatus != LotStatusForMO2LotLink.LOTSTATUS_STOP)
                    {
                        mo2lotlink.LotStatus = LotStatusForMO2LotLink.LOTSTATUS_USE;
                        moLotFacade.UpdateMO2LotLink(mo2lotlink);

                        actionEventArgs.ProductInfo.NowSimulation.LotQty = mo2lotlink.LotQty;
                        actionEventArgs.ProductInfo.NowSimulation.GoodQty = mo2lotlink.LotQty;
                        actionEventArgs.ProductInfo.NowSimulation.NGQty = 0;
                    }
                    else
                    {
                        throw new Exception("$CS_Mo2LotLink_Error_Status");
                    }
                }
                else
                {
                    throw new Exception("$CS_Mo2LotLink_Not_Exist");
                }

                //messages.AddMessages( dataCollectFacade.WriteSimulation(id,actionType,resourceCode,userCode,product));
                actionEventArgs.ProductInfo.NowSimulation.RouteCode = route.RouteCode;
                actionEventArgs.ProductInfo.NowSimulation.OPCode = op.OPCode;

                actionEventArgs.ProductInfo.NowSimulation.LastAction = ActionType.DataCollectAction_GoMO;
                actionEventArgs.ProductInfo.NowSimulation.ActionList = ";" + ActionType.DataCollectAction_GoMO + ";";
                actionEventArgs.ProductInfo.NowSimulation.LotCode = actionEventArgs.LotCode;
                actionEventArgs.ProductInfo.NowSimulation.MOCode = mo.MOCode;
                actionEventArgs.ProductInfo.NowSimulation.ItemCode = mo.ItemCode;

                //update by andy xin rmaBillCode
                actionEventArgs.ProductInfo.NowSimulation.RMABillCode = rmaBillCode;//mo.RMABillCode;

                Model model = actionCheckStatus.Model;
                if (model == null)
                {
                    model = mf.GetModelByItemCode(mo.ItemCode);
                    actionCheckStatus.Model = model;
                    if (model == null)
                    {
                        throw new Exception("$CS_Model_Lost $CS_Param_ItemCode=" + mo.ItemCode);
                    }
                }
                // Changed end
                actionEventArgs.ProductInfo.NowSimulation.ModelCode = model.ModelCode;
                actionEventArgs.ProductInfo.NowSimulation.IsComplete = ProductComplete.NoComplete;
                actionEventArgs.ProductInfo.NowSimulation.ResCode = actionEventArgs.ResourceCode;
                actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.GOOD;
                actionEventArgs.ProductInfo.NowSimulation.LotStatus = LotStatusForMO2LotLink.LOTSTATUS_USE;
                actionEventArgs.ProductInfo.NowSimulation.CollectStatus = CollectStatus.CollectStatus_BEGIN;
                actionEventArgs.ProductInfo.NowSimulation.FromOP = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.FromRoute = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.CartonCode = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.LotNo = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.PalletCode = ActionOnLineHelper.StringNull;
                actionEventArgs.ProductInfo.NowSimulation.NGTimes = ActionOnLineHelper.StartNGTimes;
                actionEventArgs.ProductInfo.NowSimulation.IsHold = 0;
                actionEventArgs.ProductInfo.NowSimulation.MOSeq = (int)mo.MOSeq;     // Added by Icyer 2007/07/03
                #endregion
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }

            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }


        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            ((GoToMOActionEventArgs)actionEventArgs).MOCode = ((GoToMOActionEventArgs)actionEventArgs).MOCode.Trim().ToUpper();
            actionEventArgs.LotCode = actionEventArgs.LotCode.Trim().ToUpper();

            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);

            if (((GoToMOActionEventArgs)actionEventArgs).MOCode.Trim() == String.Empty)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_MOCode"));
            }

            if (((GoToMOActionEventArgs)actionEventArgs).LotCode.Trim() == String.Empty)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCard"));
            }
            //add by hiro.chen 08/11/18 checkISDown
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            //messages.AddMessages(dataCollectFacade.CheckISDown(((GoToMOActionEventArgs)actionEventArgs).LotCode.Trim()));
            //end

            try
            {
                if (messages.IsSuccess())
                {
                    actionEventArgs.ProductInfo.NowSimulation.EAttribute1 = (actionEventArgs as GoToMOActionEventArgs).Memo;
                    //actionEventArgs.ProductInfo.NowSimulation.RMABillCode = (actionEventArgs as GoToMOActionEventArgs).ProductInfo.CurrentMO.MOCode;

                    messages.AddMessages(this.CheckIn(actionEventArgs));
                    // Added by Icyer 2006/12/05
                    TakeDownCarton(actionEventArgs);
                    // Added end

                    //Laws Lu,2006/07/05 add support RMA
                    if (actionEventArgs.CurrentMO != null)
                    {
                        actionEventArgs.ProductInfo.NowSimulation.RMABillCode = actionEventArgs.CurrentMO.RMABillCode;
                    }

                    if (!((GoToMOActionEventArgs)actionEventArgs).PassCheck)
                    {
                        throw new Exception("$CS_ID_Has_Already_Belong_To_This_MO $CS_Param_ID="
                            + actionEventArgs.LotCode + " $Domain_MO=" + ((GoToMOActionEventArgs)actionEventArgs).MOCode);
                    }

                    if (messages.IsSuccess())
                    {
                        ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                        //actionEventArgs.ProductInfo.NowSimulation.Eattribute1 = (actionEventArgs as GoToMOActionEventArgs).Memo;

                        // Added By Hi1/Venus Feng on 20081114 for Hisense Version : remove pallet and carton
                        // 拆箱和拆Pallet
                        DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                         
     
                        // End Added

                        messages.AddMessages(dataCollect.Execute(actionEventArgs));

                        if (messages.IsSuccess())
                        {
                            //Laws Lu,2005/10/24,注释
                            // Added by Jane Shu	Date:2005/06/02
                            //							if ( actionEventArgs.ProductInfo.NowSimulation != null )
                            //							{
                            //this.updateMOQty(actionEventArgs.ProductInfo.NowSimulation.MOCode, actionEventArgs.UserCode);
                            this.updateItem2Route(actionEventArgs.ProductInfo.NowSimulation.ItemCode, actionEventArgs.ProductInfo.NowSimulation.RouteCode, actionEventArgs.UserCode);
                            //							}

                            // 将ID添加到MO范围表内		Added by Jane Shu	Date:2005/06/03	
                            MORunningCardFacade cardFacade = new MORunningCardFacade(this.DataProvider);
                            MORunningCard card = cardFacade.CreateNewMORunningCard();

                            DBDateTime dbDateTime;
                            //Laws Lu,2006/11/13 uniform system collect date
                            //Laws Lu,2006/11/13 uniform system collect date
                            if (actionEventArgs.ProductInfo.WorkDateTime != null)
                            {
                                dbDateTime = actionEventArgs.ProductInfo.WorkDateTime;

                            }
                            else
                            {
                                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                                actionEventArgs.ProductInfo.WorkDateTime = dbDateTime;
                            }

                            card.MOCode = ((GoToMOActionEventArgs)actionEventArgs).MOCode.ToString();
                            card.MORunningCardStart = actionEventArgs.LotCode;
                            card.MORunningCardEnd = actionEventArgs.LotCode;
                            card.MaintainUser = actionEventArgs.UserCode;

                            card.MaintainDate = dbDateTime.DBDate;
                            card.MaintainTime = dbDateTime.DBTime;

                            card.EAttribute1 = (actionEventArgs as GoToMOActionEventArgs).Memo;
                            // Added by Icyer 2007/07/02
                            MOFacade moFacade = new MOFacade(this.DataProvider);
                            MO mo = (MO)moFacade.GetMO(card.MOCode);
                            card.MOSeq = mo.MOSeq;
                            // Added end

                            cardFacade.AddMORunningCard(card);


                            //ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                            //messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }

            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        // Added by Icyer 2005/10/28
        //扩展一个带ActionCheckStatus参数的方法
        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            ((GoToMOActionEventArgs)actionEventArgs).MOCode = ((GoToMOActionEventArgs)actionEventArgs).MOCode.Trim().ToUpper();
            actionEventArgs.ProductInfo.NowSimulation.EAttribute1 = (actionEventArgs as GoToMOActionEventArgs).Memo;
            //actionEventArgs.ProductInfo.NowSimulation.RMABillCode = (actionEventArgs as GoToMOActionEventArgs).ProductInfo.CurrentMO.mor;

            actionEventArgs.LotCode = actionEventArgs.LotCode.Trim().ToUpper();

            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);

            if (((GoToMOActionEventArgs)actionEventArgs).MOCode.Trim() == String.Empty)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_MOCode"));
            }

            if (((GoToMOActionEventArgs)actionEventArgs).LotCode.Trim() == String.Empty)
            {
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCard"));
            }

            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            MOFacade moFacade = new MOFacade(this.DataProvider);
            try
            {
                if (messages.IsSuccess())
                {
                    //调用含有ActionCheckStatus参数的CheckIn方法
                    messages.AddMessages(this.CheckIn(actionEventArgs, actionCheckStatus));
                    // Added by Icyer 2006/12/05
                    TakeDownCarton(actionEventArgs);
                    // Added end

                    if (!((GoToMOActionEventArgs)actionEventArgs).PassCheck)
                    {
                        throw new Exception("$CS_ID_Has_Already_Belong_To_This_MO $CS_Param_ID="
                            + actionEventArgs.LotCode + " $Domain_MO=" + ((GoToMOActionEventArgs)actionEventArgs).MOCode);
                    }

                    if (messages.IsSuccess())
                    {
                        // 拆箱和拆Pallet

                        actionEventArgs.ProductInfo.NowSimulation.EAttribute1 = (actionEventArgs as GoToMOActionEventArgs).Memo;

                        if (actionCheckStatus.NeedUpdateSimulation)
                        {
                            ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                            messages.AddMessages(dataCollect.Execute(actionEventArgs));
                        }
                        else
                        {
                            ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                            messages.AddMessages(dataCollect.Execute(actionEventArgs, actionCheckStatus));
                        }

                        if (messages.IsSuccess())
                        {
                            moFacade.UpdateMOInPutQty(actionEventArgs.ProductInfo.NowSimulation.MOCode, actionEventArgs.CurrentMO, (int)actionEventArgs.ProductInfo.NowSimulation.LotQty);
                            //是否执行过updateItem2Route操作
                            if (actionCheckStatus.IsUpdateRefItem2Route == false)
                            {
                                this.updateItem2Route(actionEventArgs.ProductInfo.NowSimulation.ItemCode, actionEventArgs.ProductInfo.NowSimulation.RouteCode, actionEventArgs.UserCode);
                                actionCheckStatus.IsUpdateRefItem2Route = true;
                            }

                            MORunningCardFacade cardFacade = new MORunningCardFacade(this.DataProvider);
                            MORunningCard card = cardFacade.CreateNewMORunningCard();

                            card.MOCode = ((GoToMOActionEventArgs)actionEventArgs).MOCode.ToString();
                            card.MORunningCardStart = actionEventArgs.LotCode;
                            card.MORunningCardEnd = actionEventArgs.LotCode;
                            card.MaintainUser = actionEventArgs.UserCode;

                            DBDateTime dbDateTime;
                            //Laws Lu,2006/11/13 uniform system collect date
                            //Laws Lu,2006/11/13 uniform system collect date
                            if (actionEventArgs.ProductInfo.WorkDateTime != null)
                            {
                                dbDateTime = actionEventArgs.ProductInfo.WorkDateTime;

                            }
                            else
                            {
                                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                                actionEventArgs.ProductInfo.WorkDateTime = dbDateTime;
                            }

                            card.MaintainDate = dbDateTime.DBDate;
                            card.MaintainTime = dbDateTime.DBTime;
                            // Added by Icyer 2007/07/02
                            MO mo = (MO)moFacade.GetMO(card.MOCode);
                            card.MOSeq = mo.MOSeq;
                            // Added end

                            //将Action加入列表
                            actionCheckStatus.ActionList.Add(actionEventArgs);
                        }
                    }
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


        private void updateItem2Route(string itemCode, string routeCode, string userCode)
        {
            DBDateTime dbDateTime;

            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);


            this.DataProvider.CustomExecute(new SQLParamCondition("update tblitem2route set isref='1', MUSER=$MUSER, MDATE=$MDATE, MTIME=$MTIME where itemcode=$itemcode and routecode=$routecode and isref!='1'" + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                new SQLParameter[] {
									   new SQLParameter("MUSER", typeof(string), userCode),
									   new SQLParameter("MDATE", typeof(int), dbDateTime.DBDate),
									   new SQLParameter("MTIME", typeof(int), dbDateTime.DBTime),
									   new SQLParameter("itemcode", typeof(string), itemCode ),
									   new SQLParameter("routecode", typeof(string), routeCode )
								   }));
        }

        // Added by Icyer 2006/12/05
        private void TakeDownCarton(ActionEventArgs actionEventArgs)
        {
            bool bDo = false;
            ProductInfo product = actionEventArgs.ProductInfo;
            if (System.Configuration.ConfigurationSettings.AppSettings["TakeDownCartonReMORework"] == "1" &&
                product != null &&
                product.NowSimulation != null &&
                product.LastSimulation != null &&
                product.NowSimulation.MOCode != product.LastSimulation.MOCode &&
                product.LastSimulation.CartonCode != string.Empty)
            {
                MOFacade moFacade = new MOFacade(this.DataProvider);
                MO mo = (MO)moFacade.GetMO(product.NowSimulation.MOCode);
                if (mo == null)
                    return;
                BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = new SystemSettingFacade(this.DataProvider);
                Parameter param = (Parameter)sysFacade.GetParameter(mo.MOType, BenQGuru.eMES.Web.Helper.MOType.GroupType);
                if (param != null &&
                    param.ParameterValue == BenQGuru.eMES.Web.Helper.MOType.MOTYPE_REWORKMOTYPE)
                {
                    bDo = true;
                }
            }
            if (bDo == false)
                return;

            BenQGuru.eMES.Package.PackageFacade packageFacade = new BenQGuru.eMES.Package.PackageFacade(this.DataProvider);

            string strSql = "UPDATE tblSimulation SET CartonCode='' WHERE RCard='" + product.LastSimulation.LotCode + "' AND MOCode='" + product.LastSimulation.MOCode + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(strSql));

            strSql = "UPDATE tblSimulationReport SET CartonCode='' WHERE RCard='" + product.LastSimulation.LotCode + "' AND MOCode='" + product.LastSimulation.MOCode + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(strSql));

            packageFacade.SaveRemoveCarton2RCARDLog(product.LastSimulation.CartonCode, product.LastSimulation.LotCode, actionEventArgs.UserCode);

            packageFacade.SubtractCollected(product.LastSimulation.CartonCode);
            product.LastSimulation.CartonCode = string.Empty;
        }
        // Added end

        // Added by Icyer 2007/03/09
        // 自动检查是否归属工单
        // 参数中需要的资料：LotCode、ResourceCode
        // 返回信息：
        //		如果不需要归属工单，则返回空Messages
        //		如果需要归属工单，则返回归属工单操作的Messages
        //		如果需要归属工单，但是工单解析失败，则返回失败信息
        // 工单来源：在B/S的"资源生产工单设置"中设置
        public Messages AutoGoMO(ActionEventArgs actionEventArgs)
        {
            return AutoGoMO(actionEventArgs, null);
        }
        public Messages AutoGoMO(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages msg = new Messages();

            // 如果当前序列号是在制品，则不会做归属工单
            if (actionEventArgs.ProductInfo != null &&
                actionEventArgs.ProductInfo.LastSimulation != null &&
                actionEventArgs.ProductInfo.LastSimulation.IsComplete != "1")
            {
                return msg;
            }

            DBDateTime dbDateTime;
            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            string strResCode = actionEventArgs.ResourceCode;
            string strRCard = actionEventArgs.LotCode;
            // 根据资源检查是否需要归属工单
            Resource2MO res2mo = GetResource2MOByResource(strResCode, dbDateTime);
            if (res2mo == null)		// 如果没有设置，表示不用归属工单，直接返回
                return msg;
            // 检查序列号格式
            msg.AddMessages(CheckRCardFormatByResource2MO(res2mo, actionEventArgs.LotCode, actionEventArgs));
            if (msg.IsSuccess() == false)
                return msg;
            // 解析工单
            string strMOCode = BuildMOCodeByResource2MO(res2mo, actionEventArgs.LotCode, actionEventArgs, dbDateTime);
            // 执行归属工单的操作
            GoToMOActionEventArgs gomoArg = new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO,
                actionEventArgs.LotCode,
                actionEventArgs.UserCode,
                actionEventArgs.ResourceCode,
                actionEventArgs.ProductInfo,
                strMOCode);
            IAction gomoAction = new ActionFactory(this.DataProvider).CreateAction(ActionType.DataCollectAction_GoMO);
            try
            {
                if (actionCheckStatus != null)
                {
                    msg.AddMessages(((IActionWithStatus)gomoAction).Execute(gomoArg, actionCheckStatus));
                }
                else
                {
                    msg.AddMessages(gomoAction.Execute(gomoArg));
                }
            }
            catch (Exception e)
            {
                msg.Add(new Message(e));
            }
            if (msg.IsSuccess() == true)
            {
                msg.Add(new UserControl.Message(MessageType.Success, "$CS_GOMOSUCCESS"));

                if (actionCheckStatus == null || actionCheckStatus.NeedUpdateSimulation == true)
                {
                    ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
                    Messages msgProduct = onLine.GetIDInfo(actionEventArgs.LotCode);
                    actionEventArgs.ProductInfo = (ProductInfo)msgProduct.GetData().Values[0];
                }
                else
                {
                    actionEventArgs.ProductInfo.LastSimulation = new ExtendSimulation(gomoArg.ProductInfo.NowSimulation);
                    actionCheckStatus.ProductInfo.LastSimulation = actionEventArgs.ProductInfo.LastSimulation;
                }
            }

            return msg;
        }
        // 根据当前时间查询资源设置
        private Resource2MO GetResource2MOByResource(string resourceCode, DBDateTime dbDateTime)
        {
            string strNowTime = dbDateTime.DBDate.ToString() + dbDateTime.DBTime.ToString().PadLeft(6, '0');
            string strSql = "SELECT * FROM TBLRES2MO WHERE RESCODE='" + resourceCode + "' AND STARTDATE * 1000000 + STARTTIME<=" + strNowTime + " AND ENDDATE * 1000000 + ENDTIME>=" + strNowTime + " ORDER BY SEQ DESC ";
            object[] objsSet = this.DataProvider.CustomQuery(typeof(Resource2MO), new SQLCondition(strSql));
            if (objsSet == null || objsSet.Length == 0)
                return null;
            Resource2MO res2mo = (Resource2MO)objsSet[0];
            return res2mo;
        }
        // 根据工单设置，做序列号防呆检查
        private Messages CheckRCardFormatByResource2MO(Resource2MO res2mo, string runningCard, ActionEventArgs actionEventArgs)
        {
            Messages msg = new Messages();
            // 是否检查序列号防呆
            if (FormatHelper.StringToBoolean(res2mo.CheckRunningCardFormat) == true)
            {
                // 检查首字符串
                if (res2mo.RunningCardPrefix != "")
                {
                    if (runningCard.Length >= res2mo.RunningCardPrefix.Length &&
                        runningCard.StartsWith(res2mo.RunningCardPrefix) == true)
                    { }
                    else
                    {
                        msg.Add(new Message(MessageType.Error, "$CS_Before_Card_FLetter_NotCompare [$CS_ID_Prefix=" + res2mo.RunningCardPrefix + "]"));
                        return msg;
                    }
                }
                // 检查长度
                if (res2mo.RunningCardLength > 0)
                {
                    if (runningCard.Trim().Length != res2mo.RunningCardLength)
                    {
                        msg.Add(new Message(MessageType.Error, "$CS_Before_Card_Length_FLetter_NotCompare [$CS_ID_Length=" + res2mo.RunningCardLength.ToString() + "]"));
                        return msg;
                    }
                }
            }
            // 如果从序列号解析工单，则检查序列号的长度
            if (res2mo.MOGetType == Resource2MOGetType.GetFromRCard)
            {
                int iLen = Convert.ToInt32(res2mo.MOCodeRunningCardStartIndex + res2mo.MOCodeLength);
                if (runningCard.Length < iLen)
                {
                    msg.Add(new Message(MessageType.Error, "$Error_Resource2MO_RCard_Is_Short"));
                    return msg;
                }
            }
            return msg;
        }
        // 根据工单设置和序列号，生成工单
        private string BuildMOCodeByResource2MO(Resource2MO res2mo, string runningCard, ActionEventArgs actionEventArgs, DBDateTime dbDateTime)
        {
            string strMOCode = "";
            if (res2mo.MOGetType == Resource2MOGetType.Static)		// 固定工单
                strMOCode = res2mo.StaticMOCode;
            else if (res2mo.MOGetType == Resource2MOGetType.GetFromRCard)		// 从序列号解析工单
            {
                int iStart = Convert.ToInt32(res2mo.MOCodeRunningCardStartIndex - 1);
                int iLen = Convert.ToInt32(res2mo.MOCodeLength);
                if (runningCard.Length > iStart + iLen)
                {
                    strMOCode = runningCard.Substring(iStart, iLen);
                    // 为工单加上前缀、后缀
                    if (res2mo.MOCodePrefix != "")
                        strMOCode = res2mo.MOCodePrefix + strMOCode;
                    if (res2mo.MOCodePostfix != "")
                        strMOCode = strMOCode + res2mo.MOCodePostfix;
                }
            }
            return strMOCode;
        }

        // 从需要归属工单的序列号中，查询应该归属工单的工单信息
        // 用在第一站采NG时，查询不良代码列表
        public Messages GetItemCodeFromGoMoRCard(string resourceCode, string runningCard)
        {
            Messages msg = new Messages();
            DBDateTime dbDateTime;
            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            string strResCode = resourceCode;
            string strRCard = runningCard;
            // 根据资源检查是否需要归属工单
            Resource2MO res2mo = GetResource2MOByResource(strResCode, dbDateTime);
            if (res2mo == null)		// 如果没有设置，表示不用归属工单，直接返回
                return msg;
            // 检查序列号格式
            msg.AddMessages(CheckRCardFormatByResource2MO(res2mo, runningCard, null));
            if (msg.IsSuccess() == false)
                return msg;
            // 解析工单
            string strMOCode = BuildMOCodeByResource2MO(res2mo, runningCard, null, dbDateTime);
            // 查询工单信息
            MOFacade moFacade = new MOFacade(this.DataProvider);
            MO mo = (MO)moFacade.GetMO(strMOCode);
            if (mo == null)
            {
                msg.Add(new Message(MessageType.Error, "$CS_MO_Not_Exit [" + strMOCode + "]"));
                return msg;
            }
            msg.Add(new Message(MessageType.Data, "", new object[] { mo }));
            return msg;
        }
        // Added end

    }
}
