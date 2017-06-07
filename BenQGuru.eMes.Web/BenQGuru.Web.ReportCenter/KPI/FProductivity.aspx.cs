 using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Common.Config;
using BenQGuru.eMES.Material;
using System.Drawing;
using Infragistics.UltraGauge.Resources;

namespace BenQGuru.Web.ReportCenter.KPI
{
    public partial class Productivity : BaseMPageMinus
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private BenQGuru.eMES.WebQuery.KPIQueryFacade KPIFacade;
        private BenQGuru.eMES.BaseSetting.ShiftModelFacade ShiftFacade;
        private static decimal ResultValue = 0;
        private static int RefreshTimes = 0;
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
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }

        private void InitGauge()
        {
            RadialGauge gauge = this.UltraGauge1.Gauges[0] as RadialGauge;
            RadialGaugeScale scale = ((RadialGauge)this.UltraGauge1.Gauges[0]).Scales[0];
            scale.Markers[0].Value = 0;

            RadialGaugeRange range0 = gauge.Scales[0].Ranges[0];
            range0.StartValue = 0;
            range0.EndValue = 60;
            RadialGaugeRange range1 = gauge.Scales[0].Ranges[1];
            range1.StartValue = 60;
            range1.EndValue = 90;
            RadialGaugeRange range2 = gauge.Scales[0].Ranges[2];
            range2.StartValue = 90;
            range2.EndValue = 100;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                InitGauge();
                InitddlShiftCodeWhere();
                this.dateStartDateQuery.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.dateEndDateQuery.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void InitddlShiftCodeWhere()
        {
            if (ShiftFacade == null)
            {
                ShiftFacade = new ShiftModelFacade(base.DataProvider);
            };
            object[] objs = ShiftFacade.GetAllShift();
            this.ddlShiftCodeWhere.Items.Clear();

            ddlShiftCodeWhere.Items.Add(new ListItem("", ""));
            if (objs != null)
            {
                foreach (Shift item in objs)
                {
                    ddlShiftCodeWhere.Items.Add(new ListItem(item.ShiftCode, item.ShiftCode));
                }
            }
            this.ddlMoTypeQuery.Items.Clear();
            ddlMoTypeQuery.Items.Add(new ListItem("", ""));

        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblMOCodeQuery, txtMOCodeWhere, 40, true));
            manager.Add(new LengthCheck(lblSS, txtSSCodeWhere, 40, true));
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            if (ValidateInput())
            {
                //判断必输条件
                string mocode = this.txtMOCodeWhere.Text.Trim();
                string sscode = this.txtSSCodeWhere.Text.Trim();
                string shiftcode = this.ddlShiftCodeWhere.SelectedValue;
                string tpcode = this.ddlMoTypeQuery.SelectedValue;
            
                int begindate = FormatHelper.TODateInt(this.dateStartDateQuery.Text == string.Empty ? "0" : this.dateStartDateQuery.Text);
                int enddate = FormatHelper.TODateInt(this.dateEndDateQuery.Text == string.Empty ? "99999999" : this.dateEndDateQuery.Text);
                //this.UltraGauge1.RefreshInterval = -1;
                //按照工单，产线和时间等条件查询投入和产出数量，班次和时段可以为空 
                if (KPIFacade == null) { KPIFacade = new KPIQueryFacade(base.DataProvider); }
                string inandout = KPIFacade.GetInOut(mocode, sscode, begindate, enddate, shiftcode, tpcode);
                decimal douInOut = Convert.ToDecimal(inandout);
                ResultValue = douInOut*100;
                UltraGauge1.RefreshInterval = 1;
                RefreshTimes = 0;
            }
        }

        protected void UltraGauge1_AsyncRefresh(object sender, Infragistics.WebUI.UltraWebGauge.RefreshEventArgs e)
        {
            if (ResultValue != 0)
            {
                RadialGaugeScale scale1 = ((RadialGauge)this.UltraGauge1.Gauges[0]).Scales[0];
                //if (ResultValue > 50 && RefreshTimes == 0)//以50进行分割，超过50，指针每隔1秒动一次
                //{

                //    scale1.Markers[0].Value = 50;
                //    (this.UltraGauge1.Gauges.FromKey("digital") as SegmentedDigitalGauge).Text = "50";
                //    RefreshTimes = 1;
                //}
                //else
                //{
                    cmdQuery_ServerClick(null, null);
                    scale1.Markers[0].Value = ResultValue;
                    (this.UltraGauge1.Gauges.FromKey("digital") as SegmentedDigitalGauge).Text = Math.Round(ResultValue, 2).ToString();
                    UltraGauge1.RefreshInterval = 15;//正常15秒刷新一次
             //   }

            }
        }

        protected void ddlShiftCodeWhere_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ShiftFacade == null)
            {
                ShiftFacade = new ShiftModelFacade(base.DataProvider);
            };
            string shiftcode = this.ddlShiftCodeWhere.SelectedValue;
            object[] tpcodeobjs = ShiftFacade.GetTimePeriodByShiftCode(shiftcode);
            if (tpcodeobjs != null)
            {
                foreach (TimePeriod item in tpcodeobjs)
                {
                    this.ddlMoTypeQuery.Items.Add(new ListItem(item.TimePeriodCode, item.TimePeriodCode));
                }
            }
        }
    }
}
