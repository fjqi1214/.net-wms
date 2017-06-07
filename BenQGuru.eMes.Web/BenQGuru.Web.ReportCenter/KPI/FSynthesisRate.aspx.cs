using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.WebQuery;
using Infragistics.UltraGauge.Resources;
using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.Web.ReportCenter.KPI
{
    public partial class FSynthesisRate : BaseMPageMinus
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private BenQGuru.eMES.WebQuery.KPIQueryFacade KPIFacade;
        private BenQGuru.eMES.BaseSetting.ShiftModelFacade ShiftFacade;
        private static decimal ResultValue = 0;
        private static int RefreshTimes = 0;
        private static DateTime? firstDate;
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


            this.ddlRating.Items.Clear();
            ddlRating.Items.Add(new ListItem("", ""));
            string[] ratingarr = new string[] { "很快", "快", "一般", "慢" };
            for (int i = 0; i < ratingarr.Length; i++)
            {
                ddlRating.Items.Add(new ListItem(ratingarr[i], ratingarr[i]));
            }
            ddlRating.Items[1].Selected = true;

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
                string rating = this.ddlRating.SelectedValue;
                decimal drate = 0.00M;
                if (rating.Equals("很快"))
                {
                    drate = 1.25M;
                }
                else if (rating.Equals("快"))
                {
                    drate = 1.00M;
                }
                else if (rating.Equals("一般"))
                {
                    drate = 0.85M;
                }
                else if (rating.Equals("慢"))
                {
                    drate = 0.60M;
                }
                int begindate = FormatHelper.TODateInt(this.dateStartDateQuery.Text == string.Empty ? "0" : this.dateStartDateQuery.Text);
                int enddate = FormatHelper.TODateInt(this.dateEndDateQuery.Text == string.Empty ? "99999999" : this.dateEndDateQuery.Text);
                //this.UltraGauge1.RefreshInterval = -1;
                //根据工单和产线得到当前生产的所有资源
                if (KPIFacade == null) { KPIFacade = new KPIQueryFacade(base.DataProvider); }

                object[] Res = KPIFacade.GetKPIRes(mocode, sscode);
                List<decimal> cts = new List<decimal>();
                if (Res != null)
                {

                    firstDate = KPIFacade.GetInputTime(((Resource)Res[0]).ResourceCode, shiftcode, tpcode, begindate, enddate, mocode);
                    if (firstDate == null)
                    { return; }
                    foreach (Resource item in Res)
                    {
                       object outputs = KPIFacade.GetOutPutTimes(item.ResourceCode, mocode, shiftcode, begindate, enddate);
                       if (outputs != null)
                       {
                           int output = ((BenQGuru.eMES.Domain.Report.ReportOPQty)outputs).OutputTimes;

                           if (output > 0)
                           {
                               object serial = KPIFacade.GetMaxMiniSerial(item.ResourceCode, mocode, shiftcode, begindate, enddate);
                               if (serial != null)
                               {
                                   int maxserial = ((BenQGuru.eMES.Domain.Report.ReportOPQty)serial).InputTimes;
                                   int minserial = ((BenQGuru.eMES.Domain.Report.ReportOPQty)serial).OutputTimes;
                                   int time = KPIFacade.GetMaxMinTime(maxserial, minserial);
                                   decimal CT = Math.Round(Convert.ToDecimal(time) / (output - 1), 2);
                                   cts.Add(CT);
                               }
                           }
                       }
                    }
                }

                //根据查询条件得出生产途程的最后一道工序的资源
                object[] LastRes = KPIFacade.GetKpiLastRes(sscode, mocode);
                if (LastRes != null)
                {
                    Resource resouce = (Resource)LastRes[0];
                    int outputsum = KPIFacade.GetOutputSum(begindate, enddate, resouce.ResourceCode, shiftcode, tpcode);
                    if (outputsum > 0)
                    {
                        DateTime lastDate = KPIFacade.GetLastProTime(resouce.ResourceCode, shiftcode, tpcode, begindate, enddate, mocode);
                        
                        TimeSpan timeSpan = lastDate.Subtract(firstDate.Value);
                        decimal subTime = Convert.ToDecimal(timeSpan.Days.ToString()) * 3600 * 24 + Convert.ToDecimal(timeSpan.Hours.ToString()) * 3600 +
                            Convert.ToDecimal(timeSpan.Minutes.ToString()) * 60 + Convert.ToDecimal(timeSpan.Seconds);
                        decimal avgrating = Convert.ToDecimal(subTime) / outputsum;
                        if (cts.Count > 0)
                        {
                            decimal sum = 0;
                            decimal max = 0;
                            foreach (var ct in cts)
                            {
                                sum += ct;
                                max = max > ct ? max : ct;
                            }

                            decimal result = Math.Round(sum * drate / avgrating, 4) * 100;
                            ResultValue = result;
                            UltraGauge1.RefreshInterval = 1;
                            RefreshTimes = 0;
                        }

                    }
                   
                }
            
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
                    (this.UltraGauge1.Gauges.FromKey("digital") as SegmentedDigitalGauge).Text = Math.Round(ResultValue,2).ToString();
                    UltraGauge1.RefreshInterval = 15;//正常15秒刷新一次
              //  }

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
                    ddlMoTypeQuery.Items.Add(new ListItem(item.TimePeriodCode, item.TimePeriodCode));
                }
            }
        }
    }
}