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

using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FMOEP 的摘要说明。
    /// </summary>
    public partial class FMOEP : BenQGuru.eMES.Web.Helper.BasePage
    {
        protected System.Web.UI.WebControls.Label lblItemCodeEdit;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components = null;

        private BenQGuru.eMES.MOModel.MOFacade _facade;//= new MOFacade();



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
            this.languageComponent1.LanguagePackageDir = "D:\\eMes\\BenQGuru.eMES.Web";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        #region form events
        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FMOMP.aspx"));
        }

        public string ItemCode
        {
            get
            {
                return (string)this.ViewState["itemcode"];
            }
        }
        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (this.ValidateInput())
            {
                if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
                MO mo = (MO)GetEditObject();


                if (this._facade.IsMOStatusChanged(mo))
                {
                    this._facade.MOStatusChanged(mo);
                }
                else
                {
                    this._facade.UpdateMOInformation(mo, FormatHelper.CleanString(this.drpRouteEdit.SelectedValue));
                }



                Response.Redirect(this.MakeRedirectUrl("FMOMP.aspx"));
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                InitENV();
                SetButtonView(this.GetMOCode());
                this.InitUI();

                if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
                object objRMA = _facade.GetRMARCARDByMoCode(GetMOCode());
                if (objRMA != null)
                {
                    txtRMABillNo.Text = (objRMA as Domain.DataCollect.RMARCARD).RMABILLNO.Trim();
                }

                this.chbBIOSVersion.Attributes.Add("onclick", "SetReadOnly(this)");
                InitMORunningCardRange(this.GetMOCode());
            }
            InitMoDetailInfo();
        }

        private void InitMoDetailInfo() 
        {
            this.lblMORCardRange.Visible = false;
            this.gridWebGrid.Visible = false;
            this.cmdAddLot.Visible = false;
        }

        #endregion

        #region private method
        private string GetMOCode()
        {
            return Request.QueryString["MOCode"];
        }

        private object GetEditObject()
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            if (ValidateInput())
            {

                MO mo = (MO)this._facade.GetMO(GetMOCode());

                mo.Factory = FormatHelper.CleanString(this.txtFactoryEdit.Text, 40);

                //			this.drpMOTypeEdit.SelectedIndex =0;
                //			this.drpMOStatusEdit.SelectedIndex =0;
                //			this.txtCustomerCodeEdit.Text = string.Empty;
                //			this.txtCustomerOrderNOEdit.Text = string.Empty;
                //			this.txtMemo.Text = string.Empty;
                //			this.drpRouteEdit.SelectedIndex =0;
                //			this.chbLimitItemQtyEdit.Checked = false;
                //			this.txtMOQtyEdit.Text = string.Empty;
                //			this.txtInputQtyEdit.Text = string.Empty;
                //			this.txtCompleteQtyEdit.Text = string.Empty;
                //			this.txtUnCompleteQtyEdit.Text = string.Empty;
                //			this.txtPlanStartDateEdit.Text = string.Empty;
                //			this.txtPlanEndDateEdit.Text = string.Empty;
                //			this.txtActualStartDateEdit.Text = string.Empty;
                //			this.txtActualEndDateEdit.Text = string.Empty;
                //			this.txtMOUserEdit.Text = string.Empty;
                //			this.txtMODownloadDateEdit.Text = string.Empty;
                //			this.txtScrapeQtyEdit.Text = string.Empty;

                mo.MOType = FormatHelper.CleanString(this.drpMOTypeEdit.SelectedValue, 40);
                mo.MOPlanStartDate = FormatHelper.TODateInt(this.txtPlanStartDateEdit.Text);
                mo.MOPlanEndDate = FormatHelper.TODateInt(this.txtPlanEndDateEdit.Text);
                mo.CustomerOrderNO = FormatHelper.CleanString(this.txtCustomerOrderNOEdit.Text, 40);
                mo.CustomerCode = FormatHelper.CleanString(this.txtCustomerCodeEdit.Text, 40);
                mo.MODownloadDate = FormatHelper.TODateInt(this.txtMODownloadDateEdit.Text);
                mo.MOUser = FormatHelper.CleanString(this.txtMOUserEdit.Text, 40);
                mo.MOStatus = FormatHelper.CleanString(this.drpMOStatusEdit.SelectedValue, 40);
                mo.MOActualStartDate = (this.txtActualStartDateEdit.Text == "") ? 0 : FormatHelper.TODateInt(this.txtActualStartDateEdit.Text);		//modify by Simone
                mo.MOActualEndDate = (this.txtActualEndDateEdit.Text == "") ? 0 : FormatHelper.TODateInt(this.txtActualEndDateEdit.Text);
                mo.ItemCode = FormatHelper.CleanString(this.txtItemCodeEdit.Text, 40);
                mo.MaintainUser = this.GetUserCode();
                mo.IsControlInput = FormatHelper.BooleanToString(this.chbLimitItemQtyEdit.Checked);
                mo.IDMergeRule = System.Int32.Parse(FormatHelper.CleanString(this.txtDenominatorEdit.Text));
                mo.MOPendingCause = FormatHelper.CleanString(this.txtPendingCause.Text, 50);
                mo.MOPlanQty = System.Decimal.Parse(FormatHelper.CleanString(this.txtMOQtyEdit.Text));
                mo.MOBIOSVersion = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBIOSVersion.Text));
                mo.MOPCBAVersion = FormatHelper.CleanString(this.txtPCBAVersion.Text);
                //Laws Lu,2006/07/05 support RMA
                mo.RMABillCode = FormatHelper.CleanString(this.txtRMABillNo.Text);
                mo.IsCompareSoft = (true == this.chbBIOSVersion.Checked) ? 1 : 0;
                mo.BOMVersion = this.txtMOBomEdit.Text.Trim();
                mo.MOMemo = FormatHelper.CleanString(this.txtMemo.Text, 100);
                mo.MORemark = FormatHelper.CleanString(this.txtMORemarkEdit.Text, 500);
                if (mo.MORemark == "")
                {
                    mo.MORemark = " ";
                }
                return mo;
            }
            else
            {
                return null;
            }
        }


        private void SetButtonView(string moCode)
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            object mo = this._facade.GetMO(moCode);
            if (mo != null)
            {
                if ((((MO)mo).MOStatus == MOManufactureStatus.MOSTATUS_INITIAL) || (((MO)mo).MOStatus == MOManufactureStatus.MOSTATUS_PENDING))
                {
                    this.cmdBOM.Disabled = false;
                    this.cmdSave.Disabled = false;
                }
                else
                {
                    this.cmdBOM.Disabled = true;
                    this.cmdSave.Disabled = true;
                }


            }
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblProduceRoute, drpRouteEdit, 40, true));
            manager.Add(new NumberCheck(lblMergeRule, txtDenominatorEdit, 1, int.MaxValue, true));
            manager.Add(new LengthCheck(lblPendingCause, txtPendingCause, 50, false));
            manager.Add(new DecimalCheck(lblMOQtyEdit, txtMOQtyEdit, 1, Int32.MaxValue, true));
            manager.Add(new LengthCheck(lblMOBomEdit, txtMOBomEdit, 40, true));
            manager.Add(new LengthCheck(lblMOMemoGroup, txtMemo, 100, false));

            manager.Add(new LengthCheck(lblCustomerCodeEdit, txtCustomerCodeEdit, 40, false));
            manager.Add(new LengthCheck(lblCustomerOrderNOEdit, txtCustomerOrderNOEdit, 40, false));
            manager.Add(new DateCheck(lblPlanStartDateEdit, txtPlanStartDateEdit.Text, false));
            manager.Add(new DateCheck(lblPlanEndDateEdit, txtPlanEndDateEdit.Text, false));
            manager.Add(new DateRangeCheck(lblPlanEndDateEdit, txtPlanStartDateEdit.Text, txtPlanEndDateEdit.Text, false));

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            if (!reg.IsMatch(txtMOBomEdit.Text))
            {
                WebInfoPublish.Publish(this, "$Error_MOBOM_Mustbe_NumAndLetter", this.languageComponent1);
                return false;
            }
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            if (this.chbBIOSVersion.Checked)
            {
                if (this.txtBIOSVersion.Text.Trim() == string.Empty)
                {
                    WebInfoPublish.Publish(this, "比对软件版本已钩选,请输入软件版本信息", languageComponent1);
                    return false;
                }
            }
            return true;
        }


        private void SetEditObject(object obj)
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            if (obj == null)
            {
                this.txtFactoryEdit.Text = string.Empty;
                this.txtMOEdit.Text = string.Empty;
                this.txtItemCodeEdit.Text = string.Empty;
                this.drpMOTypeEdit.SelectedIndex = 0;
                this.drpMOStatusEdit.SelectedIndex = 0;
                this.txtCustomerCodeEdit.Text = string.Empty;
                this.txtCustomerOrderNOEdit.Text = string.Empty;
                this.txtMemo.Text = string.Empty;
                this.drpRouteEdit.SelectedIndex = 0;
                this.chbLimitItemQtyEdit.Checked = false;
                this.txtMOQtyEdit.Text = string.Empty;
                this.txtInputQtyEdit.Text = string.Empty;
                this.txtCompleteQtyEdit.Text = string.Empty;
                this.txtUnCompleteQtyEdit.Text = string.Empty;
                this.txtPlanStartDateEdit.Text = string.Empty;
                this.txtPlanEndDateEdit.Text = string.Empty;
                this.txtActualStartDateEdit.Text = string.Empty;
                this.txtActualEndDateEdit.Text = string.Empty;
                this.txtMOUserEdit.Text = string.Empty;
                this.txtMODownloadDateEdit.Text = string.Empty;
                this.txtScrapeQtyEdit.Text = string.Empty;
                this.txtDenominatorEdit.Text = string.Empty;
                this.lblCompareBOMInformation.Text = string.Empty;

                this.txtMaintainUser.Text = string.Empty;
                this.txtMaintainDate.Text = string.Empty;
                this.txtPendingCause.Text = string.Empty;

                this.txtRMABillNo.Text = string.Empty;
                this.txtMOBomEdit.Text = string.Empty;
                this.txtMORemarkEdit.Text = string.Empty;

                return;
            }

            this.txtFactoryEdit.Text = ((MO)obj).Factory.ToString();
            this.txtMOEdit.Text = ((MO)obj).MOCode.ToString();
            this.txtItemCodeEdit.Text = ((MO)obj).ItemCode.ToString();
            try
            {
                this.drpMOTypeEdit.SelectedValue = ((MO)obj).MOType.ToString();
            }
            catch
            {
                this.drpMOTypeEdit.SelectedIndex = 0;
            }
            try
            {
                this.drpMOStatusEdit.SelectedValue = ((MO)obj).MOStatus.ToString();
            }
            catch
            {
                this.drpMOStatusEdit.SelectedIndex = 0;
            }
            this.txtCustomerCodeEdit.Text = ((MO)obj).CustomerCode.ToString();
            this.txtCustomerOrderNOEdit.Text = ((MO)obj).CustomerOrderNO.ToString();
            this.txtMemo.Text = ((MO)obj).MOMemo.ToString();
            this.chbLimitItemQtyEdit.Checked = FormatHelper.StringToBoolean(((MO)obj).IsControlInput);
            this.txtMOQtyEdit.Text = ((MO)obj).MOPlanQty.ToString();
            this.txtScrapeQtyEdit.Text = ((MO)obj).MOScrapQty.ToString();
            this.txtInputQtyEdit.Text = ((MO)obj).MOInputQty.ToString();
            this.txtCompleteQtyEdit.Text = ((MO)obj).MOActualQty.ToString();
            Decimal unCompleteQty = ((MO)obj).MOInputQty - ((MO)obj).MOActualQty - ((MO)obj).MOScrapQty - ((MO)obj).MOOffQty;
            this.txtUnCompleteQtyEdit.Text = unCompleteQty.ToString();
            this.txtOffMOQtyEdit.Text = ((MO)obj).MOOffQty.ToString();
            this.txtPlanStartDateEdit.Text = FormatHelper.ToDateString(((MO)obj).MOPlanStartDate);
            this.txtPlanEndDateEdit.Text = FormatHelper.ToDateString(((MO)obj).MOPlanEndDate);
            //			this.txtActualStartDateEdit.Text = FormatHelper.ToDateString( ((MO)obj).MOActualStartDate );
            //			this.txtActualEndDateEdit.Text = FormatHelper.ToDateString( ((MO)obj).MOActualEndDate );
            this.txtActualStartDateEdit.Text = (((MO)obj).MOActualStartDate > 20000101 && ((MO)obj).MOActualStartDate < 21001231) ? FormatHelper.ToDateString(((MO)obj).MOActualStartDate) : "";		//modify by Simone
            this.txtActualEndDateEdit.Text = (((MO)obj).MOActualEndDate > 20000101 && ((MO)obj).MOActualEndDate < 21001231) ? FormatHelper.ToDateString(((MO)obj).MOActualEndDate) : "";
            this.txtMOUserEdit.Text = ((MO)obj).MOUser.ToString();
            this.txtMODownloadDateEdit.Text = FormatHelper.ToDateString(((MO)obj).MODownloadDate);
            this.txtDenominatorEdit.Text = ((MO)obj).IDMergeRule.ToString();

            this.txtMaintainUser.Text = ((MO)obj).MaintainUser;
            this.txtMaintainDate.Text = FormatHelper.ToDateString(((MO)obj).MaintainDate);
            this.txtPendingCause.Text = ((MO)obj).MOPendingCause;
            this.txtBIOSVersion.Text = ((MO)obj).MOBIOSVersion.ToString();
            this.txtPCBAVersion.Text = ((MO)obj).MOPCBAVersion.ToString();
            this.txtRMABillNo.Text = ((MO)obj).RMABillCode.ToString();
            this.txtMOBomEdit.Text = ((MO)obj).BOMVersion.ToString();
            this.txtMORemarkEdit.Text = ((MO)obj).MORemark.ToString();

            if (((MO)obj).IsCompareSoft == 1)
            {
                this.chbBIOSVersion.Checked = true;
            }

            //bom compare information 
            if (((MO)obj).IsBOMPass == IsPass.ISPASS_PASS.ToString())
            {
                this.lblCompareBOMInformation.Text = this.languageComponent1.GetString("$MSG_BOMComparePass");
            }
            else
            {
                if (((MO)obj).IsBOMPass == IsPass.ISPASS_NOPASS.ToString())
                {
                    this.lblCompareBOMInformation.Text = this.languageComponent1.GetString("$MSG_BOMCompareNOPass");
                }
                else
                {
                    this.lblCompareBOMInformation.Text = string.Empty;
                }
            }

            //moroute
            MO2Route currentMO2Route = (MO2Route)this._facade.GetMONormalRouteByMOCode(((MO)obj).MOCode);
            if (currentMO2Route != null)
            {
                try
                {
                    this.drpRouteEdit.SelectedValue = FormatHelper.CleanString(currentMO2Route.RouteCode);
                }
                catch
                {
                    this.drpRouteEdit.SelectedIndex = 0;
                }
            }
            else
            {
                //2006/09/27 Laws Lu,修改	支持默认途程
                object objDRoute = this._facade.GetDefaultItem2Route(((MO)obj).ItemCode);
                if (objDRoute != null)
                {
                    try
                    {
                        this.drpRouteEdit.SelectedValue = FormatHelper.CleanString(((DefaultItem2Route)objDRoute).RouteCode);
                    }
                    catch
                    {
                        this.drpRouteEdit.SelectedIndex = 0;
                    }
                }
                else
                {
                    this.drpRouteEdit.SelectedIndex = 0;
                }
            }

        }

        #region Init DropDownLists



        private string[] GetMoStatuses()
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            return this._facade.GetMOStatuses();
        }

        #endregion


        private void SetInputEnable(bool enable, string status)
        {
            if (!enable)
            {
                this.txtFactoryEdit.Enabled = false;
                this.txtMOEdit.Enabled = false;
                this.txtItemCodeEdit.Enabled = false;
                this.drpMOTypeEdit.Enabled = false;
                this.drpMOStatusEdit.Enabled = false;
                this.txtCustomerCodeEdit.Enabled = false;
                this.txtCustomerOrderNOEdit.Enabled = false;
                this.txtMemo.Enabled = false;
                this.drpRouteEdit.Enabled = false;
                this.chbLimitItemQtyEdit.Enabled = false;
                this.txtMOQtyEdit.Enabled = false;
                this.txtInputQtyEdit.Enabled = false;
                this.txtCompleteQtyEdit.Enabled = false;
                this.txtUnCompleteQtyEdit.Enabled = false;
                this.txtPlanStartDateEdit.Enable = "false";
                this.txtPlanEndDateEdit.Enable = "false";
                this.txtPlanStartDateEdit.DateTextIsReadOnly = true;
                this.txtPlanEndDateEdit.DateTextIsReadOnly = true;
                this.txtActualStartDateEdit.Enabled = false;
                this.txtActualEndDateEdit.Enabled = false;
                this.txtMOUserEdit.Enabled = false;
                this.txtMODownloadDateEdit.Enabled = false;
                this.txtScrapeQtyEdit.Enabled = false;
                this.txtDenominatorEdit.Enabled = false;
                this.txtMaintainUser.Enabled = false;
                this.txtMaintainDate.Enabled = false;
                this.txtPendingCause.Enabled = false;
                this.chbBIOSVersion.Enabled = false;
                this.txtBIOSVersion.Enabled = false;
                this.txtRMABillNo.Enabled = false;
                this.txtMOBomEdit.Enabled = false;
                this.txtMORemarkEdit.Enabled = false;
            }
            /* added by jessie lee, 2005/12/8
             * CS187:工单在初始化和暂停状态，允许在工单详细信息中修改计划数量，保存后进行更新工单的计划数量。 */
            if (string.Compare(status, MOManufactureStatus.MOSTATUS_PENDING, true) == 0
                || string.Compare(status, MOManufactureStatus.MOSTATUS_INITIAL, true) == 0)
            {
                this.drpRouteEdit.Enabled = true;
                this.txtMOQtyEdit.Enabled = true;
                this.txtMOBomEdit.Enabled = true;
                this.txtMOQtyEdit.ReadOnly = false;
                this.txtCustomerCodeEdit.Enabled = true;
                this.txtCustomerOrderNOEdit.Enabled = true;
                this.txtPlanStartDateEdit.Enable = "true";
                this.txtPlanEndDateEdit.Enable = "true";
                this.txtPlanStartDateEdit.DateTextIsReadOnly = false;
                this.txtPlanEndDateEdit.DateTextIsReadOnly = false;
            }

            if (string.Compare(status, MOManufactureStatus.MOSTATUS_PENDING, true) == 0)
            {
                this.txtPendingCause.Enabled = true;
            }
            else
            {
                this.txtPendingCause.Enabled = false;
            }

            if (string.Compare(status, MOManufactureStatus.MOSTATUS_INITIAL, true) == 0)
            {
                this.txtMemo.Enabled = true;
                this.txtMemo.CssClass = "textbox";
            }
            else
            {
                this.txtMemo.Enabled = false;
                this.txtMemo.CssClass = "require";
            }

        }

        private void SetPeddingInputEnable()
        {
            this.drpRouteEdit.Enabled = false;
        }

        private void InitENV()
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            string action = Request.QueryString["ACT"].Trim();

            MO mo = (MO)(this._facade.GetMO(this.GetMOCode()));
            this.ViewState["itemcode"] = mo.ItemCode;

            #region reset status list

            this.drpMOStatusEdit.Items.Clear();
            switch (mo.MOStatus)
            {
                case MOManufactureStatus.MOSTATUS_INITIAL:
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_INITIAL), MOManufactureStatus.MOSTATUS_INITIAL));
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_RELEASE), MOManufactureStatus.MOSTATUS_RELEASE));
                    SetInputEnable(true, MOManufactureStatus.MOSTATUS_INITIAL);
                    break;

                case MOManufactureStatus.MOSTATUS_RELEASE:
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_INITIAL), MOManufactureStatus.MOSTATUS_INITIAL));
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_RELEASE), MOManufactureStatus.MOSTATUS_RELEASE));
                    SetInputEnable(false, MOManufactureStatus.MOSTATUS_RELEASE);
                    break;

                case MOManufactureStatus.MOSTATUS_OPEN:
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_OPEN), MOManufactureStatus.MOSTATUS_OPEN));
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_CLOSE), MOManufactureStatus.MOSTATUS_CLOSE));
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_PENDING), MOManufactureStatus.MOSTATUS_PENDING));
                    SetInputEnable(false, MOManufactureStatus.MOSTATUS_OPEN);
                    break;


                case MOManufactureStatus.MOSTATUS_CLOSE:
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_CLOSE), MOManufactureStatus.MOSTATUS_CLOSE));
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_PENDING), MOManufactureStatus.MOSTATUS_PENDING));
                    SetInputEnable(false, MOManufactureStatus.MOSTATUS_CLOSE);
                    break;

                case MOManufactureStatus.MOSTATUS_PENDING:
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_OPEN), MOManufactureStatus.MOSTATUS_OPEN));
                    this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(MOManufactureStatus.MOSTATUS_PENDING), MOManufactureStatus.MOSTATUS_PENDING));
                    SetInputEnable(true, MOManufactureStatus.MOSTATUS_PENDING);
                    //SetPeddingInputEnable();
                    break;
            }


            this.drpMOTypeEdit.Items.Clear();
            SystemParameterListBuilder _builder = new SystemParameterListBuilder("MOTYPE", base.DataProvider);
            _builder.Build(this.drpMOTypeEdit);
            _builder.AddAllItem(languageComponent1);

            this.drpMOStatusEdit.Items.Clear();
            this.drpMOStatusEdit.Items.Add(string.Empty);
            string[] moStatuses = GetMoStatuses();
            foreach (string item in moStatuses)
            {
                this.drpMOStatusEdit.Items.Add(new ListItem(this.languageComponent1.GetString(item.Trim()), item));
            }

            this.drpRouteEdit.Items.Clear();
            this.drpRouteEdit.Items.Add(new ListItem("", ""));
            object[] objs = this._facade.QueryNormalRouteByMOEnabled(this.GetMOCode(), string.Empty);
            if (objs != null)
            {
                foreach (Route route in objs)
                {
                    this.drpRouteEdit.Items.Add(route.RouteCode);
                }
            }

            #endregion

            this.SetEditObject(mo);

        }
        #endregion

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            if (_facade == null) { _facade = new MOFacade(base.DataProvider); }
            if (this.ValidateRouteInput())
            {
                MO mo = (MO)this._facade.GetMO(GetMOCode());
                //				Response.Redirect(this.MakeRedirectUrl("FMOBOMMP.aspx",new string[] {"mocode","itemcode","routecode"},new string[] {mo.MOCode,mo.ItemCode,FormatHelper.CleanString(this.drpRouteEdit.SelectedValue)} ));
                //Test
                Response.Redirect(this.MakeRedirectUrl("FMOBOMCompare.aspx", new string[] { "mocode", "itemcode", "routecode" }, new string[] { mo.MOCode, mo.ItemCode, FormatHelper.CleanString(this.drpRouteEdit.SelectedValue) }));
            }
        }

        private bool ValidateRouteInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblProduceRoute, drpRouteEdit, 40, true));
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        private void InitMORunningCardRange(string moCode)
        {
            this.gridWebGrid.Columns.Clear();
            this.gridWebGrid.Columns.Add("MORunningCardType", "类型");
            this.gridWebGrid.Columns.Add("MORunningCardStart", "起始序列号");
            this.gridWebGrid.Columns.Add("MORunningCardEnd", "结束序列号");

            gridWebGrid.Rows.Clear();
            MORunningCardFacade rcardFacade = new MORunningCardFacade(this.DataProvider);
            object[] objRange = rcardFacade.QueryMORunningCardRange(moCode, string.Empty, 1, int.MaxValue);
            if (objRange != null)
            {
                for (int i = 0; i < objRange.Length; i++)
                {
                    MORunningCardRange rcardRange = (MORunningCardRange)objRange[i];
                    this.gridWebGrid.Rows.Add(
                        new UltraGridRow(
                            new object[]{
											this.languageComponent1.GetString(rcardRange.RunningCardType),
											rcardRange.MORunningCardStart,
											rcardRange.MORunningCardEnd
										}
                            )
                        );
                }
            }
        }

        protected void cmdAddLot_ServerClick(object sender, EventArgs e)
        {
            string strBackUrl = this.Request.Url.PathAndQuery;
            string strUrl = "FMORCardRangeMP.aspx?mocode=" + this.GetMOCode() + "&backurl=" + Server.UrlEncode(strBackUrl);
            Response.Redirect(strUrl);
        }
    }
}
