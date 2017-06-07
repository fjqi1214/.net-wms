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
using BenQGuru.eMES.Domain.Equipment;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FRouteMP 的摘要说明。
    /// </summary>
    public partial class FEQPMaintainLogAdd : BaseMPageNew
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
            this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
        }
        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            e.Row.Items.FindItemByKey("MaintenanceMemo").CssClass = "CellBackColor";
            string strScript = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq(11)').css('background-color','#fffdf1');
                               ", e.Row.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);



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
                this.drpCycleQuery_Load();
                drpMaintenanceTypeQuery_Load();
            }
            
        }

        protected override void OnPreRender(EventArgs e)
        {

            this.cmdSave.Disabled = false;

            base.OnPreRender(e);

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
            this.gridHelper.AddColumn("LastMaintenanceDate", "上次保养日期", null);
            this.gridHelper.AddCheckBoxColumn("MaintenanceResult", "保养合格", true, null);
            this.gridHelper.AddColumn("MaintenanceMemo", "保养备注", null);
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["MaintenanceResult"].ReadOnly = false;
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["MaintenanceMemo"].ReadOnly = false;
            this.gridHelper.AddDefaultColumn(true, false);

            this.gridWebGrid.Columns.FromKey("MaintenanceType").Hidden = true;
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override void cmdQuery_Click(object sender, EventArgs e)
        {
            base.cmdQuery_Click(sender, e);
            foreach (GridRecord row in this.gridWebGrid.Rows)
            {
                row.CssClass = "CellBackColor";
            }
        }

        protected override DataRow GetGridRow(object obj)
        {
            //Infragistics.WebUI.UltraWebGrid.UltraGridRow row =  new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).Eqpid,
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).EqpDesc,								
            //                    this.getCycleString(((Domain.Equipment.EQPMaintenanceQuery)obj).CycleType),
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).Frequency,
            //                    this.languageComponent1.GetString(((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType),
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType,
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainITEM,
            //                    FormatHelper.ToDateString(((Domain.Equipment.EQPMaintenanceQuery)obj).LastMaintenanceDate), 
            //                    "true",
            //                    ((Domain.Equipment.EQPMaintenanceQuery)obj).MEMO
            //    });
            DataRow row = this.DtSource.NewRow();
            row["EQPID"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).Eqpid;
            row["EQPDESC"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).EqpDesc;
            row["Cycle"] = this.getCycleString(((Domain.Equipment.EQPMaintenanceQuery)obj).CycleType);
            row["Frequency"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).Frequency;
            row["MaintenanceTypeDesc"] = this.languageComponent1.GetString(((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType);
            row["MaintenanceType"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType;
            row["MaintenanceItem"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainITEM;
            row["LastMaintenanceDate"] = FormatHelper.ToDateString(((Domain.Equipment.EQPMaintenanceQuery)obj).LastMaintenanceDate);
            row["MaintenanceResult"] = true;
            row["MaintenanceMemo"] = ((Domain.Equipment.EQPMaintenanceQuery)obj).MEMO;

          //  row.Cells[10].Style.BackColor = Color.FromArgb(255, 252, 240);

            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblEQPIDQuery, this.txtEQPIDQuery, int.MaxValue, true));
            
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return null;
            }

            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPMaintenanceForAdd(
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

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }
        #endregion

        #region Button

        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {
            ArrayList GridCheckList = this.gridHelper.GetCheckedRows();
            if (GridCheckList.Count > 0)
            {
                if (_facade == null)
                {
                    _facade = new Material.EquipmentFacade(base.DataProvider);
                }
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                EQPMaintainLog _EQPMaintainLog = null;

                foreach (GridRecord row in GridCheckList)
                {
                    _EQPMaintainLog = new EQPMaintainLog();
                    _EQPMaintainLog.Eqpid = row.Items.FindItemByKey("EQPID").Text.ToString().Trim();
                    _EQPMaintainLog.MaintainITEM = row.Items.FindItemByKey("MaintenanceItem").Text.ToString().Trim();
                    _EQPMaintainLog.MaintainType = row.Items.FindItemByKey("MaintenanceType").Text.ToString().Trim();
                    _EQPMaintainLog.Result = row.Items.FindItemByKey("MaintenanceResult").Text == "true" ? "OK" : "NG";
                    _EQPMaintainLog.MEMO = row.Items.FindItemByKey("MaintenanceMemo").Text == null ? "" : FormatHelper.CleanString(row.Items.FindItemByKey("MaintenanceMemo").Text.ToString().Trim(), 400);
                    _EQPMaintainLog.Muser = this.GetUserCode();
                    _EQPMaintainLog.Mdate = currentDateTime.DBDate;
                    _EQPMaintainLog.Mtime = currentDateTime.DBTime;
                    _facade.AddEQPMaintainLog(_EQPMaintainLog);
 
                }
            }
            this.RequestData();
        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FEQPMaintainLog.aspx"));
        }

        #endregion


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

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  
                                ((Domain.Equipment.EQPMaintenanceQuery)obj).Eqpid,
								((Domain.Equipment.EQPMaintenanceQuery)obj).EqpDesc,								
                                this.getCycleString(((Domain.Equipment.EQPMaintenanceQuery)obj).CycleType),
                                ((Domain.Equipment.EQPMaintenanceQuery)obj).Frequency.ToString(),
                                this.languageComponent1.GetString(((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainType),
                                ((Domain.Equipment.EQPMaintenanceQuery)obj).MaintainITEM,
                                FormatHelper.ToDateString(((Domain.Equipment.EQPMaintenanceQuery)obj).LastMaintenanceDate)
                              
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
                                    "LastMaintenanceDate"};
        }

        #endregion
    }
}

