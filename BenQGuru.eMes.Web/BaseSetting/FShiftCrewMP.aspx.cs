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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FShiftCrewMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        protected System.Web.UI.WebControls.TextBox txtShiftTypeEdit;

        private ShiftModel _facade = null;

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
            this.gridHelper.AddColumn("CrewCode", "班组代码", null);
            this.gridHelper.AddLinkColumn("ResourceCode", "资源列表", 100);
            this.gridHelper.AddColumn("CrewDesc", "班组描述", null);

            this.gridHelper.AddDefaultColumn(true, true);
            //this.gridWebGrid.Columns.FromKey("ResourceCode").Width = 100;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((ShiftCrew)obj).CrewCode.ToString(),
            //                    "",
            //                    ((ShiftCrew)obj).CrewDesc.ToString()});
            DataRow row = this.DtSource.NewRow();
            row["CrewCode"] = ((ShiftCrew)obj).CrewCode.ToString();
            row["CrewDesc"] = ((ShiftCrew)obj).CrewDesc.ToString();
            return row;

        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new ShiftModel(base.DataProvider);
            }
            return this._facade.QueryShiftCrew(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCrewCodeQuery.Text)), FormatHelper.CleanString(this.txtCrewDescQuery.Text), 
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new ShiftModel(base.DataProvider);
            }
            return this._facade.QueryShiftCrewCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCrewCodeQuery.Text)), FormatHelper.CleanString(this.txtCrewDescQuery.Text)
            );
        }

        #endregion

        #region Button

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
          //  base.Grid_ClickCell(cell);

            if (commandName=="ResourceCode")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FShiftCrewResourceSP.aspx",
                    new string[] { "crewCode" },
                    new string[] { row.Items.FindItemByKey("CrewCode").Text.Trim() }));
            }
        }

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ShiftModel(base.DataProvider);
            }
            this._facade.AddShiftCrew((ShiftCrew)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new ShiftModel(base.DataProvider);
            }
            this._facade.DeleteShiftCrew((ShiftCrew[])domainObjects.ToArray(typeof(ShiftCrew)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ShiftModel(base.DataProvider);
            }
            this._facade.UpdateShiftCrew((ShiftCrew)domainObject);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtCrewCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtCrewCodeEdit.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new ShiftModel(base.DataProvider);
            }
            ShiftCrew shiftCrew = this._facade.CreateNewShiftCrew();

            shiftCrew.CrewCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCrewCodeEdit.Text));
            shiftCrew.CrewDesc = FormatHelper.CleanString(this.txtCrewDescEdit.Text);
            shiftCrew.MaintainUser = base.GetUserCode();

            return shiftCrew;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new ShiftModel(base.DataProvider);
            }
            object obj = _facade.GetShiftCrew(row.Items.FindItemByKey("CrewCode").Text.ToString());

            if (obj != null)
            {
                return (ShiftCrew)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtCrewCodeEdit.Text = "";
                this.txtCrewDescEdit.Text = "";

                return;
            }

            this.txtCrewCodeEdit.Text = ((ShiftCrew)obj).CrewCode.ToString();
            this.txtCrewDescEdit.Text = ((ShiftCrew)obj).CrewDesc.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblCrewCodeEdit, this.txtCrewCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblCrewDescEdit, this.txtCrewDescEdit, 100, false));

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
            return new string[]{  ((ShiftCrew)obj).CrewCode.ToString(),
                                   ((ShiftCrew)obj).CrewDesc.ToString()};

        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"CrewCode",
                                    "CrewDesc"};
        }

        #endregion

    }
}
