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
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.BaseDataModel
{
    public partial class FImpOP : FImpFormBase
    {
        public FImpOP()
        {
            InitializeComponent();
        }

        protected override void ImportData(Excel.Worksheet sheet, string[] strHeader)
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
                    // 导入工序
                    try
                    {
                        BenQGuru.eMES.Domain.BaseSetting.Operation op = new BenQGuru.eMES.Domain.BaseSetting.Operation();
                        op.OPCode = strRow[0].Trim().ToUpper();
                        op.OPDescription = strRow[1].Trim().ToUpper();
                        op.OPControl = this.BuildOPControl(strRow);
                        op.OPCollectionType = "AUTO";
                        op.MaintainUser = ApplicationService.LoginUserCode;
                        op.MaintainDate = dbDateTime.DBDate;
                        op.MaintainTime = dbDateTime.DBTime;
                        ApplicationService.DataProvider.Insert(op);
                        iImportedCount++;
                    }
                    catch (Exception ex)
                    {
                        string strResult = ex.Message;
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

        private string BuildOPControl(string[] strRow)
        {
            string[] strOPAttrDesc = new string[] { 
                "测试工序",
                "上料工序",
                "包装工序",
                "FQC工序",
                "维修工序",
                "无",
                "中间产量工序",
                "中间投入工序" };
            string[] strOPAttrVal = new string[] { 
                "Testing",
                "ComponentLoading",
                "Packing",
                "OQC",
                "TS",
                "",
                "MidistOutput",
                "MidistInput" };
            List<string> listDesc = new List<string>();
            listDesc.AddRange(strOPAttrDesc);
            List<string> listVal = new List<string>();
            listVal.AddRange(strOPAttrVal);

            string strOpControl = "000000000000000";
            for (int i = 2; i <5; i++)
            {
                string attrDesc = strRow[i];
                if (attrDesc != "" && listDesc.Contains(attrDesc) == true)
                {
                    string attrVal = listVal[listDesc.IndexOf(attrDesc)];
                    if (attrVal != "")
                    {
                        OperationList opEnum = (OperationList)System.Enum.Parse(typeof(OperationList), attrVal, true);
                        int iIdx = (int)opEnum;
                        strOpControl = strOpControl.Substring(0, iIdx) + "1" + strOpControl.Substring(iIdx + 1);
                    }
                }
            }
            return strOpControl;
        }

    }
}