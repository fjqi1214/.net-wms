using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.DrawFlow.Data;
using System.Collections;

namespace BenQGuru.eMES.DrawFlow.Controls
{
    /// <summary>
    /// 功能进程按钮
    /// </summary>
    public abstract class FunctionButton : Button, IProcessInfo
    {
        //public delegate void ChangeGridData(SimulationReport sim);
        //public event ChangeGridData cgd;//委托事件       
      
        /// <summary>
        /// </summary>
        public FunctionButton()
        {
            fProcessID = Guid.NewGuid().ToString();
            this.Click += new EventHandler(FunctionButton_Click);
            //this.Paint += new PaintEventHandler(FunctionButton_Paint);
            Cursor = Cursors.Hand;
            FlatStyle = FlatStyle.Flat;
            //屏蔽右击菜单
            //CreateContextMenu();
            //AddGeneralMenu();
            isInspectStyle = false;
            this.FlatAppearance.BorderSize = 0;
            
        }
        /// <summary>
        /// </summary>
        public override string ToString()
        {
            return ProcessName;
        }

        /// <summary>
        /// </summary>
        public void DeleteLinks()
        {
            //清除进入的流程的引用，发出的流程的引用
            //各个方向上的引用也要清除,同时也要从图形上面去掉,后面再让他们重新建立

            foreach (FlowButton fb in InFlows)
            {
                fb.ToProcesses.RemoveObject(this);
                fb.Parent.Controls.Remove(fb);
            }
            foreach (FlowButton fb in OutFlows)
            {
                fb.FromProcesses.RemoveObject(this);
                fb.Parent.Controls.Remove(fb);
            }
        }

        /// <summary>
        /// </summary>
        protected abstract void CreateContextMenu();

        /// <summary>
        /// </summary>
        public abstract void DrawButton();

        public virtual FlowButton AddOutArrow(double degree)
        {
            return null;
        }

        /// <summary>
        /// 获取指出箭头的接触点，即箭头的尾部坐标
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public virtual Point GetNextArrowTail(double degree)
        {
            return new Point();
        }

        /// <summary>
        /// </summary>
        private void FunctionButton_Click(object sender, EventArgs e)
        {
            //FCollectionViewFlow fviewflow = (FCollectionViewFlow)this.FindForm();       
            //this.cgd += new ChangeGridData(fviewflow.initGirdData);        
            //cgd(this.SimReObj);          
        }
      
        /// <summary>
        /// </summary>
        private void CheckOneName(string value)
        {
            //检查唯一性
            if (this.Parent != null)
            {
                foreach (Control ctrl in this.Parent.Controls)
                {
                    if (ctrl is ProcessButton)
                    {
                        if ((ctrl as ProcessButton).ProcessName == value)
                        {
                            throw new Exception("选中的进程名称已经存在！");
                        }
                    }
                }
            }
        }

        // <summary>
        // </summary>
        //private void FunctionButton_Paint(object sender, PaintEventArgs e)
        //{
        //    DrawButton();
        //    this.Paint -= new PaintEventHandler(FunctionButton_Paint);
        //}

        /// <summary>
        /// </summary>
        private void AddGeneralMenu()
        {
            if (ContextMenu == null)
                ContextMenu = new ContextMenu();

            if (this.GetType() != typeof(StartButton))
            {
                //删除
                MenuItem miDelete = new MenuItem();
                miDelete.Text = "删除该进程";
                miDelete.Click += new EventHandler(miDelete_Click);
                ContextMenu.MenuItems.Add(miDelete);
            }
        }

        /// <summary>
        /// 调整控件的位置
        /// </summary>
        private void MoveRightControls(FunctionButton deleteCtrl)
        {
            //是否为维修
            bool isRepairNode = false;
            foreach (FlowButton fb in this.InFlows)
            {
                if (fb is DownArrowButton)
                {
                    isRepairNode = true;
                    break;
                }
            }
            if (isRepairNode)
            {
                deleteCtrl.Parent.Controls.Remove(deleteCtrl);
                return;
            }

            (this.Parent as Panel).AutoScrollPosition = new Point(0, 0);
            int flowWidth = 0;
            foreach (Control ctrl in this.Parent.Controls)
            {
                if (ctrl is RightArrowButton)
                {
                    flowWidth = ctrl.Width;
                    break;
                }
            }
            Hashtable htFlow = new Hashtable();
            Hashtable htProcess = new Hashtable();
            foreach (Control ctrl in this.Parent.Controls)
            {
                if (ctrl.Left > deleteCtrl.Left)
                {
                    ctrl.Left -= (deleteCtrl.Width + flowWidth);
                }
                if (ctrl is FunctionButton && ctrl != deleteCtrl)
                {
                    htProcess.Add((ctrl as FunctionButton).ProcessID, ctrl);
                }
                if (ctrl is FlowButton && ctrl != deleteCtrl)
                {
                    htFlow.Add((ctrl as FlowButton).ProcessID, ctrl);
                }
            }


            Control ctrlParent = deleteCtrl.Parent;
            string wfID = deleteCtrl.WorkFlowID;
            this.Parent.Controls.Clear();

            foreach (string code in htProcess.Keys)
            {
                if (htProcess.ContainsKey(code))
                    ctrlParent.Controls.Add(htProcess[code] as FunctionButton);
            }
            foreach (string code in htFlow.Keys)
            {
                if (htFlow.ContainsKey(code))
                    ctrlParent.Controls.Add(htFlow[code] as FlowButton);
            }

            //			Sys_WorkFlowClass swc = new Sys_WorkFlowClass();
            //			Hashtable htFLows = swc.GetDeSerializeFlow(wfID,htProcess);
            //
            //						
            //			foreach(string code in htFLows.Keys)
            //			{
            //				if(htFLows.ContainsKey(code))
            //				{
            //					FlowButton fb = htFLows[code] as FlowButton;
            //					if(fb.Left > deleteCtrl.Left)
            //						fb.Left -= (deleteCtrl.Width + flowWidth);
            //					ctrlParent.Controls.Add(fb);
            //				}
            //			}
        }

