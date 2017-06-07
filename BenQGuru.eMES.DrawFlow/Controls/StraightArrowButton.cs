using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using BenQGuru.eMES.DrawFlow.Data;
using System.Threading;

namespace BenQGuru.eMES.DrawFlow.Controls
{
    /// <summary>
    /// 直线箭头类
    /// </summary>
    public partial class StraightArrowButton : FlowButton
    {
        /// <summary>
        /// </summary>
        public override void DrawButton()
        {
            DrawUtility.DrawOneArrow(this, 1,degree);
            SetImage();
        }

        private void SetImage()
        {
            this.Image = DrawUtility.GenerateGifByDegree(degree);
           
        }
        public StraightArrowButton()
        {
            //this.BackColor = SystemColors.InactiveCaption;
            this.degree = 0;
        }

        public StraightArrowButton(Color backColor, double degree, FunctionButton btn)
        {
            //this.BackColor = backColor;
            this.degree = degree;
            this.FromProcesses.Add(btn);
        }
 
        private double degree;

        /// <summary>
        /// 用来表示箭头方向，值为水平向右顺时针旋转的度数
        /// </summary>
        public double Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        public void ResetDegree(double degree)
        {
            this.degree = degree;
        }

        

        /// <summary>
        /// 箭头头部坐标
        /// </summary>
        public Point ArrowHead;

        /// <summary>
        /// 箭头尾部坐标
        /// </summary>
        public Point ArrowTail;
        /// <summary>
        /// </summary>
        protected override void CreateContextMenu()
        {
            if (ContextMenu == null)
                ContextMenu = new ContextMenu();

            MenuItem miProcess = new MenuItem();
            miProcess.Text = "创建进程";
            miProcess.Click += new EventHandler(miProcess_Click);
            ContextMenu.MenuItems.Add(miProcess);

            MenuItem miEnd = new MenuItem();
            miEnd.Text = "创建结束符";
            miEnd.Click += new EventHandler(miEnd_Click);
            ContextMenu.MenuItems.Add(miEnd);

        }

        /// <summary>
        /// </summary>
        protected override void SetLinkArea(RectangleCollection linkAreas)
        {
            int x = this.Left + this.Width - 4;
            int y = this.Top;
            int width = this.Width;
            int height = this.Height;
            Rectangle rect = new Rectangle(x, y, width, height);
            linkAreas.Add(rect);
        }

        /// <summary>
        /// </summary>
        private void miProcess_Click(object sender, EventArgs e)
        {
            AddOutProcess(Guid.NewGuid().ToString());
        }

        public override FunctionButton AddOutProcess(string processName)
        {
            //如果已经有的不要增加了。
            if (ToProcesses.Count > 0)
            {
                MessageBox.Show("目标进程已经存在");
                return null;
            }
            else
            {
                //在右边增加一个新的进程,SystemColors.Control
                ProcessButton pb = new ProcessButton(SystemColors.Control, this.degree, this.ArrowHead);
                pb.ProcessName = processName;         
                this.Parent.Controls.Add(pb);
                pb.DrawButton();

                pb.InFlows.Add(this);
                this.ToProcesses.Add(pb);
                return pb;
            }

        }
        /// <summary>
        /// </summary>
        private void miEnd_Click(object sender, EventArgs e)
        {
            AddEnd();
        }

        public override EndButton AddEnd()
        {
            if (ToProcesses.Count > 0)
            {
                MessageBox.Show("目标进程已经存在");
                return null;
            }

            EndButton eb = new EndButton(Color.Green, this.degree, this.ArrowHead);
            eb.ProcessName = "结束";
            this.Parent.Controls.Add(eb);
            eb.DrawButton();
            eb.InFlows.Add(this);
            this.ToProcesses.Add(eb);
            return eb;
        }

        //private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        //private int offset = 0;
        //private int speed = 10;
        //private int picIndex = 1;
        //void timer1_Tick(object sender, EventArgs e)
        //{

        //    //Image backImage = DrawUtility.Rotate(Image.FromFile("ArrowBack.jpg"), degree);
        //    //TextureBrush tb = new TextureBrush(backImage); 
        //    //offset = offset % backImage.Width;
        //    //tb.TranslateTransform(offset * float.Parse((Math.Cos(degree * Math.PI / 180)).ToString()),
        //    //                      offset * float.Parse((Math.Sin(degree * Math.PI / 180)).ToString()));
        //    //offset+=speed;

        //    //using (Graphics g = Graphics.FromImage(backImage))
        //    //{
        //    //    g.FillRectangle(tb, 0, 0, backImage.Width, backImage.Height); // 這行指令其實已經變更了原圖

        //    //    this.BackgroundImage = backImage;
        //    //    this.BackgroundImageLayout = ImageLayout.Stretch;
        //    //    g.Dispose();

        //    //}
        //    string picName = string.Format("ArrowBack{0}.jpg", (picIndex % 4).ToString());
        //    this.BackgroundImage = DrawUtility.Rotate(Image.FromFile(picName), degree);
        //    picIndex = (picIndex+1)%4;
        //}
    }
}
