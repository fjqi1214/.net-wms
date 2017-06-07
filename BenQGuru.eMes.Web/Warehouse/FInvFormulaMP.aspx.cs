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
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FInvFormulaMP : BaseMPage
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade facade = null;


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
            this.gridHelper.AddColumn("FormulaCode", "规则代码", null);
            this.gridHelper.AddColumn("FormulaDescription", "规则描述", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
                                ((InvFormula)obj).FormulaCode.ToString(),
                                ((InvFormula)obj).FormulaDesc.ToString(),
                                //((InvFormula)obj).MaintainUser.ToString(),
                                 ((InvFormula)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((InvFormula)obj).MaintainDate)});
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            return this.facade.QueryInvFormula(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFormulaCodeQuery.Text)), FormatHelper.CleanString(this.txtFormulaDescriptionQuery.Text),
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QueryInvFormulaCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFormulaCodeQuery.Text)), FormatHelper.CleanString(this.txtFormulaDescriptionQuery.Text));
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InvFormula invFormula = domainObject as InvFormula;

            object obj = this.facade.GetInvFormula(invFormula.FormulaCode);

            if (obj != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Primary_Key_Overlap");
            }

            invFormula.MaintainUser = this.GetUserCode();
            invFormula.MaintainDate = dateTime.DBDate;
            invFormula.MaintainTime = dateTime.DBTime;

            this.facade.AddInvFormula(invFormula);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            this.facade.DeleteInvFormula((InvFormula[])domainObjects.ToArray(typeof(InvFormula)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            InvFormula invFormula = domainObject as InvFormula;
            invFormula.MaintainUser = this.GetUserCode();
            invFormula.MaintainDate = dateTime.DBDate;
            invFormula.MaintainTime = dateTime.DBTime;

            this.facade.UpdateInvFormula(invFormula);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtFormulaCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtFormulaCodeEdit.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            InvFormula invFormula = this.facade.CreateNewInvFormula();

            invFormula.FormulaCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFormulaCodeEdit.Text, 40));
            invFormula.FormulaDesc = FormatHelper.CleanString(this.txtFormulaDescriptionEdit.Text, 100);
            invFormula.MaintainUser = this.GetUserCode();

            return invFormula;
        }


        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            object obj = facade.GetInvFormula(row.Cells[1].Text.ToString());

            if (obj != null)
            {
                return (InvFormula)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtFormulaCodeEdit.Text = "";
                this.txtFormulaDescriptionEdit.Text = "";

                return;
            }

            this.txtFormulaCodeEdit.Text = ((InvFormula)obj).FormulaCode.ToString();
            this.txtFormulaDescriptionEdit.Text = ((InvFormula)obj).FormulaDesc.ToString();

        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblFormulaCodeEdit, this.txtFormulaCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblFormulaDescriptionEdit, this.txtFormulaDescriptionEdit, 100, true));

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
            return new string[]{((InvFormula)obj).FormulaCode.ToString(),
                                ((InvFormula)obj).FormulaDesc.ToString(),
                                //((InvFormula)obj).MaintainUser.ToString(),
                               ((InvFormula)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((InvFormula)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"FormulaCode",
                                    "FormulaDescription",
                                    "MaintainUser",	
                                    "MaintainDate"};
        }

        #endregion
    }
}
