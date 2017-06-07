/***********************************************************************
 * Module:  LeftArrowButton.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.LeftArrowButton
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
   public class LeftArrowButton : FlowButton
   {
      /// <summary>
      /// </summary>
       public override void DrawButton()
      {
      	//DrawUtility.DrawLeftArrow(this,null);
      }
      
      /// <summary>
      /// </summary>
      protected override void CreateContextMenu()
      {
      }
   
   }
}