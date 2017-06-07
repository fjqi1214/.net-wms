using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain;


namespace BenQGuru.eMES.SATOPrint
{
    public partial class SATOPrinter
    {

        unsafe struct printer_defaults
        {
            public void* pdatatype; //   lptstr   
            public void* pdevmode;             //   lpdevmode   
            public uint desiredaccess;   //   access_mask   
        };


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PRINTER_INFO_2
        {
            public string pServerName;
            public string pPrinterName;
            public string pShareName;
            public string pPortName;
            public string pDriverName;
            public string pComment;
            public string pLocation;
            public IntPtr pDevMode;
            public string pSepFile;
            public string pPrintProcessor;
            public string pDatatype;
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public uint Attributes;
            public uint Priority;
            public uint DefaultPriority;
            public uint StartTime;
            public uint UntilTime;
            public uint Status;
            public uint cJobs;
            public uint AveragePPM;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct JOB_INFO_1
        {
            long JobId;
            string pPrinterName;
            string pMachineName;
            string pUserName;
            string pDocument;
            string pDatatype;
            string pStatus;
            int Status;
            int Priority;
            int Position;
            int TotalPages;
            int PagesPrinted;
            DateTime Submitted;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DOCINFO
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pDataType;
        }


        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int StartDocPrinter(IntPtr hPrinter, int Level, ref DOCINFO pDocInfo);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern long StartPagePrinter(IntPtr hPrinter);

        [DllImport("kernel32", SetLastError = true)]
        static extern int GetLastError();

        [DllImportAttribute("winspool.drv", SetLastError = true)]
        static extern unsafe bool OpenPrinter(string pprintername, int* phprinter, void* pdefault);

        [DllImport("winspool.drv", SetLastError = true)]
        static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool WritePrinter(IntPtr hPrinter, string pBuf, int cdBuf, ref int pcWritten);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern long EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern long EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern bool GetJob(IntPtr hPrinter, int JobID, int Level, out IntPtr pjob, int cbBuf, ref int pcbNeeded);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        static extern bool EnumJobs(IntPtr hprinter, int firstjob, int nojobs, int level, out IntPtr pjob, int cbbuf, out IntPtr pcbneeded, out IntPtr pcreturned);

        const uint printer_access_administer = 0x00000004;


        private static unsafe int openPrinter(string printerName)
        {
            bool bresult;
            int hprinter;
            printer_defaults pd;
            ////pd.pdatatype = null;
            ////pd.pdevmode = null;
            ////pd.desiredaccess = printer_access_administer;
            bresult = OpenPrinter(printerName, &hprinter, &pd);
            if (!bresult)
            {
                MessageBox.Show("can not open printer " + printerName + " " + GetLastError());
            }
            return hprinter;
        }

        //读模板打印方式
        public static void Print(string pinterName, System.Collections.ArrayList printValueList)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            //order 
            byte[] byteArray = new byte[] { (byte)27 };
            string Esc = asciiEncoding.GetString(byteArray);

            DOCINFO di = new DOCINFO();
            int pcWritten = 0;
            di.pDocName = "CT4i";
            di.pDataType = "RAW";

            //打开打印机
            string PrinterName = pinterName;
            int lhPrinter = openPrinter(PrinterName);

            StartDocPrinter((IntPtr)lhPrinter, 1, ref di);
            StartPagePrinter((IntPtr)lhPrinter);


            //SATO 打印机开始指令，必须有
            string printValueA = asciiEncoding.GetString(new byte[] { (byte)2 }) + Esc + "A";
            WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);

