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
    public partial class FAlertErrorMP : BasePage
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
            url += "&FROMPAGE=AlertError";

            this.Response.Redirect(this.MakeRedirectUrl(url));
            
        }
        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {

            if (!SaveCheck())
            {
                return;
            }

            AlertError alertError = new AlertError();

            alertError.ItemSequence = this.txtAlertItemSequence.Text;
            alertError.ItemType = this.ddlItemType.SelectedValue;
            alertError.ErrorCode = this.txtErrorCode.Text;
            alertError.TimeDimension = this.rblTimeDimensionGroup.SelectedValue;

            //是否区分线别
            if (this.chbLineDivision.Checked)
            {
                alertError.LineDivision = "Y";
            }else{
                alertError.LineDivision = "N";
            }

            //产生预警通告
            if (this.chbGenerateNotice.Checked)
            {
                alertError.GenerateNotice = "Y";
            }
            else
            {
                alertError.GenerateNotice = "N";
            }

            //发送邮件
            if (this.chbSendMail.Checked)
            {
                int count = this._AlertFacade.QueryAlertMailSettingsCount(alertError.ItemSequence);
                if (count > 0)
                {
                    alertError.SendMail = "Y";
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Error_EmailSetup_Need", this._LanguageComponent1);
                    return;
                }
            }
            else
            {
                alertError.SendMail = "N";
            }

            //停线控制
            if (this.chbLinePause.Checked)
            {
                alertError.LinePause = "Y";
            }
            else
            {
                alertError.LinePause = "N";
            }

            alertError.AlertValue = int.Parse(this.txtAlertStandard.Text);
            alertError.MaintainDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
            alertError.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            alertError.MaintainUser = this.GetUserCode();

            this._AlertFacade.SaveAlertError(alertError);
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

        private bool SaveCheck(){
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblNGCodeQuery, this.txtErrorCode, int.MaxValue, true));
            manager.Add(new NumberCheck(this.lblAlertStandard, this.txtAlertStandard, true));            

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this._LanguageComponent1);
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
            object[] alertErrors = this._AlertFacade.GetAlertErrors(item.ItemSequence);

            if (alertErrors != null && alertErrors.Length > 0)
            {
                string errorCode = "";
                foreach (AlertError error in alertErrors)
                {
                    errorCode += error.ErrorCode + ",";
                }
                errorCode = errorCode.TrimEnd(',');

                this.ddlItemType.SelectedValue = ((AlertError)alertErrors[0]).ItemType;
                this.txtErrorCode.Text = errorCode;

                this.rblTimeDimensionGroup.SelectedValue = ((AlertError)alertErrors[0]).TimeDimension;

                this.txtAlertStandard.Text = ((AlertError)alertErrors[0]).AlertValue.ToString();

                //是否区分线别
                if (((AlertError)alertErrors[0]).LineDivision == "Y")
                {
                    this.chbLineDivision.Checked = true;
                }else{
                    this.chbLineDivision.Checked = false;
                }

                //产生预警通告
                if (((AlertError)alertErrors[0]).GenerateNotice == "Y")
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
                if (((AlertError)alertErrors[0]).SendMail == "Y")
                {
                    this.chbSendMail.Checked = true;
                }
                else
                {
                    this.chbSendMail.Checked = false;
                }

                //停线控制
                if (((AlertError)alertErrors[0]).LinePause == "Y")
                {
                    this.chbLinePause.Checked = true;
                }
                else
                {
                    this.chbLinePause.Checked = false;
                }
                this.ddlItemType.SelectedValue = ((AlertError)alertErrors[0]).ItemType;
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
