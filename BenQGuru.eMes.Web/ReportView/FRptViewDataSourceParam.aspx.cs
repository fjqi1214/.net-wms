using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Domain.ReportView;
using Infragistics.Web.UI.GridControls;

namespace BenQGuru.eMES.Web.ReportView
{
    public partial class FRptViewDataSourceParam : BaseMPageNew
    {
        #region Web 窗体设计器生成的代码
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private ReportViewFacade _facade = null;
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
            this.languageComponent1.LanguagePackageDir = "D:\\code\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                if (_facade == null)
                {
                    _facade = new ReportViewFacade(base.DataProvider);
                }
                object obj = _facade.GetRptViewDataSource(Convert.ToDecimal(this.GetRequestParam("id")));
                txtDBName.Text = ((RptViewDataSource)obj).Name.ToString();
                txtDllName.Text = ((RptViewDataSource)obj).DllFileName.ToString();
                datasourceid.Value = this.GetRequestParam("id");

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
            this.gridHelper.AddColumn("ParameterSequence", "次序", null);
            this.gridHelper.AddColumn("ParameterName", "参数名称", null);
            this.gridHelper.AddColumn("Description", "参数描述", null);
            this.gridHelper.AddColumn("DataType", "数据类型", null);
            this.gridHelper.AddColumn("DefaultValue", "默认值", null);
            this.gridHelper.AddDefaultColumn(true, true);
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.cmdQuery_Click(null, null);
        }

        protected override DataRow GetGridRow(object obj)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            object viewobj = _facade.GetRptViewDataSourceParam(Convert.ToDecimal(datasourceid.Value), Convert.ToDecimal(((RptViewDataSourceParam)obj).ParameterSequence));
            DataRow row = DtSource.NewRow();
            row["ParameterSequence"] = ((RptViewDataSourceParam)obj).ParameterSequence.ToString();
            row["ParameterName"] = ((RptViewDataSourceParam)obj).ParameterName.ToString();
            row["Description"] = ((RptViewDataSourceParam)obj).Description.ToString();
            row["DataType"] = ((RptViewDataSourceParam)viewobj).DataType.ToString();
            row["DefaultValue"] = ((RptViewDataSourceParam)obj).DefaultValue.ToString();
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            return this._facade.QueryDataSourceParam(Convert.ToDecimal(datasourceid.Value),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            return this._facade.QueryDataSourceParamCount(Convert.ToDecimal(datasourceid.Value));
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            this._facade.AddRptViewDataSourceParam((RptViewDataSourceParam)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            this._facade.DeleteRptViewDataSourceParam((RptViewDataSourceParam[])domainObjects.ToArray(typeof(RptViewDataSourceParam)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            this._facade.UpdateRptViewDataSourceParam((RptViewDataSourceParam)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtNameEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtNameEdit.Enabled = false;
            }
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("../ReportView/FRptViewDataSource.aspx"));
        }
        #endregion

        #region 数据初始化
        protected void drpDBTypeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpdbtype.Items.Clear();
                drpdbtype.Items.Add(new ListItem(this.languageComponent1.GetString(ReportDataType.Date), ReportDataType.Date));
                drpdbtype.Items.Add(new ListItem(this.languageComponent1.GetString(ReportDataType.Numeric), ReportDataType.Numeric));
                drpdbtype.Items.Add(new ListItem(this.languageComponent1.GetString(ReportDataType.String), ReportDataType.String));
            }
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            RptViewDataSourceParam rptviewdatasourceparm = this._facade.CreateNewRptViewDataSourceParam();

            rptviewdatasourceparm.ParameterSequence = Convert.ToDecimal(this.txtNameEdit.Text);
            rptviewdatasourceparm.ParameterName = FormatHelper.CleanString(this.txtParamNameEdit.Text);
            rptviewdatasourceparm.Description = FormatHelper.CleanString(this.txtparmdescEdit.Text, 100);
            rptviewdatasourceparm.DataType = this.drpdbtype.SelectedValue;

            rptviewdatasourceparm.DefaultValue = FormatHelper.CleanString(this.txtDefaultNameEdit.Text);
            rptviewdatasourceparm.DataSourceID = Convert.ToDecimal(datasourceid.Value);
            rptviewdatasourceparm.MaintainUser = this.GetUserCode();
            return rptviewdatasourceparm;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            object obj = _facade.GetRptViewDataSourceParam(Convert.ToDecimal(datasourceid.Value), (Convert.ToDecimal(row.Items.FindItemByKey("ParameterSequence").Text)));

            if (obj != null)
            {
                return obj as RptViewDataSourceParam;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtNameEdit.Text = "";
                this.txtParamNameEdit.Text = "";
                this.txtparmdescEdit.Text = "";
                txtDefaultNameEdit.Text = "";
                this.drpdbtype.SelectedIndex = 0;
                return;
            }

            this.txtNameEdit.Text = ((RptViewDataSourceParam)obj).ParameterSequence.ToString();
            this.txtParamNameEdit.Text = ((RptViewDataSourceParam)obj).ParameterName.ToString();
            this.txtparmdescEdit.Text = ((RptViewDataSourceParam)obj).Description.ToString();
            txtDefaultNameEdit.Text = ((RptViewDataSourceParam)obj).DefaultValue.ToString();
            drpdbtype.SelectedIndex =
                this.drpdbtype.Items.IndexOf(this.drpdbtype.Items.FindByValue(((RptViewDataSourceParam)obj).DataType));

        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblID, this.txtNameEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }

        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((RptViewDataSourceParam)obj).ParameterSequence.ToString(),
                                   ((RptViewDataSourceParam)obj).ParameterName.ToString(),
                                   ((RptViewDataSourceParam)obj).Description.ToString(),
                                   ((RptViewDataSourceParam)obj).DataType.ToString(),
                                   ((RptViewDataSourceParam)obj).DefaultValue.ToString()};
        }

        protected override string[] GetColumnHeaderText()
        {
            // TODO: 调整字段值的顺序，使之与Grid的列对应
            return new string[] {	
									"ParameterSequence",
									"ParameterName",
                                    "Description",
                                    "DataType",
									"DefaultValue"
									};
        }
        #endregion

    }
}

