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
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FErrorCode2OPRework 的摘要说明。
    /// </summary>
    public partial class FErrorCode2OPRework : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private TSModelFacade facade = null;

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

        #region Stable
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtOPCodeQuery.Text = this.GetRequestParam("OPCode");

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region NotStable
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ErrorCode", "不良代码", null);
            this.gridHelper.AddColumn("ErrorCodeDescription", "不良代码描述", null);
            this.gridHelper.AddColumn("ReworkRouteCode", "返工途程", null);
            this.gridHelper.AddColumn("ToOPCode", "返工工序代码", null);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridHelper.AddDefaultColumn(true, true);
            this.gridHelper.RequestData();
        }

        protected void AddDomainObject(ArrayList domainObject)
        {
            if (facade == null)
            {
                facade = new TSModelFacade(this.DataProvider);
            }

            this.facade.AddErrorCode2OPRework((ErrorCode2OPRework[])domainObject.ToArray(typeof(ErrorCode2OPRework)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new TSModelFacade(this.DataProvider);
            }
            this.facade.UpdateErrorCode2OPRework((ErrorCode2OPRework)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new TSModelFacade(this.DataProvider);
            }
            this.facade.DeleteErrorCode2OPRework((ErrorCode2OPRework[])domainObjects.ToArray(typeof(ErrorCode2OPRework)));
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new TSModelFacade(this.DataProvider);
            }
            return this.facade.QueryErrorCode2OPReworkCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeQuery.Text)), FormatHelper.CleanString(this.txtErrorCodeDescQuery.Text));
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["ErrorCode"] = ((ErrorCode2OPReworkNew)obj).ErrorCode.ToString();
            row["ErrorCodeDescription"] = ((ErrorCode2OPReworkNew)obj).ErrorCodeDesc;
            row["ReworkRouteCode"] = ((ErrorCode2OPReworkNew)obj).RouteCode;
            row["ToOPCode"] = ((ErrorCode2OPReworkNew)obj).GetDisplayText("ToOPCode");
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new TSModelFacade(this.DataProvider);
            }
            return facade.QueryErrorCode2OPRework(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeDescQuery.Text)),
                inclusive, exclusive);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FOperationMP.aspx"));
        }

        protected override void cmdAdd_Click(object sender, EventArgs e)
        {
            if (this.ValidateInput())
            {
                ArrayList objList = new ArrayList();
                object obj = null;
                string errorCode = this.txtErrorCode.Text;
                string[] errorCodeArray = errorCode.Split(',');

                foreach (string errorCodeNew in errorCodeArray)
                {
                    bool exist = false;
                    foreach (ErrorCode2OPRework ec2op in objList)
                    {
                        if (string.Compare(ec2op.ErrorCode, errorCodeNew, true) == 0)
                        {
                            exist = true;
                            break;
                        }
                    }

                    if (!exist)
                    {
                        obj = this.GetObject(errorCodeNew);
                        if (obj == null)
                        {
                            return;
                        }
                        else
                        {
                            objList.Add(obj);
                        }
                    }
                }

                if (objList.Count > 0)
                {
                    this.AddDomainObject(objList);
                }

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected object GetObject(string list)
        {
            if (facade == null)
            {
                facade = new TSModelFacade(this.DataProvider);
            }

            object obj = this.facade.GetErrorCode2OPRework(this.txtOPCodeQuery.Text, list, GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (obj != null)
            {
                WebInfoPublish.Publish(this, list + "$Error_Primary_Key_Overlap", this.languageComponent1);
                return null;
            }

            ErrorCode2OPRework errorCode2OPRework = facade.CreateNewErrorCode2OPRework();

            errorCode2OPRework.OPCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeQuery.Text));
            errorCode2OPRework.ErrorCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(list));
            errorCode2OPRework.RouteCode = FormatHelper.CleanString(this.txtReworkRoute.Text);
            errorCode2OPRework.ToOPCode = FormatHelper.CleanString(this.txtReworkOPCode.Text);
            errorCode2OPRework.MaintainUser = this.GetUserCode();
            errorCode2OPRework.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            return errorCode2OPRework;
        }

        protected override object GetEditObject()
        {
            if (facade == null)
            {
                facade = new TSModelFacade(this.DataProvider);
            }

            ErrorCode2OPRework errorCode2OPRework = facade.CreateNewErrorCode2OPRework();

            errorCode2OPRework.OPCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeQuery.Text));
            errorCode2OPRework.ErrorCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCode.Text));
            errorCode2OPRework.RouteCode = FormatHelper.CleanString(this.txtReworkRoute.Text);
            errorCode2OPRework.ToOPCode = FormatHelper.CleanString(this.txtReworkOPCode.Text);
            errorCode2OPRework.MaintainUser = this.GetUserCode();
            errorCode2OPRework.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            return errorCode2OPRework;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new TSModelFacade(this.DataProvider);
            }
            object obj = this.facade.GetErrorCode2OPRework(this.txtOPCodeQuery.Text, row.Items.FindItemByKey("ErrorCode").Text.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (obj != null)
            {
                return (ErrorCode2OPRework)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtErrorCode.Text = "";
                this.txtReworkOPCode.Text = "";
                this.txtReworkRoute.Text = "";
                return;
            }

            this.txtErrorCode.Text = ((ErrorCode2OPRework)obj).ErrorCode.ToString();
            this.txtReworkRoute.Text = ((ErrorCode2OPRework)obj).RouteCode.ToString();
            this.txtReworkOPCode.Text = ((ErrorCode2OPRework)obj).ToOPCode.ToString();
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtErrorCode.Readonly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtErrorCode.Readonly = true;
            }
        }
        #endregion

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblNGCodeEdit, this.txtErrorCode, 65535, true));
            manager.Add(new LengthCheck(this.lblReworkOPCodeEdit, this.txtReworkOPCode, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (this.txtReworkRoute.Text.Trim().Length != 0)
            {
                BenQGuru.eMES.BaseSetting.BaseModelFacade baseModelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                object route2op = baseModelFacade.GetRoute2Operation(this.txtReworkRoute.Text.ToUpper(), this.txtReworkOPCode.Text.ToUpper());
                if (route2op == null)
                {
                    WebInfoPublish.Publish(this, "$Error_RouteHasNoOperations", this.languageComponent1);
                    return false;
                }
            }

            return true;
        }

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((ErrorCode2OPReworkNew)obj).ErrorCode.ToString(),
								   ((ErrorCode2OPReworkNew)obj).ErrorCodeDesc.ToString(),
								   ((ErrorCode2OPReworkNew)obj).RouteCode,
                                   ((ErrorCode2OPReworkNew)obj).GetDisplayText("ToOPCode")};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"ErrorCode",
									"ErrorCodeDescription",	
									"ReworkRouteCode",
                                    "ToOPCode"};
        }

        #endregion
    }
}
