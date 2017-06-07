using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;

using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;

using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Client
{
    public partial class FPackPalletPrint : Form
    {
        public FPackPalletPrint()
        {
            InitializeComponent();
        }

        private IDomainDataProvider _domainDataProvider = Service.ApplicationService.Current().DataProvider;
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        private DataTable tblData = null;
        private bool m_AutoPrint;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private void FPackPalletPrint_Load(object sender, EventArgs e)
        {
            this.ReportViewerPallet.RefreshReport();
            UserControl.UIStyleBuilder.FormUI(this);
            timerAutoPrint.Enabled = this.m_AutoPrint;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.Print();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerAutoPrint_Tick(object sender, EventArgs e)
        {
            this.Print();
            this.Close();
        }

        private void Print()
        {
            Export(this.ReportViewerPallet.LocalReport);

            m_currentPageIndex = 0;

            if (m_streams == null || m_streams.Count == 0)
                return;

            PrintDocument printDoc = new PrintDocument();

            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();

            ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Success,
                "$CS_PRINT_SUCCESSFULLY"));

        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
              "  <PageWidth>8.27in</PageWidth>" +
              "  <PageHeight>11.7in</PageHeight>" +
              "  <MarginTop>0.25in</MarginTop>" +
              "  <MarginLeft>0.25in</MarginLeft>" +
              "  <MarginRight>0.00in</MarginRight>" +
              "  <MarginBottom>0.25in</MarginBottom>" +
              "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private Stream CreateStream(string name, string fileNameExtension, System.Text.Encoding encoding, string mimeType, bool willSeek)
        {
            string temppath = System.Environment.GetEnvironmentVariable("TEMP");

            Stream stream = new FileStream(temppath + @"\" + name + System.Guid.NewGuid().ToString() + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void InitTable()
        {
            if (this.tblData != null)
                return;

            tblData = new DataTable();
            tblData.Columns.AddRange(
                new DataColumn[]{
                    new DataColumn("Seq"),
                    new DataColumn("LotNo"),
                    new DataColumn("RCard"),
                    new DataColumn("CartonCode"),
                    new DataColumn("MOCode")
                }
            );
        }

        public void SetData(string palletCode, bool autoPrint)
        {
            this.m_AutoPrint = autoPrint;

            this.InitTable();

            PackageFacade packageFacade = new PackageFacade(this.DataProvider);
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);

            //Get head data
            DBDateTime dbDateTime;
            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string printDate = FormatHelper.ToDateString(dbDateTime.DBDate, "/");

            Pallet pallet = (Pallet)packageFacade.GetPallet(palletCode);            

            if (pallet == null)
                return;

            //string moCode = pallet.MOCode;
            string ssCode = pallet.SSCode;
            string itemCode = pallet.ItemCode;
            string itemDesc = "";
            string itemMachineType = "";
            string itemModelGroup = "";

            Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(itemCode, pallet.OrganizationID);
            if (material != null)
            {
                itemDesc = material.MaterialDescription == null ? "" : material.MaterialDescription;
                itemMachineType = material.MaterialMachineType == null ? "" : material.MaterialMachineType;
                itemModelGroup = material.MaterialModelGroup == null ? "" : material.MaterialModelGroup;
            }


            //Get grid data

            object[] palletDetail = packageFacade.GetPalletDetailInfo(palletCode);

            int seq = 0;
            if (palletDetail != null)
            {
                foreach (SimulationReport detail in palletDetail)
                {
                    seq++;

                    DataRow row = tblData.NewRow();
                    row["Seq"] = seq;
                    row["LotNo"] = detail.LOTNO;
                    row["RCard"] = detail.RunningCard;
                    row["CartonCode"] = detail.CartonCode;
                    row["MOCode"] = detail.MOCode;

                    tblData.Rows.Add(row);
                }
            }

            string appPath = Application.StartupPath + "\\PackPalletPrint.rdlc";

            this.ReportViewerPallet.LocalReport.DataSources.Clear();
            this.ReportViewerPallet.Reset();
            this.ReportViewerPallet.LocalReport.ReportPath = appPath;
            this.ReportViewerPallet.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSetPalletPrint_PalletDetail", tblData));

            ReportParameter param1 = new ReportParameter("PalletCode", new string[] { palletCode });
            ReportParameter param2 = new ReportParameter("PrintDate", new string[] { printDate });
            ReportParameter param3 = new ReportParameter("ItemCode", new string[] { itemCode });
            ReportParameter param4 = new ReportParameter("ItemDesc", new string[] { itemDesc });
            //ReportParameter param5 = new ReportParameter("MOCode", new string[] { moCode });
            ReportParameter param6 = new ReportParameter("SSCode", new string[] { ssCode });
            ReportParameter param7 = new ReportParameter("MachineCode", new string[] { itemMachineType });
            ReportParameter param8 = new ReportParameter("ScreenCode", new string[] { itemModelGroup });
            this.ReportViewerPallet.LocalReport.SetParameters(new ReportParameter[] { param1, param2, param3, param4, param6, param7, param8 });

            this.ReportViewerPallet.LocalReport.Refresh();
        }
    }
}