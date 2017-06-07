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
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FInvBusinessMP : BaseMPageNew
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
                BuildOrgList();
                this.rdoTypeIn.Checked = false;
                this.rdoTypeOut.Checked = false;
                //    this.rdoTypeProduce.Checked = false;
                //    this.rdoTypeNoneProduce.Checked = false;
                //this.lblBusinessReasonEdit.Visible = false;
                //this.DropDownListBusinessReason.Visible = false;
            }

            //if (rdoTypeIn.Checked)
            //{
            //    this.rdoTypeProduce.Enabled = true;
            //    this.rdoTypeNoneProduce.Enabled = true;
            //}
            //else
            //{
            //    this.rdoTypeProduce.Enabled = false;
            //    this.rdoTypeNoneProduce.Enabled = false;
            //}

            this.lblBusinessReasonEdit.Visible = false;
            this.DropDownListBusinessReason.Visible = false;
            //if (rdoTypeIn.Checked)
            //{
            //    this.DropDownListBusinessReason.Visible = true;
            //}
            //if (rdoTypeOut.Checked)
            //{
            //    this.DropDownListBusinessReason.Visible = false;
            //}

        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected void drpBusinessType_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpBusinessTypeQuery.Items.Insert(0, new ListItem("", ""));

                this.drpBusinessTypeQuery.Items.Insert(1, new ListItem(languageComponent1.GetString(BussinessType.type_in), BussinessType.type_in));

                this.drpBusinessTypeQuery.Items.Insert(2, new ListItem(languageComponent1.GetString(BussinessType.type_out), BussinessType.type_out));
            }
        }

        protected void rdoTypeIn_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rdoTypeOut.Checked)
            {
                this.rdoTypeOut.Checked = false;

            }

            //入库类型
            if (rdoTypeIn.Checked)
            {
                this.lblBusinessReasonEdit.Visible = true;
                this.DropDownListBusinessReason.Visible = true;
            }
        }

        protected void rdoTypeOut_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rdoTypeIn.Checked)
            {
                this.rdoTypeIn.Checked = false;

            }

            //入库类型
            if (this.rdoTypeOut.Checked)
            {
                this.lblBusinessReasonEdit.Visible = false;
                this.DropDownListBusinessReason.Visible = false;

            }

        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("BusinessCode", "业务类型代码", null);
            this.gridHelper.AddColumn("BusinessDescription", "业务类型描述", null);
            this.gridHelper.AddColumn("BusinessType", "业务类型", null);
            this.gridHelper.AddColumn("BusinessReason", "入库类型", null);
            this.gridHelper.AddColumn("IsFIFO", "是否先进先出", null);
            // this.gridHelper.AddColumn("BusinessReason", "入库类型", null);
            this.gridHelper.AddColumn("MaintainUser", "最后维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "最后维护日期", null);
            //this.gridHelper.AddLinkColumn("FormulaCode", "规则", null);
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((InvBusiness)obj).BusinessCode.ToString(),
            //                    ((InvBusiness)obj).BusinessDescription.ToString(),
            //                    languageComponent1.GetString(((InvBusiness)obj).BusinessType.ToString()),
            //                    this.languageComponent1.GetString(((InvBusiness)obj).BusinessReason.ToString()),
            //                   // ((InvBusiness)obj).MaintainUser.ToString(),
            //                  this.languageComponent1.GetString(((InvBusiness)obj).ISFIFO .ToString()),
            //                    ((InvBusiness)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((InvBusiness)obj).MaintainDate),
                                 

            //                }
            //                                                      );

            DataRow row = this.DtSource.NewRow();
            row["BusinessCode"] = ((InvBusiness)obj).BusinessCode.ToString();
            row["BusinessDescription"] = ((InvBusiness)obj).BusinessDescription.ToString();
            row["BusinessType"] = languageComponent1.GetString(((InvBusiness)obj).BusinessType.ToString());
            row["BusinessReason"] = this.languageComponent1.GetString(((InvBusiness)obj).BusinessReason.ToString());
            row["IsFIFO"] = this.languageComponent1.GetString(((InvBusiness)obj).ISFIFO.ToString());
            row["MaintainUser"] = ((InvBusiness)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((InvBusiness)obj).MaintainDate);
            return row;


        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QueryInvBusiness(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBusinessCodeQuery.Text)), FormatHelper.CleanString(this.txtBusinessDescriptionQuery.Text),
                this.drpBusinessTypeQuery.SelectedValue, GlobalVariables.CurrentOrganizations.First().OrganizationID,
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QueryInvBusinessCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBusinessCodeQuery.Text)), FormatHelper.CleanString(this.txtBusinessDescriptionQuery.Text),
                this.drpBusinessTypeQuery.SelectedValue, GlobalVariables.CurrentOrganizations.First().OrganizationID
            );
        }


        private void BuildOrgList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListOrg);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllOrg);
            builder.Build("OrganizationDescription", "OrganizationID");
            this.DropDownListOrg.Items.Insert(0, new ListItem("", ""));

            this.DropDownListOrg.SelectedIndex = 0;
        }

        private object[] GetAllOrg()
        {
            BaseModelFacade facadeBaseModel = new BaseModelFacade(base.DataProvider);
            return facadeBaseModel.GetCurrentOrgList();
        }


        #endregion

        #region Button

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "operation")
            {
                this.Response.Redirect(this.MakeRedirectUrl("FRoute2OperationSP.aspx", new string[] { "routecode" },
                    new string[] { row.Items.FindItemByKey("RouteCode").Value.ToString() }));
            }
        }

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InvBusiness invBusiness = domainObject as InvBusiness;

            object obj = this.facade.GetInvBusiness(invBusiness.BusinessCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (obj != null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Primary_Key_Overlap");
            }

            invBusiness.MaintainUser = this.GetUserCode();
            invBusiness.MaintainDate = dateTime.DBDate;
            invBusiness.MaintainTime = dateTime.DBTime;

            this.facade.AddInvBusiness(invBusiness);

            //this.rdoTypeProduce.Enabled = false;
            //this.rdoTypeNoneProduce.Enabled = false;
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            this.facade.DeleteInvBusiness((InvBusiness[])domainObjects.ToArray(typeof(InvBusiness)));

            //this.rdoTypeProduce.Enabled = false;
            //this.rdoTypeNoneProduce.Enabled = false;
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }

            DBDateTime dateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            InvBusiness invBusiness = domainObject as InvBusiness;
            invBusiness.MaintainUser = this.GetUserCode();
            invBusiness.MaintainDate = dateTime.DBDate;
            invBusiness.MaintainTime = dateTime.DBTime;

            this.facade.UpdateInvBusiness(invBusiness);

            //this.rdoTypeProduce.Enabled = false;
            //this.rdoTypeNoneProduce.Enabled = false;
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtBusinessCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtBusinessCodeEdit.ReadOnly = true;
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

            InvBusiness invBusiness = this.facade.CreateInvBusiness();

            invBusiness.BusinessCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBusinessCodeEdit.Text, 40));
            invBusiness.BusinessDescription = FormatHelper.CleanString(this.txtBusinessDescriptionEdit.Text, 100);

            if (rdoTypeIn.Checked)
            {
                invBusiness.BusinessType = BussinessType.type_in;
                invBusiness.BusinessReason = this.DropDownListBusinessReason.SelectedValue;
            }
            if (rdoTypeOut.Checked)
            {
                invBusiness.BusinessType = BussinessType.type_out;
            }

            invBusiness.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            invBusiness.MaintainUser = this.GetUserCode();
            invBusiness.ISFIFO = this.chbIsFIFO.Checked ? "Y" : "N";

            return invBusiness;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            object obj = facade.GetInvBusiness(row.Items.FindItemByKey("BusinessCode").Value.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (obj != null)
            {
                return (InvBusiness)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtBusinessCodeEdit.Text = "";
                this.txtBusinessDescriptionEdit.Text = "";
                this.rdoTypeIn.Checked = false;
                this.rdoTypeOut.Checked = false;
                this.DropDownListOrg.SelectedIndex = 0;
                return;
            }

            string businessType = ((InvBusiness)obj).BusinessType.ToString();



            this.rdoTypeIn.Checked = false;
            this.rdoTypeOut.Checked = false;
            try
            {
                this.DropDownListOrg.SelectedValue = ((InvBusiness)obj).OrgID.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = 0;
            }

            if (businessType == BussinessType.type_in)
            {
                this.rdoTypeIn.Checked = true;
                this.rdoTypeOut.Checked = false;

                this.lblBusinessReasonEdit.Visible = true;
                this.DropDownListBusinessReason.Visible = true;
                //入库类型
                try
                {
                    this.DropDownListBusinessReason.SelectedValue = ((InvBusiness)obj).BusinessReason.ToString();
                }
                catch
                {
                    this.DropDownListBusinessReason.SelectedIndex = 0;
                }
            }
            if (businessType == BussinessType.type_out)
            {
                this.rdoTypeOut.Checked = true;
                this.rdoTypeIn.Checked = false;
                this.lblBusinessReasonEdit.Visible = false;
                this.DropDownListBusinessReason.Visible = false;
            }

            this.txtBusinessCodeEdit.Text = ((InvBusiness)obj).BusinessCode.ToString();
            this.txtBusinessDescriptionEdit.Text = ((InvBusiness)obj).BusinessDescription.ToString();
            this.chbIsFIFO.Checked = ((InvBusiness)obj).ISFIFO.ToString() == "Y" ? true : false;
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblBusinessCodeEdit, this.txtBusinessCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblBusinessDescriptionEdit, this.txtBusinessDescriptionEdit, 100, true));
            manager.Add(new LengthCheck(this.lblOrgEdit, DropDownListOrg, 8, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (rdoTypeIn.Checked == false && rdoTypeOut.Checked == false)
            {
                WebInfoPublish.Publish(this, "$Must_Chose_One", this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((InvBusiness)obj).BusinessCode.ToString(),
                                ((InvBusiness)obj).BusinessDescription.ToString(),
                                languageComponent1.GetString(((InvBusiness)obj).BusinessType.ToString()),
                                languageComponent1.GetString(((InvBusiness)obj).BusinessReason.ToString()),
                                languageComponent1 .GetString (((InvBusiness )obj ).ISFIFO .ToString ()),
                                //((InvBusiness)obj).MaintainUser.ToString(),
                               ((InvBusiness)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((InvBusiness)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"BusinessCode",
                                    "BusinessDescription",
                                    "BusinessType",
                                    "BusinessReason",
                                    "IsFIFO",
	                                "MaintainUser",
                                    "MaintainDate"};
        }

        #endregion

        #region 入库类型
        protected void DropDownListBusinessReason_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.DropDownListBusinessReason.Items.Clear();
                this.DropDownListBusinessReason.Items.Insert(0, new ListItem(this.languageComponent1.GetString(BussinessReason.type_produce), BussinessReason.type_produce));
                this.DropDownListBusinessReason.Items.Insert(1, new ListItem(this.languageComponent1.GetString(BussinessReason.type_noneproduce), BussinessReason.type_noneproduce));
            }
        }
        #endregion
    }
}
