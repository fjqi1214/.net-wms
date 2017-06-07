using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Report;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Report;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Win.UltraWinGrid;

namespace BenQGuru.eMES.Client
{
    public partial class FQtyForm : BaseForm
    {
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;

        public FQtyForm()
        {
            InitializeComponent();
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private void ucButtonRefresh_Click(object sender, EventArgs e)
        {
            RefreshQty();
        }

        private void FQtyForm_Load(object sender, EventArgs e)
        {            
            Point pt = new Point(688, 267);
            this.Location = pt;
            this.ultraGridQty.DataSource = null;
            this.ultraGridQty.DataBind();
            RefreshQty();
            
            //this.InitPageLanguage();            
        }

        public void RefreshQty()
        {
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this.DataProvider);
            ReportFacade reportFacade = new ReportFacade(this.DataProvider);
            MOFacade moFacade = new MOFacade(this.DataProvider);

            //DateTime
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DateTime now = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            //ResCode
            string resCode = ApplicationService.Current().ResourceCode;

            //Shift day and shift
            string shiftCode = string.Empty;
            int shiftDay = 0;
            Resource res = (Resource)baseModelFacade.GetResource(resCode);
            if (res != null)
            {
                TimePeriod currTimePeriod = (TimePeriod)shiftModelFacade.GetTimePeriod(res.ShiftTypeCode, dbDateTime.DBTime);
                if (currTimePeriod != null) 
                {
                    shiftDay = shiftModelFacade.GetShiftDay(currTimePeriod, now);
                    shiftCode = currTimePeriod.ShiftCode;
                }

            }

            //刷新数量数据
            object[] reportSOQtyList = reportFacade.QueryMOCodeFromReportSOQty(shiftDay, shiftCode, resCode);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MOCode");
            dataTable.Columns.Add("PlanQty");
            dataTable.Columns.Add("ResOutputCurrent");
            dataTable.Columns.Add("ResOutputTotal");

            decimal sumPlanQty = 0;
            int sumResOutputCurrent = 0;
            int sumResOutputTotal = 0;
            if (reportSOQtyList != null)
            {
                foreach (ReportSOQty reportSOQty in reportSOQtyList)
                {
                    MO mo = (MO)moFacade.GetMO(reportSOQty.MOCode);
                    if (mo != null)
                    {
                        object[] outputCurrent = reportFacade.QueryOPCountSumFromReportSOQty(shiftDay, shiftCode, resCode, reportSOQty.MOCode);
                        object[] outputTotal = reportFacade.QueryOPCountSumFromReportSOQty(0, string.Empty, resCode, reportSOQty.MOCode);

                        if (outputCurrent != null && outputTotal != null)
                        {
                            DataRow row = dataTable.NewRow();
                            row["MOCode"] = reportSOQty.MOCode;
                            row["PlanQty"] = mo.MOPlanQty.ToString("0");
                            row["ResOutputCurrent"] = ((ReportSOQty)outputCurrent[0]).OPCount.ToString();
                            row["ResOutputTotal"] = ((ReportSOQty)outputTotal[0]).OPCount.ToString();
                            dataTable.Rows.Add(row);

                            sumPlanQty += mo.MOPlanQty;
                            sumResOutputCurrent += ((ReportSOQty)outputCurrent[0]).OPCount;
                            sumResOutputTotal += ((ReportSOQty)outputTotal[0]).OPCount;
                        }
                    }
                }
            }

            DataRow rowSum = dataTable.NewRow();
            rowSum["MOCode"] = UserControl.MutiLanguages.ParserString("Summary");
            rowSum["PlanQty"] = sumPlanQty.ToString("0");
            rowSum["ResOutputCurrent"] = sumResOutputCurrent.ToString();
            rowSum["ResOutputTotal"] = sumResOutputTotal.ToString();
            dataTable.Rows.Add(rowSum);

            this.ultraGridQty.DataSource = dataTable;
            this.ultraGridQty.DataBind();
        }

        private int _NormalHeight = 0;
        private FormWindowState _FormWindowState = FormWindowState.Normal;
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xf020;
            const int SC_MAXIMIZE = 0xf030;
            const int SC_CLOSE = 0xf060;

            if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_MINIMIZE)
            {
                if (_FormWindowState != FormWindowState.Minimized)
                {
                    _NormalHeight = this.Height;
                    this.Height = 0;
                    _FormWindowState = FormWindowState.Minimized;
                }
            }
            else if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_MAXIMIZE)
            {
                if (_FormWindowState == FormWindowState.Minimized)
                {
                    this.Height = _NormalHeight;
                    _FormWindowState = FormWindowState.Normal;
                }
            }
            else if ( m.WParam.ToInt32() == SC_CLOSE)
            {
                if (m.Msg == WM_SYSCOMMAND && _FormWindowState != FormWindowState.Minimized)
                {
                    _NormalHeight = this.Height;
                    this.Height = 0;
                    _FormWindowState = FormWindowState.Minimized;
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void ultraGridQty_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["MOCode"].Header.Caption = "工单";
            e.Layout.Bands[0].Columns["PlanQty"].Header.Caption = "计划数量";
            e.Layout.Bands[0].Columns["ResOutputCurrent"].Header.Caption = "当班该岗位产出";
            e.Layout.Bands[0].Columns["ResOutputTotal"].Header.Caption = "该岗位累计产出";

            e.Layout.Bands[0].Columns["MOCode"].Width = 75;
            e.Layout.Bands[0].Columns["PlanQty"].Width = 65;
            e.Layout.Bands[0].Columns["ResOutputCurrent"].Width = 100;
            e.Layout.Bands[0].Columns["ResOutputTotal"].Width = 100;

            this.ultraGridQty.DisplayLayout.Bands[0].Columns["MOCode"].CellActivation = Activation.ActivateOnly;
            this.ultraGridQty.DisplayLayout.Bands[0].Columns["PlanQty"].CellActivation = Activation.ActivateOnly;
            this.ultraGridQty.DisplayLayout.Bands[0].Columns["ResOutputCurrent"].CellActivation = Activation.ActivateOnly;
            this.ultraGridQty.DisplayLayout.Bands[0].Columns["ResOutputTotal"].CellActivation = Activation.ActivateOnly;

            this.ultraGridQty.DisplayLayout.Bands[0].Columns["PlanQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            this.ultraGridQty.DisplayLayout.Bands[0].Columns["ResOutputCurrent"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            this.ultraGridQty.DisplayLayout.Bands[0].Columns["ResOutputTotal"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

            this.InitGridLanguage(ultraGridQty);
        }

 
    }
}