        /// <summary>
        /// </summary>
        private void miDelete_Click(object sender, EventArgs e)
        {
            //这里要判断，如果是正常流程，按照下面操作并调整位置，如果是维修流程，需要把和维修相关的全部删除
            //因为维修流程进出都是双向的，可否考虑用此来判断？


            DeleteLinks();
            //把后续的控件调整一下，全部往前调整。
            MoveRightControls(this);
            //this.Parent.Controls.Remove(this);
        }

        private string fProcessName = string.Empty;
        private string fProcessID = string.Empty;       
        private int fProcessStatus = -1;
        private PointCollection fHotPoints = null;
        private FlowButtonCollection fInFlows = null;
        private FlowButtonCollection fOutFlows = null;
       
        //private FlowButton fLeftFlow = null;
        //private FlowButton fRightFlow = null;
        //private FlowButton fUpFlow = null;
        //private FlowButton fDownFlow = null;
        private string fWorkFlowID = "-1";
        private string fProcessCode = string.Empty;
        /***************************Begin****************************/
        //是否是抽检类型
        private bool isInspectStyle;
        //抽检比例
        private decimal inspectPer = 0M;
        /***************************End******************************/

        public string WorkFlowID
        {
            get
            {
                return fWorkFlowID;
            }
            set
            {
                fWorkFlowID = value;
            }
        }

        //public FlowButton LeftFlow
        //{
        //    get
        //    {
        //        return fLeftFlow;
        //    }
        //    set
        //    {
        //        fLeftFlow = value;
        //    }
        //}

        //public FlowButton RightFlow
        //{
        //    get
        //    {
        //        return fRightFlow;
        //    }
        //    set
        //    {
        //        fRightFlow = value;
        //    }
        //}

        //public FlowButton UpFlow
        //{
        //    get
        //    {
        //        return fUpFlow;
        //    }
        //    set
        //    {
        //        fUpFlow = value;
        //    }
        //}

        //public FlowButton DownFlow
        //{
        //    get
        //    {
        //        return fDownFlow;
        //    }
        //    set
        //    {
        //        fDownFlow = value;
        //    }
        //}

        public string ProcessName
        {
            get
            {
                return fProcessName;
            }
            set
            {
                if (fProcessName != value)
                {
                    CheckOneName(value);
                    fProcessName = value;
                    Text = fProcessName;
                }
            }
        }
        public string ProcessCode
        {
            get
            {
                return fProcessCode;
            }
            set
            {
                fProcessCode = value;
            }
        }    
        /// <summary>
        /// 全部大写
        /// </summary>
        public string ProcessID
        {
            get
            {
                if (fProcessID == string.Empty)
                    fProcessID = Guid.NewGuid().ToString().ToUpper();
                return fProcessID.ToUpper();
            }
            set
            {
                fProcessID = value.ToUpper();
            }
        }

        public int ProcessStatus
        {
            get
            {
                return fProcessStatus;
            }
        }

        public PointCollection HotPoints
        {
            get
            {
                if (fHotPoints == null)
                    fHotPoints = new PointCollection();
                return fHotPoints;
            }
        }

        public FlowButtonCollection InFlows
        {
            get
            {
                if (fInFlows == null)
                    fInFlows = new FlowButtonCollection();
                return fInFlows;
            }
        }

        public FlowButtonCollection OutFlows
        {
            get
            {
                if (fOutFlows == null)
                    fOutFlows = new FlowButtonCollection();
                return fOutFlows;
            }
        }

        public Point CenterPoint
        {
            get
            {
                return new Point(this.Left + this.Width / 2, this.Top + this.Height / 2);
            }
        }

        /***************************Begin****************************/
        //增加FuncationButton的抽检比例属性

        /// <summary>
        /// 抽检比例
        /// </summary>
        public decimal InspectPer
        {
            get
            {
                if (isInspectStyle == true && inspectPer >= 0 && inspectPer <= 1)
                {
                    //如果是抽检类型&&抽检比例大于零
                    return inspectPer;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value >= 0 && value <= 1)
                {
                    inspectPer = value;
                }
                else
                {
                    inspectPer = 0;
                }
            }
        }

        /// <summary>
        /// 是否是抽检类型
        /// </summary>
        public bool IsInspectStyle
        {
            get
            {
                return isInspectStyle;
            }
            set
            {
                isInspectStyle = value;
            }
        }
        /***************************End******************************/
    }
}
