/***********************************************************************
 * Module:  JumpArrowButton.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.JumpArrowButton
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
   public class JumpArrowButton : FlowButton
   {
      /// <summary>
      /// </summary>
      public JumpArrowButton(bool isBack, int length)
      {
      	fIsBack = isBack;
      	fLength = length;
      }
   
      /// <summary>
      /// </summary>
      public override void DrawButton()
      {
      	DrawUtility.DrawJumpArrow(this,null,fLength,fIsBack);
      
      }
      
      /// <summary>
      /// </summary>
      protected override void CreateContextMenu()
      {
      	if(ContextMenu == null)
      	{
      		ContextMenu = new ContextMenu();
      	}
      }
      
      /// <summary>
      /// </summary>
      protected override void SetLinkArea(RectangleCollection linkAreas)
      {
      	/*
      	int x = this.Left + (DrawUtility.rw + DrawUtility.ah) * 3 + 
      	int y = this.Top;
      	int width = this.Width;
      	int height = this.Height;
      	Rectangle rect = new Rectangle(x,y,width,height);
      	linkAreas.Add(rect);
      	*/
      }
   
      private bool fIsBack = false;
      private int fLength = 0;
   
   }
}