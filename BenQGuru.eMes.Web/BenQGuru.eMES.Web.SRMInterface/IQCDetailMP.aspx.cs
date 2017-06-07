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
using BenQGuru.eMES.Domain.IQC;

namespace BenQGuru.eMES.Web.SRMInterface
{
    public partial class IQCDetailMP : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        protected System.Web.UI.WebControls.TextBox txtShiftTypeEdit;

        //private BenQGuru.eMES.BaseSetting.ShiftModelFacade _shiftFacade = null;//new ShiftModelFacade();

        private BenQGuru.eMES.SRMInterface.InterfaceModelFacade _facade = null; //new BaseModelFacadeFactory().Create();

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
                this.txtIQCNo.Text = this.GetRequestParam("IQCNo");
                this.txtASNNo.Text = this.GetRequestParam("STNo");
                this.txtIQCStatus.Text = this.GetRequestParam("Status");
                

                // 
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
            this.gridHelper.AddColumn("STLine", "ASN行", null);
            this.gridHelper.AddColumn("ItemCode", "物料代码", null);
            this.gridHelper.AddColumn("ItemName", "物料描述", null);
            //this.gridHelper.AddColumn("PlanDate", "排程日期", null);
            //this.gridHelper.AddColumn("PlanQty", "排程数量", null);
            this.gridHelper.AddColumn("Unit", "单位", null);

            this.gridHelper.AddColumn("ReceiveQty", "收货数量", null);
            this.gridHelper.AddColumn("OrderNo", "采购订单", null);
            this.gridHelper.AddColumn("OrderLine", "订单行", null);
            this.gridHelper.AddColumn("CheckStatus", "检验结果", null);
            this.gridHelper.AddColumn("SampleQty", "抽样数", null);
            this.gridHelper.AddColumn("NGQty", "不良数", null);
            this.gridHelper.AddColumn("MemoEx", "其他说明", null);
            this.gridHelper.AddColumn("Memo", "Memo", null);
            this.gridHelper.AddColumn("PIC", "PIC确认", null);
            this.gridHelper.AddColumn("Action", "永久性措施说明", null);
            this.gridHelper.AddColumn("STDStatus", "状态", null);

            //this.gridWebGrid.Columns.FromKey("ResourceType").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("ResourceGroup").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("ShiftTypeCode").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("SegmentCode").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;

