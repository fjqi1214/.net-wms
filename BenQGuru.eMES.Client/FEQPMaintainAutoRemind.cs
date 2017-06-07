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
    public partial class FEQPMaintainAutoRemind : BaseForm
    {
        #region  变量

        private UltraWinGridHelper _UltraWinGridHelper1 = null;
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private DataTable _DataTableLoadedPart = new DataTable();

        DBDateTime dbDateTime;
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

        public FEQPMaintainAutoRemind()
        {
            InitializeComponent();
        }

        private void FOldScrutiny_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            this.InitializePROID();
            this.InitializeDUCT();
            this.edtTimer.Value = 5+"";
            //this.InitPageLanguage();
         }

        #region 初始化Grid
        private void InitUltraGridUI(UltraGrid ultraGrid)
        {
            //ultraGrid.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            //ultraGrid.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGrid.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            //ultraGrid.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            //ultraGrid.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            //ultraGrid.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            //ultraGrid.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            //ultraGrid.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGrid.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void InitializeUltraGrid()
        {
            InitUltraGridUI(this.ultraGridMaintenance);

            _DataTableLoadedPart.Columns.Add("EQPID", typeof(string));
            _DataTableLoadedPart.Columns.Add("EQPName", typeof(string));
            _DataTableLoadedPart.Columns.Add("MaintainTypeForEQP", typeof(string));
            _DataTableLoadedPart.Columns.Add("MaintainItem", typeof(string));
            _DataTableLoadedPart.Columns.Add("CycleType", typeof(string));
            _DataTableLoadedPart.Columns.Add("Frequency", typeof(string));
            _DataTableLoadedPart.Columns.Add("MPDate", typeof(string));
            _DataTableLoadedPart.Columns.Add("LastMaintenanceDate", typeof(string));
            _DataTableLoadedPart.Columns.Add("RUNuration", typeof(string));
            _DataTableLoadedPart.Columns.Add("LeftUseTime", typeof(string));

            this.ultraGridMaintenance.DataSource = this._DataTableLoadedPart;

            _DataTableLoadedPart.Clear();

            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["EQPID"].Width = 120;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["EQPName"].Width = 160;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["MaintainTypeForEQP"].Width = 80;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["MaintainItem"].Width = 160;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["CycleType"].Width = 60;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["Frequency"].Width = 60;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["MPDate"].Width = 100;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["LastMaintenanceDate"].Width = 100;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["RUNuration"].Width = 110;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["LeftUseTime"].Width = 110;

            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["EQPID"].CellActivation = Activation.NoEdit;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["EQPName"].CellActivation = Activation.NoEdit;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["MaintainTypeForEQP"].CellActivation = Activation.NoEdit;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["MaintainItem"].CellActivation = Activation.NoEdit;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["CycleType"].CellActivation = Activation.NoEdit;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["Frequency"].CellActivation = Activation.NoEdit;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["MPDate"].CellActivation = Activation.NoEdit;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["LastMaintenanceDate"].CellActivation = Activation.NoEdit;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["RUNuration"].CellActivation = Activation.NoEdit;
            ultraGridMaintenance.DisplayLayout.Bands[0].Columns["LeftUseTime"].CellActivation = Activation.NoEdit;
        }

        private void ultraGridScrutiny_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            _UltraWinGridHelper1 = new UltraWinGridHelper(this.ultraGridMaintenance);

            _UltraWinGridHelper1.AddReadOnlyColumn("EQPID", "设备编号");
            _UltraWinGridHelper1.AddReadOnlyColumn("EQPName", "设备名称");
            _UltraWinGridHelper1.AddReadOnlyColumn("MaintainTypeForEQP", "保养类型");
            _UltraWinGridHelper1.AddReadOnlyColumn("MaintainItem", "保养内容");
            _UltraWinGridHelper1.AddReadOnlyColumn("CycleType", " 周期");
            _UltraWinGridHelper1.AddReadOnlyColumn("Frequency", "频率");
            _UltraWinGridHelper1.AddReadOnlyColumn("MPDate", "保养计划维护日期");
            _UltraWinGridHelper1.AddReadOnlyColumn("LastMaintenanceDate", "上次保养日期");
            _UltraWinGridHelper1.AddReadOnlyColumn("RUNuration", "累计使用时间(天)");
            _UltraWinGridHelper1.AddReadOnlyColumn("LeftUseTime", "剩余使用时间(天)");
            //this.InitGridLanguage(ultraGridMaintenance);
        }

        #endregion

        #region 页面事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            EquipmentFacade equipmentFacade = new EquipmentFacade(this.DataProvider);
            object[] objs = equipmentFacade.QueryEQPMaintenanceAutoRemind(this.drpEQPID.SelectedItemValue.ToString(), this.ucLabelEditMaintainITEM.Value.Trim(), this.drpMaintainTYPE.SelectedItemValue.ToString());
            dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            _DataTableLoadedPart.Clear();

            if (objs == null)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                return;
            }

            for (int i = 0; i < objs.Length; i++)
            {
                DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);
                EQPMaintenanceForQuery eqpMaintenanceForQuery = objs[i] as EQPMaintenanceForQuery;
                _DataTableLoadedPart.Rows.Add(new object[] {
                                                            eqpMaintenanceForQuery.Eqpid,
                                                            eqpMaintenanceForQuery.Eqpname,                                                            
                                                            getMaintainType(eqpMaintenanceForQuery.MaintainType),
                                                            eqpMaintenanceForQuery.MaintainITEM,
                                                            getCycleString(eqpMaintenanceForQuery.CycleType),
                                                            eqpMaintenanceForQuery.Frequency,
                                                            FormatHelper.ToDateString(eqpMaintenanceForQuery.Mdate, "-"),
                                                            FormatHelper.ToDateString(eqpMaintenanceForQuery.LastMaintenanceDate, "-"),
                                                            eqpMaintenanceForQuery.ActDuration  ,
                                                            eqpMaintenanceForQuery.LastTime                                                                   
                                                             });
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.edtTimer.Value.Equals(""))
            {
                timer1.Interval = 5*60*1000;
            }
            else
            {
                timer1.Interval = (int)(double.Parse(this.edtTimer.Value) * 60 * 1000);
            }
            this.btnQuery_Click(sender, e);
        }

        private void chkIsvalid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkIsvalid.Checked == true)
            {
                this.timer1.Enabled = true;
                this.edtTimer.Value = 5+"";
            }
            else
            {
                this.timer1.Enabled = false;
            }
        }

        private void ultraGridScrutiny_InitializeRow(object sender, InitializeRowEventArgs e)
        {
               
            if (Convert.ToDouble(e.Row.Cells[9].Value.ToString())  <= int.Parse(this.ucLToMaintainDate.Value.Trim())
                && Convert.ToDouble(e.Row.Cells[9].Value.ToString()) >0 )
            {
                e.Row.Appearance.BackColor = Color.Yellow;
            }

            else  if (Convert.ToDouble(e.Row.Cells[9].Value.ToString()) <=0  )
            {
                e.Row.Appearance.BackColor = Color.Red;
            }
        }

        protected string getCycleString(string cycle)
        {
            if (cycle == "D")
                return MutiLanguages.ParserString("$CS_DAY");
            else if (cycle == "W")
                return MutiLanguages.ParserString("$CS_Week");
            else if (cycle == "M")
                return MutiLanguages.ParserString("$CS_Month");
            else if (cycle == "Y")
                return MutiLanguages.ParserString("$CS_Year");
            return "";
        }

        protected string getMaintainType(string type)
        {
            if (type == "DAYTYPE")
                return MutiLanguages.ParserString("$CS_DAYTYPE");
            else if (type == "USINGTYPE")
                return MutiLanguages.ParserString("$CS_USINGTYPE");
            return "";
        }

        #endregion

        private void InitializePROID()
        {
            string resCode = ApplicationService.Current().ResourceCode;
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

        private void InitializeDUCT()
        {
            this.drpMaintainTYPE.Clear();
            this.drpMaintainTYPE.AddItem("", "");
            this.drpMaintainTYPE.AddItem(MutiLanguages.ParserString(EqpMaintType.EqpMaintType_USINGTYPE), EqpMaintType.EqpMaintType_USINGTYPE);
            this.drpMaintainTYPE.AddItem(MutiLanguages.ParserString(EqpMaintType.EqpMaintType_DAYTYPE), EqpMaintType.EqpMaintType_DAYTYPE);
            this.drpMaintainTYPE.SelectedIndex = 0;
        }



        private void edtTimer_InnerTextChanged(object sender, EventArgs e)
        {
            if (this.edtTimer.Value.Trim() == "0")
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_Timer_Must_Bigger_Than_Zero"));
                return;
            }
            if (this.edtTimer.Value.Trim() != "")
            {
                timer1.Interval = (int)(double.Parse(this.edtTimer.Value) * 60 * 1000);
            }
        }
    }
}