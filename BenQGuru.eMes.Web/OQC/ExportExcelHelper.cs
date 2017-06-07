using System;
using System.Collections.Generic;

using System.Web;
using System.IO;
using BenQGuru.eMES.Web.Helper;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Diagnostics;
using System.Reflection;

namespace BenQGuru.eMES.Web.OQC
{
    /// <summary> 
    /// Creat Date:20160315
    /// Autor:Jinger
    /// 导出Excel帮助类
    /// 解决：BS根据模板导出Excel,并且支持单元格插入图片
    /// </summary>
    public class ExportExcelHelper
    {
        #region Filed
        /// <summary>
        /// 当前页面对象
        /// </summary>
        private System.Web.UI.Page page = null;//当前页面对象

        /// <summary>
        /// Excle对象
        /// </summary>
        private Application app = null;//Excle对象

        /// <summary>
        /// 工作薄
        /// </summary>
        private _Workbook workbook = null;//工作薄

        /// <summary>
        /// 工作表
        /// </summary>
        private _Worksheet worksheet = null;//工作表

        /// <summary>
        /// 虚拟路径
        /// </summary>
        private string virtualHostRoot = "";//虚拟路径

        /// <summary>
        /// Excel存放路径
        /// </summary>
        private string path = "";//Excel存放路径

        /// <summary>
        /// Excel名称
        /// </summary>
        private string fileName = ""; //Excel名称

        /// <summary>
        /// 错误信息
        /// </summary>
        private string errorMsg = "";//错误信息 
        #endregion

        #region Property
        public string ErrorMsg
        {
            get { return errorMsg; }
        } 
        #endregion

        #region Init
        //初始化ExcelHelper
        /// <summary>
        /// 初始化ExcelHelper
        /// </summary>
        /// <param name="page">当前页面对象</param>
        /// <param name="virtualHostRoot">虚拟路径</param>
        /// <param name="path">Excel存放路径</param>
        /// <param name="fileName">Excel名称</param>
        public ExportExcelHelper(System.Web.UI.Page page, string virtualHostRoot, string path, string fileName)
        {
            this.page = page;
            this.virtualHostRoot = virtualHostRoot;
            this.path = path;
            this.fileName = fileName;
            this.GetWorkSheet();
        } 
        #endregion

        //模板指定单元格赋值
        /// <summary>
        /// 模板指定单元格赋值
        /// </summary>
        /// <param name="row">行号</param>
        /// <param name="column">列号</param>
        /// <param name="value">值</param>
        public void AddCellValue(int row,int column,object value)
        {
             worksheet.Cells[row, column] = value;
        }

        //模板指定单元格添加图片
        /// <summary>
        /// 模板指定单元格添加图片
        /// </summary>
        /// <param name="row">行号</param>
        /// <param name="column">列号</param>
        /// <param name="picPath">图片绝对路径</param>
        public void AddCellPicture(int row, int column, string picPath)
        {
            InsertPicture((Range)worksheet.Cells[row, column], worksheet, picPath);
        }


        //模板指定单元格添加图片
        /// <summary>
        /// 模板指定单元格添加图片
        /// </summary>
        /// <param name="bRow">开始行号</param>
        /// <param name="bColumn">开始列号</param>
        /// <param name="eRow">结束行号</param>
        /// <param name="eColumn">结束列号</param>
        /// <param name="picPath">图片绝对路径</param>
        public void AddCellPicture( int bRow, int bColumn, int eRow, int eColumn, string picPath)
        {
            InsertPicture(worksheet.Cells.get_Range(worksheet.Cells[bRow, bColumn], worksheet.Cells[eRow, eColumn]), worksheet, picPath);
        }

