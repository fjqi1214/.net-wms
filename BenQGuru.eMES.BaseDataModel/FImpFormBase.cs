using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.BaseDataModel
{
    public partial class FImpFormBase : Form
    {
        public FMain MainForm = null;
        public string CurrentImportName = "";

        public List<ConfigObject> configObjList = null;
        public MatchType configMatchType = null;
        
        public FImpFormBase()
        {
            InitializeComponent();
        }

        protected Excel.Application excelApp = null;

        private void axWebBrowser1_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
        {
            
            Object o = e.pDisp;
            Object oDocument = o.GetType().InvokeMember("Document", System.Reflection.BindingFlags.GetProperty, null, o, null);
            Object oApplication = o.GetType().InvokeMember("Application", System.Reflection.BindingFlags.GetProperty, null, oDocument, null);
            excelApp = (Excel.Application)oApplication;
            
        }

        private void ReleaseCOM(Object o)
        {
            try { System.Runtime.InteropServices.Marshal.ReleaseComObject(o); }
            catch { }
            finally { o = null; }
        }

        public void ShowExcel(string fileName)
        {
            try
            {
                this.axWebBrowser1.Visible = true;
                object refmissing = System.Reflection.Missing.Value;
                this.axWebBrowser1.Navigate(fileName, ref refmissing, ref refmissing, ref refmissing, ref refmissing);
                Application.DoEvents();
                axWebBrowser1.ExecWB(SHDocVw.OLECMDID.OLECMDID_HIDETOOLBARS, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, ref refmissing, ref refmissing);
            }
            catch
            {
                this.axWebBrowser1.Visible = false;
            }
        }

        public void CloseExcelWorkbook()
        {
            if (excelApp != null)
            {
                object refmissing = System.Reflection.Missing.Value;
                this.axWebBrowser1.Navigate("about:blank");
                excelApp.ActiveWorkbook.Close(false, refmissing, refmissing);
                Application.DoEvents();
                excelApp.Quit();
                Application.DoEvents();
                this.ReleaseCOM(excelApp);
                this.axWebBrowser1.Dispose();
                excelApp = null;

            }
        }

        public void SetActive()
        {
            this.axWebBrowser1.Focus();
        }

        private void FImpFormBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                /*
                if (excelApp != null)
                {
                    try
                    {
                        object refmissing = System.Reflection.Missing.Value;
                        if (excelApp.ActiveWorkbook != null)
                            excelApp.ActiveWorkbook.Close(false, refmissing, refmissing);
                        Application.DoEvents();
                    }
                    catch { }
                    excelApp.Quit();
                    Application.DoEvents();
                    ReleaseCOM(excelApp);
                    Application.DoEvents();
                }

                Application.DoEvents();
                this.axWebBrowser1.Navigate("about:blank");
                Application.DoEvents();
                this.axWebBrowser1.Dispose();
                Application.DoEvents();
                */
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseExcelWorkbook();
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (excelApp == null || excelApp.ActiveSheet == null)
                return;
            Excel.Worksheet sheet = (Excel.Worksheet)excelApp.ActiveSheet;

            string[] strHeader = ReadExcelHeader(sheet);
            if (strHeader.Length == 0)
                return;

            ImportData(sheet, strHeader);
        }
        /// <summary>
        /// 导入数据
        /// </summary>
        protected virtual void ImportData(Excel.Worksheet sheet, string[] strHeader)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(ApplicationService.DataProvider);
            ImportDataEngine impEng = new ImportDataEngine(ApplicationService.DataProvider, this.configObjList, this.configMatchType, ApplicationService.LoginUserCode, dbDateTime);
            impEng.ImportDataMapping = Properties.Settings.Default.DataMappingType;
            impEng.UpdateRepeatData = Properties.Settings.Default.DataRepeat;
            ApplicationService.DataProvider.BeginTransaction();
            try
            {
                int iRow = 2;
                int iImportedCount = 0;
                while (true)
                {
                    string[] strRow = ReadExcelRow(sheet, iRow, strHeader.Length);
                    if (strRow == null)
                        break;
                    string strResult = impEng.ImportRow(this.CurrentImportName, strHeader, strRow);
                    if (strResult != "")
                    {
                        WriteImportErrorMessage(sheet, iRow, strHeader.Length + 1, strResult);
                        if (strResult == "$DATA_REPEAT_CANCEL")      // 如果是重复数据，并设置终止
                        {
                            throw new Exception(strResult);
                        }
                        else if (strResult != "$Import_Unique_Data_Exist")  // $Import_Unique_Data_Exist表示重复数据，设置忽略
                        {
                            if (Properties.Settings.Default.DataError == ImportDataEngine.OnErrorDeal.Cancel)
                                throw new Exception(strResult);
                        }
                    }
                    else
                        iImportedCount++;
                    
                    iRow++;
                }
                ApplicationService.DataProvider.CommitTransaction();
                MessageBox.Show(UserControl.MutiLanguages.ParserMessage("$CycleImport_Success [" + iImportedCount.ToString() + "]"), this.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ApplicationService.DataProvider.RollbackTransaction();
                MessageBox.Show(UserControl.MutiLanguages.ParserMessage(ex.Message), this.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        /// <summary>
        /// 读取Excel列头
        /// </summary>
        private string[] ReadExcelHeader(Excel.Worksheet sheet)
        {
            List<string> listHeader = new List<string>();
            int iCol = 1;
            while (true)
            {
                Excel.Range range = (Excel.Range)sheet.Cells[1, iCol];
                if (range.Value2 == null || range.Value2.ToString() == "")
                    break;
                listHeader.Add(range.Value2.ToString());
                iCol++;
            }
            string[] strHeader = new string[listHeader.Count];
            listHeader.CopyTo(strHeader);
            return strHeader;
        }
        /// <summary>
        /// 读取Excel中的一行数据
        /// </summary>
        protected string[] ReadExcelRow(Excel.Worksheet sheet, int rowIndex, int columnCount)
        {
            string[] strRow = new string[columnCount];
            bool bExistData = false;
            for (int i = 0; i < columnCount; i++)
            {
                Excel.Range range = (Excel.Range)sheet.Cells[rowIndex, i + 1];
                if (range.Value2 != null && range.Value2.ToString() != "")
                {
                    strRow[i] = range.Value2.ToString();
                    bExistData = true;
                }
                else
                    strRow[i] = "";
            }
            if (bExistData == true)
                return strRow;
            else
                return null;
        }

        /// <summary>
        /// 将导入时产生的错误信息显示到Excel中
        /// </summary>
        protected void WriteImportErrorMessage(Excel.Worksheet sheet, int rowIndex, int headerCount, string message)
        {
            Excel.Range range = (Excel.Range)sheet.Cells[rowIndex, headerCount + 1];
            range.Value2 = UserControl.MutiLanguages.ParserMessage(message);
        }

        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            if (this.excelApp != null)
            {
                try
                {
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Title = "另存为";
                    dialog.Filter = "Excel 文件|*.xls";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string strFileName = dialog.FileName;
                        object refmissing = System.Reflection.Missing.Value;
                        this.excelApp.ActiveWorkbook.SaveAs(strFileName, refmissing, refmissing, refmissing, refmissing, refmissing, Excel.XlSaveAsAccessMode.xlExclusive, refmissing, refmissing, refmissing, refmissing, refmissing);
                    }
                }
                catch
                {
                    MessageBox.Show("保存失败", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (this.excelApp != null)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Excel 文件|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string strFileName = dialog.FileName;
                    object refmissing = System.Reflection.Missing.Value;
                    this.excelApp.ActiveWorkbook.Close(false, refmissing, refmissing);
                    Application.DoEvents();

                    //this.excelApp.Workbooks.Open(strFileName, refmissing, refmissing, refmissing, refmissing, refmissing, refmissing, refmissing, refmissing, refmissing, refmissing, refmissing, refmissing, refmissing, refmissing);
                    ShowExcel(strFileName);
                }
            }
        }

    }
}