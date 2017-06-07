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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.SMT
{
    /// <summary>
    /// FFeederMP 的摘要说明。
    /// </summary>
    public partial class FFeederMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.SMT.SMTFacade _facade;//= new SMTFacadeFactory().Create();

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
            this.txtMaxCountEdit.Attributes.Add("onkeyup", "CalAlertCount()");
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
                object[] objSpec = _facade.GetAllFeederSpec();
                if (objSpec != null)
                {
                    this.ddlFeederSpecCodeQuery.Items.Add(string.Empty);
                    for (int i = 0; i < objSpec.Length; i++)
                    {
                        FeederSpec spec = (FeederSpec)objSpec[i];
                        this.ddlFeederSpecCodeQuery.Items.Add(new ListItem(spec.FeederSpecCode, spec.FeederSpecCode));
                        this.ddlFeederSpecCodeEdit.Items.Add(new ListItem(spec.FeederSpecCode, spec.FeederSpecCode));
                    }
                }

                SystemParameterListBuilder _builder = new SystemParameterListBuilder("FEEDERTYPE", base.DataProvider);
                //_builder.Build(this.ddlFeederTypeEdit);
                this.ddlFeederTypeEdit.Items.Add(new ListItem("CP", "CP"));
                _builder = new SystemParameterListBuilder("FEEDERSTATUS", base.DataProvider);
                _builder.Build(this.ddlStatusQuery);
                _builder.AddAllItem(this.languageComponent1);
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
            this.gridHelper.AddColumn("FeederCode", "Feeder代码", null);
            this.gridHelper.AddColumn("FeederType", "Feeder类型", null);
            this.gridHelper.AddColumn("FeederSpecCode", "规格代码", null);
            this.gridHelper.AddColumn("MaxCount", "最大使用次数", null);
            this.gridHelper.AddColumn("AlertCount", "预警使用次数", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddColumn("UsedCount", "当前使用次数", HorizontalAlign.Right);
            this.gridHelper.AddColumn("TotalCount", "累计使用次数", HorizontalAlign.Right);
            this.gridHelper.AddColumn("UseFlag", "是否领用", null);
            this.gridHelper.AddColumn("MOCode", "领用工单", null);
            this.gridHelper.AddColumn("sscode", "产线代码", null);

            //Add by terry 2010-11-03
            this.gridHelper.AddColumn("MaxMDay", "保养期限", null);
            this.gridHelper.AddColumn("AlterMDay", "预警期限", null);
            this.gridHelper.AddColumn("TheMaintainDate", "保养日期", null);
            this.gridHelper.AddColumn("OperationUser", "领用人员", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);


           // this.gridWebGrid.Columns.FromKey("UsedCount").Format = "#,#";
           // this.gridWebGrid.Columns.FromKey("UsedCount").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gridWebGrid.Columns.FromKey("TotalCount").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("TotalCount").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            this.gridWebGrid.Columns.FromKey("FeederType").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            Feeder feeder = (Feeder)obj;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    feeder.FeederCode,
            //                    feeder.FeederType,
            //                    feeder.FeederSpecCode,
            //                    feeder.MaxCount,
            //                    feeder.AlertCount,
            //                    (this.ddlStatusQuery.Items.FindByValue(feeder.Status.ToUpper()) == null ? feeder.Status : this.ddlStatusQuery.Items.FindByValue(feeder.Status.ToUpper()).Text),
            //                    feeder.UsedCount,
            //                    feeder.TotalCount,
            //                    FormatHelper.StringToBoolean(feeder.UseFlag).ToString(),
            //                    feeder.MOCode,
            //                    feeder.StepSequenceCode,


            //                    //Add by Terry 2010-11-03
            //                    feeder.MaxMDay,
            //                    feeder .AlterMDay,
            //                    FormatHelper.ToDateString( feeder.TheMaintainDate),
            //                    feeder.OperationUser,
            //                    feeder.MaintainUser,
            //                    FormatHelper.ToDateString(feeder.MaintainDate),
            //                    FormatHelper.ToTimeString(feeder.MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["FeederCode"] = feeder.FeederCode;
            row["FeederType"] = feeder.FeederType;
            row["FeederSpecCode"] = feeder.FeederSpecCode;
            row["MaxCount"] = feeder.MaxCount;
            row["AlertCount"] = feeder.AlertCount;
            row["Status"] = (this.ddlStatusQuery.Items.FindByValue(feeder.Status.ToUpper()) == null ? feeder.Status : this.ddlStatusQuery.Items.FindByValue(feeder.Status.ToUpper()).Text);
            row["UsedCount"] = String.Format("{0:#,#}",feeder.UsedCount);
            row["TotalCount"] = String.Format("{0:#,#}",feeder.TotalCount);
            row["UseFlag"] = FormatHelper.StringToBoolean(feeder.UseFlag).ToString();
            row["MOCode"] = feeder.MOCode;
            row["sscode"] = feeder.StepSequenceCode;
            row["MaxMDay"] = feeder.MaxMDay;
            row["AlterMDay"] = feeder.AlterMDay;
            row["TheMaintainDate"] = FormatHelper.ToDateString(feeder.TheMaintainDate);
            row["OperationUser"] = feeder.OperationUser;
            row["MaintainUser"] = feeder.MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(feeder.MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(feeder.MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            return this._facade.QueryFeeder(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFeederCodeQuery.Text)),
                ddlFeederSpecCodeQuery.SelectedValue,
                ddlStatusQuery.SelectedValue,
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            return this._facade.QueryFeederCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFeederCodeQuery.Text)),
                ddlFeederSpecCodeQuery.SelectedValue,
                ddlStatusQuery.SelectedValue);
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            this._facade.AddFeeder((Feeder)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            this._facade.DeleteFeeder((Feeder[])domainObjects.ToArray(typeof(Feeder)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            this._facade.UpdateFeeder((Feeder)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtFeederCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtFeederCodeEdit.ReadOnly = true;
            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            Feeder feeder = (Feeder)_facade.GetFeeder(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFeederCodeEdit.Text, 40)));
            if (feeder == null)
            {
                feeder = this._facade.CreateNewFeeder();
                feeder.TheMaintainDate = dbDateTime.DBDate;
                feeder.TheMaintainTime = dbDateTime.DBTime;
                feeder.TheMaintainUser = this.GetUserCode();
            }

            feeder.FeederCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFeederCodeEdit.Text, 40));
            if (ddlFeederSpecCodeEdit.SelectedItem != null)
                feeder.FeederSpecCode = ddlFeederSpecCodeEdit.SelectedValue;
            if (ddlFeederTypeEdit.SelectedItem != null)
                feeder.FeederType = ddlFeederTypeEdit.SelectedValue;
            feeder.MaxCount = Convert.ToDecimal(txtMaxCountEdit.Text);
            feeder.AlertCount = Convert.ToDecimal(txtAlertCountEdit.Text);
            feeder.MaintainUser = this.GetUserCode();
            feeder.MaintainDate = dbDateTime.DBDate;
            feeder.MaintainTime = dbDateTime.DBTime;

            //Add by Terry 2010-11-03
            feeder.MaxMDay = Convert.ToDecimal(TxtMaxMDayEdit.Text);
            feeder.AlterMDay = Convert.ToDecimal(TxtAlterMDAYEdit.Text);

            return feeder;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            object obj = _facade.GetFeeder(row.Items.FindItemByKey("FeederCode").Text.ToString());

            if (obj != null)
            {
                return (Feeder)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtFeederCodeEdit.Text = "";
                this.ddlFeederSpecCodeEdit.SelectedIndex = -1;
                this.ddlFeederTypeEdit.SelectedIndex = -1;
                this.txtMaxCountEdit.Text = "0";
                this.txtAlertCountEdit.Text = "0";
                //Add by Terry 2010-11-03
                this.TxtMaxMDayEdit.Text = "0";
                this.TxtAlterMDAYEdit.Text = "0";

                return;
            }

            Feeder feeder = (Feeder)obj;
            this.txtFeederCodeEdit.Text = feeder.FeederCode;
            if (this.ddlFeederSpecCodeEdit.Items.FindByValue(feeder.FeederSpecCode) != null)
                this.ddlFeederSpecCodeEdit.SelectedIndex = this.ddlFeederSpecCodeEdit.Items.IndexOf(this.ddlFeederSpecCodeEdit.Items.FindByValue(feeder.FeederSpecCode));
            if (this.ddlFeederTypeEdit.Items.FindByValue(feeder.FeederType) != null)
                this.ddlFeederTypeEdit.SelectedIndex = this.ddlFeederTypeEdit.Items.IndexOf(this.ddlFeederTypeEdit.Items.FindByValue(feeder.FeederType));
            this.txtMaxCountEdit.Text = feeder.MaxCount.ToString();
            this.txtAlertCountEdit.Text = feeder.AlertCount.ToString();

            //Add by Terry 2010-11-03
            this.TxtMaxMDayEdit.Text = feeder.MaxMDay.ToString();
            this.TxtAlterMDAYEdit.Text = feeder.AlterMDay.ToString();

        }


        protected override bool ValidateInput()
        {

            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblFeederCodeEdit, txtFeederCodeEdit, 40, true));
            manager.Add(new DecimalCheck(lblMaxCountEdit, txtMaxCountEdit, 0, decimal.MaxValue, true));
            manager.Add(new DecimalCheck(lblAlertCountEdit, txtAlertCountEdit, 0, decimal.MaxValue, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            if (Convert.ToDecimal(this.txtAlertCountEdit.Text) > Convert.ToDecimal(this.txtMaxCountEdit.Text))
            {
                WebInfoPublish.Publish(this, "$Feeder_AlertCount_Greater_Than_MaxCount", this.languageComponent1);
                return false;
            }


            //Add by terry 2010-11-03
            if (Convert.ToDecimal(this.TxtAlterMDAYEdit.Text) > Convert.ToDecimal(this.TxtMaxMDayEdit.Text))
            {
                WebInfoPublish.Publish(this, "$Feeder_AlterMDay_Greater_Than_MaxMDay", this.languageComponent1);
                return false;
            }

            return true;


        }

        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            Feeder feeder = (Feeder)obj;
            return new string[]{  feeder.FeederCode,
								   //feeder.FeederType,
								   feeder.FeederSpecCode,
								   feeder.MaxCount.ToString(),
								   feeder.AlertCount.ToString(),
								   (this.ddlStatusQuery.Items.FindByValue(feeder.Status.ToUpper()) == null ? feeder.Status : this.ddlStatusQuery.Items.FindByValue(feeder.Status.ToUpper()).Text),
								   feeder.UsedCount.ToString(),
								   feeder.TotalCount.ToString(),
								   FormatHelper.StringToBoolean(feeder.UseFlag).ToString(),
								   feeder.MOCode,
								   feeder.StepSequenceCode,
                                   feeder.MaxMDay.ToString (),
                                   feeder.AlterMDay.ToString (),
                                   FormatHelper.ToDateString(feeder.TheMaintainDate),
								   feeder.OperationUser,
								   feeder.MaintainUser,
								   FormatHelper.ToDateString(feeder.MaintainDate),
								   FormatHelper.ToTimeString(feeder.MaintainTime) };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
									"FeederCode",
									//"FeederType",
									"FeederSpecCode",
									"MaxCount",
									"AlertCount",
									"Status",
									"UsedCount",
									"TotalCount",
									"IsInUse",
									"MOCode",
									"sscode",
                                    "MaxMDay",
                                    "AlterMDay",
                                    "TheMaintainDate",
									"OperationUser",
									"MaintainUser",	
									"MaintainDate",
									"MaintainTime"};
        }
        #endregion
    }
}
