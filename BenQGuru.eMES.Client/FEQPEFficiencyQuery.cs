using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Domain.Equipment;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FEQPEFficiencyQuery : BaseForm
    {
        #region  变量

        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable _DataTableLoadedPart = new DataTable();

        #endregion

        #region 属性



        public IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        #endregion

        public FEQPEFficiencyQuery()
        {
            InitializeComponent();
        }

        private void FOldScrutiny_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            this.InitializePROID();
            this.InitializeSSCode();
            //this.InitPageLanguage();
        }

        #region 初始化Grid
        private void InitUltraGridUI(UltraGrid ultraGrid)
        {
            //ultraGrid.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            //ultraGrid.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            //ultraGrid.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            //ultraGrid.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            //ultraGrid.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGrid.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void InitializeUltraGrid()
        {
            InitUltraGridUI(this.ultraGridScrutiny);

            _DataTableLoadedPart.Columns.Add("EQPID", typeof(string));
            _DataTableLoadedPart.Columns.Add("EQPName", typeof(string));
            _DataTableLoadedPart.Columns.Add("SSCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("ResCode", typeof(string));
            _DataTableLoadedPart.Columns.Add("ActusedRate", typeof(string));
            _DataTableLoadedPart.Columns.Add("BXRate", typeof(string));
            _DataTableLoadedPart.Columns.Add("GoodRate", typeof(string));
            _DataTableLoadedPart.Columns.Add("OEE", typeof(string));
            this.ultraGridScrutiny.DataSource = this._DataTableLoadedPart;

            _DataTableLoadedPart.Clear();

            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["EQPID"].Width = 100;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["EQPName"].Width = 100;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["SSCode"].Width = 100;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ResCode"].Width = 100;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ActusedRate"].Width = 100;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["BXRate"].Width = 100;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["GoodRate"].Width = 100;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["OEE"].Width = 100;

            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["EQPID"].CellActivation = Activation.NoEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["EQPName"].CellActivation = Activation.NoEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["SSCode"].CellActivation = Activation.NoEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ResCode"].CellActivation = Activation.NoEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["ActusedRate"].CellActivation = Activation.NoEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["BXRate"].CellActivation = Activation.NoEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["GoodRate"].CellActivation = Activation.NoEdit;
            ultraGridScrutiny.DisplayLayout.Bands[0].Columns["OEE"].CellActivation = Activation.NoEdit;
        }

        private void ultraGridScrutiny_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridScrutiny);

            _UltraWinGridHelper1.AddReadOnlyColumn("EQPID", "设备编号");
            _UltraWinGridHelper1.AddReadOnlyColumn("EQPName", "设备名称");
            _UltraWinGridHelper1.AddReadOnlyColumn("SSCode", "产线代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("ResCode", "资源代码");
            _UltraWinGridHelper1.AddReadOnlyColumn("ActusedRate", "可用率");
            _UltraWinGridHelper1.AddReadOnlyColumn("BXRate", "表现性");
            _UltraWinGridHelper1.AddReadOnlyColumn("GoodRate", "质量指数");
            _UltraWinGridHelper1.AddReadOnlyColumn("OEE", "OEE");

            //this.InitGridLanguage(ultraGridScrutiny);
        }

        #endregion

        #region 页面事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            EquipmentFacade equipmentFacade = new EquipmentFacade(this.DataProvider);
            object[] objs = equipmentFacade.QueryEQPMaintenanceEffective(this.drpEQPID.SelectedItemValue.ToString(), this.drpSSCode.SelectedItemValue.ToString(), this.ucLabelEditResCode.Value.Trim());

            _DataTableLoadedPart.Clear();

            if (objs == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                return;
            }

            for (int i = 0; i < objs.Length; i++)
            {
                EQPMaintenanceForEffective eqpMaintenanceForEffective = objs[i] as EQPMaintenanceForEffective;
                _DataTableLoadedPart.Rows.Add(new object[] {
                                                            eqpMaintenanceForEffective.Eqpid,
                                                            eqpMaintenanceForEffective.Eqpname,
                                                            eqpMaintenanceForEffective.SSCode,
                                                            //eqpMaintenanceForEffective.OpCode,
                                                            eqpMaintenanceForEffective.ResCode,
                                                            eqpMaintenanceForEffective.ActusedRate,
                                                            eqpMaintenanceForEffective.BXRate,                                                       
                                                            eqpMaintenanceForEffective.GoodRate,
                                                            eqpMaintenanceForEffective.OEE
                                                             });
            }
        }


        #endregion

        private void InitializePROID()
        {
            EquipmentFacade equipmentFacade = new EquipmentFacade(this.DataProvider);
            object[] equipmentList = equipmentFacade.GetAllEquipment();

            if (equipmentList != null)
            {
                this.drpEQPID.Clear();
                this.drpEQPID.AddItem("", "");
                foreach (Equipment equipment in equipmentList)
                {
                    this.drpEQPID.AddItem(equipment.EqpId, equipment.EqpId);
                }
                this.drpEQPID.SelectedIndex = 0;

            }

        }

        private void InitializeSSCode()
        {
            string resCode = ApplicationService.Current().ResourceCode;
            BaseSetting.BaseModelFacade baseModelFacade = new BaseSetting.BaseModelFacade(this.DataProvider);
            object[] ssCodeList = baseModelFacade.GetAllStepSequence();

            if (ssCodeList != null)
            {
                this.drpSSCode.Clear();
                this.drpSSCode.AddItem("", "");
                foreach (StepSequence stepSequence in ssCodeList)
                {
                    this.drpSSCode.AddItem(stepSequence.StepSequenceDescription, stepSequence.StepSequenceCode);
                }
                this.drpSSCode.SelectedIndex = 0;

            }

        }

        private void buttonOpCodeQuery_Click(object sender, EventArgs e)
        {
            //FOPCodeQuery fOpCodeQuery = new FOPCodeQuery();
            //fOpCodeQuery.Owner = this;
            //fOpCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            //fOpCodeQuery.OPCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(OPCodeSelector_MCodeSelectedEvent);
            //fOpCodeQuery.ShowDialog();
            //fOpCodeQuery = null;

            FResCodeQuery fResCodeQuery = new FResCodeQuery(this.drpSSCode.SelectedItemValue.ToString());
            fResCodeQuery.Owner = this;
            fResCodeQuery.StartPosition = FormStartPosition.CenterScreen;
            fResCodeQuery.ResCodeSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(ResCodeSelector_MCodeSelectedEvent);
            fResCodeQuery.ShowDialog();
            fResCodeQuery = null;
        }

        private void ResCodeSelector_MCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.ucLabelEditResCode.Value = e.CustomObject;
        }

    }
}