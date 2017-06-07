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
    public partial class FAlertOQCNGMP : BaseMPageMinus
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

                DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);

                datStartDateWhere.Text = now.DateTime.ToString("yyyy-MM-dd");
                timeShiftBeginTimeEdit.TimeString = FormatHelper.ToTimeString(now.DBTime);

                InitNoticeInfo();
            }
        }

        protected void cmdMailSetup_ServerClick(object sender, System.EventArgs e)
        {
            string url = "./FAlertMailSettingMP.aspx?ITEMSEQUENCE=";
            url += Request["ITEMSEQUENCE"];
            url += "&FROMPAGE=AlertOQCNG";

            this.Response.Redirect(this.MakeRedirectUrl(url));

        }
        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {

            if (!SaveCheck())
            {
                return;
            }

            AlertOQCNG alertOQCNG = new AlertOQCNG();

            alertOQCNG.ItemSequence = this.txtAlertItemSequence.Text;
            alertOQCNG.ItemType = this.ddlItemType.SelectedValue;
            alertOQCNG.ErrorCode = this.txtErrorCode.Text;


            //产生预警通告
            if (this.chbGenerateNotice.Checked)
            {
                alertOQCNG.GenerateNotice = "Y";
            }
            else
            {
                alertOQCNG.GenerateNotice = "N";
            }

            //发送邮件
            if (this.chbSendMail.Checked)
            {
                int count = this._AlertFacade.QueryAlertMailSettingsCount(alertOQCNG.ItemSequence);
                if (count > 0)
                {
                    alertOQCNG.SendMail = "Y";
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Error_EmailSetup_Need", this._LanguageComponent1);
                    return;
                }
            }
            else
            {
                alertOQCNG.SendMail = "N";
            }

            alertOQCNG.AlertValue = int.Parse(this.txtAlertStandard.Text);
            alertOQCNG.MaintainDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
            alertOQCNG.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            alertOQCNG.MaintainUser = this.GetUserCode();
            alertOQCNG.StartDate = FormatHelper.TODateInt(this.datStartDateWhere.Text);
            alertOQCNG.StartTime = FormatHelper.TOTimeInt(this.timeShiftBeginTimeEdit.Text);

            this._AlertFacade.SaveAlertOQCNG(alertOQCNG);
            WebInfoPublish.Publish(this, "$CS_Save_Success", this._LanguageComponent1);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FAlertItemMP.aspx"));
        }

        private bool SaveCheck()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblNGCodeQuery, this.txtErrorCode, int.MaxValue, true));
            manager.Add(new DateCheck(this.lblBegindate, datStartDateWhere.Text, true));
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
            object[] alertOQCNG = this._AlertFacade.GetAlertOQCNGs(item.ItemSequence);

            if (alertOQCNG != null && alertOQCNG.Length > 0)
            {
                string errorCode = "";
                foreach (AlertOQCNG error in alertOQCNG)
                {
                    errorCode += error.ErrorCode + ",";
                }
                errorCode = errorCode.TrimEnd(',');

                this.ddlItemType.SelectedValue = ((AlertOQCNG)alertOQCNG[0]).ItemType;
                this.txtErrorCode.Text = errorCode;

                this.txtAlertStandard.Text = ((AlertOQCNG)alertOQCNG[0]).AlertValue.ToString();

                this.timeShiftBeginTimeEdit.Text = FormatHelper.ToTimeString(((AlertOQCNG)alertOQCNG[0]).StartTime);
                this.datStartDateWhere.Text = FormatHelper.ToDateString(((AlertOQCNG)alertOQCNG[0]).StartDate);

                //产生预警通告
                if (((AlertOQCNG)alertOQCNG[0]).GenerateNotice == "Y")
                {
                    this.chbGenerateNotice.Checked = true;
                }
                else
                {
                    this.chbGenerateNotice.Checked = false;
                }

                //发送邮件
                if (((AlertOQCNG)alertOQCNG[0]).SendMail == "Y")
                {
                    this.chbSendMail.Checked = true;
                }
                else
                {
                    this.chbSendMail.Checked = false;
                }

                this.ddlItemType.SelectedValue = ((AlertOQCNG)alertOQCNG[0]).ItemType;
            }

        }

        private void InitItemType()
        {
            this.ddlItemType.Items.Clear();
            this.ddlItemType.Items.Add(new ListItem(_LanguageComponent1.GetString(ItemType.ITEMTYPE_FINISHEDPRODUCT), ItemType.ITEMTYPE_FINISHEDPRODUCT));
            this.ddlItemType.Items.Add(new ListItem(_LanguageComponent1.GetString(ItemType.ITEMTYPE_SEMIMANUFACTURE), ItemType.ITEMTYPE_SEMIMANUFACTURE));
        }

        #endregion
    }
}
