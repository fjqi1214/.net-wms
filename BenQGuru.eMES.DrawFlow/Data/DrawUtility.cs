/***********************************************************************
 * Module:  DrawUtility.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Data.DrawUtility
 ***********************************************************************/

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BenQGuru.eMES.DrawFlow.Controls;
using System.IO;

namespace BenQGuru.eMES.DrawFlow.Data
{
    /// <summary>
    /// </summary>
    public sealed class DrawUtility
    {
        /// <summary>
        /// </summary>
        public static void DrawJumpArrow(object sender, PaintEventArgs e, int length, bool isBack)
        {
            Control control1 = sender as Control;
            if (control1 != null)
            {
                GraphicsPath path1 = new GraphicsPath();
                Point[] result;
                if (isBack)
                {
                    result = GetBackJumpPoints(length);
                    control1.BackColor = SystemColors.InactiveBorder;
                }
                else
                {
                    result = GetJumpPoints(length);
                    control1.BackColor = SystemColors.InactiveCaption;
                }
                path1.AddPolygon(result);

                Point[] minMaxPoints = GetMinMaxPoint(result);
                control1.Size = new Size(minMaxPoints[1].X - minMaxPoints[0].X, minMaxPoints[1].Y - minMaxPoints[0].Y);
                control1.Region = new Region(path1);
            }
        }

        /// <summary>
        /// </summary>
        public static void DrawStart(StartButton btnStart)
        {
            if (btnStart != null)
            {
                using (GraphicsPath path1 = new GraphicsPath())
                {
                    path1.AddEllipse(0, 0, btnStart.Radius * 2, btnStart.Radius * 2);
                    btnStart.Region = new Region(path1);
                }

                btnStart.Size = new Size(btnStart.Radius * 2, btnStart.Radius * 2);
                
            }
        }

        /// <summary>
        /// </summary>
        public static void DrawEnd(EndButton btnEnd)
        {
            if (btnEnd != null)
            {
                using (GraphicsPath path1 = new GraphicsPath())
                {
                    path1.AddEllipse(0, 0, btnEnd.Radius * 2, btnEnd.Radius * 2);
                   // path1.AddEllipse(btnEnd.Radius / 2, btnEnd.Radius / 2, btnEnd.Radius, btnEnd.Radius);
                    btnEnd.Region = new Region(path1);
                }
                btnEnd.Size = new Size(btnEnd.Radius * 2, btnEnd.Radius * 2);         

                Point arrowPoint = DrawUtility.GetCyclePoint(100, 0, btnEnd.InArrowDegree);
                btnEnd.Left = btnEnd.InPoint.X + arrowPoint.X * btnEnd.Radius / 100 - btnEnd.Radius;
                btnEnd.Top = btnEnd.InPoint.Y + arrowPoint.Y * btnEnd.Radius / 100 - btnEnd.Radius;
            }
        }

        /// <summary>
        /// </summary>
        public static void DrawProcess(ProcessButton btnProcess)
        {
            if (btnProcess != null)
            {
                GraphicsPath path1 = new GraphicsPath();
              
                    //path1.AddRectangle(DrawUtility.GetNormalRecangle(0, 0));
                    //control1.BackColor = SystemColors.Control;
                    path1 =  CreateRoundedRectanglePath(DrawUtility.GetNormalRecangle(0, 0), a*2);

                    btnProcess.Size = new Size(Convert.ToInt16((ah + rw - 5)*1.2 * a),Convert.ToInt16(8*1.2 * a));
                    btnProcess.Region = new Region(path1);
              
                double degree = (btnProcess.InArrowDegree % 360 + 360) % 360;
                if (degree <= 45 || degree > 315)
                {
                    //接触点为矩形按钮的左侧中点
                    btnProcess.Left = btnProcess.InPoint.X;
                    btnProcess.Top = btnProcess.InPoint.Y - btnProcess.Height / 2;
                }
                else if (degree > 45 && degree <= 135)
                {
                    //接触点为矩形按钮的上侧中点
                    btnProcess.Left = btnProcess.InPoint.X - btnProcess.Width / 2;
                    btnProcess.Top = btnProcess.InPoint.Y;
                }
                else if (degree > 135 && degree <= 225)
                {
                    //接触点为矩形按钮的右侧中点
                    btnProcess.Left = btnProcess.InPoint.X - btnProcess.Width;
                    btnProcess.Top = btnProcess.InPoint.Y - btnProcess.Height / 2;
                }
                else if (degree > 225 && degree <= 315)
                {
                    //接触点为矩形按钮的下侧中点
                    btnProcess.Left = btnProcess.InPoint.X - btnProcess.Width / 2;
                    btnProcess.Top = btnProcess.InPoint.Y - btnProcess.Height;
                }
            }
        }

