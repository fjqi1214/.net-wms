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

using BenQGuru.eMES.Common.Config;
using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.WatchPanel;
using BenQGuru.eMES.Web.Helper;

namespace BenQuru.eMES.Web
{
    public partial class FException : BasePage
    {
        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;
                this.AutoRefresh = false;
            }

            DoQuery();
        }

        public bool AutoRefresh
        {
            get
            {
                if (this.ViewState["AutoRefresh"] != null)
                {
                    try
                    {
                        return bool.Parse(this.ViewState["AutoRefresh"].ToString());
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            set
            {
                this.ViewState["AutoRefresh"] = value.ToString();

                if (value)
                {
                    this.RefreshController1.Start();
                }
                else
                {
                    this.RefreshController1.Stop();
                }
            }
        }

        private void DoQuery()
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);

            //Exception
            Label labelException = new Label();
            labelException.Text = string.Empty;

            object[] exceptionArray = watchPanelFacade.QueryExceptionList(string.Empty, now.DBDate);
            if (exceptionArray != null)
            {
                for (int i = 0; i < exceptionArray.Length; i++)
                {
                    ExceptionEventWithDescription exception = (ExceptionEventWithDescription)exceptionArray[i];
                    if (labelException.Text.Trim().Length > 0)
                    {
                        labelException.Text += "<br>";
                    }

                    int seq = i + 1;
                    labelException.Text += seq.ToString() + ": ";
                    labelException.Text += exception.SSCode + " - " + exception.SSDesc + " - " + exception.ItemCode + " - " + exception.Description + " - " + exception.Memo;
                    labelException.Text += "(" + FormatHelper.ToTimeString(exception.BeginTime) + " - " + FormatHelper.ToTimeString(exception.EndTime) + ")";
                }
            }

            this.PanelException.Controls.Add(labelException);
        }
    }
}
