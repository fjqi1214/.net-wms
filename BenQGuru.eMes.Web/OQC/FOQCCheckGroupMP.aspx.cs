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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.OQC;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.OQC
{
    /// <summary>
    /// FResourceMP 的摘要说明。
    /// </summary>
    public partial class FOQCCheckGroupMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        protected System.Web.UI.WebControls.TextBox txtShiftTypeEdit;

        private BenQGuru.eMES.OQC.OQCFacade _facade = null;

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
            if (!this.IsPostBack)
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
            this.gridHelper.AddColumn("CheckGroupCode", "检验类型", null);
            this.gridHelper.AddLinkColumn("CheckItemCode", "检验项目", null);
            this.gridHelper.AddColumn("MaintainUser", "最后维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "最后维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "最后维护时间", null);

           // this.gridWebGrid.Columns.FromKey("CheckItemCode").Width = 100;

            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["CheckGroupCode"] = ((OQCCheckGroup)obj).CheckGroupCode.ToString();
            row["CheckItemCode"] = "";
            row["MaintainUser"] = ((OQCCheckGroup)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString((obj as OQCCheckGroup).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString((obj as OQCCheckGroup).MaintainTime);
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new OQCFacade(base.DataProvider);
            }
            return this._facade.QueryOQCCheckGroup(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtChkGroupQuery.Text)),
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new OQCFacade(base.DataProvider);
            }
            return this._facade.QueryOQCCheckGroupCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtChkGroupQuery.Text))
            );
        }

        #endregion

        #region Button

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "CheckItemCode")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FOQCCheckGroup2List.aspx", new string[] { "checkGroup" }, new string[] { row.Items.FindItemByKey("CheckGroupCode").Value.ToString() }));
            }
           
        }

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new OQCFacade(base.DataProvider);
            }
            this._facade.AddOQCCheckGroup((OQCCheckGroup)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new OQCFacade(base.DataProvider);
            }
            this._facade.DeleteOQCCheckGroup((OQCCheckGroup[])domainObjects.ToArray(typeof(OQCCheckGroup)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new OQCFacade(base.DataProvider);
            }
            this._facade.UpdateOQCCheckGroup((OQCCheckGroup)domainObject);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtChkGroupEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtChkGroupEdit.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new OQCFacade(base.DataProvider);
            }
            OQCCheckGroup checkGroup = this._facade.CreateNewOQCCheckGroup();

            checkGroup.CheckGroupCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtChkGroupEdit.Text, 40));
            checkGroup.MaintainUser = base.GetUserCode();

            return checkGroup;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new OQCFacade(base.DataProvider);
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("CheckGroupCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetOQCCheckGroup(strCode);
            if (obj != null)
            {
                return (OQCCheckGroup)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtChkGroupEdit.Text = "";

                return;
            }

            this.txtChkGroupEdit.Text = ((OQCCheckGroup)obj).CheckGroupCode.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblChkGroupEdit, this.txtChkGroupEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region Export
        // 2005-04-06

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((OQCCheckGroup)obj).CheckGroupCode.ToString(),
                                   //((OQCCheckGroup)obj).MaintainUser.ToString(),
                                  ((OQCCheckGroup)obj).GetDisplayText("MaintainUser"),
                                   FormatHelper.ToDateString(((OQCCheckGroup)obj).MaintainDate),
                                   FormatHelper.ToTimeString(((OQCCheckGroup)obj).MaintainTime)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"CheckGroupCode",
                                    "MaintainUser",
                                    "MaintainDate",	
                                    "MaintainTime"};
        }

        #endregion

    }
}