        #region 绘制圆角矩形区域
        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        #endregion




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">箭头控件本体</param>
        /// <param name="lengthRat">长度</param>
        /// <param name="degree">相对水平向右顺时针旋转角度</param>
        /// <param name="tail">箭头尾部实际左边</param>
        /// <returns></returns>
        public static void DrawOneArrow(StraightArrowButton arrow, double lengthRat, double degree)
        {
            if (arrow != null&&arrow.Parent != null&&arrow.FromProcesses.Count==1)
            {
                GraphicsPath path1 = new GraphicsPath();
                Point[] result;
                Point[] minMaxPoints;
                int left, top, width, height;
                Point tail;
                double direction;

                while (true)
                {
                    direction = (degree % 360 + 360) % 360;
                    result = GetOneArrowPoints(lengthRat, degree);
                    minMaxPoints = GetMinMaxPoint(result);
                    tail = arrow.FromProcesses[0].GetNextArrowTail(degree);

                    left=tail.X+(minMaxPoints[0].X-(result[0].X+result[6].X)/2);//矩形区域左边缘
                    top=tail.Y+(minMaxPoints[0].Y-(result[0].Y+result[6].Y)/2);//矩形区域上边缘
                    width=minMaxPoints[1].X - minMaxPoints[0].X;//矩形区域宽度
                    height= minMaxPoints[1].Y - minMaxPoints[0].Y;//矩形区域高度

                    if (direction>180&&(top - arrow.Parent.Top) < 8 * a)
                    {
                        //箭头上边缘过界，顺时针转动90度
                        degree += 90;
                        continue;
                    }
                    if ((direction<90||direction>270)&&((left + width) + (ah + rw - 5) * a > arrow.Parent.Right))
                    {
                        //箭头右边缘过界，顺时针转动90度
                        degree += 90;
                        continue;
                    }
                    if ((direction >90 && direction < 270) && (left - arrow.Parent.Left) < (ah + rw - 5) * a)
                    {
                        //箭头左边缘过界，逆时针转动90度
                        degree -= 90;
                        continue;
                    }
                    break;
                }

                path1.AddPolygon(result);
                arrow.Size = new Size(width, height);
                arrow.Region = new Region(path1);
                arrow.Left = left;
                arrow.Top = top;
                arrow.ResetDegree(degree);
                arrow.ArrowTail = tail;

                //箭头头部坐标
                Point arrowHead=new Point (tail.X+(result [3].X-(result[0].X+result[6].X)/2),
                                           tail.Y + (result[3].Y - (result[0].Y + result[6].Y) / 2));
                arrow.ArrowHead = arrowHead;
                
            }
   
        }

        /// <summary>
        /// </summary>
        public static void DrawTwoArrow(object sender, double lengthRat, double degree, bool isDown)
        {
            Control control1 = sender as Control;
            if (control1 != null)
            {
                GraphicsPath path1 = new GraphicsPath();
                Point[] result = GetTwoArrowPoints(lengthRat, degree, isDown);
                path1.AddPolygon(result);
                Point[] minMaxPoints = GetMinMaxPoint(result);
                control1.BackColor = Color.HotPink;
                control1.Size = new Size(minMaxPoints[1].X - minMaxPoints[0].X, minMaxPoints[1].Y - minMaxPoints[0].Y);
                control1.Region = new Region(path1);
            }
        }

        /// <summary>
        /// </summary>
        public static Point GetCyclePoint(int x, int y, double degree)
        {
            double r = Math.Sqrt(x * x + y * y);
            //转换后的角度
            degree = Math.Atan(y * 0.1 / (x * 0.1)) + degree*Math.PI/180;

            return new Point((int)(r * Math.Cos(degree)), (int)(r * Math.Sin(degree)));
        }

