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
    public partial class IQCHeadMP : BaseMPage
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
            this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
        }
        #endregion

        #region 数据初始化

        //protected void drpStepSequenceCodeEdit_Load(object sender, System.EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        DropDownListBuilder builder = new DropDownListBuilder(this.drpStepSequenceCodeEdit);
        //        if (_facade == null)
        //        {
        //            _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
        //        }
        //        builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._facade.GetAllStepSequence);

        //        builder.Build("StepSequenceCode", "StepSequenceCode");

        //        //添加一个默认的空选项,新增时为非必选 modify by Simone
        //        ListItem sf0 = new ListItem("", "");
        //        this.drpStepSequenceCodeEdit.Items.Add(sf0);
        //        this.drpStepSequenceCodeEdit.Items.FindByValue("").Selected = true;
        //        this.drpStepSequenceCodeEdit_SelectedIndexChanged(this, null);

        //        this.BuildOrgList();
        //    }
        //}

        //protected void drpStepSequenceCodeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    if (_facade == null)
        //    {
        //        _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
        //    }
        //    object obj = this._facade.GetStepSequence(this.drpStepSequenceCodeEdit.SelectedValue);

        //    if (obj == null)
        //    {
        //        this.txtSegmentCodeEdit.Text = "";

        //        return;
        //    }

        //    this.txtSegmentCodeEdit.Text = ((StepSequence)obj).SegmentCode;
        //    if (obj == null)
        //    {
        //        this.txtShiftEdit.Text = "";
        //        return;
        //    }
        //    else
        //    {
        //        this.txtShiftEdit.Text = ((StepSequence)obj).ShiftTypeCode;
        //    }
        //    /*object _Segment = this._facade.GetSegment(this.txtSegmentCodeEdit.Text.Trim());
        //    if(_Segment == null)
        //    {
        //        this.txtShiftEdit.Text = "";
        //        return;
        //    }
        //    this.txtShiftEdit.Text = ((Segment)_Segment).ShiftTypeCode;
        //     * */
        //}

        //protected void drpResourceTypeEdit_Load(object sender, System.EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        IInternalSystemVariable variable = InternalSystemVariable.Lookup(ResourceType.ResourceTypeName);
        //        if (variable != null)
        //        {
        //            foreach (string code in variable.Items)
        //            {
        //                string msg = this.languageComponent1.GetString(code);
        //                if (msg == "" || msg == null)
        //                {
        //                    msg = code;
        //                }
        //                this.drpResourceTypeEdit.Items.Add(new ListItem(msg, code));
        //            }
        //        }
        //        //				else
        //        //				{
        //        //					WebInfoPublish.Publish(this,"$Error_No_ResourceType",this.languageComponent1);
        //        //
        //        //					return;
        //        //				}
        //    }
        //}

        //protected void drpResourceGroupEdit_Load(object sender, System.EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        new SystemParameterListBuilder("ResourceGroup", base.DataProvider).Build(this.drpResourceGroupEdit);
        //    }
        //}
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {

                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.lblStatusWaitCheckQuery.Checked = true;
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
            this.gridHelper.AddColumn("IQCNo", "送检单", null);
            this.gridHelper.AddLinkColumn("Detail", "详细", null);
            this.gridHelper.AddColumn("VendorCode", "供应商代码", null);
            this.gridHelper.AddColumn("VendorName", "供应商名称", null);
            this.gridHelper.AddColumn("ROHS", "ROHS", null);
            this.gridHelper.AddColumn("InvUser", "保管员", null);

            this.gridHelper.AddColumn("Applicant", "送检员", null);
            this.gridHelper.AddColumn("Inspector", "检验员", null);
            this.gridHelper.AddColumn("AppDate", "送检日期", null);
            this.gridHelper.AddColumn("AppTime", "送检时间", null);
            this.gridHelper.AddColumn("LotNo", "检验批号", null);
            this.gridHelper.AddColumn("InspectDate", "检验日期", null);
            this.gridHelper.AddColumn("ProductDate", "生产日期", null);
            this.gridHelper.AddColumn("Standard", "检验依据", null);
            this.gridHelper.AddColumn("Method", "检验形式", null);
            this.gridHelper.AddColumn("Result", "检验结果", null);
            this.gridHelper.AddColumn("ReceiveDate", "签收日期", null);
            this.gridHelper.AddColumn("PIC", "负责人", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddColumn("STNo", "ASN单", null);
            this.gridWebGrid.Columns.FromKey("STNo").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("ResourceGroup").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("ShiftTypeCode").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("SegmentCode").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
								((IQCHead)obj).IQCNo.ToString(),
                    "",
			                    			((IQCHead)obj).ROHS.ToString(),
                    			((IQCHead)obj).Applicant.ToString(),
                    			((IQCHead)obj).Inspector.ToString(),
                    			FormatHelper.ToDateString(((IQCHead)obj).AppDate),
                    			FormatHelper.ToTimeString(((IQCHead)obj).AppTime),                    			
                    			((IQCHead)obj).LotNo.ToString(),
                    			FormatHelper.ToDateString(((IQCHead)obj).InspectDate),
                    			FormatHelper.ToDateString(((IQCHead)obj).ProduceDate),
                    			((IQCHead)obj).Standard.ToString(),
                    			((IQCHead)obj).Method.ToString(),
                    			((IQCHead)obj).Result .ToString(),
                    			FormatHelper.ToDateString(((IQCHead)obj).ReceiveDate),			
                    			((IQCHead)obj).PIC.ToString(),
                                ((IQCHead)obj).Status .ToString(),
                                ((IQCHead)obj).STNo.ToString(),
								""});
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryIQCHead(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNoQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNNoQuery.Text)),
            this.lblStatusWaitCheckQuery.Checked,
            this.lblStatusClosedQuery.Checked,
            this.lblROHSQuery.Checked,
            FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)),
            FormatHelper.TODateInt(FormatHelper.CleanString(this.dateAppDateFromQuery.Text)).ToString(),
            FormatHelper.TODateInt(FormatHelper.CleanString(this.dateAppDateToQuery.Text)).ToString(),


                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryIQCHeadCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNoQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNNoQuery.Text)),
            this.lblStatusWaitCheckQuery.Checked,
            this.lblStatusWaitCheckQuery.Checked,
            this.lblROHSQuery.Checked,
            FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)),
            FormatHelper.TODateInt(FormatHelper.CleanString(this.dateAppDateFromQuery.Text)).ToString(),
            FormatHelper.TODateInt(FormatHelper.CleanString(this.dateAppDateToQuery.Text)).ToString());
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }

            if (_facade.IsIQCHeadExist(((IQCHead)domainObject).IQCNo))
            {
                WebInfoPublish.Publish(this, "$IQCExist", this.languageComponent1);
                return;
            }
            if (_facade.IsASNExist(((IQCHead)domainObject).STNo))
            {
                WebInfoPublish.Publish(this, "$ASNExist", this.languageComponent1);
                return;
            }
            this._facade.AddIQCHead((IQCHead)domainObject, this.GetUserCode());
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.DeleteIQCHead((IQCHead[])domainObjects.ToArray(typeof(IQCHead)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            if (((IQCHead)domainObject).Status.ToUpper().Equals("CLOSE"))
            {
                WebInfoPublish.Publish(this, "$CanNotModifyForClosed", this.languageComponent1);
                return;
            }
            this._facade.UpdateIQCHead((IQCHead)domainObject);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtIQCNoEdit.ReadOnly = false;
                this.txtASNNoEdit.ReadOnly = false;
                this.txtVendorCodeEdit.ReadOnly = false;
                this.txtVendorNameEdit.ReadOnly = false;
                this.txtInvUserEdit.ReadOnly = false;

            }

            if (pageAction == PageActionType.Update)
            {

                this.txtIQCNoEdit.ReadOnly = true;
                this.txtASNNoEdit.ReadOnly = true;
                this.txtVendorCodeEdit.ReadOnly = true;
                this.txtVendorNameEdit.ReadOnly = true;
                this.txtInvUserEdit.ReadOnly = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.ButtonClose.Enabled = true;
            }
            else
            {
                this.ButtonClose.Enabled = false;
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
            IQCHead iqcHead = this._facade.CreateNewIQCHead();
            iqcHead.IQCNo = FormatHelper.CleanString(this.txtIQCNoEdit.Text, 50);
            iqcHead.STNo = FormatHelper.CleanString(this.txtASNNoEdit.Text, 40);
            iqcHead.ROHS = this.lblROHSEdit.Checked.ToString().ToUpper();
            iqcHead.InventoryUser = FormatHelper.CleanString(this.txtInvUserEdit.Text, 100);
            iqcHead.Applicant = FormatHelper.CleanString(this.txtApplicantEdit.Text, 100);
            iqcHead.Inspector = FormatHelper.CleanString(this.txtInspectorEdit.Text, 100);
            iqcHead.LotNo = FormatHelper.CleanString(this.txtLotNoEdit.Text, 100);
            iqcHead.InspectDate = FormatHelper.TODateInt(this.dateInspectDateEdit.Text);
            iqcHead.ProduceDate = FormatHelper.TODateInt(this.dateProductDateEdit.Text);
            iqcHead.Standard = FormatHelper.CleanString(this.txtStandardEdit.Text, 100);
            iqcHead.Method = FormatHelper.CleanString(this.txtMethodEdit.Text, 100);
            iqcHead.Result = FormatHelper.CleanString(this.txtResultEdit.Text, 100);
            iqcHead.ReceiveDate = FormatHelper.TODateInt(this.dateReceiveDateEdit.Text);
            iqcHead.PIC = FormatHelper.CleanString(this.txtPICEdit.Text, 100);
            iqcHead.Status = this.txtStatus.Text;
            return iqcHead;
        }


        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            object obj = _facade.GetIQCHead(row.Cells[1].Text.ToString());

            if (obj != null)
            {
                return (IQCHead)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtIQCNoEdit.Text = "";
                this.txtASNNoEdit.Text = "";
                this.lblROHSEdit.Checked = false;
                this.txtVendorCodeEdit.Text = "";
                this.txtVendorNameEdit.Text = "";
                this.txtInvUserEdit.Text = "";
                this.txtApplicantEdit.Text = "";
                this.txtInspectorEdit.Text = "";
                this.txtLotNoEdit.Text = "";

                this.dateInspectDateEdit.Text = "";

                this.dateProductDateEdit.Text = "";

                this.txtStandardEdit.Text = "";
                this.txtMethodEdit.Text = "";
                this.txtResultEdit.Text = "";

                this.dateReceiveDateEdit.Text = "";
                this.txtPICEdit.Text = "";
                this.txtStatus.Text = "";
                return;
            }

            this.txtIQCNoEdit.Text = ((IQCHead)obj).IQCNo.ToString();
            this.txtASNNoEdit.Text = ((IQCHead)obj).STNo.ToString();
            this.lblROHSEdit.Checked = bool.Parse(((IQCHead)obj).ROHS.ToString());
            this.txtInvUserEdit.Text = ((IQCHead)obj).InventoryUser.ToString();
            this.txtApplicantEdit.Text = ((IQCHead)obj).Applicant.ToString();
            this.txtInspectorEdit.Text = ((IQCHead)obj).Inspector.ToString();
            this.txtLotNoEdit.Text = ((IQCHead)obj).LotNo.ToString();
            if (((IQCHead)obj).InspectDate.ToString().Trim().Length == 8)
            {
                this.dateInspectDateEdit.Text = ((IQCHead)obj).InspectDate.ToString().Insert(6, "/").Insert(4, "/");
            }
            if (((IQCHead)obj).ProduceDate.ToString().Trim().Length == 8)
            {
                this.dateProductDateEdit.Text = ((IQCHead)obj).ProduceDate.ToString().Insert(6, "/").Insert(4, "/");
            }
            this.txtStandardEdit.Text = ((IQCHead)obj).Standard.ToString();
            this.txtMethodEdit.Text = ((IQCHead)obj).Method.ToString();
            this.txtResultEdit.Text = ((IQCHead)obj).Result.ToString();
            if (((IQCHead)obj).ReceiveDate.ToString().Trim().Length == 8)
            {
                this.dateReceiveDateEdit.Text = ((IQCHead)obj).ReceiveDate.ToString().Insert(6, "/").Insert(4, "/");
            }
            this.txtPICEdit.Text = ((IQCHead)obj).PIC.ToString();
            this.txtStatus.Text=((IQCHead)obj).Status.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();



            manager.Add(new LengthCheck(this.lblIQCNoEdit, this.txtIQCNoEdit, 50, true));
            manager.Add(new LengthCheck(this.lblASNNoEdit, this.txtASNNoEdit, 40, true));
            manager.Add(new LengthCheck(this.lblVendorCodeEdit, this.txtVendorCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblVendorNameEdit, this.txtVendorNameEdit, 100, true));
            manager.Add(new LengthCheck(this.lblInvUserEdit, this.txtInvUserEdit, 100, true));
            manager.Add(new LengthCheck(this.lblApplicantEdit, this.txtApplicantEdit, 100, false));
            manager.Add(new LengthCheck(this.lblInspectorEdit, this.txtInspectorEdit, 100, false));
            manager.Add(new LengthCheck(this.lblLotNoEdit, this.txtLotNoEdit, 100, false));
            manager.Add(new LengthCheck(this.lblStandardEdit, this.txtStandardEdit, 100, false));
            manager.Add(new LengthCheck(this.lblMethodEdit, this.txtMethodEdit, 100, false));
            manager.Add(new LengthCheck(this.lblPICEdit, this.txtPICEdit, 100, false));

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

            return true;
        }

        #endregion

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            if (_facade == null)
            {
                _facade = new InterfaceModelFacadeFactory(base.DataProvider).Create();
            }
            if (!((IQCHead)_facade.GetIQCHead(((IQCHead)this.GetEditObject()).IQCNo)).Status.ToUpper().Equals("WAITCHECK"))
            {
                WebInfoPublish.Publish(this, "$OnlyCloseWaitCheck", this.languageComponent1);
                return;
            }
            
            if (_facade.IsIQCAvaliableToClose(((IQCHead)this.GetEditObject()).IQCNo))
            {
                _facade.CloseIQC(((IQCHead)this.GetEditObject()).IQCNo);
                this.SetEditObject(null);
            }
            else
            {
                WebInfoPublish.Publish(this, "$OnlyCloseFinishedCheck", this.languageComponent1);
            }

        }

        protected void gridWebGrid_Click(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
        {

        }

        private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
            if (e.Cell.Column.Index == 2)
            {
                string url = "IQCDetailMP.aspx?IQCNo=" + e.Cell.Row.Cells.FromKey("IQCNo").Text.ToString() + "&STNo=" + e.Cell.Row.Cells.FromKey("STNo").Text.ToString() + "&Status=" + e.Cell.Row.Cells.FromKey("Status").Text.ToString();
                Response.Redirect(url, true);
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
