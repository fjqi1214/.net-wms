using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Domain.ReportView;

namespace BenQGuru.eMES.Web.ReportView
{
    public abstract class ReportWizardBasePage : BaseMPageMinus
    {
        private string sessionKey = "ReportDesignView_Session";
        protected ReportDesignView designView = null;
        protected string FirstStepPage = "";

        private System.Web.UI.HtmlControls.HtmlInputSubmit cmdNextButton;
        private System.Web.UI.HtmlControls.HtmlInputSubmit cmdBackButton;
        private System.Web.UI.HtmlControls.HtmlInputSubmit cmdCancelButton;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            object obj = this.FindControl("cmdNext");
            if (obj != null)
            {
                cmdNextButton = (HtmlInputSubmit)obj;
                cmdNextButton.ServerClick += new EventHandler(cmdNextButton_ServerClick);
            }
            obj = this.FindControl("cmdBack");
            if (obj != null)
            {
                cmdBackButton = (HtmlInputSubmit)obj;
                cmdBackButton.ServerClick += new EventHandler(cmdBackButton_ServerClick);
            }
            obj = this.FindControl("cmdCancel");
            if (obj != null)
            {
                cmdCancelButton = (HtmlInputSubmit)obj;
                cmdCancelButton.ServerClick += new EventHandler(cmdCancelButton_ServerClick);
            }

            designView = this.GetDesignView();

            if (this.Request.FilePath.Substring(this.Request.FilePath.LastIndexOf("/") + 1).ToUpper().StartsWith(FirstStepPage.ToUpper()) == false)
            {
                if (this.designView == null || this.designView.DesignMain == null)
                {
                    this.Response.Redirect(FirstStepPage);
                }
            }
            
        }

        void cmdCancelButton_ServerClick(object sender, EventArgs e)
        {
            this.Session.Remove(this.sessionKey);
            Response.Redirect("FRptMaintainStartPageMP.aspx");
        }

        void cmdBackButton_ServerClick(object sender, EventArgs e)
        {
            /*
            if (ValidateInput() == false)
                return;
            UpdateReportDesignView();
            this.SaveDesignView(designView);
            */
            RedirectToBack();
        }

        void cmdNextButton_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInput() == false)
                return;
            UpdateReportDesignView();
            this.SaveDesignView(designView);
            RedirectToNext();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (this.IsPostBack == false)
            {
                DisplayDesignData();
            }
        }

        protected ReportDesignView GetDesignView()
        {
            ReportDesignView view = null;
            if (string.IsNullOrEmpty(FirstStepPage) == false && this.Request.FilePath.Substring(this.Request.FilePath.LastIndexOf("/") + 1).ToUpper().StartsWith(FirstStepPage.ToUpper()) == true && 
                string.IsNullOrEmpty(this.GetRequestParam("isback")) == true)
                this.Session.Remove(this.sessionKey);
            if (this.Session[this.sessionKey] == null)
            {
                view = new ReportDesignView();
                if (this.Request.QueryString["reportid"] != "" && this.Request.QueryString["reportid"] != null)
                {
                    string strRptId = this.Request.QueryString["reportid"];
                    ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
                    view = rptFacade.BuildReportDesignViewByReportId(strRptId);
                    view.ReportID = view.DesignMain.ReportID;
                }
                this.Session.Add(this.sessionKey, view);
            }
            else
            {
                view = (ReportDesignView)this.Session[this.sessionKey];
            }
            return view;
        }
        protected void SaveDesignView(ReportDesignView view)
        {
            this.Session[this.sessionKey] = view;
        }

        protected abstract bool ValidateInput();
        protected abstract void RedirectToNext();
        protected abstract void RedirectToBack();
        protected abstract void UpdateReportDesignView();

        protected abstract void DisplayDesignData();

    }
}