            //打印数据
            for (int i = 0; i < printValueList.Count; i++)
            {
                printValueA = printValueList[i].ToString();

                //printValueA = printValueA.Replace("\"", " ");
                //printValueA = printValueA.Replace("+", "");
                //printValueA = printValueA.Replace(" ", "");
                //printValueA = printValueA.Replace("Esc", Esc).Trim('"');

                WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);

            }
            //SATO打印结束指令
            printValueA = Esc + "Q1" + Esc + "Z" + asciiEncoding.GetString(new byte[] { (byte)3 });
            WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);
            EndPagePrinter((IntPtr)lhPrinter);
            EndDocPrinter((IntPtr)lhPrinter);
            ClosePrinter((IntPtr)lhPrinter);
        }

        //serialNumber1 和SerialNumber2 的打印方式
        public static void Print(string pinterName, System.Collections.ArrayList printValueList, string printType)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            //order 
            byte[] byteArray = new byte[] { (byte)27 };
            string Esc = asciiEncoding.GetString(byteArray);

            DOCINFO di = new DOCINFO();
            int pcWritten = 0;
            di.pDocName = "CT4i";
            di.pDataType = "RAW";

            //打开打印机
            string PrinterName = pinterName;
            int lhPrinter = openPrinter(PrinterName);
            string printValueA = string.Empty;
            int count = 0; //计算打印到了某一行第几个条码
            int lineCount = 0; //标记现在打印的是第几行
            StartDocPrinter((IntPtr)lhPrinter, 1, ref di);
            StartPagePrinter((IntPtr)lhPrinter);

            SystemSettingFacade _systemFacade = new SystemSettingFacade();
            Domain.BaseSetting.Parameter SerialNumber1 = (Domain.BaseSetting.Parameter)_systemFacade.GetParameter("SERIAL NUMBER1", "PRINTRELATE");
            Domain.BaseSetting.Parameter SerialNumber2 = (Domain.BaseSetting.Parameter)_systemFacade.GetParameter("SERIAL NUMBER2", "PRINTRELATE");

            //SATO 打印机开始指令
            if (SerialNumber1.ParameterCode != printType && SerialNumber2.ParameterCode != printType)
            {
                printValueA = asciiEncoding.GetString(new byte[] { (byte)2 }) + Esc + "A";
                WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);
            }
            //打印数据
            for (int i = 0; i < printValueList.Count; i++)
            {
                lineCount++;

                //防止\r\n被split掉
                if ((printValueList[i].ToString().IndexOf("#\r\n")) != -1)
                {
                    printValueA = printValueList[i].ToString().Substring(0, (printValueList[i].ToString().IndexOf("#\r\n")));
                }

                printValueA = printValueList[i].ToString().Replace("#", "   ");  //将字符串里面的#替换成3个空格

                WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);   //打一行
                if (lineCount == 2)
                {
                    lineCount = 0;
                    //一组条码打印好之后换到下一行
                    string NewLine = asciiEncoding.GetString(new byte[] { (byte)27 }) + asciiEncoding.GetString(new byte[] { (byte)65 }) + asciiEncoding.GetString(new byte[] { (byte)28 });
                    if (SerialNumber1.ParameterCode == printType || SerialNumber2.ParameterCode == printType)
                    {
                        NewLine = "\r\n";
                    }

                    WritePrinter((IntPtr)lhPrinter, NewLine, NewLine.Length, ref pcWritten);
                }
            }
            //SATO打印结束指令
            if (SerialNumber1.ParameterCode != printType && SerialNumber2.ParameterCode != printType)
            {
                printValueA = Esc + "Q1" + Esc + "Z" + asciiEncoding.GetString(new byte[] { (byte)3 });
                WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);
            }

            EndPagePrinter((IntPtr)lhPrinter);
            EndDocPrinter((IntPtr)lhPrinter);
            ClosePrinter((IntPtr)lhPrinter);
        }

        //2D Lable 打印方式
        public static void Print2DLabel(string pinterName, System.Collections.ArrayList printValueList)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            //order 
            byte[] byteArray = new byte[] { (byte)27 };
            string Esc = asciiEncoding.GetString(byteArray);

            DOCINFO di = new DOCINFO();
            int pcWritten = 0;
            di.pDocName = "CT4i";
            di.pDataType = "RAW";

            //打开打印机
            string PrinterName = pinterName;
            int lhPrinter = openPrinter(PrinterName);
            int lineCount = 0; //标记现在打印的是第几行
            string printValueA = string.Empty;

            //StartDocPrinter((IntPtr)lhPrinter, 1, ref di);
            //StartPagePrinter((IntPtr)lhPrinter);

            //打印数据,两行算一个条码，打完要SATO结束指令，传过来的值的行数一定是2的倍数
            for (int i = 0; i < printValueList.Count; i++)
            {
                lineCount++;
                if (lineCount == 1)
                {
                     StartDocPrinter((IntPtr)lhPrinter, 1, ref di);
                     StartPagePrinter((IntPtr)lhPrinter);
                     //SATO 打印机开始指令，必须有
                     printValueA = asciiEncoding.GetString(new byte[] { (byte)2 }) + Esc + "A";
                     WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);
                }
                printValueA = printValueList[i].ToString().Replace("\r\n" ,"");
                WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);

                if (lineCount == 2)
                {
                    lineCount = 0;
                    printValueA = Esc + "Q1" + Esc + "Z" + asciiEncoding.GetString(new byte[] { (byte)3 });
                    WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);
                    EndPagePrinter((IntPtr)lhPrinter);
                    EndDocPrinter((IntPtr)lhPrinter);
                    //ClosePrinter((IntPtr)lhPrinter);
                }
            }

            //SATO打印结束指令
            //printValueA = Esc + "Q1" + Esc + "Z" + asciiEncoding.GetString(new byte[] { (byte)3 });
            //WritePrinter((IntPtr)lhPrinter, printValueA, printValueA.Length, ref pcWritten);
            //EndPagePrinter((IntPtr)lhPrinter);
            //EndDocPrinter((IntPtr)lhPrinter);
            ClosePrinter((IntPtr)lhPrinter);
        }

        public static void Main()
        {
        }
    }
}