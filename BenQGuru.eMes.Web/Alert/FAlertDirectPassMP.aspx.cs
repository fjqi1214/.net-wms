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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.AlertModel;

namespace BenQGuru.eMES.Web.Alert
{
    public partial class FAlertDirectPassMP : BasePage
    {
        private System.ComponentModel.IContainer components;

        private LanguageComponent _LanguageComponent1;

        private AlertFacade _AlertFacade;

        #region Form Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this._LanguageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this._LanguageComponent1.Language = "CHS";
            this._LanguageComponent1.LanguagePackageDir = "";
            this._LanguageComponent1.RuntimePage = null;
            this._LanguageComponent1.RuntimeUserControl = null;
            this._LanguageComponent1.UserControlName = "";

            this._AlertFacade = new AlertFacade(this.DataProvider);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPageLanguage(this._LanguageComponent1, false);

                InitUI();
                InitItemType();
                InitTimeDimension();
                InitNoticeInfo();
            }
        }

        protected void cmdMailSetup_ServerClick(object sender, System.EventArgs e)
        {
            string url = "./FAlertMailSettingMP.aspx?ITEMSEQUENCE=";
            url += Request["ITEMSEQUENCE"];
            url += "&FROMPAGE=AlertDirectPass";

            this.Response.Redirect(this.MakeRedirectUrl(url));

        }
        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {

            if (!SaveCheck())
            {
                return;
            }

            AlertDirectPass alertDirectPass = new AlertDirectPass();

            alertDirectPass.ItemSequence = this.txtAlertItemSequence.Text;
            alertDirectPass.ItemType = this.ddlItemType.SelectedValue;
            alertDirectPass.BaseOutput = int.Parse(this.txtBaseOutPut.Text);
            alertDirectPass.TimeDimension = this.rblTimeDimensionGroup.SelectedValue;

            //产生预警通告
            if (this.chbGenerateNotice.Checked)
            {
                alertDirectPass.GenerateNotice = "Y";
            }
            else
            {
                alertDirectPass.GenerateNotice = "N";
            }

            //发送邮件
            if (this.chbSendMail.Checked)
            {
                int count = this._AlertFacade.QueryAlertMailSettingsCount(alertDirectPass.ItemSequence);
                if (count > 0)
                {
                    alertDirectPass.SendMail = "Y";
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Error_EmailSetup_Need", this._LanguageComponent1);
                    return;
                }
            }
            else
            {
                alertDirectPass.SendMail = "N";
            }

            //停线控制
            if (this.chbLinePause.Checked)
            {
                alertDirectPass.LinePause = "Y";
            }
            else
            {
                alertDirectPass.LinePause = "N";
            }

            decimal alertValue = Math.Round(Decimal.Parse(this.txtAlertStandard.Text), 2);
            if (alertValue >= 1)
            {
                alertValue = 0.99m;
            }
            else if (alertValue <= 0)
            {
                alertValue = 0.01m;
            }

            alertDirectPass.AlertValue = alertValue;
            alertDirectPass.MaintainDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
            alertDirectPass.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            alertDirectPass.MaintainUser = this.GetUserCode();

            this._AlertFacade.SaveAlertDirectPass(alertDirectPass);
            WebInfoPublish.Publish(this, "$CS_Save_Success", this._LanguageComponent1);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FAlertItemMP.aspx"));
        }

        protected void chbGenerateNotice_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chbGenerateNotice.Checked)
            {
                this.chbLinePause.Enabled = true;
                this.chbLinePause.Checked = false;
            }
            else
            {
                this.chbLinePause.Enabled = false;
                this.chbLinePause.Checked = false;
            }

        }

        private bool SaveCheck()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new NumberCheck(this.lblBaseOutPut, this.txtBaseOutPut, true));
            manager.Add(new DecimalCheck(this.lblAlertStandard, this.txtAlertStandard, 0.0001m, 0.9999m, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this._LanguageComponent1);
                return false;
            }

            if (this.txtAlertStandard.Text.Trim().Length > 6)
            {
                WebInfoPublish.Publish(this, "$AlertValueLength", this._LanguageComponent1);
                this.txtAlertStandard.Focus();
                return false;
            }
            return true;
        }

        #endregion



        #region Init Functions
        private void InitNoticeInfo()
        {
            AlertItem item = (AlertItem)this._AlertFacade.GetAlertItem(Request["ITEMSEQUENCE"]);
            this.txtAlertItemSequence.Text = item.ItemSequence;
            this.txtAlertItemDesc.Text = item.Description;

            //初始化项次设定
            object[] alertErrors = this._AlertFacade.GetAlertDirectPasses(item.ItemSequence);

            if (alertErrors != null && alertErrors.Length > 0)
            {
                this.ddlItemType.SelectedValue = ((AlertDirectPass)alertErrors[0]).ItemType;
                this.txtBaseOutPut.Text = ((AlertDirectPass)alertErrors[0]).BaseOutput.ToString();

                this.rblTimeDimensionGroup.SelectedValue = ((AlertDirectPass)alertErrors[0]).TimeDimension;

                this.txtAlertStandard.Text = ((AlertDirectPass)alertErrors[0]).AlertValue.ToString();

                //产生预警通告
                if (((AlertDirectPass)alertErrors[0]).GenerateNotice == "Y")
                {
                    this.chbGenerateNotice.Checked = true;
                    this.chbLinePause.Enabled = true;
                }
                else
                {
                    this.chbGenerateNotice.Checked = false;
                    this.chbLinePause.Enabled = false;
                }

                //发送邮件
                if (((AlertDirectPass)alertErrors[0]).SendMail == "Y")
                {
                    this.chbSendMail.Checked = true;
                }
                else
                {
                    this.chbSendMail.Checked = false;
                }

                //停线控制
                if (((AlertDirectPass)alertErrors[0]).LinePause == "Y")
                {
                    this.chbLinePause.Checked = true;
                }
                else
                {
                    this.chbLinePause.Checked = false;
                }
                this.ddlItemType.SelectedValue = ((AlertDirectPass)alertErrors[0]).ItemType;
            }

        }

        private void InitItemType()
        {
            this.ddlItemType.Items.Clear();
            this.ddlItemType.Items.Add(new ListItem(_LanguageComponent1.GetString(ItemType.ITEMTYPE_FINISHEDPRODUCT), ItemType.ITEMTYPE_FINISHEDPRODUCT));
            this.ddlItemType.Items.Add(new ListItem(_LanguageComponent1.GetString(ItemType.ITEMTYPE_SEMIMANUFACTURE), ItemType.ITEMTYPE_SEMIMANUFACTURE));
        }

        private void InitTimeDimension()
        {
            this.rblTimeDimensionGroup.Items.Clear();

            RadioButtonListBuilder builder1 = new RadioButtonListBuilder(new AlertTimeDimension(), this.rblTimeDimensionGroup, this._LanguageComponent1);
            builder1.Build();
        }

        #endregion
    }
}
