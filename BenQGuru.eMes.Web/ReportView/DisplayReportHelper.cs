using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Microsoft.Reporting.WebForms;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Domain.ReportView;

namespace BenQGuru.eMES.Web.ReportView
{
    public class DisplayReportHelper
    {
        public void BindReportViewer(ReportViewFacade rptFacade, RptViewDesignMain designMain, ReportViewer reportViewer, DataSet dataSource, RptViewUserSubscription[] viewerInput)
        {
            // 设置报表文件
            string strFile = HttpContext.Current.Server.MapPath("../") + designMain.ReportFileName;
            reportViewer.LocalReport.ReportPath = strFile;
            // 设置DataSource
            reportViewer.LocalReport.DataSources.Clear();
            if (dataSource.Tables.Count == 1)
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("MESRPT", dataSource.Tables[0]));
            else
            {
                for (int i = 0; i < dataSource.Tables.Count; i++)
                {
                    reportViewer.LocalReport.DataSources.Add(new ReportDataSource(dataSource.Tables[i].TableName, dataSource.Tables[i]));
                }
            }
            // 设置报表参数
            if (designMain.ReportBuilder == ReportBuilder.OffLine)
            {
                RptViewFileParameter[] fileParams = rptFacade.GetRptViewFileParametersByReportId(designMain.ReportID);
                if (fileParams != null)
                {
                    ReportParameter[] rptParams = new ReportParameter[fileParams.Length];
                    for (int i = 0; i < fileParams.Length; i++)
                    {
                        for (int n = 0; viewerInput != null && n < viewerInput.Length; n++)
                        {
                            if (viewerInput[n].InputType == ReportViewerInputType.FileParameter &&
                                viewerInput[n].InputName == fileParams[i].FileParameterName)
                            {
                                fileParams[i].DefaultValue = viewerInput[n].InputValue;
                            }
                        }
                        ReportParameter rptParam = new ReportParameter();
                        rptParam.Name = fileParams[i].FileParameterName;
                        rptParam.Values.Add(fileParams[i].DefaultValue);
                        rptParam.Visible = false;
                        rptParams[i] = rptParam;
                    }
                    reportViewer.LocalReport.SetParameters(rptParams);
                }
            }
        }
    }
}
