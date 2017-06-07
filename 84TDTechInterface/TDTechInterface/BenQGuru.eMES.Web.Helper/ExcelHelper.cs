using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Infragistics.Documents.Excel;

namespace BenQGuru.eMES.Web.Helper
{
    //定义委托类型的对象;
    public delegate void DataRowBindEventHandle(object sender, DataRowEventArgs e);
    public class ExcelHelper
    {
        #region 读Excel,静态方法
        /// <summary>
        /// 静态方法,读取规则的2维表的Excel成DataSet
        /// </summary>
        /// <param name="fileFullPath">全路径，包括文件名</param>
        /// <returns>DataSet</returns>
        public static DataSet Excel2DataSet(string fileFullPath)
        {
            DataSet dsExcel = new DataSet();
            DataTable dt = new DataTable();

            Infragistics.Documents.Excel.Workbook workBook = Infragistics.Documents.Excel.Workbook.Load(fileFullPath);
            Infragistics.Documents.Excel.Worksheet workSheet = workBook.Worksheets[0];

            int maxColumnCount = Infragistics.Documents.Excel.Workbook.MaxExcelColumnCount;
            int RowNum = 0;
            Int32 columnCount = 0;

            if (workSheet != null)
            {
                foreach (WorksheetRow row in workSheet.Rows)
                {
                    if (RowNum == 0)
                    {
                        Infragistics.Documents.Excel.WorksheetRow firstRow = row;

                        for (Int32 i = 0; i < maxColumnCount; i++)
                        {
                            if (firstRow.Cells[i].Value != null)
                            {
                                dt.Columns.Add(Convert.ToString(firstRow.Cells[i].Value));
                                columnCount = columnCount + 1;
                            }
                            else
                            {
                                break;
                            }
                        }

                    }
                    else
                    {
                        Infragistics.Documents.Excel.WorksheetRow dataRow = row;

                        if (dataRow.Cells[0].Value == null && dataRow.Cells[columnCount - 1].Value == null)
                        {
                            break;
                        }

                        DataRow dr = dt.NewRow();

                        for (Int32 j = 0; j < columnCount; j++)
                        {
                            dr[j] = dataRow.Cells[j].Value;
                        }

                        dt.Rows.Add(dr);
                    }
                    RowNum = RowNum + 1;
                }
            }

            dsExcel.Tables.Add(dt);

            return dsExcel;
        }
        #endregion

        #region 写Excel

        private string m_SheetName = "Data";

        /// <summary>
        /// Excel Columns Name array
        /// </summary>
        public string[] ColumnNames { get; set; }

        /// <summary>
        /// Excel Columns Fields array
        /// </summary>
        public string[] ColumnFields { get; set; }

        /// <summary>
        /// Excel Columns Name array
        /// </summary>
        public string[] ColumnDetailNames { get; set; }

        /// <summary>
        /// Excel Columns Fields array
        /// </summary>
        public string[] ColumnDetailFields { get; set; }

        public string[] RelationColumns { get; set; }

        /// <summary>
        /// Excel Sheet Name
        /// </summary>
        public string SheetName
        {
            get { return m_SheetName; }
            set { m_SheetName = value; }
        }

        /// <summary>
        /// Excel DataSource with DataTable
        /// </summary>
        public DataTable DataSource { get; set; }

        public DataSet DataSourceForTreeExcel { get; set; }


        public event DataRowBindEventHandle DataRowBind;

        private void OnDataRowBind(object sender, DataRowEventArgs e)
        {
            if (DataRowBind != null)
                DataRowBind(sender, e);
        }

