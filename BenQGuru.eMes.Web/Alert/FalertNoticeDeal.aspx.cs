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

using Infragistics.WebUI.UltraWebGrid;

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
    public partial class FalertNoticeDeal : BasePage
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
                InitNoticeInfo();
            }
        }

        protected void cmdSaveReturn_ServerClick(object sender, System.EventArgs e)
        {
            if (this.txtReasonAnalysis.Text.Trim() == string.Empty && this.txtMeasureMethod.Text.Trim() == string.Empty)
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FAlertNoticeMP.aspx"));
            }
            else
            {
                if (!SaveCheck())
                {
                    return;
                }

                AlertNotice notice = (AlertNotice)this._AlertFacade.GetAlertNotice(int.Parse(Request["SERIAL"]));

                notice.Status = AlertNoticeStatus.AlertNoticeStatus_HasDeal;
                //Modified By Nettie Chen 2009/09/22
                //notice.DealDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
                //notice.DealTime = FormatHelper.TOTimeInt(DateTime.Now);
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                notice.DealDate = dbDateTime.DBDate;
                notice.DealTime = dbDateTime.DBTime;
                //End Modified
                notice.DealUser = this.GetUserCode();
                
                notice.AnalysisReason = this.txtReasonAnalysis.Text.Trim();
                notice.DealMethods = this.txtMeasureMethod.Text.Trim();

                ArrayList noticeArray = new ArrayList();
                noticeArray.Add(notice);

                this._AlertFacade.QuickDealAlertNotice((AlertNotice[])noticeArray.ToArray(typeof(AlertNotice)));
                this.Response.Redirect(this.MakeRedirectUrl("./FAlertNoticeMP.aspx"));
            }
        }

        private bool SaveCheck()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblReasonAnalysis, this.txtReasonAnalysis, 1000, false));
            manager.Add(new LengthCheck(this.lblMeasureMethod, this.txtMeasureMethod, 1000, false));

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
            AlertNotice notice = (AlertNotice)this._AlertFacade.GetAlertNotice(int.Parse(Request["SERIAL"]));
            this.txtAlertItemType.Text = this._LanguageComponent1.GetString(notice.AlertType);
            this.txtAlertItemSequence.Text = notice.ItemSequence;
            this.txtAlertItemDesc.Text = notice.Description;
            this.txtAlertNotice.Text = notice.NoticeContent;

            this.txtReasonAnalysis.Text = notice.AnalysisReason;
            this.txtMeasureMethod.Text = notice.DealMethods;
        }

        #endregion

       
    }
}
