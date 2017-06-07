/***********************************************************************
 * Module:  DownArrowButton.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.DownArrowButton
 ***********************************************************************/

using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using BenQGuru.eMES.DrawFlow.Data;

namespace BenQGuru.eMES.DrawFlow.Controls
{
   /// <summary>
   /// </summary>
   public class DownArrowButton : FlowButton
   {
      /// <summary>
      /// </summary>
      public DownArrowButton()
      {
      }
   
      /// <summary>
      /// </summary>
      public override void DrawButton()
      {
      	DrawUtility.DrawTwoArrow(this,1,3.1415926/2,true);
      }
      
      /// <summary>
      /// </summary>
      protected override void CreateContextMenu()
      {
      	if(ContextMenu == null)
      	{
      		ContextMenu = new ContextMenu();
      	}
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
      	int x = this.Left + 2 * DrawUtility.a - (DrawUtility.rw + DrawUtility.ah) * DrawUtility.a / 2 - 4;
      	int y = this.Top + (DrawUtility.rw + DrawUtility.ah) * DrawUtility.a;
      	int width = (DrawUtility.rw + DrawUtility.ah) * DrawUtility.a;
      	int height = 4 * DrawUtility.a;
      	Rectangle rect = new Rectangle(x,y,width,height);
      	linkAreas.Add(rect);
      }
   
      /// <summary>
      /// </summary>
      private void miProcess_Click(object sender, EventArgs e)
      {
      	//在下边增加一个新的进程
      	ProcessButton pb = new ProcessButton();
      	if(this.FromProcesses.Count > 0)
      	{
      		pb.Left = (FromProcesses[0] as ProcessButton).Left;
      		pb.Top = this.Top + this.Height;
      	}
      	else
      	{
      		pb.Left = this.Left - (DrawUtility.rw + DrawUtility.ah - 4) * DrawUtility.a / 2;
      		pb.Top = this.Top + this.Height;
      	}
      	this.Parent.Controls.Add(pb);
      	pb.InFlows.Add(this);
      	this.ToProcesses.Add(pb);
      }
      
      /// <summary>
      /// </summary>
      private void miEnd_Click(object sender, EventArgs e)
      {
      	EndButton eb = new EndButton();
      	eb.Left = this.Left  - DrawUtility.a;
      	eb.Top = this.Top + this.Height;
      	this.Parent.Controls.Add(eb);
      	eb.InFlows.Add(this);
      	this.ToProcesses.Add(eb);
      }
   
   }
}