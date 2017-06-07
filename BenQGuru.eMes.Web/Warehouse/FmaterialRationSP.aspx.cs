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
    public partial class FmaterialRationSP : BaseMPage
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
            InitOnPostBack();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitWebGrid();
                this.GetPageValues();
                this.RequestData();
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
            this.txtRationNumberEdit.Text = string.Empty;
        }

        private void GetPageValues()
        {
            this.txtPlanDateFromQuery.Text = this.GetRequestParam("planDate");
            this.txtBigSSCodeGroupQuery.Text = this.GetRequestParam("bigSSCode");
            this.txtMoQuery.Text = this.GetRequestParam("moCode");
            this.txtMOSeqQuery.Text = this.GetRequestParam("moSeq");
        }


        #endregion

        #region WebGrid
        protected  void InitWebGrid()
        {
            this.gridHelper.AddColumn("IssueSEQ", "发料序号", null);
            this.gridHelper.AddColumn("IssueNumber", "数量", null);
            this.gridHelper.AddColumn("IssueUser", "配料人员", null);
            this.gridHelper.AddColumn("IssueDate", "配料日期", null);
            this.gridHelper.AddColumn("IssueTime", "配料时间", null);
            this.gridHelper.AddColumn("IssueStatus", "状态", null);                    

            this.gridHelper.AddDefaultColumn(true, false);
            this.gridWebGrid.Columns.FromKey("IssueStatus").Width = 100;
 
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
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

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
								((MaterialIssue)obj).IssueSEQ.ToString(),
								((MaterialIssue)obj).IssueQTY.ToString() ,
								((MaterialIssue)obj).MaintainUser.ToString(),
								 FormatHelper.ToDateString(((MaterialIssue)obj).MaintainDate),
                                 FormatHelper.ToTimeString(((MaterialIssue)obj).MaintainTime),
                                this.languageComponent1.GetString(((MaterialIssue)obj).IssueStatus.ToString()),
                                ""
                            });
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }

            return this._facade.QueryMaterialIssue(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroupQuery.Text)),
                FormatHelper.TODateInt(this.txtPlanDateFromQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoQuery.Text)),
                Convert.ToDecimal(this.txtMOSeqQuery.Text.Trim()),
                MaterialIssueType.MaterialIssueType_Issue,
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }
            return this._facade.GetMaterialIssueCount(
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroupQuery.Text)),
                FormatHelper.TODateInt(this.txtPlanDateFromQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoQuery.Text)),
                Convert.ToDecimal(this.txtMOSeqQuery.Text.Trim()),
                MaterialIssueType.MaterialIssueType_Issue
                );
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            MaterialIssue materialIssue = domainObject as MaterialIssue;

            this._facade.AddMaterialIssue(materialIssue);
            WorkPlan workPlan = (WorkPlan)_facade.GetWorkPlan(materialIssue.BigSSCode, materialIssue.PlanDate, materialIssue.MoCode, materialIssue.MoSeq);

            if (workPlan != null)
            {
                workPlan.MaterialStatus = MaterialWarningStatus.MaterialWarningStatus_Responsed;
                workPlan.PromiseTime = dBDateTime.DBTime;
                _facade.UpdateWorkPlan(workPlan);
            }
            this.txtRationNumberEdit.Text=string.Empty;
        }

        protected  override void cmdDelete_Click(object sender, EventArgs e)
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }

            ArrayList rowList = this.gridHelper.GetCheckedRows();
            List<MaterialIssue> materialIssueList = new List<MaterialIssue>();

            foreach (UltraGridRow row in rowList)
            {
                if (row.Cells[6].Text == this.languageComponent1.GetString(MaterialIssueStatus.MaterialIssueStatus_Close))
                {
                    WebInfoPublish.PublishInfo(this, "$Close_Cannot_Delete", this.languageComponent1);
                    return;
                }
                MaterialIssue materialIssue =(MaterialIssue) _facade.GetMaterialIssue(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroupQuery.Text)),
                                                                        FormatHelper.TODateInt(this.txtPlanDateFromQuery.Text),
                                                                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoQuery.Text)),
                                                                        Convert.ToDecimal(this.txtMOSeqQuery.Text.Trim()),
                                                                        Convert.ToDecimal(row.Cells[1].Text));
                if (materialIssue!=null)
                {
                    materialIssueList.Add(materialIssue);
                }

            }

            if (materialIssueList.Count>0)
            {
                this._facade.DeleteMaterialIssue((MaterialIssue[])materialIssueList.ToArray());
            }            

            this.RequestData();
        }



        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FmaterialRation.aspx", new string[] { }, new string[] { }));
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new MaterialFacade(this.DataProvider);
            }
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            MaterialIssue materialIssue = this._facade.CreateNewMaterialIssue();

            materialIssue.PlanDate = FormatHelper.TODateInt(this.txtPlanDateFromQuery.Text);
            materialIssue.BigSSCode = FormatHelper.CleanString(this.txtBigSSCodeGroupQuery.Text.Trim().ToUpper());
            materialIssue.MoCode = FormatHelper.CleanString(this.txtMoQuery.Text.Trim().ToUpper());
            materialIssue.MoSeq = Convert.ToDecimal(this.txtMOSeqQuery.Text.Trim());
            materialIssue.IssueSEQ = _facade.GetMaterialIssueMaxIssueSEQ(materialIssue.BigSSCode, materialIssue.PlanDate, materialIssue.MoCode, materialIssue.MoSeq);
            materialIssue.IssueQTY = Convert.ToDecimal(this.txtRationNumberEdit.Text);
            materialIssue.IssueType = MaterialIssueType.MaterialIssueType_Issue;
            materialIssue.IssueStatus = MaterialIssueStatus.MaterialIssueStatus_Delivered;
            materialIssue.MaintainUser = this.GetUserCode();
            materialIssue.MaintainDate = dBDateTime.DBDate;
            materialIssue.MaintainTime = dBDateTime.DBTime;

            return materialIssue;
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DecimalCheck(this.lblRationNumberEdit, this.txtRationNumberEdit, 0, Convert.ToDecimal(99999999.99), true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }


        #endregion

        #region 数据初始化


        #endregion

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  this.txtPlanDateFromQuery.Text.Trim(),
                                this.txtBigSSCodeGroupQuery.Text.Trim(),
                                this.txtMoQuery.Text,
                                this.txtMOSeqQuery.Text,
                                ((MaterialIssue)obj).IssueSEQ.ToString(),
								((MaterialIssue)obj).IssueQTY.ToString() ,
								((MaterialIssue)obj).MaintainUser.ToString(),
								 FormatHelper.ToDateString(((MaterialIssue)obj).MaintainDate),
                                 FormatHelper.ToTimeString(((MaterialIssue)obj).MaintainTime),
                                this.languageComponent1.GetString(((MaterialIssue)obj).IssueStatus.ToString()) };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"MaterialPlanDate",
									"BigSSCode",									
									"MoCode",
									"MoSeq",
                                    "IssueSEQ",
									"IssueNumber",
									"IssueUser",
									"IssueDate",
									"IssueTime",
                                    "IssueStatus"};
        }

        #endregion





    }
}
