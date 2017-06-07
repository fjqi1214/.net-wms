using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace BenQGuru.eMES.DataExp
{
    public class ExcelEngine
        : BaseEngine
    {
        protected override void DoExp()
        {
            if (File.Exists(this.ExpFileName))
            {
                File.Delete(this.ExpFileName);
            }

            Application app = new Application();
            try
            {
                app.Visible = false;
                Workbook book = (Workbook)app.Workbooks.Add(Missing.Value);
                Worksheet sheet = (Worksheet)book.Worksheets[1];

                #region 写Title部分
                int col = 1;
                if (this.SchemaObject.NeedTitle)
                {
                    foreach (DataExpField field in this.SchemaObject.FieldList)
                    {
                        string str = this.GetColumnDesc(field.Name);

                        sheet.Cells[1, col] = str;
                        col++;
                    }
                }
                #endregion

                #region 导出数据部分
                int row = 2;
                col = 1;
                foreach (System.Data.DataRow dr in this.Data.Rows)
                {
                    col = 1;
                    foreach (DataExpField field in this.SchemaObject.FieldList)
                    {
                        string str = dr[field.Name].ToString();

                        sheet.Cells[row, col] = str;
                        col++;
                    }
                    row++;
                }
                #endregion

                book.SaveAs(this.ExpFileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value
                    , Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                book.Close(Missing.Value, Missing.Value, Missing.Value);
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                }
            }
        }
    }
}
