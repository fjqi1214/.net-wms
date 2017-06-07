using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FBSHomeSettingDetail : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private SystemSettingFacade _SystemSettingFacade = null;
        private Dictionary<string, Type> _ParameterDic = new Dictionary<string, Type>();

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);

            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

            InitParameterTypeDic();
        }

        private void InitParameterTypeDic()
        {
            _ParameterDic = FormatHelper.GetReportParameterDic();
        }

        #endregion

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            _SystemSettingFacade = new SystemSettingFacade(this.DataProvider);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.SetQueryControls();

                this.BuildParamNameList();
            }

            SetWebControlDisplay(this.ddlParamValueEdit, false);
            SetWebControlDisplay(this.chbParamValueEdit, false);
        }

        protected override void OnPreRender(EventArgs e)
        {
            
            base.OnPreRender(e);
        }

        private void SetWebControlDisplay(WebControl control, bool display)
        {
            if (display)
            {
                control.Attributes["style"] = "display:block;";
            }
            else
            {
                control.Attributes["style"] = "display:none;";
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void BuildParamNameList()
        {
            this.ddlParamNameEdit.Items.Add(new ListItem("", ""));

            foreach (string key in _ParameterDic.Keys)
            {
                string text = key.Substring(2);
                int pos = text.IndexOf(".");
                string groupName = text.Substring(0, pos - 1);
                string paramName = text.Substring(pos + 4);
                text = this.languageComponent1.GetString("$PageControl_" + groupName) + " - " + this.languageComponent1.GetString("$PageControl_" + paramName);

                ddlParamNameEdit.Items.Add(new ListItem(text, key));
            }

            this.ddlParamNameEdit.SelectedIndex = 0;
        }

        private void SetQueryControls()
        {
            if (this.Request.Params["ReportSeq"] != null)
            {
                int reportSeq = int.Parse(this.Request.Params["ReportSeq"]);

                BSHomeSetting setting = (BSHomeSetting)_SystemSettingFacade.GetBSHomeSetting(reportSeq);

                if (setting != null)
                {
                    this.txtReportSeqQuery.Text = setting.ReportSeq.ToString();
                    this.txtModuleQuery.Text = FormatHelper.GetModuleTitle(this.languageComponent1, setting.ModuleCode);
                    this.txtChartTypeQuery.Text = this.languageComponent1.GetString(setting.ChartType);
                }
            }
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FBSHomeSettingMP.aspx", new string[] {  }, new string[] { }));
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ReportSeq", "报表位置", null);
            this.gridHelper.AddColumn("ParameterName", "参数名称", null);
            this.gridHelper.AddColumn("ParameterValue", "参数值", null);
            this.gridHelper.AddDefaultColumn(true, true);

            this.gridWebGrid.Columns.FromKey("ReportSeq").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //        "false",
            //        ((BSHomeSettingDetail)obj).ReportSeq.ToString(),
            //        ((BSHomeSettingDetail)obj).ParameterName,
            //        ((BSHomeSettingDetail)obj).ParameterValue,
            //        ""
            //    }
            //);
            DataRow row = this.DtSource.NewRow();
            row["ReportSeq"] = ((BSHomeSettingDetail)obj).ReportSeq.ToString();
            row["ParameterName"] = ((BSHomeSettingDetail)obj).ParameterName;
            row["ParameterValue"] = ((BSHomeSettingDetail)obj).ParameterValue;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._SystemSettingFacade.QueryBSHomeSettingDetail(int.Parse(this.txtReportSeqQuery.Text), string.Empty, inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            return this._SystemSettingFacade.QueryBSHomeSettingDetailCount(int.Parse(this.txtReportSeqQuery.Text), string.Empty);
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            this._SystemSettingFacade.AddBSHomeSettingDetail((BSHomeSettingDetail)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            this._SystemSettingFacade.DeleteBSHomeSettingDetail((BSHomeSettingDetail[])domainObjects.ToArray(typeof(BSHomeSettingDetail)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            this._SystemSettingFacade.UpdateBSHomeSettingDetail((BSHomeSettingDetail)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.ddlParamNameEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.ddlParamNameEdit.Enabled = false;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            BSHomeSettingDetail settingDetail = this._SystemSettingFacade.CreateNewBSHomeSettingDetail();

            settingDetail.ReportSeq = int.Parse(this.txtReportSeqQuery.Text);
            settingDetail.ParameterName = this.ddlParamNameEdit.SelectedValue;

            settingDetail.MaintainUser = this.GetUserCode();

            if (_ParameterDic[settingDetail.ParameterName] == typeof(string))
            {
                settingDetail.ParameterValue = this.txtParamValueEdit.Text.Trim();
            }
            else if (_ParameterDic[settingDetail.ParameterName] == typeof(DateTime))
            {
                settingDetail.ParameterValue = this.txtParamValueEdit.Text.Trim();
            }
            else if (_ParameterDic[settingDetail.ParameterName] == typeof(bool))
            {
                settingDetail.ParameterValue = this.chbParamValueEdit.Checked.ToString();
            }
            else
            {
                settingDetail.ParameterValue = this.ddlParamValueEdit.SelectedValue;
            }

            return settingDetail;
        }

        protected override object GetEditObject(GridRecord row)
        {

            object obj = _SystemSettingFacade.GetBSHomeSettingDetail(int.Parse(row.Items.FindItemByKey("ReportSeq").Text), row.Items.FindItemByKey("ParameterName").Text);

            if (obj != null)
            {
                return (BSHomeSettingDetail)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.ddlParamNameEdit.SelectedIndex = 0;
                this.txtParamValueEdit.Text = string.Empty;
                this.chbParamValueEdit.Checked = true;
                this.ddlParamValueEdit.Items.Clear();

                SetWebControlDisplay(this.txtParamValueEdit, true);
                SetWebControlDisplay(this.ddlParamValueEdit, false);
                SetWebControlDisplay(this.chbParamValueEdit, false);

                return;
            }

            BSHomeSettingDetail settingDetail = (BSHomeSettingDetail)obj;

            try
            {
                this.ddlParamNameEdit.SelectedValue = settingDetail.ParameterName;
                ddlParamNameEdit_SelectedIndexChanged(null, null);
            }
            catch
            {
                this.ddlParamNameEdit.SelectedIndex = 0;
                ddlParamNameEdit_SelectedIndexChanged(null, null);
            }

            if (_ParameterDic[settingDetail.ParameterName] == typeof(string))
            {
                this.txtParamValueEdit.Text = settingDetail.ParameterValue;
            }
            else if (_ParameterDic[settingDetail.ParameterName] == typeof(DateTime))
            {
                this.txtParamValueEdit.Text = settingDetail.ParameterValue;
            }
            else if (_ParameterDic[settingDetail.ParameterName] == typeof(bool))
            {
                this.chbParamValueEdit.Checked = bool.Parse(settingDetail.ParameterValue);
            }
            else
            {
                this.ddlParamValueEdit.SelectedValue = settingDetail.ParameterValue;
            }

        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblParamNameEdit, this.ddlParamNameEdit, 100, true));

            if (this.ddlParamNameEdit.SelectedValue.Trim().Length > 0)
            {
                string parameterName = this.ddlParamNameEdit.SelectedValue.Trim();

                if (_ParameterDic[parameterName] == typeof(string))
                {
                    manager.Add(new LengthCheck(this.lblParamValueEdit, this.txtParamValueEdit, 2000, true));
                }
                else if (_ParameterDic[parameterName] == typeof(DateTime))
                {
                    manager.Add(new LengthCheck(this.lblParamValueEdit, this.txtParamValueEdit, 2000, true));
                }
                else if (_ParameterDic[parameterName] == typeof(bool))
                {
                    //manager.Add(new LengthCheck(this.lblParamValueEdit, this.chbParamValueEdit, 2000, true));
                }
                else
                {
                    manager.Add(new LengthCheck(this.lblParamValueEdit, this.ddlParamValueEdit, 2000, true));
                }

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                    ((BSHomeSettingDetail)obj).ReportSeq.ToString(),
                    ((BSHomeSettingDetail)obj).ParameterName,
                    ((BSHomeSettingDetail)obj).ParameterValue
  
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
                "ReportSeq",
                "ParameterName",
                "ParameterValue"
            };
        }

        #endregion

        protected void ddlParamNameEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ParameterDic.ContainsKey(this.ddlParamNameEdit.SelectedValue))
            {
                if (_ParameterDic[this.ddlParamNameEdit.SelectedValue] == typeof(string))
                {
                    SetWebControlDisplay(this.txtParamValueEdit, true);
                    SetWebControlDisplay(this.ddlParamValueEdit, false);
                    SetWebControlDisplay(this.chbParamValueEdit, false);
                }
                else if (_ParameterDic[this.ddlParamNameEdit.SelectedValue] == typeof(DateTime))
                {
                    SetWebControlDisplay(this.txtParamValueEdit, true);
                    SetWebControlDisplay(this.ddlParamValueEdit, false);
                    SetWebControlDisplay(this.chbParamValueEdit, false);
                }
                else if (_ParameterDic[this.ddlParamNameEdit.SelectedValue] == typeof(bool))
                {
                    SetWebControlDisplay(this.txtParamValueEdit, false);
                    SetWebControlDisplay(this.ddlParamValueEdit, false);
                    SetWebControlDisplay(this.chbParamValueEdit, true);
                }
                else
                {
                    SetWebControlDisplay(this.txtParamValueEdit, false);
                    SetWebControlDisplay(this.ddlParamValueEdit, true);
                    SetWebControlDisplay(this.chbParamValueEdit, false);

                    ddlParamValueEdit.Items.Clear();
                    ddlParamValueEdit.Items.Add(new ListItem("", ""));
                    foreach (FieldInfo info in _ParameterDic[this.ddlParamNameEdit.SelectedValue].GetFields())
                    {
                        string key = info.GetValue(null).ToString();
                        string text = this.languageComponent1.GetString(key);
                        if (text.Trim().Length > 0)
                        {
                            ddlParamValueEdit.Items.Add(new ListItem(text, key));
                        }
                    }
                    ddlParamValueEdit.SelectedIndex = 0;
                }
            }
        }
    }
}
