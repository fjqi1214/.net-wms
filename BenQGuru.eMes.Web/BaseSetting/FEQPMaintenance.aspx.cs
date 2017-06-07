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
    public partial class FEQPMaintenance : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblRouteTitle;


        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = null;
        private BenQGuru.eMES.Material.EquipmentFacade _facade = null;//	new BaseModelFacadeFactory().Create();

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
                drpCycleQuery_Load();
                drpCycleEdit_Load();
                drpMaintenanceTypeQuery_Load();
                drpMaintenanceTypeEdit_Load();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("EQPID", "设备编码", null);
            this.gridHelper.AddColumn("EQPDESC", "设备描述", null);
            this.gridHelper.AddColumn("Cycle", "周期", null);
            this.gridHelper.AddColumn("Frequency", "频率", null);
            this.gridHelper.AddColumn("MaintenanceTypeDesc", "保养类型", null);
            this.gridHelper.AddColumn("MaintenanceType", "保养类型", null);
            this.gridHelper.AddColumn("MaintenanceItem", "保养内容", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddDefaultColumn(true, true);

            this.gridWebGrid.Columns.FromKey("MaintenanceType").Hidden = true;
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).Eqpid,
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).EqpDesc,								
            //                    this.getCycleString(((Domain.Equipment.EQPMaintenanceQuery)obj).CycleType),
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).Frequency,
            //                    this.languageComponent1.GetString(((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType),
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType,
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainITEM,
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).GetDisplayText("Muser"),
            //                    FormatHelper.ToDateString(((Domain.Equipment.EQPMaintenanceQuery)obj).Mdate), 
            //                    FormatHelper.ToTimeString(((Domain.Equipment.EQPMaintenanceQuery)obj).Mtime)
            //    });
            DataRow row = this.DtSource.NewRow();
            row["EQPID"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).Eqpid;
            row["EQPDESC"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).EqpDesc;
            row["Cycle"] = this.getCycleString(((Domain.Equipment.EQPMaintenanceQuery)obj).CycleType);
            row["Frequency"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).Frequency;
            row["MaintenanceTypeDesc"] = this.languageComponent1.GetString(((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType);
            row["MaintenanceType"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType;
            row["MaintenanceItem"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainITEM;
            row["MaintainUser"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).GetDisplayText("Muser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Domain.Equipment.EQPMaintenanceQuery)obj).Mdate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Domain.Equipment.EQPMaintenanceQuery)obj).Mtime);
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPMaintenance(
                FormatHelper.CleanString(this.txtEQPIDQuery.Text),
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
            return this._facade.QueryEQPMaintenanceCount(
                FormatHelper.CleanString(this.txtEQPIDQuery.Text),
                FormatHelper.CleanString(this.drpCycleQuery.SelectedValue),
                 FormatHelper.CleanString(this.ddlEQPMaintenanceTypeQuery.SelectedValue)
                );
        }


        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }

            object objEqp = this._facade.GetEquipment(((Domain.Equipment.EQPMaintenance)domainObject).Eqpid);
            if (objEqp == null)
            {
                WebInfoPublish.Publish(this, "$Error_EQPID_IS_NOT_EXIST", languageComponent1);
                return;

            }

            object obj = this._facade.GetEQPMaintenance(((Domain.Equipment.EQPMaintenance)domainObject).Eqpid, ((Domain.Equipment.EQPMaintenance)domainObject).MaintainITEM, ((Domain.Equipment.EQPMaintenance)domainObject).MaintainType);
            if (obj == null)
            {
                this._facade.AddEQPMaintenance((Domain.Equipment.EQPMaintenance)domainObject);
            }
            else
            {
                WebInfoPublish.Publish(this, "$Error_PK_is_Repeat", languageComponent1);
            }
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }

            this._facade.DeleteEQPMaintenance((Domain.Equipment.EQPMaintenance[])domainObjects.ToArray(typeof(Domain.Equipment.EQPMaintenance)));

        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            this._facade.UpdateEQPMaintenance((Domain.Equipment.EQPMaintenance)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtEQPIDEdit.Readonly = false;
                this.txtMaintenanceItemEdit.ReadOnly = false;
                this.ddlMaintenanceTypeEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtEQPIDEdit.Readonly = true;
                this.txtMaintenanceItemEdit.ReadOnly = true;
                this.ddlMaintenanceTypeEdit.Enabled = false;
            }
            if (pageAction == PageActionType.Delete)
            {
                this.gridHelper.RequestData();
            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new Material.EquipmentFacade(base.DataProvider);
            }
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            Domain.Equipment.EQPMaintenance maintenance = this._facade.CreateNewEQPMaintenance();

            maintenance.Eqpid = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDEdit.Text, 40));
            maintenance.MaintainITEM = FormatHelper.CleanString(this.txtMaintenanceItemEdit.Text, 400);
            maintenance.CycleType = this.drpCycleEdit.SelectedValue;
            maintenance.MaintainType = FormatHelper.CleanString(this.ddlMaintenanceTypeEdit.SelectedValue, 40);
            maintenance.Frequency = int.Parse(FormatHelper.CleanString(this.txtFrequencyEdit.Text, 6));
            maintenance.Muser = this.GetUserCode();
            maintenance.Mdate = currentDateTime.DBDate;
            maintenance.Mtime = currentDateTime.DBTime;

            return maintenance;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            object obj = _facade.GetEQPMaintenance(row.Items.FindItemByKey("EQPID").Text.ToString(), row.Items.FindItemByKey("MaintenanceItem").Text.ToString(), row.Items.FindItemByKey("MaintenanceType").Text.ToString());

            if (obj != null)
            {
                return (Domain.Equipment.EQPMaintenance)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtEQPIDEdit.Text = "";
                this.drpCycleEdit.SelectedIndex = 0;
                this.ddlMaintenanceTypeEdit.SelectedIndex = 0;
                this.txtMaintenanceItemEdit.Text = "";
                this.txtFrequencyEdit.Text = "";

                return;
            }

            try
            {
                this.drpCycleEdit.SelectedValue = ((Domain.Equipment.EQPMaintenance)obj).CycleType.ToString();
            }
            catch
            {
                this.drpCycleEdit.SelectedIndex = 0;
            }
            try
            {
                this.ddlMaintenanceTypeEdit.SelectedValue = ((Domain.Equipment.EQPMaintenance)obj).MaintainType.ToString();
            }
            catch
            {
                this.ddlMaintenanceTypeEdit.SelectedIndex = 0;
            }
            this.txtEQPIDEdit.Text = ((Domain.Equipment.EQPMaintenance)obj).Eqpid.ToString();
            this.txtMaintenanceItemEdit.Text = ((Domain.Equipment.EQPMaintenance)obj).MaintainITEM.ToString();
            this.txtFrequencyEdit.Text = ((Domain.Equipment.EQPMaintenance)obj).Frequency.ToString();

        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblPEQPIDEdit, this.txtEQPIDEdit, 40, true));
            manager.Add(new LengthCheck(this.lblMaintenanceTypeEdit, this.ddlMaintenanceTypeEdit, 400, true));
            manager.Add(new LengthCheck(this.lblMaintenanceItemEdit, this.txtMaintenanceItemEdit, 400, true));
            manager.Add(new NumberCheck(this.lblFrequencyEdit, this.txtFrequencyEdit, 0, 999999, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
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

        #region 数据初始化

        private void drpCycleEdit_Load()
        {
            if (!this.IsPostBack)
            {
                this.drpCycleEdit.Items.Clear();
                this.drpCycleEdit.Items.Add(new ListItem("日", "D"));
                this.drpCycleEdit.Items.Add(new ListItem("周", "W"));
                this.drpCycleEdit.Items.Add(new ListItem("月", "M"));
                this.drpCycleEdit.Items.Add(new ListItem("年", "Y"));
            }
        }

        private void drpCycleQuery_Load()
        {
            if (!this.IsPostBack)
            {
                this.drpCycleQuery.Items.Clear();
                this.drpCycleQuery.Items.Add(new ListItem("", ""));
                this.drpCycleQuery.Items.Add(new ListItem("日", "D"));
                this.drpCycleQuery.Items.Add(new ListItem("周", "W"));
                this.drpCycleQuery.Items.Add(new ListItem("月", "M"));
                this.drpCycleQuery.Items.Add(new ListItem("年", "Y"));
            }
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

        private void drpMaintenanceTypeEdit_Load()
        {
            if (!this.IsPostBack)
            {
                this.ddlMaintenanceTypeEdit.Items.Clear();
                this.ddlMaintenanceTypeEdit.Items.Add(new ListItem("", ""));
                this.ddlMaintenanceTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MaintenanceType.MaintenanceType_DayType), MaintenanceType.MaintenanceType_DayType));
                this.ddlMaintenanceTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MaintenanceType.MaintenanceType_UsingType), MaintenanceType.MaintenanceType_UsingType));
            }
        }
        #endregion

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((Domain.Equipment.EQPMaintenanceQuery)obj).Eqpid.ToString(),
                                   ((Domain.Equipment.EQPMaintenanceQuery)obj).EqpDesc.ToString(),
                                   this.getCycleString(((Domain.Equipment.EQPMaintenanceQuery)obj).CycleType.ToString()),
                                   ((Domain.Equipment.EQPMaintenanceQuery)obj).Frequency.ToString(),
                                   this.languageComponent1.GetString(((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType),
                                   ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainITEM,                                
                                   ((Domain.Equipment.EQPMaintenanceQuery)obj).Muser,
                                   FormatHelper.ToDateString(((Domain.Equipment.EQPMaintenanceQuery)obj).Mdate), 
                                   FormatHelper.ToTimeString(((Domain.Equipment.EQPMaintenanceQuery)obj).Mtime)
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"EQPID",
                                    "EQPDESC",
                                    "Cycle",
                                    "Frequency",
                                    "MaintenanceType",
                                    "MaintenanceItem",
                                    "MaintainUser","MaintainDate","MaintainTime"};
        }

        #endregion
    }
}

