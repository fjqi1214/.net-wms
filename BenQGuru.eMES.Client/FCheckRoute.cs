using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.TS;

using Infragistics.Win.UltraWinGrid;
using UserControl;


namespace BenQGuru.eMES.Client
{
    public partial class FCheckRoute:BaseForm
    {
        #region 变量
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private ItemFacade m_ItemFacade = null;
        private MOFacade m_MOFacade = null;
        private DataTable dtSource = new DataTable();
        private string _NextOP = string.Empty;
        #endregion

        #region 属性
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        private MOFacade moFacade
        {
            get
            {
                if (m_MOFacade == null)
                {
                    m_MOFacade = new MOFacade(DataProvider);
                }
                return m_MOFacade;
            }
        }

        private ItemFacade itemFacade
        {
            get
            {
                if (m_ItemFacade == null)
                {
                    m_ItemFacade = new ItemFacade(DataProvider);
                }
                return m_ItemFacade;
            }
        }

        #endregion

        public FCheckRoute()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            this.ultraGridHead.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridHead.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridHead.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridHead.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridHead.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridHead.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridHead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridHead.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FCheckRoute_Load(object sender, EventArgs e)
        {
            InitializeDataTable();

            ////this.InitPageLanguage();

        }


        private void ultraGridHead_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["opCode"].Header.Caption = "工序代码";
            e.Layout.Bands[0].Columns["opDescription"].Header.Caption = "工序描述";

            e.Layout.Bands[0].Columns["opCode"].Width = 200;
            e.Layout.Bands[0].Columns["opDescription"].Width = 200;

            this.ultraGridHead.DisplayLayout.Bands[0].Columns["opCode"].CellActivation = Activation.ActivateOnly;
            this.ultraGridHead.DisplayLayout.Bands[0].Columns["opDescription"].CellActivation = Activation.ActivateOnly;
            this.ultraGridHead.DisplayLayout.Override.ActiveRowAppearance.BackColor = Color.Empty;
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;

            //this.InitGridLanguage(ultraGridHead);
        }