        /// <summary>
        /// </summary>
        private static Point[] GetBackJumpPoints(int length)
        {
            int height = 15;
            Point p1 = new Point(a, 0);
            Point p2 = new Point(-a, 0);
            Point p3 = new Point(-a, -height * a);
            Point p4 = new Point(a - length, -height * a);
            Point p5 = new Point(a - length, -ah * a);
            Point p6 = new Point(2 * a - length, -ah * a);
            Point p7 = new Point(-length, 0);
            Point p8 = new Point(-length - 2 * a, -ah * a);
            Point p9 = new Point(-length - a, -ah * a);
            Point p10 = new Point(-length - a, -(height + 2) * a);
            Point p11 = new Point(a, -(height + 2) * a);

            Point[] temp = new Point[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11 };
            Point[] result = new Point[temp.Length];

            int i = 0;
            foreach (Point p in temp)
            {
                Point pr = new Point(p.X + length + 2 * a, p.Y + (height + 2) * a);
                result[i] = pr;
                i++;
            }
            return result;
        }

        /// <summary>
        /// </summary>
        private static Point[] GetJumpPoints(int length)
        {
            int height = 10;
            Point p1 = new Point(-a, 0);
            Point p2 = new Point(a, 0);
            Point p3 = new Point(a, -height * a);
            Point p4 = new Point(length - a, -height * a);
            Point p5 = new Point(length - a, -ah * a);
            Point p6 = new Point(length - 2 * a, -ah * a);
            Point p7 = new Point(length, 0);
            Point p8 = new Point(length + 2 * a, -ah * a);
            Point p9 = new Point(length + a, -ah * a);
            Point p10 = new Point(length + a, -(height + 2) * a);
            Point p11 = new Point(-a, -(height + 2) * a);

            Point[] temp = new Point[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11 };
            Point[] result = new Point[temp.Length];

            int i = 0;
            foreach (Point p in temp)
            {
                Point pr = new Point(p.X + a, p.Y + (height + 2) * a);
                result[i] = pr;
                i++;
            }
            return result;
        }

        /// <summary>
        /// </summary>
        private static Rectangle GetNormalRecangle(int x, int y)
        {
            return new Rectangle(x, y,Convert.ToInt16((rw + ah - 5)*1.2 * a),Convert.ToInt16( 8 *1.2* a));
        }

        /// <summary>
        /// </summary>
        private static Point[] GetMinMaxPoint(Point[] allPoints)
        {
            int minx = 10000;
            int miny = 10000;
            int maxx = -10000;
            int maxy = -10000;
            foreach (Point p in allPoints)
            {
                if (minx > p.X) minx = p.X;
                if (miny > p.Y) miny = p.Y;

                if (maxx < p.X) maxx = p.X;
                if (maxy < p.Y) maxy = p.Y;
            }
            return new Point[] { new Point(minx, miny), new Point(maxx, maxy) };
        }

        /// <summary>
        /// 获取向右的单向箭头的7个端点的坐标
        /// </summary>
        /// <param name="lengthRat"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        private static Point[] GetOneArrowPoints(double lengthRat, double degree)
        {
            int rw1 = (int)(rw * lengthRat);
            int ah1 = (int)(ah * lengthRat);

            if ((degree%180) == 0)
            {			
                rw1 /= 2;
            }
            else if ((degree % 90) == 0)
            {
                rw1 /= 3;
            }

            Point p1 = new Point(0, a);
            Point p2 = new Point(rw1 * a, a);
            Point p3 = new Point(rw1 * a, a * 2);
            Point p4 = new Point((ah1 + rw1) * a, 0);
            Point p5 = new Point(rw1 * a, -a * 2);
            Point p6 = new Point(rw1 * a, -a);
            Point p7 = new Point(0, -a);
            Point[] ps = new Point[] { p1, p2, p3, p4, p5, p6, p7 };

            Point[] result = new Point[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                Point temp = GetCyclePoint(ps[i].X, ps[i].Y, degree);
                result[i] = temp;
            }
            Point[] minMaxPoints = GetMinMaxPoint(result);
            for (int i = 0; i < result.Length; i++)
            {
                Point temp = new Point(result[i].X - minMaxPoints[0].X, result[i].Y - minMaxPoints[0].Y);
                result[i] = temp;
            }
            return result;
        }

