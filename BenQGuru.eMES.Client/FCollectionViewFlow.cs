using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Domain.DataCollect;
using UserControl;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DrawFlow.Controls;
using System.Collections;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;


namespace BenQGuru.eMES.Client
{
    public partial class FCollectionViewFlow : BaseForm
    {
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private DataCollectFacade m_DataCollectFacade;
        private DataTable _DataTableLoadedPart = new DataTable();
        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FCollectionViewFlow()
        {
            InitializeComponent();
            InitializeDatatable();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Dispose();
        }

        private void txtRcard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Messages msg = new Messages();
                if (!string.IsNullOrEmpty(FormatHelper.CleanString(this.txtRcard.Text.Trim().ToUpper())))
                {
                    initDrowdownList();
                    if (initFlowPicBox())
                    {
                        initGirdData();
                    }
                }
                else
                {
                    clear();
                    this.drownListMoCode.Clear();
                    this.drownListRouteCode.Clear();
                    msg.Add(new UserControl.Message(MessageType.Error, "$CS_RCARD_EMPTY"));
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }
                txtRcard.SelectAll();
                txtRcard.Focus();
            }
        }

        private void initDrowdownList()
        {
            Messages msg = new Messages();
            this.drownListMoCode.Clear();
            this.drownListRouteCode.Clear();
            if (m_DataCollectFacade == null) { m_DataCollectFacade = new DataCollectFacade(this.DataProvider); }
            try
            {
                //modified by klaus 不用了
                //add By Leo@20130128 
                //获取原始Rcard
                //string sourceRcard = m_DataCollectFacade.GetSourceCard(FormatHelper.CleanString(txtRcard.Text.ToUpper()), string.Empty);
                //end

                //object[] objSims = m_DataCollectFacade.QueryOnwipRoutesByRcard(FormatHelper.CleanString(sourceRcard));


                //add by klaus 20130131
                //获取当前source的所有的rcard的信息
                object[] objSims = null;
                string rcard = FormatHelper.CleanString(txtRcard.Text.ToUpper());

                string scard = m_DataCollectFacade.QueryOnWIPCardTransferByRcard(rcard);
                //scard != rcard表示做过序列号转换
                if (scard != rcard)
                {
                    ArrayList arrRcard = m_DataCollectFacade.QueryOnWIPCardTransferByScard(scard);
                    scard = "";
                    for (int i = 0; i < arrRcard.Count; i++)
                    {
                        scard += "'" + arrRcard[i].ToString() + "',";
                    }
                    scard = scard.Trim(',');
                }
                else
                {
                    scard = "'" + scard + "'";
                }
                objSims = m_DataCollectFacade.QueryOnwipRoutesInRcard(scard);
                //end;
                
                
                if (objSims == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_CS_ID_Not_Exist"));
                    txtRcard.SelectAll();
                    txtRcard.Focus();
                    ApplicationRun.GetInfoForm().Add(msg);
                    return;
                }

                //加载该产品所经过的全部途程和工单，默认显示当前所在途程的工序信息：
                //查询当前途程工序捞TBLITEMROUTE2OP表 QueryItemOpFlow(当前itemCode,当前途程Code);
                //修改为查TBLITEMROUTE2OP，然后与onwip表进行匹配            
                ArrayList arrMO = new ArrayList();
                for (int i = 0; i < objSims.Length; i++)
                {
                    string moCode = ((OnWIP)objSims[i]).MOCode;
                    if (!arrMO.Contains(moCode))
                    {
                        arrMO.Add(moCode);
                    }
                }
                if (arrMO.Count > 0)
                {
                    for (int i = 0; i < arrMO.Count; i++)
                    {
                        this.drownListMoCode.AddItem(arrMO[i].ToString(), arrMO[i].ToString());
                    }
                    this.drownListMoCode.SetSelectItemText(arrMO[0].ToString());
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
                txtRcard.Focus();
                return;
            }
        }

        private bool initFlowPicBox()
        {
            clear();
            string routeCode = "",
                moCode = "",
                itemCode;
            object[] objs;

            if (string.IsNullOrEmpty(Convert.ToString(this.drownListMoCode.SelectedItemValue)))
            {
                return false;
            }
            moCode = Convert.ToString(this.drownListMoCode.SelectedItemValue);
            if (string.IsNullOrEmpty(Convert.ToString(this.drownListRouteCode.SelectedItemValue)))
            {
                return false;
            }
            routeCode = Convert.ToString(this.drownListRouteCode.SelectedItemValue);

            Messages msg = new Messages();
            if (m_DataCollectFacade == null) { m_DataCollectFacade = new DataCollectFacade(this.DataProvider); }
            try
            {
                //add
                string sourceRcard = m_DataCollectFacade.GetSourceCard(FormatHelper.CleanString(txtRcard.Text.ToUpper()), this.drownListMoCode.SelectedItemValue.ToString());
                //end
                //object objLastSimRe = m_DataCollectFacade.GetLastSimulationReport(FormatHelper.CleanString(sourceRcard));
                object objLastSimRe = m_DataCollectFacade.GetSimulationReport(this.drownListMoCode.SelectedItemValue.ToString(), FormatHelper.CleanString(sourceRcard));
                
                
                if (objLastSimRe == null)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$Error_CS_ID_Not_Exist"));
                    txtRcard.SelectAll();
                    txtRcard.Focus();
                    ApplicationRun.GetInfoForm().Add(msg);
                    return false;
                }
                else
                {
                    SimulationReport currentSimRe = (SimulationReport)objLastSimRe;
                    itemCode = currentSimRe.ItemCode;

                    //查询当前途程工序捞TBLITEMROUTE2OP表
                    //QueryItemOpFlow(当前itemCode,当前途程Code);
                    objs = m_DataCollectFacade.QueryItemOpFlow(itemCode, routeCode);
                    if (objs == null)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                        //add by klaus 置焦点
                        txtRcard.SelectAll();
                        txtRcard.Focus();
                        return false;
                    }

                    #region 画流程图
                    this.panelDrawFlow.Controls.Clear();
                    this.panelDrawFlow.Refresh();

                    StartButton btnStart = new StartButton();
                    this.panelDrawFlow.Controls.Add(btnStart);
                    btnStart.setProcessName(MutiLanguages.ParserString("$CS_Start"));
                    btnStart.Location = new Point(10, 30);
                    btnStart.DrawButton();

                    FunctionButton lastOne = btnStart;
                    FlowButton btnFlow = new StraightArrowButton();
                    double vdegree = 0;

                    ItemRoute2OP currentOP = null;

                    for (int i = 0; i < objs.Length; i++)
                    {
                        ItemRoute2OP itemRoute2Op = objs[i] as ItemRoute2OP;

                        btnFlow = lastOne.AddOutArrow(vdegree);
                        Operation op = (Operation)new BaseModelFacade(this.DataProvider).GetOperation(itemRoute2Op.OPCode);
                        lastOne = btnFlow.AddOutProcess(itemRoute2Op.OPCode);

                        lastOne.Tag = itemRoute2Op.OPCode;
                        lastOne.ProcessID = op.OPDescription;
                        lastOne.DrawButton();
                        lastOne.BackColor = Color.Green;
                        lastOne.Click += new EventHandler(btn_Click);

                        //string actionResult = m_DataCollectFacade.CheckOpIsExist(FormatHelper.CleanString(txtRcard.Text.Trim().ToUpper()), moCode, routeCode, itemRoute2Op.OPCode);
                        string actionResult = m_DataCollectFacade.CheckOpIsExist(sourceRcard, moCode, routeCode, itemRoute2Op.OPCode);
                        if (string.IsNullOrEmpty(actionResult))
                        {
                            lastOne.BackColor = Color.White;

                        }
                        else
                        {
                            if (actionResult == ProductStatus.GOOD)
                            {
                                lastOne.BackColor = Color.Green;
                            }
                            else
                            {
                                lastOne.BackColor = Color.Red;
                            }
                        }

                        if (itemRoute2Op.OPCode == currentSimRe.OPCode
                            && itemRoute2Op.RouteCode == currentSimRe.RouteCode)
                        {
                            currentOP = (ItemRoute2OP)objs[i];
                            if (currentSimRe.Status == ProductStatus.GOOD)
                            {
                                lastOne.BackColor = Color.Green;
                            }
                            else
                            {
                                lastOne.BackColor = Color.Red;
                            }
                        }

                        #region 注释 Terry 2012-11-13
                        //如果途程里工序是从0开始，显示背景有问题。
                        //if ((currentOP != null) && (i > currentOP.OPSequence - 1))
                        //{
                        //    lastOne.BackColor = Color.WhiteSmoke;
                        //}
                        #endregion

                        if ((btnFlow as StraightArrowButton).Degree == 90)
                        {
                            vdegree = (vdegree + 180) % 360;
                        }
                    }
                    btnFlow = lastOne.AddOutArrow(vdegree);
                    EndButton btnEnd = btnFlow.AddEnd();
                    btnEnd.Text = MutiLanguages.ParserString("$CS_End");
                    btnEnd.BackColor = lastOne.BackColor;
                    #endregion

                    return true;
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
                txtRcard.Focus();
                return false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            KeyEventArgs keyevent = new KeyEventArgs(Keys.Enter);
            txtRcard_KeyDown(sender, keyevent);
        }

        private void InitializeDatatable()
        {

            _DataTableLoadedPart.Columns.Add("ITEMCODE", typeof(string));
            _DataTableLoadedPart.Columns.Add("RCARD", typeof(string));
            _DataTableLoadedPart.Columns.Add("MOCODE", typeof(string));
            _DataTableLoadedPart.Columns.Add("ACTIONRESULT", typeof(string));
            _DataTableLoadedPart.Columns.Add("OPCODE", typeof(string));
            _DataTableLoadedPart.Columns.Add("ACTION", typeof(string));
            _DataTableLoadedPart.Columns.Add("ROUTECODE", typeof(string));
            _DataTableLoadedPart.Columns.Add("SEGCODE", typeof(string));
            _DataTableLoadedPart.Columns.Add("SSCODE", typeof(string));
            _DataTableLoadedPart.Columns.Add("RESCODE", typeof(string));
            _DataTableLoadedPart.Columns.Add("MDATE", typeof(string));
            _DataTableLoadedPart.Columns.Add("MTIME", typeof(string));
            _DataTableLoadedPart.Columns.Add("MUSER", typeof(string));
            _DataTableLoadedPart.Clear();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (m_DataCollectFacade == null) { m_DataCollectFacade = new DataCollectFacade(this.DataProvider); }
            string sourceRcard = m_DataCollectFacade.GetSourceCard(FormatHelper.CleanString(txtRcard.Text.Trim().ToUpper()), this.drownListMoCode.SelectedItemValue.ToString());
            object objSim = m_DataCollectFacade.GetSimulationReport(Convert.ToString(this.drownListMoCode.SelectedItemValue), sourceRcard);
            if (objSim != null)
            {
                string opCode = Convert.ToString(((FunctionButton)sender).Tag);
                int scrollPosit = -1;

                int selectedRowIndex = gridSimulation.DisplayLayout.ActiveRow.Index;
                int ScrollPosition = gridSimulation.DisplayLayout.RowScrollRegions[0].VisibleRows[0].RowScrollRegion.ScrollPosition;

                for (int i = 0; i < gridSimulation.Rows.Count; i++)
                {
                    if (gridSimulation.Rows[i].Cells["OPCODE"].Value.ToString().Equals(opCode))
                    {
                        gridSimulation.Rows[i].CellAppearance.BackColor = ((FunctionButton)sender).BackColor;
                        if (scrollPosit == -1)
                        {
                            scrollPosit = i;
                        }
                    }
                    else
                    {
                        gridSimulation.Rows[i].CellAppearance.BackColor = Color.White;
                    }
                }
                if (scrollPosit >= 4 && gridSimulation.Rows.Count > 6)
                {

                    this.gridSimulation.Rows[scrollPosit].Activated = true;
                    this.gridSimulation.DisplayLayout.RowScrollRegions[0].VisibleRows[0].RowScrollRegion.ScrollPosition = scrollPosit;
                }
                else
                {
                    this.gridSimulation.Rows[0].Activated = true;
                    this.gridSimulation.DisplayLayout.RowScrollRegions[0].VisibleRows[0].RowScrollRegion.ScrollPosition = 0;
                }

            }
        }

        private void clear()
        {
            this.panelDrawFlow.Controls.Clear();
            _DataTableLoadedPart.Clear();
        }

        //根据LastAction显示工序结果       
        public static string GetOPResult(string lastAction)
        {
            string opResult = string.Empty;
            try
            {
                string moduleName = ActionType.GetOperationResultModule(lastAction);
                switch (moduleName)
                {
                    case "Testing":
                        opResult = MutiLanguages.ParserString("ItemTracing_testing");
                        break;
                    case "ComponentLoading":
                        opResult = MutiLanguages.ParserString("ItemTracing_componentloading");
                        break;
                    case "IDTranslation":
                        opResult = MutiLanguages.ParserString("ItemTracing_sn");
                        break;
                    case "Packing":
                        opResult = MutiLanguages.ParserString("ItemTracing_packing");
                        break;
                    case "UnPacking":
                        opResult = MutiLanguages.ParserString("ItemTracing_unpacking");
                        break;
                    case "TS":
                        opResult = MutiLanguages.ParserString("ItemTracing_ts");
                        break;
                    case "OQC":
                        opResult = MutiLanguages.ParserString("ItemTracing_oqc");
                        break;
                    //以下非工序结果 "Reject", "GoMO","ECN","SoftINFO","TRY"
                    case "Reject":
                        opResult = MutiLanguages.ParserString("ItemTracing_Reject");
                        break;
                    case "GoMO":
                        opResult = MutiLanguages.ParserString("ItemTracing_GoMO");
                        break;
                    case "ECN":
                        opResult = MutiLanguages.ParserString("ItemTracing_ECN");
                        break;
                    case "SoftINFO":
                        opResult = MutiLanguages.ParserString("ItemTracing_SoftINFO");
                        break;
                    case "TRY":
                        opResult = MutiLanguages.ParserString("ItemTracing_TRY");
                        break;
                    case "OutsideRoute":
                        opResult = MutiLanguages.ParserString("ItemTracing_OutsideRoute");
                        break;
                    case "SMT":
                        opResult = MutiLanguages.ParserString("ItemTracing_SMT");
                        break;
                    case "SPC":
                        opResult = MutiLanguages.ParserString("ItemTracing_SPC");
                        break;
                    case "DeductBOMItem":
                        opResult = MutiLanguages.ParserString("ItemTracing_DeductBOMItem");
                        break;
                    case "MidistOutput":
                        opResult = MutiLanguages.ParserString("ItemTracing_MidistOutput");
                        break;
                    case "BurnIn":
                        opResult = MutiLanguages.ParserString("ItemTracing_BurnIn");
                        break;
                    case "BurnOut":
                        opResult = MutiLanguages.ParserString("ItemTracing_BurnOut");
                        break;
                    default:
                        opResult = string.Empty;
                        break;
                }

                return opResult;
            }
            catch
            {

            }

            return opResult;

        }

        public void initGirdData()
        {

            Messages msg = new Messages();
            _DataTableLoadedPart.Clear();
            if (m_DataCollectFacade == null) { m_DataCollectFacade = new DataCollectFacade(this.DataProvider); }
            try
            {
                //加入工单的条件  add by klaus 
                string sourceRcard = m_DataCollectFacade.GetSourceCard(FormatHelper.CleanString(this.txtRcard.Text.Trim().ToUpper()), this.drownListMoCode.SelectedItemValue.ToString());
                object[] objs = m_DataCollectFacade.QueryItemTracingFlow(sourceRcard,
                    Convert.ToString(this.drownListRouteCode.SelectedItemValue),
                    Convert.ToString(this.drownListMoCode.SelectedItemValue));
                _DataTableLoadedPart.Clear();
                if (objs == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                    return;
                }

                for (int i = 0; i < objs.Length; i++)
                {
                    OnWIP wip = objs[i] as OnWIP;
                    _DataTableLoadedPart.Rows.Add(new object[] {                                                            
                                                            wip.ItemCode.ToString(),
                                                            wip.RunningCard.ToString(),
                                                            wip.MOCode.ToString(),
								                            MutiLanguages.ParserString(wip.ActionResult.ToString()),
                                                            wip.OPCode.ToString(),
                                                            GetOPResult(wip.Action),                                                            
                                                            wip.RouteCode.ToString(),
                                                            wip.SegmentCode.ToString(),
                                                            wip.StepSequenceCode.ToString(),
                                                            wip.ResourceCode.ToString(),                                                                                                         
                                                            FormatHelper.ToDateString(wip.MaintainDate),                                                         
                                                            FormatHelper.ToTimeString(wip.MaintainTime),                                                           
                                                            wip.GetDisplayText("MaintainUser")
                                                             });
                }
                this.gridSimulation.DataSource = this._DataTableLoadedPart;
            }
            catch (Exception ex)
            {

            }

        }

        //选择工单事件--加载途程下拉列表
        private void drownListMoCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(this.drownListMoCode.SelectedItemValue)))
            {
                this.drownListRouteCode.Clear();
                Messages msg = new Messages();
                if (m_DataCollectFacade == null) { m_DataCollectFacade = new DataCollectFacade(this.DataProvider); }
                try
                {
                    //add By Leo@20130128 
                    //获取原始Rcard
                    string sourceRcard = m_DataCollectFacade.GetSourceCard(FormatHelper.CleanString(txtRcard.Text.ToUpper()), this.drownListMoCode.SelectedItemValue.ToString());
                    //end
                    object[] objSims = m_DataCollectFacade.QueryOnwipRoutesByRcardAndMoCode(FormatHelper.CleanString(sourceRcard),
                    FormatHelper.CleanString(this.drownListMoCode.SelectedItemValue.ToString().Trim().ToUpper()));
                    if (objSims == null)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$Error_CS_ID_Not_Exist"));
                        txtRcard.SelectAll();
                        txtRcard.Focus();
                        ApplicationRun.GetInfoForm().Add(msg);
                        return;
                    }

                    //加载当前工单下该产品所经过的全部途程，默认显示当前所在途程的工序信息：
                    ArrayList arrRoute = new ArrayList();
                    for (int i = 0; i < objSims.Length; i++)
                    {
                        string routeCode = ((OnWIP)objSims[i]).RouteCode;
                        if (!arrRoute.Contains(routeCode))
                        {
                            arrRoute.Add(routeCode);
                        }
                    }
                    if (arrRoute.Count > 0)
                    {
                        for (int i = 0; i < arrRoute.Count; i++)
                        {
                            this.drownListRouteCode.AddItem(arrRoute[i].ToString(), arrRoute[i].ToString());
                        }
                        this.drownListRouteCode.SetSelectItemText(arrRoute[0].ToString());
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        //加载途程并显示grid中的数据
        private void drownListRouteCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(this.drownListRouteCode.SelectedItemValue))
                && !string.IsNullOrEmpty(Convert.ToString(this.drownListMoCode.SelectedItemValue)))
            {
                if (initFlowPicBox())
                {
                    initGirdData();
                }
            }
        }

        private void FCollectionViewFlow_Load(object sender, EventArgs e)
        {
            //this.InitPageLanguage();
            //this.InitGridLanguage(this.gridSimulation);
        }
    }
}
