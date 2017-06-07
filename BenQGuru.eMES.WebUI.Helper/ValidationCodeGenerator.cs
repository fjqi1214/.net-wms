using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Drawing;

namespace BenQuru.eMES.WebUI.Helper
{
    public class ValidationCodeGenerator : IHttpHandler, IRequiresSessionState
    {

        /// <summary>
        /// HttpHandler成员方法
        /// </summary>  
        public void ProcessRequest(HttpContext ctx)
        {
            string imageCodeKey = "";
            byte[] data = GenerateVerifyImage(4, ref imageCodeKey, true, false);
            ctx.Session.Add(SessionConstants.IMAGE_CODE, imageCodeKey);
            ctx.Response.OutputStream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// HttpHandler成员属性
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 生成图片验证码
        /// </summary>
        /// <param name="nLen">验证码的长度</param>
        /// <param name="strKey">输出参数，验证码的内容</param>
        /// <param name="PureBlackBackGround">是否黑白色</param>
        /// <returns>图片字节流</returns>       
        private byte[] GenerateVerifyImage(int keyLength,
                                            ref string imageCodeKey,
                                            bool pureBlackBackGround,
                                            bool drawDisturbLine)
        {
            int bmpWidth = 26 * keyLength + 8;
            int bmpHeight = 20;
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(bmpWidth, bmpHeight);

            // 1. 生成随机背景颜色
            int red, green, blue;  // 背景的三元色
            System.Random rd = new Random((int)System.DateTime.Now.Ticks);
            if (!pureBlackBackGround)
            {

                red = rd.Next(255) % 128 + 128;
                green = rd.Next(255) % 128 + 128;
                blue = rd.Next(255) % 128 + 128;
            }
            else
            {
                red = 255;
                green = 255;
                blue = 255;
            }

            // 2. 填充位图背景
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(bmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(red, green, blue))
             , 0
             , 0
             , bmpWidth
             , bmpHeight);


            // 3. 绘制干扰线条，采用比背景略深一些的颜色
            if (drawDisturbLine)
            {
                int lines = 3;
                System.Drawing.Pen pen;
                if (!pureBlackBackGround)
                {
                    pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(red - 17, green - 17, blue - 17), 1);
                }
                else
                {
                    pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(12, 12, 12), 1);
                }
                for (int a = 0; a < lines; a++)
                {
                    int x1 = rd.Next() % bmpWidth;
                    int y1 = rd.Next() % bmpHeight;
                    int x2 = rd.Next() % bmpWidth;
                    int y2 = rd.Next() % bmpHeight;
                    graph.DrawLine(pen, x1, y1, x2, y2);
                }
            }

            // 采用的字符集，可以随即拓展，并可以控制字符出现的几率
            string codeScop = "ABCDEFGHJKLMNPRSTUVWXYZ23456789";

            // 4. 循环取得字符，并绘制
            string result = "";
            for (int i = 0; i < keyLength; i++)
            {
                int coordinateX = (i * 26 + rd.Next(3));
                int coordinateY = rd.Next(2) + 1;

                // 确定字体
                System.Drawing.Font font = new System.Drawing.Font("Courier New",
                 13 + rd.Next() % 4,
                 System.Drawing.FontStyle.Bold);
                char currentChar = codeScop[rd.Next(codeScop.Length)];  // 随机获取字符
                result += currentChar.ToString();

                // 绘制字符
                if (!pureBlackBackGround)
                {
                    graph.DrawString(currentChar.ToString(),
                     font,
                     new SolidBrush(System.Drawing.Color.FromArgb(red - 60 + coordinateY * 3,
                     green - 60 + coordinateY * 3,
                     blue - 40 + coordinateY * 3)),
                     coordinateX,
                     coordinateY);
                }
                else
                {
                    graph.DrawString(currentChar.ToString(),
                    font,
                    new SolidBrush(System.Drawing.Color.FromArgb(0, 0, 0)),
                    coordinateX,
                    coordinateY);
                }
            }

            // 5. 输出字节流
            System.IO.MemoryStream bstream = new System.IO.MemoryStream();
            //bmp = TwistImage(bmp, true, 2.0, 2.0);
            bmp.Save(bstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            bmp.Dispose();
            graph.Dispose();

            imageCodeKey = result;
            byte[] byteReturn = bstream.ToArray();
            bstream.Close();

            return byteReturn;
        }

        private const double PI = 3.1415926535897932384626433832795;
        private const double DOUBLE_PI = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">源图片</param>
        /// <param name="bXDir">是否横向</param>
        /// <param name="nMultValue">波形的幅度倍数</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (DOUBLE_PI * (double)j) / dBaseAxisLen : (DOUBLE_PI * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }
    }
    public sealed class SessionConstants
    {
        public const string IMAGE_CODE = "ImageCode";
    }
}
