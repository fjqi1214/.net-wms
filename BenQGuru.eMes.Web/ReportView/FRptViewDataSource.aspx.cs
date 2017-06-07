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
using BenQGuru.eMES.Domain.ReportView;
using BenQGuru.eMES.ReportView;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.ReportView
{
    public partial class FRptViewDataSource : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private ReportViewFacade _facade = null;
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
            this.languageComponent1.LanguagePackageDir = "D:\\code\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.drpdbtype.Items.Clear();
                this.drpdbtype.Items.Add(new ListItem(this.languageComponent1.GetString(DataSourceType.SQL), DataSourceType.SQL));
                this.drpdbtype.Items.Add(new ListItem(this.languageComponent1.GetString(DataSourceType.DLL), DataSourceType.DLL));
            }

        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("DataSourceID", "ID", null);
            this.gridHelper.AddColumn("NAME", "名称", null);
            this.gridHelper.AddColumn("DESCRIPTIONTest", "描述", null);
            this.gridHelper.AddColumn("SERVICENAME", "数据库", null);
            this.gridHelper.AddColumn("SOURCETYPE", "数据源类型", null);
            this.gridHelper.AddColumn("SQL", "数据源", null);
            this.gridHelper.AddLinkColumn("SelectParm", "参数", null);
            this.gridHelper.AddLinkColumn("SelectDesc", "栏位描述", null);
            this.gridHelper.AddLinkColumn("SelectData", "预览数据", null);
            this.gridHelper.AddDefaultColumn(true, true);
            this.gridWebGrid.Columns.FromKey("DataSourceID").Hidden = true;
            this.gridWebGrid.Columns.FromKey("SelectData").Hidden = true;
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            object viewobj = _facade.GetRptViewDataConnect(((RptViewDataSource)obj).DataConnectID);
            DataRow row = DtSource.NewRow();
            row["DataSourceID"] = ((RptViewDataSource)obj).DataSourceID.ToString();
            row["NAME"] = ((RptViewDataSource)obj).Name.ToString();
            row["DESCRIPTIONTest"] = ((RptViewDataSource)obj).Description.ToString();
            row["SERVICENAME"] = ((RptViewDataConnect)viewobj).ConnectName.ToString();
            row["SOURCETYPE"] = ((RptViewDataSource)obj).SourceType.ToString();
            row["SQL"] = ((RptViewDataSource)obj).SQL.ToString();
            row["SelectParm"] = "";
            row["SelectDesc"] = "";
            row["SelectData"] = "";
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            return this._facade.QueryDataSource(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCodeQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            return this._facade.QueryDataSourceCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCodeQuery.Text)));
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            ((RptViewDataSource)domainObject).DataSourceID = Convert.ToDecimal(_facade.GetRptViewDataSourceNextId());
            base.DataProvider.BeginTransaction();
            try
            {
                this._facade.AddRptViewDataSource((RptViewDataSource)domainObject);
                this._facade.SetColumn(Server.MapPath("").ToString().Substring(0, Server.MapPath("").ToString().LastIndexOf("\\")), ((RptViewDataSource)domainObject).DllFileName, ((RptViewDataSource)domainObject).DataSourceID, this.GetUserCode());
                base.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                base.DataProvider.RollbackTransaction();
                string msg = this.languageComponent1.GetString(ex.Message);
                if (ex.Message == "$Error_Command_Execute")
                {
                    msg = "$DataSource_IS_Wrong";
                }

                if (msg == "" || msg == null)
                {
                    msg = ex.Message;
                }


                WebInfoPublish.Publish(this, msg, this.languageComponent1);
            }
        }
        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {

            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            this._facade.DeleteDataSourceWithColumnParam((RptViewDataSource[])domainObjects.ToArray(typeof(RptViewDataSource)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            ((RptViewDataSource)domainObject).DataSourceID = Convert.ToDecimal(this.datasourceid.Value);
            base.DataProvider.BeginTransaction();
            try
            {
                this._facade.UpdateRptViewDataSource((RptViewDataSource)domainObject);
                this._facade.SetColumn(Server.MapPath("").ToString().Substring(0, Server.MapPath("").ToString().LastIndexOf("\\")), ((RptViewDataSource)domainObject).DllFileName, ((RptViewDataSource)domainObject).DataSourceID, this.GetUserCode());
                base.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                base.DataProvider.RollbackTransaction();
                string msg = this.languageComponent1.GetString(ex.Message);
                if (ex.Message == "$Error_Command_Execute")
                {
                    msg = "$DataSource_IS_Wrong";
                }

                if (msg == "" || msg == null)
                {
                    msg = ex.Message;
                }

                WebInfoPublish.Publish(this, msg, this.languageComponent1);
            }

        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            //    if (pageAction == PageActionType.Add)
            //    {
            //        this..ReadOnly = false;
            //    }

            //    if (pageAction == PageActionType.Update)
            //    {
            //        this.txtShiftCodeEdit.ReadOnly = true;
            //    }
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            RptViewDataSource rptviewdatasource = this._facade.CreateNewRptViewDataSource();

            rptviewdatasource.Name = FormatHelper.CleanString(this.txtNameEdit.Text);
            rptviewdatasource.Description = FormatHelper.CleanString(this.txtDescriptEdit.Text, 100);
            rptviewdatasource.DataConnectID = Convert.ToDecimal(this.drpDBTypeEdit.SelectedValue);// FormatHelper.TOTimeInt(this.timeShiftEndTimeEdit.Text);
            rptviewdatasource.SourceType = this.drpdbtype.SelectedValue;
            if (this.drpdbtype.SelectedIndex == 0)
            {
                rptviewdatasource.SQL = this.txtDBDefaultNameEdit.Text;
            }
            else
            {
                rptviewdatasource.DllFileName = this.txtDBDefaultNameEdit.Text;
            }
            rptviewdatasource.MaintainUser = this.GetUserCode();
            return rptviewdatasource;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            object obj = _facade.GetRptViewDataSource(Convert.ToDecimal(row.Items.FindItemByKey("DataSourceID").Text));

            if (obj != null)
            {
                return obj as RptViewDataSource;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtNameEdit.Text = "";
                this.txtDescriptEdit.Text = "";
                this.txtDBDefaultNameEdit.Text = "";
                this.drpDBTypeEdit.SelectedIndex = -1;
                this.drpdbtype.SelectedIndex = 0;
                this.datasourceid.Value = "";
                return;
            }

            this.txtNameEdit.Text = ((RptViewDataSource)obj).Name.ToString();
            this.txtDescriptEdit.Text = ((RptViewDataSource)obj).Description.ToString();
            this.drpDBTypeEdit.SelectedIndex =
                 this.drpDBTypeEdit.Items.IndexOf(this.drpDBTypeEdit.Items.FindByValue(((RptViewDataSource)obj).DataConnectID.ToString()));
            drpdbtype.SelectedIndex =
                this.drpdbtype.Items.IndexOf(this.drpdbtype.Items.FindByValue(((RptViewDataSource)obj).SourceType));
            if (this.drpdbtype.SelectedIndex == 0)
            {
                this.txtDBDefaultNameEdit.Text = ((RptViewDataSource)obj).SQL.ToString();
            }
            else
            {
                this.txtDBDefaultNameEdit.Text = ((RptViewDataSource)obj).DllFileName.ToString();
            }
            this.datasourceid.Value = ((RptViewDataSource)obj).DataSourceID.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblnameEdit, this.txtNameEdit, 40, true));
            manager.Add(new LengthCheck(lblDBEdit, this.txtDBDefaultNameEdit, 4000, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }

        #endregion
        #region 数据初始化
        protected void drpDBTypeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.drpDBTypeEdit);
                if (_facade == null)
                {
                    _facade = new ReportViewFacade(base.DataProvider);
                }
                builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(_facade.GetAllRptViewDataConnect);

                builder.Build("ConnectName", "DataConnectID");
            }
        }
        #endregion


        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((RptViewDataSource)obj).Name.ToString(),
                                   ((RptViewDataSource)obj).Description.ToString(),
                                   ((RptViewDataSource)obj).SourceType.ToString(),
                                   ((RptViewDataSource)obj).SQL.ToString()};
        }

        protected override string[] GetColumnHeaderText()
        {
            //TODO: 调整字段值的顺序，使之与Grid的列对应
            return new string[] {	
                                    "Name",
                                    "Description",
                                    "SOURCETYPE",
                                    "SQL"};
        }
        #endregion

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "SelectParm" && row.Items.FindItemByKey("SOURCETYPE").Text.ToUpper().Contains("DLL"))
            {
                this.Response.Redirect(this.MakeRedirectUrl("../ReportView/FRptViewDataSourceParam.aspx", new string[] { "id" }, new string[] { row.Items.FindItemByKey("DataSourceID").Text.Trim() }));
            }
            else if (commandName == "SelectDesc")
            {
                this.Response.Redirect(this.MakeRedirectUrl("../ReportView/FRptViewDataSourceColumn.aspx", new string[] { "id" }, new string[] { row.Items.FindItemByKey("DataSourceID").Text.Trim() }));
            }

        }


    }
}


