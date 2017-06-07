/***********************************************************************
 * Module:  PointCollection.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.PointCollection
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
   public class PointCollection : CollectionBase
   {
      /// <summary>
      /// </summary>
      public PointCollection()
      {
      }
      
      /// <summary>
      /// </summary>
      public Point Add(Point pt)
      {
      	List.Add(pt);
      	return pt;
      }
   
   }
}