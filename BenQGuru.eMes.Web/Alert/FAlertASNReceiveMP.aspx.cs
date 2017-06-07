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
    public partial class FAlertASNReceiveMP : BasePage
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
                InitOPCode();
                InitNoticeInfo();
            }
        }

        protected void cmdMailSetup_ServerClick(object sender, System.EventArgs e)
        {
            string url = "./FAlertMailSettingMP.aspx?ITEMSEQUENCE=";
            url += Request["ITEMSEQUENCE"];
            url += "&FROMPAGE=AlertLinePause";

            this.Response.Redirect(this.MakeRedirectUrl(url));

        }
        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {

            if (!SaveCheck())
            {
                return;
            }

            AlertLinePause alertLinePause = new AlertLinePause();

            alertLinePause.ItemSequence = this.txtAlertItemSequence.Text;

            //产生预警通告
            if (this.chbGenerateNotice.Checked)
            {
                alertLinePause.GenerateNotice = "Y";
            }
            else
            {
                alertLinePause.GenerateNotice = "N";
            }

            //发送邮件
            if (this.chbSendMail.Checked)
            {
                int count = this._AlertFacade.QueryAlertMailSettingsCount(alertLinePause.ItemSequence);
                if (count > 0)
                {
                    alertLinePause.SendMail = "Y";
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Error_EmailSetup_Need", this._LanguageComponent1);
                    return;
                }
            }
            else
            {
                alertLinePause.SendMail = "N";
            }

            alertLinePause.AlertValue = int.Parse(this.txtAlertStandard.Text);
            alertLinePause.OPCode = this.ddlOPCode.SelectedValue;
            alertLinePause.SSCode = this.txtSSCodeWhere.Text;
            alertLinePause.MaintainDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
            alertLinePause.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            alertLinePause.MaintainUser = this.GetUserCode();

            this._AlertFacade.SaveAlertLinePause(alertLinePause);
            WebInfoPublish.Publish(this, "$CS_Save_Success", this._LanguageComponent1);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FAlertItemMP.aspx"));
        }

        private bool SaveCheck()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblSSEdit, this.txtSSCodeWhere, int.MaxValue, true));
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

            txtSSCodeWhere.TextBox.Attributes.Add("readonly", "readonly");
            //初始化项次设定
            object[] alertLinePauses = this._AlertFacade.GetAlertLinePauses(item.ItemSequence);

            if (alertLinePauses != null && alertLinePauses.Length > 0)
            {
                string SSCode = "";
                foreach (AlertLinePause error in alertLinePauses)
                {
                    SSCode += error.SSCode + ",";
                }
                SSCode = SSCode.TrimEnd(',');

                this.txtAlertStandard.Text = ((AlertLinePause)alertLinePauses[0]).AlertValue.ToString();

                this.txtSSCodeWhere.Text = SSCode;

                this.ddlOPCode.SelectedValue = ((AlertLinePause)alertLinePauses[0]).OPCode;

                //产生预警通告
                if (((AlertLinePause)alertLinePauses[0]).GenerateNotice == "Y")
                {
                    this.chbGenerateNotice.Checked = true;
                }
                else
                {
                    this.chbGenerateNotice.Checked = false;
                }

                //发送邮件
                if (((AlertLinePause)alertLinePauses[0]).SendMail == "Y")
                {
                    this.chbSendMail.Checked = true;
                }
                else
                {
                    this.chbSendMail.Checked = false;
                }
            }

        }

        private void InitOPCode()
        {
            //ddlOPCode
            DropDownListBuilder builder = new DropDownListBuilder(this.ddlOPCode);
            builder.HandleGetObjectList = new GetObjectListDelegate((new BaseModelFacade(this.DataProvider)).QueryOperation);
            builder.Build("OPCode", "OPCode");
            this.ddlOPCode.SelectedIndex = 0;
        }

        #endregion
    }
}
