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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FOnWipOP 的摘要说明。
    /// </summary>
    public partial class FSPCQP : BaseQPageNew
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton cmdQuery;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected WebQueryHelper _helper = null;
        protected System.Web.UI.WebControls.TextBox TextBox1;
        protected System.Web.UI.WebControls.TextBox TextBox2;
        protected global::System.Web.UI.WebControls.TextBox txtDateQuery;
        protected global::System.Web.UI.WebControls.TextBox txtDateToQuery;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            PostBackTrigger trigger = new PostBackTrigger();
            trigger.ControlID = this.txtItemCodeQuery.ID;
            (this.Form1.FindControl("up1") as UpdatePanel).Triggers.Add(trigger);
            //this.imgItemCode.Attributes["onclick"] = "return SelectItem();";
            string path = this.Request.Url.AbsoluteUri;
            path = path.Substring(0, path.LastIndexOf("/") + 1);

            ClientScript.RegisterHiddenField("_server", path);

            ClientScript.RegisterHiddenField("_Error_Input_Empty", MessageCenter.ParserMessage("$Error_Input_Empty", this.languageComponent1));
            ClientScript.RegisterHiddenField("_Error_Number_Format_Error", MessageCenter.ParserMessage("$Error_Number_Format_Error", this.languageComponent1));
            ClientScript.RegisterHiddenField("_Error_Number_TooLittle", MessageCenter.ParserMessage("$Error_Number_TooLittle", this.languageComponent1));


            //日期控件多语言
            string strDatepickLangType = string.Empty;
            switch (this.languageComponent1.Language)
            {
                case "CHS":
                    strDatepickLangType = "zh-CN";
                    break;
                case "CHT":
                    strDatepickLangType = "zh-TW";
                    break;
                case "ENU":
                    break;
                default:
                    break;
            }
            string strDatepickerScript = @"  
                if($('.datepicker').html()!=null)
                {
                    //JqueryUi 日期控件
                    $('.datepicker').datepicker({
                    showOn: 'both',
                    buttonImage: '" + this.VirtualHostRoot + @"skin/images/calendar.gif',
                    buttonImageOnly: true,
                    changeMonth: true,
                    changeYear: true,
                    showButtonPanel: true,
                    dateFormat: 'yy-mm-dd',
                    buttonText: 'Choose Date',
                    //closeText: 'Close',
                    constrainInput: true,
                    beforeShow: function (input, inst) {
                        $(this).select();
                        //只读或者不可用的时候，不弹出
                        if ($(this).attr('disabled') == 'disabled' || $(this).attr('readOnly') == 'readOnly') {
                            return false;
                        }
                        $('#SPC_Chart').css('margin-top','220px');
                     },
                    onClose: function (input, inst) {
                        $('#SPC_Chart').css('margin-top','0px');
                    }
                    }).datepicker('option', $.datepicker.regional['" + strDatepickLangType + @"']);

                    $('.datepicker').addClass('textbox');
                  

                }";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "strDatepickerScript", strDatepickerScript, true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strDatepickerScript", strDatepickerScript, true);


            if (!Page.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtDateQuery.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.chbAuto.Checked = true;
                SetLanguage();
            }
        }
        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            //this.cmdQuery.ServerClick += new System.EventHandler(this.cmdQuery_Click);
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
            //this.imgItemCode.Click += new System.Web.UI.ImageClickEventHandler(this.imgItemCode_Click);
            this.txtItemCodeQuery.TextBox.TextChanged += new EventHandler(txtItemCodeQuery_TextChanged);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\..";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        protected void txtItemCodeQuery_TextChanged(object sender, System.EventArgs e)
        {
            BuildTestName();
        }

        private void imgItemCode_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            BuildTestName();
        }

        /// <summary>
        /// 测试项下拉列表
        /// </summary>
        private void BuildTestName()
        {           
            // Added by Icyer 2006/08/16
            this.txtItemCodeQuery.Text = this.txtItemCodeQuery.Text.ToUpper();
            this.drpTestNameQuery.Items.Clear();
            string itemcode = this.txtItemCodeQuery.Text;
            BenQGuru.eMES.MOModel.SPCFacade facade = new BenQGuru.eMES.MOModel.SPCFacade(this.DataProvider);
            object[] objs = facade.QuerySPCItemSpec(itemcode, string.Empty, 1, int.MaxValue);
            if (objs != null && objs.Length > 0)
            {
                object[] objsList = facade.GetAllSPCObject();
                Hashtable htObj = new Hashtable();
                if (objsList != null)
                {
                    for (int i = 0; i < objsList.Length; i++)
                    {
                        BenQGuru.eMES.Domain.SPC.SPCObject spcObj = (BenQGuru.eMES.Domain.SPC.SPCObject)objsList[i];
                        htObj.Add(spcObj.ObjectCode, spcObj.ObjectName);
                    }
                }
                for (int i = 0; i < objs.Length; i++)
                {
                    BenQGuru.eMES.Domain.SPC.SPCItemSpec spec = (BenQGuru.eMES.Domain.SPC.SPCItemSpec)objs[i];
                    if (this.drpTestNameQuery.Items.FindByValue(spec.ObjectCode) == null)
                    {
                        this.drpTestNameQuery.Items.Add(new ListItem(htObj[spec.ObjectCode].ToString(), spec.ObjectCode));
                    }
                }
                this.drpTestNameQuery.SelectedIndex = 0;
                drpTestNameQuery_SelectedIndexChanged(null, null);
            }
            // Added end
        }

        protected void drpTestNameQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.drpConditionQuery.Items.Clear();
            this.txtItemCodeQuery.Text = this.txtItemCodeQuery.Text.ToUpper();
            string itemcode = this.txtItemCodeQuery.Text;
            string objectCode = this.drpTestNameQuery.SelectedValue;
            if (itemcode == string.Empty || objectCode == string.Empty)
                return;
            BenQGuru.eMES.MOModel.SPCFacade facade = new BenQGuru.eMES.MOModel.SPCFacade(this.DataProvider);
            BenQGuru.eMES.Domain.SPC.SPCObject spcObj = (BenQGuru.eMES.Domain.SPC.SPCObject)facade.GetSPCObject(objectCode);
            if (spcObj != null)
            {
                if (spcObj.DateRange == "RANGE")
                {
                    this.lblEDateQuery.Visible = true;
                    this.txtDateToQuery.Visible = true;
                }
                else
                {
                    this.lblEDateQuery.Visible = false;
                    this.txtDateToQuery.Visible = false;
                }
                if (spcObj.GraphType == SPCChartType.XBar_R_Chart)
                    hidChartType.Value = "XR";
                else if (spcObj.GraphType == SPCChartType.NormalDistributionDiagram || spcObj.GraphType == SPCChartType.OQC_NormalDistributionDiagram)
                {
                    hidChartType.Value = "HISTOGRAM";
                }
            }
            object[] objs = facade.QuerySPCItemSpec(itemcode, objectCode, 1, int.MaxValue);
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    BenQGuru.eMES.Domain.SPC.SPCItemSpec spec = (BenQGuru.eMES.Domain.SPC.SPCItemSpec)objs[i];
                    this.drpConditionQuery.Items.Add(new ListItem(spec.ConditionName, spec.GroupSeq.ToString()));
                }
            }
        }

        public void SetLanguage()
        {
            this.lblComponent.Text = this.languageComponent1.GetString("$PageControl_Component");
            this.lblActiveX.Text = this.languageComponent1.GetString("$PageControl_ActiveX");
            this.lblCloseIE.Text = this.languageComponent1.GetString("$PageControl_CloseIE");
            this.lblReset.Text = this.languageComponent1.GetString("$PageControl_Reset");
            this.lblSetActiveX.Text = this.languageComponent1.GetString("$PageControl_SetActiveX");
            this.lblExportGuru.Text = this.languageComponent1.GetString("$PageControl_ExportGuru");
            this.lblHere.Text = this.languageComponent1.GetString("$PageControl_Here");
            this.lblDownGuru.Text = this.languageComponent1.GetString("$PageControl_DownGuru");
            this.lblDownInstr.Text = this.languageComponent1.GetString("$PageControl_DownInstr");
            this.lblHhere.Text = this.languageComponent1.GetString("$PageControl_Hhere");
            this.lblInserll.Text = this.languageComponent1.GetString("$PageControl_Inserll");
            this.lblSystemRight.Text = this.languageComponent1.GetString("$PageControl_SystemRight");
            this.lblSystemReset.Text = this.languageComponent1.GetString("$PageControl_SystemReset");
            this.lblBackSPC.Text = this.languageComponent1.GetString("$PageControl_BackSPC");
        }


    }
}
