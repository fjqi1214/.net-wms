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
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Web.TSModel;

namespace BenQGuru.eMES.Web
{
    public partial class FErrorCodeItem2RouteMP : BaseMPage
    {
        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected BenQGuru.eMES.TSModel.TSModelFacade _facade;

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.txtErrorCodeQuery.Text = Request.QueryString["ErrorCode"];
            }
        }
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("ErrorCodeA", "不良代码", null);
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ReworkRouteCode", "返工途程", null);
            this.gridHelper.AddColumn("OPCode", "工序代码", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
                                ((ErrorCodeItem2Route)obj).ErrorCode.ToString(),
                                ((ErrorCodeItem2Route)obj).ItemCode.ToString(),
                                ((ErrorCodeItem2Route)obj).RouteCode.ToString(),
                                ((ErrorCodeItem2Route)obj).OPCode.ToString(),
                                ((ErrorCodeItem2Route)obj).MaintainUser.ToString(),
                                FormatHelper.ToDateString(((ErrorCodeItem2Route)obj).MaintainDate),
                                FormatHelper.ToTimeString(((ErrorCodeItem2Route)obj).MaintainTime),
                                ""});
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            return this._facade.QueryErrorCodeItem2Route(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                GlobalVariables.CurrentOrganizations.First().OrganizationID,
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            return this._facade.QueryErrorCodeItem2RouteCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                GlobalVariables.CurrentOrganizations.First().OrganizationID);
        }

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }

            if (((ErrorCodeItem2Route)domainObject).RouteCode.ToUpper() != "TS"
                && ((ErrorCodeItem2Route)domainObject).RouteCode.Trim().Length > 0)
            {
                //检查途程是不是和产品挂起来了
                BenQGuru.eMES.MOModel.ItemFacade itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);
                object ir = itemFacade.GetItem2Route(((ErrorCodeItem2Route)domainObject).ItemCode, ((ErrorCodeItem2Route)domainObject).RouteCode, ((ErrorCodeItem2Route)domainObject).OrganizationID.ToString());
                if (ir == null)
                {
                    throw new Exception("$Error_ItemRoute_NotExist");
                }
            }

            if (((ErrorCodeItem2Route)domainObject).RouteCode.Trim().Length > 0)
            {
                BenQGuru.eMES.BaseSetting.BaseModelFacade baseModelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                object route2op = baseModelFacade.GetRoute2Operation(((ErrorCodeItem2Route)domainObject).RouteCode.ToUpper(), ((ErrorCodeItem2Route)domainObject).OPCode.ToUpper());
                if (route2op == null)
                {
                    throw new Exception("$Error_RouteHasNoOperations");
                }
            }

            this._facade.AddErrorCodeItem2Route((ErrorCodeItem2Route)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            this._facade.DeleteErrorCodeItem2Route((ErrorCodeItem2Route[])domainObjects.ToArray(typeof(ErrorCodeItem2Route)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }

            if (((ErrorCodeItem2Route)domainObject).RouteCode.ToUpper() != "TS"
                && ((ErrorCodeItem2Route)domainObject).RouteCode.Trim().Length > 0)
            {
                //检查途程是不是和产品挂起来了
                BenQGuru.eMES.MOModel.ItemFacade itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);
                object ir = itemFacade.GetItem2Route(((ErrorCodeItem2Route)domainObject).ItemCode, ((ErrorCodeItem2Route)domainObject).RouteCode, ((ErrorCodeItem2Route)domainObject).OrganizationID.ToString());
                if (ir == null)
                {
                    throw new Exception("$Error_ItemRoute_NotExist");
                }
            }

            if (((ErrorCodeItem2Route)domainObject).RouteCode.Trim().Length > 0)
            {
                BenQGuru.eMES.BaseSetting.BaseModelFacade baseModelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                object route2op = baseModelFacade.GetRoute2Operation(((ErrorCodeItem2Route)domainObject).RouteCode.ToUpper(), ((ErrorCodeItem2Route)domainObject).OPCode.ToUpper());
                if (route2op == null)
                {
                    throw new Exception("$Error_RouteHasNoOperations");
                }
            }

            this._facade.UpdateErrorCodeItem2Route((ErrorCodeItem2Route)domainObject);
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            ErrorCodeItem2Route errorCodeItem2Route = this._facade.CreateNewErrorCodeItem2Route();

            errorCodeItem2Route.ErrorCode = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtErrorCodeQuery.Text), 40);
            errorCodeItem2Route.ItemCode = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtItemCodeEdit.Text), 40);
            errorCodeItem2Route.RouteCode = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtReworkRoute.Text), 40);
            errorCodeItem2Route.OPCode = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtOPCodeEdit.Text), 40);
            errorCodeItem2Route.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            errorCodeItem2Route.ErrorCodeGroupCode = "";
            errorCodeItem2Route.MaintainUser = this.GetUserCode();

            return errorCodeItem2Route;
        }

        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            object obj = _facade.GetErrorCodeItem2Route(row.Cells[1].Text.ToString(), row.Cells[2].Text.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (obj != null)
            {
                return (ErrorCodeItem2Route)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtItemCodeEdit.Text = "";
                this.txtReworkRoute.Text = "";
                this.txtOPCodeEdit.Text = "";
                return;
            }

            this.txtItemCodeEdit.Text = ((ErrorCodeItem2Route)obj).ItemCode.ToString();
            this.txtReworkRoute.Text = ((ErrorCodeItem2Route)obj).RouteCode.ToString();
            this.txtOPCodeEdit.Text = ((ErrorCodeItem2Route)obj).OPCode.ToString();
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblItemCodeEdit, this.txtItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblOPCodeEdit, this.txtOPCodeEdit, 40, true));

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
            return new string[]{
                                   ((ErrorCodeItem2Route)obj).ErrorCode.ToString(),
                                   ((ErrorCodeItem2Route)obj).ItemCode.ToString(),
                                   ((ErrorCodeItem2Route)obj).RouteCode.ToString(),
                                   ((ErrorCodeItem2Route)obj).OPCode.ToString(),
                                   ((ErrorCodeItem2Route)obj).MaintainUser.ToString(),
                                   FormatHelper.ToDateString(((ErrorCodeItem2Route)obj).MaintainDate)
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"不良代码",
                                    "产品代码",
                                    "返工途程",
                                    "工序代码",
                                    "维护用户",
                                    "维护日期" };
        }
        #endregion
    }
}
