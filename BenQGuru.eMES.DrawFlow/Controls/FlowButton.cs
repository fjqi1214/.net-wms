using System;
using System.Collections;
using System.Windows.Forms;
using BenQGuru.eMES.DrawFlow.Data;
using System.IO;
using System.Drawing;

namespace BenQGuru.eMES.DrawFlow.Controls
{
   /// <summary>
   /// 箭头流程按钮
   /// </summary>
   public abstract class FlowButton : PictureBox, IArrowInfo
   {
      /// <summary>
      /// </summary>
      public FlowButton()
      {	
      	this.Click += new EventHandler(FlowButton_Click);
      	//this.Paint += new PaintEventHandler(FlowButton_Paint);
      	this.Text = string.Empty;
      	//FlatStyle = FlatStyle.Flat;
        this.SizeMode = PictureBoxSizeMode.StretchImage;
          //右击菜单，被屏蔽
      	//CreateContextMenu();
        //AddGeneralMenu();
      }
      
	   private string fProcessID = string.Empty;
	   public string ProcessID
	   {
		   get
		   {
			   if(fProcessID == string.Empty)
				   fProcessID = Guid.NewGuid().ToString().ToUpper();
			   return fProcessID.ToUpper();
		   }
		   set
		   {
			   fProcessID = value.ToUpper();
		   }
	   }

      /// <summary>
      /// </summary>
      public void SetLinkControls()
      {		
      	if(this.LinkArea.Count == 0)
      		return;
      
        //foreach(Control ctrl in this.Parent.Controls)
        //{
        //    if(ctrl is ProcessButton)
        //    {					
        //        if(FormUtility.PointInRect(this.LinkArea[0],((ctrl as ProcessButton).CenterPoint)))
        //        {
        //            //这里去筛选有否控件正好处在这个位置，如果是，则提示建立关联
        //            ProcessButton pbLink = ctrl as ProcessButton;
        //            if(pbLink != null)
        //            {
        //                if(FormUtility.MessageBoxOK(string.Format("要与进程{0}建立关联吗？",pbLink.ProcessName)))
        //                {
        //                    ToProcesses.RemoveObject(pbLink);
        //                    ToProcesses.Add(pbLink);
        //                    return;
        //                }
        //            }						
        //        }
        //    }
        //}			
      }
      
      /// <summary>
      /// </summary>
      public void DeleteLinks()
      {
      	foreach(FunctionButton funcBtn in FromProcesses)
      	{
            //ResetFlow(funcBtn);
      		funcBtn.OutFlows.RemoveObject(this);				
      	}
      	foreach(FunctionButton funcBtn in ToProcesses)
      	{
            //ResetFlow(funcBtn);
      		funcBtn.InFlows.RemoveObject(this);
      	}
      }
   
      /// <summary>
      /// </summary>
      protected abstract void CreateContextMenu();
      
      /// <summary>
      /// </summary>
      public abstract void DrawButton();
      
      /// <summary>
      /// </summary>
      protected virtual void SetLinkArea(RectangleCollection linkAreas)
      {
      }

      public virtual FunctionButton AddOutProcess(string processName)
      {
          return null;
      }

      public virtual EndButton AddEnd()
      {
          return null;
      }

      /// <summary>
      /// </summary>
      private void AddGeneralMenu()
      {
      	if(ContextMenu == null)
      		ContextMenu = new ContextMenu();
      
      	//删除
      	MenuItem miDelete = new MenuItem();
      	miDelete.Text = "删除该流程";
      	miDelete.Click += new EventHandler(miDelete_Click);
      	ContextMenu.MenuItems.Add(miDelete);
      }
      
      /// <summary>
      /// </summary>
      private void miDelete_Click(object sender, EventArgs e)
      {		
      	DeleteLinks();
      	this.Parent.Controls.Remove(this);
      }
      
      ///// <summary>
      ///// </summary>
      //private void ResetFlow(FunctionButton funcBtn)
      //{
      //  if(funcBtn.LeftFlow == this)
      //      funcBtn.LeftFlow = null;
      //  if(funcBtn.RightFlow == this)
      //      funcBtn.RightFlow = null;
      //  if(funcBtn.UpFlow == this)
      //      funcBtn.UpFlow = null;
      //  if(funcBtn.DownFlow == this)
      //      funcBtn.DownFlow = null;
      //}
      
      /// <summary>
      /// 流程图箭头点击事件--屏蔽
      /// </summary>
      private void FlowButton_Click(object sender, EventArgs e)
      {
        //FlowSettingForm fsf = new FlowSettingForm(this);
        //DialogResult dr = fsf.ShowDialog();
        //if(dr == DialogResult.OK)
        //{
        //}
        //else
        //{
        //}
      }

      // <summary>
      // </summary>
      //private void FlowButton_Paint(object sender, PaintEventArgs e)
      //{
      //    this.Text = string.Empty;
      //    Cursor = Cursors.Hand;
      //    DrawButton();
      //    this.Paint -= new PaintEventHandler(FlowButton_Paint);
      //}
   
      private string fArrowName = string.Empty;
      private string fArrowID = string.Empty;
      private int fArrowStatus = -1;
      private bool fDoubleArrow = false;
      private PointCollection fHotPoints = null;
      private FunctionButtonCollection fFromProcesses = null;
      private FunctionButtonCollection fToProcesses = null;
      private RectangleCollection fLinkArea = null;
      private string fCondition = "(1=1)";
   
      public string Condition
      {
         get
         {
         	return fCondition;
         }
         set
         {
         	fCondition = value;
         }
      }
      
      public bool DoubleArrow
      {
         get
         {
			 if(this is UpDownArrowButton || this is DefineArrowButton || this is DownArrowButton)
				 return true;
			 return false;
         }
         set
         {
         	fDoubleArrow = true;
         }
      }
      
      public string ArrowName
      {
         get
         {
         	return fArrowName;
         }
         set
         {
         	fArrowName = value;
         	Text = fArrowName;
         }
      }
      
      public string ArrowID
      {
         get
         {
         	if(fArrowID == string.Empty)
         		fArrowID = Guid.NewGuid().ToString();
         	return fArrowID;
         }
         set
         {
         	fArrowID = value;
         }
      }
      
      public int ArrowStatus
      {
         get
         {
         	return fArrowStatus;
         }
      }
      
      public PointCollection HotPoints
      {
         get
         {
         	if(fHotPoints == null)
         	{
         		fHotPoints = new PointCollection();
         	}
         	return fHotPoints;
         }
      }
      
      public FunctionButtonCollection FromProcesses
      {
         get
         {
         	if(fFromProcesses == null)
         	{
         		fFromProcesses = new FunctionButtonCollection();
         	}
         	return fFromProcesses;
         }
      }
      
      public FunctionButtonCollection ToProcesses
      {
         get
         {
         	if(fToProcesses == null)
         	{
         		fToProcesses = new FunctionButtonCollection();
         	}
         	return fToProcesses;
         }
      }
      
      public RectangleCollection LinkArea
      {
         get
         {
         	if(fLinkArea == null)
         	{
         		fLinkArea = new RectangleCollection();
         		SetLinkArea(fLinkArea);
         	}
         	return fLinkArea;
         }
      }
   
   }
}