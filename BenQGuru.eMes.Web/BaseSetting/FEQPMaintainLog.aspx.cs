using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FRouteMP 的摘要说明。
    /// </summary>
    public partial class FEQPMaintainLog : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblRouteTitle;

        protected TextBox dateMaintenanceDateFromQuery;
        protected TextBox dateMaintenanceDateToQuery;

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = null;
        private BenQGuru.eMES.Material.EquipmentFacade _facade = null;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.drpMaintenanceResultQuery_Load();
                this.drpCycleQuery_Load();
                this.drpMaintenanceTypeQuery_Load();
               
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected void drpMaintenanceResultQuery_Load()
        {
            this.drpMaintenanceResultQuery.Items.Clear();
            this.drpMaintenanceResultQuery.Items.Add(new ListItem("", ""));
            this.drpMaintenanceResultQuery.Items.Add(new ListItem("OK", "OK"));
            this.drpMaintenanceResultQuery.Items.Add(new ListItem("NG", "NG"));
        }

        private void drpCycleQuery_Load()
        {
            this.drpCycleQuery.Items.Clear();
            this.drpCycleQuery.Items.Add(new ListItem("", ""));
            this.drpCycleQuery.Items.Add(new ListItem("日", "D"));
            this.drpCycleQuery.Items.Add(new ListItem("周", "W"));
            this.drpCycleQuery.Items.Add(new ListItem("月", "M"));
            this.drpCycleQuery.Items.Add(new ListItem("年", "Y"));
        }

        private void drpMaintenanceTypeQuery_Load()
        {
            if (!this.IsPostBack)
            {
                this.ddlEQPMaintenanceTypeQuery.Items.Clear();
                this.ddlEQPMaintenanceTypeQuery.Items.Add(new ListItem("", ""));
                this.ddlEQPMaintenanceTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(MaintenanceType.MaintenanceType_DayType), MaintenanceType.MaintenanceType_DayType));
                this.ddlEQPMaintenanceTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(MaintenanceType.MaintenanceType_UsingType), MaintenanceType.MaintenanceType_UsingType));
            }
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("Serial", "序号", null);
            this.gridHelper.AddColumn("EQPID", "设备编码", null);
            this.gridHelper.AddColumn("EQPDESC", "设备描述", null);
            this.gridHelper.AddColumn("Cycle", "周期", null);
            this.gridHelper.AddColumn("MaintenanceTypeDesc", "保养类型", null);
            this.gridHelper.AddColumn("MaintenanceType", "保养类型", null);
            this.gridHelper.AddColumn("MaintenanceItem", "保养内容", null);
            this.gridHelper.AddColumn("MaintenanceResult", "保养结果", null);
            this.gridHelper.AddColumn("MaintenanceDate", "保养日期", null);
            this.gridHelper.AddColumn("MaintenanceUser", "保养用户", null);
            this.gridHelper.AddColumn("MaintenanceMemo", "保养备注", null);

            this.gridWebGrid.Columns.FromKey("Serial").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintenanceType").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
           
            DataRow row = this.DtSource.NewRow();
            row["Serial"] = ((Domain.Equipment.EQPMaintainLogQuery)obj).Serial.ToString();
            row["EQPID"] = ((Domain.Equipment.EQPMaintainLogQuery)obj).Eqpid;
            row["EQPDESC"] = ((Domain.Equipment.EQPMaintainLogQuery)obj).EqpDesc;
            row["Cycle"] = this.getCycleString(((Domain.Equipment.EQPMaintainLogQuery)obj).CycleType);
            row["MaintenanceTypeDesc"] = this.languageComponent1.GetString(((Domain.Equipment.EQPMaintainLogQuery)obj).MaintainType);
            row["MaintenanceType"] = ((Domain.Equipment.EQPMaintainLogQuery)obj).MaintainType;
            row["MaintenanceItem"] = ((Domain.Equipment.EQPMaintainLogQuery)obj).MaintainITEM;
            row["MaintenanceResult"] = ((Domain.Equipment.EQPMaintainLogQuery)obj).Result;
            row["MaintenanceDate"] = FormatHelper.ToDateString(((Domain.Equipment.EQPMaintainLogQuery)obj).Mdate);
            row["MaintenanceUser"] = ((Domain.Equipment.EQPMaintainLogQuery)obj).GetDisplayText("Muser");
            row["MaintenanceMemo"] = ((Domain.Equipment.EQPMaintainLogQuery)obj).MEMO;
            return row;


        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPMaintainLog(
                FormatHelper.CleanString(this.txtEQPIDQuery.Text),
                FormatHelper.TODateInt(this.dateMaintenanceDateFromQuery.Text),
                FormatHelper.TODateInt(this.dateMaintenanceDateToQuery.Text),
                drpMaintenanceResultQuery.SelectedValue,
                FormatHelper.CleanString(this.drpCycleQuery.SelectedValue),
                FormatHelper.CleanString(this.ddlEQPMaintenanceTypeQuery.SelectedValue),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPMaintainLogCount(
                FormatHelper.CleanString(this.txtEQPIDQuery.Text),
                FormatHelper.TODateInt(this.dateMaintenanceDateFromQuery.Text),
                FormatHelper.TODateInt(this.dateMaintenanceDateToQuery.Text),
                drpMaintenanceResultQuery.SelectedValue,
                FormatHelper.CleanString(this.drpCycleQuery.SelectedValue),
                FormatHelper.CleanString(this.ddlEQPMaintenanceTypeQuery.SelectedValue)
                );
        }

        protected string getCycleString(string cycle)
        {
            if (cycle == "D")
                return "日";
            else if (cycle == "W")
                return "周";
            else if (cycle == "M")
                return "月";
            else if (cycle == "Y")
                return "年";
            return "";
        }
        #endregion

        #region Button

        protected void cmdSave_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FEQPMaintainLogAdd.aspx"));
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            object obj = _facade.GetEQPMaintainLog(int.Parse(row.Items.FindItemByKey("Serial").Text.ToString().Trim()));

            if (obj != null)
            {
                return (Domain.Equipment.EQPMaintainLog)obj;
            }

            return null;
        }
        #endregion 

        #region Export
  
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((Domain.Equipment.EQPMaintainLogQuery)obj).Eqpid,
								((Domain.Equipment.EQPMaintainLogQuery)obj).EqpDesc,	
							    this.getCycleString(((Domain.Equipment.EQPMaintainLogQuery)obj).CycleType),
                                this.languageComponent1.GetString(((Domain.Equipment.EQPMaintainLogQuery)obj).MaintainType),
                                ((Domain.Equipment.EQPMaintainLogQuery)obj).MaintainITEM,
                                ((Domain.Equipment.EQPMaintainLogQuery)obj).Result,
                                FormatHelper.ToDateString(((Domain.Equipment.EQPMaintainLogQuery)obj).Mdate), 
                                ((Domain.Equipment.EQPMaintainLogQuery)obj).GetDisplayText("Muser"),
                                ((Domain.Equipment.EQPMaintainLogQuery)obj).MEMO
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"EQPID",
                                    "EQPDESC",
                                    "Cycle",
                                    "MaintenanceType",
                                    "MaintenanceItem",
                                    "MaintenanceResult",
                                    "MaintenanceDate","MaintenanceUser","MaintenanceMemo"};
        }

        #endregion
    }
}

