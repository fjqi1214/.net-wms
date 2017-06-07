using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(@".\1.xls", FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");

            IRow row0 = sheet.GetRow(0);
            ICell cel0 = row0.GetCell(9);
            Console.WriteLine(cel0.StringCellValue);

            IRow row1 = sheet.GetRow(1);
            ICell cell = row1.GetCell(9);
            Console.WriteLine(cell.StringCellValue);


            IRow row2 = sheet.GetRow(2);
            ICell cell2 = row2.GetCell(9);
            Console.WriteLine(cell2.StringCellValue);



            IRow row3 = sheet.GetRow(3);
            ICell cell3 = row3.GetCell(9);
            Console.WriteLine(cell3.StringCellValue);


            IRow row4 = sheet.GetRow(4);
            ICell cell4 = row3.GetCell(9);
            Console.WriteLine(cell4.StringCellValue);



            IRow row5 = sheet.GetRow(5);
            ICell cell5 = row3.GetCell(9);
            Console.WriteLine(cell5.StringCellValue);

            IRow row6 = sheet.GetRow(6);
            ICell cell6 = row3.GetCell(9);
            Console.WriteLine(cell6.StringCellValue);
        }
    }
}