        /// <summary>
        /// </summary>
        private static Point[] GetTwoArrowPoints(double lengthRat, double degree, bool isDown)
        {
            int rw1 = (int)(rw * lengthRat);
            int ah1 = (int)(ah * lengthRat);
            if (isDown)
            {
                rw1 = (int)(rw1 * 2.5);
            }
            Point p0 = new Point(1, 0);
            Point p1 = new Point(ah1 * a, 2 * a);
            Point p2 = new Point(ah1 * a, a);
            Point p3 = new Point((ah1 + rw1) * a, a);
            Point p4 = new Point((ah1 + rw1) * a, 2 * a);
            Point p5 = new Point((ah1 * 2 + rw1) * a, 0);
            Point p6 = new Point((ah1 + rw1) * a, -2 * a);
            Point p7 = new Point((ah1 + rw1) * a, -a);
            Point p8 = new Point(ah1 * a, -a);
            Point p9 = new Point(ah1 * a, -2 * a);

            Point[] ps = new Point[] { p0, p1, p2, p3, p4, p5, p6, p7, p8, p9 };

            Point[] result = new Point[ps.Length];
            for (int i = 0; i < ps.Length; i++)
            {
                Point temp = GetCyclePoint(ps[i].X, ps[i].Y, degree);
                result[i] = temp;
            }
            Point[] minMaxPoints = GetMinMaxPoint(result);
            for (int i = 0; i < result.Length; i++)
            {
                Point temp = new Point(result[i].X - minMaxPoints[0].X, result[i].Y - minMaxPoints[0].Y);
                result[i] = temp;
            }
            return result;
        }

        /// 基数
        public const int a = 3;
        /// 矩形宽度比例
        public const int rw = 28;
        /// 矩形高度比例
        public const int rh = 4;
        /// 箭头高度比例
        public const int ah = 9;

        #region 图片处理
        #region 图片旋转函数
        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        public static Image Rotate(Image b, double angle)
        {
            angle = angle % 360;
            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(float.Parse(angle.ToString()));
            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);
            //重至绘图的所有变换
            g.ResetTransform();
            g.Save();
            g.Dispose();
            //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return dsImage;
        }
        #endregion 图片旋转函数

        /// <summary>
        /// Jpg图片路径
        /// </summary>
        public readonly static string JpgPath = @"Pic\Jpg\";
        /// <summary>
        /// Gif图片路径
        /// </summary>
        public readonly static string GifPath = @"Pic\Gif\";
        /// <summary>
        /// Gif生产时的各个帧之间的时间间隔,ms
        /// </summary>
        private static int GifInterval = 300;

        public static Image GenerateGifByDegree(Double degree)
        {
            string strDegree = Convert.ToInt32((degree % 360 + 360) % 360).ToString();
            if (!Directory.Exists(GifPath))
            {
                Directory.CreateDirectory(GifPath);
            }
            
            String outputFilePath = GifPath+"ArrowBack" + strDegree + ".gif";
            if (!File.Exists(outputFilePath))
            {
                String[] imageFilePaths = new String[] { JpgPath+"ArrowBack0.JPG", 
                                                     JpgPath+"ArrowBack1.JPG", 
                                                     JpgPath+"ArrowBack2.JPG", 
                                                     JpgPath+"ArrowBack3.JPG" };

                AnimatedGifEncoder gifEncoder = new AnimatedGifEncoder();
                gifEncoder.Start(outputFilePath);
                gifEncoder.SetDelay(GifInterval);
                //-1:no repeat,0:always repeat
                gifEncoder.SetRepeat(0);
                for (int i = 0, count = imageFilePaths.Length; i < count; i++)
                {
                    gifEncoder.AddFrame(DrawUtility.Rotate(Image.FromFile(imageFilePaths[i]), degree));
                }
                gifEncoder.Finish();
            }

            return Image.FromFile(outputFilePath);
        }
        #endregion

    }
}