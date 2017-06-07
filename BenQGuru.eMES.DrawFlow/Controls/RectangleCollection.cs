/***********************************************************************
 * Module:  RectangleCollection.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.RectangleCollection
 ***********************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using BenQGuru.eMES.DrawFlow.Data;

namespace BenQGuru.eMES.DrawFlow.Controls
{
   /// <summary>
   /// </summary>
   public class RectangleCollection : CollectionBase
   {
      /// <summary>
      /// </summary>
      public RectangleCollection()
      {
      }
      
      /// <summary>
      /// </summary>
      public Rectangle Add(Rectangle rect)
      {
      	List.Add(rect);
      	return rect;
      }
   
      public Rectangle this[int index]
      {
         get
         {
         	return (Rectangle)(List[index]);
         }
      }
   
   }
}