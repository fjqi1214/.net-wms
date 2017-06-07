using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;
using Infragistics.Web.UI.GridControls;
using System.Web.UI.WebControls;
//using BenQGuru.eMES.Web.Security;

namespace BenQGuru.eMES.Web.Helper
{
    /// <summary>
    /// BaseQPage 的摘要说明。
    /// </summary>
    public class BaseQPageNew : BaseMPageMinus
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected System.ComponentModel.IContainer components;

        public BaseQPageNew()
            : base()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);

        }



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

            this.Load += new EventHandler(BaseQPage_Load);
            this.Unload += new EventHandler(BaseQPage_Unload);
        }

        //		protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        //		{
        //#if DEBUG
        //			ResponseStatisical.Instance().SetBeginRequestTime( this,SessionHelper.Current(this.Session).ModuleCode);
        //#endif
        //
        //			base.RaisePostBackEvent (sourceControl, eventArgument);
        //#if DEBUG
        //			ResponseStatisical.Instance().SetEndRequestTime( this,SessionHelper.Current(this.Session).ModuleCode);
        //
        //			ResponseStatisical.Instance().Write();
        //#endif
        //
        //		}


        private void BaseQPage_Unload(object sender, EventArgs e)
        {
        }

        private void BaseQPage_Load(object sender, EventArgs e)
        {
            needVScroll = true;
            Control chkRe = this.FindControl("chbRefreshAuto");
            if (chkRe != null)
            {
                chkRe.Visible = false;
            }

        }

        protected override void InitWebGrid()
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "minHeight", "var minHeight=250;isQueryPage=true;", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "minHeight", "var minHeight=250;isQueryPage=true;", true);
           //this.gridWebGrid.DefaultColumnWidth = new Unit(70);
        }
        protected virtual void DoQuery()
        {
            DtSource = new DataTable();
            this.gridWebGrid.ClearDataSource();
            this.gridWebGrid.Columns.Clear();
            gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);

            //            string script = @"
            //                                           
            //                                             var grid=$('#gridWebGrid');
            //                                             var chartDiv=$('div[id$=\'Chart\']');
            //                                             if(grid.html()!=null)
            //                                             {
            //                                                 grid.closest('tr').closest('tr').css('display','block');
            //                                             }
            //                                             else if(chartDiv.html()!=null)
            //                                             {
            //                                                 chartDiv.closest('tr').closest('tr').css('display','block');
            //                                             }";
            //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SetDisplayBlock", script, true);
        }
    }
}