            if (this.txtIQCStatus.Text == "Close")
            {
                this.gridHelper.AddDefaultColumn(true, false);
            }
            else
            {
                this.gridHelper.AddDefaultColumn(true, true);
            }

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.cmdQuery_Click(null, null);
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            if (this.txtIQCStatus.Text == "Close")
            {
                return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                    new object[]{"false",
								((IQCDetail)obj).STLine.ToString(),
			                    ((IQCDetail)obj).ItemCode.ToString(),
                    			//FormatHelper.ToDateString(((IQCDetail)obj).PlanDate),
                    			//((IQCDetail)obj).PlanQty.ToString(),
                    			((IQCDetail)obj).Unit.ToString(),
                    			((IQCDetail)obj).ReceiveQty.ToString(),
                    			((IQCDetail)obj).OrderNo.ToString(),                    			
                    			((IQCDetail)obj).OrderLine.ToString(),
                                ((IQCDetail)obj).CheckStatus.ToString(),
                                ((IQCDetail)obj).SampleQty.ToString(),
                                ((IQCDetail)obj).NGQty.ToString(),
                                ((IQCDetail)obj).MemoEx.ToString(),
                                ((IQCDetail)obj).Memo.ToString(),
                                ((IQCDetail)obj).PIC.ToString(),
                                ((IQCDetail)obj).Action.ToString(),
                                ((IQCDetail)obj).STDStatus.ToString()
							});
            }
            else
            {
                return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
    new object[]{"false",
								((IQCDetail)obj).STLine.ToString(),
			                    ((IQCDetail)obj).ItemCode.ToString(),
                    			//FormatHelper.ToDateString(((IQCDetail)obj).PlanDate),
                    			//((IQCDetail)obj).PlanQty.ToString(),
                    			((IQCDetail)obj).Unit.ToString(),
                    			((IQCDetail)obj).ReceiveQty.ToString(),
                    			((IQCDetail)obj).OrderNo.ToString(),                    			
                    			((IQCDetail)obj).OrderLine.ToString(),
                                ((IQCDetail)obj).CheckStatus.ToString(),
                                ((IQCDetail)obj).SampleQty.ToString(),
                                ((IQCDetail)obj).NGQty.ToString(),
                                ((IQCDetail)obj).MemoEx.ToString(),
                                ((IQCDetail)obj).Memo.ToString(),
                                ((IQCDetail)obj).PIC.ToString(),
                                ((IQCDetail)obj).Action.ToString(),
                                ((IQCDetail)obj).STDStatus.ToString(),
								""});
            }
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryIQCDetail(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNo.Text)),


                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryIQCDetailCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNo.Text)));
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (this.txtIQCStatus.Text == "Close")
            {
                WebInfoPublish.Publish(this, "$CannotEditClosed", this.languageComponent1);
                return;
            }
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }

            if (_facade.IsIQCDetailExist(((IQCDetail)domainObject)))
            {
                WebInfoPublish.Publish(this, "$LineExist", this.languageComponent1);
                return;
            }

            this._facade.AddIQCDetail((IQCDetail)domainObject, this.GetUserCode());
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (this.txtIQCStatus.Text == "Close")
            {
                WebInfoPublish.Publish(this, "$CannotEditClosed", this.languageComponent1);
                return;
            }
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.DeleteIQCDetail((IQCDetail[])domainObjects.ToArray(typeof(IQCDetail)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.UpdateIQCDetail((IQCDetail)domainObject);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtIQCLineNo.ReadOnly = false;
                this.txtItemNo.ReadOnly = false;
                this.txtItemName.ReadOnly = false;
                //                this.datePlanDate.Enable = "false";
                this.txtPlanQty.ReadOnly = false;
                this.txtUnit.ReadOnly = false;
                this.txtOrderNo.ReadOnly = false;
                this.txtOrderLine.ReadOnly = false;
                this.txtReceiveQty.ReadOnly = false;

            }

            if (pageAction == PageActionType.Update)
            {

                this.txtIQCLineNo.ReadOnly = true;
                this.txtItemNo.ReadOnly = true;
                this.txtItemName.ReadOnly = true;
                //                this.datePlanDate.Enable = "true";
                this.txtPlanQty.ReadOnly = true;
                this.txtUnit.ReadOnly = true;
                this.txtOrderNo.ReadOnly = true;
                this.txtOrderLine.ReadOnly = true;
                this.txtReceiveQty.ReadOnly = true;
            }


        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            //this.ValidateInput();
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            IQCDetail iqcDetail = this._facade.CreateNewIQCDetail();
            iqcDetail.STLine = int.Parse(FormatHelper.CleanString(this.txtIQCLineNo.Text, 6));
            iqcDetail.ItemCode = FormatHelper.CleanString(this.txtItemNo.Text, 40);
            //iqcDetail.PlanDate = FormatHelper.TODateInt(this.datePlanDate.Text);
            //if (FormatHelper.CleanString(this.txtPlanQty.Text).Length != 0)
            //{
            //    iqcDetail.PlanQty = decimal.Parse(FormatHelper.CleanString(this.txtPlanQty.Text));
            //}
            //else
            //{
            //    iqcDetail.PlanQty = 0;
            //}
            iqcDetail.Unit = FormatHelper.CleanString(this.txtUnit.Text, 40);
            iqcDetail.OrderNo = FormatHelper.CleanString(this.txtOrderNo.Text, 40);
            iqcDetail.OrderLine = int.Parse(FormatHelper.CleanString(this.txtOrderLine.Text, 22));
            if (FormatHelper.CleanString(this.txtReceiveQty.Text).Length != 0)
            {
                iqcDetail.ReceiveQty = decimal.Parse(FormatHelper.CleanString(this.txtReceiveQty.Text));
            }
            else
            {
                iqcDetail.ReceiveQty = 0;
            }
            if (this.lblStatusQualified.Checked)
            {
                iqcDetail.CheckStatus = "Qualified";
            }
            if (this.lblStatusUnqualified.Checked)
            {
                iqcDetail.CheckStatus = "UnQualified";
            }
            if (this.lblStatusWaitCheck.Checked)
            {
                iqcDetail.CheckStatus = "WaitCheck";
            }

            if (FormatHelper.CleanString(this.txtSampleQty.Text).Length != 0)
            {
                iqcDetail.SampleQty = decimal.Parse(FormatHelper.CleanString(this.txtSampleQty.Text));
            }
            else
            {
                iqcDetail.SampleQty = 0;
            }
            if (FormatHelper.CleanString(this.txtNGQty.Text).Length != 0)
            {
                iqcDetail.NGQty = decimal.Parse(FormatHelper.CleanString(this.txtNGQty.Text));
            }
            else
            {
                iqcDetail.NGQty = 0;
            }
            iqcDetail.MemoEx = FormatHelper.CleanString(this.txtMemoEx.Text, 100);
            iqcDetail.Memo = FormatHelper.CleanString(this.txtMemo.Text, 100);
            iqcDetail.PIC = FormatHelper.CleanString(this.txtPIC.Text, 100);
            iqcDetail.Action = FormatHelper.CleanString(this.txtAction.Text, 100);

            iqcDetail.IQCNo = FormatHelper.CleanString(this.txtIQCNo.Text, 50);
            iqcDetail.STNo = FormatHelper.CleanString(this.txtASNNo.Text, 40);
            iqcDetail.Attribute = this.drpItemAttribute.SelectedValue;
            iqcDetail.STDStatus = this.txtSTDStatus.Text;
            return iqcDetail;
        }


        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            object obj = _facade.GetIQCDetail(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNo.Text)), row.Cells[1].Text.ToString());

            if (obj != null)
            {
                return (IQCDetail)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtIQCLineNo.Text = "";
                this.txtItemNo.Text = "";
                this.txtItemName.Text = "";
                this.datePlanDate.Text = "";
                this.txtPlanQty.Text = "";
                this.txtUnit.Text = "";
                this.txtOrderNo.Text = "";
                this.txtOrderLine.Text = "";
                this.txtReceiveQty.Text = "";

                this.lblStatusQualified.Checked = false;
                this.lblStatusUnqualified.Checked = false;
                this.lblStatusWaitCheck.Checked = false;
                this.txtSampleQty.Text = "";
                this.txtNGQty.Text = "";
                this.txtMemoEx.Text = "";
                this.txtMemo.Text = "";
                this.txtPIC.Text = "";
                this.txtAction.Text = "";
                this.lblStatusSTS.Checked = false;
                this.drpItemAttribute.SelectedIndex = -1;
                return;
            }

            IQCDetail iqcDetail = (IQCDetail)obj;
            this.txtIQCLineNo.Text = iqcDetail.STLine.ToString();
            this.txtItemNo.Text = iqcDetail.ItemCode;
            //this.datePlanDate.Text = iqcDetail.PlanDate.ToString().Insert(6, "/").Insert(4, "/");
            //this.txtPlanQty.Text = iqcDetail.PlanQty.ToString();
            this.txtUnit.Text = iqcDetail.Unit;
            this.txtOrderNo.Text = iqcDetail.OrderNo;
            this.txtOrderLine.Text = iqcDetail.OrderLine.ToString();
            this.txtReceiveQty.Text = iqcDetail.ReceiveQty.ToString();

            this.lblStatusQualified.Checked = false;
            this.lblStatusUnqualified.Checked = false;
            this.lblStatusWaitCheck.Checked = false;
            if (iqcDetail.CheckStatus.Trim().ToUpper().Equals("WAITCHECK"))
            {
                this.lblStatusWaitCheck.Checked = true;
            }
            if (iqcDetail.CheckStatus.Trim().ToUpper().Equals("QUALIFIED"))
            {
                this.lblStatusQualified.Checked = true;
            }
            if (iqcDetail.CheckStatus.Trim().ToUpper().Equals("UNQUALIFIED"))
            {
                this.lblStatusUnqualified.Checked = true;
            }

            this.txtSampleQty.Text = iqcDetail.SampleQty.ToString();
            this.txtNGQty.Text = iqcDetail.NGQty.ToString();
            this.txtMemoEx.Text = iqcDetail.MemoEx;
            this.txtMemo.Text = iqcDetail.Memo;
            this.txtPIC.Text = iqcDetail.PIC;
            this.txtAction.Text = iqcDetail.Action;
            try
            {
                this.drpItemAttribute.SelectedValue = iqcDetail.Attribute;
            }
            catch
            {
                this.drpItemAttribute.SelectedIndex = -1;
            }


            this.txtSTDStatus.Text = iqcDetail.STDStatus;
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();



            manager.Add(new LengthCheck(this.lblIQCLineNoEdit, this.txtIQCLineNo, 6, true));
            manager.Add(new LengthCheck(this.lblItemCodeEditSRM, this.txtItemNo, 40, true));
            manager.Add(new LengthCheck(this.lblItemNameEditSRM, this.txtItemName, 100, true));
            manager.Add(new LengthCheck(this.lblOrderNo, this.txtOrderNo, 40, true));
            manager.Add(new LengthCheck(this.lblOrderLine, this.txtOrderLine, 22, true));
            //manager.Add(new LengthCheck(this.lblPlanDate, this.datePlanDate, 8, true));
            manager.Add(new LengthCheck(this.lblPlanQty, this.txtPlanQty, 18, true));
            manager.Add(new LengthCheck(this.lblUnit, this.txtUnit, 40, true));
            manager.Add(new LengthCheck(this.lblReceiveQty, this.txtReceiveQty, 18, true));
            manager.Add(new LengthCheck(this.lblSampleQty, this.txtSampleQty, 18, false));
            manager.Add(new LengthCheck(this.lblNGQty, this.txtNGQty, 18, false));
            manager.Add(new LengthCheck(this.lblMemoEx, this.txtMemoEx, 100, false));
            manager.Add(new LengthCheck(this.lblMemo, this.txtMemo, 100, false));
            manager.Add(new LengthCheck(this.lblPIC, this.txtPIC, 100, false));
            manager.Add(new LengthCheck(this.lblAction, this.txtAction, 100, false));
            manager.Add(new LengthCheck(this.lblMemo, this.txtMemo, 100, false));

            manager.Add(new NumberCheck(this.lblIQCLineNoEdit, this.txtIQCLineNo, true));
            manager.Add(new NumberCheck(this.lblOrderLine, this.txtOrderLine, true));
            //manager.Add(new NumberCheck(this.lblPlanDate, this.datePlanDate, true));
            manager.Add(new NumberCheck(this.lblPlanQty, this.txtPlanQty, true));
            manager.Add(new NumberCheck(this.lblReceiveQty, this.txtReceiveQty,  true));
            if (this.txtSampleQty.Text.Trim().Length!=0)
            {
                manager.Add(new NumberCheck(this.lblSampleQty, this.txtSampleQty,  false));
            }
            if (this.txtNGQty.Text.Trim().Length != 0)
            {
                manager.Add(new NumberCheck(this.lblNGQty, this.txtNGQty, false));
            }
            ////			manager.Add( new LengthCheck(this.lblResourceTypeEdit, this.drpResourceTypeEdit, 40, true) );
            ////			manager.Add( new LengthCheck(this.lblResourceGroupEdit, this.drpResourceGroupEdit, 40, true) );
            ////			manager.Add( new LengthCheck(this.lblStepSequenceCodeEdit, this.drpStepSequenceCodeEdit, 40, true) );
            ////			manager.Add( new LengthCheck(this.lblSegmentCodeEdit, this.txtSegmentCodeEdit, 40, true) );
            //manager.Add(new LengthCheck(this.lblResourceDescriptionEdit, this.txtResourceDescriptionEdit, 100, false));
            //manager.Add(new LengthCheck(this.lblOrgIDEdit, this.DropDownListOrg, 8, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            //if (this.txtReworkRoute.Text.Trim().Length != 0)
            //{
            //    object route = (new BaseModelFacade(this.DataProvider)).GetRoute(this.txtReworkRoute.Text.Trim().ToUpper());
            //    if (route == null)
            //    {
            //        WebInfoPublish.Publish(this, "$Error_RouteNotExist", this.languageComponent1);
            //        return false;
            //    }

            //    object route2OP = (new BaseModelFacade(this.DataProvider)).GetOperationByRouteCode(this.txtReworkRoute.Text.Trim().ToUpper());
            //    if (route2OP == null)
            //    {
            //        WebInfoPublish.Publish(this, "$Error_RouteHasNoOperations", this.languageComponent1);
            //        return false;
            //    }
            //}

            return true;
        }

        #endregion

        protected void drpAttribute_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new SystemParameterListBuilder("ITEMATTRIBUTE", base.DataProvider).Build(this.drpItemAttribute);
            }
        }

        protected void lblStatusSTS_CheckedChanged(object sender, EventArgs e)
        {
            if (lblStatusSTS.Checked)
            {
                lblStatusQualified.Checked = true;
            }
        }

        protected void lblStatusUnqualified_CheckedChanged(object sender, EventArgs e)
        {
            if (lblStatusUnqualified.Checked)
            {
               lblStatusSTS.Checked = false;
            }

        }

        protected void lblStatusWaitCheck_CheckedChanged(object sender, EventArgs e)
        {

            if (lblStatusWaitCheck.Checked)
            {
                lblStatusSTS.Checked = false;
            }
        }



        #region Export
        // 2005-04-06

        //protected override string[] FormatExportRecord(object obj)
        //{
        //    return new string[]{  ((IQCHead)obj).ResourceCode.ToString(),
        //                           ((IQCHead)obj).ResourceDescription.ToString(),
        //                           ((IQCHead)obj).StepSequenceCode.ToString(),
        //                           ((IQCHead)obj).ReworkRouteCode.ToString(),
        //                           ((IQCHead)obj).MaintainUser.ToString(),
        //                           FormatHelper.ToDateString(((IQCHead)obj).MaintainDate)};
        //}

        //protected override string[] GetColumnHeaderText()
        //{
        //    return new string[] {	"ResourceCode",
        //                            "ResourceDescription",
        //                            "ResourceStepSequence",	
        //                            "ReworkRouteCode",
        //                            "MaintainUser",
        //                            "MaintainDate" };
        //}

        #endregion

        //private void BuildOrgList()
        //{
        //    DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListOrg);
        //    builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllOrg);
        //    builder.Build("OrganizationDescription", "OrganizationID");
        //    this.DropDownListOrg.Items.Insert(0, new ListItem("", ""));

        //    this.DropDownListOrg.SelectedIndex = 0;
        //}

        //private object[] GetAllOrg()
        //{
        //    BaseModelFacade facadeBaseModel = new BaseModelFacade(base.DataProvider);
        //    return facadeBaseModel.GetCurrentOrgList();
        //}
    }
}
