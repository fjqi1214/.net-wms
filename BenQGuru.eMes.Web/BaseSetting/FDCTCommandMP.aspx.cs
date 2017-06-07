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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FDCTCommandMP 的摘要说明。
    /// </summary>
    public partial class FDCTCommandMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private BenQGuru.eMES.BaseSetting.BaseModelFacade _facade = null; //new BaseModelFacadeFactory().Create();

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
            this.gridHelper.AddColumn("DctCode", "DCT指令代码", null);
            this.gridHelper.AddColumn("DctDesc", "DCT指令描述", null);
            this.gridHelper.AddLinkColumn("SelectResource", "资源列表", null);

            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("SelectResource").Width = 100;

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["DctCode"] = ((Dct)obj).DctCode.ToString();
            row["DctDesc"] = ((Dct)obj).Dctdesc.ToString();
            row["SelectResource"] = "";
            row["MaintainUser"] = ((Dct)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = ((Dct)obj).MaintainDate.ToString();
            row["MaintainTime"] = ((Dct)obj).MaintainTime.ToString();
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.GetDCT(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDctCommandQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDctCommandDescQuery.Text)),
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.GetDCTCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDctCommandQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDctCommandDescQuery.Text))
            );
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.AddDct((Dct)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.DeleteDct((Dct[])domainObjects.ToArray(typeof(Dct)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.UpdateDct((Dct)domainObject);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtDctCommandEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtDctCommandEdit.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            Dct dct = this._facade.CreateNewDct();
            
            dct.DctCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDctCommandEdit.Text, 40));
            dct.Dctdesc = FormatHelper.CleanString(this.txtDctCommandDescEdit.Text, 100);
            dct.MaintainUser = this.GetUserCode();

            return dct;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("DctCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetDCT(strCode);
            if (obj != null)
            {
                return (Dct)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtDctCommandEdit.Text = "";
                this.txtDctCommandDescEdit.Text = "";
                return;
            }

            this.txtDctCommandDescEdit.Text = ((Dct)obj).Dctdesc.ToString();
            this.txtDctCommandEdit.Text = ((Dct)obj).DctCode.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblDctCommandEdit, this.txtDctCommandEdit, 40, true));
            manager.Add(new LengthCheck(this.lblDctCommandDescEdit, this.txtDctCommandDescEdit, 100, false));


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
            return new string[]{  ((Dct)obj).DctCode.ToString(),
								   ((Dct)obj).Dctdesc.ToString(),
								   ((Dct)obj).GetDisplayText("MaintainUser"),
                                   ((Dct)obj).MaintainDate.ToString(),
                                   ((Dct)obj).MaintainTime.ToString()};								  
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"DctCode",
									"DctDesc",									
									"MaintainUser",
									"MaintainDate",
                                    "MaintainTime"};
        }

        #endregion


        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "SelectResource")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FDCTResourceSP.aspx", new string[] { "dctCode" }, new string[] { row.Items.FindItemByKey("DctCode").Value.ToString() }));
            }
        }

        private object[] GetAllOrg()
        {
            BaseModelFacade facadeBaseModel = new BaseModelFacade(base.DataProvider);
            return facadeBaseModel.GetCurrentOrgList();
        }
    }
}
