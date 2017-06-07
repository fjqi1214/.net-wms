using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;
using Infragistics.Win.UltraWinGrid;

using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.ATE;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Burn;

namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// 老化采集(包含老化进和老化出)
    /// </summary>
    public partial class FBurn : BaseForm
    {
        private const string ng_collect = ActionType.DataCollectAction_NG;
        private const string good_collect = ActionType.DataCollectAction_GOOD;
        private ProductInfo product;
        private double iNG = 0;
        private string _FunctionName = string.Empty;
        private string opControl = "0000000000000000000000";
        private const char isSelected = '1';
        private DataTable _DataTableLoadedBurn = new DataTable();
        private Resource Resource;
        private bool isForce = false;
        private string userCode;

        public FBurn()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            UserControl.UIStyleBuilder.FormUI(this);
            product = new ProductInfo();

            txtMem.AutoChange();

            this.InitializeGrid();
            this.BindGrid();
        }

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private void InitializeGrid()
        {
            _DataTableLoadedBurn.Columns.Add("MoCode", typeof(string));
            _DataTableLoadedBurn.Columns.Add("ItemCode", typeof(string));
            _DataTableLoadedBurn.Columns.Add("RunningCard", typeof(string));
            _DataTableLoadedBurn.Columns.Add("BurnInDate", typeof(string));
            _DataTableLoadedBurn.Columns.Add("BurnInTime", typeof(string));
            _DataTableLoadedBurn.Columns.Add("ForecastOutDate", typeof(string));
            _DataTableLoadedBurn.Columns.Add("ForecastOutTime", typeof(string));
           
            this.gridInfo.DataSource = this._DataTableLoadedBurn;

            gridInfo.DisplayLayout.Bands[0].Columns["MoCode"].CellActivation = Activation.ActivateOnly;
            gridInfo.DisplayLayout.Bands[0].Columns["ItemCode"].CellActivation = Activation.ActivateOnly;
            gridInfo.DisplayLayout.Bands[0].Columns["RunningCard"].CellActivation = Activation.ActivateOnly;
            gridInfo.DisplayLayout.Bands[0].Columns["BurnInDate"].CellActivation = Activation.ActivateOnly;
            gridInfo.DisplayLayout.Bands[0].Columns["BurnInTime"].CellActivation = Activation.ActivateOnly;
            gridInfo.DisplayLayout.Bands[0].Columns["ForecastOutDate"].CellActivation = Activation.ActivateOnly;
            gridInfo.DisplayLayout.Bands[0].Columns["ForecastOutTime"].CellActivation = Activation.ActivateOnly;

            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            Resource = (Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
        }

        private void BindGrid()
        {
            BurnFacade burnFacade = new BurnFacade(this.DataProvider);
            object[] objs = burnFacade.GetBurnBySscode(Resource.StepSequenceCode);
            _DataTableLoadedBurn.Clear();

            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    BurnWip burnWip = objs[i] as BurnWip;
                    _DataTableLoadedBurn.Rows.Add(new object[] {
                                                            burnWip.MOCode,
                                                            burnWip.ItemCode,                                                            
                                                            burnWip.RunningCard,
                                                            FormatHelper.ToDateString(burnWip.BurnInDate, "-"),
                                                            FormatHelper.ToTimeString(burnWip.BurnInTime, ":"),
                                                            FormatHelper.ToDateString(burnWip.ForecastOutDate, "-"),
                                                            FormatHelper.ToTimeString(burnWip.ForecastOutTime, ":")
                                                             });
                }
            }
        }

        /// <summary>
        /// 获得产品信息
        /// Laws Lu,2005/08/02,修改
        /// </summary>
        /// <returns></returns>
        private Messages GetProduct()
        {
            Messages productmessages = new Messages();
            ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);

            //Add By Bernard @ 2010-11-03
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);
            //end

            productmessages.AddMessages(dataCollect.GetIDInfo(sourceRCard.Trim().ToUpper()));
            if (productmessages.IsSuccess())
            {
                product = (ProductInfo)productmessages.GetData().Values[0];
            }
            else
            {
                product = new ProductInfo();
            }

            dataCollect = null;
            return productmessages;
        }

        /// <summary>
        /// 根据产品信息，决定部分控件的状态
        /// </summary>
        /// <returns></returns>
        private Messages CheckProduct()
        {
            Messages messages = new Messages();
            try
            {
                messages.AddMessages(GetProduct());
            }
            catch (Exception e)
            {
                messages.Add(new UserControl.Message(e));

            }
            return messages;
        }

        private Hashtable listActionCheckStatus = new Hashtable();

        public void txtRunningCard_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                isForce = false;
                userCode = ApplicationService.Current().UserCode;
                this.CheckAndRunOnWip();
            }
        }

        private void CheckAndRunOnWip()
        {
            if (txtRunningCard.Value.Trim() == string.Empty)
            {
                if (!this.txtGOMO.Checked)
                {
                    txtMO.Value = String.Empty;
                    txtItem.Value = String.Empty;
                    labelItemDescription.Text = "";
                }

                ApplicationRun.GetInfoForm().AddEx("$CS_Please_Input_RunningCard");

                //将焦点移到产品序列号输入框
                txtRunningCard.TextFocus(false, true);
                return;
            }
            else
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                BurnFacade burnFacade = new BurnFacade(this.DataProvider);
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);

                if (this.txtGOMO.Checked && this.txtGOMO.Value.Trim().Length == 0)
                {
                    ApplicationRun.GetInfoForm().AddEx("$CS_CMPleaseInputMO");
                    this.txtGOMO.Checked = true;
                    this.txtGOMO.TextFocus(false, true);
                    return;
                }

                if (this.txtGOMO.Checked && this.txtGOMO.InnerTextBox.Enabled)
                {
                    ApplicationRun.GetInfoForm().AddEx("$CS_PleasePressEnterOnGOMO");
                    this.txtGOMO.Checked = true;
                    this.txtGOMO.TextFocus(false, true);
                    return;
                }
                // End Added
                //Add by sandy on 20140528
                if (rdoBurnIn.Checked && rdoNG.Checked)
                {
                    UserControl.Message message = new UserControl.Message(MessageType.Error, "$CS_BurnIn_Can_Not_NG");
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, message, true);
                    txtRunningCard.TextFocus(false, true);
                    return;
                }
                //end
                if (txtRunningCard.Value.Trim().ToUpper() == ng_collect)
                {
                    rdoNG.Checked = true;
                    rdoBurnOut.Checked = true;
                    txtRunningCard.TextFocus(false, true);
                    return;
                }

                //Jarvis 20130125 支持GOOD指令
                if (txtRunningCard.Value.Trim().ToUpper() == good_collect)
                {
                    rdoGood.Checked = true;
                    txtRunningCard.TextFocus(false, true);
                    return;
                }
                //Add by sandy on 20140528
                if (txtRunningCard.Value.Trim().ToUpper() == ActionType.DataCollectAction_BurnIn)
                {
                    rdoGood.Checked = true;
                    rdoBurnIn.Checked = true;
                    txtRunningCard.TextFocus(false, true);
                    return;
                }

                if (txtRunningCard.Value.Trim().ToUpper() == ActionType.DataCollectAction_BurnOutGood)
                {
                    rdoGood.Checked = true;
                    rdoBurnOut.Checked = true;
                    txtRunningCard.TextFocus(false, true);
                    return;
                }

                if (txtRunningCard.Value.Trim().ToUpper() == ActionType.DataCollectAction_BurnOutNG)
                {
                    rdoNG.Checked = true;
                    rdoBurnOut.Checked = true;
                    txtRunningCard.TextFocus(false, true);
                    return;
                }

                //End

                //add by hiro.chen 08/11/18 TocheckIsDown
                Messages msg = new Messages();
                msg.AddMessages(dataCollectFacade.CheckISDown(sourceCard.Trim().ToUpper()));
                if (!msg.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ":" + this.txtRunningCard.Value, msg, true);
                    txtRunningCard.TextFocus(false, true);
                    return;
                }
                //end 

                //报废不能返工 
                msg.AddMessages(dataCollectFacade.CheckReworkRcardIsScarp(sourceCard.Trim().ToUpper(), ApplicationService.Current().ResourceCode));
                if (!msg.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ":" + this.txtRunningCard.Value, msg, true);
                    txtRunningCard.TextFocus(false, true);
                    return;
                }
                //end

                //Laws Lu,2005/10/19,新增	缓解性能问题
                //Laws Lu,2006/12/25 修改	减少Open/Close的次数
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

                //Laws Lu,2005/08/16,修改	把msg换成globeMSG
                globeMSG = CheckProduct();

                // Added by Icyer 2005/10/28
                if (Resource == null)
                {
                    BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                    Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
                }
                actionCheckStatus = new ActionCheckStatus();
                actionCheckStatus.ProductInfo = product;

                if (actionCheckStatus.ProductInfo != null)
                {
                    actionCheckStatus.ProductInfo.Resource = Resource;
                }

                string strMoCode = String.Empty;
                if (product != null && product.LastSimulation != null)
                {
                    strMoCode = product.LastSimulation.MOCode;
                }

                if (strMoCode != String.Empty)
                {
                    if (listActionCheckStatus.ContainsKey(strMoCode))
                    {
                        actionCheckStatus = (ActionCheckStatus)listActionCheckStatus[strMoCode];
                        actionCheckStatus.ProductInfo = product;
                        actionCheckStatus.ActionList = new ArrayList();
                    }
                    else
                    {
                        listActionCheckStatus.Add(strMoCode, actionCheckStatus);
                    }
                }
                //Amoi,Laws Lu,2005/08/02,修改

                if (txtGOMO.Checked == true)
                {
                    globeMSG.AddMessages(RunGOMO(actionCheckStatus));

                    if (!globeMSG.IsSuccess())
                    {
                        listActionCheckStatus.Clear();
                    }
                }
                else
                {
                    if (product != null && product.LastSimulation != null)
                    {
                        this.txtMO.Value = product.LastSimulation.MOCode;
                        this.txtItem.Value = product.LastSimulation.ItemCode;
                        this.labelItemDescription.Text = this.GetItemDescription(product.LastSimulation.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                    }
                    else
                    {
                        this.txtMO.Value = "";
                        this.txtItem.Value = "";
                        this.labelItemDescription.Text = "";
                    }
                }
                //EndAmoi

                //老化进和老化出工序不能同时勾选
                BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object objOP = baseModelFacade.GetOperationByResource(ApplicationService.Current().ResourceCode);
                if (product != null && product.LastSimulation != null && objOP != null)
                {
                    object objOp = itemFacade.GetItemRoute2Operation(this.txtItem.Value, product.LastSimulation.RouteCode,
                                                                     ((Operation2Resource)objOP).OPCode);
                    if (objOp != null)
                    {
                        opControl = ((ItemRoute2OP)objOp).OPControl;
                        if (opControl[(int)OperationList.BurnIn] == isSelected && opControl[(int)OperationList.BurnOut] == isSelected)
                            globeMSG.Add(new UserControl.Message(MessageType.Error, "$CS_BurnInOut_Can_Not_Choose_Together"));
                    }
                }
                //老化未达到预计时间且不是强制老化时不可做老化出良品采集
                if (!isForce && globeMSG.IsSuccess() && rdoBurnOut.Checked && rdoGood.Checked && product.LastSimulation != null)
                {
                    object objBurnWip = burnFacade.GetBurnWip(sourceCard.Trim().ToUpper(), product.LastSimulation.MOCode);
                    BurnWip burnWip = objBurnWip as BurnWip;
                    if (objBurnWip != null && burnWip.Status == BurnType.BurnIn)
                    {
                        DateTime dtForecast = FormatHelper.ToDateTime(burnWip.ForecastOutDate, burnWip.ForecastOutTime);
                        if (dbDateTime.DateTime < dtForecast)
                            globeMSG.Add(new UserControl.Message(MessageType.Error, "$CS_Burn_Time_Not_Enough"));
                    }
                }

                if (globeMSG.IsSuccess())
                    this.RunOnWip();
            }
            //刷新grid
            this.BindGrid();
            //Laws Lu,2005/10/19,新增	缓解性能问题
            //Laws Lu,2006/12/25 修改	减少Open/Close的次数
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;

            //将焦点移到产品序列号输入框
            ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, globeMSG, true);

            //Application.DoEvents();
            txtRunningCard.TextFocus(false, true);

            globeMSG.ClearMessages();
        }

        private string GetItemDescription(string itemCode, int orgID)
        {
            ItemFacade facade = new ItemFacade(this.DataProvider);
            Item item = facade.GetItem(itemCode, orgID) as Item;
            if (item == null)
            {
                return itemCode;
            }
            else
            {
                return (item as Item).ItemDescription;
            }
        }

        private void RunOnWip()
        {
            Messages messages = new Messages();
            ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

            string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);

            DataProvider.BeginTransaction();
            try
            {
                #region Laws Lu,保存按钮的主逻辑
                string burnActionType = string.Empty;

                if (rdoBurnIn.Checked)
                {
                    burnActionType = ActionType.DataCollectAction_BurnIn;
                }
                else
                {
                    if (rdoGood.Checked)
                    {
                        burnActionType = ActionType.DataCollectAction_BurnOutGood;
                    }
                    else
                    {
                        burnActionType = ActionType.DataCollectAction_BurnOutNG;
                    }
                }

                if (txtGOMO.Checked)
                {
                    if (rdoGood.Checked)
                    {
                        messages = GetProduct();

                        if (messages.IsSuccess())
                        {
                            messages.AddMessages(RunGood(actionCheckStatus, burnActionType));
                        }
                    }
                    else if (rdoNG.Checked)
                    {
                        messages = GetProduct();

                        if (messages.IsSuccess())
                        {
                            messages.AddMessages(RunNG(actionCheckStatus, burnActionType));
                        }
                    }
                }
                else
                {
                    if (rdoGood.Checked)
                    {
                        if (messages.IsSuccess())
                        {
                            messages.AddMessages(RunGood(actionCheckStatus, burnActionType));
                        }
                    }
                    else if (rdoNG.Checked)
                    {
                        if (messages.IsSuccess())
                        {
                            messages.AddMessages(RunNG(actionCheckStatus, burnActionType));
                        }
                    }
                }

                if (messages.IsSuccess())
                    this.SaveBurnWip();
                #endregion

                if (messages.IsSuccess())
                {
                    DataProvider.CommitTransaction();
                }
                else
                {
                    DataProvider.RollbackTransaction();
                }
            }
            catch (Exception exp)
            {
                DataProvider.RollbackTransaction();
                messages.Add(new UserControl.Message(exp));
                //txtRunningCard.Value = String.Empty;

                globeMSG.AddMessages(messages);
            }
            finally
            {
                globeMSG.AddMessages(messages);

                Messages newMsg = new Messages();
                string exception = globeMSG.OutPut();
                exception = exception.Replace(sourceRCard, this.txtRunningCard.Value);
                string type = string.Empty;
                if (globeMSG.functionList().IndexOf(":") > 0)
                {
                    type = globeMSG.functionList().Substring(0, globeMSG.functionList().IndexOf(":"));
                }
                newMsg.Add(new UserControl.Message(MessageType.Error, exception));
                if (type == MessageType.Error.ToString())
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, newMsg, true);
                }
                else
                {
                    ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, globeMSG, true);
                }

                globeMSG.ClearMessages();

                //重新设置页面控件状态
                if (messages.IsSuccess())
                {
                    ClearFormMessages();
                }

                //Laws Lu,2005/10/19,新增	缓解性能问题
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }

            txtRunningCard.TextFocus(false, true);
        }
        //保存老化信息
        private void SaveBurnWip()
        {
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this.DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            BurnFacade burnFacade = new BurnFacade(this.DataProvider);
            //获取时段代码
            StepSequence _StepSequence = (StepSequence)baseModelFacade.GetStepSequence(Resource.StepSequenceCode);
            TimePeriod tp = (TimePeriod)shiftModelFacade.GetTimePeriod(Resource.ShiftTypeCode, dbDateTime.DBTime);
            
            BurnWip burnWip = new BurnWip();
            string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);
            burnWip.RunningCard = sourceRCard;
            burnWip.MOCode = product.LastSimulation.MOCode;
            burnWip.ItemCode = this.txtItem.Value;
            burnWip.SsCode = Resource.StepSequenceCode;
            burnWip.ResCode = Resource.ResourceCode;
            if (tp != null)
            {
                burnWip.TpCode = tp.TimePeriodCode;
                burnWip.ShiftDay = shiftModelFacade.GetShiftDay(tp, dbDateTime.DateTime);
            }

            burnWip.ProductStatus = ProductStatus.GOOD;
            if (rdoBurnIn.Checked)
            {
                burnWip.Status = BurnType.BurnIn;
                burnWip.BurnInDate = dbDateTime.DBDate;
                burnWip.BurnInTime = dbDateTime.DBTime;

                object objItem = itemFacade.GetItem(this.txtItem.Value, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (objItem != null && ((Item)objItem).BurnUseMinutes.ToString() != "")
                {
                    burnWip.ForecastOutDate = FormatHelper.TODateInt(dbDateTime.DateTime.AddMinutes(((Item)objItem).BurnUseMinutes));
                    burnWip.ForecastOutTime = FormatHelper.TOTimeInt(dbDateTime.DateTime.AddMinutes(((Item)objItem).BurnUseMinutes));
                }
            }
            else
            {
                burnWip.Status = BurnType.BurnOut;
                burnWip.BurnOutDate = dbDateTime.DBDate;
                burnWip.BurnOutTime = dbDateTime.DBTime;
                
                if (rdoNG.Checked)
                    burnWip.ProductStatus = ProductStatus.NG;
            }

            burnWip.MaintainUser = userCode;
            burnWip.MaintainDate = dbDateTime.DBDate;
            burnWip.MaintainTime = dbDateTime.DBTime;

            object objBurn = burnFacade.GetBurnWip(burnWip.RunningCard, burnWip.MOCode);
            if (objBurn == null)
            {
                burnFacade.AddBurnWip(burnWip);
            }
            else
            {
                if (((BurnWip)objBurn).Status == BurnType.BurnOut && rdoBurnOut.Checked)  //返工或维修回流时，若没有走burn in工序，则预计完成时间更新为系统时间
                {
                    burnWip.BurnInDate = dbDateTime.DBDate;
                    burnWip.BurnInTime = dbDateTime.DBTime;
                    burnWip.ForecastOutDate = dbDateTime.DBDate;
                    burnWip.ForecastOutTime = dbDateTime.DBTime;
                }
                else
                {
                    burnWip.BurnInDate = ((BurnWip)objBurn).BurnInDate;
                    burnWip.BurnInTime = ((BurnWip)objBurn).BurnInTime;
                    burnWip.ForecastOutDate = ((BurnWip)objBurn).ForecastOutDate;
                    burnWip.ForecastOutTime = ((BurnWip)objBurn).ForecastOutTime;
                }

                burnFacade.UpdateBurnWip(burnWip);
            }
        }

        /// <summary>
        /// GOOD指令采集
        /// </summary>
        /// <returns></returns>
        private Messages RunGood(ActionCheckStatus actionCheckStatus,string actionType)
        {
            Messages messages = new Messages();
            //Add By Bernard @ 2010-11-03
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);
            //end

            IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(actionType);

            messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(new ActionEventArgs(actionType, sourceRCard.Trim(),
                userCode,
                ApplicationService.Current().ResourceCode, product), actionCheckStatus));
            if (messages.IsSuccess())
            {
                messages.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_GOODSUCCESS,$CS_Param_ID: {0}", txtRunningCard.Value.Trim())));
            }
            return messages;
        }

        /// <summary>
        /// NG指令采集
        /// </summary>
        /// <returns></returns>
        private Messages RunNG(ActionCheckStatus actionCheckStatus, string actionType)
        {
            Messages messages = new Messages();

            //Add By Bernard @ 2010-11-03
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(this.txtRunningCard.Value.Trim().ToUpper(), string.Empty);
            //end

            object[] ErrorCodes = GetSelectedErrorCodes();//取不良代码组＋不良代码
            if (ErrorCodes == null || ErrorCodes.Length == 0)
                messages.Add(new UserControl.Message(MessageType.Error, "$CS_Burn_ErrorCode_Not_Maintain"));

            IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(actionType);
            if (messages.IsSuccess())
            {
                messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(
                    new TSActionEventArgs(actionType,
                    sourceRCard.Trim(),
                    userCode,
                    ApplicationService.Current().ResourceCode,
                    product,
                    ErrorCodes,
                    null,
                    txtMem.Value), actionCheckStatus));
            }

            if (messages.IsSuccess())
            {
                iNG = iNG + 1;

                messages.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_NGSUCCESS,$CS_Param_ID: {0}", txtRunningCard.Value.Trim())));
            }
            return messages;
        }

        private ActionCheckStatus actionCheckStatus = new ActionCheckStatus();
        

        /// <summary>
        /// 工单归属采集
        /// </summary>
        /// <returns></returns>
        private Messages RunGOMO(ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();

            //Add By Bernard @ 2010-11-03
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            string sourceRCard = dataCollectFacade.GetSourceCard(txtRunningCard.Value.Trim().ToUpper(), string.Empty);
            //end

            //Laws Lu,新建数据采集Action
            ActionOnLineHelper onLine = new ActionOnLineHelper(this.DataProvider);
            //IAction dataCollectModule = (new ActionFactory(this.DataProvider)).CreateAction(ActionType.DataCollectAction_GoMO);

            //Laws Lu,2005/09/14,新增	工单不能为空
            if (txtGOMO.Checked == true && txtGOMO.Value.Trim() != String.Empty)
            {

                actionCheckStatus.ProductInfo = product;

                //产品序列号长度检查
                if (bRCardLenULE.Checked && bRCardLenULE.Value.Trim() != string.Empty)
                {

                    int len = 0;
                    try
                    {
                        len = int.Parse(bRCardLenULE.Value.Trim());
                        if (txtRunningCard.Value.Trim().Length != len)
                        {
                            messages.Add(new UserControl.Message(MessageType.Error, "$CS_Before_Card_Length_FLetter_NotCompare $CS_Param_ID:" + txtRunningCard.Value.Trim()));
                        }
                    }
                    catch
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CS_Before_Card_Length_FLetter_NotCompare $CS_Param_ID:" + txtRunningCard.Value.Trim()));
                    }
                }

                //产品序列号首字符串检查
                if (bRCardLetterULE.Checked && bRCardLetterULE.Value.Trim() != string.Empty)
                {
                    int index = -1;
                    if (bRCardLetterULE.Value.Trim().Length <= txtRunningCard.Value.Trim().Length)
                    {
                        index = txtRunningCard.Value.IndexOf(bRCardLetterULE.Value.Trim());
                    }
                    if (index == -1)
                    {
                        messages.Add(new UserControl.Message(MessageType.Error, "$CS_Before_Card_FLetter_NotCompare $CS_Param_ID:" + txtRunningCard.Value.Trim()));
                    }
                }

                //add by hiro 08/11/05 检查序列号内容为字母,数字和空格            
                string pattern = @"^([A-Za-z0-9]+[ ]*)*[A-Za-z0-9]+$";
                string rcard = this.txtRunningCard.Value.Trim().ToString();
                Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = rex.Match(rcard);
                ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                object obj = itemfacade.GetItem2SNCheck(this.txtItem.Value.Trim().ToString(), ItemCheckType.ItemCheckType_SERIAL);
                if (obj != null)
                {
                    if (((Item2SNCheck)obj).SNContentCheck == SNContentCheckStatus.SNContentCheckStatus_Need)
                    {
                        if (!match.Success)
                        {
                            messages.Add(new UserControl.Message(MessageType.Error, "$CS_SNContent_CheckWrong $CS_Param_RunSeq:" + this.txtRunningCard.Value.Trim().ToString()));
                        }
                    }
                }
                //end by hiro

                //Laws Lu,参数定义
                GoToMOActionEventArgs args = new GoToMOActionEventArgs(ActionType.DataCollectAction_GoMO,
                    sourceRCard.Trim(),
                    ApplicationService.Current().UserCode,
                    ApplicationService.Current().ResourceCode, product, txtGOMO.Value);

                //actionCheckStatus.NeedUpdateSimulation = false;

                //Laws Lu,执行工单采集并收集返回信息
                if (messages.IsSuccess())
                {
                    messages.AddMessages(onLine.Action(args, actionCheckStatus));
                }
            }

            if (messages.IsSuccess())
            {
                messages.Add(new UserControl.Message(MessageType.Success, "$CS_GOMOSUCCESS $CS_Param_ID:" + this.txtRunningCard.Value.Trim().ToString()));
                txtRunningCard.TextFocus(false, true);
            }

            return messages;
        }

        /// <summary>
        /// 保存成功后清除窗体数据并初始化控件状态
        /// Amoi,Laws Lu,2005/08/02
        /// </summary>
        private void ClearFormMessages()
        {
            txtMem.Value = string.Empty;

            InitialRunningCard();
        }

        /// <summary>
        /// 初始化RunningCard的状态
        /// Amoi,Laws Lu,2005/08/02,新增
        /// </summary>
        private void InitialRunningCard()
        {
            txtRunningCard.Value = String.Empty;
            txtRunningCard.TextFocus(false, true);
        }

        private void FBurn_Load(object sender, System.EventArgs e)
        {
            this._FunctionName = this.Text;

            InitialRunningCard();
        }

        private object[] GetSelectedErrorCodes()
        {
            TSModelFacade tsModelFacade = new TSModelFacade(this.DataProvider);
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            object[] objBurnNG = systemSettingFacade.GetParametersByParameterGroup("BURNNG");
            if (objBurnNG == null || objBurnNG.Length == 0)
                return null;
            object[] ecg2ec = tsModelFacade.GetECG2ECByECode(((Parameter)objBurnNG[0]).ParameterCode);//获取不良代码组
            return ecg2ec;
        }

        private void FBurn_Activated(object sender, System.EventArgs e)
        {
            txtRunningCard.TextFocus(false, true);
        }

        private void rdoGood_CheckedChanged(object sender, System.EventArgs e)
        {
            txtRunningCard.TextFocus(false, true);
        }

        private void rdoNG_CheckedChanged(object sender, System.EventArgs e)
        {
            txtRunningCard.TextFocus(false, true);
        }

        private void rdoBurnIn_CheckedChanged(object sender, EventArgs e)
        {
            rdoGood.Checked = true;
            btnBurnForcePass.Enabled = false;
            txtRunningCard.TextFocus(false, true);
        }

        private void rdoBurnOut_CheckedChanged(object sender, EventArgs e)
        {
            btnBurnForcePass.Enabled = true;
            txtRunningCard.TextFocus(false, true);
        }

        private void txtGOMO_CheckBoxCheckedChanged(object sender, System.EventArgs e)
        {
            if (txtGOMO.Checked == false)
            {
                this.bRCardLenULE.Value = String.Empty;
                this.bRCardLetterULE.Value = String.Empty;
                this.bRCardLetterULE.Checked = false;
                this.bRCardLenULE.Checked = false;

                this.bRCardLenULE.Enabled = false;
                this.bRCardLetterULE.Enabled = false;
            }
            if (txtGOMO.Checked == true)
            {
                this.bRCardLenULE.Value = String.Empty;
                this.bRCardLetterULE.Value = String.Empty;
                this.bRCardLetterULE.Checked = false;
                this.bRCardLenULE.Checked = false;

                this.bRCardLenULE.Enabled = true;
                this.bRCardLetterULE.Enabled = true;
            }

            this.txtMO.Value = "";
            this.txtItem.Value = "";
            this.labelItemDescription.Text = "";
        }

        private void txtGOMO_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                // Added By Hi1/Venus.Feng on 20080822 for Hisense Version : Add loading item and item description
                if (this.txtGOMO.Value.Trim().Length == 0)
                {
                    ApplicationRun.GetInfoForm().AddEx("$CS_CMPleaseInputMO");
                    this.txtMO.Value = "";
                    this.txtItem.Value = "";
                    this.labelItemDescription.Text = "";
                    this.bRCardLetterULE.Value = "";
                    this.bRCardLetterULE.Checked = false;
                    this.txtGOMO.Checked = true;
                    this.txtGOMO.TextFocus(false, true);
                    return;
                }

                string moCode = this.txtGOMO.Value.Trim().ToUpper();
                MOFacade mofacade = new MOFacade(this.DataProvider);

                object moObj = mofacade.GetMO(moCode);
                if (moObj == null)
                {
                    // mo not exist
                    ApplicationRun.GetInfoForm().AddEx("$CS_MO_NOT_EXIST");
                    this.txtMO.Value = "";
                    this.txtItem.Value = "";
                    this.labelItemDescription.Text = "";
                    this.bRCardLetterULE.Value = "";
                    this.bRCardLetterULE.Checked = false;
                    this.txtGOMO.Checked = true;
                    this.txtGOMO.TextFocus(false, true);
                    return;
                }

                MO mo = moObj as MO;
                this.txtMO.Value = mo.MOCode;

                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object itemObject = itemFacade.GetItem(mo.ItemCode, mo.OrganizationID);
                if (itemObject == null)
                {
                    // Item not exist
                    ApplicationRun.GetInfoForm().AddEx("$Error_ItemCode_NotExist $Domain_ItemCode:" + mo.ItemCode);
                    this.txtMO.Value = "";
                    this.txtItem.Value = "";
                    this.labelItemDescription.Text = "";
                    this.bRCardLetterULE.Value = "";
                    this.bRCardLetterULE.Checked = false;
                    this.txtGOMO.Checked = true;
                    this.txtGOMO.TextFocus(false, true);
                    return;
                }

                Item item = itemObject as Item;
                object item2sncheck = itemFacade.GetItem2SNCheck(item.ItemCode, ItemCheckType.ItemCheckType_SERIAL);
                if (item2sncheck == null)
                {
                    // Item2SNCheck not exist
                    ApplicationRun.GetInfoForm().AddEx("$Error_NoItemSNCheckInfo $Domain_ItemCode:" + mo.ItemCode);
                    this.txtMO.Value = "";
                    this.txtItem.Value = "";
                    this.labelItemDescription.Text = "";
                    this.bRCardLetterULE.Value = "";
                    this.bRCardLetterULE.Checked = false;
                    this.txtGOMO.Checked = true;
                    this.txtGOMO.TextFocus(false, true);
                    return;
                }

                Item2SNCheck item2SNCheck = item2sncheck as Item2SNCheck;
                this.txtItem.Value = item.ItemCode;
                this.labelItemDescription.Text = item.ItemDescription;

                SystemSettingFacade ssf = new SystemSettingFacade(this.DataProvider);

                object para = ssf.GetParameter("PRODUCTCODECONTROLSTATUS", "PRODUCTCODECONTROLSTATUS");

                if (item2SNCheck.SNPrefix.Length != 0)
                {
                    this.bRCardLetterULE.Checked = true;
                    this.bRCardLetterULE.Value = item2SNCheck.SNPrefix;
                    if (para != null)
                    {
                        if (string.Compare(((Parameter)para).ParameterAlias, "1", true) == 0)
                        {
                            this.bRCardLetterULE.Enabled = false;
                        }
                        else
                        {
                            this.bRCardLetterULE.Enabled = true;
                        }
                    }
                    else
                    {
                        this.bRCardLetterULE.Enabled = true;
                    }
                }
                else
                {
                    this.bRCardLetterULE.Enabled = true;
                }

                if (item2SNCheck.SNLength != 0)
                {
                    this.bRCardLenULE.Checked = true;
                    this.bRCardLenULE.Value = item2SNCheck.SNLength.ToString();
                    if (para != null)
                    {
                        if (string.Compare(((Parameter)para).ParameterAlias, "1", true) == 0)
                        {
                            this.bRCardLenULE.Enabled = false;
                        }
                        else
                        {
                            this.bRCardLenULE.Enabled = true;
                        }
                    }
                    else
                    {
                        this.bRCardLenULE.Enabled = true;
                    }
                }
                else
                {
                    this.bRCardLenULE.Enabled = true;
                }

                // end added
                this.txtGOMO.InnerTextBox.Enabled = false;
                txtRunningCard.TextFocus(false, true);
            }
        }

        private void FBurn_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_domainDataProvider != null)
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_domainDataProvider).PersistBroker.CloseConnection();
            }
        }

        private void btnBurnForcePass_Click(object sender, EventArgs e)
        {
            if (!rdoBurnOut.Checked || !rdoGood.Checked)
            {
                UserControl.Message message = new UserControl.Message(MessageType.Error, "$CS_BurnOut_Force_Onely");
                ApplicationRun.GetInfoForm().AddEx(this._FunctionName, this.txtRunningCard.Caption + ": " + this.txtRunningCard.Value, message, true);
                return;
            }
            FAuthentication fAuthentication = new FAuthentication("BURNRIGHTUSER");
            fAuthentication.Owner = this;
            fAuthentication.StartPosition = FormStartPosition.CenterScreen;
            fAuthentication.ShowDialog();
            fAuthentication = null;

            if (FAuthentication.m_isRightUser == true)
            {
                isForce = true;
                userCode = FAuthentication.m_UserCode;
                this.CheckAndRunOnWip();
            }
        }

    }
}