        /// <summary>
        /// Save DataSource To Excel
        /// </summary>
        /// <param name="filePath">File Path(Include file name)</param>
        public void SaveAs(string filePath)
        {
            if (this.ColumnNames == null || this.ColumnNames.Length == 0)
            {
                throw new Exception("Please Set Property: ColumnsName!");
            }

            if (this.ColumnFields == null || this.ColumnFields.Length == 0)
            {
                throw new Exception("Please Set Property: ColumnFields!");
            }

            if (this.DataSource == null)
            {
                throw new Exception("Please Set Property: DataSource");
            }

            if (filePath.Equals(string.Empty))
            {
                throw new Exception("Please input file path");
            }

            Workbook workBook = new Workbook();
            Worksheet workSheet = null;

            workSheet = workBook.Worksheets.Add(this.SheetName);
            workSheet.DefaultColumnWidth = 3000;
            int rowIndex = 0;

            #region -- 生成表头 --
            for (int i = 0; i < this.ColumnNames.Length; i++)
            {
                workSheet.Rows[rowIndex].Cells[i].Value = this.ColumnNames[i];
                workSheet.Rows[rowIndex].Cells[i].CellFormat.Alignment = HorizontalCellAlignment.Center;
                workSheet.Rows[rowIndex].Cells[i].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;

            }
            #endregion

            rowIndex++;

            #region -- 生成数据行 --
            foreach (DataRow row in this.DataSource.Rows)
            {
                ExcelRow excelRow = new ExcelRow();
                excelRow.Cells = new List<ExcelCell>();

                for (int i = 0; i < this.ColumnFields.Length; i++)
                {
                    ExcelCell excelCell = new ExcelCell();
                    excelCell.Value = row[this.ColumnFields[i]].ToString();
                    excelCell.DataType = ExcelCellType.String;
                    excelRow.Cells.Add(excelCell);
                }

                DataRowEventArgs drEventArgs = new DataRowEventArgs();
                drEventArgs.ExcelRow = excelRow;
                drEventArgs.DataRow = row;

                //触发事件
                this.OnDataRowBind(this, drEventArgs);

                for (int i = 0; i < this.ColumnFields.Length; i++)
                {
                    if (((ExcelCell)excelRow.Cells[i]).DataType == ExcelCellType.String)
                    {
                        workSheet.Rows[rowIndex].Cells[i].Value = excelRow.Cells[i].Value;
                        workSheet.Rows[rowIndex].Cells[i].CellFormat.Alignment = HorizontalCellAlignment.Left;
                        workSheet.Rows[rowIndex].Cells[i].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                    }
                    else if (((ExcelCell)excelRow.Cells[i]).DataType == ExcelCellType.Int)
                    {
                        workSheet.Rows[rowIndex].Cells[i].Value = excelRow.Cells[i].Value;
                        workSheet.Rows[rowIndex].Cells[i].CellFormat.Alignment = HorizontalCellAlignment.Right;
                        workSheet.Rows[rowIndex].Cells[i].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                    }
                    else
                    {

                    }

                }

                rowIndex++;
            }
            #endregion

            workBook.Save(filePath);

            workSheet = null;
            workBook = null;
        }

