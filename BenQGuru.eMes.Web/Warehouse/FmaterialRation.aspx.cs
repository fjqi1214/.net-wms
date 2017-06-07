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
using System.Collections.Generic;

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FRouteMP 的摘要说明。
    /// </summary>
    public partial class FmaterialRation : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.Material.MaterialFacade _facade = null;

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
                this.datePlanDateFromQuery.Date_DateTime = DateTime.Now;
                this.datePlanDateToQuery.Date_DateTime = DateTime.Now.AddMonths(1);
                this.timePromiseTimeEdit.TimeString = string.Empty;
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
            this.txtBigSSCodeGroupEdit.Text = string.Empty;
            this.txtdateDateEdit.Text = string.Empty;
            this.txtMOCodeGroupEdit.Text = string.Empty;
            this.txtMOSeqEdit.Text = string.Empty;
            this.timePromiseTimeEdit.TimeString = string.Empty;
            this.cmdSave.Disabled = true;
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("MaterialPlanDate", "计划日期", null);
            this.gridHelper.AddColumn("BigSSCode", "大线", null);
            this.gridHelper.AddColumn("PlanSeq", "生产顺序", null);
            this.gridHelper.AddColumn("MoCode", "工单", null);
            this.gridHelper.AddColumn("MoSeq", "工单项次", null);
            this.gridHelper.AddColumn("MaterialCode", "物料代码", null);
            this.gridHelper.AddColumn("MModelCode", "机型", null);
            this.gridHelper.AddColumn("PlanStartTime", "计划开始时间", null);
            this.gridHelper.AddColumn("MaterialPlanQty", "计划投入产量", null);
            this.gridHelper.AddColumn("MaterialActQty", "实际投入产量", null);
            this.gridHelper.AddColumn("ActionStatus", "执行状态", null);
            this.gridHelper.AddColumn("MaterialStatus", "配料状态", null);
            this.gridHelper.AddColumn("LastReceiveTime", "上次接收时间", null);
            this.gridHelper.AddColumn("LastReqTime", "预警时间", null);
            this.gridHelper.AddColumn("PromiseTime", "承诺配送时间", null);
            this.gridHelper.AddLinkColumn("SuitEdit", "配送维护", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
								FormatHelper.ToDateString(((WorkPlanWithQty)obj).PlanDate),
								((WorkPlanWithQty)obj).BigSSCode.ToString(),
								((WorkPlanWithQty)obj).PlanSeq.ToString() ,
								((WorkPlanWithQty)obj).MoCode.ToString(),
								((WorkPlanWithQty)obj).MoSeq.ToString(),
                                ((WorkPlanWithQty)obj).ItemCode.ToString(),
                                ((WorkPlanWithQty)obj).MaterialModelCode.ToString(),
								FormatHelper.ToTimeString(((WorkPlanWithQty)obj).PlanStartTime),
								((WorkPlanWithQty)obj).PlanQty.ToString(),
								((WorkPlanWithQty)obj).ActQty.ToString(),
								this.languageComponent1.GetString(((WorkPlanWithQty)obj).ActionStatus.ToString()),
                                this.languageComponent1.GetString(((WorkPlanWithQty)obj).MaterialStatus.ToString()),
                                FormatHelper.ToTimeString(((WorkPlanWithQty)obj).LastReceiveTime),
                                FormatHelper.ToTimeString(((WorkPlanWithQty)obj).LastReqTime),
                                FormatHelper.ToTimeString(((WorkPlanWithQty)obj).PromiseTime),
								""});
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }
            if (!QuerydateInput())
            {
                return null;
            }

            return this._facade.QueryWorkPlan(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroupQuery.Text)),
                FormatHelper.TODateInt(this.datePlanDateFromQuery.Text),
                FormatHelper.TODateInt(this.datePlanDateToQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoQuery.Text)),
                this.drpActionStatusQuery.SelectedValue,
                this.drpMaterialStatusQuery.SelectedValue,
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }
            return this._facade.QueryWorkPlanCount(
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroupQuery.Text)),
                FormatHelper.TODateInt(this.datePlanDateFromQuery.Text),
                FormatHelper.TODateInt(this.datePlanDateToQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoQuery.Text)),
                this.drpActionStatusQuery.SelectedValue,
                this.drpMaterialStatusQuery.SelectedValue
                );
        }

        #endregion

        #region Button

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }
            this._facade.DeleteWorkPlan((WorkPlan[])domainObjects.ToArray(typeof(WorkPlan)));
            this.RequestData();
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }

            WorkPlan workPlan = domainObject as WorkPlan;

            workPlan.MaterialStatus = MaterialWarningStatus.MaterialWarningStatus_Responsed;

            this._facade.UpdateWorkPlan(workPlan);
            this.RequestData();
        }


        protected void cmdOKSuit_ServerClick(object sender, EventArgs e)
        {
            this.DoSuitInButton(WorkPlanActionStatus.WorkPlanActionStatus_Init);
            RequestData();
        }

        protected void cmdCancelSuit_ServerClick(object sender, EventArgs e)
        {
            this.DoSuitInButton(WorkPlanActionStatus.WorkPlanActionStatus_Ready);
            RequestData();
        }

        protected override void Grid_ClickCell(UltraGridCell cell)
        {
            base.Grid_ClickCell(cell);
            if (this.gridHelper.IsClickColumn("SuitEdit", cell))
            {
                this.Response.Redirect(this.MakeRedirectUrl("FmaterialRationSP.aspx", new string[] { "planDate", "bigSSCode", "moCode", "moSeq" }, new string[] { cell.Row.Cells.FromKey("MaterialPlanDate").Text, cell.Row.Cells.FromKey("BigSSCode").Text, cell.Row.Cells.FromKey("MoCode").Text, cell.Row.Cells.FromKey("MoSeq").Text }));
            }
            else if (this.gridHelper.IsClickColumn("Edit", cell))
            {
                if (cell.Row.Cells.FromKey("MaterialStatus").Text == this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_No) ||
                    cell.Row.Cells.FromKey("MaterialStatus").Text == this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_Responsed))
                {
                    WebInfoPublish.Publish(this, "$Error_OnlydeliveryAndlackStatusCanDo", this.languageComponent1);
                    return;
                }
                this.SetEditObject(cell.Row);
            }
        }

        protected override void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
            this.RequestData();
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }
            WorkPlan workPlan = this._facade.CreateNewWorkPlan();

            workPlan.PlanDate = FormatHelper.TODateInt(this.txtdateDateEdit.Text);
            workPlan.BigSSCode = FormatHelper.CleanString(this.txtBigSSCodeGroupEdit.Text.Trim().ToUpper());
            workPlan.MoCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeGroupEdit.Text, 40));
            workPlan.MoSeq = Convert.ToDecimal(FormatHelper.CleanString(this.txtMOSeqEdit.Text.Trim().ToUpper()));

            workPlan =(WorkPlan)_facade.GetWorkPlan(workPlan.BigSSCode, workPlan.PlanDate, workPlan.MoCode, workPlan.MoSeq);
            if (workPlan == null)
            {
                return null;
            }

            if (this.timePromiseTimeEdit.Text.Trim().Length > 0)
            {
                workPlan.PromiseTime = FormatHelper.TOTimeInt(this.timePromiseTimeEdit.Text);
            }

            return workPlan;
        }


        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }
            object obj = _facade.GetWorkPlan(row.Cells.FromKey("BigSSCode").Text.ToString(),
                                            FormatHelper.TODateInt(row.Cells.FromKey("MaterialPlanDate").Text.ToString()),
                                            row.Cells.FromKey("MoCode").Text.ToString().Trim(),
                                            Convert.ToDecimal(row.Cells.FromKey("MoSeq").Text.ToString())
                                            );

            if (obj != null)
            {
                return (WorkPlan)obj;
            }

            return null;
        }

        protected void SetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            this.txtdateDateEdit.Text = Convert.ToDateTime(row.Cells.FromKey("MaterialPlanDate").Text.ToString()).ToString("yyyy-MM-dd");
            this.txtBigSSCodeGroupEdit.Text = row.Cells.FromKey("BigSSCode").Text;
            this.txtMOSeqEdit.Text = row.Cells.FromKey("MoSeq").Text;
            this.txtMOCodeGroupEdit.Text = row.Cells.FromKey("MoCode").Text;
            this.timePromiseTimeEdit.TimeString = row.Cells.FromKey("PromiseTime").Text;
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DateCheck(this.lblPromiseTimeEdit, this.timePromiseTimeEdit.Text, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        protected bool QuerydateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DateRangeCheck(this.lblPlanDateFrom, this.datePlanDateFromQuery.Text, this.lblTo, this.datePlanDateToQuery.Text, true));


            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        private void DoSuitInButton(string workPlanActionStatus)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }

            ArrayList arryList = this.gridHelper.GetCheckedRows();
            if (arryList.Count == 0)
            {
                WebInfoPublish.Publish(this, "$CS_CHOOSE_ONE_RECORD_AT_LEAST", languageComponent1);
                return;
            }

            List<WorkPlan> workPlanList = new List<WorkPlan>();

            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            foreach (UltraGridRow row in arryList)
            {
                if (row.Cells.FromKey("ActionStatus").Text == this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Close) ||
                    row.Cells.FromKey("ActionStatus").Text == this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Open))
                {
                    WebInfoPublish.Publish(this, "$WorkPlanActionStatus_Not_Right", languageComponent1);
                    return;
                }

                if (row.Cells.FromKey("ActionStatus").Text.Trim() == this.languageComponent1.GetString(workPlanActionStatus))
                {
                    WorkPlan workPlan = (WorkPlan)_facade.GetWorkPlan(row.Cells.FromKey("BigSSCode").Text,
                                                            FormatHelper.TODateInt(Convert.ToDateTime(row.Cells.FromKey("MaterialPlanDate").Text)),
                                                            row.Cells.FromKey("MoCode").Text,
                                                            Convert.ToDecimal(row.Cells.FromKey("MoSeq").Text.Trim()));
                    if (workPlan != null)
                    {
                        if (workPlanActionStatus == WorkPlanActionStatus.WorkPlanActionStatus_Ready)
                        {
                            workPlan.ActionStatus = WorkPlanActionStatus.WorkPlanActionStatus_Init;
                        }
                        else
                        {
                            workPlan.ActionStatus = WorkPlanActionStatus.WorkPlanActionStatus_Ready;
                        }

                        workPlan.MaintainDate = dBDateTime.DBDate;
                        workPlan.MaintainTime = dBDateTime.DBTime;
                        workPlanList.Add(workPlan);
                    }
                }
            }

            if (workPlanList.Count > 0)
            {
                try
                {
                    this.DataProvider.BeginTransaction();

                    for (int i = 0; i < workPlanList.Count; i++)
                    {
                        WorkPlan workPlanToUpdate = workPlanList[i] as WorkPlan;
                        _facade.UpdateWorkPlan(workPlanToUpdate);
                    }

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
                }

            }
        }
        #endregion

        #region 数据初始化

        protected void drpActionStatusQuery_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpActionStatusQuery.Items.Clear();
                this.drpActionStatusQuery.Items.Add(new ListItem("", ""));

                this.drpActionStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Init), WorkPlanActionStatus.WorkPlanActionStatus_Init));
                this.drpActionStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Ready), WorkPlanActionStatus.WorkPlanActionStatus_Ready));
                this.drpActionStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Open), WorkPlanActionStatus.WorkPlanActionStatus_Open));
                this.drpActionStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Close), WorkPlanActionStatus.WorkPlanActionStatus_Close));

                this.drpActionStatusQuery.SelectedIndex = 0;
            }
        }

        protected void drpMaterialStatusQuery_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpMaterialStatusQuery.Items.Clear();
                this.drpMaterialStatusQuery.Items.Add(new ListItem("", ""));

                this.drpMaterialStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_No), MaterialWarningStatus.MaterialWarningStatus_No));
                this.drpMaterialStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_Delivery), MaterialWarningStatus.MaterialWarningStatus_Delivery));
                this.drpMaterialStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_Responsed), MaterialWarningStatus.MaterialWarningStatus_Responsed));
                this.drpMaterialStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_Lack), MaterialWarningStatus.MaterialWarningStatus_Lack));

                this.drpActionStatusQuery.SelectedIndex = 0;
            }

        }
        #endregion

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  FormatHelper.ToDateString(((WorkPlanWithQty)obj).PlanDate),
								((WorkPlanWithQty)obj).BigSSCode.ToString(),
								((WorkPlanWithQty)obj).PlanSeq.ToString() ,
								((WorkPlanWithQty)obj).MoCode.ToString(),
								((WorkPlanWithQty)obj).MoSeq.ToString(),
                                ((WorkPlanWithQty)obj).ItemCode.ToString(),
                                ((WorkPlanWithQty)obj).MaterialModelCode.ToString(),
								FormatHelper.ToTimeString(((WorkPlanWithQty)obj).PlanStartTime),
								((WorkPlanWithQty)obj).PlanQty.ToString(),
								((WorkPlanWithQty)obj).ActQty.ToString(),
								this.languageComponent1.GetString(((WorkPlanWithQty)obj).ActionStatus.ToString()),
                                this.languageComponent1.GetString(((WorkPlanWithQty)obj).MaterialStatus.ToString()),
                                FormatHelper.ToTimeString(((WorkPlanWithQty)obj).LastReceiveTime),
                                FormatHelper.ToTimeString(((WorkPlanWithQty)obj).LastReqTime),
                                FormatHelper.ToTimeString(((WorkPlanWithQty)obj).PromiseTime) };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"MaterialPlanDate",
									"BigSSCode",
									"PlanSeq",
									"MoCode",
									"MoSeq",
                                     "MaterialCode",
                                    "MModelCode",
                                    "PlanStartTime",
                                    "MaterialPlanQty",
                                    "MaterialActQty",
                                    "ActionStatus",
                                    "MaterialStatus",
                                    "LastReceiveTime",
                                    "LastReqTime",
                                    "PromiseTime"};
        }

        #endregion

    }
}
