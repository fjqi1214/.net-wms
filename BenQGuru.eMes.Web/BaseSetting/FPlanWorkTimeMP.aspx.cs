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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FShiftMP 的摘要说明。
    /// </summary>
    public partial class FPlanWorkTimeMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private PerformanceFacade _facade = null; 
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
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ItemDescription", "产品描述", null);
            this.gridHelper.AddColumn("sscode", "产线代码", null);
            this.gridHelper.AddColumn("StepSequenceDescription", "生产线描述", null);
            this.gridHelper.AddColumn("BigLine", "大线", null);
            this.gridHelper.AddColumn("CycleTime", "CycleTime", null);
            this.gridHelper.AddColumn("WorkingTime", "排产工时", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
;
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((PlanWorkTimeWithMessage)obj).ItemCode.ToString(),
            //                    ((PlanWorkTimeWithMessage)obj).ItemDescription.ToString(),
            //                    ((PlanWorkTimeWithMessage)obj).SSCode.ToString(),
            //                    ((PlanWorkTimeWithMessage)obj).StepSequenceDescription.ToString(),
            //                    ((PlanWorkTimeWithMessage)obj).BigSSCode.ToString(),
            //                    ((PlanWorkTimeWithMessage)obj).CycleTime.ToString(),								
            //                    ((PlanWorkTimeWithMessage)obj).WorkingTime.ToString(),	
            //                    ((PlanWorkTimeWithMessage)obj).MaintainUser.ToString(),	
            //                    FormatHelper.ToDateString(((PlanWorkTimeWithMessage)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((PlanWorkTimeWithMessage)obj).MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["ItemCode"] = ((PlanWorkTimeWithMessage)obj).ItemCode.ToString();
            row["ItemDescription"] = ((PlanWorkTimeWithMessage)obj).ItemDescription.ToString();
            row["sscode"] = ((PlanWorkTimeWithMessage)obj).SSCode.ToString();
            row["StepSequenceDescription"] = ((PlanWorkTimeWithMessage)obj).StepSequenceDescription.ToString();
            row["BigLine"] = ((PlanWorkTimeWithMessage)obj).BigSSCode.ToString();
            row["CycleTime"] = ((PlanWorkTimeWithMessage)obj).CycleTime.ToString();
            row["WorkingTime"] = ((PlanWorkTimeWithMessage)obj).WorkingTime.ToString();
            row["MaintainUser"] = ((PlanWorkTimeWithMessage)obj).MaintainUser.ToString();
            row["MaintainDate"] = FormatHelper.ToDateString(((PlanWorkTimeWithMessage)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((PlanWorkTimeWithMessage)obj).MaintainTime);
            return row;

        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            return this._facade.QueryPlanWorkTime(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroup.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            return this._facade.GetPlanWorkTimeCount(
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroup.Text)));
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            
            PlanWorkTime planWorkTime=(PlanWorkTime)_facade.GetPlanWorkTime(FormatHelper.CleanString(this.txtItemCodeEdit.Text.ToUpper()),
                                                                FormatHelper.CleanString(this.txtSSEdit.Text.ToUpper()));
            if (planWorkTime!=null)
            {
                WebInfoPublish.Publish(this, "$The_Same_ItemAndSSCode_Is_Exist", this.languageComponent1);
                return;
            }
            this._facade.AddPlanWorkTime((PlanWorkTime)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            this._facade.DeletePlanWorkTime((PlanWorkTime[])domainObjects.ToArray(typeof(PlanWorkTime)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            this._facade.UpdatePlanWorkTime((PlanWorkTime)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtItemCodeEdit.Readonly = false;
                this.txtSSEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtItemCodeEdit.Readonly = true;
                this.txtSSEdit.ReadOnly = true;
            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            PlanWorkTime planWorkTime = this._facade.CreateNewPlanWorkTime();

            planWorkTime.ItemCode = FormatHelper.CleanString(this.txtItemCodeEdit.Text.ToUpper(), 40);
            planWorkTime.SSCode = FormatHelper.CleanString(this.txtSSEdit.Text.ToUpper(),40);
            planWorkTime.CycleTime = int.Parse(this.txtCycletimeEdit.Text.ToUpper());
            planWorkTime.WorkingTime = int.Parse(this.txtWorkingTimeEdit.Text.ToUpper());
            planWorkTime.MaintainUser = this.GetUserCode();
            planWorkTime.MaintainDate = dbDateTime.DBDate;
            planWorkTime.MaintainTime = dbDateTime.DBTime;
            return planWorkTime;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            object obj = _facade.GetPlanWorkTime(row.Items.FindItemByKey("ItemCode").Text.ToString(), row.Items.FindItemByKey("sscode").Text.ToString());

            if (obj != null)
            {
                return obj as PlanWorkTime;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtItemCodeEdit.Text = string.Empty;
                this.txtSSEdit.Text = string.Empty;
                this.txtWorkingTimeEdit.Text = string.Empty;
                this.txtCycletimeEdit.Text = string.Empty;

                return;
            }

            this.txtItemCodeEdit.Text = ((PlanWorkTime)obj).ItemCode.ToString();
            this.txtSSEdit.Text = ((PlanWorkTime)obj).SSCode;
            this.txtWorkingTimeEdit.Text = ((PlanWorkTime)obj).WorkingTime.ToString();
            this.txtCycletimeEdit.Text = ((PlanWorkTime)obj).CycleTime.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblItemCodeEdit, this.txtItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblSSEdit, txtSSEdit,40, true));
            manager.Add(new NumberCheck(lblWorkingTimeEdit, txtWorkingTimeEdit, 0, 9999999999, true));
            manager.Add(new NumberCheck(lblCycletimeEdit, txtCycletimeEdit, 0, 9999999999, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (Convert.ToInt32(this.txtWorkingTimeEdit.Text.Trim()) <= 0)
            {
                WebInfoPublish.Publish(this, "$WorkingTime_Must_Over_Zero", this.languageComponent1);
                return false;
            }

            if (Convert.ToInt32(this.txtCycletimeEdit.Text.Trim()) <= 0)
            {
                WebInfoPublish.Publish(this, "$Cycletime_Must_Over_Zero", this.languageComponent1);
                return false;
            }

            ItemFacade itemFacade = new ItemFacade(this.DataProvider);

            object itemObject = itemFacade.GetItem(FormatHelper.CleanString(this.txtItemCodeEdit.Text.ToUpper()), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (itemObject == null)
            {
                WebInfoPublish.Publish(this, "$Error_ItemCode_NotExist", this.languageComponent1);
                return false;
            }
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);

            object stepSequenceObject = baseModelFacade.GetStepSequence(FormatHelper.CleanString(this.txtSSEdit.Text.ToUpper()));

            if (stepSequenceObject == null)
            {
                WebInfoPublish.Publish(this, "$CS_STEPSEQUENCE_NOT_EXIST", this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((PlanWorkTimeWithMessage)obj).ItemCode.ToString(),
                                    ((PlanWorkTimeWithMessage)obj).ItemDescription.ToString(),
								   ((PlanWorkTimeWithMessage)obj).SSCode.ToString(),
                                    ((PlanWorkTimeWithMessage)obj).StepSequenceDescription.ToString(),
								   ((PlanWorkTimeWithMessage)obj).BigSSCode.ToString(),
								   ((PlanWorkTimeWithMessage)obj).CycleTime.ToString(),
                                   ((PlanWorkTimeWithMessage)obj).WorkingTime.ToString(),
                                   ((PlanWorkTimeWithMessage)obj).MaintainUser.ToString(),
								   FormatHelper.ToTimeString(((PlanWorkTimeWithMessage)obj).MaintainDate),
								   FormatHelper.ToTimeString(((PlanWorkTimeWithMessage)obj).MaintainTime)
                                }   ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
									"ItemCode",
                                    "ItemDescription",
									"SSCode",
                                    "StepSequenceDescription",
									"BigLine",
									"CycleTime",
									"WorkingTime",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"};
        }
        #endregion

        #region Import

        protected void cmdImport_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("../ImportData/FExcelDataImp.aspx?ImportType=PlanWorkTime"));
        }

        #endregion
    }
}