        public void SaveAsTreeExcel(string filePath)
        {
            if (this.ColumnNames == null || this.ColumnNames.Length == 0)
            {
                throw new Exception("Please Set Property: ColumnsName!");
            }

            if (this.ColumnFields == null || this.ColumnFields.Length == 0)
            {
                throw new Exception("Please Set Property: ColumnFields!");
            }

            if (this.ColumnDetailNames == null || this.ColumnDetailNames.Length == 0)
            {
                throw new Exception("Please Set Property: ColumnDetailNames!");
            }

            if (this.ColumnDetailFields == null || this.ColumnDetailFields.Length == 0)
            {
                throw new Exception("Please Set Property: ColumnDetailFields!");
            }

            if (this.DataSourceForTreeExcel == null)
            {
                throw new Exception("Please Set Property: DataSource");
            }

            if (filePath.Equals(string.Empty))
            {
                throw new Exception("Please input file path");
            }

            Workbook workBook = new Workbook();
            Worksheet workSheet = null;

            workSheet = workBook.Worksheets.Add(this.SheetName);
            workSheet.DefaultColumnWidth = 3000;
            int rowIndex = 0;

            #region -- 生成表头 --
            for (int i = 0; i < this.ColumnNames.Length; i++)
            {
                workSheet.Rows[rowIndex].Cells[i].Value = this.ColumnNames[i];
                workSheet.Rows[rowIndex].Cells[i].CellFormat.Alignment = HorizontalCellAlignment.Center;
                workSheet.Rows[rowIndex].Cells[i].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;

            }
            #endregion

            rowIndex++;

            #region -- 生成数据行 --
            foreach (DataRow row in this.DataSourceForTreeExcel.Tables[0].Rows)
            {
                ExcelRow excelRow = new ExcelRow();
                excelRow.Cells = new List<ExcelCell>();

                for (int i = 0; i < this.ColumnFields.Length; i++)
                {
                    ExcelCell excelCell = new ExcelCell();
                    excelCell.Value = row[this.ColumnFields[i]].ToString();
                    excelCell.DataType = ExcelCellType.String;
                    excelRow.Cells.Add(excelCell);
                }

                DataRowEventArgs drEventArgs = new DataRowEventArgs();
                drEventArgs.ExcelRow = excelRow;
                drEventArgs.DataRow = row;

                //触发事件
                this.OnDataRowBind(this, drEventArgs);

                for (int i = 0; i < this.ColumnFields.Length; i++)
                {
                    if (((ExcelCell)excelRow.Cells[i]).DataType == ExcelCellType.String)
                    {
                        workSheet.Rows[rowIndex].Cells[i].Value = excelRow.Cells[i].Value;
                        workSheet.Rows[rowIndex].Cells[i].CellFormat.Alignment = HorizontalCellAlignment.Left;
                        workSheet.Rows[rowIndex].Cells[i].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                    }
                    else if (((ExcelCell)excelRow.Cells[i]).DataType == ExcelCellType.Int)
                    {
                        workSheet.Rows[rowIndex].Cells[i].Value = excelRow.Cells[i].Value;
                        workSheet.Rows[rowIndex].Cells[i].CellFormat.Alignment = HorizontalCellAlignment.Right;
                        workSheet.Rows[rowIndex].Cells[i].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                    }
                    else
                    {

                    }
                }


                rowIndex++;

                string selectSQL = "";
                for (int n = 0; n < RelationColumns.Length; n++)
                {
                    selectSQL += RelationColumns[n] + "= '" + row[RelationColumns[n]].ToString() + "' and ";
                }

                selectSQL = selectSQL.Substring(0, selectSQL.Length - 5);
                DataRow[] foundRow = this.DataSourceForTreeExcel.Tables[1].Select(selectSQL);

                if (foundRow.Length > 0)
                {
                    workSheet.Rows[rowIndex - 1].OutlineLevel = 0;
                    for (int j = 0; j < this.ColumnDetailNames.Length; j++)
                    {
                        workSheet.Rows[rowIndex].Cells[j].Value = this.ColumnDetailNames[j];
                        workSheet.Rows[rowIndex].Cells[j].CellFormat.Alignment = HorizontalCellAlignment.Center;
                        workSheet.Rows[rowIndex].Cells[j].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                        workSheet.Rows[rowIndex].OutlineLevel = 1;
                        workSheet.Rows[rowIndex].Hidden = true;
                    }

                    rowIndex++;

                    foreach (DataRow dataRow in foundRow)
                    {
                        ExcelRow excelChildRow = new ExcelRow();
                        excelChildRow.Cells = new List<ExcelCell>();

                        for (int i = 0; i < this.ColumnDetailFields.Length; i++)
                        {
                            if (i < RelationColumns.Length)
                            {
                                ExcelCell excelCell = new ExcelCell();
                                excelCell.Value = string.Empty;
                                excelCell.DataType = ExcelCellType.String;
                                excelChildRow.Cells.Add(excelCell);
                            }
                            else
                            {
                                ExcelCell excelCell = new ExcelCell();
                                excelCell.Value = dataRow[i].ToString();
                                excelCell.DataType = ExcelCellType.String;
                                excelChildRow.Cells.Add(excelCell);
                            }
                        }

                        DataRowEventArgs drChildEventArgs = new DataRowEventArgs();
                        drChildEventArgs.ExcelRow = excelChildRow;
                        drChildEventArgs.DataRow = dataRow;

                        //触发事件
                        this.OnDataRowBind(this, drChildEventArgs);

                        for (int i = 0; i < this.ColumnDetailFields.Length; i++)
                        {
                            if (((ExcelCell)excelRow.Cells[i]).DataType == ExcelCellType.String)
                            {
                                workSheet.Rows[rowIndex].Cells[i].Value = excelChildRow.Cells[i].Value;
                                workSheet.Rows[rowIndex].Cells[i].CellFormat.Alignment = HorizontalCellAlignment.Left;
                                workSheet.Rows[rowIndex].Cells[i].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                            }
                            else if (((ExcelCell)excelRow.Cells[i]).DataType == ExcelCellType.Int)
                            {
                                workSheet.Rows[rowIndex].Cells[i].Value = excelChildRow.Cells[i].Value;
                                workSheet.Rows[rowIndex].Cells[i].CellFormat.Alignment = HorizontalCellAlignment.Right;
                                workSheet.Rows[rowIndex].Cells[i].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;
                            }
                            else
                            {

                            }
                        }

                        workSheet.Rows[rowIndex].OutlineLevel = 1;
                        workSheet.Rows[rowIndex].Hidden = true;
                        rowIndex++;
                    }
                }
            }
            #endregion

            workBook.Save(filePath);

            workSheet = null;
            workBook = null;
        }

        #endregion
    }

    public class DataRowEventArgs : EventArgs
    {
        public ExcelRow ExcelRow { get; set; }
        public DataRow DataRow { get; set; }
    }

    public class ExcelRow
    {
        public IList<ExcelCell> Cells { get; set; }
    }

    public class ExcelCell
    {
        public string Value { get; set; }

        public ExcelCellType DataType { get; set; }
    }

    public enum ExcelCellType
    {
        String = 0,
        Int = 1
    }
}