        //根据模板导出Excle
        /// <summary>
        /// 根据模板导出Excle
        /// </summary>
        public void ExportExcel()
        {
            if (string.IsNullOrEmpty(errorMsg))
            {
                string newFileName = fileName.Substring(0, fileName.IndexOf(".xlsx")) + "_new.xlsx";
                string newPath = page.Server.MapPath(string.Format(@"{0}{1}", path, newFileName));
                if (File.Exists(newPath))
                {
                    File.Delete(newPath);//如果文件存在删除
                }
                workbook.SaveAs(newPath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                app.DisplayAlerts = false;
                workbook.Close(Missing.Value, Missing.Value, Missing.Value);
                app.Quit();
                KillExcel(app);
                //下载
                DownLoadFile(page, virtualHostRoot, path, newFileName);
                return;
            }
            app.DisplayAlerts = false;
            workbook.Close(Missing.Value, Missing.Value, Missing.Value);
            app.Quit();
            KillExcel(app);
        }

        #region HelpMethod

        //获取模板第一个工作表
        /// <summary>
        /// 获取模板第一个工作表
        /// </summary>
        private void GetWorkSheet()
        {
            try
            {
                app = new Microsoft.Office.Interop.Excel.Application();
            }
            catch (Exception ex)
            {
                //提示:服务器上缺少Excel组件，需要安装Office软件
                errorMsg = "服务器上缺少Excel组件：" + ex.Message;
                return;
            }
            app.Visible = false;
            app.UserControl = true;
            Workbooks workbooks = app.Workbooks;
            workbook = workbooks.Add(page.Server.MapPath(string.Format(@"{0}{1}", path, fileName))); //加载模板
            Sheets sheets = workbook.Sheets;
            //第一个工作薄。
            worksheet = (_Worksheet)sheets.get_Item(1);
            if (worksheet == null)
            {
                //提示:工作薄中没有工作表
                errorMsg = "工作薄中没有工作表";
            }
        }

        //调用底层函数获取进程标示 ,杀掉excle进程释放内存
        [DllImport("User32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);
        private static void KillExcel(Application theApp)
        {
            var intptr = new IntPtr(theApp.Hwnd);
            try
            {
                int id;
                GetWindowThreadProcessId(intptr, out id);
                Process p = Process.GetProcessById(id);
                if (p != null)
                {
                    p.Kill();
                    p.Dispose();
                }
            }
            catch (Exception)
            {
            }
        }

        //下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filename">文件名称</param>
        private void DownLoadFile(System.Web.UI.Page page, string virtualHostRoot, string downloadPath, string fileName)
        {
            string strSript = @" var frameDown =$('<a></a>');
            frameDown.appendTo($('form'));
            //frameDown.attr('target','_blank');
            frameDown.html('<span></span>');
            frameDown.attr('href', '"
            + string.Format(@"{0}FDownload.aspx", virtualHostRoot)
            + "?fileName=" + string.Format(@"{0}{1}", downloadPath, fileName)
            + @"');
            frameDown.children().click();
            frameDown.remove();";
            page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), strSript, true);
            ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), strSript, true);
        }

        //将图片插入到指定的单元格位置
        /// <summary>
        /// 将图片插入到指定的单元格位置，并设置图片的宽度和高度。
        /// 注意：图片必须是绝对物理路径
        /// </summary>
        /// <param name="rng">Excel单元格选中的区域</param>
        /// <param name="PicturePath">要插入图片的绝对路径。</param>
        private void InsertPicture(Range rng, _Worksheet sheet, string PicturePath)
        {
            rng.Select();
            float PicLeft, PicTop, PicWidth, PicHeight;
            try
            {
                PicLeft = Convert.ToSingle(rng.Left);
                PicTop = Convert.ToSingle(rng.Top);
                PicWidth = Convert.ToSingle(rng.Width);
                PicHeight = Convert.ToSingle(rng.Height);

                sheet.Shapes.AddPicture(PicturePath, //图片路径
                                        Microsoft.Office.Core.MsoTriState.msoFalse,//是否链接到文件
                                        Microsoft.Office.Core.MsoTriState.msoTrue,//图片插入时是否随文档一起保存
                                        PicLeft, PicTop,//图片在文档中的坐标位置 坐标
                                        PicWidth, PicHeight//图片显示的宽度和高度
                                        );

            }
            catch (Exception ex)
            {
                errorMsg = "插入图片错误：" + ex.Message;
            }
        } 
        #endregion
    }
}