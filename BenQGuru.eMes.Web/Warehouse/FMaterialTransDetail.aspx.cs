using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using System.Collections.Generic;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FMaterialTransDetail : BaseMPage
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

        protected void Page_Load(object sender, EventArgs e)
        {
            InitOnPostBack();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitUI();
                this.InitWebGrid();
                this.GetParamsValue();
                this.RequestData();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void GetParamsValue()
        {
            ViewState["planDate"] = this.GetRequestParam("planDate");
            ViewState["bigSSCode"] = this.GetRequestParam("bigSSCode").ToString();
            ViewState["moSeq"] = this.GetRequestParam("moSeq").ToString();

            this.txtMOEdit.Text = this.GetRequestParam("moCode").ToString();
            this.txtItemCode.Text = this.GetRequestParam("itemCode").ToString();
            this.txtLactQty.Text = this.GetRequestParam("lactQty").ToString();
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelper(this.gridWebGrid);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
            this.cmdSaveTotal.Disabled = false;
        }

        #region WebGrid

        protected void InitWebGrid()
        {

            this.gridHelper.AddColumn("MaterialPlanDate", "计划日期", null);
            this.gridHelper.AddColumn("BIGSSCODE", "大线", null);
            this.gridHelper.AddColumn("PLANSEQ", "生产顺序", null);
            this.gridHelper.AddColumn("MOCODE", "工单", null);
            this.gridHelper.AddColumn("MOSEQ", "工单项次", null);
            this.gridHelper.AddColumn("PLANQTY", "计划投入产量", null);
            this.gridHelper.AddColumn("MaterialActQty", "实际投入产量", null);
            this.gridHelper.AddColumn("ACTIONSTATUS", "执行状态", null);
            this.gridHelper.AddColumn("MATERIALSTATUS", "配料状态", null);
            this.gridHelper.AddColumn("RemailQty", "物料余量", null);
            this.gridHelper.AddColumn("TransMaterialQTY", "移转数量", null);

            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridWebGrid.Columns.FromKey("TransMaterialQTY").CellStyle.BackColor = Color.FromArgb(255, 252, 220);
        }

        #endregion

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(base.DataProvider);
            }
            return this._facade.GetWorkPlanCountByItemCode(FormatHelper.CleanString(this.txtItemCode.Text.Trim().ToUpper()),
                                                            ViewState["bigSSCode"].ToString(),
                                                            Convert.ToInt32(ViewState["planDate"].ToString()),
                                                            this.txtMOEdit.Text.Trim().ToUpper(),
                                                            Convert.ToDecimal(ViewState["moSeq"].ToString()));
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(base.DataProvider);
            }

            return this._facade.QueryWorkPlanByItemCode(FormatHelper.CleanString(this.txtItemCode.Text.Trim().ToUpper()),
                                                        ViewState["bigSSCode"].ToString(),
                                                        Convert.ToInt32(ViewState["planDate"].ToString()),
                                                        this.txtMOEdit.Text.Trim().ToUpper(),
                                                        Convert.ToDecimal(ViewState["moSeq"].ToString()),
                                                        inclusive, exclusive);
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
                                "false",
								((WorkPlan)obj).PlanDate.ToString(),
								((WorkPlan)obj).BigSSCode.ToString(),
								((WorkPlan)obj).PlanSeq.ToString(),
                                ((WorkPlan)obj).MoCode.ToString(),
                                ((WorkPlan)obj).MoSeq.ToString(),
								((WorkPlan)obj).PlanQty.ToString(),
								((WorkPlan)obj).MaterialQty.ToString(),
                                this.languageComponent1.GetString(((WorkPlan)obj).ActionStatus.ToString()),
                                this.languageComponent1.GetString(((WorkPlan)obj).MaterialStatus.ToString()),
                                Convert.ToString(((WorkPlan)obj).MaterialQty-((WorkPlan)obj).ActQty),
                                Convert.ToString(((WorkPlan)obj).MaterialQty-((WorkPlan)obj).ActQty)
								});

        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FMaterialTrans.aspx", new string[] { }, new string[] { }));
        }

        protected void cmdSaveTotal_ServerClick(object sender, EventArgs e)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }

            if (this.gridHelper.GetCheckedRows().Count < 1)
            {
                WebInfoPublish.PublishInfo(this, "$CS_GRID_SELECT_ONE_RECORD", this.languageComponent1);
                return;
            }

            if (!CheckGridValue())
            {
                return;
            }

            ArrayList rowList = this.gridHelper.GetCheckedRows();
            decimal materialTransNumber = 0;
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            try
            {
                this.DataProvider.BeginTransaction();

                foreach (UltraGridRow row in rowList)
                {
                    //更新的oldWorkPlan的MaterialQty
                    WorkPlan oldWorkPlan = (WorkPlan)_facade.GetWorkPlan(row.Cells[2].Text.Trim(),
                                                               Convert.ToInt32(row.Cells[1].Text),
                                                               row.Cells[4].Text.Trim(),
                                                               Convert.ToDecimal(row.Cells[5].Text.Trim()));
                    if (oldWorkPlan != null)
                    {
                        oldWorkPlan.MaterialQty = oldWorkPlan.MaterialQty - Convert.ToDecimal(row.Cells[11].Text.Trim());
                    }

                    _facade.UpdateWorkPlan(oldWorkPlan);
                    //end

                    //新增转出Trans
                    MaterialIssue newOutMaterialIssue = new MaterialIssue();

                    newOutMaterialIssue.BigSSCode = row.Cells[2].Text.Trim();
                    newOutMaterialIssue.PlanDate = Convert.ToInt32(row.Cells[1].Text);
                    newOutMaterialIssue.MoCode = row.Cells[4].Text.Trim();
                    newOutMaterialIssue.MoSeq = Convert.ToDecimal(row.Cells[5].Text.Trim());
                    newOutMaterialIssue.IssueSEQ = _facade.GetMaterialIssueMaxIssueSEQ(newOutMaterialIssue.BigSSCode, newOutMaterialIssue.PlanDate,
                                                                                    newOutMaterialIssue.MoCode, newOutMaterialIssue.MoSeq);
                    newOutMaterialIssue.IssueQTY = Convert.ToDecimal(row.Cells[11].Text.Trim());
                    newOutMaterialIssue.IssueType = MaterialIssueType.MaterialIssueType_LineTransferOut;
                    newOutMaterialIssue.IssueStatus = MaterialIssueStatus.MaterialIssueStatus_Close;
                    newOutMaterialIssue.MaintainUser = this.GetUserCode();
                    newOutMaterialIssue.MaintainDate = dBDateTime.DBDate;
                    newOutMaterialIssue.MaintainTime = dBDateTime.DBTime;

                    _facade.AddMaterialIssue(newOutMaterialIssue);
                    //end

                    materialTransNumber += Convert.ToDecimal(row.Cells[11].Text.Trim());
                }

                //新增转入Trans
                MaterialIssue newInMaterialIssue = new MaterialIssue();

                newInMaterialIssue.BigSSCode = ViewState["bigSSCode"].ToString();
                newInMaterialIssue.PlanDate = Convert.ToInt32(ViewState["planDate"].ToString());
                newInMaterialIssue.MoCode = this.txtMOEdit.Text.Trim().ToUpper();
                newInMaterialIssue.MoSeq = Convert.ToDecimal(ViewState["moSeq"].ToString());
                newInMaterialIssue.IssueSEQ = _facade.GetMaterialIssueMaxIssueSEQ(newInMaterialIssue.BigSSCode, newInMaterialIssue.PlanDate,
                                                                                newInMaterialIssue.MoCode, newInMaterialIssue.MoSeq);
                newInMaterialIssue.IssueQTY = materialTransNumber;
                newInMaterialIssue.IssueType = MaterialIssueType.MaterialIssueType_LineTransferIn;
                newInMaterialIssue.IssueStatus = MaterialIssueStatus.MaterialIssueStatus_Close;
                newInMaterialIssue.MaintainUser = this.GetUserCode();
                newInMaterialIssue.MaintainDate = dBDateTime.DBDate;
                newInMaterialIssue.MaintainTime = dBDateTime.DBTime;

                _facade.AddMaterialIssue(newInMaterialIssue);
                //end

                //更新BigSSCode+MoCode所有的预警信息
                object[] workPlanObjects = _facade.QueryWorkPlan(ViewState["bigSSCode"].ToString(), this.txtMOEdit.Text.Trim().ToUpper());
                if (workPlanObjects != null)
                {
                    for (int j = 0; j < workPlanObjects.Length; j++)
                    {
                        WorkPlan workPlanUpdate = workPlanObjects[j] as WorkPlan;

                        //更新当前项次的数量
                        if (workPlanUpdate.PlanDate == Convert.ToInt32(ViewState["planDate"].ToString()) &&
                            workPlanUpdate.MoSeq == Convert.ToDecimal(ViewState["moSeq"].ToString()))
                        {
                            workPlanUpdate.MaterialQty += materialTransNumber;
                        }

                        workPlanUpdate.LastReceiveTime = dBDateTime.DBTime;
                        workPlanUpdate.LastReqTime = 0;
                        workPlanUpdate.PromiseTime = 0;
                        workPlanUpdate.MaterialStatus = MaterialWarningStatus.MaterialWarningStatus_No;

                        _facade.UpdateWorkPlan(workPlanUpdate);
                    }
                }

                _facade.UpdateMaterialReqInfo(ViewState["bigSSCode"].ToString(), this.txtMOEdit.Text.Trim().ToUpper());
                //end

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
            }

            this.RequestData();

        }

        private bool CheckGridValue()
        {
            decimal materialNumber = 0;
            ArrayList rowList = this.gridHelper.GetCheckedRows();
            foreach (UltraGridRow row in rowList)
            {
                if (string.IsNullOrEmpty(row.Cells[11].Text))
                {
                    WebInfoPublish.PublishInfo(this, "$ERROR_TransNumber_Cannot_Be_Empty", this.languageComponent1);
                    return false;
                }


                if (Convert.ToDecimal(row.Cells[11].Text.Trim()) < 0)
                {
                    WebInfoPublish.PublishInfo(this, "$ERROR_TransNumber_Must_Be_OVER_ZERRO", this.languageComponent1);
                    return false;
                }

                if (Convert.ToDecimal(row.Cells[10].Text.Trim()) < Convert.ToDecimal(row.Cells[11].Text.Trim()))
                {
                    WebInfoPublish.PublishInfo(this, "$ERROR_TransNumber_Cannot_Bigger_RemainNumber", this.languageComponent1);
                    return false;
                }

                materialNumber += Convert.ToDecimal(row.Cells[11].Text.Trim());
            }

            if (materialNumber > Convert.ToDecimal(this.txtLactQty.Text.Trim()))
            {
                WebInfoPublish.PublishInfo(this, "$ERROR_TransNumber_Cannot_Bigger_LackNumber", this.languageComponent1);
                return false;
            }

            return true;
        }


        #region export
        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"MaterialPlanDate",
									"BIGSSCODE",	
									"PLANSEQ",
				                    "MOCODE",
									"MOSEQ",	
									"PLANQTY",
				                    "MaterialActQty",
									"ACTIONSTATUS",	
									"MATERIALSTATUS",
                                    "REQUESTQTY"                                   
            };
        }

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{ ((WorkPlan)obj).PlanDate.ToString(),
								((WorkPlan)obj).BigSSCode.ToString(),
								((WorkPlan)obj).PlanSeq.ToString(),
                                ((WorkPlan)obj).MoCode.ToString(),
                                ((WorkPlan)obj).MoSeq.ToString(),
								((WorkPlan)obj).PlanQty.ToString(),
								((WorkPlan)obj).MaterialQty.ToString(),
                                this.languageComponent1.GetString(((WorkPlan)obj).ActionStatus.ToString()),
                                this.languageComponent1.GetString(((WorkPlan)obj).MaterialStatus.ToString()),
                                Convert.ToString(((WorkPlan)obj).MaterialQty-((WorkPlan)obj).ActQty)                                              
            };
        }

        #endregion
    }
}
