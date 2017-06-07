/***********************************************************************
 * Module:  FlowButtonCollection.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.FlowButtonCollection
 ***********************************************************************/

using System;
using System.Collections;
using System.Windows.Forms;
using BenQGuru.eMES.DrawFlow.Data;
using System.IO;
using System.Drawing;

namespace BenQGuru.eMES.DrawFlow.Controls
{
   /// <summary>
   /// </summary>
   public class FlowButtonCollection : CollectionBase
   {
      /// <summary>
      /// </summary>
      public FlowButtonCollection()
      {			
      }
      
      /// <summary>
      /// </summary>
      public FlowButton Add(FlowButton fb)
      {
      	List.Add(fb);
      	return fb;
      }
      
      /// <summary>
      /// </summary>
      public void RemoveObject(FlowButton fb)
      {
      	if(List.Contains(fb))
      		List.Remove(fb);
      }
   
      public FlowButton this[int index]
      {
         get
         {
         	if(index < 0 || index >= List.Count)
         	{
         		throw new Exception("数组超出界限");
         	}
         	return List[index] as FlowButton;
         }
      }
   
   }
}