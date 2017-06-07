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
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FShiftMP 的摘要说明。
    /// </summary>
    public partial class FLine2CrewMP : BaseMPageNew
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
            this.gridHelper.AddColumn("Date", "日期", null);
            this.gridHelper.AddColumn("sscode", "产线代码", null);
            this.gridHelper.AddColumn("StepSequenceDescription", "生产线描述", null);
            this.gridHelper.AddColumn("ShiftCode", "班次代码", null);
            this.gridHelper.AddColumn("ShiftDescription", "班次描述", null);
            this.gridHelper.AddColumn("CrewCode", "班组代码", null);
            this.gridHelper.AddColumn("CrewDesc", "班组代码", null);
            this.gridHelper.AddColumn("BigLine", "大线", null);
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
            //                    FormatHelper.ToDateString(((Line2CrewWithMessage)obj).ShiftDate),
            //                    ((Line2CrewWithMessage)obj).SSCode.ToString(),
            //                    ((Line2CrewWithMessage)obj).StepSequenceDescription.ToString(),
            //                    ((Line2CrewWithMessage)obj).ShiftCode.ToString(),
            //                    ((Line2CrewWithMessage)obj).ShiftDescription.ToString(),
            //                    ((Line2CrewWithMessage)obj).CrewCode.ToString(),
            //                    ((Line2CrewWithMessage)obj).CrewDesc.ToString(),			
            //                    ((Line2CrewWithMessage)obj).BigSSCode.ToString(),	
            //                    ((Line2CrewWithMessage)obj).MaintainUser.ToString(),	
            //                    FormatHelper.ToDateString(((Line2CrewWithMessage)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((Line2CrewWithMessage)obj).MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["Date"] = FormatHelper.ToDateString(((Line2CrewWithMessage)obj).ShiftDate);
            row["sscode"] = ((Line2CrewWithMessage)obj).SSCode.ToString();
            row["StepSequenceDescription"] = ((Line2CrewWithMessage)obj).StepSequenceDescription.ToString();
            row["ShiftCode"] = ((Line2CrewWithMessage)obj).ShiftCode.ToString();
            row["ShiftDescription"] = ((Line2CrewWithMessage)obj).ShiftDescription.ToString();
            row["CrewCode"] = ((Line2CrewWithMessage)obj).CrewCode.ToString();
            row["CrewDesc"] = ((Line2CrewWithMessage)obj).CrewDesc.ToString();
            row["BigLine"] = ((Line2CrewWithMessage)obj).BigSSCode.ToString();
            row["MaintainUser"] = ((Line2CrewWithMessage)obj).MaintainUser.ToString();
            row["MaintainDate"] = FormatHelper.ToDateString(((Line2CrewWithMessage)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Line2CrewWithMessage)obj).MaintainTime);
            return row;

        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            return this._facade.QueryLine2Crew(
                FormatHelper.TODateInt(this.DateQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCrewCodeQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            return this._facade.GetLine2CrewCount(
                 FormatHelper.TODateInt(this.DateQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCrewCodeQuery.Text)));
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }

            Line2Crew line2Crew = (Line2Crew)_facade.GetLine2Crew(FormatHelper.TODateInt(this.DateEdit.Text),
                                                                  FormatHelper.CleanString(this.txtSSEdit.Text.ToUpper()),
                                                                  FormatHelper.CleanString(this.txtShiftCodeEdit.Text.ToUpper()));
            if (line2Crew != null)
            {
                WebInfoPublish.Publish(this, "$The_Same_DateAndSSCodeAndSHiftCode_Is_Exist", this.languageComponent1);
                return;
            }
            this._facade.AddLine2Crew((Line2Crew)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            this._facade.DeleteLine2Crew((Line2Crew[])domainObjects.ToArray(typeof(Line2Crew)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            this._facade.UpdateLine2Crew((Line2Crew)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.DateEdit.ReadOnly = false;
                this.DateEdit.Enabled = true;
                this.txtSSEdit.ReadOnly = false;
                this.txtShiftCodeEdit.Readonly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.DateEdit.ReadOnly = true;
                this.DateEdit.Enabled = false;
                this.txtSSEdit.ReadOnly = true;
                this.txtShiftCodeEdit.Readonly = true;
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
            Line2Crew line2Crew = this._facade.CreateNewLine2Crew();

            line2Crew.ShiftDate = FormatHelper.TODateInt(this.DateEdit.Text);
            line2Crew.SSCode = FormatHelper.CleanString(this.txtSSEdit.Text.ToUpper(), 40);
            line2Crew.ShiftCode = FormatHelper.CleanString(this.txtShiftCodeEdit.Text.ToUpper());
            line2Crew.CrewCode = FormatHelper.CleanString(this.txtCrewCodeEdit.Text.ToUpper());
            line2Crew.MaintainUser = this.GetUserCode();
            line2Crew.MaintainDate = dbDateTime.DBDate;
            line2Crew.MaintainTime = dbDateTime.DBTime;
            return line2Crew;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new PerformanceFacade(base.DataProvider);
            }
            object obj = _facade.GetLine2Crew(FormatHelper.TODateInt(row.Items.FindItemByKey("Date").Text.ToString()), row.Items.FindItemByKey("sscode").Text.ToString(), row.Items.FindItemByKey("ShiftCode").Text.ToString());

            if (obj != null)
            {
                return obj as Line2Crew;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.DateEdit.Text = string.Empty;
                this.txtSSEdit.Text = string.Empty;
                this.txtShiftCodeEdit.Text = string.Empty;
                this.txtCrewCodeEdit.Text = string.Empty;

                return;
            }

            this.DateEdit.Text = FormatHelper.ToDateString(((Line2Crew)obj).ShiftDate); 
            this.txtSSEdit.Text = ((Line2Crew)obj).SSCode;
            this.txtShiftCodeEdit.Text = ((Line2Crew)obj).ShiftCode.ToString();
            this.txtCrewCodeEdit.Text = ((Line2Crew)obj).CrewCode.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DateCheck(lblDateQuery,DateEdit.Text,true));
            manager.Add(new LengthCheck(lblSSEdit, txtSSEdit, 40, true));
            manager.Add(new LengthCheck(lblShiftCodeEdit, txtShiftCodeEdit,40 , true));
            manager.Add(new LengthCheck(lblCrewCodeEdit, txtCrewCodeEdit,40 , true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            object stepSequenceObject = baseModelFacade.GetStepSequence(FormatHelper.CleanString(this.txtSSEdit.Text.ToUpper()));
            if (stepSequenceObject == null)
            {
                WebInfoPublish.Publish(this, "$CS_STEPSEQUENCE_NOT_EXIST", this.languageComponent1);
                return false;
            }

            ShiftModelFacade shiftModelFacade=new ShiftModelFacade(this.DataProvider);
            object shiftObject = shiftModelFacade.GetShift(FormatHelper.CleanString(this.txtShiftCodeEdit.Text.ToUpper()));
            if (shiftObject==null)
            {
                WebInfoPublish.Publish(this, "$Error_Shift_Not_Exist", this.languageComponent1);
                return false;
            }

            ShiftModel shiftModel=new ShiftModel(this.DataProvider);
            object crewObject = shiftModel.GetShiftCrew(FormatHelper.CleanString(this.txtCrewCodeEdit.Text.ToUpper()));
            if (crewObject == null)
            {
                WebInfoPublish.Publish(this, "$Error_ShiftCrew_Not_Exist", this.languageComponent1);
                return false;
            }
            return true;
        }

        #endregion


        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{ ((Line2CrewWithMessage)obj).ShiftDate.ToString(),
								((Line2CrewWithMessage)obj).SSCode.ToString(),
                                ((Line2CrewWithMessage)obj).StepSequenceDescription.ToString(),
								((Line2CrewWithMessage)obj).ShiftCode.ToString(),
                                ((Line2CrewWithMessage)obj).ShiftDescription.ToString(),
								((Line2CrewWithMessage)obj).CrewCode.ToString(),
					            ((Line2CrewWithMessage)obj).CrewDesc.ToString(),			
                                ((Line2CrewWithMessage)obj).BigSSCode.ToString(),	
                                ((Line2CrewWithMessage)obj).MaintainUser.ToString(),	
								FormatHelper.ToDateString(((Line2CrewWithMessage)obj).MaintainDate),
								FormatHelper.ToTimeString(((Line2CrewWithMessage)obj).MaintainTime)
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
									"Date",
									"SSCode",
									"StepSequenceDescription",
									"ShiftCode",
                                    "ShiftDescription",
                                    "CrewCode",
                                    "CrewDesc",
                                    "BigLine",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"};
        }
        #endregion

        protected void cmdImport_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("./ImportIndirectManCountDate/FExcelDataImp.aspx?itype=Line2Crew"));
        }

    }
}
