/***********************************************************************
 * Module:  UpDownArrowButton.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.UpDownArrowButton
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
   public class UpDownArrowButton : FlowButton
   {
      /// <summary>
      /// </summary>
       public override void DrawButton()
      {
      	//DrawUtility.DrawUpDownArrow(this,null);
      }
      
      /// <summary>
      /// </summary>
      protected override void CreateContextMenu()
      {
      	if(ContextMenu == null)
      	{
      		ContextMenu = new ContextMenu();
      	}
      
      
      	MenuItem miDownProcess = new MenuItem();
      	miDownProcess.Text = "创建下方进程";
      	miDownProcess.Click += new EventHandler(miDownProcess_Click);
      	ContextMenu.MenuItems.Add(miDownProcess);
      
      	MenuItem miEnd = new MenuItem();
      	miEnd.Text = "创建结束符";
      	miEnd.Click += new EventHandler(miEnd_Click);
      	ContextMenu.MenuItems.Add(miEnd);
      }
   
      /// <summary>
      /// </summary>
      private void miDownProcess_Click(object sender, EventArgs e)
      {
      	//在下边增加一个新的进程
      	ProcessButton pb = new ProcessButton();
      	pb.Left = this.Left - (int)(4.5 * DrawUtility.a);
      	pb.Top = this.Top + this.Height - 4;
      	this.Parent.Controls.Add(pb);
      	pb.InFlows.Add(this);
      	this.ToProcesses.Add(pb);
      }
      
      /// <summary>
      /// </summary>
      private void miEnd_Click(object sender, EventArgs e)
      {
      	EndButton eb = new EndButton();
      	eb.Left = this.Left  + DrawUtility.a;
      	eb.Top = this.Top + this.Height - 4;
      	this.Parent.Controls.Add(eb);
      	eb.InFlows.Add(this);
      	this.ToProcesses.Add(eb);
      
      }
   
   }
}