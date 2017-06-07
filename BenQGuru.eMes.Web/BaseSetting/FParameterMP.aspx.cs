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
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.UserControl;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FResourceMP 的摘要说明。
    /// </summary>
    public partial class FParameterMP : BaseMPageNew
    {
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblParameterTitle;

        private BenQGuru.eMES.BaseSetting.SystemSettingFacade _facade = null; // new SystemSettingFacade();
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected System.Web.UI.WebControls.Label lblParentParameterQuery;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

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
           // this.gridWebGrid.ClickCellButton += new ClickCellButtonEventHandler(gridWebGrid_ClickCellButton);

        }
        #endregion

        #region Init

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                InitParameterGroupList();
                if (this.GetRequestParam("parentparameter") != string.Empty)
                {
                    this.txtParentParameterQuery.Text = this.GetRequestParam("parentparameter");
                    this.chkQueryParentParameterAll.Checked = true;
                }
                if (this.GetRequestParam("parametergroup") != string.Empty)
                {
                    try
                    {
                        this.drpParameterGroupCodeQuery.SelectedValue = this.GetRequestParam("parametergroup");
                    }
                    catch { }
                }
                this.chkQueryParentParameterAll.Attributes["onclick"] = "onCheckBoxChange(this)";
            }
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ParameterCode", "参数代码", null);
            this.gridHelper.AddColumn("ParameterAlias", "参数别名", null);
            this.gridHelper.AddColumn("ParameterGroupCode", "参数组代码", null);
            this.gridHelper.AddColumn("ParameterValue", "参数值", null);
            this.gridHelper.AddColumn("IsActive", "是否为可用参数", null);
            this.gridHelper.AddColumn("IsSystem", "是否为系统参数", null);
            this.gridHelper.AddColumn("ParameterSequence", "参数顺序", null);
            this.gridHelper.AddColumn("ParameterDescription", "参数描述", null);
            this.gridHelper.AddColumn("ParentParameter", "父参数", null);
            this.gridHelper.AddLinkColumn("QueryParameterChilden", "子参数", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridWebGrid.Columns.FromKey("IsActive").Hidden = true;
            this.gridWebGrid.Columns.FromKey("IsSystem").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            if (this.GetRequestParam("parentparameter") != string.Empty)
            {
                this.txtParentParameterQuery.Text = this.GetRequestParam("parentparameter");
                this.chkQueryParentParameterAll.Checked = true;
                if (this.GetRequestParam("parametergroup") != string.Empty)
                {
                    try
                    {
                        this.drpParameterGroupCodeQuery.SelectedValue = this.GetRequestParam("parametergroup");
                    }
                    catch { }
                }
                this.gridHelper.RequestData();
            }
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterCode.ToString(),
            //                    ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterAlias.ToString(),
            //                    ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterGroupCode.ToString(),
            //                    this.languageComponent1.GetString(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterValue.ToString()),
            //                    FormatHelper.DisplayBoolean(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).IsActive, this.languageComponent1),
            //                    FormatHelper.DisplayBoolean(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).IsSystem, this.languageComponent1),
            //                    ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterSequence.ToString(),
            //                    ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterDescription.ToString(),
            //                    ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParentParameterCode.ToString(),
            //                    "",
            //                    //((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).MaintainUser.ToString(),
            //                     ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).GetDisplayText("MaintainUser"),


            //                    FormatHelper.ToDateString(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["ParameterCode"] = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterCode.ToString();
            row["ParameterAlias"] = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterAlias.ToString();
            row["ParameterGroupCode"] = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterGroupCode.ToString();
            row["ParameterValue"] = this.languageComponent1.GetString(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterValue.ToString());
            row["IsActive"] = FormatHelper.DisplayBoolean(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).IsActive, this.languageComponent1);
            row["IsSystem"] = FormatHelper.DisplayBoolean(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).IsSystem, this.languageComponent1);
            row["ParameterSequence"] = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterSequence.ToString();
            row["ParameterDescription"] = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterDescription.ToString();
            row["ParentParameter"] = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParentParameterCode.ToString();
            row["MaintainUser"] = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).MaintainTime);
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(base.DataProvider);
            }
            return this._facade.QueryParameter(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtParameterCodeQuery.Text)),
                this.drpParameterGroupCodeQuery.SelectedValue,
                this.txtParentParameterQuery.Text.ToUpper(),
                !this.chkQueryParentParameterAll.Checked,
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(base.DataProvider);
            }
            return this._facade.QueryParameterCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtParameterCodeQuery.Text)),
                this.drpParameterGroupCodeQuery.SelectedValue,
                this.txtParentParameterQuery.Text.ToUpper(),
                !this.chkQueryParentParameterAll.Checked);
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "QueryParameterChilden")
            {
                string strParamCode = row.Items.FindItemByKey("ParameterCode").Text;
                string strGroupCode = row.Items.FindItemByKey("ParameterGroupCode").Text;
                string strUrl = "FParameterMP.aspx?parentparameter=" + strParamCode + "&parametergroup=" + strGroupCode;
                this.Response.Redirect(strUrl);
                return;
            }
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(base.DataProvider);
            }
            this._facade.AddParameter((BenQGuru.eMES.Domain.BaseSetting.Parameter)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(base.DataProvider);
            }
            this._facade.DeleteParameter((BenQGuru.eMES.Domain.BaseSetting.Parameter[])domainObjects.ToArray(typeof(BenQGuru.eMES.Domain.BaseSetting.Parameter)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(base.DataProvider);
            }
            this._facade.UpdateParameter((BenQGuru.eMES.Domain.BaseSetting.Parameter)domainObject);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtParameterCodeEdit.ReadOnly = false;
                this.drpParameterGroupCodeEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtParameterCodeEdit.ReadOnly = true;
                this.drpParameterGroupCodeEdit.Enabled = false;
            }
        }

        #endregion

        #region Object <--> Page


        protected override object GetEditObject()
        {
            //this.ValidateInput();
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(base.DataProvider);
            }
            BenQGuru.eMES.Domain.BaseSetting.Parameter parameter = this._facade.CreateNewParameter();

            parameter.ParameterCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtParameterCodeEdit.Text, 40));
            parameter.ParameterDescription = FormatHelper.CleanString(this.txtParameterDescriptionEdit.Text, 40);
            parameter.IsActive = FormatHelper.BooleanToString(this.chbIsActiveEdit.Checked);
            parameter.IsSystem = FormatHelper.BooleanToString(this.chbIsSystemEdit.Checked);
            parameter.ParameterGroupCode = this.drpParameterGroupCodeEdit.SelectedValue;
            parameter.ParameterSequence = FormatHelper.CleanString(this.txtParameterSeq.Text, 40);			//参数顺序
            if (this.drpParameterGroupCodeEdit.SelectedValue == "")
            {
                parameter.ParameterValue = txtParameterCodeEdit.Text.Trim();
            }
            else
            {
                parameter.ParameterValue = this.drpParamterValueEdit.SelectedValue;
            }

            parameter.ParameterAlias = FormatHelper.CleanString(this.txtParameterAliasEdit.Text, 40);
            parameter.MaintainUser = this.GetUserCode();
            parameter.ParentParameterCode = this.drpParentParameter.SelectedValue;

            return parameter;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(base.DataProvider);
            }
            object obj = _facade.GetParameter(row.Items.FindItemByKey("ParameterCode").Text.ToString(), row.Items.FindItemByKey("ParameterGroupCode").Text.ToString());

            if (obj != null)
            {
                return (BenQGuru.eMES.Domain.BaseSetting.Parameter)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtParameterCodeEdit.Text = "";
                this.txtParameterDescriptionEdit.Text = "";
                this.chbIsActiveEdit.Checked = false;
                this.chbIsSystemEdit.Checked = false;
                this.txtParameterAliasEdit.Text = "";
                this.drpParameterGroupCodeEdit.SelectedIndex = 0;
                this.drpParamterValueEdit.Items.Clear();
                this.txtParameterSeq.Text = "1";
                this.drpParentParameter.SelectedIndex = 0;
                return;
            }

            this.txtParameterCodeEdit.Text = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterCode.ToString();
            this.txtParameterDescriptionEdit.Text = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterDescription.ToString();
            this.chbIsActiveEdit.Checked = FormatHelper.StringToBoolean(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).IsActive);
            this.chbIsSystemEdit.Checked = FormatHelper.StringToBoolean(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).IsSystem);
            this.txtParameterAliasEdit.Text = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterAlias.ToString();
            this.txtParameterSeq.Text = (((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterSequence != null && ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterSequence != string.Empty) ? ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterSequence : "1";

            try
            {
                this.drpParameterGroupCodeEdit.SelectedValue = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterGroupCode.ToString();
            }
            catch
            {
                this.drpParameterGroupCodeEdit.SelectedIndex = 0;
            }

            try
            {
                this.ChangeParameterGroupGetParameterValue();
                this.drpParamterValueEdit.SelectedValue = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterValue.ToString();
            }
            catch
            {
                this.drpParamterValueEdit.SelectedIndex = 0;
            }
            try
            {
                InitParentParameterList();
                this.drpParentParameter.SelectedValue = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParentParameterCode;
            }
            catch
            {
                this.drpParentParameter.SelectedIndex = 0;
            }
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblParameterCodeEdit, this.txtParameterCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblParameterAliasEdit, this.txtParameterAliasEdit, 40, false));
            manager.Add(new LengthCheck(this.lblParameterDescriptionEdit, this.txtParameterDescriptionEdit, 40, false));
            manager.Add(new LengthCheck(this.lblParameterGroupCodeEdit, this.drpParameterGroupCodeEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region 数据初始化

        private void InitParameterGroupList()
        {
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(base.DataProvider);
            }
            DropDownListBuilder builder = new DropDownListBuilder(this.drpParameterGroupCodeQuery);
            builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllParameterGroups);

            builder.Build("ParameterGroupCode", "ParameterGroupCode");
            this.drpParameterGroupCodeQuery.Items.Insert(0, string.Empty);

            builder = new DropDownListBuilder(this.drpParameterGroupCodeEdit);
            builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllParameterGroups);

            builder.Build("ParameterGroupCode", "ParameterGroupCode");
            this.drpParameterGroupCodeEdit.Items.Insert(0, string.Empty);

        }

        private void InitParentParameterList()
        {
            this.drpParentParameter.Items.Clear();
            drpParentParameter.Items.Add("");
            if (drpParameterGroupCodeEdit.SelectedValue == string.Empty)
                return;
            if (_facade == null)
            {
                _facade = new SystemSettingFacade(base.DataProvider);
            }
            ITreeObjectNode node = _facade.BuildParameterTree(drpParameterGroupCodeEdit.SelectedValue);
            TreeObjectNodeSet nodeSet = node.GetSubLevelChildrenNodes();
            for (int i = 0; i < nodeSet.Count; i++)
            {
                AppendParentParameter((ITreeObjectNode)nodeSet[i], "");
            }
        }
        private void AppendParentParameter(ITreeObjectNode node, string prefix)
        {
            drpParentParameter.Items.Add(new ListItem(prefix + node.Text, node.Text));
            TreeObjectNodeSet nodeSet = node.GetSubLevelChildrenNodes();
            for (int i = 0; i < nodeSet.Count; i++)
            {
                char nbsp = (char)0xA0;
                AppendParentParameter((ITreeObjectNode)nodeSet[i], prefix + (new string(nbsp, 4)));
            }
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterCode.ToString(),
								   ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterAlias.ToString(),
								   ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterGroupCode.ToString(),
								   FormatHelper.DisplayBoolean(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).IsActive, this.languageComponent1),
								   FormatHelper.DisplayBoolean(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).IsSystem, this.languageComponent1),
								   ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterDescription.ToString(),
								   ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParentParameterCode.ToString(),
                                   //((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).MaintainUser.ToString(),
                            ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).MaintainDate),
								   FormatHelper.ToTimeString(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).MaintainTime) };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"参数代码",
									"参数别名",	
									 "参数组代码",
									"是否为可用参数",
									"是否为系统参数",	
									"参数描述",
									"父参数",
									"维护用户",	
									"维护日期",	
									"维护时间" };
        }

        #endregion

        protected void drpParameterGroupCodeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ChangeParameterGroupGetParameterValue();
            this.InitParentParameterList();
        }

        private void ChangeParameterGroupGetParameterValue()
        {
            this.drpParamterValueEdit.Items.Clear();

            if (InternalSystemVariable.Lookup(drpParameterGroupCodeEdit.SelectedValue) == null)
            {
                return;
            }

            foreach (string _Items in (InternalSystemVariable.Lookup(drpParameterGroupCodeEdit.SelectedValue).Items))
            {
                string word = this.languageComponent1.GetString(_Items);

                if (word == string.Empty)
                {
                    drpParamterValueEdit.Items.Add(new ListItem(_Items, _Items));
                }
                else
                {
                    drpParamterValueEdit.Items.Add(new ListItem(word, _Items));
                }
            }
        }
    }
}
