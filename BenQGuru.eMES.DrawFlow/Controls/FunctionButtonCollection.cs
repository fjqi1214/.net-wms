/***********************************************************************
 * Module:  FunctionButtonCollection.cs
 * Author:  Administrator
 * Purpose: Definition of the Class Flow.Controls.FunctionButtonCollection
 ***********************************************************************/

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using BenQGuru.eMES.DrawFlow.Data;

namespace BenQGuru.eMES.DrawFlow.Controls
{
   /// <summary>
   /// </summary>
   public class FunctionButtonCollection : CollectionBase
   {
      /// <summary>
      /// </summary>
      public FunctionButtonCollection()
      {
      }
      
      /// <summary>
      /// </summary>
      public FunctionButton Add(FunctionButton fb)
      {
      	//如果已经存在，则不用增加了
      	if(!List.Contains(fb))
      		List.Add(fb);
      	return fb;
      }
      
      /// <summary>
      /// </summary>
      public void RemoveObject(FunctionButton fb)
      {
      	if(List.Contains(fb))
      		List.Remove(fb);
      }
   
      public FunctionButton this[int index]
      {
         get
         {
         	if(index < 0 || index >= List.Count)
         	{
         		throw new Exception("数组超出界限");
         	}
         	return List[index] as FunctionButton;
         }
      }
   
   }
}