        private void ucLabelEditRcard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                dtSource.Clear();
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                //转换成起始序列号
                string sourceCard = dataCollectFacade.GetSourceCard(this.ucLabelEditRcard.Value.ToString().ToUpper().Trim(), string.Empty);
                //end
                object objSimulationReport = dataCollectFacade.GetLastSimulationReport(sourceCard);
                if (objSimulationReport != null)
                {
                    SimulationReport simulationReport = objSimulationReport as SimulationReport;
                    if (simulationReport.IsComplete.Equals("1"))
                    {
                        ucMessageInfo.AddEx(null, this.ucLabelEditRcard.Value, new UserControl.Message(MessageType.Error, "$Error_RunningCard_IsCompelete"), true);
                        this.ucLabelEditRcard.TextFocus(false, true);
                        return;
                    }
                    this.ucLabelEditItemCode.Value = simulationReport.ItemCode;
                    this.ucLabelEditOp.Value = simulationReport.OPCode;
                    this.ucLabelEditRout.Value = simulationReport.RouteCode;
                    object objMo = moFacade.GetMO(simulationReport.MOCode);
                    object objItem = itemFacade.GetItem(simulationReport.ItemCode, ((MO)objMo).OrganizationID);
                    this.ucLabelEditName.Value = ((Item)objItem).ItemName;
                    object[] objOperation = null;
                    if (simulationReport.Status.Equals("GOOD"))
                    {
                        objOperation = itemFacade.QueryItemRoute2Operation(simulationReport.ItemCode, simulationReport.RouteCode);
                        CheckNextOp(simulationReport.MOCode, simulationReport.RouteCode, simulationReport.OPCode);
                    }
                    if (simulationReport.Status.Equals("NG"))
                    {
                        TSFacade tsFacade = new TSFacade(this.DataProvider);
                        object objTs = tsFacade.QueryLastTSByRunningCard(sourceCard);
                        if (objTs != null)
                        {
                            string refRouteCode = ((Domain.TS.TS)objTs).ReflowRouteCode;
                            string refOpCode = ((Domain.TS.TS)objTs).ReflowOPCode;
                            if (refRouteCode == string.Empty || refOpCode == string.Empty)
                            {
                                objOperation = itemFacade.QueryItemRoute2Operation(simulationReport.ItemCode, simulationReport.RouteCode);
                                CheckNextOp(simulationReport.MOCode, simulationReport.RouteCode, simulationReport.OPCode);
                            }
                            else
                            {
                                this.ucLabelEditOp.Value = ((Domain.TS.TS)objTs).ConfirmOPCode;
                                objOperation = itemFacade.QueryItemRoute2Operation(simulationReport.ItemCode, refRouteCode);
                                BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
                                object objOP2Res = baseModelFacade.GetOperationByResource(ApplicationService.Current().ResourceCode);
                                string opCode2Res = string.Empty;
                                if (objOP2Res != null)
                                {
                                    opCode2Res = ((Operation2Resource)objOP2Res).OPCode;
                                }
                                _NextOP = refOpCode;
                                if (opCode2Res != refOpCode)
                                {
                                    string opDesc = ((Operation)baseModelFacade.GetOperation(refOpCode)).OPDescription;
                                    ucMessageInfo.AddEx(null, this.ucLabelEditRcard.Value, new UserControl.Message(MessageType.Error, "$Error_Please_Send_Rcard_To " + opDesc + " $CS_Param_OPCode"), true);
                                }

                            }
                        }
                    }                    

                    if (objOperation != null)
                    {
                        foreach (Operation operation in objOperation)
                        {
                            DataRow dr = dtSource.NewRow();
                            dr["opCode"] = operation.OPCode;
                            dr["opDescription"] = operation.OPDescription;
                            dtSource.Rows.Add(dr);
                        }
                        ultraGridHead.DataSource = dtSource;
                    }
                   
                }
                else
                {
                    this.ucLabelEditItemCode.Value = "";
                    this.ucLabelEditOp.Value = "";
                    this.ucLabelEditRout.Value = "";
                    this.ucLabelEditName.Value = "";
                    ucMessageInfo.AddEx(null, this.ucLabelEditRcard.Value, new UserControl.Message(MessageType.Error, "$Error_ProductInfo_IS_Null"), true);
                }
                this.ucLabelEditRcard.TextFocus(false, true);
            }

        }

        private void ultraGridHead_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.Cells["opCode"].Value.ToString().Equals(_NextOP.Trim().ToUpper()))
            {
                e.Row.Appearance.BackColor = Color.Red;
            }
        }

        #region method
        private void InitializeDataTable()
        {
            dtSource.Columns.Add("opCode");
            dtSource.Columns.Add("opDescription");
            ultraGridHead.DataSource = dtSource;
        }

        private void CheckNextOp(string mCode, string routeCode, string opCode)
        {
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            object objOP2Res = baseModelFacade.GetOperationByResource(ApplicationService.Current().ResourceCode);
            string opCode2Res = string.Empty;
            if (objOP2Res != null)
            {
                opCode2Res = ((Operation2Resource)objOP2Res).OPCode;
            }
            object objNetxOp = itemFacade.GetMORouteNextOperation(mCode, routeCode, opCode);
            _NextOP = string.Empty;
            if (objNetxOp != null)
            {
                string opNextCode = (objNetxOp as ItemRoute2OP).OPCode;
                _NextOP = (objNetxOp as ItemRoute2OP).OPCode;
                if (opCode2Res != opNextCode)
                {
                    string opDesc = ((Operation)baseModelFacade.GetOperation(opNextCode)).OPDescription;
                    ucMessageInfo.AddEx(null, this.ucLabelEditRcard.Value, new UserControl.Message(MessageType.Error, "$Error_Please_Send_Rcard_To " + opDesc + " $CS_Param_OPCode"), true);
                }
            }
        }

        #endregion
    }
}
