/***********************************************************************
 * Module:  RightDownArrowButton.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.RightDownArrowButton
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
   public class RightDownArrowButton : FlowButton
   {
      /// <summary>
      /// </summary>
       public override void DrawButton()
      {
      	//DrawUtility.DrawRightDownArrow(this,null);
      }
      
      /// <summary>
      /// </summary>
      protected override void CreateContextMenu()
      {
      }
   
   }
}